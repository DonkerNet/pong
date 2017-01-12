using Donker.Pong.Common.Extensions;
using Donker.Pong.Common.GameComponents;
using Donker.Pong.Game.ResourceFiles;
using Donker.Pong.Game.Status;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Donker.Pong.Game.Components
{
    /// <summary>
    /// Manages drawing of the game's status information.
    /// </summary>
    public class StatusComponent : ImprovedDrawableGameComponent
    {
        private GameInfo _gameInfo;
        private SpriteBatch _spriteBatch;
        private Texture2D _textBackgroundPixel;
        private SpriteFont _textFont;

        private string _text;
        private Vector2 _textLocation;
        private Rectangle _textArea;

        public StatusComponent(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            _gameInfo = Game.Services.GetService<GameInfo>();
            _gameInfo.StateChanged += GameInfoOnStateChanged;
        }

        // When the game state changes, update the status text to show
        private void GameInfoOnStateChanged(object sender, GameStateChangedEventArgs e)
        {
            switch (_gameInfo.State)
            {
                default:
                    Visible = false;
                    _text = null;
                    return;

                case GameState.Paused:
                    _text = StringResources.GameStatusText_Paused;
                    break;
                case GameState.Ended:
                    _text = StringResources.GameStatusText_Ended;
                    break;
            }

            Vector2 textSize = _textFont.MeasureString(_text);
            _textLocation = _gameInfo.Bounds.Center - textSize / 2F;
            _textArea = new Rectangle((int)_textLocation.X, (int)_textLocation.Y, (int)textSize.X, (int)textSize.Y);

            Visible = true;
        }

        public override void LoadContent()
        {
            _spriteBatch = Game.Services.GetService<SpriteBatch>();

            _textBackgroundPixel = new Texture2D(Game.GraphicsDevice, 1, 1);
            _textBackgroundPixel.FillColor(Color.Black);

            _textFont = Game.Content.Load<SpriteFont>("Fonts\\PongFont_Large");
        }

        public override void UnloadContent()
        {
            _textBackgroundPixel.Dispose();
        }

        public override void Draw(GameTime gameTime)
        {
            if (_text == null)
                return;

            _spriteBatch.Draw(_textBackgroundPixel, destinationRectangle: _textArea);
            _spriteBatch.DrawString(_textFont, _text, _textLocation, Color.White);
        }
    }
}