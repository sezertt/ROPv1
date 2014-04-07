namespace ROPv1
{
    partial class PortFormu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PortFormu));
            this.buttonOK = new System.Windows.Forms.Button();
            this.pinboardcontrol21 = new PinboardClassLibrary.Pinboardcontrol2();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.labelUrun2 = new System.Windows.Forms.Label();
            this.labelUrun1 = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.comboServer = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonOK.BackColor = System.Drawing.SystemColors.Window;
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonOK.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonOK.Image = global::ROPv1.Properties.Resources.icon;
            this.buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOK.Location = new System.Drawing.Point(401, 186);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(83, 50);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonOK.UseVisualStyleBackColor = false;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // pinboardcontrol21
            // 
            this.pinboardcontrol21.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pinboardcontrol21.KeyboardType = PinboardClassLibrary.BoW.Standard;
            this.pinboardcontrol21.Location = new System.Drawing.Point(6, 6);
            this.pinboardcontrol21.Name = "pinboardcontrol21";
            this.pinboardcontrol21.Size = new System.Drawing.Size(293, 237);
            this.pinboardcontrol21.TabIndex = 53;
            this.pinboardcontrol21.UserKeyPressed += new PinboardClassLibrary.PinboardDelegate(this.pinboardcontrol21_UserKeyPressed);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // labelUrun2
            // 
            this.labelUrun2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelUrun2.AutoSize = true;
            this.labelUrun2.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelUrun2.ForeColor = System.Drawing.SystemColors.Window;
            this.labelUrun2.Location = new System.Drawing.Point(305, 76);
            this.labelUrun2.Name = "labelUrun2";
            this.labelUrun2.Size = new System.Drawing.Size(52, 24);
            this.labelUrun2.TabIndex = 16;
            this.labelUrun2.Tag = "2";
            this.labelUrun2.Text = "Port";
            // 
            // labelUrun1
            // 
            this.labelUrun1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelUrun1.AutoSize = true;
            this.labelUrun1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelUrun1.ForeColor = System.Drawing.SystemColors.Window;
            this.labelUrun1.Location = new System.Drawing.Point(305, 12);
            this.labelUrun1.Name = "labelUrun1";
            this.labelUrun1.Size = new System.Drawing.Size(29, 24);
            this.labelUrun1.TabIndex = 15;
            this.labelUrun1.Tag = "1";
            this.labelUrun1.Text = "IP";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxPort.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.textBoxPort.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBoxPort.Location = new System.Drawing.Point(305, 103);
            this.textBoxPort.MaxLength = 50;
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(179, 32);
            this.textBoxPort.TabIndex = 82;
            this.textBoxPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxIP
            // 
            this.textBoxIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxIP.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.textBoxIP.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBoxIP.Location = new System.Drawing.Point(305, 37);
            this.textBoxIP.MaxLength = 50;
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(179, 32);
            this.textBoxIP.TabIndex = 83;
            this.textBoxIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxIP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxIP_KeyPress);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCancel.BackColor = System.Drawing.SystemColors.Window;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.No;
            this.buttonCancel.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonCancel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonCancel.Image = global::ROPv1.Properties.Resources.deleteBig;
            this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCancel.Location = new System.Drawing.Point(305, 186);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(83, 50);
            this.buttonCancel.TabIndex = 84;
            this.buttonCancel.Text = "Çık";
            this.buttonCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonCancel.UseVisualStyleBackColor = false;
            // 
            // comboServer
            // 
            this.comboServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboServer.ContextMenuStrip = this.contextMenuStrip1;
            this.comboServer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboServer.Font = new System.Drawing.Font("Arial", 15.75F);
            this.comboServer.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.comboServer.FormattingEnabled = true;
            this.comboServer.Items.AddRange(new object[] {
            "Masa",
            "Mutfak",
            "Server"});
            this.comboServer.Location = new System.Drawing.Point(305, 148);
            this.comboServer.MaxDropDownItems = 20;
            this.comboServer.Name = "comboServer";
            this.comboServer.Size = new System.Drawing.Size(179, 32);
            this.comboServer.TabIndex = 85;
            this.comboServer.Text = "Masa";
            this.comboServer.Click += new System.EventHandler(this.showMenu);
            this.comboServer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxKeyPressed);
            // 
            // PortFormu
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(496, 248);
            this.ControlBox = false;
            this.Controls.Add(this.comboServer);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.labelUrun2);
            this.Controls.Add(this.labelUrun1);
            this.Controls.Add(this.pinboardcontrol21);
            this.Controls.Add(this.buttonOK);
            this.ForeColor = System.Drawing.SystemColors.Window;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(512, 264);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(512, 264);
            this.Name = "PortFormu";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.UrunDegistir_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private PinboardClassLibrary.Pinboardcontrol2 pinboardcontrol21;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label labelUrun2;
        private System.Windows.Forms.Label labelUrun1;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox comboServer;
    }
}