using System;
using System.Security.Principal;

namespace Xperters.Core.Security
{
    public class ImpersonatedUserContext : IUserContext
    {
        private readonly IUserContext _originalUserContext;
        private readonly IUserContext _impersonatedUserContext;
        private readonly IPrincipal _principal;

        public ImpersonatedUserContext(IUserContext originalUserContext, IUserContext impersonatedUserContext)
        {
            if (originalUserContext == null)
            {
                throw new ArgumentNullException("originalUserContext");
            }

            if (impersonatedUserContext == null)
            {
                throw new ArgumentNullException("impersonatedUserContext");
            }

            _originalUserContext = originalUserContext;
            _impersonatedUserContext = impersonatedUserContext;
            _principal = new UserContextPrincipal(this);
        }

        public string LoginName
        {
            get { return _impersonatedUserContext.LoginName; }
        }

        public string DomainName
        {
            get { return _impersonatedUserContext.DomainName; }
        }

        public string FullLoginName
        {
            get { return _impersonatedUserContext.FullLoginName; }
        }

        public bool IsAuthenticated
        {
            get { return _impersonatedUserContext.IsAuthenticated; }
        }

        public string AuthenticationType
        {
            get { return _impersonatedUserContext.AuthenticationType; }
        }

        public bool IsGuest
        {
            get { return _impersonatedUserContext.IsGuest; }
        }

        public bool IsSystem
        {
            get { return _impersonatedUserContext.IsSystem; }
        }

        public bool IsImpersonated
        {
            get { return true; }
        }

        public IUserContext OriginalUserContext
        {
            get { return _originalUserContext; }
        }

        public IPrincipal Principal
        {
            get { return _principal; }
        }

        public bool IsInRole(string role)
        {
            return _impersonatedUserContext.IsInRole(role);
        }
    }
}