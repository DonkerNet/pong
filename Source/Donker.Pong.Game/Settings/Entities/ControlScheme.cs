using System;
using Microsoft.Xna.Framework.Input;

namespace Donker.Pong.Game.Settings.Entities
{
    /// <summary>
    /// The controls for a paddle.
    /// </summary>
    public interface IControlScheme
    {
        /// <summary>
        /// Gets the key that moves a paddle upwards.
        /// </summary>
        Keys MoveUp { get; }
        /// <summary>
        /// Gets the key that moves a paddle downwards.
        /// </summary>
        Keys MoveDown { get; }
    }

    /// <summary>
    /// The controls for a paddle.
    /// </summary>
    public class ControlScheme : IControlScheme, IEquatable<ControlScheme>, ICloneable
    {
        /// <summary>
        /// Gets or sets the key that moves a paddle upwards.
        /// </summary>
        public Keys MoveUp { get; set; }
        /// <summary>
        /// Gets or sets the key that moves a paddle downwards.
        /// </summary>
        public Keys MoveDown { get; set; }

        #region Generated equality members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(ControlScheme other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return MoveUp == other.MoveUp && MoveDown == other.MoveDown;
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
            return Equals((ControlScheme) obj);
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
                return ((int) MoveUp*397) ^ (int) MoveDown;
            }
        }

        #endregion

        #region Cloning members

        /// <summary>
        /// Creates a new <see cref="ControlScheme"/> object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new <see cref="ControlScheme"/> object that is a copy of this instance.</returns>
        public ControlScheme Clone()
        {
            return new ControlScheme
            {
                MoveDown = MoveDown,
                MoveUp = MoveUp
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