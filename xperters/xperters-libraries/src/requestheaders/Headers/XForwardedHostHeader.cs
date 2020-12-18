using Microsoft.AspNetCore.Http;

namespace xperters.requestheaders.Headers
{
    /// <summary>
    /// The header value to use for Strict-Transport-Security
    /// </summary>
    public class XForwardedHostHeader : HeaderPolicyBase
    {
        /// <summary>
        /// The number of seconds in one year
        /// </summary>
        private readonly string _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="XForwardedHostHeader"/> class.
        /// </summary>
        /// <param name="value">The value to apply for the header</param>
        public XForwardedHostHeader(string value)
        {
            _value = value;
        }

        /// <inheritdoc />
        public override string Header { get; } = "X-Forwarded-Host";

        /// <inheritdoc />
        protected override string GetValue(HttpContext context) => _value;
    }
}