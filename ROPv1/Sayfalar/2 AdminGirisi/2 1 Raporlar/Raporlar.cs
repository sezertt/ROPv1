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
using ROPv1.CrystalReports;

namespace ROPv1
{
    public partial class Raporlar : UserControl
    {
        CrystalReportGunSonuRaporu raporGunSonu;
        CrystalReportUrunSatisRaporu raporUrunSatis;

        string baslangic, bitis;
        bool hangiTakvimFocuslu = true, gunSonuRaporuMu = true;

        public Raporlar(bool gunSonuRaporuMu)
        {
            InitializeComponent();

            this.gunSonuRaporuMu = gunSonuRaporuMu;

            if (gunSonuRaporuMu)
            {
                raporGunSonu = new CrystalReportGunSonuRaporu();
            }
            else
            {
                raporUrunSatis = new CrystalReportUrunSatisRaporu();
            }
        }

        private void Raporlar_Load(object sender, EventArgs e)
        {
            dateBitis.MaxDate = DateTime.Today;
            dateBaslangic.MaxDate = DateTime.Today;
            comboAdisyonAyar.SelectedIndex = 0;
        }

        private void comboAdisyonAyar_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (((ComboBox)sender).SelectedIndex)
            {
                case 0: // Bugün
                    baslangic = DateTime.Today.ToString("yyy-MM-dd");
                    bitis = baslangic;
                    break;
                case 1: // Dün
                    baslangic = DateTime.Today.AddDays(-1).ToString("yyy-MM-dd");
                    bitis = baslangic;
                    break;
                case 2: // Bu Hafta
                    baslangic = DateTime.Today.GetFirstDayOfWeek().ToString("yyy-MM-dd");
                    bitis = DateTime.Today.GetLastDayOfWeek().ToString("yyy-MM-dd");
                    break;
                case 3: // Önceki Hafta
                    baslangic = DateTime.Today.GetFirstDayOfWeek().AddDays(-7).ToString("yyy-MM-dd");
                    bitis = DateTime.Today.GetLastDayOfWeek().AddDays(-7).ToString("yyy-MM-dd");
                    break;
                case 4: // Bu Ay
                    DateTime firstOfNextMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1);
                    baslangic = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).ToString("yyy-MM-dd");
                    bitis = firstOfNextMonth.AddDays(-1).ToString("yyyy-MM-dd");
                    break;
                case 5: // Önceki Ay
                    DateTime firstOfthisMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    baslangic = new DateTime(DateTime.Today.Year, DateTime.Today.AddMonths(-1).Month, 1).ToString("yyy-MM-dd");
                    bitis = firstOfthisMonth.AddDays(-1).ToString("yyyy-MM-dd");
                    break;
                case 6: // Seçilen Tarih
                    baslangic = dateBaslangic.Value.ToString("yyyy-MM-dd");
                    bitis = dateBitis.Value.ToString("yyyy-MM-dd");
                    break;
            }

            decimal toplamSiparisTutari = 0;
            if (!gunSonuRaporuMu)
            {
                SqlCommand cmd = SQLBaglantisi.getCommand("SELECT SUM(Fiyatı*Porsiyon) FROM Siparis WHERE VerilisTarihi >='" + baslangic + "' AND VerilisTarihi <= '" + bitis + "' ");
                SqlDataReader dr = cmd.ExecuteReader();

                dr.Read();
                try
                {
                    toplamSiparisTutari = dr.GetDecimal(0);
                }
                catch
                { }

                cmd.Connection.Close();
                cmd.Connection.Dispose();

                raporUrunSatis.SetParameterValue("BaslangicTarihi", baslangic);
                raporUrunSatis.SetParameterValue("BitisTarihi", bitis);
                raporUrunSatis.SetParameterValue("toplamFiyat", toplamSiparisTutari);

                crystalReportViewer1.ReportSource = raporUrunSatis;
            }
            else
            {
                raporGunSonu.SetParameterValue("BaslangicTarihi", baslangic);
                raporGunSonu.SetParameterValue("BitisTarihi", bitis);

                crystalReportViewer1.ReportSource = raporGunSonu;
            }           
        }

        private void comboAdisyonAyar_Click(object sender, EventArgs e)
        {
            ((ComboBox)sender).DroppedDown = true;
            ((ComboBox)sender).SelectionLength = 0;
        }

        private void comboAdisyonAyar_Leave(object sender, EventArgs e)
        {
            ((ComboBox)sender).SelectionLength = 0;
        }

        private void comboAdisyonAyar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void buttonArttir_Click(object sender, EventArgs e)
        {
            if (hangiTakvimFocuslu)
            {
                dateBaslangic.Select();
            }
            else
            {
                dateBitis.Select();
            }
            SendKeys.Send("{UP}");
        }

        private void buttonAzalt_Click(object sender, EventArgs e)
        {
            if (hangiTakvimFocuslu)
            {
                dateBaslangic.Select();
            }
            else
            {
                dateBitis.Select();
            }
            SendKeys.Send("{DOWN}");
        }

        private void dateBaslangic_Enter(object sender, EventArgs e)
        {
            hangiTakvimFocuslu = true;
        }

        private void dateBitis_Enter(object sender, EventArgs e)
        {
            hangiTakvimFocuslu = false;
        }

        private void dateBitis_ValueChanged(object sender, EventArgs e)
        {
            if (dateBaslangic.Value > dateBitis.Value)
                dateBaslangic.Value = dateBitis.Value;
            dateBaslangic.MaxDate = dateBitis.Value;

            if(comboAdisyonAyar.SelectedIndex == 6)
            {
                comboAdisyonAyar_SelectedIndexChanged(comboAdisyonAyar, null);
            }
        }
    }

    public static class DateTimeExtensions
    {
        public static DateTime GetFirstDayOfWeek(this DateTime sourceDateTime)
        {
            var daysAhead = (DayOfWeek.Sunday - (int)sourceDateTime.DayOfWeek);

            sourceDateTime = sourceDateTime.AddDays((int)daysAhead);

            return sourceDateTime;
        }

        public static DateTime GetLastDayOfWeek(this DateTime sourceDateTime)
        {
            var daysAhead = DayOfWeek.Saturday - (int)sourceDateTime.DayOfWeek;

            sourceDateTime = sourceDateTime.AddDays((int)daysAhead);

            return sourceDateTime;
        }
    }
}