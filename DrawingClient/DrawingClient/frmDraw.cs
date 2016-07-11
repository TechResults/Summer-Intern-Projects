using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DrawingClient
{
    public partial class frmDraw : Form
    {
        Bitmap anim;

        BackgroundWorker bgWorker;

        //public delegate void WinnerDrawnEventHandler(Service.Player player);
        //public delegate void ShowDrawnWinnerEventHandler(Service.Player player);
        //public event ShowDrawnWinnerEventHandler ShowDrawnWinner;
        //public event WinnerDrawnEventHandler WinnerDrawn;
        //Int32 _promotionId;
        //Int32 _bucketId;
        //Int32 _drawingId;
        //Int32 _userId;
        //Service.Player player;


        public delegate void PostWinnerAndStartCountdownEventHandler(DrawingClient.Service.Player player);
        public event PostWinnerAndStartCountdownEventHandler PostWinnerAndStartCountdown;
        Service.DCService service = new DrawingClient.Service.DCService();
        private int winnerId;
        bool close = false;
        Timer labelTimer;// =  new Timer();
        string initialText;
        int autoPostDelay = int.Parse(Properties.Settings.Default.AutoPostDelay);
        DrawingClient.Service.Player player;
        

        //public int WinnerId
        //{
        //    get { return winnerId; }
        //    set { winnerId = value; }
        //}

        public frmDraw(DrawingClient.Service.Player player)
        {
            this.player = player;
            InitializeComponent();
            SetUIControlText();
            InitializeBackgroundWorker();
        }

        void labelTimer_Tick(object sender, EventArgs e)
        {
            if (autoPostDelay > 0)
            {
                this.label1.Text = initialText + "\r\n" + String.Format(TextRes.Get("AutoPosting", Program.ci), autoPostDelay);
                autoPostDelay--;
            }
            else
            {
                labelTimer.Enabled = false;
                this.PostWinnerAndStartCountdown(player);
                this.Dispose();
            }
        }

        private void InitializeBackgroundWorker()
        {
            //bgWorker = new BackgroundWorker();
            //bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            //bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);
        }

        private void SetUIControlText()
        {
            try
            {
                this.Text = TextRes.Get("frmDraw_Title", Program.ci);
                initialText = TextRes.Get("WinnerDrawn", Program.ci) + "\r\n" + player.PlayerName;
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        //public void Pause()
        //{
        //   //return true;
        //}

        //private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    System.Threading.Thread.Sleep((int)e.Argument);
        //}

        //private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    if (e.Error != null)
        //    {
        //        MessageBox.Show(e.Error.Message);
        //    }
        //} 

        private void frmDraw_Load(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                this.Invoke(new EventHandler(frmDraw_Load), new object[] { sender, e });
                return;
            }      
            
            //autoPostDelay = autoPostDelay * 1000;
            labelTimer = new Timer();

            labelTimer.Tick += new EventHandler(labelTimer_Tick);
            labelTimer.Interval = 1000;
            labelTimer_Tick(this, EventArgs.Empty);
            labelTimer.Enabled = true;    
        }

        //public DrawingClient.Service.Player DrawWinner(Int32 promotionId, Int32 bucketId, Int32 drawingId)
        //{
        //    try
        //    {
        //        _promotionId = promotionId;
        //        _bucketId = bucketId;
        //        _drawingId = drawingId;
        //        _userId = Common.Instance.UserID;
        //        //Guid requestId = Guid.NewGuid();
        //        player = service.DrawWinner(_promotionId, _bucketId, _drawingId, _userId); //, requestId);
        //        //ShowDrawnWinner(player);


        //        if (Common.Instance.UseAnimation)
        //        {
        //            //labelTimer = new Timer();
        //            //labelTimer.Tick += new EventHandler(labelTimer_Tick);
        //            //labelTimer.Interval = 1000;
        //            //labelTimer.Start();
        //            //this.Refresh();
        //            System.Threading.Thread.Sleep(autoPostDelay);
        //        }

        //        service.SetWinnerPostedByWinnerId(player.WinnerId); 
        //        player = null;
        //    }
        //    catch (Exception ex) { Error.Log(ex, true); }

        //    return player;
        //}

        //public void PostWinner(DrawingClient.Service.Player player)
        //{
        //    try
        //    {
        //        service.SetWinnerPostedByWinnerId(player.WinnerId);
        //        //WinnerDrawn(player);
        //        player = null;
        //    }
        //    catch (Exception ex) { Error.Log(ex, true); }
        //}

        //void labelTimer_Tick(object sender, EventArgs e)
        //{
        //    while (autoPostDelay > 0)
        //    {
        //        this.label1.Text = "Auto-posting in " + Convert.ToString(autoPostDelay / 1000) + " seconds";
        //        autoPostDelay = autoPostDelay - 1000;
        //        System.Threading.Thread.Sleep(1000);
        //    }
        //}

        //void DCService_DrawWinnerCompleted(object sender, DrawingClient.Service.DrawWinnerCompletedEventArgs e)
        //{
        //    try
        //    {
        //        player = e.Result;
        //        if (Common.Instance.UseAnimation)
        //        {
        //            tDrawTimer.Enabled = true;

        //            //this is Clinton Whites addition to the code
        //            //ShowDrawnWinner(player);
        //        }
        //        else
        //        {
        //            ShowDrawnWinner(player);
        //            WinnerDrawn(player);
        //            player = null;
        //        }
        //    }
        //    catch (Exception ex) { Error.Log(ex, true); }
        //}

        //void Animate(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        pbImage.Invalidate();
        //    }
        //    catch { }
        //}

        //private void pbImage_Paint(object sender, PaintEventArgs e)
        //{
        //    try
        //    {
        //        ImageAnimator.UpdateFrames();
        //        Bitmap tmp = new Bitmap(anim, anim.Width, anim.Height);
        //        tmp.MakeTransparent(Color.White);
        //        e.Graphics.Clear(pbImage.BackColor);
        //        e.Graphics.DrawImage(tmp, new Point(0, 0));
        //    }
        //    catch { }
        //}

        //private void tDrawTimer_Tick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //Guid requestId = Guid.NewGuid();
        //        //service.GetDrawingStatusAsync(requestId);
        //        //PostWinner();
        //    }
        //    catch (Exception ex) { Error.Log(ex, true); }
        //}




        //void DCService_GetDrawingStatusCompleted(object sender, DrawingClient.Service.GetDrawingStatusCompletedEventArgs e)
        //{
        //    try
        //    {
        //        if (player != null)
        //        {
        //            ShowDrawnWinner(player);
        //            tDrawTimer.Enabled = false;
        //            WinnerDrawn(player);
        //            player = null;

        //            ////Show last winner drawn in client immediately
        //            //if (tryCount == 0)
        //            //    ShowDrawnWinner(player);

        //            //if (tryCount < 5)
        //            //{
        //            //    tryCount++;

        //            //    this.label1.Text = TextRes.Get("DrawWinners", Program.ci) + " " + tryCount.ToString();

        //            //    if (e.Result == "Winner Posted")
        //            //    {


        //            //        tDrawTimer.Enabled = false;
        //            //        WinnerDrawn(player);
        //            //        player = null;
        //            //        tryCount = 0;
        //            //    }
        //            //}
        //            //else
        //            //{
      
        //            //    tDrawTimer.Enabled = false;
        //            //    WinnerDrawn(player);
        //            //    player = null;
        //            //    tryCount = 0;
        //            //}
        //        }
        //        else
        //        {
      
        //            tDrawTimer.Enabled = false;
        //            WinnerDrawn(null);
        //            player = null;
        //            //tryCount = 0;
        //        }
        //    }
        //    catch (Exception ex) { Error.Log(ex, true); }
        //}


        private void frmDraw_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!close)
                e.Cancel = true;
        }
    }
}