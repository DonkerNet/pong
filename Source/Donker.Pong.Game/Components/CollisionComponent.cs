using System.Collections.Generic;
using Donker.Pong.Common.Actors;
using Donker.Pong.Common.Collisions;
using Donker.Pong.Game.Status;
using Microsoft.Xna.Framework;

namespace Donker.Pong.Game.Components
{
    /// <summary>
    /// Manages collisions between actors and the screen bounds in the game.
    /// </summary>
    public class CollisionComponent : GameComponent
    {
        private GameInfo _gameInfo;
        private ActorRegistry _actorRegistry;

        public CollisionComponent(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            _gameInfo = Game.Services.GetService<GameInfo>();
            _actorRegistry = Game.Services.GetService<ActorRegistry>();

            _gameInfo.StateChanged += GameInfoOnStateChanged;
        }

        // Only enable collision checking when a game is active
        private void GameInfoOnStateChanged(object sender, GameStateChangedEventArgs e)
        {
            switch (e.NewState)
            {
                case GameState.InProgress:
                    Enabled = true;
                    break;
                default:
                    Enabled = false;
                    break;
            }
        }

        public override void Update(GameTime gameTime)
        {
            IReadOnlyList<IActor> actors = _actorRegistry.GetAll();
            int actorCount = actors.Count;

            for (int i = 0; i < actorCount; i++)
            {
                IActor firstCollider = actors[i];

                // Check the collision with the edge of the game grid.
                Vector2 outOfBoundsDistance;
                if (BoxCollisionDetection.CheckBoundsCollision(firstCollider, _gameInfo.Bounds, out outOfBoundsDistance))
                    firstCollider.OnBoundsCollision(outOfBoundsDistance);

                for (int j = 0; j < i; j++)
                {
                    IActor secondCollider = actors[j];

                    Vector2 intersection;

                    // Check the collision for the first collider.
                    if (BoxCollisionDetection.CheckColliderCollision(firstCollider, secondCollider, out intersection))
                    {
                        // We inform the first collider that a collision happened, allowing itself to adjust it's position or do other things.
                        firstCollider.OnColliderCollision(secondCollider, intersection);

                        // By now the first collider may or may not have adjusted itself. Either way, we need the up to date intersection data for the second collider so we can supply it to it's OnColliderCollision method.
                        BoxCollisionDetection.CheckColliderCollision(secondCollider, firstCollider, out intersection);
                        secondCollider.OnColliderCollision(firstCollider, intersection);
                    }
                }
            }
        }
    }
}