using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace AIS.Windows.Forms
{
    public class DataGridViewTimerCell : DataGridViewTextBoxCell
    {
        protected Timer timer;

        protected DateTime mTrigger;
        private TimeSpan mPauseToGo;
        private bool mPaused;

        private int time;

        public int Time
        {
            set { time = value; }
        }

        private DateTime timeStamp;

        public DateTime TimeStamp
        {
            get { return timeStamp; }
            set { timeStamp = value; }
        }

        public DataGridViewTimerCell()
        {
            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 1000;
        }

        public void StartPaused()
        {
            if (timer.Enabled)
                timer.Enabled = false;

            mTrigger = timeStamp + new TimeSpan(0, time, 0);
            timer_Tick(this, EventArgs.Empty);
        }

        public virtual void Start()
        {
            if (timer.Enabled)
                timer.Enabled = false;
            timer.Enabled = true;
            mTrigger = timeStamp + new TimeSpan(0, time, 0);
            timer_Tick(this, EventArgs.Empty);
            //if (mPaused)
            //{
            //    timer.Enabled = true;
            //    mPaused = false;
            //    mTrigger = DateTime.Now + mPauseToGo;
            //}
            //else if (timer.Enabled)
            //{
            //    timer.Enabled = false;
            //    mPauseToGo = mTrigger - DateTime.Now;
            //    mPaused = true;
            //}
            //else
            //{
            //    timer.Enabled = true;
            //    mTrigger = DateTime.Now + new TimeSpan(0, time, 0);
            //    timer_Tick(this, EventArgs.Empty);
            //}
        }

        public void Stop()
        {
            timer.Enabled = false;
            this.Style.ForeColor = System.Drawing.Color.Black;
            this.Value = "";
        }

        protected virtual void timer_Tick(object sender, EventArgs e)
        {
            TimeSpan sp = mTrigger - DateTime.Now;
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
