using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Helpers
{
    public static class SqlUtilities
    {
        private static SqlParameter AddParameter(this SqlParameterCollection parms, SqlParameter param)
        {
            if (param.Value == null)
            {
                param.Value = DBNull.Value;
                return parms.Add(param);
            }
            else
            {
                return parms.Add(param);
            }

        }

        private static SqlConnection GetConnection(string connectionName)
        {
            SqlConnection cn = new SqlConnection(connectionName);
          //  cn.Open();
            return cn;
        }

        public static DataTable ExecuteQuery(string connectionName, string storedProcName, List<SqlParameter> procParameters)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = GetConnection(connectionName))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = storedProcName;

                    foreach (var procParameter in procParameters)
                    {
                        cmd.Parameters.AddParameter(procParameter);
                    }
                    cn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static IEnumerable<IDataRecord> ExecuteQueryDataReader(string connectionName, string storedProcName, List<SqlParameter> procParameters)
        {
            SqlDataReader myReader = null;
            using (SqlConnection cn = GetConnection(connectionName))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = storedProcName;

                    foreach (var procParameter in procParameters)
                    {
                        cmd.Parameters.AddParameter(procParameter);
                    }
                    cn.Open();
                    myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    try
                    {
                        while (myReader.Read())
                        {
                            yield return myReader;
                        }
                    }
                    finally
                    {
                        myReader.Close();
                    }
                }
            }


        }

        public static IEnumerable<IDataRecord> ExecuteDynamicSql(string connectionName,string sqlQuery)
        {
            SqlDataReader myReader = null;
            using (SqlConnection cn = GetConnection(connectionName))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;
                    myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    try
                    {
                        while (myReader.Read())
                        {
                            yield return myReader;
                        }
                    }
                    finally
                    {
                        myReader.Close();
                    }
                }
            }

        }
        public static int ExecuteNonQuery(string connectionName, string storedProcName, List<SqlParameter> procParameters)
        {
            int rc;
            using (SqlConnection cn = GetConnection(connectionName))
            {

                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = storedProcName;

                    foreach (var procParameter in procParameters)
                    {
                        cmd.Parameters.Add(procParameter);
                    }
                    cn.Open();
                    rc = cmd.ExecuteNonQuery();
                }
            }
            return rc;
        }

        //public static int ExecuteNonQueryWithOutParameter(string connectionName, string storedProcName, List<SqlParameter> procParameters, string outParameterName, out string outParameterValue)
        //{
        //    int rc;
        //    outParameterValue = default(string);
        //    using (SqlConnection cn = GetConnection(connectionName))
        //    {

        //        using (SqlCommand cmd = cn.CreateCommand())
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.CommandText = storedProcName;

        //            foreach (var procParameter in procParameters)
        //            {
        //                if (procParameter.ParameterName.Equals(outParameterName, StringComparison.OrdinalIgnoreCase))
        //                {
        //                    procParameter.Direction = ParameterDirection.Output;
        //                    procParameter.Size = 255;
        //                    cmd.Parameters.AddParameter(procParameter);
        //                }
        //                else
        //                {
        //                    cmd.Parameters.AddParameter(procParameter);
        //                }
        //            }

        //            rc = cmd.ExecuteNonQuery();
        //            outParameterValue = cmd.Parameters[outParameterName].Value.ToString() ?? null;
        //        }
        //    }
        //    return rc;
        //}

        //public static int ExecuteNonQueryWithInputOutParameter(string connectionName, string storedProcName, List<SqlParameter> procParameters, string inputOutParameterName, out string inputOutParameterValue)
        //{
        //    int rc;
        //    inputOutParameterValue = default(string);
        //    using (SqlConnection cn = GetConnection(connectionName))
        //    {

        //        using (SqlCommand cmd = cn.CreateCommand())
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.CommandText = storedProcName;

        //            foreach (var procParameter in procParameters)
        //            {
        //                if (procParameter.ParameterName.Equals(inputOutParameterName, StringComparison.OrdinalIgnoreCase))
        //                {
        //                    procParameter.Direction = ParameterDirection.InputOutput;
        //                    procParameter.Size = 255;
        //                    cmd.Parameters.AddParameter(procParameter);
        //                }
        //                else
        //                {
        //                    cmd.Parameters.AddParameter(procParameter);
        //                }
        //            }

        //            rc = cmd.ExecuteNonQuery();
        //            inputOutParameterValue = cmd.Parameters[inputOutParameterName].Value.ToString() ?? null;
        //        }
        //    }
        //    return rc;
        //}

        public static int ExecuteNonQueryWithInputOutParameter(string connectionName, string storedProcName, List<SqlParameter> procParameters, string inputOutParameterName, ParameterDirection inputOutParameterDirection, out string inputOutParameterValue, int? inputOutParameterSize = 255)
        {
            int rc;
            inputOutParameterValue = default(string);
            using (SqlConnection cn = GetConnection(connectionName))
            {

                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = storedProcName;

                    foreach (var procParameter in procParameters)
                    {
                        if (procParameter.ParameterName.Equals(inputOutParameterName, StringComparison.OrdinalIgnoreCase))
                        {
                            procParameter.Direction = (ParameterDirection)inputOutParameterDirection;
                            procParameter.Size = inputOutParameterSize ?? 255;
                            cmd.Parameters.AddParameter(procParameter);
                        }
                        else
                        {
                            cmd.Parameters.AddParameter(procParameter);
                        }
                    }
                    cn.Open();
                    rc = cmd.ExecuteNonQuery();
                    inputOutParameterValue = cmd.Parameters[inputOutParameterName].Value.ToString() ?? null;
                }
            }
            return rc;
        }
    }
}
