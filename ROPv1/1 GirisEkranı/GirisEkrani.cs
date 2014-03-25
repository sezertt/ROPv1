﻿using System;
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
using System.Collections.Specialized;
using SPIA;
using SPIA.Server;


namespace ROPv1
{
    public partial class GirisEkrani : Form
    {
        // SPIA kütüphanesindeki sunucu nesnesi        
        private SPIAServer sunucu;

        // Sunucuya bağlı olan kullanıcıları saklayan liste        
        private List<BagliKullanicilar> kullanicilar;

        public List<string> son25Mesaj;

        public WPF_UserControls.VerticalCenterTextBox userNameTextBox;
        public WPF_UserControls.VerticalCenterPasswordBox passwordTextBox;

        UItemp[] infoKullanici;

        public GirisEkrani()
        {
            kullanicilar = new List<BagliKullanicilar>();
            son25Mesaj = new List<string>();
            InitializeComponent();
        }

        // SPIA sunucusunu durdurur        
        private void durdur()
        {
            if (sunucu != null)
            {
                sunucu.Durdur();
                sunucu = null;
            }
        }

        // SPIA sunucusunu başlatır        
        // <returns>İşlemin başarı durumu</returns>
        private bool baslat()
        {
            //Port numarasını Settings'den al
            int port = 0;
            try
            {
                port = Convert.ToInt32(Properties.Settings.Default.Port);
                if (port <= 0)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            //Kullanıcı listesini temizle
            kullanicilar.Clear();

            //Sunucuyu oluştur, olaylarına kaydol ve başlat
            sunucu = new SPIAServer(port);
            sunucu.ClientdanYeniMesajAlindi += new dgClientdanYeniMesajAlindi(sunucu_ClientdenYeniMesajAlindi);
            sunucu.ClientBaglantisiKapatildi += new dgClientBaglantisiKapatildi(sunucu_ClientBaglantisiKapatildi);
            sunucu.Baslat();

            return true;
        }

        private void sunucu_ClientBaglantisiKapatildi(ClientBaglantiArgumanlari e)
        {
            Invoke(new dgClientBaglantisiKapatildi(clientKapandi), e);
        }

        private void sunucu_ClientdenYeniMesajAlindi(ClientdanMesajAlmaArgumanlari e)
        {
            Invoke(new dgClientdanYeniMesajAlindi(mesajAlindi), e);
        }

        // Bir client bağlantısı kapatıldığında ilgili olay bu fonksyonu çağırır        
        // <param name="e">Kapanan clientyle ilgili bilgiler</param>
        private void clientKapandi(ClientBaglantiArgumanlari e)
        {
            komut_cikis(e.Client);
        }

        // Bir clientden mesaj alındığında ilgili olay bu fonksyonu çağırır        
        // <param name="e">Mesaj ve Client parametreleri</param>
        private void mesajAlindi(ClientdanMesajAlmaArgumanlari e)
        {
            //Gelen mesajı & ve = işaretlerine göre ayrıştır
            NameValueCollection parametreler = mesajCoz(e.Mesaj);
            //Ayrıştırma başarısızsa çık
            if (parametreler == null || parametreler.Count < 1)
            {
                return;
            }
            //Ayrıştırma sonucunda komuta göre gerekli işlemleri yap
            try
            {
                switch (parametreler["komut"])
                {
                    case "giris":
                        //parametreler: nick
                        komut_giris(e.Client, parametreler["nick"]);
                        break;
                    case "toplumesaj":
                        //parametreler: mesaj
                        komut_toplumesaj(e.Client, parametreler["mesaj"]);
                        break;
                    case "cikis":
                        //parametreler: YOK
                        komut_cikis(e.Client);
                        break;
                }
            }
            catch (Exception)
            {

            }

            //Mesajı 'Son Gelen 26 Mesaj' listesinde en başa ekle
            son25Mesaj.Insert(0, "[" + e.Client.ClientID.ToString("0000") + "] " + e.Mesaj);
            //Listedeki mesaj sayısı 25'i geçmişse sondan sil.
            if (son25Mesaj.Count > 25)
            {
                son25Mesaj.RemoveAt(25);
            }
        }

        // giris komutunu uygulayan fonksiyon        
        // <param name="client">Girşi yapan client</param>
        // <param name="nick">Seçilen nick</param>
        private void komut_giris(ClientRef client, string nick)
        {
            //Eşzamanlı erişimlere karşı koleksiyonu kilitleyelim
            lock (kullanicilar)
            {
                BagliKullanicilar kullanici = null;
                //Tüm kullanıcıları tara, 
                //aynı nickli başkası varsa giriş başarısızdır
                foreach (BagliKullanicilar kul in kullanicilar)
                {
                    if (kul.Nick == nick)
                    {
                        kullanici = kul;
                        break;
                    }
                }
                //Nick kullanımdaysa clientye uygun dönüş mesajını verip çık
                if (kullanici != null)
                {
                    client.MesajYolla("komut=giris&sonuc=basarisiz");
                    return;
                }
                //Tüm kullanıcıları tara,
                //aynı client zaten listede varsa sadece nickini güncelle
                foreach (BagliKullanicilar kul in kullanicilar)
                {
                    if (kul.Client == client)
                    {
                        kullanici = kul;
                        break;
                    }
                }
                //Client listede varsa sadece nickini güncelle
                if (kullanici != null)
                {
                    kullanici.Nick = nick;
                }
                //Listede yoksa listeye ekle
                else
                {
                    kullanicilar.Add(new BagliKullanicilar(client, nick));
                }
            }
            //Kullanıcıya işlemin başarılı olduğu bilgisini gönder
            client.MesajYolla("komut=giris&sonuc=basarili");



            #region Burası gerekmeyebilir, sonra karar ver
            //Tüm kullanıcılara bu kullanıcının giriş yaptığı bilgisini gönder
            tumKullanicilaraMesajYolla("komut=kullanicigiris&nick=" + nick);
            //Bu kullanıcıya mevcut kullanıcı listesini gönder
            kullaniciListesiniGonder(client);
            #endregion

            //Kullanıcı listesini ekranda gösterelim
            kullaniciListesiniYenile();
        }

        // toplumesaj komutunu uygulayan fonksyon        
        // <param name="client">Mesajı gönderen client</param>
        // <param name="mesaj">Gönderilen mesaj</param>
        private void komut_toplumesaj(ClientRef client, string mesaj)
        {
            //Kullanıcıları saklamak için değişkenler
            BagliKullanicilar gonderenKullanici = null;
            //Eşzamanlı erişimlere karşı koleksiyonu kilitleyelim
            lock (kullanicilar)
            {
                //Tüm kullanıcıları tara, 
                //mesajı gönderen kullanıcıyı bul
                foreach (BagliKullanicilar kul in kullanicilar)
                {
                    //Gönderen kullanıcıyı Client nesnesine göre ayırt ediyoruz
                    if (kul.Client == client)
                    {
                        gonderenKullanici = kul;
                        break;
                    }
                }
            }
            //Gönderen kullanıcı bulunamadıysa fonksyonu sonlandıralım
            if (gonderenKullanici == null)
            {
                return;
            }
            //Tüm kullanıcılara istenilen mesajı gönderelim
            tumKullanicilaraMesajYolla("komut=toplumesaj&nick=" + gonderenKullanici.Nick + "&mesaj=" + mesaj);
        }

        // Bir clientye tüm kullanıcıların listesini gönderir        
        // <param name="client"></param>
        private void kullaniciListesiniGonder(ClientRef client)
        {
            //Kullanıcı listesini "," ile ayırarak birleştir
            StringBuilder nickler = new StringBuilder();
            //Eşzamanlı erişimlere karşı koleksiyonu kilitleyelim
            lock (kullanicilar)
            {
                //Tüm kullanıcıları tara, nickleri birleştir
                foreach (BagliKullanicilar kul in kullanicilar)
                {
                    nickler.Append("," + kul.Nick);
                }
                //İlk kullanıcının başına konulan "," metnini kaldır
                if (nickler.Length >= 1)
                {
                    nickler.Remove(0, 1);
                }
            }
            //Kullanıcıya listeyi gönder
            client.MesajYolla("komut=kullanicilistesi&liste=" + nickler.ToString());
        }

        // kullanıcılar listesindeki kullanıcıların nick'lerini ekranda gösterir.        
        private void kullaniciListesiniYenile()
        {
            //Eşzamanlı erişimlere karşı koleksiyonu kilitleyelim
            lock (kullanicilar)
            {
                StringBuilder nickler = new StringBuilder();
                //Tüm kullanıcıları tara, nickleri birleştir
                foreach (BagliKullanicilar kul in kullanicilar)
                {
                    nickler.Append(", " + kul.Nick);
                }
                //İlk kullanıcının başına konulan ", " metnini kaldır
                if (nickler.Length >= 2)
                {
                    nickler.Remove(0, 2);
                }
                //Nickleri göster
                textboxOnlineKullanicilar.Text = nickler.ToString();
            }
        }

        // kullanıcılar listesindeki tüm kullanıcılara istenilen bir mesajı iletir        
        // <param name="mesaj"></param>
        private void tumKullanicilaraMesajYolla(string mesaj)
        {
            BagliKullanicilar[] kullaniciDizisi = null;
            //Eşzamanlı erişimlere karşı koleksiyonu kilitleyelim
            lock (kullanicilar)
            {
                //Listedeki tüm kullanıcıları bir diziye atalım
                kullaniciDizisi = kullanicilar.ToArray();
            }
            //Tüm kullanıcılara istenilen mesajı gönderelim
            foreach (BagliKullanicilar kul in kullaniciDizisi)
            {
                kul.Client.MesajYolla(mesaj);
            }
        }

        // cikis komutunu uygulayan fonksyon        
        // <param name="client">Çıkış yapan client</param>
        private void komut_cikis(ClientRef client)
        {
            BagliKullanicilar kullanici = null;
            //Eşzamanlı erişimlere karşı koleksiyonu kilitleyelim
            lock (kullanicilar)
            {
                //Tüm kullanıcıları tara, client nesnesini bul
                foreach (BagliKullanicilar kul in kullanicilar)
                {
                    if (kul.Client == client)
                    {
                        kullanici = kul;
                        break;
                    }
                }
                //Client listede varsa listeden çıkar
                if (kullanici != null)
                {
                    kullanicilar.Remove(kullanici);
                }
                //Listede yoksa devam etmeye gerek yok, fonksyondan çık
                else
                {
                    return;
                }
            }
            //Tüm kullanıcılara bu kullanıcının çıkış yaptığı bilgisini gönder
            tumKullanicilaraMesajYolla("komut=kullanicicikis&nick=" + kullanici.Nick);
            //Kullanıcı listesini ekranda gösterelim
            kullaniciListesiniYenile();
        }

        private NameValueCollection mesajCoz(string mesaj)
        {
            try
            {
                //& işaretine göre böl ve diziye at
                string[] parametreler = mesaj.Split('&');
                //dönüş değeri için bir NameValueCollection oluştur
                NameValueCollection nvcParametreler = new NameValueCollection(parametreler.Length);
                //bölünen her parametreyi = işaretine göre yeniden böl ve anahtar/değer çiftleri üret
                foreach (string parametre in parametreler)
                {
                    string[] esitlik = parametre.Split('=');
                    nvcParametreler.Add(esitlik[0], esitlik[1]);
                }
                //oluşturulan koleksiyonu dönder
                return nvcParametreler;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private class BagliKullanicilar
        {
            // SPIA kütüphanesindeki Client nesnesine referans            
            public ClientRef Client
            {
                get { return client; }
                set { client = value; }
            }
            private ClientRef client;

            // Kullanıcının Nick'i            
            public string Nick
            {
                get { return nick; }
                set { nick = value; }
            }
            private string nick;

            // Yeni bir Kullanıcı nesnesi oluşturur.            
            // <param name="client">SPIA kütüphanesindeki Client nesnesine referans</param>
            // <param name="nick">Kullanıcının Nick'i</param>
            public BagliKullanicilar(ClientRef client, string nick)
            {
                this.client = client;
                this.nick = nick;
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

        //sanal klayvemize basıldığında touchscreenkeyboard dll mize basılan key i yolluyoruz
        private void keyboardcontrol1_UserKeyPressed(object sender, KeyboardClassLibrary.KeyboardEventArgs e)
        {
            SendKeys.Send(e.KeyboardKeyPressed);
        }

        private void girisButtonPressed(object sender, EventArgs e)
        {
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
                //this.Close();
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
                        //this.Close();
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
            userNameTextBox = new WPF_UserControls.VerticalCenterTextBox();
            usernameBoxHost.Child = userNameTextBox;
            passwordTextBox = new WPF_UserControls.VerticalCenterPasswordBox();
            passwordBoxHost.Child = passwordTextBox;
        }

        private void siparisButtonPressed(object sender, EventArgs e)
        {
            if (!File.Exists("restoran.xml") || !File.Exists("sonKullanici.xml") || !File.Exists("kategoriler.xml") || !File.Exists("masaDizayn.xml") || !File.Exists("menu.xml") || !File.Exists("urunler.xml"))
            {
                using (KontrolFormu dialog = new KontrolFormu("Lütfen önce programı ayarları kullanarak yapılandırın", false))
                {
                    dialog.ShowDialog();
                    return;
                }
            }

            //sipariş ekranına geçilecek
            ShowWaitForm();

            SiparisMasaFormu siparisForm = new SiparisMasaFormu();
            siparisForm.Show();
            //this.Close();
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
            System.Threading.Thread.Sleep(500);
            Application.Idle += OnLoaded;
        }

        private void OnLoaded(object sender, EventArgs e)
        {
            Application.Idle -= OnLoaded;
            _waitForm.Close();
        }

        private void exitButtonPressed(object sender, EventArgs e)
        {
            DialogResult eminMisiniz;
            if (Properties.Settings.Default.Server == 2)//server
            {
                using (KontrolFormu dialog = new KontrolFormu("DİKKAT!\nÇıkarsanız Server kapatılacak!\nÇıkmak istediğinizden emin misiniz?", true))
                {
                    eminMisiniz = dialog.ShowDialog();
                }
            }
            else
            {
                using (KontrolFormu dialog = new KontrolFormu("Çıkmak istediğinizden emin misiniz?", true))
                {
                    eminMisiniz = dialog.ShowDialog();
                }
            }

            if (eminMisiniz == DialogResult.Yes)
                this.Close();
        }

        private void timerSaat_Tick(object sender, EventArgs e)
        {
            labelSaat.Text = DateTime.Now.ToString("HH:mm:ss", new CultureInfo("tr-TR"));
        }

        private void buttonMutfak_Click(object sender, EventArgs e)
        {
            if (!File.Exists("restoran.xml") || !File.Exists("sonKullanici.xml") || !File.Exists("kategoriler.xml") || !File.Exists("masaDizayn.xml") || !File.Exists("menu.xml") || !File.Exists("sonKullanici.xml") || !File.Exists("urunler.xml"))
            {
                using (KontrolFormu dialog = new KontrolFormu("Lütfen önce programı ayarları kullanarak yapılandırın", false))
                {
                    dialog.ShowDialog();
                    return;
                }
            }
            //mutfak ekranına geçilecek
            ShowWaitForm();

            //MutfakFormu mutfakForm = new MutfakFormu();
            //mutfakForm.Show();
            //this.Close();
        }

        //Form Load
        private void GirisEkrani_Load(object sender, EventArgs e)
        {
            buttonConnection_Click(null, null);
            
            labelSaat.Text = DateTime.Now.ToString("HH:mm:ss", new CultureInfo("tr-TR"));
            timerSaat.Start();
            labelGun.Text = DateTime.Now.ToString("dddd", new CultureInfo("tr-TR"));
            labelTarih.Text = DateTime.Now.Date.ToString("d MMMM yyyy", new CultureInfo("tr-TR"));

            //açılışta capslock açıksa kapatıyoruz.
            ToggleCapsLock(false);

            if (!File.Exists("tempfiles.xml")) // ilk açılışta veya bir sıkıntı sonucu kategoriler dosyası silinirse kendi default kategorilerimizi giriyoruz.
            {
                infoKullanici = new UItemp[1];

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

        // IP - Port - Server Seçimi Ekranı 
        private void GirisEkrani_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.D3) //Kısayol Tuşları ile ekranı açıyoruz ctrl+shift+3
            {
                PortFormu portFormu = new PortFormu();
                portFormu.ShowDialog();
            }
        }

        private void GirisEkrani_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Form kapatılırken, sunucu çalışıyorsa durduralım.
            if (sunucu != null)
            {
                sunucu.Durdur();
                sunucu = null;
            }
        }

        private void buttonConnection_Click(object sender, EventArgs e)
        {
            if(buttonConnection.Image != Properties.Resources.baglantiOK)
            {
                if (baslat())
                {
                    buttonConnection.Image = Properties.Resources.baglantiOK;
                }
                else
                {
                    buttonConnection.Image = Properties.Resources.baglantiYOK;
                    using (KontrolFormu dialog = new KontrolFormu("Hata! Sunucu başlatılamadı!", false))
                    {
                        dialog.ShowDialog();
                    }
                    buttonConnection.Image = Properties.Resources.baglantiYOK;
                }
            }
        }
    }
}
