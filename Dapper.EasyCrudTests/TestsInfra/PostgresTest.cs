using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using Dapper.EasyCrud;
using Dapper.EasyCrudTests.TestsInfra.Interfaces;
using Npgsql;

namespace Dapper.EasyCrudTests.TestsInfra
{
    public class PostgresTest : StartupDatabase
    {
        public void RunTests()
        {
            Setup();

            var stopwatch = Stopwatch.StartNew();
            var pgtester = new CommandsQueriesTests(Dialect.PostgreSQL);
            foreach (var method in typeof(CommandsQueriesTests).GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                var testwatch = Stopwatch.StartNew();
                Console.Write("Running " + method.Name + " in PostgreSQL");
                method.Invoke(pgtester, null);
                Console.WriteLine(" - OK! {0}ms", testwatch.ElapsedMilliseconds);
            }

            stopwatch.Stop();
            Console.Write("PostgreSQL testing complete.");
            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);

            //Console.ReadKey();
        }

        public void Setup()
        {
            using (var connection = new NpgsqlConnection(String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", "localhost", "5432", "postgres", "postgrespass", "postgres")))
            {
                connection.Open();
                // drop  database 
                connection.Execute("DROP DATABASE IF EXISTS  testdb;");
                connection.Execute("CREATE DATABASE testdb  WITH OWNER = postgres ENCODING = 'UTF8' CONNECTION LIMIT = -1;");
            }
            System.Threading.Thread.Sleep(1000);

            using (var connection = new NpgsqlConnection(String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", "localhost", "5432", "postgres", "postgrespass", "testdb")))
            {
                connection.Open();
                connection.Execute(@" create table Users (Id SERIAL PRIMARY KEY, Name varchar not null, Age int not null, ScheduledDayOff int null, CreatedDate date not null default CURRENT_DATE) ");
                connection.Execute(@" create table Car (CarId SERIAL PRIMARY KEY, Id int null, Make varchar not null, Model varchar not null) ");
                connection.Execute(@" create table BigCar (CarId BIGSERIAL PRIMARY KEY, Make varchar not null, Model varchar not null) ");
                connection.Execute(@" alter sequence bigcar_carid_seq RESTART WITH 2147483650");
                connection.Execute(@" create table City (Name varchar not null, Population int not null) ");
                connection.Execute(@" CREATE SCHEMA Log; ");
                connection.Execute(@" create table Log.CarLog (Id SERIAL PRIMARY KEY, LogNotes varchar NOT NULL) ");
                connection.Execute(@" CREATE TABLE GUIDTest(Id uuid PRIMARY KEY,name varchar NOT NULL)");
                connection.Execute(@" create table StrangeColumnNames (ItemId Serial PRIMARY KEY, word varchar not null, colstringstrangeword varchar, keywordedproperty varchar) ");
                connection.Execute(@" create table UserWithoutAutoIdentity (Id int PRIMARY KEY, Name varchar not null, Age int not null) ");

            }
        }
    }
}
