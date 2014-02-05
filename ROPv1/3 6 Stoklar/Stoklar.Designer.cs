namespace ROPv1
{
    partial class Stoklar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Stoklar));
            this.keyboardcontrol1 = new KeyboardClassLibrary.Keyboardcontrol();
            this.textboxStokName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.treeStokAdi = new System.Windows.Forms.TreeView();
            this.newStokForm = new System.Windows.Forms.GroupBox();
            this.buttonSaveNewStok = new System.Windows.Forms.Button();
            this.buttonDeleteStok = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonAddNewStok = new System.Windows.Forms.Button();
            this.newStokForm.SuspendLayout();
            this.SuspendLayout();
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
            this.keyboardcontrol1.TabIndex = 40;
            // 
            // textboxStokName
            // 
            this.textboxStokName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxStokName.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textboxStokName.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textboxStokName.Location = new System.Drawing.Point(10, 56);
            this.textboxStokName.Name = "textboxStokName";
            this.textboxStokName.Size = new System.Drawing.Size(310, 32);
            this.textboxStokName.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Location = new System.Drawing.Point(6, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 24);
            this.label5.TabIndex = 19;
            this.label5.Text = "Depo Adı:";
            // 
            // treeStokAdi
            // 
            this.treeStokAdi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeStokAdi.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.treeStokAdi.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.treeStokAdi.FullRowSelect = true;
            this.treeStokAdi.HideSelection = false;
            this.treeStokAdi.HotTracking = true;
            this.treeStokAdi.Indent = 10;
            this.treeStokAdi.ItemHeight = 35;
            this.treeStokAdi.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.treeStokAdi.Location = new System.Drawing.Point(5, 5);
            this.treeStokAdi.Name = "treeStokAdi";
            this.treeStokAdi.ShowLines = false;
            this.treeStokAdi.Size = new System.Drawing.Size(636, 282);
            this.treeStokAdi.TabIndex = 38;
            // 
            // newStokForm
            // 
            this.newStokForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.newStokForm.BackColor = System.Drawing.Color.White;
            this.newStokForm.Controls.Add(this.textboxStokName);
            this.newStokForm.Controls.Add(this.label5);
            this.newStokForm.Controls.Add(this.buttonSaveNewStok);
            this.newStokForm.Controls.Add(this.buttonDeleteStok);
            this.newStokForm.Controls.Add(this.buttonCancel);
            this.newStokForm.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.newStokForm.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.newStokForm.Location = new System.Drawing.Point(655, 5);
            this.newStokForm.Name = "newStokForm";
            this.newStokForm.Size = new System.Drawing.Size(326, 333);
            this.newStokForm.TabIndex = 41;
            this.newStokForm.TabStop = false;
            this.newStokForm.Text = "Yeni Stok";
            // 
            // buttonSaveNewStok
            // 
            this.buttonSaveNewStok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveNewStok.BackColor = System.Drawing.SystemColors.Window;
            this.buttonSaveNewStok.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonSaveNewStok.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonSaveNewStok.Image = ((System.Drawing.Image)(resources.GetObject("buttonSaveNewStok.Image")));
            this.buttonSaveNewStok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSaveNewStok.Location = new System.Drawing.Point(207, 245);
            this.buttonSaveNewStok.Name = "buttonSaveNewStok";
            this.buttonSaveNewStok.Size = new System.Drawing.Size(110, 45);
            this.buttonSaveNewStok.TabIndex = 6;
            this.buttonSaveNewStok.Text = "Kaydet";
            this.buttonSaveNewStok.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSaveNewStok.UseVisualStyleBackColor = false;
            // 
            // buttonDeleteStok
            // 
            this.buttonDeleteStok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteStok.BackColor = System.Drawing.SystemColors.Window;
            this.buttonDeleteStok.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonDeleteStok.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonDeleteStok.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonDeleteStok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDeleteStok.Location = new System.Drawing.Point(10, 245);
            this.buttonDeleteStok.Name = "buttonDeleteStok";
            this.buttonDeleteStok.Size = new System.Drawing.Size(110, 45);
            this.buttonDeleteStok.TabIndex = 5;
            this.buttonDeleteStok.Text = "Stoğu Sil";
            this.buttonDeleteStok.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonDeleteStok.UseVisualStyleBackColor = false;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.BackColor = System.Drawing.SystemColors.Window;
            this.buttonCancel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonCancel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonCancel.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCancel.Location = new System.Drawing.Point(10, 246);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(110, 44);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "İptal Et  ";
            this.buttonCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonCancel.UseVisualStyleBackColor = false;
            // 
            // buttonAddNewStok
            // 
            this.buttonAddNewStok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddNewStok.BackColor = System.Drawing.SystemColors.Window;
            this.buttonAddNewStok.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonAddNewStok.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonAddNewStok.Image = global::ROPv1.Properties.Resources.add;
            this.buttonAddNewStok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddNewStok.Location = new System.Drawing.Point(5, 293);
            this.buttonAddNewStok.Name = "buttonAddNewStok";
            this.buttonAddNewStok.Size = new System.Drawing.Size(196, 45);
            this.buttonAddNewStok.TabIndex = 39;
            this.buttonAddNewStok.Text = "      Yeni Stok Oluştur";
            this.buttonAddNewStok.UseVisualStyleBackColor = false;
            // 
            // Stoklar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.keyboardcontrol1);
            this.Controls.Add(this.buttonAddNewStok);
            this.Controls.Add(this.treeStokAdi);
            this.Controls.Add(this.newStokForm);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.Name = "Stoklar";
            this.Size = new System.Drawing.Size(993, 626);
            this.newStokForm.ResumeLayout(false);
            this.newStokForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private KeyboardClassLibrary.Keyboardcontrol keyboardcontrol1;
        private System.Windows.Forms.Button buttonAddNewStok;
        private System.Windows.Forms.TextBox textboxStokName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonSaveNewStok;
        private System.Windows.Forms.Button buttonDeleteStok;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TreeView treeStokAdi;
        private System.Windows.Forms.GroupBox newStokForm;
    }
}
