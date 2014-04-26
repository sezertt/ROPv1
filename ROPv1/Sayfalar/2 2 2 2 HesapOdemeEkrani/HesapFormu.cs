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

// ÖDEME TİPLERİ 
// 0 NAKİT
// 1 KREDİ KARTI
// 2 YEMEK FİŞİ

namespace ROPv1
{
    public partial class HesapFormu : Form
    {
        private SiparisMenuFormu menuFormu;

        string masaAdi, departmanAdi, siparisiGirenKisi;

        ListViewItem sonSecilenItem;

        MyListView listHesaptakiler;

        decimal toplamHesap = 0, indirim = 0, indirimYuzde = 0, toplamIndirim = 0;

        const int urunBoyu = 240, fiyatBoyu = 90;

        int seciliItemSayisi = 0;

        public HesapFormu(SiparisMenuFormu menuFormu, MyListView siparisListView, string masaAdi, string departmanAdi, string siparisiGirenKisi)
        {
            InitializeComponent();

            this.menuFormu = menuFormu;
            this.masaAdi = masaAdi;
            this.departmanAdi = departmanAdi;
            this.listHesaptakiler = siparisListView;
            this.siparisiGirenKisi = siparisiGirenKisi;
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

            textNumberOfItem.Text = "0,00";
            textBoxSecilenlerinTutari.Text = (toplamHesap - toplamIndirim).ToString("0.00");
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

            // EĞER BURADA ÜRÜN VARSA ADİSYON VAR MI YOK MU BAK VARSA SİPARİŞLERİ EKLE YOKSA OLUŞTUR VE EKLE
            if (listHesaptakiler.Groups[3].Items.Count > 0)
            {
                if (Properties.Settings.Default.Server == 2) //server - diğer tüm clientlara söylemeli yaptığı ikram vs. neyse
                {
                    SqlCommand cmd;

                    int adisyonID;

                    cmd = SQLBaglantisi.getCommand("SELECT AdisyonID FROM Adisyon WHERE MasaAdi='" + masaAdi + "' AND DepartmanAdi='" + departmanAdi + "' AND AcikMi=1");
                    SqlDataReader dr = cmd.ExecuteReader();
                    dr.Read();

                    try // açık
                    {
                        adisyonID = dr.GetInt32(0);
                        menuFormu.masaAcikMi = true;

                        cmd.Connection.Close();
                        cmd.Connection.Dispose();
                    }
                    catch// kapalı
                    {
                        cmd.Connection.Close();
                        cmd.Connection.Dispose();

                        adisyonID = adisyonOlustur();
                    }

                    foreach (ListViewItem siparis in listHesaptakiler.Groups[3].Items)
                    {
                        siparisOlustur(adisyonID, siparis);

                        menuFormu.masaFormu.serverdanSiparisIkramVeyaIptal(masaAdi, departmanAdi, "siparis", siparis.SubItems[0].Text, siparis.SubItems[1].Text, (Convert.ToDecimal(siparis.SubItems[2].Text) / Convert.ToDecimal(siparis.SubItems[0].Text)).ToString(), null);
                    }

                }
                else // client
                {
                    foreach (ListViewItem siparis in listHesaptakiler.Groups[3].Items)
                    {
                        menuFormu.masaFormu.serveraSiparis(masaAdi, departmanAdi, "siparis", siparis.SubItems[0].Text, siparis.SubItems[1].Text, siparisiGirenKisi, (Convert.ToDecimal(siparis.SubItems[2].Text) / Convert.ToDecimal(siparis.SubItems[0].Text)).ToString(), "");
                    }
                    menuFormu.masaAcikMi = true;
                }

                //Kaydı sql servera girilen menü formundaki siparişleri yeni sipariş grubundan eski sipariş grubuna alıyoruz
                for (int i = menuFormu.listUrunFiyat.Groups[3].Items.Count - 1; i > -1; i--)
                {
                    int listedeYeniGelenSiparisVarmi = -1; //ürün cinsi eski siparişlerde var mı bak 
                    for (int j = 0; j < menuFormu.listUrunFiyat.Groups[2].Items.Count; j++)
                    {
                        if (menuFormu.listUrunFiyat.Groups[3].Items[i].SubItems[1].Text == menuFormu.listUrunFiyat.Groups[2].Items[j].SubItems[1].Text)
                        {
                            listedeYeniGelenSiparisVarmi = j;
                            break;
                        }
                    }

                    if (listedeYeniGelenSiparisVarmi == -1) //yoksa ürünü hesaba ekle
                    {
                        menuFormu.listUrunFiyat.Groups[3].Items[i].Group = menuFormu.listUrunFiyat.Groups[2];
                    }
                    else
                    {
                        menuFormu.listUrunFiyat.Groups[2].Items[listedeYeniGelenSiparisVarmi].SubItems[0].Text = (Convert.ToDouble(menuFormu.listUrunFiyat.Groups[2].Items[listedeYeniGelenSiparisVarmi].SubItems[0].Text) + Convert.ToDouble(menuFormu.listUrunFiyat.Groups[3].Items[i].SubItems[0].Text)).ToString();
                        menuFormu.listUrunFiyat.Groups[2].Items[listedeYeniGelenSiparisVarmi].SubItems[2].Text = (Convert.ToDecimal(menuFormu.listUrunFiyat.Groups[2].Items[listedeYeniGelenSiparisVarmi].SubItems[2].Text) + Convert.ToDecimal(menuFormu.listUrunFiyat.Groups[3].Items[i].SubItems[2].Text)).ToString("0.00");
                    }
                }
                //yeni siparişleri hazırladık eski sipariş haline çevirdik artık listeden kaldırabiliriz 
                menuFormu.listUrunFiyat.Groups[3].Items.Clear();
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

            //listedeki itemların sayısı nedeniyle scroll bar çıkarsa fiyat kısımlarını biraz sola almak için
            if (this.listUrunFiyat.Items.Count > 0)
            {
                int itemsCount = this.listUrunFiyat.Items.Count;
                int itemHeight = this.listUrunFiyat.Items[0].Bounds.Height;
                int VisiableItem = (int)this.listUrunFiyat.ClientRectangle.Height / itemHeight;
                if (itemsCount >= VisiableItem)
                {
                    listUrunFiyat.Columns[1].Width = urunBoyu;
                    listUrunFiyat.Columns[2].Width = fiyatBoyu;
                }
            }

            if (Properties.Settings.Default.Server == 2)
            {
                odemeyeGec();
                //herkesi masadan çıkar 
                menuFormu.masaFormu.serverdanHesapOdeme(masaAdi, departmanAdi, "hesapOdeniyor");

                SqlCommand cmd = SQLBaglantisi.getCommand("SELECT Fiyatı, Porsiyon, YemekAdi from Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.MasaAdi='" + masaAdi + "' AND Adisyon.DepartmanAdi='" + departmanAdi + "' AND Siparis.IptalMi=0 AND Siparis.OdendiMi=1 AND Siparis.IkramMi=0 AND Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 ORDER BY Porsiyon DESC");
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    decimal yemeginFiyati;
                    double kacPorsiyon;
                    string yemeginAdi;
                    try
                    {
                        yemeginFiyati = dr.GetDecimal(0);
                        kacPorsiyon = (double)dr.GetDecimal(1);
                        yemeginAdi = dr.GetString(2);
                    }
                    catch
                    {
                        KontrolFormu dialog = new KontrolFormu("Ödeme bilgileri alınırken hata oluştu, lütfen tekrar giriş yapınız", false);
                        dialog.Show();
                        return;
                    }

                    int listedeYeniGelenSiparisVarmi = -1; //ürün cinsi hesapta var mı bak 

                    for (int i = 0; i < listOdenenler.Items.Count; i++)
                    {
                        if (yemeginAdi == listOdenenler.Items[i].SubItems[1].Text)
                        {
                            listedeYeniGelenSiparisVarmi = i;
                            break;
                        }
                    }

                    if (listedeYeniGelenSiparisVarmi == -1) //yoksa ürünü hesaba ekle
                    {
                        listOdenenler.Items.Add(kacPorsiyon.ToString());
                        listOdenenler.Items[listOdenenler.Items.Count - 1].SubItems.Add(yemeginAdi);
                        listOdenenler.Items[listOdenenler.Items.Count - 1].SubItems.Add(((decimal)kacPorsiyon * yemeginFiyati).ToString("0.00"));
                        listOdenenler.Items[listOdenenler.Items.Count - 1].Font = new Font("Calibri", 18.75F, FontStyle.Bold);
                    }
                    else
                    {
                        listOdenenler.Items[listedeYeniGelenSiparisVarmi].SubItems[0].Text = (Convert.ToDouble(listOdenenler.Items[listedeYeniGelenSiparisVarmi].SubItems[0].Text) + kacPorsiyon).ToString();

                        listOdenenler.Items[listedeYeniGelenSiparisVarmi].SubItems[2].Text = (Convert.ToDecimal(listOdenenler.Items[listedeYeniGelenSiparisVarmi].SubItems[2].Text) + (decimal)kacPorsiyon * yemeginFiyati).ToString("0.00");
                    }
                }

                // BURADA ODEME BILGILERINE SORGU AT  
                cmd = SQLBaglantisi.getCommand("SELECT OdemeTipi, OdenenMiktar from OdemeDetay JOIN Adisyon ON OdemeDetay.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.MasaAdi='" + masaAdi + "' AND Adisyon.DepartmanAdi='" + departmanAdi + "' AND Adisyon.AcikMi=1 AND Adisyon.IptalMi=0");

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    int odemeTipi;
                    decimal odenenMiktar;
                    try
                    {
                        odemeTipi = dr.GetInt32(0);
                        odenenMiktar = dr.GetDecimal(1);
                    }
                    catch
                    {
                        KontrolFormu dialog = new KontrolFormu("Ödeme bilgileri alınırken hata oluştu, lütfen tekrar giriş yapınız", false);
                        dialog.Show();
                        return;
                    }

                    if (odemeTipi == 0) // nakit
                    {
                        labelOdenenNakit.Text = (Convert.ToDecimal(labelOdenenNakit.Text) + odenenMiktar).ToString("0.00");
                    }
                    else if (odemeTipi == 1) // kredi kartı
                    {
                        labelOdenenKart.Text = (Convert.ToDecimal(labelOdenenKart.Text) + odenenMiktar).ToString("0.00");
                    }
                    else // yemek fişi
                    {
                        labelOdenenFis.Text = (Convert.ToDecimal(labelOdenenFis.Text) + odenenMiktar).ToString("0.00");
                    }

                    labelOdenenToplam.Text = (Convert.ToDecimal(labelOdenenToplam.Text) + odenenMiktar).ToString("0.00");

                    toplamHesap -= odenenMiktar;
                }
                cmd.Connection.Close();
                cmd.Connection.Dispose();

                textBoxSecilenlerinTutari.Text = toplamHesap.ToString("0.00");
                textNumberOfItem.Text = toplamHesap.ToString("0.00");
                labelKalanHesap.Text = toplamHesap.ToString("0.00");
            }
            else
            {
                //servera söyle herkesi masadan çıkarsın

                menuFormu.masaFormu.menuFormundanServeraYolla(masaAdi, departmanAdi, "hesapOdeniyor");

                menuFormu.masaFormu.menuFormundanServeraYolla(masaAdi, departmanAdi, "OdenenleriGonder");
            }
        }

