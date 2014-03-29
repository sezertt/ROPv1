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

namespace ROPv1
{
    public partial class MenuControl : UserControl
    {
        List<Menuler> menuListesi = new List<Menuler>();  // menüleri tutacak liste

        List<TumKategoriler> kategoriListesi = new List<TumKategoriler>(); // kategorileri tutacak liste

        public MenuControl()
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

        // Menü kategorisi seçildi sağ konsolu değiştir
        private void showMenuSide(object sender, EventArgs e)
        {
            newMenuForm.Visible = true;
            newKategoriForm.Visible = false;
        }

        // Tüm kategorilerde farklı bir kategori seçildi güncelle
        private void changeNewKategori(object sender, TreeViewEventArgs e)
        {
            if (treeNewKategori.Nodes.Count < 1)
                return;
            newKategoriForm.Text = treeNewKategori.SelectedNode.Text;
            textBoxYeniKategori.Text = treeNewKategori.SelectedNode.Text;
            buttonAddKategori.Enabled = true;
        }

        // Tüm kategorilerden kategori seçildi sağ konsolu değiştir
        private void showKategoriSide(object sender, EventArgs e)
        {
            newMenuForm.Visible = false;
            newKategoriForm.Visible = true;
        }

        // Farklı menü seçildi sağdaki kontrolü ve menünün kategorilerini güncelle
        private void changeMenuName(object sender, TreeViewEventArgs e)
        {
            textboxMenuName.Text = menuListesi[treeMenuName.SelectedNode.Index].menuAdi;
            newMenuForm.Text = textboxMenuName.Text;
            treeMenuKategori.Nodes.Clear();

            // menüde olan kategorileri menünün kategori listesine yazıyoruz

            for (int i = 0; i < menuListesi[treeMenuName.SelectedNode.Index].menukategorileri.Count(); i++)
            {
                treeMenuKategori.Nodes.Add(menuListesi[treeMenuName.SelectedNode.Index].menukategorileri[i]);
            }


            treeNewKategori.Nodes.Clear();

            // menüde olmayan kategori isimlerini en sağ tree viewa genel kategori listesine yazdırıyoruz

            for (int i = 0; i < kategoriListesi[0].kategoriler.Count; i++)
            {
                bool varMi = true;
                for (int j = 0; j < menuListesi[treeMenuName.SelectedNode.Index].menukategorileri.Count; j++)
                    if (menuListesi[treeMenuName.SelectedNode.Index].menukategorileri[j] == kategoriListesi[0].kategoriler[i])
                    {
                        varMi = false;
                        break;
                    }

                if (varMi)
                    treeNewKategori.Nodes.Add(kategoriListesi[0].kategoriler[i]);
            }

            if (menuListesi[treeMenuName.SelectedNode.Index].menukategorileri.Count() > 0)
                treeMenuKategori.SelectedNode = treeMenuKategori.Nodes[0];


            //Nodeların eklenmesinden sonra taşma varsa bile ekrana sığması için font boyutunu küçültüyoruz
            foreach (TreeNode node in treeMenuKategori.Nodes)
            {
                while (treeMenuKategori.Width - 12 < System.Windows.Forms.TextRenderer.MeasureText(node.Text, new Font(treeMenuKategori.Font.FontFamily, treeMenuKategori.Font.Size, treeMenuKategori.Font.Style)).Width)
                {
                    treeMenuKategori.Font = new Font(treeMenuKategori.Font.FontFamily, treeMenuKategori.Font.Size - 0.5f, treeMenuKategori.Font.Style);
                }
            }
        }

        // seçilen menüyü sil
        private void deleteMenu(object sender, EventArgs e)
        {
            DialogResult eminMisiniz;

            using (KontrolFormu dialog = new KontrolFormu(treeMenuName.SelectedNode.Text + " adlı menüyü silmek istediğinize emin misiniz?", true))
            {
                eminMisiniz = dialog.ShowDialog();

            }


            if (eminMisiniz == DialogResult.Yes)
            {
                //listeden menüyü siliyoruz
                menuListesi.RemoveAt(treeMenuName.SelectedNode.Index);

                //kaydediyoruz
                XmlSave.SaveRestoran(menuListesi, "menu.xml");

                // ağaçtan menüyü siliyoruz
                treeMenuName.SelectedNode.Remove();

                // tek menü kaldıysa silinmesin diyoruz
                if (treeMenuName.Nodes.Count < 2)
                    buttonDeleteMenu.Enabled = false;
            }
        }

