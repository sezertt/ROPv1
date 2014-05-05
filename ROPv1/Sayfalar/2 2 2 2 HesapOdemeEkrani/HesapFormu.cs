﻿using System;
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
// 3 İNDİRİM TL
// 4 İNDİRİM %

namespace ROPv1
{
    public partial class HesapFormu : Form
    {
        private SiparisMenuFormu menuFormu;

        string masaAdi, departmanAdi, siparisiGirenKisi;

        ListViewItem sonSecilenItem;

        MyListView listHesaptakiler;

        decimal toplamHesap = 0, indirim = 0, indirimYuzde = 0, toplamOdemeVeIndirim = 0;

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

            decimal secilienTutar = toplamHesap - toplamOdemeVeIndirim;

            if (secilienTutar < 0)
                secilienTutar = 0;
            textBoxSecilenlerinTutari.Text = (secilienTutar).ToString("0.00");
            textNumberOfItem.Text = textBoxSecilenlerinTutari.Text;
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

            bool odemedeSiparisSeciliMi = false;

            for (int i = listUrunFiyat.Items.Count - 1; i > -1; i--)
            {
                if (listUrunFiyat.Items[i].SubItems[1].Text != "-")
                {
                    odemedeSiparisSeciliMi = true;
                    break;
                }
            }

