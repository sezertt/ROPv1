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
using System.Runtime.InteropServices;
using SPIA;
using SPIA.Client;
using SPIA.Server;

namespace ROPv1
{
    public partial class AdisyonGoruntuleme : Form
    {
        public YaziciFormu yaziciForm = null;
        bool hangiTakvimFocuslu = true, ilkAramaEventiReturnEt = true;
        int sonQuery = 0, toplamVeriSayisi = 0;
        string baslangic, bitis;

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

        internal static class NativeMethods
        {
            //capslocku kapatmak için gerekli işlemleri yapıp kapatıyoruz
            [DllImport("user32.dll")]
            internal static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
        }
        static void ToggleCapsLock(bool onOrOff)
        {
            if (IsKeyLocked(Keys.CapsLock) == onOrOff)
                return;
            NativeMethods.keybd_event(0x14, 0x45, 0x1, (UIntPtr)0);
            NativeMethods.keybd_event(0x14, 0x45, 0x1 | 0x2, (UIntPtr)0);
        }

        //sanal klayvemize basıldığında touchscreenkeyboard dll mize basılan key i yolluyoruz
        private void keyboardcontrol1_UserKeyPressed(object sender, KeyboardClassLibrary.KeyboardEventArgs e)
        {
            SendKeys.Send(e.KeyboardKeyPressed);
        }

        private void buttonArttir_Click(object sender, EventArgs e)
        {
            if (hangiTakvimFocuslu)
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
            if (hangiTakvimFocuslu)
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
            hangiTakvimFocuslu = true;
        }

        private void dateBitis_Enter(object sender, EventArgs e)
        {
            hangiTakvimFocuslu = false;
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
            //açılışta capslock açıksa kapatıyoruz.
            ToggleCapsLock(false);

            dateBitis.MaxDate = DateTime.Today;
            dateBaslangic.MaxDate = DateTime.Today;
            comboAdisyonAyar.SelectedIndex = 0;
        }

        private void comboAdisyonAyar_Leave(object sender, EventArgs e)
        {
            ((ComboBox)sender).SelectionLength = 0;
        }

        private void buttonSayfaArttirClick(object sender, EventArgs e)
        {
            if (Convert.ToInt32(labelSayfa.Text) == Convert.ToInt32(labelSayfaSayisi.Text))
                return;
            labelSayfa.Text = (Convert.ToInt32(labelSayfa.Text) + 1).ToString();
        }

