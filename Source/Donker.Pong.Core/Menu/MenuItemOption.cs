using System;

namespace Donker.Pong.Common.Menu
{
    /// <summary>
    /// An option that can be added to a menu item.
    /// </summary>
    public class MenuItemOption
    {
        /// <summary>
        /// Gets or sets the text of this item.
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Gets or sets the value of this item.
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// Gets or sets the action that should be invoked when the item is activated.
        /// </summary>
        public Action Action { get; set; }

        /// <summary>
        /// Invokes this item's action if it has one.
        /// </summary>
        public void InvokeAction()
        {
            if (Action != null)
                Action.Invoke();
        }
    }
}