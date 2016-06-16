using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;
using PE.DataModifier;
using System.IO;

/* EXAMPLE
 *                   DataSet result9 = new DataSet();
 *                   spParams = new List<SqlParameter>();
 *                   spParams.Add(new SqlParameter("@promoid", promotionId));
 *                   spParams.Add(new SqlParameter("@bucketid", bucketId));
 *                   spParams.Add(new SqlParameter("@drawingdatetime", drawingDateTime));
 *                   result9 = DataAccess.ExecuteQuerySP("PROMOTION_DRAWING_GetNextForEvent", spParams);
 */


/// <summary>
/// Summary description for DataReturns
/// </summary>

namespace PE.DataReturn
{
    public class ServerSide
    {
        public static DateTime GetTime()
        {
            DataTable dt = DataAcess.ExecuteQuerySP("DRAW_GET_TIMEATSERVER", null).Tables[0];
            return DateTime.Parse(dt.Rows[0][0].ToString());
        }
        public static string DBGetCMSPlayerID(string mobile)
        {
            string CMSplayerID = "";
            return CMSplayerID;
        }
    }
    static class StoredProcedure
    {
        public const string GetGameInfoForPromotion = "";
    }

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

        public Account[] AccountBalances;

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
    //TODO: Make sure foreach / for work correctly. Need to attempt to run DB GET functions at some point
    public class GetGamesScreenWrapperReturn : Default
    {
        public string headerCaption;
        public string headerData;
        public List<Game> Games;
        public List<long> PromotionID;
        //SQL DB Function to get games wrapper from DB
        public GetGamesScreenWrapperReturn DBGetGamesScreenWrapper(string mobile, string ipAddress)
        {
            
            string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);
            DataSet result = new DataSet();
            List<SqlParameter> spParams = new List<SqlParameter>();
            spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
            result = DataAcess.ExecuteQuerySP("PEC.PROMOTION_ID_GetByCMSPlayerID", spParams);

            GetGamesScreenWrapperReturn data = new GetGamesScreenWrapperReturn();

            if (result.Tables[0].Rows.Count > 0)
            {
                
                for (int i = 0; i < result.Tables[0].Rows.Count; i++)
                {
                    data.PromotionID.Add(Int64.Parse(result.Tables[0].Rows[i]["PromotionID"].ToString()));
                }
                foreach (long ID in PromotionID)
                {
                    //GET Game Attributes from GD_PROMOtionGames
                    DataSet ds = new DataSet();
                    List<SqlParameter> gameParams = new List<SqlParameter>();
                    gameParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                    gameParams.Add(new SqlParameter("@PromotionID", ID));
                    gameParams.Add(new SqlParameter("@IPAddress", ipAddress));

                    ds = DataAcess.ExecuteQuerySP("PEC.MG_PROMOTION_WRAPPER_GetByPromotionID", gameParams);

                    DataSet imageSet = new DataSet();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Game newGame = new Game();
                        newGame.gameID = ds.Tables[0].Rows[0]["GameID"].ToString();
                        newGame.gameName = ds.Tables[0].Rows[0]["GameName"].ToString();
                        newGame.gameDescription = ds.Tables[0].Rows[0]["GameDescription"].ToString();
                        newGame.buttonDescription = ds.Tables[0].Rows[0]["ButtonDesc"].ToString();
                        newGame.isButtonEnabled = Convert.ToBoolean(ds.Tables[0].Rows[0]["isButtonEnabled"].ToString());

                        DataSet imageData = new DataSet();
                        List<SqlParameter> imageParam = new List<SqlParameter>();
                        imageParam.Add(new SqlParameter("@PromotionID", ID));
                        imageData = DataAcess.ExecuteQuerySP("MG_PROMOTION_WRAPPER_ReadGameIcon", imageParam);
                        if (imageData.Tables[0].Rows.Count > 0)
                        {
                            MemoryStream ms = new MemoryStream((byte[])imageData.Tables[0].Rows[0]["promoKioskImage"]);
                            byte[] bytes = ms.ToArray();
                            newGame.gameIcon = bytes;
                        }
                        else
                        {
                            newGame.gameIcon = null;
                        }
                        data.Games.Add(newGame);
                    }
                    else
                    {
                        return null;
                    }

                }
            }
            else
            {
                data = null;
            }

