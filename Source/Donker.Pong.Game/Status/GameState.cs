namespace Donker.Pong.Game.Status
{
    /// <summary>
    /// Describes the state of the game.
    /// </summary>
    public enum GameState
    {
        /// <summary>
        /// No valid state has yet been set for the game.
        /// </summary>
        None = 0,
        /// <summary>
        /// The game has not been started yet and is still in the main menu.
        /// </summary>
        InMenu = 1,
        /// <summary>
        /// The game is in progress.
        /// </summary>
        InProgress = 2,
        /// <summary>
        /// The game is currently paused.
        /// </summary>
        Paused = 3,
        /// <summary>
        /// The game is over.
        /// </summary>
        Ended = 4
    }
}