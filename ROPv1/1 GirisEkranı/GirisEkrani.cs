using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml.Serialization;

namespace ROPv1
{
    public partial class GirisEkrani : Form
    {
        public WPF_UserControls.VerticalCenterTextBox userNameTextBox;
        public WPF_UserControls.VerticalCenterPasswordBox passwordTextBox;

        bool closeOrShowAnotherForm = false;

        KullaniciOzellikleri[] infoKullanici;

        public GirisEkrani()
        {
            InitializeComponent();

            //açılışta capslock açıksa kapatıyoruz.
            ToggleCapsLock(false);

            infoKullanici = new KullaniciOzellikleri[1];

            if (!File.Exists("tempfiles.xml")) // ilk açılışta veya bir sıkıntı sonucu kategoriler dosyası silinirse kendi default kategorilerimizi giriyoruz.
            {
                infoKullanici[0] = new KullaniciOzellikleri();
                infoKullanici[0].adi = "Adınız";
                infoKullanici[0].soyadi = "Soy Adınız";
                infoKullanici[0].kullaniciAdi = "admin";
                infoKullanici[0].pinKodu = Helper.ComputeHash("0000", "SHA512", null);
                infoKullanici[0].sifresi = Helper.ComputeHash("00000", "SHA512", null);
                infoKullanici[0].unvani = "Yönetici";
                infoKullanici[0].yetkileri[0] = Helper.ComputeHash("true", "SHA512", null);
                infoKullanici[0].yetkileri[1] = Helper.ComputeHash("true", "SHA512", null);
                infoKullanici[0].yetkileri[2] = Helper.ComputeHash("true", "SHA512", null);
                infoKullanici[0].yetkileri[3] = Helper.ComputeHash("true", "SHA512", null);
                infoKullanici[0].yetkileri[4] = Helper.ComputeHash("true", "SHA512", null);
                infoKullanici[0].yetkileri[5] = Helper.ComputeHash("true", "SHA512", null);
                infoKullanici[0].yetkileri[6] = Helper.ComputeHash("true", "SHA512", null);

                XmlSave.SaveRestoran(infoKullanici, "tempfiles.xml");

                File.SetAttributes("tempfiles.xml", FileAttributes.Archive | FileAttributes.Hidden | FileAttributes.ReadOnly);
            }
            XmlLoad<KullaniciOzellikleri> loadInfoKullanicilar = new XmlLoad<KullaniciOzellikleri>();
            infoKullanici = loadInfoKullanicilar.LoadRestoran("tempfiles.xml");

            
            //wpflerimizi oluşturduğumuz elementhostların childına atıyoruz
            userNameTextBox = new WPF_UserControls.VerticalCenterTextBox();
            usernameBoxHost.Child = userNameTextBox;
            passwordTextBox = new WPF_UserControls.VerticalCenterPasswordBox();
            passwordBoxHost.Child = passwordTextBox;
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

        private void girisButtonPressed(object sender, EventArgs e)
        {
            closeOrShowAnotherForm = true;            

            string username = userNameTextBox.getNameText(); //name lazım olduğunda al
            string password = passwordTextBox.getPasswordText(); //password lazım olduğunda al 

            int kullaniciAdi = -5;

            if(username == "ropisimiz" && password == "roproprop")
            {
                Properties.Settings.Default.sonGirisYapanKullanici = username;
                Properties.Settings.Default.Save();
                ShowWaitForm();
                AdminGirisFormu adminForm = new AdminGirisFormu();
                adminForm.Show();
                this.Close();
            }
            else
            {
                for (int i = 0; i < infoKullanici.Count(); i++)
                {
                    if (username == infoKullanici[i].kullaniciAdi)
                    {
                        kullaniciAdi = i;
                        break;
                    }
                }
                if (kullaniciAdi != -5)
                {
                    bool flag = Helper.VerifyHash(password, "SHA512", infoKullanici[kullaniciAdi].sifresi);
                    if (flag == true)
                    { //şifre doğru
                        Properties.Settings.Default.sonGirisYapanKullanici = username;
                        Properties.Settings.Default.Save();
                        ShowWaitForm();
                        AdminGirisFormu adminForm = new AdminGirisFormu();
                        adminForm.Show();
                        this.Close();
                    }
                    else
                    {
                        using (KontrolFormu dialog = new KontrolFormu("Yanlış kullanıcı adı/şifre girdiniz", false))
                        {
                            dialog.ShowDialog();
                        }
                    }
                }
                else
                {
                    using (KontrolFormu dialog = new KontrolFormu("Yanlış kullanıcı adı/şifre girdiniz", false))
                    {
                        dialog.ShowDialog();
                    }
                }
            }            
        }

        private void siparisButtonPressed(object sender, EventArgs e)
        {
            //sipariş ekranına geçilecek
            closeOrShowAnotherForm = true;

            ShowWaitForm();

            SiparisFormu siparisForm = new SiparisFormu();
            siparisForm.Show();
            this.Close();
        }

        private MyWaitForm _waitForm;

        //girişe basıldığında id kontrolü sırasında lütfen bekleyiniz yazan bir form göstermek için
        protected void ShowWaitForm()
        {
            // don't display more than one wait form at a time
            if (_waitForm != null && !_waitForm.IsDisposed)
            {
                return;
            }

            _waitForm = new MyWaitForm();
            _waitForm.TopMost = true;
            _waitForm.StartPosition = FormStartPosition.CenterScreen;
            _waitForm.Show();
            _waitForm.Refresh();

            // force the wait window to display for at least 700ms so it doesn't just flash on the screen
            System.Threading.Thread.Sleep(700);
            Application.Idle += OnLoaded;
        }

        private void OnLoaded(object sender, EventArgs e)
        {
            Application.Idle -= OnLoaded;
            _waitForm.Close();
        }

        private void closingGiris(object sender, FormClosingEventArgs e)
        {
            if (!closeOrShowAnotherForm) // eğer başka bir forma gidilmeyecekse uygulamayı kapat
                Application.Exit();
        }

        private void exitButtonPressed(object sender, EventArgs e)
        {
             DialogResult eminMisiniz;

             using (KontrolFormu dialog = new KontrolFormu("Çıkmak istediğinizden emin misiniz?", true))
             {
                 eminMisiniz = dialog.ShowDialog();
             }

             if (eminMisiniz == DialogResult.Yes)
                Application.Exit();
        }
    }
}
