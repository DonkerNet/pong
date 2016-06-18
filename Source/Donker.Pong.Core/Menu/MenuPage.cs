using System;
using System.Collections.Generic;
using Donker.Pong.Common.Input;
using Donker.Pong.Common.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Donker.Pong.Common.Menu
{
    /// <summary>
    /// Base class for a page to show in the main menu of the game.
    /// </summary>
    public abstract class MenuPage
    {
        private int _selectedItemIndex;
        private Action _onClosedCallBack;
        private MenuPage _subPage;
        private string _title;

        /// <summary>
        /// Gets or sets the title of the page.
        /// </summary>
        protected string Title
        {
            get { return _title; }
            set { _title = value ?? string.Empty; }
        }
        /// <summary>
        /// Gets the list of items to show on the menu page.
        /// </summary>
        protected IList<MenuItem> Items { get; private set; }
        /// <summary>
        /// Gets or sets the index of the selected item.
        /// </summary>
        protected int SelectedIndex
        {
            get
            {
                if (_selectedItemIndex >= Items.Count)
                {
                    if (Items.Count > 0)
                        _selectedItemIndex = Items.Count - 1;
                    else
                        _selectedItemIndex = 0;
                }
                return _selectedItemIndex;
            }
        }
        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        protected MenuItem SelectedItem
        {
            get
            {
                return Items.Count > 0
                    ? Items[SelectedIndex]
                    : null;
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="MenuPage"/>.
        /// </summary>
        protected MenuPage(string title)
        {
            Title = title;
            Items = new List<MenuItem>();
        }

        /// <summary>
        /// Opens a new sub page of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of page to open.</typeparam>
        protected void OpenSubPage<T>()
            where T : MenuPage, new()
        {
            OpenSubPage(new T());
        }

        /// <summary>
        /// Opens a new sub page using the specified constructor function.
        /// </summary>
        /// <param name="constructor">The function to invoke to construct the new page.</param>
        /// <typeparam name="T">The type of page to open.</typeparam>
        protected void OpenSubPage<T>(Func<T> constructor)
            where T : MenuPage
        {
            OpenSubPage(constructor.Invoke());
        }

        /// <summary>
        /// Opens the specified page as a sub page.
        /// </summary>
        /// <typeparam name="T">The type of page to open.</typeparam>
        /// <param name="subPage">The page to open.</param>
        protected void OpenSubPage<T>(T subPage)
            where T : MenuPage
        {
            if (subPage == null)
                return;
            _subPage = subPage;
            _subPage._onClosedCallBack = CloseSubPage;
        }

        /// <summary>
        /// Closes a sub page when it was opened.
        /// </summary>
        protected virtual void CloseSubPage()
        {
            _subPage = null;
        }

        /// <summary>
        /// Invokes an action to close this page.
        /// </summary>
        protected virtual void Close()
        {
            if (_onClosedCallBack != null)
                _onClosedCallBack.Invoke();
        }

        /// <summary>
        /// Handles the input for this page and updates the sub page when opened.
        /// </summary>
        /// <param name="gameTime">The information about the game's timings.</param>
        /// <param name="inputManager">The input manager to use when retrieving the key states.</param>
        public virtual void Update(GameTime gameTime, InputManager inputManager)
        {
            if (_subPage != null)
            {
                _subPage.Update(gameTime, inputManager);
                return;
            }

            if (inputManager.IsNewKeyDown(Keys.W) || inputManager.IsNewKeyDown(Keys.Up))
            {
                if (--_selectedItemIndex < 0)
                {
                    if (Items.Count > 0)
                        _selectedItemIndex = Items.Count - 1;
                    else
                        _selectedItemIndex = 0;
                }
            }
            else if (inputManager.IsNewKeyDown(Keys.S) || inputManager.IsNewKeyDown(Keys.Down))
            {
                if (++_selectedItemIndex >= Items.Count)
                    _selectedItemIndex = 0;
            }
            else if (inputManager.IsNewKeyDown(Keys.Escape))
            {
                Close();
            }

            MenuItem item = SelectedItem;
            if (item != null)
                item.Update(inputManager);
        }

        /// <summary>
        /// Draws the page (or sub page, if selected) and it's items to the screen.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used for drawing.</param>
        /// <param name="titleFont">The font to use when drawing the title.</param>
        /// <param name="itemFont">The font to use when drawing the items.</param>
        /// <param name="bounds">The screen bounds.</param>
        public virtual void Draw(SpriteBatch spriteBatch, SpriteFont titleFont, SpriteFont itemFont, RectangleF bounds)
        {
            if (_subPage != null)
            {
                _subPage.Draw(spriteBatch, titleFont, itemFont, bounds);
                return;
            }

            Vector2 center = bounds.Center;

            Vector2 titleSize = titleFont.MeasureString(Title);
            Vector2 titlePosition = new Vector2(center.X - titleSize.X / 2F, titleSize.Y / 2F);

            spriteBatch.DrawString(titleFont, Title, titlePosition, Color.White);

            float itemStartPosY = titlePosition.Y + titleSize.Y * 2F;

            for (int i = 0; i < Items.Count; ++i)
            {
                MenuItem item = Items[i];
                string itemText = item.ToString(i == SelectedIndex);

                Vector2 itemTextSize = itemFont.MeasureString(itemText);

                spriteBatch.DrawString(
                    itemFont,
                    itemText,
                    new Vector2(
                        center.X - itemTextSize.X / 2F,
                        itemStartPosY + i * (1.5F * itemTextSize.Y)), 
                    Color.White);
            }
        }
    }
}