namespace ROPv1
{
    partial class AnketKullanicilari
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
            this.label19 = new System.Windows.Forms.Label();
            this.buttonKullaniciyiSil = new System.Windows.Forms.Button();
            this.buttonSayfaAzalt = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.labelSayfa = new System.Windows.Forms.Label();
            this.buttonSayfaArttir = new System.Windows.Forms.Button();
            this.labelSayfaSayisi = new System.Windows.Forms.Label();
            this.listKullanici = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label19.ForeColor = System.Drawing.SystemColors.Window;
            this.label19.Location = new System.Drawing.Point(20, 9);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(190, 24);
            this.label19.TabIndex = 56;
            this.label19.Text = "Anket Kullanıcıları";
            // 
            // buttonKullaniciyiSil
            // 
            this.buttonKullaniciyiSil.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonKullaniciyiSil.BackColor = System.Drawing.SystemColors.Window;
            this.buttonKullaniciyiSil.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonKullaniciyiSil.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonKullaniciyiSil.Image = global::ROPv1.Properties.Resources.deleteBig;
            this.buttonKullaniciyiSil.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonKullaniciyiSil.Location = new System.Drawing.Point(807, 566);
            this.buttonKullaniciyiSil.Name = "buttonKullaniciyiSil";
            this.buttonKullaniciyiSil.Size = new System.Drawing.Size(191, 45);
            this.buttonKullaniciyiSil.TabIndex = 71;
            this.buttonKullaniciyiSil.Text = "Seçili Kullanıcıyı Sil";
            this.buttonKullaniciyiSil.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonKullaniciyiSil.UseVisualStyleBackColor = false;
            this.buttonKullaniciyiSil.Click += new System.EventHandler(this.buttonKullaniciyiSil_Click);
            // 
            // buttonSayfaAzalt
            // 
            this.buttonSayfaAzalt.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonSayfaAzalt.BackColor = System.Drawing.SystemColors.Window;
            this.buttonSayfaAzalt.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonSayfaAzalt.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonSayfaAzalt.Image = global::ROPv1.Properties.Resources.lefticon;
            this.buttonSayfaAzalt.Location = new System.Drawing.Point(244, 558);
            this.buttonSayfaAzalt.Name = "buttonSayfaAzalt";
            this.buttonSayfaAzalt.Size = new System.Drawing.Size(120, 61);
            this.buttonSayfaAzalt.TabIndex = 73;
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
            this.label2.Font = new System.Drawing.Font("Calibri", 32F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.Window;
            this.label2.Location = new System.Drawing.Point(481, 560);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 53);
            this.label2.TabIndex = 76;
            this.label2.Text = "/";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelSayfa
            // 
            this.labelSayfa.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelSayfa.BackColor = System.Drawing.Color.Transparent;
            this.labelSayfa.Font = new System.Drawing.Font("Calibri", 32F);
            this.labelSayfa.ForeColor = System.Drawing.SystemColors.Window;
            this.labelSayfa.Location = new System.Drawing.Point(355, 560);
            this.labelSayfa.Name = "labelSayfa";
            this.labelSayfa.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelSayfa.Size = new System.Drawing.Size(138, 53);
            this.labelSayfa.TabIndex = 77;
            this.labelSayfa.Text = "0";
            this.labelSayfa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelSayfa.TextChanged += new System.EventHandler(this.labelSayfa_TextChanged);
            // 
            // buttonSayfaArttir
            // 
            this.buttonSayfaArttir.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonSayfaArttir.BackColor = System.Drawing.SystemColors.Window;
            this.buttonSayfaArttir.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonSayfaArttir.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonSayfaArttir.Image = global::ROPv1.Properties.Resources.righticon;
            this.buttonSayfaArttir.Location = new System.Drawing.Point(639, 558);
            this.buttonSayfaArttir.Name = "buttonSayfaArttir";
            this.buttonSayfaArttir.Size = new System.Drawing.Size(120, 61);
            this.buttonSayfaArttir.TabIndex = 74;
            this.buttonSayfaArttir.TabStop = false;
            this.buttonSayfaArttir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSayfaArttir.UseVisualStyleBackColor = false;
            this.buttonSayfaArttir.Click += new System.EventHandler(this.buttonSayfaArttir_Click);
            // 
            // labelSayfaSayisi
            // 
            this.labelSayfaSayisi.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelSayfaSayisi.AutoSize = true;
            this.labelSayfaSayisi.BackColor = System.Drawing.Color.Transparent;
            this.labelSayfaSayisi.Font = new System.Drawing.Font("Calibri", 32F);
            this.labelSayfaSayisi.ForeColor = System.Drawing.SystemColors.Window;
            this.labelSayfaSayisi.Location = new System.Drawing.Point(513, 560);
            this.labelSayfaSayisi.Name = "labelSayfaSayisi";
            this.labelSayfaSayisi.Size = new System.Drawing.Size(45, 53);
            this.labelSayfaSayisi.TabIndex = 75;
            this.labelSayfaSayisi.Text = "0";
            this.labelSayfaSayisi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listKullanici
            // 
            this.listKullanici.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listKullanici.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listKullanici.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listKullanici.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listKullanici.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.listKullanici.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.listKullanici.FullRowSelect = true;
            this.listKullanici.GridLines = true;
            this.listKullanici.HideSelection = false;
            this.listKullanici.LabelWrap = false;
            this.listKullanici.Location = new System.Drawing.Point(20, 40);
            this.listKullanici.Margin = new System.Windows.Forms.Padding(0);
            this.listKullanici.MultiSelect = false;
            this.listKullanici.Name = "listKullanici";
            this.listKullanici.Scrollable = false;
            this.listKullanici.Size = new System.Drawing.Size(978, 510);
            this.listKullanici.TabIndex = 72;
            this.listKullanici.UseCompatibleStateImageBehavior = false;
            this.listKullanici.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Kullanıcının Adı";
            this.columnHeader1.Width = 244;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Kullanıcının Soyadı";
            this.columnHeader2.Width = 244;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "E-posta Adresi";
            this.columnHeader3.Width = 244;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Telefonu";
            this.columnHeader4.Width = 244;
            // 
            // AnketKullanicilari
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.buttonSayfaAzalt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelSayfa);
            this.Controls.Add(this.buttonSayfaArttir);
            this.Controls.Add(this.labelSayfaSayisi);
            this.Controls.Add(this.listKullanici);
            this.Controls.Add(this.buttonKullaniciyiSil);
            this.Controls.Add(this.label19);
            this.Name = "AnketKullanicilari";
            this.Size = new System.Drawing.Size(1018, 626);
            this.Load += new System.EventHandler(this.AnketKullanicilari_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button buttonKullaniciyiSil;
        private System.Windows.Forms.Button buttonSayfaAzalt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelSayfa;
        private System.Windows.Forms.Button buttonSayfaArttir;
        private System.Windows.Forms.Label labelSayfaSayisi;
        private System.Windows.Forms.ListView listKullanici;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;

    }
}
