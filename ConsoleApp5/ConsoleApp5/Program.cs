using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleApp5
{
    class Program
    {
        static int Korr(int[,] arr)
        {
            int row = arr.GetLength(0);
            int cols = arr.GetLength(0);
            int max= 0;
          for(int i =0;i < row; i++)
            {
                for (int j = 0; j < cols; j++)
                    if (max < arr[i,j])
                    {
                        max = arr[i, j];
                    }
            }
            return max;
        }
        static void Main(string[] args)
        {
            int[,] arr = { { 1, 3, 55, 32, 66, 23, 88 },{6,23,55,65,22,9,90} };
            int max;
           max = Korr(arr);
            Console.WriteLine(max);
        }
    }
}