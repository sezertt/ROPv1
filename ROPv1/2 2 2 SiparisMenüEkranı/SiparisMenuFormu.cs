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

        int hangiMenuSecili = 0;

        int scrollPosition = 0, elemanBoyu, gorunenListeElemaniSayisi, atlama;

        string siparisiKimGirdi;

        List<Menuler> menuListesi = new List<Menuler>();  // menüleri tutacak liste

        List<UrunOzellikleri> urunListesi = new List<UrunOzellikleri>();

        List<bool> listedeSeciliOlanItemlar = new List<bool>();

        public SiparisMenuFormu(string masaninAdi, Restoran butonBilgileri, string siparisiGirenKisi)
        {
            InitializeComponent();

            siparisiKimGirdi = siparisiGirenKisi;

            hangiDepartman = butonBilgileri;

            labelMasa.Text = "Masa: " + masaninAdi;
            labelDepartman.Text = "Departman: " + hangiDepartman.departmanAdi;

            while (labelDepartman.Width < System.Windows.Forms.TextRenderer.MeasureText(labelDepartman.Text, new Font(labelDepartman.Font.FontFamily, labelDepartman.Font.Size, labelDepartman.Font.Style)).Width)
            {
                labelDepartman.Font = new Font(labelDepartman.Font.FontFamily, labelDepartman.Font.Size - 0.5f, labelDepartman.Font.Style);
            }

            while (labelMasa.Width < System.Windows.Forms.TextRenderer.MeasureText(labelMasa.Text, new Font(labelMasa.Font.FontFamily, labelMasa.Font.Size, labelMasa.Font.Style)).Width)
            {
                labelMasa.Font = new Font(labelMasa.Font.FontFamily, labelMasa.Font.Size - 0.5f, labelMasa.Font.Style);
            }

            Menuler[] infoMenu = new Menuler[1];

            // Oluşturulmuş menüleri xml den okuyoruz
            XmlLoad<Menuler> loadInfo = new XmlLoad<Menuler>();
            infoMenu = loadInfo.LoadRestoran("menu.xml");

            //menüleri tutacak listemize atıyoruz
            menuListesi.AddRange(infoMenu);

            int hangiMenu = 0;
            while (menuListesi[hangiMenu].menuAdi != hangiDepartman.departmanMenusu)
                hangiMenu++;

            for (int i = 0; i < menuListesi[hangiMenu].menukategorileri.Count; i++)
            {
                Button menuBasliklariButonlari = new Button();
                menuBasliklariButonlari.Text = menuListesi[hangiMenu].menukategorileri[i];

                menuBasliklariButonlari.UseVisualStyleBackColor = false;
                menuBasliklariButonlari.BackColor = Color.FromArgb(163, 190, 219);
                menuBasliklariButonlari.ForeColor = Color.White;
                menuBasliklariButonlari.TextAlign = ContentAlignment.MiddleCenter;
                menuBasliklariButonlari.Font = new Font("Arial", 18.00F, FontStyle.Bold);
                menuBasliklariButonlari.Tag = -1;
                menuBasliklariButonlari.Click += menuBasliklariButonlari_Click;

                flowPanelMenuBasliklari.Controls.Add(menuBasliklariButonlari);
            }

            UrunOzellikleri[] infoUrun = new UrunOzellikleri[1];

            XmlLoad<UrunOzellikleri> loadInfoUrun = new XmlLoad<UrunOzellikleri>();
            infoUrun = loadInfoUrun.LoadRestoran("urunler.xml");

            urunListesi.AddRange(infoUrun);

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
                        urunButonlari.Font = new Font("Arial", 18.00F, FontStyle.Bold);
                        urunButonlari.Tag = j;
                        urunButonlari.Click += urunButonlari_Click;

                        flowPanelUrunler.Controls.Add(urunButonlari);
                    }
                    break;
                }
            }
        }

        //Menü seçildiğinde menü içindeki itemları getiren method
        private void menuBasliklariButonlari_Click(object sender, EventArgs e)
        {
            flowPanelUrunler.Controls.Clear();
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
                            urunButonlari.Font = new Font("Arial", 18.00F, FontStyle.Bold);
                            urunButonlari.Height = (flowPanelUrunler.Bounds.Height - 30) / 4;
                            urunButonlari.Width = (flowPanelUrunler.Bounds.Width - 26) / 3;
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
                    urunButonlari.Font = new Font("Arial", 18.00F, FontStyle.Bold);
                    urunButonlari.Height = (flowPanelUrunler.Bounds.Height - 30) / 4;
                    urunButonlari.Width = (flowPanelUrunler.Bounds.Width - 26) / 3;
                    urunButonlari.Tag = j;
                    urunButonlari.Click += urunButonlari_Click;

                    flowPanelUrunler.Controls.Add(urunButonlari);
                }
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
            foreach (Control ctrl in flowPanelMenuBasliklari.Controls)
            {
                if (ctrl is Button)
                {
                    ctrl.Height = (flowPanelMenuBasliklari.Bounds.Height - 66) / 11;
                    ctrl.Width = (flowPanelMenuBasliklari.Bounds.Width - 24);

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
            foreach (Control ctrl in flowPanelUrunler.Controls)
            {
                if (ctrl is Button)
                {
                    ctrl.Height = (flowPanelUrunler.Bounds.Height - 26) / 4;
                    ctrl.Width = (flowPanelUrunler.Bounds.Width - 36) / 3;
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
                    listHesap.Columns[1].Width = 220;
                    listHesap.Columns[2].Width = 90;
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
            double kacPorsiyon;

            if (textNumberOfItem.Text != "")
                kacPorsiyon = Convert.ToDouble(textNumberOfItem.Text);
            else
                kacPorsiyon = 1;

            if (kacPorsiyon == 0)
                return;

            if (grupOlustur)
            {
                grupOlustur = false;
                listHesap.Groups.Add("siparis", siparisiKimGirdi + " - " + DateTime.Now.ToShortTimeString());
                listHesap.Groups[listHesap.Groups.Count - 1].Tag = listHesap.Groups.Count - 1;
            }

            int kacinciGrup = listHesap.Groups.Count - 1;

            int gruptaYeniGelenSiparisVarmi = -1;
            for (int i = 0; i < listHesap.Groups[kacinciGrup].Items.Count; i++)
            {
                if (((Button)sender).Text == listHesap.Groups[kacinciGrup].Items[i].SubItems[1].Text)
                {
                    gruptaYeniGelenSiparisVarmi = i;
                    break;
                }
            }

            if (gruptaYeniGelenSiparisVarmi == -1)
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
                    //listHesap.Columns[2].TextAlign = HorizontalAlignment.Left;
                    listHesap.Columns[1].Width = 220;
                    listHesap.Columns[2].Width = 90;
                    buttonUrunListUp.Visible = true;
                    buttonUrunListDown.Visible = true;
                }

                elemanBoyu = this.listHesap.Items[0].Bounds.Height;
                gorunenListeElemaniSayisi = (int)this.listHesap.ClientRectangle.Height / elemanBoyu;
                atlama = gorunenListeElemaniSayisi;

                labelToplamHesap.Text = (Convert.ToDouble(labelToplamHesap.Text) + Convert.ToDouble((listHesap.Items[listHesap.Items.Count - 1].SubItems[2].Text))).ToString("0.00");
            }
            else
            {
                listHesap.Groups[kacinciGrup].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text = (Convert.ToDouble(listHesap.Groups[kacinciGrup].Items[gruptaYeniGelenSiparisVarmi].SubItems[0].Text) + kacPorsiyon).ToString();

                listHesap.Groups[kacinciGrup].Items[listHesap.Items.Count - 1].SubItems[2].Text = (Convert.ToDouble(listHesap.Groups[kacinciGrup].Items[gruptaYeniGelenSiparisVarmi].SubItems[2].Text) + (double)kacPorsiyon * Convert.ToDouble(urunListesi[hangiMenuSecili].porsiyonFiyati[Convert.ToInt32(((Button)sender).Tag)])).ToString("0.00");


                labelToplamHesap.Text = (Convert.ToDouble(labelToplamHesap.Text) + (double)kacPorsiyon * Convert.ToDouble(urunListesi[hangiMenuSecili].porsiyonFiyati[Convert.ToInt32(((Button)sender).Tag)])).ToString("0.00");
            }
            textNumberOfItem.Text = "";

            if (labelToplamHesap.Text != "0,00")
                buttonHesapOde.Enabled = true;
        }

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
            { // only allow one decimal point

                e.Handled = true;
            }
        }

        //liste ve panellerin scroll butonları
        #region Scoll Butonları
        private void buttonUrunListUp_Click(object sender, EventArgs e)
        {
            if (scrollPosition - atlama <= 0)
                this.listHesap.EnsureVisible(0);
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
            int hangiEleman;

            if (info.Item == null || info.SubItem == null)
            {
                kacElemanSecili = 0;
                hangiEleman = -1;

                for (int i = 0; i < listedeSeciliOlanItemlar.Count; i++)
                {
                    if (listedeSeciliOlanItemlar[i])
                    {
                        listHesap.Items[i].Selected = true;
                        hangiEleman = i;
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
                    buttonUrunuArttir.Enabled = false;
                    buttonUrunuAzalt.Enabled = false;
                }
                else if (kacElemanSecili == 1)
                {
                    buttonUrunIkram.Enabled = true;
                    buttonTasi.Enabled = true;

                    if ((int)listHesap.Items[hangiEleman].Group.Tag == listHesap.Groups.Count - 1)
                    {
                        buttonUrunuArttir.Enabled = true;
                        buttonUrunuAzalt.Enabled = true;
                    }
                    else
                    {
                        buttonUrunuArttir.Enabled = false;
                        buttonUrunuAzalt.Enabled = false;
                    }

                    //eğer siparişi giren kişi ile iptal etmeye çalışan kişi aynı kişilerse iptal işlemine izin ver
                    int yeri = listHesap.Items[hangiEleman].Group.ToString().IndexOf("-") - 1;
                    string siparisiOnceGirenKisi = listHesap.Items[hangiEleman].Group.ToString().Substring(0, yeri);

                    if (siparisiKimGirdi == siparisiOnceGirenKisi)
                        buttonUrunIptal.Enabled = true;
                    else
                        buttonUrunIptal.Enabled = false;
                }
                else
                {
                    buttonUrunIkram.Enabled = true;
                    buttonTasi.Enabled = true;
                    buttonUrunuArttir.Enabled = false;
                    buttonUrunuAzalt.Enabled = false;
                    buttonUrunIptal.Enabled = false;
                }
                return;
            }

            if (listHesap.Items[info.Item.Index].Selected && listedeSeciliOlanItemlar[info.Item.Index] == false)
                listedeSeciliOlanItemlar[info.Item.Index] = true;
            else
                listedeSeciliOlanItemlar[info.Item.Index] = false;

            kacElemanSecili = 0;
            hangiEleman= -1;

            for (int i = 0; i < listedeSeciliOlanItemlar.Count; i++)
            {
                if (listedeSeciliOlanItemlar[i])
                {
                    listHesap.Items[i].Selected = true;
                    hangiEleman = i;
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
                buttonUrunuArttir.Enabled = false;
                buttonUrunuAzalt.Enabled = false;
            }
            else if (kacElemanSecili == 1)
            {
                buttonUrunIkram.Enabled = true;
                buttonTasi.Enabled = true;

                if ((int)listHesap.Items[hangiEleman].Group.Tag == listHesap.Groups.Count - 1)
                {
                    buttonUrunuArttir.Enabled = true;
                    buttonUrunuAzalt.Enabled = true;
                }
                else
                {
                    buttonUrunuArttir.Enabled = false;
                    buttonUrunuAzalt.Enabled = false;
                }

                //eğer siparişi giren kişi ile iptal etmeye çalışan kişi aynı kişilerse iptal işlemine izin ver
                int yeri = listHesap.Items[hangiEleman].Group.ToString().IndexOf("-")-1;
                string siparisiOnceGirenKisi = listHesap.Items[hangiEleman].Group.ToString().Substring(0, yeri);

                if (siparisiKimGirdi == siparisiOnceGirenKisi)
                    buttonUrunIptal.Enabled = true;
                else
                    buttonUrunIptal.Enabled = false;
            }
            else
            {
                buttonUrunIkram.Enabled = true;
                buttonTasi.Enabled = true;
                buttonUrunuArttir.Enabled = false;
                buttonUrunuAzalt.Enabled = false;
                buttonUrunIptal.Enabled = false;
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
            // eğer zaten ikramsa ikramdan çıkar
            if (buttonUrunIkram.Text == "İkram")
            {
                // ürünün değerini 0 yap, tüm hesaptan düş
                buttonUrunIkram.Text = "   İkram İptal";
            }
            else
            {
                // ürünün değerini bul ve hesaba ekle
                buttonUrunIkram.Text = "İkram";
            }
        }

        private void buttonUrunIptal_Click(object sender, EventArgs e)
        {

            if (labelToplamHesap.Text == "0,00")
                buttonHesapOde.Enabled = false;
        }

        private void buttonTasi_Click(object sender, EventArgs e)
        {
            //burada bir split viewa departman ve masalarını koy seçilen departmanın masasının hesabına seçilen siparişleri ekle(hesap yoksa yeni oluştur varsa ekle), burdakinden çıkar

        }

        private void buttonUrunuArttir_Click(object sender, EventArgs e)
        {
            // seçili ürünün miktarını 1 arttır fiyat vs arttır
        }

        private void buttonUrunuAzalt_Click(object sender, EventArgs e)
        {
            // seçili ürünün miktarını 1 azalt fiyat vs azalt 
        }
        #endregion
    }
}