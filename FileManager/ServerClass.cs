using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private string serverIP;
        private int serverPort;
        private Socket serverSocket;
        private List<Socket> clientSockets;
        //Socket client;

        public ServerClass(string ip, int port)
        {

            this.serverIP = ip;
            this.serverPort = port;
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSockets = new List<Socket>();
        }

        public void ServerStart()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(this.serverIP), this.serverPort);
            serverSocket.Bind(ep);
            serverSocket.Listen(10);
            System.Console.WriteLine("Server Start!!");
            
                serverSocket.BeginAccept(AcceptCallBack, null);

            
            //서버가 열려있고, Call Back 함수에서 계속 부를 예정   

        }

        public void SendMessage(Socket client)
        {

            //serverSocket.sen

        }

        public void ReceiveMessage()
        {

        }

        private void AcceptCallBack(IAsyncResult ar)
        {
            Socket client = serverSocket.EndAccept(ar);

            serverSocket.BeginAccept(AcceptCallBack, null); //서버는 다시 Accept 준비 

            AsyncObject obj = new AsyncObject(1024);
            obj.WorkingSocket = client;
            clientSockets.Add(client);
            obj.id = clientSockets.Count;
            Debug.WriteLine("{0}번째 Client 접속", clientSockets.Count);

            client.BeginReceive(obj.Buffer, 0, 1024, 0, DataReceive, obj);

        }

        private void DataReceive(IAsyncResult ar)
        {

            AsyncObject obj = (AsyncObject)ar.AsyncState;

            int received = obj.WorkingSocket.EndReceive(ar);

            string txt = Encoding.UTF8.GetString(obj.Buffer).Trim('\0');
            //txt = txt.Trim("\0");
            string msg = "From {"+ obj.id+"} Client:" + txt;
            System.Console.WriteLine(msg);
            byte[] sendMsg = Encoding.UTF8.GetBytes(obj.id + " ACK\n");
            obj.WorkingSocket.Send(sendMsg);
            obj.ClearBuffer();

            obj.WorkingSocket.BeginReceive(obj.Buffer, 0, 1024, 0, DataReceive, obj);
         
        }

        private class AsyncObject
        {
            public byte[] Buffer;
            public Socket WorkingSocket;
            public readonly int BufferSize;
            public int id;

            public AsyncObject(int size)
            {
                this.BufferSize = size;
                Buffer = new byte[this.BufferSize];

            }

            public void ClearBuffer()
            {
                Array.Clear(Buffer, 0, BufferSize);
            }
        }

    }
}
