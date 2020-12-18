using System;
using System.Security.Principal;

namespace Xperters.Core.Security
{
    public class UserContextPrincipal : IPrincipal
    {
        private readonly UserContextIdentity _identity;

        public UserContextPrincipal(IUserContext userContext)
            : this(new UserContextIdentity(userContext))
        {
        }

        public UserContextPrincipal(UserContextIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");                
            }

            _identity = identity;
        }

        public bool IsInRole(string role)
        {
            return _identity.Context.IsInRole(role);
        }

        public IIdentity Identity
        {
            get { return _identity; }
        }
    }
}