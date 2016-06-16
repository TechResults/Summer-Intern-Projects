﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using PE.DataReturn;

namespace PlayerElite
{
    [WebService(Namespace = "playerelite.com.au")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class PEService : System.Web.Services.WebService
    {

        public class Constants
        {
            //WebMethodIDS:
        }

        int registrationTries;
        public PEService()
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
                string storedPin = newUser.DBGetStoredPin(mobile);
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
            return new JavaScriptSerializer().Serialize(newUser);
        }
        #endregion

        #region Application Launch
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ValidatePhoneRegistered(string mobile)
        {
            ValidatePhoneRegisteredResult currentUser = new ValidatePhoneRegisteredResult();
            if (currentUser.checkIsRegistered(mobile))
            {
                currentUser.GenerateOneWayHash();
                currentUser.isRegistered = true;
                //Show balances based on configuration (Config is not elaborated by API DOC)
                currentUser.showBalancesNoPin = true;
            }
            else
            {
                currentUser.isRegistered = false;
            }
            return new JavaScriptSerializer().Serialize(currentUser);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ShowBalancesOnOpeningScreen(string mobile, string userToken)
        {
            ShowBalancesOnOpeningScreenResult currentUser = new ShowBalancesOnOpeningScreenResult();

            currentUser.DBGetAccountBalancesSet(mobile);

            return new JavaScriptSerializer().Serialize(currentUser);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ValidateUser(string mobile, string pinNum)
        {
            ValidateUserReturn currentUser = new ValidateUserReturn();
            string storedPIN = currentUser.DBGetStoredPin(mobile);
            if (storedPIN == pinNum)
            {
                currentUser.isValid = true;
                currentUser.GenerateOneWayHash();
            }
            else
            {
                currentUser.isValid = false;
                currentUser.userToken = "";
            }

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
                currentUser.DBGetLogo();
            }
            else
            {
                currentUser.logo = null;
            }
            return new JavaScriptSerializer().Serialize(currentUser);

        }
        #endregion

        #region Overview Screen
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

        #region My Games Screen
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetGamesScreenWrapper(string mobile, string userToken)
        {
            string ipAddress = HttpContext.Current.Request.UserHostAddress;
            GetGamesScreenWrapperReturn currentUser = new GetGamesScreenWrapperReturn();
            currentUser.checkSession(mobile, userToken);

            if (currentUser.validToken)
            {
                currentUser.DBGetGamesScreenWrapper(mobile, ipAddress);
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
            string ipAddress = HttpContext.Current.Request.UserHostAddress;
            GetIntervalsAndBackgroundsReturn currentUser = new GetIntervalsAndBackgroundsReturn();
            currentUser.checkSession(mobile, userToken);
            if (currentUser.validToken)
            {
                currentUser.DBGetIntervalsAndBackgrounds(mobile, gameID, variantID, ipAddress);
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
            string ipAddress = HttpContext.Current.Request.UserHostAddress;
            GetPageAttributesReturn currentUser = new GetPageAttributesReturn();
            currentUser.checkSession(mobile, userToken);
            if (currentUser.validToken)
            {
                currentUser.DBGetPageAttributes(mobile, pageName, gameID, ipAddress);
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
        public string StartGame(string mobile, string userToken, int gameID)
        {
            string ipAddress = HttpContext.Current.Request.UserHostAddress;
            StartGameReturn currentUser = new StartGameReturn();
            currentUser.checkSession(mobile, userToken);
            if (currentUser.validToken)
            {
                currentUser.DBStartGame(gameID, ipAddress);
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
                currentUser.DBSaveWinInfo(mobile, userToken, gameID, gameToken, objectsSelected);
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
                currentUser.DBSaveLoseInfo(mobile, userToken, gameID, gameToken, objectsSelected);
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

        #region My Events Screen
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetEventsScreenWallpaper(string mobile, string userToken)
        {
            GetEventsScreenWrapperReturn currentUser = new GetEventsScreenWrapperReturn();
            currentUser.checkSession(mobile, userToken);
            if (currentUser.validToken)
            {
                currentUser.DBGetEventsScreenWrapper(mobile);
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
        public string GetEventDetails(string mobile, string userToken, int eventID)
        {
            GetEventDetailsReturn currentUser = new GetEventDetailsReturn();
            currentUser.checkSession(mobile, userToken);
            if (currentUser.validToken)
            {
                currentUser.DBGetEventDetails(mobile, eventID);
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
        public string EnrollGuestInEvent(string mobile, string userToken, int eventID, int optionReferenceID)
        {
            EnrollGuestInEventReturn currentUser = new EnrollGuestInEventReturn();
            currentUser.checkSession(mobile, userToken);
            if (currentUser.validToken)
            {
                currentUser.DBEnrollGuestInEvent(mobile, eventID, optionReferenceID);
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
        public string RequestTicketsToEvent(string mobile, string userToken, int eventID, int optionReferenceID)
        {
            RequestTicketsToEventReturn currentUser = new RequestTicketsToEventReturn();
            currentUser.checkSession(mobile, userToken);
            if (currentUser.validToken)
            {
                currentUser.DBRequestTicketsToEvent(mobile, eventID, optionReferenceID);
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
        public string PurchaseTicketsToEventWithPoints(string mobile, string userToken, int eventID, int optionReferenceID, int ticketCountRequested)
        {
            PurchaseTicketsToEventWithPointsReturn currentUser = new PurchaseTicketsToEventWithPointsReturn();
            currentUser.checkSession(mobile, userToken);
            if (currentUser.validToken)
            {
                currentUser.DBPurchaseTicketsToEventWithPoints(mobile, eventID, optionReferenceID, ticketCountRequested);
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
