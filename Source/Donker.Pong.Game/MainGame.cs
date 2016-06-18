using System.Linq;
using Donker.Pong.Common.Actors;
using Donker.Pong.Common.Extensions;
using Donker.Pong.Common.GameComponents;
using Donker.Pong.Common.Input;
using Donker.Pong.Common.Settings;
using Donker.Pong.Common.Shapes;
using Donker.Pong.Game.Actors;
using Donker.Pong.Game.Audio;
using Donker.Pong.Game.Components;
using Donker.Pong.Game.ResourceFiles;
using Donker.Pong.Game.Settings;
using Donker.Pong.Game.Settings.Entities;
using Donker.Pong.Game.Status;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Donker.Pong.Game
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Microsoft.Xna.Framework.Game
    {
        private readonly GraphicsDeviceManager _graphicsDeviceManager;
        private readonly SettingsManager _settingsManager;
        private readonly AudioManager _audioManager;
        private readonly GameInfo _gameInfo;
        private readonly InputManager _inputManager;
        private readonly ActorRegistry _actorRegistry;
        
        private SpriteBatch _spriteBatch;
        private SpriteFont _statusTextFont;

        // Game components
        private MenuComponent _menuComponent;
        private BackgroundComponent _backgroundComponent;
        private ActorComponent _actorComponent;
        private CollisionComponent _collisionComponent;
        private ScoreComponent _scoreComponent;
        private TimeComponent _timeComponent;
        private SpeedComponent _speedComponent;

        public MainGame()
        {
            Window.Title = string.Format("{0} v{1}", StringResources.GameTitle, Program.AssemblyName.Version.ToString(3));

            _graphicsDeviceManager = new GraphicsDeviceManager(this);
            
            _settingsManager = new SettingsManager(this, new JsonFileSettingsStorage(SettingsConstants.SettingsFilePath));
            _settingsManager.SettingsApplied += SettingsManagerOnSettingsApplied;

            _audioManager = new AudioManager(_settingsManager, Content);
            _gameInfo = new GameInfo();

            _inputManager = new InputManager();
            _actorRegistry = new ActorRegistry();

            Content.RootDirectory = "Content";

            CreateComponents();
        }

        private void SettingsManagerOnSettingsApplied(object sender, SettingsAppliedEventArgs e)
        {
            // Update the bounds and scale when video settings are altered
            if (e.VideoChanged)
            {
                Vector2 resolution = _settingsManager.Settings.Video.Resolution;

                float boundsWidth = (resolution.X / resolution.Y) * _gameInfo.Bounds.Height;
                _gameInfo.Bounds = new RectangleF(0F, 0F, boundsWidth, _gameInfo.Bounds.Height);

                _gameInfo.Scale = resolution.Y / _gameInfo.Bounds.Height;
            }
        }

        // Create all the components that should be used by this game
        private void CreateComponents()
        {
            _menuComponent = new MenuComponent(this);
            Components.Add(_menuComponent);

            _backgroundComponent = new BackgroundComponent(this);
            Components.Add(_backgroundComponent);

            _actorComponent = new ActorComponent(this);
            Components.Add(_actorComponent);

            _collisionComponent = new CollisionComponent(this);
            Components.Add(_collisionComponent);

            _scoreComponent = new ScoreComponent(this);
            Components.Add(_scoreComponent);

            _timeComponent = new TimeComponent(this);
            Components.Add(_timeComponent);

            _speedComponent = new SpeedComponent(this);
            Components.Add(_speedComponent);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Register services
            Services.AddService(_graphicsDeviceManager);
            Services.AddService(_gameInfo);
            Services.AddService(_settingsManager);
            Services.AddService(_audioManager);
            Services.AddService(_inputManager);
            Services.AddService(_actorRegistry);

            // Initialize the components
            Components.ForEach(c => c.Initialize());

            // Load content for this game and the components
            LoadContent();

            // Apply settings
            GameSettings settings = _settingsManager.LoadSettings() ?? _settingsManager.GetDefaultSettingsCopy();
            _settingsManager.ApplySettings(settings);
            _settingsManager.SaveSettings();

            // Set the initial state of the game
            _gameInfo.State = GameState.InMenu;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(_spriteBatch);

            _statusTextFont = Content.Load<SpriteFont>("Fonts\\PongFont_Large");

            // TODO: use this.Content to load your game content here

            _audioManager.LoadAudio();
            Components.OfType<ImprovedDrawableGameComponent>().ForEach(c => c.LoadContent());
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            Components.OfType<ImprovedDrawableGameComponent>().ForEach(c => c.UnloadContent());
            _audioManager.UnloadAudio();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            _inputManager.UpdateKeyboardState(Keyboard.GetState());

            switch (_gameInfo.State)
            {
                case GameState.InProgress:
                    if (_inputManager.IsNewKeyDown(Keys.Escape))
                        _gameInfo.State = GameState.Paused;
                    break;
                case GameState.Paused:
                    if (_inputManager.IsNewKeyDown(Keys.Escape))
                        _gameInfo.State = GameState.InProgress;
                    else if (_inputManager.IsNewKeyDown(Keys.E))
                        _gameInfo.State = GameState.InMenu;
                    break;
                case GameState.Ended:
                    if (_inputManager.IsNewKeyDown(Keys.Escape))
                        _gameInfo.State = GameState.InMenu;
                    break;
            }

            Components.OfType<IUpdateable>().Where(c => c.Enabled).ForEach(c => c.Update(gameTime));
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            _spriteBatch.Begin(
                SpriteSortMode.Immediate,
                null,
                null,
                null,
                null,
                null,
                Matrix.CreateScale(_gameInfo.Scale));

            DrawStatusText();

            Components.OfType<IDrawable>().Where(c => c.Visible).ForEach(c => c.Draw(gameTime));

            _spriteBatch.End();
        }

        // Draws the paused or game over status text to the screen
        private void DrawStatusText()
        {
            string text;

            switch (_gameInfo.State)
            {
                case GameState.Paused:
                    text = StringResources.GameStatusText_Paused;
                    break;
                case GameState.Ended:
                    text = StringResources.GameStatusText_Ended;
                    break;
                default:
                    return;
            }

            Vector2 textSize = _statusTextFont.MeasureString(text);
            Vector2 center = _gameInfo.Bounds.Center;
            
            _spriteBatch.DrawString(_statusTextFont, text, center - textSize / 2F, Color.White);
        }
    }
}
