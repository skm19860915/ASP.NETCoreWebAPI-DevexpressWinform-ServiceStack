using System;

namespace xperters.enums
{
    [AttributeUsage(AttributeTargets.All)]
    public class ProcessOrderAttribute : Attribute
    {
        public ProcessOrderAttribute(int order)
        {
            Order = order;
        }

        // Define Name property.
        // This is a read-only attribute.

        public virtual int Order { get; }
    }
}