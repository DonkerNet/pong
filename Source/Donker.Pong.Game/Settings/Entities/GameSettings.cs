using System;

namespace Donker.Pong.Game.Settings.Entities
{
    /// <summary>
    /// Contains all settings for the game.
    /// </summary>
    public interface IGameSettings
    {
        /// <summary>
        /// Gets the audio settings of the game.
        /// </summary>
        IAudioSettings Audio { get; }
        /// <summary>
        /// Gets the video settings of the game.
        /// </summary>
        IVideoSettings Video { get; }
        /// <summary>
        /// Gets the settings that are used when the game is in progress.
        /// </summary>
        IGameplaySettings Gameplay { get; }
        /// <summary>
        /// Gets the control schemes for the player controller paddles.
        /// </summary>
        IControlSettings Controls { get; }
    }

    /// <summary>
    /// Contains all settings for the game.
    /// </summary>
    public class GameSettings : IGameSettings, IEquatable<GameSettings>, ICloneable
    {
        /// <summary>
        /// Gets or sets the audio settings of the game.
        /// </summary>
        public AudioSettings Audio { get; set; }
        /// <summary>
        /// Gets or sets the video settings of the game.
        /// </summary>
        public VideoSettings Video { get; set; }
        /// <summary>
        /// Gets or sets the settings that are used when the game is in progress.
        /// </summary>
        public GameplaySettings Gameplay { get; set; }
        /// <summary>
        /// Gets or sets the control schemes for the player controller paddles.
        /// </summary>
        public ControlSettings Controls { get; set; }

        #region IGameSettings implementation

        /// <summary>
        /// Gets the audio settings of the game.
        /// </summary>
        IAudioSettings IGameSettings.Audio { get { return Audio; } }
        /// <summary>
        /// Gets the video settings of the game.
        /// </summary>
        IVideoSettings IGameSettings.Video { get { return Video; } }
        /// <summary>
        /// Gets the settings that are used when the game is in progress.
        /// </summary>
        IGameplaySettings IGameSettings.Gameplay { get { return Gameplay; } }
        /// <summary>
        /// Gets the control schemes for the player controller paddles.
        /// </summary>
        IControlSettings IGameSettings.Controls { get { return Controls; } }

        #endregion

        #region Generated equality members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(GameSettings other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Audio, other.Audio) && Equals(Video, other.Video) && Equals(Gameplay, other.Gameplay) && Equals(Controls, other.Controls);
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
            return Equals((GameSettings) obj);
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
                var hashCode = (Audio != null ? Audio.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Video != null ? Video.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Gameplay != null ? Gameplay.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Controls != null ? Controls.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion

        #region Cloning members

        /// <summary>
        /// Creates a new <see cref="GameSettings"/> object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new <see cref="GameSettings"/> object that is a copy of this instance.</returns>
        public GameSettings Clone()
        {
            return new GameSettings
            {
                Audio = Audio != null ? Audio.Clone() : null,
                Video = Video != null ? Video.Clone() : null,
                Gameplay = Gameplay != null ? Gameplay.Clone() : null,
                Controls = Controls != null ? Controls.Clone() : null
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