using System;
using System.Collections.Generic;
using System.Linq;

namespace FiveGears.Core.Extensions.UtilityExtensions
{
    public static class StringExtensions
    {
        public static bool ContainsAny(this string input, IEnumerable<string> containsKeywords, StringComparison comparisonType)
        {
            return containsKeywords.Any(keyword => input.IndexOf(keyword, comparisonType) >= 0);
        }
        public static bool IsNullOrWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        public static string ToCamelCase(this string str)
        {
            if (string.IsNullOrEmpty(str) || char.IsLower(str, 0))
                return str;

            return char.ToLowerInvariant(str[0]) + str.Substring(1);
        }

        public static List<string> AsStringList(this string str)
        {
            return new List<string>()
            {
                str
            };
        }
    }
}
