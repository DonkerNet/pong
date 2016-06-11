using System;
using Donker.Pong.Game.Settings;
using Donker.Pong.Game.Status;
using Microsoft.Xna.Framework;

namespace Donker.Pong.Game.Components
{
    /// <summary>
    /// Manages the speed of the game.
    /// </summary>
    public class SpeedComponent : GameComponent
    {
        private GameInfo _gameInfo;
        private SettingsManager _settingsManager;
        private TimeSpan _totalElapsedTime;

        public SpeedComponent(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            _gameInfo = Game.Services.GetService<GameInfo>();
            _settingsManager = Game.Services.GetService<SettingsManager>();

            _gameInfo.StateChanged += GameInfoOnStateChanged;
        }

        private void GameInfoOnStateChanged(object sender, GameStateChangedEventArgs e)
        {
            switch (e.NewState)
            {
                case GameState.InProgress:
                    if (e.PreviousState < GameState.InProgress)
                    {
                        // New game, reset speed
                        _gameInfo.Speed = _settingsManager.Settings.Gameplay.GameSpeed;
                        _totalElapsedTime = TimeSpan.Zero;
                    }
                    // Enable updating when the speed should auto increase
                    Enabled = _settingsManager.Settings.Gameplay.AutoIncreaseSpeed
                        && _gameInfo.Speed >= SettingsConstants.GameSpeedMaxValue;
                    break;
                default:
                    Enabled = false;
                    break;
            }
        }

        public override void Update(GameTime gameTime)
        {
            _totalElapsedTime += gameTime.ElapsedGameTime;

            // Update the speed after each interval
            if (_totalElapsedTime >= SettingsConstants.GameSpeedIncreaseInterval)
            {
                _gameInfo.Speed += SettingsConstants.GameSpeedIncreaseFactor;
                _totalElapsedTime = TimeSpan.Zero;
            }

            // Max speed reached, disable this component
            if (_gameInfo.Speed >= SettingsConstants.GameSpeedMaxValue)
                Enabled = false;
        }
    }
}