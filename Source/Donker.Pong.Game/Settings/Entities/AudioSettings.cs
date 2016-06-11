using System;

namespace Donker.Pong.Game.Settings.Entities
{
    /// <summary>
    /// Contains settings for the game's audio.
    /// </summary>
    public interface IAudioSettings
    {
        /// <summary>
        /// Gets whether the audio is enabled or not.
        /// </summary>
        bool Enabled { get; }
        /// <summary>
        /// Gets the volume for the audio from 0 to 100.
        /// </summary>
        int Volume { get; }
        /// <summary>
        /// Gets which sound effect set to use.
        /// </summary>
        int SfxSet { get; }
    }

    /// <summary>
    /// Contains settings for the game's audio.
    /// </summary>
    public class AudioSettings : IAudioSettings, IEquatable<AudioSettings>, ICloneable
    {
        /// <summary>
        /// Gets or sets whether the audio is enabled or not.
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// Gets or sets the volume for the audio from 0 to 100.
        /// </summary>
        public int Volume { get; set; }
        /// <summary>
        /// Gets or sets which sound effect set to use.
        /// </summary>
        public int SfxSet { get; set; }

        #region Generated equality members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(AudioSettings other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Enabled == other.Enabled && Volume == other.Volume && SfxSet == other.SfxSet;
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
            return Equals((AudioSettings) obj);
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
                var hashCode = Enabled.GetHashCode();
                hashCode = (hashCode * 397) ^ Volume.GetHashCode();
                hashCode = (hashCode * 397) ^ SfxSet.GetHashCode();
                return hashCode;
            }
        }

        #endregion

        #region Cloning members

        /// <summary>
        /// Creates a new <see cref="AudioSettings"/> object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new <see cref="AudioSettings"/> object that is a copy of this instance.</returns>
        public AudioSettings Clone()
        {
            return new AudioSettings
            {
                Enabled = Enabled,
                Volume = Volume,
                SfxSet = SfxSet
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