using System;
using System.Net;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Text;
using System.Globalization;
using CMSInterface.SoapClients.Acres;
using CMSInterface.SoapClients.Acsc;
using CMSInterface.SoapClients.Oasis;
using TPUtilities.IniHandler;
using TPUtilities;
using TPUtilities.DBConnector;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using AccessManagementLib;


namespace PromoWebServices.security
{
	/// <summary>
	/// The Credentials Manager Encrypts and Decrypts
	/// UserNames, Passwords, DSN etc for storage and retrieval
	/// to and from an ini file located on in the WebServices directory.
	/// This information is used for loggin into the database.
	/// </summary>
	public class CredentialsManager
	{
		// Stores the structure of a standard, Windows ini file.
		private IniHandler.IniStructure m_Inistruct = null;

		// Used for logging and reading global ini information.
		// Gets the absolute path of the WebService which has called this class.
		private TPUtilities.TPUtilities m_Utils = TPUtilities.TPUtilities.Instance;

		private string m_IniFileName = null;

		// Validates and Encrypts
		private KeyValidate m_Encryptor = null;

		private string m_TPUserID = null;
		private string m_TPPassword = null;

		// Constants
		//public static readonly int SET_SIZE = 1024;
		public static readonly int PROMO_CONSTANT = 247956;


		public CredentialsManager(string pi_propertyname, string pi_contractnumber, string pi_install, int pi_propertyid )
		{
			m_Encryptor = KeyValidate.getKeyValidator("TR_TP", pi_propertyname, pi_contractnumber, pi_install);

			if(m_Encryptor == null)
			{
				throw(new Exception("Either the Install Key, Property Name, or Contract Number is wrong..."));
			}
			else
			{
				m_IniFileName = m_Utils.AppPath + "ENC_FILE.ini";
				this.loadProps();
				UserSecurityInfo info = this.getServiceConnectionInfo();
				this.m_TPUserID = info.LogName;
				this.m_TPPassword = info.Password;
			}
		}


		public void storeCasinoInfo(string pi_propertyname, string pi_contractnumber, int pi_propertyid)
		{
			TPDBConnection tpconn = new TPDBConnection();
			tpconn.UserID = this.m_TPUserID;
			tpconn.Password = this.m_TPPassword;

			tpconn.setCasinoInfo(pi_propertyname, pi_contractnumber, pi_propertyid);
			tpconn.releaseResources();
			tpconn.close();
		}

		/// <summary>
		/// Gets the connection info for the TotalPromo client application
		/// </summary>
		/// <returns>UserSecurityInfo for logging in</returns>
		public UserSecurityInfo getClientConnectionInfo()
		{
			string sdsn = this.decryptString(this.m_Inistruct.GetValue("Security", "TPCLIENT_DSN"));
			string sname = this.decryptString(this.m_Inistruct.GetValue("Security", "TPCLIENT_UserName"));
			string spass = this.decryptString(this.m_Inistruct.GetValue("Security", "TPCLIENT_Password"));

			return new UserSecurityInfo(sdsn, sname, spass);
		}

		/// <summary>
		/// Gets the connection information for connecting to a CMS database
		/// </summary>
		/// <returns>UserSecurityInfo for logging in</returns>
		public UserSecurityInfo getCMSConnectionInfo()
		{
			string sname = this.decryptString(this.m_Inistruct.GetValue("Security", "CMS_UserName"));
			string spass = this.decryptString(this.m_Inistruct.GetValue("Security", "CMS_Password"));

			return new UserSecurityInfo(sname, spass);
		}

		/// <summary>
		/// Gets the connection information for parsing services
		/// </summary>
		/// <returns>UserSecurityInfo for logging in</returns>
		public UserSecurityInfo getServiceConnectionInfo()
		{
			string sname = this.decryptString(this.m_Inistruct.GetValue("Security", "SERVICE_UserName"));
			string spass = this.decryptString(this.m_Inistruct.GetValue("Security", "SERVICE_Password"));

			return new UserSecurityInfo(sname, spass);
		}

		public bool moduleInstalled(string pi_modulename)
		{
			TPDBConnection tpconn = new TPDBConnection();
			tpconn.UserID = this.m_TPUserID;
			tpconn.Password = this.m_TPPassword;

			string userinstallvalue = tpconn.getModuleHash(pi_modulename);

			return this.m_Encryptor.validateModuleKey(pi_modulename, userinstallvalue);
		}

		public bool ActivateModule(string pi_modulename, string pi_activationcode)
		{
			TPDBConnection tpconn = new TPDBConnection();
			tpconn.UserID = this.m_TPUserID;
			tpconn.Password = this.m_TPPassword;

			string userinstallvalue = tpconn.getModuleHash(pi_modulename);

			if(userinstallvalue != null)
			{
				if(this.m_Encryptor.validateModuleKey(pi_modulename, userinstallvalue))
				{
					return true;
				}
			}
			
			if(this.m_Encryptor.validateModuleKey(pi_modulename, pi_activationcode))
			{
				return tpconn.setModuleHash(pi_modulename, pi_activationcode);
			}

			return false;

		}

