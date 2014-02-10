extern alias pinKeyboard;
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
    public partial class PinKoduFormu : Form
    {
        public PinKoduFormu()
        {
            InitializeComponent();

            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
        }

        private void keyboardcontrol2_UserKeyPressed(object sender, pinKeyboard.KeyboardClassLibrary.KeyboardEventArgs e)
        {
            SendKeys.Send(e.KeyboardKeyPressed);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            UItemp[] infoKullanici = new UItemp[1];

            #region xml oku

            XmlLoad<UItemp> loadInfoKullanicilar = new XmlLoad<UItemp>();
            infoKullanici = loadInfoKullanicilar.LoadRestoran("tempfiles.xml");

            #endregion

            int kullaniciAdi = -5;

            if (textboxPin.Text == ",,,,") // bizim girişimiz
            {
                //Gün Formuna Git 
                //Gün formu oluştur ve o forma git
                /*                
                GunFormu girisForm = new GunFormu();
                GunFormu.Show();
                */
                this.Close();
            }
            else // kullanıcıların girişi
            {                
                //kullanıcının yerini bul
                for (int i = 0; i < infoKullanici.Count(); i++)
                {
                    if (Helper.VerifyHash(textboxPin.Text, "SHA512", infoKullanici[i].UIPN))
                    {
                        kullaniciAdi = i;
                        break;
                    }
                }

                //yetkilerine göre işlemlere izin verme
                if (kullaniciAdi != -5)
                {
                    if (Helper.VerifyHash("true", "SHA512", infoKullanici[kullaniciAdi].UIY[5]))
                    {
                        //Gün Formuna Git 
                        //Gün formu oluştur ve o forma git
                        /*                
                        GunFormu girisForm = new GunFormu();
                        GunFormu.Show();
                        */
                        this.Close();
                    }
                    else
                    {
                        using (KontrolFormu dialog = new KontrolFormu("Gün işlemi açma/kapama yetkiniz bulunmamaktadır", false))
                        {
                            dialog.ShowDialog();
                        }
                    }
                }
                else
                {
                    using (KontrolFormu dialog = new KontrolFormu("Yanlış pin kodu girdiniz", false))
                    {
                        dialog.ShowDialog();
                    }
                }
            }            
        }

        private void pinPressed(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }

        private void buttonNO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
