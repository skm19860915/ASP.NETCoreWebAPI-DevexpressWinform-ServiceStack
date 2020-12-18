using System;
using System.Globalization;

namespace Xperters.Core.Reflection
{
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    public sealed class AssemblyBuildDateTimeOffsetAttribute : Attribute
    {
        private readonly DateTimeOffset? _buildDateTimeOffset;

        public AssemblyBuildDateTimeOffsetAttribute(string buildDateTimeOffsetAsString)
        {
            DateTimeOffset buildDateTimeOffset;
            if (DateTimeOffset.TryParse(buildDateTimeOffsetAsString, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out buildDateTimeOffset))
            {
                _buildDateTimeOffset = buildDateTimeOffset;
            }
        }

        public DateTimeOffset? BuildDateTimeOffset
        {
            get { return _buildDateTimeOffset; }
        }
    }
}