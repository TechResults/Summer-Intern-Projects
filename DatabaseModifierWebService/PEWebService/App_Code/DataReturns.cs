using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;
using PE.DataModifier;
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
    static class StoredProcedure
    {
        public const string GetGameInfoForPromotion = "";
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

        public string DBGetStoredPin(string mobile)
        {
            throw new NotImplementedException();
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

        // Generate a one way hash, API does not specify implementation
        public void GenerateOneWayHash()
        {
            //SHould set userToken = to something
            throw new NotImplementedException();
        }

        // Query Database for existing mobile number. Return true if mobile number exists, false if it does not.
        public bool checkIsRegistered(string mobile)
        {
            throw new NotImplementedException();
        }
    }
    

    [Serializable]
    public class ShowBalancesOnOpeningScreenResult
    {
        private bool _isValid;

        public bool isValid
        {
            get { return _isValid; }
            set { _isValid = value; }
        }

        public void DBGetAccountBalancesSet(string mobile)
        {
            throw new NotImplementedException();
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
        public string DBGetStoredPin(string mobile)
        {
            throw new NotImplementedException();
        }
        public void GenerateOneWayHash()
        {
            throw new NotImplementedException();
            userToken = "something";

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
        //Gets the current logo link from the server (could also be binary)
        public void DBGetLogo()
        {
            throw new NotImplementedException();
//            logo = something;
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

        public void DBGetPlayerInfo(string mobile)
        {
            throw new NotImplementedException();
            //currentUser.x = something
        }
    }

    [Serializable]
    public class GetPlayerPointBucketDetailsReturn : Default
    {
        private List<Bucket> customerPointBuckets;

        public class Bucket
        {
            private string bucketCaption;
            private Int32 bucketPointsValue;

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

                result = DataAcess.ExecuteQuerySP(StoredProcedure.GetGameInfoForPromotion, spParams);
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

                result = DataAcess.ExecuteQuerySP(StoredProcedure.GetGameInfoForPromotion, spParams);
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
                            newPromo.PromotionName = null;
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
        public int promotionID;
        public int updateEntryCount;
        public bool remoteEntryAvaliable;

        public void DBEnterRemoteEntry(string mobile, int promotionID)
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public class ClaimBonusCouponsReturn : Default
    {
        public int promotionID;
        public ResultDescription[] resultDescriptions;
        public bool claimButtonRemainsVisible;
        public bool claimButtonRemainsActive;
        public string claimButtonNewCaption;
        public byte[] claimButtonNewImage;

        public void DBClaimBonusCoupons(string mobile, int promotionID)
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public class ResultDescription
    {
        public string resultCaption;
        public string resultUpdatedElement;
        // LARS: Binary Data??
        public string resultImage;
        public HashSet<DataPoint> dataPoints;
    }
    [Serializable]
    public class DataPoint
    {
        public string dataPointCaption;
        public string dataPointData;
    }

    [Serializable]
    public class ListEntriesInNextDrawReturn : Default
    {
        public int promotionID;
        public DateTime nextDrawDate;
        public DateTime nextDrawTime;
        public int entriesForNextDraw;
        public bool isDrumPopulated;
        public string[] entryNumbers;
        public string entryNumber;
        public string specialMessage;

        public void DBListEntriesInNextDraw(string mobile, int promotionID)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region My Offers Screen
    [Serializable]
    public class GetOffersScreenWrapperReturn : Default
    {
        public string line1Caption;
        public string line1Data;
        public string line2Caption;
        public string line2Data;
        public OfferButton[] buttons;

        public void DBGetOffersScreenWrapper(string mobile)
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public class OfferButton
    {
        public string buttonCaption;
        public int buttonOrdPos;
        public int buttonOfferID;
        public byte[] buttonImage;
    }

    [Serializable]
    public class GetOfferDetailsReturn : Default
    {
        public int offerID;
        public string headerCaptionLine1;
        public string headerCaptionLine2;
        public byte[] offerLargeImage;
        public string offerCaption;
        public bool offerBarcodeDisplayed;
        public byte[] offerBarcode;
        public bool displayOptions;
        public Option[] offerOptions;
        public string footerCaptionLine1;
        public string footerCaptionLine2;


        public void DBGetOfferDetails(string mobile, int offerID)
        {
            throw new NotImplementedException();
        }
    }
    [Serializable]
    public class Option
    {
        public string optionCaption;
        public byte[] optionImage;
        public bool optionExecutable;
        public int optionReferenceID;
    }

    [Serializable]
    public class ExecuteRedemptionOptionReturn : Default
    {
        public int offerID;
        public int optionReferenceID;
        public bool redemptionExecutionSucess;
        public string headerCaptionLine1;
        public string headerCaptionLine2;
        public string bodyCaptionLine1;
        public string bodyCaptionLine2;
        public byte[] bodyImage1;
        public byte[] bodyImage2;
        public string footerCaptionLine1;
        public string footerCaptionLine2;

        public void DBExecuteRedemptionOption(string mobile, int offerID, int optionReferenceID)
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public class ViewOfferRedemptionHistoryReturn : Default
    {
        public string headerCaptionLine1;
        public string headerCaptionLine2;
        public bool offerHistoryExists;
        public Offer[] offerHistory;
        public string footerCaptionLine1;
        public string footerCaptionLine2;

        //SQL DB Retrieval Function:
        public void DBViewOfferRedemptionHistoryReturn(string mobile)
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public class Offer
    {
        public string description;
        public string value;
        public string expiresDate;
        public string issuedDate;
        public string prizeType;
        public bool isExpired;
        public bool isRedeemed;
        public bool hasBarcode;
        public byte[] barcode;

    }
    #endregion

    #region My Events Screen
    [Serializable]
    public class GetEventsScreenWrapperReturn : Default
    {
        public string line1Caption;
        public string line1Data;
        public string line2Caption;
        public string line2Data;
        public List<EventButton> buttons;

        private void RemoveData()
        {
            line1Caption = null;
            line1Data = null;
            line2Caption = null;
            line2Data = null;
            buttons = null;
        }

        public void DBGetEventsScreenWrapper(string mobile)
        {
            
        }
    }
    [Serializable]
    public class EventButton
    {
        public string buttonCaption;
        public int buttonOrdPos;
        public int buttonEventID;
        public byte[] buttonImage;
    }

    [Serializable]
    public class GetEventDetailsReturn : Default
    {
        public int eventID;
        public string headerCaptionLine1;
        public string headerCaptionLine2;
        public byte[] eventLargeImage;
        public string eventCaption;
        public DateTime eventStartDate;
        public DateTime eventStartTime;
        public DateTime eventEndDate;
        public DateTime eventEndTime;
        public bool displayOptions;
        public EventOption[] eventOptions;
        public string footerCaptionLine1;
        public string footerCaptionLine2;

        public void DBGetEventDetails(string mobile, int eventID)
        {
            throw new NotImplementedException();
        }
    }
    [Serializable]
    public class EventOption
    {
        public string optionCaption;
        public byte[] optionImage;
        public bool optionExecutable;
        public int optionReferenceID;
    }

    [Serializable]
    public class EnrollGuestInEventReturn : Default
    {
        public int eventID;
        public int optionReferenceID;
        public string headerCaptionLine1;
        public string headerCaptionLine2;
        public bool isEnrolled;
        public string confirmationNumber;
        public byte[] bodyImage1;
        public byte[] bodyImage2;
        public string footerCaptionLine1;
        public string footerCaptionLine2;

        public void DBEnrollGuestInEvent(string mobile, int eventID, int optionReferenceID)
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public class RequestTicketsToEventReturn : Default
    {
        public int eventID;
        public int optionReferenceID;
        public string headerCaptionLine1;
        public string headerCaptionLine2;
        public bool isEnrolled;
        public string confirmationNumber;
        public int ticketCountAwarded;
        public bool isOnWaitList;
        public string expectedResponseInterval;
        public byte[] bodyImage1;
        public byte[] bodyImage2;
        public string footerCaptionLine1;
        public string footerCaptionLine2;

        public void DBRequestTicketsToEvent(string mobile, int eventID, int optionReferenceID)
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public class PurchaseTicketsToEventWithPointsReturn : Default
    {
        public int eventID;
        public int optionReferenceID;
        public string headerCaptionLine1;
        public string headerCaptionLine2;
        public bool isPurchaseSuccessful;
        public float newPointsBalance;
        public bool isEnrolled;
        public string confirmationNumber;
        public int ticketCountAwarded;
        public bool isOnWaitList;
        public string expectedResponseInterval;
        public byte[] bodyImage1;
        public byte[] bodyImage2;
        public string footerCaptionLine1;
        public string footerCaptionLine2;

        public void DBPurchaseTicketsToEventWithPoints(string mobile, int eventID, int optionReferenceID, int ticketCountRequested)
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}
