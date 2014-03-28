namespace ROPv1
{
    partial class Receteler
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
            this.myListIcindekiler = new ROPv1.MyListView();
            this.columnUrunAdi = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnUrunMiktari = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnMiktarTipi = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.treeMalzemeler = new System.Windows.Forms.TreeView();
            this.buttonEkle = new System.Windows.Forms.Button();
            this.buttonCikar = new System.Windows.Forms.Button();
            this.newUrunMenuForm = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBoxUrun = new System.Windows.Forms.ComboBox();
            this.textboxFiyat = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonSaveUrun = new System.Windows.Forms.Button();
            this.buttonDeleteUrun = new System.Windows.Forms.Button();
            this.lblFiyat = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.keyboardcontrol1 = new KeyboardClassLibrary.Keyboardcontrol();
            this.ButtonYeniMalzeme = new System.Windows.Forms.Button();
            this.newUrunMenuForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // myListIcindekiler
            // 
            this.myListIcindekiler.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.myListIcindekiler.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.myListIcindekiler.BackColor = System.Drawing.SystemColors.Window;
            this.myListIcindekiler.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnUrunAdi,
            this.columnUrunMiktari,
            this.columnMiktarTipi});
            this.myListIcindekiler.Font = new System.Drawing.Font("Arial", 18.75F, System.Drawing.FontStyle.Bold);
            this.myListIcindekiler.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.myListIcindekiler.FullRowSelect = true;
            this.myListIcindekiler.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.myListIcindekiler.HideSelection = false;
            this.myListIcindekiler.LabelWrap = false;
            this.myListIcindekiler.Location = new System.Drawing.Point(3, 3);
            this.myListIcindekiler.MultiSelect = false;
            this.myListIcindekiler.Name = "myListIcindekiler";
            this.myListIcindekiler.Size = new System.Drawing.Size(290, 409);
            this.myListIcindekiler.TabIndex = 48;
            this.myListIcindekiler.UseCompatibleStateImageBehavior = false;
            this.myListIcindekiler.View = System.Windows.Forms.View.Details;
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
            // treeMalzemeler
            // 
            this.treeMalzemeler.Location = new System.Drawing.Point(585, 3);
            this.treeMalzemeler.Name = "treeMalzemeler";
            this.treeMalzemeler.Size = new System.Drawing.Size(244, 409);
            this.treeMalzemeler.TabIndex = 50;
            // 
            // buttonEkle
            // 
            this.buttonEkle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEkle.BackColor = System.Drawing.SystemColors.Window;
            this.buttonEkle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonEkle.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonEkle.Image = global::ROPv1.Properties.Resources.lefticon;
            this.buttonEkle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEkle.Location = new System.Drawing.Point(10, 161);
            this.buttonEkle.Name = "buttonEkle";
            this.buttonEkle.Size = new System.Drawing.Size(264, 41);
            this.buttonEkle.TabIndex = 51;
            this.buttonEkle.TabStop = false;
            this.buttonEkle.Text = "Malzemeyi Ekle";
            this.buttonEkle.UseVisualStyleBackColor = false;
            // 
            // buttonCikar
            // 
            this.buttonCikar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCikar.BackColor = System.Drawing.SystemColors.Window;
            this.buttonCikar.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonCikar.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonCikar.Image = global::ROPv1.Properties.Resources.righticon;
            this.buttonCikar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonCikar.Location = new System.Drawing.Point(10, 208);
            this.buttonCikar.Name = "buttonCikar";
            this.buttonCikar.Size = new System.Drawing.Size(264, 41);
            this.buttonCikar.TabIndex = 52;
            this.buttonCikar.TabStop = false;
            this.buttonCikar.Text = "Malzemeyi Çıkar";
            this.buttonCikar.UseVisualStyleBackColor = false;
            // 
            // newUrunMenuForm
            // 
            this.newUrunMenuForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.newUrunMenuForm.BackColor = System.Drawing.Color.White;
            this.newUrunMenuForm.Controls.Add(this.comboBox1);
            this.newUrunMenuForm.Controls.Add(this.comboBoxUrun);
            this.newUrunMenuForm.Controls.Add(this.buttonCikar);
            this.newUrunMenuForm.Controls.Add(this.buttonEkle);
            this.newUrunMenuForm.Controls.Add(this.textboxFiyat);
            this.newUrunMenuForm.Controls.Add(this.label5);
            this.newUrunMenuForm.Controls.Add(this.buttonSaveUrun);
            this.newUrunMenuForm.Controls.Add(this.buttonDeleteUrun);
            this.newUrunMenuForm.Controls.Add(this.lblFiyat);
            this.newUrunMenuForm.Controls.Add(this.buttonCancel);
            this.newUrunMenuForm.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.newUrunMenuForm.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.newUrunMenuForm.Location = new System.Drawing.Point(299, 3);
            this.newUrunMenuForm.Name = "newUrunMenuForm";
            this.newUrunMenuForm.Size = new System.Drawing.Size(280, 316);
            this.newUrunMenuForm.TabIndex = 53;
            this.newUrunMenuForm.TabStop = false;
            this.newUrunMenuForm.Text = "Ürün";
            // 
            // comboBox1
            // 
            this.comboBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBox1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.comboBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Adet",
            "Kilo",
            "Gram",
            "Cl"});
            this.comboBox1.Location = new System.Drawing.Point(150, 114);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(123, 32);
            this.comboBox1.TabIndex = 55;
            // 
            // comboBoxUrun
            // 
            this.comboBoxUrun.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBoxUrun.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.comboBoxUrun.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.comboBoxUrun.FormattingEnabled = true;
            this.comboBoxUrun.Items.AddRange(new object[] {
            "Adet",
            "Kilo",
            "Gram",
            "Cl"});
            this.comboBoxUrun.Location = new System.Drawing.Point(10, 52);
            this.comboBoxUrun.Name = "comboBoxUrun";
            this.comboBoxUrun.Size = new System.Drawing.Size(264, 32);
            this.comboBoxUrun.TabIndex = 54;
            // 
            // textboxFiyat
            // 
            this.textboxFiyat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxFiyat.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textboxFiyat.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textboxFiyat.Location = new System.Drawing.Point(10, 114);
            this.textboxFiyat.Name = "textboxFiyat";
            this.textboxFiyat.Size = new System.Drawing.Size(134, 32);
            this.textboxFiyat.TabIndex = 48;
            this.textboxFiyat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Location = new System.Drawing.Point(6, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 24);
            this.label5.TabIndex = 19;
            this.label5.Text = "Ürün Adı:";
            // 
            // buttonSaveUrun
            // 
            this.buttonSaveUrun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveUrun.BackColor = System.Drawing.SystemColors.Window;
            this.buttonSaveUrun.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonSaveUrun.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonSaveUrun.Image = global::ROPv1.Properties.Resources.icon;
            this.buttonSaveUrun.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSaveUrun.Location = new System.Drawing.Point(164, 265);
            this.buttonSaveUrun.Name = "buttonSaveUrun";
            this.buttonSaveUrun.Size = new System.Drawing.Size(110, 45);
            this.buttonSaveUrun.TabIndex = 5;
            this.buttonSaveUrun.Text = "Kaydet";
            this.buttonSaveUrun.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSaveUrun.UseVisualStyleBackColor = false;
            // 
            // buttonDeleteUrun
            // 
            this.buttonDeleteUrun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteUrun.BackColor = System.Drawing.SystemColors.Window;
            this.buttonDeleteUrun.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonDeleteUrun.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonDeleteUrun.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonDeleteUrun.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDeleteUrun.Location = new System.Drawing.Point(10, 265);
            this.buttonDeleteUrun.Name = "buttonDeleteUrun";
            this.buttonDeleteUrun.Size = new System.Drawing.Size(110, 45);
            this.buttonDeleteUrun.TabIndex = 31;
            this.buttonDeleteUrun.TabStop = false;
            this.buttonDeleteUrun.Text = "Ürünü Sil";
            this.buttonDeleteUrun.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonDeleteUrun.UseVisualStyleBackColor = false;
            // 
            // lblFiyat
            // 
            this.lblFiyat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFiyat.AutoSize = true;
            this.lblFiyat.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblFiyat.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblFiyat.Location = new System.Drawing.Point(6, 87);
            this.lblFiyat.Name = "lblFiyat";
            this.lblFiyat.Size = new System.Drawing.Size(138, 24);
            this.lblFiyat.TabIndex = 47;
            this.lblFiyat.Text = "Ürün Miktarı:";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonCancel.BackColor = System.Drawing.SystemColors.Window;
            this.buttonCancel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonCancel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonCancel.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCancel.Location = new System.Drawing.Point(10, 265);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(110, 44);
            this.buttonCancel.TabIndex = 40;
            this.buttonCancel.TabStop = false;
            this.buttonCancel.Text = "İptal Et  ";
            this.buttonCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonCancel.UseVisualStyleBackColor = false;
            // 
            // keyboardcontrol1
            // 
            this.keyboardcontrol1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.keyboardcontrol1.ForeColor = System.Drawing.SystemColors.Window;
            this.keyboardcontrol1.KeyboardType = KeyboardClassLibrary.BoW.Standard;
            this.keyboardcontrol1.Location = new System.Drawing.Point(0, 418);
            this.keyboardcontrol1.Name = "keyboardcontrol1";
            this.keyboardcontrol1.Size = new System.Drawing.Size(1085, 282);
            this.keyboardcontrol1.TabIndex = 54;
            // 
            // ButtonYeniMalzeme
            // 
            this.ButtonYeniMalzeme.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonYeniMalzeme.BackColor = System.Drawing.SystemColors.Window;
            this.ButtonYeniMalzeme.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ButtonYeniMalzeme.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.ButtonYeniMalzeme.Image = global::ROPv1.Properties.Resources.add;
            this.ButtonYeniMalzeme.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonYeniMalzeme.Location = new System.Drawing.Point(309, 343);
            this.ButtonYeniMalzeme.Name = "ButtonYeniMalzeme";
            this.ButtonYeniMalzeme.Size = new System.Drawing.Size(263, 45);
            this.ButtonYeniMalzeme.TabIndex = 55;
            this.ButtonYeniMalzeme.TabStop = false;
            this.ButtonYeniMalzeme.Text = "Ürün Malzemesi Oluştur";
            this.ButtonYeniMalzeme.UseVisualStyleBackColor = false;
            // 
            // Receteler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ButtonYeniMalzeme);
            this.Controls.Add(this.keyboardcontrol1);
            this.Controls.Add(this.newUrunMenuForm);
            this.Controls.Add(this.treeMalzemeler);
            this.Controls.Add(this.myListIcindekiler);
            this.Name = "Receteler";
            this.Size = new System.Drawing.Size(1124, 545);
            this.Load += new System.EventHandler(this.Receteler_Load);
            this.newUrunMenuForm.ResumeLayout(false);
            this.newUrunMenuForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyListView myListIcindekiler;
        private System.Windows.Forms.ColumnHeader columnUrunAdi;
        private System.Windows.Forms.ColumnHeader columnUrunMiktari;
        private System.Windows.Forms.ColumnHeader columnMiktarTipi;
        private System.Windows.Forms.TreeView treeMalzemeler;
        private System.Windows.Forms.Button buttonEkle;
        private System.Windows.Forms.Button buttonCikar;
        private System.Windows.Forms.GroupBox newUrunMenuForm;
        private System.Windows.Forms.TextBox textboxFiyat;
        private System.Windows.Forms.Label lblFiyat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonSaveUrun;
        private System.Windows.Forms.Button buttonDeleteUrun;
        private System.Windows.Forms.Button buttonCancel;
        private KeyboardClassLibrary.Keyboardcontrol keyboardcontrol1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBoxUrun;
        private System.Windows.Forms.Button ButtonYeniMalzeme;
    }
}
