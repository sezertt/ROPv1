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
    public partial class UrunIptalNedeniFormu : Form
    {
        public string iptalNedeni;

        bool iptalBasildi = false;

        public UrunIptalNedeniFormu(string urunBilgileri)
        {
            InitializeComponent();

            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;

            //açılışta capslock açıksa kapatıyoruz.
            labelIptalYazisi.Text = urunBilgileri;
            ToggleCapsLock(false);
            textboxNot.Focus();
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
            iptalNedeni = textboxNot.Text;

            if (string.IsNullOrWhiteSpace(textboxNot.Text))
            {
                KontrolFormu dialog = new KontrolFormu("İptal nedeni boş bırakılamaz", false);
                dialog.ShowDialog();
                textboxNot.Focus();
                return;
            }            
            
            this.Close();
        }

        private void textboxNot_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 22)
                e.Handled = true;

            if (e.KeyChar == '<' || e.KeyChar == '>' || e.KeyChar == '&' || e.KeyChar == '=' || e.KeyChar == '*' || e.KeyChar == '-')
            {
                e.Handled = true;
            }
        }

        private void AdisyonNotuFormu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textboxNot.Text) && !iptalBasildi)
            {
                e.Cancel = true;
            }
            else
                iptalNedeni = textboxNot.Text;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            iptalBasildi = true;
        }
    }
}