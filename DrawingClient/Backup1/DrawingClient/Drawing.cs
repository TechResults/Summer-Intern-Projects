/*
 * File name:           Drawing.cs
 *
 * Revision:            1.0
 * Last revised by:     Eugene T. Staten II
 * Last revision date:  3/10/2008 10:03:40 AM
 *
 * Original Author:     CTowers
 *
 * Copyright notice:
 *   THIS SOURCE FILE, ITS MACHINE READABLE FORM, AND ANY REPRESENTATION
 *   OF THE MATERIAL CONTAINED HEREIN ARE OWNED BY TECH RESULTS.
 *
 *   THESE MATERIALS ARE PROPRIETARY AND CONFIDENTIAL AND MAY NOT BE
 *   REPRODUCED IN ANY FORM WITHOUT THE PRIOR WRITTEN PERMISSION OF
 *   TECH RESULTS.
 *
 *         COPYRIGHT (C) 2008 BY TECH RESULTS
 *                ALL RIGHTS RESERVED
 *
 */


using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlTypes;

namespace DrawingClient
{
    public class Drawing
    {
        #region Instance Members
        Int32 _id;
        Int32 _promoId;
        Int32 _bucketId;
        Int32 _prizeId;
        String _prize;
        Int32 _winnerId;
        String _winner;
        SqlDateTime _winnerDob = SqlDateTime.MinValue;
        SqlDateTime _timeOut = SqlDateTime.MinValue;
        String _status;
        #endregion

        #region Properties
        public Int32 Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public Int32 PromoId
        {
            get { return _promoId; }
            set { _promoId = value; }
        }
        public Int32 BucketId
        {
            get { return _bucketId; }
            set { _bucketId = value; }
        }
        public Int32 PrizeId
        {
            get { return _prizeId; }
            set { _prizeId = value; }
        }
        public String Prize
        {
            get { return _prize; }
            set { _prize = value; }
        }
        private decimal _prizeValue;

        public decimal PrizeValue
        {
            get { return _prizeValue; }
            set { _prizeValue = value; }
        }

        public Int32 WinnerId
        {
            get { return _winnerId; }
            set { _winnerId = value; }
        }
        public String Winner
        {
            get { return _winner; }
            set { _winner = value; }
        }
        public SqlDateTime WinnerDob
        {
            get { return _winnerDob; }
            set { _winnerDob = value; }
        }
        public SqlDateTime TimeOut
        {
            get { return _timeOut; }
            set { _timeOut = value; }
        }
        public String Status
        {
            get { return _status; }
            set { _status = value; }
        }

        private String cmsPlayerID;

        public String CMSPlayerID
        {
            get { return cmsPlayerID; }
            set { cmsPlayerID = value; }
        }
        #endregion

        #region Constructors
        Drawing()
        {
        }
        #endregion
    }
}
