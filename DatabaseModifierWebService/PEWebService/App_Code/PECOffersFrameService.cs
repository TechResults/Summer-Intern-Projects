using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using PE.DataReturn;
using PE.OffersFrameDB;

/// <summary>
/// Summary description for PECOffersFrameService
/// </summary>


[WebService(Namespace = "playerelite.com.au")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]


public class PECOffersFrameService : System.Web.Services.WebService
{

    public PECOffersFrameService()
    {
    }

    #region My Offers Screen
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetOffersScreenWrapper(string mobile, string userToken)
    {
        GetOffersScreenWrapperReturn currentUser = new GetOffersScreenWrapperReturn();
        currentUser.checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser.DBGetOffersScreenWrapper(mobile);
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
    public string GetOfferDetails(string mobile, string userToken, int offerID)
    {
        GetOfferDetailsReturn currentUser = new GetOfferDetailsReturn();
        currentUser.checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser.DBGetOfferDetails(mobile, offerID);
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
    public string ExecuteRedemptionOption(string mobile, string userToken, int offerID, int optionReferenceID)
    {
        ExecuteRedemptionOptionReturn currentUser = new ExecuteRedemptionOptionReturn();
        currentUser.checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser.DBExecuteRedemptionOption(mobile, offerID, optionReferenceID);
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
    public string ViewOfferRedemptionHistory(string mobile, string userToken)
    {
        ViewOfferRedemptionHistoryReturn currentUser = new ViewOfferRedemptionHistoryReturn();
        currentUser.checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser.DBViewOfferRedemptionHistoryReturn(mobile);
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


