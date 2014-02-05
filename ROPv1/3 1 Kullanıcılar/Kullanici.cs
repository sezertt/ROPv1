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
using System.Xml.Serialization;

namespace ROPv1
{
    public partial class Kullanici : UserControl
    {
        List<TumKategoriler> kategoriListesi = new List<TumKategoriler>(); // kategorileri tutacak liste

        public Kullanici()
        {
            InitializeComponent();

            //açılışta capslock açıksa kapatıyoruz.
            ToggleCapsLock(false);
            /*
            #region xml oku

                Get whether the file has the ReadOnly attribute
                bool isReadOnly = (File.GetAttributes("tempfiles.xml") & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;

                // Remove the ReadOnly attribute
                if (isReadOnly)
                    File.SetAttributes("tempfiles.xml", File.GetAttributes("tempfiles.xml") & ~FileAttributes.ReadOnly);

            TumKategoriler[] infoKategoriler = new TumKategoriler[1];

            if (!File.Exists("kategoriler.xml")) // ilk açılışta veya bir sıkıntı sonucu kategoriler dosyası silinirse kendi default kategorilerimizi giriyoruz.
            {
                infoKategoriler[0] = new TumKategoriler();
                infoKategoriler[0].kategoriler = new List<string>();
                infoKategoriler[0].kategoriler.Add("Çorbalar");
                infoKategoriler[0].kategoriler.Add("Spesyaller");
                infoKategoriler[0].kategoriler.Add("Döner");
                infoKategoriler[0].kategoriler.Add("Pideler");
                infoKategoriler[0].kategoriler.Add("Et Yemekleri");
                infoKategoriler[0].kategoriler.Add("Kebaplar");
                infoKategoriler[0].kategoriler.Add("Salatalar");
                infoKategoriler[0].kategoriler.Add("Tatlılar");
                infoKategoriler[0].kategoriler.Add("İçecekler");
                infoKategoriler[0].kategoriler.Add("Kategorisiz Ürünler");
                XmlSave.SaveRestoran(infoKategoriler, "kategoriler.xml");
            }
            // Oluşturulmuş kategorileri xml den okuyoruz
            XmlLoad<TumKategoriler> loadInfoKategori = new XmlLoad<TumKategoriler>();
            infoKategoriler = loadInfoKategori.LoadRestoran("kategoriler.xml");

            //kategorileri tutacak listemize atıyoruz
            kategoriListesi.AddRange(infoKategoriler);

            for (int i = 0; i < kategoriListesi[0].kategoriler.Count; i++)
            {
                treeUrunAdi.Nodes.Add(kategoriListesi[0].kategoriler[i]);
                comboNewKategoriName.Items.Add(kategoriListesi[0].kategoriler[i]);
            }

            UrunOzellikleri[] infoUrun = new UrunOzellikleri[infoKategoriler[0].kategoriler.Count];

            #region ürünlerin ilk tanımlaması
            if (!File.Exists("urunler.xml"))
            {
                for (int i = 0; i < infoKategoriler[0].kategoriler.Count; i++)
                {
                    infoUrun[i] = new UrunOzellikleri();
                    infoUrun[i].urunAdi = new List<string>();
                    infoUrun[i].porsiyonFiyati = new List<string>();
                    infoUrun[i].urunKategorisi = new List<string>();
                }                

                XmlSave.SaveRestoran(infoUrun, "urunler.xml");
            }
            #endregion

            XmlLoad<UrunOzellikleri> loadInfoUrun = new XmlLoad<UrunOzellikleri>();
            infoUrun = loadInfoUrun.LoadRestoran("urunler.xml");

            UrunOzellikleri[] infoUrun2 = new UrunOzellikleri[infoKategoriler[0].kategoriler.Count];

            int count = infoUrun.Count();

            if (infoUrun.Count() > infoUrun2.Count())
                count = infoUrun2.Count();

            for (int i = 0; i < count; i++)
            {
                infoUrun2[i] = infoUrun[i];
            }

            for (int i = infoUrun.Count(); i < infoUrun2.Count(); i++)
            {
                infoUrun2[i] = new UrunOzellikleri();
                infoUrun2[i].urunAdi = new List<string>();
                infoUrun2[i].porsiyonFiyati = new List<string>();
                infoUrun2[i].urunKategorisi = new List<string>();
            }

            for (int i = 0; i < kategoriListesi[0].kategoriler.Count; i++)
            {
                infoUrun2[i].kategorininAdi = kategoriListesi[0].kategoriler[i];
            }

            List<UrunOzellikleri> urunListesiGecici = new List<UrunOzellikleri>();

            urunListesiGecici.AddRange(infoUrun2);

            int kategoriYeri = 0;

            for (int i = 0; i < urunListesiGecici.Count; i++)
            {
                for (int x = 0; x < urunListesiGecici[i].urunAdi.Count; x++)
                {
                    bool girmedi = true;
                    for (int j = 0; j < treeUrunAdi.Nodes.Count; j++)
                    {
                        if (treeUrunAdi.Nodes[j].Text == urunListesiGecici[i].urunKategorisi[x])
                        {
                            girmedi = false;
                            kategoriYeri = j;
                            break;
                        }
                    }
                    if (girmedi)
                    {
                        urunListesiGecici[urunListesiGecici.Count - 1].urunKategorisi.Add("Kategorisiz Ürünler");
                        urunListesiGecici[urunListesiGecici.Count - 1].urunAdi.Add(urunListesiGecici[i].urunAdi[x]);
                        urunListesiGecici[urunListesiGecici.Count - 1].porsiyonFiyati.Add(urunListesiGecici[i].porsiyonFiyati[x]);

                        if (i != urunListesiGecici.Count - 1)
                        {
                            urunListesiGecici[i].urunAdi.RemoveAt(x);
                            urunListesiGecici[i].urunKategorisi.RemoveAt(x);
                            urunListesiGecici[i].porsiyonFiyati.RemoveAt(x);
                            x--;
                        }
                    }
                    else
                    {
                        if (kategoriYeri <= i)
                            treeUrunAdi.Nodes[kategoriYeri].Nodes.Add(urunListesiGecici[i].urunAdi[x]);

                        if (urunListesiGecici[i].urunKategorisi[x] != urunListesiGecici[i].kategorininAdi)
                        {
                            urunListesiGecici[kategoriYeri].urunKategorisi.Add(urunListesiGecici[i].urunKategorisi[x]);
                            urunListesiGecici[kategoriYeri].urunAdi.Add(urunListesiGecici[i].urunAdi[x]);
                            urunListesiGecici[kategoriYeri].porsiyonFiyati.Add(urunListesiGecici[i].porsiyonFiyati[x]);

                            urunListesiGecici[i].urunAdi.RemoveAt(x);
                            urunListesiGecici[i].urunKategorisi.RemoveAt(x);
                            urunListesiGecici[i].porsiyonFiyati.RemoveAt(x);
                            x--;
                        }
                    }
                }

                //kategorilerden silindiğinde sil
                if (urunListesiGecici[i].urunAdi.Count < 1)
                {
                    bool varMi = false;
                    for (int j = 0; j < kategoriListesi[0].kategoriler.Count; j++)
                    {
                        if (kategoriListesi[0].kategoriler[j] == urunListesiGecici[i].kategorininAdi)
                            varMi = true;
                    }
                    if (!varMi)
                    {
                        urunListesiGecici.RemoveAt(i);
                        i--;
                    }
                }
            }

            XmlSave.SaveRestoran(urunListesiGecici, "urunler.xml");

            urunListesi.AddRange(urunListesiGecici);

            urunListesiGecici = null;

            for (int i = 0; i < treeUrunAdi.Nodes.Count; i++)
            {
                treeUrunAdi.Nodes[i].Text = treeUrunAdi.Nodes[i].Text + " (" + urunListesi[i].urunAdi.Count + " ürün)";
                urunSayisi += urunListesi[i].urunAdi.Count;
            }
            labelUrunSayisi.Text = urunSayisi.ToString();
            #endregion
            */
        }

        //capslocku kapatmak için gerekli işlemleri yapıp kapatıyoruz
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
        static void ToggleCapsLock(bool onOrOff)
        {
            if (IsKeyLocked(Keys.CapsLock) == onOrOff)
                return;
            keybd_event(0x14, 0x45, 0x1, (UIntPtr)0);
            keybd_event(0x14, 0x45, 0x1 | 0x2, (UIntPtr)0);

        }

        private void keyboardcontrol1_UserKeyPressed(object sender, KeyboardClassLibrary.KeyboardEventArgs e)
        {
            SendKeys.Send(e.KeyboardKeyPressed);
        }

        private void pinPressed(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void comboBoxKeyPressed(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void showMenu(object sender, EventArgs e)
        {
            ((ComboBox)sender).DroppedDown = true;
        }

    }
}
