using System;

namespace Xperters.Admin.UI.Common
{
	public static class Enums
	{
		public enum ErrorWarningSource
		{
			NotSet = 0,
			UploadTab = 1,
			GenerateEarnings = 2
		}

		public enum FileReferenceSheetNonStringColumns
		{
			[EnumType(typeof(DateTime))]
			[ColumnName("Inception Date")]
			InceptionDate,
			[EnumType(typeof(DateTime))]
			[ColumnName("Expiry Date")]
			ExpiryDate,
			[EnumType(typeof(decimal))]
			[ColumnName("Our Limit (Local Curr)")]
			LimitLocalAmount,
			[EnumType(typeof(decimal))]
			[ColumnName("NET3 ROL")]
			Net3RateOnline,
			[EnumType(typeof(decimal))]
			[ColumnName("Our Limit (USD)")]
			LimitUsdAmount,
		}
	}
}
