using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xperters.Admin.UI.Common.Extensions
{
    public static class IEnumerableOfTExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T obj in source)
                action(obj);
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T, int> action)
        {
            int num = 0;
            foreach (T obj in enumerable)
                action(obj, num++);
        }

        public static bool IsEmpty<TItem>(this IEnumerable<TItem> items)
        {
            return !items.Any<TItem>();
        }
    }
}
