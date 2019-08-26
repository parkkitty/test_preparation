using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileVersionManager
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentDir = Directory.GetCurrentDirectory();
            DirectoryInfo di = new DirectoryInfo(currentDir);
            string folderPathA = di.Parent.Parent.FullName + @"\INPUT1";
            string folderPathB = di.Parent.Parent.FullName + @"\INPUT2";

            //MergedFolder mergedFolder = new MergedFolder(folderPathA, folderPathB);
            //mergedFolder.StartMerged(di.Parent.Parent.FullName + @"\MERGED");

            //FileVerManager fileVerManager = new FileVerManager(folderPath);
            //fileVerManager.UpdateFileList();
            //fileVerManager.UpdateFileMD5List();
            //fileVerManager.PrintFileList(true);
            //fileVerManager.InitVersionFile();
            ServerClass Server = new ServerClass("127.0.0.1", 21);
            Server.ServerStart();
            //Task ServerTask = new Task(new Action(Server.ServerStart));
            //ServerTask.Start();

            //fileVerManager.UpdateVersion();

            System.Console.WriteLine("");


        }
    }
}
