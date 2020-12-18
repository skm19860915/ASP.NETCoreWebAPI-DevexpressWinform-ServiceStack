using System;

namespace Xperters.Admin.UI.Common
{
	public class EnumTypeAttribute : Attribute
	{
		public readonly Type Type;
		public EnumTypeAttribute(Type type)
		{
			Type = type;
		}
	}
}
