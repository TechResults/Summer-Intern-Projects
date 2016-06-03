namespace GMD_Form
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.registerFirstName = new System.Windows.Forms.TextBox();
            this.registerLastName = new System.Windows.Forms.TextBox();
            this.register = new System.Windows.Forms.Button();
            this.registerSocialSecurityNumber = new System.Windows.Forms.TextBox();
            this.registerMobileNum = new System.Windows.Forms.TextBox();
            this.registerPIN = new System.Windows.Forms.TextBox();
            this.loyaltyUpdate = new System.Windows.Forms.Button();
            this.checkLoyaltyValue = new System.Windows.Forms.Button();
            this.loginMobileNumber = new System.Windows.Forms.TextBox();
            this.loginButton = new System.Windows.Forms.Button();
            this.logoutButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // registerFirstName
            // 
            this.registerFirstName.Location = new System.Drawing.Point(13, 13);
            this.registerFirstName.Name = "registerFirstName";
            this.registerFirstName.Size = new System.Drawing.Size(81, 22);
            this.registerFirstName.TabIndex = 0;
            this.registerFirstName.Text = "First Name";
            // 
            // registerLastName
            // 
            this.registerLastName.Location = new System.Drawing.Point(100, 14);
            this.registerLastName.Name = "registerLastName";
            this.registerLastName.Size = new System.Drawing.Size(101, 22);
            this.registerLastName.TabIndex = 1;
            this.registerLastName.Text = "Last Name";
            // 
            // register
            // 
            this.register.Location = new System.Drawing.Point(13, 98);
            this.register.Name = "register";
            this.register.Size = new System.Drawing.Size(188, 32);
            this.register.TabIndex = 5;
            this.register.Text = "Register";
            this.register.UseVisualStyleBackColor = true;
            this.register.Click += new System.EventHandler(this.register_Click);
            // 
            // registerSocialSecurityNumber
            // 
            this.registerSocialSecurityNumber.Location = new System.Drawing.Point(13, 70);
            this.registerSocialSecurityNumber.Name = "registerSocialSecurityNumber";
            this.registerSocialSecurityNumber.Size = new System.Drawing.Size(114, 22);
            this.registerSocialSecurityNumber.TabIndex = 3;
            this.registerSocialSecurityNumber.Text = "Social Security #";
            // 
            // registerMobileNum
            // 
            this.registerMobileNum.Location = new System.Drawing.Point(13, 42);
            this.registerMobileNum.Name = "registerMobileNum";
            this.registerMobileNum.Size = new System.Drawing.Size(188, 22);
            this.registerMobileNum.TabIndex = 2;
            this.registerMobileNum.Text = "Mobile Number";
            // 
            // registerPIN
            // 
            this.registerPIN.Location = new System.Drawing.Point(134, 69);
            this.registerPIN.MaxLength = 4;
            this.registerPIN.Name = "registerPIN";
            this.registerPIN.Size = new System.Drawing.Size(67, 22);
            this.registerPIN.TabIndex = 4;
            this.registerPIN.Text = "PIN";
            // 
            // loyaltyUpdate
            // 
            this.loyaltyUpdate.Location = new System.Drawing.Point(13, 166);
            this.loyaltyUpdate.Name = "loyaltyUpdate";
            this.loyaltyUpdate.Size = new System.Drawing.Size(188, 74);
            this.loyaltyUpdate.TabIndex = 6;
            this.loyaltyUpdate.Text = "Add Loyalty Points";
            this.loyaltyUpdate.UseVisualStyleBackColor = true;
            this.loyaltyUpdate.Click += new System.EventHandler(this.loyaltyUpdate_Click);
            // 
            // checkLoyaltyValue
            // 
            this.checkLoyaltyValue.Location = new System.Drawing.Point(13, 247);
            this.checkLoyaltyValue.Name = "checkLoyaltyValue";
            this.checkLoyaltyValue.Size = new System.Drawing.Size(188, 72);
            this.checkLoyaltyValue.TabIndex = 7;
            this.checkLoyaltyValue.Text = "Check Loyalty Points";
            this.checkLoyaltyValue.UseVisualStyleBackColor = true;
            this.checkLoyaltyValue.Click += new System.EventHandler(this.checkLoyaltyValue_Click);
            // 
            // loginMobileNumber
            // 
            this.loginMobileNumber.Location = new System.Drawing.Point(275, 14);
            this.loginMobileNumber.Name = "loginMobileNumber";
            this.loginMobileNumber.Size = new System.Drawing.Size(156, 22);
            this.loginMobileNumber.TabIndex = 8;
            this.loginMobileNumber.Text = "Mobile Number";
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(275, 42);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(156, 32);
            this.loginButton.TabIndex = 9;
            this.loginButton.Text = "Login";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // logoutButton
            // 
            this.logoutButton.Location = new System.Drawing.Point(12, 360);
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.Size = new System.Drawing.Size(428, 97);
            this.logoutButton.TabIndex = 10;
            this.logoutButton.Text = "Logout";
            this.logoutButton.UseVisualStyleBackColor = true;
            this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 469);
            this.Controls.Add(this.logoutButton);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.loginMobileNumber);
            this.Controls.Add(this.checkLoyaltyValue);
            this.Controls.Add(this.loyaltyUpdate);
            this.Controls.Add(this.registerPIN);
            this.Controls.Add(this.registerMobileNum);
            this.Controls.Add(this.registerSocialSecurityNumber);
            this.Controls.Add(this.register);
            this.Controls.Add(this.registerLastName);
            this.Controls.Add(this.registerFirstName);
            this.Name = "Form1";
            this.Text = "GMD Client: Ellis Johnson 6/2016";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox registerFirstName;
        private System.Windows.Forms.TextBox registerLastName;
        private System.Windows.Forms.Button register;
        private System.Windows.Forms.TextBox registerSocialSecurityNumber;
        private System.Windows.Forms.TextBox registerMobileNum;
        private System.Windows.Forms.TextBox registerPIN;
        private System.Windows.Forms.Button loyaltyUpdate;
        private System.Windows.Forms.Button checkLoyaltyValue;
        private System.Windows.Forms.TextBox loginMobileNumber;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Button logoutButton;
    }
}

