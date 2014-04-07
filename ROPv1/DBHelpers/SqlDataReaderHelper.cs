using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROPv1.DBHelpers
{
        public class SqlDataReaderHelper : IDisposable
        {
            public void Dispose()
            {
                if (this.reader != null && this.reader.IsClosed == false)
                {
                    this.reader.Close();
                    this.reader.Dispose();

                }
            }

            // Fields
            private SortedList<string, int> FieldNameOrdinalPairs = new SortedList<string, int>();
            private SqlDataReader reader;

            // Methods
            public SqlDataReaderHelper(SqlDataReader SqlReader)
            {
                this.reader = SqlReader;
                this.FieldNameOrdinalPairs.Capacity = this.reader.FieldCount;
                int num2 = this.reader.FieldCount - 1;
                for (int i = 0; i <= num2; i++)
                {
                    this.FieldNameOrdinalPairs.Add(this.reader.GetName(i), i);
                }
            }

            public bool GetBoolean(string FieldName)
            {
                int i = this.FieldNameOrdinalPairs[FieldName];
                if (!this.reader.IsDBNull(i))
                {
                    return Convert.ToBoolean(this.reader.GetValue(i));
                }
                return false;
            }

            public byte GetByte(string FieldName)
            {
                int i = this.FieldNameOrdinalPairs[FieldName];
                if (!this.reader.IsDBNull(i))
                {
                    return this.reader.GetByte(i);
                }
                return 0;
            }

            public byte[] GetBytes(string FieldName)
            {
                int i = this.FieldNameOrdinalPairs[FieldName];
                if (!this.reader.IsDBNull(i))
                {
                    object objectValue = this.reader[i];
                    if (objectValue != null)
                    {
                        return (byte[])objectValue;
                    }
                    return null;
                }
                return null;
            }

            public byte[] GetBytes(string FieldName, long dataIndex, int bufferIndex, int length)
            {
                int i = this.FieldNameOrdinalPairs[FieldName];
                if (!this.reader.IsDBNull(i))
                {
                    byte[] buffer = null;
                    this.reader.GetBytes(i, dataIndex, buffer, bufferIndex, length);
                    return buffer;
                }
                return null;
            }

            public long GetChars(string FieldName, long dataIndex, char[] buffer, int bufferIndex, int length)
            {
                int i = this.FieldNameOrdinalPairs[FieldName];
                if (!this.reader.IsDBNull(i))
                {
                    return this.reader.GetChars(i, dataIndex, buffer, bufferIndex, length);
                }
                return 0L;
            }

            public DateTime GetDateTime(string FieldName)
            {
                int i = this.FieldNameOrdinalPairs[FieldName];
                if (!this.reader.IsDBNull(i))
                {
                    return this.reader.GetDateTime(i);
                }
                return DateTime.MinValue;
            }

            public decimal GetDecimal(string FieldName)
            {
                int i = this.FieldNameOrdinalPairs[FieldName];
                if (!this.reader.IsDBNull(i))
                {
                    return Convert.ToDecimal(this.reader.GetValue(i));
                }
                return decimal.Zero;
            }

            //public Single GetSingle(string FieldName)
            //{
            //    int i = this.FieldNameOrdinalPairs[FieldName];
            //    if (!this.reader.IsDBNull(i))
            //    {
            //        return this.reader.GetDouble(i);
            //    }
            //    return float.Parse("0");
            //}

            public Single GetSingle(string FieldName)
            {
                int i = this.FieldNameOrdinalPairs[FieldName];
                if (!this.reader.IsDBNull(i))
                {
                    return Convert.ToSingle(this.reader.GetValue(i));
                }
                return 0;
            }

            public double GetDouble(string FieldName)
            {
                int i = this.FieldNameOrdinalPairs[FieldName];
                if (!this.reader.IsDBNull(i))
                {
                    return Convert.ToDouble(this.reader.GetValue(i));
                }
                return 0.0;
            }

            public Guid GetGuid(string FieldName)
            {
                int i = this.FieldNameOrdinalPairs[FieldName];
                if (!this.reader.IsDBNull(i))
                {
                    return (Guid)this.reader.GetValue(i);
                }
                return new Guid();
            }

            public Int16 GetInt16(string FieldName)
            {
                int i = this.FieldNameOrdinalPairs[FieldName];
                if (!this.reader.IsDBNull(i))
                {
                    return Convert.ToInt16(this.reader.GetValue(i));
                }
                return 0;
            }

            public int GetInt32(string FieldName)
            {
                int i = this.FieldNameOrdinalPairs[FieldName];
                if (!this.reader.IsDBNull(i))
                {
                    return Convert.ToInt32(this.reader.GetValue(i));
                }
                return 0;
            }

            public Int64 GetInt64(string FieldName)
            {
                int i = this.FieldNameOrdinalPairs[FieldName];
                if (!this.reader.IsDBNull(i))
                {
                    return Convert.ToInt64(this.reader.GetValue(i));
                }
                return 0L;
            }

            public bool? GetNBoolean(string FieldName)
            {
                bool? result = new bool?();
                int i = this.FieldNameOrdinalPairs[FieldName];
                if (!this.reader.IsDBNull(i))
                {
                    result = this.reader.GetBoolean(i);
                }
                return result;
            }

            public byte? GetNByte(string FieldName)
            {
                byte? result = new byte?();
                int i = this.FieldNameOrdinalPairs[FieldName];
                if (!this.reader.IsDBNull(i))
                {
                    result = this.reader.GetByte(i);
                }
                return result;
            }

            public DateTime? GetNDateTime(string FieldName)
            {
                DateTime? result = new DateTime?();
                int i = this.FieldNameOrdinalPairs[FieldName];
                if (!this.reader.IsDBNull(i))
                {
                    result = this.reader.GetDateTime(i);
                }
                return result;
            }

            public decimal? GetNDecimal(string FieldName)
            {
                decimal? result = new decimal?();
                int i = this.FieldNameOrdinalPairs[FieldName];
                if (!this.reader.IsDBNull(i))
                {
                    result = Convert.ToDecimal(this.reader.GetValue(i));
                }
                return result;
            }

            public Single? GetNSingle(string FieldName)
            {
                float? result = new float?();
                int i = this.FieldNameOrdinalPairs[FieldName];
                if (!this.reader.IsDBNull(i))
                {
                    result = Convert.ToSingle(this.reader.GetValue(i));
                }
                return result;
            }

            public double? GetNDouble(ref double value, string FieldName)
            {
                double? result = new double?();
                int i = this.FieldNameOrdinalPairs[FieldName];
                if (!this.reader.IsDBNull(i))
                {
                    result = Convert.ToDouble(this.reader.GetValue(i));
                }
                return result;
            }

            public Guid? GetNGuid(string FieldName)
            {
                Guid? result = new Guid?();
                int i = this.FieldNameOrdinalPairs[FieldName];
                if (!this.reader.IsDBNull(i))
                {
                    result = (Guid)this.reader.GetValue(i);
                }
                return result;
            }

            public Int16? GetNInt16(string FieldName)
            {
                short? result = new short?();
                int i = this.FieldNameOrdinalPairs[FieldName];
                if (!this.reader.IsDBNull(i))
                {
                    result = Convert.ToInt16(this.reader.GetValue(i));
                }
                return result;
            }

            public int? GetNInt32(string FieldName)
            {
                int? result = new int?();
                int i = this.FieldNameOrdinalPairs[FieldName];
                if (!this.reader.IsDBNull(i))
                {
                    result = Convert.ToInt32(this.reader.GetValue(i));
                }
                return result;
            }

            public Int64? GetNInt64(string FieldName)
            {
                long? result = new long?();
                int i = this.FieldNameOrdinalPairs[FieldName];
                if (!this.reader.IsDBNull(i))
                {
                    result = Convert.ToInt64(this.reader.GetValue(i));
                }
                return result;
            }

            public string GetString(string FieldName)
            {
                int i = this.FieldNameOrdinalPairs[FieldName];
                if (!this.reader.IsDBNull(i))
                {
                    return this.reader.GetString(i);
                }
                return null;
            }

            public object GetValue(string FieldName)
            {
                int i = this.FieldNameOrdinalPairs[FieldName];
                if (!this.reader.IsDBNull(i))
                {
                    return this.reader.GetValue(i);
                }
                return null;
            }

            public bool IsDBNull(string FieldName)
            {
                int i = this.FieldNameOrdinalPairs[FieldName];
                return this.reader.IsDBNull(i);
            }
        }
    }

