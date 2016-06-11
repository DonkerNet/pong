using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Donker.Pong.Common.Settings
{
    /// <summary>
    /// Class for storing settings in a file as JSON.
    /// </summary>
    public class JsonFileSettingsStorage : ISettingsStorage
    {
        private readonly string _settingsFileDirectory;
        private readonly string _settingsFilePath;
        private readonly JsonSerializerSettings _serializerSettings;

        /// <summary>
        /// Initializes a new instance of <see cref="JsonFileSettingsStorage"/> using the specified file path.
        /// </summary>
        /// <param name="settingsFilePath">The path where the settings should be loaded and saved.</param>
        public JsonFileSettingsStorage(string settingsFilePath)
        {
            if (settingsFilePath == null)
                throw new ArgumentNullException("settingsFilePath", "The settings file path cannot be null.");
            if (settingsFilePath.Length == 0)
                throw new ArgumentException("The settings file path cannot be empty.", "settingsFilePath");

            string directory = Path.GetDirectoryName(settingsFilePath);
            string fileName = Path.GetFileName(settingsFilePath);

            if (string.IsNullOrWhiteSpace(directory) || string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("The settings file path is invalid.", "settingsFilePath");

            _settingsFileDirectory = directory;
            _settingsFilePath = settingsFilePath;

            _serializerSettings = new JsonSerializerSettings
            {
                Culture = CultureInfo.InvariantCulture,
                Formatting = Formatting.Indented,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Include,
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
                DefaultValueHandling = DefaultValueHandling.Include
            };

            // Ignore any serialization errors
            _serializerSettings.Error += (sender, args) =>
            {
                args.ErrorContext.Handled = true;
            };

            // Save enums as readable strings
            _serializerSettings.Converters.Add(new StringEnumConverter());
        }

        /// <summary>
        /// Stores a settings object.
        /// </summary>
        /// <typeparam name="TObject">The type of the settings object to store.</typeparam>
        /// <param name="obj">The settings object to store.</param>
        public void Save<TObject>(TObject obj)
        {
            if (!Directory.Exists(_settingsFileDirectory))
                Directory.CreateDirectory(_settingsFileDirectory);

            using (Stream stream = File.Create(_settingsFilePath))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    if (!ReferenceEquals(null, obj))
                    {
                        string json = JsonConvert.SerializeObject(obj, _serializerSettings);
                        writer.Write(json);
                    }
                }
            }
        }

        /// <summary>
        /// Loads a settings object from the storage.
        /// </summary>
        /// <typeparam name="TObject">The type of the settings object to load.</typeparam>
        /// <returns>The loaded settings object.</returns>
        public TObject Load<TObject>()
        {
            if (!File.Exists(_settingsFilePath))
                return default(TObject);

            TObject obj;

            using (Stream stream = File.OpenRead(_settingsFilePath))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    obj = !string.IsNullOrWhiteSpace(json)
                        ? JsonConvert.DeserializeObject<TObject>(json, _serializerSettings)
                        : default(TObject);
                }
            }

            return obj;
        }
    }
}