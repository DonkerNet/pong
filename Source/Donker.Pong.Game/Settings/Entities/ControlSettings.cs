using System;

namespace Donker.Pong.Game.Settings.Entities
{
    /// <summary>
    /// Contains the controls for the paddles.
    /// </summary>
    public interface IControlSettings
    {
        /// <summary>
        /// Gets  the controls for the left paddle.
        /// </summary>
        IControlScheme LeftPaddle { get; }
        /// <summary>
        /// Gets the controls for the right paddle.
        /// </summary>
        IControlScheme RightPaddle { get; }
    }

    /// <summary>
    /// Contains the controls for the paddles.
    /// </summary>
    public class ControlSettings : IControlSettings, IEquatable<ControlSettings>, ICloneable
    {
        /// <summary>
        /// Gets or sets the controls for the left paddle.
        /// </summary>
        public ControlScheme LeftPaddle { get; set; }
        /// <summary>
        /// Gets or sets the controls for the right paddle.
        /// </summary>
        public ControlScheme RightPaddle { get; set; }

        #region IControlSettings implementation

        /// <summary>
        /// Gets  the controls for the left paddle.
        /// </summary>
        IControlScheme IControlSettings.LeftPaddle { get { return LeftPaddle; } }
        /// <summary>
        /// Gets the controls for the right paddle.
        /// </summary>
        IControlScheme IControlSettings.RightPaddle { get { return RightPaddle; } }

        #endregion

        #region Generated equality members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(ControlSettings other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(LeftPaddle, other.LeftPaddle) && Equals(RightPaddle, other.RightPaddle);
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
            return Equals((ControlSettings) obj);
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
                return ((LeftPaddle != null ? LeftPaddle.GetHashCode() : 0)*397) ^ (RightPaddle != null ? RightPaddle.GetHashCode() : 0);
            }
        }

        #endregion

        #region Cloning members

        /// <summary>
        /// Creates a new <see cref="ControlSettings"/> object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new <see cref="ControlSettings"/> object that is a copy of this instance.</returns>
        public ControlSettings Clone()
        {
            return new ControlSettings
            {
                LeftPaddle = LeftPaddle != null ? LeftPaddle.Clone() : null,
                RightPaddle = RightPaddle != null ? RightPaddle.Clone() : null
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