        // kategoriyi menüden çıkarma
        private void deleteKategori(object sender, EventArgs e)
        {
            if (treeMenuKategori.SelectedNode == null)
                return;
            //istenilen kategoriyi menüden siliyoruz
            menuListesi[treeMenuName.SelectedNode.Index].menukategorileri.RemoveAt(treeMenuKategori.SelectedNode.Index);

            XmlSave.SaveRestoran(menuListesi, "menu.xml");

            // tüm kategoriler kısmına koyuyoruz
            treeNewKategori.Nodes.Add(treeMenuKategori.SelectedNode.Text);

            treeNewKategori.SelectedNode = treeNewKategori.Nodes[treeNewKategori.Nodes.Count - 1];

            //sonra menünün kategorilerini bulunduran treeviewdan siliyoruz
            treeMenuKategori.Nodes[treeMenuKategori.SelectedNode.Index].Remove();

            if (treeMenuKategori.Nodes.Count < 1)
                showKategoriSide(null, null);
            else
                treeMenuKategori.Focus();
        }

        // kategoriyi menüye ekleme
        private void saveKategori(object sender, EventArgs e)
        {
            if (treeNewKategori.SelectedNode == null)
                return;
            //menü listemize kategoriyi ekleyip kaydediyoruz
            menuListesi[treeMenuName.SelectedNode.Index].menukategorileri.Add(treeNewKategori.SelectedNode.Text);
            XmlSave.SaveRestoran(menuListesi, "menu.xml");

            //görünüme kategoriyi ekliyoruz
            treeMenuKategori.Nodes.Add(treeNewKategori.SelectedNode.Text);

            treeMenuKategori.SelectedNode = treeMenuKategori.Nodes[treeMenuKategori.Nodes.Count - 1];

            // tüm kategorilerden kategoriyi çıkarıyoruz
            treeNewKategori.Nodes[treeNewKategori.SelectedNode.Index].Remove();

            if (treeNewKategori.Nodes.Count < 1)
                showMenuSide(null, null);
            else
                treeNewKategori.Focus();

            //Nodeların eklenmesinden sonra taşma varsa bile ekrana sığması için font boyutunu küçültüyoruz
            foreach (TreeNode node in treeMenuKategori.Nodes)
            {
                while (treeMenuKategori.Width - 12 < System.Windows.Forms.TextRenderer.MeasureText(node.Text, new Font(treeMenuKategori.Font.FontFamily, treeMenuKategori.Font.Size, treeMenuKategori.Font.Style)).Width)
                {
                    treeMenuKategori.Font = new Font(treeMenuKategori.Font.FontFamily, treeMenuKategori.Font.Size - 0.5f, treeMenuKategori.Font.Style);
                }
            }
        }

