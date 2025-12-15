using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace pz5
{
    class Program
    {
        static string dbConn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FantasyDB;Integrated Security=True";

        static void Main(string[] args)
        {
            Console.WriteLine("===        Fantasy Battle Arena        ===");

            GetHeroes();

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            while (true)
            {
                Console.WriteLine("\n1. выбрать героя и начать бой");
                Console.WriteLine("2. Показать героев");
                Console.WriteLine("3. выход");
                Console.Write("Выберите пункт: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        StartBattleMenu(); break;
                    case "2":
                        for (int i = 0; i < GetHeroes().Count; i++)
                        {
                            Console.WriteLine($"Герой: {i}");
                            Console.WriteLine($"Айди героя: {GetHeroes()[i].Id}");
                            Console.WriteLine($"Имя: {GetHeroes()[i].Name}");
                            Console.WriteLine($"Левл: {GetHeroes()[i].Level}");
                            Console.WriteLine("");
                        }
                         break;
                    case "3": return;
                    default: Console.WriteLine("Ошибка ввода."); break;
                }
            } 
        }

        static List<Hero> GetHeroes()
        {
            List<Hero> heroes = new List<Hero>();

            using (var conn = new SqlConnection(dbConn))
            {
                conn.Open();
                string sql = "SELECT Id, Name, Race, Leve FROM Hero";

                using (var cmd = new SqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        heroes.Add(new Hero
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Race = reader.GetString(2),
                            Level = reader.GetInt32(3)
                        });
                    }
                }
            }

            return heroes;
        }

        static List<Item> GetHeroItems(int heroId)
        {
            List<Item> items = new List<Item>();

            using (var conn = new SqlConnection(dbConn))
            {
                conn.Open();
                string sql = @"";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", heroId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new Item
                            {
                                Name = reader.GetString(0),
                                Type = reader.GetString(1),
                                Quantity = reader.GetInt32(2)
                            });
                        }
                    }
                }
            }

            return items;
        }
        static void LogTurn(int battleId, int turn, int actorId, string action, string value)
        {
            using (var conn = new SqlConnection(dbConn))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "INSERT INTO BattleLog (BattleId, TurnNumber, ActorHeroId, Action, Value) VALUES (@b,@t,@a,@act,@val)", conn);
                cmd.Parameters.AddWithValue("@b", battleId);
                cmd.Parameters.AddWithValue("@t", turn);
                cmd.Parameters.AddWithValue("@a", actorId);
                cmd.Parameters.AddWithValue("@act", action);
                cmd.Parameters.AddWithValue("@val", value);
                cmd.ExecuteNonQuery();
            }
        }
        static Hero LoadHero(int id)
        {
            using (var conn = new SqlConnection(dbConn))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT Id, Name, Race, Leve, Power FROM Hero WHERE Id=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                using (var r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        return new Hero
                        {
                            Id = (int)r["Id"],
                            Name = (string)r["Name"],
                            Race = (string)r["Race"],
                            Level = (int)r["Leve"],
                            Power = (int)r["Power"]
                        };
                    }
                }
            }
            return null;
        }
        static void StartBattleMenu()
        {
            for (int i = 0; i < GetHeroes().Count; i++)
            {
                Console.WriteLine($"Герой: {i}");
                Console.WriteLine($"Айди героя: {GetHeroes()[i].Id}");
                Console.WriteLine($"Имя: {GetHeroes()[i].Name}");
                Console.WriteLine($"Левл: {GetHeroes()[i].Level}");
                Console.WriteLine("");
            }
            Console.Write("\nВведите ID своего героя: ");
            if (!int.TryParse(Console.ReadLine(), out int heroId)) return;

            Console.Write("Введите ID противника: ");
            if (!int.TryParse(Console.ReadLine(), out int enemyId)) return;

            var hero = LoadHero(heroId);
            var enemy = LoadHero(enemyId);
            if (hero == null || enemy == null)
            {
                Console.WriteLine("Некорректные ID!");
                return;
            }

            StartBattle(hero, enemy);
        }
        static void StartBattle(Hero player, Hero enemy)
        {
            int playerHP = player.HP;
            int enemyHP = enemy.HP;
            int turn = 1;
            int battleId = InsertBattle(player.Id, enemy.Id);

            Console.WriteLine($"\nНачало битвы! {player.Name} против {enemy.Name}\n");

            var rnd = new Random();
            while (playerHP > 0 && enemyHP > 0)
            {
                Console.WriteLine($"\n--- Сейчас ход {turn} ---");
                Console.WriteLine($"{player.Name}: {playerHP} HP | {enemy.Name}: {enemyHP} HP");
                Console.WriteLine("1. Удар");
                Console.WriteLine("2. Использовать предмет");
                Console.WriteLine("3. Ныть с позором");
                Console.Write("Выбор: ");
                string action = Console.ReadLine();

                if (action == "1")
                {
                    int dmg = rnd.Next(player.Power / 2, player.Power + 1);
                    enemyHP -= dmg;
                    Console.WriteLine($"{player.Name} наносит {dmg} урона!");
                    LogTurn(battleId, turn, player.Id, "Attack", $"Damage={dmg}");
                }
                else if (action == "2")
                {
                    Console.WriteLine("Вы используете кольцо силы и лечитесь на 10 HP!");
                    playerHP += 10;
                    LogTurn(battleId, turn, player.Id, "UseItem", "RingOfPower +10HP");
                }
                else if (action == "3")
                {
                    Console.WriteLine($"{player.Name} ноет!");
                    LogTurn(battleId, turn, player.Id, "Retreat", "-");
                    UpdateBattle(battleId, enemy.Id);
                    return;
                }
                else
                {
                    Console.WriteLine("не понял");
                    continue;
                }

                if (enemyHP <= 0) break;

                
                int enemyDmg = rnd.Next(enemy.Power / 2, enemy.Power + 1);
                playerHP -= enemyDmg;
                Console.WriteLine($"{enemy.Name} атакует и наносит {enemyDmg} урона!");
                LogTurn(battleId, turn, enemy.Id, "Attack", $"Damage={enemyDmg}");

                turn++;
            }

       
            int winnerId = playerHP > 0 ? player.Id : enemy.Id;
            UpdateBattle(battleId, winnerId);
            Console.WriteLine($"\n Победил: {(playerHP > 0 ? player.Name : enemy.Name)}!");
        }

      
        static int InsertBattle(int hero1Id, int hero2Id)
        {
            using (var conn = new SqlConnection(dbConn))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "INSERT INTO Battles (Hero1Id, Hero2Id, StartedAt) VALUES (@h1,@h2,GETDATE()); SELECT SCOPE_IDENTITY();", conn);
                cmd.Parameters.AddWithValue("@h1", hero1Id);
                cmd.Parameters.AddWithValue("@h2", hero2Id);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        static void UpdateBattle(int battleId, int winnerId)
        {
            using (var conn = new SqlConnection(dbConn))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "UPDATE Battles SET WinnerId=@w, EndedAt=GETDATE() WHERE BattleId=@b", conn);
                cmd.Parameters.AddWithValue("@w", winnerId);
                cmd.Parameters.AddWithValue("@b", battleId);
                cmd.ExecuteNonQuery();
            }
        }

        
    }

}

class Hero
    {
        public int Id;
        public string Name;
        public string Race;
        public int Level;
        public int Power;
        public int HP => Level * 10 + Power;
        public void Print()
        {
            Console.WriteLine($"Имя:{Name},раса:{Race},левел:{Level},сила:{Power}.");
        }

    }
    class Item
    {
        public string Name;
        public string Type;
        public int Quantity;
    }