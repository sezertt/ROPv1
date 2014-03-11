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
using System.Data.SqlClient;

namespace ROPv1
{
    public partial class SiparisMenuFormu : Form
    {
        Restoran hangiDepartman;

        int hangiMenuSecili = 555;

        const int eskiIkramlar = 0, yeniIkramlar = 1, eskiSiparisler = 2, yeniSiparisler = 3, indirimler = 4;

        const int urunBoyu = 220, fiyatBoyu = 90;

        DateTime acilisZamani;

        string siparisiKimGirdi, adisyonNotu = "", MasaAdi;

        List<Menuler> menuListesi = new List<Menuler>();  // menüleri tutacak liste

        List<UrunOzellikleri> urunListesi = new List<UrunOzellikleri>();

        List<bool> listedeSeciliOlanItemlar = new List<bool>();

        UItemp[] infoKullanici;

        bool iptalIkram = true;

        public bool masaAcikMi = false;

        decimal toplamHesap = 0, kalanHesap = 0;

        public SiparisMenuFormu(string masaninAdi, Restoran butonBilgileri, string siparisiGirenKisi, bool MasaAcikmi, decimal ToplamHesap, decimal KalanHesap)
        {
            InitializeComponent();

            masaAcikMi = MasaAcikmi;

            toplamHesap = ToplamHesap;

            kalanHesap = KalanHesap;

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

            if (Helper.VerifyHash("false", "SHA512", infoKullanici[kullaniciYeri].UIY[7]))
            {
                buttonUrunIkram.Enabled = false;
                buttonUrunIptal.Enabled = false;
                buttonindirim.Enabled = false;
                textBoxYuzde.Enabled = false;
                checkBoxYuzde.Enabled = false;
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

                flowPanelMenuBasliklari.Controls.Add(menuBasliklariButonlari);
            }

            XmlLoad<UrunOzellikleri> loadInfoUrun = new XmlLoad<UrunOzellikleri>();
            UrunOzellikleri[] infoUrun = loadInfoUrun.LoadRestoran("urunler.xml");

            urunListesi.AddRange(infoUrun);

            //ürünleri panele yerleştiriyoruz
            for (int i = 0; i < urunListesi.Count; i++)
            {
                if (urunListesi[i].kategorininAdi == flowPanelMenuBasliklari.Controls[0].Text)
                {
                    hangiMenuSecili = i;
                    flowPanelMenuBasliklari.Controls[0].Tag = i;
                    for (int j = 0; j < urunListesi[i].urunAdi.Count; j++)
                    {
                        Button urunButonlari = new Button();
                        urunButonlari.Text = urunListesi[i].urunAdi[j];

                        urunButonlari.UseVisualStyleBackColor = false;
                        urunButonlari.BackColor = Color.White;
                        urunButonlari.ForeColor = SystemColors.ActiveCaption;
                        urunButonlari.TextAlign = ContentAlignment.MiddleCenter;
                        urunButonlari.Font = new Font("Arial", 17.25F, FontStyle.Bold);
                        urunButonlari.Tag = j;
                        urunButonlari.Click += urunButonlari_Click;

                        flowPanelUrunler.Controls.Add(urunButonlari);
                    }
                    break;
                }
            }

            acilisZamani = DateTime.Now;
            listHesap.Groups[eskiSiparisler].Header += " - " + acilisZamani.ToShortTimeString();
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
            //scroll barlar yoksa yukarı aşağı butonlarını gizle
            if (!flowPanelUrunler.VerticalScroll.Visible)
            {
                buttonUrunlerDown.Visible = false;
                buttonUrunlerUp.Visible = false;
            }
            else
            {
                buttonUrunlerDown.Visible = true;
                buttonUrunlerUp.Visible = true;
            }
            //seçili olan menünün bilgisini tut, ürün seçiminde işe yarıyor
            hangiMenuSecili = Convert.ToInt32(((Button)sender).Tag);
        }

        //keypadin methodu
        private void pinboardcontrol21_UserKeyPressed(object sender, PinboardClassLibrary.PinboardEventArgs e)
        {
            if (!textBoxYuzde.Focused)
                textNumberOfItem.Focus();
            SendKeys.Send(e.KeyboardKeyPressed);
        }

