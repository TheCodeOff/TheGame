using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using TheGame.Particles;

namespace TheGame.Entities
{
    public class MouseEntity : DrawableGameComponent
    {
        private ParticleEngine particles;
        private Texture2D texture;
        private Rectangle location;
        private Rectangle screenArea;
        public static int size = 12;

        public MouseEntity(Game game) : base(game) {
            screenArea = new Rectangle(0, 0, (int)LeGame.CLIENT_SCREEN_WIDTH, (int)(LeGame.CLIENT_SCREEN_RATIO * LeGame.CLIENT_SCREEN_WIDTH));
        }

        public void Load(ContentManager content)
        {
            texture = content.Load<Texture2D>("sprites/mouse");

            List<Texture2D> textures = new List<Texture2D>();
            textures.Add(content.Load<Texture2D>("particles/circle"));
            textures.Add(content.Load<Texture2D>("particles/star"));
            textures.Add(content.Load<Texture2D>("particles/diamond"));
            particles = new ParticleEngine(textures, new Vector2(400, 240));
        }

        public void Update(MouseState mouse, GameTime gameTime)
        {
            Point mouseLocation = new Point(mouse.X, mouse.Y);
            if (screenArea.Contains(mouseLocation))
            {
                location = new Rectangle(mouseLocation.X, mouseLocation.Y, size, size);
            }

            particles.EmitterLocation = new Vector2(mouse.X, mouse.Y);
            particles.Update();

            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Particles
            particles.Draw(spriteBatch);
            // Mouse
            spriteBatch.Draw(texture, location, Color.White);
        }
    }
}
