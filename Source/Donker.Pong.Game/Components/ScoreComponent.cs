using Donker.Pong.Common.GameComponents;
using Donker.Pong.Game.Settings;
using Donker.Pong.Game.Status;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Donker.Pong.Game.Components
{
    /// <summary>
    /// Manages and draws the score of the game.
    /// </summary>
    public class ScoreComponent : ImprovedDrawableGameComponent
    {
        private GameInfo _gameInfo;
        private SettingsManager _settingsManager;
        private SpriteBatch _spriteBatch;

        // Content manager resources
        private SpriteFont _scoreFont;

        public ScoreComponent(Microsoft.Xna.Framework.Game game)
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
                        // New game has started, reset score
                        _gameInfo.LeftScore = 0;
                        _gameInfo.RightScore = 0;
                    }
                    // Only enable updating when a score limit has been set
                    Enabled = _settingsManager.Settings.Gameplay.HasScoreLimit
                        && _gameInfo.LeftScore < _settingsManager.Settings.Gameplay.ScoreLimit
                        && _gameInfo.RightScore < _settingsManager.Settings.Gameplay.ScoreLimit;
                    Visible = true;
                    break;
                // Don't update or draw the score when in the main menu
                case GameState.InMenu:
                    Enabled = false;
                    Visible = false;
                    break;
                // In any other case, always draw the score
                default:
                    Enabled = false;
                    Visible = true;
                    break;
            }
        }

        public override void LoadContent()
        {
            _spriteBatch = Game.Services.GetService<SpriteBatch>();
            _scoreFont = Game.Content.Load<SpriteFont>("Fonts\\PongFont_Large");
        }

        public override void Update(GameTime gameTime)
        {
            // Check for score limits when enabled
            if (_gameInfo.LeftScore >= _settingsManager.Settings.Gameplay.ScoreLimit || _gameInfo.RightScore >= _settingsManager.Settings.Gameplay.ScoreLimit)
                _gameInfo.State = GameState.Ended;
        }

        public override void Draw(GameTime gameTime)
        {
            float centerX = _gameInfo.Bounds.Center.X;

            string leftScoreString = _gameInfo.LeftScore.ToString();
            Vector2 leftScoreStringSize = _scoreFont.MeasureString(leftScoreString);

            // Draw the left side's score
            _spriteBatch.DrawString(
                _scoreFont,
                leftScoreString,
                new Vector2(centerX - leftScoreStringSize.X - SettingsConstants.ScoreTextMargin, SettingsConstants.ScoreTextMargin),
                Color.White);

            // Draw the right side's score
            _spriteBatch.DrawString(
                _scoreFont,
                _gameInfo.RightScore.ToString(),
                new Vector2(centerX + SettingsConstants.ScoreTextMargin, SettingsConstants.ScoreTextMargin),
                Color.White);
        }
    }
}