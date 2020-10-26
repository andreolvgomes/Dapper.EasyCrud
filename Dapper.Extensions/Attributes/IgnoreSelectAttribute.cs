using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper
{
    /// <summary>
    /// Optional IgnoreSelect attribute.
    /// Custom for DapperExtensions to exclude a property from Select methods
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreSelectAttribute : Attribute
    {
    }
}
