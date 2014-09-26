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
    public partial class PortFormu : Form
    {
        public PortFormu()
        {
            InitializeComponent();
            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
        }

        private void pinboardcontrol21_UserKeyPressed(object sender, PinboardClassLibrary.PinboardEventArgs e)
        {
            SendKeys.Send(e.KeyboardKeyPressed);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.IP = textBoxIP.Text;
            Properties.Settings.Default.Port = Convert.ToInt32(textBoxPort.Text);
            if (comboServer.Text == "Server")
            {
                Properties.Settings.Default.Server = 2;
                //Properties.Settings.Default.IP = "127.0.0.1"; // localhost olmak zorunda server için
            }
            else
                Properties.Settings.Default.Server = 0;
            Properties.Settings.Default.Save();
        }

        //Form Load
        private void UrunDegistir_Load(object sender, EventArgs e)
        {
            textBoxIP.Text = Properties.Settings.Default.IP;
            textBoxPort.Text = Properties.Settings.Default.Port.ToString();
            if (Properties.Settings.Default.Server == 2)//server
                comboServer.Text = "Server";
            else
                comboServer.Text = "Client";
        }

        private void comboBoxKeyPressed(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void showMenu(object sender, EventArgs e)
        {
            ((ComboBox)sender).DroppedDown = true;
        }

        private void textBoxIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 22)
                e.Handled = true;

            if (e.KeyChar == ',')
                e.KeyChar = '.';
        }
    }
}
