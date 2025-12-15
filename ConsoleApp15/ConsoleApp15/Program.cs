using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp15
{
   public class Character
    {
        public string Name { get; set; }
        public virtual void Attack()
        {
            Console.WriteLine("Character attacks with a basic strike");
        }
    }
    public class Warrior : Character
    {
        public override void Attack()
        {
            Console.WriteLine("Warrior swings a mighty sword");
        }
        public void Defend()
        {
            Console.WriteLine("Warrior raises a shield to block incoming attacks");
        }
    }
    public class Mage : Character
    {
        public override void Attack()
        {
            Console.WriteLine("Mage casts a powerful spell");
        }
        public void Teleport()
        {
            Console.WriteLine("Mage teleports to a safe distance");
        }
    }
    public class Archer : Character
    {
        public override void Attack()
        {
            Console.WriteLine("Archer shoots a precise arrow");
        }
        public void Hide()
        {
            Console.WriteLine("Archer hides in the shadows");
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Warrior");
            Warrior m0 = new Warrior();
            m0.Name = "Сережа";
            m0.Attack();
            m0.Defend();
            Console.WriteLine("Mage");
            Mage m1 = new Mage();
            m1.Name = "Артур";
            m1.Attack();
            m1.Teleport();
            Console.WriteLine("Archer");
            Archer m2 = new Archer();
            m2.Name = "Кто этот тип?";
            m2.Attack();
            m2.Hide();
        }
    }
}
