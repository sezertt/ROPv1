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
    public partial class TeraziBarkodFormu : Form
    {
        public TeraziBarkodFormu()
        {
            InitializeComponent();

            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;

            textBoxTeraziBarkodu.Text = Properties.Settings.Default.TeraziBarkod.ToString();
        }

        private void pinboardcontrol21_UserKeyPressed(object sender, PinboardClassLibrary.PinboardEventArgs e)
        {
            textBoxTeraziBarkodu.Focus();
            SendKeys.Send(e.KeyboardKeyPressed);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                int kod = Convert.ToInt32(textBoxTeraziBarkodu.Text);
                if(kod < 10 && textBoxTeraziBarkodu.Text.Length < 2)
                {
                    textBoxTeraziBarkodu.Text = 0 + textBoxTeraziBarkodu.Text;
                }
            }
            catch
            {
                KontrolFormu dialog = new KontrolFormu("Yanlış bir değer girdiniz", false);
                dialog.Show();
                return;
            }

            Properties.Settings.Default.TeraziBarkod = textBoxTeraziBarkodu.Text;
            Properties.Settings.Default.Save();
            this.Close();              
        }     

        private void textBoxKG_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 22)
                e.Handled = true;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }       
    }
}
