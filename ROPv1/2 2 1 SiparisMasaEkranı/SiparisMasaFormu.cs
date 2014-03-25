using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Data.SqlClient;
using SPIA;
using SPIA.Client;

namespace ROPv1
{
    public partial class SiparisMasaFormu : Form
    {
        public bool GirisYapildi
        {
            get { return girisYapildi; }
            set { girisYapildi = value; }
        }
        private bool girisYapildi = false;

        /// SPIA kütüphanesini kullanarak SPIA sunucusuna bağlı olan istemci nesnesi        
        private SPIAClient istemci;

        /// Kullanıcının seçtiği nick
        public string Nick
        {
            get { return nick; }
            set { nick = value; }
        }
        private string nick;

        /// Bağlı kullanıcı listesi        
        private List<string> kullanicilar;

        int hangiButtonSecili = 0;

        List<Restoran> restoranListesi = new List<Restoran>();

        List<MasaDizayn> masaDizaynListesi = new List<MasaDizayn>();

        DateTime hangiGun;

        decimal toplamHesap = 0, kalanHesap = 0;

        public SiparisMasaFormu()
        {
            kullanicilar = new List<string>();
            InitializeComponent();
        }

        private void myPannel_SizeChanged(object sender, EventArgs e)
        {
            panel1.SuspendLayout();
            foreach (Control ctrl in panel1.Controls)
            {
                if (ctrl is Button)
                {
                    ctrl.Width = panel1.ClientSize.Width / panel1.Controls.Count;

                    while (ctrl.Width < System.Windows.Forms.TextRenderer.MeasureText(ctrl.Text, new Font(ctrl.Font.FontFamily, ctrl.Font.Size, ctrl.Font.Style)).Width)
                    {
                        ctrl.Font = new Font(ctrl.Font.FontFamily, ctrl.Font.Size - 0.5f, ctrl.Font.Style);
                    }
                }

            }
            panel1.ResumeLayout();
        }

