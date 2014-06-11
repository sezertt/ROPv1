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
    public partial class AnketKullanicilari : UserControl
    {
        int kullaniciSayisi = 0;
        public AnketKullanicilari()
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

        private void labelSayfa_TextChanged(object sender, EventArgs e)
        {
            listKullanici.Items.Clear();

            int IlkAlinacakVerininSirasi, alinacakToplamVeriSayisi = 21;

            IlkAlinacakVerininSirasi = Convert.ToInt32(labelSayfa.Text) * 21;

            if (IlkAlinacakVerininSirasi > kullaniciSayisi)
            {
                alinacakToplamVeriSayisi = kullaniciSayisi % 21;
                IlkAlinacakVerininSirasi = kullaniciSayisi;
            }

            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT TOP (@_toplamVeri) Adi, Soyadi, Eposta, Telefon FROM (SELECT TOP (@_ilkAlinacakVeri) Adi, Soyadi, Eposta, Telefon FROM AnketKullanicilari ORDER BY Adi DESC) AS isim ORDER BY Adi ASC");

            cmd.Parameters.AddWithValue("@_toplamVeri", alinacakToplamVeriSayisi);
            cmd.Parameters.AddWithValue("@_ilkAlinacakVeri", IlkAlinacakVerininSirasi);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string adi = dr.GetString(0);

                string soyadi = dr.GetString(1);

                string eposta = "", telefon = "";

                if (!dr.IsDBNull(2))
                    eposta = dr.GetString(2);

                if (!dr.IsDBNull(3))
                    telefon = dr.GetString(3);

                listKullanici.Items.Add(adi);
                listKullanici.Items[listKullanici.Items.Count - 1].SubItems.Add(soyadi);
                listKullanici.Items[listKullanici.Items.Count - 1].SubItems.Add(eposta);
                listKullanici.Items[listKullanici.Items.Count - 1].SubItems.Add(telefon);
            }
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        private void buttonKullaniciyiSil_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("DELETE FROM AnketCevaplar WHERE AnketID = (SELECT AnketID FROM Anket WHERE KullaniciID=(SELECT KullaniciID FROM AnketKullanicilari WHERE Adi='" + listKullanici.SelectedItems[0].SubItems[0].Text + "' AND SoyAdi='" + listKullanici.SelectedItems[0].SubItems[1].Text + "' AND Eposta='" + listKullanici.SelectedItems[0].SubItems[2].Text + "' AND Telefon='" + listKullanici.SelectedItems[0].SubItems[3].Text + "'))");

            cmd.ExecuteNonQuery();

            cmd = SQLBaglantisi.getCommand("DELETE FROM Anket WHERE KullaniciID=(SELECT KullaniciID FROM AnketKullanicilari WHERE Adi='" + listKullanici.SelectedItems[0].SubItems[0].Text + "' AND SoyAdi='" + listKullanici.SelectedItems[0].SubItems[1].Text + "' AND Eposta='" + listKullanici.SelectedItems[0].SubItems[2].Text + "' AND Telefon='" + listKullanici.SelectedItems[0].SubItems[3].Text + "')");

            cmd.ExecuteNonQuery();

            cmd = SQLBaglantisi.getCommand("DELETE FROM AnketKullanicilari WHERE Adi='" + listKullanici.SelectedItems[0].SubItems[0].Text + "' AND SoyAdi='" + listKullanici.SelectedItems[0].SubItems[1].Text + "' AND Eposta='" + listKullanici.SelectedItems[0].SubItems[2].Text + "' AND Telefon='" + listKullanici.SelectedItems[0].SubItems[3].Text + "'");

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            listKullanici.SelectedItems[0].Remove();
        }

        private void AnketKullanicilari_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT COUNT(Adi) FROM AnketKullanicilari");
            SqlDataReader dr = cmd.ExecuteReader();

            dr.Read();

            kullaniciSayisi = dr.GetInt32(0);

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            if (kullaniciSayisi % 21 == 0)
                labelSayfaSayisi.Text = (kullaniciSayisi / 21).ToString();
            else
                labelSayfaSayisi.Text = ((kullaniciSayisi / 21) + 1).ToString();

            labelSayfa.Text = "1";
        }
    }
}