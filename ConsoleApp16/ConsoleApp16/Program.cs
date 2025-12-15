using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp16
{
    public class Fighter
    {
        public string Name { get; set; }
        public virtual void Fight()
        {
            Console.WriteLine("Fighter engages in combat");
        }

    }
    public class Knight : Fighter
    {
        public override void Fight()
        {
            Console.WriteLine("Knight charges with a lance");
        }
        public void ShieldBlock()
        {
            Console.WriteLine("Knight blocks the attack with a shield");
        }
    }
    public class Assassin : Fighter
    {
        public override void Fight()
        {
            Console.WriteLine("Assassin strikes from the shadows");
        }
        public void Stealth()
        {
            Console.WriteLine("Assassin disappears into the shadows");
        }
        
    }
    public class Mage : Fighter
    {
        public override void Fight()
        {
            Console.WriteLine("Mage conjures a fireball");
        }
        public void CastSpell()
        {
            Console.WriteLine("Mage casts a powerful spell");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Knight:");
            Knight m0 = new Knight();
            m0.Name = "Вася";
            m0.Fight();
            m0.ShieldBlock();
            Console.WriteLine("Assassin:");
            Assassin m1 = new Assassin();
            m1.Name = "Крутой тип";
            m1.Fight();
            m1.Stealth();
            Console.WriteLine("Mage:");
            Mage m2 = new Mage();
            m2.Name = "Какой-то тип";
            m2.Fight();
            m2.CastSpell();

        }
    }
}
