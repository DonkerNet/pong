using System;
using Donker.Pong.Common.Actors;
using Donker.Pong.Common.Collisions;
using Donker.Pong.Common.Extensions;
using Donker.Pong.Common.Helpers;
using Donker.Pong.Common.Shapes;
using Donker.Pong.Game.Actors.Paddles;
using Donker.Pong.Game.Audio;
using Donker.Pong.Game.Settings;
using Donker.Pong.Game.Status;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Donker.Pong.Game.Actors.Balls
{
    /// <summary>
    /// The ball. What else is there to say about it?  :)
    /// </summary>
    public class Ball : IActor
    {
        private readonly GameInfo _gameInfo;
        private readonly float _speed;
        private readonly Random _random;
        private readonly TimeSpan _maxResetTime;
        private readonly AudioManager _audioManager;
        private readonly Texture2D _texture;
        
        private Vector2 _movementVector;
        private float _angle;
        private TimeSpan _resetTime;

        public RectangleF Hitbox { get; protected set; }
        public Vector2 BoundsPadding { get; private set; }
        public bool IsResetting { get; private set; }

        public float Angle
        {
            get { return _angle; }
            set
            {
                _angle = value % 360F; // Mod used to avoid angles over 359 degrees
                if (_angle < 0F)
                    _angle += 360F; // Avoids negative angles
                UpdateMovementVector();
            }
        }

        public Ball(GameInfo gameInfo, AudioManager audioManager, Texture2D texture, Vector2 size, Vector2 boundsPadding, float speed)
        {
            _gameInfo = gameInfo;
            _audioManager = audioManager;
            _speed = speed;
            _random = RandomSingleton.Instance;
            _maxResetTime = TimeSpan.FromSeconds(2);
            _texture = texture;

            Hitbox = new RectangleF(0F, 0F, size.X, size.Y);
            BoundsPadding = boundsPadding;

            Reset(PaddleSide.Undetermined);
        }

        public void OnColliderCollision(IBoxCollider otherCollider, Vector2 intersection)
        {
            PaddleBase paddle = otherCollider as PaddleBase;
            
            if (paddle == null)
                return; // Only check collisions with paddles
            
            if (paddle.PaddleSide == PaddleSide.Left && (Angle.IsBetween(270, 360, true) || Angle.IsBetween(0, 90, true)))
                return; // Ignore a left paddle collision if the ball is already facing right
            if (paddle.PaddleSide == PaddleSide.Right && (Angle.IsBetween(90, 270, true)))
                return; // Ignore a right paddle collision if the ball is already facing left

            /*
             * When the ball hit a player paddle we need to bounce it back based on the location of the paddle.
             * The further the Y center of the ball is to the Y center of the paddle, the larger the angle will be.
             * 
             * For example:
             * If the ball's Y center is above the paddle's, it will bounce towards the top of the screen.
             * If it's below, it will bounce towards the bottom of the screen.
             */

            float degreesPerPixel = SettingsConstants.BallAngleRange / otherCollider.Hitbox.Height;

            // Left paddle collision
            if (paddle.PaddleSide == PaddleSide.Left)
            {
                // Adjust the angle
                const float startAngle = SettingsConstants.BallRightDirectionAngleStart + (SettingsConstants.BallAngleRange / 2F);
                Angle = startAngle + (Hitbox.Center.Y - otherCollider.Hitbox.Center.Y) * degreesPerPixel;
            }
            // Right paddle collision
            else
            {
                // Adjust the angle
                const float startAngle = SettingsConstants.BallLeftDirectionAngleStart + (SettingsConstants.BallAngleRange / 2F);
                Angle = startAngle + (otherCollider.Hitbox.Center.Y - Hitbox.Center.Y) * degreesPerPixel;
            }

            _audioManager.PlaySfx("BallPaddleHit");
        }

        public void OnBoundsCollision(Vector2 outOfBoundsDistance)
        {
            // The ball has left the screen on the left or right, so it should be reset
            if (outOfBoundsDistance.Y.Equals(0F))
            {
                bool hasLeftTheScreen = Math.Abs(outOfBoundsDistance.X) >= Hitbox.Width;

                if (hasLeftTheScreen)
                {
                    _audioManager.PlaySfx("BallReset");

                    // Determine the side that scored and to reset the ball to
                    PaddleSide paddleSide;
                    if (outOfBoundsDistance.X < 0F)
                    {
                        paddleSide = PaddleSide.Right;
                        ++_gameInfo.RightScore;
                    }
                    else
                    {
                        paddleSide = PaddleSide.Left;
                        ++_gameInfo.LeftScore;
                    }

                    Reset(paddleSide);
                }
            }

            // The ball only hit the top or bottom of the screen, so it should bounce if it is not doing that already
            else
            {
                bool bounceTop = outOfBoundsDistance.Y < 0F && Angle.IsBetween(180, 360);
                bool bounceBottom = outOfBoundsDistance.Y > 0F && Angle.IsBetween(0, 180);

                if (bounceTop || bounceBottom)
                {
                    // 'Bounce' the ball from the edge
                    double angleRadians = Math.Atan2(-_movementVector.Y, _movementVector.X);
                    Angle = MathHelper.ToDegrees((float)angleRadians);

                    _audioManager.PlaySfx("BallEdgeHit");
                }
            }
        }

        // Resets the ball for a paddle to serve
        public void Reset(PaddleSide paddleSide)
        {
            // Pick a random side (left or right) for the ball to start if no side was specified
            if (paddleSide == PaddleSide.Undetermined)
                paddleSide = (PaddleSide)_random.Next(1, 3);

            float xPosition = 0F;  // The X position of the center of the ball to set
            float minAngle = 0F;    // The minimum angle of the ball to set

            // Determine the base values for the position and angle
            switch (paddleSide)
            {
                case PaddleSide.Left:
                    xPosition = _gameInfo.Bounds.Width * 0.25F;
                    minAngle = SettingsConstants.BallRightDirectionAngleStart;
                    break;

                case PaddleSide.Right:
                    xPosition = (_gameInfo.Bounds.Width * 0.75F) - Hitbox.Width;
                    minAngle = SettingsConstants.BallLeftDirectionAngleStart;
                    break;
            }

            // Adjust the ball's position
            Hitbox.X = xPosition;
            Hitbox.Y = _gameInfo.Bounds.Height / 2F - Hitbox.Height / 2F;

            // Adjust the ball's angle
            float randomDegrees = _random.Next(0, SettingsConstants.BallAngleRange);
            Angle = minAngle + randomDegrees;

            IsResetting = true;
        }

        public void Update(GameTime gameTime)
        {
            if (_gameInfo.State != GameState.InProgress)
                return;

            // While resetting, do nothing to the ball
            if (IsResetting)
            {
                _resetTime += gameTime.ElapsedGameTime;

                if (_resetTime < _maxResetTime)
                    return;

                IsResetting = false;
                _resetTime = TimeSpan.Zero;
            }

            if (_gameInfo.State == GameState.InProgress)
            {
                // Adjusts the position based on the movement vector that was calculated
                Hitbox.Offset(
                    _movementVector.X * _gameInfo.Speed,
                    _movementVector.Y * _gameInfo.Speed);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_gameInfo.State != GameState.InProgress)
                return;

            spriteBatch.Draw(_texture, destinationRectangle: Hitbox);
        }

        private void UpdateMovementVector()
        {
            // Calculates the distance the ball shoud move based on it's speed and angle

            double x = Math.Cos(MathHelper.ToRadians(Angle));
            double y = Math.Sin(MathHelper.ToRadians(Angle));

            _movementVector = new Vector2(
                (float)(x * _speed),
                (float)(y * _speed));
        }
    }
}