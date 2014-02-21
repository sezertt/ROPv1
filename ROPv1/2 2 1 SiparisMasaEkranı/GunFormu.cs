using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.IO;

namespace ROPv1
{
    public partial class GunFormu : Form
    {
        List<GunBilgileri> gunListesi = new List<GunBilgileri>();
        string ayarYapanKisi;
        public GunFormu(string GunBasiveyaSonuYapanKisi)
        {
            InitializeComponent();

            this.BringToFront();

            labelSaat.Text = DateTime.Now.ToString("HH:mm:ss", new CultureInfo("tr-TR"));
            labelGun.Text = DateTime.Now.ToString("dddd", new CultureInfo("tr-TR"));
            labelTarih.Text = DateTime.Now.Date.ToString("d MMMM yyyy", new CultureInfo("tr-TR"));
            timerSaat.Start();

            ayarYapanKisi = GunBasiveyaSonuYapanKisi;

            if (!File.Exists("gunler.xml")) // ilk açılışta veya bir sıkıntı sonucu kategoriler dosyası silinirse kendi default menümüzü giriyoruz.
            {
                buttonGunSonu.Enabled = false;
            }
            else
            {
                // Oluşturulmuş menüleri xml den okuyoruz
                XmlLoad<GunBilgileri> loadInfo = new XmlLoad<GunBilgileri>();
                GunBilgileri[] infoGunler = loadInfo.LoadRestoran("gunler.xml");

                gunListesi.AddRange(infoGunler);

                if (gunListesi[gunListesi.Count - 1].gunSonuYapanKisi == null && gunListesi[gunListesi.Count - 1].gunBasiYapanKisi != null)
                {
                    buttonGunSonu.Enabled = true;
                    buttonGunBasi.Enabled = false;
                }
                else
                {
                    buttonGunSonu.Enabled = false;
                    buttonGunBasi.Enabled = true;
                }

                int a = 1;
                if (gunListesi.Count % 20 == 0)
                    a = 0;

                numericNumberOfCurrentPage.Maximum = gunListesi.Count / 20 + a;
                labelNumberOfPages.Text = (gunListesi.Count / 20 + a).ToString();


                this.currentPageChanged(null, null);

                if (treeGunBasi.Nodes.Count > 0)
                    treeGunBasi.SelectedNode = treeGunBasi.Nodes[0];
            }
        }

        private void buttonGunBasi_Click(object sender, EventArgs e)
        {

            numericNumberOfCurrentPage.Value = 1;

            GunBilgileri yeniGunBasi = new GunBilgileri();

            yeniGunBasi.gunBasiVakti = DateTime.Parse(DateTime.Now.ToString(), new CultureInfo("tr-TR"));
            yeniGunBasi.gunBasiYapanKisi = ayarYapanKisi;

            gunListesi.Add(yeniGunBasi);

            XmlSave.SaveRestoran(gunListesi, "gunler.xml");

            treeGunBasi.Nodes.Insert(0, yeniGunBasi.gunBasiVakti.Date.ToString("d MMMM yyyy", new CultureInfo("tr-TR")) + ", " + yeniGunBasi.gunBasiVakti.ToString("dddd", new CultureInfo("tr-TR")) + " / Saat " + yeniGunBasi.gunBasiVakti.ToString("HH:mm:ss", new CultureInfo("tr-TR")) + " ----- ");

            buttonGunSonu.Enabled = true;
            buttonGunBasi.Enabled = false;
            treeGunBasi.SelectedNode = treeGunBasi.Nodes[0];

            TimeSpan span = DateTime.Now.Subtract(gunListesi[gunListesi.Count - 1].gunBasiVakti);

            labelSure.Text = ((int)span.TotalHours).ToString() + "saat " + ((int)span.Minutes).ToString().PadLeft(2, '0') + "dk " + ((int)span.Seconds).ToString().PadLeft(2, '0') + "sn";
            timerGecenSure.Start();

        }

        private void buttonGunSonu_Click(object sender, EventArgs e)
        {
            DialogResult eminMisiniz;

            using (KontrolFormu dialog = new KontrolFormu("Gün sonu yapmak istediğinize emin misiniz?", true))
            {
                eminMisiniz = dialog.ShowDialog();
            }

            if (eminMisiniz == DialogResult.Yes)
            {

                numericNumberOfCurrentPage.Value = 1;
                gunListesi[gunListesi.Count - 1].gunSonuVakti = DateTime.Parse(DateTime.Now.ToString(), new CultureInfo("tr-TR"));
                gunListesi[gunListesi.Count - 1].gunSonuYapanKisi = ayarYapanKisi;

                XmlSave.SaveRestoran(gunListesi, "gunler.xml");

                treeGunBasi.Nodes[0].Text += DateTime.Now.Date.ToString("d MMMM yyyy", new CultureInfo("tr-TR")) + ", " + DateTime.Now.ToString("dddd", new CultureInfo("tr-TR")) + " / Saat " + DateTime.Now.ToString("HH:mm:ss", new CultureInfo("tr-TR"));

                buttonGunSonu.Enabled = false;
                buttonGunBasi.Enabled = true;
                treeGunBasi.SelectedNode = treeGunBasi.Nodes[0];
                timerGecenSure.Stop();
            }
        }

