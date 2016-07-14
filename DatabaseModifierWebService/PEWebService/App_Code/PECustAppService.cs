using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using PE.CustAppDB;

/// <summary>
/// CustAppService deals with user login, registration, and loading of assets at launch
/// </summary>

[WebService(Namespace = "playerelite.com.au")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class PECustAppService : System.Web.Services.WebService
{
    int registrationTries;
    public PECustAppService()
    {
        registrationTries = 0;
    }

    #region Application Installation and Setup
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string RegisterNewUser(string mobile, string pinCode)
    {
        RegisterNewUserResult newUser = new RegisterNewUserResult();
        if (registrationTries <= 3)
        {
            newUser.isLocked = false;
            newUser.DBGetStoredPin(mobile, pinCode);
            if (newUser.isRegistered)
            {
                newUser.isRegistered = true;
            }
            else
            {
                registrationTries++;
            }
        }
        else
        {
            newUser.isLocked = true;
        }
        return new JavaScriptSerializer().Serialize(newUser);
    }
    #endregion

    #region Application Launch

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string ValidatePhoneRegistered(string mobile)
    {
        ValidatePhoneRegisteredResult currentUser = new ValidatePhoneRegisteredResult();
        currentUser.checkIsRegistered(mobile);
        if (currentUser.IsRegistered)
        {
            currentUser.GenerateOneWayHash(mobile);
            currentUser.IsRegistered = true;
            //Show balances based on configuration (Config is not elaborated by API DOC)
            currentUser.ShowBalancesNoPin = true;
        }
        else
        {
            currentUser.IsRegistered = false;
        }
        return new JavaScriptSerializer().Serialize(currentUser);
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string ShowBalancesOnOpeningScreen(string mobile, string userToken)
    {
        ShowBalancesOnOpeningScreenResult currentUser = new ShowBalancesOnOpeningScreenResult();
        currentUser.checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser.checkSession(mobile, userToken);
            currentUser.DBGetAccountBalancesSet(mobile);
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
    public string ValidateUser(string mobile, string pinNum)
    {
        ValidateUserReturn currentUser = new ValidateUserReturn();
        currentUser.DBGetStoredPin(mobile, pinNum);

        return new JavaScriptSerializer().Serialize(currentUser);

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string LoadLogo(string mobile, string userToken)
    {
        LoadLogoReturn currentUser = new LoadLogoReturn();
        currentUser.checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser.DBGetLogo(mobile);
        }
        else
        {
            currentUser.logo = null;
        }
        return new JavaScriptSerializer().Serialize(currentUser);

    }
    #endregion

}



