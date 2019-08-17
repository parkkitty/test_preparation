using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace FileManager
{
    class FileList
    {
        // 입력으로 받은 위치를 기준으로, 하위 디렉토리 및 파일의 리스트를 Summary하여 파일로 만들어준다.
        string startDirectory;
        Dictionary<string, string> directoryInfo = new Dictionary<string, string>();

        public void GetFileList(string folder)
        {
            this.startDirectory = folder;
            DirectoryInfo di = new DirectoryInfo(folder);
            FileInfo[] temp = di.GetFiles();
            

            foreach (FileInfo fi in temp)
            {

                if (directoryInfo.ContainsKey(folder))
                {
                    string tmp = directoryInfo[folder] + "#" + fi.Name;
                    directoryInfo.Remove(folder);
                    directoryInfo.Add(folder, tmp);
                }
                else
                {
                    directoryInfo.Add(folder, fi.Name);
                }

             
            }


            if (di.GetDirectories().Length != 0)
            {

               foreach(DirectoryInfo d in di.GetDirectories())
                {
                    GetFileList(d.FullName);

                }

            }

            
        }

        public Dictionary<string,string> GetInfo()
        {
            return this.directoryInfo;
        }

        public void PrintConsole()
        {

            string parent = "";

            foreach (KeyValuePair<string,string> item in this.directoryInfo)
            {

                parent = item.Key;
                List<string> values = item.Value.Split('#').ToList();

                int cnt = 0;

                foreach(string s in values)
                {
                    if (cnt == 0)
                    {
                        System.Console.WriteLine("============================");
                        System.Console.WriteLine(parent);
                        System.Console.WriteLine("============================");
                        System.Console.WriteLine(s);
                    }

                    else 
                    {
                        System.Console.WriteLine(s);
                    }

                    cnt++;
                }


            }

        }

    }
}
