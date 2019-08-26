using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileVersionManager
{
    class ServerClass
    {
        string ServerIP;
        int ServerPort;
        Socket ServerSocket;
        Socket ClientSocket;

        public ServerClass(string ip, int port)
        {
            this.ServerIP = ip;
            this.ServerPort = port;
        }

        public void ServerStart()
        {
            IPEndPoint ServerEP = new IPEndPoint(IPAddress.Parse(this.ServerIP), this.ServerPort);
            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ServerSocket.Bind(ServerEP);

            ServerSocket.Listen(10);

            while (true)
            {
                ClientSocket = ServerSocket.Accept();
                Thread receive = new Thread(ReceivedData);
                receive.Start(ClientSocket);

            }
        

        }

        private void ReceivedData(object obj)
        {

            Socket ReceivedSocket = obj as Socket;
            Byte[] buffer = new byte[1024];

            while(ReceivedSocket.Connected && ReceivedSocket != null)
            {
                int numByte = ReceivedSocket.Receive(buffer);

                if (numByte != 0)
                {
                    string data = Encoding.UTF8.GetString(buffer).TrimEnd('\0');
                    Debug.WriteLine("From Clietn {0}: {1}", ReceivedSocket.ToString(), data);
                    ReceivedSocket.Send(Encoding.UTF8.GetBytes("ACK\n"));
                    Array.Clear(buffer, 0, buffer.Length);

                    if(data.Equals("SEND\r\n"))
                    {
                        DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
                        string path = di.Parent.Parent.FullName+@"\MERGED";
                        DirectoryInfo d = new DirectoryInfo(path);
                        FileInfo[] fi = d.GetFiles("*");

                        foreach(FileInfo f in fi)
                        {
                            ReceivedSocket.SendFile(f.FullName);
                        }
                        
                    }
                }

            }

           



        }
    }
}
