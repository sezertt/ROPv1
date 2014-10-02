using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Data.SqlClient;
using ROPv1.CrystalReportsAnaRaporlar;
using System.Reflection;
using Microsoft.Office.Interop;

namespace ROPv1
{
    public partial class Raporlar : UserControl
    {
        CrystalReportGunSonuRaporu raporGunSonu;

        string baslangic, bitis;
        bool hangiTakvimFocuslu = true, gunSonuRaporuMu = true;

        public Raporlar(bool gunSonuRaporuMu)
        {
            InitializeComponent();

            this.gunSonuRaporuMu = gunSonuRaporuMu;
        }

        private void Raporlar_Load(object sender, EventArgs e)
        {
            dateBitis.MaxDate = DateTime.Parse(DateTime.Today.ToString("d MMMM yyy") + " 23:59:59");
            dateBaslangic.MaxDate = DateTime.Parse(DateTime.Today.ToString("d MMMM yyy") + " 23:59:59");
            dateBitis.Value = DateTime.Today;
            dateBitis.Value = DateTime.Parse(DateTime.Today.ToString("d MMMM yyy") + " 23:59:59");
            dateBaslangic.Value = DateTime.Today;
        }

        private void comboAdisyonAyar_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (((ComboBox)sender).SelectedIndex)
            {
                case 0: // Bugün
                    baslangic = DateTime.Today.ToString("yyyy-MM-dd") + " 00:00:00";
                    bitis = DateTime.Today.ToString("yyyy-MM-dd") + " 23:59:59";
                    break;
                case 1: // Dün
                    baslangic = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd") + " 00:00:00";
                    bitis = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59";
                    break;
                case 2: // Bu Hafta
                    baslangic = DateTime.Today.GetFirstDayOfWeek().ToString("yyyy-MM-dd") + " 00:00:00";
                    bitis = DateTime.Today.GetLastDayOfWeek().ToString("yyyy-MM-dd") + " 23:59:59";
                    break;
                case 3: // Önceki Hafta
                    baslangic = DateTime.Today.GetFirstDayOfWeek().AddDays(-7).ToString("yyyy-MM-dd") + " 00:00:00";
                    bitis = DateTime.Today.GetLastDayOfWeek().AddDays(-7).ToString("yyyy-MM-dd") + " 23:59:59";
                    break;
                case 4: // Bu Ay
                    DateTime firstOfNextMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1);
                    baslangic = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).ToString("yyyy-MM-dd") + " 00:00:00";
                    bitis = firstOfNextMonth.AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59";
                    break;
                case 5: // Önceki Ay
                    DateTime firstOfthisMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    baslangic = new DateTime(DateTime.Today.Year, DateTime.Today.AddMonths(-1).Month, 1).ToString("yyyy-MM-dd") + " 00:00:00";
                    bitis = firstOfthisMonth.AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59";
                    break;
                case 6: // Seçilen Tarih
                    baslangic = dateBaslangic.Value.ToString("yyyy-MM-dd HH:mm") + ":00";
                    bitis = dateBitis.Value.ToString("yyyy-MM-dd HH:mm") + ":59";
                    break;
            }

            decimal toplamSiparisTutari = 0;

            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT SUM(s.Fiyatı*s.Adet) FROM Siparis s JOIN Adisyon a ON s.AdisyonID=a.AdisyonID WHERE a.KapanisZamani >='" + baslangic + "' AND a.KapanisZamani <= '" + bitis + "' ");
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

            raporGunSonu.SetParameterValue("BaslangicTarihi", baslangic);
            raporGunSonu.SetParameterValue("BitisTarihi", bitis);
            raporGunSonu.SetParameterValue("toplamFiyat", toplamSiparisTutari);

            crystalReportViewer1.ReportSource = raporGunSonu;
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

            if (comboAdisyonAyar.SelectedIndex == 6)
            {
                comboAdisyonAyar_SelectedIndexChanged(comboAdisyonAyar, null);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            raporGunSonu = new CrystalReportGunSonuRaporu();
           
            buttonRaporla.Visible = false;
            comboAdisyonAyar.SelectedIndex = 0;
            comboAdisyonAyar.Enabled = true;
        }

        void CallSaveDialog() 
        {
            var desktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = desktopFolder;
            saveFileDialog1.Filter = "PDF (*.pdf)|*.pdf"; //"txt files (*.txt)|*.txt|All files (*.*)|*.*"
            saveFileDialog1.RestoreDirectory = true;

            DialogResult saveDialog = saveFileDialog1.ShowDialog();

            if (saveDialog == DialogResult.OK)
            {
                raporGunSonu.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, saveFileDialog1.FileName); 
            }
        }

        void CallSaveDialog1()
        {
            var desktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = desktopFolder;
            saveFileDialog1.Filter = "Excel (*.xls)|*.xls"; //"txt files (*.txt)|*.txt|All files (*.*)|*.*"
            saveFileDialog1.RestoreDirectory = true;

            DialogResult saveDialog = saveFileDialog1.ShowDialog();

            if (saveDialog == DialogResult.OK)
            {
                raporGunSonu.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, saveFileDialog1.FileName);
            }

            object oMissing = Missing.Value;
            Microsoft.Office.Interop.Excel.Application app;
            Microsoft.Office.Interop.Excel.Workbook wkBk;
            Microsoft.Office.Interop.Excel.Worksheet wkSht;

            string filename = saveFileDialog1.FileName;
            app = new Microsoft.Office.Interop.Excel.Application();
            app.DisplayAlerts = false;
            wkBk = app.Workbooks.Open(filename, oMissing, oMissing, oMissing, oMissing, oMissing,
                oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);
            wkSht = (Microsoft.Office.Interop.Excel.Worksheet)wkBk.Sheets.get_Item(1);

            Microsoft.Office.Interop.Excel.Range last = wkSht.Cells.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            Microsoft.Office.Interop.Excel.Range range = wkSht.get_Range("A1", last);

            //Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)wkSht.Cells[iTotalRows, iTotalColumns];

            range.Columns.AutoFit();

            wkBk.Save();
            app.Visible = false;
            wkBk.Close(oMissing, filename, oMissing);
            app.Quit();

            Marshal.ReleaseComObject(wkSht);
            Marshal.ReleaseComObject(wkBk);
            Marshal.ReleaseComObject(app);
            wkSht = null;
            wkBk = null;
            app = null;
            GC.Collect();
        }

        void CallSaveDialog2()
        {
            var desktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = desktopFolder;
            saveFileDialog1.Filter = "Word (*.doc)|*.doc"; //"txt files (*.txt)|*.txt|All files (*.*)|*.*"
            saveFileDialog1.RestoreDirectory = true;

            DialogResult saveDialog = saveFileDialog1.ShowDialog();

            if (saveDialog == DialogResult.OK)
            {
                raporGunSonu.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, saveFileDialog1.FileName);
            }
        }

        private void buttonPdf_Click(object sender, EventArgs e)
        {
            Thread myth;
            myth = new Thread(new System.Threading.ThreadStart(CallSaveDialog));
            myth.SetApartmentState(ApartmentState.STA);
            myth.Start();              
        }

        private void buttonExcel_Click(object sender, EventArgs e)
        {
            Thread myth;
            myth = new Thread(new System.Threading.ThreadStart(CallSaveDialog1));
            myth.SetApartmentState(ApartmentState.STA);
            myth.Start();  
        }

        private void buttonWord_Click(object sender, EventArgs e)
        {
            Thread myth;
            myth = new Thread(new System.Threading.ThreadStart(CallSaveDialog2));
            myth.SetApartmentState(ApartmentState.STA);
            myth.Start();  
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