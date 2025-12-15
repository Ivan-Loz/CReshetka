using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp17
{
    public abstract class GameCharacter
    {
        public string Name { get; set; }
        public int Speed { get; set; }
        private int DistanceCovered;

        public int distanceCovered
        {
            get { return DistanceCovered; }
            set {
                DistanceCovered = value;
            }
        }

        public abstract void Run();
       
    }
   public class Dog : GameCharacter
    {
        public string Name { get; set; }
        public int Speed { get; set; }
        private int DistanceCovered;

        public int distanceCovered
        {
            get { return DistanceCovered; }
            set
            {
                    DistanceCovered = value;
               
            }
        }
        public override void Run()
        {
            for (int i = 0;i <= Speed; i++)
            {
                distanceCovered = distanceCovered + i;
            }
        }
    
    }
    public class Cat : GameCharacter
    {
        public string Name { get; set; }
        public int Speed { get; set; }
        private int DistanceCovered = 1;

        public int distanceCovered
        {
            get { return DistanceCovered; }
            set
            {
                
                DistanceCovered = value;
            }
        }
        public override void Run()
        {
            for (int i = 0; i <= Speed; i++)
            {
                distanceCovered = distanceCovered + i;
            }
        }
 
    }
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Собака:");
            Dog m0 = new Dog();
            m0.Name = "Тузик";
            m0.Speed = 125;
            m0.Run();
            Console.WriteLine($"Пробежал {m0.distanceCovered}");
            Console.WriteLine("Кот:");
            Cat m1 = new Cat();
            m1.Name = "Петя";
            m1.Speed = 1000;
            m1.Run();
            Console.WriteLine($"Пробежал {m1.distanceCovered}");
        }
    }
}