        // Tüm kategoriler içinde değiştirilen kategoriyi kaydetme butonuna basıldı
        private void saveNewKategoriPressed(object sender, EventArgs e)
        {
            if (textBoxYeniKategori.Text == "Yeni Kategori" || textBoxYeniKategori.Text == "" || textBoxYeniKategori.Text == "Kategorisiz Ürünler")
            {
                using (KontrolFormu dialog = new KontrolFormu("Eksik veya hatalı bilgi girdiniz, lütfen kontrol ediniz", false))
                {
                    dialog.ShowDialog();
                }
                return;
            }

            if (newKategoriForm.Text == "Yeni Kategori")
            {// yeni kategori kaydetme
                for (int j = 0; j < kategoriListesi[0].kategoriler.Count(); j++)
                {
                    if (kategoriListesi[0].kategoriler[j] == textBoxYeniKategori.Text)
                    {
                        using (KontrolFormu dialog = new KontrolFormu("Eksik veya hatalı bilgi girdiniz, lütfen kontrol ediniz", false))
                        {
                            dialog.ShowDialog();
                        }
                        return;
                    }
                }

                // tüm kategoriler görünümüne kategoriyi ekliyoruz
                treeNewKategori.Nodes.Insert(treeNewKategori.Nodes.Count - 1, textBoxYeniKategori.Text);

                newMenuForm.Text = textBoxYeniKategori.Text;

                // tüm kategoriler listemize kategoriyi ekleyip kaydediyoruz
                kategoriListesi[0].kategoriler.Insert(kategoriListesi[0].kategoriler.Count - 1, textBoxYeniKategori.Text);
                XmlSave.SaveRestoran(kategoriListesi, "kategoriler.xml");

                treeNewKategori.SelectedNode = treeNewKategori.Nodes[treeNewKategori.Nodes.Count - 1];
                treeNewKategori.Focus();

                buttonDeleteNewKategori.Visible = true;
                buttonAddKategori.Enabled = true;
                buttonCancelNewKategori.Visible = false;
                buttonAddNewMenu.Enabled = true;
                treeNewKategori.Enabled = true;
                treeMenuName.Enabled = true;
                treeMenuKategori.Enabled = true;

                if (kategoriListesi[0].kategoriler.Count > 1)
                    buttonDeleteNewKategori.Enabled = true;
                using (KontrolFormu dialog = new KontrolFormu("Yeni Kategori Kaydedilmiştir", false))
                {
                    dialog.ShowDialog();
                }
            }
            else // kategori düzenleme
            {
                if (treeNewKategori.SelectedNode.Text == "Kategorisiz Ürünler")
                {
                    using (KontrolFormu dialog = new KontrolFormu("Kategorisiz ürünler değiştirilemez", false))
                    {
                        dialog.ShowDialog();
                    }
                    return;
                }
                int kacTane = 0;

                for (int j = 0; j < kategoriListesi[0].kategoriler.Count(); j++)
                {
                    if (string.Equals(kategoriListesi[0].kategoriler[j], textBoxYeniKategori.Text, StringComparison.CurrentCultureIgnoreCase) )
                    {
                        kacTane++;
                    }
                    if (kacTane == 2)
                    {
                        using (KontrolFormu dialog = new KontrolFormu("Aynı isimde bir kategori bulunmaktadır", false))
                        {
                            dialog.ShowDialog();
                        }
                        return;
                    }
                }

                string nameBeforeSave = treeNewKategori.SelectedNode.Text;

                //kategorinin listedeki ismini güncelliyoruz ve kaydediyoruz
                int temp = 0;
                for (int i = 0; i < kategoriListesi[0].kategoriler.Count; i++)
                {
                    if (kategoriListesi[0].kategoriler[i] == nameBeforeSave)
                    {
                        temp = i;
                        break;
                    }
                }

                kategoriListesi[0].kategoriler[temp] = textBoxYeniKategori.Text;
                XmlSave.SaveRestoran(kategoriListesi, "kategoriler.xml");

                //görünümdeki isimleri güncelliyoruz
                treeNewKategori.SelectedNode.Text = textBoxYeniKategori.Text;
                newKategoriForm.Text = textBoxYeniKategori.Text;

                for (int i = 0; i < menuListesi.Count; i++)
                {
                    for (int j = 0; j < menuListesi[i].menukategorileri.Count(); j++)
                    {
                        if (menuListesi[i].menukategorileri[j] == nameBeforeSave)
                        {
                            menuListesi[i].menukategorileri[j] = textBoxYeniKategori.Text;
                        }
                    }
                }
                XmlSave.SaveRestoran(menuListesi, "menu.xml");
                using (KontrolFormu dialog = new KontrolFormu("Kategori Güncellenmiştir", false))
                {
                    dialog.ShowDialog();
                }
            }
        }

        // Tüm kategoriler içinden kategoriyi silme butonuna basıldı
        private void deleteNewKategori(object sender, EventArgs e)
        {
            if (treeNewKategori.SelectedNode.Text == "Kategorisiz Ürünler")
            {
                using (KontrolFormu dialog = new KontrolFormu("Kategorisiz ürünler silinemez", false))
                {
                    dialog.ShowDialog();
                }
                return;
            }

            for (int i = 0; i < menuListesi.Count; i++)
            {
                for (int j = 0; j < menuListesi[i].menukategorileri.Count(); j++)
                {
                    if (menuListesi[i].menukategorileri[j] == treeNewKategori.SelectedNode.Text)
                    {
                        using (KontrolFormu dialog = new KontrolFormu("Kategoriyi silmeden önce bulunduğu menülerden çıkarmalısınız", false))
                        {
                            dialog.ShowDialog();
                        }
                        return;
                    }
                }
            }

            //Kategoriyi tamamen silme uyarısı
            DialogResult eminMisiniz;

            using (KontrolFormu dialog = new KontrolFormu(treeNewKategori.SelectedNode.Text + " adlı kategoriyi silmek istediğinize emin misiniz?", true))
            {
                eminMisiniz = dialog.ShowDialog();
            }

            if (eminMisiniz == DialogResult.Yes)
            {
                //kategoriyi listemizden çıkarıp kaydediyoruz
                kategoriListesi[0].kategoriler.Remove(treeNewKategori.SelectedNode.Text);
                XmlSave.SaveRestoran(kategoriListesi, "kategoriler.xml");

                // görünümden çıkarıyoruz
                treeNewKategori.Nodes[treeNewKategori.SelectedNode.Index].Remove();

                if (kategoriListesi[0].kategoriler.Count < 2)
                    buttonDeleteNewKategori.Enabled = false;
            }
        }

