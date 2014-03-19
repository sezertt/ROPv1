using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ROPv1
{
    //FILE TRANSFER USING C#.NET SOCKET - SERVER
    class XMLAktarServer
    {
        Socket sock;
        public XMLAktarServer()
        {
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Any, 13124);
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            sock.Bind(ipEnd);
            sock.Listen(100);
            //clientSock is the socket object of client, so we can use it now to transfer data to client
        }

        public bool Server(string FileName)
        {
            try
            {
                Socket clientSock = sock.Accept();
                
                string fileName = FileName;// "test.txt";// "Your File Name";
                string filePath = @"C:\Program Files\ROP\";//Your File Path;
                byte[] fileNameByte = Encoding.ASCII.GetBytes(fileName);

                byte[] fileData = File.ReadAllBytes(filePath + fileName);
                byte[] clientData = new byte[4 + fileNameByte.Length + fileData.Length];
                byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);

                fileNameLen.CopyTo(clientData, 0);
                fileNameByte.CopyTo(clientData, 4);
                fileData.CopyTo(clientData, 4 + fileNameByte.Length);

                clientSock.Send(clientData);
                
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