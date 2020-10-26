using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper
{
    /// <summary>
    /// Optional IgnoreUpdate attribute.
    /// Custom for DapperExtensions to exclude a property from Update methods
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreUpdateAttribute : Attribute
    {
    }
}
