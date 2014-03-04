namespace ROPv1
{
    partial class SiparisMasaFormu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SiparisMasaFormu));
            this.labelGun = new System.Windows.Forms.Label();
            this.labelTarih = new System.Windows.Forms.Label();
            this.labelSaat = new System.Windows.Forms.Label();
            this.timerSaat = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.tablePanel = new System.Windows.Forms.TableLayoutPanel();
            this.dayButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.buttonAdisyon = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelGun
            // 
            this.labelGun.AutoSize = true;
            this.labelGun.BackColor = System.Drawing.Color.Transparent;
            this.labelGun.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelGun.ForeColor = System.Drawing.SystemColors.Window;
            this.labelGun.Location = new System.Drawing.Point(64, 88);
            this.labelGun.Name = "labelGun";
            this.labelGun.Size = new System.Drawing.Size(116, 33);
            this.labelGun.TabIndex = 12;
            this.labelGun.Text = "Pazartesi";
            this.labelGun.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelTarih
            // 
            this.labelTarih.AutoSize = true;
            this.labelTarih.BackColor = System.Drawing.Color.Transparent;
            this.labelTarih.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelTarih.ForeColor = System.Drawing.SystemColors.Window;
            this.labelTarih.Location = new System.Drawing.Point(34, 61);
            this.labelTarih.Name = "labelTarih";
            this.labelTarih.Size = new System.Drawing.Size(180, 33);
            this.labelTarih.TabIndex = 11;
            this.labelTarih.Text = "10 Şubat 2014 ";
            this.labelTarih.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.labelSaat.TabIndex = 10;
            this.labelSaat.Text = "22:55:30";
            this.labelSaat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timerSaat
            // 
            this.timerSaat.Interval = 1000;
            this.timerSaat.Tick += new System.EventHandler(this.timerSaat_Tick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(13, 126);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1287, 50);
            this.panel1.TabIndex = 14;
            this.panel1.SizeChanged += new System.EventHandler(this.myPannel_SizeChanged);
            // 
            // tablePanel
            // 
            this.tablePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tablePanel.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel.ColumnCount = 7;
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tablePanel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tablePanel.Location = new System.Drawing.Point(11, 177);
            this.tablePanel.Name = "tablePanel";
            this.tablePanel.RowCount = 6;
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tablePanel.Size = new System.Drawing.Size(1291, 636);
            this.tablePanel.TabIndex = 24;
            // 
            // dayButton
            // 
            this.dayButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dayButton.BackColor = System.Drawing.SystemColors.Window;
            this.dayButton.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dayButton.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dayButton.Image = global::ROPv1.Properties.Resources.dayOff;
            this.dayButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.dayButton.Location = new System.Drawing.Point(855, 12);
            this.dayButton.Name = "dayButton";
            this.dayButton.Size = new System.Drawing.Size(220, 110);
            this.dayButton.TabIndex = 5;
            this.dayButton.Text = "Gün İşlemleri";
            this.dayButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.dayButton.UseVisualStyleBackColor = false;
            this.dayButton.Click += new System.EventHandler(this.buttonGunIslemiPressed);
            // 
            // exitButton
            // 
            this.exitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exitButton.BackColor = System.Drawing.SystemColors.Window;
            this.exitButton.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.exitButton.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.exitButton.Image = global::ROPv1.Properties.Resources.logOut;
            this.exitButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.exitButton.Location = new System.Drawing.Point(1081, 12);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(220, 110);
            this.exitButton.TabIndex = 4;
            this.exitButton.Text = "Çıkış";
            this.exitButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitPressed);
            // 
            // buttonAdisyon
            // 
            this.buttonAdisyon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdisyon.BackColor = System.Drawing.SystemColors.Window;
            this.buttonAdisyon.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonAdisyon.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonAdisyon.Image = global::ROPv1.Properties.Resources.adisyon;
            this.buttonAdisyon.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonAdisyon.Location = new System.Drawing.Point(629, 11);
            this.buttonAdisyon.Name = "buttonAdisyon";
            this.buttonAdisyon.Size = new System.Drawing.Size(220, 110);
            this.buttonAdisyon.TabIndex = 25;
            this.buttonAdisyon.Text = "Adisyonlar";
            this.buttonAdisyon.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonAdisyon.UseVisualStyleBackColor = false;
            this.buttonAdisyon.Click += new System.EventHandler(this.buttonAdisyon_Click);
            // 
            // SiparisMasaFormu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1313, 819);
            this.Controls.Add(this.buttonAdisyon);
            this.Controls.Add(this.tablePanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelGun);
            this.Controls.Add(this.labelTarih);
            this.Controls.Add(this.labelSaat);
            this.Controls.Add(this.dayButton);
            this.Controls.Add(this.exitButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SiparisMasaFormu";
            this.Text = "SiparisFormu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CloseApp);
            this.Load += new System.EventHandler(this.SiparisMasaFormu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button dayButton;
        private System.Windows.Forms.Label labelGun;
        private System.Windows.Forms.Label labelTarih;
        private System.Windows.Forms.Label labelSaat;
        private System.Windows.Forms.Timer timerSaat;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tablePanel;
        private System.Windows.Forms.Button buttonAdisyon;
    }
}