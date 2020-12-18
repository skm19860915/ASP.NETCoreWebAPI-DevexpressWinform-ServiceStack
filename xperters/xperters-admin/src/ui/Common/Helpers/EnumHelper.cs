using System;
using System.Collections.Generic;
using System.Linq;

namespace Xperters.Admin.UI.Common.Helpers
{
	public static class EnumHelper
	{
		/// <summary>
		/// Returns all enum values in enum T as a list
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static List<T> ToList<T>()
		{
			if (!typeof(T).IsEnum)
				throw new NotSupportedException($"Type {typeof(T)} is not an enum. Only enum types are supported");

			return Enum.GetValues(typeof(T)).Cast<T>().ToList();
		}
	}
}
