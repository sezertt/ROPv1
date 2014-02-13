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
using System.Globalization;

namespace ROPv1
{
    public partial class Products : UserControl
    {
        List<UrunOzellikleri> urunListesi = new List<UrunOzellikleri>();

        List<TumKategoriler> kategoriListesi = new List<TumKategoriler>(); // kategorileri tutacak liste

        CultureInfo turkish = new CultureInfo("tr-TR");

        int urunSayisi = 0;

        bool changingPlace = false;

        public Products()
        {
            InitializeComponent();

            //açılışta capslock açıksa kapatıyoruz.
            ToggleCapsLock(false);
           
            turkish = (CultureInfo)turkish.Clone();
            turkish.NumberFormat.CurrencySymbol = "TL";

            #region xml oku

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

                infoUrun[0].urunAdi.Add("Şırdan Tuzlama");
                infoUrun[0].porsiyonFiyati.Add("7,00");
                infoUrun[0].urunKategorisi.Add("Çorbalar");

                infoUrun[0].urunAdi.Add("İşkembe");
                infoUrun[0].porsiyonFiyati.Add("6,50");
                infoUrun[0].urunKategorisi.Add("Çorbalar");

                infoUrun[0].urunAdi.Add("İşkembe Tuzlama");
                infoUrun[0].porsiyonFiyati.Add("8,00");
                infoUrun[0].urunKategorisi.Add("Çorbalar");

                infoUrun[0].urunAdi.Add("Ayak Paça");
                infoUrun[0].porsiyonFiyati.Add("8,00");
                infoUrun[0].urunKategorisi.Add("Çorbalar");

                infoUrun[0].urunAdi.Add("Kelle Paça");
                infoUrun[0].porsiyonFiyati.Add("9,00");
                infoUrun[0].urunKategorisi.Add("Çorbalar");

                infoUrun[1].urunAdi.Add("Mumbar Dolma");
                infoUrun[1].porsiyonFiyati.Add("9,00");
                infoUrun[1].urunKategorisi.Add("Spesyaller");

                infoUrun[1].urunAdi.Add("İşkembe Güveç");
                infoUrun[1].porsiyonFiyati.Add("9,00");
                infoUrun[1].urunKategorisi.Add("Spesyaller");

                infoUrun[1].urunAdi.Add("Tereyağında Tuzlama");
                infoUrun[1].porsiyonFiyati.Add("7,00");
                infoUrun[1].urunKategorisi.Add("Spesyaller");

                infoUrun[1].urunAdi.Add("Kuzu Kelle");
                infoUrun[1].porsiyonFiyati.Add("8,00");
                infoUrun[1].urunKategorisi.Add("Spesyaller");

                infoUrun[1].urunAdi.Add("Beyin Tava");
                infoUrun[1].porsiyonFiyati.Add("8,00");
                infoUrun[1].urunKategorisi.Add("Spesyaller");

                infoUrun[2].urunAdi.Add("Ankara Döneri");
                infoUrun[2].porsiyonFiyati.Add("8,00");
                infoUrun[2].urunKategorisi.Add("Döner");

                infoUrun[2].urunAdi.Add("Dürüm Döner");
                infoUrun[2].porsiyonFiyati.Add("8,00");
                infoUrun[2].urunKategorisi.Add("Döner");

                infoUrun[2].urunAdi.Add("İskender");
                infoUrun[2].porsiyonFiyati.Add("9,00");
                infoUrun[2].urunKategorisi.Add("Döner");

                infoUrun[2].urunAdi.Add("Kapalı Döner");
                infoUrun[2].porsiyonFiyati.Add("9,00");
                infoUrun[2].urunKategorisi.Add("Döner");

                infoUrun[3].urunAdi.Add("Kıymalı Pide");
                infoUrun[3].porsiyonFiyati.Add("6,00");
                infoUrun[3].urunKategorisi.Add("Pideler");

                infoUrun[3].urunAdi.Add("Kuşbaşılı Pide");
                infoUrun[3].porsiyonFiyati.Add("7,00");
                infoUrun[3].urunKategorisi.Add("Pideler");

                infoUrun[3].urunAdi.Add("Kaşarlı Pide");
                infoUrun[3].porsiyonFiyati.Add("7,00");
                infoUrun[3].urunKategorisi.Add("Pideler");

                infoUrun[3].urunAdi.Add("Karışık Pide");
                infoUrun[3].porsiyonFiyati.Add("8,00");
                infoUrun[3].urunKategorisi.Add("Pideler");

                infoUrun[3].urunAdi.Add("Sucuklu Pide");
                infoUrun[3].porsiyonFiyati.Add("8,00");
                infoUrun[3].urunKategorisi.Add("Pideler");

                infoUrun[3].urunAdi.Add("Lahmacun");
                infoUrun[3].porsiyonFiyati.Add("3,00");
                infoUrun[3].urunKategorisi.Add("Pideler");

                infoUrun[4].urunAdi.Add("Tas Kebabı");
                infoUrun[4].porsiyonFiyati.Add("10,00");
                infoUrun[4].urunKategorisi.Add("Et Yemekleri");

                infoUrun[4].urunAdi.Add("Püreli Kebap");
                infoUrun[4].porsiyonFiyati.Add("10,00");
                infoUrun[4].urunKategorisi.Add("Et Yemekleri");

                infoUrun[4].urunAdi.Add("Beğendili Kebap");
                infoUrun[4].porsiyonFiyati.Add("10,00");
                infoUrun[4].urunKategorisi.Add("Et Yemekleri");

                infoUrun[4].urunAdi.Add("Dana Rosto");
                infoUrun[4].porsiyonFiyati.Add("10,00");
                infoUrun[4].urunKategorisi.Add("Et Yemekleri");

                infoUrun[4].urunAdi.Add("Orman Kebabı");
                infoUrun[4].porsiyonFiyati.Add("10,00");
                infoUrun[4].urunKategorisi.Add("Et Yemekleri");

                infoUrun[4].urunAdi.Add("Çiftlik Kebabı");
                infoUrun[4].porsiyonFiyati.Add("10,00");
                infoUrun[4].urunKategorisi.Add("Et Yemekleri");

                infoUrun[5].urunAdi.Add("İnegöl Köfte");
                infoUrun[5].porsiyonFiyati.Add("7,00");
                infoUrun[5].urunKategorisi.Add("Kebaplar");

                infoUrun[5].urunAdi.Add("Kaşarlı Köfte");
                infoUrun[5].porsiyonFiyati.Add("8,00");
                infoUrun[5].urunKategorisi.Add("Kebaplar");

                infoUrun[5].urunAdi.Add("Adana Kebap");
                infoUrun[5].porsiyonFiyati.Add("7,50");
                infoUrun[5].urunKategorisi.Add("Kebaplar");

                infoUrun[5].urunAdi.Add("Beyti Kebap");
                infoUrun[5].porsiyonFiyati.Add("9,00");
                infoUrun[5].urunKategorisi.Add("Kebaplar");

                infoUrun[5].urunAdi.Add("Patlıcan Kebap");
                infoUrun[5].porsiyonFiyati.Add("10,00");
                infoUrun[5].urunKategorisi.Add("Kebaplar");

                infoUrun[5].urunAdi.Add("Domatesli Kebap");
                infoUrun[5].porsiyonFiyati.Add("8,00");
                infoUrun[5].urunKategorisi.Add("Kebaplar");

                infoUrun[6].urunAdi.Add("Mevsim Salata");
                infoUrun[6].porsiyonFiyati.Add("4,00");
                infoUrun[6].urunKategorisi.Add("Salatalar");

                infoUrun[6].urunAdi.Add("Çoban Salata");
                infoUrun[6].porsiyonFiyati.Add("4,00");
                infoUrun[6].urunKategorisi.Add("Salatalar");

                infoUrun[6].urunAdi.Add("Beyin Salata");
                infoUrun[6].porsiyonFiyati.Add("7,00");
                infoUrun[6].urunKategorisi.Add("Salatalar");

                infoUrun[6].urunAdi.Add("Cacık");
                infoUrun[6].porsiyonFiyati.Add("4,00");
                infoUrun[6].urunKategorisi.Add("Salatalar");

                infoUrun[7].urunAdi.Add("Kaymaklı Ekmek Kadayıfı");
                infoUrun[7].porsiyonFiyati.Add("5,00");
                infoUrun[7].urunKategorisi.Add("Tatlılar");

                infoUrun[7].urunAdi.Add("Sütlü Kadayıf");
                infoUrun[7].porsiyonFiyati.Add("4,00");
                infoUrun[7].urunKategorisi.Add("Tatlılar");

                infoUrun[7].urunAdi.Add("Şekerpare");
                infoUrun[7].porsiyonFiyati.Add("4,00");
                infoUrun[7].urunKategorisi.Add("Tatlılar");

                infoUrun[7].urunAdi.Add("Fırın Sütlaç");
                infoUrun[7].porsiyonFiyati.Add("4,00");
                infoUrun[7].urunKategorisi.Add("Tatlılar");

                infoUrun[8].urunAdi.Add("Kola");
                infoUrun[8].porsiyonFiyati.Add("2,00");
                infoUrun[8].urunKategorisi.Add("İçecekler");

                infoUrun[8].urunAdi.Add("Fanta");
                infoUrun[8].porsiyonFiyati.Add("2,00");
                infoUrun[8].urunKategorisi.Add("İçecekler");

                infoUrun[8].urunAdi.Add("Meyve Suyu");
                infoUrun[8].porsiyonFiyati.Add("2,00");
                infoUrun[8].urunKategorisi.Add("İçecekler");

                infoUrun[8].urunAdi.Add("Ayran");
                infoUrun[8].porsiyonFiyati.Add("1,50");
                infoUrun[8].urunKategorisi.Add("İçecekler");

                infoUrun[8].urunAdi.Add("Soda");
                infoUrun[8].porsiyonFiyati.Add("1,00");
                infoUrun[8].urunKategorisi.Add("İçecekler");

                infoUrun[8].urunAdi.Add("Su");
                infoUrun[8].porsiyonFiyati.Add("1,00");
                infoUrun[8].urunKategorisi.Add("İçecekler");

                infoUrun[8].urunAdi.Add("Çay");
                infoUrun[8].porsiyonFiyati.Add("0,50");
                infoUrun[8].urunKategorisi.Add("İçecekler");

                infoUrun[0].kategorininAdi = "Çorbalar";
                infoUrun[1].kategorininAdi = "Spesyaller";
                infoUrun[2].kategorininAdi = "Döner";
                infoUrun[3].kategorininAdi = "Pideler";
                infoUrun[4].kategorininAdi = "Et Yemekleri";
                infoUrun[5].kategorininAdi = "Kebaplar";
                infoUrun[6].kategorininAdi = "Salatalar";
                infoUrun[7].kategorininAdi = "Tatlılar";
                infoUrun[8].kategorininAdi = "İçecekler";
                infoUrun[9].kategorininAdi = "Kategorisiz Ürünler";

                XmlSave.SaveRestoran(infoUrun, "urunler.xml");
            }
            #endregion

