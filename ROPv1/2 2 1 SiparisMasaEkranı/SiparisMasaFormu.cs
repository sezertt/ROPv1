﻿using System;
using System.Collections.Generic;
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

namespace ROPv1
{
    public partial class SiparisMasaFormu : Form
    {

        int hangiButtonSecili = 0;

        List<Restoran> restoranListesi = new List<Restoran>();

        List<MasaDizayn> masaDizaynListesi = new List<MasaDizayn>();

        DateTime hangiGun;

        decimal toplamHesap = 0, kalanHesap = 0;

        public SiparisMasaFormu()
        {
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
                    
                    // Masa kullanımda mı bakıyoruz
                    SqlCommand cmd = SQLBaglantisi.getCommand("SELECT MasaId FROM IslemdekiMasalar WHERE DepartmanAdlari='" + restoranListesi[hangiButtonSecili].departmanAdi + "' AND MasaAdlari='" + ((Button)sender).Text + "'");
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        try
                        {
                            dr.GetInt32(0);

                            using (KontrolFormu dialog = new KontrolFormu("DİKKAT!\nSeçtiğiniz masa şu anda kullanımda. Eğer masanın kullanımda olmadığından eminseniz Masayı AÇ tuşuna basarak masaya girişi açabilirsiniz.", true, 1))
                            {
                                DialogResult masaAcilsinMi = dialog.ShowDialog();

                                if (masaAcilsinMi != DialogResult.Yes)
                                {
                                    using (KontrolFormu dialog2 = new KontrolFormu("DİKKAT!\nAçmak istediğiniz masa kullanımda ise bu masanın açılması hatalara neden olabilir. Yinede masayı açmak istiyor musunuz? ", true))
                                    {
                                        DialogResult masaAcilsinMi2 = dialog2.ShowDialog();

                                        if (masaAcilsinMi2 == DialogResult.Yes)
                                        {
                                            cmd = SQLBaglantisi.getCommand("DELETE FROM IslemdekiMasalar WHERE MasaAdlari='" + (sender as Button).Text + "' AND DepartmanAdlari='" + restoranListesi[hangiButtonSecili].departmanAdi + "'");
                                            cmd.ExecuteNonQuery();
                                            cmd.Connection.Close();
                                            cmd.Connection.Dispose();                                            
                                        }
                                        else
                                        {
                                            cmd.Connection.Close();
                                            cmd.Connection.Dispose();
                                            return;
                                        }
                                    }
                                }
                                else
                                {
                                    cmd.Connection.Close();
                                    cmd.Connection.Dispose();
                                    return;
                                }
                            }
                        }
                        catch
                        { }
                    }
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();

                    PinKoduFormu pinForm = new PinKoduFormu("Masa Görüntüleme");
                    pinForm.ShowDialog();

                    if (pinForm.dogru) //pin doğru
                    {
                        cmd = SQLBaglantisi.getCommand("INSERT INTO IslemdekiMasalar(MasaAdlari,DepartmanAdlari) values(@_MasaAdlari,@_DepartmanAdlari)");
                        cmd.Parameters.AddWithValue("@_MasaAdlari", ((Button)sender).Text);
                        cmd.Parameters.AddWithValue("@_DepartmanAdlari", restoranListesi[hangiButtonSecili].departmanAdi);
                        cmd.ExecuteNonQuery();

                        SiparisMenuFormu siparisForm;
                        if (((Button)sender).BackColor == Color.White) // masa kapalı
                        {
                            siparisForm = new SiparisMenuFormu(((Button)sender).Text, restoranListesi[hangiButtonSecili], pinForm.ayarYapanKisi, false, 0, 0);//burada masa numarasını da yolla
                            siparisForm.ShowDialog();
                        }
                        else // masa acik
                        {
                            cmd = SQLBaglantisi.getCommand("SELECT ToplamHesap,KalanHesap FROM Adisyon WHERE MasaAdi='" + ((Button)sender).Text + "' AND DepartmanAdi='" + restoranListesi[hangiButtonSecili].departmanAdi + "' AND AcikMi=1");
                            dr = cmd.ExecuteReader();
                            dr.Read();
                            toplamHesap = dr.GetDecimal(0);
                            kalanHesap = dr.GetDecimal(1);

                            cmd.Connection.Close();
                            cmd.Connection.Dispose();

                            siparisForm = new SiparisMenuFormu(((Button)sender).Text, restoranListesi[hangiButtonSecili], pinForm.ayarYapanKisi, true, toplamHesap, kalanHesap);//burada masa numarasını da yolla
                            siparisForm.ShowDialog();
                        }

                        if(siparisForm.masaAcikMi2 != "")
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
                    Button tablebutton = tablePanel.Controls.Find(dr.GetString(0), false)[0] as Button;
                    tablebutton.BackColor = Color.Firebrick;
                    tablebutton.ForeColor = Color.White;
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
            labelSaat.Text = DateTime.Now.ToString("HH:mm:ss", new CultureInfo("tr-TR"));
            labelGun.Text = DateTime.Now.ToString("dddd", new CultureInfo("tr-TR"));
            labelTarih.Text = DateTime.Now.Date.ToString("d MMMM yyyy", new CultureInfo("tr-TR"));
            timerSaat.Start();

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
                    Button tablebutton = tablePanel.Controls.Find(dr.GetString(0), false)[0] as Button;
                    tablebutton.BackColor = Color.Firebrick;
                    tablebutton.ForeColor = Color.White;
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
    }
}
