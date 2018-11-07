using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game2
{
    /// <summary>
    /// Class that represents a asteroid
    /// </summary>
    class Enemy : GameObject
    {

      
        private Vector2 playerPosition;
        float moveSpeed = 100;
         Random rand = new Random();
        private int size;
       
        /// <summary>
        /// Gets the Size of the Asteroid
        /// </summary>
        public int Size { get => size; }



        /// <summary>
        /// Asteroid constructor that sets the size of the asteroid. Asteroid color and starting position is selected at random .
        /// </summary>
        /// <param name="size">Size should between 1 to 4 and is used to select the correct texture</param>
        /// <param name="content">Content Manager for loading resources</param>
        public Enemy(int size, ContentManager content, Vector2 PlayerPosition) :this(new Vector2(new Random().Next(GameWorld.ScreenSize.Width), new Random().Next(GameWorld.ScreenSize.Height)), size, content)
        {
            playerPosition = PlayerPosition;
            position = new Vector2(rand.Next(GameWorld.ScreenSize.Width), rand.Next(GameWorld.ScreenSize.Height));
            
        }

        /// <summary>
        /// Asteroid constructor that sets the size of the asteroid. Asteroid color and starting position is selected at random .
        /// </summary>
        /// <param name="startPosition">Starting position of the asteroid</param>
        /// <param name="size">Size should between 1 to 4 and is used to select the correct texture</param>
        /// <param name="content">Content Manager for loading resources</param>
        public Enemy(Vector2 startPosition,int size, ContentManager content) : base(content, $"Asteroids_{(int)Math.Pow(2, 5 + size)}x{(int)Math.Pow(2, 5 + size)}_00{(new Random().Next(1, 8))}")
        {
            this.size = size;
            position = startPosition;
           
        }

        /// <summary>
        /// Sets the direction of the asteroid to a random direction
        /// </summary>
        private void SetRandomDirection()
        {
            Random rnd = new Random();
            Direction = new Vector2((rnd.Next(0, 2) * 2 - 1), (rnd.Next(0, 2) * 2 - 1)); //Set direction vector components to -1 or 1
            Direction.Normalize(); //Normalizes vector so that it is only a unit vector
        }


        private void SetDiraction()
        {
           
            
            Direction = realTimeplayerPosition - position;
            Direction.Normalize();
        }
        /// <summary>
        /// Update method that moves the asteroid in a specified direction. If asteroid is outside screen it sets a new random direction
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {

            SetDiraction();
            position +=  Direction *(float)(moveSpeed * gameTime.ElapsedGameTime.TotalSeconds); //Added direction vector to current position

            //If Asteroid is outside screen set it to a new random direction
            //if (!GameWorld.ScreenSize.Intersects(this.CollisionBox))
            //{
            //    SetRandomDirection();
            //}

        }

        /// <summary>
        /// Do the collide action for the Asteroid. If Player or a Bullet it explodes. If bullet it explodes into smaller asteroids
        /// </summary>
        /// <param name="otherObject">The object it collided with</param>
        public override void DoCollision(GameObject otherObject)
        {
            if (otherObject is Player || otherObject is Bullet)
            {
                Explosion ex = new Explosion(Size + 1, Position, content);
                //GameWorld.AddGameObject(ex);
                GameWorld.RemoveGameObject(this);
            }
            /*
            if (otherObject is Bullet)
            { 
                GameWorld.RemoveGameObject(otherObject);
                for (int i = 0; i < Size; i++)
                {
                    GameWorld.AddGameObject(new Asteroid(position, size - 1, content));
                }
            }
            */
        }

    }
}
