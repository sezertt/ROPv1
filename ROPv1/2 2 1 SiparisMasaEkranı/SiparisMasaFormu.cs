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
        private SPIAClient client;

        /// Kullanıcının seçtiği nick
        public string Nick
        {
            get { return nick; }
            set { nick = value; }
        }
        private string nick;

        /// Açık masaların listesi        
        private List<string> masalar;

        bool loadYapildiMi = false;

        int hangiDepartmanButonu = 0, hangiMasaDizayni = 200;

        List<Restoran> restoranListesi = new List<Restoran>();

        List<MasaDizayn> masaDizaynListesi = new List<MasaDizayn>();

        decimal toplamHesap = 0, kalanHesap = 0;

        public SiparisMasaFormu()
        {
            masalar = new List<string>();
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
            PinKoduFormu pinForm = new PinKoduFormu("Masa Görüntüleme");
            pinForm.ShowDialog();

            if (pinForm.dogru) //pin doğru
            {
                SiparisMenuFormu siparisForm;
                if (((Button)sender).BackColor == Color.White) // masa kapalı
                {
                    siparisForm = new SiparisMenuFormu(((Button)sender).Text, restoranListesi[hangiDepartmanButonu], pinForm.ayarYapanKisi, false, 0, 0);
                    siparisForm.ShowDialog();
                }
                else // masa acik
                {
                    SqlCommand cmd = SQLBaglantisi.getCommand("SELECT ToplamHesap,KalanHesap FROM Adisyon WHERE MasaAdi='" + ((Button)sender).Text + "' AND DepartmanAdi='" + restoranListesi[hangiDepartmanButonu].departmanAdi + "' AND AcikMi=1");
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

                    siparisForm = new SiparisMenuFormu(((Button)sender).Text, restoranListesi[hangiDepartmanButonu], pinForm.ayarYapanKisi, true, toplamHesap, kalanHesap);//burada masa numarasını da yolla
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
        } // düzenlenecek

        private void changeTableView(object sender, EventArgs e)
        {
            panel1.Controls[hangiDepartmanButonu].BackColor = Color.White;
            panel1.Controls[hangiDepartmanButonu].ForeColor = SystemColors.ActiveCaption;
            panel1.Controls[Convert.ToInt32(((Button)sender).Name)].BackColor = SystemColors.ActiveCaption;
            panel1.Controls[Convert.ToInt32(((Button)sender).Name)].ForeColor = Color.White;
            hangiDepartmanButonu = Convert.ToInt32(((Button)sender).Name);
            hangiMasaDizayni = Convert.ToInt32(((Button)sender).Tag);

            if (Properties.Settings.Default.Server == 2)
            {
                if (hangiMasaDizayni > masaDizaynListesi.Count - 1)
                {
                    tablePanel.Controls.Clear();
                    tablePanel.Tag = -1;
                    return;
                }
                else if ((int)tablePanel.Tag != hangiMasaDizayni) //eğer seçili masa planı zaten ekrandaysa yenisi koyulmasın, ekranda değilse eskiler silinip yenisi eklensin
                {
                    tablePanel.RowCount = 6;
                    tablePanel.ColumnCount = 7;
                    tablePanel.Controls.Clear();
                    masalar.Clear(); // acik masalari tutan listeyi sıfırlıyoruz, önceki açık masalardan kurtuluyoruz

                    SqlCommand cmd = SQLBaglantisi.getCommand("SELECT MasaAdi FROM Adisyon WHERE DepartmanAdi='" + restoranListesi[hangiDepartmanButonu].departmanAdi + "' AND AcikMi=1");
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        try
                        {
                            masalar.Add(dr.GetString(0));
                        }
                        catch { }
                    }

                    cmd.Connection.Close();
                    cmd.Connection.Dispose();

                    for (int i = 0; i < 6; i++)
                    {
                        if (masaDizaynListesi[hangiMasaDizayni].masaPlanIsmi == "")
                            break;
                        for (int j = 0; j < 7; j++)
                        {
                            if (masaDizaynListesi[hangiMasaDizayni].masaYerleri[i][j] != null)
                            {
                                Button buttonTable = new Button();
                                buttonTable.Text = masaDizaynListesi[hangiMasaDizayni].masaYerleri[i][j];

                                buttonTable.UseVisualStyleBackColor = false;

                                buttonTable.Font = new Font("Arial", 21.75F, FontStyle.Bold);
                                buttonTable.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                                buttonTable.Click += siparisButonuBasildi;
                                tablePanel.Controls.Add(buttonTable, j, i);
                                tablePanel.Tag = hangiMasaDizayni;

                                buttonTable.Name = buttonTable.Text;

                                bool masaAcikMi = false;
                                for (int x = 0; x < masalar.Count; x++)
                                {
                                    if (buttonTable.Text == masalar[x])
                                    {
                                        masaAcikMi = true;
                                        break;
                                    }
                                }
                                if (masaAcikMi)
                                {
                                    buttonTable.BackColor = Color.Firebrick;
                                    buttonTable.ForeColor = Color.White;
                                }
                                else
                                {
                                    buttonTable.BackColor = Color.White;
                                    buttonTable.ForeColor = SystemColors.ActiveCaption;
                                }
                            }
                        }
                    }

                    for (int j = 6; j > 0; j--)
                    {
                        if (masaDizaynListesi[hangiMasaDizayni].masaPlanIsmi == "")
                            break;

                        bool sutunBos = true;
                        for (int i = 5; i > 0; i--)
                        {
                            if (masaDizaynListesi[hangiMasaDizayni].masaYerleri[i][j] != null)
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
                        if (masaDizaynListesi[hangiMasaDizayni].masaPlanIsmi == "")
                            break;
                        bool sutunBos = true;
                        for (int i = 6; i > 0; i--)
                        {
                            if (masaDizaynListesi[hangiMasaDizayni].masaYerleri[j][i] != null)
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
            } // departman gösterimi serverda gerçekleşiyorsa
            else// departman gösterimi clientlarda gerçekleşiyorsa
            {
                if ((int)tablePanel.Tag != hangiMasaDizayni) //eğer seçili masa planı zaten ekrandaysa yenisi koyulmasın, ekranda değilse eskiler silinip yenisi eklensin
                {
                    client.MesajYolla("komut=departman&departmanAdi=" + restoranListesi[hangiDepartmanButonu].departmanAdi);
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
        } // düzenlenecek
                
        private void SiparisMasaFormu_Load(object sender, EventArgs e)
        {
            if (sender != null)
            {
                if (Properties.Settings.Default.Server != 2)
                {
                    buttonConnection_Click(null, null);

                    if (Properties.Settings.Default.BilgisayarAdi != "")
                    {
                        buttonName.Visible = true;
                        buttonName.Text = Properties.Settings.Default.BilgisayarAdi;
                    }
                }
                else
                {
                    buttonConnection.Visible = false;
                    buttonUpdate.Visible = false;
                }

                labelSaat.Text = DateTime.Now.ToString("HH:mm:ss", new CultureInfo("tr-TR"));
                labelGun.Text = DateTime.Now.ToString("dddd", new CultureInfo("tr-TR"));
                labelTarih.Text = DateTime.Now.Date.ToString("d MMMM yyyy", new CultureInfo("tr-TR"));
                timerSaat.Start();

                if (!girisYapildi && Properties.Settings.Default.Server != 2)
                    return;
            }

            loadYapildiMi = true;

            if (File.Exists("restoran.xml"))
            {
                XmlLoad<Restoran> loadInfo = new XmlLoad<Restoran>();
                Restoran[] info = loadInfo.LoadRestoran("restoran.xml");

                restoranListesi.AddRange(info);

                XmlLoad<MasaDizayn> loadInfoMasa = new XmlLoad<MasaDizayn>();
                MasaDizayn[] infoMasa = loadInfoMasa.LoadRestoran("masaDizayn.xml");

                //kendi listemize atıyoruz
                masaDizaynListesi.AddRange(infoMasa);

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

                    departmanButton.Tag = 200;
                    for (int j = 0; j < masaDizaynListesi.Count; j++)
                    {
                        if (restoranListesi[i].departmanEkrani == masaDizaynListesi[j].masaPlanIsmi)
                        {
                            if (i == 0)
                            {
                                hangiMasaDizayni = j;
                            }
                            departmanButton.Tag = j;
                            break;
                        }
                    }

                    departmanButton.Height = panel1.Height;
                    departmanButton.Width = panel1.Width / restoranListesi.Count;
                    departmanButton.Dock = DockStyle.Right;
                    departmanButton.Click += changeTableView;
                    panel1.Controls.Add(departmanButton);
                }
                tablePanel.Tag = -1;
                if (Properties.Settings.Default.Server == 2)
                {
                    Button birinciDepartman = panel1.Controls["0"] as Button;
                    birinciDepartman.PerformClick();
                }
                else
                {
                    client.MesajYolla("komut=departman&departmanAdi=" + restoranListesi[hangiDepartmanButonu].departmanAdi);
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
                client = new SPIAClient(ip, port);
                return client.Baglan();
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
                if (client != null)
                {
                    client.MesajYolla("komut=cikis");
                    client.BaglantiyiKes();
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
                    case "toplumesaj": //tüm gruba gelen mesaj
                        komut_toplumesaj(parametreler["mesaj"]);
                        break;
                    case "departman": //Yolladığımız giris mesajına karşılık gelen mesaj
                        komut_departman(parametreler["masa"]);
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
                buttonName.Visible = true;
                buttonName.Text = Properties.Settings.Default.BilgisayarAdi;
                if (!loadYapildiMi)
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

                client.MesajYolla("komut=giris&nick=" + nick);

            }
        }

        /// Departmandaki dolu masa bilgisini alan fonksiyon  
        private void komut_departman(string acikMasalar)
        {
            masalar.Clear();
            try
            {
                //Gelen mesajı , ile ayır
                string[] kullaniciDizisi = acikMasalar.Split(',');
                masalar.AddRange(kullaniciDizisi);
            }
            catch (Exception)
            { }

            tablePanel.RowCount = 6;
            tablePanel.ColumnCount = 7;
            tablePanel.Controls.Clear();
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (masaDizaynListesi[hangiMasaDizayni].masaYerleri[i][j] != null)
                    {
                        Button buttonTable = new Button();
                        buttonTable.Text = masaDizaynListesi[hangiMasaDizayni].masaYerleri[i][j];

                        buttonTable.UseVisualStyleBackColor = false;

                        buttonTable.Font = new Font("Arial", 21.75F, FontStyle.Bold);
                        buttonTable.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                        buttonTable.Click += siparisButonuBasildi;
                        tablePanel.Controls.Add(buttonTable, j, i);
                        tablePanel.Tag = hangiMasaDizayni;

                        buttonTable.Name = buttonTable.Text;

                        bool masaAcikMi = false;
                        for (int x = 0; x < masalar.Count; x++)
                        {
                            if (buttonTable.Text == masalar[x])
                            {
                                masaAcikMi = true;
                                break;
                            }
                        }
                        if (masaAcikMi)
                        {
                            buttonTable.BackColor = Color.Firebrick;
                            buttonTable.ForeColor = Color.White;
                        }
                        else
                        {
                            buttonTable.BackColor = Color.White;
                            buttonTable.ForeColor = SystemColors.ActiveCaption;
                        }
                    }
                }
            }

            for (int j = 6; j > 0; j--)
            {
                if (masaDizaynListesi[hangiMasaDizayni].masaPlanIsmi == "")
                    break;

                bool sutunBos = true;
                for (int i = 5; i > 0; i--)
                {
                    if (masaDizaynListesi[hangiMasaDizayni].masaYerleri[i][j] != null)
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
                if (masaDizaynListesi[hangiMasaDizayni].masaPlanIsmi == "")
                    break;
                bool sutunBos = true;
                for (int i = 6; i > 0; i--)
                {
                    if (masaDizaynListesi[hangiMasaDizayni].masaYerleri[j][i] != null)
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

        /// toplumesaj komutunu uygulayan fonksyon        
        /// <param name="nick">Mesajı gönderen kullanıcının nick'i</param>
        /// <param name="mesaj">Gönderilen mesaj</param>
        private void komut_toplumesaj(string mesaj)
        {
            switch (mesaj)
            {
                case "ServerKapandi":
                    if (client != null)
                    {
                        client.BaglantiyiKes2();
                    }
                    girisYapildi = false;
                    buttonConnection.Image = Properties.Resources.baglantiYOK;
                    buttonConnection.Text = "Bağlan";
                    using (KontrolFormu dialog = new KontrolFormu("Dikkat!\nSunucu bağlantısı koptu!", false))
                    {
                        dialog.ShowDialog();
                    }

                    break;
            }
        }

        /// Toplu mesaj yollamak için        
        private void txtTopluMesajGonder()
        {
            //mesajı sunucuya yolla
            //istemci.MesajYolla("komut=toplumesaj&mesaj=" + txtTopluMesaj.Text);
        }  // düzenlenecek

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

            if (Properties.Settings.Default.BilgisayarAdi == "")
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
                buttonConnection.Image = Properties.Resources.baglantiYOK;
                buttonConnection.Text = "Bağlan";
                return;
            }
            else
            {
                girisYapildi = true;
            }

            //Olaylara kaydol
            client.YeniMesajAlindi += new dgYeniMesajAlindi(istemci_YeniMesajAlindi);
            //Sunucuya giriş mesajı gönder
            client.MesajYolla("komut=giris&nick=" + nick);
        }

        private void buttonName_Click(object sender, EventArgs e)
        {
            AdisyonNotuFormu nickForm = new AdisyonNotuFormu("Bilgisayar adını giriniz");
            nickForm.ShowDialog();
            nick = nickForm.AdisyonNotu;
            client.MesajYolla("komut=giris&nick=" + nick);
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            bool basarili = false;

            XMLAktarClient aktarimServeri = new XMLAktarClient();
            for (int i = 0; i < 7; i++)
            {
                basarili = aktarimServeri.ClientTarafi();
                if (!basarili)
                    break;
            }

            if (basarili)
            {
                if (!File.Exists("tempfiles.xml") || !File.Exists("kategoriler.xml") || !File.Exists("masaDizayn.xml") || !File.Exists("menu.xml") || !File.Exists("urunler.xml") || !File.Exists("restoran.xml"))
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