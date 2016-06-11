/*
 * The following is a class, based on the Microsoft.Xna.Framework.Rectangle struct from the MonoGame framework.
 * It uses the float and Vector2 types instead of int and Point.
 * It also contains some extra methods for compatibility with the original Microsoft.Xna.Framework.Rectangle struct.
 */

using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;

namespace Donker.Pong.Common.Shapes
{
    /// <summary>
    /// Describes a 2D-rectangle, using <see cref="float"/> and <see cref="Vector2"/> values for better precision.
    /// </summary>
    [DataContract]
    [DebuggerDisplay("{DebugDisplayString,nq}")]
    public class RectangleF : IEquatable<RectangleF>, IEquatable<Rectangle>
    {
        #region Private Fields

        private static RectangleF emptyRectangle = new RectangleF();

        #endregion

        #region Public Fields

        /// <summary>
        /// The x coordinate of the top-left corner of this <see cref="RectangleF"/>.
        /// </summary>
        [DataMember]
        public float X;

        /// <summary>
        /// The y coordinate of the top-left corner of this <see cref="RectangleF"/>.
        /// </summary>
        [DataMember]
        public float Y;

        /// <summary>
        /// The width of this <see cref="RectangleF"/>.
        /// </summary>
        [DataMember]
        public float Width;

        /// <summary>
        /// The height of this <see cref="RectangleF"/>.
        /// </summary>
        [DataMember]
        public float Height;

        #endregion

        #region Public Properties

        /// <summary>
        /// Returns a <see cref="RectangleF"/> with X=0, Y=0, Width=0, Height=0.
        /// </summary>
        public static RectangleF Empty
        {
            get { return emptyRectangle; }
        }

        /// <summary>
        /// Returns the x coordinate of the left edge of this <see cref="RectangleF"/>.
        /// </summary>
        public float Left
        {
            get { return this.X; }
        }

        /// <summary>
        /// Returns the x coordinate of the right edge of this <see cref="RectangleF"/>.
        /// </summary>
        public float Right
        {
            get { return (this.X + this.Width); }
        }

        /// <summary>
        /// Returns the y coordinate of the top edge of this <see cref="RectangleF"/>.
        /// </summary>
        public float Top
        {
            get { return this.Y; }
        }

        /// <summary>
        /// Returns the y coordinate of the bottom edge of this <see cref="RectangleF"/>.
        /// </summary>
        public float Bottom
        {
            get { return (this.Y + this.Height); }
        }

