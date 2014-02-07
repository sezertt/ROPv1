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
    // NOT ÖNEMLİ :  add delete change ve başlangıçtaki file.exists'in içine girildiğinde ve değişiklik olduğunda sql'e değişikliği ilet
    // Bu sayede DEPARTMANLAR SADECE BAŞKA BİLGİSAYARDAN ULAŞILMAK İSTENDİĞİNDE VERİ TABANINDAN ÇEKİLECEK.
    // Normal koşullarda xml'den hızlı erişim sağlanacak

    public partial class Departman : UserControl
    {
        int departmanSayisi = Properties.Settings.Default.departmanSayisi;

        bool changingPlace = false;

        List<Restoran> restoranListesi = new List<Restoran>();

        public Departman()
        {
            InitializeComponent();

            //açılışta capslock açıksa kapatıyoruz.
            ToggleCapsLock(false);

            #region xml oku

            Restoran[] info = new Restoran[departmanSayisi];

            if (!File.Exists("restoran.xml"))
            {
                info[0] = new Restoran();
                info[0].DepartmanAdi = "Departman";
                info[0].DepartmanMenusu = "Menü";
                info[0].DepartmanEkrani = "Masa Ekranı";
                info[0].DepartmanDeposu = "Depo";
                XmlSave.SaveRestoran(info, "restoran.xml");
            }

            XmlLoad<Restoran> loadInfo = new XmlLoad<Restoran>();
            info = loadInfo.LoadRestoran("restoran.xml");

            restoranListesi.AddRange(info);

            comboNewDepName.Text = restoranListesi[0].DepartmanAdi;
            comboNewDepMenu.Text = restoranListesi[0].DepartmanMenusu;
            comboNewDepStore.Text = restoranListesi[0].DepartmanDeposu;
            comboNewDepView.Text = restoranListesi[0].DepartmanEkrani;

            newDepartmentForm.Text = comboNewDepName.Text;

            treeDepartman.Nodes.Add(restoranListesi[0].DepartmanAdi);

            for (int i = 1; i < departmanSayisi; i++)
            {
                treeDepartman.Nodes.Add(restoranListesi[i].DepartmanAdi);
            }

            //Nodeların eklenmesinden sonra taşma varsa bile ekrana sığması için font boyutunu küçültüyoruz
            foreach (TreeNode node in treeDepartman.Nodes)
            {
                while (treeDepartman.Width - 12 < System.Windows.Forms.TextRenderer.MeasureText(node.Text, new Font(treeDepartman.Font.FontFamily, treeDepartman.Font.Size, treeDepartman.Font.Style)).Width)
                {
                    treeDepartman.Font = new Font(treeDepartman.Font.FontFamily, treeDepartman.Font.Size - 0.5f, treeDepartman.Font.Style);
                }
            }

            if (File.Exists("masaDizayn.xml"))
            {
                int masaDizaynSayisi = Properties.Settings.Default.masaDizaynSayisi;

                MasaDizayn[] masaDizaynlari = new MasaDizayn[masaDizaynSayisi];

                //liste varsa okuyoruz
                XmlLoad<MasaDizayn> loadInfoMasaDizayn = new XmlLoad<MasaDizayn>();
                masaDizaynlari = loadInfoMasaDizayn.LoadRestoran("masaDizayn.xml");

                for (int i = 0; i < masaDizaynSayisi; i++)
                {
                    comboNewDepView.Items.Add(masaDizaynlari[i].masaPlanIsmi);
                }
            }

            if (File.Exists("menu.xml"))
            {
                int menuSayisi = Properties.Settings.Default.menuSayisi;

                Menuler[] menuListesi = new Menuler[menuSayisi];

                //liste varsa okuyoruz
                XmlLoad<Menuler> loadInfoMenuler = new XmlLoad<Menuler>();
                menuListesi = loadInfoMenuler.LoadRestoran("menu.xml");

                for (int i = 0; i < menuSayisi; i++)
                {
                    comboNewDepMenu.Items.Add(menuListesi[i].menuAdi);
                }
            }


            /* BURAYI AÇ STOK BİTİNCE
if (File.Exists("depolar.xml"))
{
    int depoSayisi = Properties.Settings.Default.depoSayisi;

    Depolar[] depolar = new Depolar[depoSayisi];

    //liste varsa okuyoruz
    XmlLoad<Depolar> loadInfoDepolar = new XmlLoad<Depolar>();
    depolar = loadInfoDepolar.LoadRestoran("depolar.xml");

    for (int i = 0; i < depoSayisi; i++)
    {
        comboNewDepView.Items.Add(depolar[i].DepartmanDeposu);
    }
}   
*/
            #endregion

            treeDepartman.SelectedNode = treeDepartman.Nodes[0];

            if (treeDepartman.Nodes.Count < 2)
                buttonDeleteDepartment.Enabled = false;

            if (treeDepartman.Nodes.Count > 9)
                buttonAddDepartment.Enabled = false;
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

        //sanal klayvemize basıldığında touchscreenkeyboard dll mize basılan key i yolluyoruz
        private void keyboardcontrol1_UserKeyPressed(object sender, KeyboardClassLibrary.KeyboardEventArgs e)
        {
            SendKeys.Send(e.KeyboardKeyPressed);
        }

        //Seçilen departmanın bilgileri comboboxlara aktarılır
        private void changeDepartment(object sender, TreeViewEventArgs e) // Farklı bir departman seçildi
        {
            if (changingPlace)
            {
                changingPlace = false;
                return;
            }
            //yeni bir departmanın yaratılmadığını silme tuşunun görünür olmasından anlayabiliriz, eğer yaratılıyor olsaydı iptal tuşu görünür olurdu
            if (buttonDeleteDepartment.Visible)
            {
                comboNewDepName.Text = restoranListesi[treeDepartman.SelectedNode.Index].DepartmanAdi;
                comboNewDepMenu.Text = restoranListesi[treeDepartman.SelectedNode.Index].DepartmanMenusu;
                comboNewDepStore.Text = restoranListesi[treeDepartman.SelectedNode.Index].DepartmanDeposu;
                comboNewDepView.Text = restoranListesi[treeDepartman.SelectedNode.Index].DepartmanEkrani;
                newDepartmentForm.Text = comboNewDepName.Text;

                bool menuVarMi = false;
                for (int i = 0; i < comboNewDepMenu.Items.Count; i++)
                    if (comboNewDepMenu.Text == comboNewDepMenu.Items[i].ToString())
                    {
                        menuVarMi = true;
                    }
                if (!menuVarMi)
                    comboNewDepMenu.Text = "";

                bool masaPlaniVarMi = false;
                for (int i = 0; i < comboNewDepView.Items.Count; i++)
                    if (comboNewDepView.Text == comboNewDepView.Items[i].ToString())
                    {
                        masaPlaniVarMi = true;
                    }
                if (!masaPlaniVarMi)
                    comboNewDepView.Text = "";
            }
        }

        //Var olan departmanı silme tuşu. Departmanın bilgilerini siliyoruz, kaydedip treeviewdan çıkarıyoruz
        private void deleteDepartment(object sender, EventArgs e)
        {
            DialogResult eminMisiniz;

            using (KontrolFormu dialog = new KontrolFormu(treeDepartman.SelectedNode.Text + " adlı departmanı silmek istediğinize emin misiniz?",true))
            {
                eminMisiniz = dialog.ShowDialog();
            }

            if (eminMisiniz == DialogResult.Yes)
            {
                restoranListesi.RemoveAt(treeDepartman.SelectedNode.Index);

                XmlSave.SaveRestoran(restoranListesi, "restoran.xml");

                treeDepartman.Nodes[treeDepartman.SelectedNode.Index].Remove();

                departmanSayisi--;
                Properties.Settings.Default.departmanSayisi = departmanSayisi;
                Properties.Settings.Default.Save();

                if (treeDepartman.Nodes.Count < 2)
                    buttonDeleteDepartment.Enabled = false;

                if (treeDepartman.Nodes.Count < 10)
                    buttonAddDepartment.Enabled = true;
            }
        }

        //Yeni Departman Ekle Tuşu Comboboxları ayarlayıp Ekleme Ekranını Gösteriyoruz
        private void addNewDepartment(object sender, EventArgs e)
        {
            if (newDepartmentForm.Text != "Yeni Departman")
            {
                newDepartmentForm.Text = "Yeni Departman";
                comboNewDepName.Text = "";
                comboNewDepMenu.Text = "";
                comboNewDepStore.Text = "";
                comboNewDepView.Text = "";
                buttonDeleteDepartment.Visible = false;
                buttonCancel.Visible = true;
            }
            comboNewDepName.Focus();
        }

        //Yeni departman eklemeyi iptal etme tuşu. 
        private void cancelNewDepartment(object sender, EventArgs e)
        {
            comboNewDepName.Text = restoranListesi[treeDepartman.SelectedNode.Index].DepartmanAdi;
            comboNewDepMenu.Text = restoranListesi[treeDepartman.SelectedNode.Index].DepartmanMenusu;
            comboNewDepStore.Text = restoranListesi[treeDepartman.SelectedNode.Index].DepartmanDeposu;
            comboNewDepView.Text = restoranListesi[treeDepartman.SelectedNode.Index].DepartmanEkrani;
            newDepartmentForm.Text = comboNewDepName.Text;

            buttonDeleteDepartment.Visible = true;
            buttonCancel.Visible = false;
            treeDepartman.Focus();
        }

        //Yeni Departmanı Kaydet
        private void buttonAddNewDep(object sender, EventArgs e)
        {
            if (comboNewDepName.Text == "Yeni Departman" || comboNewDepName.Text == "" || comboNewDepMenu.Text == "" || comboNewDepView.Text == "") // ilerde var olan depo,isim,menü veya masa planı var mı diye bak ona göre burayı göster 
            {
                using (KontrolFormu dialog = new KontrolFormu("Hatalı bilgi girdiniz, lütfen kontrol edin",false))
                {
                    dialog.ShowDialog();
                }
                return;
            }

            //Yeni departmanı kaydet tuşu. ekle tuşuna basıp bilgileri girdikten sonra kaydete basıyoruz önce girilen bilgilerin doğruluğu
            //kontrol edilir sonra departmanın treeviewdaki yeri + ayarlardaki yeri tag bilgisi olarak eklenir
            //daha sonra comboboxlardaki bilgiler settingse ve ana ekrandaki comboboxlara aktarılır
            if (newDepartmentForm.Text == "Yeni Departman")
            {
                treeDepartman.Nodes.Add(comboNewDepName.Text);

                Restoran newDepartman = new Restoran();
                newDepartman.DepartmanAdi = comboNewDepName.Text;
                newDepartman.DepartmanMenusu = comboNewDepMenu.Text;
                newDepartman.DepartmanDeposu = comboNewDepStore.Text;
                newDepartman.DepartmanEkrani = comboNewDepView.Text;

                newDepartmentForm.Text = comboNewDepName.Text;

                restoranListesi.Add(newDepartman);

                XmlSave.SaveRestoran(restoranListesi, "restoran.xml");

                treeDepartman.SelectedNode = treeDepartman.Nodes[treeDepartman.Nodes.Count - 1];
                treeDepartman.Focus();

                departmanSayisi++;
                Properties.Settings.Default.departmanSayisi = departmanSayisi;
                Properties.Settings.Default.Save();

                buttonDeleteDepartment.Visible = true;
                buttonCancel.Visible = false;

                if (treeDepartman.Nodes.Count > 1)
                    buttonDeleteDepartment.Enabled = true;
                if (treeDepartman.Nodes.Count > 9)
                    buttonAddDepartment.Enabled = false;

                using (KontrolFormu dialog = new KontrolFormu("Yeni Departman Bilgileri Kaydedilmiştir", false))
                {
                    dialog.ShowDialog();
                }
            }
            else
            {
                //Departmanda değişiklik yapıldıktan sonra basılan kaydet butonu.
                // Girilen bilgilerin doğruluğu kontrol edilir daha sonra comboboxlardaki bilgiler xmle aktarılır ve departman ismi treeviewda güncellenir.
                restoranListesi[treeDepartman.SelectedNode.Index].DepartmanAdi = comboNewDepName.Text;
                restoranListesi[treeDepartman.SelectedNode.Index].DepartmanMenusu = comboNewDepMenu.Text;
                restoranListesi[treeDepartman.SelectedNode.Index].DepartmanDeposu = comboNewDepStore.Text;
                restoranListesi[treeDepartman.SelectedNode.Index].DepartmanEkrani = comboNewDepView.Text;

                XmlSave.SaveRestoran(restoranListesi, "restoran.xml");

                treeDepartman.Nodes[treeDepartman.SelectedNode.Index].Text = comboNewDepName.Text;
                newDepartmentForm.Text = comboNewDepName.Text;

                using (KontrolFormu dialog = new KontrolFormu("Departman Bilgileri Güncellenmiştir", false))
                {
                    dialog.ShowDialog();
                }
            }

            //Nodeların eklenmesinden sonra taşma varsa bile ekrana sığması için font boyutunu küçültüyoruz
            foreach (TreeNode node in treeDepartman.Nodes)
            {
                while (treeDepartman.Width - 12 < System.Windows.Forms.TextRenderer.MeasureText(node.Text, new Font(treeDepartman.Font.FontFamily, treeDepartman.Font.Size, treeDepartman.Font.Style)).Width)
                {
                    treeDepartman.Font = new Font(treeDepartman.Font.FontFamily, treeDepartman.Font.Size - 0.5f, treeDepartman.Font.Style);
                }
            }
        }

        //Departmanların Sıralamasını Değiştir - YUKARI
        private void moveNodeUp(object sender, EventArgs e)
        {

            int index = treeDepartman.SelectedNode.Index; // aldığımız nodeun üstünde başka node var mı diye bakıyoruz         

            if (index - 1 < 0)
                return;

            //treeviewdaki görünümü güncelliyoruz
            changingPlace = true;
            TreeNode node = treeDepartman.Nodes[index];
            treeDepartman.Nodes[index].Remove();
            changingPlace = true;
            treeDepartman.Nodes.Insert(index - 1, node);
            treeDepartman.SelectedNode = treeDepartman.Nodes[index - 1];

            //xmldeki görünümü güncelliyoruz
            Restoran temp = new Restoran();
            temp = restoranListesi[index - 1];
            restoranListesi[index - 1] = restoranListesi[index];
            restoranListesi[index] = temp;

            XmlSave.SaveRestoran(restoranListesi, "restoran.xml");
        }

        //Departmanların Sıralamasını Değiştir - AŞAĞI
        private void modeNodeDown(object sender, EventArgs e)
        {
            int index = treeDepartman.SelectedNode.Index;

            if (index + 1 > treeDepartman.Nodes.Count - 1)
                return;

            //treeviewdaki görünümü güncelliyoruz
            changingPlace = true;
            TreeNode node = treeDepartman.Nodes[index];
            treeDepartman.Nodes[index].Remove();
            changingPlace = true;
            treeDepartman.Nodes.Insert(index + 1, node);

            treeDepartman.SelectedNode = treeDepartman.Nodes[index + 1];

            //xmldeki görünümü güncelliyoruz
            Restoran temp = new Restoran();
            temp = restoranListesi[index + 1];
            restoranListesi[index + 1] = restoranListesi[index];
            restoranListesi[index] = temp;

            XmlSave.SaveRestoran(restoranListesi, "restoran.xml");
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