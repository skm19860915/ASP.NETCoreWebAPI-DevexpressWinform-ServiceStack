using System;

namespace Xperters.Admin.UI.Common
{
	public class ColumnNameAttribute : Attribute
	{
		public string ColumnName;

		public ColumnNameAttribute(string columnName)
		{
			ColumnName = columnName;
		}
	}
}
