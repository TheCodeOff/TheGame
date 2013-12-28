using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace TheGame.Core
{
    public class ScrollBackground : DrawableGameComponent
    {
        #region Others
        private LeGame game;
        #endregion
        #region Textures
        private Texture2D background;
        private String texturePath;
        #endregion
        #region Variables
        private float scrollSpeed = 0.1f;
        private Rectangle image1, image2;
        private int maxY1, maxY2;
        #endregion

        /// <summary>
        /// A vertically scrolling background.
        /// </summary>
        /// <param name="game">Instance of game class</param>
        /// <param name="path">Path to texture</param>
        public ScrollBackground(LeGame game, String path)
            : base(game)
        {
            this.game = game;
            this.texturePath = path;
        }

        public void Initialize()
        {
            image1 = new Rectangle(0, 0, game.window.Width, game.window.Height);
            image2 = new Rectangle(0, 0 - game.window.Height, game.window.Width, game.window.Height);
            maxY1 = game.window.Height;
            maxY2 = 0;
        }
        public void LoadContent(ContentManager c) { background = c.Load<Texture2D>(texturePath); }

        public void Update(GameTime g)
        {
            int moveDistance = (int)(g.ElapsedGameTime.Milliseconds * scrollSpeed);
            // IMAGE 1 ---------------------------------------------------------------------
            if (image1.Y < maxY1) // Can scroll down.
                image1.Y += moveDistance;
            else
                image1.Y = 0; // 'Respawn'
            // IMAGE 2 ---------------------------------------------------------------------
            if (image2.Y < maxY2) // Can scroll down.
                image2.Y += moveDistance;
            else
                image2.Y = 0 - game.window.Height; // 'Respawn'
        }

        public void Draw(SpriteBatch s)
        {
            s.Draw(background, image1, new Color(50, 50, 50, 255));
            s.Draw(background, image2, new Color(50, 50, 50, 255));
        }
    }
}
