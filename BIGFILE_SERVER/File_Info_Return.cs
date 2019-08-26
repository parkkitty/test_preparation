using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIGFILE_SERVER
{
    class File_Info_Return
    {
        string FileName;
        string RootPath;
        string FileFullPath = "";
        List<string> OriginalFile;
        List<string> LineZipFile;
        List<string> CharZipFile;
        List<string> encrypFile;
        string defaultTable = "VWXYZABCDEFGHIJKLMNOPQRSTU";

        public File_Info_Return(string fileName, string path)
        {
            this.FileName = fileName;
            this.RootPath = path+@"\";

        }

        public List<string> GetOriginalFile()
        {

            OriginalFile = new List<string>();
            //this.FileFullPath = path;

            if (!this.FileFullPath.Equals(""))
            {
                string[] tmp = File.ReadAllLines(this.FileFullPath);
                this.OriginalFile = new List<string>();

                foreach (string s in tmp)
                {
                    this.OriginalFile.Add(s);
                }

            }
            else
            {
                System.Console.WriteLine("Check FileFullPath");
                this.OriginalFile = null;

            }
            
            return this.OriginalFile;
        }

        public List<string> GetLineZipFile()
        {

            if (this.OriginalFile.Count != 0)
            {

                this.LineZipFile = new List<string>();
                //앞에 라인과 비교 후, Line 압축
                string[] temp = this.OriginalFile.ToArray();
                string before = temp[0];
                int cnt = 1;
                for(int i=1; i<temp.Length; i++)
                {         
                    if (before.Equals(temp[i]))
                    {
                        LineZipFile.Remove(cnt + "#" + before);
                        cnt++;
                        LineZipFile.Add(cnt + "#" + before);
                    }
                    else
                    {
                        cnt = 1;
                        LineZipFile.Add(cnt + "#" + temp[i]);
                        before = temp[i];                        
                    }
                }
            }
            else
            {
                System.Console.WriteLine("Check OriginalFile");
                this.LineZipFile = null;
            }


            return this.LineZipFile;
        }

        public List<string> GetCharZipFile()
        {
            if (this.LineZipFile.Count != 0)
            {
                CharZipFile = new List<string>();

                foreach(string aLine in this.LineZipFile)
                {
                    string[] splitLine = aLine.Split('#');
                    string data = splitLine[1];
                    if (!data.Equals(""))
                    {
                        char[] arrayChar = data.ToCharArray();
                        int cnt = 1;
                        string zipLine = "";
                        char before = arrayChar[0];

                        for (int i = 1; i < arrayChar.Length; i++)
                        {
                            if (before.Equals(arrayChar[i]))
                            {
                                cnt++;
                                if (i == (arrayChar.Length - 1))
                                {
                                    zipLine += cnt;
                                    zipLine += before;
                                }

                            }
                            else
                            {

                                if (cnt != 1)
                                {
                                    zipLine += cnt;
                                    zipLine += before;
                                }
                                else
                                {
                                    zipLine += before;
                                }
                                cnt = 1;
                                if (i != (arrayChar.Length - 1))
                                {
                                    before = arrayChar[i];
                                }
                                else
                                {
                                    zipLine += arrayChar[i];
                                }


                            }

                        }

                        string addLine = splitLine[0] + "#" + zipLine;

                        CharZipFile.Add(addLine);
                    }

                }



            }
            else
            {
                System.Console.WriteLine("Check CharZipFile");
                this.CharZipFile = null;
            }

            return this.CharZipFile;
        }


        public string FindFilePath()
        {
            DirectoryInfo di = new DirectoryInfo(this.RootPath);
            FileInfo[] fi = di.GetFiles("*", SearchOption.AllDirectories);

            System.Console.WriteLine();

            foreach(FileInfo f in fi)
            {
                if (f.Name.Equals(this.FileName))
                {
                    this.FileFullPath = f.FullName;
                    break;
                }
            }

            return this.FileFullPath;
        }

        public List<string> GetEncryptionFile(string key = "")
        {

            encrypFile = new List<string>();
            string table = "";

            if (key.Equals(""))
            {
                table = this.defaultTable;
            }
            else
            {
                List<char> temp = new List<char>();
                char[] keyArray = key.ToCharArray();
                string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                List<char> defaultArray = alpha.ToList();

                foreach (char k in keyArray)
                {
                    temp.Add(k);
                    defaultArray.Remove(k);
                }
                foreach(char d in defaultArray)
                {
                    temp.Add(d);
                }
                foreach(char t in temp)
                {
                    table += t;
                }
                    

            }
           

            foreach (string s in this.CharZipFile)
            {
                char[] aLine = s.ToCharArray();
                char[] answer = new char[aLine.Length];
                char[] tableArray = table.ToCharArray();



                for(int i=0; i<aLine.Length; i++)
                {
                    if (aLine[i] >= 65 && aLine[i] <= 90)
                    {
                        answer[i] = tableArray[aLine[i] - 65];
                        System.Console.WriteLine("");
                    }
                    else
                    {
                        answer[i] = aLine[i];
                    }
                    
  
                }
                string tmp = new string(answer);

                encrypFile.Add(tmp);


            }

            return this.encrypFile;
        }


    }
}
