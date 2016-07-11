using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for User
/// </summary>
public class User
{
    private Int32 userID;

    public Int32 UserID
    {
        get { return userID; }
        set { userID = value; }
    }

    private string userIDText;

    public string UserIDText
    {
        get { return userIDText; }
        set { userIDText = value; }
    }


    //MR 12/21/09 Adding LoginStatus to check if an account is locked on login
    private string loginStatus;

    public string LoginStatus
    {
        get { return loginStatus; }
        set { loginStatus = value; }
    }


    private Int32 userLevel;

    public Int32 UserLevel
    {
        get { return userLevel; }
        set { userLevel = value; }
    }

    public User()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}