        private void siparisButonuBasildi(object sender, EventArgs e)
        {
            if (File.Exists("gunler.xml"))
            {
                XmlLoad<GunBilgileri> loadInfoGunler = new XmlLoad<GunBilgileri>();
                GunBilgileri[] infoGunler = loadInfoGunler.LoadRestoran("gunler.xml");

                //gün başı yapılmış mı bak yapılmışsa daybutton resmini set et            
                if (infoGunler[infoGunler.Count() - 1].gunSonuYapanKisi == null && infoGunler[infoGunler.Count() - 1].gunBasiYapanKisi != null)
                { //gün açık sipariş ekranına geçilebilir

                    if (hangiGun != DateTime.Now.Date)
                    {
                        using (KontrolFormu dialog = new KontrolFormu("Gün değişti! Gün Sonu yapmanız gerekiyor", false))
                        {
                            dialog.ShowDialog();
                            this.buttonGunIslemiPressed(null, null);
                        }
                        return;
                    }

                    PinKoduFormu pinForm = new PinKoduFormu("Masa Görüntüleme");
                    pinForm.ShowDialog();

                    if (pinForm.dogru) //pin doğru
                    {
                        SiparisMenuFormu siparisForm;
                        if (((Button)sender).BackColor == Color.White) // masa kapalı
                        {
                            siparisForm = new SiparisMenuFormu(((Button)sender).Text, restoranListesi[hangiButtonSecili], pinForm.ayarYapanKisi, false, 0, 0);//burada masa numarasını da yolla
                            siparisForm.ShowDialog();
                        }
                        else // masa acik
                        {
                            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT ToplamHesap,KalanHesap FROM Adisyon WHERE MasaAdi='" + ((Button)sender).Text + "' AND DepartmanAdi='" + restoranListesi[hangiButtonSecili].departmanAdi + "' AND AcikMi=1");
                            SqlDataReader dr = cmd.ExecuteReader();
                            dr.Read();
                            try
                            {
                                toplamHesap = dr.GetDecimal(0);
                                kalanHesap = dr.GetDecimal(1);
                            }
                            catch
                            {
                                using (KontrolFormu dialog = new KontrolFormu("Bir hata oluştu, lütfen tekrar deneyiniz", false))
                                {
                                    dialog.ShowDialog();
                                }
                                return;
                            }

                            cmd.Connection.Close();
                            cmd.Connection.Dispose();

                            siparisForm = new SiparisMenuFormu(((Button)sender).Text, restoranListesi[hangiButtonSecili], pinForm.ayarYapanKisi, true, toplamHesap, kalanHesap);//burada masa numarasını da yolla
                            siparisForm.ShowDialog();
                        }

                        if (siparisForm.masaAcikMi2 != "")
                        {
                            Button tablebutton = tablePanel.Controls.Find(siparisForm.masaAcikMi2, false)[0] as Button;
                            tablebutton.ForeColor = Color.White;
                            tablebutton.BackColor = Color.Firebrick;
                        }

                        if (siparisForm.masaAcikMi)
                        {
                            ((Button)sender).ForeColor = Color.White;
                            ((Button)sender).BackColor = Color.Firebrick;

                            switch (siparisForm.masaDegisti)
                            {
                                case 2: // departman değişmedi 1 masa açık
                                    ((Button)sender).ForeColor = SystemColors.ActiveCaption;
                                    ((Button)sender).BackColor = Color.White;

                                    Button tablebutton = tablePanel.Controls.Find(siparisForm.yeniMasaninAdi, false)[0] as Button;
                                    tablebutton.ForeColor = Color.White;
                                    tablebutton.BackColor = Color.Firebrick;
                                    break;
                                case 3: // 1 masa açık departmanda değişti
                                    ((Button)sender).ForeColor = SystemColors.ActiveCaption;
                                    ((Button)sender).BackColor = Color.White;
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            ((Button)sender).ForeColor = SystemColors.ActiveCaption;
                            ((Button)sender).BackColor = Color.White;
                        }

                    }
                }
                else
                { //gün başı yapılmalı
                    using (KontrolFormu dialog = new KontrolFormu("Gün Başı yapmanız gerekiyor", false))
                    {
                        dialog.ShowDialog();
                        this.buttonGunIslemiPressed(null, null);
                    }
                }
            }
            else
            {
                using (KontrolFormu dialog = new KontrolFormu("Gün Başı yapmanız gerekiyor", false))
                {
                    dialog.ShowDialog();
                    this.buttonGunIslemiPressed(null, null);
                }
            }
        }

        private void changeTableView(object sender, EventArgs e)
        {
            panel1.Controls[hangiButtonSecili].BackColor = Color.White;
            panel1.Controls[hangiButtonSecili].ForeColor = SystemColors.ActiveCaption;
            panel1.Controls[Convert.ToInt32(((Button)sender).Name)].BackColor = SystemColors.ActiveCaption;
            panel1.Controls[Convert.ToInt32(((Button)sender).Name)].ForeColor = Color.White;
            hangiButtonSecili = Convert.ToInt32(((Button)sender).Name);

            if ((int)((Button)sender).Tag > masaDizaynListesi.Count - 1)
            {
                tablePanel.Controls.Clear();
                tablePanel.Tag = -1;
                return;
            }
            else if ((int)tablePanel.Tag != (int)((Button)sender).Tag) //eğer seçili masa planı zaten ekrandaysa yenisi koyulmasın, ekranda değilse eskiler silinip yenisi eklensin
            {
                tablePanel.RowCount = 6;
                tablePanel.ColumnCount = 7;
                tablePanel.Controls.Clear();
                for (int i = 0; i < 6; i++)
                {
                    if (masaDizaynListesi[(int)((Button)sender).Tag].masaPlanIsmi == "")
                        break;
                    for (int j = 0; j < 7; j++)
                    {
                        if (masaDizaynListesi[(int)((Button)sender).Tag].masaYerleri[i][j] != null)
                        {
                            Button buttonTable = new Button();
                            buttonTable.Text = masaDizaynListesi[(int)((Button)sender).Tag].masaYerleri[i][j];

                            buttonTable.UseVisualStyleBackColor = false;

                            buttonTable.Font = new Font("Arial", 21.75F, FontStyle.Bold);
                            buttonTable.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                            buttonTable.Click += siparisButonuBasildi;
                            tablePanel.Controls.Add(buttonTable, j, i);
                            tablePanel.Tag = (int)((Button)sender).Tag;

                            buttonTable.Name = buttonTable.Text;

                            buttonTable.BackColor = Color.White;
                            buttonTable.ForeColor = SystemColors.ActiveCaption;
                        }
                    }
                }

                SqlCommand cmd = SQLBaglantisi.getCommand("SELECT MasaAdi FROM Adisyon WHERE DepartmanAdi='" + restoranListesi[hangiButtonSecili].departmanAdi + "' AND AcikMi=1");
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    try
                    {
                        Button tablebutton = tablePanel.Controls.Find(dr.GetString(0), false)[0] as Button;
                        tablebutton.BackColor = Color.Firebrick;
                        tablebutton.ForeColor = Color.White;
                    }
                    catch { }
                }

                cmd.Connection.Close();
                cmd.Connection.Dispose();

                for (int j = 6; j > 0; j--)
                {
                    if (masaDizaynListesi[(int)((Button)sender).Tag].masaPlanIsmi == "")
                        break;

                    bool sutunBos = true;
                    for (int i = 5; i > 0; i--)
                    {
                        if (masaDizaynListesi[(int)((Button)sender).Tag].masaYerleri[i][j] != null)
                        {
                            sutunBos = false;
                            break;
                        }
                    }
                    if (sutunBos)
                        tablePanel.ColumnCount--;
                    else
                        break;
                }

                for (int j = 5; j > 0; j--)
                {
                    if (masaDizaynListesi[(int)((Button)sender).Tag].masaPlanIsmi == "")
                        break;
                    bool sutunBos = true;
                    for (int i = 6; i > 0; i--)
                    {
                        if (masaDizaynListesi[(int)((Button)sender).Tag].masaYerleri[j][i] != null)
                        {
                            sutunBos = false;
                            break;
                        }
                    }
                    if (sutunBos)
                        tablePanel.RowCount--;
                    else
                        break;
                }
            }
        }