        //Siparişlerin çarpanını ayarlayan textboxı boşaltan buton
        private void buttonDeleteText_Click(object sender, EventArgs e)
        {
            textNumberOfItem.Text = "";
            textBoxYuzde.Text = "";
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

        //form load
        private void SiparisMenuFormu_Load(object sender, EventArgs e)
        {
            if (masaAcikMi)
            {
                SqlCommand cmd = SQLBaglantisi.getCommand("SELECT Fiyatı, Porsiyon, YemekAdi, IkramMi, Garsonu from Siparis WHERE MasaAdi='" + MasaAdi + "' and DepartmanAdi='" + hangiDepartman.departmanAdi + "' and IptalMi=0 ORDER BY Porsiyon DESC");
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    decimal yemeginFiyati = dr.GetDecimal(0);
                    double kacPorsiyon = (double)dr.GetDecimal(1);
                    string yemeginAdi = dr.GetString(2);
                    bool ikramMi = dr.GetBoolean(3);
                    string Garson = dr.GetString(4);

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
                    for (int i = 0; i < listHesap.Groups[hangiGrup].Items.Count; i++)
                    {
                        if (yemeginAdi == listHesap.Groups[hangiGrup].Items[i].SubItems[1].Text)
                        {
                            gruptaYeniGelenSiparisVarmi = i;
                            break;
                        }
                    }

                    if (gruptaYeniGelenSiparisVarmi == -1) //yoksa ürünü hesaba ekle
                    {
                        listHesap.Items.Add(kacPorsiyon.ToString());
                        listHesap.Items[listHesap.Items.Count - 1].SubItems.Add(yemeginAdi);
                        listHesap.Items[listHesap.Items.Count - 1].SubItems.Add(((decimal)kacPorsiyon * yemeginFiyati).ToString("0.00"));
                        listHesap.Items[listHesap.Items.Count - 1].Group = listHesap.Groups[hangiGrup];
                        listHesap.Items[listHesap.Items.Count - 1].Font = new Font("Calibri", 18.75F, FontStyle.Bold);
                        listedeSeciliOlanItemlar.Add(false);

                        int itemsCount = this.listHesap.Items.Count + 3;// 3 aslında grup sayısı -1
                        int itemHeight = this.listHesap.Items[0].Bounds.Height;
                        int VisiableItem = (int)this.listHesap.ClientRectangle.Height / itemHeight;

                        if (itemsCount >= VisiableItem)
                        {
                            listHesap.Columns[1].Width = urunBoyu;
                            listHesap.Columns[2].Width = fiyatBoyu;

                            for (int i = 0; i < listHesap.Items.Count; i++)
                            {
                                while (listHesap.Columns[0].Width < System.Windows.Forms.TextRenderer.MeasureText(listHesap.Items[i].SubItems[0].Text, new Font(listHesap.Items[i].Font.FontFamily, listHesap.Items[i].Font.Size, listHesap.Items[i].Font.Style)).Width)
                                {
                                    listHesap.Items[i].Font = new Font(listHesap.Items[i].Font.FontFamily, listHesap.Items[i].Font.Size - 0.5f, listHesap.Items[i].Font.Style);
                                }
                                while (listHesap.Columns[1].Width < System.Windows.Forms.TextRenderer.MeasureText(listHesap.Items[i].SubItems[1].Text, new Font(listHesap.Items[i].Font.FontFamily, listHesap.Items[i].Font.Size, listHesap.Items[i].Font.Style)).Width)
                                {
                                    listHesap.Items[i].Font = new Font(listHesap.Items[i].Font.FontFamily, listHesap.Items[i].Font.Size - 0.5f, listHesap.Items[i].Font.Style);
                                }

                                while (listHesap.Columns[2].Width < System.Windows.Forms.TextRenderer.MeasureText(listHesap.Items[i].SubItems[2].Text, new Font(listHesap.Items[i].Font.FontFamily, listHesap.Items[i].Font.Size, listHesap.Items[i].Font.Style)).Width)
                                {
                                    listHesap.Items[i].Font = new Font(listHesap.Items[i].Font.FontFamily, listHesap.Items[i].Font.Size - 0.5f, listHesap.Items[i].Font.Style);
                                }
                            }
                        }

                        while (listHesap.Columns[1].Width < System.Windows.Forms.TextRenderer.MeasureText(yemeginAdi, listHesap.Items[listHesap.Items.Count - 1].Font).Width
                            || listHesap.Columns[2].Width < System.Windows.Forms.TextRenderer.MeasureText(((decimal)kacPorsiyon * yemeginFiyati).ToString("0.00"), listHesap.Items[listHesap.Items.Count - 1].Font).Width
                            || listHesap.Columns[0].Width < System.Windows.Forms.TextRenderer.MeasureText(kacPorsiyon.ToString(), listHesap.Items[listHesap.Items.Count - 1].Font).Width)
                        {
                            listHesap.Items[listHesap.Items.Count - 1].Font = new Font(listHesap.Items[listHesap.Items.Count - 1].Font.FontFamily, listHesap.Items[listHesap.Items.Count - 1].Font.Size - 0.5f, listHesap.Items[listHesap.Items.Count - 1].Font.Style);
                        }
                    }
                    else // varsa ürünün hesaptaki değerlerini istenilene göre arttır
                    {
                        listHesap.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text = (Convert.ToDouble(listHesap.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text) + kacPorsiyon).ToString();

                        listHesap.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].SubItems[2].Text = (Convert.ToDecimal(listHesap.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].SubItems[2].Text) + (decimal)kacPorsiyon * yemeginFiyati).ToString("0.00");

                        while (listHesap.Columns[2].Width < System.Windows.Forms.TextRenderer.MeasureText(listHesap.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].SubItems[2].Text, listHesap.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].Font).Width
                            || listHesap.Columns[0].Width < System.Windows.Forms.TextRenderer.MeasureText(listHesap.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text, listHesap.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].Font).Width)
                        {
                            listHesap.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].Font = new Font(listHesap.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].Font.FontFamily, listHesap.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].Font.Size - 0.5f, listHesap.Groups[hangiGrup].Items[gruptaYeniGelenSiparisVarmi].Font.Style);
                        }
                    }

                }
                labelKalanHesap.Text = kalanHesap.ToString("0.00");
                labelToplamHesap.Text = toplamHesap.ToString("0.00");

                if (labelToplamHesap.Text != "0,00") //hesapta para varsa butonu enable et
                    buttonHesapOde.Enabled = true;
            }

            //form load olduğunda tool larımızda ekranın boyundan fazla ürün yoksa scroll butonlarını saklıyoruz
            if (!flowPanelMenuBasliklari.VerticalScroll.Visible)
            {
                buttonMenulerDown.Visible = false;
                buttonMenulerUp.Visible = false;
            }
            else
            {
                buttonMenulerDown.Visible = true;
                buttonMenulerUp.Visible = true;
            }

            if (!flowPanelUrunler.VerticalScroll.Visible)
            {
                buttonUrunlerDown.Visible = false;
                buttonUrunlerUp.Visible = false;
            }
            else
            {
                buttonUrunlerDown.Visible = true;
                buttonUrunlerUp.Visible = true;
            }

            if (this.listHesap.Items.Count > 0)
            {
                int itemsCount = this.listHesap.Items.Count + 3;// 3 aslında grup sayısı -1
                int itemHeight = this.listHesap.Items[0].Bounds.Height;
                int VisiableItem = (int)this.listHesap.ClientRectangle.Height / itemHeight;
                if (itemsCount >= VisiableItem)
                {
                    listHesap.Columns[1].Width = urunBoyu;
                    listHesap.Columns[2].Width = fiyatBoyu;
                }
            }

            if (labelToplamHesap.Text == "0,00")
                buttonHesapOde.Enabled = false;
        }

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
            for (int i = 0; i < listHesap.Groups[yeniSiparisler].Items.Count; i++)
            {
                if (((Button)sender).Text == listHesap.Groups[yeniSiparisler].Items[i].SubItems[1].Text)
                {
                    gruptaYeniGelenSiparisVarmi = i;
                    break;
                }
            }

            if (gruptaYeniGelenSiparisVarmi == -1) //yoksa ürünü hesaba ekle
            {
                listHesap.Items.Add(kacPorsiyon.ToString());
                listHesap.Items[listHesap.Items.Count - 1].SubItems.Add(((Button)sender).Text);
                listHesap.Items[listHesap.Items.Count - 1].SubItems.Add(((decimal)kacPorsiyon * Convert.ToDecimal(urunListesi[hangiMenuSecili].porsiyonFiyati[Convert.ToInt32(((Button)sender).Tag)])).ToString("0.00"));
                listHesap.Items[listHesap.Items.Count - 1].Group = listHesap.Groups[yeniSiparisler];
                listHesap.Items[listHesap.Items.Count - 1].Font = new Font("Calibri", 18.75F, FontStyle.Bold);
                listedeSeciliOlanItemlar.Add(false);

                //eğer ürün eklendiğinde ekrana sığmıyorsa scroll gösterilecektir, bu yüzden fiyatları sola kaydırıyoruz
                int itemsCount = this.listHesap.Items.Count + 3;// 3 aslında grup sayısı -1
                int itemHeight = this.listHesap.Items[0].Bounds.Height;
                int VisiableItem = (int)this.listHesap.ClientRectangle.Height / itemHeight;

                if (itemsCount >= VisiableItem)
                {
                    listHesap.Columns[1].Width = urunBoyu;
                    listHesap.Columns[2].Width = fiyatBoyu;

                    for (int i = 0; i < listHesap.Items.Count; i++)
                    {
                        while (listHesap.Columns[0].Width < System.Windows.Forms.TextRenderer.MeasureText(listHesap.Items[i].SubItems[0].Text, new Font(listHesap.Items[i].Font.FontFamily, listHesap.Items[i].Font.Size, listHesap.Items[i].Font.Style)).Width)
                        {
                            listHesap.Items[i].Font = new Font(listHesap.Items[i].Font.FontFamily, listHesap.Items[i].Font.Size - 0.5f, listHesap.Items[i].Font.Style);
                        }
                        while (listHesap.Columns[1].Width < System.Windows.Forms.TextRenderer.MeasureText(listHesap.Items[i].SubItems[1].Text, new Font(listHesap.Items[i].Font.FontFamily, listHesap.Items[i].Font.Size, listHesap.Items[i].Font.Style)).Width)
                        {
                            listHesap.Items[i].Font = new Font(listHesap.Items[i].Font.FontFamily, listHesap.Items[i].Font.Size - 0.5f, listHesap.Items[i].Font.Style);
                        }

                        while (listHesap.Columns[2].Width < System.Windows.Forms.TextRenderer.MeasureText(listHesap.Items[i].SubItems[2].Text, new Font(listHesap.Items[i].Font.FontFamily, listHesap.Items[i].Font.Size, listHesap.Items[i].Font.Style)).Width)
                        {
                            listHesap.Items[i].Font = new Font(listHesap.Items[i].Font.FontFamily, listHesap.Items[i].Font.Size - 0.5f, listHesap.Items[i].Font.Style);
                        }
                    }
                }

                while (listHesap.Columns[1].Width < System.Windows.Forms.TextRenderer.MeasureText(((Button)sender).Text, listHesap.Items[listHesap.Items.Count - 1].Font).Width
                    || listHesap.Columns[2].Width < System.Windows.Forms.TextRenderer.MeasureText(((decimal)kacPorsiyon * Convert.ToDecimal(urunListesi[hangiMenuSecili].porsiyonFiyati[Convert.ToInt32(((Button)sender).Tag)])).ToString("0.00"), listHesap.Items[listHesap.Items.Count - 1].Font).Width
                    || listHesap.Columns[0].Width < System.Windows.Forms.TextRenderer.MeasureText(kacPorsiyon.ToString(), listHesap.Items[listHesap.Items.Count - 1].Font).Width)
                {
                    listHesap.Items[listHesap.Items.Count - 1].Font = new Font(listHesap.Items[listHesap.Items.Count - 1].Font.FontFamily, listHesap.Items[listHesap.Items.Count - 1].Font.Size - 0.5f, listHesap.Items[listHesap.Items.Count - 1].Font.Style);
                }

                labelToplamHesap.Text = (Convert.ToDecimal(labelToplamHesap.Text) + Convert.ToDecimal((listHesap.Items[listHesap.Items.Count - 1].SubItems[2].Text))).ToString("0.00");
                labelKalanHesap.Text = (Convert.ToDecimal(labelKalanHesap.Text) + Convert.ToDecimal((listHesap.Items[listHesap.Items.Count - 1].SubItems[2].Text))).ToString("0.00");
            }
            else // varsa ürünün hesaptaki değerlerini istenilene göre arttır
            {
                listHesap.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text = (Convert.ToDouble(listHesap.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text) + kacPorsiyon).ToString();

                listHesap.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[2].Text = (Convert.ToDecimal(listHesap.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[2].Text) + (decimal)kacPorsiyon * Convert.ToDecimal(urunListesi[hangiMenuSecili].porsiyonFiyati[Convert.ToInt32(((Button)sender).Tag)])).ToString("0.00");

                while (listHesap.Columns[2].Width < System.Windows.Forms.TextRenderer.MeasureText(listHesap.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[2].Text, listHesap.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].Font).Width
                    || listHesap.Columns[0].Width < System.Windows.Forms.TextRenderer.MeasureText(listHesap.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text, listHesap.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].Font).Width)
                {
                    listHesap.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].Font = new Font(listHesap.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].Font.FontFamily, listHesap.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].Font.Size - 0.5f, listHesap.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].Font.Style);
                }

                labelToplamHesap.Text = (Convert.ToDecimal(labelToplamHesap.Text) + (decimal)kacPorsiyon * Convert.ToDecimal(urunListesi[hangiMenuSecili].porsiyonFiyati[Convert.ToInt32(((Button)sender).Tag)])).ToString("0.00");
                labelKalanHesap.Text = (Convert.ToDecimal(labelKalanHesap.Text) + (decimal)kacPorsiyon * Convert.ToDecimal(urunListesi[hangiMenuSecili].porsiyonFiyati[Convert.ToInt32(((Button)sender).Tag)])).ToString("0.00");
            }
            textNumberOfItem.Text = ""; // çarpanı sil

            if (labelToplamHesap.Text != "0,00") //hesapta para varsa butonu enable et
                buttonHesapOde.Enabled = true;
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

        //liste ve panellerin scroll butonları
        #region Scroll Butonları

        private void UrunScrollUp(object sender, EventArgs e)
        {
            if (flowPanelUrunler.VerticalScroll.Value - flowPanelUrunler.Bounds.Height - 3 > -1)
                flowPanelUrunler.VerticalScroll.Value -= flowPanelUrunler.Bounds.Height - 3;
            else
                flowPanelUrunler.VerticalScroll.Value = 0;

            if (flowPanelUrunler.VerticalScroll.Value - flowPanelUrunler.Bounds.Height - 3 > -1)
                flowPanelUrunler.VerticalScroll.Value -= flowPanelUrunler.Bounds.Height - 3;
            else
                flowPanelUrunler.VerticalScroll.Value = 0;

        }

        private void UrunScrollDown(object sender, EventArgs e)
        {
            if (flowPanelUrunler.VerticalScroll.Value + flowPanelUrunler.Bounds.Height - 3 > flowPanelUrunler.VerticalScroll.Maximum)
                flowPanelUrunler.VerticalScroll.Value = flowPanelUrunler.VerticalScroll.Maximum;
            else
                flowPanelUrunler.VerticalScroll.Value += flowPanelUrunler.Bounds.Height - 3;

            if (flowPanelUrunler.VerticalScroll.Value + flowPanelUrunler.Bounds.Height - 3 > flowPanelUrunler.VerticalScroll.Maximum)
                flowPanelUrunler.VerticalScroll.Value = flowPanelUrunler.VerticalScroll.Maximum;
            else
                flowPanelUrunler.VerticalScroll.Value += flowPanelUrunler.Bounds.Height - 3;
        }

        private void MenuScrollUp(object sender, EventArgs e)
        {
            if (flowPanelMenuBasliklari.VerticalScroll.Value - flowPanelMenuBasliklari.Bounds.Height - 3 > -1)
                flowPanelMenuBasliklari.VerticalScroll.Value -= flowPanelMenuBasliklari.Bounds.Height - 3;
            else
                flowPanelMenuBasliklari.VerticalScroll.Value = 0;

            if (flowPanelMenuBasliklari.VerticalScroll.Value - flowPanelMenuBasliklari.Bounds.Height - 3 > -1)
                flowPanelMenuBasliklari.VerticalScroll.Value -= flowPanelMenuBasliklari.Bounds.Height - 3;
            else
                flowPanelMenuBasliklari.VerticalScroll.Value = 0;
        }

        private void MenuScrollDown(object sender, EventArgs e)
        {
            if (flowPanelMenuBasliklari.VerticalScroll.Value + flowPanelMenuBasliklari.Bounds.Height - 3 > flowPanelMenuBasliklari.VerticalScroll.Maximum)
                flowPanelMenuBasliklari.VerticalScroll.Value = flowPanelMenuBasliklari.VerticalScroll.Maximum;
            else
                flowPanelMenuBasliklari.VerticalScroll.Value += flowPanelMenuBasliklari.Bounds.Height - 3;

            if (flowPanelMenuBasliklari.VerticalScroll.Value + flowPanelMenuBasliklari.Bounds.Height - 3 > flowPanelMenuBasliklari.VerticalScroll.Maximum)
                flowPanelMenuBasliklari.VerticalScroll.Value = flowPanelMenuBasliklari.VerticalScroll.Maximum;
            else
                flowPanelMenuBasliklari.VerticalScroll.Value += flowPanelMenuBasliklari.Bounds.Height - 3;
        }

        #endregion

        //listede eleman seçildiğinde çalışacak olan method
        private void listHesap_Click(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = listHesap.HitTest(listHesap.PointToClient(Cursor.Position));
            int kacElemanSecili;

            //itema basıldıysa seçili olanları tuttuğumuz listede o itemı false ise true, true ise false yap
            if (info.Item != null)
            {
                if (listHesap.Items[info.Item.Index].Selected && listedeSeciliOlanItemlar[info.Item.Index] == false)
                    listedeSeciliOlanItemlar[info.Item.Index] = true;
                else
                    listedeSeciliOlanItemlar[info.Item.Index] = false;
            }

            kacElemanSecili = 0;

            for (int i = 0; i < listedeSeciliOlanItemlar.Count; i++)
            {
                if (listedeSeciliOlanItemlar[i])
                {
                    listHesap.Items[i].Selected = true;
                    kacElemanSecili++;
                }
                else
                {
                    listHesap.Items[i].Selected = false;
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
                if (iptalIkram) // Yeni girilen bir sipariş veritabanına girmeden ikram edilemez
                {
                    if (listHesap.SelectedItems[0].Group != listHesap.Groups[yeniSiparisler])
                        buttonUrunIkram.Enabled = true;

                    buttonUrunIptal.Enabled = true;
                }

                if (listHesap.SelectedItems[0].Group == listHesap.Groups[eskiIkramlar] || listHesap.SelectedItems[0].Group == listHesap.Groups[yeniIkramlar])
                    buttonUrunIkram.Text = "  İkram İptal";
                else
                    buttonUrunIkram.Text = "  İkram";

                if (listHesap.SelectedItems[0].Group == listHesap.Groups[indirimler])
                    buttonindirim.Text = "     İndirim Sil";
                else
                    buttonindirim.Text = "  İndirim";

                buttonTasi.Enabled = true;
                buttonAdd.Enabled = true;
            }
            else
            {
                buttonUrunIkram.Enabled = false;
                buttonTasi.Enabled = true;
                buttonUrunIptal.Enabled = false;
                buttonAdd.Enabled = false;
            }

            if (iptalIkram)
            {
                buttonindirim.Enabled = true;
                textBoxYuzde.Enabled = true;
                checkBoxYuzde.Enabled = true;
            }
            else
            {
                buttonindirim.Enabled = false;
                textBoxYuzde.Enabled = false;
                checkBoxYuzde.Enabled = false;
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

        //adisyon notu ekleme butonu
        private void addNoteButton_Click(object sender, EventArgs e)
        {
            //eski adisyon notu varsa onu yolla yoksa boş text "" yolla            
            AdisyonNotuFormu notFormu;
            string adisyonNotuDegistiMi = adisyonNotu;
            //Burada adisyonNotu'nu sql den al
            if (adisyonNotu != "")
                notFormu = new AdisyonNotuFormu(adisyonNotu); // varsa notu yolla 
            else
                notFormu = new AdisyonNotuFormu(""); //yoksa boş yolla               

            notFormu.ShowDialog();
            if (adisyonNotu != adisyonNotuDegistiMi)
            {
                adisyonNotu = notFormu.AdisyonNotu;
            }
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

            if (carpan > Convert.ToDouble(listHesap.SelectedItems[0].SubItems[0].Text))
                carpan = Convert.ToDouble(listHesap.SelectedItems[0].SubItems[0].Text);

            // ikram et
            if (buttonUrunIkram.Text == "  İkram")
            {
                decimal istenilenikramSayisi = (decimal)carpan;

                // ürünün değerini istenilen kadar azalt, tüm hesaptan düş
                double dusulecekDeger = Convert.ToDouble(listHesap.SelectedItems[0].SubItems[2].Text) / Convert.ToDouble(listHesap.SelectedItems[0].SubItems[0].Text);
                listHesap.SelectedItems[0].SubItems[2].Text = (Convert.ToDouble(listHesap.SelectedItems[0].SubItems[2].Text) - carpan * dusulecekDeger).ToString("0.00");

                listHesap.SelectedItems[0].SubItems[0].Text = (Convert.ToDouble(listHesap.SelectedItems[0].SubItems[0].Text) - carpan).ToString();

                // labelToplamHesap.Text = (Convert.ToDouble(labelToplamHesap.Text) - (dusulecekDeger * carpan)).ToString("0.00");

                labelKalanHesap.Text = (Convert.ToDouble(labelKalanHesap.Text) - (dusulecekDeger * carpan)).ToString("0.00");

                bool ikramYok = true;

                for (int i = 0; i < listHesap.Groups[yeniIkramlar].Items.Count; i++)
                {
                    if (listHesap.Groups[yeniIkramlar].Items[i].SubItems[1].Text == listHesap.SelectedItems[0].SubItems[1].Text)
                    {

                        listHesap.Groups[yeniIkramlar].Items[i].Text = (Convert.ToDouble(listHesap.Groups[yeniIkramlar].Items[i].Text) + carpan).ToString();
                        listHesap.Groups[yeniIkramlar].Items[i].SubItems[2].Text = (Convert.ToDouble(listHesap.Groups[yeniIkramlar].Items[i].SubItems[2].Text) + (dusulecekDeger * carpan)).ToString("0.00");

                        ikramYok = false;
                    }
                }

                if (ikramYok)
                {
                    listHesap.Items.Insert(0, carpan.ToString());
                    listHesap.Items[0].SubItems.Add(listHesap.SelectedItems[0].SubItems[1].Text);
                    listHesap.Items[0].SubItems.Add((dusulecekDeger * carpan).ToString("0.00"));

                    listHesap.Items[0].Group = listHesap.Groups[yeniIkramlar];
                    listHesap.Items[0].Font = new Font("Calibri", 18.75F, FontStyle.Bold);
                    listedeSeciliOlanItemlar.Insert(0, false);
                }

                //İkram edildiğinde önce hangi grupta olduğuna bakılacak, eğer yeni eklenenler grubunda (yani 3 indeksli grup) ise SQL o grubu update etmeye gerek yok direk ikram kaldırılacak
                //Eğer eskiden ekli olanlarda ise (yani 1 indeksli grup) ikram adedine ulaşana kadar update yaparak sipariş sayısını azaltacaz
                //Eğer iptal edilen adedi tam olarak siparişlerde bulunamazsa örneğin 4 iptal var 2 tane 3 adetlik sipariş var yani toplam 6
                //İlk gelen siparişin ikram özelliği true(1) yapılacak diğerinin adedi update edilerek azaltılacak ikramın kalanı kadarıyla yeni ikram siparişi oluşturulacak

                if (listHesap.SelectedItems[0].Group == listHesap.Groups[eskiSiparisler]) //eski siparişlerle ilgili ikram
                {
                    SqlCommand cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adisyon.AdisyonID,Porsiyon FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.acikMi=1 AND Siparis.IkramMi=0 AND Siparis.IptalMi=0 AND Siparis.MasaAdi='" + MasaAdi + "' AND Siparis.DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND Siparis.YemekAdi='" + listHesap.SelectedItems[0].SubItems[1].Text + "' AND Siparis.Garsonu='" + siparisiKimGirdi + "' ORDER BY Porsiyon DESC");

                    SqlDataReader dr = cmd.ExecuteReader();

                    int siparisID, adisyonID;
                    decimal porsiyon;

                    dr.Read();

                    adisyonID = dr.GetInt32(1);

                    do
                    {
                        siparisID = dr.GetInt32(0);

                        porsiyon = dr.GetDecimal(2);

                        if (porsiyon < istenilenikramSayisi) // elimizde ikram edilmemişler ikramı istenenden küçükse
                        {
                            ikramUpdateTam(siparisID, 1);

                            istenilenikramSayisi -= porsiyon;
                        }
                        else if (porsiyon > istenilenikramSayisi) // den büyükse
                        {
                            ikramUpdateInsert(siparisID, adisyonID, porsiyon, dusulecekDeger, istenilenikramSayisi, listHesap.SelectedItems[0].SubItems[1].Text, 1);

                            istenilenikramSayisi = 0;
                        }
                        else // elimizde ikram edilmemişler ikramı istenene eşitse
                        {
                            ikramUpdateTam(siparisID, 1);

                            istenilenikramSayisi = 0;
                        }

                        if (istenilenikramSayisi == 0)
                        {
                            adisyonUpdateHesapveKalan(adisyonID);
                            break;
                        }
                    } while (dr.Read());


                    if (istenilenikramSayisi != 0)// ikram edilecekler daha bitmedi başka garsonların siparişlerinden ikram iptaline devam et
                    {
                        cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adisyon.AdisyonID,Porsiyon FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.acikMi=1 AND Siparis.IkramMi=0 AND Siparis.IptalMi=0 AND Siparis.MasaAdi='" + MasaAdi + "' AND Siparis.DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND Siparis.YemekAdi='" + listHesap.SelectedItems[0].SubItems[1].Text + "' AND Siparis.Garsonu!='" + siparisiKimGirdi + "' ORDER BY Porsiyon DESC");

                        dr = cmd.ExecuteReader();

                        dr.Read();

                        adisyonID = dr.GetInt32(1);

                        do
                        {
                            siparisID = dr.GetInt32(0);

                            porsiyon = dr.GetDecimal(2);

                            if (porsiyon < istenilenikramSayisi) // elimizde ikram edilmemişler ikramı istenenden küçükse
                            {
                                ikramUpdateTam(siparisID, 1);

                                istenilenikramSayisi -= porsiyon;
                            }
                            else if (porsiyon > istenilenikramSayisi) // den büyükse
                            {
                                ikramUpdateInsert(siparisID, adisyonID, porsiyon, dusulecekDeger, istenilenikramSayisi, listHesap.SelectedItems[0].SubItems[1].Text, 1);

                                istenilenikramSayisi = 0;
                            }
                            else // elimizde ikram edilmemişler ikramı istenene eşitse
                            {
                                ikramUpdateTam(siparisID, 1);

                                istenilenikramSayisi = 0;
                            }

                            if (istenilenikramSayisi == 0)
                            {
                                adisyonUpdateHesapveKalan(adisyonID);
                                break;
                            }
                        } while (dr.Read());
                    }
                }
                else //yeni siparişlerle ilgili ikram
                {
                    int siparisinIndexi = listHesap.SelectedItems[0].Index;

                    try // açık
                    {
                        SqlCommand cmd = SQLBaglantisi.getCommand("SELECT AdisyonID FROM Adisyon WHERE acikMi=1 AND MasaAdi='" + MasaAdi + "' AND DepartmanAdi='" + hangiDepartman.departmanAdi + "'");

                        SqlDataReader dr = cmd.ExecuteReader();

                        dr.Read();
                        int adisyonID = dr.GetInt32(0);

                        ikramInsert(adisyonID, dusulecekDeger, istenilenikramSayisi, listHesap.Items[siparisinIndexi].SubItems[1].Text);
                        masaAcikMi = true;
                    }
                    catch // kapalı
                    {
                        adisyonOlustur();
                        SqlCommand cmd = SQLBaglantisi.getCommand("SELECT AdisyonID FROM Adisyon WHERE acikMi=1 AND MasaAdi='" + MasaAdi + "' AND DepartmanAdi='" + hangiDepartman.departmanAdi + "'");

                        SqlDataReader dr = cmd.ExecuteReader();

                        dr.Read();
                        int adisyonID = dr.GetInt32(0);

                        ikramInsert(adisyonID, dusulecekDeger, istenilenikramSayisi, listHesap.Items[siparisinIndexi].SubItems[1].Text);
                        masaAcikMi = true;
                    }
                }

                if (listHesap.SelectedItems[0].Text == "0")
                {
                    listedeSeciliOlanItemlar.RemoveAt(listHesap.SelectedItems[0].Index);
                    listHesap.SelectedItems[0].Remove();
                }
            }
            else // ikramı iptal et
            {
                decimal istenilenIkramiptalSayisi = (decimal)carpan;
                // ürünün değerini bul ve hesaba ekle
                double dusulecekDeger = Convert.ToDouble(listHesap.SelectedItems[0].SubItems[2].Text) / Convert.ToDouble(listHesap.SelectedItems[0].SubItems[0].Text);
                listHesap.SelectedItems[0].SubItems[2].Text = (Convert.ToDouble(listHesap.SelectedItems[0].SubItems[2].Text) - dusulecekDeger * carpan).ToString("0.00");

                listHesap.SelectedItems[0].SubItems[0].Text = (Convert.ToDouble(listHesap.SelectedItems[0].SubItems[0].Text) - carpan).ToString();

                // labelToplamHesap.Text = (Convert.ToDouble(labelToplamHesap.Text) + (dusulecekDeger * carpan)).ToString("0.00");

                labelKalanHesap.Text = (Convert.ToDouble(labelKalanHesap.Text) + (dusulecekDeger * carpan)).ToString("0.00");

                bool urunYok = true;

                for (int i = 0; i < listHesap.Groups[eskiSiparisler].Items.Count; i++)
                {
                    if (listHesap.Groups[eskiSiparisler].Items[i].SubItems[1].Text == listHesap.SelectedItems[0].SubItems[1].Text)
                    {
                        listHesap.Groups[eskiSiparisler].Items[i].Text = (Convert.ToDouble(listHesap.Groups[eskiSiparisler].Items[i].Text) + carpan).ToString();
                        listHesap.Groups[eskiSiparisler].Items[i].SubItems[2].Text = (Convert.ToDouble(listHesap.Groups[eskiSiparisler].Items[i].SubItems[2].Text) + (dusulecekDeger * carpan)).ToString("0.00");

                        urunYok = false;
                    }
                }

                if (urunYok)
                {
                    listHesap.Items.Add(carpan.ToString());
                    listHesap.Items[listHesap.Items.Count - 1].SubItems.Add(listHesap.SelectedItems[0].SubItems[1].Text);
                    listHesap.Items[listHesap.Items.Count - 1].SubItems.Add((dusulecekDeger * carpan).ToString("0.00"));

                    listHesap.Items[listHesap.Items.Count - 1].Group = listHesap.Groups[eskiSiparisler];
                    listHesap.Items[listHesap.Items.Count - 1].Font = new Font("Calibri", 18.75F, FontStyle.Bold);
                    listedeSeciliOlanItemlar.Add(false);
                }

                //İkram iptal edildiğinde önce hangi grupta olduğuna bakılacak, eğer yeni eklenenler grubunda (yani 2. grup) ise SQL de o grubu update etmeye gerek yok
                //Eğer eskiden ekli olanlarda ise ikram adedine ulaşana kadar update yaparak sipariş sayısını azaltacaz
                //Eğer iptal edilen adedi tam olarak siparişlerde bulunamazsa örneğin 4 iptal var 2 tane 3 adetlik sipariş var yani toplam 6
                //İlk gelen siparişin ikram özelliği true(1) yapılacak diğerinin adedi update edilerek azaltılacak ikramın kalanı kadarıyla yeni ikram siparişi oluşturulacak


                SqlCommand cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adisyon.AdisyonID,Porsiyon FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.acikMi=1 AND Siparis.IkramMi=1 AND Siparis.IptalMi=0 AND Siparis.MasaAdi='" + MasaAdi + "' AND Siparis.DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND Siparis.YemekAdi='" + listHesap.SelectedItems[0].SubItems[1].Text + "' AND Siparis.Garsonu='" + siparisiKimGirdi + "' ORDER BY Porsiyon DESC");

                SqlDataReader dr = cmd.ExecuteReader();

                int siparisID, adisyonID;
                decimal porsiyon;

                dr.Read();

                adisyonID = dr.GetInt32(1);

                do
                {
                    siparisID = dr.GetInt32(0);

                    porsiyon = dr.GetDecimal(2);

                    if (porsiyon < istenilenIkramiptalSayisi) // elimizdeki ikramlar iptali istenenden küçükse
                    {
                        ikramUpdateTam(siparisID, 0);

                        istenilenIkramiptalSayisi -= porsiyon;
                    }
                    else if (porsiyon > istenilenIkramiptalSayisi) // den büyükse
                    {
                        ikramUpdateInsert(siparisID, adisyonID, porsiyon, dusulecekDeger, istenilenIkramiptalSayisi, listHesap.SelectedItems[0].SubItems[1].Text, 0);

                        istenilenIkramiptalSayisi = 0;
                    }
                    else // elimizde ikram edilmemişler ikramı istenene eşitse
                    {
                        ikramUpdateTam(siparisID, 0);

                        istenilenIkramiptalSayisi = 0;
                    }

                    if (istenilenIkramiptalSayisi == 0)
                    {
                        adisyonUpdateHesapveKalan(adisyonID);
                        break;
                    }
                } while (dr.Read());

                if (istenilenIkramiptalSayisi != 0)// ikram edilecekler daha bitmedi başka garsonların siparişlerinden ikram iptaline devam et
                {
                    cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adisyon.AdisyonID,Porsiyon FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.acikMi=1 AND Siparis.IkramMi=0 AND Siparis.IptalMi=0 AND Siparis.MasaAdi='" + MasaAdi + "' AND Siparis.DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND Siparis.YemekAdi='" + listHesap.SelectedItems[0].SubItems[1].Text + "' AND Siparis.Garsonu!='" + siparisiKimGirdi + "' ORDER BY Porsiyon DESC");

                    dr = cmd.ExecuteReader();

                    dr.Read();

                    adisyonID = dr.GetInt32(1);

                    do
                    {
                        siparisID = dr.GetInt32(0);

                        porsiyon = dr.GetDecimal(2);

                        if (porsiyon < istenilenIkramiptalSayisi) // elimizde ikram edilmemişler ikramı istenenden küçükse
                        {
                            ikramUpdateTam(siparisID, 0);

                            istenilenIkramiptalSayisi -= porsiyon;
                        }
                        else if (porsiyon > istenilenIkramiptalSayisi) // den büyükse
                        {
                            ikramUpdateInsert(siparisID, adisyonID, porsiyon, dusulecekDeger, istenilenIkramiptalSayisi, listHesap.SelectedItems[0].SubItems[1].Text, 0);

                            istenilenIkramiptalSayisi = 0;
                        }
                        else // elimizde ikram edilmemişler ikramı istenene eşitse
                        {
                            ikramUpdateTam(siparisID, 0);

                            istenilenIkramiptalSayisi = 0;
                        }

                        if (istenilenIkramiptalSayisi == 0)
                        {
                            adisyonUpdateHesapveKalan(adisyonID);
                            break;
                        }
                    } while (dr.Read());
                }

                if (listHesap.SelectedItems[0].Text == "0")
                {
                    listedeSeciliOlanItemlar.RemoveAt(listHesap.SelectedItems[0].Index);
                    listHesap.SelectedItems[0].Remove();
                }
            }

            for (int i = 0; i < listedeSeciliOlanItemlar.Count; i++)
            {
                listedeSeciliOlanItemlar[i] = false;
                listHesap.Items[i].Selected = false;
            }

            buttonUrunIkram.Enabled = false;
            buttonTasi.Enabled = false;
            buttonUrunIptal.Enabled = false;
            buttonAdd.Enabled = false;
            buttonindirim.Enabled = false;
            textBoxYuzde.Enabled = false;
            checkBoxYuzde.Enabled = false;

            textNumberOfItem.Text = "";

            if (labelToplamHesap.Text != "0,00") //hesapta para varsa butonu enable et
                buttonHesapOde.Enabled = true;
            else
                buttonHesapOde.Enabled = false; //yoksa disable et
        }

        // ürün iptal etme butonu
        private void buttonUrunIptal_Click(object sender, EventArgs e)
        {
            bool indirimSilindiMi = false;
            double carpan;
            if (textNumberOfItem.Text != "")
            {
                carpan = Convert.ToDouble(textNumberOfItem.Text);
                if (carpan > Convert.ToDouble(listHesap.SelectedItems[0].SubItems[0].Text))
                    carpan = Convert.ToDouble(listHesap.SelectedItems[0].SubItems[0].Text);
            }
            else
                carpan = Convert.ToDouble(listHesap.SelectedItems[0].SubItems[0].Text);

            if (carpan == 0)
                return;

            DialogResult eminMisiniz;

            using (KontrolFormu dialog = new KontrolFormu(carpan + " adet " + listHesap.SelectedItems[0].SubItems[1].Text + " iptal edilecek. Emin misiniz?", true))
            {
                eminMisiniz = dialog.ShowDialog();
            }

            if (eminMisiniz == DialogResult.No)
            {
                return;
            }

            double dusulecekDeger = Convert.ToDouble(listHesap.SelectedItems[0].SubItems[2].Text) / Convert.ToDouble(listHesap.SelectedItems[0].SubItems[0].Text);

            listHesap.SelectedItems[0].SubItems[0].Text = (Convert.ToDouble(listHesap.SelectedItems[0].SubItems[0].Text) - carpan).ToString();

            listHesap.SelectedItems[0].SubItems[2].Text = (Convert.ToDouble(listHesap.SelectedItems[0].SubItems[2].Text) - dusulecekDeger * carpan).ToString("0.00");

            if (listHesap.SelectedItems[0].Group == listHesap.Groups[2] || listHesap.SelectedItems[0].Group == listHesap.Groups[3])
            {
                labelKalanHesap.Text = (Convert.ToDouble(labelKalanHesap.Text) - (dusulecekDeger * carpan)).ToString("0.00");
            }

            labelToplamHesap.Text = (Convert.ToDouble(labelToplamHesap.Text) - (dusulecekDeger * carpan)).ToString("0.00");


            if (listHesap.SelectedItems[0].Group != listHesap.Groups[yeniSiparisler])
            {
                decimal istenilenSiparisiptalSayisi = (decimal)carpan;

                SqlCommand cmd;
                if (listHesap.SelectedItems[0].Group == listHesap.Groups[eskiSiparisler])
                {
                    cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adisyon.AdisyonID,Porsiyon FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.acikMi=1 AND Siparis.IkramMi=0 AND Siparis.IptalMi=0 AND Siparis.MasaAdi='" + MasaAdi + "' AND Siparis.DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND Siparis.YemekAdi='" + listHesap.SelectedItems[0].SubItems[1].Text + "' AND Siparis.Garsonu='" + siparisiKimGirdi + "' ORDER BY Porsiyon DESC");
                }
                else
                {
                    cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adisyon.AdisyonID,Porsiyon FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.acikMi=1 AND Siparis.IkramMi=1 AND Siparis.IptalMi=0 AND Siparis.MasaAdi='" + MasaAdi + "' AND Siparis.DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND Siparis.YemekAdi='" + listHesap.SelectedItems[0].SubItems[1].Text + "' AND Siparis.Garsonu='" + siparisiKimGirdi + "' ORDER BY Porsiyon DESC");
                }

                SqlDataReader dr = cmd.ExecuteReader();

                int siparisID, adisyonID;
                decimal porsiyon;

                dr.Read();

                adisyonID = dr.GetInt32(1);

                do
                {
                    siparisID = dr.GetInt32(0);

                    porsiyon = dr.GetDecimal(2);

                    if (porsiyon < istenilenSiparisiptalSayisi) // elimizdeki siparişler iptali istenenden küçükse
                    {
                        iptalUpdateTam(siparisID);

                        istenilenSiparisiptalSayisi -= porsiyon;
                    }
                    else if (porsiyon > istenilenSiparisiptalSayisi) // den büyükse
                    {
                        iptalUpdateInsert(siparisID, adisyonID, porsiyon, dusulecekDeger, istenilenSiparisiptalSayisi, listHesap.SelectedItems[0].SubItems[1].Text);

                        istenilenSiparisiptalSayisi = 0;
                    }
                    else // elimizdeki siparişler iptali istenene eşitse
                    {
                        iptalUpdateTam(siparisID);

                        istenilenSiparisiptalSayisi = 0;
                    }

                    if (istenilenSiparisiptalSayisi == 0)
                    {
                        adisyonUpdateHesapveKalan(adisyonID);
                        break;
                    }
                } while (dr.Read());

                if (istenilenSiparisiptalSayisi != 0)// iptal edilecekler daha bitmedi başka garsonların siparişlerinden iptale devam et
                {
                    cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adisyon.AdisyonID,Porsiyon FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.acikMi=1 AND Siparis.IkramMi=0 AND Siparis.IptalMi=0 AND Siparis.MasaAdi='" + MasaAdi + "' AND Siparis.DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND Siparis.YemekAdi='" + listHesap.SelectedItems[0].SubItems[1].Text + "' AND Siparis.Garsonu!='" + siparisiKimGirdi + "' ORDER BY Porsiyon DESC");

                    dr = cmd.ExecuteReader();

                    dr.Read();

                    adisyonID = dr.GetInt32(1);

                    do
                    {
                        siparisID = dr.GetInt32(0);

                        porsiyon = dr.GetDecimal(2);

                        if (porsiyon < istenilenSiparisiptalSayisi) // elimizdeki siparişler iptali istenenden küçükse
                        {
                            iptalUpdateTam(siparisID);

                            istenilenSiparisiptalSayisi -= porsiyon;
                        }
                        else if (porsiyon > istenilenSiparisiptalSayisi) // den büyükse
                        {
                            iptalUpdateInsert(siparisID, adisyonID, porsiyon, dusulecekDeger, istenilenSiparisiptalSayisi, listHesap.SelectedItems[0].SubItems[1].Text);

                            istenilenSiparisiptalSayisi = 0;
                        }
                        else // elimizdeki siparişler iptali istenene eşitse
                        {
                            iptalUpdateTam(siparisID);

                            istenilenSiparisiptalSayisi = 0;
                        }

                        if (istenilenSiparisiptalSayisi == 0)
                        {
                            adisyonUpdateHesapveKalan(adisyonID);
                            break;
                        }
                    } while (dr.Read());
                }
            }

            if (listHesap.SelectedItems[0].Text == "0")
            {
                listedeSeciliOlanItemlar.RemoveAt(listHesap.SelectedItems[0].Index);
                listHesap.SelectedItems[0].Remove();
            }

            if (labelToplamHesap.Text == "0,00")
                buttonHesapOde.Enabled = false;

            if (this.listHesap.Items.Count > 0)
            {
                int itemsCount = this.listHesap.Items.Count + 3;// 3 aslında grup sayısı -1
                int itemHeight = this.listHesap.Items[0].Bounds.Height;
                int VisiableItem = (int)this.listHesap.ClientRectangle.Height / itemHeight;
                if (itemsCount < VisiableItem)
                {
                    listHesap.Columns[1].Width = urunBoyu + 10;
                    listHesap.Columns[2].Width = fiyatBoyu + 10;
                }
            }

            for (int i = 0; i < listedeSeciliOlanItemlar.Count; i++)
            {
                listedeSeciliOlanItemlar[i] = false;
                listHesap.Items[i].Selected = false;
            }

            textNumberOfItem.Text = "";
            buttonUrunIkram.Enabled = false;
            buttonTasi.Enabled = false;
            buttonUrunIptal.Enabled = false;
            buttonAdd.Enabled = false;
            buttonindirim.Enabled = false;
            textBoxYuzde.Enabled = false;
            checkBoxYuzde.Enabled = false;
        }

        // ürüne ekleme yapma butonu
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
            for (int i = 0; i < listHesap.Groups[yeniSiparisler].Items.Count; i++)
            {
                if (listHesap.SelectedItems[0].SubItems[1].Text == listHesap.Groups[yeniSiparisler].Items[i].SubItems[1].Text)
                {
                    gruptaYeniGelenSiparisVarmi = i;
                    break;
                }
            }

            decimal eklenecekDeger = Convert.ToDecimal(listHesap.SelectedItems[0].SubItems[2].Text) / Convert.ToDecimal(listHesap.SelectedItems[0].SubItems[0].Text);

            if (gruptaYeniGelenSiparisVarmi == -1) //yoksa ürünü hesaba ekle
            {
                listHesap.Items.Add(kacPorsiyon.ToString());

                listHesap.Items[listHesap.Items.Count - 1].SubItems.Add(listHesap.SelectedItems[0].SubItems[1].Text);

                listHesap.Items[listHesap.Items.Count - 1].SubItems.Add(((decimal)kacPorsiyon * eklenecekDeger).ToString("0.00"));
                listHesap.Items[listHesap.Items.Count - 1].Group = listHesap.Groups[yeniSiparisler];
                listHesap.Items[listHesap.Items.Count - 1].Font = new Font("Calibri", 18.75F, FontStyle.Bold);
                listedeSeciliOlanItemlar.Add(false);

                //eğer ürün eklendiğinde ekrana sığmıyorsa scroll gösterilecektir, bu yüzden fiyatları sola kaydırıyoruz
                int itemsCount = this.listHesap.Items.Count + 3;// 3 aslında grup sayısı -1
                int itemHeight = this.listHesap.Items[0].Bounds.Height;
                int VisiableItem = (int)this.listHesap.ClientRectangle.Height / itemHeight;

                if (itemsCount >= VisiableItem)
                {
                    listHesap.Columns[1].Width = urunBoyu;
                    listHesap.Columns[2].Width = fiyatBoyu;

                    for (int i = 0; i < listHesap.Items.Count; i++)
                    {
                        while (listHesap.Columns[0].Width < System.Windows.Forms.TextRenderer.MeasureText(listHesap.Items[i].SubItems[0].Text, new Font(listHesap.Items[i].Font.FontFamily, listHesap.Items[i].Font.Size, listHesap.Items[i].Font.Style)).Width)
                        {
                            listHesap.Items[i].Font = new Font(listHesap.Items[i].Font.FontFamily, listHesap.Items[i].Font.Size - 0.5f, listHesap.Items[i].Font.Style);
                        }
                        while (listHesap.Columns[1].Width < System.Windows.Forms.TextRenderer.MeasureText(listHesap.Items[i].SubItems[1].Text, new Font(listHesap.Items[i].Font.FontFamily, listHesap.Items[i].Font.Size, listHesap.Items[i].Font.Style)).Width)
                        {
                            listHesap.Items[i].Font = new Font(listHesap.Items[i].Font.FontFamily, listHesap.Items[i].Font.Size - 0.5f, listHesap.Items[i].Font.Style);
                        }

                        while (listHesap.Columns[2].Width < System.Windows.Forms.TextRenderer.MeasureText(listHesap.Items[i].SubItems[2].Text, new Font(listHesap.Items[i].Font.FontFamily, listHesap.Items[i].Font.Size, listHesap.Items[i].Font.Style)).Width)
                        {
                            listHesap.Items[i].Font = new Font(listHesap.Items[i].Font.FontFamily, listHesap.Items[i].Font.Size - 0.5f, listHesap.Items[i].Font.Style);
                        }
                    }
                }

                while (listHesap.Columns[1].Width < System.Windows.Forms.TextRenderer.MeasureText(listHesap.SelectedItems[0].SubItems[2].Text, listHesap.Items[listHesap.Items.Count - 1].Font).Width
                    || listHesap.Columns[2].Width < System.Windows.Forms.TextRenderer.MeasureText(((decimal)kacPorsiyon * eklenecekDeger).ToString("0.00"), listHesap.Items[listHesap.Items.Count - 1].Font).Width
                    || listHesap.Columns[0].Width < System.Windows.Forms.TextRenderer.MeasureText(kacPorsiyon.ToString(), listHesap.Items[listHesap.Items.Count - 1].Font).Width)
                {
                    listHesap.Items[listHesap.Items.Count - 1].Font = new Font(listHesap.Items[listHesap.Items.Count - 1].Font.FontFamily, listHesap.Items[listHesap.Items.Count - 1].Font.Size - 0.5f, listHesap.Items[listHesap.Items.Count - 1].Font.Style);
                }

                labelToplamHesap.Text = (Convert.ToDecimal(labelToplamHesap.Text) + Convert.ToDecimal((listHesap.Items[listHesap.Items.Count - 1].SubItems[2].Text))).ToString("0.00");
                labelKalanHesap.Text = (Convert.ToDecimal(labelKalanHesap.Text) + Convert.ToDecimal((listHesap.Items[listHesap.Items.Count - 1].SubItems[2].Text))).ToString("0.00");
            }
            else // varsa ürünün hesaptaki değerlerini istenilene göre arttır
            {
                listHesap.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text = (Convert.ToDouble(listHesap.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text) + kacPorsiyon).ToString();

                listHesap.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[2].Text = (Convert.ToDecimal(listHesap.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[2].Text) + (decimal)kacPorsiyon * eklenecekDeger).ToString("0.00");

                while (listHesap.Columns[2].Width < System.Windows.Forms.TextRenderer.MeasureText(listHesap.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[2].Text, listHesap.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].Font).Width
                    || listHesap.Columns[0].Width < System.Windows.Forms.TextRenderer.MeasureText(listHesap.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text, listHesap.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].Font).Width)
                {
                    listHesap.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].Font = new Font(listHesap.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].Font.FontFamily, listHesap.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].Font.Size - 0.5f, listHesap.Groups[yeniSiparisler].Items[gruptaYeniGelenSiparisVarmi].Font.Style);
                }

                labelToplamHesap.Text = (Convert.ToDecimal(labelToplamHesap.Text) + (decimal)kacPorsiyon * eklenecekDeger).ToString("0.00");
                labelKalanHesap.Text = (Convert.ToDecimal(labelKalanHesap.Text) + (decimal)kacPorsiyon * eklenecekDeger).ToString("0.00");
            }
            textNumberOfItem.Text = ""; // çarpanı sil

            if (labelToplamHesap.Text != "0,00") //hesapta para varsa butonu enable et
                buttonHesapOde.Enabled = true;
        }

        //çarpan özellikleri
        private void keyPressedOnYuzdeText(object sender, KeyPressEventArgs e)
        {

            if (checkBoxYuzde.Checked)
            {
                try
                {
                    if (Convert.ToDouble(textBoxYuzde.Text) > 100)
                    {
                        textBoxYuzde.Text = "100";
                        e.Handled = true;
                        return;
                    }
                }
                catch { }
            }

            if (textBoxYuzde.Text == String.Empty)
            {
                if (e.KeyChar == '0')
                    e.Handled = true;
            }

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }
            else if (e.KeyChar == ',' && (sender as TextBox).Text.IndexOf(',') > -1)
            { // 1 kere , kullanmasına izin ver
                e.Handled = true;
            }
        }

        private void textBoxYuzde_Leave(object sender, EventArgs e)
        {
            if (checkBoxYuzde.Checked)
            {
                if (Convert.ToDouble(textBoxYuzde.Text) > 100)
                {
                    textBoxYuzde.Text = "100";
                }
            }
        }

        private void checkBoxYuzde_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxYuzde.Checked)
            {
                checkBoxYuzde.Text = "%";
                try
                {
                    if (Convert.ToDouble(textBoxYuzde.Text) > 100)
                    {
                        textBoxYuzde.Text = "100";
                    }
                }
                catch { }
                textBoxYuzde.MaxLength = 5;
            }
            else
            {
                checkBoxYuzde.Text = "TL";
                textBoxYuzde.MaxLength = 7;
            }
        }

        // sipariş işlemleri bittiğinde basılan buton
        private void buttonTamam_Click(object sender, EventArgs e)
        {
            //Burda sqle not hesap vs yazılacak..
            SqlCommand cmd;

            if (listHesap.Items.Count == 0)
            {
                int adisyonID = -6;
                try // açık
                {
                    cmd = SQLBaglantisi.getCommand("SELECT AdisyonID FROM Adisyon WHERE acikMi=1 AND IptalMi=0 AND MasaAdi='" + MasaAdi + "' AND DepartmanAdi='" + hangiDepartman.departmanAdi + "'");
                    SqlDataReader dr = cmd.ExecuteReader();
                    dr.Read();
                    adisyonID = dr.GetInt32(0);

                    cmd = SQLBaglantisi.getCommand("SELECT AdisyonID FROM Siparis WHERE IptalMi=0 MasaAdi='" + MasaAdi + "' AND AdisyonID='" + adisyonID + "'");
                    SqlDataReader dr2 = cmd.ExecuteReader();
                    dr2.GetInt32(0);
                }
                catch // kapalı
                {
                    if (adisyonID != -6)
                    {
                        masaAcikMi = false;
                        adisyonIptal(adisyonID);
                    }
                }
                this.Close();
            }
            else
            {
                try // açık
                {
                    cmd = SQLBaglantisi.getCommand("SELECT acikMi FROM Adisyon WHERE MasaAdi='" + MasaAdi + "' AND DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND acikMi=1");
                    SqlDataReader dr = cmd.ExecuteReader();
                    dr.Read();
                    dr.GetBoolean(0);
                    masaAcikMi = true;
                }
                catch // kapalı
                {
                    adisyonOlustur();
                }

                foreach (ListViewItem siparis in listHesap.Groups[yeniSiparisler].Items)
                {
                    cmd = SQLBaglantisi.getCommand("SELECT AdisyonID FROM Adisyon WHERE MasaAdi='" + MasaAdi + "' AND DepartmanAdi='" + hangiDepartman.departmanAdi + "' AND acikMi=1");
                    SqlDataReader dr = cmd.ExecuteReader();

                    dr.Read();

                    int adisyonID = dr.GetInt32(0);

                    adisyonUpdateHesapveKalan(adisyonID);

                    siparisOlustur(adisyonID, siparis);
                }
                this.Close();
            }
        }

        #region SQL İşlemleri
        public void adisyonOlustur()
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("INSERT INTO Adisyon(acikMi,AdisyonNotu,AcilisZamani,DepartmanAdi,MasaAdi,ToplamHesap,KalanHesap) VALUES(@_acikMi,@_AdisyonNotu,@_AcilisZamani,@_DepartmanAdi,@_MasaAdi,@_ToplamHesap,@_KalanHesap)");

            cmd.Parameters.AddWithValue("@_acikmi", 1);
            cmd.Parameters.AddWithValue("@_AdisyonNotu", adisyonNotu);
            cmd.Parameters.AddWithValue("@_AcilisZamani", acilisZamani);
            cmd.Parameters.AddWithValue("@_DepartmanAdi", hangiDepartman.departmanAdi);
            cmd.Parameters.AddWithValue("@_MasaAdi", MasaAdi);
            cmd.Parameters.AddWithValue("@_ToplamHesap", Convert.ToDecimal(labelToplamHesap.Text));
            cmd.Parameters.AddWithValue("@_KalanHesap", Convert.ToDecimal(labelKalanHesap.Text));
            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            masaAcikMi = true;
        }

        public void siparisOlustur(int adisyonID, ListViewItem siparis)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("INSERT INTO Siparis(AdisyonID,Garsonu,DepartmanAdi,MasaAdi,Fiyatı,Porsiyon,YemekAdi,VerilisTarihi) VALUES(@_AdisyonID,@_Garsonu,@_DepartmanAdi,@_MasaAdi,@_Fiyatı,@_Porsiyon,@_YemekAdi,@_VerilisTarihi)");
            cmd.Parameters.AddWithValue("@_AdisyonID", adisyonID);
            cmd.Parameters.AddWithValue("@_Garsonu", siparisiKimGirdi);
            cmd.Parameters.AddWithValue("@_DepartmanAdi", hangiDepartman.departmanAdi);
            cmd.Parameters.AddWithValue("@_MasaAdi", MasaAdi);
            cmd.Parameters.AddWithValue("@_Fiyatı", Convert.ToDecimal(siparis.SubItems[2].Text) / Convert.ToDecimal(siparis.SubItems[0].Text));
            cmd.Parameters.AddWithValue("@_Porsiyon", Convert.ToDecimal(siparis.SubItems[0].Text));
            cmd.Parameters.AddWithValue("@_YemekAdi", siparis.SubItems[1].Text);
            cmd.Parameters.AddWithValue("@_VerilisTarihi", DateTime.Now);

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void ikramUpdateInsert(int siparisID, int adisyonID, decimal porsiyon, double dusulecekDeger, decimal ikramAdedi, string yemekAdi, int ikramMi)
        {
            decimal yeniPorsiyonAdetiSiparis = porsiyon - ikramAdedi;

            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET Porsiyon = @Porsiyonu WHERE SiparisID=@id");
            cmd.Parameters.AddWithValue("@Porsiyonu", yeniPorsiyonAdetiSiparis);
            cmd.Parameters.AddWithValue("@id", siparisID);
            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            SqlCommand cmd2 = SQLBaglantisi.getCommand("INSERT INTO Siparis(AdisyonID,Garsonu,DepartmanAdi,MasaAdi,Fiyatı,Porsiyon,YemekAdi,IkramMi,VerilisTarihi) values(@_AdisyonID,@_Garsonu,@_DepartmanAdi,@_MasaAdi,@_Fiyatı,@_Porsiyon,@_YemekAdi,@_IkramMi,@_VerilisTarihi)");
            cmd2.Parameters.AddWithValue("@_AdisyonID", adisyonID);
            cmd2.Parameters.AddWithValue("@_Garsonu", siparisiKimGirdi);
            cmd2.Parameters.AddWithValue("@_DepartmanAdi", hangiDepartman.departmanAdi);
            cmd2.Parameters.AddWithValue("@_MasaAdi", MasaAdi);
            cmd2.Parameters.AddWithValue("@_Fiyatı", dusulecekDeger);
            cmd2.Parameters.AddWithValue("@_Porsiyon", ikramAdedi);
            cmd2.Parameters.AddWithValue("@_YemekAdi", yemekAdi);
            cmd2.Parameters.AddWithValue("@_IkramMi", ikramMi);
            cmd2.Parameters.AddWithValue("@_VerilisTarihi", DateTime.Now);

            cmd2.ExecuteNonQuery();

            cmd2.Connection.Close();
            cmd2.Connection.Dispose();
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

        public void ikramInsert(int adisyonID, double dusulecekDeger, decimal ikramAdedi, string yemekAdi)
        {
            SqlCommand cmd2 = SQLBaglantisi.getCommand("INSERT INTO Siparis(AdisyonID,Garsonu,DepartmanAdi,MasaAdi,Fiyatı,Porsiyon,YemekAdi,IkramMi,VerilisTarihi) values(@_AdisyonID,@_Garsonu,@_DepartmanAdi,@_MasaAdi,@_Fiyatı,@_Porsiyon,@_YemekAdi,@_IkramMi,@_VerilisTarihi)");
            cmd2.Parameters.AddWithValue("@_AdisyonID", adisyonID);
            cmd2.Parameters.AddWithValue("@_Garsonu", siparisiKimGirdi);
            cmd2.Parameters.AddWithValue("@_DepartmanAdi", hangiDepartman.departmanAdi);
            cmd2.Parameters.AddWithValue("@_MasaAdi", MasaAdi);
            cmd2.Parameters.AddWithValue("@_Fiyatı", dusulecekDeger);
            cmd2.Parameters.AddWithValue("@_Porsiyon", ikramAdedi);
            cmd2.Parameters.AddWithValue("@_YemekAdi", yemekAdi);
            cmd2.Parameters.AddWithValue("@_IkramMi", 1);
            cmd2.Parameters.AddWithValue("@_VerilisTarihi", DateTime.Now);
            cmd2.ExecuteNonQuery();

            cmd2.Connection.Close();
            cmd2.Connection.Dispose();
        }

        public void adisyonUpdateHesapveKalan(int adisyonID)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET ToplamHesap=@hesap, KalanHesap=@kalan WHERE AdisyonID=@id");
            cmd.Parameters.AddWithValue("@id", adisyonID);
            cmd.Parameters.AddWithValue("@hesap", Convert.ToDecimal(labelToplamHesap.Text));
            cmd.Parameters.AddWithValue("@kalan", Convert.ToDecimal(labelKalanHesap.Text));
            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void adisyonIptal(int adisyonID)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET acikMi=@acik, IptalMi=@iptal WHERE AdisyonID=@id");
            cmd.Parameters.AddWithValue("@acik", 0);
            cmd.Parameters.AddWithValue("@iptal", 1);
            cmd.Parameters.AddWithValue("@id", adisyonID);

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void iptalUpdateInsert(int siparisID, int adisyonID, decimal porsiyon, double dusulecekDeger, decimal iptalAdedi, string yemekAdi)
        {
            decimal yeniPorsiyonAdetiSiparis = porsiyon - iptalAdedi;

            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET Porsiyon = @Porsiyonu WHERE SiparisID=@id");
            cmd.Parameters.AddWithValue("@Porsiyonu", yeniPorsiyonAdetiSiparis);
            cmd.Parameters.AddWithValue("@id", siparisID);
            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            SqlCommand cmd2 = SQLBaglantisi.getCommand("INSERT INTO Siparis(AdisyonID,Garsonu,DepartmanAdi,MasaAdi,Fiyatı,Porsiyon,YemekAdi,IptalMi,VerilisTarihi) values(@_AdisyonID,@_Garsonu,@_DepartmanAdi,@_MasaAdi,@_Fiyatı,@_Porsiyon,@_YemekAdi,@_IptalMi,@_VerilisTarihi)");
            cmd2.Parameters.AddWithValue("@_AdisyonID", adisyonID);
            cmd2.Parameters.AddWithValue("@_Garsonu", siparisiKimGirdi);
            cmd2.Parameters.AddWithValue("@_DepartmanAdi", hangiDepartman.departmanAdi);
            cmd2.Parameters.AddWithValue("@_MasaAdi", MasaAdi);
            cmd2.Parameters.AddWithValue("@_Fiyatı", dusulecekDeger);
            cmd2.Parameters.AddWithValue("@_Porsiyon", iptalAdedi);
            cmd2.Parameters.AddWithValue("@_YemekAdi", yemekAdi);
            cmd2.Parameters.AddWithValue("@_IptalMi", 1);
            cmd2.Parameters.AddWithValue("@_VerilisTarihi", DateTime.Now);

            cmd2.ExecuteNonQuery();

            cmd2.Connection.Close();
            cmd2.Connection.Dispose();
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







        public void indirimUpdateInsert(int siparisID, int adisyonID, decimal porsiyon, double dusulecekDeger, decimal indirimMiktari, string yemekAdi)
        {
            decimal yeniPorsiyonAdetiSiparis = porsiyon - indirimMiktari;

            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET Porsiyon = @Porsiyonu WHERE SiparisID=@id");
            cmd.Parameters.AddWithValue("@Porsiyonu", yeniPorsiyonAdetiSiparis);
            cmd.Parameters.AddWithValue("@id", siparisID);
            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            SqlCommand cmd2 = SQLBaglantisi.getCommand("INSERT INTO Siparis(AdisyonID,Garsonu,DepartmanAdi,MasaAdi,Fiyatı,Porsiyon,YemekAdi,IptalMi,VerilisTarihi) values(@_AdisyonID,@_Garsonu,@_DepartmanAdi,@_MasaAdi,@_Fiyatı,@_Porsiyon,@_YemekAdi,@_IptalMi,@_VerilisTarihi)");
            cmd2.Parameters.AddWithValue("@_AdisyonID", adisyonID);
            cmd2.Parameters.AddWithValue("@_Garsonu", siparisiKimGirdi);
            cmd2.Parameters.AddWithValue("@_DepartmanAdi", hangiDepartman.departmanAdi);
            cmd2.Parameters.AddWithValue("@_MasaAdi", MasaAdi);
            cmd2.Parameters.AddWithValue("@_Fiyatı", dusulecekDeger);
            cmd2.Parameters.AddWithValue("@_Porsiyon", indirimMiktari);
            cmd2.Parameters.AddWithValue("@_YemekAdi", yemekAdi);
            cmd2.Parameters.AddWithValue("@_IptalMi", 1);
            cmd2.Parameters.AddWithValue("@_VerilisTarihi", DateTime.Now);

            cmd2.ExecuteNonQuery();

            cmd2.Connection.Close();
            cmd2.Connection.Dispose();
        }

        public void indirimUpdateTam(int siparisID)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET IptalMi=@iptal, Garsonu=@Garson WHERE SiparisID=@id");
            cmd.Parameters.AddWithValue("@iptal", 1);
            cmd.Parameters.AddWithValue("@Garson", siparisiKimGirdi);
            cmd.Parameters.AddWithValue("@id", siparisID);
            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }
        public void indirim(int adisyonID, double dusulecekDeger, decimal indirimMiktari, string yemekAdi)
        {
            SqlCommand cmd2 = SQLBaglantisi.getCommand("INSERT INTO Siparis(AdisyonID,Garsonu,DepartmanAdi,MasaAdi,Fiyatı,Porsiyon,YemekAdi,IkramMi,VerilisTarihi) values(@_AdisyonID,@_Garsonu,@_DepartmanAdi,@_MasaAdi,@_Fiyatı,@_Porsiyon,@_YemekAdi,@_IkramMi,@_VerilisTarihi)");
            cmd2.Parameters.AddWithValue("@_AdisyonID", adisyonID);
            cmd2.Parameters.AddWithValue("@_Garsonu", siparisiKimGirdi);
            cmd2.Parameters.AddWithValue("@_DepartmanAdi", hangiDepartman.departmanAdi);
            cmd2.Parameters.AddWithValue("@_MasaAdi", MasaAdi);
            cmd2.Parameters.AddWithValue("@_Fiyatı", dusulecekDeger);
            cmd2.Parameters.AddWithValue("@_Porsiyon", indirimMiktari);
            cmd2.Parameters.AddWithValue("@_YemekAdi", yemekAdi);
            cmd2.Parameters.AddWithValue("@_IkramMi", 1);
            cmd2.Parameters.AddWithValue("@_VerilisTarihi", DateTime.Now);
            cmd2.ExecuteNonQuery();

            cmd2.Connection.Close();
            cmd2.Connection.Dispose();
        }



        #endregion





        //ödeme kısmına geçiş butonu
        private void paymentButton_Click(object sender, EventArgs e)
        {
            //ödendiğinde sql de ödendi flagini 1 yap 
        }

        //Masaların adisyonlarını değiştiren method
        private void changeTablesButton_Click(object sender, EventArgs e)
        {
            //burada bir split viewa departman ve masalarını koy seçilen departmana göre masa ve departman adını değiştir.
        }

        //Seçili siparişlerin adisyonunu değiştiren method
        private void buttonTasi_Click(object sender, EventArgs e)
        {
            //burada bir split viewa departman ve masalarını koy seçilen departmanın masasının hesabına seçilen siparişleri ekle(hesap yoksa yeni oluştur varsa ekle), burdakinden çıkar

        }

        //Adisyona eklenecek indirim
        private void buttonindirim_Click(object sender, EventArgs e)
        {
            double carpan;
            if (textBoxYuzde.Text != "")
                carpan = Convert.ToDouble(textBoxYuzde.Text);
            else
                carpan = 1;

            if (carpan == 0)
                return;

            // indirim ekle
            if (buttonindirim.Text == "  İndirim")
            {
                if (checkBoxYuzde.Checked) // indirim hesaba yüzdesel olarak yansıtılacaksa
                {
                    try
                    {
                        if (listHesap.Groups[indirimler].Items[0].SubItems[0].Text != "") // 1. indirim satırı % li mi bak değilse ya yoktur yada sabit bir miktardır, catche gir
                        {
                            double toplamIndirim = carpan + Convert.ToDouble(listHesap.Groups[indirimler].Items[0].SubItems[0].Text);

                            if(toplamIndirim > 100)
                            {
                                DialogResult eminMisiniz;

                                using (KontrolFormu dialog = new KontrolFormu("Toplam indirim hesabın tutarını geçiyor. Hesabın tamamını indirimli yapmak istediğinize emin misiniz?", true))
                                {
                                    eminMisiniz = dialog.ShowDialog();
                                }

                                if (eminMisiniz == DialogResult.No)
                                {
                                    return;
                                }

                                toplamIndirim = 100;
                            }

                            listHesap.Groups[indirimler].Items[0].SubItems[0].Text = (toplamIndirim).ToString();
                            listHesap.Groups[indirimler].Items[0].SubItems[2].Text = (Convert.ToDouble(labelToplamHesap.Text) / 100 * Convert.ToDouble(listHesap.Groups[indirimler].Items[0].SubItems[0].Text)).ToString();
                        }                        
                    }
                    catch
                    {
                        try
                        {
                            if (listHesap.Groups[indirimler].Items[1].SubItems[0].Text != "") // 2. indirim satırı % li mi bak değilse yoktur, catche gir
                            {
                                double toplamIndirim = carpan + Convert.ToDouble(listHesap.Groups[indirimler].Items[1].SubItems[0].Text);

                                if (toplamIndirim > 100)
                                {
                                    DialogResult eminMisiniz;

                                    using (KontrolFormu dialog = new KontrolFormu("Toplam indirim hesabın tutarını geçiyor. Hesabın tamamını indirimli yapmak istediğinize emin misiniz?", true))
                                    {
                                        eminMisiniz = dialog.ShowDialog();
                                    }

                                    if (eminMisiniz == DialogResult.No)
                                    {
                                        return;
                                    }

                                    toplamIndirim = 100;
                                }

                                listHesap.Groups[indirimler].Items[1].SubItems[0].Text = (toplamIndirim).ToString();
                                listHesap.Groups[indirimler].Items[1].SubItems[2].Text = (Convert.ToDouble(labelToplamHesap.Text) / 100 * Convert.ToDouble(listHesap.Groups[indirimler].Items[1].SubItems[0].Text)).ToString();
                            }                         
                        }
                        catch
                        {
                            listHesap.Groups[indirimler].Items.Add(carpan.ToString());
                            listHesap.Groups[indirimler].Items[1].SubItems.Add("İndirim %");
                            listHesap.Groups[indirimler].Items[1].SubItems.Add((Convert.ToDouble(labelToplamHesap.Text) / 100 * carpan).ToString());
                        }
                    }
                }
                else // indirim hesaba sabit bir miktar olarak yansıtılacaksa
                {
                    try
                    {
                        if (listHesap.Groups[indirimler].Items[0].SubItems[0].Text == "") // 1. indirim satırı sabit mi bak değilse ya yoktur yada % li bir miktardır, catche gir
                        {
                            double toplamIndirim = carpan + Convert.ToDouble(listHesap.Groups[indirimler].Items[0].SubItems[0].Text);

                            if (toplamIndirim > Convert.ToDouble(labelToplamHesap.Text))
                            {
                                DialogResult eminMisiniz;

                                using (KontrolFormu dialog = new KontrolFormu("Toplam indirim hesabın tutarını geçiyor. Hesabın tamamını indirimli yapmak istediğinize emin misiniz?", true))
                                {
                                    eminMisiniz = dialog.ShowDialog();
                                }

                                if (eminMisiniz == DialogResult.No)
                                {
                                    return;
                                }

                                toplamIndirim = 100;
                            }
                            listHesap.Groups[indirimler].Items[0].SubItems[2].Text = (Convert.ToDouble(listHesap.Groups[indirimler].Items[0].SubItems[2].Text)+carpan).ToString();
                        }
                    }
                    catch
                    {
                        try
                        {
                            if (listHesap.Groups[indirimler].Items[1].SubItems[0].Text == "") // 2. indirim satırı sabit mi bak değilse yoktur, catche gir
                            {
                                listHesap.Groups[indirimler].Items[1].SubItems[2].Text = (Convert.ToDouble(listHesap.Groups[indirimler].Items[1].SubItems[2].Text) + carpan).ToString();
                            }
                        }
                        catch
                        {
                            listHesap.Groups[indirimler].Items.Add("");
                            listHesap.Groups[indirimler].Items[1].SubItems.Add("İndirim Sabit");
                            listHesap.Groups[indirimler].Items[1].SubItems.Add(carpan.ToString());
                        }
                    }
                }
            }
            else//indirim kaldır. indirim kaldırırken checkboxyuzdenin bir önemi yok seçilen neyse ona göre işlem yapılır. 
            {
                if(textBoxYuzde.Text != "") // indirimin istenilen kadarını sil
                {
                    if (listHesap.SelectedItems[0].SubItems[0].Text != "") // indirim hesaptan yüzdesel olarak düşülecekse
                    {
                        if (carpan > Convert.ToDouble(listHesap.SelectedItems[0].SubItems[0].Text))
                            carpan = Convert.ToDouble(listHesap.SelectedItems[0].SubItems[0].Text);
                        
                        try
                        {
                            if (listHesap.Groups[indirimler].Items[0].SubItems[0].Text != "") // 1. indirim satırı % li mi bak değilse ya yoktur yada sabit bir miktardır, catche gir
                            {
                                listHesap.Groups[indirimler].Items[0].SubItems[0].Text = (Convert.ToDouble(listHesap.Groups[indirimler].Items[0].SubItems[0].Text) + carpan).ToString();
                                listHesap.Groups[indirimler].Items[0].SubItems[2].Text = (Convert.ToDouble(labelToplamHesap.Text) / 100 * Convert.ToDouble(listHesap.Groups[indirimler].Items[0].SubItems[0].Text)).ToString();
                            }
                        }
                        catch
                        {
                            try
                            {
                                if (listHesap.Groups[indirimler].Items[1].SubItems[0].Text != "") // 2. indirim satırı % li mi bak değilse yoktur, catche gir
                                {
                                    listHesap.Groups[indirimler].Items[1].SubItems[0].Text = (Convert.ToDouble(listHesap.Groups[indirimler].Items[1].SubItems[0].Text) + carpan).ToString();
                                    listHesap.Groups[indirimler].Items[1].SubItems[2].Text = (Convert.ToDouble(labelToplamHesap.Text) / 100 * Convert.ToDouble(listHesap.Groups[indirimler].Items[1].SubItems[0].Text)).ToString();
                                }
                            }
                            catch
                            {
                                listHesap.Groups[indirimler].Items.Add(carpan.ToString());
                                listHesap.Groups[indirimler].Items[1].SubItems.Add("İndirim %");
                                listHesap.Groups[indirimler].Items[1].SubItems.Add((Convert.ToDouble(labelToplamHesap.Text) / 100 * carpan).ToString());
                            }
                        }
                    }
                    else // indirim hesaptan sabit bir miktar olarak düşülecekse
                    {
                        try
                        {
                            if (listHesap.Groups[indirimler].Items[0].SubItems[0].Text == "") // 1. indirim satırı sabit mi bak değilse ya yoktur yada % li bir miktardır, catche gir
                            {
                                listHesap.Groups[indirimler].Items[0].SubItems[2].Text = (Convert.ToDouble(listHesap.Groups[indirimler].Items[0].SubItems[2].Text) + carpan).ToString();
                            }
                        }
                        catch
                        {
                            try
                            {
                                if (listHesap.Groups[indirimler].Items[1].SubItems[0].Text == "") // 2. indirim satırı sabit mi bak değilse yoktur, catche gir
                                {
                                    listHesap.Groups[indirimler].Items[1].SubItems[2].Text = (Convert.ToDouble(listHesap.Groups[indirimler].Items[1].SubItems[2].Text) + carpan).ToString();
                                }
                            }
                            catch
                            {
                                listHesap.Groups[indirimler].Items.Add("");
                                listHesap.Groups[indirimler].Items[1].SubItems.Add("İndirim Sabit");
                                listHesap.Groups[indirimler].Items[1].SubItems.Add(carpan.ToString());
                            }
                        }
                    }
                }
                else // indirimin tamamını sil
                {

                }
            }
        }
    }
}