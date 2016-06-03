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
        public const int FunctionFailureID = 0;
        public const int GetHashID = 1;
        public const int RegisterID = 2;
        public const int RegisterFailedID = 3;
        public const int GetBalanceID = 4;
        public const int UpdateLoyaltyID = 5;

        //Stored Procedure Names:
        public const string checkRegistration = "dbo.uspCheckRegistration";
        public const string addRegistration = "dbo.uspAddRegistration";
        public const string getUser = "dbo.uspGetUser";
        public const string logWM = "dbo.uspLogWebMethod";
        public const string addLoyalty = "dbo.uspAddLoyalty";
        public const string checkExpiration = "dbo.uspCheckExpiration";
        public const string updateLogin = "dbo.uspUpdateLogin";

        //Registration States:
        public const Int32 REGnewUser = 1;
        public const Int32 REGexistingUser = 2;


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
        //Works
        #region GenerateGUID
        /// <summary>
        ///     Generates a GUID
        /// </summary>
        /// <returns></returns>
        public Guid GenerateGUID()
        {
            //Generate GUID
            Guid userToken = Guid.NewGuid();

            //Return GUID as string
            return userToken;

        }
        #endregion

        //Works
        #region LogWebMethods
        /// <summary>
        ///		Log Web Methods, users, time taken, and success
        ///		<para>
        ///			
        ///		</para>
        /// </summary>
        /// <param name="methodID">Integer representing a web method. Constants.MethodName may be helpful</param>
        /// <param name="currentUser">User class of the current user</param>
        /// <param name="sucess">Boolean of whether the operation returned sucessfully</param>
        /// <param name="startTime">DateTime of the start time of the method to be logged</param>
        /// <returns>Bool - Returns true if log is sucessful, or false if unsucessful</returns>
        /// 
        public static bool LogWM(int methodID, User currentUser, bool sucess, DateTime startTime)
        {
            bool isSucessful = false;
            try
            {
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@WMID", methodID));
                spParams.Add(new SqlParameter("@UsID", currentUser.ID));
                spParams.Add(new SqlParameter("@TAcessed", startTime.ToString()));
                spParams.Add(new SqlParameter("@Complete", Convert.ToInt32(sucess)));

                int rowsImpacted = DataAcess.ExecuteNonQuerySP(Constants.logWM, spParams);
                
                if (rowsImpacted == 1)
                {
                    isSucessful = true;
                }

                else
                {
                    isSucessful = false;
                }
            }
            catch
            {
                isSucessful = false;
            }
            return isSucessful;
        }
        #endregion

        //Works
        #region RegisterUser
        /// <summary>
        ///		Attempt to register new user, or exits if user is already registered
        ///		<para>
        ///			
        ///		</para>
        /// </summary>
        /// <param name="toRegister">Reference User Class to update an individual user.</param>
        /// <param name="wasCreated">Reference Boolean of whether a new user was created</param>
        /// <returns>Bool - Return True if the operation was sucessful, or false if it was not.</returns>
        /// 
        public bool RegisterUser(ref User toRegister, ref bool wasCreated)
        {
            //Set booleans to false. If operations are sucessful they will be updated.
            wasCreated = false;
            bool isSucessful = false;
            bool userExist = false;
            userExist = GetUserDB(ref toRegister);

            if (userExist)
            {
                wasCreated = false;
                isSucessful = true;
                return isSucessful;
                //IF Login has expired, update login
                bool doesExpirationWork = false;
                if (checkExpiration(ref toRegister, ref doesExpirationWork))
                {
                    bool doesUpdateWork = updateLogin(ref toRegister);
                }
            }

            else
            { 
                toRegister.userToken = GenerateGUID();
                DataSet back = new DataSet();
                List<SqlParameter> adParams = new List<SqlParameter>();
                adParams.Add(new SqlParameter("@FN", toRegister.firstName));
                adParams.Add(new SqlParameter("@LN", toRegister.lastName));
                adParams.Add(new SqlParameter("@SSN", toRegister.SSN));
                adParams.Add(new SqlParameter("@MobileNum", toRegister.mobileNumber));
                adParams.Add(new SqlParameter("@NewPIN", toRegister.PIN));
                adParams.Add(new SqlParameter("NewGUID", toRegister.userToken.ToString()));

                back = DataAcess.ExecuteQuerySP(Constants.addRegistration, adParams);

                if (back.Tables.Count > 0)
                {
                    toRegister.ID = long.Parse(back.Tables[0].Rows[0]["ID"].ToString());
                    wasCreated = true;
                }

                else
                {
                    isSucessful = false;
                }
                return isSucessful;
            }
        }

        #endregion

        //Works
        #region UpdateLoyalty
        /// <summary>
        /// Updates the User.loyaltyValue and then passes the new value to the SQL DB
        /// </summary>
        /// <param name="currentUser">User - Passes User class to modify and find user in DB Table</param>
        /// <returns>BOOL: Function increments loyalty value in the database and returns true if sucessful or false if not</returns>
        public bool UpdateLoyalty(ref User currentUser)
        {
            bool isSucessful = false;
            try
            {
                currentUser.loyaltyVal++;
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@ID", currentUser.ID));
                spParams.Add(new SqlParameter("@LoyaltyValue", currentUser.loyaltyVal));

                int rowsImpacted = DataAcess.ExecuteNonQuerySP(Constants.addLoyalty, spParams);
                if (rowsImpacted == 1)
                    isSucessful = true;
            }

            catch
            {
                currentUser.loyaltyVal--;
                isSucessful = false;
            }
            return isSucessful;
        }

        #endregion

        //Works
        #region getUserDB
        /// <summary>
        /// Gets a User from DB
        /// </summary>
        /// <param name="currentUser">User - Passes User class to modify and find user in DB Table</param>
        /// <returns>BOOL: Function returns true if user is present and is removed</returns>
        public bool GetUserDB(ref User currentUser)
        {
            bool userPresent = false;
            try
            {
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@MobileNum", currentUser.mobileNumber));
                result = DataAcess.ExecuteQuerySP(Constants.getUser, spParams);
                
                if (result.Tables.Count > 0)
                {
                    currentUser.firstName = result.Tables[0].Rows[0]["FirstName"].ToString();
                    currentUser.lastName = result.Tables[0].Rows[0]["LastName"].ToString();
                    currentUser.ID = long.Parse(result.Tables[0].Rows[0]["ID"].ToString());
                    currentUser.SSN = long.Parse(result.Tables[0].Rows[0]["SSNum"].ToString());
                    currentUser.mobileNumber = result.Tables[0].Rows[0]["MobileNumber"].ToString();
                    currentUser.PIN = int.Parse(result.Tables[0].Rows[0]["PIN"].ToString());
                    currentUser.userToken = new Guid(result.Tables[0].Rows[0]["Token"].ToString());
                    currentUser.loyaltyVal = int.Parse(result.Tables[0].Rows[0]["AcessedNum"].ToString());
                    userPresent = true;
                }
                else
                {
                    userPresent = false;
                }
            }

            catch
            {
                userPresent = false;
            }
            return userPresent;
        }

        #endregion

        //Works
        #region getCurrentTime
        /// <summary>
        ///     returns current time as a datetime object
        /// </summary>
        /// <returns>DateTime - current Time</returns>
        public DateTime getCurrentTime()
        {
            var currentTime = new DateTime();
            currentTime = DateTime.Now;
            return currentTime;
        }
        #endregion

        //Works
        #region checkExpirationTime
        /// <summary>
        ///     
        /// </summary>
        /// <param name="currentUser">User - current User object</param>
        /// <param name="isSucessful">Bool - if function operates correctly change to true</param>
        /// <returns>Bool - Returns true if user should be kicked (credentials are not valid)</returns>
        public bool checkExpiration(ref User currentUser, ref bool isSucessful)
        {
            var currentTime = getCurrentTime();
            var expirationTime = new DateTime();
            bool forceUpdate = false;
            isSucessful = false;
            

            try
            {
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@ID", currentUser.ID));
                spParams.Add(new SqlParameter("@ClientToken", currentUser.userToken.ToString()));
                result = DataAcess.ExecuteQuerySP(Constants.checkExpiration, spParams);

                if (result.Tables.Count > 0)
                {
                    expirationTime = DateTime.Parse(result.Tables[0].Rows[0]["TimeCheck"].ToString());
                    Guid serverToken = new Guid(result.Tables[0].Rows[0]["Token"].ToString());
                    
                    if (expirationTime <= currentTime || serverToken != currentUser.userToken)
                    {
                        forceUpdate = true;
                    }
                }
                else
                {
                    forceUpdate = true;
                }

                isSucessful = true;
            }

            catch
            {
                forceUpdate = true;
                isSucessful = false;
            }
            return forceUpdate;
        }
        #endregion

        //Works
        #region updateLogin
        /// <summary>
        ///     Updates userToken and expiration date
        /// </summary>
        /// <param name="currentUser">User - current User object</param>
        /// <param name="isSucessful">Bool - if function operates correctly change to true</param>
        /// <returns>Bool - Returns true if operation was sucessful</returns>
        public bool updateLogin(ref User currentUser)
        {
            bool isSucessful = false;
            try
            {
                //Update UserToken
                currentUser.userToken = GenerateGUID();

                //Push new UserToken to server and add new TimeExpiration(done in server)
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@ID", currentUser.ID));
                spParams.Add(new SqlParameter("@Token", currentUser.userToken.ToString()));

                int rowsImpacted = DataAcess.ExecuteNonQuerySP(Constants.updateLogin, spParams);

                if (rowsImpacted == 1)
                {
                    isSucessful = true;
                }

                else
                {
                    isSucessful = false;
                }
            }
            catch
            {
                isSucessful = false;
            }
            return isSucessful;
        }
        #endregion


        //Register A User (WORKS)
        [WebMethod]
        public User RegisterUserWeb(User newUser)
        {
            //GET START TIME OF FUNCTION:
            var timeOfStart = getCurrentTime();

            bool functionSucess = false;
            int returnOption = Constants.REGnewUser;


            //Variable to check if a new user was added or if it is an existing user
            bool wasCreated = false;

            //Register User:
            functionSucess = RegisterUser(ref newUser, ref wasCreated);
            if (wasCreated && functionSucess)
                LogWM(Constants.RegisterID, newUser, functionSucess, timeOfStart);
            else if (!wasCreated && functionSucess)
            {
                LogWM(Constants.RegisterFailedID, newUser, functionSucess, timeOfStart);
                returnOption = Constants.REGexistingUser;
            }
            newUser.option = returnOption;
            return newUser;
        }


        //Update Loyalty values for a user (WORKS)
        [WebMethod]
        public User addLoyalty(User currentUser)
        {
            //IF Login has expired, update login
            bool doesExpirationWork = false;
            if (checkExpiration(ref currentUser, ref doesExpirationWork))
            {
                bool doesUpdateWork = updateLogin(ref currentUser);
            }

            DateTime startTime = getCurrentTime();
            bool sucess = UpdateLoyalty(ref currentUser);
            LogWM(Constants.UpdateLoyaltyID, currentUser, sucess, startTime);
            return currentUser;
        }

        //Get loyalty balance from server (WORKS)
        [WebMethod]
        public User getLoyaltyBalance(User currentUser)
        {
            bool sucessful = true;


            //IF Login has expired, update login
            bool doesExpirationWork = false;
            if (checkExpiration(ref currentUser, ref doesExpirationWork))
            {
                bool doesUpdateWork = updateLogin(ref currentUser);
            }

            
            LogWM(Constants.GetBalanceID, currentUser, sucessful, getCurrentTime());

            return currentUser;
        }
    }
}