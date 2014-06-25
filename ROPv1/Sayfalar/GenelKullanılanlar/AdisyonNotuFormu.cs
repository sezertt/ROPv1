using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace ROPv1
{
    public partial class AdisyonNotuFormu : Form
    {
        public string AdisyonNotu;
        bool bilgisayarAdiGeldi = false;

        public AdisyonNotuFormu(string eskiNot)
        {
            InitializeComponent();

            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;

            //açılışta capslock açıksa kapatıyoruz.
            ToggleCapsLock(false);
            textboxNot.Text = eskiNot;
            textboxNot.Select();

            if (eskiNot == "Bilgisayar adını giriniz" || eskiNot == "Girilen bilgisayar adı kullanımda, lütfen başka bir bilgisayar adı giriniz")
            {
                bilgisayarAdiGeldi = true;
                checkBoxSave.Visible = true;
            }
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

        //sanal klayvemize basıldığında touchscreenkeyboard dll mize basılan key i yolluyoruz
        private void keyboardcontrol1_UserKeyPressed(object sender, KeyboardClassLibrary.KeyboardEventArgs e)
        {
            textboxNot.Select();
            SendKeys.Send(e.KeyboardKeyPressed);
        }

        private void buttonTamam_Click(object sender, EventArgs e)
        {
            AdisyonNotu = textboxNot.Text;
            if (bilgisayarAdiGeldi)
            {
                if (string.IsNullOrWhiteSpace(textboxNot.Text))
                {
                    KontrolFormu dialog = new KontrolFormu("Bilgisayar adı boş bırakılamaz", false);
                    dialog.ShowDialog();
                    textboxNot.Focus();
                    return;
                }

                if (checkBoxSave.Checked)
                {
                    Properties.Settings.Default.BilgisayarAdi = AdisyonNotu;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    Properties.Settings.Default.BilgisayarAdi = "";
                    Properties.Settings.Default.Save();
                }
            }
            this.Close();
        }

        private void textboxNot_Click(object sender, EventArgs e)
        {
            if (bilgisayarAdiGeldi)
                textboxNot.Select(0, textboxNot.TextLength);
        }

        private void textboxNot_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '<' || e.KeyChar == '>' || e.KeyChar == '&' || e.KeyChar == '=' || e.KeyChar == '*' || e.KeyChar == '-')
            {
                e.Handled = true;
            }
        }

        private void AdisyonNotuFormu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textboxNot.Text) && bilgisayarAdiGeldi)
            {
                e.Cancel = true;
            }
        }
    }
}
