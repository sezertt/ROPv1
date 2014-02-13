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

// NOT : KISALTMALARIN AÇIKLAMALARI İÇİN UItemp.cs ye BAK

namespace ROPv1
{
    public partial class Kullanici : UserControl
    {
        List<UItemp> kullaniciListesi = new List<UItemp>(); // kategorileri tutacak liste

        public Kullanici()
        {
            InitializeComponent();

            //açılışta capslock açıksa kapatıyoruz.
            ToggleCapsLock(false);

            UItemp[] infoKullanici = new UItemp[1];

            #region xml oku

            XmlLoad<UItemp> loadInfoKullanicilar = new XmlLoad<UItemp>();
            infoKullanici = loadInfoKullanicilar.LoadRestoran("tempfiles.xml");

            #endregion

            //kategorileri tutacak listemize atıyoruz
            kullaniciListesi.AddRange(infoKullanici);

            for (int i = 0; i < kullaniciListesi.Count; i++)
            {
                treeUserName.Nodes.Add((new UnicodeEncoding()).GetString(kullaniciListesi[i].UIN) + " " + (new UnicodeEncoding()).GetString(kullaniciListesi[i].UIS));
            }

            comboNewTitle.Items.Add("Yönetici");
            comboNewTitle.Items.Add("Şef Garson");
            comboNewTitle.Items.Add("Garson");

            if (treeUserName.Nodes.Count < 2)
                buttonDeleteUser.Enabled = false;

            //Nodeların eklenmesinden sonra taşma varsa bile ekrana sığması için font boyutunu küçültüyoruz
            foreach (TreeNode node in treeUserName.Nodes)
            {
                while (treeUserName.Width - 12 < System.Windows.Forms.TextRenderer.MeasureText(node.Text, new Font(treeUserName.Font.FontFamily, treeUserName.Font.Size, treeUserName.Font.Style)).Width)
                {
                    treeUserName.Font = new Font(treeUserName.Font.FontFamily, treeUserName.Font.Size - 0.5f, treeUserName.Font.Style);
                }
            }

            treeUserName.SelectedNode = treeUserName.Nodes[0];
        }

        private void changeKullanici(object sender, TreeViewEventArgs e) // Farklı bir kullanıcı seçildi
        {
            if (buttonDeleteUser.Visible)
            {
                int i = treeUserName.SelectedNode.Index;
                textboxName.Text = (new UnicodeEncoding()).GetString(kullaniciListesi[i].UIN);
                textboxSurname.Text = (new UnicodeEncoding()).GetString(kullaniciListesi[i].UIS); 
                textboxUserName.Text = (new UnicodeEncoding()).GetString(kullaniciListesi[i].UIUN); 
                textboxPin.Text = "";
                textBoxPassword.Text = "";
                comboNewTitle.Text = (new UnicodeEncoding()).GetString(kullaniciListesi[i].UIU); 
                newUserForm.Text = textboxUserName.Text;

                for (int j = 0; j < 7; j++)
                {
                    if (Helper.VerifyHash("true", "SHA512", kullaniciListesi[i].UIY[j]))
                    {
                        treeYetkiler.Nodes[j].Checked = true;
                    }
                }

                if (i == 0)
                {
                    buttonDeleteUser.Enabled = false;
                    treeYetkiler.Enabled = false;
                    comboNewTitle.Enabled = false;
                }
                else
                {
                    buttonDeleteUser.Enabled = true;
                    treeYetkiler.Enabled = true;
                    comboNewTitle.Enabled = true;
                }
            }
        }

        //capslocku kapatmak için gerekli işlemleri yapıp kapatıyoruz
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
        static void ToggleCapsLock(bool onOrOff)
        {
            if (IsKeyLocked(Keys.CapsLock) == onOrOff)
                return;
            keybd_event(0x14, 0x45, 0x1, (UIntPtr)0);
            keybd_event(0x14, 0x45, 0x1 | 0x2, (UIntPtr)0);

        }

        private void keyboardcontrol1_UserKeyPressed(object sender, KeyboardClassLibrary.KeyboardEventArgs e)
        {
            SendKeys.Send(e.KeyboardKeyPressed);
        }

