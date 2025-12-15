using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp20
{
    public interface IWarrior
    {
        void getDescription();
        void attack();
        void defend();

    }
    public interface IMage
    {
        void getDescription();
        void attack();
        void defend();

    }
    public interface IArcher
    {
        void getDescription();
        void attack();
        void defend();

    }
    public class ElfWarrior : IWarrior
    {
        public string Name;
        public int Hp;
        public int strength;
        public ElfWarrior(string name, int hp, int strength)
        {
            this.Name = name;
            this.Hp = hp;
            this.strength = strength;
        }
        public void getDescription()
        {
            Console.WriteLine($"Имя {Name}");
            Console.WriteLine($"ХП {Hp}");
            Console.WriteLine($"Сила {strength}");

        }
        public void attack()
        {
            Console.WriteLine("Бьёт мечём!");
        }
        public void defend()
        {
            Console.WriteLine("Прикрывается щитом");
        }


    }
    public class ElfMage : IMage
    {
        public string Name;
        public int Hp;
        public int mana;
        public ElfMage(string name, int hp, int mana)
        {
            this.Name = name;
            this.Hp = hp;
            this.mana = mana;
        }
        public void getDescription()
        {
            Console.WriteLine($"Имя {Name}");
            Console.WriteLine($"ХП {Hp}");
            Console.WriteLine($"Мана {mana}");

        }
        public void attack()
        {
            Console.WriteLine("Огненый шар");
        }
        public void defend()
        {
            Console.WriteLine("Каст барьера");
        }


    }
    public class ElfArcher : IArcher
    {
        public string Name;
        public int Hp;
        public int dexterity;
        public ElfArcher(string name, int hp, int dexterity)
        {
            this.Name = name;
            this.Hp = hp;
            this.dexterity = dexterity;
        }
        public void getDescription()
        {
            Console.WriteLine($"Имя {Name}");
            Console.WriteLine($"ХП {Hp}");
            Console.WriteLine($"Мана {dexterity}");

        }
        public void attack()
        {
            Console.WriteLine("Выпускает стрелы ");
        }
        public void defend()
        {
            Console.WriteLine("Убегает");
        }


    }


    public class OrcWarrior : IWarrior
    {
        public string Name;
        public int Hp;
        public int strength;
        public OrcWarrior(string name, int hp, int strength)
        {
            this.Name = name;
            this.Hp = hp;
            this.strength = strength;
        }
        public void getDescription()
        {
            Console.WriteLine($"Имя {Name}");
            Console.WriteLine($"ХП {Hp}");
            Console.WriteLine($"Сила {strength}");

        }
        public void attack()
        {
            Console.WriteLine("Бьёт мечём!");
        }
        public void defend()
        {
            Console.WriteLine("Прикрывается щитом");
        }


    }
    public class OrcMage : IMage
    {
        public string Name;
        public int Hp;
        public int mana;
        public OrcMage(string name, int hp, int mana)
        {
            this.Name = name;
            this.Hp = hp;
            this.mana = mana;
        }
        public void getDescription()
        {
            Console.WriteLine($"Имя {Name}");
            Console.WriteLine($"ХП {Hp}");
            Console.WriteLine($"Мана {mana}");

        }
        public void attack()
        {
            Console.WriteLine("Огненый шар");
        }
        public void defend()
        {
            Console.WriteLine("Каст барьера");
        }


    }
    public class OrcArcher : IArcher
    {
        public string Name;
        public int Hp;
        public int dexterity;
        public OrcArcher(string name, int hp, int dexterity)
        {
            this.Name = name;
            this.Hp = hp;
            this.dexterity = dexterity;
        }
        public void getDescription()
        {
            Console.WriteLine($"Имя {Name}");
            Console.WriteLine($"ХП {Hp}");
            Console.WriteLine($"Мана {dexterity}");

        }
        public void attack()
        {
            Console.WriteLine("Выпускает стрелы ");
        }
        public void defend()
        {
            Console.WriteLine("Убегает");
        }


    }

    public class HumanWarrior : IWarrior
    {
        public string Name;
        public int Hp;
        public int strength;
        public HumanWarrior(string name, int hp, int strength)
        {
            this.Name = name;
            this.Hp = hp;
            this.strength = strength;
        }
        public void getDescription()
        {
            Console.WriteLine($"Имя {Name}");
            Console.WriteLine($"ХП {Hp}");
            Console.WriteLine($"Сила {strength}");

        }
        public void attack()
        {
            Console.WriteLine("Бьёт мечём!");
        }
        public void defend()
        {
            Console.WriteLine("Прикрывается щитом");
        }


    }
    public class HumanMage : IMage
    {
        public string Name;
        public int Hp;
        public int mana;
        public HumanMage(string name, int hp, int mana)
        {
            this.Name = name;
            this.Hp = hp;
            this.mana = mana;
        }
        public void getDescription()
        {
            Console.WriteLine($"Имя {Name}");
            Console.WriteLine($"ХП {Hp}");
            Console.WriteLine($"Мана {mana}");

        }
        public void attack()
        {
            Console.WriteLine("Огненый шар");
        }
        public void defend()
        {
            Console.WriteLine("Каст барьера");
        }


    }
    public class HumanArcher : IArcher
    {
        public string Name;
        public int Hp;
        public int dexterity;
        public HumanArcher(string name, int hp, int dexterity)
        {
            this.Name = name;
            this.Hp = hp;
            this.dexterity = dexterity;
        }
        public void getDescription()
        {
            Console.WriteLine($"Имя Мага {Name}");
            Console.WriteLine($"ХП Мага {Hp}");
            Console.WriteLine($"Мана {dexterity}");

        }
        public void attack()
        {
            Console.WriteLine("Выпускает стрелы ");
        }
        public void defend()
        {
            Console.WriteLine("Убегает");
        }


    }
    public interface ICharacterFactory
    {
        IWarrior createWarrior();
        IMage createMage();
        IArcher createArcher();
    }
    public class ElfFactory : ICharacterFactory
    {
        public IWarrior createWarrior() => new ElfWarrior("Паша", 100, 10);
        public IMage createMage() => new ElfMage("Егор", 100, 400);
        public IArcher createArcher() => new ElfArcher("Сергей", 100, 50);
    }
    public class OrcFactory : ICharacterFactory
    {
        public IWarrior createWarrior() => new ElfWarrior("Олег", 100, 10);
        public IMage createMage() => new ElfMage("Крут", 100, 400);
        public IArcher createArcher() => new ElfArcher("ДАААА", 100, 50);
    }
    public class HumanFactory : ICharacterFactory
    {
        public IWarrior createWarrior() => new ElfWarrior("Так", 100, 10);
        public IMage createMage() => new ElfMage("Кто", 100, 400);
        public IArcher createArcher() => new ElfArcher("Шавуха", 100, 50);
    }

public class Game
    {
        private readonly IWarrior _warrior;
        private readonly IMage _mage;
        private readonly IArcher _archer;

        
        public Game(ICharacterFactory factory)
        {
            _warrior = factory.createWarrior();
            _mage = factory.createMage();
            _archer = factory.createArcher();
        }

        public void CreateTeam()
        {
           _warrior.getDescription();
            _mage.getDescription();
           _archer.getDescription();
        }

        public void TeamAction()
        {
            _warrior.attack();
            _mage.attack();
            _archer.attack();
        }
    }


    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Создание команды Орков:");
            Game orcTeam = new Game(new OrcFactory());
            orcTeam.CreateTeam();
            orcTeam.TeamAction();
        }
    }
}