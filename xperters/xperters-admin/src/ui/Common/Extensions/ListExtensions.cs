using System.Collections.Generic;

namespace Xperters.Admin.UI.Common.Extensions
{
    public static class ListExtensions
    {
        public static void ClearAndAddRange<T>(this List<T> collection, IEnumerable<T> range)
        {
            collection.Clear();
            collection.AddRange(range);
        }
    }
}
