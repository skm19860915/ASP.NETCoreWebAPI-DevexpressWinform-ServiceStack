using System;

public static class FeesExtensions{
    
    public static int FindFeeBand(this decimal value, decimal[] bandLowerValues)
    {
        for (int i = 0; i < bandLowerValues.Length; ++i)
            if (value < bandLowerValues[i])
                return Math.Max(0, i-1);

        return bandLowerValues.Length;
    }        
}