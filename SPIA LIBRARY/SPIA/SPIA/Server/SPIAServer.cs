using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.IO;

namespace SPIA.Server
{
    
    /// Asenkron Soketli Mesajlaþma Sunucusu
    
    public class SPIAServer
    {
        // PUBLIC ÖZELLÝKLER //////////////////////////////////////////////////
        
        /// Dinlenen Port        
        public int Port
        {
            get { return port; }
        }
        private int port;

        // OLAYLAR ////////////////////////////////////////////////////////////
        
        /// Sunucuya yeni bir client baðlantýðýnda tetiklenir.        
        public event dgYeniClientBaglandi YeniClientBaglandi;
        
        /// Sunucuya baðlý bir client baðlantýsý kapatýldýðýnda tetiklenir.        
        public event dgClientBaglantisiKapatildi ClientBaglantisiKapatildi;
        
        /// Bir clientden yeni bir mesaj alýndýðýnda tetiklenir.        
        public event dgClientdanYeniMesajAlindi ClientdanYeniMesajAlindi;

        // PRIVATE ALANLAR ////////////////////////////////////////////////////
        
        /// En son baðlantý saðlanan client ID'si        
        private long sonClientID = 0;
        
        /// O anda baðlantý kurulmuþ olan clientlerin listesi        
        private SortedList<long, Client> clients;
        
        /// Sunucunun çalýþma durumu        
        private volatile bool calisiyor;
        
        /// Senkronizasyonda kullanýlacak nesne        
        private object objSenk = new object();
        
        /// Baðlantý dinleyen nesne        
        private BaglantiDinleyici baglantiDinleyici;

        // PUBLIC FONKSYONLAR /////////////////////////////////////////////////        
        
        /// Bir SPIA sunucusu oluþturur        
        /// <param name="port">Dinlenecek port</param>
        public SPIAServer(int port)
        {
            this.port = port;
            this.clients = new SortedList<long, Client>();
            this.baglantiDinleyici = new BaglantiDinleyici(this, port);
        }
        
        /// Sunucunun çalýþmasýný baþlatýr        
        public void Baslat()
        {
            //Eðer zaten çalýþýyorsa iþlem yapmadan çýk
            if (calisiyor)
            {
                return;
            }
            //Dinleyiciyi baþlat
            if (!baglantiDinleyici.Baslat())
            {
                return;
            }
            //Çalýþýyor bayraðýný iþaretle
            calisiyor = true;
        }
                
        /// Sunucunun çalýþmasýný durdurur        
        public void Durdur()
        {
            //Dinleyiciyi durdur
            baglantiDinleyici.Durdur();
            //Tüm clientleri durdur
            calisiyor = false;
            try
            {
                IList<Client> clientListesi = clients.Values;
                foreach (Client ist in clientListesi)
                {
                    ist.Durdur();
                }
            }
            catch (Exception)
            {
            }
            //Ýstemcileri temizle
            clients.Clear();
            //Çalýþýyor bayraðýndaki iþareti kaldýr
            calisiyor = false;
        }
        
        /// Bir clientye bir mesaj yollar        
        /// <param name="mesaj">Yollanacak mesaj</param>
        /// <returns>Ýþlemin baþarý durumu</returns>
        public bool MesajYolla(ClientRef client, string mesaj)
        {
            return client.MesajYolla(mesaj);
        }

        // PRIVATE FONKSYONLAR ////////////////////////////////////////////////
        
        /// Yeni bir client baðlantýðýnda buraya gönderilir.        
        /// <param name="clientSoketi">Yeni baðlanan client soketi</param>
        private void yeniClientSoketiBaglandi(Socket clientSoketi)
        {
            //Yeni baðlanan clientyi listeye ekle
            Client client = null;
            lock (objSenk)
            {
                client = new Client(this, clientSoketi, ++sonClientID);
                clients.Add(client.ClientID, client);
            }
            //Ýstemciyi çalýþmaya baþlat
            client.Baslat();
            //YeniClientBaglandi olayýný tetikle
            if (YeniClientBaglandi != null)
            {
                YeniClientBaglandi(new ClientBaglantiArgumanlari(client));
            }
        }
        
        /// Bir Client nesnesi bir mesaj aldýðýnda buraya iletir        
        /// <param name="client">Paketi alan Client nesnesi</param>
        /// <param name="mesaj">Mesaj nesnesi</param>
        private void yeniClientMesajiAlindi(Client client, string mesaj)
        {
            if (ClientdanYeniMesajAlindi != null)
            {
                ClientdanYeniMesajAlindi(new ClientdanMesajAlmaArgumanlari(client, mesaj));
            }
        }
        
