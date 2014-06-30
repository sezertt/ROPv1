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
    public partial class MyWaitForm : Form
    {
        string labelText;

        public MyWaitForm(string labelText)
        {
            InitializeComponent();
            this.labelText = labelText;
        }

        private void MyWaitForm_Load(object sender, EventArgs e)
        {
            label1.Text = labelText;
        }
    }
}
