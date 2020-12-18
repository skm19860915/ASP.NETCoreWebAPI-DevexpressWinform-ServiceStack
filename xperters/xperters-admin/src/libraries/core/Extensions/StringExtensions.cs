namespace Xperters.Core.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveAnyWhitespace(this string s)
        {
            return s.Replace(" ", string.Empty).Replace("\t", string.Empty);
        }
    }
}
