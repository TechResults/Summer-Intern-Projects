using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using AccessManagementLib;

[WebService(Namespace = "http://www.techresults.com")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]

public class KeyGenWS : System.Web.Services.WebService
{
    public KeyGenWS()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GenerateKey(string appName, string propertyName, string contractNum)
    {
        string newKey = KeyGen.createPublicKey(appName, propertyName, contractNum);
        return new JavaScriptSerializer().Serialize(newKey);
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GenerateModuleKey(string appName, string propertyName, string contractNum, string moduleName)
    {
        string moduleKey = KeyGen.createModuleKey(appName, propertyName, contractNum, moduleName);
        return new JavaScriptSerializer().Serialize(moduleKey);
    }
}