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
            this.components = new System.ComponentModel.Container();
            this.keyboardcontrol1 = new KeyboardClassLibrary.Keyboardcontrol();
            this.textboxUrunAdi = new System.Windows.Forms.TextBox();
            this.labelUrunAdi = new System.Windows.Forms.Label();
            this.newStokForm = new System.Windows.Forms.GroupBox();
            this.comboBoxMiktarTipi = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.labelUrunMiktari = new System.Windows.Forms.Label();
            this.buttonSaveNewStok = new System.Windows.Forms.Button();
            this.buttonDeleteStok = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxUrunMiktari = new System.Windows.Forms.TextBox();
            this.buttonAddNewStok = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblUrunAra = new System.Windows.Forms.Label();
            this.txtStogaEkle = new System.Windows.Forms.TextBox();
            this.btnStogaEkle = new System.Windows.Forms.Button();
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
            this.textboxUrunAdi.Location = new System.Drawing.Point(12, 56);
            this.textboxUrunAdi.Name = "textboxUrunAdi";
            this.textboxUrunAdi.Size = new System.Drawing.Size(302, 32);
            this.textboxUrunAdi.TabIndex = 1;
            this.textboxUrunAdi.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textboxUrunAdi_KeyDown);
            // 
            // labelUrunAdi
            // 
            this.labelUrunAdi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUrunAdi.AutoSize = true;
            this.labelUrunAdi.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelUrunAdi.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelUrunAdi.Location = new System.Drawing.Point(8, 29);
            this.labelUrunAdi.Name = "labelUrunAdi";
            this.labelUrunAdi.Size = new System.Drawing.Size(103, 24);
            this.labelUrunAdi.TabIndex = 19;
            this.labelUrunAdi.Text = "Ürün Adı:";
            // 
            // newStokForm
            // 
            this.newStokForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.newStokForm.Location = new System.Drawing.Point(750, 5);
            this.newStokForm.Name = "newStokForm";
            this.newStokForm.Size = new System.Drawing.Size(326, 220);
            this.newStokForm.TabIndex = 41;
            this.newStokForm.TabStop = false;
            this.newStokForm.Text = "Yeni Ürün";
            // 
            // comboBoxMiktarTipi
            // 
            this.comboBoxMiktarTipi.ContextMenuStrip = this.contextMenuStrip1;
            this.comboBoxMiktarTipi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBoxMiktarTipi.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.comboBoxMiktarTipi.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.comboBoxMiktarTipi.FormattingEnabled = true;
            this.comboBoxMiktarTipi.Items.AddRange(new object[] {
            "Adet",
            "Kilo",
            "Gram",
            "Cl"});
            this.comboBoxMiktarTipi.Location = new System.Drawing.Point(193, 122);
            this.comboBoxMiktarTipi.Name = "comboBoxMiktarTipi";
            this.comboBoxMiktarTipi.Size = new System.Drawing.Size(121, 32);
            this.comboBoxMiktarTipi.TabIndex = 3;
            this.comboBoxMiktarTipi.Click += new System.EventHandler(this.showMenu);
            this.comboBoxMiktarTipi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxKeyPressed);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // labelUrunMiktari
            // 
            this.labelUrunMiktari.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUrunMiktari.AutoSize = true;
            this.labelUrunMiktari.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelUrunMiktari.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelUrunMiktari.Location = new System.Drawing.Point(8, 95);
            this.labelUrunMiktari.Name = "labelUrunMiktari";
            this.labelUrunMiktari.Size = new System.Drawing.Size(137, 24);
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
            this.buttonSaveNewStok.Location = new System.Drawing.Point(193, 166);
            this.buttonSaveNewStok.Name = "buttonSaveNewStok";
            this.buttonSaveNewStok.Size = new System.Drawing.Size(121, 44);
            this.buttonSaveNewStok.TabIndex = 4;
            this.buttonSaveNewStok.Text = "Kaydet  ";
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
            this.buttonDeleteStok.Location = new System.Drawing.Point(12, 166);
            this.buttonDeleteStok.Name = "buttonDeleteStok";
            this.buttonDeleteStok.Size = new System.Drawing.Size(121, 44);
            this.buttonDeleteStok.TabIndex = 5;
            this.buttonDeleteStok.Text = "Stoğu Sil  ";
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
            this.buttonCancel.Location = new System.Drawing.Point(12, 166);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(121, 44);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "İptal Et    ";
            this.buttonCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Visible = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBoxUrunMiktari
            // 
            this.textBoxUrunMiktari.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUrunMiktari.ContextMenuStrip = this.contextMenuStrip1;
            this.textBoxUrunMiktari.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxUrunMiktari.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBoxUrunMiktari.Location = new System.Drawing.Point(12, 122);
            this.textBoxUrunMiktari.Name = "textBoxUrunMiktari";
            this.textBoxUrunMiktari.Size = new System.Drawing.Size(175, 32);
            this.textBoxUrunMiktari.TabIndex = 2;
            this.textBoxUrunMiktari.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxUrunMiktari_KeyDown);
            this.textBoxUrunMiktari.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxUrunMiktari_KeyPress);
            this.textBoxUrunMiktari.Leave += new System.EventHandler(this.textBoxUrunMiktari_Leave);
            // 
            // buttonAddNewStok
            // 
            this.buttonAddNewStok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddNewStok.BackColor = System.Drawing.SystemColors.Window;
            this.buttonAddNewStok.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonAddNewStok.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonAddNewStok.Image = global::ROPv1.Properties.Resources.add;
            this.buttonAddNewStok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddNewStok.Location = new System.Drawing.Point(762, 293);
            this.buttonAddNewStok.Name = "buttonAddNewStok";
            this.buttonAddNewStok.Size = new System.Drawing.Size(302, 45);
            this.buttonAddNewStok.TabIndex = 9;
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
            this.textBox1.Location = new System.Drawing.Point(118, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(623, 32);
            this.textBox1.TabIndex = 10;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // lblUrunAra
            // 
            this.lblUrunAra.AutoSize = true;
            this.lblUrunAra.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblUrunAra.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblUrunAra.Location = new System.Drawing.Point(7, 11);
            this.lblUrunAra.Name = "lblUrunAra";
            this.lblUrunAra.Size = new System.Drawing.Size(105, 24);
            this.lblUrunAra.TabIndex = 44;
            this.lblUrunAra.Text = "Ürün Ara:";
            // 
            // txtStogaEkle
            // 
            this.txtStogaEkle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStogaEkle.ContextMenuStrip = this.contextMenuStrip1;
            this.txtStogaEkle.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtStogaEkle.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtStogaEkle.Location = new System.Drawing.Point(900, 243);
            this.txtStogaEkle.Name = "txtStogaEkle";
            this.txtStogaEkle.Size = new System.Drawing.Size(164, 32);
            this.txtStogaEkle.TabIndex = 8;
            this.txtStogaEkle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStogaEkle_KeyPress);
            this.txtStogaEkle.Leave += new System.EventHandler(this.txtStogaEkle_Leave);
            // 
            // btnStogaEkle
            // 
            this.btnStogaEkle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStogaEkle.BackColor = System.Drawing.SystemColors.Window;
            this.btnStogaEkle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnStogaEkle.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnStogaEkle.Image = global::ROPv1.Properties.Resources.add;
            this.btnStogaEkle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStogaEkle.Location = new System.Drawing.Point(762, 237);
            this.btnStogaEkle.Name = "btnStogaEkle";
            this.btnStogaEkle.Size = new System.Drawing.Size(132, 44);
            this.btnStogaEkle.TabIndex = 7;
            this.btnStogaEkle.Text = "Stoğa Ekle";
            this.btnStogaEkle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStogaEkle.UseVisualStyleBackColor = false;
            this.btnStogaEkle.Click += new System.EventHandler(this.btnStogaEkle_Click);
            // 
            // myListUrunler
            // 
            this.myListUrunler.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.myListUrunler.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.myListUrunler.BackColor = System.Drawing.SystemColors.Window;
            this.myListUrunler.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnUrunAdi,
            this.columnUrunMiktari,
            this.columnMiktarTipi});
            this.myListUrunler.Font = new System.Drawing.Font("Arial", 18.75F, System.Drawing.FontStyle.Bold);
            this.myListUrunler.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.myListUrunler.FullRowSelect = true;
            this.myListUrunler.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.myListUrunler.HideSelection = false;
            this.myListUrunler.LabelWrap = false;
            this.myListUrunler.Location = new System.Drawing.Point(11, 46);
            this.myListUrunler.MultiSelect = false;
            this.myListUrunler.Name = "myListUrunler";
            this.myListUrunler.Size = new System.Drawing.Size(730, 292);
            this.myListUrunler.TabIndex = 11;
            this.myListUrunler.UseCompatibleStateImageBehavior = false;
            this.myListUrunler.View = System.Windows.Forms.View.Details;
            this.myListUrunler.SelectedIndexChanged += new System.EventHandler(this.myListUrunler_SelectedIndexChanged);
            this.myListUrunler.SizeChanged += new System.EventHandler(this.myListUrunler_SizeChanged);
            this.myListUrunler.MouseUp += new System.Windows.Forms.MouseEventHandler(this.myListUrunler_MouseUp);
            // 
            // columnUrunAdi
            // 
            this.columnUrunAdi.Text = "Ürün Adı";
            // 
            // columnUrunMiktari
            // 
            this.columnUrunMiktari.Text = "Ürün Miktarı";
            this.columnUrunMiktari.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // columnMiktarTipi
            // 
            this.columnMiktarTipi.Text = "Miktar Tipi";
            this.columnMiktarTipi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Stoklar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnStogaEkle);
            this.Controls.Add(this.txtStogaEkle);
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
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox txtStogaEkle;
        private System.Windows.Forms.Button btnStogaEkle;
    }
}
