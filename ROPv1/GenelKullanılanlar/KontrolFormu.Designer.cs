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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KontrolFormu));
            this.buttonNO = new System.Windows.Forms.Button();
            this.buttonYES = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.labelAciklama = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonNO
            // 
            this.buttonNO.BackColor = System.Drawing.SystemColors.Window;
            this.buttonNO.DialogResult = System.Windows.Forms.DialogResult.No;
            this.buttonNO.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonNO.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonNO.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonNO.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonNO.Location = new System.Drawing.Point(333, 158);
            this.buttonNO.Name = "buttonNO";
            this.buttonNO.Size = new System.Drawing.Size(125, 50);
            this.buttonNO.TabIndex = 28;
            this.buttonNO.Text = "Hayır   ";
            this.buttonNO.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonNO.UseVisualStyleBackColor = false;
            // 
            // buttonYES
            // 
            this.buttonYES.BackColor = System.Drawing.SystemColors.Window;
            this.buttonYES.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.buttonYES.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonYES.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonYES.Image = ((System.Drawing.Image)(resources.GetObject("buttonYES.Image")));
            this.buttonYES.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonYES.Location = new System.Drawing.Point(58, 158);
            this.buttonYES.Name = "buttonYES";
            this.buttonYES.Size = new System.Drawing.Size(125, 50);
            this.buttonYES.TabIndex = 27;
            this.buttonYES.Text = "Evet   ";
            this.buttonYES.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonYES.UseVisualStyleBackColor = false;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(9, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 22);
            this.label5.TabIndex = 30;
            this.label5.Text = "Dikkat!";
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
            this.panel1.Location = new System.Drawing.Point(33, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(450, 110);
            this.panel1.TabIndex = 32;
            // 
            // buttonOK
            // 
            this.buttonOK.BackColor = System.Drawing.SystemColors.Window;
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonOK.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonOK.Image = ((System.Drawing.Image)(resources.GetObject("buttonOK.Image")));
            this.buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOK.Location = new System.Drawing.Point(195, 158);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(125, 50);
            this.buttonOK.TabIndex = 33;
            this.buttonOK.Text = "Tamam";
            this.buttonOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonOK.UseVisualStyleBackColor = false;
            this.buttonOK.Visible = false;
            // 
            // KontrolFormu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImage = global::ROPv1.Properties.Resources.BackOfDialog;
            this.ClientSize = new System.Drawing.Size(516, 230);
            this.ControlBox = false;
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonNO);
            this.Controls.Add(this.buttonYES);
            this.ForeColor = System.Drawing.SystemColors.Window;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KontrolFormu";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Black;
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonNO;
        private System.Windows.Forms.Button buttonYES;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelAciklama;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonOK;
    }
}