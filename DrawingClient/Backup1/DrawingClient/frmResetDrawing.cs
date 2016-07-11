using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DrawingClient
{
    public partial class frmResetDrawing : Form
    {
        public frmResetDrawing()
        {
            InitializeComponent();
            SetUIControlText();
        }

        private void SetUIControlText()
        {
            try
            {
                this.cbOK.Text = TextRes.Get("ResetConfirm", Program.ci);
                this.btnOK.Text = TextRes.Get("OK", Program.ci);
                this.btnCancel.Text = TextRes.Get("Cancel", Program.ci);
                this.Text = TextRes.Get("frmResetDrawing_Title", Program.ci);
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void cbOK_CheckedChanged(object sender, EventArgs e)
        {
            if (cbOK.Checked)
                btnOK.Enabled = true;
            else
                btnOK.Enabled = false;
        }
    }
}