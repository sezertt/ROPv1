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
    public partial class SiparisMenuFormu : Form
    {
        Restoran hangiDepartman;
        int ButonX,ButonY;
        public SiparisMenuFormu(string butonunYeri, Restoran butonBilgileri)
        {
            InitializeComponent();
            hangiDepartman = butonBilgileri;
            ButonX = Convert.ToInt32(butonunYeri[0]);
            ButonY = Convert.ToInt32(butonunYeri[1]);
        }
    }
}
