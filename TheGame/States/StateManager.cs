using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheGame.States
{
    public class StateManager
    {
        public enum State { Menu, Single, Multi };
        private State curState;
        private StateMenu menu;
        private StateSingle single;
        private StateMulti multi;

        public StateManager(LeGame game)
        {
            // Create states
            menu = new StateMenu(game);
            single = new StateSingle(game);
            multi = new StateMulti(game);
            //Singleplayer for now
            curState = State.Single;
        }

        /// <summary>
        /// Get the current state.
        /// </summary>
        /// <returns>enum value from StateManager.State</returns>
        public State getCurrentState()
        {
            return curState;
        }

        public void setCurrentState(State newState)
        {
            curState = newState;
        }

        internal void Initialize()
        {
            Console.WriteLine("Initializing, " + this.GetType().ToString());
            menu.Initialize();
            single.Initialize();
            multi.Initialize();
        }

        internal void LoadContent(ContentManager c)
        {
            Console.WriteLine("Loading content, " + this.GetType().ToString());
            menu.LoadContent(c);
            single.LoadContent(c);
            multi.LoadContent(c);
        }

        internal void Update(GameTime g)
        {
            switch (curState)
            {
                case State.Menu:
                    menu.Update(g);
                    break;
                case State.Multi:
                    multi.Update(g);
                    break;
                default:
                    single.Update(g);
                    break;
            }
        }

        internal void Draw(SpriteBatch s, GameTime g)
        {
            switch (curState)
            {
                case State.Menu:
                    menu.Draw(s, g);
                    break;
                case State.Multi:
                    multi.Draw(s, g);
                    break;
                default:
                    single.Draw(s, g);
                    break;
            }
        }

        internal void KeyPressed(Keys[] keys)
        {
            switch (curState)
            {
                case State.Menu:
                    menu.KeyPressed(keys);
                    break;
                case State.Multi:
                    multi.KeyPressed(keys);
                    break;
                default:
                    single.KeyPressed(keys);
                    break;
            }
        }
    }
}
