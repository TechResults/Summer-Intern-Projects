using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;
using System.Windows.Forms;

using AIS.Windows.Forms;

namespace DrawingClient
{
    public class DataGridViewTimerCell : AIS.Windows.Forms.DataGridViewTimerCell
    {
        Service.DCService service;
        //Timer timer;

        private int drawingId;
        private TimeSpan time;

        public new TimeSpan Time
        {
            get { return time; }
            set { time = value; }
        }


        public int DrawingId
        {
            get { return drawingId; }
            set { drawingId = value; }
        }

        public DataGridViewTimerCell()
        {
            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 400;
            service = new DrawingClient.Service.DCService();
        }

        public override void Start()
        {
            if (timer.Enabled)
                timer.Enabled = false;
            timer.Enabled = true;
            mTrigger = this.TimeStamp + this.Time;
            timer_Tick(this, EventArgs.Empty);
        }

        protected override void timer_Tick(object sender, EventArgs e)
        {
            if (this.TimeStamp == SqlDateTime.MinValue)
            {
                this.TimeStamp = Common.Instance.UseAnimation ? service.GetDrawing(this.DrawingId).TimeOut : service.GetDrawing(this.DrawingId).TimeStarted;
                mTrigger = this.TimeStamp + this.Time;
                return;                 
            }

            TimeSpan sp = mTrigger - service.GetTime();
            if (sp.Ticks < 0)
            {
                sp = new TimeSpan();
                timer.Enabled = false;
            }
            string val = string.Format("{0}:{1:00}", sp.Minutes, sp.Seconds);
            if (val == "0:00")
                this.Style.ForeColor = System.Drawing.Color.Red;
            else
                this.Style.ForeColor = System.Drawing.Color.Black;
            this.Value = val;
        }
    }
}