        /// Bir Client nesnesiyle iliþkili baðlantý kapatýldýðýnda, burasý çaðýrýlýr        
        /// <param name="client">Kapatýlan client baðlantýsý</param>
        private void clientBaglantisiKapatildi(Client client)
        {
            //ClientBaglantisiKapatildi olayýný tetikle
            if (ClientBaglantisiKapatildi != null)
            {
                ClientBaglantisiKapatildi(new ClientBaglantiArgumanlari(client));
            }
            //Kapanan clientyi listeden çýkar
            if (calisiyor)
            {
                lock (objSenk)
                {
                    if (clients.ContainsKey(client.ClientID))
                    {
                        clients.Remove(client.ClientID);
                    }
                }
            }
        }

        // ALT SINIFLAR ///////////////////////////////////////////////////////        
        
        /// Ayrý bir thread olarak çalýþýp gelen soket baðlantýlarýný kabul ederek 
        /// SPIASunucusu modülüne ileten sýnýf.
        private class BaglantiDinleyici
        {
            /** Gelen baðlantýlarýn aktarýlacaðý modül */
            private SPIAServer sunucu;
            /** Gelen baðlantýlarý dinlemek için sunucu soketi */
            private TcpListener dinleyiciSoket;
            /** Dinlenen portun numarasý */
            private int port;
            /** thread'in çalýþmasýný kontrol eden bayrak */
            private volatile bool calisiyor = false;
            /** çalýþan thread'e referans */
            private volatile Thread thread;
            
            /// Kurucu fonksyon.            
            /// <param name="port">Dinlenecek port no</param>
            public BaglantiDinleyici(SPIAServer sunucu, int port)
            {
                this.sunucu = sunucu;
                this.port = port;
            }
            
            /// Dinlemeyi baþlatýr            
            /// <returns>iþlemin baþarý durumu</returns>
            public bool Baslat()
            {
                if (baglan())
                {
                    calisiyor = true;
                    thread = new Thread(new ThreadStart(tDinle));
                    thread.Start();
                    return true;
                }
                else
                {
                    return false;
                }
            }
                        
