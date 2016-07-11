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

namespace TotalPromo
{
    [Serializable]
    /// <summary>
    /// Summary description for Drawing
    /// </summary>
    public class Drawing
    {
        #region Instance Members
        Int32 _id;
        Int32 _promoId;
        Int32 _bucketId;
        Int32 _prizeId;
        String _prize;
        Int32 _winnerId;
        String _winner;
        SqlDateTime _winnerDob = SqlDateTime.MinValue;
        SqlDateTime _timeOut = SqlDateTime.MinValue;
        String _status;
        private Int32 ticketNumber;
        #endregion

        #region Properties
        public Int32 TicketNumber
        {
            get { return ticketNumber; }
            set { ticketNumber = value; }
        }
        public Int32 Id
        {
            get { return _id; }
            set { _id = value; }
        }
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
        public Int32 PrizeId
        {
            get { return _prizeId; }
            set { _prizeId = value; }
        }
        public String Prize
        {
            get { return _prize; }
            set { _prize = value; }
        }
        private decimal _prizeValue;

        public decimal PrizeValue
        {
            get { return _prizeValue; }
            set { _prizeValue = value; }
        }

        public Int32 WinnerId
        {
            get { return _winnerId; }
            set { _winnerId = value; }
        }
        public String Winner
        {
            get { return _winner; }
            set { _winner = value; }
        }
        public SqlDateTime WinnerDob
        {
            get { return _winnerDob; }
            set { _winnerDob = value; }
        }
        public SqlDateTime TimeOut
        {
            get { return _timeOut; }
            set { _timeOut = value; }
        }
        public String Status
        {
            get { return _status; }
            set { _status = value; }
        }

        private String _cmsPlayerID;

        public String CMSPlayerID
        {
            get { return _cmsPlayerID; }
            set { _cmsPlayerID = value; }
        }
        private string _playerAccountNum;

        public string PlayerAccountNum
        {
            get { return _playerAccountNum; }
            set { _playerAccountNum = value; }
        }

        private bool _validated;

        public bool Validated
        {
            get { return _validated; }
            set { _validated = value; }
        }
        private SqlDateTime _timeStarted;

        public SqlDateTime TimeStarted
        {
            get { return _timeStarted; }
            set { _timeStarted = value; }
        }

        private string afterCountdown;

        public string AfterCountdown
        {
            get { return afterCountdown; }
            set { afterCountdown = value; }
        }

        #endregion

        #region Instance Methods
        public Drawing()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public Drawing(Int32 drawingId, Int32 promotionId, Int32 bucketId, Int32 prizeId, string prize, decimal prizeValue, Int32 winnerId, SqlDateTime timeOut, string winner, SqlDateTime winnerDOB, string cmsPlayerId, bool validated, string playerAccountNum, SqlDateTime timeStarted)
        {
            this._id = drawingId;
            this._promoId = promotionId;
            this._bucketId = bucketId;
            this._prizeId = prizeId;
            this._prize = prize;
            this._prizeValue = prizeValue;
            this._winnerId = winnerId;
            this._winner = winner;
            this._winnerDob = winnerDOB;
            this._cmsPlayerID = cmsPlayerId;
            this._timeOut = timeOut;
            this._validated = validated;
            this._playerAccountNum = playerAccountNum;
            this._timeStarted = timeStarted;
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Loads into promo object for the given promoId
        /// </summary>
        /// <param name="promoId"></param>
        /// <returns></returns>
        public Drawing LoadSelected(Int32 promoId, Int32 bucketId, Int32 drawingId, Int32 prizeId)
        {
            try 
            {
                this.Id = drawingId;
                this.PromoId = promoId;
                this.BucketId = bucketId;

                List<SqlParameter> spParams = new List<SqlParameter>();
                // PRIZE_GetByKey
                spParams.Add(new SqlParameter("@PrizeID", prizeId));
                DataRow drPrize = DataAccess.GetDataRow("PRIZE_GetByKey", spParams);
                if (null != drPrize)
                {
                    this.PrizeId = prizeId;
                    this.Prize = drPrize.IsNull("PrizeName") ? String.Empty : Convert.ToString(drPrize["PrizeName"]);
                    this.PrizeValue = drPrize.IsNull("CashRedemptionValue") ? 0 : Convert.ToDecimal(drPrize["CashRedemptionValue"].ToString());
                }

                // PROMOTION_DRAWING_GetCurrentWinner
                spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@promoid", promoId));
                spParams.Add(new SqlParameter("@bucketid", bucketId));
                spParams.Add(new SqlParameter("@drawingid", drawingId));
                DataRow drWinner = DataAccess.GetDataRow("PROMOTION_DRAWING_GetCurrentWinner", spParams);
                if (null != drWinner)
                {
                    this.WinnerId = drWinner.IsNull("WinnerID") ? 0 : Convert.ToInt32(drWinner["WinnerID"]);
                    this.TimeOut = drWinner.IsNull("Timeout") ? SqlDateTime.MinValue : Convert.ToDateTime(drWinner["Timeout"]);
                    this._playerAccountNum = drWinner["PlayerAccountNum"].ToString();
                    if (drWinner["validated"].ToString() == "0")
                        this._validated = false;
                    else
                        this._validated = true;
                }
                // PLAYER_GetByKey
                spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@ACCOUNTNUM", drWinner["PlayerAccountNum"].ToString()));
                spParams.Add(new SqlParameter("@CMSPLAYERID", drWinner["CMSPlayerID"].ToString()));
                DataRow drPlayer = DataAccess.GetDataRow("PLAYER_GetByKey", spParams);

                // TO DO -- Dont know how ro get the below fields
                if (!drPlayer.IsNull("PlayerName"))
                    this.Winner = drPlayer["PlayerName"].ToString();
                if (!drPlayer.IsNull("PlayerDOB"))
                    this.WinnerDob = DateTime.Parse(drPlayer["PlayerDOB"].ToString());
                else
                    this.WinnerDob = SqlDateTime.MinValue;
                if (!drPlayer.IsNull("CMSPlayerID"))
                    this._cmsPlayerID = drPlayer["CMSPlayerID"].ToString();
            }
            catch (Exception ex)
            {

            }
            return this;
        }
        #endregion
    }
}