        private void pinPressed(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void comboBoxKeyPressed(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void showMenu(object sender, EventArgs e)
        {
            ((ComboBox)sender).DroppedDown = true;
        }

        //yeni kullanıcı ekle veya kullanıcıyı güncelle butonu
        private void buttonSaveNewUserPressed(object sender, EventArgs e)
        {
            if (textboxUserName.Text == "Yeni Kullanıcı" || textboxUserName.Text == "" || textboxSurname.Text == "" || textboxName.Text == "")
            {
                using (KontrolFormu dialog = new KontrolFormu("Hatalı bilgi girdiniz, lütfen kontrol edin", false))
                {
                    dialog.ShowDialog();
                }
                return;
            }

            if (newUserForm.Text == "Yeni Kullanıcı")
            {// yeni Kullanıcı kaydetme
                if (textboxPin.Text == "" || textBoxPassword.Text == "")
                {
                    using (KontrolFormu dialog = new KontrolFormu("Pin/Şifre girmediniz, lütfen kontrol edin", false))
                    {
                        dialog.ShowDialog();
                    }
                    return;
                }

                for (int j = 0; j < kullaniciListesi.Count(); j++)
                {
                    if (textboxUserName.Text == (new UnicodeEncoding()).GetString(kullaniciListesi[j].UIUN))
                    {
                        using (KontrolFormu dialog = new KontrolFormu("Kullanımda olan bir kullanıcı adı girdiniz, lütfen kontrol edin", false))
                        {
                            dialog.ShowDialog();
                        }
                        return;
                    }
                    else if (Helper.VerifyHash(textboxPin.Text, "SHA512", kullaniciListesi[j].UIPN))
                    {
                        using (KontrolFormu dialog = new KontrolFormu("Kullanımda olan bir pin girdiniz, lütfen kontrol edin", false))
                        {
                            dialog.ShowDialog();
                        }
                        return;
                    }
                }

                // tüm Kullanıcılar görünümüne kategoriyi ekliyoruz
                treeUserName.Nodes.Add(textboxName.Text + " " + textboxSurname.Text);

                newUserForm.Text = textboxUserName.Text;

                // tüm Kullanıcılar listemize Kullanıcıyı ekleyip kaydediyoruz
                UItemp temp = new UItemp();
                temp.UIN = (new UnicodeEncoding()).GetBytes(textboxName.Text);
                temp.UIS = (new UnicodeEncoding()).GetBytes(textboxSurname.Text);
                temp.UIUN = (new UnicodeEncoding()).GetBytes(textboxUserName.Text);
                temp.UIPN = Helper.ComputeHash(textboxPin.Text, "SHA512", null);
                temp.UIPW = Helper.ComputeHash(textBoxPassword.Text, "SHA512", null);
                temp.UIU = (new UnicodeEncoding()).GetBytes(comboNewTitle.Text);
                for (int i = 0; i < 7; i++)
                {
                    if(treeYetkiler.Nodes[i].Checked)
                        temp.UIY[i] = Helper.ComputeHash("true", "SHA512", null);
                    else
                        temp.UIY[i] = Helper.ComputeHash("false", "SHA512", null);
                }

                kullaniciListesi.Add(temp);

                // dosya korumayı açıyoruz
                File.SetAttributes("tempfiles.xml", FileAttributes.Normal);

                //kaydediyoruz
                XmlSave.SaveRestoran(kullaniciListesi, "tempfiles.xml");

                //yeniden korumaları ekliyoruz
                File.SetAttributes("tempfiles.xml", FileAttributes.Archive | FileAttributes.Hidden | FileAttributes.ReadOnly);

                treeUserName.SelectedNode = treeUserName.Nodes[treeUserName.Nodes.Count - 1];
                treeUserName.Focus();

                buttonDeleteUser.Visible = true;
                buttonAddNewUser.Enabled = true;
                buttonCancel.Visible = false;

                using (KontrolFormu dialog = new KontrolFormu("Yeni Kullanıcı Bilgileri Kaydedilmiştir", false))
                {
                    dialog.ShowDialog();
                }
            }
            else // Kullanıcı düzenleme
            {
                if (textboxPin.Text != "")
                {
                    DialogResult eminMisiniz;

                    using (KontrolFormu dialog = new KontrolFormu(treeUserName.SelectedNode.Text + " adlı kullanıcının pinini değiştirmek istediğinize emin misiniz?", true))
                    {
                        eminMisiniz = dialog.ShowDialog();
                    }

                    if (eminMisiniz == DialogResult.No)
                    {
                        textboxPin.Text = "";
                    }
                }

                if (textBoxPassword.Text != "")
                {
                    DialogResult eminMisiniz;

                    using (KontrolFormu dialog = new KontrolFormu(treeUserName.SelectedNode.Text + " adlı kullanıcının şifresini değiştirmek istediğinize emin misiniz?", true))
                    {
                        eminMisiniz = dialog.ShowDialog();
                    }

                    if (eminMisiniz == DialogResult.No)
                    {
                        textBoxPassword.Text = "";
                    }
                }
                                
                int kacTane = 0,kacTane1 = 0;

                for (int j = 0; j < kullaniciListesi.Count(); j++)
                {
                    if (Helper.VerifyHash(textboxPin.Text, "SHA512", kullaniciListesi[j].UIPN))
                    {
                        kacTane++;
                    }
                    if (textboxUserName.Text == (new UnicodeEncoding()).GetString(kullaniciListesi[j].UIUN))
                        kacTane1++;

                    if (kacTane == 2 || kacTane1 == 2)
                    {
                        using (KontrolFormu dialog = new KontrolFormu("Hatalı kullanıcı adı veya pin girdiniz, lütfen kontrol edin", false))
                        {
                            dialog.ShowDialog();
                        }
                        return;
                    }
                }

                int i = treeUserName.SelectedNode.Index;
                //kullanıcının listedeki bilgilerini güncelliyoruz ve kaydediyoruz
                kullaniciListesi[i].UIUN = (new UnicodeEncoding()).GetBytes(textboxUserName.Text);
                kullaniciListesi[i].UIN = (new UnicodeEncoding()).GetBytes(textboxName.Text);
                kullaniciListesi[i].UIS = (new UnicodeEncoding()).GetBytes(textboxSurname.Text);
                kullaniciListesi[i].UIUN = (new UnicodeEncoding()).GetBytes(textboxUserName.Text);

                if (textboxPin.Text != "")
                    kullaniciListesi[i].UIPN = Helper.ComputeHash(textboxPin.Text, "SHA512", null); 

                if (textBoxPassword.Text != "")
                    kullaniciListesi[i].UIPW = Helper.ComputeHash(textBoxPassword.Text, "SHA512", null);

                kullaniciListesi[i].UIU = (new UnicodeEncoding()).GetBytes(comboNewTitle.Text);

                for (int x = 0; x < 7; x++)
                {
                    if (treeYetkiler.Nodes[x].Checked)
                        kullaniciListesi[i].UIY[x] = Helper.ComputeHash("true", "SHA512", null);
                    else
                        kullaniciListesi[i].UIY[x] = Helper.ComputeHash("false", "SHA512", null);
                }

                // dosya korumayı açıyoruz
                File.SetAttributes("tempfiles.xml", FileAttributes.Normal);

                //kaydediyoruz
                XmlSave.SaveRestoran(kullaniciListesi, "tempfiles.xml");

                //yeniden korumaları ekliyoruz
                File.SetAttributes("tempfiles.xml", FileAttributes.Archive | FileAttributes.Hidden | FileAttributes.ReadOnly);

                //görünümdeki isimleri güncelliyoruz
                treeUserName.SelectedNode.Text = textboxName.Text +" "+textboxSurname.Text;
                newUserForm.Text = textboxUserName.Text;
                using (KontrolFormu dialog = new KontrolFormu("Kullanıcı Bilgileri Güncellenmiştir", false))
                {
                    dialog.ShowDialog();
                }
            }
        }

        //seçilen kullanıcıyı sil butonu
        private void buttonDeleteExistingUserPressed(object sender, EventArgs e)
        {
            DialogResult eminMisiniz;

            using (KontrolFormu dialog = new KontrolFormu(treeUserName.SelectedNode.Text + " adlı kullanıcıyı silmek istediğinize emin misiniz?", true))
            {
                eminMisiniz = dialog.ShowDialog();
            }

            if (eminMisiniz == DialogResult.Yes)
            {
                //listeden kullanıcıyı siliyoruz
                kullaniciListesi.RemoveAt(treeUserName.SelectedNode.Index);

                // dosya korumayı açıyoruz
                File.SetAttributes("tempfiles.xml", FileAttributes.Normal);

                //kaydediyoruz
                XmlSave.SaveRestoran(kullaniciListesi, "tempfiles.xml");

                //yeniden korumaları ekliyoruz
                File.SetAttributes("tempfiles.xml", FileAttributes.Archive | FileAttributes.Hidden | FileAttributes.ReadOnly);

                // ağaçtan ürünü siliyoruz
                treeUserName.SelectedNode.Remove();
            }
        }

        //yeni kullanıcı oluşturmayı iptal et butonu
        private void buttonCancelSavingNewUserPressed(object sender, EventArgs e)
        {
            int i = treeUserName.SelectedNode.Index;
            textboxName.Text = (new UnicodeEncoding()).GetString(kullaniciListesi[i].UIN);
            textboxSurname.Text = (new UnicodeEncoding()).GetString(kullaniciListesi[i].UIS);
            textboxUserName.Text = (new UnicodeEncoding()).GetString(kullaniciListesi[i].UIUN);
            textboxPin.Text = "";
            textBoxPassword.Text = "";
            comboNewTitle.Text = (new UnicodeEncoding()).GetString(kullaniciListesi[i].UIU);
            newUserForm.Text = textboxUserName.Text;

            for (int j = 0; j < 7; j++)
            {
                if (Helper.VerifyHash("true", "SHA512", kullaniciListesi[i].UIY[j]))
                {
                    treeYetkiler.Nodes[j].Checked = true;
                }
            }

            if (i == 0)
            {
                buttonDeleteUser.Enabled = false;
                treeYetkiler.Enabled = false;
                comboNewTitle.Enabled = false;
            }
            else
            {
                buttonDeleteUser.Enabled = true;
                treeYetkiler.Enabled = true;
                comboNewTitle.Enabled = true;
            }

            buttonDeleteUser.Visible = true;
            buttonCancel.Visible = false;
            buttonAddNewUser.Enabled = true;
            treeUserName.Focus();
        }

        //yeni kullanıcı oluşturmaya başla butonu
        private void buttonCreateNewUserPressed(object sender, EventArgs e)
        {
            if (newUserForm.Text != "Yeni Kullanıcı") // her basışta yeniden ayarlanmasın diye, ayarlandı mı kontrolü
            {
                newUserForm.Text = "Yeni Kullanıcı";
                textboxName.Text = "";
                textboxSurname.Text = "";
                textboxUserName.Text = "";
                textboxPin.Text = "";
                textBoxPassword.Text = "";
                comboNewTitle.SelectedIndex = 2;

                for (int j = 0; j < 7; j++)
                {
                    treeYetkiler.Nodes[j].Checked = false;
                }

                buttonDeleteUser.Visible = false;
                buttonCancel.Visible = true;
                buttonAddNewUser.Enabled = false;
                buttonDeleteUser.Enabled = true;
                treeYetkiler.Enabled = true;
                comboNewTitle.Enabled = true;
            }
            textboxName.Focus();
        }

        private void checkYetkiFromTree(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeViewHitTestInfo info = treeYetkiler.HitTest(treeYetkiler.PointToClient(Cursor.Position));

            if (info != null && info.Location != TreeViewHitTestLocations.StateImage)
            {
                int index = info.Node.Index;
                if (treeYetkiler.Nodes[index].Checked == true)
                    treeYetkiler.Nodes[index].Checked = false;
                else
                    treeYetkiler.Nodes[index].Checked = true;

                //pin şifre değiştirme seçilirse ayarlarda seçili olmak zorunda, pin seçiliyken ayarlar seçimi kaldırılırsa pinde kaldırılmalı
                if (e.Node.Index == 6 && !treeYetkiler.Nodes[4].Checked)
                {
                    treeYetkiler.Nodes[4].Checked = true;
                }
                if (e.Node.Index == 4 && treeYetkiler.Nodes[6].Checked)
                {
                    treeYetkiler.Nodes[6].Checked = false;
                }
            }
        }

        private void comboBoxYetkileriDegisti(object sender, EventArgs e)
        {
            if (comboNewTitle.SelectedIndex == 0)
            {
                for (int j = 0; j < 7; j++)
                {
                    treeYetkiler.Nodes[j].Checked = true;
                }
            }
            else if (comboNewTitle.SelectedIndex == 1)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (j == 4 || j == 6)
                    {
                        treeYetkiler.Nodes[j].Checked = false;
                    }
                    else
                        treeYetkiler.Nodes[j].Checked = true;
                }
            }
            else
            {
                for (int j = 0; j < 7; j++)
                {
                    treeYetkiler.Nodes[j].Checked = false;
                }
            }
        }

        private void ifPinChecked(object sender, TreeViewEventArgs e)
        {

        }
    }
}