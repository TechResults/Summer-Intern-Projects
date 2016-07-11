using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DrawingClient
{
    public partial class frmCompleteDrawing : Form
    {
        public frmCompleteDrawing()
        {
            InitializeComponent();
            SetUIControlText();
        }

        private void SetUIControlText()
        {
            try
            {
                label1.Text = TextRes.Get("CompleteDrawingMessage", Program.ci);
                cbCompleteDrawing.Text = TextRes.Get("CompleteDrawingConfirm", Program.ci);
                btnOK.Text = TextRes.Get("OK", Program.ci);
                btnCancel.Text = TextRes.Get("Cancel", Program.ci);
                this.Text = TextRes.Get("frmCompleteDrawing_Title", Program.ci);
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        private void cbCompleteDrawing_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCompleteDrawing.Checked)
                btnOK.Enabled = true;
            else
                btnOK.Enabled = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
    }
}