using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BIGFILE_SERVER
{
    class ServerFunc
    {
        string serverIp;
        int serverPort;
        Socket serverSocket;
        Dictionary<int, Socket> clientSockets;
        Socket client;

        public ServerFunc(string ip, int port)
        {
            this.serverIp = ip;
            this.serverPort = port;

        }

        public void ServerStart()
        {
            IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse(this.serverIp), this.serverPort);
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            serverSocket.Bind(serverEP);
            serverSocket.Listen(10);

            client = serverSocket.Accept();

            byte[] data = new byte[1024];
            int received = client.Receive(data);

            if (received != 0)
            {
                string requestedFile = Encoding.UTF8.GetString(data).TrimEnd('\0');
                requestedFile = requestedFile.Substring(0, requestedFile.Length - 2);
                //requestedFile = requestedFile.
                DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
                string path = di.Parent.Parent.FullName;
                File_Info_Return file_Info_Return = new File_Info_Return(requestedFile, path);
                string filePath = file_Info_Return.FindFilePath();
                List<string> orginal = file_Info_Return.GetOriginalFile();
                List<string> zip = file_Info_Return.GetLineZipFile();
                List<string> charZip = file_Info_Return.GetCharZipFile();
                List<string> targetFile = file_Info_Return.GetEncryptionFile("LGCNS");
                string[] targetLines = targetFile.ToArray();
           
                
                int idx = 0;
                string response = "ACK";
                byte[] aLineByte = new byte[1024];

                while (true)
                {
                    if(response=="ACK")
                    {

                        if (idx >= targetLines.Length)
                        {
                            break;
                        }
                        string aLine = targetLines[idx];
                        
                        aLineByte = Encoding.UTF8.GetBytes(aLine);
                        client.Send(aLineByte);
                        idx++;
                    }
                    else if (response == "ERR")
                    {
                        string aLine = targetLines[idx-1];
                       
                        aLineByte = Encoding.UTF8.GetBytes(aLine);
                        client.Send(aLineByte);
                        
                    }
                    else if(int.TryParse(response,out int num)){

                        string aLine = targetLines[num];
                       
                        aLineByte = Encoding.UTF8.GetBytes(aLine);
                        client.Send(aLineByte);
                        idx = num + 1;

                    }

                    Array.Clear(data, 0, data.Length);
                    int n = client.Receive(data);

                    response = Encoding.UTF8.GetString(data).TrimEnd('\0');
                    response = response.Substring(0, response.Length - 2);

                }


            }

        }

    }
    
}
