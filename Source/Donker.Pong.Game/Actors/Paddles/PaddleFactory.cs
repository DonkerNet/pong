using System;
using System.Collections.Generic;
using Donker.Pong.Common.Actors;
using Donker.Pong.Common.Extensions;
using Donker.Pong.Common.Input;
using Donker.Pong.Game.Settings;
using Donker.Pong.Game.Status;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Donker.Pong.Game.Actors.Paddles
{
    /// <summary>
    /// Factory class responsible for created instances of the <see cref="PaddleBase"/> class.
    /// </summary>
    public class PaddleFactory : IDisposable
    {
        private static readonly IDictionary<Type, Func<PaddleFactory, PaddleSide, PaddleBase>> Constructors;

        // Services
        private readonly GameInfo _gameInfo;
        private readonly SettingsManager _settingsManager;
        private readonly InputManager _inputManager;
        private readonly ActorRegistry _actorRegistry;

        // Resources
        private Texture2D _paddlePixel;

        /// <summary>
        /// Initializes the static fields of the <see cref="PaddleFactory"/> class.
        /// </summary>
        static PaddleFactory()
        {
            Constructors = new Dictionary<Type, Func<PaddleFactory, PaddleSide, PaddleBase>>
            {
                { typeof(PlayerPaddle), (f, s) => f.CreatePlayerPaddle(s) },
                { typeof(BasicAiPaddle), (f, s) => f.CreateBasicAiPaddle(s) }
            };
        }

        /// <summary>
        /// Initializes a new instance of <see cref="PaddleFactory"/> using the specified services.
        /// </summary>
        /// <param name="services">The services to use when creating new paddles.</param>
        public PaddleFactory(GameServiceContainer services)
        {
            // Get services
            _gameInfo = services.GetService<GameInfo>();
            _settingsManager = services.GetService<SettingsManager>();
            _inputManager = services.GetService<InputManager>();
            _actorRegistry = services.GetService<ActorRegistry>();

            GraphicsDeviceManager graphics = services.GetService<GraphicsDeviceManager>();
            _paddlePixel = new Texture2D(graphics.GraphicsDevice, 1, 1);
            _paddlePixel.FillColor(Color.White);
        }

        /// <summary>
        /// Checks whether a type is a supported paddle type.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns><c>true</c> if supported; otherwise, <c>false</c>.</returns>
        public static bool IsPaddleSupported(Type type)
        {
            return type != null && Constructors.ContainsKey(type);
        }

        /// <summary>
        /// Retrieves a list of supported paddle types.
        /// </summary>
        /// <returns>The list of paddle types as a <see cref="IList{Type}"/> object.</returns>
        public static IList<Type> GetSupportedTypes()
        {
            Type[] types = new Type[Constructors.Count];
            Constructors.Keys.CopyTo(types, 0);
            return types;
        }

        /// <summary>
        /// Creates a new paddle of a certain type for a specific side.
        /// </summary>
        /// <param name="type">The type of the paddle to create.</param>
        /// <param name="paddleSide">The side where the paddle should be placed.</param>
        /// <returns>The new paddle instance as a <see cref="PaddleBase"/> object.</returns>
        public PaddleBase CreatePaddle(Type type, PaddleSide paddleSide)
        {
            Func<PaddleFactory, PaddleSide, PaddleBase> constructor;
            if (Constructors.TryGetValue(type, out constructor))
                return constructor.Invoke(this, paddleSide);
            return null;
        }

        private PaddleBase CreatePlayerPaddle(PaddleSide paddleSide)
        {
            ThrowExceptionWhenDisposed();

            return new PlayerPaddle(
                _gameInfo,
                _paddlePixel,
                _settingsManager,
                _inputManager,
                paddleSide,
                SettingsConstants.PaddleSize,
                SettingsConstants.PaddleBoundsPadding,
                SettingsConstants.PaddleSpeed);
        }

        private PaddleBase CreateBasicAiPaddle(PaddleSide paddleSide)
        {
            ThrowExceptionWhenDisposed();

            return new BasicAiPaddle(
                _gameInfo,
                _paddlePixel,
                _actorRegistry,
                paddleSide,
                SettingsConstants.PaddleSize,
                SettingsConstants.PaddleBoundsPadding,
                SettingsConstants.PaddleSpeed);
        }

        #region Generated Dispose members

        /// <summary>
        /// Gets whether this instance is disposed or not.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Override this method to add your own dispose logic.
        /// </summary>
        /// <param name="disposing">If the instance is being disposed after the public <see cref="Dispose()"/> method has been called.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            if (disposing)
            {
                _paddlePixel.Dispose();
            }

            _paddlePixel = null;
        }

        /// <summary>
        /// Call this method to thrown an <see cref="ObjectDisposedException"/> when this instance has been disposed.
        /// </summary>
        protected void ThrowExceptionWhenDisposed()
        {
            if (!IsDisposed)
                return;

            Type thisType = typeof (PaddleFactory);
            throw new ObjectDisposedException(thisType.Name);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources for this <see cref="PaddleFactory"/> instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        void IDisposable.Dispose()
        {
            Dispose();
        }

        /// <summary>
        /// Destroys this <see cref="PaddleFactory"/> instance.
        /// </summary>
        ~PaddleFactory()
        {
            Dispose(false);
        }

        #endregion
    }
}