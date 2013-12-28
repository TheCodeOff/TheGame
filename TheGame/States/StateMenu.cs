using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheGame.Entities;

namespace TheGame.States
{
    class StateMenu : GameState
    {
        #region Textures
        private Texture2D textureBackground;
        private Texture2D textureButtons;
        #endregion
        #region Background Effects

        #endregion
        #region Colors
        private Color BUTTON_COLOR = new Color(207, 106, 0);
        private Color HOVER_COLOR = Color.Transparent;
        private Color TEXT_COLOR = new Color(252, 130, 0, 245);
        private Color singleColor, multiColor, optionsColor, exitColor;
        #endregion
        #region Buttons
        public static Rectangle SINGLE_PLAYER = new Rectangle(80, 200, 180, 40);
        public static Rectangle MULTI_PLAYER = new Rectangle(80, 300, 164, 40);
        public static Rectangle OPTIONS = new Rectangle(80, 400, 110, 40);
        public static Rectangle EXIT = new Rectangle(80, 500, 58, 40);
        #endregion
        #region Mouse
        MouseEntity mouse;
        #endregion

        public StateMenu(LeGame g)
            : base(g)
        {
            mouse = new MouseEntity(game);
            singleColor = multiColor = optionsColor = exitColor = TEXT_COLOR;
        }

        internal void Initialize() { }
        internal void LoadContent(ContentManager c)
        {
            textureBackground = c.Load<Texture2D>("menu/background");
            textureButtons = c.Load<Texture2D>("menu/buttons");
            mouse.Load(c);
        }
        internal void Update(GameTime g)
        {
            mouse.Update(Mouse.GetState(), g);
        }
        internal void Draw(SpriteBatch s, GameTime g)
        {
            #region Clear screen
            s.GraphicsDevice.Clear(new Color(33, 33, 58));
            #endregion
            #region Draw background
            s.Draw(textureBackground, game.window, Color.White);
            #endregion
            #region Draw buttons
            s.DrawString(LeGame.menuBackFont, "Singleplayer", new Vector2(SINGLE_PLAYER.X - 2, SINGLE_PLAYER.Y - 6), BUTTON_COLOR);
            s.DrawString(LeGame.menuFont, "Singleplayer", new Vector2(SINGLE_PLAYER.X, SINGLE_PLAYER.Y - 5), singleColor);
            s.DrawString(LeGame.menuBackFont, "Multiplayer", new Vector2(MULTI_PLAYER.X - 2, MULTI_PLAYER.Y - 6), BUTTON_COLOR);
            s.DrawString(LeGame.menuFont, "Multiplayer", new Vector2(MULTI_PLAYER.X, MULTI_PLAYER.Y - 5), multiColor);
            s.DrawString(LeGame.menuBackFont, "Options", new Vector2(OPTIONS.X - 2, OPTIONS.Y - 6), BUTTON_COLOR);
            s.DrawString(LeGame.menuFont, "Options", new Vector2(OPTIONS.X, OPTIONS.Y - 5), optionsColor);
            s.DrawString(LeGame.menuBackFont, "Exit", new Vector2(EXIT.X - 2, EXIT.Y - 6), BUTTON_COLOR);
            s.DrawString(LeGame.menuFont, "Exit", new Vector2(EXIT.X, EXIT.Y - 5), exitColor);
            #endregion
            #region Draw mouse
            mouse.Draw(s);
            #endregion
        }

        internal void KeyPressed(Keys[] keys) { }
        internal void MouseClicked(MouseState m)
        {
            Point clickLocation = new Point(m.X, m.Y);
            #region Button clicks
            // Singleplayer
            if (SINGLE_PLAYER.Contains(clickLocation))
                game.states.setCurrentState(StateManager.State.Single);
            // Multiplayer
            if (MULTI_PLAYER.Contains(clickLocation))
                return;
            // Options
            if (OPTIONS.Contains(clickLocation))
                return;
            // Exit
            if (EXIT.Contains(clickLocation))
                game.Exit();
            #endregion
        }

        internal void MouseHover(MouseState m)
        {
            Point hoverLocation = new Point(m.X, m.Y);
            #region Button Hovers
            // Singleplayer
            if (SINGLE_PLAYER.Contains(hoverLocation))
                singleColor = HOVER_COLOR;
            else
                singleColor = TEXT_COLOR;
            // Multiplayer
            if (MULTI_PLAYER.Contains(hoverLocation))
                multiColor = HOVER_COLOR;
            else
                multiColor = TEXT_COLOR;
            // Options
            if (OPTIONS.Contains(hoverLocation))
                optionsColor = HOVER_COLOR;
            else
                optionsColor = TEXT_COLOR;
            // Exit
            if (EXIT.Contains(hoverLocation))
                exitColor = HOVER_COLOR;
            else
                exitColor = TEXT_COLOR;
            #endregion
        }
    }
}