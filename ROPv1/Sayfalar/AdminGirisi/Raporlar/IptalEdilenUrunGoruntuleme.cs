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
    public partial class IptalEdilenUrunGoruntuleme : UserControl
    {
        int iptalEdilenUrunSayisi = 0;

        public IptalEdilenUrunGoruntuleme()
        {
            InitializeComponent();
        }

        private void AnketDegerlendirme_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT COUNT(SiparisID) FROM Siparis WHERE IptalNedeni IS NOT NULL");
            SqlDataReader dr = cmd.ExecuteReader();

            dr.Read();

            iptalEdilenUrunSayisi = dr.GetInt32(0);

            if (iptalEdilenUrunSayisi % 10 == 0)
                labelSayfaSayisi.Text = (iptalEdilenUrunSayisi / 10).ToString();
            else
                labelSayfaSayisi.Text = ((iptalEdilenUrunSayisi / 10) + 1).ToString();

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
            listIptalEdilenSorular.Items.Clear();

            int IlkAlinacakVerininSirasi, alinacakToplamVeriSayisi = 10;

            IlkAlinacakVerininSirasi = Convert.ToInt32(labelSayfa.Text) * 10;

            if (IlkAlinacakVerininSirasi > iptalEdilenUrunSayisi)
            {
                alinacakToplamVeriSayisi = iptalEdilenUrunSayisi % 10;
                IlkAlinacakVerininSirasi = iptalEdilenUrunSayisi;
            }

            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT TOP (@_toplamVeri) Garsonu, Fiyatı, Adet, YemekAdi, IkramMi, Porsiyon, IptalNedeni, VerilisTarihi FROM (SELECT TOP (@_ilkAlinacakVeri) Garsonu, Fiyatı, Adet, YemekAdi, IkramMi, Porsiyon, IptalNedeni, VerilisTarihi FROM Siparis WHERE IptalNedeni IS NOT NULL ORDER BY VerilisTarihi DESC) AS isim ORDER BY VerilisTarihi ASC");

            cmd.Parameters.AddWithValue("@_toplamVeri", alinacakToplamVeriSayisi);
            cmd.Parameters.AddWithValue("@_ilkAlinacakVeri", IlkAlinacakVerininSirasi);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string garsonu = dr.GetString(0);
                decimal fiyati = dr.GetDecimal(1);
                int adedi = dr.GetInt32(2);
                string yemekAdi = dr.GetString(3);
                bool ikramMi = dr.GetBoolean(4);
                decimal porsiyonu = dr.GetDecimal(5);
                string iptalNedeni = dr.GetString(6);

                string ikramBilgisi;
                if (ikramMi)
                    ikramBilgisi = "Evet";
                else
                    ikramBilgisi = "Hayır";

                listIptalEdilenSorular.Items.Insert(0, " - İPTAL NEDENİ - " + iptalNedeni);

                listIptalEdilenSorular.Items.Insert(0, " + ÜRÜN - " + yemekAdi + " + ADET - " + adedi + " + PORSİYON - " + porsiyonu + " + FİYAT - " + fiyati + " + İKRAM MI ? - " + ikramBilgisi + " + TOPLAM FİYAT - " + fiyati * adedi + " + GARSON - " + garsonu);                
            }
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }
    }
}