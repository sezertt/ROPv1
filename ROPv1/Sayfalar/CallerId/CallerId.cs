using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ROPv1.Sayfalar.CallerId
{
    public partial class CallerIDFormu : Form
    {
        public bool formAcik;
        Axcidv5callerid.ICIDv5Events_OnCallerIDEvent info;
        CallerIDDAL cDal;
        public void FillInfo()
        {
            Customer customer = new Customer();
            cDal = new CallerIDDAL();
            try
            {
                customer = cDal.selectCustomer(info.phoneNumber);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            if (info != null)
            {
                txtPhoneLine1.Text = info.phoneNumber;
                if (customer != null)
                {
                    txtNameLine1.Text = customer.Name;
                    txtLastNameLine1.Text = customer.LastName;
                    addressLine1.Text = customer.Address;
                    if (!string.IsNullOrEmpty(customer.Region))
                        txtRegionLine1.Text = customer.Region;
                    if (string.IsNullOrEmpty(customer.CustomerNote))
                        txtNoteLine1.Text = customer.CustomerNote;
                }
            }
        }
        public void insertCustomer()
        {
            cDal = new CallerIDDAL();
            Customer customer = new Customer();
            bool inserted = false;
            customer.Name = txtNameLine1.Text;
            customer.LastName = txtLastNameLine1.Text;
            customer.Address = addressLine1.Text;
            customer.Phone = txtPhoneLine1.Text;
            customer.Region = txtRegionLine1.Text;
            customer.CustomerNote = txtNoteLine1.Text;
            if (customer != null)
            {
                inserted = cDal.insertNewCustomer(customer);
            }

            if (!inserted)
                MessageBox.Show("Müşteri ad, soyad ve adres bilgilerini lütfen eksiksiz bir şekilde doldurunuz.");
        }

        public CallerIDFormu(GirisEkrani girisEkrani, Axcidv5callerid.ICIDv5Events_OnCallerIDEvent e)
        {
            InitializeComponent();
            girisEkrani.axCIDv51.OnCallerID += axCIDv51_OnCallerID;
            girisEkrani.axCIDv51.Start();
            this.info = e;
        }

        private void axCIDv51_OnCallerID(object sender, Axcidv5callerid.ICIDv5Events_OnCallerIDEvent e)
        {
            this.info = e;
            FillInfo();
        }

        private void CallerId_Load(object sender, EventArgs e)
        {
            formAcik = true;
            FillInfo();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            formAcik = false;
            this.Close();
        }

        private void btnAddCustomerLine1_Click(object sender, EventArgs e)
        {
            insertCustomer();
        }
        private void keyboardcontrol1_UserKeyPressed(object sender, KeyboardClassLibrary.KeyboardEventArgs e)
        {
            SendKeys.Send(e.KeyboardKeyPressed);
        }

    }
}
