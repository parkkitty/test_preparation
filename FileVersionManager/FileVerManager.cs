using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace FileVersionManager
{
    class FileVerManager
    {
        string FolderPath;
        List<string> FileList;
        Dictionary<string, string> FileNameMD5;

        public FileVerManager(string path)
        {

            this.FolderPath = path;
            FileList = new List<string>();
            FileNameMD5 = new Dictionary<string, string>();
        }
        public void UpdateFileList()
        {
            //오름차순 정렬
            DirectoryInfo di = new DirectoryInfo(this.FolderPath);
            FileInfo[] fi = di.GetFiles("*");
            List<string> temp = new List<string>();
            FileList.Clear();

            foreach(FileInfo f in fi)
            {
                temp.Add(f.Name);
            }

            var sortedFiles = from s in temp orderby s ascending select s;

            foreach(string s in sortedFiles)
            {
                FileList.Add(s);
            }
     

        } 


        public void UpdateFileMD5List()
        {
            DirectoryInfo di = new DirectoryInfo(this.FolderPath);
            FileInfo[] fi = di.GetFiles("*");
            List<string> temp = new List<string>();
            FileNameMD5.Clear();


            foreach (FileInfo f in fi)
            {
                temp.Add(f.Name);
            }

            var sortedFiles = from s in temp orderby s ascending select s;

            foreach (string s in sortedFiles)
            {
               
                FileNameMD5.Add(s, MakeMD5String(s));

            }
        }
        public void PrintFileList(bool MD5 = false)
        {
            if (!MD5)
            {
                foreach (string s in this.FileList)
                {
                    System.Console.WriteLine(s);
                }

            }
            else
            {
                foreach (KeyValuePair<string, string> keyValuePair in FileNameMD5)
                {
                    System.Console.WriteLine(keyValuePair.Key + "##" + keyValuePair.Value);
                }


            }
        }

        private string MakeMD5String(string fileName)
        {
            string MD5String = "";
            string[] fileLines = File.ReadAllLines(this.FolderPath + @"\" + fileName);
            string input = "";
            foreach(string s in fileLines)
            {
                input += s;
            }

            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder stringBuilder = new StringBuilder();

            foreach(byte b in data)
            {
                stringBuilder.Append(b.ToString("x2"));
            }

            MD5String = stringBuilder.ToString();

            return MD5String;
        }


        public void UpdateVersion()
        {
            //MD5가 다르면 M, 삭제되었으면 D , 추가 A, 변경내용 없음 - ,바뀔때마다 version + 1
            // sample.txt:2:M:MD5 hash

            List<string> beforeVerFile = File.ReadAllLines(this.FolderPath + @"\version.txt").ToList();
            List<string> currentFileInfo = this.UpdatedFileInfo();

            List<string> newFileString = new List<string>();
            List<string> currentFileNames = new List<string>(); 

            foreach(string s in beforeVerFile)
            {
                newFileString.Add(s.Split(':')[0]);
            }

            foreach(string s in currentFileInfo)
            {
                currentFileNames.Add(s.Split(':')[0]);
            }

            //1.추가
           
            foreach(string current in currentFileNames)
            {
                if (!newFileString.Contains(current))
                {
                    newFileString.Add(current + ":0:A:" +this.MakeMD5String(current));
                }
            }

            //2. 삭제
            int cnt = 0;

            List<string> tempDel = new List<string>();

            foreach(string before in newFileString)
            {
                if (!currentFileNames.Contains(before))
                {
                    string[] tmp = beforeVerFile[cnt].Split(':');
                    if (tmp.Length==4)
                    {
                        tempDel.Add(tmp[0] + ":" + tmp[1] + ":D:" + tmp[3]);
                    }
                    
                }
                else
                {
                    string fileName = beforeVerFile[cnt].Split(':')[0];
                    string md5 = beforeVerFile[cnt].Split(':')[3];

                    if (md5.Equals(this.MakeMD5String(fileName)))
                    {
                        string[] tmp = beforeVerFile[cnt].Split(':');
                        tempDel.Add(tmp[0] + ":" + tmp[1] + ":-:" + tmp[3]);
                    }
                    else
                    {
                        string[] tmp = beforeVerFile[cnt].Split(':');
                        tempDel.Add(tmp[0] + ":" + (int.Parse(tmp[1]) + 1).ToString() + ":M:" + this.MakeMD5String(fileName));

                    }

                }

                cnt++;

                }

            File.Delete(this.FolderPath + @"\version.txt");
            File.AppendAllLines(this.FolderPath + @"\version.txt", tempDel);
            

            }
  
        public void InitVersionFile()
        {
            List<string> aLines = new List<string>();
            
            foreach(KeyValuePair<string,string> pair in this.FileNameMD5)
            {
                string aLine = pair.Key + ":0:-:" + pair.Value;
                aLines.Add(aLine);
            }

            File.AppendAllLines(this.FolderPath + @"\version.txt", aLines);

        }

        private List<string> UpdatedFileInfo()
        {
            List<string> updatedFileList = new List<string>();

            this.UpdateFileList();
            this.UpdateFileMD5List();

            foreach (KeyValuePair<string, string> pair in this.FileNameMD5)
            {
                string aLine = pair.Key + ":0:-:" + pair.Value;
                updatedFileList.Add(aLine);
            }

            return updatedFileList;
        }

        public List<string> GetVersionFile()
        {
            List<string> version = new List<string>();

            version = File.ReadAllLines(this.FolderPath+@"\version.txt").ToList();

            int cnt = 0;

            foreach(string s in version)
            {
                if (s.Split(':')[0].Equals("version.txt"))
                {
                    break;
                }

                cnt++;
            }

            version.RemoveAt(cnt);

            return version;
        }
    }
}
