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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Products));
            this.newProductForm = new System.Windows.Forms.GroupBox();
            this.labelUrunSayisi = new System.Windows.Forms.Label();
            this.labelUrunSayisiYazisi = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboNewKategoriName = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.textboxUrunFiyat = new System.Windows.Forms.TextBox();
            this.textboxUrunName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonSaveNewProduct = new System.Windows.Forms.Button();
            this.buttonDeleteProduct = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonAddNewProduct = new System.Windows.Forms.Button();
            this.keyboardcontrol1 = new KeyboardClassLibrary.Keyboardcontrol();
            this.treeUrunAdi = new System.Windows.Forms.TreeView();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.newProductForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // newProductForm
            // 
            this.newProductForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.newProductForm.BackColor = System.Drawing.Color.White;
            this.newProductForm.Controls.Add(this.labelUrunSayisi);
            this.newProductForm.Controls.Add(this.labelUrunSayisiYazisi);
            this.newProductForm.Controls.Add(this.label1);
            this.newProductForm.Controls.Add(this.comboNewKategoriName);
            this.newProductForm.Controls.Add(this.textboxUrunFiyat);
            this.newProductForm.Controls.Add(this.textboxUrunName);
            this.newProductForm.Controls.Add(this.label7);
            this.newProductForm.Controls.Add(this.label5);
            this.newProductForm.Controls.Add(this.buttonSaveNewProduct);
            this.newProductForm.Controls.Add(this.buttonDeleteProduct);
            this.newProductForm.Controls.Add(this.buttonCancel);
            this.newProductForm.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.newProductForm.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.newProductForm.Location = new System.Drawing.Point(647, 3);
            this.newProductForm.Name = "newProductForm";
            this.newProductForm.Size = new System.Drawing.Size(343, 335);
            this.newProductForm.TabIndex = 35;
            this.newProductForm.TabStop = false;
            this.newProductForm.Text = "Yeni Ürün";
            // 
            // labelUrunSayisi
            // 
            this.labelUrunSayisi.AutoSize = true;
            this.labelUrunSayisi.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelUrunSayisi.Location = new System.Drawing.Point(146, 299);
            this.labelUrunSayisi.Name = "labelUrunSayisi";
            this.labelUrunSayisi.Size = new System.Drawing.Size(22, 24);
            this.labelUrunSayisi.TabIndex = 44;
            this.labelUrunSayisi.Text = "0";
            // 
            // labelUrunSayisiYazisi
            // 
            this.labelUrunSayisiYazisi.AutoSize = true;
            this.labelUrunSayisiYazisi.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelUrunSayisiYazisi.Location = new System.Drawing.Point(10, 299);
            this.labelUrunSayisiYazisi.Name = "labelUrunSayisiYazisi";
            this.labelUrunSayisiYazisi.Size = new System.Drawing.Size(137, 24);
            this.labelUrunSayisiYazisi.TabIndex = 43;
            this.labelUrunSayisiYazisi.Text = "Ürün Sayısı =";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(6, 166);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 24);
            this.label1.TabIndex = 42;
            this.label1.Text = "Kategorisi";
            // 
            // comboNewKategoriName
            // 
            this.comboNewKategoriName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboNewKategoriName.ContextMenuStrip = this.contextMenuStrip1;
            this.comboNewKategoriName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboNewKategoriName.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.comboNewKategoriName.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.comboNewKategoriName.FormattingEnabled = true;
            this.comboNewKategoriName.Location = new System.Drawing.Point(10, 195);
            this.comboNewKategoriName.Name = "comboNewKategoriName";
            this.comboNewKategoriName.Size = new System.Drawing.Size(324, 32);
            this.comboNewKategoriName.TabIndex = 4;
            this.comboNewKategoriName.Click += new System.EventHandler(this.showMenu);
            this.comboNewKategoriName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxKeyPressed);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // textboxUrunFiyat
            // 
            this.textboxUrunFiyat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxUrunFiyat.ContextMenuStrip = this.contextMenuStrip1;
            this.textboxUrunFiyat.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textboxUrunFiyat.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textboxUrunFiyat.Location = new System.Drawing.Point(10, 126);
            this.textboxUrunFiyat.Name = "textboxUrunFiyat";
            this.textboxUrunFiyat.Size = new System.Drawing.Size(324, 32);
            this.textboxUrunFiyat.TabIndex = 3;
            this.textboxUrunFiyat.Enter += new System.EventHandler(this.fiyatGirilcek);
            this.textboxUrunFiyat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyPressedOnPriceText);
            this.textboxUrunFiyat.Leave += new System.EventHandler(this.fiyatGirildi);
            // 
            // textboxUrunName
            // 
            this.textboxUrunName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxUrunName.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textboxUrunName.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textboxUrunName.Location = new System.Drawing.Point(10, 57);
            this.textboxUrunName.Name = "textboxUrunName";
            this.textboxUrunName.Size = new System.Drawing.Size(324, 32);
            this.textboxUrunName.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label7.Location = new System.Drawing.Point(6, 97);
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
            this.label5.Location = new System.Drawing.Point(6, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 24);
            this.label5.TabIndex = 19;
            this.label5.Text = "Ürün Adı:";
            // 
            // buttonSaveNewProduct
            // 
            this.buttonSaveNewProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveNewProduct.BackColor = System.Drawing.SystemColors.Window;
            this.buttonSaveNewProduct.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonSaveNewProduct.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonSaveNewProduct.Image = ((System.Drawing.Image)(resources.GetObject("buttonSaveNewProduct.Image")));
            this.buttonSaveNewProduct.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSaveNewProduct.Location = new System.Drawing.Point(224, 244);
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
            this.buttonDeleteProduct.Location = new System.Drawing.Point(11, 244);
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
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonCancel.BackColor = System.Drawing.SystemColors.Window;
            this.buttonCancel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonCancel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonCancel.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCancel.Location = new System.Drawing.Point(11, 244);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(110, 44);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "İptal Et  ";
            this.buttonCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.cancelNewProduct);
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
            this.keyboardcontrol1.Location = new System.Drawing.Point(0, 344);
            this.keyboardcontrol1.Name = "keyboardcontrol1";
            this.keyboardcontrol1.Size = new System.Drawing.Size(993, 282);
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
            // Products
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonAddNewProduct);
            this.Controls.Add(this.treeUrunAdi);
            this.Controls.Add(this.keyboardcontrol1);
            this.Controls.Add(this.newProductForm);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.Name = "Products";
            this.Size = new System.Drawing.Size(993, 626);
            this.newProductForm.ResumeLayout(false);
            this.newProductForm.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.TextBox textboxUrunFiyat;
        private System.Windows.Forms.TreeView treeUrunAdi;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboNewKategoriName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label labelUrunSayisi;
        private System.Windows.Forms.Label labelUrunSayisiYazisi;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;

    }
}
