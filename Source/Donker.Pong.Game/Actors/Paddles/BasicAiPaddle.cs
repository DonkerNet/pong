using System;
using System.Linq;
using Donker.Pong.Common.Actors;
using Donker.Pong.Common.Extensions;
using Donker.Pong.Game.Actors.Balls;
using Donker.Pong.Game.Status;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Donker.Pong.Game.Actors.Paddles
{
    /// <summary>
    /// A paddle that is controlled by the computer.
    /// </summary>
    public class BasicAiPaddle : PaddleBase
    {
        // Cheap way of slowing down the paddle's speed so that it can't always stop the ball
        private const float DifficultySpeedFactor = 0.64F;

        private readonly ActorRegistry _actorRegistry;

        public BasicAiPaddle(GameInfo gameInfo, Texture2D texture, ActorRegistry actorRegistry, PaddleSide paddleSide, Vector2 size, Vector2 boundsPadding, float speed)
            : base(gameInfo, texture, paddleSide, size, boundsPadding, speed)
        {
            _actorRegistry = actorRegistry;
        }

        public override void Update(GameTime gameTime)
        {
            // Find the ball that is closest to the paddle and can still be saved
            Ball ball;

            if (PaddleSide == PaddleSide.Left)
            {
                // Padle is on the left, so find the closest ball moving left
                ball = _actorRegistry.GetAll()
                    .OfType<Ball>()
                    .Where(b => !b.IsResetting && b.Hitbox.Right > Hitbox.Left && (b.Angle.IsBetween(90, 270)))
                    .OrderBy(b => b.Hitbox.Left)
                    .FirstOrDefault();
            }
            else
            {
                // Padle is on the right, so find the closest ball moving right
                ball = _actorRegistry.GetAll()
                    .OfType<Ball>()
                    .Where(b => !b.IsResetting && b.Hitbox.Left < Hitbox.Right && (b.Angle.IsBetween(270, 360) || b.Angle.IsBetween(0, 90)))
                    .OrderByDescending(b => b.Hitbox.Right)
                    .FirstOrDefault();
            }

            // If a ball is moving towards the paddle, calculate which way the paddle should move to stop the ball
            if (ball != null)
            {
                float actualSpeed = DifficultySpeedFactor * (Speed * GameInfo.Speed);
                float distance = 0F;

                if (ball.Hitbox.Top < Hitbox.Top)
                {
                    // Ball is higher than the paddle, so move up
                    distance = Hitbox.Top - ball.Hitbox.Top;
                    if (distance > actualSpeed)
                        distance = actualSpeed;
                    distance = -distance;
                }
                else if (ball.Hitbox.Bottom > Hitbox.Bottom)
                {
                    // Ball is lower than the paddle, so move down
                    distance = ball.Hitbox.Bottom - Hitbox.Bottom;
                    if (distance > actualSpeed)
                        distance = actualSpeed;
                }

                // Move the paddle
                if (Math.Abs(distance) > 0F)
                    Hitbox.Offset(0F, distance);
            }
        }
    }
}