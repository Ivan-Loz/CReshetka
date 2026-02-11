using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Shablon
{
    class Program
    {
        static List<string> Questions =
            new List<string>
            {
                "Для чего нужен 'cout' в C++ ? (1. Для получения данных от пользователя,2.Для создания рекурсии,3. Для вывода текста или переменных в консоль",
                "Что бы перенести строку в конце, нужно? (1. написать cin>>cout<<'Привет'2.Он сам перенесёи строку 3.Нужно написать cout<<'Привет'<<endl )",
                "Что происходит на примере 'cout<<Hello<<endl (1.Вывод строки с переносом в конце 2.Создание переменной 3.Создание класса)"
            };
        static string masterConn =
             @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True";

        static string connString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GameC;Integrated Security=True";
        static void CreateDatabase()
        {
            string masterConn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True";
            string createDbSql = "IF DB_ID('GameC') IS NULL CREATE DATABASE GameC;";
            using (var conn = new SqlConnection(masterConn))
            using (var cmd = new SqlCommand(createDbSql, conn))
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            Console.WriteLine("База данных GameC проверена/создана.");
        }
        static void CreateTables()
        {
            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandText = @" 
        IF OBJECT_ID('CorrectAnswers') IS NULL 
        CREATE TABLE CorrectAnswers ( 
            Id INT PRIMARY KEY IDENTITY(1,1), 
           Answers NVARCHAR(90));  
        IF OBJECT_ID('PlayerResponses') IS NULL
        CREATE TABLE PlayerResponses(
            Id INT PRIMARY KEY IDENTITY(1, 1),
            Answers NVARCHAR(90),
            AnswerCorrect NVARCHAR(50),
            Ball INT
                 );

                IF OBJECT_ID('Leaders') IS NULL
        CREATE TABLE Leaders(
           Name NVARCHAR(90) ,
           Balls INT
            )";

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
        IF NOT EXISTS (SELECT * FROM CorrectAnswers) 
        BEGIN 
            INSERT INTO CorrectAnswers (Answers) VALUES 
            ('1'), 
            ('3'), 
            ('1'); END";
                cmd.ExecuteNonQuery();
            }
            Console.WriteLine("Тестовые данные добавлены (если их не было).");
        }
        static void Quests()
        {
            foreach (string questions in Questions)
            {
                Console.WriteLine(questions);
                Console.Write("Ответ: "); var f = Console.ReadLine();
                string sql = "INSERT INTO PlayerResponses (Answers) VALUES  (@Answers)";
                using (var conn = new SqlConnection(connString))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Answers", f);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                //Закончил здесь на проверке ответов 
            }
        } 
        static void Main(string[] args)
        {
            CreateDatabase();
            CreateTables();     
            SeedData();
            Quests();
            Console.Read();
        }
    }
}