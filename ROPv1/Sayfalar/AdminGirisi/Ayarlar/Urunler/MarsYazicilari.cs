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
    public partial class MarsYazicilari : Form
    {
        List<KategorilerineGoreUrunler> urunListesi;
        int urunKategoriYeri, urunYeri;

        public MarsYazicilari(List<KategorilerineGoreUrunler> urunListesi, int urunKategoriYeri, int urunYeri)
        {
            InitializeComponent();

            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;

            this.urunKategoriYeri = urunKategoriYeri;
            this.urunListesi = urunListesi;
            this.urunYeri = urunYeri;
        }
    }
}