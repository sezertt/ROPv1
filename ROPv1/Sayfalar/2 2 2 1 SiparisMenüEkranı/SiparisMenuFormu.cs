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

namespace ROPv1
{
    public partial class SiparisMenuFormu : Form
    {
        public SiparisMasaFormu masaFormu;

        public MasaDegistirFormu masaDegistirForm;

        CrystalReportMutfak raporMutfak = new CrystalReportMutfak();

        public HesapFormu hesapForm;

        Restoran hangiDepartman;

        int hangiMenuSecili = 555, menuSirasi = 0;

        const int eskiIkramlar = 0, yeniIkramlar = 1, eskiSiparisler = 2, yeniSiparisler = 3;

        const int urunBoyu = 220, fiyatBoyu = 90;

        string siparisiKimGirdi, adisyonNotu = "", MasaAdi;

        List<Menuler> menuListesi = new List<Menuler>();  // menüleri tutacak liste

        List<KategorilerineGoreUrunler> urunListesi = new List<KategorilerineGoreUrunler>();

        List<bool> listedeSeciliOlanItemlar = new List<bool>();

        KontrolFormu dialog2;

        UItemp[] infoKullanici;

        bool iptalIkram = true, adisyonNotuGuncellenmeliMi = false;

        public bool masaAcikMi = false;

        public int masaDegisti = -1;

        public string yeniMasaninAdi = "", urunTasinirkenYeniMasaOlusturulduysaOlusanMasaninAdi = "";

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

            if (Helper.VerifyHash("false", "SHA512", infoKullanici[kullaniciYeri].UIY[4]))
            {
                buttonUrunIkram.Enabled = false;
                buttonUrunIptal.Enabled = false;
                iptalIkram = false;
            }

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
            if (hangiMenuSecili == (int)((Button)sender).Tag)
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
            hangiMenuSecili = Convert.ToInt32(((Button)sender).Tag);
        }

        //keypadin methodu
        private void pinboardcontrol21_UserKeyPressed(object sender, PinboardClassLibrary.PinboardEventArgs e)
        {
            textNumberOfItem.Focus();
            SendKeys.Send(e.KeyboardKeyPressed);
        }

        //Siparişlerin çarpanını ayarlayan textboxı boşaltan buton
        private void buttonDeleteText_Click(object sender, EventArgs e)
        {
            textNumberOfItem.Text = "";
        }

