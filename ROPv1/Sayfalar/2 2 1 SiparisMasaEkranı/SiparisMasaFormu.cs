﻿using System;
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
using SPIA.Server;

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

        // SPIA kütüphanesini kullanarak SPIA sunucusuna bağlı olan istemci nesnesi        
        private SPIAClient client;

        // Kullanıcının seçtiği nick
        public string Nick
        {
            get { return nick; }
            set { nick = value; }
        }
        private string nick;

        public bool masayiIslemYapmadanKapat = false; // masa değiştirme işlemi gerçekleştiğinde, masa(menü ekranı) başka clientta açıksa, o clientın menü ekranı kapanırken masa rengi işlemi yapılmamalı, bunu bu değişken ile belirliyoruz

        // Açık masaların listesi        
        private List<string> masalar;

        public KontrolFormu dialog2;

        bool loadYapildiMi = false, acikMasaVarsaYapma = false;

        int hangiDepartmanButonu = 0, hangiMasaDizayni = 200;

        string hangiMasa, ayarYapanKisi;

        List<Restoran> restoranListesi = new List<Restoran>();

        List<MasaDizayn> masaDizaynListesi = new List<MasaDizayn>();

        List<ROPv1.GirisEkrani.BagliKullanicilar> kullanicilar;

        public Button hangiMasaButonunaBasildi;

        public SiparisMenuFormu siparisMenuForm;

        public string viewdakiDepartmaninAdi;

        PinKoduFormu pinForm;

        public SiparisMasaFormu(List<ROPv1.GirisEkrani.BagliKullanicilar> AlinanKullanicilar)
        {
            kullanicilar = AlinanKullanicilar;
            masalar = new List<string>();
            InitializeComponent();
        }

        #region Eventler

        private void myPannel_SizeChanged(object sender, EventArgs e)
        {
            if (panel1.Controls.Count > 0)
            {
                panel1.SuspendLayout();

                foreach (Control ctrl in panel1.Controls)
                {
                    if (ctrl is Button)
                    {
                        ctrl.Width = panel1.ClientSize.Width / 6;

                        while (ctrl.Width < System.Windows.Forms.TextRenderer.MeasureText(ctrl.Text, new Font(ctrl.Font.FontFamily, ctrl.Font.Size, ctrl.Font.Style)).Width)
                        {
                            ctrl.Font = new Font(ctrl.Font.FontFamily, ctrl.Font.Size - 0.5f, ctrl.Font.Style);
                        }
                    }
                }

                panel1.ResumeLayout();
            }
        }

        private void siparisButonuBasildi(object sender, EventArgs e)
        {
            if (pinForm != null)
            {
                if (pinForm.Visible)
                {
                    pinForm.BringToFront();
                    return;
                }
            }

            if (dialog2 != null)
            {
                if (dialog2.Visible)
                {
                    dialog2.BringToFront();
                    return;
                }
            }

            if (acikMasaVarsaYapma)
            {
                acikMasaVarsaUyariVerFormuOneGetir();
                return;
            }

            pinForm = new PinKoduFormu("Masa Görüntüleme", this);
            pinForm.Show();

            hangiMasaButonunaBasildi = sender as Button;
            hangiMasa = ((Button)sender).Text;
        }

        //departman değiştirme butonlarından birine basıldıysa
        private void changeTableView(object sender, EventArgs e)
        {
            if (pinForm != null)
            {
                if (pinForm.Visible)
                {
                    pinForm.BringToFront();
                    return;
                }
            }

            if (dialog2 != null)
            {
                if (dialog2.Visible)
                {
                    dialog2.BringToFront();
                    return;
                }
            }

            if (acikMasaVarsaYapma)
            {
                acikMasaVarsaUyariVerFormuOneGetir();
                return;
            }
            panel1.Controls[hangiDepartmanButonu].BackColor = Color.White;
            panel1.Controls[hangiDepartmanButonu].ForeColor = SystemColors.ActiveCaption;
            panel1.Controls[Convert.ToInt32(((Button)sender).Name)].BackColor = SystemColors.ActiveCaption;
            panel1.Controls[Convert.ToInt32(((Button)sender).Name)].ForeColor = Color.White;
            hangiDepartmanButonu = Convert.ToInt32(((Button)sender).Name);
            hangiMasaDizayni = Convert.ToInt32(((Button)sender).Tag);

            viewdakiDepartmaninAdi = restoranListesi[hangiDepartmanButonu].departmanAdi;

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
            if (pinForm != null)
            {
                if (pinForm.Visible)
                {
                    pinForm.BringToFront();
                    return;
                }
            }

            if (dialog2 != null)
            {
                if (dialog2.Visible)
                {
                    dialog2.BringToFront();
                    return;
                }
            }

            if (acikMasaVarsaYapma)
            {
                acikMasaVarsaUyariVerFormuOneGetir();
                return;
            }

            if (Properties.Settings.Default.Server == 2) // bu makina server
                this.Close();
            else
                Application.Exit();
        }

        private void timerSaat_Tick(object sender, EventArgs e)
        {
            labelSaat.Text = DateTime.Now.ToString("HH:mm:ss", new CultureInfo("tr-TR"));
        }

        private void buttonAdisyon_Click(object sender, EventArgs e)
        {
            if (pinForm != null)
            {
                if (pinForm.Visible)
                {
                    pinForm.BringToFront();
                    return;
                }
            }

            if (dialog2 != null)
            {
                if (dialog2.Visible)
                {
                    dialog2.BringToFront();
                    return;
                }
            }

            if (acikMasaVarsaYapma)
            {
                acikMasaVarsaUyariVerFormuOneGetir();
                return;
            }

            pinForm = new PinKoduFormu("Adisyon Görüntüleme", this);
            pinForm.Show();
        } // düzenlenecek

        public void gelenPinDogruMu(bool pinDogruMu, string ayarYapanKisi, string yapilacakIslemNe)
        {
            this.ayarYapanKisi = ayarYapanKisi;

            if (pinDogruMu)
            {
                if (yapilacakIslemNe == "Masa Görüntüleme")
                {
                    if (Properties.Settings.Default.Server == 2) // server
                    {
                        SqlCommand cmd = SQLBaglantisi.getCommand("SELECT OdemeYapiliyor FROM Adisyon WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Adisyon.MasaAdi='" + hangiMasa + "' AND Adisyon.DepartmanAdi='" + viewdakiDepartmaninAdi + "'");

                        SqlDataReader dr = cmd.ExecuteReader();

                        dr.Read();

                        bool masaSerbestMi = false;

                        try
                        {
                            masaSerbestMi = dr.GetBoolean(0);
                        }
                        catch
                        {
                            masaSerbestMi = false;
                        }

                        cmd.Connection.Close();
                        cmd.Connection.Dispose();

                        if (masaSerbestMi)
                        {
                            komut_masaGirilebilirMi("false");
                        }
                        else
                        {
                            komut_masaGirilebilirMi("True");
                        }
                    }
                    else
                    {
                        client.MesajYolla("komut=masaGirilebilirMi" + "&masa=" + hangiMasa + "&departmanAdi=" + viewdakiDepartmaninAdi);
                    }
                }
                else if (yapilacakIslemNe == "Adisyon Görüntüleme")  // burada adisyon sayfası oluşturulacak ve ona geçilecek
                {

                }
            }
        }

        //form load
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

                int departmanButtonWidth = 0;

                //departman butonlarını ekrana ekliyoruz
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

                    if (restoranListesi.Count < 6)
                    {
                        departmanButton.Height = panel1.Height;
                        departmanButton.Width = panel1.Width / restoranListesi.Count;
                    }
                    else
                    {
                        departmanButton.Height = panel1.Height;
                        departmanButton.Width = panel1.Width / 6;
                    }

                    departmanButtonWidth = departmanButton.Width;

                    departmanButton.Click += changeTableView;
                    departmanButton.Anchor = AnchorStyles.Top;
                    departmanButton.Margin = new Padding(0, 0, 0, 0);
                    panel1.Controls.Add(departmanButton);
                }

                panel1.AutoScrollMinSize = new System.Drawing.Size(departmanButtonWidth * restoranListesi.Count, 30);

                tablePanel.Tag = -1;

                if (panel1.HorizontalScroll.Visible)
                {
                    panel1.Height += 17;
                    tablePanel.Location = new Point(tablePanel.Location.X, tablePanel.Location.Y + 17);
                    tablePanel.Height -= 17;
                }

                //Masa butonlarını eklemek için serversa direk ilk departman butonuna kod ile basıyoruz, clientsa servera sorarak açık masa bilgileriyle birlikte alıyoruz
                Button birinciDepartman = panel1.Controls["0"] as Button;
                birinciDepartman.PerformClick();
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

        private void buttonConnection_Click(object sender, EventArgs e)
        {
            if (acikMasaVarsaYapma)
            {
                acikMasaVarsaUyariVerFormuOneGetir();
                return;
            }

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
                KontrolFormu dialog = new KontrolFormu("Sunucuya bağlanılamadı, ayarları kontrol edip tekrar deneyiniz", false);
                dialog.Show();

                buttonConnection.Image = Properties.Resources.baglantiYOK;
                buttonConnection.Text = "Bağlan";
                return;
            }
            else
            {
                girisYapildi = true;
            }

            //Olaylara kaydol
            client.YeniMesajAlindi += new dgYeniMesajAlindi(istemciYeniMesajAlindi);
            //Sunucuya giriş mesajı gönder
            client.MesajYolla("komut=giris&nick=" + nick);
        }

        private void buttonName_Click(object sender, EventArgs e)
        {
            if (acikMasaVarsaYapma)
            {
                acikMasaVarsaUyariVerFormuOneGetir();
                return;
            }
            AdisyonNotuFormu nickForm = new AdisyonNotuFormu("Bilgisayar adını giriniz");
            nickForm.ShowDialog();
            nick = nickForm.AdisyonNotu;
            client.MesajYolla("komut=giris&nick=" + nick);
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (acikMasaVarsaYapma)
            {
                acikMasaVarsaUyariVerFormuOneGetir();
                return;
            }

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
                    dialog2 = new KontrolFormu("Dosyalarda eksik var, lütfen serverdaki dosyaları kontrol ediniz", false);
                    dialog2.Show();
                }
                else
                {
                    dialog2 = new KontrolFormu("Dosya alımı başarılı, lütfen yeniden giriş yapınız", false);
                    dialog2.Show();
                }
            }
            else
            {
                KontrolFormu dialog4 = new KontrolFormu("Dosya alımı başarısız, server alıma ayarlanmamışsa ayarladıktan sonra lütfen tekrar deneyiniz", false);
                dialog4.Show();

            }
        }

        #endregion

        #region Serverdan Clientlara

        public void serverdanSiparisIkramVeyaIptal(string masa, string departman, string komut, string miktar, string yemekAdi, string dusulecekDeger, string ikramYeniMiEskiMi)
        {
            if (ikramYeniMiEskiMi == null)
            {
                tumKullanicilaraMesajYolla("komut=" + komut + "&masa=" + masa + "&departmanAdi=" + departman + "&miktar=" + miktar + "&yemekAdi=" + yemekAdi + "&dusulecekDeger=" + dusulecekDeger);
            }
            else
            {
                tumKullanicilaraMesajYolla("komut=" + komut + "&masa=" + masa + "&departmanAdi=" + departman + "&miktar=" + miktar + "&yemekAdi=" + yemekAdi + "&dusulecekDeger=" + dusulecekDeger + "&ikramYeniMiEskiMi=" + ikramYeniMiEskiMi);
            }
        }

        public void serverdanMasaDegisikligi(string masa, string departmanAdi, string yeniMasa, string yeniDepartmanAdi, string komut)
        {
            //Tüm kullanıcılara masa değiştir mesajı gönderelim
            tumKullanicilaraMesajYolla("komut=" + komut + "&masa=" + masa + "&departmanAdi=" + departmanAdi + "&yeniMasa=" + yeniMasa + "&yeniDepartmanAdi=" + yeniDepartmanAdi);
        }

        public void serverdanHesapOdeme(string masa, string departmanAdi, string komut)
        {
            // tüm kullanıcıları bilgilendir
            tumKullanicilaraMesajYolla("komut=hesapOdeniyor&masa=" + masa + "&departmanAdi=" + departmanAdi);
        }

        public void tumKullanicilaraMesajYolla(string mesaj)
        {
            ROPv1.GirisEkrani.BagliKullanicilar[] kullaniciDizisi = null;
            //Eşzamanlı erişimlere karşı koleksiyonu kilitleyelim
            lock (kullanicilar)
            {
                //Listedeki tüm kullanıcıları bir diziye atalım
                kullaniciDizisi = kullanicilar.ToArray();
            }
            //Tüm kullanıcılara istenilen mesajı gönderelim
            foreach (ROPv1.GirisEkrani.BagliKullanicilar kul in kullaniciDizisi)
            {
                kul.Client.MesajYolla(mesaj);
            }
        }

        #endregion

        void istemciYeniMesajAlindi(MesajAlmaArgumanlari e)
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
                if (client != null && girisYapildi == true)
                {
                    client.MesajYolla("komut=cikis");
                    client.BaglantiyiKes();
                }
            }
        }

        // Sunucudan bir mesaj alındığında buraya gelir        
        private void mesajAlindi(MesajAlmaArgumanlari e)
        {
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
                    case "siparis":
                        komut_siparis(parametreler["masa"], parametreler["departmanAdi"], parametreler["miktar"], parametreler["yemekAdi"], parametreler["dusulecekDeger"]);
                        break;
                    case "iptal": // serverdan iptal isteğinin sonucu geldiğinde
                        komut_iptal(parametreler["masa"], parametreler["departmanAdi"], parametreler["miktar"], parametreler["yemekAdi"], parametreler["dusulecekDeger"], parametreler["ikramYeniMiEskiMi"]);
                        break;
                    case "hesapOdeniyor": // yeni masa açıldığı bilgisi geldiğinde
                        komut_hesapOdeniyor(parametreler["masa"], parametreler["departmanAdi"]);
                        break;
                    case "masaGirilebilirMi": // yeni masa açıldığı bilgisi geldiğinde
                        komut_masaGirilebilirMi(parametreler["cevap"]);
                        break;
                    case "masaDegistir": // masa değişikliği bilgisi geldiğinde eğer o masalar bizde açıksa kapatmalıyız
                    case "urunTasindi": // ürün aktarma bilgisi geldiğinde eğer o masalar bizde açıksa kapatmalıyız
                        komut_masaDegisti(parametreler["masa"], parametreler["departmanAdi"], parametreler["yeniMasa"], parametreler["yeniDepartmanAdi"], parametreler["komut"]);
                        break;
                    case "ikram": // serverdan ikram isteğinin sonucu geldiğinde
                        komut_ikram(parametreler["masa"], parametreler["departmanAdi"], parametreler["miktar"], parametreler["yemekAdi"], parametreler["dusulecekDeger"]);
                        break;
                    case "ikramIptal": // serverdan ikram iptal isteğinin sonucu geldiğinde
                        komut_ikramIptal(parametreler["masa"], parametreler["departmanAdi"], parametreler["miktar"], parametreler["yemekAdi"], parametreler["dusulecekDeger"], parametreler["ikramYeniMiEskiMi"]);
                        break;
                    case "BulunanYazicilar": //Yolladığımız giris mesajına karşılık gelen mesaj
                        komut_yazicilarGeldi(parametreler["adisyonYazicilari"], parametreler["digerYazicilar"], parametreler["garson"], parametreler["acilisZamani"]);
                        break;
                    case "giris": //Yolladığımız giris mesajına karşılık gelen mesaj
                        komut_giris(parametreler["sonuc"]);
                        break;
                    case "IndirimOnay": //Yolladığımız giris mesajına karşılık gelen mesaj
                        komut_IndirimOnay(parametreler["odemeTipi"], parametreler["odemeMiktari"]);
                        break;
                    case "OdemeOnay": //Yolladığımız giris mesajına karşılık gelen mesaj
                        komut_OdemeOnay(parametreler["odemeTipi"], parametreler["odemeMiktari"], parametreler["secilipOdenenSiparisBilgileri"]);
                        break;
                    case "LoadSiparis": // serverdan siparis bilgileri geldiğinde
                        komut_loadSiparis(parametreler["siparisBilgileri"]);
                        break;
                    case "OdenenleriGonder": // serverdan siparis bilgileri geldiğinde
                        komut_OdenenleriGonder(parametreler["siparisBilgileri"], parametreler["odemeBilgileri"]);
                        break;
                    case "toplumesaj": //tüm gruba gelen mesaj - server kapandığında(şimdilik)
                        komut_topluMesaj(parametreler["mesaj"]);
                        break;
                    case "departman": //açık masa bilgilerini geldiğinde
                        try
                        {
                            //burada masa değiştir formdaki komut_departmana yönlendirme yapılmalı
                            siparisMenuForm.masaDegistirForm.komut_departman(parametreler["masa"]);
                            siparisMenuForm.masaDegistirForm.BringToFront();
                        }
                        catch
                        {
                            komut_departman(parametreler["masa"]);
                        }
                        break;
                    case "masaAcildi": // yeni masa açıldığı bilgisi geldiğinde
                        komut_masaAcildi(parametreler["masa"], parametreler["departmanAdi"]);
                        try
                        {
                            //burada masa değiştir formdaki komut_masaAcildi yönlendirme yapılmalı
                            siparisMenuForm.masaDegistirForm.komut_masaAcildi(parametreler["masa"], parametreler["departmanAdi"]);
                        }
                        catch { }
                        break;
                    case "masaKapandi": // yeni masa kapandığı bilgisi geldiğinde
                        komut_masaKapandi(parametreler["masa"], parametreler["departmanAdi"]);
                        try
                        {
                            //burada masa değiştir formdaki komut_masaKapandi yönlendirme yapılmalı (eğer siparismenuformun masa değiştir formu açıksa diye) 
                            siparisMenuForm.masaDegistirForm.komut_masaKapandi(parametreler["masa"], parametreler["departmanAdi"]);
                        }
                        catch { }
                        break;
                    case "AdisyonNotu": // istenilen adisyon notu geldiğinde
                        komut_adisyonNotu(parametreler["adisyonNotu"]);
                        break;
                    case "IslemHatasi": // bir hata oluştuğunda
                        komut_IslemHatasi(parametreler["hata"]);
                        break;
                }
            }
            catch (Exception)
            {
                komut_IslemHatasi(parametreler["hata"]);
            }
        }

        #region Clienttan Servera

        /// Masaformu vasıtasıyla sunucuya bir mesaj yollamak içindir.        
        public void menuFormundanServeraYolla(string masa, string departman, string komut)
        {
            client.MesajYolla("komut=" + komut + "&masa=" + masa + "&departmanAdi=" + departman);
        }

        public void hesapFormundanOdeme(string masa, string departman, string komut, int odemeTipi, decimal odemeMiktari, StringBuilder secilipOdenenSiparisBilgileri)
        {
            client.MesajYolla("komut=" + komut + "&masa=" + masa + "&departmanAdi=" + departman + "&odemeTipi=" + odemeTipi.ToString() + "&odemeMiktari=" + odemeMiktari.ToString() + "&secilipOdenenSiparisBilgileri=" + secilipOdenenSiparisBilgileri);
        }

        public void hesapFormundanOdemeBitti(string masa, string departman, string komut, bool odenmeyenSiparisVarMiGelen)
        {
            int odenmeyenSiparisVarMi = 0; // ödenmeyen sipariş yok siparişleri ödendi ye çevir ve ödemeyapılıyoru 0 yap

            if (odenmeyenSiparisVarMiGelen)
                odenmeyenSiparisVarMi = 1; // ödenmeyen sipariş var sadece ödemeyapılıyoru 0 yap

            client.MesajYolla("komut=" + komut + "&masa=" + masa + "&departmanAdi=" + departman + "&odenmeyenSiparisVarMi=" + odenmeyenSiparisVarMi);
        }

        public void hesapFormundanYazicilariIste(string komut, string masaAdi, string departmanAdi)
        {
            client.MesajYolla("komut=" + komut + "&masa=" + masaAdi + "&departmanAdi=" + departmanAdi);
        }

        public void hesapFormundanIndirim(string masa, string departman, string komut, int odemeTipi, decimal odemeMiktari)
        {
            client.MesajYolla("komut=" + komut + "&masa=" + masa + "&departmanAdi=" + departman + "&odemeTipi=" + odemeTipi + "&odemeMiktari=" + odemeMiktari);
        }

        /// Masaformu vasıtasıyla sunucuya bir mesaj yollamak içindir.        
        public void menuFormundanServeraSiparisYolla(string masa, string departman, string komut, string miktar, string yemekAdi, string siparisiGirenKisi, string dusulecekDeger, string adisyonNotu, string ikramYeniMiEskiMi)
        {
            if (ikramYeniMiEskiMi == null)
                client.MesajYolla("komut=" + komut + "&masa=" + masa + "&departmanAdi=" + departman + "&miktar=" + miktar + "&yemekAdi=" + yemekAdi + "&siparisiGirenKisi=" + siparisiGirenKisi + "&dusulecekDeger=" + dusulecekDeger + "&adisyonNotu=" + adisyonNotu);
            else
                client.MesajYolla("komut=" + komut + "&masa=" + masa + "&departmanAdi=" + departman + "&miktar=" + miktar + "&yemekAdi=" + yemekAdi + "&siparisiGirenKisi=" + siparisiGirenKisi + "&dusulecekDeger=" + dusulecekDeger + "&adisyonNotu=" + adisyonNotu + "&ikramYeniMiEskiMi=" + ikramYeniMiEskiMi);
        }

        /// Masaformu vasıtasıyla sunucuya bir mesaj yollamak içindir.        
        public void menuFormundanServeraUrunTasinacakBilgisiGonder(string masa, string departman, string komut, string yeniMasa, string yeniDepartman, string siparisiGirenKisi, StringBuilder aktarmaBilgileri)
        {
            client.MesajYolla("komut=" + komut + "&masa=" + masa + "&departmanAdi=" + departman + "&yeniMasa=" + yeniMasa + "&yeniDepartmanAdi=" + yeniDepartman + "&siparisiGirenKisi=" + siparisiGirenKisi + "&aktarmaBilgileri=" + aktarmaBilgileri);
        }

        /// Masaformu vasıtasıyla sunucuya bir mesaj yollamak içindir.        
        public void siparisListesiBos(string masa, string departman, string komut)
        {
            client.MesajYolla("komut=" + komut + "&masa=" + masa + "&departmanAdi=" + departman);
        }

        /// Masaformu vasıtasıyla sunucuya bir mesaj yollamak içindir.        
        public void serveraSiparis(string masa, string departman, string komut, string miktar, string yemekAdi, string siparisiGirenKisi, string dusulecekDeger, string adisyonNotu)
        {
            client.MesajYolla("komut=" + komut + "&masa=" + masa + "&departmanAdi=" + departman + "&miktar=" + miktar + "&yemekAdi=" + yemekAdi + "&siparisiGirenKisi=" + siparisiGirenKisi + "&dusulecekDeger=" + dusulecekDeger + "&adisyonNotu=" + adisyonNotu);
        }

        public void serveraNotuYolla(string masa, string departman, string komut, string adisyonNotu)
        {
            client.MesajYolla("komut=" + komut + "&masa=" + masa + "&departmanAdi=" + departman + "&adisyonNotu=" + adisyonNotu);
        }

        public void menuFormundanServeraMasaDegisikligi(string yeniMasa, string yeniDepartman, string eskiMasa, string eskiDepartman, int yapilmasiGerekenIslem, string komut)
        {
            client.MesajYolla("komut=" + komut + "&yeniMasa=" + yeniMasa + "&yeniDepartmanAdi=" + yeniDepartman + "&eskiMasa=" + eskiMasa + "&eskiDepartmanAdi=" + eskiDepartman + "&yapilmasiGereken=" + yapilmasiGerekenIslem.ToString());
        }

        #endregion

        //siparis masa formu kapatılırken bu method çalışıyor
        public void siparisFormKapandiginda()
        {
            if (!masayiIslemYapmadanKapat)
            {
                if (Properties.Settings.Default.Server != 2) // client
                {
                    if (siparisMenuForm.masaAcikMi)
                    {
                        switch (siparisMenuForm.masaDegisti)
                        {
                            case 2: // departman değişmedi 1 masa açık
                                client.MesajYolla("komut=masaKapandi&masa=" + hangiMasa + "&departmanAdi=" + restoranListesi[hangiDepartmanButonu].departmanAdi);

                                client.MesajYolla("komut=masaAcildi&masa=" + siparisMenuForm.yeniMasaninAdi + "&departmanAdi=" + restoranListesi[hangiDepartmanButonu].departmanAdi);
                                break;
                            case 3: // 1 masa açık departmanda değişti
                                client.MesajYolla("komut=masaKapandi&masa=" + hangiMasa + "&departmanAdi=" + restoranListesi[hangiDepartmanButonu].departmanAdi);
                                break;
                            default:
                                Button tablebutton = tablePanel.Controls[hangiMasa] as Button;
                                if (tablebutton.BackColor != Color.Firebrick)
                                    client.MesajYolla("komut=masaAcildi&masa=" + hangiMasa + "&departmanAdi=" + restoranListesi[hangiDepartmanButonu].departmanAdi);
                                break;
                        }
                    }
                    else
                    {
                        client.MesajYolla("komut=masaKapandi&masa=" + hangiMasa + "&departmanAdi=" + restoranListesi[hangiDepartmanButonu].departmanAdi);
                    }
                }
                else // server
                {
                    if (siparisMenuForm.urunTasinirkenYeniMasaOlusturulduysaOlusanMasaninAdi != "")
                    {
                        Button tablebutton = tablePanel.Controls[siparisMenuForm.urunTasinirkenYeniMasaOlusturulduysaOlusanMasaninAdi] as Button;

                        if(tablebutton != null)
                        {
                            tablebutton.ForeColor = Color.White;
                            tablebutton.BackColor = Color.Firebrick;
                        }

                        tumKullanicilaraMesajYolla("komut=masaAcildi&masa=" + siparisMenuForm.urunTasinirkenYeniMasaOlusturulduysaOlusanMasaninAdi + "&departmanAdi=" + restoranListesi[hangiDepartmanButonu].departmanAdi);
                    }

                    if (siparisMenuForm.masaAcikMi)
                    {
                        switch (siparisMenuForm.masaDegisti)
                        {
                            case 2: // departman değişmedi 1 masa açık
                                hangiMasaButonunaBasildi.ForeColor = SystemColors.ActiveCaption;
                                hangiMasaButonunaBasildi.BackColor = Color.White;

                                Button tablebutton = tablePanel.Controls[siparisMenuForm.yeniMasaninAdi] as Button;
                                tablebutton.ForeColor = Color.White;
                                tablebutton.BackColor = Color.Firebrick;

                                tumKullanicilaraMesajYolla("komut=masaKapandi&masa=" + hangiMasaButonunaBasildi.Text + "&departmanAdi=" + restoranListesi[hangiDepartmanButonu].departmanAdi);
                                tumKullanicilaraMesajYolla("komut=masaAcildi&masa=" + siparisMenuForm.yeniMasaninAdi + "&departmanAdi=" + restoranListesi[hangiDepartmanButonu].departmanAdi);
                                break;
                            case 3: // 1 masa açık departmanda değişti
                                hangiMasaButonunaBasildi.ForeColor = SystemColors.ActiveCaption;
                                hangiMasaButonunaBasildi.BackColor = Color.White;
                                tumKullanicilaraMesajYolla("komut=masaKapandi&masa=" + hangiMasaButonunaBasildi.Text + "&departmanAdi=" + restoranListesi[hangiDepartmanButonu].departmanAdi);
                                break;
                            default:
                                hangiMasaButonunaBasildi.ForeColor = Color.White;
                                hangiMasaButonunaBasildi.BackColor = Color.Firebrick;
                                tumKullanicilaraMesajYolla("komut=masaAcildi&masa=" + hangiMasaButonunaBasildi.Text + "&departmanAdi=" + restoranListesi[hangiDepartmanButonu].departmanAdi);
                                break;
                        }
                    }
                    else
                    {
                        hangiMasaButonunaBasildi.ForeColor = SystemColors.ActiveCaption;
                        hangiMasaButonunaBasildi.BackColor = Color.White;
                        tumKullanicilaraMesajYolla("komut=masaKapandi&masa=" + hangiMasaButonunaBasildi.Text + "&departmanAdi=" + restoranListesi[hangiDepartmanButonu].departmanAdi);
                    }
                }
            }
            else
            {
                masayiIslemYapmadanKapat = false;
            }

            siparisMenuForm = null;
            acikMasaVarsaYapma = false;
        }

        #region Komutlar

        //yazıcıları hesap formuna gönder
        private void komut_yazicilarGeldi(string aYazicilari, string dYazicilari, string garson, string acilisZamani)
        {
            siparisMenuForm.hesapForm.yazicilarGeldi(aYazicilari, dYazicilari, garson, acilisZamani);
        }

        private void komut_IndirimOnay(string odemeTipi, string odemeMiktari)
        {
            try
            {
                //Mesajı yönlendirelim
                siparisMenuForm.hesapForm.indirimOnaylandi(odemeTipi, odemeMiktari);
            }
            catch
            { }
        }

        private void komut_OdemeOnay(string odemeTipi, string odemeMiktari, string secilipOdenenSiparisBilgileri)
        {
            try
            {
                //Mesajı yönlendirelim
                siparisMenuForm.hesapForm.odemeOnaylandi(odemeTipi, odemeMiktari, secilipOdenenSiparisBilgileri);
            }
            catch
            { }
        }

        private void komut_OdenenleriGonder(string siparisBilgileri, string odemeBilgileri)
        {
            try
            {
                //Mesajı yönlendirelim
                siparisMenuForm.hesapForm.odenenlerGeldi(siparisBilgileri, odemeBilgileri);
            }
            catch
            { }
        }

        public void komut_masaGirilebilirMi(string cevap)
        {
            if (cevap == "True")
            {
                bool masaAcikMi = false;

                if (hangiMasaButonunaBasildi.ForeColor == Color.White)
                    masaAcikMi = true;

                siparisMenuForm = new SiparisMenuFormu(this, hangiMasa, restoranListesi[hangiDepartmanButonu], ayarYapanKisi, masaAcikMi);
                siparisMenuForm.Show();
                acikMasaVarsaYapma = true;
            }
            else
            {
                dialog2 = new KontrolFormu("Hesap ödeniyor lütfen daha sonra tekrar giriş yapmayı deneyin", false, this);
                dialog2.Show();
            }
        }

        private void komut_hesapOdeniyor(string masa, string departmanAdi)
        {
            //eğer hesabı ödenmeye başlanan masa serverda açıksa
            if (siparisMenuForm != null && (viewdakiDepartmaninAdi == departmanAdi && hangiMasaButonunaBasildi.Text == masa))
            {
                if (siparisMenuForm.hesapForm == null)
                    menuFormunuKapatHesapOdeniyor(masa, departmanAdi);
            }
        }

        public void komut_masaDegisti(string masa, string departmanAdi, string yeniMasa, string yeniDepartmanAdi, string komut)
        {
            if (siparisMenuForm != null && ((restoranListesi[hangiDepartmanButonu].departmanAdi == departmanAdi && hangiMasaButonunaBasildi.Text == masa) || (restoranListesi[hangiDepartmanButonu].departmanAdi == yeniDepartmanAdi && hangiMasaButonunaBasildi.Text == yeniMasa)))
            {
                if (komut == "masaDegistir")
                {
                    masayiIslemYapmadanKapat = true;
                    siparisMenuForm.Close();
                    dialog2 = new KontrolFormu("Masanın(" + masa + ") hesabı " + yeniDepartmanAdi + " departmanının, " + yeniMasa + " masasıyla değiştirildi, masaya yeniden giriş yapınız", false);
                    dialog2.Show();
                }
                else
                {
                    masayiIslemYapmadanKapat = false;
                    siparisMenuForm.Close();

                    dialog2 = new KontrolFormu("Masada(" + masa + ") ürün aktarımı gerçekleştirildi\nSeçilen ürünler" + yeniDepartmanAdi + " departmanındaki, " + yeniMasa + " masasına aktarıldı\nLütfen masaya yeniden giriş yapınız", false);
                    dialog2.Show();
                }
            }
        }

        // giris komutunu uygulayan fonksyon    
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

        // Departmandaki dolu masa bilgisini alan fonksiyon  
        private void komut_departman(string acikMasalar)
        {
            masalar.Clear();
            try
            {
                //Gelen mesajı * ile ayır
                string[] masaDizisi = acikMasalar.Split('*');
                masalar.AddRange(masaDizisi);
            }
            catch (Exception)
            {
                KontrolFormu dialog = new KontrolFormu("Masa durumlarını alırken bir hata oluştu, lütfen tekrar deneyiniz", false);
                dialog.Show();
                return;
            }

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

        // toplumesaj komutunu uygulayan fonksyon        
        private void komut_topluMesaj(string mesaj)
        {
            switch (mesaj)
            {
                case "ServerKapandi":
                    if (client != null)
                    {
                        client.BaglantiyiKes2();
                    }
                    girisYapildi = false;
                    KontrolFormu dialog = new KontrolFormu("Dikkat!\nSunucu bağlantısı koptu!\nLütfen yeniden giriş yapın", false);
                    dialog.ShowDialog();
                    Application.Exit();
                    break;
            }
        }

        private void komut_iptal(string masa, string departmanAdi, string miktar, string yemekAdi, string dusulecekDeger, string ikramYeniMiEskiMi)
        {
            // Eğer clientta da aynı departmanın aynı masası açıksa mesajı yönlendirelim
            if (siparisMenuForm != null && restoranListesi[hangiDepartmanButonu].departmanAdi == departmanAdi && hangiMasaButonunaBasildi.Text == masa)
            {
                siparisMenuForm.iptalGeldi(miktar, yemekAdi, dusulecekDeger, ikramYeniMiEskiMi);
            }
        }

        private void komut_ikram(string masa, string departmanAdi, string miktar, string yemekAdi, string dusulecekDeger)
        {
            // Eğer clientta da aynı departmanın aynı masası açıksa mesajı yönlendirelim
            if (siparisMenuForm != null && restoranListesi[hangiDepartmanButonu].departmanAdi == departmanAdi && hangiMasaButonunaBasildi.Text == masa)
            {
                siparisMenuForm.ikramGeldi(miktar, yemekAdi, dusulecekDeger);
            }
        }

        private void komut_ikramIptal(string masa, string departmanAdi, string miktar, string yemekAdi, string dusulecekDeger, string ikramYeniMiEskiMi)
        {
            // Eğer clientta da aynı departmanın aynı masası açıksa mesajı yönlendirelim
            if (siparisMenuForm != null && restoranListesi[hangiDepartmanButonu].departmanAdi == departmanAdi && hangiMasaButonunaBasildi.Text == masa)
            {
                siparisMenuForm.ikramIptaliGeldi(miktar, yemekAdi, dusulecekDeger, ikramYeniMiEskiMi);
            }
        }

        //parametre hatalı istenilen işlem yapılamadı hatası ver
        private void komut_IslemHatasi(string hata)
        {
            if (hata == "" || hata == null)
                hata = "İstenilen işlem gerçekleştirilemedi, lütfen tekrar deneyiniz";

            KontrolFormu dialog = new KontrolFormu(hata, false);
            dialog.Show();
            if (siparisMenuForm != null)
            {
                siparisMenuForm.Close();
            }
        }

        private void komut_masaAcildi(string masa, string departmanAdi)
        {
            if (restoranListesi[hangiDepartmanButonu].departmanAdi == departmanAdi)
            {
                Button tablebutton = tablePanel.Controls[masa] as Button;
                tablebutton.ForeColor = Color.White;
                tablebutton.BackColor = Color.Firebrick;
            }
        }

        private void komut_masaKapandi(string masa, string departmanAdi)
        {
            if (restoranListesi[hangiDepartmanButonu].departmanAdi == departmanAdi)
            {
                Button tablebutton = tablePanel.Controls[masa] as Button;
                tablebutton.ForeColor = SystemColors.ActiveCaption;
                tablebutton.BackColor = Color.White;
            }
        }

        private void komut_loadSiparis(string siparisBilgileri)
        {
            if (siparisMenuForm != null)
            {
                //Mesajı yönlendirelim
                siparisMenuForm.LoadSiparis(siparisBilgileri);
            }
        }

        private void komut_adisyonNotu(string adisyonNotu)
        {
            if (siparisMenuForm != null)
            {
                //Mesajı yönlendirelim
                siparisMenuForm.AdisyonNotuGeldi(adisyonNotu);
            }
        }

        private void komut_siparis(string masa, string departmanAdi, string miktar, string yemekAdi, string fiyat)
        {
            // Eğer clientta da aynı departmanın aynı masası açıksa mesajı yönlendirelim
            if (siparisMenuForm != null && restoranListesi[hangiDepartmanButonu].departmanAdi == departmanAdi && hangiMasaButonunaBasildi.Text == masa)
            {
                siparisMenuForm.siparisOnayiGeldi(miktar, yemekAdi, fiyat);
            }
        }
        #endregion

        public void masaDegisikligiFormundanAcikMasaBilgisiIstegi(string mesaj)
        {
            client.MesajYolla(mesaj);
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

        //eğer açık masa varsa işlem yapma o formu öne getir ve uyarı ver
        public void acikMasaVarsaUyariVerFormuOneGetir()
        {
            siparisMenuForm.BringToFront();
        }

        public void menuFormunuKapat(string masaAdi, string yeniDepartmanAdi, string yeniMasa)
        {
            siparisMenuForm.Close();

            dialog2 = new KontrolFormu("Masada(" + masaAdi + ") ürün aktarımı gerçekleştirildi\nSeçilen ürünler" + yeniDepartmanAdi + " departmanındaki, " + yeniMasa + " masasına aktarıldı\nLütfen masaya yeniden giriş yapınız", false);
            dialog2.Show();
        }

        public void menuFormunuKapatHesapOdeniyor(string masaAdi, string yeniDepartmanAdi)
        {
            siparisMenuForm.Close();

            dialog2 = new KontrolFormu("Hesap ödeniyor lütfen daha sonra tekrar giriş yapmayı deneyin", false);
            dialog2.Show();
        }
    }
}