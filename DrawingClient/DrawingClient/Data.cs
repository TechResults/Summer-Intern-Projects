using System;
using System.Data;

namespace DrawingClient
{
    public sealed class Data
    {
        private static volatile Data instance;
        private static object syncRoot = new Object();

        private Data() { }

        public static Data Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Data();
                    }
                }
                return instance;
            }
        }

        public DataSet GetAllActivePromotions()
        {
            Service.DCService service = new DrawingClient.Service.DCService();
            return service.GetAllActivePromotions();
        }

        public Service.DrawingGroup GetDrawingGroup(Int32 promotionID)
        {
            Service.DCService service = new DrawingClient.Service.DCService();
            return service.GetDrawingGroup(promotionID);
        }
    }
}
