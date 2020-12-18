using System;

namespace Xperters.Core.Reflection
{
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    public sealed class AssemblyBuildBranchNameAttribute : Attribute
    {
        private readonly string _buildBranchName;

        public AssemblyBuildBranchNameAttribute(string buildBranchName)
        {
            _buildBranchName = buildBranchName;
        }

        public string BuildBranchName
        {
            get { return _buildBranchName; }
        }
    }
}