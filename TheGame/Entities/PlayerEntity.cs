using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections;
using TheGame.Core;


namespace TheGame.Entities
{
    public class PlayerEntity : DrawableGameComponent
    {
        private LeGame parent;
        #region Texture And Draw Variables
        private Texture2D texture;
        private Texture2D sightTexture;
        private AnimatedSprite sprite;
        public static int size = 42;
        public static Vector2 drawLocation;
        #endregion

        #region Location And Movement Variables
        public Rectangle collisionBox;
        private int collisionBoxSize;
        public Vector2 previousLocation;
        public static Vector2 location;
        public float angle;
        public bool isMoving;
        private float speed = 2.0f;
        #endregion

        #region Sight Variables
        public Rectangle sight;
        private int sightRadius = 150;
        #endregion

        #region Sprint Settings
        public int sprintFrame = 0;
        public int sprintMax = 50;
        public int sprintTimeoutFrame = 0;
        public readonly int sprintTimeout = 300;
        public readonly int sprintRestGain = 50;
        public bool isSprinting;
        public bool canSprint;
        #endregion

        #region Sound Effects
        private SoundEffectInstance walkSound;
        private SoundEffectInstance laughSound;
        private int soundFrame = 0;
        private int soundTime = 16;
        #endregion

        public int health = 20;

        public PlayerEntity(Game game, Vector2 startLocation)
            : base(game)
        {
            this.parent = (LeGame)game;
            location = startLocation;
            previousLocation = startLocation;
            isMoving = false;
            isSprinting = false;
            canSprint = true;
            angle = 0.0f;
            collisionBoxSize = size - 5;
            drawLocation = Vector2.One;
            drawLocation.X = (float)((LeGame.CLIENT_SCREEN_WIDTH / 2.0) + (size / 2.0));
            drawLocation.Y = (float)((LeGame.CLIENT_SCREEN_HEIGHT / 2) + (size / 2));
        }
        public override void Initialize()
        {
            base.Initialize();
        }

        public void Load(ContentManager content)
        {
            texture = content.Load<Texture2D>("sprites/player");
            sightTexture = content.Load<Texture2D>("gui/sight");
            sprite = new AnimatedSprite(texture, 1, 8, 0.12);
            walkSound = content.Load<SoundEffect>("soundfx/walk").CreateInstance();
            laughSound = content.Load<SoundEffect>("soundfx/male_laugh").CreateInstance();
        }

        public override void Update(GameTime gameTime)
        {
            if (isMoving)
            {
                if (soundFrame >= soundTime)
                {
                    walkSound.Pitch = 0.0f;
                    walkSound.Play();
                    soundFrame = 0;
                }
                else { soundFrame++; }

                sprite.Update();
                isMoving = false;
            }
            else { sprite.currentFrame = 0; soundFrame = 0; walkSound.Stop(); }

            if (isSprinting)
            {
                if (sprintFrame >= sprintMax)
                {
                    SetSprinting(false);
                    canSprint = false;
                    sprintMax = 0;
                    sprintFrame = 0;
                }
                else sprintFrame += 1;
            }
            else if (sprintMax <= 0)
            {
                if (sprintTimeoutFrame >= sprintTimeout)
                {
                    canSprint = true;
                    if (sprintMax < (sprintRestGain * 3)) sprintMax += sprintRestGain;
                    else sprintMax = sprintRestGain * 3;
                    sprintTimeoutFrame = 0;
                }
                else sprintTimeoutFrame += 1;
            }

            collisionBox = new Rectangle((int)location.X - (collisionBoxSize / 2), (int)location.Y - (collisionBoxSize / 2), collisionBoxSize, collisionBoxSize);
            sight = new Rectangle((int)location.X - sightRadius, (int)location.Y - sightRadius - 3, (sightRadius * 2) + 6, (sightRadius * 2) + 6);
            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Sight
            if (LeGame.SHOW_PLAYER_SIGHT) spriteBatch.Draw(sightTexture, sight, Color.White);
            // Sprite
            sprite.Draw(spriteBatch, location, angle);
        }

        public void DrawCentered(SpriteBatch spriteBatch)
        {
            // Sight
            Rectangle cS = new Rectangle((int)drawLocation.X - sightRadius, (int)drawLocation.Y - sightRadius - 3, (sightRadius * 2) + 6, (sightRadius * 2) + 6);
            //if (LeGame.SHOW_PLAYER_SIGHT) spriteBatch.Draw(sightTexture, cS, Color.White);
            if (LeGame.SHOW_PLAYER_SIGHT) spriteBatch.Draw(sightTexture, sight, Color.White);
            // Sprite
            sprite.Draw(spriteBatch, drawLocation, angle);
        }

        public void Move()
        {
            isMoving = true;
            Vector2 direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            direction.Normalize();
            previousLocation = location;
            location += direction * speed;
        }

        public void Turn(float turnAngle) { angle += turnAngle; }
        public void Damage(int damage) { health -= damage; }
        public void Knockback() { location = previousLocation; }
        public void MouseClicked(Point click) { }
        public float Vector2ToRadian(Vector2 direction) { return (float)Math.Atan2(direction.X, -direction.Y); }
        public void WalkOnUnwalkableTile() { Knockback(); }

        public void SetSprinting(bool p)
        {
            if (p && canSprint) { speed = 3.5f; isSprinting = p; }
            else if (!p) { speed = 2.0f; isSprinting = p; }
        }

        public void PlayerPressedKey(Keys key)
        {
            switch (key)
            {
                case Keys.W: // W - Forward.
                    Move();
                    break;
                case Keys.A:
                    Turn(-0.1f);
                    break;
                case Keys.D:
                    Turn(0.1f);
                    break;
                case Keys.Q:
                    if (laughSound.State == SoundState.Stopped) { laughSound.Play(); }
                    break;
            }
        }

        public Vector2 getLocation()
        {
            return location;
        }
    }
}
