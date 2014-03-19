using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ROPv1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Properties.Settings.Default.Server == 2) // bu makina server
            {
                //Burada serverı aç
                Application.Run(new GirisEkrani());
            }
            else //client
            {
                if (!File.Exists("tempfiles.xml") || !File.Exists("kategoriler.xml") || !File.Exists("masaDizayn.xml") || !File.Exists("menu.xml") || !File.Exists("stoklar.xml") || !File.Exists("urunler.xml") || !File.Exists("gunler.xml") || !File.Exists("restoran.xml"))
                {
                    using (KontrolFormu dialog = new KontrolFormu("Bilgiler girilmemiş veri aktarımını başlatmak ister misiniz?", true))
                    {
                        DialogResult cevap = dialog.ShowDialog();
                        bool basarili = true;

                        if (cevap == DialogResult.Yes)
                        {
                            XMLAktarClient aktarimServeri = new XMLAktarClient();
                            for (int i = 0; i < 8; i++)
                            {
                                basarili = aktarimServeri.ClientTarafi();
                                if (!basarili)
                                    break;
                            }

                            if (basarili)
                            {
                                if (!File.Exists("tempfiles.xml") || !File.Exists("kategoriler.xml") || !File.Exists("masaDizayn.xml") || !File.Exists("menu.xml") || !File.Exists("stoklar.xml") || !File.Exists("urunler.xml") || !File.Exists("gunler.xml") || !File.Exists("restoran.xml"))
                                {
                                    using (KontrolFormu dialog2 = new KontrolFormu("Dosyalarda eksik var, lütfen serverdaki dosyaları kontorl ediniz", false))
                                    {
                                        dialog.ShowDialog();
                                        Application.Exit();
                                    }
                                }
                                using (KontrolFormu dialog2 = new KontrolFormu("Dosya alımı başarılı, lütfen yeniden giriş yapınız", false))
                                {
                                    dialog.ShowDialog();
                                    Application.Exit();
                                }
                            }
                            else
                            {
                                using (KontrolFormu dialog2 = new KontrolFormu("Dosya alımı başarısız, lütfen tekrar deneyiniz", false))
                                {
                                    dialog.ShowDialog();
                                    Application.Exit();
                                }
                            }
                        }
                        else
                        {
                            Application.Exit();
                        }
                    }
                }
                else
                {
                    // Burada servera bağla 




                    if (Properties.Settings.Default.Server == 1) // bu makina mutfak
                    {
                        // Application.Run(new MutfakFormu());

                        //BURAYI DÜZELT MUTFAK EKRANI GELİNCE DEĞİŞTİR ÜSTTEKİYLE
                        Application.Run(new SiparisMasaFormu());
                    }
                    else// garson girişi
                    {
                        Application.Run(new SiparisMasaFormu());
                    }
                }
            }
        }
    }
}