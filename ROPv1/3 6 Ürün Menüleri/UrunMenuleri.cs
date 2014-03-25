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
    public partial class UrunMenuleri : UserControl
    {
        List<UrunOzellikleri> UrunListesi = new List<UrunOzellikleri>(); // Tüm ürünlerin listesi
        List<MenuBilgileri> UrunMenuListesi = new List<MenuBilgileri>(); // Menülerin listesi    
        MenuBilgileri yeniMenu; // Tek Menü
        public UrunMenuleri()
        {
            InitializeComponent();
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

        //Form Load
        private void UrunMenuleri_Load(object sender, EventArgs e)
        {
            //açılışta capslock açıksa kapatıyoruz.
            ToggleCapsLock(false);
            MenuBilgileri[] info = new MenuBilgileri[1];
            UrunOzellikleri[] info2 = new UrunOzellikleri[1];

            if (File.Exists("urunler.xml"))
            {
                XmlLoad<UrunOzellikleri> loadInfo2 = new XmlLoad<UrunOzellikleri>();
                info2 = loadInfo2.LoadRestoran("urunler.xml");
                UrunListesi.AddRange(info2);
                UrunListesiGoster();
                //Nodeların eklenmesinden sonra taşma varsa bile ekrana sığması için font boyutunu küçültüyoruz
                foreach (TreeNode node in treeUrunler.Nodes)
                {
                    while (treeUrunler.Width - 12 < System.Windows.Forms.TextRenderer.MeasureText(node.Text, new Font(treeUrunler.Font.FontFamily, treeUrunler.Font.Size, treeUrunler.Font.Style)).Width)
                    {
                        treeUrunler.Font = new Font(treeUrunler.Font.FontFamily, treeUrunler.Font.Size - 0.5f, treeUrunler.Font.Style);
                    }
                }
            }

            if (File.Exists("UrunMenuleri.xml"))
            {
                XmlLoad<MenuBilgileri> loadInfo = new XmlLoad<MenuBilgileri>();
                info = loadInfo.LoadRestoran("UrunMenuleri.xml");
                UrunMenuListesi.AddRange(info);
                MenuListesiGoster();
                //Nodeların eklenmesinden sonra taşma varsa bile ekrana sığması için font boyutunu küçültüyoruz
                foreach (TreeNode node in treeMenuler.Nodes)
                {
                    while (treeMenuler.Width - 12 < System.Windows.Forms.TextRenderer.MeasureText(node.Text, new Font(treeMenuler.Font.FontFamily, treeMenuler.Font.Size, treeMenuler.Font.Style)).Width)
                    {
                        treeMenuler.Font = new Font(treeMenuler.Font.FontFamily, treeMenuler.Font.Size - 0.5f, treeMenuler.Font.Style);
                    }
                }
            }

            if (treeMenuler.Nodes.Count > 0)
            {
                treeMenuler.SelectedNode = treeMenuler.Nodes[0];
                newUrunMenuForm.Text = UrunMenuListesi[treeMenuler.SelectedNode.Index].menuAdi;
                textboxMenuName.Text = UrunMenuListesi[treeMenuler.SelectedNode.Index].menuAdi;
                textboxFiyat.Text = UrunMenuListesi[treeMenuler.SelectedNode.Index].menuFiyati.ToString("0.00");

            }
            else
            {
                newUrunMenuForm.Enabled = false;
            }
        }

        //treeMenuler-En Soldaki Menü Treesine menü adlarını ekler
        private void MenuListesiGoster()
        {
            for (int i = 0; i < UrunMenuListesi.Count; i++)
            {
                treeMenuler.Nodes.Add(UrunMenuListesi[i].menuAdi);
            }
        }

        //treeUrunler-En Sağdaki Ürün Treesine ürün adlarını ekler
        private void UrunListesiGoster()
        {
            for (int i = 0; i < UrunListesi.Count; i++)
            {
                for (int j = 0; j < UrunListesi[i].urunAdi.Count; j++)
                {
                    treeUrunler.Nodes.Add(UrunListesi[i].urunAdi[j]);
                }
            }
        }

        //Yeni Menü Ekle butonuna basıldığında gerekli ayarlamaları yapar
        private void buttonAddNewMenu_Click(object sender, EventArgs e)
        {
            yeniMenu = new MenuBilgileri();
            if (newUrunMenuForm.Text != "Yeni Menü")
            {
                newUrunMenuForm.Text = "Yeni Menü";
                textboxMenuName.Clear();
                textboxFiyat.Clear();
                treeMenununUrunler.Nodes.Clear();
                buttonDeleteMenu.Visible = false;
                buttonCancel.Visible = true;
            }

            if (!newUrunMenuForm.Enabled)
            {
                newUrunMenuForm.Enabled = true;
                buttonDeleteMenu.Visible = false;
                buttonCancel.Visible = true;
            }
            textboxMenuName.Focus();
        }

        //Yeni menü eklemeyi iptal eder
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            buttonDeleteMenu.Visible = true;
            buttonCancel.Visible = false;
            buttonAddNewMenu.Enabled = true;
            buttonDeleteUrun.Enabled = true;
            buttonAddUrun.Enabled = true;
            treeMenununUrunler.Nodes.Clear();
            if (treeMenuler.Nodes.Count > 0)
            {//herhangi bir menü kayıtlıysa seçili menü bilgilerini gösterir.
                if (treeMenuler.SelectedNode != null)
                {
                    int newSelected = treeMenuler.SelectedNode.Index;
                    treeMenuler.SelectedNode = null;
                    treeMenuler.SelectedNode = treeMenuler.Nodes[newSelected];

                }
                else
                {
                    treeMenuler.SelectedNode = treeMenuler.Nodes[0];
                }
            }

            else
            {//herhangi bir menü kayıtlı değilse yeni ürün formu gözükmez.
                textboxMenuName.Clear();
                textboxFiyat.Clear();
                newUrunMenuForm.Enabled = false;

            }


        }

        //Yeni menü eklemeyi veya menü düzenlemeyi kaydeder
        private void buttonSaveMenu_Click(object sender, EventArgs e)
        {
            if (textboxMenuName.Text == "" || treeMenununUrunler.Nodes.Count < 1)
            {
                using (KontrolFormu dialog = new KontrolFormu("Eksik veya hatalı bilgi girdiniz, lütfen kontrol ediniz", false))
                {
                    dialog.ShowDialog();
                }
                textboxMenuName.Focus();
                return;
            }
            if (newUrunMenuForm.Text == "Yeni Menü")
            {
                bool varmi = false, ayniMi = false;
                int bulunanindis = 0;
                for (int i = 0; i < UrunMenuListesi.Count; i++)
                {
                    if (string.Equals(UrunMenuListesi[i].menuAdi, textboxMenuName.Text, StringComparison.CurrentCultureIgnoreCase) && UrunMenuListesi[i].menuFiyati != Convert.ToDouble(textboxFiyat.Text))
                    {
                        ayniMi = true;
                        bulunanindis = i;
                        break;
                    }

                    if (string.Equals(UrunMenuListesi[i].menuAdi, textboxMenuName.Text, StringComparison.CurrentCultureIgnoreCase))
                    {
                        varmi = true;
                        bulunanindis = i;
                        break;
                    }
                }
                if (ayniMi)
                {
                    DialogResult eminMisiniz;

                    using (KontrolFormu dialog = new KontrolFormu("Eklemek istediğiniz menü listede bulunmaktadır. Ancak ürün fiyatı farklı girilmiş, ürün fiyatını değiştirmek ister misiniz?", true))
                    {
                        eminMisiniz = dialog.ShowDialog();
                    }

                    if (eminMisiniz == DialogResult.Yes)
                    {
                        UrunMenuListesi[bulunanindis].menuFiyati = Convert.ToDouble(textboxFiyat.Text);
                        XmlSave.SaveRestoran(UrunMenuListesi, "UrunMenuleri.xml");

                        using (KontrolFormu dialog = new KontrolFormu(UrunMenuListesi[bulunanindis].menuAdi + " adlı menü güncellenmiştir", false))
                        {
                            dialog.ShowDialog();
                        }
                    }
                    else
                    {
                        textboxMenuName.Focus();
                    }
                    return;
                }
                else if (varmi)
                {
                    using (KontrolFormu dialog = new KontrolFormu(UrunMenuListesi[bulunanindis].menuAdi + "Eklemek istediğiniz menü zaten aynı fiyatla listede bulunmaktadır.Lütfen menü ismini değiştiriniz", false))
                    {
                        dialog.ShowDialog();
                    }
                    textboxMenuName.Focus();
                    return;
                }
                newUrunMenuForm.Text = textboxMenuName.Text;
                yeniMenu.menuAdi = textboxMenuName.Text;
                yeniMenu.menuFiyati = Convert.ToDouble(textboxFiyat.Text);
                UrunMenuListesi.Add(yeniMenu);
                treeMenuler.Nodes.Add(textboxMenuName.Text);
                treeMenuler.SelectedNode = treeMenuler.Nodes[treeMenuler.Nodes.Count - 1];
                buttonCancel.Visible = false;
                buttonDeleteMenu.Visible = true;
                using (KontrolFormu dialog = new KontrolFormu("Yeni Menü Bilgileri Kaydedilmiştir", false))
                {
                    dialog.ShowDialog();
                }

                // Oluşturulan Menüyü ürün menülerine ekleyebilmek için ürün özellikleri cinsinden kaydediyoruz.
                XmlLoad<UrunOzellikleri> loadInfo = new XmlLoad<UrunOzellikleri>();
                UrunOzellikleri[] infoUrun = loadInfo.LoadRestoran("urunler.xml");
                XmlLoad<TumKategoriler> loadInfoKategori = new XmlLoad<TumKategoriler>();
                TumKategoriler[] infoKategori = loadInfoKategori.LoadRestoran("kategoriler.xml");

                if (infoKategori[0].kategoriler[0] != "Ürün Menüleri")
                {
                    infoKategori[0].kategoriler.Insert(0, "Ürün Menüleri");
                    XmlSave.SaveRestoran(infoKategori, "kategoriler.xml");
                }
                infoUrun[0].urunAdi.Add(UrunMenuListesi[UrunMenuListesi.Count - 1].menuAdi);
                infoUrun[0].porsiyonFiyati.Add(UrunMenuListesi[UrunMenuListesi.Count - 1].menuFiyati.ToString("0.00"));
                infoUrun[0].urunKategorisi.Add("Ürün Menüleri");
                infoUrun[0].urunKDV.Add(8);
                infoUrun[0].kategorininAdi = "Ürün Menüleri";
                XmlSave.SaveRestoran(infoUrun, "urunler.xml");
                //treeUrunler.Nodes.Insert(0, UrunMenuListesi[UrunMenuListesi.Count - 1].menuAdi);
            }
            else//menüde değişiklik yapıldıktan sonra basılan kaydet butonu.
            {
                if (sender != null)
                {
                    bool varmi = false, ayniMi = false;
                    int bulunanindis = 0;
                    for (int i = 0; i < UrunMenuListesi.Count; i++)
                    {
                        if (string.Equals(UrunMenuListesi[i].menuAdi, textboxMenuName.Text, StringComparison.CurrentCultureIgnoreCase) && UrunMenuListesi[i].menuFiyati != Convert.ToDouble(textboxFiyat.Text))
                        {
                            ayniMi = true;
                            bulunanindis = i;
                            break;
                        }

                        if (string.Equals(UrunMenuListesi[i].menuAdi, textboxMenuName.Text, StringComparison.CurrentCultureIgnoreCase))
                        {
                            varmi = true;
                            bulunanindis = i;
                            break;
                        }
                    }
                    if (ayniMi)
                    {
                        DialogResult eminMisiniz;

                        using (KontrolFormu dialog = new KontrolFormu("Eklemek istediğiniz menü listede bulunmaktadır. Ancak ürün fiyatı farklı girilmiş, ürün fiyatını değiştirmek ister misiniz?", true))
                        {
                            eminMisiniz = dialog.ShowDialog();
                        }

                        if (eminMisiniz == DialogResult.Yes)
                        {

                            UrunMenuListesi[bulunanindis].menuFiyati = Convert.ToDouble(textboxFiyat.Text);
                            XmlSave.SaveRestoran(UrunMenuListesi, "UrunMenuleri.xml");
                            XmlLoad<UrunOzellikleri> loadInfo = new XmlLoad<UrunOzellikleri>();
                            UrunOzellikleri[] infoUrun = loadInfo.LoadRestoran("urunler.xml");
                            infoUrun[0].porsiyonFiyati[bulunanindis] = UrunMenuListesi[bulunanindis].menuFiyati.ToString("0.00");
                            XmlSave.SaveRestoran(infoUrun, "urunler.xml");
                            using (KontrolFormu dialog = new KontrolFormu(UrunMenuListesi[bulunanindis].menuAdi + " adlı menü güncellenmiştir", false))
                            {
                                dialog.ShowDialog();
                            }
                        }
                        else
                        {
                            textboxMenuName.Focus();
                        }
                        return;
                    }
                    else if (varmi)
                    {
                        using (KontrolFormu dialog = new KontrolFormu(UrunMenuListesi[bulunanindis].menuAdi + "Eklemek istediğiniz menü zaten aynı fiyatla listede bulunmaktadır.Lütfen menü ismini değiştiriniz", false))
                        {
                            dialog.ShowDialog();
                        }
                        textboxMenuName.Focus();
                        return;
                    }
                }

                using (KontrolFormu dialog = new KontrolFormu("Menü Bilgileri Güncellenmiştir", false))
                {
                    dialog.ShowDialog();
                }
            }

            XmlSave.SaveRestoran(UrunMenuListesi, "UrunMenuleri.xml");
            newUrunMenuForm.Text = textboxMenuName.Text;
            //Nodeların eklenmesinden sonra taşma varsa bile ekrana sığması için font boyutunu küçültüyoruz
            foreach (TreeNode node in treeMenuler.Nodes)
            {
                while (treeMenuler.Width - 12 < System.Windows.Forms.TextRenderer.MeasureText(node.Text, new Font(treeMenuler.Font.FontFamily, treeMenuler.Font.Size, treeMenuler.Font.Style)).Width)
                {
                    treeMenuler.Font = new Font(treeMenuler.Font.FontFamily, treeMenuler.Font.Size - 0.5f, treeMenuler.Font.Style);
                }
            }

            
        }

        //Menüye Sağdaki Ürün treesinde seçili olan ürünü ekler
        private void buttonAddUrun_Click(object sender, EventArgs e)
        {
            if (treeUrunler.SelectedNode != null)
            {

                int donecek, aradigim = treeUrunler.SelectedNode.Index + 1, index = 0;
                for (int i = 0; i < UrunListesi.Count; i++)
                {
                    donecek = UrunListesi[i].porsiyonFiyati.Count;
                    if (UrunListesi[i].porsiyonFiyati.Count >= aradigim)
                    {
                        index = i;
                        break;
                    }
                    aradigim -= donecek;
                }
                if (textboxFiyat.Text == "")
                {
                    textboxFiyat.Text = UrunListesi[index].porsiyonFiyati[aradigim - 1];
                }
                else
                {
                    double fiyat = Convert.ToDouble(textboxFiyat.Text);
                    fiyat += Convert.ToDouble(UrunListesi[index].porsiyonFiyati[aradigim - 1]);
                    textboxFiyat.Text = fiyat.ToString();
                }
                MenuUrunuBilgisi yeniurun = new MenuUrunuBilgisi();
                yeniurun.porsiyonFiyati = UrunListesi[index].porsiyonFiyati[aradigim - 1];
                yeniurun.urunAdi = UrunListesi[index].urunAdi[aradigim - 1];
                yeniurun.urunKategorisi = UrunListesi[index].urunKategorisi[aradigim - 1];
                yeniurun.urunKDV = UrunListesi[index].urunKDV[aradigim - 1];

                if (buttonCancel.Visible)
                {
                    yeniMenu.menuAdi = textboxMenuName.Text;
                    yeniMenu.menuFiyati = Convert.ToDouble(textboxFiyat.Text);
                    yeniMenu.urun.Add(yeniurun);
                }
                else
                    UrunMenuListesi[treeMenuler.SelectedNode.Index].urun.Add(yeniurun);
                treeMenununUrunler.Nodes.Add(yeniurun.urunAdi);

                //Nodeların eklenmesinden sonra taşma varsa bile ekrana sığması için font boyutunu küçültüyoruz
                foreach (TreeNode node in treeMenununUrunler.Nodes)
                {
                    while (treeMenununUrunler.Width - 12 < System.Windows.Forms.TextRenderer.MeasureText(node.Text, new Font(treeMenununUrunler.Font.FontFamily, treeMenununUrunler.Font.Size, treeMenununUrunler.Font.Style)).Width)
                    {
                        treeMenununUrunler.Font = new Font(treeMenununUrunler.Font.FontFamily, treeMenununUrunler.Font.Size - 0.5f, treeMenununUrunler.Font.Style);
                    }
                }
            }
            else
            {
                using (KontrolFormu dialog = new KontrolFormu("Lütfen menüye eklemek istediğiniz ürünü seçiniz", false))
                {
                    dialog.ShowDialog();
                    return;
                }

            }

        }

        //Menüden 2. treedeki (treeMenununUrunler) seçili ürünü siler
        private void buttonDeleteUrun_Click(object sender, EventArgs e)
        {
            if (treeMenununUrunler.SelectedNode != null)
            {
                double fiyat = Convert.ToDouble(textboxFiyat.Text);
                if (buttonCancel.Visible)
                {
                    fiyat -= Convert.ToDouble(yeniMenu.urun[treeMenununUrunler.SelectedNode.Index].porsiyonFiyati);
                    yeniMenu.urun.RemoveAt(treeMenununUrunler.SelectedNode.Index);
                }
                else
                {
                    fiyat -= Convert.ToDouble(UrunMenuListesi[treeMenuler.SelectedNode.Index].urun[treeMenununUrunler.SelectedNode.Index].porsiyonFiyati);
                }
                treeMenununUrunler.Nodes.Remove(treeMenununUrunler.SelectedNode);

                textboxFiyat.Text = fiyat.ToString();
            }
        }

        //arama metodu
        private void textboxUrunAra_TextChanged(object sender, EventArgs e)
        {
            treeUrunler.Nodes.Clear();
            for (int i = 0; i < UrunListesi.Count; i++)
            {
                int kac = textboxUrunAra.Text.Length;
                for (int j = 0; j < UrunListesi[i].urunAdi.Count; j++)
                {
                    if (UrunListesi[i].urunAdi[j].Length < kac)
                        continue;
                    if (string.Equals(UrunListesi[i].urunAdi[j].Substring(0, kac), textboxUrunAra.Text.Substring(0, kac), StringComparison.CurrentCultureIgnoreCase))
                    {
                        treeUrunler.Nodes.Add(UrunListesi[i].urunAdi[j]);
                    }

                }
            }
        }

        //Soldaki treede (treeMenuler) seçili menüyü siler
        private void buttonDeleteMenu_Click(object sender, EventArgs e)
        {
            DialogResult eminMisiniz;

            using (KontrolFormu dialog = new KontrolFormu(treeMenuler.SelectedNode.Text + " adlı menüyü silmek istediğinize emin misiniz?", true))
            {
                eminMisiniz = dialog.ShowDialog();
            }

            if (eminMisiniz == DialogResult.Yes)
            {
                //listeden menüyü siliyoruz
                UrunMenuListesi.RemoveAt(treeMenuler.SelectedNode.Index);
                XmlSave.SaveRestoran(UrunMenuListesi, "UrunMenuleri.xml");
                UrunListesi[0].urunAdi.RemoveAt(treeMenuler.SelectedNode.Index);
                UrunListesi[0].urunKategorisi.RemoveAt(treeMenuler.SelectedNode.Index);
                UrunListesi[0].porsiyonFiyati.RemoveAt(treeMenuler.SelectedNode.Index);
                UrunListesi[0].urunKDV.RemoveAt(treeMenuler.SelectedNode.Index);
                XmlSave.SaveRestoran(UrunListesi, "urunler.xml");
                int selectedPlace = treeMenuler.SelectedNode.Index;
                treeMenuler.SelectedNode.Remove();
                //treeMenununUrunler.Nodes.Clear();
                if (treeMenuler.Nodes.Count > 0)
                    treeMenuler.SelectedNode = treeMenuler.Nodes[0];

                if (treeMenuler.Nodes.Count > 0)
                {
                    if (selectedPlace > treeMenuler.Nodes.Count - 1)
                        selectedPlace = treeMenuler.Nodes.Count - 1;

                    treeMenuler.SelectedNode = treeMenuler.Nodes[selectedPlace];
                }
                else
                {
                    newUrunMenuForm.Enabled = false;
                }
            }
        }

        //Soldaki treede (treeMenuler) seçili menü bilgilerini textboxlara atar
        private void treeMenuler_AfterSelect(object sender, TreeViewEventArgs e)
        {
            buttonCancel.Visible = false;
            buttonDeleteMenu.Visible = true;
            treeMenununUrunler.Nodes.Clear();
            textboxMenuName.Text = UrunMenuListesi[treeMenuler.SelectedNode.Index].menuAdi;
            newUrunMenuForm.Text = textboxMenuName.Text;
            textboxFiyat.Text = UrunMenuListesi[treeMenuler.SelectedNode.Index].menuFiyati.ToString("0.00");
            for (int i = 0; i < UrunMenuListesi[treeMenuler.SelectedNode.Index].urun.Count; i++)
            {
                treeMenununUrunler.Nodes.Add(UrunMenuListesi[treeMenuler.SelectedNode.Index].urun[i].urunAdi);
            }
        }
    }
}
