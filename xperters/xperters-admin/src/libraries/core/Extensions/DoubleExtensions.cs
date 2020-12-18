using System;

namespace Xperters.Core.Extensions
{
    public static class DoubleExtensions
    {
        /// <summary>
        /// returns true if 2 doubles are within 10-12.
        /// </summary>
        /// <param name="o">original value to compare</param>
        /// <param name="to">value to compare to</param>
        /// <returns>true if Math.Abs(to - o) <= 1e-12</returns>
        public static bool AlmostEqualTo(this double o, double to)
        {
            return Math.Abs(to - o) <= 1e-12;
        }

        /// <summary>
        /// returns true if 2 doubles are within precision
        /// </summary>
        /// <param name="o">original value to compare</param>
        /// <param name="to">value to compare to</param>
        /// <param name="precision">precision allowance</param>
        /// <returns>true if Math.Abs(to - o) <= precision</returns>
        public static bool AlmostEqualTo(this double o, double to, double precision)
        {
            return Math.Abs(to - o) <= precision;
        }
    }
}
