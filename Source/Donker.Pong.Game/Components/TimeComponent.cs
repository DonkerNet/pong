using System;
using Donker.Pong.Game.Settings;
using Donker.Pong.Game.Status;
using Microsoft.Xna.Framework;

namespace Donker.Pong.Game.Components
{
    /// <summary>
    /// Manages time limits for the game.
    /// </summary>
    public class TimeComponent : GameComponent
    {
        private GameInfo _gameInfo;
        private SettingsManager _settingsManager;
        private TimeSpan _totalElapsedTime;

        public TimeComponent(Microsoft.Xna.Framework.Game game)
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
                        _totalElapsedTime = TimeSpan.Zero;
                    Enabled = _settingsManager.Settings.Gameplay.HasTimeLimit && _totalElapsedTime < _settingsManager.Settings.Gameplay.TimeLimit;
                    break;
                default:
                    Enabled = false;
                    break;
            }
        }

        public override void Update(GameTime gameTime)
        {
            _totalElapsedTime += gameTime.ElapsedGameTime;

            if (_totalElapsedTime >= _settingsManager.Settings.Gameplay.TimeLimit)
                _gameInfo.State = GameState.Ended;
        }
    }
}