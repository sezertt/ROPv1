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
using System.Data.SqlClient;

namespace ROPv1
{
    public partial class MasaDegistirFormu : Form
    {
        int hangiMasaDizayni = 200, hangiDepartmanButonu = 0;

        List<Restoran> restoranListesi = new List<Restoran>();

        List<MasaDizayn> masaDizaynListesi = new List<MasaDizayn>();

        public string yeniMasa;

        public string yeniDepartman;

        public int yapilmasiGerekenIslem;

        string eskiMasa, eskiDepartman;

        // Açık masaların listesi        
        private List<string> masalar;

        SiparisMenuFormu gelenSiparisFormu;


        public MasaDegistirFormu(string masaAdi, string DepartmanAdi, SiparisMenuFormu gelenSiparisFormu)
        {
            masalar = new List<string>();
            InitializeComponent();

            this.gelenSiparisFormu = gelenSiparisFormu;
            eskiMasa = masaAdi;
            eskiDepartman = DepartmanAdi;
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
            yeniMasa = ((Button)sender).Text;

            if (yeniDepartman == "" || yeniDepartman == null) // departman değişmemiş demektir
                yeniDepartman = eskiDepartman;

            if (((Button)sender).BackColor == Color.Firebrick && yeniDepartman == eskiDepartman) // departman değişmedi ve masaların ikisi de açık
            {
                yapilmasiGerekenIslem = 0;
            }
            else if (((Button)sender).BackColor == Color.Firebrick) // masalar açık departman değişti
            {
                yapilmasiGerekenIslem = 1;
            }
            else if (yeniDepartman == eskiDepartman) // departman değişmedi 1 masa açık
            {
                yapilmasiGerekenIslem = 2;
            }
            else // departmanda değişti 1 masa açık 
            {
                yapilmasiGerekenIslem = 3;
            }
            this.Close();
        }


