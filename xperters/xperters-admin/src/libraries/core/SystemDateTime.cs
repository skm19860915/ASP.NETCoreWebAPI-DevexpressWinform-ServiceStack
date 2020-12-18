using System;
using System.Globalization;

namespace Xperters.Core
{
    public class SystemDateTime : IDateTime
    {
        public int Compare(DateTime t1, DateTime t2)
        {
            return DateTime.Compare(t1, t2);
        }

        public int DaysInMonth(int year, int month)
        {
            return DateTime.DaysInMonth(year, month);
        }

        public bool Equals(DateTime t1, DateTime t2)
        {
            return DateTime.Equals(t1, t2);
        }

        public DateTime FromBinary(long dateData)
        {
            return DateTime.FromBinary(dateData);
        }

        public DateTime FromFileTime(long fileTime)
        {
            return DateTime.FromFileTime(fileTime);
        }

        public DateTime FromFileTimeUtc(long fileTime)
        {
            return DateTime.FromFileTimeUtc(fileTime);
        }

        public DateTime FromOADate(double d)
        {
            return DateTime.FromOADate(d);
        }

        public DateTime SpecifyKind(DateTime value, DateTimeKind kind)
        {
            return DateTime.SpecifyKind(value, kind);
        }

        public bool IsLeapYear(int year)
        {
            return DateTime.IsLeapYear(year);
        }

        public DateTime Parse(string s)
        {
            return DateTime.Parse(s);
        }

        public DateTime Parse(string s, IFormatProvider provider)
        {
            return DateTime.Parse(s, provider);
        }

        public DateTime Parse(string s, IFormatProvider provider, DateTimeStyles style)
        {
            return DateTime.Parse(s, provider, style);
        }

        public DateTime ParseExact(string s, string format, IFormatProvider provider)
        {
            return DateTime.ParseExact(s, format, provider);
        }

        public DateTime ParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style)
        {
            return DateTime.ParseExact(s, format, provider, style);
        }

        public DateTime ParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style)
        {
            return DateTime.ParseExact(s, formats, provider, style);
        }

        public bool TryParse(string s, out DateTime result)
        {
            return DateTime.TryParse(s, out result);
        }

        public bool TryParse(string s, IFormatProvider provider, DateTimeStyles style, out DateTime result)
        {
            return DateTime.TryParse(s, provider, style, out result);
        }

        public bool TryParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style, out DateTime result)
        {
            return DateTime.TryParseExact(s, format, provider, style, out result);
        }

        public bool TryParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style, out DateTime result)
        {
            return DateTime.TryParseExact(s, formats, provider, style, out result);
        }

        public DateTime Now
        {
            get { return DateTime.Now; }
        }
        
        public DateTime UtcNow
        {
            get { return DateTime.UtcNow; }
        }

        public DateTime Today
        {
            get { return DateTime.Today; }
        }
    }
}