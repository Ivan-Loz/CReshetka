busing System;
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
            Console.WriteLine("Введите от:");
            numb1 = Console.ReadLine();
            Console.WriteLine("Введите до:");
            numb2 = Console.ReadLine();
            double numbe1 = Convert.ToDouble(numb1);
            double numbe2 = Convert.ToDouble(numb2);
            double chet = 0;
            double NoChet = 0;
            double summ = 0;
            double summ2 = 0;
            for (; numbe1 <= numbe2; numbe1++)
            {
                
                if (numbe1 % 2 == 0)
                {
                    summ += numbe1;
                    chet += 1;
                }
                else
                {
                    summ2 += numbe1;
                    NoChet += 1;
                }
                
            }
            Console.WriteLine("чётные:" + chet);
            Console.WriteLine("не чётные:" + NoChet);
            Console.WriteLine("сумма чётные:" + summ);
            Console.WriteLine("сумма не чётные:" + summ2);
        }
    }
}