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
/// Summary description for DrawingClientStatus
/// </summary>
public class DrawingClientStatus
{
    private bool drawingClientInUse;

    public bool DrawingClientInUse
    {
        get { return drawingClientInUse; }
        set { drawingClientInUse = value; }
    }

    private string drawingClientInUseBy;

    public string DrawingClientInUseBy
    {
        get { return drawingClientInUseBy; }
        set { drawingClientInUseBy = value; }
    }

    public DrawingClientStatus()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}
