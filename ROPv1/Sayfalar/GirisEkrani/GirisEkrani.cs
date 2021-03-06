﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Globalization;
using System.Collections.Specialized;
using System.Data.SqlClient;
using SPIA;
using SPIA.Server;
using System.Threading;
using ROPv1.CrystalReportsAnaRaporlar;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Security;
using System.Net.NetworkInformation;
using CrystalDecisions.CrystalReports.Engine;
using ROPv1.Sayfalar.CallerId;

namespace ROPv1
{
    public partial class GirisEkrani : Form
    {
        const int adet = 12;

        double gecenSure = 0;

        List<KategorilerineGoreUrunler> urunListesi = new List<KategorilerineGoreUrunler>();

        // SPIA kütüphanesindeki sunucu nesnesi        
        private SPIAServer sunucu;

        // Sunucuya bağlı olan kullanıcıları saklayan liste        
        private List<BagliKullanicilar> kullanicilar;

        public List<string> son50Mesaj;

        public WPF_UserControls.VerticalCenterTextBox userNameTextBox;
        public WPF_UserControls.VerticalCenterPasswordBox passwordTextBox;

        CrystalReportMutfak raporMutfak = new CrystalReportMutfak();

        CrystalReportAdisyon raporAdisyon = new CrystalReportAdisyon();

        CrystalReportMutfakUrunIptal raporMutfakIptal = new CrystalReportMutfakUrunIptal();

        public SiparisMasaFormu siparisForm;

        public AdminGirisFormu adminForm;

        public CallerIDFormu frmCallerId;

        string siparisiKimGirdi, adisyonNotu;

        UItemp[] infoKullanici;

        public GirisEkrani()
        {
            kullanicilar = new List<BagliKullanicilar>();
            son50Mesaj = new List<string>();
            InitializeComponent();
        }

        // SPIA sunucusunu durdurur        
        public void durdur()
        {
            if (sunucu != null)
            {
                sunucu.Durdur();
                sunucu = null;
            }
        }

