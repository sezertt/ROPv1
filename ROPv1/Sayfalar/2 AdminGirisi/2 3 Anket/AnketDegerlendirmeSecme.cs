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
    public partial class AnketDegerlendirmeSecme : UserControl
    {
        int SecmeliSoruSayisi = 0;

        public AnketDegerlendirmeSecme()
        {
            InitializeComponent();
        }

        private void AnketDegerlendirme_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT COUNT(SoruID) FROM AnketSorular WHERE SorununSirasi<16");
            SqlDataReader dr = cmd.ExecuteReader();

            dr.Read();

            SecmeliSoruSayisi = dr.GetInt32(0);

            cmd = SQLBaglantisi.getCommand("SELECT AVG(AnketPuani) FROM Anket");
            dr = cmd.ExecuteReader();
            dr.Read();
            
            decimal ortPuan = dr.GetDecimal(0);

            int yildiz = 0;

            if (ortPuan == 0)
            {
            }
            else if (ortPuan > 0 && ortPuan < 31) // 1 Yıldız
            {
                yildiz = 1;
            }
            else if (ortPuan < 41) // 2 Yıldız
            {
                yildiz = 2;
            }
            else if (ortPuan < 71) // 3 Yıldız
            {
                yildiz = 3;
            }
            else if (ortPuan < 91) // 4 Yıldız
            {
                yildiz = 4;
            }
            else // 5 Yıldız
            {
                yildiz = 5;
            }

            labelGenelPuanYildiz.Text = ortPuan.ToString("0.00") + " Puan\r\n" + yildiz + " Yıldız";

            cmd = SQLBaglantisi.getCommand("SELECT Cevap FROM AnketCevaplar WHERE SorununSirasi<16");
            dr = cmd.ExecuteReader();

            decimal[] cevapSayilari = new decimal[5];

            decimal toplamCevapSayisi = 0;

            while (dr.Read())
            {
                toplamCevapSayisi++;
                cevapSayilari[Convert.ToInt32(dr.GetString(0)) - 1]++;
            }

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            if(toplamCevapSayisi > 0)
            {
                label1Yildiz.Text = "%" + (cevapSayilari[0] / toplamCevapSayisi * 100).ToString("0.0") + " - 1 Yıldız";
                label2Yildiz.Text = "%" + (cevapSayilari[1] / toplamCevapSayisi * 100).ToString("0.0") + " - 2 Yıldız";
                label3Yildiz.Text = "%" + (cevapSayilari[2] / toplamCevapSayisi * 100).ToString("0.0") + " - 3 Yıldız";
                label4Yildiz.Text = "%" + (cevapSayilari[3] / toplamCevapSayisi * 100).ToString("0.0") + " - 4 Yıldız";
                label5Yildiz.Text = "%" + (cevapSayilari[4] / toplamCevapSayisi * 100).ToString("0.0") + " - 5 Yıldız";
            }

            if (SecmeliSoruSayisi % 19 == 0)
                labelSayfaSayisi.Text = (SecmeliSoruSayisi / 19).ToString();
            else
                labelSayfaSayisi.Text = ((SecmeliSoruSayisi / 19) + 1).ToString();

            labelSayfa.Text = "1";
        }

        private void buttonSayfaAzalt_Click_1(object sender, EventArgs e)
        {
            if (Convert.ToInt32(labelSayfa.Text) < 2)
                return;
            labelSayfa.Text = (Convert.ToInt32(labelSayfa.Text) - 1).ToString();
        }

        private void buttonSayfaArttir_Click_1(object sender, EventArgs e)
        {
            if (Convert.ToInt32(labelSayfa.Text) == Convert.ToInt32(labelSayfaSayisi.Text))
                return;
            labelSayfa.Text = (Convert.ToInt32(labelSayfa.Text) + 1).ToString();
        }

        private void labelSayfa_TextChanged_1(object sender, EventArgs e)
        {
            listSecmeliSoru.Items.Clear();

            int IlkAlinacakVerininSirasi, alinacakToplamVeriSayisi = 19;

            IlkAlinacakVerininSirasi = Convert.ToInt32(labelSayfa.Text) * 19;

            if (IlkAlinacakVerininSirasi > SecmeliSoruSayisi)
            {
                alinacakToplamVeriSayisi = SecmeliSoruSayisi % 19;
                IlkAlinacakVerininSirasi = SecmeliSoruSayisi;
            }

            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT TOP (@_toplamVeri) SoruID, Soru FROM (SELECT TOP (@_ilkAlinacakVeri) SoruID, Soru FROM AnketSorular WHERE SorununSirasi<16 ORDER BY SoruID DESC) AS isim ORDER BY SoruID ASC");

            cmd.Parameters.AddWithValue("@_toplamVeri", alinacakToplamVeriSayisi);
            cmd.Parameters.AddWithValue("@_ilkAlinacakVeri", IlkAlinacakVerininSirasi);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                int soruID = dr.GetInt32(0);
                string soru = dr.GetString(1);

                listSecmeliSoru.Items.Insert(0, soru);

                SqlCommand cmd2 = SQLBaglantisi.getCommand("SELECT Cevap FROM AnketCevaplar WHERE SoruID=@_SoruID AND SorununSirasi<16");

                cmd2.Parameters.AddWithValue("@_SoruID", soruID);

                SqlDataReader dr2 = cmd2.ExecuteReader();

                int cevapSayisi = 0;
                int[] yildizSayisi = new int[5];

                while (dr2.Read())
                {
                    cevapSayisi++;

                    int cevap = Convert.ToInt32(dr2.GetString(0)) - 1;

                    yildizSayisi[cevap]++;

                }

                cmd2.Connection.Close();
                cmd2.Connection.Dispose();

                listSecmeliSoru.Items[0].UseItemStyleForSubItems = false;

                if (cevapSayisi > 0)
                {
                    for (int i = 4; i >= 0; i--)
                    {
                        listSecmeliSoru.Items[0].SubItems.Add((yildizSayisi[i] / cevapSayisi * 100).ToString());
                    }
                }

                decimal ortalama = yildizSayisi[0] + yildizSayisi[1] * 2 + yildizSayisi[2] * 3 + yildizSayisi[3] * 4 + yildizSayisi[4] * 5;

                if (ortalama > 0 && ortalama <= cevapSayisi) // 1 yıldız
                {
                    listSecmeliSoru.Items[0].SubItems[5].BackColor = SystemColors.ActiveCaption;
                    listSecmeliSoru.Items[0].SubItems[5].ForeColor = Color.White;
                }
                else if (ortalama > cevapSayisi && ortalama <= cevapSayisi * 2) // 2 yıldız
                {
                    listSecmeliSoru.Items[0].SubItems[4].BackColor = SystemColors.ActiveCaption;
                    listSecmeliSoru.Items[0].SubItems[4].ForeColor = Color.White;
                }
                else if (ortalama > cevapSayisi && ortalama <= cevapSayisi * 3) // 3 yıldız
                {
                    listSecmeliSoru.Items[0].SubItems[3].BackColor = SystemColors.ActiveCaption;
                    listSecmeliSoru.Items[0].SubItems[3].ForeColor = Color.White;
                }
                else if (ortalama > cevapSayisi && ortalama <= cevapSayisi * 4) // 4 yıldız
                {
                    listSecmeliSoru.Items[0].SubItems[2].BackColor = SystemColors.ActiveCaption;
                    listSecmeliSoru.Items[0].SubItems[2].ForeColor = Color.White;
                }
                else if (ortalama > cevapSayisi && ortalama <= cevapSayisi * 5) // 5 yıldız
                {
                    listSecmeliSoru.Items[0].SubItems[1].BackColor = SystemColors.ActiveCaption;
                    listSecmeliSoru.Items[0].SubItems[1].ForeColor = Color.White;
                }
            }
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }
    }
}