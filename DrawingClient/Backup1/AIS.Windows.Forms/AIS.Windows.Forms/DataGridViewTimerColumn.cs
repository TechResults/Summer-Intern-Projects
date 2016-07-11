using System.Windows.Forms;

namespace AIS.Windows.Forms
{
    public class DataGridViewTimerColumn : DataGridViewTextBoxColumn
    {
        public DataGridViewTimerColumn()
        {
            this.CellTemplate = new DataGridViewTimerCell();
        }
    }
}