            XmlLoad<UrunOzellikleri> loadInfoUrun = new XmlLoad<UrunOzellikleri>();
            infoUrun = loadInfoUrun.LoadRestoran("urunler.xml");

            UrunOzellikleri[] infoUrun2 = new UrunOzellikleri[infoKategoriler[0].kategoriler.Count];

            int count = infoUrun.Count();

            //eklenen üğrün var ise sayısını buluyoruz
            if (infoUrun.Count() > infoUrun2.Count())
                count = infoUrun2.Count();
             
            //var olan ürünleri ekliyoruz
            for (int i = 0; i < count; i++)
            {
                infoUrun2[i] = infoUrun[i];
            }

            //eklenen üğrün var ise onlara yer açıyoruz 
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

            if (treeUrunAdi.Nodes[0].GetNodeCount(false) > 0)
                treeUrunAdi.SelectedNode = treeUrunAdi.Nodes[0].Nodes[0];
        }

        //Seçilen ürünün bilgileri comboboxlara aktarılır
        private void changeProduct(object sender, TreeViewEventArgs e) // Farklı bir departman seçildi
        {
            if (changingPlace)
            {
                changingPlace = false;
                return;
            }
            if (treeUrunAdi.SelectedNode.Parent != null)
            {
                newProductForm.Enabled = true;
                if (buttonDeleteProduct.Visible)
                {
                    textboxUrunName.Text = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAdi[treeUrunAdi.SelectedNode.Index];
                    double fiyat = Convert.ToDouble(urunListesi[treeUrunAdi.SelectedNode.Parent.Index].porsiyonFiyati[treeUrunAdi.SelectedNode.Index]);
                    textboxUrunFiyat.Text = fiyat.ToString("C", turkish);
                    comboNewKategoriName.Text = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKategorisi[treeUrunAdi.SelectedNode.Index];
                    newProductForm.Text = textboxUrunName.Text;
                }
            }
            else
                newProductForm.Enabled = false;
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

        private void comboBoxKeyPressed(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void showMenu(object sender, EventArgs e)
        {
            ((ComboBox)sender).DroppedDown = true;
        }

        private void expandOrCollapseNode(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeViewHitTestInfo info = treeUrunAdi.HitTest(treeUrunAdi.PointToClient(Cursor.Position));

            if (info != null && info.Node != null)
                if (info.Node.Parent == null)
                {
                    newProductForm.Enabled = false;

                    int index = info.Node.Index;
                    if (treeUrunAdi.Nodes[index].IsExpanded)
                        treeUrunAdi.Nodes[index].Collapse();
                    else
                        treeUrunAdi.Nodes[index].Expand();
                }
        }

        private void fiyatGirildi(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == "")
                ((TextBox)sender).Text = "5";
            double fiyat = Convert.ToDouble(((TextBox)sender).Text);
            if (fiyat <= 0)
                fiyat = 5;

            ((TextBox)sender).Text = fiyat.ToString("C", turkish);
        }

        private void keyPressedOnPriceText(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == ',' && (sender as TextBox).Text.IndexOf(',') > -1)
            {
                e.Handled = true;
            }
        }

