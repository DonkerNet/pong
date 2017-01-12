using System;

namespace Donker.Pong.Common.Helpers
{
    /// <summary>
    /// Contains a static default <see cref="Random"/> instance.
    /// </summary>
    public static class RandomSingleton
    {
        private static readonly Lazy<Random> LazyInstance = new Lazy<Random>(() => new Random());

        /// <summary>
        /// Gets the <see cref="Random"/> instance.
        /// </summary>
        public static Random Instance { get { return LazyInstance.Value; } }
    }
}