using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.EasyCrudTests.TestsInfra.Interfaces
{
    public interface StartupDatabase
    {
        void Setup();
        void RunTests();
    }
}