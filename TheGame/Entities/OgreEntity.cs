using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using TheGame.Core;

namespace TheGame.Entities
{
    public class OgreEntity : DrawableGameComponent
    {
        private LeGame game;
        private Texture2D texture;
        private Texture2D sight;
        private AnimatedSprite sprite;
        private Vector2 location;
        private Vector2 target;
        private float angle;
        private int size;
        private float speed = 1.0f;
        private bool isWalking;
        private SoundEffectInstance laughSound;

        public OgreEntity(Game game, Vector2 location, float angle)
            : base(game)
        {
            this.game = (LeGame)game;
            this.location = location;
            this.angle = angle;
            isWalking = false;
        }

        public override void Initialize() { }

        public void Load(ContentManager cm)
        {
            texture = cm.Load<Texture2D>("sprites/ogre");
            sight = cm.Load<Texture2D>("gui/sight");
            sprite = new AnimatedSprite(texture, 1, 8, 0.12);
            size = texture.Width / 8;
            laughSound = cm.Load<SoundEffect>("soundfx/male_laugh").CreateInstance();
        }

        public void Update(Vector2 target, GameTime gt)
        {
            Rectangle dest = new Rectangle((int)location.X, (int)location.Y, size, size);
            if (!dest.Intersects(game.player.collisionBox))
            {
                isWalking = true;
                Vector2 direction = location - target;
                direction.Normalize();
                this.angle = Vector2ToRadian(direction) + (float)Math.PI;
                location -= direction * speed;
                if (laughSound.State == SoundState.Playing) laughSound.Stop();
            }
            else
            {
                sprite.currentFrame = 0;
                laughSound.Play();
                isWalking = false;
            }

            if (isWalking) sprite.Update();
        }

        public void Draw(SpriteBatch sp)
        {
            Rectangle dest = new Rectangle((int)location.X + (size / 2), (int)location.Y, size, size);
            sprite.Draw(sp, dest, angle);
            // Sight
            if (LeGame.SHOW_ENEMY_SIGHT)
                sp.Draw(sight, dest, Color.White);
        }

        public void Draw(SpriteBatch sp)
        {
            Rectangle dest = new Rectangle((int)location.X + (size / 2), (int)location.Y, size, size);
            sprite.Draw(sp, dest, angle);
            // Sight
            if (LeGame.SHOW_ENEMY_SIGHT)
                sp.Draw(sight, dest, Color.White);
        }

        public float Vector2ToRadian(Vector2 direction) { return (float)Math.Atan2(direction.X, -direction.Y); }

    }
}
