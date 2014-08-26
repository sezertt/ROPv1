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
    public partial class AnketDegerlendirmeYazili : UserControl
    {
        int YaziliSoruSayisi = 0;

        public AnketDegerlendirmeYazili()
        {
            InitializeComponent();
        }

        private void AnketDegerlendirme_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT COUNT(AnketID) FROM AnketCevaplar WHERE SorununSirasi>15");
            SqlDataReader dr = cmd.ExecuteReader();

            dr.Read();

            YaziliSoruSayisi = dr.GetInt32(0);

            if (YaziliSoruSayisi % 10 == 0)
                labelSayfaSayisi.Text = (YaziliSoruSayisi / 10).ToString();
            else
                labelSayfaSayisi.Text = ((YaziliSoruSayisi / 10) + 1).ToString();

            labelSayfa.Text = "1";
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

        private void labelSayfa_TextChanged(object sender, EventArgs e)
        {
            listYaziliSoru.Items.Clear();

            int IlkAlinacakVerininSirasi, alinacakToplamVeriSayisi = 10;

            IlkAlinacakVerininSirasi = Convert.ToInt32(labelSayfa.Text) * 10;

            if (IlkAlinacakVerininSirasi > YaziliSoruSayisi)
            {
                alinacakToplamVeriSayisi = YaziliSoruSayisi % 10;
                IlkAlinacakVerininSirasi = YaziliSoruSayisi;
            }

            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT TOP (@_toplamVeri) Soru, Cevap FROM (SELECT TOP (@_ilkAlinacakVeri) Soru, Cevap FROM AnketSorular JOIN AnketCevaplar ON AnketSorular.SoruID=AnketCevaplar.SoruID WHERE AnketCevaplar.SorununSirasi>15 ORDER BY AnketSorular.Soru DESC) AS isim ORDER BY isim.Soru ASC");

            cmd.Parameters.AddWithValue("@_toplamVeri", alinacakToplamVeriSayisi);
            cmd.Parameters.AddWithValue("@_ilkAlinacakVeri", IlkAlinacakVerininSirasi);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string soru = dr.GetString(0);
                string cevap = dr.GetString(1);

                listYaziliSoru.Items.Insert(0, " - " + cevap);
                listYaziliSoru.Items.Insert(0, " + " + soru);
            }
            cmd.Connection.Close();
            cmd.Connection.Dispose();

            listYaziliSoru.Columns[0].Width = listYaziliSoru.Width;
        }

        private void listYaziliSoru_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}