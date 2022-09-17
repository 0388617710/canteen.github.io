using System;
using System.Data;
using System.Data.SqlClient;

namespace SoficCanteen
{
    public class DataProvider
    {
        private SqlDataAdapter myAdapter;
        private SqlConnection conn;
        //public static string sConnect = @"Data Source=10.11.4.23;Initial Catalog=QL_HANHCHINH;User ID=pos;Password=pos;Connect Timeout=120";
        //public static string sConnect = @"Data Source=127.0.0.1;Initial Catalog=SOFIC;User ID=pos;Password=pos;Connect Timeout=120";
        //public static string sConnect = @"Data Source=localhost;Initial Catalog=SUATAN_CHULAI;User ID=po;Password=12345678;Connect Timeout=120";
        public static string sConnect = @"Data Source=10.11.7.23;Initial Catalog=SUATAN_CHULAI;User ID=itdept;Password=TH@c0123456;Connect Timeout=120";

        #region DataProvider
        public DataProvider()
        {
            conn = new SqlConnection(sConnect);
        }
        #endregion

        #region openConnection
        private SqlConnection openConnection()
        {
            try
            {
                if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
                {
                    conn.Open();
                }
            }
            catch (Exception ex)
            {
                SystemUti.WriteLogError(ex.ToString());
            }

            return conn;
        }
        #endregion

        #region closeConnection
        private SqlConnection closeConnection()
        {
            if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
            {
                conn.Close();
                conn.Dispose();
            }
            return conn;
        }
        #endregion

        #region GetDateTable Method     