        public void odenenlerGeldi(string odenenSiparisler, string odemeler)
        {
            string[] siparisler;
            try
            {
                siparisler = odenenSiparisler.Split('*');
            }
            catch
            {
                KontrolFormu dialog = new KontrolFormu("Ödeme bilgileri alınırken hata oluştu, lütfen tekrar giriş yapınız", false);
                dialog.Show();
                return;
            }

            decimal yemeginFiyati;
            double kacPorsiyon;
            string yemeginAdi;

            for (int i = 0; i < siparisler.Count(); i++)
            {
                string[] detaylari = siparisler[i].Split('-');
                yemeginFiyati = Convert.ToDecimal(detaylari[0]);
                kacPorsiyon = Convert.ToDouble(detaylari[1]);
                yemeginAdi = detaylari[2];

                int gruptaYeniGelenSiparisVarmi = -1; //ürün cinsi hesapta var mı bak 
                for (int j = 0; j < listOdenenler.Items.Count; j++)
                {
                    if (yemeginAdi == listOdenenler.Items[j].SubItems[1].Text)
                    {
                        gruptaYeniGelenSiparisVarmi = j;
                        break;
                    }
                }

                if (gruptaYeniGelenSiparisVarmi == -1) //yoksa ürünü hesaba ekle
                {
                    listOdenenler.Items.Add(kacPorsiyon.ToString());
                    listOdenenler.Items[listOdenenler.Items.Count - 1].SubItems.Add(yemeginAdi);
                    listOdenenler.Items[listOdenenler.Items.Count - 1].SubItems.Add(((decimal)kacPorsiyon * yemeginFiyati).ToString("0.00"));
                    listOdenenler.Items[listOdenenler.Items.Count - 1].Font = new Font("Calibri", 18.75F, FontStyle.Bold);

                    int itemsCount = this.listOdenenler.Items.Count;
                    int itemHeight = this.listOdenenler.Items[0].Bounds.Height;
                    int VisiableItem = (int)this.listOdenenler.ClientRectangle.Height / itemHeight;

                    if (itemsCount >= VisiableItem)
                    {
                        listOdenenler.Columns[1].Width = urunBoyu;
                        listOdenenler.Columns[2].Width = fiyatBoyu;

                        for (int k = 0; k < listOdenenler.Items.Count; k++)
                        {
                            while (listOdenenler.Columns[0].Width < System.Windows.Forms.TextRenderer.MeasureText(listOdenenler.Items[k].SubItems[0].Text, new Font(listOdenenler.Items[k].Font.FontFamily, listOdenenler.Items[k].Font.Size, listOdenenler.Items[k].Font.Style)).Width)
                            {
                                listOdenenler.Items[k].Font = new Font(listOdenenler.Items[k].Font.FontFamily, listOdenenler.Items[k].Font.Size - 0.5f, listOdenenler.Items[k].Font.Style);
                            }
                            while (listOdenenler.Columns[1].Width < System.Windows.Forms.TextRenderer.MeasureText(listOdenenler.Items[k].SubItems[1].Text, new Font(listOdenenler.Items[k].Font.FontFamily, listOdenenler.Items[k].Font.Size, listOdenenler.Items[k].Font.Style)).Width)
                            {
                                listOdenenler.Items[k].Font = new Font(listOdenenler.Items[k].Font.FontFamily, listOdenenler.Items[k].Font.Size - 0.5f, listOdenenler.Items[k].Font.Style);
                            }

                            while (listOdenenler.Columns[2].Width < System.Windows.Forms.TextRenderer.MeasureText(listOdenenler.Items[k].SubItems[2].Text, new Font(listOdenenler.Items[k].Font.FontFamily, listOdenenler.Items[k].Font.Size, listOdenenler.Items[k].Font.Style)).Width)
                            {
                                listOdenenler.Items[k].Font = new Font(listOdenenler.Items[k].Font.FontFamily, listOdenenler.Items[k].Font.Size - 0.5f, listOdenenler.Items[k].Font.Style);
                            }
                        }
                    }

                    while (listOdenenler.Columns[1].Width < System.Windows.Forms.TextRenderer.MeasureText(yemeginAdi, listOdenenler.Items[listOdenenler.Items.Count - 1].Font).Width
                        || listOdenenler.Columns[2].Width < System.Windows.Forms.TextRenderer.MeasureText(((decimal)kacPorsiyon * yemeginFiyati).ToString("0.00"), listOdenenler.Items[listOdenenler.Items.Count - 1].Font).Width
                        || listOdenenler.Columns[0].Width < System.Windows.Forms.TextRenderer.MeasureText(kacPorsiyon.ToString(), listOdenenler.Items[listOdenenler.Items.Count - 1].Font).Width)
                    {
                        listOdenenler.Items[listOdenenler.Items.Count - 1].Font = new Font(listOdenenler.Items[listOdenenler.Items.Count - 1].Font.FontFamily, listOdenenler.Items[listOdenenler.Items.Count - 1].Font.Size - 0.5f, listOdenenler.Items[listOdenenler.Items.Count - 1].Font.Style);
                    }
                }
                else // varsa ürünün hesaptaki değerlerini istenilene göre arttır
                {
                    listOdenenler.Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text = (Convert.ToDouble(listOdenenler.Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text) + kacPorsiyon).ToString();

                    listOdenenler.Items[gruptaYeniGelenSiparisVarmi].SubItems[2].Text = (Convert.ToDecimal(listOdenenler.Items[gruptaYeniGelenSiparisVarmi].SubItems[2].Text) + (decimal)kacPorsiyon * yemeginFiyati).ToString("0.00");

                    while (listOdenenler.Columns[2].Width < System.Windows.Forms.TextRenderer.MeasureText(listOdenenler.Items[gruptaYeniGelenSiparisVarmi].SubItems[2].Text, listOdenenler.Items[gruptaYeniGelenSiparisVarmi].Font).Width
                        || listOdenenler.Columns[0].Width < System.Windows.Forms.TextRenderer.MeasureText(listOdenenler.Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text, listOdenenler.Items[gruptaYeniGelenSiparisVarmi].Font).Width)
                    {
                        listOdenenler.Items[gruptaYeniGelenSiparisVarmi].Font = new Font(listOdenenler.Items[gruptaYeniGelenSiparisVarmi].Font.FontFamily, listOdenenler.Items[gruptaYeniGelenSiparisVarmi].Font.Size - 0.5f, listOdenenler.Items[gruptaYeniGelenSiparisVarmi].Font.Style);
                    }
                }
            }

            if (this.listOdenenler.Items.Count > 0)
            {
                int itemsCount = this.listOdenenler.Items.Count;
                int itemHeight = this.listOdenenler.Items[0].Bounds.Height;
                int VisiableItem = (int)this.listOdenenler.ClientRectangle.Height / itemHeight;
                if (itemsCount >= VisiableItem)
                {
                    listOdenenler.Columns[1].Width = urunBoyu;
                    listOdenenler.Columns[2].Width = fiyatBoyu;
                }
            }

            string[] odeme;
            try
            {
                odeme = odemeler.Split('*');
            }
            catch
            {
                KontrolFormu dialog = new KontrolFormu("Ödeme bilgileri alınırken hata oluştu, lütfen tekrar giriş yapınız", false);
                dialog.Show();
                return;
            }

            int odemeTipi;
            decimal odenenMiktar;

            for (int i = 0; i < odeme.Count(); i++)
            {
                string[] detaylari = siparisler[i].Split('-');
                odemeTipi = Convert.ToInt32(detaylari[0]);
                odenenMiktar = Convert.ToDecimal(detaylari[1]);
                if (odemeTipi == 0) // nakit
                {
                    labelOdenenNakit.Text = (Convert.ToDecimal(labelOdenenNakit.Text) + odenenMiktar).ToString("0.00");
                }
                else if (odemeTipi == 1) // kredi kartı
                {
                    labelOdenenKart.Text = (Convert.ToDecimal(labelOdenenKart.Text) + odenenMiktar).ToString("0.00");
                }
                else // yemek fişi
                {
                    labelOdenenFis.Text = (Convert.ToDecimal(labelOdenenFis.Text) + odenenMiktar).ToString("0.00");
                }

                labelOdenenToplam.Text = (Convert.ToDecimal(labelOdenenToplam.Text) + odenenMiktar).ToString("0.00");

                toplamHesap -= odenenMiktar;
            }

            textBoxSecilenlerinTutari.Text = toplamHesap.ToString("0.00");
            textNumberOfItem.Text = toplamHesap.ToString("0.00");
            labelKalanHesap.Text = toplamHesap.ToString("0.00");
        }

