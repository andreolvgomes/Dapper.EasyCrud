using System;
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

            //PostgreSQL tests assume port 5432 with username postgres and password postgrespass
            //they are commented out by default since postgres setup is required to run tests
            //SetupPg(); 
            //RunTestsPg();   
        }
    }
}