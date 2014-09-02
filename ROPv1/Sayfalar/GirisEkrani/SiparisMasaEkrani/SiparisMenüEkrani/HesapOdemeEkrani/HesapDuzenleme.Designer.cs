namespace ROPv1
{
    partial class HesapDuzenleme
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HesapDuzenleme));
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonNO = new System.Windows.Forms.Button();
            this.pinboardcontrol21 = new PinboardClassLibrary.Pinboardcontrol2();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.textBoxFis = new System.Windows.Forms.TextBox();
            this.textBoxKart = new System.Windows.Forms.TextBox();
            this.textBoxNakit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelNakit = new System.Windows.Forms.Label();
            this.labelKart = new System.Windows.Forms.Label();
            this.labelFis = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelDegisim = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelDepartmanAdi = new System.Windows.Forms.Label();
            this.labelMasaAdi = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOK.BackColor = System.Drawing.SystemColors.Window;
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonOK.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonOK.Image = global::ROPv1.Properties.Resources.icon;
            this.buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOK.Location = new System.Drawing.Point(343, 386);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(125, 50);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "Kaydet";
            this.buttonOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonOK.UseVisualStyleBackColor = false;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonNO
            // 
            this.buttonNO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonNO.BackColor = System.Drawing.SystemColors.Window;
            this.buttonNO.DialogResult = System.Windows.Forms.DialogResult.No;
            this.buttonNO.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonNO.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonNO.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonNO.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonNO.Location = new System.Drawing.Point(12, 387);
            this.buttonNO.Name = "buttonNO";
            this.buttonNO.Size = new System.Drawing.Size(125, 50);
            this.buttonNO.TabIndex = 3;
            this.buttonNO.Text = "İptal     ";
            this.buttonNO.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonNO.UseVisualStyleBackColor = false;
            this.buttonNO.Click += new System.EventHandler(this.buttonNO_Click);
            // 
            // pinboardcontrol21
            // 
            this.pinboardcontrol21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pinboardcontrol21.KeyboardType = PinboardClassLibrary.BoW.Standard;
            this.pinboardcontrol21.Location = new System.Drawing.Point(7, 150);
            this.pinboardcontrol21.Name = "pinboardcontrol21";
            this.pinboardcontrol21.Size = new System.Drawing.Size(308, 237);
            this.pinboardcontrol21.TabIndex = 53;
            this.pinboardcontrol21.UserKeyPressed += new PinboardClassLibrary.PinboardDelegate(this.pinboardcontrol21_UserKeyPressed);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // textBoxFis
            // 
            this.textBoxFis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFis.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.textBoxFis.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBoxFis.Location = new System.Drawing.Point(332, 111);
            this.textBoxFis.MaxLength = 50;
            this.textBoxFis.Name = "textBoxFis";
            this.textBoxFis.Size = new System.Drawing.Size(149, 32);
            this.textBoxFis.TabIndex = 81;
            this.textBoxFis.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxFis.Click += new System.EventHandler(this.textBoxUrun1_Click);
            this.textBoxFis.TextChanged += new System.EventHandler(this.textBoxNakit_TextChanged);
            this.textBoxFis.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxUrun5_KeyPress);
            // 
            // textBoxKart
            // 
            this.textBoxKart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxKart.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.textBoxKart.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBoxKart.Location = new System.Drawing.Point(332, 73);
            this.textBoxKart.MaxLength = 50;
            this.textBoxKart.Name = "textBoxKart";
            this.textBoxKart.Size = new System.Drawing.Size(149, 32);
            this.textBoxKart.TabIndex = 82;
            this.textBoxKart.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxKart.Click += new System.EventHandler(this.textBoxUrun1_Click);
            this.textBoxKart.TextChanged += new System.EventHandler(this.textBoxNakit_TextChanged);
            this.textBoxKart.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxUrun5_KeyPress);
            // 
            // textBoxNakit
            // 
            this.textBoxNakit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxNakit.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.textBoxNakit.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBoxNakit.Location = new System.Drawing.Point(332, 35);
            this.textBoxNakit.MaxLength = 50;
            this.textBoxNakit.Name = "textBoxNakit";
            this.textBoxNakit.Size = new System.Drawing.Size(149, 32);
            this.textBoxNakit.TabIndex = 83;
            this.textBoxNakit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxNakit.Click += new System.EventHandler(this.textBoxUrun1_Click);
            this.textBoxNakit.TextChanged += new System.EventHandler(this.textBoxNakit_TextChanged);
            this.textBoxNakit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxUrun5_KeyPress);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(8, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 24);
            this.label1.TabIndex = 84;
            this.label1.Tag = "6";
            this.label1.Text = "Nakit";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.SystemColors.Window;
            this.label2.Location = new System.Drawing.Point(8, 76);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 24);
            this.label2.TabIndex = 85;
            this.label2.Tag = "6";
            this.label2.Text = "Kredi Kartı";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.ForeColor = System.Drawing.SystemColors.Window;
            this.label3.Location = new System.Drawing.Point(8, 114);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 24);
            this.label3.TabIndex = 86;
            this.label3.Tag = "6";
            this.label3.Text = "Yemek Fişi";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.ForeColor = System.Drawing.SystemColors.Window;
            this.label4.Location = new System.Drawing.Point(136, 6);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(181, 24);
            this.label4.TabIndex = 87;
            this.label4.Tag = "6";
            this.label4.Text = "İlk Durum";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.ForeColor = System.Drawing.SystemColors.Window;
            this.label5.Location = new System.Drawing.Point(332, 6);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(149, 24);
            this.label5.TabIndex = 88;
            this.label5.Tag = "6";
            this.label5.Text = "Son Durum";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelNakit
            // 
            this.labelNakit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelNakit.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelNakit.ForeColor = System.Drawing.SystemColors.Window;
            this.labelNakit.Location = new System.Drawing.Point(132, 38);
            this.labelNakit.Margin = new System.Windows.Forms.Padding(0);
            this.labelNakit.Name = "labelNakit";
            this.labelNakit.Size = new System.Drawing.Size(185, 24);
            this.labelNakit.TabIndex = 89;
            this.labelNakit.Tag = "6";
            this.labelNakit.Text = "---";
            this.labelNakit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelKart
            // 
            this.labelKart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelKart.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelKart.ForeColor = System.Drawing.SystemColors.Window;
            this.labelKart.Location = new System.Drawing.Point(132, 76);
            this.labelKart.Margin = new System.Windows.Forms.Padding(0);
            this.labelKart.Name = "labelKart";
            this.labelKart.Size = new System.Drawing.Size(185, 24);
            this.labelKart.TabIndex = 90;
            this.labelKart.Tag = "6";
            this.labelKart.Text = "---";
            this.labelKart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelFis
            // 
            this.labelFis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelFis.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelFis.ForeColor = System.Drawing.SystemColors.Window;
            this.labelFis.Location = new System.Drawing.Point(132, 114);
            this.labelFis.Margin = new System.Windows.Forms.Padding(0);
            this.labelFis.Name = "labelFis";
            this.labelFis.Size = new System.Drawing.Size(185, 24);
            this.labelFis.TabIndex = 91;
            this.labelFis.Tag = "6";
            this.labelFis.Text = "---";
            this.labelFis.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.ForeColor = System.Drawing.SystemColors.Window;
            this.label6.Location = new System.Drawing.Point(317, 160);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(178, 24);
            this.label6.TabIndex = 92;
            this.label6.Tag = "6";
            this.label6.Text = "Değişim";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelDegisim
            // 
            this.labelDegisim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelDegisim.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelDegisim.ForeColor = System.Drawing.SystemColors.Window;
            this.labelDegisim.Location = new System.Drawing.Point(317, 190);
            this.labelDegisim.Margin = new System.Windows.Forms.Padding(0);
            this.labelDegisim.Name = "labelDegisim";
            this.labelDegisim.Size = new System.Drawing.Size(178, 24);
            this.labelDegisim.TabIndex = 93;
            this.labelDegisim.Tag = "6";
            this.labelDegisim.Text = "0,00";
            this.labelDegisim.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.ForeColor = System.Drawing.SystemColors.Window;
            this.label7.Location = new System.Drawing.Point(317, 240);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(178, 24);
            this.label7.TabIndex = 94;
            this.label7.Tag = "6";
            this.label7.Text = "Departman";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelDepartmanAdi
            // 
            this.labelDepartmanAdi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelDepartmanAdi.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelDepartmanAdi.ForeColor = System.Drawing.SystemColors.Window;
            this.labelDepartmanAdi.Location = new System.Drawing.Point(317, 270);
            this.labelDepartmanAdi.Margin = new System.Windows.Forms.Padding(0);
            this.labelDepartmanAdi.Name = "labelDepartmanAdi";
            this.labelDepartmanAdi.Size = new System.Drawing.Size(178, 24);
            this.labelDepartmanAdi.TabIndex = 95;
            this.labelDepartmanAdi.Tag = "6";
            this.labelDepartmanAdi.Text = "---";
            this.labelDepartmanAdi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelMasaAdi
            // 
            this.labelMasaAdi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelMasaAdi.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelMasaAdi.ForeColor = System.Drawing.SystemColors.Window;
            this.labelMasaAdi.Location = new System.Drawing.Point(317, 340);
            this.labelMasaAdi.Margin = new System.Windows.Forms.Padding(0);
            this.labelMasaAdi.Name = "labelMasaAdi";
            this.labelMasaAdi.Size = new System.Drawing.Size(178, 24);
            this.labelMasaAdi.TabIndex = 97;
            this.labelMasaAdi.Tag = "6";
            this.labelMasaAdi.Text = "---";
            this.labelMasaAdi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label10.ForeColor = System.Drawing.SystemColors.Window;
            this.label10.Location = new System.Drawing.Point(317, 310);
            this.label10.Margin = new System.Windows.Forms.Padding(0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(178, 24);
            this.label10.TabIndex = 96;
            this.label10.Tag = "6";
            this.label10.Text = "Masa";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HesapDuzenleme
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(496, 448);
            this.ControlBox = false;
            this.Controls.Add(this.labelMasaAdi);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.labelDepartmanAdi);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.labelDegisim);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.labelFis);
            this.Controls.Add(this.labelKart);
            this.Controls.Add(this.labelNakit);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxNakit);
            this.Controls.Add(this.textBoxKart);
            this.Controls.Add(this.textBoxFis);
            this.Controls.Add(this.pinboardcontrol21);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonNO);
            this.ForeColor = System.Drawing.SystemColors.Window;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(512, 464);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(512, 464);
            this.Name = "HesapDuzenleme";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HesapDuzenleme_FormClosing);
            this.Load += new System.EventHandler(this.HesapDuzenleme_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonNO;
        private PinboardClassLibrary.Pinboardcontrol2 pinboardcontrol21;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox textBoxFis;
        private System.Windows.Forms.TextBox textBoxKart;
        private System.Windows.Forms.TextBox textBoxNakit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelNakit;
        private System.Windows.Forms.Label labelKart;
        private System.Windows.Forms.Label labelFis;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelDegisim;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelDepartmanAdi;
        private System.Windows.Forms.Label labelMasaAdi;
        private System.Windows.Forms.Label label10;
    }
}