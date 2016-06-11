using Donker.Pong.Common.Menu;
using Donker.Pong.Game.ResourceFiles;
using Donker.Pong.Game.Settings;

namespace Donker.Pong.Game.Menu
{
    /// <summary>
    /// The menu page where you can choose which settings to change.
    /// </summary>
    public class SettingsMenuPage : MenuPage
    {
        /// <summary>
        /// Initializes a new instance of <see cref="SettingsMenuPage"/> using the specified settings manager.
        /// </summary>
        /// <param name="settingsManager">The settings manager to use when reading and applying settings.</param>
        public SettingsMenuPage(SettingsManager settingsManager)
            : base(StringResources.MenuTitle_Settings)
        {
            MenuItem controlsItem = new MenuItem { Name = StringResources.Controls };
            controlsItem.Options.Add(new MenuItemOption { Action = () => OpenSubPage(() => new ControlSettingsMenuPage(settingsManager)) });
            Items.Add(controlsItem);

            MenuItem videoItem = new MenuItem { Name = StringResources.Video };
            videoItem.Options.Add(new MenuItemOption { Action = () => OpenSubPage(() => new VideoSettingsMenuPage(settingsManager)) });
            Items.Add(videoItem);

            MenuItem audioItem = new MenuItem { Name = StringResources.Audio };
            audioItem.Options.Add(new MenuItemOption { Action = () => OpenSubPage(() => new AudioSettingsMenuPage(settingsManager)) });
            Items.Add(audioItem);

            MenuItem backItem = new MenuItem { Name = StringResources.Back };
            backItem.Options.Add(new MenuItemOption { Action = Close });
            Items.Add(backItem);
        }
    }
}