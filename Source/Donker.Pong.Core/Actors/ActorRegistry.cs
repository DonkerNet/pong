using System.Collections.Generic;
using System.Linq;

namespace Donker.Pong.Common.Actors
{
    /// <summary>
    /// A list containing all actors currently in the game.
    /// </summary>
    public class ActorRegistry : List<IActor>
    {
        /// <summary>
        /// Adds the actors of the specified collection to the end of the list.
        /// </summary>
        /// <param name="actors">The collection with actors that should be added to the end of the list.</param>
        public void AddRange(params IActor[] actors)
        {
            if (actors == null)
                return;
            AddRange(actors.Where(a => a != null));
        }
    }
}