using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DataReturns
/// </summary>

namespace PE.DataReturn
{
    [Serializable]
    public class Default
    {
        public bool validToken;
        public bool checkSession(string mobile, string userToken)
        {
            throw new NotImplementedException();
            if (true) //valid
            {
                validToken = true;
            }

        }
    }

    #region Application Installation and Setup Classes
    [Serializable]
    public class RegisterNewUserResult
    {
        public bool isRegistered;
        public bool isLocked;

        public string DBGetStoredPin(string mobile)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region Application Launch Classes
    [Serializable]
    public class ValidatePhoneRegisteredResult
    {
        public string userToken;
        public bool showBalancesNoPin;
        public bool isRegistered;

        // Generate a one way hash, API does not specify implementation
        public void GenerateOneWayHash()
        {
            //SHould set userToken = to something
            throw new NotImplementedException();
        }

        // Query Database for existing mobile number. Return true if mobile number exists, false if it does not.
        public bool checkIsRegistered(string mobile)
        {
            throw new NotImplementedException();
        }
    }
    

    [Serializable]
    public class ShowBalancesOnOpeningScreenResult
    {
        public bool isValid;
        public void DBGetAccountBalancesSet(string mobile)
        {
            throw new NotImplementedException();
        }

        public Account []AccountBalances;

    }

    [Serializable]
    public class Account
    {
        public string accountName;
        public string accountBalance;
    }
    

    [Serializable]
    public class ValidateUserReturn
    {
        public bool isValid;
        public string userToken;
        public string DBGetStoredPin(string mobile)
        {
            throw new NotImplementedException();
        }
        public void GenerateOneWayHash()
        {
            throw new NotImplementedException();
            userToken = "something";

        }
    }

    [Serializable]
    public class LoadLogoReturn : Default
    {
        public byte[] logo;
        //Gets the current logo link from the server (could also be binary)
        public void DBGetLogo()
        {
            throw new NotImplementedException();
//            logo = something;
        }
    }
    #endregion

    #region Overview Screen Classes
    [Serializable]
    public class GetPlayerGeneralInfoReturn : Default
    {
        public string callToActionCaption;
        public string callToActionText;
        public bool callToActionIsScrolling;
        public string customerName;
        public string customerNumber;
        public string customerTierLevelText;
        public string customerAspirationalText;
        public string customerAwardCaption;
        public string customerAwardText;

        public void DBGetPlayerInfo(string mobile)
        {
            throw new NotImplementedException();
            //currentUser.x = something
        }
    }

    [Serializable]
    public class GetPlayerPointBucketDetailsReturn : Default
    {
        public Bucket[] customerPointBuckets;

        //Get a set of buckets from SQL DB
        public void DBGetPointsBucket(string mobile)
        {
            throw new NotImplementedException();
//            customerPointBuckets = something;
        }
    }

    [Serializable]
    public class Bucket
    {
        string bucketCaption;
        Int32 bucketPointsValue;
    }

    [Serializable]
    public class GetPlayerCardImageDetailsReturn : Default
    {
        public string playerCardImageFrontLink;
        public string playerCardImageBackLink;
        //GetPlayerCard details from the SQL DB
        public void DBGetPlayerCardImage(string mobile)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region My Games Screen
    [Serializable]
    public class GetGamesScreenWrapperReturn : Default
    {
        public string headerCaption;
        public string headerData;
        public Game[] Games;

        //SQL DB Function to get games wrapper from DB
        public void DBGetGamesScreenWrapper(string mobile)
        {
            throw new NotImplementedException();
        }
    }
    [Serializable]
    public class Game
    {
        public string gameID;
        public string gameName;
        public string gameDescription;
        public string gameIcon;
        public string buttonDescription;
        public bool isButtonEnabled;
    }

    [Serializable]
    public class GetPageAttributesReturn : Default
    {
        public string gameName;
        public string pageName;
        public string caption;
        public Attribute[] attributes;

