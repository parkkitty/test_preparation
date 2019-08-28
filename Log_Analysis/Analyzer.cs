using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Log_Analysis
{
    class Analyzer
    {
        string LogFileName;
        string ExtFilePath;
        ExternalProgram extProgram;
        private object lockObject = new object();

        public Analyzer(string fileName, string path)
        {
            this.LogFileName = fileName;
            this.ExtFilePath = path;

            this.extProgram = new ExternalProgram(this.ExtFilePath);


        }


        public void FileAnalysis(string reportName)
        {
            List<string> logs = File.ReadAllLines(this.LogFileName).ToList();
            Dictionary<string, int> errorType = new Dictionary<string, int>();

            foreach (string s in logs)
            {
                string[] temp = s.Split('#');
                string errType = temp[1];
                string message = temp[2];

                //errorType.ContainsKey

                if (errorType.ContainsKey(errType))
                {
                    errorType[errType] = errorType[errType]+1;
                }
                else
                {
                    errorType.Add(errType, 1);
                }

            }

            List<string> result = new List<string>();

            foreach(KeyValuePair<string, int> pair in errorType)
            {
                result.Add(pair.Key + "#" + pair.Value.ToString());
            }

            File.WriteAllLines(reportName, result);


        }

        public void ThreadAnaysis()
        {

            List<string> logs = File.ReadAllLines(LogFileName).ToList();
            Thread.Sleep(100);

            int num = 0;

            foreach(string s in logs)
            {

                string[] tmp = s.Split('#');
                string logFileName = tmp[1];
                string message = tmp[2];

                string answer = this.extProgram.GetReturnFromExtProg(message);
               // string param = logFileName+"#"+ answer ;
                //ThreadPool.QueueUserWorkItem(WriteFile, param);
                //Task aTask = WriteFile(logFileName, answer);
                //aTask.Start();
               // await WriteFile(logFileName, answer);

                //await aTask;
                var t = new Thread(() => WriteFile(logFileName, answer));
                t.Start();
                Debug.WriteLine("{0}:{1}번째 쓰레드 시작! ",logFileName, num);
                num++;
                //Thread t = new Thread(WriteFile);

            }


        }

        public void WriteFile(string fileName, string answer)
        {
           

            string fileFullName = fileName + ".txt";
           
           /* if (!File.Exists(fileFullName))
             {
                File.Create(fileFullName);
                File.
             }
             */
            lock (lockObject)
            {
                using (var fs = File.Open(fileFullName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);

                    sw.WriteLine(answer);
                    sw.Flush();
                    sw.Close();
                    fs.Close();
                }

            }

                //File.AppendAllLines()
              
            
           

        }

    }
}
