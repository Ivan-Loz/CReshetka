using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp11
{
    class Animal
    {
        public string name;
        public int age;
        public string Species;
        public string Superpower;
      public void ShowSuperpower()
        {
            Console.WriteLine($"{name} {Superpower}");
        }


    }
    class Program
    {
        static void Main(string[] args)
        {
            Animal dog = new Animal{name = "Шкет",age = 3,Species = "Собака",Superpower = "Умеет думать правым ухом!"};
            dog.ShowSuperpower();
            Animal kot = new Animal { name = "Барсик", age = 20000, Species = "Кот", Superpower = "Бессмертный" };
            kot.ShowSuperpower();

        }
    }
}
