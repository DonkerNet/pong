using System;
using Donker.Pong.Common.Menu;
using Donker.Pong.Game.ResourceFiles;
using Donker.Pong.Game.Settings;
using Donker.Pong.Game.Status;

namespace Donker.Pong.Game.Menu
{
    /// <summary>
    /// The first menu page that the user will see when the menu is opened.
    /// </summary>
    public class RootMenuPage : MenuPage
    {
        /// <summary>
        /// Event triggered when the game needs to exit.
        /// </summary>
        public event EventHandler Exit;

        /// <summary>
        /// Initializes a new instance of <see cref="RootMenuPage"/> using the specified settings manager.
        /// </summary>
        /// <param name="gameInfo">The information about the game's state.</param>
        /// <param name="settingsManager">The settings manager to use when reading and applying settings.</param>
        public RootMenuPage(GameInfo gameInfo, SettingsManager settingsManager)
            : base(StringResources.GameTitle)
        {
            MenuItem newGameItem = new MenuItem { Name = StringResources.NewGame };
            newGameItem.Options.Add(new MenuItemOption { Action = () => OpenSubPage(() => new NewGameMenuPage(gameInfo, settingsManager)) });
            Items.Add(newGameItem);

            MenuItem settingsItem = new MenuItem { Name = StringResources.Settings };
            settingsItem.Options.Add(new MenuItemOption { Action = () => OpenSubPage(() => new SettingsMenuPage(settingsManager)) });
            Items.Add(settingsItem);

            MenuItem exitItem = new MenuItem { Name = StringResources.Exit };
            exitItem.Options.Add(new MenuItemOption { Action = InvokeExit });
            Items.Add(exitItem);
        }

        protected override void Close()
        {
            // Overridden because the root page cannot be closed since it has no parent
        }

        private void InvokeExit()
        {
            if (Exit != null)
                Exit.Invoke(this, EventArgs.Empty);
        }
    }
}