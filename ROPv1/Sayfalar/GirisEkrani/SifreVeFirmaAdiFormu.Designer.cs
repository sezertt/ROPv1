namespace ROPv1
{
    partial class SifreVeFirmaAdiFormu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SifreVeFirmaAdiFormu));
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelUrun1 = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.keyboardcontrol1 = new KeyboardClassLibrary.Keyboardcontrol();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.BackColor = System.Drawing.SystemColors.Window;
            this.buttonOK.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonOK.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonOK.Image = global::ROPv1.Properties.Resources.icon;
            this.buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOK.Location = new System.Drawing.Point(858, 300);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(121, 50);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "Tamam";
            this.buttonOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonOK.UseVisualStyleBackColor = false;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // labelUrun1
            // 
            this.labelUrun1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelUrun1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelUrun1.ForeColor = System.Drawing.SystemColors.Window;
            this.labelUrun1.Location = new System.Drawing.Point(221, 7);
            this.labelUrun1.Name = "labelUrun1";
            this.labelUrun1.Size = new System.Drawing.Size(572, 24);
            this.labelUrun1.TabIndex = 15;
            this.labelUrun1.Tag = "1";
            this.labelUrun1.Text = "Firma Adı";
            this.labelUrun1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxIP
            // 
            this.textBoxIP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxIP.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.textBoxIP.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBoxIP.Location = new System.Drawing.Point(300, 34);
            this.textBoxIP.MaxLength = 50;
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(400, 32);
            this.textBoxIP.TabIndex = 83;
            this.textBoxIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // keyboardcontrol1
            // 
            this.keyboardcontrol1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.keyboardcontrol1.ForeColor = System.Drawing.SystemColors.Window;
            this.keyboardcontrol1.KeyboardType = KeyboardClassLibrary.BoW.Standard;
            this.keyboardcontrol1.Location = new System.Drawing.Point(0, 71);
            this.keyboardcontrol1.Name = "keyboardcontrol1";
            this.keyboardcontrol1.Size = new System.Drawing.Size(984, 282);
            this.keyboardcontrol1.TabIndex = 85;
            this.keyboardcontrol1.UserKeyPressed += new KeyboardClassLibrary.KeyboardDelegate(this.keyboardcontrol1_UserKeyPressed);
            // 
            // SifreVeFirmaAdiFormu
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(984, 353);
            this.ControlBox = false;
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.labelUrun1);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.keyboardcontrol1);
            this.ForeColor = System.Drawing.SystemColors.Window;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1000, 369);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1000, 369);
            this.Name = "SifreVeFirmaAdiFormu";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SifreVeFirmaAdiFormu_FormClosing);
            this.Load += new System.EventHandler(this.FirmaAdiFormu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelUrun1;
        private System.Windows.Forms.TextBox textBoxIP;
        private KeyboardClassLibrary.Keyboardcontrol keyboardcontrol1;
    }
}