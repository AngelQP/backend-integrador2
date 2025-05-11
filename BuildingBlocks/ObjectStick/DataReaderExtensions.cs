using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.ObjectStick
{
    public static class DataReaderExtensions
    {
        public static Dictionary<int, string> GetNames(this IDataReader reader) 
        {
            Dictionary<int, string> data = new();
            foreach (var item in Enumerable.Range(0, reader.FieldCount))
            {
                data.Add(item, reader.GetName(item));
            }
            return data;
        }
        public static bool CheckExistsColumn(this IDataReader reader, string columnName)
        {
            return Enumerable
                    .Range(0, reader.FieldCount - 1)
                    .Any(i => reader
                                .GetName(i)
                                .Equals(columnName, StringComparison.InvariantCultureIgnoreCase));

        }

        public static bool GetTryValue<TResult>(this IDataReader dataReader, Func<int, TResult> func, string columnName, ref TResult value)
        {
            if (!dataReader.CheckExistsColumn(columnName)) return false;

            var ordinal = dataReader.GetOrdinal(columnName);

            value = dataReader.IsDBNull(ordinal) ? default : func(ordinal);

            return true;
        }

        public static TResult GetValue<TResult>(this IDataReader dataReader, string columnName, Func<int, TResult> func)
        {
            var ordinal = dataReader.GetOrdinal(columnName);

            return dataReader.IsDBNull(ordinal) ? default : func(ordinal);
        }

        public static TResult GetValue<TResult>(this IDataReader dataReader, string columnName)
        {
            var ordinal = dataReader.GetOrdinal(columnName);

            return dataReader.IsDBNull(ordinal) ? default : (TResult)dataReader.GetValue(ordinal);
        }

        public static object GetValue(this IDataReader dataReader, string columnName)
        {
            var ordinal = dataReader.GetOrdinal(columnName);

            return dataReader.IsDBNull(ordinal) ? default : dataReader.GetValue(ordinal);
        }

        public static bool IsDbNull(this IDataReader dataReader, string columnName) 
        {
            var ordinal = dataReader.GetOrdinal(columnName);

            return dataReader.IsDBNull(ordinal);
        }

        public static bool GetTryString(this IDataReader dataReader, string columnName, bool includeEmpty, out string value)
        {
            var ordinal = dataReader.GetOrdinal(columnName);

            value = null;
            
            if (dataReader.IsDBNull(ordinal) || dataReader.GetString(ordinal).Trim().Equals(string.Empty, StringComparison.OrdinalIgnoreCase)) 
                return false;
            

            value = dataReader.GetString(ordinal);

            return true;
        }


        public static string GetString(this IDataReader dataReader, string columnName) => dataReader.GetValue(columnName, dataReader.GetString);

        public static bool GetBoolean(this IDataReader dataReader, string columnName) => dataReader.GetValue(columnName, dataReader.GetBoolean);

        public static DateTime GetDateTime(this IDataReader dataReader, string columnName) => dataReader.GetValue(columnName, dataReader.GetDateTime);

        public static decimal GetDecimal(this IDataReader dataReader, string columnName) => dataReader.GetValue(columnName, dataReader.GetDecimal);

        public static double GetDouble(this IDataReader dataReader, string columnName) => dataReader.GetValue(columnName, dataReader.GetDouble);

        public static float GetFloat(this IDataReader dataReader, string columnName) => dataReader.GetValue(columnName, dataReader.GetFloat);

        public static Guid GetGuid(this IDataReader dataReader, string columnName) => dataReader.GetValue(columnName, dataReader.GetGuid);

        public static short GetInt16(this IDataReader dataReader, string columnName) => dataReader.GetValue(columnName, dataReader.GetInt16);

        public static int GetInt32(this IDataReader dataReader, string columnName) => dataReader.GetValue(columnName, dataReader.GetInt32);

        public static long GetInt64(this IDataReader dataReader, string columnName) => dataReader.GetValue(columnName, dataReader.GetInt64);

        public static Guid? GetGuidNullable(this IDataReader dataReader, string columnName) => dataReader.GetValue(columnName, dataReader.GetGuid);

        public static bool? GetBooleanNullable(this IDataReader dataReader, string columnName) => dataReader.GetValue(columnName, dataReader.GetBoolean);

        public static DateTime? GetDateTimeNullable(this IDataReader dataReader, string columnName) => dataReader.GetValue(columnName, dataReader.GetDateTime);

    }
}
