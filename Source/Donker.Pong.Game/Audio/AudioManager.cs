using System.Collections.Generic;
using Donker.Pong.Game.Settings;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Donker.Pong.Game.Audio
{
    /// <summary>
    /// Manages the audio in the entire game.
    /// </summary>
    public class AudioManager
    {
        private readonly SettingsManager _settings;
        private readonly ContentManager _content;
        private readonly IDictionary<string, SoundEffect> _sfx;

        private bool _enabled;
        private float _volume;
        private int _sfxSet;

        /// <summary>
        /// The amount of sound effect sets available in the game.
        /// </summary>
        public const int SfxSetCount = 2;

        /// <summary>
        /// Initializes a new instance of <see cref="AudioManager"/> using the specified settings and content managers.
        /// </summary>
        /// <param name="settings">The settings manager used for keeping track of audio setting changes.</param>
        /// <param name="content">The content manager to get the audio files from.</param>
        public AudioManager(SettingsManager settings, ContentManager content)
        {
            _settings = settings;
            _content = content;
            _sfx = new Dictionary<string, SoundEffect>();

            settings.SettingsApplied += OnSettingsApplied;
        }

        // Keep track of changes made to the audio settings
        private void OnSettingsApplied(object sender, SettingsAppliedEventArgs e)
        {
            if (!e.AudioChanged)
                return;

            _enabled = _settings.Settings.Audio.Enabled;
            _volume = _settings.Settings.Audio.Volume / 100F;
            _sfxSet = _settings.Settings.Audio.SfxSet;
        }

        /// <summary>
        /// Loads all the available audio files into the manager.
        /// </summary>
        public void LoadAudio()
        {
            LoadSfx();
        }

        // Loads the sound effects
        private void LoadSfx()
        {
            for (int i = 1; i <= SfxSetCount; i++)
            {
                _sfx["BallReset_" + i] = _content.Load<SoundEffect>("SFX\\BallReset_" + i);
                _sfx["BallPaddleHit_" + i] = _content.Load<SoundEffect>("SFX\\BallPaddleHit_" + i);
                _sfx["BallEdgeHit_" + i] = _content.Load<SoundEffect>("SFX\\BallEdgeHit_" + i);
            }
        }

        /// <summary>
        /// Plays a sound effect with the specified name.
        /// </summary>
        /// <param name="name">The name of the sound effect to play.</param>
        public void PlaySfx(string name)
        {
            if (!_enabled || _volume <= 0F || name == null)
                return;

            SoundEffect sfx;
            if (_sfx.TryGetValue(string.Format("{0}_{1}", name, _sfxSet), out sfx))
                sfx.Play(_volume, 0F, 0F);
        }

        /// <summary>
        /// Unloads all of the audio files that were loaded previously.
        /// </summary>
        public void UnloadAudio()
        {
            _sfx.Clear();
        }
    }
}