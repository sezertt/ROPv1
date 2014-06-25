using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace SPIA.Client
{
    
    /// Asenkron Soketli Mesajla�ma �stemcisi
    
    public class SPIAClient
    {
        // SAB�TLER ///////////////////////////////////////////////////////////

        private const byte BASLANGIC_BYTE = (byte)60; // <
        private const byte BITIS_BYTE = (byte)62; // >

        // PUBLIC �ZELL�KLER //////////////////////////////////////////////////

        /// SPIA Sunucu IP Adresi
        public string ServerIpAdresi
        {
            get { return serverIpAdresi; }
        }
        private string serverIpAdresi;

        /// SPIA Sunucu Port Numaras�
        public int ServerPort
        {
            get { return serverPort; }
            set { serverPort = value; }
        }
        private int serverPort;

        // OLAYLAR ////////////////////////////////////////////////////////////

        /// Sunucu ile olan ba�lant� kapand���nda tetiklenir
        public event dgBaglantiKapatildi BaglantiKapatildi;

        /// Sunucudan yeni bir mesaj al�nd���nda tetiklenir
        public event dgYeniMesajAlindi YeniMesajAlindi;

        // PRIVATE ALANLAR ////////////////////////////////////////////////////

        /// Sunucuya ba�lant�y� sa�layan soket nesnesi
        private Socket clientBaglantisi;

        /// Sunucuyla ileti�imi sa�layan temel ak�� nesnesi        
        private NetworkStream agAkisi;
        
        /// Veri transfer etmek i�in kullan�lan ak�� nesnesi        
        private BinaryWriter binaryYazici;
        
        /// Veri transfer etmek i�in kullan�lan ak�� nesnesi        
        private BinaryReader binaryOkuyucu;
        
        /// �al��an thread'e referans        
        private Thread thread;
        
        /// �stemci ile ileti�im devam ediyorsa true, aksi halde false        
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
        
        /// Bir SPIA �stemcisi olu�turur.        
        /// <param name="serverIpAdresi">SPIA Sunucusunun IP adresi</param>
        /// <param name="serverPort">SPIA sunucusunun port numaras�</param>
        public SPIAClient(string serverIpAdresi, int serverPort)
        {
            this.serverIpAdresi = serverIpAdresi;
            this.serverPort = serverPort;
        }

        
        // Sunucuya ba�lant�y� kurar.        
        // <returns>ba�lant� sa�land�ysa true, aksi halde false</returns>
        public bool Baglan()
        {
            try
            {
                //sunucuya ba�lan
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

        
        /// Sunucuya olan ba�lant�y� kapat�r.        
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

        /// Sunucuya olan ba�lant�y� kapat�r.        
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
        
        /// Sunucuya bir mesaj yollamak i�indir        
        /// <param name="mesaj">Yollanacak mesaj</param>
        /// <returns>��lemin ba�ar� durumu</returns>
        public bool MesajYolla(string mesaj)
        {
            try
            {
                //Mesaj� byte dizisine �evirelim
                byte[] bMesaj = System.Text.Encoding.UTF8.GetBytes(mesaj);
                //Kar�� tarafa g�nderilecek byte dizisini olu�tural�m
                byte[] b = new byte[bMesaj.Length + 2];
                Array.Copy(bMesaj, 0, b, 1, bMesaj.Length);
                b[0] = BASLANGIC_BYTE;
                b[b.Length - 1] = BITIS_BYTE;
                //Mesaj� sokete yazal�m
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
        
        /// Sunucudan mesajlar� dinleyen fonksiyon        
        private void tCalis()
        {
            //Her d�ng�de bir mesaj okunur
            while (calisiyor)
            {
                try
                {
                    //Ba�lang�� Byte'�n� oku
                    byte b = binaryOkuyucu.ReadByte();
                    if (b != BASLANGIC_BYTE)
                    {
                        //Hatal� paket, ba�lant�y� kes!
                        break;
                    }
                    //Mesaj� oku
                    List<byte> bList = new List<byte>();
                    while ((b = binaryOkuyucu.ReadByte()) != BITIS_BYTE)
                    {
                        bList.Add(b);
                    }                    
                    string mesaj = System.Text.Encoding.UTF8.GetString(bList.ToArray());
                    //Yeni mesaj ba�ar�yla al�nd�
                    yeniMesajAlindiTetikle(mesaj);
                }
                catch (Exception)
                {
                    //Hata olu�tu, ba�lant�y� kes!
                    break;
                }
            }
            //D�ng�den ��k�ld���na g�re ba�lant� kapat�lm�� demektir
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