using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.Extensions
{
    public static class StringExtension
    {
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        public static string Substring(this string value, int length, bool permissive)
        {
            if (string.IsNullOrEmpty(value)) return value;

            return value.Substring(length > value.Length ? value.Length - 1 : length);
        }

        public static string Substring(this string value, int startIndex, int length, bool permissive)
        {
            if (string.IsNullOrEmpty(value)) return value;

            int l = length;

            if (l > value.Length - startIndex)
                l = value.Length - startIndex;

            if (l < 0)
                return "";

            return value.Substring(startIndex, l);
        }

        public static string PadInner(this string value, char filling, string endString, int totalLenght)
        {
            string result = value.PadRight(totalLenght, filling);
            result = result.Substring(0, result.Length - endString.Length, true);
            return $"{result}{endString}";
        }

        public static string Trim(this string value, bool permissive)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            return value.Trim();
        }

        public static List<string> SplitToList(this string value, params char[] parametros)
        {
            if (string.IsNullOrEmpty(value))
                return new List<string>();

            var tmpList = value.Split(parametros).ToList();
            var list = new List<string>();
            foreach (var item in tmpList)
            {
                if (string.IsNullOrEmpty(item))
                    continue;

                list.Add(item);
            }
            return value.Split(parametros).ToList();
        }
    }
}
