using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp21
{
    public class Character
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int AttackPower { get; set; }
        public int Defense { get; set; }
        public int Level { get; set; }
        public virtual void DisplayStats()
        {
            Console.WriteLine($"Имя: {Name}");
            Console.WriteLine($"Здоровье: {Health}");
            Console.WriteLine($"Урон: {AttackPower}");
            Console.WriteLine($"Зашита: {Defense}");
            Console.WriteLine($"Уровень: {Level}");
        }
    }
    public interface IAttacker
    {
        void Attack(Character target);
    }
    public interface IDrop
    {
        string DropItem();
    }
    public class Player : Character, IAttacker
    {
        private int exp;

        public int EXP
        {
            get { return exp; }
            set { 
                if(exp == 100)
                {
                    exp = 0;
                    Level += 1;
                }
                else
                exp = value;
                
            }
        }
        public Player(string Name)
        {
            this.Name = Name;
            Health = 100;
            AttackPower = 12;
        }
        public List<string> Inventory { get; } = new List<string>();

        public void Attack(Character target)
        {
            int damage = AttackPower - target.Defense;
            target.Health -= damage;
            Console.WriteLine($"{Name} атакует {target.Name}! Урон: {damage}");
        }
        public void UseItem(string itemName)
        {
            if(itemName == "Зелье")
            {
                Health += 30;
                Inventory.Remove("Зелье");
            }
            else if(itemName == "Крутой клинок")
            {
                AttackPower += 20;
                Inventory.Remove("Крутой клинок");
            }
        }
        public override void DisplayStats()
        {
            Console.WriteLine($"Имя: {Name}");
            Console.WriteLine($"Здоровье: {Health}");
            Console.WriteLine($"Урон: {AttackPower}");
            Console.WriteLine($"Зашита: {Defense}");
            Console.WriteLine($"Уровень: {Level}");
            Console.WriteLine($"Опыт: {exp}/100");
        }
    }
    public class Goblin : Character, IAttacker, IDrop
    {
        public Goblin()
        {
            Health = 50;
            AttackPower = 10;
        }

        public void Attack(Character target)
        {
            int damage = AttackPower - target.Defense;
            target.Health -= damage;
            Console.WriteLine($"{Name} атакует {target.Name}! Урон: {damage}");
        }

        public string DropItem()
        {
            return new Random().Next(2) == 0 ? "Золото" : "Зелье";
        }
    }
    public class Dragon : Character, IAttacker, IDrop
    {
        public Dragon()
        {
            Health = 100;
            AttackPower = 20;
        }

        public void Attack(Character target)
        {
            int damage = AttackPower - target.Defense;
            target.Health -= damage;
            Console.WriteLine($"{Name} атакует {target.Name}! Урон: {damage}");
        }

        public string DropItem()
        {
           return new Random().Next(2) == 0 ? "Крутой клинок" : "Зелье";
        }
    }

    

    class Program
    {
        static void Battle(Character enemy, Player player)
        {
            string Choice;
            int Ch = 0;
            while (true)
            {
                player.DisplayStats();
                Console.WriteLine(" ");
                enemy.DisplayStats();
                Console.WriteLine(" ");
                Console.WriteLine("1 это атака");
                Console.WriteLine("2 это использовать предмет");
                
                Choice = Console.ReadLine();
                Ch = Convert.ToInt32(Choice);
                if (Ch == 1)
                {
                    player.Attack(enemy);
                    ((IAttacker)enemy).Attack(player);

                }
                else if (Ch == 2)
                {
                    string item;
                    for (int i =0;i <= player.Inventory.Count; i++)
                    {
                        Console.WriteLine($"{i}. {player.Inventory[i]}");
                        
                    }
                    Console.WriteLine("Напешите название предмета или 0 для отмены");
                    item = Console.ReadLine();
                    if (item == "0")
                    {
                        break;
                    }
                    else 
                    {
                        player.UseItem(item);
                    }
                if(enemy.Health == 0)
                {
                        player.Inventory.Add(((IDrop)enemy).DropItem());
                }

                }
            }
        }
            static void ExploreDungeon(Player player)
        {
            Random rnd = new Random();
            int Event = 0;
            Event = rnd.Next(3);
            if(Event == 0)
            {
                Console.WriteLine("Найден сундук!");
                Random drop = new Random();
                int dr = 0;
                dr = drop.Next(2);
                if(dr == 0)
                {
                    Console.WriteLine("Вы нашли Золото");
                    player.Inventory.Add("Золото");
                }
                else if(dr == 1)
                {
                    Console.WriteLine("Вы нашли Зелье!");
                    player.Inventory.Add("Зелье");
                }
            }
            else if(Event == 1)
            {
                Console.WriteLine("Вы встретили гоблина!");
                Goblin goblin = new Goblin();
                Battle(goblin, player);
            }
            else if(Event == 2)
            {
                Console.WriteLine("Вы встретели Дракона!");
                Dragon dragon = new Dragon();
                Battle(dragon, player);
            }

        }
        static void UseInventory(Player player)
        {
            while (true)
            {
                string item;
                for (int i = 0; i <= player.Inventory.Count; i++)
                {
                    Console.WriteLine($"{i}. {player.Inventory[i]}");

                }
                Console.WriteLine("Напешите название предмета или 0 для отмены");
                item = Console.ReadLine();
                if (item == "0")
                {
                    break;
                }
                else
                {
                    player.UseItem(item);
                }
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Напиши имя героя:");
            string Name;
            Name = Console.ReadLine();
            Player player = new Player();
            player.Name = Name;
            while (true)
            {
                player.DisplayStats();
                Console.WriteLine(" ");
                Console.WriteLine("1 это исследовать подземелье");
                Console.WriteLine("2 это открыть инвентарь");
                Console.WriteLine("0 это выход");
                string vb;
                int vv;
                vb = Console.ReadLine();
                vv = Convert.ToInt32(vb);
                if (vv == 1)
                {
                    ExploreDungeon(player);
                }
                else if (vv == 2)
                {
                    for (int i = 0; i <= player.Inventory.Count; i++)
                    {
                        Console.WriteLine($"{i}. {player.Inventory[i]}");

                    }
                }
                else if(vv == 0)
                {
                    break;
                }
            }
        }
    }
}