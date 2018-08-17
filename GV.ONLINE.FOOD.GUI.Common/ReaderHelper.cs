using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace GV.ONLINE.FOOD.GUI.Common
{
    public static class ReaderHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ConvertField<T>(this DataRow dataRow, string columnName)
        {
            if (!dataRow.IsNull(columnName))
            {
                return (T) dataRow[columnName];
            }

            return default(T);
        }

        /// <summary>
        /// ConvertField
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataRow"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ConvertField<T>(this DbDataReader dataRow, string columnName)
        {
            if (dataRow[columnName] != DBNull.Value)
            {
                return (T) dataRow[columnName];
            }

            return default(T);
        }

        /// <summary>
        /// ConvertField
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataRow"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ConvertField<T>(this DataRow dataRow, int columnIndex)
        {
            if (!dataRow.IsNull(columnIndex))
            {
                return (T) dataRow[columnIndex];
            }

            return default(T);
        }

        /// <summary>
        /// ConvertField
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataRow"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ConvertField<T>(this DbDataReader dataRow, int columnIndex)
        {
            if (dataRow[columnIndex] != DBNull.Value)
            {
                return (T) dataRow[columnIndex];
            }

            return default(T);
        }

        /// <summary>
        /// Convert
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Convert<T>(this DbDataReader reader, string columnName)
        {
            if (reader[columnName] != DBNull.Value)
            {
                return (T) reader[columnName];
            }
            return default(T);
        }

        /// <summary>
        /// CheckColumnConvert
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <param name="schemaTable"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CheckColumnConvert<T>(this DbDataReader reader, string columnName, DataTable schemaTable)
        {
            if (schemaTable != null)
            {
                schemaTable.DefaultView.RowFilter = string.Format("ColumnName= '{0}'", columnName);
                if (schemaTable.DefaultView.Count > 0)
                {
                    if (reader[columnName] != DBNull.Value)
                    {
                        return (T) reader[columnName];
                    }
                }
            }

            return default(T);
        }

        /// <summary>
        /// CheckFieldConvert
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataRow"></param>
        /// <param name="columnName"></param>
        /// <param name="schemaTable"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CheckFieldConvert<T>(this DataRow dataRow, string columnName, DataTable schemaTable)
        {
            if (schemaTable != null && schemaTable.Rows.Count > 0)
            {
                //schemaTable.DefaultView.RowFilter = string.Format("ColumnName= '{0}'", columnName);
                //if (schemaTable.DefaultView.Count > 0)
                //{
                if (schemaTable.Columns[columnName] != null)
                {
                    if (!dataRow.IsNull(columnName))
                    {
                        return (T) dataRow[columnName];
                    }
                }

                return default(T);
                //}
            }

            return default(T);
        }

        /// <summary>
        /// Convert
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Convert<T>(this DbDataReader reader, int columnIndex)
        {
            if (reader[columnIndex] != DBNull.Value)
            {
                return (T) reader[columnIndex];
            }

            return default(T);
        }

        /// <summary>
        /// AddParameter
        /// </summary>
        /// <param name="parms"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static SqlParameter AddParameter(this SqlParameterCollection parms, SqlParameter param)
        {
            if (param.Value == null)
            {
                param.Value = DBNull.Value;
                return parms.Add(param);
            }
            return parms.Add(param);
        }

        /// <summary>
        /// HasColumn
        /// </summary>
        /// <param name="r"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static bool HasColumn(this IDataRecord r, string columnName)
        {
            try
            {
                return r.GetOrdinal(columnName) >= 0;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }
    }
}