        private void exitPressed(object sender, EventArgs e)
        {
            DialogResult eminMisiniz;

            using (KontrolFormu dialog = new KontrolFormu("Çıkmak istediğinizden emin misiniz?", true))
            {
                eminMisiniz = dialog.ShowDialog();
            }

            if (eminMisiniz == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void buttonGunIslemiPressed(object sender, EventArgs e)
        {
            PinKoduFormu pinForm = new PinKoduFormu("Gün İşlemi");
            pinForm.ShowDialog();

            if (pinForm.dogru)
            {
                GunFormu gunForm = new GunFormu(pinForm.ayarYapanKisi);
                gunForm.ShowDialog();

                XmlLoad<GunBilgileri> loadInfoGunler = new XmlLoad<GunBilgileri>();

                GunBilgileri[] infoGunler;

                if (File.Exists("gunler.xml"))
                    infoGunler = loadInfoGunler.LoadRestoran("gunler.xml");
                else
                {
                    dayButton.Image = global::ROPv1.Properties.Resources.dayOff;
                    return;
                }

                //gün başı yapılmış mı bak
                if (infoGunler[infoGunler.Count() - 1].gunSonuYapanKisi == null && infoGunler[infoGunler.Count() - 1].gunBasiYapanKisi != null)
                {
                    dayButton.Image = global::ROPv1.Properties.Resources.dayOn;
                    hangiGun = infoGunler[infoGunler.Count() - 1].gunBasiVakti.Date;
                }
                else
                {
                    dayButton.Image = global::ROPv1.Properties.Resources.dayOff;
                }
            }
        }

        private void timerSaat_Tick(object sender, EventArgs e)
        {
            labelSaat.Text = DateTime.Now.ToString("HH:mm:ss", new CultureInfo("tr-TR"));
        }

        private void buttonAdisyon_Click(object sender, EventArgs e)
        {
            PinKoduFormu pinForm = new PinKoduFormu("Adisyon Görüntüleme");
            pinForm.ShowDialog();

            if (pinForm.dogru) // burada adisyon sayfası oluşturulacak ve ona geçilecek
            {
                /*
                GunFormu gunForm = new GunFormu(pinForm.ayarYapanKisi);
                gunForm.ShowDialog();

                XmlLoad<GunBilgileri> loadInfoGunler = new XmlLoad<GunBilgileri>();
                GunBilgileri[] infoGunler = loadInfoGunler.LoadRestoran("gunler.xml");
                */
            }
        }

        private void SiparisMasaFormu_Load(object sender, EventArgs e)
        {
            if (sender != null)
            {
                if (Properties.Settings.Default.Server != 2)
                {
                    buttonConnection_Click(null, null);
                }
                else
                {
                    buttonConnection.Visible = false;
                    dayButton.Visible = true;

                    //burada gün bilgisi alınacak ondan sonra devam edilecek

                    if (File.Exists("gunler.xml"))
                    {
                        XmlLoad<GunBilgileri> loadInfoGunler = new XmlLoad<GunBilgileri>();
                        GunBilgileri[] infoGunler = loadInfoGunler.LoadRestoran("gunler.xml");

                        //gün başı yapılmış mı bak yapılmışsa daybutton resmini set et            
                        if (infoGunler[infoGunler.Count() - 1].gunSonuYapanKisi == null && infoGunler[infoGunler.Count() - 1].gunBasiYapanKisi != null)
                        {
                            dayButton.Image = global::ROPv1.Properties.Resources.dayOn;
                            hangiGun = infoGunler[infoGunler.Count() - 1].gunBasiVakti.Date;
                        }
                        else
                        {
                            dayButton.Image = global::ROPv1.Properties.Resources.dayOff;
                        }
                    }
                    else
                    {
                        dayButton.Image = global::ROPv1.Properties.Resources.dayOff;
                    }
                }

                labelSaat.Text = DateTime.Now.ToString("HH:mm:ss", new CultureInfo("tr-TR"));
                labelGun.Text = DateTime.Now.ToString("dddd", new CultureInfo("tr-TR"));
                labelTarih.Text = DateTime.Now.Date.ToString("d MMMM yyyy", new CultureInfo("tr-TR"));
                timerSaat.Start();

                if (!girisYapildi && Properties.Settings.Default.Server != 2)
                    return;
            }

            if (File.Exists("restoran.xml"))
            {
                XmlLoad<Restoran> loadInfo = new XmlLoad<Restoran>();
                Restoran[] info = loadInfo.LoadRestoran("restoran.xml");

                restoranListesi.AddRange(info);

                int a = 0;

                for (int i = 0; i < restoranListesi.Count; i++)
                {
                    Button departmanButton = new Button();
                    departmanButton.Text = restoranListesi[i].departmanAdi;
                    if (i == 0)
                    {
                        departmanButton.BackColor = SystemColors.ActiveCaption;
                        departmanButton.ForeColor = Color.White;
                    }
                    else
                    {
                        departmanButton.BackColor = Color.White;
                        departmanButton.ForeColor = SystemColors.ActiveCaption;
                    }
                    departmanButton.Font = new Font("Arial", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                    departmanButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    departmanButton.UseVisualStyleBackColor = false;
                    departmanButton.Name = "" + i;

                    if (restoranListesi[i].departmanEkrani == "")
                        departmanButton.Tag = 200;
                    else
                    {
                        departmanButton.Tag = a;
                        a++;
                    }

                    departmanButton.Height = panel1.Height;
                    departmanButton.Width = panel1.Width / restoranListesi.Count;
                    departmanButton.Dock = DockStyle.Right;
                    departmanButton.Click += changeTableView;
                    panel1.Controls.Add(departmanButton);
                }

                XmlLoad<MasaDizayn> loadInfoMasa = new XmlLoad<MasaDizayn>();
                MasaDizayn[] infoMasa = loadInfoMasa.LoadRestoran("masaDizayn.xml");

                //kendi listemize atıyoruz
                masaDizaynListesi.AddRange(infoMasa);

                for (int i = 0; i < 6; i++)
                {
                    if (masaDizaynListesi[0].masaPlanIsmi == "")
                        break;

                    for (int j = 0; j < 7; j++)
                    {
                        if (masaDizaynListesi[0].masaYerleri[i][j] != null)
                        {
                            Button buttonTable = new Button();
                            buttonTable.Text = masaDizaynListesi[0].masaYerleri[i][j];
                            buttonTable.UseVisualStyleBackColor = false;

                            buttonTable.Font = new Font("Arial", 21.75F, FontStyle.Bold);
                            buttonTable.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                            tablePanel.Controls.Add(buttonTable, j, i);
                            buttonTable.Click += siparisButonuBasildi;
                            tablePanel.Tag = 0;
                            buttonTable.Name = buttonTable.Text;

                            buttonTable.BackColor = Color.White;
                            buttonTable.ForeColor = SystemColors.ActiveCaption;
                        }
                    }
                }


                SqlCommand cmd = SQLBaglantisi.getCommand("SELECT MasaAdi FROM Adisyon WHERE DepartmanAdi='" + restoranListesi[hangiButtonSecili].departmanAdi + "' AND AcikMi=1");
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    try
                    {
                        Button tablebutton = tablePanel.Controls.Find(dr.GetString(0), false)[0] as Button;
                        tablebutton.BackColor = Color.Firebrick;
                        tablebutton.ForeColor = Color.White;
                    }
                    catch { }
                }

                cmd.Connection.Close();
                cmd.Connection.Dispose();

                for (int j = 6; j > 0; j--)
                {
                    if (masaDizaynListesi[0].masaPlanIsmi == "")
                        break;

                    bool sutunBos = true;
                    for (int i = 5; i > 0; i--)
                    {
                        if (masaDizaynListesi[0].masaYerleri[i][j] != null)
                        {
                            sutunBos = false;
                            break;
                        }
                    }
                    if (sutunBos)
                        tablePanel.ColumnCount--;
                    else
                        break;
                }

                for (int j = 5; j > 0; j--)
                {
                    if (masaDizaynListesi[0].masaPlanIsmi == "")
                        break;

                    bool sutunBos = true;
                    for (int i = 6; i > 0; i--)
                    {
                        if (masaDizaynListesi[0].masaYerleri[j][i] != null)
                        {
                            sutunBos = false;
                            break;
                        }
                    }
                    if (sutunBos)
                        tablePanel.RowCount--;
                    else
                        break;
                }

            }
        }

        private void SiparisMasaFormu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.D3) //Kısayol Tuşları ile ekranı açıyoruz ctrl+shift+3
            {
                PortFormu portFormu = new PortFormu();
                portFormu.ShowDialog();
            }
        }

