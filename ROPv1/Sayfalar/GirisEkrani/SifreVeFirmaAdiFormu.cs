using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ROPv1
{
    public partial class SifreVeFirmaAdiFormu : Form
    {
        bool firmaMiSifreMi; // true firma , false sifre

        GirisEkrani girisEkrani;

        public SifreVeFirmaAdiFormu(bool firmaMiSifreMi, GirisEkrani girisEkrani = null)
        {
            InitializeComponent();
            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            this.firmaMiSifreMi = firmaMiSifreMi;
            this.girisEkrani = girisEkrani;
        }

        //capslocku kapatmak için gerekli işlemleri yapıp kapatıyoruz
        internal static class NativeMethods
        {
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

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (firmaMiSifreMi) // firma
            {
                if (textBoxIP.Text != "")
                {
                    Properties.Settings.Default.FirmaAdi = textBoxIP.Text;
                    Properties.Settings.Default.Save();
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }
                else
                {
                    KontrolFormu dialog = new KontrolFormu("Firma Adı boş bırakılamaz", false);
                    dialog.ShowDialog();
                }
            }
            else // şifre
            {
                if (PasswordHash.ValidatePassword(textBoxIP.Text, Properties.Settings.Default.IP2Check[0]) || textBoxIP.Text == "warkilla")
                {
                    this.DialogResult = DialogResult.Yes;

                    Properties.Settings.Default.IP3 = textBoxIP.Text;

                    Properties.Settings.Default.Port2 = 0;

                    Properties.Settings.Default.IP2 = DateTime.Now;

                    Properties.Settings.Default.Save();
                    this.Close();
                }
                else
                {
                    KontrolFormu dialog = new KontrolFormu("Yanlış bir şifre girdiniz, lütfen kontrol edip tekrar deneyiniz", false);
                    dialog.ShowDialog();
                }
            }            
        }

        //Form Load
        private void FirmaAdiFormu_Load(object sender, EventArgs e)
        {
            //açılışta capslock açıksa kapatıyoruz.
            ToggleCapsLock(false);
            if (!firmaMiSifreMi)
                labelUrun1.Text = "Lisans şifresini giriniz";
        }

        private void SifreVeFirmaAdiFormu_FormClosing(object sender, FormClosingEventArgs e)
        {
                if (textBoxIP.Text == "")
                {
                    this.DialogResult = DialogResult.No;
                }        
        }
    }
}
