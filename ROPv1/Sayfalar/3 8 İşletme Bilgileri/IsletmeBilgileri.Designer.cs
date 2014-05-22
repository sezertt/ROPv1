namespace ROPv1
{
    partial class IsletmeBilgileri
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.keyboardcontrol1 = new KeyboardClassLibrary.Keyboardcontrol();
            this.buttonYeniYaziciEkle = new System.Windows.Forms.Button();
            this.treeYaziciAdi = new System.Windows.Forms.TreeView();
            this.newYaziciForm = new System.Windows.Forms.GroupBox();
            this.comboYaziciAdi = new System.Windows.Forms.ComboBox();
            this.textBoxTelefon = new System.Windows.Forms.MaskedTextBox();
            this.comboBoxFirmaAdi = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxAdres = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboYukluYazicilar = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonYaziciyiKaydet = new System.Windows.Forms.Button();
            this.buttonYaziciyiSil = new System.Windows.Forms.Button();
            this.buttonIptal = new System.Windows.Forms.Button();
            this.newYaziciForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // keyboardcontrol1
            // 
            this.keyboardcontrol1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.keyboardcontrol1.ForeColor = System.Drawing.SystemColors.Window;
            this.keyboardcontrol1.KeyboardType = KeyboardClassLibrary.BoW.Standard;
            this.keyboardcontrol1.Location = new System.Drawing.Point(0, 344);
            this.keyboardcontrol1.Name = "keyboardcontrol1";
            this.keyboardcontrol1.Size = new System.Drawing.Size(1085, 282);
            this.keyboardcontrol1.TabIndex = 40;
            this.keyboardcontrol1.UserKeyPressed += new KeyboardClassLibrary.KeyboardDelegate(this.keyboardcontrol1_UserKeyPressed);
            // 
            // buttonYeniYaziciEkle
            // 
            this.buttonYeniYaziciEkle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonYeniYaziciEkle.BackColor = System.Drawing.SystemColors.Window;
            this.buttonYeniYaziciEkle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonYeniYaziciEkle.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonYeniYaziciEkle.Image = global::ROPv1.Properties.Resources.add;
            this.buttonYeniYaziciEkle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonYeniYaziciEkle.Location = new System.Drawing.Point(601, 182);
            this.buttonYeniYaziciEkle.Name = "buttonYeniYaziciEkle";
            this.buttonYeniYaziciEkle.Size = new System.Drawing.Size(232, 62);
            this.buttonYeniYaziciEkle.TabIndex = 42;
            this.buttonYeniYaziciEkle.Text = "      Yeni Yazıcı Oluştur";
            this.buttonYeniYaziciEkle.UseVisualStyleBackColor = false;
            this.buttonYeniYaziciEkle.Click += new System.EventHandler(this.buttonYeniYaziciEkle_Click);
            // 
            // treeYaziciAdi
            // 
            this.treeYaziciAdi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeYaziciAdi.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.treeYaziciAdi.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.treeYaziciAdi.FullRowSelect = true;
            this.treeYaziciAdi.HideSelection = false;
            this.treeYaziciAdi.HotTracking = true;
            this.treeYaziciAdi.Indent = 5;
            this.treeYaziciAdi.ItemHeight = 35;
            this.treeYaziciAdi.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.treeYaziciAdi.Location = new System.Drawing.Point(7, 7);
            this.treeYaziciAdi.Margin = new System.Windows.Forms.Padding(0);
            this.treeYaziciAdi.Name = "treeYaziciAdi";
            this.treeYaziciAdi.ShowLines = false;
            this.treeYaziciAdi.Size = new System.Drawing.Size(223, 331);
            this.treeYaziciAdi.TabIndex = 41;
            this.treeYaziciAdi.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeYaziciAdi_AfterSelect);
            // 
            // newYaziciForm
            // 
            this.newYaziciForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.newYaziciForm.BackColor = System.Drawing.Color.White;
            this.newYaziciForm.Controls.Add(this.comboYaziciAdi);
            this.newYaziciForm.Controls.Add(this.textBoxTelefon);
            this.newYaziciForm.Controls.Add(this.comboBoxFirmaAdi);
            this.newYaziciForm.Controls.Add(this.label3);
            this.newYaziciForm.Controls.Add(this.textBoxAdres);
            this.newYaziciForm.Controls.Add(this.label2);
            this.newYaziciForm.Controls.Add(this.buttonYeniYaziciEkle);
            this.newYaziciForm.Controls.Add(this.label1);
            this.newYaziciForm.Controls.Add(this.comboYukluYazicilar);
            this.newYaziciForm.Controls.Add(this.label7);
            this.newYaziciForm.Controls.Add(this.label5);
            this.newYaziciForm.Controls.Add(this.buttonYaziciyiKaydet);
            this.newYaziciForm.Controls.Add(this.buttonYaziciyiSil);
            this.newYaziciForm.Controls.Add(this.buttonIptal);
            this.newYaziciForm.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.newYaziciForm.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.newYaziciForm.Location = new System.Drawing.Point(236, 5);
            this.newYaziciForm.Name = "newYaziciForm";
            this.newYaziciForm.Size = new System.Drawing.Size(846, 335);
            this.newYaziciForm.TabIndex = 43;
            this.newYaziciForm.TabStop = false;
            this.newYaziciForm.Text = "Yeni Yazıcı";
            // 
            // comboYaziciAdi
            // 
            this.comboYaziciAdi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboYaziciAdi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboYaziciAdi.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.comboYaziciAdi.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.comboYaziciAdi.FormattingEnabled = true;
            this.comboYaziciAdi.Items.AddRange(new object[] {
            "Adisyon",
            "Mutfak"});
            this.comboYaziciAdi.Location = new System.Drawing.Point(13, 52);
            this.comboYaziciAdi.MaxDropDownItems = 20;
            this.comboYaziciAdi.MaxLength = 100;
            this.comboYaziciAdi.Name = "comboYaziciAdi";
            this.comboYaziciAdi.Size = new System.Drawing.Size(400, 32);
            this.comboYaziciAdi.TabIndex = 51;
            this.comboYaziciAdi.Click += new System.EventHandler(this.comboYaziciAdi_Click);
            this.comboYaziciAdi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.combo_KeyPress);
            // 
            // textBoxTelefon
            // 
            this.textBoxTelefon.Font = new System.Drawing.Font("Arial", 15.75F);
            this.textBoxTelefon.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBoxTelefon.Location = new System.Drawing.Point(196, 293);
            this.textBoxTelefon.Mask = "(999) 000-0000";
            this.textBoxTelefon.Name = "textBoxTelefon";
            this.textBoxTelefon.Size = new System.Drawing.Size(399, 32);
            this.textBoxTelefon.TabIndex = 50;
            // 
            // comboBoxFirmaAdi
            // 
            this.comboBoxFirmaAdi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxFirmaAdi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBoxFirmaAdi.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.comboBoxFirmaAdi.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.comboBoxFirmaAdi.FormattingEnabled = true;
            this.comboBoxFirmaAdi.Location = new System.Drawing.Point(13, 117);
            this.comboBoxFirmaAdi.MaxDropDownItems = 20;
            this.comboBoxFirmaAdi.MaxLength = 100;
            this.comboBoxFirmaAdi.Name = "comboBoxFirmaAdi";
            this.comboBoxFirmaAdi.Size = new System.Drawing.Size(820, 32);
            this.comboBoxFirmaAdi.TabIndex = 49;
            this.comboBoxFirmaAdi.Click += new System.EventHandler(this.comboYazicilar_Click);
            this.comboBoxFirmaAdi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.combo_KeyPress);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Location = new System.Drawing.Point(9, 296);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(181, 24);
            this.label3.TabIndex = 47;
            this.label3.Text = "İşletme Telefonu:";
            // 
            // textBoxAdres
            // 
            this.textBoxAdres.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAdres.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxAdres.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBoxAdres.Location = new System.Drawing.Point(13, 182);
            this.textBoxAdres.MaxLength = 250;
            this.textBoxAdres.Multiline = true;
            this.textBoxAdres.Name = "textBoxAdres";
            this.textBoxAdres.Size = new System.Drawing.Size(582, 98);
            this.textBoxAdres.TabIndex = 44;
            this.textBoxAdres.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.combo_KeyPress);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(429, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 24);
            this.label2.TabIndex = 43;
            this.label2.Text = "Yazıcıyı Seçin:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(9, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 24);
            this.label1.TabIndex = 42;
            this.label1.Text = "İşletme Adresi:";
            // 
            // comboYukluYazicilar
            // 
            this.comboYukluYazicilar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboYukluYazicilar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboYukluYazicilar.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.comboYukluYazicilar.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.comboYukluYazicilar.FormattingEnabled = true;
            this.comboYukluYazicilar.Location = new System.Drawing.Point(433, 52);
            this.comboYukluYazicilar.MaxDropDownItems = 20;
            this.comboYukluYazicilar.MaxLength = 50;
            this.comboYukluYazicilar.Name = "comboYukluYazicilar";
            this.comboYukluYazicilar.Size = new System.Drawing.Size(400, 32);
            this.comboYukluYazicilar.TabIndex = 4;
            this.comboYukluYazicilar.Click += new System.EventHandler(this.comboYazicilar_Click);
            this.comboYukluYazicilar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboYazicilar_KeyPress);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label7.Location = new System.Drawing.Point(9, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(127, 24);
            this.label7.TabIndex = 25;
            this.label7.Text = "İşletme Adı:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Location = new System.Drawing.Point(9, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 24);
            this.label5.TabIndex = 19;
            this.label5.Text = "Yazıcı Adı:";
            // 
            // buttonYaziciyiKaydet
            // 
            this.buttonYaziciyiKaydet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonYaziciyiKaydet.BackColor = System.Drawing.SystemColors.Window;
            this.buttonYaziciyiKaydet.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonYaziciyiKaydet.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonYaziciyiKaydet.Image = global::ROPv1.Properties.Resources.icon;
            this.buttonYaziciyiKaydet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonYaziciyiKaydet.Location = new System.Drawing.Point(727, 269);
            this.buttonYaziciyiKaydet.Name = "buttonYaziciyiKaydet";
            this.buttonYaziciyiKaydet.Size = new System.Drawing.Size(106, 55);
            this.buttonYaziciyiKaydet.TabIndex = 6;
            this.buttonYaziciyiKaydet.Text = "Kaydet";
            this.buttonYaziciyiKaydet.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonYaziciyiKaydet.UseVisualStyleBackColor = false;
            this.buttonYaziciyiKaydet.Click += new System.EventHandler(this.buttonYaziciyiKaydet_Click);
            // 
            // buttonYaziciyiSil
            // 
            this.buttonYaziciyiSil.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonYaziciyiSil.BackColor = System.Drawing.SystemColors.Window;
            this.buttonYaziciyiSil.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonYaziciyiSil.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonYaziciyiSil.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonYaziciyiSil.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonYaziciyiSil.Location = new System.Drawing.Point(601, 269);
            this.buttonYaziciyiSil.Name = "buttonYaziciyiSil";
            this.buttonYaziciyiSil.Size = new System.Drawing.Size(120, 55);
            this.buttonYaziciyiSil.TabIndex = 5;
            this.buttonYaziciyiSil.Text = "Yazıcıyı Sil";
            this.buttonYaziciyiSil.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonYaziciyiSil.UseVisualStyleBackColor = false;
            this.buttonYaziciyiSil.Click += new System.EventHandler(this.buttonYaziciyiSil_Click);
            // 
            // buttonIptal
            // 
            this.buttonIptal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonIptal.BackColor = System.Drawing.SystemColors.Window;
            this.buttonIptal.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonIptal.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonIptal.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonIptal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonIptal.Location = new System.Drawing.Point(601, 269);
            this.buttonIptal.Name = "buttonIptal";
            this.buttonIptal.Size = new System.Drawing.Size(110, 56);
            this.buttonIptal.TabIndex = 8;
            this.buttonIptal.Text = "İptal Et  ";
            this.buttonIptal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonIptal.UseVisualStyleBackColor = false;
            this.buttonIptal.Click += new System.EventHandler(this.buttonIptal_Click);
            // 
            // IsletmeBilgileri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeYaziciAdi);
            this.Controls.Add(this.newYaziciForm);
            this.Controls.Add(this.keyboardcontrol1);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.Name = "IsletmeBilgileri";
            this.Size = new System.Drawing.Size(1085, 626);
            this.Load += new System.EventHandler(this.IsletmeBilgileri_Load);
            this.newYaziciForm.ResumeLayout(false);
            this.newYaziciForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private KeyboardClassLibrary.Keyboardcontrol keyboardcontrol1;
        private System.Windows.Forms.Button buttonYeniYaziciEkle;
        private System.Windows.Forms.TreeView treeYaziciAdi;
        private System.Windows.Forms.GroupBox newYaziciForm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboYukluYazicilar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonYaziciyiKaydet;
        private System.Windows.Forms.Button buttonYaziciyiSil;
        private System.Windows.Forms.Button buttonIptal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxAdres;
        private System.Windows.Forms.ComboBox comboBoxFirmaAdi;
        private System.Windows.Forms.MaskedTextBox textBoxTelefon;
        private System.Windows.Forms.ComboBox comboYaziciAdi;
    }
}
