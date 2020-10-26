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
    }
}