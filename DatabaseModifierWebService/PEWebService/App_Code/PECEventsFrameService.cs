using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using PE.DataReturn;
using PE.EventsFrameDB;

/// <summary>
/// Summary description for PECEventsFrameService
/// </summary>

[WebService(Namespace = "playerelite.com.au")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class PECEventsFrameService : System.Web.Services.WebService
{

    public PECEventsFrameService()
    {
    }

    #region My Events Screen
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetEventsScreenWallpaper(string mobile, string userToken)
    {
        GetEventsScreenWrapperReturn currentUser = new GetEventsScreenWrapperReturn();
        currentUser.checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser.DBGetEventsScreenWrapper(mobile);
            currentUser.validToken = true;
        }
        else
        {
            currentUser = null;
            currentUser.validToken = false;
        }
        return new JavaScriptSerializer().Serialize(currentUser);
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetEventDetails(string mobile, string userToken, int eventID)
    {
        GetEventDetailsReturn currentUser = new GetEventDetailsReturn();
        currentUser.checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser.DBGetEventDetails(mobile, eventID);
            currentUser.validToken = true;
        }
        else
        {
            currentUser = null;
            currentUser.validToken = false;
        }
        return new JavaScriptSerializer().Serialize(currentUser);
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string EnrollGuestInEvent(string mobile, string userToken, int eventID, int optionReferenceID)
    {
        EnrollGuestInEventReturn currentUser = new EnrollGuestInEventReturn();
        currentUser.checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser.DBEnrollGuestInEvent(mobile, eventID, optionReferenceID);
            currentUser.validToken = true;
        }
        else
        {
            currentUser = null;
            currentUser.validToken = false;
        }
        return new JavaScriptSerializer().Serialize(currentUser);
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string RequestTicketsToEvent(string mobile, string userToken, int eventID, int optionReferenceID, int numberOfTickets)
    {
        RequestTicketsToEventReturn currentUser = new RequestTicketsToEventReturn();
        currentUser.checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser.DBRequestTicketsToEvent(mobile, eventID, optionReferenceID, numberOfTickets);
            currentUser.validToken = true;
        }
        else
        {
            currentUser = null;
            currentUser.validToken = false;
        }
        return new JavaScriptSerializer().Serialize(currentUser);
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string PurchaseTicketsToEventWithPoints(string mobile, string userToken, int eventID, int optionReferenceID, int ticketCountRequested)
    {
        PurchaseTicketsToEventWithPointsReturn currentUser = new PurchaseTicketsToEventWithPointsReturn();
        currentUser.checkSession(mobile, userToken);
        if (currentUser.validToken)
        {
            currentUser.DBPurchaseTicketsToEventWithPoints(mobile, eventID, optionReferenceID, ticketCountRequested);
            currentUser.validToken = true;
        }
        else
        {
            currentUser = null;
            currentUser.validToken = false;
        }
        return new JavaScriptSerializer().Serialize(currentUser);
    }
    #endregion

}
