using System;
using Donker.Pong.Common.Input;
using Donker.Pong.Common.Menu;
using Donker.Pong.Game.ResourceFiles;
using Microsoft.Xna.Framework;

namespace Donker.Pong.Game.Menu
{
    /// <summary>
    /// A confirmation page that gives the user 15 seconds to confirm their changes.
    /// </summary>
    public class ConfirmationMenuPage : MenuPage
    {
        private readonly MenuItem _confirmItem;

        private bool _confirmed;
        private TimeSpan _timeLeft;

        /// <summary>
        /// Event triggered when no confirmation was given.
        /// </summary>
        public event EventHandler Cancel;
        /// <summary>
        /// Event triggered when the user confirmed their changes.
        /// </summary>
        public event EventHandler Confirm;

        /// <summary>
        /// Initializes a new instance of <see cref="ConfirmationMenuPage"/>.
        /// </summary>
        public ConfirmationMenuPage()
            : base(StringResources.MenuTitle_Confirmation)
        {
            _timeLeft = TimeSpan.FromSeconds(15D);

            MenuItem cancelItem = new MenuItem { Name = StringResources.RevertChanges };
            cancelItem.Options.Add(new MenuItemOption { Action = Close });
            Items.Add(cancelItem);

            _confirmItem = new MenuItem {Name = StringResources.ConfirmChanges };
            _confirmItem.Options.Add(new MenuItemOption { Action = HandleConfirmation });
            Items.Add(_confirmItem);
        }

        // Overridden to update the time that is left and show it in a menu item
        public override void Update(GameTime gameTime, InputManager inputManager)
        {
            if (_timeLeft <= TimeSpan.Zero)
            {
                Close();
                return;
            }

            _confirmItem.SelectedOption.Text = Math.Ceiling(_timeLeft.TotalSeconds).ToString("#");

            _timeLeft -= gameTime.ElapsedGameTime;

            base.Update(gameTime, inputManager);
        }

        // Set as confirmed and close the screen
        private void HandleConfirmation()
        {
            _confirmed = true;
            Close();
        }

        // Overridden so that an event is fired on close based on the confirmation state
        protected override void Close()
        {
            base.Close();

            if (_confirmed)
            {
                if (Confirm != null)
                    Confirm.Invoke(this, EventArgs.Empty);
            }
            else
            {
                if (Cancel != null)
                    Cancel.Invoke(this, EventArgs.Empty);
            }
        }
    }
}