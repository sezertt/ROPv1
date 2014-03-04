namespace ROPv1
{
    partial class Stoklar
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
            this.textboxUrunAdi = new System.Windows.Forms.TextBox();
            this.labelUrunAdi = new System.Windows.Forms.Label();
            this.newStokForm = new System.Windows.Forms.GroupBox();
            this.comboBoxMiktarTipi = new System.Windows.Forms.ComboBox();
            this.labelUrunMiktari = new System.Windows.Forms.Label();
            this.buttonSaveNewStok = new System.Windows.Forms.Button();
            this.buttonDeleteStok = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxUrunMiktari = new System.Windows.Forms.TextBox();
            this.buttonAddNewStok = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblUrunAra = new System.Windows.Forms.Label();
            this.myListUrunler = new ROPv1.MyListView();
            this.columnUrunAdi = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnUrunMiktari = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnMiktarTipi = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.newStokForm.SuspendLayout();
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
            // textboxUrunAdi
            // 
            this.textboxUrunAdi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxUrunAdi.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textboxUrunAdi.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textboxUrunAdi.Location = new System.Drawing.Point(10, 56);
            this.textboxUrunAdi.Name = "textboxUrunAdi";
            this.textboxUrunAdi.Size = new System.Drawing.Size(310, 32);
            this.textboxUrunAdi.TabIndex = 2;
            this.textboxUrunAdi.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textboxUrunAdi_KeyDown);
            // 
            // labelUrunAdi
            // 
            this.labelUrunAdi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUrunAdi.AutoSize = true;
            this.labelUrunAdi.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelUrunAdi.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelUrunAdi.Location = new System.Drawing.Point(6, 29);
            this.labelUrunAdi.Name = "labelUrunAdi";
            this.labelUrunAdi.Size = new System.Drawing.Size(104, 24);
            this.labelUrunAdi.TabIndex = 19;
            this.labelUrunAdi.Text = "Ürün Adı:";
            // 
            // newStokForm
            // 
            this.newStokForm.BackColor = System.Drawing.Color.White;
            this.newStokForm.Controls.Add(this.comboBoxMiktarTipi);
            this.newStokForm.Controls.Add(this.labelUrunMiktari);
            this.newStokForm.Controls.Add(this.textboxUrunAdi);
            this.newStokForm.Controls.Add(this.labelUrunAdi);
            this.newStokForm.Controls.Add(this.buttonSaveNewStok);
            this.newStokForm.Controls.Add(this.buttonDeleteStok);
            this.newStokForm.Controls.Add(this.buttonCancel);
            this.newStokForm.Controls.Add(this.textBoxUrunMiktari);
            this.newStokForm.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.newStokForm.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.newStokForm.Location = new System.Drawing.Point(747, 5);
            this.newStokForm.Name = "newStokForm";
            this.newStokForm.Size = new System.Drawing.Size(326, 280);
            this.newStokForm.TabIndex = 41;
            this.newStokForm.TabStop = false;
            this.newStokForm.Text = "Yeni Ürün";
            // 
            // comboBoxMiktarTipi
            // 
            this.comboBoxMiktarTipi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMiktarTipi.FormattingEnabled = true;
            this.comboBoxMiktarTipi.Items.AddRange(new object[] {
            "Adet",
            "Kilo",
            "Gram",
            "Cl"});
            this.comboBoxMiktarTipi.Location = new System.Drawing.Point(172, 140);
            this.comboBoxMiktarTipi.Name = "comboBoxMiktarTipi";
            this.comboBoxMiktarTipi.Size = new System.Drawing.Size(107, 30);
            this.comboBoxMiktarTipi.TabIndex = 21;
            this.comboBoxMiktarTipi.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBoxMiktarTipi_KeyDown);
            // 
            // labelUrunMiktari
            // 
            this.labelUrunMiktari.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUrunMiktari.AutoSize = true;
            this.labelUrunMiktari.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelUrunMiktari.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelUrunMiktari.Location = new System.Drawing.Point(6, 112);
            this.labelUrunMiktari.Name = "labelUrunMiktari";
            this.labelUrunMiktari.Size = new System.Drawing.Size(138, 24);
            this.labelUrunMiktari.TabIndex = 19;
            this.labelUrunMiktari.Text = "Ürün Miktarı:";
            // 
            // buttonSaveNewStok
            // 
            this.buttonSaveNewStok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveNewStok.BackColor = System.Drawing.SystemColors.Window;
            this.buttonSaveNewStok.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonSaveNewStok.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonSaveNewStok.Image = global::ROPv1.Properties.Resources.icon;
            this.buttonSaveNewStok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSaveNewStok.Location = new System.Drawing.Point(193, 197);
            this.buttonSaveNewStok.Name = "buttonSaveNewStok";
            this.buttonSaveNewStok.Size = new System.Drawing.Size(110, 45);
            this.buttonSaveNewStok.TabIndex = 6;
            this.buttonSaveNewStok.Text = "Kaydet";
            this.buttonSaveNewStok.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSaveNewStok.UseVisualStyleBackColor = false;
            this.buttonSaveNewStok.Click += new System.EventHandler(this.buttonSaveNewStok_Click);
            // 
            // buttonDeleteStok
            // 
            this.buttonDeleteStok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteStok.BackColor = System.Drawing.SystemColors.Window;
            this.buttonDeleteStok.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonDeleteStok.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonDeleteStok.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonDeleteStok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDeleteStok.Location = new System.Drawing.Point(23, 196);
            this.buttonDeleteStok.Name = "buttonDeleteStok";
            this.buttonDeleteStok.Size = new System.Drawing.Size(110, 45);
            this.buttonDeleteStok.TabIndex = 5;
            this.buttonDeleteStok.Text = "Stoğu Sil";
            this.buttonDeleteStok.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonDeleteStok.UseVisualStyleBackColor = false;
            this.buttonDeleteStok.Click += new System.EventHandler(this.buttonDeleteStok_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.BackColor = System.Drawing.SystemColors.Window;
            this.buttonCancel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonCancel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonCancel.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCancel.Location = new System.Drawing.Point(23, 197);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(110, 44);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "İptal Et  ";
            this.buttonCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBoxUrunMiktari
            // 
            this.textBoxUrunMiktari.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUrunMiktari.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxUrunMiktari.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBoxUrunMiktari.Location = new System.Drawing.Point(10, 139);
            this.textBoxUrunMiktari.Name = "textBoxUrunMiktari";
            this.textBoxUrunMiktari.Size = new System.Drawing.Size(138, 32);
            this.textBoxUrunMiktari.TabIndex = 20;
            this.textBoxUrunMiktari.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxUrunMiktari_KeyDown);
            this.textBoxUrunMiktari.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxUrunMiktari_KeyPress);
            // 
            // buttonAddNewStok
            // 
            this.buttonAddNewStok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddNewStok.BackColor = System.Drawing.SystemColors.Window;
            this.buttonAddNewStok.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonAddNewStok.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonAddNewStok.Image = global::ROPv1.Properties.Resources.add;
            this.buttonAddNewStok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddNewStok.Location = new System.Drawing.Point(5, 293);
            this.buttonAddNewStok.Name = "buttonAddNewStok";
            this.buttonAddNewStok.Size = new System.Drawing.Size(196, 45);
            this.buttonAddNewStok.TabIndex = 39;
            this.buttonAddNewStok.Text = "      Yeni Ürün Ekle";
            this.buttonAddNewStok.UseVisualStyleBackColor = false;
            this.buttonAddNewStok.Click += new System.EventHandler(this.buttonAddNewStok_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBox1.Location = new System.Drawing.Point(119, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(599, 32);
            this.textBox1.TabIndex = 43;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // lblUrunAra
            // 
            this.lblUrunAra.AutoSize = true;
            this.lblUrunAra.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblUrunAra.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblUrunAra.Location = new System.Drawing.Point(7, 11);
            this.lblUrunAra.Name = "lblUrunAra";
            this.lblUrunAra.Size = new System.Drawing.Size(106, 24);
            this.lblUrunAra.TabIndex = 44;
            this.lblUrunAra.Text = "Ürün Ara:";
            // 
            // myListUrunler
            // 
            this.myListUrunler.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.myListUrunler.BackColor = System.Drawing.SystemColors.Window;
            this.myListUrunler.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnUrunAdi,
            this.columnUrunMiktari,
            this.columnMiktarTipi});
            this.myListUrunler.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.myListUrunler.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.myListUrunler.FullRowSelect = true;
            this.myListUrunler.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.myListUrunler.Location = new System.Drawing.Point(11, 51);
            this.myListUrunler.MultiSelect = false;
            this.myListUrunler.Name = "myListUrunler";
            this.myListUrunler.Size = new System.Drawing.Size(707, 234);
            this.myListUrunler.TabIndex = 42;
            this.myListUrunler.UseCompatibleStateImageBehavior = false;
            this.myListUrunler.View = System.Windows.Forms.View.Details;
            this.myListUrunler.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnUrunAdi
            // 
            this.columnUrunAdi.Text = "Ürün Adı";
            this.columnUrunAdi.Width = 200;
            // 
            // columnUrunMiktari
            // 
            this.columnUrunMiktari.Text = "Ürün Miktarı";
            // 
            // columnMiktarTipi
            // 
            this.columnMiktarTipi.Text = "Miktar Tipi";
            // 
            // Stoklar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblUrunAra);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.myListUrunler);
            this.Controls.Add(this.keyboardcontrol1);
            this.Controls.Add(this.buttonAddNewStok);
            this.Controls.Add(this.newStokForm);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.Name = "Stoklar";
            this.Size = new System.Drawing.Size(1085, 626);
            this.Load += new System.EventHandler(this.Stoklar_Load);
            this.newStokForm.ResumeLayout(false);
            this.newStokForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private KeyboardClassLibrary.Keyboardcontrol keyboardcontrol1;
        private System.Windows.Forms.Button buttonAddNewStok;
        private System.Windows.Forms.TextBox textboxUrunAdi;
        private System.Windows.Forms.Label labelUrunAdi;
        private System.Windows.Forms.Button buttonSaveNewStok;
        private System.Windows.Forms.Button buttonDeleteStok;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelUrunMiktari;
        private System.Windows.Forms.ComboBox comboBoxMiktarTipi;
        private System.Windows.Forms.TextBox textBoxUrunMiktari;
        private MyListView myListUrunler;
        private System.Windows.Forms.ColumnHeader columnUrunMiktari;
        private System.Windows.Forms.ColumnHeader columnUrunAdi;
        private System.Windows.Forms.ColumnHeader columnMiktarTipi;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblUrunAra;
        private System.Windows.Forms.GroupBox newStokForm;
    }
}