            if (odemedeSiparisSeciliMi)
            {
                if (Convert.ToDecimal(textNumberOfItem.Text) < Convert.ToDecimal(textBoxSecilenlerinTutari.Text))
                {
                    textNumberOfItem.Text = Convert.ToDecimal(textBoxSecilenlerinTutari.Text).ToString("0.00");
                }
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
                textNumberOfItem.Text = (toplamHesap * carpan - toplamOdemeVeIndirim).ToString("0.00");
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
                    toplamHesap += Convert.ToDecimal((decimal)kacPorsiyon * yemeginFiyati);
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
                    else if (odemeTipi == 2)// yemek fişi
                    {
                        labelOdenenFis.Text = (Convert.ToDecimal(labelOdenenFis.Text) + odenenMiktar).ToString("0.00");
                    }
                    else if (odemeTipi == 3)// indirim TL
                    {
                        labelIndirimTLTutar.Text = odenenMiktar.ToString("0.00");

                        labelIndirimToplam.Text = labelIndirimToplam.Text.Substring(9, labelIndirimToplam.Text.Length - 11);
                        labelIndirimToplam.Text = "(indirim:" + (Convert.ToDecimal(labelIndirimToplam.Text) + odenenMiktar).ToString("0.00") + ")";

                        if (odenenMiktar == 0)
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
                        }
                    }
                    else // indirim Yüzde
                    {

                        odenenMiktar = toplamHesap * odenenMiktar / 100;

                        labelIndirimYuzdeTutar.Text = odenenMiktar.ToString("0.00");

                        labelIndirimToplam.Text = labelIndirimToplam.Text.Substring(9, labelIndirimToplam.Text.Length - 11);
                        labelIndirimToplam.Text = "(indirim:" + (Convert.ToDecimal(labelIndirimToplam.Text) + odenenMiktar).ToString("0.00") + ")";

                        if (odenenMiktar == 0)
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
                        }
                    }
                    labelOdenenToplam.Text = (Convert.ToDecimal(labelOdenenToplam.Text) + odenenMiktar).ToString("0.00");
                    toplamOdemeVeIndirim += odenenMiktar;
                }
                cmd.Connection.Close();
                cmd.Connection.Dispose();

                textBoxSecilenlerinTutari.Text = (toplamHesap - toplamOdemeVeIndirim).ToString("0.00");
                textNumberOfItem.Text = (toplamHesap - toplamOdemeVeIndirim).ToString("0.00");
                labelKalanHesap.Text = (toplamHesap - toplamOdemeVeIndirim).ToString("0.00");
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

            if (siparisler.Count() > 0 && siparisler[0] != "")
            {
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

            if (odeme.Count() > 0 && odeme[0] != "")
            {
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
                    else if (odemeTipi == 2) // yemek fişi
                    {
                        labelOdenenFis.Text = (Convert.ToDecimal(labelOdenenFis.Text) + odenenMiktar).ToString("0.00");
                    }
                    else if (odemeTipi == 3) // indirim TL
                    {
                        labelIndirimTLTutar.Text = odenenMiktar.ToString("0.00");

                        labelIndirimToplam.Text = labelIndirimToplam.Text.Substring(9, labelIndirimToplam.Text.Length - 11);
                        labelIndirimToplam.Text = "(indirim:" + (Convert.ToDecimal(labelIndirimToplam.Text) + odenenMiktar).ToString("0.00") + ")";

                        if (odenenMiktar == 0)
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
                        }
                    }
                    else // indirim Yüzde
                    {

                        odenenMiktar = toplamHesap * odenenMiktar / 100;

                        labelIndirimYuzdeTutar.Text = odenenMiktar.ToString("0.00");

                        labelIndirimToplam.Text = labelIndirimToplam.Text.Substring(9, labelIndirimToplam.Text.Length - 11);
                        labelIndirimToplam.Text = "(indirim:" + (Convert.ToDecimal(labelIndirimToplam.Text) + odenenMiktar).ToString("0.00") + ")";

                        if (odenenMiktar == 0)
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
                        }
                    }
                    labelOdenenToplam.Text = (Convert.ToDecimal(labelOdenenToplam.Text) + odenenMiktar).ToString("0.00");
                    toplamOdemeVeIndirim -= odenenMiktar;                    
                }
            }

            textBoxSecilenlerinTutari.Text = (toplamHesap - toplamOdemeVeIndirim).ToString("0.00");
            textNumberOfItem.Text = (toplamHesap - toplamOdemeVeIndirim).ToString("0.00");
            labelKalanHesap.Text = (toplamHesap - toplamOdemeVeIndirim).ToString("0.00");
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
            toplamOdemeVeIndirim -= indirim;
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

            if (indirim > toplamHesap)
            {
                indirim = toplamHesap;
            }

            if (Properties.Settings.Default.Server == 2)
            {
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
                    toplamOdemeVeIndirim += indirim;
                }

                if (indirim == toplamHesap)
                {
                    textBoxSecilenlerinTutari.Text = "0,00";
                }
                else
                {
                    textBoxSecilenlerinTutari.Text = (toplamHesap - toplamOdemeVeIndirim).ToString("0.00");
                }

                textNumberOfItem.Text = textBoxSecilenlerinTutari.Text;

                labelKalanHesap.Text = (toplamHesap - toplamOdemeVeIndirim).ToString("0.00");

                SqlCommand cmd = SQLBaglantisi.getCommand("SELECT AdisyonID FROM Adisyon WHERE AcikMi=1 AND MasaAdi='" + masaAdi + "' AND DepartmanAdi='" + departmanAdi + "'");
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int adisyonID = dr.GetInt32(0);

                cmd = SQLBaglantisi.getCommand("IF EXISTS (SELECT * FROM OdemeDetay WHERE AdisyonID='" + adisyonID + "' AND OdemeTipi='" + Convert.ToInt32(((Button)sender).Tag) + "') UPDATE OdemeDetay SET OdenenMiktar='" + indirim + "' WHERE AdisyonID='" + adisyonID + "' AND OdemeTipi='" + Convert.ToInt32(((Button)sender).Tag) + "' ELSE INSERT INTO OdemeDetay(AdisyonID,OdemeTipi,OdenenMiktar) VALUES(@_AdisyonID,@_OdemeTipi,@_OdenenMiktar)");

                cmd.Parameters.AddWithValue("@_AdisyonID", adisyonID);
                cmd.Parameters.AddWithValue("@_OdemeTipi", Convert.ToInt32(Convert.ToInt32(((Button)sender).Tag)));
                cmd.Parameters.AddWithValue("@_OdenenMiktar", Convert.ToDecimal(indirim));

                cmd.ExecuteNonQuery();

                cmd.Connection.Close();
                cmd.Connection.Dispose();
            }
            else
            {
                //servera indirimi eklet
                menuFormu.masaFormu.hesapFormundanIndirim(masaAdi, departmanAdi, "Indirim", Convert.ToInt32(((Button)sender).Tag), indirim);
            }
        }

        private void buttonIndirimYuzdeli_Click(object sender, EventArgs e)
        {
            toplamOdemeVeIndirim -= indirimYuzde; // önceki indirimi çıkarıyoruz
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

            if (Properties.Settings.Default.Server == 2)
            {
                if (indirimYuzde > 100)
                {
                    indirimYuzde = toplamHesap;
                }
                else
                {
                    indirimYuzde = toplamHesap * indirimYuzde / 100;
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
                    toplamOdemeVeIndirim += indirimYuzde;
                }

                if (indirimYuzde == toplamHesap)
                {
                    textBoxSecilenlerinTutari.Text = "0,00";
                }
                else
                {
                    textBoxSecilenlerinTutari.Text = (toplamHesap - toplamOdemeVeIndirim).ToString("0.00");
                }

                textNumberOfItem.Text = textBoxSecilenlerinTutari.Text;

                labelKalanHesap.Text = (toplamHesap - toplamOdemeVeIndirim).ToString("0.00");

                SqlCommand cmd = SQLBaglantisi.getCommand("SELECT AdisyonID FROM Adisyon WHERE AcikMi=1 AND MasaAdi='" + masaAdi + "' AND DepartmanAdi='" + departmanAdi + "'");
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int adisyonID = dr.GetInt32(0);

                cmd = SQLBaglantisi.getCommand("IF EXISTS (SELECT * FROM OdemeDetay WHERE AdisyonID='" + adisyonID + "' AND OdemeTipi='" + Convert.ToInt32(((Button)sender).Tag) + "') UPDATE OdemeDetay SET OdenenMiktar='" + indirimYuzde + "' WHERE AdisyonID='" + adisyonID + "' AND OdemeTipi='" + Convert.ToInt32(((Button)sender).Tag) + "' ELSE INSERT INTO OdemeDetay(AdisyonID,OdemeTipi,OdenenMiktar) VALUES(@_AdisyonID,@_OdemeTipi,@_OdenenMiktar)");

                cmd.Parameters.AddWithValue("@_AdisyonID", adisyonID);
                cmd.Parameters.AddWithValue("@_OdemeTipi", Convert.ToInt32(Convert.ToInt32(((Button)sender).Tag)));
                cmd.Parameters.AddWithValue("@_OdenenMiktar", Convert.ToDecimal(indirimYuzde));

                cmd.ExecuteNonQuery();

                cmd.Connection.Close();
                cmd.Connection.Dispose();
            }
            else
            {
                if (indirimYuzde > 100)
                    indirimYuzde = 100;

                //servera indirimi eklet
                menuFormu.masaFormu.hesapFormundanIndirim(masaAdi, departmanAdi, "Indirim", Convert.ToInt32(((Button)sender).Tag), indirimYuzde);
            }
        }

        public void indirimOnaylandi(string odemeTipiGelen, string odenenMiktarGelen)
        {
            decimal indirimGelen = Convert.ToDecimal(odenenMiktarGelen);

            if (odemeTipiGelen == "3") // indirim TL
            {
                labelIndirimTLTutar.Text = indirimGelen.ToString("0.00");

                try
                {
                    labelIndirimToplam.Text = "(indirim:" + (Convert.ToDecimal(labelIndirimToplam.Text) + indirimGelen).ToString("0.00") + ")";
                }
                catch
                {
                    labelIndirimToplam.Text = labelIndirimToplam.Text.Substring(9, labelIndirimToplam.Text.Length - 11);
                    labelIndirimToplam.Text = "(indirim:" + (Convert.ToDecimal(labelIndirimToplam.Text) + indirimGelen).ToString("0.00") + ")";
                }

                if (indirimGelen == 0)
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
                    toplamOdemeVeIndirim += indirimGelen;
                }

                if (indirimGelen == toplamHesap)
                {
                    textBoxSecilenlerinTutari.Text = "0,00";
                }
                else
                {
                    textBoxSecilenlerinTutari.Text = (toplamHesap - toplamOdemeVeIndirim).ToString("0.00");
                }

                textNumberOfItem.Text = textBoxSecilenlerinTutari.Text;
                labelKalanHesap.Text = (toplamHesap - toplamOdemeVeIndirim).ToString("0.00");
            }
            else  // indirim yüzde
            {
                indirimGelen = toplamHesap * indirimGelen / 100;

                labelIndirimYuzdeTutar.Text = indirimGelen.ToString("0.00");

                try
                {
                    labelIndirimToplam.Text = "(indirim:" + (Convert.ToDecimal(labelIndirimToplam.Text) + indirimGelen).ToString("0.00") + ")";
                }
                catch
                {
                    labelIndirimToplam.Text = labelIndirimToplam.Text.Substring(9, labelIndirimToplam.Text.Length - 11);
                    labelIndirimToplam.Text = "(indirim:" + (Convert.ToDecimal(labelIndirimToplam.Text) + indirimGelen).ToString("0.00") + ")";
                }

                if (indirimGelen == 0)
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
                    toplamOdemeVeIndirim += indirimGelen;
                }

                if (indirimGelen == toplamHesap)
                {
                    textBoxSecilenlerinTutari.Text = "0,00";
                }
                else
                {
                    textBoxSecilenlerinTutari.Text = (toplamHesap - toplamOdemeVeIndirim).ToString("0.00");
                }

                textNumberOfItem.Text = textBoxSecilenlerinTutari.Text;
                labelKalanHesap.Text = (toplamHesap - toplamOdemeVeIndirim).ToString("0.00");
            }
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
                textBoxSecilenlerinTutari.Text = (toplamHesap - toplamOdemeVeIndirim).ToString("0.00");
                textNumberOfItem.Text = textBoxSecilenlerinTutari.Text;
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

            if (Convert.ToDecimal(textBoxSecilenlerinTutari.Text) > (toplamHesap - toplamOdemeVeIndirim))
                textBoxSecilenlerinTutari.Text = (toplamHesap - toplamOdemeVeIndirim).ToString("0.00");
            if (Convert.ToDecimal(textNumberOfItem.Text) > (toplamHesap - toplamOdemeVeIndirim))
                textNumberOfItem.Text = (toplamHesap - toplamOdemeVeIndirim).ToString("0.00");

            if (Convert.ToDecimal(textBoxSecilenlerinTutari.Text) < 0)
                textBoxSecilenlerinTutari.Text = "0,00";
            if (Convert.ToDecimal(textNumberOfItem.Text) < 0)
                textNumberOfItem.Text = "0,00";

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
                        //BURADA AKTARMALARDAKİ SİPARİŞLERİ UPDATE ET BOL VS. ikram iptaldeki gibi

                        decimal kacPorsiyon = Convert.ToDecimal(listUrunFiyat.Items[i].SubItems[1].Text.Substring(1, listUrunFiyat.Items[i].SubItems[1].Text.Length - 2));
                        string yemeginAdi = listUrunFiyat.Items[i].SubItems[2].Text;
                        decimal yemeginFiyati = Convert.ToDecimal(listUrunFiyat.Items[i].SubItems[3].Text) / Convert.ToDecimal(listUrunFiyat.Items[i].SubItems[0].Text);

                        cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Porsiyon,Siparis.VerilisTarihi,Siparis.Garsonu FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi=0 AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + masaAdi + "' AND Adisyon.DepartmanAdi='" + departmanAdi + "' AND Siparis.YemekAdi='" + yemeginAdi + "'ORDER BY Porsiyon DESC");

                        dr = cmd.ExecuteReader();

                        int siparisID;
                        decimal porsiyon;
                        DateTime verilisTarihi;
                        while (dr.Read())
                        {
                            try
                            {
                                siparisID = dr.GetInt32(0);
                                porsiyon = dr.GetDecimal(1);
                                verilisTarihi = dr.GetDateTime(2);
                                siparisiGirenKisi = dr.GetString(3);
                            }
                            catch
                            {
                                cmd.Connection.Close();
                                cmd.Connection.Dispose();
                                //HATA MESAJI GÖNDER
                                KontrolFormu dialog = new KontrolFormu("Ödeme işlemi gerçekleşirken hata oluştu, lütfen tekrar deneyiniz", false);
                                dialog.Show();
                                return;
                            }

                            if (porsiyon < kacPorsiyon) // ödenmesi istenenlerin sayısı(kacPorsiyon) ödenebileceklerden(porsiyon) küçükse
                            {
                                odendiUpdateTam(siparisID);

                                kacPorsiyon -= porsiyon;
                            }
                            else if (porsiyon > kacPorsiyon) // den büyükse
                            {
                                odendiUpdateInsert(siparisID, adisyonID, porsiyon, (double)yemeginFiyati, kacPorsiyon, yemeginAdi, verilisTarihi);

                                kacPorsiyon = 0;
                            }
                            else // elimizde ikram edilmemişler ikramı istenene eşitse
                            {
                                odendiUpdateTam(siparisID);

                                kacPorsiyon = 0;
                            }
                            if (kacPorsiyon == 0)
                                break;
                        }

                        cmd.Connection.Close();
                        cmd.Connection.Dispose();

                        int listedeYeniGelenSiparisVarmi = -1; //ürün cinsi alttaki ödenenlerde var mı bak 

                        for (int j = 0; j < listOdenenler.Items.Count; j++)
                        {
                            if (listUrunFiyat.Items[i].SubItems[2].Text == listOdenenler.Items[j].SubItems[1].Text)
                            {
                                listedeYeniGelenSiparisVarmi = j;
                                break;
                            }
                        }

                        double odenmekIstenen = Convert.ToDouble(listUrunFiyat.Items[i].SubItems[1].Text.Substring(1, listUrunFiyat.Items[i].SubItems[1].Text.Length - 2));
                        decimal fiyat = Convert.ToDecimal(listUrunFiyat.Items[i].SubItems[3].Text) / Convert.ToDecimal(listUrunFiyat.Items[i].SubItems[0].Text);

                        if (listedeYeniGelenSiparisVarmi == -1) //yoksa ürünü hesaba ekle
                        {
                            listOdenenler.Items.Add(odenmekIstenen.ToString());
                            listOdenenler.Items[listOdenenler.Items.Count - 1].SubItems.Add(listUrunFiyat.Items[i].SubItems[2].Text);
                            listOdenenler.Items[listOdenenler.Items.Count - 1].SubItems.Add(((decimal)odenmekIstenen * fiyat).ToString("0.00"));
                            listOdenenler.Items[listOdenenler.Items.Count - 1].Font = new Font("Calibri", 18.75F, FontStyle.Bold);
                        }
                        else // varsa ödenenlerdeki ürün adedi ve fiyatını arttır
                        {
                            listOdenenler.Items[listedeYeniGelenSiparisVarmi].SubItems[0].Text = (odenmekIstenen + Convert.ToDouble(listOdenenler.Items[listedeYeniGelenSiparisVarmi].SubItems[0].Text)).ToString();
                            listOdenenler.Items[listedeYeniGelenSiparisVarmi].SubItems[2].Text = (Convert.ToDecimal(listOdenenler.Items[listedeYeniGelenSiparisVarmi].SubItems[2].Text) + (decimal)odenmekIstenen * fiyat).ToString("0.00");
                        }

                        //azaltan kısım
                        listUrunFiyat.Items[i].SubItems[3].Text = (Convert.ToDecimal(listUrunFiyat.Items[i].SubItems[3].Text) - ((decimal)odenmekIstenen * fiyat)).ToString("0.00");
                        listUrunFiyat.Items[i].SubItems[0].Text = (Convert.ToDouble(listUrunFiyat.Items[i].SubItems[0].Text) - odenmekIstenen).ToString();

                        menuFormu.listUrunFiyat.Items[i].SubItems[2].Text = (Convert.ToDecimal(listUrunFiyat.Items[i].SubItems[3].Text) - ((decimal)odenmekIstenen * fiyat)).ToString("0.00");
                        menuFormu.listUrunFiyat.Items[i].SubItems[0].Text = (Convert.ToDouble(listUrunFiyat.Items[i].SubItems[0].Text) - odenmekIstenen).ToString();

                        //tamamı seçildiyse var olanı sil
                        if (Convert.ToDouble(listUrunFiyat.Items[i].SubItems[0].Text) == 0)
                        {
                            menuFormu.listUrunFiyat.Items[i].Remove();
                            listUrunFiyat.Items[i].Remove();
                        }
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

                toplamOdemeVeIndirim += odenenMiktar;

                menuFormu.labelKalanHesap.Text = (toplamHesap - toplamOdemeVeIndirim).ToString("0.00");
                textBoxSecilenlerinTutari.Text = (toplamHesap - toplamOdemeVeIndirim).ToString("0.00");
                labelKalanHesap.Text = (toplamHesap - toplamOdemeVeIndirim).ToString("0.00");
                textNumberOfItem.Text = textBoxSecilenlerinTutari.Text;
                buttonDeleteText_Click(null, null);
            }
            else //client
            {
                //ödeme yapılırken sipariş seçilmişse onları secilipOdenenSiparisBilgileri ne ekle
                StringBuilder secilipOdenenSiparisBilgileri = new StringBuilder();

                for (int i = listUrunFiyat.Items.Count - 1; i > -1; i--)
                {
                    if (listUrunFiyat.Items[i].SubItems[1].Text != "-")
                    {
                        double odenmekIstenen = Convert.ToDouble(listUrunFiyat.Items[i].SubItems[1].Text.Substring(1, listUrunFiyat.Items[i].SubItems[1].Text.Length - 2));
                        decimal fiyat = Convert.ToDecimal(listUrunFiyat.Items[i].SubItems[3].Text) / Convert.ToDecimal(listUrunFiyat.Items[i].SubItems[0].Text);

                        secilipOdenenSiparisBilgileri.Append("*" + odenmekIstenen.ToString() + "-" + listUrunFiyat.Items[i].SubItems[2].Text + "-" + fiyat);

                        //eklenenleri var olandan düş
                        listUrunFiyat.Items[i].SubItems[3].Text = (Convert.ToDecimal(listUrunFiyat.Items[i].SubItems[3].Text) - ((decimal)odenmekIstenen * fiyat)).ToString("0.00");
                        listUrunFiyat.Items[i].SubItems[0].Text = (Convert.ToDouble(listUrunFiyat.Items[i].SubItems[0].Text) - odenmekIstenen).ToString();

                        menuFormu.listUrunFiyat.Items[i].SubItems[2].Text = (Convert.ToDecimal(listUrunFiyat.Items[i].SubItems[3].Text) - ((decimal)odenmekIstenen * fiyat)).ToString("0.00");
                        menuFormu.listUrunFiyat.Items[i].SubItems[0].Text = (Convert.ToDouble(listUrunFiyat.Items[i].SubItems[0].Text) - odenmekIstenen).ToString();

                        //tamamı eklendiyse var olanı sil
                        if (Convert.ToDouble(listUrunFiyat.Items[i].SubItems[0].Text) == 0)
                        {
                            menuFormu.listUrunFiyat.Items[i].Remove();
                            listUrunFiyat.Items[i].Remove();
                        }
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

            if (siparisler.Count() > 0 && siparisler[0] != "")
            {
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

            toplamOdemeVeIndirim += odenenMiktar;

            menuFormu.labelKalanHesap.Text = (toplamHesap - toplamOdemeVeIndirim).ToString("0.00");
            textBoxSecilenlerinTutari.Text = (toplamHesap - toplamOdemeVeIndirim).ToString("0.00");
            labelKalanHesap.Text = (toplamHesap - toplamOdemeVeIndirim).ToString("0.00");
            textNumberOfItem.Text = textBoxSecilenlerinTutari.Text;
            buttonDeleteText_Click(null, null);
        }

        #region SQL İşlemleri

        public void odendiUpdateTam(int siparisID)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET OdendiMi=1 WHERE SiparisID=@id");
            cmd.Parameters.AddWithValue("@id", siparisID);
            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void odendiUpdateInsert(int siparisID, int adisyonID, decimal porsiyon, double fiyati, decimal odemeAdedi, string yemekAdi, DateTime verilisTarihi)
        {
            decimal yeniPorsiyonAdetiSiparis = porsiyon - odemeAdedi;

            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET Porsiyon = @Porsiyonu, OdendiMi=1 WHERE SiparisID=@id");
            cmd.Parameters.AddWithValue("@Porsiyonu", odemeAdedi);
            cmd.Parameters.AddWithValue("@id", siparisID);
            cmd.ExecuteNonQuery();

            cmd = SQLBaglantisi.getCommand("INSERT INTO Siparis(AdisyonID,Garsonu,Fiyatı,Porsiyon,YemekAdi,VerilisTarihi) values(@_AdisyonID,@_Garsonu,@_Fiyatı,@_Porsiyon,@_YemekAdi,@_VerilisTarihi)");
            cmd.Parameters.AddWithValue("@_AdisyonID", adisyonID);
            cmd.Parameters.AddWithValue("@_Garsonu", siparisiGirenKisi);
            cmd.Parameters.AddWithValue("@_Fiyatı", fiyati);
            cmd.Parameters.AddWithValue("@_Porsiyon", yeniPorsiyonAdetiSiparis);
            cmd.Parameters.AddWithValue("@_YemekAdi", yemekAdi);
            cmd.Parameters.AddWithValue("@_VerilisTarihi", verilisTarihi);

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

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

        private void buttonTamam_Click(object sender, EventArgs e)
        {
            if (buttonNakit.Enabled == false)
            {
                for (int i = menuFormu.listUrunFiyat.Items.Count - 1; i > -1; i--)
                    menuFormu.listUrunFiyat.Items[i].Remove();
            }

            if (Properties.Settings.Default.Server == 2) //server 
            {
                SqlCommand cmd;
                if (buttonNakit.Enabled == false) // eğer herşey ödenmişse siparişlerin ödendimi değerini 1 yap
                {
                    cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET OdendiMi=1 WHERE AdisyonID=(SELECT AdisyonID FROM Adisyon WHERE AcikMi=1 AND MasaAdi='" + masaAdi + "' AND DepartmanAdi='" + departmanAdi + "')");
                    cmd.ExecuteNonQuery();
                }

                // ödeme yapılıyor değerini 0 yap
                cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET OdemeYapiliyor=@odemeYapiliyor WHERE AdisyonID=(SELECT AdisyonID FROM Adisyon WHERE AcikMi=1 AND MasaAdi='" + masaAdi + "' AND DepartmanAdi='" + departmanAdi + "')");

                cmd.Parameters.AddWithValue("@odemeYapiliyor", 0);

                cmd.ExecuteNonQuery();

                cmd.Connection.Close();
                cmd.Connection.Dispose();
            }
            else //client
            {
                menuFormu.masaFormu.hesapFormundanOdemeBitti(masaAdi, departmanAdi, "OdemeBitti", buttonNakit.Enabled); // herşey ödenmişse(buttonNakit.Enabled = false) servera masa daki siparişleri ödendi yapmasını ilet ve ödenmeyapiliyor değerini 0 yaptır
            }
            this.Close();
        }

        private void buttonHesapYazdir_Click(object sender, EventArgs e)
        {

        }

        private void buttonAdisyonYazdir_Click(object sender, EventArgs e)
        {
            // masaya bakan ilk garsonun ismini döndüren sql sorgusu
            //  SELECT TOP 1 Garsonu FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE MasaAdi='S2' AND DepartmanAdi='Departman' AND AcikMi=1 ORDER BY VerilisTarihi ASC
        }

        private void textBoxSecilenlerinTutari_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(((TextBox)sender).Text) < 0)
                ((TextBox)sender).Text = "0,00";
        }

        private void textNumberOfItem_Click(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
    }
}