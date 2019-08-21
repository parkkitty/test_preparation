using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server("127.0.0.1", 21);
            server.ServerStart();

            System.Console.ReadLine();

        }
    }
}
