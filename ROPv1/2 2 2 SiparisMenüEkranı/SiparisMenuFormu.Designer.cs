namespace ROPv1
{
    partial class SiparisMenuFormu
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("İkramlar", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Siparişler", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Yeni İkramlar", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Yeni Siparişler", System.Windows.Forms.HorizontalAlignment.Left);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SiparisMenuFormu));
            this.flowPanelUrunler = new System.Windows.Forms.FlowLayoutPanel();
            this.labelGun = new System.Windows.Forms.Label();
            this.labelToplamHesap = new System.Windows.Forms.Label();
            this.textNumberOfItem = new System.Windows.Forms.TextBox();
            this.buttonDeleteText = new System.Windows.Forms.Button();
            this.pinboardcontrol21 = new PinboardClassLibrary.Pinboardcontrol2();
            this.flowPanelMenuBasliklari = new System.Windows.Forms.FlowLayoutPanel();
            this.labelMasa = new System.Windows.Forms.Label();
            this.labelDepartman = new System.Windows.Forms.Label();
            this.ımageList1 = new System.Windows.Forms.ImageList(this.components);
            this.listHesap = new ROPv1.MyListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonUrunIptal = new System.Windows.Forms.Button();
            this.buttonUrunIkram = new System.Windows.Forms.Button();
            this.buttonTasi = new System.Windows.Forms.Button();
            this.buttonUrunListDown = new System.Windows.Forms.Button();
            this.buttonUrunListUp = new System.Windows.Forms.Button();
            this.buttonMenulerDown = new System.Windows.Forms.Button();
            this.buttonMenulerUp = new System.Windows.Forms.Button();
            this.buttonUrunlerDown = new System.Windows.Forms.Button();
            this.buttonUrunlerUp = new System.Windows.Forms.Button();
            this.buttonMasaDegistir = new System.Windows.Forms.Button();
            this.buttonNotEkle = new System.Windows.Forms.Button();
            this.buttonTamam = new System.Windows.Forms.Button();
            this.buttonHesapOde = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // flowPanelUrunler
            // 
            this.flowPanelUrunler.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.flowPanelUrunler.AutoScroll = true;
            this.flowPanelUrunler.Location = new System.Drawing.Point(12, 12);
            this.flowPanelUrunler.Name = "flowPanelUrunler";
            this.flowPanelUrunler.Size = new System.Drawing.Size(504, 454);
            this.flowPanelUrunler.TabIndex = 0;
            this.flowPanelUrunler.SizeChanged += new System.EventHandler(this.urunPanelSizeChanged);
            // 
            // labelGun
            // 
            this.labelGun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelGun.AutoSize = true;
            this.labelGun.BackColor = System.Drawing.Color.Transparent;
            this.labelGun.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelGun.ForeColor = System.Drawing.SystemColors.Window;
            this.labelGun.Location = new System.Drawing.Point(769, 670);
            this.labelGun.Name = "labelGun";
            this.labelGun.Size = new System.Drawing.Size(179, 33);
            this.labelGun.TabIndex = 64;
            this.labelGun.Text = "Toplam Hesap:";
            this.labelGun.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelToplamHesap
            // 
            this.labelToplamHesap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelToplamHesap.BackColor = System.Drawing.Color.Transparent;
            this.labelToplamHesap.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelToplamHesap.ForeColor = System.Drawing.SystemColors.Window;
            this.labelToplamHesap.Location = new System.Drawing.Point(963, 670);
            this.labelToplamHesap.Margin = new System.Windows.Forms.Padding(0);
            this.labelToplamHesap.Name = "labelToplamHesap";
            this.labelToplamHesap.Size = new System.Drawing.Size(397, 33);
            this.labelToplamHesap.TabIndex = 65;
            this.labelToplamHesap.Text = "0,00";
            this.labelToplamHesap.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textNumberOfItem
            // 
            this.textNumberOfItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textNumberOfItem.Font = new System.Drawing.Font("Arial", 39F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textNumberOfItem.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textNumberOfItem.Location = new System.Drawing.Point(373, 617);
            this.textNumberOfItem.MaxLength = 3;
            this.textNumberOfItem.Name = "textNumberOfItem";
            this.textNumberOfItem.Size = new System.Drawing.Size(122, 67);
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
            this.buttonDeleteText.Location = new System.Drawing.Point(373, 688);
            this.buttonDeleteText.Name = "buttonDeleteText";
            this.buttonDeleteText.Size = new System.Drawing.Size(122, 68);
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
            this.pinboardcontrol21.Location = new System.Drawing.Point(5, 467);
            this.pinboardcontrol21.Name = "pinboardcontrol21";
            this.pinboardcontrol21.Size = new System.Drawing.Size(362, 297);
            this.pinboardcontrol21.TabIndex = 74;
            this.pinboardcontrol21.UserKeyPressed += new PinboardClassLibrary.PinboardDelegate(this.pinboardcontrol21_UserKeyPressed);
            // 
            // flowPanelMenuBasliklari
            // 
            this.flowPanelMenuBasliklari.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowPanelMenuBasliklari.AutoScroll = true;
            this.flowPanelMenuBasliklari.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowPanelMenuBasliklari.Location = new System.Drawing.Point(517, 12);
            this.flowPanelMenuBasliklari.Name = "flowPanelMenuBasliklari";
            this.flowPanelMenuBasliklari.Size = new System.Drawing.Size(250, 671);
            this.flowPanelMenuBasliklari.TabIndex = 63;
            this.flowPanelMenuBasliklari.WrapContents = false;
            this.flowPanelMenuBasliklari.SizeChanged += new System.EventHandler(this.myPannel_SizeChanged);
            // 
            // labelMasa
            // 
            this.labelMasa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMasa.AutoSize = true;
            this.labelMasa.BackColor = System.Drawing.Color.Transparent;
            this.labelMasa.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelMasa.ForeColor = System.Drawing.SystemColors.Window;
            this.labelMasa.Location = new System.Drawing.Point(767, 125);
            this.labelMasa.Name = "labelMasa";
            this.labelMasa.Size = new System.Drawing.Size(83, 33);
            this.labelMasa.TabIndex = 76;
            this.labelMasa.Text = "Masa:";
            this.labelMasa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelDepartman
            // 
            this.labelDepartman.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDepartman.BackColor = System.Drawing.Color.Transparent;
            this.labelDepartman.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelDepartman.ForeColor = System.Drawing.SystemColors.Window;
            this.labelDepartman.Location = new System.Drawing.Point(1046, 125);
            this.labelDepartman.Margin = new System.Windows.Forms.Padding(0);
            this.labelDepartman.Name = "labelDepartman";
            this.labelDepartman.Size = new System.Drawing.Size(314, 33);
            this.labelDepartman.TabIndex = 77;
            this.labelDepartman.Text = "Departman:";
            this.labelDepartman.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ımageList1
            // 
            this.ımageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.ımageList1.ImageSize = new System.Drawing.Size(1, 28);
            this.ımageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // listHesap
            // 
            this.listHesap.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listHesap.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listHesap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listHesap.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listHesap.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.listHesap.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.listHesap.FullRowSelect = true;
            listViewGroup1.Header = "İkramlar";
            listViewGroup1.Name = "ikramGrubu";
            listViewGroup1.Tag = "0";
            listViewGroup2.Header = "Siparişler";
            listViewGroup2.Name = "siparisGrubu";
            listViewGroup2.Tag = "1";
            listViewGroup3.Header = "Yeni İkramlar";
            listViewGroup3.Name = "YeniIkramGrubu";
            listViewGroup4.Header = "Yeni Siparişler";
            listViewGroup4.Name = "YeniSiparisGrubu";
            listViewGroup4.Tag = "2";
            this.listHesap.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3,
            listViewGroup4});
            this.listHesap.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listHesap.HideSelection = false;
            this.listHesap.LabelWrap = false;
            this.listHesap.Location = new System.Drawing.Point(969, 161);
            this.listHesap.Margin = new System.Windows.Forms.Padding(0);
            this.listHesap.Name = "listHesap";
            this.listHesap.Size = new System.Drawing.Size(385, 504);
            this.listHesap.SmallImageList = this.ımageList1;
            this.listHesap.TabIndex = 0;
            this.listHesap.UseCompatibleStateImageBehavior = false;
            this.listHesap.View = System.Windows.Forms.View.Details;
            this.listHesap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listHesap_Click);
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
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.BackColor = System.Drawing.SystemColors.Window;
            this.buttonAdd.Enabled = false;
            this.buttonAdd.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonAdd.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonAdd.Image = global::ROPv1.Properties.Resources.addBig;
            this.buttonAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAdd.Location = new System.Drawing.Point(772, 331);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Padding = new System.Windows.Forms.Padding(8);
            this.buttonAdd.Size = new System.Drawing.Size(190, 79);
            this.buttonAdd.TabIndex = 84;
            this.buttonAdd.Text = "Ekle";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonUrunIptal
            // 
            this.buttonUrunIptal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUrunIptal.BackColor = System.Drawing.SystemColors.Window;
            this.buttonUrunIptal.Enabled = false;
            this.buttonUrunIptal.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonUrunIptal.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonUrunIptal.Image = global::ROPv1.Properties.Resources.deleteBig;
            this.buttonUrunIptal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonUrunIptal.Location = new System.Drawing.Point(772, 501);
            this.buttonUrunIptal.Name = "buttonUrunIptal";
            this.buttonUrunIptal.Padding = new System.Windows.Forms.Padding(8);
            this.buttonUrunIptal.Size = new System.Drawing.Size(190, 79);
            this.buttonUrunIptal.TabIndex = 81;
            this.buttonUrunIptal.Text = " İptal Et";
            this.buttonUrunIptal.UseVisualStyleBackColor = false;
            this.buttonUrunIptal.Click += new System.EventHandler(this.buttonUrunIptal_Click);
            // 
            // buttonUrunIkram
            // 
            this.buttonUrunIkram.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUrunIkram.BackColor = System.Drawing.SystemColors.Window;
            this.buttonUrunIkram.Enabled = false;
            this.buttonUrunIkram.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonUrunIkram.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonUrunIkram.Image = global::ROPv1.Properties.Resources.icon;
            this.buttonUrunIkram.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonUrunIkram.Location = new System.Drawing.Point(772, 416);
            this.buttonUrunIkram.Name = "buttonUrunIkram";
            this.buttonUrunIkram.Padding = new System.Windows.Forms.Padding(8);
            this.buttonUrunIkram.Size = new System.Drawing.Size(190, 79);
            this.buttonUrunIkram.TabIndex = 80;
            this.buttonUrunIkram.Text = "İkram";
            this.buttonUrunIkram.UseVisualStyleBackColor = false;
            this.buttonUrunIkram.Click += new System.EventHandler(this.buttonUrunIkram_Click);
            // 
            // buttonTasi
            // 
            this.buttonTasi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTasi.BackColor = System.Drawing.SystemColors.Window;
            this.buttonTasi.Enabled = false;
            this.buttonTasi.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonTasi.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonTasi.Image = global::ROPv1.Properties.Resources.tableSmall;
            this.buttonTasi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonTasi.Location = new System.Drawing.Point(772, 246);
            this.buttonTasi.Name = "buttonTasi";
            this.buttonTasi.Padding = new System.Windows.Forms.Padding(8);
            this.buttonTasi.Size = new System.Drawing.Size(190, 79);
            this.buttonTasi.TabIndex = 83;
            this.buttonTasi.Text = "Taşı";
            this.buttonTasi.UseVisualStyleBackColor = false;
            this.buttonTasi.Click += new System.EventHandler(this.buttonTasi_Click);
            // 
            // buttonUrunListDown
            // 
            this.buttonUrunListDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUrunListDown.BackColor = System.Drawing.SystemColors.Window;
            this.buttonUrunListDown.BackgroundImage = global::ROPv1.Properties.Resources.downBig;
            this.buttonUrunListDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonUrunListDown.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonUrunListDown.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonUrunListDown.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonUrunListDown.Location = new System.Drawing.Point(772, 586);
            this.buttonUrunListDown.Name = "buttonUrunListDown";
            this.buttonUrunListDown.Size = new System.Drawing.Size(190, 79);
            this.buttonUrunListDown.TabIndex = 79;
            this.buttonUrunListDown.UseVisualStyleBackColor = false;
            this.buttonUrunListDown.Click += new System.EventHandler(this.buttonUrunListDown_Click);
            // 
            // buttonUrunListUp
            // 
            this.buttonUrunListUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUrunListUp.BackColor = System.Drawing.SystemColors.Window;
            this.buttonUrunListUp.BackgroundImage = global::ROPv1.Properties.Resources.upBig;
            this.buttonUrunListUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonUrunListUp.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonUrunListUp.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonUrunListUp.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonUrunListUp.Location = new System.Drawing.Point(772, 161);
            this.buttonUrunListUp.Name = "buttonUrunListUp";
            this.buttonUrunListUp.Size = new System.Drawing.Size(190, 79);
            this.buttonUrunListUp.TabIndex = 78;
            this.buttonUrunListUp.UseVisualStyleBackColor = false;
            this.buttonUrunListUp.Click += new System.EventHandler(this.buttonUrunListUp_Click);
            // 
            // buttonMenulerDown
            // 
            this.buttonMenulerDown.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonMenulerDown.BackColor = System.Drawing.Color.White;
            this.buttonMenulerDown.BackgroundImage = global::ROPv1.Properties.Resources.downBig;
            this.buttonMenulerDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonMenulerDown.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonMenulerDown.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonMenulerDown.Location = new System.Drawing.Point(645, 688);
            this.buttonMenulerDown.Name = "buttonMenulerDown";
            this.buttonMenulerDown.Size = new System.Drawing.Size(122, 68);
            this.buttonMenulerDown.TabIndex = 73;
            this.buttonMenulerDown.TabStop = false;
            this.buttonMenulerDown.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonMenulerDown.UseVisualStyleBackColor = false;
            this.buttonMenulerDown.Click += new System.EventHandler(this.MenuScrollDown);
            // 
            // buttonMenulerUp
            // 
            this.buttonMenulerUp.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonMenulerUp.BackColor = System.Drawing.Color.White;
            this.buttonMenulerUp.BackgroundImage = global::ROPv1.Properties.Resources.upBig;
            this.buttonMenulerUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonMenulerUp.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonMenulerUp.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonMenulerUp.Location = new System.Drawing.Point(517, 688);
            this.buttonMenulerUp.Name = "buttonMenulerUp";
            this.buttonMenulerUp.Size = new System.Drawing.Size(122, 68);
            this.buttonMenulerUp.TabIndex = 72;
            this.buttonMenulerUp.TabStop = false;
            this.buttonMenulerUp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonMenulerUp.UseVisualStyleBackColor = false;
            this.buttonMenulerUp.Click += new System.EventHandler(this.MenuScrollUp);
            // 
            // buttonUrunlerDown
            // 
            this.buttonUrunlerDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonUrunlerDown.BackColor = System.Drawing.Color.White;
            this.buttonUrunlerDown.BackgroundImage = global::ROPv1.Properties.Resources.downBig;
            this.buttonUrunlerDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonUrunlerDown.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonUrunlerDown.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonUrunlerDown.Location = new System.Drawing.Point(373, 546);
            this.buttonUrunlerDown.Name = "buttonUrunlerDown";
            this.buttonUrunlerDown.Size = new System.Drawing.Size(122, 68);
            this.buttonUrunlerDown.TabIndex = 71;
            this.buttonUrunlerDown.TabStop = false;
            this.buttonUrunlerDown.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonUrunlerDown.UseVisualStyleBackColor = false;
            this.buttonUrunlerDown.Click += new System.EventHandler(this.UrunScrollDown);
            // 
            // buttonUrunlerUp
            // 
            this.buttonUrunlerUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonUrunlerUp.BackColor = System.Drawing.Color.White;
            this.buttonUrunlerUp.BackgroundImage = global::ROPv1.Properties.Resources.upBig;
            this.buttonUrunlerUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonUrunlerUp.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonUrunlerUp.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonUrunlerUp.Location = new System.Drawing.Point(373, 474);
            this.buttonUrunlerUp.Name = "buttonUrunlerUp";
            this.buttonUrunlerUp.Size = new System.Drawing.Size(122, 68);
            this.buttonUrunlerUp.TabIndex = 70;
            this.buttonUrunlerUp.TabStop = false;
            this.buttonUrunlerUp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonUrunlerUp.UseVisualStyleBackColor = false;
            this.buttonUrunlerUp.Click += new System.EventHandler(this.UrunScrollUp);
            // 
            // buttonMasaDegistir
            // 
            this.buttonMasaDegistir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMasaDegistir.BackColor = System.Drawing.SystemColors.Window;
            this.buttonMasaDegistir.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonMasaDegistir.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonMasaDegistir.Image = global::ROPv1.Properties.Resources.swap;
            this.buttonMasaDegistir.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonMasaDegistir.Location = new System.Drawing.Point(772, 12);
            this.buttonMasaDegistir.Name = "buttonMasaDegistir";
            this.buttonMasaDegistir.Size = new System.Drawing.Size(190, 110);
            this.buttonMasaDegistir.TabIndex = 67;
            this.buttonMasaDegistir.Text = "Masa Değiştir";
            this.buttonMasaDegistir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonMasaDegistir.UseVisualStyleBackColor = false;
            this.buttonMasaDegistir.Click += new System.EventHandler(this.changeTablesButton_Click);
            // 
            // buttonNotEkle
            // 
            this.buttonNotEkle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNotEkle.BackColor = System.Drawing.SystemColors.Window;
            this.buttonNotEkle.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonNotEkle.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonNotEkle.Image = global::ROPv1.Properties.Resources.adisyon;
            this.buttonNotEkle.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonNotEkle.Location = new System.Drawing.Point(968, 12);
            this.buttonNotEkle.Name = "buttonNotEkle";
            this.buttonNotEkle.Size = new System.Drawing.Size(190, 110);
            this.buttonNotEkle.TabIndex = 66;
            this.buttonNotEkle.Text = "Not Ekle";
            this.buttonNotEkle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonNotEkle.UseVisualStyleBackColor = false;
            this.buttonNotEkle.Click += new System.EventHandler(this.addNoteButton_Click);
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
            this.buttonTamam.Location = new System.Drawing.Point(1164, 12);
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
            this.buttonHesapOde.Location = new System.Drawing.Point(772, 708);
            this.buttonHesapOde.Name = "buttonHesapOde";
            this.buttonHesapOde.Size = new System.Drawing.Size(582, 48);
            this.buttonHesapOde.TabIndex = 57;
            this.buttonHesapOde.TabStop = false;
            this.buttonHesapOde.Text = "HESAP ÖDEME";
            this.buttonHesapOde.UseVisualStyleBackColor = false;
            this.buttonHesapOde.Click += new System.EventHandler(this.paymentButton_Click);
            // 
            // SiparisMenuFormu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonUrunIptal);
            this.Controls.Add(this.buttonUrunIkram);
            this.Controls.Add(this.listHesap);
            this.Controls.Add(this.buttonTasi);
            this.Controls.Add(this.buttonUrunListDown);
            this.Controls.Add(this.buttonUrunListUp);
            this.Controls.Add(this.labelDepartman);
            this.Controls.Add(this.labelMasa);
            this.Controls.Add(this.pinboardcontrol21);
            this.Controls.Add(this.buttonMenulerDown);
            this.Controls.Add(this.buttonMenulerUp);
            this.Controls.Add(this.buttonUrunlerDown);
            this.Controls.Add(this.buttonUrunlerUp);
            this.Controls.Add(this.buttonDeleteText);
            this.Controls.Add(this.textNumberOfItem);
            this.Controls.Add(this.buttonMasaDegistir);
            this.Controls.Add(this.buttonNotEkle);
            this.Controls.Add(this.labelToplamHesap);
            this.Controls.Add(this.labelGun);
            this.Controls.Add(this.flowPanelMenuBasliklari);
            this.Controls.Add(this.buttonTamam);
            this.Controls.Add(this.buttonHesapOde);
            this.Controls.Add(this.flowPanelUrunler);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SiparisMenuFormu";
            this.Text = "SiparisMenuFormu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SiparisMenuFormu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowPanelUrunler;
        private System.Windows.Forms.Button buttonHesapOde;
        private System.Windows.Forms.Button buttonTamam;
        private System.Windows.Forms.Label labelGun;
        private System.Windows.Forms.Label labelToplamHesap;
        private System.Windows.Forms.Button buttonNotEkle;
        private System.Windows.Forms.Button buttonMasaDegistir;
        private System.Windows.Forms.TextBox textNumberOfItem;
        private System.Windows.Forms.Button buttonDeleteText;
        private System.Windows.Forms.Button buttonUrunlerDown;
        private System.Windows.Forms.Button buttonUrunlerUp;
        private System.Windows.Forms.Button buttonMenulerUp;
        private System.Windows.Forms.Button buttonMenulerDown;
        private PinboardClassLibrary.Pinboardcontrol2 pinboardcontrol21;
        private System.Windows.Forms.FlowLayoutPanel flowPanelMenuBasliklari;
        private System.Windows.Forms.Label labelMasa;
        private System.Windows.Forms.Label labelDepartman;
        private System.Windows.Forms.Button buttonUrunListUp;
        private System.Windows.Forms.Button buttonUrunListDown;
        private System.Windows.Forms.Button buttonUrunIkram;
        private System.Windows.Forms.Button buttonUrunIptal;
        private System.Windows.Forms.Button buttonTasi;
        private System.Windows.Forms.ImageList ımageList1;
        private MyListView listHesap;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button buttonAdd;
    }
}