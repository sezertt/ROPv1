namespace ROPv1
{
    partial class SiparisFormu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SiparisFormu));
            this.exitButton = new System.Windows.Forms.Button();
            this.dayButton = new System.Windows.Forms.Button();
            this.labelGun = new System.Windows.Forms.Label();
            this.labelTarih = new System.Windows.Forms.Label();
            this.labelSaat = new System.Windows.Forms.Label();
            this.timerSaat = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // exitButton
            // 
            this.exitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exitButton.BackColor = System.Drawing.SystemColors.Window;
            this.exitButton.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.exitButton.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.exitButton.Image = global::ROPv1.Properties.Resources.logOut;
            this.exitButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.exitButton.Location = new System.Drawing.Point(845, 11);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(220, 110);
            this.exitButton.TabIndex = 4;
            this.exitButton.Text = "Çıkış";
            this.exitButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitPressed);
            // 
            // dayButton
            // 
            this.dayButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dayButton.BackColor = System.Drawing.SystemColors.Window;
            this.dayButton.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dayButton.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dayButton.Image = global::ROPv1.Properties.Resources.dayOff;
            this.dayButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.dayButton.Location = new System.Drawing.Point(619, 11);
            this.dayButton.Name = "dayButton";
            this.dayButton.Size = new System.Drawing.Size(220, 110);
            this.dayButton.TabIndex = 5;
            this.dayButton.Text = "Gün İşlemleri";
            this.dayButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.dayButton.UseVisualStyleBackColor = false;
            this.dayButton.Click += new System.EventHandler(this.buttonGunIslemiPressed);
            // 
            // labelGun
            // 
            this.labelGun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelGun.AutoSize = true;
            this.labelGun.BackColor = System.Drawing.Color.Transparent;
            this.labelGun.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelGun.ForeColor = System.Drawing.SystemColors.Window;
            this.labelGun.Location = new System.Drawing.Point(1128, 90);
            this.labelGun.Name = "labelGun";
            this.labelGun.Size = new System.Drawing.Size(116, 33);
            this.labelGun.TabIndex = 12;
            this.labelGun.Text = "Pazartesi";
            this.labelGun.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelTarih
            // 
            this.labelTarih.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTarih.AutoSize = true;
            this.labelTarih.BackColor = System.Drawing.Color.Transparent;
            this.labelTarih.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelTarih.ForeColor = System.Drawing.SystemColors.Window;
            this.labelTarih.Location = new System.Drawing.Point(1098, 63);
            this.labelTarih.Name = "labelTarih";
            this.labelTarih.Size = new System.Drawing.Size(180, 33);
            this.labelTarih.TabIndex = 11;
            this.labelTarih.Text = "10 Şubat 2014 ";
            this.labelTarih.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelSaat
            // 
            this.labelSaat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSaat.AutoSize = true;
            this.labelSaat.BackColor = System.Drawing.Color.Transparent;
            this.labelSaat.Font = new System.Drawing.Font("Calibri", 45F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelSaat.ForeColor = System.Drawing.SystemColors.Window;
            this.labelSaat.Location = new System.Drawing.Point(1064, 2);
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
            // SiparisFormu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1313, 819);
            this.Controls.Add(this.labelGun);
            this.Controls.Add(this.labelTarih);
            this.Controls.Add(this.labelSaat);
            this.Controls.Add(this.dayButton);
            this.Controls.Add(this.exitButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SiparisFormu";
            this.Text = "SiparisFormu";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CloseApp);
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
    }
}