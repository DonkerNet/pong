using Microsoft.Xna.Framework;

namespace Donker.Pong.Common.GameComponents
{
    /// <summary>
    /// Overrides <see cref="DrawableGameComponent"/> to separate <see cref="DrawableGameComponent.LoadContent"/> from <see cref="DrawableGameComponent.Initialize"/>
    /// and make <see cref="DrawableGameComponent.LoadContent"/> and <see cref="DrawableGameComponent.UnloadContent"/> public.
    /// </summary>
    public abstract class ImprovedDrawableGameComponent : DrawableGameComponent
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ImprovedDrawableGameComponent"/> for the specified game.
        /// </summary>
        /// <param name="game">The game this component is for.</param>
        protected ImprovedDrawableGameComponent(Game game)
            : base(game)
        {
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        public override void Initialize()
        {
        }

        /// <summary>
        /// Loads all content needed by this component.
        /// </summary>
        public new virtual void LoadContent()
        {
        }

        /// <summary>
        /// Unloads all prevoisly loaded content used by this component.
        /// </summary>
        public new virtual void UnloadContent()
        {
        }
    }
}