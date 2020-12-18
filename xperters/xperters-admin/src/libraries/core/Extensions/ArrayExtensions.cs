using System;
using System.Collections.Generic;

namespace Xperters.Core.Extensions
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Simple Transpose of data. Creates a new Array.
        /// </summary>
        public static T[,] Transpose<T>(this T[,] matrix)
        {
            int dimension = matrix.Rank;
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            T[,] tmatrix = new T[columns, rows];
            for (int row = 0; row < rows; row++)
                for (int col = 0; col < columns; col++)
                    tmatrix[col, row] = matrix[row, col];
            return tmatrix;
        }
        
        /// <summary>
        /// Extracst a row from the matrix array
        /// </summary>
        public static IEnumerable<T> SliceRow<T>(this T[,] array, int row)
        {
            if (row > array.GetLength(0))
                throw new ArgumentOutOfRangeException($"Tried to access row {row}, but only {array.GetLength(0)} rowns");
            if (row < array.GetLowerBound(0))
                throw new ArgumentOutOfRangeException($"Tried to access row {row}, but rows start at index {array.GetLowerBound(0)}");
            var x = new T[array.GetLength(1)];
            for (var col = array.GetLowerBound(1); col <= array.GetUpperBound(1); col++)
                yield return array[row, col];
        }

        /// <summary>
        /// Extracst a column from the matrix array
        /// </summary>
        public static IEnumerable<T> SliceColumn<T>(this T[,] array, int column)
        {
            if (column > array.GetLength(1))
                throw new ArgumentOutOfRangeException($"Tried to access column {column}, but only {array.GetLength(1)} columns");
            if (column < array.GetLowerBound(1))
                throw new ArgumentOutOfRangeException($"Tried to access column {column}, but columns start at index {array.GetLowerBound(1)}");
            var x = new T[array.GetLength(0)];
            for (var row = array.GetLowerBound(0); row <= array.GetUpperBound(0); row++)
                yield return array[row, column];
        }

        /// <summary>
        /// Converts data but keeps it in an array format. Proxy to Array.ConvertAll
        /// </summary>
        public static TOutput[] ConvertAll<TInput, TOutput>(this TInput[] array, Converter<TInput, TOutput> c)
        {
            return Array.ConvertAll(array, c);
        }

        public static int IndexOf<T>(this T[] array, T o)
        {
            return Array.IndexOf(array, o);
        }
    }
}
