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

namespace ROPv1
{
    public partial class MasaPlan : UserControl
    {
        //masa planının değişip değişmediğini kontrol eden editingDesign ve masa ismi değiştiyse kaydedilip edilmemesi gereğini belirleyen shouldsavetablename değişkenleri
        bool editingDesign, shouldSaveTableName;

        //toplam masa dizaynlarının sayısını tuttuğumuz settings değişkenini integer bir değişkene atarak programın geri kalanında kullanmamızı sağlıyoruz
        int masaDizaynSayisi = Properties.Settings.Default.masaDizaynSayisi;

        //xml olarak kayıtlı tutulan masa dizaynlarını programımızda kullanmak amacıyla oluşturacağımız dizayn listesi
        List<MasaDizayn> masaDizaynListesi = new List<MasaDizayn>();

        //Programın ilk oluşumunda default verilen ve yeni dizayn oluşumunda default gösterilen masa planını bir stringde tutuyoruz


        public MasaPlan()
        {
            InitializeComponent();
            /*
            Properties.Settings.Default.masaDizaynSayisi = 1;
            Properties.Settings.Default.Save();
            */

            //açılışta capslock açıksa kapatıyoruz.
            ToggleCapsLock(false);

            #region xml oku

            MasaDizayn[] info = new MasaDizayn[masaDizaynSayisi];

            //eğer masaDizayn listesi bulunmuyorsa default değerlerle ilk dizaynı oluşturuyoruz
            if (!File.Exists("masaDizayn.xml"))
            {
                int departmanSayisi = Properties.Settings.Default.departmanSayisi;
                Restoran[] departmanVarmi = new Restoran[departmanSayisi];

                if (!File.Exists("restoran.xml"))
                {
                    departmanVarmi[0] = new Restoran();
                    departmanVarmi[0].DepartmanAdi = "Departman";
                    departmanVarmi[0].DepartmanMenusu = "Menü";
                    departmanVarmi[0].DepartmanEkrani = "Masa Ekranı";
                    departmanVarmi[0].DepartmanDeposu = "Depo";
                    XmlSave.SaveRestoran(departmanVarmi, "restoran.xml");
                }

                XmlLoad<Restoran> loadInfoFromDepartman = new XmlLoad<Restoran>();
                departmanVarmi = loadInfoFromDepartman.LoadRestoran("restoran.xml");


                info[0] = new MasaDizayn();
                info[0].masaPlanIsmi = departmanVarmi[0].DepartmanEkrani;

                string[][] refresher = new string[][]
                {
                    new string[] {"RP1", "RP2", "RP3", "RP4", "RP5", null, null},
                    new string[] {"RP6", "RP7", "RP8", "RP9", "RP10", null, null},
                    new string[] {"RP11", "RP12", "RP13", "RP14", "RP15", null, null},
                    new string[] {"RP16", "RP17", "RP18", "RP19", "RP20", null, null},
                    new string[] {"RP21", "RP22", "RP23", "RP24", "RP25", null, null},
                    new string[] {null, null, null, null, null, null, null}
                };

                info[0].masaYerleri = refresher;
                XmlSave.SaveRestoran(info, "masaDizayn.xml");
            }

            //liste varsa okuyoruz
            XmlLoad<MasaDizayn> loadInfo = new XmlLoad<MasaDizayn>();
            info = loadInfo.LoadRestoran("masaDizayn.xml");

            //kendi listemize atıyoruz
            masaDizaynListesi.AddRange(info);

            //listenin ilk elemanının ismini ekranda gösteriyoruz
            textTableDesignName.Text = masaDizaynListesi[0].masaPlanIsmi;

            //Kaç masa eklendiğini tutması için oluşturulan değişken
            int masaSayisi = 0;

            //tüm masalar oluşturuluyor(42 adet) ve listenin ilk elemanının planına göre dolular belirleniyor
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Button buttonTable = new Button();
                    buttonTable.UseVisualStyleBackColor = false;
                    buttonTable.BackColor = Color.White;
                    buttonTable.ForeColor = SystemColors.ActiveCaption;
                    buttonTable.Font = new Font("Arial", 12, FontStyle.Bold);
                    buttonTable.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    tablePanel.Controls.Add(buttonTable, j, i);
                    buttonTable.Click += buttonTablePressed;
                    buttonTable.Name = "" + i + j;

                    if (masaDizaynListesi[0].masaYerleri[i][j] != null)
                    {
                        buttonTable.Text = masaDizaynListesi[0].masaYerleri[i][j];
                        buttonTable.Visible = true;
                        masaSayisi++;
                    }
                    else
                        buttonTable.Visible = false;
                }
            }

