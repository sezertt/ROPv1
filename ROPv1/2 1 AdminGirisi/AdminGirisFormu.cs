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

namespace ROPv1
{
    public partial class AdminGirisFormu : Form
    {
        bool closeOrShowAnotherForm = false;
        int whichCheckBoxShouldUncheck = 0;
        int kullaniciAdi = 0;
        UItemp[] infoKullanici;

        public AdminGirisFormu()
        {
            InitializeComponent();
        }

        private void CloseApp(object sender, FormClosedEventArgs e)
        {
            if (!closeOrShowAnotherForm)  // eğer başka bir forma gidilmeyecekse uygulamayı kapat
                Application.Exit();
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
                closeOrShowAnotherForm = true; // başka forma geçilecek uygulamayı kapatma

                GirisEkrani girisForm = new GirisEkrani();
                girisForm.Show();


                this.Close();
            }
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
                    saleCheckBox.Image = global::ROPv1.Properties.Resources.salescolor;
                    saleCheckBox.ImageAlign = ContentAlignment.TopCenter;
                    saleCheckBox.ForeColor = SystemColors.ActiveCaption;
                    break;
                case 2:
                    reportCheckBox.Image = global::ROPv1.Properties.Resources.reportscolor;
                    reportCheckBox.ImageAlign = ContentAlignment.TopCenter;
                    reportCheckBox.ForeColor = SystemColors.ActiveCaption;
                    break;
                case 3:
                    stokCheckBox.Image = global::ROPv1.Properties.Resources.stockcolor;
                    stokCheckBox.ImageAlign = ContentAlignment.TopCenter;
                    stokCheckBox.ForeColor = SystemColors.ActiveCaption;
                    break;
                case 4:
                    adisyonCheckBox.Image = global::ROPv1.Properties.Resources.adisyon;
                    adisyonCheckBox.ImageAlign = ContentAlignment.TopCenter;
                    adisyonCheckBox.ForeColor = SystemColors.ActiveCaption;
                    break;
                case 5:
                    ayarCheckBox.Image = global::ROPv1.Properties.Resources.settingscolor;
                    ayarCheckBox.ImageAlign = ContentAlignment.TopCenter;
                    ayarCheckBox.ForeColor = SystemColors.ActiveCaption;
                    break;
                default:
                    break;
                #endregion
            }


            switch (Convert.ToInt32(((CheckBox)sender).Tag)) // yeni seçilen checkboxı işaretle ve gerekli işlemleri yap
            {
                #region
                case 1:
                    saleCheckBox.Image = global::ROPv1.Properties.Resources.salesback;

                    //sale işlemlerini split panelin 1. kısmına koy, seçili işlemi 2. kısma yok

                    break;
                case 2:
                    reportCheckBox.Image = global::ROPv1.Properties.Resources.reportsback;

                    //report işlemlerini split panelin 1. kısmına koy, seçili işlemi 2. kısma yok

                    break;
                case 3:
                    stokCheckBox.Image = global::ROPv1.Properties.Resources.stockback;

                    //stok işlemlerini split panelin 1. kısmına koy, seçili işlemi 2. kısma yok

                    break;
                case 4:
                    adisyonCheckBox.Image = global::ROPv1.Properties.Resources.adisyonback;

                    //adisyon işlemlerini split panelin 1. kısmına koy, seçili işlemi 2. kısma yok

                    break;
                case 5:
                    ayarCheckBox.Image = global::ROPv1.Properties.Resources.settingsback;
                    leftPanelView.Nodes.Add("Kullanıcılar");
                    leftPanelView.Nodes.Add("Departmanlar");
                    leftPanelView.Nodes.Add("Masa Yerleşim Planı");
                    leftPanelView.Nodes.Add("Menüler");
                    leftPanelView.Nodes.Add("Ürünler");
                    leftPanelView.Nodes.Add("Ürün Menüleri");
                    leftPanelView.Nodes.Add("Stok Ayarları");

                    if (Helper.VerifyHash("false", "SHA512", infoKullanici[kullaniciAdi].UIY[6]))
                    {
                        leftPanelView.SelectedNode = leftPanelView.Nodes[1];
                    }
                    else
                        leftPanelView.SelectedNode = leftPanelView.Nodes[0];


                    //Veri tabanından gerekli verileri al                  
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
            switch (leftPanelView.SelectedNode.Index) // settingsin içeriğindeki seçim değiştiğinde panel2 nin içeriğini değiştiriyoruz
            {
                #region
                case 0: //Kullanıcılar Seçildi
                    if (Helper.VerifyHash("false", "SHA512", infoKullanici[kullaniciAdi].UIY[6]))
                    {
                        leftPanelView.SelectedNode = leftPanelView.Nodes[1];
                    }
                    else
                    {
                        leftPanelView.SelectedNode = leftPanelView.Nodes[0];
                        Kullanici kullaniciView = new Kullanici();
                        splitPanel.Panel2.Controls.Add(kullaniciView);
                        kullaniciView.Dock = DockStyle.Fill;
                    }
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

                case 5: //Ürün Menüleri Seçildi
                    UrunMenuleri urunMenuView = new UrunMenuleri();
                    splitPanel.Panel2.Controls.Add(urunMenuView);
                    urunMenuView.Dock = DockStyle.Fill;
                    break;

                case 6: //Stok Ayarları Seçildi
                    Stoklar stokView = new Stoklar();
                    splitPanel.Panel2.Controls.Add(stokView);
                    stokView.Dock = DockStyle.Fill;

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
                for (int i = 0; i < 5; i++)
                {
                    if (Helper.VerifyHash("false", "SHA512", infoKullanici[kullaniciAdi].UIY[i]))
                    {
                        flowLayoutPanel1.Controls[i].Enabled = false;
                    }
                }
            }

            if (saleCheckBox.Enabled == true)
                changeButonChecked(saleCheckBox);
            else if (reportCheckBox.Enabled == true)
                changeButonChecked(reportCheckBox);
            else if (stokCheckBox.Enabled == true)
                changeButonChecked(stokCheckBox);
            else if (adisyonCheckBox.Enabled == true)
                changeButonChecked(adisyonCheckBox);
            else if (ayarCheckBox.Enabled == true)
                changeButonChecked(ayarCheckBox);
        }
    }
}