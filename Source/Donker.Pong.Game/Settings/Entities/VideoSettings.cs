using System;
using Microsoft.Xna.Framework;

namespace Donker.Pong.Game.Settings.Entities
{
    /// <summary>
    /// Contains the video related settings of the game.
    /// </summary>
    public interface IVideoSettings
    {
        /// <summary>
        /// Gets whether the game should be shown in full screen or not.
        /// </summary>
        bool FullScreen { get; }
        /// <summary>
        /// Gets the resolution of the game as long as it's supported.
        /// </summary>
        Vector2 Resolution { get; }
        /// <summary>
        /// Gets whether vertical synchronization should be enabled or not.
        /// </summary>
        bool VSync { get; }
    }

    /// <summary>
    /// Contains the video related settings of the game.
    /// </summary>
    public class VideoSettings : IVideoSettings, IEquatable<VideoSettings>, ICloneable
    {
        /// <summary>
        /// Gets or sets whether the game should be shown in full screen or not.
        /// </summary>
        public bool FullScreen { get; set; }
        /// <summary>
        /// Gets or sets the resolution of the game as long as it's supported.
        /// </summary>
        public Vector2 Resolution { get; set; }
        /// <summary>
        /// Gets or sets whether vertical synchronization should be enabled or not.
        /// </summary>
        public bool VSync { get; set; }

        #region Generated equality members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(VideoSettings other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return FullScreen == other.FullScreen && Resolution.Equals(other.Resolution) && VSync == other.VSync;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((VideoSettings) obj);
        }

        /// <summary>
        /// Serves as the default hash function. 
        /// </summary>
        /// <returns>
        /// A hash code for the current object.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = FullScreen.GetHashCode();
                hashCode = (hashCode*397) ^ Resolution.GetHashCode();
                hashCode = (hashCode*397) ^ VSync.GetHashCode();
                return hashCode;
            }
        }

        #endregion

        #region Cloning members

        /// <summary>
        /// Creates a new <see cref="VideoSettings"/> object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new <see cref="VideoSettings"/> object that is a copy of this instance.</returns>
        public VideoSettings Clone()
        {
            return new VideoSettings
            {
                FullScreen = FullScreen,
                Resolution = Resolution,
                VSync = VSync
            };
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        object ICloneable.Clone()
        {
            return Clone();
        }

        #endregion
    }
}