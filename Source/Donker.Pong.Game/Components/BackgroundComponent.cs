using Donker.Pong.Common.Extensions;
using Donker.Pong.Common.GameComponents;
using Donker.Pong.Game.Settings;
using Donker.Pong.Game.Status;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Donker.Pong.Game.Components
{
    /// <summary>
    /// Manages drawing of the game's background.
    /// </summary>
    public class BackgroundComponent : ImprovedDrawableGameComponent
    {
        private GameInfo _gameInfo;
        private SpriteBatch _spriteBatch;
        private Texture2D _horizontalBlockTexture;
        private Texture2D _verticalBlockTexture;

        private Vector2[] _horizontalBlocks;
        private Vector2[] _verticalBlocks;

        const float BlockMarginTotal = SettingsConstants.BackgroundBlockMargin * 2F;

        public BackgroundComponent(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            _gameInfo = Game.Services.GetService<GameInfo>();
            _gameInfo.StateChanged += GameInfoOnStateChanged;
        }

        // When a new game is started, recalculate the positions of the blocks to draw on the screen and allow the background to be drawn
        private void GameInfoOnStateChanged(object sender, GameStateChangedEventArgs e)
        {
            switch (e.NewState)
            {
                case GameState.InProgress:
                    if (e.PreviousState < GameState.InProgress)
                    {
                        CalculateVerticalBlocks();
                        CalculateHorizontalBlocks();
                    }
                    Visible = true;
                    break;
                case GameState.InMenu:
                    Visible = false;
                    break;
                default:
                    Visible = true;
                    break;
            }
        }

        private void CalculateVerticalBlocks()
        {
            // Get how many times a whole block, including margins, can fit vertically
            int blockCount = (int)(_gameInfo.Bounds.Height / (SettingsConstants.BackgroundBlockLength + BlockMarginTotal));

            // Get how much vertical space if left when adding all blocks with margins
            float freeSpaceY = _gameInfo.Bounds.Height - blockCount * (SettingsConstants.BackgroundBlockLength + BlockMarginTotal);

            // Use the space left and margin to determine the start position on the Y axis while centering the blocks vertically
            float startPosY = freeSpaceY / 2F + SettingsConstants.BackgroundBlockMargin;

            // Determine the X position of the blocks when centered horizontally
            float blockX = _gameInfo.Bounds.Width / 2F - SettingsConstants.BackgroundBlockThickness / 2F;

            // Create a new array with the vectors for where a block should be drawn
            _verticalBlocks = new Vector2[blockCount];
            for (int i = 0; i < blockCount; i++)
            {
                float blockY = startPosY + i * (SettingsConstants.BackgroundBlockLength + BlockMarginTotal);
                _verticalBlocks[i] = new Vector2(blockX, blockY);
            }
        }

        private void CalculateHorizontalBlocks()
        {
            // Get how many times a whole block, including margins, can fit horizontally
            int blockCount = (int)(_gameInfo.Bounds.Width / (SettingsConstants.BackgroundBlockLength + BlockMarginTotal));

            // Get how much horizontal space if left when adding all blocks with margins
            float freeSpaceX = _gameInfo.Bounds.Width - blockCount * (SettingsConstants.BackgroundBlockLength + BlockMarginTotal);

            // Use the space left and margin to determine the start position on the X axis while centering the blocks horizontally
            float startPosX = freeSpaceX / 2F + SettingsConstants.BackgroundBlockMargin;

            // Determine the Y position of the blocks for the top and bottom when centered horizontally
            float blockYTop = SettingsConstants.BackgroundBlockThickness;
            float blockYBottom = _gameInfo.Bounds.Bottom - SettingsConstants.BackgroundBlockThickness * 2F;

            // Create a new array with the vectors for where a block should be drawn (top and bottom)
            _horizontalBlocks = new Vector2[blockCount * 2];
            for (int i = 0; i < blockCount; i++)
            {
                float blockX = startPosX + i * (SettingsConstants.BackgroundBlockLength + BlockMarginTotal);

                _horizontalBlocks[i] = new Vector2(blockX, blockYTop);
                _horizontalBlocks[i + blockCount] = new Vector2(blockX, blockYBottom);
            }
        }

        public override void LoadContent()
        {
            _spriteBatch = Game.Services.GetService<SpriteBatch>();

            _verticalBlockTexture = new Texture2D(Game.GraphicsDevice, (int) SettingsConstants.BackgroundBlockThickness, (int) SettingsConstants.BackgroundBlockLength);
            _verticalBlockTexture.FillColor(Color.White);

            _horizontalBlockTexture = new Texture2D(Game.GraphicsDevice, (int) SettingsConstants.BackgroundBlockLength, (int) SettingsConstants.BackgroundBlockThickness);
            _horizontalBlockTexture.FillColor(Color.White);
        }

        public override void UnloadContent()
        {
            _verticalBlockTexture.Dispose();
            _horizontalBlockTexture.Dispose();
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Vector2 horizontalBlockTop in _horizontalBlocks)
            {
                _spriteBatch.Draw(_horizontalBlockTexture, horizontalBlockTop);
            }

            foreach (Vector2 verticalBlock in _verticalBlocks)
            {
                _spriteBatch.Draw(_verticalBlockTexture, verticalBlock);
            }
        }
    }
}