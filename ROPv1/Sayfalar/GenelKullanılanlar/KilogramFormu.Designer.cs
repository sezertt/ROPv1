namespace ROPv1
{
    partial class KilogramFormu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KilogramFormu));
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonNO = new System.Windows.Forms.Button();
            this.pinboardcontrol21 = new PinboardClassLibrary.Pinboardcontrol2();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.buttonKG = new System.Windows.Forms.Button();
            this.buttonGram = new System.Windows.Forms.Button();
            this.textBoxKG = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonOK.BackColor = System.Drawing.SystemColors.Window;
            this.buttonOK.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonOK.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonOK.Image = global::ROPv1.Properties.Resources.icon;
            this.buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOK.Location = new System.Drawing.Point(170, 345);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(125, 50);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "Tamam";
            this.buttonOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonOK.UseVisualStyleBackColor = false;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonNO
            // 
            this.buttonNO.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonNO.BackColor = System.Drawing.SystemColors.Window;
            this.buttonNO.DialogResult = System.Windows.Forms.DialogResult.No;
            this.buttonNO.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonNO.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonNO.Image = global::ROPv1.Properties.Resources.delete;
            this.buttonNO.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonNO.Location = new System.Drawing.Point(10, 346);
            this.buttonNO.Name = "buttonNO";
            this.buttonNO.Size = new System.Drawing.Size(125, 50);
            this.buttonNO.TabIndex = 3;
            this.buttonNO.Text = "İptal    ";
            this.buttonNO.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonNO.UseVisualStyleBackColor = false;
            // 
            // pinboardcontrol21
            // 
            this.pinboardcontrol21.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pinboardcontrol21.KeyboardType = PinboardClassLibrary.BoW.Standard;
            this.pinboardcontrol21.Location = new System.Drawing.Point(6, 104);
            this.pinboardcontrol21.Name = "pinboardcontrol21";
            this.pinboardcontrol21.Size = new System.Drawing.Size(293, 237);
            this.pinboardcontrol21.TabIndex = 53;
            this.pinboardcontrol21.UserKeyPressed += new PinboardClassLibrary.PinboardDelegate(this.pinboardcontrol21_UserKeyPressed);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // buttonKG
            // 
            this.buttonKG.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonKG.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonKG.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonKG.ForeColor = System.Drawing.Color.White;
            this.buttonKG.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonKG.Location = new System.Drawing.Point(10, 9);
            this.buttonKG.Name = "buttonKG";
            this.buttonKG.Size = new System.Drawing.Size(139, 50);
            this.buttonKG.TabIndex = 54;
            this.buttonKG.Text = "Kilogram";
            this.buttonKG.UseVisualStyleBackColor = false;
            this.buttonKG.Click += new System.EventHandler(this.buttonKG_Click);
            // 
            // buttonGram
            // 
            this.buttonGram.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonGram.BackColor = System.Drawing.SystemColors.Window;
            this.buttonGram.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonGram.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonGram.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonGram.Location = new System.Drawing.Point(156, 9);
            this.buttonGram.Name = "buttonGram";
            this.buttonGram.Size = new System.Drawing.Size(139, 50);
            this.buttonGram.TabIndex = 56;
            this.buttonGram.Text = "Gram";
            this.buttonGram.UseVisualStyleBackColor = false;
            this.buttonGram.Click += new System.EventHandler(this.buttonGram_Click);
            // 
            // textBoxKG
            // 
            this.textBoxKG.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBoxKG.ContextMenuStrip = this.contextMenuStrip1;
            this.textBoxKG.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.textBoxKG.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBoxKG.Location = new System.Drawing.Point(12, 67);
            this.textBoxKG.MaxLength = 20;
            this.textBoxKG.Name = "textBoxKG";
            this.textBoxKG.Size = new System.Drawing.Size(283, 32);
            this.textBoxKG.TabIndex = 84;
            this.textBoxKG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxKG.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxKG_KeyPress);
            // 
            // KilogramFormu
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(304, 404);
            this.ControlBox = false;
            this.Controls.Add(this.textBoxKG);
            this.Controls.Add(this.buttonGram);
            this.Controls.Add(this.buttonKG);
            this.Controls.Add(this.pinboardcontrol21);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonNO);
            this.ForeColor = System.Drawing.SystemColors.Window;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(320, 420);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(320, 420);
            this.Name = "KilogramFormu";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonNO;
        private PinboardClassLibrary.Pinboardcontrol2 pinboardcontrol21;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button buttonKG;
        private System.Windows.Forms.Button buttonGram;
        private System.Windows.Forms.TextBox textBoxKG;
    }
}