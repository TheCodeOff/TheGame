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
            Log("Creating states");
            menu = new StateMenu(game);
            single = new StateSingle(game);
            multi = new StateMulti(game);
            curState = LeGame.STARTING_STATE;
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
            Log("Changing state from " + curState + " to " + newState);
            curState = newState;
        }

        internal void Initialize()
        {
            Log("Initializing");
            menu.Initialize();
            single.Initialize();
            multi.Initialize();
        }

        internal void LoadContent(ContentManager c)
        {
            Log("Loading content");
            menu.LoadContent(c);
            multi.LoadContent(c);
            single.LoadContent(c);
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

        internal void MouseClicked(MouseState m, InputHandler.MouseButton b)
        {
            if (b == InputHandler.MouseButton.Left)
            {
                switch (curState)
                {
                    case State.Menu:
                        menu.MouseClicked(m);
                        break;
                    case State.Multi:
                        multi.MouseClicked(m);
                        break;
                    default:
                        single.MouseClicked(m);
                        break;
                }
            }
        }

        internal void MouseHover(MouseState m)
        {
            switch (curState)
            {
                case State.Menu:
                    menu.MouseHover(m);
                    break;
                case State.Multi:
                    multi.MouseHover(m);
                    break;
                case State.Single:
                    single.MouseHover(m);
                    break;
                default:
                    return;                    
            }
        }
        
        /// <summary>
        /// Log a entry.
        /// </summary>
        /// <param name="message">Entry message</param>
        public void Log(String message) { Console.WriteLine("[StateManager] " + message); }
    }
}
