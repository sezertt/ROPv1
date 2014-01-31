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
            this.exitButton = new System.Windows.Forms.Button();
            this.dayButton = new System.Windows.Forms.Button();
            this.adminButton = new System.Windows.Forms.Button();
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
            this.exitButton.Location = new System.Drawing.Point(1136, 12);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(275, 120);
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
            this.dayButton.Location = new System.Drawing.Point(574, 12);
            this.dayButton.Name = "dayButton";
            this.dayButton.Size = new System.Drawing.Size(275, 120);
            this.dayButton.TabIndex = 5;
            this.dayButton.Text = "Gün İşlemleri";
            this.dayButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.dayButton.UseVisualStyleBackColor = false;
            // 
            // adminButton
            // 
            this.adminButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.adminButton.BackColor = System.Drawing.SystemColors.Window;
            this.adminButton.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.adminButton.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.adminButton.Image = global::ROPv1.Properties.Resources.username;
            this.adminButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.adminButton.Location = new System.Drawing.Point(855, 12);
            this.adminButton.Name = "adminButton";
            this.adminButton.Size = new System.Drawing.Size(275, 120);
            this.adminButton.TabIndex = 6;
            this.adminButton.Text = "Yönetici İşlemleri";
            this.adminButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.adminButton.UseVisualStyleBackColor = false;
            this.adminButton.Click += new System.EventHandler(this.adminButton_Click);
            // 
            // SiparisFormu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1423, 819);
            this.Controls.Add(this.adminButton);
            this.Controls.Add(this.dayButton);
            this.Controls.Add(this.exitButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SiparisFormu";
            this.Text = "SiparisFormu";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CloseApp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button dayButton;
        private System.Windows.Forms.Button adminButton;
    }
}