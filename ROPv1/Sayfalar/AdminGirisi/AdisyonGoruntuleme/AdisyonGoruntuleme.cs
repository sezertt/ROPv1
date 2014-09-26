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
        bool hangiTakvimFocuslu = true, ilkAramaEventiReturnEt = true, adisyonDegistirebilirMi = false;
        int sonQuery = 0, toplamVeriSayisi = 0;
        string baslangic, bitis, siparisiGirenKisi;

        public AdisyonGoruntuleme(string siparisiGirenKisi, bool adisyonDegistirebilirMi)
        {
            this.siparisiGirenKisi = siparisiGirenKisi;
            this.adisyonDegistirebilirMi = adisyonDegistirebilirMi;
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

            if (!adisyonDegistirebilirMi)
            {
                buttonHesapDuzenle.Enabled = false;
            }                    
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
            DateTime kapanisZamani;
            bool iptalMi;
            decimal adisyonFiyati = 0, toplamFiyat = 0;

            baslangic = dateBaslangic.Value.ToString("yyyy-MM-dd") + " 00:00:00";
            bitis = dateBitis.Value.ToString("yyyy-MM-dd") + " 23:59:59";

            DateTime testDate = new DateTime();
            testDate = DateTime.Parse("2001-01-01 00:00:00");

            string testDateString = testDate.ToString("yyyy-MM-dd") + " 00:00:00";

            switch (comboAdisyonAyar.Text)
            {
                case "Açık Adisyonlar":
                    sonQuery = 1;
                    cmd = SQLBaglantisi.getCommand("SELECT COUNT(AdisyonID) FROM Adisyon WHERE AcikMi=1");
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    toplamVeriSayisi = dr.GetInt32(0);
                    adisyonSayisi = Convert.ToDouble(toplamVeriSayisi);

                    cmd = SQLBaglantisi.getCommand("SELECT TOP 23 AdisyonID, AdisyonNotu, AcilisZamani, KapanisZamani, DepartmanAdi, MasaAdi, IptalMi FROM Adisyon WHERE AcikMi=1 ORDER BY AdisyonID DESC");
                    break;

                case "Tüm Adisyonlar":
                    sonQuery = 2;
                    cmd = SQLBaglantisi.getCommand("SELECT COUNT(AdisyonID) FROM Adisyon WHERE (KapanisZamani >='" + baslangic + "' AND KapanisZamani <= '" + bitis + "') OR KapanisZamani<'" + testDate + "'");
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    toplamVeriSayisi = dr.GetInt32(0);
                    adisyonSayisi = Convert.ToDouble(toplamVeriSayisi);

                    cmd = SQLBaglantisi.getCommand("SELECT TOP 23 AdisyonID, AdisyonNotu, AcilisZamani, KapanisZamani, DepartmanAdi, MasaAdi, IptalMi FROM Adisyon WHERE (KapanisZamani >='" + baslangic + "' AND KapanisZamani <= '" + bitis + "') OR KapanisZamani<'" + testDate + "' ORDER BY AdisyonID DESC");
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
                    cmd = SQLBaglantisi.getCommand("SELECT COUNT(AdisyonID) FROM Adisyon WHERE AdisyonID='" + girilenAdisyonID + "' AND KapanisZamani >='" + baslangic + "' AND KapanisZamani <= '" + bitis + "'");
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    toplamVeriSayisi = dr.GetInt32(0);
                    adisyonSayisi = Convert.ToDouble(toplamVeriSayisi);

                    cmd = SQLBaglantisi.getCommand("SELECT TOP 23 AdisyonID, AdisyonNotu, AcilisZamani, KapanisZamani, DepartmanAdi, MasaAdi, IptalMi FROM Adisyon WHERE KapanisZamani >='" + baslangic + "' AND KapanisZamani <= '" + bitis + "' AND AdisyonID LIKE '%" + girilenAdisyonID + "%' ORDER BY AdisyonID DESC");
                    break;
                case "Masa Adı":
                    sonQuery = 4;
                    cmd = SQLBaglantisi.getCommand("SELECT COUNT(AdisyonID) FROM Adisyon WHERE MasaAdi='" + textboxAdisyonID.Text + "' AND KapanisZamani >='" + baslangic + "' AND KapanisZamani <= '" + bitis + "'");
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    toplamVeriSayisi = dr.GetInt32(0);
                    adisyonSayisi = Convert.ToDouble(toplamVeriSayisi);

                    cmd = SQLBaglantisi.getCommand("SELECT TOP 23 AdisyonID, AdisyonNotu, AcilisZamani, KapanisZamani, DepartmanAdi, MasaAdi, IptalMi FROM Adisyon WHERE KapanisZamani >='" + baslangic + "' AND KapanisZamani <= '" + bitis + "' AND MasaAdi LIKE '%" + textboxAdisyonID.Text + "%' ORDER BY AdisyonID DESC");
                    break;
                case "Departman Adı":
                    sonQuery = 5;
                    cmd = SQLBaglantisi.getCommand("SELECT COUNT(AdisyonID) FROM Adisyon WHERE DepartmanAdi='" + textboxAdisyonID.Text + "' AND KapanisZamani >='" + baslangic + "' AND KapanisZamani <= '" + bitis + "'");
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    toplamVeriSayisi = dr.GetInt32(0);
                    adisyonSayisi = Convert.ToDouble(toplamVeriSayisi);

                    cmd = SQLBaglantisi.getCommand("SELECT TOP 23 AdisyonID, AdisyonNotu, AcilisZamani, KapanisZamani, DepartmanAdi, MasaAdi, IptalMi FROM Adisyon WHERE KapanisZamani >='" + baslangic + "' AND KapanisZamani <= '" + bitis + "' AND DepartmanAdi LIKE '%" + textboxAdisyonID.Text + "%' ORDER BY AdisyonID DESC");
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

                kapanisZamani = dr.GetDateTime(3);

                departmanAdi = dr.GetString(4);
                masaAdi = dr.GetString(5);
                iptalMi = dr.GetBoolean(6);

                if (kapanisZamani.Date > testDate.Date)
                {
                    DateTime temp = kapanisZamani;
                    acilisKapanisSaati = acilisZamani.ToShortTimeString() + " - " + temp.ToShortTimeString();
                }
                else
                {
                    acilisKapanisSaati = acilisZamani.ToShortTimeString();
                }

                tarih = acilisZamani.ToShortDateString();

                SqlCommand cmd2 = SQLBaglantisi.getCommand("SELECT Fiyatı*Adet FROM Siparis WHERE AdisyonID='" + adisyonID + "'");
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
                    listAdisyon.Items[listAdisyon.Items.Count - 1].ForeColor = Color.White;
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
            DateTime kapanisZamani;
            bool iptalMi;
            decimal adisyonFiyati = 0, toplamFiyat = 0;

            switch (sonQuery)
            {
                case 0:
                    return;

                case 1://açık adisyonlar
                    listAdisyon.Items.Clear();

                    cmd = SQLBaglantisi.getCommand("SELECT TOP (@_toplamVeri) * FROM (SELECT TOP (@_ilkAlinacakVeri) AdisyonID, AdisyonNotu, AcilisZamani, KapanisZamani, DepartmanAdi, MasaAdi, IptalMi FROM Adisyon WHERE KapanisZamani >='" + baslangic + "' AND KapanisZamani <= '" + bitis + "' AND AcikMi=1 ORDER BY AdisyonID DESC) AS isim ORDER BY AdisyonID ASC");
                    break;

                case 2://tüm adisyonlar
                    listAdisyon.Items.Clear();

                    cmd = SQLBaglantisi.getCommand("SELECT TOP (@_toplamVeri) * FROM (SELECT TOP (@_ilkAlinacakVeri) AdisyonID, AdisyonNotu, AcilisZamani, KapanisZamani, DepartmanAdi, MasaAdi, IptalMi FROM Adisyon WHERE KapanisZamani >='" + baslangic + "' AND KapanisZamani <= '" + bitis + "' ORDER BY AdisyonID DESC) AS isim ORDER BY AdisyonID ASC");
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

                    cmd = SQLBaglantisi.getCommand("SELECT TOP (@_toplamVeri) * FROM (SELECT TOP (@_ilkAlinacakVeri) AdisyonID, AdisyonNotu, AcilisZamani, KapanisZamani, DepartmanAdi, MasaAdi, IptalMi FROM Adisyon WHERE KapanisZamani >='" + baslangic + "' AND KapanisZamani <= '" + bitis + "' AND AdisyonID LIKE '%" + girilenAdisyonID + "%' ORDER BY AdisyonID DESC) AS isim ORDER BY AdisyonID ASC");
                    break;

                case 4://masa adı
                    listAdisyon.Items.Clear();

                    cmd = SQLBaglantisi.getCommand("SELECT TOP (@_toplamVeri) * FROM (SELECT TOP (@_ilkAlinacakVeri) AdisyonID, AdisyonNotu, AcilisZamani, KapanisZamani, DepartmanAdi, MasaAdi, IptalMi FROM Adisyon WHERE KapanisZamani >='" + baslangic + "' AND KapanisZamani <= '" + bitis + "' AND MasaAdi LIKE '%" + textboxAdisyonID.Text + "%' ORDER BY AdisyonID DESC) AS isim ORDER BY AdisyonID ASC");
                    break;

                case 5://departman adı
                    listAdisyon.Items.Clear();

                    cmd = SQLBaglantisi.getCommand("SELECT TOP (@_toplamVeri) * FROM (SELECT TOP (@_ilkAlinacakVeri) AdisyonID, AdisyonNotu, AcilisZamani, KapanisZamani, DepartmanAdi, MasaAdi, IptalMi FROM Adisyon WHERE KapanisZamani >='" + baslangic + "' AND KapanisZamani <= '" + bitis + "' AND DepartmanAdi LIKE '%" + textboxAdisyonID.Text + "%' ORDER BY AdisyonID DESC) AS isim ORDER BY AdisyonID ASC");
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

                kapanisZamani = dr.GetDateTime(3);

                departmanAdi = dr.GetString(4);
                masaAdi = dr.GetString(5);
                iptalMi = dr.GetBoolean(6);

                DateTime testDate = new DateTime();
                testDate = DateTime.Parse("2001-01-01 00:00:00");

                if (kapanisZamani.Date > testDate.Date)
                {
                    DateTime temp = kapanisZamani;
                    acilisKapanisSaati = acilisZamani.ToShortTimeString() + " - " + temp.ToShortTimeString();

                }
                else
                {
                    acilisKapanisSaati = acilisZamani.ToShortTimeString();
                }

                tarih = acilisZamani.ToShortDateString();

                SqlCommand cmd2 = SQLBaglantisi.getCommand("SELECT Fiyatı*Adet FROM Siparis WHERE AdisyonID='" + adisyonID + "'");
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
                    listAdisyon.Items[listAdisyon.Items.Count - 1].ForeColor = Color.White;
                }
            }
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        private void listAdisyon_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonYazdir.Enabled = false;
            buttonHesapDuzenle.Enabled = false;

            if (listAdisyon.SelectedItems.Count != 1)
                return;

            listAdisyonDetay.Items.Clear();

            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT Garsonu,Fiyatı,Adet,YemekAdi,IkramMi,IptalMi,Porsiyon,VerilisTarihi FROM Siparis WHERE AdisyonID='" + listAdisyon.SelectedItems[0].SubItems[0].Text + "'");
            SqlDataReader dr = cmd.ExecuteReader();

            decimal adisyonHesabi = 0;

            while (dr.Read())
            {
                string yemekAdi = dr.GetString(3);
                bool ikramMi = dr.GetBoolean(4), iptalMi = dr.GetBoolean(5);
                decimal fiyati = dr.GetDecimal(1);
                int adedi = dr.GetInt32(2);
                decimal porsiyonu = dr.GetDecimal(6);
                DateTime verilisTarihi = dr.GetDateTime(7);

                GlacialComponents.Controls.GLItem glitem = new GlacialComponents.Controls.GLItem();

                glitem.Text = dr.GetString(0);

                glitem.SubItems[1].Text = yemekAdi;
                glitem.SubItems[2].Text = adedi.ToString();
                glitem.SubItems[3].Text = Convert.ToDouble(porsiyonu).ToString();
                glitem.SubItems[4].Text = verilisTarihi.ToShortTimeString();

                if (ikramMi)
                {
                    glitem.SubItems[5].Text = "(ikram)";
                }
                else
                {
                    glitem.SubItems[5].Text = (adedi * fiyati).ToString("0.00");
                }


                glitem.SubItems[0].ForeColor = SystemColors.ActiveCaption;
                glitem.SubItems[1].ForeColor = SystemColors.ActiveCaption;
                glitem.SubItems[2].ForeColor = SystemColors.ActiveCaption;
                glitem.SubItems[3].ForeColor = SystemColors.ActiveCaption;
                glitem.SubItems[4].ForeColor = SystemColors.ActiveCaption;
                glitem.SubItems[5].ForeColor = SystemColors.ActiveCaption;

                glitem.ForeColor = SystemColors.ActiveCaption;

                glitem.SubItems[0].ForceText = true;
                glitem.SubItems[1].ForceText = true;
                glitem.SubItems[2].ForceText = true;
                glitem.SubItems[3].ForceText = true;
                glitem.SubItems[4].ForceText = true;
                glitem.SubItems[5].ForceText = true;


                listAdisyonDetay.Items.Add(glitem);

                if (iptalMi)
                {
                    glitem.BackColor = Color.IndianRed;
                    glitem.ForeColor = Color.White;

                }
                else
                {
                    try
                    {
                        adisyonHesabi += Convert.ToDecimal(glitem.SubItems[5].Text);
                    }
                    catch
                    { }
                }
  
                buttonYazdir.Enabled = true;
                if (adisyonDegistirebilirMi)
                {
                    buttonHesapDuzenle.Enabled = true;
                }   
            }

            labelToplamHesap.Text = adisyonHesabi.ToString("0.00");

            cmd = SQLBaglantisi.getCommand("SELECT OdemeTipi, OdenenMiktar, IndirimiKimGirdi from OdemeDetay JOIN Adisyon ON OdemeDetay.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AdisyonID=@_AdisyonID");

            cmd.Parameters.AddWithValue("@_AdisyonID", Convert.ToInt32(listAdisyon.SelectedItems[0].SubItems[0].Text));

            dr = cmd.ExecuteReader();

            listViewOdemeler.Items[0].SubItems[2].Text = "0,00";
            listViewOdemeler.Items[1].SubItems[2].Text = "0,00";
            listViewOdemeler.Items[2].SubItems[2].Text = "0,00";
            listViewOdemeler.Items[3].SubItems[2].Text = "0,00";
            listViewOdemeler.Items[4].SubItems[2].Text = "0,00";
            listViewOdemeler.Items[5].SubItems[2].Text = "0,00";

            listViewOdemeler.Items[0].SubItems[1].Text = "---";
            listViewOdemeler.Items[1].SubItems[1].Text = "---";
            listViewOdemeler.Items[2].SubItems[1].Text = "---";
            listViewOdemeler.Items[3].SubItems[1].Text = "---";
            listViewOdemeler.Items[4].SubItems[1].Text = "---";

            while (dr.Read())
            {
                int odemeTipi;
                decimal odenenMiktar;
                string odemeyiAlanKisi;

                try
                {
                    odemeTipi = dr.GetInt32(0);
                    odenenMiktar = dr.GetDecimal(1);
                    odemeyiAlanKisi = dr.GetString(2);
                }
                catch
                {
                    KontrolFormu dialog = new KontrolFormu("Ödeme bilgileri alınırken hata oluştu, lütfen tekrar giriş yapınız", false);
                    dialog.Show();
                    return;
                }

                if (odemeTipi == 101) // nakit
                {
                    listViewOdemeler.Items[0].SubItems[2].Text = odenenMiktar.ToString("0.00");
                    listViewOdemeler.Items[0].SubItems[1].Text = odemeyiAlanKisi;
                }
                else if (odemeTipi == 102) // kredi kartı
                {
                    listViewOdemeler.Items[1].SubItems[2].Text = odenenMiktar.ToString("0.00");
                    listViewOdemeler.Items[1].SubItems[1].Text = odemeyiAlanKisi;
                }
                else if (odemeTipi == 103)// yemek fişi
                {
                    listViewOdemeler.Items[2].SubItems[2].Text = odenenMiktar.ToString("0.00");
                    listViewOdemeler.Items[2].SubItems[1].Text = odemeyiAlanKisi;
                }
                else if (odemeTipi == 104)// indirim TL
                {
                    listViewOdemeler.Items[3].SubItems[2].Text = odenenMiktar.ToString("0.00");
                    listViewOdemeler.Items[3].SubItems[1].Text = odemeyiAlanKisi;
                }
                else // indirim Yüzde
                {
                    listViewOdemeler.Items[4].SubItems[2].Text = odenenMiktar.ToString("0.00");
                    listViewOdemeler.Items[4].SubItems[1].Text = odemeyiAlanKisi;
                }
                listViewOdemeler.Items[5].SubItems[2].Text = (Convert.ToDecimal(listViewOdemeler.Items[5].SubItems[2].Text) + odenenMiktar).ToString("0.00");
            }
            cmd.Connection.Close();
            cmd.Connection.Dispose();
            listAdisyonDetay.Refresh();
            listAdisyonDetay.Items[0].Selected = true;
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
            GlacialComponents.Controls.GLItem glitem = new GlacialComponents.Controls.GLItem();

            glitem.Text = "TOPLAM";

            glitem.SubItems[1].Text = "";
            glitem.SubItems[2].Text = "";
            glitem.SubItems[3].Text = "";
            glitem.SubItems[4].Text = "";
            glitem.SubItems[5].Text = labelToplamHesap.Text;

            listAdisyonDetay.Items.Add(glitem);

            // yazdırıyoruz
            listViewPrinter printer = new listViewPrinter(listAdisyonDetay, new Point(13, 10), false, false, listAdisyon.SelectedItems[0].SubItems[1].Text + " " + listAdisyon.SelectedItems[0].SubItems[2].Text + " Adisyon Dökümü(" + listAdisyon.SelectedItems[0].SubItems[0].Text + ")\n" + listAdisyon.SelectedItems[0].SubItems[3].Text + " / " + listAdisyon.SelectedItems[0].SubItems[4].Text);
            printer.print(yaziciBilgileri[3]);

            listAdisyonDetay.Items.RemoveAt(listAdisyonDetay.Items.Count - 1);
        }

        private void buttonHesapDuzenle_Click(object sender, EventArgs e)
        {
            decimal[] odemeler = new decimal[3];
            odemeler[0] = Convert.ToDecimal(listViewOdemeler.Items[0].SubItems[2].Text);
            odemeler[1] = Convert.ToDecimal(listViewOdemeler.Items[1].SubItems[2].Text);
            odemeler[2] = Convert.ToDecimal(listViewOdemeler.Items[2].SubItems[2].Text);

            //Hesap Formu Aç Direk Ödenen Miktarlar Değiştirilebilsin
            HesapDuzenleme hesapDuzenlemeFormu = new HesapDuzenleme(odemeler, listAdisyon.SelectedItems[0].SubItems[2].Text, listAdisyon.SelectedItems[0].SubItems[1].Text, this, siparisiGirenKisi, listAdisyon.SelectedItems[0].SubItems[0].Text);
            hesapDuzenlemeFormu.ShowDialog();
        }

        public void odemeGuncellemeGeldi(decimal[] odemeler, decimal[] gelenOdemeler, string siparisiGirenKisi)
        {
            if (odemeler[0] != gelenOdemeler[0])
            {
                listViewOdemeler.Items[0].SubItems[1].Text = siparisiGirenKisi;
                listViewOdemeler.Items[0].SubItems[2].Text = (gelenOdemeler[0]).ToString("0.00");
                listViewOdemeler.Items[5].SubItems[2].Text = (Convert.ToDecimal(listViewOdemeler.Items[5].SubItems[2].Text) - odemeler[0] + gelenOdemeler[0]).ToString("0.00");
            }

            if (odemeler[1] != gelenOdemeler[1])
            {
                listViewOdemeler.Items[1].SubItems[1].Text = siparisiGirenKisi;
                listViewOdemeler.Items[1].SubItems[2].Text = (gelenOdemeler[1]).ToString("0.00");
                listViewOdemeler.Items[5].SubItems[2].Text = (Convert.ToDecimal(listViewOdemeler.Items[5].SubItems[2].Text) - odemeler[1] + gelenOdemeler[1]).ToString("0.00");
            }

            if (odemeler[2] != gelenOdemeler[2])
            {
                listViewOdemeler.Items[2].SubItems[1].Text = siparisiGirenKisi;
                listViewOdemeler.Items[2].SubItems[2].Text = (gelenOdemeler[2]).ToString("0.00");
                listViewOdemeler.Items[5].SubItems[2].Text = (Convert.ToDecimal(listViewOdemeler.Items[5].SubItems[2].Text) - odemeler[2] + gelenOdemeler[2]).ToString("0.00");
            }
        }
    }
}