using System;
using System.Collections;
using System.Text;
using System.Globalization;

namespace AccessManagementLib
{
	/// <summary>
	/// Summary description for KeyGen.
	/// </summary>
	public sealed class KeyValidate
	{
		private string m_PublicKey = null;
		private EncryptionUtility.PublicKeyDelegate m_Delegate = null;
		private string m_AppName = null;
		private string m_PropertyName = null;
		private string m_ContractNumber = null;

		private KeyValidate(string pi_appname)
		{
			m_AppName = pi_appname;
			m_Delegate = new EncryptionUtility.PublicKeyDelegate(GetPublicKey);
		}

		private UInt64 GetPublicKey()
		{
			if(m_PublicKey == null)
				return 0;
			else
			{
				string strpkey = m_PublicKey.Replace("-", "");
				return UInt64.Parse(strpkey, System.Globalization.NumberStyles.AllowHexSpecifier);
			}
		}	

		public string InstallKey
		{
			get
			{
				return m_PublicKey;
			}
		}

		private bool validateInstallKey(string pi_propertyname, string pi_contractnum, string pi_userinstallkey)
		{
			EncryptionUtility eu = new EncryptionUtility(m_AppName, m_Delegate);
			return eu.validateInstallKey(pi_propertyname, pi_contractnum, pi_userinstallkey);
		}

		public bool validateModuleKey(string pi_modulename, string pi_usermodulekey)
		{
			EncryptionUtility eu = new EncryptionUtility(m_AppName, m_Delegate);
			return eu.validateInstallKey(m_PropertyName, m_ContractNumber + pi_modulename.Substring(0, 3).ToUpper(), pi_usermodulekey);
		}

		public string encryptString(string pi_toencrypt)
		{
			EncryptionUtility eu = new EncryptionUtility(m_AppName, m_Delegate);
			return eu.encryptString(pi_toencrypt);
		}

		public string decryptString(string pi_todecrypt)
		{
			EncryptionUtility eu = new EncryptionUtility(m_AppName, m_Delegate);
			return eu.decryptString(pi_todecrypt);
		}

		public static bool isInstallKeyValid(string pi_appname, string pi_propertyname, string pi_contractnum, string pi_userinstallkey)
		{
			KeyValidate ae = new KeyValidate(pi_appname);
			return ae.validateInstallKey(pi_propertyname, pi_contractnum, pi_userinstallkey);
		}

		public static bool isModuleKeyValid(string pi_appname, string pi_propertyname, string pi_contractnum, string pi_modulename, string pi_userinstallkey)
		{
			KeyValidate ae = getKeyValidator(pi_appname, pi_propertyname, pi_contractnum, pi_userinstallkey);
			return ae.validateModuleKey(pi_modulename, pi_userinstallkey);
		}

		public static KeyValidate getKeyValidator(string pi_appname, string pi_propertyname, string pi_contractnum, string pi_userinstallkey)
		{
			KeyValidate ae = new KeyValidate(pi_appname);
			if(!ae.validateInstallKey(pi_propertyname, pi_contractnum, pi_userinstallkey))
			{
				return null;
			}
			else
			{
				ae.m_PropertyName = pi_propertyname;
				ae.m_ContractNumber = pi_contractnum;
				ae.m_PublicKey = pi_userinstallkey;
			}

			return ae;
		}

	}
}
