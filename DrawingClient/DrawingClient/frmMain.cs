using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AIS.Windows.Forms;
using System.Data.SqlTypes;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Globalization;

namespace DrawingClient
{
    public partial class frmMain : Form
    {
        #region Members
        /// <summary>
        /// Current drawing group
        /// </summary>
        Service.DrawingGroup drawingGroup;

        /// <summary>
        ///  Indicates whether or not data is loading
        /// </summary>
        bool isLoading = true;

        /// <summary>
        /// Currently drawn row
        /// </summary>
        Int32 currentRow = 0;

        /// <summary>
        /// Indicates whether there is a drawing in progress
        /// </summary>
        bool drawingInProgress = false;

        /// <summary>
        /// Indicates whether or not all winners are being drawn
        /// </summary>
        bool drawAll = false;

        ///// <summary>
        ///// Instance of frmDraw
        ///// </summary>
        //frmDraw drawForm;

        /// <summary>
        /// Determines if winner must be present to claim prize.
        /// </summary>
        bool winnerMustBePresent = false;

        /// <summary>
        /// Number of remaining entries in the drawing
        /// </summary>
        Int32 remainingEntries = 0;

        /// <summary>
        /// Determines whether the dialog stating there are no entries has been displayed
        /// </summary>
        bool displayedNoEntries = false;

        /// <summary>
        /// Determines whether the dialog stating there are no players has been displayed
        /// </summary>
        bool displayedNoPlayers = false;

        /// <summary>
        /// Instance of frmPopulateDrum
        /// </summary>
        frmPopulateDrum populateDrumForm = null;

        /// <summary>
        /// Enable the solicit EGMs/Patrons button (only available if PCS 1.1 or higher is installed)
        /// </summary>
        bool enableSolicitBtn = false;

        Service.DCService service;
        DrawingClient.Service.Player player;


        #endregion Members

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>

        public frmMain()
        {
            InitializeComponent();
            SetUIControlText();
            service = new DrawingClient.Service.DCService();
        }

        #endregion Constructor


        #region Delegates, Events, & Callbacks
        delegate void ResetDrawingEventHandler();

        /// <summary>
        /// Event fired when all active promotions have been returned
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DCService_GetAllActivePromotionsCompleted(object sender, DrawingClient.Service.GetAllActivePromotionsCompletedEventArgs e)
        {
            try
            {
                cbActivePromotions.DataSource = e.Result.Tables[0].DefaultView;
                e.Result.Tables[0].DefaultView.Sort = "PromotionName";
                cbActivePromotions.DisplayMember = "PromotionName";
                cbActivePromotions.ValueMember = "PromotionId";
                cbActivePromotions.Enabled = true;
                cbActivePromotions.SelectedIndex = 0;
                isLoading = false;

            }
            catch (Exception ex) { Error.Log(ex, true); }
            GetDrawingGroup();
        }

