using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Data.SqlClient;

namespace ROPv1
{
    public partial class AdminGirisFormu : Form
    {
        int whichCheckBoxShouldUncheck = 0;
        int kullaniciAdi = 0;
        UItemp[] infoKullanici;

        Raporlar gunRaporView, urunRaporView;

        public AdminGirisFormu()
        {
            InitializeComponent();
        }

        private void exitPressed(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void saleCheckChanged(object sender, EventArgs e)
        {
            changeButonChecked(sender); // seçilen checkboxa gönder
        }

        private void changeButonChecked(object sender)
        {
            if (Convert.ToInt32(((CheckBox)sender).Tag) == whichCheckBoxShouldUncheck) // eğer checkbox zaten seçiliyse birşey yapmadan dön
                return;

            leftPanelView.Nodes.Clear();
            splitPanel.Panel2.Controls.Clear();

            switch (whichCheckBoxShouldUncheck) // önceden seçili olan checkboxı kaldır
            {
                #region
                case 1:
                    reportCheckBox.Image = global::ROPv1.Properties.Resources.reportscolor;
                    reportCheckBox.ImageAlign = ContentAlignment.TopCenter;
                    reportCheckBox.ForeColor = SystemColors.ActiveCaption;
                    break;
                case 2:
                    stokCheckBox.Image = global::ROPv1.Properties.Resources.stockcolor;
                    stokCheckBox.ImageAlign = ContentAlignment.TopCenter;
                    stokCheckBox.ForeColor = SystemColors.ActiveCaption;
                    break;
                case 3:
                    ayarCheckBox.Image = global::ROPv1.Properties.Resources.settingscolor;
                    ayarCheckBox.ImageAlign = ContentAlignment.TopCenter;
                    ayarCheckBox.ForeColor = SystemColors.ActiveCaption;
                    break;
                case 4:
                    anketCheckBox.Image = global::ROPv1.Properties.Resources.anket;
                    anketCheckBox.ImageAlign = ContentAlignment.TopCenter;
                    anketCheckBox.ForeColor = SystemColors.ActiveCaption;
                    break;
                default:
                    break;
                #endregion
            }


            switch (Convert.ToInt32(((CheckBox)sender).Tag)) // yeni seçilen checkboxı işaretle ve gerekli işlemleri yap
            {
                #region
                case 1:
                    reportCheckBox.Image = global::ROPv1.Properties.Resources.reportsback;
                    buttonBilgiAktar.Visible = false;
                    //report işlemlerini split panelin 1. kısmına koy, seçili işlemi 2. kısma yok
                    leftPanelView.Nodes.Add("Gün Sonu Raporu");
                    leftPanelView.Nodes.Add("Ürün Satış Raporu");

                    leftPanelView.SelectedNode = leftPanelView.Nodes[0];
                    break;
                case 2:
                    stokCheckBox.Image = global::ROPv1.Properties.Resources.stockback;
                    buttonBilgiAktar.Visible = false;
                    //stok işlemlerini split panelin 1. kısmına koy, seçili işlemi 2. kısma yok
                    break;
                case 3:
                    ayarCheckBox.Image = global::ROPv1.Properties.Resources.settingsback;
                    buttonBilgiAktar.Visible = true;

                    leftPanelView.Nodes.Add("Kullanıcılar");
                    leftPanelView.Nodes.Add("Departmanlar");
                    leftPanelView.Nodes.Add("Masa Yerleşim Planı");
                    leftPanelView.Nodes.Add("Menüler");
                    leftPanelView.Nodes.Add("Ürünler");
                    leftPanelView.Nodes.Add("Stok Ayarları");
                    leftPanelView.Nodes.Add("Reçeteler");
                    leftPanelView.Nodes.Add("İşletme Bilgileri");

                    leftPanelView.SelectedNode = leftPanelView.Nodes[0];
                    break;
                case 4:
                    anketCheckBox.Image = global::ROPv1.Properties.Resources.anketBack;
                    buttonBilgiAktar.Visible = false;

                    leftPanelView.Nodes.Add("Anket Sonuçları");
                    leftPanelView.Nodes.Add("Kullanıcı Bilgileri");
                    leftPanelView.Nodes.Add("Anket Ayarları");

                    leftPanelView.SelectedNode = leftPanelView.Nodes[0];
                    break;
                default:
                    break;
                #endregion
            }
            // yeni seçilen checkboxı işaretlemenin kalan kısmı
            ((CheckBox)sender).ImageAlign = ContentAlignment.MiddleCenter;
            ((CheckBox)sender).ForeColor = Color.White;

            //Nodeların eklenmesinden sonra taşma varsa bile ekrana sığması için font boyutunu küçültüyoruz
            foreach (TreeNode node in leftPanelView.Nodes)
            {
                while (leftPanelView.Width - 12 < System.Windows.Forms.TextRenderer.MeasureText(node.Text, new Font(leftPanelView.Font.FontFamily, leftPanelView.Font.Size, leftPanelView.Font.Style)).Width)
                {
                    leftPanelView.Font = new Font(leftPanelView.Font.FontFamily, leftPanelView.Font.Size - 0.5f, leftPanelView.Font.Style);
                }
            }

            //yeni seçilen checkboxı son seçilen checkbox yap
            whichCheckBoxShouldUncheck = Convert.ToInt32(((CheckBox)sender).Tag);

            leftPanelView.Focus();
        }

        private void changeSettingsScreen(object sender, TreeViewEventArgs e)
        {
            splitPanel.Panel2.Controls.Clear();
            if (leftPanelView.Nodes[0].Text == "Kullanıcılar")
            {
                switch (leftPanelView.SelectedNode.Index) // settingsin içeriğindeki seçim değiştiğinde panel2 nin içeriğini değiştiriyoruz
                {
                    #region
                    case 0: //Kullanıcılar Seçildi
                        Kullanici kullaniciView = new Kullanici();
                        splitPanel.Panel2.Controls.Add(kullaniciView);
                        kullaniciView.Dock = DockStyle.Fill;
                        break;

                    case 1: //Departmanlar Seçildi
                        Departman departmanView = new Departman();
                        splitPanel.Panel2.Controls.Add(departmanView);
                        departmanView.Dock = DockStyle.Fill;
                        break;

                    case 2: //Departman Yerleşim Planı Seçildi
                        MasaPlan masaPlanView = new MasaPlan();
                        splitPanel.Panel2.Controls.Add(masaPlanView);
                        masaPlanView.Dock = DockStyle.Fill;
                        break;

                    case 3: //Menüler Seçildi
                        MenuControl menuView = new MenuControl();
                        splitPanel.Panel2.Controls.Add(menuView);
                        menuView.Dock = DockStyle.Fill;
                        break;

                    case 4: //Ürünler Seçildi
                        Products productView = new Products();
                        splitPanel.Panel2.Controls.Add(productView);
                        productView.Dock = DockStyle.Fill;
                        break;
                    case 5: //Stok Ayarları Seçildi
                        Stoklar stokView = new Stoklar();
                        splitPanel.Panel2.Controls.Add(stokView);
                        stokView.Dock = DockStyle.Fill;

                        break;
                    case 6: //Reçeteler Seçildi
                        Receteler receteView = new Receteler();
                        splitPanel.Panel2.Controls.Add(receteView);
                        receteView.Dock = DockStyle.Fill;
                        break;

                    case 7: //İşletme Bilgileri Seçildi
                        IsletmeBilgileri isletmeBilgileriView = new IsletmeBilgileri();
                        splitPanel.Panel2.Controls.Add(isletmeBilgileriView);
                        isletmeBilgileriView.Dock = DockStyle.Fill;
                        break;

                    default:
                        break;
                    #endregion
                }
            }
            else if (leftPanelView.Nodes[0].Text == "Gün Sonu Raporu")
            {
                SqlCommand cmd = SQLBaglantisi.getCommand("SELECT AcikMi FROM Adisyon WHERE AcikMi=1 AND IptalMi=0");
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    KontrolFormu dialog = new KontrolFormu("Sistemde açık masa bulunurken raporlama düzgün görüntülenemeyebilir, devam etmek istiyor musunuz?", true, this);
                    dialog.Show();
                }
                cmd.Connection.Close();
                cmd.Connection.Dispose();
            }
            else if (leftPanelView.Nodes[0].Text == "Anket Sonuçları")
            {
                switch (leftPanelView.SelectedNode.Index) // settingsin içeriğindeki seçim değiştiğinde panel2 nin içeriğini değiştiriyoruz
                {
                    #region
                    case 0: //Anket Sonuçları Seçildi

                        break;

                    case 1: //Kullanıcı Bilgileri Seçildi

                        break;

                    case 2: //Anket Ayarları Seçildi
                        AnketAyarlari anketAyarView = new AnketAyarlari();
                        splitPanel.Panel2.Controls.Add(anketAyarView);
                        anketAyarView.Dock = DockStyle.Fill;
                        break;

                    default:
                        break;
                    #endregion
                }
            }
        }

