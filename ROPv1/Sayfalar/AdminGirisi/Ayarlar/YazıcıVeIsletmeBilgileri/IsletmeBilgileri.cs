using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Globalization;
using System.Data.SqlClient;
using System.Drawing.Printing;

namespace ROPv1
{
    public partial class IsletmeBilgileri : UserControl
    {
        List<string[]> yazicilar = new List<string[]>();

        public IsletmeBilgileri()
        {
            InitializeComponent();
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

        private void keyboardcontrol1_UserKeyPressed(object sender, KeyboardClassLibrary.KeyboardEventArgs e)
        {
            SendKeys.Send(e.KeyboardKeyPressed);
        }

        private void IsletmeBilgileri_Load(object sender, EventArgs e)
        {
            //açılışta capslock açıksa kapatıyoruz.
            ToggleCapsLock(false);

            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT * FROM Yazici");
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string[] yazici = new string[5];
                yazici[0] = dr.GetString(0); // yazici adi
                yazici[1] = dr.GetString(1); // firma adi
                yazici[2] = dr.GetString(2); // firma adres
                yazici[3] = dr.GetString(3); // yazici
                yazici[4] = dr.GetString(4); // telefon
                yazicilar.Add(yazici);

                treeYaziciAdi.Nodes.Add(yazici[0]);

                bool firmaYok = true;
                for (int i = 0; i < comboBoxFirmaAdi.Items.Count; i++)
                {
                    if (comboBoxFirmaAdi.Items[i].ToString() == yazici[1])
                    {
                        firmaYok = false;
                        break;
                    }
                }
                if (firmaYok)
                    comboBoxFirmaAdi.Items.Add(yazici[1]);

                if (treeYaziciAdi.Nodes.Count < 2)
                {  
                    comboYaziciAdi.Text = yazici[0];
                    comboBoxFirmaAdi.Text = yazici[1];
                    textBoxAdres.Text = yazici[2];                  
                    comboYukluYazicilar.Text = yazici[3];
                    textBoxTelefon.Text = yazici[4];
                }
            }

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            // Yüklü printer var mı bak
            if (PrinterSettings.InstalledPrinters.Count > 0)
            {
                //varsa comboboxa yazdır
                foreach (String printer in PrinterSettings.InstalledPrinters)
                {
                    comboYukluYazicilar.Items.Add(printer.ToString());
                }
            }

            //Nodeların eklenmesinden sonra taşma varsa bile ekrana sığması için font boyutunu küçültüyoruz
            foreach (TreeNode node in treeYaziciAdi.Nodes)
            {
                while (treeYaziciAdi.Width - 12 < System.Windows.Forms.TextRenderer.MeasureText(node.Text, new Font(treeYaziciAdi.Font.FontFamily, treeYaziciAdi.Font.Size, treeYaziciAdi.Font.Style)).Width)
                {
                    treeYaziciAdi.Font = new Font(treeYaziciAdi.Font.FontFamily, treeYaziciAdi.Font.Size - 0.5f, treeYaziciAdi.Font.Style);
                }
            }
            if (treeYaziciAdi.Nodes.Count > 0)
                treeYaziciAdi.SelectedNode = treeYaziciAdi.Nodes[0];

            if (treeYaziciAdi.Nodes.Count < 2)
                buttonYaziciyiSil.Enabled = false;
        }

        private void combo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 22)
                e.Handled = true;

