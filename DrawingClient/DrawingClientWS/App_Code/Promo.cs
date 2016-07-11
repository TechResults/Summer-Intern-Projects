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
    /// Summary description for Promo
    /// </summary>
    public class Promo
    {
        #region Instance Members
        Int32 _id;
        String _name;
        SqlDateTime _startDate;
        SqlDateTime _endDate;
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
        public SqlDateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }
        public SqlDateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }
        #endregion

        #region Instance Methods
        public Promo()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Loads into promo object for the given promoId
        /// </summary>
        /// <param name="promoId"></param>
        /// <returns></returns>
        public Promo LoadSelected(Int32 promoId)
        {
            try
            {
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@PromotionID", promoId));
                DataRow dr = DataAccess.GetDataRow("PROMOTION_GetByKey", spParams);
                if (null != dr)
                {
                    this.Id = dr.IsNull("PromotionID") ? 0 : Convert.ToInt32(dr["PromotionID"]);
                    this.Name = dr.IsNull("PromotionName") ? string.Empty : Convert.ToString(dr["PromotionName"]);
                    this.StartDate = dr.IsNull("StartDate") ? SqlDateTime.Null : Convert.ToDateTime(dr["StartDate"]);
                    this.EndDate = dr.IsNull("EndDate") ? SqlDateTime.Null : Convert.ToDateTime(dr["EndDate"]);
                }
            }
            catch (Exception ex)
            {

            }
            return this;
        }

        /// <summary>
        /// Gets all active promotions
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllActivePromotions()
        {
            return DataAccess.ExecuteQuerySP("PROMOTION_LIST_ACTIVE", null);
        }
        #endregion
    }
}