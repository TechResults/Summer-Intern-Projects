using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;
using System.Collections;



namespace DatabaseModifier
{

    static class Constants
    {
        public const int GetHashID = 1;
        public const int RegisterID = 2;
        public const int GetBalanceID = 3;

        //Stored Procedure Names:
        public const string checkRegistration = "dbo.uspCheckRegistration";
        public const string addRegistration = "dbo.uspAddRegistration";
        public const string getUserID = "dbo.uspGetUserID";
        public const string logWM = "dbo.uspLogWebMethod";


    }
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://www.ellistrtest.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        #region GetHash
        /// <summary>
        ///		GetHashCodeFromServer
        ///		<para>
        ///			Finds User Mobile, Generates UserToken through a hash Function
        ///		</para>
        /// </summary>
        /// <param name="searchNum">Mobile Phone to search for.</param>
        /// <param name="UserID"> Pass by Reference UserID</param>
        /// <param name="complete">Boolean value to determine whether function operated correctly</param>
        /// <returns>string - return string of usertoken.</returns>

        public static bool GetHash(User toHash, ref string hash)
        {
            string matchingNum = "-1";
            string tempStringUserID = "-1";
            var con = ConfigurationManager.ConnectionStrings["EllisTR"].ToString();
            using (SqlConnection myConnection = new SqlConnection(con))
            {
                //Open Connection to SQL Server
                myConnection.Open();

                //Command to find user in DB
                string oString = "Select [UserToken] from [User] where MobileNumber like '" + searchNum + "'";  //1
                SqlCommand oCmd = new SqlCommand(oString, myConnection);

                //Command to find userID in DB
                string oUserID = "Select [UserID] from [User] where MobileNumber like '" + searchNum + "'";
                SqlCommand oUserCmd = new SqlCommand(oUserID, myConnection);

                //Get Expiration Time of generated Hash (1000 seconds after generation)
                var expireTime = new DateTime();
                expireTime = DateTime.Now.AddSeconds(1000);
                string expStr = expireTime.ToString();


                //Command to add expiration time value into DB
                string addString = "UPDATE [User] SET TimeCheck = '" + expStr + "' WHERE MobileNumber like '" + searchNum + "'";
                SqlCommand addCmd = new SqlCommand(addString, myConnection);

                //Get User Token From DB
                //In a real setting the server should generate a new hash at each new request.
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        matchingNum = oReader["UserToken"].ToString();
                    }

                }

                //Get UserID From DB

                using (SqlDataReader userReader = oUserCmd.ExecuteReader())
                {
                    while (userReader.Read())
                    {
                        tempStringUserID = userReader["UserID"].ToString();
                    }
                }

                UserID = Convert.ToInt64(tempStringUserID);

                //Add Expiration Time
                int numberAdditions = addCmd.ExecuteNonQuery();

                if (numberAdditions == 1 && UserID != -1)
                {
                    complete = true;
                }
                else
                {
                    complete = false;
                }

