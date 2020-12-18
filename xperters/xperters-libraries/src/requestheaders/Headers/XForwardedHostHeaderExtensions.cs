using System;

namespace xperters.requestheaders.Headers
{
    /// <summary>
    /// Extension methods for adding a <see cref="XForwardedHostHeader" /> to a <see cref="HeaderPolicyCollection" />
    /// </summary>
    public static class XForwardedHostHeaderExtensions
    {
        /// <summary>
        /// Add a custom header to all requests
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <param name="fullyQualifiedWebsite">The value to name of the public service that is hosting the website</param>
        /// <returns>The <see cref="HeaderPolicyCollection" /> for method chaining</returns>
        public static HeaderPolicyCollection AddXForwardedHost(this HeaderPolicyCollection policies, string fullyQualifiedWebsite)
        {
            if (string.IsNullOrEmpty(fullyQualifiedWebsite))
            {
                throw new ArgumentNullException(nameof(fullyQualifiedWebsite));
            }

            return policies.ApplyPolicy(new XForwardedHostHeader(fullyQualifiedWebsite));
        }
    }
}