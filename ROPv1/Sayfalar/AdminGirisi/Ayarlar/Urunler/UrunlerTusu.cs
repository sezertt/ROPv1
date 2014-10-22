using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Data.SqlClient;
using GlacialComponents.Controls;

namespace ROPv1
{
    public partial class UrunlerTusu : UserControl
    {
        List<KategorilerineGoreUrunler> urunListesi = new List<KategorilerineGoreUrunler>();

        public UrunlerTusu()
        {
            InitializeComponent();
        }

        private void UrunlerTusu_Load(object sender, EventArgs e)
        {
            XmlLoad<KategorilerineGoreUrunler> loadInfoUrun = new XmlLoad<KategorilerineGoreUrunler>();
            KategorilerineGoreUrunler[] infoUrun = loadInfoUrun.LoadRestoran("urunler.xml");

            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT YaziciAdi FROM Yazici");
            SqlDataReader dr = cmd.ExecuteReader();

            List<string> yazicilar = new List<string>();

            while (dr.Read())
            {
                yazicilar.Add(dr.GetString(0));
            }

            urunListesi.AddRange(infoUrun);

            for (int i = 0; i < urunListesi.Count; i++)
            {
                for (int j = 0; j < urunListesi[i].urunAdi.Count; j++)
                {
                    ComboBox comboBoxPorsiyon = new ComboBox();
                    comboBoxPorsiyon.Items.Add("Bir Porsiyon");
                    comboBoxPorsiyon.Items.Add("Yarım Porsiyon");
                    comboBoxPorsiyon.Items.Add("Çeyrek Porsiyon");

                    comboBoxPorsiyon.Tag = urunListesi[i].urunAdi[j] + "p";

                    if (urunListesi[i].urunPorsiyonSinifi[j] == 0) // 1 porsiyon
                        comboBoxPorsiyon.Text = "Bir Porsiyon";
                    else if (urunListesi[i].urunPorsiyonSinifi[j] == 1) // Yarım porsiyon
                        comboBoxPorsiyon.Text = "Yarım Porsiyon";
                    else // Çeyrek porsiyon
                        comboBoxPorsiyon.Text = "Çeyrek Porsiyon";

                    comboBoxPorsiyon.ContextMenuStrip = contextMenuStrip1;
                    comboBoxPorsiyon.KeyPress += comboBox_KeyPress;
                    comboBoxPorsiyon.Click += showMenu;

                    ComboBox comboBoxYaziciBilgilendirme = new ComboBox();
                    comboBoxYaziciBilgilendirme.Items.Add("Yazıcı Bilgilendirilsin");
                    comboBoxYaziciBilgilendirme.Items.Add("Bildirim Yok");

                    comboBoxYaziciBilgilendirme.Tag = urunListesi[i].urunAdi[j] + "m";

                    if (urunListesi[i].urunYaziciyaBildirilmeliMi[j])
                        comboBoxYaziciBilgilendirme.Text = "Yazıcı Bilgilendirilsin";
                    else
                        comboBoxYaziciBilgilendirme.Text = "Bildirim Yok";

                    comboBoxYaziciBilgilendirme.ContextMenuStrip = contextMenuStrip1;
                    comboBoxYaziciBilgilendirme.KeyPress += comboBox_KeyPress;
                    comboBoxYaziciBilgilendirme.Click += showMenu;

                    

                    ComboBox comboBoxYazici = new ComboBox();

                    foreach(string yazici in yazicilar)
                    {
                        comboBoxYazici.Items.Add(yazici);
                    }

                    comboBoxYazici.Tag = urunListesi[i].urunAdi[j] + "y";

                    comboBoxYazici.Text = urunListesi[i].urunYazicisi[j];

                    comboBoxYazici.ContextMenuStrip = contextMenuStrip1;
                    comboBoxYazici.KeyPress += comboBox_KeyPress;
                    comboBoxYazici.Click += showMenu;

                    GLItem urun = new GLItem();
                    urun.ForeColor = SystemColors.ActiveCaption;
                    urun.Text = "    " + urunListesi[i].urunAdi[j];

                    glacialListUrunler.Items.Add(urun);
                    glacialListUrunler.Items[glacialListUrunler.Items.Count - 1].SubItems[1].Control = comboBoxPorsiyon;
                    glacialListUrunler.Items[glacialListUrunler.Items.Count - 1].SubItems[2].Control = comboBoxYaziciBilgilendirme;
                    glacialListUrunler.Items[glacialListUrunler.Items.Count - 1].SubItems[3].Control = comboBoxYazici;
                    glacialListUrunler.Items[glacialListUrunler.Items.Count - 1].SubItems[0].ForeColor = SystemColors.ActiveCaption;
                    glacialListUrunler.Items[glacialListUrunler.Items.Count - 1].SubItems[1].Control.ForeColor = SystemColors.ActiveCaption;
                    glacialListUrunler.Items[glacialListUrunler.Items.Count - 1].SubItems[2].Control.ForeColor = SystemColors.ActiveCaption;
                    glacialListUrunler.Items[glacialListUrunler.Items.Count - 1].SubItems[3].Control.ForeColor = SystemColors.ActiveCaption;

                }
            }
        }

