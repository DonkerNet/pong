using System.Collections.Generic;
using Donker.Pong.Common.Input;
using Microsoft.Xna.Framework.Input;

namespace Donker.Pong.Common.Menu
{
    /// <summary>
    /// An item for a menu page.
    /// </summary>
    public class MenuItem
    {
        private int _selectedOptionIndex;

        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets a list op options selectable for this item.
        /// </summary>
        public List<MenuItemOption> Options { get; private set; }
        /// <summary>
        /// Gets or sets the index of the selected option.
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                if (_selectedOptionIndex >= Options.Count)
                {
                    if (Options.Count > 0)
                        _selectedOptionIndex = Options.Count - 1;
                    else
                        _selectedOptionIndex = 0;
                }
                return _selectedOptionIndex;
            }
            set
            {
                if (value < 0 || Options.Count == 0)
                    _selectedOptionIndex = 0;
                else if (value >= Options.Count)
                    _selectedOptionIndex = Options.Count - 1;
                else
                    _selectedOptionIndex = value;
            }
        }
        /// <summary>
        /// Gets or sets the selected option.
        /// </summary>
        public MenuItemOption SelectedOption
        {
            get
            {
                return Options.Count > 0
                    ? Options[SelectedIndex]
                    : null;
            }
            set
            {
                if (value == null)
                    return;
                int index = Options.IndexOf(value);
                if (index >= 0)
                    _selectedOptionIndex = index;
            }
        }
        /// <summary>
        /// Gets the value of the selected option, or sets the selected option by specifying it's value.
        /// </summary>
        public object SelectedValue
        {
            get
            {
                MenuItemOption option = SelectedOption;
                if (option != null)
                    return option.Value;
                return null;
            }
            set
            {
                int index = Options.FindIndex(o => Equals(o.Value, value));
                if (index >= 0)
                    _selectedOptionIndex = index;
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="MenuItem"/>.
        /// </summary>
        public MenuItem()
        {
            Options = new List<MenuItemOption>();
        }

        /// <summary>
        /// Manages option selection and invocation when specific keys are pressed.
        /// </summary>
        /// <param name="inputManager">The input manager to use when retrieving the key states.</param>
        public virtual void Update(InputManager inputManager)
        {
            if (inputManager.IsNewKeyDown(Keys.A) || inputManager.IsNewKeyDown(Keys.Left))
            {
                if (--_selectedOptionIndex < 0)
                {
                    if (Options.Count > 0)
                        _selectedOptionIndex = Options.Count - 1;
                    else
                        _selectedOptionIndex = 0;
                }
            }
            else if (inputManager.IsNewKeyDown(Keys.D) || inputManager.IsNewKeyDown(Keys.Right))
            {
                if (++_selectedOptionIndex >= Options.Count)
                    _selectedOptionIndex = 0;
            }
            else if (inputManager.IsNewKeyDown(Keys.Enter))
            {
                MenuItemOption selectedOption = SelectedOption;
                if (selectedOption != null)
                    SelectedOption.InvokeAction();
            }
        }

        /// <summary>
        /// Gets the full text of the item and its selected option to show on the menu page.
        /// </summary>
        /// <param name="isSelected">Specifies if the current menu item is selected.</param>
        /// <returns>A <see cref="string"/> representation of this item.</returns>
        public string ToString(bool isSelected)
        {
            MenuItemOption selectedOption = SelectedOption;

            string text;

            if (string.IsNullOrEmpty(Name))
                text = selectedOption != null ? selectedOption.Text : string.Empty;
            else if (selectedOption == null || string.IsNullOrEmpty(selectedOption.Text))
                text = Name;
            else
                text = string.Format("{0}: {1}", Name, selectedOption.Text);

            if (isSelected)
            {
                if (Options.Count > 1)
                    text = string.Format("<{0}>", text);
                else
                    text = string.Format("[{0}]", text);
            }

            return text;
        }

        public override string ToString()
        {
            return ToString(false);
        }
    }
}