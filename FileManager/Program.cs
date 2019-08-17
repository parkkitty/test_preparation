using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class Program
    {
        static void Main(string[] args)
        {
            //서브 폴더 안에 폴더 이름 및 파일을 리스트로 보내여준다
            //Client에서 접속해서, 요청하는 파일을 Socket을 통해 전송한다

            FileList fl = new FileList();

            string currentFolder = Directory.GetCurrentDirectory();

            currentFolder = @"C:\Users\parkk\Documents\GitHub\test_folder_0";

            Dictionary<string, string> fileInfo = new Dictionary<string, string>();

            fl.GetFileList(currentFolder);

            fileInfo = fl.GetInfo();
            fl.PrintConsole();

            System.Console.WriteLine();

        }
    }
}
