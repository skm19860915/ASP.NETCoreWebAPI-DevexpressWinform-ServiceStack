using System;

namespace Xperters.Core.Reflection
{
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    public sealed class AssemblyBuildVersionAttribute : Attribute
    {
        private readonly string _buildVersion;

        public AssemblyBuildVersionAttribute(string buildVersion)
        {
            _buildVersion = buildVersion;
        }

        public string BuildVersion
        {
            get { return _buildVersion; }
        }
    }
}
