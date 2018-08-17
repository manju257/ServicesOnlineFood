using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using GV.ONLINE.FOOD.GUI.Common;

namespace GV.ONLINE.FOOD.GUI.Datarepository
{
    public class OnlineFoodDataContext : BaseDataRepository, IOnlineFoodDataContext   //DbContext,
    {
        public static string ONLINEFOOD_CONNECTION_STRING;

        static OnlineFoodDataContext()
        {
            ONLINEFOOD_CONNECTION_STRING = ConfigurationManager.ConnectionStrings["OnlineFood_Dev"].ConnectionString;
        }

        #region Newly Enhanced with error key

        /// <summary>
        /// ExecuteQueryDataReaderNewWithErrorKey
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcName"></param>
        /// <param name="procParameters"></param>
        /// <param name="errorKey"></param>
        /// <returns></returns>
        public List<T> ExecuteQueryDataReaderNewWithErrorKey<T>(string storedProcName, List<SqlParameter> procParameters,
            out int? errorKey) where T : new()
        {
            SqlDataReader myReader = null;
            var returnList = new List<T>();
            var type = typeof(T);
            var properties = type.GetProperties();
            errorKey = default(int);
            using (var cn = new SqlConnection(ONLINEFOOD_CONNECTION_STRING))
            {
                using (var cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = storedProcName;

                    foreach (var procParameter in procParameters)
                    {
                        cmd.Parameters.AddParameter(procParameter);
                    }

                    var returnValueParameter = new SqlParameter();
                    returnValueParameter.ParameterName = "@retval";
                    returnValueParameter.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(returnValueParameter);
                    cn.Open();
                    cmd.CommandTimeout = 0;
                    myReader = cmd.ExecuteReader();
                    try
                    {
                        while (myReader.Read())
                        {
                            var p = new T();

                            foreach (var v in properties)
                            {
                                var ppt = typeof(T).GetProperty(v.Name);
                                if (myReader.HasColumn(v.Name))
                                {
                                    ppt.SetValue(p, myReader[v.Name] == DBNull.Value ? null : myReader[v.Name], null);
                                }
                            }

                            returnList.Add(p);
                            // yield return p;
                        }
                    }
                    finally
                    {
                        myReader.Close();
                        errorKey = (int) cmd.Parameters["@retval"].Value;
                    }
                }
            }

            return returnList;
        }

        #endregion

