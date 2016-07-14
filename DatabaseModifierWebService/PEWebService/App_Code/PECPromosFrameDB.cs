using System;
using System.Collections.Generic;
using System.Linq;
using PE.DataReturn;
using System.Data.SqlClient;
using System.Data;
using PE.DataReturns;
using System.IO;


/// <summary>
/// CustAppService deals with user login, registration, and loading of assets at launch
/// </summary>
namespace PE.PromosFrameDB
{
    #region My Promotions Screen
    [Serializable]
    public class GetPromotionsScreenWrapperReturn : Default
    {
        private string line1Caption;
        private string line1Data;
        private string line2Caption;
        private string line2Data;
        private List<PromoButton> buttons;
        #region publics
        public string Line1Caption
        {
            get
            {
                return line1Caption;
            }

            set
            {
                line1Caption = value;
            }
        }

        public string Line1Data
        {
            get
            {
                return line1Data;
            }

            set
            {
                line1Data = value;
            }
        }

        public string Line2Caption
        {
            get
            {
                return line2Caption;
            }

            set
            {
                line2Caption = value;
            }
        }

        public string Line2Data
        {
            get
            {
                return line2Data;
            }

            set
            {
                line2Data = value;
            }
        }

        public List<PromoButton> Buttons
        {
            get
            {
                return buttons;
            }

            set
            {
                buttons = value;
            }
        }
        #endregion

        public class PromoButton
        {
            private string buttonCaption;
            private int buttonOrdPos;

            public string ButtonCaption
            {
                get
                {
                    return buttonCaption;
                }

                set
                {
                    buttonCaption = value;
                }
            }

            public int ButtonOrdPos
            {
                get
                {
                    return buttonOrdPos;
                }

                set
                {
                    buttonOrdPos = value;
                }
            }
        }

        private void RemoveData()
        {
            Line1Caption = null;
            Line2Caption = null;
            Line1Data = null;
            Line2Data = null;
            Buttons = null;

        }
        //DB Get Promotions Screen Wrapper from SQL Server
        public void DBGetPromotionsScreenWrapperReturn(string mobile)
        {
            try
            {
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);

                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                result = DataAcess.ExecuteQuerySP("PEC.PROMOTIONS_GetWrapper", spParams);

                if (result.Tables[0].Rows.Count > 0)
                {
                    Line1Caption = result.Tables[0].Rows[0]["Line1Caption"].ToString();
                    Line1Data = result.Tables[0].Rows[0]["Line1Data"].ToString();
                    Line2Caption = result.Tables[0].Rows[0]["Line2Caption"].ToString();
                    Line2Data = result.Tables[0].Rows[0]["Line2Data"].ToString();

                    DataSet buttonDS = new DataSet();
                    List<SqlParameter> buttonParams = new List<SqlParameter>();
                    buttonParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                    buttonDS = DataAcess.ExecuteQuerySP("PEC.PROMOTIONS_WrapperButtons", buttonParams);

                    if (buttonDS.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < buttonDS.Tables[0].Rows.Count; i++)
                        {
                            PromoButton newButton = new PromoButton();
                            newButton.ButtonCaption = buttonDS.Tables[0].Rows[i]["ButtonCaption"].ToString();
                            newButton.ButtonOrdPos = Convert.ToInt32(buttonDS.Tables[0].Rows[i]["ButtonOrdPos"].ToString());
                            Buttons.Add(newButton);
                        }
                    }
                    else
                    {
                        RemoveData();
                    }
                }
                else
                {
                    RemoveData();
                }
            }



