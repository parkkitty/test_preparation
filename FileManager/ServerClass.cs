using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class ServerClass
    {
        // 클라이언트와 연결하여, 파일을 전송한다.

        string serverIP;
        int serverPort;
        Socket serverSocket;
        List<Socket> clientSockets;
        Socket client;


        public ServerClass(string ip, int port)
        {

            this.serverIP = ip;
            this.serverPort = port;
        serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        }

        public void ServerStart()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(this.serverIP), this.serverPort);
            serverSocket.Bind(ep);
            serverSocket.Listen(10);
            
            while (true)
            {
                this.clientSockets.Add(serverSocket.Accept());
            }


        }

        public void SendMessage(Socket client)
        {

        }

        public void ReceiveMessage()
        {

        }

    }
}
