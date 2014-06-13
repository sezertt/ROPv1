namespace ROPv1
{
    partial class AnketSonuclari
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
            this.listSecmeliSoru = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listAnketID = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonSayfaAzalt = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.labelSayfa = new System.Windows.Forms.Label();
            this.buttonSayfaArttir = new System.Windows.Forms.Button();
            this.labelSayfaSayisi = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelGenelPuan = new System.Windows.Forms.Label();
            this.labelYildiz = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxYaziliSorular = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // listSecmeliSoru
            // 
            this.listSecmeliSoru.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listSecmeliSoru.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listSecmeliSoru.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listSecmeliSoru.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader5,
            this.columnHeader7,
            this.columnHeader8});
            this.listSecmeliSoru.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold);
            this.listSecmeliSoru.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.listSecmeliSoru.FullRowSelect = true;
            this.listSecmeliSoru.GridLines = true;
            this.listSecmeliSoru.LabelWrap = false;
            this.listSecmeliSoru.Location = new System.Drawing.Point(230, 38);
            this.listSecmeliSoru.Margin = new System.Windows.Forms.Padding(0);
            this.listSecmeliSoru.MultiSelect = false;
            this.listSecmeliSoru.Name = "listSecmeliSoru";
            this.listSecmeliSoru.Scrollable = false;
            this.listSecmeliSoru.ShowItemToolTips = true;
            this.listSecmeliSoru.Size = new System.Drawing.Size(769, 435);
            this.listSecmeliSoru.TabIndex = 45;
            this.listSecmeliSoru.UseCompatibleStateImageBehavior = false;
            this.listSecmeliSoru.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Seçmeli Soru";
            this.columnHeader6.Width = 556;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Cevap";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 70;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Etki";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader7.Width = 70;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Puan";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader8.Width = 70;
            // 
            // listAnketID
            // 
            this.listAnketID.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listAnketID.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listAnketID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listAnketID.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listAnketID.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.listAnketID.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.listAnketID.FullRowSelect = true;
            this.listAnketID.GridLines = true;
            this.listAnketID.HideSelection = false;
            this.listAnketID.LabelWrap = false;
            this.listAnketID.Location = new System.Drawing.Point(10, 38);
            this.listAnketID.Margin = new System.Windows.Forms.Padding(0);
            this.listAnketID.MultiSelect = false;
            this.listAnketID.Name = "listAnketID";
            this.listAnketID.Scrollable = false;
            this.listAnketID.Size = new System.Drawing.Size(200, 533);
            this.listAnketID.TabIndex = 44;
            this.listAnketID.UseCompatibleStateImageBehavior = false;
            this.listAnketID.View = System.Windows.Forms.View.Details;
            this.listAnketID.SelectedIndexChanged += new System.EventHandler(this.listAnketID_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Anket ID";
            this.columnHeader1.Width = 200;
            // 
            // buttonSayfaAzalt
            // 
            this.buttonSayfaAzalt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSayfaAzalt.BackColor = System.Drawing.SystemColors.Window;
            this.buttonSayfaAzalt.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonSayfaAzalt.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonSayfaAzalt.Image = global::ROPv1.Properties.Resources.lefticon;
            this.buttonSayfaAzalt.Location = new System.Drawing.Point(10, 574);
            this.buttonSayfaAzalt.Name = "buttonSayfaAzalt";
            this.buttonSayfaAzalt.Size = new System.Drawing.Size(86, 48);
            this.buttonSayfaAzalt.TabIndex = 78;
            this.buttonSayfaAzalt.TabStop = false;
            this.buttonSayfaAzalt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSayfaAzalt.UseVisualStyleBackColor = false;
            this.buttonSayfaAzalt.Click += new System.EventHandler(this.buttonSayfaAzalt_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Calibri", 24.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.SystemColors.Window;
            this.label2.Location = new System.Drawing.Point(92, -2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 40);
            this.label2.TabIndex = 81;
            this.label2.Text = "/";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelSayfa
            // 
            this.labelSayfa.BackColor = System.Drawing.Color.Transparent;
            this.labelSayfa.Font = new System.Drawing.Font("Calibri", 24.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelSayfa.ForeColor = System.Drawing.SystemColors.Window;
            this.labelSayfa.Location = new System.Drawing.Point(9, -2);
            this.labelSayfa.Name = "labelSayfa";
            this.labelSayfa.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelSayfa.Size = new System.Drawing.Size(87, 39);
            this.labelSayfa.TabIndex = 82;
            this.labelSayfa.Text = "0";
            this.labelSayfa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelSayfa.TextChanged += new System.EventHandler(this.labelSayfa_TextChanged);
            // 
            // buttonSayfaArttir
            // 
            this.buttonSayfaArttir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSayfaArttir.BackColor = System.Drawing.SystemColors.Window;
            this.buttonSayfaArttir.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonSayfaArttir.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonSayfaArttir.Image = global::ROPv1.Properties.Resources.righticon;
            this.buttonSayfaArttir.Location = new System.Drawing.Point(124, 574);
            this.buttonSayfaArttir.Name = "buttonSayfaArttir";
            this.buttonSayfaArttir.Size = new System.Drawing.Size(86, 48);
            this.buttonSayfaArttir.TabIndex = 79;
            this.buttonSayfaArttir.TabStop = false;
            this.buttonSayfaArttir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSayfaArttir.UseVisualStyleBackColor = false;
            this.buttonSayfaArttir.Click += new System.EventHandler(this.buttonSayfaArttir_Click);
            // 
            // labelSayfaSayisi
            // 
            this.labelSayfaSayisi.AutoSize = true;
            this.labelSayfaSayisi.BackColor = System.Drawing.Color.Transparent;
            this.labelSayfaSayisi.Font = new System.Drawing.Font("Calibri", 24.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelSayfaSayisi.ForeColor = System.Drawing.SystemColors.Window;
            this.labelSayfaSayisi.Location = new System.Drawing.Point(122, -2);
            this.labelSayfaSayisi.Name = "labelSayfaSayisi";
            this.labelSayfaSayisi.Size = new System.Drawing.Size(34, 40);
            this.labelSayfaSayisi.TabIndex = 80;
            this.labelSayfaSayisi.Text = "0";
            this.labelSayfaSayisi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Calibri", 24.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(227, -3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 40);
            this.label1.TabIndex = 84;
            this.label1.Text = "Anket Puanı =";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelGenelPuan
            // 
            this.labelGenelPuan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelGenelPuan.AutoSize = true;
            this.labelGenelPuan.BackColor = System.Drawing.Color.Transparent;
            this.labelGenelPuan.Font = new System.Drawing.Font("Calibri", 24.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelGenelPuan.ForeColor = System.Drawing.SystemColors.Window;
            this.labelGenelPuan.Location = new System.Drawing.Point(425, -3);
            this.labelGenelPuan.Name = "labelGenelPuan";
            this.labelGenelPuan.Size = new System.Drawing.Size(34, 40);
            this.labelGenelPuan.TabIndex = 85;
            this.labelGenelPuan.Text = "0";
            this.labelGenelPuan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelYildiz
            // 
            this.labelYildiz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelYildiz.AutoSize = true;
            this.labelYildiz.BackColor = System.Drawing.Color.Transparent;
            this.labelYildiz.Font = new System.Drawing.Font("Calibri", 24.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelYildiz.ForeColor = System.Drawing.SystemColors.Window;
            this.labelYildiz.Location = new System.Drawing.Point(965, -2);
            this.labelYildiz.Name = "labelYildiz";
            this.labelYildiz.Size = new System.Drawing.Size(34, 40);
            this.labelYildiz.TabIndex = 87;
            this.labelYildiz.Text = "0";
            this.labelYildiz.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Calibri", 24.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.ForeColor = System.Drawing.SystemColors.Window;
            this.label4.Location = new System.Drawing.Point(853, -2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 40);
            this.label4.TabIndex = 86;
            this.label4.Text = "Yıldız =";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxYaziliSorular
            // 
            this.textBoxYaziliSorular.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxYaziliSorular.BackColor = System.Drawing.Color.White;
            this.textBoxYaziliSorular.Cursor = System.Windows.Forms.Cursors.Default;
            this.textBoxYaziliSorular.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold);
            this.textBoxYaziliSorular.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBoxYaziliSorular.Location = new System.Drawing.Point(230, 477);
            this.textBoxYaziliSorular.Multiline = true;
            this.textBoxYaziliSorular.Name = "textBoxYaziliSorular";
            this.textBoxYaziliSorular.ReadOnly = true;
            this.textBoxYaziliSorular.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxYaziliSorular.Size = new System.Drawing.Size(769, 145);
            this.textBoxYaziliSorular.TabIndex = 88;
            this.textBoxYaziliSorular.TabStop = false;
            // 
            // AnketSonuclari
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.textBoxYaziliSorular);
            this.Controls.Add(this.labelYildiz);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelGenelPuan);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSayfaAzalt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelSayfa);
            this.Controls.Add(this.buttonSayfaArttir);
            this.Controls.Add(this.labelSayfaSayisi);
            this.Controls.Add(this.listSecmeliSoru);
            this.Controls.Add(this.listAnketID);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name = "AnketSonuclari";
            this.Size = new System.Drawing.Size(1018, 626);
            this.Load += new System.EventHandler(this.AnketSonuclari_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listSecmeliSoru;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ListView listAnketID;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button buttonSayfaAzalt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelSayfa;
        private System.Windows.Forms.Button buttonSayfaArttir;
        private System.Windows.Forms.Label labelSayfaSayisi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelGenelPuan;
        private System.Windows.Forms.Label labelYildiz;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxYaziliSorular;


    }
}
