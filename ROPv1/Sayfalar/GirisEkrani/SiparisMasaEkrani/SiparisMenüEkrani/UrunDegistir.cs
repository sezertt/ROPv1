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
    public partial class UrunDegistir : Form
    {
        ListView.SelectedListViewItemCollection urunler;
        public List<int> miktarlar = new List<int>();

        public UrunDegistir(ListView.SelectedListViewItemCollection selectedListViewItems)
        {
            InitializeComponent();

            urunler = selectedListViewItems;

            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
        }

        private void pinboardcontrol21_UserKeyPressed(object sender, PinboardClassLibrary.PinboardEventArgs e)
        {
            SendKeys.Send(e.KeyboardKeyPressed);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (textBoxUrun1.Text != "")
                miktarlar.Add(Convert.ToInt32(textBoxUrun1.Text));
            else
            {
                miktarlar.Add(Convert.ToInt32(0));
                return;
            }
            switch (urunler.Count)
            {
                case 2:
                    if (textBoxUrun2.Text != "")
                        miktarlar.Add(Convert.ToInt32(textBoxUrun2.Text));
                    else
                    {
                        miktarlar.Add(Convert.ToInt32(0));
                        return;
                    }
                    break;
                case 3:
                    if (textBoxUrun2.Text != "")
                    {
                        miktarlar.Add(Convert.ToInt32(textBoxUrun2.Text));
                    }
                    else
                    {
                        miktarlar.Add(Convert.ToInt32(0));
                        return;
                    }
                    if (textBoxUrun3.Text != "")
                    {
                        miktarlar.Add(Convert.ToInt32(textBoxUrun3.Text));
                    }
                    else
                    {
                        miktarlar.Add(Convert.ToInt32(0));
                        return;
                    }
                    break;
                case 4:
                    if (textBoxUrun2.Text != "")
                    {
                        miktarlar.Add(Convert.ToInt32(textBoxUrun2.Text));
                    }
                    else
                    {
                        miktarlar.Add(Convert.ToInt32(0));
                        return;
                    }
                    if (textBoxUrun3.Text != "")
                    {
                        miktarlar.Add(Convert.ToInt32(textBoxUrun3.Text));
                    }
                    else
                    {
                        miktarlar.Add(Convert.ToInt32(0));
                        return;
                    }
                    if (textBoxUrun4.Text != "")
                    {
                        miktarlar.Add(Convert.ToInt32(textBoxUrun4.Text));
                    }
                    else
                    {
                        miktarlar.Add(Convert.ToInt32(0));
                        return;
                    }
                    break;
                case 5:
                    if (textBoxUrun2.Text != "")
                    {
                        miktarlar.Add(Convert.ToInt32(textBoxUrun2.Text));
                    }
                    else
                    {
                        miktarlar.Add(Convert.ToInt32(0));
                        return;
                    }
                    if (textBoxUrun3.Text != "")
                    {
                        miktarlar.Add(Convert.ToInt32(textBoxUrun3.Text));
                    }
                    else
                    {
                        miktarlar.Add(Convert.ToInt32(0));
                        return;
                    }
                    if (textBoxUrun4.Text != "")
                    {
                        miktarlar.Add(Convert.ToInt32(textBoxUrun4.Text));
                    }
                    else
                    {
                        miktarlar.Add(Convert.ToInt32(0));
                        return;
                    }
                    if (textBoxUrun5.Text != "")
                    {
                        miktarlar.Add(Convert.ToInt32(textBoxUrun5.Text));
                    }
                    else
                    {
                        miktarlar.Add(Convert.ToInt32(0));
                        return;
                    }
                    break;
                default:
                    break;
            }
            this.Close();
        }

        private void buttonNO_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UrunDegistir_Load(object sender, EventArgs e)
        {
            foreach (Control urun in this.Controls)
            {
                if (urun is Label)
                {
                    switch (Convert.ToInt32(urun.Tag))
                    {
                        case 1:
                            urun.Text = urunler[0].SubItems[2].Text;
                            textBoxUrun1.Text = urunler[0].SubItems[0].Text;
                            textBoxUrun1.Tag = Convert.ToDecimal(textBoxUrun1.Text);
                            if (urunler[0].Group.Tag.ToString() != "2")
                                labelUrun1.Text = "(ikram)" + labelUrun1.Text;
                            break;
                        case 2:
                            if (urunler.Count > 1)
                            {
                                urun.Text = urunler[1].SubItems[2].Text;
                                textBoxUrun2.Text = urunler[1].SubItems[0].Text;
                                textBoxUrun2.Tag = Convert.ToDecimal(textBoxUrun2.Text);
                                if (urunler[1].Group.Tag.ToString() != "2")
                                    labelUrun2.Text = "(ikram)" + labelUrun2.Text;
                            }
                            else
                            {
                                urun.Enabled = false;
                                textBoxUrun2.Enabled = false;
                            }
                            break;
                        case 3:
                            if (urunler.Count > 2)
                            {
                                urun.Text = urunler[2].SubItems[2].Text;
                                textBoxUrun3.Text = urunler[2].SubItems[0].Text;
                                textBoxUrun3.Tag = Convert.ToDecimal(textBoxUrun3.Text);
                                if (urunler[2].Group.Tag.ToString() != "2")
                                    labelUrun3.Text = "(ikram)" + labelUrun3.Text;
                            }
                            else
                            {
                                urun.Enabled = false;
                                textBoxUrun3.Enabled = false;
                            }
                            break;
                        case 4:
                            if (urunler.Count > 3)
                            {
                                urun.Text = urunler[3].SubItems[2].Text;
                                textBoxUrun4.Text = urunler[3].SubItems[0].Text;
                                textBoxUrun4.Tag = Convert.ToDecimal(textBoxUrun4.Text);
                                if (urunler[3].Group.Tag.ToString() != "2")
                                    labelUrun4.Text = "(ikram)" + labelUrun4.Text;
                            }
                            else
                            {
                                urun.Enabled = false;
                                textBoxUrun4.Enabled = false;
                            }
                            break;
                        case 5:
                            if (urunler.Count > 4)
                            {
                                urun.Text = urunler[4].SubItems[2].Text;
                                textBoxUrun5.Text = urunler[4].SubItems[0].Text;
                                textBoxUrun5.Tag = Convert.ToDecimal(textBoxUrun5.Text);
                                if (urunler[4].Group.Tag.ToString() != "2")
                                    labelUrun5.Text = "(ikram)" + labelUrun5.Text;
                            }
                            else
                            {
                                urun.Enabled = false;
                                textBoxUrun5.Enabled = false;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void textBoxUrun5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((sender as TextBox).Text == String.Empty)
            {
                if (e.KeyChar == '0')
                    e.Handled = true;
            }

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textNumberOfItem_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text == "")
                return;

            if (Convert.ToDouble((sender as TextBox).Text) > Convert.ToDouble((sender as TextBox).Tag) && (sender as TextBox).Tag != null)
            {
                (sender as TextBox).Text = (sender as TextBox).Tag.ToString();
            }
            else if (Convert.ToDouble((sender as TextBox).Text) < 0)
                (sender as TextBox).Text = "0";
        }

        private void textBoxUrun1_Click(object sender, EventArgs e)
        {
            (sender as TextBox).Select(0, (sender as TextBox).TextLength);
        }
    }
}
