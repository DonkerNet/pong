using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace Donker.Pong.Common.Input
{
    /// <summary>
    /// Stores the current and previous keyboard states, which allows you to check for any changes between them.
    /// </summary>
    public class InputManager
    {
        private KeyboardState _previousKeyboardState;
        private KeyboardState _currentKeyboardState;

        /// <summary>
        /// Initializes a new instance of the <see cref="InputManager"/> class.
        /// </summary>
        public InputManager()
        {
            _currentKeyboardState = Keyboard.GetState();
            _previousKeyboardState = _currentKeyboardState;
        }

        /// <summary>
        /// Updates the current and previous keyboard states.
        /// </summary>
        /// <param name="keyboardState"></param>
        public void UpdateKeyboardState(KeyboardState keyboardState)
        {
            _previousKeyboardState = _currentKeyboardState;
            _currentKeyboardState = keyboardState;
        }

        /// <summary>
        /// Checks if a key is pressed down.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns><c>true</c> if pressed; otherwise, <c>false</c>.</returns>
        public bool IsKeyDown(Keys key)
        {
            return _currentKeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Checks if a key is pressed down but was not pressed previously.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns><c>true</c> if pressed for the first time; otherwise, <c>false</c>.</returns>
        public bool IsNewKeyDown(Keys key)
        {
            return _currentKeyboardState.IsKeyDown(key)
                && !_previousKeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Checks if a key is not being pressed.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns><c>true</c> when not pressed; otherwise, <c>false</c>.</returns>
        public bool IsKeyUp(Keys key)
        {
            return _currentKeyboardState.IsKeyUp(key);
        }

        /// <summary>
        /// Checks if a key is not being pressed but was pressed previously.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns><c>true</c> when not pressed for the first time; otherwise, <c>false</c>.</returns>
        public bool IsNewKeyUp(Keys key)
        {
            return _currentKeyboardState.IsKeyUp(key)
                && !_previousKeyboardState.IsKeyUp(key);
        }

        /// <summary>
        /// Returns a collection of values holding keys that are currently being pressed.
        /// </summary>
        /// <returns>The keys that are currently being pressed.</returns>
        public IList<Keys> GetPressedKeys()
        {
            return _currentKeyboardState.GetPressedKeys();
        }

        /// <summary>
        /// Returns a collection of values holding keys that are currently being pressed but were not pressed previously.
        /// </summary>
        /// <returns>The keys that are currently being pressed but were not pressed previously.</returns>
        public IList<Keys> GetNewPressedKeys()
        {
            Keys[] previousPressedKeys = _previousKeyboardState.GetPressedKeys();
            Keys[] currentPressedKeys = _currentKeyboardState.GetPressedKeys();
            return currentPressedKeys
                .Where(k => !previousPressedKeys.Contains(k))
                .ToList();
        }
    }
}