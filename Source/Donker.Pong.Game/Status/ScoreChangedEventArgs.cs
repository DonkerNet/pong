using System;

namespace Donker.Pong.Game.Status
{
    /// <summary>
    /// Contains information about the previous and new scores of both players after it has been changed.
    /// </summary>
    public class ScoreChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the previous score of the left player.
        /// </summary>
        public int PreviousLeftScore { get; private set; }
        /// <summary>
        /// Gets the new score of the left player.
        /// </summary>
        public int NewLeftScore { get; private set; }
        /// <summary>
        /// Gets the previous score of the right player.
        /// </summary>
        public int PreviousRightScore { get; private set; }
        /// <summary>
        /// Gets the new score of the right player.
        /// </summary>
        public int NewRightScore { get; private set; }

        /// <summary>
        /// Initializes a new instance of <see cref="GameStateChangedEventArgs"/> for the specified previous and new scores.
        /// </summary>
        /// <param name="previousLeftScore">The previous score of the left player.</param>
        /// <param name="newLeftScore">The new score of the left player.</param>
        /// <param name="previousRightScore">The previous score of the right player.</param>
        /// <param name="newRightScore">The new score of the right player.</param>
        public ScoreChangedEventArgs(int previousLeftScore, int newLeftScore, int previousRightScore, int newRightScore)
        {
            PreviousLeftScore = previousLeftScore;
            NewLeftScore = newLeftScore;
            PreviousRightScore = previousRightScore;
            NewRightScore = newRightScore;
        }
    }
}