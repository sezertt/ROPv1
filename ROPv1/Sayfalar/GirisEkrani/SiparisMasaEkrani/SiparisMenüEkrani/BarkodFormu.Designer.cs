namespace ROPv1
{
    partial class BarkodFormu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BarkodFormu));
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonNO = new System.Windows.Forms.Button();
            this.pinboardcontrol21 = new PinboardClassLibrary.Pinboardcontrol2();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.textBoxBarkod = new System.Windows.Forms.TextBox();
            this.labelEklenecekUrun = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelUrun2Ad = new System.Windows.Forms.Label();
            this.labelUrun3Ad = new System.Windows.Forms.Label();
            this.labelUrun4Ad = new System.Windows.Forms.Label();
            this.labelUrun5Ad = new System.Windows.Forms.Label();
            this.labelUrun6Ad = new System.Windows.Forms.Label();
            this.labelUrun2KiloAdet = new System.Windows.Forms.Label();
            this.labelUrun3KiloAdet = new System.Windows.Forms.Label();
            this.labelUrun4KiloAdet = new System.Windows.Forms.Label();
            this.labelUrun5KiloAdet = new System.Windows.Forms.Label();
            this.labelUrun6KiloAdet = new System.Windows.Forms.Label();
            this.labelUrun2Fiyat = new System.Windows.Forms.Label();
            this.labelUrun3Fiyat = new System.Windows.Forms.Label();
            this.labelUrun4Fiyat = new System.Windows.Forms.Label();
            this.labelUrun5Fiyat = new System.Windows.Forms.Label();
            this.labelUrun6Fiyat = new System.Windows.Forms.Label();
            this.buttonUrun2Cancel = new System.Windows.Forms.Button();
            this.buttonUrun3Cancel = new System.Windows.Forms.Button();
            this.buttonUrun4Cancel = new System.Windows.Forms.Button();
            this.buttonUrun5Cancel = new System.Windows.Forms.Button();
            this.buttonUrun6Cancel = new System.Windows.Forms.Button();
            this.labelUrun1Fiyat = new System.Windows.Forms.Label();
            this.labelUrun1KiloAdet = new System.Windows.Forms.Label();
            this.buttonUrun1Cancel = new System.Windows.Forms.Button();
            this.labelUrun1Ad = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOK.BackColor = System.Drawing.SystemColors.Window;
            this.buttonOK.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonOK.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonOK.Image = global::ROPv1.Properties.Resources.icon;
            this.buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOK.Location = new System.Drawing.Point(170, 292);
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
            this.buttonNO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonNO.BackColor = System.Drawing.SystemColors.Window;
            this.buttonNO.DialogResult = System.Windows.Forms.DialogResult.No;
            this.buttonNO.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonNO.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonNO.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonNO.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonNO.Location = new System.Drawing.Point(10, 293);
            this.buttonNO.Name = "buttonNO";
            this.buttonNO.Size = new System.Drawing.Size(125, 50);
            this.buttonNO.TabIndex = 3;
            this.buttonNO.Text = "İptal    ";
            this.buttonNO.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonNO.UseVisualStyleBackColor = false;
            // 
            // pinboardcontrol21
            // 
            this.pinboardcontrol21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pinboardcontrol21.KeyboardType = PinboardClassLibrary.BoW.Standard;
            this.pinboardcontrol21.Location = new System.Drawing.Point(6, 51);
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
            // textBoxBarkod
            // 
            this.textBoxBarkod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxBarkod.ContextMenuStrip = this.contextMenuStrip1;
            this.textBoxBarkod.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.textBoxBarkod.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBoxBarkod.Location = new System.Drawing.Point(12, 14);
            this.textBoxBarkod.MaxLength = 13;
            this.textBoxBarkod.Name = "textBoxBarkod";
            this.textBoxBarkod.Size = new System.Drawing.Size(283, 32);
            this.textBoxBarkod.TabIndex = 84;
            this.textBoxBarkod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxBarkod.TextChanged += new System.EventHandler(this.textBoxBarkod_TextChanged);
            this.textBoxBarkod.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxKG_KeyPress);
            // 
            // labelEklenecekUrun
            // 
            this.labelEklenecekUrun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelEklenecekUrun.AutoSize = true;
            this.labelEklenecekUrun.BackColor = System.Drawing.Color.Transparent;
            this.labelEklenecekUrun.Font = new System.Drawing.Font("Calibri", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelEklenecekUrun.ForeColor = System.Drawing.SystemColors.Window;
            this.labelEklenecekUrun.Location = new System.Drawing.Point(353, 13);
            this.labelEklenecekUrun.Name = "labelEklenecekUrun";
            this.labelEklenecekUrun.Size = new System.Drawing.Size(354, 33);
            this.labelEklenecekUrun.TabIndex = 94;
            this.labelEklenecekUrun.Text = "Ürün Adı                                        ";
            this.labelEklenecekUrun.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Calibri", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(701, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 33);
            this.label1.TabIndex = 95;
            this.label1.Text = "Adet/Kilo";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Calibri", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.SystemColors.Window;
            this.label2.Location = new System.Drawing.Point(819, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 33);
            this.label2.TabIndex = 96;
            this.label2.Text = "Fiyat       ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelUrun2Ad
            // 
            this.labelUrun2Ad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUrun2Ad.BackColor = System.Drawing.Color.Transparent;
            this.labelUrun2Ad.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold);
            this.labelUrun2Ad.ForeColor = System.Drawing.SystemColors.Window;
            this.labelUrun2Ad.Location = new System.Drawing.Point(359, 100);
            this.labelUrun2Ad.Name = "labelUrun2Ad";
            this.labelUrun2Ad.Size = new System.Drawing.Size(340, 45);
            this.labelUrun2Ad.TabIndex = 98;
            this.labelUrun2Ad.Text = "1";
            this.labelUrun2Ad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelUrun2Ad.UseCompatibleTextRendering = true;
            this.labelUrun2Ad.Visible = false;
            // 
            // labelUrun3Ad
            // 
            this.labelUrun3Ad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUrun3Ad.BackColor = System.Drawing.Color.Transparent;
            this.labelUrun3Ad.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold);
            this.labelUrun3Ad.ForeColor = System.Drawing.SystemColors.Window;
            this.labelUrun3Ad.Location = new System.Drawing.Point(359, 150);
            this.labelUrun3Ad.Name = "labelUrun3Ad";
            this.labelUrun3Ad.Size = new System.Drawing.Size(340, 45);
            this.labelUrun3Ad.TabIndex = 99;
            this.labelUrun3Ad.Text = "1";
            this.labelUrun3Ad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelUrun3Ad.UseCompatibleTextRendering = true;
            this.labelUrun3Ad.Visible = false;
            // 
            // labelUrun4Ad
            // 
            this.labelUrun4Ad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUrun4Ad.BackColor = System.Drawing.Color.Transparent;
            this.labelUrun4Ad.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold);
            this.labelUrun4Ad.ForeColor = System.Drawing.SystemColors.Window;
            this.labelUrun4Ad.Location = new System.Drawing.Point(359, 200);
            this.labelUrun4Ad.Name = "labelUrun4Ad";
            this.labelUrun4Ad.Size = new System.Drawing.Size(340, 45);
            this.labelUrun4Ad.TabIndex = 100;
            this.labelUrun4Ad.Text = "1";
            this.labelUrun4Ad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelUrun4Ad.UseCompatibleTextRendering = true;
            this.labelUrun4Ad.Visible = false;
            // 
            // labelUrun5Ad
            // 
            this.labelUrun5Ad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUrun5Ad.BackColor = System.Drawing.Color.Transparent;
            this.labelUrun5Ad.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold);
            this.labelUrun5Ad.ForeColor = System.Drawing.SystemColors.Window;
            this.labelUrun5Ad.Location = new System.Drawing.Point(359, 250);
            this.labelUrun5Ad.Name = "labelUrun5Ad";
            this.labelUrun5Ad.Size = new System.Drawing.Size(340, 45);
            this.labelUrun5Ad.TabIndex = 101;
            this.labelUrun5Ad.Text = "1";
            this.labelUrun5Ad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelUrun5Ad.UseCompatibleTextRendering = true;
            this.labelUrun5Ad.Visible = false;
            // 
            // labelUrun6Ad
            // 
            this.labelUrun6Ad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUrun6Ad.BackColor = System.Drawing.Color.Transparent;
            this.labelUrun6Ad.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold);
            this.labelUrun6Ad.ForeColor = System.Drawing.SystemColors.Window;
            this.labelUrun6Ad.Location = new System.Drawing.Point(359, 300);
            this.labelUrun6Ad.Name = "labelUrun6Ad";
            this.labelUrun6Ad.Size = new System.Drawing.Size(340, 45);
            this.labelUrun6Ad.TabIndex = 102;
            this.labelUrun6Ad.Text = "1";
            this.labelUrun6Ad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelUrun6Ad.UseCompatibleTextRendering = true;
            this.labelUrun6Ad.Visible = false;
            // 
            // labelUrun2KiloAdet
            // 
            this.labelUrun2KiloAdet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUrun2KiloAdet.BackColor = System.Drawing.Color.Transparent;
            this.labelUrun2KiloAdet.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold);
            this.labelUrun2KiloAdet.ForeColor = System.Drawing.SystemColors.Window;
            this.labelUrun2KiloAdet.Location = new System.Drawing.Point(707, 100);
            this.labelUrun2KiloAdet.Name = "labelUrun2KiloAdet";
            this.labelUrun2KiloAdet.Size = new System.Drawing.Size(110, 45);
            this.labelUrun2KiloAdet.TabIndex = 104;
            this.labelUrun2KiloAdet.Text = "1";
            this.labelUrun2KiloAdet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelUrun2KiloAdet.UseCompatibleTextRendering = true;
            this.labelUrun2KiloAdet.Visible = false;
            // 
            // labelUrun3KiloAdet
            // 
            this.labelUrun3KiloAdet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUrun3KiloAdet.BackColor = System.Drawing.Color.Transparent;
            this.labelUrun3KiloAdet.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold);
            this.labelUrun3KiloAdet.ForeColor = System.Drawing.SystemColors.Window;
            this.labelUrun3KiloAdet.Location = new System.Drawing.Point(707, 150);
            this.labelUrun3KiloAdet.Name = "labelUrun3KiloAdet";
            this.labelUrun3KiloAdet.Size = new System.Drawing.Size(110, 45);
            this.labelUrun3KiloAdet.TabIndex = 105;
            this.labelUrun3KiloAdet.Text = "1";
            this.labelUrun3KiloAdet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelUrun3KiloAdet.UseCompatibleTextRendering = true;
            this.labelUrun3KiloAdet.Visible = false;
            // 
            // labelUrun4KiloAdet
            // 
            this.labelUrun4KiloAdet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUrun4KiloAdet.BackColor = System.Drawing.Color.Transparent;
            this.labelUrun4KiloAdet.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold);
            this.labelUrun4KiloAdet.ForeColor = System.Drawing.SystemColors.Window;
            this.labelUrun4KiloAdet.Location = new System.Drawing.Point(707, 200);
            this.labelUrun4KiloAdet.Name = "labelUrun4KiloAdet";
            this.labelUrun4KiloAdet.Size = new System.Drawing.Size(110, 45);
            this.labelUrun4KiloAdet.TabIndex = 106;
            this.labelUrun4KiloAdet.Text = "1";
            this.labelUrun4KiloAdet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelUrun4KiloAdet.UseCompatibleTextRendering = true;
            this.labelUrun4KiloAdet.Visible = false;
            // 
            // labelUrun5KiloAdet
            // 
            this.labelUrun5KiloAdet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUrun5KiloAdet.BackColor = System.Drawing.Color.Transparent;
            this.labelUrun5KiloAdet.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold);
            this.labelUrun5KiloAdet.ForeColor = System.Drawing.SystemColors.Window;
            this.labelUrun5KiloAdet.Location = new System.Drawing.Point(707, 250);
            this.labelUrun5KiloAdet.Name = "labelUrun5KiloAdet";
            this.labelUrun5KiloAdet.Size = new System.Drawing.Size(110, 45);
            this.labelUrun5KiloAdet.TabIndex = 107;
            this.labelUrun5KiloAdet.Text = "1";
            this.labelUrun5KiloAdet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelUrun5KiloAdet.UseCompatibleTextRendering = true;
            this.labelUrun5KiloAdet.Visible = false;
            // 
            // labelUrun6KiloAdet
            // 
            this.labelUrun6KiloAdet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUrun6KiloAdet.BackColor = System.Drawing.Color.Transparent;
            this.labelUrun6KiloAdet.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold);
            this.labelUrun6KiloAdet.ForeColor = System.Drawing.SystemColors.Window;
            this.labelUrun6KiloAdet.Location = new System.Drawing.Point(707, 300);
            this.labelUrun6KiloAdet.Name = "labelUrun6KiloAdet";
            this.labelUrun6KiloAdet.Size = new System.Drawing.Size(110, 45);
            this.labelUrun6KiloAdet.TabIndex = 108;
            this.labelUrun6KiloAdet.Text = "1";
            this.labelUrun6KiloAdet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelUrun6KiloAdet.UseCompatibleTextRendering = true;
            this.labelUrun6KiloAdet.Visible = false;
            // 
            // labelUrun2Fiyat
            // 
            this.labelUrun2Fiyat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUrun2Fiyat.BackColor = System.Drawing.Color.Transparent;
            this.labelUrun2Fiyat.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold);
            this.labelUrun2Fiyat.ForeColor = System.Drawing.SystemColors.Window;
            this.labelUrun2Fiyat.Location = new System.Drawing.Point(825, 100);
            this.labelUrun2Fiyat.Name = "labelUrun2Fiyat";
            this.labelUrun2Fiyat.Size = new System.Drawing.Size(100, 45);
            this.labelUrun2Fiyat.TabIndex = 110;
            this.labelUrun2Fiyat.Text = "1";
            this.labelUrun2Fiyat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelUrun2Fiyat.UseCompatibleTextRendering = true;
            this.labelUrun2Fiyat.Visible = false;
            // 
            // labelUrun3Fiyat
            // 
            this.labelUrun3Fiyat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUrun3Fiyat.BackColor = System.Drawing.Color.Transparent;
            this.labelUrun3Fiyat.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold);
            this.labelUrun3Fiyat.ForeColor = System.Drawing.SystemColors.Window;
            this.labelUrun3Fiyat.Location = new System.Drawing.Point(825, 150);
            this.labelUrun3Fiyat.Name = "labelUrun3Fiyat";
            this.labelUrun3Fiyat.Size = new System.Drawing.Size(100, 45);
            this.labelUrun3Fiyat.TabIndex = 111;
            this.labelUrun3Fiyat.Text = "1";
            this.labelUrun3Fiyat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelUrun3Fiyat.UseCompatibleTextRendering = true;
            this.labelUrun3Fiyat.Visible = false;
            // 
            // labelUrun4Fiyat
            // 
            this.labelUrun4Fiyat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUrun4Fiyat.BackColor = System.Drawing.Color.Transparent;
            this.labelUrun4Fiyat.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold);
            this.labelUrun4Fiyat.ForeColor = System.Drawing.SystemColors.Window;
            this.labelUrun4Fiyat.Location = new System.Drawing.Point(825, 200);
            this.labelUrun4Fiyat.Name = "labelUrun4Fiyat";
            this.labelUrun4Fiyat.Size = new System.Drawing.Size(100, 45);
            this.labelUrun4Fiyat.TabIndex = 112;
            this.labelUrun4Fiyat.Text = "1";
            this.labelUrun4Fiyat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelUrun4Fiyat.UseCompatibleTextRendering = true;
            this.labelUrun4Fiyat.Visible = false;
            // 
            // labelUrun5Fiyat
            // 
            this.labelUrun5Fiyat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUrun5Fiyat.BackColor = System.Drawing.Color.Transparent;
            this.labelUrun5Fiyat.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold);
            this.labelUrun5Fiyat.ForeColor = System.Drawing.SystemColors.Window;
            this.labelUrun5Fiyat.Location = new System.Drawing.Point(825, 250);
            this.labelUrun5Fiyat.Name = "labelUrun5Fiyat";
            this.labelUrun5Fiyat.Size = new System.Drawing.Size(100, 45);
            this.labelUrun5Fiyat.TabIndex = 113;
            this.labelUrun5Fiyat.Text = "1";
            this.labelUrun5Fiyat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelUrun5Fiyat.UseCompatibleTextRendering = true;
            this.labelUrun5Fiyat.Visible = false;
            // 
            // labelUrun6Fiyat
            // 
            this.labelUrun6Fiyat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUrun6Fiyat.BackColor = System.Drawing.Color.Transparent;
            this.labelUrun6Fiyat.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold);
            this.labelUrun6Fiyat.ForeColor = System.Drawing.SystemColors.Window;
            this.labelUrun6Fiyat.Location = new System.Drawing.Point(825, 300);
            this.labelUrun6Fiyat.Name = "labelUrun6Fiyat";
            this.labelUrun6Fiyat.Size = new System.Drawing.Size(100, 45);
            this.labelUrun6Fiyat.TabIndex = 114;
            this.labelUrun6Fiyat.Text = "1";
            this.labelUrun6Fiyat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelUrun6Fiyat.UseCompatibleTextRendering = true;
            this.labelUrun6Fiyat.Visible = false;
            // 
            // buttonUrun2Cancel
            // 
            this.buttonUrun2Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUrun2Cancel.BackColor = System.Drawing.SystemColors.Window;
            this.buttonUrun2Cancel.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonUrun2Cancel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonUrun2Cancel.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonUrun2Cancel.Location = new System.Drawing.Point(308, 104);
            this.buttonUrun2Cancel.Name = "buttonUrun2Cancel";
            this.buttonUrun2Cancel.Size = new System.Drawing.Size(39, 37);
            this.buttonUrun2Cancel.TabIndex = 116;
            this.buttonUrun2Cancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonUrun2Cancel.UseVisualStyleBackColor = false;
            this.buttonUrun2Cancel.Visible = false;
            this.buttonUrun2Cancel.Click += new System.EventHandler(this.buttonUrun2Cancel_Click);
            // 
            // buttonUrun3Cancel
            // 
            this.buttonUrun3Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUrun3Cancel.BackColor = System.Drawing.SystemColors.Window;
            this.buttonUrun3Cancel.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonUrun3Cancel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonUrun3Cancel.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonUrun3Cancel.Location = new System.Drawing.Point(308, 154);
            this.buttonUrun3Cancel.Name = "buttonUrun3Cancel";
            this.buttonUrun3Cancel.Size = new System.Drawing.Size(39, 37);
            this.buttonUrun3Cancel.TabIndex = 117;
            this.buttonUrun3Cancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonUrun3Cancel.UseVisualStyleBackColor = false;
            this.buttonUrun3Cancel.Visible = false;
            this.buttonUrun3Cancel.Click += new System.EventHandler(this.buttonUrun3Cancel_Click);
            // 
            // buttonUrun4Cancel
            // 
            this.buttonUrun4Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUrun4Cancel.BackColor = System.Drawing.SystemColors.Window;
            this.buttonUrun4Cancel.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonUrun4Cancel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonUrun4Cancel.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonUrun4Cancel.Location = new System.Drawing.Point(308, 204);
            this.buttonUrun4Cancel.Name = "buttonUrun4Cancel";
            this.buttonUrun4Cancel.Size = new System.Drawing.Size(39, 37);
            this.buttonUrun4Cancel.TabIndex = 118;
            this.buttonUrun4Cancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonUrun4Cancel.UseVisualStyleBackColor = false;
            this.buttonUrun4Cancel.Visible = false;
            this.buttonUrun4Cancel.Click += new System.EventHandler(this.buttonUrun4Cancel_Click);
            // 
            // buttonUrun5Cancel
            // 
            this.buttonUrun5Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUrun5Cancel.BackColor = System.Drawing.SystemColors.Window;
            this.buttonUrun5Cancel.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonUrun5Cancel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonUrun5Cancel.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonUrun5Cancel.Location = new System.Drawing.Point(308, 254);
            this.buttonUrun5Cancel.Name = "buttonUrun5Cancel";
            this.buttonUrun5Cancel.Size = new System.Drawing.Size(39, 37);
            this.buttonUrun5Cancel.TabIndex = 119;
            this.buttonUrun5Cancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonUrun5Cancel.UseVisualStyleBackColor = false;
            this.buttonUrun5Cancel.Visible = false;
            this.buttonUrun5Cancel.Click += new System.EventHandler(this.buttonUrun5Cancel_Click);
            // 
            // buttonUrun6Cancel
            // 
            this.buttonUrun6Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUrun6Cancel.BackColor = System.Drawing.SystemColors.Window;
            this.buttonUrun6Cancel.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonUrun6Cancel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonUrun6Cancel.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonUrun6Cancel.Location = new System.Drawing.Point(308, 304);
            this.buttonUrun6Cancel.Name = "buttonUrun6Cancel";
            this.buttonUrun6Cancel.Size = new System.Drawing.Size(39, 37);
            this.buttonUrun6Cancel.TabIndex = 120;
            this.buttonUrun6Cancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonUrun6Cancel.UseVisualStyleBackColor = false;
            this.buttonUrun6Cancel.Visible = false;
            this.buttonUrun6Cancel.Click += new System.EventHandler(this.buttonUrun6Cancel_Click);
            // 
            // labelUrun1Fiyat
            // 
            this.labelUrun1Fiyat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUrun1Fiyat.BackColor = System.Drawing.Color.Transparent;
            this.labelUrun1Fiyat.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold);
            this.labelUrun1Fiyat.ForeColor = System.Drawing.SystemColors.Window;
            this.labelUrun1Fiyat.Location = new System.Drawing.Point(825, 50);
            this.labelUrun1Fiyat.Name = "labelUrun1Fiyat";
            this.labelUrun1Fiyat.Size = new System.Drawing.Size(100, 45);
            this.labelUrun1Fiyat.TabIndex = 109;
            this.labelUrun1Fiyat.Text = "1";
            this.labelUrun1Fiyat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelUrun1Fiyat.UseCompatibleTextRendering = true;
            this.labelUrun1Fiyat.Visible = false;
            // 
            // labelUrun1KiloAdet
            // 
            this.labelUrun1KiloAdet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUrun1KiloAdet.BackColor = System.Drawing.Color.Transparent;
            this.labelUrun1KiloAdet.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold);
            this.labelUrun1KiloAdet.ForeColor = System.Drawing.SystemColors.Window;
            this.labelUrun1KiloAdet.Location = new System.Drawing.Point(707, 50);
            this.labelUrun1KiloAdet.Name = "labelUrun1KiloAdet";
            this.labelUrun1KiloAdet.Size = new System.Drawing.Size(110, 45);
            this.labelUrun1KiloAdet.TabIndex = 103;
            this.labelUrun1KiloAdet.Text = "1";
            this.labelUrun1KiloAdet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelUrun1KiloAdet.UseCompatibleTextRendering = true;
            this.labelUrun1KiloAdet.Visible = false;
            // 
            // buttonUrun1Cancel
            // 
            this.buttonUrun1Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUrun1Cancel.BackColor = System.Drawing.SystemColors.Window;
            this.buttonUrun1Cancel.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonUrun1Cancel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonUrun1Cancel.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonUrun1Cancel.Location = new System.Drawing.Point(308, 54);
            this.buttonUrun1Cancel.Name = "buttonUrun1Cancel";
            this.buttonUrun1Cancel.Size = new System.Drawing.Size(39, 37);
            this.buttonUrun1Cancel.TabIndex = 115;
            this.buttonUrun1Cancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonUrun1Cancel.UseVisualStyleBackColor = false;
            this.buttonUrun1Cancel.Visible = false;
            this.buttonUrun1Cancel.Click += new System.EventHandler(this.buttonUrun1Cancel_Click);
            // 
            // labelUrun1Ad
            // 
            this.labelUrun1Ad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUrun1Ad.BackColor = System.Drawing.Color.Transparent;
            this.labelUrun1Ad.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold);
            this.labelUrun1Ad.ForeColor = System.Drawing.SystemColors.Window;
            this.labelUrun1Ad.Location = new System.Drawing.Point(359, 50);
            this.labelUrun1Ad.Name = "labelUrun1Ad";
            this.labelUrun1Ad.Size = new System.Drawing.Size(340, 45);
            this.labelUrun1Ad.TabIndex = 97;
            this.labelUrun1Ad.Text = "1";
            this.labelUrun1Ad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelUrun1Ad.UseCompatibleTextRendering = true;
            this.labelUrun1Ad.Visible = false;
            // 
            // BarkodFormu
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(930, 353);
            this.ControlBox = false;
            this.Controls.Add(this.buttonUrun6Cancel);
            this.Controls.Add(this.buttonUrun5Cancel);
            this.Controls.Add(this.buttonUrun4Cancel);
            this.Controls.Add(this.buttonUrun3Cancel);
            this.Controls.Add(this.buttonUrun2Cancel);
            this.Controls.Add(this.buttonUrun1Cancel);
            this.Controls.Add(this.labelUrun6Fiyat);
            this.Controls.Add(this.labelUrun5Fiyat);
            this.Controls.Add(this.labelUrun4Fiyat);
            this.Controls.Add(this.labelUrun3Fiyat);
            this.Controls.Add(this.labelUrun2Fiyat);
            this.Controls.Add(this.labelUrun1Fiyat);
            this.Controls.Add(this.labelUrun6KiloAdet);
            this.Controls.Add(this.labelUrun5KiloAdet);
            this.Controls.Add(this.labelUrun4KiloAdet);
            this.Controls.Add(this.labelUrun3KiloAdet);
            this.Controls.Add(this.labelUrun2KiloAdet);
            this.Controls.Add(this.labelUrun1KiloAdet);
            this.Controls.Add(this.labelUrun6Ad);
            this.Controls.Add(this.labelUrun5Ad);
            this.Controls.Add(this.labelUrun4Ad);
            this.Controls.Add(this.labelUrun3Ad);
            this.Controls.Add(this.labelUrun2Ad);
            this.Controls.Add(this.labelUrun1Ad);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelEklenecekUrun);
            this.Controls.Add(this.textBoxBarkod);
            this.Controls.Add(this.pinboardcontrol21);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonNO);
            this.ForeColor = System.Drawing.SystemColors.Window;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(946, 369);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(946, 369);
            this.Name = "BarkodFormu";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BarkodFormu_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonNO;
        private PinboardClassLibrary.Pinboardcontrol2 pinboardcontrol21;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox textBoxBarkod;
        private System.Windows.Forms.Label labelEklenecekUrun;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelUrun2Ad;
        private System.Windows.Forms.Label labelUrun3Ad;
        private System.Windows.Forms.Label labelUrun4Ad;
        private System.Windows.Forms.Label labelUrun5Ad;
        private System.Windows.Forms.Label labelUrun6Ad;
        private System.Windows.Forms.Label labelUrun2KiloAdet;
        private System.Windows.Forms.Label labelUrun3KiloAdet;
        private System.Windows.Forms.Label labelUrun4KiloAdet;
        private System.Windows.Forms.Label labelUrun5KiloAdet;
        private System.Windows.Forms.Label labelUrun6KiloAdet;
        private System.Windows.Forms.Label labelUrun2Fiyat;
        private System.Windows.Forms.Label labelUrun3Fiyat;
        private System.Windows.Forms.Label labelUrun4Fiyat;
        private System.Windows.Forms.Label labelUrun5Fiyat;
        private System.Windows.Forms.Label labelUrun6Fiyat;
        private System.Windows.Forms.Button buttonUrun2Cancel;
        private System.Windows.Forms.Button buttonUrun3Cancel;
        private System.Windows.Forms.Button buttonUrun4Cancel;
        private System.Windows.Forms.Button buttonUrun5Cancel;
        private System.Windows.Forms.Button buttonUrun6Cancel;
        private System.Windows.Forms.Label labelUrun1Fiyat;
        private System.Windows.Forms.Label labelUrun1KiloAdet;
        private System.Windows.Forms.Button buttonUrun1Cancel;
        private System.Windows.Forms.Label labelUrun1Ad;
    }
}