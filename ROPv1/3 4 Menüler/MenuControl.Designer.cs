namespace ROPv1
{
    partial class MenuControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuControl));
            this.newMenuForm = new System.Windows.Forms.GroupBox();
            this.buttonDeleteKategori = new System.Windows.Forms.Button();
            this.textboxMenuName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonSaveNewMenu = new System.Windows.Forms.Button();
            this.buttonDeleteMenu = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.treeMenuName = new System.Windows.Forms.TreeView();
            this.keyboardcontrol1 = new KeyboardClassLibrary.Keyboardcontrol();
            this.treeNewKategori = new System.Windows.Forms.TreeView();
            this.newKategoriForm = new System.Windows.Forms.GroupBox();
            this.buttonAddKategori = new System.Windows.Forms.Button();
            this.textBoxYeniKategori = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSaveNewKategori = new System.Windows.Forms.Button();
            this.buttonDeleteNewKategori = new System.Windows.Forms.Button();
            this.buttonCancelNewKategori = new System.Windows.Forms.Button();
            this.treeMenuKategori = new System.Windows.Forms.TreeView();
            this.buttonAddNewKategori = new System.Windows.Forms.Button();
            this.buttonAddNewMenu = new System.Windows.Forms.Button();
            this.newMenuForm.SuspendLayout();
            this.newKategoriForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // newMenuForm
            // 
            this.newMenuForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.newMenuForm.BackColor = System.Drawing.Color.White;
            this.newMenuForm.Controls.Add(this.buttonDeleteKategori);
            this.newMenuForm.Controls.Add(this.textboxMenuName);
            this.newMenuForm.Controls.Add(this.label5);
            this.newMenuForm.Controls.Add(this.buttonSaveNewMenu);
            this.newMenuForm.Controls.Add(this.buttonDeleteMenu);
            this.newMenuForm.Controls.Add(this.buttonCancel);
            this.newMenuForm.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.newMenuForm.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.newMenuForm.Location = new System.Drawing.Point(504, 5);
            this.newMenuForm.Name = "newMenuForm";
            this.newMenuForm.Size = new System.Drawing.Size(278, 195);
            this.newMenuForm.TabIndex = 38;
            this.newMenuForm.TabStop = false;
            this.newMenuForm.Text = "Yeni Menü";
            // 
            // buttonDeleteKategori
            // 
            this.buttonDeleteKategori.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteKategori.BackColor = System.Drawing.SystemColors.Window;
            this.buttonDeleteKategori.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonDeleteKategori.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonDeleteKategori.Image = global::ROPv1.Properties.Resources.righticon;
            this.buttonDeleteKategori.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonDeleteKategori.Location = new System.Drawing.Point(5, 94);
            this.buttonDeleteKategori.Name = "buttonDeleteKategori";
            this.buttonDeleteKategori.Size = new System.Drawing.Size(266, 44);
            this.buttonDeleteKategori.TabIndex = 43;
            this.buttonDeleteKategori.TabStop = false;
            this.buttonDeleteKategori.Text = "Kategoriyi Menüden Çıkar   ";
            this.buttonDeleteKategori.UseVisualStyleBackColor = false;
            this.buttonDeleteKategori.Click += new System.EventHandler(this.deleteKategori);
            // 
            // textboxMenuName
            // 
            this.textboxMenuName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxMenuName.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textboxMenuName.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textboxMenuName.Location = new System.Drawing.Point(6, 52);
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
            this.label5.Location = new System.Drawing.Point(6, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 24);
            this.label5.TabIndex = 19;
            this.label5.Text = "Menu Adı:";
            // 
            // buttonSaveNewMenu
            // 
            this.buttonSaveNewMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveNewMenu.BackColor = System.Drawing.SystemColors.Window;
            this.buttonSaveNewMenu.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonSaveNewMenu.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonSaveNewMenu.Image = ((System.Drawing.Image)(resources.GetObject("buttonSaveNewMenu.Image")));
            this.buttonSaveNewMenu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSaveNewMenu.Location = new System.Drawing.Point(161, 144);
            this.buttonSaveNewMenu.Name = "buttonSaveNewMenu";
            this.buttonSaveNewMenu.Size = new System.Drawing.Size(110, 45);
            this.buttonSaveNewMenu.TabIndex = 5;
            this.buttonSaveNewMenu.Text = "Kaydet";
            this.buttonSaveNewMenu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSaveNewMenu.UseVisualStyleBackColor = false;
            this.buttonSaveNewMenu.Click += new System.EventHandler(this.saveMenuButtonPressed);
            // 
            // buttonDeleteMenu
            // 
            this.buttonDeleteMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteMenu.BackColor = System.Drawing.SystemColors.Window;
            this.buttonDeleteMenu.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonDeleteMenu.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonDeleteMenu.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonDeleteMenu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDeleteMenu.Location = new System.Drawing.Point(6, 144);
            this.buttonDeleteMenu.Name = "buttonDeleteMenu";
            this.buttonDeleteMenu.Size = new System.Drawing.Size(124, 45);
            this.buttonDeleteMenu.TabIndex = 31;
            this.buttonDeleteMenu.TabStop = false;
            this.buttonDeleteMenu.Text = "Menüyü Sil";
            this.buttonDeleteMenu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonDeleteMenu.UseVisualStyleBackColor = false;
            this.buttonDeleteMenu.Click += new System.EventHandler(this.deleteMenu);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonCancel.BackColor = System.Drawing.SystemColors.Window;
            this.buttonCancel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonCancel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonCancel.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCancel.Location = new System.Drawing.Point(5, 145);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(110, 44);
            this.buttonCancel.TabIndex = 40;
            this.buttonCancel.TabStop = false;
            this.buttonCancel.Text = "İptal Et  ";
            this.buttonCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.cancelNewMenu);
            // 
            // treeMenuName
            // 
            this.treeMenuName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeMenuName.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.treeMenuName.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.treeMenuName.FullRowSelect = true;
            this.treeMenuName.HideSelection = false;
            this.treeMenuName.HotTracking = true;
            this.treeMenuName.Indent = 8;
            this.treeMenuName.ItemHeight = 35;
            this.treeMenuName.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.treeMenuName.Location = new System.Drawing.Point(5, 5);
            this.treeMenuName.Name = "treeMenuName";
            this.treeMenuName.ShowLines = false;
            this.treeMenuName.ShowRootLines = false;
            this.treeMenuName.Size = new System.Drawing.Size(287, 333);
            this.treeMenuName.TabIndex = 39;
            this.treeMenuName.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.changeMenuName);
            this.treeMenuName.Enter += new System.EventHandler(this.showMenuSide);
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
            this.keyboardcontrol1.UserKeyPressed += new KeyboardClassLibrary.KeyboardDelegate(this.keyboardcontrol1_UserKeyPressed);
            // 
            // treeNewKategori
            // 
            this.treeNewKategori.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeNewKategori.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.treeNewKategori.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.treeNewKategori.FullRowSelect = true;
            this.treeNewKategori.HideSelection = false;
            this.treeNewKategori.HotTracking = true;
            this.treeNewKategori.Indent = 8;
            this.treeNewKategori.ItemHeight = 35;
            this.treeNewKategori.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.treeNewKategori.Location = new System.Drawing.Point(788, 5);
            this.treeNewKategori.Name = "treeNewKategori";
            this.treeNewKategori.ShowLines = false;
            this.treeNewKategori.ShowRootLines = false;
            this.treeNewKategori.Size = new System.Drawing.Size(200, 333);
            this.treeNewKategori.TabIndex = 40;
            this.treeNewKategori.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.changeNewKategori);
            this.treeNewKategori.Enter += new System.EventHandler(this.showKategoriSide);
            // 
            // newKategoriForm
            // 
            this.newKategoriForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.newKategoriForm.BackColor = System.Drawing.Color.White;
            this.newKategoriForm.Controls.Add(this.buttonAddKategori);
            this.newKategoriForm.Controls.Add(this.textBoxYeniKategori);
            this.newKategoriForm.Controls.Add(this.label1);
            this.newKategoriForm.Controls.Add(this.buttonSaveNewKategori);
            this.newKategoriForm.Controls.Add(this.buttonDeleteNewKategori);
            this.newKategoriForm.Controls.Add(this.buttonCancelNewKategori);
            this.newKategoriForm.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.newKategoriForm.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.newKategoriForm.Location = new System.Drawing.Point(504, 5);
            this.newKategoriForm.Name = "newKategoriForm";
            this.newKategoriForm.Size = new System.Drawing.Size(278, 195);
            this.newKategoriForm.TabIndex = 42;
            this.newKategoriForm.TabStop = false;
            this.newKategoriForm.Text = "Yeni Kategori";
            this.newKategoriForm.Visible = false;
            // 
            // buttonAddKategori
            // 
            this.buttonAddKategori.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddKategori.BackColor = System.Drawing.SystemColors.Window;
            this.buttonAddKategori.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonAddKategori.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonAddKategori.Image = global::ROPv1.Properties.Resources.lefticon;
            this.buttonAddKategori.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddKategori.Location = new System.Drawing.Point(6, 94);
            this.buttonAddKategori.Name = "buttonAddKategori";
            this.buttonAddKategori.Size = new System.Drawing.Size(266, 44);
            this.buttonAddKategori.TabIndex = 46;
            this.buttonAddKategori.TabStop = false;
            this.buttonAddKategori.Text = "Menüye Ekle";
            this.buttonAddKategori.UseVisualStyleBackColor = false;
            this.buttonAddKategori.Click += new System.EventHandler(this.saveKategori);
            // 
            // textBoxYeniKategori
            // 
            this.textBoxYeniKategori.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxYeniKategori.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxYeniKategori.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBoxYeniKategori.Location = new System.Drawing.Point(6, 52);
            this.textBoxYeniKategori.Name = "textBoxYeniKategori";
            this.textBoxYeniKategori.Size = new System.Drawing.Size(266, 32);
            this.textBoxYeniKategori.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 24);
            this.label1.TabIndex = 19;
            this.label1.Text = "Kategori Adı:";
            // 
            // buttonSaveNewKategori
            // 
            this.buttonSaveNewKategori.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveNewKategori.BackColor = System.Drawing.SystemColors.Window;
            this.buttonSaveNewKategori.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonSaveNewKategori.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonSaveNewKategori.Image = global::ROPv1.Properties.Resources.icon;
            this.buttonSaveNewKategori.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSaveNewKategori.Location = new System.Drawing.Point(162, 144);
            this.buttonSaveNewKategori.Name = "buttonSaveNewKategori";
            this.buttonSaveNewKategori.Size = new System.Drawing.Size(110, 45);
            this.buttonSaveNewKategori.TabIndex = 5;
            this.buttonSaveNewKategori.Text = "Kaydet";
            this.buttonSaveNewKategori.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSaveNewKategori.UseVisualStyleBackColor = false;
            this.buttonSaveNewKategori.Click += new System.EventHandler(this.saveNewKategoriPressed);
            // 
            // buttonDeleteNewKategori
            // 
            this.buttonDeleteNewKategori.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteNewKategori.BackColor = System.Drawing.SystemColors.Window;
            this.buttonDeleteNewKategori.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonDeleteNewKategori.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonDeleteNewKategori.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonDeleteNewKategori.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDeleteNewKategori.Location = new System.Drawing.Point(6, 145);
            this.buttonDeleteNewKategori.Name = "buttonDeleteNewKategori";
            this.buttonDeleteNewKategori.Size = new System.Drawing.Size(142, 44);
            this.buttonDeleteNewKategori.TabIndex = 31;
            this.buttonDeleteNewKategori.TabStop = false;
            this.buttonDeleteNewKategori.Text = "Kategoriyi Sil";
            this.buttonDeleteNewKategori.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonDeleteNewKategori.UseVisualStyleBackColor = false;
            this.buttonDeleteNewKategori.Click += new System.EventHandler(this.deleteNewKategori);
            // 
            // buttonCancelNewKategori
            // 
            this.buttonCancelNewKategori.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonCancelNewKategori.BackColor = System.Drawing.SystemColors.Window;
            this.buttonCancelNewKategori.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonCancelNewKategori.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonCancelNewKategori.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonCancelNewKategori.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCancelNewKategori.Location = new System.Drawing.Point(6, 145);
            this.buttonCancelNewKategori.Name = "buttonCancelNewKategori";
            this.buttonCancelNewKategori.Size = new System.Drawing.Size(110, 44);
            this.buttonCancelNewKategori.TabIndex = 40;
            this.buttonCancelNewKategori.TabStop = false;
            this.buttonCancelNewKategori.Text = "İptal Et  ";
            this.buttonCancelNewKategori.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonCancelNewKategori.UseVisualStyleBackColor = false;
            this.buttonCancelNewKategori.Click += new System.EventHandler(this.cancelNewKategori);
            // 
            // treeMenuKategori
            // 
            this.treeMenuKategori.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeMenuKategori.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.treeMenuKategori.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.treeMenuKategori.FullRowSelect = true;
            this.treeMenuKategori.HideSelection = false;
            this.treeMenuKategori.HotTracking = true;
            this.treeMenuKategori.Indent = 8;
            this.treeMenuKategori.ItemHeight = 35;
            this.treeMenuKategori.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.treeMenuKategori.Location = new System.Drawing.Point(298, 5);
            this.treeMenuKategori.Name = "treeMenuKategori";
            this.treeMenuKategori.ShowLines = false;
            this.treeMenuKategori.ShowRootLines = false;
            this.treeMenuKategori.Size = new System.Drawing.Size(200, 333);
            this.treeMenuKategori.TabIndex = 44;
            this.treeMenuKategori.Enter += new System.EventHandler(this.showMenuSide);
            // 
            // buttonAddNewKategori
            // 
            this.buttonAddNewKategori.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddNewKategori.BackColor = System.Drawing.SystemColors.Window;
            this.buttonAddNewKategori.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonAddNewKategori.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonAddNewKategori.Image = global::ROPv1.Properties.Resources.add;
            this.buttonAddNewKategori.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddNewKategori.Location = new System.Drawing.Point(504, 286);
            this.buttonAddNewKategori.Name = "buttonAddNewKategori";
            this.buttonAddNewKategori.Size = new System.Drawing.Size(278, 52);
            this.buttonAddNewKategori.TabIndex = 41;
            this.buttonAddNewKategori.TabStop = false;
            this.buttonAddNewKategori.Text = "     Yeni Kategori Oluştur";
            this.buttonAddNewKategori.UseVisualStyleBackColor = false;
            this.buttonAddNewKategori.Click += new System.EventHandler(this.createNewKategoriButtonPressed);
            // 
            // buttonAddNewMenu
            // 
            this.buttonAddNewMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddNewMenu.BackColor = System.Drawing.SystemColors.Window;
            this.buttonAddNewMenu.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonAddNewMenu.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonAddNewMenu.Image = global::ROPv1.Properties.Resources.add;
            this.buttonAddNewMenu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddNewMenu.Location = new System.Drawing.Point(504, 218);
            this.buttonAddNewMenu.Name = "buttonAddNewMenu";
            this.buttonAddNewMenu.Size = new System.Drawing.Size(278, 52);
            this.buttonAddNewMenu.TabIndex = 34;
            this.buttonAddNewMenu.TabStop = false;
            this.buttonAddNewMenu.Text = "     Yeni Menü Oluştur";
            this.buttonAddNewMenu.UseVisualStyleBackColor = false;
            this.buttonAddNewMenu.Click += new System.EventHandler(this.createNewMenuButtonPressed);
            // 
            // MenuControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeMenuKategori);
            this.Controls.Add(this.newKategoriForm);
            this.Controls.Add(this.buttonAddNewKategori);
            this.Controls.Add(this.treeNewKategori);
            this.Controls.Add(this.newMenuForm);
            this.Controls.Add(this.treeMenuName);
            this.Controls.Add(this.buttonAddNewMenu);
            this.Controls.Add(this.keyboardcontrol1);
            this.Name = "MenuControl";
            this.Size = new System.Drawing.Size(993, 626);
            this.newMenuForm.ResumeLayout(false);
            this.newMenuForm.PerformLayout();
            this.newKategoriForm.ResumeLayout(false);
            this.newKategoriForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox newMenuForm;
        private System.Windows.Forms.TextBox textboxMenuName;
        private System.Windows.Forms.Button buttonAddNewMenu;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonSaveNewMenu;
        private System.Windows.Forms.Button buttonDeleteMenu;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TreeView treeMenuName;
        private KeyboardClassLibrary.Keyboardcontrol keyboardcontrol1;
        private System.Windows.Forms.TreeView treeNewKategori;
        private System.Windows.Forms.Button buttonAddNewKategori;
        private System.Windows.Forms.GroupBox newKategoriForm;
        private System.Windows.Forms.TreeView treeMenuKategori;
        private System.Windows.Forms.TextBox textBoxYeniKategori;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSaveNewKategori;
        private System.Windows.Forms.Button buttonDeleteNewKategori;
        private System.Windows.Forms.Button buttonCancelNewKategori;
        private System.Windows.Forms.Button buttonAddKategori;
        private System.Windows.Forms.Button buttonDeleteKategori;

    }
}