        // SPIA sunucusunu başlatır        
        private bool baslat()
        {
            //Port numarasını Settings'den al
            int port = 0;
            try
            {
                port = Convert.ToInt32(Properties.Settings.Default.Port);
                if (port <= 0)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            //Kullanıcı listesini temizle
            kullanicilar.Clear();

            //Sunucuyu oluştur, olaylarına kaydol ve başlat
            sunucu = new SPIAServer(port);
            sunucu.ClientdanYeniMesajAlindi += new dgClientdanYeniMesajAlindi(sunucu_ClientdenYeniMesajAlindi);
            sunucu.ClientBaglantisiKapatildi += new dgClientBaglantisiKapatildi(sunucu_ClientBaglantisiKapatildi);
            sunucu.Baslat();

            return true;
        }

        private void sunucu_ClientBaglantisiKapatildi(ClientBaglantiArgumanlari e)
        {
            Invoke(new dgClientBaglantisiKapatildi(clientKapandi), e);
        }

        private void sunucu_ClientdenYeniMesajAlindi(ClientdanMesajAlmaArgumanlari e)
        {
            Invoke(new dgClientdanYeniMesajAlindi(mesajAlindi), e);
        }

        // Bir client bağlantısı kapatıldığında ilgili olay bu fonksyonu çağırır        
        private void clientKapandi(ClientBaglantiArgumanlari e)
        {
            komut_cikis(e.Client);
        }

        // Bir clientden mesaj alındığında ilgili olay bu fonksyonu çağırır        
        private void mesajAlindi(ClientdanMesajAlmaArgumanlari e)
        {
            //Gelen mesajı & ve = işaretlerine göre ayrıştır
            NameValueCollection parametreler = mesajCoz(e.Mesaj);
            //Ayrıştırma başarısızsa çık
            if (parametreler == null || parametreler.Count < 1)
            {
                return;
            }
            //Ayrıştırma sonucunda komuta göre gerekli işlemleri yap
            try
            {
                switch (parametreler["komut"])
                {
                    case "siparis":
                        komut_siparis(parametreler["masa"], parametreler["departmanAdi"], parametreler["miktar"], parametreler["yemekAdi"], parametreler["siparisiGirenKisi"], parametreler["dusulecekDeger"], e.Client, parametreler["adisyonNotu"], parametreler["sonSiparisMi"], parametreler["porsiyon"], parametreler["tur"], parametreler["ilkSiparis"], parametreler["porsiyonSinifi"]);
                        break;
                    case "iptal": // ürün iptal edildiği bilgisini dağıtmak için
                        komut_iptal(parametreler["masa"], parametreler["departmanAdi"], parametreler["miktar"], parametreler["yemekAdi"], parametreler["siparisiGirenKisi"], parametreler["dusulecekDeger"], e.Client, parametreler["adisyonNotu"], parametreler["ikramYeniMiEskiMi"], parametreler["porsiyon"], parametreler["tur"], parametreler["iptalNedeni"]);
                        break;
                    case "hesapOdeniyor": // yeni masa açıldığı bilgisi geldiğinde
                        komut_hesapOdeniyor(parametreler["masa"], parametreler["departmanAdi"]);
                        break;
                    case "OdemeBitti": // yeni masa açıldığı bilgisi geldiğinde
                        komut_hesapOdemeBitti(parametreler["masa"], parametreler["departmanAdi"], parametreler["odenmeyenSiparisVarMi"]);
                        break;
                    case "AdisyonYazdir": // ikramın iptal edildiği bilgisini dağıtmak için
                        komut_adisyonYazdir(parametreler["masa"], parametreler["departmanAdi"], parametreler["garson"], parametreler["yazdirilacakIndirim"], parametreler["acilisZamani"], parametreler["firmaAdi"], parametreler["firmaAdresTelefon"], parametreler["yaziciWindowsAdi"], parametreler["odenenMiktar"]);
                        break;
                    case "Indirim": // yeni masa açıldığı bilgisi geldiğinde
                        komut_hesapIndirim(parametreler["masa"], parametreler["departmanAdi"], parametreler["odemeTipi"], parametreler["odemeMiktari"], e.Client, parametreler["indirimYapanKisi"]);
                        break;
                    case "bildirim":
                        komut_bildirim(parametreler["masalar"], e.Client);
                        break;
                    case "bildirimGoruldu":
                        komut_bildirimGoruldu(parametreler["masa"], parametreler["departmanAdi"], parametreler["yemekAdi"], parametreler["adedi"], parametreler["porsiyonu"]);
                        break;
                    case "GarsonIstendi":
                    case "HesapIstendi":
                    case "TemizlikIstendi":
                        komut_OzelBildirim(parametreler["masa"], parametreler["departmanAdi"], parametreler["komut"], e.Client, parametreler["kalanHesap"]);
                        break;
                    case "GarsonGoruldu":
                    case "HesapGoruldu":
                    case "TemizlikGoruldu":
                        komut_OzelBildirimGoruldu(parametreler["masa"], parametreler["departmanAdi"], parametreler["komut"]);
                        break;
                    case "masaGirilebilirMi":
                        komut_masaGirilebilirMi(parametreler["masa"], parametreler["departmanAdi"], e.Client);
                        break;
                    case "masaDegistirTablet":
                    case "masaDegistir": // Masa değiştirmek ve bu bilgiyi diğer kullanıcılara bildirmek için
                        komut_masaDegistir(parametreler["yeniMasa"], parametreler["yeniDepartmanAdi"], parametreler["eskiMasa"], parametreler["eskiDepartmanAdi"], parametreler["yapilmasiGereken"], parametreler["komut"], e.Client);
                        break;
                    case "ikram": // ürün ikram edildiği bilgisini dağıtmak için
                        komut_ikram(parametreler["masa"], parametreler["departmanAdi"], parametreler["miktar"], parametreler["yemekAdi"], parametreler["siparisiGirenKisi"], parametreler["dusulecekDeger"], e.Client, parametreler["adisyonNotu"], parametreler["porsiyon"], parametreler["tur"]);
                        break;
                    case "ikramIptal": // ikramın iptal edildiği bilgisini dağıtmak için
                        komut_ikramIptal(parametreler["masa"], parametreler["departmanAdi"], parametreler["miktar"], parametreler["yemekAdi"], parametreler["siparisiGirenKisi"], parametreler["dusulecekDeger"], e.Client, parametreler["adisyonNotu"], parametreler["ikramYeniMiEskiMi"], parametreler["porsiyon"], parametreler["tur"]);
                        break;
                    case "urunuTasiTablet":
                    case "urunuTasi":
                        komut_urunuTasi(parametreler["masa"], parametreler["departmanAdi"], parametreler["yeniMasa"], parametreler["yeniDepartmanAdi"], parametreler["siparisiGirenKisi"], parametreler["aktarmaBilgileri"], e.Client);
                        break;
                    case "OdemeYapildi": // herhangi bir ödeme yapıldığında
                        komut_OdemeYapildi(parametreler["masa"], parametreler["departmanAdi"], parametreler["odemeTipi"], parametreler["odemeMiktari"], e.Client, parametreler["secilipOdenenSiparisBilgileri"], parametreler["odemeyiAlanKisi"]);
                        break;
                    case "OdemeGuncelle": // Ödeme güncellendiğinde
                        komut_OdemeGuncelle(parametreler["masa"], parametreler["departmanAdi"], parametreler["odemeler"], parametreler["gelenOdemeler"], e.Client, parametreler["siparisiGirenKisi"]);
                        break;
                    case "masayiAc":
                        komut_masayiAc(parametreler["masa"], parametreler["departmanAdi"]);
                        break;
                    case "giris": // bir kullanıcı servera bağlandığında
                        komut_giris(e.Client, parametreler["nick"]);
                        break;
                    case "YaziciIstegi": // bir kullanıcı servera bağlandığında
                        komut_yaziciGonder(e.Client, parametreler["masa"], parametreler["departmanAdi"]);
                        break;
                    case "LoadSiparis": // bir kullanıcı menü ekranını açmak istediğinde masada verilen siparişleri aktarmak için
                        komut_loadSiparis(e.Client, parametreler["masa"], parametreler["departmanAdi"]);
                        break;
                    case "OdenenleriGonder": // bir kullanıcı menü ekranını açmak istediğinde masada verilen siparişleri aktarmak için
                        komut_OdenenleriGonder(e.Client, parametreler["masa"], parametreler["departmanAdi"]);
                        break;
                    case "OdemeBilgileriGuncelleTablet":
                    case "OdemeBilgileriTablet":
                        komut_OdemeBilgileriTablet(e.Client, parametreler["masa"], parametreler["departmanAdi"], parametreler["komut"]);
                        break;
                    case "AdisyonNotu": // adisyon notu değiştirileceğinde eski adisyon notunu göstermek için
                        komut_adisyonNotu(e.Client, parametreler["masa"], parametreler["departmanAdi"]);
                        break;
                    case "departmanMasaSecimiIcin": // ürün taşıma için              
                    case "departman"://departmanın masaları hakkında bilgi    
                    case "departmanMasaTasimaIcin":
                        komut_departman(e.Client, parametreler["departmanAdi"], parametreler["komut"], parametreler["masaDepartman"]);
                        break;
                    case "anketIstegi":// anket isteği geldiğinde
                        komut_anketIstegi(e.Client);
                        break;
                    case "anketCevaplari": // anket cevapları geldiğinde
                        komut_anketCevaplari(parametreler["kullaniciBilgileri"], parametreler["cevapBilgileri"], parametreler["soruBilgileri"]); // anket cevapları ve kullanıcı bilgileri
                        break;
                    case "cikis": // bir kullanıcı serverdan çıktığında
                        komut_cikis(e.Client);
                        break;
                    case "masaAcildi": // yeni bir masa açıldığı bilgisini dağıtmak için
                        komut_masaAcildi(parametreler["masa"], parametreler["departmanAdi"]);
                        break;
                    case "masaKapandi": // bir masa kapandığı bilgisini dağıtmak için
                        komut_masaKapandi(parametreler["masa"], parametreler["departmanAdi"]);
                        break;
                    case "listeBos":
                        komut_listeBos(parametreler["masa"], parametreler["departmanAdi"]);
                        break;
                    case "adisyonNotunuGuncelle":
                        komut_adisyonNotunuGuncelle(parametreler["masa"], parametreler["departmanAdi"], parametreler["adisyonNotu"]);
                        break;
                    case "masaGirilebilir":
                        komut_masaGirilebilir(parametreler["masa"], parametreler["departmanAdi"]);
                        break;
                    case "veriGonder":
                        komut_veriGonder(e.Client, parametreler["kacinci"], parametreler["sadeceXML"]);
                        break;
                }
            }
            catch
            {
                //parametre hatalı istenilen işlem yapılamadı hatası ver
                if (e.Client != null)
                {
                    komut_IslemHatasi(e.Client, "İstenilen işlem gerçekleştirilemedi, lütfen tekrar deneyiniz");
                }
            }

            //Mesajı 'Son Gelen 50 Mesaj' listesinde en başa ekle
            son50Mesaj.Insert(0, "" + e.Mesaj);
            //Listedeki mesaj sayısı 50'i geçmişse sondan sil.
            if (son50Mesaj.Count > 50)
            {
                son50Mesaj.RemoveAt(50);
            }
        }

        #region Komutlar

        private void komut_bildirimGoruldu(string masa, string departmanAdi, string yemekAdi, string adedi, string porsiyonu)
        {
            SqlCommand cmd;

            if (yemekAdi == "hepsi")
            {
                cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET NotificationGorulduMu=@_NotificationGorulduMu FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.MasaAdi=@_MasaAdi AND Adisyon.DepartmanAdi=@_DepartmanAdi AND Adisyon.IptalMi=0 AND Siparis.IptalMi=0 AND Adisyon.AcikMi=1");
                cmd.Parameters.AddWithValue("@_NotificationGorulduMu", 1);
                cmd.Parameters.AddWithValue("@_MasaAdi", masa);
                cmd.Parameters.AddWithValue("@_DepartmanAdi", departmanAdi);

                cmd.ExecuteNonQuery();

                cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET HesapIstendi=0, TemizlikIstendi=0, GarsonIstendi=0 WHERE Adisyon.MasaAdi=@_MasaAdi AND Adisyon.DepartmanAdi=@_DepartmanAdi AND IptalMi=0 AND AcikMi=1");
                cmd.Parameters.AddWithValue("@_MasaAdi", masa);
                cmd.Parameters.AddWithValue("@_DepartmanAdi", departmanAdi);

                cmd.ExecuteNonQuery();
            }
            else
            {
                cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET NotificationGorulduMu=@_NotificationGorulduMu FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE YemekAdi=@_YemekAdi AND Adet=@_Adet AND Porsiyon=CONVERT(DECIMAL(5,2),@_Porsiyon) AND Adisyon.MasaAdi=@_MasaAdi AND Adisyon.DepartmanAdi=@_DepartmanAdi AND Adisyon.IptalMi=0 AND Siparis.IptalMi=0 AND Adisyon.AcikMi=1");
                cmd.Parameters.AddWithValue("@_NotificationGorulduMu", 1);
                cmd.Parameters.AddWithValue("@_YemekAdi", yemekAdi);
                cmd.Parameters.AddWithValue("@_Adet", Convert.ToInt32(adedi));
                cmd.Parameters.AddWithValue("@_Porsiyon", porsiyonu);
                cmd.Parameters.AddWithValue("@_MasaAdi", masa);
                cmd.Parameters.AddWithValue("@_DepartmanAdi", departmanAdi);

                cmd.ExecuteNonQuery();
            }

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            tumKullanicilaraMesajYolla("komut=bildirimGoruldu&masa=" + masa + "&departmanAdi=" + departmanAdi + "&yemekAdi=" + yemekAdi + "&adedi=" + adedi + "&porsiyonu=" + porsiyonu);
        }

        private void komut_OzelBildirim(string masa, string departmanAdi, string komut, ClientRef client, string kalanHesap = null)
        {
            SqlCommand cmd;

            if (komut == "HesapIstendi")
            {
                cmd = SQLBaglantisi.getCommand("SELECT OdemeYapiliyor FROM Adisyon WHERE AcikMi=1 AND IptalMi=0 AND MasaAdi=@_MasaAdi AND DepartmanAdi=@_DepartmanAdi");
                cmd.Parameters.AddWithValue("@_MasaAdi", masa);
                cmd.Parameters.AddWithValue("@_DepartmanAdi", departmanAdi);

                SqlDataReader dr = cmd.ExecuteReader();

                dr.Read();
                try
                {
                    if (dr.GetBoolean(0))
                    {
                        client.MesajYolla("komut=hesapIslemde");
                        cmd.Connection.Close();
                        cmd.Connection.Dispose();
                        return;
                    }
                }
                catch
                { }
            }

            cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET " + komut + "=1 WHERE MasaAdi=@_MasaAdi AND DepartmanAdi=@_DepartmanAdi AND AcikMi=1 AND IptalMi=0");
            cmd.Parameters.AddWithValue("@_MasaAdi", masa);
            cmd.Parameters.AddWithValue("@_DepartmanAdi", departmanAdi);

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();


            if (kalanHesap != null && Convert.ToDecimal(kalanHesap.Replace('.', ',')) == 0)
            {
                //masayı kapat
                if (siparisForm != null)
                {
                    if (siparisForm.viewdakiDepartmaninAdi != null)
                    {
                        if (siparisForm.viewdakiDepartmaninAdi == departmanAdi)
                        {
                            Button tablebutton = siparisForm.tablePanel.Controls[masa] as Button;
                            tablebutton.ForeColor = SystemColors.ActiveCaption;
                            tablebutton.BackColor = Color.White;

                            if (siparisForm.hangiMasaButonunaBasildi != null)
                            {
                                if (siparisForm.hangiMasaButonunaBasildi.Text == masa)
                                {
                                    if (siparisForm.siparisMenuForm != null)
                                    {
                                        if (siparisForm.siparisMenuForm.hesapForm != null)
                                        {
                                            this.Invoke((MethodInvoker)delegate
                                            {
                                                siparisForm.siparisMenuForm.hesapForm.Close();
                                            });
                                        }

                                        this.Invoke((MethodInvoker)delegate
                                        {
                                            siparisForm.siparisMenuForm.Close();
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
                komut_listeBos(masa, departmanAdi);
                //Tüm kullanıcılara kapatılmak istenilen masayı gönderelim
                tumKullanicilaraMesajYolla("komut=masaKapandi&masa=" + masa + "&departmanAdi=" + departmanAdi);
            }
            else
            {
                tumKullanicilaraMesajYolla("komut=" + komut + "&masa=" + masa + "&departmanAdi=" + departmanAdi);
            }
        }

        private void komut_OzelBildirimGoruldu(string masa, string departmanAdi, string komut)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET " + komut.Replace("Goruldu", "Istendi") + "=0 WHERE MasaAdi=@_MasaAdi AND DepartmanAdi=@_DepartmanAdi AND AcikMi=1 AND IptalMi=0");
            cmd.Parameters.AddWithValue("@_MasaAdi", masa);
            cmd.Parameters.AddWithValue("@_DepartmanAdi", departmanAdi);

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            tumKullanicilaraMesajYolla("komut=" + komut + "&masa=" + masa + "&departmanAdi=" + departmanAdi);
        }

        private void komut_bildirim(string masalar, ClientRef client)
        {
            string[] departmanVeMasalar;
            string[][] masaBilgileri;

            if (masalar != "hepsi")
            {
                try
                {
                    departmanVeMasalar = masalar.Split('*');

                    masaBilgileri = new string[departmanVeMasalar.Count()][];

                    for (int i = 0; i < departmanVeMasalar.Count(); i++)
                    {
                        masaBilgileri[i] = new string[departmanVeMasalar[i].Count()];
                        masaBilgileri[i] = departmanVeMasalar[0].Split('-');
                    }
                }
                catch
                {
                    return;
                }

                SqlCommand cmd = null;
                StringBuilder bildirimBilgileri = null;
                StringBuilder ozelBildirimBilgileri = null;

                for (int i = 0; i < masaBilgileri.Count(); i++)
                {
                    for (int j = 1; j < masaBilgileri[i].Count(); j++)
                    {
                        bildirimBilgileri = new StringBuilder();
                        cmd = SQLBaglantisi.getCommand("SELECT Fiyatı, Adet, YemekAdi, Porsiyon from Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.MasaAdi=@_MasaAdi and Adisyon.DepartmanAdi=@_DepartmanAdi AND Siparis.NotificationGorulduMu=0 AND Siparis.IkramMi=0 AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 ORDER BY Adet DESC");
                        cmd.Parameters.AddWithValue("@_MasaAdi", masaBilgileri[i][j]);
                        cmd.Parameters.AddWithValue("@_DepartmanAdi", masaBilgileri[i][0]);

                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            try
                            {
                                bildirimBilgileri.Append("*" + masaBilgileri[i][0] + "-" + masaBilgileri[i][j] + "-" + dr.GetDecimal(0).ToString() + "-" + dr.GetInt32(1).ToString() + "-" + dr.GetString(2) + "-" + dr.GetDecimal(3));
                            }
                            catch
                            {
                                //HATA MESAJI GÖNDER
                                komut_IslemHatasi(client, "İşlem gerçekleştirilemedi, lütfen tekrar deneyiniz");
                                bildirimBilgileri.Clear();
                                return;
                            }
                        }
                        ozelBildirimBilgileri = new StringBuilder();

                        cmd = SQLBaglantisi.getCommand("SELECT DepartmanAdi, MasaAdi, HesapIstendi, TemizlikIstendi, GarsonIstendi from Adisyon WHERE AcikMi=1 AND IptalMi=0 AND (HesapIstendi=1 OR TemizlikIstendi=1 OR GarsonIstendi=1) AND MasaAdi=@_MasaAdi and DepartmanAdi=@_DepartmanAdi");
                        cmd.Parameters.AddWithValue("@_MasaAdi", masaBilgileri[i][j]);
                        cmd.Parameters.AddWithValue("@_DepartmanAdi", masaBilgileri[i][0]);

                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            try
                            {
                                int ozelBildirim = 0;

                                if (dr.GetBoolean(2)) //HesapIstendi ==> 1
                                {
                                    ozelBildirim += 1;
                                }

                                if (dr.GetBoolean(3)) //TemizlikIstendi ==> 2
                                {
                                    ozelBildirim += 2;
                                }

                                if (dr.GetBoolean(4)) //GarsonIstendi ==> 4
                                {
                                    ozelBildirim += 4;
                                }

                                // 3 ==> Hesap + Temizlik        5 ==> Hesap + Garson       6 ==> Temizlik + Garson      7 ==> Hepsi

                                ozelBildirimBilgileri.Append("*" + ozelBildirim + "-" + dr.GetString(0) + "-" + dr.GetString(1));
                            }
                            catch
                            {
                                //HATA MESAJI GÖNDER
                                komut_IslemHatasi(client, "İşlem gerçekleştirilemedi, lütfen tekrar deneyiniz");
                                ozelBildirimBilgileri.Clear();
                                return;
                            }
                        }
                    }
                }

                cmd.Connection.Close();
                cmd.Connection.Dispose();

                if (bildirimBilgileri != null)
                {
                    if (bildirimBilgileri.Length >= 1)
                    {
                        bildirimBilgileri.Remove(0, 1);
                    }
                }
                if (ozelBildirimBilgileri != null)
                {
                    if (ozelBildirimBilgileri.Length >= 1)
                    {
                        ozelBildirimBilgileri.Remove(0, 1);
                    }
                }
                client.MesajYolla("komut=bildirimBilgileri&bildirimBilgileri=" + bildirimBilgileri + "&ozelBildirimBilgileri=" + ozelBildirimBilgileri);
            }
            else
            {
                SqlCommand cmd = null;
                StringBuilder bildirimBilgileri = null;

                bildirimBilgileri = new StringBuilder();
                cmd = SQLBaglantisi.getCommand("SELECT Fiyatı, Adet, YemekAdi, Porsiyon, DepartmanAdi, MasaAdi from Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Siparis.NotificationGorulduMu=0 AND Siparis.IkramMi=0 AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 ORDER BY Adet DESC");
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    try
                    {
                        bildirimBilgileri.Append("*" + dr.GetString(4) + "-" + dr.GetString(5) + "-" + dr.GetDecimal(0).ToString() + "-" + dr.GetInt32(1).ToString() + "-" + dr.GetString(2) + "-" + dr.GetDecimal(3));
                    }
                    catch
                    {
                        //HATA MESAJI GÖNDER
                        komut_IslemHatasi(client, "İşlem gerçekleştirilemedi, lütfen tekrar deneyiniz");
                        bildirimBilgileri.Clear();
                        return;
                    }
                }

                StringBuilder ozelBildirimBilgileri = null;

                ozelBildirimBilgileri = new StringBuilder();

                cmd = SQLBaglantisi.getCommand("SELECT DepartmanAdi, MasaAdi, HesapIstendi, TemizlikIstendi, GarsonIstendi from Adisyon WHERE AcikMi=1 AND IptalMi=0 AND (HesapIstendi=1 OR TemizlikIstendi=1 OR GarsonIstendi=1)");
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    try
                    {
                        int ozelBildirim = 0;

                        if (dr.GetBoolean(2)) //HesapIstendi ==> 1
                        {
                            ozelBildirim += 1;
                        }

                        if (dr.GetBoolean(3)) //TemizlikIstendi ==> 2
                        {
                            ozelBildirim += 2;
                        }

                        if (dr.GetBoolean(4)) //GarsonIstendi ==> 4
                        {
                            ozelBildirim += 4;
                        }

                        // 3 ==> Hesap + Temizlik        5 ==> Hesap + Garson       6 ==> Temizlik + Garson      7 ==> Hepsi

                        ozelBildirimBilgileri.Append("*" + ozelBildirim + "-" + dr.GetString(0) + "-" + dr.GetString(1));
                    }
                    catch
                    {
                        //HATA MESAJI GÖNDER
                        komut_IslemHatasi(client, "İşlem gerçekleştirilemedi, lütfen tekrar deneyiniz");
                        ozelBildirimBilgileri.Clear();
                        return;
                    }
                }

                cmd.Connection.Close();
                cmd.Connection.Dispose();

                if (bildirimBilgileri != null)
                {
                    if (bildirimBilgileri.Length >= 1)
                    {
                        bildirimBilgileri.Remove(0, 1);
                    }
                }

                if (ozelBildirimBilgileri != null)
                {
                    if (ozelBildirimBilgileri.Length >= 1)
                    {
                        ozelBildirimBilgileri.Remove(0, 1);
                    }
                }

                client.MesajYolla("komut=bildirimBilgileri&bildirimBilgileri=" + bildirimBilgileri + "&ozelBildirimBilgileri=" + ozelBildirimBilgileri);
            }
        }

        private void komut_veriGonder(ClientRef client, string kacinci, string sadeceXML)
        {
            int kacinciDosya = Convert.ToInt32(kacinci);
            int sadeceXMLMi = Convert.ToInt32(sadeceXML);

            string path2 = Application.StartupPath;

            string[] xmlDosyalari = Directory.GetFiles(path2, "*.xml", SearchOption.TopDirectoryOnly);

            if (sadeceXMLMi == 1)
            {
                if (kacinciDosya > xmlDosyalari.Count())
                {
                    if (adminForm != null)
                        adminForm.veriAktarimiTamamlandi(adminForm.buttonBilgiAktar);

                    client.MesajYolla("komut=aktarimTamamlandi");
                    return;
                }
                client.gonder("<komut=dosyalar&kacinci=" + kacinciDosya + ">", Path.GetFileName(xmlDosyalari[kacinciDosya - 1]), path2 + "\\");
            }
            else
            {
                string image_outputDir = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
                DirectoryInfo df = new DirectoryInfo(Application.StartupPath + @"\resimler\");

                if (!df.Exists) // klasör yoksa oluştur
                {
                    // create new directory
                    DirectoryInfo di = Directory.CreateDirectory(image_outputDir + @"\resimler\");
                }

                string path1 = Application.StartupPath + @"\resimler";

                string[] imageFiles = Directory.GetFiles(path1, "*.png", SearchOption.TopDirectoryOnly);

                if (kacinciDosya > imageFiles.Count() + xmlDosyalari.Count())
                {
                    adminForm.veriAktarimiTamamlandi(adminForm.buttonBilgiAktar);
                    client.MesajYolla("komut=aktarimTamamlandi");
                    return;
                }

                if (kacinciDosya <= xmlDosyalari.Count())
                {
                    client.gonder("<komut=dosyalar&kacinci=" + kacinciDosya + ">", Path.GetFileName(xmlDosyalari[kacinciDosya - 1]), path2 + "\\");
                }
                else
                {
                    client.gonder("<komut=dosyalar&kacinci=" + kacinciDosya + ">", Path.GetFileName(imageFiles[kacinciDosya - 1 - xmlDosyalari.Count()]), path1 + "\\");
                }
            }
        }

        // Anket doldurulduktan sonra cevapları gelince çalışacak fonksiyon
        private void komut_anketCevaplari(string gelenKullaniciBilgileri, string gelenCevapBilgileri, string gelenSoruBilgileri) // anket cevapları ve kullanıcı bilgileri
        {
            string[] kullaniciBilgileri, cevapBilgileri, soruBilgileri;
            try
            {
                kullaniciBilgileri = gelenKullaniciBilgileri.Split('*');
                cevapBilgileri = gelenCevapBilgileri.Split('*');
                soruBilgileri = gelenSoruBilgileri.Split('*');
            }
            catch
            {
                return;
            }

            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT KullaniciID FROM AnketKullanicilari WHERE Adi=@_Adi AND SoyAdi=@_SoyAdi AND Eposta=@_Eposta");

            cmd.Parameters.AddWithValue("@_Adi", kullaniciBilgileri[0]);
            cmd.Parameters.AddWithValue("@_SoyAdi", kullaniciBilgileri[1]);
            cmd.Parameters.AddWithValue("@_Eposta", kullaniciBilgileri[2]);

            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            int kullaniciID, anketID;

            try
            {
                kullaniciID = dr.GetInt32(0);
                cmd = SQLBaglantisi.getCommand("UPDATE AnketKullanicilari SET Telefon=@_Telefon WHERE KullaniciID=@_KullaniciID");
                cmd.Parameters.AddWithValue("@_Telefon", kullaniciBilgileri[3]);
                cmd.Parameters.AddWithValue("@_KullaniciID", kullaniciID);

                cmd.ExecuteNonQuery();
            }
            catch
            {
                cmd = SQLBaglantisi.getCommand("INSERT INTO AnketKullanicilari(Adi,SoyAdi,Eposta,Telefon) VALUES(@_Adi,@_SoyAdi,@_Eposta,@_Telefon) SELECT SCOPE_IDENTITY()");

                cmd.Parameters.AddWithValue("@_Adi", kullaniciBilgileri[0]);
                cmd.Parameters.AddWithValue("@_SoyAdi", kullaniciBilgileri[1]);
                cmd.Parameters.AddWithValue("@_Eposta", kullaniciBilgileri[2]);
                cmd.Parameters.AddWithValue("@_Telefon", kullaniciBilgileri[3]);

                kullaniciID = Convert.ToInt32(cmd.ExecuteScalar());
            }

            decimal genelPuan = 0;

            for (int i = 0; i < soruBilgileri.Count(); i++)
            {
                cmd = SQLBaglantisi.getCommand("SELECT EtkiYuzdesi FROM AnketSorular WHERE Soru='" + soruBilgileri[i] + "' AND SorununSirasi<16");
                SqlDataReader dr2 = cmd.ExecuteReader();

                while (dr2.Read())
                {
                    decimal etki = dr2.GetDecimal(0);

                    int cevapDegeri = Convert.ToInt32(Convert.ToDouble(cevapBilgileri[i]));
                    decimal carpan = 0;

                    switch (cevapDegeri)
                    {
                        case 1:
                            carpan = 0;
                            break;
                        case 2:
                            carpan = 0.25M;
                            break;
                        case 3:
                            carpan = 0.5M;
                            break;
                        case 4:
                            carpan = 0.75M;
                            break;
                        case 5:
                            carpan = 1;
                            break;
                        default:
                            carpan = 0;
                            break;
                    }
                    genelPuan += carpan * etki;
                }
            }

            cmd = SQLBaglantisi.getCommand("INSERT INTO Anket(KullaniciID,AnketPuani) VALUES(@_KullaniciID,@_AnketPuani) SELECT SCOPE_IDENTITY()");
            cmd.Parameters.AddWithValue("@_KullaniciID", kullaniciID);
            cmd.Parameters.AddWithValue("@_AnketPuani", genelPuan);

            try
            {
                anketID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch
            {
                return;
            }

            for (int i = 0; i < soruBilgileri.Count(); i++)
            {
                cmd = SQLBaglantisi.getCommand("SELECT SoruID,EtkiYuzdesi,SorununSirasi FROM AnketSorular WHERE Soru='" + soruBilgileri[i] + "'");
                SqlDataReader dr2 = cmd.ExecuteReader();
                dr2.Read();

                int soruID = dr2.GetInt32(0), sorununSirasi = dr2.GetInt32(2);
                decimal etkiYuzdesi = dr2.GetDecimal(1);

                cmd = SQLBaglantisi.getCommand("INSERT INTO AnketCevaplar(AnketID,Cevap,EtkiYuzdesi,SoruID,SorununSirasi) VALUES(@_AnketID,@_Cevap,@_EtkiYuzdesi,@_SoruID,@SorununSirasi)");
                cmd.Parameters.AddWithValue("@_AnketID", anketID);
                cmd.Parameters.AddWithValue("@_Cevap", cevapBilgileri[i]);
                cmd.Parameters.AddWithValue("@_EtkiYuzdesi", etkiYuzdesi);
                cmd.Parameters.AddWithValue("@_SoruID", soruID);
                cmd.Parameters.AddWithValue("@SorununSirasi", sorununSirasi);

                cmd.ExecuteNonQuery();
            }

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        // Anket isteği geldiğinde anket sorularını clienta gönderen fonksiyon
        private void komut_anketIstegi(ClientRef client)
        {
            StringBuilder anketSorulari = new StringBuilder();
            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT Soru,SorununSirasi FROM AnketSorular WHERE SoruAktifMi=1 ORDER BY SorununSirasi ASC");
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                try
                {
                    anketSorulari.Append("*" + dr.GetString(0) + "-" + dr.GetInt32(1));
                }
                catch
                {
                    anketSorulari.Clear();
                }
            }
            cmd.Connection.Close();
            cmd.Connection.Dispose();

            //İlk sorunun başına konulan "*" metnini kaldır
            if (anketSorulari.Length >= 1)
            {
                anketSorulari.Remove(0, 1);
            }

            //Kullanıcıya soruları gönderelim
            client.MesajYolla("komut=anketIstegi&sorular=" + anketSorulari);
        }

        private void komut_yaziciGonder(ClientRef client, string masaAdi, string departmanAdi)
        {
            List<string[]> adisyonYazicilari = new List<string[]>();
            List<string[]> digerYazicilar = new List<string[]>();

            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT YaziciAdi,FirmaAdi,FirmaAdres,Yazici,Telefon FROM Yazici");
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string[] yazici = new string[5];

                yazici[0] = dr.GetString(0); // yazici adı
                yazici[1] = dr.GetString(1); // firma adı
                yazici[2] = dr.GetString(2); // firma adres
                yazici[3] = dr.GetString(3); // yazıcı windows adı
                yazici[4] = dr.GetString(4); // telefon

                try
                {
                    if (yazici[0].Substring(0, 7) == "Adisyon")
                    {
                        adisyonYazicilari.Add(yazici);
                    }
                    else
                    {
                        digerYazicilar.Add(yazici);
                    }
                }
                catch
                {
                    digerYazicilar.Add(yazici);
                }
            }

            // masaya bakan ilk garsonun ismini döndüren sql sorgusu
            cmd = SQLBaglantisi.getCommand("SELECT TOP 1 Garsonu,AcilisZamani FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE MasaAdi='" + masaAdi + "' AND DepartmanAdi='" + departmanAdi + "' AND AcikMi=1 ORDER BY VerilisTarihi ASC");
            dr = cmd.ExecuteReader();
            dr.Read();

            string garson;
            DateTime acilisZamani;

            try // açık
            {
                garson = dr.GetString(0);
                acilisZamani = dr.GetDateTime(1);
            }
            catch
            {
                KontrolFormu dialog = new KontrolFormu("Adisyon bilgileri alınırken hata oluştu, lütfen tekrar deneyiniz", false);
                dialog.Show();
                return;
            }

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            StringBuilder aYazicilari = new StringBuilder(), dYazicilari = new StringBuilder();

            for (int i = 0; i < adisyonYazicilari.Count; i++)
            {
                aYazicilari.Append("*" + adisyonYazicilari[i][0] + "-" + adisyonYazicilari[i][1] + "-" + adisyonYazicilari[i][2] + "-" + adisyonYazicilari[i][3] + "-" + adisyonYazicilari[i][4]);
            }

            for (int i = 0; i < digerYazicilar.Count; i++)
            {
                dYazicilari.Append("*" + digerYazicilar[i][0] + "-" + digerYazicilar[i][1] + "-" + digerYazicilar[i][2] + "-" + digerYazicilar[i][3] + "-" + digerYazicilar[i][4]);
            }

            //baştaki * ı sil
            if (aYazicilari.Length >= 1)
            {
                aYazicilari.Remove(0, 1);
            }

            if (dYazicilari.Length >= 1)
            {
                dYazicilari.Remove(0, 1);
            }

            client.MesajYolla("komut=BulunanYazicilar&adisyonYazicilari=" + aYazicilari + "&digerYazicilar=" + dYazicilari + "&garson=" + garson + "&acilisZamani=" + acilisZamani.ToString());
        }

        private void komut_hesapIndirim(string masa, string departmanAdi, string odemeTipi, string odemeMiktari, ClientRef client, string indirimYapanKisi)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT AdisyonID FROM Adisyon WHERE AcikMi=1 AND MasaAdi='" + masa + "' AND DepartmanAdi='" + departmanAdi + "'");
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            int adisyonID = dr.GetInt32(0);

            if (Convert.ToInt32(odemeTipi) < 101)
            {
                if (indirimYapanKisi != "")
                {
                    cmd = SQLBaglantisi.getCommand("IF EXISTS (SELECT * FROM OdemeDetay WHERE AdisyonID='" + adisyonID + "' AND OdemeTipi='" + odemeTipi + "') UPDATE OdemeDetay SET OdenenMiktar='" + odemeMiktari + "', IndirimiKimGirdi=@_IndirimiKimGirdi WHERE AdisyonID='" + adisyonID + "' AND OdemeTipi='" + odemeTipi + "' ELSE INSERT INTO OdemeDetay(AdisyonID,OdemeTipi,OdenenMiktar,IndirimiKimGirdi2) VALUES(@_AdisyonID,@_OdemeTipi,@_OdenenMiktar,@_IndirimiKimGirdi2)");
                    cmd.Parameters.AddWithValue("@_IndirimiKimGirdi", indirimYapanKisi);
                    cmd.Parameters.AddWithValue("@_IndirimiKimGirdi2", indirimYapanKisi);
                }
                else
                {
                    cmd = SQLBaglantisi.getCommand("IF EXISTS (SELECT * FROM OdemeDetay WHERE AdisyonID='" + adisyonID + "' AND OdemeTipi='" + odemeTipi + "') UPDATE OdemeDetay SET OdenenMiktar='" + odemeMiktari + "' WHERE AdisyonID='" + adisyonID + "' AND OdemeTipi='" + odemeTipi + "' ELSE INSERT INTO OdemeDetay(AdisyonID,OdemeTipi,OdenenMiktar) VALUES(@_AdisyonID,@_OdemeTipi,@_OdenenMiktar)");
                }

            }
            else
            {
                if (indirimYapanKisi != "")
                {
                    cmd = SQLBaglantisi.getCommand("IF EXISTS (SELECT * FROM OdemeDetay WHERE AdisyonID='" + adisyonID + "' AND OdemeTipi<101) UPDATE OdemeDetay SET OdenenMiktar=@_OdenenMiktar2, OdemeTipi=@_OdemeTipi2, IndirimiKimGirdi=@_IndirimiKimGirdi WHERE AdisyonID='" + adisyonID + "' AND OdemeTipi=(SELECT OdemeTipi FROM OdemeDetay WHERE AdisyonID='" + adisyonID + "' AND OdemeTipi<101) ELSE INSERT INTO OdemeDetay(AdisyonID,OdemeTipi,OdenenMiktar,IndirimiKimGirdi) VALUES(@_AdisyonID,@_OdemeTipi,@_OdenenMiktar,@_IndirimiKimGirdi2)");

                    cmd.Parameters.AddWithValue("@_IndirimiKimGirdi", indirimYapanKisi);
                    cmd.Parameters.AddWithValue("@_OdenenMiktar2", Convert.ToDecimal(odemeMiktari));
                    cmd.Parameters.AddWithValue("@_OdemeTipi2", Convert.ToInt32(odemeTipi));
                    cmd.Parameters.AddWithValue("@_IndirimiKimGirdi2", indirimYapanKisi);
                }
                else
                {
                    cmd = SQLBaglantisi.getCommand("IF EXISTS (SELECT * FROM OdemeDetay WHERE AdisyonID='" + adisyonID + "' AND OdemeTipi<101) UPDATE OdemeDetay SET OdenenMiktar=@_OdenenMiktar2, OdemeTipi=@_OdemeTipi2 WHERE AdisyonID='" + adisyonID + "' AND OdemeTipi=(SELECT OdemeTipi FROM OdemeDetay WHERE AdisyonID='" + adisyonID + "' AND OdemeTipi<101) ELSE INSERT INTO OdemeDetay(AdisyonID,OdemeTipi,OdenenMiktar) VALUES(@_AdisyonID,@_OdemeTipi,@_OdenenMiktar)");

                    cmd.Parameters.AddWithValue("@_OdenenMiktar2", Convert.ToDecimal(odemeMiktari));
                    cmd.Parameters.AddWithValue("@_OdemeTipi2", Convert.ToInt32(odemeTipi));
                }
            }

            cmd.Parameters.AddWithValue("@_AdisyonID", adisyonID);
            cmd.Parameters.AddWithValue("@_OdemeTipi", Convert.ToInt32(odemeTipi));
            cmd.Parameters.AddWithValue("@_OdenenMiktar", Convert.ToDecimal(odemeMiktari));

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            client.MesajYolla("komut=IndirimOnay&odemeTipi=" + odemeTipi + "&odemeMiktari=" + odemeMiktari);

            tumKullanicilaraMesajYolla("komut=OdemeIndirimOnayTablet&masaAdi=" + masa + "&departmanAdi=" + departmanAdi);

        }

        private void komut_OdemeGuncelle(string masa, string departmanAdi, string _odemeler, string _gelenOdemeler, ClientRef client, string siparisiGirenKisi)
        {
            string[] stringOdemeler, stringGelenOdemeler;
            try
            {
                stringOdemeler = _odemeler.Split('*');
                stringGelenOdemeler = _gelenOdemeler.Split('*');
            }
            catch
            {
                //HATA MESAJI GÖNDER
                komut_IslemHatasi(client, "İşlem gerçekleştirilemedi, lütfen tekrar deneyiniz");
                return;
            }

            decimal[] odemeler = { 0, 0, 0 }, gelenOdemeler = { 0, 0, 0 };

            odemeler[0] = Convert.ToDecimal(stringOdemeler[0]);
            odemeler[1] = Convert.ToDecimal(stringOdemeler[1]);
            odemeler[2] = Convert.ToDecimal(stringOdemeler[2]);

            gelenOdemeler[0] = Convert.ToDecimal(stringGelenOdemeler[0]);
            gelenOdemeler[1] = Convert.ToDecimal(stringGelenOdemeler[1]);
            gelenOdemeler[2] = Convert.ToDecimal(stringGelenOdemeler[2]);

            // adisyon id al 
            int adisyonID;
            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT AdisyonID FROM Adisyon WHERE MasaAdi='" + masa + "' AND DepartmanAdi='" + departmanAdi + "' AND AcikMi=1");
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            try // açık
            {
                adisyonID = dr.GetInt32(0);
            }
            catch// kapalı
            {
                cmd.Connection.Close();
                cmd.Connection.Dispose();
                KontrolFormu dialog = new KontrolFormu("Ödeme bilgileri kaydedilirken bir hata oluştu, lütfen tekrar deneyiniz", false);
                dialog.Show();
                return;
            }

            decimal odenenMiktar = 0;

            for (int i = 0; i < odemeler.Length; i++)
            {
                if (odemeler[i] != gelenOdemeler[i]) // değişen ödemeleri güncelle
                {
                    odenenMiktar = gelenOdemeler[i];
                    int odemeTipi = 101 + i;

                    cmd = SQLBaglantisi.getCommand("IF EXISTS (SELECT * FROM OdemeDetay WHERE AdisyonID=@_AdisyonID1 AND OdemeTipi=@_OdemeTipi1) UPDATE OdemeDetay SET OdenenMiktar=@_OdenenMiktar1, IndirimiKimGirdi=@_IndirimiKimGirdi1 WHERE OdemeTipi=@_OdemeTipi2 AND AdisyonID=@_AdisyonID2 ELSE INSERT INTO OdemeDetay(AdisyonID,OdemeTipi,OdenenMiktar,IndirimiKimGirdi) VALUES(@_AdisyonID3,@_OdemeTipi2,@_OdenenMiktar2,@_IndirimiKimGirdi2)");

                    cmd.Parameters.AddWithValue("@_AdisyonID1", adisyonID);
                    cmd.Parameters.AddWithValue("@_OdemeTipi1", odemeTipi);
                    cmd.Parameters.AddWithValue("@_OdenenMiktar1", odenenMiktar);
                    cmd.Parameters.AddWithValue("@_IndirimiKimGirdi1", siparisiGirenKisi);

                    cmd.Parameters.AddWithValue("@_OdemeTipi2", odemeTipi);
                    cmd.Parameters.AddWithValue("@_AdisyonID2", adisyonID);

                    cmd.Parameters.AddWithValue("@_AdisyonID3", adisyonID);
                    cmd.Parameters.AddWithValue("@_OdemeTipi2", odemeTipi);
                    cmd.Parameters.AddWithValue("@_OdenenMiktar2", odenenMiktar);
                    cmd.Parameters.AddWithValue("@_IndirimiKimGirdi2", siparisiGirenKisi);
                    cmd.ExecuteNonQuery();

                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                }
            }

            client.MesajYolla("komut=odemeGuncelleTamamlandi&odemeler=" + odemeler[0] + "*" + odemeler[1] + "*" + odemeler[2] + "&gelenOdemeler=" + gelenOdemeler[0] + "*" + gelenOdemeler[1] + "*" + gelenOdemeler[2] + "&siparisiGirenKisi=" + siparisiGirenKisi);

            tumKullanicilaraMesajYolla("komut=OdemeIndirimOnayTablet&masaAdi=" + masa + "&departmanAdi=" + departmanAdi);
        }

        private void komut_OdemeYapildi(string masa, string departmanAdi, string odemeTipi, string odemeMiktari, ClientRef client, string secilipOdenenSiparisBilgileri, string odemeyiAlanKisi)
        {
            string[] siparisler;
            try
            {
                siparisler = secilipOdenenSiparisBilgileri.Split('*');
            }
            catch
            {
                //HATA MESAJI GÖNDER
                komut_IslemHatasi(client, "İşlem gerçekleştirilemedi, lütfen tekrar deneyiniz");
                return;
            }

            SqlCommand cmd;

            int adisyonID = -1;

            if (siparisler.Count() > 0 && siparisler[0] != "")
            {
                int gelenAdet;
                string yemeginAdi, tur;
                decimal yemeginFiyati, porsiyon;
                bool turBool = false;

                for (int i = 0; i < siparisler.Count(); i++)
                {
                    string[] detaylari = siparisler[i].Split('-');
                    try
                    {
                        gelenAdet = Convert.ToInt32(detaylari[0]);
                        yemeginAdi = detaylari[1];
                        yemeginFiyati = Convert.ToDecimal(detaylari[2]);
                        porsiyon = Convert.ToDecimal(detaylari[3]);
                        tur = Convert.ToString(detaylari[4]);

                        if (tur == "K")
                            turBool = true;
                    }
                    catch
                    {
                        break;
                    }

                    cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adisyon.AdisyonID,Adet,Siparis.VerilisTarihi,Siparis.Garsonu,NotificationGorulduMu FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi=0 AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi=@_MasaAdi AND Adisyon.DepartmanAdi=@_DepartmanAdi' AND Siparis.YemekAdi=@_YemekAdi AND Siparis.Porsiyon=CONVERT(DECIMAL(5,2),@_Porsiyon) AND Siparis.KiloSatisiMi=@_Tur ORDER BY Adet DESC");


                    cmd.Parameters.AddWithValue("@_MasaAdi", masa);
                    cmd.Parameters.AddWithValue("@_DepartmanAdi", departmanAdi);
                    cmd.Parameters.AddWithValue("@_YemekAdi", yemeginAdi);
                    cmd.Parameters.AddWithValue("@_Porsiyon", porsiyon);
                    cmd.Parameters.AddWithValue("@_Tur", turBool);

                    SqlDataReader dr = cmd.ExecuteReader();

                    int siparisID, adet;
                    DateTime verilisTarihi;
                    bool NotificationGorulduMu;
                    while (dr.Read())
                    {
                        try
                        {
                            siparisID = dr.GetInt32(0);
                            adisyonID = dr.GetInt32(1);
                            adet = dr.GetInt32(2);
                            verilisTarihi = dr.GetDateTime(3);
                            siparisiKimGirdi = dr.GetString(4);
                            NotificationGorulduMu = dr.GetBoolean(5);
                            cmd.Connection.Close();
                            cmd.Connection.Dispose();
                        }
                        catch
                        {
                            cmd.Connection.Close();
                            cmd.Connection.Dispose();
                            //HATA MESAJI GÖNDER
                            komut_IslemHatasi(client, "Ödeme işlemi gerçekleşirken hata oluştu, lütfen tekrar deneyiniz");
                            return;
                        }

                        if (adet < gelenAdet) // ödenmesi istenenlerin sayısı(kacPorsiyon) ödenebileceklerden(porsiyon) küçükse
                        {
                            odendiUpdateTam(siparisID);

                            gelenAdet -= adet;
                        }
                        else if (adet > gelenAdet) // den büyükse
                        {
                            odendiUpdateInsert(siparisID, adisyonID, adet, (double)yemeginFiyati, gelenAdet, yemeginAdi, verilisTarihi, porsiyon, turBool, NotificationGorulduMu);

                            gelenAdet = 0;
                        }
                        else // elimizde ikram edilmemişler ikramı istenene eşitse
                        {
                            odendiUpdateTam(siparisID);

                            gelenAdet = 0;
                        }
                        if (gelenAdet == 0)
                            break;
                    }
                }
            }

            if (adisyonID == -1)
            {
                cmd = SQLBaglantisi.getCommand("SELECT AdisyonID FROM Adisyon WHERE AcikMi=1 AND MasaAdi='" + masa + "' AND DepartmanAdi='" + departmanAdi + "'");
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                adisyonID = dr.GetInt32(0);
                cmd.Connection.Close();
                cmd.Connection.Dispose();
            }

            cmd = SQLBaglantisi.getCommand("IF EXISTS (SELECT * FROM OdemeDetay WHERE AdisyonID=@_AdisyonID1 AND OdemeTipi=@_OdemeTipi1) UPDATE OdemeDetay SET OdenenMiktar=OdenenMiktar+@_OdenenMiktar1, IndirimiKimGirdi=@_IndirimiKimGirdi1 WHERE OdemeTipi=@_OdemeTipi2 AND AdisyonID=@_AdisyonID2 ELSE INSERT INTO OdemeDetay(AdisyonID,OdemeTipi,OdenenMiktar,IndirimiKimGirdi) VALUES(@_AdisyonID3,@_OdemeTipi3,@_OdenenMiktar2,@_IndirimiKimGirdi2)");

            cmd.Parameters.AddWithValue("@_AdisyonID1", adisyonID);
            cmd.Parameters.AddWithValue("@_OdemeTipi1", Convert.ToInt32(odemeTipi));
            cmd.Parameters.AddWithValue("@_OdenenMiktar1", Convert.ToDecimal(odemeMiktari));
            cmd.Parameters.AddWithValue("@_IndirimiKimGirdi1", odemeyiAlanKisi);

            cmd.Parameters.AddWithValue("@_OdemeTipi2", Convert.ToInt32(odemeTipi));
            cmd.Parameters.AddWithValue("@_AdisyonID2", adisyonID);

            cmd.Parameters.AddWithValue("@_AdisyonID3", adisyonID);
            cmd.Parameters.AddWithValue("@_OdemeTipi3", Convert.ToInt32(odemeTipi));
            cmd.Parameters.AddWithValue("@_OdenenMiktar2", Convert.ToDecimal(odemeMiktari));
            cmd.Parameters.AddWithValue("@_IndirimiKimGirdi2", odemeyiAlanKisi);

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            client.MesajYolla("komut=OdemeOnay&odemeTipi=" + odemeTipi + "&odemeMiktari=" + odemeMiktari + "&secilipOdenenSiparisBilgileri=" + secilipOdenenSiparisBilgileri);

            tumKullanicilaraMesajYolla("komut=OdemeIndirimOnayTablet&masaAdi=" + masa + "&departmanAdi=" + departmanAdi);
        }

        //yapılan ödemelerden ödenen ürünler çıkarılarak seçilmeden ödenen miktar bulunur ve gönderilir
        private void komut_OdemeBilgileriTablet(ClientRef client, string masa, string departmanAdi, string komut)
        {
            decimal alinanOdemeler = 0, odenenUrunler = 0, indirimler = 0;

            StringBuilder odenenUrunBilgileri = new StringBuilder();

            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT Fiyatı, Adet, Porsiyon, YemekAdi, KiloSatisiMi from Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.MasaAdi='" + masa + "' and Adisyon.DepartmanAdi='" + departmanAdi + "' and Siparis.IptalMi=0 AND Siparis.OdendiMi=1 AND Siparis.IkramMi=0 AND Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 ORDER BY Adet DESC");
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                try
                {
                    odenenUrunler += dr.GetDecimal(0) * dr.GetInt32(1);
                    //fiyat - adet -porsiyon - yemekadi - kiloSatışıMı
                    odenenUrunBilgileri.Append("*" + dr.GetDecimal(0).ToString() + "-" + dr.GetInt32(1).ToString() + "-" + dr.GetDecimal(2) + "-" + dr.GetString(3) + "-" + dr.GetBoolean(4));
                }
                catch
                {
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                    //HATA MESAJI GÖNDER
                    komut_IslemHatasi(client, "İşlem gerçekleştirilemedi, lütfen tekrar deneyiniz");
                    return;
                }
            }

            if (odenenUrunBilgileri.Length >= 1)
            {
                odenenUrunBilgileri.Remove(0, 1);
            }

            cmd = SQLBaglantisi.getCommand("SELECT OdemeTipi, OdenenMiktar from OdemeDetay JOIN Adisyon ON OdemeDetay.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.MasaAdi='" + masa + "' AND Adisyon.DepartmanAdi='" + departmanAdi + "' AND Adisyon.AcikMi=1 AND Adisyon.IptalMi=0");
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                try
                {
                    int odemeTipi = dr.GetInt32(0);
                    if (odemeTipi == 104 || odemeTipi < 101)
                    {
                        indirimler += dr.GetDecimal(1);
                    }
                    else
                    {
                        alinanOdemeler += dr.GetDecimal(1);
                    }
                }
                catch
                {
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                    //HATA MESAJI GÖNDER
                    komut_IslemHatasi(client, "İşlem gerçekleştirilemedi, lütfen tekrar deneyiniz");
                    return;
                }
            }

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            alinanOdemeler -= odenenUrunler;

            if (komut == "OdemeBilgileriGuncelleTablet" && odenenUrunBilgileri.Length >= 1)
            {
                client.MesajYolla("komut=" + komut + "&alinanOdemeler=" + alinanOdemeler + "&indirimler=" + indirimler + "&odenenUrunBilgileri=" + odenenUrunBilgileri);

            }
            else
            {
                client.MesajYolla("komut=" + komut + "&alinanOdemeler=" + alinanOdemeler + "&indirimler=" + indirimler);
            }
        }

        //hesap ekranı load olurken ödenen siparişlere dair bilgileri gönderen method
        private void komut_OdenenleriGonder(ClientRef client, string masa, string departmanAdi)
        {
            StringBuilder siparisBilgileri = new StringBuilder();
            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT Fiyatı, Adet, YemekAdi, Porsiyon, KiloSatisiMi from Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.MasaAdi='" + masa + "' and Adisyon.DepartmanAdi='" + departmanAdi + "' and Siparis.IptalMi=0 AND Siparis.OdendiMi=1 AND Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 ORDER BY Adet DESC");
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                try
                {
                    siparisBilgileri.Append("*" + dr.GetInt32(0).ToString() + "-" + dr.GetDecimal(1).ToString() + "-" + dr.GetString(2) + "-" + dr.GetDecimal(3) + "-" + dr.GetBoolean(4));
                }
                catch
                {
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                    //HATA MESAJI GÖNDER
                    komut_IslemHatasi(client, "İşlem gerçekleştirilemedi, lütfen tekrar deneyiniz");
                    siparisBilgileri.Clear();
                    return;
                }
            }

            StringBuilder odemeBilgileri = new StringBuilder();
            cmd = SQLBaglantisi.getCommand("SELECT OdemeTipi, OdenenMiktar from OdemeDetay JOIN Adisyon ON OdemeDetay.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.MasaAdi='" + masa + "' AND Adisyon.DepartmanAdi='" + departmanAdi + "' AND Adisyon.AcikMi=1 AND Adisyon.IptalMi=0");
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                try
                {
                    odemeBilgileri.Append("*" + dr.GetInt32(0).ToString() + "-" + dr.GetDecimal(1).ToString());
                }
                catch
                {
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                    //HATA MESAJI GÖNDER
                    komut_IslemHatasi(client, "İşlem gerçekleştirilemedi, lütfen tekrar deneyiniz");
                    odemeBilgileri.Clear();
                    return;
                }
            }

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            if (odemeBilgileri.Length >= 1)
            {
                odemeBilgileri.Remove(0, 1);
            }

            if (siparisBilgileri.Length >= 1)
            {
                siparisBilgileri.Remove(0, 1);
            }

            client.MesajYolla("komut=OdenenleriGonder&siparisBilgileri=" + siparisBilgileri + "&odemeBilgileri=" + odemeBilgileri);
        }

        private void komut_masaGirilebilirMi(string masa, string departmanAdi, ClientRef client)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT OdemeYapiliyor FROM Adisyon WHERE AcikMi=1 AND IptalMi=0 AND MasaAdi=@_MasaAdi AND DepartmanAdi=@_DepartmanAdi");
            cmd.Parameters.AddWithValue("@_MasaAdi", masa);
            cmd.Parameters.AddWithValue("@_DepartmanAdi", departmanAdi);

            SqlDataReader dr = cmd.ExecuteReader();

            dr.Read();
            bool masaSerbestMi = false;
            try
            {
                masaSerbestMi = dr.GetBoolean(0);
            }
            catch
            {
                masaSerbestMi = false;
            }

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            client.MesajYolla("komut=masaGirilebilirMi&cevap=" + !masaSerbestMi);    // masaSebestMi nin ! ini yollarız çünkü hesap alınıyorsa true gelicek alınmıyorsa yani masa serbestse false gelicek
        }

