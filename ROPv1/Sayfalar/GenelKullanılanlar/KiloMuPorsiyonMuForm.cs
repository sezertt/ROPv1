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
    public partial class KiloMuPorsiyonMuForm : Form
    {
        public KiloMuPorsiyonMuForm()
        {
            InitializeComponent();

            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
        }
    }
}