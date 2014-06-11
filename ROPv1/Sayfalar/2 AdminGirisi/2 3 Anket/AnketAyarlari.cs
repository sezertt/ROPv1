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
using System.Data.SqlClient;

namespace ROPv1
{
    public partial class AnketAyarlari : UserControl
    {
        public AnketAyarlari()
        {
            InitializeComponent();
        }

        private void Raporlar_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT EtkiYuzdesi,Soru,SorununSirasi FROM AnketSorular WHERE SoruAktifMi=1");
            SqlDataReader dr = cmd.ExecuteReader();

            int i = 18, j = 0, k = 0;
            while (dr.Read())
            {
                decimal yuzde = dr.GetDecimal(0);
                string soru = dr.GetString(1);
                int sorununSirasi = dr.GetInt32(2);

                if (sorununSirasi < 16) // textboxsoru
                {
                    (this.Controls.Find("textBoxSoru" + sorununSirasi, true).FirstOrDefault() as TextBox).Text = soru;
                    (this.Controls.Find("numericSoru" + sorununSirasi, true).FirstOrDefault() as NumericUpDown).Value = yuzde;
                    j++;
                }
                else if (sorununSirasi < 18) // textboxyazi 
                {
                    (this.Controls.Find("textBoxYazi" + sorununSirasi, true).FirstOrDefault() as TextBox).Text = soru;
                    (this.Controls.Find("numericYazi" + sorununSirasi, true).FirstOrDefault() as NumericUpDown).Value = yuzde;
                    k++;
                }
                else //textbox
                {
                    (this.Controls.Find("textBox" + i, true).FirstOrDefault() as TextBox).Text = soru;
                    i++;
                }
            }

            numericSecmeliSoruSayisi.Value = j;
            numericYaziliSoruSayisi.Value = k;

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        private void secmeliSoruSayisi_ValueChanged(object sender, EventArgs e)
        {
            int secmeliSoruAdedi = Convert.ToInt32(numericSecmeliSoruSayisi.Value);

            foreach (NumericUpDown puan in this.Controls.OfType<NumericUpDown>())
            {
                if (puan.Name != "numericSecmeliSoruSayisi" && puan.Name != "numericYaziliSoruSayisi")
                {
                    int kacinci = Convert.ToInt32(System.Text.RegularExpressions.Regex.Match(puan.Name, @"\d+").Value);
                    if (kacinci <= 15) // secmeli soru
                    {
                        if (kacinci <= secmeliSoruAdedi)
                        {
                            puan.Enabled = true;
                        }
                        else
                        {
                            puan.Enabled = false;
                        }
                    }
                }
            }

            foreach (TextBox puan in this.Controls.OfType<TextBox>())
            {
                int kacinci = Convert.ToInt32(System.Text.RegularExpressions.Regex.Match(puan.Name, @"\d+").Value);
                if (kacinci <= 15) // secmeli soru
                {
                    if (kacinci <= secmeliSoruAdedi)
                    {
                        puan.Enabled = true;
                    }
                    else
                    {
                        puan.Enabled = false;
                    }
                }
            }

            if (secmeliSoruAdedi == 0 && Convert.ToInt32(numericYaziliSoruSayisi.Value) == 0)
            {
                numericSecmeliSoruSayisi.Value = 1;
                textBoxSoru1.Enabled = true;
                numericSoru1.Enabled = true;

            }
        }

        private void yaziliSoruSayisi_ValueChanged(object sender, EventArgs e)
        {
            int yaziliSoruAdedi = Convert.ToInt32(numericYaziliSoruSayisi.Value);
            if (yaziliSoruAdedi == 2)
            {
                textBoxYazi16.Enabled = true;
                textBoxYazi17.Enabled = true;
                numericYazi16.Enabled = true;
                numericYazi17.Enabled = true;
            }
            else if (yaziliSoruAdedi == 1)
            {
                textBoxYazi16.Enabled = true;
                textBoxYazi17.Enabled = false;
                numericYazi16.Enabled = true;
                numericYazi17.Enabled = false;
            }
            else if (Convert.ToInt32(numericSecmeliSoruSayisi.Value) > 0)
            {
                textBoxYazi16.Enabled = false;
                textBoxYazi17.Enabled = false;
                numericYazi16.Enabled = false;
                numericYazi17.Enabled = false;
            }
            else
            {
                numericYaziliSoruSayisi.Value = 1;
                textBoxYazi16.Enabled = true;
                textBoxYazi17.Enabled = false;
                numericYazi16.Enabled = true;
                numericYazi17.Enabled = false;
            }

        }

