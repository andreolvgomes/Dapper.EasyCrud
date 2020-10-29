using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.EasyCrudTests
{
    public class Settings
    {
        public static string SqlServer(string database = "DapperSimpleCrudTestDb")
        {
            //DapperSimpleCrudTestDb
            return $@"Data Source=.\sqlexpress;Initial Catalog={database};Integrated Security=True;MultipleActiveResultSets=true;";
        }

        internal static string MySql(string database = "testdb")
        {
            return String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", "localhost", "3306", "root", "123456", database);
        }
    }
}