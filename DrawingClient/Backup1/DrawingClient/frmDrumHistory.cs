using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlTypes;
using System.Globalization;

namespace DrawingClient
{
    public partial class frmDrumHistory : Form
    {
        Service.DrawingGroup drawingGroup;

        public frmDrumHistory()
        {
            InitializeComponent();
            SetUIControlText();
        }

        private void SetUIControlText()
        {
            try
            {
                this.Column1.HeaderText = TextRes.Get("DateTime", Program.ci);
                this.Column1.DefaultCellStyle.FormatProvider = Program.ci;
                this.Column4.HeaderText = TextRes.Get("Id", Program.ci);
                this.Column5.HeaderText = TextRes.Get("PromotionId", Program.ci);
                this.Column6.HeaderText = TextRes.Get("BucketId", Program.ci);
                this.Column2.HeaderText = TextRes.Get("NumberofEntries", Program.ci);
                this.Column3.HeaderText = TextRes.Get("NumberofPlayers", Program.ci);
                this.Text = TextRes.Get("frmDrumHistory_Title", Program.ci);
                this.groupBox2.Text = TextRes.Get("DrumPanel", Program.ci);
                this.label1.Text = TextRes.Get("DrumName", Program.ci);
                this.label5.Text = TextRes.Get("Entries", Program.ci);
                this.label6.Text = TextRes.Get("Players", Program.ci);
                this.label3.Text = TextRes.Get("LastPopulated", Program.ci);
                this.label4.Text = TextRes.Get("DrumType", Program.ci);
                this.groupBox1.Text = TextRes.Get("HistoryPanel", Program.ci);
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }


        public frmDrumHistory(Service.DrawingGroup group)
        {
            InitializeComponent();
            SetUIControlText();
            drawingGroup = group;
        }

        public void Update(string drumName, string drumType, string lastPopulated, Int32 players, Int32 entries)
        {
            UpdateStatsEventHandler u = new UpdateStatsEventHandler(UpdateStatsCallback);
            this.Invoke(u, new object[] { drumName, drumType, lastPopulated, players, entries });
        }

        delegate void UpdateStatsEventHandler(string drumName, string drumType, string lastPopulated, Int32 players, Int32 entries);

        void UpdateStatsCallback(string drumName, string drumType, string lastPopulated, Int32 players, Int32 entries)
        {
            Service.DCService service = new DrawingClient.Service.DCService();
            dgvDrumHistory.DataSource = service.GetDrumHistory(drawingGroup.PromoId, drawingGroup.BucketId).Tables[0].DefaultView;
            tbDrumName.Text = drumName;
            tbDrumType.Text = drumType;
            if (Convert.ToDateTime(lastPopulated) == SqlDateTime.MinValue)
                tbLastPopulated.Text = TextRes.Get("Never", Program.ci);
            else
                tbLastPopulated.Text = DateTime.Parse(lastPopulated).ToString(Program.ci);
            tbPlayers.Text = players.ToString("N0");
            tbEntries.Text = entries.ToString("N0");
        }

        private void frmDrumHistory_Load(object sender, EventArgs e)
        {
            Service.DCService service = new DrawingClient.Service.DCService();
            dgvDrumHistory.DataSource = service.GetDrumHistory(drawingGroup.PromoId, drawingGroup.BucketId).Tables[0].DefaultView;
            Service.Drum drum = service.GetDrumLastHistoryItem(drawingGroup.PromoId, drawingGroup.BucketId);
            tbDrumName.Text = drawingGroup.Drum.Name;
            tbDrumType.Text = TextRes.Get(drawingGroup.Drum.Type, Program.ci);
            if (Convert.ToDateTime(drum.LastPopulated.ToString()) == SqlDateTime.MinValue)
                tbLastPopulated.Text = TextRes.Get("Never", Program.ci);
            else
                tbLastPopulated.Text = drum.LastPopulated.ToString(Program.ci);
            tbPlayers.Text = drum.NumberOfPlayers.ToString("N0");
            tbEntries.Text = drum.NumberOfEntries.ToString("N0");
        }
    }
}