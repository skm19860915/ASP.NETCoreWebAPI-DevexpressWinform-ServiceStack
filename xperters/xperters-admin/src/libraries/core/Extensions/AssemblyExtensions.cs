using System;
using System.IO;
using System.Reflection;
using Xperters.Core.Reflection;

// ReSharper disable once CheckNamespace
namespace Xperters
{
    public static class AssemblyExtensions
    {
        public static string GetManifestResourceString(this Assembly assembly, string resourceName)
        {
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    return null;
                }

                using (var reader = new StreamReader(stream))
                {
                    var contents = reader.ReadToEnd();
                    return contents;
                }
            }
        }

        public static string GetInformationalVersion(this Assembly assembly, bool fallbackToAssemblyVersion = true)
        {
            var infoVersion = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            if (infoVersion != null)
            {
                return infoVersion.InformationalVersion;
            }

            if (fallbackToAssemblyVersion)
            {
                return assembly.GetName().Version.ToString();
            }

            return null;
        }

        public static string GetBuildVersion(this Assembly assembly, bool fallbackToInformationalVersionFirst = true, bool fallbackToAssemblyVersionSecond = true)
        {
            var buildVersionAttr = assembly.GetCustomAttribute<AssemblyBuildVersionAttribute>();
            if (buildVersionAttr != null)
            {
                return buildVersionAttr.BuildVersion;
            }

            if (fallbackToInformationalVersionFirst)
            {
                return assembly.GetInformationalVersion(fallbackToAssemblyVersion: fallbackToAssemblyVersionSecond);
            }

            if (fallbackToAssemblyVersionSecond)
            {
                return assembly.GetName().Version.ToString();
            }

            return null;
        }

        public static DateTimeOffset? GetBuildDateTimeOffset(this Assembly assembly)
        {
            var buildDateTimeOffsetAttr = assembly.GetCustomAttribute<AssemblyBuildDateTimeOffsetAttribute>();
            if (buildDateTimeOffsetAttr != null)
            {
                return buildDateTimeOffsetAttr.BuildDateTimeOffset;
            }

            return null;
        }

        public static string GetBuildBranchName(this Assembly assembly)
        {
            var buildBranchNameAttr = assembly.GetCustomAttribute<AssemblyBuildBranchNameAttribute>();
            if (buildBranchNameAttr != null)
            {
                return buildBranchNameAttr.BuildBranchName;
            }

            return null;
        }

        public static string GetBuildCommitHash(this Assembly assembly)
        {
            var buildCommitHashAttr = assembly.GetCustomAttribute<AssemblyBuildCommitHashAttribute>();
            if (buildCommitHashAttr != null)
            {
                return buildCommitHashAttr.CommitHash;
            }

            return null;
        }

        public static string GetBuildShortCommitHash(this Assembly assembly)
        {
            var buildCommitHashAttr = assembly.GetCustomAttribute<AssemblyBuildCommitHashAttribute>();
            if (buildCommitHashAttr != null)
            {
                return buildCommitHashAttr.ShortCommitHash;
            }

            return null;
        }
    }
}
