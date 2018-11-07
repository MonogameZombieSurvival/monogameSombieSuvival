using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Game2
{
    class Explosion : AnimatedGameObject
    {
        private SoundEffectInstance explosionSound;
        public Explosion(int size, Vector2 startPosition, ContentManager content) : base(6, 5, startPosition, content, "explosion_"+size)
        {
            explosionSound = content.Load<SoundEffect>("8bit_bomb_explosion").CreateInstance();
            explosionSound.Play();
        }

        public override void Update(GameTime gameTime)
        {
            if (explosionSound.State != SoundState.Playing)
            {
                GameWorld.RemoveGameObject(this);
            }
            base.Update(gameTime);
        }
    }
}
