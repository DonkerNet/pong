using Donker.Pong.Common.Menu;
using Donker.Pong.Game.Audio;
using Donker.Pong.Game.ResourceFiles;
using Donker.Pong.Game.Settings;
using Donker.Pong.Game.Settings.Entities;

namespace Donker.Pong.Game.Menu
{
    /// <summary>
    /// The menu page where you can alter the audio settings.
    /// </summary>
    public class AudioSettingsMenuPage : MenuPage
    {
        private readonly SettingsManager _settingsManager;

        private MenuItem _enabledItem;
        private MenuItem _volumeItem;
        private MenuItem _sfxSetItem;

        /// <summary>
        /// Initializes a new instance of <see cref="AudioSettingsMenuPage"/> using the specified settings manager.
        /// </summary>
        /// <param name="settingsManager">The settings manager to use when reading and applying settings.</param>
        public AudioSettingsMenuPage(SettingsManager settingsManager)
            : base(StringResources.MenuTitle_AudioSettings)
        {
            _settingsManager = settingsManager;

            CreateAudioSettingItems();

            MenuItem applyItem = new MenuItem { Name = StringResources.Apply };
            applyItem.Options.Add(new MenuItemOption { Action = Apply });
            Items.Add(applyItem);

            MenuItem cancelItem = new MenuItem { Name = StringResources.Cancel };
            cancelItem.Options.Add(new MenuItemOption { Action = Close });
            Items.Add(cancelItem);
        }

        // Sets up the options to show on the page
        private void CreateAudioSettingItems()
        {
            IAudioSettings audioSettings = _settingsManager.Settings.Audio;

            // Add the option for enabling the audo

            _enabledItem = new MenuItem();
            _enabledItem.Options.Add(new MenuItemOption { Text = StringResources.Enabled, Value = true });
            _enabledItem.Options.Add(new MenuItemOption { Text = StringResources.Disabled, Value = false });
            _enabledItem.SelectedValue = audioSettings.Enabled;
            Items.Add(_enabledItem);

            // Add the options for setting the volume

            _volumeItem = new MenuItem { Name = StringResources.Volume };
            for (int i = 10; i <= 100; i+= 10)
            {
                string text = string.Format("{0}%", i);
                _volumeItem.Options.Add(new MenuItemOption { Text = text, Value = i });
            }
            _volumeItem.SelectedValue = audioSettings.Volume;
            Items.Add(_volumeItem);

            // Add the options for choosing the type of sound effects to use

            _sfxSetItem = new MenuItem { Name = StringResources.SFX };
            for (int i = 1; i <= AudioManager.SfxSetCount; i++)
            {
                _sfxSetItem.Options.Add(new MenuItemOption { Text = string.Format("{0} {1}", StringResources.Set, i), Value = i });
            }
            _sfxSetItem.SelectedValue = audioSettings.SfxSet;
            Items.Add(_sfxSetItem);
        }

        // Applies the audio settings
        private void Apply()
        {
            GameSettings settings = _settingsManager.GetSettingsCopy();

            settings.Audio.Enabled = (bool)_enabledItem.SelectedValue;
            settings.Audio.Volume = (int)_volumeItem.SelectedValue;
            settings.Audio.SfxSet = (int)_sfxSetItem.SelectedValue;

            _settingsManager.ApplySettings(settings);
            _settingsManager.SaveSettings();

            Close();
        }
    }
}