            catch (SqlException ex)
            {
                RemoveData();
                string errorMessage = ex.Message;
            }

        }
    }

    [Serializable]
    public class GetPromotionListReturn : Default
    {
        private List<Promotion> promotions;

        public List<Promotion> Promotions
        {
            get
            {
                return promotions;
            }

            set
            {
                promotions = value;
            }
        }

        public class Promotion
        {
            private int promotionID;
            private int promoVersionID;
            private int gameID;
            private string promotionName;
            private DateTime startDate;
            private DateTime endDate;
            private bool enrolled;
            private bool couponsAvaliable;
            private DateTime nextDrawingDate;
            private bool checkInAvaliable;
            private bool freeEntryAvaliable;
            private bool remoteEntryAvaliable;
            private int pointsToDate;
            private int entriesToDate;
            private int pointsPerEntry;
            private int todaysPoint;
            private int dailyEntryLImit;
            private string thresholdType;
            private int thresholdPoints;
            private string prizeName;
            private bool thresholdReached;
            private bool prizeClaimed;
            private bool gameAvaliable;
            private string checkInStatusMessage;
            private bool raffleAvaliable;
            private List<RaffleTicketList> raffleTickets;
            private byte[] promotionImage;
            #region Publics
            public int PromotionID
            {
                get
                {
                    return promotionID;
                }

                set
                {
                    promotionID = value;
                }
            }

            public int PromoVersionID
            {
                get
                {
                    return promoVersionID;
                }

                set
                {
                    promoVersionID = value;
                }
            }

            public int GameID
            {
                get
                {
                    return gameID;
                }

                set
                {
                    gameID = value;
                }
            }

            public string PromotionName
            {
                get
                {
                    return promotionName;
                }

                set
                {
                    promotionName = value;
                }
            }

            public DateTime StartDate
            {
                get
                {
                    return startDate;
                }

                set
                {
                    startDate = value;
                }
            }

            public DateTime EndDate
            {
                get
                {
                    return endDate;
                }

                set
                {
                    endDate = value;
                }
            }

            public bool Enrolled
            {
                get
                {
                    return enrolled;
                }

                set
                {
                    enrolled = value;
                }
            }

            public bool CouponsAvaliable
            {
                get
                {
                    return couponsAvaliable;
                }

                set
                {
                    couponsAvaliable = value;
                }
            }

            public DateTime NextDrawingDate
            {
                get
                {
                    return nextDrawingDate;
                }

                set
                {
                    nextDrawingDate = value;
                }
            }

            public bool CheckInAvaliable
            {
                get
                {
                    return checkInAvaliable;
                }

                set
                {
                    checkInAvaliable = value;
                }
            }

            public bool FreeEntryAvaliable
            {
                get
                {
                    return freeEntryAvaliable;
                }

                set
                {
                    freeEntryAvaliable = value;
                }
            }

            public bool RemoteEntryAvaliable
            {
                get
                {
                    return remoteEntryAvaliable;
                }

                set
                {
                    remoteEntryAvaliable = value;
                }
            }

            public int PointsToDate
            {
                get
                {
                    return pointsToDate;
                }

                set
                {
                    pointsToDate = value;
                }
            }

            public int EntriesToDate
            {
                get
                {
                    return entriesToDate;
                }

                set
                {
                    entriesToDate = value;
                }
            }

            public int PointsPerEntry
            {
                get
                {
                    return pointsPerEntry;
                }

                set
                {
                    pointsPerEntry = value;
                }
            }

            public int TodaysPoint
            {
                get
                {
                    return todaysPoint;
                }

                set
                {
                    todaysPoint = value;
                }
            }

            public int DailyEntryLImit
            {
                get
                {
                    return dailyEntryLImit;
                }

                set
                {
                    dailyEntryLImit = value;
                }
            }

            public string ThresholdType
            {
                get
                {
                    return thresholdType;
                }

                set
                {
                    thresholdType = value;
                }
            }

            public int ThresholdPoints
            {
                get
                {
                    return thresholdPoints;
                }

                set
                {
                    thresholdPoints = value;
                }
            }

            public string PrizeName
            {
                get
                {
                    return prizeName;
                }

                set
                {
                    prizeName = value;
                }
            }

            public bool ThresholdReached
            {
                get
                {
                    return thresholdReached;
                }

                set
                {
                    thresholdReached = value;
                }
            }

            public bool PrizeClaimed
            {
                get
                {
                    return prizeClaimed;
                }

                set
                {
                    prizeClaimed = value;
                }
            }

            public bool GameAvaliable
            {
                get
                {
                    return gameAvaliable;
                }

                set
                {
                    gameAvaliable = value;
                }
            }

            public string CheckInStatusMessage
            {
                get
                {
                    return checkInStatusMessage;
                }

                set
                {
                    checkInStatusMessage = value;
                }
            }

            public bool RaffleAvaliable
            {
                get
                {
                    return raffleAvaliable;
                }

                set
                {
                    raffleAvaliable = value;
                }
            }

            public List<RaffleTicketList> RaffleTickets
            {
                get
                {
                    return raffleTickets;
                }

                set
                {
                    raffleTickets = value;
                }
            }

            public byte[] PromotionImage
            {
                get
                {
                    return promotionImage;
                }

                set
                {
                    promotionImage = value;
                }
            }
        }
        #endregion
        public class RaffleTicketList
        {
            private string raffleTicket;

            public string RaffleTicket
            {
                get
                {
                    return raffleTicket;
                }

                set
                {
                    raffleTicket = value;
                }
            }
        }

        private void RemoveData()
        {
            Promotions = null;
        }
        //DB Get Promotion List
        public void DBGetPromotionList(string mobile)
        {
            try
            {
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);
                DataSet ds = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                ds = DataAcess.ExecuteQuerySP("PEC.PROMOTIONS_GetPromotionList_ByCMSPlayerID", spParams);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Promotion newPromo = new Promotion();
                        newPromo.PromotionID = Convert.ToInt32(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.PromoVersionID = Convert.ToInt32(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.GameID = Convert.ToInt32(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.PromotionName = ds.Tables[0].Rows[i][""].ToString();
                        newPromo.StartDate = Convert.ToDateTime(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.EndDate = Convert.ToDateTime(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.Enrolled = Convert.ToBoolean(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.CouponsAvaliable = Convert.ToBoolean(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.NextDrawingDate = Convert.ToDateTime(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.CheckInAvaliable = Convert.ToBoolean(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.CheckInStatusMessage = ds.Tables[0].Rows[i][""].ToString();
                        newPromo.FreeEntryAvaliable = Convert.ToBoolean(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.RemoteEntryAvaliable = Convert.ToBoolean(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.PointsToDate = Convert.ToInt32(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.EntriesToDate = Convert.ToInt32(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.PointsPerEntry = Convert.ToInt32(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.TodaysPoint = Convert.ToInt32(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.DailyEntryLImit = Convert.ToInt32(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.ThresholdType = ds.Tables[0].Rows[i][""].ToString();
                        newPromo.ThresholdPoints = Convert.ToInt32(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.PrizeName = ds.Tables[0].Rows[i][""].ToString();
                        newPromo.ThresholdReached = Convert.ToBoolean(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.PrizeClaimed = Convert.ToBoolean(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.GameAvaliable = Convert.ToBoolean(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.RaffleAvaliable = Convert.ToBoolean(ds.Tables[0].Rows[i][""].ToString());

                        DataSet raffleDS = new DataSet();
                        List<SqlParameter> raffleParams = new List<SqlParameter>();
                        raffleParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                        raffleDS = DataAcess.ExecuteQuerySP("PEC.PROMOTIONS_GetPromotionRaffleTickets", raffleParams);

                        if (raffleDS.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < raffleDS.Tables[0].Rows.Count; j++)
                            {
                                RaffleTicketList newRaffleTicket = new RaffleTicketList();
                                newRaffleTicket.RaffleTicket = raffleDS.Tables[0].Rows[j][""].ToString();
                                newPromo.RaffleTickets.Add(newRaffleTicket);
                            }
                        }
                        else
                        {
                            newPromo.RaffleTickets = null;
                        }

                        DataSet imageDS = new DataSet();
                        List<SqlParameter> imageParams = new List<SqlParameter>();
                        imageParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                        imageDS = DataAcess.ExecuteQuerySP("PEC.PROMOTIONS_GetPromotionImage", imageParams);

                        if (imageDS.Tables[0].Rows.Count > 0)
                        {
                            MemoryStream ms = new MemoryStream((byte[])imageDS.Tables[0].Rows[0]["Image"]);
                            byte[] bytes = ms.ToArray();
                            newPromo.PromotionImage = bytes;
                        }
                        else
                        {
                            newPromo.PromotionImage = null;
                        }

                        Promotions.Add(newPromo);
                    }
                }
                else
                {
                    RemoveData();
                }
            }
            catch (SqlException ex)
            {
                string errorMessage = ex.Message;
                RemoveData();
            }
        }


    }

    [Serializable]
    public class EnterRemoteEntryReturn : Default
    {
        private int promotionID;
        private int updateEntryCount;
        private bool remoteEntryAvaliable;
        private void RemoveData()
        {
            PromotionID = -1;
            UpdateEntryCount = -1;
            RemoteEntryAvaliable = false;
        }
        #region publics
        public int PromotionID
        {
            get
            {
                return promotionID;
            }

            set
            {
                promotionID = value;
            }
        }

        public int UpdateEntryCount
        {
            get
            {
                return updateEntryCount;
            }

            set
            {
                updateEntryCount = value;
            }
        }

        public bool RemoteEntryAvaliable
        {
            get
            {
                return remoteEntryAvaliable;
            }

            set
            {
                remoteEntryAvaliable = value;
            }
        }
        #endregion
        public void DBEnterRemoteEntry(string mobile, int promotionID)
        {
            PromotionID = promotionID;
            try
            {
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);

                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                spParams.Add(new SqlParameter("@PromotionID", promotionID));
                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);

                if (result.Tables[0].Rows.Count > 0)
                {
                    UpdateEntryCount = Convert.ToInt32(result.Tables[0].Rows[0]["UpdateEntryCount"].ToString());
                    RemoteEntryAvaliable = Convert.ToBoolean(result.Tables[0].Rows[0]["RemoteEntryAvaliable"].ToString());
                }
                else
                {
                    RemoveData();
                }
            }
            catch (SqlException ex)
            {
                string errorMessage = ex.Message;
                RemoveData();
            }
        }
    }

    [Serializable]
    public class ClaimBonusCouponsReturn : Default
    {
        private int promotionID;
        private List<ResultDescriptionBonus> resultDescriptions;
        private bool claimButtonRemainsVisible;
        private bool claimButtonRemainsActive;
        private string claimButtonNewCaption;
        private byte[] claimButtonNewImage;
        #region publics
        public int PromotionID
        {
            get
            {
                return promotionID;
            }

            set
            {
                promotionID = value;
            }
        }

        public List<ResultDescriptionBonus> ResultDescriptions
        {
            get
            {
                return resultDescriptions;
            }

            set
            {
                resultDescriptions = value;
            }
        }

        public bool ClaimButtonRemainsVisible
        {
            get
            {
                return claimButtonRemainsVisible;
            }

            set
            {
                claimButtonRemainsVisible = value;
            }
        }

        public bool ClaimButtonRemainsActive
        {
            get
            {
                return claimButtonRemainsActive;
            }

            set
            {
                claimButtonRemainsActive = value;
            }
        }

        public string ClaimButtonNewCaption
        {
            get
            {
                return claimButtonNewCaption;
            }

            set
            {
                claimButtonNewCaption = value;
            }
        }

        public byte[] ClaimButtonNewImage
        {
            get
            {
                return claimButtonNewImage;
            }

            set
            {
                claimButtonNewImage = value;
            }
        }
        #endregion
        public class ResultDescriptionBonus
        {
            private string resultCaption;
            private string resultUpdatedElement;
            private byte[] resultImage;
            private List<DataPoint> dataPoints;
            #region publics
            public string ResultCaption
            {
                get
                {
                    return resultCaption;
                }

                set
                {
                    resultCaption = value;
                }
            }

            public string ResultUpdatedElement
            {
                get
                {
                    return resultUpdatedElement;
                }

                set
                {
                    resultUpdatedElement = value;
                }
            }

            public byte[] ResultImage
            {
                get
                {
                    return resultImage;
                }

                set
                {
                    resultImage = value;
                }
            }

            public List<DataPoint> DataPoints
            {
                get
                {
                    return dataPoints;
                }

                set
                {
                    dataPoints = value;
                }
            }
            #endregion

        }

        public class DataPoint
        {
            private string dataPointCaption;
            private string dataPointData;

            public string DataPointCaption
            {
                get
                {
                    return dataPointCaption;
                }

                set
                {
                    dataPointCaption = value;
                }
            }

            public string DataPointData
            {
                get
                {
                    return dataPointData;
                }

                set
                {
                    dataPointData = value;
                }
            }
        }

        private void RemoveData()
        {
            PromotionID = -1;
            ResultDescriptions = null;
            ClaimButtonNewCaption = null;
            ClaimButtonNewImage = null;
            ClaimButtonRemainsVisible = false;
            ClaimButtonRemainsActive = false;
        }

        public void DBClaimBonusCoupons(string mobile, int promotionID)
        {
            try
            {
                PromotionID = promotionID;
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                spParams.Add(new SqlParameter("@PromotionID", PromotionID));
                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);

                if (result.Tables[0].Rows.Count > 0)
                {
                    ClaimButtonRemainsVisible = Convert.ToBoolean(result.Tables[0].Rows[0][""].ToString());
                    ClaimButtonRemainsActive = Convert.ToBoolean(result.Tables[0].Rows[0][""].ToString());
                    ClaimButtonNewCaption = result.Tables[0].Rows[0][""].ToString();

                    MemoryStream ms = new MemoryStream((byte[])result.Tables[0].Rows[0]["Image"]);
                    byte[] bytes = ms.ToArray();
                    ClaimButtonNewImage = bytes;

                    DataSet descDS = new DataSet();
                    List<SqlParameter> descParams = new List<SqlParameter>();
                    descParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                    descParams.Add(new SqlParameter("@PromotionID", PromotionID));
                    descDS = DataAcess.ExecuteQuerySP("PEC.TODO", descParams);


                    if (descDS.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < descDS.Tables[0].Rows.Count; i++)
                        {
                            ResultDescriptionBonus newRDB = new ResultDescriptionBonus();
                            newRDB.ResultCaption = descDS.Tables[0].Rows[i][""].ToString();
                            newRDB.ResultUpdatedElement = descDS.Tables[0].Rows[i][""].ToString();

                            MemoryStream msResult = new MemoryStream((byte[])descDS.Tables[0].Rows[i]["Image"]);
                            byte[] resultBytes = msResult.ToArray();
                            newRDB.ResultImage = resultBytes;

                            DataSet dP = new DataSet();
                            List<SqlParameter> pointsParams = new List<SqlParameter>();
                            pointsParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                            pointsParams.Add(new SqlParameter("@PromotionID", PromotionID));
                            dP = DataAcess.ExecuteQuerySP("PEC.TODO", pointsParams);
                            if (dP.Tables[0].Rows.Count > 0)
                            {
                                for (int j = 0; j < dP.Tables[0].Rows.Count; j++)
                                {
                                    DataPoint newPoint = new DataPoint();
                                    newPoint.DataPointCaption = dP.Tables[0].Rows[i]["DataPointCaption"].ToString();
                                    newPoint.DataPointData = dP.Tables[0].Rows[i]["DataPointData"].ToString();
                                    newRDB.DataPoints.Add(newPoint);
                                }
                            }
                            else
                            {
                                newRDB.DataPoints = null;
                            }

                        }
                    }
                    else
                    {
                        ResultDescriptions = null;
                    }

                }
                else
                {
                    RemoveData();
                }
            }
            catch (SqlException ex)
            {
                string errorMessage = ex.Message;
                RemoveData();
            }
        }
    }

    [Serializable]
    public class ListEntriesInNextDrawReturn : Default
    {
        private int promotionID;
        private DateTime nextDrawDate;
        private DateTime nextDrawTime;
        private int entriesForNextDraw;
        private bool isDrumPopulated;
        private List<string> entryNumbers;
        private string specialMessage;
        #region publics
        public int PromotionID
        {
            get
            {
                return promotionID;
            }

            set
            {
                promotionID = value;
            }
        }

        public DateTime NextDrawDate
        {
            get
            {
                return nextDrawDate;
            }

            set
            {
                nextDrawDate = value;
            }
        }

        public DateTime NextDrawTime
        {
            get
            {
                return nextDrawTime;
            }

            set
            {
                nextDrawTime = value;
            }
        }

        public int EntriesForNextDraw
        {
            get
            {
                return entriesForNextDraw;
            }

            set
            {
                entriesForNextDraw = value;
            }
        }

        public bool IsDrumPopulated
        {
            get
            {
                return isDrumPopulated;
            }

            set
            {
                isDrumPopulated = value;
            }
        }

        public List<string> EntryNumbers
        {
            get
            {
                return entryNumbers;
            }

            set
            {
                entryNumbers = value;
            }
        }

        public string SpecialMessage
        {
            get
            {
                return specialMessage;
            }

            set
            {
                specialMessage = value;
            }
        }

        #endregion
        private void RemoveData()
        {
            PromotionID = -1;
            EntriesForNextDraw = -1;
            IsDrumPopulated = false;
            EntryNumbers = null;
            SpecialMessage = null;
        }
        public void DBListEntriesInNextDraw(string mobile, int promotionID)
        {
            try
            {
                PromotionID = promotionID;
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                spParams.Add(new SqlParameter("@PromotionID", PromotionID));
                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);

                if (result.Tables[0].Rows.Count > 0)
                {
                    NextDrawDate = Convert.ToDateTime(result.Tables[0].Rows[0]["NextDrawDate"].ToString());
                    NextDrawTime = Convert.ToDateTime(result.Tables[0].Rows[0]["NextDrawTime"].ToString());
                    EntriesForNextDraw = Convert.ToInt32(result.Tables[0].Rows[0]["EntriesForNextDraw"].ToString());
                    IsDrumPopulated = Convert.ToBoolean(result.Tables[0].Rows[0]["IsPopulated"].ToString());
                    SpecialMessage = result.Tables[0].Rows[0]["SpecialMessage"].ToString();
                    if (IsDrumPopulated)
                    {
                        DataSet entryDS = new DataSet();
                        entryDS = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);
                        if (entryDS.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < entryDS.Tables[0].Rows.Count; i++)
                            {
                                string entryNum = entryDS.Tables[0].Rows[i]["EntryNumber"].ToString();
                                EntryNumbers.Add(entryNum);
                            }
                        }
                        else
                        {
                            EntryNumbers = null;
                        }
                    }
                    else
                    {
                        EntryNumbers = null;
                    }
                }
                else
                {
                    RemoveData();
                }

            }
            catch (SqlException ex)
            {
                string errorMessage = ex.Message;
                RemoveData();
            }
        }
    }
    #endregion
}