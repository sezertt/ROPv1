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

namespace ROPv1
{
    public partial class SiparisFormu : Form
    {
        bool closeOrShowAnotherForm = false;

        int hangiButtonSecili = 0;

        List<Restoran> restoranListesi = new List<Restoran>();

        List<MasaDizayn> masaDizaynListesi = new List<MasaDizayn>();

        public SiparisFormu()
        {
            InitializeComponent();

            labelSaat.Text = DateTime.Now.ToString("HH:mm:ss", new CultureInfo("tr-TR"));
            labelGun.Text = DateTime.Now.ToString("dddd", new CultureInfo("tr-TR"));
            labelTarih.Text = DateTime.Now.Date.ToString("d MMMM yyyy", new CultureInfo("tr-TR"));
            timerSaat.Start();

            //gün başı yapılmış mı bak yapılmışsa daybutton resmini set et            
            if (Properties.Settings.Default.gunAcikMi)
            {
                dayButton.Image = global::ROPv1.Properties.Resources.dayOn;
            }
            else
            {
                dayButton.Image = global::ROPv1.Properties.Resources.dayOff;
            }

            if (File.Exists("restoran.xml"))
            {
                Restoran[] info = new Restoran[1];
                XmlLoad<Restoran> loadInfo = new XmlLoad<Restoran>();
                info = loadInfo.LoadRestoran("restoran.xml");

                restoranListesi.AddRange(info);

                Button[] departmanButtons = new Button[restoranListesi.Count];

                int a = 0;

                for (int i = 0; i < departmanButtons.Length; i++)
                {
                    departmanButtons[i] = new Button();
                    departmanButtons[i].Text = restoranListesi[i].departmanAdi;
                    if (i == 0)
                    {
                        departmanButtons[i].BackColor = SystemColors.ActiveCaption;
                        departmanButtons[i].ForeColor = Color.White;
                    }
                    else
                    {
                        departmanButtons[i].BackColor = Color.White;
                        departmanButtons[i].ForeColor = SystemColors.ActiveCaption;
                    }
                    departmanButtons[i].Font = new Font("Arial", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                    departmanButtons[i].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    departmanButtons[i].UseVisualStyleBackColor = false;
                    departmanButtons[i].Name = "" + i;

                    if (restoranListesi[i].departmanEkrani == "")
                        departmanButtons[i].Tag = 200;
                    else
                    {
                        departmanButtons[i].Tag = a;
                        a++;
                    }

                    departmanButtons[i].Height = panel1.Height;
                    departmanButtons[i].Dock = DockStyle.Right;
                    departmanButtons[i].Click += changeTableView;
                    panel1.Controls.Add(departmanButtons[i]);
                }

                MasaDizayn[] infoMasa = new MasaDizayn[1];

                XmlLoad<MasaDizayn> loadInfoMasa = new XmlLoad<MasaDizayn>();
                infoMasa = loadInfoMasa.LoadRestoran("masaDizayn.xml");

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
                            buttonTable.BackColor = Color.White;
                            buttonTable.ForeColor = SystemColors.ActiveCaption;
                            buttonTable.Font = new Font("Arial", 21.75F, FontStyle.Bold);
                            buttonTable.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                            tablePanel.Controls.Add(buttonTable, j, i);
                            buttonTable.Name = "" + i + j;
                            buttonTable.Click += siparisButonuBasildi;
                            tablePanel.Tag = 0;
                        }
                    }
                }

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

        private void myPannel_SizeChanged(object sender, EventArgs e)
        {
            panel1.SuspendLayout();
            foreach (Control ctrl in panel1.Controls)
            {
                if (ctrl is Button)
                {
                    ctrl.Width = panel1.ClientSize.Width / panel1.Controls.Count;

                    while (ctrl.Width - 12 < System.Windows.Forms.TextRenderer.MeasureText(ctrl.Text, new Font(ctrl.Font.FontFamily, ctrl.Font.Size, ctrl.Font.Style)).Width)
                    {
                        ctrl.Font = new Font(ctrl.Font.FontFamily, ctrl.Font.Size - 0.5f, ctrl.Font.Style);
                    }
                }

            }
            panel1.ResumeLayout();
        }
        
        private void siparisButonuBasildi(object sender, EventArgs e)
        {
            //gün başı yapılmış mı bak yapılmışsa daybutton resmini set et            
            if (Properties.Settings.Default.gunAcikMi)
            { //gün açık sipariş ekranına geçilebilir
                
            }
            else
            { //gün başı yapılmalı
                using (KontrolFormu dialog = new KontrolFormu("Gün Başı yapmanız gerekiyor", false))
                {
                    dialog.ShowDialog();
                    this.buttonGunIslemiPressed(null,null);
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
            else if ((int)tablePanel.Tag != (int)((Button)sender).Tag) //burayı düzelt eğer seçili masa planı zaten ekrandaysa yenisi koyulmasın, ekranda değilse eskiler silinip yenisi eklensin
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
                            buttonTable.BackColor = Color.White;
                            buttonTable.ForeColor = SystemColors.ActiveCaption;
                            buttonTable.Font = new Font("Arial", 21.75F, FontStyle.Bold);
                            buttonTable.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                            buttonTable.Click += siparisButonuBasildi;
                            buttonTable.Name = "" + i + j;
                            tablePanel.Controls.Add(buttonTable, j, i);                            
                            tablePanel.Tag = (int)((Button)sender).Tag;
                        }
                    }
                }

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

        private void CloseApp(object sender, FormClosedEventArgs e)
        {
            if (!closeOrShowAnotherForm) // eğer başka bir forma gidilmeyecekse uygulamayı kapat
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

        private void buttonGunIslemiPressed(object sender, EventArgs e)
        {
            PinKoduFormu pinForm = new PinKoduFormu();
            pinForm.ShowDialog();

            if(pinForm.dogru)
            {
                GunFormu gunForm = new GunFormu(pinForm.ayarYapanKisi);
                gunForm.ShowDialog();                

                //gün başı yapılmış mı bak
                if (Properties.Settings.Default.gunAcikMi)
                {
                    dayButton.Image = global::ROPv1.Properties.Resources.dayOn;
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
    }
}
