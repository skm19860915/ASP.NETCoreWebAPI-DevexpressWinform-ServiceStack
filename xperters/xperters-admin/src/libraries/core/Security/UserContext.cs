using System;

namespace Xperters.Core.Security
{
    /// <summary>
    /// An optional static entry point for user context information that can be easily referenced
    /// by different parts of an application. To configure the <see cref="T:Xperters.Core.Security.UserContext" />
    /// set the Current static property to a user context instance.
    /// </summary>
    public static class UserContext
    {
        private static IUserContext _userContext = new EmptyUserContext();
        private static readonly object _syncLock = new object();

        /// <summary>The globally-shared user context.</summary>
        /// <exception cref="T:System.ArgumentNullException"></exception>
        public static IUserContext Current
        {
            get => _userContext;

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                lock (_syncLock)
                {
                    _userContext = value;
                }
            }
        }
    }
}