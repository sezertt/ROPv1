namespace ROPv1
{
    partial class AdisyonGoruntuleme
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdisyonGoruntuleme));
            this.labelGun = new System.Windows.Forms.Label();
            this.labelTarih = new System.Windows.Forms.Label();
            this.labelSaat = new System.Windows.Forms.Label();
            this.timerSaat = new System.Windows.Forms.Timer(this.components);
            this.exitButton = new System.Windows.Forms.Button();
            this.buttonAdisyonlariGetir = new System.Windows.Forms.Button();
            this.buttonArttir = new System.Windows.Forms.Button();
            this.buttonAzalt = new System.Windows.Forms.Button();
            this.textboxAdisyonID = new System.Windows.Forms.TextBox();
            this.dateBaslangic = new System.Windows.Forms.DateTimePicker();
            this.dateBitis = new System.Windows.Forms.DateTimePicker();
            this.comboAdisyonAyar = new System.Windows.Forms.ComboBox();
            this.listAdisyon = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listAdisyonDetay = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonSayfaArttir = new System.Windows.Forms.Button();
            this.buttonSayfaAzalt = new System.Windows.Forms.Button();
            this.labelSayfaSayisi = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelSayfa = new System.Windows.Forms.Label();
            this.labelToplamHesap = new System.Windows.Forms.Label();
            this.labelKalanText = new System.Windows.Forms.Label();
            this.buttonYazdir = new System.Windows.Forms.Button();
            this.keyboardcontrol1 = new KeyboardClassLibrary.Keyboardcontrol();
            this.SuspendLayout();
            // 
            // labelGun
            // 
            this.labelGun.BackColor = System.Drawing.Color.Transparent;
            this.labelGun.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelGun.ForeColor = System.Drawing.SystemColors.Window;
            this.labelGun.Location = new System.Drawing.Point(13, 88);
            this.labelGun.Name = "labelGun";
            this.labelGun.Size = new System.Drawing.Size(212, 33);
            this.labelGun.TabIndex = 12;
            this.labelGun.Text = "Pazartesi";
            this.labelGun.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTarih
            // 
            this.labelTarih.BackColor = System.Drawing.Color.Transparent;
            this.labelTarih.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelTarih.ForeColor = System.Drawing.SystemColors.Window;
            this.labelTarih.Location = new System.Drawing.Point(13, 61);
            this.labelTarih.Name = "labelTarih";
            this.labelTarih.Size = new System.Drawing.Size(212, 33);
            this.labelTarih.TabIndex = 11;
            this.labelTarih.Text = "10 Şubat 2014 ";
            this.labelTarih.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelSaat
            // 
            this.labelSaat.AutoSize = true;
            this.labelSaat.BackColor = System.Drawing.Color.Transparent;
            this.labelSaat.Font = new System.Drawing.Font("Calibri", 45F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelSaat.ForeColor = System.Drawing.SystemColors.Window;
            this.labelSaat.Location = new System.Drawing.Point(0, 0);
            this.labelSaat.Name = "labelSaat";
            this.labelSaat.Size = new System.Drawing.Size(246, 73);
            this.labelSaat.TabIndex = 10;
            this.labelSaat.Text = "22:55:30";
            this.labelSaat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerSaat
            // 
            this.timerSaat.Interval = 1000;
            this.timerSaat.Tick += new System.EventHandler(this.timerSaat_Tick);
            // 
            // exitButton
            // 
            this.exitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exitButton.BackColor = System.Drawing.SystemColors.Window;
            this.exitButton.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.exitButton.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.exitButton.Image = global::ROPv1.Properties.Resources.logOut;
            this.exitButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.exitButton.Location = new System.Drawing.Point(1134, 12);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(220, 110);
            this.exitButton.TabIndex = 4;
            this.exitButton.Text = "Çıkış";
            this.exitButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitPressed);
            // 
            // buttonAdisyonlariGetir
            // 
            this.buttonAdisyonlariGetir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdisyonlariGetir.BackColor = System.Drawing.SystemColors.Window;
            this.buttonAdisyonlariGetir.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonAdisyonlariGetir.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonAdisyonlariGetir.Image = global::ROPv1.Properties.Resources.checkmark;
            this.buttonAdisyonlariGetir.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonAdisyonlariGetir.Location = new System.Drawing.Point(908, 12);
            this.buttonAdisyonlariGetir.Name = "buttonAdisyonlariGetir";
            this.buttonAdisyonlariGetir.Size = new System.Drawing.Size(220, 110);
            this.buttonAdisyonlariGetir.TabIndex = 15;
            this.buttonAdisyonlariGetir.Text = "Adisyon Bul";
            this.buttonAdisyonlariGetir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonAdisyonlariGetir.UseVisualStyleBackColor = false;
            this.buttonAdisyonlariGetir.Click += new System.EventHandler(this.buttonAdisyonlariGetir_Click);
            // 
            // buttonArttir
            // 
            this.buttonArttir.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonArttir.BackColor = System.Drawing.Color.White;
            this.buttonArttir.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonArttir.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonArttir.Image = global::ROPv1.Properties.Resources.upBig;
            this.buttonArttir.Location = new System.Drawing.Point(656, 12);
            this.buttonArttir.Name = "buttonArttir";
            this.buttonArttir.Size = new System.Drawing.Size(120, 110);
            this.buttonArttir.TabIndex = 37;
            this.buttonArttir.TabStop = false;
            this.buttonArttir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonArttir.UseVisualStyleBackColor = false;
            this.buttonArttir.Click += new System.EventHandler(this.buttonArttir_Click);
            // 
            // buttonAzalt
            // 
            this.buttonAzalt.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonAzalt.BackColor = System.Drawing.SystemColors.Window;
            this.buttonAzalt.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonAzalt.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonAzalt.Image = global::ROPv1.Properties.Resources.downBig;
            this.buttonAzalt.Location = new System.Drawing.Point(782, 12);
            this.buttonAzalt.Name = "buttonAzalt";
            this.buttonAzalt.Size = new System.Drawing.Size(120, 110);
            this.buttonAzalt.TabIndex = 38;
            this.buttonAzalt.TabStop = false;
            this.buttonAzalt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAzalt.UseVisualStyleBackColor = false;
            this.buttonAzalt.Click += new System.EventHandler(this.buttonAzalt_Click);
            // 
            // textboxAdisyonID
            // 
            this.textboxAdisyonID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textboxAdisyonID.Font = new System.Drawing.Font("Calibri", 16F);
            this.textboxAdisyonID.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textboxAdisyonID.Location = new System.Drawing.Point(495, 13);
            this.textboxAdisyonID.MaxLength = 10;
            this.textboxAdisyonID.Name = "textboxAdisyonID";
            this.textboxAdisyonID.Size = new System.Drawing.Size(154, 34);
            this.textboxAdisyonID.TabIndex = 40;
            this.textboxAdisyonID.Enter += new System.EventHandler(this.textboxAdisyonID_Enter);
            this.textboxAdisyonID.Leave += new System.EventHandler(this.textboxAdisyonID_Leave);
            // 
            // dateBaslangic
            // 
            this.dateBaslangic.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dateBaslangic.CalendarFont = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dateBaslangic.CalendarForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dateBaslangic.CalendarMonthBackground = System.Drawing.SystemColors.ActiveCaption;
            this.dateBaslangic.CalendarTitleForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dateBaslangic.CalendarTrailingForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dateBaslangic.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dateBaslangic.Location = new System.Drawing.Point(247, 51);
            this.dateBaslangic.Name = "dateBaslangic";
            this.dateBaslangic.Size = new System.Drawing.Size(402, 33);
            this.dateBaslangic.TabIndex = 14;
            this.dateBaslangic.Enter += new System.EventHandler(this.dateBaslangic_Enter);
            // 
            // dateBitis
            // 
            this.dateBitis.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dateBitis.CalendarFont = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dateBitis.CalendarForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dateBitis.CalendarMonthBackground = System.Drawing.SystemColors.ActiveCaption;
            this.dateBitis.CalendarTitleForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dateBitis.CalendarTrailingForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dateBitis.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dateBitis.Location = new System.Drawing.Point(247, 88);
            this.dateBitis.Name = "dateBitis";
            this.dateBitis.Size = new System.Drawing.Size(402, 33);
            this.dateBitis.TabIndex = 16;
            this.dateBitis.ValueChanged += new System.EventHandler(this.dateBitis_ValueChanged);
            this.dateBitis.Enter += new System.EventHandler(this.dateBitis_Enter);
            // 
            // comboAdisyonAyar
            // 
            this.comboAdisyonAyar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboAdisyonAyar.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboAdisyonAyar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.comboAdisyonAyar.FormattingEnabled = true;
            this.comboAdisyonAyar.Items.AddRange(new object[] {
            "Açık Adisyonlar",
            "Tüm Adisyonlar",
            "Adisyon ID",
            "Masa Adı",
            "Departman Adı"});
            this.comboAdisyonAyar.Location = new System.Drawing.Point(247, 12);
            this.comboAdisyonAyar.Name = "comboAdisyonAyar";
            this.comboAdisyonAyar.Size = new System.Drawing.Size(242, 34);
            this.comboAdisyonAyar.TabIndex = 41;
            this.comboAdisyonAyar.Click += new System.EventHandler(this.comboAdisyonAyar_Click);
            this.comboAdisyonAyar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboAdisyonAyar_KeyPress);
            this.comboAdisyonAyar.Leave += new System.EventHandler(this.comboAdisyonAyar_Leave);
            // 
            // listAdisyon
            // 
            this.listAdisyon.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listAdisyon.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listAdisyon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listAdisyon.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11});
            this.listAdisyon.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.listAdisyon.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.listAdisyon.FullRowSelect = true;
            this.listAdisyon.GridLines = true;
            this.listAdisyon.HideSelection = false;
            this.listAdisyon.LabelWrap = false;
            this.listAdisyon.Location = new System.Drawing.Point(9, 127);
            this.listAdisyon.Margin = new System.Windows.Forms.Padding(0);
            this.listAdisyon.MultiSelect = false;
            this.listAdisyon.Name = "listAdisyon";
            this.listAdisyon.Scrollable = false;
            this.listAdisyon.ShowItemToolTips = true;
            this.listAdisyon.Size = new System.Drawing.Size(915, 607);
            this.listAdisyon.TabIndex = 42;
            this.listAdisyon.UseCompatibleStateImageBehavior = false;
            this.listAdisyon.View = System.Windows.Forms.View.Details;
            this.listAdisyon.SelectedIndexChanged += new System.EventHandler(this.listAdisyon_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Adisyon ID";
            this.columnHeader1.Width = 85;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Departman Adı";
            this.columnHeader2.Width = 116;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Masa Adı";
            this.columnHeader3.Width = 75;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Tarih";
            this.columnHeader4.Width = 85;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Açılış - Kapanış";
            this.columnHeader9.Width = 113;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Adisyon Notu";
            this.columnHeader10.Width = 338;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Toplam";
            this.columnHeader11.Width = 100;
            // 
            // listAdisyonDetay
            // 
            this.listAdisyonDetay.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listAdisyonDetay.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listAdisyonDetay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listAdisyonDetay.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader5,
            this.columnHeader7,
            this.columnHeader8});
            this.listAdisyonDetay.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.listAdisyonDetay.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.listAdisyonDetay.FullRowSelect = true;
            this.listAdisyonDetay.GridLines = true;
            this.listAdisyonDetay.LabelWrap = false;
            this.listAdisyonDetay.Location = new System.Drawing.Point(934, 127);
            this.listAdisyonDetay.Margin = new System.Windows.Forms.Padding(0);
            this.listAdisyonDetay.MultiSelect = false;
            this.listAdisyonDetay.Name = "listAdisyonDetay";
            this.listAdisyonDetay.ShowItemToolTips = true;
            this.listAdisyonDetay.Size = new System.Drawing.Size(420, 602);
            this.listAdisyonDetay.TabIndex = 43;
            this.listAdisyonDetay.UseCompatibleStateImageBehavior = false;
            this.listAdisyonDetay.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Garson";
            this.columnHeader6.Width = 109;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Ürün";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 163;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Adet";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader7.Width = 53;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Fiyat";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader8.Width = 91;
            // 
            // buttonSayfaArttir
            // 
            this.buttonSayfaArttir.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonSayfaArttir.BackColor = System.Drawing.SystemColors.Window;
            this.buttonSayfaArttir.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonSayfaArttir.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonSayfaArttir.Image = global::ROPv1.Properties.Resources.righticon;
            this.buttonSayfaArttir.Location = new System.Drawing.Point(622, 746);
            this.buttonSayfaArttir.Name = "buttonSayfaArttir";
            this.buttonSayfaArttir.Size = new System.Drawing.Size(120, 61);
            this.buttonSayfaArttir.TabIndex = 45;
            this.buttonSayfaArttir.TabStop = false;
            this.buttonSayfaArttir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSayfaArttir.UseVisualStyleBackColor = false;
            this.buttonSayfaArttir.Click += new System.EventHandler(this.buttonSayfaArttirClick);
            // 
            // buttonSayfaAzalt
            // 
            this.buttonSayfaAzalt.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonSayfaAzalt.BackColor = System.Drawing.SystemColors.Window;
            this.buttonSayfaAzalt.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonSayfaAzalt.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonSayfaAzalt.Image = global::ROPv1.Properties.Resources.lefticon;
            this.buttonSayfaAzalt.Location = new System.Drawing.Point(227, 746);
            this.buttonSayfaAzalt.Name = "buttonSayfaAzalt";
            this.buttonSayfaAzalt.Size = new System.Drawing.Size(120, 61);
            this.buttonSayfaAzalt.TabIndex = 44;
            this.buttonSayfaAzalt.TabStop = false;
            this.buttonSayfaAzalt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSayfaAzalt.UseVisualStyleBackColor = false;
            this.buttonSayfaAzalt.Click += new System.EventHandler(this.buttonSayfaAzalt_Click);
            // 
            // labelSayfaSayisi
            // 
            this.labelSayfaSayisi.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelSayfaSayisi.AutoSize = true;
            this.labelSayfaSayisi.BackColor = System.Drawing.Color.Transparent;
            this.labelSayfaSayisi.Font = new System.Drawing.Font("Calibri", 32F);
            this.labelSayfaSayisi.ForeColor = System.Drawing.SystemColors.Window;
            this.labelSayfaSayisi.Location = new System.Drawing.Point(496, 748);
            this.labelSayfaSayisi.Name = "labelSayfaSayisi";
            this.labelSayfaSayisi.Size = new System.Drawing.Size(45, 53);
            this.labelSayfaSayisi.TabIndex = 47;
            this.labelSayfaSayisi.Text = "0";
            this.labelSayfaSayisi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Calibri", 32F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.Window;
            this.label2.Location = new System.Drawing.Point(464, 748);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 53);
            this.label2.TabIndex = 48;
            this.label2.Text = "/";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelSayfa
            // 
            this.labelSayfa.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelSayfa.BackColor = System.Drawing.Color.Transparent;
            this.labelSayfa.Font = new System.Drawing.Font("Calibri", 32F);
            this.labelSayfa.ForeColor = System.Drawing.SystemColors.Window;
            this.labelSayfa.Location = new System.Drawing.Point(338, 748);
            this.labelSayfa.Name = "labelSayfa";
            this.labelSayfa.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelSayfa.Size = new System.Drawing.Size(138, 53);
            this.labelSayfa.TabIndex = 49;
            this.labelSayfa.Text = "0";
            this.labelSayfa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelSayfa.TextChanged += new System.EventHandler(this.labelSayfa_TextChanged);
            // 
            // labelToplamHesap
            // 
            this.labelToplamHesap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelToplamHesap.BackColor = System.Drawing.Color.Transparent;
            this.labelToplamHesap.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelToplamHesap.ForeColor = System.Drawing.SystemColors.Window;
            this.labelToplamHesap.Location = new System.Drawing.Point(1202, 760);
            this.labelToplamHesap.Margin = new System.Windows.Forms.Padding(0);
            this.labelToplamHesap.Name = "labelToplamHesap";
            this.labelToplamHesap.Size = new System.Drawing.Size(152, 33);
            this.labelToplamHesap.TabIndex = 102;
            this.labelToplamHesap.Text = "0,00";
            this.labelToplamHesap.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelKalanText
            // 
            this.labelKalanText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelKalanText.AutoSize = true;
            this.labelKalanText.BackColor = System.Drawing.Color.Transparent;
            this.labelKalanText.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelKalanText.ForeColor = System.Drawing.SystemColors.Window;
            this.labelKalanText.Location = new System.Drawing.Point(1095, 760);
            this.labelKalanText.Name = "labelKalanText";
            this.labelKalanText.Size = new System.Drawing.Size(104, 33);
            this.labelKalanText.TabIndex = 103;
            this.labelKalanText.Text = "Toplam:";
            this.labelKalanText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonYazdir
            // 
            this.buttonYazdir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonYazdir.BackColor = System.Drawing.SystemColors.Window;
            this.buttonYazdir.Enabled = false;
            this.buttonYazdir.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold);
            this.buttonYazdir.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonYazdir.Location = new System.Drawing.Point(934, 750);
            this.buttonYazdir.Name = "buttonYazdir";
            this.buttonYazdir.Size = new System.Drawing.Size(120, 55);
            this.buttonYazdir.TabIndex = 104;
            this.buttonYazdir.TabStop = false;
            this.buttonYazdir.Text = "Yazdır";
            this.buttonYazdir.UseVisualStyleBackColor = false;
            this.buttonYazdir.Click += new System.EventHandler(this.buttonYazdir_Click);
            // 
            // keyboardcontrol1
            // 
            this.keyboardcontrol1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.keyboardcontrol1.ForeColor = System.Drawing.SystemColors.Window;
            this.keyboardcontrol1.KeyboardType = KeyboardClassLibrary.BoW.Standard;
            this.keyboardcontrol1.Location = new System.Drawing.Point(4, 535);
            this.keyboardcontrol1.Name = "keyboardcontrol1";
            this.keyboardcontrol1.Size = new System.Drawing.Size(924, 282);
            this.keyboardcontrol1.TabIndex = 50;
            this.keyboardcontrol1.Visible = false;
            this.keyboardcontrol1.UserKeyPressed += new KeyboardClassLibrary.KeyboardDelegate(this.keyboardcontrol1_UserKeyPressed);
            // 
            // AdisyonGoruntuleme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1366, 819);
            this.ControlBox = false;
            this.Controls.Add(this.buttonYazdir);
            this.Controls.Add(this.labelKalanText);
            this.Controls.Add(this.labelToplamHesap);
            this.Controls.Add(this.keyboardcontrol1);
            this.Controls.Add(this.buttonSayfaAzalt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelSayfa);
            this.Controls.Add(this.buttonSayfaArttir);
            this.Controls.Add(this.labelSayfaSayisi);
            this.Controls.Add(this.listAdisyonDetay);
            this.Controls.Add(this.listAdisyon);
            this.Controls.Add(this.comboAdisyonAyar);
            this.Controls.Add(this.textboxAdisyonID);
            this.Controls.Add(this.buttonAzalt);
            this.Controls.Add(this.buttonArttir);
            this.Controls.Add(this.dateBitis);
            this.Controls.Add(this.buttonAdisyonlariGetir);
            this.Controls.Add(this.dateBaslangic);
            this.Controls.Add(this.labelGun);
            this.Controls.Add(this.labelTarih);
            this.Controls.Add(this.labelSaat);
            this.Controls.Add(this.exitButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdisyonGoruntuleme";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "SiparisFormu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AdisyonGoruntuleme_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label labelGun;
        private System.Windows.Forms.Label labelTarih;
        private System.Windows.Forms.Label labelSaat;
        private System.Windows.Forms.Timer timerSaat;
        private System.Windows.Forms.Button buttonAdisyonlariGetir;
        private System.Windows.Forms.Button buttonArttir;
        private System.Windows.Forms.Button buttonAzalt;
        private System.Windows.Forms.TextBox textboxAdisyonID;
        private System.Windows.Forms.DateTimePicker dateBaslangic;
        private System.Windows.Forms.DateTimePicker dateBitis;
        private System.Windows.Forms.ComboBox comboAdisyonAyar;
        private System.Windows.Forms.ListView listAdisyon;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ListView listAdisyonDetay;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Button buttonSayfaArttir;
        private System.Windows.Forms.Button buttonSayfaAzalt;
        private System.Windows.Forms.Label labelSayfaSayisi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelSayfa;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Label labelToplamHesap;
        private System.Windows.Forms.Label labelKalanText;
        private System.Windows.Forms.Button buttonYazdir;
        private KeyboardClassLibrary.Keyboardcontrol keyboardcontrol1;
    }
}