            numericTableCount.Value = masaSayisi;

            newTableForm.Text = textTableDesignName.Text;

            treeMasaPlanName.Nodes.Add(masaDizaynListesi[0].masaPlanIsmi);

            //listede başka dizaynlar varsa onların da isimleri gösteriliyor
            for (int i = 1; i < masaDizaynSayisi; i++)
            {
                treeMasaPlanName.Nodes.Add(masaDizaynListesi[i].masaPlanIsmi);
            }

            //Nodeların eklenmesinden sonra taşma varsa bile ekrana sığması için font boyutunu küçültüyoruz
            foreach (TreeNode node in treeMasaPlanName.Nodes)
            {
                while (treeMasaPlanName.Width - 12 < System.Windows.Forms.TextRenderer.MeasureText(node.Text, new Font(treeMasaPlanName.Font.FontFamily, treeMasaPlanName.Font.Size, treeMasaPlanName.Font.Style)).Width)
                {
                    treeMasaPlanName.Font = new Font(treeMasaPlanName.Font.FontFamily, treeMasaPlanName.Font.Size - 0.5f, treeMasaPlanName.Font.Style);
                }
            }

            #endregion

            //ilk dizaynı seçili dizayn yapıyoruz
            treeMasaPlanName.SelectedNode = treeMasaPlanName.Nodes[0];

            // 1 dizayn varsa silinemesin
            if (treeMasaPlanName.Nodes.Count < 2)
                buttonDeleteTable.Enabled = false;