        public void raporla()
        {
            switch (leftPanelView.SelectedNode.Index) // settingsin içeriğindeki seçim değiştiğinde panel2 nin içeriğini değiştiriyoruz
            {
                #region
                case 0: //Gün sonu raporu seçildi
                    if(gunRaporView == null)
                    {
                        gunRaporView = new Raporlar(true);
                        splitPanel.Panel2.Controls.Add(gunRaporView);
                        gunRaporView.Dock = DockStyle.Fill;
                    }
                    gunRaporView.BringToFront();
                    break;

                case 1: //Ürün satış raporu seçildi
                    if (urunRaporView == null)
                    {
                        urunRaporView = new Raporlar(false);
                        splitPanel.Panel2.Controls.Add(urunRaporView);
                        urunRaporView.Dock = DockStyle.Fill;
                    }
                    urunRaporView.BringToFront();
                    break;

                default:
                    break;
                #endregion
            }
        }

        private void timerSaat_Tick(object sender, EventArgs e)
        {
            labelSaat.Text = DateTime.Now.ToString("HH:mm:ss", new CultureInfo("tr-TR"));
        }

        private void AdminGirisFormu_Load(object sender, EventArgs e)
        {
            labelSaat.Text = DateTime.Now.ToString("HH:mm:ss", new CultureInfo("tr-TR"));
            labelGun.Text = DateTime.Now.ToString("dddd", new CultureInfo("tr-TR"));
            labelTarih.Text = DateTime.Now.Date.ToString("d MMMM yyyy", new CultureInfo("tr-TR"));
            timerSaat.Start();

            splitPanel.SplitterDistance = Screen.FromControl(this).Bounds.Size.Width / 6;

            #region xml oku

            XmlLoad<UItemp> loadInfoKullanicilar = new XmlLoad<UItemp>();
            infoKullanici = loadInfoKullanicilar.LoadRestoran("tempfiles.xml");

            XmlLoad<string> loadInfoSonKullanici = new XmlLoad<string>();
            string[] sonGirisYapanKullanici = loadInfoSonKullanici.LoadRestoran("sonKullanici.xml");
            #endregion

            if (sonGirisYapanKullanici[0] != "ropisimiz")
            {
                //kullanıcının yerini bul
                for (int i = 0; i < infoKullanici.Count(); i++)
                {
                    if (sonGirisYapanKullanici[0] == (new UnicodeEncoding()).GetString(infoKullanici[i].UIUN))
                    {
                        kullaniciAdi = i;
                        break;
                    }
                }
                //yetkilerine göre işlemlere izin verme
                for (int i = 0; i < 4; i++)
                {
                    if (Helper.VerifyHash("false", "SHA512", infoKullanici[kullaniciAdi].UIY[i]))
                    {
                        flowLayoutPanel1.Controls[i].Enabled = false;
                    }
                }
            }
        }

