using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirFileNum
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> input = new List<string>();
            input.Add("A/B/C/A.TXT");
            input.Add("A/CC/D/");
            input.Add("A/DD/E.TXT");
            input.Add("A/CC/README");
            input.Add("A/DD/LL/");
            input.Add("BB/HELP/DIC/T.PNG");
            input.Add("BB/CC/D/O.PNG");
            input.Add("BB/DD/BB/HELP");
            input.Add("BB/DD/BB/CC.EXE");
            input.Add("BB/BB/BB/");

            int fileNum = 0;
            List<string> directories = new List<string>();

            foreach (string s in input)
            {
                //1. 파일 개수 새기

               // bool file = false;
                
                char[] c = s.ToArray();
                string[] directory = s.Split('/');
                string temp = directory[0];
                
                if (c[c.Length - 1] != '/')
                {
                    fileNum++;
                    directory[directory.Length-1] = "";
                   
                }

                

                if (!directories.Contains(temp))
                {
                    directories.Add(temp);
                }

                for (int i=1; i< directory.Length- 1; i++)
                {
                   

                    temp = temp + "/" + directory[i];
                    if (!directories.Contains(temp))
                    {
                        directories.Add(temp);

                    }
                }
            }

            foreach(string s in input)
            {
                System.Console.WriteLine(s);
            }

            System.Console.WriteLine("File No: {0}", fileNum);

            foreach (string s in directories)
            {
                System.Console.WriteLine(s);
            }
            System.Console.WriteLine("Directory No: {0}", directories.Count);

            System.Console.ReadLine();
        }
    }
}
