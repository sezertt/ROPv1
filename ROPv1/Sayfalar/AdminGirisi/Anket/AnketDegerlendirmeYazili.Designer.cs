﻿namespace ROPv1
{
    partial class AnketDegerlendirmeYazili
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
            this.buttonSayfaAzalt = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.labelSayfa = new System.Windows.Forms.Label();
            this.labelSayfaSayisi = new System.Windows.Forms.Label();
            this.buttonSayfaArttir = new System.Windows.Forms.Button();
            this.listYaziliSoru = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // buttonSayfaAzalt
            // 
            this.buttonSayfaAzalt.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonSayfaAzalt.BackColor = System.Drawing.SystemColors.Window;
            this.buttonSayfaAzalt.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonSayfaAzalt.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonSayfaAzalt.Image = global::ROPv1.Properties.Resources.lefticon;
            this.buttonSayfaAzalt.Location = new System.Drawing.Point(321, 566);
            this.buttonSayfaAzalt.Name = "buttonSayfaAzalt";
            this.buttonSayfaAzalt.Size = new System.Drawing.Size(86, 48);
            this.buttonSayfaAzalt.TabIndex = 84;
            this.buttonSayfaAzalt.TabStop = false;
            this.buttonSayfaAzalt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSayfaAzalt.UseVisualStyleBackColor = false;
            this.buttonSayfaAzalt.Click += new System.EventHandler(this.buttonSayfaAzalt_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Calibri", 24.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.SystemColors.Window;
            this.label2.Location = new System.Drawing.Point(496, 567);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 40);
            this.label2.TabIndex = 87;
            this.label2.Text = "/";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelSayfa
            // 
            this.labelSayfa.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelSayfa.BackColor = System.Drawing.Color.Transparent;
            this.labelSayfa.Font = new System.Drawing.Font("Calibri", 24.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelSayfa.ForeColor = System.Drawing.SystemColors.Window;
            this.labelSayfa.Location = new System.Drawing.Point(413, 567);
            this.labelSayfa.Name = "labelSayfa";
            this.labelSayfa.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelSayfa.Size = new System.Drawing.Size(87, 39);
            this.labelSayfa.TabIndex = 88;
            this.labelSayfa.Text = "0";
            this.labelSayfa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelSayfa.TextChanged += new System.EventHandler(this.labelSayfa_TextChanged);
            // 
            // labelSayfaSayisi
            // 
            this.labelSayfaSayisi.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelSayfaSayisi.AutoSize = true;
            this.labelSayfaSayisi.BackColor = System.Drawing.Color.Transparent;
            this.labelSayfaSayisi.Font = new System.Drawing.Font("Calibri", 24.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelSayfaSayisi.ForeColor = System.Drawing.SystemColors.Window;
            this.labelSayfaSayisi.Location = new System.Drawing.Point(526, 567);
            this.labelSayfaSayisi.Name = "labelSayfaSayisi";
            this.labelSayfaSayisi.Size = new System.Drawing.Size(34, 40);
            this.labelSayfaSayisi.TabIndex = 86;
            this.labelSayfaSayisi.Text = "0";
            this.labelSayfaSayisi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonSayfaArttir
            // 
            this.buttonSayfaArttir.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonSayfaArttir.BackColor = System.Drawing.SystemColors.Window;
            this.buttonSayfaArttir.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonSayfaArttir.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonSayfaArttir.Image = global::ROPv1.Properties.Resources.righticon;
            this.buttonSayfaArttir.Location = new System.Drawing.Point(612, 566);
            this.buttonSayfaArttir.Name = "buttonSayfaArttir";
            this.buttonSayfaArttir.Size = new System.Drawing.Size(86, 48);
            this.buttonSayfaArttir.TabIndex = 85;
            this.buttonSayfaArttir.TabStop = false;
            this.buttonSayfaArttir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSayfaArttir.UseVisualStyleBackColor = false;
            this.buttonSayfaArttir.Click += new System.EventHandler(this.buttonSayfaArttir_Click);
            // 
            // listYaziliSoru
            // 
            this.listYaziliSoru.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listYaziliSoru.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listYaziliSoru.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listYaziliSoru.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6});
            this.listYaziliSoru.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold);
            this.listYaziliSoru.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.listYaziliSoru.FullRowSelect = true;
            this.listYaziliSoru.GridLines = true;
            this.listYaziliSoru.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listYaziliSoru.LabelWrap = false;
            this.listYaziliSoru.Location = new System.Drawing.Point(6, 12);
            this.listYaziliSoru.Margin = new System.Windows.Forms.Padding(0);
            this.listYaziliSoru.MultiSelect = false;
            this.listYaziliSoru.Name = "listYaziliSoru";
            this.listYaziliSoru.Scrollable = false;
            this.listYaziliSoru.ShowItemToolTips = true;
            this.listYaziliSoru.Size = new System.Drawing.Size(1006, 544);
            this.listYaziliSoru.TabIndex = 89;
            this.listYaziliSoru.UseCompatibleStateImageBehavior = false;
            this.listYaziliSoru.View = System.Windows.Forms.View.Details;
            this.listYaziliSoru.SelectedIndexChanged += new System.EventHandler(this.listYaziliSoru_SelectedIndexChanged);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Seçmeli Soru";
            this.columnHeader6.Width = 1006;
            // 
            // AnketDegerlendirmeYazili
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.listYaziliSoru);
            this.Controls.Add(this.buttonSayfaAzalt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelSayfa);
            this.Controls.Add(this.buttonSayfaArttir);
            this.Controls.Add(this.labelSayfaSayisi);
            this.Name = "AnketDegerlendirmeYazili";
            this.Size = new System.Drawing.Size(1018, 626);
            this.Load += new System.EventHandler(this.AnketDegerlendirme_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSayfaAzalt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelSayfa;
        private System.Windows.Forms.Label labelSayfaSayisi;
        private System.Windows.Forms.Button buttonSayfaArttir;
        private System.Windows.Forms.ListView listYaziliSoru;
        private System.Windows.Forms.ColumnHeader columnHeader6;


    }
}