        public GetPageAttributesReturn DBGetPageAttributes(string mobile)
        {
            throw new NotImplementedException();
        }
    }
    [Serializable]
    public class Attributes
    {
        public string typeName;
        public string objectName;
        public string attributeName;
        public string attributeValue;
        public string attributeValueBinary;
    }

    [Serializable]
    public class StartGameReturn : Default
    {
        public string gameToken;
        public string startGameCaption;
        public string startGameText;

        public void DBStartGame(int gameID)
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public class GetGameInfoForPromotionReturn : Default
    {
        public int variantID;
        public string gameNameForDisplay;
        public string playInstructions;
        public string gameType;
        public GameObject[] GameObjects;

        public void DBGetGameInfoForPromotion(int gameID, string gameToken)
        {
            throw new NotImplementedException();
        }
    }
    [Serializable]
    public class GameObject
    {
        public string objectName;
        public int objectID;
        public string GOAttributeName;
        public string GOAttributeValue;
        public string GOAttributeValueBinary;
    }

    [Serializable]
    public class SaveWinInfoReturn : Default
    {
        public string gameNameForDisplay;
        public string closingCaption;
        public string prizePickUpDescription;
        public string callToActionCaption;
        public string callToActionText;
        public string callToActionScrolling;

        //Convert parameters into object, save to database, and return object
        public void DBSaveWinInfo(string mobile, string userToken, int gameID, string gameToken, string objectsSelected)
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public class SaveLoseInfoReturn : Default
    {
        public string gameNameForDisplay;
        public string closingCaption;
        public string closingText;
        public string callToActionCaption;
        public string callToActionText;
        public string callToActionScrolling;

        //Convert parameters to object, save to DB, return object
        public void DBSaveLoseInfo(string mobile, string userToken, int gameID, string gameToken, string objectsSelected)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region My Promotions Screen
    [Serializable]
    public class GetPromotionsScreenWrapperReturn : Default
    {
        public string line1Caption;
        public string line1Data;
        public string line2Caption;
        public string line2Data;
        public PromoButton[] buttons;

        //DB Get Promotions Screen Wrapper from SQL Server
        public void DBGetPromotionsScreenWrapperReturn(string mobile)
        {
            throw new NotImplementedException();
        }
    }
    [Serializable]
    public class PromoButton
    {
        public string buttonCaption;
        public int buttonOrdPos;
    }

    [Serializable]
    public class GetPromotionListReturn : Default
    {
        public Promotion[] promotions;

        //DB Get Promotion List
        public void DBGetPromotionList(string mobile)
        {
            throw new NotImplementedException();
        }
    }
    [Serializable]
    public class Promotion
    {
        public int promotionID;
        public int promoVersionID;
        public int gameID;
        public string promotionName;
        public string startDate;
        public string endDate;
        public bool enrolled;
        public bool couponsAvaliable;
        public string nextDrawingDate;
        public bool checkInAvaliable;
        public bool freeEntryAvaliable;
        public bool remoteEntryAvaliable;
        public int pointsToDate;
        public int entriesToDate;
        public int pointsPerEntry;
        public int todaysPoint;
        public int dailyEntryLImit;
        public string thresholdType;
        public int thresholdPoints;
        public string prizeName;
        public bool thresholdReached;
        public bool prizeClaimed;
        public bool gameAvaliable;
        public string checkInStatusMessage;
        public bool raffleAvaliable;
        public RaffleTicket[] raffleTickets;
        public byte[] promotionImage;
    }
    [Serializable]
    public class RaffleTicket
    {
        public string raffleTicket;
    }

    [Serializable]
    public class EnterRemoteEntryReturn : Default
    {
        public int promotionID;
        public int updateEntryCount;
        public bool remoteEntryAvaliable;

        public void DBEnterRemoteEntry(string mobile, int promotionID)
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public class ClaimBonusCouponsReturn : Default
    {
        public int promotionID;
        public ResultDescription[] resultDescriptions;
        public bool claimButtonRemainsVisible;
        public bool claimButtonRemainsActive;
        public string claimButtonNewCaption;
        public byte[] claimButtonNewImage;

        public void DBClaimBonusCoupons(string mobile, int promotionID)
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public class ResultDescription
    {
        public string resultCaption;
        public string resultUpdatedElement;
        // LARS: Binary Data??
        public string resultImage;
        public HashSet<DataPoint> dataPoints;
    }
    [Serializable]
    public class DataPoint
    {
        public string dataPointCaption;
        public string dataPointData;
    }

    [Serializable]
    public class ListEntriesInNextDrawReturn : Default
    {
        public int promotionID;
        public DateTime nextDrawDate;
        public DateTime nextDrawTime;
        public int entriesForNextDraw;
        public bool isDrumPopulated;
        public string[] entryNumbers;
        public string entryNumber;
        public string specialMessage;

        public void DBListEntriesInNextDraw(string mobile, int promotionID)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region My Offers Screen
    [Serializable]
    public class GetOffersScreenWrapperReturn : Default
    {
        public string line1Caption;
        public string line1Data;
        public string line2Caption;
        public string line2Data;
        public OfferButton[] buttons;

        public void DBGetOffersScreenWrapper(string mobile)
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public class OfferButton
    {
        public string buttonCaption;
        public int buttonOrdPos;
        public int buttonOfferID;
        public byte[] buttonImage;
    }

    [Serializable]
    public class GetOfferDetailsReturn : Default
    {
        public int offerID;
        public string headerCaptionLine1;
        public string headerCaptionLine2;
        public byte[] offerLargeImage;
        public string offerCaption;
        public bool offerBarcodeDisplayed;
        public byte[] offerBarcode;
        public bool displayOptions;
        public Option[] offerOptions;
        public string footerCaptionLine1;
        public string footerCaptionLine2;


        public void DBGetOfferDetails(string mobile, int offerID)
        {
            throw new NotImplementedException();
        }
    }
    [Serializable]
    public class Option
    {
        public string optionCaption;
        public byte[] optionImage;
        public bool optionExecutable;
        public int optionReferenceID;
    }

    [Serializable]
    public class ExecuteRedemptionOptionReturn : Default
    {
        public int offerID;
        public int optionReferenceID;
        public bool redemptionExecutionSucess;
        public string headerCaptionLine1;
        public string headerCaptionLine2;
        public string bodyCaptionLine1;
        public string bodyCaptionLine2;
        public byte[] bodyImage1;
        public byte[] bodyImage2;
        public string footerCaptionLine1;
        public string footerCaptionLine2;

        public void DBExecuteRedemptionOption(string mobile, int offerID, int optionReferenceID)
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public class ViewOfferRedemptionHistoryReturn : Default
    {
        public string headerCaptionLine1;
        public string headerCaptionLine2;
        public bool offerHistoryExists;
        public Offer[] offerHistory;
        public string footerCaptionLine1;
        public string footerCaptionLine2;

        //SQL DB Retrieval Function:
        public void DBViewOfferRedemptionHistoryReturn(string mobile)
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public class Offer
    {
        public string description;
        public string value;
        public string expiresDate;
        public string issuedDate;
        public string prizeType;
        public bool isExpired;
        public bool isRedeemed;
        public bool hasBarcode;
        public byte[] barcode;

    }
    #endregion

    #region My Events Screen
    [Serializable]
    public class GetEventsScreenWrapperReturn : Default
    {
        public string line1Caption;
        public string line1Data;
        public string line2Caption;
        public string line2Data;
        public EventButton[] buttons;

        public void DBGetEventsScreenWrapper(string mobile)
        {
            throw new NotImplementedException();
        }
    }
    [Serializable]
    public class EventButton
    {
        public string buttonCaption;
        public int buttonOrdPos;
        public int buttonEventID;
        public byte[] buttonImage;
    }

    [Serializable]
    public class GetEventDetailsReturn : Default
    {
        public int eventID;
        public string headerCaptionLine1;
        public string headerCaptionLine2;
    }

    #endregion
}
