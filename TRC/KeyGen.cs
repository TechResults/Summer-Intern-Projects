using System;
using System.Collections;
using System.Text;
using System.Globalization;

namespace AccessManagementLib
{
	/// <summary>
	/// Summary description for KeyGen.
	/// </summary>
	public sealed class KeyGen
	{
		private EncryptionUtility.PublicKeyDelegate m_Delegate = null;
		private string m_AppName = null;
		private string m_PublicKey = null;

		private KeyGen(string pi_appname)
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


		private string generatePublicKey(string pi_propertyname, string pi_contractnum)
		{
			EncryptionUtility eu = new EncryptionUtility(m_AppName, m_Delegate);
			return eu.generatePublicKey(pi_propertyname, pi_contractnum);
		}

		private string generateModuleKey(string pi_propertyname, string pi_contractnum, string pi_modulename)
		{
			EncryptionUtility eu = new EncryptionUtility(m_AppName, m_Delegate);
			return eu.generatePublicKey(pi_propertyname, pi_contractnum + pi_modulename.Substring(0, 3).ToUpper());
		}

		private static KeyGen getKeyGenerator(string pi_appname, string pi_propertyname, string pi_contractnum)
		{
			KeyGen kg = new KeyGen(pi_appname);
			string pk = kg.generatePublicKey(pi_propertyname, pi_contractnum);
			kg.m_PublicKey = pk;

			return kg;
		}


		public static string createPublicKey(string pi_appname, string pi_propertyname, string pi_contractnum)
		{
			KeyGen kg = new KeyGen(pi_appname);
			return kg.generatePublicKey(pi_propertyname, pi_contractnum);
		}

		public static string createModuleKey(string pi_appname, string pi_propertyname, string pi_contractnum, string pi_modulename)
		{
			KeyGen kg = getKeyGenerator(pi_appname, pi_propertyname, pi_contractnum);
			return kg.generateModuleKey(pi_propertyname, pi_contractnum, pi_modulename);
		}

	}
}