            // 10 dan fazla dizayn eklenemesin
            if (treeMasaPlanName.Nodes.Count > 9)
                buttonAddTableDesign.Enabled = false;
        }

        //Seçilen masa dizaynının bilgileri aktarılır
        private void changeTableDesign(object sender, TreeViewEventArgs e)
        {
            // eğer kayıt edilmesi gereken masa ismi varsa kaydedilir
            if (shouldSaveTableName)
            {
                XmlSave.SaveRestoran(masaDizaynListesi, "masaDizayn.xml");
                shouldSaveTableName = false;
                textTableName.Enabled = false;
            }

            //silme tuşu disabled durumdaysa masa değişimi yapılmaz, önce masa düzenlemenin kapatılması gerekir
            if (buttonDeleteTable.Enabled)
            {
                //seçilen masa dizaynı ismini görünüme ekle 
                textTableDesignName.Text = masaDizaynListesi[treeMasaPlanName.SelectedNode.Index].masaPlanIsmi;

                //seçilen masa dizaynının masalarına göre ekranı yerleştir
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        Button tablebutton = tablePanel.Controls.Find("" + i + j, false)[0] as Button;
                        if (masaDizaynListesi[treeMasaPlanName.SelectedNode.Index].masaYerleri[i][j] != null)
                        {
                            tablebutton.Text = masaDizaynListesi[treeMasaPlanName.SelectedNode.Index].masaYerleri[i][j];
                            tablebutton.Visible = true;
                            tablebutton.ForeColor = SystemColors.ActiveCaption;
                        }
                        else
                        {
                            tablebutton.Text = "";
                            tablebutton.Visible = false;
                            tablebutton.ForeColor = Color.White;
                        }
                        tablebutton.BackColor = Color.White;
                    }
                }

                newTableForm.Text = textTableDesignName.Text;
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

        //sanal klayvemize basıldığında touchscreenkeyboard dll mize basılan key i yolluyoruz
        private void keyboardcontrol1_UserKeyPressed(object sender, KeyboardClassLibrary.KeyboardEventArgs e)
        {
            SendKeys.Send(e.KeyboardKeyPressed);
        }

        //Var olan departmanı silme tuşu. Departmanın bilgilerini siliyoruz, kaydedip treeviewdan çıkarıyoruz
        private void deleteTableDesign(object sender, EventArgs e)
        {
            // eğer kayıt edilmesi gereken masa ismi varsa kaydedilir
            if (shouldSaveTableName)
            {
                shouldSaveTableName = false;
                textTableName.Enabled = false;
            }

            DialogResult eminMisiniz;

            using (KontrolFormu dialog = new KontrolFormu(treeMasaPlanName.SelectedNode.Text + " adlı masa planını silmek istediğinize emin misiniz?", true))
            {
                eminMisiniz = dialog.ShowDialog();
            }

            if (eminMisiniz == DialogResult.Yes)
            {
                // seçilen dizaynı listeden çıkar
                masaDizaynListesi.RemoveAt(treeMasaPlanName.SelectedNode.Index);

                // listeyi xmle güncelle
                XmlSave.SaveRestoran(masaDizaynListesi, "masaDizayn.xml");

                // seçilen dizaynı görünümden çıkar
                treeMasaPlanName.Nodes[treeMasaPlanName.SelectedNode.Index].Remove();

                //dizayn sayısını azalt ve settingi kaydet
                masaDizaynSayisi--;
                Properties.Settings.Default.masaDizaynSayisi = masaDizaynSayisi;
                Properties.Settings.Default.Save();

                // 1 masa dizaynı kaldıysa silemesin
                if (treeMasaPlanName.Nodes.Count < 2)
                    buttonDeleteTable.Enabled = false;

                // 10 dan az dizayn kaldıysa ekleme butonunu aç
                if (treeMasaPlanName.Nodes.Count < 10)
                    buttonAddTableDesign.Enabled = true;

                newTableForm.Enabled = false;
            }           
        }

        string[][] refresher2 = new string[][]
                {
                    new string[] {"RP1", "RP2", "RP3", "RP4", "RP5", null, null},
                    new string[] {"RP6", "RP7", "RP8", "RP9", "RP10", null, null},
                    new string[] {"RP11", "RP12", "RP13", "RP14", "RP15", null, null},
                    new string[] {"RP16", "RP17", "RP18", "RP19", "RP20", null, null},
                    new string[] {"RP21", "RP22", "RP23", "RP24", "RP25", null, null},
                    new string[] {null, null, null, null, null, null, null}
                };

        //Yeni Masa düzeni Ekle Tuşu text ve numericleri ayarlayıp Ekleme Ekranını Gösteriyoruz
        private void addNewTableDesign(object sender, EventArgs e)
        {
            //eğer yeni masa eklenmek isteniyorsa ilk basıldığında düzenle, sonraki basamaklarda aynı işlemleri yapmamak için kontrol ediyoruz
            if (newTableForm.Text != "Yeni Masa Planı")
            {
                textTableName.Enabled = false;
                editingDesign = true;
                newTableForm.Text = "Yeni Masa Planı";
                textTableDesignName.Text = "";
                numericTableCount.Value = 25;
                buttonDeleteTable.Enabled = false;
                newTableForm.Enabled = true;
                buttonEditDesign.Enabled = false;

                //default dizaynımız olan refreshera göre ekranı düzenle
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        Button tablebutton = tablePanel.Controls.Find("" + i + j, false)[0] as Button;
                        if (refresher2[i][j] != null)
                        {
                            tablebutton.BackColor = SystemColors.ActiveCaption;
                            tablebutton.ForeColor = Color.White;
                            tablebutton.Text = "Seçildi";
                        }
                        else
                        {
                            tablebutton.BackColor = Color.White;
                            tablebutton.Visible = true;
                            tablebutton.Text = "";
                        }
                    }
                }
            }
            textTableDesignName.Focus();
        }

        //Yeni eklemeyi iptal etme tuşu. 
        private void cancelNewTable(object sender, EventArgs e)
        {
            //yeni ekleme seçildiğinde yapılan düzenlemeleri geri al ve o an seçili olan dizaynın bilgileriyle ekranı doldur
            textTableDesignName.Text = masaDizaynListesi[treeMasaPlanName.SelectedNode.Index].masaPlanIsmi;

            int masaSayisi = 0;

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Button tablebutton = tablePanel.Controls.Find("" + i + j, false)[0] as Button;
                    if (masaDizaynListesi[treeMasaPlanName.SelectedNode.Index].masaYerleri[i][j] != null)
                    {
                        tablebutton.Text = masaDizaynListesi[treeMasaPlanName.SelectedNode.Index].masaYerleri[i][j];
                        tablebutton.Visible = true;
                        tablebutton.ForeColor = SystemColors.ActiveCaption;
                        masaSayisi++;
                    }
                    else
                    {
                        tablebutton.Text = "";
                        tablebutton.Visible = false;
                    }
                    tablebutton.BackColor = Color.White;
                }
            }
            numericTableCount.Value = masaSayisi;
            newTableForm.Text = textTableDesignName.Text;

            if (treeMasaPlanName.Nodes.Count > 1)
                buttonDeleteTable.Enabled = true;

            newTableForm.Enabled = false;
            buttonAddTableDesign.Enabled = true;
            buttonEditDesign.Enabled = true;
            treeMasaPlanName.Focus();
            editingDesign = false;
        }

        //Yeni Masa Planını Kaydet
        private void buttonAddNewTableDesign(object sender, EventArgs e)
        {
            if (textTableDesignName.Text == "Yeni Masa Planı" || textTableDesignName.Text == "")
            {
                using (KontrolFormu dialog = new KontrolFormu("İzin verilmeyen bir masa planı ismi girdiniz, lütfen kontrol edin", false))
                {
                    dialog.ShowDialog();
                }
                return;
            }

            //Yeni masa planını kaydet tuşu. ekle tuşuna basıp bilgileri girdikten sonra kaydete basıyoruz önce girilen bilgilerin doğruluğu
            //kontrol edilir daha sonra tablepanel ve textboxtaki bilgiler xmle ve ana ekrana
            if (newTableForm.Text == "Yeni Masa Planı")
            {
                treeMasaPlanName.Nodes.Add(textTableDesignName.Text);

                //yeni bir dizayn oluşturulur, bilgiler tablepanelden ve textboxtan alındıktan sonra listeye eklenir
                MasaDizayn newTableDesign = new MasaDizayn();
                newTableDesign.masaPlanIsmi = textTableDesignName.Text;

                string[][] refresher3 = new string[][]
                {
                    new string[] {null, null, null, null, null, null, null},
                    new string[] {null, null, null, null, null, null, null},
                    new string[] {null, null, null, null, null, null, null},
                    new string[] {null, null, null, null, null, null, null},
                    new string[] {null, null, null, null, null, null, null},
                    new string[] {null, null, null, null, null, null, null}
                };

                newTableDesign.masaYerleri = refresher3;

                foreach (Button tablebutton in tablePanel.Controls)
                {
                    if (tablebutton.Text != "")
                    {
                        newTableDesign.masaYerleri[tablePanel.GetRow(tablebutton)][tablePanel.GetColumn(tablebutton)] = tablebutton.Text;
                    }
                }

                masaDizaynListesi.Add(newTableDesign);

                //liste kaydedilir
                XmlSave.SaveRestoran(masaDizaynListesi, "masaDizayn.xml");

                //seçilen node olarak son eklenen node gösterilir
                treeMasaPlanName.SelectedNode = treeMasaPlanName.Nodes[treeMasaPlanName.Nodes.Count - 1];

                //masaların initial ismi girilen dizayn ismine göre, ilk 2 kelimesinin ilk harfleriyle belirlenir
                string buttonText = "";
                Array.ForEach(newTableDesign.masaPlanIsmi.Split(' '), s => buttonText += s[0]);

                //dizayn ismi tek kelimeden oluşması durumunda ismin ilk harfi alınır
                if (buttonText.Length > 2)
                    buttonText = buttonText[0].ToString().ToUpper() + buttonText[1].ToString().ToUpper();
                else
                    buttonText = buttonText.ToUpper();

                int masaSayisi = 1;

                //masaların bilgileri tablepanele girilir 
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        Button tablebutton = tablePanel.Controls.Find("" + i + j, false)[0] as Button;
                        if (masaDizaynListesi[treeMasaPlanName.SelectedNode.Index].masaYerleri[i][j] != null)
                        {
                            tablebutton.Text = buttonText + masaSayisi;
                            masaDizaynListesi[treeMasaPlanName.SelectedNode.Index].masaYerleri[i][j] = tablebutton.Text;
                            tablebutton.Visible = true;
                            tablebutton.ForeColor = SystemColors.ActiveCaption;
                            masaSayisi++;
                        }
                        else
                        {
                            masaDizaynListesi[treeMasaPlanName.SelectedNode.Index].masaYerleri[i][j] = null;
                            tablebutton.Text = "";
                            tablebutton.Visible = false;
                        }
                        tablebutton.BackColor = Color.White;
                    }
                }

                //liste kaydedilir
                XmlSave.SaveRestoran(masaDizaynListesi, "masaDizayn.xml");

                treeMasaPlanName.Focus();

                //dizayn sayısının arttığı bilgisi settingse girilir
                masaDizaynSayisi++;
                Properties.Settings.Default.masaDizaynSayisi = masaDizaynSayisi;
                Properties.Settings.Default.Save();


                //eğer 1den fazla dizayn olmuşsa silme butonu aktif hale getirilir
                if (treeMasaPlanName.Nodes.Count > 1)
                    buttonDeleteTable.Enabled = true;

                //eğer 10dan fazla dizayn olmuşsa ekleme butonu devredışı duruma  getirilir
                if (treeMasaPlanName.Nodes.Count > 9)
                    buttonAddTableDesign.Enabled = false;
            }
            else
            {
                //Masa dizaynında değişiklik yapıldıktan sonra basılan kaydet butonu.
                // Girilen bilgilerin doğruluğu kontrol edilir daha sonra tablepanel ve textboxtaki bilgiler xmle aktarılır ve dizayn ismi treeviewda güncellenir.
                masaDizaynListesi[treeMasaPlanName.SelectedNode.Index].masaPlanIsmi = textTableDesignName.Text;

                //tablepaneldeki buton bilgileri alınarak listedeki seçili dizaynın değerleri güncellenir 
                foreach (Button tablebutton in tablePanel.Controls)
                {
                    if (tablebutton.Text != "")
                    {
                        masaDizaynListesi[treeMasaPlanName.SelectedNode.Index].masaYerleri[tablePanel.GetRow(tablebutton)][tablePanel.GetColumn(tablebutton)] = tablebutton.Text;
                    }
                    else
                    {
                        masaDizaynListesi[treeMasaPlanName.SelectedNode.Index].masaYerleri[tablePanel.GetRow(tablebutton)][tablePanel.GetColumn(tablebutton)] = null;
                    }
                }

                //liste kaydedilir
                XmlSave.SaveRestoran(masaDizaynListesi, "masaDizayn.xml");

                // ekranda görünen dizayn ismi güncellenir
                treeMasaPlanName.Nodes[treeMasaPlanName.SelectedNode.Index].Text = textTableDesignName.Text;

                //ekranda görünen butonların bilgileri eklenir
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        Button tablebutton = tablePanel.Controls.Find("" + i + j, false)[0] as Button;
                        if (masaDizaynListesi[treeMasaPlanName.SelectedNode.Index].masaYerleri[i][j] != null)
                        {
                            tablebutton.Text = masaDizaynListesi[treeMasaPlanName.SelectedNode.Index].masaYerleri[i][j];
                            tablebutton.Visible = true;
                            tablebutton.ForeColor = SystemColors.ActiveCaption;
                        }
                        else
                        {
                            tablebutton.Text = "";
                            tablebutton.Visible = false;
                        }
                        tablebutton.BackColor = Color.White;
                    }
                }
            }
            //kısıtlanan butonların kısıtları kaldırılır, düzen ekranı devredışı bırakılır, düzenlemenin bittiği belirtilir
            newTableForm.Text = textTableDesignName.Text;
            buttonAddTableDesign.Enabled = true;
            buttonEditDesign.Enabled = true;
            newTableForm.Enabled = false;
            editingDesign = false;

            //Nodeların eklenmesinden sonra taşma varsa bile ekrana sığması için font boyutunu küçültüyoruz
            foreach (TreeNode node in treeMasaPlanName.Nodes)
            {
                while (treeMasaPlanName.Width - 12 < System.Windows.Forms.TextRenderer.MeasureText(node.Text, new Font(treeMasaPlanName.Font.FontFamily, treeMasaPlanName.Font.Size, treeMasaPlanName.Font.Style)).Width)
                {
                    treeMasaPlanName.Font = new Font(treeMasaPlanName.Font.FontFamily, treeMasaPlanName.Font.Size - 0.5f, treeMasaPlanName.Font.Style);
                }
            }
        }

        //düzenleme butonu
        private void editDesignPressed(object sender, EventArgs e)
        {
            //zaten düzenleme yapılıyorsa birşey yapılmaz
            if (!editingDesign)
            {
                textTableName.Enabled = false;
                editingDesign = true;
                buttonAddTableDesign.Enabled = false;
                newTableForm.Enabled = true;

                //butonların görünümleri düzenleme durumuna getirilir
                foreach (Button masabutton in tablePanel.Controls)
                {
                    if (masabutton.Text != "")
                    {
                        masabutton.BackColor = SystemColors.ActiveCaption;
                        if (masabutton.Text == "")
                            masabutton.Text = "Seçildi";
                        masabutton.ForeColor = Color.White;
                    }
                    else
                    {
                        masabutton.Visible = true;
                        masabutton.Text = "";
                    }
                }
            }
        }

        //tablepaneldeki masa butonlarına basılırsa
        private void buttonTablePressed(object sender, EventArgs e)
        {
            //eğer düzenleme durumundaysa butonların görünüşleri ayarlanır ve yeni masa eklendiğinde masa sayısı arttırılır, çıkarılıncada azaltılır
            if (editingDesign)
            {
                if (((Button)sender).BackColor == Color.White)
                {
                    ((Button)sender).BackColor = SystemColors.ActiveCaption;
                    if (((Button)sender).Text == "")
                        ((Button)sender).Text = "Seçildi";
                    ((Button)sender).ForeColor = Color.White;
                    numericTableCount.Value++;
                }
                else
                {
                    ((Button)sender).BackColor = Color.White;
                    ((Button)sender).Text = "";
                    numericTableCount.Value--;
                }
            }
            else // düzenleme durumunda değilse masanın isminin değiştirilmesi istendiğinde düzenleme durumunda değilken masaya basılır ve ismi textTableName textboxından değiştirilir
            {
                textTableName.Enabled = false;
                textTableName.Text = ((Button)sender).Text;
                textTableName.Tag = ((Button)sender).Name;
                textTableName.Enabled = true;
                textTableName.Focus();
            }
        }

        //her hangi bir masanını ismi değiştiğinde bunun kaydedilmesini bildiren bool shouldsavetablename değişkeni true yapılır.
        private void TableName_TextChanged(object sender, EventArgs e)
        {
            if (textTableName.Enabled)
            {
                Button tablebutton = tablePanel.Controls.Find("" + textTableName.Tag, false)[0] as Button;
                tablebutton.Text = textTableName.Text;
                masaDizaynListesi[treeMasaPlanName.SelectedNode.Index].masaYerleri[Convert.ToInt32(textTableName.Tag) / 10][Convert.ToInt32(textTableName.Tag) % 10] = textTableName.Text;
                shouldSaveTableName = true;
            }
        }

        private void Leaving(object sender, EventArgs e)
        {
            // eğer kayıt edilmesi gereken masa ismi varsa kaydedilir
            if (shouldSaveTableName)
            {
                XmlSave.SaveRestoran(masaDizaynListesi, "masaDizayn.xml");
                shouldSaveTableName = false;
                textTableName.Enabled = false;
            }
        }
    }
}