        private void buttonSayfaAzalt_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(labelSayfa.Text) < 2)
                return;
            labelSayfa.Text = (Convert.ToInt32(labelSayfa.Text) - 1).ToString();
        }

        private void textboxAdisyonID_Enter(object sender, EventArgs e)
        {
            keyboardcontrol1.Visible = true;
        }

        private void textboxAdisyonID_Leave(object sender, EventArgs e)
        {
            keyboardcontrol1.Visible = false;
        }

        private void buttonAdisyonlariGetir_Click(object sender, EventArgs e)
        {
            listAdisyon.Items.Clear();
            SqlCommand cmd = null;
            SqlDataReader dr = null;

            double adisyonID, adisyonSayisi = 0;
            string adisyonNotu, departmanAdi, masaAdi, tarih, acilisKapanisSaati;
            DateTime acilisZamani;
            DateTime? kapanisZamani = null;
            bool iptalMi;
            decimal adisyonFiyati = 0, toplamFiyat = 0;

            baslangic = dateBaslangic.Value.ToString("yyyy-MM-dd") + " 00:00:00";
            bitis = dateBitis.Value.ToString("yyyy-MM-dd") + " 23:59:59";

            switch (comboAdisyonAyar.Text)
            {
                case "Açık Adisyonlar":
                    sonQuery = 1;
                    cmd = SQLBaglantisi.getCommand("SELECT COUNT(AdisyonID) FROM Adisyon WHERE AcikMi=1");
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    toplamVeriSayisi = dr.GetInt32(0);
                    adisyonSayisi = Convert.ToDouble(toplamVeriSayisi);

                    cmd = SQLBaglantisi.getCommand("SELECT TOP 23 AdisyonID, AdisyonNotu, AcilisZamani, KapanisZamani, DepartmanAdi, MasaAdi, IptalMi FROM Adisyon WHERE AcilisZamani >='" + baslangic + "' AND AcilisZamani <= '" + bitis + "' AND AcikMi=1 ORDER BY AdisyonID DESC");
                    break;

                case "Tüm Adisyonlar":
                    sonQuery = 2;
                    cmd = SQLBaglantisi.getCommand("SELECT COUNT(AdisyonID) FROM Adisyon");
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    toplamVeriSayisi = dr.GetInt32(0);
                    adisyonSayisi = Convert.ToDouble(toplamVeriSayisi);

                    cmd = SQLBaglantisi.getCommand("SELECT TOP 23 AdisyonID, AdisyonNotu, AcilisZamani, KapanisZamani, DepartmanAdi, MasaAdi, IptalMi FROM Adisyon WHERE AcilisZamani >='" + baslangic + "' AND AcilisZamani <= '" + bitis + "' ORDER BY AdisyonID DESC");
                    break;
                case "Adisyon ID":
                    sonQuery = 3;
                    int girilenAdisyonID = 0;
                    try
                    {
                        girilenAdisyonID = Convert.ToInt32(textboxAdisyonID.Text);
                    }
                    catch
                    {
                        KontrolFormu dialog = new KontrolFormu("Hatalı Adisyon ID girdiniz, lütfen tekrar deneyiniz", false);
                        dialog.Show();
                    }
                    cmd = SQLBaglantisi.getCommand("SELECT COUNT(AdisyonID) FROM Adisyon WHERE AdisyonID='" + girilenAdisyonID + "'");
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    toplamVeriSayisi = dr.GetInt32(0);
                    adisyonSayisi = Convert.ToDouble(toplamVeriSayisi);

                    cmd = SQLBaglantisi.getCommand("SELECT TOP 23 AdisyonID, AdisyonNotu, AcilisZamani, KapanisZamani, DepartmanAdi, MasaAdi, IptalMi FROM Adisyon WHERE AcilisZamani >='" + baslangic + "' AND AcilisZamani <= '" + bitis + "' AND AdisyonID LIKE '%" + girilenAdisyonID + "%' ORDER BY AdisyonID DESC");
                    break;
                case "Masa Adı":
                    sonQuery = 4;
                    cmd = SQLBaglantisi.getCommand("SELECT COUNT(AdisyonID) FROM Adisyon WHERE MasaAdi='" + textboxAdisyonID.Text + "'");
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    toplamVeriSayisi = dr.GetInt32(0);
                    adisyonSayisi = Convert.ToDouble(toplamVeriSayisi);

                    cmd = SQLBaglantisi.getCommand("SELECT TOP 23 AdisyonID, AdisyonNotu, AcilisZamani, KapanisZamani, DepartmanAdi, MasaAdi, IptalMi FROM Adisyon WHERE AcilisZamani >='" + baslangic + "' AND AcilisZamani <= '" + bitis + "' AND MasaAdi LIKE '%" + textboxAdisyonID.Text + "%' ORDER BY AdisyonID DESC");
                    break;
                case "Departman Adı":
                    sonQuery = 5;
                    cmd = SQLBaglantisi.getCommand("SELECT COUNT(AdisyonID) FROM Adisyon WHERE DepartmanAdi='" + textboxAdisyonID.Text + "'");
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    toplamVeriSayisi = dr.GetInt32(0);
                    adisyonSayisi = Convert.ToDouble(toplamVeriSayisi);

                    cmd = SQLBaglantisi.getCommand("SELECT TOP 23 AdisyonID, AdisyonNotu, AcilisZamani, KapanisZamani, DepartmanAdi, MasaAdi, IptalMi FROM Adisyon WHERE AcilisZamani >='" + baslangic + "' AND AcilisZamani <= '" + bitis + "' AND DepartmanAdi LIKE '%" + textboxAdisyonID.Text + "%' ORDER BY AdisyonID DESC");
                    break;
                default:
                    sonQuery = 0;
                    break;
            }
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                adisyonFiyati = 0;
                adisyonID = dr.GetInt32(0);
                adisyonNotu = dr.GetString(1);
                acilisZamani = dr.GetDateTime(2);

                if (!dr.IsDBNull(3))
                {
                    kapanisZamani = dr.GetDateTime(3);
                }

                departmanAdi = dr.GetString(4);
                masaAdi = dr.GetString(5);
                iptalMi = dr.GetBoolean(6);

                if (kapanisZamani != null)
                {
                    DateTime temp = kapanisZamani.Value;
                    acilisKapanisSaati = acilisZamani.ToShortTimeString() + " - " + temp.ToShortTimeString();

                }
                else
                {
                    acilisKapanisSaati = acilisZamani.ToShortTimeString();
                }

                tarih = acilisZamani.ToShortDateString();

                SqlCommand cmd2 = SQLBaglantisi.getCommand("SELECT Fiyatı*Porsiyon FROM Siparis WHERE AdisyonID='" + adisyonID + "'");
                SqlDataReader dr2 = cmd2.ExecuteReader();

                while (dr2.Read())
                {
                    adisyonFiyati += dr2.GetDecimal(0);
                }
                cmd2.Connection.Close();
                cmd2.Connection.Dispose();

                toplamFiyat += adisyonFiyati;

                listAdisyon.Items.Add(adisyonID.ToString());
                listAdisyon.Items[listAdisyon.Items.Count - 1].SubItems.Add(departmanAdi);
                listAdisyon.Items[listAdisyon.Items.Count - 1].SubItems.Add(masaAdi);
                listAdisyon.Items[listAdisyon.Items.Count - 1].SubItems.Add(tarih);
                listAdisyon.Items[listAdisyon.Items.Count - 1].SubItems.Add(acilisKapanisSaati);
                listAdisyon.Items[listAdisyon.Items.Count - 1].SubItems.Add(adisyonNotu);
                listAdisyon.Items[listAdisyon.Items.Count - 1].SubItems.Add(adisyonFiyati.ToString("0.00"));

                if (iptalMi)
                {
                    listAdisyon.Items[listAdisyon.Items.Count - 1].BackColor = Color.IndianRed;
                }
            }
            labelSayfaSayisi.Text = Math.Ceiling(adisyonSayisi / 23).ToString();

            ilkAramaEventiReturnEt = true;
            labelSayfa.Text = "1";

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        private void labelSayfa_TextChanged(object sender, EventArgs e)
        {
            if (ilkAramaEventiReturnEt && labelSayfa.Text == "1")
            {
                ilkAramaEventiReturnEt = false;
                return;
            }
            ilkAramaEventiReturnEt = false;

            int IlkAlinacakVerininSirasi, alinacakToplamVeriSayisi = 23;
            IlkAlinacakVerininSirasi = Convert.ToInt32(labelSayfa.Text);

            if (IlkAlinacakVerininSirasi == 1)
            {
                switch (sonQuery)
                {
                    case 0:
                        return;
                    case 1://açık adisyonlar
                        comboAdisyonAyar.Text = "Açık Adisyonlar";

                        break;
                    case 2://tüm adisyonlar
                        comboAdisyonAyar.Text = "Tüm Adisyonlar";

                        break;
                    case 3://adisyonID
                        comboAdisyonAyar.Text = "Adisyon ID";

                        break;
                    case 4://masa adı
                        comboAdisyonAyar.Text = "Masa Adı";

                        break;
                    case 5://departman adı
                        comboAdisyonAyar.Text = "Departman Adı";

                        break;
                    default:
                        return;
                }
                buttonAdisyonlariGetir_Click(null, null);
                return;
            }
            else
            {
                IlkAlinacakVerininSirasi = IlkAlinacakVerininSirasi * 23;
            }

            if (IlkAlinacakVerininSirasi > toplamVeriSayisi)
            {
                alinacakToplamVeriSayisi = toplamVeriSayisi % 23;
                IlkAlinacakVerininSirasi = toplamVeriSayisi;
            }

            SqlCommand cmd = null;
            SqlDataReader dr = null;
            double adisyonID;
            string adisyonNotu, departmanAdi, masaAdi, tarih, acilisKapanisSaati;
            DateTime acilisZamani;
            DateTime? kapanisZamani = null;
            bool iptalMi;
            decimal adisyonFiyati = 0, toplamFiyat = 0;

            switch (sonQuery)
            {
                case 0:
                    return;

                case 1://açık adisyonlar
                    listAdisyon.Items.Clear();

                    cmd = SQLBaglantisi.getCommand("SELECT TOP (@_toplamVeri) * FROM (SELECT TOP (@_ilkAlinacakVeri) AdisyonID, AdisyonNotu, AcilisZamani, KapanisZamani, DepartmanAdi, MasaAdi, IptalMi FROM Adisyon WHERE AcilisZamani >='" + baslangic + "' AND AcilisZamani <= '" + bitis + "' AND AcikMi=1 ORDER BY AdisyonID DESC) AS isim ORDER BY AdisyonID ASC");
                    break;

                case 2://tüm adisyonlar
                    listAdisyon.Items.Clear();

                    cmd = SQLBaglantisi.getCommand("SELECT TOP (@_toplamVeri) * FROM (SELECT TOP (@_ilkAlinacakVeri) AdisyonID, AdisyonNotu, AcilisZamani, KapanisZamani, DepartmanAdi, MasaAdi, IptalMi FROM Adisyon WHERE AcilisZamani >='" + baslangic + "' AND AcilisZamani <= '" + bitis + "' ORDER BY AdisyonID DESC) AS isim ORDER BY AdisyonID ASC");
                    break;

                case 3://adisyonID
                    listAdisyon.Items.Clear();

                    int girilenAdisyonID = 0;
                    try
                    {
                        girilenAdisyonID = Convert.ToInt32(textboxAdisyonID.Text);
                    }
                    catch
                    {
                        KontrolFormu dialog = new KontrolFormu("Hatalı Adisyon ID girdiniz, lütfen tekrar deneyiniz", false);
                        dialog.Show();
                    }

                    cmd = SQLBaglantisi.getCommand("SELECT TOP (@_toplamVeri) * FROM (SELECT TOP (@_ilkAlinacakVeri) AdisyonID, AdisyonNotu, AcilisZamani, KapanisZamani, DepartmanAdi, MasaAdi, IptalMi FROM Adisyon WHERE AcilisZamani >='" + baslangic + "' AND AcilisZamani <= '" + bitis + "' AND AdisyonID LIKE '%" + girilenAdisyonID + "%' ORDER BY AdisyonID DESC) AS isim ORDER BY AdisyonID ASC");
                    break;

                case 4://masa adı
                    listAdisyon.Items.Clear();

                    cmd = SQLBaglantisi.getCommand("SELECT TOP (@_toplamVeri) * FROM (SELECT TOP (@_ilkAlinacakVeri) AdisyonID, AdisyonNotu, AcilisZamani, KapanisZamani, DepartmanAdi, MasaAdi, IptalMi FROM Adisyon WHERE AcilisZamani >='" + baslangic + "' AND AcilisZamani <= '" + bitis + "' AND MasaAdi LIKE '%" + textboxAdisyonID.Text + "%' ORDER BY AdisyonID DESC) AS isim ORDER BY AdisyonID ASC");
                    break;

                case 5://departman adı
                    listAdisyon.Items.Clear();

                    cmd = SQLBaglantisi.getCommand("SELECT TOP (@_toplamVeri) * FROM (SELECT TOP (@_ilkAlinacakVeri) AdisyonID, AdisyonNotu, AcilisZamani, KapanisZamani, DepartmanAdi, MasaAdi, IptalMi FROM Adisyon WHERE AcilisZamani >='" + baslangic + "' AND AcilisZamani <= '" + bitis + "' AND DepartmanAdi LIKE '%" + textboxAdisyonID.Text + "%' ORDER BY AdisyonID DESC) AS isim ORDER BY AdisyonID ASC");
                    break;

                default:
                    return;
            }

            cmd.Parameters.AddWithValue("@_toplamVeri", alinacakToplamVeriSayisi);
            cmd.Parameters.AddWithValue("@_ilkAlinacakVeri", IlkAlinacakVerininSirasi);

            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                adisyonFiyati = 0;
                adisyonID = dr.GetInt32(0);
                adisyonNotu = dr.GetString(1);
                acilisZamani = dr.GetDateTime(2);

                if (!dr.IsDBNull(3))
                {
                    kapanisZamani = dr.GetDateTime(3);
                }

                departmanAdi = dr.GetString(4);
                masaAdi = dr.GetString(5);
                iptalMi = dr.GetBoolean(6);

                if (kapanisZamani != null)
                {
                    DateTime temp = kapanisZamani.Value;
                    acilisKapanisSaati = acilisZamani.ToShortTimeString() + " - " + temp.ToShortTimeString();

                }
                else
                {
                    acilisKapanisSaati = acilisZamani.ToShortTimeString();
                }

                tarih = acilisZamani.ToShortDateString();

                SqlCommand cmd2 = SQLBaglantisi.getCommand("SELECT Fiyatı*Porsiyon FROM Siparis WHERE AdisyonID='" + adisyonID + "'");
                SqlDataReader dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {
                    adisyonFiyati += dr2.GetDecimal(0);
                }
                cmd2.Connection.Close();
                cmd2.Connection.Dispose();

                toplamFiyat += adisyonFiyati;

                listAdisyon.Items.Insert(0, adisyonID.ToString());
                listAdisyon.Items[0].SubItems.Add(departmanAdi);
                listAdisyon.Items[0].SubItems.Add(masaAdi);
                listAdisyon.Items[0].SubItems.Add(tarih);
                listAdisyon.Items[0].SubItems.Add(acilisKapanisSaati);
                listAdisyon.Items[0].SubItems.Add(adisyonNotu);
                listAdisyon.Items[0].SubItems.Add(adisyonFiyati.ToString("0.00"));
                if (iptalMi)
                {
                    listAdisyon.Items[listAdisyon.Items.Count - 1].BackColor = Color.IndianRed;
                }
            }
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        private void listAdisyon_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonYazdir.Enabled = false;

            if (listAdisyon.SelectedItems.Count != 1)
                return;

            listAdisyonDetay.Items.Clear();

            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT Garsonu,Fiyatı,Porsiyon,YemekAdi,IkramMi,IptalMi FROM Siparis WHERE AdisyonID='" + listAdisyon.SelectedItems[0].SubItems[0].Text + "'");
            SqlDataReader dr = cmd.ExecuteReader();

            decimal adisyonHesabi = 0;

            while (dr.Read())
            {
                string yemekAdi = dr.GetString(3);
                bool ikramMi = dr.GetBoolean(4), iptalMi = dr.GetBoolean(5);
                decimal adedi = dr.GetDecimal(2), fiyati = dr.GetDecimal(1);

                listAdisyonDetay.Items.Add(dr.GetString(0));
                listAdisyonDetay.Items[listAdisyonDetay.Items.Count - 1].SubItems.Add(yemekAdi);
                listAdisyonDetay.Items[listAdisyonDetay.Items.Count - 1].SubItems.Add(Convert.ToDouble(adedi).ToString());
                if (ikramMi)
                {
                    listAdisyonDetay.Items[listAdisyonDetay.Items.Count - 1].SubItems.Add("(ikram)");
                }
                else
                {
                    listAdisyonDetay.Items[listAdisyonDetay.Items.Count - 1].SubItems.Add((adedi * fiyati).ToString("0.00"));
                }
                listAdisyonDetay.Items[listAdisyonDetay.Items.Count - 1].Font = new Font("Calibri", 14F, FontStyle.Bold);


                if (iptalMi)
                {
                    listAdisyonDetay.Items[listAdisyonDetay.Items.Count - 1].BackColor = Color.IndianRed;
                }
                else
                {
                    try
                    {
                        adisyonHesabi += Convert.ToDecimal(listAdisyonDetay.Items[listAdisyonDetay.Items.Count - 1].SubItems[3].Text);
                    }
                    catch
                    { }
                }
                buttonYazdir.Enabled = true;
            }
            labelToplamHesap.Text = adisyonHesabi.ToString("0.00");
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        private void buttonYazdir_Click(object sender, EventArgs e)
        {
            if (yaziciForm != null)
            {
                yaziciForm.BringToFront();
                return;
            }

            // yazıcıların içerisinde Adisyon ismi ile başlayan yazıcı var mı diye bak varsa o yazıcıya gönder yoksa 
            // Show(); ile yazıcı seçim formu göster. seçildiğinde seçilen yazıcıya gönder

            List<string[]> adisyonYazicilari = new List<string[]>();
            List<string[]> digerYazicilar = new List<string[]>();

            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT YaziciAdi,FirmaAdi,FirmaAdres,Yazici,Telefon FROM Yazici");
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string[] yazici = new string[5];

                yazici[0] = dr.GetString(0); // yazici adı
                yazici[1] = dr.GetString(1); // firma adı
                yazici[2] = dr.GetString(2); // firma adres
                yazici[3] = dr.GetString(3); // yazıcı windows adı
                yazici[4] = dr.GetString(4); // telefon

                try
                {
                    if (yazici[0].Substring(0, 7) == "Adisyon")
                    {
                        adisyonYazicilari.Add(yazici);
                    }
                    else
                    {
                        digerYazicilar.Add(yazici);
                    }
                }
                catch
                {
                    digerYazicilar.Add(yazici);
                }
            }

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            if (adisyonYazicilari.Count == 1) // tek adisyon yazıcısı var direk gönder
            {
                yazdir(adisyonYazicilari[0]);
            }
            else if (adisyonYazicilari.Count > 1) // 1 den fazla adisyon yazıcısı var hangisinin istendiğini sor
            {
                yaziciForm = new YaziciFormu(this, digerYazicilar, adisyonYazicilari);
                yaziciForm.Show();
            }
            else if (digerYazicilar.Count > 0)// adisyon yazıcısı yok, olan yazıcıları göster
            {
                yaziciForm = new YaziciFormu(this, digerYazicilar);
                yaziciForm.Show();
            }
            else // hata mesajı, lütfen yazıcı yükleyiniz 
            {
                KontrolFormu dialog = new KontrolFormu("Yüklü yazıcı bulunamadı, lütfen yazıcı yükleyin", false);
                dialog.Show();
                return;
            }
        }

        // yazıcı formundan dönen cevap
        public void yazdir(string[] yaziciBilgileri)
        {
            //yazdırmadan önce sayfaya göre list viewı ayarlıyoruz
            listAdisyonDetay.Items.Add("");
            listAdisyonDetay.Items[listAdisyonDetay.Items.Count - 1].SubItems.Add("Toplam");
            listAdisyonDetay.Items[listAdisyonDetay.Items.Count - 1].SubItems.Add(":");
            listAdisyonDetay.Items[listAdisyonDetay.Items.Count - 1].SubItems.Add(labelToplamHesap.Text);

            listAdisyonDetay.Font = new Font("Calibri", 10F, FontStyle.Bold);
            for(int i=0; i< listAdisyonDetay.Items.Count;i++)
            {
                listAdisyonDetay.Items[i].Font = new Font("Calibri", 10F, FontStyle.Bold);
            }
            listAdisyonDetay.Columns[2].Text = "Ad.";
            listAdisyonDetay.Columns[0].Width = 90;
            listAdisyonDetay.Columns[1].Width = 110;
            listAdisyonDetay.Columns[2].Width = 30;
            listAdisyonDetay.Columns[3].Width = 55;

            // yazdırıyoruz
            listViewPrinter printer = new listViewPrinter(listAdisyonDetay, new Point(13, 10), false, false, listAdisyon.SelectedItems[0].SubItems[1].Text + " " + listAdisyon.SelectedItems[0].SubItems[2].Text + " Adisyon Dökümü(" + listAdisyon.SelectedItems[0].SubItems[0].Text + ")\n" + listAdisyon.SelectedItems[0].SubItems[3].Text + " / " + listAdisyon.SelectedItems[0].SubItems[4].Text);
            printer.print(yaziciBilgileri[3]);

            //listviewı eski haline getiriyoruz
            listAdisyonDetay.Font = new Font("Calibri", 14F, FontStyle.Bold);
            for (int i = 0; i < listAdisyonDetay.Items.Count; i++)
            {
                listAdisyonDetay.Items[i].Font = new Font("Calibri", 14F, FontStyle.Bold);
            }
            listAdisyonDetay.Columns[2].Text = "Adet";
            listAdisyonDetay.Columns[0].Width = 109;
            listAdisyonDetay.Columns[1].Width = 163;
            listAdisyonDetay.Columns[2].Width = 53;
            listAdisyonDetay.Columns[3].Width = 91;

            listAdisyonDetay.Items.RemoveAt(listAdisyonDetay.Items.Count - 1);
        }
    }
}