        private void komut_hesapOdemeBitti(string masa, string departmanAdi, string odenmeyenSiparisVarMi)
        {
            //masanın adisyonundaki odemeyapiliyor bilgisini güncelle
            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET OdemeYapiliyor=@_OdemeYapiliyor WHERE AdisyonID=(SELECT AdisyonID FROM Adisyon WHERE IptalMi=0 AND AcikMi=1 AND MasaAdi=@_MasaAdi AND DepartmanAdi=@_DepartmanAdi)");
            cmd.Parameters.AddWithValue("@_MasaAdi", masa);
            cmd.Parameters.AddWithValue("@_DepartmanAdi", departmanAdi);
            cmd.Parameters.AddWithValue("@_OdemeYapiliyor", 0);
            cmd.ExecuteNonQuery();

            if (odenmeyenSiparisVarMi == "0") // ödenmemiş sipariş yoksa siparişleri ödendi yap
            {
                cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET OdendiMi=1 WHERE Siparis.IptalMi=0 AND AdisyonID=(SELECT AdisyonID FROM Adisyon WHERE IptalMi=0 AND AcikMi=1 AND MasaAdi='" + masa + "' AND DepartmanAdi='" + departmanAdi + "')");
                cmd.ExecuteNonQuery();
            }

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        private void komut_hesapOdeniyor(string masa, string departmanAdi)
        {
            //masanın adisyonundaki odemeyapiliyor bilgisini güncelle
            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET OdemeYapiliyor=@_OdemeYapiliyor WHERE AdisyonID=(SELECT AdisyonID FROM Adisyon WHERE IptalMi=0 AND AcikMi=1 AND MasaAdi=@_MasaAdi AND DepartmanAdi=@_DepartmanAdi)");
            cmd.Parameters.AddWithValue("@_MasaAdi", masa);
            cmd.Parameters.AddWithValue("@_DepartmanAdi", departmanAdi);
            cmd.Parameters.AddWithValue("@_OdemeYapiliyor", 1);
            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            //eğer hesabı ödenmeye başlanan masa serverda açıksa
            if (siparisForm != null)
            {
                if (siparisForm.hangiMasaButonunaBasildi != null && siparisForm.viewdakiDepartmaninAdi != null)
                {
                    if (siparisForm.siparisMenuForm != null && (siparisForm.viewdakiDepartmaninAdi == departmanAdi && siparisForm.hangiMasaButonunaBasildi.Text == masa))
                    {
                        //invoke thread ler arası haberleşme
                        this.Invoke((MethodInvoker)delegate
                        {
                            siparisForm.menuFormunuKapatHesapOdeniyor(masa, departmanAdi);
                        });
                    }
                }
            }

            // tüm kullanıcıları bilgilendir
            tumKullanicilaraMesajYolla("komut=hesapOdeniyor&masa=" + masa + "&departmanAdi=" + departmanAdi);
        }

        private void komut_urunuTasi(string MasaAdi, string departmanAdi, string yeniMasa, string yeniDepartmanAdi, string siparisiGirenKisi, string aktarmaBilgileri, ClientRef client)
        {
            string[] aktarmalar;
            try
            {
                aktarmalar = aktarmaBilgileri.Split('*');
            }
            catch
            {
                //HATA MESAJI GÖNDER
                komut_IslemHatasi(client, "İşlem gerçekleştirilemedi, lütfen tekrar deneyiniz");
                return;
            }

            this.siparisiKimGirdi = siparisiGirenKisi;

            int aktarilacakMasaninAdisyonID;

            string urunTasinirkenYeniMasaOlusturulduysaOlusanMasaninAdi = null, urunTasinirkenYeniMasaOlusturulduysaOlusanDepartmaninAdi = null;

            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT AdisyonID FROM Adisyon WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Adisyon.MasaAdi='" + yeniMasa + "' AND Adisyon.DepartmanAdi='" + yeniDepartmanAdi + "'");

            SqlDataReader dr = cmd.ExecuteReader();

            dr.Read();

            try
            {
                aktarilacakMasaninAdisyonID = dr.GetInt32(0);
            }
            catch
            {
                aktarilacakMasaninAdisyonID = bosAdisyonOlustur(yeniMasa, yeniDepartmanAdi);

                urunTasinirkenYeniMasaOlusturulduysaOlusanMasaninAdi = yeniMasa;
                urunTasinirkenYeniMasaOlusturulduysaOlusanDepartmaninAdi = yeniDepartmanAdi;
            }

            for (int i = 0; i < aktarmalar.Count(); i++)
            {
                string yemekAdi, tur;
                double dusulecekDeger;
                int istenilenTasimaMiktari, tasinacakUrunIkramMi;
                decimal porsiyon;

                string[] detaylari = aktarmalar[i].Split('-');

                detaylari[1] = detaylari[1].Replace('.', ',');
                detaylari[2] = detaylari[2].Replace('.', ',');
                detaylari[4] = detaylari[4].Replace('.', ',');

                yemekAdi = detaylari[0];
                dusulecekDeger = Convert.ToDouble(detaylari[1]);
                istenilenTasimaMiktari = Convert.ToInt32(detaylari[2]);
                tasinacakUrunIkramMi = Convert.ToInt32(detaylari[3]);
                porsiyon = Convert.ToDecimal(detaylari[4]);
                tur = Convert.ToString(detaylari[5]);

                bool turBool = false;

                if (tur == "K")
                {
                    turBool = true;
                }

                cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adet,Siparis.VerilisTarihi, NotificationGorulduMu FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi='" + tasinacakUrunIkramMi + "' AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + MasaAdi + "' AND Adisyon.DepartmanAdi='" + departmanAdi + "' AND Siparis.YemekAdi='" + yemekAdi + "' AND Siparis.Garsonu='" + siparisiKimGirdi + "' AND Siparis.Porsiyon=CONVERT(DECIMAL(5,2),@_Porsiyon) AND Siparis.KiloSatisiMi=@_Tur ORDER BY Adet DESC");

                cmd.Parameters.AddWithValue("@_Porsiyon", porsiyon);
                cmd.Parameters.AddWithValue("@_Tur", turBool);

                dr = cmd.ExecuteReader();

                int siparisID, adet;
                DateTime verilisTarihi;
                bool NotificationGorulduMu;
                while (dr.Read())
                {
                    try
                    {
                        siparisID = dr.GetInt32(0);
                        adet = dr.GetInt32(1);
                        verilisTarihi = dr.GetDateTime(2);
                        NotificationGorulduMu = dr.GetBoolean(3);
                    }
                    catch
                    {
                        //HATA MESAJI GÖNDER
                        komut_IslemHatasi(client, "Ürünü taşırken bir hata oluştu, lütfen tekrar deneyiniz");
                        return;
                    }

                    if (adet < istenilenTasimaMiktari) // elimizde ikram edilmemişler ikramı istenenden küçükse
                    {
                        urunTasimaUpdateTam(siparisID, aktarilacakMasaninAdisyonID);

                        istenilenTasimaMiktari -= adet;
                    }
                    else if (adet > istenilenTasimaMiktari) // den büyükse
                    {
                        urunTasimaUpdateInsert(siparisID, aktarilacakMasaninAdisyonID, adet, dusulecekDeger, istenilenTasimaMiktari, yemekAdi, tasinacakUrunIkramMi, verilisTarihi, porsiyon, turBool, NotificationGorulduMu);

                        istenilenTasimaMiktari = 0;
                    }
                    else // elimizde ikram edilmemişler ikramı istenene eşitse
                    {
                        urunTasimaUpdateTam(siparisID, aktarilacakMasaninAdisyonID);

                        istenilenTasimaMiktari = 0;
                    }

                    if (istenilenTasimaMiktari == 0)
                        break;
                }

                if (istenilenTasimaMiktari != 0)// aktarılacaklar daha bitmedi başka garsonların siparişlerinden aktarıma devam et
                {
                    cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adet,Siparis.VerilisTarihi,NotificationGorulduMu FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi='" + tasinacakUrunIkramMi + "' AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + MasaAdi + "' AND Adisyon.DepartmanAdi='" + departmanAdi + "' AND Siparis.YemekAdi='" + yemekAdi + "' AND Siparis.Garsonu!='" + siparisiKimGirdi + "' AND Siparis.Porsiyon=CONVERT(DECIMAL(5,2),@_Porsiyon) AND Siparis.KiloSatisiMi=@_Tur ORDER BY Adet DESC");

                    cmd.Parameters.AddWithValue("@_Porsiyon", porsiyon);
                    cmd.Parameters.AddWithValue("@_Tur", turBool);

                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        try
                        {
                            siparisID = dr.GetInt32(0);
                            adet = dr.GetInt32(1);
                            verilisTarihi = dr.GetDateTime(2);
                            NotificationGorulduMu = dr.GetBoolean(3);
                        }
                        catch
                        {
                            //HATA MESAJI GÖNDER
                            komut_IslemHatasi(client, "Ürünü taşırken bir hata oluştu, lütfen tekrar deneyiniz");
                            return;
                        }

                        if (adet < istenilenTasimaMiktari) // elimizde ikram edilmemişler ikramı istenenden küçükse
                        {
                            urunTasimaUpdateTam(siparisID, aktarilacakMasaninAdisyonID);

                            istenilenTasimaMiktari -= adet;
                        }
                        else if (adet > istenilenTasimaMiktari) // den büyükse
                        {
                            urunTasimaUpdateInsert(siparisID, aktarilacakMasaninAdisyonID, adet, dusulecekDeger, istenilenTasimaMiktari, yemekAdi, tasinacakUrunIkramMi, verilisTarihi, porsiyon, turBool, NotificationGorulduMu);

                            istenilenTasimaMiktari = 0;
                        }
                        else // elimizde ikram edilmemişler ikramı istenene eşitse
                        {
                            urunTasimaUpdateTam(siparisID, aktarilacakMasaninAdisyonID);

                            istenilenTasimaMiktari = 0;
                        }

                        if (istenilenTasimaMiktari == 0)
                            break;
                    }
                }
            }

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            //clientları bilgilendir 
            //kapatma mesajını ve urunTasinirkenYeniMasaOlusturulduysaOlusanMasaninAdi bilgisini gönder
            if (siparisForm != null)
            {
                if (siparisForm.viewdakiDepartmaninAdi != null)
                {
                    if (siparisForm.viewdakiDepartmaninAdi == departmanAdi || siparisForm.viewdakiDepartmaninAdi == yeniDepartmanAdi)
                    {
                        if (urunTasinirkenYeniMasaOlusturulduysaOlusanMasaninAdi != null)
                        {
                            Button tablebutton = siparisForm.tablePanel.Controls[urunTasinirkenYeniMasaOlusturulduysaOlusanMasaninAdi] as Button;
                            if (tablebutton != null)
                            {
                                tablebutton.ForeColor = Color.White;
                                tablebutton.BackColor = Color.Firebrick;
                            }
                        }
                        if (siparisForm.hangiMasaButonunaBasildi != null)
                        {
                            if (siparisForm.siparisMenuForm != null && (siparisForm.hangiMasaButonunaBasildi.Text == yeniMasa || siparisForm.hangiMasaButonunaBasildi.Text == MasaAdi))
                            {
                                //invoke thread ler arası haberleşme
                                this.Invoke((MethodInvoker)delegate
                                {
                                    siparisForm.menuFormunuKapat(MasaAdi, yeniDepartmanAdi, yeniMasa);
                                });
                            }
                        }
                    }
                }
            }

