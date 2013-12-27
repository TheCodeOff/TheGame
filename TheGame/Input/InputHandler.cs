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
            if (keys.Contains(Keys.W)) game.player.PlayerPressedKey(Keys.W);
            if (keys.Contains(Keys.A)) game.player.PlayerPressedKey(Keys.A);
            if (keys.Contains(Keys.D)) game.player.PlayerPressedKey(Keys.D);
            if (keys.Contains(Keys.Q)) game.player.PlayerPressedKey(Keys.Q);
            if (keys.Contains(Keys.LeftShift)) game.player.SetSprinting(true);  // SHIFT - Sprint.
            else if (!keys.Contains(Keys.LeftShift)) game.player.SetSprinting(false); //Stop Sprint         

        }

        private void MouseRightClicked(MouseState mouse)
        {
            Console.WriteLine("Mouse right clicked...");
        }

        private void MouseLeftClicked(MouseState mouse)
        {
            Console.WriteLine("Mouse left clicked...");
            game.audioControl.PlayerMouseClicked(new Point(mouse.X, mouse.Y));
            game.player.MouseClicked(new Point(mouse.X, mouse.Y));
        }
    }
}
