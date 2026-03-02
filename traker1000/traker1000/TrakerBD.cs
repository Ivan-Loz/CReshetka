using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace traker1000
{
    class TrakerBD
    {
        static string masterConn =
             @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True";

        static string connString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TrackerDB;Integrated Security=True";

       public static void CreateDatabase() //Метод для создания Базы данных :3
        {
            string masterConn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True";
            string createDbSql = "IF DB_ID('TrackerDB') IS NULL CREATE DATABASE TrackerDB;";
            using (var conn = new SqlConnection(masterConn))
            using (var cmd = new SqlCommand(createDbSql, conn))
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public static  void CreateTables() //Метод для создания табличек :3
        {
            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandText = @" 
        IF OBJECT_ID('Users') IS NULL 
        CREATE TABLE Users ( 
            Id INT PRIMARY KEY IDENTITY(1,1), 
           UserName NVARCHAR(90),
            Connect NVARCHAR(90),
            Time TIME,
            Date DATETIME
            
                );  

        IF OBJECT_ID('TrackerData') IS NULL
        CREATE TABLE TrackerData(
            NameApplication NVARCHAR(120),
            TimeUse TIME,
            LaunchDate DATETIME,
            ClosingDate DATETIME
                 )";

                cmd.ExecuteNonQuery();
            }
        }
    }
}
