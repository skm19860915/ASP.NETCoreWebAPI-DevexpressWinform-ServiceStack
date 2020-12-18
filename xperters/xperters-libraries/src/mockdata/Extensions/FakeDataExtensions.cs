using System;

namespace xperters.mockdata.Extensions
{
    public static class FakeDataExtensions
    {
        
        private static readonly int[] GuidByteOrder =
            { 15, 14, 13, 12, 11, 10, 9, 8, 6, 7, 4, 5, 0, 1, 2, 3 };
        
        public static Guid Increment(this Guid guid)
        {
            var bytes = guid.ToByteArray();
            bool carry = true;
            for (int i = 0; i < GuidByteOrder.Length && carry; i++)
            {
                int index = GuidByteOrder[i];
                byte oldValue = bytes[index]++;
                carry = oldValue > bytes[index];
            }
            return new Guid(bytes);
        } 
        
        public static bool NextBool(this Random r, int truePercentage = 50)
        {
            return r.NextDouble() < truePercentage / 100.0;
        }        
    }
}