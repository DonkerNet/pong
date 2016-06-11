using System;
using Donker.Pong.Common.Shapes;
using Microsoft.Xna.Framework;

namespace Donker.Pong.Common.Collisions
{
    /// <summary>
    /// Contains methods for performing 2D bounding box collision detection.
    /// </summary>
    public static class BoxCollisionDetection
    {
        /// <summary>
        /// Checks if a collider is out of bounds and outputs the amount of distance.
        /// </summary>
        /// <param name="collider">The collider to check.</param>
        /// <param name="bounds">The bounds in which the collider should stay.</param>
        /// <param name="outOfBoundsDistance">The amount of distance the collider is out of bounds.</param>
        /// <returns><value>true</value> if out of bounds; otherwise, <value>false</value>.</returns>
        public static bool CheckBoundsCollision(IBoxCollider collider, RectangleF bounds, out Vector2 outOfBoundsDistance)
        {
            float? outOfBoundsX = null;
            float? outOfBoundsY = null;

            // Add padding to determine the actual bounds
            float boundsLeft = bounds.Left + collider.BoundsPadding.X;
            float boundsRight = bounds.Right - collider.BoundsPadding.X;
            float boundsTop = bounds.Top + collider.BoundsPadding.Y;
            float boundsBottom = bounds.Bottom - collider.BoundsPadding.Y;

            // Determine the out of bounds distance on the X-axis
            if (collider.Hitbox.Left < boundsLeft)
                outOfBoundsX = -Math.Abs(boundsLeft - collider.Hitbox.Left);
            else if (collider.Hitbox.Right > boundsRight)
                outOfBoundsX = collider.Hitbox.Right - boundsRight;

            // Determine the out of bounds distance on the Y-axis
            if (collider.Hitbox.Top < boundsTop)
                outOfBoundsY = -Math.Abs(boundsTop - collider.Hitbox.Top);
            else if (collider.Hitbox.Bottom > boundsBottom)
                outOfBoundsY = collider.Hitbox.Bottom - boundsBottom;

            outOfBoundsDistance = new Vector2(outOfBoundsX ?? 0F, outOfBoundsY ?? 0F);
            return outOfBoundsDistance != Vector2.Zero;
        }

        /// <summary>
        /// Checks if a collision between two colliders happend and outputs the amount of distance the first collider intersects with the second.
        /// </summary>
        /// <param name="first">The main collider to check the collision for.</param>
        /// <param name="second">The second collider to check the collision with.</param>
        /// <param name="intersection">The amount of distance the first collider intersects with the second.</param>
        /// <returns><value>true</value> if a collision was detected; otherwise, <value>false</value>.</returns>
        public static bool CheckColliderCollision(IBoxCollider first, IBoxCollider second, out Vector2 intersection)
        {
            // The main collider can have a negative or positive intersection, based on the location of it's center relative to the center of the other collider.
            // This way, we can determine where a collision happened and how the collider's position should/could be adjusted.
            // For example: a positive X intersection would mean a collision happened on the left side of second collider and the intersection should be subtracted.

            float? intersectionX = null;
            float? intersectionY = null;

            Vector2 firstCenter = first.Hitbox.Center;
            Vector2 secondCenter = second.Hitbox.Center;

            if (firstCenter.X < secondCenter.X)
            {
                if (first.Hitbox.Right > second.Hitbox.Left)
                {
                    // Positive X intersection
                    intersectionX = first.Hitbox.Right - second.Hitbox.Left;
                }
            }
            else if (first.Hitbox.Left < second.Hitbox.Right)
            {
                // Negative X intersection
                intersectionX = first.Hitbox.Left - second.Hitbox.Right;
            }

            if (intersectionX.HasValue)
            {
                if (firstCenter.Y < secondCenter.Y)
                {
                    if (first.Hitbox.Bottom > second.Hitbox.Top)
                    {
                        // Positive Y intersection
                        intersectionY = first.Hitbox.Bottom - second.Hitbox.Top;
                    }
                }
                else if (first.Hitbox.Top < second.Hitbox.Bottom)
                {
                    // Negative Y intersection
                    intersectionY = first.Hitbox.Top - second.Hitbox.Bottom;
                }

                if (intersectionY.HasValue)
                {
                    intersection = new Vector2(intersectionX.Value, intersectionY.Value);
                    return true;
                }
            }

            intersection = Vector2.Zero;
            return false;
        }
    }
}