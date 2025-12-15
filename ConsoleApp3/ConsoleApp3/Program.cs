using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int secretNumber = random.Next(1, 101);
            bool a = true;
            string numb1;
            while (a)
            {
                Console.WriteLine("Введите число:");
                numb1 = Console.ReadLine();
                int numbe1 = Convert.ToInt32(numb1);
                if (numbe1 == secretNumber)
                {
                    Console.WriteLine("Ты ГОЙДА!!!");
                    a = false;
                }
                else if (numbe1 < secretNumber)
                {
                    Console.WriteLine("Число N больше");
                }
                else if (numbe1 > secretNumber)
                {
                    Console.WriteLine("Число N меньше");
                }
                
            }
        }
    }
}