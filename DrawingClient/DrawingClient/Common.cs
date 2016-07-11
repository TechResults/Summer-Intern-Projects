using System;

namespace DrawingClient
{
    public sealed class Common
    {
        private static volatile Common instance;
        private static object syncRoot = new Object();

        private Common() { }

        public static Common Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Common();
                    }
                }
                return instance;
            }
        }

        //private Service.DCService dcService = new DrawingClient.Service.DCService();

        //public Service.DCService DCService
        //{
        //    get { return dcService; }
        //}

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

        private Int32 userLevel;

        public Int32 UserLevel
        {
            get { return userLevel; }
            set { userLevel = value; }
        }

        private bool useAnimation;

        public bool UseAnimation
        {
            get { return useAnimation; }
            set { useAnimation = value; }
        }
    }
}
