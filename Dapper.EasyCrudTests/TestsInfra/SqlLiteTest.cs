﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using Dapper.EasyCrud;
using Dapper.EasyCrudTests.TestsInfra.Interfaces;

namespace Dapper.EasyCrudTests.TestsInfra
{
    public class SqlLiteTest : StartupDatabase
    {
        public void RunTests()
        {
            Setup();
            var stopwatch = Stopwatch.StartNew();
            var pgtester = new CommandsQueriesTests(Dialect.SQLite);
            foreach (var method in typeof(CommandsQueriesTests).GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                //skip schema tests
                if (method.Name.Contains("Schema")) continue;
                var testwatch = Stopwatch.StartNew();
                Console.Write("Running " + method.Name + " in SQLite");
                method.Invoke(pgtester, null);
                Console.WriteLine(" - OK! {0}ms", testwatch.ElapsedMilliseconds);
            }
            stopwatch.Stop();
            Console.Write("\n\nSQLite testing complete.");
            Console.WriteLine("\nTime elapsed: {0}", stopwatch.Elapsed);            
            //Console.ReadKey();
        }

        public void Setup()
        {
            File.Delete(Directory.GetCurrentDirectory() + "\\MyDatabase.sqlite");
            SQLiteConnection.CreateFile("MyDatabase.sqlite");
            var connection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            using (connection)
            {
                connection.Open();
                connection.Execute(@" create table Users (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name nvarchar(100) not null, Age int not null, ScheduledDayOff int null, CreatedDate datetime default current_timestamp ) ");                
                connection.Execute(@" create table Car (CarId INTEGER PRIMARY KEY AUTOINCREMENT, Id INTEGER null, Make nvarchar(100) not null, Model nvarchar(100) not null) ");
                connection.Execute(@" create table BigCar (CarId INTEGER PRIMARY KEY AUTOINCREMENT, Make nvarchar(100) not null, Model nvarchar(100) not null) ");
                connection.Execute(@" insert into BigCar (CarId,Make,Model) Values (2147483649,'car','car') ");
                connection.Execute(@" create table City (Name nvarchar(100) not null, Population int not null) ");
                connection.Execute(@" CREATE TABLE GUIDTest([Id] [uniqueidentifier] NOT NULL,[name] [varchar](50) NOT NULL, CONSTRAINT [PK_GUIDTest] PRIMARY KEY  ([Id] ASC))");
                connection.Execute(@" create table StrangeColumnNames (ItemId INTEGER PRIMARY KEY AUTOINCREMENT, word nvarchar(100) not null, colstringstrangeword nvarchar(100) not null, KeywordedProperty nvarchar(100) null) ");
                connection.Execute(@" create table UserWithoutAutoIdentity (Id INTEGER PRIMARY KEY, Name nvarchar(100) not null, Age int not null) ");
                connection.Execute(@" create table IgnoreColumns (Id INTEGER PRIMARY KEY AUTOINCREMENT, IgnoreInsert nvarchar(100) null, IgnoreUpdate nvarchar(100) null, IgnoreSelect nvarchar(100)  null, IgnoreAll nvarchar(100) null) ");
                connection.Execute(@" CREATE TABLE KeyMaster (Key1 INTEGER NOT NULL, Key2 INTEGER NOT NULL, PRIMARY KEY ([Key1], [Key2]))");
                connection.Execute(@" CREATE TABLE stringtest (stringkey nvarchar(50) NOT NULL,name nvarchar(50) NOT NULL, PRIMARY KEY ([stringkey] ASC))");

                connection.Execute(@" create table DateTimeAt (Id INTEGER PRIMARY KEY AUTOINCREMENT, CreateAt datetime, UpdateAt datetime)");
                connection.Execute(@" create table GuidEmpty (Id INTEGER PRIMARY KEY AUTOINCREMENT, Identifier [uniqueidentifier] NOT NULL)");
            }
        }
    }
}