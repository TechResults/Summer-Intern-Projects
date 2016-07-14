using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;
using PE.DataReturns;
using System.IO;

/// <summary>
/// Summary description for DataReturns
/// </summary>

namespace PE.DataReturn
{
    public class ServerSide
    {
        public static DateTime GetTime()
        {
            DataTable dt = DataAcess.ExecuteQuerySP("DRAW_GET_TIMEATSERVER", null).Tables[0];
            return DateTime.Parse(dt.Rows[0][0].ToString());
        }
        public static string DBGetCMSPlayerID(string mobile)
        {
            string CMSplayerID = "";
            return CMSplayerID;
        }
    }

    [Serializable]
    public class Default
    {
        private bool _validToken;

        public bool validToken
        {
            get { return _validToken; }
            set { _validToken = value; }
        }

        public bool checkSession(string mobile, string userToken)
        {
            throw new NotImplementedException();
            if (true) //valid
            {
                validToken = true;
            }

        }
    }



}
