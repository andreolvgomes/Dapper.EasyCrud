using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Dapper.EasyCrud;
using Dapper.EasyCrudTests.TestsInfra;

namespace Dapper.EasyCrudTests
{
    class Program
    {
        static void Main()
        {
            new SqlServerTest().RunTests();
            new SqlLiteTest().RunTests();
            //new MySqlTest().RunTests();
            //new PostgresTest().RunTests();
        }
    }
}