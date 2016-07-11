using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using PE.DataReturn;

/// <summary>
/// Summary description for PECOverviewService
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
        /// Method validates the current user account by pinCode
        /// </summary>
        /// <param name="mobile">User's mobile number passed in as a string with the format "XXX-XXX-XXXX"</param>
        /// <param name="userToken">Current generated user token on the client side, used to check current session validity</param>
        /// IMPLEMENTED: YES
        /// DB FUNCTION IMP: YES (With the exception of GenerateOneWayHash)
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
