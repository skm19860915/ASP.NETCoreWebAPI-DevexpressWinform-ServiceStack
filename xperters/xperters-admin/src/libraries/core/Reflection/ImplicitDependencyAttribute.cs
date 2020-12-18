using System;

namespace Xperters.Core.Reflection
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class ImplicitDependencyAttribute : Attribute
    {
        public Type ImplicitlyUsedType { get; }

        public ImplicitDependencyAttribute(Type implicitlyUsedType)
        {
            ImplicitlyUsedType = implicitlyUsedType;
        }
    }
}