		/// <summary>
		/// Sets the connection info for the TotalPromo client application
		/// </summary>
		/// <returns>true if success false otherwize</returns>
		public string saveNewClientConnectionInfo(string pi_dsn, string pi_username, string pi_password, string pi_sauser, string pi_sapass)
		{
			string result = "";

			TPDBConnection conn = new TPDBConnection();
			conn.UserID = pi_sauser;
			conn.Password = pi_sapass;
			IDbConnection dbconn = conn.Open(pi_sauser, pi_sapass);
			if(dbconn != null)
			{
				if(dbconn.State == ConnectionState.Open)
				{
					try
					{
						conn.close();
					}
					catch{}
					dbconn = conn.Open(pi_username, pi_password);

					if(dbconn != null)
					{
						if(dbconn.State == ConnectionState.Open)
						{
							conn.close();

							this.loadProps();
							this.m_Inistruct.ModifyValue("Security", "TPCLIENT_DSN", this.encryptString(pi_dsn));
							this.m_Inistruct.ModifyValue("Security", "TPCLIENT_UserName", this.encryptString(pi_username));
							this.m_Inistruct.ModifyValue("Security", "TPCLIENT_Password", this.encryptString(pi_password));
							bool res = this.saveProps();

							if(!res)
							{
								result = "<Result>\n\t<SUCCESS>false</SUCCESS>\n\t<MESSAGE>Error Writing New Name and Password to File</MESSAGE>\n</Result>";
							}
							else
							{
								result = "<Result>\n\t<SUCCESS>true</SUCCESS>\n\t<MESSAGE>New Name and Password Correctly Stored</MESSAGE>\n</Result>";
							}
						}
						else
						{
							result = "<Result>\n\t<SUCCESS>false</SUCCESS>\n\t<MESSAGE>New User Name or Password is Invalid</MESSAGE>\n</Result>";
						}

					}
					else
					{
						result = "<Result>\n\t<SUCCESS>false</SUCCESS>\n\t<MESSAGE>New User Name or Password is Invalid</MESSAGE>\n</Result>";
					}
				}
				else
				{
					result = "<Result>\n\t<SUCCESS>false</SUCCESS>\n\t<MESSAGE>Admin User Name or Password is Invalid</MESSAGE>\n</Result>";
				}
			}
			else
			{
				result = "<Result>\n\t<SUCCESS>false</SUCCESS>\n\t<MESSAGE>Admin User Name or Password is Invalid</MESSAGE>\n</Result>";
			}

			return result;
		}

		/// <summary>
		/// Sets the connection info for the CMS database
		/// </summary>
		/// <returns>true if success false otherwize</returns>
		public string saveNewCMSConnectionInfo(string pi_username, string pi_password, string pi_sauser, string pi_sapass)
		{
			string result = "";

			//OasisDBConnection conn = new OasisDBConnection();
			//IDbConnection dbconn = conn.Open(pi_sauser, pi_sapass);
			string cmstype = AppSettings.getProperty("InterfaceType").ToString();
			string cmssystem = AppSettings.getProperty("InterfaceSystem").ToString();
			if(cmstype.ToLower().Equals("socket"))
			{
				result = "<Result>\n\t<SUCCESS>false</SUCCESS>\n\t<MESSAGE>A Socket based CMS System is invalid for this operation</MESSAGE>\n</Result>";
			}
			else
			{
				bool dbconn = false;

				if(cmssystem.ToLower().Equals("ACRES"))
				{
					AcresRatingsInterface rats = new AcresRatingsInterface(AppSettings.getProperty("CMSServer").ToString());
					dbconn = rats.loginOK(pi_sauser, pi_sapass, pi_username, pi_password);
				}
				else if(cmssystem.ToLower().Equals("OASIS"))
				{
					OasisRatingsInterface rats = new OasisRatingsInterface(AppSettings.getProperty("CMSServer").ToString());
					dbconn = rats.loginOK(pi_sauser, pi_sapass, pi_username, pi_password);
				}
				else
				{
					dbconn = true;
				}


				if(dbconn)
				{

					this.loadProps();
					this.m_Inistruct.ModifyValue("Security", "CMS_UserName", this.encryptString(pi_username));
					this.m_Inistruct.ModifyValue("Security", "CMS_Password", this.encryptString(pi_password));
					bool res = this.saveProps();

					if(!res)
					{
						result = "<Result>\n\t<SUCCESS>false</SUCCESS>\n\t<MESSAGE>Error Writing New Name and Password to File</MESSAGE>\n</Result>";
					}
					else
					{
						result = "<Result>\n\t<SUCCESS>true</SUCCESS>\n\t<MESSAGE>New Name and Password Correctly Stored</MESSAGE>\n</Result>";
					}
				}
				else
				{
					result = "<Result>\n\t<SUCCESS>false</SUCCESS>\n\t<MESSAGE>New User Name or Password is Invalid</MESSAGE>\n</Result>";
				}
			}


			return result;
		}

