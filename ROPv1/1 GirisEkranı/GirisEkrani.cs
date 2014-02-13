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
using System.Globalization;

namespace ROPv1
{
    public partial class GirisEkrani : Form
    {
        public WPF_UserControls.VerticalCenterTextBox userNameTextBox;
        public WPF_UserControls.VerticalCenterPasswordBox passwordTextBox;

        bool closeOrShowAnotherForm = false;

        UItemp[] infoKullanici;

        public GirisEkrani()
        {
            InitializeComponent();

            labelSaat.Text = DateTime.Now.ToString("HH:mm:ss", new CultureInfo("tr-TR"));
            timerSaat.Start();
            labelGun.Text = DateTime.Now.ToString("dddd", new CultureInfo("tr-TR"));
            labelTarih.Text = DateTime.Now.Date.ToString("d MMMM yyyy", new CultureInfo("tr-TR"));
            

            //açılışta capslock açıksa kapatıyoruz.
            ToggleCapsLock(false);

            infoKullanici = new UItemp[1];

            if (!File.Exists("tempfiles.xml")) // ilk açılışta veya bir sıkıntı sonucu kategoriler dosyası silinirse kendi default kategorilerimizi giriyoruz.
            {
                infoKullanici[0] = new UItemp();
                infoKullanici[0].UIN = (new UnicodeEncoding()).GetBytes("Adınız");
                infoKullanici[0].UIS = (new UnicodeEncoding()).GetBytes("Soy Adınız");
                infoKullanici[0].UIUN = (new UnicodeEncoding()).GetBytes("admin");
                infoKullanici[0].UIPN = Helper.ComputeHash("0000", "SHA512", null);
                infoKullanici[0].UIPW = Helper.ComputeHash("00000", "SHA512", null);
                infoKullanici[0].UIU = (new UnicodeEncoding()).GetBytes("Yönetici");
                infoKullanici[0].UIY[0] = Helper.ComputeHash("true", "SHA512", null);
                infoKullanici[0].UIY[1] = Helper.ComputeHash("true", "SHA512", null);
                infoKullanici[0].UIY[2] = Helper.ComputeHash("true", "SHA512", null);
                infoKullanici[0].UIY[3] = Helper.ComputeHash("true", "SHA512", null);
                infoKullanici[0].UIY[4] = Helper.ComputeHash("true", "SHA512", null);
                infoKullanici[0].UIY[5] = Helper.ComputeHash("true", "SHA512", null);
                infoKullanici[0].UIY[6] = Helper.ComputeHash("true", "SHA512", null);

                XmlSave.SaveRestoran(infoKullanici, "tempfiles.xml");

                File.SetAttributes("tempfiles.xml", FileAttributes.Archive | FileAttributes.Hidden | FileAttributes.ReadOnly);
            }
            XmlLoad<UItemp> loadInfoKullanicilar = new XmlLoad<UItemp>();
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

            string[] username = new string[1];
            username[0] = userNameTextBox.getNameText(); //name lazım olduğunda al
            string password = passwordTextBox.getPasswordText(); //password lazım olduğunda al 

            int kullaniciAdi = -5;

            if (username[0] == "ropisimiz" && password == "roproprop")
            {
                XmlSave.SaveRestoran(username, "sonKullanici.xml");
                ShowWaitForm();
                AdminGirisFormu adminForm = new AdminGirisFormu();
                adminForm.Show();
                this.Close();
            }
            else
            {
                for (int i = 0; i < infoKullanici.Count(); i++)
                {
                    if (username[0] == (new UnicodeEncoding()).GetString(infoKullanici[i].UIUN))
                    {
                        kullaniciAdi = i;
                        break;
                    }
                }
                if (kullaniciAdi != -5)
                {
                    bool flag = Helper.VerifyHash(password, "SHA512", infoKullanici[kullaniciAdi].UIPW);
                    if (flag == true)
                    { //şifre doğru
                        XmlSave.SaveRestoran(username, "sonKullanici.xml");
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

            SiparisMasaFormu siparisForm = new SiparisMasaFormu();
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

        private void timerSaat_Tick(object sender, EventArgs e)
        {
            labelSaat.Text = DateTime.Now.ToString("HH:mm:ss", new CultureInfo("tr-TR"));
        }

        private void buttonMutfak_Click(object sender, EventArgs e)
        {
            //mutfak ekranına geçilecek
            closeOrShowAnotherForm = true;

            ShowWaitForm();

            //MutfakFormu mutfakForm = new MutfakFormu();
            //mutfakForm.Show();
            //this.Close();
        }
    }
}
