using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TheGame.Environment
{
    public class TileTextureList
    {
        private Texture2D[] tileTextures;
        public TileTextureList()
        {
            tileTextures = new Texture2D[4];
        }

        public Texture2D[] GetTextureList(ContentManager c)
        {
            tileTextures[0] = c.Load<Texture2D>("environment/void");
            tileTextures[1] = c.Load<Texture2D>("environment/grass");
            tileTextures[2] = c.Load<Texture2D>("environment/road");
            tileTextures[3] = c.Load<Texture2D>("environment/ice");
            return tileTextures;
        }
    }
}
