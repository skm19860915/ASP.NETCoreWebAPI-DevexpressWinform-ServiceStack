using System.Collections.Generic;

namespace Xperters.Admin.ServiceModel.Constants
{
    public static class UserRoles
    {
        public const string ReadRole = "Xperters.Read";
        public const string WriteRole = "Xperters.Write";
        public const string AdminRole = "Xperters.Admin";

        public static IReadOnlyCollection<string> AllRoles => new[] { ReadRole, WriteRole, AdminRole };
        public static IReadOnlyCollection<string> RolesWithWritePermissions => new[] { WriteRole, AdminRole };
    }

}
