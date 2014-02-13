namespace ROPv1
{
    partial class GunFormu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GunFormu));
            this.treeGunBasi = new System.Windows.Forms.TreeView();
            this.buttonGunBasi = new System.Windows.Forms.Button();
            this.buttonGunSonu = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelGun = new System.Windows.Forms.Label();
            this.labelTarih = new System.Windows.Forms.Label();
            this.labelSaat = new System.Windows.Forms.Label();
            this.exitButton = new System.Windows.Forms.Button();
            this.timerSaat = new System.Windows.Forms.Timer(this.components);
            this.labelGunBasi = new System.Windows.Forms.Label();
            this.labelGunSonu = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericNumberOfCurrentPage = new System.Windows.Forms.NumericUpDown();
            this.labelNumberOfPages = new System.Windows.Forms.Label();
            this.labelSure = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timerGecenSure = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numericNumberOfCurrentPage)).BeginInit();
            this.SuspendLayout();
            // 
            // treeGunBasi
            // 
            this.treeGunBasi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeGunBasi.Font = new System.Drawing.Font("Arial", 18.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.treeGunBasi.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.treeGunBasi.FullRowSelect = true;
            this.treeGunBasi.HideSelection = false;
            this.treeGunBasi.HotTracking = true;
            this.treeGunBasi.Indent = 5;
            this.treeGunBasi.ItemHeight = 35;
            this.treeGunBasi.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.treeGunBasi.Location = new System.Drawing.Point(12, 12);
            this.treeGunBasi.Name = "treeGunBasi";
            this.treeGunBasi.Scrollable = false;
            this.treeGunBasi.ShowLines = false;
            this.treeGunBasi.ShowPlusMinus = false;
            this.treeGunBasi.ShowRootLines = false;
            this.treeGunBasi.Size = new System.Drawing.Size(1012, 735);
            this.treeGunBasi.TabIndex = 39;
            this.treeGunBasi.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.gunBilgisiDoldur);
            // 
            // buttonGunBasi
            // 
            this.buttonGunBasi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGunBasi.BackColor = System.Drawing.SystemColors.Window;
            this.buttonGunBasi.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonGunBasi.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonGunBasi.Image = global::ROPv1.Properties.Resources.dayOn;
            this.buttonGunBasi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonGunBasi.Location = new System.Drawing.Point(1062, 259);
            this.buttonGunBasi.Name = "buttonGunBasi";
            this.buttonGunBasi.Size = new System.Drawing.Size(212, 75);
            this.buttonGunBasi.TabIndex = 40;
            this.buttonGunBasi.Text = "Gün Başı";
            this.buttonGunBasi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonGunBasi.UseVisualStyleBackColor = false;
            this.buttonGunBasi.Click += new System.EventHandler(this.buttonGunBasi_Click);
            // 
            // buttonGunSonu
            // 
            this.buttonGunSonu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGunSonu.BackColor = System.Drawing.SystemColors.Window;
            this.buttonGunSonu.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonGunSonu.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonGunSonu.Image = global::ROPv1.Properties.Resources.dayOff;
            this.buttonGunSonu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonGunSonu.Location = new System.Drawing.Point(1062, 340);
            this.buttonGunSonu.Name = "buttonGunSonu";
            this.buttonGunSonu.Size = new System.Drawing.Size(212, 75);
            this.buttonGunSonu.TabIndex = 41;
            this.buttonGunSonu.Text = "Gün Sonu";
            this.buttonGunSonu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonGunSonu.UseVisualStyleBackColor = false;
            this.buttonGunSonu.Click += new System.EventHandler(this.buttonGunSonu_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.ForeColor = System.Drawing.SystemColors.Window;
            this.label5.Location = new System.Drawing.Point(1058, 433);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(177, 24);
            this.label5.TabIndex = 44;
            this.label5.Text = "Gün Başı Yapan:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(1058, 515);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 24);
            this.label1.TabIndex = 45;
            this.label1.Text = "Gün Sonu Yapan:";
            // 
            // labelGun
            // 
            this.labelGun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelGun.AutoSize = true;
            this.labelGun.BackColor = System.Drawing.Color.Transparent;
            this.labelGun.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelGun.ForeColor = System.Drawing.SystemColors.Window;
            this.labelGun.Location = new System.Drawing.Point(1113, 213);
            this.labelGun.Name = "labelGun";
            this.labelGun.Size = new System.Drawing.Size(116, 33);
            this.labelGun.TabIndex = 48;
            this.labelGun.Text = "Pazartesi";
            this.labelGun.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelTarih
            // 
            this.labelTarih.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTarih.AutoSize = true;
            this.labelTarih.BackColor = System.Drawing.Color.Transparent;
            this.labelTarih.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelTarih.ForeColor = System.Drawing.SystemColors.Window;
            this.labelTarih.Location = new System.Drawing.Point(1083, 186);
            this.labelTarih.Name = "labelTarih";
            this.labelTarih.Size = new System.Drawing.Size(180, 33);
            this.labelTarih.TabIndex = 47;
            this.labelTarih.Text = "10 Şubat 2014 ";
            this.labelTarih.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelSaat
            // 
            this.labelSaat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSaat.AutoSize = true;
            this.labelSaat.BackColor = System.Drawing.Color.Transparent;
            this.labelSaat.Font = new System.Drawing.Font("Calibri", 45F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelSaat.ForeColor = System.Drawing.SystemColors.Window;
            this.labelSaat.Location = new System.Drawing.Point(1049, 125);
            this.labelSaat.Name = "labelSaat";
            this.labelSaat.Size = new System.Drawing.Size(246, 73);
            this.labelSaat.TabIndex = 46;
            this.labelSaat.Text = "22:55:30";
            this.labelSaat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // exitButton
            // 
            this.exitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exitButton.BackColor = System.Drawing.SystemColors.Window;
            this.exitButton.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.exitButton.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.exitButton.Image = global::ROPv1.Properties.Resources.logOut;
            this.exitButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.exitButton.Location = new System.Drawing.Point(1062, 12);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(212, 110);
            this.exitButton.TabIndex = 49;
            this.exitButton.Text = "Çıkış";
            this.exitButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // timerSaat
            // 
            this.timerSaat.Interval = 1000;
            this.timerSaat.Tick += new System.EventHandler(this.timerSaat_Tick);
            // 
            // labelGunBasi
            // 
            this.labelGunBasi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelGunBasi.BackColor = System.Drawing.SystemColors.Window;
            this.labelGunBasi.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelGunBasi.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelGunBasi.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelGunBasi.Location = new System.Drawing.Point(1062, 465);
            this.labelGunBasi.Name = "labelGunBasi";
            this.labelGunBasi.Size = new System.Drawing.Size(212, 30);
            this.labelGunBasi.TabIndex = 50;
            this.labelGunBasi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelGunSonu
            // 
            this.labelGunSonu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelGunSonu.BackColor = System.Drawing.SystemColors.Window;
            this.labelGunSonu.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelGunSonu.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelGunSonu.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelGunSonu.Location = new System.Drawing.Point(1062, 547);
            this.labelGunSonu.Name = "labelGunSonu";
            this.labelGunSonu.Size = new System.Drawing.Size(212, 30);
            this.labelGunSonu.TabIndex = 51;
            this.labelGunSonu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.SystemColors.Window;
            this.label2.Location = new System.Drawing.Point(566, 767);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 33);
            this.label2.TabIndex = 52;
            this.label2.Text = "/";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericNumberOfCurrentPage
            // 
            this.numericNumberOfCurrentPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.numericNumberOfCurrentPage.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.numericNumberOfCurrentPage.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.numericNumberOfCurrentPage.Location = new System.Drawing.Point(469, 763);
            this.numericNumberOfCurrentPage.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericNumberOfCurrentPage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericNumberOfCurrentPage.Name = "numericNumberOfCurrentPage";
            this.numericNumberOfCurrentPage.Size = new System.Drawing.Size(86, 40);
            this.numericNumberOfCurrentPage.TabIndex = 53;
            this.numericNumberOfCurrentPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericNumberOfCurrentPage.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericNumberOfCurrentPage.ValueChanged += new System.EventHandler(this.currentPageChanged);
            // 
            // labelNumberOfPages
            // 
            this.labelNumberOfPages.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelNumberOfPages.AutoSize = true;
            this.labelNumberOfPages.BackColor = System.Drawing.Color.Transparent;
            this.labelNumberOfPages.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelNumberOfPages.ForeColor = System.Drawing.SystemColors.Window;
            this.labelNumberOfPages.Location = new System.Drawing.Point(594, 768);
            this.labelNumberOfPages.Name = "labelNumberOfPages";
            this.labelNumberOfPages.Size = new System.Drawing.Size(29, 33);
            this.labelNumberOfPages.TabIndex = 54;
            this.labelNumberOfPages.Text = "1";
            this.labelNumberOfPages.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelSure
            // 
            this.labelSure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSure.BackColor = System.Drawing.SystemColors.Window;
            this.labelSure.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSure.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelSure.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelSure.Location = new System.Drawing.Point(1062, 629);
            this.labelSure.Name = "labelSure";
            this.labelSure.Size = new System.Drawing.Size(212, 30);
            this.labelSure.TabIndex = 56;
            this.labelSure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.ForeColor = System.Drawing.SystemColors.Window;
            this.label4.Location = new System.Drawing.Point(1058, 597);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 24);
            this.label4.TabIndex = 55;
            this.label4.Text = "Geçen Süre:";
            // 
            // timerGecenSure
            // 
            this.timerGecenSure.Interval = 1000;
            this.timerGecenSure.Tick += new System.EventHandler(this.timerGecenSure_Tick);
            // 
            // GunFormu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1313, 819);
            this.Controls.Add(this.labelSure);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericNumberOfCurrentPage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelGunSonu);
            this.Controls.Add(this.labelGunBasi);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.labelGun);
            this.Controls.Add(this.labelTarih);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonGunSonu);
            this.Controls.Add(this.buttonGunBasi);
            this.Controls.Add(this.treeGunBasi);
            this.Controls.Add(this.labelSaat);
            this.Controls.Add(this.labelNumberOfPages);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GunFormu";
            this.Text = "GunFormu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.numericNumberOfCurrentPage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeGunBasi;
        private System.Windows.Forms.Button buttonGunBasi;
        private System.Windows.Forms.Button buttonGunSonu;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelGun;
        private System.Windows.Forms.Label labelTarih;
        private System.Windows.Forms.Label labelSaat;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Timer timerSaat;
        private System.Windows.Forms.Label labelGunBasi;
        private System.Windows.Forms.Label labelGunSonu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericNumberOfCurrentPage;
        private System.Windows.Forms.Label labelNumberOfPages;
        private System.Windows.Forms.Label labelSure;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timerGecenSure;
    }
}