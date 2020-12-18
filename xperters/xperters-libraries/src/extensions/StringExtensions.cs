using System;

namespace xperters.extensions
{
    public static class StringExtensions
    {
        public static bool IsBlank(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNotBlank(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }
        public static bool IsBlankOrUnknown(this string value)
        {
            return string.IsNullOrEmpty(value) || value.Equals("Unknown", StringComparison.InvariantCultureIgnoreCase);
        }

        public static string ReplaceHostName(this string originalUrl, string newHostName)
        {
            var builder = new UriBuilder(originalUrl) {Host = newHostName};
            var uri = builder.Uri.ToString();
            return uri;
        }
    }
}
