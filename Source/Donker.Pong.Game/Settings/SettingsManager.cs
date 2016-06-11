using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Donker.Pong.Common.Extensions;
using Donker.Pong.Common.Settings;
using Donker.Pong.Game.Actors.Paddles;
using Donker.Pong.Game.Audio;
using Donker.Pong.Game.Settings.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Donker.Pong.Game.Settings
{
    /// <summary>
    /// Manages all the settings throughout the game.
    /// </summary>
    public class SettingsManager
    {
        private readonly Microsoft.Xna.Framework.Game _game;
        private readonly Lazy<GraphicsDeviceManager> _graphics;
        private readonly ISettingsStorage _settingsStorage;
        private GameSettings _defaultSettings;
        private GameSettings _settings;
        
        /// <summary>
        /// An event that is fired when the settings are applied with the <see cref="ApplySettings"/> method.
        /// </summary>
        public event EventHandler<SettingsAppliedEventArgs> SettingsApplied;

        /// <summary>
        /// Gets a direct reference to the current settings.
        /// </summary>
        public IGameSettings Settings
        {
            // Since we don't want to clone every time we want to read the settings, we return a reference of the actual settings as IGameSettings, since this interface is built to be read-only
            get { return GetSettings(); }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="SettingsManager"/> for the specified game.
        /// </summary>
        /// <param name="game">The game this settings manager is for.</param>
        /// <param name="settingsStorage">The storage to use when loading and saving settings.</param>
        public SettingsManager(Microsoft.Xna.Framework.Game game, ISettingsStorage settingsStorage)
        {
            _game = game;
            _settingsStorage = settingsStorage;

            // We keep a lazy reference since the GraphicsDeviceManager is not yet available at this point
            _graphics = new Lazy<GraphicsDeviceManager>(() => _game.Services.GetService<GraphicsDeviceManager>());
        }

        /// <summary>
        /// Returns a copy of the default settings for editing.
        /// </summary>
        /// <returns>A copy as a <see cref="GameSettings"/> object.</returns>
        public GameSettings GetDefaultSettingsCopy()
        {
            // The object is cloned so that any changes will not affect the default settings object used by the SettingsManager itself
            return GetDefaultSettings().Clone();
        }

        /// <summary>
        /// Returns a copy of the current settings for editing.
        /// </summary>
        /// <returns>A copy as a <see cref="GameSettings"/> object.</returns>
        public GameSettings GetSettingsCopy()
        {
            // The object is cloned so that any changes will not directly affect the settings throughout the game unless you apply them with the ApplySettings method
            return GetSettings().Clone();
        }

        /// <summary>
        /// Loads the settings object from the storage. This does not directly apply the settings.
        /// </summary>
        /// <returns>The settings as a <see cref="GameSettings"/> object.</returns>
        public GameSettings LoadSettings()
        {
            GameSettings settings = null;

            try
            {
                settings = _settingsStorage.Load<GameSettings>();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }

            if (settings != null)
                SanitizeSettings(settings);

            return settings;
        }

        /// <summary>
        /// Saves the settings to the storage.
        /// </summary>
        public void SaveSettings()
        {
            GameSettings settings = GetSettings();

            try
            {
                _settingsStorage.Save(settings);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
        }

        /// <summary>
        /// Sanitizes and applies a <see cref="GameSettings"/> object so that the settings will be available throughout the game.
        /// </summary>
        /// <param name="newSettings">The settings to apply.</param>
        /// <exception cref="ArgumentNullException"><paramref name="newSettings"/> is null.</exception>
        public void ApplySettings(GameSettings newSettings)
        {
            if (newSettings == null)
                throw new ArgumentNullException("newSettings");

            GameSettings oldSettings = _settings;
            SanitizeSettings(newSettings);
            _settings = newSettings.Clone();

            // Check which types of settings have been changed
            bool audioSettingsChanged = oldSettings == null || !_settings.Audio.Equals(oldSettings.Audio);
            bool videoSettingsChanged = oldSettings == null || !_settings.Video.Equals(oldSettings.Video);
            bool gamePlaySettingsChanged = oldSettings == null || !_settings.Gameplay.Equals(oldSettings.Gameplay);
            bool controlSettingsChanged = oldSettings == null || !_settings.Controls.Equals(oldSettings.Controls);

            if (videoSettingsChanged)
            {
                // Apply graphics changes
                GraphicsDeviceManager graphics = _graphics.Value;
                graphics.SynchronizeWithVerticalRetrace = _settings.Video.VSync;
                graphics.PreferredBackBufferWidth = (int)_settings.Video.Resolution.X;
                graphics.PreferredBackBufferHeight = (int)_settings.Video.Resolution.Y;
                graphics.IsFullScreen = _settings.Video.FullScreen;
                graphics.ApplyChanges();

                // If windowed and the resolution changed, reposition the window on the screen
                if (!_settings.Video.FullScreen && (oldSettings == null || oldSettings.Video.FullScreen || oldSettings.Video.Resolution != _settings.Video.Resolution))
                {
                    Rectangle windowBounds = _game.Window.ClientBounds;
                    DisplayMode currentDisplayMode = graphics.GraphicsDevice.Adapter.CurrentDisplayMode;

                    // Window is larger than the screen, position it top-left
                    if (windowBounds.Width >= currentDisplayMode.Width || windowBounds.Height >= currentDisplayMode.Height)
                    {
                        _game.Window.Position = new Point(0, 0);
                    }
                    // Window is smaller than the screen, center it
                    else
                    {
                        _game.Window.Position = new Point(
                            currentDisplayMode.Width / 2 - windowBounds.Width / 2,
                            currentDisplayMode.Height / 2 - windowBounds.Height / 2);
                    }
                }
            }

            // Inform event handlers that the settings have been applied
            if (SettingsApplied != null)
            {
                SettingsAppliedEventArgs eventArgs = new SettingsAppliedEventArgs(
                    audioSettingsChanged,
                    videoSettingsChanged,
                    gamePlaySettingsChanged,
                    controlSettingsChanged);

                SettingsApplied.Invoke(this, eventArgs);
            }
        }

        /// <summary>
        /// Returns a list of screen resolutions supported by the graphics device.
        /// </summary>
        /// <returns>The resolutions as a <see cref="IList{Vector2}"/> object.</returns>
        public IList<Vector2> GetSupportedResolutions()
        {
            GraphicsDeviceManager graphics = _graphics.Value;
            return graphics.GraphicsDevice.Adapter.SupportedDisplayModes
                .Select(dm => new Vector2(dm.Width, dm.Height))
                .ToList();
        }

        // Returns the default settings instance and creates it if it doesn't exist yet
        private GameSettings GetDefaultSettings()
        {
            if (_defaultSettings == null)
            {
                AudioSettings audioSettings = new AudioSettings
                {
                    Enabled = true,
                    Volume = 100,
                    SfxSet = 1
                };

                VideoSettings videoSettings = new VideoSettings
                {
                    Resolution = SettingsConstants.DefaultResolution,
                    VSync = true
                };

                Type paddleType = typeof(PlayerPaddle);

                GameplaySettings gameplaySettings = new GameplaySettings
                {
                    BallCount = 1,
                    GameSpeed = 1F,
                    LeftPaddleType = paddleType,
                    RightPaddleType = paddleType,
                    ScoreLimit = 5
                };
                
                ControlSettings controls = new ControlSettings
                {
                    LeftPaddle = new ControlScheme
                    {
                        MoveUp = Keys.W,
                        MoveDown = Keys.S
                    },
                    RightPaddle = new ControlScheme
                    {
                        MoveUp = Keys.Up,
                        MoveDown = Keys.Down
                    }
                };

                _defaultSettings = new GameSettings
                {
                    Audio = audioSettings,
                    Video = videoSettings,
                    Gameplay = gameplaySettings,
                    Controls = controls
                };
            }

            return _defaultSettings;
        }

        // Returns the current settings instance and uses the default settings if no settings are currently set yet
        private GameSettings GetSettings()
        {
            if (_settings == null)
                _settings = GetDefaultSettings();
            return _settings;
        }

        // Validates and alters the settings to fix any invalid values
        private void SanitizeSettings(GameSettings settings)
        {
            GameSettings defaultSettings = GetDefaultSettings();

            // Sanitize audio or use defaults
            if (settings.Audio == null)
            {
                settings.Audio = defaultSettings.Audio.Clone();
            }
            else
            {
                settings.Audio.Volume = settings.Audio.Volume.Clamp(0, 100);
                settings.Audio.SfxSet = settings.Audio.SfxSet.Clamp(1, AudioManager.SfxSetCount);
            }

            // Sanitize video or use defaults
            if (settings.Video == null)
            {
                settings.Video = defaultSettings.Video.Clone();
            }
            else
            {
                GraphicsDeviceManager graphics = _graphics.Value;
                bool isSupportedDisplayMode = graphics.GraphicsDevice.Adapter.SupportedDisplayModes.Any(
                    sdm => sdm.Width == (int)settings.Video.Resolution.X && sdm.Height == (int)settings.Video.Resolution.Y);
                if (!isSupportedDisplayMode)
                    settings.Video.Resolution = SettingsConstants.DefaultResolution;
            }

            // Sanitize gameplay or use defaults
            if (settings.Gameplay == null)
            {
                settings.Gameplay = defaultSettings.Gameplay.Clone();
            }
            else
            {
                settings.Gameplay.BallCount = settings.Gameplay.BallCount.Clamp(SettingsConstants.BallMinCount, SettingsConstants.BallMaxCount);
                settings.Gameplay.GameSpeed = settings.Gameplay.GameSpeed.Clamp(SettingsConstants.GameSpeedMinValue, SettingsConstants.GameSpeedMaxValue);
                settings.Gameplay.ScoreLimit = settings.Gameplay.ScoreLimit.Clamp(0, int.MaxValue);
                settings.Gameplay.TimeLimit = settings.Gameplay.TimeLimit.Clamp(TimeSpan.Zero, TimeSpan.MaxValue);

                if (!PaddleFactory.IsPaddleSupported(settings.Gameplay.LeftPaddleType))
                    settings.Gameplay.LeftPaddleType = defaultSettings.Gameplay.LeftPaddleType;
                if (!PaddleFactory.IsPaddleSupported(settings.Gameplay.RightPaddleType))
                    settings.Gameplay.RightPaddleType = defaultSettings.Gameplay.RightPaddleType;
            }

            // Sanitize controls or use defaults
            if (settings.Controls == null)
            {
                settings.Controls = defaultSettings.Controls.Clone();
            }
            else
            {
                if (settings.Controls.LeftPaddle == null)
                {
                    settings.Controls.LeftPaddle = defaultSettings.Controls.LeftPaddle.Clone();
                }
                else
                {
                    if (settings.Controls.LeftPaddle.MoveUp == Keys.None)
                        settings.Controls.LeftPaddle.MoveUp = defaultSettings.Controls.LeftPaddle.MoveUp;
                    if (settings.Controls.LeftPaddle.MoveDown == Keys.None)
                        settings.Controls.LeftPaddle.MoveDown = defaultSettings.Controls.LeftPaddle.MoveDown;
                }

                if (settings.Controls.RightPaddle == null)
                {
                    settings.Controls.RightPaddle = defaultSettings.Controls.RightPaddle.Clone();
                }
                else
                {
                    if (settings.Controls.RightPaddle.MoveUp == Keys.None)
                        settings.Controls.RightPaddle.MoveUp = defaultSettings.Controls.RightPaddle.MoveUp;
                    if (settings.Controls.RightPaddle.MoveDown == Keys.None)
                        settings.Controls.RightPaddle.MoveDown = defaultSettings.Controls.RightPaddle.MoveDown;
                }
            }
        }
    }
}