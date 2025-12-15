using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace pzz
{
    class Program
    {
        static string masterConn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True";

        static string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FantasyDB;Integrated Security=True";

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
            Console.WriteLine("База данных StudentDB проверена/создана.");
        }
        static void EnsureDatabaseAndTables()
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
            Power INT  ); ";

                cmd.ExecuteNonQuery();
                Console.WriteLine("Таблицы проверены/созданы.");
            }
        }
        /*static void SeedData()
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
            ('Mari', 'Elf',30, '35'); 
           END";
                cmd.ExecuteNonQuery();
            }
            Console.WriteLine("Тестовые данные добавлены (если их не было).");
        }*/
        static void Main()
        {
            CreateDatabase();
            EnsureDatabaseAndTables();
            // SeedData();

            void EnsureDatabaseAndTables()
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
}
