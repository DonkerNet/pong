namespace Donker.Pong.Common.Settings
{
    /// <summary>
    /// Interface for classes that store settings.
    /// </summary>
    public interface ISettingsStorage
    {
        /// <summary>
        /// Stores a settings object.
        /// </summary>
        /// <typeparam name="TObject">The type of the settings object to store.</typeparam>
        /// <param name="obj">The settings object to store.</param>
        void Save<TObject>(TObject obj);
        /// <summary>
        /// Loads a settings object from the storage.
        /// </summary>
        /// <typeparam name="TObject">The type of the settings object to load.</typeparam>
        /// <returns>The loaded settings object.</returns>
        TObject Load<TObject>();
    }
}