        // Menüyü kaydetme butonu basıldı / sadece menünün adı kaydedilir, kategoriler zaten ekleme çıkarma anında kaydediliyor
        private void saveMenuButtonPressed(object sender, EventArgs e)
        {
            if (textboxMenuName.Text == "Yeni Menü" || textboxMenuName.Text == "")
            {
                using (KontrolFormu dialog = new KontrolFormu("Eksik veya hatalı bilgi girdiniz, lütfen kontrol ediniz", false))
                {
                    dialog.ShowDialog();
                }
                return;
            }
            //Yeni menüyü kaydet tuşu. ekle tuşuna basıp bilgileri girdikten sonra kaydete basıyoruz önce girilen bilgilerin doğruluğu
            //kontrol edilir sonra yeni menü mü ekleniyor yoksa eski menü mü düzenleniyor ona bakılır
            if (newMenuForm.Text == "Yeni Menü") // yeni menü ekleniyor
            {
                //menü ismi görünüme eklenir
                treeMenuName.Nodes.Add(textboxMenuName.Text);

                Menuler newMenu = new Menuler();
                newMenu.menuAdi = textboxMenuName.Text;

                newMenuForm.Text = textboxMenuName.Text;

                //yeni menü listeye eklenip kaydedilir
                menuListesi.Add(newMenu);
                XmlSave.SaveRestoran(menuListesi, "menu.xml");

                treeMenuName.SelectedNode = treeMenuName.Nodes[treeMenuName.Nodes.Count - 1];
                treeMenuName.Focus();

                buttonDeleteMenu.Visible = true;
                buttonCancel.Visible = false;

                buttonAddNewKategori.Enabled = true;
                treeNewKategori.Enabled = true;
                treeMenuName.Enabled = true;

                if (treeMenuName.Nodes.Count > 1)
                    buttonDeleteMenu.Enabled = true;
                using (KontrolFormu dialog = new KontrolFormu("Yeni Menü Kaydedilmiştir", false))
                {
                    dialog.ShowDialog();
                }
            }
            else // eski menü düzenleniyor
            {
                //eski menünün listedeki ismi güncellenip kaydedilir
                menuListesi[treeMenuName.SelectedNode.Index].menuAdi = textboxMenuName.Text;
                XmlSave.SaveRestoran(menuListesi, "menu.xml");

                //eski menünün görünümdeki ismi güncellenir
                treeMenuName.Nodes[treeMenuName.SelectedNode.Index].Text = textboxMenuName.Text;
                newMenuForm.Text = textboxMenuName.Text;
                using (KontrolFormu dialog = new KontrolFormu("Menü Güncellenmiştir", false))
                {
                    dialog.ShowDialog();
                }
            }
            buttonDeleteKategori.Enabled = true;
            //Nodeların eklenmesinden sonra taşma varsa bile ekrana sığması için font boyutunu küçültüyoruz
            foreach (TreeNode node in treeMenuName.Nodes)
            {
                while (treeMenuName.Width - 12 < System.Windows.Forms.TextRenderer.MeasureText(node.Text, new Font(treeMenuName.Font.FontFamily, treeMenuName.Font.Size, treeMenuName.Font.Style)).Width)
                {
                    treeMenuName.Font = new Font(treeMenuName.Font.FontFamily, treeMenuName.Font.Size - 0.5f, treeMenuName.Font.Style);
                }
            }
        }

        // Yeni Menü Oluşturma Butonu Basıldı
        private void createNewMenuButtonPressed(object sender, EventArgs e)
        {
            if (newMenuForm.Text != "Yeni Menü") // her basışta yeniden ayarlanmasın diye, ayarlandı mı kontrolü
            {
                newMenuForm.Text = "Yeni Menü";
                textboxMenuName.Text = "";

                treeMenuKategori.Nodes.Clear();

                buttonDeleteMenu.Visible = false;
                buttonDeleteKategori.Enabled = false;
                buttonCancel.Visible = true;
                buttonAddNewKategori.Enabled = false;
                treeNewKategori.Enabled = false;
                treeMenuName.Enabled = false;
            }
            showMenuSide(null, null);
            textboxMenuName.Focus();
        }

        // menü oluşturmayı iptal et
        private void cancelNewMenu(object sender, EventArgs e)
        {
            textboxMenuName.Text = menuListesi[treeMenuName.SelectedNode.Index].menuAdi;
            newMenuForm.Text = textboxMenuName.Text;

            buttonDeleteMenu.Visible = true;
            buttonCancel.Visible = false;
            buttonAddNewKategori.Enabled = true;
            treeNewKategori.Enabled = true;
            treeMenuName.Enabled = true;
            buttonDeleteKategori.Enabled = true;
            changeMenuName(null, null);
            treeMenuName.Focus();
        }

