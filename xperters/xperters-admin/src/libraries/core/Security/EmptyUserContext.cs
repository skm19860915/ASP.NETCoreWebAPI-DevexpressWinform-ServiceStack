using System.Security.Principal;

namespace Xperters.Core.Security
{
    public sealed class EmptyUserContext : IUserContext
    {
        private readonly IPrincipal _principal;

        public EmptyUserContext()
        {
            _principal = new UserContextPrincipal(this);
        }

        public string LoginName
        {
            get { return null; }
        }

        public string DomainName
        {
            get { return null; }
        }

        public string FullLoginName
        {
            get { return null; }
        }

        public bool IsAuthenticated
        {
            get { return false; }
        }

        public string AuthenticationType
        {
            get { return null; }
        }

        public bool IsGuest
        {
            get { return false; }
        }

        public bool IsSystem
        {
            get { return false; }
        }

        public bool IsImpersonated
        {
            get { return false; }
        }

        public IUserContext OriginalUserContext
        {
            get { return null; }
        }

        public IPrincipal Principal
        {
            get { return _principal; }
        }

        public bool IsInRole(string role)
        {
            return false;
        }
    }
}