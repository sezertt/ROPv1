namespace ROPv1
{
    partial class AdminGirisFormu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminGirisFormu));
            this.splitPanel = new System.Windows.Forms.SplitContainer();
            this.buttonModem = new System.Windows.Forms.Button();
            this.buttonUrunOzellikleri = new System.Windows.Forms.Button();
            this.buttonBilgiAktar = new System.Windows.Forms.Button();
            this.leftPanelView = new System.Windows.Forms.TreeView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.labelSaat = new System.Windows.Forms.Label();
            this.labelTarih = new System.Windows.Forms.Label();
            this.labelGun = new System.Windows.Forms.Label();
            this.timerSaat = new System.Windows.Forms.Timer(this.components);
            this.buttonTerazi = new System.Windows.Forms.Button();
            this.adisyonCheckBox = new System.Windows.Forms.CheckBox();
            this.exitButton = new System.Windows.Forms.Button();
            this.reportCheckBox = new System.Windows.Forms.CheckBox();
            this.ayarCheckBox = new System.Windows.Forms.CheckBox();
            this.stokCheckBox = new System.Windows.Forms.CheckBox();
            this.anketCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel)).BeginInit();
            this.splitPanel.Panel1.SuspendLayout();
            this.splitPanel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitPanel
            // 
            this.splitPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitPanel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitPanel.IsSplitterFixed = true;
            this.splitPanel.Location = new System.Drawing.Point(12, 128);
            this.splitPanel.Name = "splitPanel";
            // 
            // splitPanel.Panel1
            // 
            this.splitPanel.Panel1.BackColor = System.Drawing.SystemColors.Window;
            this.splitPanel.Panel1.Controls.Add(this.buttonTerazi);
            this.splitPanel.Panel1.Controls.Add(this.buttonModem);
            this.splitPanel.Panel1.Controls.Add(this.buttonUrunOzellikleri);
            this.splitPanel.Panel1.Controls.Add(this.buttonBilgiAktar);
            this.splitPanel.Panel1.Controls.Add(this.leftPanelView);
            // 
            // splitPanel.Panel2
            // 
            this.splitPanel.Panel2.BackColor = System.Drawing.SystemColors.Window;
            this.splitPanel.Size = new System.Drawing.Size(1354, 649);
            this.splitPanel.SplitterDistance = 200;
            this.splitPanel.SplitterWidth = 6;
            this.splitPanel.TabIndex = 6;
            // 
            // buttonModem
            // 
            this.buttonModem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonModem.BackColor = System.Drawing.SystemColors.Window;
            this.buttonModem.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonModem.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonModem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonModem.Location = new System.Drawing.Point(3, 518);
            this.buttonModem.Name = "buttonModem";
            this.buttonModem.Size = new System.Drawing.Size(192, 60);
            this.buttonModem.TabIndex = 9;
            this.buttonModem.Text = "Modem Bilgileri";
            this.buttonModem.UseVisualStyleBackColor = false;
            this.buttonModem.Click += new System.EventHandler(this.buttonModem_Click);
            // 
            // buttonUrunOzellikleri
            // 
            this.buttonUrunOzellikleri.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUrunOzellikleri.BackColor = System.Drawing.SystemColors.Window;
            this.buttonUrunOzellikleri.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonUrunOzellikleri.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonUrunOzellikleri.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonUrunOzellikleri.Location = new System.Drawing.Point(3, 584);
            this.buttonUrunOzellikleri.Name = "buttonUrunOzellikleri";
            this.buttonUrunOzellikleri.Size = new System.Drawing.Size(192, 60);
            this.buttonUrunOzellikleri.TabIndex = 8;
            this.buttonUrunOzellikleri.Text = "Ürün Özellikleri";
            this.buttonUrunOzellikleri.UseVisualStyleBackColor = false;
            this.buttonUrunOzellikleri.Click += new System.EventHandler(this.buttonUrunOzellikleri_Click);
            // 
            // buttonBilgiAktar
            // 
            this.buttonBilgiAktar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBilgiAktar.BackColor = System.Drawing.SystemColors.Window;
            this.buttonBilgiAktar.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonBilgiAktar.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonBilgiAktar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonBilgiAktar.Location = new System.Drawing.Point(3, 386);
            this.buttonBilgiAktar.Name = "buttonBilgiAktar";
            this.buttonBilgiAktar.Size = new System.Drawing.Size(192, 60);
            this.buttonBilgiAktar.TabIndex = 7;
            this.buttonBilgiAktar.Text = "Verileri Güncelle";
            this.buttonBilgiAktar.UseVisualStyleBackColor = false;
            this.buttonBilgiAktar.Visible = false;
            this.buttonBilgiAktar.Click += new System.EventHandler(this.buttonBilgiAktar_Click);
            // 
            // leftPanelView
            // 
            this.leftPanelView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.leftPanelView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.leftPanelView.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.leftPanelView.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.leftPanelView.FullRowSelect = true;
            this.leftPanelView.HideSelection = false;
            this.leftPanelView.HotTracking = true;
            this.leftPanelView.Indent = 8;
            this.leftPanelView.ItemHeight = 35;
            this.leftPanelView.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.leftPanelView.Location = new System.Drawing.Point(4, 4);
            this.leftPanelView.Name = "leftPanelView";
            this.leftPanelView.ShowLines = false;
            this.leftPanelView.ShowRootLines = false;
            this.leftPanelView.Size = new System.Drawing.Size(192, 640);
            this.leftPanelView.TabIndex = 1;
            this.leftPanelView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.changeSettingsScreen);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.exitButton);
            this.flowLayoutPanel1.Controls.Add(this.reportCheckBox);
            this.flowLayoutPanel1.Controls.Add(this.ayarCheckBox);
            this.flowLayoutPanel1.Controls.Add(this.stokCheckBox);
            this.flowLayoutPanel1.Controls.Add(this.anketCheckBox);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(439, 7);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(930, 116);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // labelSaat
            // 
            this.labelSaat.AutoSize = true;
            this.labelSaat.BackColor = System.Drawing.Color.Transparent;
            this.labelSaat.Font = new System.Drawing.Font("Calibri", 45F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelSaat.ForeColor = System.Drawing.SystemColors.Window;
            this.labelSaat.Location = new System.Drawing.Point(0, 0);
            this.labelSaat.Name = "labelSaat";
            this.labelSaat.Size = new System.Drawing.Size(246, 73);
            this.labelSaat.TabIndex = 7;
            this.labelSaat.Text = "22:55:30";
            this.labelSaat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTarih
            // 
            this.labelTarih.BackColor = System.Drawing.Color.Transparent;
            this.labelTarih.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelTarih.ForeColor = System.Drawing.SystemColors.Window;
            this.labelTarih.Location = new System.Drawing.Point(12, 61);
            this.labelTarih.Name = "labelTarih";
            this.labelTarih.Size = new System.Drawing.Size(218, 33);
            this.labelTarih.TabIndex = 8;
            this.labelTarih.Text = "10 Şubat 2014 ";
            this.labelTarih.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelGun
            // 
            this.labelGun.BackColor = System.Drawing.Color.Transparent;
            this.labelGun.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelGun.ForeColor = System.Drawing.SystemColors.Window;
            this.labelGun.Location = new System.Drawing.Point(12, 91);
            this.labelGun.Name = "labelGun";
            this.labelGun.Size = new System.Drawing.Size(218, 33);
            this.labelGun.TabIndex = 9;
            this.labelGun.Text = "Pazartesi";
            this.labelGun.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerSaat
            // 
            this.timerSaat.Interval = 1000;
            this.timerSaat.Tick += new System.EventHandler(this.timerSaat_Tick);
            // 
            // buttonTerazi
            // 
            this.buttonTerazi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTerazi.BackColor = System.Drawing.SystemColors.Window;
            this.buttonTerazi.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonTerazi.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonTerazi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonTerazi.Location = new System.Drawing.Point(3, 452);
            this.buttonTerazi.Name = "buttonTerazi";
            this.buttonTerazi.Size = new System.Drawing.Size(192, 60);
            this.buttonTerazi.TabIndex = 10;
            this.buttonTerazi.Text = "Terazi Barkodu";
            this.buttonTerazi.UseVisualStyleBackColor = false;
            this.buttonTerazi.Click += new System.EventHandler(this.buttonTerazi_Click);
            // 
            // adisyonCheckBox
            // 
            this.adisyonCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.adisyonCheckBox.BackColor = System.Drawing.SystemColors.Window;
            this.adisyonCheckBox.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.adisyonCheckBox.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.adisyonCheckBox.Image = global::ROPv1.Properties.Resources.adisyon;
            this.adisyonCheckBox.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.adisyonCheckBox.Location = new System.Drawing.Point(236, 10);
            this.adisyonCheckBox.MaximumSize = new System.Drawing.Size(300, 110);
            this.adisyonCheckBox.MinimumSize = new System.Drawing.Size(180, 110);
            this.adisyonCheckBox.Name = "adisyonCheckBox";
            this.adisyonCheckBox.Size = new System.Drawing.Size(180, 110);
            this.adisyonCheckBox.TabIndex = 6;
            this.adisyonCheckBox.Tag = "4";
            this.adisyonCheckBox.Text = "Adisyonlar";
            this.adisyonCheckBox.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.adisyonCheckBox.UseVisualStyleBackColor = false;
            this.adisyonCheckBox.Click += new System.EventHandler(this.adisyonCheckBox_Click);
            // 
            // exitButton
            // 
            this.exitButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.exitButton.AutoSize = true;
            this.exitButton.BackColor = System.Drawing.SystemColors.Window;
            this.exitButton.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.exitButton.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.exitButton.Image = global::ROPv1.Properties.Resources.logOut;
            this.exitButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.exitButton.Location = new System.Drawing.Point(747, 3);
            this.exitButton.MaximumSize = new System.Drawing.Size(300, 110);
            this.exitButton.MinimumSize = new System.Drawing.Size(180, 110);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(180, 110);
            this.exitButton.TabIndex = 5;
            this.exitButton.Text = "Çıkış";
            this.exitButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitPressed);
            // 
            // reportCheckBox
            // 
            this.reportCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.reportCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.reportCheckBox.BackColor = System.Drawing.SystemColors.Window;
            this.reportCheckBox.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.reportCheckBox.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.reportCheckBox.Image = global::ROPv1.Properties.Resources.reportscolor;
            this.reportCheckBox.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.reportCheckBox.Location = new System.Drawing.Point(561, 3);
            this.reportCheckBox.MaximumSize = new System.Drawing.Size(300, 110);
            this.reportCheckBox.MinimumSize = new System.Drawing.Size(180, 110);
            this.reportCheckBox.Name = "reportCheckBox";
            this.reportCheckBox.Size = new System.Drawing.Size(180, 110);
            this.reportCheckBox.TabIndex = 2;
            this.reportCheckBox.Tag = "1";
            this.reportCheckBox.Text = "Raporlar";
            this.reportCheckBox.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.reportCheckBox.UseVisualStyleBackColor = false;
            this.reportCheckBox.CheckedChanged += new System.EventHandler(this.saleCheckChanged);
            // 
            // ayarCheckBox
            // 
            this.ayarCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ayarCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.ayarCheckBox.BackColor = System.Drawing.SystemColors.Window;
            this.ayarCheckBox.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ayarCheckBox.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.ayarCheckBox.Image = global::ROPv1.Properties.Resources.settingscolor;
            this.ayarCheckBox.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ayarCheckBox.Location = new System.Drawing.Point(375, 3);
            this.ayarCheckBox.MaximumSize = new System.Drawing.Size(300, 110);
            this.ayarCheckBox.MinimumSize = new System.Drawing.Size(180, 110);
            this.ayarCheckBox.Name = "ayarCheckBox";
            this.ayarCheckBox.Size = new System.Drawing.Size(180, 110);
            this.ayarCheckBox.TabIndex = 4;
            this.ayarCheckBox.Tag = "3";
            this.ayarCheckBox.Text = "Ayarlar";
            this.ayarCheckBox.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ayarCheckBox.UseVisualStyleBackColor = false;
            this.ayarCheckBox.CheckedChanged += new System.EventHandler(this.saleCheckChanged);
            // 
            // stokCheckBox
            // 
            this.stokCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.stokCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.stokCheckBox.BackColor = System.Drawing.SystemColors.Window;
            this.stokCheckBox.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.stokCheckBox.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.stokCheckBox.Image = global::ROPv1.Properties.Resources.stockcolor;
            this.stokCheckBox.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.stokCheckBox.Location = new System.Drawing.Point(189, 3);
            this.stokCheckBox.MaximumSize = new System.Drawing.Size(300, 110);
            this.stokCheckBox.MinimumSize = new System.Drawing.Size(180, 110);
            this.stokCheckBox.Name = "stokCheckBox";
            this.stokCheckBox.Size = new System.Drawing.Size(180, 110);
            this.stokCheckBox.TabIndex = 1;
            this.stokCheckBox.Tag = "2";
            this.stokCheckBox.Text = "Stoklar";
            this.stokCheckBox.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.stokCheckBox.UseVisualStyleBackColor = false;
            this.stokCheckBox.CheckedChanged += new System.EventHandler(this.saleCheckChanged);
            // 
            // anketCheckBox
            // 
            this.anketCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.anketCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.anketCheckBox.BackColor = System.Drawing.SystemColors.Window;
            this.anketCheckBox.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.anketCheckBox.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.anketCheckBox.Image = global::ROPv1.Properties.Resources.anket;
            this.anketCheckBox.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.anketCheckBox.Location = new System.Drawing.Point(3, 3);
            this.anketCheckBox.MaximumSize = new System.Drawing.Size(300, 110);
            this.anketCheckBox.MinimumSize = new System.Drawing.Size(180, 110);
            this.anketCheckBox.Name = "anketCheckBox";
            this.anketCheckBox.Size = new System.Drawing.Size(180, 110);
            this.anketCheckBox.TabIndex = 3;
            this.anketCheckBox.Tag = "4";
            this.anketCheckBox.Text = "Anket";
            this.anketCheckBox.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.anketCheckBox.UseVisualStyleBackColor = false;
            this.anketCheckBox.CheckedChanged += new System.EventHandler(this.saleCheckChanged);
            // 
            // AdminGirisFormu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1378, 788);
            this.ControlBox = false;
            this.Controls.Add(this.labelTarih);
            this.Controls.Add(this.adisyonCheckBox);
            this.Controls.Add(this.labelGun);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.splitPanel);
            this.Controls.Add(this.labelSaat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdminGirisFormu";
            this.ShowInTaskbar = false;
            this.Text = "AdminGirisFormu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AdminGirisFormu_FormClosing);
            this.Load += new System.EventHandler(this.AdminGirisFormu_Load);
            this.splitPanel.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel)).EndInit();
            this.splitPanel.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitPanel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.CheckBox reportCheckBox;
        private System.Windows.Forms.CheckBox stokCheckBox;
        private System.Windows.Forms.CheckBox adisyonCheckBox;
        private System.Windows.Forms.CheckBox anketCheckBox;
        private System.Windows.Forms.TreeView leftPanelView;
        private System.Windows.Forms.Label labelSaat;
        private System.Windows.Forms.Label labelTarih;
        private System.Windows.Forms.Label labelGun;
        private System.Windows.Forms.Timer timerSaat;
        private System.Windows.Forms.CheckBox ayarCheckBox;
        private System.Windows.Forms.Button buttonUrunOzellikleri;
        private System.Windows.Forms.Button buttonModem;
        public System.Windows.Forms.Button buttonBilgiAktar;
        private System.Windows.Forms.Button buttonTerazi;

    }
}