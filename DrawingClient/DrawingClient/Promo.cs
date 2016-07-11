/*
 * File name:           Promo.cs
 *
 * Revision:            1.0
 * Last revised by:     Eugene T. Staten II
 * Last revision date:  3/10/2008 10:01:39 AM
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
    public class Promo
    {
        #region Instance Members
        Int32 _id;
        String _name;
        SqlDateTime _startDate;
        SqlDateTime _endDate;
        #endregion

        #region Properties
        public Int32 Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public SqlDateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }
        public SqlDateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }
        #endregion

        #region Constructors
        Promo()
        {
        }
        #endregion
    }
}
