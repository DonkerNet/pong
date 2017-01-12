using System;
using System.Collections.Generic;
using Donker.Pong.Common.Menu;
using Donker.Pong.Game.Actors.Paddles;
using Donker.Pong.Game.ResourceFiles;
using Donker.Pong.Game.Settings;
using Donker.Pong.Game.Settings.Entities;
using Donker.Pong.Game.Status;

namespace Donker.Pong.Game.Menu
{
    /// <summary>
    /// The menu page where you can setup and start a new game.
    /// </summary>
    public class NewGameMenuPage : MenuPage
    {
        private readonly GameInfo _gameInfo;
        private readonly SettingsManager _settingsManager;

        private MenuItem _playersItem;
        private MenuItem _scoreLimitItem;
        private MenuItem _timeLimitItem;
        private MenuItem _ballCountItem;
        private MenuItem _gameSpeedItem;

        /// <summary>
        /// Initializes a new instance of <see cref="NewGameMenuPage"/> using the specified settings manager.
        /// </summary>
        /// <param name="gameInfo">The information about the game's state.</param>
        /// <param name="settingsManager">The settings manager to use when reading and applying settings.</param>
        public NewGameMenuPage(GameInfo gameInfo, SettingsManager settingsManager)
            : base(StringResources.MenuTitle_NewGame)
        {
            _gameInfo = gameInfo;
            _settingsManager = settingsManager;

            MenuItem playItem = new MenuItem { Name = StringResources.Play };
            playItem.Options.Add(new MenuItemOption { Action = Play });
            Items.Add(playItem);

            CreateGameplaySettingItems();

            MenuItem cancelItem = new MenuItem { Name = StringResources.Cancel };
            cancelItem.Options.Add(new MenuItemOption { Action = Close });
            Items.Add(cancelItem);
        }

        // Sets up the options to show on the page
        private void CreateGameplaySettingItems()
        {
            IGameplaySettings gameplaySettings = _settingsManager.Settings.Gameplay;

            // Add player type selection

            IList<Type> paddleTypes = PaddleFactory.GetSupportedTypes();

            _playersItem = new MenuItem { Name = StringResources.Players };
            MenuItemOption selectedPlayerOption = null;

            // Add all available left+right player combinations, for example: "AI vs. Player"
            foreach (Type leftPaddleType in paddleTypes)
            {
                foreach (Type rightPaddleType in paddleTypes)
                {
                    string text = string.Format("{0} {1} {2}",
                        StringResources.ResourceManager.GetString("MenuText_Paddle_" + leftPaddleType.Name),
                        StringResources.versus,
                        StringResources.ResourceManager.GetString("MenuText_Paddle_" + rightPaddleType.Name));

                    Tuple<Type, Type> value = new Tuple<Type, Type>(leftPaddleType, rightPaddleType);
                    MenuItemOption option = new MenuItemOption { Text = text, Value = value };
                    _playersItem.Options.Add(option);

                    if (leftPaddleType == gameplaySettings.LeftPaddleType && rightPaddleType == gameplaySettings.RightPaddleType)
                        selectedPlayerOption = option;
                }
            }

            if (selectedPlayerOption != null)
                _playersItem.SelectedOption = selectedPlayerOption;
            Items.Add(_playersItem);

            // Add score limit item

            _scoreLimitItem = new MenuItem { Name = StringResources.ScoreLimit };
            _scoreLimitItem.Options.Add(new MenuItemOption { Text = StringResources.None, Value = 0 });
            foreach (int scoreLimit in new[] { 5, 10, 20, 30 })
            {
                string text = string.Format("{0} {1}", scoreLimit, StringResources.pts);
                _scoreLimitItem.Options.Add(new MenuItemOption { Text = text, Value = scoreLimit });
            }

            _scoreLimitItem.SelectedValue = gameplaySettings.ScoreLimit;
            Items.Add(_scoreLimitItem);

            // Add time limit item

            _timeLimitItem = new MenuItem { Name = StringResources.TimeLimit };
            _timeLimitItem.Options.Add(new MenuItemOption { Text = StringResources.None, Value = TimeSpan.Zero });
            foreach (int timeLimit in new[] { 5, 10, 20, 30 })
            {
                string text = string.Format("{0} {1}", timeLimit, StringResources.minutes);
                _timeLimitItem.Options.Add(new MenuItemOption { Text = text, Value = TimeSpan.FromMinutes(timeLimit) });
            }

            _timeLimitItem.SelectedValue = gameplaySettings.TimeLimit;
            Items.Add(_timeLimitItem);

            // Add ball count item

            _ballCountItem = new MenuItem { Name = StringResources.Balls };
            for (int i = SettingsConstants.BallMinCount; i <= SettingsConstants.BallMaxCount; i++)
            {
                _ballCountItem.Options.Add(new MenuItemOption { Text = i.ToString(), Value = i });
            }

            _ballCountItem.SelectedValue = gameplaySettings.BallCount;
            Items.Add(_ballCountItem);

            // Add game speed item

            _gameSpeedItem = new MenuItem { Name = StringResources.GameSpeed };
            for (float i = SettingsConstants.GameSpeedMinValue; i <= SettingsConstants.GameSpeedMaxValue; i = (float)Math.Round(i + SettingsConstants.GameSpeedIncreaseFactor, 1))
            {
                string text = string.Format("{0}%", (int)Math.Round(i * 100F, 1));
                _gameSpeedItem.Options.Add(new MenuItemOption { Text = text, Value = i });
            }
            _gameSpeedItem.Options.Add(new MenuItemOption { Text = StringResources.AutoIncrease, Value = null });

            _gameSpeedItem.SelectedValue = gameplaySettings.AutoIncreaseSpeed ? null : (object)gameplaySettings.GameSpeed;
            Items.Add(_gameSpeedItem);
        }

        // Applies the gameplay settings and starts the game
        private void Play()
        {
            GameSettings settings = _settingsManager.GetSettingsCopy();

            Tuple<Type, Type> paddleTypes = (Tuple<Type, Type>)_playersItem.SelectedValue;

            settings.Gameplay.LeftPaddleType = paddleTypes.Item1;
            settings.Gameplay.RightPaddleType = paddleTypes.Item2;
            settings.Gameplay.ScoreLimit = (int)_scoreLimitItem.SelectedValue;
            settings.Gameplay.TimeLimit = (TimeSpan)_timeLimitItem.SelectedValue;
            settings.Gameplay.BallCount = (int)_ballCountItem.SelectedValue;

            // When 'auto increase' has been selected as speed, enabled it and start at the default speed
            if (_gameSpeedItem.SelectedValue == null)
            {
                settings.Gameplay.GameSpeed = SettingsConstants.GameSpeedMinValue;
                settings.Gameplay.AutoIncreaseSpeed = true;
            }
            else
            {
                settings.Gameplay.GameSpeed = (float)_gameSpeedItem.SelectedValue;
                settings.Gameplay.AutoIncreaseSpeed = false;
            }
            
            _settingsManager.ApplySettings(settings);
            _settingsManager.SaveSettings();

            // Change the game's state to start the game
            _gameInfo.State = GameState.InProgress;

            Close();
        }
    }
}