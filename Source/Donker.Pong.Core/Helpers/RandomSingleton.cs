using System;

namespace Donker.Pong.Common.Helpers
{
    /// <summary>
    /// Contains a static default <see cref="Random"/> instance.
    /// </summary>
    public static class RandomSingleton
    {
        /// <summary>
        /// Gets the <see cref="Random"/> instance.
        /// </summary>
        public static Random Instance { get { return Nested.NestedInstance; } }

        private static class Nested
        {
            public static readonly Random NestedInstance = new Random();
        }
    }
}