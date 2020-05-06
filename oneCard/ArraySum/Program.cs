using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArraySum
{
    class Program
    {
        static void Main(string[] args)
        {
            //MXN 행렬을 행으로 더해서 열을 추가한다
            //가운데 기준으로 좌우를 바꾼다. (홀수열일 경우, 가운데는 가만히 둔다)
            //열을 더해서 행을 추가한다
            
            int[,] test = new int[,] { { 82, 69, 46,20,5,74 },
                                       {98,40,21,34,29,21 },
                                       {59,99,50,39,33,76},
                                       {14,14,7,67,24,5 },
                                       {98,9,46,21,49,50 },
                                       {24,19,37,11,35,66 },
                                       {11,30,87,5,59,78 },
                                       {28,95,29,79,35,99 },
                                       {23,21,79,74,80,41 },
                                       {84,18,36,83,27,20 },
                                       };

            int[,] result = addRow(test);
            int[,] change = changeLeftRight(result);
            int[,] addCol = addColumn(change);

            
            

            System.Console.WriteLine("OK");


        }

        static int[,] addRow(int[,] array)
        {
            
            int[,] result = new int[array.GetLength(0), array.GetLength(1)+1];

            for(int i=0; i<array.GetLength(0); i++)
            {
                int sum = 0;
                for(int j=0; j < array.GetLength(1); j++)
                {
                    result[i, j] = array[i, j];
                    sum += array[i,j];
                }

                result[i, array.GetLength(1)] = sum;

            }

            return result;


        }

        static int [,] changeLeftRight(int[,] array)
        {
            int[,] result = new int[array.GetLength(0), array.GetLength(1)];
            //거꾸로 받으

            bool odd = true;

            if(array.GetLength(1)%2 == 0)
            {
                odd = false;
            }
            
            int centerIdx = array.GetLength(1) / 2;


            if (!odd)
            {
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    for (int j = 0; j < array.GetLength(1); j++)
                    {
                        if (j < centerIdx)
                        {
                            result[i, j + centerIdx] = array[i, j];
                        }
                        else
                        {
                            result[i, j - centerIdx] = array[i, j];
                        }
                    }
                }
            }
            else
            {
                for(int i = 0; i < array.GetLength(0); i++)
                {
                    for(int j=0; j < array.GetLength(1); j++)
                    {
                        if (j < centerIdx)
                        {
                            result[i, j + centerIdx + 1] = array[i, j];
                        }
                        else if (j == centerIdx)
                        {
                            result[i, j] = array[i, j];
                        }
                        else
                        {
                            result[i, j -(centerIdx + 1)] = array[i, j];
                        }
                    }
                }
            }


            // 3개면 0,1,2
            //4개면 0,1,2,3 -> 23

            return result;
        }

        static int[,] addColumn(int[,] array)
        {
            int[,] result = new int[array.GetLength(0)+1, array.GetLength(1)];

            for(int j=0; j < array.GetLength(1); j++)
            {
                int sum = 0;
                for(int i=0; i < array.GetLength(0); i++)
                {
                    result[i, j] = array[i, j];
                    sum += array[i, j];
                }

                result[array.GetLength(0), j] = sum;
            }

            return result;
        }
    }
}
