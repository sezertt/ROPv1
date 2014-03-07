namespace ROPv1
{
    partial class UrunMenuleri
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
            this.treeMenununUrunler = new System.Windows.Forms.TreeView();
            this.buttonDeleteUrun = new System.Windows.Forms.Button();
            this.buttonSaveMenu = new System.Windows.Forms.Button();
            this.buttonDeleteMenu = new System.Windows.Forms.Button();
            this.buttonAddUrun = new System.Windows.Forms.Button();
            this.treeUrunler = new System.Windows.Forms.TreeView();
            this.newUrunMenuForm = new System.Windows.Forms.GroupBox();
            this.textboxMenuName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.treeMenuler = new System.Windows.Forms.TreeView();
            this.keyboardcontrol1 = new KeyboardClassLibrary.Keyboardcontrol();
            this.buttonAddNewMenu = new System.Windows.Forms.Button();
            this.newUrunMenuForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeMenununUrunler
            // 
            this.treeMenununUrunler.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeMenununUrunler.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.treeMenununUrunler.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.treeMenununUrunler.FullRowSelect = true;
            this.treeMenununUrunler.HideSelection = false;
            this.treeMenununUrunler.HotTracking = true;
            this.treeMenununUrunler.Indent = 8;
            this.treeMenununUrunler.ItemHeight = 35;
            this.treeMenununUrunler.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.treeMenununUrunler.Location = new System.Drawing.Point(298, 5);
            this.treeMenununUrunler.Name = "treeMenununUrunler";
            this.treeMenununUrunler.ShowLines = false;
            this.treeMenununUrunler.ShowRootLines = false;
            this.treeMenununUrunler.Size = new System.Drawing.Size(200, 333);
            this.treeMenununUrunler.TabIndex = 52;
            // 
            // buttonDeleteUrun
            // 
            this.buttonDeleteUrun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteUrun.BackColor = System.Drawing.SystemColors.Window;
            this.buttonDeleteUrun.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonDeleteUrun.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonDeleteUrun.Image = global::ROPv1.Properties.Resources.righticon;
            this.buttonDeleteUrun.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonDeleteUrun.Location = new System.Drawing.Point(6, 158);
            this.buttonDeleteUrun.Name = "buttonDeleteUrun";
            this.buttonDeleteUrun.Size = new System.Drawing.Size(266, 60);
            this.buttonDeleteUrun.TabIndex = 43;
            this.buttonDeleteUrun.TabStop = false;
            this.buttonDeleteUrun.Text = "   Menüden Çıkar   ";
            this.buttonDeleteUrun.UseVisualStyleBackColor = false;
            // 
            // buttonSaveMenu
            // 
            this.buttonSaveMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveMenu.BackColor = System.Drawing.SystemColors.Window;
            this.buttonSaveMenu.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonSaveMenu.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonSaveMenu.Image = global::ROPv1.Properties.Resources.icon;
            this.buttonSaveMenu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSaveMenu.Location = new System.Drawing.Point(162, 224);
            this.buttonSaveMenu.Name = "buttonSaveMenu";
            this.buttonSaveMenu.Size = new System.Drawing.Size(110, 45);
            this.buttonSaveMenu.TabIndex = 5;
            this.buttonSaveMenu.Text = "Kaydet";
            this.buttonSaveMenu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSaveMenu.UseVisualStyleBackColor = false;
            // 
            // buttonDeleteMenu
            // 
            this.buttonDeleteMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteMenu.BackColor = System.Drawing.SystemColors.Window;
            this.buttonDeleteMenu.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonDeleteMenu.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonDeleteMenu.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonDeleteMenu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDeleteMenu.Location = new System.Drawing.Point(6, 224);
            this.buttonDeleteMenu.Name = "buttonDeleteMenu";
            this.buttonDeleteMenu.Size = new System.Drawing.Size(124, 45);
            this.buttonDeleteMenu.TabIndex = 31;
            this.buttonDeleteMenu.TabStop = false;
            this.buttonDeleteMenu.Text = "Menüyü Sil";
            this.buttonDeleteMenu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonDeleteMenu.UseVisualStyleBackColor = false;
            // 
            // buttonAddUrun
            // 
            this.buttonAddUrun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddUrun.BackColor = System.Drawing.SystemColors.Window;
            this.buttonAddUrun.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonAddUrun.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonAddUrun.Image = global::ROPv1.Properties.Resources.lefticon;
            this.buttonAddUrun.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddUrun.Location = new System.Drawing.Point(6, 92);
            this.buttonAddUrun.Name = "buttonAddUrun";
            this.buttonAddUrun.Size = new System.Drawing.Size(266, 60);
            this.buttonAddUrun.TabIndex = 46;
            this.buttonAddUrun.TabStop = false;
            this.buttonAddUrun.Text = "Menüye Ekle";
            this.buttonAddUrun.UseVisualStyleBackColor = false;
            // 
            // treeUrunler
            // 
            this.treeUrunler.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeUrunler.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.treeUrunler.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.treeUrunler.FullRowSelect = true;
            this.treeUrunler.HideSelection = false;
            this.treeUrunler.HotTracking = true;
            this.treeUrunler.Indent = 8;
            this.treeUrunler.ItemHeight = 35;
            this.treeUrunler.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.treeUrunler.Location = new System.Drawing.Point(788, 5);
            this.treeUrunler.Name = "treeUrunler";
            this.treeUrunler.ShowLines = false;
            this.treeUrunler.ShowRootLines = false;
            this.treeUrunler.Size = new System.Drawing.Size(200, 333);
            this.treeUrunler.TabIndex = 49;
            // 
            // newUrunMenuForm
            // 
            this.newUrunMenuForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.newUrunMenuForm.BackColor = System.Drawing.Color.White;
            this.newUrunMenuForm.Controls.Add(this.buttonAddUrun);
            this.newUrunMenuForm.Controls.Add(this.buttonDeleteUrun);
            this.newUrunMenuForm.Controls.Add(this.textboxMenuName);
            this.newUrunMenuForm.Controls.Add(this.label5);
            this.newUrunMenuForm.Controls.Add(this.buttonSaveMenu);
            this.newUrunMenuForm.Controls.Add(this.buttonDeleteMenu);
            this.newUrunMenuForm.Controls.Add(this.buttonCancel);
            this.newUrunMenuForm.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.newUrunMenuForm.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.newUrunMenuForm.Location = new System.Drawing.Point(504, 5);
            this.newUrunMenuForm.Name = "newUrunMenuForm";
            this.newUrunMenuForm.Size = new System.Drawing.Size(278, 275);
            this.newUrunMenuForm.TabIndex = 47;
            this.newUrunMenuForm.TabStop = false;
            this.newUrunMenuForm.Text = "Yeni Menü";
            // 
            // textboxMenuName
            // 
            this.textboxMenuName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxMenuName.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textboxMenuName.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textboxMenuName.Location = new System.Drawing.Point(6, 54);
            this.textboxMenuName.Name = "textboxMenuName";
            this.textboxMenuName.Size = new System.Drawing.Size(266, 32);
            this.textboxMenuName.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Location = new System.Drawing.Point(6, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 24);
            this.label5.TabIndex = 19;
            this.label5.Text = "Menu Adı:";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonCancel.BackColor = System.Drawing.SystemColors.Window;
            this.buttonCancel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonCancel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonCancel.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCancel.Location = new System.Drawing.Point(6, 224);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(110, 44);
            this.buttonCancel.TabIndex = 40;
            this.buttonCancel.TabStop = false;
            this.buttonCancel.Text = "İptal Et  ";
            this.buttonCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonCancel.UseVisualStyleBackColor = false;
            // 
            // treeMenuler
            // 
            this.treeMenuler.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeMenuler.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.treeMenuler.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.treeMenuler.FullRowSelect = true;
            this.treeMenuler.HideSelection = false;
            this.treeMenuler.HotTracking = true;
            this.treeMenuler.Indent = 8;
            this.treeMenuler.ItemHeight = 35;
            this.treeMenuler.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.treeMenuler.Location = new System.Drawing.Point(5, 5);
            this.treeMenuler.Name = "treeMenuler";
            this.treeMenuler.ShowLines = false;
            this.treeMenuler.ShowRootLines = false;
            this.treeMenuler.Size = new System.Drawing.Size(287, 333);
            this.treeMenuler.TabIndex = 48;
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
            this.keyboardcontrol1.TabIndex = 46;
            // 
            // buttonAddNewMenu
            // 
            this.buttonAddNewMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddNewMenu.BackColor = System.Drawing.SystemColors.Window;
            this.buttonAddNewMenu.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonAddNewMenu.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonAddNewMenu.Image = global::ROPv1.Properties.Resources.add;
            this.buttonAddNewMenu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddNewMenu.Location = new System.Drawing.Point(504, 286);
            this.buttonAddNewMenu.Name = "buttonAddNewMenu";
            this.buttonAddNewMenu.Size = new System.Drawing.Size(278, 52);
            this.buttonAddNewMenu.TabIndex = 45;
            this.buttonAddNewMenu.TabStop = false;
            this.buttonAddNewMenu.Text = "     Yeni Menü Oluştur";
            this.buttonAddNewMenu.UseVisualStyleBackColor = false;
            // 
            // UrunMenuleri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeMenununUrunler);
            this.Controls.Add(this.treeUrunler);
            this.Controls.Add(this.newUrunMenuForm);
            this.Controls.Add(this.treeMenuler);
            this.Controls.Add(this.keyboardcontrol1);
            this.Controls.Add(this.buttonAddNewMenu);
            this.Name = "UrunMenuleri";
            this.Size = new System.Drawing.Size(993, 626);
            this.newUrunMenuForm.ResumeLayout(false);
            this.newUrunMenuForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeMenununUrunler;
        private System.Windows.Forms.Button buttonDeleteUrun;
        private System.Windows.Forms.Button buttonSaveMenu;
        private System.Windows.Forms.Button buttonDeleteMenu;
        private System.Windows.Forms.Button buttonAddUrun;
        private System.Windows.Forms.TreeView treeUrunler;
        private System.Windows.Forms.GroupBox newUrunMenuForm;
        private System.Windows.Forms.TextBox textboxMenuName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TreeView treeMenuler;
        private KeyboardClassLibrary.Keyboardcontrol keyboardcontrol1;
        private System.Windows.Forms.Button buttonAddNewMenu;


    }
}
