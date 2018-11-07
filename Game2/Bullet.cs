using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Game2
{
    /// <summary>
    /// Class that represents a Bullet fired from the spaceship
    /// </summary>
    class Bullet : GameObject
    {
        private Vector2 direction;
        private const float movementSpeed = 200;

        /// <summary>
        /// Constructor for a Bullet
        /// </summary>
        /// <param name="direction">Sets the movement direction for the Bullet</param>
        /// <param name="startPosition">Sets its starting position</param>
        /// <param name="content">Content Manager for loading resources</param>
        public Bullet(Vector2 direction, Vector2 startPosition, ContentManager content) : base(startPosition,content, "smallbullet")
        {
            this.direction = direction;
            this.direction.Normalize();
            startPosition.X = 100;
        }

        /// <summary>
        /// Moves the Bullet in the designated direction. If it goes outside the screen area it gets removed
        /// </summary>
        /// <param name="gameTime">The elasped time since last update call</param>
        public override void Update(GameTime gameTime)
        {
            position += direction * (float)(movementSpeed * gameTime.ElapsedGameTime.TotalSeconds);
            if (!GameWorld.ScreenSize.Intersects(CollisionBox))
            {
                GameWorld.RemoveGameObject(this);
            }
        }

        
    }
}
