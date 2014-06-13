using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Data.SqlClient;

namespace ROPv1
{
    public partial class AnketDegerlendirmeYazili : UserControl
    {
        int SecmeliSoruSayisi = 0;

        public AnketDegerlendirmeYazili()
        {
            InitializeComponent();
        }

        private void AnketDegerlendirme_Load(object sender, EventArgs e)
        {
            
        }

        private void buttonSayfaAzalt_Click_1(object sender, EventArgs e)
        {
            if (Convert.ToInt32(labelSayfa.Text) < 2)
                return;
            labelSayfa.Text = (Convert.ToInt32(labelSayfa.Text) - 1).ToString();
        }

        private void buttonSayfaArttir_Click_1(object sender, EventArgs e)
        {
            if (Convert.ToInt32(labelSayfa.Text) == Convert.ToInt32(labelSayfaSayisi.Text))
                return;
            labelSayfa.Text = (Convert.ToInt32(labelSayfa.Text) + 1).ToString();
        }

        private void labelSayfa_TextChanged_1(object sender, EventArgs e)
        {
           
        }
    }
}