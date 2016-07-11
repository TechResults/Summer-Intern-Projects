/*
 * File name:           DrawingGroup.cs
 *
 * Revision:            1.0
 * Last revised by:     Eugene T. Staten II
 * Last revision date:  3/10/2008 10:00:07 AM
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
    public class DrawingGroup
    {
        #region Constructors
        DrawingGroup()
        {
        }
        #endregion

        #region Instance Members
        Int32 _promoId;
        Int32 _bucketId;
        SqlDateTime _drawDate = SqlDateTime.MinValue;
        SqlDateTime _nextDrawDate = SqlDateTime.MinValue;
        Promo _promo;
        Drum _drum;
        List<Drawing> _drawings = new List<Drawing>();
        #endregion

        #region Properties
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
        public SqlDateTime DrawDate
        {
            get { return _drawDate; }
            set { _drawDate = value; }
        }
        public SqlDateTime NextDrawDate
        {
            get { return _nextDrawDate; }
            set { _nextDrawDate = value; }
        }
        public Promo Promo
        {
            get { return _promo; }
            set { _promo = value; }
        }
        public Drum Drum
        {
            get { return _drum; }
            set { _drum = value; }
        }
        public List<Drawing> Drawings
        {
            get { return _drawings; }
        }

        private SqlDateTime checkinStartTime;

        public SqlDateTime CheckinStartTime
        {
            get { return checkinStartTime; }
            set { checkinStartTime = value; }
        }

        private SqlDateTime checkinEndTime;

        public SqlDateTime CheckinEndTime
        {
            get { return checkinEndTime; }
            set { checkinEndTime = value; }
        }

        #endregion
    }
}
