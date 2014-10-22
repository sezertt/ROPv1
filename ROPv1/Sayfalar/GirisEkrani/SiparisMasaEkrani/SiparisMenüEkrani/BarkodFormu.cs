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
    public partial class BarkodFormu : Form
    {
        List<KategorilerineGoreUrunler> urunListesi;
        int textFromKeyboardint = 0;
        bool textFromKeyboard = false;

        public KategorilerineGoreUrunler [] barkodGirisiyleEklenecekUrunler = new KategorilerineGoreUrunler[6];

        public BarkodFormu(List<KategorilerineGoreUrunler> urunListesi)
        {
            InitializeComponent();

            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            this.urunListesi = urunListesi;
        }

        private void pinboardcontrol21_UserKeyPressed(object sender, PinboardClassLibrary.PinboardEventArgs e)
        {
            textFromKeyboardint = 0;
            textFromKeyboard = true;

            textBoxBarkod.Focus();
            SendKeys.Send(e.KeyboardKeyPressed);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            // bura yapılacak
            this.Close();
        }

        private void textBoxKG_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 22)
                e.Handled = true;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void BarkodFormu_KeyDown(object sender, KeyEventArgs e)
        {
            if (char.IsDigit((char)e.KeyCode))
            {
                textBoxBarkod.Focus();
                if (textFromKeyboard)
                {
                    textFromKeyboard = false;
                    textFromKeyboardint = 0;
                }
                else
                {
                    if (textFromKeyboardint == 0)
                        textBoxBarkod.SelectAll();
                    textFromKeyboardint++;
                }
            }
        }

        private void textBoxBarkod_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text.Length == ((TextBox)sender).MaxLength)
            {
                if (textBoxBarkod.Text.Length < textBoxBarkod.MaxLength)
                {
                    KontrolFormu dialog = new KontrolFormu("Yanlış bir değer girdiniz, barkod 13 haneden oluşmalıdır", false);
                    dialog.Show();
                    return;
                }

                string urunBarkodu = textBoxBarkod.Text.Substring(8, 5);

                int barkoduArananUrununGrubu = -1, barkoduArananUrununGruptakiYeri = -1;

                for (int i = 0; i < urunListesi.Count; i++)
                {
                    for (int j = 0; j < urunListesi[i].urunBarkodu.Count; j++)
                    {
                        if (urunListesi[i].urunBarkodu[j] != "")
                        {
                            if (string.Equals(urunBarkodu, urunListesi[i].urunBarkodu[j], StringComparison.CurrentCultureIgnoreCase))
                            {
                                barkoduArananUrununGrubu = i;
                                barkoduArananUrununGruptakiYeri = j;
                                break;
                            }
                        }
                    }
                }

                if (barkoduArananUrununGrubu == -1)
                {
                    KontrolFormu dialog = new KontrolFormu("Yanlış bir değer girdiniz, barkod bulunamadı", false);
                    dialog.Show();
                    return;
                }
                else
                {
                    double porsiyonVeyaKilo = 0;

                    bool kiloMuAdetMi = Convert.ToBoolean(Convert.ToInt32(textBoxBarkod.Text.Substring(2, 1)));

                    if (kiloMuAdetMi)
                    {
                        porsiyonVeyaKilo = Convert.ToDouble(textBoxBarkod.Text.Substring(3, 2)) + (Convert.ToDouble(textBoxBarkod.Text.Substring(5, 3)) / 1000);
                    }
                    else
                    {
                        porsiyonVeyaKilo = Convert.ToInt32(textBoxBarkod.Text.Substring(3, 5));
                    }

                    if (porsiyonVeyaKilo == 0)
                        return;

                    int urununEklenecegiSira = 0;

                    string urunAdi = urunListesi[barkoduArananUrununGrubu].urunAdi[barkoduArananUrununGruptakiYeri],
                        urunPorsiyonFiyati = urunListesi[barkoduArananUrununGrubu].urunPorsiyonFiyati[barkoduArananUrununGruptakiYeri],
                        urunKiloFiyati = urunListesi[barkoduArananUrununGrubu].urunKiloFiyati[barkoduArananUrununGruptakiYeri];


                    string yazilacakFiyat;
                    string tur = "P";

                    if (kiloMuAdetMi)
                    {
                        yazilacakFiyat = urunKiloFiyati;
                        tur = "K";
                    }
                    else
                        yazilacakFiyat = urunPorsiyonFiyati;


                    if (!labelUrun1Ad.Visible)
                    {
                        buttonUrun1Cancel.Visible = true;
                        labelUrun1Ad.Text = urunAdi;
                        labelUrun1KiloAdet.Text = porsiyonVeyaKilo + tur;
                        labelUrun1Fiyat.Text = yazilacakFiyat;

                        labelUrun1Ad.Visible = true;
                        labelUrun1KiloAdet.Visible = true;
                        labelUrun1Fiyat.Visible = true;
                    }
                    else if (!labelUrun2Ad.Visible)
                    {
                        urununEklenecegiSira = 1;
                        buttonUrun2Cancel.Visible = true;
                        labelUrun2Ad.Text = urunAdi;
                        labelUrun2KiloAdet.Text = porsiyonVeyaKilo + tur;
                        labelUrun2Fiyat.Text = yazilacakFiyat;

                        labelUrun2Ad.Visible = true;
                        labelUrun2KiloAdet.Visible = true;
                        labelUrun2Fiyat.Visible = true;
                    }
                    else if (!labelUrun3Ad.Visible)
                    {
                        urununEklenecegiSira = 2;
                        buttonUrun3Cancel.Visible = true;
                        labelUrun3Ad.Text = urunAdi;
                        labelUrun3KiloAdet.Text = porsiyonVeyaKilo + tur;
                        labelUrun3Fiyat.Text = yazilacakFiyat;

                        labelUrun3Ad.Visible = true;
                        labelUrun3KiloAdet.Visible = true;
                        labelUrun3Fiyat.Visible = true;
                    }
                    else if (!labelUrun4Ad.Visible)
                    {
                        urununEklenecegiSira = 3;
                        buttonUrun4Cancel.Visible = true;
                        labelUrun4Ad.Text = urunAdi;
                        labelUrun4KiloAdet.Text = porsiyonVeyaKilo + tur;
                        labelUrun4Fiyat.Text = yazilacakFiyat;

                        labelUrun4Ad.Visible = true;
                        labelUrun4KiloAdet.Visible = true;
                        labelUrun4Fiyat.Visible = true;
                    }
                    else if (!labelUrun5Ad.Visible)
                    {
                        urununEklenecegiSira = 4;
                        buttonUrun5Cancel.Visible = true;
                        labelUrun5Ad.Text = urunAdi;
                        labelUrun5KiloAdet.Text = porsiyonVeyaKilo + tur;
                        labelUrun5Fiyat.Text = yazilacakFiyat;

                        labelUrun5Ad.Visible = true;
                        labelUrun5KiloAdet.Visible = true;
                        labelUrun5Fiyat.Visible = true;
                    }
                    else if (!labelUrun6Ad.Visible)
                    {
                        urununEklenecegiSira = 5;
                        buttonUrun6Cancel.Visible = true;
                        labelUrun6Ad.Text = urunAdi;
                        labelUrun6KiloAdet.Text = porsiyonVeyaKilo + tur;
                        labelUrun6Fiyat.Text = yazilacakFiyat;

                        labelUrun6Ad.Visible = true;
                        labelUrun6KiloAdet.Visible = true;
                        labelUrun6Fiyat.Visible = true;
                    }
                    else
                    {
                        KontrolFormu dialog = new KontrolFormu("Bir kerede en fazla 6 ürün girişi yapılabilir", false);
                        dialog.Show();
                        return;
                    }

                    barkodGirisiyleEklenecekUrunler[urununEklenecegiSira] = new KategorilerineGoreUrunler();
                    barkodGirisiyleEklenecekUrunler[urununEklenecegiSira].urunAdi.Add(urunAdi);
                    barkodGirisiyleEklenecekUrunler[urununEklenecegiSira].urunPorsiyonSinifi.Add(urunListesi[barkoduArananUrununGrubu].urunPorsiyonSinifi[barkoduArananUrununGruptakiYeri]);
                    barkodGirisiyleEklenecekUrunler[urununEklenecegiSira].urunPorsiyonFiyati.Add(urunPorsiyonFiyati);
                    barkodGirisiyleEklenecekUrunler[urununEklenecegiSira].urunKiloFiyati.Add(urunKiloFiyati);
                    barkodGirisiyleEklenecekUrunler[urununEklenecegiSira].urunTuru.Add(urunListesi[barkoduArananUrununGrubu].urunTuru[barkoduArananUrununGruptakiYeri]);
                    barkodGirisiyleEklenecekUrunler[urununEklenecegiSira].urunYaziciyaBildirilmeliMi.Add(urunListesi[barkoduArananUrununGrubu].urunYaziciyaBildirilmeliMi[barkoduArananUrununGruptakiYeri]);
                    barkodGirisiyleEklenecekUrunler[urununEklenecegiSira].urunKategorisi.Add(porsiyonVeyaKilo.ToString());// kategori yerine adedini veya kilosunu yazıyoruz
                    
                    if (kiloMuAdetMi)
                        barkodGirisiyleEklenecekUrunler[urununEklenecegiSira].urunBarkodu.Add("1"); // barkod yerine ürünün kilo cinsinden girildiğini yazıyoruz
                    else
                        barkodGirisiyleEklenecekUrunler[urununEklenecegiSira].urunBarkodu.Add("0");// barkod yerine ürünün porsiyon cinsinden girildiğini yazıyoruz

                    textBoxBarkod.Clear();
                }
            }
        }

        private void buttonUrun1Cancel_Click(object sender, EventArgs e) 
        {
            barkodGirisiyleEklenecekUrunler[0].urunAdi.RemoveAt(0);
            barkodGirisiyleEklenecekUrunler[0].urunPorsiyonSinifi.RemoveAt(0);
            barkodGirisiyleEklenecekUrunler[0].urunPorsiyonFiyati.RemoveAt(0);
            barkodGirisiyleEklenecekUrunler[0].urunKiloFiyati.RemoveAt(0);
            barkodGirisiyleEklenecekUrunler[0].urunTuru.RemoveAt(0);
            barkodGirisiyleEklenecekUrunler[0].urunYaziciyaBildirilmeliMi.RemoveAt(0);

            buttonUrun1Cancel.Visible = false;
            labelUrun1Ad.Visible = false;
            labelUrun1KiloAdet.Visible = false;
            labelUrun1Fiyat.Visible = false;
        }

        private void buttonUrun2Cancel_Click(object sender, EventArgs e)
        {
            barkodGirisiyleEklenecekUrunler[1].urunAdi.RemoveAt(0);
            barkodGirisiyleEklenecekUrunler[1].urunPorsiyonSinifi.RemoveAt(0);
            barkodGirisiyleEklenecekUrunler[1].urunPorsiyonFiyati.RemoveAt(0);
            barkodGirisiyleEklenecekUrunler[1].urunKiloFiyati.RemoveAt(0);
            barkodGirisiyleEklenecekUrunler[1].urunTuru.RemoveAt(0);
            barkodGirisiyleEklenecekUrunler[1].urunYaziciyaBildirilmeliMi.RemoveAt(0); buttonUrun2Cancel.Visible = false;
            labelUrun2Ad.Visible = false;
            labelUrun2KiloAdet.Visible = false;
            labelUrun2Fiyat.Visible = false;
        }

        private void buttonUrun3Cancel_Click(object sender, EventArgs e)
        {
            barkodGirisiyleEklenecekUrunler[2].urunAdi.RemoveAt(0);
            barkodGirisiyleEklenecekUrunler[2].urunPorsiyonSinifi.RemoveAt(0);
            barkodGirisiyleEklenecekUrunler[2].urunPorsiyonFiyati.RemoveAt(0);
            barkodGirisiyleEklenecekUrunler[2].urunKiloFiyati.RemoveAt(0);
            barkodGirisiyleEklenecekUrunler[2].urunTuru.RemoveAt(0);
            barkodGirisiyleEklenecekUrunler[2].urunYaziciyaBildirilmeliMi.RemoveAt(0); buttonUrun3Cancel.Visible = false;
            labelUrun3Ad.Visible = false;
            labelUrun3KiloAdet.Visible = false;
            labelUrun3Fiyat.Visible = false;
        }

        private void buttonUrun4Cancel_Click(object sender, EventArgs e)
        {
            barkodGirisiyleEklenecekUrunler[3].urunAdi.RemoveAt(0);
            barkodGirisiyleEklenecekUrunler[3].urunPorsiyonSinifi.RemoveAt(0);
            barkodGirisiyleEklenecekUrunler[3].urunPorsiyonFiyati.RemoveAt(0);
            barkodGirisiyleEklenecekUrunler[3].urunKiloFiyati.RemoveAt(0);
            barkodGirisiyleEklenecekUrunler[3].urunTuru.RemoveAt(0);
            barkodGirisiyleEklenecekUrunler[3].urunYaziciyaBildirilmeliMi.RemoveAt(0); buttonUrun4Cancel.Visible = false;
            labelUrun4Ad.Visible = false;
            labelUrun4KiloAdet.Visible = false;
            labelUrun4Fiyat.Visible = false;
        }

        private void buttonUrun5Cancel_Click(object sender, EventArgs e)
        {
            barkodGirisiyleEklenecekUrunler[4].urunAdi.RemoveAt(0);
            barkodGirisiyleEklenecekUrunler[4].urunPorsiyonSinifi.RemoveAt(0);
            barkodGirisiyleEklenecekUrunler[4].urunPorsiyonFiyati.RemoveAt(0);
            barkodGirisiyleEklenecekUrunler[4].urunKiloFiyati.RemoveAt(0);
            barkodGirisiyleEklenecekUrunler[4].urunTuru.RemoveAt(0);
            barkodGirisiyleEklenecekUrunler[4].urunYaziciyaBildirilmeliMi.RemoveAt(0); buttonUrun5Cancel.Visible = false;
            labelUrun5Ad.Visible = false;
            labelUrun5KiloAdet.Visible = false;
            labelUrun5Fiyat.Visible = false;
        }

        private void buttonUrun6Cancel_Click(object sender, EventArgs e)
        {
            barkodGirisiyleEklenecekUrunler[5].urunAdi.RemoveAt(0);
            barkodGirisiyleEklenecekUrunler[5].urunPorsiyonSinifi.RemoveAt(0);
            barkodGirisiyleEklenecekUrunler[5].urunPorsiyonFiyati.RemoveAt(0);
            barkodGirisiyleEklenecekUrunler[5].urunKiloFiyati.RemoveAt(0);
            barkodGirisiyleEklenecekUrunler[5].urunTuru.RemoveAt(0);
            barkodGirisiyleEklenecekUrunler[5].urunYaziciyaBildirilmeliMi.RemoveAt(0); buttonUrun6Cancel.Visible = false;
            labelUrun6Ad.Visible = false;
            labelUrun6KiloAdet.Visible = false;
            labelUrun6Fiyat.Visible = false;
        }
    }
}
