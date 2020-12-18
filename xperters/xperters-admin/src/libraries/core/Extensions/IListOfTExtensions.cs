using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Xperters
{
    // ReSharper disable once InconsistentNaming
    public static class IListOfTExtensions
    {
        public static void RemoveAll<T>(this IList<T> source, Predicate<T> predicate)
        {
            var itemsToRemove = new List<T>();

            foreach (T item in source)
            {
                if (predicate(item))
                {
                    itemsToRemove.Add(item);
                }
            }

            foreach (T obj in itemsToRemove)
            {
                source.Remove(obj);
            }

            itemsToRemove.Clear();
        }
    }
}
