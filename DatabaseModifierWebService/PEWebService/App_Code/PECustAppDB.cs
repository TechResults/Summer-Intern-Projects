using System;
using System.Collections.Generic;
using System.Linq;
using PE.DataReturn;
using System.Data.SqlClient;
using System.Data;
using PE.DataReturns;
using System.IO;


/// <summary>
/// CustAppService deals with user login, registration, and loading of assets at launch
/// </summary>
namespace PE.CustAppDB
{
    [Serializable]
    public class RegisterNewUserResult
    {
        private bool _isRegistered;
        private bool _isLocked;
        #region Publics
        public bool isRegistered
        {
            get { return _isRegistered; }
            set { _isRegistered = value; }
        }

        public bool isLocked
        {
            get { return _isLocked; }
            set { _isLocked = value; }
        }
        #endregion
        public void DBGetStoredPin(string mobile, string pinCode)
        {
            try
            {
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@Mobile", mobile));
                spParams.Add(new SqlParameter("@PIN", pinCode));
                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);
                if (result.Tables[0].Rows.Count > 0)
                {
                    isRegistered = true;
                    isLocked = false;

                }
                else
                {
                    //Increment some form of counter for login attempts
                    int loginAttempts = 0;
                    if (loginAttempts > 3)
                    {
                        isLocked = true;
                        isRegistered = false;
                    }
                    else
                    {
                        isLocked = false;
                        isRegistered = false;
                    }
                }
            }
            catch (SqlException ex)
            {
                string errorMessage = ex.Message;

            }
        }
    }

    [Serializable]
    public class ValidatePhoneRegisteredResult
    {
        private string _userToken;
        private bool _showBalancesNoPin;
        private bool _isRegistered;
        #region PUBLICS
        public string userToken
        {
            get { return _userToken; }
            set { _userToken = value; }
        }

        public bool ShowBalancesNoPin
        {
            get
            {
                return _showBalancesNoPin;
            }

            set
            {
                _showBalancesNoPin = value;
            }
        }

        public bool IsRegistered
        {
            get
            {
                return _isRegistered;
            }

            set
            {
                _isRegistered = value;
            }
        }
        #endregion
        // Generate a one way hash, API does not specify implementation
        public void GenerateOneWayHash(string mobile)
        {
            //SHould set userToken = to something
            string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);
            //Generate hash based on mobile and current time
            string generatedHash = "hash";
            userToken = generatedHash;
        }

        // Query Database for existing mobile number. Return true if mobile number exists, false if it does not.
        public void checkIsRegistered(string mobile)
        {
            try
            {
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@Mobile", mobile));
                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);
                if (result.Tables[0].Rows.Count > 0)
                {
                    IsRegistered = true;
                }
                else
                {
                    IsRegistered = false;
                }
                //Add logic for showbalancesnopin. Not specified in API doc (default to false?)
                ShowBalancesNoPin = false;
            }
            catch (SqlException ex)
            {
                string errorMessage = ex.Message;

            }
        }
    }


    [Serializable]
    public class ShowBalancesOnOpeningScreenResult : Default
    {
        public void DBGetAccountBalancesSet(string mobile)
        {
            try
            {
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);
                if (result.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < result.Tables[0].Rows.Count; i++)
                    {
                        Account newAccount = new Account();
                        newAccount.accountName = result.Tables[0].Rows[i][""].ToString();
                        newAccount.accountBalance = result.Tables[0].Rows[i][""].ToString();

                        AccountBalances.Add(newAccount);
                    }
                }
                else
                {
                    AccountBalances = null;
                }
            }
            catch (SqlException ex)
            {
                string errorMessage = ex.Message;
            }
        }

        private List<Account> AccountBalances;

        private class Account
        {
            private string _accountName;
            public string accountName
            {
                get { return _accountName; }
                set { _accountName = value; }
            }
            private string _accountBalance;
            public string accountBalance
            {
                get { return _accountBalance; }
                set { _accountBalance = value; }
            }
        }
    }


    [Serializable]
    public class ValidateUserReturn
    {
        private bool _isValid;
        private string _userToken;

        public bool isValid
        {
            get { return _isValid; }
            set { _isValid = value; }
        }
        public string userToken
        {
            get { return _userToken; }
            set { _userToken = value; }
        }

        public void DBGetStoredPin(string mobile, string pinCode)
        {
            try
            {
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@Mobile", mobile));
                spParams.Add(new SqlParameter("@PIN", pinCode));
                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);
                if (result.Tables[0].Rows.Count > 0)
                {
                    isValid = true;
                    GenerateOneWayHash(mobile);
                }
                else
                {
                    isValid = false;
                    userToken = "";
                }
            }
            catch (SqlException ex)
            {
                string errorMessage = ex.Message;

            }
        }
        public void GenerateOneWayHash(string mobile)
        {
            try
            {
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@Mobile", mobile));
                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);
                userToken = result.Tables[0].Rows[0][""].ToString();
            }
            catch (SqlException ex)
            {
                string errorMessage = ex.Message;
                isValid = false;
                userToken = "";
            }

        }
    }

    [Serializable]
    public class LoadLogoReturn : Default
    {
        private byte[] _logo;
        public byte[] logo
        {
            get { return _logo; }
            set { _logo = value; }
        }

        public void DBGetLogo(string mobile)
        {
            try
            {
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);
                if (result.Tables[0].Rows.Count > 0)
                {
                    MemoryStream ms = new MemoryStream((byte[])result.Tables[0].Rows[0][""]);
                    byte[] bytes = ms.ToArray();
                    logo = bytes;
                }
                else
                {
                    logo = null;
                }
            }
            catch (SqlException ex)
            {
                string errorMessage = ex.Message;
                logo = null;
            }
        }
    }

}


