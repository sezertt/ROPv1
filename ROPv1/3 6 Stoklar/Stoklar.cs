using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Globalization;

namespace ROPv1
{
    public partial class Stoklar : UserControl
    {
        List<StokBilgileri> stokListesi = new List<StokBilgileri>();

        public Stoklar()
        {
            InitializeComponent();

            buttonDeleteStok.Visible = true;
            buttonDeleteStok.Enabled = false;
            buttonSaveNewStok.Enabled = false;
            buttonCancel.Visible = false;
            buttonAddNewStok.Enabled = true;
            textboxUrunAdi.Enabled = false;
            textBoxUrunMiktari.Enabled = false;

            StokBilgileri[] info = new StokBilgileri[1];

            if (File.Exists("stoklar.xml"))
            {

                XmlLoad<StokBilgileri> loadInfo = new XmlLoad<StokBilgileri>();
                info = loadInfo.LoadRestoran("stoklar.xml");
                stokListesi.AddRange(info);
                goster();
            }
        }
        //yeni ürün ekle butonuna basıldığında
        private void buttonAddNewStok_Click(object sender, EventArgs e)
        {
            buttonDeleteStok.Visible = false;
            buttonCancel.Visible = true;
            buttonCancel.Enabled = true;
            buttonSaveNewStok.Enabled = true;
            buttonAddNewStok.Enabled = false;
            textboxUrunAdi.Enabled = true;
            textBoxUrunMiktari.Enabled = true;
            newStokForm.Text = "Yeni Ürün";
            textboxUrunAdi.Clear();
            textBoxUrunMiktari.Clear();
            textboxUrunAdi.Focus();
        }
        //yeni ürün ekleme iptal edildiğinde
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            buttonDeleteStok.Visible = true;
            buttonCancel.Visible = false;
            buttonAddNewStok.Enabled = true;
            buttonSaveNewStok.Enabled = false;
            textboxUrunAdi.Clear();
            textBoxUrunMiktari.Clear();
            goster();

        }
        //ürünleri listview'a aktarmak için gereken fonksiyon
        private void goster()
        {
            for (int i = 0; i < stokListesi.Count; i++)
            {
                myListUrunler.Items.Add(stokListesi[i].StokAdi);
                myListUrunler.Items[i].SubItems.Add(stokListesi[i].StokMiktari);
                myListUrunler.Items[i].SubItems.Add(stokListesi[i].MiktarTipi);
            }
        }
        //ürün eklemek için gereken fonksiyon
        private void kaydet ()
        {
            buttonDeleteStok.Enabled = false;
            buttonSaveNewStok.Enabled = false;
            buttonCancel.Enabled = false;
            buttonAddNewStok.Enabled = true;
            if (textboxUrunAdi.Text == "" || textBoxUrunMiktari.Text == "")
            {
                MessageBox.Show("Eksik bilgi girdiniz. Lütfen ürün adı ve ürün miktarı girdiğinizden emin olunuz.");
                buttonDeleteStok.Visible = false;
                buttonCancel.Visible = true;
                buttonCancel.Enabled = true;
                buttonSaveNewStok.Enabled = true;
                buttonAddNewStok.Enabled = false;
                textboxUrunAdi.Enabled = true;
                textBoxUrunMiktari.Enabled = true;
                newStokForm.Text = "Yeni Ürün";
                textboxUrunAdi.Focus();
            }
            else
            {
                newStokForm.Text = textboxUrunAdi.Text;
                StokBilgileri yeniurun = new StokBilgileri();
                yeniurun.StokAdi = textboxUrunAdi.Text;
                yeniurun.StokMiktari = textBoxUrunMiktari.Text;
                yeniurun.MiktarTipi = comboBoxMiktarTipi.Text;
                myListUrunler.Items.Add(textboxUrunAdi.Text);
                myListUrunler.Items[myListUrunler.Items.Count - 1].SubItems.Add(textBoxUrunMiktari.Text);
                myListUrunler.Items[myListUrunler.Items.Count - 1].SubItems.Add(comboBoxMiktarTipi.Text);
                stokListesi.Add(yeniurun);
                XmlSave.SaveRestoran(stokListesi, "stoklar.xml");
            }
        }

        private void buttonSaveNewStok_Click(object sender, EventArgs e)
        {
            kaydet();
        }

        private void keyboardcontrol1_UserKeyPressed(object sender, KeyboardClassLibrary.KeyboardEventArgs e)
        {
            SendKeys.Send(e.KeyboardKeyPressed);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonCancel.Visible = false;
            buttonDeleteStok.Visible = true;

            //listviewden satır seçip textboxa aktarma
            if (myListUrunler.SelectedItems.Count > 0)
            {
                textboxUrunAdi.Text = myListUrunler.SelectedItems[0].SubItems[0].Text;
                textBoxUrunMiktari.Text = myListUrunler.SelectedItems[0].SubItems[1].Text;
                comboBoxMiktarTipi.Text = myListUrunler.SelectedItems[0].SubItems[2].Text;
                newStokForm.Text = textboxUrunAdi.Text;
                buttonDeleteStok.Enabled = true;
                buttonAddNewStok.Enabled = true;
            }
        }

        private void buttonDeleteStok_Click(object sender, EventArgs e)
        {
            DialogResult eminMisiniz;
            using (KontrolFormu dialog = new KontrolFormu("silmek istediğinize emin misiniz?", true))
            {
                eminMisiniz = dialog.ShowDialog();

            }


            if (eminMisiniz == DialogResult.Yes)
            {
                //listeden menüyü siliyoruz
                stokListesi.RemoveAt(myListUrunler.SelectedItems[0].Index);
                myListUrunler.SelectedItems[0].Remove();
                XmlSave.SaveRestoran(stokListesi, "stoklar.xml");
            }
            textboxUrunAdi.Clear();
            textBoxUrunMiktari.Clear();
            newStokForm.Text = "";
            }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            myListUrunler.Items.Clear();
            for (int i = 0; i < stokListesi.Count; i++)
            {
                int kac = textBox1.Text.Length;
                 if(textBox1.Text.Substring(0,kac)==stokListesi[i].StokAdi.Substring(0,kac))
                {
                    myListUrunler.Items.Add(stokListesi[i].StokAdi);
                    myListUrunler.Items[myListUrunler.Items.Count - 1].SubItems.Add(stokListesi[i].StokMiktari);
                    myListUrunler.Items[myListUrunler.Items.Count - 1].SubItems.Add(stokListesi[i].MiktarTipi);
                }
                 
            }
        }

        private void textBoxUrunMiktari_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void Stoklar_Load(object sender, EventArgs e)
        {
            this.columnUrunAdi.Width = myListUrunler.Width * 70/100;
            this.columnUrunMiktari.Width = myListUrunler.Width * 15/100;
            this.columnMiktarTipi.Width = myListUrunler.Width * 15/100;
        }

        private void textboxUrunAdi_KeyDown(object sender, KeyEventArgs e)
        {
             if (e.KeyCode == Keys.Enter)
             {
                 textBoxUrunMiktari.Focus();
                 e.Handled = true;
                 e.SuppressKeyPress = true;
             }
        }

        private void textBoxUrunMiktari_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                comboBoxMiktarTipi.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void comboBoxMiktarTipi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                kaydet();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

    }
}
