using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROP.Business
{
    public class DataBaseSettings
    {
        private const string CONNECTION_STRING = "server=.;database=ropv1;integrated security=true";
        private static SqlConnection conn = null;
        public static SqlConnection GetConnection()
        {
            conn = new SqlConnection(CONNECTION_STRING);
            if (conn.State.Equals(ConnectionState.Closed))
            {
                conn.Open();
            }
            return conn;
        }
        public static void CloseConnection()
        {
            if (conn != null)
            {
                if (conn.State.Equals(ConnectionState.Open))
                {
                    conn.Close();
                }
            }
        }
    }
}