        //Kalan hesap 0 ın altına indiğinde çalışması gereken method
        private void labelKalanHesap_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(((Label)sender).Text) <= 0)
            {
                labelKalanText.Text = "Para Üstü:";
                ((Label)sender).ForeColor = Color.Firebrick;
                labelKalanText.ForeColor = Color.Firebrick;
                buttonKart.Enabled = false;
                buttonNakit.Enabled = false;
                buttonYemekFisi.Enabled = false;
            }
            else
            {
                labelKalanText.Text = "Kalan:";
                ((Label)sender).ForeColor = Color.White;
                labelKalanText.ForeColor = Color.White;
                buttonKart.Enabled = true;
                buttonNakit.Enabled = true;
                buttonYemekFisi.Enabled = true;
            }
        }

        private void buttonIndirim_Click(object sender, EventArgs e)
        {
            toplamIndirim -= indirim;
            labelIndirimToplam.Text = labelIndirimToplam.Text.Substring(9, labelIndirimToplam.Text.Length - 11);
            labelIndirimToplam.Text = (Convert.ToDecimal(labelIndirimToplam.Text) - indirim).ToString("0.00");
            try
            {
                indirim = Convert.ToDecimal(textNumberOfItem.Text);
            }
            catch
            {
                indirim = 0;
            }

            if (indirim >= toplamHesap)
            {
                indirim = toplamHesap;
                textBoxSecilenlerinTutari.Text = "0,00";
            }
            else
            {
                textBoxSecilenlerinTutari.Text = (toplamHesap - indirim).ToString("0.00");
            }

            labelIndirimTLTutar.Text = indirim.ToString("0.00");
            labelIndirimToplam.Text = "(indirim:" + (Convert.ToDecimal(labelIndirimToplam.Text) + indirim).ToString("0.00") + ")";


            if (indirim == 0)
            {
                labelIndirimTL.Visible = false;
                labelIndirimTLTutar.Visible = false;
                if (!labelIndirimYuzde.Visible) // eğer yüzdeli indirim de yoksa labelları kaldır
                {
                    labelIndirimToplam.Visible = false;
                }
            }
            else
            {
                labelIndirimToplam.Visible = true;
                labelIndirimTL.Visible = true;
                labelIndirimTLTutar.Visible = true;
                toplamIndirim += indirim;
            }

            textNumberOfItem.Text = "0,00";
            labelKalanHesap.Text = (toplamHesap - toplamIndirim).ToString("0.00");
        }

        private void buttonIndirimYuzdeli_Click(object sender, EventArgs e)
        {
            toplamIndirim -= indirimYuzde;
            labelIndirimToplam.Text = labelIndirimToplam.Text.Substring(9, labelIndirimToplam.Text.Length - 11);
            labelIndirimToplam.Text = (Convert.ToDecimal(labelIndirimToplam.Text) - indirimYuzde).ToString("0.00");
            try
            {
                indirimYuzde = Convert.ToDecimal(textNumberOfItem.Text);
            }
            catch
            {
                indirimYuzde = 0;
            }

            if (indirimYuzde >= 100)
            {
                indirimYuzde = toplamHesap;
                textBoxSecilenlerinTutari.Text = "0,00";
            }
            else
            {
                indirimYuzde = toplamHesap * indirimYuzde / 100;
                textBoxSecilenlerinTutari.Text = (toplamHesap - indirimYuzde).ToString("0.00");
            }

            labelIndirimYuzdeTutar.Text = indirimYuzde.ToString("0.00");
            labelIndirimToplam.Text = "(indirim:" + (Convert.ToDecimal(labelIndirimToplam.Text) + indirimYuzde).ToString("0.00") + ")";

            if (indirimYuzde == 0)
            {
                labelIndirimYuzde.Visible = false;
                labelIndirimYuzdeTutar.Visible = false;
                if (!labelIndirimTL.Visible) // eğer normal indirim de yoksa labelları kaldır
                {
                    labelIndirimToplam.Visible = false;
                }
            }
            else
            {
                labelIndirimToplam.Visible = true;
                labelIndirimYuzde.Visible = true;
                labelIndirimYuzdeTutar.Visible = true;
                toplamIndirim += indirimYuzde;
            }

            textNumberOfItem.Text = "0,00";
            labelKalanHesap.Text = (toplamHesap - toplamIndirim).ToString("0.00");
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
            else if (adet <= 1)
            {
                sonSecilenItem.SubItems[1].Text = "-";
                seciliItemSayisi--;
            }

            if (seciliItemSayisi != 0)
            {
                textBoxSecilenlerinTutari.Text = (Convert.ToDecimal(textBoxSecilenlerinTutari.Text) - (Convert.ToDecimal(sonSecilenItem.SubItems[3].Text) / Convert.ToDecimal(sonSecilenItem.SubItems[0].Text))).ToString("0.00");
                textNumberOfItem.Text = (Convert.ToDecimal(textNumberOfItem.Text) - (Convert.ToDecimal(sonSecilenItem.SubItems[3].Text) / Convert.ToDecimal(sonSecilenItem.SubItems[0].Text))).ToString("0.00");
            }
            else
            {
                textNumberOfItem.Text = "0,00";
                textBoxSecilenlerinTutari.Text = (toplamHesap - toplamIndirim).ToString("0.00");
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
                    adet++;
                    if (adet > Convert.ToDouble(sonSecilenItem.SubItems[0].Text))
                        adet = Convert.ToDouble(sonSecilenItem.SubItems[0].Text);

                    sonSecilenItem.SubItems[1].Text = "(" + (adet) + ")";

                    textBoxSecilenlerinTutari.Text = (Convert.ToDecimal(textBoxSecilenlerinTutari.Text) + (Convert.ToDecimal(sonSecilenItem.SubItems[3].Text) / Convert.ToDecimal(sonSecilenItem.SubItems[0].Text))).ToString("0.00");
                    textNumberOfItem.Text = (Convert.ToDecimal(textNumberOfItem.Text) + (Convert.ToDecimal(sonSecilenItem.SubItems[3].Text) / Convert.ToDecimal(sonSecilenItem.SubItems[0].Text))).ToString("0.00");
                    seciliItemSayisi++;
                }
            }
            catch
            {
                sonSecilenItem.SubItems[1].Text = "(1)";
                if (seciliItemSayisi != 0)
                {
                    textBoxSecilenlerinTutari.Text = (Convert.ToDecimal(textBoxSecilenlerinTutari.Text) + (Convert.ToDecimal(sonSecilenItem.SubItems[3].Text) / Convert.ToDecimal(sonSecilenItem.SubItems[0].Text))).ToString("0.00");
                    textNumberOfItem.Text = (Convert.ToDecimal(textNumberOfItem.Text) + (Convert.ToDecimal(sonSecilenItem.SubItems[3].Text) / Convert.ToDecimal(sonSecilenItem.SubItems[0].Text))).ToString("0.00");
                }
                else
                {
                    textBoxSecilenlerinTutari.Text = (Convert.ToDecimal(sonSecilenItem.SubItems[3].Text) / Convert.ToDecimal(sonSecilenItem.SubItems[0].Text)).ToString("0.00");
                    textNumberOfItem.Text = (Convert.ToDecimal(sonSecilenItem.SubItems[3].Text) / Convert.ToDecimal(sonSecilenItem.SubItems[0].Text)).ToString("0.00");
                }
                seciliItemSayisi++;
            }

            if (Convert.ToDecimal(textBoxSecilenlerinTutari.Text) > toplamHesap)
                textBoxSecilenlerinTutari.Text = toplamHesap.ToString("0.00");
            if (Convert.ToDecimal(textNumberOfItem.Text) > toplamHesap)
                textNumberOfItem.Text = toplamHesap.ToString("0.00");

            listUrunFiyat.SelectedItems.Clear();
        }

        private void buttonOdeme_Click(object sender, EventArgs e)
        {
            decimal odenenMiktar;
            try
            {
                odenenMiktar = Convert.ToDecimal(textNumberOfItem.Text);
                if (odenenMiktar <= 0)
                {
                    textNumberOfItem.Text = "0,00";
                    return;
                }
            }
            catch
            {
                textNumberOfItem.Text = "0,00";
                return;
            }

            if (Properties.Settings.Default.Server == 2) //server - diğer tüm clientlara söylemeli yaptığı ikram vs. neyse
            {

                // adisyon id al 
                int adisyonID;
                SqlCommand cmd = SQLBaglantisi.getCommand("SELECT AdisyonID FROM Adisyon WHERE MasaAdi='" + masaAdi + "' AND DepartmanAdi='" + departmanAdi + "' AND AcikMi=1");
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();

                try // açık
                {
                    adisyonID = dr.GetInt32(0);
                }
                catch// kapalı
                {
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                    KontrolFormu dialog = new KontrolFormu("Ödeme bilgileri kaydedilirken bir hata oluştu, lütfen tekrar deneyiniz", false);
                    dialog.Show();
                    return;
                }

                // odeme miktarını ve türünü gir
                cmd = SQLBaglantisi.getCommand("INSERT INTO OdemeDetay(AdisyonID,OdemeTipi,OdenenMiktar) VALUES(@_AdisyonID,@_OdemeTipi,@_OdenenMiktar)");
                cmd.Parameters.AddWithValue("@_AdisyonID", adisyonID);
                cmd.Parameters.AddWithValue("@_OdemeTipi", Convert.ToInt32(((Button)sender).Tag));
                cmd.Parameters.AddWithValue("@_OdenenMiktar", odenenMiktar);

                cmd.ExecuteNonQuery();

                cmd.Connection.Close();
                cmd.Connection.Dispose();

                //ödeme yapılırken sipariş seçilmişse seçilenleri azalt
                for (int i = listUrunFiyat.Items.Count - 1; i > -1; i--)
                {
                    if (listUrunFiyat.Items[i].SubItems[1].Text != "-")
                    {
                        int listedeYeniGelenSiparisVarmi = -1; //ürün cinsi alttaki ödenenlerde var mı bak 

                        for (int j = 0; j < listOdenenler.Items.Count; j++)
                        {
                            if (listUrunFiyat.Items[i].SubItems[2].Text == listOdenenler.Items[j].SubItems[1].Text)
                            {
                                listedeYeniGelenSiparisVarmi = j;
                                break;
                            }
                        }

                        if (listedeYeniGelenSiparisVarmi == -1) //yoksa ürünü hesaba ekle
                        {
                            listOdenenler.Items.Add(listUrunFiyat.Items[i].SubItems[1].Text.ToString());
                            listOdenenler.Items[listOdenenler.Items.Count - 1].SubItems.Add(listUrunFiyat.Items[i].SubItems[2].Text);
                            listOdenenler.Items[listOdenenler.Items.Count - 1].SubItems.Add((Convert.ToDecimal(listUrunFiyat.Items[i].SubItems[1].Text) * Convert.ToDecimal(listUrunFiyat.Items[i].SubItems[3].Text)).ToString("0.00"));
                            listOdenenler.Items[listOdenenler.Items.Count - 1].Font = new Font("Calibri", 18.75F, FontStyle.Bold);
                        }
                        else // varsa ödenenlerdeki ürün adedi ve fiyatını arttır
                        {
                            listOdenenler.Items[listedeYeniGelenSiparisVarmi].SubItems[0].Text = (Convert.ToDouble(listUrunFiyat.Items[listedeYeniGelenSiparisVarmi].SubItems[1].Text) + Convert.ToDouble(listOdenenler.Items[listedeYeniGelenSiparisVarmi].SubItems[0].Text)).ToString();

                            listOdenenler.Items[listedeYeniGelenSiparisVarmi].SubItems[2].Text = (Convert.ToDecimal(listOdenenler.Items[listedeYeniGelenSiparisVarmi].SubItems[2].Text) + Convert.ToDecimal(listUrunFiyat.Items[listedeYeniGelenSiparisVarmi].SubItems[1].Text) * Convert.ToDecimal(listUrunFiyat.Items[listedeYeniGelenSiparisVarmi].SubItems[3].Text)).ToString("0.00");
                        }

                        //azaltan kısım
                        listUrunFiyat.Items[i].SubItems[0].Text = (Convert.ToDouble(listUrunFiyat.Items[i].SubItems[0].Text) - Convert.ToDouble(listUrunFiyat.Items[i].SubItems[1].Text)).ToString();
                        //tamamı seçildiyse var olanı sil
                        if (Convert.ToDouble(listUrunFiyat.Items[i].SubItems[0].Text) == 0)
                            listUrunFiyat.Items[i].Remove();
                    }
                }

                // ödeme bilgilerini ekranda göster
                int odemeTipi = Convert.ToInt32(((Button)sender).Tag);

                if (odemeTipi == 0) // nakit
                {
                    labelOdenenNakit.Text = (Convert.ToDecimal(labelOdenenNakit.Text) + odenenMiktar).ToString("0.00");
                }
                else if (odemeTipi == 1) // kredi kartı
                {
                    labelOdenenKart.Text = (Convert.ToDecimal(labelOdenenKart.Text) + odenenMiktar).ToString("0.00");
                }
                else // yemek fişi
                {
                    labelOdenenFis.Text = (Convert.ToDecimal(labelOdenenFis.Text) + odenenMiktar).ToString("0.00");
                }

                labelOdenenToplam.Text = (Convert.ToDecimal(labelOdenenToplam.Text) + odenenMiktar).ToString("0.00");

                toplamHesap -= odenenMiktar;

                textBoxSecilenlerinTutari.Text = (toplamHesap - toplamIndirim).ToString("0.00");
                labelKalanHesap.Text = toplamHesap.ToString("0.00");
                textNumberOfItem.Text = "0,00";
            }
            else //client
            {
                //ödeme yapılırken sipariş seçilmişse onları secilipOdenenSiparisBilgileri ne ekle
                StringBuilder secilipOdenenSiparisBilgileri = new StringBuilder();

                for (int i = listUrunFiyat.Items.Count - 1; i > -1; i--)
                {
                    if (listUrunFiyat.Items[i].SubItems[1].Text != "-")
                    {
                        secilipOdenenSiparisBilgileri.Append("*" + listUrunFiyat.Items[i].SubItems[1].Text + "-" + listUrunFiyat.Items[i].SubItems[2].Text + "-" + listUrunFiyat.Items[i].SubItems[3].Text);
                        //eklenenleri var olandan düş
                        listUrunFiyat.Items[i].SubItems[0].Text = (Convert.ToDouble(listUrunFiyat.Items[i].SubItems[0].Text) - Convert.ToDouble(listUrunFiyat.Items[i].SubItems[1].Text)).ToString();
                        //tamamı eklendiyse var olanı sil
                        if (Convert.ToDouble(listUrunFiyat.Items[i].SubItems[0].Text) == 0)
                            listUrunFiyat.Items[i].Remove();
                    }
                }
                //baştaki * ı sil
                if (secilipOdenenSiparisBilgileri.Length >= 1)
                {
                    secilipOdenenSiparisBilgileri.Remove(0, 1);
                }
                else
                {
                    secilipOdenenSiparisBilgileri = null;
                }
                //bilgileri servera gönder
                menuFormu.masaFormu.hesapFormundanOdeme(masaAdi, departmanAdi, "OdemeYapildi", Convert.ToInt32(((Button)sender).Tag), Convert.ToDecimal(textNumberOfItem.Text), secilipOdenenSiparisBilgileri);
            }
            buttonDeleteText_Click(null, null);
        }

        public void odemeOnaylandi(string odemeTipiGelen, string odenenMiktarGelen, string secilipOdenenSiparisBilgileri)
        {
            string[] siparisler;
            try
            {
                siparisler = secilipOdenenSiparisBilgileri.Split('*');
            }
            catch
            {
                KontrolFormu dialog = new KontrolFormu("Ödeme bilgileri alınırken hata oluştu, lütfen tekrar giriş yapınız", false);
                dialog.Show();
                return;
            }
            double kacPorsiyon;
            string yemeginAdi;
            decimal yemeginFiyati;

            for (int i = 0; i < siparisler.Count(); i++)
            {
                string[] detaylari = siparisler[i].Split('-');
                kacPorsiyon = Convert.ToDouble(detaylari[0]);
                yemeginAdi = detaylari[1];
                yemeginFiyati = Convert.ToDecimal(detaylari[2]);


                int listedeYeniGelenSiparisVarmi = -1; //ürün cinsi hesapta var mı bak 

                for (int j = 0; j < listOdenenler.Items.Count; j++)
                {
                    if (yemeginAdi == listOdenenler.Items[j].SubItems[1].Text)
                    {
                        listedeYeniGelenSiparisVarmi = j;
                        break;
                    }
                }

                if (listedeYeniGelenSiparisVarmi == -1) //yoksa ürünü hesaba ekle
                {
                    listOdenenler.Items.Add(kacPorsiyon.ToString());
                    listOdenenler.Items[listOdenenler.Items.Count - 1].SubItems.Add(yemeginAdi);
                    listOdenenler.Items[listOdenenler.Items.Count - 1].SubItems.Add(((decimal)kacPorsiyon * yemeginFiyati).ToString("0.00"));
                    listOdenenler.Items[listOdenenler.Items.Count - 1].Font = new Font("Calibri", 18.75F, FontStyle.Bold);
                }
                else
                {
                    listOdenenler.Items[listedeYeniGelenSiparisVarmi].SubItems[0].Text = (Convert.ToDouble(listOdenenler.Items[listedeYeniGelenSiparisVarmi].SubItems[0].Text) + kacPorsiyon).ToString();

                    listOdenenler.Items[listedeYeniGelenSiparisVarmi].SubItems[2].Text = (Convert.ToDecimal(listOdenenler.Items[listedeYeniGelenSiparisVarmi].SubItems[2].Text) + (decimal)kacPorsiyon * yemeginFiyati).ToString("0.00");
                }
            }

            int odemeTipi = Convert.ToInt32(odemeTipiGelen);
            decimal odenenMiktar = Convert.ToDecimal(odenenMiktarGelen);

            if (odemeTipi == 0) // nakit
            {
                labelOdenenNakit.Text = (Convert.ToDecimal(labelOdenenNakit.Text) + odenenMiktar).ToString("0.00");
            }
            else if (odemeTipi == 1) // kredi kartı
            {
                labelOdenenKart.Text = (Convert.ToDecimal(labelOdenenKart.Text) + odenenMiktar).ToString("0.00");
            }
            else // yemek fişi
            {
                labelOdenenFis.Text = (Convert.ToDecimal(labelOdenenFis.Text) + odenenMiktar).ToString("0.00");
            }

            labelOdenenToplam.Text = (Convert.ToDecimal(labelOdenenToplam.Text) + odenenMiktar).ToString("0.00");

            toplamHesap -= odenenMiktar;

            textBoxSecilenlerinTutari.Text = (toplamHesap - toplamIndirim).ToString("0.00");
            labelKalanHesap.Text = toplamHesap.ToString("0.00");
            textNumberOfItem.Text = "0,00";
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

        #region SQL İşlemleri

        public void odemeyeGec()
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET OdemeYapiliyor=@odemeYapiliyor WHERE AdisyonID=(SELECT AdisyonID FROM Adisyon WHERE AcikMi=1 AND MasaAdi='" + masaAdi + "' AND DepartmanAdi='" + departmanAdi + "')");

            cmd.Parameters.AddWithValue("@odemeYapiliyor", 1);

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public int adisyonOlustur()
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("INSERT INTO Adisyon(AcikMi,AdisyonNotu,AcilisZamani,DepartmanAdi,MasaAdi) VALUES(@_acikMi,@_AdisyonNotu,@_AcilisZamani,@_DepartmanAdi,@_MasaAdi) SELECT SCOPE_IDENTITY()");

            cmd.Parameters.AddWithValue("@_acikmi", 1);
            cmd.Parameters.AddWithValue("@_AdisyonNotu", "");
            cmd.Parameters.AddWithValue("@_AcilisZamani", DateTime.Now);
            cmd.Parameters.AddWithValue("@_DepartmanAdi", departmanAdi);
            cmd.Parameters.AddWithValue("@_MasaAdi", masaAdi);

            int adisyonID = Convert.ToInt32(cmd.ExecuteScalar());

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            menuFormu.masaAcikMi = true;
            menuFormu.buttonMasaDegistir.Enabled = true;

            return adisyonID;
        }

        public void siparisOlustur(int adisyonID, ListViewItem siparis)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("INSERT INTO Siparis(AdisyonID,Garsonu,Fiyatı,Porsiyon,YemekAdi,VerilisTarihi) VALUES(@_AdisyonID,@_Garsonu,@_Fiyatı,@_Porsiyon,@_YemekAdi,@_VerilisTarihi)");
            cmd.Parameters.AddWithValue("@_AdisyonID", adisyonID);
            cmd.Parameters.AddWithValue("@_Garsonu", siparisiGirenKisi);
            cmd.Parameters.AddWithValue("@_Fiyatı", Convert.ToDecimal(siparis.SubItems[2].Text) / Convert.ToDecimal(siparis.SubItems[0].Text));
            cmd.Parameters.AddWithValue("@_Porsiyon", Convert.ToDecimal(siparis.SubItems[0].Text));
            cmd.Parameters.AddWithValue("@_YemekAdi", siparis.SubItems[1].Text);
            cmd.Parameters.AddWithValue("@_VerilisTarihi", DateTime.Now);

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        #endregion

        private void buttonHesapYazdir_Click(object sender, EventArgs e)
        {

        }

        private void buttonAdisyonYazdir_Click(object sender, EventArgs e)
        {

        }


    }
}