            if (e.KeyChar == '<' || e.KeyChar == '>' || e.KeyChar == '&' || e.KeyChar == '=' || e.KeyChar == '*' || e.KeyChar == '-')
            {
                e.Handled = true;
            }
        }

        private void comboYazicilar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboYazicilar_Click(object sender, EventArgs e)
        {
            comboYukluYazicilar.Items.Clear();
            // Yüklü printer var mı bak
            if (PrinterSettings.InstalledPrinters.Count > 0)
            {
                //varsa comboboxa yazdır
                foreach (String printer in PrinterSettings.InstalledPrinters)
                {
                    comboYukluYazicilar.Items.Add(printer.ToString());
                }
            }
            ((ComboBox)sender).DroppedDown = true;
        }

        private void treeYaziciAdi_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (buttonYaziciyiSil.Visible)
            {
                comboYaziciAdi.Text = yazicilar[treeYaziciAdi.SelectedNode.Index][0];
                comboBoxFirmaAdi.Text = yazicilar[treeYaziciAdi.SelectedNode.Index][1];
                textBoxAdres.Text = yazicilar[treeYaziciAdi.SelectedNode.Index][2];
                comboYukluYazicilar.Text = yazicilar[treeYaziciAdi.SelectedNode.Index][3];
                textBoxTelefon.Text = yazicilar[treeYaziciAdi.SelectedNode.Index][4];
                newYaziciForm.Text = comboYaziciAdi.Text;
            }
        }

        private void buttonYeniYaziciEkle_Click(object sender, EventArgs e)
        {
            if (treeYaziciAdi.Nodes.Count > 11)
                return;

            if (newYaziciForm.Text != "Yeni Yazıcı")
            {
                newYaziciForm.Text = "Yeni Yazıcı";
                textBoxAdres.Text = "";
                comboYaziciAdi.Text = "";
                comboYukluYazicilar.Text = "";
                textBoxTelefon.Text = "";
                buttonYaziciyiSil.Visible = false;
                buttonIptal.Visible = true;
            }
            comboBoxFirmaAdi.Focus();
        }

        private void buttonIptal_Click(object sender, EventArgs e)
        {
            comboYaziciAdi.Text = yazicilar[treeYaziciAdi.SelectedNode.Index][0];
            comboBoxFirmaAdi.Text = yazicilar[treeYaziciAdi.SelectedNode.Index][1];
            textBoxAdres.Text = yazicilar[treeYaziciAdi.SelectedNode.Index][2];
            comboYukluYazicilar.Text = yazicilar[treeYaziciAdi.SelectedNode.Index][3];
            textBoxTelefon.Text = yazicilar[treeYaziciAdi.SelectedNode.Index][4];
            newYaziciForm.Text = comboYaziciAdi.Text;

            buttonYaziciyiSil.Visible = true;
            buttonIptal.Visible = false;
            treeYaziciAdi.Focus();
        }

        private void buttonYaziciyiSil_Click(object sender, EventArgs e)
        {
            if (treeYaziciAdi.SelectedNode == null)
                return;

            DialogResult eminMisiniz;

            KontrolFormu dialog = new KontrolFormu(treeYaziciAdi.SelectedNode.Text + " adlı yazıcıyı silmek istediğinize emin misiniz?", true);
            eminMisiniz = dialog.ShowDialog();

            if (eminMisiniz == DialogResult.Yes)
            {
                SqlCommand cmd = SQLBaglantisi.getCommand("DELETE FROM Yazici WHERE YaziciAdi='" + yazicilar[treeYaziciAdi.SelectedNode.Index][0] + "'");

                cmd.ExecuteNonQuery();

                cmd.Connection.Close();
                cmd.Connection.Dispose();

                treeYaziciAdi.Nodes[treeYaziciAdi.SelectedNode.Index].Remove();

                if (treeYaziciAdi.Nodes.Count < 2)
                    buttonYaziciyiSil.Enabled = false;
            }
        }

        private void buttonYaziciyiKaydet_Click(object sender, EventArgs e)
        {
            KontrolFormu dialog;

            if(comboYukluYazicilar.Text.Contains("-") || comboYukluYazicilar.Text.Contains("<") || comboYukluYazicilar.Text.Contains(">") || comboYukluYazicilar.Text.Contains("&") || comboYukluYazicilar.Text.Contains("=") || comboYukluYazicilar.Text.Contains("*"))
            {
                dialog = new KontrolFormu("Yazıcı adında -, &, <, >, * karakterleri bulunamaz, lütfen yazıcınızı tekrar başka bir isimle yükleyin veya başka bir yazıcı seçin ", false);
                dialog.Show();
                return;
            }

            if (comboYaziciAdi.Text == "Yeni Yazıcı" || comboBoxFirmaAdi.Text == "" || comboYukluYazicilar.Text == "")
            {
                dialog = new KontrolFormu("Eksik veya hatalı bilgi girdiniz, lütfen kontrol ediniz", false);
                dialog.Show();
                return;
            }

            //Yeni yazıcıyı kaydet tuşu. ekle tuşuna basıp bilgileri girdikten sonra kaydete basıyoruz önce girilen bilgilerin doğruluğu
            //kontrol edilir sonra yazıcının treeviewdaki yeri + ayarlardaki yeri tag bilgisi olarak eklenir
            if (newYaziciForm.Text == "Yeni Yazıcı")
            {
                for (int i = 0; i < yazicilar.Count; i++)
                {
                    if (string.Equals(yazicilar[i][0], comboYaziciAdi.Text, StringComparison.CurrentCultureIgnoreCase))
                    {
                        dialog = new KontrolFormu("Aynı yazıcı ismi sistemde kayıtlıdır. Yeni yazıcınızı Adisyon5/Mutfak5 gibi kaydediniz.", false);

                        dialog.Show();
                        return;
                    }
                }

                treeYaziciAdi.Nodes.Add(comboYaziciAdi.Text);

                SqlCommand cmd = SQLBaglantisi.getCommand("INSERT INTO Yazici(YaziciAdi,FirmaAdi,FirmaAdres,Yazici,Telefon) VALUES(@_YaziciAdi,@_FirmaAdi,@_FirmaAdres,@_Yazici,@_Telefon)");
                cmd.Parameters.AddWithValue("@_YaziciAdi", comboYaziciAdi.Text);
                cmd.Parameters.AddWithValue("@_FirmaAdi", comboBoxFirmaAdi.Text);
                cmd.Parameters.AddWithValue("@_FirmaAdres", textBoxAdres.Text);
                cmd.Parameters.AddWithValue("@_Yazici", comboYukluYazicilar.Text);
                cmd.Parameters.AddWithValue("@_Telefon", textBoxTelefon.Text);

                cmd.ExecuteNonQuery();

                cmd.Connection.Close();
                cmd.Connection.Dispose();

                string[] yazici = new string[5];
                yazici[0] = comboYaziciAdi.Text; // yazici adi
                yazici[1] = comboBoxFirmaAdi.Text; // firma adi
                yazici[2] = textBoxAdres.Text; // firma adres
                yazici[3] = comboYukluYazicilar.Text; // yazici
                yazici[4] = textBoxTelefon.Text; // telefon
                yazicilar.Add(yazici);

                newYaziciForm.Text = comboYaziciAdi.Text;

                treeYaziciAdi.SelectedNode = treeYaziciAdi.Nodes[treeYaziciAdi.Nodes.Count - 1];
                treeYaziciAdi.Focus();

                buttonYaziciyiSil.Visible = true;
                buttonIptal.Visible = false;

                if (treeYaziciAdi.Nodes.Count > 1)
                    buttonYaziciyiSil.Enabled = true;

                bool firmaYok = true;
                for (int i = 0; i < comboBoxFirmaAdi.Items.Count; i++)
                {
                    if (comboBoxFirmaAdi.Items[i].ToString() == yazici[1])
                    {
                        firmaYok = false;
                        break;
                    }
                }
                if(firmaYok)
                    comboBoxFirmaAdi.Items.Add(yazici[1]);

                dialog = new KontrolFormu("Yeni Yazıcı Bilgileri Kaydedilmiştir", false);
                dialog.Show();
            }
            else
            {
                if (comboYaziciAdi.Text != yazicilar[treeYaziciAdi.SelectedNode.Index][0])
                {
                    for (int i = 0; i < yazicilar.Count; i++)
                    {
                        if (string.Equals(yazicilar[i][0], comboYaziciAdi.Text, StringComparison.CurrentCultureIgnoreCase))
                        {
                            dialog = new KontrolFormu("Aynı yazıcı ismi sistemde kayıtlıdır. Yeni yazıcınızı Adisyon5/Mutfak5 gibi kaydediniz.", false);
                            dialog.Show();
                            return;
                        }
                    }
                }

                // Yazıcıda değişiklik yapıldıktan sonra basılan kaydet butonu.
                SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Yazici SET YaziciAdi=@yaziciAdi,FirmaAdi=@firmaAdi,FirmaAdres=@firmaAdres,Yazici=@yazici,Telefon=@telefon WHERE YaziciAdi=@yaziciAdi2");
                cmd.Parameters.AddWithValue("@yaziciAdi", comboYaziciAdi.Text);
                cmd.Parameters.AddWithValue("@firmaAdi", comboBoxFirmaAdi.Text);
                cmd.Parameters.AddWithValue("@firmaAdres", textBoxAdres.Text);
                cmd.Parameters.AddWithValue("@yazici", comboYukluYazicilar.Text);
                cmd.Parameters.AddWithValue("@telefon",textBoxTelefon.Text);
                cmd.Parameters.AddWithValue("@yaziciAdi2", yazicilar[treeYaziciAdi.SelectedNode.Index][0]);
                cmd.ExecuteNonQuery();

                cmd.Connection.Close();
                cmd.Connection.Dispose();

                yazicilar[treeYaziciAdi.SelectedNode.Index][0] = comboYaziciAdi.Text;
                yazicilar[treeYaziciAdi.SelectedNode.Index][1] = comboBoxFirmaAdi.Text;
                yazicilar[treeYaziciAdi.SelectedNode.Index][2] = textBoxAdres.Text;
                yazicilar[treeYaziciAdi.SelectedNode.Index][3] = comboYukluYazicilar.Text;
                yazicilar[treeYaziciAdi.SelectedNode.Index][4] = textBoxTelefon.Text;

                treeYaziciAdi.Nodes[treeYaziciAdi.SelectedNode.Index].Text = comboYaziciAdi.Text;
                newYaziciForm.Text = comboYaziciAdi.Text;

                dialog = new KontrolFormu("Yazıcı Bilgileri Güncellenmiştir", false);

                dialog.Show();
            }


            //Nodeların eklenmesinden sonra taşma varsa bile ekrana sığması için font boyutunu küçültüyoruz
            foreach (TreeNode node in treeYaziciAdi.Nodes)
            {
                while (treeYaziciAdi.Width - 12 < System.Windows.Forms.TextRenderer.MeasureText(node.Text, new Font(treeYaziciAdi.Font.FontFamily, treeYaziciAdi.Font.Size, treeYaziciAdi.Font.Style)).Width)
                {
                    treeYaziciAdi.Font = new Font(treeYaziciAdi.Font.FontFamily, treeYaziciAdi.Font.Size - 0.5f, treeYaziciAdi.Font.Style);
                }
            }
        }

        private void comboYaziciAdi_Click(object sender, EventArgs e)
        {
            ((ComboBox)sender).DroppedDown = true;
        }
    }
}