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
    /// Summary description for Drum
    /// </summary>
    public class Drum
    {
        #region Instance Members
        Int32 _id;
        String _name;
        String _type;
        Int32 _numberOfEntries;
        Int32 _numberOfPlayers;
        SqlDateTime _lastPopulated = SqlDateTime.MinValue;
        #endregion

        #region Properties
        public Int32 Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public String Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public Int32 NumberOfEntries
        {
            get { return _numberOfEntries; }
            set { _numberOfEntries = value; }
        }
        public Int32 NumberOfPlayers
        {
            get { return _numberOfPlayers; }
            set { _numberOfPlayers = value; }
        }
        public SqlDateTime LastPopulated
        {
            get { return _lastPopulated; }
            set { _lastPopulated = value; }
        }
        #endregion

        #region Instance Methods
        public Drum()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Loads into drum object for the given promoId/bucketId
        /// </summary>
        /// <param name="promoId"></param>
        /// <param name="bucketId"></param>
        /// <returns></returns>
        public Drum LoadSelected(Int32 promoId, Int32 bucketId)
        {
            try
            {
                List<SqlParameter> spParams = new List<SqlParameter>();
                // PROMOTION_DRUM_GetByKey
                spParams.Add(new SqlParameter("@promoid", promoId));
                spParams.Add(new SqlParameter("@bucketid", bucketId));
                DataRow drDrum = DataAccess.GetDataRow("PROMOTION_DRUM_GetByKey", spParams);
                if (null != drDrum)
                {
                    this.Id = drDrum.IsNull("DrumID") ? 0 : Convert.ToInt32(drDrum["DrumID"]);
                    this.Name = drDrum.IsNull("BucketHumanReadable") ? string.Empty : Convert.ToString(drDrum["BucketHumanReadable"]);
                    bool isRollup = drDrum.IsNull("IsRollup") ? false : Convert.ToBoolean(drDrum["IsRollup"]);
                    this.Type = (true == isRollup ? "Roll Up" : "Entry");
                }
                // PROMOTION_DRUM_GetEntryCount
                spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@promoid", promoId));
                spParams.Add(new SqlParameter("@bucketid", bucketId));
                DataRow drEntryCount = DataAccess.GetDataRow("PROMOTION_DRUM_GetEntryCount", spParams);
                if (null != drEntryCount)
                {
                    this.NumberOfEntries = drEntryCount.IsNull(0) ? 0 : Convert.ToInt32(drEntryCount[0]);
                    this.LastPopulated = drEntryCount.IsNull("LastPopulated") ? SqlDateTime.MinValue : Convert.ToDateTime(drEntryCount["LastPopulated"]);
                }
                // PROMOTION_DRUM_GetPlayerCount
                spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@promoid", promoId));
                spParams.Add(new SqlParameter("@bucketid", bucketId));
                DataRow drPlayerCount = DataAccess.GetDataRow("PROMOTION_DRUM_GetPlayerCount", spParams);
                if (null != drPlayerCount)
                {
                    this.NumberOfPlayers = drPlayerCount.IsNull(0) ? 0 : Convert.ToInt32(drPlayerCount[0]);
                }
            }
            catch (Exception ex)
            {

            }
            return this;
        }
        #endregion
    }
}
