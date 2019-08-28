using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach(string s in args)
            {
                
                byte[] bytes = Encoding.Unicode.GetBytes(s);
                string base64 = Convert.ToBase64String(bytes);

                System.Console.WriteLine(base64);

            }

           // System.Console.WriteLine("Hi");
        }
    }
}
