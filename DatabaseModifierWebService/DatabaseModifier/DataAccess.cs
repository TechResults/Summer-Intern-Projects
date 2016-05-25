using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;
using System.Collections;


namespace DatabaseModifier
{
    class DataAccess
    {

        #region GetHash
        /// <summary>
        ///		Get Hash from SQL Database
        ///		<para>
        ///			Passes a string of a number, returns the user code with that set.
        ///		</para>
        /// </summary>
        /// <param name="spName">Stored procedure name.</param>
        /// <param name="parameters">List of parameters provided for the stored procedure.</param>
        /// <returns>Int32 - return parameter of stored procedure.</returns>
        public static string GetHash(string searchNum)
        {
            string matchingNum = " ";
            var con = ConfigurationManager.ConnectionStrings["EllisTR"].ToString();
            using (SqlConnection myConnection = new SqlConnection(con))
            {
                string oString = "Select UserToken from User where MobileNumber=@searchNum";
                SqlCommand oCmd = new SqlCommand(oString, myConnection);
                
                string addString = "Insert into User ('ExpirationTime') values (1000)";
                SqlCommand addCmd = new SqlCommand(addString, myConnection);

                oCmd.Parameters.AddWithValue("@searchNum", searchNum);
                myConnection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while(oReader.Read())
                    {
                        matchingNum = oReader["UserToken"].ToString();
                    }

                }
                int temp = addCmd.ExecuteNonQuery();
                
                myConnection.Close();
            }
            return matchingNum;
        }
        #endregion
    }
}
