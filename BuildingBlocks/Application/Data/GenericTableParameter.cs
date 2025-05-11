using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.Application.Data
{
    public static class GenericTableParameter
    {
        public static DataTable MapToTable<T>(this IEnumerable<T> values, string columnName="value") 
        {
            DataTable table = new();
            table.Columns.Add(columnName, typeof(T));
            foreach (var item in values)
                table.Rows.Add(item);
            return table;
        }

    }
}
