using Donker.Pong.Common.Actors;
using Donker.Pong.Common.GameComponents;
using Donker.Pong.Game.Actors.Balls;
using Donker.Pong.Game.Actors.Paddles;
using Donker.Pong.Game.Settings;
using Donker.Pong.Game.Status;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Donker.Pong.Game.Components
{
    /// <summary>
    /// Manages creation, updating and drawing of the game's actors.
    /// </summary>
    public class ActorComponent : ImprovedDrawableGameComponent
    {
        private PaddleFactory _paddleFactory;
        private BallFactory _ballFactory;

        private SpriteBatch _spriteBatch;
        private GameInfo _gameInfo;
        private ActorRegistry _actorRegistry;
        private SettingsManager _settingsManager;
        
        public ActorComponent(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            _gameInfo = Game.Services.GetService<GameInfo>();
            _actorRegistry = Game.Services.GetService<ActorRegistry>();
            _settingsManager = Game.Services.GetService<SettingsManager>();

            _gameInfo.StateChanged += GameInfoOnStateChanged;
        }

        private void GameInfoOnStateChanged(object sender, GameStateChangedEventArgs e)
        {
            switch (e.NewState)
            {
                // If a new game started, create new actors
                case GameState.InProgress:
                    if (e.PreviousState < GameState.InProgress)
                        CreateActors();
                    Enabled = true;
                    Visible = true;
                    break;
                case GameState.InMenu:
                    Enabled = false;
                    Visible = false;
                    break;
                default:
                    Enabled = false;
                    Visible = true;
                    break;
            }
        }

        // Clear old actors and create new ones based on the settings
        private void CreateActors()
        {
            _actorRegistry.Clear();

            PaddleBase leftPaddle = _paddleFactory.CreatePaddle(_settingsManager.Settings.Gameplay.LeftPaddleType, PaddleSide.Left);
            PaddleBase rightPaddle = _paddleFactory.CreatePaddle(_settingsManager.Settings.Gameplay.RightPaddleType, PaddleSide.Right);

            _actorRegistry.AddRange(leftPaddle, rightPaddle);

            for (int i = 0; i < _settingsManager.Settings.Gameplay.BallCount; i++)
            {
                Ball ball = _ballFactory.CreateBall();
                _actorRegistry.Add(ball);
            }
        }

        public override void LoadContent()
        {
            _spriteBatch = Game.Services.GetService<SpriteBatch>();
            
            _paddleFactory = new PaddleFactory(Game.Services);
            _ballFactory = new BallFactory(Game.Services);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (IActor actor in _actorRegistry)
            {
                actor.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (IActor actor in _actorRegistry)
            {
                actor.Draw(_spriteBatch);
            }
        }

        public override void UnloadContent()
        {
            // Dispose of the factories so that they can dispose of their textures and other resources
            _paddleFactory.Dispose();
            _ballFactory.Dispose();
        }
    }
}