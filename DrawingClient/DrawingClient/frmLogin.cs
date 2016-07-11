using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DrawingClient
{
    public partial class frmLogin : Form
    {
        bool error = false;

        public frmLogin()
        {
            InitializeComponent();
            SetUIControlText();
        }

        private void SetUIControlText()
        {
            try
            {
                this.Text = TextRes.Get("frmLogin_Title", Program.ci);
                lblUsername.Text = TextRes.Get("Username", Program.ci);
                lblPassword.Text = TextRes.Get("Password", Program.ci);
                cbUseAnimation.Text = TextRes.Get("UseAnimation", Program.ci);
                btnLogin.Text = TextRes.Get("Login", Program.ci);
            }
            catch (Exception ex) { Error.Log(ex, true); }
        }

        private void HandleTextChanged(object sender, EventArgs e)
        {
            if (error)
                ValidateForm();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        bool ValidateForm()
        {
            bool error = false;

            if (tbUsername.Text.Trim().Length > 0)
            {
                epError.SetError(tbUsername, "");
            }
            else
            {
                epError.SetError(tbUsername, TextRes.Get("Required", Program.ci));
                error = true;
            }

            if (tbPassword.Text.Trim().Length > 0)
            {
                epError.SetError(tbPassword, "");
            }
            else
            {
                epError.SetError(tbPassword, TextRes.Get("Required", Program.ci));
                error = true;
            }
            return error;
        }

        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Login();
            if (e.KeyCode == Keys.Escape)
                Application.Exit();
        }

        void Login()
        {
            if (!ValidateForm())
            {
                tbUsername.Enabled = false;
                tbPassword.Enabled = false;
                cbUseAnimation.Enabled = false;
                btnLogin.Enabled = false;
                try
                {
                    Service.DCService service = new DrawingClient.Service.DCService();

                    if (!((Service.DrawingClientStatus)service.GetDrawingClientInUse()).DrawingClientInUse)
                    {
                        Service.User user = service.ValidateUser(tbUsername.Text, tbPassword.Text);
                        if (user != null)
                        {
                            //MR 12/22/09 Adding this code to deal with locked accounts.
                            if (user.LoginStatus == "LCK")
                            {
                                MessageBox.Show(TextRes.Get("LockedAccount", Program.ci), TextRes.Get("Login", Program.ci), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                tbUsername.Enabled = true;
                                tbPassword.Enabled = true;
                                cbUseAnimation.Enabled = true;
                                btnLogin.Enabled = true;
                            }
                            else
                            {
                                Common.Instance.UserID = user.UserID;
                                Common.Instance.UserIDText = user.UserIDText;
                                Common.Instance.UserLevel = user.UserLevel;
                                Common.Instance.UseAnimation = cbUseAnimation.Checked;
                                service.SetDrawingClientInUse("DrawingClientOpen", tbUsername.Text);
                                this.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show(TextRes.Get("InvalidLogin", Program.ci), TextRes.Get("Login", Program.ci), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tbUsername.Enabled = true;
                            tbPassword.Enabled = true;
                            cbUseAnimation.Enabled = true;
                            btnLogin.Enabled = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show(TextRes.Get("DrawingClientInUse", Program.ci), TextRes.Get("Login", Program.ci), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tbUsername.Enabled = true;
                        tbPassword.Enabled = true;
                        cbUseAnimation.Enabled = true;
                        btnLogin.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    Error.Log(ex, true);
                    tbUsername.Enabled = true;
                    tbPassword.Enabled = true;
                    cbUseAnimation.Enabled = true;
                    btnLogin.Enabled = true;
                }
            }
            else
            {
                error = true;
            }
        }

        //void Localize()
        //{
        //    this.Text = Localization.Login;
        //    lblUsername.Text = Localization.Username;
        //    lblPassword.Text = Localization.Password;
        //    cbUseAnimation.Text = Localization.UseAnimation;
        //    btnLogin.Text = Localization.Login;
        //}

        private void frmLogin_Load(object sender, EventArgs e)
        {
            //Localize();
//#if DEBUG
//            tbUsername.Text = "rshotkoski";
//            tbPassword.Text = "password";
//#endif
        }
    }
}
