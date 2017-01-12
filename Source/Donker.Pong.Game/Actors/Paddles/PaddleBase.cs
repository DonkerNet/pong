using Donker.Pong.Common.Actors;
using Donker.Pong.Common.Collisions;
using Donker.Pong.Common.Shapes;
using Donker.Pong.Game.Status;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Donker.Pong.Game.Actors.Paddles
{
    /// <summary>
    /// Base class for any paddle.
    /// </summary>
    public abstract class PaddleBase : IActor
    {
        private readonly Texture2D _texture;

        protected GameInfo GameInfo { get; private set; }
        protected float Speed { get; private set; }

        public PaddleSide PaddleSide { get; private set; }
        public RectangleF Hitbox { get; protected set; }
        public Vector2 BoundsPadding { get; private set; }

        protected PaddleBase(GameInfo gameInfo, Texture2D texture, PaddleSide paddleSide, Vector2 size, Vector2 boundsPadding, float speed)
        {
            float xPosition = paddleSide == PaddleSide.Left
                ? boundsPadding.X
                : gameInfo.Bounds.Right - boundsPadding.X - size.X;

            float yPosition = gameInfo.Bounds.Height / 2F - size.Y / 2F;

            _texture = texture;
            GameInfo = gameInfo;
            PaddleSide = paddleSide;
            Speed = speed;
            Hitbox = new RectangleF(xPosition, yPosition, size.X, size.Y);
            BoundsPadding = boundsPadding;
        }

        public abstract void Update(GameTime gameTime);

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, destinationRectangle: Hitbox);
        }

        public void OnColliderCollision(IBoxCollider otherCollider, Vector2 intersection)
        {
            // Do nothing, since the ball will adjust itself instead
        }

        public void OnBoundsCollision(Vector2 outOfBoundsDistance)
        {
            // Adjust position
            Hitbox.Offset(-outOfBoundsDistance);
        }
    }
}