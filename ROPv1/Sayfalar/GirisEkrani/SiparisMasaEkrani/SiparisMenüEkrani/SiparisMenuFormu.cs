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
using System.Data.SqlClient;
using System.Threading;
using ROPv1.CrystalReports;

namespace ROPv1
{
    public partial class SiparisMenuFormu : Form
    {
        public SiparisMasaFormu masaFormu;

        public MasaDegistirFormu masaDegistirForm;

        CrystalReportMutfak raporMutfakNormal = new CrystalReportMutfak();

        CrystalReportMutfakUrunIptal raporMutfakIptal = new CrystalReportMutfakUrunIptal();

        public HesapFormu hesapForm;

        Restoran hangiDepartman;

        PorsiyonFormu porsiyonForm;

        int hangiKategoriSecili = 555, menuSirasi = 0;

        const int eskiIkramlar = 0, yeniIkramlar = 1, eskiSiparisler = 2, yeniSiparisler = 3;

        const int urunBoyu = 205, fiyatBoyu = 80;

        List<Menuler> menuListesi = new List<Menuler>();  // menüleri tutacak liste

        List<KategorilerineGoreUrunler> urunListesi = new List<KategorilerineGoreUrunler>();

        KategorilerineGoreUrunler secilenUrun = new KategorilerineGoreUrunler();

        List<bool> listedeSeciliOlanItemlar = new List<bool>();

        KontrolFormu dialogTimer;

        UItemp[] infoKullanici;

        bool iptalIkram = true, adisyonNotuGuncellenmeliMi = false, adisyonDegistirebilirMi = false, satisYapabilirMi = false;

        public bool masaAcikMi = false;

        public int masaDegisti = -1;

        public string yeniDepartmaninAdi = "", yeniMasaninAdi = "", urunTasinirkenYeniMasaOlusturulduysaOlusanMasaninAdi = "", urunTasinirkenYeniMasaOlusturulduysaOlusanDepartmaninAdi, siparisiKimGirdi, adisyonNotu = "", MasaAdi;

        public SiparisMenuFormu(SiparisMasaFormu masaFormu, string masaninAdi, Restoran butonBilgileri, string siparisiGirenKisi, bool masaAcikmi)
        {
            InitializeComponent();

            this.masaFormu = masaFormu;

            this.masaAcikMi = masaAcikmi;

            //siparişi vermek için pin giren kisinin bilgisi
            siparisiKimGirdi = siparisiGirenKisi;

            //hangi departmanda olduğumuzu tutan değişken
            hangiDepartman = butonBilgileri;

            XmlLoad<UItemp> loadInfoKullanicilar = new XmlLoad<UItemp>();
            infoKullanici = loadInfoKullanicilar.LoadRestoran("tempfiles.xml");

            int kullaniciYeri = 0;

            //kullanıcının yerini bul
            for (int i = 0; i < infoKullanici.Count(); i++)
            {
                if (siparisiKimGirdi == (new UnicodeEncoding()).GetString(infoKullanici[i].UIN) + " " + (new UnicodeEncoding()).GetString(infoKullanici[i].UIS))
                {
                    kullaniciYeri = i;
                    break;
                }
            }
            //yetkilerine göre işlemlere izin verme

            if (PasswordHash.ValidatePassword("false", infoKullanici[kullaniciYeri].UIY[4]))
            {
                buttonUrunIkram.Enabled = false;
                buttonUrunIptal.Enabled = false;
                iptalIkram = false;
            }

            if (PasswordHash.ValidatePassword("true", infoKullanici[kullaniciYeri].UIY[3]))
                adisyonDegistirebilirMi = true;

            if (PasswordHash.ValidatePassword("true", infoKullanici[kullaniciYeri].UIY[0]))
                satisYapabilirMi = true;

            MasaAdi = masaninAdi;
            labelMasa.Text = "Masa: " + MasaAdi;
            labelDepartman.Text = "Departman: " + hangiDepartman.departmanAdi;

            //labelların fontunu ayarlıyoruz
            while (labelDepartman.Width < System.Windows.Forms.TextRenderer.MeasureText(labelDepartman.Text, new Font(labelDepartman.Font.FontFamily, labelDepartman.Font.Size, labelDepartman.Font.Style)).Width)
            {
                labelDepartman.Font = new Font(labelDepartman.Font.FontFamily, labelDepartman.Font.Size - 0.5f, labelDepartman.Font.Style);
            }

            while (labelMasa.Width < System.Windows.Forms.TextRenderer.MeasureText(labelMasa.Text, new Font(labelMasa.Font.FontFamily, labelMasa.Font.Size, labelMasa.Font.Style)).Width)
            {
                labelMasa.Font = new Font(labelMasa.Font.FontFamily, labelMasa.Font.Size - 0.5f, labelMasa.Font.Style);
            }

            // Oluşturulmuş menüleri xml den okuyoruz
            XmlLoad<Menuler> loadInfo = new XmlLoad<Menuler>();
            Menuler[] infoMenu = loadInfo.LoadRestoran("menu.xml");

            //menüleri tutacak listemize atıyoruz
            menuListesi.AddRange(infoMenu);

            int hangiMenu = 0;
            while (menuListesi[hangiMenu].menuAdi != hangiDepartman.departmanMenusu)
                hangiMenu++;

            //kategorileri panele yerleştiriyoruz
            for (int i = 0; i < menuListesi[hangiMenu].menukategorileri.Count; i++)
            {
                Button menuBasliklariButonlari = new Button();
                menuBasliklariButonlari.Text = menuListesi[hangiMenu].menukategorileri[i];

                menuBasliklariButonlari.UseVisualStyleBackColor = false;
                menuBasliklariButonlari.BackColor = Color.White;
                menuBasliklariButonlari.ForeColor = SystemColors.ActiveCaption;
                menuBasliklariButonlari.TextAlign = ContentAlignment.MiddleCenter;
                menuBasliklariButonlari.Font = new Font("Arial", 18.00F, FontStyle.Bold);
                menuBasliklariButonlari.Tag = -1;
                menuBasliklariButonlari.Click += menuBasliklariButonlari_Click;
                menuBasliklariButonlari.Name = "" + i;

                flowPanelMenuBasliklari.Controls.Add(menuBasliklariButonlari);
            }

            XmlLoad<KategorilerineGoreUrunler> loadInfoUrun = new XmlLoad<KategorilerineGoreUrunler>();
            KategorilerineGoreUrunler[] infoUrun = loadInfoUrun.LoadRestoran("urunler.xml");

            urunListesi.AddRange(infoUrun);
        }

        //kategori seçildiğinde kategori içindeki ürünleri panele getiren method
        private void menuBasliklariButonlari_Click(object sender, EventArgs e)
        {
            if (hangiKategoriSecili == (int)((Button)sender).Tag)
                return;

            flowPanelUrunler.Controls.Clear();

            int height = (flowPanelUrunler.Bounds.Height - 26) / 4;
            int width = (flowPanelUrunler.Bounds.Width - 36) / 3;

            if ((int)((Button)sender).Tag == -1)
            {
                for (int i = 0; i < urunListesi.Count; i++)
                {
                    if (urunListesi[i].kategorininAdi == ((Button)sender).Text)
                    {
                        ((Button)sender).Tag = i;
                        for (int j = 0; j < urunListesi[i].urunAdi.Count; j++)
                        {
                            Button urunButonlari = new Button();
                            urunButonlari.Text = urunListesi[i].urunAdi[j];

                            urunButonlari.UseVisualStyleBackColor = false;
                            urunButonlari.BackColor = Color.White;
                            urunButonlari.ForeColor = SystemColors.ActiveCaption;
                            urunButonlari.TextAlign = ContentAlignment.MiddleCenter;
                            urunButonlari.Font = new Font("Arial", 17.25F, FontStyle.Bold);
                            urunButonlari.Height = height;
                            urunButonlari.Width = width;
                            urunButonlari.Tag = j;
                            urunButonlari.Name = urunListesi[i].urunAdi[j];
                            urunButonlari.Click += urunButonlari_Click;

                            flowPanelUrunler.Controls.Add(urunButonlari);
                        }
                        break;
                    }
                }
            }
            else
            {
                int i = Convert.ToInt32(((Button)sender).Tag);
                for (int j = 0; j < urunListesi[i].urunAdi.Count; j++)
                {
                    Button urunButonlari = new Button();
                    urunButonlari.Text = urunListesi[i].urunAdi[j];

                    urunButonlari.UseVisualStyleBackColor = false;
                    urunButonlari.BackColor = Color.White;
                    urunButonlari.ForeColor = SystemColors.ActiveCaption;
                    urunButonlari.TextAlign = ContentAlignment.MiddleCenter;
                    urunButonlari.Font = new Font("Arial", 17.25F, FontStyle.Bold);
                    urunButonlari.Height = height;
                    urunButonlari.Width = width;
                    urunButonlari.Tag = j;
                    urunButonlari.Name = urunListesi[i].urunAdi[j];
                    urunButonlari.Click += urunButonlari_Click;

                    flowPanelUrunler.Controls.Add(urunButonlari);
                }
            }

            Button menuBasligi = flowPanelMenuBasliklari.Controls[menuSirasi] as Button;
            menuBasligi.BackColor = Color.White;
            menuBasligi.ForeColor = SystemColors.ActiveCaption;

            menuSirasi = Convert.ToInt32((sender as Button).Name);

            menuBasligi = flowPanelMenuBasliklari.Controls[menuSirasi] as Button;
            menuBasligi.BackColor = SystemColors.ActiveCaption;
            menuBasligi.ForeColor = Color.White;

            //menünün paneldeki yeri
            menuSirasi = Convert.ToInt32((sender as Button).Name);

            //seçili olan menünün bilgisini tut, ürün seçiminde işe yarıyor
            hangiKategoriSecili = Convert.ToInt32(((Button)sender).Tag);

            Button yeniSecilenUrunButonu = null;
            try
            {
                yeniSecilenUrunButonu = flowPanelUrunler.Controls.Find(secilenUrun.urunAdi[0], true).FirstOrDefault() as Button;
            }
            catch
            { }

            if (yeniSecilenUrunButonu != null)
            {
                yeniSecilenUrunButonu.BackColor = SystemColors.ActiveCaption;
                yeniSecilenUrunButonu.ForeColor = Color.White;
            }
        }

        #region Panellerdeki itemların görünümünü ekrana göre ayarlıyoruz
        private void myPannel_SizeChanged(object sender, EventArgs e)
        {
            flowPanelMenuBasliklari.SuspendLayout();

            int height = (flowPanelMenuBasliklari.Bounds.Height - 66) / 10;
            int width = (flowPanelMenuBasliklari.Bounds.Width - 24);

            foreach (Control ctrl in flowPanelMenuBasliklari.Controls)
            {
                if (ctrl is Button)
                {
                    ctrl.Height = height;
                    ctrl.Width = width;

                    while (ctrl.Width < System.Windows.Forms.TextRenderer.MeasureText(ctrl.Text, new Font(ctrl.Font.FontFamily, ctrl.Font.Size, ctrl.Font.Style)).Width)
                    {
                        ctrl.Font = new Font(ctrl.Font.FontFamily, ctrl.Font.Size - 0.5f, ctrl.Font.Style);
                    }
                }
            }
            flowPanelMenuBasliklari.ResumeLayout();

            if (flowPanelMenuBasliklari.VerticalScroll.Visible)
            {
                flowPanelMenuBasliklari.VerticalScroll.Value = flowPanelMenuBasliklari.VerticalScroll.Maximum;
                flowPanelMenuBasliklari.HorizontalScroll.Value = flowPanelMenuBasliklari.HorizontalScroll.Maximum;
                flowPanelMenuBasliklari.VerticalScroll.Value = 0;
                flowPanelMenuBasliklari.HorizontalScroll.Value = 0;
            }
        }

        private void urunPanelSizeChanged(object sender, EventArgs e)
        {
            flowPanelUrunler.SuspendLayout();
            int height = (flowPanelUrunler.Bounds.Height - 26) / 4;
            int width = (flowPanelUrunler.Bounds.Width - 36) / 3;
            foreach (Control ctrl in flowPanelUrunler.Controls)
            {
                if (ctrl is Button)
                {
                    ctrl.Height = height;
                    ctrl.Width = width;
                }
            }
            flowPanelUrunler.ResumeLayout();

            if (flowPanelUrunler.VerticalScroll.Visible)
            {
                flowPanelUrunler.VerticalScroll.Value = flowPanelUrunler.VerticalScroll.Maximum;
                flowPanelUrunler.HorizontalScroll.Value = flowPanelUrunler.HorizontalScroll.Maximum;
                flowPanelUrunler.VerticalScroll.Value = 0;
                flowPanelUrunler.HorizontalScroll.Value = 0;
            }
        }
        #endregion

        //ürün butonlarına basıldığında çalışacak olan method
        void urunButonlari_Click(object sender, EventArgs e)
        {
            Button oncekiSeciliUrunButonu = null;
            try
            {
                oncekiSeciliUrunButonu = flowPanelUrunler.Controls.Find(secilenUrun.urunAdi[0], true).FirstOrDefault() as Button;
            }
            catch
            { }

            if (oncekiSeciliUrunButonu != null)
            {
                if (oncekiSeciliUrunButonu == ((Button)sender))
                    return;
                oncekiSeciliUrunButonu.BackColor = Color.White;
                oncekiSeciliUrunButonu.ForeColor = SystemColors.ActiveCaption;
            }

            ((Button)sender).ForeColor = Color.White;
            ((Button)sender).BackColor = SystemColors.ActiveCaption;

            buttonTemizle_Click(null, null);

            secilenUrun.urunAdi.Add(((Button)sender).Text);
            secilenUrun.urunPorsiyonSinifi.Add(urunListesi[hangiKategoriSecili].urunPorsiyonSinifi[Convert.ToInt32(((Button)sender).Tag)]);
            secilenUrun.urunPorsiyonFiyati.Add(urunListesi[hangiKategoriSecili].urunPorsiyonFiyati[Convert.ToInt32(((Button)sender).Tag)]);
            secilenUrun.urunKiloFiyati.Add(urunListesi[hangiKategoriSecili].urunKiloFiyati[Convert.ToInt32(((Button)sender).Tag)]);
            secilenUrun.urunTuru.Add(urunListesi[hangiKategoriSecili].urunTuru[Convert.ToInt32(((Button)sender).Tag)]);

            if (secilenUrun.urunTuru[0] == "Kilogram")
                buttonPorsiyonSec.Enabled = false;
            else
                buttonPorsiyonSec.Enabled = true;

            labelEklenecekUrun.Text = ((Button)sender).Text;
        }

        //listede (listurunfiyat) eleman seçildiğinde çalışacak olan method
        private void listHesap_Click(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = listUrunFiyat.HitTest(listUrunFiyat.PointToClient(Cursor.Position));
            int kacElemanSecili;

            //itema basıldıysa seçili olanları tuttuğumuz listede o itemı false ise true, true ise false yap
            if (info.Item != null)
            {
                if (listUrunFiyat.Items[info.Item.Index].Selected && listedeSeciliOlanItemlar[info.Item.Index] == false)
                    listedeSeciliOlanItemlar[info.Item.Index] = true;
                else
                    listedeSeciliOlanItemlar[info.Item.Index] = false;
            }

            kacElemanSecili = 0;

            for (int i = 0; i < listedeSeciliOlanItemlar.Count; i++)
            {
                if (listedeSeciliOlanItemlar[i])
                {
                    listUrunFiyat.Items[i].Selected = true;
                    kacElemanSecili++;
                }
                else
                {
                    listUrunFiyat.Items[i].Selected = false;
                }
            }

            if (kacElemanSecili == 0)
            {
                buttonUrunIkram.Enabled = false;
                buttonUrunIptal.Enabled = false;
                buttonTasi.Enabled = false;
            }
            else if (kacElemanSecili == 1)
            {
                if (iptalIkram) //yetki
                {
                    if (listUrunFiyat.SelectedItems[0].Group != listUrunFiyat.Groups[yeniSiparisler])// Yeni girilen bir sipariş veritabanına girmeden ikram edilemez
                        buttonUrunIkram.Enabled = true;

                    buttonUrunIptal.Enabled = true;
                }

                if (listUrunFiyat.SelectedItems[0].Group == listUrunFiyat.Groups[eskiIkramlar] || listUrunFiyat.SelectedItems[0].Group == listUrunFiyat.Groups[yeniIkramlar])
                    buttonUrunIkram.Text = "  İkr. İptal";
                else
                    buttonUrunIkram.Text = "İkram";

                if (listUrunFiyat.SelectedItems[0].Group == listUrunFiyat.Groups[yeniSiparisler])
                    buttonTasi.Enabled = false;
                else
                    buttonTasi.Enabled = true;
            }
            else
            {
                buttonUrunIkram.Enabled = false;
                buttonTasi.Enabled = true;
                buttonUrunIptal.Enabled = false;

                if (kacElemanSecili > 5)
                    buttonTasi.Enabled = false;
                else
                {
                    for (int i = 0; i < listUrunFiyat.SelectedItems.Count; i++)
                    {
                        if (listUrunFiyat.SelectedItems[i].Group == listUrunFiyat.Groups[yeniSiparisler])
                            buttonTasi.Enabled = false;
                    }
                }
            }

            if (listUrunFiyat.SelectedItems.Count > 0)
            {
                Button oncekiSeciliUrunButonu = null;
                try
                {
                    oncekiSeciliUrunButonu = flowPanelUrunler.Controls.Find(secilenUrun.urunAdi[0], true).FirstOrDefault() as Button;
                }
                catch
                { }

                if (oncekiSeciliUrunButonu != null)
                {
                    oncekiSeciliUrunButonu.BackColor = Color.White;
                    oncekiSeciliUrunButonu.ForeColor = SystemColors.ActiveCaption;
                }

                secilenUrun.urunAdi.Clear();
                secilenUrun.urunPorsiyonSinifi.Clear();
                secilenUrun.urunPorsiyonFiyati.Clear();
                secilenUrun.urunTuru.Clear();
                secilenUrun.urunKiloFiyati.Clear();

                secilenUrun.urunAdi.Add(listUrunFiyat.SelectedItems[0].SubItems[2].Text);
                secilenUrun.urunPorsiyonSinifi.Add(Convert.ToInt32(listUrunFiyat.SelectedItems[0].SubItems[1].Tag));
                secilenUrun.urunTuru.Add(listUrunFiyat.SelectedItems[0].SubItems[2].Tag.ToString());

                if (listUrunFiyat.SelectedItems[0].SubItems[3].Text[listUrunFiyat.SelectedItems[0].SubItems[3].Text.Length - 1].ToString() == "P")
                {
                    secilenUrun.urunPorsiyonFiyati.Add(listUrunFiyat.SelectedItems[0].SubItems[3].Text);
                    secilenUrun.urunKiloFiyati.Add(listUrunFiyat.SelectedItems[0].SubItems[3].Tag.ToString());
                }
                else
                {
                    secilenUrun.urunPorsiyonFiyati.Add(listUrunFiyat.SelectedItems[0].SubItems[3].Tag.ToString());
                    secilenUrun.urunKiloFiyati.Add(listUrunFiyat.SelectedItems[0].SubItems[3].Text);
                }

                labelEklenecekUrun.Text = listUrunFiyat.SelectedItems[0].SubItems[2].Text;

                Button yeniSecilenUrunButonu = null;
                try
                {
                    yeniSecilenUrunButonu = flowPanelUrunler.Controls.Find(secilenUrun.urunAdi[0], true).FirstOrDefault() as Button;
                }
                catch
                { }

                if (yeniSecilenUrunButonu != null)
                {
                    yeniSecilenUrunButonu.BackColor = SystemColors.ActiveCaption;
                    yeniSecilenUrunButonu.ForeColor = Color.White;
                }
            }
        }

