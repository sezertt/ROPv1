﻿using System;
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

        SiparisMasaFormu gelenForm;

        public PinKoduFormu(string yapilacakIslemNe, SiparisMasaFormu gelenForm)
        {
            InitializeComponent();

            this.gelenForm = gelenForm;

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
                    if (Helper.VerifyHash(textboxPin.Text, "SHA512", infoKullanici[i].UIPN))
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
                        if (Helper.VerifyHash("true", "SHA512", infoKullanici[kullaniciAdi].UIY[3]))
                        {
                            dogru = true;
                            ayarYapanKisi = (new UnicodeEncoding()).GetString(infoKullanici[kullaniciAdi].UIN) + " " + (new UnicodeEncoding()).GetString(infoKullanici[kullaniciAdi].UIS);
                            this.Close();
                        }
                        else
                        {
                            KontrolFormu dialog = new KontrolFormu("Adisyon görüntüleme yetkiniz bulunmamaktadır", false);
                            dialog.Show();
                        }
                    }
                }
                else
                {
                    KontrolFormu dialog = new KontrolFormu("Yanlış pin kodu girdiniz", false);
                    dialog.Show();
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
            gelenForm.gelenPinDogruMu(dogru, ayarYapanKisi, yapilacakIslem);
        }
    }
}
