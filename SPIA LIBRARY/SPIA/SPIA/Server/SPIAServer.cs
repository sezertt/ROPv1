using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.IO;

namespace SPIA.Server
{
    
    /// Asenkron Soketli Mesajla�ma Sunucusu
    
    public class SPIAServer
    {
        // PUBLIC �ZELL�KLER //////////////////////////////////////////////////
        
        /// Dinlenen Port        
        public int Port
        {
            get { return port; }
        }
        private int port;

        // OLAYLAR ////////////////////////////////////////////////////////////
        
        /// Sunucuya yeni bir client ba�lant���nda tetiklenir.        
        public event dgYeniClientBaglandi YeniClientBaglandi;
        
        /// Sunucuya ba�l� bir client ba�lant�s� kapat�ld���nda tetiklenir.        
        public event dgClientBaglantisiKapatildi ClientBaglantisiKapatildi;
        
        /// Bir clientden yeni bir mesaj al�nd���nda tetiklenir.        
        public event dgClientdanYeniMesajAlindi ClientdanYeniMesajAlindi;

        // PRIVATE ALANLAR ////////////////////////////////////////////////////
        
        /// En son ba�lant� sa�lanan client ID'si        
        private long sonClientID = 0;
        
        /// O anda ba�lant� kurulmu� olan clientlerin listesi        
        private SortedList<long, Client> clients;
        
        /// Sunucunun �al��ma durumu        
        private volatile bool calisiyor;
        
        /// Senkronizasyonda kullan�lacak nesne        
        private object objSenk = new object();
        
        /// Ba�lant� dinleyen nesne        
        private BaglantiDinleyici baglantiDinleyici;

        // PUBLIC FONKSYONLAR /////////////////////////////////////////////////        
        
        /// Bir SPIA sunucusu olu�turur        
        /// <param name="port">Dinlenecek port</param>
        public SPIAServer(int port)
        {
            this.port = port;
            this.clients = new SortedList<long, Client>();
            this.baglantiDinleyici = new BaglantiDinleyici(this, port);
        }
        
        /// Sunucunun �al��mas�n� ba�lat�r        
        public void Baslat()
        {
            //E�er zaten �al���yorsa i�lem yapmadan ��k
            if (calisiyor)
            {
                return;
            }
            //Dinleyiciyi ba�lat
            if (!baglantiDinleyici.Baslat())
            {
                return;
            }
            //�al���yor bayra��n� i�aretle
            calisiyor = true;
        }
                
        /// Sunucunun �al��mas�n� durdurur        
        public void Durdur()
        {
            //Dinleyiciyi durdur
            baglantiDinleyici.Durdur();
            //T�m clientleri durdur
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
            //�stemcileri temizle
            clients.Clear();
            //�al���yor bayra��ndaki i�areti kald�r
            calisiyor = false;
        }
        
        /// Bir clientye bir mesaj yollar        
        /// <param name="mesaj">Yollanacak mesaj</param>
        /// <returns>��lemin ba�ar� durumu</returns>
        public bool MesajYolla(ClientRef client, string mesaj)
        {
            return client.MesajYolla(mesaj);
        }

        // PRIVATE FONKSYONLAR ////////////////////////////////////////////////
        
        /// Yeni bir client ba�lant���nda buraya g�nderilir.        
        /// <param name="clientSoketi">Yeni ba�lanan client soketi</param>
        private void yeniClientSoketiBaglandi(Socket clientSoketi)
        {
            //Yeni ba�lanan clientyi listeye ekle
            Client client = null;
            lock (objSenk)
            {
                client = new Client(this, clientSoketi, ++sonClientID);
                clients.Add(client.ClientID, client);
            }
            //�stemciyi �al��maya ba�lat
            client.Baslat();
            //YeniClientBaglandi olay�n� tetikle
            if (YeniClientBaglandi != null)
            {
                YeniClientBaglandi(new ClientBaglantiArgumanlari(client));
            }
        }
        
        /// Bir Client nesnesi bir mesaj ald���nda buraya iletir        
        /// <param name="client">Paketi alan Client nesnesi</param>
        /// <param name="mesaj">Mesaj nesnesi</param>
        private void yeniClientMesajiAlindi(Client client, string mesaj)
        {
            if (ClientdanYeniMesajAlindi != null)
            {
                ClientdanYeniMesajAlindi(new ClientdanMesajAlmaArgumanlari(client, mesaj));
            }
        }
        