        void istemci_YeniMesajAlindi(MesajAlmaArgumanlari e)
        {
            Invoke(new dgYeniMesajAlindi(mesajAlindi), e);
        }

        private bool baglan()
        {
            try
            {
                //IP ve PORT bilgilerini al
                string ip = Properties.Settings.Default.IP;
                int port = Convert.ToInt32(Properties.Settings.Default.Port);
                //Bir istemci nesnesi oluştur ve bağlan
                istemci = new SPIAClient(ip, port);
                return istemci.Baglan();
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void SiparisMasaFormu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Properties.Settings.Default.Server != 2)
            {
                //Form kapatılırken sunucuya olan bağlantıyı keselim
                if (istemci != null)
                {
                    istemci.MesajYolla("komut=cikis");
                    istemci.BaglantiyiKes();
                }
            }
        }

        /// Sunucudan bir mesaj alındığında buraya gelir        
        /// <param name="e">Alınan mesajla ilgili bilgiler</param>
        private void mesajAlindi(MesajAlmaArgumanlari e)
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
                    case "giris": //Yolladığımız giris mesajına karşılık gelen mesaj
                        komut_giris(parametreler["sonuc"]);
                        break;
                    case "toplumesaj": //Bir kişiden tüm gruba gelen mesaj
                        komut_toplumesaj(parametreler["nick"], parametreler["mesaj"]);
                        break;
                    case "kullanicigiris": //Bir kişi girdiğinde bize gelen bilgi
                        komut_kullanicigiris(parametreler["nick"]);
                        break;
                    case "kullanicicikis": //Bir kişi çıktığında bize gelen bilgi
                        komut_kullanicicikis(parametreler["nick"]);
                        break;
                    case "kullanicilistesi": //Tüm kullanıcıların listesi
                        komut_kullanicilistesi(parametreler["liste"]);
                        break;
                }
            }
            catch (Exception)
            { }
        }

        /// giris komutunu uygulayan fonksyon        
        /// <param name="sonuc">giriş sonucu</param>
        private void komut_giris(string sonuc)
        {
            //giriş başarılıysa gerekli kontrolleri aktif yap
            if (sonuc == "basarili")
            {
                buttonConnection.Image = Properties.Resources.baglantiOK;
                buttonConnection.Text = "Bağlı";
                if (timerSaat.Enabled)
                    SiparisMasaFormu_Load(null, null);
            }
            //giriş başarısızsa (nick kullanımdaysa) sonuna 1-9 arası rastgele bir sayı ekleyip yeniden giriş yap
            else
            {
                buttonConnection.Image = Properties.Resources.baglantiYOK;
                buttonConnection.Text = "Bağlan";

                if (Properties.Settings.Default.BilgisayarAdi == "")
                {
                    AdisyonNotuFormu nickForm = new AdisyonNotuFormu("Girilen bilgisayar adı kullanımda, lütfen başka bir bilgisayar adı giriniz");
                    nickForm.ShowDialog();
                    nick = nickForm.AdisyonNotu;
                }
                else
                {
                    nick = Properties.Settings.Default.BilgisayarAdi;
                }

                istemci.MesajYolla("komut=giris&nick=" + nick);

            }
        }

        /// toplumesaj komutunu uygulayan fonksyon        
        /// <param name="nick">Mesajı gönderen kullanıcının nick'i</param>
        /// <param name="mesaj">Gönderilen mesaj</param>
        private void komut_toplumesaj(string nick, string mesaj)
        {
            //gelen mesajı sohbet alanına ekle
            /* henüz işe yaramaz, burada gönderilecek mesaj yok hesap kısmında olabilir
            string mesajlar = txtTopluMesajlar.Text;
            mesajlar += "\r\n" + nick + ": " + mesaj;
            txtTopluMesajlar.Text = mesajlar;*/
        }

        /// kullanicigiris komutunu uygulayan fonksyon        
        /// <param name="nick"></param>
        private void komut_kullanicigiris(string nick)
        {
            //Eğer kullanıcı 'kullanıcılar' listesinde yoksa listeye ekle
            lock (kullanicilar)
            {
                if (!kullanicilar.Contains(nick))
                {
                    kullanicilar.Add(nick);
                }
                kullanicilar.Sort();
            }
        }

        /// kullanicicikis komutunu uygulayan fonksyon        
        /// <param name="nick"></param>
        private void komut_kullanicicikis(string nick)
        {
            //Eğer kullanıcı 'kullanıcılar' listesinde varsa listeden sil
            lock (kullanicilar)
            {
                if (kullanicilar.Contains(nick))
                {
                    kullanicilar.Remove(nick);
                }
            }
        }

        /// kullanicilistesi komutunu uygulayan fonksyon        
        /// <param name="liste">Sistemdeki kullanıcıların , ile ayırılmış listesi</param>
        private void komut_kullanicilistesi(string liste)
        {
            //Tüm kullanıcıları temizle ve gelen listeye göre yeniden oluştur
            try
            {
                //Gelen mesajı , ile ayır
                string[] kullaniciDizisi = liste.Split(',');
                lock (kullanicilar)
                {
                    //Mevcut listeyi temizle
                    kullanicilar.Clear();
                    //Gelen listeyi ekle
                    kullanicilar.AddRange(kullaniciDizisi);
                }
            }
            catch (Exception)
            { }
        }

        /// Toplu mesaj yollamak için        
        private void txtTopluMesajGonder()
        {
            //mesajı sunucuya yolla
            //istemci.MesajYolla("komut=toplumesaj&mesaj=" + txtTopluMesaj.Text);
        }

        public NameValueCollection mesajCoz(string mesaj)
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

        private void buttonConnection_Click(object sender, EventArgs e)
        {
            if (girisYapildi)
                return;
            if(Properties.Settings.Default.BilgisayarAdi == "")
            {
                AdisyonNotuFormu nickForm = new AdisyonNotuFormu("Bilgisayar adını giriniz");
                nickForm.ShowDialog();
                nick = nickForm.AdisyonNotu;
            }
            else
            {
                nick = Properties.Settings.Default.BilgisayarAdi;
            }

            buttonConnection.Text = "Bağlanıyor";

            if (!baglan())
            {
                using (KontrolFormu dialog = new KontrolFormu("Sunucuya bağlanılamadı, ayarları kontrol edip tekrar deneyiniz", false))
                {
                    dialog.ShowDialog();
                }
                buttonConnection.Text = "Bağlan";
                return;
            }
            else
            {
                girisYapildi = true;              
            }

            //Olaylara kaydol
            istemci.YeniMesajAlindi += new dgYeniMesajAlindi(istemci_YeniMesajAlindi);
            //Sunucuya giriş mesajı gönder
            istemci.MesajYolla("komut=giris&nick=" + nick);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            bool basarili = false;
            
            XMLAktarClient aktarimServeri = new XMLAktarClient();
            for (int i = 0; i < 8; i++)
            {
                basarili = aktarimServeri.ClientTarafi();
                if (!basarili)
                    break;
            }

            if (basarili)
            {
                if (!File.Exists("tempfiles.xml") || !File.Exists("kategoriler.xml") || !File.Exists("masaDizayn.xml") || !File.Exists("menu.xml") || !File.Exists("urunler.xml") || !File.Exists("gunler.xml") || !File.Exists("restoran.xml"))
                {
                    using (KontrolFormu dialog2 = new KontrolFormu("Dosyalarda eksik var, lütfen serverdaki dosyaları kontrol ediniz", false))
                    {
                        dialog2.ShowDialog();
                    }
                }
                else
                {
                    using (KontrolFormu dialog3 = new KontrolFormu("Dosya alımı başarılı, lütfen yeniden giriş yapınız", false))
                    {
                        dialog3.ShowDialog();
                    }
                }
            }
            else
            {
                using (KontrolFormu dialog4 = new KontrolFormu("Dosya alımı başarısız, server alıma ayarlanmamışsa ayarladıktan sonra lütfen tekrar deneyiniz", false))
                {
                    dialog4.ShowDialog();
                }
            }
        }
    }
}
