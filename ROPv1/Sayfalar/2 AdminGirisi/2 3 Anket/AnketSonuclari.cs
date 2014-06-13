using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Data.SqlClient;

namespace ROPv1
{
    public partial class AnketSonuclari : UserControl
    {
        int anketSayisi = 0;

        public AnketSonuclari()
        {
            InitializeComponent();
        }

        private void buttonSayfaArttir_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(labelSayfa.Text) == Convert.ToInt32(labelSayfaSayisi.Text))
                return;
            labelSayfa.Text = (Convert.ToInt32(labelSayfa.Text) + 1).ToString();
        }

        private void buttonSayfaAzalt_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(labelSayfa.Text) < 2)
                return;
            labelSayfa.Text = (Convert.ToInt32(labelSayfa.Text) - 1).ToString();
        }

        private void AnketSonuclari_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT COUNT(AnketID) FROM Anket");
            SqlDataReader dr = cmd.ExecuteReader();

            dr.Read();

            anketSayisi = dr.GetInt32(0);

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            if (anketSayisi % 22 == 0)
                labelSayfaSayisi.Text = (anketSayisi / 22).ToString();
            else
                labelSayfaSayisi.Text = ((anketSayisi / 22) + 1).ToString();

            labelSayfa.Text = "1";
        }

        private void labelSayfa_TextChanged(object sender, EventArgs e)
        {
            listAnketID.Items.Clear();

            int IlkAlinacakVerininSirasi, alinacakToplamVeriSayisi = 22;

            IlkAlinacakVerininSirasi = Convert.ToInt32(labelSayfa.Text) * 22;

            if (IlkAlinacakVerininSirasi > anketSayisi)
            {
                alinacakToplamVeriSayisi = anketSayisi % 22;
                IlkAlinacakVerininSirasi = anketSayisi;
            }

            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT TOP (@_toplamVeri) AnketID FROM (SELECT TOP (@_ilkAlinacakVeri) AnketID FROM Anket ORDER BY AnketID DESC) AS isim ORDER BY AnketID ASC");

            cmd.Parameters.AddWithValue("@_toplamVeri", alinacakToplamVeriSayisi);
            cmd.Parameters.AddWithValue("@_ilkAlinacakVeri", IlkAlinacakVerininSirasi);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                int AnketID = dr.GetInt32(0);
                listAnketID.Items.Insert(0, AnketID.ToString());
            }
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        private void listAnketID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listAnketID.SelectedItems.Count != 1)
                return;

            listSecmeliSoru.Items.Clear();
            textBoxYaziliSorular.Clear();

            decimal genelPuan = 0;
            int yildiz = 0;

            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT Cevap,AnketCevaplar.EtkiYuzdesi,Soru,AnketCevaplar.SorununSirasi FROM AnketCevaplar JOIN AnketSorular ON AnketSorular.SoruID=AnketCevaplar.SoruID WHERE AnketID='" + listAnketID.SelectedItems[0].Text + "' ORDER BY AnketCevaplar.SorununSirasi ASC");
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string cevap = dr.GetString(0), soru = dr.GetString(2);
                int sorununSirasi = dr.GetInt32(3);

                if (sorununSirasi < 16)
                {
                    decimal etki = dr.GetDecimal(1);
                    int cevapDegeri = Convert.ToInt32(cevap);
                    decimal carpan = 0;

                    switch(cevapDegeri)
                    {
                        case 1:
                            carpan = 0;
                            break;
                        case 2:
                            carpan = 0.25M;
                            break;
                        case 3:
                            carpan = 0.5M;
                            break;
                        case 4:
                            carpan = 0.75M;
                            break;
                        case 5:
                            carpan = 1;
                            break;
                    }

                    listSecmeliSoru.Items.Add(soru);
                    listSecmeliSoru.Items[listSecmeliSoru.Items.Count - 1].SubItems.Add(cevap);
                    listSecmeliSoru.Items[listSecmeliSoru.Items.Count - 1].SubItems.Add(etki.ToString());
                    listSecmeliSoru.Items[listSecmeliSoru.Items.Count - 1].SubItems.Add((carpan * etki).ToString("0.0"));
                    genelPuan += carpan * etki;
                }
                else
                {
                    textBoxYaziliSorular.Text += " + " + soru + "\r\n";
                    textBoxYaziliSorular.Text += " - " + cevap + "\r\n";
                }
            }

            labelGenelPuan.Text = genelPuan.ToString("0.0");
            if(genelPuan == 0)
            {
            }
            if (genelPuan > 0 && genelPuan < 31) // 1 Yıldız
            {
                yildiz = 1;
            }
            else if (genelPuan < 41) // 2 Yıldız
            {
                yildiz = 2;
            }
            else if (genelPuan < 71) // 3 Yıldız
            {
                yildiz = 3;
            }
            else if (genelPuan < 91) // 4 Yıldız
            {
                yildiz = 4;
            }
            else // 5 Yıldız
            {
                yildiz = 5;
            }

            labelYildiz.Text = yildiz.ToString();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }
    }
}