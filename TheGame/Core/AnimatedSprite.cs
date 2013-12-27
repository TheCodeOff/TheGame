using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using TheGame.Entities;

namespace TheGame.Core
{
    public class AnimatedSprite
    {

        public Texture2D _texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int currentFrame;
        public double frameSpeedBuffer = 0.0;
        public double frameSpeed = 1.0;
        private int totalFrames;

        public AnimatedSprite(Texture2D texture, int rows, int columns, double frameSpeed)
        {
            _texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            this.frameSpeed = frameSpeed;
        }

        public void Update()
        {
            if (frameSpeedBuffer >= frameSpeed)
            {
                currentFrame++;
                if (currentFrame == totalFrames)
                {
                    currentFrame = 0;
                }
                frameSpeedBuffer = 0;
            }
            else frameSpeedBuffer += 0.5;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, float angle)
        {
            this.Draw(spriteBatch, new Rectangle((int)location.X, (int)location.Y, PlayerEntity.size, PlayerEntity.size), angle);
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle destination, float angle)
        {
            int width = _texture.Width / Columns;
            int height = _texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;


            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Vector2 origin = new Vector2(width / 2, height / 2);

            spriteBatch.Draw(_texture, destination, sourceRectangle, Color.White, angle, origin, SpriteEffects.None, 1);
        }
    }
}
