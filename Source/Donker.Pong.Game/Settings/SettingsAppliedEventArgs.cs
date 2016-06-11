using System;

namespace Donker.Pong.Game.Settings
{
    /// <summary>
    /// Contains information about which type of settings have been changed.
    /// </summary>
    public class SettingsAppliedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets if the audio settings have been changed.
        /// </summary>
        public bool AudioChanged { get; private set; }
        /// <summary>
        /// Gets if the video settings have been changed.
        /// </summary>
        public bool VideoChanged { get; private set; }
        /// <summary>
        /// Gets if the gameplay settings have been changed.
        /// </summary>
        public bool GameplayChanged { get; private set; }
        /// <summary>
        /// Gets if the control settings have been changed.
        /// </summary>
        public bool ControlsChanged { get; private set; }

        /// <summary>
        /// Initializes a new instance of <see cref="SettingsAppliedEventArgs"/> while specifying which types of settings have been altered.
        /// </summary>
        /// <param name="audioChanged">If the audio settings have been changed.</param>
        /// <param name="videoChanged">If the video settings have been changed.</param>
        /// <param name="gameplayChanged">If the gameplay settings have been changed.</param>
        /// <param name="controlsChanged">If the control settings have been changed.</param>
        public SettingsAppliedEventArgs(bool audioChanged, bool videoChanged, bool gameplayChanged, bool controlsChanged)
        {
            AudioChanged = audioChanged;
            VideoChanged = videoChanged;
            GameplayChanged = gameplayChanged;
            ControlsChanged = controlsChanged;
        }
    }
}