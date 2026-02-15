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
                "Что происходит на примере 'cout<<Hello<<endl (1.Вывод строки с переносом в конце 2.Создание переменной 3.Создание класса)",
                "Для чего нужен cin в C++ (1. Для создания класса 2. Для записи данных от пользователя 3. Для вывода строк",
                "Запись 'cin>>A;' делает? (1. Запись в переменную 'A'2. Создаёт обьект сласса 'A' 3.Я не знаю блин)",
                "Что такое 'if'? (1. Создание переменной 2. Создание функции 3.Логическая операция)",
                "Как понять запись 'if A < 4' (1. Если A < 4, то... 2. Функция принимает А < 4 3.Не знаю)",
                "Что такое for? (1. Напиток 2. Наследование классов 3. Цикл)",
                "Что обозначает запись 'for(int i;i <= A;++)' (1.Создание метода 2. Условие цикла 3. Класс i наследуется от класса A)"
            };
        static List<string> Ans = new List<string>();
        static List<string> CorrAns = new List<string>();
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
            AnswerCorrect NVARCHAR(90),
            Ball INT
                 );

                IF OBJECT_ID('Leaders') IS NULL
        CREATE TABLE Leaders(
           Id INT PRIMARY KEY IDENTITY(1,1),
           Name NVARCHAR(90) ,
           Balls INT
            )";

                cmd.ExecuteNonQuery();
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
            ('3'), 
            ('3'), 
            ('1'),
            ('2'),
            ('1'),
            ('3'),
            ('1'),
            ('3'),
            ('2');END";
                cmd.ExecuteNonQuery();
            }
        }
        static void Quests() 
        {
            foreach (string questions in Questions)
            {
                Console.WriteLine("==="+questions +"===\n");
                Console.Write($"Ответ:") ; var f = Console.ReadLine(); 
                Console.WriteLine("\n");
                Ans.Add(f);
            }
        }
        static int YourPoints()
        {

            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand("SELECT Ball FROM PlayerResponses", conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    int points = 0;
                    int Endpoints = 0;
                   while (reader.Read())
                   {
                      points = int.Parse(reader["Ball"].ToString());
                        Endpoints += points;   
                   }
                    return Endpoints;

                }
            }
            
        }
        static void Examination()
        {
            
            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand("SELECT  Answers FROM  CorrectAnswers", conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                     while (reader.Read())
                     {
                        CorrAns.Add(reader["Answers"].ToString());
                     }

                }
            }
            int i = 0;
            foreach (string questions in CorrAns) 
            {
                if (Ans[i] == CorrAns[i])
                {
                    string sql = "INSERT INTO PlayerResponses(Answers,AnswerCorrect, Ball) VALUES(@answers,@answerCorrect, @ball)";
                    using (var conn = new SqlConnection(connString))
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@answers", Ans[i]);
                        cmd.Parameters.AddWithValue("@answerCorrect", "Верен");
                        cmd.Parameters.AddWithValue("@ball", 10);
                        
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        i++;
                        continue;
                    }
                }
                else
                {
                    string sql = "INSERT INTO PlayerResponses(Answers,AnswerCorrect, Ball) VALUES(@answers,@answerCorrect, @ball)";
                    using (var con = new SqlConnection(connString))
                    using (var cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@answers", Ans[i]);
                        cmd.Parameters.AddWithValue("@answerCorrect", "Не верен");
                        cmd.Parameters.AddWithValue("@ball", 0);
                        
                        con.Open();
                        cmd.ExecuteNonQuery();
                        i++;
                        continue;
                    }
                
                }
                
            }
         }
        static void Leaders(string Name)
        {
            string sql = "INSERT INTO Leaders(Name,Balls) VALUES(@name,@balls)";
            using (var con = new SqlConnection(connString))
            using (var cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@name", Name);
                cmd.Parameters.AddWithValue("@balls", YourPoints());
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        static void End()
        {
            Console.WriteLine("Ваши результаты:\n");
            Console.WriteLine($"Ваши баллы:{YourPoints()}\n");

            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand("SELECT * FROM PlayerResponses", conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Ваш ответ: {reader["Answers"]}, ответ был: {reader["AnswerCorrect"]}, баллов получено: {reader["Ball"]}\n");
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Введите своё имя!");
            string Name;
            Name = Console.ReadLine();
            CreateDatabase();
            CreateTables();     
            SeedData();
            Quests();
            Examination();
            Leaders(Name);
            Console.WriteLine($"Игрок: {Name}");
            End();
            Console.Read();
        }
    }
}