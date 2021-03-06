﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Dapper.EasyCrud
{
    public class ColumnNameResolver : IColumnNameResolver
    {
        public virtual string ResolveColumnName(PropertyInfo propertyInfo)
        {
            var columnName = DapperEasyCrud.Encapsulate(propertyInfo.Name);

            var columnattr = propertyInfo.GetCustomAttributes(true).SingleOrDefault(attr => attr.GetType().Name == typeof(ColumnAttribute).Name) as dynamic;
            if (columnattr != null)
            {
                columnName = DapperEasyCrud.Encapsulate(columnattr.Name);
                if (Debugger.IsAttached)
                    Trace.WriteLine(String.Format("Column name for type overridden from {0} to {1}", propertyInfo.Name, columnName));
            }
            return columnName;
        }
    }
}
