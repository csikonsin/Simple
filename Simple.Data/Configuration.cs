using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Data
{
    public static class Configuration
    {
        public static void Initialize()
        {
            DapperExtensions.DapperExtensions.DefaultMapper = typeof(CustomPluralizedMapper<>);
        }
    }

    public class CustomPluralizedMapper<T> : PluralizedAutoClassMapper<T> where T : class
    {
        public override void Table(string tableName)
        {
            //if (tableName.Equals("Person", StringComparison.CurrentCultureIgnoreCase))
            //{
            //    TableName = "Persons";
            //}

            base.Table(tableName);
        }
    }
}