		/// <summary>
		/// Sets the connection info for the parsing services
		/// </summary>
		/// <returns>true if success false otherwize</returns>
		public string saveNewServiceConnectionInfo(string pi_username, string pi_password, string pi_sauser, string pi_sapass)
		{
			string result = "";

			TPDBConnection conn = new TPDBConnection();
			IDbConnection dbconn = conn.Open(pi_sauser, pi_sapass);
			if(dbconn != null)
			{
				if(dbconn.State == ConnectionState.Open)
				{
					conn.close();

					dbconn = conn.Open(pi_username, pi_password);

					if(dbconn != null)
					{
						if(dbconn.State == ConnectionState.Open)
						{
							conn.close();

							this.loadProps();
							this.m_Inistruct.ModifyValue("Security", "SERVICE_UserName", this.encryptString(pi_username));
							this.m_Inistruct.ModifyValue("Security", "SERVICE_Password", this.encryptString(pi_password));
							bool res = this.saveProps();

							if(!res)
							{
								result = "<Result>\n\t<SUCCESS>false</SUCCESS>\n\t<MESSAGE>Error Writing New Name and Password to File</MESSAGE>\n</Result>";
							}
							else
							{
								result = "<Result>\n\t<SUCCESS>true</SUCCESS>\n\t<MESSAGE>New Name and Password Correctly Stored</MESSAGE>\n</Result>";
							}
						}
						else
						{
							result = "<Result>\n\t<SUCCESS>false</SUCCESS>\n\t<MESSAGE>New User Name or Password is Invalid</MESSAGE>\n</Result>";
						}

					}
					else
					{
						result = "<Result>\n\t<SUCCESS>false</SUCCESS>\n\t<MESSAGE>New User Name or Password is Invalid</MESSAGE>\n</Result>";
					}
				}
				else
				{
					result = "<Result>\n\t<SUCCESS>false</SUCCESS>\n\t<MESSAGE>Admin User Name or Password is Invalid</MESSAGE>\n</Result>";
				}
			}
			else
			{
				result = "<Result>\n\t<SUCCESS>false</SUCCESS>\n\t<MESSAGE>Admin User Name or Password is Invalid</MESSAGE>\n</Result>";
			}


			return result;
		}
		
		/// <summary>
		/// Loads the encrypted connection information from the local ini file
		/// </summary>
		private void loadProps()
		{
			TPUtilities.IniHandler.IniHandler handler = TPUtilities.IniHandler.IniHandler.Instance;

			try
			{
				m_Inistruct = handler.ReadIni(m_IniFileName);
			}
			catch
			{
				m_Inistruct = null;
			}
			
			if(m_Inistruct == null)
			{
				m_Inistruct = new IniHandler.IniStructure();
				m_Inistruct.AddSection("Security");
				m_Inistruct.AddValue("Security", "TPCLIENT_DSN", this.encryptString("sweepstakes"));
				m_Inistruct.AddValue("Security", "TPCLIENT_UserName", this.encryptString("trpromo"));
				m_Inistruct.AddValue("Security", "TPCLIENT_Password", this.encryptString("sweep"));
				m_Inistruct.AddValue("Security", "CMS_UserName", this.encryptString("trpromo"));
				m_Inistruct.AddValue("Security", "CMS_Password", this.encryptString("sweep"));
				m_Inistruct.AddValue("Security", "SERVICE_UserName", this.encryptString("trpromo"));
				m_Inistruct.AddValue("Security", "SERVICE_Password", this.encryptString("sweep"));

                handler.WriteIni(m_Inistruct, m_IniFileName);
			}
		}

		/// <summary>
		/// Stores the encrypted connection information to the local ini file
		/// </summary>
		private bool saveProps()
		{
			bool success = false;
			TPUtilities.IniHandler.IniHandler handler = TPUtilities.IniHandler.IniHandler.Instance;
			
			if(m_Inistruct != null)
			{
				success = handler.WriteIni(m_Inistruct, m_IniFileName);
			}
			
			return success;
		}

		#region Encryption Code


		/// <summary>
		/// Decrypts a given encrypted string
		/// </summary>
		/// <param name="pi_todecrypt">string to decrypt</param>
		/// <returns>Unencrypted string</returns>
		private string decryptString(string pi_todecrypt)
		{
			return this.m_Encryptor.decryptString(pi_todecrypt);
		}

		/// <summary>
		/// Encrypts a given plain text string
		/// </summary>
		/// <param name="pi_toencrypt">string to encrypt</param>
		/// <returns>Encrypted string</returns>
		private string encryptString(string pi_toencrypt)
		{
			return this.m_Encryptor.encryptString(pi_toencrypt);
		}
		#endregion
	}
}
