using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace Bigstick.BuildingBlocks.Extensions
{
    public static class DataExtension
    {
        public static DataTable ToDataTableScalar<T>(this IEnumerable<T> list)
        {
            DataTable listaGenerica = new DataTable();
            listaGenerica.Columns.Add("value", typeof(string));

            foreach (var x in list)
            {
                listaGenerica.Rows.Add(x);
            }

            return listaGenerica;
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> data)
        {
            DataTable table = new DataTable();

            if (data != null)
            {
                PropertyDescriptorCollection props =
                    TypeDescriptor.GetProperties(typeof(T));

                foreach (PropertyDescriptor prop in props)
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                foreach (T item in data)
                {
                    DataRow row = table.NewRow();
                    foreach (PropertyDescriptor prop in props)
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    table.Rows.Add(row);
                }

            }
            return table;
        }
    }
}
