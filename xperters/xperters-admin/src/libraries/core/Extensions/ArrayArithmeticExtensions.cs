using System;
using System.Linq;

namespace Xperters.Core.Extensions
{
    public static class ArrayArithmeticExtensions
    {
        public static double[] Plus(this double[] values, double d)
        {
            return values.ConvertAll(v => v + d);
        }

        public static double[] Plus(this double[] values, double[] other)
        {
            if (values.Length != other.Length)
                throw new ArgumentException($"Expected values[{values.Length}] to be the same size as other[{other.Length}].");
            return values.Zip(other, (v, o) => v + o).ToArray();
        }

        public static double[] Plus(this double[] values, params double[][] other)
        {
            var res = (double[])values.Clone();
            for (int i = 0; i < other.Length; i++)
                res = res.Plus(other[i]);
            return res;
        }

        public static double[,] Plus(this double[,] values, double[,] other)
        {
            if (values.Length != other.Length)
                throw new ArgumentException($"Expected values[{values.Length}] to be the same size as other[{other.Length}].");
            var res = new double[values.GetLength(0), values.GetLength(1)];
            for (int row = 0; row < values.GetLength(0); row++)
                for (int col = 0; col < values.GetLength(1); col++)
                    res[row, col] = values[row, col] + other[row, col];
            return res;
        }

        public static double[] Minus(this double[] values, double d)
        {
            return values.ConvertAll(v => v - d);
        }

        public static double[] Minus(this double[] values, double[] other)
        {
            if (values.Length != other.Length)
                throw new ArgumentException($"Expected values[{values.Length}] to be the same size as other[{other.Length}].");
            return values.Zip(other, (v, o) => v - o).ToArray();
        }

        public static double[,] Minus(this double[,] values, double[,] other)
        {
            if (values.Length != other.Length)
                throw new ArgumentException($"Expected values[{values.Length}] to be the same size as other[{other.Length}].");
            var res = new double[values.GetLength(0), values.GetLength(1)];
            for (int row = 0; row < values.GetLength(0); row++)
                for (int col = 0; col < values.GetLength(1); col++)
                    res[row, col] = values[row, col] - other[row, col];
            return res;
        }

        public static double[] Times(this double[] values, double d)
        {
            return values.ConvertAll(v => v * d);
        }

        public static double[] Times(this double[] values, double[] other)
        {
            if (values.Length != other.Length)
                throw new ArgumentException($"Expected values[{values.Length}] to be the same size as other[{other.Length}].");
            return values.Zip(other, (v, o) => v * o).ToArray();
        }

        public static double[] Times(this double[] values, bool[] other)
        {
            if (values.Length != other.Length)
                throw new ArgumentException($"Expected values[{values.Length}] to be the same size as other[{other.Length}].");
            return values.Zip(other, (v, o) => o ? v : 0.0).ToArray();
        }

        public static double[,] Times(this double[,] values, double[,] other)
        {
            if (values.Length != other.Length)
                throw new ArgumentException($"Expected values[{values.Length}] to be the same size as other[{other.Length}].");
            var res = new double[values.GetLength(0), values.GetLength(1)];
            for (int row = 0; row < values.GetLength(0); row++)
                for (int col = 0; col < values.GetLength(1); col++)
                    res[row, col] = values[row, col] * other[row, col];
            return res;
        }


        public static double[] DivideBy(this double[] values, double d)
        {
            if (d == 0.0)
                throw new DivideByZeroException();
            return values.ConvertAll(v => v / d);
        }

        public static double[] DivideBy(this double[] values, double[] other)
        {
            if (values.Length != other.Length)
                throw new ArgumentException($"Expected values[{values.Length}] to be the same size as other[{other.Length}].");
            return values.Zip(other, (v, o) => v / o).ToArray();
        }

        public static double[,] DivideBy(this double[,] values, double[,] other)
        {
            if (values.Length != other.Length)
                throw new ArgumentException($"Expected values[{values.Length}] to be the same size as other[{other.Length}].");
            var res = new double[values.GetLength(0), values.GetLength(1)];
            for (int row = 0; row < values.GetLength(0); row++)
                for (int col = 0; col < values.GetLength(1); col++)
                    res[row, col] = values[row, col] / other[row, col];
            return res;
        }

    }
}
