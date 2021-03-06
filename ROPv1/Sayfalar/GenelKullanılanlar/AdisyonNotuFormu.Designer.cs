﻿namespace ROPv1
{
    partial class AdisyonNotuFormu
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
            this.keyboardcontrol1 = new KeyboardClassLibrary.Keyboardcontrol();
            this.textboxNot = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.checkBoxSave = new System.Windows.Forms.CheckBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonTamam = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // keyboardcontrol1
            // 
            this.keyboardcontrol1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.keyboardcontrol1.ForeColor = System.Drawing.SystemColors.Window;
            this.keyboardcontrol1.KeyboardType = KeyboardClassLibrary.BoW.Standard;
            this.keyboardcontrol1.Location = new System.Drawing.Point(12, 102);
            this.keyboardcontrol1.MaximumSize = new System.Drawing.Size(993, 282);
            this.keyboardcontrol1.MinimumSize = new System.Drawing.Size(993, 282);
            this.keyboardcontrol1.Name = "keyboardcontrol1";
            this.keyboardcontrol1.Size = new System.Drawing.Size(993, 282);
            this.keyboardcontrol1.TabIndex = 1;
            this.keyboardcontrol1.UserKeyPressed += new KeyboardClassLibrary.KeyboardDelegate(this.keyboardcontrol1_UserKeyPressed);
            // 
            // textboxNot
            // 
            this.textboxNot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxNot.ContextMenuStrip = this.contextMenuStrip1;
            this.textboxNot.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textboxNot.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textboxNot.Location = new System.Drawing.Point(18, 12);
            this.textboxNot.MaxLength = 250;
            this.textboxNot.Multiline = true;
            this.textboxNot.Name = "textboxNot";
            this.textboxNot.Size = new System.Drawing.Size(981, 81);
            this.textboxNot.TabIndex = 35;
            this.textboxNot.Click += new System.EventHandler(this.textboxNot_Click);
            this.textboxNot.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textboxNot_KeyPress);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // checkBoxSave
            // 
            this.checkBoxSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxSave.AutoSize = true;
            this.checkBoxSave.Checked = true;
            this.checkBoxSave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSave.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.checkBoxSave.ForeColor = System.Drawing.SystemColors.Window;
            this.checkBoxSave.Location = new System.Drawing.Point(633, 351);
            this.checkBoxSave.Name = "checkBoxSave";
            this.checkBoxSave.Size = new System.Drawing.Size(225, 20);
            this.checkBoxSave.TabIndex = 36;
            this.checkBoxSave.Text = "İsmi Kaydet ve Bir Daha Sorma";
            this.checkBoxSave.UseVisualStyleBackColor = true;
            this.checkBoxSave.Visible = false;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.BackColor = System.Drawing.SystemColors.Window;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonCancel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonCancel.Image = global::ROPv1.Properties.Resources.deleteBig;
            this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCancel.Location = new System.Drawing.Point(16, 334);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(125, 50);
            this.buttonCancel.TabIndex = 37;
            this.buttonCancel.Text = "Vazgeç";
            this.buttonCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonCancel.UseVisualStyleBackColor = false;
            // 
            // buttonTamam
            // 
            this.buttonTamam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTamam.BackColor = System.Drawing.SystemColors.Window;
            this.buttonTamam.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonTamam.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonTamam.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonTamam.Image = global::ROPv1.Properties.Resources.icon;
            this.buttonTamam.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonTamam.Location = new System.Drawing.Point(874, 334);
            this.buttonTamam.Name = "buttonTamam";
            this.buttonTamam.Size = new System.Drawing.Size(125, 50);
            this.buttonTamam.TabIndex = 34;
            this.buttonTamam.Text = "Tamam";
            this.buttonTamam.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonTamam.UseVisualStyleBackColor = false;
            this.buttonTamam.Click += new System.EventHandler(this.buttonTamam_Click);
            // 
            // AdisyonNotuFormu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1019, 396);
            this.ControlBox = false;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.checkBoxSave);
            this.Controls.Add(this.buttonTamam);
            this.Controls.Add(this.textboxNot);
            this.Controls.Add(this.keyboardcontrol1);
            this.ForeColor = System.Drawing.SystemColors.Window;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1035, 412);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1035, 412);
            this.Name = "AdisyonNotuFormu";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AdisyonNotuFormu_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private KeyboardClassLibrary.Keyboardcontrol keyboardcontrol1;
        private System.Windows.Forms.Button buttonTamam;
        private System.Windows.Forms.TextBox textboxNot;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxSave;
    }
}