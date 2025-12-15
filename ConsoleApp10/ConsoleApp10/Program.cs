using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp10
{ 
class GrandmaPie
{
    public string filling;
    public double price;
    public void DescribePie()
    {
      Console.WriteLine($"Пирожок с {filling},стоит {price} ");
      Console.WriteLine("Этот пирожок такой няма няма что прям кот Васька не устоял");
    }
}
    class Grandma
    {
        public GrandmaPie[] pies = new GrandmaPie[3];
        public int pieCount = 0;
        public void AddPie(GrandmaPie pie)
        {
            if(pieCount <= 3)
            {
                pies[pieCount] = pie;
            }
            else if (pieCount > 3)
            {
                Console.WriteLine("Всё Стоп Бабушка устала!");
            }
            pieCount++;
        }
        public void ShowAllPies()
        {
            for (int i = 0; i < pieCount; i++)
            {
                pies[i].DescribePie();
            }
        }
        public double  CalculateTotalEarnings()
        {
            double sum= 0;
            for (int i = 0; i < pieCount; i++)
            {
                sum = pies[i].price +sum;
            }
            return sum;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            GrandmaPie p0 = new GrandmaPie();
            p0.filling = "Яблоко";
            p0.price = 100;
            GrandmaPie p1 = new GrandmaPie();
            p1.filling = "Вишня";
            p1.price = 200;
            GrandmaPie p2 = new GrandmaPie();
            p2.filling = "Пельмени";
            p2.price = 10000;
            Grandma Gala = new Grandma();
            Gala.AddPie(p0);
            Gala.AddPie(p1);
            Gala.AddPie(p2);
            Gala.ShowAllPies();
            Console.WriteLine(Gala.CalculateTotalEarnings());
        }
    }
}