        private void SiparisMasaFormu_Load(object sender, EventArgs e)
        {
            if (File.Exists("restoran.xml"))
            {
                XmlLoad<Restoran> loadInfo = new XmlLoad<Restoran>();
                Restoran[] info = loadInfo.LoadRestoran("restoran.xml");

                restoranListesi.AddRange(info);

                XmlLoad<MasaDizayn> loadInfoMasa = new XmlLoad<MasaDizayn>();
                MasaDizayn[] infoMasa = loadInfoMasa.LoadRestoran("masaDizayn.xml");

                //kendi listemize atıyoruz
                masaDizaynListesi.AddRange(infoMasa);

                bool departmanBulunduMu = false;

                for (int i = 0; i < restoranListesi.Count; i++)
                {
                    Button departmanButton = new Button();
                    departmanButton.Text = restoranListesi[i].departmanAdi;

                    departmanButton.BackColor = Color.White;
                    departmanButton.ForeColor = SystemColors.ActiveCaption;

                    if (!departmanBulunduMu)
                    {
                        if (departmanButton.Text != eskiDepartman)
                        {
                            hangiDepartmanButonu++;
                        }
                        else
                        {
                            departmanBulunduMu = true;
                        }
                    } 

                    departmanButton.Font = new Font("Arial", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                    departmanButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    departmanButton.UseVisualStyleBackColor = false;
                    departmanButton.Name = "" + i;

                    departmanButton.Tag = 200;

                    for (int j = 0; j < masaDizaynListesi.Count; j++)
                    {
                        if (restoranListesi[i].departmanEkrani == masaDizaynListesi[j].masaPlanIsmi)
                        {
                            if (eskiDepartman == restoranListesi[i].departmanAdi)
                            {
                                hangiMasaDizayni = j;
                            }
                            departmanButton.Tag = j;
                            break;
                        }
                    }

                    departmanButton.Height = panel1.Height;
                    departmanButton.Width = panel1.Width / restoranListesi.Count;
                    departmanButton.Dock = DockStyle.Right;
                    departmanButton.Click += changeTableView;
                    panel1.Controls.Add(departmanButton);
                }
                tablePanel.Tag = -1;

                //Masa butonlarını eklemek için serversa direk ilk departman butonuna kod ile basıyoruz, clientsa servera sorarak açık masa bilgileriyle birlikte alıyoruz
                if (Properties.Settings.Default.Server == 2)
                {
                    Button birinciDepartman = panel1.Controls["" + hangiDepartmanButonu] as Button;
                    birinciDepartman.PerformClick();
                }
                else
                {
                    gelenSiparisFormu.masaDegisikligiFormundanAcikMasaBilgisiIstegiGeldiMasaFormunaIlet("komut=departman&departmanAdi=" + restoranListesi[hangiDepartmanButonu].departmanAdi);
                }
            }
        }

        private void changeTableView(object sender, EventArgs e)
        {
            if (hangiDepartmanButonu != Convert.ToInt32(((Button)sender).Name))
            {
                panel1.Controls[hangiDepartmanButonu].BackColor = Color.White;
                panel1.Controls[hangiDepartmanButonu].ForeColor = SystemColors.ActiveCaption;
            }

            panel1.Controls[Convert.ToInt32(((Button)sender).Name)].BackColor = SystemColors.ActiveCaption;
            panel1.Controls[Convert.ToInt32(((Button)sender).Name)].ForeColor = Color.White;

            hangiDepartmanButonu = Convert.ToInt32(((Button)sender).Name);
            hangiMasaDizayni = Convert.ToInt32(((Button)sender).Tag);

            if (Properties.Settings.Default.Server == 2)
            {
                if (hangiMasaDizayni > masaDizaynListesi.Count - 1)
                {
                    tablePanel.Controls.Clear();
                    tablePanel.Tag = -1;
                    return;
                }
                else if ((int)tablePanel.Tag != hangiMasaDizayni) //eğer seçili masa planı zaten ekrandaysa yenisi koyulmasın, ekranda değilse eskiler silinip yenisi eklensin
                {
                    tablePanel.RowCount = 6;
                    tablePanel.ColumnCount = 7;
                    tablePanel.Controls.Clear();
                    masalar.Clear(); // acik masalari tutan listeyi sıfırlıyoruz, önceki açık masalardan kurtuluyoruz

                    SqlCommand cmd = SQLBaglantisi.getCommand("SELECT MasaAdi FROM Adisyon WHERE DepartmanAdi='" + restoranListesi[hangiDepartmanButonu].departmanAdi + "' AND AcikMi=1");
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        try
                        {
                            masalar.Add(dr.GetString(0));
                        }
                        catch { }
                    }

                    cmd.Connection.Close();
                    cmd.Connection.Dispose();

                    for (int i = 0; i < 6; i++)
                    {
                        if (masaDizaynListesi[hangiMasaDizayni].masaPlanIsmi == "")
                            break;
                        for (int j = 0; j < 7; j++)
                        {
                            if (masaDizaynListesi[hangiMasaDizayni].masaYerleri[i][j] != null)
                            {
                                Button buttonTable = new Button();
                                buttonTable.Text = masaDizaynListesi[hangiMasaDizayni].masaYerleri[i][j];

                                buttonTable.UseVisualStyleBackColor = false;

                                buttonTable.Font = new Font("Arial", 21.75F, FontStyle.Bold);
                                buttonTable.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                                buttonTable.Click += siparisButonuBasildi;
                                tablePanel.Controls.Add(buttonTable, j, i);
                                tablePanel.Tag = hangiMasaDizayni;

                                buttonTable.Name = buttonTable.Text;

                                bool masaAcikMi = false;
                                for (int x = 0; x < masalar.Count; x++)
                                {
                                    if (buttonTable.Text == masalar[x])
                                    {
                                        masaAcikMi = true;
                                        break;
                                    }
                                }
                                if (masaAcikMi)
                                {
                                    buttonTable.BackColor = Color.Firebrick;
                                    buttonTable.ForeColor = Color.White;
                                }
                                else
                                {
                                    buttonTable.BackColor = Color.White;
                                    buttonTable.ForeColor = SystemColors.ActiveCaption;
                                }
                            }
                        }
                    }

                    for (int j = 6; j >= 0; j--)
                    {
                        if (masaDizaynListesi[hangiMasaDizayni].masaPlanIsmi == "")
                            break;

                        bool sutunBos = true;
                        for (int i = 5; i >= 0; i--)
                        {
                            if (masaDizaynListesi[hangiMasaDizayni].masaYerleri[i][j] != null)
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

                    for (int j = 5; j >= 0; j--)
                    {
                        if (masaDizaynListesi[hangiMasaDizayni].masaPlanIsmi == "")
                            break;
                        bool sutunBos = true;
                        for (int i = 6; i >= 0; i--)
                        {
                            if (masaDizaynListesi[hangiMasaDizayni].masaYerleri[j][i] != null)
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
                    yeniDepartman = restoranListesi[hangiDepartmanButonu].departmanAdi;
                }
            }
            else
            {
                if ((int)tablePanel.Tag != hangiMasaDizayni) //eğer seçili masa planı zaten ekrandaysa yenisi koyulmasın, ekranda değilse eskiler silinip yenisi eklensin
                {
                    gelenSiparisFormu.masaDegisikligiFormundanAcikMasaBilgisiIstegiGeldiMasaFormunaIlet("komut=departman&departmanAdi=" + restoranListesi[hangiDepartmanButonu].departmanAdi);
                }
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            yeniMasa = "iptalEdildi";
            this.Close();
        }

        // Departmandaki dolu masa bilgisini alan fonksiyon  
        public void komut_departman(string acikMasalar)
        {
            masalar.Clear();
            try
            {
                //Gelen mesajı * ile ayır
                string[] masaDizisi = acikMasalar.Split('*');
                masalar.AddRange(masaDizisi);
            }
            catch (Exception)
            {
                KontrolFormu dialog = new KontrolFormu("Masa durumlarını alırken bir hata oluştu, lütfen tekrar deneyiniz", false);
                dialog.Show();
                return;
            }

            tablePanel.RowCount = 6;
            tablePanel.ColumnCount = 7;
            tablePanel.Controls.Clear();
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (masaDizaynListesi[hangiMasaDizayni].masaYerleri[i][j] != null)
                    {
                        Button buttonTable = new Button();
                        buttonTable.Text = masaDizaynListesi[hangiMasaDizayni].masaYerleri[i][j];

                        buttonTable.UseVisualStyleBackColor = false;

                        buttonTable.Font = new Font("Arial", 21.75F, FontStyle.Bold);
                        buttonTable.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                        buttonTable.Click += siparisButonuBasildi;
                        tablePanel.Controls.Add(buttonTable, j, i);
                        tablePanel.Tag = hangiMasaDizayni;

                        buttonTable.Name = buttonTable.Text;

                        bool masaAcikMi = false;
                        for (int x = 0; x < masalar.Count; x++)
                        {
                            if (buttonTable.Text == masalar[x])
                            {
                                masaAcikMi = true;
                                break;
                            }
                        }
                        if (masaAcikMi)
                        {
                            buttonTable.BackColor = Color.Firebrick;
                            buttonTable.ForeColor = Color.White;
                        }
                        else
                        {
                            buttonTable.BackColor = Color.White;
                            buttonTable.ForeColor = SystemColors.ActiveCaption;
                        }
                    }
                }
            }

            for (int j = 6; j >= 0; j--)
            {
                if (masaDizaynListesi[hangiMasaDizayni].masaPlanIsmi == "")
                    break;

                bool sutunBos = true;
                for (int i = 5; i >= 0; i--)
                {
                    if (masaDizaynListesi[hangiMasaDizayni].masaYerleri[i][j] != null)
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

            for (int j = 5; j >= 0; j--)
            {
                if (masaDizaynListesi[hangiMasaDizayni].masaPlanIsmi == "")
                    break;
                bool sutunBos = true;
                for (int i = 6; i >= 0; i--)
                {
                    if (masaDizaynListesi[hangiMasaDizayni].masaYerleri[j][i] != null)
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

        public void komut_masaAcildi(string masa, string departmanAdi)
        {
            if (restoranListesi[hangiDepartmanButonu].departmanAdi == departmanAdi)
            {
                Button tablebutton = tablePanel.Controls[masa] as Button;
                tablebutton.ForeColor = Color.White;
                tablebutton.BackColor = Color.Firebrick;
            }
        }

        public void komut_masaKapandi(string masa, string departmanAdi)
        {
            if (restoranListesi[hangiDepartmanButonu].departmanAdi == departmanAdi)
            {
                Button tablebutton = tablePanel.Controls[masa] as Button;
                tablebutton.ForeColor = SystemColors.ActiveCaption;
                tablebutton.BackColor = Color.White;
            }
        }
    }
}