        private void fiyatGirilcek(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text.Length > 3)
                ((TextBox)sender).Text = ((TextBox)sender).Text.Substring(0, ((TextBox)sender).Text.Length - 3);
        }

        // seçilen ürünü sil
        private void deleteProduct(object sender, EventArgs e)
        {
            if (treeUrunAdi.SelectedNode == null || treeUrunAdi.SelectedNode.Parent == null)
                return;
            DialogResult eminMisiniz;

            using (KontrolFormu dialog = new KontrolFormu(treeUrunAdi.SelectedNode.Text + " adlı ürünü silmek istediğinize emin misiniz?", true))
            {
                eminMisiniz = dialog.ShowDialog();
            }

            if (eminMisiniz == DialogResult.Yes)
            {
                if (treeUrunAdi.SelectedNode.Parent != null)
                {
                    string agactakiKategori = treeUrunAdi.SelectedNode.Parent.Text.Remove(treeUrunAdi.SelectedNode.Parent.Text.LastIndexOf(' ')); ;
                    agactakiKategori = agactakiKategori.Remove(agactakiKategori.LastIndexOf(' '));
                    treeUrunAdi.SelectedNode.Parent.Text = agactakiKategori + " (" + (treeUrunAdi.SelectedNode.Parent.GetNodeCount(false) - 1) + " ürün)";
                }

                //listeden ürünüü siliyoruz
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].porsiyonFiyati.RemoveAt(treeUrunAdi.SelectedNode.Index);
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKategorisi.RemoveAt(treeUrunAdi.SelectedNode.Index);
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAdi.RemoveAt(treeUrunAdi.SelectedNode.Index);

                //kaydediyoruz
                XmlSave.SaveRestoran(urunListesi, "urunler.xml");

                // ağaçtan ürünü siliyoruz
                treeUrunAdi.SelectedNode.Remove();

                urunSayisi--;
                labelUrunSayisi.Text = urunSayisi.ToString();
            }
        }

        // ürün oluşturmayı iptal et
        private void cancelNewProduct(object sender, EventArgs e)
        {
            textboxUrunName.Text = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAdi[treeUrunAdi.SelectedNode.Index];
            textboxUrunFiyat.Text = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].porsiyonFiyati[treeUrunAdi.SelectedNode.Index];
            comboNewKategoriName.Text = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKategorisi[treeUrunAdi.SelectedNode.Index];
            newProductForm.Text = textboxUrunName.Text;

            buttonDeleteProduct.Visible = true;
            buttonCancel.Visible = false;
            buttonAddNewProduct.Enabled = true;
            treeUrunAdi.Focus();
        }

        // Yeni Ürün Oluşturma Butonu Basıldı
        private void createNewProductButtonPressed(object sender, EventArgs e)
        {
            if (treeUrunAdi.SelectedNode == null)
                return;

            if (newProductForm.Text != "Yeni Ürün") // her basışta yeniden ayarlanmasın diye, ayarlandı mı kontrolü
            {
                newProductForm.Text = "Yeni Ürün";
                textboxUrunName.Text = "";
                if(treeUrunAdi.SelectedNode.Parent == null)
                    comboNewKategoriName.Text = urunListesi[treeUrunAdi.SelectedNode.Index].kategorininAdi;
                else
                    comboNewKategoriName.Text = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].kategorininAdi;

                double fiyat = 5.00;
                textboxUrunFiyat.Text = fiyat.ToString("C", turkish);

                buttonDeleteProduct.Visible = false;
                buttonCancel.Visible = true;
                buttonAddNewProduct.Enabled = false;
                newProductForm.Enabled = true;
            }
            textboxUrunName.Focus();
        }

        // ürünü kaydetme butonuna basıldı 
        private void saveProductButtonPressed(object sender, EventArgs e)
        {
            if (treeUrunAdi.SelectedNode == null)
                return;

            if (textboxUrunName.Text == "Yeni Ürün" || textboxUrunName.Text == "")
            {
                using (KontrolFormu dialog = new KontrolFormu("Hatalı bilgi girdiniz, lütfen kontrol edin", false))
                {
                    dialog.ShowDialog();
                }
                return;
            }

            string urunAdi = textboxUrunName.Text, urunKategorisi = comboNewKategoriName.Text, urunFiyati = textboxUrunFiyat.Text.Substring(0, textboxUrunFiyat.Text.Length - 3);

            int kategoriYeri = 0;
            for (int i = 0; i < treeUrunAdi.Nodes.Count; i++) // kategori varsa yerini al
            {
                string agactakiKategoriler = treeUrunAdi.Nodes[i].Text.Remove(treeUrunAdi.Nodes[i].Text.LastIndexOf(' '));
                agactakiKategoriler = agactakiKategoriler.Remove(agactakiKategoriler.LastIndexOf(' '));
                if (agactakiKategoriler == urunKategorisi)
                {
                    kategoriYeri = i;
                    break;
                }
            }

            string agactakiKategori = treeUrunAdi.Nodes[kategoriYeri].Text.Remove(treeUrunAdi.Nodes[kategoriYeri].Text.LastIndexOf(' ')); ;
            agactakiKategori = agactakiKategori.Remove(agactakiKategori.LastIndexOf(' '));

            //Yeni ürünü kaydet tuşu. ekle tuşuna basıp bilgileri girdikten sonra kaydete basıyoruz önce girilen bilgilerin doğruluğu
            //kontrol edilir sonra yeni ürün mü ekleniyor yoksa eski ürün mü düzenleniyor ona bakılır
            if (newProductForm.Text == "Yeni Ürün") // yeni ürün ekleniyor
            {
                //ürünün kategorisine göre ürünü ağaca ekleriz

                treeUrunAdi.Nodes[kategoriYeri].Nodes.Add(urunAdi);

                //ürün eklenen kategorideki ürün sayısını 1 arttır
                treeUrunAdi.Nodes[kategoriYeri].Text = agactakiKategori + " (" + (treeUrunAdi.Nodes[kategoriYeri].GetNodeCount(false)) + " ürün)";

                //yeni ürün listeye eklenip kaydedilir
                urunListesi[kategoriYeri].urunAdi.Add(urunAdi);// burayı düzenle
                urunListesi[kategoriYeri].urunKategorisi.Add(urunKategorisi);
                urunListesi[kategoriYeri].porsiyonFiyati.Add(urunFiyati);
                XmlSave.SaveRestoran(urunListesi, "urunler.xml");

                newProductForm.Text = urunAdi;

                //eklenen ürün ağaçta seçili node yapılır
                treeUrunAdi.SelectedNode = treeUrunAdi.Nodes[kategoriYeri].Nodes[treeUrunAdi.Nodes[kategoriYeri].Nodes.Count - 1]; // burayı düzenle eklenen ağaç olmalı
                treeUrunAdi.Focus();

                buttonDeleteProduct.Visible = true;
                buttonCancel.Visible = false;
                buttonAddNewProduct.Enabled = true;

                urunSayisi++;
                labelUrunSayisi.Text = urunSayisi.ToString();
                using (KontrolFormu dialog = new KontrolFormu("Yeni Ürün Bilgileri Kaydedilmiştir", false))
                {
                    dialog.ShowDialog();
                }
            }
            else // eski ürün düzenleniyor
            {
                //Ürünün kategorisi değişmediyse yerinin değişmesine gerek yok
                if (agactakiKategori == urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKategorisi[treeUrunAdi.SelectedNode.Index])
                {
                    //eski ürünün listedeki bilgileri güncellenip kaydedilir
                    urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAdi[treeUrunAdi.SelectedNode.Index] = urunAdi;
                    urunListesi[treeUrunAdi.SelectedNode.Parent.Index].porsiyonFiyati[treeUrunAdi.SelectedNode.Index] = urunFiyati;
                    urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKategorisi[treeUrunAdi.SelectedNode.Index] = urunKategorisi;

                    //eski ürünün görünümdeki ismi güncellenir
                    treeUrunAdi.SelectedNode.Text = urunAdi;
                }
                else //Ürünün kategorisi değiştiyse yerini de değiştirmeliyiz
                {
                    //ürünün yeri değişti çıkan kategorideki ürün sayısını 1 azalt yaz
                    if (treeUrunAdi.SelectedNode.Parent != null)
                    {
                        string agactakiKategoriEskiYer = treeUrunAdi.Nodes[treeUrunAdi.SelectedNode.Parent.Index].Text.Remove(treeUrunAdi.Nodes[treeUrunAdi.SelectedNode.Parent.Index].Text.LastIndexOf(' ')); ;
                        agactakiKategoriEskiYer = agactakiKategoriEskiYer.Remove(agactakiKategoriEskiYer.LastIndexOf(' '));
                        treeUrunAdi.Nodes[treeUrunAdi.SelectedNode.Parent.Index].Text = agactakiKategoriEskiYer + " (" + (treeUrunAdi.Nodes[treeUrunAdi.SelectedNode.Parent.Index].GetNodeCount(false) - 1) + " ürün)";
                    }

                    //eski ürünü, yeni yerine koyarız
                    urunListesi[kategoriYeri].urunAdi.Add(urunAdi);// burayı düzenle
                    urunListesi[kategoriYeri].urunKategorisi.Add(urunKategorisi);
                    urunListesi[kategoriYeri].porsiyonFiyati.Add(urunFiyati);

                    //eski ürünü listeden çıkarırız
                    urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAdi.RemoveAt(treeUrunAdi.SelectedNode.Index);
                    urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKategorisi.RemoveAt(treeUrunAdi.SelectedNode.Index);
                    urunListesi[treeUrunAdi.SelectedNode.Parent.Index].porsiyonFiyati.RemoveAt(treeUrunAdi.SelectedNode.Index);

                    //eski ürünü ağaçtan çıkarırız
                    treeUrunAdi.Nodes[treeUrunAdi.SelectedNode.Parent.Index].Nodes.RemoveAt(treeUrunAdi.SelectedNode.Index);
                    //yeni ürünü ağaca ekleriz
                    treeUrunAdi.Nodes[kategoriYeri].Nodes.Add(urunAdi);

                    //ürünün yeri değişti girdiği kategorideki ürün sayısını yaz                   
                    treeUrunAdi.Nodes[kategoriYeri].Text = agactakiKategori + " (" + (treeUrunAdi.Nodes[kategoriYeri].GetNodeCount(false)) + " ürün)";
                }

                XmlSave.SaveRestoran(urunListesi, "urunler.xml");

                //eski ürünün görünümdeki ismi güncellenir
                newProductForm.Text = urunAdi;
                using (KontrolFormu dialog = new KontrolFormu("Ürün Bilgileri Güncellenmiştir", false))
                {
                    dialog.ShowDialog();
                }
            }
        }

        //Ürünün veya Kategorinin Sıralamasını Değiştir - YUKARI
        private void moveNodeUp(object sender, EventArgs e)
        {
            int index = treeUrunAdi.SelectedNode.Index; // aldığımız nodeun üstünde başka node var mı diye bakıyoruz         

            if (index - 1 < 0)
                return;

            if (treeUrunAdi.SelectedNode.Parent == null) // Kategorinin yerini değiştir
            {
                // kategori listesindeki kategorilerin sıralamasını değiştir ve kaydet

                if (treeUrunAdi.SelectedNode.Index == (treeUrunAdi.Nodes.Count - 1))
                    return;

                //treeviewdaki görünümü güncelliyoruz
                changingPlace = true;
                TreeNode node = treeUrunAdi.Nodes[index];
                treeUrunAdi.Nodes[index].Remove();
                changingPlace = true;
                treeUrunAdi.Nodes.Insert(index - 1, node);
                treeUrunAdi.SelectedNode = treeUrunAdi.Nodes[index - 1];

                //xmldeki görünümü güncelliyoruz
                string temp = kategoriListesi[0].kategoriler[index - 1];
                kategoriListesi[0].kategoriler[index - 1] = kategoriListesi[0].kategoriler[index];
                kategoriListesi[0].kategoriler[index] = temp;

                XmlSave.SaveRestoran(kategoriListesi, "kategoriler.xml");

                // urun listesindeki kategorinin ve ürünlerin beraber yerini değiştir ve kaydet

                UrunOzellikleri geciciUrun = new UrunOzellikleri();
                geciciUrun = urunListesi[index - 1];
                urunListesi[index - 1] = urunListesi[index];
                urunListesi[index] = geciciUrun;

                XmlSave.SaveRestoran(urunListesi, "urunler.xml");
            }
            else // ürünün yerini değiştir
            {
                // urun listesindeki ürünlerin yerini değiştir ve kaydet

                //treeviewdaki görünümü güncelliyoruz
                changingPlace = true;
                TreeNode node = treeUrunAdi.SelectedNode.Parent.Nodes[index];
                treeUrunAdi.SelectedNode.Parent.Nodes[index].Remove();
                changingPlace = true;
                treeUrunAdi.SelectedNode.Parent.Nodes.Insert(index - 1, node);
                treeUrunAdi.SelectedNode = treeUrunAdi.SelectedNode.Parent.Nodes[index - 1];

                //xmldeki görünümü güncelliyoruz
                string geciciPorsiyonFiyati = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].porsiyonFiyati[index - 1],
                geciciUrunAdi = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAdi[index - 1],
                geciciUrunKategorisi = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKategorisi[index - 1];

                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].porsiyonFiyati[index - 1] = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].porsiyonFiyati[index];
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAdi[index - 1] = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAdi[index];
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKategorisi[index - 1] = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKategorisi[index];

                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].porsiyonFiyati[index] = geciciPorsiyonFiyati;
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAdi[index] = geciciUrunAdi;
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKategorisi[index] = geciciUrunKategorisi;

                XmlSave.SaveRestoran(urunListesi, "urunler.xml");
            }
        }

        //Ürünün veya Kategorinin Sıralamasını Değiştir - AŞAĞI
        private void modeNodeDown(object sender, EventArgs e)
        {
            int index = treeUrunAdi.SelectedNode.Index;

            if (treeUrunAdi.SelectedNode.Parent == null) // Kategorinin yerini değiştir
            {
                if (index + 1 >= treeUrunAdi.Nodes.Count - 1 || treeUrunAdi.SelectedNode.Index == (treeUrunAdi.Nodes.Count - 1))
                    return;
                // kategori listesindeki kategorilerin sıralamasını değiştir ve kaydet

                //treeviewdaki görünümü güncelliyoruz
                changingPlace = true;
                TreeNode node = treeUrunAdi.Nodes[index];
                treeUrunAdi.Nodes[index].Remove();
                changingPlace = true;
                treeUrunAdi.Nodes.Insert(index + 1, node);

                treeUrunAdi.SelectedNode = treeUrunAdi.Nodes[index + 1];

                //xmldeki görünümü güncelliyoruz
                string temp = kategoriListesi[0].kategoriler[index + 1];
                kategoriListesi[0].kategoriler[index + 1] = kategoriListesi[0].kategoriler[index];
                kategoriListesi[0].kategoriler[index] = temp;

                XmlSave.SaveRestoran(kategoriListesi, "kategoriler.xml");

                // urun listesindeki kategorinin ve ürünlerin beraber yerini değiştir ve kaydet

                UrunOzellikleri geciciUrun = new UrunOzellikleri();
                geciciUrun = urunListesi[index + 1];
                urunListesi[index + 1] = urunListesi[index];
                urunListesi[index] = geciciUrun;

                XmlSave.SaveRestoran(urunListesi, "urunler.xml");

            }
            else // ürünün yerini değiştir
            {
                if (index + 1 >= treeUrunAdi.SelectedNode.Parent.Nodes.Count)
                    return;
                // urun listesindeki kategorinin ve ürünlerin beraber yerini değiştir ve kaydet

                //treeviewdaki görünümü güncelliyoruz
                changingPlace = true;
                TreeNode node = treeUrunAdi.SelectedNode.Parent.Nodes[index];
                treeUrunAdi.SelectedNode.Parent.Nodes[index].Remove();
                changingPlace = true;
                treeUrunAdi.SelectedNode.Parent.Nodes.Insert(index + 1, node);
                treeUrunAdi.SelectedNode = treeUrunAdi.SelectedNode.Parent.Nodes[index + 1];

                //xmldeki görünümü güncelliyoruz
                string geciciPorsiyonFiyati = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].porsiyonFiyati[index + 1],
                geciciUrunAdi = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAdi[index + 1],
                geciciUrunKategorisi = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKategorisi[index + 1];

                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].porsiyonFiyati[index + 1] = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].porsiyonFiyati[index];
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAdi[index + 1] = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAdi[index];
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKategorisi[index + 1] = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKategorisi[index];

                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].porsiyonFiyati[index] = geciciPorsiyonFiyati;
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAdi[index] = geciciUrunAdi;
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKategorisi[index] = geciciUrunKategorisi;

                XmlSave.SaveRestoran(urunListesi, "urunler.xml");
            }
        }
    }
}