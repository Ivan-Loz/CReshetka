using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp18
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                int Stone = 1;
                int Paper = 2;
                int scissors = 3;
                string play;
                int pc = 0;
                int pl = 0;
                Random rnd = new Random();
                pc = rnd.Next(1,3);
                Console.WriteLine("Введите 1 Камень, 2 Бумага, 3 ножницы Введите 4 или больше для остоновки игры");
                play = Console.ReadLine();
                pl = Convert.ToInt32(play);
                if (pl >= 4)
                {
                    System.Environment.Exit(0);
                }
                if (pl < pc)
                {
                    Console.WriteLine("Победа пк");
                }
                else if (pl > pc)
                {
                    Console.WriteLine("Ты победил");
                }
                else
                {
                    Console.WriteLine("Ничья");
                }
               
            }
         
        }
    }
}
