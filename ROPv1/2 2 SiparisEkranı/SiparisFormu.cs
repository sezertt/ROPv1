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
    public partial class SiparisFormu : Form
    {
        bool closeOrShowAnotherForm = false;
                
        public SiparisFormu()
        {            
            InitializeComponent();
            //gün başı yapılmış mı bak yapılmışsa daybutton resmini set et
            /*
             if(gunBasi)
             {
                dayButton.Image = global::ROPv1.Properties.Resources.dayOn;
             }
             else
             {
                dayButton.Image = global::ROPv1.Properties.Resources.dayOff;
             }
             */
        }

        private void CloseApp(object sender, FormClosedEventArgs e)
        {
            if (!closeOrShowAnotherForm) // eğer başka bir forma gidilmeyecekse uygulamayı kapat
                Application.Exit();
        }

        private void exitPressed(object sender, EventArgs e)
        {
            DialogResult eminMisiniz;

            using (KontrolFormu dialog = new KontrolFormu("Çıkmak istediğinizden emin misiniz?", true))
            {
                eminMisiniz = dialog.ShowDialog();
            }
            if (eminMisiniz == DialogResult.Yes)
            {
                closeOrShowAnotherForm = true; // başka forma geçilecek uygulamayı kapatma
                GirisEkrani girisForm = new GirisEkrani();
                girisForm.Show();
                this.Close();
            }
        }

        private void adminButton_Click(object sender, EventArgs e)
        {
            closeOrShowAnotherForm = true; // başka forma geçilecek uygulamayı kapatma
            AdminGirisFormu adminForm = new AdminGirisFormu(closeOrShowAnotherForm); // admin formuna siparis formundan gidildiği bilgisini ver
            adminForm.Show();
            this.Close();
        }
    }
}
