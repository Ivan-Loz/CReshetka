using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            string numb1;
            string numb2;
            string z;
            Console.WriteLine("Введите число:");
            numb1 = Console.ReadLine();
            Console.WriteLine("Введите число номер 2:");
            numb2 = Console.ReadLine();
            Console.WriteLine("Выбери знак: +,-,*,/ :");
            z = Console.ReadLine();
            double numbe1 = Convert.ToDouble(numb1);
            double numbe2 = Convert.ToDouble(numb2);
            switch (z)
            {
                case "+":
                    Console.WriteLine(numbe1 + numbe2);
                    break;
                case "-":
                    
                    Console.WriteLine(numbe1 - numbe2);
                    break;
                case "*":
                    Console.WriteLine(numbe1 * numbe2);
                    break;
                case "/":
                    if (numbe2 == 0)
                    {
                        Console.WriteLine("Делить на 0 не надо");
                        
                    }
                    else
                        Console.WriteLine(numbe1 / numbe2);
                        break;
                default:
                    Console.WriteLine("Ну ты да");
                    break;




            }
        }
    }
}