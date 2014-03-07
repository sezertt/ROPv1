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
            ayarYapanKisi = GunBasiveyaSonuYapanKisi;
        }

        private void buttonGunBasi_Click(object sender, EventArgs e)
        {
            numericNumberOfCurrentPage.Value = 1;

            GunBilgileri yeniGunBasi = new GunBilgileri();

            yeniGunBasi.gunBasiVakti = DateTime.Parse(DateTime.Now.ToString(), new CultureInfo("tr-TR"));
            yeniGunBasi.gunBasiYapanKisi = ayarYapanKisi;

            gunListesi.Add(yeniGunBasi);

            XmlSave.SaveRestoran(gunListesi, "gunler.xml");

            listHesap.Items.Insert(0, yeniGunBasi.gunBasiVakti.Date.ToString("d MMMM yyyy", new CultureInfo("tr-TR")));
            listHesap.Items[0].SubItems.Add(yeniGunBasi.gunBasiVakti.ToString("dddd", new CultureInfo("tr-TR")));
            listHesap.Items[0].SubItems.Add(yeniGunBasi.gunBasiVakti.ToString("HH:mm:ss", new CultureInfo("tr-TR")));

            buttonGunSonu.Enabled = true;
            buttonGunBasi.Enabled = false;
            listHesap.FocusedItem = listHesap.Items[0];

            TimeSpan span = DateTime.Now.Subtract(gunListesi[gunListesi.Count - 1].gunBasiVakti);

            labelSure.Text = ((int)span.TotalHours).ToString() + "saat " + ((int)span.Minutes).ToString().PadLeft(2, '0') + "dk " + ((int)span.Seconds).ToString().PadLeft(2, '0') + "sn";
            timerGecenSure.Start();
            labelGunBasi.Text = ayarYapanKisi;
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

                listHesap.Items[0].SubItems.Add("");
                listHesap.Items[0].SubItems.Add(DateTime.Now.Date.ToString("d MMMM yyyy", new CultureInfo("tr-TR")));
                listHesap.Items[0].SubItems.Add(DateTime.Now.ToString("dddd", new CultureInfo("tr-TR")));
                listHesap.Items[0].SubItems.Add(DateTime.Now.ToString("HH:mm:ss", new CultureInfo("tr-TR")));

                buttonGunSonu.Enabled = false;
                buttonGunBasi.Enabled = true;

                listHesap.FocusedItem = listHesap.Items[0];

                timerGecenSure.Stop();
                labelGunSonu.Text = ayarYapanKisi;
            }
        }
        private void listHesap_MouseUp(object sender, MouseEventArgs e)
        {
            int neresi = gunListesi.Count - 1 - (((int)numericNumberOfCurrentPage.Value - 1) * 19) - listHesap.SelectedItems[0].Index;
            labelGunBasi.Text = gunListesi[neresi].gunBasiYapanKisi;
            labelGunSonu.Text = gunListesi[neresi].gunSonuYapanKisi;
            if (listHesap.SelectedItems[0].Index == 0 && labelGunSonu.Text == "")
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
            listHesap.Items.Clear();

            int yazdirilacakDeger = gunListesi.Count - 1 - ((int)numericNumberOfCurrentPage.Value - 1) * 19;
            int durulacakDeger = yazdirilacakDeger - 19;

            if (durulacakDeger < -1)
                durulacakDeger = -1;

            if (gunListesi[yazdirilacakDeger].gunSonuVakti.Date.Year > 10)
            {
                listHesap.Items.Add(gunListesi[yazdirilacakDeger].gunBasiVakti.Date.ToString("d MMMM yyyy", new CultureInfo("tr-TR")));

                listHesap.Items[listHesap.Items.Count - 1].SubItems.Add(gunListesi[yazdirilacakDeger].gunBasiVakti.ToString("dddd", new CultureInfo("tr-TR")));

                listHesap.Items[listHesap.Items.Count - 1].SubItems.Add(gunListesi[yazdirilacakDeger].gunBasiVakti.ToString("HH:mm:ss", new CultureInfo("tr-TR")));

                listHesap.Items[listHesap.Items.Count - 1].SubItems.Add("");

                listHesap.Items[listHesap.Items.Count - 1].SubItems.Add(gunListesi[yazdirilacakDeger].gunSonuVakti.Date.ToString("d MMMM yyyy", new CultureInfo("tr-TR")));

                listHesap.Items[listHesap.Items.Count - 1].SubItems.Add(gunListesi[yazdirilacakDeger].gunSonuVakti.ToString("dddd", new CultureInfo("tr-TR")));

                listHesap.Items[listHesap.Items.Count - 1].SubItems.Add(gunListesi[yazdirilacakDeger].gunSonuVakti.ToString("HH:mm:ss", new CultureInfo("tr-TR")));
            }
            else if (gunListesi[yazdirilacakDeger].gunBasiVakti != null)
            {
                listHesap.Items.Add(gunListesi[yazdirilacakDeger].gunBasiVakti.Date.ToString("d MMMM yyyy", new CultureInfo("tr-TR")));

                listHesap.Items[listHesap.Items.Count - 1].SubItems.Add(gunListesi[yazdirilacakDeger].gunBasiVakti.ToString("dddd", new CultureInfo("tr-TR")));

                listHesap.Items[listHesap.Items.Count - 1].SubItems.Add(gunListesi[yazdirilacakDeger].gunBasiVakti.ToString("HH:mm:ss", new CultureInfo("tr-TR")));
            }


            for (int i = yazdirilacakDeger - 1; i > durulacakDeger; i--)
            {
                listHesap.Items.Add(gunListesi[i].gunBasiVakti.Date.ToString("d MMMM yyyy", new CultureInfo("tr-TR")));

                listHesap.Items[listHesap.Items.Count - 1].SubItems.Add(gunListesi[i].gunBasiVakti.ToString("dddd", new CultureInfo("tr-TR")));

                listHesap.Items[listHesap.Items.Count - 1].SubItems.Add(gunListesi[i].gunBasiVakti.ToString("HH:mm:ss", new CultureInfo("tr-TR")));

                listHesap.Items[listHesap.Items.Count - 1].SubItems.Add("");

                listHesap.Items[listHesap.Items.Count - 1].SubItems.Add(gunListesi[i].gunSonuVakti.Date.ToString("d MMMM yyyy", new CultureInfo("tr-TR")));

                listHesap.Items[listHesap.Items.Count - 1].SubItems.Add(gunListesi[i].gunSonuVakti.ToString("dddd", new CultureInfo("tr-TR")));

                listHesap.Items[listHesap.Items.Count - 1].SubItems.Add(gunListesi[i].gunSonuVakti.ToString("HH:mm:ss", new CultureInfo("tr-TR")));
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

        private void GunFormu_Load(object sender, EventArgs e)
        {
            this.BringToFront();

            labelSaat.Text = DateTime.Now.ToString("HH:mm:ss", new CultureInfo("tr-TR"));
            labelGun.Text = DateTime.Now.ToString("dddd", new CultureInfo("tr-TR"));
            labelTarih.Text = DateTime.Now.Date.ToString("d MMMM yyyy", new CultureInfo("tr-TR"));
            timerSaat.Start();

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
                if (gunListesi.Count % 19 == 0)
                    a = 0;

                numericNumberOfCurrentPage.Maximum = gunListesi.Count / 19 + a;
                labelNumberOfPages.Text = (gunListesi.Count / 19 + a).ToString();


                this.currentPageChanged(null, null);

                if (listHesap.Items.Count > 0)
                    listHesap.FocusedItem = listHesap.Items[0];
            }
            listHesap.Columns[0].Width = listHesap.Width / 7;
            listHesap.Columns[1].Width = listHesap.Width / 7;
            listHesap.Columns[2].Width = listHesap.Width / 7;
            listHesap.Columns[3].Width = listHesap.Width / 7;
            listHesap.Columns[4].Width = listHesap.Width / 7;
            listHesap.Columns[5].Width = listHesap.Width / 7;
            listHesap.Columns[6].Width = listHesap.Width / 7;
        }

        private void listHesap_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = listHesap.Columns[e.ColumnIndex].Width;
        }
    }
}