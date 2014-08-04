namespace ROPv1
{
    partial class UrunlerTusu
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
            GlacialComponents.Controls.GLColumn glColumn1 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn2 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn3 = new GlacialComponents.Controls.GLColumn();
            this.buttonSaveNewProduct = new System.Windows.Forms.Button();
            this.glacialListUrunler = new GlacialComponents.Controls.GlacialList();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.labelUrunSayisiYazisi = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonSaveNewProduct
            // 
            this.buttonSaveNewProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveNewProduct.BackColor = System.Drawing.SystemColors.Window;
            this.buttonSaveNewProduct.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonSaveNewProduct.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonSaveNewProduct.Image = global::ROPv1.Properties.Resources.icon;
            this.buttonSaveNewProduct.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSaveNewProduct.Location = new System.Drawing.Point(876, 572);
            this.buttonSaveNewProduct.Name = "buttonSaveNewProduct";
            this.buttonSaveNewProduct.Size = new System.Drawing.Size(135, 48);
            this.buttonSaveNewProduct.TabIndex = 90;
            this.buttonSaveNewProduct.Text = "Kaydet     ";
            this.buttonSaveNewProduct.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSaveNewProduct.UseVisualStyleBackColor = false;
            this.buttonSaveNewProduct.Click += new System.EventHandler(this.buttonSaveNewProduct_Click);
            // 
            // glacialListUrunler
            // 
            this.glacialListUrunler.AllowColumnResize = false;
            this.glacialListUrunler.AllowMultiselect = false;
            this.glacialListUrunler.AlternateBackground = System.Drawing.Color.DarkGreen;
            this.glacialListUrunler.AlternatingColors = false;
            this.glacialListUrunler.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.glacialListUrunler.AutoHeight = true;
            this.glacialListUrunler.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.glacialListUrunler.BackgroundStretchToFit = true;
            glColumn1.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn1.CheckBoxes = false;
            glColumn1.ImageIndex = -1;
            glColumn1.Name = "Column3";
            glColumn1.NumericSort = false;
            glColumn1.Text = "    Ürün";
            glColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn1.Width = 450;
            glColumn2.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn2.CheckBoxes = false;
            glColumn2.ImageIndex = -1;
            glColumn2.Name = "Column2";
            glColumn2.NumericSort = false;
            glColumn2.Text = "Porsiyon";
            glColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            glColumn2.Width = 250;
            glColumn3.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn3.CheckBoxes = false;
            glColumn3.ImageIndex = -1;
            glColumn3.Name = "Column4";
            glColumn3.NumericSort = false;
            glColumn3.Text = "Mutfak Bildirimi";
            glColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            glColumn3.Width = 299;
            this.glacialListUrunler.Columns.AddRange(new GlacialComponents.Controls.GLColumn[] {
            glColumn1,
            glColumn2,
            glColumn3});
            this.glacialListUrunler.ControlStyle = GlacialComponents.Controls.GLControlStyles.XP;
            this.glacialListUrunler.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.glacialListUrunler.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.glacialListUrunler.FullRowSelect = true;
            this.glacialListUrunler.GridColor = System.Drawing.Color.LightGray;
            this.glacialListUrunler.GridLines = GlacialComponents.Controls.GLGridLines.gridBoth;
            this.glacialListUrunler.GridLineStyle = GlacialComponents.Controls.GLGridLineStyles.gridSolid;
            this.glacialListUrunler.GridTypes = GlacialComponents.Controls.GLGridTypes.gridOnExists;
            this.glacialListUrunler.HeaderHeight = 30;
            this.glacialListUrunler.HeaderVisible = true;
            this.glacialListUrunler.HeaderWordWrap = false;
            this.glacialListUrunler.HotColumnTracking = false;
            this.glacialListUrunler.HotItemTracking = false;
            this.glacialListUrunler.HotTrackingColor = System.Drawing.Color.LightGray;
            this.glacialListUrunler.HoverEvents = false;
            this.glacialListUrunler.HoverTime = 1;
            this.glacialListUrunler.ImageList = null;
            this.glacialListUrunler.ItemHeight = 23;
            this.glacialListUrunler.ItemWordWrap = false;
            this.glacialListUrunler.Location = new System.Drawing.Point(8, 8);
            this.glacialListUrunler.Name = "glacialListUrunler";
            this.glacialListUrunler.Selectable = true;
            this.glacialListUrunler.SelectedTextColor = System.Drawing.Color.White;
            this.glacialListUrunler.SelectionColor = System.Drawing.SystemColors.ActiveCaption;
            this.glacialListUrunler.ShowBorder = true;
            this.glacialListUrunler.ShowFocusRect = false;
            this.glacialListUrunler.Size = new System.Drawing.Size(1003, 558);
            this.glacialListUrunler.SortType = GlacialComponents.Controls.SortTypes.InsertionSort;
            this.glacialListUrunler.SuperFlatHeaderColor = System.Drawing.Color.White;
            this.glacialListUrunler.TabIndex = 91;
            this.glacialListUrunler.Text = "glacialList1";
            this.glacialListUrunler.SizeChanged += new System.EventHandler(this.glacialListUrunler_SizeChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // labelUrunSayisiYazisi
            // 
            this.labelUrunSayisiYazisi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelUrunSayisiYazisi.AutoSize = true;
            this.labelUrunSayisiYazisi.Font = new System.Drawing.Font("Arial", 12.75F, System.Drawing.FontStyle.Bold);
            this.labelUrunSayisiYazisi.ForeColor = System.Drawing.SystemColors.Window;
            this.labelUrunSayisiYazisi.Location = new System.Drawing.Point(26, 572);
            this.labelUrunSayisiYazisi.Name = "labelUrunSayisiYazisi";
            this.labelUrunSayisiYazisi.Size = new System.Drawing.Size(834, 38);
            this.labelUrunSayisiYazisi.TabIndex = 92;
            this.labelUrunSayisiYazisi.Text = "Porsiyon = Yarım ve çeyrek porsiyon olup olayamacağının belirlenmesi - çeyrek por" +
    "siyon yarımı da kapsar\r\n\r\n";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(26, 598);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(623, 19);
            this.label1.TabIndex = 93;
            this.label1.Text = "Mutfak Bildirimi = Ürün sipariş edildiğinde mutfak yazıcısından bildirim alınması" +
    "";
            // 
            // UrunlerTusu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelUrunSayisiYazisi);
            this.Controls.Add(this.glacialListUrunler);
            this.Controls.Add(this.buttonSaveNewProduct);
            this.Name = "UrunlerTusu";
            this.Size = new System.Drawing.Size(1018, 626);
            this.Load += new System.EventHandler(this.UrunlerTusu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSaveNewProduct;
        private GlacialComponents.Controls.GlacialList glacialListUrunler;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label labelUrunSayisiYazisi;
        private System.Windows.Forms.Label label1;


    }
}
