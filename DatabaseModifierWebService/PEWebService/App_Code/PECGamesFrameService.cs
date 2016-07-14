using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using PE.DataReturn;
using PE.GamesFrameDB;

/// <summary>
/// 
/// </summary>

[WebService(Namespace = "playerelite.com.au")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

public class PECGamesFrameService : System.Web.Services.WebService
{
    public PECGamesFrameService()
    {
    }

    #region My Games Screen
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetGamesScreenWrapper(string mobile, string userToken)
    {
        GetGamesScreenWrapperReturn currentUser = new GetGamesScreenWrapperReturn();
        currentUser.checkSession(mobile, userToken);

        if (currentUser.validToken)
        {
            currentUser.DBGetGamesScreenWrapper(mobile);
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
    //LARS: SHOULD GETINTEVALS take a variantID or should this be found from the server somehow?
    public string GetIntervalsAndBackgrounds(string mobile, string userToken, int gameID, int variantID)
    {
        GetIntervalsAndBackgroundsReturn currentUser = new GetIntervalsAndBackgroundsReturn();
        currentUser.checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser.DBGetIntervalsAndBackgrounds(mobile, gameID, variantID);
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
    public string GetPageAttributes(string mobile, string userToken, string pageName, int gameID)
    {
        GetPageAttributesReturn currentUser = new GetPageAttributesReturn();
        currentUser.checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser.DBGetPageAttributes(mobile, pageName, gameID);
            currentUser.validToken = true;
        }
        else
        {
            currentUser = null;
            currentUser.validToken = false;
        }
        return new JavaScriptSerializer().Serialize(currentUser);
    }


    // Lars: changed function to take promotionID (LET ME KNOW IF THIS IS A NO GO)
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string StartGame(string mobile, string userToken, int gameID, long promotionID)
    {
        StartGameReturn currentUser = new StartGameReturn();
        currentUser.checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser.DBStartGame(mobile, gameID, promotionID);
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
    public string GetGameInfoForPromotion(string mobile, string userToken, int gameID, string gameToken)
    {
        GetGameInfoForPromotionReturn currentUser = new GetGameInfoForPromotionReturn();
        currentUser.checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser.DBGetGameInfoForPromotion(mobile, gameID, gameToken);
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
    public string SaveWinInfo(string mobile, string userToken, int gameID, string gameToken, string objectsSelected)
    {
        SaveWinInfoReturn currentUser = new SaveWinInfoReturn();
        currentUser.checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser.DBSaveWinInfo(mobile, gameID, gameToken, objectsSelected);
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
    public string SaveLoseInfo(string mobile, string userToken, int gameID, string gameToken, string objectsSelected)
    {
        SaveLoseInfoReturn currentUser = new SaveLoseInfoReturn();
        currentUser.checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser.DBSaveLoseInfo(mobile, gameID, gameToken, objectsSelected);
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

