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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Eski İkramlar", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Yeni İkramlar", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Eski Siparişler", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Yeni Siparişler", System.Windows.Forms.HorizontalAlignment.Left);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SiparisMenuFormu));
            this.flowPanelUrunler = new System.Windows.Forms.FlowLayoutPanel();
            this.flowPanelMenuBasliklari = new System.Windows.Forms.FlowLayoutPanel();
            this.labelMasa = new System.Windows.Forms.Label();
            this.labelDepartman = new System.Windows.Forms.Label();
            this.ımageList1 = new System.Windows.Forms.ImageList(this.components);
            this.labelKalanHesap = new System.Windows.Forms.Label();
            this.labelKalan = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonUrunIptal = new System.Windows.Forms.Button();
            this.buttonTasi = new System.Windows.Forms.Button();
            this.buttonMasaDegistir = new System.Windows.Forms.Button();
            this.buttonNotEkle = new System.Windows.Forms.Button();
            this.buttonTamam = new System.Windows.Forms.Button();
            this.buttonHesapOde = new System.Windows.Forms.Button();
            this.buttonTemizle = new System.Windows.Forms.Button();
            this.timerDialogClose = new System.Windows.Forms.Timer(this.components);
            this.AddGroupBox = new System.Windows.Forms.GroupBox();
            this.buttonUrunIkram = new System.Windows.Forms.Button();
            this.buttonPorsiyonSec = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbCoklu = new System.Windows.Forms.GroupBox();
            this.labelCokluAdet = new System.Windows.Forms.Label();
            this.buttonCokluCikar = new System.Windows.Forms.Button();
            this.buttonCokluEkle = new System.Windows.Forms.Button();
            this.labelEklenecekUrun = new System.Windows.Forms.Label();
            this.listUrunFiyat = new ROPv1.MyListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AddGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbCoklu.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowPanelUrunler
            // 
            this.flowPanelUrunler.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowPanelUrunler.AutoScroll = true;
            this.flowPanelUrunler.Location = new System.Drawing.Point(647, 46);
            this.flowPanelUrunler.Name = "flowPanelUrunler";
            this.flowPanelUrunler.Size = new System.Drawing.Size(459, 710);
            this.flowPanelUrunler.TabIndex = 0;
            this.flowPanelUrunler.SizeChanged += new System.EventHandler(this.urunPanelSizeChanged);
            // 
            // flowPanelMenuBasliklari
            // 
            this.flowPanelMenuBasliklari.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowPanelMenuBasliklari.AutoScroll = true;
            this.flowPanelMenuBasliklari.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowPanelMenuBasliklari.Location = new System.Drawing.Point(1115, 10);
            this.flowPanelMenuBasliklari.Name = "flowPanelMenuBasliklari";
            this.flowPanelMenuBasliklari.Size = new System.Drawing.Size(250, 746);
            this.flowPanelMenuBasliklari.TabIndex = 63;
            this.flowPanelMenuBasliklari.WrapContents = false;
            this.flowPanelMenuBasliklari.SizeChanged += new System.EventHandler(this.myPannel_SizeChanged);
            // 
            // labelMasa
            // 
            this.labelMasa.AutoSize = true;
            this.labelMasa.BackColor = System.Drawing.Color.Transparent;
            this.labelMasa.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelMasa.ForeColor = System.Drawing.SystemColors.Window;
            this.labelMasa.Location = new System.Drawing.Point(7, 125);
            this.labelMasa.Name = "labelMasa";
            this.labelMasa.Size = new System.Drawing.Size(83, 33);
            this.labelMasa.TabIndex = 76;
            this.labelMasa.Text = "Masa:";
            this.labelMasa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelDepartman
            // 
            this.labelDepartman.BackColor = System.Drawing.Color.Transparent;
            this.labelDepartman.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelDepartman.ForeColor = System.Drawing.SystemColors.Window;
            this.labelDepartman.Location = new System.Drawing.Point(320, 125);
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
            // labelKalanHesap
            // 
            this.labelKalanHesap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelKalanHesap.BackColor = System.Drawing.Color.Transparent;
            this.labelKalanHesap.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelKalanHesap.ForeColor = System.Drawing.SystemColors.Window;
            this.labelKalanHesap.Location = new System.Drawing.Point(183, 670);
            this.labelKalanHesap.Margin = new System.Windows.Forms.Padding(0);
            this.labelKalanHesap.Name = "labelKalanHesap";
            this.labelKalanHesap.Size = new System.Drawing.Size(451, 33);
            this.labelKalanHesap.TabIndex = 86;
            this.labelKalanHesap.Text = "0,00";
            this.labelKalanHesap.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelKalan
            // 
            this.labelKalan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelKalan.AutoSize = true;
            this.labelKalan.BackColor = System.Drawing.Color.Transparent;
            this.labelKalan.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelKalan.ForeColor = System.Drawing.SystemColors.Window;
            this.labelKalan.Location = new System.Drawing.Point(12, 670);
            this.labelKalan.Name = "labelKalan";
            this.labelKalan.Size = new System.Drawing.Size(165, 33);
            this.labelKalan.TabIndex = 85;
            this.labelKalan.Text = "Kalan Hesap :";
            this.labelKalan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackColor = System.Drawing.SystemColors.Window;
            this.buttonAdd.Enabled = false;
            this.buttonAdd.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonAdd.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonAdd.Image = global::ROPv1.Properties.Resources.addBig;
            this.buttonAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAdd.Location = new System.Drawing.Point(8, 24);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(157, 55);
            this.buttonAdd.TabIndex = 84;
            this.buttonAdd.Text = "Ekle";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonUrunIptal
            // 
            this.buttonUrunIptal.BackColor = System.Drawing.SystemColors.Window;
            this.buttonUrunIptal.Enabled = false;
            this.buttonUrunIptal.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonUrunIptal.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonUrunIptal.Image = global::ROPv1.Properties.Resources.deleteBig;
            this.buttonUrunIptal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonUrunIptal.Location = new System.Drawing.Point(8, 85);
            this.buttonUrunIptal.Name = "buttonUrunIptal";
            this.buttonUrunIptal.Size = new System.Drawing.Size(157, 55);
            this.buttonUrunIptal.TabIndex = 81;
            this.buttonUrunIptal.Text = "İptal";
            this.buttonUrunIptal.UseVisualStyleBackColor = false;
            this.buttonUrunIptal.Click += new System.EventHandler(this.buttonUrunIptal_Click);
            // 
            // buttonTasi
            // 
            this.buttonTasi.BackColor = System.Drawing.SystemColors.Window;
            this.buttonTasi.Enabled = false;
            this.buttonTasi.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.buttonTasi.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonTasi.Image = global::ROPv1.Properties.Resources.tableSmall;
            this.buttonTasi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonTasi.Location = new System.Drawing.Point(5, 579);
            this.buttonTasi.Name = "buttonTasi";
            this.buttonTasi.Size = new System.Drawing.Size(174, 40);
            this.buttonTasi.TabIndex = 83;
            this.buttonTasi.Text = "     Ürün Taşı";
            this.buttonTasi.UseVisualStyleBackColor = false;
            this.buttonTasi.Click += new System.EventHandler(this.buttonTasi_Click);
            // 
            // buttonMasaDegistir
            // 
            this.buttonMasaDegistir.BackColor = System.Drawing.SystemColors.Window;
            this.buttonMasaDegistir.Enabled = false;
            this.buttonMasaDegistir.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonMasaDegistir.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonMasaDegistir.Image = global::ROPv1.Properties.Resources.swap;
            this.buttonMasaDegistir.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonMasaDegistir.Location = new System.Drawing.Point(444, 12);
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
            this.buttonNotEkle.BackColor = System.Drawing.SystemColors.Window;
            this.buttonNotEkle.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonNotEkle.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonNotEkle.Image = global::ROPv1.Properties.Resources.adisyon;
            this.buttonNotEkle.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonNotEkle.Location = new System.Drawing.Point(228, 12);
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
            this.buttonTamam.BackColor = System.Drawing.SystemColors.Window;
            this.buttonTamam.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonTamam.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonTamam.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonTamam.Image = global::ROPv1.Properties.Resources.checkmark;
            this.buttonTamam.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonTamam.Location = new System.Drawing.Point(12, 12);
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
            this.buttonHesapOde.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonHesapOde.BackColor = System.Drawing.SystemColors.Window;
            this.buttonHesapOde.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonHesapOde.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonHesapOde.Image = global::ROPv1.Properties.Resources.pay;
            this.buttonHesapOde.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonHesapOde.Location = new System.Drawing.Point(12, 708);
            this.buttonHesapOde.Name = "buttonHesapOde";
            this.buttonHesapOde.Size = new System.Drawing.Size(622, 48);
            this.buttonHesapOde.TabIndex = 57;
            this.buttonHesapOde.TabStop = false;
            this.buttonHesapOde.Text = "HESAP ÖDEME";
            this.buttonHesapOde.UseVisualStyleBackColor = false;
            this.buttonHesapOde.Click += new System.EventHandler(this.paymentButton_Click);
            // 
            // buttonTemizle
            // 
            this.buttonTemizle.BackColor = System.Drawing.SystemColors.Window;
            this.buttonTemizle.Font = new System.Drawing.Font("Arial", 16.5F, System.Drawing.FontStyle.Bold);
            this.buttonTemizle.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonTemizle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonTemizle.Location = new System.Drawing.Point(5, 625);
            this.buttonTemizle.Name = "buttonTemizle";
            this.buttonTemizle.Size = new System.Drawing.Size(174, 40);
            this.buttonTemizle.TabIndex = 87;
            this.buttonTemizle.Text = "Seçimi Temizle";
            this.buttonTemizle.UseVisualStyleBackColor = false;
            this.buttonTemizle.Click += new System.EventHandler(this.buttonTemizle_Click);
            // 
            // timerDialogClose
            // 
            this.timerDialogClose.Interval = 4000;
            this.timerDialogClose.Tick += new System.EventHandler(this.timerDialogClose_Tick);
            // 
            // AddGroupBox
            // 
            this.AddGroupBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.AddGroupBox.Controls.Add(this.buttonAdd);
            this.AddGroupBox.Controls.Add(this.buttonUrunIkram);
            this.AddGroupBox.Controls.Add(this.buttonUrunIptal);
            this.AddGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddGroupBox.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.AddGroupBox.ForeColor = System.Drawing.SystemColors.Window;
            this.AddGroupBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.AddGroupBox.Location = new System.Drawing.Point(6, 236);
            this.AddGroupBox.Margin = new System.Windows.Forms.Padding(2);
            this.AddGroupBox.Name = "AddGroupBox";
            this.AddGroupBox.Padding = new System.Windows.Forms.Padding(2);
            this.AddGroupBox.Size = new System.Drawing.Size(173, 210);
            this.AddGroupBox.TabIndex = 90;
            this.AddGroupBox.TabStop = false;
            this.AddGroupBox.Text = "Ürün İşlemleri";
            // 
            // buttonUrunIkram
            // 
            this.buttonUrunIkram.BackColor = System.Drawing.SystemColors.Window;
            this.buttonUrunIkram.Enabled = false;
            this.buttonUrunIkram.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonUrunIkram.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonUrunIkram.Image = global::ROPv1.Properties.Resources.icon;
            this.buttonUrunIkram.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonUrunIkram.Location = new System.Drawing.Point(8, 146);
            this.buttonUrunIkram.Name = "buttonUrunIkram";
            this.buttonUrunIkram.Size = new System.Drawing.Size(157, 55);
            this.buttonUrunIkram.TabIndex = 80;
            this.buttonUrunIkram.Text = "İkram";
            this.buttonUrunIkram.UseVisualStyleBackColor = false;
            this.buttonUrunIkram.Click += new System.EventHandler(this.buttonUrunIkram_Click);
            // 
            // buttonPorsiyonSec
            // 
            this.buttonPorsiyonSec.BackColor = System.Drawing.SystemColors.Window;
            this.buttonPorsiyonSec.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonPorsiyonSec.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonPorsiyonSec.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonPorsiyonSec.Location = new System.Drawing.Point(8, 23);
            this.buttonPorsiyonSec.Name = "buttonPorsiyonSec";
            this.buttonPorsiyonSec.Size = new System.Drawing.Size(157, 40);
            this.buttonPorsiyonSec.TabIndex = 84;
            this.buttonPorsiyonSec.Text = "Tam";
            this.buttonPorsiyonSec.UseVisualStyleBackColor = false;
            this.buttonPorsiyonSec.Click += new System.EventHandler(this.buttonPorsiyonSec_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Controls.Add(this.buttonPorsiyonSec);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Window;
            this.groupBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.groupBox1.Location = new System.Drawing.Point(6, 160);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(173, 71);
            this.groupBox1.TabIndex = 91;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Porsiyon Seç";
            // 
            // gbCoklu
            // 
            this.gbCoklu.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.gbCoklu.Controls.Add(this.labelCokluAdet);
            this.gbCoklu.Controls.Add(this.buttonCokluCikar);
            this.gbCoklu.Controls.Add(this.buttonCokluEkle);
            this.gbCoklu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbCoklu.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.gbCoklu.ForeColor = System.Drawing.SystemColors.Window;
            this.gbCoklu.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.gbCoklu.Location = new System.Drawing.Point(6, 450);
            this.gbCoklu.Margin = new System.Windows.Forms.Padding(2);
            this.gbCoklu.Name = "gbCoklu";
            this.gbCoklu.Padding = new System.Windows.Forms.Padding(2);
            this.gbCoklu.Size = new System.Drawing.Size(173, 124);
            this.gbCoklu.TabIndex = 91;
            this.gbCoklu.TabStop = false;
            this.gbCoklu.Text = "İşlem Miktarı";
            // 
            // labelCokluAdet
            // 
            this.labelCokluAdet.BackColor = System.Drawing.Color.Transparent;
            this.labelCokluAdet.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelCokluAdet.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelCokluAdet.ForeColor = System.Drawing.SystemColors.Window;
            this.labelCokluAdet.Location = new System.Drawing.Point(12, 25);
            this.labelCokluAdet.Name = "labelCokluAdet";
            this.labelCokluAdet.Size = new System.Drawing.Size(150, 33);
            this.labelCokluAdet.TabIndex = 94;
            this.labelCokluAdet.Text = "1";
            this.labelCokluAdet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonCokluCikar
            // 
            this.buttonCokluCikar.BackColor = System.Drawing.SystemColors.Window;
            this.buttonCokluCikar.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonCokluCikar.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonCokluCikar.Image = global::ROPv1.Properties.Resources.minus;
            this.buttonCokluCikar.Location = new System.Drawing.Point(8, 61);
            this.buttonCokluCikar.Name = "buttonCokluCikar";
            this.buttonCokluCikar.Size = new System.Drawing.Size(75, 55);
            this.buttonCokluCikar.TabIndex = 86;
            this.buttonCokluCikar.UseVisualStyleBackColor = false;
            this.buttonCokluCikar.Click += new System.EventHandler(this.buttonCokluCikar_Click);
            // 
            // buttonCokluEkle
            // 
            this.buttonCokluEkle.BackColor = System.Drawing.SystemColors.Window;
            this.buttonCokluEkle.Font = new System.Drawing.Font("Arial", 30F, System.Drawing.FontStyle.Bold);
            this.buttonCokluEkle.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonCokluEkle.Image = global::ROPv1.Properties.Resources.addBig;
            this.buttonCokluEkle.Location = new System.Drawing.Point(89, 61);
            this.buttonCokluEkle.Name = "buttonCokluEkle";
            this.buttonCokluEkle.Size = new System.Drawing.Size(75, 55);
            this.buttonCokluEkle.TabIndex = 85;
            this.buttonCokluEkle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonCokluEkle.UseVisualStyleBackColor = false;
            this.buttonCokluEkle.Click += new System.EventHandler(this.buttonCokluEkle_Click);
            // 
            // labelEklenecekUrun
            // 
            this.labelEklenecekUrun.AutoSize = true;
            this.labelEklenecekUrun.BackColor = System.Drawing.Color.Transparent;
            this.labelEklenecekUrun.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelEklenecekUrun.ForeColor = System.Drawing.SystemColors.Window;
            this.labelEklenecekUrun.Location = new System.Drawing.Point(646, 12);
            this.labelEklenecekUrun.Name = "labelEklenecekUrun";
            this.labelEklenecekUrun.Size = new System.Drawing.Size(154, 33);
            this.labelEklenecekUrun.TabIndex = 93;
            this.labelEklenecekUrun.Text = "Ürün Seçiniz";
            this.labelEklenecekUrun.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelEklenecekUrun.TextChanged += new System.EventHandler(this.labelEklenecekUrun_TextChanged);
            // 
            // listUrunFiyat
            // 
            this.listUrunFiyat.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listUrunFiyat.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listUrunFiyat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listUrunFiyat.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader4,
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
            this.listUrunFiyat.Location = new System.Drawing.Point(183, 161);
            this.listUrunFiyat.Margin = new System.Windows.Forms.Padding(0);
            this.listUrunFiyat.Name = "listUrunFiyat";
            this.listUrunFiyat.ShowItemToolTips = true;
            this.listUrunFiyat.Size = new System.Drawing.Size(450, 504);
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
            // columnHeader4
            // 
            this.columnHeader4.Text = "Porsiyon";
            this.columnHeader4.Width = 90;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Siparişler";
            this.columnHeader2.Width = 215;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Fiyatları";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader3.Width = 90;
            // 
            // SiparisMenuFormu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.ControlBox = false;
            this.Controls.Add(this.flowPanelMenuBasliklari);
            this.Controls.Add(this.labelDepartman);
            this.Controls.Add(this.labelEklenecekUrun);
            this.Controls.Add(this.gbCoklu);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.AddGroupBox);
            this.Controls.Add(this.labelKalanHesap);
            this.Controls.Add(this.flowPanelUrunler);
            this.Controls.Add(this.buttonTemizle);
            this.Controls.Add(this.labelKalan);
            this.Controls.Add(this.listUrunFiyat);
            this.Controls.Add(this.buttonTasi);
            this.Controls.Add(this.labelMasa);
            this.Controls.Add(this.buttonMasaDegistir);
            this.Controls.Add(this.buttonNotEkle);
            this.Controls.Add(this.buttonTamam);
            this.Controls.Add(this.buttonHesapOde);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SiparisMenuFormu";
            this.ShowInTaskbar = false;
            this.Text = "SiparisMenuFormu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SiparisMenuFormu_FormClosing);
            this.Load += new System.EventHandler(this.SiparisMenuFormu_Load);
            this.AddGroupBox.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.gbCoklu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowPanelUrunler;
        private System.Windows.Forms.Button buttonHesapOde;
        private System.Windows.Forms.Button buttonTamam;
        private System.Windows.Forms.Button buttonNotEkle;
        private System.Windows.Forms.FlowLayoutPanel flowPanelMenuBasliklari;
        private System.Windows.Forms.Label labelMasa;
        private System.Windows.Forms.Label labelDepartman;
        private System.Windows.Forms.Button buttonUrunIptal;
        private System.Windows.Forms.Button buttonTasi;
        private System.Windows.Forms.ImageList ımageList1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Label labelKalan;
        private System.Windows.Forms.Button buttonTemizle;
        private System.Windows.Forms.Timer timerDialogClose;
        public System.Windows.Forms.Button buttonMasaDegistir;
        public MyListView listUrunFiyat;
        public System.Windows.Forms.Label labelKalanHesap;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.GroupBox AddGroupBox;
        private System.Windows.Forms.Button buttonUrunIkram;
        private System.Windows.Forms.Button buttonPorsiyonSec;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbCoklu;
        private System.Windows.Forms.Label labelCokluAdet;
        private System.Windows.Forms.Button buttonCokluCikar;
        private System.Windows.Forms.Button buttonCokluEkle;
        private System.Windows.Forms.Label labelEklenecekUrun;
    }
}