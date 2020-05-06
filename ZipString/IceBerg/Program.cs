using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceBerg
{
    class Program
    {
        static void Main(string[] args)
        {

            //1. 바깥쪽 얼음을 찾는 함수
            //2. 안쪽 얼음을 찾는 함수
            //3. 얼음은 4, 물은 0, 내부물은 9로 바꾸는 함수

            int[,] input = new int[,] { { 0, 0, 0, 0 }, { 0, 0, 4, 0 }, { 0, 4, 0, 4 }, { 0, 4, 0, 4 }, { 0, 4, 4, 4 }, { 0, 0, 0, 0 } };

            int[,] convertedMap = ConvertInnerWater(input);

            int year = 0;

            while (hasIce(convertedMap))
            {
                year++;
                convertedMap = Warm(convertedMap);
                convertedMap = ConvertInnerWater(convertedMap);
            }

            System.Console.WriteLine(year);
            System.Console.WriteLine("");

        }

        static int[,] ConvertInnerWater(int[,] input)
        {
            int[,] innerMap = new int[input.GetLength(0), input.GetLength(1)];

            int row = input.GetLength(0);
            int col = input.GetLength(1);

            //외곽부터 초기화
            for (int i = 0; i < row; i++)
            {

                for (int j = 0; j < col; j++)
                {


                    innerMap[i,j] = input[i, j];

                    if (i == 0 || j == 0 || i == row - 1 || j == col - 1)
                    {
                        if (innerMap[i, j] == 0)
                        {
                            innerMap[i, j] = -1;
                        }

                    }

                }

            }

            //주변 체크

            for(int i=0; i<Math.Max(row,col); i++)
            {
                CheckOuterWater(innerMap);
                
            }

            CheckInnerWater(innerMap); // 안쪽 물을 9로 바꾼다
            RollBackOutWater(innerMap); //-1을 0으로 바꾼다

            return innerMap;
        }

        static void CheckOuterWater(int[,] input)
        {
            //int[,] updatedArray = input;

            for (int i=0; i< input.GetLength(0); i++)
            {
                for(int j=0; j < input.GetLength(1); j++)
                {
                    if (input[i, j] == -1)
                    {
                        continue;
                    }
                    else if(input[i,j] == 0){

                        int[] around = getAround(input, i, j);

                        List<int> aroundList = around.ToList();

                        if (aroundList.Contains(-1))
                        {
                            input[i, j] = -1;
                        }
                    }
                }
            }

            //return updatedArray;

        }

        static int[] getAround(int[,] input, int i, int j)
        {
            int[] around = new int[4];

            int up = -9;
            int down = -9;
            int left = -9;
            int right = -9;

            if (i != 0)
            {
                up = input[i - 1, j];
                //Up 구할수 있음
            }
            if (i != input.GetLength(0) - 1)
            {
                down = input[i + 1, j];
                //Down 구할수 있음
            }
            if (j != 0)
            {
                left = input[i, j - 1];
                //Left
            }
            if (j != input.GetLength(1) - 1)
            {
                right = input[i, j + 1];
                //Right
            }

            around[0] = up;
            around[1] = down;
            around[2] = left;
            around[3] = right;

            return around;
        }

        static void CheckInnerWater(int[,] input)
        {
            //int[,] updatedInput = input;

            for(int i=0; i< input.GetLength(0); i++)
            {
                for(int j=0; j < input.GetLength(1); j++)
                {
                    if (input[i, j] == 0)
                    {
                        input[i, j] = 9;
                    }
                }
            }
            //return input;
        }

        static void RollBackOutWater(int[,] input)
        {
            //int[,] rollBack = input;

            for(int i=0; i< input.GetLength(0); i++)
            {
                for(int j=0; j < input.GetLength(1); j++)
                {
                    if (input[i, j] == -1)
                    {
                        input[i, j] = 0;
                    }
                }
            }

            //return input;
        }

        static int[,] Warm(int[,] input)
        {
            int[,] innerMap = new int[input.GetLength(0), input.GetLength(1)];

            for(int i=0; i< input.GetLength(0); i++)
            {
                for(int j=0; j< input.GetLength(1); j++)
                {
                    innerMap[i, j] = input[i, j];
                }
            }

            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {

                    if(innerMap[i, j]>=1&& innerMap[i, j] <= 4)
                    {
                        int[] around = getAround(input,i,j);
                        int outWaterCnt = 0;
                        foreach(int k in around)
                        {
                            if(k == 0)
                            {
                                outWaterCnt++;
                            }
                        }

                        if (outWaterCnt == 1)
                        {
                            innerMap[i, j] = Math.Max(0, innerMap[i, j] - 1);
                        }
                        else if (outWaterCnt == 2)
                        {
                            innerMap[i, j] = Math.Max(0, innerMap[i, j] - 2);
                        }
                        else if (outWaterCnt == 3)
                        {
                            innerMap[i, j] = Math.Max(0, innerMap[i, j] - 3);
                        }
                        else if (outWaterCnt == 4)
                        {
                            innerMap[i, j] = Math.Max(0, innerMap[i, j] - 4);

                        }
                    }

                    
                }
            }

            for(int i=0; i< input.GetLength(0); i++)
            {
                for(int j=0; j < input.GetLength(1); j++)
                {

                    if(innerMap[i,j] == 9)
                    {
                        innerMap[i, j] = 0;
                    }

                }
            }

            return innerMap;
        }

        static bool hasIce(int[,] input)
        {
            for(int i=0; i<input.GetLength(0); i++)
            {
                for(int j=0; j<input.GetLength(1); j++)
                {
                    if (input[i, j] != 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
