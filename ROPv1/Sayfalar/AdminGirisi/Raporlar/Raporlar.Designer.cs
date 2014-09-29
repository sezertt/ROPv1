namespace ROPv1
{
    partial class Raporlar
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
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.comboAdisyonAyar = new System.Windows.Forms.ComboBox();
            this.dateBitis = new System.Windows.Forms.DateTimePicker();
            this.dateBaslangic = new System.Windows.Forms.DateTimePicker();
            this.buttonRaporla = new System.Windows.Forms.Button();
            this.buttonExcel = new System.Windows.Forms.Button();
            this.buttonWord = new System.Windows.Forms.Button();
            this.buttonPdf = new System.Windows.Forms.Button();
            this.buttonAzalt = new System.Windows.Forms.Button();
            this.buttonArttir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.ShowCloseButton = false;
            this.crystalReportViewer1.ShowCopyButton = false;
            this.crystalReportViewer1.ShowExportButton = false;
            this.crystalReportViewer1.ShowGotoPageButton = false;
            this.crystalReportViewer1.ShowGroupTreeButton = false;
            this.crystalReportViewer1.ShowLogo = false;
            this.crystalReportViewer1.ShowParameterPanelButton = false;
            this.crystalReportViewer1.ShowRefreshButton = false;
            this.crystalReportViewer1.ShowTextSearchButton = false;
            this.crystalReportViewer1.Size = new System.Drawing.Size(1018, 626);
            this.crystalReportViewer1.TabIndex = 38;
            this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // comboAdisyonAyar
            // 
            this.comboAdisyonAyar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboAdisyonAyar.Enabled = false;
            this.comboAdisyonAyar.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboAdisyonAyar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.comboAdisyonAyar.FormattingEnabled = true;
            this.comboAdisyonAyar.Items.AddRange(new object[] {
            "Bugün",
            "Dün",
            "Bu Hafta",
            "Önceki Hafta",
            "Bu Ay",
            "Önceki Ay",
            "Seçilen Tarih"});
            this.comboAdisyonAyar.Location = new System.Drawing.Point(200, 7);
            this.comboAdisyonAyar.Name = "comboAdisyonAyar";
            this.comboAdisyonAyar.Size = new System.Drawing.Size(131, 34);
            this.comboAdisyonAyar.TabIndex = 46;
            this.comboAdisyonAyar.SelectedIndexChanged += new System.EventHandler(this.comboAdisyonAyar_SelectedIndexChanged);
            this.comboAdisyonAyar.Click += new System.EventHandler(this.comboAdisyonAyar_Click);
            this.comboAdisyonAyar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboAdisyonAyar_KeyPress);
            this.comboAdisyonAyar.Leave += new System.EventHandler(this.comboAdisyonAyar_Leave);
            // 
            // dateBitis
            // 
            this.dateBitis.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dateBitis.CalendarFont = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dateBitis.CalendarForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dateBitis.CalendarMonthBackground = System.Drawing.SystemColors.ActiveCaption;
            this.dateBitis.CalendarTitleForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dateBitis.CalendarTrailingForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dateBitis.CustomFormat = "d MMMM yyy - HH:mm";
            this.dateBitis.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dateBitis.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateBitis.Location = new System.Drawing.Point(602, 7);
            this.dateBitis.Name = "dateBitis";
            this.dateBitis.Size = new System.Drawing.Size(259, 33);
            this.dateBitis.TabIndex = 43;
            this.dateBitis.Value = new System.DateTime(2014, 6, 6, 10, 45, 0, 0);
            this.dateBitis.ValueChanged += new System.EventHandler(this.dateBitis_ValueChanged);
            this.dateBitis.Enter += new System.EventHandler(this.dateBitis_Enter);
            // 
            // dateBaslangic
            // 
            this.dateBaslangic.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dateBaslangic.CalendarFont = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dateBaslangic.CalendarForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dateBaslangic.CalendarMonthBackground = System.Drawing.SystemColors.ActiveCaption;
            this.dateBaslangic.CalendarTitleForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dateBaslangic.CalendarTrailingForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dateBaslangic.CustomFormat = "d MMMM yyy - HH:mm";
            this.dateBaslangic.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dateBaslangic.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateBaslangic.Location = new System.Drawing.Point(337, 7);
            this.dateBaslangic.Name = "dateBaslangic";
            this.dateBaslangic.Size = new System.Drawing.Size(259, 33);
            this.dateBaslangic.TabIndex = 42;
            this.dateBaslangic.Value = new System.DateTime(2014, 9, 1, 5, 29, 0, 0);
            this.dateBaslangic.ValueChanged += new System.EventHandler(this.dateBitis_ValueChanged);
            this.dateBaslangic.Enter += new System.EventHandler(this.dateBaslangic_Enter);
            // 
            // buttonRaporla
            // 
            this.buttonRaporla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRaporla.BackColor = System.Drawing.Color.White;
            this.buttonRaporla.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.buttonRaporla.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonRaporla.Location = new System.Drawing.Point(0, 0);
            this.buttonRaporla.Name = "buttonRaporla";
            this.buttonRaporla.Size = new System.Drawing.Size(1015, 626);
            this.buttonRaporla.TabIndex = 47;
            this.buttonRaporla.TabStop = false;
            this.buttonRaporla.Text = "Rapor Çıkar \r\n(Dikkat:Raporlama uzun sürebilir)";
            this.buttonRaporla.UseVisualStyleBackColor = false;
            this.buttonRaporla.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonExcel
            // 
            this.buttonExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExcel.BackColor = System.Drawing.Color.Transparent;
            this.buttonExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExcel.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonExcel.ForeColor = System.Drawing.Color.Transparent;
            this.buttonExcel.Image = global::ROPv1.Properties.Resources.excel;
            this.buttonExcel.Location = new System.Drawing.Point(772, 521);
            this.buttonExcel.Name = "buttonExcel";
            this.buttonExcel.Size = new System.Drawing.Size(64, 72);
            this.buttonExcel.TabIndex = 50;
            this.buttonExcel.TabStop = false;
            this.buttonExcel.UseVisualStyleBackColor = false;
            this.buttonExcel.Click += new System.EventHandler(this.buttonExcel_Click);
            // 
            // buttonWord
            // 
            this.buttonWord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonWord.BackColor = System.Drawing.Color.Transparent;
            this.buttonWord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonWord.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonWord.ForeColor = System.Drawing.Color.Transparent;
            this.buttonWord.Image = global::ROPv1.Properties.Resources.word;
            this.buttonWord.Location = new System.Drawing.Point(912, 521);
            this.buttonWord.Name = "buttonWord";
            this.buttonWord.Size = new System.Drawing.Size(64, 72);
            this.buttonWord.TabIndex = 49;
            this.buttonWord.TabStop = false;
            this.buttonWord.UseVisualStyleBackColor = false;
            this.buttonWord.Click += new System.EventHandler(this.buttonWord_Click);
            // 
            // buttonPdf
            // 
            this.buttonPdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPdf.BackColor = System.Drawing.Color.Transparent;
            this.buttonPdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPdf.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonPdf.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonPdf.Image = global::ROPv1.Properties.Resources.pdf;
            this.buttonPdf.Location = new System.Drawing.Point(842, 521);
            this.buttonPdf.Name = "buttonPdf";
            this.buttonPdf.Size = new System.Drawing.Size(64, 72);
            this.buttonPdf.TabIndex = 48;
            this.buttonPdf.TabStop = false;
            this.buttonPdf.UseVisualStyleBackColor = false;
            this.buttonPdf.Click += new System.EventHandler(this.buttonPdf_Click);
            // 
            // buttonAzalt
            // 
            this.buttonAzalt.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonAzalt.BackColor = System.Drawing.SystemColors.Window;
            this.buttonAzalt.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonAzalt.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonAzalt.Image = global::ROPv1.Properties.Resources.downIcon;
            this.buttonAzalt.Location = new System.Drawing.Point(940, 7);
            this.buttonAzalt.Name = "buttonAzalt";
            this.buttonAzalt.Size = new System.Drawing.Size(70, 34);
            this.buttonAzalt.TabIndex = 45;
            this.buttonAzalt.TabStop = false;
            this.buttonAzalt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAzalt.UseVisualStyleBackColor = false;
            this.buttonAzalt.Click += new System.EventHandler(this.buttonAzalt_Click);
            // 
            // buttonArttir
            // 
            this.buttonArttir.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonArttir.BackColor = System.Drawing.Color.White;
            this.buttonArttir.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonArttir.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonArttir.Image = global::ROPv1.Properties.Resources.upIcon;
            this.buttonArttir.Location = new System.Drawing.Point(866, 7);
            this.buttonArttir.Name = "buttonArttir";
            this.buttonArttir.Size = new System.Drawing.Size(70, 34);
            this.buttonArttir.TabIndex = 44;
            this.buttonArttir.TabStop = false;
            this.buttonArttir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonArttir.UseVisualStyleBackColor = false;
            this.buttonArttir.Click += new System.EventHandler(this.buttonArttir_Click);
            // 
            // Raporlar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonRaporla);
            this.Controls.Add(this.buttonExcel);
            this.Controls.Add(this.buttonWord);
            this.Controls.Add(this.buttonPdf);
            this.Controls.Add(this.comboAdisyonAyar);
            this.Controls.Add(this.buttonAzalt);
            this.Controls.Add(this.buttonArttir);
            this.Controls.Add(this.dateBitis);
            this.Controls.Add(this.dateBaslangic);
            this.Controls.Add(this.crystalReportViewer1);
            this.Name = "Raporlar";
            this.Size = new System.Drawing.Size(1015, 626);
            this.Load += new System.EventHandler(this.Raporlar_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.ComboBox comboAdisyonAyar;
        private System.Windows.Forms.Button buttonAzalt;
        private System.Windows.Forms.Button buttonArttir;
        private System.Windows.Forms.DateTimePicker dateBitis;
        private System.Windows.Forms.DateTimePicker dateBaslangic;
        private System.Windows.Forms.Button buttonRaporla;
        private System.Windows.Forms.Button buttonPdf;
        private System.Windows.Forms.Button buttonWord;
        private System.Windows.Forms.Button buttonExcel;

    }
}
