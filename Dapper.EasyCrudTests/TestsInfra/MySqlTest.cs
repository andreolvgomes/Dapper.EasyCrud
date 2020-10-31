using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using Dapper.EasyCrud;
using Dapper.EasyCrudTests.TestsInfra.Interfaces;
using MySql.Data.MySqlClient;

namespace Dapper.EasyCrudTests.TestsInfra
{
    public class MySqlTest : StartupDatabase
    {
        public void RunTests()
        {
            Setup();

            var stopwatch = Stopwatch.StartNew();
            var mysqltester = new CommandsQueriesTests(Dialect.MySQL);
            foreach (var method in typeof(CommandsQueriesTests).GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                //skip schema tests
                if (method.Name.Contains("Schema")) continue;
                if (method.Name.Contains("Guid")) continue;

                var testwatch = Stopwatch.StartNew();
                Console.Write("Running " + method.Name + " in MySQL");
                method.Invoke(mysqltester, null);
                Console.WriteLine(" - OK! {0}ms", testwatch.ElapsedMilliseconds);
            }
            stopwatch.Stop();
            Console.Write("\n\nMySQL testing complete.");
            Console.WriteLine("\nTime elapsed: {0}", stopwatch.Elapsed);
            
            //Console.ReadKey();
        }

        public void Setup()
        {
            using (var connection = new MySqlConnection(Settings.MySql("sys")))
            {
                connection.Open();
                // drop  database 
                connection.Execute("DROP DATABASE IF EXISTS testdb;");
                connection.Execute("CREATE DATABASE testdb;");
            }
            System.Threading.Thread.Sleep(1000);

            using (var connection = new MySqlConnection(Settings.MySql()))
            {
                connection.Open();
                connection.Execute(@" create table Users (Id INTEGER PRIMARY KEY AUTO_INCREMENT, Name nvarchar(100) not null, Age int not null, ScheduledDayOff int null, CreatedDate datetime default current_timestamp ) ");                
                connection.Execute(@" create table Car (CarId INTEGER PRIMARY KEY AUTO_INCREMENT, Id INTEGER null, Make nvarchar(100) not null, Model nvarchar(100) not null) ");
                connection.Execute(@" create table BigCar (CarId BIGINT PRIMARY KEY AUTO_INCREMENT, Make nvarchar(100) not null, Model nvarchar(100) not null) ");
                connection.Execute(@" insert into BigCar (CarId,Make,Model) Values (2147483649,'car','car') ");
                connection.Execute(@" create table City (Name nvarchar(100) not null, Population int not null) ");
                connection.Execute(@" CREATE TABLE GUIDTest(Id CHAR(36) NOT NULL,name varchar(50) NOT NULL, CONSTRAINT PK_GUIDTest PRIMARY KEY (Id ASC))");
                connection.Execute(@" create table StrangeColumnNames (ItemId INTEGER PRIMARY KEY AUTO_INCREMENT, word nvarchar(100) not null, colstringstrangeword nvarchar(100) not null, KeywordedProperty nvarchar(100) null) ");
                connection.Execute(@" create table UserWithoutAutoIdentity (Id INTEGER PRIMARY KEY, Name nvarchar(100) not null, Age int not null) ");
                connection.Execute(@" create table IgnoreColumns (Id INTEGER PRIMARY KEY AUTO_INCREMENT, IgnoreInsert nvarchar(100) null, IgnoreUpdate nvarchar(100) null, IgnoreSelect nvarchar(100)  null, IgnoreAll nvarchar(100) null) ");
                connection.Execute(@" create table KeyMaster (Key1 INTEGER NOT NULL, Key2 INTEGER NOT NULL, CONSTRAINT PK_KeyMaster PRIMARY KEY CLUSTERED (Key1 ASC, Key2 ASC))");
                connection.Execute(@" create table stringtest(stringkey varchar(50) NOT NULL,name varchar(50) NOT NULL, CONSTRAINT PK_stringkey PRIMARY KEY CLUSTERED (stringkey ASC))");

                connection.Execute(@" create table DateTimeAt (Id INTEGER PRIMARY KEY AUTO_INCREMENT, CreateAt datetime, UpdateAt datetime)");
                connection.Execute(@" create table GuidEmpty (Id INTEGER PRIMARY KEY AUTO_INCREMENT, Identifier CHAR(36) NOT NULL)");
            }
        }
    }
}
