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
        int hangiButtonSecili = 0;

        List<Restoran> restoranListesi = new List<Restoran>();

        List<MasaDizayn> masaDizaynListesi = new List<MasaDizayn>();

        public string yeniMasa;

        public string yeniDepartman;

        public int yapilmasiGerekenIslem;

        string eskiMasa, eskiDepartman;

        bool MasaMi;

        public MasaDegistirFormu(string masaAdi, string DepartmanAdi, bool MasaMiUrunMu)
        {
            InitializeComponent();

            eskiMasa = masaAdi;
            eskiDepartman = DepartmanAdi;
            MasaMi = MasaMiUrunMu;
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

            // Masa kullanımda mı bakıyoruz
            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT MasaId FROM IslemdekiMasalar WHERE DepartmanAdlari='" + yeniDepartman + "' AND MasaAdlari='" + yeniMasa + "'");
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                try
                {
                    dr.GetInt32(0);
                    using (KontrolFormu dialog = new KontrolFormu("Seçtiğiniz masa şu anda kullanımda lütfen başka bir masa seçiniz ya da daha sonra tekrar deneyiniz", false))
                    {
                        dialog.ShowDialog();
                        cmd.Connection.Close();
                        cmd.Connection.Dispose();
                        return;
                    }
                }
                catch
                { }
            }
            cmd.Connection.Close();
            cmd.Connection.Dispose();

            DialogResult eminMisiniz;

            if (MasaMi) //masa değişimi
            {
                using (KontrolFormu dialog = new KontrolFormu(eskiDepartman + " departmanındaki, " + eskiMasa + " adlı masanın hesabı " + yeniDepartman + " departmanındaki " + yeniMasa + " adlı masanın hesabı ile yer değiştirilsin mi ?", true))
                {
                    eminMisiniz = dialog.ShowDialog();
                }
            }
            else // ürün kaydırma
            {
                using (KontrolFormu dialog = new KontrolFormu("Ürünler " + eskiDepartman + " departmanındaki, " + eskiMasa + " masasından " + yeniDepartman + " departmanındaki " + yeniMasa + " masasına aktarılsın mı ?", true))
                {
                    eminMisiniz = dialog.ShowDialog();
                }
            }

            if (eminMisiniz == DialogResult.Yes)
            {

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
        }

        private void SiparisMasaFormu_Load(object sender, EventArgs e)
        {

            if (File.Exists("restoran.xml"))
            {
                XmlLoad<Restoran> loadInfo = new XmlLoad<Restoran>();
                Restoran[] info = loadInfo.LoadRestoran("restoran.xml");

                restoranListesi.AddRange(info);

                int a = 0, departmanYeri = 0;

                for (int i = 0; i < restoranListesi.Count; i++)
                {
                    Button departmanButton = new Button();
                    departmanButton.Text = restoranListesi[i].departmanAdi;
                    if (departmanButton.Text == eskiDepartman)
                    {
                        departmanButton.BackColor = SystemColors.ActiveCaption;
                        departmanButton.ForeColor = Color.White;
                        departmanYeri = i;
                        hangiButtonSecili = i;
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
                    if (masaDizaynListesi[departmanYeri].masaPlanIsmi == "")
                        break;

                    for (int j = 0; j < 7; j++)
                    {
                        if (masaDizaynListesi[departmanYeri].masaYerleri[i][j] != null)
                        {
                            Button buttonTable = new Button();
                            buttonTable.Text = masaDizaynListesi[departmanYeri].masaYerleri[i][j];
                            buttonTable.UseVisualStyleBackColor = false;

                            buttonTable.Font = new Font("Arial", 21.75F, FontStyle.Bold);
                            buttonTable.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                            tablePanel.Controls.Add(buttonTable, j, i);
                            buttonTable.Click += siparisButonuBasildi;
                            tablePanel.Tag = departmanYeri;
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
                    if (masaDizaynListesi[departmanYeri].masaPlanIsmi == "")
                        break;

                    bool sutunBos = true;
                    for (int i = 5; i > 0; i--)
                    {
                        if (masaDizaynListesi[departmanYeri].masaYerleri[i][j] != null)
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
                    if (masaDizaynListesi[departmanYeri].masaPlanIsmi == "")
                        break;

                    bool sutunBos = true;
                    for (int i = 6; i > 0; i--)
                    {
                        if (masaDizaynListesi[departmanYeri].masaYerleri[j][i] != null)
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
                yeniDepartman = restoranListesi[(int)((Button)sender).Tag].departmanAdi;
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            yeniMasa = "iptalEdildi";
            this.Close();
        }
    }
}
