using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp13
{
    class Program
    {
      static void Foo(ref int[] numbers,int a) {
                Array.Resize(ref numbers, numbers.Length + 1);
            numbers[numbers.Length - 1] = a;
            }
        static int Max(int[] arr)
        {
            int max = 0;
            for (int i = 0; i <= arr.Length - 1; i++)
            {
                if (arr[i] >= max)
                {
                    max = arr[i];
                }
            }
            return max;
        }
        static void Main(string[] args)
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            Foo(ref numbers, 40);
            Console.WriteLine(Max(numbers));
        }
    }
}
