using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;

namespace ROPv1
{
    public partial class HesapFormu : Form
    {
        private SiparisMenuFormu menuFormu;

        public MasaDegistirFormu masaDegistirForm;

        const int eskiIkramlar = 0, yeniIkramlar = 1, eskiSiparisler = 2, yeniSiparisler = 3;

        const int urunBoyu = 220, fiyatBoyu = 90;

        List<Menuler> menuListesi = new List<Menuler>();  // menüleri tutacak liste

        List<KategorilerineGoreUrunler> urunListesi = new List<KategorilerineGoreUrunler>();

        List<bool> listedeSeciliOlanItemlar = new List<bool>();

        public bool masaAcikMi = false;

        public int masaDegisti = -1;

        public string yeniMasaninAdi = "", urunTasinirkenYeniMasaOlusturulduysaOlusanMasaninAdi = "";

        public HesapFormu(SiparisMenuFormu menuFormu, string masaninAdi, Restoran butonBilgileri, string siparisiGirenKisi, bool masaAcikmi)
        {
            InitializeComponent();

            this.menuFormu = menuFormu;

            
        }

        //keypadin methodu
        private void pinboardcontrol21_UserKeyPressed(object sender, PinboardClassLibrary.PinboardEventArgs e)
        {
            textNumberOfItem.Focus();
            SendKeys.Send(e.KeyboardKeyPressed);
        }

        //Siparişlerin çarpanını ayarlayan textboxı boşaltan buton
        private void buttonDeleteText_Click(object sender, EventArgs e)
        {
            textNumberOfItem.Text = "";
        }

        //çarpan özellikleri
        private void keyPressedOnPriceText(object sender, KeyPressEventArgs e)
        {
            if (textNumberOfItem.Text == String.Empty)
            {
                if (e.KeyChar == '0')
                    e.Handled = true;
            }

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }
            else if (e.KeyChar == ',' && (sender as TextBox).Text.IndexOf(',') > -1 && (sender as TextBox).Text.Length < 4)
            { // 1 kere , kullanmasına izin ver

                e.Handled = true;
            }
        }

        //listede eleman seçildiğinde çalışacak olan method
        private void listHesap_Click(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = listUrunFiyat.HitTest(listUrunFiyat.PointToClient(Cursor.Position));
            int kacElemanSecili;

            //itema basıldıysa seçili olanları tuttuğumuz listede o itemı false ise true, true ise false yap
            if (info.Item != null)
            {
                if (listUrunFiyat.Items[info.Item.Index].Selected && listedeSeciliOlanItemlar[info.Item.Index] == false)
                    listedeSeciliOlanItemlar[info.Item.Index] = true;
                else
                    listedeSeciliOlanItemlar[info.Item.Index] = false;
            }

            kacElemanSecili = 0;

            for (int i = 0; i < listedeSeciliOlanItemlar.Count; i++)
            {
                if (listedeSeciliOlanItemlar[i])
                {
                    listUrunFiyat.Items[i].Selected = true;
                    kacElemanSecili++;
                }
                else
                {
                    listUrunFiyat.Items[i].Selected = false;
                }
            }

            if (kacElemanSecili == 0) //list viewda seçili eleman yok 
            {
                
            }
            else if (kacElemanSecili == 1) //list viewda 1 eleman seçili
            {
                
            }
            else //list viewda 1 den fazla seçili eleman var
            {
              
            }
        }

        //çarpanın ayarları
        private void textNumberOfItem_TextChanged(object sender, EventArgs e)
        {
            if (textNumberOfItem.Text == ",")
            {
                textNumberOfItem.Text = "0,";
                textNumberOfItem.SelectionStart = textNumberOfItem.Text.Length;
            }
        }

        //listede seçili elemanların seçimini kaldırır, çarpanı 0 lar ikram/iptal/ekle/taşı butonlarını disable eder
        private void buttonTemizle_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listedeSeciliOlanItemlar.Count; i++)
            {
                listedeSeciliOlanItemlar[i] = false;
                listUrunFiyat.Items[i].Selected = false;
            }

            textNumberOfItem.Text = "";
        }

        //form load
        private void HesapFormu_Load(object sender, EventArgs e)
        {

        }

        private void buttonTamam_Click(object sender, EventArgs e)
        {
           
        }

        private void paymentButton_Click(object sender, EventArgs e)
        {
           
        }    

        #region SQL İşlemleri

        #endregion
    }
}