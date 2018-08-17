using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace GV.ONLINE.FOOD.GUI.Datarepository
{
    /// <summary>
    /// Object context
    /// </summary>
    public interface IOnlineFoodDataContext
    {
        TResult ExecuteRoutineWithTableType<TResult, TEntity>(string routineName, string tableParamName,
            DataTable dtParam, Dictionary<string, object> parameters, Func<DataRow, TEntity> getObject)
            where TResult : ICollection<TEntity>, new();

        TEntity ExecuteRoutine<TEntity>(string routineName, Dictionary<string, object> parameters,
            Func<DbDataReader, TEntity> getObject);

        TResult ExecuteRoutine<TResult, TEntity>(string routineName, Dictionary<string, object> parameters,
            Func<DbDataReader, TEntity> getObject) where TResult : ICollection<TEntity>, new();

        TEntity ExecuteScalarRoutine<TEntity>(string routineName, Dictionary<string, object> parameters);
        TEntity ExecuteScalarInlineRoutine<TEntity>(string routineName);
        int ExecuteNonQueryRoutine(string routineName, IDictionary<string, object> parameters = null);

        object ExecuteNonQueryRoutineForInputOutput(string routineName, List<string> outParams,
            List<string> returnParams, IDictionary<string, object> parameters = null);

        int ExecuteNonQueryInline(string routineName);
        List<T> ExecuteQueryDataReaderNew<T>(string storedProcName, List<SqlParameter> procParameters) where T : new();

        List<T> ExecuteQueryDataReaderNewWithErrorKey<T>(string storedProcName, List<SqlParameter> procParameters,
            out int? errorKey) where T : new();

        DataSet GetDataSetResultSet(string storedProcName, List<SqlParameter> procParameters);
        DataTable GetDataTableResultSet(string storedProcName, List<SqlParameter> procParameters); 

        IDictionary<string, object> ExecuteNonQueryRoutineForInputOutput(string routineName,
            IDictionary<string, int> outParams, IDictionary<string, object> parameters = null);

        DataTable ExecuteQueryAndReturnDataReader(string storedProcName, List<SqlParameter> procParameters,
            bool isProcedure);
        DataSet ExecuteQueryAndReturnDataSet(string storedProcName, List<SqlParameter> procParameters,
            bool isProcedure);
    }
}