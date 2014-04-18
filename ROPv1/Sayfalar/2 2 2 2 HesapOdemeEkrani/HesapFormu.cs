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
using System.Globalization;

namespace ROPv1
{
    public partial class HesapFormu : Form
    {
        private SiparisMenuFormu menuFormu;

        List<bool> listedeSeciliOlanItemlar = new List<bool>();

        string masaAdi, departmanAdi;

        ListViewItem sonSecilenItem;

        MyListView listHesaptakiler;

        decimal toplamHesap = 0, odenmekIstenenHesap = 0, indirim;

        const int urunBoyu = 240, fiyatBoyu = 90;

        int seciliItemSayisi = 0;

        public HesapFormu(SiparisMenuFormu menuFormu, MyListView siparisListView, string masaAdi, string departmanAdi)
        {
            InitializeComponent();

            this.menuFormu = menuFormu;
            this.masaAdi = masaAdi;
            this.departmanAdi = departmanAdi;
            this.listHesaptakiler = siparisListView;
        }

        decimal KesiriDecimalYap(string kesir) // verilen kesirli stringi decimale çevirerek return eder
        {
            decimal sonuc;

            if (decimal.TryParse(kesir, out sonuc))
            {
                return sonuc;
            }

            string[] split = kesir.Split(new char[] { ' ', '/' });

            if (split.Length == 2 || split.Length == 3)
            {
                int a, b;

                if (int.TryParse(split[0], out a) && int.TryParse(split[1], out b))
                {
                    if (split.Length == 2)
                    {
                        return (decimal)a / b;
                    }

                    int c;

                    if (int.TryParse(split[2], out c))
                    {
                        return a + (decimal)b / c;
                    }
                }
            }
            return 10;
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
            for (int i = 0; i < listUrunFiyat.Items.Count; i++)
            {
                listUrunFiyat.Items[i].SubItems[1].Text = "-";
            }
            seciliItemSayisi = 0;

            textNumberOfItem.Text = "";
            textBoxSecilenlerinTutari.Text = toplamHesap.ToString("0.00");
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

        //çarpanın ayarları
        private void textNumberOfItem_TextChanged(object sender, EventArgs e)
        {
            if (textNumberOfItem.Text == ",")
            {
                textNumberOfItem.Text = "0,";
                textNumberOfItem.SelectionStart = textNumberOfItem.Text.Length;
            }
        }

        private void timerSaat_Tick(object sender, EventArgs e)
        {
            labelSaat.Text = DateTime.Now.ToString("HH:mm:ss", new CultureInfo("tr-TR"));
        }

        private void hesaplaButonlarindanBirineBasildi(object sender, EventArgs e)
        {
            decimal carpan;
            try
            {
                carpan = Convert.ToDecimal(((Button)sender).Text);

                textNumberOfItem.Text = (Convert.ToDecimal(textNumberOfItem.Text) + carpan).ToString("0.00");
                odenmekIstenenHesap = (Convert.ToDecimal(textNumberOfItem.Text) + carpan);
            }
            catch //tümü butonuna basılmış demektir
            {
                carpan = KesiriDecimalYap(((Button)sender).Text);

                if (((Button)sender).Name == "buttonbolun") //name = buttonbolun --> 1/n olan buton
                {
                    carpan = KesiriDecimalYap("1/" + textNumberOfItem.Text);
                }

                if (carpan > 1)
                {
                    carpan = 1;
                }
                textNumberOfItem.Text = (toplamHesap * carpan).ToString("0.00");
                odenmekIstenenHesap = toplamHesap * carpan;
            }
        }

        //form load
        private void HesapFormu_Load(object sender, EventArgs e)
        {
            labelSaat.Text = DateTime.Now.ToString("HH:mm:ss", new CultureInfo("tr-TR"));
            timerSaat.Start();
            labelGun.Text = DateTime.Now.ToString("dddd", new CultureInfo("tr-TR"));
            labelTarih.Text = DateTime.Now.Date.ToString("d MMMM yyyy", new CultureInfo("tr-TR"));

            labelMasa.Text = "Masa: " + masaAdi;
            labelDepartman.Text = "Departman: " + departmanAdi;

            //labelların fontunu ayarlıyoruz
            while (labelDepartman.Width < System.Windows.Forms.TextRenderer.MeasureText(labelDepartman.Text, new Font(labelDepartman.Font.FontFamily, labelDepartman.Font.Size, labelDepartman.Font.Style)).Width)
            {
                labelDepartman.Font = new Font(labelDepartman.Font.FontFamily, labelDepartman.Font.Size - 0.5f, labelDepartman.Font.Style);
            }

            while (labelMasa.Width < System.Windows.Forms.TextRenderer.MeasureText(labelMasa.Text, new Font(labelMasa.Font.FontFamily, labelMasa.Font.Size, labelMasa.Font.Style)).Width)
            {
                labelMasa.Font = new Font(labelMasa.Font.FontFamily, labelMasa.Font.Size - 0.5f, labelMasa.Font.Style);
            }

            //Urunleri listeye ekliyoruz , fiyatlarını alıyoruz
            for (int i = 0; i < listHesaptakiler.Groups[2].Items.Count; i++)
            {
                listUrunFiyat.Items.Add(listHesaptakiler.Groups[2].Items[i].Text);
                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add("-");
                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(listHesaptakiler.Groups[2].Items[i].SubItems[1].Text);
                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(listHesaptakiler.Groups[2].Items[i].SubItems[2].Text);
                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font = new Font("Calibri", 18.75F, FontStyle.Bold);
                toplamHesap += Convert.ToDecimal(listHesaptakiler.Groups[2].Items[i].SubItems[2].Text);
            }






            // DİKKAT EĞER BURADA ÜRÜN VARSA ADİSYON VAR MI YOK MU BAK VARSA SİPARİŞLERİ EKLE YOKSA OLUŞTUR VE EKLE

            for (int i = 0; i < listHesaptakiler.Groups[3].Items.Count; i++)
            {
                listUrunFiyat.Items.Add(listHesaptakiler.Groups[3].Items[i].Text);
                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add("-");
                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(listHesaptakiler.Groups[3].Items[i].SubItems[1].Text);
                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(listHesaptakiler.Groups[3].Items[i].SubItems[2].Text);
                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font = new Font("Calibri", 18.75F, FontStyle.Bold);
                toplamHesap += Convert.ToDecimal(listHesaptakiler.Groups[3].Items[i].SubItems[2].Text);
            }

            textBoxSecilenlerinTutari.Text = toplamHesap.ToString("0.00");
            odenmekIstenenHesap = toplamHesap;

            //listedeki itemların sayısı nedeniyle scroll bar çıkarsa fiyat kısımlarını biraz sola almak için
            if (this.listUrunFiyat.Items.Count > 0)
            {
                int itemsCount = this.listUrunFiyat.Items.Count + 3; // 3 aslında grup sayısı -1
                int itemHeight = this.listUrunFiyat.Items[0].Bounds.Height;
                int VisiableItem = (int)this.listUrunFiyat.ClientRectangle.Height / itemHeight;
                if (itemsCount >= VisiableItem)
                {
                    listUrunFiyat.Columns[1].Width = urunBoyu;
                    listUrunFiyat.Columns[2].Width = fiyatBoyu;
                }
            }
        }

        //listede eleman seçildiğinde çalışacak olan method
        private void listHesap_Click(object sender, EventArgs e)
        {
            sonSecilenItem = listUrunFiyat.SelectedItems[0];

            try
            {
                // başındaki ve sonundaki parantezileri siliyoruz ()
                double adet = Convert.ToDouble(sonSecilenItem.SubItems[1].Text.Substring(1, sonSecilenItem.SubItems[1].Text.Length - 2));

                if (adet < Convert.ToDouble(sonSecilenItem.SubItems[0].Text))
                {
                    sonSecilenItem.SubItems[1].Text = "(" + (adet + 1) + ")";

                    textBoxSecilenlerinTutari.Text = (Convert.ToDecimal(textBoxSecilenlerinTutari.Text) + (Convert.ToDecimal(sonSecilenItem.SubItems[3].Text) / Convert.ToDecimal(sonSecilenItem.SubItems[0].Text))).ToString("0.00");

                    seciliItemSayisi++;
                }
            }
            catch
            {
                sonSecilenItem.SubItems[1].Text = "(1)";
                if (seciliItemSayisi != 0)
                {
                    textBoxSecilenlerinTutari.Text = (Convert.ToDecimal(textBoxSecilenlerinTutari.Text) + (Convert.ToDecimal(sonSecilenItem.SubItems[3].Text) / Convert.ToDecimal(sonSecilenItem.SubItems[0].Text))).ToString("0.00");
                }
                else
                {
                    textBoxSecilenlerinTutari.Text = (Convert.ToDecimal(sonSecilenItem.SubItems[3].Text) / Convert.ToDecimal(sonSecilenItem.SubItems[0].Text)).ToString("0.00");
                }
                seciliItemSayisi++;
            }

            listUrunFiyat.SelectedItems.Clear();
        }

        private void buttonIndirim_Click(object sender, EventArgs e)
        {
            indirim = Convert.ToDecimal(textNumberOfItem.Text);

            if (indirim > 0) // indirim yapıldı hesaptan düş
            {
                if (indirim > toplamHesap)
                {
                    indirim = toplamHesap;
                }

                labelIndirimTL.Visible = true;
                labelIndirimTLTutar.Visible = true;
                labelIndirim.Visible = true;
                labelIndirimTutar.Visible = true;
            }
            else // indirim kaldırıldı yeniden hesaba ekle
            {


                labelIndirimTL.Visible = false;
                labelIndirimTLTutar.Visible = false;
                if(!labelIndirimYuzde.Visible) // eğer yüzdeli indirim de yoksa labelları kaldır
                {
                    labelIndirim.Visible = false;
                    labelIndirimTutar.Visible = false;
                }
            }
        }

        private void buttonIndirimYuzdeli_Click(object sender, EventArgs e)
        {
            indirim = Convert.ToDecimal(textNumberOfItem.Text);

            if (indirim > 0) // indirim yapıldı hesaptan düş
            {
                if (indirim > 100)
                {
                    indirim = 100;
                }

                labelIndirimYuzde.Visible = true;
                labelIndirimYuzdeTutar.Visible = true;
                labelIndirim.Visible = true;
                labelIndirimTutar.Visible = true;
            }
            else // indirim kaldırıldı yeniden hesaba ekle
            {


                labelIndirimYuzde.Visible = false;
                labelIndirimYuzdeTutar.Visible = false;
                if (!labelIndirimTL.Visible) // eğer normal indirim de yoksa labelları kaldır
                {
                    labelIndirim.Visible = false;
                    labelIndirimTutar.Visible = false;
                }
            }
        }

        private void buttonNakit_Click(object sender, EventArgs e)
        {

        }

        private void buttonKart_Click(object sender, EventArgs e)
        {

        }

        private void buttonYemekFisi_Click(object sender, EventArgs e)
        {

        }

        private void buttonSecileniAzalt_Click(object sender, EventArgs e)
        {
            double adet;

            try
            {
                adet = Convert.ToDouble(sonSecilenItem.SubItems[1].Text.Substring(1, sonSecilenItem.SubItems[1].Text.Length - 2));
            }
            catch
            {
                return;
            }

            if (adet > 1)
            {
                sonSecilenItem.SubItems[1].Text = "(" + (adet - 1) + ")";
                seciliItemSayisi--;
            }
            else if(adet == 1)
            {
                sonSecilenItem.SubItems[1].Text = "-";
                seciliItemSayisi--;
            }

            if (seciliItemSayisi != 0)
            {
                textBoxSecilenlerinTutari.Text = (Convert.ToDecimal(textBoxSecilenlerinTutari.Text) - (Convert.ToDecimal(sonSecilenItem.SubItems[3].Text) / Convert.ToDecimal(sonSecilenItem.SubItems[0].Text))).ToString("0.00");
            }
            else
            {
                textNumberOfItem.Text = "";
                textBoxSecilenlerinTutari.Text = toplamHesap.ToString("0.00");
            }
        }

        private void buttonTamam_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Server == 2) //server - diğer tüm clientlara söylemeli yaptığı ikram vs. neyse
            {

            }
            else //client
            {

            }
        }

        private void buttonHesapYazdir_Click(object sender, EventArgs e)
        {

        }

        #region SQL İşlemleri

        #endregion
    }
}