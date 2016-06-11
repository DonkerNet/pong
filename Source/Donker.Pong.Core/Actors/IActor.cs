using Donker.Pong.Common.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Donker.Pong.Common.Actors
{
    /// <summary>
    /// Interface for any entity that interacts with the game.
    /// </summary>
    public interface IActor : IBoxCollider
    {
        /// <summary>
        /// Updates the actor.
        /// </summary>
        /// <param name="gameTime">Information about the game's timings.</param>
        void Update(GameTime gameTime);
        /// <summary>
        /// Draws the actor.
        /// </summary>
        /// <param name="spriteBatch">The spritebatch to use when drawing the actor.</param>
        void Draw(SpriteBatch spriteBatch);
    }
}