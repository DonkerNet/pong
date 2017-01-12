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

        private string _leftScoreString;
        private Vector2 _leftScoreLocation;
        private string _rightScoreString;
        private Vector2 _rightScoreLocation;

        public ScoreComponent(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            _gameInfo = Game.Services.GetService<GameInfo>();
            _settingsManager = Game.Services.GetService<SettingsManager>();

            _gameInfo.StateChanged += GameInfoOnStateChanged;
            _gameInfo.ScoreChanged += GameInfoOnScoreChanged;
        }

        private void GameInfoOnStateChanged(object sender, GameStateChangedEventArgs e)
        {
            switch (e.NewState)
            {
                case GameState.InProgress:
                    if (e.PreviousState < GameState.InProgress)
                    {
                        // New game has started, reset scores
                        _gameInfo.ResetScores();
                        UpdateScores(0, 0);
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

        private void UpdateScores(int leftScore, int rightScore)
        {
            _leftScoreString = leftScore.ToString();
            _rightScoreString = rightScore.ToString();

            float centerX = _gameInfo.Bounds.Center.X;
            Vector2 leftScoreStringSize = _scoreFont.MeasureString(_leftScoreString);

            _leftScoreLocation = new Vector2(centerX - leftScoreStringSize.X - SettingsConstants.ScoreTextMargin, SettingsConstants.ScoreTextMargin);
            _rightScoreLocation = new Vector2(centerX + SettingsConstants.ScoreTextMargin, SettingsConstants.ScoreTextMargin);
        }

        private void GameInfoOnScoreChanged(object sender, ScoreChangedEventArgs e)
        {
            UpdateScores(e.NewLeftScore, e.NewRightScore);
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
            if (_leftScoreString != null)
                _spriteBatch.DrawString(_scoreFont, _leftScoreString, _leftScoreLocation, Color.White);
            if (_rightScoreString != null)
                _spriteBatch.DrawString(_scoreFont, _rightScoreString, _rightScoreLocation, Color.White);
        }
    }
}