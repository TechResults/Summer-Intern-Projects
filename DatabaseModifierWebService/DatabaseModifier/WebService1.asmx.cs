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
        public const int GetHash = 1;
        public const string EllisID = "702-496-9401";
        public const string OwenID = "702-499-3967";

    }
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
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

        public static string GetHash(string searchNum, ref int UserID, ref bool complete)
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
                    while(userReader.Read())
                    {
                        tempStringUserID = userReader["UserID"].ToString();
                    }
                }

                UserID = Convert.ToInt32(tempStringUserID);

                    //Add Expiration Time
                int numberAdditions = addCmd.ExecuteNonQuery();

                if(numberAdditions == 1 && UserID != -1)
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

        public static void LogWM(int MethodID, int UserID, bool sucess, DateTime startTime)
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

                myConnection.Close();
            }
        }        



        #endregion

        //Generate A Hash Code 
        [WebMethod]
        public string GenerateHash()
        {
            
            //GET START TIME OF FUNCTION:
            var timeOfStart = new DateTime();
            timeOfStart = DateTime.Now;

            //Initialize Local Variables
            int UserID = -1;
            bool isSuccessful = false;
            
            //Get Hash
            string hash = GetHash(Constants.EllisID, ref UserID, ref isSuccessful);
            LogWM(Constants.GetHash, UserID, isSuccessful, timeOfStart);
            return hash;
        }
    }
}