        private void gunBilgisiDoldur(object sender, TreeViewEventArgs e)
        {
            int neresi = gunListesi.Count - 1 - (((int)numericNumberOfCurrentPage.Value - 1) * 20) - treeGunBasi.SelectedNode.Index;
            labelGunBasi.Text = gunListesi[neresi].gunBasiYapanKisi;
            labelGunSonu.Text = gunListesi[neresi].gunSonuYapanKisi;
            if (treeGunBasi.SelectedNode.Index == 0 && labelGunSonu.Text == "")
            {
                TimeSpan span = DateTime.Now.Subtract(gunListesi[gunListesi.Count - 1].gunBasiVakti);

                labelSure.Text = ((int)span.TotalHours).ToString() + "saat " + ((int)span.Minutes).ToString().PadLeft(2, '0') + "dk " + ((int)span.Seconds).ToString().PadLeft(2, '0') + "sn";
                timerGecenSure.Start();

            }
            else
            {
                timerGecenSure.Stop();
                TimeSpan span = gunListesi[neresi].gunSonuVakti.Subtract(gunListesi[neresi].gunBasiVakti);
                labelSure.Text = ((int)span.TotalHours).ToString() + "saat " + ((int)span.Minutes).ToString().PadLeft(2, '0') + "dk " + ((int)span.Seconds).ToString().PadLeft(2, '0') + "sn";
            }
        }

        private void currentPageChanged(object sender, EventArgs e)
        {
            treeGunBasi.Nodes.Clear();
            int yazdirilacakDeger = gunListesi.Count - 1 - ((int)numericNumberOfCurrentPage.Value - 1) * 20;
            int durulacakDeger = yazdirilacakDeger - 20;

            if (durulacakDeger < -1)
                durulacakDeger = -1;

            if (gunListesi[yazdirilacakDeger].gunSonuVakti.Date.Year > 10)
                treeGunBasi.Nodes.Add(gunListesi[yazdirilacakDeger].gunBasiVakti.Date.ToString("d MMMM yyyy", new CultureInfo("tr-TR")) + ", " + gunListesi[yazdirilacakDeger].gunBasiVakti.ToString("dddd", new CultureInfo("tr-TR")) + " / Saat " + gunListesi[yazdirilacakDeger].gunBasiVakti.ToString("HH:mm:ss", new CultureInfo("tr-TR")) + " ----- " + gunListesi[yazdirilacakDeger].gunSonuVakti.Date.ToString("d MMMM yyyy", new CultureInfo("tr-TR")) + ", " + gunListesi[yazdirilacakDeger].gunSonuVakti.ToString("dddd", new CultureInfo("tr-TR")) + " / Saat " + gunListesi[yazdirilacakDeger].gunSonuVakti.ToString("HH:mm:ss", new CultureInfo("tr-TR")));
            else if (gunListesi[yazdirilacakDeger].gunBasiVakti != null)
                treeGunBasi.Nodes.Add(gunListesi[yazdirilacakDeger].gunBasiVakti.Date.ToString("d MMMM yyyy", new CultureInfo("tr-TR")) + ", " + gunListesi[yazdirilacakDeger].gunBasiVakti.ToString("dddd", new CultureInfo("tr-TR")) + " / Saat " + gunListesi[yazdirilacakDeger].gunBasiVakti.ToString("HH:mm:ss", new CultureInfo("tr-TR")) + " ----- ");

            for (int i = yazdirilacakDeger - 1; i > durulacakDeger; i--)
            {
                treeGunBasi.Nodes.Add(gunListesi[i].gunBasiVakti.Date.ToString("d MMMM yyyy", new CultureInfo("tr-TR")) + ", " + gunListesi[i].gunBasiVakti.ToString("dddd", new CultureInfo("tr-TR")) + " / Saat " + gunListesi[i].gunBasiVakti.ToString("HH:mm:ss", new CultureInfo("tr-TR")) + " ----- " + gunListesi[i].gunSonuVakti.Date.ToString("d MMMM yyyy", new CultureInfo("tr-TR")) + ", " + gunListesi[i].gunSonuVakti.ToString("dddd", new CultureInfo("tr-TR")) + " / Saat " + gunListesi[i].gunSonuVakti.ToString("HH:mm:ss", new CultureInfo("tr-TR")));
            }
        }

        private void timerSaat_Tick(object sender, EventArgs e)
        {
            labelSaat.Text = DateTime.Now.ToString("HH:mm:ss", new CultureInfo("tr-TR"));
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timerGecenSure_Tick(object sender, EventArgs e)
        {
            TimeSpan span = DateTime.Now.Subtract(gunListesi[gunListesi.Count - 1].gunBasiVakti);

            labelSure.Text = ((int)span.TotalHours).ToString() + "saat " + ((int)span.Minutes).ToString().PadLeft(2, '0') + "dk " + ((int)span.Seconds).ToString().PadLeft(2, '0') + "sn";
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            numericNumberOfCurrentPage.UpButton();
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            numericNumberOfCurrentPage.DownButton();
        }

        private void showKeyPad(object sender, EventArgs e)
        {
            pinboardcontrol21.Visible = true;
        }

        private void hideKeyPad(object sender, EventArgs e)
        {
            pinboardcontrol21.Visible = false;
        }

        private void pinboardcontrol21_UserKeyPressed(object sender, PinboardClassLibrary.PinboardEventArgs e)
        {
            SendKeys.Send(e.KeyboardKeyPressed);
        }
    }
}
