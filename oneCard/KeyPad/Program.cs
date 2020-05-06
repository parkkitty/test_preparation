using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyPad
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputData = "262619";
            char[] inputDataArray = inputData.ToCharArray();

            int[] inputInt = new int[inputDataArray.Length];

            int idx = 0;

            foreach(char c in inputDataArray)
            {
                inputInt[idx]=int.Parse(c.ToString());
                idx++;
            }

            //bool result = seriseCheck(inputInt);
            bool series = seriseSameNum(inputInt);

            if (!patternCheck(inputInt))
            {
                System.Console.WriteLine("연속되는 숫자 패턴 존재");
            }


            int i = 0;
        }

        static bool seriseCheck(int[] pattern)
        {
            bool result = false;
            int serise = 0;

            Dictionary<int, List<int>> keyMap = new Dictionary<int, List<int>>();
            keyMap[0] = new List<int>() {1,2,3 };
            keyMap[1] = new List<int>() {2, 4, 5 };
            keyMap[2] = new List<int>() {0,1,3,4,5,6};
            keyMap[3] = new List<int>() {2,5,6 };
            keyMap[4] = new List<int>() {1,2,5,7,8 };
            keyMap[5] = new List<int>() {1,2,3,4,6,7,8,9};
            keyMap[6] = new List<int>() { 2,3,5,8,9};
            keyMap[7] = new List<int>() {4,5,8 };
            keyMap[8] = new List<int>() {4,5,6,7,9 };
            keyMap[9] = new List<int>() {5,6,8 };

            for(int i=0; i<pattern.Length-1; i++)
            {
                if (keyMap[pattern[i]].Contains(pattern[i + 1]))
                {
                    serise++;
                }
                else
                {
                    serise = 0;
                }

                if (serise == 2)
                {
                    System.Console.WriteLine("3 Numbers are serise");
                    break;
                }
                
            }

            return result;
        }

        static bool seriseSameNum(int[] pattern)
        {
            bool result = true;

            for(int i=0; i < pattern.Length-2; i++)
            {
                if(pattern[i]==pattern[i+1]&& pattern[i]==pattern[i + 2] )
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        static bool patternCheck(int[] pattern)
        {
            bool result = true;

            for(int i =0; i<pattern.Length-3; i++)
            {
                //2자리 체크, 3자리 체크
                string firstTwoNumber = pattern[i].ToString() + pattern[i + 1].ToString();
                string secondTwoNumber = pattern[i+2].ToString() + pattern[i +3].ToString();

                if (firstTwoNumber.Equals(secondTwoNumber))
                {
                    result = false;
                    return result;
                }

            }

            for (int i = 0; i < pattern.Length - 5; i++)
            {
                //2자리 체크, 3자리 체크
                string firstThreeNumber = pattern[i].ToString() + pattern[i + 1].ToString()+ pattern[i+2].ToString();
                string secondThreeNumber = pattern[i+3].ToString() + pattern[i + 4].ToString() + pattern[i + 5].ToString();

                if (firstThreeNumber.Equals(secondThreeNumber))
                {
                    result = false;
                    return result;
                }

            }

            return result;

        }
    }
}
