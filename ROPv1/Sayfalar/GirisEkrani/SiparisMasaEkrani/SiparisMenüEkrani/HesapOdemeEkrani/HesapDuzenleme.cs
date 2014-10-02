using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ROPv1
{
    public partial class HesapDuzenleme : Form
    {
        HesapFormu hesapForm;

        AdisyonGoruntuleme adisyonGoruntulemeForm;

        decimal[] eskiOdemeler, yeniOdemeler = { 0, 0, 0 };

        public List<int> miktarlar = new List<int>();

        bool iptal = false;

        string masaAdi, departmanAdi, siparisiGirenKisi, gelenAdisyonID;

        public HesapDuzenleme(decimal[] eskiOdemeler, string masaAdi, string departmanAdi, HesapFormu hesapForm, string siparisiGirenKisi)
        {
            InitializeComponent();

            this.siparisiGirenKisi = siparisiGirenKisi;
            this.eskiOdemeler = eskiOdemeler;
            this.hesapForm = hesapForm;
            this.masaAdi = masaAdi;
            this.departmanAdi = departmanAdi;

            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
        }

        public HesapDuzenleme(decimal[] eskiOdemeler, string masaAdi, string departmanAdi, AdisyonGoruntuleme adisyonGoruntulemeForm, string siparisiGirenKisi, string gelenAdisyonID)
        {
            InitializeComponent();

            this.siparisiGirenKisi = siparisiGirenKisi;
            this.eskiOdemeler = eskiOdemeler;
            this.adisyonGoruntulemeForm = adisyonGoruntulemeForm;
            this.masaAdi = masaAdi;
            this.departmanAdi = departmanAdi;
            this.gelenAdisyonID = gelenAdisyonID;

            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
        }

        private void pinboardcontrol21_UserKeyPressed(object sender, PinboardClassLibrary.PinboardEventArgs e)
        {
            SendKeys.Send(e.KeyboardKeyPressed);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            yeniOdemeler[0] = Convert.ToDecimal(textBoxNakit.Text);
            yeniOdemeler[1] = Convert.ToDecimal(textBoxKart.Text);
            yeniOdemeler[2] = Convert.ToDecimal(textBoxFis.Text);

            if (yeniOdemeler[0] + yeniOdemeler[1] + yeniOdemeler[2] != eskiOdemeler[0] + eskiOdemeler[1] + eskiOdemeler[2])
            {
                KontrolFormu dialog = new KontrolFormu("Girilen ödeme bilgileri toplamı hesaba eşit olmak zorunda, lütfen kontrol edip tekrar deneyiniz", false);
                dialog.Show();
                iptal = true;
                return;
            }

            if (gelenAdisyonID != null)
            {
                decimal odenenMiktar = 0;

                for (int i = 0; i < eskiOdemeler.Length; i++)
                {
                    if (eskiOdemeler[i] != yeniOdemeler[i]) // değişen ödemeleri güncelle
                    {
                        odenenMiktar = yeniOdemeler[i];
                        int odemeTipi = 101 + i;

                        SqlCommand cmd = SQLBaglantisi.getCommand("IF EXISTS (SELECT * FROM OdemeDetay WHERE AdisyonID=@_AdisyonID1 AND OdemeTipi=@_OdemeTipi1) UPDATE OdemeDetay SET OdenenMiktar=@_OdenenMiktar1, IndirimiKimGirdi=@_IndirimiKimGirdi1 WHERE OdemeTipi=@_OdemeTipi2 AND AdisyonID=@_AdisyonID2 ELSE INSERT INTO OdemeDetay(AdisyonID,OdemeTipi,OdenenMiktar,IndirimiKimGirdi) VALUES(@_AdisyonID3,@_OdemeTipi3,@_OdenenMiktar2,@_IndirimiKimGirdi2)");

                        cmd.Parameters.AddWithValue("@_AdisyonID1", gelenAdisyonID);
                        cmd.Parameters.AddWithValue("@_OdemeTipi1", odemeTipi);
                        cmd.Parameters.AddWithValue("@_OdenenMiktar1", odenenMiktar);
                        cmd.Parameters.AddWithValue("@_IndirimiKimGirdi1", siparisiGirenKisi);

                        cmd.Parameters.AddWithValue("@_OdemeTipi2", odemeTipi);
                        cmd.Parameters.AddWithValue("@_AdisyonID2", gelenAdisyonID);

                        cmd.Parameters.AddWithValue("@_AdisyonID3", gelenAdisyonID);
                        cmd.Parameters.AddWithValue("@_OdemeTipi3", odemeTipi);
                        cmd.Parameters.AddWithValue("@_OdenenMiktar2", odenenMiktar);
                        cmd.Parameters.AddWithValue("@_IndirimiKimGirdi2", siparisiGirenKisi);
                        cmd.ExecuteNonQuery();

                        cmd.Connection.Close();
                        cmd.Connection.Dispose();
                    }
                }
            }
            else
            {
                if (Properties.Settings.Default.Server == 2) // server
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

                    decimal odenenMiktar = 0;

                    for (int i = 0; i < eskiOdemeler.Length; i++)
                    {
                        if (eskiOdemeler[i] != yeniOdemeler[i]) // değişen ödemeleri güncelle
                        {
                            odenenMiktar = yeniOdemeler[i];
                            int odemeTipi = 101 + i;

                            cmd = SQLBaglantisi.getCommand("IF EXISTS (SELECT * FROM OdemeDetay WHERE AdisyonID=@_AdisyonID1 AND OdemeTipi=@_OdemeTipi1) UPDATE OdemeDetay SET OdenenMiktar=@_OdenenMiktar1, IndirimiKimGirdi=@_IndirimiKimGirdi1 WHERE OdemeTipi=@_OdemeTipi2 AND AdisyonID=@_AdisyonID2 ELSE INSERT INTO OdemeDetay(AdisyonID,OdemeTipi,OdenenMiktar,IndirimiKimGirdi) VALUES(@_AdisyonID3,@_OdemeTipi3,@_OdenenMiktar2,@_IndirimiKimGirdi2)");

                            cmd.Parameters.AddWithValue("@_AdisyonID1", adisyonID);
                            cmd.Parameters.AddWithValue("@_OdemeTipi1", odemeTipi);
                            cmd.Parameters.AddWithValue("@_OdenenMiktar1", odenenMiktar);
                            cmd.Parameters.AddWithValue("@_IndirimiKimGirdi1", siparisiGirenKisi);

                            cmd.Parameters.AddWithValue("@_OdemeTipi2", odemeTipi);
                            cmd.Parameters.AddWithValue("@_AdisyonID2", adisyonID);

                            cmd.Parameters.AddWithValue("@_AdisyonID3", adisyonID);
                            cmd.Parameters.AddWithValue("@_OdemeTipi3", odemeTipi);
                            cmd.Parameters.AddWithValue("@_OdenenMiktar2", odenenMiktar);
                            cmd.Parameters.AddWithValue("@_IndirimiKimGirdi2", siparisiGirenKisi);
                            cmd.ExecuteNonQuery();

                            cmd.Connection.Close();
                            cmd.Connection.Dispose();
                        }
                    }
                    hesapForm.menuFormu.masaFormu.tumKullanicilaraMesajYolla("komut=OdemeIndirimOnayTablet&masaAdi=" + masaAdi + "&departmanAdi=" + departmanAdi);
                }
                else
                {
                    //servera ödeme değişimini ilet
                    hesapForm.menuFormu.masaFormu.hesapFormundanOdemeGuncelle(masaAdi, departmanAdi, "OdemeGuncelle", eskiOdemeler, yeniOdemeler, siparisiGirenKisi);
                    iptal = true;
                }
            }
            this.Close();
        }

        private void buttonNO_Click(object sender, EventArgs e)
        {
            iptal = true;
            this.Close();
        }

        private void textBoxUrun5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 22)
                e.Handled = true;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }
            else if (e.KeyChar == ',' && (sender as TextBox).SelectedText.Length == (sender as TextBox).Text.Length)
            {
                e.Handled = false;
            }
            else if (e.KeyChar == ',' && (sender as TextBox).Text.IndexOf(',') > -1)
            { // 1 kere , kullanmasına izin ver
                e.Handled = true;
            }
        }

        private void textBoxUrun1_Click(object sender, EventArgs e)
        {
            (sender as TextBox).Select(0, (sender as TextBox).TextLength);
        }

        private void HesapDuzenleme_Load(object sender, EventArgs e)
        {
            labelNakit.Text = eskiOdemeler[0].ToString("0.00");
            labelKart.Text = eskiOdemeler[1].ToString("0.00");
            labelFis.Text = eskiOdemeler[2].ToString("0.00");

            textBoxNakit.Text = eskiOdemeler[0].ToString("0.00");
            textBoxKart.Text = eskiOdemeler[1].ToString("0.00");
            textBoxFis.Text = eskiOdemeler[2].ToString("0.00");
            labelDepartmanAdi.Text = departmanAdi;
            labelMasaAdi.Text = masaAdi;
        }

        private void HesapDuzenleme_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!iptal && hesapForm != null)
            {
                hesapForm.odemeGuncellemeGeldi(eskiOdemeler, yeniOdemeler, siparisiGirenKisi);
            }
            else if (!iptal && adisyonGoruntulemeForm != null)
            {
                adisyonGoruntulemeForm.odemeGuncellemeGeldi(eskiOdemeler, yeniOdemeler, siparisiGirenKisi);
            }
        }

        private void textBoxNakit_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text == ",")
            {
                (sender as TextBox).Text = "0,";
                (sender as TextBox).SelectionStart = (sender as TextBox).Text.Length;
            }

            decimal eskiNakit, eskiFis, eskiKart, yeniFis, yeniNakit, yeniKart;

            if (labelNakit.Text != "")
                eskiNakit = Convert.ToDecimal(labelNakit.Text);
            else
                eskiNakit = 0;

            if (labelKart.Text != "")
                eskiKart = Convert.ToDecimal(labelKart.Text);
            else
                eskiKart = 0;

            if (labelFis.Text != "")
                eskiFis = Convert.ToDecimal(labelFis.Text);
            else
                eskiFis = 0;

            if (textBoxNakit.Text != "")
                yeniNakit = Convert.ToDecimal(textBoxNakit.Text);
            else
                yeniNakit = 0;

            if (textBoxFis.Text != "")
                yeniFis = Convert.ToDecimal(textBoxFis.Text);
            else
                yeniFis = 0;

            if (textBoxKart.Text != "")
                yeniKart = Convert.ToDecimal(textBoxKart.Text);
            else
                yeniKart = 0;

            labelDegisim.Text = ((yeniFis + yeniNakit + yeniKart) - (eskiNakit + eskiFis + eskiKart)).ToString("0.00");
        }
    }
}
