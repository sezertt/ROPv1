namespace ROPv1
{
    partial class GirisEkrani
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GirisEkrani));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.passwordBoxHost = new System.Windows.Forms.Integration.ElementHost();
            this.usernameBoxHost = new System.Windows.Forms.Integration.ElementHost();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.girisButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.keyboardcontrol1 = new KeyboardClassLibrary.Keyboardcontrol();
            this.exitButton = new System.Windows.Forms.Button();
            this.labelGun = new System.Windows.Forms.Label();
            this.labelTarih = new System.Windows.Forms.Label();
            this.labelSaat = new System.Windows.Forms.Label();
            this.timerSaat = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.passwordBoxHost);
            this.groupBox1.Controls.Add(this.usernameBoxHost);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.girisButton);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Window;
            this.groupBox1.Location = new System.Drawing.Point(471, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(360, 262);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Yönetici Girişi";
            // 
            // passwordBoxHost
            // 
            this.passwordBoxHost.Location = new System.Drawing.Point(114, 124);
            this.passwordBoxHost.Name = "passwordBoxHost";
            this.passwordBoxHost.Size = new System.Drawing.Size(220, 80);
            this.passwordBoxHost.TabIndex = 3;
            this.passwordBoxHost.Child = null;
            // 
            // usernameBoxHost
            // 
            this.usernameBoxHost.Location = new System.Drawing.Point(114, 44);
            this.usernameBoxHost.Name = "usernameBoxHost";
            this.usernameBoxHost.Size = new System.Drawing.Size(220, 80);
            this.usernameBoxHost.TabIndex = 2;
            this.usernameBoxHost.Child = null;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBox1.BackgroundImage = global::ROPv1.Properties.Resources.username;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(32, 46);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(60, 74);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBox2.BackgroundImage = global::ROPv1.Properties.Resources.password;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(32, 134);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(60, 60);
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // girisButton
            // 
            this.girisButton.BackColor = System.Drawing.SystemColors.Window;
            this.girisButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.girisButton.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.girisButton.Image = ((System.Drawing.Image)(resources.GetObject("girisButton.Image")));
            this.girisButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.girisButton.Location = new System.Drawing.Point(264, 211);
            this.girisButton.Name = "girisButton";
            this.girisButton.Size = new System.Drawing.Size(90, 45);
            this.girisButton.TabIndex = 4;
            this.girisButton.Text = "Giriş";
            this.girisButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.girisButton.UseVisualStyleBackColor = false;
            this.girisButton.Click += new System.EventHandler(this.girisButtonPressed);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button2.BackColor = System.Drawing.SystemColors.Window;
            this.button2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button2.Location = new System.Drawing.Point(451, 334);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(400, 35);
            this.button2.TabIndex = 5;
            this.button2.Text = "Sipariş Ekranına Geç";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.siparisButtonPressed);
            // 
            // keyboardcontrol1
            // 
            this.keyboardcontrol1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.keyboardcontrol1.ForeColor = System.Drawing.SystemColors.Window;
            this.keyboardcontrol1.KeyboardType = KeyboardClassLibrary.BoW.Standard;
            this.keyboardcontrol1.Location = new System.Drawing.Point(154, 525);
            this.keyboardcontrol1.Name = "keyboardcontrol1";
            this.keyboardcontrol1.Size = new System.Drawing.Size(993, 282);
            this.keyboardcontrol1.TabIndex = 0;
            this.keyboardcontrol1.UserKeyPressed += new KeyboardClassLibrary.KeyboardDelegate(this.keyboardcontrol1_UserKeyPressed);
            // 
            // exitButton
            // 
            this.exitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exitButton.AutoSize = true;
            this.exitButton.BackColor = System.Drawing.SystemColors.Window;
            this.exitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.exitButton.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.exitButton.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.exitButton.Image = global::ROPv1.Properties.Resources.logOut;
            this.exitButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.exitButton.Location = new System.Drawing.Point(848, 12);
            this.exitButton.MaximumSize = new System.Drawing.Size(300, 110);
            this.exitButton.MinimumSize = new System.Drawing.Size(150, 110);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(200, 110);
            this.exitButton.TabIndex = 7;
            this.exitButton.Text = "Kapat";
            this.exitButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButtonPressed);
            // 
            // labelGun
            // 
            this.labelGun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelGun.AutoSize = true;
            this.labelGun.BackColor = System.Drawing.Color.Transparent;
            this.labelGun.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelGun.ForeColor = System.Drawing.SystemColors.Window;
            this.labelGun.Location = new System.Drawing.Point(1117, 90);
            this.labelGun.Name = "labelGun";
            this.labelGun.Size = new System.Drawing.Size(0, 33);
            this.labelGun.TabIndex = 15;
            this.labelGun.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelTarih
            // 
            this.labelTarih.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTarih.AutoSize = true;
            this.labelTarih.BackColor = System.Drawing.Color.Transparent;
            this.labelTarih.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelTarih.ForeColor = System.Drawing.SystemColors.Window;
            this.labelTarih.Location = new System.Drawing.Point(1087, 63);
            this.labelTarih.Name = "labelTarih";
            this.labelTarih.Size = new System.Drawing.Size(0, 33);
            this.labelTarih.TabIndex = 14;
            this.labelTarih.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelSaat
            // 
            this.labelSaat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSaat.AutoSize = true;
            this.labelSaat.BackColor = System.Drawing.Color.Transparent;
            this.labelSaat.Font = new System.Drawing.Font("Calibri", 45F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelSaat.ForeColor = System.Drawing.SystemColors.Window;
            this.labelSaat.Location = new System.Drawing.Point(1053, 2);
            this.labelSaat.Name = "labelSaat";
            this.labelSaat.Size = new System.Drawing.Size(0, 73);
            this.labelSaat.TabIndex = 13;
            this.labelSaat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timerSaat
            // 
            this.timerSaat.Interval = 1000;
            this.timerSaat.Tick += new System.EventHandler(this.timerSaat_Tick);
            // 
            // GirisEkrani
            // 
            this.AcceptButton = this.girisButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CancelButton = this.exitButton;
            this.ClientSize = new System.Drawing.Size(1302, 819);
            this.ControlBox = false;
            this.Controls.Add(this.labelGun);
            this.Controls.Add(this.labelTarih);
            this.Controls.Add(this.labelSaat);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.keyboardcontrol1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GirisEkrani";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GirisEkrani";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.closingGiris);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button girisButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Integration.ElementHost usernameBoxHost;
        private System.Windows.Forms.Integration.ElementHost passwordBoxHost;
        private KeyboardClassLibrary.Keyboardcontrol keyboardcontrol1;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label labelGun;
        private System.Windows.Forms.Label labelTarih;
        private System.Windows.Forms.Label labelSaat;
        private System.Windows.Forms.Timer timerSaat;
    }
}

