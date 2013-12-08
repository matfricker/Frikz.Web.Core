using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Frikz.Web.Core
{
    public class DataAccessLayer
    {
        private static String ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString; }
        }

        public DataAccessLayer()
        { 
            // TODO: Add constructor logic here
        }

        public static DataTable ExecuteDataTable(string storedProcedureName, params SqlParameter[] arrParam)
        {
            DataTable dt = new DataTable();
            SqlConnection cn = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand(storedProcedureName, cn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (cn.State == ConnectionState.Closed || cn.State == ConnectionState.Broken)
            {
                cn.Open();
            }

            try
            {
                if (arrParam != null)
                {
                    foreach (SqlParameter param in arrParam)
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

        public static object DBNullHandler(object instance)
        {
            if (instance != null)
            {
                return instance;
            }
            else
            {
                return DBNull.Value;
            }
        }

        public static object DBIntHandler(object instance)
        {
            if (instance != null)
            {
                return instance;
            }
            else
            {
                return 0;
            }
        }
    }
}
