using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using PEClient;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Constants
{
    //WebMethodIDS:
    public const int ShowBalancesOnOpeningScreenID = 3;
}
public class Service : System.Web.Services.WebService
{
    private int registrationTries;

    public Service()
    {
        //Uncomment the following line if using designed components 
        registrationTries = 0;
        //InitializeComponent(); 
    }

    [WebMethod]
    public RegisterNewUserResult RegisterNewUser(string mobile, string pinCode)
    {
        RegisterNewUserResult newUser = new RegisterNewUserResult();
        if (registrationTries <= 3)
        {
            newUser.isLocked = false;
            string storedPin = DBGetStoredPin(mobile);
            if (pinCode == storedPin)
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
        return newUser;
    }

    // SQL DB Get Initial Stored PIN for RegisterNewUser function
    private string DBGetStoredPin(string mobile)
    {
        throw new NotImplementedException();
    }

    [WebMethod]
    public ValidatePhoneRegisteredResult ValidatePhoneRegistered(string mobile)
    {
        ValidatePhoneRegisteredResult currentUser = new ValidatePhoneRegisteredResult();
        if (isRegistered(mobile))
        {
            currentUser.isRegistered = true;
            currentUser.userToken = GenerateOneWayHash();

            //Show balances based on configuration (Config is not elaborated by API DOC)
            currentUser.showBalancesNoPin = true;
        }
        else
        {
            currentUser.isRegistered = false;
        }
        return currentUser;
    }

    // Generate a one way hash, API does not specify implementation
    private string GenerateOneWayHash()
    {
        throw new NotImplementedException();
    }

    // Query Database for existing mobile number. Return true if mobile number exists, false if it does not.
    private bool isRegistered(string mobile)
    {
        throw new NotImplementedException();
    }

    //ID: 3
    [WebMethod]
    public ShowBalancesOnOpeningScreenResult ShowBalancesOnOpeningScreen(string mobile, string userToken)
    {
        ShowBalancesOnOpeningScreenResult currentUser = new ShowBalancesOnOpeningScreenResult();

        currentUser.AccountBalances = DBGetAccountBalancesSet(mobile);

        return currentUser;
    }

    //Get Account Set From SQL DB
    private HashSet<Account> DBGetAccountBalancesSet(string mobile)
    {
        throw new NotImplementedException();
    }


    // Return True if the user is Valid, or False if they are not.
    [WebMethod]
    public ValidateUserReturn ValidateUser(string mobile, string pinNum)
    {
        ValidateUserReturn currentUser = new ValidateUserReturn();
        string storedPIN = DBGetStoredPin(mobile);
        if (storedPIN == pinNum)
        {
            currentUser.isValid = true;
            currentUser.userToken = GenerateOneWayHash();
        }
        else
        {
            currentUser.isValid = false;
            currentUser.userToken = "";
        }

        return currentUser;

    }

    [WebMethod]
    public LoadLogoReturn LoadLogo(string mobile, string userToken)
    {
        LoadLogoReturn currentUser = new LoadLogoReturn();
        currentUser.validToken = checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser.logo = DBGetLogo();
        }
        else
        {
            currentUser.logo = null;
        }
        return currentUser;

    }

    //Gets the current logo link from the server (could also be binary)
    private string DBGetLogo()
    {
        throw new NotImplementedException();
    }

    //Query DB to see if UserToken & Current Time are still valid
    private bool checkSession(string mobile, string userToken)
    {
        throw new NotImplementedException();
    }