        //listede seçili elemanların seçimini kaldırır, ikram/iptal/ekle/taşı butonlarını disable eder
        private void buttonTemizle_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listedeSeciliOlanItemlar.Count; i++)
            {
                listedeSeciliOlanItemlar[i] = false;
                listUrunFiyat.Items[i].Selected = false;
            }

            Button oncekiSeciliUrunButonu = null;
            try
            {
                oncekiSeciliUrunButonu = flowPanelUrunler.Controls.Find(secilenUrun.urunAdi[0], true).FirstOrDefault() as Button;
            }
            catch
            { }

            if (oncekiSeciliUrunButonu != null)
            {
                oncekiSeciliUrunButonu.BackColor = Color.White;
                oncekiSeciliUrunButonu.ForeColor = SystemColors.ActiveCaption;
            }

            buttonPorsiyonSec.Text = "Tam";
            labelEklenecekUrun.Text = "Ürün Seçiniz";
            labelCokluAdet.Text = "1";

            buttonUrunIkram.Enabled = false;
            buttonTasi.Enabled = false;
            buttonUrunIptal.Enabled = false;

            secilenUrun.urunAdi.Clear();
            secilenUrun.urunPorsiyonSinifi.Clear();
            secilenUrun.urunPorsiyonFiyati.Clear();
            secilenUrun.urunTuru.Clear();
            secilenUrun.urunKiloFiyati.Clear();
        }

        //listede seçili üründen çarpan kadar(çarpan yoksa 1 tane) ekleme butonu(ekle)
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string kiloMuPorsiyonMu = secilenUrun.urunTuru[0];

            if (kiloMuPorsiyonMu == "Porsiyon & Kilogram")
            {
                // kilo mu porsiyon mu satmak istiyorsunuz diye sor, eğer porsiyon seçilirse kiloMuPorsiyonMu yu Porsiyon yap, kilogram seçilirse normal devam et else'e yani kilograma girer zaten
                KiloMuPorsiyonMuForm kiloMuPorsiyonMuBelirlemeFormu = new KiloMuPorsiyonMuForm();
                DialogResult kiloMu = kiloMuPorsiyonMuBelirlemeFormu.ShowDialog();

                if (kiloMu != DialogResult.OK)
                {
                    kiloMuPorsiyonMu = "Porsiyon";
                }
            }

            if (kiloMuPorsiyonMu == "Porsiyon")
            {
                int kacAdet = Convert.ToInt32(labelCokluAdet.Text);

                double porsiyon = porsiyonNe();

                int gruptaYeniGelenSiparisVarmi = siparisGruptaVarMi(yeniSiparisler, secilenUrun.urunAdi[0], (porsiyon + "P").ToString()); // ürün cinsi hesapta var mı bak 

                decimal eklenecekDeger = Convert.ToDecimal(secilenUrun.urunPorsiyonFiyati[0]) * Convert.ToDecimal(porsiyon);

                if (gruptaYeniGelenSiparisVarmi == -1) // yoksa ürünü hesaba ekle
                {
                    listUrunFiyat.Items.Add(kacAdet.ToString()); // adet

                    listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(porsiyon + "P"); // porsiyon
                    listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(secilenUrun.urunAdi[0]); // ad
                    listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(eklenecekDeger.ToString("0.00")); // fiyat

                    listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems[1].Tag = secilenUrun.urunPorsiyonSinifi[0]; // sınıfı
                    listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems[2].Tag = secilenUrun.urunTuru[0]; // türü 
                    listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems[3].Tag = secilenUrun.urunKiloFiyati[0]; // kilo fiyatı


                    listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Group = listUrunFiyat.Groups[yeniSiparisler];
                    listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font = new Font("Calibri", 18.75F, FontStyle.Bold);
                    listedeSeciliOlanItemlar.Add(false);

                    gorunumuDuzenle(secilenUrun.urunAdi[0], listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems[3].Text, labelCokluAdet.Text, porsiyon.ToString());
                }
                else // varsa ürünün hesaptaki değerlerini istenilene göre arttır
                {
                    listUrunFiyat.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text = (Convert.ToDouble(listUrunFiyat.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text) + kacAdet).ToString();
                }

                labelKalanHesap.Text = (Convert.ToDecimal(labelKalanHesap.Text) + kacAdet * eklenecekDeger).ToString("0.00");
            }
            else // kilogram
            {
                // kilo girme ekranı göster
                KilogramFormu kilogramDegeriFormu = new KilogramFormu();
                DialogResult kilogramDevamMi = kilogramDegeriFormu.ShowDialog();

                if (kilogramDevamMi != DialogResult.No)
                {
                    double kilo = kilogramDegeriFormu.kilo;

                    int kacAdet = 1;

                    int gruptaYeniGelenSiparisVarmi = siparisGruptaVarMi(yeniSiparisler, secilenUrun.urunAdi[0], (((Double)kilo).ToString("0.00") + "K").ToString()); // ürün cinsi hesapta var mı bak 

                    decimal eklenecekDeger = Convert.ToDecimal(secilenUrun.urunKiloFiyati[0]) * Convert.ToDecimal(kilo);

                    if (gruptaYeniGelenSiparisVarmi == -1) // yoksa ürünü hesaba ekle
                    {
                        listUrunFiyat.Items.Add(kacAdet.ToString()); // adet

                        listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(kilo + "K"); // porsiyon
                        listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(secilenUrun.urunAdi[0]); // ad
                        listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(eklenecekDeger.ToString("0.00")); // fiyat

                        listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems[1].Tag = secilenUrun.urunPorsiyonSinifi[0]; // sınıfı
                        listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems[2].Tag = secilenUrun.urunTuru[0]; // türü 
                        listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems[3].Tag = secilenUrun.urunPorsiyonFiyati[0]; // porsiyon fiyatı


                        listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Group = listUrunFiyat.Groups[yeniSiparisler];
                        listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font = new Font("Calibri", 18.75F, FontStyle.Bold);
                        listedeSeciliOlanItemlar.Add(false);

                        gorunumuDuzenle(secilenUrun.urunAdi[0], listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems[3].Text, labelCokluAdet.Text, kilo.ToString());
                    }
                    else // varsa ürünün hesaptaki değerlerini istenilene göre arttır
                    {
                        listUrunFiyat.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text = (Convert.ToDouble(listUrunFiyat.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text) + kacAdet).ToString();
                    }

                    labelKalanHesap.Text = (Convert.ToDecimal(labelKalanHesap.Text) + kacAdet * eklenecekDeger).ToString("0.00");
                }
            }
        }

        private double porsiyonNe()
        {
            if (buttonPorsiyonSec.Text == "Tam")
                return 1.00;
            else if (buttonPorsiyonSec.Text == "Yarım")
                return 0.50;
            else if (buttonPorsiyonSec.Text == "Bir Buçuk")
                return 1.50;
            else if (buttonPorsiyonSec.Text == "Duble")
                return 2.00;
            else if (buttonPorsiyonSec.Text == "Çeyrek")
                return 0.25;
            else if (buttonPorsiyonSec.Text == "Üç Çeyrek")
                return 0.75;
            return 1.00;
        }

        private string[] porsiyonSinifiTuruPorsiyonFiyatiBul(string urunAdi)
        {
            string[] urunPorsiyonSinifiTuruPorsiyonFiyati = new string[3];
            for (int i = 0; i < urunListesi.Count; i++)
            {
                for (int j = 0; j < urunListesi[i].urunAdi.Count; j++)
                {
                    if (urunListesi[i].urunAdi[j] == urunAdi)
                    {
                        urunPorsiyonSinifiTuruPorsiyonFiyati[0] = urunListesi[i].urunPorsiyonSinifi[j].ToString();
                        urunPorsiyonSinifiTuruPorsiyonFiyati[1] = urunListesi[i].urunTuru[j].ToString();
                        urunPorsiyonSinifiTuruPorsiyonFiyati[2] = urunListesi[i].urunPorsiyonFiyati[j].ToString();
                        return urunPorsiyonSinifiTuruPorsiyonFiyati;
                    }
                }
            }
            return null;
        }

        private string[] porsiyonSinifiTuruKiloFiyatiBul(string urunAdi)
        {
            string[] urunPorsiyonSinifiTuruKiloFiyati = new string[3];
            for (int i = 0; i < urunListesi.Count; i++)
            {
                for (int j = 0; j < urunListesi[i].urunAdi.Count; j++)
                {
                    if (urunListesi[i].urunAdi[j] == urunAdi)
                    {
                        urunPorsiyonSinifiTuruKiloFiyati[0] = urunListesi[i].urunPorsiyonSinifi[j].ToString();
                        urunPorsiyonSinifiTuruKiloFiyati[1] = urunListesi[i].urunTuru[j].ToString();
                        urunPorsiyonSinifiTuruKiloFiyati[2] = urunListesi[i].urunKiloFiyati[j].ToString();
                        return urunPorsiyonSinifiTuruKiloFiyati;
                    }
                }
            }
            return null;
        }

        public void masaDegisimHatasi(string mesaj)
        {
            KontrolFormu dialog = new KontrolFormu(mesaj, false);
            dialog.Show();
        }

        //Bu form kapandığında masanın son durumunu düzenlemesi için masa formunu uyar
        private void SiparisMenuFormu_FormClosing(object sender, FormClosingEventArgs e)
        {
            masaFormu.siparisFormKapandiginda();
        }

        //form load
        private void SiparisMenuFormu_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Server == 2)
            {
                if (masaAcikMi)
                {
                    buttonMasaDegistir.Enabled = true;

                    SqlCommand cmd = SQLBaglantisi.getCommand("SELECT Fiyatı, Adet, YemekAdi, IkramMi, Garsonu, Porsiyon, KiloSatisiMi from Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.MasaAdi='" + MasaAdi + "' AND Adisyon.DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 ORDER BY Adet DESC");
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        decimal yemeginFiyati;
                        int kacAdet;
                        string yemeginAdi;
                        bool ikramMi;
                        string Garson;
                        double porsiyonu;
                        bool kiloSatisiMi;

                        try
                        {
                            yemeginFiyati = dr.GetDecimal(0);
                            kacAdet = dr.GetInt32(1);
                            yemeginAdi = dr.GetString(2);
                            ikramMi = dr.GetBoolean(3);
                            Garson = dr.GetString(4);
                            porsiyonu = Convert.ToDouble(dr.GetDecimal(5));
                            kiloSatisiMi = dr.GetBoolean(6);
                        }
                        catch
                        {
                            KontrolFormu dialog = new KontrolFormu("Masa bilgileri alınırken hata oluştu, lütfen tekrar deneyiniz", false);
                            dialog.Show();
                            break;
                        }

                        int hangiGrup;

                        if (ikramMi)
                        {
                            hangiGrup = 0;
                        }
                        else
                        {
                            hangiGrup = 2;
                        }

                        string tur = "P";

                        if (kiloSatisiMi)
                            tur = "K";

                        int gruptaYeniGelenSiparisVarmi = siparisGruptaVarMi(hangiGrup, yemeginAdi, (porsiyonu + tur).ToString()); //ürün cinsi hesapta var mı bak 


                        if (gruptaYeniGelenSiparisVarmi == -1) //yoksa ürünü hesaba ekle
                        {
                            listUrunFiyat.Items.Add(kacAdet.ToString());
                            listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(porsiyonu + tur);
                            listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(yemeginAdi);
                            listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(yemeginFiyati.ToString("0.00"));

                            string[] porsiyonSinifiTuruKiloFiyati = porsiyonSinifiTuruKiloFiyatiBul(yemeginAdi);

                            listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems[1].Tag = porsiyonSinifiTuruKiloFiyati[0]; // sınıfı
                            listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems[2].Tag = porsiyonSinifiTuruKiloFiyati[1]; // türü 
                            listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems[3].Tag = porsiyonSinifiTuruKiloFiyati[2]; // kilo fiyatı   

                            listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Group = listUrunFiyat.Groups[hangiGrup];
                            listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font = new Font("Calibri", 18.75F, FontStyle.Bold);
                            listedeSeciliOlanItemlar.Add(false);

                            gorunumuDuzenle(yemeginAdi, yemeginFiyati.ToString("0.00"), kacAdet.ToString(), porsiyonu.ToString());
                        }
                        else // varsa ürünün hesaptaki değerlerini istenilene göre arttır
                        {
                            listUrunFiyat.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text = (Convert.ToDouble(listUrunFiyat.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text) + kacAdet).ToString();
                        }

                        if (!ikramMi) // ikram değilse kalan hesaba gir
                            labelKalanHesap.Text = (Convert.ToDecimal(labelKalanHesap.Text) + kacAdet * yemeginFiyati).ToString("0.00");

                    }

                    cmd = SQLBaglantisi.getCommand("SELECT OdenenMiktar from OdemeDetay JOIN Adisyon ON OdemeDetay.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.MasaAdi='" + MasaAdi + "' AND Adisyon.DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND Adisyon.AcikMi=1 AND Adisyon.IptalMi=0");
                    dr = cmd.ExecuteReader();

                    decimal odenenMiktar = 0;
                    while (dr.Read())
                    {
                        odenenMiktar += dr.GetDecimal(0);
                    }

                    labelKalanHesap.Text = (Convert.ToDecimal(labelKalanHesap.Text) - odenenMiktar).ToString("0.00");

                    cmd = SQLBaglantisi.getCommand("SELECT Fiyatı*Adet FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.MasaAdi='" + MasaAdi + "' AND Adisyon.DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND Siparis.IptalMi=0 AND Siparis.OdendiMi=1 AND Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 ORDER BY Adet DESC");
                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        labelKalanHesap.Text = (Convert.ToDecimal(labelKalanHesap.Text) + dr.GetDecimal(0)).ToString("0.00");
                    }

                    cmd.Connection.Close();
                    cmd.Connection.Dispose();

                    if (this.listUrunFiyat.Items.Count > 0)
                    {
                        int itemsCount = this.listUrunFiyat.Items.Count + 3;// 3 aslında grup sayısı -1
                        int itemHeight = this.listUrunFiyat.Items[0].Bounds.Height;
                        int VisiableItem = (int)this.listUrunFiyat.ClientRectangle.Height / itemHeight;
                        if (itemsCount >= VisiableItem)
                        {
                            listUrunFiyat.Columns[2].Width = urunBoyu;
                            listUrunFiyat.Columns[3].Width = fiyatBoyu;
                        }
                    }
                }
            }
            else
            {
                if (masaAcikMi)
                {
                    masaFormu.menuFormundanServeraYolla(MasaAdi, hangiDepartman.departmanAdi, "LoadSiparis");
                }
            }

            Button menuBasligi = flowPanelMenuBasliklari.Controls[0] as Button;
            menuBasligi.PerformClick();
            menuBasligi.BackColor = SystemColors.ActiveCaption;
            menuBasligi.ForeColor = Color.White;
        }

        //form load sırasında masanın sipariş bilgileri serverdan clienta geldiğinde çalışan method
        public void LoadSiparis(string siparisBilgileri)
        {
            buttonMasaDegistir.Enabled = true;

            string[] siparisler = null;
            try
            {
                siparisler = siparisBilgileri.Split('*');
            }
            catch
            {
                KontrolFormu dialog = new KontrolFormu("Masa bilgileri alınırken hata oluştu, lütfen tekrar deneyiniz", false);
                dialog.Show();
                return;
            }

            decimal yemeginFiyati, porsiyonu;
            int adet;
            string yemeginAdi, Garson;
            bool ikramMi, kiloSatisiMi;

            for (int i = 0; i < siparisler.Count(); i++)
            {
                string[] detaylari = siparisler[i].Split('-');
                yemeginFiyati = Convert.ToDecimal(detaylari[0]);
                adet = Convert.ToInt32(detaylari[1]);
                yemeginAdi = detaylari[2];
                ikramMi = Convert.ToBoolean(detaylari[3]);
                Garson = detaylari[4];
                porsiyonu = Convert.ToDecimal(detaylari[5]);
                kiloSatisiMi = Convert.ToBoolean(detaylari[6]);

                int hangiGrup;

                if (ikramMi)
                {
                    hangiGrup = 0;
                }
                else
                {
                    hangiGrup = 2;
                }

                string tur = "P";

                if (kiloSatisiMi)
                    tur = "K";

                int gruptaYeniGelenSiparisVarmi = siparisGruptaVarMi(hangiGrup, yemeginAdi, ((Double)porsiyonu + tur).ToString()); //ürün cinsi hesapta var mı bak

                decimal eklenecekDeger = yemeginFiyati * porsiyonu;

                if (gruptaYeniGelenSiparisVarmi == -1) //yoksa ürünü hesaba ekle
                {
                    listUrunFiyat.Items.Add(adet.ToString());
                    listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add((Double)porsiyonu + tur);
                    listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(yemeginAdi);
                    listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(eklenecekDeger.ToString("0.00"));

                    string[] porsiyonSinifiTuruKiloVeyaPorsiyonFiyati;

                    if (tur == "P")
                        porsiyonSinifiTuruKiloVeyaPorsiyonFiyati = porsiyonSinifiTuruPorsiyonFiyatiBul(yemeginAdi);
                    else
                        porsiyonSinifiTuruKiloVeyaPorsiyonFiyati = porsiyonSinifiTuruKiloFiyatiBul(yemeginAdi);

                    listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems[1].Tag = porsiyonSinifiTuruKiloVeyaPorsiyonFiyati[0]; // sınıfı
                    listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems[2].Tag = porsiyonSinifiTuruKiloVeyaPorsiyonFiyati[1]; // türü 
                    listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems[3].Tag = porsiyonSinifiTuruKiloVeyaPorsiyonFiyati[2]; // kilo fiyatı

                    listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Group = listUrunFiyat.Groups[hangiGrup];
                    listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font = new Font("Calibri", 18.75F, FontStyle.Bold);
                    listedeSeciliOlanItemlar.Add(false);

                    gorunumuDuzenle(yemeginAdi, eklenecekDeger.ToString(), porsiyonu.ToString(), adet.ToString());
                }
                else // varsa ürünün hesaptaki değerlerini istenilene göre arttır
                {
                    listUrunFiyat.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text = (Convert.ToDouble(listUrunFiyat.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text) + adet).ToString();
                }

                if (!ikramMi) // ikram değilse kalan hesaba gir
                    labelKalanHesap.Text = (Convert.ToDecimal(labelKalanHesap.Text) + (decimal)adet * eklenecekDeger).ToString("0.00");
            }

            if (this.listUrunFiyat.Items.Count > 0)
            {
                int itemsCount = this.listUrunFiyat.Items.Count + 3;// 3 aslında grup sayısı -1
                int itemHeight = this.listUrunFiyat.Items[0].Bounds.Height;
                int VisiableItem = (int)this.listUrunFiyat.ClientRectangle.Height / itemHeight;
                if (itemsCount >= VisiableItem)
                {
                    listUrunFiyat.Columns[2].Width = urunBoyu;
                    listUrunFiyat.Columns[3].Width = fiyatBoyu;
                }
            }
        }

        //adisyon notu ekleme butonu
        private void addNoteButton_Click(object sender, EventArgs e)
        {
            adisyonNotuGuncellenmeliMi = true;
            if (Properties.Settings.Default.Server == 2)
            {
                AdisyonNotuFormu notFormu;

                //adisyonNotu'nu sql den al
                SqlCommand cmd = SQLBaglantisi.getCommand("SELECT AdisyonNotu FROM Adisyon WHERE AcikMi=1 AND MasaAdi='" + MasaAdi + "' AND DepartmanAdi='" + hangiDepartman.departmanAdi + "'");
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();

                try
                {
                    adisyonNotu = dr.GetString(0);
                }
                catch
                {
                    KontrolFormu dialog = new KontrolFormu("Adisyon notunu oluştururken bir hata oluştu, lütfen tekrar deneyiniz", false);
                    dialog.Show();
                    return;
                }

                cmd.Connection.Close();
                cmd.Connection.Dispose();

                notFormu = new AdisyonNotuFormu(adisyonNotu);

                notFormu.ShowDialog();

                adisyonNotu = notFormu.AdisyonNotu;
            }
            else
            {
                masaFormu.menuFormundanServeraYolla(MasaAdi, hangiDepartman.departmanAdi, "AdisyonNotu");
            }
        }

        public void AdisyonNotuGeldi(string gelenAdisyonNotu)
        {
            if (gelenAdisyonNotu == "1")
            {
                KontrolFormu dialog = new KontrolFormu("Adisyon notunu oluştururken bir hata oluştu, lütfen tekrar deneyiniz", false);
                dialog.Show();
                return;
            }
            AdisyonNotuFormu notFormu;

            notFormu = new AdisyonNotuFormu(gelenAdisyonNotu);

            notFormu.ShowDialog();

            adisyonNotu = notFormu.AdisyonNotu;
        }

        public void ikramGeldi(string miktar, string yemekAdi, string dusulecekDegerGelen, string porsiyonu, bool kiloSatisiMi)
        {
            int carpan = Convert.ToInt32(miktar);
            string tur = "P";

            if (kiloSatisiMi)
                tur = "K";

            // ürünün değerini istenilen kadar azalt, kalan hesaptan düş
            double dusulecekDeger = Convert.ToDouble(dusulecekDegerGelen);

            for (int i = 0; i < listUrunFiyat.Groups[eskiSiparisler].Items.Count; i++)
            {
                if (listUrunFiyat.Groups[eskiSiparisler].Items[i].SubItems[2].Text == yemekAdi && Convert.ToDouble(listUrunFiyat.Groups[eskiSiparisler].Items[i].SubItems[1].Text.Substring(0, listUrunFiyat.Groups[eskiSiparisler].Items[i].SubItems[1].Text.Length - 1)) == Convert.ToDouble(porsiyonu) && tur == listUrunFiyat.Groups[eskiSiparisler].Items[i].SubItems[1].Text[listUrunFiyat.Groups[eskiSiparisler].Items[i].SubItems[1].Text.Length - 1].ToString())
                {
                    listUrunFiyat.Groups[eskiSiparisler].Items[i].SubItems[0].Text = (Convert.ToDouble(listUrunFiyat.Groups[eskiSiparisler].Items[i].SubItems[0].Text) - carpan).ToString();

                    labelKalanHesap.Text = (Convert.ToDouble(labelKalanHesap.Text) - (dusulecekDeger * carpan)).ToString("0.00");

                    bool ikramYok = true; // ikram yeni ikramlar listesinde var mı diye bak 

                    for (int x = 0; x < listUrunFiyat.Groups[yeniIkramlar].Items.Count; x++) // varsa yeni ikramı olana ekle
                    {
                        if (listUrunFiyat.Groups[yeniIkramlar].Items[x].SubItems[2].Text == yemekAdi && Convert.ToDouble(listUrunFiyat.Groups[yeniIkramlar].Items[x].SubItems[1].Text.Substring(0, listUrunFiyat.Groups[yeniIkramlar].Items[x].SubItems[1].Text.Length - 1)) == Convert.ToDouble(porsiyonu))
                        {
                            listUrunFiyat.Groups[yeniIkramlar].Items[x].Text = (Convert.ToDouble(listUrunFiyat.Groups[yeniIkramlar].Items[x].Text) + carpan).ToString();

                            ikramYok = false;
                            break;
                        }
                    }

                    if (ikramYok) // yok yeni ikramı listeye ekle
                    {
                        listUrunFiyat.Items.Insert(0, carpan.ToString());

                        listUrunFiyat.Items[0].SubItems.Add(porsiyonu + tur);
                        listUrunFiyat.Items[0].SubItems.Add(yemekAdi);
                        listUrunFiyat.Items[0].SubItems.Add((dusulecekDeger).ToString("0.00"));

                        string[] porsiyonSinifiTuruKiloVeyaPorsiyonFiyati;

                        if (tur == "P")
                            porsiyonSinifiTuruKiloVeyaPorsiyonFiyati = porsiyonSinifiTuruPorsiyonFiyatiBul(yemekAdi);
                        else
                            porsiyonSinifiTuruKiloVeyaPorsiyonFiyati = porsiyonSinifiTuruKiloFiyatiBul(yemekAdi);

                        listUrunFiyat.Items[0].SubItems[1].Tag = porsiyonSinifiTuruKiloVeyaPorsiyonFiyati[0]; // sınıfı
                        listUrunFiyat.Items[0].SubItems[2].Tag = porsiyonSinifiTuruKiloVeyaPorsiyonFiyati[1]; // türü 
                        listUrunFiyat.Items[0].SubItems[3].Tag = porsiyonSinifiTuruKiloVeyaPorsiyonFiyati[2]; // kilo fiyatı  

                        listUrunFiyat.Items[0].Group = listUrunFiyat.Groups[yeniIkramlar];
                        listUrunFiyat.Items[0].Font = new Font("Calibri", 18.75F, FontStyle.Bold);
                        listedeSeciliOlanItemlar.Insert(0, false);
                    }

                    if (listUrunFiyat.Groups[eskiSiparisler].Items[i].Text == "0")
                    {
                        listedeSeciliOlanItemlar.RemoveAt(listUrunFiyat.Groups[eskiSiparisler].Items[i].Index);
                        listUrunFiyat.Groups[eskiSiparisler].Items[i].Remove();
                    }

                    buttonTemizle_Click(null, null);
                    this.Enabled = true;
                    return;
                }
            }

            dialogTimer = new KontrolFormu("Siparişlerde değişiklik oldu, lütfen masaya tekrar giriniz", false, this);
            this.Enabled = false;
            timerDialogClose.Start();
            dialogTimer.Show();
        }

        public void ikramIptaliGeldi(string miktar, string yemekAdi, string dusulecekDegerGelen, string ikramYeniMiEskiMiGelen, string porsiyonu, bool kiloSatisiMi)
        {
            int ikramYeniMiEskiMi = (int)Convert.ToDouble(ikramYeniMiEskiMiGelen);

            int carpan = Convert.ToInt32(miktar);

            string tur = "P";

            if (kiloSatisiMi)
                tur = "K";

            // ürünün değerini bul ve hesaba ekle
            double dusulecekDeger = Convert.ToDouble(dusulecekDegerGelen);

            int degisecekSiparisIndexi = -1;
            for (int i = 0; i < listUrunFiyat.Groups[ikramYeniMiEskiMi].Items.Count; i++)
            {
                if (listUrunFiyat.Groups[ikramYeniMiEskiMi].Items[i].SubItems[2].Text == yemekAdi && Convert.ToDouble(listUrunFiyat.Groups[ikramYeniMiEskiMi].Items[i].SubItems[1].Text.Substring(0, listUrunFiyat.Groups[ikramYeniMiEskiMi].Items[i].SubItems[1].Text.Length - 1)) == Convert.ToDouble(porsiyonu) && Convert.ToInt32(listUrunFiyat.Groups[ikramYeniMiEskiMi].Items[i].SubItems[0].Text) >= carpan && tur == listUrunFiyat.Groups[ikramYeniMiEskiMi].Items[i].SubItems[1].Text[listUrunFiyat.Groups[ikramYeniMiEskiMi].Items[i].SubItems[1].Text.Length - 1].ToString())
                {
                    degisecekSiparisIndexi = i;
                    break;
                }
            }

            if (ikramYeniMiEskiMiGelen == "1,0")
            {
                for (int i = 0; i < listUrunFiyat.Groups[0].Items.Count; i++)
                {
                    if (listUrunFiyat.Groups[0].Items[i].SubItems[2].Text == yemekAdi && Convert.ToDouble(listUrunFiyat.Groups[0].Items[i].SubItems[1].Text.Substring(0, listUrunFiyat.Groups[0].Items[i].SubItems[1].Text.Length - 1)) == Convert.ToDouble(porsiyonu) && Convert.ToInt32(listUrunFiyat.Groups[0].Items[i].SubItems[0].Text) >= carpan && tur == listUrunFiyat.Groups[ikramYeniMiEskiMi].Items[i].SubItems[1].Text[listUrunFiyat.Groups[ikramYeniMiEskiMi].Items[i].SubItems[1].Text.Length - 1].ToString())
                    {
                        degisecekSiparisIndexi = i;
                        ikramYeniMiEskiMi = 0;
                        break;
                    }
                }
            }

            if (degisecekSiparisIndexi == -1)
            {
                dialogTimer = new KontrolFormu("Siparişlerde değişiklik oldu, lütfen masaya tekrar giriniz", false, this);

                timerDialogClose.Start();
                this.Enabled = false;
                dialogTimer.Show();
                return;
            }

            listUrunFiyat.Groups[ikramYeniMiEskiMi].Items[degisecekSiparisIndexi].SubItems[0].Text = (Convert.ToDouble(listUrunFiyat.Groups[ikramYeniMiEskiMi].Items[degisecekSiparisIndexi].SubItems[0].Text) - carpan).ToString();

            labelKalanHesap.Text = (Convert.ToDouble(labelKalanHesap.Text) + (dusulecekDeger * carpan)).ToString("0.00");

            bool urunYok = true;

            for (int i = 0; i < listUrunFiyat.Groups[eskiSiparisler].Items.Count; i++)
            {
                if (listUrunFiyat.Groups[eskiSiparisler].Items[i].SubItems[2].Text == yemekAdi && listUrunFiyat.Groups[eskiSiparisler].Items[i].SubItems[1].Text == Convert.ToDouble(porsiyonu).ToString() + tur)
                {
                    listUrunFiyat.Groups[eskiSiparisler].Items[i].Text = (Convert.ToDouble(listUrunFiyat.Groups[eskiSiparisler].Items[i].Text) + carpan).ToString();
                    urunYok = false;
                    break;
                }
            }

            if (urunYok)
            {
                listUrunFiyat.Items.Add(carpan.ToString());
                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(Convert.ToDouble(porsiyonu).ToString() + tur);
                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(yemekAdi);
                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(dusulecekDeger.ToString("0.00"));
                
                string[] porsiyonSinifiTuruKiloVeyaPorsiyonFiyati;

                if (tur == "K")
                    porsiyonSinifiTuruKiloVeyaPorsiyonFiyati = porsiyonSinifiTuruPorsiyonFiyatiBul(yemekAdi);
                else
                    porsiyonSinifiTuruKiloVeyaPorsiyonFiyati = porsiyonSinifiTuruKiloFiyatiBul(yemekAdi);

                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems[1].Tag = porsiyonSinifiTuruKiloVeyaPorsiyonFiyati[0]; // sınıfı
                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems[2].Tag = porsiyonSinifiTuruKiloVeyaPorsiyonFiyati[1]; // türü 
                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems[3].Tag = porsiyonSinifiTuruKiloVeyaPorsiyonFiyati[2]; // kilo veya porsiyon fiyatı  
                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Group = listUrunFiyat.Groups[eskiSiparisler];
                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font = new Font("Calibri", 18.75F, FontStyle.Bold);
                listedeSeciliOlanItemlar.Add(false);
            }

            if (listUrunFiyat.Groups[ikramYeniMiEskiMi].Items[degisecekSiparisIndexi].Text == "0")
            {
                listedeSeciliOlanItemlar.RemoveAt(listUrunFiyat.Groups[ikramYeniMiEskiMi].Items[degisecekSiparisIndexi].Index);
                listUrunFiyat.Groups[ikramYeniMiEskiMi].Items[degisecekSiparisIndexi].Remove();
            }

            buttonTemizle_Click(null, null);
            this.Enabled = true;
        }

        // ürün ikram etme ve ikramı iptal etme butonu
        private void buttonUrunIkram_Click(object sender, EventArgs e)
        {
            int kacAdet = Convert.ToInt32(labelCokluAdet.Text);

            if (Convert.ToDecimal(listUrunFiyat.SelectedItems[0].SubItems[3].Text) * kacAdet > Convert.ToDecimal(labelKalanHesap.Text) && buttonUrunIkram.Text == "İkram")
            {
                KontrolFormu dialog = new KontrolFormu("Ürün fiyatı kalan hesaptan büyük olduğu için ürün ikram edilemez", false);
                dialog.Show();
                return;
            }

            double porsiyonu = Convert.ToDouble(listUrunFiyat.SelectedItems[0].SubItems[1].Text.Substring(0, listUrunFiyat.SelectedItems[0].SubItems[1].Text.Length - 1));

            if (kacAdet > Convert.ToInt32(listUrunFiyat.SelectedItems[0].SubItems[0].Text))
                kacAdet = Convert.ToInt32(listUrunFiyat.SelectedItems[0].SubItems[0].Text);

            double dusulecekDeger = Convert.ToDouble(listUrunFiyat.SelectedItems[0].SubItems[3].Text);

            string tur = listUrunFiyat.SelectedItems[0].SubItems[1].Text[listUrunFiyat.SelectedItems[0].SubItems[1].Text.Length - 1].ToString();

            bool turBool= false;

            if (tur == "K")
            {
                turBool = true;
            }

            if (Properties.Settings.Default.Server == 2) //server - diğer tüm clientlara söylemeli yaptığı ikram vs. neyse
            {
                // ikram et
                if (buttonUrunIkram.Text == "İkram")
                {
                    int istenilenikramSayisi = kacAdet;

                    // ürünün değerini istenilen kadar azalt, kalan hesaptan düş
                    listUrunFiyat.SelectedItems[0].SubItems[0].Text = (Convert.ToDouble(listUrunFiyat.SelectedItems[0].SubItems[0].Text) - kacAdet).ToString();

                    labelKalanHesap.Text = (Convert.ToDouble(labelKalanHesap.Text) - (dusulecekDeger * kacAdet)).ToString("0.00");

                    bool ikramYok = true; // ikram  yeni ikramlar listesinde var mı diye bak 

                    for (int i = 0; i < listUrunFiyat.Groups[yeniIkramlar].Items.Count; i++) // varsa yeni ikramı olana ekle
                    {
                        if (listUrunFiyat.Groups[yeniIkramlar].Items[i].SubItems[2].Text == listUrunFiyat.SelectedItems[0].SubItems[2].Text && listUrunFiyat.Groups[yeniIkramlar].Items[i].SubItems[1].Text == listUrunFiyat.SelectedItems[0].SubItems[1].Text && listUrunFiyat.Groups[yeniIkramlar].Items[i].SubItems[1].Text[listUrunFiyat.Groups[yeniIkramlar].Items[i].SubItems[1].Text.Length - 1] == listUrunFiyat.SelectedItems[0].SubItems[1].Text[listUrunFiyat.SelectedItems[0].SubItems[1].Text.Length - 1]) // yemek adi, porsiyon, tür kontrolü
                        {
                            listUrunFiyat.Groups[yeniIkramlar].Items[i].Text = (Convert.ToDouble(listUrunFiyat.Groups[yeniIkramlar].Items[i].Text) + kacAdet).ToString();
                            ikramYok = false;
                            break;
                        }
                    }

                    if (ikramYok) // yok yeni ikramı listeye ekle
                    {
                        listUrunFiyat.Items.Insert(0, kacAdet.ToString());
                        listUrunFiyat.Items[0].SubItems.Add(listUrunFiyat.SelectedItems[0].SubItems[1].Text);
                        listUrunFiyat.Items[0].SubItems.Add(listUrunFiyat.SelectedItems[0].SubItems[2].Text);
                        listUrunFiyat.Items[0].SubItems.Add((dusulecekDeger).ToString("0.00"));

                        string[] porsiyonSinifiTuruKiloVeyaPorsiyonFiyati;

                        if(tur == "P")
                            porsiyonSinifiTuruKiloVeyaPorsiyonFiyati = porsiyonSinifiTuruPorsiyonFiyatiBul(listUrunFiyat.SelectedItems[0].SubItems[2].Text);
                        else
                            porsiyonSinifiTuruKiloVeyaPorsiyonFiyati = porsiyonSinifiTuruKiloFiyatiBul(listUrunFiyat.SelectedItems[0].SubItems[2].Text);

                        listUrunFiyat.Items[0].SubItems[1].Tag = porsiyonSinifiTuruKiloVeyaPorsiyonFiyati[0]; // sınıfı
                        listUrunFiyat.Items[0].SubItems[2].Tag = porsiyonSinifiTuruKiloVeyaPorsiyonFiyati[1]; // türü 
                        listUrunFiyat.Items[0].SubItems[3].Tag = porsiyonSinifiTuruKiloVeyaPorsiyonFiyati[2]; // kilo fiyatı  

                        listUrunFiyat.Items[0].Group = listUrunFiyat.Groups[yeniIkramlar];
                        listUrunFiyat.Items[0].Font = new Font("Calibri", 18.75F, FontStyle.Bold);
                        listedeSeciliOlanItemlar.Insert(0, false);
                    }

                    //İkram edildiğinde önce hangi grupta olduğuna bakılacak, eğer yeni eklenenler grubunda (yani 3 indeksli grup) ise SQL o grubu update etmeye gerek yok direk ikram kaldırılacak
                    //Eğer eskiden ekli olanlarda ise (yani 1 indeksli grup) ikram adedine ulaşana kadar update yaparak sipariş sayısını azaltacaz
                    //Eğer iptal edilen adedi tam olarak siparişlerde bulunamazsa örneğin 4 iptal var 2 tane 3 adetlik sipariş var yani toplam 6
                    //İlk gelen siparişin ikram özelliği true(1) yapılacak diğerinin adedi update edilerek azaltılacak ikramın kalanı kadarıyla yeni ikram siparişi oluşturulacak

                    SqlCommand cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adisyon.AdisyonID,Adet,Siparis.VerilisTarihi,NotificationGorulduMu FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi=0 AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + MasaAdi + "' AND Adisyon.DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND Siparis.YemekAdi='" + listUrunFiyat.SelectedItems[0].SubItems[2].Text + "' AND Siparis.Garsonu='" + siparisiKimGirdi + "' AND Siparis.Porsiyon=CONVERT(DECIMAL(5,2),@_Porsiyon) AND Siparis.KiloSatisiMi=@_Tur ORDER BY Adet DESC");

                    cmd.Parameters.AddWithValue("@_Porsiyon", porsiyonu);
                    cmd.Parameters.AddWithValue("@_Tur", turBool);

                    SqlDataReader dr = cmd.ExecuteReader();

                    int siparisID, adisyonID = 0, adet;
                    DateTime verilisTarihi;
                    bool NotificationGorulduMu;
                    while (dr.Read())
                    {
                        try
                        {
                            siparisID = dr.GetInt32(0);
                            adisyonID = dr.GetInt32(1);
                            adet = dr.GetInt32(2);
                            verilisTarihi = dr.GetDateTime(3);
                            NotificationGorulduMu = dr.GetBoolean(4);
                        }
                        catch
                        {
                            KontrolFormu dialog = new KontrolFormu("Ürünü ikram ederken bir hata oluştu, lütfen tekrar deneyiniz", false);
                            dialog.Show();
                            return;
                        }

                        if (adet < istenilenikramSayisi) // elimizde ikram edilmemişler ikramı istenenden küçükse
                        {
                            ikramUpdateTam(siparisID, 1);

                            istenilenikramSayisi -= adet;
                        }
                        else if (adet > istenilenikramSayisi) // den büyükse
                        {
                            ikramUpdateInsert(siparisID, adisyonID, adet, dusulecekDeger, istenilenikramSayisi, listUrunFiyat.SelectedItems[0].SubItems[2].Text, 1, verilisTarihi, porsiyonu, turBool, NotificationGorulduMu);

                            istenilenikramSayisi = 0;
                        }
                        else // elimizde ikram edilmemişler ikramı istenene eşitse
                        {
                            ikramUpdateTam(siparisID, 1);

                            istenilenikramSayisi = 0;
                        }
                    }

                    if (istenilenikramSayisi != 0)// ikram edilecekler daha bitmedi başka garsonların siparişlerinden ikram iptaline devam et
                    {
                        cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adet,Siparis.VerilisTarihi,Adisyon.AdisyonID,NotificationGorulduMu FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi=0 AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + MasaAdi + "' AND Adisyon.DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND Siparis.YemekAdi='" + listUrunFiyat.SelectedItems[0].SubItems[2].Text + "' AND Siparis.Garsonu!='" + siparisiKimGirdi + "' AND Siparis.Porsiyon=CONVERT(DECIMAL(5,2),@_Porsiyon) AND Siparis.KiloSatisiMi=@_Tur ORDER BY Adet DESC");

                        cmd.Parameters.AddWithValue("@_Porsiyon", porsiyonu);
                        cmd.Parameters.AddWithValue("@_Tur", turBool);

                        dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            try
                            {
                                siparisID = dr.GetInt32(0);
                                adet = dr.GetInt32(1);
                                verilisTarihi = dr.GetDateTime(2);
                                adisyonID = dr.GetInt32(3);
                                NotificationGorulduMu = dr.GetBoolean(4);
                            }
                            catch
                            {
                                KontrolFormu dialog = new KontrolFormu("Ürünü ikram ederken bir hata oluştu, lütfen tekrar deneyiniz", false);
                                dialog.Show();
                                return;
                            }

                            if (adet < istenilenikramSayisi) // elimizde ikram edilmemişler ikramı istenenden küçükse
                            {
                                ikramUpdateTam(siparisID, 1);

                                istenilenikramSayisi -= adet;
                            }
                            else if (adet > istenilenikramSayisi) // den büyükse
                            {
                                ikramUpdateInsert(siparisID, adisyonID, adet, dusulecekDeger, istenilenikramSayisi, listUrunFiyat.SelectedItems[0].SubItems[2].Text, 1, verilisTarihi, porsiyonu, turBool, NotificationGorulduMu);

                                istenilenikramSayisi = 0;
                            }
                            else // elimizde ikram edilmemişler ikramı istenene eşitse
                            {
                                ikramUpdateTam(siparisID, 1);

                                istenilenikramSayisi = 0;
                            }
                        }
                    }

                    adisyonNotuUpdate(adisyonID);

                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                    
                    masaFormu.serverdanSiparisIkramVeyaIptal(MasaAdi, hangiDepartman.departmanAdi, "ikram", kacAdet.ToString(), listUrunFiyat.SelectedItems[0].SubItems[2].Text, dusulecekDeger.ToString(), null, porsiyonu.ToString(), tur);

                    if (listUrunFiyat.SelectedItems[0].Text == "0")
                    {
                        listedeSeciliOlanItemlar.RemoveAt(listUrunFiyat.SelectedItems[0].Index);
                        listUrunFiyat.SelectedItems[0].Remove();
                    }
                }
                else // ikramı iptal et
                {
                    int istenilenIkramiptalSayisi = kacAdet;
                    // ürünün değerini bul ve hesaba ekle

                    listUrunFiyat.SelectedItems[0].SubItems[0].Text = (Convert.ToDouble(listUrunFiyat.SelectedItems[0].SubItems[0].Text) - kacAdet).ToString();

                    labelKalanHesap.Text = (Convert.ToDouble(labelKalanHesap.Text) + (dusulecekDeger * kacAdet)).ToString("0.00");

                    bool urunYok = true;

                    for (int i = 0; i < listUrunFiyat.Groups[eskiSiparisler].Items.Count; i++)
                    {
                        if (listUrunFiyat.Groups[eskiSiparisler].Items[i].SubItems[2].Text == listUrunFiyat.SelectedItems[0].SubItems[2].Text && listUrunFiyat.Groups[eskiSiparisler].Items[i].SubItems[1].Text == listUrunFiyat.SelectedItems[0].SubItems[1].Text && listUrunFiyat.Groups[eskiSiparisler].Items[i].SubItems[1].Text[listUrunFiyat.Groups[eskiSiparisler].Items[i].SubItems[1].Text.Length - 1] == listUrunFiyat.SelectedItems[0].SubItems[1].Text[listUrunFiyat.SelectedItems[0].SubItems[1].Text.Length - 1]) // yemek adi, porsiyon, tür kontrolü
                        {
                            listUrunFiyat.Groups[eskiSiparisler].Items[i].Text = (Convert.ToDouble(listUrunFiyat.Groups[eskiSiparisler].Items[i].Text) + kacAdet).ToString();
                            urunYok = false;
                            break;
                        }
                    }

                    if (urunYok)
                    {
                        listUrunFiyat.Items.Add(kacAdet.ToString());
                        listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(listUrunFiyat.SelectedItems[0].SubItems[1].Text);
                        listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(listUrunFiyat.SelectedItems[0].SubItems[2].Text);
                        listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(dusulecekDeger.ToString("0.00"));

                        string[] porsiyonSinifiTuruKiloVeyaPorsiyonFiyati;

                        if (tur == "P")
                            porsiyonSinifiTuruKiloVeyaPorsiyonFiyati = porsiyonSinifiTuruPorsiyonFiyatiBul(listUrunFiyat.SelectedItems[0].SubItems[2].Text);
                        else
                            porsiyonSinifiTuruKiloVeyaPorsiyonFiyati = porsiyonSinifiTuruKiloFiyatiBul(listUrunFiyat.SelectedItems[0].SubItems[2].Text);

                        listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems[1].Tag = porsiyonSinifiTuruKiloVeyaPorsiyonFiyati[0]; // sınıfı
                        listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems[2].Tag = porsiyonSinifiTuruKiloVeyaPorsiyonFiyati[1]; // türü 
                        listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems[3].Tag = porsiyonSinifiTuruKiloVeyaPorsiyonFiyati[2]; // kilo fiyatı  

                        listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Group = listUrunFiyat.Groups[eskiSiparisler];
                        listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font = new Font("Calibri", 18.75F, FontStyle.Bold);
                        listedeSeciliOlanItemlar.Add(false);
                    }

                    //İkram iptal edildiğinde önce hangi grupta olduğuna bakılacak, eğer yeni eklenenler grubunda (yani 2. grup) ise SQL de o grubu update etmeye gerek yok
                    //Eğer eskiden ekli olanlarda ise ikram adedine ulaşana kadar update yaparak sipariş sayısını azaltacaz
                    //Eğer iptal edilen adedi tam olarak siparişlerde bulunamazsa örneğin 4 iptal var 2 tane 3 adetlik sipariş var yani toplam 6
                    //İlk gelen siparişin ikram özelliği true(1) yapılacak diğerinin adedi update edilerek azaltılacak ikramın kalanı kadarıyla yeni ikram siparişi oluşturulacak


                    SqlCommand cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adisyon.AdisyonID,Adet,Siparis.VerilisTarihi,NotificationGorulduMu FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi=1 AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + MasaAdi + "' AND Adisyon.DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND Siparis.YemekAdi='" + listUrunFiyat.SelectedItems[0].SubItems[2].Text + "' AND Siparis.Garsonu='" + siparisiKimGirdi + "' AND Siparis.Porsiyon=CONVERT(DECIMAL(5,2),@_Porsiyon) AND Siparis.KiloSatisiMi=@_Tur ORDER BY Adet DESC");

                    cmd.Parameters.AddWithValue("@_Porsiyon", porsiyonu);
                    cmd.Parameters.AddWithValue("@_Tur", turBool);

                    SqlDataReader dr = cmd.ExecuteReader();

                    int siparisID, adisyonID = 0, adet;
                    DateTime verilisTarihi;
                    bool NotificationGorulduMu;

                    while (dr.Read())
                    {
                        try
                        {
                            siparisID = dr.GetInt32(0);
                            adisyonID = dr.GetInt32(1);
                            adet = dr.GetInt32(2);
                            verilisTarihi = dr.GetDateTime(3);
                            NotificationGorulduMu = dr.GetBoolean(4);
                        }
                        catch
                        {
                            KontrolFormu dialog = new KontrolFormu("İkramı iptal ederken bir hata oluştu, lütfen tekrar deneyiniz", false);
                            dialog.Show();
                            return;
                        }

                        if (adet < istenilenIkramiptalSayisi) // elimizdeki ikramlar iptali istenenden küçükse
                        {
                            ikramUpdateTam(siparisID, 0);

                            istenilenIkramiptalSayisi -= adet;
                        }
                        else if (adet > istenilenIkramiptalSayisi) // den büyükse
                        {
                            ikramUpdateInsert(siparisID, adisyonID, adet, dusulecekDeger, istenilenIkramiptalSayisi, listUrunFiyat.SelectedItems[0].SubItems[2].Text, 0, verilisTarihi, porsiyonu, turBool, NotificationGorulduMu);

                            istenilenIkramiptalSayisi = 0;
                        }
                        else // elimizde ikram edilmemişler ikramı istenene eşitse
                        {
                            ikramUpdateTam(siparisID, 0);

                            istenilenIkramiptalSayisi = 0;
                        }
                    }

                    if (istenilenIkramiptalSayisi != 0)// ikram edilecekler daha bitmedi başka garsonların siparişlerinden ikram iptaline devam et
                    {
                        cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adet,Siparis.VerilisTarihi,Adisyon.AdisyonID,NotificationGorulduMu FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi=0 AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + MasaAdi + "' AND Adisyon.DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND Siparis.YemekAdi='" + listUrunFiyat.SelectedItems[0].SubItems[2].Text + "' AND Siparis.Garsonu!='" + siparisiKimGirdi + "' AND Siparis.Porsiyon=CONVERT(DECIMAL(5,2),@_Porsiyon) AND Siparis.KiloSatisiMi=@_Tur ORDER BY Adet DESC");

                        cmd.Parameters.AddWithValue("@_Porsiyon", porsiyonu);
                        cmd.Parameters.AddWithValue("@_Tur", turBool);

                        dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            try
                            {
                                siparisID = dr.GetInt32(0);
                                adet = dr.GetInt32(1);
                                verilisTarihi = dr.GetDateTime(2);
                                adisyonID = dr.GetInt32(3);
                                NotificationGorulduMu = dr.GetBoolean(4);
                            }
                            catch
                            {
                                KontrolFormu dialog = new KontrolFormu("İkramı iptal ederken bir hata oluştu, lütfen tekrar deneyiniz", false);
                                dialog.Show();
                                return;
                            }

                            if (adet < istenilenIkramiptalSayisi) // elimizde ikram edilmemişler ikramı istenenden küçükse
                            {
                                ikramUpdateTam(siparisID, 0);

                                istenilenIkramiptalSayisi -= adet;
                            }
                            else if (adet > istenilenIkramiptalSayisi) // den büyükse
                            {
                                ikramUpdateInsert(siparisID, adisyonID, adet, dusulecekDeger, istenilenIkramiptalSayisi, listUrunFiyat.SelectedItems[0].SubItems[2].Text, 0, verilisTarihi, porsiyonu, turBool, NotificationGorulduMu);

                                istenilenIkramiptalSayisi = 0;
                            }
                            else // elimizde ikram edilmemişler ikramı istenene eşitse
                            {
                                ikramUpdateTam(siparisID, 0);

                                istenilenIkramiptalSayisi = 0;
                            }
                        }
                    }

                    adisyonNotuUpdate(adisyonID);

                    masaFormu.serverdanSiparisIkramVeyaIptal(MasaAdi, hangiDepartman.departmanAdi, "ikramIptal", kacAdet.ToString(), listUrunFiyat.SelectedItems[0].SubItems[2].Text, dusulecekDeger.ToString(), listUrunFiyat.SelectedItems[0].Group.Tag.ToString(), porsiyonu.ToString(), tur);

                    if (listUrunFiyat.SelectedItems[0].Text == "0")
                    {
                        listedeSeciliOlanItemlar.RemoveAt(listUrunFiyat.SelectedItems[0].Index);
                        listUrunFiyat.SelectedItems[0].Remove();
                    }

                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                }
                buttonTemizle_Click(null, null);
            }
            else //client 
            {
                // ikram et
                if (buttonUrunIkram.Text == "İkram")
                {
                    masaFormu.menuFormundanServeraSiparisYolla(MasaAdi, hangiDepartman.departmanAdi, "ikram", kacAdet.ToString(), listUrunFiyat.SelectedItems[0].SubItems[2].Text, siparisiKimGirdi, dusulecekDeger.ToString(), adisyonNotu, null, porsiyonu.ToString(),tur);
                }
                else // ikramı iptal et
                {
                    masaFormu.menuFormundanServeraSiparisYolla(MasaAdi, hangiDepartman.departmanAdi, "ikramIptal", kacAdet.ToString(), listUrunFiyat.SelectedItems[0].SubItems[2].Text, siparisiKimGirdi, dusulecekDeger.ToString(), adisyonNotu, listUrunFiyat.SelectedItems[0].Group.Tag.ToString(), porsiyonu.ToString(),tur);
                }
                this.Enabled = false;
            }
        }

        public void iptalGeldi(string miktar, string yemekAdi, string dusulecekDegerGelen, string ikramMiGelen, string porsiyon, bool kiloSatisiMi)
        {
            int ikraminGrubu = Convert.ToInt32(ikramMiGelen);

            int carpan = Convert.ToInt32(miktar);

            double dusulecekDeger = Convert.ToDouble(dusulecekDegerGelen);

            string tur = "P";

            if (kiloSatisiMi)
                tur = "K";

            for (int i = 0; i < listUrunFiyat.Groups[ikraminGrubu].Items.Count; i++)
            {
                if (listUrunFiyat.Groups[ikraminGrubu].Items[i].SubItems[2].Text == yemekAdi && Convert.ToDouble(listUrunFiyat.Groups[ikraminGrubu].Items[i].SubItems[1].Text.Substring(0, listUrunFiyat.Groups[ikraminGrubu].Items[i].SubItems[1].Text.Length - 1)) == Convert.ToDouble(porsiyon) && tur == listUrunFiyat.Groups[ikraminGrubu].Items[i].SubItems[1].Text[listUrunFiyat.Groups[ikraminGrubu].Items[i].SubItems[1].Text.Length - 1].ToString())
                {
                    listUrunFiyat.Groups[ikraminGrubu].Items[i].SubItems[0].Text = (Convert.ToDouble(listUrunFiyat.Groups[ikraminGrubu].Items[i].SubItems[0].Text) - carpan).ToString();

                    if (ikraminGrubu == 2) // iptali istenilen ürün ikram değilse kalan hesaptan da düşülmeli
                    {
                        labelKalanHesap.Text = (Convert.ToDouble(labelKalanHesap.Text) - (dusulecekDeger * carpan)).ToString("0.00");
                    }

                    if (listUrunFiyat.Groups[ikraminGrubu].Items[i].Text == "0")
                    {
                        listedeSeciliOlanItemlar.RemoveAt(listUrunFiyat.Groups[ikraminGrubu].Items[i].Index);
                        listUrunFiyat.Groups[ikraminGrubu].Items[i].Remove();
                    }

                    if (this.listUrunFiyat.Items.Count > 0)
                    {
                        int itemsCount = this.listUrunFiyat.Items.Count + 3; // 3 aslında grup sayısı -1
                        int itemHeight = this.listUrunFiyat.Items[0].Bounds.Height;
                        int VisiableItem = (int)this.listUrunFiyat.ClientRectangle.Height / itemHeight;
                        if (itemsCount < VisiableItem)
                        {
                            listUrunFiyat.Columns[2].Width = urunBoyu + 10;
                            listUrunFiyat.Columns[3].Width = fiyatBoyu + 10;
                        }
                    }
                    buttonTemizle_Click(null, null);
                    this.Enabled = true;
                    return;
                }
            }
            dialogTimer = new KontrolFormu("Siparişlerde değişiklik oldu, lütfen masaya tekrar giriniz", false, this);
            timerDialogClose.Start();
            this.Enabled = false;
            dialogTimer.Show();
            return;
        }

        // ürün iptal etme butonu
        private void buttonUrunIptal_Click(object sender, EventArgs e)
        {
            int kacAdet = Convert.ToInt32(labelCokluAdet.Text);

            if (kacAdet > Convert.ToInt32(listUrunFiyat.SelectedItems[0].SubItems[0].Text))
                kacAdet = Convert.ToInt32(listUrunFiyat.SelectedItems[0].SubItems[0].Text);

            if (Convert.ToDecimal(listUrunFiyat.SelectedItems[0].SubItems[3].Text) * kacAdet > Convert.ToDecimal(labelKalanHesap.Text))
            {
                KontrolFormu dialog = new KontrolFormu("Ürün fiyatı kalan hesaptan büyük olduğu için ürün iptal edilemez", false);
                dialog.Show();
                return;
            }

            double porsiyonu = Convert.ToDouble(listUrunFiyat.SelectedItems[0].SubItems[1].Text.Substring(0, listUrunFiyat.SelectedItems[0].SubItems[1].Text.Length - 1));

            string iptalNedeni = "";

            if (listUrunFiyat.SelectedItems[0].Group == listUrunFiyat.Groups[yeniSiparisler])
            {
                DialogResult eminMisiniz;

                using (KontrolFormu dialog = new KontrolFormu(kacAdet + " adet " + listUrunFiyat.SelectedItems[0].SubItems[2].Text + " iptal edilecek. Emin misiniz?", true))
                {
                    eminMisiniz = dialog.ShowDialog();

                    if (eminMisiniz == DialogResult.No)
                        return;
                }
            }
            else
            {
                UrunIptalNedeniFormu urunIptalFormu = new UrunIptalNedeniFormu(kacAdet + " adet " + listUrunFiyat.SelectedItems[0].SubItems[2].Text + " iptal edilecek. Kısaca nedenini yazınız");

                DialogResult eminMisiniz = urunIptalFormu.ShowDialog();

                if (eminMisiniz == DialogResult.No)
                    return;

                iptalNedeni = urunIptalFormu.iptalNedeni;
            }

            string tur = listUrunFiyat.SelectedItems[0].SubItems[1].Text[listUrunFiyat.SelectedItems[0].SubItems[1].Text.Length - 1].ToString();

            bool turBool = false;

            if (tur == "K")
            {
                turBool = true;
            }

            double dusulecekDeger = Convert.ToDouble(listUrunFiyat.SelectedItems[0].SubItems[3].Text);

            if (Properties.Settings.Default.Server == 2) //server - diğer tüm clientlara söylemeli yaptığı ikram vs. neyse
            {
                listUrunFiyat.SelectedItems[0].SubItems[0].Text = (Convert.ToDouble(listUrunFiyat.SelectedItems[0].SubItems[0].Text) - kacAdet).ToString();

                if (listUrunFiyat.SelectedItems[0].Group == listUrunFiyat.Groups[eskiSiparisler] || listUrunFiyat.SelectedItems[0].Group == listUrunFiyat.Groups[yeniSiparisler])
                {
                    labelKalanHesap.Text = (Convert.ToDouble(labelKalanHesap.Text) - (dusulecekDeger * kacAdet)).ToString("0.00");
                }

                if (listUrunFiyat.SelectedItems[0].Group != listUrunFiyat.Groups[yeniSiparisler])
                {
                    int istenilenSiparisiptalSayisi = kacAdet;

                    SqlCommand cmd;

                    string ikramSQLGirdisi;

                    if (listUrunFiyat.SelectedItems[0].Group == listUrunFiyat.Groups[eskiSiparisler])
                    {
                        ikramSQLGirdisi = "0";
                    }
                    else
                    {
                        ikramSQLGirdisi = "1";
                    }

                    cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adisyon.AdisyonID,Adet,Siparis.VerilisTarihi FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi='" + ikramSQLGirdisi + "' AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + MasaAdi + "' AND Adisyon.DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND Siparis.YemekAdi='" + listUrunFiyat.SelectedItems[0].SubItems[2].Text + "' AND Siparis.Garsonu='" + siparisiKimGirdi + "' AND Siparis.Porsiyon=CONVERT(DECIMAL(5,2),@_Porsiyon) AND Siparis.KiloSatisiMi=@_Tur ORDER BY Adet DESC");

                    cmd.Parameters.AddWithValue("@_Porsiyon", porsiyonu);
                    cmd.Parameters.AddWithValue("@_Tur", turBool);

                    SqlDataReader dr = cmd.ExecuteReader();

                    int siparisID, adisyonID = 0, adet;
                    DateTime verilisTarihi;

                    while (dr.Read())
                    {
                        try
                        {
                            siparisID = dr.GetInt32(0);
                            adisyonID = dr.GetInt32(1);
                            adet = dr.GetInt32(2);
                            verilisTarihi = dr.GetDateTime(3);
                        }
                        catch
                        {
                            KontrolFormu dialog = new KontrolFormu("Ürünü iptal ederken bir hata oluştu, lütfen tekrar deneyiniz", false);
                            dialog.Show();
                            return;
                        }

                        if (adet < istenilenSiparisiptalSayisi) // elimizdeki siparişler iptali istenenden küçükse
                        {
                            iptalUpdateTam(siparisID, iptalNedeni);

                            istenilenSiparisiptalSayisi -= adet;
                        }
                        else if (adet > istenilenSiparisiptalSayisi) // den büyükse
                        {
                            iptalUpdateInsert(siparisID, adisyonID, adet, dusulecekDeger, istenilenSiparisiptalSayisi, listUrunFiyat.SelectedItems[0].SubItems[2].Text, verilisTarihi, porsiyonu, iptalNedeni, turBool);

                            istenilenSiparisiptalSayisi = 0;
                        }
                        else // elimizdeki siparişler iptali istenene eşitse
                        {
                            iptalUpdateTam(siparisID, iptalNedeni);

                            istenilenSiparisiptalSayisi = 0;
                        }
                    }

                    if (istenilenSiparisiptalSayisi != 0)// iptal edilecekler daha bitmedi başka garsonların siparişlerinden iptale devam et
                    {
                        cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adet,Siparis.VerilisTarihi,Adisyon.AdisyonID FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi='" + ikramSQLGirdisi + "' AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + MasaAdi + "' AND Adisyon.DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND Siparis.YemekAdi='" + listUrunFiyat.SelectedItems[0].SubItems[2].Text + "' AND Siparis.Garsonu!='" + siparisiKimGirdi + "' AND Siparis.Porsiyon=CONVERT(DECIMAL(5,2),@_Porsiyon) AND Siparis.KiloSatisiMi=@_Tur ORDER BY Adet DESC");

                        cmd.Parameters.AddWithValue("@_Porsiyon", porsiyonu);
                        cmd.Parameters.AddWithValue("@_Tur", turBool);

                        dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            try
                            {
                                siparisID = dr.GetInt32(0);
                                adet = dr.GetInt32(1);
                                verilisTarihi = dr.GetDateTime(2);
                                adisyonID = dr.GetInt32(3);
                            }
                            catch
                            {
                                KontrolFormu dialog = new KontrolFormu("Ürünü iptal ederken bir hata oluştu, lütfen tekrar deneyiniz", false);
                                dialog.Show();
                                return;
                            }

                            if (adet < istenilenSiparisiptalSayisi) // elimizdeki siparişler iptali istenenden küçükse
                            {
                                iptalUpdateTam(siparisID, iptalNedeni);

                                istenilenSiparisiptalSayisi -= adet;
                            }
                            else if (adet > istenilenSiparisiptalSayisi) // den büyükse
                            {
                                iptalUpdateInsert(siparisID, adisyonID, adet, dusulecekDeger, istenilenSiparisiptalSayisi, listUrunFiyat.SelectedItems[0].SubItems[2].Text, verilisTarihi, porsiyonu, iptalNedeni, turBool);

                                istenilenSiparisiptalSayisi = 0;
                            }
                            else // elimizdeki siparişler iptali istenene eşitse
                            {
                                iptalUpdateTam(siparisID, iptalNedeni);

                                istenilenSiparisiptalSayisi = 0;
                            }
                        }
                    }

                    adisyonNotuUpdate(adisyonID);

                    cmd.Connection.Close();
                    cmd.Connection.Dispose();

                    masaFormu.serverdanSiparisIkramVeyaIptal(MasaAdi, hangiDepartman.departmanAdi, "iptal", kacAdet.ToString(), listUrunFiyat.SelectedItems[0].SubItems[2].Text, dusulecekDeger.ToString(), listUrunFiyat.SelectedItems[0].Group.Tag.ToString(), porsiyonu.ToString(), tur);
                }

                if (listUrunFiyat.SelectedItems[0].Text == "0")
                {
                    listedeSeciliOlanItemlar.RemoveAt(listUrunFiyat.SelectedItems[0].Index);
                    listUrunFiyat.SelectedItems[0].Remove();
                    buttonTemizle_Click(null, null);
                }

                if (this.listUrunFiyat.Items.Count > 0)
                {
                    int itemsCount = this.listUrunFiyat.Items.Count + 3;// 3 aslında grup sayısı -1
                    int itemHeight = this.listUrunFiyat.Items[0].Bounds.Height;
                    int VisiableItem = (int)this.listUrunFiyat.ClientRectangle.Height / itemHeight;
                    if (itemsCount < VisiableItem)
                    {
                        listUrunFiyat.Columns[2].Width = urunBoyu + 10;
                        listUrunFiyat.Columns[3].Width = fiyatBoyu + 10;
                    }
                }
            }
            else //client 
            {
                if (listUrunFiyat.SelectedItems[0].Group == listUrunFiyat.Groups[yeniSiparisler]) //eğer sipariş yeni verilenlerdense henüz sisteme girişi yapılmamış demektir
                {
                    listUrunFiyat.SelectedItems[0].SubItems[0].Text = (Convert.ToDouble(listUrunFiyat.SelectedItems[0].SubItems[0].Text) - kacAdet).ToString();

                    if (listUrunFiyat.SelectedItems[0].Group == listUrunFiyat.Groups[2] || listUrunFiyat.SelectedItems[0].Group == listUrunFiyat.Groups[3])
                    {
                        labelKalanHesap.Text = (Convert.ToDouble(labelKalanHesap.Text) - (dusulecekDeger * kacAdet)).ToString("0.00");
                    }

                    if (listUrunFiyat.SelectedItems[0].Text == "0")
                    {
                        listedeSeciliOlanItemlar.RemoveAt(listUrunFiyat.SelectedItems[0].Index);
                        listUrunFiyat.SelectedItems[0].Remove();
                    }

                    if (this.listUrunFiyat.Items.Count > 0)
                    {
                        int itemsCount = this.listUrunFiyat.Items.Count + 3;// 3 aslında grup sayısı -1
                        int itemHeight = this.listUrunFiyat.Items[0].Bounds.Height;
                        int VisiableItem = (int)this.listUrunFiyat.ClientRectangle.Height / itemHeight;
                        if (itemsCount < VisiableItem)
                        {
                            listUrunFiyat.Columns[2].Width = urunBoyu + 10;
                            listUrunFiyat.Columns[3].Width = fiyatBoyu + 10;
                        }
                    }

                    buttonTemizle_Click(null, null);
                }
                else // yeni sipariş değilse sisteme girişi yapılmıştır ve diğer makinalara bilgi verilmelidir
                {
                    string ikramMi = listUrunFiyat.SelectedItems[0].Group.Tag.ToString();

                    masaFormu.menuFormundanServeraSiparisYolla(MasaAdi, hangiDepartman.departmanAdi, "iptal", kacAdet.ToString(), listUrunFiyat.SelectedItems[0].SubItems[2].Text, siparisiKimGirdi, dusulecekDeger.ToString(), adisyonNotu, ikramMi, porsiyonu.ToString(), tur, iptalNedeni);
                    this.Enabled = false;
                }
            }
        }

        // sipariş işlemleri bittiğinde basılan buton
        private void buttonTamam_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Server == 2) //server - diğer tüm clientlara söylemeli yaptığı ikram vs. neyse
            {
                SqlCommand cmd = SQLBaglantisi.getCommand("SELECT OdemeYapiliyor,HesapIstendi FROM Adisyon WHERE AcikMi=1 AND IptalMi=0 AND MasaAdi=@_MasaAdi AND DepartmanAdi=@_DepartmanAdi");
                cmd.Parameters.AddWithValue("@_MasaAdi", MasaAdi);
                cmd.Parameters.AddWithValue("@_DepartmanAdi", hangiDepartman.departmanAdi);

                SqlDataReader dr = cmd.ExecuteReader();

                dr.Read();
                try
                {
                    if (dr.GetBoolean(0))
                    {
                        this.Close();
                    }
                    else if (dr.GetBoolean(1))
                    {
                        this.Close();
                    }
                }
                catch
                { }

                cmd.Connection.Close();
                cmd.Connection.Dispose();

                if (listUrunFiyat.Groups[2].Items.Count == 0 && listUrunFiyat.Groups[3].Items.Count == 0) // listede hiç sipariş yoksa, siparişler ya ödenmiştir yada iptal edilmiştir
                {
                    cmd = SQLBaglantisi.getCommand("SELECT OdenenMiktar from OdemeDetay JOIN Adisyon ON OdemeDetay.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.MasaAdi='" + MasaAdi + "' AND Adisyon.DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND Adisyon.AcikMi=1 AND Adisyon.IptalMi=0");
                    dr = cmd.ExecuteReader();

                    try // eğer masanın ödenmiş siparişi varsa hesabı kapat 
                    {
                        decimal odenenmiktar = 0;

                        while (dr.Read()) // ödenen miktarları topluyoruz
                        {
                            odenenmiktar += dr.GetDecimal(0);
                        }

                        cmd = SQLBaglantisi.getCommand("SELECT COUNT(Adet) from Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.MasaAdi='" + MasaAdi + "' AND Adisyon.DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IptalMi=0 AND Siparis.IkramMi=1");
                        dr = cmd.ExecuteReader();

                        dr.Read();

                        int ikramSayisi = 0;

                        try
                        {
                            ikramSayisi = dr.GetInt32(0);
                        }
                        catch
                        { }

                        if (odenenmiktar != 0 || ikramSayisi > 0) // eğer sıfırdan farklı ise adisyonu kapatıyoruz
                        {
                            cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET OdendiMi=1 WHERE Siparis.IptalMi=0 AND AdisyonID=(SELECT AdisyonID FROM Adisyon WHERE AcikMi=1 AND MasaAdi='" + MasaAdi + "' AND DepartmanAdi='" + hangiDepartman.departmanAdi + "')");
                            cmd.ExecuteNonQuery();
                            cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET AcikMi=0,KapanisZamani=@date WHERE AdisyonID=(SELECT AdisyonID FROM Adisyon WHERE AcikMi=1 AND MasaAdi='" + MasaAdi + "' AND DepartmanAdi='" + hangiDepartman.departmanAdi + "')");
                            cmd.Parameters.AddWithValue("@date", DateTime.Now);
                        }
                        else // değilse adisyonu iptal ediyoruz
                        {
                            cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET AcikMi=0, IptalMi=1, KapanisZamani=@date WHERE AdisyonID=(SELECT AdisyonID FROM Adisyon WHERE AcikMi=1 AND MasaAdi='" + MasaAdi + "' AND DepartmanAdi='" + hangiDepartman.departmanAdi + "')");
                            cmd.Parameters.AddWithValue("@date", DateTime.Now);
                        }
                    }
                    catch // eğer masanın ödenmiş siparişi yoksa iptal 
                    {
                        cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET AcikMi=0, IptalMi=1, KapanisZamani=@date WHERE AdisyonID=(SELECT AdisyonID FROM Adisyon WHERE AcikMi=1 AND MasaAdi='" + MasaAdi + "' AND DepartmanAdi='" + hangiDepartman.departmanAdi + "')");
                        cmd.Parameters.AddWithValue("@date", DateTime.Now);
                    }

                    try //adisyonID alınabilirse adisyon var demektir, ancak sipariş yok - o zaman adisyon kapatılır
                    {
                        cmd.ExecuteNonQuery();
                        cmd.Connection.Close();
                        cmd.Connection.Dispose();
                    }
                    catch (Exception) // masaya ait adisyon yok, çık
                    {
                        cmd.Connection.Dispose();
                        this.Close();
                        return;
                    }

                    if (masaDegisti != 2)
                        masaAcikMi = false;

                    this.Close();
                }
                else
                {
                    int adisyonID;

                    cmd = SQLBaglantisi.getCommand("SELECT AdisyonID FROM Adisyon WHERE MasaAdi='" + MasaAdi + "' AND DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND AcikMi=1");
                    dr = cmd.ExecuteReader();
                    dr.Read();

                    try // açık
                    {
                        adisyonID = dr.GetInt32(0);
                        masaAcikMi = true;
                        cmd.Connection.Close();
                        cmd.Connection.Dispose();
                    }
                    catch// kapalı
                    {
                        cmd.Connection.Close();
                        cmd.Connection.Dispose();
                        adisyonID = adisyonOlustur();
                    }

                    foreach (ListViewItem siparis in listUrunFiyat.Groups[yeniSiparisler].Items)
                    {
                        siparisOlustur(adisyonID, siparis);

                        masaFormu.serverdanSiparisIkramVeyaIptal(MasaAdi, hangiDepartman.departmanAdi, "siparis", siparis.SubItems[0].Text, siparis.SubItems[2].Text, Convert.ToDecimal(siparis.SubItems[3].Text).ToString(), null, siparis.SubItems[1].Text.Substring(0, siparis.SubItems[1].Text.Length - 1), siparis.SubItems[1].Text[siparis.SubItems[1].Text.Length - 1].ToString());
                    }

                    //yeni ürünler için mutfak bildirimi
                    cmd = SQLBaglantisi.getCommand("SELECT MutfakCiktisiAlindiMi FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND MutfakCiktisiAlindiMi=0 AND Siparis.IptalMi=0 AND Siparis.IkramMi=0 AND Siparis.OdendiMi=0 AND MasaAdi=@_MasaAdi AND DepartmanAdi=@_DepartmanAdi");
                    cmd.Parameters.AddWithValue("@_MasaAdi", MasaAdi);
                    cmd.Parameters.AddWithValue("@_DepartmanAdi", hangiDepartman.departmanAdi);
                    dr = cmd.ExecuteReader();

                    KontrolFormu dialog2 = null;

                    try
                    {
                        dr.Read();

                        dr.GetBoolean(0);

                        // mutfak adisyonu iste
                        cmd = SQLBaglantisi.getCommand("SELECT FirmaAdi, Yazici FROM Yazici WHERE YaziciAdi LIKE 'Mutfak%'");

                        dr = cmd.ExecuteReader();

                        string firmaAdi = "", yaziciAdi = "";

                        while (dr.Read())
                        {
                            firmaAdi = dr.GetString(0);
                            yaziciAdi = dr.GetString(1);
                        }

                        cmd.Connection.Close();
                        cmd.Connection.Dispose();

                        if (yaziciAdi != "")
                            asyncYaziciyaGonder(MasaAdi, hangiDepartman.departmanAdi, firmaAdi, yaziciAdi, raporMutfakNormal);
                        else
                        {
                            dialog2 = new KontrolFormu("Mutfak yazıcısı bulunamadı", false);
                            dialog2.Show();
                        }
                    }
                    catch
                    {
                        cmd.Connection.Close();
                        cmd.Connection.Dispose();
                    }

                    // iptal edilen ürünler için mutfağa adisyon
                    cmd = SQLBaglantisi.getCommand("SELECT MutfakCiktisiAlindiMi FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND MutfakCiktisiAlindiMi=0 AND Siparis.IptalMi=1 AND Siparis.IkramMi=0 AND Siparis.OdendiMi=0 AND MasaAdi=@_MasaAdi AND DepartmanAdi=@_DepartmanAdi");
                    cmd.Parameters.AddWithValue("@_MasaAdi", MasaAdi);
                    cmd.Parameters.AddWithValue("@_DepartmanAdi", hangiDepartman.departmanAdi);
                    dr = cmd.ExecuteReader();

                    try
                    {
                        dr.Read();

                        dr.GetBoolean(0);

                        // mutfak adisyonu iste 
                        cmd = SQLBaglantisi.getCommand("SELECT FirmaAdi,Yazici FROM Yazici WHERE YaziciAdi LIKE 'Mutfak%'");
                        dr = cmd.ExecuteReader();

                        string firmaAdi = "", yaziciAdi = "";

                        while (dr.Read())
                        {
                            firmaAdi = dr.GetString(0);
                            yaziciAdi = dr.GetString(1);
                        }

                        cmd.Connection.Close();
                        cmd.Connection.Dispose();

                        if (yaziciAdi != "")
                            asyncYaziciyaGonder(MasaAdi, hangiDepartman.departmanAdi, firmaAdi, yaziciAdi, raporMutfakIptal);
                        else
                        {
                            if (dialog2 == null)
                            {
                                dialog2 = new KontrolFormu("Mutfak yazıcısı bulunamadı", false);
                                dialog2.Show();
                            }
                        }
                    }
                    catch
                    {
                        cmd.Connection.Close();
                        cmd.Connection.Dispose();
                    }

                    if (adisyonNotuGuncellenmeliMi) // eğer sipariş notuna dokunulmuşsa not update edilsin
                    {
                        adisyonNotuUpdate(adisyonID);
                    }
                    this.Close();
                }
            }
            else //client
            {
                if (listUrunFiyat.Items.Count == 0)
                {
                    masaFormu.siparisListesiBos(MasaAdi, hangiDepartman.departmanAdi, "listeBos");
                    masaAcikMi = false;
                    this.Close();
                }
                else
                {
                    // eğer son siparişse mutfak adisyonu yazdırılmalı
                    int sonSiparisMi = listUrunFiyat.Groups[yeniSiparisler].Items.Count;

                    foreach (ListViewItem siparis in listUrunFiyat.Groups[yeniSiparisler].Items)
                    {
                        sonSiparisMi--;
                        masaFormu.serveraSiparis(MasaAdi, hangiDepartman.departmanAdi, "siparis", siparis.SubItems[0].Text, siparis.SubItems[2].Text, siparisiKimGirdi, Convert.ToDecimal(siparis.SubItems[3].Text).ToString(), adisyonNotu, sonSiparisMi, siparis.SubItems[1].Text.Substring(0, siparis.SubItems[1].Text.Length - 1),siparis.SubItems[1].Text[siparis.SubItems[1].Text.Length - 1].ToString());
                        adisyonNotuGuncellenmeliMi = false;
                    }

                    if (adisyonNotuGuncellenmeliMi)
                    {
                        masaFormu.serveraNotuYolla(MasaAdi, hangiDepartman.departmanAdi, "adisyonNotunuGuncelle", adisyonNotu);
                    }
                    masaAcikMi = true;
                    this.Close();
                }
            }
        }

        // mutfak adisyonu
        public Thread asyncYaziciyaGonder(string masaAdi, string departmanAdi, string firmaAdi, string printerAdi, CrystalReportMutfak rapor)
        {
            var t = new Thread(() => Basla(masaAdi, departmanAdi, firmaAdi, printerAdi, rapor));
            t.Start();
            return t;
        }

        private static void Basla(string masaAdi, string departmanAdi, string firmaAdi, string printerAdi, CrystalReportMutfak rapor)
        {
            rapor.Refresh();

            rapor.SetParameterValue("Masa", masaAdi);
            rapor.SetParameterValue("Departman", departmanAdi);
            rapor.SetParameterValue("FirmaAdi", firmaAdi); // firma adı
            try
            {
                rapor.PrintOptions.PrinterName = printerAdi;
                rapor.PrintToPrinter(1, false, 0, 0);
            }
            catch
            {
                KontrolFormu dialog = new KontrolFormu("Yazıcı bulunamadı\nLütfen ayarlarınızı kontrol edin", false);
                dialog.Show();
                return;
            }

            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET MutfakCiktisiAlindiMi=1 WHERE AdisyonID=(SELECT AdisyonID FROM Adisyon WHERE IptalMi=0 AND AcikMi=1 AND MasaAdi=@masaninAdi AND DepartmanAdi=@departmanAdi) AND Siparis.IptalMi=0");
            cmd.Parameters.AddWithValue("@masaninAdi", masaAdi);
            cmd.Parameters.AddWithValue("@departmanAdi", departmanAdi);

            cmd.ExecuteNonQuery();

            cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET AdisyonNotu=@_AdisyonNotu WHERE AcikMi=1 AND MasaAdi=@masaninAdi AND DepartmanAdi=@departmanAdi");
            cmd.Parameters.AddWithValue("@_AdisyonNotu", "");
            cmd.Parameters.AddWithValue("@masaninAdi", masaAdi);
            cmd.Parameters.AddWithValue("@departmanAdi", departmanAdi);

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        // mutfak ürün iptal adisyonu
        public Thread asyncYaziciyaGonder(string masaAdi, string departmanAdi, string firmaAdi, string printerAdi, CrystalReportMutfakUrunIptal rapor)
        {
            var t = new Thread(() => Basla(masaAdi, departmanAdi, firmaAdi, printerAdi, rapor));
            t.Start();
            return t;
        }

        private static void Basla(string masaAdi, string departmanAdi, string firmaAdi, string printerAdi, CrystalReportMutfakUrunIptal rapor)
        {
            rapor.Refresh();

            rapor.SetParameterValue("Masa", masaAdi);
            rapor.SetParameterValue("Departman", departmanAdi);
            rapor.SetParameterValue("FirmaAdi", firmaAdi); // firma adı
            try
            {
                rapor.PrintOptions.PrinterName = printerAdi;
                rapor.PrintToPrinter(1, false, 0, 0);
            }
            catch
            {
                KontrolFormu dialog = new KontrolFormu("Yazıcı bulunamadı\nLütfen ayarlarınızı kontrol edin", false);
                dialog.Show();
                return;
            }

            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET MutfakCiktisiAlindiMi=1 WHERE AdisyonID=(SELECT AdisyonID FROM Adisyon WHERE IptalMi=0 AND AcikMi=1 AND MasaAdi=@masaninAdi AND DepartmanAdi=@departmanAdi) AND Siparis.IptalMi=1");
            cmd.Parameters.AddWithValue("@masaninAdi", masaAdi);
            cmd.Parameters.AddWithValue("@departmanAdi", departmanAdi);

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        private void masaAktarmaIslemlerindenSonraCik(string yeniMasaAdi, string yeniDepartmanAdi)
        {
            if (!adisyonNotuGuncellenmeliMi && listUrunFiyat.Groups[3].Items.Count == 0)
                return;

            if (Properties.Settings.Default.Server == 2) //server - diğer tüm clientlara söylemeli yaptığı ikram vs. neyse
            {
                SqlCommand cmd;

                int adisyonID;

                cmd = SQLBaglantisi.getCommand("SELECT AdisyonID FROM Adisyon WHERE MasaAdi='" + yeniMasaAdi + "' AND DepartmanAdi='" + yeniDepartmanAdi + "' AND AcikMi=1");
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();

                adisyonID = dr.GetInt32(0);

                cmd.Connection.Close();
                cmd.Connection.Dispose();

                foreach (ListViewItem siparis in listUrunFiyat.Groups[yeniSiparisler].Items)
                {
                    siparisOlustur(adisyonID, siparis);

                    masaFormu.serverdanSiparisIkramVeyaIptal(yeniMasaAdi, yeniDepartmanAdi, "siparis", siparis.SubItems[0].Text, siparis.SubItems[2].Text, Convert.ToDecimal(siparis.SubItems[3].Text).ToString(), null, siparis.SubItems[1].Text.Substring(0, siparis.SubItems[1].Text.Length - 1),siparis.SubItems[1].Text[siparis.SubItems[1].Text.Length - 1].ToString());
                }

                //yeni ürünler için mutfak bildirimi
                cmd = SQLBaglantisi.getCommand("SELECT MutfakCiktisiAlindiMi FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND MutfakCiktisiAlindiMi=0 AND Siparis.IptalMi=0 AND Siparis.IkramMi=0 AND Siparis.OdendiMi=0 AND MasaAdi=@_MasaAdi AND DepartmanAdi=@_DepartmanAdi");
                cmd.Parameters.AddWithValue("@_MasaAdi", yeniMasaAdi);
                cmd.Parameters.AddWithValue("@_DepartmanAdi", yeniDepartmanAdi);
                dr = cmd.ExecuteReader();

                KontrolFormu dialog2 = null;

                try
                {
                    dr.Read();

                    dr.GetBoolean(0);

                    // mutfak adisyonu iste
                    cmd = SQLBaglantisi.getCommand("SELECT FirmaAdi, Yazici FROM Yazici WHERE YaziciAdi LIKE 'Mutfak%'");

                    dr = cmd.ExecuteReader();

                    string firmaAdi = "", yaziciAdi = "";

                    while (dr.Read())
                    {
                        firmaAdi = dr.GetString(0);
                        yaziciAdi = dr.GetString(1);
                    }

                    cmd.Connection.Close();
                    cmd.Connection.Dispose();

                    if (yaziciAdi != "")
                        asyncYaziciyaGonder(yeniMasaAdi, yeniDepartmanAdi, firmaAdi, yaziciAdi, raporMutfakNormal);
                    else
                    {
                        dialog2 = new KontrolFormu("Mutfak yazıcısı bulunamadı", false);
                        dialog2.Show();
                    }
                }
                catch
                {
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                }

                // iptal edilen ürünler için mutfağa adisyon
                cmd = SQLBaglantisi.getCommand("SELECT MutfakCiktisiAlindiMi FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND MutfakCiktisiAlindiMi=0 AND Siparis.IptalMi=1 AND Siparis.IkramMi=0 AND Siparis.OdendiMi=0 AND MasaAdi=@_MasaAdi AND DepartmanAdi=@_DepartmanAdi");
                cmd.Parameters.AddWithValue("@_MasaAdi", yeniMasaAdi);
                cmd.Parameters.AddWithValue("@_DepartmanAdi", yeniDepartmanAdi);
                dr = cmd.ExecuteReader();

                try
                {
                    dr.Read();

                    dr.GetBoolean(0);

                    // mutfak adisyonu iste 
                    cmd = SQLBaglantisi.getCommand("SELECT FirmaAdi,Yazici FROM Yazici WHERE YaziciAdi LIKE 'Mutfak%'");
                    dr = cmd.ExecuteReader();

                    string firmaAdi = "", yaziciAdi = "";

                    while (dr.Read())
                    {
                        firmaAdi = dr.GetString(0);
                        yaziciAdi = dr.GetString(1);
                    }

                    cmd.Connection.Close();
                    cmd.Connection.Dispose();

                    if (yaziciAdi != "")
                        asyncYaziciyaGonder(yeniMasaAdi, yeniDepartmanAdi, firmaAdi, yaziciAdi, raporMutfakIptal);
                    else
                    {
                        if (dialog2 == null)
                        {
                            dialog2 = new KontrolFormu("Mutfak yazıcısı bulunamadı", false);
                            dialog2.Show();
                        }
                    }
                }
                catch
                {
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                }

                if (adisyonNotuGuncellenmeliMi) // eğer sipariş notuna dokunulmuşsa not update edilsin
                {
                    adisyonNotuUpdate(adisyonID);
                }
            }
            else //client
            {
                if (listUrunFiyat.Groups[3].Items.Count > 0)
                {
                    int sonSiparisMi = listUrunFiyat.Groups[3].Items.Count;

                    foreach (ListViewItem siparis in listUrunFiyat.Groups[yeniSiparisler].Items)
                    {
                        sonSiparisMi--;

                        masaFormu.serveraSiparis(yeniMasaAdi, yeniDepartmanAdi, "siparis", siparis.SubItems[0].Text, siparis.SubItems[2].Text, siparisiKimGirdi, Convert.ToDecimal(siparis.SubItems[3].Text).ToString(), adisyonNotu, sonSiparisMi, siparis.SubItems[1].Text.Substring(0, siparis.SubItems[1].Text.Length - 1),siparis.SubItems[1].Text[siparis.SubItems[1].Text.Length - 1].ToString());
                        adisyonNotuGuncellenmeliMi = false;
                    }
                }

                if (adisyonNotuGuncellenmeliMi)
                {
                    masaFormu.serveraNotuYolla(yeniMasaAdi, yeniDepartmanAdi, "adisyonNotunuGuncelle", adisyonNotu);
                }
            }
        }

        //her yeni gelen sipariş için çalışan kısım
        public void siparisOnayiGeldi(string miktar, string yemekAdi, string fiyatGelen, string porsiyonu, bool kiloSatisiMi, string ilkSiparisMi = "")
        {
            if (ilkSiparisMi != "") // eğer hesap ödeme formuna geçerken gelen siparişler ise listeye ekleme çünkü onları zaten açılışta ekliyoruz
            {
                return;
            }

            string tur = "P";

            if(kiloSatisiMi)
            {
                tur = "K";
            }

            int gruptaYeniGelenSiparisVarmi = siparisGruptaVarMi(eskiSiparisler, yemekAdi, Convert.ToDouble(porsiyonu).ToString() + tur); //ürün cinsi hesapta var mı bak           
            int kacAdet = Convert.ToInt32(miktar);

            if (gruptaYeniGelenSiparisVarmi == -1) //yoksa ürünü hesaba ekle
            {
                listUrunFiyat.Items.Add(miktar);
                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(porsiyonu + tur);
                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(yemekAdi);
                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add((Convert.ToDouble(fiyatGelen)).ToString("0.00"));

                string[] porsiyonSinifiTuruKiloVeyaPorsiyonFiyati;

                if (tur == "P")
                    porsiyonSinifiTuruKiloVeyaPorsiyonFiyati = porsiyonSinifiTuruPorsiyonFiyatiBul(yemekAdi);
                else
                    porsiyonSinifiTuruKiloVeyaPorsiyonFiyati = porsiyonSinifiTuruKiloFiyatiBul(yemekAdi);

                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems[1].Tag = porsiyonSinifiTuruKiloVeyaPorsiyonFiyati[0]; // sınıfı
                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems[2].Tag = porsiyonSinifiTuruKiloVeyaPorsiyonFiyati[1]; // türü 
                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems[3].Tag = porsiyonSinifiTuruKiloVeyaPorsiyonFiyati[2]; // kilo fiyatı  

                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Group = listUrunFiyat.Groups[eskiSiparisler];
                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font = new Font("Calibri", 18.75F, FontStyle.Bold);
                listedeSeciliOlanItemlar.Add(false);

                gorunumuDuzenle(yemekAdi, fiyatGelen, kacAdet.ToString(), porsiyonu.ToString());
            }
            else // varsa ürünün hesaptaki değerlerini istenilene göre arttır
            {
                listUrunFiyat.Groups[eskiSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text = (Convert.ToDouble(listUrunFiyat.Groups[eskiSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text) + (double)kacAdet).ToString();
            }
            labelKalanHesap.Text = (Convert.ToDecimal(labelKalanHesap.Text) + kacAdet * Convert.ToDecimal(fiyatGelen)).ToString("0.00");
        }

        //Masaların adisyonlarını değiştiren butonun methodu
        private void changeTablesButton_Click(object sender, EventArgs e)
        {
            masaDegistirForm = new MasaDegistirFormu(MasaAdi, hangiDepartman.departmanAdi, this);
            masaDegistirForm.ShowDialog();

            if (masaDegistirForm.yeniMasa == "iptalEdildi")
                return;
            else
            {
                if (Properties.Settings.Default.Server == 2) // server
                {
                    SqlCommand cmd;
                    switch (masaDegistirForm.yapilmasiGerekenIslem)
                    {
                        case 0: // departman değişmedi ve masaların ikisi de açık
                            cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET MasaAdi = CASE MasaAdi WHEN @masaninAdiEski THEN @masaninAdiYeni WHEN @masaninAdiYeni THEN @masaninAdiEski END WHERE MasaAdi in (@masaninAdiEski,@masaninAdiYeni) AND AcikMi=1 AND DepartmanAdi=@departmanAdiEski");

                            cmd.Parameters.AddWithValue("@masaninAdiEski", MasaAdi);
                            cmd.Parameters.AddWithValue("@masaninAdiYeni", masaDegistirForm.yeniMasa);
                            cmd.Parameters.AddWithValue("@departmanAdiEski", hangiDepartman.departmanAdi);
                            break;
                        case 1: // masalar açık departman değişti
                            cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET MasaAdi = CASE MasaAdi WHEN @masaninAdiEski THEN @masaninAdiYeni WHEN @masaninAdiYeni THEN @masaninAdiEski END, DepartmanAdi = CASE DepartmanAdi WHEN @departmanAdiEski THEN @departmanAdiYeni WHEN @departmanAdiYeni THEN @departmanAdiEski END WHERE MasaAdi in (@masaninAdiEski,@masaninAdiYeni) AND AcikMi=1 AND DepartmanAdi in (@departmanAdiEski,@departmanAdiYeni)");

                            cmd.Parameters.AddWithValue("@masaninAdiEski", MasaAdi);
                            cmd.Parameters.AddWithValue("@masaninAdiYeni", masaDegistirForm.yeniMasa);
                            cmd.Parameters.AddWithValue("@departmanAdiEski", hangiDepartman.departmanAdi);
                            cmd.Parameters.AddWithValue("@departmanAdiYeni", masaDegistirForm.yeniDepartman);
                            break;
                        case 2: // departman değişmedi 1 masa açık
                            cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET MasaAdi=@masaninAdi WHERE MasaAdi='" + MasaAdi + "' AND DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND AcikMi=1");
                            cmd.Parameters.AddWithValue("@masaninAdi", masaDegistirForm.yeniMasa);
                            break;
                        case 3: // departman değişti 1 masa açık
                            cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET MasaAdi=@masaninAdi, DepartmanAdi=@departmanAdi  WHERE MasaAdi='" + MasaAdi + "' AND DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND AcikMi=1");
                            cmd.Parameters.AddWithValue("@masaninAdi", masaDegistirForm.yeniMasa);
                            cmd.Parameters.AddWithValue("@departmanAdi", masaDegistirForm.yeniDepartman);
                            break;
                        default:
                            cmd = null;
                            break;
                    }
                    cmd.ExecuteNonQuery();

                    cmd.Connection.Close();
                    cmd.Connection.Dispose();

                    yeniMasaninAdi = masaDegistirForm.yeniMasa;
                    yeniDepartmaninAdi = masaDegistirForm.yeniDepartman;
                    masaDegisti = masaDegistirForm.yapilmasiGerekenIslem;

                    masaFormu.serverdanMasaDegisikligi(MasaAdi, hangiDepartman.departmanAdi, masaDegistirForm.yeniMasa, masaDegistirForm.yeniDepartman, "masaDegistir");
                }
                else // client
                {
                    masaFormu.menuFormundanServeraMasaDegisikligi(masaDegistirForm.yeniMasa, masaDegistirForm.yeniDepartman, MasaAdi, hangiDepartman.departmanAdi, masaDegistirForm.yapilmasiGerekenIslem, "masaDegistir");

                    yeniMasaninAdi = masaDegistirForm.yeniMasa;
                    yeniDepartmaninAdi = masaDegistirForm.yeniDepartman;
                    masaDegisti = masaDegistirForm.yapilmasiGerekenIslem;
                }
                masaAktarmaIslemlerindenSonraCik(masaDegistirForm.yeniMasa, masaDegistirForm.yeniDepartman);

                if (masaDegisti == 2 || masaDegisti == 3)
                    listUrunFiyat.Items.Clear();

                masaDegistirForm = null;
                buttonTamam_Click(null, null);
            }
        }

        public void masaDegisikligiFormundanAcikMasaBilgisiIstegiGeldiMasaFormunaIlet(string mesaj)
        {
            masaFormu.masaDegisikligiFormundanAcikMasaBilgisiIstegi(mesaj);
        }

        //ödeme kısmına geçiş butonu
        private void paymentButton_Click(object sender, EventArgs e)
        {
            if (listUrunFiyat.Items.Count > 0)
            {
                //ödendiğinde sql de ödendi flagini 1 yap 
                hesapForm = new HesapFormu(this, listUrunFiyat, MasaAdi, hangiDepartman.departmanAdi, siparisiKimGirdi, iptalIkram, adisyonDegistirebilirMi, satisYapabilirMi);
                hesapForm.ShowDialog();
            }
        }

        #region SQL İşlemleri

        public int adisyonOlustur()
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("INSERT INTO Adisyon(AcikMi,AdisyonNotu,AcilisZamani,DepartmanAdi,MasaAdi) VALUES(@_acikMi,@_AdisyonNotu,@_AcilisZamani,@_DepartmanAdi,@_MasaAdi) SELECT SCOPE_IDENTITY()");

            cmd.Parameters.AddWithValue("@_acikmi", 1);
            cmd.Parameters.AddWithValue("@_AdisyonNotu", adisyonNotu);
            cmd.Parameters.AddWithValue("@_AcilisZamani", DateTime.Now);
            cmd.Parameters.AddWithValue("@_DepartmanAdi", hangiDepartman.departmanAdi);
            cmd.Parameters.AddWithValue("@_MasaAdi", MasaAdi);

            int adisyonID = Convert.ToInt32(cmd.ExecuteScalar());

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            masaAcikMi = true;
            buttonMasaDegistir.Enabled = true;

            return adisyonID;
        }

        public int bosAdisyonOlustur(string masaAdi, string departmanAdi)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("INSERT INTO Adisyon(AcikMi,AdisyonNotu,AcilisZamani,DepartmanAdi,MasaAdi) VALUES(@_acikMi,@_AdisyonNotu,@_AcilisZamani,@_DepartmanAdi,@_MasaAdi) SELECT SCOPE_IDENTITY()");

            cmd.Parameters.AddWithValue("@_acikmi", 1);
            cmd.Parameters.AddWithValue("@_AdisyonNotu", "");
            cmd.Parameters.AddWithValue("@_AcilisZamani", DateTime.Now);
            cmd.Parameters.AddWithValue("@_DepartmanAdi", departmanAdi);
            cmd.Parameters.AddWithValue("@_MasaAdi", masaAdi);
            int adisyonID = Convert.ToInt32(cmd.ExecuteScalar());

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            return adisyonID;
        }

        public void siparisOlustur(int adisyonID, ListViewItem siparis)
        {
            bool urunMutfagaBildirilmeliMi = mutfakBilgilendirilmeliMi(siparis.SubItems[2].Text);

            SqlCommand cmd = SQLBaglantisi.getCommand("INSERT INTO Siparis(AdisyonID,Garsonu,Fiyatı,Adet,YemekAdi,VerilisTarihi,MutfakCiktisiAlinmaliMi,Porsiyon,KiloSatisiMi,NotificationGorulduMu) VALUES(@_AdisyonID,@_Garsonu,@_Fiyatı,@_Adet,@_YemekAdi,@_VerilisTarihi,@_MutfakCiktisiAlinmaliMi,@_Porsiyon,@_KiloSatisiMi,@_NotificationGorulduMu)");
            cmd.Parameters.AddWithValue("@_AdisyonID", adisyonID);
            cmd.Parameters.AddWithValue("@_Garsonu", siparisiKimGirdi);
            cmd.Parameters.AddWithValue("@_Fiyatı", Convert.ToDecimal(siparis.SubItems[3].Text));
            cmd.Parameters.AddWithValue("@_Adet", Convert.ToDecimal(siparis.SubItems[0].Text));
            cmd.Parameters.AddWithValue("@_YemekAdi", siparis.SubItems[2].Text);
            cmd.Parameters.AddWithValue("@_VerilisTarihi", DateTime.Now);
            cmd.Parameters.AddWithValue("@_MutfakCiktisiAlinmaliMi", urunMutfagaBildirilmeliMi);
            cmd.Parameters.AddWithValue("@_Porsiyon", Convert.ToDecimal(siparis.SubItems[1].Text.Substring(0, siparis.SubItems[1].Text.Length - 1)));
            if (siparis.SubItems[1].Text[siparis.SubItems[1].Text.Length - 1] == 'P')
            {
                cmd.Parameters.AddWithValue("@_KiloSatisiMi", 0);
                cmd.Parameters.AddWithValue("@_NotificationGorulduMu", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@_KiloSatisiMi", 1);
                cmd.Parameters.AddWithValue("@_NotificationGorulduMu", 1);
            }
            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void ikramUpdateInsert(int siparisID, int adisyonID, int adet, double dusulecekDeger, int ikramAdedi, string yemekAdi, int ikramMi, DateTime verilisTarihi, double porsiyonu, bool turBool, bool NotificationGorulduMu)
        {
            int yeniSiparisAdeti = adet - ikramAdedi;

            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET Adet = @_Adet WHERE SiparisID=@id");
            cmd.Parameters.AddWithValue("@_Adet", yeniSiparisAdeti);
            cmd.Parameters.AddWithValue("@id", siparisID);
            cmd.ExecuteNonQuery();

            bool urunMutfagaBildirilmeliMi = mutfakBilgilendirilmeliMi(yemekAdi);

            cmd = SQLBaglantisi.getCommand("INSERT INTO Siparis(AdisyonID,Garsonu,Fiyatı,Adet,YemekAdi,IkramMi,VerilisTarihi,MutfakCiktisiAlindiMi,MutfakCiktisiAlinmaliMi,Porsiyon,KiloSatisiMi,NotificationGorulduMu) values(@_AdisyonID,@_Garsonu,@_Fiyatı,@_Adet,@_YemekAdi,@_IkramMi,@_VerilisTarihi,@_MutfakCiktisiAlindiMi,@_MutfakCiktisiAlinmaliMi,@_Porsiyon,@_KiloSatisiMi,@_NotificationGorulduMu)");
            cmd.Parameters.AddWithValue("@_AdisyonID", adisyonID);
            cmd.Parameters.AddWithValue("@_Garsonu", siparisiKimGirdi);
            cmd.Parameters.AddWithValue("@_Fiyatı", dusulecekDeger);
            cmd.Parameters.AddWithValue("@_Adet", ikramAdedi);
            cmd.Parameters.AddWithValue("@_YemekAdi", yemekAdi);
            cmd.Parameters.AddWithValue("@_IkramMi", ikramMi);
            cmd.Parameters.AddWithValue("@_VerilisTarihi", verilisTarihi);
            cmd.Parameters.AddWithValue("@_MutfakCiktisiAlindiMi", 1);
            cmd.Parameters.AddWithValue("@_MutfakCiktisiAlinmaliMi", urunMutfagaBildirilmeliMi);
            cmd.Parameters.AddWithValue("@_Porsiyon", porsiyonu);

            if (turBool)
            {
                cmd.Parameters.AddWithValue("@_KiloSatisiMi", 1);
                cmd.Parameters.AddWithValue("@_NotificationGorulduMu", 1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@_KiloSatisiMi", 0);
                cmd.Parameters.AddWithValue("@_NotificationGorulduMu", NotificationGorulduMu);
            }

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void ikramUpdateTam(int siparisID, int ikramMi)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET IkramMi=@ikram, Garsonu=@Garson WHERE SiparisID=@id");
            cmd.Parameters.AddWithValue("@ikram", ikramMi);
            cmd.Parameters.AddWithValue("@Garson", siparisiKimGirdi);
            cmd.Parameters.AddWithValue("@id", siparisID);
            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void adisyonNotuUpdate(int adisyonID)
        {
            if (adisyonNotu != "" && adisyonNotu != null)
            {
                SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET AdisyonNotu=@adisyonNotu WHERE AdisyonID=@id");

                cmd.Parameters.AddWithValue("@adisyonNotu", adisyonNotu);
                cmd.Parameters.AddWithValue("@id", adisyonID);
                cmd.ExecuteNonQuery();

                cmd.Connection.Close();
                cmd.Connection.Dispose();
            }
        }

        public void iptalUpdateInsert(int siparisID, int adisyonID, int adet, double dusulecekDeger, int iptalAdedi, string yemekAdi, DateTime verilisTarihi, double porsiyon, string iptalNedeni, bool tur)
        {
            int yeniSiparisAdeti = adet - iptalAdedi;

            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET Adet = @_Adet WHERE SiparisID=@_id");
            cmd.Parameters.AddWithValue("@_Adet", yeniSiparisAdeti);
            cmd.Parameters.AddWithValue("@_id", siparisID);
            cmd.ExecuteNonQuery();

            bool urunMutfagaBildirilmeliMi = mutfakBilgilendirilmeliMi(yemekAdi);

            if (tur)
            {
                cmd = SQLBaglantisi.getCommand("INSERT INTO Siparis(AdisyonID,Garsonu,Fiyatı,Adet,YemekAdi,IptalMi,VerilisTarihi,MutfakCiktisiAlinmaliMi,Porsiyon,IptalNedeni,MutfakCiktisiAlindiMi,KiloSatisiMi,NotificationGorulduMu) values(@_AdisyonID,@_Garsonu,@_Fiyatı,@_Adet,@_YemekAdi,@_IptalMi,@_VerilisTarihi,@_MutfakCiktisiAlinmaliMi,@_Porsiyon,@_IptalNedeni,@_MutfakCiktisiAlindiMi,@_KiloSatisiMi,@_NotificationGorulduMu)");
                cmd.Parameters.AddWithValue("@_AdisyonID", adisyonID);
                cmd.Parameters.AddWithValue("@_Garsonu", siparisiKimGirdi);
                cmd.Parameters.AddWithValue("@_Fiyatı", dusulecekDeger);
                cmd.Parameters.AddWithValue("@_Adet", iptalAdedi);
                cmd.Parameters.AddWithValue("@_YemekAdi", yemekAdi);
                cmd.Parameters.AddWithValue("@_IptalMi", 1);
                cmd.Parameters.AddWithValue("@_VerilisTarihi", verilisTarihi);
                cmd.Parameters.AddWithValue("@_MutfakCiktisiAlinmaliMi", urunMutfagaBildirilmeliMi);
                cmd.Parameters.AddWithValue("@_Porsiyon", porsiyon);
                cmd.Parameters.AddWithValue("@_IptalNedeni", iptalNedeni);
                cmd.Parameters.AddWithValue("@_MutfakCiktisiAlindiMi", 0);
                cmd.Parameters.AddWithValue("@_KiloSatisiMi", 1);
                cmd.Parameters.AddWithValue("@_NotificationGorulduMu", 1);
            }
            else
            {
                cmd = SQLBaglantisi.getCommand("INSERT INTO Siparis(AdisyonID,Garsonu,Fiyatı,Adet,YemekAdi,IptalMi,VerilisTarihi,MutfakCiktisiAlinmaliMi,Porsiyon,IptalNedeni,MutfakCiktisiAlindiMi,KiloSatisiMi) values(@_AdisyonID,@_Garsonu,@_Fiyatı,@_Adet,@_YemekAdi,@_IptalMi,@_VerilisTarihi,@_MutfakCiktisiAlinmaliMi,@_Porsiyon,@_IptalNedeni,@_MutfakCiktisiAlindiMi,@_KiloSatisiMi)");
                cmd.Parameters.AddWithValue("@_AdisyonID", adisyonID);
                cmd.Parameters.AddWithValue("@_Garsonu", siparisiKimGirdi);
                cmd.Parameters.AddWithValue("@_Fiyatı", dusulecekDeger);
                cmd.Parameters.AddWithValue("@_Adet", iptalAdedi);
                cmd.Parameters.AddWithValue("@_YemekAdi", yemekAdi);
                cmd.Parameters.AddWithValue("@_IptalMi", 1);
                cmd.Parameters.AddWithValue("@_VerilisTarihi", verilisTarihi);
                cmd.Parameters.AddWithValue("@_MutfakCiktisiAlinmaliMi", urunMutfagaBildirilmeliMi);
                cmd.Parameters.AddWithValue("@_Porsiyon", porsiyon);
                cmd.Parameters.AddWithValue("@_IptalNedeni", iptalNedeni);
                cmd.Parameters.AddWithValue("@_MutfakCiktisiAlindiMi", 0);
                cmd.Parameters.AddWithValue("@_KiloSatisiMi", 0);
            }

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void iptalUpdateTam(int siparisID, string iptalNedeni)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET IptalMi=@_Iptal, Garsonu=@_Garson, IptalNedeni=@_IptalNedeni, MutfakCiktisiAlindiMi=@_MutfakCiktisiAlindiMi WHERE SiparisID=@_id");
            cmd.Parameters.AddWithValue("@_Iptal", 1);
            cmd.Parameters.AddWithValue("@_Garson", siparisiKimGirdi);
            cmd.Parameters.AddWithValue("@_IptalNedeni", iptalNedeni);
            cmd.Parameters.AddWithValue("@_id", siparisID);
            cmd.Parameters.AddWithValue("@_MutfakCiktisiAlindiMi", 0);

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void urunTasimaUpdateTam(int siparisID, int aktarimYapilacakMasaninAdisyonID)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET Garsonu=@Garson, AdisyonID=@adisyonId WHERE SiparisID=@id");
            cmd.Parameters.AddWithValue("@Garson", siparisiKimGirdi);
            cmd.Parameters.AddWithValue("@adisyonId", aktarimYapilacakMasaninAdisyonID);
            cmd.Parameters.AddWithValue("@id", siparisID);

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void urunTasimaUpdateInsert(int siparisID, int aktarimYapilacakMasaninAdisyonID, int adet, double dusulecekDeger, int tasinacakMiktar, string yemekAdi, int ikramMi, DateTime verilisTarihi, string porsiyon, bool turBool, bool NotificationGorulduMu)
        {
            int yeniSiparisAdeti = adet - tasinacakMiktar;

            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET Adet = @_Adet WHERE SiparisID=@id");
            cmd.Parameters.AddWithValue("@_Adet", yeniSiparisAdeti);
            cmd.Parameters.AddWithValue("@id", siparisID);
            cmd.ExecuteNonQuery();

            bool urunMutfagaBildirilmeliMi = mutfakBilgilendirilmeliMi(yemekAdi);

            cmd = SQLBaglantisi.getCommand("INSERT INTO Siparis(AdisyonID,Garsonu,Fiyatı,Adet,YemekAdi,IkramMi,VerilisTarihi,MutfakCiktisiAlindiMi,MutfakCiktisiAlinmaliMi,Porsiyon,KiloSatisiMi,NotificationGorulduMu) values(@_AdisyonID,@_Garsonu,@_Fiyatı,@_Adet,@_YemekAdi,@_IkramMi,@_VerilisTarihi,@_MutfakCiktisiAlindiMi,@_MutfakCiktisiAlinmaliMi,@_Porsiyon,@_KiloSatisiMi,@_NotificationGorulduMu)");
            cmd.Parameters.AddWithValue("@_AdisyonID", aktarimYapilacakMasaninAdisyonID);
            cmd.Parameters.AddWithValue("@_Garsonu", siparisiKimGirdi);
            cmd.Parameters.AddWithValue("@_Fiyatı", dusulecekDeger);
            cmd.Parameters.AddWithValue("@_Adet", tasinacakMiktar);
            cmd.Parameters.AddWithValue("@_YemekAdi", yemekAdi);
            cmd.Parameters.AddWithValue("@_IkramMi", ikramMi);
            cmd.Parameters.AddWithValue("@_VerilisTarihi", verilisTarihi);
            cmd.Parameters.AddWithValue("@_MutfakCiktisiAlindiMi", 1);
            cmd.Parameters.AddWithValue("@_MutfakCiktisiAlinmaliMi", urunMutfagaBildirilmeliMi);
            cmd.Parameters.AddWithValue("@_Porsiyon", Convert.ToDecimal(porsiyon));

            if (turBool)
            {
                cmd.Parameters.AddWithValue("@_KiloSatisiMi", 1);
                cmd.Parameters.AddWithValue("@_NotificationGorulduMu", 1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@_KiloSatisiMi", 0);
                cmd.Parameters.AddWithValue("@_NotificationGorulduMu", NotificationGorulduMu);
            }

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        #endregion

        private bool mutfakBilgilendirilmeliMi(string yemekAdi)
        {
            for (int i = 0; i < urunListesi.Count(); i++)
            {
                for (int j = 0; j < urunListesi[i].urunMutfagaBildirilmeliMi.Count; j++)
                {
                    if (urunListesi[i].urunAdi[j] == yemekAdi)
                    {
                        return urunListesi[i].urunMutfagaBildirilmeliMi[j];
                    }
                }
            }
            return true;
        }

        //Seçili siparişlerin adisyonunu değiştiren butonun methodu -- Yeni Siparişler taşınamaz
        private void buttonTasi_Click(object sender, EventArgs e)
        {
            UrunDegistir urunDegistirForm = new UrunDegistir(listUrunFiyat.SelectedItems);
            DialogResult urunDegissinMi = urunDegistirForm.ShowDialog();

            if (urunDegissinMi == DialogResult.OK)
            {
                Decimal toplam = 0;

                //Eğer taşınmak istenen ürünlerin  miktarı 0 yapılmışsa, 0 yapılanları taşımaya çalışma
                //Eğer taşınmak istenen ürünlerin fiyatları hesabı aşıyorsa ürünleri taşıma
                for (int i = 0; i < urunDegistirForm.miktarlar.Count; i++)
                {
                    if (urunDegistirForm.miktarlar[i] == 0)
                    {
                        urunDegistirForm.miktarlar.RemoveAt(i);
                        listUrunFiyat.Items[listUrunFiyat.SelectedItems[i].Index].Selected = false;
                        i--;
                    }
                    else
                    {
                        toplam += Convert.ToDecimal(listUrunFiyat.Items[listUrunFiyat.SelectedItems[i].Index].SubItems[3].Text);
                    }
                }

                //Eğer taşınması gereken ürün sayılarında 0 yapılanlar varsa ve onların dışında taşınacak ürün yoksa, ürün taşıma
                if (urunDegistirForm.miktarlar.Count < 1)
                {
                    buttonTemizle_Click(null, null);
                    return;
                }

                if (toplam > Convert.ToDecimal(labelKalanHesap.Text))
                {
                    buttonTemizle_Click(null, null);
                    KontrolFormu dialog = new KontrolFormu("Ürün taşıma gerçekleştirilemedi\nTaşınmak istenen ürünlerin toplam tutarı,\n kalan hesabı geçemez", false);
                    dialog.Show();
                    return;
                }

                masaDegistirForm = new MasaDegistirFormu(MasaAdi, hangiDepartman.departmanAdi, this);
                masaDegistirForm.ShowDialog();

                if (masaDegistirForm.yeniMasa == "iptalEdildi")
                {
                    buttonTemizle_Click(null, null);
                    return;
                }
                else
                {
                    int istenilenTasimaMiktari;

                    int tasinacakUrunIkramMi;

                    StringBuilder aktarmaBilgileri = new StringBuilder();

                    if (Properties.Settings.Default.Server == 2) // server
                    {
                        int aktarilacakMasaninAdisyonID;

                        SqlCommand cmd = SQLBaglantisi.getCommand("SELECT AdisyonID FROM Adisyon WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Adisyon.MasaAdi='" + masaDegistirForm.yeniMasa + "' AND Adisyon.DepartmanAdi='" + masaDegistirForm.yeniDepartman + "' ");

                        SqlDataReader dr = cmd.ExecuteReader();

                        dr.Read();

                        try
                        {
                            aktarilacakMasaninAdisyonID = dr.GetInt32(0);
                        }
                        catch
                        {
                            aktarilacakMasaninAdisyonID = bosAdisyonOlustur(masaDegistirForm.yeniMasa, masaDegistirForm.yeniDepartman);

                            urunTasinirkenYeniMasaOlusturulduysaOlusanMasaninAdi = masaDegistirForm.yeniMasa;
                            urunTasinirkenYeniMasaOlusturulduysaOlusanDepartmaninAdi = masaDegistirForm.yeniDepartman;
                        }

                        if (listUrunFiyat.Items.Count == urunDegistirForm.miktarlar.Count)
                        {
                            masaAcikMi = false;
                        }

                        for (int i = 0; i < urunDegistirForm.miktarlar.Count; i++)
                        {
                            string tur = listUrunFiyat.SelectedItems[i].SubItems[1].Text[listUrunFiyat.SelectedItems[0].SubItems[1].Text.Length - 1].ToString();

                            bool turBool = false;

                            if (tur == "K")
                            {
                                turBool = true;
                            }

                            istenilenTasimaMiktari = urunDegistirForm.miktarlar[i];

                            if (Convert.ToDouble(listUrunFiyat.SelectedItems[i].SubItems[0].Text) > Convert.ToDouble(istenilenTasimaMiktari))
                                masaAcikMi = true;

                            double dusulecekDeger = Convert.ToDouble(listUrunFiyat.SelectedItems[i].SubItems[3].Text);

                            if (listUrunFiyat.SelectedItems[i].Group == listUrunFiyat.Groups[2]) // ürünü diğer adisyona geçirirken IkramMi değerini bu değişkenden alacağız
                            {
                                tasinacakUrunIkramMi = 0;
                            }
                            else
                            {
                                tasinacakUrunIkramMi = 1;
                            }

                            cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adisyon.AdisyonID,Adet,Siparis.VerilisTarihi,NotificationGorulduMu FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi='" + tasinacakUrunIkramMi + "' AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + MasaAdi + "' AND Adisyon.DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND Siparis.YemekAdi='" + listUrunFiyat.SelectedItems[i].SubItems[2].Text + "' AND Siparis.Garsonu='" + siparisiKimGirdi + "'  AND Siparis.Porsiyon=CONVERT(DECIMAL(5,2),@_Porsiyon) AND Siparis.KiloSatisiMi=@_Tur ORDER BY Adet DESC");
                        
                            cmd.Parameters.AddWithValue("@_Porsiyon", Convert.ToDecimal(listUrunFiyat.SelectedItems[i].SubItems[1].Text.Substring(0, listUrunFiyat.SelectedItems[i].SubItems[1].Text.Length - 1)));

                            cmd.Parameters.AddWithValue("@_Tur", turBool);

                            dr = cmd.ExecuteReader();

                            int siparisID, adisyonID, adet;
                            DateTime verilisTarihi;
                            bool NotificationGorulduMu;
                            while (dr.Read())
                            {
                                try
                                {
                                    siparisID = dr.GetInt32(0);
                                    adisyonID = dr.GetInt32(1);
                                    adet = dr.GetInt32(2);
                                    verilisTarihi = dr.GetDateTime(3);
                                    NotificationGorulduMu = dr.GetBoolean(4);
                                }
                                catch
                                {
                                    buttonTemizle_Click(null, null);
                                    KontrolFormu dialog = new KontrolFormu("Ürünü taşırken bir hata oluştu, lütfen tekrar deneyiniz", false);
                                    dialog.Show();
                                    return;
                                }

                                if (adet < istenilenTasimaMiktari) // elimizde ikram edilmemişler ikramı istenenden küçükse
                                {
                                    urunTasimaUpdateTam(siparisID, aktarilacakMasaninAdisyonID);

                                    istenilenTasimaMiktari -= adet;
                                }
                                else if (adet > istenilenTasimaMiktari) // den büyükse
                                {
                                    urunTasimaUpdateInsert(siparisID, aktarilacakMasaninAdisyonID, adet, dusulecekDeger, istenilenTasimaMiktari, listUrunFiyat.SelectedItems[i].SubItems[2].Text, tasinacakUrunIkramMi, verilisTarihi, listUrunFiyat.SelectedItems[i].SubItems[1].Text.Substring(0, listUrunFiyat.SelectedItems[i].SubItems[1].Text.Length - 1), turBool, NotificationGorulduMu);

                                    istenilenTasimaMiktari = 0;
                                }
                                else // elimizde ikram edilmemişler ikramı istenene eşitse
                                {
                                    urunTasimaUpdateTam(siparisID, aktarilacakMasaninAdisyonID);

                                    istenilenTasimaMiktari = 0;
                                }

                                if (istenilenTasimaMiktari == 0 && urunDegistirForm.miktarlar.Count - 1 == i)
                                    break;
                                else if (istenilenTasimaMiktari == 0)
                                    break;
                            }

                            if (istenilenTasimaMiktari != 0)// aktarılacaklar daha bitmedi başka garsonların siparişlerinden aktarıma devam et
                            {
                                cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adet,Siparis.VerilisTarihi,NotificationGorulduMu FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi='" + tasinacakUrunIkramMi + "' AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + MasaAdi + "' AND Adisyon.DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND Siparis.YemekAdi='" + listUrunFiyat.SelectedItems[i].SubItems[2].Text + "' AND Siparis.Garsonu!='" + siparisiKimGirdi + "' AND Siparis.Porsiyon=CONVERT(DECIMAL(5,2),@_Porsiyon) AND Siparis.KiloSatisiMi=@_Tur ORDER BY Adet DESC");

                                cmd.Parameters.AddWithValue("@_Porsiyon", Convert.ToDecimal(listUrunFiyat.SelectedItems[i].SubItems[1].Text.Substring(0, listUrunFiyat.SelectedItems[i].SubItems[1].Text.Length - 1)));
                                
                                cmd.Parameters.AddWithValue("@_Tur", turBool);

                                dr = cmd.ExecuteReader();

                                while (dr.Read())
                                {
                                    try
                                    {
                                        siparisID = dr.GetInt32(0);
                                        adet = dr.GetInt32(1);
                                        verilisTarihi = dr.GetDateTime(2);
                                        NotificationGorulduMu = dr.GetBoolean(3);
                                    }
                                    catch
                                    {
                                        buttonTemizle_Click(null, null);
                                        KontrolFormu dialog = new KontrolFormu("Ürünü taşırken bir hata oluştu, lütfen tekrar deneyiniz", false);
                                        dialog.Show();
                                        return;
                                    }

                                    if (adet < istenilenTasimaMiktari) // elimizde ikram edilmemişler ikramı istenenden küçükse
                                    {
                                        urunTasimaUpdateTam(siparisID, aktarilacakMasaninAdisyonID);

                                        istenilenTasimaMiktari -= adet;
                                    }
                                    else if (adet > istenilenTasimaMiktari) // den büyükse
                                    {
                                        urunTasimaUpdateInsert(siparisID, aktarilacakMasaninAdisyonID, adet, dusulecekDeger, istenilenTasimaMiktari, listUrunFiyat.SelectedItems[i].SubItems[2].Text, tasinacakUrunIkramMi, verilisTarihi, listUrunFiyat.SelectedItems[i].SubItems[1].Text.Substring(0, listUrunFiyat.SelectedItems[i].SubItems[1].Text.Length - 1), turBool, NotificationGorulduMu);

                                        istenilenTasimaMiktari = 0;
                                    }
                                    else // elimizde ikram edilmemişler ikramı istenene eşitse
                                    {
                                        urunTasimaUpdateTam(siparisID, aktarilacakMasaninAdisyonID);

                                        istenilenTasimaMiktari = 0;
                                    }

                                    if (istenilenTasimaMiktari == 0 && urunDegistirForm.miktarlar.Count - 1 == i)
                                        break;
                                    else if (istenilenTasimaMiktari == 0)
                                        break;
                                }
                            }

                            cmd.Connection.Close();
                            cmd.Connection.Dispose();
                        }

                        if (!masaAcikMi)
                        {
                            cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET AcikMi=0, IptalMi=1, KapanisZamani=@date WHERE AdisyonID=(SELECT AdisyonID FROM Adisyon WHERE AcikMi=1 AND MasaAdi='" + MasaAdi + "' AND DepartmanAdi='" + hangiDepartman.departmanAdi + "')");
                            cmd.Parameters.AddWithValue("@date", DateTime.Now);

                            try //adisyonID alınabilirse adisyon var demektir, ancak sipariş yok - o zaman adisyon kapatılır
                            {
                                cmd.ExecuteNonQuery();
                                cmd.Connection.Close();
                                cmd.Connection.Dispose();
                            }
                            catch // masaya ait adisyon yok, çık
                            {
                                cmd.Connection.Close();
                                cmd.Connection.Dispose();
                                this.Close();
                                return;
                            }
                        }

                        masaFormu.serverdanMasaDegisikligi(MasaAdi, hangiDepartman.departmanAdi, masaDegistirForm.yeniMasa, masaDegistirForm.yeniDepartman, "urunTasindi");

                        urunAktarmaIslemlerindenSonraSiparisleriGir(MasaAdi, hangiDepartman.departmanAdi);

                        dialogTimer = new KontrolFormu("Masa(" + MasaAdi + ")'dan seçilen ürünler " + masaDegistirForm.yeniDepartman + "\ndepartmanındaki, " + masaDegistirForm.yeniMasa + " masasına aktarıldı\nLütfen masaya yeniden giriş yapınız", false, this);
                        timerDialogClose.Start();
                        dialogTimer.Show();
                        this.Enabled = false;
                    }
                    else //client
                    {
                        if (listUrunFiyat.Items.Count == urunDegistirForm.miktarlar.Count)
                        {
                            masaAcikMi = false;
                        }

                        urunTasinirkenYeniMasaOlusturulduysaOlusanMasaninAdi = masaDegistirForm.yeniMasa;
                        urunTasinirkenYeniMasaOlusturulduysaOlusanDepartmaninAdi = masaDegistirForm.yeniDepartman;

                        for (int i = 0; i < urunDegistirForm.miktarlar.Count; i++)
                        {
                            istenilenTasimaMiktari = urunDegistirForm.miktarlar[i];

                            if (listUrunFiyat.SelectedItems[i].Group == listUrunFiyat.Groups[2]) // ürünü diğer adisyona geçirirken IkramMi değerini bu değişkenden alacağız
                            {
                                tasinacakUrunIkramMi = 0;
                            }
                            else
                            {
                                tasinacakUrunIkramMi = 1;
                            }

                            double dusulecekDeger = Convert.ToDouble(listUrunFiyat.SelectedItems[i].SubItems[3].Text);

                            aktarmaBilgileri.Append("*" + listUrunFiyat.SelectedItems[i].SubItems[2].Text + "-" + dusulecekDeger.ToString() + "-" + istenilenTasimaMiktari.ToString() + "-" + tasinacakUrunIkramMi.ToString() + "-" + listUrunFiyat.SelectedItems[i].SubItems[1].Text.Substring(0, listUrunFiyat.SelectedItems[i].SubItems[1].Text.Length - 1) + "-" + listUrunFiyat.SelectedItems[i].SubItems[1].Text[listUrunFiyat.SelectedItems[i].SubItems[1].Text.Length - 1].ToString());

                            if (Convert.ToDouble(listUrunFiyat.SelectedItems[i].SubItems[0].Text) > Convert.ToDouble(istenilenTasimaMiktari)) // eğer kalan ürün varsa masa açık
                                masaAcikMi = true;
                        }

                        if (aktarmaBilgileri.Length >= 1)
                        {
                            aktarmaBilgileri.Remove(0, 1);
                        }

                        urunAktarmaIslemlerindenSonraSiparisleriGir(MasaAdi, hangiDepartman.departmanAdi);

                        masaFormu.menuFormundanServeraUrunTasinacakBilgisiGonder(MasaAdi, hangiDepartman.departmanAdi, "urunuTasi", masaDegistirForm.yeniMasa, masaDegistirForm.yeniDepartman, siparisiKimGirdi, aktarmaBilgileri);

                        if (!masaAcikMi)
                        {
                            masaFormu.siparisListesiBos(MasaAdi, hangiDepartman.departmanAdi, "listeBos");
                        }
                    }
                }
            }
        }

        private void urunAktarmaIslemlerindenSonraSiparisleriGir(string masaAdi, string departmanAdi)
        {
            if (!adisyonNotuGuncellenmeliMi && listUrunFiyat.Groups[3].Items.Count == 0)
                return;

            if (Properties.Settings.Default.Server == 2) //server - diğer tüm clientlara söylemeli yaptığı ikram vs. neyse
            {
                SqlCommand cmd;

                int adisyonID;

                cmd = SQLBaglantisi.getCommand("SELECT AdisyonID FROM Adisyon WHERE MasaAdi='" + masaAdi + "' AND DepartmanAdi='" + departmanAdi + "' AND AcikMi=1");
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();

                adisyonID = dr.GetInt32(0);

                cmd.Connection.Close();
                cmd.Connection.Dispose();

                foreach (ListViewItem siparis in listUrunFiyat.Groups[yeniSiparisler].Items)
                {
                    siparisOlustur(adisyonID, siparis);

                    masaFormu.serverdanSiparisIkramVeyaIptal(masaAdi, departmanAdi, "siparis", siparis.SubItems[0].Text, siparis.SubItems[2].Text, Convert.ToDecimal(siparis.SubItems[3].Text).ToString(), null, siparis.SubItems[1].Text.Substring(0, siparis.SubItems[1].Text.Length - 1),siparis.SubItems[1].Text[siparis.SubItems[1].Text.Length - 1].ToString());
                }

                //yeni ürünler için mutfak bildirimi
                cmd = SQLBaglantisi.getCommand("SELECT MutfakCiktisiAlindiMi FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND MutfakCiktisiAlindiMi=0 AND Siparis.IptalMi=0 AND Siparis.IkramMi=0 AND Siparis.OdendiMi=0 AND MasaAdi=@_MasaAdi AND DepartmanAdi=@_DepartmanAdi");
                cmd.Parameters.AddWithValue("@_MasaAdi", masaAdi);
                cmd.Parameters.AddWithValue("@_DepartmanAdi", departmanAdi);
                dr = cmd.ExecuteReader();

                KontrolFormu dialog2 = null;

                try
                {
                    dr.Read();

                    dr.GetBoolean(0);

                    // mutfak adisyonu iste
                    cmd = SQLBaglantisi.getCommand("SELECT FirmaAdi, Yazici FROM Yazici WHERE YaziciAdi LIKE 'Mutfak%'");

                    dr = cmd.ExecuteReader();

                    string firmaAdi = "", yaziciAdi = "";

                    while (dr.Read())
                    {
                        firmaAdi = dr.GetString(0);
                        yaziciAdi = dr.GetString(1);
                    }

                    cmd.Connection.Close();
                    cmd.Connection.Dispose();

                    if (yaziciAdi != "")
                        asyncYaziciyaGonder(masaAdi, departmanAdi, firmaAdi, yaziciAdi, raporMutfakNormal);
                    else
                    {
                        dialog2 = new KontrolFormu("Mutfak yazıcısı bulunamadı", false);
                        dialog2.Show();
                    }
                }
                catch
                {
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                }

                // iptal edilen ürünler için mutfağa adisyon
                cmd = SQLBaglantisi.getCommand("SELECT MutfakCiktisiAlindiMi FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND MutfakCiktisiAlindiMi=0 AND Siparis.IptalMi=1 AND Siparis.IkramMi=0 AND Siparis.OdendiMi=0 AND MasaAdi=@_MasaAdi AND DepartmanAdi=@_DepartmanAdi");
                cmd.Parameters.AddWithValue("@_MasaAdi", masaAdi);
                cmd.Parameters.AddWithValue("@_DepartmanAdi", departmanAdi);
                dr = cmd.ExecuteReader();

                try
                {
                    dr.Read();

                    dr.GetBoolean(0);

                    // mutfak adisyonu iste 
                    cmd = SQLBaglantisi.getCommand("SELECT FirmaAdi,Yazici FROM Yazici WHERE YaziciAdi LIKE 'Mutfak%'");
                    dr = cmd.ExecuteReader();

                    string firmaAdi = "", yaziciAdi = "";

                    while (dr.Read())
                    {
                        firmaAdi = dr.GetString(0);
                        yaziciAdi = dr.GetString(1);
                    }

                    cmd.Connection.Close();
                    cmd.Connection.Dispose();

                    if (yaziciAdi != "")
                        asyncYaziciyaGonder(masaAdi, departmanAdi, firmaAdi, yaziciAdi, raporMutfakIptal);
                    else
                    {
                        if (dialog2 == null)
                        {
                            dialog2 = new KontrolFormu("Mutfak yazıcısı bulunamadı", false);
                            dialog2.Show();
                        }
                    }
                }
                catch
                {
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                }

                if (adisyonNotuGuncellenmeliMi) // eğer sipariş notuna dokunulmuşsa not update edilsin
                {
                    adisyonNotuUpdate(adisyonID);
                }
            }
            else //client
            {
                if (listUrunFiyat.Groups[3].Items.Count > 0)
                {
                    int sonSiparisMi = listUrunFiyat.Groups[3].Items.Count;

                    foreach (ListViewItem siparis in listUrunFiyat.Groups[yeniSiparisler].Items)
                    {
                        sonSiparisMi--;

                        masaFormu.serveraSiparis(masaAdi, departmanAdi, "siparis", siparis.SubItems[0].Text, siparis.SubItems[2].Text, siparisiKimGirdi, Convert.ToDecimal(siparis.SubItems[3].Text).ToString(), adisyonNotu, sonSiparisMi, siparis.SubItems[1].Text.Substring(0, siparis.SubItems[1].Text.Length - 1), siparis.SubItems[1].Text[siparis.SubItems[1].Text.Length - 1].ToString());
                        adisyonNotuGuncellenmeliMi = false;
                    }
                }

                if (adisyonNotuGuncellenmeliMi)
                {
                    masaFormu.serveraNotuYolla(masaAdi, departmanAdi, "adisyonNotunuGuncelle", adisyonNotu);
                }
            }
        }

        private void timerDialogClose_Tick(object sender, EventArgs e)
        {
            dialogTimer.Close();
            timerDialogClose.Stop();
            veriAktarimiTamamlandi(this);
        }

        delegate void setButtonValueCallBack(SiparisMenuFormu veriButton);
        public void veriAktarimiTamamlandi(SiparisMenuFormu veriBtn)
        {
            if (veriBtn.InvokeRequired)
            {
                setButtonValueCallBack btndelegate = new setButtonValueCallBack(veriAktarimiTamamlandi);
                this.Invoke(btndelegate, new object[] { this });
            }
            else
            {
                this.Close();
            }
        }

        private void gorunumuDuzenle(string yemekAdi, string fiyati, string adedi, string porsiyonu)
        {
            //eğer ürün eklendiğinde ekrana sığmıyorsa scroll gösterilecektir, bu yüzden fiyatları sola kaydırıyoruz
            int itemsCount = this.listUrunFiyat.Items.Count + 3;// 3 aslında grup sayısı -1
            int itemHeight = this.listUrunFiyat.Items[0].Bounds.Height;
            int VisiableItem = (int)this.listUrunFiyat.ClientRectangle.Height / itemHeight;

            if (itemsCount >= VisiableItem)
            {
                listUrunFiyat.Columns[2].Width = urunBoyu;
                listUrunFiyat.Columns[3].Width = fiyatBoyu;
            }
        }

        private int siparisGruptaVarMi(int grup, string yemekAdi, string porsiyonu)
        {
            //ürün cinsi hesapta var mı bak 
            for (int i = 0; i < listUrunFiyat.Groups[grup].Items.Count; i++)
            {
                if (yemekAdi == listUrunFiyat.Groups[grup].Items[i].SubItems[2].Text && porsiyonu == listUrunFiyat.Groups[grup].Items[i].SubItems[1].Text)
                {
                    return i;
                }
            }
            return -1;
        }

        public void menuFormunuKapat()
        {
            buttonTamam_Click(null, null);
        }

        private void buttonCokluEkle_Click(object sender, EventArgs e)
        {
            labelCokluAdet.Text = (Convert.ToInt32(labelCokluAdet.Text) + 1).ToString();
        }

        private void buttonCokluCikar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(labelCokluAdet.Text) > 1)
                labelCokluAdet.Text = (Convert.ToInt32(labelCokluAdet.Text) - 1).ToString();
        }

        private void buttonPorsiyonSec_Click(object sender, EventArgs e)
        {
            if (secilenUrun.urunPorsiyonSinifi.Count > 0)
            {
                porsiyonForm = new PorsiyonFormu(secilenUrun.urunPorsiyonSinifi[0], this);
                porsiyonForm.ShowDialog();
            }
        }

        public void porsiyonFormKapaniyor(string porsiyonMiktari)
        {
            buttonPorsiyonSec.Text = porsiyonMiktari;
        }

        private void labelEklenecekUrun_TextChanged(object sender, EventArgs e)
        {
            if (((Label)sender).Text == "Ürün Seçiniz")
            {
                if (buttonAdd.Enabled)
                    buttonAdd.Enabled = false;
            }
            else
            {
                if (!buttonAdd.Enabled)
                    buttonAdd.Enabled = true;
            }
        }
    }
}