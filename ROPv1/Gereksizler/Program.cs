using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Principal;
using System.Diagnostics;

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

            if (!IsUserAdministrator())
            {
                ProcessStartInfo proc = new ProcessStartInfo();
                proc.UseShellExecute = true;
                proc.WorkingDirectory = Environment.CurrentDirectory;
                proc.FileName = Application.ExecutablePath;
                proc.Verb = "runas";

                try
                {
                    Process.Start(proc);
                }
                catch
                {
                    // The user refused the elevation.
                    // Do nothing and return directly ...
                    return;
                }
                Environment.Exit(7);
            }

            if (Properties.Settings.Default.Server == 2) // bu makina server
            {
                Application.Run(new GirisEkrani());
            }
            else //client
            {
                if (!File.Exists("tempfiles.xml") || !File.Exists("kategoriler.xml") || !File.Exists("masaDizayn.xml") || !File.Exists("menu.xml") || !File.Exists("urunler.xml") || !File.Exists("restoran.xml"))
                {
                    using (KontrolFormu dialog = new KontrolFormu("Bilgiler girilmemiş veri aktarımını başlatmak ister misiniz?", true))
                    {
                        DialogResult cevap = dialog.ShowDialog();
                        bool basarili = false;

                        if (cevap == DialogResult.Yes)
                        {
                            XMLAktarClient aktarimServeri = new XMLAktarClient();
                            for (int i = 0; i < 7; i++)
                            {
                                basarili = aktarimServeri.ClientTarafi();
                                if (!basarili)
                                    break;
                            }

                            if (basarili)
                            {
                                if (!File.Exists("tempfiles.xml") || !File.Exists("kategoriler.xml") || !File.Exists("masaDizayn.xml") || !File.Exists("menu.xml") || !File.Exists("urunler.xml") || !File.Exists("restoran.xml"))
                                {
                                    using (KontrolFormu dialog2 = new KontrolFormu("Dosyalarda eksik var, lütfen serverdaki dosyaları kontrol ediniz", false))
                                    {
                                        dialog2.ShowDialog();
                                        Environment.Exit(7);
                                    }
                                }
                                else
                                {
                                    using (KontrolFormu dialog3 = new KontrolFormu("Dosya alımı başarılı, lütfen yeniden giriş yapınız", false))
                                    {
                                        dialog3.ShowDialog();
                                        Environment.Exit(7);
                                    }
                                }
                            }
                            else
                            {
                                using (KontrolFormu dialog4 = new KontrolFormu("Dosya alımı başarısız, lütfen tekrar deneyiniz", false))
                                {
                                    dialog4.ShowDialog();
                                    Environment.Exit(7);
                                }
                            }
                        }
                        else
                        {
                            Environment.Exit(7);
                        }
                    }
                }
                else
                {
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

        //Kullanıcıyı admin girişine zorlamak için
        public static bool IsUserAdministrator()
        {
            bool isAdmin;
            try
            {
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch
            {
                isAdmin = false;
            }
            return isAdmin;
        }
    }
}