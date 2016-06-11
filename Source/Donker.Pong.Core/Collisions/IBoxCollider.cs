using Donker.Pong.Common.Shapes;
using Microsoft.Xna.Framework;

namespace Donker.Pong.Common.Collisions
{
    /// <summary>
    /// An interface for objects compatible with 2D bounding box collision detection.
    /// </summary>
    public interface IBoxCollider
    {
        /// <summary>
        /// Gets the hitbox of this collider.
        /// </summary>
        RectangleF Hitbox { get; }
        /// <summary>
        /// Gets the padding to apply when performing bounds collision detection.
        /// </summary>
        Vector2 BoundsPadding { get; }

        /// <summary>
        /// Method to call when a collision happened with another collider.
        /// </summary>
        /// <param name="otherCollider">The other collider that collided with this one.</param>
        /// <param name="intersection">The amount that this collider intersects with the other one.</param>
        void OnColliderCollision(IBoxCollider otherCollider, Vector2 intersection);
        /// <summary>
        /// Method to call when the collider has left certain bounds.
        /// </summary>
        /// <param name="outOfBoundsDistance">The out of bounds distance of the collider.</param>
        void OnBoundsCollision(Vector2 outOfBoundsDistance);
    }
}