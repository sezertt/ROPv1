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
        }

        internal static class NativeMethods
        {
            //capslocku kapatmak için gerekli işlemleri yapıp kapatıyoruz
            [DllImport("user32.dll")]
            internal static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
        }

        static void ToggleCapsLock(bool onOrOff)
        {
            if (IsKeyLocked(Keys.CapsLock) == onOrOff)
                return;
            NativeMethods.keybd_event(0x14, 0x45, 0x1, (UIntPtr)0);
            NativeMethods.keybd_event(0x14, 0x45, 0x1 | 0x2, (UIntPtr)0);
        }

        private void keyboardcontrol1_UserKeyPressed(object sender, KeyboardClassLibrary.KeyboardEventArgs e)
        {
            SendKeys.Send(e.KeyboardKeyPressed);
        }

        private void buttonDeleteStok_Click(object sender, EventArgs e)
        {
            DialogResult eminMisiniz;

            using (KontrolFormu dialog = new KontrolFormu(myListUrunler.SelectedItems[0].SubItems[0].Text + " adlı stoğu silmek istediğinize emin misiniz?", true))
            {
                eminMisiniz = dialog.ShowDialog();
            }

            if (eminMisiniz == DialogResult.Yes)
            {
                //listeden menüyü siliyoruz
                stokListesi.RemoveAt(myListUrunler.SelectedItems[0].Index);
                XmlSave.SaveRestoran(stokListesi, "stoklar.xml");

                int selectedPlace = myListUrunler.SelectedIndices[0];

                myListUrunler.SelectedItems[0].Remove();

                if (myListUrunler.Items.Count > 0)
                {
                    if (selectedPlace > myListUrunler.Items.Count - 1)
                        selectedPlace = myListUrunler.Items.Count - 1;

                    myListUrunler.Items[selectedPlace].Selected = true;
                    textboxUrunAdi.Text = myListUrunler.SelectedItems[0].SubItems[0].Text;
                    textBoxUrunMiktari.Text = myListUrunler.SelectedItems[0].SubItems[1].Text;
                    comboBoxMiktarTipi.Text = myListUrunler.SelectedItems[0].SubItems[2].Text;
                    newStokForm.Text = textboxUrunAdi.Text;
                    myListUrunler.Focus();
                }
                else
                {
                    newStokForm.Enabled = false;
                }
            }
        }

        //yeni ürün ekle butonuna basıldığında
        private void buttonAddNewStok_Click(object sender, EventArgs e)
        {
            if (newStokForm.Text != "Yeni Ürün")
            {
                newStokForm.Text = "Yeni Ürün";
                textboxUrunAdi.Clear();
                textBoxUrunMiktari.Clear();

                buttonDeleteStok.Visible = false;
                buttonCancel.Visible = true;
            }
            if (!newStokForm.Enabled)
            {
                newStokForm.Enabled = true;
                buttonDeleteStok.Visible = false;
                buttonCancel.Visible = true;
            }

            textboxUrunAdi.Focus();
        }

        //yeni ürün ekleme iptal edildiğinde
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            buttonDeleteStok.Visible = true;
            buttonCancel.Visible = false;

            if (myListUrunler.Items.Count > 0)
            {
                textboxUrunAdi.Text = myListUrunler.SelectedItems[0].SubItems[0].Text;
                textBoxUrunMiktari.Text = myListUrunler.SelectedItems[0].SubItems[1].Text;
                comboBoxMiktarTipi.Text = myListUrunler.SelectedItems[0].SubItems[2].Text;
                newStokForm.Text = textboxUrunAdi.Text;
                myListUrunler.Focus();
            }
            else
            {
                newStokForm.Enabled = false;
            }
        }

        //ürün eklemek için veya güncellemek için gereken fonksiyon
        private void buttonSaveNewStok_Click(object sender, EventArgs e)
        {

            if (textboxUrunAdi.Text == "Yeni Ürün" || textboxUrunAdi.Text == "" || textBoxUrunMiktari.Text == "" || comboBoxMiktarTipi.Text == "")
            {
                using (KontrolFormu dialog = new KontrolFormu("Eksik veya hatalı bilgi girdiniz, lütfen kontrol ediniz", false))
                {
                    dialog.ShowDialog();
                }
                textboxUrunAdi.Focus();
                return;
            }


            //Yeni ürün kaydet tuşu. ekle tuşuna basıp bilgileri girdikten sonra kaydete basıyoruz
            if (newStokForm.Text == "Yeni Ürün")
            {
                bool varmi = true;
                int bulunanindis = 0;
                for (int i = 0; i < stokListesi.Count; i++)
                {
                    if (stokListesi[i].StokAdi == textboxUrunAdi.Text)
                    {
                        varmi = false;
                        bulunanindis = i;
                        break;
                    }
                }
                if (varmi == false)
                {
                    DialogResult eminMisiniz;

                    using (KontrolFormu dialog = new KontrolFormu("Eklemek istediğiniz ürün stoklarda bulunmaktadır. Ürün miktarını varolan stoğa eklemek ister misiniz?", true))
                    {
                        eminMisiniz = dialog.ShowDialog();
                    }

                    if (eminMisiniz == DialogResult.Yes)
                    {
                        stokListesi[bulunanindis].StokMiktari = (Convert.ToInt32(stokListesi[bulunanindis].StokMiktari) + Convert.ToInt32(textBoxUrunMiktari.Text)).ToString();
                        myListUrunler.Items[bulunanindis].SubItems[1].Text = stokListesi[bulunanindis].StokMiktari;
                        XmlSave.SaveRestoran(stokListesi, "stoklar.xml");
                    }
                    else
                    {
                        textboxUrunAdi.Focus();
                        return;
                    }
                }

                newStokForm.Text = textboxUrunAdi.Text;

                StokBilgileri yeniurun = new StokBilgileri();

                yeniurun.StokAdi = textboxUrunAdi.Text;
                yeniurun.StokMiktari = textBoxUrunMiktari.Text;
                yeniurun.MiktarTipi = comboBoxMiktarTipi.Text;

                stokListesi.Add(yeniurun);

                XmlSave.SaveRestoran(stokListesi, "stoklar.xml");


                myListUrunler.Items.Add(yeniurun.StokAdi);
                myListUrunler.Items[myListUrunler.Items.Count - 1].SubItems.Add(yeniurun.StokMiktari);
                myListUrunler.Items[myListUrunler.Items.Count - 1].SubItems.Add(yeniurun.MiktarTipi);

                myListUrunler.Items[myListUrunler.Items.Count - 1].Selected = true;

                if (myListUrunler.Items.Count > 0)
                    newStokForm.Enabled = true;

                buttonDeleteStok.Visible = true;
                buttonCancel.Visible = false;

                using (KontrolFormu dialog = new KontrolFormu("Yeni Ürün Bilgileri Kaydedilmiştir", false))
                {
                    dialog.ShowDialog();
                }
            }
            else //üründe değişiklik yapıldıktan sonra basılan kaydet butonu.
            {
                if(sender != null)
                {
                    bool varmi = true;
                    int bulunanindis = 0;
                    for (int i = 0; i < stokListesi.Count; i++)
                    {
                        if (stokListesi[i].StokAdi == textboxUrunAdi.Text)
                        {
                            varmi = false;
                            bulunanindis = i;
                            break;
                        }
                    }
                    if (varmi == false)
                    {
                        DialogResult eminMisiniz;

                        using (KontrolFormu dialog = new KontrolFormu("Eklemek istediğiniz ürün stoklarda bulunmaktadır. Ürün miktarını varolan stoğa eklemek ister misiniz?", true))
                        {
                            eminMisiniz = dialog.ShowDialog();
                        }

                        if (eminMisiniz == DialogResult.Yes)
                        {
                            stokListesi[bulunanindis].StokMiktari = (Convert.ToInt32(stokListesi[bulunanindis].StokMiktari) + Convert.ToInt32(textBoxUrunMiktari.Text)).ToString();
                            myListUrunler.Items[bulunanindis].SubItems[1].Text = stokListesi[bulunanindis].StokMiktari;
                            XmlSave.SaveRestoran(stokListesi, "stoklar.xml");
                        }
                        else
                        {
                            textboxUrunAdi.Focus();
                            return;
                        }
                    }
                }
                
                stokListesi[myListUrunler.SelectedIndices[0]].StokAdi = textboxUrunAdi.Text;
                stokListesi[myListUrunler.SelectedIndices[0]].StokMiktari = textBoxUrunMiktari.Text;
                stokListesi[myListUrunler.SelectedIndices[0]].MiktarTipi = comboBoxMiktarTipi.Text;

                XmlSave.SaveRestoran(stokListesi, "stoklar.xml");

                myListUrunler.Items[myListUrunler.SelectedIndices[0]].Text = textboxUrunAdi.Text;
                myListUrunler.Items[myListUrunler.SelectedIndices[0]].SubItems[1].Text = textBoxUrunMiktari.Text;
                myListUrunler.Items[myListUrunler.SelectedIndices[0]].SubItems[2].Text = comboBoxMiktarTipi.Text;
                newStokForm.Text = textboxUrunAdi.Text;

                using (KontrolFormu dialog = new KontrolFormu("Ürün Bilgileri Güncellenmiştir", false))
                {
                    dialog.ShowDialog();
                }
            }

        }

        //arama methodu
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            myListUrunler.Items.Clear();
            for (int i = 0; i < stokListesi.Count; i++)
            {
                int kac = textBox1.Text.Length;

                if (stokListesi[i].StokAdi.Length < kac)
                    continue;

                if (textBox1.Text.Substring(0, kac) == stokListesi[i].StokAdi.Substring(0, kac))
                {
                    myListUrunler.Items.Add(stokListesi[i].StokAdi);
                    myListUrunler.Items[myListUrunler.Items.Count - 1].SubItems.Add(stokListesi[i].StokMiktari);
                    myListUrunler.Items[myListUrunler.Items.Count - 1].SubItems.Add(stokListesi[i].MiktarTipi);
                }

            }
        }

        //ürünün miktarı girilirken sadece sayı ve 1 , girilebilsin
        private void textBoxUrunMiktari_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == ',' && (sender as TextBox).Text.IndexOf(',') > -1)
            {
                e.Handled = true;
            }
        }

        //form load event
        private void Stoklar_Load(object sender, EventArgs e)
        {
            //açılışta capslock açıksa kapatıyoruz.
            ToggleCapsLock(false);

            StokBilgileri[] info = new StokBilgileri[1];

            if (File.Exists("stoklar.xml"))
            {

                XmlLoad<StokBilgileri> loadInfo = new XmlLoad<StokBilgileri>();
                info = loadInfo.LoadRestoran("stoklar.xml");
                stokListesi.AddRange(info);
                goster();
            }

            //varsa ilk ürün seçilip bilgileri girilsin
            if (myListUrunler.Items.Count > 0)
            {
                myListUrunler.Items[0].Selected = true;
                myListUrunler.Focus();
                textboxUrunAdi.Text = myListUrunler.SelectedItems[0].SubItems[0].Text;
                textBoxUrunMiktari.Text = myListUrunler.SelectedItems[0].SubItems[1].Text;
                comboBoxMiktarTipi.Text = myListUrunler.SelectedItems[0].SubItems[2].Text;
                newStokForm.Text = textboxUrunAdi.Text;
            }
            else
            {
                newStokForm.Enabled = false;
            }
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
            if (e.KeyCode == Keys.Enter)
            {
                comboBoxMiktarTipi.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        //listviewda row seçildiğinde yapılacaklar
        private void myListUrunler_MouseUp(object sender, MouseEventArgs e)
        {
            //yeni bir stoğun yaratılmadığını silme tuşunun görünür olmasından anlayabiliriz, eğer yaratılıyor olsaydı iptal tuşu görünür olurdu
            if (buttonDeleteStok.Visible)
            {
                textboxUrunAdi.Text = myListUrunler.SelectedItems[0].SubItems[0].Text;
                textBoxUrunMiktari.Text = myListUrunler.SelectedItems[0].SubItems[1].Text;
                comboBoxMiktarTipi.Text = myListUrunler.SelectedItems[0].SubItems[2].Text;
                newStokForm.Text = textboxUrunAdi.Text;
            }
        }

        //listview column width ayarlaması
        private void myListUrunler_SizeChanged(object sender, EventArgs e)
        {
            myListUrunler.Columns[0].Width = myListUrunler.Width / 10 * 6 - 1;
            myListUrunler.Columns[1].Width = myListUrunler.Width / 10 * 2 - 1;
            myListUrunler.Columns[2].Width = myListUrunler.Width / 10 * 2 - 1;
        }

        //comboboxa girdi yapılamasın
        private void comboBoxKeyPressed(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        //comboboxa basıldığında içeriği açılsın
        private void showMenu(object sender, EventArgs e)
        {
            ((ComboBox)sender).DroppedDown = true;
        }

        //miktar 00023 gibi girilemesin
        private void textBoxUrunMiktari_Leave(object sender, EventArgs e)
        {
            double fiyat = Convert.ToDouble(((TextBox)sender).Text);
            ((TextBox)sender).Text = fiyat.ToString();
        }
        private void btnStogaEkle_Click(object sender, EventArgs e)
        {
            if (txtStogaEkle.Text == String.Empty)
            {
                using (KontrolFormu dialog = new KontrolFormu("Lütfen eklenecek stok miktarını giriniz.", false))
                {
                    dialog.ShowDialog();
                }
                textboxUrunAdi.Focus();
                return;
            }
            else
            {
                int c = Convert.ToInt32(textBoxUrunMiktari.Text) + Convert.ToInt32(txtStogaEkle.Text);
                textBoxUrunMiktari.Text = c.ToString();
                buttonSaveNewStok_Click(null, null);
                txtStogaEkle.Text = "";
            }
        }

        //miktar 00023 gibi girilemesin
        private void txtStogaEkle_Leave(object sender, EventArgs e)
        {
            double fiyat = Convert.ToDouble(((TextBox)sender).Text);
            ((TextBox)sender).Text = fiyat.ToString();
        }

        //ürünün miktarı girilirken sadece sayı ve 1 , girilebilsin
        private void txtStogaEkle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == ',' && (sender as TextBox).Text.IndexOf(',') > -1)
            {
                e.Handled = true;
            }
        }

        private void myListUrunler_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (myListUrunler.SelectedItems.Count < 1)
                buttonSaveNewStok.Enabled = false;
            else
                buttonSaveNewStok.Enabled = true;
        }
    }
}