        // Yeni Kategori Oluşturma Butonu Basıldı
        private void createNewKategoriButtonPressed(object sender, EventArgs e)
        {
            if (newKategoriForm.Text != "Yeni Kategori")
            {
                newKategoriForm.Text = "Yeni Kategori";
                textBoxYeniKategori.Text = "";

                buttonDeleteNewKategori.Visible = false;
                buttonCancelNewKategori.Visible = true;
                buttonAddKategori.Enabled = false;
                buttonAddNewMenu.Enabled = false;
                treeNewKategori.Enabled = false;
                treeMenuKategori.Enabled = false;
                treeMenuName.Enabled = false;
            }
            showKategoriSide(null, null);
            textBoxYeniKategori.Focus();
        }

        // kategori oluşturmayı iptal et
        private void cancelNewKategori(object sender, EventArgs e)
        {
            if (treeNewKategori.SelectedNode != null)
                textBoxYeniKategori.Text = kategoriListesi[0].kategoriler[treeNewKategori.SelectedNode.Index];
            else if (kategoriListesi[0].kategoriler.Count > 0)
                textBoxYeniKategori.Text = kategoriListesi[0].kategoriler[0];
            else
                textBoxYeniKategori.Text = "";

            newKategoriForm.Text = textBoxYeniKategori.Text;

            buttonDeleteNewKategori.Visible = true;
            buttonCancelNewKategori.Visible = false;
            buttonAddNewMenu.Enabled = true;
            treeMenuKategori.Enabled = true;
            treeNewKategori.Enabled = true;
            treeMenuName.Enabled = true;
            buttonAddKategori.Enabled = true;
            treeNewKategori.Focus();
        }

