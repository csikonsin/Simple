using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Simple.Data
{
    public class Connection
    {
        public static SqlConnection GetSql()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data"));

            var connectionString = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
            return new System.Data.SqlClient.SqlConnection(connectionString);
        }
    }
}