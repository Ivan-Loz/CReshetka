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
        static void Main(string[] args)
        {
            CreateDatabase();
            CreateTables();
        }
    }
}
