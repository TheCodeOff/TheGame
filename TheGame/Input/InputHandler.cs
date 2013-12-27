using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace TheGame
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class InputHandler : Microsoft.Xna.Framework.GameComponent
    {
        private LeGame game;

        private MouseState previousMouseState;
        private MouseState currentMouseState;

        private KeyboardState previousKeyboardState;
        private KeyboardState currentKeyboardState;

        public InputHandler(Game game)
            : base(game)
        {
            this.game = (LeGame)game;
            previousMouseState = Mouse.GetState();
            currentMouseState = Mouse.GetState();
            previousKeyboardState = Keyboard.GetState();
            currentKeyboardState = Keyboard.GetState();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {

            #region Mouse
            MouseState currentMouseState = Mouse.GetState();

            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                MouseLeftClicked(currentMouseState);
            }
            else if (currentMouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released)
            {
                MouseRightClicked(currentMouseState);
            }

            previousMouseState = currentMouseState;
            #endregion

            #region Keyboard
            KeyboardState currentKeyboardState = Keyboard.GetState();

            if (currentKeyboardState.GetPressedKeys().Length > 0)
            {
                KeyPressed(currentKeyboardState.GetPressedKeys());
            }

            previousKeyboardState = currentKeyboardState;
            #endregion

            base.Update(gameTime);
        }

        private void KeyPressed(Keys[] keys)
        {
            game.states.KeyPressed(keys);
        }

        private void MouseRightClicked(MouseState mouse)
        {
            if (game.window.Contains(new Rectangle(mouse.X, mouse.Y, 1, 1)))
                Console.WriteLine("Mouse right clicked...");
        }

        private void MouseLeftClicked(MouseState mouse)
        {
            if (game.window.Contains(new Rectangle(mouse.X, mouse.Y, 1, 1)))
                Console.WriteLine("Mouse left clicked...");
        }
    }
}
