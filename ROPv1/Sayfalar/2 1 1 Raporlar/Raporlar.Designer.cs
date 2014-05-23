﻿namespace ROPv1
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.keyboardcontrol1 = new KeyboardClassLibrary.Keyboardcontrol();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.comboAdisyonAyar = new System.Windows.Forms.ComboBox();
            this.buttonAzalt = new System.Windows.Forms.Button();
            this.buttonArttir = new System.Windows.Forms.Button();
            this.dateBitis = new System.Windows.Forms.DateTimePicker();
            this.dateBaslangic = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
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
            this.keyboardcontrol1.TabIndex = 37;
            this.keyboardcontrol1.Visible = false;
            this.keyboardcontrol1.UserKeyPressed += new KeyboardClassLibrary.KeyboardDelegate(this.keyboardcontrol1_UserKeyPressed);
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.crystalReportViewer1.CachedPageNumberPerDoc = 10;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Location = new System.Drawing.Point(0, 48);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.ShowCloseButton = false;
            this.crystalReportViewer1.ShowCopyButton = false;
            this.crystalReportViewer1.ShowGotoPageButton = false;
            this.crystalReportViewer1.ShowGroupTreeButton = false;
            this.crystalReportViewer1.ShowLogo = false;
            this.crystalReportViewer1.ShowParameterPanelButton = false;
            this.crystalReportViewer1.ShowTextSearchButton = false;
            this.crystalReportViewer1.Size = new System.Drawing.Size(997, 578);
            this.crystalReportViewer1.TabIndex = 38;
            this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // comboAdisyonAyar
            // 
            this.comboAdisyonAyar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboAdisyonAyar.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboAdisyonAyar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.comboAdisyonAyar.FormattingEnabled = true;
            this.comboAdisyonAyar.Items.AddRange(new object[] {
            "Açık Adisyonlar",
            "Tüm Adisyonlar",
            "Adisyon ID",
            "Masa Adı",
            "Departman Adı"});
            this.comboAdisyonAyar.Location = new System.Drawing.Point(8, 8);
            this.comboAdisyonAyar.Name = "comboAdisyonAyar";
            this.comboAdisyonAyar.Size = new System.Drawing.Size(242, 34);
            this.comboAdisyonAyar.TabIndex = 46;
            // 
            // buttonAzalt
            // 
            this.buttonAzalt.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonAzalt.BackColor = System.Drawing.SystemColors.Window;
            this.buttonAzalt.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonAzalt.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonAzalt.Image = global::ROPv1.Properties.Resources.downIcon;
            this.buttonAzalt.Location = new System.Drawing.Point(915, 8);
            this.buttonAzalt.Name = "buttonAzalt";
            this.buttonAzalt.Size = new System.Drawing.Size(70, 34);
            this.buttonAzalt.TabIndex = 45;
            this.buttonAzalt.TabStop = false;
            this.buttonAzalt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAzalt.UseVisualStyleBackColor = false;
            // 
            // buttonArttir
            // 
            this.buttonArttir.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonArttir.BackColor = System.Drawing.Color.White;
            this.buttonArttir.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonArttir.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonArttir.Image = global::ROPv1.Properties.Resources.upIcon;
            this.buttonArttir.Location = new System.Drawing.Point(837, 8);
            this.buttonArttir.Name = "buttonArttir";
            this.buttonArttir.Size = new System.Drawing.Size(70, 34);
            this.buttonArttir.TabIndex = 44;
            this.buttonArttir.TabStop = false;
            this.buttonArttir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonArttir.UseVisualStyleBackColor = false;
            // 
            // dateBitis
            // 
            this.dateBitis.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dateBitis.CalendarFont = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dateBitis.CalendarForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dateBitis.CalendarMonthBackground = System.Drawing.SystemColors.ActiveCaption;
            this.dateBitis.CalendarTitleForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dateBitis.CalendarTrailingForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dateBitis.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dateBitis.Location = new System.Drawing.Point(548, 8);
            this.dateBitis.Name = "dateBitis";
            this.dateBitis.Size = new System.Drawing.Size(278, 33);
            this.dateBitis.TabIndex = 43;
            // 
            // dateBaslangic
            // 
            this.dateBaslangic.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dateBaslangic.CalendarFont = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dateBaslangic.CalendarForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dateBaslangic.CalendarMonthBackground = System.Drawing.SystemColors.ActiveCaption;
            this.dateBaslangic.CalendarTitleForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dateBaslangic.CalendarTrailingForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dateBaslangic.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dateBaslangic.Location = new System.Drawing.Point(260, 8);
            this.dateBaslangic.Name = "dateBaslangic";
            this.dateBaslangic.Size = new System.Drawing.Size(278, 33);
            this.dateBaslangic.TabIndex = 42;
            // 
            // Raporlar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboAdisyonAyar);
            this.Controls.Add(this.buttonAzalt);
            this.Controls.Add(this.buttonArttir);
            this.Controls.Add(this.dateBitis);
            this.Controls.Add(this.dateBaslangic);
            this.Controls.Add(this.keyboardcontrol1);
            this.Controls.Add(this.crystalReportViewer1);
            this.Name = "Raporlar";
            this.Size = new System.Drawing.Size(993, 626);
            this.Load += new System.EventHandler(this.Raporlar_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private KeyboardClassLibrary.Keyboardcontrol keyboardcontrol1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.ComboBox comboAdisyonAyar;
        private System.Windows.Forms.Button buttonAzalt;
        private System.Windows.Forms.Button buttonArttir;
        private System.Windows.Forms.DateTimePicker dateBitis;
        private System.Windows.Forms.DateTimePicker dateBaslangic;

    }
}