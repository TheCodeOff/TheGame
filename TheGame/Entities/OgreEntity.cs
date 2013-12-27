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
        #region Properties
        private float FOLLOW_DISTANCE = 300;
        private float MOVEMENT_SPEED = 1.0f;
        private int SIZE;
        #endregion
        #region Textures and Sprites
        private AnimatedSprite sprite;
        private Texture2D texture;
        private Texture2D sightAreaTexture;
        private Texture2D healthTexture;
        #endregion
        #region Location and Movement
        private Vector2 location;
        private Vector2 target;
        private float angle;
        private bool isWalking;
        private Rectangle drawDestination, moveDestination;
        #endregion
        #region Audio & Video
        private SoundEffectInstance laughSound;
        #endregion
        #region Colors
        private Color BackdropColor = new Color(100, 100, 100, 100);
        private Color HealthBarColor = new Color(200, 0, 0, 255);
        private Color TextColor = Color.WhiteSmoke;
        #endregion

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
            sightAreaTexture = cm.Load<Texture2D>("gui/sight");
            healthTexture = cm.Load<Texture2D>("gui/rect");
            sprite = new AnimatedSprite(texture, 1, 8, 0.12);
            SIZE = texture.Width / 8;
            laughSound = cm.Load<SoundEffect>("soundfx/male_laugh").CreateInstance();
        }

        public void Update(PlayerEntity player, GameTime gt)
        {
            target = player.getLocation();
            moveDestination = new Rectangle((int)location.X, (int)location.Y, SIZE, SIZE);
            if (moveDestination.Intersects(player.collisionBox))
            {
                sprite.currentFrame = 0;
                if (!LeGame.AUDIO_IS_MUTED) laughSound.Play();
                isWalking = false;
            }
            else if (Vector2.Distance(location, target) >= FOLLOW_DISTANCE)
            {
                isWalking = false;
                sprite.currentFrame = 0;
            }
            else
            {
                isWalking = true;
                Vector2 direction = location - target;
                direction.Normalize();
                this.angle = Vector2ToRadian(direction) + (float)Math.PI;
                location -= direction * MOVEMENT_SPEED;
                if (laughSound.State == SoundState.Playing) laughSound.Stop();
            }

            if (isWalking) sprite.Update();
        }

        public void Draw(SpriteBatch sp)
        {
            drawDestination = new Rectangle((int)location.X + (SIZE / 2), (int)location.Y, SIZE, SIZE);
            #region Draw sprite
            sprite.Draw(sp, drawDestination, angle);
            #endregion
            #region Draw sight area
            if (LeGame.SHOW_ENEMY_SIGHT)
                sp.Draw(sightAreaTexture, drawDestination, Color.White);
            #endregion
            #region Draw health bar and Tag
            #endregion
        }

        public void DrawRelativeToPlayer(PlayerEntity p, SpriteBatch s)
        {
            #region Get draw location relative to player location
            int dX = (int)(PlayerEntity.drawLocation.X - PlayerEntity.location.X + location.X);
            int dY = (int)(PlayerEntity.drawLocation.Y - PlayerEntity.location.Y + location.Y);
            Rectangle drawLocation = new Rectangle(dX, dY, SIZE, SIZE);
            #endregion
            #region Draw sprite
            sprite.Draw(s, drawLocation, angle);
            #endregion
            #region Draw sight area
            if (LeGame.SHOW_ENEMY_SIGHT)
                s.Draw(sightAreaTexture, drawLocation, Color.White);
            #endregion
            #region Draw health bar and Tag
            s.Draw(healthTexture, new Rectangle(drawLocation.X - (SIZE / 2) - 1, drawLocation.Y - 11 - (SIZE / 2), SIZE + 2, 12), BackdropColor);
            s.Draw(healthTexture, new Rectangle(drawLocation.X - (SIZE / 2), drawLocation.Y - 10 - (SIZE / 2), SIZE, 10), HealthBarColor);
            s.DrawString(LeGame.gameFont, "Ogre", new Vector2(drawLocation.X - (SIZE / 4), drawLocation.Y - 13 - (SIZE / 2)), TextColor);
            #endregion
        }

        public float Vector2ToRadian(Vector2 direction) { return (float)Math.Atan2(direction.X, -direction.Y); }

    }
}