        /// Bir Client nesnesiyle ili�kili ba�lant� kapat�ld���nda, buras� �a��r�l�r        
        /// <param name="client">Kapat�lan client ba�lant�s�</param>
        private void clientBaglantisiKapatildi(Client client)
        {
            //ClientBaglantisiKapatildi olay�n� tetikle
            if (ClientBaglantisiKapatildi != null)
            {
                ClientBaglantisiKapatildi(new ClientBaglantiArgumanlari(client));
            }
            //Kapanan clientyi listeden ��kar
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
        
        /// Ayr� bir thread olarak �al���p gelen soket ba�lant�lar�n� kabul ederek 
        /// SPIASunucusu mod�l�ne ileten s�n�f.
        private class BaglantiDinleyici
        {
            /** Gelen ba�lant�lar�n aktar�laca�� mod�l */
            private SPIAServer sunucu;
            /** Gelen ba�lant�lar� dinlemek i�in sunucu soketi */
            private TcpListener dinleyiciSoket;
            /** Dinlenen portun numaras� */
            private int port;
            /** thread'in �al��mas�n� kontrol eden bayrak */
            private volatile bool calisiyor = false;
            /** �al��an thread'e referans */
            private volatile Thread thread;
            
            /// Kurucu fonksyon.            
            /// <param name="port">Dinlenecek port no</param>
            public BaglantiDinleyici(SPIAServer sunucu, int port)
            {
                this.sunucu = sunucu;
                this.port = port;
            }
            
            /// Dinlemeyi ba�lat�r            
            /// <returns>i�lemin ba�ar� durumu</returns>
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
            /// <returns>i�lemin ba�ar� durumu</returns>
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
            
            /// Port dinleme i�lemini ba�lat�r ve ba�lant�y� a��k hale getirir            
            /// <returns>��lemin ba�ar� durumu</returns>
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
            
            /// Port dinleme i�lemini kapat�r            
            /// <returns>��lemin ba�ar� durumu</returns>
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
            
            /// Ayr� bir thread olarak �al���p gelen soket ba�lant�lar�n� kabul ederek 
            /// SPIASunucusu mod�l�ne ileten fonksyon.            
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
                            //ba�lant�y� s�f�rla
                            baglantiyiKes();
                            //1 saniye bekle
                            try { Thread.Sleep(1000); }
                            catch (Exception) { }
                            //yeniden ba�lant� kur
                            baglan();
                        }
                    }
                }
            }
        }
        //---------------------------------------------------------------------
                
        /// Sunucuya ba�lant� kuran bir clientyi temsil eder.        
        private class Client : ClientRef
        {
            // Sabitler -------------------------------------------------------
            private const byte BASLANGIC_BYTE = (byte)60;
            private const byte BITIS_BYTE = (byte)62;

            // Public �zellikler ----------------------------------------------
            
            /// �stemciyi temsil eden tekil ID de�eri            
            public long ClientID
            {
                get { return clientID; }
            }
            
            /// �stemci ile ba�lant�n�n do�ru �ekilde kurulu olup olmad���n� verir. True ise mesaj yollan�p al�nabilir.            
            public bool BaglantiVar
            {
                get { return calisiyor; }
            }

            // OLAYLAR --------------------------------------------------------
            
            /// Sunucu ile olan ba�lant� kapand���nda tetiklenir            
            public event dgBaglantiKapatildi BaglantiKapatildi;
            
            /// Sunucudan yeni bir mesaj al�nd���nda tetiklenir            
            public event dgYeniMesajAlindi YeniMesajAlindi;

            // Private Alanlar ------------------------------------------------
            
            /// �stemci ile ileti�imde kullan�lan soket ba�lant�s�            
            private Socket soket;
            
            /// Sunucuya referans            
            private SPIAServer sunucu;
            
            /// �stemciyi temsil eden tekil ID de�eri            
            private long clientID;
            
            /// Veri transfer etmek i�in kullan�lan ak�� nesnesi            
            private NetworkStream agAkisi;
            
            /// Veri transfer etmek i�in kullan�lan ak�� nesnesi            
            private BinaryReader binaryOkuyucu;
            
            /// Veri transfer etmek i�in kullan�lan ak�� nesnesi            
            private BinaryWriter binaryYazici;
            
            /// �stemci ile ileti�im devam ediyorsa true, aksi halde false            
            private volatile bool calisiyor = false;
            
            /// �al��an thread'e referans            
            private Thread thread;

            // Public Fonksyonlar ---------------------------------------------
                        
            /// Bir Client nesnesi olu�turur            
            /// <param name="sunucu">Sunucuya referans</param>
            /// <param name="clientSoketi">�stemci ile ileti�imde kullan�lan soket ba�lant�s�</param>
            /// <param name="clientID">�stemciyi temsil eden tekil ID de�eri</param>
            public Client(SPIAServer sunucu, Socket clientSoketi, long clientID)
            {
                this.sunucu = sunucu;
                this.soket = clientSoketi;
                this.clientID = clientID;
            }
                        
            /// �stemci ile mesaj al��veri�ini ba�lat�r            
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
            
            /// �stemci ile mesaj al��veri�ini durdurur            
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
            
            /// �stemci ile olan ba�lant�y� keser            
            public void BaglantiyiKapat()
            {
                this.Durdur();
            }
                        
            /// �stemciye bir mesaj yollamak i�indir            
            /// <param name="mesaj">Yollanacak mesaj</param>
            /// <returns>��lemin ba�ar� durumu</returns>
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
            
            /// �stemciden mesajlar� dinleyen fonksyon            
            private void tCalis()
            {
                //Her d�ng�de bir mesaj okunup sunucuya iletilir
                while (calisiyor)
                {
                    try
                    {
                        //Ba�lang�� Byte'�n� oku
                        byte b = binaryOkuyucu.ReadByte();
                        if (b != BASLANGIC_BYTE)
                        {
                            //Ba�lang�� byte'� de�il, bu karakteri atla!
                            continue;
                        }
                        //Mesaj� oku
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
                        //Hata olu�tu, ba�lant�y� kes!
                        break;
                    }
                }
                //D�ng�den ��k�ld���na g�re clientyle ba�lant� kapat�lm�� ya da bir hata olu�mu� demektir
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

            // Olaylar� tetikleyen i� fonksyonlar -----------------------------
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
