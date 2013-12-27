using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TheGame.States
{
    class StateMenu : GameState
    {
        public StateMenu(LeGame g) : base(g) { }
        internal void Initialize() { }
        internal void LoadContent(ContentManager c) { }
        internal void Update(GameTime g) { }
        internal void Draw(SpriteBatch s, GameTime g) { }
        internal void KeyPressed(Keys[] keys) { }
    }
}