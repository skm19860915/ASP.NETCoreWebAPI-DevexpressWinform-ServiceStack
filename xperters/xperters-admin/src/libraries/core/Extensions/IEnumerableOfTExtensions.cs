using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Xperters.Core.Extensions
{
    // ReSharper disable once InconsistentNaming
    public static class IEnumerableOfTExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var obj in source)
            {
                action(obj);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> ie, Action<T, int> action)
        {
            var i = 0;
            foreach (var e in ie)
                action(e, i++);
        }

        public static bool IsEmpty<TItem>(this IEnumerable<TItem> items)
        {
            return items.Count() <= 0;
        }

        public static string ToCsvOrNull<T>(this IEnumerable<T> source)
        {
            return ToCsvOrNull(source, CultureInfo.CurrentCulture);
        }

        public static string ToCsvOrNull<T>(this IEnumerable<T> source, CultureInfo culture, bool escapeCharsWhenNeeded = true)
        {
            var delimitedString = ToCsvOrEmpty(source, culture, escapeCharsWhenNeeded);
            return delimitedString == string.Empty ? null : delimitedString;
        }

        public static string ToCsvOrEmpty<T>(this IEnumerable<T> source)
        {
            return ToCsvOrEmpty(source, CultureInfo.CurrentCulture);
        }

        public static string ToCsvOrEmpty<T>(this IEnumerable<T> source, CultureInfo culture, bool escapeCharsWhenNeeded = true)
        {
            return ToCsvOrEmpty(source, culture.TextInfo.ListSeparator, escapeCharsWhenNeeded);
        }

        public static string ToCsvOrEmpty<T>(this IEnumerable<T> source, string separator, bool escapeCharsWhenNeeded = true)
        {
            const string doubleQuote = "\"";
            const string twoDoubleQuotes = "\"\"";

            var builder = new StringBuilder();
            var appendSeparatorNextTime = false;

            foreach (var item in source)
            {
                var itemAsString = item.ToString();

                var hasQuotes = itemAsString.Contains(doubleQuote);
                if (escapeCharsWhenNeeded && hasQuotes)
                {
                    itemAsString = itemAsString.Replace(doubleQuote, twoDoubleQuotes);
                }

                if (appendSeparatorNextTime)
                {
                    builder.Append(separator);
                }

                var needQuotes = escapeCharsWhenNeeded && (hasQuotes || itemAsString.Contains(separator));
                if (needQuotes)
                {
                    builder.AppendFormat("{0}{1}{0}", doubleQuote, itemAsString);
                }
                else
                {
                    builder.Append(itemAsString);
                }

                appendSeparatorNextTime = true;
            }

            return builder.ToString();
        }
    }
}
