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

/* EXAMPLE
 *                   DataSet result9 = new DataSet();
 *                   spParams = new List<SqlParameter>();
 *                   spParams.Add(new SqlParameter("@promoid", promotionId));
 *                   spParams.Add(new SqlParameter("@bucketid", bucketId));
 *                   spParams.Add(new SqlParameter("@drawingdatetime", drawingDateTime));
 *                   result9 = DataAccess.ExecuteQuerySP("PROMOTION_DRAWING_GetNextForEvent", spParams);
 */


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

    #region Application Installation and Setup Classes
    [Serializable]
    public class RegisterNewUserResult
    {
        private bool _isRegistered;
        private bool _isLocked;
        #region Publics
        public bool isRegistered
        {
            get { return _isRegistered; }
            set { _isRegistered = value; }
        }

        public bool isLocked
        {
            get { return _isLocked; }
            set { _isLocked = value; }
        }
        #endregion
        public void DBGetStoredPin(string mobile, string pinCode)
        {
            try
            {
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@Mobile", mobile));
                spParams.Add(new SqlParameter("@PIN", pinCode));
                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);
                if(result.Tables[0].Rows.Count > 0)
                {
                    isRegistered = true;
                    isLocked = false;
                    
                }
                else
                {
                    //Increment some form of counter for login attempts
                    int loginAttempts = 0;
                    if(loginAttempts > 3)
                    {
                        isLocked = true;
                        isRegistered = false;
                    }
                    else
                    {
                        isLocked = false;
                        isRegistered = false;
                    }
                }
            }
            catch (SqlException ex)
            {
                string errorMessage = ex.Message;
                
            }
        }
    }
    #endregion

    #region Application Launch Classes
    [Serializable]
    public class ValidatePhoneRegisteredResult
    {
        private string _userToken;
        private bool _showBalancesNoPin;
        private bool _isRegistered;
        #region PUBLICS
        public string userToken
        {
            get { return _userToken; }
            set { _userToken = value; }
        }

        public bool ShowBalancesNoPin
        {
            get
            {
                return _showBalancesNoPin;
            }

            set
            {
                _showBalancesNoPin = value;
            }
        }

        public bool IsRegistered
        {
            get
            {
                return _isRegistered;
            }

            set
            {
                _isRegistered = value;
            }
        }
        #endregion
        // Generate a one way hash, API does not specify implementation
        public void GenerateOneWayHash(string mobile)
        {
            //SHould set userToken = to something
            string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);
            //Generate hash based on mobile and current time
            string generatedHash = "hash";
            userToken = generatedHash;
        }

        // Query Database for existing mobile number. Return true if mobile number exists, false if it does not.
        public void checkIsRegistered(string mobile)
        {
            try
            {
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@Mobile", mobile));
                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);
                if (result.Tables[0].Rows.Count > 0)
                {
                    IsRegistered = true;
                }
                else
                {
                    IsRegistered = false;
                }
                //Add logic for showbalancesnopin. Not specified in API doc (default to false?)
                ShowBalancesNoPin = false;
            }
            catch (SqlException ex)
            {
                string errorMessage = ex.Message;
                
            }
        }
    }
    

    [Serializable]
    public class ShowBalancesOnOpeningScreenResult : Default
    {
        public void DBGetAccountBalancesSet(string mobile)
        {
            try
            {
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);
                if(result.Tables[0].Rows.Count > 0)
                {
                    for(int i = 0; i < result.Tables[0].Rows.Count; i++)
                    {
                        Account newAccount = new Account();
                        newAccount.accountName = result.Tables[0].Rows[i][""].ToString();
                        newAccount.accountBalance = result.Tables[0].Rows[i][""].ToString();
                        
                        AccountBalances.Add(newAccount);
                    }
                }
                else
                {
                    AccountBalances = null;
                }
            }
            catch (SqlException ex)
            {
                string errorMessage = ex.Message;
            }
        }

        private List<Account> AccountBalances;

        private class Account
        {
            private string _accountName;
            public string accountName
            {
                get { return _accountName; }
                set { _accountName = value; }
            }
            private string _accountBalance;
            public string accountBalance
            {
                get { return _accountBalance;  }
                set { _accountBalance = value; }
            }
        }
    }

    
    [Serializable]
    public class ValidateUserReturn
    {
        private bool _isValid;
        private string _userToken;

        public bool isValid
        {
            get { return _isValid; }
            set { _isValid = value; }
        }
        public string userToken
        {
            get { return _userToken; }
            set { _userToken = value; }
        }

        public void DBGetStoredPin(string mobile, string pinCode)
        {
            try
            {
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@Mobile", mobile));
                spParams.Add(new SqlParameter("@PIN", pinCode));
                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);
                if (result.Tables[0].Rows.Count > 0)
                {
                    isValid = true;
                    GenerateOneWayHash(mobile);
                }
                else
                {
                    isValid = false;
                    userToken = "";
                }
            }
            catch (SqlException ex)
            {
                string errorMessage = ex.Message;

            }
        }
        public void GenerateOneWayHash(string mobile)
        {
            try
            {
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@Mobile", mobile));
                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);
                userToken = result.Tables[0].Rows[0][""].ToString();
            }
            catch (SqlException ex) 
            {
                string errorMessage = ex.Message;
                isValid = false;
                userToken = "";
            }

        }
    }

    [Serializable]
    public class LoadLogoReturn : Default
    {
        private byte[] _logo;
        public byte[] logo
        {
            get { return _logo; }
            set { _logo = value; }
        }

        public void DBGetLogo(string mobile)
        {
            try
            {
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);
                if(result.Tables[0].Rows.Count > 0)
                {
                    MemoryStream ms = new MemoryStream((byte[])result.Tables[0].Rows[0][""]);
                    byte[] bytes = ms.ToArray();
                    logo = bytes;
                }
                else
                {
                    logo = null;
                }
            }
            catch (SqlException ex)
            {
                string errorMessage = ex.Message;
                logo = null;
            }
        }
    }
    #endregion

    #region Overview Screen Classes
    [Serializable]
    public class GetPlayerGeneralInfoReturn : Default
    {
        private string callToActionCaption;
        private string callToActionText;
        private bool callToActionIsScrolling;
        private string customerName;
        private string customerNumber;
        private string customerTierLevelText;
        private string customerAspirationalText;
        private string customerAwardCaption;
        private string customerAwardText;

        public string CallToActionCaption
        {
            get
            {
                return callToActionCaption;
            }

            set
            {
                callToActionCaption = value;
            }
        }

        public string CallToActionText
        {
            get
            {
                return callToActionText;
            }

            set
            {
                callToActionText = value;
            }
        }

        public bool CallToActionIsScrolling
        {
            get
            {
                return callToActionIsScrolling;
            }

            set
            {
                callToActionIsScrolling = value;
            }
        }

        public string CustomerName
        {
            get
            {
                return customerName;
            }

            set
            {
                customerName = value;
            }
        }

        public string CustomerNumber
        {
            get
            {
                return customerNumber;
            }

            set
            {
                customerNumber = value;
            }
        }

        public string CustomerTierLevelText
        {
            get
            {
                return customerTierLevelText;
            }

            set
            {
                customerTierLevelText = value;
            }
        }

        public string CustomerAspirationalText
        {
            get
            {
                return customerAspirationalText;
            }

            set
            {
                customerAspirationalText = value;
            }
        }

        public string CustomerAwardCaption
        {
            get
            {
                return customerAwardCaption;
            }

            set
            {
                customerAwardCaption = value;
            }
        }

        public string CustomerAwardText
        {
            get
            {
                return customerAwardText;
            }

            set
            {
                customerAwardText = value;
            }
        }

        private void RemoveData()
        {
            CallToActionCaption = null;
            CallToActionText = null;
            CallToActionIsScrolling = false;
            CustomerName = null;
            CustomerNumber = null;
            CustomerTierLevelText = null;
            CustomerAspirationalText = null;
            CustomerAwardCaption = null;
            CustomerAwardText = null;
        }
        public void DBGetPlayerInfo(string mobile)
        {
            try
            {

            }
            catch (SqlException ex)
            {
                string errorMessage = ex.Message;
                RemoveData();
            }
        }
    }

    [Serializable]
    public class GetPlayerPointBucketDetailsReturn : Default
    {
        private List<Bucket> customerPointBuckets;

        public class Bucket
        {
            private string bucketCaption;
            private int bucketPointsValue;

            public string BucketCaption
            {
                get
                {
                    return bucketCaption;
                }

                set
                {
                    bucketCaption = value;
                }
            }

            public int BucketPointsValue
            {
                get
                {
                    return bucketPointsValue;
                }

                set
                {
                    bucketPointsValue = value;
                }
            }
        }

        //Get a set of buckets from SQL DB
        public void DBGetPointsBucket(string mobile)
        {
            throw new NotImplementedException();
//            customerPointBuckets = something;
        }
    }

    [Serializable]
    public class GetPlayerCardImageDetailsReturn : Default
    {
        private byte[] _playerCardImageFront;
        private byte[] _playerCardImageBack;

        public byte[] PlayerCardImageFront
        {
            get
            {
                return _playerCardImageFront;
            }

            set
            {
                _playerCardImageFront = value;
            }
        }

        public byte[] PlayerCardImageBack
        {
            get
            {
                return _playerCardImageBack;
            }

            set
            {
                _playerCardImageBack = value;
            }
        }

        //GetPlayerCard details from the SQL DB
        public void DBGetPlayerCardImage(string mobile)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region My Games Screen
    [Serializable]
    //TODO: Make sure foreach / for work correctly. Need to attempt to run DB GET functions at some point
    public class GetGamesScreenWrapperReturn : Default
    {
        private class Game
        {
            private string gameID;
            private string gameName;
            private string gameDescription;
            private byte[] gameIcon;
            private string buttonDescription;
            private bool isButtonEnabled;

            public string GameID
            {
                get
                {
                    return gameID;
                }

                set
                {
                    gameID = value;
                }
            }

            public string GameName
            {
                get
                {
                    return gameName;
                }

                set
                {
                    gameName = value;
                }
            }

            public string GameDescription
            {
                get
                {
                    return gameDescription;
                }

                set
                {
                    gameDescription = value;
                }
            }

            public byte[] GameIcon
            {
                get
                {
                    return gameIcon;
                }

                set
                {
                    gameIcon = value;
                }
            }

            public string ButtonDescription
            {
                get
                {
                    return buttonDescription;
                }

                set
                {
                    buttonDescription = value;
                }
            }

            public bool IsButtonEnabled
            {
                get
                {
                    return isButtonEnabled;
                }

                set
                {
                    isButtonEnabled = value;
                }
            }
        }
        private string headerCaption;
        private string headerData;
        private List<Game> Games;
        private List<long> PromotionID;
        private void RemoveData()
        {
            headerCaption = null;
            headerData = null;
            Games = null;
            PromotionID = null;
        }
        //SQL DB Function to get games wrapper from DB
        public void DBGetGamesScreenWrapper(string mobile)
        {
            try
            {
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);

                // TODO: SP for header
                DataSet headerDS = new DataSet();
                List<SqlParameter> headerParams = new List<SqlParameter>();
                headerParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                headerDS = DataAcess.ExecuteQuerySP("PEC.", headerParams);

                if(headerDS.Tables[0].Rows.Count > 0)
                {
                    headerCaption = headerDS.Tables[0].Rows[0]["HeaderCaption"].ToString();
                    headerData = headerDS.Tables[0].Rows[0]["HeaderData"].ToString();
                }

                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                result = DataAcess.ExecuteQuerySP("PEC.PROMOTION_ID_GetByCMSPlayerID", spParams);


                if (result.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < result.Tables[0].Rows.Count; i++)
                    {
                        PromotionID.Add(Int64.Parse(result.Tables[0].Rows[i]["PromotionID"].ToString()));
                    }
                    foreach (long ID in PromotionID)
                    {
                        //GET Game Attributes from GD_PROMOtionGames
                        DataSet ds = new DataSet();
                        List<SqlParameter> gameParams = new List<SqlParameter>();
                        gameParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                        gameParams.Add(new SqlParameter("@PromotionID", ID));

                        ds = DataAcess.ExecuteQuerySP("PEC.MG_PROMOTION_WRAPPER_GetByPromotionID", gameParams);

                        DataSet imageSet = new DataSet();
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Game newGame = new Game();
                            newGame.GameID = ds.Tables[0].Rows[0]["GameID"].ToString();
                            newGame.GameName = ds.Tables[0].Rows[0]["GameName"].ToString();
                            newGame.GameDescription = ds.Tables[0].Rows[0]["GameDescription"].ToString();
                            newGame.ButtonDescription = ds.Tables[0].Rows[0]["ButtonDesc"].ToString();
                            newGame.IsButtonEnabled = Convert.ToBoolean(ds.Tables[0].Rows[0]["isButtonEnabled"].ToString());

                            DataSet imageData = new DataSet();
                            List<SqlParameter> imageParam = new List<SqlParameter>();
                            imageParam.Add(new SqlParameter("@PromotionID", ID));
                            imageData = DataAcess.ExecuteQuerySP("MG_PROMOTION_WRAPPER_ReadGameIcon", imageParam);
                            if (imageData.Tables[0].Rows.Count > 0)
                            {
                                MemoryStream ms = new MemoryStream((byte[])imageData.Tables[0].Rows[0]["promoKioskImage"]);
                                byte[] bytes = ms.ToArray();
                                newGame.GameIcon = bytes;
                            }
                            else
                            {
                                newGame.GameIcon = null;
                            }
                            Games.Add(newGame);
                        }

                    }
                }
            }

            catch(SqlException ex)
            {
                RemoveData();
                string errorMessage = ex.Message;
            }
            
        }
    }



    // Lars: Where should i be looking for AttributeBinaryValue? Would I just select all 'image' from AttributeName and store the
    // value as attributeBinaryValue, else leave blank? Need to know for writing this method.

    // TODO: Implement attributeBinaryValue (clarify with LK)
    [Serializable]
    public class GetIntervalsAndBackgroundsReturn : Default
    {
        private List<AttributeInfo> Attributes;
        private void RemoveData()
        {
            Attributes = null;
        }

        private class AttributeInfo
        {
            private string gameName;
            private string pageName;
            private string typeName;
            private string objectName;
            private string attributeName;
            private string attributeValue;
            private byte[] attributeBinaryValue;

            public string GameName
            {
                get
                {
                    return gameName;
                }

                set
                {
                    gameName = value;
                }
            }

            public string PageName
            {
                get
                {
                    return pageName;
                }

                set
                {
                    pageName = value;
                }
            }

            public string TypeName
            {
                get
                {
                    return typeName;
                }

                set
                {
                    typeName = value;
                }
            }

            public string ObjectName
            {
                get
                {
                    return objectName;
                }

                set
                {
                    objectName = value;
                }
            }

            public string AttributeName
            {
                get
                {
                    return attributeName;
                }

                set
                {
                    attributeName = value;
                }
            }

            public string AttributeValue
            {
                get
                {
                    return attributeValue;
                }

                set
                {
                    attributeValue = value;
                }
            }

            public byte[] AttributeBinaryValue
            {
                get
                {
                    return attributeBinaryValue;
                }

                set
                {
                    attributeBinaryValue = value;
                }
            }
        }

        // TODO: Binary Data for background
        // LARS: What is the binary data I'm looking for here? Not sure based on API Doc
        public void DBGetIntervalsAndBackgrounds(string mobile, int gameID, int variantID)
        {
            string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);

            DataSet result = new DataSet();
            List<SqlParameter> spParams = new List<SqlParameter>();
            spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
            spParams.Add(new SqlParameter("@GameID", gameID));
            spParams.Add(new SqlParameter("@VariantID", variantID));

            result = DataAcess.ExecuteQuerySP("MG_PROMOTION_BACKGROUND_GetByGameID", spParams);

            if (result.Tables[0].Rows.Count > 0)
            {
                for (int j = 0; j < result.Tables[0].Rows.Count; j++)
                {
                    AttributeInfo gameAttributes = new AttributeInfo();
                    gameAttributes.GameName = result.Tables[0].Rows[j]["gameName"].ToString();
                    gameAttributes.PageName = result.Tables[0].Rows[j]["pageName"].ToString();
                    gameAttributes.TypeName = result.Tables[0].Rows[j]["typename"].ToString();
                    gameAttributes.ObjectName = result.Tables[0].Rows[j]["objectName"].ToString();
                    gameAttributes.AttributeName = result.Tables[0].Rows[j]["attributeName"].ToString();
                    gameAttributes.AttributeValue = result.Tables[0].Rows[j]["attributeValue"].ToString();

                    DataSet binaryData = new DataSet();
                    List<SqlParameter> binaryParams = new List<SqlParameter>();
                    binaryParams.Add(new SqlParameter("@GameID", gameID));
                    binaryParams.Add(new SqlParameter("@VariantID", variantID));

                    binaryData = DataAcess.ExecuteQuerySP("TODO", binaryParams);

                    if (binaryData.Tables[0].Rows.Count > 0)
                    {
                        MemoryStream ms = new MemoryStream((byte[])binaryData.Tables[0].Rows[0]["attributeBinaryData"]);
                        byte[] bytes = ms.ToArray();
                        gameAttributes.AttributeBinaryValue = bytes;
                    }
                    else
                    {
                        gameAttributes.AttributeBinaryValue = null;
                    }
                    Attributes.Add(gameAttributes);
                }
            }

            else
            {
                RemoveData();
            }
        }
    }
   
    // TODO
    [Serializable]
    public class GetPageAttributesReturn : Default
    {
        private string gameName;
        private string pageName;
        private string caption;
        private List<Attributes> listAttributes;

        private class Attributes
        {
            private string typeName;
            private string objectName;
            private string attributeName;
            private string attributeValue;
            private byte[] attributeValueBinary;

            public string TypeName
            {
                get
                {
                    return typeName;
                }

                set
                {
                    typeName = value;
                }
            }

            public string ObjectName
            {
                get
                {
                    return objectName;
                }

                set
                {
                    objectName = value;
                }
            }

            public string AttributeName
            {
                get
                {
                    return attributeName;
                }

                set
                {
                    attributeName = value;
                }
            }

            public string AttributeValue
            {
                get
                {
                    return attributeValue;
                }

                set
                {
                    attributeValue = value;
                }
            }

            public byte[] AttributeValueBinary
            {
                get
                {
                    return attributeValueBinary;
                }

                set
                {
                    attributeValueBinary = value;
                }
            }
        }



        public string GameName
        {
            get
            {
                return gameName;
            }

            set
            {
                gameName = value;
            }
        }

        public string PageName
        {
            get
            {
                return pageName;
            }

            set
            {
                pageName = value;
            }
        }

        public string Caption
        {
            get
            {
                return caption;
            }

            set
            {
                caption = value;
            }
        }

        private void RemoveData()
        {
            GameName = null;
            PageName = null;
            Caption = null;
            listAttributes = null;
        }
        
        // TODO: ADD binary data SP
        // LARS: Where do I find the binary data for page attributes? Not clear from API DOC
        public void DBGetPageAttributes(string mobile, string inPageName, int gameID)
        {
            try
            {
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@GameID", gameID));

                result = DataAcess.ExecuteQuerySP("PEC.MG_PROMOTION_GetGameNameANDCaption_ByGameID", spParams);
                if (result.Tables[0].Rows.Count > 0)
                {
                    GameName = result.Tables[0].Rows[0]["GameName"].ToString();
                    PageName = inPageName;
                    Caption = result.Tables[0].Rows[0]["Caption"].ToString();

                    DataSet aR = new DataSet();
                    List<SqlParameter> attParams = new List<SqlParameter>();
                    attParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                    attParams.Add(new SqlParameter("@GameID", gameID));
                    attParams.Add(new SqlParameter("@PageName", PageName));

                    aR = DataAcess.ExecuteQuerySP("PEC.MG_PROMOTION_GetPageAttributes", attParams);

                    if (aR.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < aR.Tables[0].Rows.Count; j++)
                        {
                            Attributes pageAttributes = new Attributes();
                            pageAttributes.TypeName = aR.Tables[0].Rows[j]["TypeName"].ToString();
                            pageAttributes.ObjectName = aR.Tables[0].Rows[j]["ObjectName"].ToString();
                            pageAttributes.AttributeName = aR.Tables[0].Rows[j]["AttributeName"].ToString();
                            pageAttributes.AttributeValue = aR.Tables[0].Rows[j]["AttributeValue"].ToString();


                            DataSet binaryData = new DataSet();
                            List<SqlParameter> binaryParams = new List<SqlParameter>();
                            binaryParams.Add(new SqlParameter("@GameID", gameID));
                            binaryParams.Add(new SqlParameter("@PageName", PageName));

                            binaryData = DataAcess.ExecuteQuerySP("TODO", binaryParams);

                            if (binaryData.Tables[0].Rows.Count > 0)
                            {
                                MemoryStream ms = new MemoryStream((byte[])binaryData.Tables[0].Rows[0]["attributeBinaryData"]);
                                byte[] bytes = ms.ToArray();
                                pageAttributes.AttributeValueBinary = bytes;
                            }
                            else
                            {
                                pageAttributes.AttributeValueBinary = null;
                            }

                            listAttributes.Add(pageAttributes);
                        }

                    }
                    //ELSE: There are no attributes for a given pageNumber
                    else
                    {
                        RemoveData();
                    }
                }
                else
                {
                    RemoveData();
                }
            }

            catch(SqlException ex)
            {
                string errorMessage = ex.Message;
                RemoveData();
            }
            
        }
    }

    [Serializable]
    public class StartGameReturn : Default
    {
        private string gameToken;
        private string startGameCaption;
        private string startGameText;

        public string GameToken
        {
            get
            {
                return gameToken;
            }

            set
            {
                gameToken = value;
            }
        }

        public string StartGameCaption
        {
            get
            {
                return startGameCaption;
            }

            set
            {
                startGameCaption = value;
            }
        }

        public string StartGameText
        {
            get
            {
                return startGameText;
            }

            set
            {
                startGameText = value;
            }
        }

        private void RemoveData()
        {
            GameToken = null;
            StartGameCaption = null;
            StartGameText = null;
        }

        public void DBStartGame(string mobile, int gameID, long PromotionID)
        {

            try
            {
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);
                StartGameReturn data = new StartGameReturn();
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@GameID", gameID));
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                spParams.Add(new SqlParameter("@PromotionID", PromotionID));
                result = DataAcess.ExecuteQuerySP("PEC.MG_PROMOTION_StartGame", spParams);

                if (result.Tables[0].Rows.Count > 0)
                {
                    GameToken = result.Tables[0].Rows[0]["GameToken"].ToString();
                    StartGameCaption = result.Tables[0].Rows[0]["StartGameCaption"].ToString();
                    StartGameText = result.Tables[0].Rows[0]["StartGameText"].ToString();
                }
                else
                {
                    RemoveData();
                }
            }

            catch(SqlException ex)
            {
                string errorMessage = ex.Message;
                RemoveData();
            }
        }
    }

    [Serializable]
    public class GetGameInfoForPromotionReturn : Default
    {
        private int variantID;
        private string gameNameForDisplay;
        private string playInstructions;
        private string gameType;
        private List<GameObject> GameObjects;

        public int VariantID
        {
            get
            {
                return variantID;
            }

            set
            {
                variantID = value;
            }
        }

        public string GameNameForDisplay
        {
            get
            {
                return gameNameForDisplay;
            }

            set
            {
                gameNameForDisplay = value;
            }
        }

        public string PlayInstructions
        {
            get
            {
                return playInstructions;
            }

            set
            {
                playInstructions = value;
            }
        }

        public string GameType
        {
            get
            {
                return gameType;
            }

            set
            {
                gameType = value;
            }
        }

        private void RemoveData()
        {
            VariantID = -1;
            GameNameForDisplay = null;
            PlayInstructions = null;
            GameType = null;
            GameObjects = null;
        }

        private class GameObject
        {
            private string objectName;
            private int objectID;
            private string gOAttributeName;
            private string gOAttributeValue;
            private byte[] gOAttributeValueBinary;
            #region publics
            public string ObjectName
            {
                get
                {
                    return objectName;
                }

                set
                {
                    objectName = value;
                }
            }

            public int ObjectID
            {
                get
                {
                    return objectID;
                }

                set
                {
                    objectID = value;
                }
            }

            public string GOAttributeName
            {
                get
                {
                    return gOAttributeName;
                }

                set
                {
                    gOAttributeName = value;
                }
            }

            public string GOAttributeValue
            {
                get
                {
                    return gOAttributeValue;
                }

                set
                {
                    gOAttributeValue = value;
                }
            }

            public byte[] GOAttributeValueBinary
            {
                get
                {
                    return gOAttributeValueBinary;
                }

                set
                {
                    gOAttributeValueBinary = value;
                }
            }
            #endregion 
        }

        public void DBGetGameInfoForPromotion(string mobile, int gameID, string gameToken)
        {
            if(gameID < 0 || gameToken == null)
            {
                throw new WrongParamsError();
            }
            else
            {
                try
                {
                    string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);

                    DataSet result = new DataSet();
                    List<SqlParameter> spParams = new List<SqlParameter>();
                    spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                    spParams.Add(new SqlParameter("@GameID", gameID));
                    spParams.Add(new SqlParameter("@GameToken", gameToken));
                    

                    result = DataAcess.ExecuteQuerySP("PEC.MG_", spParams);
                    if(result.Tables[0].Rows.Count > 0)
                    {
                        variantID = Convert.ToInt32(result.Tables[0].Rows[0]["VariantID"].ToString());
                        gameNameForDisplay = result.Tables[0].Rows[0]["GameName"].ToString();
                        playInstructions = result.Tables[0].Rows[0]["PlayInstructions"].ToString();
                        gameType = result.Tables[0].Rows[0]["GameType"].ToString();



                        DataSet gameDS = new DataSet();
                        List<SqlParameter> gParams = new List<SqlParameter>();
                        gParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                        gParams.Add(new SqlParameter("@GameID", gameID));
                        gParams.Add(new SqlParameter("@VariantID", variantID));

                        gameDS = DataAcess.ExecuteQuerySP("PEC.MG_PROMOTIONS_GetGameObjects", gParams);

                        for(int i = 0; i < gameDS.Tables[0].Rows.Count; i++)
                        {
                            GameObject newObject = new GameObject();
                            newObject.ObjectName = gameDS.Tables[0].Rows[i]["ObjectName"].ToString();
                            newObject.ObjectID = Convert.ToInt32(gameDS.Tables[0].Rows[i]["ObjectID"].ToString());
                            newObject.GOAttributeName = gameDS.Tables[0].Rows[i]["AttributeName"].ToString();
                            newObject.GOAttributeValue = gameDS.Tables[0].Rows[i]["AttributeValue"].ToString();

                            DataSet imageData = new DataSet();
                            List<SqlParameter> imageParam = new List<SqlParameter>();
                            imageParam.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                            imageParam.Add(new SqlParameter("@GameID", gameID));
                            imageParam.Add(new SqlParameter("@VariantID", variantID));

                            imageData = DataAcess.ExecuteQuerySP("MG_PROMOTION_GameObjectGetImage", imageParam);
                            if (imageData.Tables[0].Rows.Count > 0)
                            {
                                MemoryStream ms = new MemoryStream((byte[])imageData.Tables[0].Rows[0]["GameObjectAttributeBinary"]);
                                byte[] bytes = ms.ToArray();
                                newObject.GOAttributeValueBinary = bytes;
                            }
                            else
                            {
                                newObject.GOAttributeValueBinary = null;
                            }
                            GameObjects.Add(newObject);
                        }
                    }
                    else
                    {
                        throw new Exception("No data");
                    }
                }
                
                catch (Exception)
                {
                    RemoveData();
                }
            }
        }
    }

    [Serializable]
    public class SaveWinInfoReturn : Default
    {
        private string gameNameForDisplay;
        private string closingCaption;
        private string prizePickUpDescription;
        private string callToActionCaption;
        private string callToActionText;
        private bool callToActionIsScrolling;

        #region publics
        public string GameNameForDisplay
        {
            get
            {
                return gameNameForDisplay;
            }

            set
            {
                gameNameForDisplay = value;
            }
        }

        public string ClosingCaption
        {
            get
            {
                return closingCaption;
            }

            set
            {
                closingCaption = value;
            }
        }

        public string PrizePickUpDescription
        {
            get
            {
                return prizePickUpDescription;
            }

            set
            {
                prizePickUpDescription = value;
            }
        }

        public string CallToActionCaption
        {
            get
            {
                return callToActionCaption;
            }

            set
            {
                callToActionCaption = value;
            }
        }

        public string CallToActionText
        {
            get
            {
                return callToActionText;
            }

            set
            {
                callToActionText = value;
            }
        }

        public bool CallToActionIsScrolling
        {
            get
            {
                return callToActionIsScrolling;
            }

            set
            {
                callToActionIsScrolling = value;
            }
        }
        #endregion

        private void RemoveData()
        {
            GameNameForDisplay = null;
            ClosingCaption = null;
            PrizePickUpDescription = null;
            CallToActionCaption = null;
            CallToActionText = null;
            CallToActionIsScrolling = false;
        }

        //Convert parameters into object, save to database, and return object
        public void DBSaveWinInfo(string mobile, int gameID, string gameToken, string objectsSelected)
        {
            try
            {
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);

                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                spParams.Add(new SqlParameter("@GameID", gameID));
                spParams.Add(new SqlParameter("@GameToken", gameToken));
                spParams.Add(new SqlParameter("@ObjectsSelected", objectsSelected));

                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);
                if(result.Tables[0].Rows.Count > 0)
                {
                    GameNameForDisplay = result.Tables[0].Rows[0]["GameName"].ToString();
                    ClosingCaption = result.Tables[0].Rows[0]["ClosingCaption"].ToString();
                    PrizePickUpDescription = result.Tables[0].Rows[0]["PrizePickupDescription"].ToString();
                    CallToActionCaption = result.Tables[0].Rows[0]["CallToActionCaption"].ToString();
                    CallToActionText = result.Tables[0].Rows[0]["CallToActionText"].ToString();
                    CallToActionIsScrolling = Convert.ToBoolean(result.Tables[0].Rows[0]["CallToActionScrolling"].ToString());
                }
                else
                {
                    throw new Exception("DBSaveWinInfo Failed");
                }
            }

            catch(SqlException ex)
            {
                string errorMessage = ex.Message;
                RemoveData();
            }
            

        }
    }

    [Serializable]
    public class SaveLoseInfoReturn : Default
    {
        private string gameNameForDisplay;
        private string closingCaption;
        private string closingText;
        private string callToActionCaption;
        private string callToActionText;
        private bool callToActionIsScrolling;

        #region public
        public string GameNameForDisplay
        {
            get
            {
                return gameNameForDisplay;
            }

            set
            {
                gameNameForDisplay = value;
            }
        }

        public string ClosingCaption
        {
            get
            {
                return closingCaption;
            }

            set
            {
                closingCaption = value;
            }
        }

        public string ClosingText
        {
            get
            {
                return closingText;
            }

            set
            {
                closingText = value;
            }
        }

        public string CallToActionCaption
        {
            get
            {
                return callToActionCaption;
            }

            set
            {
                callToActionCaption = value;
            }
        }

        public string CallToActionText
        {
            get
            {
                return callToActionText;
            }

            set
            {
                callToActionText = value;
            }
        }

        public bool CallToActionIsScrolling
        {
            get
            {
                return callToActionIsScrolling;
            }

            set
            {
                callToActionIsScrolling = value;
            }
        }
        #endregion

        private void RemoveData()
        {
            gameNameForDisplay = null;
            closingCaption = null;
            callToActionCaption = null;
            callToActionText = null;
            callToActionIsScrolling = false;
        }
        //Convert parameters to object, save to DB, return object
        public void DBSaveLoseInfo(string mobile, int gameID, string gameToken, string objectsSelected)
        {
            try
            {
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);

                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", mobile));
                spParams.Add(new SqlParameter("@GameID", gameID));
                spParams.Add(new SqlParameter("@GameToken", gameToken));
                spParams.Add(new SqlParameter("@ObjectsSelected", objectsSelected));

                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);
                if (result.Tables[0].Rows.Count > 0)
                {
                    GameNameForDisplay = result.Tables[0].Rows[0]["GameName"].ToString();
                    ClosingCaption = result.Tables[0].Rows[0]["ClosingCaption"].ToString();
                    ClosingText = result.Tables[0].Rows[0]["ClosingText"].ToString();
                    CallToActionCaption = result.Tables[0].Rows[0]["CallToActionCaption"].ToString();
                    CallToActionText = result.Tables[0].Rows[0]["CallToActionText"].ToString();
                    CallToActionIsScrolling = Convert.ToBoolean(result.Tables[0].Rows[0]["CallToActionScrolling"].ToString());
                }
                else
                {
                    throw new Exception("DBSaveWinInfo Failed");
                }
            }

            catch (SqlException ex)
            {
                string errorMessage = ex.Message;
                RemoveData();
            }


        }
    }
    #endregion

    #region My Promotions Screen
    [Serializable]
    public class GetPromotionsScreenWrapperReturn : Default
    {
        private string line1Caption;
        private string line1Data;
        private string line2Caption;
        private string line2Data;
        private List<PromoButton> buttons;
        #region publics
        public string Line1Caption
        {
            get
            {
                return line1Caption;
            }

            set
            {
                line1Caption = value;
            }
        }

        public string Line1Data
        {
            get
            {
                return line1Data;
            }

            set
            {
                line1Data = value;
            }
        }

        public string Line2Caption
        {
            get
            {
                return line2Caption;
            }

            set
            {
                line2Caption = value;
            }
        }

        public string Line2Data
        {
            get
            {
                return line2Data;
            }

            set
            {
                line2Data = value;
            }
        }

        public List<PromoButton> Buttons
        {
            get
            {
                return buttons;
            }

            set
            {
                buttons = value;
            }
        }
        #endregion

        public class PromoButton
        {
            private string buttonCaption;
            private int buttonOrdPos;

            public string ButtonCaption
            {
                get
                {
                    return buttonCaption;
                }

                set
                {
                    buttonCaption = value;
                }
            }

            public int ButtonOrdPos
            {
                get
                {
                    return buttonOrdPos;
                }

                set
                {
                    buttonOrdPos = value;
                }
            }
        }

        private void RemoveData()
        {
            Line1Caption = null;
            Line2Caption = null;
            Line1Data = null;
            Line2Data = null;
            Buttons = null;

        }
        //DB Get Promotions Screen Wrapper from SQL Server
        public void DBGetPromotionsScreenWrapperReturn(string mobile)
        {
            try
            {
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);

                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                result = DataAcess.ExecuteQuerySP("PEC.PROMOTIONS_GetWrapper", spParams);

                if (result.Tables[0].Rows.Count > 0)
                {
                    Line1Caption = result.Tables[0].Rows[0]["Line1Caption"].ToString();
                    Line1Data = result.Tables[0].Rows[0]["Line1Data"].ToString();
                    Line2Caption = result.Tables[0].Rows[0]["Line2Caption"].ToString();
                    Line2Data = result.Tables[0].Rows[0]["Line2Data"].ToString();

                    DataSet buttonDS = new DataSet();
                    List<SqlParameter> buttonParams = new List<SqlParameter>();
                    buttonParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                    buttonDS = DataAcess.ExecuteQuerySP("PEC.PROMOTIONS_WrapperButtons", buttonParams);

                    if (buttonDS.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < buttonDS.Tables[0].Rows.Count; i++)
                        {
                            PromoButton newButton = new PromoButton();
                            newButton.ButtonCaption = buttonDS.Tables[0].Rows[i]["ButtonCaption"].ToString();
                            newButton.ButtonOrdPos = Convert.ToInt32(buttonDS.Tables[0].Rows[i]["ButtonOrdPos"].ToString());
                            Buttons.Add(newButton);
                        }
                    }
                    else
                    {
                        RemoveData();
                    }
                }
                else
                {
                    RemoveData();
                }
            }



            catch (SqlException ex)
            {
                RemoveData();
                string errorMessage = ex.Message;
            }

        }
    }

    [Serializable]
    public class GetPromotionListReturn : Default
    {
        private List<Promotion> promotions;

        public List<Promotion> Promotions
        {
            get
            {
                return promotions;
            }

            set
            {
                promotions = value;
            }
        }

        public class Promotion
        {
            private int promotionID;
            private int promoVersionID;
            private int gameID;
            private string promotionName;
            private DateTime startDate;
            private DateTime endDate;
            private bool enrolled;
            private bool couponsAvaliable;
            private DateTime nextDrawingDate;
            private bool checkInAvaliable;
            private bool freeEntryAvaliable;
            private bool remoteEntryAvaliable;
            private int pointsToDate;
            private int entriesToDate;
            private int pointsPerEntry;
            private int todaysPoint;
            private int dailyEntryLImit;
            private string thresholdType;
            private int thresholdPoints;
            private string prizeName;
            private bool thresholdReached;
            private bool prizeClaimed;
            private bool gameAvaliable;
            private string checkInStatusMessage;
            private bool raffleAvaliable;
            private List<RaffleTicketList> raffleTickets;
            private byte[] promotionImage;
            #region Publics
            public int PromotionID
            {
                get
                {
                    return promotionID;
                }

                set
                {
                    promotionID = value;
                }
            }

            public int PromoVersionID
            {
                get
                {
                    return promoVersionID;
                }

                set
                {
                    promoVersionID = value;
                }
            }

            public int GameID
            {
                get
                {
                    return gameID;
                }

                set
                {
                    gameID = value;
                }
            }

            public string PromotionName
            {
                get
                {
                    return promotionName;
                }

                set
                {
                    promotionName = value;
                }
            }

            public DateTime StartDate
            {
                get
                {
                    return startDate;
                }

                set
                {
                    startDate = value;
                }
            }

            public DateTime EndDate
            {
                get
                {
                    return endDate;
                }

                set
                {
                    endDate = value;
                }
            }

            public bool Enrolled
            {
                get
                {
                    return enrolled;
                }

                set
                {
                    enrolled = value;
                }
            }

            public bool CouponsAvaliable
            {
                get
                {
                    return couponsAvaliable;
                }

                set
                {
                    couponsAvaliable = value;
                }
            }

            public DateTime NextDrawingDate
            {
                get
                {
                    return nextDrawingDate;
                }

                set
                {
                    nextDrawingDate = value;
                }
            }

            public bool CheckInAvaliable
            {
                get
                {
                    return checkInAvaliable;
                }

                set
                {
                    checkInAvaliable = value;
                }
            }

            public bool FreeEntryAvaliable
            {
                get
                {
                    return freeEntryAvaliable;
                }

                set
                {
                    freeEntryAvaliable = value;
                }
            }

            public bool RemoteEntryAvaliable
            {
                get
                {
                    return remoteEntryAvaliable;
                }

                set
                {
                    remoteEntryAvaliable = value;
                }
            }

            public int PointsToDate
            {
                get
                {
                    return pointsToDate;
                }

                set
                {
                    pointsToDate = value;
                }
            }

            public int EntriesToDate
            {
                get
                {
                    return entriesToDate;
                }

                set
                {
                    entriesToDate = value;
                }
            }

            public int PointsPerEntry
            {
                get
                {
                    return pointsPerEntry;
                }

                set
                {
                    pointsPerEntry = value;
                }
            }

            public int TodaysPoint
            {
                get
                {
                    return todaysPoint;
                }

                set
                {
                    todaysPoint = value;
                }
            }

            public int DailyEntryLImit
            {
                get
                {
                    return dailyEntryLImit;
                }

                set
                {
                    dailyEntryLImit = value;
                }
            }

            public string ThresholdType
            {
                get
                {
                    return thresholdType;
                }

                set
                {
                    thresholdType = value;
                }
            }

            public int ThresholdPoints
            {
                get
                {
                    return thresholdPoints;
                }

                set
                {
                    thresholdPoints = value;
                }
            }

            public string PrizeName
            {
                get
                {
                    return prizeName;
                }

                set
                {
                    prizeName = value;
                }
            }

            public bool ThresholdReached
            {
                get
                {
                    return thresholdReached;
                }

                set
                {
                    thresholdReached = value;
                }
            }

            public bool PrizeClaimed
            {
                get
                {
                    return prizeClaimed;
                }

                set
                {
                    prizeClaimed = value;
                }
            }

            public bool GameAvaliable
            {
                get
                {
                    return gameAvaliable;
                }

                set
                {
                    gameAvaliable = value;
                }
            }

            public string CheckInStatusMessage
            {
                get
                {
                    return checkInStatusMessage;
                }

                set
                {
                    checkInStatusMessage = value;
                }
            }

            public bool RaffleAvaliable
            {
                get
                {
                    return raffleAvaliable;
                }

                set
                {
                    raffleAvaliable = value;
                }
            }

            public List<RaffleTicketList> RaffleTickets
            {
                get
                {
                    return raffleTickets;
                }

                set
                {
                    raffleTickets = value;
                }
            }

            public byte[] PromotionImage
            {
                get
                {
                    return promotionImage;
                }

                set
                {
                    promotionImage = value;
                }
            }
        }
        #endregion
        public class RaffleTicketList
        {
            private string raffleTicket;

            public string RaffleTicket
            {
                get
                {
                    return raffleTicket;
                }

                set
                {
                    raffleTicket = value;
                }
            }
        }

        private void RemoveData()
        {
            Promotions = null;
        }
        //DB Get Promotion List
        public void DBGetPromotionList(string mobile)
        {
            try
            {
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);
                DataSet ds = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                ds = DataAcess.ExecuteQuerySP("PEC.PROMOTIONS_GetPromotionList_ByCMSPlayerID", spParams);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Promotion newPromo = new Promotion();
                        newPromo.PromotionID = Convert.ToInt32(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.PromoVersionID = Convert.ToInt32(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.GameID = Convert.ToInt32(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.PromotionName = ds.Tables[0].Rows[i][""].ToString();
                        newPromo.StartDate = Convert.ToDateTime(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.EndDate = Convert.ToDateTime(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.Enrolled = Convert.ToBoolean(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.CouponsAvaliable = Convert.ToBoolean(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.NextDrawingDate = Convert.ToDateTime(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.CheckInAvaliable = Convert.ToBoolean(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.CheckInStatusMessage = ds.Tables[0].Rows[i][""].ToString();
                        newPromo.FreeEntryAvaliable = Convert.ToBoolean(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.RemoteEntryAvaliable = Convert.ToBoolean(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.PointsToDate = Convert.ToInt32(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.EntriesToDate = Convert.ToInt32(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.PointsPerEntry = Convert.ToInt32(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.TodaysPoint = Convert.ToInt32(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.DailyEntryLImit = Convert.ToInt32(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.ThresholdType = ds.Tables[0].Rows[i][""].ToString();
                        newPromo.ThresholdPoints = Convert.ToInt32(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.PrizeName = ds.Tables[0].Rows[i][""].ToString();
                        newPromo.ThresholdReached = Convert.ToBoolean(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.PrizeClaimed = Convert.ToBoolean(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.GameAvaliable = Convert.ToBoolean(ds.Tables[0].Rows[i][""].ToString());
                        newPromo.RaffleAvaliable = Convert.ToBoolean(ds.Tables[0].Rows[i][""].ToString());

                        DataSet raffleDS = new DataSet();
                        List<SqlParameter> raffleParams = new List<SqlParameter>();
                        raffleParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                        raffleDS = DataAcess.ExecuteQuerySP("PEC.PROMOTIONS_GetPromotionRaffleTickets", raffleParams);

                        if(raffleDS.Tables[0].Rows.Count > 0)
                        {
                            for(int j = 0; j < raffleDS.Tables[0].Rows.Count; j++)
                            {
                                RaffleTicketList newRaffleTicket = new RaffleTicketList();
                                newRaffleTicket.RaffleTicket = raffleDS.Tables[0].Rows[j][""].ToString();
                                newPromo.RaffleTickets.Add(newRaffleTicket);
                            }
                        }
                        else
                        {
                            newPromo.RaffleTickets = null;
                        }

                        DataSet imageDS = new DataSet();
                        List<SqlParameter> imageParams = new List<SqlParameter>();
                        imageParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                        imageDS = DataAcess.ExecuteQuerySP("PEC.PROMOTIONS_GetPromotionImage", imageParams);

                        if(imageDS.Tables[0].Rows.Count > 0)
                        {
                            MemoryStream ms = new MemoryStream((byte[])imageDS.Tables[0].Rows[0]["Image"]);
                            byte[] bytes = ms.ToArray();
                            newPromo.PromotionImage = bytes;
                        }
                        else
                        {
                            newPromo.PromotionImage = null;
                        }

                        Promotions.Add(newPromo);
                    }
                }
                else
                {
                    RemoveData();
                }
            }
            catch (SqlException ex)
            {
                string errorMessage = ex.Message;
                RemoveData();
            }
        }


    }

    [Serializable]
    public class EnterRemoteEntryReturn : Default
    {
        private int promotionID;
        private int updateEntryCount;
        private bool remoteEntryAvaliable;
        private void RemoveData()
        {
            PromotionID = -1;
            UpdateEntryCount = -1;
            RemoteEntryAvaliable = false;
        }
        #region publics
        public int PromotionID
        {
            get
            {
                return promotionID;
            }

            set
            {
                promotionID = value;
            }
        }

        public int UpdateEntryCount
        {
            get
            {
                return updateEntryCount;
            }

            set
            {
                updateEntryCount = value;
            }
        }

        public bool RemoteEntryAvaliable
        {
            get
            {
                return remoteEntryAvaliable;
            }

            set
            {
                remoteEntryAvaliable = value;
            }
        }
        #endregion
        public void DBEnterRemoteEntry(string mobile, int promotionID)
        {
            PromotionID = promotionID;
            try
            {
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);

                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                spParams.Add(new SqlParameter("@PromotionID", promotionID));
                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);
                
                if(result.Tables[0].Rows.Count > 0)
                {
                    UpdateEntryCount = Convert.ToInt32(result.Tables[0].Rows[0]["UpdateEntryCount"].ToString());
                    RemoteEntryAvaliable = Convert.ToBoolean(result.Tables[0].Rows[0]["RemoteEntryAvaliable"].ToString());
                }
                else
                {
                    RemoveData();
                }
            }
            catch (SqlException ex)
            {
                string errorMessage = ex.Message;
                RemoveData();
            }
        }
    }

    [Serializable]
    public class ClaimBonusCouponsReturn : Default
    {
        private int promotionID;
        private List<ResultDescriptionBonus> resultDescriptions;
        private bool claimButtonRemainsVisible;
        private bool claimButtonRemainsActive;
        private string claimButtonNewCaption;
        private byte[] claimButtonNewImage;
        #region publics
        public int PromotionID
        {
            get
            {
                return promotionID;
            }

            set
            {
                promotionID = value;
            }
        }

        public List<ResultDescriptionBonus> ResultDescriptions
        {
            get
            {
                return resultDescriptions;
            }

            set
            {
                resultDescriptions = value;
            }
        }

        public bool ClaimButtonRemainsVisible
        {
            get
            {
                return claimButtonRemainsVisible;
            }

            set
            {
                claimButtonRemainsVisible = value;
            }
        }

        public bool ClaimButtonRemainsActive
        {
            get
            {
                return claimButtonRemainsActive;
            }

            set
            {
                claimButtonRemainsActive = value;
            }
        }

        public string ClaimButtonNewCaption
        {
            get
            {
                return claimButtonNewCaption;
            }

            set
            {
                claimButtonNewCaption = value;
            }
        }

        public byte[] ClaimButtonNewImage
        {
            get
            {
                return claimButtonNewImage;
            }

            set
            {
                claimButtonNewImage = value;
            }
        }
        #endregion
        public class ResultDescriptionBonus
        {
            private string resultCaption;
            private string resultUpdatedElement;
            private byte[] resultImage;
            private List<DataPoint> dataPoints;
            #region publics
            public string ResultCaption
            {
                get
                {
                    return resultCaption;
                }

                set
                {
                    resultCaption = value;
                }
            }

            public string ResultUpdatedElement
            {
                get
                {
                    return resultUpdatedElement;
                }

                set
                {
                    resultUpdatedElement = value;
                }
            }

            public byte[] ResultImage
            {
                get
                {
                    return resultImage;
                }

                set
                {
                    resultImage = value;
                }
            }

            public List<DataPoint> DataPoints
            {
                get
                {
                    return dataPoints;
                }

                set
                {
                    dataPoints = value;
                }
            }
            #endregion

        }

        public class DataPoint
        {
            private string dataPointCaption;
            private string dataPointData;

            public string DataPointCaption
            {
                get
                {
                    return dataPointCaption;
                }

                set
                {
                    dataPointCaption = value;
                }
            }

            public string DataPointData
            {
                get
                {
                    return dataPointData;
                }

                set
                {
                    dataPointData = value;
                }
            }
        }

        private void RemoveData()
        {
            PromotionID = -1;
            ResultDescriptions = null;
            ClaimButtonNewCaption = null;
            ClaimButtonNewImage = null;
            ClaimButtonRemainsVisible = false;
            ClaimButtonRemainsActive = false;
        }

        public void DBClaimBonusCoupons(string mobile, int promotionID)
        {
            try
            {
                PromotionID = promotionID;
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                spParams.Add(new SqlParameter("@PromotionID", PromotionID));
                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);
                
                if(result.Tables[0].Rows.Count > 0)
                {
                    ClaimButtonRemainsVisible = Convert.ToBoolean(result.Tables[0].Rows[0][""].ToString());
                    ClaimButtonRemainsActive = Convert.ToBoolean(result.Tables[0].Rows[0][""].ToString());
                    ClaimButtonNewCaption = result.Tables[0].Rows[0][""].ToString();

                    MemoryStream ms = new MemoryStream((byte[])result.Tables[0].Rows[0]["Image"]);
                    byte[] bytes = ms.ToArray();
                    ClaimButtonNewImage = bytes;

                    DataSet descDS = new DataSet();
                    List<SqlParameter> descParams = new List<SqlParameter>();
                    descParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                    descParams.Add(new SqlParameter("@PromotionID", PromotionID));
                    descDS = DataAcess.ExecuteQuerySP("PEC.TODO", descParams);


                    if(descDS.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < descDS.Tables[0].Rows.Count; i++)
                        {
                            ResultDescriptionBonus newRDB = new ResultDescriptionBonus();
                            newRDB.ResultCaption = descDS.Tables[0].Rows[i][""].ToString();
                            newRDB.ResultUpdatedElement = descDS.Tables[0].Rows[i][""].ToString();

                            MemoryStream msResult = new MemoryStream((byte[])descDS.Tables[0].Rows[i]["Image"]);
                            byte[] resultBytes = msResult.ToArray();
                            newRDB.ResultImage = resultBytes;

                            DataSet dP = new DataSet();
                            List<SqlParameter> pointsParams = new List<SqlParameter>();
                            pointsParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                            pointsParams.Add(new SqlParameter("@PromotionID", PromotionID));
                            dP = DataAcess.ExecuteQuerySP("PEC.TODO", pointsParams);
                            if(dP.Tables[0].Rows.Count > 0)
                            {
                                for(int j = 0; j < dP.Tables[0].Rows.Count; j++)
                                {
                                    DataPoint newPoint = new DataPoint();
                                    newPoint.DataPointCaption = dP.Tables[0].Rows[i]["DataPointCaption"].ToString();
                                    newPoint.DataPointData = dP.Tables[0].Rows[i]["DataPointData"].ToString();
                                    newRDB.DataPoints.Add(newPoint);
                                }
                            }
                            else
                            {
                                newRDB.DataPoints = null;
                            }

                        }
                    }
                    else
                    {
                        ResultDescriptions = null;
                    }

                }
                else
                {
                    RemoveData();
                }
            }
            catch (SqlException ex)
            {
                string errorMessage = ex.Message;
                RemoveData();
            }
        }
    }

    [Serializable]
    public class ListEntriesInNextDrawReturn : Default
    {
        private int promotionID;
        private DateTime nextDrawDate;
        private DateTime nextDrawTime;
        private int entriesForNextDraw;
        private bool isDrumPopulated;
        private List<string> entryNumbers;
        private string specialMessage;
        #region publics
        public int PromotionID
        {
            get
            {
                return promotionID;
            }

            set
            {
                promotionID = value;
            }
        }

        public DateTime NextDrawDate
        {
            get
            {
                return nextDrawDate;
            }

            set
            {
                nextDrawDate = value;
            }
        }

        public DateTime NextDrawTime
        {
            get
            {
                return nextDrawTime;
            }

            set
            {
                nextDrawTime = value;
            }
        }

        public int EntriesForNextDraw
        {
            get
            {
                return entriesForNextDraw;
            }

            set
            {
                entriesForNextDraw = value;
            }
        }

        public bool IsDrumPopulated
        {
            get
            {
                return isDrumPopulated;
            }

            set
            {
                isDrumPopulated = value;
            }
        }

        public List<string> EntryNumbers
        {
            get
            {
                return entryNumbers;
            }

            set
            {
                entryNumbers = value;
            }
        }

        public string SpecialMessage
        {
            get
            {
                return specialMessage;
            }

            set
            {
                specialMessage = value;
            }
        }

        #endregion
        private void RemoveData()
        {
            PromotionID = -1;
            EntriesForNextDraw = -1;
            IsDrumPopulated = false;
            EntryNumbers = null;
            SpecialMessage = null;
        }
        public void DBListEntriesInNextDraw(string mobile, int promotionID)
        {
            try
            {
                PromotionID = promotionID;
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                spParams.Add(new SqlParameter("@PromotionID", PromotionID));
                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);

                if(result.Tables[0].Rows.Count > 0)
                {
                    NextDrawDate = Convert.ToDateTime(result.Tables[0].Rows[0]["NextDrawDate"].ToString());
                    NextDrawTime = Convert.ToDateTime(result.Tables[0].Rows[0]["NextDrawTime"].ToString());
                    EntriesForNextDraw = Convert.ToInt32(result.Tables[0].Rows[0]["EntriesForNextDraw"].ToString());
                    IsDrumPopulated = Convert.ToBoolean(result.Tables[0].Rows[0]["IsPopulated"].ToString());
                    SpecialMessage = result.Tables[0].Rows[0]["SpecialMessage"].ToString();
                    if(IsDrumPopulated)
                    {
                        DataSet entryDS = new DataSet();
                        entryDS = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);
                        if(entryDS.Tables[0].Rows.Count > 0)
                        {
                            for(int i = 0; i < entryDS.Tables[0].Rows.Count; i++)
                            {
                                string entryNum = entryDS.Tables[0].Rows[i]["EntryNumber"].ToString();
                                EntryNumbers.Add(entryNum);
                            }
                        }
                        else
                        {
                            EntryNumbers = null;
                        }
                    }
                    else
                    {
                        EntryNumbers = null;
                    }
                }
                else
                {
                    RemoveData();
                }

            }
            catch (SqlException ex)
            {
                string errorMessage = ex.Message;
                RemoveData();
            }
        }
    }
    #endregion

    #region My Offers Screen
    [Serializable]
    public class GetOffersScreenWrapperReturn : Default
    {
        private string line1Caption;
        private string line1Data;
        private string line2Caption;
        private string line2Data;
        private List<OfferButton> offerButtons;
        
        private void RemoveData()
        {
            Line1Caption = null;
            Line1Data = null;
            Line2Caption = null;
            Line2Data = null;
            OfferButtons = null;
        }
        #region Publics
        public string Line1Caption
        {
            get
            {
                return line1Caption;
            }

            set
            {
                line1Caption = value;
            }
        }

        public string Line1Data
        {
            get
            {
                return line1Data;
            }

            set
            {
                line1Data = value;
            }
        }

        public string Line2Caption
        {
            get
            {
                return line2Caption;
            }

            set
            {
                line2Caption = value;
            }
        }

        public string Line2Data
        {
            get
            {
                return line2Data;
            }

            set
            {
                line2Data = value;
            }
        }

        public List<OfferButton> OfferButtons
        {
            get
            {
                return offerButtons;
            }

            set
            {
                offerButtons = value;
            }
        }

        public class OfferButton
        {
            private string buttonCaption;
            private int buttonOrdPos;
            private int buttonOfferID;
            private byte[] buttonImage;

            public string ButtonCaption
            {
                get
                {
                    return buttonCaption;
                }

                set
                {
                    buttonCaption = value;
                }
            }

            public int ButtonOrdPos
            {
                get
                {
                    return buttonOrdPos;
                }

                set
                {
                    buttonOrdPos = value;
                }
            }

            public int ButtonOfferID
            {
                get
                {
                    return buttonOfferID;
                }

                set
                {
                    buttonOfferID = value;
                }
            }

            public byte[] ButtonImage
            {
                get
                {
                    return buttonImage;
                }

                set
                {
                    buttonImage = value;
                }
            }
        }
        #endregion
        public void DBGetOffersScreenWrapper(string mobile)
        {
            try
            {
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);

                if(result.Tables[0].Rows.Count > 0)
                {
                    Line1Caption = result.Tables[0].Rows[0][""].ToString();
                    Line1Data = result.Tables[0].Rows[0][""].ToString();
                    Line2Caption = result.Tables[0].Rows[0][""].ToString(); 
                    Line2Data = result.Tables[0].Rows[0][""].ToString();

                    DataSet buttonDS = new DataSet();
                    List<SqlParameter> buttonParams = new List<SqlParameter>();
                    buttonParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                    buttonDS = DataAcess.ExecuteQuerySP("PEC.TODO", buttonParams);
                    if(buttonDS.Tables[0].Rows.Count > 0)
                    {
                        for(int i = 0; i < buttonDS.Tables[0].Rows.Count; i++)
                        {
                            OfferButton newButton = new OfferButton();
                            newButton.ButtonCaption = buttonDS.Tables[0].Rows[i]["ButtonCaption"].ToString();
                            newButton.ButtonOrdPos = Convert.ToInt32(buttonDS.Tables[0].Rows[i]["ButtonOrdPos"].ToString());
                            newButton.ButtonOfferID = Convert.ToInt32(buttonDS.Tables[0].Rows[i]["ButtonOfferID"].ToString());
                            MemoryStream ms = new MemoryStream((byte[])buttonDS.Tables[0].Rows[i]["Image"]);
                            byte[] bytes = ms.ToArray();
                            newButton.ButtonImage = bytes;
                            OfferButtons.Add(newButton);
                        }
                    }
                    else
                    {
                        OfferButtons = null;
                    }
                }
                else
                {
                    RemoveData();
                }
            }
            catch (SqlException ex)
            {
                string errorMessage = ex.Message;
                RemoveData();
            }
        }
    }

    [Serializable]
    public class GetOfferDetailsReturn : Default
    {
        private int offerID;
        private string headerCaptionLine1;
        private string headerCaptionLine2;
        private byte[] offerLargeImage;
        private string offerCaption;
        private bool offerBarcodeDisplayed;
        private byte[] offerBarcode;
        private bool displayOptions;
        private List<Option> offerOptions;
        private string footerCaptionLine1;
        private string footerCaptionLine2;
        #region Publics
        public int OfferID
        {
            get
            {
                return offerID;
            }

            set
            {
                offerID = value;
            }
        }

        public string HeaderCaptionLine1
        {
            get
            {
                return headerCaptionLine1;
            }

            set
            {
                headerCaptionLine1 = value;
            }
        }

        public string HeaderCaptionLine2
        {
            get
            {
                return headerCaptionLine2;
            }

            set
            {
                headerCaptionLine2 = value;
            }
        }

        public byte[] OfferLargeImage
        {
            get
            {
                return offerLargeImage;
            }

            set
            {
                offerLargeImage = value;
            }
        }

        public string OfferCaption
        {
            get
            {
                return offerCaption;
            }

            set
            {
                offerCaption = value;
            }
        }

        public bool OfferBarcodeDisplayed
        {
            get
            {
                return offerBarcodeDisplayed;
            }

            set
            {
                offerBarcodeDisplayed = value;
            }
        }

        public byte[] OfferBarcode
        {
            get
            {
                return offerBarcode;
            }

            set
            {
                offerBarcode = value;
            }
        }

        public bool DisplayOptions
        {
            get
            {
                return displayOptions;
            }

            set
            {
                displayOptions = value;
            }
        }

        public List<Option> OfferOptions
        {
            get
            {
                return offerOptions;
            }

            set
            {
                offerOptions = value;
            }
        }

        public string FooterCaptionLine1
        {
            get
            {
                return footerCaptionLine1;
            }

            set
            {
                footerCaptionLine1 = value;
            }
        }

        public string FooterCaptionLine2
        {
            get
            {
                return footerCaptionLine2;
            }

            set
            {
                footerCaptionLine2 = value;
            }
        }

        #endregion
        public class Option
        {
            private string optionCaption;
            private byte[] optionImage;
            private bool optionExecutable;
            private int optionReferenceID;
            #region Public
            public string OptionCaption
            {
                get
                {
                    return optionCaption;
                }

                set
                {
                    optionCaption = value;
                }
            }

            public byte[] OptionImage
            {
                get
                {
                    return optionImage;
                }

                set
                {
                    optionImage = value;
                }
            }

            public bool OptionExecutable
            {
                get
                {
                    return optionExecutable;
                }

                set
                {
                    optionExecutable = value;
                }
            }

            public int OptionReferenceID
            {
                get
                {
                    return optionReferenceID;
                }

                set
                {
                    optionReferenceID = value;
                }
            }
            #endregion
        }
        private void RemoveData()
        {
            OfferID = -1;
            HeaderCaptionLine1 = null;
            HeaderCaptionLine2 = null;
            OfferLargeImage = null;
            OfferCaption = null;
            OfferBarcodeDisplayed = false;
            OfferBarcode = null;
            DisplayOptions = false;
            OfferOptions = null;
            FooterCaptionLine1 = null;
            FooterCaptionLine2 = null;
        }

        public void DBGetOfferDetails(string mobile, int offerID)
        {
            try
            {
                OfferID = offerID;
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                spParams.Add(new SqlParameter("@OfferID", OfferID));

                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);

                if(result.Tables[0].Rows.Count > 0)
                {
                    HeaderCaptionLine1 = result.Tables[0].Rows[0][""].ToString();
                    HeaderCaptionLine2 = result.Tables[0].Rows[0][""].ToString();
                    OfferCaption = result.Tables[0].Rows[0][""].ToString();
                    FooterCaptionLine1 = result.Tables[0].Rows[0][""].ToString();
                    FooterCaptionLine2 = result.Tables[0].Rows[0][""].ToString();
                    OfferID = Convert.ToInt32(result.Tables[0].Rows[0][""].ToString());
                    DisplayOptions = Convert.ToBoolean(result.Tables[0].Rows[0][""].ToString());

                    MemoryStream ms = new MemoryStream((byte[])result.Tables[0].Rows[0][""]);
                    byte[] bytes = ms.ToArray();
                    OfferLargeImage = bytes;

                    MemoryStream barcodeMS = new MemoryStream((byte[])result.Tables[0].Rows[0][""]);
                    byte[] barcodeBytes = barcodeMS.ToArray();
                    OfferBarcode = barcodeBytes;

                    if(DisplayOptions)
                    {
                        DataSet optionDS = new DataSet();
                        List<SqlParameter> optionParams = new List<SqlParameter>();
                        optionParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                        optionParams.Add(new SqlParameter("@OfferID", OfferID));

                        optionDS = DataAcess.ExecuteQuerySP("PEC.TODO", optionParams);
                        if (optionDS.Tables[0].Rows.Count > 0)
                        {
                            for(int i = 0; i < optionDS.Tables[0].Rows.Count; i++)
                            {
                                Option newOption = new Option();
                                newOption.OptionCaption = result.Tables[0].Rows[i][""].ToString();
                                MemoryStream optionMS = new MemoryStream((byte[])result.Tables[0].Rows[i][""]);
                                byte[] optionBytes = optionMS.ToArray();
                                newOption.OptionImage = optionBytes;
                                newOption.OptionExecutable = Convert.ToBoolean(result.Tables[0].Rows[i][""].ToString());
                                newOption.OptionReferenceID = Convert.ToInt32(result.Tables[0].Rows[i][""].ToString());

                                OfferOptions.Add(newOption);
                            }
                        }
                        else
                        {
                            OfferOptions = null;
                        }
                    }
                    else
                    {
                        OfferOptions = null;
                    }
                }
                else
                {
                    RemoveData();
                }
            }
            catch (SqlException ex)
            {
                string errorMessage = ex.Message;
                RemoveData();
            }
        }
    }

    [Serializable]
    public class ExecuteRedemptionOptionReturn : Default
    {
        private int offerID;
        private int optionReferenceID;
        private bool redemptionExecutionSucess;
        private string headerCaptionLine1;
        private string headerCaptionLine2;
        private string bodyCaptionLine1;
        private string bodyCaptionLine2;
        private byte[] bodyImage1;
        private byte[] bodyImage2;
        private string footerCaptionLine1;
        private string footerCaptionLine2;
        #region Publics
        public int OfferID
        {
            get
            {
                return offerID;
            }

            set
            {
                offerID = value;
            }
        }

        public int OptionReferenceID
        {
            get
            {
                return optionReferenceID;
            }

            set
            {
                optionReferenceID = value;
            }
        }

        public bool RedemptionExecutionSucess
        {
            get
            {
                return redemptionExecutionSucess;
            }

            set
            {
                redemptionExecutionSucess = value;
            }
        }

        public string HeaderCaptionLine1
        {
            get
            {
                return headerCaptionLine1;
            }

            set
            {
                headerCaptionLine1 = value;
            }
        }

        public string HeaderCaptionLine2
        {
            get
            {
                return headerCaptionLine2;
            }

            set
            {
                headerCaptionLine2 = value;
            }
        }

        public string BodyCaptionLine1
        {
            get
            {
                return bodyCaptionLine1;
            }

            set
            {
                bodyCaptionLine1 = value;
            }
        }

        public string BodyCaptionLine2
        {
            get
            {
                return bodyCaptionLine2;
            }

            set
            {
                bodyCaptionLine2 = value;
            }
        }

        public byte[] BodyImage1
        {
            get
            {
                return bodyImage1;
            }

            set
            {
                bodyImage1 = value;
            }
        }

        public byte[] BodyImage2
        {
            get
            {
                return bodyImage2;
            }

            set
            {
                bodyImage2 = value;
            }
        }

        public string FooterCaptionLine1
        {
            get
            {
                return footerCaptionLine1;
            }

            set
            {
                footerCaptionLine1 = value;
            }
        }

        public string FooterCaptionLine2
        {
            get
            {
                return footerCaptionLine2;
            }

            set
            {
                footerCaptionLine2 = value;
            }
        }
        #endregion

        private void RemoveData()
        {
            OfferID = -1;
            OptionReferenceID = -1;
            RedemptionExecutionSucess = false;
            HeaderCaptionLine1 = null;
            BodyCaptionLine1 = null;
            HeaderCaptionLine2 = null;
            BodyCaptionLine2 = null;
            FooterCaptionLine1 = null;
            FooterCaptionLine2 = null;
            BodyImage1 = null;
            BodyImage2 = null;
        }
        public void DBExecuteRedemptionOption(string mobile, int offerID, int optionReferenceID)
        {
            try
            {
                OfferID = offerID;
                OptionReferenceID = optionReferenceID;
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                spParams.Add(new SqlParameter("@OfferID", OfferID));
                spParams.Add(new SqlParameter("@OptionReferenceID", OptionReferenceID));

                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);

                if (result.Tables[0].Rows.Count > 0)
                {
                    RedemptionExecutionSucess = true;
                    HeaderCaptionLine1 = result.Tables[0].Rows[0][""].ToString();
                    BodyCaptionLine1 = result.Tables[0].Rows[0][""].ToString();
                    HeaderCaptionLine2 = result.Tables[0].Rows[0][""].ToString();
                    BodyCaptionLine2 = result.Tables[0].Rows[0][""].ToString();
                    FooterCaptionLine1 = result.Tables[0].Rows[0][""].ToString();
                    FooterCaptionLine2 = result.Tables[0].Rows[0][""].ToString();

                    MemoryStream ms = new MemoryStream((byte[])result.Tables[0].Rows[0][""]);
                    byte[] bytes = ms.ToArray();
                    if(bytes != null)
                    {
                        BodyImage1 = bytes;
                    }
                    else
                    {
                        BodyImage1 = null;
                    }

                    MemoryStream bodyImage2MS = new MemoryStream((byte[])result.Tables[0].Rows[0][""]);
                    byte[] bodyImage2Bytes = bodyImage2MS.ToArray();
                    if (bodyImage2Bytes != null)
                    {
                        BodyImage2 = bodyImage2Bytes;
                    }
                    else
                    {
                        BodyImage2 = null;
                    }
                    BodyImage2 = bodyImage2Bytes;

                }
                else
                {
                    RemoveData();
                    RedemptionExecutionSucess = false;
                }
            }
            catch (SqlException ex)
            {
                string errorMessage = ex.Message;
                RemoveData();
            }
        }
    }

    [Serializable]
    public class ViewOfferRedemptionHistoryReturn : Default
    {
        private string headerCaptionLine1;
        private string headerCaptionLine2;
        private bool offerHistoryExists;
        private List<Offer> offerHistory;
        private string footerCaptionLine1;
        private string footerCaptionLine2;
        #region Publics
        public string HeaderCaptionLine1
        {
            get
            {
                return headerCaptionLine1;
            }

            set
            {
                headerCaptionLine1 = value;
            }
        }

        public string HeaderCaptionLine2
        {
            get
            {
                return headerCaptionLine2;
            }

            set
            {
                headerCaptionLine2 = value;
            }
        }

        public bool OfferHistoryExists
        {
            get
            {
                return offerHistoryExists;
            }

            set
            {
                offerHistoryExists = value;
            }
        }

        public List<Offer> OfferHistory
        {
            get
            {
                return offerHistory;
            }

            set
            {
                offerHistory = value;
            }
        }

        public string FooterCaptionLine1
        {
            get
            {
                return footerCaptionLine1;
            }

            set
            {
                footerCaptionLine1 = value;
            }
        }

        public string FooterCaptionLine2
        {
            get
            {
                return footerCaptionLine2;
            }

            set
            {
                footerCaptionLine2 = value;
            }
        }

        #endregion

        public class Offer
        {
            private string description;
            private string value;
            private DateTime expiresDate;
            private DateTime issuedDate;
            private string prizeType;
            private bool isExpired;
            private bool isRedeemed;
            private bool hasBarcode;
            private byte[] barcode;
            #region Publics
            public string Description
            {
                get
                {
                    return description;
                }

                set
                {
                    description = value;
                }
            }

            public string Value
            {
                get
                {
                    return value;
                }

                set
                {
                    this.value = value;
                }
            }

            public DateTime ExpiresDate
            {
                get
                {
                    return expiresDate;
                }

                set
                {
                    expiresDate = value;
                }
            }

            public DateTime IssuedDate
            {
                get
                {
                    return issuedDate;
                }

                set
                {
                    issuedDate = value;
                }
            }

            public string PrizeType
            {
                get
                {
                    return prizeType;
                }

                set
                {
                    prizeType = value;
                }
            }

            public bool IsExpired
            {
                get
                {
                    return isExpired;
                }

                set
                {
                    isExpired = value;
                }
            }

            public bool IsRedeemed
            {
                get
                {
                    return isRedeemed;
                }

                set
                {
                    isRedeemed = value;
                }
            }

            public bool HasBarcode
            {
                get
                {
                    return hasBarcode;
                }

                set
                {
                    hasBarcode = value;
                }
            }

            public byte[] Barcode
            {
                get
                {
                    return barcode;
                }

                set
                {
                    barcode = value;
                }
            }

            #endregion

        }

        private void RemoveData()
        {
            HeaderCaptionLine1 = null;
            HeaderCaptionLine2 = null;
            FooterCaptionLine1 = null;
            FooterCaptionLine2 = null;
            OfferHistory = null;
            OfferHistoryExists = false;
        }
        //SQL DB Retrieval Function:
        public void DBViewOfferRedemptionHistoryReturn(string mobile)
        {
            try
            {
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));

                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);

                if(result.Tables[0].Rows.Count > 0)
                {
                    HeaderCaptionLine1 = result.Tables[0].Rows[0]["HeaderCaption"].ToString();
                    HeaderCaptionLine2 = result.Tables[0].Rows[0]["HeaderCaptionLine2"].ToString();
                    OfferHistoryExists = Convert.ToBoolean(result.Tables[0].Rows[0]["OfferHistoryExists"].ToString());
                    FooterCaptionLine1 = result.Tables[0].Rows[0]["HeaderCaption"].ToString();
                    FooterCaptionLine2 = result.Tables[0].Rows[0]["HeaderCaption"].ToString();
                    if (OfferHistoryExists)
                    {
                        DataSet oHDS = new DataSet();
                        List<SqlParameter> offerHistoryParams = new List<SqlParameter>();
                        offerHistoryParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));

                        oHDS = DataAcess.ExecuteQuerySP("PEC.TODO", offerHistoryParams);
                        for(int i = 0; i < oHDS.Tables[0].Rows.Count; i++)
                        {
                            Offer newOffer = new Offer();
                            newOffer.Description = oHDS.Tables[0].Rows[i][""].ToString();
                            newOffer.Value = oHDS.Tables[0].Rows[i][""].ToString();
                            newOffer.ExpiresDate = Convert.ToDateTime(oHDS.Tables[0].Rows[i][""].ToString());
                            newOffer.IssuedDate = Convert.ToDateTime(oHDS.Tables[0].Rows[i][""].ToString());
                            newOffer.PrizeType = oHDS.Tables[0].Rows[i][""].ToString();
                            newOffer.IsExpired = Convert.ToBoolean(oHDS.Tables[0].Rows[i][""].ToString());
                            newOffer.IsRedeemed = Convert.ToBoolean(oHDS.Tables[0].Rows[i][""].ToString());
                            newOffer.HasBarcode = Convert.ToBoolean(oHDS.Tables[0].Rows[i][""].ToString());
                            if(newOffer.HasBarcode)
                            {
                                MemoryStream ms = new MemoryStream((byte[])oHDS.Tables[0].Rows[i][""]);
                                byte[] bytes = ms.ToArray();
                                newOffer.Barcode = bytes;
                            }
                            else
                            {
                                newOffer.Barcode = null;
                            }

                            OfferHistory.Add(newOffer);
                        }
                    }
                }
                else
                {
                    RemoveData();
                }
            }   
            catch (SqlException ex)
            {
                string errorMessage = ex.Message;

            }
        }
    }
    #endregion

    #region My Events Screen
    [Serializable]
    public class GetEventsScreenWrapperReturn : Default
    {
        private string line1Caption;
        private string line1Data;
        private string line2Caption;
        private string line2Data;
        private List<EventButton> buttons;
        #region Publics
        public string Line1Caption
        {
            get
            {
                return line1Caption;
            }

            set
            {
                line1Caption = value;
            }
        }

        public string Line1Data
        {
            get
            {
                return line1Data;
            }

            set
            {
                line1Data = value;
            }
        }

        public string Line2Caption
        {
            get
            {
                return line2Caption;
            }

            set
            {
                line2Caption = value;
            }
        }

        public string Line2Data
        {
            get
            {
                return line2Data;
            }

            set
            {
                line2Data = value;
            }
        }

        public List<EventButton> Buttons
        {
            get
            {
                return buttons;
            }

            set
            {
                buttons = value;
            }
        }
        #endregion
        public class EventButton
        {
            private string buttonCaption;
            private int buttonOrdPos;
            private int buttonEventID;
            private byte[] buttonImage;
            #region Publics
            public string ButtonCaption
            {
                get
                {
                    return buttonCaption;
                }

                set
                {
                    buttonCaption = value;
                }
            }

            public int ButtonOrdPos
            {
                get
                {
                    return buttonOrdPos;
                }

                set
                {
                    buttonOrdPos = value;
                }
            }

            public int ButtonEventID
            {
                get
                {
                    return buttonEventID;
                }

                set
                {
                    buttonEventID = value;
                }
            }

            public byte[] ButtonImage
            {
                get
                {
                    return buttonImage;
                }

                set
                {
                    buttonImage = value;
                }
            }
            #endregion
        }

        private void RemoveData()
        {
            Line1Caption = null;
            Line1Data = null;
            Line2Caption = null;
            Line2Data = null;
            Buttons = null;
        }

        public void DBGetEventsScreenWrapper(string mobile)
        {
            try
            { 
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));

                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);

                if (result.Tables[0].Rows.Count > 0)
                {
                    Line1Caption = result.Tables[0].Rows[0][""].ToString();
                    Line1Data = result.Tables[0].Rows[0][""].ToString();
                    Line2Caption = result.Tables[0].Rows[0][""].ToString();
                    Line2Data = result.Tables[0].Rows[0][""].ToString();
                    

                    DataSet buttonDS = new DataSet();
                    List<SqlParameter> buttonParams = new List<SqlParameter>();
                    buttonParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                    buttonDS = DataAcess.ExecuteQuerySP("PEC.TODO", buttonParams);

                    if (buttonDS.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < buttonDS.Tables[0].Rows.Count; i++)
                        {
                            EventButton newButton = new EventButton();
                            newButton.ButtonCaption = result.Tables[0].Rows[i][""].ToString();
                            newButton.ButtonOrdPos = Convert.ToInt32(result.Tables[0].Rows[i][""].ToString());
                            newButton.ButtonEventID = Convert.ToInt32(result.Tables[0].Rows[i][""].ToString());
                            MemoryStream ButtonMS = new MemoryStream((byte[])result.Tables[0].Rows[i][""]);
                            byte[] ButtonBytes = ButtonMS.ToArray();
                            newButton.ButtonImage = ButtonBytes;
                            Buttons.Add(newButton);
                        }
                    }
                    else
                    {
                        Buttons = null;
                    }
                }
                else
                {
                    RemoveData();
                }
            }
            catch (SqlException ex)
            {
                string errorMessage = ex.Message;
                RemoveData();
            }
        }
    }


    [Serializable]
    public class GetEventDetailsReturn : Default
    {
        private int eventID;
        private string headerCaptionLine1;
        private string headerCaptionLine2;
        private byte[] eventLargeImage;
        private string eventCaption;
        private DateTime eventStartDate;
        private DateTime eventStartTime;
        private DateTime eventEndDate;
        private DateTime eventEndTime;
        private bool displayOptions;
        private List<EventOption> eventOptions;
        private string footerCaptionLine1;
        private string footerCaptionLine2;
        #region PUBLICS
        public int EventID
        {
            get
            {
                return eventID;
            }

            set
            {
                eventID = value;
            }
        }

        public string HeaderCaptionLine1
        {
            get
            {
                return headerCaptionLine1;
            }

            set
            {
                headerCaptionLine1 = value;
            }
        }

        public string HeaderCaptionLine2
        {
            get
            {
                return headerCaptionLine2;
            }

            set
            {
                headerCaptionLine2 = value;
            }
        }

        public byte[] EventLargeImage
        {
            get
            {
                return eventLargeImage;
            }

            set
            {
                eventLargeImage = value;
            }
        }

        public string EventCaption
        {
            get
            {
                return eventCaption;
            }

            set
            {
                eventCaption = value;
            }
        }

        public DateTime EventStartDate
        {
            get
            {
                return eventStartDate;
            }

            set
            {
                eventStartDate = value;
            }
        }

        public DateTime EventStartTime
        {
            get
            {
                return eventStartTime;
            }

            set
            {
                eventStartTime = value;
            }
        }

        public DateTime EventEndDate
        {
            get
            {
                return eventEndDate;
            }

            set
            {
                eventEndDate = value;
            }
        }

        public DateTime EventEndTime
        {
            get
            {
                return eventEndTime;
            }

            set
            {
                eventEndTime = value;
            }
        }

        public bool DisplayOptions
        {
            get
            {
                return displayOptions;
            }

            set
            {
                displayOptions = value;
            }
        }

        public List<EventOption> EventOptions
        {
            get
            {
                return eventOptions;
            }

            set
            {
                eventOptions = value;
            }
        }

        public string FooterCaptionLine1
        {
            get
            {
                return footerCaptionLine1;
            }

            set
            {
                footerCaptionLine1 = value;
            }
        }

        public string FooterCaptionLine2
        {
            get
            {
                return footerCaptionLine2;
            }

            set
            {
                footerCaptionLine2 = value;
            }
        }

        #endregion

        public class EventOption
        {
            private string optionCaption;
            private byte[] optionImage;
            private bool optionExecutable;
            private int optionReferenceID;

            public string OptionCaption
            {
                get
                {
                    return optionCaption;
                }

                set
                {
                    optionCaption = value;
                }
            }

            public byte[] OptionImage
            {
                get
                {
                    return optionImage;
                }

                set
                {
                    optionImage = value;
                }
            }

            public bool OptionExecutable
            {
                get
                {
                    return optionExecutable;
                }

                set
                {
                    optionExecutable = value;
                }
            }

            public int OptionReferenceID
            {
                get
                {
                    return optionReferenceID;
                }

                set
                {
                    optionReferenceID = value;
                }
            }
        }

        private void RemoveData()
        {
            EventID = -1;
            HeaderCaptionLine1 = null;
            HeaderCaptionLine2 = null;
            EventLargeImage = null;
            EventCaption = null;
            DisplayOptions = false;
            FooterCaptionLine1 = null;
            FooterCaptionLine2 = null;
        }
        public void DBGetEventDetails(string mobile, int eventID)
        {
            try
            {
                EventID = eventID;
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                spParams.Add(new SqlParameter("@EventID", eventID));
                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);

                if (result.Tables[0].Rows.Count > 0)
                {
                    HeaderCaptionLine1 = result.Tables[0].Rows[0][""].ToString();
                    HeaderCaptionLine2 = result.Tables[0].Rows[0][""].ToString();
                    EventCaption = result.Tables[0].Rows[0][""].ToString();
                    EventStartDate = Convert.ToDateTime(result.Tables[0].Rows[0][""]);
                    EventStartTime = Convert.ToDateTime(result.Tables[0].Rows[0][""]);
                    EventEndDate = Convert.ToDateTime(result.Tables[0].Rows[0][""]);
                    EventEndTime = Convert.ToDateTime(result.Tables[0].Rows[0][""]);
                    DisplayOptions = Convert.ToBoolean(result.Tables[0].Rows[0][""]);
                    FooterCaptionLine1 = result.Tables[0].Rows[0][""].ToString();
                    FooterCaptionLine2 = result.Tables[0].Rows[0][""].ToString();
                    MemoryStream ms = new MemoryStream((byte[])result.Tables[0].Rows[0]["LargeImage"]);
                    byte[] largeImage = ms.ToArray();
                    EventLargeImage = largeImage;

                    if(DisplayOptions)
                    {
                        DataSet optionDS = new DataSet();
                        List<SqlParameter> optionParams = new List<SqlParameter>();
                        optionParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                        optionParams.Add(new SqlParameter("@EventID", EventID));
                        optionDS = DataAcess.ExecuteQuerySP("PEC.TODO", optionParams);
                        if(optionDS.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < optionDS.Tables[0].Rows.Count; i++)
                            {
                                EventOption newEvent = new EventOption();
                                newEvent.OptionCaption = optionDS.Tables[0].Rows[i]["OptionCaption"].ToString();
                                newEvent.OptionExecutable = Convert.ToBoolean(optionDS.Tables[0].Rows[i]["OptionExecutable"].ToString());
                                MemoryStream optionMS = new MemoryStream((byte[])optionDS.Tables[0].Rows[i]["OptionImage"]);
                                byte[] optionImageByte = optionMS.ToArray();
                                newEvent.OptionImage = optionImageByte;
                                newEvent.OptionReferenceID = Convert.ToInt32(optionDS.Tables[0].Rows[i]["ReferenceID"].ToString());
                                EventOptions.Add(newEvent);
                            }
                        }
                        else
                        {
                            EventOptions = null;
                        }
                    }
                    else
                    {
                        EventOptions = null;
                    }
                }
                else
                {
                    RemoveData();
                }
            }
            catch (SqlException ex)
            {
                string errorMessage = ex.Message;
                RemoveData();
            }
        }
    }

    [Serializable]
    public class EnrollGuestInEventReturn : Default
    {
        private int eventID;
        private int optionReferenceID;
        private string headerCaptionLine1;
        private string headerCaptionLine2;
        private bool isEnrolled;
        private string confirmationNumber;
        private byte[] bodyImage1;
        private byte[] bodyImage2;
        private string footerCaptionLine1;
        private string footerCaptionLine2;
        #region PUBLICS
        public int EventID
        {
            get
            {
                return eventID;
            }

            set
            {
                eventID = value;
            }
        }

        public int OptionReferenceID
        {
            get
            {
                return optionReferenceID;
            }

            set
            {
                optionReferenceID = value;
            }
        }

        public string HeaderCaptionLine1
        {
            get
            {
                return headerCaptionLine1;
            }

            set
            {
                headerCaptionLine1 = value;
            }
        }

        public string HeaderCaptionLine2
        {
            get
            {
                return headerCaptionLine2;
            }

            set
            {
                headerCaptionLine2 = value;
            }
        }

        public bool IsEnrolled
        {
            get
            {
                return isEnrolled;
            }

            set
            {
                isEnrolled = value;
            }
        }

        public string ConfirmationNumber
        {
            get
            {
                return confirmationNumber;
            }

            set
            {
                confirmationNumber = value;
            }
        }

        public byte[] BodyImage1
        {
            get
            {
                return bodyImage1;
            }

            set
            {
                bodyImage1 = value;
            }
        }

        public byte[] BodyImage2
        {
            get
            {
                return bodyImage2;
            }

            set
            {
                bodyImage2 = value;
            }
        }

        public string FooterCaptionLine1
        {
            get
            {
                return footerCaptionLine1;
            }

            set
            {
                footerCaptionLine1 = value;
            }
        }

        public string FooterCaptionLine2
        {
            get
            {
                return footerCaptionLine2;
            }

            set
            {
                footerCaptionLine2 = value;
            }
        }
        #endregion
        private void RemoveData()
        {
            EventID = -1;
            OptionReferenceID = -1;
            HeaderCaptionLine1 = null;
            HeaderCaptionLine2 = null;
            IsEnrolled = false;
            ConfirmationNumber = null;
            BodyImage1 = null;
            BodyImage2 = null;
            FooterCaptionLine1 = null;
            FooterCaptionLine2 = null;
        }
        public void DBEnrollGuestInEvent(string mobile, int eventID, int optionReferenceID)
        {
            try
            {
                EventID = eventID;
                OptionReferenceID = optionReferenceID;
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                spParams.Add(new SqlParameter("@EventID", eventID));
                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);
                HeaderCaptionLine1 = result.Tables[0].Rows[0]["HeaderCaptionLine1"].ToString();
                HeaderCaptionLine2 = result.Tables[0].Rows[0][""].ToString();
                IsEnrolled = Convert.ToBoolean(result.Tables[0].Rows[0][""].ToString());
                ConfirmationNumber = result.Tables[0].Rows[0][""].ToString();
                MemoryStream bodyImage1MS = new MemoryStream((byte[])result.Tables[0].Rows[0][""]);
                byte[] bodyImage1Byte = bodyImage1MS.ToArray();
                BodyImage1 = bodyImage1Byte;

                MemoryStream bodyImage2MS = new MemoryStream((byte[])result.Tables[0].Rows[0][""]);
                byte[] bodyImage2Byte = bodyImage2MS.ToArray();
                BodyImage2 = bodyImage2Byte;

                FooterCaptionLine1 = result.Tables[0].Rows[0][""].ToString();
                FooterCaptionLine2 = result.Tables[0].Rows[0][""].ToString();

            }
            catch (SqlException ex)
            {
                string errorMessage = ex.Message;
                RemoveData();
            }
        }
    }

    [Serializable]
    public class RequestTicketsToEventReturn : Default
    {
        private int eventID;
        private int optionReferenceID;
        private string headerCaptionLine1;
        private string headerCaptionLine2;
        private bool isEnrolled;
        private string confirmationNumber;
        private int ticketCountAwarded;
        private bool isOnWaitList;
        private string expectedResponseInterval;
        private byte[] bodyImage1;
        private byte[] bodyImage2;
        private string footerCaptionLine1;
        private string footerCaptionLine2;
        #region PUBLICS
        public int EventID
        {
            get
            {
                return eventID;
            }

            set
            {
                eventID = value;
            }
        }

        public int OptionReferenceID
        {
            get
            {
                return optionReferenceID;
            }

            set
            {
                optionReferenceID = value;
            }
        }

        public string HeaderCaptionLine1
        {
            get
            {
                return headerCaptionLine1;
            }

            set
            {
                headerCaptionLine1 = value;
            }
        }

        public string HeaderCaptionLine2
        {
            get
            {
                return headerCaptionLine2;
            }

            set
            {
                headerCaptionLine2 = value;
            }
        }

        public bool IsEnrolled
        {
            get
            {
                return isEnrolled;
            }

            set
            {
                isEnrolled = value;
            }
        }

        public string ConfirmationNumber
        {
            get
            {
                return confirmationNumber;
            }

            set
            {
                confirmationNumber = value;
            }
        }

        public int TicketCountAwarded
        {
            get
            {
                return ticketCountAwarded;
            }

            set
            {
                ticketCountAwarded = value;
            }
        }

        public bool IsOnWaitList
        {
            get
            {
                return isOnWaitList;
            }

            set
            {
                isOnWaitList = value;
            }
        }

        public string ExpectedResponseInterval
        {
            get
            {
                return expectedResponseInterval;
            }

            set
            {
                expectedResponseInterval = value;
            }
        }

        public byte[] BodyImage1
        {
            get
            {
                return bodyImage1;
            }

            set
            {
                bodyImage1 = value;
            }
        }

        public byte[] BodyImage2
        {
            get
            {
                return bodyImage2;
            }

            set
            {
                bodyImage2 = value;
            }
        }

        public string FooterCaptionLine1
        {
            get
            {
                return footerCaptionLine1;
            }

            set
            {
                footerCaptionLine1 = value;
            }
        }

        public string FooterCaptionLine2
        {
            get
            {
                return footerCaptionLine2;
            }

            set
            {
                footerCaptionLine2 = value;
            }
        }
        #endregion

        private void RemoveData()
        {
            EventID = -1;
            OptionReferenceID = -1;
            HeaderCaptionLine1 = null;
            HeaderCaptionLine2 = null;
            IsEnrolled = false;
            ConfirmationNumber = null;
            TicketCountAwarded = -1;
            IsOnWaitList = false;
            ExpectedResponseInterval = null;
            BodyImage1 = null;
            BodyImage2 = null;
            FooterCaptionLine1 = null;
            FooterCaptionLine2 = null;
        }
        public void DBRequestTicketsToEvent(string mobile, int eventID, int optionReferenceID, int ticketCountRequested)
        {
            try
            {
                EventID = eventID;
                OptionReferenceID = optionReferenceID;
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                spParams.Add(new SqlParameter("@EventID", eventID));
                spParams.Add(new SqlParameter("@OptionReferenceID", optionReferenceID));
                spParams.Add(new SqlParameter("@TicketCountRequested", ticketCountRequested));

                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);
                HeaderCaptionLine1 = result.Tables[0].Rows[0]["HeaderCaptionLine1"].ToString();
                HeaderCaptionLine2 = result.Tables[0].Rows[0][""].ToString();
                IsEnrolled = Convert.ToBoolean(result.Tables[0].Rows[0][""].ToString());
                ConfirmationNumber = result.Tables[0].Rows[0][""].ToString();
                MemoryStream bodyImage1MS = new MemoryStream((byte[])result.Tables[0].Rows[0][""]);
                byte[] bodyImage1Byte = bodyImage1MS.ToArray();
                BodyImage1 = bodyImage1Byte;

                MemoryStream bodyImage2MS = new MemoryStream((byte[])result.Tables[0].Rows[0][""]);
                byte[] bodyImage2Byte = bodyImage2MS.ToArray();
                BodyImage2 = bodyImage2Byte;

                FooterCaptionLine1 = result.Tables[0].Rows[0][""].ToString();
                FooterCaptionLine2 = result.Tables[0].Rows[0][""].ToString();

            }
            catch (SqlException ex)
            {
                string errorMessage = ex.Message;
                RemoveData();
            }
        }
    }

    [Serializable]
    public class PurchaseTicketsToEventWithPointsReturn : Default
    {
        private int eventID;
        private int optionReferenceID;
        private string headerCaptionLine1;
        private string headerCaptionLine2;
        private bool isPurchaseSuccessful;
        private float newPointsBalance;
        private bool isEnrolled;
        private string confirmationNumber;
        private int ticketCountAwarded;
        private bool isOnWaitList;
        private string expectedResponseInterval;
        private byte[] bodyImage1;
        private byte[] bodyImage2;
        private string footerCaptionLine1;
        private string footerCaptionLine2;
        #region PUBLICS
        public int EventID
        {
            get
            {
                return eventID;
            }

            set
            {
                eventID = value;
            }
        }

        public int OptionReferenceID
        {
            get
            {
                return optionReferenceID;
            }

            set
            {
                optionReferenceID = value;
            }
        }

        public string HeaderCaptionLine1
        {
            get
            {
                return headerCaptionLine1;
            }

            set
            {
                headerCaptionLine1 = value;
            }
        }

        public string HeaderCaptionLine2
        {
            get
            {
                return headerCaptionLine2;
            }

            set
            {
                headerCaptionLine2 = value;
            }
        }

        public bool IsPurchaseSuccessful
        {
            get
            {
                return isPurchaseSuccessful;
            }

            set
            {
                isPurchaseSuccessful = value;
            }
        }

        public float NewPointsBalance
        {
            get
            {
                return newPointsBalance;
            }

            set
            {
                newPointsBalance = value;
            }
        }

        public bool IsEnrolled
        {
            get
            {
                return isEnrolled;
            }

            set
            {
                isEnrolled = value;
            }
        }

        public string ConfirmationNumber
        {
            get
            {
                return confirmationNumber;
            }

            set
            {
                confirmationNumber = value;
            }
        }

        public int TicketCountAwarded
        {
            get
            {
                return ticketCountAwarded;
            }

            set
            {
                ticketCountAwarded = value;
            }
        }

        public bool IsOnWaitList
        {
            get
            {
                return isOnWaitList;
            }

            set
            {
                isOnWaitList = value;
            }
        }

        public string ExpectedResponseInterval
        {
            get
            {
                return expectedResponseInterval;
            }

            set
            {
                expectedResponseInterval = value;
            }
        }

        public byte[] BodyImage1
        {
            get
            {
                return bodyImage1;
            }

            set
            {
                bodyImage1 = value;
            }
        }

        public byte[] BodyImage2
        {
            get
            {
                return bodyImage2;
            }

            set
            {
                bodyImage2 = value;
            }
        }

        public string FooterCaptionLine1
        {
            get
            {
                return footerCaptionLine1;
            }

            set
            {
                footerCaptionLine1 = value;
            }
        }

        public string FooterCaptionLine2
        {
            get
            {
                return footerCaptionLine2;
            }

            set
            {
                footerCaptionLine2 = value;
            }
        }
        #endregion
        private void RemoveData()
        {
            EventID = -1;
            OptionReferenceID = -1;
            HeaderCaptionLine1 = null;
            HeaderCaptionLine2 = null;
            IsPurchaseSuccessful = false;
            NewPointsBalance = -1;
            IsEnrolled = false;
            ConfirmationNumber = null;
            TicketCountAwarded = -1;
            IsOnWaitList = false;
            ExpectedResponseInterval = null;
            BodyImage1 = null;
            BodyImage2 = null;
            FooterCaptionLine1 = null;
            FooterCaptionLine2 = null;
        }
        public void DBPurchaseTicketsToEventWithPoints(string mobile, int eventID, int optionReferenceID, int ticketCountRequested)
        {
            try
            {
                EventID = eventID;
                OptionReferenceID = optionReferenceID;
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);
                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                spParams.Add(new SqlParameter("@EventID", eventID));
                spParams.Add(new SqlParameter("@OptionReferenceID", optionReferenceID));
                spParams.Add(new SqlParameter("@TicketCountRequested", ticketCountRequested));

                result = DataAcess.ExecuteQuerySP("PEC.TODO", spParams);
                HeaderCaptionLine1 = result.Tables[0].Rows[0]["HeaderCaptionLine1"].ToString();
                HeaderCaptionLine2 = result.Tables[0].Rows[0][""].ToString();
                IsPurchaseSuccessful = Convert.ToBoolean(result.Tables[0].Rows[0][""].ToString());
                NewPointsBalance = float.Parse(result.Tables[0].Rows[0][""].ToString());
                IsEnrolled = Convert.ToBoolean(result.Tables[0].Rows[0][""].ToString());
                ConfirmationNumber = result.Tables[0].Rows[0][""].ToString();
                TicketCountAwarded = Convert.ToInt32(result.Tables[0].Rows[0][""].ToString());
                IsOnWaitList = Convert.ToBoolean(result.Tables[0].Rows[0][""].ToString());
                ExpectedResponseInterval = result.Tables[0].Rows[0][""].ToString();

                MemoryStream bodyImage1MS = new MemoryStream((byte[])result.Tables[0].Rows[0][""]);
                byte[] bodyImage1Byte = bodyImage1MS.ToArray();
                BodyImage1 = bodyImage1Byte;

                MemoryStream bodyImage2MS = new MemoryStream((byte[])result.Tables[0].Rows[0][""]);
                byte[] bodyImage2Byte = bodyImage2MS.ToArray();
                BodyImage2 = bodyImage2Byte;

                FooterCaptionLine1 = result.Tables[0].Rows[0][""].ToString();
                FooterCaptionLine2 = result.Tables[0].Rows[0][""].ToString();

            }
            catch (SqlException ex)
            {
                string errorMessage = ex.Message;
                RemoveData();
            }
        }
    }
    #endregion
}
