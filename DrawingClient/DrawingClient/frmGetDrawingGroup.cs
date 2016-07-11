using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DrawingClient
{
    public partial class frmGetDrawingGroup : Form
    {
        private Int32 _promotionId;

        private Service.DrawingGroup _drawingGroup;

        public Service.DrawingGroup DrawingGroup
        {
            get { return _drawingGroup; }
            set { _drawingGroup = value; }
        }

        public frmGetDrawingGroup(Int32 promotionId)
        {
            try
            {
                InitializeComponent();
                SetUIControlText();
                this._promotionId = promotionId;
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        private void SetUIControlText()
        {
            try
            {
                this.Text = TextRes.Get("Promotions", Program.ci);
                label1.Text = TextRes.Get("PleaseWait", Program.ci);
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        void DCService_GetDrawingGroupCompleted(object sender, DrawingClient.Service.GetDrawingGroupCompletedEventArgs e)
        {
            try
            {
                this._drawingGroup = e.Result;
                close = true;
                this.Close();
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        private void frmGetDrawingGroup_Load(object sender, EventArgs e)
        {
            try
            {
                Guid requestId = Guid.NewGuid();
                Service.DCService service = new DrawingClient.Service.DCService();
                service.GetDrawingGroupCompleted += new DrawingClient.Service.GetDrawingGroupCompletedEventHandler(DCService_GetDrawingGroupCompleted);
                //service.GetDrawingGroup(_promotionId);
                service.GetDrawingGroupAsync(_promotionId, requestId);
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        bool close = false;

        private void frmGetDrawingGroup_FormClosing(object sender, FormClosingEventArgs e)
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