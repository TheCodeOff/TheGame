﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TheGame.States
{
    internal class GameState
    {
        internal LeGame game;

        public GameState(LeGame g)
        {
            this.game = g;
        }
    }
}
