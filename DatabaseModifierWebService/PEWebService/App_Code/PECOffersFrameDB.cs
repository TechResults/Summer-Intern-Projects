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
namespace PE.OffersFrameDB
{
    #region My Offers Screen
    [Serializable]
    public class GetOffersScreenWrapperReturn : Default
    {
        private string line1Caption;
        private string line1Data;
        private string line2Caption;
        private string line2Data;
        private List<OfferButton> offerButtons;

        private void RemoveData()
        {
            Line1Caption = null;
            Line1Data = null;
            Line2Caption = null;
            Line2Data = null;
            OfferButtons = null;
        }
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

        public List<OfferButton> OfferButtons
        {
            get
            {
                return offerButtons;
            }

            set
            {
                offerButtons = value;
            }
        }

        public class OfferButton
        {
            private string buttonCaption;
            private int buttonOrdPos;
            private int buttonOfferID;
            private byte[] buttonImage;

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

            public int ButtonOfferID
            {
                get
                {
                    return buttonOfferID;
                }

                set
                {
                    buttonOfferID = value;
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
        }
        #endregion
        public void DBGetOffersScreenWrapper(string mobile)
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
                            OfferButton newButton = new OfferButton();
                            newButton.ButtonCaption = buttonDS.Tables[0].Rows[i]["ButtonCaption"].ToString();
                            newButton.ButtonOrdPos = Convert.ToInt32(buttonDS.Tables[0].Rows[i]["ButtonOrdPos"].ToString());
                            newButton.ButtonOfferID = Convert.ToInt32(buttonDS.Tables[0].Rows[i]["ButtonOfferID"].ToString());
                            MemoryStream ms = new MemoryStream((byte[])buttonDS.Tables[0].Rows[i]["Image"]);
                            byte[] bytes = ms.ToArray();
                            newButton.ButtonImage = bytes;
                            OfferButtons.Add(newButton);
                        }
                    }
                    else
                    {
                        OfferButtons = null;
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
    public class GetOfferDetailsReturn : Default
    {
        private int offerID;
        private string headerCaptionLine1;
        private string headerCaptionLine2;
        private byte[] offerLargeImage;
        private string offerCaption;
        private bool offerBarcodeDisplayed;
        private byte[] offerBarcode;
        private bool displayOptions;
        private List<Option> offerOptions;
        private string footerCaptionLine1;
        private string footerCaptionLine2;
        #region Publics
        public int OfferID
        {
            get
            {
                return offerID;
            }

            set
            {
                offerID = value;
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

        public byte[] OfferLargeImage
        {
            get
            {
                return offerLargeImage;
            }

            set
            {
                offerLargeImage = value;
            }
        }

        public string OfferCaption
        {
            get
            {
                return offerCaption;
            }

            set
            {
                offerCaption = value;
            }
        }

        public bool OfferBarcodeDisplayed
        {
            get
            {
                return offerBarcodeDisplayed;
            }

            set
            {
                offerBarcodeDisplayed = value;
            }
        }

        public byte[] OfferBarcode
        {
            get
            {
                return offerBarcode;
            }

            set
            {
                offerBarcode = value;
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

        public List<Option> OfferOptions
        {
            get
            {
                return offerOptions;
            }

            set
            {
                offerOptions = value;
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
        public class Option
        {
            private string optionCaption;
            private byte[] optionImage;
            private bool optionExecutable;
            private int optionReferenceID;
            #region Public
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
            #endregion
        }
        private void RemoveData()
        {
            OfferID = -1;
            HeaderCaptionLine1 = null;
            HeaderCaptionLine2 = null;
            OfferLargeImage = null;
            OfferCaption = null;
            OfferBarcodeDisplayed = false;
            OfferBarcode = null;
            DisplayOptions = false;
            OfferOptions = null;
            FooterCaptionLine1 = null;
            FooterCaptionLine2 = null;
        }

        public void DBGetOfferDetails(string mobile, int offerID)
        {
            try
            {
                OfferID = offerID;
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                spParams.Add(new SqlParameter("@OfferID", OfferID));

                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);

                if (result.Tables[0].Rows.Count > 0)
                {
                    HeaderCaptionLine1 = result.Tables[0].Rows[0][""].ToString();
                    HeaderCaptionLine2 = result.Tables[0].Rows[0][""].ToString();
                    OfferCaption = result.Tables[0].Rows[0][""].ToString();
                    FooterCaptionLine1 = result.Tables[0].Rows[0][""].ToString();
                    FooterCaptionLine2 = result.Tables[0].Rows[0][""].ToString();
                    OfferID = Convert.ToInt32(result.Tables[0].Rows[0][""].ToString());
                    DisplayOptions = Convert.ToBoolean(result.Tables[0].Rows[0][""].ToString());

                    MemoryStream ms = new MemoryStream((byte[])result.Tables[0].Rows[0][""]);
                    byte[] bytes = ms.ToArray();
                    OfferLargeImage = bytes;

                    MemoryStream barcodeMS = new MemoryStream((byte[])result.Tables[0].Rows[0][""]);
                    byte[] barcodeBytes = barcodeMS.ToArray();
                    OfferBarcode = barcodeBytes;

                    if (DisplayOptions)
                    {
                        DataSet optionDS = new DataSet();
                        List<SqlParameter> optionParams = new List<SqlParameter>();
                        optionParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                        optionParams.Add(new SqlParameter("@OfferID", OfferID));

                        optionDS = DataAcess.ExecuteQuerySP("PEC.TODO", optionParams);
                        if (optionDS.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < optionDS.Tables[0].Rows.Count; i++)
                            {
                                Option newOption = new Option();
                                newOption.OptionCaption = result.Tables[0].Rows[i][""].ToString();
                                MemoryStream optionMS = new MemoryStream((byte[])result.Tables[0].Rows[i][""]);
                                byte[] optionBytes = optionMS.ToArray();
                                newOption.OptionImage = optionBytes;
                                newOption.OptionExecutable = Convert.ToBoolean(result.Tables[0].Rows[i][""].ToString());
                                newOption.OptionReferenceID = Convert.ToInt32(result.Tables[0].Rows[i][""].ToString());

                                OfferOptions.Add(newOption);
                            }
                        }
                        else
                        {
                            OfferOptions = null;
                        }
                    }
                    else
                    {
                        OfferOptions = null;
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
    public class ExecuteRedemptionOptionReturn : Default
    {
        private int offerID;
        private int optionReferenceID;
        private bool redemptionExecutionSucess;
        private string headerCaptionLine1;
        private string headerCaptionLine2;
        private string bodyCaptionLine1;
        private string bodyCaptionLine2;
        private byte[] bodyImage1;
        private byte[] bodyImage2;
        private string footerCaptionLine1;
        private string footerCaptionLine2;
        #region Publics
        public int OfferID
        {
            get
            {
                return offerID;
            }

            set
            {
                offerID = value;
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

        public bool RedemptionExecutionSucess
        {
            get
            {
                return redemptionExecutionSucess;
            }

            set
            {
                redemptionExecutionSucess = value;
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

        public string BodyCaptionLine1
        {
            get
            {
                return bodyCaptionLine1;
            }

            set
            {
                bodyCaptionLine1 = value;
            }
        }

        public string BodyCaptionLine2
        {
            get
            {
                return bodyCaptionLine2;
            }

            set
            {
                bodyCaptionLine2 = value;
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
            OfferID = -1;
            OptionReferenceID = -1;
            RedemptionExecutionSucess = false;
            HeaderCaptionLine1 = null;
            BodyCaptionLine1 = null;
            HeaderCaptionLine2 = null;
            BodyCaptionLine2 = null;
            FooterCaptionLine1 = null;
            FooterCaptionLine2 = null;
            BodyImage1 = null;
            BodyImage2 = null;
        }
        public void DBExecuteRedemptionOption(string mobile, int offerID, int optionReferenceID)
        {
            try
            {
                OfferID = offerID;
                OptionReferenceID = optionReferenceID;
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                spParams.Add(new SqlParameter("@OfferID", OfferID));
                spParams.Add(new SqlParameter("@OptionReferenceID", OptionReferenceID));

                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);

                if (result.Tables[0].Rows.Count > 0)
                {
                    RedemptionExecutionSucess = true;
                    HeaderCaptionLine1 = result.Tables[0].Rows[0][""].ToString();
                    BodyCaptionLine1 = result.Tables[0].Rows[0][""].ToString();
                    HeaderCaptionLine2 = result.Tables[0].Rows[0][""].ToString();
                    BodyCaptionLine2 = result.Tables[0].Rows[0][""].ToString();
                    FooterCaptionLine1 = result.Tables[0].Rows[0][""].ToString();
                    FooterCaptionLine2 = result.Tables[0].Rows[0][""].ToString();

                    MemoryStream ms = new MemoryStream((byte[])result.Tables[0].Rows[0][""]);
                    byte[] bytes = ms.ToArray();
                    if (bytes != null)
                    {
                        BodyImage1 = bytes;
                    }
                    else
                    {
                        BodyImage1 = null;
                    }

                    MemoryStream bodyImage2MS = new MemoryStream((byte[])result.Tables[0].Rows[0][""]);
                    byte[] bodyImage2Bytes = bodyImage2MS.ToArray();
                    if (bodyImage2Bytes != null)
                    {
                        BodyImage2 = bodyImage2Bytes;
                    }
                    else
                    {
                        BodyImage2 = null;
                    }
                    BodyImage2 = bodyImage2Bytes;

                }
                else
                {
                    RemoveData();
                    RedemptionExecutionSucess = false;
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
    public class ViewOfferRedemptionHistoryReturn : Default
    {
        private string headerCaptionLine1;
        private string headerCaptionLine2;
        private bool offerHistoryExists;
        private List<Offer> offerHistory;
        private string footerCaptionLine1;
        private string footerCaptionLine2;
        #region Publics
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

        public bool OfferHistoryExists
        {
            get
            {
                return offerHistoryExists;
            }

            set
            {
                offerHistoryExists = value;
            }
        }

        public List<Offer> OfferHistory
        {
            get
            {
                return offerHistory;
            }

            set
            {
                offerHistory = value;
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

        public class Offer
        {
            private string description;
            private string value;
            private DateTime expiresDate;
            private DateTime issuedDate;
            private string prizeType;
            private bool isExpired;
            private bool isRedeemed;
            private bool hasBarcode;
            private byte[] barcode;
            #region Publics
            public string Description
            {
                get
                {
                    return description;
                }

                set
                {
                    description = value;
                }
            }

            public string Value
            {
                get
                {
                    return value;
                }

                set
                {
                    this.value = value;
                }
            }

            public DateTime ExpiresDate
            {
                get
                {
                    return expiresDate;
                }

                set
                {
                    expiresDate = value;
                }
            }

            public DateTime IssuedDate
            {
                get
                {
                    return issuedDate;
                }

                set
                {
                    issuedDate = value;
                }
            }

            public string PrizeType
            {
                get
                {
                    return prizeType;
                }

                set
                {
                    prizeType = value;
                }
            }

            public bool IsExpired
            {
                get
                {
                    return isExpired;
                }

                set
                {
                    isExpired = value;
                }
            }

            public bool IsRedeemed
            {
                get
                {
                    return isRedeemed;
                }

                set
                {
                    isRedeemed = value;
                }
            }

            public bool HasBarcode
            {
                get
                {
                    return hasBarcode;
                }

                set
                {
                    hasBarcode = value;
                }
            }

            public byte[] Barcode
            {
                get
                {
                    return barcode;
                }

                set
                {
                    barcode = value;
                }
            }

            #endregion

        }

        private void RemoveData()
        {
            HeaderCaptionLine1 = null;
            HeaderCaptionLine2 = null;
            FooterCaptionLine1 = null;
            FooterCaptionLine2 = null;
            OfferHistory = null;
            OfferHistoryExists = false;
        }
        //SQL DB Retrieval Function:
        public void DBViewOfferRedemptionHistoryReturn(string mobile)
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
                    HeaderCaptionLine1 = result.Tables[0].Rows[0]["HeaderCaption"].ToString();
                    HeaderCaptionLine2 = result.Tables[0].Rows[0]["HeaderCaptionLine2"].ToString();
                    OfferHistoryExists = Convert.ToBoolean(result.Tables[0].Rows[0]["OfferHistoryExists"].ToString());
                    FooterCaptionLine1 = result.Tables[0].Rows[0]["HeaderCaption"].ToString();
                    FooterCaptionLine2 = result.Tables[0].Rows[0]["HeaderCaption"].ToString();
                    if (OfferHistoryExists)
                    {
                        DataSet oHDS = new DataSet();
                        List<SqlParameter> offerHistoryParams = new List<SqlParameter>();
                        offerHistoryParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));

                        oHDS = DataAcess.ExecuteQuerySP("PEC.TODO", offerHistoryParams);
                        for (int i = 0; i < oHDS.Tables[0].Rows.Count; i++)
                        {
                            Offer newOffer = new Offer();
                            newOffer.Description = oHDS.Tables[0].Rows[i][""].ToString();
                            newOffer.Value = oHDS.Tables[0].Rows[i][""].ToString();
                            newOffer.ExpiresDate = Convert.ToDateTime(oHDS.Tables[0].Rows[i][""].ToString());
                            newOffer.IssuedDate = Convert.ToDateTime(oHDS.Tables[0].Rows[i][""].ToString());
                            newOffer.PrizeType = oHDS.Tables[0].Rows[i][""].ToString();
                            newOffer.IsExpired = Convert.ToBoolean(oHDS.Tables[0].Rows[i][""].ToString());
                            newOffer.IsRedeemed = Convert.ToBoolean(oHDS.Tables[0].Rows[i][""].ToString());
                            newOffer.HasBarcode = Convert.ToBoolean(oHDS.Tables[0].Rows[i][""].ToString());
                            if (newOffer.HasBarcode)
                            {
                                MemoryStream ms = new MemoryStream((byte[])oHDS.Tables[0].Rows[i][""]);
                                byte[] bytes = ms.ToArray();
                                newOffer.Barcode = bytes;
                            }
                            else
                            {
                                newOffer.Barcode = null;
                            }

                            OfferHistory.Add(newOffer);
                        }
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

            }
        }
    }
    #endregion
}