using System;

namespace Xperters.Core.Reflection
{
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    public sealed class AssemblyBuildCommitHashAttribute : Attribute
    {
        private readonly string _commitHash;

        public AssemblyBuildCommitHashAttribute(string commitHash)
        {
            _commitHash = commitHash;
        }

        public string CommitHash
        {
            get { return _commitHash; }
        }

        public string ShortCommitHash
        {
            get
            {
                if (_commitHash == null)
                {
                    return null;
                }

                return _commitHash.Substring(0, Math.Min(7, _commitHash.Length));
            }
        }
    }
}