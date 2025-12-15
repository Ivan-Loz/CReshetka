using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace pz3
{
    class Program
    {

        static string masterConn =
             @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True";

        static string connString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FantasyDB;Integrated Security=True";
        static void CreateDatabase()
        {
            string masterConn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True";
            string createDbSql = "IF DB_ID('FantasyDB') IS NULL CREATE DATABASE FantasyDB;";
            using (var conn = new SqlConnection(masterConn))
            using (var cmd = new SqlCommand(createDbSql, conn))
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            Console.WriteLine("База данных FantasuDB проверена/создана.");
        }
        static void CreateTables()
        {
            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandText = @" 
        IF OBJECT_ID('Hero') IS NULL 
        CREATE TABLE Hero ( 
            Id INT PRIMARY KEY IDENTITY(1,1), 
           Name NVARCHAR(50), 
            Race NVARCHAR(30), 
            Leve INT, 
            Power INT  );  
        IF OBJECT_ID('Items') IS NULL
        CREATE TABLE Items(
            Id INT PRIMARY KEY IDENTITY(1, 1),
            ItemName NVARCHAR(50),
            Type NVARCHAR(30)
                 );

                IF OBJECT_ID('HeroItems') IS NULL
        CREATE TABLE HeroItems(
           HeroId INT FOREIGN KEY REFERENCES Hero(Id),
            ItemId INT FOREIGN KEY REFERENCES Items(Id),
            Quantity INT)";

                cmd.ExecuteNonQuery();
                Console.WriteLine("Таблицы проверены/созданы.");
            }
        }

        static void SeedData()
        {
            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();

                cmd.CommandText = @" 
        IF NOT EXISTS (SELECT * FROM Hero) 
        BEGIN 
            INSERT INTO Hero (Name, Race, Leve, Power) VALUES 
            ('rere', 'Huma', 1, '10'), 
            ('Dimon', 'Orc', 25, '30'), 
            ('Mari', 'Elf',30, '35'); END"; 
                cmd.ExecuteNonQuery();
            }
            Console.WriteLine("Тестовые данные добавлены (если их не было).");
        }

        static void ShowHero()
        {
            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand("SELECT * FROM Hero ORDER BY Id", conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\nГерои:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Id"]}. {reader["Name"]}, {reader["Race"]} —  { reader["Leve"]} уровень, сила { reader["Power"]}");
                    }
                }
            }
        }
        static void AddHero()
        {
            Console.Write("Имя: "); var f = Console.ReadLine();
            Console.Write("Раса: "); var l = Console.ReadLine();
            Console.Write("Левл: "); var a = int.Parse(Console.ReadLine());
            Console.Write("Сила: "); var g = int.Parse(Console.ReadLine());

            string sql = "INSERT INTO Hero (Name, Race, Leve, Power) VALUES  (@name, @race, @leve, @power)";
            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@name", f);
                cmd.Parameters.AddWithValue("@race", l);
                cmd.Parameters.AddWithValue("@leve", a);
                cmd.Parameters.AddWithValue("@power", g);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            Console.WriteLine("Герой добавлен.");
        }
      static void LevelUpHero()
      {
            Console.Write("Айди: "); var id = Console.ReadLine();

            string sql = "UPDATE Hero SET Leve = Leve + 1, Power = Power + 5 WHERE Id = @id";
            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            Console.WriteLine("Левел повишен.");
        }
        static void DeleteHero()
        {
            Console.Write("ID героя для удаления: ");
            int id = int.Parse(Console.ReadLine());
            string sqli = "DELETE FROM HeroItems WHERE HeroId=@Hid";
            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand(sqli, conn))
            {
                cmd.Parameters.AddWithValue("@Hid", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            Console.WriteLine("предметы удалён.");

            
            string sql = "DELETE FROM Hero WHERE Id=@id";
            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            Console.WriteLine("Герой удалён.");
        }

        static void SeedItems()
        {
            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();

                cmd.CommandText = @" 
        IF NOT EXISTS (SELECT * FROM Items) 
        BEGIN 
            INSERT INTO Items (ItemName, Type) VALUES 
            ('Sword', 'Weapon'), 
            ('Shield', 'Armor'), 
            ('Ring of Power', 'Artifact'); END";
                cmd.ExecuteNonQuery();
            }
            Console.WriteLine("Тестовые данные добавлены (если их не было).");
        }
        static void pHeroItem()
        {
            using (var conn = new SqlConnection(connString))
            {

                conn.Open();
                string seedHeroItem = @" 
        IF NOT EXISTS (SELECT * FROM HeroItems) 
        BEGIN  
            INSERT INTO HeroItems(HeroId,ItemId,Quantity) 
             VALUES (1,1,1), (2,2,1),(3,3,1); END";
                new SqlCommand(seedHeroItem, conn).ExecuteNonQuery();
            }
            Console.WriteLine("Тестовые данные добавлены (если их не было).");
        }
       static void GiveItemToHero()
       {
            
            int heroId;
            int itemId; 
            int qty;
            Console.Write("ID Hero: "); heroId = int.Parse(Console.ReadLine());
            Console.Write("ID Items: "); itemId = int.Parse(Console.ReadLine());
            Console.Write("Quantity: "); qty = int.Parse(Console.ReadLine());
            

            string sql = "INSERT INTO HeroItems (HeroId, ItemId, Quantity) VALUES (@h, @i, @q)";
            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand(sql, conn))
            {

                cmd.Parameters.AddWithValue("@h", heroId);
                cmd.Parameters.AddWithValue("@i", itemId);
                cmd.Parameters.AddWithValue("@q", qty);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            Console.WriteLine("предмет добавлен.");

       }
      
        static void ShowHeroItems()
        {
            int IdHero;
            Console.Write("ID Hero: "); IdHero = int.Parse(Console.ReadLine());
            string sql = "SELECT h.Name, i.ItemName, hi.Quantity FROM HeroItems hi JOIN Hero h ON hi.HeroId = h.Id JOIN Items i ON hi.ItemId = i.Id WHERE h.Id = @id";

            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", IdHero);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("У героев нет предметов или не верный айди");
                    }
                    string heroName = "";
                    Console.WriteLine("\nПредмет героя:");
                    while (reader.Read())
                    {
                        heroName = reader["Name"].ToString();
                        Console.WriteLine($"{reader["ItemName"]}. {reader["Quantity"]}, шт.");
                    }
                    Console.WriteLine($"Имя героя: {heroName}");
                }
            }
        }
        static void ShowRaceStats()
        {
            string sql = " SELECT Race, COUNT(*) AS Count FROM Hero GROUP BY Race;";
            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    Console.WriteLine("\nКоличество героев по расам:");
                    while (r.Read())
                        Console.WriteLine($"{r["Race"]}: {r["Count"]}");
                }
            }
        }

        static void Main()
        {
            CreateTables();
            CreateDatabase();
            SeedData();
            SeedItems();
            pHeroItem();

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            EnsureDatabaseAndTables();
            while (true)
            {
                Console.WriteLine("\n1. Показать Героев");
                Console.WriteLine("2. Добавить Героев");
                Console.WriteLine("3. Уровень ап");
                Console.WriteLine("4. Удалить героя");
                Console.WriteLine("5. Дать предмет герою");
                Console.WriteLine("6. Количество по рассе");
                Console.WriteLine("7. количество предметов героя");
                Console.WriteLine("8. Выход");
                Console.Write("Выберите пункт: ");

                switch (Console.ReadLine())
                {
                    case "1": ShowHero(); break;
                    case "2": AddHero(); break;
                    case "3": LevelUpHero(); break;
                    case "4": DeleteHero(); break;
                    case "5": GiveItemToHero(); break;
                    case "6": ShowRaceStats(); break;
                    case "7":ShowHeroItems(); break;
                    case "8": return;
                    default: Console.WriteLine("Ошибка ввода."); break;
                }
            }
        }

        static void EnsureDatabaseAndTables()
        {
            using (var conn = new SqlConnection(masterConn))
            {
                conn.Open();
                string createDb = "IF DB_ID('FantasyDB') IS NULL CREATE DATABASE FantasyDB;";
                new SqlCommand(createDb, conn).ExecuteNonQuery();
            }
        }
    }
}