                myConnection.Close();
            }
            return matchingNum;
        }
        #endregion

        #region GenerateGUID
        /// <summary>
        ///     Generates a GUID based on current time
        /// </summary>
        /// <param name="expStr">A string variable of when the generated userToken will expire</param>
        /// <returns></returns>
        public string GenerateGUID(ref string expStr)
        {

            //Declare Current and Expiration Time of generated Hash (Expire is 1000 seconds after generation)
            var currentTime = new DateTime();
            var expireTime = new DateTime();

            //Get expire time:
            currentTime = DateTime.Now;
            expireTime = currentTime.AddSeconds(1000);

            //Save expire time as a string
            expStr = expireTime.ToString();

            //Generate GUID
            Guid userToken = Guid.NewGuid();


            return userToken.ToString();

        }
        #endregion

        #region LogWebMethods
        /// <summary>
        ///		Log Web Methods, users, time taken, and success
        ///		<para>
        ///			BLAH BLAH
        ///		</para>
        /// </summary>
        /// <param name="methodID">Integer representing a web method.</param>
        /// <param name="sucess">Boolean of whether the operation returned sucessfully</param>
        /// <returns>Void - Return Nothing.</returns>
        /// 

        public static void LogWM(int MethodID, long UserID, bool sucess, DateTime startTime)
        {
            //Get Connection String for Database
            var con = ConfigurationManager.ConnectionStrings["EllisTR"].ToString();
            using (SqlConnection myConnection = new SqlConnection(con))
            {
                //Open Connection to SQL Server
                myConnection.Open();


                int isSuccess = Convert.ToInt32(sucess);
                string isSuccessc = Convert.ToString(isSuccess);

                //Get Current Time
                var endTime = new DateTime();
                endTime = DateTime.Now;

                //Convert CurrentTime to a string
                string endTString = endTime.ToString();
                string startTString = startTime.ToString();

                //Command to add information to WebMethodLog
                string addString = "INSERT INTO WebMethodLog (WebMethodID, [UserID], TimeAcessed, EndTime, Sucess) VALUES ('" + MethodID.ToString() + "', '" + UserID.ToString() + "', '" + startTString + "', '" + endTString + "', '" + isSuccessc + "')";
                SqlCommand addCmd = new SqlCommand(addString, myConnection);

                int temp = addCmd.ExecuteNonQuery();

                myConnection.Close();
            }
        }
        #endregion
        /*
        #region RegisterUser
        /// <summary>
        ///		Attempt to register new user, or return that user is already registered
        ///		<para>
        ///			BLAH BLAH
        ///		</para>
        /// </summary>
        /// <param name="methodID">Integer representing a web method.</param>
        /// <param name="sucess">Boolean of whether the operation returned sucessfully</param>
        /// <returns>Void - Return Nothing.</returns>
        /// 
        public long RegisterUser(string firstName, string lastName, long socialSecurityNum, long mobileNumber, ref bool wasCreated, ref bool isSucessful, int PIN, ref long userID)
        {
            long registrationID = 0;
            string stringRegID = "NULL";
            string tempStringUserID = " ";

            //Set booleans to false. If operations are sucessful they will be updated.
            wasCreated = false;
            isSucessful = false;

            var con = ConfigurationManager.ConnectionStrings["EllisTR"].ToString();
            using (SqlConnection myConnection = new SqlConnection(con))
            {
                //Open Connection to SQL Server
                myConnection.Open();

                //Command to get RegistrationID for already present user or get ' ' for new user
                string checkReg = "SELECT RegID from Registration WHERE MobileNumber LIKE '" + mobileNumber + "'";
                SqlCommand checkRegCMD = new SqlCommand(checkReg, myConnection);

                
                using (SqlDataReader regReader = checkRegCMD.ExecuteReader())
                {
                    while(regReader.Read())
                    {
                        stringRegID = regReader["RegID"].ToString();
                    }
                }

                //If the user is not registered
                if (stringRegID == "NULL")
                {
                    wasCreated = true;

                    //Generate a new userToken for new user along with expiration time
                    string tokenExpireTime = " ";
                    string generatedToken = GenerateGUID(ref tokenExpireTime);

                    //Add user and generated values to the registration and user tables in the SQL database
                    string addString = "INSERT INTO Registration (FirstName, LastName, SSNum, MobileNumber) VALUES ('" + firstName.ToString() + "', '" + lastName.ToString() + "', '" + socialSecurityNum.ToString() + "', '" + mobileNumber.ToString() + "')";
                    string addToUser = "INSERT INTO [User] (MobileNumber, PIN, [UserToken], TimeCheck) VALUES ('" + mobileNumber.ToString() + "', '" + PIN.ToString() + "', '" + generatedToken + "', '" + tokenExpireTime.ToString() + "')";

                    SqlCommand addCmd = new SqlCommand(addString, myConnection);
                    SqlCommand addUserCmd = new SqlCommand(addToUser, myConnection);

                    //Execute both SQL commands
                    int registrationImpact = addCmd.ExecuteNonQuery();
                    int userImpact = addUserCmd.ExecuteNonQuery();

                    //If both commands add 1 row, operation is sucessful
                    if (registrationImpact == 1 && userImpact == 1)
                    {
                        isSucessful = true;
                    }

                    using (SqlDataReader regReader = checkRegCMD.ExecuteReader())
                    {
                        while (regReader.Read())
                        {
                            stringRegID = regReader["RegID"].ToString();
                        }
                    }
                }

                else
                {
                    wasCreated = false;
                }

                //GET USERID
                string oUserID = "Select [UserID] from [User] where MobileNumber like '" + mobileNumber + "'";
                SqlCommand oUserCmd = new SqlCommand(oUserID, myConnection);

                using (SqlDataReader userReader = oUserCmd.ExecuteReader())
                {
                    while (userReader.Read())
                    {
                        tempStringUserID = userReader["UserID"].ToString();
                    }
                }

                userID = Convert.ToInt64(tempStringUserID);

                //Close Connection to SQL Server
                myConnection.Close();
                registrationID = Convert.ToInt64(stringRegID);
            }
            return registrationID;
        }

        #endregion
    */

        #region RegisterUser
        /// <summary>
        ///		Attempt to register new user, or return that user is already registered
        ///		<para>
        ///			BLAH BLAH
        ///		</para>
        /// </summary>
        /// <param name="methodID">Integer representing a web method.</param>
        /// <param name="sucess">Boolean of whether the operation returned sucessfully</param>
        /// <returns>Void - Return Nothing.</returns>
        /// 
        public long RegisterUser(User toRegister, ref bool wasCreated, ref bool isSucessful)
        {
            long registrationID = 0;
            string stringRegID = "NULL";
            string tempStringUserID = " ";

            //Set booleans to false. If operations are sucessful they will be updated.
            wasCreated = false;
            isSucessful = false;

            try
            {
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@MobileNum", toRegister.mobileNumber));
                result = DataAcess.ExecuteQuerySP(Constants.checkRegistration, spParams);


                if(result.Tables.Count > 0)
                {
                    toRegister.regID = long.Parse(result.Tables[0].Rows[0]["RegID"].ToString());
                    isSucessful = true;
                }

                else
                {
                    wasCreated = true;
                    //Generate a hash for user TODO
                    //toRegister.userToken = GenerateHash();
                    DataSet back = new DataSet();
                    List<SqlParameter> adParams = new List<SqlParameter>();
                    adParams.Add(new SqlParameter("@FN", toRegister.firstName));
                    adParams.Add(new SqlParameter("@LN", toRegister.lastName));
                    adParams.Add(new SqlParameter("@SSN", toRegister.SSN));
                    adParams.Add(new SqlParameter("@MobileNum", toRegister.mobileNumber));
                    adParams.Add(new SqlParameter("@NewPIN", toRegister.PIN));
                    adParams.Add(new SqlParameter("NewGUID", toRegister.userToken.ToString()));



                }



                 
            }

            catch { }

            var con = ConfigurationManager.ConnectionStrings["EllisTR"].ToString();
            using (SqlConnection myConnection = new SqlConnection(con))
            {
                //Open Connection to SQL Server
                myConnection.Open();

                //Command to get RegistrationID for already present user or get ' ' for new user
                string checkReg = "SELECT RegID from Registration WHERE MobileNumber LIKE '" + mobileNumber + "'";
                SqlCommand checkRegCMD = new SqlCommand(checkReg, myConnection);


                using (SqlDataReader regReader = checkRegCMD.ExecuteReader())
                {
                    while (regReader.Read())
                    {
                        stringRegID = regReader["RegID"].ToString();
                    }
                }

                //If the user is not registered
                if (stringRegID == "NULL")
                {
                    wasCreated = true;

                    //Generate a new userToken for new user along with expiration time
                    string tokenExpireTime = " ";
                    string generatedToken = GenerateGUID(ref tokenExpireTime);

                    //Add user and generated values to the registration and user tables in the SQL database
                    string addString = "INSERT INTO Registration (FirstName, LastName, SSNum, MobileNumber) VALUES ('" + firstName.ToString() + "', '" + lastName.ToString() + "', '" + socialSecurityNum.ToString() + "', '" + mobileNumber.ToString() + "')";
                    string addToUser = "INSERT INTO [User] (MobileNumber, PIN, [UserToken], TimeCheck) VALUES ('" + mobileNumber.ToString() + "', '" + PIN.ToString() + "', '" + generatedToken + "', '" + tokenExpireTime.ToString() + "')";

                    SqlCommand addCmd = new SqlCommand(addString, myConnection);
                    SqlCommand addUserCmd = new SqlCommand(addToUser, myConnection);

                    //Execute both SQL commands
                    int registrationImpact = addCmd.ExecuteNonQuery();
                    int userImpact = addUserCmd.ExecuteNonQuery();

                    //If both commands add 1 row, operation is sucessful
                    if (registrationImpact == 1 && userImpact == 1)
                    {
                        isSucessful = true;
                    }

                    using (SqlDataReader regReader = checkRegCMD.ExecuteReader())
                    {
                        while (regReader.Read())
                        {
                            stringRegID = regReader["RegID"].ToString();
                        }
                    }
                }

                else
                {
                    wasCreated = false;
                }

                //GET USERID
                string oUserID = "Select [UserID] from [User] where MobileNumber like '" + mobileNumber + "'";
                SqlCommand oUserCmd = new SqlCommand(oUserID, myConnection);

                using (SqlDataReader userReader = oUserCmd.ExecuteReader())
                {
                    while (userReader.Read())
                    {
                        tempStringUserID = userReader["UserID"].ToString();
                    }
                }

                userID = Convert.ToInt64(tempStringUserID);

                //Close Connection to SQL Server
                myConnection.Close();
                registrationID = Convert.ToInt64(stringRegID);
            }
            return registrationID;
        }

        #endregion

        #region IncrementLoyalty
        /// <summary>
        /// Increments a simple counter based on the number of times the user acesses the application
        /// </summary>
        /// <param name="userID">long - Passes userID to find matching database loyalty value</param>
        /// <returns>VOID: Function increments loyalty value in the database</returns>
        public void IncrementLoyalty(long userID)
        {
            //Get Connection String for Database
            var con = ConfigurationManager.ConnectionStrings["EllisTR"].ToString();
            using (SqlConnection myConnection = new SqlConnection(con))
            {
                //Open Connection to SQL Server
                myConnection.Open();
                string addValue = "UPDATE [User] SET AcessedNum = AcessedNum + 1 WHERE [UserID] LIKE '" + userID + "'";
                SqlCommand addCmd = new SqlCommand(addValue, myConnection);

                int temp = addCmd.ExecuteNonQuery();

                myConnection.Close();
            }

        }

        #endregion

        #region GetLoyaltyBalance
        /// <summary>
        /// Gets the clients loyalty balance
        /// </summary>
        /// <param name="userID">long - Passes userID to find matching database loyalty value</param>
        /// <returns>Long: Function returns loyalty value</returns>
        public long GetLoyaltyBalance(long userID, ref bool isSuccesful)
        {
            long loyaltyBalance = -1;
            string tempBalString = " ";
            //Get Connection String for Database
            var con = ConfigurationManager.ConnectionStrings["EllisTR"].ToString();
            using (SqlConnection myConnection = new SqlConnection(con))
            {
                //Open Connection to SQL Server
                myConnection.Open();
                string getBal = "SELECT AcessedNum FROM [User] WHERE [UserID] LIKE '" + userID + "'";
                SqlCommand getBalCmd = new SqlCommand(getBal, myConnection);

                using (SqlDataReader balReader = getBalCmd.ExecuteReader())
                {
                    while (balReader.Read())
                    {
                        tempBalString = balReader["AcessedNum"].ToString();
                    }
                }
                myConnection.Close();
            }
            loyaltyBalance = Convert.ToInt64(tempBalString);
            if (loyaltyBalance >= 0)
                isSuccesful = true;
            return loyaltyBalance;
        }
        #endregion

        public DateTime getCurrentTime()
        {
            var currentTime = new DateTime();
            currentTime = DateTime.Now;
            return currentTime;
        }
        //Generate A Hash Code 
        [WebMethod]
        public string GenerateHash()
        {
            
            //GET START TIME OF FUNCTION:
            var timeOfStart = new DateTime();
            timeOfStart = DateTime.Now;

            //Initialize Local Variables
            long UserID = -1;
            bool isSuccessful = false;
            
            //Get Hash
            string hash = GetHash(Constants.OwenNumber.ToString(), ref UserID, ref isSuccessful);
            LogWM(Constants.GetHashID, UserID, isSuccessful, timeOfStart);
            return hash;
        }

        //Register A User
        [WebMethod]
        public long RegisterUser(string firstName, string lastName, long socialSecurityNumber, string mobileNumber, int PIN, ref long userID)
        {
            
            //GET START TIME OF FUNCTION:
            var timeOfStart = new DateTime();
            timeOfStart = DateTime.Now;

            //Initialize Local Variables
            long regID = -1;
            userID = -1;

            bool isSuccessful = false;

            //Variable to check if a new user was added or if it is an existing user
            bool wasCreated = false;

            //Register User:
            regID = RegisterUser(firstName, lastName, socialSecurityNumber, mobileNumber, ref wasCreated, ref isSuccessful, PIN, ref userID);
            LogWM(Constants.RegisterID, userID, isSuccessful, timeOfStart);
            return regID;
        }

        [WebMethod]
        public void updateLoyalty()
        {
            IncrementLoyalty(Constants.OwenID);
        }

        [WebMethod]
        public long getBalance()
        {
            long userID = Constants.OwenID;
            bool sucessful = false;
            long balance = GetLoyaltyBalance(userID, ref sucessful);
            
            LogWM(Constants.GetBalanceID, userID, sucessful, getCurrentTime());

            return balance;
        }
    }
}