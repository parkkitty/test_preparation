using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIGFILE_SERVER
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
            string path = di.Parent.Parent.FullName+@"\BIGFILE";


            /* //string input = System.Console.ReadLine();
             string input = "test.txt";

             File_Info_Return file_Info_Return = new File_Info_Return(input, path);

             string filePath =file_Info_Return.FindFilePath();
             List<string> orginal = file_Info_Return.GetOriginalFile();
             List<string> zip = file_Info_Return.GetLineZipFile();
             List<string> charZip = file_Info_Return.GetCharZipFile();
             List<string> encrypFile = file_Info_Return.GetEncryptionFile();

             File.AppendAllLines(filePath, encrypFile);

     */
            ServerFunc server = new ServerFunc("127.0.0.1", 21);
            server.ServerStart();

            System.Console.WriteLine("");

        }
    }
}
