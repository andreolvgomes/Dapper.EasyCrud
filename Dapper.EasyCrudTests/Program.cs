using System;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Dapper.EasyCrud;
using Dapper.EasyCrudTests.TestsInfra;
using MySql.Data.MySqlClient;
using Npgsql;

namespace Dapper.EasyCrudTests
{
    class Program
    {
        static void Main()
        {
            new SqlServer().RunTests();
            new SqlLite().RunTests();

            //PostgreSQL tests assume port 5432 with username postgres and password postgrespass
            //they are commented out by default since postgres setup is required to run tests
            //SetupPg(); 
            //RunTestsPg();   

            //MySQL tests assume port 3306 with username admin and password admin
            //they are commented out by default since mysql setup is required to run tests
            //SetupMySQL();
            //RunTestsMySQL();
        }
    }
}