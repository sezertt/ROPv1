using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Data.SqlClient;
using SPIA;
using SPIA.Client;
using SPIA.Server;

namespace ROPv1
{
    public partial class AdisyonGoruntuleme : Form
    {
        bool hangiTakvikFocuslu = true;

        public AdisyonGoruntuleme()
        {
            InitializeComponent();
        }

        private void timerSaat_Tick(object sender, EventArgs e)
        {
            labelSaat.Text = DateTime.Now.ToString("HH:mm:ss", new CultureInfo("tr-TR"));
        }

        private void exitPressed(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonArttir_Click(object sender, EventArgs e)
        {
            if (hangiTakvikFocuslu)
            {
                dateBaslangic.Select();
            }
            else
            {
                dateBitis.Select();
            }
            SendKeys.Send("{UP}");
        }

        private void buttonAzalt_Click(object sender, EventArgs e)
        {
            if (hangiTakvikFocuslu)
            {
                dateBaslangic.Select();
            }
            else
            {
                dateBitis.Select();
            }
            SendKeys.Send("{DOWN}");
        }
        
        private void dateBaslangic_Enter(object sender, EventArgs e)
        {
            hangiTakvikFocuslu = true;
        }

        private void dateBitis_Enter(object sender, EventArgs e)
        {
            hangiTakvikFocuslu = false;
        }
               
        private void dateBitis_ValueChanged(object sender, EventArgs e)
        {
            if (dateBaslangic.Value > dateBitis.Value)
                dateBaslangic.Value = dateBitis.Value;
            dateBaslangic.MaxDate = dateBitis.Value;
        }

        private void comboAdisyonAyar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboAdisyonAyar_Click(object sender, EventArgs e)
        {
            ((ComboBox)sender).DroppedDown = true;
            ((ComboBox)sender).SelectionLength = 0;
        }

        private void AdisyonGoruntuleme_Load(object sender, EventArgs e)
        {
            dateBitis.MaxDate = DateTime.Today;
            dateBaslangic.MaxDate = DateTime.Today;
            comboAdisyonAyar.SelectedIndex = 0;
        }
        
        private void buttonAdisyonlariGetir_Click(object sender, EventArgs e)
        {

        }

        private void comboAdisyonAyar_Leave(object sender, EventArgs e)
        {
            ((ComboBox)sender).SelectionLength = 0;
        }

        private void comboAdisyonAyar_TextChanged(object sender, EventArgs e)
        {
            ((ComboBox)sender).SelectionLength = 0;

        }
    }
}