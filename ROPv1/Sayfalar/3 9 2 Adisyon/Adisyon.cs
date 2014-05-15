using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ROPv1
{
    public partial class Adisyon : Form
    {
        string masaAdi, departmanAdi, garson;
        DateTime acilisZamani;
        decimal indirim;
        string[] yaziciBilgileri;

        public Adisyon(string masaAdi, string departmanAdi, string garson, DateTime acilisZamani, decimal indirim, string [] yaziciBilgileri)
        {
            InitializeComponent();
            this.masaAdi = masaAdi;
            this.departmanAdi = departmanAdi;
            this.garson = garson;
            this.acilisZamani = acilisZamani;
            this.indirim = indirim;
            this.yaziciBilgileri = yaziciBilgileri;

            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
        }

        private void Adisyon_Load(object sender, EventArgs e)
        {
            /*
            yaziciBilgileri[0] // yazici adı    
            yaziciBilgileri[3] // yazıcı windows adı
            */
            //bura düzeltilecek direk yazdırma için ayarlanmalı
            CrystalReport1 rapor = new CrystalReport1();
            rapor.SetParameterValue("Indirim", indirim);
            rapor.SetParameterValue("FirmaAdi",yaziciBilgileri[1]); // firma adı
            rapor.SetParameterValue("FirmaAdresTelefon",yaziciBilgileri[2] + " " + yaziciBilgileri[4]); // firma adres ve telefon
            rapor.SetParameterValue("Garson",garson);
            rapor.SetParameterValue("Masa",masaAdi);
            rapor.SetParameterValue("Departman",departmanAdi);
            rapor.SetParameterValue("AcilisZamani",acilisZamani);
            ReportView.ReportSource = rapor;

            rapor.PrintToPrinter(1, false, 0, 0);
        }
    }
}