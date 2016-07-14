using System;
using System.Collections.Generic;
using System.Linq;
using PE.DataReturn;
using System.Data.SqlClient;
using System.Data;
using PE.DataReturns;
using System.IO;


/// <summary>
///
/// </summary>
namespace PE.GamesFrameDB
{
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

                DataSet headerDS = new DataSet();
                List<SqlParameter> headerParams = new List<SqlParameter>();
                headerParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                headerDS = DataAcess.ExecuteQuerySP("PEC.", headerParams);

                if (headerDS.Tables[0].Rows.Count > 0)
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

            catch (SqlException ex)
            {
                RemoveData();
                string errorMessage = ex.Message;
            }

        }
    }


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

            catch (SqlException ex)
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

            catch (SqlException ex)
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
            try
            {
                string CMSPlayerID = ServerSide.DBGetCMSPlayerID(mobile);

                DataSet result = new DataSet();
                List<SqlParameter> spParams = new List<SqlParameter>();
                spParams.Add(new SqlParameter("@CMSPlayerID", CMSPlayerID));
                spParams.Add(new SqlParameter("@GameID", gameID));
                spParams.Add(new SqlParameter("@GameToken", gameToken));


                result = DataAcess.ExecuteQuerySP("PEC.MG_", spParams);
                if (result.Tables[0].Rows.Count > 0)
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

                    for (int i = 0; i < gameDS.Tables[0].Rows.Count; i++)
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
                if (result.Tables[0].Rows.Count > 0)
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

            catch (SqlException ex)
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
}