using System.Collections.Generic;
using Donker.Pong.Common.Input;
using Donker.Pong.Common.Menu;
using Donker.Pong.Game.ResourceFiles;
using Donker.Pong.Game.Settings;
using Donker.Pong.Game.Settings.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Donker.Pong.Game.Menu
{
    /// <summary>
    /// The menu page where you can alter the controls.
    /// </summary>
    public class ControlSettingsMenuPage : MenuPage
    {
        private readonly SettingsManager _settingsManager;

        private MenuItem _leftUpItem;
        private MenuItem _leftDownItem;
        private MenuItem _rightUpItem;
        private MenuItem _rightDownItem;

        private bool _isWaitingForInput;

        /// <summary>
        /// Initializes a new instance of <see cref="ControlSettingsMenuPage"/> using the specified settings manager.
        /// </summary>
        /// <param name="settingsManager">The settings manager to use when reading and applying settings.</param>
        public ControlSettingsMenuPage(SettingsManager settingsManager)
            : base(StringResources.MenuTitle_ControlSettings)
        {
            _settingsManager = settingsManager;

            CreateControlSettingItems();

            MenuItem applyItem = new MenuItem { Name = StringResources.Apply };
            applyItem.Options.Add(new MenuItemOption { Action = Apply });
            Items.Add(applyItem);

            MenuItem cancelItem = new MenuItem { Name = StringResources.Cancel };
            cancelItem.Options.Add(new MenuItemOption { Action = Close });
            Items.Add(cancelItem);
        }

        // Sets up the controls to show on the page
        private void CreateControlSettingItems()
        {
            IControlSettings controlSettings = _settingsManager.Settings.Controls;

            // Left player up key

            _leftUpItem = new MenuItem { Name = StringResources.MenuText_Controls_LeftUp };
            _leftUpItem.Options.Add(new MenuItemOption
            {
                Text = controlSettings.LeftPaddle.MoveUp.ToString(),
                Value = controlSettings.LeftPaddle.MoveUp
            });
            Items.Add(_leftUpItem);

            // Left player down key

            _leftDownItem = new MenuItem { Name = StringResources.MenuText_Controls_LeftDown };
            _leftDownItem.Options.Add(new MenuItemOption
            {
                Text = controlSettings.LeftPaddle.MoveDown.ToString(),
                Value = controlSettings.LeftPaddle.MoveDown
            });
            Items.Add(_leftDownItem);

            // Right player up key

            _rightUpItem = new MenuItem { Name = StringResources.MenuText_Controls_RightUp };
            _rightUpItem.Options.Add(new MenuItemOption
            {
                Text = controlSettings.RightPaddle.MoveUp.ToString(),
                Value = controlSettings.RightPaddle.MoveUp
            });
            Items.Add(_rightUpItem);

            // Right player down key

            _rightDownItem = new MenuItem { Name = StringResources.MenuText_Controls_RightDown };
            _rightDownItem.Options.Add(new MenuItemOption
            {
                Text = controlSettings.RightPaddle.MoveDown.ToString(),
                Value = controlSettings.RightPaddle.MoveDown
            });
            Items.Add(_rightDownItem);
        }

        // Overridden so we can switch to a state where we wait for a pressed key to bind
        public override void Update(GameTime gameTime, InputManager inputManager)
        {
            if (_isWaitingForInput)
            {
                IList<Keys> pressedKeys = inputManager.GetNewPressedKeys();
                if (pressedKeys.Count == 0)
                    return;
                // If the users presses escape, we cancel the input
                if (pressedKeys.Contains(Keys.Escape))
                    ResetKey();
                // Otherwise we bind the pressed key
                else
                    SetKey(pressedKeys[0]);
            }
            else
            {
                // When a control has been selected and the user pressed enter, we go to the input waiting state
                if (inputManager.IsNewKeyDown(Keys.Enter) && SelectedItem.SelectedValue is Keys)
                    WaitForInput();

                base.Update(gameTime, inputManager);
            }
        }

        // Switches to the input waiting state
        private void WaitForInput()
        {
            MenuItemOption option = SelectedItem.SelectedOption;
            option.Text = "...";
            _isWaitingForInput = true;
        }

        // Cancels the input and resets the original key
        private void ResetKey()
        {
            SetKey((Keys)SelectedItem.SelectedValue);
        }

        // Changes the key binding and resets to the normal state
        private void SetKey(Keys key)
        {
            MenuItemOption option = SelectedItem.SelectedOption;
            option.Text = key.ToString();
            option.Value = key;
            _isWaitingForInput = false;
        }

        // Applies the controls
        private void Apply()
        {
            GameSettings settings = _settingsManager.GetSettingsCopy();

            settings.Controls.LeftPaddle.MoveUp = (Keys)_leftUpItem.SelectedValue;
            settings.Controls.LeftPaddle.MoveDown = (Keys)_leftDownItem.SelectedValue;
            settings.Controls.RightPaddle.MoveUp = (Keys)_rightUpItem.SelectedValue;
            settings.Controls.RightPaddle.MoveDown = (Keys)_rightDownItem.SelectedValue;

            _settingsManager.ApplySettings(settings);
            _settingsManager.SaveSettings();

            Close();
        }
    }
}