        private void MenuControl_Load(object sender, EventArgs e)
        {
            //açılışta capslock açıksa kapatıyoruz.
            ToggleCapsLock(false);

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

            Menuler[] infoMenu = new Menuler[1];

            if (!File.Exists("menu.xml")) // ilk açılışta veya bir sıkıntı sonucu kategoriler dosyası silinirse kendi default menümüzü giriyoruz.
            {
                infoMenu[0] = new Menuler();
                infoMenu[0].menuAdi = "Menü";
                infoMenu[0].menukategorileri = new List<string>();
                infoMenu[0].menukategorileri.AddRange(infoKategoriler[0].kategoriler);
                XmlSave.SaveRestoran(infoMenu, "menu.xml");
            }

            // Oluşturulmuş menüleri xml den okuyoruz
            XmlLoad<Menuler> loadInfo = new XmlLoad<Menuler>();
            infoMenu = loadInfo.LoadRestoran("menu.xml");

            //menüleri tutacak listemize atıyoruz
            menuListesi.AddRange(infoMenu);

            // ilk menüyü seçili olacağından kullanıcıya hakkında bilgi gösteriyoruz
            textboxMenuName.Text = menuListesi[0].menuAdi;

            newMenuForm.Text = textboxMenuName.Text;

            treeMenuName.Nodes.Add(menuListesi[0].menuAdi);

            // menüde olmayan kategori isimlerini en sağ tree viewa yazdırıyoruz
            for (int i = 0; i < kategoriListesi[0].kategoriler.Count; i++)
            {
                bool varMi = true;
                for (int j = 0; j < menuListesi[0].menukategorileri.Count; j++)
                    if (menuListesi[0].menukategorileri[j] == kategoriListesi[0].kategoriler[i])
                    {
                        varMi = false;
                        break;
                    }

                if (varMi)
                    treeNewKategori.Nodes.Add(kategoriListesi[0].kategoriler[i]);
            }

            //Menü isimlerini en sol tree viewa yazdırıyoruz
            for (int i = 1; i < menuListesi.Count; i++)
            {
                treeMenuName.Nodes.Add(menuListesi[i].menuAdi);
            }

            //menünün kategorilerini ortadaki treeviewa yazdırıyoruz
            for (int i = 0; i < menuListesi[0].menukategorileri.Count(); i++)
            {
                treeMenuKategori.Nodes.Add(menuListesi[0].menukategorileri[i]);
            }
            #endregion

            treeMenuName.SelectedNode = treeMenuName.Nodes[0];


            if (kategoriListesi[0].kategoriler.Count() > 0 && treeNewKategori.Nodes.Count > 0)
            {
                newKategoriForm.Text = treeNewKategori.Nodes[0].Text;
                textBoxYeniKategori.Text = treeNewKategori.Nodes[0].Text;
            }
            else
                newKategoriForm.Text = "";

            if (treeMenuName.Nodes.Count < 2)
                buttonDeleteMenu.Enabled = false;

            if (kategoriListesi[0].kategoriler.Count < 2)
                buttonDeleteNewKategori.Enabled = false;

            //Nodeların eklenmesinden sonra taşma varsa bile ekrana sığması için font boyutunu küçültüyoruz
            foreach (TreeNode node in treeMenuName.Nodes)
            {
                while (treeMenuName.Width - 12 < System.Windows.Forms.TextRenderer.MeasureText(node.Text, new Font(treeMenuName.Font.FontFamily, treeMenuName.Font.Size, treeMenuName.Font.Style)).Width)
                {
                    treeMenuName.Font = new Font(treeMenuName.Font.FontFamily, treeMenuName.Font.Size - 0.5f, treeMenuName.Font.Style);
                }
            }

            #region ürünlerin ilk tanımlaması
            if (!File.Exists("urunler.xml"))
            {
                KategorilerineGoreUrunler[] infoUrun = new KategorilerineGoreUrunler[kategoriListesi[0].kategoriler.Count];

                for (int i = 0; i < kategoriListesi[0].kategoriler.Count; i++)
                {
                    infoUrun[i] = new KategorilerineGoreUrunler();
                    infoUrun[i].urunAdi = new List<string>();
                    infoUrun[i].porsiyonFiyati = new List<string>();
                    infoUrun[i].urunKategorisi = new List<string>();
                    infoUrun[i].urunKDV = new List<int>();
                }

                infoUrun[0].urunAdi.Add("Şırdan Tuzlama");
                infoUrun[0].porsiyonFiyati.Add("7,00");
                infoUrun[0].urunKategorisi.Add("Çorbalar");
                infoUrun[0].urunKDV.Add(8);

                infoUrun[0].urunAdi.Add("İşkembe");
                infoUrun[0].porsiyonFiyati.Add("6,50");
                infoUrun[0].urunKategorisi.Add("Çorbalar");
                infoUrun[0].urunKDV.Add(8);

                infoUrun[0].urunAdi.Add("İşkembe Tuzlama");
                infoUrun[0].porsiyonFiyati.Add("8,00");
                infoUrun[0].urunKategorisi.Add("Çorbalar");
                infoUrun[0].urunKDV.Add(8);

                infoUrun[0].urunAdi.Add("Ayak Paça");
                infoUrun[0].porsiyonFiyati.Add("8,00");
                infoUrun[0].urunKategorisi.Add("Çorbalar");
                infoUrun[0].urunKDV.Add(8);

                infoUrun[0].urunAdi.Add("Kelle Paça");
                infoUrun[0].porsiyonFiyati.Add("9,00");
                infoUrun[0].urunKategorisi.Add("Çorbalar");
                infoUrun[0].urunKDV.Add(8);

                infoUrun[1].urunAdi.Add("Mumbar Dolma");
                infoUrun[1].porsiyonFiyati.Add("9,00");
                infoUrun[1].urunKategorisi.Add("Spesyaller");
                infoUrun[1].urunKDV.Add(8);

                infoUrun[1].urunAdi.Add("İşkembe Güveç");
                infoUrun[1].porsiyonFiyati.Add("9,00");
                infoUrun[1].urunKategorisi.Add("Spesyaller");
                infoUrun[1].urunKDV.Add(8);

                infoUrun[1].urunAdi.Add("Tereyağında Tuzlama");
                infoUrun[1].porsiyonFiyati.Add("7,00");
                infoUrun[1].urunKategorisi.Add("Spesyaller");
                infoUrun[1].urunKDV.Add(8);

                infoUrun[1].urunAdi.Add("Kuzu Kelle");
                infoUrun[1].porsiyonFiyati.Add("8,00");
                infoUrun[1].urunKategorisi.Add("Spesyaller");
                infoUrun[1].urunKDV.Add(8);

                infoUrun[1].urunAdi.Add("Beyin Tava");
                infoUrun[1].porsiyonFiyati.Add("8,00");
                infoUrun[1].urunKategorisi.Add("Spesyaller");
                infoUrun[1].urunKDV.Add(8);

                infoUrun[2].urunAdi.Add("Ankara Döneri");
                infoUrun[2].porsiyonFiyati.Add("8,00");
                infoUrun[2].urunKategorisi.Add("Döner");
                infoUrun[2].urunKDV.Add(8);

                infoUrun[2].urunAdi.Add("Dürüm Döner");
                infoUrun[2].porsiyonFiyati.Add("8,00");
                infoUrun[2].urunKategorisi.Add("Döner");
                infoUrun[2].urunKDV.Add(8);

                infoUrun[2].urunAdi.Add("İskender");
                infoUrun[2].porsiyonFiyati.Add("9,00");
                infoUrun[2].urunKategorisi.Add("Döner");
                infoUrun[2].urunKDV.Add(8);

                infoUrun[2].urunAdi.Add("Kapalı Döner");
                infoUrun[2].porsiyonFiyati.Add("9,00");
                infoUrun[2].urunKategorisi.Add("Döner");
                infoUrun[2].urunKDV.Add(8);

                infoUrun[3].urunAdi.Add("Kıymalı Pide");
                infoUrun[3].porsiyonFiyati.Add("6,00");
                infoUrun[3].urunKategorisi.Add("Pideler");
                infoUrun[3].urunKDV.Add(8);

                infoUrun[3].urunAdi.Add("Kuşbaşılı Pide");
                infoUrun[3].porsiyonFiyati.Add("7,00");
                infoUrun[3].urunKategorisi.Add("Pideler");
                infoUrun[3].urunKDV.Add(8);

                infoUrun[3].urunAdi.Add("Kaşarlı Pide");
                infoUrun[3].porsiyonFiyati.Add("7,00");
                infoUrun[3].urunKategorisi.Add("Pideler");
                infoUrun[3].urunKDV.Add(8);

                infoUrun[3].urunAdi.Add("Karışık Pide");
                infoUrun[3].porsiyonFiyati.Add("8,00");
                infoUrun[3].urunKategorisi.Add("Pideler");
                infoUrun[3].urunKDV.Add(8);

                infoUrun[3].urunAdi.Add("Sucuklu Pide");
                infoUrun[3].porsiyonFiyati.Add("8,00");
                infoUrun[3].urunKategorisi.Add("Pideler");
                infoUrun[3].urunKDV.Add(8);

                infoUrun[3].urunAdi.Add("Lahmacun");
                infoUrun[3].porsiyonFiyati.Add("3,00");
                infoUrun[3].urunKategorisi.Add("Pideler");
                infoUrun[3].urunKDV.Add(8);

                infoUrun[4].urunAdi.Add("Tas Kebabı");
                infoUrun[4].porsiyonFiyati.Add("10,00");
                infoUrun[4].urunKategorisi.Add("Et Yemekleri");
                infoUrun[4].urunKDV.Add(8);

                infoUrun[4].urunAdi.Add("Püreli Kebap");
                infoUrun[4].porsiyonFiyati.Add("10,00");
                infoUrun[4].urunKategorisi.Add("Et Yemekleri");
                infoUrun[4].urunKDV.Add(8);

                infoUrun[4].urunAdi.Add("Beğendili Kebap");
                infoUrun[4].porsiyonFiyati.Add("10,00");
                infoUrun[4].urunKategorisi.Add("Et Yemekleri");
                infoUrun[4].urunKDV.Add(8);

                infoUrun[4].urunAdi.Add("Dana Rosto");
                infoUrun[4].porsiyonFiyati.Add("10,00");
                infoUrun[4].urunKategorisi.Add("Et Yemekleri");
                infoUrun[4].urunKDV.Add(8);

                infoUrun[4].urunAdi.Add("Orman Kebabı");
                infoUrun[4].porsiyonFiyati.Add("10,00");
                infoUrun[4].urunKategorisi.Add("Et Yemekleri");
                infoUrun[4].urunKDV.Add(8);

                infoUrun[4].urunAdi.Add("Çiftlik Kebabı");
                infoUrun[4].porsiyonFiyati.Add("10,00");
                infoUrun[4].urunKategorisi.Add("Et Yemekleri");
                infoUrun[4].urunKDV.Add(8);

                infoUrun[5].urunAdi.Add("İnegöl Köfte");
                infoUrun[5].porsiyonFiyati.Add("7,00");
                infoUrun[5].urunKategorisi.Add("Kebaplar");
                infoUrun[5].urunKDV.Add(8);

                infoUrun[5].urunAdi.Add("Kaşarlı Köfte");
                infoUrun[5].porsiyonFiyati.Add("8,00");
                infoUrun[5].urunKategorisi.Add("Kebaplar");
                infoUrun[5].urunKDV.Add(8);

                infoUrun[5].urunAdi.Add("Adana Kebap");
                infoUrun[5].porsiyonFiyati.Add("7,50");
                infoUrun[5].urunKategorisi.Add("Kebaplar");
                infoUrun[5].urunKDV.Add(8);

                infoUrun[5].urunAdi.Add("Beyti Kebap");
                infoUrun[5].porsiyonFiyati.Add("9,00");
                infoUrun[5].urunKategorisi.Add("Kebaplar");
                infoUrun[5].urunKDV.Add(8);

                infoUrun[5].urunAdi.Add("Patlıcan Kebap");
                infoUrun[5].porsiyonFiyati.Add("10,00");
                infoUrun[5].urunKategorisi.Add("Kebaplar");
                infoUrun[5].urunKDV.Add(8);

                infoUrun[5].urunAdi.Add("Domatesli Kebap");
                infoUrun[5].porsiyonFiyati.Add("8,00");
                infoUrun[5].urunKategorisi.Add("Kebaplar");
                infoUrun[5].urunKDV.Add(8);

                infoUrun[6].urunAdi.Add("Mevsim Salata");
                infoUrun[6].porsiyonFiyati.Add("4,00");
                infoUrun[6].urunKategorisi.Add("Salatalar");
                infoUrun[6].urunKDV.Add(8);

                infoUrun[6].urunAdi.Add("Çoban Salata");
                infoUrun[6].porsiyonFiyati.Add("4,00");
                infoUrun[6].urunKategorisi.Add("Salatalar");
                infoUrun[6].urunKDV.Add(8);

                infoUrun[6].urunAdi.Add("Beyin Salata");
                infoUrun[6].porsiyonFiyati.Add("7,00");
                infoUrun[6].urunKategorisi.Add("Salatalar");
                infoUrun[6].urunKDV.Add(8);

                infoUrun[6].urunAdi.Add("Cacık");
                infoUrun[6].porsiyonFiyati.Add("4,00");
                infoUrun[6].urunKategorisi.Add("Salatalar");
                infoUrun[6].urunKDV.Add(8);

                infoUrun[7].urunAdi.Add("Kaymaklı Ekmek Kadayıfı");
                infoUrun[7].porsiyonFiyati.Add("5,00");
                infoUrun[7].urunKategorisi.Add("Tatlılar");
                infoUrun[7].urunKDV.Add(8);

                infoUrun[7].urunAdi.Add("Sütlü Kadayıf");
                infoUrun[7].porsiyonFiyati.Add("4,00");
                infoUrun[7].urunKategorisi.Add("Tatlılar");
                infoUrun[7].urunKDV.Add(8);

                infoUrun[7].urunAdi.Add("Şekerpare");
                infoUrun[7].porsiyonFiyati.Add("4,00");
                infoUrun[7].urunKategorisi.Add("Tatlılar");
                infoUrun[7].urunKDV.Add(8);

                infoUrun[7].urunAdi.Add("Fırın Sütlaç");
                infoUrun[7].porsiyonFiyati.Add("4,00");
                infoUrun[7].urunKategorisi.Add("Tatlılar");
                infoUrun[7].urunKDV.Add(8);

                infoUrun[8].urunAdi.Add("Kola");
                infoUrun[8].porsiyonFiyati.Add("2,00");
                infoUrun[8].urunKategorisi.Add("İçecekler");
                infoUrun[8].urunKDV.Add(8);

                infoUrun[8].urunAdi.Add("Fanta");
                infoUrun[8].porsiyonFiyati.Add("2,00");
                infoUrun[8].urunKategorisi.Add("İçecekler");
                infoUrun[8].urunKDV.Add(8);

                infoUrun[8].urunAdi.Add("Meyve Suyu");
                infoUrun[8].porsiyonFiyati.Add("2,00");
                infoUrun[8].urunKategorisi.Add("İçecekler");
                infoUrun[8].urunKDV.Add(8);

                infoUrun[8].urunAdi.Add("Ayran");
                infoUrun[8].porsiyonFiyati.Add("1,50");
                infoUrun[8].urunKategorisi.Add("İçecekler");
                infoUrun[8].urunKDV.Add(8);

                infoUrun[8].urunAdi.Add("Soda");
                infoUrun[8].porsiyonFiyati.Add("1,00");
                infoUrun[8].urunKategorisi.Add("İçecekler");
                infoUrun[8].urunKDV.Add(8);

                infoUrun[8].urunAdi.Add("Su");
                infoUrun[8].porsiyonFiyati.Add("1,00");
                infoUrun[8].urunKategorisi.Add("İçecekler");
                infoUrun[8].urunKDV.Add(8);

                infoUrun[8].urunAdi.Add("Çay");
                infoUrun[8].porsiyonFiyati.Add("0,50");
                infoUrun[8].urunKategorisi.Add("İçecekler");
                infoUrun[8].urunKDV.Add(8);

                infoUrun[0].kategorininAdi = "Çorbalar";
                infoUrun[1].kategorininAdi = "Spesyaller";
                infoUrun[2].kategorininAdi = "Döner";
                infoUrun[3].kategorininAdi = "Pideler";
                infoUrun[4].kategorininAdi = "Et Yemekleri";
                infoUrun[5].kategorininAdi = "Kebaplar";
                infoUrun[6].kategorininAdi = "Salatalar";
                infoUrun[7].kategorininAdi = "Tatlılar";
                infoUrun[8].kategorininAdi = "İçecekler";
                infoUrun[8].kategorininAdi = "Kategorisiz Ürünler";

                XmlSave.SaveRestoran(infoUrun, "urunler.xml");
            }
            #endregion
        }

        private void textBoxYeniKategori_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '<' || e.KeyChar == '>' || e.KeyChar == '&' || e.KeyChar == '=' || e.KeyChar == '*' || e.KeyChar == '-')
            {
                e.Handled = true;
            }
        }
    }
}