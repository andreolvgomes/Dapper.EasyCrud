using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.EasyCrud
{
    public interface ITableNameResolver
    {
        string ResolveTableName(Type type);
    }
}
