using System;
using Donker.Pong.Common.GameComponents;
using Donker.Pong.Common.Input;
using Donker.Pong.Game.Menu;
using Donker.Pong.Game.Settings;
using Donker.Pong.Game.Status;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Donker.Pong.Game.Components
{
    /// <summary>
    /// Manages the main menu of the game
    /// </summary>
    public class MenuComponent : ImprovedDrawableGameComponent
    {
        private GameInfo _gameInfo;
        private InputManager _inputManager;
        private SettingsManager _settingsManager;
        private RootMenuPage _rootMenuPage;

        private SpriteBatch _spriteBatch;
        private SpriteFont _titleFont;
        private SpriteFont _itemFont;
        
        public MenuComponent(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            _gameInfo = Game.Services.GetService<GameInfo>();
            _gameInfo.StateChanged += GameInfoOnStateChanged;
            _inputManager = Game.Services.GetService<InputManager>();
            _settingsManager = Game.Services.GetService<SettingsManager>();

            ShowMenu();
        }

        // Show or hide the main menu based on it's new state
        private void GameInfoOnStateChanged(object sender, GameStateChangedEventArgs e)
        {
            switch (e.NewState)
            {
                case GameState.InMenu:
                    if (e.PreviousState != GameState.InMenu)
                        ShowMenu();
                    break;

                default:
                    HideMenu();
                    break;
            }
        }

        // Starts the main menu
        private void ShowMenu()
        {
            if (_rootMenuPage == null)
            {
                _rootMenuPage = new RootMenuPage(_gameInfo, _settingsManager);
                _rootMenuPage.Exit += RootMenuPageOnExit;
            }

            Enabled = true;
            Visible = true;
        }

        // Stops the main menu
        private void HideMenu()
        {
            Enabled = false;
            Visible = false;

            if (_rootMenuPage != null)
            {
                _rootMenuPage.Exit -= RootMenuPageOnExit;
                _rootMenuPage = null;
            }
        }

        // When the root menu calls for an exit, the entire game should exit
        private void RootMenuPageOnExit(object sender, EventArgs eventArgs)
        {
            Microsoft.Xna.Framework.Game game = Game;
            game.Exit();
        }

        public override void LoadContent()
        {
            _spriteBatch = Game.Services.GetService<SpriteBatch>();
            _titleFont = Game.Content.Load<SpriteFont>("Fonts\\PongFont_Large");
            _itemFont = Game.Content.Load<SpriteFont>("Fonts\\PongFont_Small");
        }

        public override void Update(GameTime gameTime)
        {
            _rootMenuPage.Update(gameTime, _inputManager);
        }

        public override void Draw(GameTime gameTime)
        {
            _rootMenuPage.Draw(_spriteBatch, _titleFont, _itemFont, _gameInfo.Bounds);
        }
    }
}