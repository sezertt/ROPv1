namespace ROPv1
{
    partial class Kullanici
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Kullanici));
            this.keyboardcontrol1 = new KeyboardClassLibrary.Keyboardcontrol();
            this.label1 = new System.Windows.Forms.Label();
            this.comboNewTitle = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.buttonAddNewUser = new System.Windows.Forms.Button();
            this.textboxPin = new System.Windows.Forms.TextBox();
            this.textboxNameSurname = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonSaveNewUser = new System.Windows.Forms.Button();
            this.buttonDeleteUser = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.treeUserName = new System.Windows.Forms.TreeView();
            this.newUserForm = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textboxUserName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.treeYetkiler = new System.Windows.Forms.TreeView();
            this.newUserForm.SuspendLayout();
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
            this.keyboardcontrol1.UserKeyPressed += new KeyboardClassLibrary.KeyboardDelegate(this.keyboardcontrol1_UserKeyPressed);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(10, 210);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 24);
            this.label1.TabIndex = 42;
            this.label1.Text = "Ünvanı";
            // 
            // comboNewTitle
            // 
            this.comboNewTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboNewTitle.ContextMenuStrip = this.contextMenuStrip1;
            this.comboNewTitle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboNewTitle.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.comboNewTitle.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.comboNewTitle.FormattingEnabled = true;
            this.comboNewTitle.Location = new System.Drawing.Point(14, 237);
            this.comboNewTitle.MaxDropDownItems = 20;
            this.comboNewTitle.Name = "comboNewTitle";
            this.comboNewTitle.Size = new System.Drawing.Size(307, 32);
            this.comboNewTitle.TabIndex = 7;
            this.comboNewTitle.Click += new System.EventHandler(this.showMenu);
            this.comboNewTitle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxKeyPressed);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // buttonAddNewUser
            // 
            this.buttonAddNewUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddNewUser.BackColor = System.Drawing.SystemColors.Window;
            this.buttonAddNewUser.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonAddNewUser.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonAddNewUser.Image = global::ROPv1.Properties.Resources.add;
            this.buttonAddNewUser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddNewUser.Location = new System.Drawing.Point(5, 293);
            this.buttonAddNewUser.Name = "buttonAddNewUser";
            this.buttonAddNewUser.Size = new System.Drawing.Size(440, 45);
            this.buttonAddNewUser.TabIndex = 39;
            this.buttonAddNewUser.TabStop = false;
            this.buttonAddNewUser.Text = "Yeni Kullanıcı Oluştur";
            this.buttonAddNewUser.UseVisualStyleBackColor = false;
            // 
            // textboxPin
            // 
            this.textboxPin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxPin.ContextMenuStrip = this.contextMenuStrip1;
            this.textboxPin.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textboxPin.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textboxPin.Location = new System.Drawing.Point(14, 175);
            this.textboxPin.MaxLength = 4;
            this.textboxPin.Name = "textboxPin";
            this.textboxPin.PasswordChar = '*';
            this.textboxPin.Size = new System.Drawing.Size(142, 32);
            this.textboxPin.TabIndex = 5;
            this.textboxPin.UseSystemPasswordChar = true;
            this.textboxPin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.pinPressed);
            // 
            // textboxNameSurname
            // 
            this.textboxNameSurname.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxNameSurname.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textboxNameSurname.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textboxNameSurname.Location = new System.Drawing.Point(14, 51);
            this.textboxNameSurname.Name = "textboxNameSurname";
            this.textboxNameSurname.Size = new System.Drawing.Size(142, 32);
            this.textboxNameSurname.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label7.Location = new System.Drawing.Point(10, 148);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(154, 24);
            this.label7.TabIndex = 25;
            this.label7.Text = "Pin Kodu(0-9):";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Location = new System.Drawing.Point(10, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 24);
            this.label5.TabIndex = 19;
            this.label5.Text = "Adı:";
            // 
            // buttonSaveNewUser
            // 
            this.buttonSaveNewUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveNewUser.BackColor = System.Drawing.SystemColors.Window;
            this.buttonSaveNewUser.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonSaveNewUser.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonSaveNewUser.Image = ((System.Drawing.Image)(resources.GetObject("buttonSaveNewUser.Image")));
            this.buttonSaveNewUser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSaveNewUser.Location = new System.Drawing.Point(211, 277);
            this.buttonSaveNewUser.Name = "buttonSaveNewUser";
            this.buttonSaveNewUser.Size = new System.Drawing.Size(110, 45);
            this.buttonSaveNewUser.TabIndex = 9;
            this.buttonSaveNewUser.Text = "Kaydet";
            this.buttonSaveNewUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSaveNewUser.UseVisualStyleBackColor = false;
            // 
            // buttonDeleteUser
            // 
            this.buttonDeleteUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteUser.BackColor = System.Drawing.SystemColors.Window;
            this.buttonDeleteUser.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonDeleteUser.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonDeleteUser.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonDeleteUser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDeleteUser.Location = new System.Drawing.Point(14, 277);
            this.buttonDeleteUser.Name = "buttonDeleteUser";
            this.buttonDeleteUser.Size = new System.Drawing.Size(142, 45);
            this.buttonDeleteUser.TabIndex = 8;
            this.buttonDeleteUser.Text = "Kullanıcıyı Sil";
            this.buttonDeleteUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonDeleteUser.UseVisualStyleBackColor = false;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.BackColor = System.Drawing.SystemColors.Window;
            this.buttonCancel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonCancel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonCancel.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCancel.Location = new System.Drawing.Point(14, 277);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(110, 44);
            this.buttonCancel.TabIndex = 10;
            this.buttonCancel.Text = "İptal Et  ";
            this.buttonCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonCancel.UseVisualStyleBackColor = false;
            // 
            // treeUserName
            // 
            this.treeUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeUserName.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.treeUserName.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.treeUserName.FullRowSelect = true;
            this.treeUserName.HideSelection = false;
            this.treeUserName.HotTracking = true;
            this.treeUserName.Indent = 10;
            this.treeUserName.ItemHeight = 35;
            this.treeUserName.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.treeUserName.Location = new System.Drawing.Point(5, 5);
            this.treeUserName.Name = "treeUserName";
            this.treeUserName.ShowLines = false;
            this.treeUserName.Size = new System.Drawing.Size(440, 282);
            this.treeUserName.TabIndex = 38;
            // 
            // newUserForm
            // 
            this.newUserForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.newUserForm.BackColor = System.Drawing.Color.White;
            this.newUserForm.Controls.Add(this.textBox1);
            this.newUserForm.Controls.Add(this.label6);
            this.newUserForm.Controls.Add(this.textBoxPassword);
            this.newUserForm.Controls.Add(this.label4);
            this.newUserForm.Controls.Add(this.textboxUserName);
            this.newUserForm.Controls.Add(this.label3);
            this.newUserForm.Controls.Add(this.label2);
            this.newUserForm.Controls.Add(this.treeYetkiler);
            this.newUserForm.Controls.Add(this.label1);
            this.newUserForm.Controls.Add(this.comboNewTitle);
            this.newUserForm.Controls.Add(this.textboxPin);
            this.newUserForm.Controls.Add(this.textboxNameSurname);
            this.newUserForm.Controls.Add(this.label7);
            this.newUserForm.Controls.Add(this.label5);
            this.newUserForm.Controls.Add(this.buttonSaveNewUser);
            this.newUserForm.Controls.Add(this.buttonDeleteUser);
            this.newUserForm.Controls.Add(this.buttonCancel);
            this.newUserForm.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.newUserForm.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.newUserForm.Location = new System.Drawing.Point(451, 5);
            this.newUserForm.Name = "newUserForm";
            this.newUserForm.Size = new System.Drawing.Size(529, 333);
            this.newUserForm.TabIndex = 41;
            this.newUserForm.TabStop = false;
            this.newUserForm.Text = "Yeni Kullanıcı";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBox1.Location = new System.Drawing.Point(179, 51);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(142, 32);
            this.textBox1.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label6.Location = new System.Drawing.Point(175, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 24);
            this.label6.TabIndex = 49;
            this.label6.Text = "Soyadı:";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPassword.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxPassword.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBoxPassword.Location = new System.Drawing.Point(179, 175);
            this.textBoxPassword.MaxLength = 10;
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(142, 32);
            this.textBoxPassword.TabIndex = 6;
            this.textBoxPassword.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label4.Location = new System.Drawing.Point(175, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 24);
            this.label4.TabIndex = 47;
            this.label4.Text = "Şifre:";
            // 
            // textboxUserName
            // 
            this.textboxUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxUserName.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textboxUserName.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textboxUserName.Location = new System.Drawing.Point(14, 113);
            this.textboxUserName.MaxLength = 10;
            this.textboxUserName.Name = "textboxUserName";
            this.textboxUserName.Size = new System.Drawing.Size(307, 32);
            this.textboxUserName.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Location = new System.Drawing.Point(10, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 24);
            this.label3.TabIndex = 45;
            this.label3.Text = "Kullanıcı Adı:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(323, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 24);
            this.label2.TabIndex = 43;
            this.label2.Text = "Kullanıcı Yetkileri:";
            // 
            // treeYetkiler
            // 
            this.treeYetkiler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.treeYetkiler.CheckBoxes = true;
            this.treeYetkiler.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.treeYetkiler.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.treeYetkiler.FullRowSelect = true;
            this.treeYetkiler.HideSelection = false;
            this.treeYetkiler.HotTracking = true;
            this.treeYetkiler.Indent = 10;
            this.treeYetkiler.ItemHeight = 35;
            this.treeYetkiler.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.treeYetkiler.Location = new System.Drawing.Point(327, 51);
            this.treeYetkiler.Name = "treeYetkiler";
            this.treeYetkiler.ShowLines = false;
            this.treeYetkiler.Size = new System.Drawing.Size(196, 270);
            this.treeYetkiler.TabIndex = 42;
            // 
            // Kullanici
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.keyboardcontrol1);
            this.Controls.Add(this.buttonAddNewUser);
            this.Controls.Add(this.treeUserName);
            this.Controls.Add(this.newUserForm);
            this.Name = "Kullanici";
            this.Size = new System.Drawing.Size(993, 626);
            this.newUserForm.ResumeLayout(false);
            this.newUserForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private KeyboardClassLibrary.Keyboardcontrol keyboardcontrol1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboNewTitle;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button buttonAddNewUser;
        private System.Windows.Forms.TextBox textboxPin;
        private System.Windows.Forms.TextBox textboxNameSurname;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonSaveNewUser;
        private System.Windows.Forms.Button buttonDeleteUser;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TreeView treeUserName;
        private System.Windows.Forms.GroupBox newUserForm;
        private System.Windows.Forms.TreeView treeYetkiler;
        private System.Windows.Forms.TextBox textboxUserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label6;
    }
}
