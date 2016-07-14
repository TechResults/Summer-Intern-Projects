using System;
using System.Collections.Generic;
using System.Linq;
using PE.DataReturn;
using System.Data.SqlClient;
using System.Data;
using PE.DataReturns;
using System.IO;


/// <summary>
/// 
/// </summary>
namespace PE.EventsFrameDB
{
    #region My Events Screen
    [Serializable]
    public class GetEventsScreenWrapperReturn : Default
    {
        private string line1Caption;
        private string line1Data;
        private string line2Caption;
        private string line2Data;
        private List<EventButton> buttons;
        #region Publics
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

        public List<EventButton> Buttons
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
        public class EventButton
        {
            private string buttonCaption;
            private int buttonOrdPos;
            private int buttonEventID;
            private byte[] buttonImage;
            #region Publics
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

            public int ButtonEventID
            {
                get
                {
                    return buttonEventID;
                }

                set
                {
                    buttonEventID = value;
                }
            }

            public byte[] ButtonImage
            {
                get
                {
                    return buttonImage;
                }

                set
                {
                    buttonImage = value;
                }
            }
            #endregion
        }

        private void RemoveData()
        {
            Line1Caption = null;
            Line1Data = null;
            Line2Caption = null;
            Line2Data = null;
            Buttons = null;
        }

        public void DBGetEventsScreenWrapper(string mobile)
        {
            try
            {
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));

                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);

