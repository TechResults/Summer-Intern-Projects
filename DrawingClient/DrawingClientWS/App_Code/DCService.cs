using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data.SqlTypes;

namespace TotalPromo
{
    [WebService(Namespace = "TotalPromo")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class DCService : System.Web.Services.WebService
    {
        public DCService()
        {
            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        [WebMethod]
        public Drum GetDrumLastHistoryItem(Int32 promoId, Int32 bucketId)
        {
            return BusinessLogic.GetDrumLastHistoryItem(promoId, bucketId);
        }

        [WebMethod]
        public DataSet GetMisc()
        {
            return BusinessLogic.GetMisc();
        }

        [WebMethod]
        public DateTime GetNextDrawingDateTime(Int32 promotionId, Int32 bucketId, DateTime drawingDateTime)
        {
            return DateTime.Parse(BusinessLogic.GetNextDrawingDateTime(promotionId, bucketId, drawingDateTime).ToString());
        }

        [WebMethod]
        public Drawing GetDrawing(Int32 drawingId)
        {
            return BusinessLogic.GetDrawing(drawingId);
        }

        [WebMethod]
        public bool MustBePresent(Int32 promotionId)
        {
            return BusinessLogic.MustBePresent(promotionId);
        }

        [WebMethod]
        public DateTime GetTime()
        {
            return BusinessLogic.GetTime();
        }

        [WebMethod]
        public String GetAnimationURL()
        {
            return ConfigurationManager.AppSettings["AnimationURL"];
        }

        [WebMethod]
        public String GetOnlineHelp()
        {
            return ConfigurationManager.AppSettings["OnlineHelp"];
        }
        [WebMethod]
        public String GetDrawingStatus()
        {
            return BusinessLogic.GetDrawingStatus();
        }

        [WebMethod]
        public Drum GetDrum(Int32 promoId, Int32 bucketId)
        {
            return BusinessLogic.GetDrum(promoId, bucketId);
        }

        [WebMethod]
        public Player RedrawWinner(Int32 promotionId, Int32 bucketId, Int32 drawingId, Int32 userID)
        {
            return BusinessLogic.RedrawWinner(promotionId, bucketId, drawingId, userID);
        }

        [WebMethod]
        public DrawingGroup CompleteDrawing(Int32 promotionId, Int32 bucketId, DateTime drawingDateTime, Int32 userId)
        {
            return BusinessLogic.CompleteDrawing(promotionId, bucketId, drawingDateTime, userId);
        }

        /// <summary>
        /// Validates user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [WebMethod]
        public User ValidateUser(String userId, String password)
        {
            return BusinessLogic.ValidateUser(userId, password);
        }

        [WebMethod]
        public DrawingClientStatus GetDrawingClientInUse()
        {
            return BusinessLogic.GetDrawingClientInUse();
        }

        /// <summary>
        /// Returns version number of Digital Display installed at client site.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public String GetDigitalDisplayVersion()
        {
            return BusinessLogic.DigitalDisplayVersionInstalled();
        }
        /// <summary>
        /// Set flag to indicate that a client is already using DrawingClient
        /// 2009-04-21 JMS: Modified to include useAnimation
        /// </summary>
        /// <param name="drawingClientOpen"></param>
        /// <param name="drawingClientUser"></param>
        /// <param name="useAnimation"></param>
        /// <returns></returns>
        [WebMethod]
        public String SetDrawingClientInUse(String drawingClientOpen, String drawingClientUser)
        {
            return BusinessLogic.SetDrawingClientInUse(drawingClientOpen, drawingClientUser);
        }

        [WebMethod]
        public int SetWinnerPostedByWinnerId(int winnerId)
        {
            return BusinessLogic.SetWinnerPostedByWinnerId(winnerId);
        }

        /// <summary>
        /// Gets all incomplete promotions
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public DataSet GetAllActivePromotions()
        {
            return BusinessLogic.GetAllActivePromotions();
        }

        /// <summary>
        /// Gets details of the promotion selected
        /// </summary>
        /// <param name="promoId"></param>
        /// <returns></returns>
        [WebMethod]
        public DrawingGroup GetDrawingGroup(Int32 promoId)
        {
            return BusinessLogic.GetDrawingGroup(promoId);
        }

        //2009-04-21 JMS: Waiting on next PCS release to add this method back
        //[WebMethod]
        //public String StartSolicitingEGMs(Int32 promotionId)
        //{
        //    return BusinessLogic.StartSolicitingEGMs(promotionId);
        //}

        /// <summary>
        /// Sets flag to indicate countdown has been started
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public String StartCountdown(Int32 promotionId)
        {
            return BusinessLogic.StartCountdown(promotionId);
        }

        /// <summary>
        /// Populates drum
        /// </summary>
        /// <param name="promoId"></param>
        /// <param name="bucketId"></param>
        /// <param name="drawingDateTime"></param>
        /// <param name="currentDate"></param>
        /// <param name="checkInStart"></param>
        /// <param name="checkInEnd"></param>
        /// <returns></returns>
        [WebMethod]
        public String PopulateDrum(Int32 promoId, Int32 bucketId, DateTime drawingDateTime,
                                    DateTime currentDate, DateTime checkInStart, DateTime checkInEnd, Int32 userID)
        {
            return BusinessLogic.PopulateDrum(promoId, bucketId, drawingDateTime,
                                    currentDate, checkInStart, checkInEnd, userID);
        }

        [WebMethod]
        public String StartDrawing(Int32 promoId, Int32 bucketId)
        {
            String errMessage = String.Empty;
            //errMessage = OptimizeDrum(promoId, bucketId);
            return errMessage;
        }

        /// <summary>
        /// Optimizes drum
        /// </summary>
        /// <param name="promoId"></param>
        /// <param name="bucketId"></param>
        /// <returns></returns>
        [WebMethod]
        public String OptimizeDrum(Int32 promoId, Int32 bucketId)
        {
            return BusinessLogic.OptimizeDrum(promoId, bucketId);
        }

        /// <summary>
        /// Sets flag to indicate countdown has been sopped
        /// </summary>
        /// <param name="promoId"></param>
        /// <param name="bucketId"></param>
        /// <param name="drawingdate"></param>
        /// <returns></returns>
        [WebMethod]
        public String StopCountdown(Int32 promotionID, Int32 bucketID, DateTime drawingDate)
        {
            return BusinessLogic.StopCountdown(promotionID, bucketID, drawingDate);
        }

        /// <summary>
        /// Draws winner for the given drawing
        /// </summary>
        /// <param name="drawingId"></param>
        /// <param name="drawingdate"></param>
        /// <returns></returns>
        [WebMethod]
        public Player DrawWinner(Int32 promotionId, Int32 bucketId, Int32 drawingId, Int32 userID, int drawingClientRowNumber)
        {
            return BusinessLogic.DrawWinner(promotionId, bucketId, drawingId, userID, drawingClientRowNumber);
        }

        [WebMethod]
        public void DrawAllWinners(Int32 promotionId, Int32 bucketId, Int32 userID)
        {
            BusinessLogic.DrawAllWinners(promotionId, bucketId, userID);
        }

        /// <summary>
        /// Get Possible Count
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetPossibleCount(Int32 promoId, Int32 bucketId, DateTime drawingDate)
        {
            String errMessage = String.Empty;
            if (0 == BusinessLogic.GetPossibleCount(promoId, bucketId, drawingDate))
                errMessage = "There are no possible winners in the drum. Perhaps you forgot to populate?";
            return errMessage;
        }

        [WebMethod]
        public void DisqualifyWinner(Int32 promotionId, Int32 bucketId, Int32 drawingId, string playerAccountNum, string cmsPlayerId, Int32 ticketNumber)
        {
            BusinessLogic.DisqualifyWinner(promotionId, bucketId, drawingId, playerAccountNum, cmsPlayerId, ticketNumber);
        }
        /// <summary>
        /// Draws winners for all drawing under a promo
        /// </summary>
        /// <param name="promoId"></param>
        /// <param name="drawingId"></param>
        /// <param name="drawingdate"></param>
        /// <returns></returns>
        //[WebMethod]
        //public List<Drawing> DrawAllWinners(Int32 promoId, DateTime drawingdate)
        //{
        //    return BusinessLogic.DrawAllWinners(promoId, drawingdate);
        //}

        /// <summary>
        /// Resets all drawing under the given promotion
        /// </summary>
        /// <param name="promoId"></param>
        /// <returns></returns>
        [WebMethod]
        public String ResetDrawing(Int32 promoId, Int32 bucketId, DateTime drawingDateTime)
        {
            return BusinessLogic.ResetDrawing(promoId, bucketId, drawingDateTime);
        }

        /// <summary>
        /// Gets drum history for the given promo/bucket combination
        /// </summary>
        /// <param name="promoId"></param>
        /// <param name="bucketId"></param>
        /// <returns></returns>
        [WebMethod]
        public DataSet GetDrumHistory(Int32 promoId, Int32 bucketId)
        {
            return BusinessLogic.GetDrumHistory(promoId, bucketId);
        }

        /// <summary>
        /// Not using - just to get the Promo thru proxy
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public Promo GetPromoProxy()
        {
            return new Promo();
        }

        /// <summary>
        /// Not using - just to get the Drum thru proxy
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public Drum GetDrumProxy()
        {
            return new Drum();
        }

        [WebMethod]
        public void Validate(Int32 promoId, Int32 bucketId, Int32 drawingId, String playerAccountNum, String cmsPlayerId, Int32 ticketNumber, Int32 userId)
        {
            BusinessLogic.Validate(promoId, bucketId, drawingId, playerAccountNum, cmsPlayerId, ticketNumber, userId);
        }
    }
}