        void populateDrumForm_DrumOptimized()
        {
            try
            {
                HidePopulateDrumEventHandler h = new HidePopulateDrumEventHandler(HidePopulateDrum);
                this.Invoke(h);
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        delegate void HidePopulateDrumEventHandler();

        void HidePopulateDrum()
        {
        }
        #endregion Delegates, Events, & Callbacks

        #region ASync Callbacks
        void DCService_ResetDrawingCompleted(object sender, DrawingClient.Service.ResetDrawingCompletedEventArgs e)
        {
            try
            {
                drawingGroup = null;
                remainingEntries = 0;
                displayedNoEntries = false;
                displayedNoPlayers = false;
                cbActivePromotions.Enabled = true;
                if (Common.Instance.UseAnimation)
                {
                    btnStartCountdown.Enabled = true;
                    btnStartDrawing.Enabled = false;
                    btnStopCountdown.Enabled = false;
                    btnDrawAllWinners.Enabled = false;
                    btnPopulateDrum.Enabled = true;
                    //if(enableSolicitBtn)
                    //    btnSolicitDigitalDisplays.Enabled = true;
                }
                MessageBox.Show(TextRes.Get("DrawingReset", Program.ci), TextRes.Get("ResetDrawing", Program.ci), MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnResetDrawing.Enabled = true;
                GetDrawingGroup();
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        #endregion ASync Callbacks

        #region FormEvents
        /// <summary>
        /// On form load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            InitializeClient();
        }

        private void SetUIControlText()
        {
            try
            {
                this.Column1.HeaderText = TextRes.Get("Prize", Program.ci);
                this.Column9.HeaderText = TextRes.Get("PrizeValue", Program.ci);
                this.Column9.DefaultCellStyle.Format = String.Format(Program.ciSiteNumeric, "C2", 1);
                this.Column3.HeaderText = TextRes.Get("WinnerID", Program.ci);
                this.Column2.HeaderText = TextRes.Get("Winner", Program.ci);
                this.Column4.HeaderText = TextRes.Get("WinnerDOB", Program.ci);
                this.Column4.DefaultCellStyle.FormatProvider = Program.ci;
                this.Column5.HeaderText = TextRes.Get("TimeLeft", Program.ci);
                this.Column6.HeaderText = TextRes.Get("Status", Program.ci);
                this.Column10.HeaderText = TextRes.Get("DrawingId", Program.ci);
                this.Column11.HeaderText = TextRes.Get("EntryId", Program.ci);
                this.Column12.HeaderText = TextRes.Get("PlayerId", Program.ci);
                this.Column13.HeaderText = TextRes.Get("PlayerCMS", Program.ci);
                this.Column14.HeaderText = TextRes.Get("WinnerID", Program.ci);
                this.Column15.HeaderText = TextRes.Get("WinnerDOB", Program.ci);
                this.Column15.DefaultCellStyle.FormatProvider = Program.ci;
                this.Column16.HeaderText = TextRes.Get("DrawnAt", Program.ci);
                this.Column17.HeaderText = TextRes.Get("Validated", Program.ci);
                this.Column18.HeaderText = TextRes.Get("TimeStarted", Program.ci);

                this.fileToolStripMenuItem.Text = TextRes.Get("File", Program.ci);
                this.exitToolStripMenuItem.Text = TextRes.Get("Exit", Program.ci);
                this.toolsToolStripMenuItem.Text = TextRes.Get("Tools", Program.ci);
                this.totalPromoUserAdminToolStripMenuItem.Text = TextRes.Get("TotalPromoUserAdmin", Program.ci);
                this.totalPromoCustomerServiceDesktopToolStripMenuItem.Text = TextRes.Get("TotalPromoCustomerServiceDesktop", Program.ci);
                this.totalPromoMarketingManagerToolStripMenuItem.Text = TextRes.Get("TotalPromoMarketingManager", Program.ci);
                this.animationClientToolStripMenuItem.Text = TextRes.Get("AnimationClient", Program.ci);
                this.helpToolStripMenuItem.Text = TextRes.Get("Help", Program.ci);
                this.aboutDrawingClientToolStripMenuItem.Text = TextRes.Get("AboutDrawingClient", Program.ci);
                this.onlineSupportToolStripMenuItem.Text = TextRes.Get("OnlineSupport", Program.ci);
                //this.drawingAnimationClientHelpToolStripMenuItem.Text = TextRes.Get("Error", Program.ci).DrawingAnimationClientHelp;
                this.toolStripStatusLabel1.Text = TextRes.Get("LoggedInUser", Program.ci);
                this.groupBox1.Text = TextRes.Get("Promotion", Program.ci);
                this.label11.Text = TextRes.Get("EndDate", Program.ci);
                this.label10.Text = TextRes.Get("StartDate", Program.ci);
                this.label9.Text = TextRes.Get("Promotion", Program.ci);
                this.label7.Text = TextRes.Get("NextDrawing", Program.ci);
                this.label8.Text = TextRes.Get("Drawing", Program.ci);
                this.groupBox2.Text = TextRes.Get("DrumStatistics", Program.ci);
                this.linkLabel1.Text = TextRes.Get("ViewDrumHistory", Program.ci);
                this.label2.Text = TextRes.Get("AvgEntriesPlayer", Program.ci);
                this.btnPopulateDrum.Text = TextRes.Get("PopulateDrum", Program.ci);
                this.label1.Text = TextRes.Get("DrumName", Program.ci);
                this.label5.Text = TextRes.Get("Entries", Program.ci);
                this.label6.Text = TextRes.Get("Players", Program.ci);
                this.label3.Text = TextRes.Get("LastPopulated", Program.ci);
                this.label4.Text = TextRes.Get("DrumType", Program.ci);
                this.groupBox7.Text = TextRes.Get("ResetDrawing", Program.ci);
                this.btnResetDrawing.Text = TextRes.Get("ResetDrawing", Program.ci);
                this.groupBox4.Text = TextRes.Get("Actions", Program.ci);
                this.btnCompleteDrawing.Text = TextRes.Get("CompleteDrawing", Program.ci);
                this.btnStopCountdown.Text = TextRes.Get("StopCountdown", Program.ci);
                this.btnDrawAllWinners.Text = TextRes.Get("DrawAllWinners", Program.ci);
                this.btnStartDrawing.Text = TextRes.Get("PrepareDrawing", Program.ci);
                this.btnStartCountdown.Text = TextRes.Get("StartCountdown", Program.ci);
                //this.btnSolicitDigitalDisplays.Text = "Solicit Digital Displays";// TextRes.Get("SolicitDigitalDisplays", Program.ci);
                this.groupBox3.Text = TextRes.Get("Drawing", Program.ci);
                this.groupBox5.Text = TextRes.Get("Animation", Program.ci);
                this.lblAnimationInUse.Text = TextRes.Get("AnimationInUse", Program.ci);
                this.dataGridViewDisableButtonColumn1.Text = TextRes.Get("Draw", Program.ci);
                this.dataGridViewDisableButtonColumn2.Text = TextRes.Get("Validate", Program.ci);
                this.groupBox6.Text = TextRes.Get("DrawingInfoPanel", Program.ci);
                this.Column7.Text = TextRes.Get("Draw", Program.ci);
                this.Column8.Text = TextRes.Get("Validate", Program.ci);
                this.Text = TextRes.Get("frmMain_Title", Program.ci);
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        /// <summary>
        /// On form closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Service.DCService service = new DrawingClient.Service.DCService();
                service.SetDrawingClientInUse(String.Empty, String.Empty);
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        #endregion FormEvents

        #region UI
        /// <summary>
        /// Initialize the client
        /// </summary>
        void InitializeClient()
        {
            try
            {
                toolStripStatusLabel1.Text = TextRes.Get("LoggedIn", Program.ci) + ": " + Common.Instance.UserIDText;

                if (Common.Instance.UseAnimation)
                {
                    lblAnimationInUse.ForeColor = System.Drawing.Color.ForestGreen;
                    lblAnimationInUse.Text = TextRes.Get("AnimationInUse", Program.ci);
                    pnlAnimation.Visible = true;
                    btnStartCountdown.Enabled = true;
                    btnStartDrawing.Enabled = false;
                    btnStopCountdown.Enabled = false;
                    btnDrawAllWinners.Enabled = false;
                    tUpdateStats.Interval = Int32.Parse(Properties.Settings.Default.UpdateInterval) * 60000;

                    //2009-04-21 JMS: Waiting for next PCS release to uncomment this
                    //Show solicit patrons button only if digital display is installed at client site
                    //int indexOfDot = digitalDisplayVersion.IndexOf('.');
                    //int indexOfDot2 = 0;

                    //if (indexOfDot != 0)
                    //    indexOfDot2 = digitalDisplayVersion.IndexOf('.', indexOfDot+1);
                    //int ddVer = 0;
                    //if (indexOfDot != 0 && indexOfDot2 != 0 && 
                    //    Int32.TryParse(digitalDisplayVersion.Substring(0, indexOfDot), out ddVer))
                    //    if (ddVer > 0 &&
                    //        Int32.TryParse(digitalDisplayVersion.Substring(indexOfDot+1, 1), out ddVer))
                    //    {
                    //        //Must be at PCS 1.1 or higher to have this button enabled
                    //        if (ddVer > 0)
                    //        {
                    //            enableSolicitBtn = true;
                    //            //btnSolicitDigitalDisplays.Visible = true;
                    //        }
                    //    }

                }
                else
                {
                    lblAnimationInUse.ForeColor = System.Drawing.Color.Red;
                    lblAnimationInUse.Text = TextRes.Get("AnimationNotInUse", Program.ci);
                    pnlAnimation.Visible = false;
                    btnStartCountdown.Enabled = false;
                    btnStartDrawing.Enabled = true;
                    btnStopCountdown.Enabled = false;
                    btnDrawAllWinners.Enabled = false;
                    tUpdateStats.Interval = Int32.Parse(Properties.Settings.Default.UpdateInterval) * 60000;
                }

            }
            catch (Exception ex) { Error.Log(ex, true); }
            BindActivePromotions();
        }

        /// <summary>
        /// Make async call to get all active promotions
        /// </summary>
        void BindActivePromotions()
        {
            try
            {
                isLoading = true;
                Guid requestId = Guid.NewGuid();
                Service.DCService service = new DrawingClient.Service.DCService();
                service.GetAllActivePromotionsCompleted += new DrawingClient.Service.GetAllActivePromotionsCompletedEventHandler(DCService_GetAllActivePromotionsCompleted);
                service.GetAllActivePromotionsAsync(requestId);
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        /// <summary>
        /// Update the drum information area of the client.
        /// </summary>
        void UpdateDrumInfo()
        {
            try
            {
                Service.DCService service = new DrawingClient.Service.DCService();
                Service.Drum drum = service.GetDrum(drawingGroup.PromoId, drawingGroup.BucketId);
                Service.Drum drumLastHistory = service.GetDrumLastHistoryItem(drawingGroup.PromoId, drawingGroup.BucketId);
                tbDrumName.Text = drum.Name;
                tbDrumType.Text = TextRes.Get(drum.Type, Program.ci);
                if (Convert.ToDateTime(drumLastHistory.LastPopulated.ToString()).ToString() == SqlDateTime.MinValue.ToString())
                    tbLastPopulated.Text = TextRes.Get("Never", Program.ci);
                else
                    tbLastPopulated.Text = drumLastHistory.LastPopulated.ToString(Program.ci);
                tbPlayers.Text = drumLastHistory.NumberOfPlayers.ToString("N0");
                tbEntries.Text = drumLastHistory.NumberOfEntries.ToString("N0");
                remainingEntries = drum.NumberOfEntries;
                Int32 avg = 0;
                if (drumLastHistory.NumberOfEntries > 0 && drumLastHistory.NumberOfPlayers > 0)
                    avg = Int32.Parse(drumLastHistory.NumberOfEntries.ToString()) / Int32.Parse(drumLastHistory.NumberOfPlayers.ToString());
                tbAvgEntries.Text = avg.ToString("N0");

                if (drumHistoryForm != null && drumHistoryForm.Visible)
                {
                    drumHistoryForm.Update(drum.Name, drum.Type, drumLastHistory.LastPopulated.ToString(), drumLastHistory.NumberOfPlayers, drumLastHistory.NumberOfEntries);
                }
                if (drum.NumberOfEntries == 0)
                {
                    btnStartDrawing.Enabled = true;
                    btnDrawAllWinners.Enabled = false;
                }
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }
        #endregion UI

        #region DrawingGroup
        /// <summary>
        /// Gets the drawing group for the promotion selected in the cbActivePromotions combobox
        /// </summary>

        delegate void GetDrawingGroupEventHandler();

        void GetDrawingGroupCallback()
        {
            try
            {
                if (!isLoading)
                {
                    if (cbActivePromotions.SelectedValue != null)
                    {
                        Int32 promoId = Convert.ToInt32(cbActivePromotions.SelectedValue);
                        if (promoId > 0)
                        {

                            Service.DCService service = new DrawingClient.Service.DCService();
                            winnerMustBePresent = service.MustBePresent(promoId);
                            frmGetDrawingGroup gdgForm = new frmGetDrawingGroup(promoId);
                            gdgForm.ShowDialog();
                            GetDrawingGroupCompleted(gdgForm.DrawingGroup);
                        }
                        else
                        {
                            drawingGroup = null;
                            GetDrawingGroupCompleted(drawingGroup);
                        }
                    }
                }
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        void GetDrawingGroup()
        {
            try
            {
                GetDrawingGroupEventHandler g = new GetDrawingGroupEventHandler(GetDrawingGroupCallback);
                this.Invoke(g);
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        void DisplayDrawingGroup(Service.DrawingGroup d)
        {
            try
            {
                if (d != null)
                {
                    btnResetDrawing.Enabled = true;
                    linkLabel1.Enabled = true;
                    btnCompleteDrawing.Enabled = true;
                    drawingInProgress = false;
                    cbActivePromotions.Enabled = true;
                    if (Common.Instance.UseAnimation)
                    {
                        btnPopulateDrum.Enabled = false;
                        btnStartCountdown.Enabled = true;
                        btnStartDrawing.Enabled = false;
                        btnStopCountdown.Enabled = false;
                        btnDrawAllWinners.Enabled = false;
                    }
                    else
                    {
                        btnPopulateDrum.Enabled = true;
                        btnStartCountdown.Enabled = false;
                        btnStartDrawing.Enabled = true;
                        btnStopCountdown.Enabled = false;
                        btnDrawAllWinners.Enabled = false;
                    }

                    drawingGroup = d;

                    tbDrawing.Text = drawingGroup.DrawDate.ToString(Program.ci);

                    Service.DCService service = new DrawingClient.Service.DCService();
                    DateTime nextDrawingDateTime = service.GetNextDrawingDateTime(drawingGroup.PromoId, drawingGroup.BucketId, drawingGroup.DrawDate);

                    tbNextDrawing.Text = nextDrawingDateTime.CompareTo(drawingGroup.DrawDate) < 0 ? TextRes.Get("NoneText", Program.ci) : nextDrawingDateTime.ToString(Program.ci);

                    bsDrawings.DataSource = drawingGroup.Drawings;

                    Int32 entriesDrawn = 0;

                    foreach (DataGridViewRow row in dgvDrawings.Rows)
                    {
                        if (drawingInProgress != true)
                        {
                            string tempVal = row.Cells[10].Value.ToString();
                            drawingInProgress = row.Cells[10].Value.ToString() != "0" ? true : false;
                        }
                    }

                    if (drawingInProgress)
                    {
                        cbActivePromotions.Enabled = false;
                        UpdateDrumInfo();
                        btnStartCountdown.Enabled = false;
                        btnStartDrawing.Enabled = false;
                        btnStopCountdown.Enabled = false;
                        btnDrawAllWinners.Enabled = false;
                        btnPopulateDrum.Enabled = false;
                    }
                    foreach (DataGridViewRow row in dgvDrawings.Rows)
                    {
                        DataGridViewDisableButtonCell drawCell = (DataGridViewDisableButtonCell)row.Cells[7];
                        DataGridViewDisableButtonCell validateCell = (DataGridViewDisableButtonCell)row.Cells[8];
                        DataGridViewTimerCell timerCell = (DataGridViewTimerCell)dgvDrawings.Rows[row.Index].Cells[5];
                        DataGridViewImageCell imageCell = (DataGridViewImageCell)row.Cells[6];


                        if (Convert.ToDateTime(row.Cells[14].Value.ToString()).ToString() != SqlDateTime.MinValue.ToString())
                        {
                            row.Cells[4].Value = row.Cells[14].Value;
                        }

                        if (!drawingInProgress)
                        {
                            /// Drawing not in progress, no winners drawn
                            drawCell.Enabled = false;
                            validateCell.Enabled = false;
                            imageCell.Value = Properties.Resources.None;
                            row.Cells[10].Value = 0;
                            row.Cells[11].Value = 0;
                            row.Cells[12].Value = null;
                            row.Cells[13].Value = null;
                            dgvDrawings.Invalidate();
                        }
                        else
                        {
                            /// Drawing in progress, winners drawn
                            if (row.Cells[10].Value.ToString() == "0")
                            {
                                /// No winner drawn for this prize
                                drawCell.Enabled = true;
                                validateCell.Enabled = false;
                                imageCell.Value = Properties.Resources.None;
                                dgvDrawings.Invalidate();
                            }
                            else
                            {
                                drawCell.UseColumnTextForButtonValue = false;
                                /// Winner drawn for this prize.
                                if (row.Cells[16].Value.ToString().ToUpper() == "TRUE")
                                {
                                    drawCell.Value = String.Empty;
                                    drawCell.Enabled = false;
                                    validateCell.UseColumnTextForButtonValue = false;
                                    validateCell.Value = TextRes.Get("Validated", Program.ci);
                                    validateCell.Enabled = false;
                                    imageCell.Value = Properties.Resources.Check;
                                }
                                else
                                {
                                    drawCell.Value = TextRes.Get("Redraw", Program.ci);
                                    drawCell.Enabled = true;
                                    validateCell.Enabled = true;
                                    imageCell.Value = Properties.Resources.Unknown;
                                    Service.Drawing drawing = service.GetDrawing(int.Parse(dgvDrawings.Rows[row.Index].Cells[9].Value.ToString()));
                                    string[] timeSpan = drawing.AfterCountdown.Split(':');

                                    int minutes = int.Parse(timeSpan[0]);
                                    int seconds = int.Parse(timeSpan[1]);

                                    timerCell.Time = new TimeSpan(0, minutes, seconds);

                                    timerCell.DrawingId = int.Parse(dgvDrawings.Rows[row.Index].Cells[9].Value.ToString());

                                    if (Common.Instance.UseAnimation)
                                    {
                                        timerCell.TimeStamp = drawing.TimeOut;
                                    }
                                    else
                                    {
                                        timerCell.TimeStamp = drawing.TimeStarted;
                                    }

                                    timerCell.Start();
                                }
                                dgvDrawings.Invalidate();
                            }
                        }

                        if (remainingEntries <= 0 && ((DrawingClient.Service.Drawing)(dgvDrawings.Rows[row.Index].DataBoundItem)).PlayerAccountNum == null)
                        {
                            ClearRow(row.Index);
                        }
                    }
                    dgvDrawings.Invalidate();

                    tbPromoStart.Text = drawingGroup.Promo.StartDate.ToString(Program.ci);
                    tbPromoEnd.Text = drawingGroup.Promo.EndDate.ToString(Program.ci);
                    tbDrumName.Text = drawingGroup.Drum.Name;
                    tbDrumType.Text = TextRes.Get(drawingGroup.Drum.Type, Program.ci);
                    if (Convert.ToDateTime(drawingGroup.Drum.LastPopulated.ToString()).ToString() != SqlDateTime.MinValue.ToString())
                        tbLastPopulated.Text = drawingGroup.Drum.LastPopulated.ToString(Program.ci);
                    tbPlayers.Text = drawingGroup.Drum.NumberOfPlayers.ToString();
                    tbEntries.Text = drawingGroup.Drum.NumberOfEntries.ToString();
                    Int32 avg = 0;
                    if (drawingGroup.Drum.NumberOfEntries > 0 && drawingGroup.Drum.NumberOfPlayers > 0)
                        avg = Int32.Parse(drawingGroup.Drum.NumberOfEntries.ToString()) / Int32.Parse(drawingGroup.Drum.NumberOfPlayers.ToString());
                    tbAvgEntries.Text = avg.ToString();
                    UpdateDrumInfo();
                    dgvDrawings.Enabled = true;
                    tUpdateStats.Enabled = true;
                }
                else
                {
                    tbPromoStart.Text = String.Empty;
                    tbPromoEnd.Text = String.Empty;
                    tbDrawing.Text = String.Empty;
                    tbNextDrawing.Text = String.Empty;
                    tbDrumName.Text = String.Empty;
                    tbDrumType.Text = String.Empty;
                    tbLastPopulated.Text = String.Empty;
                    tbPlayers.Text = String.Empty;
                    tbEntries.Text = String.Empty;
                    tbAvgEntries.Text = String.Empty;
                    btnResetDrawing.Enabled = false;
                    btnPopulateDrum.Enabled = false;
                    linkLabel1.Enabled = false;
                    btnCompleteDrawing.Enabled = false;
                    bsDrawings.DataSource = null;
                    drawingGroup = null;
                    tUpdateStats.Enabled = false;
                    //btnSolicitDigitalDisplays.Enabled = false;
                }
                this.Enabled = true;
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        /// <summary>
        /// Fired when GetDrawingGroup has completed.
        /// </summary>
        /// <param name="d"></param>
        void GetDrawingGroupCompleted(Service.DrawingGroup d)
        {
            DisplayDrawingGroup(d);
        }
        #endregion DrawingGroup

        #region ControlEvents
        private void aboutDrawingClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmAbout aboutForm = new frmAbout();
                aboutForm.ShowDialog();
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        private void tDrawTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                BackgroundWorker bw = new BackgroundWorker();
                bw.WorkerSupportsCancellation = true;
                bw.DoWork += new DoWorkEventHandler(bw_DoWork_DrawTimer);
                bw.RunWorkerAsync();
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        void bw_DoWork_DrawTimer(object sender, DoWorkEventArgs e)
        {
            //try
            //{
            //    String drawingStatus = Common.Instance.DCService.GetDrawingStatus();
            //    if (drawingStatus == "Winner Posted")
            //    {
            //        if (rowToDraw <= (dgvDrawings.Rows.Count - 1))
            //        {
            //            DrawWinner(Int32.Parse(dgvDrawings.Rows[rowToDraw].Cells[9].Value.ToString()), rowToDraw);
            //            rowToDraw++;
            //        }
            //        else
            //        {
            //            tDrawTimer.Enabled = false;
            //        }
            //    }
            //}
            //catch (Exception ex) { Error.Log(ex, true); }
        }

        private void dgvDrawings_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                DataGridView.HitTestInfo hitTestInfo = dgvDrawings.HitTest(e.X, e.Y);
                if (hitTestInfo.RowIndex > -1)
                {
                    DataGridViewDisableButtonCell drawCell = (DataGridViewDisableButtonCell)dgvDrawings.Rows[hitTestInfo.RowIndex].Cells[7];
                    DataGridViewDisableButtonCell validateCell = (DataGridViewDisableButtonCell)dgvDrawings.Rows[hitTestInfo.RowIndex].Cells[8];
                    dgvDrawings.SelectedRows[0].Selected = false;
                    dgvDrawings.Rows[hitTestInfo.RowIndex].Selected = true;
                    switch (hitTestInfo.ColumnIndex)
                    {
                        case 7:
                            if (drawCell.Enabled)
                            {
                                if (drawCell.Value.ToString() == TextRes.Get("Draw", Program.ci))
                                {
                                    btnDrawAllWinners.Enabled = false;
                                    DrawWinner(hitTestInfo.RowIndex);
                                }
                                else
                                {
                                    DialogResult resultRedraw = MessageBox.Show(TextRes.Get("RedrawConfirm", Program.ci), TextRes.Get("Redraw", Program.ci), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (resultRedraw == DialogResult.Yes)
                                        RedrawWinner(Int32.Parse(dgvDrawings.Rows[hitTestInfo.RowIndex].Cells[9].Value.ToString()), hitTestInfo.RowIndex);
                                }
                            }
                            break;
                        case 8:
                            if (validateCell.Enabled)
                            {
                                DialogResult resultValidate = MessageBox.Show(TextRes.Get("ConfirmWinner", Program.ci), TextRes.Get("Validate", Program.ci), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (resultValidate == DialogResult.Yes)
                                {
                                    ValidateWinner(Int32.Parse(dgvDrawings.Rows[hitTestInfo.RowIndex].Cells[9].Value.ToString()), hitTestInfo.RowIndex);
                                    drawCell.Value = String.Empty;
                                    validateCell.UseColumnTextForButtonValue = false;
                                    validateCell.Value = TextRes.Get("Validated", Program.ci);
                                    drawCell.Enabled = false;
                                    validateCell.Enabled = false;
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        private void btnDrawAllWinners_Click(object sender, EventArgs e)
        {
            try
            {
                if (remainingEntries < dgvDrawings.Rows.Count)
                {
                    DialogResult result = MessageBox.Show(TextRes.Get("DrawPrizeConfirm", Program.ci), TextRes.Get("Drawing", Program.ci), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result != DialogResult.Yes)
                    {
                        return;
                    }
                }
                else
                {
                    btnDrawAllWinners.Enabled = false;
                    for (int rowCount = 0; rowCount <= dgvDrawings.Rows.Count - 1; rowCount++)
                    {
                        DrawWinner(rowCount);
                    }
                }
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        private void dgvDrawings_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvDrawings.IsCurrentCellDirty)
                    dgvDrawings.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        private void btnStartCountdown_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                Service.DCService service = new DrawingClient.Service.DCService();
                service.StartCountdown(drawingGroup.PromoId);
                btnStartCountdown.Enabled = false;
                btnStartDrawing.Enabled = true;
                cbActivePromotions.Enabled = false;
                if (Common.Instance.UseAnimation)
                    btnPopulateDrum.Enabled = true;
                this.Enabled = true;
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        private void cbActivePromotions_SelectedValueChanged(object sender, EventArgs e)
        {
            btnStartCountdown.Enabled = false;
            btnStartDrawing.Enabled = false;
            btnStopCountdown.Enabled = false;
            btnDrawAllWinners.Enabled = false;
            dgvDrawings.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            GetDrawingGroup();
        }

        private void btnStopCountdown_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                Service.DCService service = new DrawingClient.Service.DCService();
                string message = service.GetPossibleCount(drawingGroup.PromoId, drawingGroup.BucketId, drawingGroup.DrawDate);
                if (message == String.Empty)
                    MessageBox.Show(message, TextRes.Get("StopCountdown", Program.ci), MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult result = MessageBox.Show(TextRes.Get("StopCountdownConfirm", Program.ci), TextRes.Get("StopCountdown", Program.ci), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    service.StopCountdown(drawingGroup.PromoId, drawingGroup.BucketId, drawingGroup.DrawDate);
                    foreach (DataGridViewRow row in dgvDrawings.Rows)
                    {
                        DataGridViewDisableButtonCell buttonCell = (DataGridViewDisableButtonCell)row.Cells[7];
                        buttonCell.Enabled = true;
                    }
                    btnDrawAllWinners.Enabled = true;
                    btnStopCountdown.Enabled = false;
                    MessageBox.Show(TextRes.Get("Countdownstopped", Program.ci), TextRes.Get("StopCountdown", Program.ci), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Enabled = true;
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        private void btnPopulateDrum_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                populateDrumForm = new frmPopulateDrum(drawingGroup.PromoId, drawingGroup.BucketId, drawingGroup.DrawDate, drawingGroup.CheckinStartTime, drawingGroup.CheckinEndTime, false);
                populateDrumForm.DrumOptimized += new frmPopulateDrum.DrumOptimizedEventHandler(populateDrumForm_DrumOptimized);
                populateDrumForm.ShowDialog();
                Service.DCService service = new DrawingClient.Service.DCService();

                drawingGroup = service.GetDrawingGroup(drawingGroup.PromoId);
                UpdateDrumInfo();
                this.Enabled = true;
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        private void btnStartDrawing_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                frmPopulateDrum popDrum = new frmPopulateDrum(drawingGroup.PromoId, drawingGroup.BucketId, drawingGroup.DrawDate, drawingGroup.CheckinStartTime, drawingGroup.CheckinEndTime, true);
                popDrum.DrumOptimized += new frmPopulateDrum.DrumOptimizedEventHandler(populateDrumForm_DrumOptimized);
                popDrum.ShowDialog();
                Service.DCService service = new DrawingClient.Service.DCService();

                drawingGroup = service.GetDrawingGroup(drawingGroup.PromoId);

                UpdateDrumInfo();

                cbActivePromotions.Enabled = false;

                if (drawingGroup.Drum.NumberOfEntries > 0)
                {
                    btnStartDrawing.Enabled = false;
                    btnPopulateDrum.Enabled = false;
                    if (Common.Instance.UseAnimation)
                        btnStopCountdown.Enabled = true;
                    else
                        btnStopCountdown.Enabled = false;

                    if (Common.Instance.UseAnimation)
                    {
                        btnDrawAllWinners.Enabled = false;
                    }
                    else
                    {
                        foreach (DataGridViewRow row in dgvDrawings.Rows)
                        {
                            DataGridViewDisableButtonCell buttonCell = (DataGridViewDisableButtonCell)row.Cells[7];
                            buttonCell.Enabled = true;
                        }
                        btnDrawAllWinners.Enabled = true;
                    }
                    dgvDrawings.Invalidate();
                }
                else
                {
                    MessageBox.Show(TextRes.Get("NoDrawEntries", Program.ci), TextRes.Get("Drawing", Program.ci), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbActivePromotions.Enabled = true;
                }
                this.Enabled = true;
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        private void btnCompleteDrawing_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                frmCompleteDrawing completeDrawingForm = new frmCompleteDrawing();
                completeDrawingForm.ShowDialog();
                if (completeDrawingForm.DialogResult == DialogResult.Yes)
                {
                    if (tbNextDrawing.Text != TextRes.Get("NoneText", Program.ci))
                    {
                        drawingInProgress = false;
                        Service.DCService service = new DrawingClient.Service.DCService();
                        Service.DrawingGroup d = service.CompleteDrawing(drawingGroup.PromoId, drawingGroup.BucketId, DateTime.Parse(tbDrawing.Text), Common.Instance.UserID);
                        DisplayDrawingGroup(d);
                    }
                    else
                    {
                        drawingInProgress = false;
                        Service.DCService service = new DrawingClient.Service.DCService();
                        Service.DrawingGroup d = service.CompleteDrawing(drawingGroup.PromoId, drawingGroup.BucketId, DateTime.Parse(tbDrawing.Text), Common.Instance.UserID);
                        BindActivePromotions();
                    }
                    cbActivePromotions.Enabled = true;
                    MessageBox.Show(TextRes.Get("DrawingComplete", Program.ci), TextRes.Get("CompleteDrawing", Program.ci), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Enabled = true;
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        frmDrumHistory drumHistoryForm = null;

        private void btnViewDrumHistory_Click(object sender, EventArgs e)
        {
            try
            {
                drumHistoryForm = new frmDrumHistory(drawingGroup);
                drumHistoryForm.Show();
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        private void btnResetDrawing_Click(object sender, EventArgs e)
        {
            try
            {
                btnResetDrawing.Enabled = false;
                ResetDrawingEventHandler r = new ResetDrawingEventHandler(ResetDrawing);
                this.Invoke(r);
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        void ResetDrawing()
        {
            try
            {
                tUpdateStats.Enabled = false;
                this.Enabled = false;
                frmResetDrawing resetDrawingForm = new frmResetDrawing();
                resetDrawingForm.ShowDialog();
                if (resetDrawingForm.DialogResult == DialogResult.Yes)
                {
                    Guid requestId = Guid.NewGuid();
                    Service.DCService service = new DrawingClient.Service.DCService();
                    service.ResetDrawingCompleted += new DrawingClient.Service.ResetDrawingCompletedEventHandler(DCService_ResetDrawingCompleted);
                    service.ResetDrawingAsync(drawingGroup.PromoId, drawingGroup.BucketId, drawingGroup.DrawDate, requestId);
                }
                else
                {
                    this.Enabled = true;
                    btnResetDrawing.Enabled = true;
                }
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        private void dgvDrawings_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgvDrawings.RowHeadersDefaultCellStyle.ForeColor))
            {
                int rowNumber = e.RowIndex + 1;
                e.Graphics.DrawString(rowNumber.ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
            }
        }

        private void onlineSupportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Service.DCService service = new DrawingClient.Service.DCService();
            System.Diagnostics.Process.Start(service.GetOnlineHelp());
        }

        private void totalPromoUserAdminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("Total Promo User Admin.exe");
            }
            catch
            {
                MessageBox.Show(TextRes.Get("TPUserAdminNotFound", Program.ci), TextRes.Get("ToolNotFound", Program.ci), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void totalPromoCustomerServiceDesktopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("Total Promo Customer Service Desktop.exe");
            }
            catch
            {
                MessageBox.Show(TextRes.Get("TPCustServiceNotFound", Program.ci), TextRes.Get("ToolNotFound", Program.ci), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void totalPromoMarketingManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("Total Promo Marketing Manager.exe");
            }
            catch
            {
                MessageBox.Show(TextRes.Get("TPMktgMngerNotFound", Program.ci), TextRes.Get("ToolNotFound", Program.ci), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void animationClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("Animation\\TotalPromo.exe");
            }
            catch
            {
                MessageBox.Show(TextRes.Get("AnimationClientNotFound", Program.ci), TextRes.Get("ToolNotFound", Program.ci), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion ControlEvents

        #region DrawWinner
        void DrawWinner(Int32 rowIndex)
        {
            try
            {
                if (remainingEntries > 0)
                {
                    currentRow = rowIndex;
                    Int32 drawingId = Int32.Parse(dgvDrawings.Rows[currentRow].Cells[9].Value.ToString());
                    player = service.DrawWinner(drawingGroup.PromoId, drawingGroup.BucketId, drawingId, Common.Instance.UserID, rowIndex + 1);

                    if (player == null)
                    {
                        if (!displayedNoPlayers)
                        {
                            MessageBox.Show("No more valid winners.", TextRes.Get("Draw", Program.ci), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            displayedNoPlayers = true;
                        }
                        cbActivePromotions.Enabled = true;
                        this.Enabled = true;
                        this.Focus();
                        ClearRow(rowIndex);
                        return;
                    }

                    ShowDrawnWinner(player);
                    StartCountdown();

                    if (Common.Instance.UseAnimation)
                    {
                        frmDraw drawForm = new frmDraw(player);
                        drawForm.PostWinnerAndStartCountdown += new frmDraw.PostWinnerAndStartCountdownEventHandler(PostWinnerAndStartCountdown);
                        drawForm.ShowDialog();
                    }
                }
                else
                {
                    if (!displayedNoEntries)
                    {
                        MessageBox.Show(TextRes.Get("NoMoreDrawEntries", Program.ci), TextRes.Get("Draw", Program.ci), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        displayedNoEntries = true;
                        this.Enabled = true;
                    }
                    ClearRow(rowIndex);
                }
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        private void PostWinnerAndStartCountdown(DrawingClient.Service.Player player)
        {
            if (player != null)
            {
                service.SetWinnerPostedByWinnerId(player.WinnerId);
            }

            this.Activate();
        }

        /*
         * This method was created to break out the logic of the StartCountdown method.
         * We want to show the last winner drawn immediately in the client but we still want
         * to wait to draw the next winner until after the Flash Animation has set the DrawingStatus
         * to Winner Posted in the DB tblMasssDrawingSpecs
         * */
        void ShowDrawnWinner(DrawingClient.Service.Player player)
        {
            if (player == null)
                return;

            remainingEntries--;
            DataGridViewTimerCell timerCell = (DataGridViewTimerCell)dgvDrawings.Rows[currentRow].Cells[5];
            DataGridViewImageCell imageCell = (DataGridViewImageCell)dgvDrawings.Rows[currentRow].Cells[6];
            DataGridViewDisableButtonCell drawCell = (DataGridViewDisableButtonCell)dgvDrawings.Rows[currentRow].Cells[7];
            DataGridViewDisableButtonCell validateCell = (DataGridViewDisableButtonCell)dgvDrawings.Rows[currentRow].Cells[8];

            imageCell.Value = Properties.Resources.Unknown;

            Int32 drawingId = Int32.Parse(dgvDrawings.Rows[currentRow].Cells[9].Value.ToString());
            Service.DCService service = new DrawingClient.Service.DCService();

            Service.Drawing drawing = service.GetDrawing(drawingId);

            if (winnerMustBePresent)
            {
                string[] timeSpan = drawing.AfterCountdown.Split(':');

                int minutes = int.Parse(timeSpan[0]);
                int seconds = int.Parse(timeSpan[1]);

                timerCell.Time = new TimeSpan(0, minutes, seconds);
                timerCell.Value = string.Format("{0}:{1:00}", minutes, seconds);

                drawCell.UseColumnTextForButtonValue = false;
                drawCell.Value = TextRes.Get("Redraw", Program.ci);
                validateCell.Enabled = true;
            }
            else
            {
                drawCell.UseColumnTextForButtonValue = false;
                drawCell.Enabled = false;
                drawCell.Value = TextRes.Get("Complete", Program.ci);
                validateCell.Enabled = false;
            }

            dgvDrawings.Rows[currentRow].Cells[2].Value = player.CMSPlayerID;
            dgvDrawings.Rows[currentRow].Cells[3].Value = player.PlayerName;
            dgvDrawings.Rows[currentRow].Cells[4].Value = player.PlayerDOB.ToString("d", Program.ci);
            dgvDrawings.Rows[currentRow].Cells[10].Value = player.EntryId.ToString();
            dgvDrawings.Rows[currentRow].Cells[11].Value = player.PlayerAccountNum.ToString();
            dgvDrawings.Rows[currentRow].Cells[12].Value = player.CMSPlayerID.ToString();
            dgvDrawings.Invalidate();
        }

        /*
         * This method is called after the above method drawForm_ShowDrawnWinner and is now
         * only responsable for starting the countdown which is already showing in the timer cell
         * */
        void StartCountdown()
        {
            DataGridViewTimerCell timerCell = (DataGridViewTimerCell)dgvDrawings.Rows[currentRow].Cells[5];
            DataGridViewDisableButtonCell drawCell = (DataGridViewDisableButtonCell)dgvDrawings.Rows[currentRow].Cells[7];
            DataGridViewDisableButtonCell validateCell = (DataGridViewDisableButtonCell)dgvDrawings.Rows[currentRow].Cells[8];

            //We must call this now to find the DrawnAt (aka 'Timeout') to sync the countdown
            Int32 drawingId = Int32.Parse(dgvDrawings.Rows[currentRow].Cells[9].Value.ToString());
            Service.DCService service = new DrawingClient.Service.DCService();

            Service.Drawing drawing = service.GetDrawing(drawingId);

            if (winnerMustBePresent)
            {
                timerCell.DrawingId = drawingId;

                if (Common.Instance.UseAnimation)
                {
                    timerCell.TimeStamp = drawing.TimeOut;
                }
                else
                {
                    timerCell.TimeStamp = drawing.TimeStarted;
                }
                timerCell.Start();
            }
            dgvDrawings.Invalidate();
        }
        #endregion DrawWinner

        #region ValidateWinner
        void ValidateWinner(Int32 drawingId, Int32 rowIndex)
        {
            try
            {
                DataGridViewTimerCell timerCell = (DataGridViewTimerCell)dgvDrawings.Rows[rowIndex].Cells[5];
                DataGridViewDisableButtonCell drawCell = (DataGridViewDisableButtonCell)dgvDrawings.Rows[rowIndex].Cells[7];
                DataGridViewDisableButtonCell validateCell = (DataGridViewDisableButtonCell)dgvDrawings.Rows[rowIndex].Cells[8];
                DataGridViewImageCell imageCell = (DataGridViewImageCell)dgvDrawings.Rows[rowIndex].Cells[6];
                if (validateCell.Enabled)
                {
                    imageCell.Value = Properties.Resources.Check;
                    timerCell.Stop();
                    drawCell.Enabled = false;
                    drawCell.Value = TextRes.Get("Complete", Program.ci);
                    validateCell.Enabled = false;
                    dgvDrawings.Invalidate();
                    String playerAccountNum = dgvDrawings.Rows[rowIndex].Cells[11].Value.ToString();
                    String playerCMSId = dgvDrawings.Rows[rowIndex].Cells[12].Value.ToString();
                    Int32 ticketNumber = Int32.Parse(dgvDrawings.Rows[rowIndex].Cells[10].Value.ToString());
                    Service.DCService service = new DrawingClient.Service.DCService();
                    service.Validate(drawingGroup.PromoId, drawingGroup.BucketId, drawingId, playerAccountNum, playerCMSId, ticketNumber, Common.Instance.UserID);
                }
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }
        #endregion ValidateWinner

        #region RedrawWinner
        void RedrawWinner(Int32 drawingId, Int32 rowIndex)
        {
            try
            {
                Service.DCService service = new DrawingClient.Service.DCService();
                service.DisqualifyWinner(drawingGroup.PromoId, drawingGroup.BucketId, drawingId, dgvDrawings.Rows[rowIndex].Cells[11].Value.ToString(), dgvDrawings.Rows[rowIndex].Cells[12].Value.ToString(), Int32.Parse(dgvDrawings.Rows[rowIndex].Cells[10].Value.ToString()));
                DrawWinner(rowIndex);
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }
        #endregion RedrawWinner

        private void ClearRow(int rowIndex)
        {
            dgvDrawings.Rows[rowIndex].Cells[0].Value = String.Empty;
            dgvDrawings.Rows[rowIndex].Cells[1].Value = decimal.Parse("0");
            dgvDrawings.Rows[rowIndex].Cells[2].Value = String.Empty;
            dgvDrawings.Rows[rowIndex].Cells[3].Value = String.Empty;
            dgvDrawings.Rows[rowIndex].Cells[4].Value = String.Empty;
            (dgvDrawings.Rows[rowIndex].Cells[5] as DataGridViewTimerCell).Stop();
            (dgvDrawings.Rows[rowIndex].Cells[5] as DataGridViewTimerCell).Value = String.Empty;
            (dgvDrawings.Rows[rowIndex].Cells[6] as DataGridViewImageCell).Value = Properties.Resources.None;
            (dgvDrawings.Rows[rowIndex].Cells[7] as DataGridViewDisableButtonCell).Enabled = false;
            (dgvDrawings.Rows[rowIndex].Cells[8] as DataGridViewDisableButtonCell).Enabled = false;
            dgvDrawings.Rows[rowIndex].Cells[9].Value = 0;
        }

        private void tUpdateStats_Tick(object sender, EventArgs e)
        {
            UpdateStatsEventHandler u = new UpdateStatsEventHandler(UpdateDrumInfo);
            this.Invoke(u);
        }

        delegate void UpdateStatsEventHandler();

        //2009-04-21 JMS: Waiting until next PCS release to add the button and logic
        //private void btnSolicitDigitalDisplays_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.Enabled = false;
        //        if (MessageBox.Show("The solicitation message asks patrons if they would like to " +
        //            "watch the drawing on their gaming machine. " +
        //            "Are you sure you want to broadcast the solicitation a message to all casino floor gaming machines at this time?",
        //            "Confirm Solicitation Broadcast",
        //            MessageBoxButtons.YesNo, MessageBoxIcon.Question,
        //            MessageBoxDefaultButton.Button2) == DialogResult.Yes)
        //        {
        //            Service.DCService service = new DrawingClient.Service.DCService();
        //            service.StartSolicitingEGMs(drawingGroup.PromoId);
        //            MessageBox.Show("Digital Display broadcasting.  Gathering patron's responses...", "Debug Message");
        //            btnSolicitDigitalDisplays.Enabled = false;
        //            cbActivePromotions.Enabled = false;
        //        }
        //        else
        //        {

        //        }
        //        this.Enabled = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Enabled = true;
        //        Error.Log(ex, true);
        //    }
        //}
    }
}