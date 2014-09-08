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
        const int goruntulenecekAdet = 9;

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

            if (iptalEdilenUrunSayisi % goruntulenecekAdet == 0)
                labelSayfaSayisi.Text = (iptalEdilenUrunSayisi / goruntulenecekAdet).ToString();
            else
                labelSayfaSayisi.Text = ((iptalEdilenUrunSayisi / goruntulenecekAdet) + 1).ToString();

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

            int IlkAlinacakVerininSirasi, alinacakToplamVeriSayisi = goruntulenecekAdet;

            IlkAlinacakVerininSirasi = Convert.ToInt32(labelSayfa.Text) * goruntulenecekAdet;

            if (IlkAlinacakVerininSirasi > iptalEdilenUrunSayisi)
            {
                alinacakToplamVeriSayisi = iptalEdilenUrunSayisi % goruntulenecekAdet;
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

                GlacialComponents.Controls.GLItem glitem = new GlacialComponents.Controls.GLItem();

                glitem.Text = yemekAdi;

                glitem.SubItems[1].Text = adedi.ToString();
                glitem.SubItems[2].Text = porsiyonu.ToString();
                glitem.SubItems[3].Text = fiyati.ToString();
                glitem.SubItems[4].Text = ikramBilgisi;
                glitem.SubItems[5].Text = (fiyati * adedi).ToString();
                glitem.SubItems[6].Text = garsonu;
                glitem.SubItems[7].Text = iptalNedeni;

                glitem.SubItems[0].ForeColor = SystemColors.ActiveCaption;
                glitem.SubItems[1].ForeColor = SystemColors.ActiveCaption;
                glitem.SubItems[2].ForeColor = SystemColors.ActiveCaption;
                glitem.SubItems[3].ForeColor = SystemColors.ActiveCaption;
                glitem.SubItems[4].ForeColor = SystemColors.ActiveCaption;
                glitem.SubItems[5].ForeColor = SystemColors.ActiveCaption;
                glitem.SubItems[6].ForeColor = SystemColors.ActiveCaption;
                glitem.SubItems[7].ForeColor = SystemColors.ActiveCaption;

                glitem.ForeColor = SystemColors.ActiveCaption;

                glitem.SubItems[0].ForceText = true;
                glitem.SubItems[1].ForceText = true;
                glitem.SubItems[2].ForceText = true;
                glitem.SubItems[3].ForceText = true;
                glitem.SubItems[4].ForceText = true;
                glitem.SubItems[5].ForceText = true;
                glitem.SubItems[6].ForceText = true;
                glitem.SubItems[7].ForceText = true;

                listIptalEdilenSorular.Items.Insert(0, glitem);

            }
            cmd.Connection.Close();
            cmd.Connection.Dispose();

            listIptalEdilenSorular.Refresh();
            listIptalEdilenSorular.Items[0].Selected = true;
        }
    }
}