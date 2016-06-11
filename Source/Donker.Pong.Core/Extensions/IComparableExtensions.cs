using System;

namespace Donker.Pong.Common.Extensions
{
    /// <summary>
    /// Contains extension methods for <see cref="IComparable{T}"/> objects.
    /// </summary>
    public static class IComparableExtensions
    {
        /// <summary>
        /// Checks whether a value is between a minimum and maximum value, while specifying if the minimum and maximum values themselves should be included in the comparison.
        /// </summary>
        /// <typeparam name="T">The type of the value to check.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="min">The value to check must be higher than this.</param>
        /// <param name="max">The value to check must be lower than this.</param>
        /// <param name="inclusive">Whether to include the minimum and maximum values themselves in the comparison.</param>
        /// <returns><c>true</c> if the value is between the minimum and maximum values; otherwise, <c>false</c>.</returns>
        public static bool IsBetween<T>(this T value, T min, T max, bool inclusive)
            where T : IComparable<T>
        {
            return inclusive
                ? value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0
                : value.CompareTo(min) > 0 && value.CompareTo(max) < 0;
        }

        /// <summary>
        /// Checks whether a value is higher than the minimum value and lower than the maximum.
        /// </summary>
        /// <typeparam name="T">The type of the value to check.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="min">The value to check must be higher than this.</param>
        /// <param name="max">The value to check must be lower than this.</param>
        /// <returns><c>true</c> if the value is between the minimum and maximum values; otherwise, <c>false</c>.</returns>
        public static bool IsBetween<T>(this T value, T min, T max)
            where T : IComparable<T>
        {
            return IsBetween(value, min, max, false);
        }

        /// <summary>
        /// Clamps a value between a minimum and maximum value.
        /// </summary>
        /// <typeparam name="T">The type of the value to clamp.</typeparam>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value that is allowed.</param>
        /// <param name="max">The maximum value that is allowed.</param>
        /// <returns><paramref name="min"/> if the value is lower, <paramref name="max"/> if higher; otherwise, the <paramref name="value"/> is returned.</returns>
        public static T Clamp<T>(this T value, T min, T max)
            where T : IComparable<T>
        {
            if (value.CompareTo(min) < 0)
                return min;
            if (value.CompareTo(max) > 0)
                return max;
            return value;
        }
    }
}