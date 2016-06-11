using System;

namespace Donker.Pong.Game.Status
{
    /// <summary>
    /// Contains information about the previous game state and the new one after it has been changed.
    /// </summary>
    public class GameStateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the previous game state.
        /// </summary>
        public GameState PreviousState { get; private set; }

        /// <summary>
        /// Gets the new state of the game.
        /// </summary>
        public GameState NewState { get; private set; }

        /// <summary>
        /// Initializes a new instance of <see cref="GameStateChangedEventArgs"/> for the specified previous state and the new one.
        /// </summary>
        /// <param name="previousState">The previous game state.</param>
        /// <param name="newState">The new state of the game.</param>
        public GameStateChangedEventArgs(GameState previousState, GameState newState)
        {
            PreviousState = previousState;
            NewState = newState;
        }
    }
}