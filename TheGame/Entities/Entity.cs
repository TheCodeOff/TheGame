using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace TheGame.Entities
{
    public abstract class Entity : DrawableGameComponent
    {
        Texture2D texture;
        Vector2 location;

        Entity(Game game) : base(game) { }

        public abstract override void Initialize();
        public abstract void Load(ContentManager cm);
        public abstract override void Update(GameTime gt);
        public abstract void Draw(SpriteBatch sp);
        public abstract void DrawCentered(SpriteBatch sp);
    }
}
