using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace TotalPromo
{
    [Serializable]
    /// <summary>
    /// Summary description for DrawingGroup
    /// </summary>
    public class DrawingGroup
    {
        #region Instance Members
        Int32 _promoId;
        Int32 _bucketId;
        SqlDateTime _drawDate = SqlDateTime.MinValue;
        SqlDateTime _nextDrawDate = SqlDateTime.MinValue;
        Promo _promo;
        Drum _drum;
        List<Drawing> _drawings = new List<Drawing>();
        #endregion

        #region Properties
        public Int32 PromoId
        {
            get { return _promoId; }
            set { _promoId = value; }
        }
        public Int32 BucketId
        {
            get { return _bucketId; }
            set { _bucketId = value; }
        }
        public SqlDateTime DrawDate
        {
            get { return _drawDate; }
            set { _drawDate = value; }
        }
        public SqlDateTime NextDrawDate
        {
            get { return _nextDrawDate; }
            set { _nextDrawDate = value; }
        }
        public Promo Promo
        {
            get { return _promo; }
            set { _promo = value; }
        }
        public Drum Drum
        {
            get { return _drum; }
            set { _drum = value; }
        }
        public List<Drawing> Drawings
        {
            get { return _drawings; }
        }

        private SqlDateTime checkinStartTime;

        public SqlDateTime CheckinStartTime
        {
            get { return checkinStartTime; }
            set { checkinStartTime = value; }
        }

        private SqlDateTime checkinEndTime;

        public SqlDateTime CheckinEndTime
        {
            get { return checkinEndTime; }
            set { checkinEndTime = value; }
        }

        #endregion


        #region Instance Methods
        public DrawingGroup()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Loads into DrawingGroup object for the given promoId
        /// </summary>
        /// <param name="promoId"></param>
        /// <returns></returns>
        public DrawingGroup LoadSelected(Int32 promoId)
        {
            try
            {
                List<SqlParameter> spParams = new List<SqlParameter>();
                
                // PROMOTION_DRAWING_EVENT_GetNextAvailableForPromo
                spParams.Add(new SqlParameter("@promoid", promoId));
                DataRow drPromo = DataAccess.GetDataRow("PROMOTION_DRAWING_EVENT_GetNextAvailableForPromo", spParams);
                if (null != drPromo)
                {
                    this.PromoId = promoId;
                    this.BucketId = drPromo.IsNull("BucketID") ? 0 : Convert.ToInt32(drPromo["BucketID"]);
                    this.DrawDate = drPromo.IsNull("DrawingDateTime") ? SqlDateTime.Null : Convert.ToDateTime(drPromo["DrawingDateTime"]);
                    this.checkinStartTime = drPromo.IsNull("CheckinStartTime") ? SqlDateTime.Null : Convert.ToDateTime(drPromo["CheckinStartTime"]);
                    this.checkinEndTime = drPromo.IsNull("CheckinEndTime") ? SqlDateTime.Null : Convert.ToDateTime(drPromo["CheckinEndTime"]);
                }

                // PROMOTION_DRAWING_EVENT_GetNextAvailable
                spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@promoid", this.PromoId));
                spParams.Add(new SqlParameter("@bucketid", this.BucketId));
                spParams.Add(new SqlParameter("@drawingdate", this.DrawDate));
                DataRow drNext = DataAccess.GetDataRow("PROMOTION_DRAWING_EVENT_GetNextAvailable_v2", spParams);
                if (null != drNext)
                {
                    this.NextDrawDate = drNext.IsNull("DrawingDateTime") ? SqlDateTime.MinValue : Convert.ToDateTime(drNext["DrawingDateTime"]);
                }
                // PROMOTION_DRAWING_GetNextForEvent ??????
                // TODO - Talk to Rex before doing this SP call

                //// PROMOTION_GetByKey
                Promo promo = new Promo();
                this._promo = promo.LoadSelected(this.PromoId);
                
                //// PROMOTION_DRUM_GetByKey
                Drum drum = new Drum();
                this._drum = drum.LoadSelected(this.PromoId, this.BucketId);

                //// PROMOTION_DRAWING_ListByEvent
                spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@promoid", this.PromoId));
                spParams.Add(new SqlParameter("@bucketid", this.BucketId));
                spParams.Add(new SqlParameter("@drawingdatetime", this.DrawDate));
                DataTable dtDrawings = DataAccess.ExecuteQuerySP("PROMOTION_DRAWING_ListByEvent_v2", spParams).Tables[0];
                foreach (DataRowView row in dtDrawings.DefaultView)
                {
                    Drawing drawing = new Drawing();
                    drawing.Id = Int32.Parse(row["DrawingId"].ToString());
                    drawing.PromoId = Int32.Parse(row["PromotionId"].ToString());
                    drawing.BucketId = Int32.Parse(row["BucketId"].ToString());
                    drawing.PrizeId = Int32.Parse(row["PrizeId"].ToString());
                    drawing.Prize = row["Prize"].ToString();
                    drawing.PrizeValue = decimal.Parse(row["PrizeValue"].ToString());
                    if (row["WinnerId"].ToString() != String.Empty)
                        drawing.WinnerId = Int32.Parse(row["WinnerId"].ToString());
                    if (row["TimeOut"].ToString() != String.Empty)
                        drawing.TimeOut = DateTime.Parse(row["TimeOut"].ToString());
                    if (row["TimeStarted"].ToString() != String.Empty)
                        drawing.TimeStarted = DateTime.Parse(row["TimeStarted"].ToString());
                    if (row["PlayerName"].ToString() != String.Empty)
                        drawing.Winner = row["PlayerName"].ToString();
                    if (row["PlayerAccountNum"].ToString() != String.Empty)
                        drawing.PlayerAccountNum = row["PlayerAccountNum"].ToString();
                    if (row["PlayerDOB"].ToString() != String.Empty)
                        drawing.WinnerDob = DateTime.Parse(row["PlayerDOB"].ToString());
                    if (row["CMSPlayerID"].ToString() != String.Empty)
                        drawing.CMSPlayerID = row["CMSPlayerID"].ToString();
                    if (row["TicketNumber"].ToString() != String.Empty)
                        drawing.TicketNumber = Int32.Parse(row["TicketNumber"].ToString());
                    if (row["Validated"].ToString() == "True")
                        drawing.Validated = true;
                    else
                        drawing.Validated = false;
                    this._drawings.Add(drawing);
                }


                //DataTable dtDrawings = DataAccess.ExecuteQuerySP("PROMOTION_DRAWING_ListByEvent", spParams).Tables[0];
                //if (null != dtDrawings)
                //{
                //    foreach (DataRow drDrawing in dtDrawings.Rows)
                //    {
                //        Drawing drawing = new Drawing();
                //        this._drawings.Add(drawing.LoadSelected(this.PromoId,
                //                                                this.BucketId,
                //                                                Convert.ToInt32(drDrawing["DrawingID"]),
                //                                                Convert.ToInt32(drDrawing["PrizeID"])));
                //    }
                //}
            }
            catch (Exception ex)
            {

            }
            return this;
        }
        #endregion
    }
}