            if (urunTasinirkenYeniMasaOlusturulduysaOlusanMasaninAdi != null && urunTasinirkenYeniMasaOlusturulduysaOlusanDepartmaninAdi != null)
            {
                tumKullanicilaraMesajYolla("komut=masaAcildi&masa=" + urunTasinirkenYeniMasaOlusturulduysaOlusanMasaninAdi + "&departmanAdi=" + urunTasinirkenYeniMasaOlusturulduysaOlusanDepartmaninAdi);
            }

            tumKullanicilaraMesajYolla("komut=urunuTasiTablet&masa=" + MasaAdi + "&departmanAdi=" + departmanAdi + "&yeniMasa=" + yeniMasa + "&yeniDepartmanAdi=" + yeniDepartmanAdi + "&aktarmaBilgileri=" + aktarmaBilgileri);

            //Tüm kullanıcılara ürün taşındı mesajı gönderelim
            tumKullanicilaraMesajYolla("komut=urunTasindi&masa=" + MasaAdi + "&departmanAdi=" + departmanAdi + "&yeniMasa=" + yeniMasa + "&yeniDepartmanAdi=" + yeniDepartmanAdi);

        }

        private void komut_masaDegistir(string yeniMasa, string yeniDepartmanAdi, string eskiMasa, string eskiDepartmanAdi, string yapilmasiGereken, string komut, ClientRef client)
        {
            SqlCommand cmd;

            switch (Convert.ToInt32(yapilmasiGereken))
            {
                case 0: // departman değişmedi ve masaların ikisi de açık
                    cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET MasaAdi = CASE MasaAdi WHEN @masaninAdiEski THEN @masaninAdiYeni WHEN @masaninAdiYeni THEN @masaninAdiEski END WHERE MasaAdi in (@masaninAdiEski,@masaninAdiYeni) AND AcikMi=1 AND DepartmanAdi=@departmanAdiEski");

                    cmd.Parameters.AddWithValue("@masaninAdiEski", eskiMasa);
                    cmd.Parameters.AddWithValue("@masaninAdiYeni", yeniMasa);
                    cmd.Parameters.AddWithValue("@departmanAdiEski", eskiDepartmanAdi);
                    cmd.ExecuteNonQuery();

                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                    break;
                case 1: // masalar açık departman değişti
                    cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET MasaAdi = CASE MasaAdi WHEN @masaninAdiEski THEN @masaninAdiYeni WHEN @masaninAdiYeni THEN @masaninAdiEski END, DepartmanAdi = CASE DepartmanAdi WHEN @departmanAdiEski THEN @departmanAdiYeni WHEN @departmanAdiYeni THEN @departmanAdiEski END WHERE MasaAdi in (@masaninAdiEski,@masaninAdiYeni) AND AcikMi=1 AND DepartmanAdi in (@departmanAdiEski,@departmanAdiYeni)");

                    cmd.Parameters.AddWithValue("@masaninAdiEski", eskiMasa);
                    cmd.Parameters.AddWithValue("@masaninAdiYeni", yeniMasa);
                    cmd.Parameters.AddWithValue("@departmanAdiEski", eskiDepartmanAdi);
                    cmd.Parameters.AddWithValue("@departmanAdiYeni", yeniDepartmanAdi);
                    cmd.ExecuteNonQuery();

                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                    break;
                case 2: // departman değişmedi 1 masa açık
                    cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET MasaAdi=@masaninAdi WHERE MasaAdi='" + eskiMasa + "' AND DepartmanAdi='" + eskiDepartmanAdi + "' AND AcikMi=1");
                    cmd.Parameters.AddWithValue("@masaninAdi", yeniMasa);
                    cmd.ExecuteNonQuery();

                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                    break;
                case 3: // departman değişti 1 masa açık
                    cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET MasaAdi=@masaninAdi, DepartmanAdi=@departmanAdi  WHERE MasaAdi='" + eskiMasa + "' AND DepartmanAdi='" + eskiDepartmanAdi + "' AND AcikMi=1");
                    cmd.Parameters.AddWithValue("@masaninAdi", yeniMasa);
                    cmd.Parameters.AddWithValue("@departmanAdi", yeniDepartmanAdi);
                    cmd.ExecuteNonQuery();

                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                    break;
                default:
                    break;
            }

            if (siparisForm != null)
            {
                if (siparisForm.siparisMenuForm != null)
                {
                    //invoke thread ler arası haberleşme
                    this.Invoke((MethodInvoker)delegate
                    {
                        siparisForm.komut_masaDegisti(eskiMasa, eskiDepartmanAdi, yeniMasa, yeniDepartmanAdi, "masaDegistir");
                    });
                }
            }

            //Tüm kullanıcılara masa değiştir mesajı gönderelim
            tumKullanicilaraMesajYolla("komut=masaDegistir&masa=" + eskiMasa + "&departmanAdi=" + eskiDepartmanAdi + "&yeniMasa=" + yeniMasa + "&yeniDepartmanAdi=" + yeniDepartmanAdi);


            if (komut == "masaDegistirTablet")
                client.MesajYolla("komut=masaDegistirTablet&masa=" + eskiMasa + "&departmanAdi=" + eskiDepartmanAdi + "&yeniMasa=" + yeniMasa + "&yeniDepartmanAdi=" + yeniDepartmanAdi + "&yapilmasiGerekenIslem=" + yapilmasiGereken);
        }

        private void komut_masaGirilebilir(string masa, string departmanAdi)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET OdemeYapiliyor=@_OdemeYapiliyor WHERE AdisyonID=(SELECT AdisyonID FROM Adisyon WHERE IptalMi=0 AND AcikMi=1 AND MasaAdi=@_MasaAdi AND DepartmanAdi=@_DepartmanAdi)");
            cmd.Parameters.AddWithValue("@_MasaAdi", masa);
            cmd.Parameters.AddWithValue("@_DepartmanAdi", departmanAdi);
            cmd.Parameters.AddWithValue("@_OdemeYapiliyor", 0);
            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        private void komut_adisyonNotunuGuncelle(string masa, string departmanAdi, string adisyonNotuGelen)
        {
            SqlCommand cmd;

            cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET AdisyonNotu=@adisyonNotu WHERE AdisyonID=(SELECT AdisyonID FROM Adisyon WHERE AcikMi=1 AND MasaAdi='" + masa + "' AND DepartmanAdi='" + departmanAdi + "')");

            cmd.Parameters.AddWithValue("@adisyonNotu", adisyonNotuGelen);
            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        private void komut_siparis(string masa, string departmanAdi, string miktar, string yemekAdi, string siparisiGirenKisi, string dusulecekDegerGelen, ClientRef client, string adisyonNotuGelen, string sonSiparisMi, string porsiyon, string tur = "P", string ilkSiparisMi = "", string porsiyonSinifi = "")
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT OdemeYapiliyor,HesapIstendi FROM Adisyon WHERE AcikMi=1 AND IptalMi=0 AND MasaAdi=@_MasaAdi AND DepartmanAdi=@_DepartmanAdi");
            cmd.Parameters.AddWithValue("@_MasaAdi", masa);
            cmd.Parameters.AddWithValue("@_DepartmanAdi", departmanAdi);

            SqlDataReader dr = cmd.ExecuteReader();

            dr.Read();
            try
            {
                if (dr.GetBoolean(0))
                {
                    if (Convert.ToInt32(sonSiparisMi) == 0)
                        client.MesajYolla("komut=hesapIslemde&miktar=" + miktar + "&yemekAdi=" + yemekAdi + "&fiyat=" + dusulecekDegerGelen + "&porsiyon=" + porsiyon + "&porsiyonSinifi=" + porsiyonSinifi);
                    else
                        client.MesajYolla("komut=siparisListesineGeriEkle&miktar=" + miktar + "&yemekAdi=" + yemekAdi + "&fiyat=" + dusulecekDegerGelen + "&porsiyon=" + porsiyon + "&porsiyonSinifi=" + porsiyonSinifi);
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                    return;
                }
                else if (dr.GetBoolean(1))
                {
                    if (Convert.ToInt32(sonSiparisMi) == 0)
                        client.MesajYolla("komut=hesapGeliyor");
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                    return;
                }
            }
            catch
            { }

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            miktar = miktar.Replace('.', ',');
            dusulecekDegerGelen = dusulecekDegerGelen.Replace('.', ',');
            porsiyon = porsiyon.Replace('.', ',');

            bool turBool = false;

            if (tur == "K")
            {
                turBool = true;
            }

            if (siparisForm != null)
            {
                if (siparisForm.hangiMasaButonunaBasildi != null && siparisForm.viewdakiDepartmaninAdi != null)
                {
                    if (siparisForm.siparisMenuForm != null && siparisForm.viewdakiDepartmaninAdi == departmanAdi && siparisForm.hangiMasaButonunaBasildi.Text == masa)
                    {
                        //invoke thread ler arası haberleşme
                        this.Invoke((MethodInvoker)delegate
                        {
                            siparisForm.siparisMenuForm.siparisOnayiGeldi(miktar, yemekAdi, dusulecekDegerGelen, porsiyon, turBool);
                        });
                    }
                }
            }

            siparisiKimGirdi = siparisiGirenKisi;

            cmd = SQLBaglantisi.getCommand("SELECT AdisyonID FROM Adisyon WHERE MasaAdi='" + masa + "' AND DepartmanAdi='" + departmanAdi + "' AND AcikMi=1");
            dr = cmd.ExecuteReader();
            dr.Read();

            int adisyonID;

            try // açık
            {
                adisyonID = dr.GetInt32(0);
            }
            catch// kapalı
            {
                cmd = SQLBaglantisi.getCommand("INSERT INTO Adisyon(AcikMi,AdisyonNotu,AcilisZamani,DepartmanAdi,MasaAdi) VALUES(@_acikMi,@_AdisyonNotu,@_AcilisZamani,@_DepartmanAdi,@_MasaAdi) SELECT SCOPE_IDENTITY()");

                cmd.Parameters.AddWithValue("@_acikmi", 1);
                cmd.Parameters.AddWithValue("@_AdisyonNotu", adisyonNotuGelen);
                cmd.Parameters.AddWithValue("@_AcilisZamani", DateTime.Now);
                cmd.Parameters.AddWithValue("@_DepartmanAdi", departmanAdi);
                cmd.Parameters.AddWithValue("@_MasaAdi", masa);
                try
                {
                    adisyonID = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch
                {
                    komut_IslemHatasi(client, "İşlem gerçekleştirilemedi, lütfen tekrar deneyiniz");
                    return;
                }
            }

            bool urunYaziciyaBildirilmeliMi = yaziciBilgilendirilmeliMi(yemekAdi);

            string ciktiAlinanYazici = urunYazicisiniBul(yemekAdi);

            cmd = SQLBaglantisi.getCommand("INSERT INTO Siparis(AdisyonID,Garsonu,Fiyatı,Adet,YemekAdi,VerilisTarihi,CiktiAlinmaliMi,Porsiyon,KiloSatisiMi,NotificationGorulduMu,CiktiAlinanYazici) VALUES(@_AdisyonID,@_Garsonu,@_Fiyatı,@_Adet,@_YemekAdi,@_VerilisTarihi,@_CiktiAlinmaliMi,@_Porsiyon,@_KiloSatisiMi,@_NotificationGorulduMu,@_CiktiAlinanYazici)");

            cmd.Parameters.AddWithValue("@_AdisyonID", adisyonID);
            cmd.Parameters.AddWithValue("@_Garsonu", siparisiKimGirdi);
            cmd.Parameters.AddWithValue("@_Fiyatı", Convert.ToDecimal(dusulecekDegerGelen));
            cmd.Parameters.AddWithValue("@_Adet", Convert.ToInt32(miktar));
            cmd.Parameters.AddWithValue("@_YemekAdi", yemekAdi);
            cmd.Parameters.AddWithValue("@_VerilisTarihi", DateTime.Now);
            cmd.Parameters.AddWithValue("@_CiktiAlinmaliMi", urunYaziciyaBildirilmeliMi);
            cmd.Parameters.AddWithValue("@_Porsiyon", Convert.ToDecimal(porsiyon));
            cmd.Parameters.AddWithValue("@_CiktiAlinanYazici", ciktiAlinanYazici);

            if (!turBool)
            {
                cmd.Parameters.AddWithValue("@_KiloSatisiMi", 0);
                cmd.Parameters.AddWithValue("@_NotificationGorulduMu", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@_KiloSatisiMi", 1);
                cmd.Parameters.AddWithValue("@_NotificationGorulduMu", 1);
            }

            cmd.ExecuteNonQuery();

            if (adisyonNotuGelen != "")
            {
                cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET AdisyonNotu=@adisyonNotu WHERE AdisyonID=@id");
                cmd.Parameters.AddWithValue("@adisyonNotu", adisyonNotuGelen);
                cmd.Parameters.AddWithValue("@id", adisyonID);
                cmd.ExecuteNonQuery();
            }

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            if (Convert.ToInt32(sonSiparisMi) == 0) // mutfağa adisyon gönder
            {
                cmd = SQLBaglantisi.getCommand("SELECT DISTINCT CiktiAlinanYazici,Garsonu FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Siparis.AdisyonID=@_AdisyonID AND AcikMi=1 AND IkramMi=0 AND Adisyon.IptalMi=0 AND Siparis.IptalMi=0 AND CiktiAlindiMi=0 AND CiktiAlinmaliMi=1");
                cmd.Parameters.AddWithValue("@_AdisyonID", adisyonID);

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    string tanimliYaziciAdi = "", garsonu = "";
                   
                    if (!dr.IsDBNull(0))
                        tanimliYaziciAdi = dr.GetString(0);

                    if (tanimliYaziciAdi == "")
                        continue;

                    garsonu = dr.GetString(1);

                    SqlCommand cmd2 = SQLBaglantisi.getCommand("SELECT FirmaAdi,Yazici FROM Yazici WHERE YaziciAdi=@_YaziciAdi");
                    cmd2.Parameters.AddWithValue("@_YaziciAdi", tanimliYaziciAdi);

                    SqlDataReader dr2 = cmd2.ExecuteReader();

                    string firmaAdi = "", yaziciAdi = "";

                    while (dr2.Read())
                    {
                        firmaAdi = dr2.GetString(0);
                        yaziciAdi = dr2.GetString(1);
                    }

                    cmd2.Connection.Close();
                    cmd2.Connection.Dispose();

                    asyncYaziciyaGonder(masa, departmanAdi, firmaAdi, yaziciAdi, tanimliYaziciAdi, garsonu, raporMutfak).Join();
                }
                cmd.Connection.Close();
                cmd.Connection.Dispose();
            }

            //Tüm kullanıcılara sipariş mesajı gönderelim
            tumKullanicilaraMesajYolla("komut=siparis&masa=" + masa + "&departmanAdi=" + departmanAdi + "&miktar=" + miktar + "&yemekAdi=" + yemekAdi + "&dusulecekDeger=" + dusulecekDegerGelen + "&porsiyon=" + porsiyon + "&tur=" + tur + "&ilkSiparis=" + ilkSiparisMi);
        }

        public Thread asyncYaziciyaGonder(string masaAdi, string departmanAdi, string firmaAdi, string yaziciAdi, string tanimliYaziciAdi, string garsonu, CrystalReportMutfak rapor)
        {
            var t = new Thread(() => Basla(masaAdi, departmanAdi, firmaAdi, yaziciAdi, tanimliYaziciAdi, garsonu, rapor));
            t.Start();
            return t;
        }

        private static void Basla(string masaAdi, string departmanAdi, string firmaAdi, string yaziciAdi, string tanimliYaziciAdi, string garsonu, CrystalReportMutfak rapor)
        {
            rapor.Refresh();

            rapor.SetParameterValue("Masa", masaAdi);
            rapor.SetParameterValue("Departman", departmanAdi);
            rapor.SetParameterValue("FirmaAdi", firmaAdi); // firma adı
            rapor.SetParameterValue("CiktiAlinanYazici", tanimliYaziciAdi);
            rapor.SetParameterValue("Garson", garsonu);

            try
            {
                rapor.PrintOptions.PrinterName = yaziciAdi;
                rapor.PrintToPrinter(1, false, 0, 0);
            }
            catch
            {
                KontrolFormu dialog = new KontrolFormu("Yazıcı bulunamadı\nLütfen ayarlarınızı kontrol edin", false);
                dialog.Show();
                return;
            }

            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET CiktiAlindiMi=1 WHERE AdisyonID=(SELECT AdisyonID FROM Adisyon WHERE IptalMi=0 AND AcikMi=1 AND MasaAdi=@_MasaninAdi AND DepartmanAdi=@_DepartmanAdi) AND CiktiAlinanYazici=@_CiktiAlinanYazici AND Siparis.IptalMi=0");
            cmd.Parameters.AddWithValue("@_MasaninAdi", masaAdi);
            cmd.Parameters.AddWithValue("@_DepartmanAdi", departmanAdi);
            cmd.Parameters.AddWithValue("@_CiktiAlinanYazici", tanimliYaziciAdi);

            cmd.ExecuteNonQuery();

            cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET AdisyonNotu=@_AdisyonNotu WHERE AcikMi=1 AND MasaAdi=@_MasaninAdi AND DepartmanAdi=@_DepartmanAdi");
            cmd.Parameters.AddWithValue("@_AdisyonNotu", "");
            cmd.Parameters.AddWithValue("@_MasaninAdi", masaAdi);
            cmd.Parameters.AddWithValue("@_DepartmanAdi", departmanAdi);

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public Thread asyncYaziciyaGonder(string masaAdi, string departmanAdi, string firmaAdi, string yaziciAdi, string tanimliYaziciAdi, string garsonu, CrystalReportMutfakUrunIptal rapor)
        {
            var t = new Thread(() => Basla(masaAdi, departmanAdi, firmaAdi, yaziciAdi, tanimliYaziciAdi, garsonu, rapor));
            t.Start();
            return t;
        }

        private static void Basla(string masaAdi, string departmanAdi, string firmaAdi, string yaziciAdi, string tanimliYaziciAdi, string garsonu, CrystalReportMutfakUrunIptal rapor)
        {
            rapor.Refresh();

            rapor.SetParameterValue("Masa", masaAdi);
            rapor.SetParameterValue("Departman", departmanAdi);
            rapor.SetParameterValue("FirmaAdi", firmaAdi); // firma adı
            rapor.SetParameterValue("CiktiAlinanYazici", tanimliYaziciAdi);
            rapor.SetParameterValue("Garson", garsonu);

            try
            {
                rapor.PrintOptions.PrinterName = yaziciAdi;
                rapor.PrintToPrinter(1, false, 0, 0);
            }
            catch
            {
                KontrolFormu dialog = new KontrolFormu("Yazıcı bulunamadı\nLütfen ayarlarınızı kontrol edin", false);
                dialog.Show();
                return;
            }

            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET CiktiAlindiMi=1 WHERE AdisyonID=(SELECT AdisyonID FROM Adisyon WHERE IptalMi=0 AND AcikMi=1 AND MasaAdi=@_MasaninAdi AND DepartmanAdi=@_DepartmanAdi) AND CiktiAlinanYazici=@_CiktiAlinanYazici AND Siparis.IptalMi=1");
            cmd.Parameters.AddWithValue("@_MasaninAdi", masaAdi);
            cmd.Parameters.AddWithValue("@_DepartmanAdi", departmanAdi);
            cmd.Parameters.AddWithValue("@_CiktiAlinanYazici", tanimliYaziciAdi);

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        private void komut_adisyonYazdir(string masa, string departmanAdi, string garson, string yazdirilacakIndirim, string acilisZamani, string firmaAdi, string firmaAdresTelefon, string yaziciWindowsAdi, string odenenMiktar)
        {
            asyncYaziciyaGonder(masa, departmanAdi, garson, yazdirilacakIndirim, acilisZamani, firmaAdi, firmaAdresTelefon, yaziciWindowsAdi, raporAdisyon, odenenMiktar).Join();
        }

        public Thread asyncYaziciyaGonder(string masaAdi, string departmanAdi, string garson, string yazdirilacakIndirim, string acilisZamani, string firmaAdi, string adresTelefon, string printerAdi, CrystalReportAdisyon rapor, string odenenMiktar)
        {
            var t = new Thread(() => Basla(masaAdi, departmanAdi, garson, yazdirilacakIndirim, acilisZamani, firmaAdi, adresTelefon, printerAdi, rapor, odenenMiktar));
            t.Start();
            return t;
        }

        private static void Basla(string masaAdi, string departmanAdi, string garson, string yazdirilacakIndirim, string acilisZamani, string firmaAdi, string adresTelefon, string printerAdi, CrystalReportAdisyon rapor, string odenenMiktar)
        {
            rapor.Refresh();

            decimal odemesiYapilanMiktar = Convert.ToDecimal(odenenMiktar), indirim = Convert.ToDecimal(yazdirilacakIndirim);

            odemesiYapilanMiktar -= indirim;

            if (odemesiYapilanMiktar <= 0 && indirim <= 0)
            {
                ReportObjects ro = rapor.ReportDefinition.ReportObjects;
                ((LineObject)ro[name: "line4"]).ObjectFormat.EnableSuppress = true;
            }

            rapor.SetParameterValue("FirmaAdi", firmaAdi); // firma adı
            rapor.SetParameterValue("Garson", garson);
            rapor.SetParameterValue("Departman", departmanAdi);
            rapor.SetParameterValue("Masa", masaAdi);
            rapor.SetParameterValue("FirmaAdresTelefon", adresTelefon); // firma adres ve telefon        
            rapor.SetParameterValue("AcilisZamani", acilisZamani);
            rapor.SetParameterValue("Indirim", indirim);
            rapor.SetParameterValue("OdenenMiktar", odemesiYapilanMiktar);
            try
            {
                rapor.PrintOptions.PrinterName = printerAdi;
                rapor.PrintToPrinter(1, false, 0, 0);
            }
            catch
            {
                KontrolFormu dialog = new KontrolFormu("Yazıcı bulunamadı\nLütfen ayarlarınızı kontrol edin", false);
                dialog.Show();
            }
        }

        void iptalYazicisi(string masa, string departmanAdi)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT DISTINCT CiktiAlinanYazici,Garsonu FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE MasaAdi=@_MasaAdi AND DepartmanAdi=@_DepartmanAdi AND AcikMi=1 AND IkramMi=0 AND Adisyon.IptalMi=0 AND Siparis.IptalMi=1 AND CiktiAlindiMi=0 AND CiktiAlinmaliMi=1");
            cmd.Parameters.AddWithValue("@_MasaAdi", masa);
            cmd.Parameters.AddWithValue("@_DepartmanAdi", departmanAdi);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string tanimliYaziciAdi = "", garsonu = "";

                if (!dr.IsDBNull(0))
                    tanimliYaziciAdi = dr.GetString(0);

                if (tanimliYaziciAdi == "")
                    continue;

                garsonu = dr.GetString(1);

                SqlCommand cmd2 = SQLBaglantisi.getCommand("SELECT FirmaAdi,Yazici FROM Yazici WHERE YaziciAdi=@_YaziciAdi");
                cmd2.Parameters.AddWithValue("@_YaziciAdi", tanimliYaziciAdi);

                SqlDataReader dr2 = cmd2.ExecuteReader();

                string firmaAdi = "", yaziciAdi = "";

                while (dr2.Read())
                {
                    firmaAdi = dr2.GetString(0);
                    yaziciAdi = dr2.GetString(1);
                }
                cmd2.Connection.Close();
                cmd2.Connection.Dispose();

                asyncYaziciyaGonder(masa, departmanAdi, firmaAdi, yaziciAdi, tanimliYaziciAdi, garsonu, raporMutfakIptal).Join();
            }
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        private void komut_listeBos(string masa, string departmanAdi)
        {
            SqlCommand cmd;

            // iptal edilen ürünler için mutfağa adisyon
            iptalYazicisi(masa, departmanAdi);

            cmd = SQLBaglantisi.getCommand("SELECT OdenenMiktar from OdemeDetay JOIN Adisyon ON OdemeDetay.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.MasaAdi='" + masa + "' AND Adisyon.DepartmanAdi='" + departmanAdi + "' AND Adisyon.AcikMi=1 AND Adisyon.IptalMi=0");
            SqlDataReader dr = cmd.ExecuteReader();

            try // eğer masanın ödenmiş siparişi varsa hesabı kapat 
            {
                decimal odenenmiktar = 0;

                while (dr.Read()) // ödenen miktarları topluyoruz
                {
                    odenenmiktar += dr.GetDecimal(0);
                }

                if (odenenmiktar != 0) // eğer sıfırdan farklı ise adisyonu kapatıyoruz
                {
                    cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET OdendiMi=1 WHERE Siparis.IptalMi=0 AND AdisyonID=(SELECT AdisyonID FROM Adisyon WHERE AcikMi=1 AND MasaAdi='" + masa + "' AND DepartmanAdi='" + departmanAdi + "')");
                    cmd.ExecuteNonQuery();
                    cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET AcikMi=0,KapanisZamani=@date WHERE AdisyonID=(SELECT AdisyonID FROM Adisyon WHERE AcikMi=1 AND MasaAdi='" + masa + "' AND DepartmanAdi='" + departmanAdi + "')");
                    cmd.Parameters.AddWithValue("@date", DateTime.Now);
                }
                else // değilse adisyonu iptal ediyoruz
                {
                    cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET AcikMi=0, IptalMi=1, KapanisZamani=@date WHERE AdisyonID=(SELECT AdisyonID FROM Adisyon WHERE AcikMi=1 AND MasaAdi='" + masa + "' AND DepartmanAdi='" + departmanAdi + "')");
                    cmd.Parameters.AddWithValue("@date", DateTime.Now);
                }
            }
            catch // eğer masanın ödenmiş siparişi yoksa iptal 
            {
                cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET AcikMi=0, IptalMi=1, KapanisZamani=@date WHERE AdisyonID=(SELECT AdisyonID FROM Adisyon WHERE AcikMi=1 AND MasaAdi='" + masa + "' AND DepartmanAdi='" + departmanAdi + "')");
                cmd.Parameters.AddWithValue("@date", DateTime.Now);
            }

            try //adisyonID alınabilirse adisyon var demektir, ancak sipariş yok - o zaman adisyon kapatılır
            {
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Connection.Dispose();
            }
            catch (Exception) // masaya ait adisyon yok, çık
            {
                cmd.Connection.Close();
                cmd.Connection.Dispose();
            }
        }

        private void komut_iptal(string masa, string departmanAdi, string miktar, string yemekAdi, string siparisiGirenKisi, string dusulecekDegerGelen, ClientRef client, string adisyonNotu, string ikraminGrubu, string porsiyon, string tur, string iptalNedeni)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT OdemeYapiliyor,HesapIstendi FROM Adisyon WHERE AcikMi=1 AND IptalMi=0 AND MasaAdi=@_MasaAdi AND DepartmanAdi=@_DepartmanAdi");
            cmd.Parameters.AddWithValue("@_MasaAdi", masa);
            cmd.Parameters.AddWithValue("@_DepartmanAdi", departmanAdi);

            SqlDataReader dr = cmd.ExecuteReader();

            dr.Read();
            try
            {
                if (dr.GetBoolean(0))
                {
                    client.MesajYolla("komut=hesapIslemde");
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                    return;
                }
                else if (dr.GetBoolean(1))
                {
                    client.MesajYolla("komut=hesapGeliyor");
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                    return;
                }
            }
            catch
            { }

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            miktar = miktar.Replace('.', ',');
            dusulecekDegerGelen = dusulecekDegerGelen.Replace('.', ',');
            porsiyon = porsiyon.Replace('.', ',');

            bool turBool = false;

            if (tur == "K")
            {
                turBool = true;
            }

            if (siparisForm != null)
            {
                if (siparisForm.hangiMasaButonunaBasildi != null && siparisForm.viewdakiDepartmaninAdi != null)
                {
                    if (siparisForm.siparisMenuForm != null && siparisForm.viewdakiDepartmaninAdi == departmanAdi && siparisForm.hangiMasaButonunaBasildi.Text == masa)
                    {
                        //invoke thread ler arası haberleşme
                        this.Invoke((MethodInvoker)delegate
                        {
                            siparisForm.siparisMenuForm.iptalGeldi(miktar, yemekAdi, dusulecekDegerGelen, ikraminGrubu, porsiyon, turBool);
                        });
                    }
                }
            }

            siparisiKimGirdi = siparisiGirenKisi;

            int istenilenSiparisiptalSayisi = Convert.ToInt32(miktar);

            this.adisyonNotu = adisyonNotu;

            double dusulecekDeger = Convert.ToDouble(dusulecekDegerGelen);

            string ikramSQLGirdisi;

            if (ikraminGrubu == "0" || ikraminGrubu == "1") // eski ikram veya yeni ikram grubundaysa, ikramlardan iptal edilecek
                ikramSQLGirdisi = "1";
            else // değilse siparişlerden ikram edilecek
                ikramSQLGirdisi = "0";

            cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adisyon.AdisyonID,Adet,Siparis.VerilisTarihi FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi='" + ikramSQLGirdisi + "' AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + masa + "' AND Adisyon.DepartmanAdi='" + departmanAdi + "' AND Siparis.YemekAdi='" + yemekAdi + "' AND Siparis.Garsonu='" + siparisiGirenKisi + "' AND Siparis.Porsiyon=CONVERT(DECIMAL(5,2),@_Porsiyon) AND Siparis.KiloSatisiMi=@_Tur ORDER BY Adet DESC");

            cmd.Parameters.AddWithValue("@_Porsiyon", Convert.ToDecimal(porsiyon));
            cmd.Parameters.AddWithValue("@_Tur", turBool);

            dr = cmd.ExecuteReader();

            int siparisID, adisyonID = 0;
            int adet;
            DateTime verilisTarihi;

            while (dr.Read())
            {
                try
                {
                    siparisID = dr.GetInt32(0);
                    adisyonID = dr.GetInt32(1);
                    adet = dr.GetInt32(2);
                    verilisTarihi = dr.GetDateTime(3);
                }
                catch
                {
                    //HATA MESAJI GÖNDER
                    komut_IslemHatasi(client, "İptal etme gerçekleştirilemedi, lütfen tekrar deneyiniz");
                    return;
                }

                if (adet < istenilenSiparisiptalSayisi) // elimizdeki siparişler iptali istenenden küçükse
                {
                    iptalUpdateTam(siparisID, iptalNedeni);

                    istenilenSiparisiptalSayisi -= adet;
                }
                else if (adet > istenilenSiparisiptalSayisi) // den büyükse
                {
                    iptalUpdateInsert(siparisID, adisyonID, adet, dusulecekDeger, istenilenSiparisiptalSayisi, yemekAdi, verilisTarihi, Convert.ToDecimal(porsiyon), iptalNedeni, turBool);

                    istenilenSiparisiptalSayisi = 0;
                }
                else // elimizdeki siparişler iptali istenene eşitse
                {
                    iptalUpdateTam(siparisID, iptalNedeni);

                    istenilenSiparisiptalSayisi = 0;
                }
                if (istenilenSiparisiptalSayisi == 0)
                    break;
            }

            if (istenilenSiparisiptalSayisi != 0)// iptal edilecekler daha bitmedi başka garsonların siparişlerinden iptale devam et
            {
                cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adisyon.AdisyonID,Adet,Siparis.VerilisTarihi FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi='" + ikramSQLGirdisi + "' AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + masa + "' AND Adisyon.DepartmanAdi='" + departmanAdi + "' AND Siparis.YemekAdi='" + yemekAdi + "' AND Siparis.Garsonu!='" + siparisiGirenKisi + "' AND Siparis.Porsiyon=CONVERT(DECIMAL(5,2),@_Porsiyon) AND Siparis.KiloSatisiMi=@_Tur ORDER BY Adet DESC");

                cmd.Parameters.AddWithValue("@_Porsiyon", Convert.ToDecimal(porsiyon));
                cmd.Parameters.AddWithValue("@_Tur", turBool);

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    try
                    {
                        siparisID = dr.GetInt32(0);
                        adisyonID = dr.GetInt32(1);
                        adet = dr.GetInt32(2);
                        verilisTarihi = dr.GetDateTime(3);
                    }
                    catch
                    {
                        //HATA MESAJI GÖNDER
                        komut_IslemHatasi(client, "İptal etme gerçekleştirilemedi, lütfen tekrar deneyiniz");
                        return;
                    }

                    if (adet < istenilenSiparisiptalSayisi) // elimizdeki siparişler iptali istenenden küçükse
                    {
                        iptalUpdateTam(siparisID, iptalNedeni);

                        istenilenSiparisiptalSayisi -= adet;
                    }
                    else if (adet > istenilenSiparisiptalSayisi) // den büyükse
                    {
                        iptalUpdateInsert(siparisID, adisyonID, adet, dusulecekDeger, istenilenSiparisiptalSayisi, yemekAdi, verilisTarihi, Convert.ToDecimal(porsiyon), iptalNedeni, turBool);

                        istenilenSiparisiptalSayisi = 0;
                    }
                    else // elimizdeki siparişler iptali istenene eşitse
                    {
                        iptalUpdateTam(siparisID, iptalNedeni);

                        istenilenSiparisiptalSayisi = 0;
                    }
                    if (istenilenSiparisiptalSayisi == 0)
                        break;
                }
            }

            // iptal edilen ürünler için mutfağa adisyon
            iptalYazicisi(masa, departmanAdi);

            if (adisyonNotu != null && adisyonNotu != "")
            {
                cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET AdisyonNotu=@adisyonNotu WHERE AdisyonID=@id");

                cmd.Parameters.AddWithValue("@adisyonNotu", adisyonNotu);
                cmd.Parameters.AddWithValue("@id", adisyonID);

                cmd.ExecuteNonQuery();
            }

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            //Tüm kullanıcılara iptal mesajı gönderelim
            tumKullanicilaraMesajYolla("komut=iptal&masa=" + masa + "&departmanAdi=" + departmanAdi + "&miktar=" + miktar + "&yemekAdi=" + yemekAdi + "&dusulecekDeger=" + dusulecekDeger + "&ikramYeniMiEskiMi=" + ikraminGrubu + "&porsiyon=" + porsiyon + "&tur=" + tur);
        }

        private void komut_ikram(string masa, string departmanAdi, string miktar, string yemekAdi, string siparisiGirenKisi, string dusulecekDegerGelen, ClientRef client, string adisyonNotu, string porsiyon, string tur)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT OdemeYapiliyor,HesapIstendi FROM Adisyon WHERE AcikMi=1 AND IptalMi=0 AND MasaAdi=@_MasaAdi AND DepartmanAdi=@_DepartmanAdi");
            cmd.Parameters.AddWithValue("@_MasaAdi", masa);
            cmd.Parameters.AddWithValue("@_DepartmanAdi", departmanAdi);

            SqlDataReader dr = cmd.ExecuteReader();

            dr.Read();
            try
            {
                if (dr.GetBoolean(0))
                {
                    client.MesajYolla("komut=hesapIslemde");
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                    return;
                }
                else if (dr.GetBoolean(1))
                {
                    client.MesajYolla("komut=hesapGeliyor");
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                    return;
                }
            }
            catch
            { }

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            miktar = miktar.Replace('.', ',');
            dusulecekDegerGelen = dusulecekDegerGelen.Replace('.', ',');
            porsiyon = porsiyon.Replace('.', ',');

            bool turBool = false;

            if (tur == "K")
            {
                turBool = true;
            }

            if (siparisForm != null)
            {
                if (siparisForm.hangiMasaButonunaBasildi != null && siparisForm.viewdakiDepartmaninAdi != null)
                {
                    if (siparisForm.siparisMenuForm != null && siparisForm.viewdakiDepartmaninAdi == departmanAdi && siparisForm.hangiMasaButonunaBasildi.Text == masa)
                    {
                        //invoke thread ler arası haberleşme
                        this.Invoke((MethodInvoker)delegate
                        {
                            siparisForm.siparisMenuForm.ikramGeldi(miktar, yemekAdi, dusulecekDegerGelen, porsiyon, turBool);
                        });
                    }
                }
            }

            siparisiKimGirdi = siparisiGirenKisi;

            this.adisyonNotu = adisyonNotu;

            int istenilenikramSayisi = Convert.ToInt32(miktar);

            // ürünün değerini istenilen kadar azalt, kalan hesaptan düş
            double dusulecekDeger = Convert.ToDouble(dusulecekDegerGelen);

            cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adisyon.AdisyonID,Adet,Siparis.VerilisTarihi,NotificationGorulduMu FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi=0 AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + masa + "' AND Adisyon.DepartmanAdi='" + departmanAdi + "' AND Siparis.YemekAdi='" + yemekAdi + "' AND Siparis.Garsonu='" + siparisiGirenKisi + "' AND Siparis.Porsiyon=CONVERT(DECIMAL(5,2),@_Porsiyon) AND Siparis.KiloSatisiMi=@_Tur ORDER BY Adet DESC");

            cmd.Parameters.AddWithValue("@_Porsiyon", Convert.ToDecimal(porsiyon));
            cmd.Parameters.AddWithValue("@_Tur", turBool);

            dr = cmd.ExecuteReader();

            int siparisID, adisyonID = 0;
            int adet;
            bool NotificationGorulduMu;
            DateTime verilisTarihi;

            while (dr.Read())
            {
                try
                {
                    siparisID = dr.GetInt32(0);
                    adisyonID = dr.GetInt32(1);
                    adet = dr.GetInt32(2);
                    verilisTarihi = dr.GetDateTime(3);
                    NotificationGorulduMu = dr.GetBoolean(4);
                }
                catch
                {
                    //HATA MESAJI GÖNDER
                    komut_IslemHatasi(client, "İkram etme gerçekleştirilemedi, lütfen tekrar deneyiniz");
                    return;
                }

                if (adet < istenilenikramSayisi) // elimizde ikram edilmemişler ikramı istenenden küçükse
                {
                    ikramUpdateTam(siparisID, 1);

                    istenilenikramSayisi -= adet;
                }
                else if (adet > istenilenikramSayisi) // den büyükse
                {
                    ikramUpdateInsert(siparisID, adisyonID, adet, dusulecekDeger, istenilenikramSayisi, yemekAdi, 1, verilisTarihi, Convert.ToDecimal(porsiyon), turBool, NotificationGorulduMu);

                    istenilenikramSayisi = 0;
                }
                else // elimizde ikram edilmemişler ikramı istenene eşitse
                {
                    ikramUpdateTam(siparisID, 1);

                    istenilenikramSayisi = 0;
                }

                if (istenilenikramSayisi == 0)
                    break;
            }

            if (istenilenikramSayisi != 0)// ikram edilecekler daha bitmedi başka garsonların siparişlerinden ikram iptaline devam et
            {
                cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adisyon.AdisyonID,Adet,Siparis.VerilisTarihi,NotificationGorulduMu FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi=0 AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + masa + "' AND Adisyon.DepartmanAdi='" + departmanAdi + "' AND Siparis.YemekAdi='" + yemekAdi + "' AND Siparis.Garsonu!='" + siparisiGirenKisi + "' AND Siparis.Porsiyon=CONVERT(DECIMAL(5,2),@_Porsiyon) AND Siparis.KiloSatisiMi=@_Tur ORDER BY Adet DESC");

                cmd.Parameters.AddWithValue("@_Porsiyon", Convert.ToDecimal(porsiyon));
                cmd.Parameters.AddWithValue("@_Tur", turBool);

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    try
                    {
                        siparisID = dr.GetInt32(0);
                        adisyonID = dr.GetInt32(1);
                        adet = dr.GetInt32(2);
                        verilisTarihi = dr.GetDateTime(3);
                        NotificationGorulduMu = dr.GetBoolean(4);
                    }
                    catch
                    {
                        //HATA MESAJI GÖNDER
                        komut_IslemHatasi(client, "İkram etme gerçekleştirilemedi, lütfen tekrar deneyiniz");
                        return;
                    }

                    if (adet < istenilenikramSayisi) // elimizde ikram edilmemişler ikramı istenenden küçükse
                    {
                        ikramUpdateTam(siparisID, 1);

                        istenilenikramSayisi -= adet;
                    }
                    else if (adet > istenilenikramSayisi) // den büyükse
                    {
                        ikramUpdateInsert(siparisID, adisyonID, adet, dusulecekDeger, istenilenikramSayisi, yemekAdi, 1, verilisTarihi, Convert.ToDecimal(porsiyon), turBool, NotificationGorulduMu);

                        istenilenikramSayisi = 0;
                    }
                    else // elimizde ikram edilmemişler ikramı istenene eşitse
                    {
                        ikramUpdateTam(siparisID, 1);

                        istenilenikramSayisi = 0;
                    }

                    if (istenilenikramSayisi == 0)
                        break;
                }
            }

            if (adisyonNotu != null && adisyonNotu != "")
            {
                cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET AdisyonNotu=@adisyonNotu WHERE AdisyonID=@id");

                cmd.Parameters.AddWithValue("@adisyonNotu", adisyonNotu);
                cmd.Parameters.AddWithValue("@id", adisyonID);
                cmd.ExecuteNonQuery();
            }

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            //Tüm kullanıcılara ikram mesajı gönderelim
            tumKullanicilaraMesajYolla("komut=ikram&masa=" + masa + "&departmanAdi=" + departmanAdi + "&miktar=" + miktar + "&yemekAdi=" + yemekAdi + "&dusulecekDeger=" + dusulecekDeger + "&porsiyon=" + porsiyon + "&tur=" + tur);
        }

        private void komut_ikramIptal(string masa, string departmanAdi, string miktar, string yemekAdi, string siparisiGirenKisi, string dusulecekDegerGelen, ClientRef client, string adisyonNotu, string ikramYeniMiEskiMi, string porsiyon, string tur)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT OdemeYapiliyor,HesapIstendi FROM Adisyon WHERE AcikMi=1 AND IptalMi=0 AND MasaAdi=@_MasaAdi AND DepartmanAdi=@_DepartmanAdi");
            cmd.Parameters.AddWithValue("@_MasaAdi", masa);
            cmd.Parameters.AddWithValue("@_DepartmanAdi", departmanAdi);

            SqlDataReader dr = cmd.ExecuteReader();

            dr.Read();
            try
            {
                if (dr.GetBoolean(0))
                {
                    client.MesajYolla("komut=hesapIslemde");
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                    return;
                }
                else if (dr.GetBoolean(1))
                {
                    client.MesajYolla("komut=hesapGeliyor");
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                    return;
                }
            }
            catch
            { }

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            miktar = miktar.Replace('.', ',');
            dusulecekDegerGelen = dusulecekDegerGelen.Replace('.', ',');
            porsiyon = porsiyon.Replace('.', ',');

            bool turBool = false;

            if (tur == "K")
            {
                turBool = true;
            }

            if (siparisForm != null)
            {
                if (siparisForm.hangiMasaButonunaBasildi != null && siparisForm.viewdakiDepartmaninAdi != null)
                {
                    if (siparisForm.siparisMenuForm != null && siparisForm.viewdakiDepartmaninAdi == departmanAdi && siparisForm.hangiMasaButonunaBasildi.Text == masa)
                    {
                        //invoke thread ler arası haberleşme
                        this.Invoke((MethodInvoker)delegate
                        {
                            siparisForm.siparisMenuForm.ikramIptaliGeldi(miktar, yemekAdi, dusulecekDegerGelen, ikramYeniMiEskiMi, porsiyon, turBool);
                        });
                    }
                }
            }

            siparisiKimGirdi = siparisiGirenKisi;

            this.adisyonNotu = adisyonNotu;

            int istenilenIkramiptalSayisi = Convert.ToInt32(miktar);
            // ürünün değerini bul ve hesaba ekle
            double dusulecekDeger = Convert.ToDouble(dusulecekDegerGelen);

            cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adisyon.AdisyonID,Adet,Siparis.VerilisTarihi,NotificationGorulduMu FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi=1 AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + masa + "' AND Adisyon.DepartmanAdi='" + departmanAdi + "' AND Siparis.YemekAdi='" + yemekAdi + "' AND Siparis.Garsonu='" + siparisiGirenKisi + "' AND Siparis.Porsiyon=CONVERT(DECIMAL(5,2),@_Porsiyon) AND Siparis.KiloSatisiMi=@_Tur ORDER BY Adet DESC");

            cmd.Parameters.AddWithValue("@_Porsiyon", Convert.ToDecimal(porsiyon));
            cmd.Parameters.AddWithValue("@_Tur", turBool);

            dr = cmd.ExecuteReader();

            int siparisID, adisyonID = 0, adet;
            DateTime verilisTarihi;
            bool NotificationGorulduMu;

            while (dr.Read())
            {
                try
                {
                    siparisID = dr.GetInt32(0);
                    adisyonID = dr.GetInt32(1);
                    adet = dr.GetInt32(2);
                    verilisTarihi = dr.GetDateTime(3);
                    NotificationGorulduMu = dr.GetBoolean(4);
                }
                catch
                {
                    //HATA MESAJI GÖNDER
                    komut_IslemHatasi(client, "İkramı iptal etme gerçekleştirilemedi, lütfen tekrar deneyiniz");
                    return;
                }

                if (adet < istenilenIkramiptalSayisi) // elimizdeki ikramlar iptali istenenden küçükse
                {
                    ikramUpdateTam(siparisID, 0);

                    istenilenIkramiptalSayisi -= adet;
                }
                else if (adet > istenilenIkramiptalSayisi) // den büyükse
                {
                    ikramUpdateInsert(siparisID, adisyonID, adet, dusulecekDeger, istenilenIkramiptalSayisi, yemekAdi, 0, verilisTarihi, Convert.ToDecimal(porsiyon), turBool, NotificationGorulduMu);

                    istenilenIkramiptalSayisi = 0;
                }
                else // elimizde ikram edilmemişler ikramı istenene eşitse
                {
                    ikramUpdateTam(siparisID, 0);

                    istenilenIkramiptalSayisi = 0;
                }

                if (istenilenIkramiptalSayisi == 0)
                    break;
            }

            if (istenilenIkramiptalSayisi != 0)// ikram edilecekler daha bitmedi başka garsonların siparişlerinden ikram iptaline devam et
            {
                cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adisyon.AdisyonID,Adet,Siparis.VerilisTarihi,NotificationGorulduMu FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi=0 AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + masa + "' AND Adisyon.DepartmanAdi='" + departmanAdi + "' AND Siparis.YemekAdi='" + yemekAdi + "' AND Siparis.Garsonu!='" + siparisiKimGirdi + "' AND Siparis.Porsiyon=CONVERT(DECIMAL(5,2),@_Porsiyon) AND Siparis.KiloSatisiMi=@_Tur ORDER BY Adet DESC");

                cmd.Parameters.AddWithValue("@_Porsiyon", Convert.ToDecimal(porsiyon));
                cmd.Parameters.AddWithValue("@_Tur", turBool);

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    try
                    {
                        siparisID = dr.GetInt32(0);
                        adisyonID = dr.GetInt32(1);
                        adet = dr.GetInt32(2);
                        verilisTarihi = dr.GetDateTime(3);
                        NotificationGorulduMu = dr.GetBoolean(4);
                    }
                    catch
                    {
                        //HATA MESAJI GÖNDER
                        komut_IslemHatasi(client, "İkramı iptal etme gerçekleştirilemedi, lütfen tekrar deneyiniz");
                        return;
                    }

                    if (adet < istenilenIkramiptalSayisi) // elimizde ikram edilmemişler ikramı istenenden küçükse
                    {
                        ikramUpdateTam(siparisID, 0);

                        istenilenIkramiptalSayisi -= adet;
                    }
                    else if (adet > istenilenIkramiptalSayisi) // den büyükse
                    {
                        ikramUpdateInsert(siparisID, adisyonID, adet, dusulecekDeger, istenilenIkramiptalSayisi, yemekAdi, 0, verilisTarihi, Convert.ToDecimal(porsiyon), turBool, NotificationGorulduMu);

                        istenilenIkramiptalSayisi = 0;
                    }
                    else // elimizde ikram edilmemişler ikramı istenene eşitse
                    {
                        ikramUpdateTam(siparisID, 0);

                        istenilenIkramiptalSayisi = 0;
                    }
                    if (istenilenIkramiptalSayisi == 0)
                        break;
                }
            }

            if (adisyonNotu != null && adisyonNotu != "")
            {
                cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET AdisyonNotu=@adisyonNotu WHERE AdisyonID=@id");

                cmd.Parameters.AddWithValue("@adisyonNotu", adisyonNotu);
                cmd.Parameters.AddWithValue("@id", adisyonID);
                cmd.ExecuteNonQuery();
            }

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            //Tüm kullanıcılara ikram iptal mesajı gönderelim
            tumKullanicilaraMesajYolla("komut=ikramIptal&masa=" + masa + "&departmanAdi=" + departmanAdi + "&miktar=" + miktar + "&yemekAdi=" + yemekAdi + "&dusulecekDeger=" + dusulecekDeger + "&ikramYeniMiEskiMi=" + ikramYeniMiEskiMi + "&porsiyon=" + porsiyon + "&tur=" + tur);
        }

        //parametre hatalı istenilen işlem yapılamadı hatası ver
        private void komut_IslemHatasi(ClientRef client, string hata)
        {
            client.MesajYolla("komut=IslemHatasi&hata=" + hata);
        }

        //Adisyonun notunu değiştirme fonksiyonu
        private void komut_adisyonNotu(ClientRef client, string masa, string departmanAdi)
        {
            string adisyonNotu = "";

            //adisyonNotu'nu sql den al
            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT AdisyonNotu FROM Adisyon WHERE AcikMi=1 AND MasaAdi='" + masa + "' AND DepartmanAdi='" + departmanAdi + "'");
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            try
            {
                adisyonNotu = dr.GetString(0);
            }
            catch
            {
                adisyonNotu = "1";
            }

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            client.MesajYolla("komut=AdisyonNotu&adisyonNotu=" + adisyonNotu);
        }

        //siparis ekranı load olurken hesaba dair bilgileri gönderen method
        private void komut_loadSiparis(ClientRef client, string masa, string departmanAdi)
        {

            StringBuilder siparisBilgileri = new StringBuilder();
            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT Fiyatı, Adet, YemekAdi, IkramMi, Garsonu, Porsiyon, KiloSatisiMi from Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.MasaAdi='" + masa + "' and Adisyon.DepartmanAdi='" + departmanAdi + "' and Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 ORDER BY Adet DESC");
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                try
                {
                    siparisBilgileri.Append("*" + dr.GetDecimal(0).ToString() + "-" + dr.GetInt32(1).ToString() + "-" + dr.GetString(2) + "-" + dr.GetBoolean(3) + "-" + dr.GetString(4) + "-" + dr.GetDecimal(5) + "-" + dr.GetBoolean(6));
                }
                catch
                {
                    //HATA MESAJI GÖNDER
                    komut_IslemHatasi(client, "İşlem gerçekleştirilemedi, lütfen tekrar deneyiniz");
                    siparisBilgileri.Clear();
                    return;
                }
            }

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            if (siparisBilgileri.Length >= 1)
            {
                siparisBilgileri.Remove(0, 1);
            }

            client.MesajYolla("komut=LoadSiparis&siparisBilgileri=" + siparisBilgileri);
        }

        private void komut_masaAcildi(string masa, string departmanAdi)
        {
            if (siparisForm != null)
            {
                if (siparisForm.viewdakiDepartmaninAdi != null)
                {
                    if (siparisForm.viewdakiDepartmaninAdi == departmanAdi)
                    {
                        Button tablebutton = siparisForm.tablePanel.Controls[masa] as Button;
                        tablebutton.ForeColor = Color.White;
                        tablebutton.BackColor = Color.Firebrick;
                    }
                }
            }
            //Tüm kullanıcılara açılmak istenilen masayı gönderelim
            tumKullanicilaraMesajYolla("komut=masaAcildi&masa=" + masa + "&departmanAdi=" + departmanAdi);
        }

        private void komut_masayiAc(string masa, string departmanAdi)
        {
            bosAdisyonOlustur(masa, departmanAdi);
        }

        private void komut_masaKapandi(string masa, string departmanAdi)
        {
            if (siparisForm != null)
            {
                if (siparisForm.viewdakiDepartmaninAdi != null)
                {
                    if (siparisForm.viewdakiDepartmaninAdi == departmanAdi)
                    {
                        Button tablebutton = siparisForm.tablePanel.Controls[masa] as Button;
                        tablebutton.ForeColor = SystemColors.ActiveCaption;
                        tablebutton.BackColor = Color.White;
                        if (siparisForm.hangiMasaButonunaBasildi != null)
                        {
                            if (siparisForm.hangiMasaButonunaBasildi.Text == masa)
                            {
                                if (siparisForm.siparisMenuForm != null)
                                {
                                    if (siparisForm.siparisMenuForm.hesapForm != null)
                                    {
                                        this.Invoke((MethodInvoker)delegate
                                        {
                                            siparisForm.siparisMenuForm.hesapForm.Close();
                                        });
                                    }
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        siparisForm.siparisMenuForm.Close();
                                    });
                                }
                            }
                        }
                    }
                }
            }

            //Tüm kullanıcılara kapatılmak istenilen masayı gönderelim
            tumKullanicilaraMesajYolla("komut=masaKapandi&masa=" + masa + "&departmanAdi=" + departmanAdi);
        }

        // departman komutu- açık masa bilgilerini alan fonksiyon       
        private void komut_departman(ClientRef client, string departmanAdi, string komut, string masaDepartman = null)
        {
            StringBuilder acikMasalar = new StringBuilder();
            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT MasaAdi FROM Adisyon WHERE DepartmanAdi='" + departmanAdi + "' AND AcikMi=1");
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                try
                {
                    acikMasalar.Append("*" + dr.GetString(0));
                }
                catch
                {
                    acikMasalar.Clear();
                }
            }
            cmd.Connection.Close();
            cmd.Connection.Dispose();

            //İlk masanın başına konulan "*" metnini kaldır
            if (acikMasalar.Length >= 1)
            {
                acikMasalar.Remove(0, 1);
            }
            if (masaDepartman == null)
            {
                //Kullanıcıya istenilen departmanın açık kapalı masalarını gönderelim
                client.MesajYolla("komut=" + komut + "&masa=" + acikMasalar);
            }
            else
            {
                client.MesajYolla("komut=" + komut + "&masa=" + acikMasalar + "&masaDepartman=" + masaDepartman);
            }
        }

        // giris komutunu uygulayan fonksiyon        
        private void komut_giris(ClientRef client, string nick)
        {
            //Eşzamanlı erişimlere karşı koleksiyonu kilitleyelim
            lock (kullanicilar)
            {
                BagliKullanicilar kullanici = null;

            asd:
                //Tüm kullanıcıları tara, 
                //aynı nickli başkası varsa ismi değiştir
                foreach (BagliKullanicilar kul in kullanicilar)
                {
                    if (kul.Nick == nick)
                    {
                        Random x = new Random();
                        nick += x.Next(0, 10);
                        goto asd;
                    }
                }

                ////Nick kullanımdaysa clientye uygun dönüş mesajını verip çık
                //if (kullanici != null)
                //{
                //    client.MesajYolla("komut=giris&sonuc=basarisiz");
                //    return;
                //}

                //Tüm kullanıcıları tara,
                //aynı client zaten listede varsa sadece nickini güncelle
                foreach (BagliKullanicilar kul in kullanicilar)
                {
                    if (kul.Client == client)
                    {
                        kullanici = kul;
                        break;
                    }
                }
                //Client listede varsa sadece nickini güncelle
                if (kullanici != null)
                {
                    kullanici.Nick = nick;
                }
                //Listede yoksa listeye ekle
                else
                {
                    kullanicilar.Add(new BagliKullanicilar(client, nick));

                    if (kullanicilar.Count > Properties.Settings.Default.IP4B)
                    {
                        Properties.Settings.Default.IP4B = kullanicilar.Count;
                        Properties.Settings.Default.Save();
                    }
                }
            }
            //Kullanıcıya işlemin başarılı olduğu bilgisini gönder
            client.MesajYolla("komut=giris&sonuc=basarili");

            //Kullanıcı listesini ekranda gösterelim
            kullaniciListesiniYenile();
        }

        // toplumesaj komutunu uygulayan fonksyon        
        private void komut_toplumesaj(string mesaj)
        {
            //Tüm kullanıcılara istenilen mesajı gönderelim
            tumKullanicilaraMesajYolla("komut=toplumesaj&mesaj=" + mesaj);
        }

        // kullanıcılar listesindeki kullanıcıların nick'lerini ekranda gösterir.        
        private void kullaniciListesiniYenile()
        {
            //Eşzamanlı erişimlere karşı koleksiyonu kilitleyelim
            lock (kullanicilar)
            {
                StringBuilder nickler = new StringBuilder();
                //Tüm kullanıcıları tara, nickleri birleştir
                foreach (BagliKullanicilar kul in kullanicilar)
                {
                    nickler.Append(", " + kul.Nick);
                }
                //İlk kullanıcının başına konulan ", " metnini kaldır
                if (nickler.Length >= 2)
                {
                    nickler.Remove(0, 2);
                }
                //Nickleri göster
                textboxOnlineKullanicilar.Text = nickler.ToString();
            }
        }

        // kullanıcılar listesindeki tüm kullanıcılara istenilen bir mesajı iletir        
        public void tumKullanicilaraMesajYolla(string mesaj)
        {
            BagliKullanicilar[] kullaniciDizisi = null;
            //Eşzamanlı erişimlere karşı koleksiyonu kilitleyelim
            lock (kullanicilar)
            {
                //Listedeki tüm kullanıcıları bir diziye atalım
                kullaniciDizisi = kullanicilar.ToArray();
            }
            //Tüm kullanıcılara istenilen mesajı gönderelim
            foreach (BagliKullanicilar kul in kullaniciDizisi)
            {
                kul.Client.MesajYolla(mesaj);
            }
        }

        // cikis komutunu uygulayan fonksyon        
        private void komut_cikis(ClientRef client)
        {
            BagliKullanicilar kullanici = null;
            //Eşzamanlı erişimlere karşı koleksiyonu kilitleyelim
            lock (kullanicilar)
            {
                //Tüm kullanıcıları tara, client nesnesini bul
                foreach (BagliKullanicilar kul in kullanicilar)
                {
                    if (kul.Client == client)
                    {
                        kullanici = kul;
                        break;
                    }
                }
                //Client listede varsa listeden çıkar
                if (kullanici != null)
                {
                    kullanicilar.Remove(kullanici);
                    kullaniciListesiniYenile();
                }
                //Listede yoksa devam etmeye gerek yok, fonksiyondan çık
                else
                {
                    return;
                }
            }
            //Kullanıcı listesini ekranda gösterelim
            kullaniciListesiniYenile();
        }
        #endregion

        private NameValueCollection mesajCoz(string mesaj)
        {
            try
            {
                //& işaretine göre böl ve diziye at
                string[] parametreler = mesaj.Split('&');
                //dönüş değeri için bir NameValueCollection oluştur
                NameValueCollection nvcParametreler = new NameValueCollection(parametreler.Length);
                //bölünen her parametreyi = işaretine göre yeniden böl ve anahtar/değer çiftleri üret
                foreach (string parametre in parametreler)
                {
                    string[] esitlik = parametre.Split('=');
                    nvcParametreler.Add(esitlik[0], esitlik[1]);
                }
                //oluşturulan koleksiyonu dönder
                return nvcParametreler;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public class BagliKullanicilar
        {
            // SPIA kütüphanesindeki Client nesnesine referans            
            public ClientRef Client
            {
                get { return client; }
                set { client = value; }
            }
            private ClientRef client;

            // Kullanıcının Nick'i            
            public string Nick
            {
                get { return nick; }
                set { nick = value; }
            }
            private string nick;

            // Yeni bir Kullanıcı nesnesi oluşturur.            
            // <param name="client">SPIA kütüphanesindeki Client nesnesine referans</param>
            // <param name="nick">Kullanıcının Nick'i</param>
            public BagliKullanicilar(ClientRef client, string nick)
            {
                this.client = client;
                this.nick = nick;
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
            SendKeys.Send(e.KeyboardKeyPressed);
        }

        private void girisButtonPressed(object sender, EventArgs e)
        {
            if (adminForm == null)
            {
                if (!File.Exists("tempfiles.xml")) // ilk açılışta veya bir sıkıntı sonucu kategoriler dosyası silinirse kendi default kategorilerimizi giriyoruz.
                {
                    infoKullanici = new UItemp[1];

                    infoKullanici[0] = new UItemp();
                    infoKullanici[0].UIN = (new UnicodeEncoding()).GetBytes("Adınız");
                    infoKullanici[0].UIS = (new UnicodeEncoding()).GetBytes("Soy Adınız");
                    infoKullanici[0].UIUN = (new UnicodeEncoding()).GetBytes("admin");
                    infoKullanici[0].UIU = (new UnicodeEncoding()).GetBytes("Yönetici");

                    infoKullanici[0].UIPN = PasswordHash.CreateHash("0000");
                    infoKullanici[0].UIPW = PasswordHash.CreateHash("00000");
                    infoKullanici[0].UIY[0] = PasswordHash.CreateHash("true");
                    infoKullanici[0].UIY[1] = PasswordHash.CreateHash("true");
                    infoKullanici[0].UIY[2] = PasswordHash.CreateHash("true");
                    infoKullanici[0].UIY[3] = PasswordHash.CreateHash("true");
                    infoKullanici[0].UIY[4] = PasswordHash.CreateHash("true");


                    XmlSave.SaveRestoran(infoKullanici, "tempfiles.xml");

                    File.SetAttributes("tempfiles.xml", FileAttributes.Archive);
                }
                XmlLoad<UItemp> loadInfoKullanicilar = new XmlLoad<UItemp>();
                infoKullanici = loadInfoKullanicilar.LoadRestoran("tempfiles.xml");


                string[] username = new string[1];
                username[0] = userNameTextBox.getNameText(); //name lazım olduğunda al
                string password = passwordTextBox.getPasswordText(); //password lazım olduğunda al 

                int kullaniciAdi = -5;

                if (username[0] == "ropisimiz" && password == "roproprop")
                {
                    XmlSave.SaveRestoran(username, "sonKullanici.xml");


                    try
                    {
                        Task reportTask = Task.Factory.StartNew(
                            () =>
                            {
                                adminForm = new AdminGirisFormu(this, true);
                                adminForm.ShowDialog();
                            }
                            , CancellationToken.None
                            , TaskCreationOptions.None
                            , TaskScheduler.FromCurrentSynchronizationContext()
                            );

                        reportTask.Wait();
                    }
                    catch
                    { }

                    //Task.Factory.StartNew(() => adminForm.ShowDialog());
                    //adminForm.Show();
                }
                else
                {
                    for (int i = 0; i < infoKullanici.Count(); i++)
                    {
                        if (username[0] == (new UnicodeEncoding()).GetString(infoKullanici[i].UIUN))
                        {
                            kullaniciAdi = i;
                            break;
                        }
                    }
                    if (kullaniciAdi != -5)
                    {
                        //bool flag = Helper.VerifyHash(password, "SHA512", infoKullanici[kullaniciAdi].UIPW);

                        bool flag = PasswordHash.ValidatePassword(password, infoKullanici[kullaniciAdi].UIPW);

                        bool adisyonDegistirebilirMi = false;

                        if (PasswordHash.ValidatePassword("true", infoKullanici[kullaniciAdi].UIY[3]))
                            adisyonDegistirebilirMi = true;

                        if (flag == true)
                        { //şifre doğru
                            XmlSave.SaveRestoran(username, "sonKullanici.xml");

                            try
                            {
                                Task reportTask = Task.Factory.StartNew(
                                    () =>
                                    {
                                        adminForm = new AdminGirisFormu(this, adisyonDegistirebilirMi);

                                        adminForm.ShowDialog();
                                    }
                                    , CancellationToken.None
                                    , TaskCreationOptions.None
                                    , TaskScheduler.FromCurrentSynchronizationContext()
                                    );

                                reportTask.Wait();
                            }
                            catch
                            { }
                            //Task.Factory.StartNew(() => adminForm.ShowDialog());
                        }
                        else
                        {
                            KontrolFormu dialog2 = new KontrolFormu("Yanlış kullanıcı adı/şifre girdiniz", false);
                            dialog2.Show();
                        }
                    }
                    else
                    {
                        KontrolFormu dialog2 = new KontrolFormu("Yanlış kullanıcı adı/şifre girdiniz", false);
                        dialog2.Show();
                    }
                }

                userNameTextBox = new WPF_UserControls.VerticalCenterTextBox();
                usernameBoxHost.Child = userNameTextBox;
                passwordTextBox = new WPF_UserControls.VerticalCenterPasswordBox();
                passwordBoxHost.Child = passwordTextBox;
            }
            else
            {
                this.SendToBack();
            }
        }

        private void siparisButtonPressed(object sender, EventArgs e)
        {
            if (!File.Exists("restoran.xml") || !File.Exists("sonKullanici.xml") || !File.Exists("kategoriler.xml") || !File.Exists("masaDizayn.xml") || !File.Exists("menu.xml") || !File.Exists("urunler.xml"))
            {
                KontrolFormu dialog2 = new KontrolFormu("Lütfen önce programı ayarları kullanarak yapılandırın", false);
                dialog2.Show();
                return;
            }

            if (siparisForm == null)
            {
                //sipariş ekranına geçilecek

                //Task.Factory.StartNew(() => siparisForm.ShowDialog());
                try
                {
                    Task reportTask = Task.Factory.StartNew(
                        () =>
                        {
                            siparisForm = new SiparisMasaFormu(kullanicilar, this);

                            siparisForm.ShowDialog();
                        }
                        , CancellationToken.None
                        , TaskCreationOptions.None
                        , TaskScheduler.FromCurrentSynchronizationContext()
                        );

                    reportTask.Wait();
                }
                catch
                { }
            }
            else
            {
                this.SendToBack();
            }
        }

        private void exitButtonPressed(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void timerSaat_Tick(object sender, EventArgs e)
        {
            labelSaat.Text = DateTime.Now.ToString("HH:mm:ss", new CultureInfo("tr-TR"));
        }

        private static string GetUniqueKey(int maxSize)
        {
            char[] chars = new char[62];
            chars =
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }

        public bool IsConnectedToInternet()
        {
            string host = "74.125.228.22";//gnail
            bool result = false;
            Ping p = new Ping();
            try
            {
                PingReply reply = p.Send(host, 3000);
                if (reply.Status == IPStatus.Success)
                    return true;
            }
            catch { }
            return result;
        }

        public SecureString convertToSecureString(string strPassword)
        {
            var secureStr = new SecureString();
            if (strPassword.Length > 0)
            {
                foreach (var c in strPassword.ToCharArray()) secureStr.AppendChar(c);
            }
            return secureStr;
        }

        private void yeniSifreYaratveGonder()
        {
            string[] keys = new string[adet];

            for (int i = 0; i < adet; i++)
            {
                keys[i] = GetUniqueKey(10);
            }

            bool mailGonderildi = false;

            while (!mailGonderildi)
            {
                if (IsConnectedToInternet())
                {
                    try
                    {
                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();

                        message.From = new MailAddress("restomasyon@gmail.com");
                        message.To.Add(new MailAddress("restomasyon@gmail.com"));

                        message.Subject = "" + Properties.Settings.Default.FirmaAdi;

                        for (int i = 0; i < adet; i++)
                        {
                            message.Body += keys[i] + "\n";
                        }

                        smtp.Port = 587;
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        SecureString sfr = new System.Security.SecureString();

                        sfr = convertToSecureString("Otomasyon23");
                        sfr.MakeReadOnly();

                        smtp.Credentials = new NetworkCredential("restomasyon@gmail.com", sfr);
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Send(message);
                        mailGonderildi = true;

                        for (int i = 0; i < adet; i++)
                        {
                            keys[i] = PasswordHash.CreateHash(keys[i]);
                            Properties.Settings.Default.IP2Check.Add(keys[i]);
                        }
                        Properties.Settings.Default.Save();
                    }
                    catch
                    {
                        KontrolFormu dialog = new KontrolFormu("Devam edebilmek için internet bağlantısı sağlamanız gerekmektedir, bağlantınızı yaptıktan sonra Tamam tuşuna basınız", false);
                        dialog.ShowDialog();
                    }
                }
                else
                {
                    KontrolFormu dialog = new KontrolFormu("Devam edebilmek için internet bağlantısı sağlamanız gerekmektedir, bağlantınızı yaptıktan sonra Tamam tuşuna basınız", false);
                    dialog.ShowDialog();
                }
            }
        }

        private bool sendConnections(int connections)
        {
            bool mailGonderildi = false;

            while (!mailGonderildi)
            {
                if (IsConnectedToInternet())
                {
                    try
                    {
                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();

                        message.From = new MailAddress("restomasyon@gmail.com");
                        message.To.Add(new MailAddress("restomasyon@gmail.com"));

                        if (Properties.Settings.Default.FirmaAdi == string.Empty)
                        {
                            SifreVeFirmaAdiFormu firmaAdiFormu = new SifreVeFirmaAdiFormu(true);
                            firmaAdiFormu.ShowDialog();

                            if (firmaAdiFormu.DialogResult == DialogResult.No)
                            {
                                return false;
                            }
                        }

                        message.Subject = "" + Properties.Settings.Default.FirmaAdi;

                        message.Body = "MBKS = " + Properties.Settings.Default.IP4B;

                        smtp.Port = 587;
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        SecureString sfr = new System.Security.SecureString();

                        sfr = convertToSecureString("Otomasyon23");
                        sfr.MakeReadOnly();

                        smtp.Credentials = new NetworkCredential("restomasyon@gmail.com", sfr);
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Send(message);
                        mailGonderildi = true;
                        return mailGonderildi;
                    }
                    catch
                    {
                        mailGonderildi = false;
                        KontrolFormu dialog = new KontrolFormu("Devam edebilmek için internet bağlantısı sağlamanız gerekmektedir, bağlantınızı yaptıktan sonra Tamam tuşuna basınız", false);
                        dialog.ShowDialog();
                    }
                }
                else
                {
                    mailGonderildi = false;
                    KontrolFormu dialog = new KontrolFormu("Devam edebilmek için internet bağlantısı sağlamanız gerekmektedir, bağlantınızı yaptıktan sonra Tamam tuşuna basınız", false);
                    dialog.ShowDialog();
                }
            }
            return mailGonderildi;
        }

        private void bagliKullaniciSayisiIlet()
        {
            try
            {
                string path = Application.StartupPath + "\\mbks.txt";

                System.IO.File.WriteAllText(path, "MBKS = " + Properties.Settings.Default.IP4B);

                DateTime dateOfBeginning = Properties.Settings.Default.IP4;

                if (dateOfBeginning == DateTime.Parse("01.01.2000"))
                {
                    dateOfBeginning = DateTime.Today;
                    Properties.Settings.Default.IP4 = DateTime.Today;
                    Properties.Settings.Default.Save();
                }

                if (dateOfBeginning.AddDays(60) < DateTime.Today)
                {
                    int connections = Properties.Settings.Default.IP4B;

                    bool gonderildiMi = sendConnections(connections);

                    if (!gonderildiMi)
                    {
                        System.Windows.Forms.Application.Exit();
                        return;
                    }

                    Properties.Settings.Default.IP4 = DateTime.Today;
                    Properties.Settings.Default.Save();
                }
            }
            catch
            { }
        }

        //Form Load
        private void GirisEkrani_Load(object sender, EventArgs e)
        {
            axCIDv51.Start();
            //Properties.Settings.Default.Reset();
            if (Properties.Settings.Default.FirmaAdi.Trim() == "")
            {
                SifreVeFirmaAdiFormu firmaAdiFormu = new SifreVeFirmaAdiFormu(true);
                firmaAdiFormu.ShowDialog();

                if (firmaAdiFormu.DialogResult == DialogResult.No)
                {
                    System.Windows.Forms.Application.Exit();
                    return;
                }
            }

            try
            {
                gecenSure = Properties.Settings.Default.Port2;

                DateTime x = new DateTime();
                if (gecenSure != -1)
                {
                    if ((DateTime.Now >= Properties.Settings.Default.IP2.AddDays(30) && Properties.Settings.Default.IP2 > x.AddDays(1)) || DateTime.Now < Properties.Settings.Default.IP2 || gecenSure >= 43200)
                    {
                        Properties.Settings.Default.IP2Check.RemoveAt(0);
                        Properties.Settings.Default.Port2 = -1;
                        Properties.Settings.Default.IP2 = default(DateTime);
                        Properties.Settings.Default.Save();
                    }
                }
            }
            catch
            { }

            string sifre = PasswordHash.CreateHash("warkilla");
            bool sifreKaldi = false;

            if (Properties.Settings.Default.IP2Check != null)
            {
                for (int i = 0; i < Properties.Settings.Default.IP2Check.Count; i++)
                {
                    if (Properties.Settings.Default.IP2Check[i] != null && Properties.Settings.Default.IP2Check[i] != "")
                    {
                        sifre = Properties.Settings.Default.IP2Check[i];
                        sifreKaldi = true;
                        break;
                    }
                    else
                    {
                        Properties.Settings.Default.IP2Check.RemoveAt(i);
                        Properties.Settings.Default.Port2 = -1;
                        Properties.Settings.Default.IP2 = default(DateTime);
                        i--;
                    }
                }
                Properties.Settings.Default.Save();
            }

            if (!sifreKaldi) // yeni şifre gönder
            {
                yeniSifreYaratveGonder();
            }

            if (Properties.Settings.Default.IP3 != "warkilla")
            {
                if (!PasswordHash.ValidatePassword(Properties.Settings.Default.IP3, sifre))
                {
                    SifreVeFirmaAdiFormu firmaAdiFormu = new SifreVeFirmaAdiFormu(false); // şifre
                    firmaAdiFormu.ShowDialog();

                    if (firmaAdiFormu.DialogResult == DialogResult.No)
                    {
                        System.Windows.Forms.Application.Exit();
                        return;
                    }
                    bagliKullaniciSayisiIlet();

                    Properties.Settings.Default.Port2 = 0;
                    Properties.Settings.Default.Save();
                }
            }
            else
            {
                bagliKullaniciSayisiIlet();
            }

            //SQL SERVER BAĞLANTI KONTROLÜ YAPILIYOR
            SqlConnection cnn;
            try
            {
                cnn = new SqlConnection("server=.;database=ropv1;integrated security=true");
                cnn.Open();
                cnn.Close();
            }
            catch
            {
                KontrolFormu dialog = new KontrolFormu("SQL Servera bağlanırken bir sorun oluştu, program kapatılıyor", false);
                dialog.ShowDialog();
                cnn = null;

                System.Windows.Forms.Application.Exit();
                return;
            }

            buttonConnection_Click(null, null);

            labelSaat.Text = DateTime.Now.ToString("HH:mm:ss", new CultureInfo("tr-TR"));
            timerSaat.Start();
            labelGun.Text = DateTime.Now.ToString("dddd", new CultureInfo("tr-TR"));
            labelTarih.Text = DateTime.Now.Date.ToString("d MMMM yyyy", new CultureInfo("tr-TR"));

            //açılışta capslock açıksa kapatıyoruz.
            ToggleCapsLock(false);

            //wpflerimizi oluşturduğumuz elementhostların childına atıyoruz
            userNameTextBox = new WPF_UserControls.VerticalCenterTextBox();
            usernameBoxHost.Child = userNameTextBox;
            passwordTextBox = new WPF_UserControls.VerticalCenterPasswordBox();
            passwordBoxHost.Child = passwordTextBox;
            if (File.Exists("urunler.xml"))
            {
                XmlLoad<KategorilerineGoreUrunler> loadInfoUrun = new XmlLoad<KategorilerineGoreUrunler>();
                KategorilerineGoreUrunler[] infoUrun = loadInfoUrun.LoadRestoran("urunler.xml");

                urunListesi.AddRange(infoUrun);
            }
            timer1.Start();
        }

        // IP - Port - Server Seçimi Ekranı 
        private void GirisEkrani_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.D3) //Kısayol Tuşları ile ekranı açıyoruz ctrl+shift+3
            {
                PortFormu portFormu = new PortFormu();
                portFormu.ShowDialog();
            }
        }

        private void GirisEkrani_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Form kapatılırken, sunucu çalışıyorsa durduralım.
            if (sunucu != null)
            {
                komut_toplumesaj("ServerKapandi");
                sunucu.Durdur();
                sunucu = null;
            }
            if (Properties.Settings.Default.Port2 != 0)
            {
                Properties.Settings.Default.Port2 = gecenSure;
                Properties.Settings.Default.Save();
            }
        }

        private void buttonConnection_Click(object sender, EventArgs e)
        {
            if (buttonConnection.Image != Properties.Resources.baglantiOK)
            {
                if (baslat())
                {
                    buttonConnection.Image = Properties.Resources.baglantiOK;
                }
                else
                {
                    buttonConnection.Image = Properties.Resources.baglantiYOK;
                    KontrolFormu dialog2 = new KontrolFormu("Hata! Sunucu başlatılamadı!", false);
                    dialog2.Show();
                    buttonConnection.Image = Properties.Resources.baglantiYOK;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Port2 != -1)
                gecenSure += 1;
        }

        #region SQL İşlemleri

        public void odendiUpdateTam(int siparisID)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET OdendiMi=1 WHERE SiparisID=@id");
            cmd.Parameters.AddWithValue("@id", siparisID);
            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void odendiUpdateInsert(int siparisID, int adisyonID, int adet, double fiyati, int odemeAdedi, string yemekAdi, DateTime verilisTarihi, decimal porsiyon, bool turBool, bool NotificationGorulduMu)
        {
            int yeniPorsiyonAdetiSiparis = adet - odemeAdedi;

            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET Adet = @_Adet, OdendiMi=1 WHERE SiparisID=@id");
            cmd.Parameters.AddWithValue("@_Adet", odemeAdedi);
            cmd.Parameters.AddWithValue("@id", siparisID);
            cmd.ExecuteNonQuery();

            bool urunYaziciyaBildirilmeliMi = yaziciBilgilendirilmeliMi(yemekAdi);
            string ciktiAlinanYazici = urunYazicisiniBul(yemekAdi);

            cmd = SQLBaglantisi.getCommand("INSERT INTO Siparis(AdisyonID,Garsonu,Fiyatı,Adet,YemekAdi,VerilisTarihi,CiktiAlindiMi,CiktiAlinmaliMi,Porsiyon,KiloSatisiMi,NotificationGorulduMu,CiktiAlinanYazici) values(@_AdisyonID,@_Garsonu,@_Fiyatı,@_Adet,@_YemekAdi,@_VerilisTarihi,@_CiktiAlindiMi,@_CiktiAlinmaliMi,@_Porsiyon,@_KiloSatisiMi,@_NotificationGorulduMu,@_CiktiAlinanYazici)");
            cmd.Parameters.AddWithValue("@_AdisyonID", adisyonID);
            cmd.Parameters.AddWithValue("@_Garsonu", siparisiKimGirdi);
            cmd.Parameters.AddWithValue("@_Fiyatı", fiyati);
            cmd.Parameters.AddWithValue("@_Adet", yeniPorsiyonAdetiSiparis);
            cmd.Parameters.AddWithValue("@_YemekAdi", yemekAdi);
            cmd.Parameters.AddWithValue("@_VerilisTarihi", verilisTarihi);
            cmd.Parameters.AddWithValue("@_CiktiAlindiMi", 1);
            cmd.Parameters.AddWithValue("@_CiktiAlinmaliMi", urunYaziciyaBildirilmeliMi);
            cmd.Parameters.AddWithValue("@_Porsiyon", porsiyon);
            cmd.Parameters.AddWithValue("@_CiktiAlinanYazici", ciktiAlinanYazici);

            if (turBool)
            {
                cmd.Parameters.AddWithValue("@_KiloSatisiMi", 1);
                cmd.Parameters.AddWithValue("@_NotificationGorulduMu", 1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@_KiloSatisiMi", 0);
                cmd.Parameters.AddWithValue("@_NotificationGorulduMu", NotificationGorulduMu);
            }

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        private bool yaziciBilgilendirilmeliMi(string yemekAdi)
        {
            for (int i = 0; i < urunListesi.Count(); i++)
            {
                for (int j = 0; j < urunListesi[i].urunYaziciyaBildirilmeliMi.Count; j++)
                {
                    if (urunListesi[i].urunAdi[j] == yemekAdi)
                    {
                        return urunListesi[i].urunYaziciyaBildirilmeliMi[j];
                    }
                }
            }
            return true;
        }

        private string urunYazicisiniBul(string yemekAdi)
        {
            for (int i = 0; i < urunListesi.Count(); i++)
            {
                for (int j = 0; j < urunListesi[i].urunYazicisi.Count; j++)
                {
                    if (urunListesi[i].urunAdi[j] == yemekAdi)
                    {
                        return urunListesi[i].urunYazicisi[j];
                    }
                }
            }
            return "";
        }

        public void ikramUpdateInsert(int siparisID, int adisyonID, int adet, double dusulecekDeger, int ikramAdedi, string yemekAdi, int ikramMi, DateTime verilisTarihi, decimal porsiyon, bool turBool, bool NotificationGorulduMu)
        {
            int yeniPorsiyonAdetiSiparis = adet - ikramAdedi;

            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET Adet = @_Adet WHERE SiparisID=@id");
            cmd.Parameters.AddWithValue("@_Adet", yeniPorsiyonAdetiSiparis);
            cmd.Parameters.AddWithValue("@id", siparisID);
            cmd.ExecuteNonQuery();

            bool urunYaziciyaBildirilmeliMi = yaziciBilgilendirilmeliMi(yemekAdi);
            string ciktiAlinanYazici = urunYazicisiniBul(yemekAdi);

            cmd = SQLBaglantisi.getCommand("INSERT INTO Siparis(AdisyonID,Garsonu,Fiyatı,Adet,YemekAdi,IkramMi,VerilisTarihi,CiktiAlindiMi,CiktiAlinmaliMi,Porsiyon,KiloSatisiMi,NotificationGorulduMu,CiktiAlinanYazici) values(@_AdisyonID,@_Garsonu,@_Fiyatı,@_Adet,@_YemekAdi,@_IkramMi,@_VerilisTarihi,@_CiktiAlindiMi,@_CiktiAlinmaliMi,@_Porsiyon,@_KiloSatisiMi,@_NotificationGorulduMu,@_CiktiAlinanYazici)");

            cmd.Parameters.AddWithValue("@_AdisyonID", adisyonID);
            cmd.Parameters.AddWithValue("@_Garsonu", siparisiKimGirdi);
            cmd.Parameters.AddWithValue("@_Fiyatı", dusulecekDeger);
            cmd.Parameters.AddWithValue("@_Adet", ikramAdedi);
            cmd.Parameters.AddWithValue("@_YemekAdi", yemekAdi);
            cmd.Parameters.AddWithValue("@_IkramMi", ikramMi);
            cmd.Parameters.AddWithValue("@_VerilisTarihi", DateTime.Now);
            cmd.Parameters.AddWithValue("@_CiktiAlindiMi", 1);
            cmd.Parameters.AddWithValue("@_CiktiAlinmaliMi", urunYaziciyaBildirilmeliMi);
            cmd.Parameters.AddWithValue("@_Porsiyon", porsiyon);
            cmd.Parameters.AddWithValue("@_CiktiAlinanYazici", ciktiAlinanYazici);

            if (turBool)
            {
                cmd.Parameters.AddWithValue("@_KiloSatisiMi", 1);
                cmd.Parameters.AddWithValue("@_NotificationGorulduMu", 1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@_KiloSatisiMi", 0);
                cmd.Parameters.AddWithValue("@_NotificationGorulduMu", NotificationGorulduMu);
            }

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void ikramUpdateTam(int siparisID, int ikramMi)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET IkramMi=@ikram, Garsonu=@Garson WHERE SiparisID=@id");
            cmd.Parameters.AddWithValue("@ikram", ikramMi);
            cmd.Parameters.AddWithValue("@Garson", siparisiKimGirdi);
            cmd.Parameters.AddWithValue("@id", siparisID);
            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void iptalUpdateInsert(int siparisID, int adisyonID, int adet, double dusulecekDeger, int iptalAdedi, string yemekAdi, DateTime verilisTarihi, decimal porsiyon, string iptalNedeni, bool tur)
        {
            int yeniPorsiyonAdetiSiparis = adet - iptalAdedi;

            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET Adet = @_Adet WHERE SiparisID=@id");
            cmd.Parameters.AddWithValue("@_Adet", yeniPorsiyonAdetiSiparis);
            cmd.Parameters.AddWithValue("@id", siparisID);
            cmd.ExecuteNonQuery();

            bool urunYaziciyaBildirilmeliMi = yaziciBilgilendirilmeliMi(yemekAdi);
            string ciktiAlinanYazici = urunYazicisiniBul(yemekAdi);

            if (tur)
            {
                cmd = SQLBaglantisi.getCommand("INSERT INTO Siparis(AdisyonID,Garsonu,Fiyatı,Adet,YemekAdi,IptalMi,VerilisTarihi,CiktiAlinmaliMi,Porsiyon,IptalNedeni,KiloSatisiMi,NotificationGorulduMu,CiktiAlinanYazici) values(@_AdisyonID,@_Garsonu,@_Fiyatı,@_Adet,@_YemekAdi,@_IptalMi,@_VerilisTarihi,@_CiktiAlinmaliMi,@_Porsiyon,@_IptalNedeni,@_KiloSatisiMi,@_NotificationGorulduMu,@_CiktiAlinanYazici)");
                cmd.Parameters.AddWithValue("@_AdisyonID", adisyonID);
                cmd.Parameters.AddWithValue("@_Garsonu", siparisiKimGirdi);
                cmd.Parameters.AddWithValue("@_Fiyatı", dusulecekDeger);
                cmd.Parameters.AddWithValue("@_Adet", iptalAdedi);
                cmd.Parameters.AddWithValue("@_YemekAdi", yemekAdi);
                cmd.Parameters.AddWithValue("@_IptalMi", 1);
                cmd.Parameters.AddWithValue("@_VerilisTarihi", verilisTarihi);
                cmd.Parameters.AddWithValue("@_CiktiAlinmaliMi", urunYaziciyaBildirilmeliMi);
                cmd.Parameters.AddWithValue("@_Porsiyon", porsiyon);
                cmd.Parameters.AddWithValue("@_IptalNedeni", iptalNedeni);
                cmd.Parameters.AddWithValue("@_KiloSatisiMi", 1);
                cmd.Parameters.AddWithValue("@_NotificationGorulduMu", 1);
                cmd.Parameters.AddWithValue("@_CiktiAlinanYazici", ciktiAlinanYazici);
            }
            else
            {
                cmd = SQLBaglantisi.getCommand("INSERT INTO Siparis(AdisyonID,Garsonu,Fiyatı,Adet,YemekAdi,IptalMi,VerilisTarihi,CiktiAlinmaliMi,Porsiyon,IptalNedeni,KiloSatisiMi,CiktiAlinanYazici) values(@_AdisyonID,@_Garsonu,@_Fiyatı,@_Adet,@_YemekAdi,@_IptalMi,@_VerilisTarihi,@_CiktiAlinmaliMi,@_Porsiyon,@_IptalNedeni,@_KiloSatisiMi,@_CiktiAlinanYazici)");
                cmd.Parameters.AddWithValue("@_AdisyonID", adisyonID);
                cmd.Parameters.AddWithValue("@_Garsonu", siparisiKimGirdi);
                cmd.Parameters.AddWithValue("@_Fiyatı", dusulecekDeger);
                cmd.Parameters.AddWithValue("@_Adet", iptalAdedi);
                cmd.Parameters.AddWithValue("@_YemekAdi", yemekAdi);
                cmd.Parameters.AddWithValue("@_IptalMi", 1);
                cmd.Parameters.AddWithValue("@_VerilisTarihi", verilisTarihi);
                cmd.Parameters.AddWithValue("@_CiktiAlinmaliMi", urunYaziciyaBildirilmeliMi);
                cmd.Parameters.AddWithValue("@_Porsiyon", porsiyon);
                cmd.Parameters.AddWithValue("@_IptalNedeni", iptalNedeni);
                cmd.Parameters.AddWithValue("@_KiloSatisiMi", 0);
<<<<<<< HEAD
=======
                cmd.Parameters.AddWithValue("@_CiktiAlinanYazici", ciktiAlinanYazici);
>>>>>>> origin/master
            }

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void iptalUpdateTam(int siparisID, string iptalNedeni)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET IptalMi=@_Iptal, Garsonu=@_Garson, IptalNedeni=@_IptalNedeni WHERE SiparisID=@_id");
            cmd.Parameters.AddWithValue("@_Iptal", 1);
            cmd.Parameters.AddWithValue("@_Garson", siparisiKimGirdi);
            cmd.Parameters.AddWithValue("@_IptalNedeni", iptalNedeni);
            cmd.Parameters.AddWithValue("@_id", siparisID);
            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public int bosAdisyonOlustur(string masaAdi, string departmanAdi)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("INSERT INTO Adisyon(AcikMi,AdisyonNotu,AcilisZamani,DepartmanAdi,MasaAdi) VALUES(@_acikMi,@_AdisyonNotu,@_AcilisZamani,@_DepartmanAdi,@_MasaAdi) SELECT SCOPE_IDENTITY()");

            cmd.Parameters.AddWithValue("@_acikmi", 1);
            cmd.Parameters.AddWithValue("@_AdisyonNotu", "");
            cmd.Parameters.AddWithValue("@_AcilisZamani", DateTime.Now);
            cmd.Parameters.AddWithValue("@_DepartmanAdi", departmanAdi);
            cmd.Parameters.AddWithValue("@_MasaAdi", masaAdi);
            int adisyonID = Convert.ToInt32(cmd.ExecuteScalar());

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            return adisyonID;
        }

        public void urunTasimaUpdateTam(int siparisID, int aktarimYapilacakMasaninAdisyonID)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET Garsonu=@Garson, AdisyonID=@adisyonId WHERE SiparisID=@id");
            cmd.Parameters.AddWithValue("@Garson", siparisiKimGirdi);
            cmd.Parameters.AddWithValue("@adisyonId", aktarimYapilacakMasaninAdisyonID);
            cmd.Parameters.AddWithValue("@id", siparisID);

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void urunTasimaUpdateInsert(int siparisID, int aktarimYapilacakMasaninAdisyonID, int adet, double dusulecekDeger, int tasinacakMiktar, string yemekAdi, int ikramMi, DateTime verilisTarihi, decimal porsiyon, bool turBool, bool NotificationGorulduMu)
        {
            int yeniPorsiyonAdetiSiparis = adet - tasinacakMiktar;

            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET Adet = @_Adet WHERE SiparisID=@id");
            cmd.Parameters.AddWithValue("@_Adet", yeniPorsiyonAdetiSiparis);
            cmd.Parameters.AddWithValue("@id", siparisID);
            cmd.ExecuteNonQuery();

            bool urunYaziciyaBildirilmeliMi = yaziciBilgilendirilmeliMi(yemekAdi);
            string ciktiAlinanYazici = urunYazicisiniBul(yemekAdi);

            cmd = SQLBaglantisi.getCommand("INSERT INTO Siparis(AdisyonID,Garsonu,Fiyatı,Adet,YemekAdi,IkramMi,VerilisTarihi,CiktiAlindiMi,CiktiAlinmaliMi,Porsiyon,KiloSatisiMi,NotificationGorulduMu,CiktiAlinanYazici) values(@_AdisyonID,@_Garsonu,@_Fiyatı,@_Adet,@_YemekAdi,@_IkramMi,@_VerilisTarihi,@_CiktiAlindiMi,@_CiktiAlinmaliMi,@_Porsiyon,@_KiloSatisiMi,@_NotificationGorulduMu,@_CiktiAlinanYazici)");
            cmd.Parameters.AddWithValue("@_AdisyonID", aktarimYapilacakMasaninAdisyonID);
            cmd.Parameters.AddWithValue("@_Garsonu", siparisiKimGirdi);
            cmd.Parameters.AddWithValue("@_Fiyatı", dusulecekDeger);
            cmd.Parameters.AddWithValue("@_Adet", tasinacakMiktar);
            cmd.Parameters.AddWithValue("@_YemekAdi", yemekAdi);
            cmd.Parameters.AddWithValue("@_IkramMi", ikramMi);
            cmd.Parameters.AddWithValue("@_VerilisTarihi", DateTime.Now);
            cmd.Parameters.AddWithValue("@_CiktiAlindiMi", 1);
            cmd.Parameters.AddWithValue("@_CiktiAlinmaliMi", urunYaziciyaBildirilmeliMi);
            cmd.Parameters.AddWithValue("@_Porsiyon", porsiyon);
            cmd.Parameters.AddWithValue("@_CiktiAlinanYazici", ciktiAlinanYazici);

            if (turBool)
            {
                cmd.Parameters.AddWithValue("@_KiloSatisiMi", 1);
                cmd.Parameters.AddWithValue("@_NotificationGorulduMu", 1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@_KiloSatisiMi", 0);
                cmd.Parameters.AddWithValue("@_NotificationGorulduMu", NotificationGorulduMu);
            }
            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        #endregion
    }
}