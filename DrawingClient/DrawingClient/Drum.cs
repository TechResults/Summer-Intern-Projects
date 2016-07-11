/*
 * File name:           Drum.cs
 *
 * Revision:            1.0
 * Last revised by:     Eugene T. Staten II
 * Last revision date:  3/10/2008 10:02:40 AM
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
    public class Drum
    {
        #region Instance Members
        Int32 _id;
        String _name;
        String _type;
        Int32 _numberOfEntries;
        Int32 _numberOfPlayers;
        SqlDateTime _lastPopulated = SqlDateTime.MinValue;
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
        public String Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public Int32 NumberOfEntries
        {
            get { return _numberOfEntries; }
            set { _numberOfEntries = value; }
        }
        public Int32 NumberOfPlayers
        {
            get { return _numberOfPlayers; }
            set { _numberOfPlayers = value; }
        }
        public SqlDateTime LastPopulated
        {
            get { return _lastPopulated; }
            set { _lastPopulated = value; }
        }
        #endregion

        #region Constructors
        Drum()
        {
        }
        #endregion
    }
}
