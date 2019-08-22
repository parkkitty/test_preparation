using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientClass client = new ClientClass("127.0.0.1", 21);

            client.ConnectServer();
            string msg = "";

            while (!msg.Equals("EXIT"))
            {
                msg = System.Console.ReadLine();
                client.SendMessageToServer(msg);

            }


        }
    }
}
