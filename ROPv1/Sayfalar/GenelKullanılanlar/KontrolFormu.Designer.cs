namespace ROPv1
{
    partial class KontrolFormu
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
            this.labelAciklama = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonTamam = new System.Windows.Forms.Button();
            this.buttonNO = new System.Windows.Forms.Button();
            this.buttonYES = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelAciklama
            // 
            this.labelAciklama.BackColor = System.Drawing.Color.Transparent;
            this.labelAciklama.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelAciklama.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelAciklama.ForeColor = System.Drawing.SystemColors.Window;
            this.labelAciklama.Location = new System.Drawing.Point(0, 0);
            this.labelAciklama.Name = "labelAciklama";
            this.labelAciklama.Size = new System.Drawing.Size(450, 110);
            this.labelAciklama.TabIndex = 31;
            this.labelAciklama.Text = "Text";
            this.labelAciklama.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelAciklama);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(450, 110);
            this.panel1.TabIndex = 32;
            // 
            // buttonTamam
            // 
            this.buttonTamam.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonTamam.BackColor = System.Drawing.SystemColors.Window;
            this.buttonTamam.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonTamam.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonTamam.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonTamam.Image = global::ROPv1.Properties.Resources.icon;
            this.buttonTamam.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonTamam.Location = new System.Drawing.Point(174, 132);
            this.buttonTamam.Name = "buttonTamam";
            this.buttonTamam.Size = new System.Drawing.Size(125, 50);
            this.buttonTamam.TabIndex = 33;
            this.buttonTamam.Text = "Tamam";
            this.buttonTamam.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonTamam.UseVisualStyleBackColor = false;
            this.buttonTamam.Visible = false;
            this.buttonTamam.Click += new System.EventHandler(this.buttonTamam_Click);
            // 
            // buttonNO
            // 
            this.buttonNO.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonNO.BackColor = System.Drawing.SystemColors.Window;
            this.buttonNO.DialogResult = System.Windows.Forms.DialogResult.No;
            this.buttonNO.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonNO.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonNO.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonNO.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonNO.Location = new System.Drawing.Point(37, 132);
            this.buttonNO.Name = "buttonNO";
            this.buttonNO.Size = new System.Drawing.Size(125, 50);
            this.buttonNO.TabIndex = 28;
            this.buttonNO.Text = "Hayır   ";
            this.buttonNO.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonNO.UseVisualStyleBackColor = false;
            this.buttonNO.Click += new System.EventHandler(this.buttonTamam_Click);
            // 
            // buttonYES
            // 
            this.buttonYES.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonYES.BackColor = System.Drawing.SystemColors.Window;
            this.buttonYES.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.buttonYES.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonYES.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonYES.Image = global::ROPv1.Properties.Resources.icon;
            this.buttonYES.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonYES.Location = new System.Drawing.Point(312, 132);
            this.buttonYES.Name = "buttonYES";
            this.buttonYES.Size = new System.Drawing.Size(125, 50);
            this.buttonYES.TabIndex = 27;
            this.buttonYES.Text = "Evet   ";
            this.buttonYES.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonYES.UseVisualStyleBackColor = false;
            this.buttonYES.Click += new System.EventHandler(this.buttonTamam_Click);
            // 
            // KontrolFormu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(474, 189);
            this.ControlBox = false;
            this.Controls.Add(this.buttonTamam);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonNO);
            this.Controls.Add(this.buttonYES);
            this.ForeColor = System.Drawing.SystemColors.Window;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(490, 205);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(490, 205);
            this.Name = "KontrolFormu";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KontrolFormu_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KontrolFormu_KeyDown);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonNO;
        private System.Windows.Forms.Button buttonYES;
        private System.Windows.Forms.Label labelAciklama;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonTamam;
    }
}