        /// <summary>
        /// Whether or not this <see cref="RectangleF"/> has a <see cref="Width"/> and
        /// <see cref="Height"/> of 0, and a <see cref="Location"/> of (0, 0).
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return ((((this.Width.Equals(0F)) && (this.Height.Equals(0F))) && (this.X.Equals(0F))) && (this.Y.Equals(0F)));
            }
        }

        /// <summary>
        /// The top-left coordinates of this <see cref="RectangleF"/>.
        /// </summary>
        public Vector2 Location
        {
            get
            {
                return new Vector2(this.X, this.Y);
            }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        /// <summary>
        /// The width-height coordinates of this <see cref="RectangleF"/>.
        /// </summary>
        public Vector2 Size
        {
            get
            {
                return new Vector2(this.Width, this.Height);
            }
            set
            {
                Width = value.X;
                Height = value.Y;
            }
        }

        /// <summary>
        /// A <see cref="Vector2"/> located in the center of this <see cref="RectangleF"/>.
        /// </summary>
        /// <remarks>
        /// If <see cref="Width"/> or <see cref="Height"/> is an odd number,
        /// the center point will be rounded down.
        /// </remarks>
        public Vector2 Center
        {
            get
            {
                return new Vector2(this.X + (this.Width / 2F), this.Y + (this.Height / 2F));
            }
        }

        #endregion

        #region Internal Properties

        internal string DebugDisplayString
        {
            get
            {
                return string.Concat(
                    this.X, "  ",
                    this.Y, "  ",
                    this.Width, "  ",
                    this.Height
                    );
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of <see cref="RectangleF"/> class.
        /// </summary>
        public RectangleF()
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="RectangleF"/> class, with the specified
        /// position, width, and height.
        /// </summary>
        /// <param name="x">The x coordinate of the top-left corner of the created <see cref="RectangleF"/>.</param>
        /// <param name="y">The y coordinate of the top-left corner of the created <see cref="RectangleF"/>.</param>
        /// <param name="width">The width of the created <see cref="RectangleF"/>.</param>
        /// <param name="height">The height of the created <see cref="RectangleF"/>.</param>
        public RectangleF(float x, float y, float width, float height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// Creates a new instance of <see cref="RectangleF"/> class, with the specified
        /// position, width, and height.
        /// </summary>
        /// <param name="x">The x coordinate of the top-left corner of the created <see cref="RectangleF"/>.</param>
        /// <param name="y">The y coordinate of the top-left corner of the created <see cref="RectangleF"/>.</param>
        /// <param name="width">The width of the created <see cref="RectangleF"/>.</param>
        /// <param name="height">The height of the created <see cref="RectangleF"/>.</param>
        public RectangleF(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// Creates a new instance of <see cref="RectangleF"/> class, with the specified
        /// location and size.
        /// </summary>
        /// <param name="location">The x and y coordinates of the top-left corner of the created <see cref="RectangleF"/>.</param>
        /// <param name="size">The width and height of the created <see cref="RectangleF"/>.</param>
        public RectangleF(Vector2 location, Vector2 size)
        {
            this.X = location.X;
            this.Y = location.Y;
            this.Width = size.X;
            this.Height = size.Y;
        }

        /// <summary>
        /// Creates a new instance of <see cref="RectangleF"/> class, with the specified
        /// location and size.
        /// </summary>
        /// <param name="location">The x and y coordinates of the top-left corner of the created <see cref="RectangleF"/>.</param>
        /// <param name="size">The width and height of the created <see cref="RectangleF"/>.</param>
        public RectangleF(Point location, Point size)
        {
            this.X = location.X;
            this.Y = location.Y;
            this.Width = size.X;
            this.Height = size.Y;
        }

        /// <summary>
        /// Creates a new instance of <see cref="RectangleF"/> class, from the specified <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="rectangle">The <see cref="RectangleF"/> containing the x and y coordinates of the top-left corner and the width and height, of the created <see cref="RectangleF"/>.</param>
        public RectangleF(RectangleF rectangle)
        {
            this.X = rectangle.X;
            this.Y = rectangle.Y;
            this.Width = rectangle.X;
            this.Height = rectangle.Y;
        }

        /// <summary>
        /// Creates a new instance of <see cref="RectangleF"/> class, from the specified <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="rectangle">The <see cref="Rectangle"/> containing the x and y coordinates of the top-left corner and the width and height, of the created <see cref="RectangleF"/>.</param>
        public RectangleF(Rectangle rectangle)
        {
            this.X = rectangle.X;
            this.Y = rectangle.Y;
            this.Width = rectangle.X;
            this.Height = rectangle.Y;
        }

        /// <summary>
        /// Creates a new instance of <see cref="RectangleF"/> class, from the specified <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="rectangle">The <see cref="Rectangle"/> containing the x and y coordinates of the top-left corner and the width and height, of the created <see cref="RectangleF"/>.</param>
        public RectangleF(ref Rectangle rectangle)
        {
            this.X = rectangle.X;
            this.Y = rectangle.Y;
            this.Width = rectangle.X;
            this.Height = rectangle.Y;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Compares whether two <see cref="RectangleF"/> instances are equal.
        /// </summary>
        /// <param name="a"><see cref="RectangleF"/> instance on the left of the equal sign.</param>
        /// <param name="b"><see cref="RectangleF"/> instance on the right of the equal sign.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public static bool operator ==(RectangleF a, RectangleF b)
        {
            if (ReferenceEquals(null, a))
                return ReferenceEquals(null, b);
            if (ReferenceEquals(null, b))
                return false;
            return ((a.X.Equals(b.X)) && (a.Y.Equals(b.Y)) && (a.Width.Equals(b.Width)) && (a.Height.Equals(b.Height)));
        }

        /// <summary>
        /// Compares whether a <see cref="RectangleF"/> and <see cref="Rectangle"/> instance are equal.
        /// </summary>
        /// <param name="a"><see cref="RectangleF"/> instance on the left of the equal sign.</param>
        /// <param name="b"><see cref="Rectangle"/> instance on the right of the equal sign.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public static bool operator ==(RectangleF a, Rectangle b)
        {
            if (ReferenceEquals(null, a))
                return false;
            return ((a.X.Equals(b.X)) && (a.Y.Equals(b.Y)) && (a.Width.Equals(b.Width)) && (a.Height.Equals(b.Height)));
        }

        /// <summary>
        /// Compares whether a <see cref="Rectangle"/> and <see cref="RectangleF"/> instance are equal.
        /// </summary>
        /// <param name="a"><see cref="Rectangle"/> instance on the left of the equal sign.</param>
        /// <param name="b"><see cref="RectangleF"/> instance on the right of the equal sign.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public static bool operator ==(Rectangle a, RectangleF b)
        {
            if (ReferenceEquals(null, b))
                return false;
            return ((b.X.Equals(a.X)) && (b.Y.Equals(a.Y)) && (b.Width.Equals(a.Width)) && (b.Height.Equals(a.Height)));
        }

        /// <summary>
        /// Compares whether two <see cref="RectangleF"/> instances are not equal.
        /// </summary>
        /// <param name="a"><see cref="RectangleF"/> instance on the left of the not equal sign.</param>
        /// <param name="b"><see cref="RectangleF"/> instance on the right of the not equal sign.</param>
        /// <returns><c>true</c> if the instances are not equal; <c>false</c> otherwise.</returns>
        public static bool operator !=(RectangleF a, RectangleF b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Compares whether a <see cref="RectangleF"/> and <see cref="Rectangle"/> instance are not equal.
        /// </summary>
        /// <param name="a"><see cref="RectangleF"/> instance on the left of the not equal sign.</param>
        /// <param name="b"><see cref="Rectangle"/> instance on the right of the not equal sign.</param>
        /// <returns><c>true</c> if the instances are not equal; <c>false</c> otherwise.</returns>
        public static bool operator !=(RectangleF a, Rectangle b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Compares whether a <see cref="Rectangle"/> and <see cref="RectangleF"/> instance are not equal.
        /// </summary>
        /// <param name="a"><see cref="Rectangle"/> instance on the left of the not equal sign.</param>
        /// <param name="b"><see cref="RectangleF"/> instance on the right of the not equal sign.</param>
        /// <returns><c>true</c> if the instances are not equal; <c>false</c> otherwise.</returns>
        public static bool operator !=(Rectangle a, RectangleF b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Converts a <see cref="RectangleF"/> to a <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="value">The <see cref="RectangleF"/> to convert.</param>
        public static implicit operator Rectangle(RectangleF value)
        {
            return new Rectangle((int)value.X, (int)value.Y, (int)value.Width, (int)value.Height);
        }

        /// <summary>
        /// Converts a <see cref="Rectangle"/> to a <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="value">The <see cref="Rectangle"/> to convert.</param>
        public static implicit operator RectangleF(Rectangle value)
        {
            return new RectangleF(value.X, value.Y, value.Width, value.Height);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets whether or not the provided coordinates lie within the bounds of this <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="x">The x coordinate of the point to check for containment.</param>
        /// <param name="y">The y coordinate of the point to check for containment.</param>
        /// <returns><c>true</c> if the provided coordinates lie inside this <see cref="RectangleF"/>; <c>false</c> otherwise.</returns>
        public bool Contains(int x, int y)
        {
            return ((((this.X <= x) && (x < (this.X + this.Width))) && (this.Y <= y)) && (y < (this.Y + this.Height)));
        }

        /// <summary>
        /// Gets whether or not the provided coordinates lie within the bounds of this <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="x">The x coordinate of the point to check for containment.</param>
        /// <param name="y">The y coordinate of the point to check for containment.</param>
        /// <returns><c>true</c> if the provided coordinates lie inside this <see cref="RectangleF"/>; <c>false</c> otherwise.</returns>
        public bool Contains(float x, float y)
        {
            return ((((this.X <= x) && (x < (this.X + this.Width))) && (this.Y <= y)) && (y < (this.Y + this.Height)));
        }

        /// <summary>
        /// Gets whether or not the provided <see cref="Point"/> lies within the bounds of this <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="value">The coordinates to check for inclusion in this <see cref="RectangleF"/>.</param>
        /// <returns><c>true</c> if the provided <see cref="Point"/> lies inside this <see cref="RectangleF"/>; <c>false</c> otherwise.</returns>
        public bool Contains(Point value)
        {
            return ((((this.X <= value.X) && (value.X < (this.X + this.Width))) && (this.Y <= value.Y)) && (value.Y < (this.Y + this.Height)));
        }

        /// <summary>
        /// Gets whether or not the provided <see cref="Point"/> lies within the bounds of this <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="value">The coordinates to check for inclusion in this <see cref="RectangleF"/>.</param>
        /// <param name="result"><c>true</c> if the provided <see cref="Point"/> lies inside this <see cref="RectangleF"/>; <c>false</c> otherwise. As an output parameter.</param>
        public void Contains(ref Point value, out bool result)
        {
            result = ((((this.X <= value.X) && (value.X < (this.X + this.Width))) && (this.Y <= value.Y)) && (value.Y < (this.Y + this.Height)));
        }

        /// <summary>
        /// Gets whether or not the provided <see cref="Vector2"/> lies within the bounds of this <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="value">The coordinates to check for inclusion in this <see cref="RectangleF"/>.</param>
        /// <returns><c>true</c> if the provided <see cref="Vector2"/> lies inside this <see cref="RectangleF"/>; <c>false</c> otherwise.</returns>
        public bool Contains(Vector2 value)
        {
            return ((((this.X <= value.X) && (value.X < (this.X + this.Width))) && (this.Y <= value.Y)) && (value.Y < (this.Y + this.Height)));
        }

        /// <summary>
        /// Gets whether or not the provided <see cref="Vector2"/> lies within the bounds of this <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="value">The coordinates to check for inclusion in this <see cref="RectangleF"/>.</param>
        /// <param name="result"><c>true</c> if the provided <see cref="Vector2"/> lies inside this <see cref="RectangleF"/>; <c>false</c> otherwise. As an output parameter.</param>
        public void Contains(ref Vector2 value, out bool result)
        {
            result = ((((this.X <= value.X) && (value.X < (this.X + this.Width))) && (this.Y <= value.Y)) && (value.Y < (this.Y + this.Height)));
        }

        /// <summary>
        /// Gets whether or not the provided <see cref="Rectangle"/> lies within the bounds of this <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="value">The <see cref="Rectangle"/> to check for inclusion in this <see cref="RectangleF"/>.</param>
        /// <returns><c>true</c> if the provided <see cref="Rectangle"/>'s bounds lie entirely inside this <see cref="RectangleF"/>; <c>false</c> otherwise.</returns>
        public bool Contains(Rectangle value)
        {
            return ((((this.X <= value.X) && ((value.X + value.Width) <= (this.X + this.Width))) && (this.Y <= value.Y)) && ((value.Y + value.Height) <= (this.Y + this.Height)));
        }

        /// <summary>
        /// Gets whether or not the provided <see cref="Rectangle"/> lies within the bounds of this <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="value">The <see cref="Rectangle"/> to check for inclusion in this <see cref="RectangleF"/>.</param>
        /// <param name="result"><c>true</c> if the provided <see cref="Rectangle"/>'s bounds lie entirely inside this <see cref="RectangleF"/>; <c>false</c> otherwise. As an output parameter.</param>
        public void Contains(ref Rectangle value, out bool result)
        {
            result = ((((this.X <= value.X) && ((value.X + value.Width) <= (this.X + this.Width))) && (this.Y <= value.Y)) && ((value.Y + value.Height) <= (this.Y + this.Height)));
        }

        /// <summary>
        /// Gets whether or not the provided <see cref="RectangleF"/> lies within the bounds of this <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="value">The <see cref="RectangleF"/> to check for inclusion in this <see cref="RectangleF"/>.</param>
        /// <returns><c>true</c> if the provided <see cref="RectangleF"/>'s bounds lie entirely inside this <see cref="RectangleF"/>; <c>false</c> otherwise.</returns>
        public bool Contains(RectangleF value)
        {
            if (ReferenceEquals(null, value))
                return false;
            return ((((this.X <= value.X) && ((value.X + value.Width) <= (this.X + this.Width))) && (this.Y <= value.Y)) && ((value.Y + value.Height) <= (this.Y + this.Height)));
        }

        /// <summary>
        /// Gets whether or not the provided <see cref="RectangleF"/> lies within the bounds of this <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="value">The <see cref="RectangleF"/> to check for inclusion in this <see cref="RectangleF"/>.</param>
        /// <param name="result"><c>true</c> if the provided <see cref="RectangleF"/>'s bounds lie entirely inside this <see cref="RectangleF"/>; <c>false</c> otherwise. As an output parameter.</param>
        public void Contains(RectangleF value, out bool result)
        {
            if (ReferenceEquals(null, value))
            {
                result = false;
                return;
            }
            result = ((((this.X <= value.X) && ((value.X + value.Width) <= (this.X + this.Width))) && (this.Y <= value.Y)) && ((value.Y + value.Height) <= (this.Y + this.Height)));
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            return ((obj is RectangleF) && this == ((RectangleF)obj))
                || (obj is Rectangle) && this == ((Rectangle)obj);
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="other">The <see cref="Rectangle"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public bool Equals(Rectangle other)
        {
            return this == other;
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="other">The <see cref="RectangleF"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public bool Equals(RectangleF other)
        {
            return this == other;
        }

        /// <summary>
        /// Gets the hash code of this <see cref="RectangleF"/>.
        /// </summary>
        /// <returns>Hash code of this <see cref="RectangleF"/>.</returns>
        public override int GetHashCode()
        {
            return ((int)X ^ (int)Y ^ (int)Width ^ (int)Height);
        }

        /// <summary>
        /// Adjusts the edges of this <see cref="RectangleF"/> by specified horizontal and vertical amounts. 
        /// </summary>
        /// <param name="horizontalAmount">Value to adjust the left and right edges.</param>
        /// <param name="verticalAmount">Value to adjust the top and bottom edges.</param>
        public void Inflate(int horizontalAmount, int verticalAmount)
        {
            X -= horizontalAmount;
            Y -= verticalAmount;
            Width += horizontalAmount * 2F;
            Height += verticalAmount * 2F;
        }

        /// <summary>
        /// Adjusts the edges of this <see cref="RectangleF"/> by specified horizontal and vertical amounts. 
        /// </summary>
        /// <param name="horizontalAmount">Value to adjust the left and right edges.</param>
        /// <param name="verticalAmount">Value to adjust the top and bottom edges.</param>
        public void Inflate(float horizontalAmount, float verticalAmount)
        {
            X -= horizontalAmount;
            Y -= verticalAmount;
            Width += horizontalAmount * 2F;
            Height += verticalAmount * 2F;
        }

        /// <summary>
        /// Gets whether or not the other <see cref="RectangleF"/> intersects with this rectangle.
        /// </summary>
        /// <param name="value">The other rectangle for testing.</param>
        /// <returns><c>true</c> if other <see cref="RectangleF"/> intersects with this rectangle; <c>false</c> otherwise.</returns>
        public bool Intersects(RectangleF value)
        {
            if (ReferenceEquals(null, value))
                return false;
            return value.Left < Right &&
                   Left < value.Right &&
                   value.Top < Bottom &&
                   Top < value.Bottom;
        }

        /// <summary>
        /// Gets whether or not the other <see cref="Rectangle"/> intersects with this rectangle.
        /// </summary>
        /// <param name="value">The other rectangle for testing.</param>
        /// <returns><c>true</c> if other <see cref="Rectangle"/> intersects with this rectangle; <c>false</c> otherwise.</returns>
        public bool Intersects(Rectangle value)
        {
            return value.Left < Right &&
                   Left < value.Right &&
                   value.Top < Bottom &&
                   Top < value.Bottom;
        }

        /// <summary>
        /// Gets whether or not the other <see cref="RectangleF"/> intersects with this rectangle.
        /// </summary>
        /// <param name="value">The other rectangle for testing.</param>
        /// <param name="result"><c>true</c> if other <see cref="RectangleF"/> intersects with this rectangle; <c>false</c> otherwise. As an output parameter.</param>
        public void Intersects(RectangleF value, out bool result)
        {
            if (ReferenceEquals(null, value))
            {
                result = false;
                return;
            }
            result = value.Left < Right &&
                     Left < value.Right &&
                     value.Top < Bottom &&
                     Top < value.Bottom;
        }

        /// <summary>
        /// Gets whether or not the other <see cref="Rectangle"/> intersects with this rectangle.
        /// </summary>
        /// <param name="value">The other rectangle for testing.</param>
        /// <param name="result"><c>true</c> if other <see cref="Rectangle"/> intersects with this rectangle; <c>false</c> otherwise. As an output parameter.</param>
        public void Intersects(ref Rectangle value, out bool result)
        {
            result = value.Left < Right &&
                     Left < value.Right &&
                     value.Top < Bottom &&
                     Top < value.Bottom;
        }

        /// <summary>
        /// Creates a new <see cref="RectangleF"/> that contains overlapping region of two other rectangles.
        /// </summary>
        /// <param name="value1">The first <see cref="RectangleF"/>.</param>
        /// <param name="value2">The second <see cref="RectangleF"/>.</param>
        /// <returns>Overlapping region of the two rectangles.</returns>
        public static RectangleF Intersect(RectangleF value1, RectangleF value2)
        {
            RectangleF rectangle;
            Intersect(value1, value2, out rectangle);
            return rectangle;
        }

        /// <summary>
        /// Creates a new <see cref="RectangleF"/> that contains overlapping region of two other rectangles.
        /// </summary>
        /// <param name="value1">The first <see cref="RectangleF"/>.</param>
        /// <param name="value2">The second <see cref="Rectangle"/>.</param>
        /// <returns>Overlapping region of the two rectangles.</returns>
        public static RectangleF Intersect(RectangleF value1, Rectangle value2)
        {
            RectangleF rectangle;
            Intersect(value1, ref value2, out rectangle);
            return rectangle;
        }

        /// <summary>
        /// Creates a new <see cref="RectangleF"/> that contains overlapping region of two other rectangles.
        /// </summary>
        /// <param name="value1">The first <see cref="Rectangle"/>.</param>
        /// <param name="value2">The second <see cref="RectangleF"/>.</param>
        /// <returns>Overlapping region of the two rectangles.</returns>
        public static RectangleF Intersect(Rectangle value1, RectangleF value2)
        {
            RectangleF rectangle;
            Intersect(ref value1, value2, out rectangle);
            return rectangle;
        }

        /// <summary>
        /// Creates a new <see cref="RectangleF"/> that contains overlapping region of two other rectangles.
        /// </summary>
        /// <param name="value1">The first <see cref="Rectangle"/>.</param>
        /// <param name="value2">The second <see cref="Rectangle"/>.</param>
        /// <returns>Overlapping region of the two rectangles.</returns>
        public static RectangleF Intersect(Rectangle value1, Rectangle value2)
        {
            RectangleF rectangle;
            Intersect(ref value1, ref value2, out rectangle);
            return rectangle;
        }

        /// <summary>
        /// Creates a new <see cref="RectangleF"/> that contains overlapping region of two other rectangles.
        /// </summary>
        /// <param name="value1">The first <see cref="RectangleF"/>.</param>
        /// <param name="value2">The second <see cref="RectangleF"/>.</param>
        /// <param name="result">Overlapping region of the two rectangles as an output parameter.</param>
        public static void Intersect(RectangleF value1, RectangleF value2, out RectangleF result)
        {
            if (value1.Intersects(value2))
            {
                float right_side = Math.Min(value1.X + value1.Width, value2.X + value2.Width);
                float left_side = Math.Max(value1.X, value2.X);
                float top_side = Math.Max(value1.Y, value2.Y);
                float bottom_side = Math.Min(value1.Y + value1.Height, value2.Y + value2.Height);
                result = new RectangleF(left_side, top_side, right_side - left_side, bottom_side - top_side);
            }
            else
            {
                result = new RectangleF(0F, 0F, 0F, 0F);
            }
        }

        /// <summary>
        /// Creates a new <see cref="RectangleF"/> that contains overlapping region of two other rectangles.
        /// </summary>
        /// <param name="value1">The first <see cref="RectangleF"/>.</param>
        /// <param name="value2">The second <see cref="Rectangle"/>.</param>
        /// <param name="result">Overlapping region of the two rectangles as an output parameter.</param>
        public static void Intersect(RectangleF value1, ref Rectangle value2, out RectangleF result)
        {
            if (value1.Intersects(value2))
            {
                float right_side = Math.Min(value1.X + value1.Width, value2.X + value2.Width);
                float left_side = Math.Max(value1.X, value2.X);
                float top_side = Math.Max(value1.Y, value2.Y);
                float bottom_side = Math.Min(value1.Y + value1.Height, value2.Y + value2.Height);
                result = new RectangleF(left_side, top_side, right_side - left_side, bottom_side - top_side);
            }
            else
            {
                result = new RectangleF(0F, 0F, 0F, 0F);
            }
        }

        /// <summary>
        /// Creates a new <see cref="RectangleF"/> that contains overlapping region of two other rectangles.
        /// </summary>
        /// <param name="value1">The first <see cref="Rectangle"/>.</param>
        /// <param name="value2">The second <see cref="RectangleF"/>.</param>
        /// <param name="result">Overlapping region of the two rectangles as an output parameter.</param>
        public static void Intersect(ref Rectangle value1, RectangleF value2, out RectangleF result)
        {
            if (value2.Intersects(value1))
            {
                float right_side = Math.Min(value1.X + value1.Width, value2.X + value2.Width);
                float left_side = Math.Max(value1.X, value2.X);
                float top_side = Math.Max(value1.Y, value2.Y);
                float bottom_side = Math.Min(value1.Y + value1.Height, value2.Y + value2.Height);
                result = new RectangleF(left_side, top_side, right_side - left_side, bottom_side - top_side);
            }
            else
            {
                result = new RectangleF(0F, 0F, 0F, 0F);
            }
        }

        /// <summary>
        /// Creates a new <see cref="RectangleF"/> that contains overlapping region of two other rectangles.
        /// </summary>
        /// <param name="value1">The first <see cref="Rectangle"/>.</param>
        /// <param name="value2">The second <see cref="Rectangle"/>.</param>
        /// <param name="result">Overlapping region of the two rectangles as an output parameter.</param>
        public static void Intersect(ref Rectangle value1, ref Rectangle value2, out RectangleF result)
        {
            if (value1.Intersects(value2))
            {
                int right_side = Math.Min(value1.X + value1.Width, value2.X + value2.Width);
                int left_side = Math.Max(value1.X, value2.X);
                int top_side = Math.Max(value1.Y, value2.Y);
                int bottom_side = Math.Min(value1.Y + value1.Height, value2.Y + value2.Height);
                result = new RectangleF(left_side, top_side, right_side - left_side, bottom_side - top_side);
            }
            else
            {
                result = new RectangleF(0F, 0F, 0F, 0F);
            }
        }

        /// <summary>
        /// Changes the <see cref="Location"/> of this <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="offsetX">The x coordinate to add to this <see cref="RectangleF"/>.</param>
        /// <param name="offsetY">The y coordinate to add to this <see cref="RectangleF"/>.</param>
        public void Offset(int offsetX, int offsetY)
        {
            X += offsetX;
            Y += offsetY;
        }

        /// <summary>
        /// Changes the <see cref="Location"/> of this <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="offsetX">The x coordinate to add to this <see cref="RectangleF"/>.</param>
        /// <param name="offsetY">The y coordinate to add to this <see cref="RectangleF"/>.</param>
        public void Offset(float offsetX, float offsetY)
        {
            X += offsetX;
            Y += offsetY;
        }

        /// <summary>
        /// Changes the <see cref="Location"/> of this <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="amount">The x and y components to add to this <see cref="RectangleF"/>.</param>
        public void Offset(Point amount)
        {
            X += amount.X;
            Y += amount.Y;
        }

        /// <summary>
        /// Changes the <see cref="Location"/> of this <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="amount">The x and y components to add to this <see cref="RectangleF"/>.</param>
        public void Offset(Vector2 amount)
        {
            X += amount.X;
            Y += amount.Y;
        }

        /// <summary>
        /// Returns a <see cref="String"/> representation of this <see cref="RectangleF"/> in the format:
        /// {X:[<see cref="X"/>] Y:[<see cref="Y"/>] Width:[<see cref="Width"/>] Height:[<see cref="Height"/>]}
        /// </summary>
        /// <returns><see cref="String"/> representation of this <see cref="RectangleF"/>.</returns>
        public override string ToString()
        {
            return "{X:" + X + " Y:" + Y + " Width:" + Width + " Height:" + Height + "}";
        }

        /// <summary>
        /// Creates a new <see cref="RectangleF"/> that completely contains two other rectangles.
        /// </summary>
        /// <param name="value1">The first <see cref="RectangleF"/>.</param>
        /// <param name="value2">The second <see cref="RectangleF"/>.</param>
        /// <returns>The union of the two rectangles.</returns>
        public static RectangleF Union(RectangleF value1, RectangleF value2)
        {
            float x = Math.Min(value1.X, value2.X);
            float y = Math.Min(value1.Y, value2.Y);
            return new RectangleF(x, y,
                                 Math.Max(value1.Right, value2.Right) - x,
                                     Math.Max(value1.Bottom, value2.Bottom) - y);
        }

        /// <summary>
        /// Creates a new <see cref="RectangleF"/> that completely contains two other rectangles.
        /// </summary>
        /// <param name="value1">The first <see cref="Rectangle"/>.</param>
        /// <param name="value2">The second <see cref="RectangleF"/>.</param>
        /// <returns>The union of the two rectangles.</returns>
        public static RectangleF Union(Rectangle value1, RectangleF value2)
        {
            float x = Math.Min(value1.X, value2.X);
            float y = Math.Min(value1.Y, value2.Y);
            return new RectangleF(x, y,
                                 Math.Max(value1.Right, value2.Right) - x,
                                     Math.Max(value1.Bottom, value2.Bottom) - y);
        }

        /// <summary>
        /// Creates a new <see cref="RectangleF"/> that completely contains two other rectangles.
        /// </summary>
        /// <param name="value1">The first <see cref="RectangleF"/>.</param>
        /// <param name="value2">The second <see cref="Rectangle"/>.</param>
        /// <returns>The union of the two rectangles.</returns>
        public static RectangleF Union(RectangleF value1, Rectangle value2)
        {
            float x = Math.Min(value1.X, value2.X);
            float y = Math.Min(value1.Y, value2.Y);
            return new RectangleF(x, y,
                                 Math.Max(value1.Right, value2.Right) - x,
                                     Math.Max(value1.Bottom, value2.Bottom) - y);
        }

        /// <summary>
        /// Creates a new <see cref="RectangleF"/> that completely contains two other rectangles.
        /// </summary>
        /// <param name="value1">The first <see cref="Rectangle"/>.</param>
        /// <param name="value2">The second <see cref="Rectangle"/>.</param>
        /// <returns>The union of the two rectangles.</returns>
        public static RectangleF Union(Rectangle value1, Rectangle value2)
        {
            int x = Math.Min(value1.X, value2.X);
            int y = Math.Min(value1.Y, value2.Y);
            return new RectangleF(x, y,
                                 Math.Max(value1.Right, value2.Right) - x,
                                     Math.Max(value1.Bottom, value2.Bottom) - y);
        }

        /// <summary>
        /// Creates a new <see cref="RectangleF"/> that completely contains two other rectangles.
        /// </summary>
        /// <param name="value1">The first <see cref="RectangleF"/>.</param>
        /// <param name="value2">The second <see cref="RectangleF"/>.</param>
        /// <param name="result">The union of the two rectangles as an output parameter.</param>
        public static void Union(RectangleF value1, RectangleF value2, out RectangleF result)
        {
            result = new RectangleF();
            result.X = Math.Min(value1.X, value2.X);
            result.Y = Math.Min(value1.Y, value2.Y);
            result.Width = Math.Max(value1.Right, value2.Right) - result.X;
            result.Height = Math.Max(value1.Bottom, value2.Bottom) - result.Y;
        }

        /// <summary>
        /// Creates a new <see cref="RectangleF"/> that completely contains two other rectangles.
        /// </summary>
        /// <param name="value1">The first <see cref="Rectangle"/>.</param>
        /// <param name="value2">The second <see cref="RectangleF"/>.</param>
        /// <param name="result">The union of the two rectangles as an output parameter.</param>
        public static void Union(ref Rectangle value1, RectangleF value2, out RectangleF result)
        {
            result = new RectangleF();
            result.X = Math.Min(value1.X, value2.X);
            result.Y = Math.Min(value1.Y, value2.Y);
            result.Width = Math.Max(value1.Right, value2.Right) - result.X;
            result.Height = Math.Max(value1.Bottom, value2.Bottom) - result.Y;
        }

        /// <summary>
        /// Creates a new <see cref="RectangleF"/> that completely contains two other rectangles.
        /// </summary>
        /// <param name="value1">The first <see cref="RectangleF"/>.</param>
        /// <param name="value2">The second <see cref="Rectangle"/>.</param>
        /// <param name="result">The union of the two rectangles as an output parameter.</param>
        public static void Union(RectangleF value1, ref Rectangle value2, out RectangleF result)
        {
            result = new RectangleF();
            result.X = Math.Min(value1.X, value2.X);
            result.Y = Math.Min(value1.Y, value2.Y);
            result.Width = Math.Max(value1.Right, value2.Right) - result.X;
            result.Height = Math.Max(value1.Bottom, value2.Bottom) - result.Y;
        }

        /// <summary>
        /// Creates a new <see cref="RectangleF"/> that completely contains two other rectangles.
        /// </summary>
        /// <param name="value1">The first <see cref="Rectangle"/>.</param>
        /// <param name="value2">The second <see cref="Rectangle"/>.</param>
        /// <param name="result">The union of the two rectangles as an output parameter.</param>
        public static void Union(ref Rectangle value1, ref Rectangle value2, out RectangleF result)
        {
            result = new RectangleF();
            result.X = Math.Min(value1.X, value2.X);
            result.Y = Math.Min(value1.Y, value2.Y);
            result.Width = Math.Max(value1.Right, value2.Right) - result.X;
            result.Height = Math.Max(value1.Bottom, value2.Bottom) - result.Y;
        }

        #endregion
    }
}