        private void AdminGirisFormu_KeyDown(object sender, KeyEventArgs e)
        {
            if (ayarCheckBox.Visible == false)
            {
                if (e.Control && e.Shift && e.KeyCode == Keys.D3)
                {
                    ayarCheckBox.Visible = true;                    
                }
            }
        }

        private void buttonBilgiAktar_Click(object sender, EventArgs e)
        {
            ShowWaitForm();
            bool basarili = true;
            string[] xmlDosyalari = { "kategoriler.xml", "masaDizayn.xml", "menu.xml", "restoran.xml", "stoklar.xml", "tempfiles.xml", "urunler.xml" };

            XMLAktarServer aktarimServeri = new XMLAktarServer();

            for (int i = 0; i < 7; i++)
            {
                basarili = aktarimServeri.Server(xmlDosyalari[i]);
                if (!basarili)
                {
                    break;
                }
            }
            OnLoaded(null, null);
            if (basarili)
            {
                KontrolFormu dialog = new KontrolFormu("Dosya gönderimi başarılı", false);
                dialog.Show();
            }
            else
            {
                KontrolFormu dialog = new KontrolFormu("Dosya gönderimi başarısız", false);
                dialog.Show();
            }
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

        private void adisyonCheckBox_Click(object sender, EventArgs e)
        {
            AdisyonGoruntuleme adisyonForm = new AdisyonGoruntuleme();
            adisyonForm.Show();
        }
    }
}