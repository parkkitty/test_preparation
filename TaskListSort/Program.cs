using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskListSort
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. 외부 프로그램 실행하기
            string programName = "tasklist";
            ProcessStartInfo psi = new ProcessStartInfo(); // 프로그램 정보
            Process ps = new Process(); // 실행시킬 프로세스

            psi.FileName = programName;
            psi.RedirectStandardOutput = true;
            psi.UseShellExecute = false;
            
            ps.StartInfo = psi;
            ps.Start();

            StreamReader sr = ps.StandardOutput; //외부프로그램으로부터 받을 Stream Reader
            string s;
            int cnt = 0;

            int lengthProcessName = 0;
            int lengthMemoryLength = 0;

            Dictionary<string,int> processList = new Dictionary<string, int>();
            HashSet<string> processName = new HashSet<string>();


            while (( s =sr.ReadLine())!=null)
            {
        
                cnt++;

                if (cnt == 3)
                {
                    string[] temp = s.Split(' ');
                    lengthProcessName = temp[0].Length;
                    lengthMemoryLength = temp[4].Length;
                }

                if (cnt > 3)
                {
                    string name = s.Substring(0, lengthProcessName);
                    string memory = s.Substring(s.Length - lengthMemoryLength);
                    name = name.Trim();
                    memory = memory.Trim();
                    memory = memory.Replace("K", "");
                    memory = memory.Replace(",", "");
                    // 중복여부 확인
                    if (processList.ContainsKey(name))
                    {
                        int tmp =  processList[name];
                        tmp += int.Parse(memory);
                        processList.Remove(name);
                        processList.Add(name, tmp);
                        
                    }
                    else
                    {
                        processList.Add(name, int.Parse(memory));
                    }
                    


                }

            }

            

            //Dictionary 내림차순 정렬

            var items = from pair in processList
                        orderby pair.Value descending
                        select pair;

            int num = 3;

            // 정렬된 아이템 뽑아내기

            
            List<string> list = new List<string>();
            foreach(KeyValuePair<string,int> pair in items)
            {

                //string value = ;
               

                string text = "Key : " + pair.Key + " Value : " + string.Format("{0:#,###}", pair.Value);
                list.Add(text);

                System.Console.WriteLine("Key : {0}, Value : {1:#,###} K", pair.Key, pair.Value);

                num--;

                if(num == 0)
                {
                    break;
                }
            }
            File.AppendAllLines("testFile.txt", list);




        }
    }
}
