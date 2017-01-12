using System;
using Donker.Pong.Common.Extensions;
using Donker.Pong.Game.Audio;
using Donker.Pong.Game.Settings;
using Donker.Pong.Game.Status;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Donker.Pong.Game.Actors.Balls
{
    /// <summary>
    /// Factory class responsible for created instances of the <see cref="Ball"/> class.
    /// </summary>
    public class BallFactory : IDisposable
    {
        // Services
        private readonly GameInfo _gameInfo;
        private readonly AudioManager _audioManager;

        // Disposable resources
        private Texture2D _ballPixel;

        /// <summary>
        /// Initializes a new instance of the <see cref="BallFactory"/> using the specified services.
        /// </summary>
        /// <param name="services">The services to use when creating a new ball.</param>
        public BallFactory(GameServiceContainer services)
        {
            // Get services
            _gameInfo = services.GetService<GameInfo>();
            _audioManager = services.GetService<AudioManager>();

            GraphicsDeviceManager graphics = services.GetService<GraphicsDeviceManager>();
            _ballPixel = new Texture2D(graphics.GraphicsDevice, 1, 1);
            _ballPixel.FillColor(Color.White);
        }

        /// <summary>
        /// Creates a new <see cref="Ball"/> instance.
        /// </summary>
        /// <returns>A new <see cref="Ball"/> instance.</returns>
        public Ball CreateBall()
        {
            ThrowExceptionWhenDisposed();

            return new Ball(
               _gameInfo,
               _audioManager,
               _ballPixel,
               SettingsConstants.BallSize,
               SettingsConstants.BallBoundsPadding,
               SettingsConstants.BallSpeed);
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
                _ballPixel.Dispose();
            }

            _ballPixel = null;
        }

        /// <summary>
        /// Call this method to thrown an <see cref="ObjectDisposedException"/> when this instance has been disposed.
        /// </summary>
        protected void ThrowExceptionWhenDisposed()
        {
            if (!IsDisposed)
                return;

            Type thisType = typeof (BallFactory);
            throw new ObjectDisposedException(thisType.Name);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources for this <see cref="BallFactory"/> instance.
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
        /// Destroys this <see cref="BallFactory"/> instance.
        /// </summary>
        ~BallFactory()
        {
            Dispose(false);
        }

        #endregion
    }
}