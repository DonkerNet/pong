using Donker.Pong.Common.Input;
using Donker.Pong.Game.Settings;
using Donker.Pong.Game.Settings.Entities;
using Donker.Pong.Game.Status;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Donker.Pong.Game.Actors.Paddles
{
    /// <summary>
    /// A paddle that is player controlled.
    /// </summary>
    public class PlayerPaddle : PaddleBase
    {
        private readonly InputManager _inputManager;
        private readonly SettingsManager _settingsManager;

        private IControlScheme Controls
        {
            get
            {
                return PaddleSide == PaddleSide.Left
                    ? _settingsManager.Settings.Controls.LeftPaddle
                    : _settingsManager.Settings.Controls.RightPaddle;
            }
        }

        public PlayerPaddle(GameInfo gameInfo, Texture2D texture, SettingsManager settingsManager, InputManager inputManager, PaddleSide paddleSide, Vector2 size, Vector2 boundsPadding, float speed)
            : base(gameInfo, texture, paddleSide, size, boundsPadding, speed)
        {
            _inputManager = inputManager;
            _settingsManager = settingsManager;
        }

        // Moves the paddle based on the controls
        public override void Update(GameTime gameTime)
        {
            bool moveUp = _inputManager.IsKeyDown(Controls.MoveUp);
            bool moveDown = _inputManager.IsKeyDown(Controls.MoveDown);

            if (moveUp || moveDown)
            {
                float actualSpeed = Speed * GameInfo.Speed;

                if (moveUp)
                    actualSpeed = -actualSpeed;

                Hitbox.Offset(0F, actualSpeed);
            }
        }
    }
}