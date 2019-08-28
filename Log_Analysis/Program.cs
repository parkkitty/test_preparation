using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log_Analysis
{
    class Program
    {
        static void Main(string[] args)
        {
            string ProgramPath = "";
            DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
            ProgramPath = di.Parent.Parent.FullName + @"\TestProgram";

            ExternalProgram exp = new ExternalProgram(ProgramPath);

           // string answer = exp.GetReturnFromExtProg("TEST");
           // System.Console.WriteLine("From Ext Program: " + answer);

            //1.Message Read & Analysis Class

            Analyzer anal = new Analyzer(di.Parent.Parent.FullName + @"\LOG_FILE.txt", ProgramPath);
            anal.FileAnalysis("test.txt");
            
            anal.ThreadAnaysis();


        }
    }
}
