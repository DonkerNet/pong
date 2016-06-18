using System;
using System.IO;
using Donker.Pong.Common.Shapes;
using Microsoft.Xna.Framework;

namespace Donker.Pong.Game.Settings
{
    /// <summary>
    /// Contains read-only settings used throughout the game.
    /// </summary>
    public class SettingsConstants
    {
        /// <summary>
        /// <para>The full path and name of the file where the settings should be stored in.</para>
        /// <para>Value = Documents/MyGames/Donker.Pong/settings.json</para>
        /// </summary>
        public static string SettingsFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games/DonkerPONG/settings.json");

        #region Screen settings

        /// <summary>
        /// <para>The default resolution of the game.</para>
        /// <para>Value = 1024F, 768F</para>
        /// </summary>
        public static Vector2 DefaultResolution = new Vector2(1024F, 768F);
        /// <summary>
        /// <para>The default bounds the game was programmed for.</para>
        /// <para>Value = 0F, 0F, 1024F, 768F</para>
        /// </summary>
        public static RectangleF DefaultBounds = new RectangleF(0F, 0F, 1024F, 768F);

        #endregion

        #region Score settings

        /// <summary>
        /// A margin to add around the score when drawing it to the screen.
        /// </summary>
        public const float ScoreTextMargin = 24F;

        #endregion

        #region Background settings

        /// <summary>
        /// The thickness of one of the background blocks.
        /// </summary>
        public const float BackgroundBlockThickness = 8F;
        /// <summary>
        /// The length of one of the background blocks.
        /// </summary>
        public const float BackgroundBlockLength = 40F;
        /// <summary>
        /// The margin added to both ends of a background block.
        /// </summary>
        public const float BackgroundBlockMargin = 10F;

        #endregion

        #region Game speed settings

        /// <summary>
        /// <para>The interval for increasing the game speed.</para>
        /// <para>Value = 00:00:30.000</para>
        /// </summary>
        public static TimeSpan GameSpeedIncreaseInterval = TimeSpan.FromSeconds(30D);
        /// <summary>
        /// The amount to add when increasing the game speed.
        /// </summary>
        public const float GameSpeedIncreaseFactor = 0.1F;
        /// <summary>
        /// The minimum speed the game may have.
        /// </summary>
        public const float GameSpeedMinValue = 1.0F;
        /// <summary>
        /// The maximum speed the game may have.
        /// </summary>
        public const float GameSpeedMaxValue = 2.0F;

        #endregion

        #region Ball settings

        /// <summary>
        /// The minimum number of balls that may be present in a game.
        /// </summary>
        public const int BallMinCount = 1;
        /// <summary>
        /// The maximum number of balls that may be present in a game.
        /// </summary>
        public const int BallMaxCount = 2;
        /// <summary>
        /// <para>The size of a ball.</para>
        /// <para>Value = 24F, 24F</para>
        /// </summary>
        public static Vector2 BallSize = new Vector2(24F, 24F);
        /// <summary>
        /// <para>The padding to add when checking if a ball collides with the game's bounds.</para>
        /// <para>Value = 0F, 16F</para>
        /// </summary>
        public static Vector2 BallBoundsPadding = new Vector2(0F, 16F);
        /// <summary>
        /// The base speed of a ball.
        /// </summary>
        public const float BallSpeed = 10F;
        /// <summary>
        /// <para>The time it should take before a ball can move after it has been reset.</para>
        /// <para>Value = 00:00:01.500</para>
        /// </summary>
        public static TimeSpan BallResetTime = TimeSpan.FromMilliseconds(1500D);
        /// <summary>
        /// The minimum angle a ball may have when facing left.
        /// </summary>
        public const int BallLeftDirectionAngleStart = 135;
        /// <summary>
        /// The minimum angle a ball may have when facing right.
        /// </summary>
        public const int BallRightDirectionAngleStart = 315;
        /// <summary>
        /// The maximum range the angle can differ from it's minimum value.
        /// </summary>
        public const int BallAngleRange = 90;

        #endregion

        #region Paddle settings

        /// <summary>
        /// <para>The size of a paddle.</para>
        /// <para>Value = 24F, 96F</para>
        /// </summary>
        public static Vector2 PaddleSize = new Vector2(24F, 96F);
        /// <summary>
        /// <para>The padding to add when checking if a paddle collides with the game's bounds.</para>
        /// <para>Value = 32F, 24F</para>
        /// </summary>
        public static Vector2 PaddleBoundsPadding = new Vector2(32F, 24F);
        /// <summary>
        /// The base speed of a paddle.
        /// </summary>
        public const float PaddleSpeed = 10F;

        #endregion
    }
}