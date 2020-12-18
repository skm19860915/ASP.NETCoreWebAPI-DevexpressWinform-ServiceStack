using System.Collections.Generic;
using xperters.requestheaders.Headers;

// ReSharper disable once CheckNamespace
namespace xperters.requestheaders
{
    /// <summary>
    /// Defines the policies to use for customising security headers for a request.
    /// </summary>
    public class HeaderPolicyCollection : Dictionary<string, IHeaderPolicy>
    {
    }
}