using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ROPv1
{
    //FILE TRANSFER USING C#.NET SOCKET - CLIENT
    class XMLAktarClient
    {
        Socket clientSock;
        IPEndPoint ipEnd;
        public XMLAktarClient()
        {

        }

        public bool ClientTarafi()
        {
            try
            {
                //IPAddress[] ipAddress = Dns.GetHostAddresses("localhost");
                //IPEndPoint ipEnd = new IPEndPoint(ipAddress[0], 13124);
                IPAddress[] ipAddress = Dns.GetHostAddresses("192.168.2.94");
                ipEnd = new IPEndPoint(ipAddress[0], 13124);
                clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                clientSock.Connect(ipEnd);


                byte[] clientData = new byte[1024 * 5000];
                string receivedPath = @"C:\Program Files\ROP\";

                int receivedBytesLen = clientSock.Receive(clientData);

                int fileNameLen = BitConverter.ToInt32(clientData, 0);
                string fileName = Encoding.ASCII.GetString(clientData, 4, fileNameLen);

                BinaryWriter bWrite = new BinaryWriter(File.Open(receivedPath + fileName, FileMode.Create));
                bWrite.Write(clientData, 4 + fileNameLen, receivedBytesLen - 4 - fileNameLen);

                bWrite.Close();
                clientSock.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}