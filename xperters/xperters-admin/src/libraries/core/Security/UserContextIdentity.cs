using System;
using System.Security.Principal;

namespace Xperters.Core.Security
{
    public class UserContextIdentity : IIdentity
    {
        private readonly IUserContext _userContext;

        public UserContextIdentity(IUserContext userContext)
        {
            if (userContext == null)
            {
                throw new ArgumentNullException("userContext");
            }

            _userContext = userContext;
        }

        public IUserContext Context
        {
            get { return _userContext; }
        }

        public string Name
        {
            get { return _userContext.FullLoginName; }
        }

        public string AuthenticationType
        {
            get { return _userContext.AuthenticationType; }
        }

        public bool IsAuthenticated
        {
            get { return _userContext.IsAuthenticated; }
        }
    }
}