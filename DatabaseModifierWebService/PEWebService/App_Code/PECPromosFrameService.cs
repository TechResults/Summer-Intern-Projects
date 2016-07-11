using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using PE.DataReturn;

/// <summary>
/// Summary description for PECPromosFrameService
/// </summary>

namespace PE.PromosFrameService
{
    [WebService(Namespace = "playerelite.com.au")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    public class PECPromosFrameService : System.Web.Services.WebService
    {

        public PECPromosFrameService()
        {
        }

        #region My Promotions Screen
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetPromotionsScreenWrapper(string mobile, string userToken)
        {
            GetPromotionsScreenWrapperReturn currentUser = new GetPromotionsScreenWrapperReturn();
            currentUser.checkSession(mobile, userToken);
            if (currentUser.validToken)
            {
                currentUser.DBGetPromotionsScreenWrapperReturn(mobile);
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
        public string GetPromotionList(string mobile, string userToken)
        {
            GetPromotionListReturn currentUser = new GetPromotionListReturn();
            currentUser.checkSession(mobile, userToken);
            if (currentUser.validToken)
            {
                currentUser.DBGetPromotionList(mobile);
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
        public string EnterRemoteEntry(string mobile, string userToken, int promotionID)
        {
            EnterRemoteEntryReturn currentUser = new EnterRemoteEntryReturn();
            currentUser.checkSession(mobile, userToken);
            if (currentUser.validToken)
            {
                currentUser.DBEnterRemoteEntry(mobile, promotionID);
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
        public string ClaimBonusCoupons(string mobile, string userToken, int promotionID)
        {
            ClaimBonusCouponsReturn currentUser = new ClaimBonusCouponsReturn();
            currentUser.checkSession(mobile, userToken);
            if (currentUser.validToken)
            {
                currentUser.DBClaimBonusCoupons(mobile, promotionID);
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
        public string ListEntriesInNextDraw(string mobile, string userToken, string promotionID)
        {
            ListEntriesInNextDrawReturn currentUser = new ListEntriesInNextDrawReturn();
            currentUser.checkSession(mobile, userToken);
            if (currentUser.validToken)
            {
                currentUser.DBListEntriesInNextDraw(mobile, Convert.ToInt32(promotionID));
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

