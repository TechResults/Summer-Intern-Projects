using System;
using System.Collections;
using System.Text;
using System.Globalization;

namespace AccessManagementLib
{
	/// <summary>
	/// Summary description for EncryptionUtility.
	/// </summary>
	internal sealed class EncryptionUtility
	{

		// Generates random numbers
		private Random m_Rng = new Random();

		private string m_AppName = null;
		private static Hashtable m_Keys = null;
		private static readonly int SET_SIZE = 1024;

		internal delegate UInt64 PublicKeyDelegate();

		private PublicKeyDelegate PublicKey = null;

		static EncryptionUtility()
		{
			m_Keys = new Hashtable();

			m_Keys.Add("TR_TP", PRIVATE_KEYS.TR_TP_PRIVATE_KEY);
			m_Keys.Add("TR_WG", PRIVATE_KEYS.TR_WG_PRIVATE_KEY);
			m_Keys.Add("TR_HV", PRIVATE_KEYS.TR_HV_PRIVATE_KEY);
			m_Keys.Add("TR_MD", PRIVATE_KEYS.TR_MD_PRIVATE_KEY);

		}

		internal EncryptionUtility(string pi_appname, PublicKeyDelegate pi_func)
		{
			m_AppName = pi_appname;
			PublicKey = pi_func;
		}

		/// <summary>
		/// Decrypts a given encrypted string
		/// </summary>
		/// <param name="pi_todecrypt">string to decrypt</param>
		/// <returns>Unencrypted string</returns>
		internal string decryptString(string pi_todecrypt)
		{
			int iPos;
			int i = this.getFirstRandomNumber(SET_SIZE);

			char[] decryptarray = pi_todecrypt.ToCharArray();

			for(int j = 0; j < pi_todecrypt.Length; j++)
			{
				i = this.getNextRandomNumber(SET_SIZE);
				iPos = (int) decryptarray[j];
				iPos = iPos ^ i;
				decryptarray[j] = (char)iPos;
			}

			return new string(decryptarray);
		}

		/// <summary>
		/// Encrypts a given plain text string
		/// </summary>
		/// <param name="pi_toencrypt">string to encrypt</param>
		/// <returns>Encrypted string</returns>
		internal string encryptString(string pi_toencrypt)
		{
			int iPos;
			int i = this.getFirstRandomNumber(SET_SIZE);

			char[] encryptarray = pi_toencrypt.ToCharArray();

			for(int j = 0; j < pi_toencrypt.Length; j++)
			{
				i = this.getNextRandomNumber(SET_SIZE);
				iPos = (int) encryptarray[j];
				iPos = iPos ^ i;
				encryptarray[j] = (char)iPos;
			}

			return new string(encryptarray);
		}

		internal string generatePublicKey(string pi_propertyname, string pi_contractnum)
		{
			UInt16[] publickeyarray = this.generateHashValArray(this.encryptString(pi_propertyname + pi_contractnum));
			string keyout = publickeyarray[0].ToString("X4") + "-" + publickeyarray[1].ToString("X4") + "-" + publickeyarray[2].ToString("X4") + "-" + publickeyarray[3].ToString("X4");
			return keyout;
		}

		internal bool validateInstallKey(string pi_propertyname, string pi_contractnum, string pi_userinstallnum)
		{
			string pubkey = this.generatePublicKey(pi_propertyname, pi_contractnum);
			return pubkey.Equals(pi_userinstallnum);
		}

		#region Encryption Code

		private int PrivateKey
		{
			get
			{
				return (int)m_Keys[m_AppName];
			}
		}


		/// <summary>
		/// Creates a hash value of the given string which 
		/// can then be compared to other hash values.
		/// </summary>
		/// <param name="pi_encrypted"></param>
		/// <returns>long representing the hash value of the string</returns>
		private UInt16[] generateHashValArray(string pi_encrypted)
		{
			byte[] encryptedbytes = Encoding.UTF8.GetBytes(pi_encrypted);
			byte[] bytes = new byte[8]{0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff};

			for(int i = 0; i < encryptedbytes.Length; i++)
			{
				bytes[i % 8] ^= encryptedbytes[i];
			}

			UInt16[] theints = new UInt16[4];

			theints[0] = BitConverter.ToUInt16(bytes, 0);
			theints[1] = BitConverter.ToUInt16(bytes, 2);
			theints[2] = BitConverter.ToUInt16(bytes, 4);
			theints[3] = BitConverter.ToUInt16(bytes, 6);
			return theints;
		}
		
		/// <summary>
		/// Gets the next number from the Random class
		/// </summary>
		/// <param name="pi_size"></param>
		/// <returns>int The next random number</returns>
		private int getNextRandomNumber(int pi_size)
		{
			return this.m_Rng.Next(pi_size);
		}

		/// <summary>
		/// ReInitializes the Random class using a seed number
		/// </summary>
		/// <param name="pi_seed"></param>
		private void setSeed(int pi_seed)
		{
			this.m_Rng = new Random(pi_seed);
		}

		/// <summary>
		/// Gets the first Random number from the Random class
		/// </summary>
		/// <param name="pi_size"></param>
		/// <returns>int The first random number</returns>
		private int getFirstRandomNumber(int pi_size)
		{
			UInt64 tempVal = 0;

			tempVal = this.PublicKey();
			int seedVal = 0;

			if(tempVal != 0)
			{
				int tintval = BitConverter.ToInt32(BitConverter.GetBytes(tempVal), 0);
				tintval ^= BitConverter.ToInt32(BitConverter.GetBytes(tempVal), 2);
				seedVal = (int)tempVal * this.PrivateKey;
			}
			else
			{
				seedVal = (int)this.PrivateKey;
			}

			this.setSeed(seedVal);

			// Generate throwaway randoms
			int randomThrowNum, numThrowaways, currentRandom;
			randomThrowNum = this.getNextRandomNumber(pi_size);
			numThrowaways = randomThrowNum % 100;

			for(int k = 0; k < numThrowaways; k++)
			{
				currentRandom = this.getNextRandomNumber(pi_size);
			}

			return this.getNextRandomNumber(pi_size);
		}
		
		#endregion


		private enum PRIVATE_KEYS 
		{
			TR_TP_PRIVATE_KEY = 247956,
			TR_WG_PRIVATE_KEY = 246597,
			TR_HV_PRIVATE_KEY = 249765,
			TR_MD_PRIVATE_KEY = 246579
		}

	}
}
