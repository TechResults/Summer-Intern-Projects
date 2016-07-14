using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using PE.DataReturn;
using PE.OverviewDB;

/// <summary>
/// Loads overview screen elements
/// </summary>
///

[WebService(Namespace = "playerelite.com.au")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

public class PECOverviewService : System.Web.Services.WebService
{
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
}

