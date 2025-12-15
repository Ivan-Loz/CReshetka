using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
    class Program
    {
       public enum WeatherCondition
        {
            Sunny, Cloudy, Rainy, Snowy, Windy
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Введите (Sunny, Cloudy, Rainy, Snowy, Windy):");
            string input = Console.ReadLine();
            if (Enum.TryParse(input, true, out WeatherCondition light))
            {
                switch (light)
                {
                    case WeatherCondition.Sunny:
                        Console.WriteLine("Светло и жарко");
                        break;
                    case WeatherCondition.Cloudy:
                        Console.WriteLine("Тепло но не светло");
                        break;
                    case WeatherCondition.Rainy:
                        Console.WriteLine("Прохладно и не светло");
                        break;
                    case WeatherCondition.Snowy:
                        Console.WriteLine("Светло и холодно");
                        break;
                    case WeatherCondition.Windy:
                        Console.WriteLine("Ну тут может быть как тепло так и холодно как светло как и темно");
                        break;
                }
            }
        }
        
    }
}
