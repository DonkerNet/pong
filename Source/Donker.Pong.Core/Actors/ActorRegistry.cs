using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Donker.Pong.Common.Actors
{
    /// <summary>
    /// A list containing all actors currently in the game.
    /// </summary>
    public class ActorRegistry
    {
        private List<IActor> _actors;

        /// <summary>
        /// Initializes a new instance of <see cref="ActorRegistry"/>.
        /// </summary>
        public ActorRegistry()
        {
            _actors = new List<IActor>();
        }

        /// <summary>
        /// Adds the actors of the specified collection to the registry.
        /// </summary>
        /// <param name="actors">The collection with actors that should be added to the registry.</param>
        public void AddRange(IEnumerable<IActor> actors)
        {
            if (actors == null)
                return;

            // We create a new list instance so that the previous instance does not change, as that break any currently running enumerations

            List<IActor> newActorList = new List<IActor>(_actors);
            newActorList.AddRange(actors.Where(a => a != null));
            _actors = newActorList;
        }

        /// <summary>
        /// Adds the actors of the specified collection to the registry.
        /// </summary>
        /// <param name="actors">The collection with actors that should be added to the registry.</param>
        public void AddRange(params IActor[] actors)
        {
            AddRange((IEnumerable<IActor>)actors);
        }

        /// <summary>
        /// Returns a read-only list containg all actors.
        /// </summary>
        /// <returns>The actor list as a <see cref="IReadOnlyList{T}"/>.</returns>
        public IReadOnlyList<IActor> GetAll()
        {
            return new ReadOnlyCollection<IActor>(_actors);
        }

        /// <summary>
        /// Clears the actors from the registry.
        /// </summary>
        public void Clear()
        {
            _actors = new List<IActor>();
        }
    }
}