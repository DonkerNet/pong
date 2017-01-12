using System;
using Donker.Pong.Common.Shapes;
using Donker.Pong.Game.Settings;

namespace Donker.Pong.Game.Status
{
    /// <summary>
    /// Contains information about the currect state of the game.
    /// </summary>
    public class GameInfo
    {
        private GameState _state;
        private int _leftScore;
        private int _rightScore;

        /// <summary>
        /// An event that is triggered when the state of the game changes.
        /// </summary>
        public event EventHandler<GameStateChangedEventArgs> StateChanged;
        /// <summary>
        /// An event that is triggered when the score of a player changes.
        /// </summary>
        public event EventHandler<ScoreChangedEventArgs> ScoreChanged;

        /// <summary>
        /// Gets or sets the state of the game.
        /// </summary>
        public GameState State
        {
            get { return _state; }
            set
            {
                GameState previous = _state;

                if (previous == value)
                    return;
                
                _state = value;

                if (StateChanged != null)
                    StateChanged.Invoke(this, new GameStateChangedEventArgs(previous, value));
            }
        }

        /// <summary>
        /// Gets or sets the score for the left paddle.
        /// </summary>
        public int LeftScore
        {
            get { return _leftScore; }
            set
            {
                int previous = _leftScore;

                if (previous == value)
                    return;

                _leftScore = value;

                if (ScoreChanged != null)
                    ScoreChanged.Invoke(this, new ScoreChangedEventArgs(previous, value, _rightScore, _rightScore));
            }
        }

        /// <summary>
        /// Gets or sets the score for the right paddle.
        /// </summary>
        public int RightScore
        {
            get { return _rightScore; }
            set
            {
                int previous = _rightScore;

                if (previous == value)
                    return;

                _rightScore = value;

                if (ScoreChanged != null)
                    ScoreChanged.Invoke(this, new ScoreChangedEventArgs(_leftScore, _leftScore, previous, value));
            }
        }

        /// <summary>
        /// Gets or sets the game speed.
        /// </summary>
        public float Speed { get; set; }
        /// <summary>
        /// Gets or sets the bounds in which the actors should move.
        /// </summary>
        /// <remarks>
        /// This basically is the base resolution the game is programmed to work with.
        /// </remarks>
        public RectangleF Bounds { get; set; }
        /// <summary>
        /// Gets or sets the scale that represents the difference between the game's bounds and resolution.
        /// </summary>
        public float Scale { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameInfo"/> class.
        /// </summary>
        public GameInfo()
        {
            Bounds = SettingsConstants.DefaultBounds; // The default bounds which the game is programmed for
            Scale = 1.0F; // Default scale
        }

        /// <summary>
        /// Resets the score for both the left and right players. Does not invoke the <see cref="ScoreChanged"/> event.
        /// </summary>
        public void ResetScores()
        {
            _leftScore = 0;
            _rightScore = 0;
        }
    }
}