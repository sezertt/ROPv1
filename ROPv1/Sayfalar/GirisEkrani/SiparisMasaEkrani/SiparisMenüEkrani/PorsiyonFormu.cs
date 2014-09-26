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
    public partial class PorsiyonFormu : Form
    {
        SiparisMenuFormu menuForm;

        public PorsiyonFormu(int porsiyonSinifi, SiparisMenuFormu menuForm)
        {
            this.menuForm = menuForm;
            InitializeComponent();

            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;

            if(porsiyonSinifi == 0) // tam porsiyon
            {
                buttonYarim.Enabled = false;
                buttonBirBucuk.Enabled = false;
                buttonCeyrek.Enabled = false;
                buttonUcCeyrek.Enabled = false;
            }
            else if(porsiyonSinifi == 1) //yarım porsiyon
            {
                buttonCeyrek.Enabled = false;
                buttonUcCeyrek.Enabled = false;
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            menuForm.porsiyonFormKapaniyor(((Button)sender).Text);
            this.Close();
        }
    }
}