        #region Panellerdeki itemların görünümünü ekrana göre ayarlıyoruz
        private void myPannel_SizeChanged(object sender, EventArgs e)
        {
            flowPanelMenuBasliklari.SuspendLayout();

            int height = (flowPanelMenuBasliklari.Bounds.Height - 66) / 11;
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
            double kacPorsiyon = 1;

            if (textNumberOfItem.Text != "")
                kacPorsiyon = Convert.ToDouble(textNumberOfItem.Text);
            else
                kacPorsiyon = 1;

            if (kacPorsiyon == 0)
                return;

            int gruptaYeniGelenSiparisVarmi = -1; //ürün cinsi hesapta var mı bak 
            for (int i = 0; i < listUrunFiyat.Groups[yeniSiparisler].Items.Count; i++)
            {
                if (((Button)sender).Text == listUrunFiyat.Groups[yeniSiparisler].Items[i].SubItems[1].Text)
                {
                    gruptaYeniGelenSiparisVarmi = i;
                    break;
                }
            }

            if (gruptaYeniGelenSiparisVarmi == -1) //yoksa ürünü hesaba ekle
            {
                listUrunFiyat.Items.Add(kacPorsiyon.ToString());
                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(((Button)sender).Text);
                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(((decimal)kacPorsiyon * Convert.ToDecimal(urunListesi[hangiMenuSecili].porsiyonFiyati[Convert.ToInt32(((Button)sender).Tag)])).ToString("0.00"));
                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Group = listUrunFiyat.Groups[yeniSiparisler];
                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font = new Font("Calibri", 18.75F, FontStyle.Bold);
                listedeSeciliOlanItemlar.Add(false);

                //eğer ürün eklendiğinde ekrana sığmıyorsa scroll gösterilecektir, bu yüzden fiyatları sola kaydırıyoruz
                int itemsCount = this.listUrunFiyat.Items.Count + 3;// 3 aslında grup sayısı -1
                int itemHeight = this.listUrunFiyat.Items[0].Bounds.Height;
                int VisiableItem = (int)this.listUrunFiyat.ClientRectangle.Height / itemHeight;

                if (itemsCount >= VisiableItem)
                {
                    listUrunFiyat.Columns[1].Width = urunBoyu;
                    listUrunFiyat.Columns[2].Width = fiyatBoyu;

                    for (int i = 0; i < listUrunFiyat.Items.Count; i++)
                    {
                        while (listUrunFiyat.Columns[0].Width < System.Windows.Forms.TextRenderer.MeasureText(listUrunFiyat.Items[i].SubItems[0].Text, new Font(listUrunFiyat.Items[i].Font.FontFamily, listUrunFiyat.Items[i].Font.Size, listUrunFiyat.Items[i].Font.Style)).Width)
                        {
                            listUrunFiyat.Items[i].Font = new Font(listUrunFiyat.Items[i].Font.FontFamily, listUrunFiyat.Items[i].Font.Size - 0.5f, listUrunFiyat.Items[i].Font.Style);
                        }
                        while (listUrunFiyat.Columns[1].Width < System.Windows.Forms.TextRenderer.MeasureText(listUrunFiyat.Items[i].SubItems[1].Text, new Font(listUrunFiyat.Items[i].Font.FontFamily, listUrunFiyat.Items[i].Font.Size, listUrunFiyat.Items[i].Font.Style)).Width)
                        {
                            listUrunFiyat.Items[i].Font = new Font(listUrunFiyat.Items[i].Font.FontFamily, listUrunFiyat.Items[i].Font.Size - 0.5f, listUrunFiyat.Items[i].Font.Style);
                        }

                        while (listUrunFiyat.Columns[2].Width < System.Windows.Forms.TextRenderer.MeasureText(listUrunFiyat.Items[i].SubItems[2].Text, new Font(listUrunFiyat.Items[i].Font.FontFamily, listUrunFiyat.Items[i].Font.Size, listUrunFiyat.Items[i].Font.Style)).Width)
                        {
                            listUrunFiyat.Items[i].Font = new Font(listUrunFiyat.Items[i].Font.FontFamily, listUrunFiyat.Items[i].Font.Size - 0.5f, listUrunFiyat.Items[i].Font.Style);
                        }
                    }
                }

                while (listUrunFiyat.Columns[1].Width < System.Windows.Forms.TextRenderer.MeasureText(((Button)sender).Text, listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font).Width
                    || listUrunFiyat.Columns[2].Width < System.Windows.Forms.TextRenderer.MeasureText(((decimal)kacPorsiyon * Convert.ToDecimal(urunListesi[hangiMenuSecili].porsiyonFiyati[Convert.ToInt32(((Button)sender).Tag)])).ToString("0.00"), listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font).Width
                    || listUrunFiyat.Columns[0].Width < System.Windows.Forms.TextRenderer.MeasureText(kacPorsiyon.ToString(), listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font).Width)
                {
                    listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font = new Font(listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font.FontFamily, listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font.Size - 0.5f, listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font.Style);
                }
            }
            else // varsa ürünün hesaptaki değerlerini istenilene göre arttır
            {
                listUrunFiyat.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text = (Convert.ToDouble(listUrunFiyat.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text) + kacPorsiyon).ToString();

                listUrunFiyat.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[2].Text = (Convert.ToDecimal(listUrunFiyat.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[2].Text) + (decimal)kacPorsiyon * Convert.ToDecimal(urunListesi[hangiMenuSecili].porsiyonFiyati[Convert.ToInt32(((Button)sender).Tag)])).ToString("0.00");

                while (listUrunFiyat.Columns[2].Width < System.Windows.Forms.TextRenderer.MeasureText(listUrunFiyat.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[2].Text, listUrunFiyat.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].Font).Width
                    || listUrunFiyat.Columns[0].Width < System.Windows.Forms.TextRenderer.MeasureText(listUrunFiyat.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text, listUrunFiyat.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].Font).Width)
                {
                    listUrunFiyat.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].Font = new Font(listUrunFiyat.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].Font.FontFamily, listUrunFiyat.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].Font.Size - 0.5f, listUrunFiyat.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].Font.Style);
                }


            }

            labelKalanHesap.Text = (Convert.ToDecimal(labelKalanHesap.Text) + (decimal)kacPorsiyon * Convert.ToDecimal(urunListesi[hangiMenuSecili].porsiyonFiyati[Convert.ToInt32(((Button)sender).Tag)])).ToString("0.00");

            textNumberOfItem.Text = ""; // çarpanı sil
        }

        //çarpan özellikleri
        private void keyPressedOnPriceText(object sender, KeyPressEventArgs e)
        {
            if (textNumberOfItem.Text == String.Empty)
            {
                if (e.KeyChar == '0')
                    e.Handled = true;
            }

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }
            else if (e.KeyChar == ',' && (sender as TextBox).Text.IndexOf(',') > -1 && (sender as TextBox).Text.Length < 4)
            { // 1 kere , kullanmasına izin ver

                e.Handled = true;
            }
        }

        //listede eleman seçildiğinde çalışacak olan method
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
                buttonAdd.Enabled = false;
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
                    buttonUrunIkram.Text = "    İkram İptal";
                else
                    buttonUrunIkram.Text = "  İkram";

                buttonAdd.Enabled = true;

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
                buttonAdd.Enabled = false;

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
        }

        //çarpanın ayarları
        private void textNumberOfItem_TextChanged(object sender, EventArgs e)
        {
            if (textNumberOfItem.Text == ",")
            {
                textNumberOfItem.Text = "0,";
                textNumberOfItem.SelectionStart = textNumberOfItem.Text.Length;
            }
        }

        //listede seçili elemanların seçimini kaldırır, çarpanı 0 lar ikram/iptal/ekle/taşı butonlarını disable eder
        private void buttonTemizle_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listedeSeciliOlanItemlar.Count; i++)
            {
                listedeSeciliOlanItemlar[i] = false;
                listUrunFiyat.Items[i].Selected = false;
            }

            textNumberOfItem.Text = "";
            buttonUrunIkram.Enabled = false;
            buttonTasi.Enabled = false;
            buttonUrunIptal.Enabled = false;
            buttonAdd.Enabled = false;
        }

        //listede seçili üründen çarpan kadar(çarpan yoksa 1 tane) ekleme butonu(ekle)
        private void buttonAdd_Click(object sender, EventArgs e)
        {

            double kacPorsiyon = 1;

            if (textNumberOfItem.Text != "")
                kacPorsiyon = Convert.ToDouble(textNumberOfItem.Text);
            else
                kacPorsiyon = 1;

            if (kacPorsiyon == 0)
                return;

            int gruptaYeniGelenSiparisVarmi = -1; //ürün cinsi hesapta var mı bak 
            for (int i = 0; i < listUrunFiyat.Groups[yeniSiparisler].Items.Count; i++)
            {
                if (listUrunFiyat.SelectedItems[0].SubItems[1].Text == listUrunFiyat.Groups[yeniSiparisler].Items[i].SubItems[1].Text)
                {
                    gruptaYeniGelenSiparisVarmi = i;
                    break;
                }
            }

            decimal eklenecekDeger = Convert.ToDecimal(listUrunFiyat.SelectedItems[0].SubItems[2].Text) / Convert.ToDecimal(listUrunFiyat.SelectedItems[0].SubItems[0].Text);

            if (gruptaYeniGelenSiparisVarmi == -1) //yoksa ürünü hesaba ekle
            {
                listUrunFiyat.Items.Add(kacPorsiyon.ToString());

                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(listUrunFiyat.SelectedItems[0].SubItems[1].Text);

                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(((decimal)kacPorsiyon * eklenecekDeger).ToString("0.00"));
                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Group = listUrunFiyat.Groups[yeniSiparisler];
                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font = new Font("Calibri", 18.75F, FontStyle.Bold);
                listedeSeciliOlanItemlar.Add(false);

                //eğer ürün eklendiğinde ekrana sığmıyorsa scroll gösterilecektir, bu yüzden fiyatları sola kaydırıyoruz
                int itemsCount = this.listUrunFiyat.Items.Count + 3;// 3 aslında grup sayısı -1
                int itemHeight = this.listUrunFiyat.Items[0].Bounds.Height;
                int VisiableItem = (int)this.listUrunFiyat.ClientRectangle.Height / itemHeight;

                if (itemsCount >= VisiableItem)
                {
                    listUrunFiyat.Columns[1].Width = urunBoyu;
                    listUrunFiyat.Columns[2].Width = fiyatBoyu;

                    for (int i = 0; i < listUrunFiyat.Items.Count; i++)
                    {
                        while (listUrunFiyat.Columns[0].Width < System.Windows.Forms.TextRenderer.MeasureText(listUrunFiyat.Items[i].SubItems[0].Text, new Font(listUrunFiyat.Items[i].Font.FontFamily, listUrunFiyat.Items[i].Font.Size, listUrunFiyat.Items[i].Font.Style)).Width)
                        {
                            listUrunFiyat.Items[i].Font = new Font(listUrunFiyat.Items[i].Font.FontFamily, listUrunFiyat.Items[i].Font.Size - 0.5f, listUrunFiyat.Items[i].Font.Style);
                        }
                        while (listUrunFiyat.Columns[1].Width < System.Windows.Forms.TextRenderer.MeasureText(listUrunFiyat.Items[i].SubItems[1].Text, new Font(listUrunFiyat.Items[i].Font.FontFamily, listUrunFiyat.Items[i].Font.Size, listUrunFiyat.Items[i].Font.Style)).Width)
                        {
                            listUrunFiyat.Items[i].Font = new Font(listUrunFiyat.Items[i].Font.FontFamily, listUrunFiyat.Items[i].Font.Size - 0.5f, listUrunFiyat.Items[i].Font.Style);
                        }

                        while (listUrunFiyat.Columns[2].Width < System.Windows.Forms.TextRenderer.MeasureText(listUrunFiyat.Items[i].SubItems[2].Text, new Font(listUrunFiyat.Items[i].Font.FontFamily, listUrunFiyat.Items[i].Font.Size, listUrunFiyat.Items[i].Font.Style)).Width)
                        {
                            listUrunFiyat.Items[i].Font = new Font(listUrunFiyat.Items[i].Font.FontFamily, listUrunFiyat.Items[i].Font.Size - 0.5f, listUrunFiyat.Items[i].Font.Style);
                        }
                    }
                }

                while (listUrunFiyat.Columns[1].Width < System.Windows.Forms.TextRenderer.MeasureText(listUrunFiyat.SelectedItems[0].SubItems[2].Text, listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font).Width
                    || listUrunFiyat.Columns[2].Width < System.Windows.Forms.TextRenderer.MeasureText(((decimal)kacPorsiyon * eklenecekDeger).ToString("0.00"), listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font).Width
                    || listUrunFiyat.Columns[0].Width < System.Windows.Forms.TextRenderer.MeasureText(kacPorsiyon.ToString(), listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font).Width)
                {
                    listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font = new Font(listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font.FontFamily, listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font.Size - 0.5f, listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font.Style);
                }
            }
            else // varsa ürünün hesaptaki değerlerini istenilene göre arttır
            {
                listUrunFiyat.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text = (Convert.ToDouble(listUrunFiyat.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text) + kacPorsiyon).ToString();

                listUrunFiyat.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[2].Text = (Convert.ToDecimal(listUrunFiyat.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[2].Text) + (decimal)kacPorsiyon * eklenecekDeger).ToString("0.00");

                while (listUrunFiyat.Columns[2].Width < System.Windows.Forms.TextRenderer.MeasureText(listUrunFiyat.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[2].Text, listUrunFiyat.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].Font).Width
                    || listUrunFiyat.Columns[0].Width < System.Windows.Forms.TextRenderer.MeasureText(listUrunFiyat.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text, listUrunFiyat.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].Font).Width)
                {
                    listUrunFiyat.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].Font = new Font(listUrunFiyat.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].Font.FontFamily, listUrunFiyat.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].Font.Size - 0.5f, listUrunFiyat.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].Font.Style);
                }
            }

            labelKalanHesap.Text = (Convert.ToDecimal(labelKalanHesap.Text) + (decimal)kacPorsiyon * eklenecekDeger).ToString("0.00");

            textNumberOfItem.Text = ""; // çarpanı sil
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

                    SqlCommand cmd = SQLBaglantisi.getCommand("SELECT Fiyatı, Porsiyon, YemekAdi, IkramMi, Garsonu from Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.MasaAdi='" + MasaAdi + "' AND Adisyon.DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 ORDER BY Porsiyon DESC");
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        decimal yemeginFiyati;
                        double kacPorsiyon;
                        string yemeginAdi;
                        bool ikramMi;
                        string Garson;
                        try
                        {
                            yemeginFiyati = dr.GetDecimal(0);
                            kacPorsiyon = (double)dr.GetDecimal(1);
                            yemeginAdi = dr.GetString(2);
                            ikramMi = dr.GetBoolean(3);
                            Garson = dr.GetString(4);
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

                        int gruptaYeniGelenSiparisVarmi = -1; //ürün cinsi hesapta var mı bak 
                        for (int i = 0; i < listUrunFiyat.Groups[hangiGrup].Items.Count; i++)
                        {
                            if (yemeginAdi == listUrunFiyat.Groups[hangiGrup].Items[i].SubItems[1].Text)
                            {
                                gruptaYeniGelenSiparisVarmi = i;
                                break;
                            }
                        }

                        if (gruptaYeniGelenSiparisVarmi == -1) //yoksa ürünü hesaba ekle
                        {
                            listUrunFiyat.Items.Add(kacPorsiyon.ToString());
                            listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(yemeginAdi);
                            listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(((decimal)kacPorsiyon * yemeginFiyati).ToString("0.00"));
                            listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Group = listUrunFiyat.Groups[hangiGrup];
                            listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font = new Font("Calibri", 18.75F, FontStyle.Bold);
                            listedeSeciliOlanItemlar.Add(false);

                            int itemsCount = this.listUrunFiyat.Items.Count + 3;// 3 aslında grup sayısı -1
                            int itemHeight = this.listUrunFiyat.Items[0].Bounds.Height;
                            int VisiableItem = (int)this.listUrunFiyat.ClientRectangle.Height / itemHeight;

                            if (itemsCount >= VisiableItem)
                            {
                                listUrunFiyat.Columns[1].Width = urunBoyu;
                                listUrunFiyat.Columns[2].Width = fiyatBoyu;

                                for (int i = 0; i < listUrunFiyat.Items.Count; i++)
                                {
                                    while (listUrunFiyat.Columns[0].Width < System.Windows.Forms.TextRenderer.MeasureText(listUrunFiyat.Items[i].SubItems[0].Text, new Font(listUrunFiyat.Items[i].Font.FontFamily, listUrunFiyat.Items[i].Font.Size, listUrunFiyat.Items[i].Font.Style)).Width)
                                    {
                                        listUrunFiyat.Items[i].Font = new Font(listUrunFiyat.Items[i].Font.FontFamily, listUrunFiyat.Items[i].Font.Size - 0.5f, listUrunFiyat.Items[i].Font.Style);
                                    }
                                    while (listUrunFiyat.Columns[1].Width < System.Windows.Forms.TextRenderer.MeasureText(listUrunFiyat.Items[i].SubItems[1].Text, new Font(listUrunFiyat.Items[i].Font.FontFamily, listUrunFiyat.Items[i].Font.Size, listUrunFiyat.Items[i].Font.Style)).Width)
                                    {
                                        listUrunFiyat.Items[i].Font = new Font(listUrunFiyat.Items[i].Font.FontFamily, listUrunFiyat.Items[i].Font.Size - 0.5f, listUrunFiyat.Items[i].Font.Style);
                                    }

                                    while (listUrunFiyat.Columns[2].Width < System.Windows.Forms.TextRenderer.MeasureText(listUrunFiyat.Items[i].SubItems[2].Text, new Font(listUrunFiyat.Items[i].Font.FontFamily, listUrunFiyat.Items[i].Font.Size, listUrunFiyat.Items[i].Font.Style)).Width)
                                    {
                                        listUrunFiyat.Items[i].Font = new Font(listUrunFiyat.Items[i].Font.FontFamily, listUrunFiyat.Items[i].Font.Size - 0.5f, listUrunFiyat.Items[i].Font.Style);
                                    }
                                }
                            }

                            while (listUrunFiyat.Columns[1].Width < System.Windows.Forms.TextRenderer.MeasureText(yemeginAdi, listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font).Width
                                || listUrunFiyat.Columns[2].Width < System.Windows.Forms.TextRenderer.MeasureText(((decimal)kacPorsiyon * yemeginFiyati).ToString("0.00"), listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font).Width
                                || listUrunFiyat.Columns[0].Width < System.Windows.Forms.TextRenderer.MeasureText(kacPorsiyon.ToString(), listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font).Width)
                            {
                                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font = new Font(listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font.FontFamily, listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font.Size - 0.5f, listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font.Style);
                            }
                        }
                        else // varsa ürünün hesaptaki değerlerini istenilene göre arttır
                        {
                            listUrunFiyat.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text = (Convert.ToDouble(listUrunFiyat.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text) + kacPorsiyon).ToString();

                            listUrunFiyat.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].SubItems[2].Text = (Convert.ToDecimal(listUrunFiyat.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].SubItems[2].Text) + (decimal)kacPorsiyon * yemeginFiyati).ToString("0.00");

                            while (listUrunFiyat.Columns[2].Width < System.Windows.Forms.TextRenderer.MeasureText(listUrunFiyat.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].SubItems[2].Text, listUrunFiyat.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].Font).Width
                                || listUrunFiyat.Columns[0].Width < System.Windows.Forms.TextRenderer.MeasureText(listUrunFiyat.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text, listUrunFiyat.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].Font).Width)
                            {
                                listUrunFiyat.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].Font = new Font(listUrunFiyat.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].Font.FontFamily, listUrunFiyat.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].Font.Size - 0.5f, listUrunFiyat.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].Font.Style);
                            }
                        }

                        if (!ikramMi) // ikram değilse kalan hesaba gir
                            labelKalanHesap.Text = (Convert.ToDecimal(labelKalanHesap.Text) + (decimal)kacPorsiyon * yemeginFiyati).ToString("0.00");
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
                            listUrunFiyat.Columns[1].Width = urunBoyu;
                            listUrunFiyat.Columns[2].Width = fiyatBoyu;
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

            string[] siparisler;
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

            decimal yemeginFiyati;
            double kacPorsiyon;
            string yemeginAdi, Garson;
            bool ikramMi;

            for (int i = 0; i < siparisler.Count(); i++)
            {
                string[] detaylari = siparisler[i].Split('-');
                yemeginFiyati = Convert.ToDecimal(detaylari[0]);
                kacPorsiyon = Convert.ToDouble(detaylari[1]);
                yemeginAdi = detaylari[2];
                ikramMi = Convert.ToBoolean(detaylari[3]);
                Garson = detaylari[4];

                int hangiGrup;

                if (ikramMi)
                {
                    hangiGrup = 0;
                }
                else
                {
                    hangiGrup = 2;
                }

                int gruptaYeniGelenSiparisVarmi = -1; //ürün cinsi hesapta var mı bak 
                for (int j = 0; j < listUrunFiyat.Groups[hangiGrup].Items.Count; j++)
                {
                    if (yemeginAdi == listUrunFiyat.Groups[hangiGrup].Items[j].SubItems[1].Text)
                    {
                        gruptaYeniGelenSiparisVarmi = j;
                        break;
                    }
                }

                if (gruptaYeniGelenSiparisVarmi == -1) //yoksa ürünü hesaba ekle
                {
                    listUrunFiyat.Items.Add(kacPorsiyon.ToString());
                    listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(yemeginAdi);
                    listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(((decimal)kacPorsiyon * yemeginFiyati).ToString("0.00"));
                    listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Group = listUrunFiyat.Groups[hangiGrup];
                    listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font = new Font("Calibri", 18.75F, FontStyle.Bold);
                    listedeSeciliOlanItemlar.Add(false);

                    int itemsCount = this.listUrunFiyat.Items.Count + 3;// 3 aslında grup sayısı -1
                    int itemHeight = this.listUrunFiyat.Items[0].Bounds.Height;
                    int VisiableItem = (int)this.listUrunFiyat.ClientRectangle.Height / itemHeight;

                    if (itemsCount >= VisiableItem)
                    {
                        listUrunFiyat.Columns[1].Width = urunBoyu;
                        listUrunFiyat.Columns[2].Width = fiyatBoyu;

                        for (int k = 0; k < listUrunFiyat.Items.Count; k++)
                        {
                            while (listUrunFiyat.Columns[0].Width < System.Windows.Forms.TextRenderer.MeasureText(listUrunFiyat.Items[k].SubItems[0].Text, new Font(listUrunFiyat.Items[k].Font.FontFamily, listUrunFiyat.Items[k].Font.Size, listUrunFiyat.Items[k].Font.Style)).Width)
                            {
                                listUrunFiyat.Items[k].Font = new Font(listUrunFiyat.Items[k].Font.FontFamily, listUrunFiyat.Items[k].Font.Size - 0.5f, listUrunFiyat.Items[k].Font.Style);
                            }
                            while (listUrunFiyat.Columns[1].Width < System.Windows.Forms.TextRenderer.MeasureText(listUrunFiyat.Items[k].SubItems[1].Text, new Font(listUrunFiyat.Items[k].Font.FontFamily, listUrunFiyat.Items[k].Font.Size, listUrunFiyat.Items[k].Font.Style)).Width)
                            {
                                listUrunFiyat.Items[k].Font = new Font(listUrunFiyat.Items[k].Font.FontFamily, listUrunFiyat.Items[k].Font.Size - 0.5f, listUrunFiyat.Items[k].Font.Style);
                            }

                            while (listUrunFiyat.Columns[2].Width < System.Windows.Forms.TextRenderer.MeasureText(listUrunFiyat.Items[k].SubItems[2].Text, new Font(listUrunFiyat.Items[k].Font.FontFamily, listUrunFiyat.Items[k].Font.Size, listUrunFiyat.Items[k].Font.Style)).Width)
                            {
                                listUrunFiyat.Items[k].Font = new Font(listUrunFiyat.Items[k].Font.FontFamily, listUrunFiyat.Items[k].Font.Size - 0.5f, listUrunFiyat.Items[k].Font.Style);
                            }
                        }
                    }

                    while (listUrunFiyat.Columns[1].Width < System.Windows.Forms.TextRenderer.MeasureText(yemeginAdi, listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font).Width
                        || listUrunFiyat.Columns[2].Width < System.Windows.Forms.TextRenderer.MeasureText(((decimal)kacPorsiyon * yemeginFiyati).ToString("0.00"), listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font).Width
                        || listUrunFiyat.Columns[0].Width < System.Windows.Forms.TextRenderer.MeasureText(kacPorsiyon.ToString(), listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font).Width)
                    {
                        listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font = new Font(listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font.FontFamily, listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font.Size - 0.5f, listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font.Style);
                    }
                }
                else // varsa ürünün hesaptaki değerlerini istenilene göre arttır
                {
                    listUrunFiyat.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text = (Convert.ToDouble(listUrunFiyat.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text) + kacPorsiyon).ToString();

                    listUrunFiyat.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].SubItems[2].Text = (Convert.ToDecimal(listUrunFiyat.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].SubItems[2].Text) + (decimal)kacPorsiyon * yemeginFiyati).ToString("0.00");

                    while (listUrunFiyat.Columns[2].Width < System.Windows.Forms.TextRenderer.MeasureText(listUrunFiyat.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].SubItems[2].Text, listUrunFiyat.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].Font).Width
                        || listUrunFiyat.Columns[0].Width < System.Windows.Forms.TextRenderer.MeasureText(listUrunFiyat.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text, listUrunFiyat.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].Font).Width)
                    {
                        listUrunFiyat.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].Font = new Font(listUrunFiyat.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].Font.FontFamily, listUrunFiyat.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].Font.Size - 0.5f, listUrunFiyat.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].Font.Style);
                    }
                }

                if (!ikramMi) // ikram değilse kalan hesaba gir
                    labelKalanHesap.Text = (Convert.ToDecimal(labelKalanHesap.Text) + (decimal)kacPorsiyon * yemeginFiyati).ToString("0.00");
            }

            if (this.listUrunFiyat.Items.Count > 0)
            {
                int itemsCount = this.listUrunFiyat.Items.Count + 3;// 3 aslında grup sayısı -1
                int itemHeight = this.listUrunFiyat.Items[0].Bounds.Height;
                int VisiableItem = (int)this.listUrunFiyat.ClientRectangle.Height / itemHeight;
                if (itemsCount >= VisiableItem)
                {
                    listUrunFiyat.Columns[1].Width = urunBoyu;
                    listUrunFiyat.Columns[2].Width = fiyatBoyu;
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

                //Burada adisyonNotu'nu sql den al
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

        public void ikramGeldi(string miktar, string yemekAdi, string dusulecekDegerGelen)
        {
            double carpan = Convert.ToDouble(miktar);

            // ürünün değerini istenilen kadar azalt, kalan hesaptan düş
            double dusulecekDeger = Convert.ToDouble(dusulecekDegerGelen);

            int degisecekSiparisIndexi = -1;
            for (int i = 0; i < listUrunFiyat.Groups[eskiSiparisler].Items.Count; i++)
            {
                if (listUrunFiyat.Groups[eskiSiparisler].Items[i].SubItems[1].Text == yemekAdi)
                {
                    degisecekSiparisIndexi = i;
                    break;
                }
            }

            if (degisecekSiparisIndexi == -1)
            {
                dialog2 = new KontrolFormu("Siparişlerde değişiklik oldu, lütfen masaya tekrar giriniz", false, this);
                this.Enabled = false;
                timerDialogClose.Start();
                dialog2.Show();
                return;
            }

            listUrunFiyat.Groups[eskiSiparisler].Items[degisecekSiparisIndexi].SubItems[2].Text = (Convert.ToDouble(listUrunFiyat.Groups[eskiSiparisler].Items[degisecekSiparisIndexi].SubItems[2].Text) - carpan * dusulecekDeger).ToString("0.00");

            listUrunFiyat.Groups[eskiSiparisler].Items[degisecekSiparisIndexi].SubItems[0].Text = (Convert.ToDouble(listUrunFiyat.Groups[eskiSiparisler].Items[degisecekSiparisIndexi].SubItems[0].Text) - carpan).ToString();

            labelKalanHesap.Text = (Convert.ToDouble(labelKalanHesap.Text) - (dusulecekDeger * carpan)).ToString("0.00");

            bool ikramYok = true; // ikram  yeni ikramlar listesinde var mı diye bak 

            for (int i = 0; i < listUrunFiyat.Groups[yeniIkramlar].Items.Count; i++) // varsa yeni ikramı olana ekle
            {
                if (listUrunFiyat.Groups[yeniIkramlar].Items[i].SubItems[1].Text == yemekAdi)
                {
                    listUrunFiyat.Groups[yeniIkramlar].Items[i].Text = (Convert.ToDouble(listUrunFiyat.Groups[yeniIkramlar].Items[i].Text) + carpan).ToString();
                    listUrunFiyat.Groups[yeniIkramlar].Items[i].SubItems[2].Text = (Convert.ToDouble(listUrunFiyat.Groups[yeniIkramlar].Items[i].SubItems[2].Text) + (dusulecekDeger * carpan)).ToString("0.00");

                    ikramYok = false;
                    break;
                }
            }

            if (ikramYok) // yok yeni ikramı listeye ekle
            {
                listUrunFiyat.Items.Insert(0, carpan.ToString());
                listUrunFiyat.Items[0].SubItems.Add(yemekAdi);
                listUrunFiyat.Items[0].SubItems.Add((dusulecekDeger * carpan).ToString("0.00"));

                listUrunFiyat.Items[0].Group = listUrunFiyat.Groups[yeniIkramlar];
                listUrunFiyat.Items[0].Font = new Font("Calibri", 18.75F, FontStyle.Bold);
                listedeSeciliOlanItemlar.Insert(0, false);
            }

            if (listUrunFiyat.Groups[eskiSiparisler].Items[degisecekSiparisIndexi].Text == "0")
            {
                listedeSeciliOlanItemlar.RemoveAt(listUrunFiyat.Groups[eskiSiparisler].Items[degisecekSiparisIndexi].Index);
                listUrunFiyat.Groups[eskiSiparisler].Items[degisecekSiparisIndexi].Remove();
            }

            buttonTemizle_Click(null, null);
            this.Enabled = true;
        }

        public void ikramIptaliGeldi(string miktar, string yemekAdi, string dusulecekDegerGelen, string ikramYeniMiEskiMiGelen)
        {
            int ikramYeniMiEskiMi = Convert.ToInt32(ikramYeniMiEskiMiGelen);

            double carpan = Convert.ToDouble(miktar);

            // ürünün değerini bul ve hesaba ekle
            double dusulecekDeger = Convert.ToDouble(dusulecekDegerGelen);

            int degisecekSiparisIndexi = -1;
            for (int i = 0; i < listUrunFiyat.Groups[ikramYeniMiEskiMi].Items.Count; i++)
            {
                if (listUrunFiyat.Groups[ikramYeniMiEskiMi].Items[i].SubItems[1].Text == yemekAdi)
                {
                    degisecekSiparisIndexi = i;
                    break;
                }
            }

            if (degisecekSiparisIndexi == -1)
            {
                dialog2 = new KontrolFormu("Siparişlerde değişiklik oldu, lütfen masaya tekrar giriniz", false, this);

                timerDialogClose.Start();
                this.Enabled = false;
                dialog2.Show();
                return;
            }

            listUrunFiyat.Groups[ikramYeniMiEskiMi].Items[degisecekSiparisIndexi].SubItems[2].Text = (Convert.ToDouble(listUrunFiyat.Groups[ikramYeniMiEskiMi].Items[degisecekSiparisIndexi].SubItems[2].Text) - dusulecekDeger * carpan).ToString("0.00");

            listUrunFiyat.Groups[ikramYeniMiEskiMi].Items[degisecekSiparisIndexi].SubItems[0].Text = (Convert.ToDouble(listUrunFiyat.Groups[ikramYeniMiEskiMi].Items[degisecekSiparisIndexi].SubItems[0].Text) - carpan).ToString();

            labelKalanHesap.Text = (Convert.ToDouble(labelKalanHesap.Text) + (dusulecekDeger * carpan)).ToString("0.00");

            bool urunYok = true;

            for (int i = 0; i < listUrunFiyat.Groups[eskiSiparisler].Items.Count; i++)
            {
                if (listUrunFiyat.Groups[eskiSiparisler].Items[i].SubItems[1].Text == yemekAdi)
                {
                    listUrunFiyat.Groups[eskiSiparisler].Items[i].Text = (Convert.ToDouble(listUrunFiyat.Groups[eskiSiparisler].Items[i].Text) + carpan).ToString();
                    listUrunFiyat.Groups[eskiSiparisler].Items[i].SubItems[2].Text = (Convert.ToDouble(listUrunFiyat.Groups[eskiSiparisler].Items[i].SubItems[2].Text) + (dusulecekDeger * carpan)).ToString("0.00");

                    urunYok = false;
                    break;
                }
            }

            if (urunYok)
            {
                listUrunFiyat.Items.Add(carpan.ToString());
                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(yemekAdi);
                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add((dusulecekDeger * carpan).ToString("0.00"));

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
            double carpan;
            if (textNumberOfItem.Text != "")
                carpan = Convert.ToDouble(textNumberOfItem.Text);
            else
                carpan = 1;

            if (carpan == 0)
                return;

            if (carpan > Convert.ToDouble(listUrunFiyat.SelectedItems[0].SubItems[0].Text))
                carpan = Convert.ToDouble(listUrunFiyat.SelectedItems[0].SubItems[0].Text);

            double dusulecekDeger = Convert.ToDouble(listUrunFiyat.SelectedItems[0].SubItems[2].Text) / Convert.ToDouble(listUrunFiyat.SelectedItems[0].SubItems[0].Text);

            if (Properties.Settings.Default.Server == 2) //server - diğer tüm clientlara söylemeli yaptığı ikram vs. neyse
            {
                // ikram et
                if (buttonUrunIkram.Text == "  İkram")
                {
                    decimal istenilenikramSayisi = (decimal)carpan;

                    // ürünün değerini istenilen kadar azalt, kalan hesaptan düş
                    listUrunFiyat.SelectedItems[0].SubItems[2].Text = (Convert.ToDouble(listUrunFiyat.SelectedItems[0].SubItems[2].Text) - carpan * dusulecekDeger).ToString("0.00");

                    listUrunFiyat.SelectedItems[0].SubItems[0].Text = (Convert.ToDouble(listUrunFiyat.SelectedItems[0].SubItems[0].Text) - carpan).ToString();

                    labelKalanHesap.Text = (Convert.ToDouble(labelKalanHesap.Text) - (dusulecekDeger * carpan)).ToString("0.00");

                    bool ikramYok = true; // ikram  yeni ikramlar listesinde var mı diye bak 

                    for (int i = 0; i < listUrunFiyat.Groups[yeniIkramlar].Items.Count; i++) // varsa yeni ikramı olana ekle
                    {
                        if (listUrunFiyat.Groups[yeniIkramlar].Items[i].SubItems[1].Text == listUrunFiyat.SelectedItems[0].SubItems[1].Text)
                        {
                            listUrunFiyat.Groups[yeniIkramlar].Items[i].Text = (Convert.ToDouble(listUrunFiyat.Groups[yeniIkramlar].Items[i].Text) + carpan).ToString();
                            listUrunFiyat.Groups[yeniIkramlar].Items[i].SubItems[2].Text = (Convert.ToDouble(listUrunFiyat.Groups[yeniIkramlar].Items[i].SubItems[2].Text) + (dusulecekDeger * carpan)).ToString("0.00");

                            ikramYok = false;
                            break;
                        }
                    }

                    if (ikramYok) // yok yeni ikramı listeye ekle
                    {
                        listUrunFiyat.Items.Insert(0, carpan.ToString());
                        listUrunFiyat.Items[0].SubItems.Add(listUrunFiyat.SelectedItems[0].SubItems[1].Text);
                        listUrunFiyat.Items[0].SubItems.Add((dusulecekDeger * carpan).ToString("0.00"));

                        listUrunFiyat.Items[0].Group = listUrunFiyat.Groups[yeniIkramlar];
                        listUrunFiyat.Items[0].Font = new Font("Calibri", 18.75F, FontStyle.Bold);
                        listedeSeciliOlanItemlar.Insert(0, false);
                    }

                    //İkram edildiğinde önce hangi grupta olduğuna bakılacak, eğer yeni eklenenler grubunda (yani 3 indeksli grup) ise SQL o grubu update etmeye gerek yok direk ikram kaldırılacak
                    //Eğer eskiden ekli olanlarda ise (yani 1 indeksli grup) ikram adedine ulaşana kadar update yaparak sipariş sayısını azaltacaz
                    //Eğer iptal edilen adedi tam olarak siparişlerde bulunamazsa örneğin 4 iptal var 2 tane 3 adetlik sipariş var yani toplam 6
                    //İlk gelen siparişin ikram özelliği true(1) yapılacak diğerinin adedi update edilerek azaltılacak ikramın kalanı kadarıyla yeni ikram siparişi oluşturulacak


                    SqlCommand cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adisyon.AdisyonID,Porsiyon,Siparis.VerilisTarihi FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi=0 AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + MasaAdi + "' AND Adisyon.DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND Siparis.YemekAdi='" + listUrunFiyat.SelectedItems[0].SubItems[1].Text + "' AND Siparis.Garsonu='" + siparisiKimGirdi + "' ORDER BY Porsiyon DESC");

                    SqlDataReader dr = cmd.ExecuteReader();

                    int siparisID, adisyonID = 0;
                    decimal porsiyon;
                    DateTime verilisTarihi;

                    while (dr.Read())
                    {
                        try
                        {
                            siparisID = dr.GetInt32(0);
                            adisyonID = dr.GetInt32(1);
                            porsiyon = dr.GetDecimal(2);
                            verilisTarihi = dr.GetDateTime(3);
                        }
                        catch
                        {
                            KontrolFormu dialog = new KontrolFormu("Ürünü ikram ederken bir hata oluştu, lütfen tekrar deneyiniz", false);
                            dialog.Show();
                            return;
                        }

                        if (porsiyon < istenilenikramSayisi) // elimizde ikram edilmemişler ikramı istenenden küçükse
                        {
                            ikramUpdateTam(siparisID, 1);

                            istenilenikramSayisi -= porsiyon;
                        }
                        else if (porsiyon > istenilenikramSayisi) // den büyükse
                        {
                            ikramUpdateInsert(siparisID, adisyonID, porsiyon, dusulecekDeger, istenilenikramSayisi, listUrunFiyat.SelectedItems[0].SubItems[1].Text, 1, verilisTarihi);

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
                        cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Porsiyon,Siparis.VerilisTarihi,Adisyon.AdisyonID FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi=0 AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + MasaAdi + "' AND Adisyon.DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND Siparis.YemekAdi='" + listUrunFiyat.SelectedItems[0].SubItems[1].Text + "' AND Siparis.Garsonu!='" + siparisiKimGirdi + "' ORDER BY Porsiyon DESC");

                        dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            try
                            {
                                siparisID = dr.GetInt32(0);
                                porsiyon = dr.GetDecimal(1);
                                verilisTarihi = dr.GetDateTime(2);
                                adisyonID = dr.GetInt32(3);
                            }
                            catch
                            {
                                KontrolFormu dialog = new KontrolFormu("Ürünü ikram ederken bir hata oluştu, lütfen tekrar deneyiniz", false);
                                dialog.Show();
                                return;
                            }

                            if (porsiyon < istenilenikramSayisi) // elimizde ikram edilmemişler ikramı istenenden küçükse
                            {
                                ikramUpdateTam(siparisID, 1);

                                istenilenikramSayisi -= porsiyon;
                            }
                            else if (porsiyon > istenilenikramSayisi) // den büyükse
                            {
                                ikramUpdateInsert(siparisID, adisyonID, porsiyon, dusulecekDeger, istenilenikramSayisi, listUrunFiyat.SelectedItems[0].SubItems[1].Text, 1, verilisTarihi);

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

                    masaFormu.serverdanSiparisIkramVeyaIptal(MasaAdi, hangiDepartman.departmanAdi, "ikram", carpan.ToString(), listUrunFiyat.SelectedItems[0].SubItems[1].Text, dusulecekDeger.ToString(), null);

                    if (listUrunFiyat.SelectedItems[0].Text == "0")
                    {
                        listedeSeciliOlanItemlar.RemoveAt(listUrunFiyat.SelectedItems[0].Index);
                        listUrunFiyat.SelectedItems[0].Remove();
                    }
                }
                else // ikramı iptal et
                {
                    decimal istenilenIkramiptalSayisi = (decimal)carpan;
                    // ürünün değerini bul ve hesaba ekle
                    listUrunFiyat.SelectedItems[0].SubItems[2].Text = (Convert.ToDouble(listUrunFiyat.SelectedItems[0].SubItems[2].Text) - dusulecekDeger * carpan).ToString("0.00");

                    listUrunFiyat.SelectedItems[0].SubItems[0].Text = (Convert.ToDouble(listUrunFiyat.SelectedItems[0].SubItems[0].Text) - carpan).ToString();

                    labelKalanHesap.Text = (Convert.ToDouble(labelKalanHesap.Text) + (dusulecekDeger * carpan)).ToString("0.00");

                    bool urunYok = true;

                    for (int i = 0; i < listUrunFiyat.Groups[eskiSiparisler].Items.Count; i++)
                    {
                        if (listUrunFiyat.Groups[eskiSiparisler].Items[i].SubItems[1].Text == listUrunFiyat.SelectedItems[0].SubItems[1].Text)
                        {
                            listUrunFiyat.Groups[eskiSiparisler].Items[i].Text = (Convert.ToDouble(listUrunFiyat.Groups[eskiSiparisler].Items[i].Text) + carpan).ToString();
                            listUrunFiyat.Groups[eskiSiparisler].Items[i].SubItems[2].Text = (Convert.ToDouble(listUrunFiyat.Groups[eskiSiparisler].Items[i].SubItems[2].Text) + (dusulecekDeger * carpan)).ToString("0.00");

                            urunYok = false;
                            break;
                        }
                    }

                    if (urunYok)
                    {
                        listUrunFiyat.Items.Add(carpan.ToString());
                        listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(listUrunFiyat.SelectedItems[0].SubItems[1].Text);
                        listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add((dusulecekDeger * carpan).ToString("0.00"));

                        listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Group = listUrunFiyat.Groups[eskiSiparisler];
                        listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font = new Font("Calibri", 18.75F, FontStyle.Bold);
                        listedeSeciliOlanItemlar.Add(false);
                    }

                    //İkram iptal edildiğinde önce hangi grupta olduğuna bakılacak, eğer yeni eklenenler grubunda (yani 2. grup) ise SQL de o grubu update etmeye gerek yok
                    //Eğer eskiden ekli olanlarda ise ikram adedine ulaşana kadar update yaparak sipariş sayısını azaltacaz
                    //Eğer iptal edilen adedi tam olarak siparişlerde bulunamazsa örneğin 4 iptal var 2 tane 3 adetlik sipariş var yani toplam 6
                    //İlk gelen siparişin ikram özelliği true(1) yapılacak diğerinin adedi update edilerek azaltılacak ikramın kalanı kadarıyla yeni ikram siparişi oluşturulacak


                    SqlCommand cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adisyon.AdisyonID,Porsiyon,Siparis.VerilisTarihi FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi=1 AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + MasaAdi + "' AND Adisyon.DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND Siparis.YemekAdi='" + listUrunFiyat.SelectedItems[0].SubItems[1].Text + "' AND Siparis.Garsonu='" + siparisiKimGirdi + "' ORDER BY Porsiyon DESC");

                    SqlDataReader dr = cmd.ExecuteReader();

                    int siparisID, adisyonID = 0;
                    decimal porsiyon;
                    DateTime verilisTarihi;

                    while (dr.Read())
                    {
                        try
                        {
                            siparisID = dr.GetInt32(0);
                            adisyonID = dr.GetInt32(1);
                            porsiyon = dr.GetDecimal(2);
                            verilisTarihi = dr.GetDateTime(3);
                        }
                        catch
                        {
                            KontrolFormu dialog = new KontrolFormu("İkramı iptal ederken bir hata oluştu, lütfen tekrar deneyiniz", false);
                            dialog.Show();
                            return;
                        }

                        if (porsiyon < istenilenIkramiptalSayisi) // elimizdeki ikramlar iptali istenenden küçükse
                        {
                            ikramUpdateTam(siparisID, 0);

                            istenilenIkramiptalSayisi -= porsiyon;
                        }
                        else if (porsiyon > istenilenIkramiptalSayisi) // den büyükse
                        {
                            ikramUpdateInsert(siparisID, adisyonID, porsiyon, dusulecekDeger, istenilenIkramiptalSayisi, listUrunFiyat.SelectedItems[0].SubItems[1].Text, 0, verilisTarihi);

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
                        cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Porsiyon,Siparis.VerilisTarihi,Adisyon.AdisyonID FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi=0 AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + MasaAdi + "' AND Adisyon.DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND Siparis.YemekAdi='" + listUrunFiyat.SelectedItems[0].SubItems[1].Text + "' AND Siparis.Garsonu!='" + siparisiKimGirdi + "' ORDER BY Porsiyon DESC");

                        dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            try
                            {
                                siparisID = dr.GetInt32(0);
                                porsiyon = dr.GetDecimal(1);
                                verilisTarihi = dr.GetDateTime(2);
                                adisyonID = dr.GetInt32(3);
                            }
                            catch
                            {
                                KontrolFormu dialog = new KontrolFormu("İkramı iptal ederken bir hata oluştu, lütfen tekrar deneyiniz", false);
                                dialog.Show();
                                return;
                            }

                            if (porsiyon < istenilenIkramiptalSayisi) // elimizde ikram edilmemişler ikramı istenenden küçükse
                            {
                                ikramUpdateTam(siparisID, 0);

                                istenilenIkramiptalSayisi -= porsiyon;
                            }
                            else if (porsiyon > istenilenIkramiptalSayisi) // den büyükse
                            {
                                ikramUpdateInsert(siparisID, adisyonID, porsiyon, dusulecekDeger, istenilenIkramiptalSayisi, listUrunFiyat.SelectedItems[0].SubItems[1].Text, 0, verilisTarihi);

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

                    masaFormu.serverdanSiparisIkramVeyaIptal(MasaAdi, hangiDepartman.departmanAdi, "ikramIptal", carpan.ToString(), listUrunFiyat.SelectedItems[0].SubItems[1].Text, dusulecekDeger.ToString(), listUrunFiyat.SelectedItems[0].Group.Tag.ToString());

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
                if (buttonUrunIkram.Text == "  İkram")
                {
                    masaFormu.menuFormundanServeraSiparisYolla(MasaAdi, hangiDepartman.departmanAdi, "ikram", carpan.ToString(), listUrunFiyat.SelectedItems[0].SubItems[1].Text, siparisiKimGirdi, dusulecekDeger.ToString(), adisyonNotu, null);
                }
                else // ikramı iptal et
                {
                    masaFormu.menuFormundanServeraSiparisYolla(MasaAdi, hangiDepartman.departmanAdi, "ikramIptal", carpan.ToString(), listUrunFiyat.SelectedItems[0].SubItems[1].Text, siparisiKimGirdi, dusulecekDeger.ToString(), adisyonNotu, listUrunFiyat.SelectedItems[0].Group.Tag.ToString());
                }
                this.Enabled = false;
            }
        }

        public void iptalGeldi(string miktar, string yemekAdi, string dusulecekDegerGelen, string ikramMiGelen)
        {
            int ikraminGrubu = Convert.ToInt32(ikramMiGelen);

            double carpan = Convert.ToDouble(miktar);

            double dusulecekDeger = Convert.ToDouble(dusulecekDegerGelen);

            int degisecekSiparisIndexi = -1;

            for (int i = 0; i < listUrunFiyat.Groups[ikraminGrubu].Items.Count; i++)
            {
                if (listUrunFiyat.Groups[ikraminGrubu].Items[i].SubItems[1].Text == yemekAdi)
                {
                    degisecekSiparisIndexi = i;
                    break;
                }
            }

            if (degisecekSiparisIndexi == -1)
            {
                dialog2 = new KontrolFormu("Siparişlerde değişiklik oldu, lütfen masaya tekrar giriniz", false, this);
                timerDialogClose.Start();
                this.Enabled = false;
                dialog2.Show();
                return;

            }

            listUrunFiyat.Groups[ikraminGrubu].Items[degisecekSiparisIndexi].SubItems[0].Text = (Convert.ToDouble(listUrunFiyat.Groups[ikraminGrubu].Items[degisecekSiparisIndexi].SubItems[0].Text) - carpan).ToString();

            listUrunFiyat.Groups[ikraminGrubu].Items[degisecekSiparisIndexi].SubItems[2].Text = (Convert.ToDouble(listUrunFiyat.Groups[ikraminGrubu].Items[degisecekSiparisIndexi].SubItems[2].Text) - dusulecekDeger * carpan).ToString("0.00");

            if (ikraminGrubu == 2) // iptali istenilen ürün ikram değilse kalan hesaptan da düşülmeli
            {
                labelKalanHesap.Text = (Convert.ToDouble(labelKalanHesap.Text) - (dusulecekDeger * carpan)).ToString("0.00");
            }

            if (listUrunFiyat.Groups[ikraminGrubu].Items[degisecekSiparisIndexi].Text == "0")
            {
                listedeSeciliOlanItemlar.RemoveAt(listUrunFiyat.Groups[ikraminGrubu].Items[degisecekSiparisIndexi].Index);
                listUrunFiyat.Groups[ikraminGrubu].Items[degisecekSiparisIndexi].Remove();
            }

            if (this.listUrunFiyat.Items.Count > 0)
            {
                int itemsCount = this.listUrunFiyat.Items.Count + 3;// 3 aslında grup sayısı -1
                int itemHeight = this.listUrunFiyat.Items[0].Bounds.Height;
                int VisiableItem = (int)this.listUrunFiyat.ClientRectangle.Height / itemHeight;
                if (itemsCount < VisiableItem)
                {
                    listUrunFiyat.Columns[1].Width = urunBoyu + 10;
                    listUrunFiyat.Columns[2].Width = fiyatBoyu + 10;
                }
            }

            buttonTemizle_Click(null, null);
            this.Enabled = true;
        }

        // ürün iptal etme butonu
        private void buttonUrunIptal_Click(object sender, EventArgs e)
        {
            double carpan;
            if (textNumberOfItem.Text != "")
            {
                carpan = Convert.ToDouble(textNumberOfItem.Text);
                if (carpan > Convert.ToDouble(listUrunFiyat.SelectedItems[0].SubItems[0].Text))
                    carpan = Convert.ToDouble(listUrunFiyat.SelectedItems[0].SubItems[0].Text);
            }
            else
                carpan = Convert.ToDouble(listUrunFiyat.SelectedItems[0].SubItems[0].Text);

            if (carpan == 0)
                return;

            DialogResult eminMisiniz;

            using (KontrolFormu dialog = new KontrolFormu(carpan + " adet " + listUrunFiyat.SelectedItems[0].SubItems[1].Text + " iptal edilecek. Emin misiniz?", true))
            {
                eminMisiniz = dialog.ShowDialog();
            }

            if (eminMisiniz == DialogResult.No)
            {
                return;
            }

            double dusulecekDeger = Convert.ToDouble(listUrunFiyat.SelectedItems[0].SubItems[2].Text) / Convert.ToDouble(listUrunFiyat.SelectedItems[0].SubItems[0].Text);

            if (Properties.Settings.Default.Server == 2) //server - diğer tüm clientlara söylemeli yaptığı ikram vs. neyse
            {
                listUrunFiyat.SelectedItems[0].SubItems[0].Text = (Convert.ToDouble(listUrunFiyat.SelectedItems[0].SubItems[0].Text) - carpan).ToString();

                listUrunFiyat.SelectedItems[0].SubItems[2].Text = (Convert.ToDouble(listUrunFiyat.SelectedItems[0].SubItems[2].Text) - dusulecekDeger * carpan).ToString("0.00");

                if (listUrunFiyat.SelectedItems[0].Group == listUrunFiyat.Groups[2] || listUrunFiyat.SelectedItems[0].Group == listUrunFiyat.Groups[3])
                {
                    labelKalanHesap.Text = (Convert.ToDouble(labelKalanHesap.Text) - (dusulecekDeger * carpan)).ToString("0.00");
                }

                if (listUrunFiyat.SelectedItems[0].Group != listUrunFiyat.Groups[yeniSiparisler])
                {
                    decimal istenilenSiparisiptalSayisi = (decimal)carpan;

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

                    cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adisyon.AdisyonID,Porsiyon,Siparis.VerilisTarihi FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi='" + ikramSQLGirdisi + "' AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + MasaAdi + "' AND Adisyon.DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND Siparis.YemekAdi='" + listUrunFiyat.SelectedItems[0].SubItems[1].Text + "' AND Siparis.Garsonu='" + siparisiKimGirdi + "' ORDER BY Porsiyon DESC");

                    SqlDataReader dr = cmd.ExecuteReader();

                    int siparisID, adisyonID = 0;
                    decimal porsiyon;
                    DateTime verilisTarihi;

                    while (dr.Read())
                    {
                        try
                        {
                            siparisID = dr.GetInt32(0);
                            adisyonID = dr.GetInt32(1);
                            porsiyon = dr.GetDecimal(2);
                            verilisTarihi = dr.GetDateTime(3);
                        }
                        catch
                        {
                            KontrolFormu dialog = new KontrolFormu("Ürünü iptal ederken bir hata oluştu, lütfen tekrar deneyiniz", false);
                            dialog.Show();
                            return;
                        }

                        if (porsiyon < istenilenSiparisiptalSayisi) // elimizdeki siparişler iptali istenenden küçükse
                        {
                            iptalUpdateTam(siparisID);

                            istenilenSiparisiptalSayisi -= porsiyon;
                        }
                        else if (porsiyon > istenilenSiparisiptalSayisi) // den büyükse
                        {
                            iptalUpdateInsert(siparisID, adisyonID, porsiyon, dusulecekDeger, istenilenSiparisiptalSayisi, listUrunFiyat.SelectedItems[0].SubItems[1].Text, verilisTarihi);

                            istenilenSiparisiptalSayisi = 0;
                        }
                        else // elimizdeki siparişler iptali istenene eşitse
                        {
                            iptalUpdateTam(siparisID);

                            istenilenSiparisiptalSayisi = 0;
                        }
                    }

                    if (istenilenSiparisiptalSayisi != 0)// iptal edilecekler daha bitmedi başka garsonların siparişlerinden iptale devam et
                    {
                        cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Porsiyon,Siparis.VerilisTarihi,Adisyon.AdisyonID FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi='" + ikramSQLGirdisi + "' AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + MasaAdi + "' AND Adisyon.DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND Siparis.YemekAdi='" + listUrunFiyat.SelectedItems[0].SubItems[1].Text + "' AND Siparis.Garsonu!='" + siparisiKimGirdi + "' ORDER BY Porsiyon DESC");

                        dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            try
                            {
                                siparisID = dr.GetInt32(0);
                                porsiyon = dr.GetDecimal(1);
                                verilisTarihi = dr.GetDateTime(2);
                                adisyonID = dr.GetInt32(3);
                            }
                            catch
                            {
                                KontrolFormu dialog = new KontrolFormu("Ürünü iptal ederken bir hata oluştu, lütfen tekrar deneyiniz", false);
                                dialog.Show();
                                return;
                            }

                            if (porsiyon < istenilenSiparisiptalSayisi) // elimizdeki siparişler iptali istenenden küçükse
                            {
                                iptalUpdateTam(siparisID);

                                istenilenSiparisiptalSayisi -= porsiyon;
                            }
                            else if (porsiyon > istenilenSiparisiptalSayisi) // den büyükse
                            {
                                iptalUpdateInsert(siparisID, adisyonID, porsiyon, dusulecekDeger, istenilenSiparisiptalSayisi, listUrunFiyat.SelectedItems[0].SubItems[1].Text, verilisTarihi);

                                istenilenSiparisiptalSayisi = 0;
                            }
                            else // elimizdeki siparişler iptali istenene eşitse
                            {
                                iptalUpdateTam(siparisID);

                                istenilenSiparisiptalSayisi = 0;
                            }
                        }
                    }

                    adisyonNotuUpdate(adisyonID);

                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                }

                masaFormu.serverdanSiparisIkramVeyaIptal(MasaAdi, hangiDepartman.departmanAdi, "iptal", carpan.ToString(), listUrunFiyat.SelectedItems[0].SubItems[1].Text, dusulecekDeger.ToString(), listUrunFiyat.SelectedItems[0].Group.Tag.ToString());

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
                        listUrunFiyat.Columns[1].Width = urunBoyu + 10;
                        listUrunFiyat.Columns[2].Width = fiyatBoyu + 10;
                    }
                }

                buttonTemizle_Click(null, null);
            }
            else //client 
            {
                if (listUrunFiyat.SelectedItems[0].Group == listUrunFiyat.Groups[yeniSiparisler]) //eğer sipariş yeni verilenlerdense henüz sisteme girişi yapılmamış demektir
                {
                    listUrunFiyat.SelectedItems[0].SubItems[0].Text = (Convert.ToDouble(listUrunFiyat.SelectedItems[0].SubItems[0].Text) - carpan).ToString();

                    listUrunFiyat.SelectedItems[0].SubItems[2].Text = (Convert.ToDouble(listUrunFiyat.SelectedItems[0].SubItems[2].Text) - dusulecekDeger * carpan).ToString("0.00");

                    if (listUrunFiyat.SelectedItems[0].Group == listUrunFiyat.Groups[2] || listUrunFiyat.SelectedItems[0].Group == listUrunFiyat.Groups[3])
                    {
                        labelKalanHesap.Text = (Convert.ToDouble(labelKalanHesap.Text) - (dusulecekDeger * carpan)).ToString("0.00");
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
                            listUrunFiyat.Columns[1].Width = urunBoyu + 10;
                            listUrunFiyat.Columns[2].Width = fiyatBoyu + 10;
                        }
                    }

                    buttonTemizle_Click(null, null);
                }
                else // yeni sipariş değilse sisteme girişi yapılmıştır ve diğer makinalara bilgi verilmelidir
                {
                    string ikramMi = listUrunFiyat.SelectedItems[0].Group.Tag.ToString();

                    masaFormu.menuFormundanServeraSiparisYolla(MasaAdi, hangiDepartman.departmanAdi, "iptal", carpan.ToString(), listUrunFiyat.SelectedItems[0].SubItems[1].Text, siparisiKimGirdi, dusulecekDeger.ToString(), adisyonNotu, ikramMi);
                    this.Enabled = false;
                }
            }
        }

        // sipariş işlemleri bittiğinde basılan buton
        private void buttonTamam_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Server == 2) //server - diğer tüm clientlara söylemeli yaptığı ikram vs. neyse
            {
                SqlCommand cmd;

                if (listUrunFiyat.Groups[2].Items.Count == 0 && listUrunFiyat.Groups[3].Items.Count == 0) // listede hiç sipariş yoksa, siparişler ya ödenmiştir yada iptal edilmiştir
                {
                    cmd = SQLBaglantisi.getCommand("SELECT OdenenMiktar from OdemeDetay JOIN Adisyon ON OdemeDetay.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.MasaAdi='" + MasaAdi + "' AND Adisyon.DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND Adisyon.AcikMi=1 AND Adisyon.IptalMi=0");
                    SqlDataReader dr = cmd.ExecuteReader();
                    dr.Read();

                    try // eğer masanın ödenmiş siparişi varsa hesabı kapat 
                    {
                        dr.GetDecimal(0);

                        cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET OdendiMi=1 WHERE AdisyonID=(SELECT AdisyonID FROM Adisyon WHERE AcikMi=1 AND MasaAdi='" + MasaAdi + "' AND DepartmanAdi='" + hangiDepartman.departmanAdi + "')");
                        cmd.ExecuteNonQuery();
                        cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET AcikMi=0,KapanisZamani=@date WHERE AdisyonID=(SELECT AdisyonID FROM Adisyon WHERE AcikMi=1 AND MasaAdi='" + MasaAdi + "' AND DepartmanAdi='" + hangiDepartman.departmanAdi + "')");
                        cmd.Parameters.AddWithValue("@date", DateTime.Now);
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
                        cmd.Connection.Close();
                        cmd.Connection.Dispose();
                        this.Close();
                        return;
                    }
                    masaAcikMi = false;

                    this.Close();
                }
                else
                {
                    int adisyonID;

                    cmd = SQLBaglantisi.getCommand("SELECT AdisyonID FROM Adisyon WHERE MasaAdi='" + MasaAdi + "' AND DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND AcikMi=1");
                    SqlDataReader dr = cmd.ExecuteReader();
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

                    bool mutfakAdisyonuYazdir = false;

                    foreach (ListViewItem siparis in listUrunFiyat.Groups[yeniSiparisler].Items)
                    {
                        mutfakAdisyonuYazdir = true;
                        siparisOlustur(adisyonID, siparis);

                        masaFormu.serverdanSiparisIkramVeyaIptal(MasaAdi, hangiDepartman.departmanAdi, "siparis", siparis.SubItems[0].Text, siparis.SubItems[1].Text, (Convert.ToDecimal(siparis.SubItems[2].Text) / Convert.ToDecimal(siparis.SubItems[0].Text)).ToString(), null);
                    }

                    //burada mutfak adisyonu iste 

                    if (mutfakAdisyonuYazdir)
                    {
                        cmd = SQLBaglantisi.getCommand("SELECT FirmaAdi,Yazici FROM Yazici WHERE YaziciAdi LIKE 'Mutfak%'");
                        dr = cmd.ExecuteReader();

                        dr.Read();

                        string firmaAdi = dr.GetString(0), yaziciAdi = dr.GetString(1);

                        cmd.Connection.Close();
                        cmd.Connection.Dispose();

                        asyncYaziciyaGonder(MasaAdi, hangiDepartman.departmanAdi, firmaAdi, yaziciAdi, raporMutfak);
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
                    int sonSiparisMi = listUrunFiyat.Groups[yeniSiparisler].Items.Count;

                    foreach (ListViewItem siparis in listUrunFiyat.Groups[yeniSiparisler].Items)
                    {
                        sonSiparisMi--;
                        masaFormu.serveraSiparis(MasaAdi, hangiDepartman.departmanAdi, "siparis", siparis.SubItems[0].Text, siparis.SubItems[1].Text, siparisiKimGirdi, (Convert.ToDecimal(siparis.SubItems[2].Text) / Convert.ToDecimal(siparis.SubItems[0].Text)).ToString(), adisyonNotu,sonSiparisMi);
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

        public Thread asyncYaziciyaGonder(string masaAdi, string departmanAdi, string firmaAdi, string printerAdi, CrystalReportMutfak rapor)
        {
            var t = new Thread(() => Basla(masaAdi, departmanAdi, firmaAdi, printerAdi, rapor));
            t.Start();
            return t;
        }

        private static void Basla(string masaAdi, string departmanAdi, string firmaAdi, string printerAdi, CrystalReportMutfak rapor)
        {
            rapor.SetParameterValue("Masa", masaAdi);
            rapor.SetParameterValue("Departman", departmanAdi);
            rapor.SetParameterValue("FirmaAdi", firmaAdi); // firma adı
            rapor.PrintOptions.PrinterName = printerAdi;
            rapor.PrintToPrinter(1, false, 0, 0);

            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET MutfakCiktisiAlindiMi=1 WHERE AdisyonID=(SELECT AdisyonID FROM Adisyon WHERE AcikMi=1 AND MasaAdi=@masaninAdi AND DepartmanAdi=@departmanAdi)");
            cmd.Parameters.AddWithValue("@masaninAdi", masaAdi);
            cmd.Parameters.AddWithValue("@departmanAdi", departmanAdi);

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        private void masaAktarmaIslemlerindenSonraCik(string yeniMasaAdi, string yeniDepartmanAdi)
        {
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

                bool mutfakAdisyonuYazdir = false;

                foreach (ListViewItem siparis in listUrunFiyat.Groups[yeniSiparisler].Items)
                {
                    mutfakAdisyonuYazdir = true;

                    siparisOlustur(adisyonID, siparis);

                    masaFormu.serverdanSiparisIkramVeyaIptal(yeniMasaAdi, yeniDepartmanAdi, "siparis", siparis.SubItems[0].Text, siparis.SubItems[1].Text, (Convert.ToDecimal(siparis.SubItems[2].Text) / Convert.ToDecimal(siparis.SubItems[0].Text)).ToString(), null);
                }

                //burada mutfak adisyonu iste 

                if (mutfakAdisyonuYazdir)
                {
                    cmd = SQLBaglantisi.getCommand("SELECT FirmaAdi,Yazici FROM Yazici WHERE YaziciAdi LIKE 'Mutfak%'");
                    dr = cmd.ExecuteReader();

                    dr.Read();

                    string firmaAdi = dr.GetString(0), yaziciAdi = dr.GetString(1);

                    cmd.Connection.Close();
                    cmd.Connection.Dispose();

                    asyncYaziciyaGonder(MasaAdi, hangiDepartman.departmanAdi, firmaAdi, yaziciAdi, raporMutfak);
                }

                if (adisyonNotuGuncellenmeliMi) // eğer sipariş notuna dokunulmuşsa not update edilsin
                {
                    adisyonNotuUpdate(adisyonID);
                }
                this.Close();
            }
            else //client
            {
                int sonSiparisMi = listUrunFiyat.Groups[3].Items.Count;

                foreach (ListViewItem siparis in listUrunFiyat.Groups[yeniSiparisler].Items)
                {
                    sonSiparisMi--;

                    masaFormu.serveraSiparis(yeniMasaAdi, yeniDepartmanAdi, "siparis", siparis.SubItems[0].Text, siparis.SubItems[1].Text, siparisiKimGirdi, (Convert.ToDecimal(siparis.SubItems[2].Text) / Convert.ToDecimal(siparis.SubItems[0].Text)).ToString(), adisyonNotu, sonSiparisMi);
                    adisyonNotuGuncellenmeliMi = false;
                }

                if (adisyonNotuGuncellenmeliMi)
                {
                    masaFormu.serveraNotuYolla(yeniMasaAdi, yeniDepartmanAdi, "adisyonNotunuGuncelle", adisyonNotu);
                }
                this.Close();
            }
        }

        //her yeni gelen sipariş için çalışan kısım
        public void siparisOnayiGeldi(string miktar, string yemekAdi, string fiyatGelen)
        {
            int gruptaYeniGelenSiparisVarmi = -1; //ürün cinsi hesapta var mı bak 
            for (int i = 0; i < listUrunFiyat.Groups[eskiSiparisler].Items.Count; i++)
            {
                if (yemekAdi == listUrunFiyat.Groups[eskiSiparisler].Items[i].SubItems[1].Text)
                {
                    gruptaYeniGelenSiparisVarmi = i;
                    break;
                }
            }

            decimal kacPorsiyon = Convert.ToDecimal(miktar);

            if (gruptaYeniGelenSiparisVarmi == -1) //yoksa ürünü hesaba ekle
            {
                listUrunFiyat.Items.Add(miktar);
                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add(yemekAdi);
                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].SubItems.Add((kacPorsiyon * Convert.ToDecimal(fiyatGelen)).ToString("0.00"));
                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Group = listUrunFiyat.Groups[eskiSiparisler];
                listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font = new Font("Calibri", 18.75F, FontStyle.Bold);
                listedeSeciliOlanItemlar.Add(false);

                //eğer ürün eklendiğinde ekrana sığmıyorsa scroll gösterilecektir, bu yüzden fiyatları sola kaydırıyoruz
                int itemsCount = this.listUrunFiyat.Items.Count + 3;// 3 aslında grup sayısı -1
                int itemHeight = this.listUrunFiyat.Items[0].Bounds.Height;
                int VisiableItem = (int)this.listUrunFiyat.ClientRectangle.Height / itemHeight;

                if (itemsCount >= VisiableItem)
                {
                    listUrunFiyat.Columns[1].Width = urunBoyu;
                    listUrunFiyat.Columns[2].Width = fiyatBoyu;

                    for (int i = 0; i < listUrunFiyat.Items.Count; i++)
                    {
                        while (listUrunFiyat.Columns[0].Width < System.Windows.Forms.TextRenderer.MeasureText(listUrunFiyat.Items[i].SubItems[0].Text, new Font(listUrunFiyat.Items[i].Font.FontFamily, listUrunFiyat.Items[i].Font.Size, listUrunFiyat.Items[i].Font.Style)).Width)
                        {
                            listUrunFiyat.Items[i].Font = new Font(listUrunFiyat.Items[i].Font.FontFamily, listUrunFiyat.Items[i].Font.Size - 0.5f, listUrunFiyat.Items[i].Font.Style);
                        }
                        while (listUrunFiyat.Columns[1].Width < System.Windows.Forms.TextRenderer.MeasureText(listUrunFiyat.Items[i].SubItems[1].Text, new Font(listUrunFiyat.Items[i].Font.FontFamily, listUrunFiyat.Items[i].Font.Size, listUrunFiyat.Items[i].Font.Style)).Width)
                        {
                            listUrunFiyat.Items[i].Font = new Font(listUrunFiyat.Items[i].Font.FontFamily, listUrunFiyat.Items[i].Font.Size - 0.5f, listUrunFiyat.Items[i].Font.Style);
                        }

                        while (listUrunFiyat.Columns[2].Width < System.Windows.Forms.TextRenderer.MeasureText(listUrunFiyat.Items[i].SubItems[2].Text, new Font(listUrunFiyat.Items[i].Font.FontFamily, listUrunFiyat.Items[i].Font.Size, listUrunFiyat.Items[i].Font.Style)).Width)
                        {
                            listUrunFiyat.Items[i].Font = new Font(listUrunFiyat.Items[i].Font.FontFamily, listUrunFiyat.Items[i].Font.Size - 0.5f, listUrunFiyat.Items[i].Font.Style);
                        }
                    }
                }

                while (listUrunFiyat.Columns[1].Width < System.Windows.Forms.TextRenderer.MeasureText(yemekAdi, listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font).Width
                    || listUrunFiyat.Columns[2].Width < System.Windows.Forms.TextRenderer.MeasureText((kacPorsiyon * Convert.ToDecimal(fiyatGelen)).ToString("0.00"), listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font).Width
                    || listUrunFiyat.Columns[0].Width < System.Windows.Forms.TextRenderer.MeasureText(kacPorsiyon.ToString(), listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font).Width)
                {
                    listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font = new Font(listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font.FontFamily, listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font.Size - 0.5f, listUrunFiyat.Items[listUrunFiyat.Items.Count - 1].Font.Style);
                }
            }
            else // varsa ürünün hesaptaki değerlerini istenilene göre arttır
            {
                listUrunFiyat.Groups[eskiSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text = (Convert.ToDouble(listUrunFiyat.Groups[eskiSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text) + (double)kacPorsiyon).ToString();

                listUrunFiyat.Groups[eskiSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[2].Text = (Convert.ToDecimal(listUrunFiyat.Groups[eskiSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[2].Text) + kacPorsiyon * Convert.ToDecimal(fiyatGelen)).ToString("0.00");

                while (listUrunFiyat.Columns[2].Width < System.Windows.Forms.TextRenderer.MeasureText(listUrunFiyat.Groups[eskiSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[2].Text, listUrunFiyat.Groups[eskiSiparisler].Items[gruptaYeniGelenSiparisVarmi].Font).Width
                    || listUrunFiyat.Columns[0].Width < System.Windows.Forms.TextRenderer.MeasureText(listUrunFiyat.Groups[eskiSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text, listUrunFiyat.Groups[eskiSiparisler].Items[gruptaYeniGelenSiparisVarmi].Font).Width)
                {
                    listUrunFiyat.Groups[eskiSiparisler].Items[gruptaYeniGelenSiparisVarmi].Font = new Font(listUrunFiyat.Groups[eskiSiparisler].Items[gruptaYeniGelenSiparisVarmi].Font.FontFamily, listUrunFiyat.Groups[eskiSiparisler].Items[gruptaYeniGelenSiparisVarmi].Font.Size - 0.5f, listUrunFiyat.Groups[eskiSiparisler].Items[gruptaYeniGelenSiparisVarmi].Font.Style);
                }
            }
            labelKalanHesap.Text = (Convert.ToDecimal(labelKalanHesap.Text) + kacPorsiyon * Convert.ToDecimal(fiyatGelen)).ToString("0.00");
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
                    masaDegisti = masaDegistirForm.yapilmasiGerekenIslem;

                    masaFormu.serverdanMasaDegisikligi(MasaAdi, hangiDepartman.departmanAdi, masaDegistirForm.yeniMasa, masaDegistirForm.yeniDepartman, "masaDegistir");
                }
                else // client
                {
                    masaFormu.menuFormundanServeraMasaDegisikligi(masaDegistirForm.yeniMasa, masaDegistirForm.yeniDepartman, MasaAdi, hangiDepartman.departmanAdi, masaDegistirForm.yapilmasiGerekenIslem, "masaDegistir");

                    yeniMasaninAdi = masaDegistirForm.yeniMasa;
                    masaDegisti = masaDegistirForm.yapilmasiGerekenIslem;
                }
                masaAktarmaIslemlerindenSonraCik(masaDegistirForm.yeniMasa, masaDegistirForm.yeniDepartman);
                masaDegistirForm = null;
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

                hesapForm = new HesapFormu(this, listUrunFiyat, MasaAdi, hangiDepartman.departmanAdi, siparisiKimGirdi);
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
            SqlCommand cmd = SQLBaglantisi.getCommand("INSERT INTO Siparis(AdisyonID,Garsonu,Fiyatı,Porsiyon,YemekAdi,VerilisTarihi) VALUES(@_AdisyonID,@_Garsonu,@_Fiyatı,@_Porsiyon,@_YemekAdi,@_VerilisTarihi)");
            cmd.Parameters.AddWithValue("@_AdisyonID", adisyonID);
            cmd.Parameters.AddWithValue("@_Garsonu", siparisiKimGirdi);
            cmd.Parameters.AddWithValue("@_Fiyatı", Convert.ToDecimal(siparis.SubItems[2].Text) / Convert.ToDecimal(siparis.SubItems[0].Text));
            cmd.Parameters.AddWithValue("@_Porsiyon", Convert.ToDecimal(siparis.SubItems[0].Text));
            cmd.Parameters.AddWithValue("@_YemekAdi", siparis.SubItems[1].Text);
            cmd.Parameters.AddWithValue("@_VerilisTarihi", DateTime.Now);

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void ikramUpdateInsert(int siparisID, int adisyonID, decimal porsiyon, double dusulecekDeger, decimal ikramAdedi, string yemekAdi, int ikramMi, DateTime verilisTarihi)
        {
            decimal yeniPorsiyonAdetiSiparis = porsiyon - ikramAdedi;

            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET Porsiyon = @Porsiyonu WHERE SiparisID=@id");
            cmd.Parameters.AddWithValue("@Porsiyonu", yeniPorsiyonAdetiSiparis);
            cmd.Parameters.AddWithValue("@id", siparisID);
            cmd.ExecuteNonQuery();

            cmd = SQLBaglantisi.getCommand("INSERT INTO Siparis(AdisyonID,Garsonu,Fiyatı,Porsiyon,YemekAdi,IkramMi,VerilisTarihi) values(@_AdisyonID,@_Garsonu,@_Fiyatı,@_Porsiyon,@_YemekAdi,@_IkramMi,@_VerilisTarihi)");
            cmd.Parameters.AddWithValue("@_AdisyonID", adisyonID);
            cmd.Parameters.AddWithValue("@_Garsonu", siparisiKimGirdi);
            cmd.Parameters.AddWithValue("@_Fiyatı", dusulecekDeger);
            cmd.Parameters.AddWithValue("@_Porsiyon", ikramAdedi);
            cmd.Parameters.AddWithValue("@_YemekAdi", yemekAdi);
            cmd.Parameters.AddWithValue("@_IkramMi", ikramMi);
            cmd.Parameters.AddWithValue("@_VerilisTarihi", verilisTarihi);

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

        public void iptalUpdateInsert(int siparisID, int adisyonID, decimal porsiyon, double dusulecekDeger, decimal iptalAdedi, string yemekAdi, DateTime verilisTarihi)
        {
            decimal yeniPorsiyonAdetiSiparis = porsiyon - iptalAdedi;

            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET Porsiyon = @Porsiyonu WHERE SiparisID=@id");
            cmd.Parameters.AddWithValue("@Porsiyonu", yeniPorsiyonAdetiSiparis);
            cmd.Parameters.AddWithValue("@id", siparisID);
            cmd.ExecuteNonQuery();

            cmd = SQLBaglantisi.getCommand("INSERT INTO Siparis(AdisyonID,Garsonu,Fiyatı,Porsiyon,YemekAdi,IptalMi,VerilisTarihi) values(@_AdisyonID,@_Garsonu,@_Fiyatı,@_Porsiyon,@_YemekAdi,@_IptalMi,@_VerilisTarihi)");
            cmd.Parameters.AddWithValue("@_AdisyonID", adisyonID);
            cmd.Parameters.AddWithValue("@_Garsonu", siparisiKimGirdi);
            cmd.Parameters.AddWithValue("@_Fiyatı", dusulecekDeger);
            cmd.Parameters.AddWithValue("@_Porsiyon", iptalAdedi);
            cmd.Parameters.AddWithValue("@_YemekAdi", yemekAdi);
            cmd.Parameters.AddWithValue("@_IptalMi", 1);
            cmd.Parameters.AddWithValue("@_VerilisTarihi", verilisTarihi);

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void iptalUpdateTam(int siparisID)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET IptalMi=@iptal, Garsonu=@Garson WHERE SiparisID=@id");
            cmd.Parameters.AddWithValue("@iptal", 1);
            cmd.Parameters.AddWithValue("@Garson", siparisiKimGirdi);
            cmd.Parameters.AddWithValue("@id", siparisID);
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

        public void urunTasimaUpdateInsert(int siparisID, int aktarimYapilacakMasaninAdisyonID, decimal porsiyon, double dusulecekDeger, decimal tasinacakMiktar, string yemekAdi, int ikramMi, DateTime verilisTarihi)
        {
            decimal yeniPorsiyonAdetiSiparis = porsiyon - tasinacakMiktar;

            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET Porsiyon = @Porsiyonu WHERE SiparisID=@id");
            cmd.Parameters.AddWithValue("@Porsiyonu", yeniPorsiyonAdetiSiparis);
            cmd.Parameters.AddWithValue("@id", siparisID);
            cmd.ExecuteNonQuery();

            cmd = SQLBaglantisi.getCommand("INSERT INTO Siparis(AdisyonID,Garsonu,Fiyatı,Porsiyon,YemekAdi,IkramMi,VerilisTarihi) values(@_AdisyonID,@_Garsonu,@_Fiyatı,@_Porsiyon,@_YemekAdi,@_IkramMi,@_VerilisTarihi)");
            cmd.Parameters.AddWithValue("@_AdisyonID", aktarimYapilacakMasaninAdisyonID);
            cmd.Parameters.AddWithValue("@_Garsonu", siparisiKimGirdi);
            cmd.Parameters.AddWithValue("@_Fiyatı", dusulecekDeger);
            cmd.Parameters.AddWithValue("@_Porsiyon", tasinacakMiktar);
            cmd.Parameters.AddWithValue("@_YemekAdi", yemekAdi);
            cmd.Parameters.AddWithValue("@_IkramMi", ikramMi);
            cmd.Parameters.AddWithValue("@_VerilisTarihi", verilisTarihi);

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        #endregion

        //Seçili siparişlerin adisyonunu değiştiren butonun methodu -- Yeni Siparişler taşınamaz
        private void buttonTasi_Click(object sender, EventArgs e)
        {
            UrunDegistir urunDegistirForm = new UrunDegistir(listUrunFiyat.SelectedItems);
            DialogResult urunDegissinMi = urunDegistirForm.ShowDialog();

            if (urunDegissinMi == DialogResult.OK)
            {
                masaDegistirForm = new MasaDegistirFormu(MasaAdi, hangiDepartman.departmanAdi, this);
                masaDegistirForm.ShowDialog();

                if (masaDegistirForm.yeniMasa == "iptalEdildi")
                    return;
                else
                {
                    //Eğer taşınmak istenen ürünlerin  miktarı 0 yapılmışsa, 0 yapılanları taşımaya çalışma
                    for (int i = 0; i < urunDegistirForm.miktarlar.Count; i++)
                    {
                        if (urunDegistirForm.miktarlar[i] == 0)
                        {
                            urunDegistirForm.miktarlar.RemoveAt(i);
                            listUrunFiyat.Items[listUrunFiyat.SelectedItems[i].Index].Selected = false;
                        }
                    }

                    //Eğer taşınması gereken ürün sayılarında 0 yapılanlar varsa ve onların dışında taşınacak ürün yoksa, ürün taşıma
                    if (urunDegistirForm.miktarlar.Count < 1)
                        return;

                    decimal istenilenTasimaMiktari;

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
                        }

                        if (listUrunFiyat.Items.Count == urunDegistirForm.miktarlar.Count)
                        {
                            masaAcikMi = false;
                        }

                        for (int i = 0; i < urunDegistirForm.miktarlar.Count; i++)
                        {
                            istenilenTasimaMiktari = urunDegistirForm.miktarlar[i];

                            if (Convert.ToDouble(listUrunFiyat.SelectedItems[i].SubItems[0].Text) > Convert.ToDouble(istenilenTasimaMiktari))
                                masaAcikMi = true;

                            double dusulecekDeger = Convert.ToDouble(listUrunFiyat.SelectedItems[i].SubItems[2].Text) / Convert.ToDouble(listUrunFiyat.SelectedItems[i].SubItems[0].Text);

                            if (listUrunFiyat.SelectedItems[i].Group == listUrunFiyat.Groups[2]) // ürünü diğer adisyona geçirirken IkramMi değerini bu değişkenden alacağız
                            {
                                tasinacakUrunIkramMi = 0;
                            }
                            else
                            {
                                tasinacakUrunIkramMi = 1;
                            }

                            cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adisyon.AdisyonID,Porsiyon,Siparis.VerilisTarihi FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi='" + tasinacakUrunIkramMi + "' AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + MasaAdi + "' AND Adisyon.DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND Siparis.YemekAdi='" + listUrunFiyat.SelectedItems[i].SubItems[1].Text + "' AND Siparis.Garsonu='" + siparisiKimGirdi + "' ORDER BY Porsiyon DESC");

                            dr = cmd.ExecuteReader();

                            int siparisID, adisyonID;
                            decimal porsiyon;
                            DateTime verilisTarihi;

                            while (dr.Read())
                            {
                                try
                                {
                                    siparisID = dr.GetInt32(0);
                                    adisyonID = dr.GetInt32(1);
                                    porsiyon = dr.GetDecimal(2);
                                    verilisTarihi = dr.GetDateTime(3);
                                }
                                catch
                                {
                                    KontrolFormu dialog = new KontrolFormu("Ürünü taşırken bir hata oluştu, lütfen tekrar deneyiniz", false);
                                    dialog.Show();
                                    return;
                                }

                                if (porsiyon < istenilenTasimaMiktari) // elimizde ikram edilmemişler ikramı istenenden küçükse
                                {
                                    urunTasimaUpdateTam(siparisID, aktarilacakMasaninAdisyonID);

                                    istenilenTasimaMiktari -= porsiyon;
                                }
                                else if (porsiyon > istenilenTasimaMiktari) // den büyükse
                                {
                                    urunTasimaUpdateInsert(siparisID, aktarilacakMasaninAdisyonID, porsiyon, dusulecekDeger, istenilenTasimaMiktari, listUrunFiyat.SelectedItems[i].SubItems[1].Text, tasinacakUrunIkramMi, verilisTarihi);

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
                                cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Porsiyon,Siparis.VerilisTarihi FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi='" + tasinacakUrunIkramMi + "' AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + MasaAdi + "' AND Adisyon.DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND Siparis.YemekAdi='" + listUrunFiyat.SelectedItems[i].SubItems[1].Text + "' AND Siparis.Garsonu!='" + siparisiKimGirdi + "' ORDER BY Porsiyon DESC");

                                dr = cmd.ExecuteReader();

                                while (dr.Read())
                                {
                                    try
                                    {
                                        siparisID = dr.GetInt32(0);
                                        porsiyon = dr.GetDecimal(1);
                                        verilisTarihi = dr.GetDateTime(2);
                                    }
                                    catch
                                    {
                                        KontrolFormu dialog = new KontrolFormu("Ürünü taşırken bir hata oluştu, lütfen tekrar deneyiniz", false);
                                        dialog.Show();
                                        return;
                                    }

                                    if (porsiyon < istenilenTasimaMiktari) // elimizde ikram edilmemişler ikramı istenenden küçükse
                                    {
                                        urunTasimaUpdateTam(siparisID, aktarilacakMasaninAdisyonID);

                                        istenilenTasimaMiktari -= porsiyon;
                                    }
                                    else if (porsiyon > istenilenTasimaMiktari) // den büyükse
                                    {
                                        urunTasimaUpdateInsert(siparisID, aktarilacakMasaninAdisyonID, porsiyon, dusulecekDeger, istenilenTasimaMiktari, listUrunFiyat.SelectedItems[i].SubItems[1].Text, tasinacakUrunIkramMi, verilisTarihi);

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

                        masaAktarmaIslemlerindenSonraSiparisleriGir(masaDegistirForm.yeniMasa, masaDegistirForm.yeniDepartman);

                        dialog2 = new KontrolFormu("Masa(" + MasaAdi + ")'dan seçilen ürünler " + masaDegistirForm.yeniDepartman + "\ndepartmanındaki, " + masaDegistirForm.yeniMasa + " masasına aktarıldı\nLütfen masaya yeniden giriş yapınız", false, this);
                        timerDialogClose.Start();
                        dialog2.Show();
                        this.Enabled = false;
                    }
                    else //client
                    {
                        if (listUrunFiyat.Items.Count == urunDegistirForm.miktarlar.Count)
                        {
                            masaAcikMi = false;
                        }

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

                            double dusulecekDeger = Convert.ToDouble(listUrunFiyat.SelectedItems[i].SubItems[2].Text) / Convert.ToDouble(listUrunFiyat.SelectedItems[i].SubItems[0].Text);

                            aktarmaBilgileri.Append("*" + listUrunFiyat.SelectedItems[i].SubItems[1].Text + "-" + dusulecekDeger.ToString() + "-" + istenilenTasimaMiktari.ToString() + "-" + tasinacakUrunIkramMi.ToString());

                            if (Convert.ToDouble(listUrunFiyat.SelectedItems[i].SubItems[0].Text) > Convert.ToDouble(istenilenTasimaMiktari))
                                masaAcikMi = true;
                        }

                        if (aktarmaBilgileri.Length >= 1)
                        {
                            aktarmaBilgileri.Remove(0, 1);
                        }

                        masaAktarmaIslemlerindenSonraSiparisleriGir(masaDegistirForm.yeniMasa, masaDegistirForm.yeniDepartman);

                        masaFormu.menuFormundanServeraUrunTasinacakBilgisiGonder(MasaAdi, hangiDepartman.departmanAdi, "urunuTasi", masaDegistirForm.yeniMasa, masaDegistirForm.yeniDepartman, siparisiKimGirdi, aktarmaBilgileri);

                        if (!masaAcikMi)
                        {
                            masaFormu.siparisListesiBos(MasaAdi, hangiDepartman.departmanAdi, "listeBos");
                        }
                    }
                }
            }
        }

        private void masaAktarmaIslemlerindenSonraSiparisleriGir(string yeniMasaAdi, string yeniDepartmanAdi)
        {
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

                bool mutfakAdisyonuYazdir = false;

                foreach (ListViewItem siparis in listUrunFiyat.Groups[yeniSiparisler].Items)
                {
                    mutfakAdisyonuYazdir = true;
                    siparisOlustur(adisyonID, siparis);

                    masaFormu.serverdanSiparisIkramVeyaIptal(yeniMasaAdi, yeniDepartmanAdi, "siparis", siparis.SubItems[0].Text, siparis.SubItems[1].Text, (Convert.ToDecimal(siparis.SubItems[2].Text) / Convert.ToDecimal(siparis.SubItems[0].Text)).ToString(), null);
                }

                //burada mutfak adisyonu iste 

                if (mutfakAdisyonuYazdir)
                {
                    cmd = SQLBaglantisi.getCommand("SELECT FirmaAdi,Yazici FROM Yazici WHERE YaziciAdi LIKE 'Mutfak%'");
                    dr = cmd.ExecuteReader();

                    dr.Read();

                    string firmaAdi = dr.GetString(0), yaziciAdi = dr.GetString(1);

                    cmd.Connection.Close();
                    cmd.Connection.Dispose();

                    asyncYaziciyaGonder(MasaAdi, hangiDepartman.departmanAdi, firmaAdi, yaziciAdi, raporMutfak);
                }

                if (adisyonNotuGuncellenmeliMi) // eğer sipariş notuna dokunulmuşsa not update edilsin
                {
                    adisyonNotuUpdate(adisyonID);
                }
            }
            else //client
            {
                int sonSiparisMi = listUrunFiyat.Groups[3].Items.Count;

                foreach (ListViewItem siparis in listUrunFiyat.Groups[yeniSiparisler].Items)
                {
                    sonSiparisMi--;

                    masaFormu.serveraSiparis(yeniMasaAdi, yeniDepartmanAdi, "siparis", siparis.SubItems[0].Text, siparis.SubItems[1].Text, siparisiKimGirdi, (Convert.ToDecimal(siparis.SubItems[2].Text) / Convert.ToDecimal(siparis.SubItems[0].Text)).ToString(), adisyonNotu,sonSiparisMi);
                    adisyonNotuGuncellenmeliMi = false;
                }

                if (adisyonNotuGuncellenmeliMi)
                {
                    masaFormu.serveraNotuYolla(yeniMasaAdi, yeniDepartmanAdi, "adisyonNotunuGuncelle", adisyonNotu);
                }
            }
        }

        private void timerDialogClose_Tick(object sender, EventArgs e)
        {
            dialog2.Close();
            timerDialogClose.Stop();
            this.Close();
        }
    }
}