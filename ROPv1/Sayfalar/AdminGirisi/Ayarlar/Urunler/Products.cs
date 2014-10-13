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
        List<KategorilerineGoreUrunler> urunListesi = new List<KategorilerineGoreUrunler>();

        List<TumKategoriler> kategoriListesi = new List<TumKategoriler>(); // kategorileri tutacak liste

        CultureInfo turkish = new CultureInfo("tr-TR");

        List<string> marsYazicilari;

        int urunSayisi = 0;

        bool changingPlace = false;

        public Products()
        {
            InitializeComponent();
        }

        //Seçilen ürünün bilgileri comboboxlara aktarılır
        private void changeProduct(object sender, TreeViewEventArgs e) // Farklı bir departman seçildi
        {
            if (changingPlace)
            {
                changingPlace = false;
                return;
            }
            marsYazicilari = new List<string>();

            if (treeUrunAdi.SelectedNode.Parent != null)
            {
                if (buttonDeleteProduct.Visible)
                {
                    textboxUrunName.Text = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAdi[treeUrunAdi.SelectedNode.Index];
                    textBoxBarkod.Text = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunBarkodu[treeUrunAdi.SelectedNode.Index];

                    double fiyatPorsiyon = Convert.ToDouble(urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunPorsiyonFiyati[treeUrunAdi.SelectedNode.Index]);
                    double fiyatKilo = Convert.ToDouble(urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKiloFiyati[treeUrunAdi.SelectedNode.Index]);

                    textboxUrunPorsiyonFiyat.Text = fiyatPorsiyon.ToString("C", turkish);
                    textboxUrunKiloFiyat.Text = fiyatKilo.ToString("C", turkish);
                    comboNewKategoriName.Text = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKategorisi[treeUrunAdi.SelectedNode.Index];
                    comboTur.Text = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunTuru[treeUrunAdi.SelectedNode.Index];
                    comboKDV.Text = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKDV[treeUrunAdi.SelectedNode.Index].ToString();
                    textBoxUrunAciklamasi.Text = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAciklamasi[treeUrunAdi.SelectedNode.Index];
                    newProductForm.Text = textboxUrunName.Text;
                    textBoxUrunAciklamasi.Enabled = true;
                    newProductForm.Enabled = true;

                    buttonMarsYazicilari.Text = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunMarsYazicilari[treeUrunAdi.SelectedNode.Index].Count() + " Adet";

                    string path = Application.StartupPath + @"\resimler\" + textboxUrunName.Text + ".png";

                    if (System.IO.File.Exists(path))
                    {
                        buttonResim.Text = textboxUrunName.Text;
                    }
                    else
                    {
                        buttonResim.Text = "Ürün Fotoğrafı";
                    }
                }

            }
            else
            {
                if (buttonDeleteProduct.Visible)
                {
                    newProductForm.Enabled = false;
                    textBoxUrunAciklamasi.Enabled = false;
                }
            }
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
                    if (buttonDeleteProduct.Visible)
                    {
                        newProductForm.Enabled = false;
                        textBoxUrunAciklamasi.Enabled = false;
                    }

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

                //listeden ürünü siliyoruz
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunPorsiyonFiyati.RemoveAt(treeUrunAdi.SelectedNode.Index);
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKiloFiyati.RemoveAt(treeUrunAdi.SelectedNode.Index);
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKategorisi.RemoveAt(treeUrunAdi.SelectedNode.Index);
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunTuru.RemoveAt(treeUrunAdi.SelectedNode.Index);
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAdi.RemoveAt(treeUrunAdi.SelectedNode.Index);
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKDV.RemoveAt(treeUrunAdi.SelectedNode.Index);
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunYaziciyaBildirilmeliMi.RemoveAt(treeUrunAdi.SelectedNode.Index);
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunYazicisi.RemoveAt(treeUrunAdi.SelectedNode.Index);
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunPorsiyonSinifi.RemoveAt(treeUrunAdi.SelectedNode.Index);
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAciklamasi.RemoveAt(treeUrunAdi.SelectedNode.Index);
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunBarkodu.RemoveAt(treeUrunAdi.SelectedNode.Index);
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunMarsYazicilari.RemoveAt(treeUrunAdi.SelectedNode.Index);

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
            if (treeUrunAdi.SelectedNode.Parent == null)
            {
                newProductForm.Enabled = false;
                textBoxUrunAciklamasi.Enabled = false;
            }
            else
            {
                double fiyatPorsiyon = Convert.ToDouble(urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunPorsiyonFiyati[treeUrunAdi.SelectedNode.Index]);
                double fiyatKilo = Convert.ToDouble(urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKiloFiyati[treeUrunAdi.SelectedNode.Index]);

                buttonMarsYazicilari.Text = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunMarsYazicilari[treeUrunAdi.SelectedNode.Index].Count() + " Adet";

                textboxUrunName.Text = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAdi[treeUrunAdi.SelectedNode.Index];
                textBoxBarkod.Text = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunBarkodu[treeUrunAdi.SelectedNode.Index];
                textboxUrunPorsiyonFiyat.Text = fiyatPorsiyon.ToString("C", turkish);
                textboxUrunKiloFiyat.Text = fiyatKilo.ToString("C", turkish);
                comboNewKategoriName.Text = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKategorisi[treeUrunAdi.SelectedNode.Index];
                comboTur.Text = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunTuru[treeUrunAdi.SelectedNode.Index];
                comboKDV.Text = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKDV[treeUrunAdi.SelectedNode.Index].ToString();
                textBoxUrunAciklamasi.Text = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAciklamasi[treeUrunAdi.SelectedNode.Index];
                newProductForm.Text = textboxUrunName.Text;
            }

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
                textBoxBarkod.Text = "";

                if (treeUrunAdi.SelectedNode.Parent == null)
                    comboNewKategoriName.Text = urunListesi[treeUrunAdi.SelectedNode.Index].kategorininAdi;
                else
                    comboNewKategoriName.Text = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].kategorininAdi;

                comboTur.Text = "Porsiyon";
                textboxUrunKiloFiyat.Enabled = false;

                double fiyat = 5.00;
                textboxUrunPorsiyonFiyat.Text = fiyat.ToString("C", turkish);
                fiyat = 0.00;
                textboxUrunKiloFiyat.Text = fiyat.ToString("C", turkish);

                buttonDeleteProduct.Visible = false;
                buttonCancel.Visible = true;
                buttonAddNewProduct.Enabled = false;
                newProductForm.Enabled = true;
                textBoxUrunAciklamasi.Enabled = true;
            }
            textboxUrunName.Focus();
        }

        // ürünü kaydetme butonuna basıldı 
        private void saveProductButtonPressed(object sender, EventArgs e)
        {
            KontrolFormu dialog;

            if (treeUrunAdi.SelectedNode == null)
                return;

            if (textboxUrunName.Text == "Yeni Ürün" || textboxUrunName.Text == "")
            {
                dialog = new KontrolFormu("Eksik veya hatalı bilgi girdiniz, lütfen kontrol ediniz", false);
                dialog.Show();
                return;
            }

            if (textBoxBarkod.Text != "")
            {
                while (textBoxBarkod.Text.Length < textBoxBarkod.MaxLength)
                {
                    textBoxBarkod.Text = 0 + textBoxBarkod.Text;
                }
            }

            string urunAdi = textboxUrunName.Text, urunTuru = comboTur.Text, urunKategorisi = comboNewKategoriName.Text, urunPorsiyonFiyati = textboxUrunPorsiyonFiyat.Text.Substring(0, textboxUrunPorsiyonFiyat.Text.Length - 3), urunKiloFiyati = textboxUrunKiloFiyat.Text.Substring(0, textboxUrunKiloFiyat.Text.Length - 3), urunBarkodu = textBoxBarkod.Text;
            int urunKDV = Convert.ToInt32(comboKDV.Text);

            bool urunYaziciyaBildirilmeliMi = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunYaziciyaBildirilmeliMi[treeUrunAdi.SelectedNode.Index];
            int urunPorsiyonu = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunPorsiyonSinifi[treeUrunAdi.SelectedNode.Index];
            string urunYazicisi = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunYazicisi[treeUrunAdi.SelectedNode.Index];
            string urunAciklamasi = textBoxUrunAciklamasi.Text;

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
                for (int i = 0; i < urunListesi.Count; i++)
                {
                    for (int j = 0; j < urunListesi[i].urunAdi.Count; j++)
                    {
                        if (string.Equals(urunAdi, urunListesi[i].urunAdi[j], StringComparison.CurrentCultureIgnoreCase))
                        {
                            dialog = new KontrolFormu("Aynı isimde bir ürün bulunmaktadır, lütfen ürün ismini değiştirin", false);
                            dialog.Show();
                            return;
                        }

                        if (urunBarkodu != "")
                        {
                            if (string.Equals(urunBarkodu, urunListesi[i].urunBarkodu[j], StringComparison.CurrentCultureIgnoreCase))
                            {
                                dialog = new KontrolFormu("Aynı barkoda sahip bir ürün bulunmaktadır, lütfen ürün barkodunu değiştirin", false);
                                dialog.Show();
                                return;
                            }
                        }
                    }
                }
                //ürünün kategorisine göre ürünü ağaca ekleriz
                treeUrunAdi.Nodes[kategoriYeri].Nodes.Add(urunAdi);

                //ürün eklenen kategorideki ürün sayısını 1 arttır
                treeUrunAdi.Nodes[kategoriYeri].Text = agactakiKategori + " (" + (treeUrunAdi.Nodes[kategoriYeri].GetNodeCount(false)) + " ürün)";

                //yeni ürün listeye eklenip kaydedilir
                urunListesi[kategoriYeri].urunAdi.Add(urunAdi);
                urunListesi[kategoriYeri].urunKategorisi.Add(urunKategorisi);
                urunListesi[kategoriYeri].urunTuru.Add(urunTuru);
                urunListesi[kategoriYeri].urunPorsiyonFiyati.Add(urunPorsiyonFiyati);
                urunListesi[kategoriYeri].urunKiloFiyati.Add(urunKiloFiyati);
                urunListesi[kategoriYeri].urunKDV.Add(urunKDV);
                urunListesi[kategoriYeri].urunYaziciyaBildirilmeliMi.Add(true);
                urunListesi[kategoriYeri].urunPorsiyonSinifi.Add(0);
                urunListesi[kategoriYeri].urunAciklamasi.Add(urunAciklamasi);
                urunListesi[kategoriYeri].urunBarkodu.Add(urunBarkodu);
                urunListesi[kategoriYeri].urunYazicisi.Add(urunYazicisi);
                urunListesi[kategoriYeri].urunMarsYazicilari.Add(marsYazicilari);

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
                dialog = new KontrolFormu("Yeni Ürün Bilgileri Kaydedilmiştir", false);
                dialog.Show();
            }
            else // eski ürün düzenleniyor
            {
                for (int i = 0; i < urunListesi.Count; i++)
                {
                    for (int j = 0; j < urunListesi[i].urunAdi.Count; j++)
                    {
                        if (i == treeUrunAdi.SelectedNode.Parent.Index && j == treeUrunAdi.SelectedNode.Index)
                            continue;
                        if (string.Equals(urunAdi, urunListesi[i].urunAdi[j], StringComparison.CurrentCultureIgnoreCase))
                        {
                            dialog = new KontrolFormu("Aynı isimde bir ürün bulunmaktadır, lütfen ürün ismini değiştirin", false);
                            dialog.Show();
                            return;
                        }

                        if (urunBarkodu != "")
                        {
                            if (string.Equals(urunBarkodu, urunListesi[i].urunBarkodu[j], StringComparison.CurrentCultureIgnoreCase))
                            {
                                dialog = new KontrolFormu("Aynı barkoda sahip bir ürün bulunmaktadır, lütfen ürün barkodunu değiştirin", false);
                                dialog.Show();
                                return;
                            }
                        }
                    }
                }

                //Ürünün kategorisi değişmediyse yerinin değişmesine gerek yok
                if (agactakiKategori == urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKategorisi[treeUrunAdi.SelectedNode.Index])
                {
                    //eski ürünün listedeki bilgileri güncellenip kaydedilir
                    urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAdi[treeUrunAdi.SelectedNode.Index] = urunAdi;
                    urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunPorsiyonFiyati[treeUrunAdi.SelectedNode.Index] = urunPorsiyonFiyati;
                    urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKiloFiyati[treeUrunAdi.SelectedNode.Index] = urunKiloFiyati;
                    urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKategorisi[treeUrunAdi.SelectedNode.Index] = urunKategorisi;
                    urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunTuru[treeUrunAdi.SelectedNode.Index] = urunTuru;
                    urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKDV[treeUrunAdi.SelectedNode.Index] = urunKDV;
                    urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAciklamasi[treeUrunAdi.SelectedNode.Index] = urunAciklamasi;
                    urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunBarkodu[treeUrunAdi.SelectedNode.Index] = urunBarkodu;
                    urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunYazicisi[treeUrunAdi.SelectedNode.Index] = urunYazicisi;
                    urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunMarsYazicilari[treeUrunAdi.SelectedNode.Index] = marsYazicilari;

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
                    urunListesi[kategoriYeri].urunAdi.Add(urunAdi);
                    urunListesi[kategoriYeri].urunKategorisi.Add(urunKategorisi);
                    urunListesi[kategoriYeri].urunTuru.Add(urunTuru);
                    urunListesi[kategoriYeri].urunPorsiyonFiyati.Add(urunPorsiyonFiyati);
                    urunListesi[kategoriYeri].urunKiloFiyati.Add(urunKiloFiyati);
                    urunListesi[kategoriYeri].urunKDV.Add(urunKDV);
                    urunListesi[kategoriYeri].urunYaziciyaBildirilmeliMi.Add(urunYaziciyaBildirilmeliMi);
                    urunListesi[kategoriYeri].urunPorsiyonSinifi.Add(urunPorsiyonu);
                    urunListesi[kategoriYeri].urunAciklamasi.Add(urunAciklamasi);
                    urunListesi[kategoriYeri].urunBarkodu.Add(urunBarkodu);
                    urunListesi[kategoriYeri].urunYazicisi.Add(urunYazicisi);
                    urunListesi[kategoriYeri].urunMarsYazicilari.Add(marsYazicilari);

                    //eski ürünü listeden çıkarırız
                    urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAdi.RemoveAt(treeUrunAdi.SelectedNode.Index);
                    urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKategorisi.RemoveAt(treeUrunAdi.SelectedNode.Index);
                    urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunTuru.RemoveAt(treeUrunAdi.SelectedNode.Index);
                    urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunPorsiyonFiyati.RemoveAt(treeUrunAdi.SelectedNode.Index);
                    urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKiloFiyati.RemoveAt(treeUrunAdi.SelectedNode.Index);
                    urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKDV.RemoveAt(treeUrunAdi.SelectedNode.Index);
                    urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunYaziciyaBildirilmeliMi.RemoveAt(treeUrunAdi.SelectedNode.Index);
                    urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunPorsiyonSinifi.RemoveAt(treeUrunAdi.SelectedNode.Index);
                    urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAciklamasi.RemoveAt(treeUrunAdi.SelectedNode.Index);
                    urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunBarkodu.RemoveAt(treeUrunAdi.SelectedNode.Index);
                    urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunYazicisi.RemoveAt(treeUrunAdi.SelectedNode.Index);
                    urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunMarsYazicilari.RemoveAt(treeUrunAdi.SelectedNode.Index);

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
                dialog = new KontrolFormu("Ürün Bilgileri Güncellenmiştir", false);
                dialog.Show();
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

                KategorilerineGoreUrunler geciciUrun = new KategorilerineGoreUrunler();
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
                string geciciUrunPorsiyonFiyati = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunPorsiyonFiyati[index - 1],
                    geciciUrunKiloFiyati = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKiloFiyati[index - 1],
                geciciUrunAdi = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAdi[index - 1],
                geciciUrunAciklamasi = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAciklamasi[index - 1],
                geciciUrunKategorisi = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKategorisi[index - 1],
                geciciUrunBarkodu = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunBarkodu[index - 1],
                geciciUrunTuru = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunTuru[index - 1],
                geciciUrunYazicisi = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunYazicisi[index - 1];

                int geciciUrunKDV = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKDV[index - 1];
                int geciciUrunPorsiyonu = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunPorsiyonSinifi[index - 1];
                bool geciciurunYaziciyaBildirilmeliMi = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunYaziciyaBildirilmeliMi[index - 1];

                List<string> geciciMarsYazicilari = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunMarsYazicilari[index - 1];

                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunPorsiyonFiyati[index - 1] = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunPorsiyonFiyati[index];
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKiloFiyati[index - 1] = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKiloFiyati[index];
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAdi[index - 1] = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAdi[index];
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAciklamasi[index - 1] = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAciklamasi[index];
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKategorisi[index - 1] = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKategorisi[index];
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunTuru[index - 1] = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunTuru[index];
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKDV[index - 1] = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKDV[index];
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunPorsiyonSinifi[index - 1] = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunPorsiyonSinifi[index];
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunYaziciyaBildirilmeliMi[index - 1] = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunYaziciyaBildirilmeliMi[index];
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunBarkodu[index - 1] = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunBarkodu[index];
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunYazicisi[index - 1] = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunYazicisi[index];
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunMarsYazicilari[index - 1] = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunMarsYazicilari[index];

                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunPorsiyonFiyati[index] = geciciUrunPorsiyonFiyati;
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKiloFiyati[index] = geciciUrunKiloFiyati;
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAdi[index] = geciciUrunAdi;
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKategorisi[index] = geciciUrunKategorisi;
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunTuru[index] = geciciUrunTuru;
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAciklamasi[index] = geciciUrunAciklamasi;
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKDV[index] = geciciUrunKDV;
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunPorsiyonSinifi[index] = geciciUrunPorsiyonu;
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunYaziciyaBildirilmeliMi[index] = geciciurunYaziciyaBildirilmeliMi;
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunBarkodu[index] = geciciUrunBarkodu;
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunYazicisi[index] = geciciUrunYazicisi;
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunMarsYazicilari[index] = geciciMarsYazicilari;

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

                KategorilerineGoreUrunler geciciUrun = new KategorilerineGoreUrunler();
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
                string geciciUrunPorsiyonFiyati = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunPorsiyonFiyati[index + 1],
                    geciciUrunKiloFiyati = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKiloFiyati[index + 1],
                geciciUrunAdi = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAdi[index + 1],
                geciciUrunKategorisi = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKategorisi[index + 1],
                geciciUrunBarkodu = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunBarkodu[index + 1],
                geciciUrunTuru = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunTuru[index + 1],
                geciciUrunAciklamasi = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAciklamasi[index + 1],
                geciciUrunYazicisi = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunYazicisi[index + 1];

                bool geciciUrunYaziciyaBildirilmeliMi = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunYaziciyaBildirilmeliMi[index + 1];

                int geciciUrunKDV = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKDV[index + 1], geciciUrunPorsiyonSinifi = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunPorsiyonSinifi[index + 1];

                List<string> geciciMarsYazicilari = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunMarsYazicilari[index + 1];

                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunPorsiyonFiyati[index + 1] = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunPorsiyonFiyati[index];
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKiloFiyati[index + 1] = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKiloFiyati[index];
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAdi[index + 1] = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAdi[index];
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKategorisi[index + 1] = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKategorisi[index];
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunTuru[index + 1] = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunTuru[index];
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunBarkodu[index + 1] = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunBarkodu[index];
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAciklamasi[index + 1] = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAciklamasi[index];
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunYaziciyaBildirilmeliMi[index + 1] = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunYaziciyaBildirilmeliMi[index];
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKDV[index + 1] = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKDV[index];
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunPorsiyonSinifi[index + 1] = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunPorsiyonSinifi[index];
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunYazicisi[index + 1] = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunYazicisi[index];
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunMarsYazicilari[index + 1] = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunMarsYazicilari[index];

                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunPorsiyonFiyati[index] = geciciUrunPorsiyonFiyati;
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKiloFiyati[index] = geciciUrunKiloFiyati;
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAdi[index] = geciciUrunAdi;
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKategorisi[index] = geciciUrunKategorisi;
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunTuru[index] = geciciUrunTuru;
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunBarkodu[index] = geciciUrunBarkodu;
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAciklamasi[index] = geciciUrunAciklamasi;
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunYaziciyaBildirilmeliMi[index] = geciciUrunYaziciyaBildirilmeliMi;
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunKDV[index] = geciciUrunKDV;
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunPorsiyonSinifi[index] = geciciUrunPorsiyonSinifi;
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunYazicisi[index] = geciciUrunYazicisi;
                urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunMarsYazicilari[index] = geciciMarsYazicilari;

                XmlSave.SaveRestoran(urunListesi, "urunler.xml");
            }
        }

        //form load
        private void Products_Load(object sender, EventArgs e)
        {
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
                infoKategoriler[0].kategoriler.Add("Kahvaltı");
                infoKategoriler[0].kategoriler.Add("Sahanda Servisler");
                infoKategoriler[0].kategoriler.Add("Omletler");
                infoKategoriler[0].kategoriler.Add("Tostlar");
                infoKategoriler[0].kategoriler.Add("Gözlemeler");
                infoKategoriler[0].kategoriler.Add("Aperatifler");
                infoKategoriler[0].kategoriler.Add("Salatalar");
                infoKategoriler[0].kategoriler.Add("Burgerler");
                infoKategoriler[0].kategoriler.Add("Dürümler");
                infoKategoriler[0].kategoriler.Add("Kumpir");
                infoKategoriler[0].kategoriler.Add("Makarnalar");
                infoKategoriler[0].kategoriler.Add("Pizza");
                infoKategoriler[0].kategoriler.Add("Krep");
                infoKategoriler[0].kategoriler.Add("Ana Yemekler");
                infoKategoriler[0].kategoriler.Add("Anadolu Mutfağı");
                infoKategoriler[0].kategoriler.Add("Vejeteryan Yemekler");
                infoKategoriler[0].kategoriler.Add("Balıklar");
                infoKategoriler[0].kategoriler.Add("Soğuk İçecekler");
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

            KategorilerineGoreUrunler[] infoUrun = new KategorilerineGoreUrunler[infoKategoriler[0].kategoriler.Count];

            #region ürünlerin ilk tanımlaması
            if (!File.Exists("urunler.xml"))
            {
                for (int i = 0; i < infoKategoriler[0].kategoriler.Count; i++)
                {
                    infoUrun[i] = new KategorilerineGoreUrunler();
                    infoUrun[i].urunAdi = new List<string>();
                    infoUrun[i].urunPorsiyonFiyati = new List<string>();
                    infoUrun[i].urunKiloFiyati = new List<string>();
                    infoUrun[i].urunKategorisi = new List<string>();
                    infoUrun[i].urunTuru = new List<string>();
                    infoUrun[i].urunAciklamasi = new List<string>();
                    infoUrun[i].urunKDV = new List<int>();
                    infoUrun[i].urunPorsiyonSinifi = new List<int>();
                    infoUrun[i].urunYaziciyaBildirilmeliMi = new List<bool>();
                    infoUrun[i].urunBarkodu = new List<string>();
                    infoUrun[i].urunYazicisi = new List<string>();
                    infoUrun[i].urunMarsYazicilari = new List<List<string>>();
                }

                infoUrun[0].urunAdi.Add("Osmanlı Kahvaltı");
                infoUrun[0].urunPorsiyonFiyati.Add("23,00");
                infoUrun[0].urunKiloFiyati.Add("0,00");
                infoUrun[0].urunTuru.Add("Porsiyon");
                infoUrun[0].urunKategorisi.Add("Kahvaltı");
                infoUrun[0].urunAciklamasi.Add("Keçi peyniri, tulum peyniri, tereyağ, siyah-yeşil zeytin, domates, salatalık, kızılcık murabbası,katmerli gözleme, pastırmalı yumurta, süt");
                infoUrun[0].urunKDV.Add(8);
                infoUrun[0].urunPorsiyonSinifi.Add(0);
                infoUrun[0].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[0].urunBarkodu.Add("");
                infoUrun[0].urunYazicisi.Add("Mutfak");
                infoUrun[0].urunMarsYazicilari.Add(new List<string>());

                infoUrun[0].urunAdi.Add("Liva Özel Kahvaltı");
                infoUrun[0].urunPorsiyonFiyati.Add("21,75");
                infoUrun[0].urunKiloFiyati.Add("0,00");
                infoUrun[0].urunTuru.Add("Porsiyon");
                infoUrun[0].urunKategorisi.Add("Kahvaltı");
                infoUrun[0].urunAciklamasi.Add("Beyaz peynir, taze kaşar, sepet peyniri, siyah-yeşil zeytin, tavuk jambon, dana jambon, macar salam, 2 çeşit reçel, süzme bal, köy tereyağı, kaymak, livatella, domates, salatalık, biber, ceviz, sigara böreği, yumurta");
                infoUrun[0].urunKDV.Add(8);
                infoUrun[0].urunPorsiyonSinifi.Add(0);
                infoUrun[0].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[0].urunBarkodu.Add("");
                infoUrun[0].urunYazicisi.Add("Mutfak");
                infoUrun[0].urunMarsYazicilari.Add(new List<string>());

                infoUrun[0].urunAdi.Add("Sıcak Kahvaltı");
                infoUrun[0].urunPorsiyonFiyati.Add("21,75");
                infoUrun[0].urunKiloFiyati.Add("0,00");
                infoUrun[0].urunTuru.Add("Porsiyon");
                infoUrun[0].urunKategorisi.Add("Kahvaltı");
                infoUrun[0].urunAciklamasi.Add("Sote patates, ızgara sucuk, sosis ,ızgara hellim peyniri, sigara böreği, kaşar pane, domates, biber, bazlama ekmeği üstüne tek göz yumurta");
                infoUrun[0].urunKDV.Add(8);
                infoUrun[0].urunPorsiyonSinifi.Add(0);
                infoUrun[0].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[0].urunBarkodu.Add("");
                infoUrun[0].urunYazicisi.Add("Mutfak");
                infoUrun[0].urunMarsYazicilari.Add(new List<string>());

                infoUrun[0].urunAdi.Add("Form Kahvaltı");
                infoUrun[0].urunPorsiyonFiyati.Add("21,00");
                infoUrun[0].urunKiloFiyati.Add("0,00");
                infoUrun[0].urunTuru.Add("Porsiyon");
                infoUrun[0].urunKategorisi.Add("Kahvaltı");
                infoUrun[0].urunAciklamasi.Add("Form kahvaltıyla formunuzu koruyun sağlıklı yaşayın");
                infoUrun[0].urunKDV.Add(8);
                infoUrun[0].urunPorsiyonSinifi.Add(0);
                infoUrun[0].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[0].urunBarkodu.Add("");
                infoUrun[0].urunYazicisi.Add("Mutfak");
                infoUrun[0].urunMarsYazicilari.Add(new List<string>());

                infoUrun[0].urunAdi.Add("Ekonomik Kahvaltı");
                infoUrun[0].urunPorsiyonFiyati.Add("19,50");
                infoUrun[0].urunKiloFiyati.Add("0,00");
                infoUrun[0].urunTuru.Add("Porsiyon");
                infoUrun[0].urunKategorisi.Add("Kahvaltı");
                infoUrun[0].urunAciklamasi.Add("Hem miğdenize hem de cebinize göre kahvaltı");
                infoUrun[0].urunKDV.Add(8);
                infoUrun[0].urunPorsiyonSinifi.Add(0);
                infoUrun[0].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[0].urunBarkodu.Add("");
                infoUrun[0].urunYazicisi.Add("Mutfak");
                infoUrun[0].urunMarsYazicilari.Add(new List<string>());

                infoUrun[0].urunAdi.Add("Annemin Kahvaltı");
                infoUrun[0].urunPorsiyonFiyati.Add("21,00");
                infoUrun[0].urunKiloFiyati.Add("0,00");
                infoUrun[0].urunTuru.Add("Porsiyon");
                infoUrun[0].urunKategorisi.Add("Kahvaltı");
                infoUrun[0].urunAciklamasi.Add("Anne eli değmiş gibi");
                infoUrun[0].urunKDV.Add(8);
                infoUrun[0].urunPorsiyonSinifi.Add(0);
                infoUrun[0].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[0].urunBarkodu.Add("");
                infoUrun[0].urunYazicisi.Add("Mutfak");
                infoUrun[0].urunMarsYazicilari.Add(new List<string>());

                infoUrun[0].urunAdi.Add("Anadolu Kahvaltı");
                infoUrun[0].urunPorsiyonFiyati.Add("21,00");
                infoUrun[0].urunKiloFiyati.Add("0,00");
                infoUrun[0].urunTuru.Add("Porsiyon");
                infoUrun[0].urunKategorisi.Add("Kahvaltı");
                infoUrun[0].urunAciklamasi.Add("Anadolu lezzetleri bu kahvaltıda buluşuyor");
                infoUrun[0].urunKDV.Add(8);
                infoUrun[0].urunPorsiyonSinifi.Add(0);
                infoUrun[0].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[0].urunBarkodu.Add("");
                infoUrun[0].urunYazicisi.Add("Mutfak");
                infoUrun[0].urunMarsYazicilari.Add(new List<string>());

                infoUrun[1].urunAdi.Add("Sahanda Yumurta");
                infoUrun[1].urunPorsiyonFiyati.Add("10,00");
                infoUrun[1].urunKiloFiyati.Add("0,00");
                infoUrun[1].urunTuru.Add("Porsiyon");
                infoUrun[1].urunKategorisi.Add("Sahanda Servisler");
                infoUrun[1].urunAciklamasi.Add("");
                infoUrun[1].urunKDV.Add(8);
                infoUrun[1].urunPorsiyonSinifi.Add(0);
                infoUrun[1].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[1].urunBarkodu.Add("");
                infoUrun[1].urunYazicisi.Add("Mutfak");
                infoUrun[1].urunMarsYazicilari.Add(new List<string>());

                infoUrun[1].urunAdi.Add("Sahanda Sucuklu Yumurta");
                infoUrun[1].urunPorsiyonFiyati.Add("11,75");
                infoUrun[1].urunKiloFiyati.Add("0,00");
                infoUrun[1].urunTuru.Add("Porsiyon");
                infoUrun[1].urunKategorisi.Add("Sahanda Servisler");
                infoUrun[1].urunAciklamasi.Add("");
                infoUrun[1].urunKDV.Add(8);
                infoUrun[1].urunPorsiyonSinifi.Add(0);
                infoUrun[1].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[1].urunBarkodu.Add("");
                infoUrun[1].urunYazicisi.Add("Mutfak");
                infoUrun[1].urunMarsYazicilari.Add(new List<string>());

                infoUrun[1].urunAdi.Add("Sahanda Pastırmalı Yumurta");
                infoUrun[1].urunPorsiyonFiyati.Add("13,50");
                infoUrun[1].urunKiloFiyati.Add("0,00");
                infoUrun[1].urunTuru.Add("Porsiyon");
                infoUrun[1].urunKategorisi.Add("Sahanda Servisler");
                infoUrun[1].urunAciklamasi.Add("");
                infoUrun[1].urunKDV.Add(8);
                infoUrun[1].urunPorsiyonSinifi.Add(0);
                infoUrun[1].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[1].urunBarkodu.Add("");
                infoUrun[1].urunYazicisi.Add("Mutfak");
                infoUrun[1].urunMarsYazicilari.Add(new List<string>());

                infoUrun[1].urunAdi.Add("Sahanda Menemen");
                infoUrun[1].urunPorsiyonFiyati.Add("11,50");
                infoUrun[1].urunKiloFiyati.Add("0,00");
                infoUrun[1].urunTuru.Add("Porsiyon");
                infoUrun[1].urunKategorisi.Add("Sahanda Servisler");
                infoUrun[1].urunAciklamasi.Add("");
                infoUrun[1].urunKDV.Add(8);
                infoUrun[1].urunPorsiyonSinifi.Add(0);
                infoUrun[1].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[1].urunBarkodu.Add("");
                infoUrun[1].urunYazicisi.Add("Mutfak");
                infoUrun[1].urunMarsYazicilari.Add(new List<string>());

                infoUrun[1].urunAdi.Add("Sahanda Kavurmalı Yumurta");
                infoUrun[1].urunPorsiyonFiyati.Add("13,50");
                infoUrun[1].urunKiloFiyati.Add("0,00");
                infoUrun[1].urunTuru.Add("Porsiyon");
                infoUrun[1].urunKategorisi.Add("Sahanda Servisler");
                infoUrun[1].urunAciklamasi.Add("");
                infoUrun[1].urunKDV.Add(8);
                infoUrun[1].urunPorsiyonSinifi.Add(0);
                infoUrun[1].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[1].urunBarkodu.Add("");
                infoUrun[1].urunYazicisi.Add("Mutfak");
                infoUrun[1].urunMarsYazicilari.Add(new List<string>());

                infoUrun[1].urunAdi.Add("Karadeniz Usulü Mıhlama");
                infoUrun[1].urunPorsiyonFiyati.Add("11,50");
                infoUrun[1].urunKiloFiyati.Add("0,00");
                infoUrun[1].urunTuru.Add("Porsiyon");
                infoUrun[1].urunKategorisi.Add("Sahanda Servisler");
                infoUrun[1].urunAciklamasi.Add("");
                infoUrun[1].urunKDV.Add(8);
                infoUrun[1].urunPorsiyonSinifi.Add(0);
                infoUrun[1].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[1].urunBarkodu.Add("");
                infoUrun[1].urunYazicisi.Add("Mutfak");
                infoUrun[1].urunMarsYazicilari.Add(new List<string>());

                infoUrun[1].urunAdi.Add("Sahanda Kavurmalı Menemen");
                infoUrun[1].urunPorsiyonFiyati.Add("13,50");
                infoUrun[1].urunKiloFiyati.Add("0,00");
                infoUrun[1].urunTuru.Add("Porsiyon");
                infoUrun[1].urunKategorisi.Add("Sahanda Servisler");
                infoUrun[1].urunAciklamasi.Add("");
                infoUrun[1].urunKDV.Add(8);
                infoUrun[1].urunPorsiyonSinifi.Add(0);
                infoUrun[1].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[1].urunBarkodu.Add("");
                infoUrun[1].urunYazicisi.Add("Mutfak");
                infoUrun[1].urunMarsYazicilari.Add(new List<string>());

                infoUrun[2].urunAdi.Add("Sebzeli Omlet");
                infoUrun[2].urunPorsiyonFiyati.Add("13,50");
                infoUrun[2].urunKiloFiyati.Add("0,00");
                infoUrun[2].urunTuru.Add("Porsiyon");
                infoUrun[2].urunKategorisi.Add("Omletler");
                infoUrun[2].urunAciklamasi.Add("");
                infoUrun[2].urunKDV.Add(8);
                infoUrun[2].urunPorsiyonSinifi.Add(0);
                infoUrun[2].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[2].urunBarkodu.Add("");
                infoUrun[2].urunYazicisi.Add("Mutfak");
                infoUrun[2].urunMarsYazicilari.Add(new List<string>());

                infoUrun[2].urunAdi.Add("Karışık Omlet");
                infoUrun[2].urunPorsiyonFiyati.Add("15,00");
                infoUrun[2].urunKiloFiyati.Add("0,00");
                infoUrun[2].urunTuru.Add("Porsiyon");
                infoUrun[2].urunKategorisi.Add("Omletler");
                infoUrun[2].urunAciklamasi.Add("");
                infoUrun[2].urunKDV.Add(8);
                infoUrun[2].urunPorsiyonSinifi.Add(0);
                infoUrun[2].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[2].urunBarkodu.Add("");
                infoUrun[2].urunYazicisi.Add("Mutfak");
                infoUrun[2].urunMarsYazicilari.Add(new List<string>());

                infoUrun[2].urunAdi.Add("Kaşarlı Omlet");
                infoUrun[2].urunPorsiyonFiyati.Add("13,50");
                infoUrun[2].urunKiloFiyati.Add("0,00");
                infoUrun[2].urunTuru.Add("Porsiyon");
                infoUrun[2].urunKategorisi.Add("Omletler");
                infoUrun[2].urunAciklamasi.Add("");
                infoUrun[2].urunKDV.Add(8);
                infoUrun[2].urunPorsiyonSinifi.Add(0);
                infoUrun[2].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[2].urunBarkodu.Add("");
                infoUrun[2].urunYazicisi.Add("Mutfak");
                infoUrun[2].urunMarsYazicilari.Add(new List<string>());

                infoUrun[2].urunAdi.Add("Mantarlı Omlet");
                infoUrun[2].urunPorsiyonFiyati.Add("13,75");
                infoUrun[2].urunKiloFiyati.Add("0,00");
                infoUrun[2].urunTuru.Add("Porsiyon");
                infoUrun[2].urunKategorisi.Add("Omletler");
                infoUrun[2].urunAciklamasi.Add("");
                infoUrun[2].urunKDV.Add(8);
                infoUrun[2].urunPorsiyonSinifi.Add(0);
                infoUrun[2].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[2].urunBarkodu.Add("");
                infoUrun[2].urunYazicisi.Add("Mutfak");
                infoUrun[2].urunMarsYazicilari.Add(new List<string>());

                infoUrun[2].urunAdi.Add("Sade Omlet");
                infoUrun[2].urunPorsiyonFiyati.Add("12,50");
                infoUrun[2].urunKiloFiyati.Add("0,00");
                infoUrun[2].urunTuru.Add("Porsiyon");
                infoUrun[2].urunKategorisi.Add("Omletler");
                infoUrun[2].urunAciklamasi.Add("");
                infoUrun[2].urunKDV.Add(8);
                infoUrun[2].urunPorsiyonSinifi.Add(0);
                infoUrun[2].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[2].urunBarkodu.Add("");
                infoUrun[2].urunYazicisi.Add("Mutfak");
                infoUrun[2].urunMarsYazicilari.Add(new List<string>());

                infoUrun[2].urunAdi.Add("İspanyol Rulo Omlet");
                infoUrun[2].urunPorsiyonFiyati.Add("13,50");
                infoUrun[2].urunKiloFiyati.Add("0,00");
                infoUrun[2].urunTuru.Add("Porsiyon");
                infoUrun[2].urunKategorisi.Add("Omletler");
                infoUrun[2].urunAciklamasi.Add("");
                infoUrun[2].urunKDV.Add(8);
                infoUrun[2].urunPorsiyonSinifi.Add(0);
                infoUrun[2].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[2].urunBarkodu.Add("");
                infoUrun[2].urunYazicisi.Add("Mutfak");
                infoUrun[2].urunMarsYazicilari.Add(new List<string>());

                infoUrun[2].urunAdi.Add("Beyaz Peynirli Domatesli Omlet");
                infoUrun[2].urunPorsiyonFiyati.Add("13,50");
                infoUrun[2].urunKiloFiyati.Add("0,00");
                infoUrun[2].urunTuru.Add("Porsiyon");
                infoUrun[2].urunKategorisi.Add("Omletler");
                infoUrun[2].urunAciklamasi.Add("");
                infoUrun[2].urunKDV.Add(8);
                infoUrun[2].urunPorsiyonSinifi.Add(0);
                infoUrun[2].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[2].urunBarkodu.Add("");
                infoUrun[2].urunYazicisi.Add("Mutfak");
                infoUrun[2].urunMarsYazicilari.Add(new List<string>());

                infoUrun[3].urunAdi.Add("Bazlama Ekmeğine Kavurmalı Tost");
                infoUrun[3].urunPorsiyonFiyati.Add("15,75");
                infoUrun[3].urunKiloFiyati.Add("0,00");
                infoUrun[3].urunTuru.Add("Porsiyon");
                infoUrun[3].urunKategorisi.Add("Tostlar");
                infoUrun[3].urunAciklamasi.Add("");
                infoUrun[3].urunKDV.Add(8);
                infoUrun[3].urunPorsiyonSinifi.Add(0);
                infoUrun[3].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[3].urunBarkodu.Add("");
                infoUrun[3].urunYazicisi.Add("Mutfak");
                infoUrun[3].urunMarsYazicilari.Add(new List<string>());

                infoUrun[3].urunAdi.Add("Bazlama Ekmeğine Kocaman Tost");
                infoUrun[3].urunPorsiyonFiyati.Add("15,00");
                infoUrun[3].urunKiloFiyati.Add("0,00");
                infoUrun[3].urunTuru.Add("Porsiyon");
                infoUrun[3].urunKategorisi.Add("Tostlar");
                infoUrun[3].urunAciklamasi.Add("");
                infoUrun[3].urunKDV.Add(8);
                infoUrun[3].urunPorsiyonSinifi.Add(0);
                infoUrun[3].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[3].urunBarkodu.Add("");
                infoUrun[3].urunYazicisi.Add("Mutfak");
                infoUrun[3].urunMarsYazicilari.Add(new List<string>());

                infoUrun[3].urunAdi.Add("Karışık French Tost");
                infoUrun[3].urunPorsiyonFiyati.Add("15,00");
                infoUrun[3].urunKiloFiyati.Add("0,00");
                infoUrun[3].urunTuru.Add("Porsiyon");
                infoUrun[3].urunKategorisi.Add("Tostlar");
                infoUrun[3].urunAciklamasi.Add("");
                infoUrun[3].urunKDV.Add(8);
                infoUrun[3].urunPorsiyonSinifi.Add(0);
                infoUrun[3].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[3].urunBarkodu.Add("");
                infoUrun[3].urunYazicisi.Add("Mutfak");
                infoUrun[3].urunMarsYazicilari.Add(new List<string>());

                infoUrun[3].urunAdi.Add("Mantarlı French Tost");
                infoUrun[3].urunPorsiyonFiyati.Add("14,75");
                infoUrun[3].urunKiloFiyati.Add("0,00");
                infoUrun[3].urunTuru.Add("Porsiyon");
                infoUrun[3].urunKategorisi.Add("Tostlar");
                infoUrun[3].urunAciklamasi.Add("");
                infoUrun[3].urunKDV.Add(8);
                infoUrun[3].urunPorsiyonSinifi.Add(0);
                infoUrun[3].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[3].urunBarkodu.Add("");
                infoUrun[3].urunYazicisi.Add("Mutfak");
                infoUrun[3].urunMarsYazicilari.Add(new List<string>());

                infoUrun[3].urunAdi.Add("Tavuklu French Tost");
                infoUrun[3].urunPorsiyonFiyati.Add("15,00");
                infoUrun[3].urunKiloFiyati.Add("0,00");
                infoUrun[3].urunTuru.Add("Porsiyon");
                infoUrun[3].urunKategorisi.Add("Tostlar");
                infoUrun[3].urunAciklamasi.Add("");
                infoUrun[3].urunKDV.Add(8);
                infoUrun[3].urunPorsiyonSinifi.Add(0);
                infoUrun[3].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[3].urunBarkodu.Add("");
                infoUrun[3].urunYazicisi.Add("Mutfak");
                infoUrun[3].urunMarsYazicilari.Add(new List<string>());

                infoUrun[3].urunAdi.Add("Şefin Tostu");
                infoUrun[3].urunPorsiyonFiyati.Add("13,50");
                infoUrun[3].urunKiloFiyati.Add("0,00");
                infoUrun[3].urunTuru.Add("Porsiyon");
                infoUrun[3].urunKategorisi.Add("Tostlar");
                infoUrun[3].urunAciklamasi.Add("");
                infoUrun[3].urunKDV.Add(8);
                infoUrun[3].urunPorsiyonSinifi.Add(0);
                infoUrun[3].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[3].urunBarkodu.Add("");
                infoUrun[3].urunYazicisi.Add("Mutfak");
                infoUrun[3].urunMarsYazicilari.Add(new List<string>());

                infoUrun[3].urunAdi.Add("Karışık Tost");
                infoUrun[3].urunPorsiyonFiyati.Add("13,75");
                infoUrun[3].urunKiloFiyati.Add("0,00");
                infoUrun[3].urunTuru.Add("Porsiyon");
                infoUrun[3].urunKategorisi.Add("Tostlar");
                infoUrun[3].urunAciklamasi.Add("");
                infoUrun[3].urunKDV.Add(8);
                infoUrun[3].urunPorsiyonSinifi.Add(0);
                infoUrun[3].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[3].urunBarkodu.Add("");
                infoUrun[3].urunYazicisi.Add("Mutfak");
                infoUrun[3].urunMarsYazicilari.Add(new List<string>());

                infoUrun[3].urunAdi.Add("Sucuklu Kaşarlı Tost");
                infoUrun[3].urunPorsiyonFiyati.Add("13,00");
                infoUrun[3].urunKiloFiyati.Add("0,00");
                infoUrun[3].urunTuru.Add("Porsiyon");
                infoUrun[3].urunKategorisi.Add("Tostlar");
                infoUrun[3].urunAciklamasi.Add("");
                infoUrun[3].urunKDV.Add(8);
                infoUrun[3].urunPorsiyonSinifi.Add(0);
                infoUrun[3].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[3].urunBarkodu.Add("");
                infoUrun[3].urunYazicisi.Add("Mutfak");
                infoUrun[3].urunMarsYazicilari.Add(new List<string>());

                infoUrun[3].urunAdi.Add("Kaşarlı Tost");
                infoUrun[3].urunPorsiyonFiyati.Add("11,75");
                infoUrun[3].urunKiloFiyati.Add("0,00");
                infoUrun[3].urunTuru.Add("Porsiyon");
                infoUrun[3].urunKategorisi.Add("Tostlar");
                infoUrun[3].urunAciklamasi.Add("");
                infoUrun[3].urunKDV.Add(8);
                infoUrun[3].urunPorsiyonSinifi.Add(0);
                infoUrun[3].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[3].urunBarkodu.Add("");
                infoUrun[3].urunYazicisi.Add("Mutfak");
                infoUrun[3].urunMarsYazicilari.Add(new List<string>());

                infoUrun[3].urunAdi.Add("Beyaz Peynirli Tost");
                infoUrun[3].urunPorsiyonFiyati.Add("11,50");
                infoUrun[3].urunKiloFiyati.Add("0,00");
                infoUrun[3].urunTuru.Add("Porsiyon");
                infoUrun[3].urunKategorisi.Add("Tostlar");
                infoUrun[3].urunAciklamasi.Add("");
                infoUrun[3].urunKDV.Add(8);
                infoUrun[3].urunPorsiyonSinifi.Add(0);
                infoUrun[3].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[3].urunBarkodu.Add("");
                infoUrun[3].urunYazicisi.Add("Mutfak");
                infoUrun[3].urunMarsYazicilari.Add(new List<string>());

                infoUrun[4].urunAdi.Add("Teksas Usulü Tavuklu Quesedilla");
                infoUrun[4].urunPorsiyonFiyati.Add("17,50");
                infoUrun[4].urunKiloFiyati.Add("0,00");
                infoUrun[4].urunTuru.Add("Porsiyon");
                infoUrun[4].urunKategorisi.Add("Gözlemeler");
                infoUrun[4].urunAciklamasi.Add("Tortilla ekmeğinde mevsim sebzeleri, tavuk, kaşar, kızartılmış cips, avokado püresi, jalapeno biberi, salsa ve roka dip sos");
                infoUrun[4].urunKDV.Add(8);
                infoUrun[4].urunPorsiyonSinifi.Add(0);
                infoUrun[4].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[4].urunBarkodu.Add("");
                infoUrun[4].urunYazicisi.Add("Mutfak");
                infoUrun[4].urunMarsYazicilari.Add(new List<string>());

                infoUrun[4].urunAdi.Add("Macar Gözleme");
                infoUrun[4].urunPorsiyonFiyati.Add("15,25");
                infoUrun[4].urunKiloFiyati.Add("0,00");
                infoUrun[4].urunTuru.Add("Porsiyon");
                infoUrun[4].urunKategorisi.Add("Gözlemeler");
                infoUrun[4].urunAciklamasi.Add("Sac yufkasında sotelenmiş dana jambon, tavuk jambon, sosis, biber mix, kaşar peyniri, yanında patates ve livaya özel salata");
                infoUrun[4].urunKDV.Add(8);
                infoUrun[4].urunPorsiyonSinifi.Add(0);
                infoUrun[4].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[4].urunBarkodu.Add("");
                infoUrun[4].urunYazicisi.Add("Mutfak");
                infoUrun[4].urunMarsYazicilari.Add(new List<string>());

                infoUrun[4].urunAdi.Add("Köylüm Gözleme");
                infoUrun[4].urunPorsiyonFiyati.Add("13,25");
                infoUrun[4].urunKiloFiyati.Add("0,00");
                infoUrun[4].urunTuru.Add("Porsiyon");
                infoUrun[4].urunKategorisi.Add("Gözlemeler");
                infoUrun[4].urunAciklamasi.Add("Sac yufkasında kavrulmuş kıyma, patates püresi ve rende kaşar peyniri, yanında patates ve livaya özel salata");
                infoUrun[4].urunKDV.Add(8);
                infoUrun[4].urunPorsiyonSinifi.Add(0);
                infoUrun[4].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[4].urunBarkodu.Add("");
                infoUrun[4].urunYazicisi.Add("Mutfak");
                infoUrun[4].urunMarsYazicilari.Add(new List<string>());

                infoUrun[4].urunAdi.Add("Özel Liva Gözleme");
                infoUrun[4].urunPorsiyonFiyati.Add("15,50");
                infoUrun[4].urunKiloFiyati.Add("0,00");
                infoUrun[4].urunTuru.Add("Porsiyon");
                infoUrun[4].urunKategorisi.Add("Gözlemeler");
                infoUrun[4].urunAciklamasi.Add("Sac yufkasında kavrulmuş kıyma, patates püresi ve rende kaşar, yanında patates ve livaya özel salata");
                infoUrun[4].urunKDV.Add(8);
                infoUrun[4].urunPorsiyonSinifi.Add(0);
                infoUrun[4].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[4].urunBarkodu.Add("");
                infoUrun[4].urunYazicisi.Add("Mutfak");
                infoUrun[4].urunMarsYazicilari.Add(new List<string>());

                infoUrun[4].urunAdi.Add("Kaşar Peynirli Gözleme");
                infoUrun[4].urunPorsiyonFiyati.Add("14,75");
                infoUrun[4].urunKiloFiyati.Add("0,00");
                infoUrun[4].urunTuru.Add("Porsiyon");
                infoUrun[4].urunKategorisi.Add("Gözlemeler");
                infoUrun[4].urunAciklamasi.Add("");
                infoUrun[4].urunKDV.Add(8);
                infoUrun[4].urunPorsiyonSinifi.Add(0);
                infoUrun[4].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[4].urunBarkodu.Add("");
                infoUrun[4].urunYazicisi.Add("Mutfak");
                infoUrun[4].urunMarsYazicilari.Add(new List<string>());

                infoUrun[4].urunAdi.Add("Patatesli Gözleme");
                infoUrun[4].urunPorsiyonFiyati.Add("14,00");
                infoUrun[4].urunKiloFiyati.Add("0,00");
                infoUrun[4].urunTuru.Add("Porsiyon");
                infoUrun[4].urunKategorisi.Add("Gözlemeler");
                infoUrun[4].urunAciklamasi.Add("");
                infoUrun[4].urunKDV.Add(8);
                infoUrun[4].urunPorsiyonSinifi.Add(0);
                infoUrun[4].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[4].urunBarkodu.Add("");
                infoUrun[4].urunYazicisi.Add("Mutfak");
                infoUrun[4].urunMarsYazicilari.Add(new List<string>());

                infoUrun[4].urunAdi.Add("Beyaz Peynirli Gözleme");
                infoUrun[4].urunPorsiyonFiyati.Add("14,50");
                infoUrun[4].urunKiloFiyati.Add("0,00");
                infoUrun[4].urunTuru.Add("Porsiyon");
                infoUrun[4].urunKategorisi.Add("Gözlemeler");
                infoUrun[4].urunAciklamasi.Add("Sac yufkasında beyaz peynir ve maydanoz, yanında patates ve livaya özel salata");
                infoUrun[4].urunKDV.Add(8);
                infoUrun[4].urunPorsiyonSinifi.Add(0);
                infoUrun[4].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[4].urunBarkodu.Add("");
                infoUrun[4].urunYazicisi.Add("Mutfak");
                infoUrun[4].urunMarsYazicilari.Add(new List<string>());

                infoUrun[4].urunAdi.Add("Ispanaklı Mozarella Gözleme");
                infoUrun[4].urunPorsiyonFiyati.Add("15,25");
                infoUrun[4].urunKiloFiyati.Add("0,00");
                infoUrun[4].urunTuru.Add("Porsiyon");
                infoUrun[4].urunKategorisi.Add("Gözlemeler");
                infoUrun[4].urunAciklamasi.Add("");
                infoUrun[4].urunKDV.Add(8);
                infoUrun[4].urunPorsiyonSinifi.Add(0);
                infoUrun[4].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[4].urunBarkodu.Add("");
                infoUrun[4].urunYazicisi.Add("Mutfak");
                infoUrun[4].urunMarsYazicilari.Add(new List<string>());

                infoUrun[5].urunAdi.Add("Paçanga Böreği");
                infoUrun[5].urunPorsiyonFiyati.Add("15,99");
                infoUrun[5].urunKiloFiyati.Add("0,00");
                infoUrun[5].urunTuru.Add("Porsiyon");
                infoUrun[5].urunKategorisi.Add("Aperatifler");
                infoUrun[5].urunAciklamasi.Add("Sac yufkasında sotelenmiş julyen pastırma, biber mix, rende kaşar, yanında patates salsa ve roka dip sos");
                infoUrun[5].urunKDV.Add(8);
                infoUrun[5].urunPorsiyonSinifi.Add(0);
                infoUrun[5].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[5].urunBarkodu.Add("");
                infoUrun[5].urunYazicisi.Add("Mutfak");
                infoUrun[5].urunMarsYazicilari.Add(new List<string>());

                infoUrun[5].urunAdi.Add("Sigara Böreği");
                infoUrun[5].urunPorsiyonFiyati.Add("15,00");
                infoUrun[5].urunKiloFiyati.Add("0,00");
                infoUrun[5].urunTuru.Add("Porsiyon");
                infoUrun[5].urunKategorisi.Add("Aperatifler");
                infoUrun[5].urunAciklamasi.Add("");
                infoUrun[5].urunKDV.Add(8);
                infoUrun[5].urunPorsiyonSinifi.Add(0);
                infoUrun[5].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[5].urunBarkodu.Add("");
                infoUrun[5].urunYazicisi.Add("Mutfak");
                infoUrun[5].urunMarsYazicilari.Add(new List<string>());

                infoUrun[5].urunAdi.Add("Çin Böreği");
                infoUrun[5].urunPorsiyonFiyati.Add("17,00");
                infoUrun[5].urunKiloFiyati.Add("0,00");
                infoUrun[5].urunTuru.Add("Porsiyon");
                infoUrun[5].urunKategorisi.Add("Aperatifler");
                infoUrun[5].urunAciklamasi.Add("Sote julyen mevsim sebzeleri, tavuk parçaları, soya sos ile tatlandırılıp özel teriyaki sos ve livaya özel salata");
                infoUrun[5].urunKDV.Add(8);
                infoUrun[5].urunPorsiyonSinifi.Add(0);
                infoUrun[5].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[5].urunBarkodu.Add("");
                infoUrun[5].urunYazicisi.Add("Mutfak");
                infoUrun[5].urunMarsYazicilari.Add(new List<string>());

                infoUrun[5].urunAdi.Add("Mantar Dolması");
                infoUrun[5].urunPorsiyonFiyati.Add("15,00");
                infoUrun[5].urunKiloFiyati.Add("0,00");
                infoUrun[5].urunTuru.Add("Porsiyon");
                infoUrun[5].urunKategorisi.Add("Aperatifler");
                infoUrun[5].urunAciklamasi.Add("Fırınlanmış mantar üzerine pastırma, salam, sucuk, dana jambon, tavuk jambon ve kaşar");
                infoUrun[5].urunKDV.Add(8);
                infoUrun[5].urunPorsiyonSinifi.Add(0);
                infoUrun[5].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[5].urunBarkodu.Add("");
                infoUrun[5].urunYazicisi.Add("Mutfak");
                infoUrun[5].urunMarsYazicilari.Add(new List<string>());

                infoUrun[5].urunAdi.Add("Sarımsaklı Ekmek");
                infoUrun[5].urunPorsiyonFiyati.Add("6,00");
                infoUrun[5].urunKiloFiyati.Add("0,00");
                infoUrun[5].urunTuru.Add("Porsiyon");
                infoUrun[5].urunKategorisi.Add("Aperatifler");
                infoUrun[5].urunAciklamasi.Add("");
                infoUrun[5].urunKDV.Add(8);
                infoUrun[5].urunPorsiyonSinifi.Add(0);
                infoUrun[5].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[5].urunBarkodu.Add("");
                infoUrun[5].urunYazicisi.Add("Mutfak");
                infoUrun[5].urunMarsYazicilari.Add(new List<string>());

                infoUrun[5].urunAdi.Add("Karışık Sıcak Sepeti");
                infoUrun[5].urunPorsiyonFiyati.Add("19,50");
                infoUrun[5].urunKiloFiyati.Add("0,00");
                infoUrun[5].urunTuru.Add("Porsiyon");
                infoUrun[5].urunKategorisi.Add("Aperatifler");
                infoUrun[5].urunAciklamasi.Add("Kaşar pane, yıldız sosis, paçanga böreği, cornflakes paneli tavuk ve patates");
                infoUrun[5].urunKDV.Add(8);
                infoUrun[5].urunPorsiyonSinifi.Add(0);
                infoUrun[5].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[5].urunBarkodu.Add("");
                infoUrun[5].urunYazicisi.Add("Mutfak");
                infoUrun[5].urunMarsYazicilari.Add(new List<string>());

                infoUrun[5].urunAdi.Add("Piliç Fingers");
                infoUrun[5].urunPorsiyonFiyati.Add("19,00");
                infoUrun[5].urunKiloFiyati.Add("0,00");
                infoUrun[5].urunTuru.Add("Porsiyon");
                infoUrun[5].urunKategorisi.Add("Aperatifler");
                infoUrun[5].urunAciklamasi.Add("Çeşnilerle tatlandırılmış julten tavuk parçaları, patates, roka dip sos ve barbekü sos");
                infoUrun[5].urunKDV.Add(8);
                infoUrun[5].urunPorsiyonSinifi.Add(0);
                infoUrun[5].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[5].urunBarkodu.Add("");
                infoUrun[5].urunYazicisi.Add("Mutfak");
                infoUrun[5].urunMarsYazicilari.Add(new List<string>());

                infoUrun[5].urunAdi.Add("Kıtır Mozarella Çubukları");
                infoUrun[5].urunPorsiyonFiyati.Add("15,99");
                infoUrun[5].urunKiloFiyati.Add("0,00");
                infoUrun[5].urunTuru.Add("Porsiyon");
                infoUrun[5].urunKategorisi.Add("Aperatifler");
                infoUrun[5].urunAciklamasi.Add("Patates, livaya özel salata, roka dip sos ve salsa sos");
                infoUrun[5].urunKDV.Add(8);
                infoUrun[5].urunPorsiyonSinifi.Add(0);
                infoUrun[5].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[5].urunBarkodu.Add("");
                infoUrun[5].urunYazicisi.Add("Mutfak");
                infoUrun[5].urunMarsYazicilari.Add(new List<string>());

                infoUrun[5].urunAdi.Add("Kaşar Pane");
                infoUrun[5].urunPorsiyonFiyati.Add("12,50");
                infoUrun[5].urunKiloFiyati.Add("0,00");
                infoUrun[5].urunTuru.Add("Porsiyon");
                infoUrun[5].urunKategorisi.Add("Aperatifler");
                infoUrun[5].urunAciklamasi.Add("Patates, dip sos ve barbekü sos");
                infoUrun[5].urunKDV.Add(8);
                infoUrun[5].urunPorsiyonSinifi.Add(0);
                infoUrun[5].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[5].urunBarkodu.Add("");
                infoUrun[5].urunYazicisi.Add("Mutfak");
                infoUrun[5].urunMarsYazicilari.Add(new List<string>());

                infoUrun[5].urunAdi.Add("Patates Kroket");
                infoUrun[5].urunPorsiyonFiyati.Add("12,50");
                infoUrun[5].urunKiloFiyati.Add("0,00");
                infoUrun[5].urunTuru.Add("Porsiyon");
                infoUrun[5].urunKategorisi.Add("Aperatifler");
                infoUrun[5].urunAciklamasi.Add("");
                infoUrun[5].urunKDV.Add(8);
                infoUrun[5].urunPorsiyonSinifi.Add(0);
                infoUrun[5].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[5].urunBarkodu.Add("");
                infoUrun[5].urunYazicisi.Add("Mutfak");
                infoUrun[5].urunMarsYazicilari.Add(new List<string>());

                infoUrun[5].urunAdi.Add("Soğan Halkaları");
                infoUrun[5].urunPorsiyonFiyati.Add("12,50");
                infoUrun[5].urunKiloFiyati.Add("0,00");
                infoUrun[5].urunTuru.Add("Porsiyon");
                infoUrun[5].urunKategorisi.Add("Aperatifler");
                infoUrun[5].urunAciklamasi.Add("Roka dip sos ve salsa sos");
                infoUrun[5].urunKDV.Add(8);
                infoUrun[5].urunPorsiyonSinifi.Add(0);
                infoUrun[5].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[5].urunBarkodu.Add("");
                infoUrun[5].urunYazicisi.Add("Mutfak");
                infoUrun[5].urunMarsYazicilari.Add(new List<string>());

                infoUrun[5].urunAdi.Add("Sosis Tava");
                infoUrun[5].urunPorsiyonFiyati.Add("12,75");
                infoUrun[5].urunKiloFiyati.Add("0,00");
                infoUrun[5].urunTuru.Add("Porsiyon");
                infoUrun[5].urunKategorisi.Add("Aperatifler");
                infoUrun[5].urunAciklamasi.Add("Patates ve barbekü sos");
                infoUrun[5].urunKDV.Add(8);
                infoUrun[5].urunPorsiyonSinifi.Add(0);
                infoUrun[5].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[5].urunBarkodu.Add("");
                infoUrun[5].urunYazicisi.Add("Mutfak");
                infoUrun[5].urunMarsYazicilari.Add(new List<string>());

                infoUrun[5].urunAdi.Add("Bonfrit Patates");
                infoUrun[5].urunPorsiyonFiyati.Add("13,00");
                infoUrun[5].urunKiloFiyati.Add("0,00");
                infoUrun[5].urunTuru.Add("Porsiyon");
                infoUrun[5].urunKategorisi.Add("Aperatifler");
                infoUrun[5].urunAciklamasi.Add("Barbekü sos");
                infoUrun[5].urunKDV.Add(8);
                infoUrun[5].urunPorsiyonSinifi.Add(0);
                infoUrun[5].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[5].urunBarkodu.Add("");
                infoUrun[5].urunYazicisi.Add("Mutfak");
                infoUrun[5].urunMarsYazicilari.Add(new List<string>());

                infoUrun[5].urunAdi.Add("Güveçte Kaşarlı Mantar");
                infoUrun[5].urunPorsiyonFiyati.Add("12,75");
                infoUrun[5].urunKiloFiyati.Add("0,00");
                infoUrun[5].urunTuru.Add("Porsiyon");
                infoUrun[5].urunKategorisi.Add("Aperatifler");
                infoUrun[5].urunAciklamasi.Add("");
                infoUrun[5].urunKDV.Add(8);
                infoUrun[5].urunPorsiyonSinifi.Add(0);
                infoUrun[5].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[5].urunBarkodu.Add("");
                infoUrun[5].urunYazicisi.Add("Mutfak");
                infoUrun[5].urunMarsYazicilari.Add(new List<string>());

                infoUrun[5].urunAdi.Add("Elma Dilim Patates");
                infoUrun[5].urunPorsiyonFiyati.Add("13,25");
                infoUrun[5].urunKiloFiyati.Add("0,00");
                infoUrun[5].urunTuru.Add("Porsiyon");
                infoUrun[5].urunKategorisi.Add("Aperatifler");
                infoUrun[5].urunAciklamasi.Add("");
                infoUrun[5].urunKDV.Add(8);
                infoUrun[5].urunPorsiyonSinifi.Add(0);
                infoUrun[5].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[5].urunBarkodu.Add("");
                infoUrun[5].urunYazicisi.Add("Mutfak");
                infoUrun[5].urunMarsYazicilari.Add(new List<string>());

                infoUrun[6].urunAdi.Add("Taco Home Salata");
                infoUrun[6].urunPorsiyonFiyati.Add("21,00");
                infoUrun[6].urunKategorisi.Add("Salatalar");
                infoUrun[6].urunKiloFiyati.Add("0,00");
                infoUrun[6].urunTuru.Add("Porsiyon");
                infoUrun[6].urunAciklamasi.Add("Taco içerisinde mevsim yeşillikleri, meksika fasülyesi, siyah zeytin, domates, salatalık, soya filizi, baby mısır, mantar, bonfile, kaşar, tobasco sos");
                infoUrun[6].urunKDV.Add(8);
                infoUrun[6].urunPorsiyonSinifi.Add(0);
                infoUrun[6].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[6].urunBarkodu.Add("");
                infoUrun[6].urunYazicisi.Add("Mutfak");
                infoUrun[6].urunMarsYazicilari.Add(new List<string>());

                infoUrun[6].urunAdi.Add("Mevsim Salata");
                infoUrun[6].urunPorsiyonFiyati.Add("16,75");
                infoUrun[6].urunKiloFiyati.Add("0,00");
                infoUrun[6].urunTuru.Add("Porsiyon");
                infoUrun[6].urunKategorisi.Add("Salatalar");
                infoUrun[6].urunAciklamasi.Add("Mevsim yeşillikleri, havuç, kırmızı lahana, domates, salatalık, soya filizi, baby mısır, zeytin, beyaz peynir, zeytinyağı ve limon");
                infoUrun[6].urunKDV.Add(8);
                infoUrun[6].urunPorsiyonSinifi.Add(0);
                infoUrun[6].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[6].urunBarkodu.Add("");
                infoUrun[6].urunYazicisi.Add("Mutfak");
                infoUrun[6].urunMarsYazicilari.Add(new List<string>());

                infoUrun[6].urunAdi.Add("Ton Balıklı Salata");
                infoUrun[6].urunPorsiyonFiyati.Add("19,25");
                infoUrun[6].urunKiloFiyati.Add("0,00");
                infoUrun[6].urunTuru.Add("Porsiyon");
                infoUrun[6].urunKategorisi.Add("Salatalar");
                infoUrun[6].urunAciklamasi.Add("Mevsim yeşillikleri üzerine ton balığı, haşlanmış yumurta, közlenmiş biber, soğan, domates, salatalık, kapari çiçeği, siyah zeytin");
                infoUrun[6].urunKDV.Add(8);
                infoUrun[6].urunPorsiyonSinifi.Add(0);
                infoUrun[6].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[6].urunBarkodu.Add("");
                infoUrun[6].urunYazicisi.Add("Mutfak");
                infoUrun[6].urunMarsYazicilari.Add(new List<string>());

                infoUrun[6].urunAdi.Add("Çok Peynirli Salata");
                infoUrun[6].urunPorsiyonFiyati.Add("19,25");
                infoUrun[6].urunKiloFiyati.Add("0,00");
                infoUrun[6].urunTuru.Add("Porsiyon");
                infoUrun[6].urunKategorisi.Add("Salatalar");
                infoUrun[6].urunAciklamasi.Add("Karışık yeşillik, domates, salatalık, köz biber, siyah zeytin, ceviz, ezme peyniri, kaşar, dil peyniri, taze nane, feşleğen, pesto sos");
                infoUrun[6].urunKDV.Add(8);
                infoUrun[6].urunPorsiyonSinifi.Add(0);
                infoUrun[6].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[6].urunBarkodu.Add("");
                infoUrun[6].urunYazicisi.Add("Mutfak");
                infoUrun[6].urunMarsYazicilari.Add(new List<string>());

                infoUrun[6].urunAdi.Add("Sezar Salata");
                infoUrun[6].urunPorsiyonFiyati.Add("19,25");
                infoUrun[6].urunKiloFiyati.Add("0,00");
                infoUrun[6].urunTuru.Add("Porsiyon");
                infoUrun[6].urunKategorisi.Add("Salatalar");
                infoUrun[6].urunAciklamasi.Add("Iceberg, ızgara tavuk parçaları, sarımsaklı graten ekmek, tane mısır, sezar sos ve parmesan peyniri");
                infoUrun[6].urunKDV.Add(8);
                infoUrun[6].urunPorsiyonSinifi.Add(0);
                infoUrun[6].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[6].urunBarkodu.Add("");
                infoUrun[6].urunYazicisi.Add("Mutfak");
                infoUrun[6].urunMarsYazicilari.Add(new List<string>());

                infoUrun[6].urunAdi.Add("Hellim Peynirli Salata");
                infoUrun[6].urunPorsiyonFiyati.Add("19,25");
                infoUrun[6].urunKiloFiyati.Add("0,00");
                infoUrun[6].urunTuru.Add("Porsiyon");
                infoUrun[6].urunKategorisi.Add("Salatalar");
                infoUrun[6].urunAciklamasi.Add("Iceberg, akdeniz yeşillikleri, kornişon turşu, cherry domates, kuru kayısı, ızgara hellim peyniri, pesto sos");
                infoUrun[6].urunKDV.Add(8);
                infoUrun[6].urunPorsiyonSinifi.Add(0);
                infoUrun[6].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[6].urunBarkodu.Add("");
                infoUrun[6].urunYazicisi.Add("Mutfak");
                infoUrun[6].urunMarsYazicilari.Add(new List<string>());

                infoUrun[6].urunAdi.Add("Tavuklu Bademli Salata");
                infoUrun[6].urunPorsiyonFiyati.Add("20,50");
                infoUrun[6].urunKiloFiyati.Add("0,00");
                infoUrun[6].urunTuru.Add("Porsiyon");
                infoUrun[6].urunKategorisi.Add("Salatalar");
                infoUrun[6].urunAciklamasi.Add("Iceberg, akdeniz yeşillikleri, havuç, meksika fasülyesi, tane mısır, küp beyaz peynir, cherry domates, küp tavuk, soya sosu, zeytinyağı ve limon sos");
                infoUrun[6].urunKDV.Add(8);
                infoUrun[6].urunPorsiyonSinifi.Add(0);
                infoUrun[6].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[6].urunBarkodu.Add("");
                infoUrun[6].urunYazicisi.Add("Mutfak");
                infoUrun[6].urunMarsYazicilari.Add(new List<string>());

                infoUrun[6].urunAdi.Add("Deniz Mahsülleri Salata");
                infoUrun[6].urunPorsiyonFiyati.Add("20,50");
                infoUrun[6].urunKiloFiyati.Add("0,00");
                infoUrun[6].urunTuru.Add("Porsiyon");
                infoUrun[6].urunKategorisi.Add("Salatalar");
                infoUrun[6].urunAciklamasi.Add("Iceberg, akdeniz yeşillikleri, kornişon tursu, havuç, mısır, kırmızı soğan, cherry domates, karides, mezgit, yengeç, parmesan peyniri, zeytinyağı ve limon sos");
                infoUrun[6].urunKDV.Add(8);
                infoUrun[6].urunPorsiyonSinifi.Add(0);
                infoUrun[6].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[6].urunBarkodu.Add("");
                infoUrun[6].urunYazicisi.Add("Mutfak");
                infoUrun[6].urunMarsYazicilari.Add(new List<string>());

                infoUrun[6].urunAdi.Add("Lor Peynirli Roka Salata");
                infoUrun[6].urunPorsiyonFiyati.Add("18,50");
                infoUrun[6].urunKiloFiyati.Add("0,00");
                infoUrun[6].urunTuru.Add("Porsiyon");
                infoUrun[6].urunKategorisi.Add("Salatalar");
                infoUrun[6].urunAciklamasi.Add("Roka, ceviz, domates, lor peyniri, portakal ve balzamik sos");
                infoUrun[6].urunKDV.Add(8);
                infoUrun[6].urunPorsiyonSinifi.Add(0);
                infoUrun[6].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[6].urunBarkodu.Add("");
                infoUrun[6].urunYazicisi.Add("Mutfak");
                infoUrun[6].urunMarsYazicilari.Add(new List<string>());

                infoUrun[6].urunAdi.Add("Tavuklu Peynirli Salata");
                infoUrun[6].urunPorsiyonFiyati.Add("19,25");
                infoUrun[6].urunKiloFiyati.Add("0,00");
                infoUrun[6].urunTuru.Add("Porsiyon");
                infoUrun[6].urunKategorisi.Add("Salatalar");
                infoUrun[6].urunAciklamasi.Add("Iceberg, kaşar, ceviz, ızgara tavuk, cherry domates, kızarmış susam ve özel krema sos");
                infoUrun[6].urunKDV.Add(8);
                infoUrun[6].urunPorsiyonSinifi.Add(0);
                infoUrun[6].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[6].urunBarkodu.Add("");
                infoUrun[6].urunYazicisi.Add("Mutfak");
                infoUrun[6].urunMarsYazicilari.Add(new List<string>());

                infoUrun[6].urunAdi.Add("Izgara Biftekli Salata");
                infoUrun[6].urunPorsiyonFiyati.Add("20,50");
                infoUrun[6].urunKiloFiyati.Add("0,00");
                infoUrun[6].urunTuru.Add("Porsiyon");
                infoUrun[6].urunKategorisi.Add("Salatalar");
                infoUrun[6].urunAciklamasi.Add("Dilimlenmiş ve marine edilmiş ızgara biftek, iceberg, mevsim yeşillikleri, domates, salatalık, turşu, zeytin, havuç ve hardallı krema sos");
                infoUrun[6].urunKDV.Add(8);
                infoUrun[6].urunPorsiyonSinifi.Add(0);
                infoUrun[6].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[6].urunBarkodu.Add("");
                infoUrun[6].urunYazicisi.Add("Mutfak");
                infoUrun[6].urunMarsYazicilari.Add(new List<string>());

                infoUrun[6].urunAdi.Add("Kısırlı Köylü Salata");
                infoUrun[6].urunPorsiyonFiyati.Add("17,25");
                infoUrun[6].urunKiloFiyati.Add("0,00");
                infoUrun[6].urunTuru.Add("Porsiyon");
                infoUrun[6].urunKategorisi.Add("Salatalar");
                infoUrun[6].urunAciklamasi.Add("Mevsim yeşillikleri, domates, salatalık, kornişon turşu, siyah zeytin, köz biber, havuç, beyaz peynir, kısır, çörekokut, zeytinyağı ve nar ekşisi sos");
                infoUrun[6].urunKDV.Add(8);
                infoUrun[6].urunPorsiyonSinifi.Add(0);
                infoUrun[6].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[6].urunBarkodu.Add("");
                infoUrun[6].urunYazicisi.Add("Mutfak");
                infoUrun[6].urunMarsYazicilari.Add(new List<string>());

                infoUrun[6].urunAdi.Add("Susamlı Fingers Salata");
                infoUrun[6].urunPorsiyonFiyati.Add("20,00");
                infoUrun[6].urunKiloFiyati.Add("0,00");
                infoUrun[6].urunTuru.Add("Porsiyon");
                infoUrun[6].urunKategorisi.Add("Salatalar");
                infoUrun[6].urunAciklamasi.Add("");
                infoUrun[6].urunKDV.Add(8);
                infoUrun[6].urunPorsiyonSinifi.Add(0);
                infoUrun[6].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[6].urunBarkodu.Add("");
                infoUrun[6].urunYazicisi.Add("Mutfak");
                infoUrun[6].urunMarsYazicilari.Add(new List<string>());

                infoUrun[6].urunAdi.Add("Roma Salatası");
                infoUrun[6].urunPorsiyonFiyati.Add("19,00");
                infoUrun[6].urunKiloFiyati.Add("0,00");
                infoUrun[6].urunTuru.Add("Porsiyon");
                infoUrun[6].urunKategorisi.Add("Salatalar");
                infoUrun[6].urunAciklamasi.Add("");
                infoUrun[6].urunKDV.Add(8);
                infoUrun[6].urunPorsiyonSinifi.Add(0);
                infoUrun[6].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[6].urunBarkodu.Add("");
                infoUrun[6].urunYazicisi.Add("Mutfak");
                infoUrun[6].urunMarsYazicilari.Add(new List<string>());

                infoUrun[6].urunAdi.Add("Keçi Peynirli Köylü Salata");
                infoUrun[6].urunPorsiyonFiyati.Add("18,00");
                infoUrun[6].urunKiloFiyati.Add("0,00");
                infoUrun[6].urunTuru.Add("Porsiyon");
                infoUrun[6].urunKategorisi.Add("Salatalar");
                infoUrun[6].urunAciklamasi.Add("");
                infoUrun[6].urunKDV.Add(8);
                infoUrun[6].urunPorsiyonSinifi.Add(0);
                infoUrun[6].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[6].urunBarkodu.Add("");
                infoUrun[6].urunYazicisi.Add("Mutfak");
                infoUrun[6].urunMarsYazicilari.Add(new List<string>());

                infoUrun[6].urunAdi.Add("Enginar Kalbi Salata");
                infoUrun[6].urunPorsiyonFiyati.Add("17,50");
                infoUrun[6].urunKiloFiyati.Add("0,00");
                infoUrun[6].urunTuru.Add("Porsiyon");
                infoUrun[6].urunKategorisi.Add("Salatalar");
                infoUrun[6].urunAciklamasi.Add("");
                infoUrun[6].urunKDV.Add(8);
                infoUrun[6].urunPorsiyonSinifi.Add(0);
                infoUrun[6].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[6].urunBarkodu.Add("");
                infoUrun[6].urunYazicisi.Add("Mutfak");
                infoUrun[6].urunMarsYazicilari.Add(new List<string>());

                infoUrun[6].urunAdi.Add("Bahçe Salatası");
                infoUrun[6].urunPorsiyonFiyati.Add("17,50");
                infoUrun[6].urunKiloFiyati.Add("0,00");
                infoUrun[6].urunTuru.Add("Porsiyon");
                infoUrun[6].urunKategorisi.Add("Salatalar");
                infoUrun[6].urunAciklamasi.Add("");
                infoUrun[6].urunKDV.Add(8);
                infoUrun[6].urunPorsiyonSinifi.Add(0);
                infoUrun[6].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[6].urunBarkodu.Add("");
                infoUrun[6].urunYazicisi.Add("Mutfak");
                infoUrun[6].urunMarsYazicilari.Add(new List<string>());

                infoUrun[7].urunAdi.Add("Texas Burger");
                infoUrun[7].urunPorsiyonFiyati.Add("17,25");
                infoUrun[7].urunKiloFiyati.Add("0,00");
                infoUrun[7].urunTuru.Add("Porsiyon");
                infoUrun[7].urunKategorisi.Add("Burgerler");
                infoUrun[7].urunAciklamasi.Add("Lav taşında pişmiş hamburger köftesi, yeşillik, turşu domates, özel texas sos, burger peyniri, soğan ve patates");
                infoUrun[7].urunKDV.Add(8);
                infoUrun[7].urunPorsiyonSinifi.Add(0);
                infoUrun[7].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[7].urunBarkodu.Add("");
                infoUrun[7].urunYazicisi.Add("Mutfak");
                infoUrun[7].urunMarsYazicilari.Add(new List<string>());

                infoUrun[7].urunAdi.Add("Köfte Burger");
                infoUrun[7].urunPorsiyonFiyati.Add("17,25");
                infoUrun[7].urunKiloFiyati.Add("0,00");
                infoUrun[7].urunTuru.Add("Porsiyon");
                infoUrun[7].urunKategorisi.Add("Burgerler");
                infoUrun[7].urunAciklamasi.Add("Lav taşında pişmiş ızgara köfte, rus salatası, turşu, domates, yeşillik soğan ve patates");
                infoUrun[7].urunKDV.Add(8);
                infoUrun[7].urunPorsiyonSinifi.Add(0);
                infoUrun[7].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[7].urunBarkodu.Add("");
                infoUrun[7].urunYazicisi.Add("Mutfak");
                infoUrun[7].urunMarsYazicilari.Add(new List<string>());

                infoUrun[7].urunAdi.Add("Steak Burger");
                infoUrun[7].urunPorsiyonFiyati.Add("17,00");
                infoUrun[7].urunKiloFiyati.Add("0,00");
                infoUrun[7].urunTuru.Add("Porsiyon");
                infoUrun[7].urunKategorisi.Add("Burgerler");
                infoUrun[7].urunAciklamasi.Add("Lav taşında pişmiş biftek, rus salatası, yeşillik, soğan, domates ve patates");
                infoUrun[7].urunKDV.Add(8);
                infoUrun[7].urunPorsiyonSinifi.Add(0);
                infoUrun[7].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[7].urunBarkodu.Add("");
                infoUrun[7].urunYazicisi.Add("Mutfak");
                infoUrun[7].urunMarsYazicilari.Add(new List<string>());

                infoUrun[7].urunAdi.Add("Cheeseburger");
                infoUrun[7].urunPorsiyonFiyati.Add("15,60");
                infoUrun[7].urunKiloFiyati.Add("0,00");
                infoUrun[7].urunTuru.Add("Porsiyon");
                infoUrun[7].urunKategorisi.Add("Burgerler");
                infoUrun[7].urunAciklamasi.Add("");
                infoUrun[7].urunKDV.Add(8);
                infoUrun[7].urunPorsiyonSinifi.Add(0);
                infoUrun[7].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[7].urunBarkodu.Add("");
                infoUrun[7].urunYazicisi.Add("Mutfak");
                infoUrun[7].urunMarsYazicilari.Add(new List<string>());

                infoUrun[7].urunAdi.Add("Hamburger");
                infoUrun[7].urunPorsiyonFiyati.Add("15,00");
                infoUrun[7].urunKiloFiyati.Add("0,00");
                infoUrun[7].urunTuru.Add("Porsiyon");
                infoUrun[7].urunKategorisi.Add("Burgerler");
                infoUrun[7].urunAciklamasi.Add("Lav taşında pişmiş hamburger köftesi, rus salatası, yeşillik, soğan, domates, turşu ve patates");
                infoUrun[7].urunKDV.Add(8);
                infoUrun[7].urunPorsiyonSinifi.Add(0);
                infoUrun[7].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[7].urunBarkodu.Add("");
                infoUrun[7].urunYazicisi.Add("Mutfak");
                infoUrun[7].urunMarsYazicilari.Add(new List<string>());

                infoUrun[7].urunAdi.Add("Chicken Burger");
                infoUrun[7].urunPorsiyonFiyati.Add("16,25");
                infoUrun[7].urunKiloFiyati.Add("0,00");
                infoUrun[7].urunTuru.Add("Porsiyon");
                infoUrun[7].urunKategorisi.Add("Burgerler");
                infoUrun[7].urunAciklamasi.Add("");
                infoUrun[7].urunKDV.Add(8);
                infoUrun[7].urunPorsiyonSinifi.Add(0);
                infoUrun[7].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[7].urunBarkodu.Add("");
                infoUrun[7].urunYazicisi.Add("Mutfak");
                infoUrun[7].urunMarsYazicilari.Add(new List<string>());

                infoUrun[7].urunAdi.Add("Üçlü Mini Burger");
                infoUrun[7].urunPorsiyonFiyati.Add("16,50");
                infoUrun[7].urunKiloFiyati.Add("0,00");
                infoUrun[7].urunTuru.Add("Porsiyon");
                infoUrun[7].urunKategorisi.Add("Burgerler");
                infoUrun[7].urunAciklamasi.Add("");
                infoUrun[7].urunKDV.Add(8);
                infoUrun[7].urunPorsiyonSinifi.Add(0);
                infoUrun[7].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[7].urunBarkodu.Add("");
                infoUrun[7].urunYazicisi.Add("Mutfak");
                infoUrun[7].urunMarsYazicilari.Add(new List<string>());

                infoUrun[8].urunAdi.Add("Tavuk Dürüm");
                infoUrun[8].urunPorsiyonFiyati.Add("21,50");
                infoUrun[8].urunKiloFiyati.Add("0,00");
                infoUrun[8].urunTuru.Add("Porsiyon");
                infoUrun[8].urunKategorisi.Add("Dürümler");
                infoUrun[8].urunAciklamasi.Add("");
                infoUrun[8].urunKDV.Add(8);
                infoUrun[8].urunPorsiyonSinifi.Add(0);
                infoUrun[8].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[8].urunBarkodu.Add("");
                infoUrun[8].urunYazicisi.Add("Mutfak");
                infoUrun[8].urunMarsYazicilari.Add(new List<string>());

                infoUrun[8].urunAdi.Add("Sebzeli Dürüm");
                infoUrun[8].urunPorsiyonFiyati.Add("18,50");
                infoUrun[8].urunKiloFiyati.Add("0,00");
                infoUrun[8].urunTuru.Add("Porsiyon");
                infoUrun[8].urunKategorisi.Add("Dürümler");
                infoUrun[8].urunAciklamasi.Add("");
                infoUrun[8].urunKDV.Add(8);
                infoUrun[8].urunPorsiyonSinifi.Add(0);
                infoUrun[8].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[8].urunBarkodu.Add("");
                infoUrun[8].urunYazicisi.Add("Mutfak");
                infoUrun[8].urunMarsYazicilari.Add(new List<string>());

                infoUrun[8].urunAdi.Add("Köfteli Dürüm");
                infoUrun[8].urunPorsiyonFiyati.Add("21,75");
                infoUrun[8].urunKiloFiyati.Add("0,00");
                infoUrun[8].urunTuru.Add("Porsiyon");
                infoUrun[8].urunKategorisi.Add("Dürümler");
                infoUrun[8].urunAciklamasi.Add("");
                infoUrun[8].urunKDV.Add(8);
                infoUrun[8].urunPorsiyonSinifi.Add(0);
                infoUrun[8].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[8].urunBarkodu.Add("");
                infoUrun[8].urunYazicisi.Add("Mutfak");
                infoUrun[8].urunMarsYazicilari.Add(new List<string>());

                infoUrun[8].urunAdi.Add("Etli Dürüm");
                infoUrun[8].urunPorsiyonFiyati.Add("23,00");
                infoUrun[8].urunKiloFiyati.Add("0,00");
                infoUrun[8].urunTuru.Add("Porsiyon");
                infoUrun[8].urunKategorisi.Add("Dürümler");
                infoUrun[8].urunAciklamasi.Add("");
                infoUrun[8].urunKDV.Add(8);
                infoUrun[8].urunPorsiyonSinifi.Add(0);
                infoUrun[8].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[8].urunBarkodu.Add("");
                infoUrun[8].urunYazicisi.Add("Mutfak");
                infoUrun[8].urunMarsYazicilari.Add(new List<string>());

                infoUrun[9].urunAdi.Add("Ton Balıklı Kumpir");
                infoUrun[9].urunPorsiyonFiyati.Add("15,50");
                infoUrun[9].urunKiloFiyati.Add("0,00");
                infoUrun[9].urunTuru.Add("Porsiyon");
                infoUrun[9].urunKategorisi.Add("Kumpir");
                infoUrun[9].urunAciklamasi.Add("Tereyağı, kaşar, ton balığı, tane mısır, turşu, zeytin");
                infoUrun[9].urunKDV.Add(8);
                infoUrun[9].urunPorsiyonSinifi.Add(0);
                infoUrun[9].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[9].urunBarkodu.Add("");
                infoUrun[9].urunYazicisi.Add("Mutfak");
                infoUrun[9].urunMarsYazicilari.Add(new List<string>());

                infoUrun[9].urunAdi.Add("Sosisli Kumpir");
                infoUrun[9].urunPorsiyonFiyati.Add("14,50");
                infoUrun[9].urunKiloFiyati.Add("0,00");
                infoUrun[9].urunTuru.Add("Porsiyon");
                infoUrun[9].urunKategorisi.Add("Kumpir");
                infoUrun[9].urunAciklamasi.Add("Tereyağı, kaşar, sosis, tane mısır, zeytin, turşu, rus salatası");
                infoUrun[9].urunKDV.Add(8);
                infoUrun[9].urunPorsiyonSinifi.Add(0);
                infoUrun[9].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[9].urunBarkodu.Add("");
                infoUrun[9].urunYazicisi.Add("Mutfak");
                infoUrun[9].urunMarsYazicilari.Add(new List<string>());

                infoUrun[9].urunAdi.Add("Kaşarlı Kumpir");
                infoUrun[9].urunPorsiyonFiyati.Add("13,50");
                infoUrun[9].urunKiloFiyati.Add("0,00");
                infoUrun[9].urunTuru.Add("Porsiyon");
                infoUrun[9].urunKategorisi.Add("Kumpir");
                infoUrun[9].urunAciklamasi.Add("Tereyağı, kaşar, tane mısır, turşu, zeytin");
                infoUrun[9].urunKDV.Add(8);
                infoUrun[9].urunPorsiyonSinifi.Add(0);
                infoUrun[9].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[9].urunBarkodu.Add("");
                infoUrun[9].urunYazicisi.Add("Mutfak");
                infoUrun[9].urunMarsYazicilari.Add(new List<string>());

                infoUrun[9].urunAdi.Add("Kıtır Piliç Kumpir");
                infoUrun[9].urunPorsiyonFiyati.Add("15,50");
                infoUrun[9].urunKiloFiyati.Add("0,00");
                infoUrun[9].urunTuru.Add("Porsiyon");
                infoUrun[9].urunKategorisi.Add("Kumpir");
                infoUrun[9].urunAciklamasi.Add("");
                infoUrun[9].urunKDV.Add(8);
                infoUrun[9].urunPorsiyonSinifi.Add(0);
                infoUrun[9].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[9].urunBarkodu.Add("");
                infoUrun[9].urunYazicisi.Add("Mutfak");
                infoUrun[9].urunMarsYazicilari.Add(new List<string>());

                infoUrun[9].urunAdi.Add("Karışık Kumpir");
                infoUrun[9].urunPorsiyonFiyati.Add("14,75");
                infoUrun[9].urunKiloFiyati.Add("0,00");
                infoUrun[9].urunTuru.Add("Porsiyon");
                infoUrun[9].urunKategorisi.Add("Kumpir");
                infoUrun[9].urunAciklamasi.Add("");
                infoUrun[9].urunKDV.Add(8);
                infoUrun[9].urunPorsiyonSinifi.Add(0);
                infoUrun[9].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[9].urunBarkodu.Add("");
                infoUrun[9].urunYazicisi.Add("Mutfak");
                infoUrun[9].urunMarsYazicilari.Add(new List<string>());

                infoUrun[9].urunAdi.Add("Acılı Kumpir");
                infoUrun[9].urunPorsiyonFiyati.Add("13,50");
                infoUrun[9].urunKiloFiyati.Add("0,00");
                infoUrun[9].urunTuru.Add("Porsiyon");
                infoUrun[9].urunKategorisi.Add("Kumpir");
                infoUrun[9].urunAciklamasi.Add("");
                infoUrun[9].urunKDV.Add(8);
                infoUrun[9].urunPorsiyonSinifi.Add(0);
                infoUrun[9].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[9].urunBarkodu.Add("");
                infoUrun[9].urunYazicisi.Add("Mutfak");
                infoUrun[9].urunMarsYazicilari.Add(new List<string>());

                infoUrun[10].urunAdi.Add("Sebzeli Capellini");
                infoUrun[10].urunPorsiyonFiyati.Add("19,75");
                infoUrun[10].urunKiloFiyati.Add("0,00");
                infoUrun[10].urunTuru.Add("Porsiyon");
                infoUrun[10].urunKategorisi.Add("Makarnalar");
                infoUrun[10].urunAciklamasi.Add("Capellini makarna, kurutulmuş domates, sarımsak püresi, soya sos ve taze yeşil soğan");
                infoUrun[10].urunKDV.Add(8);
                infoUrun[10].urunPorsiyonSinifi.Add(0);
                infoUrun[10].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[10].urunBarkodu.Add("");
                infoUrun[10].urunYazicisi.Add("Mutfak");
                infoUrun[10].urunMarsYazicilari.Add(new List<string>());

                infoUrun[10].urunAdi.Add("Shiitake Mantarlı Casarecce");
                infoUrun[10].urunPorsiyonFiyati.Add("20,50");
                infoUrun[10].urunKiloFiyati.Add("0,00");
                infoUrun[10].urunTuru.Add("Porsiyon");
                infoUrun[10].urunKategorisi.Add("Makarnalar");
                infoUrun[10].urunAciklamasi.Add("Casarecce makarna, mantar, sarımsak, sh,,take mantarı, kremai, domastes kurusu ve parmesan peyniri");
                infoUrun[10].urunKDV.Add(8);
                infoUrun[10].urunPorsiyonSinifi.Add(0);
                infoUrun[10].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[10].urunBarkodu.Add("");
                infoUrun[10].urunYazicisi.Add("Mutfak");
                infoUrun[10].urunMarsYazicilari.Add(new List<string>());

                infoUrun[10].urunAdi.Add("Biftekli Fettucini");
                infoUrun[10].urunPorsiyonFiyati.Add("22,50");
                infoUrun[10].urunKiloFiyati.Add("0,00");
                infoUrun[10].urunTuru.Add("Porsiyon");
                infoUrun[10].urunKategorisi.Add("Makarnalar");
                infoUrun[10].urunAciklamasi.Add("Biftek, demiglace sos, krema ve parmesan peyniri");
                infoUrun[10].urunKDV.Add(8);
                infoUrun[10].urunPorsiyonSinifi.Add(0);
                infoUrun[10].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[10].urunBarkodu.Add("");
                infoUrun[10].urunYazicisi.Add("Mutfak");
                infoUrun[10].urunMarsYazicilari.Add(new List<string>());

                infoUrun[10].urunAdi.Add("Üç Renkli 3 Lezzetli Tortellini");
                infoUrun[10].urunPorsiyonFiyati.Add("21,00");
                infoUrun[10].urunKiloFiyati.Add("0,00");
                infoUrun[10].urunTuru.Add("Porsiyon");
                infoUrun[10].urunKategorisi.Add("Makarnalar");
                infoUrun[10].urunAciklamasi.Add("Teryağında sarımsakla sotelenmiş ıspanak, domates, sade tortellini, krem sos, domates sos ve labne peyniri");
                infoUrun[10].urunKDV.Add(8);
                infoUrun[10].urunPorsiyonSinifi.Add(0);
                infoUrun[10].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[10].urunBarkodu.Add("");
                infoUrun[10].urunYazicisi.Add("Mutfak");
                infoUrun[10].urunMarsYazicilari.Add(new List<string>());

                infoUrun[10].urunAdi.Add("Spaghetti Napoliten");
                infoUrun[10].urunPorsiyonFiyati.Add("19,25");
                infoUrun[10].urunKiloFiyati.Add("0,00");
                infoUrun[10].urunTuru.Add("Porsiyon");
                infoUrun[10].urunKategorisi.Add("Makarnalar");
                infoUrun[10].urunAciklamasi.Add("Domates sos ve kaşar peyniri");
                infoUrun[10].urunKDV.Add(8);
                infoUrun[10].urunPorsiyonSinifi.Add(0);
                infoUrun[10].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[10].urunBarkodu.Add("");
                infoUrun[10].urunYazicisi.Add("Mutfak");
                infoUrun[10].urunMarsYazicilari.Add(new List<string>());

                infoUrun[10].urunAdi.Add("Spaghetti Bolonez");
                infoUrun[10].urunPorsiyonFiyati.Add("21,00");
                infoUrun[10].urunKiloFiyati.Add("0,00");
                infoUrun[10].urunTuru.Add("Porsiyon");
                infoUrun[10].urunKategorisi.Add("Makarnalar");
                infoUrun[10].urunAciklamasi.Add("");
                infoUrun[10].urunKDV.Add(8);
                infoUrun[10].urunPorsiyonSinifi.Add(0);
                infoUrun[10].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[10].urunBarkodu.Add("");
                infoUrun[10].urunYazicisi.Add("Mutfak");
                infoUrun[10].urunMarsYazicilari.Add(new List<string>());

                infoUrun[10].urunAdi.Add("Tavuklu Noodle");
                infoUrun[10].urunPorsiyonFiyati.Add("21,75");
                infoUrun[10].urunKiloFiyati.Add("0,00");
                infoUrun[10].urunTuru.Add("Porsiyon");
                infoUrun[10].urunKategorisi.Add("Makarnalar");
                infoUrun[10].urunAciklamasi.Add("Julyen tavuk, havuç, kabak, soya filizi, renkli biber ve soya sosu");
                infoUrun[10].urunKDV.Add(8);
                infoUrun[10].urunPorsiyonSinifi.Add(0);
                infoUrun[10].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[10].urunBarkodu.Add("");
                infoUrun[10].urunYazicisi.Add("Mutfak");
                infoUrun[10].urunMarsYazicilari.Add(new List<string>());

                infoUrun[10].urunAdi.Add("Karidesli Noodle");
                infoUrun[10].urunPorsiyonFiyati.Add("21,75");
                infoUrun[10].urunKiloFiyati.Add("0,00");
                infoUrun[10].urunTuru.Add("Porsiyon");
                infoUrun[10].urunKategorisi.Add("Makarnalar");
                infoUrun[10].urunAciklamasi.Add("Julyen havuç, kabak, soya filizi, renkli biber, karides ve soya sosu");
                infoUrun[10].urunKDV.Add(8);
                infoUrun[10].urunPorsiyonSinifi.Add(0);
                infoUrun[10].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[10].urunBarkodu.Add("");
                infoUrun[10].urunYazicisi.Add("Mutfak");
                infoUrun[10].urunMarsYazicilari.Add(new List<string>());

                infoUrun[10].urunAdi.Add("Lazanya");
                infoUrun[10].urunPorsiyonFiyati.Add("21,50");
                infoUrun[10].urunKiloFiyati.Add("0,00");
                infoUrun[10].urunTuru.Add("Porsiyon");
                infoUrun[10].urunKategorisi.Add("Makarnalar");
                infoUrun[10].urunAciklamasi.Add("Güveçte lazanya yaprakları arasına ,ağır ateşte pişmiş kıymalı domates sos ve bol kaşar");
                infoUrun[10].urunKDV.Add(8);
                infoUrun[10].urunPorsiyonSinifi.Add(0);
                infoUrun[10].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[10].urunBarkodu.Add("");
                infoUrun[10].urunYazicisi.Add("Mutfak");
                infoUrun[10].urunMarsYazicilari.Add(new List<string>());

                infoUrun[10].urunAdi.Add("Tulum Peynirli Cevizli Köy Eriştesi");
                infoUrun[10].urunPorsiyonFiyati.Add("18,75");
                infoUrun[10].urunKiloFiyati.Add("0,00");
                infoUrun[10].urunTuru.Add("Porsiyon");
                infoUrun[10].urunKategorisi.Add("Makarnalar");
                infoUrun[10].urunAciklamasi.Add("Tereyağında tatlandırılmış köy eriştesi, kırık ceviz ve üzerinde tulum peyniri");
                infoUrun[10].urunKDV.Add(8);
                infoUrun[10].urunPorsiyonSinifi.Add(0);
                infoUrun[10].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[10].urunBarkodu.Add("");
                infoUrun[10].urunYazicisi.Add("Mutfak");
                infoUrun[10].urunMarsYazicilari.Add(new List<string>());

                infoUrun[10].urunAdi.Add("Biftekli Penne Polo");
                infoUrun[10].urunPorsiyonFiyati.Add("21,50");
                infoUrun[10].urunKiloFiyati.Add("0,00");
                infoUrun[10].urunTuru.Add("Porsiyon");
                infoUrun[10].urunKategorisi.Add("Makarnalar");
                infoUrun[10].urunAciklamasi.Add("Biftek, pesto sos, krema, parmesan peyniri");
                infoUrun[10].urunKDV.Add(8);
                infoUrun[10].urunPorsiyonSinifi.Add(0);
                infoUrun[10].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[10].urunBarkodu.Add("");
                infoUrun[10].urunYazicisi.Add("Mutfak");
                infoUrun[10].urunMarsYazicilari.Add(new List<string>());

                infoUrun[10].urunAdi.Add("Sebzeli Yöre Makarnası");
                infoUrun[10].urunPorsiyonFiyati.Add("19,00");
                infoUrun[10].urunKiloFiyati.Add("0,00");
                infoUrun[10].urunTuru.Add("Porsiyon");
                infoUrun[10].urunKategorisi.Add("Makarnalar");
                infoUrun[10].urunAciklamasi.Add("Julyen tavuk ızgara, kabak, havuç, soya filizi, köy eriştesi, köri baharatı ve soya sos");
                infoUrun[10].urunKDV.Add(8);
                infoUrun[10].urunPorsiyonSinifi.Add(0);
                infoUrun[10].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[10].urunBarkodu.Add("");
                infoUrun[10].urunYazicisi.Add("Mutfak");
                infoUrun[10].urunMarsYazicilari.Add(new List<string>());

                infoUrun[10].urunAdi.Add("Porcini Mantarlı Tortellini");
                infoUrun[10].urunPorsiyonFiyati.Add("22,50");
                infoUrun[10].urunKiloFiyati.Add("0,00");
                infoUrun[10].urunTuru.Add("Porsiyon");
                infoUrun[10].urunKategorisi.Add("Makarnalar");
                infoUrun[10].urunAciklamasi.Add("Porcini mantarı, labne peyniri, krema, parmesan peyniri ve krem sos");
                infoUrun[10].urunKDV.Add(8);
                infoUrun[10].urunPorsiyonSinifi.Add(0);
                infoUrun[10].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[10].urunBarkodu.Add("");
                infoUrun[10].urunYazicisi.Add("Mutfak");
                infoUrun[10].urunMarsYazicilari.Add(new List<string>());

                infoUrun[10].urunAdi.Add("Ceviz Dolgulu Margherita");
                infoUrun[10].urunPorsiyonFiyati.Add("21,00");
                infoUrun[10].urunKiloFiyati.Add("0,00");
                infoUrun[10].urunTuru.Add("Porsiyon");
                infoUrun[10].urunKategorisi.Add("Makarnalar");
                infoUrun[10].urunAciklamasi.Add("Ceviz ve peynir dolgulu margherita, sarımsak, ceviz, krema sos ve parmesan peyniri");
                infoUrun[10].urunKDV.Add(8);
                infoUrun[10].urunPorsiyonSinifi.Add(0);
                infoUrun[10].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[10].urunBarkodu.Add("");
                infoUrun[10].urunYazicisi.Add("Mutfak");
                infoUrun[10].urunMarsYazicilari.Add(new List<string>());

                infoUrun[10].urunAdi.Add("Fettucini Alfredo");
                infoUrun[10].urunPorsiyonFiyati.Add("19,50");
                infoUrun[10].urunKiloFiyati.Add("0,00");
                infoUrun[10].urunTuru.Add("Porsiyon");
                infoUrun[10].urunKategorisi.Add("Makarnalar");
                infoUrun[10].urunAciklamasi.Add("Izgara tavuk, dolmalık fıstık, mantar, taze fesleğen, parmesan peyniri ve alfredo sos");
                infoUrun[10].urunKDV.Add(8);
                infoUrun[10].urunPorsiyonSinifi.Add(0);
                infoUrun[10].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[10].urunBarkodu.Add("");
                infoUrun[10].urunYazicisi.Add("Mutfak");
                infoUrun[10].urunMarsYazicilari.Add(new List<string>());

                infoUrun[10].urunAdi.Add("Penne Arabiatta");
                infoUrun[10].urunPorsiyonFiyati.Add("20,00");
                infoUrun[10].urunKiloFiyati.Add("0,00");
                infoUrun[10].urunTuru.Add("Porsiyon");
                infoUrun[10].urunKategorisi.Add("Makarnalar");
                infoUrun[10].urunAciklamasi.Add("");
                infoUrun[10].urunKDV.Add(8);
                infoUrun[10].urunPorsiyonSinifi.Add(0);
                infoUrun[10].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[10].urunBarkodu.Add("");
                infoUrun[10].urunYazicisi.Add("Mutfak");
                infoUrun[10].urunMarsYazicilari.Add(new List<string>());

                infoUrun[10].urunAdi.Add("Fusilli Lunghi");
                infoUrun[10].urunPorsiyonFiyati.Add("19,75");
                infoUrun[10].urunKiloFiyati.Add("0,00");
                infoUrun[10].urunTuru.Add("Porsiyon");
                infoUrun[10].urunKategorisi.Add("Makarnalar");
                infoUrun[10].urunAciklamasi.Add("");
                infoUrun[10].urunKDV.Add(8);
                infoUrun[10].urunPorsiyonSinifi.Add(0);
                infoUrun[10].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[10].urunBarkodu.Add("");
                infoUrun[10].urunYazicisi.Add("Mutfak");
                infoUrun[10].urunMarsYazicilari.Add(new List<string>());

                infoUrun[10].urunAdi.Add("Beş Peynirli Ravioli");
                infoUrun[10].urunPorsiyonFiyati.Add("21,00");
                infoUrun[10].urunKiloFiyati.Add("0,00");
                infoUrun[10].urunTuru.Add("Porsiyon");
                infoUrun[10].urunKategorisi.Add("Makarnalar");
                infoUrun[10].urunAciklamasi.Add("");
                infoUrun[10].urunKDV.Add(8);
                infoUrun[10].urunPorsiyonSinifi.Add(0);
                infoUrun[10].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[10].urunBarkodu.Add("");
                infoUrun[10].urunYazicisi.Add("Mutfak");
                infoUrun[10].urunMarsYazicilari.Add(new List<string>());

                infoUrun[11].urunAdi.Add("Chef Pizza");
                infoUrun[11].urunPorsiyonFiyati.Add("20,00");
                infoUrun[11].urunKiloFiyati.Add("0,00");
                infoUrun[11].urunTuru.Add("Porsiyon");
                infoUrun[11].urunKategorisi.Add("Pizza");
                infoUrun[11].urunAciklamasi.Add("");
                infoUrun[11].urunKDV.Add(8);
                infoUrun[11].urunPorsiyonSinifi.Add(0);
                infoUrun[11].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[11].urunBarkodu.Add("");
                infoUrun[11].urunYazicisi.Add("Mutfak");
                infoUrun[11].urunMarsYazicilari.Add(new List<string>());

                infoUrun[11].urunAdi.Add("Anadolu Pizza");
                infoUrun[11].urunPorsiyonFiyati.Add("17,00");
                infoUrun[11].urunKiloFiyati.Add("0,00");
                infoUrun[11].urunTuru.Add("Porsiyon");
                infoUrun[11].urunKategorisi.Add("Pizza");
                infoUrun[11].urunAciklamasi.Add("");
                infoUrun[11].urunKDV.Add(8);
                infoUrun[11].urunPorsiyonSinifi.Add(0);
                infoUrun[11].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[11].urunBarkodu.Add("");
                infoUrun[11].urunYazicisi.Add("Mutfak");
                infoUrun[11].urunMarsYazicilari.Add(new List<string>());

                infoUrun[12].urunAdi.Add("Ispanaklı Krep");
                infoUrun[12].urunPorsiyonFiyati.Add("17,00");
                infoUrun[12].urunKiloFiyati.Add("0,00");
                infoUrun[12].urunTuru.Add("Porsiyon");
                infoUrun[12].urunKategorisi.Add("Krep");
                infoUrun[12].urunAciklamasi.Add("");
                infoUrun[12].urunKDV.Add(8);
                infoUrun[12].urunPorsiyonSinifi.Add(0);
                infoUrun[12].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[12].urunBarkodu.Add("");
                infoUrun[12].urunYazicisi.Add("Mutfak");
                infoUrun[12].urunMarsYazicilari.Add(new List<string>());

                infoUrun[12].urunAdi.Add("Güveçte Tavuklu Mantarlı Krep");
                infoUrun[12].urunPorsiyonFiyati.Add("14,25");
                infoUrun[12].urunKiloFiyati.Add("0,00");
                infoUrun[12].urunTuru.Add("Porsiyon");
                infoUrun[12].urunKategorisi.Add("Krep");
                infoUrun[12].urunAciklamasi.Add("");
                infoUrun[12].urunKDV.Add(8);
                infoUrun[12].urunPorsiyonSinifi.Add(0);
                infoUrun[12].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[12].urunBarkodu.Add("");
                infoUrun[12].urunYazicisi.Add("Mutfak");
                infoUrun[12].urunMarsYazicilari.Add(new List<string>());

                infoUrun[12].urunAdi.Add("Güveçte Sebzeli Krep");
                infoUrun[12].urunPorsiyonFiyati.Add("14,00");
                infoUrun[12].urunKiloFiyati.Add("0,00");
                infoUrun[12].urunTuru.Add("Porsiyon");
                infoUrun[12].urunKategorisi.Add("Krep");
                infoUrun[12].urunAciklamasi.Add("");
                infoUrun[12].urunKDV.Add(8);
                infoUrun[12].urunPorsiyonSinifi.Add(0);
                infoUrun[12].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[12].urunBarkodu.Add("");
                infoUrun[12].urunYazicisi.Add("Mutfak");
                infoUrun[12].urunMarsYazicilari.Add(new List<string>());

                infoUrun[12].urunAdi.Add("Güveçte Ispanaklı Krep");
                infoUrun[12].urunPorsiyonFiyati.Add("14,00");
                infoUrun[12].urunKiloFiyati.Add("0,00");
                infoUrun[12].urunTuru.Add("Porsiyon");
                infoUrun[12].urunKategorisi.Add("Krep");
                infoUrun[12].urunAciklamasi.Add("");
                infoUrun[12].urunKDV.Add(8);
                infoUrun[12].urunPorsiyonSinifi.Add(0);
                infoUrun[12].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[12].urunBarkodu.Add("");
                infoUrun[12].urunYazicisi.Add("Mutfak");
                infoUrun[12].urunMarsYazicilari.Add(new List<string>());

                infoUrun[12].urunAdi.Add("Güveçte Biftekli Krep");
                infoUrun[12].urunPorsiyonFiyati.Add("14,75");
                infoUrun[12].urunKiloFiyati.Add("0,00");
                infoUrun[12].urunTuru.Add("Porsiyon");
                infoUrun[12].urunKategorisi.Add("Krep");
                infoUrun[12].urunAciklamasi.Add("");
                infoUrun[12].urunKDV.Add(8);
                infoUrun[12].urunPorsiyonSinifi.Add(0);
                infoUrun[12].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[12].urunBarkodu.Add("");
                infoUrun[12].urunYazicisi.Add("Mutfak");
                infoUrun[12].urunMarsYazicilari.Add(new List<string>());

                infoUrun[12].urunAdi.Add("Biftekli Krep");
                infoUrun[12].urunPorsiyonFiyati.Add("17,75");
                infoUrun[12].urunKiloFiyati.Add("0,00");
                infoUrun[12].urunTuru.Add("Porsiyon");
                infoUrun[12].urunKategorisi.Add("Krep");
                infoUrun[12].urunAciklamasi.Add("");
                infoUrun[12].urunKDV.Add(8);
                infoUrun[12].urunPorsiyonSinifi.Add(0);
                infoUrun[12].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[12].urunBarkodu.Add("");
                infoUrun[12].urunYazicisi.Add("Mutfak");
                infoUrun[12].urunMarsYazicilari.Add(new List<string>());

                infoUrun[13].urunAdi.Add("Tikka Soslu Tavuk");
                infoUrun[13].urunPorsiyonFiyati.Add("29,75");
                infoUrun[13].urunKiloFiyati.Add("0,00");
                infoUrun[13].urunTuru.Add("Porsiyon");
                infoUrun[13].urunKategorisi.Add("Ana Yemekler");
                infoUrun[13].urunAciklamasi.Add("");
                infoUrun[13].urunKDV.Add(8);
                infoUrun[13].urunPorsiyonSinifi.Add(0);
                infoUrun[13].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[13].urunBarkodu.Add("");
                infoUrun[13].urunYazicisi.Add("Mutfak");
                infoUrun[13].urunMarsYazicilari.Add(new List<string>());

                infoUrun[13].urunAdi.Add("Oyster Soslu Piliç");
                infoUrun[13].urunPorsiyonFiyati.Add("27,50");
                infoUrun[13].urunKiloFiyati.Add("0,00");
                infoUrun[13].urunTuru.Add("Porsiyon");
                infoUrun[13].urunKategorisi.Add("Ana Yemekler");
                infoUrun[13].urunAciklamasi.Add("");
                infoUrun[13].urunKDV.Add(8);
                infoUrun[13].urunPorsiyonSinifi.Add(0);
                infoUrun[13].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[13].urunBarkodu.Add("");
                infoUrun[13].urunYazicisi.Add("Mutfak");
                infoUrun[13].urunMarsYazicilari.Add(new List<string>());

                infoUrun[13].urunAdi.Add("Mantarlı Fleminyon");
                infoUrun[13].urunPorsiyonFiyati.Add("25,50");
                infoUrun[13].urunKiloFiyati.Add("0,00");
                infoUrun[13].urunTuru.Add("Porsiyon");
                infoUrun[13].urunKategorisi.Add("Ana Yemekler");
                infoUrun[13].urunAciklamasi.Add("");
                infoUrun[13].urunKDV.Add(8);
                infoUrun[13].urunPorsiyonSinifi.Add(0);
                infoUrun[13].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[13].urunBarkodu.Add("");
                infoUrun[13].urunYazicisi.Add("Mutfak");
                infoUrun[13].urunMarsYazicilari.Add(new List<string>());

                infoUrun[13].urunAdi.Add("Liva Steak");
                infoUrun[13].urunPorsiyonFiyati.Add("31,00");
                infoUrun[13].urunKiloFiyati.Add("0,00");
                infoUrun[13].urunTuru.Add("Porsiyon");
                infoUrun[13].urunKategorisi.Add("Ana Yemekler");
                infoUrun[13].urunAciklamasi.Add("");
                infoUrun[13].urunKDV.Add(8);
                infoUrun[13].urunPorsiyonSinifi.Add(0);
                infoUrun[13].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[13].urunBarkodu.Add("");
                infoUrun[13].urunYazicisi.Add("Mutfak");
                infoUrun[13].urunMarsYazicilari.Add(new List<string>());

                infoUrun[13].urunAdi.Add("Rokfor Soslu Peynirli Bonfile");
                infoUrun[13].urunPorsiyonFiyati.Add("34,75");
                infoUrun[13].urunKiloFiyati.Add("0,00");
                infoUrun[13].urunTuru.Add("Porsiyon");
                infoUrun[13].urunKategorisi.Add("Ana Yemekler");
                infoUrun[13].urunAciklamasi.Add("");
                infoUrun[13].urunKDV.Add(8);
                infoUrun[13].urunPorsiyonSinifi.Add(0);
                infoUrun[13].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[13].urunBarkodu.Add("");
                infoUrun[13].urunYazicisi.Add("Mutfak");
                infoUrun[13].urunMarsYazicilari.Add(new List<string>());

                infoUrun[13].urunAdi.Add("Liva Usulü Ispanaklı Schnitzel");
                infoUrun[13].urunPorsiyonFiyati.Add("28,50");
                infoUrun[13].urunKiloFiyati.Add("0,00");
                infoUrun[13].urunTuru.Add("Porsiyon");
                infoUrun[13].urunKategorisi.Add("Ana Yemekler");
                infoUrun[13].urunAciklamasi.Add("");
                infoUrun[13].urunKDV.Add(8);
                infoUrun[13].urunPorsiyonSinifi.Add(0);
                infoUrun[13].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[13].urunBarkodu.Add("");
                infoUrun[13].urunYazicisi.Add("Mutfak");
                infoUrun[13].urunMarsYazicilari.Add(new List<string>());

                infoUrun[13].urunAdi.Add("Jülyen Soslu Tavuk");
                infoUrun[13].urunPorsiyonFiyati.Add("28,75");
                infoUrun[13].urunKiloFiyati.Add("0,00");
                infoUrun[13].urunTuru.Add("Porsiyon");
                infoUrun[13].urunKategorisi.Add("Ana Yemekler");
                infoUrun[13].urunAciklamasi.Add("");
                infoUrun[13].urunKDV.Add(8);
                infoUrun[13].urunPorsiyonSinifi.Add(0);
                infoUrun[13].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[13].urunBarkodu.Add("");
                infoUrun[13].urunYazicisi.Add("Mutfak");
                infoUrun[13].urunMarsYazicilari.Add(new List<string>());

                infoUrun[13].urunAdi.Add("Fırında Mantar Soslu Antrikot");
                infoUrun[13].urunPorsiyonFiyati.Add("32,50");
                infoUrun[13].urunKiloFiyati.Add("0,00");
                infoUrun[13].urunTuru.Add("Porsiyon");
                infoUrun[13].urunKategorisi.Add("Ana Yemekler");
                infoUrun[13].urunAciklamasi.Add("");
                infoUrun[13].urunKDV.Add(8);
                infoUrun[13].urunPorsiyonSinifi.Add(0);
                infoUrun[13].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[13].urunBarkodu.Add("");
                infoUrun[13].urunYazicisi.Add("Mutfak");
                infoUrun[13].urunMarsYazicilari.Add(new List<string>());

                infoUrun[13].urunAdi.Add("Ekşi Tatlı Soslu Tavuk But");
                infoUrun[13].urunPorsiyonFiyati.Add("29,50");
                infoUrun[13].urunKiloFiyati.Add("0,00");
                infoUrun[13].urunTuru.Add("Porsiyon");
                infoUrun[13].urunKategorisi.Add("Ana Yemekler");
                infoUrun[13].urunAciklamasi.Add("");
                infoUrun[13].urunKDV.Add(8);
                infoUrun[13].urunPorsiyonSinifi.Add(0);
                infoUrun[13].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[13].urunBarkodu.Add("");
                infoUrun[13].urunYazicisi.Add("Mutfak");
                infoUrun[13].urunMarsYazicilari.Add(new List<string>());

                infoUrun[13].urunAdi.Add("Cafe De Paris Soslu Antrikot");
                infoUrun[13].urunPorsiyonFiyati.Add("32,50");
                infoUrun[13].urunKiloFiyati.Add("0,00");
                infoUrun[13].urunTuru.Add("Porsiyon");
                infoUrun[13].urunKategorisi.Add("Ana Yemekler");
                infoUrun[13].urunAciklamasi.Add("");
                infoUrun[13].urunKDV.Add(8);
                infoUrun[13].urunPorsiyonSinifi.Add(0);
                infoUrun[13].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[13].urunBarkodu.Add("");
                infoUrun[13].urunYazicisi.Add("Mutfak");
                infoUrun[13].urunMarsYazicilari.Add(new List<string>());

                infoUrun[13].urunAdi.Add("Bonfile Mozerella");
                infoUrun[13].urunPorsiyonFiyati.Add("29,50");
                infoUrun[13].urunKiloFiyati.Add("0,00");
                infoUrun[13].urunTuru.Add("Porsiyon");
                infoUrun[13].urunKategorisi.Add("Ana Yemekler");
                infoUrun[13].urunAciklamasi.Add("");
                infoUrun[13].urunKDV.Add(8);
                infoUrun[13].urunPorsiyonSinifi.Add(0);
                infoUrun[13].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[13].urunBarkodu.Add("");
                infoUrun[13].urunYazicisi.Add("Mutfak");
                infoUrun[13].urunMarsYazicilari.Add(new List<string>());

                infoUrun[13].urunAdi.Add("Arpacık Soğanlı Antrikot");
                infoUrun[13].urunPorsiyonFiyati.Add("32,50");
                infoUrun[13].urunKiloFiyati.Add("0,00");
                infoUrun[13].urunTuru.Add("Porsiyon");
                infoUrun[13].urunKategorisi.Add("Ana Yemekler");
                infoUrun[13].urunAciklamasi.Add("");
                infoUrun[13].urunKDV.Add(8);
                infoUrun[13].urunPorsiyonSinifi.Add(0);
                infoUrun[13].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[13].urunBarkodu.Add("");
                infoUrun[13].urunYazicisi.Add("Mutfak");
                infoUrun[13].urunMarsYazicilari.Add(new List<string>());

                infoUrun[14].urunAdi.Add("Kuzu Pirzola");
                infoUrun[14].urunPorsiyonFiyati.Add("30,50");
                infoUrun[14].urunKiloFiyati.Add("0,00");
                infoUrun[14].urunTuru.Add("Porsiyon");
                infoUrun[14].urunKategorisi.Add("Anadolu Mutfağı");
                infoUrun[14].urunAciklamasi.Add("");
                infoUrun[14].urunKDV.Add(8);
                infoUrun[14].urunPorsiyonSinifi.Add(0);
                infoUrun[14].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[14].urunBarkodu.Add("");
                infoUrun[14].urunYazicisi.Add("Mutfak");
                infoUrun[14].urunMarsYazicilari.Add(new List<string>());

                infoUrun[14].urunAdi.Add("Sac Kavurma");
                infoUrun[14].urunPorsiyonFiyati.Add("28,00");
                infoUrun[14].urunKiloFiyati.Add("0,00");
                infoUrun[14].urunTuru.Add("Porsiyon");
                infoUrun[14].urunKategorisi.Add("Anadolu Mutfağı");
                infoUrun[14].urunAciklamasi.Add("");
                infoUrun[14].urunKDV.Add(8);
                infoUrun[14].urunPorsiyonSinifi.Add(0);
                infoUrun[14].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[14].urunBarkodu.Add("");
                infoUrun[14].urunYazicisi.Add("Mutfak");
                infoUrun[14].urunMarsYazicilari.Add(new List<string>());

                infoUrun[14].urunAdi.Add("Köz Patlıcan Yatağında Kuzu Kavurma");
                infoUrun[14].urunPorsiyonFiyati.Add("30,50");
                infoUrun[14].urunKiloFiyati.Add("0,00");
                infoUrun[14].urunTuru.Add("Porsiyon");
                infoUrun[14].urunKategorisi.Add("Anadolu Mutfağı");
                infoUrun[14].urunAciklamasi.Add("");
                infoUrun[14].urunKDV.Add(8);
                infoUrun[14].urunPorsiyonSinifi.Add(0);
                infoUrun[14].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[14].urunBarkodu.Add("");
                infoUrun[14].urunYazicisi.Add("Mutfak");
                infoUrun[14].urunMarsYazicilari.Add(new List<string>());

                infoUrun[14].urunAdi.Add("Sultan Kebabı");
                infoUrun[14].urunPorsiyonFiyati.Add("31,00");
                infoUrun[14].urunKiloFiyati.Add("0,00");
                infoUrun[14].urunTuru.Add("Porsiyon");
                infoUrun[14].urunKategorisi.Add("Anadolu Mutfağı");
                infoUrun[14].urunAciklamasi.Add("");
                infoUrun[14].urunKDV.Add(8);
                infoUrun[14].urunPorsiyonSinifi.Add(0);
                infoUrun[14].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[14].urunBarkodu.Add("");
                infoUrun[14].urunYazicisi.Add("Mutfak");
                infoUrun[14].urunMarsYazicilari.Add(new List<string>());

                infoUrun[14].urunAdi.Add("Kızarmış Mantı");
                infoUrun[14].urunPorsiyonFiyati.Add("16,75");
                infoUrun[14].urunKiloFiyati.Add("0,00");
                infoUrun[14].urunTuru.Add("Porsiyon");
                infoUrun[14].urunKategorisi.Add("Anadolu Mutfağı");
                infoUrun[14].urunAciklamasi.Add("");
                infoUrun[14].urunKDV.Add(8);
                infoUrun[14].urunPorsiyonSinifi.Add(0);
                infoUrun[14].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[14].urunBarkodu.Add("");
                infoUrun[14].urunYazicisi.Add("Mutfak");
                infoUrun[14].urunMarsYazicilari.Add(new List<string>());

                infoUrun[14].urunAdi.Add("Karışık Izgara Tabağı");
                infoUrun[14].urunPorsiyonFiyati.Add("32,00");
                infoUrun[14].urunKiloFiyati.Add("0,00");
                infoUrun[14].urunTuru.Add("Porsiyon");
                infoUrun[14].urunKategorisi.Add("Anadolu Mutfağı");
                infoUrun[14].urunAciklamasi.Add("");
                infoUrun[14].urunKDV.Add(8);
                infoUrun[14].urunPorsiyonSinifi.Add(0);
                infoUrun[14].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[14].urunBarkodu.Add("");
                infoUrun[14].urunYazicisi.Add("Mutfak");
                infoUrun[14].urunMarsYazicilari.Add(new List<string>());

                infoUrun[14].urunAdi.Add("İçli Köfte");
                infoUrun[14].urunPorsiyonFiyati.Add("17,25");
                infoUrun[14].urunKiloFiyati.Add("0,00");
                infoUrun[14].urunTuru.Add("Porsiyon");
                infoUrun[14].urunKategorisi.Add("Anadolu Mutfağı");
                infoUrun[14].urunAciklamasi.Add("");
                infoUrun[14].urunKDV.Add(8);
                infoUrun[14].urunPorsiyonSinifi.Add(0);
                infoUrun[14].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[14].urunBarkodu.Add("");
                infoUrun[14].urunYazicisi.Add("Mutfak");
                infoUrun[14].urunMarsYazicilari.Add(new List<string>());

                infoUrun[14].urunAdi.Add("Etli Yaprak Sarma");
                infoUrun[14].urunPorsiyonFiyati.Add("17,00");
                infoUrun[14].urunKiloFiyati.Add("0,00");
                infoUrun[14].urunTuru.Add("Porsiyon");
                infoUrun[14].urunKategorisi.Add("Anadolu Mutfağı");
                infoUrun[14].urunAciklamasi.Add("");
                infoUrun[14].urunKDV.Add(8);
                infoUrun[14].urunPorsiyonSinifi.Add(0);
                infoUrun[14].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[14].urunBarkodu.Add("");
                infoUrun[14].urunYazicisi.Add("Mutfak");
                infoUrun[14].urunMarsYazicilari.Add(new List<string>());

                infoUrun[14].urunAdi.Add("Çökertme Kebabı");
                infoUrun[14].urunPorsiyonFiyati.Add("29,00");
                infoUrun[14].urunKiloFiyati.Add("0,00");
                infoUrun[14].urunTuru.Add("Porsiyon");
                infoUrun[14].urunKategorisi.Add("Anadolu Mutfağı");
                infoUrun[14].urunAciklamasi.Add("");
                infoUrun[14].urunKDV.Add(8);
                infoUrun[14].urunPorsiyonSinifi.Add(0);
                infoUrun[14].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[14].urunBarkodu.Add("");
                infoUrun[14].urunYazicisi.Add("Mutfak");
                infoUrun[14].urunMarsYazicilari.Add(new List<string>());

                infoUrun[15].urunAdi.Add("Izgara Sebze Tabağı");
                infoUrun[15].urunPorsiyonFiyati.Add("18,00");
                infoUrun[15].urunKiloFiyati.Add("0,00");
                infoUrun[15].urunTuru.Add("Porsiyon");
                infoUrun[15].urunKategorisi.Add("Vejeteryan Yemekler");
                infoUrun[15].urunAciklamasi.Add("");
                infoUrun[15].urunKDV.Add(8);
                infoUrun[15].urunPorsiyonSinifi.Add(0);
                infoUrun[15].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[15].urunBarkodu.Add("");
                infoUrun[15].urunYazicisi.Add("Mutfak");
                infoUrun[15].urunMarsYazicilari.Add(new List<string>());

                infoUrun[16].urunAdi.Add("Taco Somon");
                infoUrun[16].urunPorsiyonFiyati.Add("33,50");
                infoUrun[16].urunKiloFiyati.Add("0,00");
                infoUrun[16].urunTuru.Add("Porsiyon");
                infoUrun[16].urunKategorisi.Add("Balıklar");
                infoUrun[16].urunAciklamasi.Add("");
                infoUrun[16].urunKDV.Add(8);
                infoUrun[16].urunPorsiyonSinifi.Add(0);
                infoUrun[16].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[16].urunBarkodu.Add("");
                infoUrun[16].urunYazicisi.Add("Mutfak");
                infoUrun[16].urunMarsYazicilari.Add(new List<string>());

                infoUrun[16].urunAdi.Add("Levrek Fileto");
                infoUrun[16].urunPorsiyonFiyati.Add("31,75");
                infoUrun[16].urunKiloFiyati.Add("0,00");
                infoUrun[16].urunTuru.Add("Porsiyon");
                infoUrun[16].urunKategorisi.Add("Balıklar");
                infoUrun[16].urunAciklamasi.Add("");
                infoUrun[16].urunKDV.Add(8);
                infoUrun[16].urunPorsiyonSinifi.Add(0);
                infoUrun[16].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[16].urunBarkodu.Add("");
                infoUrun[16].urunYazicisi.Add("Mutfak");
                infoUrun[16].urunMarsYazicilari.Add(new List<string>());

                infoUrun[17].urunAdi.Add("Coca Cola");
                infoUrun[17].urunPorsiyonFiyati.Add("3,00");
                infoUrun[17].urunKiloFiyati.Add("0,00");
                infoUrun[17].urunTuru.Add("Porsiyon");
                infoUrun[17].urunKategorisi.Add("Soğuk İçecekler");
                infoUrun[17].urunAciklamasi.Add("");
                infoUrun[17].urunKDV.Add(8);
                infoUrun[17].urunPorsiyonSinifi.Add(0);
                infoUrun[17].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[17].urunBarkodu.Add("");
                infoUrun[17].urunYazicisi.Add("Mutfak");
                infoUrun[17].urunMarsYazicilari.Add(new List<string>());

                infoUrun[17].urunAdi.Add("Coca Cola Zero");
                infoUrun[17].urunPorsiyonFiyati.Add("3,00");
                infoUrun[17].urunKiloFiyati.Add("0,00");
                infoUrun[17].urunTuru.Add("Porsiyon");
                infoUrun[17].urunKategorisi.Add("Soğuk İçecekler");
                infoUrun[17].urunAciklamasi.Add("");
                infoUrun[17].urunKDV.Add(8);
                infoUrun[17].urunPorsiyonSinifi.Add(0);
                infoUrun[17].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[17].urunBarkodu.Add("");
                infoUrun[17].urunYazicisi.Add("Mutfak");
                infoUrun[17].urunMarsYazicilari.Add(new List<string>());

                infoUrun[17].urunAdi.Add("Coca Cola Light");
                infoUrun[17].urunPorsiyonFiyati.Add("3,00");
                infoUrun[17].urunKiloFiyati.Add("0,00");
                infoUrun[17].urunTuru.Add("Porsiyon");
                infoUrun[17].urunKategorisi.Add("Soğuk İçecekler");
                infoUrun[17].urunAciklamasi.Add("");
                infoUrun[17].urunKDV.Add(8);
                infoUrun[17].urunPorsiyonSinifi.Add(0);
                infoUrun[17].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[17].urunBarkodu.Add("");
                infoUrun[17].urunYazicisi.Add("Mutfak");
                infoUrun[17].urunMarsYazicilari.Add(new List<string>());

                infoUrun[17].urunAdi.Add("Fuse Tea Şeftali");
                infoUrun[17].urunPorsiyonFiyati.Add("3,00");
                infoUrun[17].urunKiloFiyati.Add("0,00");
                infoUrun[17].urunTuru.Add("Porsiyon");
                infoUrun[17].urunKategorisi.Add("Soğuk İçecekler");
                infoUrun[17].urunAciklamasi.Add("");
                infoUrun[17].urunKDV.Add(8);
                infoUrun[17].urunPorsiyonSinifi.Add(0);
                infoUrun[17].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[17].urunBarkodu.Add("");
                infoUrun[17].urunYazicisi.Add("Mutfak");
                infoUrun[17].urunMarsYazicilari.Add(new List<string>());

                infoUrun[17].urunAdi.Add("Fuse Tea Limon");
                infoUrun[17].urunPorsiyonFiyati.Add("3,00");
                infoUrun[17].urunKiloFiyati.Add("0,00");
                infoUrun[17].urunTuru.Add("Porsiyon");
                infoUrun[17].urunKategorisi.Add("Soğuk İçecekler");
                infoUrun[17].urunAciklamasi.Add("");
                infoUrun[17].urunKDV.Add(8);
                infoUrun[17].urunPorsiyonSinifi.Add(0);
                infoUrun[17].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[17].urunBarkodu.Add("");
                infoUrun[17].urunYazicisi.Add("Mutfak");
                infoUrun[17].urunMarsYazicilari.Add(new List<string>());

                infoUrun[17].urunAdi.Add("Fanta");
                infoUrun[17].urunPorsiyonFiyati.Add("3,00");
                infoUrun[17].urunKiloFiyati.Add("0,00");
                infoUrun[17].urunTuru.Add("Porsiyon");
                infoUrun[17].urunKategorisi.Add("Soğuk İçecekler");
                infoUrun[17].urunAciklamasi.Add("");
                infoUrun[17].urunKDV.Add(8);
                infoUrun[17].urunPorsiyonSinifi.Add(0);
                infoUrun[17].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[17].urunBarkodu.Add("");
                infoUrun[17].urunYazicisi.Add("Mutfak");
                infoUrun[17].urunMarsYazicilari.Add(new List<string>());

                infoUrun[17].urunAdi.Add("Sprite");
                infoUrun[17].urunPorsiyonFiyati.Add("3,00");
                infoUrun[17].urunKiloFiyati.Add("0,00");
                infoUrun[17].urunTuru.Add("Porsiyon");
                infoUrun[17].urunKategorisi.Add("Soğuk İçecekler");
                infoUrun[17].urunAciklamasi.Add("");
                infoUrun[17].urunKDV.Add(8);
                infoUrun[17].urunPorsiyonSinifi.Add(0);
                infoUrun[17].urunYaziciyaBildirilmeliMi.Add(true);
                infoUrun[17].urunBarkodu.Add("");
                infoUrun[17].urunYazicisi.Add("Mutfak");
                infoUrun[17].urunMarsYazicilari.Add(new List<string>());

                infoUrun[0].kategorininAdi = "Kahvaltı";
                infoUrun[1].kategorininAdi = "Sahanda Servisler";
                infoUrun[2].kategorininAdi = "Omletler";
                infoUrun[3].kategorininAdi = "Tostlar";
                infoUrun[4].kategorininAdi = "Gözlemeler";
                infoUrun[5].kategorininAdi = "Aperatifler";
                infoUrun[6].kategorininAdi = "Salatalar";
                infoUrun[7].kategorininAdi = "Burgerler";
                infoUrun[8].kategorininAdi = "Dürümler";
                infoUrun[9].kategorininAdi = "Kumpir";
                infoUrun[10].kategorininAdi = "Makarnalar";
                infoUrun[11].kategorininAdi = "Pizza";
                infoUrun[12].kategorininAdi = "Krep";
                infoUrun[13].kategorininAdi = "Ana Yemekler";
                infoUrun[14].kategorininAdi = "Anadolu Mutfağı";
                infoUrun[15].kategorininAdi = "Vejeteryan Yemekler";
                infoUrun[16].kategorininAdi = "Balıklar";
                infoUrun[17].kategorininAdi = "Soğuk İçecekler";
                infoUrun[18].kategorininAdi = "Kategorisiz Ürünler";

                XmlSave.SaveRestoran(infoUrun, "urunler.xml");
            }
            #endregion

            XmlLoad<KategorilerineGoreUrunler> loadInfoUrun = new XmlLoad<KategorilerineGoreUrunler>();
            infoUrun = loadInfoUrun.LoadRestoran("urunler.xml");

            KategorilerineGoreUrunler[] infoUrun2 = new KategorilerineGoreUrunler[infoKategoriler[0].kategoriler.Count];

            int count = infoUrun.Count(); // yeni eklenen kategoriler yokken toplam kategori sayısı

            //eklenen kategori var ise sayısını buluyoruz
            if (infoUrun.Count() > infoUrun2.Count())
                count = infoUrun2.Count();

            //var olan ürünleri ekliyoruz 
            for (int i = 0; i < count; i++)
            {
                infoUrun2[i] = infoUrun[i];
            }

            //eklenen ürün var ise onlara yer açıyoruz 
            for (int i = infoUrun.Count(); i < infoUrun2.Count(); i++)
            {
                infoUrun2[i] = new KategorilerineGoreUrunler();
                infoUrun2[i].urunAdi = new List<string>();
                infoUrun2[i].urunPorsiyonFiyati = new List<string>();
                infoUrun2[i].urunKiloFiyati = new List<string>();
                infoUrun2[i].urunKategorisi = new List<string>();
                infoUrun2[i].urunTuru = new List<string>();
                infoUrun2[i].urunAciklamasi = new List<string>();
                infoUrun2[i].urunKDV = new List<int>();
                infoUrun2[i].urunPorsiyonSinifi = new List<int>();
                infoUrun2[i].urunYaziciyaBildirilmeliMi = new List<bool>();
                infoUrun2[i].urunBarkodu = new List<string>();
                infoUrun2[i].urunYazicisi = new List<string>();
                infoUrun2[i].urunMarsYazicilari = new List<List<string>>();
            }

            for (int i = 0; i < kategoriListesi[0].kategoriler.Count; i++)
            {
                infoUrun2[i].kategorininAdi = kategoriListesi[0].kategoriler[i];
            }

            List<KategorilerineGoreUrunler> urunListesiGecici = new List<KategorilerineGoreUrunler>();

            urunListesiGecici.AddRange(infoUrun2);

            int kategoriYeri = 0;

            for (int i = 0; i < urunListesiGecici.Count; i++)
            {
                for (int x = 0; x < urunListesiGecici[i].urunAdi.Count; x++)
                {
                    bool urunKategorisiVar = true;
                    //ürünün kategorisi şu anki listede var mı bak 
                    for (int j = 0; j < treeUrunAdi.Nodes.Count; j++)
                    {
                        if (treeUrunAdi.Nodes[j].Text == urunListesiGecici[i].urunKategorisi[x])
                        {
                            urunKategorisiVar = false;
                            kategoriYeri = j;
                            break;
                        }
                    }

                    //yoksa ürünü kategorisini gecici listeye ekle
                    if (urunKategorisiVar)
                    {
                        urunListesiGecici[urunListesiGecici.Count - 1].urunKategorisi.Add("Kategorisiz Ürünler");
                        urunListesiGecici[urunListesiGecici.Count - 1].urunTuru.Add(urunListesiGecici[i].urunTuru[x]);
                        urunListesiGecici[urunListesiGecici.Count - 1].urunAdi.Add(urunListesiGecici[i].urunAdi[x]);
                        urunListesiGecici[urunListesiGecici.Count - 1].urunAciklamasi.Add(urunListesiGecici[i].urunAciklamasi[x]);
                        urunListesiGecici[urunListesiGecici.Count - 1].urunPorsiyonFiyati.Add(urunListesiGecici[i].urunPorsiyonFiyati[x]);
                        urunListesiGecici[urunListesiGecici.Count - 1].urunKiloFiyati.Add(urunListesiGecici[i].urunKiloFiyati[x]);
                        urunListesiGecici[urunListesiGecici.Count - 1].urunKDV.Add(urunListesiGecici[i].urunKDV[x]);
                        urunListesiGecici[urunListesiGecici.Count - 1].urunPorsiyonSinifi.Add(urunListesiGecici[i].urunPorsiyonSinifi[x]);
                        urunListesiGecici[urunListesiGecici.Count - 1].urunYaziciyaBildirilmeliMi.Add(urunListesiGecici[i].urunYaziciyaBildirilmeliMi[x]);
                        urunListesiGecici[urunListesiGecici.Count - 1].urunBarkodu.Add(urunListesiGecici[i].urunBarkodu[x]);
                        urunListesiGecici[urunListesiGecici.Count - 1].urunYazicisi.Add(urunListesiGecici[i].urunYazicisi[x]);
                        urunListesiGecici[urunListesiGecici.Count - 1].urunMarsYazicilari.Add(urunListesiGecici[i].urunMarsYazicilari[x]);

                        //ürün kategorisiz ürünlerdense sil çünkü kategorisiz ürünler en sonda olduğu için, en son döngüde o ürünler yeniden eklenecek.
                        if (i != urunListesiGecici.Count - 1)
                        {
                            urunListesiGecici[i].urunAdi.RemoveAt(x);
                            urunListesiGecici[i].urunKategorisi.RemoveAt(x);
                            urunListesiGecici[i].urunTuru.RemoveAt(x);
                            urunListesiGecici[i].urunPorsiyonFiyati.RemoveAt(x);
                            urunListesiGecici[i].urunKiloFiyati.RemoveAt(x);
                            urunListesiGecici[i].urunAciklamasi.RemoveAt(x);
                            urunListesiGecici[i].urunKDV.RemoveAt(x);
                            urunListesiGecici[i].urunPorsiyonSinifi.RemoveAt(x);
                            urunListesiGecici[i].urunYaziciyaBildirilmeliMi.RemoveAt(x);
                            urunListesiGecici[i].urunBarkodu.RemoveAt(x);
                            urunListesiGecici[i].urunYazicisi.RemoveAt(x);
                            urunListesiGecici[i].urunMarsYazicilari.RemoveAt(x);
                            x--;
                        }
                    }
                    else // varsa ürünü
                    {
                        if (kategoriYeri <= i)
                            treeUrunAdi.Nodes[kategoriYeri].Nodes.Add(urunListesiGecici[i].urunAdi[x]);

                        if (urunListesiGecici[i].urunKategorisi[x] != urunListesiGecici[i].kategorininAdi)
                        {
                            urunListesiGecici[kategoriYeri].urunKategorisi.Add(urunListesiGecici[i].urunKategorisi[x]);
                            urunListesiGecici[kategoriYeri].urunTuru.Add(urunListesiGecici[i].urunTuru[x]);
                            urunListesiGecici[kategoriYeri].urunAdi.Add(urunListesiGecici[i].urunAdi[x]);
                            urunListesiGecici[kategoriYeri].urunPorsiyonFiyati.Add(urunListesiGecici[i].urunPorsiyonFiyati[x]);
                            urunListesiGecici[kategoriYeri].urunKiloFiyati.Add(urunListesiGecici[i].urunKiloFiyati[x]);
                            urunListesiGecici[kategoriYeri].urunAciklamasi.Add(urunListesiGecici[i].urunAciklamasi[x]);
                            urunListesiGecici[kategoriYeri].urunKDV.Add(urunListesiGecici[i].urunKDV[x]);
                            urunListesiGecici[kategoriYeri].urunPorsiyonSinifi.Add(urunListesiGecici[i].urunPorsiyonSinifi[x]);
                            urunListesiGecici[kategoriYeri].urunYaziciyaBildirilmeliMi.Add(urunListesiGecici[i].urunYaziciyaBildirilmeliMi[x]);
                            urunListesiGecici[kategoriYeri].urunBarkodu.Add(urunListesiGecici[i].urunBarkodu[x]);
                            urunListesiGecici[kategoriYeri].urunYazicisi.Add(urunListesiGecici[i].urunYazicisi[x]);
                            urunListesiGecici[kategoriYeri].urunMarsYazicilari.Add(urunListesiGecici[i].urunMarsYazicilari[x]);

                            urunListesiGecici[i].urunAdi.RemoveAt(x);
                            urunListesiGecici[i].urunKategorisi.RemoveAt(x);
                            urunListesiGecici[i].urunTuru.RemoveAt(x);
                            urunListesiGecici[i].urunPorsiyonFiyati.RemoveAt(x);
                            urunListesiGecici[i].urunKiloFiyati.RemoveAt(x);
                            urunListesiGecici[i].urunAciklamasi.RemoveAt(x);
                            urunListesiGecici[i].urunKDV.RemoveAt(x);
                            urunListesiGecici[i].urunPorsiyonSinifi.RemoveAt(x);
                            urunListesiGecici[i].urunYaziciyaBildirilmeliMi.RemoveAt(x);
                            urunListesiGecici[i].urunBarkodu.RemoveAt(x);
                            urunListesiGecici[i].urunYazicisi.RemoveAt(x);
                            urunListesiGecici[i].urunMarsYazicilari.RemoveAt(x);

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
            {
                if (File.Exists("urunler.xml"))
                    treeUrunAdi.SelectedNode = treeUrunAdi.Nodes[0].Nodes[0];
                else
                {
                    KontrolFormu errorDialog = new KontrolFormu("Ürünler kaydediliyor lütfen tekrar giriniz", false);
                    errorDialog.Show();
                }
            }
        }

        private void textboxUrunName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 22)
                e.Handled = true;

            if (e.KeyChar == '<' || e.KeyChar == '>' || e.KeyChar == '&' || e.KeyChar == '=' || e.KeyChar == '*' || e.KeyChar == '-')
            {
                e.Handled = true;
            }
        }

        // ürün fotoğraf butonu
        private void button3_Click(object sender, EventArgs e)
        {
            Invoker dialog = new Invoker();

            if (dialog.Invoke() == DialogResult.OK)
            {
                ((Button)sender).Text = urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAdi[treeUrunAdi.SelectedNode.Index];

                string newPath = Application.StartupPath + @"\resimler\" + urunListesi[treeUrunAdi.SelectedNode.Parent.Index].urunAdi[treeUrunAdi.SelectedNode.Index] + ".png";
                try
                {
                    System.IO.File.Copy(dialog.InvokeDialog.FileName, newPath, true);
                }
                catch
                {
                    KontrolFormu errorDialog = new KontrolFormu("Dosya kaydedilirken bir hata oluştu lütfen tekrar deneyiniz", false);
                    errorDialog.Show();
                }
            }
        }

        private void comboTur_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboTur.SelectedIndex == 0)
            {
                textboxUrunKiloFiyat.Enabled = false;
                textboxUrunPorsiyonFiyat.Enabled = true;
            }
            else if (comboTur.SelectedIndex == 1)
            {
                textboxUrunKiloFiyat.Enabled = true;
                textboxUrunPorsiyonFiyat.Enabled = false;
            }
            else
            {
                textboxUrunKiloFiyat.Enabled = true;
                textboxUrunPorsiyonFiyat.Enabled = true;
            }
        }

        private void textBoxBarkod_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 22)
                e.Handled = true;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxBarkod_Click(object sender, EventArgs e)
        {
            textBoxBarkod.SelectAll();
        }

        private void buttonMarsYazicilari_Click(object sender, EventArgs e)
        {
            MarsYazicilari marsYazicisiForm = new MarsYazicilari(urunListesi, treeUrunAdi.SelectedNode.Parent.Index, treeUrunAdi.SelectedNode.Index);
            marsYazicisiForm.ShowDialog(); 
        }
    }
}