        private void buttonKaydet_Click(object sender, EventArgs e)
        {
            decimal toplam = 0;

            int secmeliSoruAdedi = Convert.ToInt32(numericSecmeliSoruSayisi.Value);
            int yaziliSoruAdedi = Convert.ToInt32(numericYaziliSoruSayisi.Value);
            int alinacakSecmeliSoruAdedi = secmeliSoruAdedi;
            int alinacakYaziliSoruAdedi = yaziliSoruAdedi;

            string[] sorular = new string[22];
            decimal[] puanlandırma = new decimal[22];

            foreach (NumericUpDown puan in this.Controls.OfType<NumericUpDown>())
            {
                if (puan.Name != "numericSecmeliSoruSayisi" && puan.Name != "numericYaziliSoruSayisi")
                {
                    int kacinci = Convert.ToInt32(System.Text.RegularExpressions.Regex.Match(puan.Name, @"\d+").Value);
                    if (kacinci <= 15) // secmeli soru
                    {
                        if (alinacakSecmeliSoruAdedi > 0)
                        {
                            if (kacinci <= secmeliSoruAdedi)
                            {
                                puanlandırma[kacinci - 1] = puan.Value;
                                toplam += puan.Value;
                                alinacakSecmeliSoruAdedi--;
                            }
                        }
                    }
                    else // yazılı soru
                    {
                        if (alinacakYaziliSoruAdedi > 0)
                        {
                            if (yaziliSoruAdedi == 1)
                            {
                                if (kacinci == 16)
                                {
                                    toplam += puan.Value;
                                    alinacakYaziliSoruAdedi--;
                                }
                            }
                            else
                            {
                                toplam += puan.Value;
                                alinacakYaziliSoruAdedi--;
                            }
                            puanlandırma[kacinci - 1] = puan.Value;
                        }
                    }
                    if (alinacakYaziliSoruAdedi == 0 && alinacakSecmeliSoruAdedi == 0)
                        break;
                }
            }

            if (toplam > 100)
            {
                KontrolFormu dialog = new KontrolFormu("Puanlandırma toplamı %100'den fazla(%" + toplam + "), lütfen düzeltiniz.", false);
                dialog.Show();
                return;
            }
            else if (toplam < 100)
            {
                KontrolFormu dialog = new KontrolFormu("Puanlandırma toplamı %100'den az(%" + toplam + "), lütfen düzeltiniz.", false);
                dialog.Show();
                return;
            }

            foreach (TextBox puan in this.Controls.OfType<TextBox>())
            {
                int kacinci = Convert.ToInt32(System.Text.RegularExpressions.Regex.Match(puan.Name, @"\d+").Value);
                if (kacinci <= 15) // secmeli soru
                {
                    if (kacinci <= secmeliSoruAdedi)
                    {
                        sorular[kacinci - 1] = puan.Text;
                    }
                    else
                    {
                        sorular[kacinci - 1] = "";
                    }
                }
                else if (kacinci < 18) // yazili soru
                {
                    if (yaziliSoruAdedi == 1)
                    {
                        if (kacinci == 16)
                        {
                            sorular[kacinci - 1] = puan.Text;
                            sorular[kacinci] = "";
                        }
                    }
                    else if (yaziliSoruAdedi == 2)
                    {
                        sorular[kacinci - 1] = puan.Text;
                    }
                    else
                    {
                        sorular[kacinci - 1] = "";
                        sorular[kacinci] = "";
                    }
                }
                else
                {
                    if (puan.Text == "")
                    {
                        KontrolFormu dialog = new KontrolFormu("Şıklar boş bırakılamaz", false);
                        dialog.Show();
                        return;
                    }
                    sorular[kacinci - 1] = puan.Text;
                }
            }

            SqlCommand cmd;

            for (int i = 0; i < 22; i++)
            {
                if (i < numericSecmeliSoruSayisi.Value)// seçmeli sorular
                {
                    cmd = SQLBaglantisi.getCommand("IF EXISTS (SELECT * FROM AnketSorular WHERE Soru=@_Soru1 AND SorununSirasi<16) UPDATE AnketSorular SET EtkiYuzdesi=@_EtkiYuzdesi1, SoruAktifMi=1, SorununSirasi=@_sorununSirasi1 WHERE Soru=@_Soru2 AND SorununSirasi<16 ELSE INSERT INTO AnketSorular(EtkiYuzdesi,Soru,SoruAktifMi,SorununSirasi) VALUES(@_EtkiYuzdesi2,@_Soru3,1,@_SorununSirasi2)");

                    cmd.Parameters.AddWithValue("@_Soru1", sorular[i]);
                    cmd.Parameters.AddWithValue("@_EtkiYuzdesi1", puanlandırma[i]);
                    cmd.Parameters.AddWithValue("@_sorununSirasi1", i + 1);

                    cmd.Parameters.AddWithValue("@_Soru2", sorular[i]);

                    cmd.Parameters.AddWithValue("@_EtkiYuzdesi2", puanlandırma[i]);
                    cmd.Parameters.AddWithValue("@_Soru3", sorular[i]);
                    cmd.Parameters.AddWithValue("@_sorununSirasi2", i + 1);
                    cmd.ExecuteNonQuery();

                }
                else if (i < numericYaziliSoruSayisi.Value + 15 && i > 14) // yazılı sorular
                {
                    cmd = SQLBaglantisi.getCommand("IF EXISTS (SELECT * FROM AnketSorular WHERE Soru=@_Soru1 AND SorununSirasi>15) UPDATE AnketSorular SET EtkiYuzdesi=@_EtkiYuzdesi1, SoruAktifMi=1, SorununSirasi=@_sorununSirasi1 WHERE Soru=@_Soru2 AND SorununSirasi>15 ELSE INSERT INTO AnketSorular(EtkiYuzdesi,Soru,SoruAktifMi,SorununSirasi) VALUES(@_EtkiYuzdesi2,@_Soru3,1,@_SorununSirasi2)");

                    cmd.Parameters.AddWithValue("@_Soru1", sorular[i]);
                    cmd.Parameters.AddWithValue("@_EtkiYuzdesi1", puanlandırma[i]);
                    cmd.Parameters.AddWithValue("@_sorununSirasi1", i + 1);

                    cmd.Parameters.AddWithValue("@_Soru2", sorular[i]);

                    cmd.Parameters.AddWithValue("@_EtkiYuzdesi2", puanlandırma[i]);
                    cmd.Parameters.AddWithValue("@_Soru3", sorular[i]);
                    cmd.Parameters.AddWithValue("@_sorununSirasi2", i + 1);
                    cmd.ExecuteNonQuery();

                }
                else if(i > 16)// şıklar
                {
                    cmd = SQLBaglantisi.getCommand("IF EXISTS (SELECT * FROM AnketSorular WHERE SorununSirasi=@_sorununSirasi1) UPDATE AnketSorular SET Soru=@_Soru WHERE SorununSirasi=@_sorununSirasi2 ELSE INSERT INTO AnketSorular(EtkiYuzdesi,Soru,SoruAktifMi,SorununSirasi) VALUES(0,@_Soru,1,@_SorununSirasi3)");

                    cmd.Parameters.AddWithValue("@_sorununSirasi1", i + 1);
                    cmd.Parameters.AddWithValue("@_sorununSirasi2", i + 1);

                    cmd.Parameters.AddWithValue("@_Soru", sorular[i]);
                    cmd.Parameters.AddWithValue("@_sorununSirasi3", i + 1);

                    cmd.ExecuteNonQuery();
                }
            }

            cmd = SQLBaglantisi.getCommand("UPDATE AnketSorular SET SoruAktifMi=0 WHERE (SorununSirasi>@_SecmelilerinSirasi AND SorununSirasi<16) OR (SorununSirasi>@_YazililarinSirasi AND SorununSirasi<18)");

            cmd.Parameters.AddWithValue("@_SecmelilerinSirasi", numericSecmeliSoruSayisi.Value);

            cmd.Parameters.AddWithValue("@_YazililarinSirasi", numericYaziliSoruSayisi.Value + 15);
            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            KontrolFormu dialog2 = new KontrolFormu("Değişiklikler kaydedildi", false);
            dialog2.Show();
        }
    }
}