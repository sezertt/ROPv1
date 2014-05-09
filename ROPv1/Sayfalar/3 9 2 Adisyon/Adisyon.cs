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
        public Adisyon(string masaAdi, string departmanAdi, string garson, DateTime acilisZamani)
        {
            InitializeComponent();
            this.masaAdi = masaAdi;
            this.departmanAdi = departmanAdi;
            this.garson = garson;
            this.acilisZamani = acilisZamani;

            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
        }

        private void Adisyon_Load(object sender, EventArgs e)
        {
            CrystalReport1 rapor = new CrystalReport1();
            rapor.SetParameterValue("FirmaAdi","Liva");
            rapor.SetParameterValue("FirmaAdresTelefon","asdgadgdgsdg dsgfsdgsd sdgfsd");
            rapor.SetParameterValue("Garson",garson);
            rapor.SetParameterValue("Masa",masaAdi);
            rapor.SetParameterValue("Departman",departmanAdi);
            rapor.SetParameterValue("AcilisZamani",acilisZamani);
            ReportView.ReportSource = rapor;
        }
    }
}