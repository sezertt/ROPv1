namespace ROPv1
{
    partial class AdisyonGoruntuleme
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdisyonGoruntuleme));
            this.labelGun = new System.Windows.Forms.Label();
            this.labelTarih = new System.Windows.Forms.Label();
            this.labelSaat = new System.Windows.Forms.Label();
            this.timerSaat = new System.Windows.Forms.Timer(this.components);
            this.exitButton = new System.Windows.Forms.Button();
            this.buttonAdisyonlariGetir = new System.Windows.Forms.Button();
            this.buttonArttir = new System.Windows.Forms.Button();
            this.buttonAzalt = new System.Windows.Forms.Button();
            this.textboxAdisyonID = new System.Windows.Forms.TextBox();
            this.dateBaslangic = new System.Windows.Forms.DateTimePicker();
            this.dateBitis = new System.Windows.Forms.DateTimePicker();
            this.comboAdisyonAyar = new System.Windows.Forms.ComboBox();
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
            // exitButton
            // 
            this.exitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exitButton.BackColor = System.Drawing.SystemColors.Window;
            this.exitButton.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.exitButton.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.exitButton.Image = global::ROPv1.Properties.Resources.logOut;
            this.exitButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.exitButton.Location = new System.Drawing.Point(1134, 12);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(220, 110);
            this.exitButton.TabIndex = 4;
            this.exitButton.Text = "Çıkış";
            this.exitButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitPressed);
            // 
            // buttonAdisyonlariGetir
            // 
            this.buttonAdisyonlariGetir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdisyonlariGetir.BackColor = System.Drawing.SystemColors.Window;
            this.buttonAdisyonlariGetir.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonAdisyonlariGetir.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonAdisyonlariGetir.Image = global::ROPv1.Properties.Resources.checkmark;
            this.buttonAdisyonlariGetir.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonAdisyonlariGetir.Location = new System.Drawing.Point(908, 12);
            this.buttonAdisyonlariGetir.Name = "buttonAdisyonlariGetir";
            this.buttonAdisyonlariGetir.Size = new System.Drawing.Size(220, 110);
            this.buttonAdisyonlariGetir.TabIndex = 15;
            this.buttonAdisyonlariGetir.Text = "Adisyon Bul";
            this.buttonAdisyonlariGetir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonAdisyonlariGetir.UseVisualStyleBackColor = false;
            this.buttonAdisyonlariGetir.Click += new System.EventHandler(this.buttonAdisyonlariGetir_Click);
            // 
            // buttonArttir
            // 
            this.buttonArttir.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonArttir.BackColor = System.Drawing.SystemColors.Window;
            this.buttonArttir.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonArttir.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonArttir.Image = global::ROPv1.Properties.Resources.upBig;
            this.buttonArttir.Location = new System.Drawing.Point(656, 12);
            this.buttonArttir.Name = "buttonArttir";
            this.buttonArttir.Size = new System.Drawing.Size(120, 110);
            this.buttonArttir.TabIndex = 37;
            this.buttonArttir.TabStop = false;
            this.buttonArttir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonArttir.UseVisualStyleBackColor = false;
            this.buttonArttir.Click += new System.EventHandler(this.buttonArttir_Click);
            // 
            // buttonAzalt
            // 
            this.buttonAzalt.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonAzalt.BackColor = System.Drawing.SystemColors.Window;
            this.buttonAzalt.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonAzalt.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonAzalt.Image = global::ROPv1.Properties.Resources.downBig;
            this.buttonAzalt.Location = new System.Drawing.Point(782, 12);
            this.buttonAzalt.Name = "buttonAzalt";
            this.buttonAzalt.Size = new System.Drawing.Size(120, 110);
            this.buttonAzalt.TabIndex = 38;
            this.buttonAzalt.TabStop = false;
            this.buttonAzalt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAzalt.UseVisualStyleBackColor = false;
            this.buttonAzalt.Click += new System.EventHandler(this.buttonAzalt_Click);
            // 
            // textboxAdisyonID
            // 
            this.textboxAdisyonID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textboxAdisyonID.Font = new System.Drawing.Font("Calibri", 16F);
            this.textboxAdisyonID.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textboxAdisyonID.Location = new System.Drawing.Point(495, 13);
            this.textboxAdisyonID.MaxLength = 10;
            this.textboxAdisyonID.Name = "textboxAdisyonID";
            this.textboxAdisyonID.Size = new System.Drawing.Size(154, 34);
            this.textboxAdisyonID.TabIndex = 40;
            // 
            // dateBaslangic
            // 
            this.dateBaslangic.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dateBaslangic.CalendarFont = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dateBaslangic.CalendarForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dateBaslangic.CalendarMonthBackground = System.Drawing.SystemColors.ActiveCaption;
            this.dateBaslangic.CalendarTitleForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dateBaslangic.CalendarTrailingForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dateBaslangic.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dateBaslangic.Location = new System.Drawing.Point(247, 51);
            this.dateBaslangic.Name = "dateBaslangic";
            this.dateBaslangic.Size = new System.Drawing.Size(402, 33);
            this.dateBaslangic.TabIndex = 14;
            this.dateBaslangic.Enter += new System.EventHandler(this.dateBaslangic_Enter);
            // 
            // dateBitis
            // 
            this.dateBitis.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dateBitis.CalendarFont = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dateBitis.CalendarForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dateBitis.CalendarMonthBackground = System.Drawing.SystemColors.ActiveCaption;
            this.dateBitis.CalendarTitleForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dateBitis.CalendarTrailingForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dateBitis.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dateBitis.Location = new System.Drawing.Point(247, 88);
            this.dateBitis.Name = "dateBitis";
            this.dateBitis.Size = new System.Drawing.Size(402, 33);
            this.dateBitis.TabIndex = 16;
            this.dateBitis.ValueChanged += new System.EventHandler(this.dateBitis_ValueChanged);
            this.dateBitis.Enter += new System.EventHandler(this.dateBitis_Enter);
            // 
            // comboAdisyonAyar
            // 
            this.comboAdisyonAyar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboAdisyonAyar.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboAdisyonAyar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.comboAdisyonAyar.FormattingEnabled = true;
            this.comboAdisyonAyar.Items.AddRange(new object[] {
            "Açık Adisyonlar",
            "Tüm Adisyonlar",
            "Adisyon Id",
            "Masa Adı",
            "Departman Adı "});
            this.comboAdisyonAyar.Location = new System.Drawing.Point(247, 12);
            this.comboAdisyonAyar.Name = "comboAdisyonAyar";
            this.comboAdisyonAyar.Size = new System.Drawing.Size(242, 34);
            this.comboAdisyonAyar.TabIndex = 41;
            this.comboAdisyonAyar.TextChanged += new System.EventHandler(this.comboAdisyonAyar_TextChanged);
            this.comboAdisyonAyar.Click += new System.EventHandler(this.comboAdisyonAyar_Click);
            this.comboAdisyonAyar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboAdisyonAyar_KeyPress);
            this.comboAdisyonAyar.Leave += new System.EventHandler(this.comboAdisyonAyar_Leave);
            // 
            // AdisyonGoruntuleme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1366, 819);
            this.ControlBox = false;
            this.Controls.Add(this.comboAdisyonAyar);
            this.Controls.Add(this.textboxAdisyonID);
            this.Controls.Add(this.buttonAzalt);
            this.Controls.Add(this.buttonArttir);
            this.Controls.Add(this.dateBitis);
            this.Controls.Add(this.buttonAdisyonlariGetir);
            this.Controls.Add(this.dateBaslangic);
            this.Controls.Add(this.labelGun);
            this.Controls.Add(this.labelTarih);
            this.Controls.Add(this.labelSaat);
            this.Controls.Add(this.exitButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdisyonGoruntuleme";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "SiparisFormu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AdisyonGoruntuleme_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label labelGun;
        private System.Windows.Forms.Label labelTarih;
        private System.Windows.Forms.Label labelSaat;
        private System.Windows.Forms.Timer timerSaat;
        private System.Windows.Forms.Button buttonAdisyonlariGetir;
        private System.Windows.Forms.Button buttonArttir;
        private System.Windows.Forms.Button buttonAzalt;
        private System.Windows.Forms.TextBox textboxAdisyonID;
        private System.Windows.Forms.DateTimePicker dateBaslangic;
        private System.Windows.Forms.DateTimePicker dateBitis;
        private System.Windows.Forms.ComboBox comboAdisyonAyar;
    }
}