using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oneCard
{
    class Program
    {
        static void Main(string[] args)
        {
            //One Card Rule 확인

            string[] inputTrue = {"S4,","H4","D4","D9","H9","H4","H2","S2" };
            string[] inputFalse = {"C5","H5","D2","D6","D3","S3" };

            string[] input = inputFalse;

            string present = input[0];

            for( int i = 1; i< input.Length; i++)
            {
                bool result = CheckPatternOrNumber(present, input[i]);

                if (result)
                {
                    present = input[i];
                }
                else
                {
                    System.Console.WriteLine("{0},{1}",present,input[i]);

                    break;
                }
            }

            System.Console.WriteLine("End");
             


        }
        static public bool CheckPatternOrNumber(string present, string after)
        {
            bool result = false;

            char[] presentArray = present.ToArray();
            char presentChar = presentArray[0];
            char presentNum = presentArray[1];

            char[] afterArray = after.ToArray();
            char afterChar = afterArray[0];
            char afterNum = afterArray[1];

            if (presentChar == afterChar || presentNum == afterNum)
            {
                result = true;
            }

            return result;
        }

    }
}
