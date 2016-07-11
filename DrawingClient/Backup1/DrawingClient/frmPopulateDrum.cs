using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DrawingClient
{
    public partial class frmPopulateDrum : Form
    {
        Bitmap anim;

        Int32 _promotionId;
        Int32 _bucketId;
        DateTime _drawDate;
        DateTime _checkinStartTime;
        DateTime _checkinEndTime;
        bool _optimize = false;
        public delegate void DrumOptimizedEventHandler();
        public event DrumOptimizedEventHandler DrumOptimized;

        public frmPopulateDrum(Int32 promotionId, Int32 bucketId, DateTime drawDate, DateTime checkinStartTime, DateTime checkinEndTime,bool optimize)
        {
            InitializeComponent();
            SetUIControlText();
            _promotionId = promotionId;
            _bucketId = bucketId;
            _drawDate = drawDate;
            _checkinStartTime = checkinStartTime;
            _checkinEndTime = checkinEndTime;
            _optimize = optimize;
            Service.DCService service = new DrawingClient.Service.DCService();
            service.PopulateDrumCompleted += new DrawingClient.Service.PopulateDrumCompletedEventHandler(DCService_PopulateDrumCompleted);
            service.OptimizeDrumCompleted += new DrawingClient.Service.OptimizeDrumCompletedEventHandler(DCService_OptimizeDrumCompleted);
        }

        private void SetUIControlText()
        {
            try
            {
                this.Text = TextRes.Get("frmPopulateDrum_Title", Program.ci);
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        private void frmPopulateDrum_Load(object sender, EventArgs e)
        {
            anim = Properties.Resources.PopulatingDrum;
            ImageAnimator.Animate(anim, new EventHandler(Animate));
            lblAction.Text = TextRes.Get("PopulatingDrum", Program.ci);
            try
            {
                Guid requestId = Guid.NewGuid();
                Service.DCService service = new DrawingClient.Service.DCService();
                service.PopulateDrumCompleted += new DrawingClient.Service.PopulateDrumCompletedEventHandler(DCService_PopulateDrumCompleted);

                //Common.Instance.DCService.PopulateDrumAsync(_promotionId, _bucketId, _drawDate, DateTime.Now, _checkinStartTime, _checkinEndTime, Common.Instance.UserID, _promotionId);
                service.PopulateDrumAsync(_promotionId, _bucketId, _drawDate, DateTime.Now, _checkinStartTime, _checkinEndTime, Common.Instance.UserID, _promotionId);
                //service.PopulateDrum(_promotionId, _bucketId, _drawDate, DateTime.Now, _checkinStartTime, _checkinEndTime, Common.Instance.UserID);
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        void DCService_PopulateDrumCompleted(object sender, DrawingClient.Service.PopulateDrumCompletedEventArgs e)
        {
            try
            {
                
                if (_optimize)
                {
                    Guid requestId = Guid.NewGuid();
                    Service.DCService service = new DrawingClient.Service.DCService();
                    service.OptimizeDrumCompleted += new DrawingClient.Service.OptimizeDrumCompletedEventHandler(DCService_OptimizeDrumCompleted);

                    //Common.Instance.DCService.OptimizeDrumAsync(_promotionId, _bucketId, _promotionId);
                    service.OptimizeDrumAsync(_promotionId, _bucketId, _promotionId);
                }
                else
                {
                    close = true;
                    this.Close();
                    //DrumOptimized();
                }
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        void DCService_OptimizeDrumCompleted(object sender, DrawingClient.Service.OptimizeDrumCompletedEventArgs e)
        {
            try
            {
                close = true;
                this.Close();
                //DrumOptimized();
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }


        void Animate(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Invalidate();
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }


        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                ImageAnimator.UpdateFrames();
                Bitmap tmp = new Bitmap(anim, anim.Width, anim.Height);
                tmp.MakeTransparent(Color.White);
                e.Graphics.Clear(pictureBox1.BackColor);
                e.Graphics.DrawImage(tmp, new Point(0, 0));
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        bool close = false;

        private void frmPopulateDrum_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!close)
                    e.Cancel = true;
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }
    }
}