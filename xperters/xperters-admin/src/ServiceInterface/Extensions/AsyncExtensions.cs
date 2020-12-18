using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xperters.Admin.ServiceInterface.Extensions
{
	public static class AsyncExtensions
	{
		public static async Task<List<T>> WhereAsync<T>(
			this IEnumerable<T> source, Func<T, Task<bool>> func)
		{
			var items = new List<T>();
			foreach (var element in source)
			{
				if (await func(element))
					items.Add(element);
			}
			return items;
		}
	}
}