using System.Security.Principal;

namespace Xperters.Core.Security
{
    public interface IUserContext
    {
        string LoginName { get; }
        string DomainName { get; }
        string FullLoginName { get; }

        bool IsAuthenticated { get; }
        string AuthenticationType { get; }

        bool IsGuest { get; }
        bool IsSystem { get; }

        bool IsImpersonated { get; }
        IUserContext OriginalUserContext { get; }

        IPrincipal Principal { get; }

        bool IsInRole(string role);
    }
}
