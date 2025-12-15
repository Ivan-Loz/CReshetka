using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp14
{
    class Program
    {
        class Character
        {
            private string Name;

            public string name
            {
                get { return Name; }
                set { Name = value; }
            }
            private int Health;


            public int heal
            {
                get { return Health; }
                set
                {
                    if(value > 0 || value < 100)
                    Health = value;
                    else
                        Console.WriteLine("Так нельзя");
                }
            }
            public int Level { get; set; }

            private int Experience;

            public int exp
            {
                get { return Experience; }
                set {
                    if (value != 100)
                        Experience = value;
                    else if (value == 100)
                        Experience = 0;
                    Level++;
                }
            }
           public void GainExperience(int amount)
            {
                exp = amount;
            }

            public int ataka(Weapon m0, Character z0)
            {

                return z0.heal = z0.heal - m0.damage; 
            }

        }

        class Weapon
        {
            public string Name;
            public int damag;
            public string NameWep
            {
                get { return Name; }
                set { Name = value; }
            }
            public int damage
            {
                get { return damag; }
                set { damag = value; }
            }


        }
        static void Main(string[] args)
        {
            Character Art = new Character();
            Art.name = "Артур";
            Art.heal = 100;
            Art.GainExperience(46);
            Console.WriteLine(Art.name);
            Console.WriteLine(Art.heal);
            Console.WriteLine(Art.exp);
            Console.WriteLine(Art.Level);

            Weapon svorld = new Weapon();
            svorld.NameWep = "Балада о заре";
            svorld.damage = 35;

            Console.WriteLine("До урона");

            Character Kto = new Character();
            Kto.name = "Кто?";
            Kto.heal = 100;
            Kto.GainExperience(46);
            Console.WriteLine(Kto.name);
            Console.WriteLine(Kto.heal);
            Console.WriteLine(Kto.exp);
            Console.WriteLine(Kto.Level);

            Console.WriteLine("После");
            Console.WriteLine($"Получина опыта за удар {Art.exp = Art.ataka(svorld, Kto)}");

            Kto.GainExperience(46);
            Console.WriteLine(Kto.name);
            Console.WriteLine(Kto.heal);
            Console.WriteLine(Kto.exp);
            Console.WriteLine(Kto.Level);
            
        }
    }
}