using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace WebApplication.Models.DAO
{
    public class ConfigDB
    {
        private static SqlConnection conn = null;

        public static SqlConnection getConnect()
        {
            if (conn == null)
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["testConnectionString"].ConnectionString);
            }
            return conn;
        }
    }
}