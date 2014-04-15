namespace ROPv1
{
    partial class HesapFormu
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Eski İkramlar", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Yeni İkramlar", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Eski Siparişler", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Yeni Siparişler", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("Eski İkramlar", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup6 = new System.Windows.Forms.ListViewGroup("Yeni İkramlar", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup7 = new System.Windows.Forms.ListViewGroup("Eski Siparişler", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup8 = new System.Windows.Forms.ListViewGroup("Yeni Siparişler", System.Windows.Forms.HorizontalAlignment.Left);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HesapFormu));
            this.textNumberOfItem = new System.Windows.Forms.TextBox();
            this.buttonDeleteText = new System.Windows.Forms.Button();
            this.pinboardcontrol21 = new PinboardClassLibrary.Pinboardcontrol2();
            this.labelMasa = new System.Windows.Forms.Label();
            this.labelDepartman = new System.Windows.Forms.Label();
            this.ımageList1 = new System.Windows.Forms.ImageList(this.components);
            this.labelKalanHesap = new System.Windows.Forms.Label();
            this.labelKalan = new System.Windows.Forms.Label();
            this.buttonTamam = new System.Windows.Forms.Button();
            this.buttonHesapOde = new System.Windows.Forms.Button();
            this.listUrunFiyat = new ROPv1.MyListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.myListView1 = new ROPv1.MyListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // textNumberOfItem
            // 
            this.textNumberOfItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textNumberOfItem.Font = new System.Drawing.Font("Arial", 39F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textNumberOfItem.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textNumberOfItem.Location = new System.Drawing.Point(971, 78);
            this.textNumberOfItem.MaxLength = 3;
            this.textNumberOfItem.Name = "textNumberOfItem";
            this.textNumberOfItem.Size = new System.Drawing.Size(255, 67);
            this.textNumberOfItem.TabIndex = 68;
            this.textNumberOfItem.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textNumberOfItem.TextChanged += new System.EventHandler(this.textNumberOfItem_TextChanged);
            this.textNumberOfItem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyPressedOnPriceText);
            // 
            // buttonDeleteText
            // 
            this.buttonDeleteText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDeleteText.BackColor = System.Drawing.SystemColors.Window;
            this.buttonDeleteText.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonDeleteText.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonDeleteText.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDeleteText.Location = new System.Drawing.Point(1232, 78);
            this.buttonDeleteText.Name = "buttonDeleteText";
            this.buttonDeleteText.Size = new System.Drawing.Size(122, 67);
            this.buttonDeleteText.TabIndex = 69;
            this.buttonDeleteText.TabStop = false;
            this.buttonDeleteText.Text = "SİL";
            this.buttonDeleteText.UseVisualStyleBackColor = false;
            this.buttonDeleteText.Click += new System.EventHandler(this.buttonDeleteText_Click);
            // 
            // pinboardcontrol21
            // 
            this.pinboardcontrol21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pinboardcontrol21.KeyboardType = PinboardClassLibrary.BoW.Standard;
            this.pinboardcontrol21.Location = new System.Drawing.Point(962, 151);
            this.pinboardcontrol21.Name = "pinboardcontrol21";
            this.pinboardcontrol21.Size = new System.Drawing.Size(400, 308);
            this.pinboardcontrol21.TabIndex = 74;
            this.pinboardcontrol21.UserKeyPressed += new PinboardClassLibrary.PinboardDelegate(this.pinboardcontrol21_UserKeyPressed);
            // 
            // labelMasa
            // 
            this.labelMasa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMasa.AutoSize = true;
            this.labelMasa.BackColor = System.Drawing.Color.Transparent;
            this.labelMasa.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelMasa.ForeColor = System.Drawing.SystemColors.Window;
            this.labelMasa.Location = new System.Drawing.Point(965, 42);
            this.labelMasa.Name = "labelMasa";
            this.labelMasa.Size = new System.Drawing.Size(83, 33);
            this.labelMasa.TabIndex = 76;
            this.labelMasa.Text = "Masa:";
            this.labelMasa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelDepartman
            // 
            this.labelDepartman.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDepartman.AutoSize = true;
            this.labelDepartman.BackColor = System.Drawing.Color.Transparent;
            this.labelDepartman.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelDepartman.ForeColor = System.Drawing.SystemColors.Window;
            this.labelDepartman.Location = new System.Drawing.Point(965, 9);
            this.labelDepartman.Margin = new System.Windows.Forms.Padding(0);
            this.labelDepartman.Name = "labelDepartman";
            this.labelDepartman.Size = new System.Drawing.Size(148, 33);
            this.labelDepartman.TabIndex = 77;
            this.labelDepartman.Text = "Departman:";
            this.labelDepartman.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ımageList1
            // 
            this.ımageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.ımageList1.ImageSize = new System.Drawing.Size(1, 28);
            this.ımageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // labelKalanHesap
            // 
            this.labelKalanHesap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelKalanHesap.BackColor = System.Drawing.Color.Transparent;
            this.labelKalanHesap.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelKalanHesap.ForeColor = System.Drawing.SystemColors.Window;
            this.labelKalanHesap.Location = new System.Drawing.Point(99, 727);
            this.labelKalanHesap.Margin = new System.Windows.Forms.Padding(0);
            this.labelKalanHesap.Name = "labelKalanHesap";
            this.labelKalanHesap.Size = new System.Drawing.Size(379, 33);
            this.labelKalanHesap.TabIndex = 86;
            this.labelKalanHesap.Text = "0,00";
            this.labelKalanHesap.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelKalan
            // 
            this.labelKalan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelKalan.AutoSize = true;
            this.labelKalan.BackColor = System.Drawing.Color.Transparent;
            this.labelKalan.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelKalan.ForeColor = System.Drawing.SystemColors.Window;
            this.labelKalan.Location = new System.Drawing.Point(12, 727);
            this.labelKalan.Name = "labelKalan";
            this.labelKalan.Size = new System.Drawing.Size(84, 33);
            this.labelKalan.TabIndex = 85;
            this.labelKalan.Text = "Kalan:";
            this.labelKalan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonTamam
            // 
            this.buttonTamam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTamam.BackColor = System.Drawing.SystemColors.Window;
            this.buttonTamam.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonTamam.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonTamam.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonTamam.Image = global::ROPv1.Properties.Resources.checkmark;
            this.buttonTamam.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonTamam.Location = new System.Drawing.Point(1164, 465);
            this.buttonTamam.Name = "buttonTamam";
            this.buttonTamam.Size = new System.Drawing.Size(190, 110);
            this.buttonTamam.TabIndex = 60;
            this.buttonTamam.Text = "Tamam";
            this.buttonTamam.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonTamam.UseVisualStyleBackColor = false;
            this.buttonTamam.Click += new System.EventHandler(this.buttonTamam_Click);
            // 
            // buttonHesapOde
            // 
            this.buttonHesapOde.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHesapOde.BackColor = System.Drawing.SystemColors.Window;
            this.buttonHesapOde.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonHesapOde.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonHesapOde.Image = global::ROPv1.Properties.Resources.pay;
            this.buttonHesapOde.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonHesapOde.Location = new System.Drawing.Point(968, 708);
            this.buttonHesapOde.Name = "buttonHesapOde";
            this.buttonHesapOde.Size = new System.Drawing.Size(386, 48);
            this.buttonHesapOde.TabIndex = 57;
            this.buttonHesapOde.TabStop = false;
            this.buttonHesapOde.Text = "HESAP ÖDEME";
            this.buttonHesapOde.UseVisualStyleBackColor = false;
            this.buttonHesapOde.Click += new System.EventHandler(this.paymentButton_Click);
            // 
            // listUrunFiyat
            // 
            this.listUrunFiyat.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listUrunFiyat.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listUrunFiyat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listUrunFiyat.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listUrunFiyat.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.listUrunFiyat.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.listUrunFiyat.FullRowSelect = true;
            listViewGroup1.Header = "Eski İkramlar";
            listViewGroup1.Name = "ikramGrubu";
            listViewGroup1.Tag = "0";
            listViewGroup2.Header = "Yeni İkramlar";
            listViewGroup2.Name = "YeniIkramGrubu";
            listViewGroup2.Tag = "1";
            listViewGroup3.Header = "Eski Siparişler";
            listViewGroup3.Name = "siparisGrubu";
            listViewGroup3.Tag = "2";
            listViewGroup4.Header = "Yeni Siparişler";
            listViewGroup4.Name = "YeniSiparisGrubu";
            listViewGroup4.Tag = "3";
            this.listUrunFiyat.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3,
            listViewGroup4});
            this.listUrunFiyat.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listUrunFiyat.HideSelection = false;
            this.listUrunFiyat.LabelWrap = false;
            this.listUrunFiyat.Location = new System.Drawing.Point(13, 9);
            this.listUrunFiyat.Margin = new System.Windows.Forms.Padding(0);
            this.listUrunFiyat.Name = "listUrunFiyat";
            this.listUrunFiyat.Size = new System.Drawing.Size(465, 718);
            this.listUrunFiyat.SmallImageList = this.ımageList1;
            this.listUrunFiyat.TabIndex = 0;
            this.listUrunFiyat.UseCompatibleStateImageBehavior = false;
            this.listUrunFiyat.View = System.Windows.Forms.View.Details;
            this.listUrunFiyat.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listHesap_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Adet";
            this.columnHeader1.Width = 50;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Siparişler";
            this.columnHeader2.Width = 230;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Fiyatları";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader3.Width = 100;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(598, 727);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(358, 33);
            this.label1.TabIndex = 89;
            this.label1.Text = "0,00";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.SystemColors.Window;
            this.label2.Location = new System.Drawing.Point(485, 727);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 33);
            this.label2.TabIndex = 88;
            this.label2.Text = "Ödenen:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // myListView1
            // 
            this.myListView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.myListView1.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.myListView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.myListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.myListView1.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.myListView1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.myListView1.FullRowSelect = true;
            listViewGroup5.Header = "Eski İkramlar";
            listViewGroup5.Name = "ikramGrubu";
            listViewGroup5.Tag = "0";
            listViewGroup6.Header = "Yeni İkramlar";
            listViewGroup6.Name = "YeniIkramGrubu";
            listViewGroup6.Tag = "1";
            listViewGroup7.Header = "Eski Siparişler";
            listViewGroup7.Name = "siparisGrubu";
            listViewGroup7.Tag = "2";
            listViewGroup8.Header = "Yeni Siparişler";
            listViewGroup8.Name = "YeniSiparisGrubu";
            listViewGroup8.Tag = "3";
            this.myListView1.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup5,
            listViewGroup6,
            listViewGroup7,
            listViewGroup8});
            this.myListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.myListView1.HideSelection = false;
            this.myListView1.LabelWrap = false;
            this.myListView1.Location = new System.Drawing.Point(491, 9);
            this.myListView1.Margin = new System.Windows.Forms.Padding(0);
            this.myListView1.Name = "myListView1";
            this.myListView1.Size = new System.Drawing.Size(465, 718);
            this.myListView1.SmallImageList = this.ımageList1;
            this.myListView1.TabIndex = 87;
            this.myListView1.UseCompatibleStateImageBehavior = false;
            this.myListView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Adet";
            this.columnHeader4.Width = 50;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Siparişler";
            this.columnHeader5.Width = 230;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Fiyatları";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader6.Width = 100;
            // 
            // HesapFormu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.myListView1);
            this.Controls.Add(this.labelKalanHesap);
            this.Controls.Add(this.labelKalan);
            this.Controls.Add(this.listUrunFiyat);
            this.Controls.Add(this.labelDepartman);
            this.Controls.Add(this.labelMasa);
            this.Controls.Add(this.pinboardcontrol21);
            this.Controls.Add(this.buttonDeleteText);
            this.Controls.Add(this.textNumberOfItem);
            this.Controls.Add(this.buttonTamam);
            this.Controls.Add(this.buttonHesapOde);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HesapFormu";
            this.ShowInTaskbar = false;
            this.Text = "SiparisMenuFormu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.HesapFormu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonHesapOde;
        private System.Windows.Forms.Button buttonTamam;
        private System.Windows.Forms.TextBox textNumberOfItem;
        private System.Windows.Forms.Button buttonDeleteText;
        private PinboardClassLibrary.Pinboardcontrol2 pinboardcontrol21;
        private System.Windows.Forms.Label labelMasa;
        private System.Windows.Forms.Label labelDepartman;
        private System.Windows.Forms.ImageList ımageList1;
        private MyListView listUrunFiyat;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label labelKalanHesap;
        private System.Windows.Forms.Label labelKalan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private MyListView myListView1;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
    }
}