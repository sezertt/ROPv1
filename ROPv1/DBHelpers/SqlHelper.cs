using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROPv1.DBHelpers
{
    public class SqlHelper
    {
        //protected enum CommandMethod
        //{
        //    ExecuteReader = 0,
        //    ExecuteNonQuery = 1,
        //    ExecuteScalar = 2
        //}
        private Collection<SqlParameter> Parameters = new Collection<SqlParameter>();

        public void AddParameter(SqlParameter param)
        {
            this.Parameters.Add(param);
        }

        public void AddParameter(string paramName, object paramValue)
        {
            SqlParameter item = new SqlParameter();
            item.ParameterName = paramName;
            item.Value = this.ValueOrDbNull(paramValue);
            this.Parameters.Add(item);
        }

        public void AddParameter(string paramName, SqlDbType paramDbType, object paramValue)
        {
            SqlParameter item = new SqlParameter();
            item.ParameterName = paramName;
            item.SqlDbType = paramDbType;
            item.Value = this.ValueOrDbNull(paramValue);
            this.Parameters.Add(item);
        }

        public void ClearParameter()
        {
            this.Parameters.Clear();
        }

        public static SqlConnection GenerateConnection(string connStringKey)
        {
            if (string.IsNullOrWhiteSpace(connStringKey))
                return new SqlConnection(ConnStringHelper.SqlConnString);
            else
                return new SqlConnection(ConnStringHelper.SqlConnStringCustom(connStringKey));
        }

        public static SqlConnection GenerateConnection()
        {
            return new SqlConnection(ConnStringHelper.SqlConnString);
        }

        public static string ConnectionString
        {
            get
            {
                return ConnStringHelper.SqlConnString;
            }
        }

        public static SqlTransaction GenerateTransaction(SqlConnection connection)
        {
            if (connection != null && connection.State != ConnectionState.Open)
                connection.Open();

            return connection.BeginTransaction();
        }

        public static SqlTransaction GenerateTransaction(SqlConnection connection, IsolationLevel isoLevel)
        {
            if (connection != null && connection.State != ConnectionState.Open)
                connection.Open();

            return connection.BeginTransaction(isoLevel);
        }

        private void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trns, string cmdText)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trns != null)
            {
                cmd.Transaction = trns;
            }
            if (this.Parameters != null && this.Parameters.Count > 0)
            {
                foreach (SqlParameter parameter in this.Parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
            try
            {
                //LogDbOperation(ref cmd, ref trns);
            }
            catch (Exception)
            {
            }
        }

        protected object ValueOrDbNull(object paramvalue)
        {
            object objectValue = DBNull.Value;
            if (paramvalue == null || paramvalue == DBNull.Value)
            {
                return objectValue;
            }
            if (!paramvalue.GetType().IsValueType)
            {
                if (paramvalue.GetType() == typeof(string))
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(paramvalue)))
                    {
                        objectValue = paramvalue;
                    }
                }
                else
                {
                    if (paramvalue.GetType().BaseType == typeof(Array) && ((Array)paramvalue).Length > 0)
                    {
                        objectValue = paramvalue;
                    }
                    else if (paramvalue.GetType().BaseType == typeof(Array) && ((Array)paramvalue).Length == 0)
                    {
                        if (paramvalue.GetType().HasElementType)
                        {
                            //aşağıdaki type dan SQlDbType a çevirmek için bir hal çaresine bak
                            Type type = paramvalue.GetType().GetElementType();
                        }
                    }
                    else
                    {
                        throw new Exception("SqlHelper' da ele alınmamış veri tipine rastlandı. Gelen veri tipi:" + paramvalue.GetType().ToString() + ", temel veri tipi:" + paramvalue.GetType().BaseType.ToString());
                    }
                }
            }
            else
            {
                objectValue = paramvalue;
            }

            return objectValue;
        }


        public int ExecuteInsertQueryAndGetIdentity(string cmdText, SqlTransaction trns)
        {
            object objectValue = null;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmdText = cmdText + "; Select SCOPE_IDENTITY() as 'Identity';";

            if (trns == null)
            {
                using (SqlConnection connection = GenerateConnection())
                {
                    SqlTransaction transaction = null;
                    this.PrepareCommand(cmd, connection, transaction, cmdText);
                    objectValue = cmd.ExecuteScalar();
                }
            }
            else
            {
                this.PrepareCommand(cmd, trns.Connection, trns, cmdText);
                objectValue = cmd.ExecuteScalar();
            }
            this.ClearParameter();

            if (objectValue == null || objectValue == DBNull.Value)
            {
                throw new Exception("Kayıt işlemi sonucunda Id alınamadı");
            }

            return Convert.ToInt32(objectValue);

        }


        public int ExecuteNonQuery(string cmdText, SqlTransaction trns = null)
        {
            return this.ExecuteNonQueryBase(CommandType.Text, cmdText, trns);
        }

        protected int ExecuteNonQueryBase(CommandType cmdType, string cmdText, SqlTransaction trns)
        {
            int num2 = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = cmdType;
            if (trns == null)
            {
                using (SqlConnection connection = SQLBaglantisi.getConnection())
                {
                    SqlTransaction transaction = null;
                    this.PrepareCommand(cmd, connection, transaction, cmdText);
                    num2 = cmd.ExecuteNonQuery();
                }
            }
            else
            {
                this.PrepareCommand(cmd, trns.Connection, trns, cmdText);
                num2 = cmd.ExecuteNonQuery();
                try { trns.Commit(); }
                catch { trns.Rollback(); }
            }

            this.ClearParameter();
            return num2;
        }

        public int ExecuteNonQueryProcedure(string cmdText, SqlTransaction trns)
        {
            return this.ExecuteNonQueryBase(CommandType.StoredProcedure, cmdText, trns);
        }

        public SqlDataReader ExecuteReader(string cmdText)
        {
            return this.ExecuteReaderBase(CommandType.Text, cmdText, null, null);
        }

        public SqlDataReader ExecuteReader(string cmdText, SqlTransaction trns)
        {
            //if (trns == null)
            //{ throw new NullReferenceException("Alınan SqlTransaction parametresi NULL olamaz."); }

            return this.ExecuteReaderBase(CommandType.Text, cmdText, null, trns);
        }

        public SqlDataReader ExecuteReader(string cmdText, SqlConnection conn)
        {
            //if (conn == null)
            //{ throw new NullReferenceException("Alınan SqlConnection parametresi NULL olamaz."); }

            return this.ExecuteReaderBase(CommandType.Text, cmdText, conn, null);
        }

        //public SqlDataReader ExecuteReader2(string cmdText, SqlConnection conn)
        //{
        //    return this.ExecuteReaderBase2(CommandType.Text, cmdText, conn);
        //}

        protected SqlDataReader ExecuteReaderBase(CommandType cmdType, string cmdText, SqlConnection conn, SqlTransaction trns)
        {
            SqlDataReader reader2 = null;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = cmdType;


            if (trns != null)//transaction verildiyse command bu transaction içerisinden çalışıştırılıyor
            {
                this.PrepareCommand(cmd, trns.Connection, trns, cmdText);
                reader2 = cmd.ExecuteReader();
            }
            else//transaction verilMEdiyse
            {
                if (conn != null)//connection verildiyse command bu connection içerisinden çalışıştırılıyor.
                {
                    //NOT: verilen conn kapalıysa bile PrepareCommand bu connection açar
                    this.PrepareCommand(cmd, conn, null, cmdText);
                    reader2 = cmd.ExecuteReader();
                }
                else//connection da verilMEdiyse 
                {
                    try
                    {//kullanacağımız connection nesnesini kendimiz açıyoruz.
                        conn = GenerateConnection();
                        this.PrepareCommand(cmd, conn, null, cmdText);
                        reader2 = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    }
                    catch (Exception exc)
                    {
                        if (conn != null && conn.State != ConnectionState.Closed)
                        {
                            conn.Close();
                        }
                        throw exc;
                    }
                }
            }

            this.ClearParameter();
            return reader2;
        }

        public SqlDataReader ExecuteReaderProcedure(string cmdText, SqlTransaction trns)
        {
            return this.ExecuteReaderBase(CommandType.StoredProcedure, cmdText, null, trns);
        }


        public object ExecuteScalar(string cmdText, SqlTransaction trns)
        {
            return this.ExecuteScalarBase(CommandType.Text, cmdText, trns);
        }

        protected object ExecuteScalarBase(CommandType cmdType, string cmdText, SqlTransaction trns)
        {
            object objectValue = null;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = cmdType;
            if (trns == null)
            {
                using (SqlConnection connection = GenerateConnection())
                {
                    SqlTransaction transaction = null;
                    this.PrepareCommand(cmd, connection, transaction, cmdText);
                    objectValue = cmd.ExecuteScalar();
                }
            }
            else
            {
                this.PrepareCommand(cmd, trns.Connection, trns, cmdText);
                objectValue = cmd.ExecuteScalar();
            }
            this.ClearParameter();
            return objectValue;
        }

        public object ExecuteScalarProcedure(string cmdText, SqlTransaction trns)
        {
            return this.ExecuteScalarBase(CommandType.StoredProcedure, cmdText, trns);
        }


        //public void FillDataset(ref DataSet dsResult, string cmdText, SqlTransaction trns, int StartRecord, int MaxRecord, string srcTable)
        //{
        //    this.FillDatasetBase(CommandType.Text, ref dsResult, cmdText, trns, StartRecord, MaxRecord, "");
        //}

        //protected void FillDatasetBase(CommandType cmdType, ref DataSet dsResult, string cmdText, SqlTransaction trns, int StartRecord, int MaxRecord, string srcTable)
        //{
        //    if (dsResult == null)
        //    {
        //        dsResult = new DataSet();
        //    }
        //    SqlDataAdapter adapter = new SqlDataAdapter();
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandType = cmdType;
        //    if (trns == null)
        //    {
        //        using (SqlConnection connection = GenerateConnection())
        //        {
        //            SqlTransaction transaction = null;
        //            this.PrepareCommand(cmd, connection, transaction, cmdText);
        //            adapter.SelectCommand = cmd;
        //            if (StartRecord > -1 && MaxRecord > -1)
        //            {
        //                adapter.Fill(dsResult, StartRecord, MaxRecord, srcTable);
        //            }
        //            else
        //            {
        //                adapter.Fill(dsResult);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        this.PrepareCommand(cmd, trns.Connection, trns, cmdText);
        //        adapter.SelectCommand = cmd;
        //        if (StartRecord > -1 && MaxRecord > -1)
        //        {
        //            adapter.Fill(dsResult, StartRecord, MaxRecord, srcTable);
        //        }
        //        else
        //        {
        //            adapter.Fill(dsResult);
        //        }
        //    }
        //    this.ClearParameter();
        //}

        //public void FillDatasetProcedure(ref DataSet dsResult, string cmdText, SqlTransaction trns, int StartRecord, int MaxRecord, string srcTable)
        //{
        //    this.FillDatasetBase(CommandType.StoredProcedure, ref dsResult, cmdText, trns, StartRecord, MaxRecord, "");
        //}

        public DataTable FillDataTable(string cmdText, SqlTransaction trns = null)
        {
            return this.FillDataTableBase(CommandType.Text, cmdText, trns);
        }


        protected DataTable FillDataTableBase(CommandType cmdType, string cmdText, SqlTransaction trns)
        {
            DataTable dtResult = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = cmdType;
            if (trns == null)
            {
                using (SqlConnection connection = GenerateConnection())
                {
                    SqlTransaction transaction = null;
                    this.PrepareCommand(cmd, connection, transaction, cmdText);
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dtResult);
                }
            }
            else
            {
                this.PrepareCommand(cmd, trns.Connection, trns, cmdText);
                adapter.SelectCommand = cmd;
                adapter.Fill(dtResult);
            }
            this.ClearParameter();

            return dtResult;
        }

        public DataTable FillDataTableProcedure(string cmdText, SqlTransaction trns)
        {
            return this.FillDataTableBase(CommandType.StoredProcedure, cmdText, trns);
        }


    }
}

