using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7
{
    class Program
    {
        public enum BeverageType
        {
            Coffee, Tea, Juice, Water
        }
        public static int GetBeveragePrice(BeverageType drink)
        {
            int sum = 0;
            switch (drink)
            {
                case BeverageType.Coffee:
                    sum = 100;
                    return sum;

                case BeverageType.Tea:
                    sum = 50;
                    return sum;

                case BeverageType.Juice:
                    sum = 150;
                    return sum;

                case BeverageType.Water:
                    sum = 20;
                    return sum;

            }
            return 0;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Скажи Coffe или Tea или Juice или Water");
            string drink = Console.ReadLine();
            int sum= 0;
            switch (drink)
            
            {
                case "Coffee":
                  sum = GetBeveragePrice(BeverageType.Coffee);
                    Console.WriteLine(sum);
                    break;
                case "Tea":
                    sum = GetBeveragePrice(BeverageType.Tea);
                    Console.WriteLine(sum);
                    break;
                case "Juice":
                    sum = GetBeveragePrice(BeverageType.Juice);
                    Console.WriteLine(sum);
                    break;
                case "Water":
                    sum = GetBeveragePrice(BeverageType.Water);
                    Console.WriteLine(sum);
                    break;
            }


        }
    }
}