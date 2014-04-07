namespace ROPv1
{
    partial class Departman
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
            this.treeDepartman = new System.Windows.Forms.TreeView();
            this.keyboardcontrol1 = new KeyboardClassLibrary.Keyboardcontrol();
            this.newDepartmentForm = new System.Windows.Forms.GroupBox();
            this.comboNewDepView = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.comboNewDepMenu = new System.Windows.Forms.ComboBox();
            this.comboNewDepName = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonDeleteDepartment = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonSaveNewDep = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonAddDepartment = new System.Windows.Forms.Button();
            this.newDepartmentForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeDepartman
            // 
            this.treeDepartman.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeDepartman.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.treeDepartman.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.treeDepartman.FullRowSelect = true;
            this.treeDepartman.HideSelection = false;
            this.treeDepartman.HotTracking = true;
            this.treeDepartman.Indent = 8;
            this.treeDepartman.ItemHeight = 35;
            this.treeDepartman.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.treeDepartman.Location = new System.Drawing.Point(5, 5);
            this.treeDepartman.Name = "treeDepartman";
            this.treeDepartman.ShowLines = false;
            this.treeDepartman.ShowRootLines = false;
            this.treeDepartman.Size = new System.Drawing.Size(588, 296);
            this.treeDepartman.TabIndex = 1;
            this.treeDepartman.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.changeDepartment);
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
            // newDepartmentForm
            // 
            this.newDepartmentForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.newDepartmentForm.BackColor = System.Drawing.Color.White;
            this.newDepartmentForm.Controls.Add(this.comboNewDepView);
            this.newDepartmentForm.Controls.Add(this.comboNewDepMenu);
            this.newDepartmentForm.Controls.Add(this.comboNewDepName);
            this.newDepartmentForm.Controls.Add(this.label7);
            this.newDepartmentForm.Controls.Add(this.label4);
            this.newDepartmentForm.Controls.Add(this.buttonDeleteDepartment);
            this.newDepartmentForm.Controls.Add(this.label5);
            this.newDepartmentForm.Controls.Add(this.buttonSaveNewDep);
            this.newDepartmentForm.Controls.Add(this.buttonCancel);
            this.newDepartmentForm.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.newDepartmentForm.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.newDepartmentForm.Location = new System.Drawing.Point(604, 5);
            this.newDepartmentForm.Name = "newDepartmentForm";
            this.newDepartmentForm.Size = new System.Drawing.Size(379, 315);
            this.newDepartmentForm.TabIndex = 19;
            this.newDepartmentForm.TabStop = false;
            this.newDepartmentForm.Text = "Yeni Departman";
            // 
            // comboNewDepView
            // 
            this.comboNewDepView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboNewDepView.ContextMenuStrip = this.contextMenuStrip1;
            this.comboNewDepView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboNewDepView.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.comboNewDepView.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.comboNewDepView.FormattingEnabled = true;
            this.comboNewDepView.Location = new System.Drawing.Point(9, 139);
            this.comboNewDepView.MaxDropDownItems = 20;
            this.comboNewDepView.Name = "comboNewDepView";
            this.comboNewDepView.Size = new System.Drawing.Size(360, 32);
            this.comboNewDepView.TabIndex = 3;
            this.comboNewDepView.Click += new System.EventHandler(this.showMenu);
            this.comboNewDepView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxKeyPressed);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 26);
            // 
            // comboNewDepMenu
            // 
            this.comboNewDepMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboNewDepMenu.ContextMenuStrip = this.contextMenuStrip1;
            this.comboNewDepMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboNewDepMenu.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.comboNewDepMenu.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.comboNewDepMenu.FormattingEnabled = true;
            this.comboNewDepMenu.Location = new System.Drawing.Point(10, 216);
            this.comboNewDepMenu.MaxDropDownItems = 20;
            this.comboNewDepMenu.Name = "comboNewDepMenu";
            this.comboNewDepMenu.Size = new System.Drawing.Size(360, 32);
            this.comboNewDepMenu.TabIndex = 4;
            this.comboNewDepMenu.Click += new System.EventHandler(this.showMenu);
            this.comboNewDepMenu.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxKeyPressed);
            // 
            // comboNewDepName
            // 
            this.comboNewDepName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboNewDepName.BackColor = System.Drawing.SystemColors.Window;
            this.comboNewDepName.ContextMenuStrip = this.contextMenuStrip1;
            this.comboNewDepName.Cursor = System.Windows.Forms.Cursors.Default;
            this.comboNewDepName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.comboNewDepName.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.comboNewDepName.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.comboNewDepName.Location = new System.Drawing.Point(10, 62);
            this.comboNewDepName.MaxLength = 20;
            this.comboNewDepName.Name = "comboNewDepName";
            this.comboNewDepName.Size = new System.Drawing.Size(360, 32);
            this.comboNewDepName.TabIndex = 2;
            this.comboNewDepName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboNewDepName_KeyPress);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label7.Location = new System.Drawing.Point(6, 107);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(275, 24);
            this.label7.TabIndex = 25;
            this.label7.Text = "Departmanın Masa Ekranı:";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label4.Location = new System.Drawing.Point(12, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(330, 24);
            this.label4.TabIndex = 21;
            this.label4.Text = "Departmanın Kullanacağı Menü:";
            // 
            // buttonDeleteDepartment
            // 
            this.buttonDeleteDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteDepartment.BackColor = System.Drawing.SystemColors.Window;
            this.buttonDeleteDepartment.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonDeleteDepartment.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonDeleteDepartment.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonDeleteDepartment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDeleteDepartment.Location = new System.Drawing.Point(10, 260);
            this.buttonDeleteDepartment.Name = "buttonDeleteDepartment";
            this.buttonDeleteDepartment.Size = new System.Drawing.Size(154, 45);
            this.buttonDeleteDepartment.TabIndex = 0;
            this.buttonDeleteDepartment.TabStop = false;
            this.buttonDeleteDepartment.Text = "Departmanı Sil";
            this.buttonDeleteDepartment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonDeleteDepartment.UseVisualStyleBackColor = false;
            this.buttonDeleteDepartment.Click += new System.EventHandler(this.deleteDepartment);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Location = new System.Drawing.Point(12, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(184, 24);
            this.label5.TabIndex = 19;
            this.label5.Text = "Departmanın Adı:";
            // 
            // buttonSaveNewDep
            // 
            this.buttonSaveNewDep.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonSaveNewDep.BackColor = System.Drawing.SystemColors.Window;
            this.buttonSaveNewDep.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonSaveNewDep.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonSaveNewDep.Image = global::ROPv1.Properties.Resources.icon;
            this.buttonSaveNewDep.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSaveNewDep.Location = new System.Drawing.Point(269, 260);
            this.buttonSaveNewDep.Name = "buttonSaveNewDep";
            this.buttonSaveNewDep.Size = new System.Drawing.Size(101, 45);
            this.buttonSaveNewDep.TabIndex = 6;
            this.buttonSaveNewDep.Text = "Kaydet";
            this.buttonSaveNewDep.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSaveNewDep.UseVisualStyleBackColor = false;
            this.buttonSaveNewDep.Click += new System.EventHandler(this.buttonAddNewDep);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonCancel.BackColor = System.Drawing.SystemColors.Window;
            this.buttonCancel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonCancel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonCancel.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCancel.Location = new System.Drawing.Point(10, 260);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(101, 45);
            this.buttonCancel.TabIndex = 17;
            this.buttonCancel.TabStop = false;
            this.buttonCancel.Text = "İptal Et";
            this.buttonCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.cancelNewDepartment);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.SystemColors.Window;
            this.button2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button2.Image = global::ROPv1.Properties.Resources.downIcon;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.Location = new System.Drawing.Point(473, 305);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 45);
            this.button2.TabIndex = 27;
            this.button2.TabStop = false;
            this.button2.Text = "Aşağı Taşı";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.modeNodeDown);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.SystemColors.Window;
            this.button1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.Image = global::ROPv1.Properties.Resources.upIcon;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.Location = new System.Drawing.Point(347, 305);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 45);
            this.button1.TabIndex = 26;
            this.button1.TabStop = false;
            this.button1.Text = "Yukarı Taşı";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.moveNodeUp);
            // 
            // buttonAddDepartment
            // 
            this.buttonAddDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddDepartment.BackColor = System.Drawing.SystemColors.Window;
            this.buttonAddDepartment.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonAddDepartment.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonAddDepartment.Image = global::ROPv1.Properties.Resources.add;
            this.buttonAddDepartment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddDepartment.Location = new System.Drawing.Point(5, 305);
            this.buttonAddDepartment.Name = "buttonAddDepartment";
            this.buttonAddDepartment.Size = new System.Drawing.Size(268, 45);
            this.buttonAddDepartment.TabIndex = 0;
            this.buttonAddDepartment.TabStop = false;
            this.buttonAddDepartment.Text = "Yeni Departman Ekle";
            this.buttonAddDepartment.UseVisualStyleBackColor = false;
            this.buttonAddDepartment.Click += new System.EventHandler(this.addNewDepartment);
            // 
            // Departman
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.newDepartmentForm);
            this.Controls.Add(this.keyboardcontrol1);
            this.Controls.Add(this.buttonAddDepartment);
            this.Controls.Add(this.treeDepartman);
            this.Name = "Departman";
            this.Size = new System.Drawing.Size(993, 638);
            this.Load += new System.EventHandler(this.Departman_Load);
            this.newDepartmentForm.ResumeLayout(false);
            this.newDepartmentForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeDepartman;
        private System.Windows.Forms.Button buttonDeleteDepartment;
        private System.Windows.Forms.Button buttonAddDepartment;
        private KeyboardClassLibrary.Keyboardcontrol keyboardcontrol1;
        private System.Windows.Forms.GroupBox newDepartmentForm;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonSaveNewDep;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboNewDepName;
        private System.Windows.Forms.ComboBox comboNewDepMenu;
        private System.Windows.Forms.ComboBox comboNewDepView;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

    }
}
