using System.Collections.Generic;

namespace Xperters.Core.Text
{
    public class NaturalStringComparer : IComparer<string>
    {
        public int Compare(string strA, string strB)
        {
            if (strA == null && strB == null)
            {
                return 0;
            }

            if (strA == null)
            {
                return -1;
            }

            if (strB == null)
            {
                return 1;
            }

            var lengthOfStrA = strA.Length;
            var lengthOfStrB = strB.Length;

            for (int indexStrA = 0, indexStrB = 0; indexStrA < lengthOfStrA && indexStrB < lengthOfStrB; indexStrA++, indexStrB++)
            {
                if (char.IsDigit(strA[indexStrA]) && char.IsDigit(strB[indexStrB]))
                {
                    long numericValueStrA = 0;
                    long numericValueStrB = 0;

                    for (; indexStrA < lengthOfStrA && char.IsDigit(strA[indexStrA]); indexStrA++)
                    {
                        numericValueStrA = numericValueStrA * 10 + strA[indexStrA] - '0';
                    }

                    for (; indexStrB < lengthOfStrB && char.IsDigit(strB[indexStrB]); indexStrB++)
                    {
                        numericValueStrB = numericValueStrB * 10 + strB[indexStrB] - '0';
                    }

                    if (numericValueStrA != numericValueStrB)
                    {
                        return numericValueStrA > numericValueStrB ? 1 : -1;
                    }
                }

                if (indexStrA < lengthOfStrA && indexStrB < lengthOfStrB && strA[indexStrA] != strB[indexStrB])
                {
                    return strA[indexStrA] > strB[indexStrB] ? 1 : -1;
                }
            }

            return lengthOfStrA - lengthOfStrB;
        }
    }
}
