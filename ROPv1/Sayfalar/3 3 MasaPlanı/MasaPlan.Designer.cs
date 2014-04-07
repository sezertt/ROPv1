namespace ROPv1
{
    partial class MasaPlan
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
            this.treeMasaPlanName = new System.Windows.Forms.TreeView();
            this.newTableForm = new System.Windows.Forms.GroupBox();
            this.numericTableCount = new System.Windows.Forms.NumericUpDown();
            this.textTableDesignName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonSaveNewTable = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tablePanel = new System.Windows.Forms.TableLayoutPanel();
            this.textTableName = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.buttonEditDesign = new System.Windows.Forms.Button();
            this.buttonDeleteTable = new System.Windows.Forms.Button();
            this.buttonAddTableDesign = new System.Windows.Forms.Button();
            this.newTableForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericTableCount)).BeginInit();
            this.SuspendLayout();
            // 
            // keyboardcontrol1
            // 
            this.keyboardcontrol1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.keyboardcontrol1.ForeColor = System.Drawing.SystemColors.Window;
            this.keyboardcontrol1.KeyboardType = KeyboardClassLibrary.BoW.Standard;
            this.keyboardcontrol1.Location = new System.Drawing.Point(0, 356);
            this.keyboardcontrol1.Name = "keyboardcontrol1";
            this.keyboardcontrol1.Size = new System.Drawing.Size(993, 282);
            this.keyboardcontrol1.TabIndex = 18;
            this.keyboardcontrol1.UserKeyPressed += new KeyboardClassLibrary.KeyboardDelegate(this.keyboardcontrol1_UserKeyPressed);
            // 
            // treeMasaPlanName
            // 
            this.treeMasaPlanName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeMasaPlanName.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.treeMasaPlanName.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.treeMasaPlanName.FullRowSelect = true;
            this.treeMasaPlanName.HideSelection = false;
            this.treeMasaPlanName.HotTracking = true;
            this.treeMasaPlanName.Indent = 8;
            this.treeMasaPlanName.ItemHeight = 35;
            this.treeMasaPlanName.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.treeMasaPlanName.Location = new System.Drawing.Point(5, 5);
            this.treeMasaPlanName.Name = "treeMasaPlanName";
            this.treeMasaPlanName.ShowLines = false;
            this.treeMasaPlanName.ShowRootLines = false;
            this.treeMasaPlanName.Size = new System.Drawing.Size(189, 287);
            this.treeMasaPlanName.TabIndex = 19;
            this.treeMasaPlanName.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.changeTableDesign);
            // 
            // newTableForm
            // 
            this.newTableForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.newTableForm.BackColor = System.Drawing.Color.White;
            this.newTableForm.Controls.Add(this.numericTableCount);
            this.newTableForm.Controls.Add(this.textTableDesignName);
            this.newTableForm.Controls.Add(this.label7);
            this.newTableForm.Controls.Add(this.label5);
            this.newTableForm.Controls.Add(this.buttonSaveNewTable);
            this.newTableForm.Controls.Add(this.buttonCancel);
            this.newTableForm.Enabled = false;
            this.newTableForm.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.newTableForm.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.newTableForm.Location = new System.Drawing.Point(719, 5);
            this.newTableForm.Name = "newTableForm";
            this.newTableForm.Size = new System.Drawing.Size(262, 214);
            this.newTableForm.TabIndex = 22;
            this.newTableForm.TabStop = false;
            this.newTableForm.Text = "Yeni Masa Planı";
            // 
            // numericTableCount
            // 
            this.numericTableCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericTableCount.Enabled = false;
            this.numericTableCount.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.numericTableCount.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.numericTableCount.Location = new System.Drawing.Point(9, 117);
            this.numericTableCount.Maximum = new decimal(new int[] {
            42,
            0,
            0,
            0});
            this.numericTableCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericTableCount.Name = "numericTableCount";
            this.numericTableCount.Size = new System.Drawing.Size(243, 32);
            this.numericTableCount.TabIndex = 3;
            this.numericTableCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // textTableDesignName
            // 
            this.textTableDesignName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textTableDesignName.ContextMenuStrip = this.contextMenuStrip1;
            this.textTableDesignName.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textTableDesignName.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textTableDesignName.Location = new System.Drawing.Point(9, 54);
            this.textTableDesignName.Name = "textTableDesignName";
            this.textTableDesignName.Size = new System.Drawing.Size(244, 32);
            this.textTableDesignName.TabIndex = 2;
            this.textTableDesignName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textTableName_KeyPress);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label7.Location = new System.Drawing.Point(5, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(238, 24);
            this.label7.TabIndex = 25;
            this.label7.Text = "Masa Sayısı(Max = 42):";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Location = new System.Drawing.Point(5, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(194, 24);
            this.label5.TabIndex = 19;
            this.label5.Text = "Masa Planının Adı:";
            // 
            // buttonSaveNewTable
            // 
            this.buttonSaveNewTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveNewTable.BackColor = System.Drawing.SystemColors.Window;
            this.buttonSaveNewTable.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonSaveNewTable.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonSaveNewTable.Image = global::ROPv1.Properties.Resources.icon;
            this.buttonSaveNewTable.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSaveNewTable.Location = new System.Drawing.Point(152, 160);
            this.buttonSaveNewTable.Name = "buttonSaveNewTable";
            this.buttonSaveNewTable.Size = new System.Drawing.Size(101, 45);
            this.buttonSaveNewTable.TabIndex = 5;
            this.buttonSaveNewTable.Text = "Kaydet";
            this.buttonSaveNewTable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSaveNewTable.UseVisualStyleBackColor = false;
            this.buttonSaveNewTable.Click += new System.EventHandler(this.buttonAddNewTableDesign);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.BackColor = System.Drawing.SystemColors.Window;
            this.buttonCancel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonCancel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonCancel.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCancel.Location = new System.Drawing.Point(9, 160);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(101, 45);
            this.buttonCancel.TabIndex = 17;
            this.buttonCancel.TabStop = false;
            this.buttonCancel.Text = "İptal Et";
            this.buttonCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.cancelNewTable);
            // 
            // tablePanel
            // 
            this.tablePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tablePanel.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel.ColumnCount = 7;
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.166667F));
            this.tablePanel.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tablePanel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tablePanel.Location = new System.Drawing.Point(200, 5);
            this.tablePanel.Name = "tablePanel";
            this.tablePanel.RowCount = 6;
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tablePanel.Size = new System.Drawing.Size(513, 345);
            this.tablePanel.TabIndex = 23;
            // 
            // textTableName
            // 
            this.textTableName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textTableName.ContextMenuStrip = this.contextMenuStrip1;
            this.textTableName.Enabled = false;
            this.textTableName.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textTableName.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textTableName.Location = new System.Drawing.Point(728, 305);
            this.textTableName.MaxLength = 7;
            this.textTableName.Name = "textTableName";
            this.textTableName.Size = new System.Drawing.Size(244, 32);
            this.textTableName.TabIndex = 28;
            this.textTableName.TextChanged += new System.EventHandler(this.TableName_TextChanged);
            this.textTableName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textTableName_KeyPress);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(724, 278);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 24);
            this.label1.TabIndex = 29;
            this.label1.Text = "Masa Adı:";
            // 
            // buttonEditDesign
            // 
            this.buttonEditDesign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditDesign.BackColor = System.Drawing.SystemColors.Window;
            this.buttonEditDesign.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonEditDesign.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonEditDesign.Image = global::ROPv1.Properties.Resources.editicon;
            this.buttonEditDesign.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEditDesign.Location = new System.Drawing.Point(862, 225);
            this.buttonEditDesign.Name = "buttonEditDesign";
            this.buttonEditDesign.Size = new System.Drawing.Size(110, 52);
            this.buttonEditDesign.TabIndex = 30;
            this.buttonEditDesign.TabStop = false;
            this.buttonEditDesign.Text = "      Düzenle";
            this.buttonEditDesign.UseVisualStyleBackColor = false;
            this.buttonEditDesign.Click += new System.EventHandler(this.editDesignPressed);
            // 
            // buttonDeleteTable
            // 
            this.buttonDeleteTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteTable.BackColor = System.Drawing.SystemColors.Window;
            this.buttonDeleteTable.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonDeleteTable.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonDeleteTable.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonDeleteTable.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDeleteTable.Location = new System.Drawing.Point(728, 225);
            this.buttonDeleteTable.Name = "buttonDeleteTable";
            this.buttonDeleteTable.Size = new System.Drawing.Size(101, 52);
            this.buttonDeleteTable.TabIndex = 0;
            this.buttonDeleteTable.TabStop = false;
            this.buttonDeleteTable.Text = "Planı Sil";
            this.buttonDeleteTable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonDeleteTable.UseVisualStyleBackColor = false;
            this.buttonDeleteTable.Click += new System.EventHandler(this.deleteTableDesign);
            // 
            // buttonAddTableDesign
            // 
            this.buttonAddTableDesign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddTableDesign.BackColor = System.Drawing.SystemColors.Window;
            this.buttonAddTableDesign.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonAddTableDesign.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonAddTableDesign.Image = global::ROPv1.Properties.Resources.add;
            this.buttonAddTableDesign.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddTableDesign.Location = new System.Drawing.Point(5, 298);
            this.buttonAddTableDesign.Name = "buttonAddTableDesign";
            this.buttonAddTableDesign.Size = new System.Drawing.Size(189, 52);
            this.buttonAddTableDesign.TabIndex = 21;
            this.buttonAddTableDesign.TabStop = false;
            this.buttonAddTableDesign.Text = "    Yeni Masa Planı Oluştur";
            this.buttonAddTableDesign.UseVisualStyleBackColor = false;
            this.buttonAddTableDesign.Click += new System.EventHandler(this.addNewTableDesign);
            // 
            // MasaPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.buttonEditDesign);
            this.Controls.Add(this.textTableName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonDeleteTable);
            this.Controls.Add(this.tablePanel);
            this.Controls.Add(this.newTableForm);
            this.Controls.Add(this.buttonAddTableDesign);
            this.Controls.Add(this.treeMasaPlanName);
            this.Controls.Add(this.keyboardcontrol1);
            this.Name = "MasaPlan";
            this.Size = new System.Drawing.Size(993, 638);
            this.Load += new System.EventHandler(this.MasaPlan_Load);
            this.Leave += new System.EventHandler(this.Leaving);
            this.newTableForm.ResumeLayout(false);
            this.newTableForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericTableCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private KeyboardClassLibrary.Keyboardcontrol keyboardcontrol1;
        private System.Windows.Forms.TreeView treeMasaPlanName;
        private System.Windows.Forms.Button buttonAddTableDesign;
        private System.Windows.Forms.GroupBox newTableForm;
        private System.Windows.Forms.Button buttonDeleteTable;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonSaveNewTable;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textTableDesignName;
        private System.Windows.Forms.NumericUpDown numericTableCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TableLayoutPanel tablePanel;
        private System.Windows.Forms.TextBox textTableName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonEditDesign;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

    }
}
