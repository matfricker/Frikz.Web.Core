using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Frikz.Web.Core
{
    public static class DataAccessLayer
    {
        static DataAccessLayer() {}
        
        private static SqlConnection connection;

        private static void OpenConnection()
        {
            connection = (connection == null) ? new SqlConnection(ConnectionString) : connection;
            if (connection.State == ConnectionState.Closed)
                connection.Open();
        }

        private static String ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString; }
        }

        public static DataTable ExecuteDataTable(string storedProcedureName,  params SqlParameter[] paramArray)
        {
            DataTable dt = new DataTable();
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(storedProcedureName, cn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (cn.State == ConnectionState.Closed || cn.State == ConnectionState.Broken)
                cn.Open();

            try
            {
                if (paramArray != null)
                {
                    foreach (SqlParameter param in paramArray)
                    {
                        cmd.Parameters.Add(param);
                    }
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Parameters.Clear();
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                cmd.Dispose();
                cn.Close();
            }
        }

        public static bool ExecuteProcedure(string procedureName, List<Tuple<string, object>> paramList)
        {
            using (SqlCommand cmd = DataAccessLayer.GetCommand(procedureName))
            {
                for (int i = 0; i < paramList.Count; i++)
                {
                    DataAccessLayer.PopulateParameter(cmd, paramList[i].Item1, paramList[i].Item2);
                }
                cmd.ExecuteNonQuery();
            }
            return true;
        }

        public static void PopulateParameter(SqlCommand command, string name, object val)
        {
            if (command != null && name != null)
            {
                // Checks the number of parameters attached to the command.
                if (command.Parameters.Count == 0)
                    SqlCommandBuilder.DeriveParameters(command);

                // Checks for the existence of a success flag.
                //if (command.Parameters.Contains("@Success") && command.Parameters["@Success"].Value == null)
                //{
                //    command.Parameters["@Success"].Value = 0;
                //}

                // Populate parameter with value, if the value is null ignore it.
                string pname = string.Concat("@", name);
                // ensure parameter name is not longer than 30 chars.
                //pname = pname.Length > 30 ? pname.Substring(0, 30) : pname;
                // Check for parameter
                if (command.Parameters.Contains(pname))
                {
                    SqlParameter param = command.Parameters[pname];

                    if (val != DBNull.Value && val != null)
                    {
                        switch (param.SqlDbType)
                        {
                            case SqlDbType.Date:
                                DateTime? t = Utilities.ConvertEx<DateTime?>(val, null);
                                param.Value = t != DateTime.MinValue ? t : null;
                                break;

                            case SqlDbType.Decimal:
                                param.Value = Utilities.ConvertEx<Decimal?>(val, null);
                                break;

                            default:
                                param.Value = val;
                                break;
                        }
                    }
                }
            }
        }

        public static SqlCommand GetCommand(string procedure)
        {
            return GetCommand(procedure, CommandType.StoredProcedure);
        }

        public static SqlCommand GetCommand(string sql, CommandType type)
        {
            OpenConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = type;
            command.CommandText = sql;
            return command;
        }        

    }
}
