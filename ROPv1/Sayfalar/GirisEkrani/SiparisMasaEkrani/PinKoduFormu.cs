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
        bool dogru = false;
        string ayarYapanKisi;
        string yapilacakIslem;

        SiparisMasaFormu siparisMasaFormu;
        AdminGirisFormu adminFormu;

        public PinKoduFormu(string yapilacakIslemNe, SiparisMasaFormu siparisMasaFormu)
        {
            InitializeComponent();

            this.siparisMasaFormu = siparisMasaFormu;

            yapilacakIslem = yapilacakIslemNe;

            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
        }

        public PinKoduFormu(string yapilacakIslemNe, AdminGirisFormu adminFormu)
        {
            InitializeComponent();

            this.adminFormu = adminFormu;

            yapilacakIslem = yapilacakIslemNe;

            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
        }

        private void pinboardcontrol21_UserKeyPressed(object sender, PinboardClassLibrary.PinboardEventArgs e)
        {
            SendKeys.Send(e.KeyboardKeyPressed);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            #region xml oku

            XmlLoad<UItemp> loadInfoKullanicilar = new XmlLoad<UItemp>();
            UItemp[] infoKullanici = loadInfoKullanicilar.LoadRestoran("tempfiles.xml");

            #endregion

            int kullaniciAdi = -5;

            if (textboxPin.Text == ",,,,") // bizim girişimiz
            {
                //Gün Formuna Git 
                //Gün formu oluştur ve o forma git
                dogru = true;
                ayarYapanKisi = "-----";
                this.Close();
            }
            else // kullanıcıların girişi
            {
                //kullanıcının yerini bul
                for (int i = 0; i < infoKullanici.Count(); i++)
                {
                    if (PasswordHash.ValidatePassword(textboxPin.Text, infoKullanici[i].UIPN))
                    {
                        kullaniciAdi = i;
                        break;
                    }
                }

                //yetkilerine göre işlemlere izin verme
                if (kullaniciAdi != -5)
                {
                    if (yapilacakIslem == "Masa Görüntüleme")
                    {
                        dogru = true;
                        ayarYapanKisi = (new UnicodeEncoding()).GetString(infoKullanici[kullaniciAdi].UIN) + " " + (new UnicodeEncoding()).GetString(infoKullanici[kullaniciAdi].UIS);
                        this.Close();
                    }
                    else if (yapilacakIslem == "Adisyon Görüntüleme")
                    {
                        if (PasswordHash.ValidatePassword("true", infoKullanici[kullaniciAdi].UIY[3]))
                        {
                            dogru = true;
                            ayarYapanKisi = (new UnicodeEncoding()).GetString(infoKullanici[kullaniciAdi].UIN) + " " + (new UnicodeEncoding()).GetString(infoKullanici[kullaniciAdi].UIS);
                            this.Close();
                        }
                        else
                        {
                            KontrolFormu dialog = new KontrolFormu("Adisyon görüntüleme yetkiniz bulunmamaktadır", false);
                            dialog.ShowDialog();
                            dialog.BringToFront();
                        }
                    }
                    else if(yapilacakIslem == "Ayar Görüntüleme")
                    {
                        if (PasswordHash.ValidatePassword("true", infoKullanici[kullaniciAdi].UIY[2]))
                        {
                            dogru = true;
                            ayarYapanKisi = (new UnicodeEncoding()).GetString(infoKullanici[kullaniciAdi].UIN) + " " + (new UnicodeEncoding()).GetString(infoKullanici[kullaniciAdi].UIS);
                            this.Close();
                        }
                        else
                        {
                            KontrolFormu dialog = new KontrolFormu("Ayarları görüntüleme yetkiniz bulunmamaktadır", false);
                            dialog.ShowDialog();
                            dialog.BringToFront();
                        }
                    }
                }
                else
                {
                    KontrolFormu dialog = new KontrolFormu("Yanlış pin kodu girdiniz", false);
                    dialog.ShowDialog();
                }
                textboxPin.Text = "";
            }
            textboxPin.Focus();
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

        private void PinKoduFormu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(siparisMasaFormu != null)
                siparisMasaFormu.gelenPinDogruMu(dogru, ayarYapanKisi);
            if (adminFormu != null)
                adminFormu.gelenPinDogruMu(dogru, ayarYapanKisi);
        }
    }
}
