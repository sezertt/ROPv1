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
    // NOT ÖNEMLİ :  add delete change ve başlangıçtaki file.exists'in içine girildiğinde ve değişiklik olduğunda sql'e değişikliği ilet
    // Bu sayede DEPARTMANLAR SADECE BAŞKA BİLGİSAYARDAN ULAŞILMAK İSTENDİĞİNDE VERİ TABANINDAN ÇEKİLECEK.
    // Normal koşullarda xml'den hızlı erişim sağlanacak

    public partial class Departman : UserControl
    {
        bool changingPlace = false;

        List<Restoran> restoranListesi = new List<Restoran>();


        List<MasaDizayn> masaListesi = new List<MasaDizayn>();

        public Departman()
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
                comboNewDepName.Text = restoranListesi[treeDepartman.SelectedNode.Index].departmanAdi;
                comboNewDepMenu.Text = restoranListesi[treeDepartman.SelectedNode.Index].departmanMenusu;
                comboNewDepView.Text = restoranListesi[treeDepartman.SelectedNode.Index].departmanEkrani;
                newDepartmentForm.Text = comboNewDepName.Text;

                int temp2 = -1;
                for (int i = 0; i < comboNewDepMenu.Items.Count; i++)
                    if (comboNewDepMenu.Text == comboNewDepMenu.Items[i].ToString())
                    {
                        temp2 = i;
                    }
                if (temp2 == -1)
                {
                    comboNewDepMenu.Text = "";
                    restoranListesi[treeDepartman.SelectedNode.Index].departmanMenusu = "";
                }

                int temp = -1;
                for (int i = 0; i < masaListesi.Count; i++)
                    if (comboNewDepView.Text == masaListesi[i].masaPlanIsmi)
                    {
                        temp = i;
                    }
                if (temp == -1)
                {
                    comboNewDepView.Text = "";
                    restoranListesi[treeDepartman.SelectedNode.Index].departmanEkrani = "";
                }

                if (temp == -1 || temp2 == -1)
                    XmlSave.SaveRestoran(restoranListesi, "restoran.xml");

                comboNewDepView.Items.Clear();
                for (int i = 0; i < masaListesi.Count(); i++)
                {
                    bool dizaynVar = false;
                    for (int j = 0; j < restoranListesi.Count; j++)
                    {
                        if (restoranListesi[j].departmanEkrani == masaListesi[i].masaPlanIsmi)
                            dizaynVar = true;
                    }
                    if (!dizaynVar)
                        comboNewDepView.Items.Add(masaListesi[i].masaPlanIsmi);
                }
            }
        }

        //Var olan departmanı silme tuşu. Departmanın bilgilerini siliyoruz, kaydedip treeviewdan çıkarıyoruz
        private void deleteDepartment(object sender, EventArgs e)
        {
            DialogResult eminMisiniz;

            using (KontrolFormu dialog = new KontrolFormu(treeDepartman.SelectedNode.Text + " adlı departmanı silmek istediğinize emin misiniz?", true))
            {
                eminMisiniz = dialog.ShowDialog();
            }

            if (eminMisiniz == DialogResult.Yes)
            {
                restoranListesi.RemoveAt(treeDepartman.SelectedNode.Index);

                XmlSave.SaveRestoran(restoranListesi, "restoran.xml");

                treeDepartman.Nodes[treeDepartman.SelectedNode.Index].Remove();

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
                comboNewDepView.Text = "";
                buttonDeleteDepartment.Visible = false;
                buttonCancel.Visible = true;
            }
            comboNewDepName.Focus();
        }

        //Yeni departman eklemeyi iptal etme tuşu. 
        private void cancelNewDepartment(object sender, EventArgs e)
        {
            comboNewDepName.Text = restoranListesi[treeDepartman.SelectedNode.Index].departmanAdi;
            comboNewDepMenu.Text = restoranListesi[treeDepartman.SelectedNode.Index].departmanMenusu;
            comboNewDepView.Text = restoranListesi[treeDepartman.SelectedNode.Index].departmanEkrani;
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
                using (KontrolFormu dialog = new KontrolFormu("Eksik veya hatalı bilgi girdiniz, lütfen kontrol ediniz", false))
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
                for (int i = 0; i < restoranListesi.Count; i++)
                {
                    if (string.Equals(restoranListesi[i].departmanAdi, comboNewDepName.Text, StringComparison.CurrentCultureIgnoreCase))
                    {
                        using (KontrolFormu dialog = new KontrolFormu("Aynı departman ismi sistemde kayıtlıdır. Lütfen başka bir isimle departman ekleyiniz", false))
                        {
                            dialog.ShowDialog();
                        }
                        comboNewDepName.Select(0, comboNewDepName.Text.Length);
                        return;
                    }
                }
                
                treeDepartman.Nodes.Add(comboNewDepName.Text);

                Restoran newDepartman = new Restoran();
                newDepartman.departmanAdi = comboNewDepName.Text;
                newDepartman.departmanMenusu = comboNewDepMenu.Text;
                newDepartman.departmanEkrani = comboNewDepView.Text;

                newDepartmentForm.Text = comboNewDepName.Text;

                restoranListesi.Add(newDepartman);

                XmlSave.SaveRestoran(restoranListesi, "restoran.xml");

                treeDepartman.SelectedNode = treeDepartman.Nodes[treeDepartman.Nodes.Count - 1];
                treeDepartman.Focus();

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
                if(comboNewDepName.Text != restoranListesi[treeDepartman.SelectedNode.Index].departmanAdi)
                {
                    for (int i = 0; i < restoranListesi.Count; i++)
                    {
                        if (string.Equals(restoranListesi[i].departmanAdi, comboNewDepName.Text, StringComparison.CurrentCultureIgnoreCase))
                        {
                            using (KontrolFormu dialog = new KontrolFormu("Aynı departman ismi sistemde kayıtlıdır. Lütfen başka bir isimle departman ekleyiniz", false))
                            {
                                dialog.ShowDialog();
                            }
                            comboNewDepName.Select(0, comboNewDepName.Text.Length);
                            return;
                        }
                    }
                }

                //Departmanda değişiklik yapıldıktan sonra basılan kaydet butonu.
                // Girilen bilgilerin doğruluğu kontrol edilir daha sonra comboboxlardaki bilgiler xmle aktarılır ve departman ismi treeviewda güncellenir.
                restoranListesi[treeDepartman.SelectedNode.Index].departmanAdi = comboNewDepName.Text;
                restoranListesi[treeDepartman.SelectedNode.Index].departmanMenusu = comboNewDepMenu.Text;
                restoranListesi[treeDepartman.SelectedNode.Index].departmanEkrani = comboNewDepView.Text;

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
            comboNewDepView.Items.Clear();
            for (int i = 0; i < masaListesi.Count(); i++)
            {
                bool dizaynVar = false;
                for (int j = 0; j < restoranListesi.Count; j++)
                {
                    if (restoranListesi[j].departmanEkrani == masaListesi[i].masaPlanIsmi)
                        dizaynVar = true;
                }
                if (!dizaynVar)
                    comboNewDepView.Items.Add(masaListesi[i].masaPlanIsmi);
            }
            ((ComboBox)sender).DroppedDown = true;
        }

        private void Departman_Load(object sender, EventArgs e)
        {
            //açılışta capslock açıksa kapatıyoruz.
            ToggleCapsLock(false);

            #region xml oku

            Restoran[] info = new Restoran[1];

            if (!File.Exists("restoran.xml"))
            {
                info[0] = new Restoran();
                info[0].departmanAdi = "Departman";
                info[0].departmanMenusu = "Menü";
                info[0].departmanEkrani = "Masa Ekranı";
                XmlSave.SaveRestoran(info, "restoran.xml");
            }

            XmlLoad<Restoran> loadInfo = new XmlLoad<Restoran>();
            info = loadInfo.LoadRestoran("restoran.xml");

            restoranListesi.AddRange(info);

            comboNewDepName.Text = restoranListesi[0].departmanAdi;
            comboNewDepMenu.Text = restoranListesi[0].departmanMenusu;
            comboNewDepView.Text = restoranListesi[0].departmanEkrani;

            newDepartmentForm.Text = comboNewDepName.Text;

            treeDepartman.Nodes.Add(restoranListesi[0].departmanAdi);

            for (int i = 1; i < restoranListesi.Count; i++)
            {
                treeDepartman.Nodes.Add(restoranListesi[i].departmanAdi);
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
                //listeyi okuyoruz
                XmlLoad<MasaDizayn> loadInfoMasaDizayn = new XmlLoad<MasaDizayn>();
                MasaDizayn[] masaDizaynlari = loadInfoMasaDizayn.LoadRestoran("masaDizayn.xml");

                masaListesi.AddRange(masaDizaynlari);

                for (int i = 0; i < masaDizaynlari.Count(); i++)
                {
                    bool dizaynVar = false;
                    for (int j = 0; j < restoranListesi.Count; j++)
                    {
                        if (restoranListesi[j].departmanEkrani == masaDizaynlari[i].masaPlanIsmi)
                            dizaynVar = true;
                    }
                    if (!dizaynVar)
                        comboNewDepView.Items.Add(masaDizaynlari[i].masaPlanIsmi);
                }
            }

            if (File.Exists("menu.xml"))
            {
                //liste varsa okuyoruz
                XmlLoad<Menuler> loadInfoMenuler = new XmlLoad<Menuler>();
                Menuler[] menuListesi = loadInfoMenuler.LoadRestoran("menu.xml");

                for (int i = 0; i < menuListesi.Count(); i++)
                {
                    comboNewDepMenu.Items.Add(menuListesi[i].menuAdi);
                }
            }

            #endregion

            treeDepartman.SelectedNode = treeDepartman.Nodes[0];

            if (treeDepartman.Nodes.Count < 2)
                buttonDeleteDepartment.Enabled = false;

            if (treeDepartman.Nodes.Count > 9)
                buttonAddDepartment.Enabled = false;
        }

        private void comboNewDepName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '<' || e.KeyChar == '>' || e.KeyChar == '&' || e.KeyChar == '=' || e.KeyChar == ',' || e.KeyChar == '-')
            {
                e.Handled = true;
            }
        }
    }
}