using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DataReturns
/// </summary>

namespace PEClient
{

    #region Application Installation and Setup Classes
    [Serializable]
    public class RegisterNewUserResult
    {
        public bool isRegistered;
        public bool isLocked;
    }
    #endregion

    #region Application Launch Classes
    [Serializable]
    public class ValidatePhoneRegisteredResult
    {
        public bool isRegistered;
        public string userToken;
        public bool showBalancesNoPin;
    }
    

    [Serializable]
    public class ShowBalancesOnOpeningScreenResult
    {
        public bool isValid;
        public HashSet<Account> AccountBalances;

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
    }

    [Serializable]
    public class LoadLogoReturn
    {
        public bool validToken;
        // LARS: Not sure if this should be represented as something else, XML says (binary)
        public string logo;
    }
    #endregion

    #region Overview Screen Classes
    [Serializable]
    public class GetPlayerGeneralInfoReturn
    {
        public bool validToken;
        public string callToActionCaption;
        public string callToActionText;
        public bool callToActionIsScrolling;
        public string customerName;
        public string customerNumber;
        public string customerTierLevelText;
        public string customerAspirationalText;
        public string customerAwardCaption;
        public string customerAwardText;
    }

    [Serializable]
    public class GetPlayerPointBucketDetailsReturn
    {
        public bool validToken;
        public HashSet<Bucket> customerPointBuckets;
        /*
        public class Bucket
        {
            string bucketCaption;
            Int32 bucketPointsValue;
        }
        */
    }
    [Serializable]
    public class Bucket
    {
        string bucketCaption;
        Int32 bucketPointsValue;
    }

    [Serializable]
    public class GetPlayerCardImageDetailsReturn
    {
        public bool validToken;
        public string playerCardImageFrontLink;
        public string playerCardImageBackLink;
    }
    #endregion
    #region My Games Screen
    [Serializable]
    public class GetGamesScreenWrapperReturn
    {
        public bool validToken;
        public string headerCaption;
        public string headerData;
        public HashSet<Game> Games;
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
    public class GetPageAttributesReturn
    {
        public bool validToken;
        public string gameName;
        public string pageName;
        public string caption;
        public HashSet<Attributes> attributes;
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
    public class StartGameReturn
    {
        public bool validToken;
        public string gameToken;
        public string startGameCaption;
        public string startGameText;
    }
    [Serializable]
    public class GetGameInfoForPromotionReturn
    {
        public bool validToken;
        public int variantID;
        public string gameNameForDisplay;
        public string playInstructions;
        public string gameType;
        public HashSet<GameObject> GameObjects;
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
    public class SaveWinInfoReturn
    {
        public bool validToken;
        public string gameNameForDisplay;
        public string closingCaption;
        public string prizePickUpDescription;
        public string callToActionCaption;
        public string callToActionText;
        public string callToActionScrolling;
    }
    [Serializable]
    public class SaveLoseInfoReturn
    {
        public bool validToken;
        public string gameNameForDisplay;
        public string closingCaption;
        public string closingText;
        public string callToActionCaption;
        public string callToActionText;
        public string callToActionScrolling;
    }
    #endregion

    #region My Promotions Screen
    [Serializable]
    public class GetPromotionsScreenWrapperReturn
    {
        public bool validToken;
        public string line1Caption;
        public string line1Data;
        public string line2Caption;
        public string line2Data;
        HashSet<PromoButton> buttons;
    }

    [Serializable]
    public class PromoButton
    {
        public string buttonCaption;
        public int buttonOrdPos;
    }

    [Serializable]
    public class GetPromotionListReturn
    {
        public bool validToken;
        HashSet<Promotion> promotions;
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
        public HashSet<RaffleTicket> raffleTickets;
        // LARS: Should it be some way of transmitting image
        public string promotionImage;
    }

    [Serializable]
    public class RaffleTicket
    {
        public string raffleTicket;
    }

    [Serializable]
    public class EnterRemoteEntryReturn
    {
        public bool validToken;
        public int promotionID;
        public int updateEntryCount;
        public bool remoteEntryAvaliable;
    }

    [Serializable]
    public class ClaimBonusCouponsReturn
    {
        public bool validToken;
        public int promotionID;
        public HashSet<ResultDescription> resultDescriptions;
        public bool claimButtonRemainsVisible;
        public bool claimButtonRemainsActive;
        public string claimButtonNewCaption;
        // LARS: Binary Data??
        public string claimButtonNewImage;
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
    public class ListEntriesInNextDrawReturn
    {
        public bool validToken;
        public int promotionID;
        public DateTime nextDrawDate;
        public DateTime nextDrawTime;
        public int entriesForNextDraw;
        public bool isDrumPopulated;
        public HashSet<string> entryNumbers;
        public string entryNumber;
        public string specialMessage;
    }
    #endregion

    #region My Offers Screen
    [Serializable]
    public class GetOffersScreenWrapperReturn
    {
        public bool validToken;
        public string line1Caption;
        public string line1Data;
        public string line2Caption;
        public string line2Data;
        public HashSet<OfferButton> buttons;
    }

    [Serializable]
    public class OfferButton
    {
        public string buttonCaption;
        public int buttonOrdPos;
        public int buttonOfferID;
        // LARS: Binary Data??
        // |
        // v
        public string buttonImage;
    }

    [Serializable]
    public class GetOfferDetailsReturn
    {
        public bool validToken;
        public int offerID;
        public string headerCaptionLine1;
        public string headerCaptionLine2;
        // LARS: Binary Data??
        public string offerLargeImage;

        public string offerCaption;
        public bool offerBarcodeDisplayed;
        // LARS: Binary Data??
        public string offerBarcode;

        public bool displayOptions;
        public HashSet<Option> offerOptions;
        public string footerCaptionLine1;
        public string footerCaptionLine2;
    }
    [Serializable]
    public class Option
    {
        public string optionCaption;
        // LARS: Binary Data??
        public string optionImage;

        public bool optionExecutable;
        public int optionReferenceID;
    }

    [Serializable]
    public class ExecuteRedemptionOptionReturn
    {
        public bool validToken;
        public int offerID;
        public int optionReferenceID;
        public bool redemptionExecutionSucess;
        public string headerCaptionLine1;
        public string headerCaptionLine2;
        public string bodyCaptionLine1;
        public string bodyCaptionLine2;
        // LARS: binary data??
        public string bodyImage1;
        public string bodyImage2;

        public string footerCaptionLine1;
        public string footerCaptionLine2;
    }

    [Serializable]
    public class ViewOfferRedemptionHistoryReturn
    {
        public bool validToken;
        public string headerCaptionLine1;
        public string headerCaptionLine2;
        public bool offerHistoryExists;
        public HashSet<Offer> offerHistory;
        public string footerCaptionLine1;
        public string footerCaptionLine2;
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
        // LARS: Binary Data:
        public string barcode;

    }
    #endregion
}
