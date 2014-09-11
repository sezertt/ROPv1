using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ROPv1
{
    public partial class KontrolFormu : Form
    {
        SiparisMenuFormu menuFormu;
        SiparisMasaFormu masaFormu;
        AnketKullanicilari anketKullaniciFormu;

        public KontrolFormu(string textboxText, bool BoxType, SiparisMenuFormu menuFormuGelen)
        {
            InitializeComponent();

            if (menuFormuGelen != null)
                this.menuFormu = menuFormuGelen;

            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;

            labelAciklama.Text = textboxText;

            if (!BoxType) //OK Box
            {
                buttonNO.Visible = false;
                buttonYES.Visible = false;
                buttonTamam.Visible = true;
            }
        }

        public KontrolFormu(string textboxText, bool BoxType, SiparisMasaFormu masaFormuGelen)
        {
            InitializeComponent();

            if (masaFormuGelen != null)
                this.masaFormu = masaFormuGelen;

            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;

            labelAciklama.Text = textboxText;
            buttonDevamEt.Visible = true;
            buttonDevamEtme.Visible = true;
            buttonNO.Visible = false;
            buttonYES.Visible = false;
            buttonTamam.Visible = false;
        }

        public KontrolFormu(string textboxText, bool BoxType, AnketKullanicilari anketKullaniciFormu)
        {
            InitializeComponent();

            if (anketKullaniciFormu != null)
                this.anketKullaniciFormu = anketKullaniciFormu;

            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;

            labelAciklama.Text = textboxText;
            buttonDevamEt.Visible = false;
            buttonDevamEtme.Visible = false;
            buttonNO.Visible = true;
            buttonYES.Visible = true;
            buttonTamam.Visible = false;
        }

        public KontrolFormu(string textboxText, bool BoxType)
        {
            InitializeComponent();

            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;

            labelAciklama.Text = textboxText;

            if (!BoxType) //OK Box
            {
                buttonNO.Visible = false;
                buttonYES.Visible = false;
                buttonTamam.Visible = true;
            }
        }


        private void KontrolFormu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.D3) //Kısayol Tuşları ile ekranı açıyoruz ctrl+shift+3
            {
                PortFormu portFormu = new PortFormu();
                portFormu.ShowDialog();
            }
        }

        private void buttonTamam_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonDevamEt_Click(object sender, EventArgs e)
        {
            if (masaFormu != null)
                masaFormu.komut_masaGirilebilirMi("True",true);
            this.Close();
        }

        private void buttonNO_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonDevamEtme_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonYES_Click(object sender, EventArgs e)
        {
            if (anketKullaniciFormu != null)
                anketKullaniciFormu.kullaniciyiSilOnaylandi();
            this.Close();
        }

        private void KontrolFormu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(menuFormu!=null)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    menuFormu.Close();
                }); 
            }
        }
    }
}