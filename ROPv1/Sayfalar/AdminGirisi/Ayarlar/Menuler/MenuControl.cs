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

            KontrolFormu dialog = new KontrolFormu(treeMenuName.SelectedNode.Text + " adlı menüyü silmek istediğinize emin misiniz?", true);

            eminMisiniz = dialog.ShowDialog();

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
            KontrolFormu dialog;
            if (textBoxYeniKategori.Text == "Yeni Kategori" || textBoxYeniKategori.Text == "" || textBoxYeniKategori.Text == "Kategorisiz Ürünler")
            {
                dialog = new KontrolFormu("Eksik veya hatalı bilgi girdiniz, lütfen kontrol ediniz", false);
                dialog.Show();
                return;
            }

            if (newKategoriForm.Text == "Yeni Kategori")
            {// yeni kategori kaydetme
                for (int j = 0; j < kategoriListesi[0].kategoriler.Count(); j++)
                {
                    if (kategoriListesi[0].kategoriler[j] == textBoxYeniKategori.Text)
                    {
                        dialog = new KontrolFormu("Eksik veya hatalı bilgi girdiniz, lütfen kontrol ediniz", false);
                        dialog.Show();
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
                dialog = new KontrolFormu("Yeni Kategori Kaydedilmiştir", false);
                dialog.Show();
            }
            else // kategori düzenleme
            {
                if (treeNewKategori.SelectedNode.Text == "Kategorisiz Ürünler")
                {
                    dialog = new KontrolFormu("Kategorisiz ürünler değiştirilemez", false);
                    dialog.Show();
                    return;
                }
                int kacTane = 0;

                for (int j = 0; j < kategoriListesi[0].kategoriler.Count(); j++)
                {
                    if (string.Equals(kategoriListesi[0].kategoriler[j], textBoxYeniKategori.Text, StringComparison.CurrentCultureIgnoreCase))
                    {
                        kacTane++;
                    }
                    if (kacTane == 2)
                    {
                        dialog = new KontrolFormu("Aynı isimde bir kategori bulunmaktadır", false);
                        dialog.Show();
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
                dialog = new KontrolFormu("Kategori Güncellenmiştir", false);
                dialog.Show();
            }
        }

        // Tüm kategoriler içinden kategoriyi silme butonuna basıldı
        private void deleteNewKategori(object sender, EventArgs e)
        {
            if (treeNewKategori.SelectedNode.Text == "Kategorisiz Ürünler")
            {
                KontrolFormu dialog2 = new KontrolFormu("Kategorisiz ürünler silinemez", false);
                dialog2.Show();
                return;
            }

            for (int i = 0; i < menuListesi.Count; i++)
            {
                for (int j = 0; j < menuListesi[i].menukategorileri.Count(); j++)
                {
                    if (menuListesi[i].menukategorileri[j] == treeNewKategori.SelectedNode.Text)
                    {
                        KontrolFormu dialog = new KontrolFormu("Kategoriyi silmeden önce bulunduğu menülerden çıkarmalısınız", false);
                        dialog.Show();
                        return;
                    }
                }
            }

            //Kategoriyi tamamen silme uyarısı
            DialogResult eminMisiniz;

            KontrolFormu dialog22 = new KontrolFormu(treeNewKategori.SelectedNode.Text + " adlı kategoriyi silmek istediğinize emin misiniz?", true);
            eminMisiniz = dialog22.ShowDialog();

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
                KontrolFormu dialog = new KontrolFormu("Eksik veya hatalı bilgi girdiniz, lütfen kontrol ediniz", false);
                dialog.Show();
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
                KontrolFormu dialog = new KontrolFormu("Yeni Menü Kaydedilmiştir", false);
                dialog.Show();
            }
            else // eski menü düzenleniyor
            {
                //eski menünün listedeki ismi güncellenip kaydedilir
                menuListesi[treeMenuName.SelectedNode.Index].menuAdi = textboxMenuName.Text;
                XmlSave.SaveRestoran(menuListesi, "menu.xml");

                //eski menünün görünümdeki ismi güncellenir
                treeMenuName.Nodes[treeMenuName.SelectedNode.Index].Text = textboxMenuName.Text;
                newMenuForm.Text = textboxMenuName.Text;
                KontrolFormu dialog = new KontrolFormu("Menü Güncellenmiştir", false);
                dialog.Show();
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
        }

        private void textBoxYeniKategori_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 22)
                e.Handled = true;

            if (e.KeyChar == '<' || e.KeyChar == '>' || e.KeyChar == '&' || e.KeyChar == '=' || e.KeyChar == '*' || e.KeyChar == '-')
            {
                e.Handled = true;
            }
        }
    }
}