    [WebMethod]
    public GetPlayerGeneralInfoReturn GetPlayerGeneralInfo(string mobile, string userToken)
    {
        GetPlayerGeneralInfoReturn currentUser = new GetPlayerGeneralInfoReturn();
        currentUser.validToken = checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser = DBGetPlayerInfo(mobile);
            currentUser.validToken = true;
        }
        else
        {
            currentUser = null;
            currentUser.validToken = false;
        }
        return currentUser;
    }

    //Gets player information from SQL database. 
    private GetPlayerGeneralInfoReturn DBGetPlayerInfo(string mobile)
    {
        throw new NotImplementedException();
    }

    [WebMethod]
    public GetPlayerPointBucketDetailsReturn GetPlayerPointBucketDetails(string mobile, string userToken)
    {
        GetPlayerPointBucketDetailsReturn currentUser = new GetPlayerPointBucketDetailsReturn();
        currentUser.validToken = checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser.customerPointBuckets = DBGetPointsBucket(mobile);
        }
        else
        {
            currentUser = null;
            currentUser.validToken = false;
        }

        return currentUser;

    }

    //Get a set of buckets from SQL DB
    private HashSet<Bucket> DBGetPointsBucket(string mobile)
    {
        throw new NotImplementedException();
    }

    [WebMethod]
    public GetPlayerCardImageDetailsReturn GetPlayerCardImageDetails(string mobile, string userToken)
    {
        GetPlayerCardImageDetailsReturn currentUser = new GetPlayerCardImageDetailsReturn();

        currentUser.validToken = checkSession(mobile, userToken);

        if (currentUser.validToken)
        {
            currentUser = DBGetPlayerCardImage(mobile);
            currentUser.validToken = true;
        }

        else
        {
            currentUser = null;
            currentUser.validToken = false;
        }

        return currentUser;
    }

    //GetPlayerCard details from the SQL DB
    private GetPlayerCardImageDetailsReturn DBGetPlayerCardImage(string mobile)
    {
        throw new NotImplementedException();
    }

    [WebMethod]
    public GetGamesScreenWrapperReturn GetGamesScreenWrapper(string mobile, string userToken)
    {
        GetGamesScreenWrapperReturn currentUser = new GetGamesScreenWrapperReturn();
        currentUser.validToken = checkSession(mobile, userToken);

        if (currentUser.validToken)
        {
            currentUser = DBGetGamesScreenWrapper(mobile);
            currentUser.validToken = true;
        }
        else
        {
            currentUser = null;
            currentUser.validToken = false;
        }
        return currentUser;
    }

    //SQL DB Function to get games wrapper from DB
    private GetGamesScreenWrapperReturn DBGetGamesScreenWrapper(string mobile)
    {
        throw new NotImplementedException();
    }

    [WebMethod]
    public GetPageAttributesReturn GetPageAttributes(string mobile, string userToken, string pageName)
    {
        GetPageAttributesReturn currentUser = new GetPageAttributesReturn();
        currentUser.validToken = checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser = DBGetPageAttributes(mobile);
            currentUser.validToken = true;
        }
        else
        {
            currentUser = null;
            currentUser.validToken = false;
        }
        return currentUser;
    }

    private GetPageAttributesReturn DBGetPageAttributes(string mobile)
    {
        throw new NotImplementedException();
    }

    [WebMethod]
    public StartGameReturn StartGame(string mobile, string userToken, int gameID)
    {
        StartGameReturn currentUser = new StartGameReturn();
        currentUser.validToken = checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser = DBStartGame(gameID);
            currentUser.validToken = true;
        }
        else
        {
            currentUser = null;
            currentUser.validToken = false;
        }
        return currentUser;
    }

    private StartGameReturn DBStartGame(int gameID)
    {
        throw new NotImplementedException();
    }

    [WebMethod]
    public GetGameInfoForPromotionReturn GetGameInfoForPromotion(string mobile, string userToken, int gameID, string gameToken)
    {
        GetGameInfoForPromotionReturn currentUser = new GetGameInfoForPromotionReturn();
        currentUser.validToken = checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser = DBGetGameInfoForPromotion(gameID, gameToken);
            currentUser.validToken = true;
        }
        else
        {
            currentUser = null;
            currentUser.validToken = false;
        }
        return currentUser;
    }

    private GetGameInfoForPromotionReturn DBGetGameInfoForPromotion(int gameID, string gameToken)
    {
        throw new NotImplementedException();
    }

    [WebMethod]
    public SaveWinInfoReturn SaveWinInfo(string mobile, string userToken, int gameID, string gameToken, string objectsSelected)
    {
        SaveWinInfoReturn currentUser = new SaveWinInfoReturn();
        currentUser.validToken = checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser = DBSaveWinInfo(mobile, userToken, gameID, gameToken, objectsSelected);
            currentUser.validToken = true;
        }
        else
        {
            currentUser = null;
            currentUser.validToken = false;
        }
        return currentUser;
    }

    //Convert parameters into object, save to database, and return object
    private SaveWinInfoReturn DBSaveWinInfo(string mobile, string userToken, int gameID, string gameToken, string objectsSelected)
    {
        throw new NotImplementedException();
    }

    [WebMethod]
    public SaveLoseInfoReturn SaveLoseInfo(string mobile, string userToken, int gameID, string gameToken, string objectsSelected)
    {
        SaveLoseInfoReturn currentUser = new SaveLoseInfoReturn();
        currentUser.validToken = checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser = DBSaveLoseInfo(mobile, userToken, gameID, gameToken, objectsSelected);
            currentUser.validToken = true;
        }
        else
        {
            currentUser = null;
            currentUser.validToken = false;
        }
        return currentUser;
    }

    //Convert parameters to object, save to DB, return object
    private SaveLoseInfoReturn DBSaveLoseInfo(string mobile, string userToken, int gameID, string gameToken, string objectsSelected)
    {
        throw new NotImplementedException();
    }

    public GetPromotionsScreenWrapperReturn GetPromotionsScreenWrapper(string mobile, string userToken)
    {
        GetPromotionsScreenWrapperReturn currentUser = new GetPromotionsScreenWrapperReturn();
        currentUser.validToken = checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser = DBGetPromotionsScreenWrapperReturn(mobile);
            currentUser.validToken = true;
        }
        else
        {
            currentUser = null;
            currentUser.validToken = false;
        }
        return currentUser;
    }

    //DB Get Promotions Screen Wrapper from SQL Server
    private GetPromotionsScreenWrapperReturn DBGetPromotionsScreenWrapperReturn(string mobile)
    {
        throw new NotImplementedException();
    }

    public GetPromotionListReturn GetPromotionList(string mobile, string userToken)
    {
        GetPromotionListReturn currentUser = new GetPromotionListReturn();
        currentUser.validToken = checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser = DBGetPromotionList(mobile);
            currentUser.validToken = true;
        }
        else
        {
            currentUser = null;
            currentUser.validToken = false;
        }
        return currentUser;
    }

    //DB Get Promotion List
    private GetPromotionListReturn DBGetPromotionList(string mobile)
    {
        throw new NotImplementedException();
    }

    public EnterRemoteEntryReturn EnterRemoteEntry(string mobile, string userToken, int promotionID)
    {
        EnterRemoteEntryReturn currentUser = new EnterRemoteEntryReturn();
        currentUser.validToken = checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser = DBEnterRemoteEntry(mobile, promotionID);
            currentUser.validToken = true;
        }
        else
        {
            currentUser = null;
            currentUser.validToken = false;
        }
        return currentUser;
    }

    private EnterRemoteEntryReturn DBEnterRemoteEntry(string mobile, int promotionID)
    {
        throw new NotImplementedException();
    }

    public ClaimBonusCouponsReturn ClaimBonusCoupons(string mobile, string userToken, int promotionID)
    {
        ClaimBonusCouponsReturn currentUser = new ClaimBonusCouponsReturn();
        currentUser.validToken = checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser = DBClaimBonusCoupons(mobile, promotionID);
            currentUser.validToken = true;
        }
        else
        {
            currentUser = null;
            currentUser.validToken = false;
        }
        return currentUser;
    }

    private ClaimBonusCouponsReturn DBClaimBonusCoupons(string mobile, int promotionID)
    {
        throw new NotImplementedException();
    }

    public ListEntriesInNextDrawReturn ListEntriesInNextDraw(string mobile, string userToken, string promotionID)
    {
        ListEntriesInNextDrawReturn currentUser = new ListEntriesInNextDrawReturn();
        currentUser.validToken = checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser = DBListEntriesInNextDraw(mobile, Convert.ToInt32(promotionID));
            currentUser.validToken = true;
        }
        else
        {
            currentUser = null;
            currentUser.validToken = false;
        }
        return currentUser;
    }

    private ListEntriesInNextDrawReturn DBListEntriesInNextDraw(string mobile, int promotionID)
    {
        throw new NotImplementedException();
    }

    public GetOffersScreenWrapperReturn GetOffersScreenWrapper(string mobile, string userToken)
    {
        GetOffersScreenWrapperReturn currentUser = new GetOffersScreenWrapperReturn();
        currentUser.validToken = checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser = DBGetOffersScreenWrapper(mobile);
            currentUser.validToken = true;
        }
        else
        {
            currentUser = null;
            currentUser.validToken = false;
        }
        return currentUser;
    }

    private GetOffersScreenWrapperReturn DBGetOffersScreenWrapper(string mobile)
    {
        throw new NotImplementedException();
    }

    public GetOfferDetailsReturn GetOfferDetails(string mobile, string userToken, int offerID)
    {
        GetOfferDetailsReturn currentUser = new GetOfferDetailsReturn();
        currentUser.validToken = checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser = DBGetOfferDetails(mobile, offerID);
            currentUser.validToken = true;
        }
        else
        {
            currentUser = null;
            currentUser.validToken = false;
        }
        return currentUser;
    }

    private GetOfferDetailsReturn DBGetOfferDetails(string mobile, int offerID)
    {
        throw new NotImplementedException();
    }

    public ExecuteRedemptionOptionReturn ExecuteRedemptionOption(string mobile, string userToken, int offerID, int optionReferenceID)
    {
        ExecuteRedemptionOptionReturn currentUser = new ExecuteRedemptionOptionReturn();
        currentUser.validToken = checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser = DBExecuteRedemptionOption(mobile, offerID, optionReferenceID);
            currentUser.validToken = true;
        }
        else
        {
            currentUser = null;
            currentUser.validToken = false;
        }
        return currentUser;
    }

    private ExecuteRedemptionOptionReturn DBExecuteRedemptionOption(string mobile, int offerID, int optionReferenceID)
    {
        throw new NotImplementedException();
    }

    public ViewOfferRedemptionHistoryReturn ViewOfferRedemptionHistory(string mobile, string userToken)
    {
        ViewOfferRedemptionHistoryReturn currentUser = new ViewOfferRedemptionHistoryReturn();
        currentUser.validToken = checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser = DBViewOfferRedemptionHistoryReturn(mobile);
            currentUser.validToken = true;
        }
        else
        {
            currentUser = null;
            currentUser.validToken = false;
        }
        return currentUser;
    }


    //SQL DB Retrieval Function:
    private ViewOfferRedemptionHistoryReturn DBViewOfferRedemptionHistoryReturn(string mobile)
    {
        throw new NotImplementedException();
    }

}