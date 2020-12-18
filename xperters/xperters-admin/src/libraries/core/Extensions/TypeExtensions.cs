using System;

namespace Xperters.Core.Extensions
{
    public static class TypeExtensions
    {
        public static bool HasAttribute<T>(this Type type, bool inherit = false)
            where T : Attribute
        {
            return type.HasAttribute(typeof(T), inherit);
        }

        public static bool HasAttribute(this Type type, Type attributeType, bool inherit = false)
        {
            return type.GetCustomAttributes(attributeType, inherit).Length > 0;
        }
    }
}
