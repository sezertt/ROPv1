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
    public partial class YaziciFormu : Form
    {
        HesapFormu hesapFormu;
        string[] yaziciAdi;
        bool yazdir = true;
        List<string[]> digerYazicilar, adisyonYazicilari;
        AdisyonGoruntuleme adisyonFormu;

        public YaziciFormu(HesapFormu hesapFormu, List<string[]> digerYazicilar, List<string[]> adisyonYazicilari = null)
        {
            InitializeComponent();

            this.hesapFormu = hesapFormu;
            this.digerYazicilar = digerYazicilar;
            this.adisyonYazicilari = adisyonYazicilari;

            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
        }

        public YaziciFormu(AdisyonGoruntuleme adisyonFormu, List<string[]> digerYazicilar, List<string[]> adisyonYazicilari = null)
        {
            InitializeComponent();

            this.adisyonFormu = adisyonFormu;
            this.digerYazicilar = digerYazicilar;
            this.adisyonYazicilari = adisyonYazicilari;

            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
        }

        private void yaziciButonuBasildi(object sender, EventArgs e)
        {
            if ((sender as Button).Name == "a")
            {
                yaziciAdi = adisyonYazicilari[Convert.ToInt32((sender as Button).Tag)];
            }
            else
            {
                yaziciAdi = digerYazicilar[Convert.ToInt32((sender as Button).Tag)];
            }
            this.Close();
        }

        private void checkBoxSave_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                for (int i = 0; i < digerYazicilar.Count; i++)
                {
                    Button buttonTable = new Button();
                    buttonTable.Text = digerYazicilar[i][0];

                    buttonTable.ForeColor = SystemColors.ActiveCaption;
                    buttonTable.BackColor = Color.White;

                    buttonTable.UseVisualStyleBackColor = false;

                    buttonTable.Font = new Font("Arial", 21.75F, FontStyle.Bold);
                    buttonTable.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    buttonTable.Click += yaziciButonuBasildi;
                    tablePanel.Controls.Add(buttonTable);
                    buttonTable.Name = "d";
                    buttonTable.Tag = "" + i;
                }
            }
            else
            {
                foreach (Control ctrl in tablePanel.Controls)
                {
                    if ((ctrl as Button).Name == "d")
                        tablePanel.Controls.Remove(ctrl);
                }
            }
        }

        private void YaziciFormu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (yazdir)
            {
                if (hesapFormu != null)
                    hesapFormu.yazdir(yaziciAdi);
                else if (adisyonFormu != null)
                    adisyonFormu.yazdir(yaziciAdi);
            }
        }

        private void buttonNO_Click(object sender, EventArgs e)
        {
            yazdir = false;
            if (hesapFormu != null)
                hesapFormu.yaziciForm = null;
            else if (adisyonFormu != null)
                adisyonFormu.yazdir(yaziciAdi);
            
            this.Close();
        }

        private void YaziciFormu_Load(object sender, EventArgs e)
        {
            switch (adisyonYazicilari.Count + digerYazicilar.Count)
            {
                case 1:
                    tablePanel.RowCount = 1;
                    tablePanel.ColumnCount = 1;
                    break;
                case 2:
                    tablePanel.RowCount = 1;
                    tablePanel.ColumnCount = 2;
                    break;
                case 3:
                case 4:
                    tablePanel.RowCount = 2;
                    tablePanel.ColumnCount = 2;
                    break;
                case 5:
                case 6:
                    tablePanel.RowCount = 3;
                    tablePanel.ColumnCount = 2;
                    break;
                case 7:
                case 8:
                case 9:
                    tablePanel.RowCount = 3;
                    tablePanel.ColumnCount = 3;
                    break;
                case 10:
                case 11:
                case 12:
                    tablePanel.RowCount = 4;
                    tablePanel.ColumnCount = 3;
                    break;
            }

            if (adisyonYazicilari != null)
            {
                for (int i = 0; i < adisyonYazicilari.Count; i++)
                {
                    Button buttonTable = new Button();
                    buttonTable.Text = adisyonYazicilari[i][0];

                    buttonTable.ForeColor = SystemColors.ActiveCaption;
                    buttonTable.BackColor = Color.White;

                    buttonTable.UseVisualStyleBackColor = false;

                    buttonTable.Font = new Font("Arial", 21.75F, FontStyle.Bold);
                    buttonTable.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    buttonTable.Click += yaziciButonuBasildi;
                    tablePanel.Controls.Add(buttonTable);
                    buttonTable.Name = "a";
                    buttonTable.Tag = "" + i;
                }
            }
            else if (digerYazicilar != null)
            {
                for (int i = 0; i < digerYazicilar.Count; i++)
                {
                    Button buttonTable = new Button();
                    buttonTable.Text = digerYazicilar[i][0];

                    buttonTable.ForeColor = SystemColors.ActiveCaption;
                    buttonTable.BackColor = Color.White;

                    buttonTable.UseVisualStyleBackColor = false;

                    buttonTable.Font = new Font("Arial", 21.75F, FontStyle.Bold);
                    buttonTable.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    buttonTable.Click += yaziciButonuBasildi;
                    tablePanel.Controls.Add(buttonTable);
                    buttonTable.Name = "d";
                    buttonTable.Tag = "" + i;
                }
            }
        }
    }
}