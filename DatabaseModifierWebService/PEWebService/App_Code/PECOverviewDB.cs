using System;
using System.Collections.Generic;
using System.Linq;
using PE.DataReturn;
using System.Data.SqlClient;
using System.Data;
using PE.DataReturns;
using System.IO;

/// <summary>
/// Loads overview screen elements
/// </summary>
///
namespace PE.OverviewDB
{
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
        private void RemoveData()
        {
            PlayerCardImageBack = null;
            PlayerCardImageFront = null;
        }
        //GetPlayerCard details from the SQL DB
        public void DBGetPlayerCardImage(string mobile)
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
    #endregion

}
