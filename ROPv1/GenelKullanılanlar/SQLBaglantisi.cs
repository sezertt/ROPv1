using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROPv1
{
    public static class SQLBaglantisi
    {
        public static SqlConnection getConnection()
        {
            SqlConnection cnn = new SqlConnection("server=.;database=ropv1;integrated security=true");
            
            cnn.Open();
            return cnn;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public static SqlCommand getCommand (string query)
        {
            SqlCommand cmd = new SqlCommand(query,getConnection());
            return cmd;
        }
    }
}