                if (result.Tables[0].Rows.Count > 0)
                {
                    Line1Caption = result.Tables[0].Rows[0][""].ToString();
                    Line1Data = result.Tables[0].Rows[0][""].ToString();
                    Line2Caption = result.Tables[0].Rows[0][""].ToString();
                    Line2Data = result.Tables[0].Rows[0][""].ToString();


                    DataSet buttonDS = new DataSet();
                    List<SqlParameter> buttonParams = new List<SqlParameter>();
                    buttonParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                    buttonDS = DataAcess.ExecuteQuerySP("PEC.TODO", buttonParams);

                    if (buttonDS.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < buttonDS.Tables[0].Rows.Count; i++)
                        {
                            EventButton newButton = new EventButton();
                            newButton.ButtonCaption = result.Tables[0].Rows[i][""].ToString();
                            newButton.ButtonOrdPos = Convert.ToInt32(result.Tables[0].Rows[i][""].ToString());
                            newButton.ButtonEventID = Convert.ToInt32(result.Tables[0].Rows[i][""].ToString());
                            MemoryStream ButtonMS = new MemoryStream((byte[])result.Tables[0].Rows[i][""]);
                            byte[] ButtonBytes = ButtonMS.ToArray();
                            newButton.ButtonImage = ButtonBytes;
                            Buttons.Add(newButton);
                        }
                    }
                    else
                    {
                        Buttons = null;
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
    public class GetEventDetailsReturn : Default
    {
        private int eventID;
        private string headerCaptionLine1;
        private string headerCaptionLine2;
        private byte[] eventLargeImage;
        private string eventCaption;
        private DateTime eventStartDate;
        private DateTime eventStartTime;
        private DateTime eventEndDate;
        private DateTime eventEndTime;
        private bool displayOptions;
        private List<EventOption> eventOptions;
        private string footerCaptionLine1;
        private string footerCaptionLine2;
        #region PUBLICS
        public int EventID
        {
            get
            {
                return eventID;
            }

            set
            {
                eventID = value;
            }
        }

        public string HeaderCaptionLine1
        {
            get
            {
                return headerCaptionLine1;
            }

            set
            {
                headerCaptionLine1 = value;
            }
        }

        public string HeaderCaptionLine2
        {
            get
            {
                return headerCaptionLine2;
            }

            set
            {
                headerCaptionLine2 = value;
            }
        }

        public byte[] EventLargeImage
        {
            get
            {
                return eventLargeImage;
            }

            set
            {
                eventLargeImage = value;
            }
        }

        public string EventCaption
        {
            get
            {
                return eventCaption;
            }

            set
            {
                eventCaption = value;
            }
        }

        public DateTime EventStartDate
        {
            get
            {
                return eventStartDate;
            }

            set
            {
                eventStartDate = value;
            }
        }

        public DateTime EventStartTime
        {
            get
            {
                return eventStartTime;
            }

            set
            {
                eventStartTime = value;
            }
        }

        public DateTime EventEndDate
        {
            get
            {
                return eventEndDate;
            }

            set
            {
                eventEndDate = value;
            }
        }

        public DateTime EventEndTime
        {
            get
            {
                return eventEndTime;
            }

            set
            {
                eventEndTime = value;
            }
        }

        public bool DisplayOptions
        {
            get
            {
                return displayOptions;
            }

            set
            {
                displayOptions = value;
            }
        }

        public List<EventOption> EventOptions
        {
            get
            {
                return eventOptions;
            }

            set
            {
                eventOptions = value;
            }
        }

        public string FooterCaptionLine1
        {
            get
            {
                return footerCaptionLine1;
            }

            set
            {
                footerCaptionLine1 = value;
            }
        }

        public string FooterCaptionLine2
        {
            get
            {
                return footerCaptionLine2;
            }

            set
            {
                footerCaptionLine2 = value;
            }
        }

        #endregion

        public class EventOption
        {
            private string optionCaption;
            private byte[] optionImage;
            private bool optionExecutable;
            private int optionReferenceID;

            public string OptionCaption
            {
                get
                {
                    return optionCaption;
                }

                set
                {
                    optionCaption = value;
                }
            }

            public byte[] OptionImage
            {
                get
                {
                    return optionImage;
                }

                set
                {
                    optionImage = value;
                }
            }

            public bool OptionExecutable
            {
                get
                {
                    return optionExecutable;
                }

                set
                {
                    optionExecutable = value;
                }
            }

            public int OptionReferenceID
            {
                get
                {
                    return optionReferenceID;
                }

                set
                {
                    optionReferenceID = value;
                }
            }
        }

        private void RemoveData()
        {
            EventID = -1;
            HeaderCaptionLine1 = null;
            HeaderCaptionLine2 = null;
            EventLargeImage = null;
            EventCaption = null;
            DisplayOptions = false;
            FooterCaptionLine1 = null;
            FooterCaptionLine2 = null;
        }
        public void DBGetEventDetails(string mobile, int eventID)
        {
            try
            {
                EventID = eventID;
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                spParams.Add(new SqlParameter("@EventID", eventID));
                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);

                if (result.Tables[0].Rows.Count > 0)
                {
                    HeaderCaptionLine1 = result.Tables[0].Rows[0][""].ToString();
                    HeaderCaptionLine2 = result.Tables[0].Rows[0][""].ToString();
                    EventCaption = result.Tables[0].Rows[0][""].ToString();
                    EventStartDate = Convert.ToDateTime(result.Tables[0].Rows[0][""]);
                    EventStartTime = Convert.ToDateTime(result.Tables[0].Rows[0][""]);
                    EventEndDate = Convert.ToDateTime(result.Tables[0].Rows[0][""]);
                    EventEndTime = Convert.ToDateTime(result.Tables[0].Rows[0][""]);
                    DisplayOptions = Convert.ToBoolean(result.Tables[0].Rows[0][""]);
                    FooterCaptionLine1 = result.Tables[0].Rows[0][""].ToString();
                    FooterCaptionLine2 = result.Tables[0].Rows[0][""].ToString();
                    MemoryStream ms = new MemoryStream((byte[])result.Tables[0].Rows[0]["LargeImage"]);
                    byte[] largeImage = ms.ToArray();
                    EventLargeImage = largeImage;

                    if (DisplayOptions)
                    {
                        DataSet optionDS = new DataSet();
                        List<SqlParameter> optionParams = new List<SqlParameter>();
                        optionParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                        optionParams.Add(new SqlParameter("@EventID", EventID));
                        optionDS = DataAcess.ExecuteQuerySP("PEC.TODO", optionParams);
                        if (optionDS.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < optionDS.Tables[0].Rows.Count; i++)
                            {
                                EventOption newEvent = new EventOption();
                                newEvent.OptionCaption = optionDS.Tables[0].Rows[i]["OptionCaption"].ToString();
                                newEvent.OptionExecutable = Convert.ToBoolean(optionDS.Tables[0].Rows[i]["OptionExecutable"].ToString());
                                MemoryStream optionMS = new MemoryStream((byte[])optionDS.Tables[0].Rows[i]["OptionImage"]);
                                byte[] optionImageByte = optionMS.ToArray();
                                newEvent.OptionImage = optionImageByte;
                                newEvent.OptionReferenceID = Convert.ToInt32(optionDS.Tables[0].Rows[i]["ReferenceID"].ToString());
                                EventOptions.Add(newEvent);
                            }
                        }
                        else
                        {
                            EventOptions = null;
                        }
                    }
                    else
                    {
                        EventOptions = null;
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
    public class EnrollGuestInEventReturn : Default
    {
        private int eventID;
        private int optionReferenceID;
        private string headerCaptionLine1;
        private string headerCaptionLine2;
        private bool isEnrolled;
        private string confirmationNumber;
        private byte[] bodyImage1;
        private byte[] bodyImage2;
        private string footerCaptionLine1;
        private string footerCaptionLine2;
        #region PUBLICS
        public int EventID
        {
            get
            {
                return eventID;
            }

            set
            {
                eventID = value;
            }
        }

        public int OptionReferenceID
        {
            get
            {
                return optionReferenceID;
            }

            set
            {
                optionReferenceID = value;
            }
        }

        public string HeaderCaptionLine1
        {
            get
            {
                return headerCaptionLine1;
            }

            set
            {
                headerCaptionLine1 = value;
            }
        }

        public string HeaderCaptionLine2
        {
            get
            {
                return headerCaptionLine2;
            }

            set
            {
                headerCaptionLine2 = value;
            }
        }

        public bool IsEnrolled
        {
            get
            {
                return isEnrolled;
            }

            set
            {
                isEnrolled = value;
            }
        }

        public string ConfirmationNumber
        {
            get
            {
                return confirmationNumber;
            }

            set
            {
                confirmationNumber = value;
            }
        }

        public byte[] BodyImage1
        {
            get
            {
                return bodyImage1;
            }

            set
            {
                bodyImage1 = value;
            }
        }

        public byte[] BodyImage2
        {
            get
            {
                return bodyImage2;
            }

            set
            {
                bodyImage2 = value;
            }
        }

        public string FooterCaptionLine1
        {
            get
            {
                return footerCaptionLine1;
            }

            set
            {
                footerCaptionLine1 = value;
            }
        }

        public string FooterCaptionLine2
        {
            get
            {
                return footerCaptionLine2;
            }

            set
            {
                footerCaptionLine2 = value;
            }
        }
        #endregion
        private void RemoveData()
        {
            EventID = -1;
            OptionReferenceID = -1;
            HeaderCaptionLine1 = null;
            HeaderCaptionLine2 = null;
            IsEnrolled = false;
            ConfirmationNumber = null;
            BodyImage1 = null;
            BodyImage2 = null;
            FooterCaptionLine1 = null;
            FooterCaptionLine2 = null;
        }
        public void DBEnrollGuestInEvent(string mobile, int eventID, int optionReferenceID)
        {
            try
            {
                EventID = eventID;
                OptionReferenceID = optionReferenceID;
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                spParams.Add(new SqlParameter("@EventID", eventID));
                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);
                HeaderCaptionLine1 = result.Tables[0].Rows[0]["HeaderCaptionLine1"].ToString();
                HeaderCaptionLine2 = result.Tables[0].Rows[0][""].ToString();
                IsEnrolled = Convert.ToBoolean(result.Tables[0].Rows[0][""].ToString());
                ConfirmationNumber = result.Tables[0].Rows[0][""].ToString();
                MemoryStream bodyImage1MS = new MemoryStream((byte[])result.Tables[0].Rows[0][""]);
                byte[] bodyImage1Byte = bodyImage1MS.ToArray();
                BodyImage1 = bodyImage1Byte;

                MemoryStream bodyImage2MS = new MemoryStream((byte[])result.Tables[0].Rows[0][""]);
                byte[] bodyImage2Byte = bodyImage2MS.ToArray();
                BodyImage2 = bodyImage2Byte;

                FooterCaptionLine1 = result.Tables[0].Rows[0][""].ToString();
                FooterCaptionLine2 = result.Tables[0].Rows[0][""].ToString();

            }
            catch (SqlException ex)
            {
                string errorMessage = ex.Message;
                RemoveData();
            }
        }
    }

    [Serializable]
    public class RequestTicketsToEventReturn : Default
    {
        private int eventID;
        private int optionReferenceID;
        private string headerCaptionLine1;
        private string headerCaptionLine2;
        private bool isEnrolled;
        private string confirmationNumber;
        private int ticketCountAwarded;
        private bool isOnWaitList;
        private string expectedResponseInterval;
        private byte[] bodyImage1;
        private byte[] bodyImage2;
        private string footerCaptionLine1;
        private string footerCaptionLine2;
        #region PUBLICS
        public int EventID
        {
            get
            {
                return eventID;
            }

            set
            {
                eventID = value;
            }
        }

        public int OptionReferenceID
        {
            get
            {
                return optionReferenceID;
            }

            set
            {
                optionReferenceID = value;
            }
        }

        public string HeaderCaptionLine1
        {
            get
            {
                return headerCaptionLine1;
            }

            set
            {
                headerCaptionLine1 = value;
            }
        }

        public string HeaderCaptionLine2
        {
            get
            {
                return headerCaptionLine2;
            }

            set
            {
                headerCaptionLine2 = value;
            }
        }

        public bool IsEnrolled
        {
            get
            {
                return isEnrolled;
            }

            set
            {
                isEnrolled = value;
            }
        }

        public string ConfirmationNumber
        {
            get
            {
                return confirmationNumber;
            }

            set
            {
                confirmationNumber = value;
            }
        }

        public int TicketCountAwarded
        {
            get
            {
                return ticketCountAwarded;
            }

            set
            {
                ticketCountAwarded = value;
            }
        }

        public bool IsOnWaitList
        {
            get
            {
                return isOnWaitList;
            }

            set
            {
                isOnWaitList = value;
            }
        }

        public string ExpectedResponseInterval
        {
            get
            {
                return expectedResponseInterval;
            }

            set
            {
                expectedResponseInterval = value;
            }
        }

        public byte[] BodyImage1
        {
            get
            {
                return bodyImage1;
            }

            set
            {
                bodyImage1 = value;
            }
        }

        public byte[] BodyImage2
        {
            get
            {
                return bodyImage2;
            }

            set
            {
                bodyImage2 = value;
            }
        }

        public string FooterCaptionLine1
        {
            get
            {
                return footerCaptionLine1;
            }

            set
            {
                footerCaptionLine1 = value;
            }
        }

        public string FooterCaptionLine2
        {
            get
            {
                return footerCaptionLine2;
            }

            set
            {
                footerCaptionLine2 = value;
            }
        }
        #endregion

        private void RemoveData()
        {
            EventID = -1;
            OptionReferenceID = -1;
            HeaderCaptionLine1 = null;
            HeaderCaptionLine2 = null;
            IsEnrolled = false;
            ConfirmationNumber = null;
            TicketCountAwarded = -1;
            IsOnWaitList = false;
            ExpectedResponseInterval = null;
            BodyImage1 = null;
            BodyImage2 = null;
            FooterCaptionLine1 = null;
            FooterCaptionLine2 = null;
        }
        public void DBRequestTicketsToEvent(string mobile, int eventID, int optionReferenceID, int ticketCountRequested)
        {
            try
            {
                EventID = eventID;
                OptionReferenceID = optionReferenceID;
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                spParams.Add(new SqlParameter("@EventID", eventID));
                spParams.Add(new SqlParameter("@OptionReferenceID", optionReferenceID));
                spParams.Add(new SqlParameter("@TicketCountRequested", ticketCountRequested));

                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);
                HeaderCaptionLine1 = result.Tables[0].Rows[0]["HeaderCaptionLine1"].ToString();
                HeaderCaptionLine2 = result.Tables[0].Rows[0][""].ToString();
                IsEnrolled = Convert.ToBoolean(result.Tables[0].Rows[0][""].ToString());
                ConfirmationNumber = result.Tables[0].Rows[0][""].ToString();
                MemoryStream bodyImage1MS = new MemoryStream((byte[])result.Tables[0].Rows[0][""]);
                byte[] bodyImage1Byte = bodyImage1MS.ToArray();
                BodyImage1 = bodyImage1Byte;

                MemoryStream bodyImage2MS = new MemoryStream((byte[])result.Tables[0].Rows[0][""]);
                byte[] bodyImage2Byte = bodyImage2MS.ToArray();
                BodyImage2 = bodyImage2Byte;

                FooterCaptionLine1 = result.Tables[0].Rows[0][""].ToString();
                FooterCaptionLine2 = result.Tables[0].Rows[0][""].ToString();

            }
            catch (SqlException ex)
            {
                string errorMessage = ex.Message;
                RemoveData();
            }
        }
    }

    [Serializable]
    public class PurchaseTicketsToEventWithPointsReturn : Default
    {
        private int eventID;
        private int optionReferenceID;
        private string headerCaptionLine1;
        private string headerCaptionLine2;
        private bool isPurchaseSuccessful;
        private float newPointsBalance;
        private bool isEnrolled;
        private string confirmationNumber;
        private int ticketCountAwarded;
        private bool isOnWaitList;
        private string expectedResponseInterval;
        private byte[] bodyImage1;
        private byte[] bodyImage2;
        private string footerCaptionLine1;
        private string footerCaptionLine2;
        #region PUBLICS
        public int EventID
        {
            get
            {
                return eventID;
            }

            set
            {
                eventID = value;
            }
        }

        public int OptionReferenceID
        {
            get
            {
                return optionReferenceID;
            }

            set
            {
                optionReferenceID = value;
            }
        }

        public string HeaderCaptionLine1
        {
            get
            {
                return headerCaptionLine1;
            }

            set
            {
                headerCaptionLine1 = value;
            }
        }

        public string HeaderCaptionLine2
        {
            get
            {
                return headerCaptionLine2;
            }

            set
            {
                headerCaptionLine2 = value;
            }
        }

        public bool IsPurchaseSuccessful
        {
            get
            {
                return isPurchaseSuccessful;
            }

            set
            {
                isPurchaseSuccessful = value;
            }
        }

        public float NewPointsBalance
        {
            get
            {
                return newPointsBalance;
            }

            set
            {
                newPointsBalance = value;
            }
        }

        public bool IsEnrolled
        {
            get
            {
                return isEnrolled;
            }

            set
            {
                isEnrolled = value;
            }
        }

        public string ConfirmationNumber
        {
            get
            {
                return confirmationNumber;
            }

            set
            {
                confirmationNumber = value;
            }
        }

        public int TicketCountAwarded
        {
            get
            {
                return ticketCountAwarded;
            }

            set
            {
                ticketCountAwarded = value;
            }
        }

        public bool IsOnWaitList
        {
            get
            {
                return isOnWaitList;
            }

            set
            {
                isOnWaitList = value;
            }
        }

        public string ExpectedResponseInterval
        {
            get
            {
                return expectedResponseInterval;
            }

            set
            {
                expectedResponseInterval = value;
            }
        }

        public byte[] BodyImage1
        {
            get
            {
                return bodyImage1;
            }

            set
            {
                bodyImage1 = value;
            }
        }

        public byte[] BodyImage2
        {
            get
            {
                return bodyImage2;
            }

            set
            {
                bodyImage2 = value;
            }
        }

        public string FooterCaptionLine1
        {
            get
            {
                return footerCaptionLine1;
            }

            set
            {
                footerCaptionLine1 = value;
            }
        }

        public string FooterCaptionLine2
        {
            get
            {
                return footerCaptionLine2;
            }

            set
            {
                footerCaptionLine2 = value;
            }
        }
        #endregion
        private void RemoveData()
        {
            EventID = -1;
            OptionReferenceID = -1;
            HeaderCaptionLine1 = null;
            HeaderCaptionLine2 = null;
            IsPurchaseSuccessful = false;
            NewPointsBalance = -1;
            IsEnrolled = false;
            ConfirmationNumber = null;
            TicketCountAwarded = -1;
            IsOnWaitList = false;
            ExpectedResponseInterval = null;
            BodyImage1 = null;
            BodyImage2 = null;
            FooterCaptionLine1 = null;
            FooterCaptionLine2 = null;
        }
        public void DBPurchaseTicketsToEventWithPoints(string mobile, int eventID, int optionReferenceID, int ticketCountRequested)
        {
            try
            {
                EventID = eventID;
                OptionReferenceID = optionReferenceID;
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                spParams.Add(new SqlParameter("@EventID", eventID));
                spParams.Add(new SqlParameter("@OptionReferenceID", optionReferenceID));
                spParams.Add(new SqlParameter("@TicketCountRequested", ticketCountRequested));

                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);
                HeaderCaptionLine1 = result.Tables[0].Rows[0]["HeaderCaptionLine1"].ToString();
                HeaderCaptionLine2 = result.Tables[0].Rows[0][""].ToString();
                IsPurchaseSuccessful = Convert.ToBoolean(result.Tables[0].Rows[0][""].ToString());
                NewPointsBalance = float.Parse(result.Tables[0].Rows[0][""].ToString());
                IsEnrolled = Convert.ToBoolean(result.Tables[0].Rows[0][""].ToString());
                ConfirmationNumber = result.Tables[0].Rows[0][""].ToString();
                TicketCountAwarded = Convert.ToInt32(result.Tables[0].Rows[0][""].ToString());
                IsOnWaitList = Convert.ToBoolean(result.Tables[0].Rows[0][""].ToString());
                ExpectedResponseInterval = result.Tables[0].Rows[0][""].ToString();

                MemoryStream bodyImage1MS = new MemoryStream((byte[])result.Tables[0].Rows[0][""]);
                byte[] bodyImage1Byte = bodyImage1MS.ToArray();
                BodyImage1 = bodyImage1Byte;

                MemoryStream bodyImage2MS = new MemoryStream((byte[])result.Tables[0].Rows[0][""]);
                byte[] bodyImage2Byte = bodyImage2MS.ToArray();
                BodyImage2 = bodyImage2Byte;

                FooterCaptionLine1 = result.Tables[0].Rows[0][""].ToString();
                FooterCaptionLine2 = result.Tables[0].Rows[0][""].ToString();

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