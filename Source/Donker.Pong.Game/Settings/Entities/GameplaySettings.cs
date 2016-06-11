using System;

namespace Donker.Pong.Game.Settings.Entities
{
    /// <summary>
    /// Contains the settings that are used when the game is in progress.
    /// </summary>
    public interface IGameplaySettings
    {
        /// <summary>
        /// Gets or sets whether the game speed should automatically be increased over time.
        /// </summary>
        bool AutoIncreaseSpeed { get; }
        /// <summary>
        /// Gets the game speed modifier.
        /// </summary>
        float GameSpeed { get; }
        /// <summary>
        /// Gets the type of the left paddle.
        /// </summary>
        Type LeftPaddleType { get; }
        /// <summary>
        /// Gets the type of the right paddle.
        /// </summary>
        Type RightPaddleType { get; }
        /// <summary>
        /// Gets number of balls to add to the game.
        /// </summary>
        int BallCount { get; }
        /// <summary>
        /// Gets the maximum number of points after which the game should end.
        /// </summary>
        int ScoreLimit { get; }
        /// <summary>
        /// Gets the maximum time after which the game should end.
        /// </summary>
        TimeSpan TimeLimit { get; }
        /// <summary>
        /// Gets whether a score limit has been set or not.
        /// </summary>
        bool HasScoreLimit { get; }
        /// <summary>
        /// Gets whether a time limit has been set or not.
        /// </summary>
        bool HasTimeLimit { get; }
    }

    /// <summary>
    /// Contains the settings that are used when the game is in progress.
    /// </summary>
    public class GameplaySettings : IGameplaySettings, IEquatable<GameplaySettings>, ICloneable
    {
        /// <summary>
        /// Gets or sets whether the game speed should automatically be increased over time.
        /// </summary>
        public bool AutoIncreaseSpeed { get; set; }
        /// <summary>
        /// Gets or sets the game speed modifier.
        /// </summary>
        public float GameSpeed { get; set; }
        /// <summary>
        /// Gets or sets the type of the left paddle.
        /// </summary>
        public Type LeftPaddleType { get; set; }
        /// <summary>
        /// Gets or sets the type of the right paddle.
        /// </summary>
        public Type RightPaddleType { get; set; }
        /// <summary>
        /// Gets or sets number of balls to add to the game.
        /// </summary>
        public int BallCount { get; set; }
        /// <summary>
        /// Gets or sets the maximum number of points after which the game should end.
        /// </summary>
        public int ScoreLimit { get; set; }
        /// <summary>
        /// Gets or sets the maximum time after which the game should end.
        /// </summary>
        public TimeSpan TimeLimit { get; set; }

        #region IGameplaySettings implementation

        /// <summary>
        /// Gets whether a score limit has been set or not.
        /// </summary>
        bool IGameplaySettings.HasScoreLimit { get { return ScoreLimit > 0; } }
        /// <summary>
        /// Gets whether a time limit has been set or not.
        /// </summary>
        bool IGameplaySettings.HasTimeLimit { get { return TimeLimit > TimeSpan.Zero; } }

        #endregion

        #region Generated equality members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(GameplaySettings other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return AutoIncreaseSpeed == other.AutoIncreaseSpeed && GameSpeed.Equals(other.GameSpeed) && Equals(LeftPaddleType, other.LeftPaddleType) && Equals(RightPaddleType, other.RightPaddleType) && BallCount == other.BallCount && TimeLimit.Equals(other.TimeLimit) && ScoreLimit == other.ScoreLimit;
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
            return Equals((GameplaySettings) obj);
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
                var hashCode = AutoIncreaseSpeed.GetHashCode();
                hashCode = (hashCode*397) ^ GameSpeed.GetHashCode();
                hashCode = (hashCode*397) ^ (LeftPaddleType != null ? LeftPaddleType.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (RightPaddleType != null ? RightPaddleType.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ BallCount;
                hashCode = (hashCode*397) ^ TimeLimit.GetHashCode();
                hashCode = (hashCode*397) ^ ScoreLimit;
                return hashCode;
            }
        }

        #endregion

        #region Cloning members

        /// <summary>
        /// Creates a new <see cref="GameplaySettings"/> object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new <see cref="GameplaySettings"/> object that is a copy of this instance.</returns>
        public GameplaySettings Clone()
        {
            return new GameplaySettings
            {
                AutoIncreaseSpeed = AutoIncreaseSpeed,
                GameSpeed = GameSpeed,
                LeftPaddleType = LeftPaddleType,
                RightPaddleType = RightPaddleType,
                BallCount = BallCount,
                ScoreLimit = ScoreLimit,
                TimeLimit = TimeLimit
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