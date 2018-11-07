using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Game2
{

    class Player : AnimatedGameObject
    {
        private const float moveSpeed = 100;
        private const float rotationSpeed = MathHelper.Pi;
        public Vector2 direction = new Vector2(0,-1);
        private double lastShoot = 0;
        public Vector2 playerPosition {
            get
            {
                return position;
            }
                }
      
      
        private int health;
        

        public int Health
        {
            get { return health; }
        }

   

        public Player(ContentManager content) : base(1,5, new Vector2(GameWorld.ScreenSize.Width / 2, GameWorld.ScreenSize.Height / 2), content, "playerImg")
        {
            this.content = content;
            health = 1000;
            
            
        }

        public override void Update(GameTime gameTime)
        {
            
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                position.X -= (float)(moveSpeed * gameTime.ElapsedGameTime.TotalSeconds);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                position.X += (float)(moveSpeed * gameTime.ElapsedGameTime.TotalSeconds);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                position.Y -= (float)(moveSpeed * gameTime.ElapsedGameTime.TotalSeconds);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                position.Y += (float)(moveSpeed * gameTime.ElapsedGameTime.TotalSeconds);
            }


            
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                rotation += (float)(rotationSpeed * gameTime.ElapsedGameTime.TotalSeconds);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                rotation -= (float)(rotationSpeed * gameTime.ElapsedGameTime.TotalSeconds);
            }

            
            direction = new Vector2((float)Math.Cos(rotation-MathHelper.Pi*0.0f), (float)Math.Sin(rotation - MathHelper.Pi * 0.0f));

            position += direction * (float)(gameTime.ElapsedGameTime.TotalSeconds);


            lastShoot += gameTime.ElapsedGameTime.TotalSeconds;
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && lastShoot > 0.3f)
            {
                GameWorld.AddGameObject(new Bullet(direction, position, content));
                lastShoot = 0;
            }


            base.Update(gameTime);
        }

        public override void DoCollision(GameObject otherObject)
        {
            if (otherObject is Asteroid)
            {
                health--;
            }

        }

      
    }
}
