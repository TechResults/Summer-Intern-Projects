using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;

/// <summary>
/// Summary description for DataAcess
/// </summary>
/// 
namespace PE.DataReturns
{
    class DataAcess
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["PEClient"].ConnectionString;

        private static SqlConnection PEConnection = new SqlConnection(connectionString);

        #region ExecuteNonQuerySP
        /// <summary>
        ///		ExecuteNonQuerySP
        ///		<para>
        ///			Executes a stored procedure but does not return a resultset.
        ///		</para>
        /// </summary>
        /// <param name="spName">Stored procedure name.</param>
        /// <param name="parameters">List of parameters provided for the stored procedure.</param>
        /// <returns>Int32 - return parameter of stored procedure.</returns>
        public static Int32 ExecuteNonQuerySP(String spName, IList parameters)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = null;
            try
            {
                conn = GetConnection();
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                cmd.Connection = conn;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;
                cmd.CommandTimeout = conn.ConnectionTimeout;
                if (parameters != null)
                {
                    foreach (SqlParameter p in parameters)
                    {
                        if (p.Value == null)
                            cmd.Parameters.AddWithValue(p.ParameterName, DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue(p.ParameterName, p.Value);
                    }
                }

                return cmd.ExecuteNonQuery();

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    //conn.Dispose();
                }
                cmd.Dispose();
            }
        }
        #endregion

        #region ExecuteQuerySP
        /// <summary>
        ///		ExecuteNonQuerySP
        ///		<para>
        ///			Executes a stored procedure but and returns a resultset.
        ///		</para>
        /// </summary>
        /// <param name="spName">Stored procedure name.</param>
        /// <param name="parameters">List of parameters provided for the stored procedure.</param>
        /// <returns>Dataset - results of stored procedure.</returns>
        public static DataSet ExecuteQuerySP(String spName, IList parameters)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds;
            SqlDataAdapter dataAdapter;
            SqlConnection conn = null;
            try
            {
                conn = GetConnection();
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                cmd.Connection = conn;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;
                cmd.CommandTimeout = conn.ConnectionTimeout;

                if (parameters != null)
                {
                    foreach (SqlParameter p in parameters)
                    {
                        if (p.Value == null)
                            cmd.Parameters.AddWithValue(p.ParameterName, DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue(p.ParameterName, p.Value);
                    }
                }

                dataAdapter = new SqlDataAdapter(cmd);

                ds = new DataSet();
                //ds.Locale = GlobalizationUtils.CultureInfo;
                //***Needs to be updated for localiztion

                dataAdapter.Fill(ds, spName);


            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    //conn.Dispose();
                }
                cmd.Dispose();
            }
            return ds;
        }

        /// <summary>
        /// Gets only one table row from the first datatable
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="spParams"></param>
        /// <returns></returns>
        public static DataRow GetDataRow(String spName, IList spParams)
        {
            DataSet dsResult = ExecuteQuerySP(spName, spParams);
            DataRow row = null;
            if (dsResult.Tables.Count > 0)
            {
                DataTable dtResult = null;
                dtResult = dsResult.Tables[0];
                if (dtResult.Rows.Count == 0)
                {
                    row = null;
                }
                else
                {
                    row = dtResult.Rows[0];
                }
            }
            return row;
        }
        #endregion

        #region GetConnection
        /// <summary>
        /// Gets static connection object
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
        #endregion

    }
}
