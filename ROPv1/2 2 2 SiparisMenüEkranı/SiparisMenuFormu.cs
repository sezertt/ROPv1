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

namespace ROPv1
{
    public partial class SiparisMenuFormu : Form
    {
        Restoran hangiDepartman;

        bool grupOlustur = true;

        int hangiMenuSecili = 0, scrollPosition = 0, elemanBoyu, gorunenListeElemaniSayisi, atlama;

        const int urunBoyu = 220, fiyatBoyu = 90;

        string siparisiKimGirdi;

        List<Menuler> menuListesi = new List<Menuler>();  // menüleri tutacak liste

        List<UrunOzellikleri> urunListesi = new List<UrunOzellikleri>();

        List<bool> listedeSeciliOlanItemlar = new List<bool>();

        public SiparisMenuFormu(string masaninAdi, Restoran butonBilgileri, string siparisiGirenKisi)
        {
            InitializeComponent();

            //siparişi vermek için pin giren kisinin bilgisi
            siparisiKimGirdi = siparisiGirenKisi;

            //hangi departmanda olduğumuzu tutan değişken
            hangiDepartman = butonBilgileri;

            labelMasa.Text = "Masa: " + masaninAdi;
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
        }

        //kategori seçildiğinde kategori içindeki ürünleri panele getiren method
        private void menuBasliklariButonlari_Click(object sender, EventArgs e)
        {
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

        //form load
        private void SiparisMenuFormu_Load(object sender, EventArgs e)
        {
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
                int itemsCount = this.listHesap.Items.Count + this.listHesap.Groups.Count;
                int itemHeight = this.listHesap.Items[0].Bounds.Height;
                int VisiableItem = (int)this.listHesap.ClientRectangle.Height / itemHeight;
                if (itemsCount >= VisiableItem)
                {
                    listHesap.Columns[1].Width = urunBoyu;
                    listHesap.Columns[2].Width = fiyatBoyu;
                    buttonUrunListUp.Visible = true;
                    buttonUrunListDown.Visible = true;
                }
                else
                {
                    buttonUrunListUp.Visible = false;
                    buttonUrunListDown.Visible = false;
                }
            }
            else
            {
                buttonUrunListUp.Visible = false;
                buttonUrunListDown.Visible = false;
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

            if (grupOlustur) // masaya yeni bir giriş yapıldıysa saatini ve kişi tutmak için grup oluştur
            {
                grupOlustur = false;
                listHesap.Groups.Add("siparis", siparisiKimGirdi + " - " + DateTime.Now.ToShortTimeString());
                listHesap.Groups[listHesap.Groups.Count - 1].Tag = listHesap.Groups.Count - 1;
            }

            int kacinciGrup = listHesap.Groups.Count - 1;

            int gruptaYeniGelenSiparisVarmi = -1; //ürün cinsi hesapta var mı bak 
            for (int i = 0; i < listHesap.Groups[kacinciGrup].Items.Count; i++)
            {
                if (((Button)sender).Text == listHesap.Groups[kacinciGrup].Items[i].SubItems[1].Text)
                {
                    gruptaYeniGelenSiparisVarmi = i;
                    break;
                }
            }

            if (gruptaYeniGelenSiparisVarmi == -1) //yoksa ürünü hesaba ekle
            {
                listHesap.Items.Add("" + kacPorsiyon);
                listHesap.Items[listHesap.Items.Count - 1].SubItems.Add(((Button)sender).Text);
                listHesap.Items[listHesap.Items.Count - 1].SubItems.Add(((double)kacPorsiyon * Convert.ToDouble(urunListesi[hangiMenuSecili].porsiyonFiyati[Convert.ToInt32(((Button)sender).Tag)])).ToString("0.00"));
                listHesap.Items[listHesap.Items.Count - 1].Group = listHesap.Groups[kacinciGrup];
                listHesap.Items[listHesap.Items.Count - 1].Font = new Font("Calibri", 18.75F, FontStyle.Bold);
                listedeSeciliOlanItemlar.Add(false);

                //eğer ürün eklendiğinde ekrana sığmıyorsa scroll gösterilecektir, bu yüzden fiyatları sola kaydırıyoruz
                int itemsCount = this.listHesap.Items.Count + this.listHesap.Groups.Count;
                int itemHeight = this.listHesap.Items[0].Bounds.Height;
                int VisiableItem = (int)this.listHesap.ClientRectangle.Height / itemHeight;
                if (itemsCount >= VisiableItem)
                {
                    listHesap.Columns[1].Width = urunBoyu;
                    listHesap.Columns[2].Width = fiyatBoyu;
                    buttonUrunListUp.Visible = true;
                    buttonUrunListDown.Visible = true;

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

                elemanBoyu = this.listHesap.Items[0].Bounds.Height;
                gorunenListeElemaniSayisi = (int)this.listHesap.ClientRectangle.Height / elemanBoyu;
                atlama = gorunenListeElemaniSayisi;

                while (listHesap.Columns[1].Width < System.Windows.Forms.TextRenderer.MeasureText(((Button)sender).Text, listHesap.Items[listHesap.Items.Count - 1].Font).Width
                    || listHesap.Columns[2].Width < System.Windows.Forms.TextRenderer.MeasureText(((double)kacPorsiyon * Convert.ToDouble(urunListesi[hangiMenuSecili].porsiyonFiyati[Convert.ToInt32(((Button)sender).Tag)])).ToString("0.00"), listHesap.Items[listHesap.Items.Count - 1].Font).Width
                    || listHesap.Columns[0].Width < System.Windows.Forms.TextRenderer.MeasureText(kacPorsiyon.ToString(), listHesap.Items[listHesap.Items.Count - 1].Font).Width)
                {
                    listHesap.Items[listHesap.Items.Count - 1].Font = new Font(listHesap.Items[listHesap.Items.Count - 1].Font.FontFamily, listHesap.Items[listHesap.Items.Count - 1].Font.Size - 0.5f, listHesap.Items[listHesap.Items.Count - 1].Font.Style);
                }

                labelToplamHesap.Text = (Convert.ToDouble(labelToplamHesap.Text) + Convert.ToDouble((listHesap.Items[listHesap.Items.Count - 1].SubItems[2].Text))).ToString("0.00");
            }
            else // varsa ürünün hesaptaki değerlerini istenilene göre arttır
            {
                listHesap.Groups[kacinciGrup].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text = (Convert.ToDouble(listHesap.Groups[kacinciGrup].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text) + kacPorsiyon).ToString();

                listHesap.Groups[kacinciGrup].Items[gruptaYeniGelenSiparisVarmi].SubItems[2].Text = (Convert.ToDouble(listHesap.Groups[kacinciGrup].Items[gruptaYeniGelenSiparisVarmi].SubItems[2].Text) + (double)kacPorsiyon * Convert.ToDouble(urunListesi[hangiMenuSecili].porsiyonFiyati[Convert.ToInt32(((Button)sender).Tag)])).ToString("0.00");

                while (listHesap.Columns[2].Width < System.Windows.Forms.TextRenderer.MeasureText(listHesap.Groups[kacinciGrup].Items[gruptaYeniGelenSiparisVarmi].SubItems[2].Text, listHesap.Groups[kacinciGrup].Items[gruptaYeniGelenSiparisVarmi].Font).Width
                    || listHesap.Columns[0].Width < System.Windows.Forms.TextRenderer.MeasureText(listHesap.Groups[kacinciGrup].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text, listHesap.Groups[kacinciGrup].Items[gruptaYeniGelenSiparisVarmi].Font).Width)
                {
                    listHesap.Groups[kacinciGrup].Items[gruptaYeniGelenSiparisVarmi].Font = new Font(listHesap.Groups[kacinciGrup].Items[gruptaYeniGelenSiparisVarmi].Font.FontFamily, listHesap.Groups[kacinciGrup].Items[gruptaYeniGelenSiparisVarmi].Font.Size - 0.5f, listHesap.Groups[kacinciGrup].Items[gruptaYeniGelenSiparisVarmi].Font.Style);
                }

                labelToplamHesap.Text = (Convert.ToDouble(labelToplamHesap.Text) + (double)kacPorsiyon * Convert.ToDouble(urunListesi[hangiMenuSecili].porsiyonFiyati[Convert.ToInt32(((Button)sender).Tag)])).ToString("0.00");
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
            else if (e.KeyChar == ',' && (sender as TextBox).Text.IndexOf(',') > -1)
            { // 1 kere , kullanmasına izin ver

                e.Handled = true;
            }
        }

        //liste ve panellerin scroll butonları
        #region Scoll Butonları
        private void buttonUrunListUp_Click(object sender, EventArgs e)
        {
            listHesap.Focus();
            if (scrollPosition - atlama <= 0)
            {
                this.listHesap.EnsureVisible(0);
            }
            else
            {
                this.listHesap.EnsureVisible(scrollPosition - atlama);
                scrollPosition -= atlama;
            }
        }

        private void buttonUrunListDown_Click(object sender, EventArgs e)
        {

            if (scrollPosition + atlama >= this.listHesap.Items.Count - 1)
                listHesap.EnsureVisible(this.listHesap.Items.Count - 1);
            else
            {
                listHesap.EnsureVisible(scrollPosition + atlama);
                scrollPosition += atlama;
            }
        }

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

                // BURADA PİN KONTROLÜ YAP EĞER İKRAM İZNİ VARSA İKRAM BUTONUNU AÇ 
                // BURADA PİN KONTROLÜ YAP EĞER İKRAM İZNİ VARSA İKRAM BUTONUNU AÇ 
                // BURADA PİN KONTROLÜ YAP EĞER İKRAM İZNİ VARSA İKRAM BUTONUNU AÇ 
                // BURADA PİN KONTROLÜ YAP EĞER İKRAM İZNİ VARSA İKRAM BUTONUNU AÇ                 
                buttonUrunIkram.Enabled = true;
                // BURADA PİN KONTROLÜ YAP EĞER İKRAM İZNİ VARSA İKRAM BUTONUNU AÇ 
                // BURADA PİN KONTROLÜ YAP EĞER İKRAM İZNİ VARSA İKRAM BUTONUNU AÇ 
                // BURADA PİN KONTROLÜ YAP EĞER İKRAM İZNİ VARSA İKRAM BUTONUNU AÇ 
                // BURADA PİN KONTROLÜ YAP EĞER İKRAM İZNİ VARSA İKRAM BUTONUNU AÇ 


                if (listHesap.SelectedItems[0].Group.Name == "ikramgrubu")
                    buttonUrunIkram.Text = "   İkram İptal";
                else
                    buttonUrunIkram.Text = "İkram";

                buttonTasi.Enabled = true;

                buttonAdd.Enabled = true;

                string siparisiOnceGirenKisi = "";
                if (listHesap.SelectedItems[0].Group.Name != "ikramgrubu")
                {
                    //eğer siparişi giren kişi ile iptal etmeye çalışan kişi aynı kişilerse iptal işlemine izin ver
                    int yeri = listHesap.SelectedItems[0].Group.ToString().IndexOf("-") - 1;
                    siparisiOnceGirenKisi = listHesap.SelectedItems[0].Group.ToString().Substring(0, yeri);
                }

                if (siparisiKimGirdi == siparisiOnceGirenKisi)
                    buttonUrunIptal.Enabled = true;
                else
                    buttonUrunIptal.Enabled = false;
            }
            else
            {
                buttonUrunIkram.Enabled = false;
                buttonTasi.Enabled = true;
                buttonUrunIptal.Enabled = false;
                buttonAdd.Enabled = false;
            }
        }

        private void textNumberOfItem_TextChanged(object sender, EventArgs e)
        {
            if (textNumberOfItem.Text == ",")
            {
                textNumberOfItem.Text = "0,";
                textNumberOfItem.SelectionStart = textNumberOfItem.Text.Length;
            }
        }




        private void changeTablesButton_Click(object sender, EventArgs e)
        {
            //burada bir split viewa departman ve masalarını koy seçilen departmana göre masa ve departman adını değiştir.
        }

        private void addNoteButton_Click(object sender, EventArgs e)
        {
            //burda başka bir user control göster not girilsin ve girilen not kaydedilsin exitte not sqle yazılsın
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            //Burda sqle not hesap vs yazılacak..
            this.Close();
        }

        private void paymentButton_Click(object sender, EventArgs e)
        {
            //ödendiğinde sql de ödendi flagini 1 yap 
        }

        #region Ürüne Basıldığında Enable Olan Butonlar
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


            // eğer zaten ikramsa ikramdan çıkar
            if (buttonUrunIkram.Text == "İkram")
            {
                // ürünün değerini 1 ürün kadar azalt, tüm hesaptan düş
                double dusulecekDeger = Convert.ToDouble(listHesap.SelectedItems[0].SubItems[2].Text) / Convert.ToDouble(listHesap.SelectedItems[0].SubItems[0].Text);
                listHesap.SelectedItems[0].SubItems[2].Text = (Convert.ToDouble(listHesap.SelectedItems[0].SubItems[2].Text) - carpan * dusulecekDeger).ToString("0.00");

                listHesap.SelectedItems[0].SubItems[0].Text = (Convert.ToDouble(listHesap.SelectedItems[0].SubItems[0].Text) - carpan).ToString();

                labelToplamHesap.Text = (Convert.ToDouble(labelToplamHesap.Text) - (dusulecekDeger * carpan)).ToString("0.00");


                bool ikramYok = true;

                for (int i = 0; i < listHesap.Groups[0].Items.Count; i++)
                {
                    if (listHesap.Groups[0].Items[i].SubItems[1].Text == listHesap.SelectedItems[0].SubItems[1].Text)
                    {

                        listHesap.Groups[0].Items[i].Text = (Convert.ToDouble(listHesap.Groups[0].Items[i].Text) + carpan).ToString();
                        listHesap.Groups[0].Items[i].SubItems[2].Text = (Convert.ToDouble(listHesap.Groups[0].Items[i].SubItems[2].Text) + (dusulecekDeger * carpan)).ToString("0.00");

                        ikramYok = false;
                    }
                }

                if (ikramYok)
                {

                    listHesap.Items.Insert(0, carpan.ToString());
                    listHesap.Items[0].SubItems.Add(listHesap.SelectedItems[0].SubItems[1].Text);
                    listHesap.Items[0].SubItems.Add((dusulecekDeger * carpan).ToString("0.00"));

                    listHesap.Items[0].Group = listHesap.Groups[0];
                    listHesap.Items[0].Font = new Font("Calibri", 18.75F, FontStyle.Bold);
                    listedeSeciliOlanItemlar.Insert(0, false);
                }

                if (listHesap.SelectedItems[0].Text == "0")
                {
                    listedeSeciliOlanItemlar.RemoveAt(listHesap.SelectedItems[0].Index);
                    listHesap.SelectedItems[0].Remove();
                }

                string ikramVerenler = listHesap.Groups[0].Header;

                if (ikramVerenler.IndexOf(siparisiKimGirdi) < 0)
                    listHesap.Groups[0].Header += " - " + siparisiKimGirdi;
            }
            else
            {
                // ürünün değerini bul ve hesaba ekle
                double dusulecekDeger = Convert.ToDouble(listHesap.SelectedItems[0].SubItems[2].Text) / Convert.ToDouble(listHesap.SelectedItems[0].SubItems[0].Text);
                listHesap.SelectedItems[0].SubItems[2].Text = (Convert.ToDouble(listHesap.SelectedItems[0].SubItems[2].Text) - dusulecekDeger * carpan).ToString("0.00");

                listHesap.SelectedItems[0].SubItems[0].Text = (Convert.ToDouble(listHesap.SelectedItems[0].SubItems[0].Text) - carpan).ToString();

                labelToplamHesap.Text = (Convert.ToDouble(labelToplamHesap.Text) + (dusulecekDeger * carpan)).ToString("0.00");

                bool urunYok = true;

                for (int i = listHesap.Groups[0].Items.Count; i < listHesap.Items.Count; i++)
                {
                    if (listHesap.Items[i].SubItems[1].Text == listHesap.SelectedItems[0].SubItems[1].Text)
                    {
                        listHesap.Items[i].Text = (Convert.ToDouble(listHesap.Items[i].Text) + carpan).ToString();
                        listHesap.Items[i].SubItems[2].Text = (Convert.ToDouble(listHesap.Items[i].SubItems[2].Text) + (dusulecekDeger * carpan)).ToString("0.00");
                        urunYok = false;
                    }
                }

                if (urunYok)
                {
                    listHesap.Items.Add(carpan.ToString());
                    listHesap.Items[listHesap.Items.Count - 1].SubItems.Add(listHesap.SelectedItems[0].SubItems[1].Text);
                    listHesap.Items[listHesap.Items.Count - 1].SubItems.Add((dusulecekDeger * carpan).ToString("0.00"));

                    listHesap.Items[listHesap.Items.Count - 1].Group = listHesap.Groups[listHesap.Groups.Count - 1];
                    listHesap.Items[listHesap.Items.Count - 1].Font = new Font("Calibri", 18.75F, FontStyle.Bold);
                    listedeSeciliOlanItemlar.Add(false);
                }

                if (listHesap.SelectedItems[0].Text == "0")
                {
                    listedeSeciliOlanItemlar.RemoveAt(listHesap.SelectedItems[0].Index);
                    listHesap.SelectedItems[0].Remove();
                }

                string ikramVerenler = listHesap.Groups[0].Header;

                if (listHesap.Groups[0].Items.Count == 0)
                    listHesap.Groups[0].Header = "İkram";
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

            textNumberOfItem.Text = "";

            if (labelToplamHesap.Text != "0,00") //hesapta para varsa butonu enable et
                buttonHesapOde.Enabled = true;
            else
                buttonHesapOde.Enabled = false; //yoksa disable et
        }

        private void buttonUrunIptal_Click(object sender, EventArgs e)
        {
            double carpan;
            bool direkSil = true;
            if (textNumberOfItem.Text != "")
            {
                carpan = Convert.ToDouble(textNumberOfItem.Text);
                if (carpan > Convert.ToDouble(listHesap.SelectedItems[0].SubItems[0].Text))
                    carpan = Convert.ToDouble(listHesap.SelectedItems[0].SubItems[0].Text);
                direkSil = false;
            }
            else
                carpan = Convert.ToDouble(listHesap.SelectedItems[0].SubItems[0].Text);

            if (carpan == 0)
                return;

            if (Convert.ToDouble(listHesap.SelectedItems[0].SubItems[0].Text) > 1 && direkSil)
            {
                DialogResult eminMisiniz;

                using (KontrolFormu dialog = new KontrolFormu("Seçili siparişin tamamını silmek istediğinize emin misiniz?", true))
                {
                    eminMisiniz = dialog.ShowDialog();
                }

                if (eminMisiniz == DialogResult.No)
                {
                    return;
                }
            }            

            double dusulecekDeger = Convert.ToDouble(listHesap.SelectedItems[0].SubItems[2].Text) / Convert.ToDouble(listHesap.SelectedItems[0].SubItems[0].Text);

            listHesap.SelectedItems[0].SubItems[0].Text = (Convert.ToDouble(listHesap.SelectedItems[0].SubItems[0].Text) - carpan).ToString();

            listHesap.SelectedItems[0].SubItems[2].Text = (Convert.ToDouble(listHesap.SelectedItems[0].SubItems[2].Text) - dusulecekDeger * carpan).ToString("0.00");

            labelToplamHesap.Text = (Convert.ToDouble(labelToplamHesap.Text) - (dusulecekDeger * carpan)).ToString("0.00");

            int silinecekGrup = Convert.ToInt32(listHesap.SelectedItems[0].Group.Tag);

            if(listHesap.SelectedItems[0].Text == "0")
            {
                listedeSeciliOlanItemlar.RemoveAt(listHesap.SelectedItems[0].Index);
                listHesap.SelectedItems[0].Remove();
            }

            if (listHesap.Groups[silinecekGrup].Items.Count < 1)
            {
                listHesap.Groups.RemoveAt(silinecekGrup);
                grupOlustur = true;
            }

            if (labelToplamHesap.Text == "0,00")
                buttonHesapOde.Enabled = false;

            if (this.listHesap.Items.Count > 0)
            {
                int itemsCount = this.listHesap.Items.Count + this.listHesap.Groups.Count;
                int itemHeight = this.listHesap.Items[0].Bounds.Height;
                int VisiableItem = (int)this.listHesap.ClientRectangle.Height / itemHeight;
                if (itemsCount < VisiableItem)
                {
                    listHesap.Columns[1].Width = urunBoyu + 10;
                    listHesap.Columns[2].Width = fiyatBoyu + 10;
                    buttonUrunListUp.Visible = false;
                    buttonUrunListDown.Visible = false;
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
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            double carpan;

            if (textNumberOfItem.Text != "")
            {
                carpan = Convert.ToDouble(textNumberOfItem.Text);
            }
            else
                carpan = 1;

            if (carpan == 0)
                return;

            double dusulecekDeger = Convert.ToDouble(listHesap.SelectedItems[0].SubItems[2].Text) / Convert.ToDouble(listHesap.SelectedItems[0].SubItems[0].Text);

            listHesap.SelectedItems[0].SubItems[0].Text = (Convert.ToDouble(listHesap.SelectedItems[0].SubItems[0].Text) + carpan).ToString();

            listHesap.SelectedItems[0].SubItems[2].Text = (Convert.ToDouble(listHesap.SelectedItems[0].SubItems[2].Text) + dusulecekDeger * carpan).ToString("0.00");

            labelToplamHesap.Text = (Convert.ToDouble(labelToplamHesap.Text) + (dusulecekDeger * carpan)).ToString("0.00");

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
        }

        private void buttonTasi_Click(object sender, EventArgs e)
        {
            //burada bir split viewa departman ve masalarını koy seçilen departmanın masasının hesabına seçilen siparişleri ekle(hesap yoksa yeni oluştur varsa ekle), burdakinden çıkar

        }
        #endregion
    }
}