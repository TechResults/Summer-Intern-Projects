﻿namespace GMD_Form
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
            this.registerLastName.Location = new System.Drawing.Point(101, 12);
            this.registerLastName.Name = "registerLastName";
            this.registerLastName.Size = new System.Drawing.Size(100, 22);
            this.registerLastName.TabIndex = 1;
            this.registerLastName.Text = "Last Name";
            // 
            // register
            // 
            this.register.Location = new System.Drawing.Point(126, 97);
            this.register.Name = "register";
            this.register.Size = new System.Drawing.Size(75, 32);
            this.register.TabIndex = 2;
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
            this.registerMobileNum.TabIndex = 4;
            this.registerMobileNum.Text = "Mobile Number";
            // 
            // registerPIN
            // 
            this.registerPIN.Location = new System.Drawing.Point(134, 69);
            this.registerPIN.MaxLength = 4;
            this.registerPIN.Name = "registerPIN";
            this.registerPIN.Size = new System.Drawing.Size(67, 22);
            this.registerPIN.TabIndex = 5;
            this.registerPIN.Text = "PIN";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 469);
            this.Controls.Add(this.registerPIN);
            this.Controls.Add(this.registerMobileNum);
            this.Controls.Add(this.registerSocialSecurityNumber);
            this.Controls.Add(this.register);
            this.Controls.Add(this.registerLastName);
            this.Controls.Add(this.registerFirstName);
            this.Name = "Form1";
            this.Text = "Form1";
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
    }
}
