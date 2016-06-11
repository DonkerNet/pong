using System;
using System.Collections.Generic;

namespace Donker.Pong.Common.Extensions
{
    /// <summary>
    /// Contains extension methods for <see cref="IEnumerable{T}"/> collections.
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Performs the specified action on each element of the collection.
        /// </summary>
        /// <param name="source">The source to loop through.</param>
        /// <param name="action">The action to perform on each element of the collection.</param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null || action == null)
                return;
            
            foreach (T item in source)
            {
                action.Invoke(item);
            }
        }
    }
}