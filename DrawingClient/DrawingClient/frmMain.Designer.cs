using AIS.Windows.Forms;

namespace DrawingClient
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutDrawingClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onlineSupportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.totalPromoUserAdminToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.totalPromoCustomerServiceDesktopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.totalPromoMarketingManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.animationClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbPromoEnd = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbPromoStart = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cbActivePromotions = new System.Windows.Forms.ComboBox();
            this.tbNextDrawing = new System.Windows.Forms.TextBox();
            this.tbDrawing = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.tbAvgEntries = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDrumName = new System.Windows.Forms.TextBox();
            this.btnPopulateDrum = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbEntries = new System.Windows.Forms.TextBox();
            this.tbPlayers = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbLastPopulated = new System.Windows.Forms.TextBox();
            this.tbDrumType = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnResetDrawing = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnCompleteDrawing = new System.Windows.Forms.Button();
            this.btnStopCountdown = new System.Windows.Forms.Button();
            this.btnDrawAllWinners = new System.Windows.Forms.Button();
            this.btnStartDrawing = new System.Windows.Forms.Button();
            this.btnStartCountdown = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvDrawings = new System.Windows.Forms.DataGridView();
            this.bsDrawings = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.pnlAnimation = new System.Windows.Forms.WebBrowser();
            this.lblAnimationInUse = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTimerColumn1 = new AIS.Windows.Forms.DataGridViewTimerColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewDisableButtonColumn1 = new AIS.Windows.Forms.DataGridViewDisableButtonColumn();
            this.dataGridViewDisableButtonColumn2 = new AIS.Windows.Forms.DataGridViewDisableButtonColumn();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.tDrawTimer = new System.Windows.Forms.Timer(this.components);
            this.tUpdateStats = new System.Windows.Forms.Timer(this.components);
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new DataGridViewTimerColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column7 = new AIS.Windows.Forms.DataGridViewDisableButtonColumn();
            this.Column8 = new AIS.Windows.Forms.DataGridViewDisableButtonColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDrawings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDrawings)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(970, 24);
            this.menuStrip1.TabIndex = 1;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(12, 20);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(67, 22);
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutDrawingClientToolStripMenuItem,
            this.onlineSupportToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(12, 20);
            this.helpToolStripMenuItem.Visible = false;
            // 
            // aboutDrawingClientToolStripMenuItem
            // 
            this.aboutDrawingClientToolStripMenuItem.Name = "aboutDrawingClientToolStripMenuItem";
            this.aboutDrawingClientToolStripMenuItem.Size = new System.Drawing.Size(67, 22);
            this.aboutDrawingClientToolStripMenuItem.Click += new System.EventHandler(this.aboutDrawingClientToolStripMenuItem_Click);
            // 
            // onlineSupportToolStripMenuItem
            // 
            this.onlineSupportToolStripMenuItem.Name = "onlineSupportToolStripMenuItem";
            this.onlineSupportToolStripMenuItem.Size = new System.Drawing.Size(67, 22);
            this.onlineSupportToolStripMenuItem.Click += new System.EventHandler(this.onlineSupportToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.totalPromoUserAdminToolStripMenuItem,
            this.totalPromoCustomerServiceDesktopToolStripMenuItem,
            this.totalPromoMarketingManagerToolStripMenuItem,
            this.animationClientToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(12, 20);
            this.toolsToolStripMenuItem.Visible = false;
            // 
            // totalPromoUserAdminToolStripMenuItem
            // 
            this.totalPromoUserAdminToolStripMenuItem.Name = "totalPromoUserAdminToolStripMenuItem";
            this.totalPromoUserAdminToolStripMenuItem.Size = new System.Drawing.Size(67, 22);
            this.totalPromoUserAdminToolStripMenuItem.Click += new System.EventHandler(this.totalPromoUserAdminToolStripMenuItem_Click);
            // 
            // totalPromoCustomerServiceDesktopToolStripMenuItem
            // 
            this.totalPromoCustomerServiceDesktopToolStripMenuItem.Name = "totalPromoCustomerServiceDesktopToolStripMenuItem";
            this.totalPromoCustomerServiceDesktopToolStripMenuItem.Size = new System.Drawing.Size(67, 22);
            this.totalPromoCustomerServiceDesktopToolStripMenuItem.Click += new System.EventHandler(this.totalPromoCustomerServiceDesktopToolStripMenuItem_Click);
            // 
            // totalPromoMarketingManagerToolStripMenuItem
            // 
            this.totalPromoMarketingManagerToolStripMenuItem.Name = "totalPromoMarketingManagerToolStripMenuItem";
            this.totalPromoMarketingManagerToolStripMenuItem.Size = new System.Drawing.Size(67, 22);
            this.totalPromoMarketingManagerToolStripMenuItem.Click += new System.EventHandler(this.totalPromoMarketingManagerToolStripMenuItem_Click);
            // 
            // animationClientToolStripMenuItem
            // 
            this.animationClientToolStripMenuItem.Name = "animationClientToolStripMenuItem";
            this.animationClientToolStripMenuItem.Size = new System.Drawing.Size(67, 22);
            this.animationClientToolStripMenuItem.Click += new System.EventHandler(this.animationClientToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 712);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(970, 22);
            this.statusStrip1.TabIndex = 8;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tbPromoEnd);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.tbPromoStart);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.cbActivePromotions);
            this.groupBox1.Location = new System.Drawing.Point(4, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(711, 45);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // tbPromoEnd
            // 
            this.tbPromoEnd.Location = new System.Drawing.Point(562, 16);
            this.tbPromoEnd.Name = "tbPromoEnd";
            this.tbPromoEnd.ReadOnly = true;
            this.tbPromoEnd.Size = new System.Drawing.Size(128, 20);
            this.tbPromoEnd.TabIndex = 13;
            this.tbPromoEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(501, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(0, 13);
            this.label11.TabIndex = 12;
            // 
            // tbPromoStart
            // 
            this.tbPromoStart.Location = new System.Drawing.Point(367, 16);
            this.tbPromoStart.Name = "tbPromoStart";
            this.tbPromoStart.ReadOnly = true;
            this.tbPromoStart.Size = new System.Drawing.Size(128, 20);
            this.tbPromoStart.TabIndex = 11;
            this.tbPromoStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(303, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(0, 13);
            this.label10.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 13);
            this.label9.TabIndex = 9;
            // 
            // cbActivePromotions
            // 
            this.cbActivePromotions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbActivePromotions.FormattingEnabled = true;
            this.cbActivePromotions.Location = new System.Drawing.Point(71, 16);
            this.cbActivePromotions.Name = "cbActivePromotions";
            this.cbActivePromotions.Size = new System.Drawing.Size(226, 21);
            this.cbActivePromotions.TabIndex = 8;
            this.cbActivePromotions.SelectedValueChanged += new System.EventHandler(this.cbActivePromotions_SelectedValueChanged);
            // 
            // tbNextDrawing
            // 
            this.tbNextDrawing.Location = new System.Drawing.Point(102, 45);
            this.tbNextDrawing.Name = "tbNextDrawing";
            this.tbNextDrawing.ReadOnly = true;
            this.tbNextDrawing.Size = new System.Drawing.Size(121, 20);
            this.tbNextDrawing.TabIndex = 7;
            this.tbNextDrawing.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbDrawing
            // 
            this.tbDrawing.Location = new System.Drawing.Point(102, 19);
            this.tbDrawing.Name = "tbDrawing";
            this.tbDrawing.ReadOnly = true;
            this.tbDrawing.Size = new System.Drawing.Size(121, 20);
            this.tbDrawing.TabIndex = 6;
            this.tbDrawing.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 13);
            this.label7.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 13);
            this.label8.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.linkLabel1);
            this.groupBox2.Controls.Add(this.tbAvgEntries);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.tbDrumName);
            this.groupBox2.Controls.Add(this.btnPopulateDrum);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.tbEntries);
            this.groupBox2.Controls.Add(this.tbPlayers);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.tbLastPopulated);
            this.groupBox2.Controls.Add(this.tbDrumType);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(721, 247);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(244, 237);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Enabled = false;
            this.linkLabel1.Location = new System.Drawing.Point(76, 216);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(0, 13);
            this.linkLabel1.TabIndex = 18;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Click += new System.EventHandler(this.btnViewDrumHistory_Click);
            // 
            // tbAvgEntries
            // 
            this.tbAvgEntries.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tbAvgEntries.Location = new System.Drawing.Point(113, 161);
            this.tbAvgEntries.Name = "tbAvgEntries";
            this.tbAvgEntries.ReadOnly = true;
            this.tbAvgEntries.Size = new System.Drawing.Size(124, 20);
            this.tbAvgEntries.TabIndex = 17;
            this.tbAvgEntries.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 16;
            // 
            // tbDrumName
            // 
            this.tbDrumName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tbDrumName.Location = new System.Drawing.Point(113, 15);
            this.tbDrumName.Multiline = true;
            this.tbDrumName.Name = "tbDrumName";
            this.tbDrumName.ReadOnly = true;
            this.tbDrumName.Size = new System.Drawing.Size(124, 38);
            this.tbDrumName.TabIndex = 15;
            // 
            // btnPopulateDrum
            // 
            this.btnPopulateDrum.Enabled = false;
            this.btnPopulateDrum.Location = new System.Drawing.Point(72, 187);
            this.btnPopulateDrum.Name = "btnPopulateDrum";
            this.btnPopulateDrum.Size = new System.Drawing.Size(101, 23);
            this.btnPopulateDrum.TabIndex = 1;
            this.btnPopulateDrum.UseVisualStyleBackColor = true;
            this.btnPopulateDrum.Click += new System.EventHandler(this.btnPopulateDrum_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 14;
            // 
            // tbEntries
            // 
            this.tbEntries.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tbEntries.Location = new System.Drawing.Point(113, 135);
            this.tbEntries.Name = "tbEntries";
            this.tbEntries.ReadOnly = true;
            this.tbEntries.Size = new System.Drawing.Size(124, 20);
            this.tbEntries.TabIndex = 11;
            this.tbEntries.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbPlayers
            // 
            this.tbPlayers.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tbPlayers.Location = new System.Drawing.Point(113, 110);
            this.tbPlayers.Name = "tbPlayers";
            this.tbPlayers.ReadOnly = true;
            this.tbPlayers.Size = new System.Drawing.Size(124, 20);
            this.tbPlayers.TabIndex = 10;
            this.tbPlayers.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 13);
            this.label5.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 113);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 13);
            this.label6.TabIndex = 8;
            // 
            // tbLastPopulated
            // 
            this.tbLastPopulated.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tbLastPopulated.Location = new System.Drawing.Point(113, 84);
            this.tbLastPopulated.Name = "tbLastPopulated";
            this.tbLastPopulated.ReadOnly = true;
            this.tbLastPopulated.Size = new System.Drawing.Size(124, 20);
            this.tbLastPopulated.TabIndex = 7;
            this.tbLastPopulated.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbDrumType
            // 
            this.tbDrumType.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tbDrumType.Location = new System.Drawing.Point(113, 59);
            this.tbDrumType.Name = "tbDrumType";
            this.tbDrumType.ReadOnly = true;
            this.tbDrumType.Size = new System.Drawing.Size(124, 20);
            this.tbDrumType.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 4;
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.Controls.Add(this.btnResetDrawing);
            this.groupBox7.Location = new System.Drawing.Point(721, 105);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(244, 51);
            this.groupBox7.TabIndex = 3;
            this.groupBox7.TabStop = false;
            // 
            // btnResetDrawing
            // 
            this.btnResetDrawing.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnResetDrawing.Enabled = false;
            this.btnResetDrawing.Location = new System.Drawing.Point(47, 18);
            this.btnResetDrawing.Name = "btnResetDrawing";
            this.btnResetDrawing.Size = new System.Drawing.Size(150, 23);
            this.btnResetDrawing.TabIndex = 4;
            this.btnResetDrawing.UseVisualStyleBackColor = true;
            this.btnResetDrawing.Click += new System.EventHandler(this.btnResetDrawing_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.btnCompleteDrawing);
            this.groupBox4.Controls.Add(this.btnStopCountdown);
            this.groupBox4.Controls.Add(this.btnDrawAllWinners);
            this.groupBox4.Controls.Add(this.btnStartDrawing);
            this.groupBox4.Controls.Add(this.btnStartCountdown);
            this.groupBox4.Location = new System.Drawing.Point(4, 667);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(711, 40);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            // 
            // btnCompleteDrawing
            // 
            this.btnCompleteDrawing.Enabled = false;
            this.btnCompleteDrawing.Location = new System.Drawing.Point(435, 13);
            this.btnCompleteDrawing.Name = "btnCompleteDrawing";
            this.btnCompleteDrawing.Size = new System.Drawing.Size(101, 23);
            this.btnCompleteDrawing.TabIndex = 3;
            this.btnCompleteDrawing.UseVisualStyleBackColor = true;
            this.btnCompleteDrawing.Click += new System.EventHandler(this.btnCompleteDrawing_Click);
            // 
            // btnStopCountdown
            // 
            this.btnStopCountdown.Enabled = false;
            this.btnStopCountdown.Location = new System.Drawing.Point(221, 13);
            this.btnStopCountdown.Name = "btnStopCountdown";
            this.btnStopCountdown.Size = new System.Drawing.Size(101, 23);
            this.btnStopCountdown.TabIndex = 3;
            this.btnStopCountdown.UseVisualStyleBackColor = true;
            this.btnStopCountdown.Click += new System.EventHandler(this.btnStopCountdown_Click);
            // 
            // btnDrawAllWinners
            // 
            this.btnDrawAllWinners.Enabled = false;
            this.btnDrawAllWinners.Location = new System.Drawing.Point(328, 13);
            this.btnDrawAllWinners.Name = "btnDrawAllWinners";
            this.btnDrawAllWinners.Size = new System.Drawing.Size(101, 23);
            this.btnDrawAllWinners.TabIndex = 2;
            this.btnDrawAllWinners.UseVisualStyleBackColor = true;
            this.btnDrawAllWinners.Click += new System.EventHandler(this.btnDrawAllWinners_Click);
            // 
            // btnStartDrawing
            // 
            this.btnStartDrawing.Enabled = false;
            this.btnStartDrawing.Location = new System.Drawing.Point(114, 13);
            this.btnStartDrawing.Name = "btnStartDrawing";
            this.btnStartDrawing.Size = new System.Drawing.Size(101, 23);
            this.btnStartDrawing.TabIndex = 2;
            this.btnStartDrawing.UseVisualStyleBackColor = true;
            this.btnStartDrawing.Click += new System.EventHandler(this.btnStartDrawing_Click);
            // 
            // btnStartCountdown
            // 
            this.btnStartCountdown.Location = new System.Drawing.Point(7, 13);
            this.btnStartCountdown.Name = "btnStartCountdown";
            this.btnStartCountdown.Size = new System.Drawing.Size(101, 23);
            this.btnStartCountdown.TabIndex = 0;
            this.btnStartCountdown.UseVisualStyleBackColor = true;
            this.btnStartCountdown.Click += new System.EventHandler(this.btnStartCountdown_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.dgvDrawings);
            this.groupBox3.Location = new System.Drawing.Point(4, 72);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(711, 592);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // dgvDrawings
            // 
            this.dgvDrawings.AllowUserToAddRows = false;
            this.dgvDrawings.AllowUserToDeleteRows = false;
            this.dgvDrawings.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvDrawings.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDrawings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDrawings.AutoGenerateColumns = false;
            this.dgvDrawings.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDrawings.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDrawings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column9,
            this.Column3,
            this.Column2,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13,
            this.Column14,
            this.Column15,
            this.Column16,
            this.Column17,
            this.Column18});
            this.dgvDrawings.DataSource = this.bsDrawings;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDrawings.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvDrawings.Location = new System.Drawing.Point(6, 19);
            this.dgvDrawings.Name = "dgvDrawings";
            this.dgvDrawings.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.NullValue = "0";
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDrawings.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvDrawings.RowHeadersWidth = 50;
            this.dgvDrawings.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvDrawings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDrawings.Size = new System.Drawing.Size(699, 567);
            this.dgvDrawings.TabIndex = 0;
            this.dgvDrawings.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvDrawings_MouseDown);
            this.dgvDrawings.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvDrawings_CurrentCellDirtyStateChanged);
            this.dgvDrawings.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvDrawings_RowPostPaint);
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.pnlAnimation);
            this.groupBox5.Controls.Add(this.lblAnimationInUse);
            this.groupBox5.Location = new System.Drawing.Point(721, 490);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(244, 217);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            // 
            // pnlAnimation
            // 
            this.pnlAnimation.Location = new System.Drawing.Point(9, 32);
            this.pnlAnimation.MinimumSize = new System.Drawing.Size(20, 20);
            this.pnlAnimation.Name = "pnlAnimation";
            this.pnlAnimation.ScrollBarsEnabled = false;
            this.pnlAnimation.Size = new System.Drawing.Size(228, 179);
            this.pnlAnimation.TabIndex = 1;
            // 
            // lblAnimationInUse
            // 
            this.lblAnimationInUse.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblAnimationInUse.AutoSize = true;
            this.lblAnimationInUse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnimationInUse.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblAnimationInUse.Location = new System.Drawing.Point(22, 16);
            this.lblAnimationInUse.MinimumSize = new System.Drawing.Size(200, 0);
            this.lblAnimationInUse.Name = "lblAnimationInUse";
            this.lblAnimationInUse.Size = new System.Drawing.Size(200, 13);
            this.lblAnimationInUse.TabIndex = 0;
            this.lblAnimationInUse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Prize";
            this.dataGridViewTextBoxColumn1.HeaderText = "Prize";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 79;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "PrizeValue";
            this.dataGridViewTextBoxColumn2.HeaderText = "Prize Value";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 80;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "WinnerID";
            this.dataGridViewTextBoxColumn3.HeaderText = "Winner ID";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 79;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Winner";
            this.dataGridViewTextBoxColumn4.HeaderText = "Winner";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 80;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "WinnerDOB";
            this.dataGridViewTextBoxColumn5.HeaderText = "Winner DOB";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 79;
            // 
            // dataGridViewTimerColumn1
            // 
            this.dataGridViewTimerColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTimerColumn1.HeaderText = "Time Left";
            this.dataGridViewTimerColumn1.Name = "dataGridViewTimerColumn1";
            this.dataGridViewTimerColumn1.Width = 80;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn6.HeaderText = "Status";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 79;
            // 
            // dataGridViewDisableButtonColumn1
            // 
            this.dataGridViewDisableButtonColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewDisableButtonColumn1.HeaderText = "";
            this.dataGridViewDisableButtonColumn1.Name = "dataGridViewDisableButtonColumn1";
            this.dataGridViewDisableButtonColumn1.UseColumnTextForButtonValue = true;
            this.dataGridViewDisableButtonColumn1.Width = 90;
            // 
            // dataGridViewDisableButtonColumn2
            // 
            this.dataGridViewDisableButtonColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewDisableButtonColumn2.HeaderText = "";
            this.dataGridViewDisableButtonColumn2.Name = "dataGridViewDisableButtonColumn2";
            this.dataGridViewDisableButtonColumn2.UseColumnTextForButtonValue = true;
            this.dataGridViewDisableButtonColumn2.Width = 89;
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.tbDrawing);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Controls.Add(this.tbNextDrawing);
            this.groupBox6.Controls.Add(this.label7);
            this.groupBox6.Location = new System.Drawing.Point(721, 162);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(244, 79);
            this.groupBox6.TabIndex = 4;
            this.groupBox6.TabStop = false;
            // 
            // tDrawTimer
            // 
            this.tDrawTimer.Interval = 3000;
            this.tDrawTimer.Tick += new System.EventHandler(this.tDrawTimer_Tick);
            // 
            // tUpdateStats
            // 
            this.tUpdateStats.Interval = 10000;
            this.tUpdateStats.Tick += new System.EventHandler(this.tUpdateStats_Tick);
            // 
            // pbLogo
            // 
            this.pbLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbLogo.BackgroundImage = global::DrawingClient.Properties.Resources.Logo;
            this.pbLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbLogo.Location = new System.Drawing.Point(721, 8);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(242, 111);
            this.pbLogo.TabIndex = 14;
            this.pbLogo.TabStop = false;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.DataPropertyName = "Prize";
            this.Column1.HeaderText = "Prize";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "PrizeValue";
            dataGridViewCellStyle3.Format = "C2";
            dataGridViewCellStyle3.NullValue = null;
            this.Column9.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column9.HeaderText = "Value";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 41;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "CMSPlayerID";
            this.Column3.HeaderText = "Winner ID";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 65;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.DataPropertyName = "Winner";
            this.Column2.HeaderText = "Winner";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column4
            // 
            dataGridViewCellStyle4.Format = "d";
            dataGridViewCellStyle4.NullValue = null;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column4.HeaderText = "DOB";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 60;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Time Left";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 65;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Status";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 45;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.UseColumnTextForButtonValue = true;
            this.Column7.Width = 60;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.UseColumnTextForButtonValue = true;
            this.Column8.Width = 60;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "Id";
            this.Column10.HeaderText = "DrawingId";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Visible = false;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "TicketNumber";
            this.Column11.HeaderText = "EntryId";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Visible = false;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "PlayerAccountNum";
            this.Column12.HeaderText = "PlayerId";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Visible = false;
            // 
            // Column13
            // 
            this.Column13.DataPropertyName = "CMSPlayerId";
            this.Column13.HeaderText = "PlayerCMS";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Visible = false;
            // 
            // Column14
            // 
            this.Column14.DataPropertyName = "WinnerId";
            this.Column14.HeaderText = "WinnerID";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.Visible = false;
            // 
            // Column15
            // 
            this.Column15.DataPropertyName = "WinnerDOB";
            this.Column15.HeaderText = "WinnerDOB";
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            this.Column15.Visible = false;
            // 
            // Column16
            // 
            this.Column16.DataPropertyName = "TimeOut";
            this.Column16.HeaderText = "DrawnAt";
            this.Column16.Name = "Column16";
            this.Column16.ReadOnly = true;
            this.Column16.Visible = false;
            // 
            // Column17
            // 
            this.Column17.DataPropertyName = "Validated";
            this.Column17.HeaderText = "Validated";
            this.Column17.Name = "Column17";
            this.Column17.ReadOnly = true;
            this.Column17.Visible = false;
            // 
            // Column18
            // 
            this.Column18.DataPropertyName = "TimeStarted";
            this.Column18.HeaderText = "TimeStarted";
            this.Column18.Name = "Column18";
            this.Column18.ReadOnly = true;
            this.Column18.Visible = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(970, 734);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pbLogo);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDrawings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDrawings)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbActivePromotions;
        private System.Windows.Forms.TextBox tbNextDrawing;
        private System.Windows.Forms.TextBox tbDrawing;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbEntries;
        private System.Windows.Forms.TextBox tbPlayers;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbLastPopulated;
        private System.Windows.Forms.TextBox tbDrumType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnResetDrawing;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnCompleteDrawing;
        private System.Windows.Forms.Button btnStopCountdown;
        private System.Windows.Forms.Button btnDrawAllWinners;
        private System.Windows.Forms.Button btnPopulateDrum;
        private System.Windows.Forms.Button btnStartDrawing;
        private System.Windows.Forms.Button btnStartCountdown;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvDrawings;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lblAnimationInUse;
        private System.Windows.Forms.TextBox tbDrumName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutDrawingClientToolStripMenuItem;
        private DataGridViewDisableButtonColumn dataGridViewDisableButtonColumn1;
        private DataGridViewDisableButtonColumn dataGridViewDisableButtonColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private AIS.Windows.Forms.DataGridViewTimerColumn dataGridViewTimerColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.BindingSource bsDrawings;
        private System.Windows.Forms.TextBox tbAvgEntries;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.TextBox tbPromoEnd;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbPromoStart;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem totalPromoUserAdminToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem totalPromoCustomerServiceDesktopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem totalPromoMarketingManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem animationClientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onlineSupportToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.WebBrowser pnlAnimation;
        private System.Windows.Forms.Timer tDrawTimer;
        private System.Windows.Forms.Timer tUpdateStats;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private DataGridViewTimerColumn Column5;
        private System.Windows.Forms.DataGridViewImageColumn Column6;
        private DataGridViewDisableButtonColumn Column7;
        private DataGridViewDisableButtonColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column18;
    }
}