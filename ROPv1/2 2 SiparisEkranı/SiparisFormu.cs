using System;
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
using System.Xml.Serialization;

namespace ROPv1
{
    public partial class SiparisFormu : Form
    {
        bool closeOrShowAnotherForm = false;

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
                for (int i = 0; i < departmanButtons.Length; i++)
                {
                    departmanButtons[i] = new Button();
                    departmanButtons[i].Text = restoranListesi[i].departmanAdi;
                    departmanButtons[i].BackColor = Color.White;
                    departmanButtons[i].ForeColor = SystemColors.ActiveCaption;
                    departmanButtons[i].Font = new Font("Arial", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                    departmanButtons[i].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    departmanButtons[i].UseVisualStyleBackColor = false;
                    departmanButtons[i].Tag = i;
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
                            tablePanel.Tag = 0;
                        }
                    }
                }

                for (int j = 6; j > 0; j--)
                {
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

        private void changeTableView(object sender, EventArgs e)
        {
            tablePanel.RowCount = 6;
            tablePanel.ColumnCount = 7;

            if ((int)tablePanel.Tag != (int)((Button)sender).Tag) //burayı düzelt eğer seçili masa planı zaten ekrandaysa yenisi koyulmasın, ekranda değilse eskiler silinip yenisi eklensin
            {
                tablePanel.Controls.Clear();
                for (int i = 0; i < 6; i++)
                {
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
                            tablePanel.Controls.Add(buttonTable, j, i);
                            buttonTable.Name = "" + i + j;
                            tablePanel.Tag = (int)((Button)sender).Tag;
                        }
                    }
                }

                for (int j = 6; j > 0; j--)
                {
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
        }

        private void timerSaat_Tick(object sender, EventArgs e)
        {
            labelSaat.Text = DateTime.Now.ToString("HH:mm:ss", new CultureInfo("tr-TR"));
        }
    }
}
