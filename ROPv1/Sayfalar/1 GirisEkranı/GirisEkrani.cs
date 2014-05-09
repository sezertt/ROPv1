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
using System.IO;
using System.Globalization;
using System.Collections.Specialized;
using System.Data.SqlClient;
using SPIA;
using SPIA.Server;
using ROPv1.BLL;
using ROPv1.Entity;


namespace ROPv1
{
    public partial class GirisEkrani : Form
    {
        // SPIA kütüphanesindeki sunucu nesnesi        
        private SPIAServer sunucu;

        // Sunucuya bağlı olan kullanıcıları saklayan liste        
        private List<BagliKullanicilar> kullanicilar;

        public List<string> son50Mesaj;

        public WPF_UserControls.VerticalCenterTextBox userNameTextBox;
        public WPF_UserControls.VerticalCenterPasswordBox passwordTextBox;

        public SiparisMasaFormu siparisForm;

        string siparisiKimGirdi, adisyonNotu;

        UItemp[] infoKullanici;

        public GirisEkrani()
        {
            kullanicilar = new List<BagliKullanicilar>();
            son50Mesaj = new List<string>();
            InitializeComponent();
        }

        // SPIA sunucusunu durdurur        
        private void durdur()
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
                        komut_siparis(parametreler["masa"], parametreler["departmanAdi"], parametreler["miktar"], parametreler["yemekAdi"], parametreler["siparisiGirenKisi"], parametreler["dusulecekDeger"], e.Client, parametreler["adisyonNotu"]);
                        break;
                    case "iptal": // ürün iptal edildiği bilgisini dağıtmak için
                        komut_iptal(parametreler["masa"], parametreler["departmanAdi"], parametreler["miktar"], parametreler["yemekAdi"], parametreler["siparisiGirenKisi"], parametreler["dusulecekDeger"], e.Client, parametreler["adisyonNotu"], parametreler["ikramYeniMiEskiMi"]);
                        break;
                    case "hesapOdeniyor": // yeni masa açıldığı bilgisi geldiğinde
                        komut_hesapOdeniyor(parametreler["masa"], parametreler["departmanAdi"]);
                        break;
                    case "OdemeBitti": // yeni masa açıldığı bilgisi geldiğinde
                        komut_hesapOdemeBitti(parametreler["masa"], parametreler["departmanAdi"], parametreler["odenmeyenSiparisVarMi"]);
                        break;
                    case "Indirim": // yeni masa açıldığı bilgisi geldiğinde
                        komut_hesapIndirim(parametreler["masa"], parametreler["departmanAdi"], parametreler["odemeTipi"], parametreler["odemeMiktari"], e.Client);
                        break;
                    case "masaGirilebilirMi": // yeni masa açıldığı bilgisi geldiğinde
                        komut_masaGirilebilirMi(parametreler["masa"], parametreler["departmanAdi"], e.Client);
                        break;
                    case "masaDegistir": // Masa değiştirmek ve bu bilgiyi diğer kullanıcılara bildirmek için
                        komut_masaDegistir(parametreler["yeniMasa"], parametreler["yeniDepartmanAdi"], parametreler["eskiMasa"], parametreler["eskiDepartmanAdi"], parametreler["yapilmasiGereken"]);
                        break;
                    case "ikram": // ürün ikram edildiği bilgisini dağıtmak için
                        komut_ikram(parametreler["masa"], parametreler["departmanAdi"], parametreler["miktar"], parametreler["yemekAdi"], parametreler["siparisiGirenKisi"], parametreler["dusulecekDeger"], e.Client, parametreler["adisyonNotu"]);
                        break;
                    case "ikramIptal": // ikramın iptal edildiği bilgisini dağıtmak için
                        komut_ikramIptal(parametreler["masa"], parametreler["departmanAdi"], parametreler["miktar"], parametreler["yemekAdi"], parametreler["siparisiGirenKisi"], parametreler["dusulecekDeger"], e.Client, parametreler["adisyonNotu"], parametreler["ikramYeniMiEskiMi"]);
                        break;
                    case "urunuTasi": // ikramın iptal edildiği bilgisini dağıtmak için
                        komut_urunuTasi(parametreler["masa"], parametreler["departmanAdi"], parametreler["yeniMasa"], parametreler["yeniDepartmanAdi"], parametreler["siparisiGirenKisi"], parametreler["aktarmaBilgileri"], e.Client);
                        break;
                    case "OdemeYapildi": // herhangi bir ödeme yapıldığında
                        komut_OdemeYapildi(parametreler["masa"], parametreler["departmanAdi"], parametreler["odemeTipi"], parametreler["odemeMiktari"], e.Client, parametreler["secilipOdenenSiparisBilgileri"]);
                        break;
                    case "giris": // bir kullanıcı servera bağlandığında
                        komut_giris(e.Client, parametreler["nick"]);
                        break;
                    case "LoadSiparis": // bir kullanıcı menü ekranını açmak istediğinde masada verilen siparişleri aktarmak için
                        komut_loadSiparis(e.Client, parametreler["masa"], parametreler["departmanAdi"]);
                        break;
                    case "OdenenleriGonder": // bir kullanıcı menü ekranını açmak istediğinde masada verilen siparişleri aktarmak için
                        komut_OdenenleriGonder(e.Client, parametreler["masa"], parametreler["departmanAdi"]);
                        break;
                    case "AdisyonNotu": // adisyon notu değiştirileceğinde eski adisyon notunu göstermek için
                        komut_adisyonNotu(e.Client, parametreler["masa"], parametreler["departmanAdi"]);
                        break;
                    case "departman"://departmanın masaları hakkında bilgi                 
                        komut_departman(e.Client, parametreler["departmanAdi"]);
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
                }
            }
            catch (Exception)
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

        private void komut_hesapIndirim(string masa, string departmanAdi, string odemeTipi, string odemeMiktari, ClientRef client)
        {                  
            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT AdisyonID FROM Adisyon WHERE AcikMi=1 AND MasaAdi='" + masa + "' AND DepartmanAdi='" + departmanAdi + "'");
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            int adisyonID = dr.GetInt32(0);

            cmd = SQLBaglantisi.getCommand("IF EXISTS (SELECT * FROM OdemeDetay WHERE AdisyonID='" + adisyonID + "' AND OdemeTipi='" + odemeTipi + "') UPDATE OdemeDetay SET OdenenMiktar='" + odemeMiktari + "' WHERE AdisyonID='" + adisyonID + "' AND OdemeTipi='" + odemeTipi + "' ELSE INSERT INTO OdemeDetay(AdisyonID,OdemeTipi,OdenenMiktar) VALUES(@_AdisyonID,@_OdemeTipi,@_OdenenMiktar)");

            cmd.Parameters.AddWithValue("@_AdisyonID", adisyonID);
            cmd.Parameters.AddWithValue("@_OdemeTipi", Convert.ToInt32(odemeTipi));
            cmd.Parameters.AddWithValue("@_OdenenMiktar", Convert.ToDecimal(odemeMiktari));

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            client.MesajYolla("komut=IndirimOnay&odemeTipi=" + odemeTipi + "&odemeMiktari=" + odemeMiktari);
        }

        private void komut_OdemeYapildi(string masa, string departmanAdi, string odemeTipi, string odemeMiktari, ClientRef client, string secilipOdenenSiparisBilgileri)
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

            //BURADA AKTARMALARDAKİ SİPARİŞLERİ UPDATE ET BOL VS. ikram iptaldeki gibi

            int adisyonID = -1;

            if (siparisler.Count() > 0 && siparisler[0] != "")
            {
                decimal kacPorsiyon;
                string yemeginAdi;
                decimal yemeginFiyati;

                for (int i = 0; i < siparisler.Count(); i++)
                {
                    string[] detaylari = siparisler[i].Split('-');
                    try
                    {
                        kacPorsiyon = Convert.ToDecimal(detaylari[0]);
                        yemeginAdi = detaylari[1];
                        yemeginFiyati = Convert.ToDecimal(detaylari[2]);
                    }
                    catch
                    {
                        break;
                    }

                    cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adisyon.AdisyonID,Porsiyon,Siparis.VerilisTarihi,Siparis.Garsonu FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi=0 AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + masa + "' AND Adisyon.DepartmanAdi='" + departmanAdi + "' AND Siparis.YemekAdi='" + yemeginAdi + "'ORDER BY Porsiyon DESC");

                    SqlDataReader dr = cmd.ExecuteReader();

                    int siparisID;
                    decimal porsiyon;
                    DateTime verilisTarihi;

                    while (dr.Read())
                    {
                        try
                        {
                            siparisID = dr.GetInt32(0);
                            adisyonID = dr.GetInt32(1);
                            porsiyon = dr.GetDecimal(2);
                            verilisTarihi = dr.GetDateTime(3);
                            siparisiKimGirdi = dr.GetString(4);

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

                        if (porsiyon < kacPorsiyon) // ödenmesi istenenlerin sayısı(kacPorsiyon) ödenebileceklerden(porsiyon) küçükse
                        {
                            odendiUpdateTam(siparisID);

                            kacPorsiyon -= porsiyon;
                        }
                        else if (porsiyon > kacPorsiyon) // den büyükse
                        {
                            odendiUpdateInsert(siparisID, adisyonID, porsiyon, (double)yemeginFiyati, kacPorsiyon, yemeginAdi, verilisTarihi);

                            kacPorsiyon = 0;
                        }
                        else // elimizde ikram edilmemişler ikramı istenene eşitse
                        {
                            odendiUpdateTam(siparisID);

                            kacPorsiyon = 0;
                        }
                        if (kacPorsiyon == 0)
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

            cmd = SQLBaglantisi.getCommand("INSERT INTO OdemeDetay(AdisyonID,OdemeTipi,OdenenMiktar) VALUES(@_AdisyonID,@_OdemeTipi,@_OdenenMiktar)");
            cmd.Parameters.AddWithValue("@_AdisyonID", adisyonID);
            cmd.Parameters.AddWithValue("@_OdemeTipi", Convert.ToInt32(odemeTipi));
            cmd.Parameters.AddWithValue("@_OdenenMiktar", Convert.ToDecimal(odemeMiktari));

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            client.MesajYolla("komut=OdemeOnay&odemeTipi=" + odemeTipi + "&odemeMiktari=" + odemeMiktari + "&secilipOdenenSiparisBilgileri=" + secilipOdenenSiparisBilgileri);
        }

        //hesap ekranı load olurken ödenen siparişlere dair bilgileri gönderen method
        private void komut_OdenenleriGonder(ClientRef client, string masa, string departmanAdi)
        {
            StringBuilder siparisBilgileri = new StringBuilder();
            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT Fiyatı, Porsiyon, YemekAdi from Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.MasaAdi='" + masa + "' and Adisyon.DepartmanAdi='" + departmanAdi + "' and Siparis.IptalMi=0 AND Siparis.OdendiMi=1 AND Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 ORDER BY Porsiyon DESC");
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                try
                {
                    siparisBilgileri.Append("*" + dr.GetDecimal(0).ToString() + "-" + dr.GetDecimal(1).ToString() + "-" + dr.GetString(2));
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
            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT OdemeYapiliyor FROM Adisyon WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Adisyon.MasaAdi='" + masa + "' AND Adisyon.DepartmanAdi='" + departmanAdi + "'");

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
            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET OdemeYapiliyor=@odemeYapiliyor WHERE AdisyonID=(SELECT AdisyonID FROM Adisyon WHERE AcikMi=1 AND MasaAdi='" + masa + "' AND DepartmanAdi='" + departmanAdi + "')");

            cmd.Parameters.AddWithValue("@odemeYapiliyor", 1);
            cmd.ExecuteNonQuery();

            if (odenmeyenSiparisVarMi == "0") // ödenmemiş sipariş yoksa siparişleri ödendi yap
            {
                cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET OdendiMi=1 WHERE AdisyonID=(SELECT AdisyonID FROM Adisyon WHERE AcikMi=1 AND MasaAdi='" + masa + "' AND DepartmanAdi='" + departmanAdi + "')");
                cmd.ExecuteNonQuery();
            }

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        private void komut_hesapOdeniyor(string masa, string departmanAdi)
        {
            //masanın adisyonundaki odemeyapiliyor bilgisini güncelle
            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET OdemeYapiliyor=@odemeYapiliyor WHERE AdisyonID=(SELECT AdisyonID FROM Adisyon WHERE AcikMi=1 AND MasaAdi='" + masa + "' AND DepartmanAdi='" + departmanAdi + "')");

            cmd.Parameters.AddWithValue("@odemeYapiliyor", 1);
            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            //eğer hesabı ödenmeye başlanan masa serverda açıksa
            if (siparisForm != null)
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

            string urunTasinirkenYeniMasaOlusturulduysaOlusanMasaninAdi = null;

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
            }

            for (int i = 0; i < aktarmalar.Count(); i++)
            {
                string yemekAdi;
                double dusulecekDeger;
                decimal istenilenTasimaMiktari;
                int tasinacakUrunIkramMi;

                string[] detaylari = aktarmalar[i].Split('-');
                yemekAdi = detaylari[0];
                dusulecekDeger = Convert.ToDouble(detaylari[1]);
                istenilenTasimaMiktari = Convert.ToDecimal(detaylari[2]);
                tasinacakUrunIkramMi = Convert.ToInt32(detaylari[3]);


                cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Porsiyon,Siparis.VerilisTarihi FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi='" + tasinacakUrunIkramMi + "' AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + MasaAdi + "' AND Adisyon.DepartmanAdi='" + departmanAdi + "' AND Siparis.YemekAdi='" + yemekAdi + "' AND Siparis.Garsonu='" + siparisiKimGirdi + "' ORDER BY Porsiyon DESC");

                dr = cmd.ExecuteReader();

                int siparisID;
                decimal porsiyon;
                DateTime verilisTarihi;

                while (dr.Read())
                {
                    try
                    {
                        siparisID = dr.GetInt32(0);
                        porsiyon = dr.GetDecimal(1);
                        verilisTarihi = dr.GetDateTime(2);
                    }
                    catch
                    {
                        //HATA MESAJI GÖNDER
                        komut_IslemHatasi(client, "Ürünü taşırken bir hata oluştu, lütfen tekrar deneyiniz");
                        return;
                    }

                    if (porsiyon < istenilenTasimaMiktari) // elimizde ikram edilmemişler ikramı istenenden küçükse
                    {
                        urunTasimaUpdateTam(siparisID, aktarilacakMasaninAdisyonID);

                        istenilenTasimaMiktari -= porsiyon;
                    }
                    else if (porsiyon > istenilenTasimaMiktari) // den büyükse
                    {
                        urunTasimaUpdateInsert(siparisID, aktarilacakMasaninAdisyonID, porsiyon, dusulecekDeger, istenilenTasimaMiktari, yemekAdi, tasinacakUrunIkramMi, verilisTarihi);

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
                    cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Porsiyon,Siparis.VerilisTarihi FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi='" + tasinacakUrunIkramMi + "' AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + MasaAdi + "' AND Adisyon.DepartmanAdi='" + departmanAdi + "' AND Siparis.YemekAdi='" + yemekAdi + "' AND Siparis.Garsonu!='" + siparisiKimGirdi + "' ORDER BY Porsiyon DESC");

                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        try
                        {
                            siparisID = dr.GetInt32(0);
                            porsiyon = dr.GetDecimal(1);
                            verilisTarihi = dr.GetDateTime(2);
                        }
                        catch
                        {
                            //HATA MESAJI GÖNDER
                            komut_IslemHatasi(client, "Ürünü taşırken bir hata oluştu, lütfen tekrar deneyiniz");
                            return;
                        }

                        if (porsiyon < istenilenTasimaMiktari) // elimizde ikram edilmemişler ikramı istenenden küçükse
                        {
                            urunTasimaUpdateTam(siparisID, aktarilacakMasaninAdisyonID);

                            istenilenTasimaMiktari -= porsiyon;
                        }
                        else if (porsiyon > istenilenTasimaMiktari) // den büyükse
                        {
                            urunTasimaUpdateInsert(siparisID, aktarilacakMasaninAdisyonID, porsiyon, dusulecekDeger, istenilenTasimaMiktari, yemekAdi, tasinacakUrunIkramMi, verilisTarihi);

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
            if (siparisForm != null && (siparisForm.viewdakiDepartmaninAdi == departmanAdi || siparisForm.viewdakiDepartmaninAdi == yeniDepartmanAdi))
            {
                if (urunTasinirkenYeniMasaOlusturulduysaOlusanMasaninAdi != null)
                {
                    Button tablebutton = siparisForm.tablePanel.Controls[urunTasinirkenYeniMasaOlusturulduysaOlusanMasaninAdi] as Button;
                    tablebutton.ForeColor = Color.White;
                    tablebutton.BackColor = Color.Firebrick;
                    tumKullanicilaraMesajYolla("komut=masaAcildi&masa=" + urunTasinirkenYeniMasaOlusturulduysaOlusanMasaninAdi + "&departmanAdi=" + departmanAdi);
                }
                if (siparisForm.siparisMenuForm != null && (siparisForm.hangiMasaButonunaBasildi.Text == yeniMasa || siparisForm.hangiMasaButonunaBasildi.Text == MasaAdi))
                {
                    //invoke thread ler arası haberleşme
                    this.Invoke((MethodInvoker)delegate
                    {
                        siparisForm.menuFormunuKapat(MasaAdi, yeniDepartmanAdi, yeniMasa);
                    });
                }
            }
            //Tüm kullanıcılara ürün taşındı mesajı gönderelim
            tumKullanicilaraMesajYolla("komut=urunTasindi&masa=" + MasaAdi + "&departmanAdi=" + departmanAdi + "&yeniMasa=" + yeniMasa + "&yeniDepartmanAdi=" + yeniDepartmanAdi);
        }

        private void komut_masaDegistir(string yeniMasa, string yeniDepartmanAdi, string eskiMasa, string eskiDepartmanAdi, string yapilmasiGereken)
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

            if (siparisForm.siparisMenuForm != null)
            {
                //invoke thread ler arası haberleşme
                this.Invoke((MethodInvoker)delegate
                {
                    siparisForm.komut_masaDegisti(eskiMasa, eskiDepartmanAdi, yeniMasa, yeniDepartmanAdi, "masaDegistir");
                });
            }

            //Tüm kullanıcılara masa değiştir mesajı gönderelim
            tumKullanicilaraMesajYolla("komut=masaDegistir&masa=" + eskiMasa + "&departmanAdi=" + eskiDepartmanAdi + "&yeniMasa=" + yeniMasa + "&yeniDepartmanAdi=" + yeniDepartmanAdi);
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

        private void komut_siparis(string masa, string departmanAdi, string miktar, string yemekAdi, string siparisiGirenKisi, string dusulecekDegerGelen, ClientRef client, string adisyonNotuGelen)
        {
            if (siparisForm != null)
            {
                if (siparisForm.siparisMenuForm != null && siparisForm.viewdakiDepartmaninAdi == departmanAdi && siparisForm.hangiMasaButonunaBasildi.Text == masa)
                {
                    //invoke thread ler arası haberleşme
                    this.Invoke((MethodInvoker)delegate
                    {
                        siparisForm.siparisMenuForm.siparisOnayiGeldi(miktar, yemekAdi, dusulecekDegerGelen);
                    });
                }
            }


            siparisiKimGirdi = siparisiGirenKisi;

            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT AdisyonID FROM Adisyon WHERE MasaAdi='" + masa + "' AND DepartmanAdi='" + departmanAdi + "' AND AcikMi=1");
            SqlDataReader dr = cmd.ExecuteReader();
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

            cmd = SQLBaglantisi.getCommand("INSERT INTO Siparis(AdisyonID,Garsonu,Fiyatı,Porsiyon,YemekAdi,VerilisTarihi) VALUES(@_AdisyonID,@_Garsonu,@_Fiyatı,@_Porsiyon,@_YemekAdi,@_VerilisTarihi)");
            cmd.Parameters.AddWithValue("@_AdisyonID", adisyonID);
            cmd.Parameters.AddWithValue("@_Garsonu", siparisiKimGirdi);
            cmd.Parameters.AddWithValue("@_Fiyatı", Convert.ToDecimal(dusulecekDegerGelen));
            cmd.Parameters.AddWithValue("@_Porsiyon", Convert.ToDecimal(miktar));
            cmd.Parameters.AddWithValue("@_YemekAdi", yemekAdi);
            cmd.Parameters.AddWithValue("@_VerilisTarihi", DateTime.Now);

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

            //Tüm kullanıcılara sipariş mesajı gönderelim
            tumKullanicilaraMesajYolla("komut=siparis&masa=" + masa + "&departmanAdi=" + departmanAdi + "&miktar=" + miktar + "&yemekAdi=" + yemekAdi + "&dusulecekDeger=" + dusulecekDegerGelen);
        }

        private void komut_listeBos(string masa, string departmanAdi)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Adisyon SET AcikMi=0, IptalMi=1, KapanisZamani=@date WHERE AdisyonID=(SELECT AdisyonID FROM Adisyon WHERE AcikMi=1 AND MasaAdi='" + masa + "' AND DepartmanAdi='" + departmanAdi + "')");
            cmd.Parameters.AddWithValue("@date", DateTime.Now);

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        private void komut_iptal(string masa, string departmanAdi, string miktar, string yemekAdi, string siparisiGirenKisi, string dusulecekDegerGelen, ClientRef client, string adisyonNotu, string ikraminGrubu)
        {
            if (siparisForm != null)
            {
                if (siparisForm.siparisMenuForm != null && siparisForm.viewdakiDepartmaninAdi == departmanAdi && siparisForm.hangiMasaButonunaBasildi.Text == masa)
                {
                    //invoke thread ler arası haberleşme
                    this.Invoke((MethodInvoker)delegate
                    {
                        siparisForm.siparisMenuForm.iptalGeldi(miktar, yemekAdi, dusulecekDegerGelen, ikraminGrubu);
                    });
                }
            }

            siparisiKimGirdi = siparisiGirenKisi;

            decimal istenilenSiparisiptalSayisi = Convert.ToDecimal(miktar);

            this.adisyonNotu = adisyonNotu;

            double dusulecekDeger = Convert.ToDouble(dusulecekDegerGelen);

            SqlCommand cmd;

            string ikramSQLGirdisi;

            if (ikraminGrubu == "0" || ikraminGrubu == "1") // eski ikram veya yeni ikram grubundaysa, ikramlardan iptal edilecek
                ikramSQLGirdisi = "1";
            else // değilse siparişlerden ikram edilecek
                ikramSQLGirdisi = "0";

            cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adisyon.AdisyonID,Porsiyon,Siparis.VerilisTarihi FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi='" + ikramSQLGirdisi + "' AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + masa + "' AND Adisyon.DepartmanAdi='" + departmanAdi + "' AND Siparis.YemekAdi='" + yemekAdi + "' AND Siparis.Garsonu='" + siparisiGirenKisi + "' ORDER BY Porsiyon DESC");

            SqlDataReader dr = cmd.ExecuteReader();

            int siparisID, adisyonID = 0;
            decimal porsiyon;
            DateTime verilisTarihi;

            while (dr.Read())
            {
                try
                {
                    siparisID = dr.GetInt32(0);
                    adisyonID = dr.GetInt32(1);
                    porsiyon = dr.GetDecimal(2);
                    verilisTarihi = dr.GetDateTime(3);
                }
                catch
                {
                    //HATA MESAJI GÖNDER
                    komut_IslemHatasi(client, "İptal etme gerçekleştirilemedi, lütfen tekrar deneyiniz");
                    return;
                }

                if (porsiyon < istenilenSiparisiptalSayisi) // elimizdeki siparişler iptali istenenden küçükse
                {
                    iptalUpdateTam(siparisID);

                    istenilenSiparisiptalSayisi -= porsiyon;
                }
                else if (porsiyon > istenilenSiparisiptalSayisi) // den büyükse
                {
                    iptalUpdateInsert(siparisID, adisyonID, porsiyon, dusulecekDeger, istenilenSiparisiptalSayisi, yemekAdi, verilisTarihi);

                    istenilenSiparisiptalSayisi = 0;
                }
                else // elimizdeki siparişler iptali istenene eşitse
                {
                    iptalUpdateTam(siparisID);

                    istenilenSiparisiptalSayisi = 0;
                }
                if (istenilenSiparisiptalSayisi == 0)
                    break;
            }

            if (istenilenSiparisiptalSayisi != 0)// iptal edilecekler daha bitmedi başka garsonların siparişlerinden iptale devam et
            {
                cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adisyon.AdisyonID,Porsiyon,Siparis.VerilisTarihi FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi='" + ikramSQLGirdisi + "' AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + masa + "' AND Adisyon.DepartmanAdi='" + departmanAdi + "' AND Siparis.YemekAdi='" + yemekAdi + "' AND Siparis.Garsonu!='" + siparisiGirenKisi + "' ORDER BY Porsiyon DESC");

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    try
                    {
                        siparisID = dr.GetInt32(0);
                        adisyonID = dr.GetInt32(1);
                        porsiyon = dr.GetDecimal(2);
                        verilisTarihi = dr.GetDateTime(3);
                    }
                    catch
                    {
                        //HATA MESAJI GÖNDER
                        komut_IslemHatasi(client, "İptal etme gerçekleştirilemedi, lütfen tekrar deneyiniz");
                        return;
                    }

                    if (porsiyon < istenilenSiparisiptalSayisi) // elimizdeki siparişler iptali istenenden küçükse
                    {
                        iptalUpdateTam(siparisID);

                        istenilenSiparisiptalSayisi -= porsiyon;
                    }
                    else if (porsiyon > istenilenSiparisiptalSayisi) // den büyükse
                    {
                        iptalUpdateInsert(siparisID, adisyonID, porsiyon, dusulecekDeger, istenilenSiparisiptalSayisi, yemekAdi, verilisTarihi);

                        istenilenSiparisiptalSayisi = 0;
                    }
                    else // elimizdeki siparişler iptali istenene eşitse
                    {
                        iptalUpdateTam(siparisID);

                        istenilenSiparisiptalSayisi = 0;
                    }
                    if (istenilenSiparisiptalSayisi == 0)
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

            //Tüm kullanıcılara iptal mesajı gönderelim
            tumKullanicilaraMesajYolla("komut=iptal&masa=" + masa + "&departmanAdi=" + departmanAdi + "&miktar=" + miktar + "&yemekAdi=" + yemekAdi + "&dusulecekDeger=" + dusulecekDeger + "&ikramYeniMiEskiMi=" + ikraminGrubu);
        }

        private void komut_ikram(string masa, string departmanAdi, string miktar, string yemekAdi, string siparisiGirenKisi, string dusulecekDegerGelen, ClientRef client, string adisyonNotu)
        {
            if (siparisForm != null)
            {
                if (siparisForm.siparisMenuForm != null && siparisForm.viewdakiDepartmaninAdi == departmanAdi && siparisForm.hangiMasaButonunaBasildi.Text == masa)
                {
                    //invoke thread ler arası haberleşme
                    this.Invoke((MethodInvoker)delegate
                    {
                        siparisForm.siparisMenuForm.ikramGeldi(miktar, yemekAdi, dusulecekDegerGelen);
                    });
                }
            }

            siparisiKimGirdi = siparisiGirenKisi;

            this.adisyonNotu = adisyonNotu;

            decimal istenilenikramSayisi = Convert.ToDecimal(miktar);

            // ürünün değerini istenilen kadar azalt, kalan hesaptan düş
            double dusulecekDeger = Convert.ToDouble(dusulecekDegerGelen);

            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adisyon.AdisyonID,Porsiyon,Siparis.VerilisTarihi FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi=0 AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + masa + "' AND Adisyon.DepartmanAdi='" + departmanAdi + "' AND Siparis.YemekAdi='" + yemekAdi + "' AND Siparis.Garsonu='" + siparisiGirenKisi + "' ORDER BY Porsiyon DESC");

            SqlDataReader dr = cmd.ExecuteReader();

            int siparisID, adisyonID = 0;
            decimal porsiyon;
            DateTime verilisTarihi;

            while (dr.Read())
            {
                try
                {
                    siparisID = dr.GetInt32(0);
                    adisyonID = dr.GetInt32(1);
                    porsiyon = dr.GetDecimal(2);
                    verilisTarihi = dr.GetDateTime(3);
                }
                catch
                {
                    //HATA MESAJI GÖNDER
                    komut_IslemHatasi(client, "İkram etme gerçekleştirilemedi, lütfen tekrar deneyiniz");
                    return;
                }

                if (porsiyon < istenilenikramSayisi) // elimizde ikram edilmemişler ikramı istenenden küçükse
                {
                    ikramUpdateTam(siparisID, 1);

                    istenilenikramSayisi -= porsiyon;
                }
                else if (porsiyon > istenilenikramSayisi) // den büyükse
                {
                    ikramUpdateInsert(siparisID, adisyonID, porsiyon, dusulecekDeger, istenilenikramSayisi, yemekAdi, 1, verilisTarihi);

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
                cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adisyon.AdisyonID,Porsiyon,Siparis.VerilisTarihi FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi=0 AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + masa + "' AND Adisyon.DepartmanAdi='" + departmanAdi + "' AND Siparis.YemekAdi='" + yemekAdi + "' AND Siparis.Garsonu!='" + siparisiGirenKisi + "' ORDER BY Porsiyon DESC");

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    try
                    {
                        siparisID = dr.GetInt32(0);
                        adisyonID = dr.GetInt32(1);
                        porsiyon = dr.GetDecimal(2);
                        verilisTarihi = dr.GetDateTime(3);
                    }
                    catch
                    {
                        //HATA MESAJI GÖNDER
                        komut_IslemHatasi(client, "İkram etme gerçekleştirilemedi, lütfen tekrar deneyiniz");
                        return;
                    }

                    if (porsiyon < istenilenikramSayisi) // elimizde ikram edilmemişler ikramı istenenden küçükse
                    {
                        ikramUpdateTam(siparisID, 1);

                        istenilenikramSayisi -= porsiyon;
                    }
                    else if (porsiyon > istenilenikramSayisi) // den büyükse
                    {
                        ikramUpdateInsert(siparisID, adisyonID, porsiyon, dusulecekDeger, istenilenikramSayisi, yemekAdi, 1, verilisTarihi);

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
            tumKullanicilaraMesajYolla("komut=ikram&masa=" + masa + "&departmanAdi=" + departmanAdi + "&miktar=" + miktar + "&yemekAdi=" + yemekAdi + "&dusulecekDeger=" + dusulecekDeger);
        }

        private void komut_ikramIptal(string masa, string departmanAdi, string miktar, string yemekAdi, string siparisiGirenKisi, string dusulecekDegerGelen, ClientRef client, string adisyonNotu, string ikramYeniMiEskiMi)
        {
            if (siparisForm != null)
            {
                if (siparisForm.siparisMenuForm != null && siparisForm.viewdakiDepartmaninAdi == departmanAdi && siparisForm.hangiMasaButonunaBasildi.Text == masa)
                {
                    //invoke thread ler arası haberleşme
                    this.Invoke((MethodInvoker)delegate
                    {
                        siparisForm.siparisMenuForm.ikramIptaliGeldi(miktar, yemekAdi, dusulecekDegerGelen, ikramYeniMiEskiMi);
                    });
                }
            }

            siparisiKimGirdi = siparisiGirenKisi;

            this.adisyonNotu = adisyonNotu;

            decimal istenilenIkramiptalSayisi = Convert.ToDecimal(miktar);
            // ürünün değerini bul ve hesaba ekle
            double dusulecekDeger = Convert.ToDouble(dusulecekDegerGelen);

            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adisyon.AdisyonID,Porsiyon,Siparis.VerilisTarihi FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi=1 AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + masa + "' AND Adisyon.DepartmanAdi='" + departmanAdi + "' AND Siparis.YemekAdi='" + yemekAdi + "' AND Siparis.Garsonu='" + siparisiGirenKisi + "' ORDER BY Porsiyon DESC");

            SqlDataReader dr = cmd.ExecuteReader();

            int siparisID, adisyonID = 0;
            decimal porsiyon;
            DateTime verilisTarihi;

            while (dr.Read())
            {
                try
                {
                    siparisID = dr.GetInt32(0);
                    adisyonID = dr.GetInt32(1);
                    porsiyon = dr.GetDecimal(2);
                    verilisTarihi = dr.GetDateTime(3);
                }
                catch
                {
                    //HATA MESAJI GÖNDER
                    komut_IslemHatasi(client, "İkramı iptal etme gerçekleştirilemedi, lütfen tekrar deneyiniz");
                    return;
                }

                if (porsiyon < istenilenIkramiptalSayisi) // elimizdeki ikramlar iptali istenenden küçükse
                {
                    ikramUpdateTam(siparisID, 0);

                    istenilenIkramiptalSayisi -= porsiyon;
                }
                else if (porsiyon > istenilenIkramiptalSayisi) // den büyükse
                {
                    ikramUpdateInsert(siparisID, adisyonID, porsiyon, dusulecekDeger, istenilenIkramiptalSayisi, yemekAdi, 0, verilisTarihi);

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
                cmd = SQLBaglantisi.getCommand("SELECT SiparisID,Adisyon.AdisyonID,Porsiyon,Siparis.VerilisTarihi FROM Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 AND Siparis.IkramMi=0 AND Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.MasaAdi='" + masa + "' AND Adisyon.DepartmanAdi='" + departmanAdi + "' AND Siparis.YemekAdi='" + yemekAdi + "' AND Siparis.Garsonu!='" + siparisiKimGirdi + "' ORDER BY Porsiyon DESC");

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    try
                    {
                        siparisID = dr.GetInt32(0);
                        adisyonID = dr.GetInt32(1);
                        porsiyon = dr.GetDecimal(2);
                        verilisTarihi = dr.GetDateTime(3);
                    }
                    catch
                    {
                        //HATA MESAJI GÖNDER
                        komut_IslemHatasi(client, "İkramı iptal etme gerçekleştirilemedi, lütfen tekrar deneyiniz");
                        return;
                    }

                    if (porsiyon < istenilenIkramiptalSayisi) // elimizde ikram edilmemişler ikramı istenenden küçükse
                    {
                        ikramUpdateTam(siparisID, 0);

                        istenilenIkramiptalSayisi -= porsiyon;
                    }
                    else if (porsiyon > istenilenIkramiptalSayisi) // den büyükse
                    {
                        ikramUpdateInsert(siparisID, adisyonID, porsiyon, dusulecekDeger, istenilenIkramiptalSayisi, yemekAdi, 0, verilisTarihi);

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
            tumKullanicilaraMesajYolla("komut=ikramIptal&masa=" + masa + "&departmanAdi=" + departmanAdi + "&miktar=" + miktar + "&yemekAdi=" + yemekAdi + "&dusulecekDeger=" + dusulecekDeger + "&ikramYeniMiEskiMi=" + ikramYeniMiEskiMi);
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

            //Burada adisyonNotu'nu sql den al
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
            SqlCommand cmd = SQLBaglantisi.getCommand("SELECT Fiyatı, Porsiyon, YemekAdi, IkramMi, Garsonu from Siparis JOIN Adisyon ON Siparis.AdisyonID=Adisyon.AdisyonID WHERE Adisyon.MasaAdi='" + masa + "' and Adisyon.DepartmanAdi='" + departmanAdi + "' and Siparis.IptalMi=0 AND Siparis.OdendiMi=0 AND Adisyon.AcikMi=1 AND Adisyon.IptalMi=0 ORDER BY Porsiyon DESC");
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                try
                {
                    siparisBilgileri.Append("*" + dr.GetDecimal(0).ToString() + "-" + dr.GetDecimal(1).ToString() + "-" + dr.GetString(2) + "-" + dr.GetBoolean(3) + "-" + dr.GetString(4));
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
                if (siparisForm.viewdakiDepartmaninAdi == departmanAdi)
                {
                    Button tablebutton = siparisForm.tablePanel.Controls[masa] as Button;
                    tablebutton.ForeColor = Color.White;
                    tablebutton.BackColor = Color.Firebrick;
                }
            }
            //Tüm kullanıcılara açılmak istenilen masayı gönderelim
            tumKullanicilaraMesajYolla("komut=masaAcildi&masa=" + masa + "&departmanAdi=" + departmanAdi);
        }

        private void komut_masaKapandi(string masa, string departmanAdi)
        {
            if (siparisForm != null)
            {
                if (siparisForm.viewdakiDepartmaninAdi == departmanAdi)
                {
                    Button tablebutton = siparisForm.tablePanel.Controls[masa] as Button;
                    tablebutton.ForeColor = SystemColors.ActiveCaption;
                    tablebutton.BackColor = Color.White;
                }
            }
            //Tüm kullanıcılara kapatılmak istenilen masayı gönderelim
            tumKullanicilaraMesajYolla("komut=masaKapandi&masa=" + masa + "&departmanAdi=" + departmanAdi);
        }

        // departman komutu- açık masa bilgilerini alan fonksiyon       
        private void komut_departman(ClientRef client, string departmanAdi)
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

            //Kullanıcıya istenilen departmanın açık kapalı masalarını gönderelim
            client.MesajYolla("komut=departman&masa=" + acikMasalar);
        }

        // giris komutunu uygulayan fonksiyon        
        private void komut_giris(ClientRef client, string nick)
        {
            //Eşzamanlı erişimlere karşı koleksiyonu kilitleyelim
            lock (kullanicilar)
            {
                BagliKullanicilar kullanici = null;
                //Tüm kullanıcıları tara, 
                //aynı nickli başkası varsa giriş başarısızdır
                foreach (BagliKullanicilar kul in kullanicilar)
                {
                    if (kul.Nick == nick)
                    {
                        kullanici = kul;
                        break;
                    }
                }
                //Nick kullanımdaysa clientye uygun dönüş mesajını verip çık
                if (kullanici != null)
                {
                    client.MesajYolla("komut=giris&sonuc=basarisiz");
                    return;
                }
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
        private void tumKullanicilaraMesajYolla(string mesaj)
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
            //Tüm kullanıcılara bu kullanıcının çıkış yaptığı bilgisini gönder
            tumKullanicilaraMesajYolla("komut=kullanicicikis&nick=" + kullanici.Nick);
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
            if (!File.Exists("tempfiles.xml")) // ilk açılışta veya bir sıkıntı sonucu kategoriler dosyası silinirse kendi default kategorilerimizi giriyoruz.
            {
                infoKullanici = new UItemp[1];

                infoKullanici[0] = new UItemp();
                infoKullanici[0].UIN = (new UnicodeEncoding()).GetBytes("Adınız");
                infoKullanici[0].UIS = (new UnicodeEncoding()).GetBytes("Soy Adınız");
                infoKullanici[0].UIUN = (new UnicodeEncoding()).GetBytes("admin");
                infoKullanici[0].UIPN = Helper.ComputeHash("0000", "SHA512", null);
                infoKullanici[0].UIPW = Helper.ComputeHash("00000", "SHA512", null);
                infoKullanici[0].UIU = (new UnicodeEncoding()).GetBytes("Yönetici");
                infoKullanici[0].UIY[0] = Helper.ComputeHash("true", "SHA512", null);
                infoKullanici[0].UIY[1] = Helper.ComputeHash("true", "SHA512", null);
                infoKullanici[0].UIY[2] = Helper.ComputeHash("true", "SHA512", null);
                infoKullanici[0].UIY[3] = Helper.ComputeHash("true", "SHA512", null);
                infoKullanici[0].UIY[4] = Helper.ComputeHash("true", "SHA512", null);

                XmlSave.SaveRestoran(infoKullanici, "tempfiles.xml");

                File.SetAttributes("tempfiles.xml", FileAttributes.Archive | FileAttributes.Hidden | FileAttributes.ReadOnly);
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

                AdminGirisFormu adminForm = new AdminGirisFormu();
                Task.Factory.StartNew(() => adminForm.ShowDialog());
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
                    bool flag = Helper.VerifyHash(password, "SHA512", infoKullanici[kullaniciAdi].UIPW);
                    if (flag == true)
                    { //şifre doğru
                        XmlSave.SaveRestoran(username, "sonKullanici.xml");

                        AdminGirisFormu adminForm = new AdminGirisFormu();
                        Task.Factory.StartNew(() => adminForm.ShowDialog());
                        //adminForm.Show();
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

        private void siparisButtonPressed(object sender, EventArgs e)
        {
            if (!File.Exists("restoran.xml") || !File.Exists("sonKullanici.xml") || !File.Exists("kategoriler.xml") || !File.Exists("masaDizayn.xml") || !File.Exists("menu.xml") || !File.Exists("urunler.xml"))
            {
                KontrolFormu dialog2 = new KontrolFormu("Lütfen önce programı ayarları kullanarak yapılandırın", false);
                dialog2.Show();
                return;
            }

            //sipariş ekranına geçilecek
            siparisForm = new SiparisMasaFormu(kullanicilar);

            Task.Factory.StartNew(() => siparisForm.ShowDialog());
            //siparisForm.Show();
        }

        private void exitButtonPressed(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timerSaat_Tick(object sender, EventArgs e)
        {
            labelSaat.Text = DateTime.Now.ToString("HH:mm:ss", new CultureInfo("tr-TR"));
        }

        //Form Load
        private void GirisEkrani_Load(object sender, EventArgs e)
        {

            //SQL SERVER BAĞLANTI KONTROLÜ BURADA YAPILIYOR
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
                Application.Exit();
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
        }

        // IP - Port - Server Seçimi Ekranı 
        private void GirisEkrani_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.D3) //Kısayol Tuşları ile ekranı açıyoruz ctrl+shift+3
            {
                PortFormu portFormu = new PortFormu();
                portFormu.Show();
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

        #region SQL İşlemleri

        public void odendiUpdateTam(int siparisID)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET OdendiMi=1 WHERE SiparisID=@id");
            cmd.Parameters.AddWithValue("@id", siparisID);
            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void odendiUpdateInsert(int siparisID, int adisyonID, decimal porsiyon, double fiyati, decimal odemeAdedi, string yemekAdi, DateTime verilisTarihi)
        {
            decimal yeniPorsiyonAdetiSiparis = porsiyon - odemeAdedi;

            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET Porsiyon = @Porsiyonu, OdendiMi=1 WHERE SiparisID=@id");
            cmd.Parameters.AddWithValue("@Porsiyonu", odemeAdedi);
            cmd.Parameters.AddWithValue("@id", siparisID);
            cmd.ExecuteNonQuery();

            cmd = SQLBaglantisi.getCommand("INSERT INTO Siparis(AdisyonID,Garsonu,Fiyatı,Porsiyon,YemekAdi,VerilisTarihi) values(@_AdisyonID,@_Garsonu,@_Fiyatı,@_Porsiyon,@_YemekAdi,@_VerilisTarihi)");
            cmd.Parameters.AddWithValue("@_AdisyonID", adisyonID);
            cmd.Parameters.AddWithValue("@_Garsonu", siparisiKimGirdi);
            cmd.Parameters.AddWithValue("@_Fiyatı", fiyati);
            cmd.Parameters.AddWithValue("@_Porsiyon", yeniPorsiyonAdetiSiparis);
            cmd.Parameters.AddWithValue("@_YemekAdi", yemekAdi);
            cmd.Parameters.AddWithValue("@_VerilisTarihi", verilisTarihi);

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }


        public void ikramUpdateInsert(int siparisID, int adisyonID, decimal porsiyon, double dusulecekDeger, decimal ikramAdedi, string yemekAdi, int ikramMi, DateTime verilisTarihi)
        {
            decimal yeniPorsiyonAdetiSiparis = porsiyon - ikramAdedi;

            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET Porsiyon = @Porsiyonu WHERE SiparisID=@id");
            cmd.Parameters.AddWithValue("@Porsiyonu", yeniPorsiyonAdetiSiparis);
            cmd.Parameters.AddWithValue("@id", siparisID);
            cmd.ExecuteNonQuery();

            cmd = SQLBaglantisi.getCommand("INSERT INTO Siparis(AdisyonID,Garsonu,Fiyatı,Porsiyon,YemekAdi,IkramMi,VerilisTarihi) values(@_AdisyonID,@_Garsonu,@_Fiyatı,@_Porsiyon,@_YemekAdi,@_IkramMi,@_VerilisTarihi)");
            cmd.Parameters.AddWithValue("@_AdisyonID", adisyonID);
            cmd.Parameters.AddWithValue("@_Garsonu", siparisiKimGirdi);
            cmd.Parameters.AddWithValue("@_Fiyatı", dusulecekDeger);
            cmd.Parameters.AddWithValue("@_Porsiyon", ikramAdedi);
            cmd.Parameters.AddWithValue("@_YemekAdi", yemekAdi);
            cmd.Parameters.AddWithValue("@_IkramMi", ikramMi);
            cmd.Parameters.AddWithValue("@_VerilisTarihi", DateTime.Now);

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

        public void iptalUpdateInsert(int siparisID, int adisyonID, decimal porsiyon, double dusulecekDeger, decimal iptalAdedi, string yemekAdi, DateTime verilisTarihi)
        {
            decimal yeniPorsiyonAdetiSiparis = porsiyon - iptalAdedi;

            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET Porsiyon = @Porsiyonu WHERE SiparisID=@id");
            cmd.Parameters.AddWithValue("@Porsiyonu", yeniPorsiyonAdetiSiparis);
            cmd.Parameters.AddWithValue("@id", siparisID);
            cmd.ExecuteNonQuery();

            cmd = SQLBaglantisi.getCommand("INSERT INTO Siparis(AdisyonID,Garsonu,Fiyatı,Porsiyon,YemekAdi,IptalMi,VerilisTarihi) values(@_AdisyonID,@_Garsonu,@_Fiyatı,@_Porsiyon,@_YemekAdi,@_IptalMi,@_VerilisTarihi)");
            cmd.Parameters.AddWithValue("@_AdisyonID", adisyonID);
            cmd.Parameters.AddWithValue("@_Garsonu", siparisiKimGirdi);
            cmd.Parameters.AddWithValue("@_Fiyatı", dusulecekDeger);
            cmd.Parameters.AddWithValue("@_Porsiyon", iptalAdedi);
            cmd.Parameters.AddWithValue("@_YemekAdi", yemekAdi);
            cmd.Parameters.AddWithValue("@_IptalMi", 1);
            cmd.Parameters.AddWithValue("@_VerilisTarihi", DateTime.Now);

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void iptalUpdateTam(int siparisID)
        {
            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET IptalMi=@iptal, Garsonu=@Garson WHERE SiparisID=@id");
            cmd.Parameters.AddWithValue("@iptal", 1);
            cmd.Parameters.AddWithValue("@Garson", siparisiKimGirdi);
            cmd.Parameters.AddWithValue("@id", siparisID);
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

        public void urunTasimaUpdateInsert(int siparisID, int aktarimYapilacakMasaninAdisyonID, decimal porsiyon, double dusulecekDeger, decimal tasinacakMiktar, string yemekAdi, int ikramMi, DateTime verilisTarihi)
        {
            decimal yeniPorsiyonAdetiSiparis = porsiyon - tasinacakMiktar;

            SqlCommand cmd = SQLBaglantisi.getCommand("UPDATE Siparis SET Porsiyon = @Porsiyonu WHERE SiparisID=@id");
            cmd.Parameters.AddWithValue("@Porsiyonu", yeniPorsiyonAdetiSiparis);
            cmd.Parameters.AddWithValue("@id", siparisID);
            cmd.ExecuteNonQuery();

            cmd = SQLBaglantisi.getCommand("INSERT INTO Siparis(AdisyonID,Garsonu,Fiyatı,Porsiyon,YemekAdi,IkramMi,VerilisTarihi) values(@_AdisyonID,@_Garsonu,@_Fiyatı,@_Porsiyon,@_YemekAdi,@_IkramMi,@_VerilisTarihi)");
            cmd.Parameters.AddWithValue("@_AdisyonID", aktarimYapilacakMasaninAdisyonID);
            cmd.Parameters.AddWithValue("@_Garsonu", siparisiKimGirdi);
            cmd.Parameters.AddWithValue("@_Fiyatı", dusulecekDeger);
            cmd.Parameters.AddWithValue("@_Porsiyon", tasinacakMiktar);
            cmd.Parameters.AddWithValue("@_YemekAdi", yemekAdi);
            cmd.Parameters.AddWithValue("@_IkramMi", ikramMi);
            cmd.Parameters.AddWithValue("@_VerilisTarihi", DateTime.Now);

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }
        #endregion
    }
}
