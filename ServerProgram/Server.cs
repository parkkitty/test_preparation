using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;

namespace ServerProgram
{
    class Server
    {
        private string ServerIp;
        private int Port;
        private Socket ServerSocket;
        private List<Socket> ClientSockets;


        public Server(string ip, int port)
        {
            this.ServerIp = ip;
            this.Port = port;

        }

        public void ServerStart()
        {
            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse(this.ServerIp), this.Port);
            ClientSockets = new List<Socket>();
            ServerSocket.Bind(serverEP);
            ServerSocket.Listen(10);

            ServerSocket.BeginAccept(AcceptCallBack, null);
        }

        private void AcceptCallBack(IAsyncResult ar)
        {
            Socket client = ServerSocket.EndAccept(ar);
            ServerSocket.BeginAccept(AcceptCallBack, null);

            AsyincObject obj = new AsyincObject();
            obj.WorkingSocket = client;
            ClientSockets.Add(client);
            obj.ID = ClientSockets.Count;
            client.BeginReceive(obj.Buffer, 0, obj.BufferSize, SocketFlags.None, ReceivedDataFromClient, obj);

        }

        private void ReceivedDataFromClient(IAsyncResult ar)
        {
            AsyincObject obj = (AsyincObject)ar.AsyncState;



            int num = obj.WorkingSocket.EndReceive(ar);

            string msgFromClient = Encoding.UTF8.GetString(obj.Buffer).Trim('\0');

            string msg = "From {" + obj.ID + "}" + " :" + msgFromClient;

            Debug.Write(msg);


            //byte[] sendMsgFromServer = new byte[obj.BufferSize];
            msg = "{" + obj.ID + "} : ACK";
            obj.WorkingSocket.Send(Encoding.UTF8.GetBytes(msg));
            obj.ClearBuffer();

            try
            {
                obj.WorkingSocket.BeginReceive(obj.Buffer, 0, obj.BufferSize, SocketFlags.None, ReceivedDataFromClient, obj);
            }
            catch(Exception e)
            {
                int i = 0;

                foreach(Socket s in ClientSockets)
                {
                    if (!s.Connected)
                    {
                        ClientSockets.RemoveAt(i);
                        break;
                    }

                    i++;
                }

                Debug.WriteLine("Num of Client : "+ClientSockets.Count);
            }
               

            

        }


        private class AsyincObject
        {
            public int ID;
            public readonly int BufferSize = 1024;
            public byte[] Buffer;
            public Socket WorkingSocket;

            public AsyincObject()
            {
                Buffer = new byte[BufferSize];

            }

            public void ClearBuffer()
            {
                Array.Clear(Buffer, 0, BufferSize);
            }
            


        }


    }
}
