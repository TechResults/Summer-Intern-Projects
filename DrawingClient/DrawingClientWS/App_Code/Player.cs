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
/// Summary description for Player
/// </summary>
public class Player
{
    private string playerAccountNum;
    private string cmsPlayerID;
    private string playerName;
    private DateTime playerDOB;
    private Int32 entryId;
    private int winnerId;

    public Player()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int WinnerId
    {
        get { return winnerId; }
        set { winnerId = value; }
    }
	
    
    public string PlayerAccountNum
    {
        get { return playerAccountNum; }
        set { playerAccountNum = value; }
    }


    public string CMSPlayerID
    {
        get { return cmsPlayerID; }
        set { cmsPlayerID = value; }
    }


    public string PlayerName
    {
        get { return playerName; }
        set { playerName = value; }
    }


    public DateTime PlayerDOB
    {
        get { return playerDOB; }
        set { playerDOB = value; }
    }


    public Int32 EntryId
    {
        get { return entryId; }
        set { entryId = value; }
    }


}
