using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        /// <summary>
        /// Attempts to return a humanly readable description for the specified key.
        /// </summary>
        /// <param name="key">The key to describe.</param>
        /// <returns>The description as a <see cref="string"/>.</returns>
        public static string GetKeyDescription(Keys key)
        {
            switch (key)
            {
                case Keys.Back:
                    return "Backspace";
                case Keys.ImeConvert:
                    return "IME Convert";
                case Keys.ImeNoConvert:
                    return "IME No Convert";
                case Keys.Multiply:
                    return "Num Pad *";
                case Keys.Add:
                    return "Num Pad +";
                case Keys.Subtract:
                    return "Num Pad -";
                case Keys.Decimal:
                    return "Num Pad .";
                case Keys.Divide:
                    return "Num Pad /";
                case Keys.OemSemicolon:
                    return ";";
                case Keys.OemPlus:
                    return "=";
                case Keys.OemComma:
                    return ",";
                case Keys.OemMinus:
                    return "-";
                case Keys.OemPeriod:
                    return ".";
                case Keys.OemQuestion:
                    return "/";
                case Keys.OemTilde:
                    return "`";
                case Keys.OemOpenBrackets:
                    return "[";
                case Keys.OemPipe:
                    return "\\";
                case Keys.OemCloseBrackets:
                    return "]";
                case Keys.OemQuotes:
                    return "'";
                case Keys.OemBackslash:
                    return "\\";
                case Keys.Oem8:
                    return key.ToString();
                case Keys.OemCopy:
                    return "Copy";
                case Keys.OemAuto:
                    return "Auto";
                case Keys.OemEnlW:
                    return "Enlarge Window";
                case Keys.Crsel:
                    return "CrSel";
                case Keys.Exsel:
                    return "ExSel";
                case Keys.EraseEof:
                    return "Erase EOF";
                case Keys.Pa1:
                    return "PA1";
                case Keys.OemClear:
                    return "Clear";
            }

            if (key >= Keys.D0 && key <= Keys.D9)
                return key.ToString().Substring(1, 1);

            string keyString = key.ToString();

            StringBuilder descriptionBuilder = new StringBuilder();

            for (int i = 0; i < keyString.Length; i++)
            {
                char c = keyString[i];

                if (i > 0 && (char.IsUpper(c) || char.IsNumber(c)))
                    descriptionBuilder.Append(' ');

                descriptionBuilder.Append(c);
            }

            return descriptionBuilder.ToString();
        }
    }
}