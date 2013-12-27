using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheGame.Items
{
    internal class Weapon
    {
        internal LeGame game;
        internal float damage;

        internal Weapon(LeGame g)
        {
            game = g;
            damage = 0.0f;
        }
    }
}