            return data;
        }
    }
    [Serializable]
    public class Game
    {
        public string gameID;
        public string gameName;
        public string gameDescription;
        public byte[] gameIcon;
        public string buttonDescription;
        public bool isButtonEnabled;
    }


    // Lars: Where should i be looking for AttributeBinaryValue? Would I just select all 'image' from AttributeName and store the
    // value as attributeBinaryValue, else leave blank? Need to know for writing this method.

    // TODO: Implement attributeBinaryValue (clarify with LK)
    [Serializable]
    public class GetIntervalsAndBackgroundsReturn : Default
    {
        public List<AttributeInfo> Attributes;

        public GetIntervalsAndBackgroundsReturn DBGetIntervalsAndBackgrounds(string mobile, int gameID, int variantID, string IPAddress)
        {
            GetIntervalsAndBackgroundsReturn data = new GetIntervalsAndBackgroundsReturn();

            string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);
            DataSet result = new DataSet();
            List<SqlParameter> spParams = new List<SqlParameter>();
            spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
            spParams.Add(new SqlParameter("@GameID", gameID));
            spParams.Add(new SqlParameter("@VariantID", variantID));
            spParams.Add(new SqlParameter("@IPAddress", IPAddress));

            result = DataAcess.ExecuteQuerySP("MG_PROMOTION_BACKGROUND_GetByGameID", spParams);

            if(result.Tables[0].Rows.Count > 0)
            {
                for (int j = 0; j < result.Tables[0].Rows.Count; j++)
                {
                    AttributeInfo gameAttributes = new AttributeInfo();
                    gameAttributes.gameName = result.Tables[0].Rows[j]["gameName"].ToString();
                    gameAttributes.pageName = result.Tables[0].Rows[j]["pageName"].ToString();
                    gameAttributes.typeName = result.Tables[0].Rows[j]["typename"].ToString();
                    gameAttributes.objectName = result.Tables[0].Rows[j]["objectName"].ToString();
                    gameAttributes.attributeName = result.Tables[0].Rows[j]["attributeName"].ToString();
                    gameAttributes.attributeValue = result.Tables[0].Rows[j]["attributeValue"].ToString();


                    // TODO: See above for LK check
                    DataSet binaryData = new DataSet();
                    List<SqlParameter> binaryParams = new List<SqlParameter>();
                    binaryParams.Add(new SqlParameter("@GameID", gameID));
                    binaryParams.Add(new SqlParameter("@VariantID", variantID));

                    binaryData = DataAcess.ExecuteQuerySP("TODO", binaryParams);

                    if (binaryData.Tables[0].Rows.Count > 0)
                    {
                        MemoryStream ms = new MemoryStream((byte[])binaryData.Tables[0].Rows[0]["attributeBinaryData"]);
                        byte[] bytes = ms.ToArray();
                        gameAttributes.attributeBinaryValue = bytes;
                    }
                    else
                    {
                        gameAttributes.attributeBinaryValue = null;
                    }
                    data.Attributes.Add(gameAttributes);
                }
            }

            else
            {
                data = null;
            }
            return data;
        }
    }
    [Serializable]
    public class AttributeInfo
    {
        public string gameName;
        public string pageName;
        public string typeName;
        public string objectName;
        public string attributeName;
        public string attributeValue;
        public byte[] attributeBinaryValue;
    }

    [Serializable]
    public class GetPageAttributesReturn : Default
    {
        public string gameName;
        public string pageName;
        public string caption;
        public List<Attributes> listAttributes;

        public GetPageAttributesReturn DBGetPageAttributes(string mobile, string pageName, int gameID, string IPAddress)
        {
            GetPageAttributesReturn data = new GetPageAttributesReturn();

            string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);
            DataSet result = new DataSet();
            List<SqlParameter> spParams = new List<SqlParameter>();
            spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
            spParams.Add(new SqlParameter("@GameID", gameID));
            spParams.Add(new SqlParameter("@IPAddress", IPAddress));
            spParams.Add(new SqlParameter("@PageName", pageName));

            result = DataAcess.ExecuteQuerySP("PEC.MG_PROMOTION_GetGamePageNameANDCaption", spParams);
            if(result.Tables[0].Rows.Count > 0)
            {
                data.gameName = result.Tables[0].Rows[0]["GameName"].ToString();
                data.pageName = result.Tables[0].Rows[0]["PageName"].ToString();
                data.caption = result.Tables[0].Rows[0]["Caption"].ToString();

                DataSet aR = new DataSet();
                List<SqlParameter> attParams = new List<SqlParameter>();
                attParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                attParams.Add(new SqlParameter("@GameID", gameID));
                attParams.Add(new SqlParameter("@IPAddress", IPAddress));
                attParams.Add(new SqlParameter("@PageName", pageName));

                aR = DataAcess.ExecuteQuerySP("PEC", attParams);
                
                if(aR.Tables[0].Rows.Count > 0)
                {
                    for(int j = 0; j < aR.Tables[0].Rows.Count; j++)
                    {
                        Attributes pageAttributes = new Attributes();
                        pageAttributes.typeName = aR.Tables[0].Rows[j]["TypeName"].ToString();
                        pageAttributes.objectName = aR.Tables[0].Rows[j]["ObjectName"].ToString();
                        pageAttributes.attributeName = aR.Tables[0].Rows[j]["AttributeName"].ToString();
                        pageAttributes.attributeValue = aR.Tables[0].Rows[j]["AttributeValue"].ToString();


                        DataSet binaryData = new DataSet();
                        List<SqlParameter> binaryParams = new List<SqlParameter>();
                        binaryParams.Add(new SqlParameter("@GameID", gameID));
                        binaryParams.Add(new SqlParameter("@PageName", pageName));

                        binaryData = DataAcess.ExecuteQuerySP("TODO", binaryParams);

                        if (binaryData.Tables[0].Rows.Count > 0)
                        {
                            MemoryStream ms = new MemoryStream((byte[])binaryData.Tables[0].Rows[0]["attributeBinaryData"]);
                            byte[] bytes = ms.ToArray();
                            pageAttributes.attributeValueBinary = bytes;
                        }
                        else
                        {
                            pageAttributes.attributeValueBinary = null;
                        } 

                        data.listAttributes.Add(pageAttributes);
                    }
                    
                }
                //ELSE: There are no attributes for a given pageNumber
                else
                {
                    data = null;
                }
            }
            else
            {
                data = null;
            }

            return data;
        }
    }
    [Serializable]
    public class Attributes
    {
        public string typeName;
        public string objectName;
        public string attributeName;
        public string attributeValue;
        public byte[] attributeValueBinary;
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

        public void DBGetGameInfoForPromotion(string mobile, int gameID, string gameToken)
        {
            if(gameID == 0 || gameToken == null)
            {
                throw new WrongParamsError();
            }
            else
            {
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@Mobile", mobile));
                spParams.Add(new SqlParameter("@GameID", gameID));
                spParams.Add(new SqlParameter("@GameToken", gameToken));
                result = DataAcess.ExecuteQuerySP(StoredProcedure.GetGameInfoForPromotion, spParams);
                gameNameForDisplay = result.Tables[0].Rows[0]["GameName"].ToString();
                playInstructions = result.Tables[0].Rows[0]["PlayInstructions"].ToString();
                gameType = result.Tables[0].Rows[0]["GameType"].ToString();
            }
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
        public byte[] eventLargeImage;
        public string eventCaption;
        public DateTime eventStartDate;
        public DateTime eventStartTime;
        public DateTime eventEndDate;
        public DateTime eventEndTime;
        public bool displayOptions;
        public EventOption[] eventOptions;
        public string footerCaptionLine1;
        public string footerCaptionLine2;

        public void DBGetEventDetails(string mobile, int eventID)
        {
            throw new NotImplementedException();
        }
    }
    [Serializable]
    public class EventOption
    {
        public string optionCaption;
        public byte[] optionImage;
        public bool optionExecutable;
        public int optionReferenceID;
    }

    [Serializable]
    public class EnrollGuestInEventReturn : Default
    {
        public int eventID;
        public int optionReferenceID;
        public string headerCaptionLine1;
        public string headerCaptionLine2;
        public bool isEnrolled;
        public string confirmationNumber;
        public byte[] bodyImage1;
        public byte[] bodyImage2;
        public string footerCaptionLine1;
        public string footerCaptionLine2;

        public void DBEnrollGuestInEvent(string mobile, int eventID, int optionReferenceID)
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public class RequestTicketsToEventReturn : Default
    {
        public int eventID;
        public int optionReferenceID;
        public string headerCaptionLine1;
        public string headerCaptionLine2;
        public bool isEnrolled;
        public string confirmationNumber;
        public int ticketCountAwarded;
        public bool isOnWaitList;
        public string expectedResponseInterval;
        public byte[] bodyImage1;
        public byte[] bodyImage2;
        public string footerCaptionLine1;
        public string footerCaptionLine2;

        public void DBRequestTicketsToEvent(string mobile, int eventID, int optionReferenceID)
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public class PurchaseTicketsToEventWithPointsReturn : Default
    {
        public int eventID;
        public int optionReferenceID;
        public string headerCaptionLine1;
        public string headerCaptionLine2;
        public bool isPurchaseSuccessful;
        public float newPointsBalance;
        public bool isEnrolled;
        public string confirmationNumber;
        public int ticketCountAwarded;
        public bool isOnWaitList;
        public string expectedResponseInterval;
        public byte[] bodyImage1;
        public byte[] bodyImage2;
        public string footerCaptionLine1;
        public string footerCaptionLine2;

        public void DBPurchaseTicketsToEventWithPoints(string mobile, int eventID, int optionReferenceID, int ticketCountRequested)
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}
