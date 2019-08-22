using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientProgram
{
    class ClientClass
    {
        string targetIP;
        int targetPort;
        Socket ClientSocket;

        public ClientClass(string ip, int port)
        {
            this.targetIP = ip;
            this.targetPort = port;

        }

        public void ConnectServer()
        {
            ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.ClientSocket.Connect(IPAddress.Parse(this.targetIP),this.targetPort);
            AsyncObject obj = new AsyncObject(1024);
            obj.WorkingSocket = this.ClientSocket;

            ClientSocket.BeginReceive(obj.Buffer, 0, obj.BufferSize,SocketFlags.None, ReceivedFromServer, obj);
        

        }

        public void ReceivedFromServer(IAsyncResult ar)
        {

            try
            {
                AsyncObject obj = (AsyncObject)ar.AsyncState;

                int numReceived = obj.WorkingSocket.EndReceive(ar);

                if (numReceived <= 0)
                {
                    obj.WorkingSocket.Close();
                    return;
                }

                string txt = Encoding.UTF8.GetString(obj.Buffer).Trim('\0');

                System.Console.WriteLine("Messege From Server : " + txt);

                obj.ClearBuffer();

                obj.WorkingSocket.BeginReceive(obj.Buffer, 0, obj.BufferSize, SocketFlags.None, ReceivedFromServer, obj);

            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
            }


        }

        public void SendMessageToServer(string msg)
        {
            if (!this.ClientSocket.IsBound)
            {
                System.Console.WriteLine("Server is not available");
                return;
            }

            IPEndPoint ip = (IPEndPoint)ClientSocket.LocalEndPoint;

            byte[] buffer = Encoding.UTF8.GetBytes(msg);

            ClientSocket.Send(buffer);



        }

        public class AsyncObject
        {
            public byte[] Buffer;
            public Socket WorkingSocket;
            public readonly int BufferSize;

            public AsyncObject(int size)
            {
                this.BufferSize = size;
                Buffer = new byte[size];

            }


            public void ClearBuffer()
            {
                Array.Clear(this.Buffer, 0, this.BufferSize);
            }



        }

    }
}
