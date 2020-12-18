using System.Collections.Generic;
using System.Linq;

namespace Xperters.Admin.ServiceModel.Extensions
{
    public static class ListExtensions
    {
        public static List<T> ToListOrEmptyIfNull<T>(this IEnumerable<T> collection)
        {
            return collection?.ToList() ?? Enumerable.Empty<T>().ToList();
        }
	}
}
