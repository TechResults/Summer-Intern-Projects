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
            this.firstName = new System.Windows.Forms.TextBox();
            this.lastName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // firstName
            // 
            this.firstName.Location = new System.Drawing.Point(13, 13);
            this.firstName.Name = "firstName";
            this.firstName.Size = new System.Drawing.Size(81, 22);
            this.firstName.TabIndex = 0;
            this.firstName.Text = "First Name";
            // 
            // lastName
            // 
            this.lastName.Location = new System.Drawing.Point(101, 12);
            this.lastName.Name = "lastName";
            this.lastName.Size = new System.Drawing.Size(100, 22);
            this.lastName.TabIndex = 1;
            this.lastName.TextChanged += new System.EventHandler(this.lastName_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 469);
            this.Controls.Add(this.lastName);
            this.Controls.Add(this.firstName);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox firstName;
        private System.Windows.Forms.TextBox lastName;
    }
}

