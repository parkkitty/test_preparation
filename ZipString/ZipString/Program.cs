using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipString
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "ab9cd2";

            char[] inputArray = input.ToArray();
            List<char> answer = new List<char>();

            foreach(char c in inputArray)
            {
                if (c > 49 && c < 58)
                {
                    //숫자
                    int num = int.Parse(c.ToString());
                    char zipChar = answer.Last();

                    for (int i=0; i<num-1; i++)
                    {
                        answer.Add(zipChar);
                    }

                }
                else
                {
                    //문자
                    answer.Add(c);
                }
            }

            string answerString = "";

            foreach(char c in answer)
            {
                answerString = answerString + c;
            }

            System.Console.WriteLine("Input String : {0}", input);
            System.Console.WriteLine("Result : {0}", answerString);

            System.Console.ReadLine();

        }
    }
}