        public DataTable GetDataTable(string sql, CommandType ct, params SqlParameter[] para)
        {
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            using (SqlConnection connection = new SqlConnection(sConnect))
            {
                try
                {
                    command = new SqlCommand(sql, connection);
                    command.Connection.Open();
                    command.CommandType = ct;
                    command.CommandTimeout = 6000;
                    command.Parameters.Clear();
                    if (para != null)
                    {
                        foreach (SqlParameter p in para)
                        {
                            command.Parameters.Add(p);
                        }
                    }
                    da = new SqlDataAdapter(command);
                    da.Fill(dt);

                }
                catch (Exception ex)
                {
                    SystemUti.WriteLogError(ex.ToString());
                }
                finally
                {
                    command.Connection = closeConnection();
                }
            }
            return dt;
        }
        public static DataTable GetDataTable(string synConnect, string sql, CommandType ct, params SqlParameter[] para)
        {
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            using (SqlConnection connection = new SqlConnection(synConnect))
            {
                try
                {
                    command = new SqlCommand(sql, connection);
                    command.Connection.Open();
                    command.CommandType = ct;
                    command.CommandTimeout = 6000;
                    command.Parameters.Clear();
                    if (para != null)
                    {
                        foreach (SqlParameter p in para)
                        {
                            command.Parameters.Add(p);
                        }
                    }
                    da = new SqlDataAdapter(command);
                    da.Fill(dt);

                }
                catch (Exception ex)
                {
                    SystemUti.WriteLogError(ex.ToString());
                }
                finally
                {
                    //command.Connection = closeConnection();
                    if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
            return dt;
        }
        #endregion

        #region GetDataReader Method
        public SqlDataReader MyExcuteReader(string sql, CommandType ct, params SqlParameter[] param)
        {
            SqlDataReader rdr = null;
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Connection = openConnection();
                cmd = new SqlCommand(sql, conn);
                cmd.CommandType = ct;
                cmd.CommandTimeout = 6000;
                cmd.Parameters.Clear();
                if (param != null)
                {
                    foreach (SqlParameter p in param)
                    {
                        cmd.Parameters.Add(p);
                    }
                }

                rdr = cmd.ExecuteReader();
            }

            catch (Exception)
            {

                if (rdr != null)
                {
                    rdr.Close();
                }
            }
            finally
            {
                cmd.Connection = closeConnection();
            }
            return rdr;
        }

        #endregion

        #region GetDataSet Method
        /*
        public DataSet GetDataSet(string sql, CommandType ct, params SqlParameter[] para)
        {
            DataSet ds = new DataSet();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            using (SqlConnection connection = new SqlConnection(sConnect))
            {
                try
                {
                    command = new SqlCommand(sql, connection);
                    command.Connection.Open();
                    command.CommandType = ct;
                    command.CommandTimeout = 6000;
                    command.Parameters.Clear();
                    if (para != null)
                    {
                        foreach (SqlParameter p in para)
                        {
                            command.Parameters.Add(p);
                        }
                    }
                    da = new SqlDataAdapter(command);
                    da.Fill(ds);

                }
                catch (Exception)
                {

                }
                finally
                {
                    command.Connection = closeConnection();
                }
            }
            return ds;
        }
        public DataSet GetDataSet(string synConnect, string sql, CommandType ct, params SqlParameter[] para)
        {
            DataSet ds = new DataSet();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            using (SqlConnection connection = new SqlConnection(synConnect))
            {
                try
                {
                    command = new SqlCommand(sql, connection);
                    command.Connection.Open();
                    command.CommandType = ct;
                    command.CommandTimeout = 6000;
                    command.Parameters.Clear();
                    if (para != null)
                    {
                        foreach (SqlParameter p in para)
                        {
                            command.Parameters.Add(p);
                        }
                    }
                    da = new SqlDataAdapter(command);
                    da.Fill(ds);

                }
                catch (Exception)
                {

                }
                finally
                {
                    command.Connection = closeConnection();
                }
            }
            return ds;
        }
        */
        #endregion

        #region ExecuteScalar Method
        public string MyExecuteScalar(string sql, CommandType ct, params SqlParameter[] para)
        {
            string ketqua = null;
            //string synConnect = @"Data Source=113.161.6.133,9433;Initial Catalog=AutocomEnergy_Final;User ID=ppxx;Password=TH@c0123456;Connect Timeout=5";

            SqlCommand command = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(sConnect))
            {
                try
                {
                    command = new SqlCommand(sql, connection);
                    command.Connection.Open();
                    command.CommandType = ct;
                    command.CommandTimeout = 6000;
                    command.Parameters.Clear();
                    if (para != null)
                    {
                        foreach (SqlParameter p in para)
                        {
                            command.Parameters.Add(p);
                        }
                    }
                    ketqua = ((DateTime)command.ExecuteScalar()).ToString("yyyy-MM-dd HH:mm:ss.fffffff");
                }
                catch (Exception ex)
                {
                    SystemUti.WriteLogError(ex.ToString());
                }
                finally
                {
                    command.Connection = closeConnection();
                }
            }
            return ketqua;
        }
        public static byte[] MyExecuteScalar(string synConnect, string sql, CommandType ct, params SqlParameter[] para)
        {
            byte[] ketqua = null;
            //string synConnect = @"Data Source=113.161.6.133,9433;Initial Catalog=AutocomEnergy_Final;User ID=ppxx;Password=TH@c0123456;Connect Timeout=5";

            SqlCommand command = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(synConnect))
            {
                try
                {
                    command = new SqlCommand(sql, connection);
                    command.Connection.Open();
                    command.CommandType = ct;
                    command.CommandTimeout = 6000;
                    command.Parameters.Clear();
                    if (para != null)
                    {
                        foreach (SqlParameter p in para)
                        {
                            command.Parameters.Add(p);
                        }
                    }
                    ketqua = (byte[])command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    SystemUti.WriteLogError(ex.ToString());
                }
                finally
                {
                    //command.Connection = closeConnection();
                    if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
            return ketqua;
        }
        #endregion

        #region MyExcuteNonQuery
        public bool MyExcuteNonQuery(string sql, CommandType ct, params SqlParameter[] param)
        {
            bool ketqua = false;
            SqlCommand command = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(sConnect))
            {
                try
                {
                    command = new SqlCommand(sql, connection);
                    command.Connection.Open();
                    command.CommandType = ct;
                    command.CommandTimeout = 6000;
                    command.Parameters.Clear();
                    if (param != null)
                    {
                        foreach (SqlParameter p in param)
                        {
                            command.Parameters.Add(p);
                        }
                    }

                    command.ExecuteNonQuery();

                    ketqua = true;
                }
                catch (Exception ex)
                {
                    ketqua = false;
                    SystemUti.WriteLogError(ex.ToString());
                }
                finally
                {
                    command.Connection = closeConnection();
                }
            }
            return ketqua;
        }
        public static bool MyExcuteNonQuery(string synConnect, string sql, CommandType ct, params SqlParameter[] param)
        {
            bool ketqua = false;
            SqlCommand command = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(synConnect))
            {
                try
                {
                    command = new SqlCommand(sql, connection);
                    command.Connection.Open();
                    command.CommandType = ct;
                    command.CommandTimeout = 6000;
                    command.Parameters.Clear();
                    if (param != null)
                    {
                        foreach (SqlParameter p in param)
                        {
                            command.Parameters.Add(p);
                        }
                    }

                    command.ExecuteNonQuery();

                    ketqua = true;
                }
                catch (Exception ex)
                {
                    ketqua = false;
                    SystemUti.WriteLogError(ex.ToString());
                }
                finally
                {
                    //command.Connection = closeConnection();
                    if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
            return ketqua;
        }
        #endregion

        #region BulkCopy
        public static bool BulkCopy(DataTable dt, string tableName)
        {
            bool ketqua = false;
            SqlCommand command = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(sConnect))
            {
                try
                {
                    connection.Open();
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                    {
                        bulkCopy.DestinationTableName = tableName;

                        try
                        {
                            // Write from the source to the destination.
                            bulkCopy.WriteToServer(dt);
                            ketqua = true;
                        }
                        catch (Exception ex)
                        {
                            SystemUti.WriteLogError(ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    SystemUti.WriteLogError(ex.Message);
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return ketqua;
        }
        public static bool BulkCopy(string synConnect, DataTable dt, string tableName)
        {
            bool ketqua = false;
            //string synConnect = @"Data Source=113.161.6.133,9433;Initial Catalog=AutocomEnergy_Final;User ID=ppxx;Password=TH@c0123456;Connect Timeout=120";
            SqlCommand command = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(synConnect))
            {
                try
                {
                    connection.Open();
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                    {
                        bulkCopy.DestinationTableName = tableName;

                        try
                        {
                            // Write from the source to the destination.
                            bulkCopy.WriteToServer(dt);
                            ketqua = true;
                        }
                        catch (Exception ex)
                        {
                            SystemUti.WriteLogError(ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    SystemUti.WriteLogError(ex.Message);
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return ketqua;
        }
        #endregion
    }
}