        private static void AdjustViewColumns(GlacialList view)
        {
            int w = view.Size.Width - SystemInformation.VerticalScrollBarWidth;
            // NOTE: can't use ClientSize.Width if vertical scrollbar is already shown, wing it 
            w -= 4;

            for (int x = view.Columns.Count - 1; x >= 0; x--)
            {
                if (view.Columns[x].Width < w) w -= view.Columns[x].Width;
                else
                {
                    view.Columns[x].Width = w;
                    // Hide columns that can't fit 
                    for (int jx = x + 1; jx < view.Columns.Count; ++jx)
                        view.Columns[jx].Width = 0;
                    return;
                }
            }
            // Widen last column to fill view 
            view.Columns[0].Width += w;
        }

        private void showMenu(object sender, EventArgs e)
        {
            ((ComboBox)sender).DroppedDown = true;
        }

        private void comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void glacialListUrunler_SizeChanged(object sender, EventArgs e)
        {
            AdjustViewColumns(glacialListUrunler);
        }

        private void buttonSaveNewProduct_Click(object sender, EventArgs e)
        {
            int glacialUrunListesiSirasi = 0;

            for (int i = 0; i < urunListesi.Count; i++)
            {
                for (int j = 0; j < urunListesi[i].urunAdi.Count; j++)
                {
                    string urunPorsiyonuString = ((ComboBox)glacialListUrunler.Items[glacialUrunListesiSirasi].SubItems[1].Control).Text;

                    int urunPorsiyonu = 0; // Bir porsiyon

                    if (urunPorsiyonuString == "Yarım Porsiyon")
                    {
                        urunPorsiyonu = 1;
                    }
                    else if (urunPorsiyonuString == "Çeyrek Porsiyon")
                    {
                        urunPorsiyonu = 2;
                    }

                    urunListesi[i].urunPorsiyonSinifi[j] = urunPorsiyonu;

                    string urunYaziciyaBildirilmeliMiString = ((ComboBox)glacialListUrunler.Items[glacialUrunListesiSirasi].SubItems[2].Control).Text;

                    bool urunYaziciyaBildirilmeliMi = true; // Yazıcı Bilgilendirilsin

                    if (urunYaziciyaBildirilmeliMiString == "Bildirim Yok")
                    {
                        urunYaziciyaBildirilmeliMi = false;
                    }

                    urunListesi[i].urunYaziciyaBildirilmeliMi[j] = urunYaziciyaBildirilmeliMi;

                    urunListesi[i].urunYazicisi[j] = ((ComboBox)glacialListUrunler.Items[glacialUrunListesiSirasi].SubItems[3].Control).Text;

                    glacialUrunListesiSirasi++;
                }
            }
            XmlSave.SaveRestoran(urunListesi, "urunler.xml");
            KontrolFormu dialog = new KontrolFormu("Ürün Bilgileri Kaydedilmiştir", false);
            dialog.Show();
        }
    }
}