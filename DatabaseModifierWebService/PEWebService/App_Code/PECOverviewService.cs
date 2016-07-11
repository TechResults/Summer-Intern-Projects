using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using PE.DataReturn;

/// <summary>
/// Loads overview screen elements
/// </summary>
///
namespace PE.OverviewService
{
    [WebService(Namespace = "playerelite.com.au")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    public class PECOverviewService : System.Web.Services.WebService
    {
        #region Overview Screen
        /// <summary>
        /// Method recieves back data and labels to display in the overview frame
        /// User is checked for valid token, and then assets are loaded from a SQL DB accessing function.
        /// </summary>
        /// <param name="mobile">User's mobile number passed in as a string with the format "XXX-XXX-XXXX"</param>
        /// <param name="userToken">Current generated user token on the client side, used to check current session validity</param>
        /// IMPLEMENTED: YES
        /// DB FUNCTION IMP: NO
        /// STORED PROCEDURE IMP: NO
        /// <returns>Serialized JSON string</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetPlayerGeneralInfo(string mobile, string userToken)
        {
            GetPlayerGeneralInfoReturn currentUser = new GetPlayerGeneralInfoReturn();
            currentUser.checkSession(mobile, userToken);
            if (currentUser.validToken)
            {
                currentUser.DBGetPlayerInfo(mobile);
                currentUser.validToken = true;
            }
            else
            {
                currentUser = null;
                currentUser.validToken = false;
            }
            return new JavaScriptSerializer().Serialize(currentUser);
        }

        /// <summary>
        /// Method recieves player point bucket data and labels to display in the frame
        /// User's session is checked to ensure validity and a SQL DB accessing method is called to return data.
        /// This data is then serialized and returned as a string.
        /// </summary>
        /// <param name="mobile">User's mobile number passed in as a string with the format "XXX-XXX-XXXX"</param>
        /// <param name="userToken">Current generated user token on the client side, used to check current session validity</param>
        /// IMPLEMENTED: YES
        /// DB FUNCTION IMP: NO
        /// STORED PROCEDURE IMP: NO
        /// <returns>Serialized JSON string</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetPlayerPointBucketDetails(string mobile, string userToken)
        {
            GetPlayerPointBucketDetailsReturn currentUser = new GetPlayerPointBucketDetailsReturn();
            currentUser.checkSession(mobile, userToken);
            if (currentUser.validToken)
            {
                currentUser.DBGetPointsBucket(mobile);
            }
            else
            {
                currentUser = null;
                currentUser.validToken = false;
            }

            return new JavaScriptSerializer().Serialize(currentUser);
        }

        /// <summary>
        /// User's session is checked for validity. If valid the method recieves player card images for the front and stores them in an array of bytes.
        /// This data is then serialized and returned as a string.
        /// If the user's session is not valid, the method returns a serialized object with only validToken set to false.
        /// </summary>
        /// <param name="mobile">User's mobile number passed in as a string with the format "XXX-XXX-XXXX"</param>
        /// <param name="userToken">Current generated user token on the client side, used to check current session validity</param>
        /// IMPLEMENTED: YES
        /// DB FUNCTION IMP: NO
        /// STORED PROCEDURE IMP: NO
        /// <returns>Serialized JSON string</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetPlayerCardImageDetails(string mobile, string userToken)
        {
            GetPlayerCardImageDetailsReturn currentUser = new GetPlayerCardImageDetailsReturn();

            currentUser.checkSession(mobile, userToken);

            if (currentUser.validToken)
            {
                currentUser.DBGetPlayerCardImage(mobile);
                currentUser.validToken = true;
            }

            else
            {
                currentUser = null;
                currentUser.validToken = false;
            }

            return new JavaScriptSerializer().Serialize(currentUser);
        }
        #endregion
    }

}
