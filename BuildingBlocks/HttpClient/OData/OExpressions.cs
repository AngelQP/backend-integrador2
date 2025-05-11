using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Bigstick.BuildingBlocks.HttpClient.OData
{
    public static class OExpressions
    {

        private static string FilterRegex = @"(?<Filter>" +
         "\n" + @"     (?<Resource>.+?)\s+" +
         "\n" + @"     (?<Operator>lk|eq|ne|gt|ge|lt|le|add|sub|mul|div|mod)\s+" +
         "\n" + @"     '?(?<Value>.+?)'?" +
         "\n" + @")" +
         "\n" + @"(?:" +
         "\n" + @"    \s*$" +
         "\n" + @"   |\s+(?:or|and|not)\s+" +
         "\n" + @")" +
         "\n";
        private static string FilterFormat = @"${Resource}§${Operator}§${Value}" + Environment.NewLine;
        private static System.Text.RegularExpressions.Regex Filter = new Regex(FilterRegex, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

        private static string OrderByRegex = @"(?<Field>.+?){1}\s+(?<Dir>.+){0,1}";
        private static string OrderByFormat = @"${Field},${Dir}";
        private static System.Text.RegularExpressions.Regex OrderBy = new Regex(OrderByRegex, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

        const string filterParameterName = "$filter";
        public static QueryObject ParseFilter(string value) 
        {
            return Parse(filterParameterName, value);
        }
        public static QueryObject Parse(string param, string value)
        {
            QueryObject options = new QueryObject();
            Dictionary<string, StringValues> values = new();
            values.Add(param, value);
            return options.Parse(values);
        }
        public static QueryObject Parse(this QueryObject options, string param, string value) 
        {
            Dictionary<string, StringValues> values = new();
            values.Add(param, value);
            return options.Parse(values);
        }
        public static QueryObject Parse(Dictionary<string, StringValues> query) 
        {
            QueryObject options = new QueryObject();

            return Parse(options, query);
        }
        public static QueryObject Parse(this QueryObject options, Dictionary<string, StringValues> query)
        {
            //input = HttpUtility.UrlDecode(input);
            

            //string[] resources = input.Split(new[] { '?' }, 2);

            //string filter = resources.Length == 2 ? resources[1] : null;
            if (!query.Any())
                return options;

            //    var parts = filter.Split('&');

            // Default Options.
            options.PageOption = new PagingOptions();
            foreach (var keyValuePair in query)
            {
                string key = HttpUtility.UrlDecode(keyValuePair.Key);
                string value = HttpUtility.UrlDecode(keyValuePair.Value);

                switch (key.ToLower())
                {
                    case "$filter":
                        options.Filters = ProcessFilter(value);
                        options.QueryParams.Add("$filter", value);
                        break;
                    case "$orderby":
                        options.OrderBys = ProcessOrderBy(value);
                        options.QueryParams.Add("$orderby", value);
                        break;
                    case "$startat":
                        options.PageOption.StartAt = ProcessTop(value);
                        break;
                    case "$maxresult":
                        options.PageOption.MaxResult = ProcessSkip(value);
                        break;
                    case "$metadata":
                        options.Metadata = ProcessMetadata(value);
                        break;
                    case "$select":
                        options.Select = ProcessSelect(value);
                        break;
                    case "$expand":
                        options.Expands = ProcessExpand(value);
                        break;
                }
            }

            return options;
        }

        private static Expand[] ProcessExpand(string value)
        {
            var clauses = value.Split(new[] { ',' });
            Expand[] results = new Expand[clauses.Length];
            for (int i = 0; i < clauses.Length; i++)
            {
                results[i] = new Expand() { Name = clauses[i] };
                // TODO:
            }
            return results;
        }

        private static string ProcessSelect(string value)
        {
            return value;
        }

        private static bool ProcessMetadata(string value)
        {
            return true;
        }
        private static OrderByOptions[] ProcessOrderBy(string value)
        {
            List<OrderByOptions> result = new List<OrderByOptions>();
            var clauses = value.Split(new[] { ',' });
            for (int i = 0; i < clauses.Length; i++)
            {
                var clause = OrderBy.Replace(clauses[i], OrderByFormat).Split(',');

                var orderBy = new OrderByOptions() { Name = clause[0] };
                if (clause.Length == 2)
                {
                    switch (clause[1].ToLower())
                    {
                        case "asc":
                            orderBy.Ascending = true;
                            break;
                        case "desc":
                            orderBy.Ascending = false;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    orderBy.Ascending = true;
                }

                result.Add(orderBy);
            }

            return result.ToArray();
        }
        public static Filter[] ProcessFilter(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return new Filter[0];
            List<Filter> result = new List<Filter>();
            var parse = Filter.Replace(value, FilterFormat);

            var reader = new StringReader(parse);

            var item = reader.ReadLine();
            while (!string.IsNullOrWhiteSpace(item))
            {
                var line = item.Split('§');
                var filter = new Filter();
                filter.Attribute = line[0];
                filter.Operator = (FilterOperator)Enum.Parse(typeof(FilterOperator), line[1]);
                filter.Value = line[2];
                result.Add(filter);
                item = reader.ReadLine();
            }
            return result.ToArray();
        }
        private static int ProcessTop(string value)
        {
            return GetPositiveInteger("pageSize", value);
        }
        private static int ProcessSkip(string value)
        {
            return GetPositiveInteger("pageNumber", value);
        }
        private static int GetPositiveInteger(string key, string value)
        {
            int intValue;
            if (!int.TryParse(value, out intValue) || intValue < 0)
            {
                throw new Exception(String.Format(" {1} must be positive", key));
            }
            return intValue;
        }

    }
}
