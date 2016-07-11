using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using RNG;
using System.Data.SqlTypes;

namespace TotalPromo
{
    /// <summary>
    /// Summary description for BusinessLogic
    /// </summary>
    public class BusinessLogic
    {
        public BusinessLogic()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static DataSet GetMisc()
        {
            return DataAccess.ExecuteQuerySP("GET_tblmisc2", null);
        }

        public static DateTime GetTime()
        {
            DataTable dt = DataAccess.ExecuteQuerySP("DRAW_GET_TIMEATSERVER", null).Tables[0];
            return DateTime.Parse(dt.Rows[0][0].ToString());
        }

        public static String GetDrawingStatus()
        {
            // PROMOTION_DRAWING_EVENT_MarkComplete
            DataTable dt = DataAccess.ExecuteQuerySP("DRAW_GET_STATUS", null).Tables[0];
            return dt.Rows[0][0].ToString();
        }

        public static bool MustBePresent(Int32 promotionId)
        {
            try
            {
                bool mustBePresent = false;
                // PROMOTION_PLAYER_ELIGIBILITY_RULES_GetByKey
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@promotionID", promotionId));
                DataSet ds = DataAccess.ExecuteQuerySP("PROMOTION_PLAYER_ELIGIBILITY_RULES_GetByKey", spParams);
                if (ds.Tables.Count > 0)
                {
                    mustBePresent = bool.Parse(ds.Tables[0].Rows[0]["WinnerMustBePresent"].ToString());
                }
                return mustBePresent;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public static Drum GetDrum(Int32 promoId, Int32 bucketId)
        {
            Drum drum = new Drum();
            return drum.LoadSelected(promoId, bucketId);
        }

        public static Drum GetDrumLastHistoryItem(Int32 promoId, Int32 bucketId)
        {
            Drum drum = new Drum();
            try
            {
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@PromotionId", promoId));
                spParams.Add(new SqlParameter("@BucketId", bucketId));
                result = DataAccess.ExecuteQuerySP("PROMOTION_DRUM_GetDrumLastHistoryItem", spParams);
                if (result.Tables.Count > 0)
                {
                    drum.NumberOfPlayers = int.Parse(result.Tables[0].Rows[0]["NumberOfPlayers"].ToString());
                    drum.NumberOfEntries = int.Parse(result.Tables[0].Rows[0]["NumberOfEntries"].ToString());
                    drum.LastPopulated = DateTime.Parse(result.Tables[0].Rows[0]["DrumTimeStamp"].ToString());
                }
            }
            catch { }
            return drum;
        }

        public static DateTime GetNextDrawingDateTime(Int32 promotionId, Int32 bucketId, DateTime drawingDateTime)
        {
            DateTime nextDateTime = new DateTime();
            try
            {
                // PROMOTION_DRAWING_EVENT_GetNextAvailable
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@promoid", promotionId));
                spParams.Add(new SqlParameter("@bucketid", bucketId));
                spParams.Add(new SqlParameter("@drawingdate", drawingDateTime));
                result = DataAccess.ExecuteQuerySP("PROMOTION_DRAWING_EVENT_GetNextAvailable", spParams);
                if (result.Tables.Count > 0)
                {
                    if (result.Tables[0].Rows.Count > 0)
                        nextDateTime = DateTime.Parse(result.Tables[0].Rows[0]["DrawingDateTime"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return nextDateTime;
        }

        public static DrawingGroup CompleteDrawing(Int32 promotionId, Int32 bucketId, DateTime drawingDateTime, Int32 userId)
        {
            try
            {
                // DRAW_RESET_CHECK
                DataAccess.ExecuteNonQuerySP("DRAW_RESET_CHECK", null);
                // DRAW_CLOSE_MASSANIMATION
                DataAccess.ExecuteNonQuerySP("DRAW_CLOSE_MASSANIMATION", null);

                // PROMOTION_DRAWING_EVENT_MarkComplete
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@promotionID", promotionId));
                spParams.Add(new SqlParameter("@bucketID", bucketId));
                spParams.Add(new SqlParameter("@drawingDateTime", drawingDateTime));
                spParams.Add(new SqlParameter("@userID", userId));
                DataAccess.ExecuteNonQuerySP("PROMOTION_DRAWING_EVENT_MarkComplete", spParams);

                // PROMOTION_DRAWING_EVENT_RecordPermanentWinners
                spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@promoid", promotionId));
                spParams.Add(new SqlParameter("@bucketid", bucketId));
                spParams.Add(new SqlParameter("@drawingdate", drawingDateTime));
                DataAccess.ExecuteNonQuerySP("PROMOTION_DRAWING_EVENT_RecordPermanentWinners", spParams);

                // PROMOTION_DRUM_Clear
                spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@promotionID", promotionId));
                spParams.Add(new SqlParameter("@bucketID", bucketId));
                DataAccess.ExecuteNonQuerySP("PROMOTION_DRUM_Clear", spParams);

                // PROMOTION_DRAWING_EVENT_GetNextAvailable
                DataSet result = new DataSet();
                spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@promoid", promotionId));
                spParams.Add(new SqlParameter("@bucketid", bucketId));
                spParams.Add(new SqlParameter("@drawingdate", drawingDateTime));
                result = DataAccess.ExecuteQuerySP("PROMOTION_DRAWING_EVENT_GetNextAvailable", spParams);
                if (result.Tables[0].Rows.Count == 0)
                {
                    // PROMOTION_DRAWING_ListByEvent
                    DataSet results = new DataSet();
                    spParams = new List<SqlParameter>();
                    spParams.Add(new SqlParameter("@promoid", promotionId));
                    spParams.Add(new SqlParameter("@bucketid", bucketId));
                    spParams.Add(new SqlParameter("@drawingdatetime", drawingDateTime));
                    results = DataAccess.ExecuteQuerySP("PROMOTION_DRAWING_ListByEvent_v2", spParams);

                    // PROMOTION_DRAWING_EVENT_GetNextAvailableForPromo
                    DataSet result2 = new DataSet();
                    spParams = new List<SqlParameter>();
                    spParams.Add(new SqlParameter("@promoid", promotionId));
                    result2 = DataAccess.ExecuteQuerySP("PROMOTION_DRAWING_EVENT_GetNextAvailableForPromo", spParams);

                    // PROMOTION_DRAWING_GetNextForEvent
                    DataSet result3 = new DataSet();
                    spParams = new List<SqlParameter>();
                    spParams.Add(new SqlParameter("@promoid", promotionId));
                    spParams.Add(new SqlParameter("@bucketid", bucketId));
                    spParams.Add(new SqlParameter("@drawingdatetime", drawingDateTime));
                    result3 = DataAccess.ExecuteQuerySP("PROMOTION_DRAWING_GetNextForEvent", spParams);

                    // PROMOTION_DRUM_GetByKey
                    DataSet result4 = new DataSet();
                    spParams = new List<SqlParameter>();
                    spParams.Add(new SqlParameter("@promoid", promotionId));
                    spParams.Add(new SqlParameter("@bucketid", bucketId));
                    result4 = DataAccess.ExecuteQuerySP("PROMOTION_DRUM_GetByKey", spParams);

                    // PROMOTION_DRUM_GetEntryCount
                    DataSet result5 = new DataSet();
                    spParams = new List<SqlParameter>();
                    spParams.Add(new SqlParameter("@promoid", promotionId));
                    spParams.Add(new SqlParameter("@bucketid", bucketId));
                    result5 = DataAccess.ExecuteQuerySP("PROMOTION_DRUM_GetEntryCount", spParams);

                    // PROMOTION_DRUM_GetPlayerCount
                    DataSet result6 = new DataSet();
                    spParams = new List<SqlParameter>();
                    spParams.Add(new SqlParameter("@promoid", promotionId));
                    spParams.Add(new SqlParameter("@bucketid", bucketId));
                    result6 = DataAccess.ExecuteQuerySP("PROMOTION_DRUM_GetPlayerCount", spParams);

                    // PROMOTION_DRAWING_ListByEvent
                    DataSet result7 = new DataSet();
                    spParams = new List<SqlParameter>();
                    spParams.Add(new SqlParameter("@promoid", promotionId));
                    spParams.Add(new SqlParameter("@bucketid", bucketId));
                    spParams.Add(new SqlParameter("@drawingdatetime", drawingDateTime));
                    result7 = DataAccess.ExecuteQuerySP("PROMOTION_DRAWING_ListByEvent", spParams);

                    Int32 drawingId = Convert.ToInt32(result3.Tables[0].Rows[0]["DrawingID"].ToString());
                    // PROMOTION_DRAWING_GetCurrentWinner
                    DataSet result8 = new DataSet();
                    spParams = new List<SqlParameter>();
                    spParams.Add(new SqlParameter("@promoid", promotionId));
                    spParams.Add(new SqlParameter("@bucketid", bucketId));
                    spParams.Add(new SqlParameter("@drawingid", drawingId));
                    result8 = DataAccess.ExecuteQuerySP("PROMOTION_DRAWING_GetCurrentWinner", spParams);

                    // PROMOTION_DRAWING_GetNextForEvent
                    DataSet result9 = new DataSet();
                    spParams = new List<SqlParameter>();
                    spParams.Add(new SqlParameter("@promoid", promotionId));
                    spParams.Add(new SqlParameter("@bucketid", bucketId));
                    spParams.Add(new SqlParameter("@drawingdatetime", drawingDateTime));
                    result9 = DataAccess.ExecuteQuerySP("PROMOTION_DRAWING_GetNextForEvent", spParams);
                    return null;
                }
                else
                {
                    return BusinessLogic.GetDrawingGroup(promotionId);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Validates User
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static User ValidateUser(String userId, String password)
        {
            try
            {
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@userIDText", userId));
                spParams.Add(new SqlParameter("@userPass", password));
                DataSet result = DataAccess.ExecuteQuerySP("USER_LOGIN", spParams);
                if (result.Tables[0].Rows.Count > 0)
                {
                    User user = new User();
                    user.UserID = Int32.Parse(result.Tables[0].Rows[0]["UserID"].ToString());
                    user.UserIDText = result.Tables[0].Rows[0]["UserIdText"].ToString();
                    user.UserLevel = Int32.Parse(result.Tables[0].Rows[0]["UserLevel"].ToString());
                    /*
                     * MR 12/21/09 Adding a check for a LoginStatus return value to show a message when
                     * the user account is locked
                     * 
                     * 
                     * */
                    if (result.Tables[0].Rows[0]["LoginStatus"] != null)
                    {
                        user.LoginStatus = result.Tables[0].Rows[0]["LoginStatus"].ToString();
                    }
                    else
                    {
                        user.LoginStatus = "";
                    }
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DrawingClientStatus GetDrawingClientInUse()
        {
            try
            {
                DataSet result = DataAccess.ExecuteQuerySP("DRAW_GetDrawingClientInUse", null);
                DrawingClientStatus status = new DrawingClientStatus();
                status.DrawingClientInUse = result.Tables[0].Rows[0]["DrawingClientOpen"].ToString() == "DrawingClientOpen" ? true : false;
                status.DrawingClientInUseBy = result.Tables[0].Rows[0]["DrawingClientUser"].ToString() != String.Empty ? result.Tables[0].Rows[0]["DrawingClientUser"].ToString() : String.Empty;
                return status;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Set flag to indicate that a client is already using DrawingClient
        /// </summary>
        /// <param name="drawingClientOpen"></param>
        /// <param name="drawingClientUser"></param>
        /// <returns></returns>
        public static String DigitalDisplayVersionInstalled()
        {
            String versionNumber = String.Empty;
            try
            {
                DataSet result = DataAccess.ExecuteQuerySP("S3_CheckForDigitalDisplayVersion", null);
                

                if (result.Tables[0].Rows.Count > 0)
                {
                    versionNumber = (result.Tables[0].Rows[0]["value"].ToString());
                }
                else
                {
                    versionNumber = "0.0.0.0";
                }
            }
            catch (SqlException ex)
            {
                versionNumber = "0.0.0.0";
            }
            return versionNumber;
        }

        /// <summary>
        /// Set flag to indicate that a client is already using DrawingClient
        /// </summary>
        /// <param name="drawingClientOpen"></param>
        /// <param name="drawingClientUser"></param>
        /// <returns></returns>
        public static String SetDrawingClientInUse(String drawingClientOpen, String drawingClientUser)
        {
            String errMessage = String.Empty;
            try
            {
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@DrawingClientOpen", drawingClientOpen));
                spParams.Add(new SqlParameter("@DrawingClientUser", drawingClientUser));
                //spParams.Add(new SqlParameter("@AnimInUse", useAnimation));
                Int32 result = DataAccess.ExecuteNonQuerySP("DRAW_SetDrawingClientInUse", spParams);
            }
            catch (SqlException ex)
            {
                errMessage = ex.Message;
            }
            return errMessage;
        }

        public static int SetWinnerPostedByWinnerId(int winnerId)
        {
            try
            {
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@WinnerId", winnerId));
                return DataAccess.ExecuteNonQuerySP("DRAW_UPDATE_DRAWINGWINNERS_MAINAV", spParams);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets all active (incomplete) promotions
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAllActivePromotions()
        {
            DataSet ds = new DataSet();
            try
            {
                Promo promo = new Promo();
                ds = promo.GetAllActivePromotions();
                DataRow row = ds.Tables[0].NewRow();
                row["PromotionId"] = 0;
                row["PromotionName"] = String.Empty;
                ds.Tables[0].Rows.InsertAt(row, 0);
            }
            catch (SqlException ex)
            {
                String errMessage = ex.Message;
            }
            return ds;
        }

        /// <summary>
        /// Gets details of the promotion selected
        /// </summary>
        /// <param name="promoId"></param>
        /// <returns></returns>
        public static DrawingGroup GetDrawingGroup(Int32 promoId)
        {
            DrawingGroup drawingGroup = new DrawingGroup();
            drawingGroup = drawingGroup.LoadSelected(promoId);
            // DRAW_UPDATE_MASSDRAWSPECS
            List<SqlParameter> spParams = new List<SqlParameter>();
            spParams.Add(new SqlParameter("@promoID", drawingGroup.PromoId));
            spParams.Add(new SqlParameter("@drawingDate", drawingGroup.DrawDate));
            spParams.Add(new SqlParameter("@bucketCode", drawingGroup.BucketId));
            spParams.Add(new SqlParameter("@currentDrawTime", drawingGroup.DrawDate));
            spParams.Add(new SqlParameter("@nextDrawTime", drawingGroup.NextDrawDate));
            DataAccess.ExecuteNonQuerySP("DRAW_UPDATE_MASSDRAWSPECS", spParams);
            return drawingGroup;
        }

        public static Drawing GetDrawing(Int32 drawingId)
        {
            try
            {
                // DRAW_UPDATE_MASSDRAWSPECS
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@DrawingID", drawingId));
                DataRow row = DataAccess.ExecuteQuerySP("PROMOTION_DRAWING_GetDrawing", spParams).Tables[0].Rows[0];
                Drawing drawing = new Drawing();
                drawing.PromoId = Int32.Parse(row["PromotionID"].ToString());
                drawing.BucketId = Int32.Parse(row["BucketID"].ToString());
                drawing.PrizeId = Int32.Parse(row["PrizeID"].ToString());
                drawing.Prize = row["Prize"].ToString();
                drawing.PrizeValue = decimal.Parse(row["PrizeValue"].ToString());
                drawing.WinnerId = Int32.Parse(row["WinnerID"].ToString());
                if (row["Timeout"].ToString() != String.Empty)
                    drawing.TimeOut = DateTime.Parse(row["Timeout"].ToString());
                drawing.Winner = row["PlayerName"].ToString();
                if (row["PlayerDOB"].ToString() != String.Empty)
                    drawing.WinnerDob = DateTime.Parse(row["PlayerDOB"].ToString());
                drawing.CMSPlayerID = row["CMSPlayerID"].ToString();
                drawing.Validated = bool.Parse(row["Validated"].ToString());
                drawing.PlayerAccountNum = row["PlayerAccountNum"].ToString();
                if (row["TimeStarted"].ToString() != String.Empty)
                    drawing.TimeStarted = DateTime.Parse(row["TimeStarted"].ToString());
                drawing.AfterCountdown = row["AfterCountdown"].ToString();
                //Drawing drawing = new Drawing(drawingId,
                //    Int32.Parse(row["PromotionID"].ToString()),
                //    Int32.Parse(row["BucketID"].ToString()),
                //    Int32.Parse(row["PrizeID"].ToString()),
                //    row["Prize"].ToString(),
                //    decimal.Parse(row["PrizeValue"].ToString()),
                //    Int32.Parse(row["WinnerID"].ToString()),
                //    SqlDateTime.Parse(row["Timeout"].ToString()),
                //    row["PlayerName"].ToString(),
                //    SqlDateTime.Parse(row["PlayerDOB"].ToString()),
                //    row["CMSPlayerID"].ToString(),
                //    bool.Parse(row["Validated"].ToString()),
                //    row["PlayerAccountNum"].ToString(),
                //    SqlDateTime.Parse(row["TimeStarted"].ToString()));
                return drawing;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Sets flag to indicate patron solicitation has been started
        /// </summary>
        /// <returns></returns>
        public static String StartSolicitingEGMs(Int32 promoId)
        {
            String errMessage = String.Empty;
            try
            {
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@promoid", promoId));

                // [S3_DRAW_SET_SOLICIT_IN_PROGRESS]
                DataAccess.ExecuteNonQuerySP("S3_DRAW_SET_SOLICIT_IN_PROGRESS", spParams);
            }
            catch (SqlException ex)
            {
                errMessage = ex.Message;
            }
            return errMessage;
        }

        /// <summary>
        /// Sets flag to indicate countdown has been started
        /// </summary>
        /// <returns></returns>
        public static String StartCountdown(Int32 promoId)
        {
            String errMessage = String.Empty;
            try
            {
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@promoid", promoId));
                DataSet result = DataAccess.ExecuteQuerySP("PROMOTION_DRAWING_EVENT_GetNextAvailableForPromo", spParams);

                // DRAW_UPDATE_MASSDRAWSPECS
                spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@promoID", promoId));
                spParams.Add(new SqlParameter("@drawingDate", DateTime.Parse(result.Tables[0].Rows[0]["DrawingDateTime"].ToString())));
                spParams.Add(new SqlParameter("@bucketCode", result.Tables[0].Rows[0]["BucketCode"].ToString()));
                spParams.Add(new SqlParameter("@currentDrawTime", DateTime.Parse(result.Tables[0].Rows[0]["DrawingDateTime"].ToString())));
                spParams.Add(new SqlParameter("@nextDrawTime", DateTime.Parse(result.Tables[0].Rows[0]["DrawingDateTime"].ToString())));
                DataAccess.ExecuteNonQuerySP("DRAW_UPDATE_MASSDRAWSPECS", spParams);

                // DRAW_SET_ANIMLOAD
                DataAccess.ExecuteNonQuerySP("DRAW_SET_ANIMLOAD", null);

                // DRAW_SET_COUNTDOWN_START
                DataAccess.ExecuteNonQuerySP("DRAW_SET_COUNTDOWN_START", null);
            }
            catch (SqlException ex)
            {
                errMessage = ex.Message;
            }
            return errMessage;
        }

        /// <summary>
        /// Populate drum
        /// </summary>
        /// <param name="promoId"></param>
        /// <param name="bucketId"></param>
        /// <param name="drawingDateTime"></param>
        /// <param name="currentDate"></param>
        /// <param name="checkInStart"></param>
        /// <param name="checkInEnd"></param>
        /// <returns></returns>
        public static String PopulateDrum(Int32 promoId, Int32 bucketId, DateTime drawingDateTime,
                                    DateTime currentDate, DateTime checkInStart, DateTime checkInEnd, Int32 userID)
        {
            String errMessage = String.Empty;
            try
            {
                //// DRAW_SET_COUNTDOWN_START
                //DataAccess.ExecuteNonQuerySP("DRAW_SET_COUNTDOWN_START", null);

                // TR_SET_DRAWING_IN_PROGRESS
                DataAccess.ExecuteNonQuerySP("TR_SET_DRAWING_IN_PROGRESS", null);

                // PROMOTION_POINTS_PARTICIPATION_RULES_GetByKey
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@PromotionID", promoId));
                DataSet results1 = DataAccess.ExecuteQuerySP("PROMOTION_POINTS_PARTICIPATION_RULES_GetByKey", spParams);

                if (results1.Tables.Count > 0)
                {
                    //string checkInRequired = results1.Tables[0].Rows[0]["CheckInReqd"].ToString();
                    //string implicitRequired = results1.Tables[0].Rows[0]["ImplicitCheckinSupported"].ToString();
                    // If CheckIn Is Required?
                    if (results1.Tables[0].Rows[0]["CheckInReqd"].ToString() == "True")
                    {
                        // If Implicit CheckIn Is Supported?
                        if (results1.Tables[0].Rows[0]["ImplicitCheckinSupported"].ToString() == "True")
                        {
                            // PROMOTION_DRAWING_EVENT_ImplicitCheckIn
                            spParams = new List<SqlParameter>();
                            spParams.Add(new SqlParameter("@PromotionID", promoId));
                            spParams.Add(new SqlParameter("@BucketID", bucketId));
                            spParams.Add(new SqlParameter("@DrawingDateTime", drawingDateTime));
                            spParams.Add(new SqlParameter("@CurrentDate", currentDate));
                            spParams.Add(new SqlParameter("@CheckInStart", checkInStart));
                            spParams.Add(new SqlParameter("@CheckInEnd", checkInEnd));
                            DataAccess.ExecuteNonQuerySP("PROMOTION_DRAWING_EVENT_ImplicitCheckIn", spParams);
                        }
                    }
                    // PROMOTION_DRUM_GetPopulationEntries
                    // Iterate through this to create standard entries.
                    spParams = new List<SqlParameter>();
                    spParams.Add(new SqlParameter("@promotionID", promoId));
                    spParams.Add(new SqlParameter("@bucketID", bucketId));
                    DataSet results2 = DataAccess.ExecuteQuerySP("PROMOTION_DRUM_GetPopulationEntries", spParams);

                    // PROMOTION_DRUM_ReturnEligibleCount
                    spParams = new List<SqlParameter>();
                    spParams.Add(new SqlParameter("@promotionID", promoId));
                    spParams.Add(new SqlParameter("@bucketID", bucketId));
                    // EligibleCount
                    DataSet result3 = DataAccess.ExecuteQuerySP("PROMOTION_DRUM_ReturnEligibleCount", spParams);
                    Int32 totalPlayers = Int32.Parse(result3.Tables[0].Rows[0][0].ToString());

                    // DRAW_SET_POPULATION_VARIABLES
                    spParams = new List<SqlParameter>();
                    spParams.Add(new SqlParameter("@totalPlayers", totalPlayers));
                    DataAccess.ExecuteNonQuerySP("DRAW_SET_POPULATION_VARIABLES", spParams);

                    // TR_SET_DRAWING_IN_PROGRESS
                    DataAccess.ExecuteNonQuerySP("TR_SET_DRAWING_IN_PROGRESS", null);


                    // For Each Row In results2 (PROMOTION_DRUM_GetPopulationEntries) Populate standard entry & call DRAW_SET_CURRENT_POPULATED ++ 1
                    foreach (DataRow row in results2.Tables[0].Rows)
                    {
                        // PROMOTION_DRUM_PopulateStandardEntry
                        spParams = new List<SqlParameter>();
                        spParams.Add(new SqlParameter("@promoID", promoId));
                        spParams.Add(new SqlParameter("@BucketID", bucketId));
                        spParams.Add(new SqlParameter("@PlayerAccountNum", row["PlayerAccountNum"].ToString()));
                        spParams.Add(new SqlParameter("@CMSPlayerID", row["CMSPlayerID"].ToString()));
                        spParams.Add(new SqlParameter("@Entries", row["Entries"].ToString()));
                        spParams.Add(new SqlParameter("@userID", userID));
                        DataAccess.ExecuteNonQuerySP("PROMOTION_DRUM_PopulateStandardEntry", spParams);

                        // exec DRAW_SET_CURRENT_POPULATED
                        spParams = new List<SqlParameter>();
                        spParams.Add(new SqlParameter("@populatedPlayers", promoId));
                        DataAccess.ExecuteNonQuerySP("DRAW_SET_CURRENT_POPULATED", spParams);
                    }

                    // DRAW_RESET_TEMPDRUM
                    DataAccess.ExecuteNonQuerySP("DRAW_RESET_TEMPDRUM", null);

                    // DRAW_SET_POPULATION_COMPLETE
                    DataAccess.ExecuteNonQuerySP("DRAW_SET_POPULATION_COMPLETE", null);

                    // TR_CLEAR_DRAWING_IN_PROGRESS
                    DataAccess.ExecuteNonQuerySP("TR_CLEAR_DRAWING_IN_PROGRESS", null);


                    DrawingGroup group = BusinessLogic.GetDrawingGroup(promoId);

                    // PROMOTION_DRUM_InsertDrumHistory
                    //spParams = new List<SqlParameter>();
                    //spParams.Add(new SqlParameter("@PromotionID", promoId));
                    //spParams.Add(new SqlParameter("@BucketID", bucketId));
                    //spParams.Add(new SqlParameter("@DrumTimeStamp", group.Drum.LastPopulated));
                    //spParams.Add(new SqlParameter("@NumberOfEntries", group.Drum.NumberOfEntries));
                    //spParams.Add(new SqlParameter("@NumberOfPlayers", group.Drum.NumberOfPlayers));
                    //DataAccess.ExecuteNonQuerySP("PROMOTION_DRUM_InsertDrumHistory", spParams);
                }

            }
            catch (SqlException ex)
            {
                errMessage = ex.Message;
            }
            return errMessage;
        }

        /// <summary>
        /// Optimize drum
        /// </summary>
        /// <param name="promoId"></param>
        /// <param name="bucketId"></param>
        /// <returns></returns>
        public static String OptimizeDrum(Int32 promoId, Int32 bucketId)
        {
            String errMessage = String.Empty;
            try
            {
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@PromotionID", promoId));
                spParams.Add(new SqlParameter("@BucketID", bucketId));
                DataRow drEntries = DataAccess.GetDataRow("PROMOTION_DRUM_GetEntriesExist", spParams);
                if (null != drEntries)
                {
                    Int32 entries = drEntries.IsNull("Entries") ? 0 : Convert.ToInt32(drEntries["Entries"]);
                    if (entries > 0)
                    {
                        Int32 result = 0;
                        result = DataAccess.ExecuteNonQuerySP("DRAW_RESET_TEMPDRUM", null);
                        spParams = new List<SqlParameter>();
                        spParams.Add(new SqlParameter("@promoid", promoId));
                        spParams.Add(new SqlParameter("@bucketid", bucketId));
                        result = DataAccess.ExecuteNonQuerySP("PROMOTION_DRUM_PopulateTempDrum", spParams);
                    }
                }
            }
            catch (SqlException ex)
            {
                errMessage = ex.Message;
            }
            return errMessage;
        }

        /// <summary>
        /// Gets possible winners in the drum
        /// </summary>
        /// <param name="promoId"></param>
        /// <param name="bucketId"></param>
        /// <param name="drawingdate"></param>
        /// <returns></returns>
        public static Int32 GetPossibleCount(Int32 promoId, Int32 bucketId, DateTime drawingdate)
        {
            Int32 possibleCount = 0;
            try
            {

            }
            catch (SqlException ex)
            {
                String errMessage = ex.Message;
            }
            return possibleCount;
        }

        /// <summary>
        /// Stops countdown and sets ready to draw flag
        /// </summary>
        /// <returns></returns>
        public static String StopCountdown(Int32 promotionID, Int32 bucketID, DateTime drawingDate)
        {
            String errMessage = String.Empty;
            try
            {
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@promotionID", promotionID));
                spParams.Add(new SqlParameter("@bucketID", bucketID));
                spParams.Add(new SqlParameter("@drawingdate", drawingDate));

                DataAccess.ExecuteNonQuerySP("PROMOTION_DRAWING_EVENT_GetPossibleCount", spParams);

                DataAccess.ExecuteNonQuerySP("DRAW_SET_STARTDRAWINGS", null);
            }
            catch (SqlException ex)
            {
                errMessage = ex.Message;
            }
            return errMessage;
        }

        /// <summary>
        /// Draws winner for the given drawing
        /// </summary>
        /// <param name="drawingId"></param>
        /// <param name="drawingdate"></param>
        /// <returns></returns>
        public static Player DrawWinner(Int32 promotionId, Int32 bucketId, Int32 drawingId, Int32 userID, int drawingClientRowNumber)
        {
            Player winner = null;
            try
            {
                // PROMOTION_DRAWING_ReturnPossibleCount
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@promotionID", promotionId));
                spParams.Add(new SqlParameter("@bucketID", bucketId));
                spParams.Add(new SqlParameter("@DrawingID", drawingId));
                DataSet result = DataAccess.ExecuteQuerySP("PROMOTION_DRAWING_ReturnPossibleCount", spParams);
                Int32 entryCount = Int32.Parse(result.Tables[0].Rows[0]["EntryCount"].ToString());
                TRRandom rng = new TRRandom();
                Int32 winCount = rng.GenerateRandomInt(entryCount);
                // PROMOTION_DRAWING_DrawEntryUsingTemp
                spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@promotionID", promotionId));
                spParams.Add(new SqlParameter("@bucketID", bucketId));
                spParams.Add(new SqlParameter("@drawingID", drawingId));
                spParams.Add(new SqlParameter("@winCount", winCount));
                spParams.Add(new SqlParameter("@userID", userID));
                spParams.Add(new SqlParameter("@drawingClientRowNumber", drawingClientRowNumber));
                DataSet result2 = DataAccess.ExecuteQuerySP("PROMOTION_DRAWING_DrawEntryUsingTemp", spParams);
                //// PROMOTION_DRUM_ENTRY_GetByKey
                String playeraccountnum = result2.Tables[0].Rows[0]["PlayerAccountNum"].ToString();

                //Return null if we did not get back a player (could be db problem or no more possible winners)
                if (string.IsNullOrEmpty(playeraccountnum))
                {
                    return null;
                }

                String cmsplayerid = result2.Tables[0].Rows[0]["CMSPlayerID"].ToString();

                winner = new Player();
                winner.WinnerId = int.Parse(result2.Tables[0].Rows[0]["WinnerId"].ToString());
                // Does TicketNumber = EntryId?
                try
                {
                    Int32 entryid = Int32.Parse(result2.Tables[0].Rows[0]["TicketNumber"].ToString());
                    spParams = new List<SqlParameter>();
                    spParams.Add(new SqlParameter("@promoid", promotionId));
                    spParams.Add(new SqlParameter("@bucketid", bucketId));
                    spParams.Add(new SqlParameter("@playeraccountnum", drawingId));
                    spParams.Add(new SqlParameter("@cmsplayerid", cmsplayerid));
                    spParams.Add(new SqlParameter("@entryid", entryid));
                    DataSet result3 = DataAccess.ExecuteQuerySP("PROMOTION_DRUM_ENTRY_GetByKey", spParams);

                    //// PROMOTION_PLAYER_GetByKey
                    spParams = new List<SqlParameter>();
                    spParams.Add(new SqlParameter("@PROMOTIONID", promotionId));
                    spParams.Add(new SqlParameter("@ACCOUNTNUM", playeraccountnum));
                    spParams.Add(new SqlParameter("@CMSPLAYERID", cmsplayerid));
                    DataSet result4 = DataAccess.ExecuteQuerySP("PROMOTION_PLAYER_GetByKey", spParams);

                    //// PLAYER_GetByKey
                    spParams = new List<SqlParameter>();
                    spParams.Add(new SqlParameter("@ACCOUNTNUM", playeraccountnum));
                    spParams.Add(new SqlParameter("@CMSPLAYERID", cmsplayerid));
                    DataSet result5 = DataAccess.ExecuteQuerySP("PLAYER_GetByKey", spParams);

                    winner.PlayerAccountNum = playeraccountnum;
                    winner.CMSPlayerID = cmsplayerid;
                    winner.PlayerName = result5.Tables[0].Rows[0]["PlayerName"].ToString();
                    winner.PlayerDOB = DateTime.Parse(result5.Tables[0].Rows[0]["PlayerDOB"].ToString());
                    winner.EntryId = entryid;

                    //// DRAW_DISPLAY
                    DataAccess.ExecuteNonQuerySP("DRAW_DISPLAY", null);

                    //// DRAW_DISPLAY
                    DataAccess.ExecuteNonQuerySP("DRAW_GET_STATUS", null);

                    //// DRAW_SET_WINNER_DISPLAYED
                    //DataAccess.ExecuteNonQuerySP("DRAW_SET_WINNER_DISPLAYED", null);

                    //// DRAW_SET_WINNER_PENDING
                    //DataAccess.ExecuteNonQuerySP("DRAW_SET_WINNER_PENDING", null);
                }
                catch { }
            }
            catch (SqlException ex)
            {
                String errMessage = ex.Message;
            }
            return winner;
        }

        public static void DisqualifyWinner(Int32 promotionId, Int32 bucketId, Int32 drawingId, string playerAccountNum, string cmsPlayerId, Int32 ticketNumber)
        {
            try
            {
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@PromotionID", promotionId));
                spParams.Add(new SqlParameter("@BucketID", bucketId));
                spParams.Add(new SqlParameter("@DrawingID", drawingId));
                spParams.Add(new SqlParameter("@PlayerAccountNum", playerAccountNum));
                spParams.Add(new SqlParameter("@CMSPlayerID", cmsPlayerId));
                spParams.Add(new SqlParameter("@TicketNumber", ticketNumber));
                DataAccess.ExecuteNonQuerySP("PROMOTION_DRAWING_WINNER_Disqualify", spParams);
            }
            catch { }
        }

        public static Player RedrawWinner(Int32 promotionId, Int32 bucketId, Int32 drawingId, Int32 userID)
        {
            Player winner = null;
            try
            {
                // PROMOTION_DRAWING_ReturnPossibleCount
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@promotionID", promotionId));
                spParams.Add(new SqlParameter("@bucketID", bucketId));
                spParams.Add(new SqlParameter("@DrawingID", drawingId));
                DataSet result = DataAccess.ExecuteQuerySP("PROMOTION_DRAWING_ReturnPossibleCount", spParams);
                Int32 entryCount = Int32.Parse(result.Tables[0].Rows[0]["EntryCount"].ToString());
                TRRandom rng = new TRRandom();
                Int32 winCount = rng.GenerateRandomInt(entryCount);

                // PROMOTION_DRAWING_DrawEntryUsingTemp
                spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@promotionID", promotionId));
                spParams.Add(new SqlParameter("@bucketID", bucketId));
                spParams.Add(new SqlParameter("@drawingID", drawingId));
                spParams.Add(new SqlParameter("@winCount", winCount));
                spParams.Add(new SqlParameter("@userID", userID));
                DataSet result2 = DataAccess.ExecuteQuerySP("PROMOTION_DRAWING_DrawEntryUsingTemp", spParams);
                //// PROMOTION_DRUM_ENTRY_GetByKey
                String playeraccountnum = result2.Tables[0].Rows[0]["PlayerAccountNum"].ToString();
                String cmsplayerid = result2.Tables[0].Rows[0]["CMSPlayerID"].ToString();
                // Does TicketNumber = EntryId?
                Int32 entryid = Int32.Parse(result2.Tables[0].Rows[0]["TicketNumber"].ToString());
                spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@promoid", promotionId));
                spParams.Add(new SqlParameter("@bucketid", bucketId));
                spParams.Add(new SqlParameter("@playeraccountnum", drawingId));
                spParams.Add(new SqlParameter("@cmsplayerid", cmsplayerid));
                spParams.Add(new SqlParameter("@entryid", entryid));
                DataSet result3 = DataAccess.ExecuteQuerySP("PROMOTION_DRUM_ENTRY_GetByKey", spParams);

                //// PROMOTION_PLAYER_GetByKey
                spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@PROMOTIONID", promotionId));
                spParams.Add(new SqlParameter("@ACCOUNTNUM", playeraccountnum));
                spParams.Add(new SqlParameter("@CMSPLAYERID", cmsplayerid));
                DataSet result4 = DataAccess.ExecuteQuerySP("PROMOTION_PLAYER_GetByKey", spParams);

                //// PLAYER_GetByKey
                spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@ACCOUNTNUM", playeraccountnum));
                spParams.Add(new SqlParameter("@CMSPLAYERID", cmsplayerid));
                DataSet result5 = DataAccess.ExecuteQuerySP("PLAYER_GetByKey", spParams);

                winner = new Player();
                winner.PlayerAccountNum = playeraccountnum;
                winner.CMSPlayerID = cmsplayerid;
                winner.PlayerName = result5.Tables[0].Rows[0]["PlayerName"].ToString();
                winner.PlayerDOB = DateTime.Parse(result5.Tables[0].Rows[0]["PlayerDOB"].ToString());

                //// DRAW_SET_WINNER_PENDING
                DataAccess.ExecuteNonQuerySP("DRAW_SET_WINNER_PENDING", null);
            }
            catch (SqlException ex)
            {
                String errMessage = ex.Message;
            }
            return winner;
        }
        /// <summary>
        /// Draws winners for all drawing under a promo
        /// </summary>
        /// <param name="promoId"></param>
        /// <param name="drawingdate"></param>
        /// <returns></returns>
        public static void DrawAllWinners(Int32 promotionId, Int32 bucketId, Int32 userID)
        {
            try
            {
                DrawingGroup drawingGroup = BusinessLogic.GetDrawingGroup(promotionId);
                foreach (Drawing drawing in drawingGroup.Drawings)
                    DrawWinner(promotionId, bucketId, drawing.Id, userID, int.MinValue);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// Resets all drawing under the given promotion
        /// </summary>
        /// <returns></returns>
        public static String ResetDrawing(Int32 promoId, Int32 bucketId, DateTime drawingDateTime)
        {
            String errMessage = String.Empty;
            try
            {
                // DRAW_RESET_CHECK
                DataAccess.ExecuteNonQuerySP("DRAW_RESET_CHECK", null);

                // PROMOTION_DRAWING_EVENT_RemoveAllWinners
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@promotionID", promoId));
                spParams.Add(new SqlParameter("@bucketID", bucketId));
                spParams.Add(new SqlParameter("@drawingDateTime", drawingDateTime));
                DataAccess.ExecuteNonQuerySP("PROMOTION_DRAWING_EVENT_RemoveAllWinners", spParams);

                // DRAW_CLOSE_MASSANIMATION
                DataAccess.ExecuteNonQuerySP("DRAW_CLOSE_MASSANIMATION", null);
                /*
                //2008-02-25 JMS commented out the following b/c its useless and throwing exceptions
                // PROMOTION_DRAWING_ListByEvent
                DataSet result = new DataSet();
                spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@promoid", promoId));
                spParams.Add(new SqlParameter("@bucketid", bucketId));
                spParams.Add(new SqlParameter("@drawingdatetime", drawingDateTime));
                result = DataAccess.ExecuteQuerySP("PROMOTION_DRAWING_ListByEvent", spParams);

                // PROMOTION_DRAWING_GetNextForEvent
                DataSet result2 = new DataSet();
                spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@promoid", promoId));
                spParams.Add(new SqlParameter("@bucketid", bucketId));
                spParams.Add(new SqlParameter("@drawingdatetime", drawingDateTime));
                result2 = DataAccess.ExecuteQuerySP("PROMOTION_DRAWING_GetNextForEvent", spParams);

                // PROMOTION_DRAWING_EVENT_GetNextAvailable
                DataSet result3 = new DataSet();
                spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@promoid", promoId));
                spParams.Add(new SqlParameter("@bucketid", bucketId));
                spParams.Add(new SqlParameter("@drawingdate", drawingDateTime));//2008-02-25 JMS changed drawingdatetime to drawingdate b/c of exception thrown?
                result3 = DataAccess.ExecuteQuerySP("PROMOTION_DRAWING_EVENT_GetNextAvailable", spParams);

                // PROMOTION_DRUM_GetEntryCount
                DataSet result4 = new DataSet();
                spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@promoid", promoId));
                spParams.Add(new SqlParameter("@bucketid", bucketId));
                result4 = DataAccess.ExecuteQuerySP("PROMOTION_DRUM_GetEntryCount", spParams);

                // PROMOTION_DRUM_GetPlayerCount
                DataSet result5 = new DataSet();
                spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@promoid", promoId));
                spParams.Add(new SqlParameter("@bucketid", bucketId));
                result5 = DataAccess.ExecuteQuerySP("PROMOTION_DRUM_GetPlayerCount", spParams);

                Int32 drawingId = Convert.ToInt32(result2.Tables[0].Rows[0]["DrawingID"].ToString());
                // PROMOTION_DRAWING_GetCurrentWinner
                DataSet result6 = new DataSet();
                spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@promoid", promoId));
                spParams.Add(new SqlParameter("@bucketid", bucketId));
                spParams.Add(new SqlParameter("@drawingid", drawingId));
                result6 = DataAccess.ExecuteQuerySP("PROMOTION_DRAWING_GetCurrentWinner", spParams);
                //<!----------------------------------------------------------!>
                */
            }
            catch (SqlException ex)
            {
                errMessage = ex.Message;
            }
            return errMessage;
        }

        /// <summary>
        /// Gets drum history
        /// </summary>
        /// <param name="promoId"></param>
        /// <param name="bucketId"></param>
        /// <returns></returns>
        public static DataSet GetDrumHistory(Int32 promoId, Int32 bucketId)
        {
            DataSet dsHistory = new DataSet();
            try
            {
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@promotionID", promoId));
                spParams.Add(new SqlParameter("@BucketID", bucketId));
                dsHistory = DataAccess.ExecuteQuerySP("PROMOTION_DRUM_GetHistory", spParams);
            }
            catch (SqlException ex)
            {
                String errMessage = ex.Message;
            }
            return dsHistory;
        }

        public static void Validate(Int32 promoId, Int32 bucketId, Int32 drawingId, String playerAccountNum, String cmsPlayerId, Int32 ticketNumber, Int32 userId)
        {
            try
            {
                // PROMOTION_DRAWING_WINNER_Validate
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@PromotionID", promoId));
                spParams.Add(new SqlParameter("@BucketID", bucketId));
                spParams.Add(new SqlParameter("@DrawingID", drawingId));
                spParams.Add(new SqlParameter("@PlayerAccountNum", playerAccountNum));
                spParams.Add(new SqlParameter("@CMSPlayerID", cmsPlayerId));
                spParams.Add(new SqlParameter("@TicketNumber", ticketNumber));
                spParams.Add(new SqlParameter("@UserID", userId));
                DataAccess.ExecuteNonQuerySP("PROMOTION_DRAWING_WINNER_Validate", spParams);
            }
            catch { }
        }
    }
}


