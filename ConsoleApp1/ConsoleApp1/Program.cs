using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string b;
            string input;
            Console.WriteLine("Введите число:");
            input = Console.ReadLine();
            Console.WriteLine("Введите число номер 2:");
            b = Console.ReadLine();
            double number2 = Convert.ToDouble(b);
            double number = Convert.ToDouble(input);
            Console.WriteLine(number / number2);
            
        }
    }
}
