using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ROPv1
{
    public partial class KilogramFormu : Form
    {
        public double kilo;

        public KilogramFormu()
        {
            InitializeComponent();

            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
        }

        private void pinboardcontrol21_UserKeyPressed(object sender, PinboardClassLibrary.PinboardEventArgs e)
        {
            textBoxKG.Focus();
            SendKeys.Send(e.KeyboardKeyPressed);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {  
            try
            {
                if (buttonKG.BackColor == SystemColors.ActiveCaption)
                {
                    kilo = Convert.ToDouble(textBoxKG.Text);
                }
                else
                {
                    kilo = Convert.ToDouble(textBoxKG.Text) / 1000;
                }

                kilo = Math.Round(kilo, 2);
                
                this.Close();
            }
            catch
            {
                KontrolFormu dialog = new KontrolFormu("Lütfen geçerli bir değer giriniz", false);
                dialog.Show();
            }                
        }     

        private void textBoxKG_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 22)
                e.Handled = true;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == ',' && (sender as TextBox).Text.IndexOf(',') > -1)
            {
                e.Handled = true;
            }
        }

        private void buttonKG_Click(object sender, EventArgs e)
        {
            if(buttonKG.BackColor == SystemColors.ActiveCaption)
            {
                return;
            }
            else
            {
                try
                {
                    textBoxKG.Text = (Convert.ToDouble(textBoxKG.Text) / 1000).ToString();
                }
                catch
                {
                    textBoxKG.Text = "0";
                }

                buttonKG.BackColor = SystemColors.ActiveCaption;
                buttonKG.ForeColor = Color.White;
                buttonGram.BackColor = Color.White;
                buttonGram.ForeColor = SystemColors.ActiveCaption;
            }            
        }

        private void buttonGram_Click(object sender, EventArgs e)
        {
            if (buttonGram.BackColor == SystemColors.ActiveCaption)
            {
                return;
            }
            else
            {
                try
                {
                    textBoxKG.Text = (Convert.ToDouble(textBoxKG.Text) * 1000).ToString();
                }
                catch
                {
                    textBoxKG.Text = "0";
                }

                buttonGram.BackColor = SystemColors.ActiveCaption;
                buttonGram.ForeColor = Color.White;
                buttonKG.BackColor = Color.White;
                buttonKG.ForeColor = SystemColors.ActiveCaption;
            }
        }
    }
}
