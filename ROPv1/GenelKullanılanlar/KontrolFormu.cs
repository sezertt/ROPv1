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
        public KontrolFormu(string textboxText,bool BoxType)
        {
            InitializeComponent();
            labelAciklama.Text = textboxText;
            if (!BoxType) //OK Box
            {
                buttonNO.Visible = false;
                buttonYES.Visible = false;
                buttonOK.Visible = true;
            }
        }
    }
}
