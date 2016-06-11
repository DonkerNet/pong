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
            // The buffer is to make sure that a collision check between two colliders only happens once.
            List<IActor> buffer = new List<IActor>();

            foreach (IActor collider in _actorRegistry)
            {
                // Check the collision with the edge of the game grid.
                Vector2 outOfBoundsDistance;
                if (BoxCollisionDetection.CheckBoundsCollision(collider, _gameInfo.Bounds, out outOfBoundsDistance))
                    collider.OnBoundsCollision(outOfBoundsDistance);

                foreach (IActor bufferedCollider in buffer)
                {
                    Vector2 intersection;

                    // Check the collision for the first collider.
                    if (BoxCollisionDetection.CheckColliderCollision(collider, bufferedCollider, out intersection))
                    {
                        // We inform the first collider that a collision happened, allowing itself to adjust it's position or do other things.
                        collider.OnColliderCollision(bufferedCollider, intersection);

                        // By now the first collider may or may not have adjusted itself. Either way, we need the up to date intersection data for the second collider so we can supply it to it's OnColliderCollision method.
                        BoxCollisionDetection.CheckColliderCollision(bufferedCollider, collider, out intersection);
                        bufferedCollider.OnColliderCollision(collider, intersection);
                    }
                }

                buffer.Add(collider);
            }
        }
    }
}