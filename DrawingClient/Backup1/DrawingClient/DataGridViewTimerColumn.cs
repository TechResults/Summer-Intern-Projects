using System;
using System.Collections.Generic;
using System.Text;

using AIS.Windows.Forms;

namespace DrawingClient
{
    public class DataGridViewTimerColumn : AIS.Windows.Forms.DataGridViewTimerColumn
    {
        public DataGridViewTimerColumn()
        {
            this.CellTemplate = new DataGridViewTimerCell();
        }
    }
}
