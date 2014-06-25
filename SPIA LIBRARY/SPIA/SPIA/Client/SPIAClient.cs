using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace SPIA.Client
{
    
    /// Asenkron Soketli Mesajlaþma Ýstemcisi
    
    public class SPIAClient
    {
        // SABÝTLER ///////////////////////////////////////////////////////////

        private const byte BASLANGIC_BYTE = (byte)60; // <
        private const byte BITIS_BYTE = (byte)62; // >

        // PUBLIC ÖZELLÝKLER //////////////////////////////////////////////////

        /// SPIA Sunucu IP Adresi
        public string ServerIpAdresi
        {
            get { return serverIpAdresi; }
        }
        private string serverIpAdresi;

        /// SPIA Sunucu Port Numarasý
        public int ServerPort
        {
            get { return serverPort; }
            set { serverPort = value; }
        }
        private int serverPort;

        // OLAYLAR ////////////////////////////////////////////////////////////

        /// Sunucu ile olan baðlantý kapandýðýnda tetiklenir
        public event dgBaglantiKapatildi BaglantiKapatildi;

        /// Sunucudan yeni bir mesaj alýndýðýnda tetiklenir
        public event dgYeniMesajAlindi YeniMesajAlindi;

        // PRIVATE ALANLAR ////////////////////////////////////////////////////

        /// Sunucuya baðlantýyý saðlayan soket nesnesi
        private Socket clientBaglantisi;

        /// Sunucuyla iletiþimi saðlayan temel akýþ nesnesi        
        private NetworkStream agAkisi;
        
        /// Veri transfer etmek için kullanýlan akýþ nesnesi        
        private BinaryWriter binaryYazici;
        
        /// Veri transfer etmek için kullanýlan akýþ nesnesi        
        private BinaryReader binaryOkuyucu;
        
        /// Çalýþan thread'e referans        
        private Thread thread;
        
        /// Ýstemci ile iletiþim devam ediyorsa true, aksi halde false        
        private volatile bool calisiyor = false;

        // PUBLIC FONKSYONLAR /////////////////////////////////////////////////

        public bool dosyaAl()
        {
            try
            {
                byte[] clientData = new byte[1024 * 5000];
                string receivedPath = @"C:\ROP\";

                int receivedBytesLen = clientBaglantisi.Receive(clientData);

                int fileNameLen = BitConverter.ToInt32(clientData, 0);
                string fileName = Encoding.ASCII.GetString(clientData, 4, fileNameLen);

                BinaryWriter bWrite = new BinaryWriter(File.Open(receivedPath + fileName, FileMode.Create));
                bWrite.Write(clientData, 4 + fileNameLen, receivedBytesLen - 4 - fileNameLen);

                bWrite.Close();
                return true;
            }
            catch(Exception ex)
            {
                string xczx = ex.ToString();
                return false;
            }
        }
        
        /// Bir SPIA Ýstemcisi oluþturur.        
        /// <param name="serverIpAdresi">SPIA Sunucusunun IP adresi</param>
        /// <param name="serverPort">SPIA sunucusunun port numarasý</param>
        public SPIAClient(string serverIpAdresi, int serverPort)
        {
            this.serverIpAdresi = serverIpAdresi;
            this.serverPort = serverPort;
        }

        
        // Sunucuya baðlantýyý kurar.        
        // <returns>baðlantý saðlandýysa true, aksi halde false</returns>
        public bool Baglan()
        {
            try
            {
                //sunucuya baðlan
                clientBaglantisi = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(serverIpAdresi), serverPort);
                clientBaglantisi.Connect(ipep);
                agAkisi = new NetworkStream(clientBaglantisi);
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

        
        /// Sunucuya olan baðlantýyý kapatýr.        
        public void BaglantiyiKes()
        {
            try
            {
                calisiyor = false;
                clientBaglantisi.Close();
                thread.Join();
            }
            catch (Exception)
            {}
        }

        /// Sunucuya olan baðlantýyý kapatýr.        
        public void BaglantiyiKes2()
        {
            try
            {
                calisiyor = false;
                clientBaglantisi.Close();
            }
            catch (Exception)
            { }
        }
        
        /// Sunucuya bir mesaj yollamak içindir        
        /// <param name="mesaj">Yollanacak mesaj</param>
        /// <returns>Ýþlemin baþarý durumu</returns>
        public bool MesajYolla(string mesaj)
        {
            try
            {
                //Mesajý byte dizisine çevirelim
                byte[] bMesaj = System.Text.Encoding.UTF8.GetBytes(mesaj);
                //Karþý tarafa gönderilecek byte dizisini oluþturalým
                byte[] b = new byte[bMesaj.Length + 2];
                Array.Copy(bMesaj, 0, b, 1, bMesaj.Length);
                b[0] = BASLANGIC_BYTE;
                b[b.Length - 1] = BITIS_BYTE;
                //Mesajý sokete yazalým
                binaryYazici.Write(b);
                agAkisi.Flush();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // PRIVATE FONKSYONLAR ////////////////////////////////////////////////
        
        /// Sunucudan mesajlarý dinleyen fonksiyon        
        private void tCalis()
        {
            //Her döngüde bir mesaj okunur
            while (calisiyor)
            {
                try
                {
                    //Baþlangýç Byte'ýný oku
                    byte b = binaryOkuyucu.ReadByte();
                    if (b != BASLANGIC_BYTE)
                    {
                        //Hatalý paket, baðlantýyý kes!
                        break;
                    }
                    //Mesajý oku
                    List<byte> bList = new List<byte>();
                    while ((b = binaryOkuyucu.ReadByte()) != BITIS_BYTE)
                    {
                        bList.Add(b);
                    }                    
                    string mesaj = System.Text.Encoding.UTF8.GetString(bList.ToArray());
                    //Yeni mesaj baþarýyla alýndý
                    yeniMesajAlindiTetikle(mesaj);
                }
                catch (Exception)
                {
                    //Hata oluþtu, baðlantýyý kes!
                    break;
                }
            }
            //Döngüden çýkýldýðýna göre baðlantý kapatýlmýþ demektir
            calisiyor = false;
            baglantiKapatildiTetikle();
        }

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