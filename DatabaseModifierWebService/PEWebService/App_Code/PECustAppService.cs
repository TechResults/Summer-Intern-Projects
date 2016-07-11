using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using PE.DataReturn;

/// <summary>
/// CustAppService deals with user login, registration, and loading of assets at launch
/// </summary>
namespace PE.CustApp
{
    [WebService(Namespace = "playerelite.com.au")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class PECustAppService : System.Web.Services.WebService
    {
        int registrationTries;
        public PECustAppService()
        {
            registrationTries = 0;
        }

        #region Application Installation and Setup

        /// <summary>
        /// Function registers new user by passing their phone's mobile number and the pin code assigned by the MCM at the venue
        /// If the pin code entered matches the one stored in the database, isRegistered is set to true. A counter outside of this
        /// method should determine the number of registration tries a user has made. If a user has attempted to register more than three
        /// times, they should be locked out and isLocked should return true.
        /// </summary>
        /// <param name="mobile">User's mobile number passed in as a string with the format "XXX-XXX-XXXX"</param>
        /// <param name="pinCode">User entered pin code. This will be checked against the server's registered PIN</param>
        /// <returns></returns>
        /// IMPLEMENTED
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string RegisterNewUser(string mobile, string pinCode)
        {
            RegisterNewUserResult newUser = new RegisterNewUserResult();
            if (registrationTries <= 3)
            {
                newUser.isLocked = false;
                newUser.DBGetStoredPin(mobile, pinCode);
                if (newUser.isRegistered)
                {
                    newUser.isRegistered = true;
                }
                else
                {
                    registrationTries++;
                }
            }
            else
            {
                newUser.isLocked = true;
            }
            return new JavaScriptSerializer().Serialize(newUser);
        }
        #endregion

        #region Application Launch

        /// <summary>
        /// Method recieves mobile number and sets isRegistered to true or false depending on whether there is a match of the mobile number
        /// that is registered. Method calls GenerateOneWayHash to generate a userToken if the user is sucessfully registered. Additionally,
        /// the configuration fo showing balance on pin is currently true. This implementation of options is not elaborated on in the API DOC.
        /// IMPLEMENTED: YES
        /// DB FUNCTION IMP: YES (With the exception of Generate One Way Hash)
        /// STORED PROCEDURE IMP: NO
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ValidatePhoneRegistered(string mobile)
        {
            ValidatePhoneRegisteredResult currentUser = new ValidatePhoneRegisteredResult();
            currentUser.checkIsRegistered(mobile);
            if (currentUser.IsRegistered)
            {
                currentUser.GenerateOneWayHash(mobile);
                currentUser.IsRegistered = true;
                //Show balances based on configuration (Config is not elaborated by API DOC)
                currentUser.ShowBalancesNoPin = true;
            }
            else
            {
                currentUser.IsRegistered = false;
            }
            return new JavaScriptSerializer().Serialize(currentUser);
        }

        /// <summary>
        /// Show the customer's account balances from one or more balance buckets. Method calls a db function which will get the user's
        /// balance set
        /// </summary>
        /// <param name="mobile">User's mobile number</param>
        /// <param name="userToken">Current generated user token on the client side, used to check current session validity</param>
        /// IMPLEMENTED: YES
        /// DB FUNCTION IMP: YES (With the exception of CheckSession)
        /// STORED PROCEDURE IMP: NO
        /// <returns>Serialized JSON string</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ShowBalancesOnOpeningScreen(string mobile, string userToken)
        {
            ShowBalancesOnOpeningScreenResult currentUser = new ShowBalancesOnOpeningScreenResult();
            currentUser.checkSession(mobile, userToken);
            if (currentUser.validToken)
            {
                currentUser.checkSession(mobile, userToken);
                currentUser.DBGetAccountBalancesSet(mobile);
            }
            else
            {
                currentUser = null;
                currentUser.validToken = false;
            }

            return new JavaScriptSerializer().Serialize(currentUser);
        }

        /// <summary>
        /// Method validates the current user account by pinCode. If the user input pin matches the registered pin a new userToken is 
        /// generated and isValid is set to true. Otherwise, isValid is false and userToekn is an empty string ""
        /// </summary>
        /// <param name="mobile">User's mobile number passed in as a string with the format "XXX-XXX-XXXX"</param>
        /// <param name="userToken">Current generated user token on the client side, used to check current session validity</param>
        /// IMPLEMENTED: YES
        /// DB FUNCTION IMP: YES (With the exception of GenerateOneWayHash)
        /// STORED PROCEDURE IMP: NO
        /// <returns>Serialized JSON string</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ValidateUser(string mobile, string pinNum)
        {
            ValidateUserReturn currentUser = new ValidateUserReturn();
            currentUser.DBGetStoredPin(mobile, pinNum);

            return new JavaScriptSerializer().Serialize(currentUser);

        }


        /// <summary>
        /// Method loads logo from server
        /// </summary>
        /// <param name="mobile">User's mobile number passed in as a string with the format "XXX-XXX-XXXX"</param>
        /// <param name="userToken">Current generated user token on the client side, used to check current session validity</param>
        /// IMPLEMENTED: YES
        /// DB FUNCTION IMP: YES
        /// STORED PROCEDURE IMP: NO
        /// <returns>Serialized JSON string</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string LoadLogo(string mobile, string userToken)
        {
            LoadLogoReturn currentUser = new LoadLogoReturn();
            currentUser.checkSession(mobile, userToken);
            if (currentUser.validToken)
            {
                currentUser.DBGetLogo(mobile);
            }
            else
            {
                currentUser.logo = null;
            }
            return new JavaScriptSerializer().Serialize(currentUser);

        }
        #endregion

    }

}


