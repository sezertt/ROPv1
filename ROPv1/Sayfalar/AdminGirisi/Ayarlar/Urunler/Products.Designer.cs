namespace ROPv1
{
    partial class Products
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
            this.components = new System.ComponentModel.Container();
            this.newProductForm = new System.Windows.Forms.GroupBox();
            this.buttonResim = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboKDV = new System.Windows.Forms.ComboBox();
            this.comboNewKategoriName = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.textboxUrunPorsiyonFiyat = new System.Windows.Forms.TextBox();
            this.textboxUrunName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonSaveNewProduct = new System.Windows.Forms.Button();
            this.buttonDeleteProduct = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelUrunSayisi = new System.Windows.Forms.Label();
            this.labelUrunSayisiYazisi = new System.Windows.Forms.Label();
            this.buttonAddNewProduct = new System.Windows.Forms.Button();
            this.keyboardcontrol1 = new KeyboardClassLibrary.Keyboardcontrol();
            this.treeUrunAdi = new System.Windows.Forms.TreeView();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxUrunAciklamasi = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textboxUrunKiloFiyat = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboTur = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.newProductForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // newProductForm
            // 
            this.newProductForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.newProductForm.BackColor = System.Drawing.Color.White;
            this.newProductForm.Controls.Add(this.label4);
            this.newProductForm.Controls.Add(this.comboTur);
            this.newProductForm.Controls.Add(this.textboxUrunKiloFiyat);
            this.newProductForm.Controls.Add(this.label3);
            this.newProductForm.Controls.Add(this.buttonResim);
            this.newProductForm.Controls.Add(this.label2);
            this.newProductForm.Controls.Add(this.comboKDV);
            this.newProductForm.Controls.Add(this.label1);
            this.newProductForm.Controls.Add(this.comboNewKategoriName);
            this.newProductForm.Controls.Add(this.textboxUrunPorsiyonFiyat);
            this.newProductForm.Controls.Add(this.textboxUrunName);
            this.newProductForm.Controls.Add(this.label7);
            this.newProductForm.Controls.Add(this.label5);
            this.newProductForm.Controls.Add(this.buttonSaveNewProduct);
            this.newProductForm.Controls.Add(this.buttonDeleteProduct);
            this.newProductForm.Controls.Add(this.buttonCancel);
            this.newProductForm.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.newProductForm.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.newProductForm.Location = new System.Drawing.Point(657, 3);
            this.newProductForm.Name = "newProductForm";
            this.newProductForm.Size = new System.Drawing.Size(322, 335);
            this.newProductForm.TabIndex = 35;
            this.newProductForm.TabStop = false;
            this.newProductForm.Text = "Yeni Ürün";
            // 
            // buttonResim
            // 
            this.buttonResim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonResim.BackColor = System.Drawing.SystemColors.Window;
            this.buttonResim.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonResim.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonResim.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonResim.Location = new System.Drawing.Point(8, 244);
            this.buttonResim.Name = "buttonResim";
            this.buttonResim.Size = new System.Drawing.Size(305, 38);
            this.buttonResim.TabIndex = 45;
            this.buttonResim.Text = "Ürün Fotoğrafı";
            this.buttonResim.UseVisualStyleBackColor = false;
            this.buttonResim.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(4, 210);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 24);
            this.label2.TabIndex = 44;
            this.label2.Text = "KDV (%)";
            // 
            // comboKDV
            // 
            this.comboKDV.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboKDV.Font = new System.Drawing.Font("Arial", 15.75F);
            this.comboKDV.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.comboKDV.FormattingEnabled = true;
            this.comboKDV.Items.AddRange(new object[] {
            "1",
            "8",
            "18"});
            this.comboKDV.Location = new System.Drawing.Point(100, 208);
            this.comboKDV.Name = "comboKDV";
            this.comboKDV.Size = new System.Drawing.Size(213, 32);
            this.comboKDV.TabIndex = 43;
            this.comboKDV.Click += new System.EventHandler(this.showMenu);
            this.comboKDV.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxKeyPressed);
            // 
            // comboNewKategoriName
            // 
            this.comboNewKategoriName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboNewKategoriName.ContextMenuStrip = this.contextMenuStrip1;
            this.comboNewKategoriName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboNewKategoriName.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.comboNewKategoriName.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.comboNewKategoriName.FormattingEnabled = true;
            this.comboNewKategoriName.Location = new System.Drawing.Point(118, 171);
            this.comboNewKategoriName.MaxDropDownItems = 20;
            this.comboNewKategoriName.Name = "comboNewKategoriName";
            this.comboNewKategoriName.Size = new System.Drawing.Size(195, 32);
            this.comboNewKategoriName.TabIndex = 4;
            this.comboNewKategoriName.Click += new System.EventHandler(this.showMenu);
            this.comboNewKategoriName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxKeyPressed);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // textboxUrunPorsiyonFiyat
            // 
            this.textboxUrunPorsiyonFiyat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxUrunPorsiyonFiyat.ContextMenuStrip = this.contextMenuStrip1;
            this.textboxUrunPorsiyonFiyat.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textboxUrunPorsiyonFiyat.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textboxUrunPorsiyonFiyat.Location = new System.Drawing.Point(187, 97);
            this.textboxUrunPorsiyonFiyat.Name = "textboxUrunPorsiyonFiyat";
            this.textboxUrunPorsiyonFiyat.Size = new System.Drawing.Size(126, 32);
            this.textboxUrunPorsiyonFiyat.TabIndex = 3;
            this.textboxUrunPorsiyonFiyat.Enter += new System.EventHandler(this.fiyatGirilcek);
            this.textboxUrunPorsiyonFiyat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyPressedOnPriceText);
            this.textboxUrunPorsiyonFiyat.Leave += new System.EventHandler(this.fiyatGirildi);
            // 
            // textboxUrunName
            // 
            this.textboxUrunName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxUrunName.ContextMenuStrip = this.contextMenuStrip1;
            this.textboxUrunName.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textboxUrunName.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textboxUrunName.Location = new System.Drawing.Point(52, 22);
            this.textboxUrunName.MaxLength = 30;
            this.textboxUrunName.Name = "textboxUrunName";
            this.textboxUrunName.Size = new System.Drawing.Size(261, 32);
            this.textboxUrunName.TabIndex = 2;
            this.textboxUrunName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textboxUrunName_KeyPress);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label7.Location = new System.Drawing.Point(4, 100);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(183, 24);
            this.label7.TabIndex = 25;
            this.label7.Text = "1 Porsiyon Fiyatı:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Location = new System.Drawing.Point(4, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 24);
            this.label5.TabIndex = 19;
            this.label5.Text = "Adı:";
            // 
            // buttonSaveNewProduct
            // 
            this.buttonSaveNewProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveNewProduct.BackColor = System.Drawing.SystemColors.Window;
            this.buttonSaveNewProduct.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonSaveNewProduct.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonSaveNewProduct.Image = global::ROPv1.Properties.Resources.icon;
            this.buttonSaveNewProduct.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSaveNewProduct.Location = new System.Drawing.Point(203, 284);
            this.buttonSaveNewProduct.Name = "buttonSaveNewProduct";
            this.buttonSaveNewProduct.Size = new System.Drawing.Size(110, 45);
            this.buttonSaveNewProduct.TabIndex = 6;
            this.buttonSaveNewProduct.Text = "Kaydet";
            this.buttonSaveNewProduct.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSaveNewProduct.UseVisualStyleBackColor = false;
            this.buttonSaveNewProduct.Click += new System.EventHandler(this.saveProductButtonPressed);
            // 
            // buttonDeleteProduct
            // 
            this.buttonDeleteProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteProduct.BackColor = System.Drawing.SystemColors.Window;
            this.buttonDeleteProduct.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonDeleteProduct.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonDeleteProduct.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonDeleteProduct.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDeleteProduct.Location = new System.Drawing.Point(8, 284);
            this.buttonDeleteProduct.Name = "buttonDeleteProduct";
            this.buttonDeleteProduct.Size = new System.Drawing.Size(110, 45);
            this.buttonDeleteProduct.TabIndex = 5;
            this.buttonDeleteProduct.Text = "Ürünü Sil";
            this.buttonDeleteProduct.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonDeleteProduct.UseVisualStyleBackColor = false;
            this.buttonDeleteProduct.Click += new System.EventHandler(this.deleteProduct);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.BackColor = System.Drawing.SystemColors.Window;
            this.buttonCancel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonCancel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonCancel.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCancel.Location = new System.Drawing.Point(8, 284);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(110, 45);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "İptal Et  ";
            this.buttonCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.cancelNewProduct);
            // 
            // labelUrunSayisi
            // 
            this.labelUrunSayisi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelUrunSayisi.AutoSize = true;
            this.labelUrunSayisi.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelUrunSayisi.Location = new System.Drawing.Point(354, 305);
            this.labelUrunSayisi.Name = "labelUrunSayisi";
            this.labelUrunSayisi.Size = new System.Drawing.Size(22, 24);
            this.labelUrunSayisi.TabIndex = 44;
            this.labelUrunSayisi.Text = "0";
            // 
            // labelUrunSayisiYazisi
            // 
            this.labelUrunSayisiYazisi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelUrunSayisiYazisi.AutoSize = true;
            this.labelUrunSayisiYazisi.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelUrunSayisiYazisi.Location = new System.Drawing.Point(218, 305);
            this.labelUrunSayisiYazisi.Name = "labelUrunSayisiYazisi";
            this.labelUrunSayisiYazisi.Size = new System.Drawing.Size(141, 24);
            this.labelUrunSayisiYazisi.TabIndex = 43;
            this.labelUrunSayisiYazisi.Text = "Ürün Sayısı =";
            // 
            // buttonAddNewProduct
            // 
            this.buttonAddNewProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddNewProduct.BackColor = System.Drawing.SystemColors.Window;
            this.buttonAddNewProduct.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonAddNewProduct.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonAddNewProduct.Image = global::ROPv1.Properties.Resources.add;
            this.buttonAddNewProduct.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddNewProduct.Location = new System.Drawing.Point(5, 293);
            this.buttonAddNewProduct.Name = "buttonAddNewProduct";
            this.buttonAddNewProduct.Size = new System.Drawing.Size(196, 45);
            this.buttonAddNewProduct.TabIndex = 7;
            this.buttonAddNewProduct.Text = "      Yeni Ürün Oluştur";
            this.buttonAddNewProduct.UseVisualStyleBackColor = false;
            this.buttonAddNewProduct.Click += new System.EventHandler(this.createNewProductButtonPressed);
            // 
            // keyboardcontrol1
            // 
            this.keyboardcontrol1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.keyboardcontrol1.ForeColor = System.Drawing.SystemColors.Window;
            this.keyboardcontrol1.KeyboardType = KeyboardClassLibrary.BoW.Standard;
            this.keyboardcontrol1.Location = new System.Drawing.Point(0, 382);
            this.keyboardcontrol1.Name = "keyboardcontrol1";
            this.keyboardcontrol1.Size = new System.Drawing.Size(993, 244);
            this.keyboardcontrol1.TabIndex = 32;
            this.keyboardcontrol1.UserKeyPressed += new KeyboardClassLibrary.KeyboardDelegate(this.keyboardcontrol1_UserKeyPressed);
            // 
            // treeUrunAdi
            // 
            this.treeUrunAdi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeUrunAdi.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.treeUrunAdi.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.treeUrunAdi.FullRowSelect = true;
            this.treeUrunAdi.HideSelection = false;
            this.treeUrunAdi.HotTracking = true;
            this.treeUrunAdi.Indent = 10;
            this.treeUrunAdi.ItemHeight = 35;
            this.treeUrunAdi.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.treeUrunAdi.Location = new System.Drawing.Point(5, 5);
            this.treeUrunAdi.Name = "treeUrunAdi";
            this.treeUrunAdi.ShowLines = false;
            this.treeUrunAdi.Size = new System.Drawing.Size(636, 282);
            this.treeUrunAdi.TabIndex = 1;
            this.treeUrunAdi.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.changeProduct);
            this.treeUrunAdi.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.expandOrCollapseNode);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.SystemColors.Window;
            this.button2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button2.Image = global::ROPv1.Properties.Resources.downIcon;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.Location = new System.Drawing.Point(521, 293);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 45);
            this.button2.TabIndex = 37;
            this.button2.TabStop = false;
            this.button2.Text = "Aşağı Taşı";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.modeNodeDown);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.SystemColors.Window;
            this.button1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.Image = global::ROPv1.Properties.Resources.upIcon;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.Location = new System.Drawing.Point(395, 293);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 45);
            this.button1.TabIndex = 36;
            this.button1.TabStop = false;
            this.button1.Text = "Yukarı Taşı";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.moveNodeUp);
            // 
            // textBoxUrunAciklamasi
            // 
            this.textBoxUrunAciklamasi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUrunAciklamasi.ContextMenuStrip = this.contextMenuStrip1;
            this.textBoxUrunAciklamasi.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxUrunAciklamasi.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBoxUrunAciklamasi.Location = new System.Drawing.Point(5, 344);
            this.textBoxUrunAciklamasi.MaxLength = 80;
            this.textBoxUrunAciklamasi.Name = "textBoxUrunAciklamasi";
            this.textBoxUrunAciklamasi.Size = new System.Drawing.Size(974, 32);
            this.textBoxUrunAciklamasi.TabIndex = 45;
            this.textBoxUrunAciklamasi.Text = "Ürün Açıklaması";
            this.textBoxUrunAciklamasi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textboxUrunName_KeyPress);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(4, 174);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 24);
            this.label1.TabIndex = 42;
            this.label1.Text = "Kategorisi";
            // 
            // textboxUrunKiloFiyat
            // 
            this.textboxUrunKiloFiyat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxUrunKiloFiyat.ContextMenuStrip = this.contextMenuStrip1;
            this.textboxUrunKiloFiyat.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textboxUrunKiloFiyat.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textboxUrunKiloFiyat.Location = new System.Drawing.Point(187, 134);
            this.textboxUrunKiloFiyat.Name = "textboxUrunKiloFiyat";
            this.textboxUrunKiloFiyat.Size = new System.Drawing.Size(126, 32);
            this.textboxUrunKiloFiyat.TabIndex = 46;
            this.textboxUrunKiloFiyat.Enter += new System.EventHandler(this.fiyatGirilcek);
            this.textboxUrunKiloFiyat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyPressedOnPriceText);
            this.textboxUrunKiloFiyat.Leave += new System.EventHandler(this.fiyatGirildi);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Location = new System.Drawing.Point(4, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 24);
            this.label3.TabIndex = 47;
            this.label3.Text = "1 Kilogram Fiyatı:";
            // 
            // comboTur
            // 
            this.comboTur.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboTur.ContextMenuStrip = this.contextMenuStrip1;
            this.comboTur.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboTur.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.comboTur.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.comboTur.FormattingEnabled = true;
            this.comboTur.Items.AddRange(new object[] {
            "Porsiyon",
            "Kilogram",
            "Porsiyon & Kilogram"});
            this.comboTur.Location = new System.Drawing.Point(69, 59);
            this.comboTur.MaxDropDownItems = 20;
            this.comboTur.Name = "comboTur";
            this.comboTur.Size = new System.Drawing.Size(244, 32);
            this.comboTur.TabIndex = 48;
            this.comboTur.SelectedIndexChanged += new System.EventHandler(this.comboTur_SelectedIndexChanged);
            this.comboTur.Click += new System.EventHandler(this.showMenu);
            this.comboTur.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxKeyPressed);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label4.Location = new System.Drawing.Point(4, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 24);
            this.label4.TabIndex = 49;
            this.label4.Text = "Türü:";
            // 
            // Products
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxUrunAciklamasi);
            this.Controls.Add(this.labelUrunSayisi);
            this.Controls.Add(this.labelUrunSayisiYazisi);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonAddNewProduct);
            this.Controls.Add(this.treeUrunAdi);
            this.Controls.Add(this.keyboardcontrol1);
            this.Controls.Add(this.newProductForm);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.Name = "Products";
            this.Size = new System.Drawing.Size(993, 626);
            this.Load += new System.EventHandler(this.Products_Load);
            this.newProductForm.ResumeLayout(false);
            this.newProductForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox newProductForm;
        private System.Windows.Forms.TextBox textboxUrunName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonSaveNewProduct;
        private KeyboardClassLibrary.Keyboardcontrol keyboardcontrol1;
        private System.Windows.Forms.Button buttonDeleteProduct;
        private System.Windows.Forms.Button buttonAddNewProduct;
        private System.Windows.Forms.TextBox textboxUrunPorsiyonFiyat;
        private System.Windows.Forms.TreeView treeUrunAdi;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox comboNewKategoriName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label labelUrunSayisi;
        private System.Windows.Forms.Label labelUrunSayisiYazisi;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboKDV;
        private System.Windows.Forms.TextBox textBoxUrunAciklamasi;
        private System.Windows.Forms.Button buttonResim;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboTur;
        private System.Windows.Forms.TextBox textboxUrunKiloFiyat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;

    }
}
