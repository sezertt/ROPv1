namespace ROPv1
{
    partial class UrunDegistir
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UrunDegistir));
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonNO = new System.Windows.Forms.Button();
            this.pinboardcontrol21 = new PinboardClassLibrary.Pinboardcontrol2();
            this.labelUrun3 = new System.Windows.Forms.Label();
            this.labelUrun4 = new System.Windows.Forms.Label();
            this.labelUrun5 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.labelUrun2 = new System.Windows.Forms.Label();
            this.labelUrun1 = new System.Windows.Forms.Label();
            this.textBoxUrun5 = new System.Windows.Forms.TextBox();
            this.textBoxUrun4 = new System.Windows.Forms.TextBox();
            this.textBoxUrun3 = new System.Windows.Forms.TextBox();
            this.textBoxUrun2 = new System.Windows.Forms.TextBox();
            this.textBoxUrun1 = new System.Windows.Forms.TextBox();
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
            this.buttonOK.Location = new System.Drawing.Point(360, 435);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(125, 50);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "Tamam";
            this.buttonOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonOK.UseVisualStyleBackColor = false;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonNO
            // 
            this.buttonNO.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonNO.BackColor = System.Drawing.SystemColors.Window;
            this.buttonNO.DialogResult = System.Windows.Forms.DialogResult.No;
            this.buttonNO.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonNO.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonNO.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonNO.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonNO.Location = new System.Drawing.Point(12, 435);
            this.buttonNO.Name = "buttonNO";
            this.buttonNO.Size = new System.Drawing.Size(125, 50);
            this.buttonNO.TabIndex = 3;
            this.buttonNO.Text = "İptal    ";
            this.buttonNO.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonNO.UseVisualStyleBackColor = false;
            this.buttonNO.Click += new System.EventHandler(this.buttonNO_Click);
            // 
            // pinboardcontrol21
            // 
            this.pinboardcontrol21.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pinboardcontrol21.KeyboardType = PinboardClassLibrary.BoW.Standard;
            this.pinboardcontrol21.Location = new System.Drawing.Point(102, 198);
            this.pinboardcontrol21.Name = "pinboardcontrol21";
            this.pinboardcontrol21.Size = new System.Drawing.Size(293, 237);
            this.pinboardcontrol21.TabIndex = 53;
            this.pinboardcontrol21.UserKeyPressed += new PinboardClassLibrary.PinboardDelegate(this.pinboardcontrol21_UserKeyPressed);
            // 
            // labelUrun3
            // 
            this.labelUrun3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelUrun3.AutoSize = true;
            this.labelUrun3.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelUrun3.ForeColor = System.Drawing.SystemColors.Window;
            this.labelUrun3.Location = new System.Drawing.Point(9, 93);
            this.labelUrun3.Name = "labelUrun3";
            this.labelUrun3.Size = new System.Drawing.Size(234, 24);
            this.labelUrun3.TabIndex = 17;
            this.labelUrun3.Tag = "3";
            this.labelUrun3.Text = "--------------------------------";
            // 
            // labelUrun4
            // 
            this.labelUrun4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelUrun4.AutoSize = true;
            this.labelUrun4.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelUrun4.ForeColor = System.Drawing.SystemColors.Window;
            this.labelUrun4.Location = new System.Drawing.Point(9, 131);
            this.labelUrun4.Name = "labelUrun4";
            this.labelUrun4.Size = new System.Drawing.Size(234, 24);
            this.labelUrun4.TabIndex = 18;
            this.labelUrun4.Tag = "4";
            this.labelUrun4.Text = "--------------------------------";
            // 
            // labelUrun5
            // 
            this.labelUrun5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelUrun5.AutoSize = true;
            this.labelUrun5.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelUrun5.ForeColor = System.Drawing.SystemColors.Window;
            this.labelUrun5.Location = new System.Drawing.Point(9, 169);
            this.labelUrun5.Name = "labelUrun5";
            this.labelUrun5.Size = new System.Drawing.Size(234, 24);
            this.labelUrun5.TabIndex = 19;
            this.labelUrun5.Tag = "5";
            this.labelUrun5.Text = "--------------------------------";
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
            this.labelUrun2.Location = new System.Drawing.Point(9, 55);
            this.labelUrun2.Name = "labelUrun2";
            this.labelUrun2.Size = new System.Drawing.Size(234, 24);
            this.labelUrun2.TabIndex = 16;
            this.labelUrun2.Tag = "2";
            this.labelUrun2.Text = "--------------------------------";
            // 
            // labelUrun1
            // 
            this.labelUrun1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelUrun1.AutoSize = true;
            this.labelUrun1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelUrun1.ForeColor = System.Drawing.SystemColors.Window;
            this.labelUrun1.Location = new System.Drawing.Point(9, 17);
            this.labelUrun1.Name = "labelUrun1";
            this.labelUrun1.Size = new System.Drawing.Size(234, 24);
            this.labelUrun1.TabIndex = 15;
            this.labelUrun1.Tag = "1";
            this.labelUrun1.Text = "--------------------------------";
            // 
            // textBoxUrun5
            // 
            this.textBoxUrun5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxUrun5.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.textBoxUrun5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBoxUrun5.Location = new System.Drawing.Point(395, 167);
            this.textBoxUrun5.MaxLength = 3;
            this.textBoxUrun5.Name = "textBoxUrun5";
            this.textBoxUrun5.Size = new System.Drawing.Size(86, 32);
            this.textBoxUrun5.TabIndex = 79;
            this.textBoxUrun5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxUrun5.Click += new System.EventHandler(this.textBoxUrun1_Click);
            this.textBoxUrun5.TextChanged += new System.EventHandler(this.textNumberOfItem_TextChanged);
            this.textBoxUrun5.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxUrun5_KeyPress);
            // 
            // textBoxUrun4
            // 
            this.textBoxUrun4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxUrun4.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.textBoxUrun4.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBoxUrun4.Location = new System.Drawing.Point(395, 127);
            this.textBoxUrun4.MaxLength = 3;
            this.textBoxUrun4.Name = "textBoxUrun4";
            this.textBoxUrun4.Size = new System.Drawing.Size(86, 32);
            this.textBoxUrun4.TabIndex = 80;
            this.textBoxUrun4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxUrun4.Click += new System.EventHandler(this.textBoxUrun1_Click);
            this.textBoxUrun4.TextChanged += new System.EventHandler(this.textNumberOfItem_TextChanged);
            this.textBoxUrun4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxUrun5_KeyPress);
            // 
            // textBoxUrun3
            // 
            this.textBoxUrun3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxUrun3.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.textBoxUrun3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBoxUrun3.Location = new System.Drawing.Point(395, 90);
            this.textBoxUrun3.MaxLength = 3;
            this.textBoxUrun3.Name = "textBoxUrun3";
            this.textBoxUrun3.Size = new System.Drawing.Size(86, 32);
            this.textBoxUrun3.TabIndex = 81;
            this.textBoxUrun3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxUrun3.Click += new System.EventHandler(this.textBoxUrun1_Click);
            this.textBoxUrun3.TextChanged += new System.EventHandler(this.textNumberOfItem_TextChanged);
            this.textBoxUrun3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxUrun5_KeyPress);
            // 
            // textBoxUrun2
            // 
            this.textBoxUrun2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxUrun2.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.textBoxUrun2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBoxUrun2.Location = new System.Drawing.Point(395, 52);
            this.textBoxUrun2.MaxLength = 3;
            this.textBoxUrun2.Name = "textBoxUrun2";
            this.textBoxUrun2.Size = new System.Drawing.Size(86, 32);
            this.textBoxUrun2.TabIndex = 82;
            this.textBoxUrun2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxUrun2.Click += new System.EventHandler(this.textBoxUrun1_Click);
            this.textBoxUrun2.TextChanged += new System.EventHandler(this.textNumberOfItem_TextChanged);
            this.textBoxUrun2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxUrun5_KeyPress);
            // 
            // textBoxUrun1
            // 
            this.textBoxUrun1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxUrun1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.textBoxUrun1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBoxUrun1.Location = new System.Drawing.Point(395, 14);
            this.textBoxUrun1.MaxLength = 3;
            this.textBoxUrun1.Name = "textBoxUrun1";
            this.textBoxUrun1.Size = new System.Drawing.Size(86, 32);
            this.textBoxUrun1.TabIndex = 83;
            this.textBoxUrun1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxUrun1.Click += new System.EventHandler(this.textBoxUrun1_Click);
            this.textBoxUrun1.TextChanged += new System.EventHandler(this.textNumberOfItem_TextChanged);
            this.textBoxUrun1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxUrun5_KeyPress);
            // 
            // UrunDegistir
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(496, 496);
            this.ControlBox = false;
            this.Controls.Add(this.textBoxUrun1);
            this.Controls.Add(this.textBoxUrun2);
            this.Controls.Add(this.textBoxUrun3);
            this.Controls.Add(this.textBoxUrun4);
            this.Controls.Add(this.textBoxUrun5);
            this.Controls.Add(this.labelUrun2);
            this.Controls.Add(this.labelUrun1);
            this.Controls.Add(this.labelUrun5);
            this.Controls.Add(this.labelUrun4);
            this.Controls.Add(this.labelUrun3);
            this.Controls.Add(this.pinboardcontrol21);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonNO);
            this.ForeColor = System.Drawing.SystemColors.Window;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(512, 512);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(512, 512);
            this.Name = "UrunDegistir";
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
        private System.Windows.Forms.Button buttonNO;
        private PinboardClassLibrary.Pinboardcontrol2 pinboardcontrol21;
        private System.Windows.Forms.Label labelUrun3;
        private System.Windows.Forms.Label labelUrun4;
        private System.Windows.Forms.Label labelUrun5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label labelUrun2;
        private System.Windows.Forms.Label labelUrun1;
        private System.Windows.Forms.TextBox textBoxUrun5;
        private System.Windows.Forms.TextBox textBoxUrun4;
        private System.Windows.Forms.TextBox textBoxUrun3;
        private System.Windows.Forms.TextBox textBoxUrun2;
        private System.Windows.Forms.TextBox textBoxUrun1;
    }
}