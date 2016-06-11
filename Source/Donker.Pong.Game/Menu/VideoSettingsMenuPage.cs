using Donker.Pong.Common.Menu;
using Donker.Pong.Game.ResourceFiles;
using Donker.Pong.Game.Settings;
using Donker.Pong.Game.Settings.Entities;
using Microsoft.Xna.Framework;

namespace Donker.Pong.Game.Menu
{
    /// <summary>
    /// The menu page where you can alter the video settings.
    /// </summary>
    public class VideoSettingsMenuPage : MenuPage
    {
        private readonly SettingsManager _settingsManager;

        private MenuItem _resolutionsItem;
        private MenuItem _vSyncItem;
        private MenuItem _fullScreenItem;

        /// <summary>
        /// Initializes a new instance of <see cref="VideoSettingsMenuPage"/> using the specified settings manager.
        /// </summary>
        /// <param name="settingsManager">The settings manager to use when reading and applying settings.</param>
        public VideoSettingsMenuPage(SettingsManager settingsManager)
            : base(StringResources.MenuTitle_VideoSettings)
        {
            _settingsManager = settingsManager;

            CreateVideoSettingItems();

            MenuItem applyItem = new MenuItem { Name = StringResources.Apply };
            applyItem.Options.Add(new MenuItemOption { Action = Apply });
            Items.Add(applyItem);

            MenuItem cancelItem = new MenuItem { Name = StringResources.Cancel };
            cancelItem.Options.Add(new MenuItemOption { Action = Close });
            Items.Add(cancelItem);
        }

        // Sets up the options to show on the page
        private void CreateVideoSettingItems()
        {
            IVideoSettings videoSettings = _settingsManager.Settings.Video;

            // Add the resolution options

            _resolutionsItem = new MenuItem { Name = StringResources.Resolution };
            foreach (Vector2 resolution in _settingsManager.GetSupportedResolutions())
            {
                _resolutionsItem.Options.Add(new MenuItemOption
                {
                    Text = string.Format("{0}x{1}", resolution.X, resolution.Y),
                    Value = resolution
                });
            }
            _resolutionsItem.SelectedValue = videoSettings.Resolution;
            Items.Add(_resolutionsItem);

            // Add the VSync option

            _vSyncItem = new MenuItem { Name = StringResources.VSync };
            _vSyncItem.Options.Add(new MenuItemOption { Text = StringResources.On, Value = true });
            _vSyncItem.Options.Add(new MenuItemOption { Text = StringResources.Off, Value = false });
            _vSyncItem.SelectedValue = videoSettings.VSync;
            Items.Add(_vSyncItem);

            // Add the full screen option

            _fullScreenItem = new MenuItem();
            _fullScreenItem.Options.Add(new MenuItemOption { Text = StringResources.FullScreen, Value = true });
            _fullScreenItem.Options.Add(new MenuItemOption { Text = StringResources.Windowed, Value = false });
            _fullScreenItem.SelectedValue = videoSettings.FullScreen;
            Items.Add(_fullScreenItem);
        }

        // Applies the video settings
        private void Apply()
        {
            GameSettings originalSettings = _settingsManager.GetSettingsCopy();
            GameSettings newSettings = originalSettings.Clone();

            newSettings.Video.Resolution = (Vector2)_resolutionsItem.SelectedValue;
            newSettings.Video.VSync = (bool)_vSyncItem.SelectedValue;
            newSettings.Video.FullScreen = (bool)_fullScreenItem.SelectedValue;

            // Simply close the page when nothing has changed
            if (originalSettings.Video.Equals(newSettings.Video))
            {
                Close();
                return;
            }

            _settingsManager.ApplySettings(newSettings);

            // Setup and open the confirmation page and revert the changes when no confirmation has been given

            ConfirmationMenuPage confirmationPage = new ConfirmationMenuPage();

            confirmationPage.Confirm += (sender, args) =>
            {
                _settingsManager.SaveSettings();
                Close();
            };

            confirmationPage.Cancel += (sender, args) =>
            {
                _settingsManager.ApplySettings(originalSettings);
                _settingsManager.SaveSettings();
                Close();
            };

            OpenSubPage(confirmationPage);
        }
    }
}