        /// <summary>
        /// ExecuteQueryAndReturnDataReader
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <param name="procParameters"></param>
        /// <param name="isProcedure"></param>
        /// <returns></returns>
        public DataTable ExecuteQueryAndReturnDataReader(string storedProcName, List<SqlParameter> procParameters,
            bool isProcedure)
        {
            SqlDataReader myReader = null;
            var dt = new DataTable();
            using (var cn = new SqlConnection(ONLINEFOOD_CONNECTION_STRING))
            {
                using (var cmd = cn.CreateCommand())
                {
                    if (isProcedure)
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        foreach (var procParameter in procParameters)
                        {
                            cmd.Parameters.AddParameter(procParameter);
                        }
                    }
                    else
                        cmd.CommandType = CommandType.Text;
                    cmd.CommandText = storedProcName;
                    cn.Open();
                    myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    try
                    {
                        dt.Load(myReader);
                    }
                    finally
                    {
                        myReader.Close();
                    }
                }
            }
            return dt;
        }
        public DataSet ExecuteQueryAndReturnDataSet(string storedProcName, List<SqlParameter> procParameters,
            bool isProcedure)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            using (var cn = new SqlConnection(ONLINEFOOD_CONNECTION_STRING))
            {
                using (var cmd = cn.CreateCommand())
                {
                    if (isProcedure)
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        foreach (var procParameter in procParameters)
                        {
                            cmd.Parameters.AddParameter(procParameter);
                        }
                    }
                    else
                    {
                        cmd.CommandType = CommandType.Text;
                    }
                    cmd.CommandText = storedProcName;
                    cn.Open();
                    try
                    {
                        da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                    }
                    finally
                    {
                    }
                }
            }
            return ds;
        }
        #region ADO

        /// <summary>
        /// ExecuteNonQueryRoutine
        /// </summary>
        /// <param name="routineName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteNonQueryRoutine(string routineName, IDictionary<string, object> parameters = null)
        {
            try
            {
                int record;
                using (var connection = new SqlConnection(ONLINEFOOD_CONNECTION_STRING))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = routineName;
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }
                    connection.Open();
                    record = command.ExecuteNonQuery();
                }

                return record;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ExecuteNonQueryInline
        /// </summary>
        /// <param name="routineName"></param>
        /// <returns></returns>
        public int ExecuteNonQueryInline(string routineName)
        {
            try
            {
                int record;
                using (var connection = new SqlConnection(ONLINEFOOD_CONNECTION_STRING))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = routineName;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    record = command.ExecuteNonQuery();
                }
                return record;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ExecuteNonQueryRoutineForInputOutput
        /// </summary>
        /// <param name="routineName"></param>
        /// <param name="outParams"></param>
        /// <param name="returnParams"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object ExecuteNonQueryRoutineForInputOutput(string routineName, List<string> outParams,
            List<string> returnParams, IDictionary<string, object> parameters = null)
        {
            try
            {
                int record;
                using (var connection = new SqlConnection(ONLINEFOOD_CONNECTION_STRING))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = routineName;
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            if (outParams.Any(obj => obj == param.Key))
                                command.Parameters.AddWithValue(param.Key, param.Value).Direction =
                                    ParameterDirection.InputOutput;
                            else if (returnParams.Any(obj => obj == param.Key))
                                command.Parameters.AddWithValue(param.Key, param.Value).Direction =
                                    ParameterDirection.ReturnValue;
                            else
                                command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }
                    connection.Open();
                    record = command.ExecuteNonQuery();
                }
                return record;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ExecuteNonQueryRoutineForInputOutput
        /// </summary>
        /// <param name="routineName"></param>
        /// <param name="outParams"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IDictionary<string, object> ExecuteNonQueryRoutineForInputOutput(string routineName,
            IDictionary<string, int> outParams, IDictionary<string, object> parameters = null)
        {
            var outputs = new Dictionary<string, object>();
            try
            {
                int record;
                using (var connection = new SqlConnection(ONLINEFOOD_CONNECTION_STRING))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = routineName;
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                        foreach (var param in outParams)
                        {
                            var parameter = new SqlParameter();
                            parameter.Direction = ParameterDirection.InputOutput;
                            parameter.Size = param.Value;
                            parameter.Value = string.Empty;
                            if (param.Key == "@AppUserKey")
                                parameter.Value = DBNull.Value;
                            parameter.ParameterName = param.Key;
                            command.Parameters.Add(parameter);
                        }
                    }
                    connection.Open();
                    record = command.ExecuteNonQuery();

                    foreach (SqlParameter param in command.Parameters)
                    {
                        if (param.Direction == ParameterDirection.InputOutput)
                        {
                            outputs.Add(param.ParameterName, param.Value);
                        }
                    }
                }
                return outputs;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ExecuteRoutine
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="routineName"></param>
        /// <param name="parameters"></param>
        /// <param name="getObject"></param>
        /// <returns></returns>
        public TEntity ExecuteRoutine<TEntity>(string routineName, Dictionary<string, object> parameters,
            Func<DbDataReader, TEntity> getObject)
        {
            var returnValue = default(TEntity);
            try
            {
                using (var connection = new SqlConnection(ONLINEFOOD_CONNECTION_STRING))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = routineName;
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }
                    connection.Open();
                    var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    if (reader.Read())
                    {
                        returnValue = getObject(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            if (returnValue == null)
            {
                returnValue = default(TEntity);
            }

            return returnValue;
        }

        /// <summary>
        /// ExecuteRoutine
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="routineName"></param>
        /// <param name="parameters"></param>
        /// <param name="getObject"></param>
        /// <returns></returns>
        public TResult ExecuteRoutine<TResult, TEntity>(string routineName, Dictionary<string, object> parameters,
            Func<DbDataReader, TEntity> getObject)
            where TResult : ICollection<TEntity>, new()
        {
            var returnValue = new TResult();
            try
            {
                using (var connection = new SqlConnection(ONLINEFOOD_CONNECTION_STRING))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = routineName;
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }
                    connection.Open();
                    var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        returnValue.Add(getObject(reader));
                    }
                    //using (SqlDataAdapter da = new SqlDataAdapter(command))
                    //{
                    //    DataSet ds = new DataSet();
                    //    da.Fill(ds);
                    //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    //    {
                    //        foreach (DataRow dr in ds.Tables[0].Rows)
                    //            returnValue.Add(getObject(dr));
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return returnValue;
        }

        /// <summary>
        /// ExecuteScalarRoutine
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="routineName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public TEntity ExecuteScalarRoutine<TEntity>(string routineName, Dictionary<string, object> parameters)
        {
            object returnValue;
            try
            {
                using (var connection = new SqlConnection(ONLINEFOOD_CONNECTION_STRING))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = routineName;
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }
                    connection.Open();
                    returnValue = command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            if (returnValue == null || returnValue is DBNull)
            {
                returnValue = default(TEntity);
            }

            return (TEntity) returnValue;
        }

        /// <summary>
        /// ExecuteScalarInlineRoutine
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="routineName"></param>
        /// <returns></returns>
        public TEntity ExecuteScalarInlineRoutine<TEntity>(string routineName)
        {
            object returnValue;
            try
            {
                using (var connection = new SqlConnection(ONLINEFOOD_CONNECTION_STRING))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = routineName;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    returnValue = command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            if (returnValue == null || returnValue is DBNull)
            {
                returnValue = default(TEntity);
            }
            return (TEntity) returnValue;
        }


        /// <summary>
        /// ExecuteRoutineWithTableType
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="routineName"></param>
        /// <param name="tableParamName"></param>
        /// <param name="dtParam"></param>
        /// <param name="parameters"></param>
        /// <param name="getObject"></param>
        /// <returns></returns>
        public TResult ExecuteRoutineWithTableType<TResult, TEntity>(string routineName, string tableParamName,
            DataTable dtParam, Dictionary<string, object> parameters, Func<DataRow, TEntity> getObject)
            where TResult : ICollection<TEntity>, new()
        {
            var returnValue = new TResult();
            try
            {
                using (var connection = new SqlConnection(ONLINEFOOD_CONNECTION_STRING))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = routineName;
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }
                    if (dtParam != null && dtParam.Rows.Count > 0)
                    {
                        var tvpParam = command.Parameters.AddWithValue(tableParamName, dtParam); //Needed TVP
                        tvpParam.SqlDbType = SqlDbType.Structured; //tells ADO.NET we are passing TVP
                    }
                    connection.Open();
                    using (var da = new SqlDataAdapter(command))
                    {
                        var ds = new DataSet();
                        da.Fill(ds);
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            var dr = ds.Tables[0].Rows[0];
                            returnValue.Add(getObject(dr));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return returnValue;
        }

        #endregion

        #region Newly Enhanced

        /// <summary>
        /// ExecuteQueryDataReaderNew
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcName"></param>
        /// <param name="procParameters"></param>
        /// <returns></returns>
        public List<T> ExecuteQueryDataReaderNew<T>(string storedProcName, List<SqlParameter> procParameters)
            where T : new()
        {
            SqlDataReader myReader = null;
            var returnList = new List<T>();
            var type = typeof(T);
            var properties = type.GetProperties();

            using (var cn = new SqlConnection(ONLINEFOOD_CONNECTION_STRING))
            {
                using (var cmd = cn.CreateCommand())
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
                            var p = new T();

                            foreach (var v in properties)
                            {
                                var ppt = typeof(T).GetProperty(v.Name);
                                if (myReader.HasColumn(v.Name))
                                {
                                    ppt.SetValue(p, myReader[v.Name] == DBNull.Value ? null : myReader[v.Name], null);
                                }
                            }

                            returnList.Add(p);
                            // yield return p;
                        }
                    }
                    finally
                    {
                        myReader.Close();
                    }
                }
            }

            return returnList;
        }
        /// <summary>
        /// GetDataTableResultSet
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <param name="procParameters"></param>
        /// <returns></returns>
        public DataSet GetDataSetResultSet(string storedProcName, List<SqlParameter> procParameters)
        {
            var ds = new DataSet();
            using (var cn = new SqlConnection(ONLINEFOOD_CONNECTION_STRING))
            {
                using (var cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = storedProcName;

                    foreach (var procParameter in procParameters)
                    {
                        cmd.Parameters.AddParameter(procParameter);
                    }
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandTimeout = 200;
                        cmd.CommandType = CommandType.StoredProcedure;
                        da.Fill(ds);
                    }
                }
            }
            return ds;
        }
        public DataTable GetDataTableResultSet(string storedProcName, List<SqlParameter> procParameters)
        {
            var dt = new DataTable();
            using (var cn = new SqlConnection(ONLINEFOOD_CONNECTION_STRING))
            {
                using (var cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = storedProcName;

                    foreach (var procParameter in procParameters)
                    {
                        cmd.Parameters.AddParameter(procParameter);
                    }
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandTimeout = 200;
                        cmd.CommandType = CommandType.StoredProcedure;
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }
        #endregion
    }
}