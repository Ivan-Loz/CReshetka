using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[10];
            Random rand = new Random();
            int count = 0;
            for (int i = 0; i < 10; i++)
            {
                arr[i] = rand.Next(100);
            }
            foreach (int item in arr)
            {
                if(item % 2 == 0)
                {
                    count++;
                }
            }
            int [] arr2 = new int[count];
            int num= 0;
            for (int i = 0;i < 10; i++)
            {
                if (arr[i] % 2 == 0)
                {
                    arr2[i] += arr[i];
                }
            }for (int i = 0; i < arr2[10]; i++)
            {
                num += arr2[i];
                Console.WriteLine(num);
            }
            
            
        }
    }
}
