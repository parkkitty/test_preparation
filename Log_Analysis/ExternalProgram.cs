using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log_Analysis
{
    class ExternalProgram
    {

        string path;
        string answer;

        public ExternalProgram(string programPath)
        {
            this.path = programPath;

        }

        public string GetReturnFromExtProg(string argument)
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            Process ps = new Process();
            psi.FileName = this.path;
            psi.RedirectStandardOutput = true;
            //psi.RedirectStandardInput = true;
            psi.UseShellExecute = false;
            psi.Arguments = argument;

            ps.StartInfo = psi;
            ps.Start();

            StreamReader sr = ps.StandardOutput;

            string s;


            if ((s=sr.ReadLine() )!= null)
            {
                answer = s;
            }
            else
            {
                answer = "NO";
            }

            return answer;
        }
    }
}