            /// dinlemeyi durdurur            
            /// <returns>iþlemin baþarý durumu</returns>
            public bool Durdur()
            {
                try
                {
                    calisiyor = false;
                    baglantiyiKes();
                    thread.Join();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            
            /// Port dinleme iþlemini baþlatýr ve baðlantýyý açýk hale getirir            
            /// <returns>Ýþlemin baþarý durumu</returns>
            private bool baglan()
            {
                try
                {
                    dinleyiciSoket = new TcpListener(System.Net.IPAddress.Any, port);
                    dinleyiciSoket.Start();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            
            /// Port dinleme iþlemini kapatýr            
            /// <returns>Ýþlemin baþarý durumu</returns>
            private bool baglantiyiKes()
            {
                try
                {
                    dinleyiciSoket.Stop();
                    dinleyiciSoket = null;
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            
            /// Ayrý bir thread olarak çalýþýp gelen soket baðlantýlarýný kabul ederek 
            /// SPIASunucusu modülüne ileten fonksyon.            
            public void tDinle()
            {
                Socket clientSoketi;
                while (calisiyor)
                {
                    try
                    {
                        clientSoketi = dinleyiciSoket.AcceptSocket();
                        if (clientSoketi.Connected)
                        {
                            try { sunucu.yeniClientSoketiBaglandi(clientSoketi); }
                            catch (Exception) { }
                        }
                    }
                    catch (Exception)
                    {
                        if (calisiyor)
                        {
                            //baðlantýyý sýfýrla
                            baglantiyiKes();
                            //1 saniye bekle
                            try { Thread.Sleep(1000); }
                            catch (Exception) { }
                            //yeniden baðlantý kur
                            baglan();
                        }
                    }
                }
            }
        }
        //---------------------------------------------------------------------
                
        /// Sunucuya baðlantý kuran bir clientyi temsil eder.        
        private class Client : ClientRef
        {
            // Sabitler -------------------------------------------------------
            private const byte BASLANGIC_BYTE = (byte)60;
            private const byte BITIS_BYTE = (byte)62;

            // Public Özellikler ----------------------------------------------
            
            /// Ýstemciyi temsil eden tekil ID deðeri            
            public long ClientID
            {
                get { return clientID; }
            }
            
            /// Ýstemci ile baðlantýnýn doðru þekilde kurulu olup olmadýðýný verir. True ise mesaj yollanýp alýnabilir.            
            public bool BaglantiVar
            {
                get { return calisiyor; }
            }

            // OLAYLAR --------------------------------------------------------
            
            /// Sunucu ile olan baðlantý kapandýðýnda tetiklenir            
            public event dgBaglantiKapatildi BaglantiKapatildi;
            
            /// Sunucudan yeni bir mesaj alýndýðýnda tetiklenir            
            public event dgYeniMesajAlindi YeniMesajAlindi;

            // Private Alanlar ------------------------------------------------
            
            /// Ýstemci ile iletiþimde kullanýlan soket baðlantýsý            
            private Socket soket;
            
            /// Sunucuya referans            
            private SPIAServer sunucu;
            
            /// Ýstemciyi temsil eden tekil ID deðeri            
            private long clientID;
            
            /// Veri transfer etmek için kullanýlan akýþ nesnesi            
            private NetworkStream agAkisi;
            
            /// Veri transfer etmek için kullanýlan akýþ nesnesi            
            private BinaryReader binaryOkuyucu;
            
            /// Veri transfer etmek için kullanýlan akýþ nesnesi            
            private BinaryWriter binaryYazici;
            
            /// Ýstemci ile iletiþim devam ediyorsa true, aksi halde false            
            private volatile bool calisiyor = false;
            
            /// Çalýþan thread'e referans            
            private Thread thread;

            // Public Fonksyonlar ---------------------------------------------
                        
            /// Bir Client nesnesi oluþturur            
            /// <param name="sunucu">Sunucuya referans</param>
            /// <param name="clientSoketi">Ýstemci ile iletiþimde kullanýlan soket baðlantýsý</param>
            /// <param name="clientID">Ýstemciyi temsil eden tekil ID deðeri</param>
            public Client(SPIAServer sunucu, Socket clientSoketi, long clientID)
            {
                this.sunucu = sunucu;
                this.soket = clientSoketi;
                this.clientID = clientID;
            }
                        
            /// Ýstemci ile mesaj alýþveriþini baþlatýr            
            public bool Baslat()
            {
                try
                {
                    agAkisi = new NetworkStream(soket);
                    binaryOkuyucu = new BinaryReader(agAkisi, Encoding.UTF8);
                    binaryYazici = new BinaryWriter(agAkisi, Encoding.UTF8);
                    thread = new Thread(new ThreadStart(tCalis));
                    calisiyor = true;
                    thread.Start();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            
            /// Ýstemci ile mesaj alýþveriþini durdurur            
            public void Durdur()
            {
                try
                {
                    calisiyor = false;
                    soket.Close();
                    thread.Abort();
                    thread.Join();
                }
                catch (Exception)
                {

                }
            }
            
            /// Ýstemci ile olan baðlantýyý keser            
            public void BaglantiyiKapat()
            {
                this.Durdur();
            }
                        
            /// Ýstemciye bir mesaj yollamak içindir            
            /// <param name="mesaj">Yollanacak mesaj</param>
            /// <returns>Ýþlemin baþarý durumu</returns>
            public bool MesajYolla(string mesaj)
            {
                try
                {
                    byte[] bMesaj = System.Text.Encoding.UTF8.GetBytes(mesaj);
                    byte[] b = new byte[bMesaj.Length + 2];
                    Array.Copy(bMesaj, 0, b, 1, bMesaj.Length);
                    b[0] = BASLANGIC_BYTE;
                    b[b.Length - 1] = BITIS_BYTE;
                    binaryYazici.Write(b);
                    agAkisi.Flush();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            // PRIVATE FONKSYONLAR ////////////////////////////////////////////
            
            /// Ýstemciden mesajlarý dinleyen fonksyon            
            private void tCalis()
            {
                //Her döngüde bir mesaj okunup sunucuya iletilir
                while (calisiyor)
                {
                    try
                    {
                        //Baþlangýç Byte'ýný oku
                        byte b = binaryOkuyucu.ReadByte();
                        if (b != BASLANGIC_BYTE)
                        {
                            //Baþlangýç byte'ý deðil, bu karakteri atla!
                            continue;
                        }
                        //Mesajý oku
                        List<byte> bList = new List<byte>();
                        while ((b = binaryOkuyucu.ReadByte()) != BITIS_BYTE)
                        {
                            bList.Add(b);
                        }
                        string mesaj = System.Text.Encoding.UTF8.GetString(bList.ToArray());
                        //Okunan paketi sunucuya ilet
                        sunucu.yeniClientMesajiAlindi(this, mesaj);
                        yeniMesajAlindiTetikle(mesaj);
                    }
                    catch (Exception)
                    {
                        //Hata oluþtu, baðlantýyý kes!
                        break;
                    }
                }
                //Döngüden çýkýldýðýna göre clientyle baðlantý kapatýlmýþ ya da bir hata oluþmuþ demektir
                calisiyor = false;
                try
                {
                    if (soket.Connected)
                    {
                        soket.Close();
                    }
                }
                catch (Exception)
                {

                }
                sunucu.clientBaglantisiKapatildi(this);
                baglantiKapatildiTetikle();
            }

            // Olaylarý tetikleyen iç fonksyonlar -----------------------------
            private void baglantiKapatildiTetikle()
            {
                if (BaglantiKapatildi != null)
                {
                    BaglantiKapatildi();
                }
            }

            private void yeniMesajAlindiTetikle(string mesaj)
            {
                if (YeniMesajAlindi != null)
                {
                    YeniMesajAlindi(new MesajAlmaArgumanlari(mesaj));
                }
            }
        }
    }
}
