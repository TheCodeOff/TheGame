using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheGame.Entities;
using TheGame.Core;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace TheGame.States
{
    class StateMenu : GameState
    {
        #region Textures
        private Texture2D textureButtons;
        #endregion
        #region Background Effects
        private ScrollBackground background;
        #endregion
        #region Colors
        private Color LOADINGBAR_COLOR = new Color(Color.PaleGoldenrod.R, Color.PaleGoldenrod.G, Color.PaleGoldenrod.B, 100);
        private Color LOADINGBAR_FILL_COLOR = new Color(252, 130, 0, 245);
        private Color BUTTON_COLOR = new Color(207, 106, 0);
        private Color HOVER_COLOR = Color.Transparent;
        private Color TEXT_COLOR = new Color(252, 130, 0, 245);
        private Color VERSION_COLOR = Color.OrangeRed;
        private Color TITLE_COLOR_1 = new Color(Color.OrangeRed.R, Color.OrangeRed.G, Color.OrangeRed.B, 100);
        private Color TITLE_COLOR_2 = new Color(Color.PaleGoldenrod.R, Color.PaleGoldenrod.G, Color.PaleGoldenrod.B, 100);
        private Color TRANSITION_COLOR = new Color(0, 0, 0, 0);
        private Color singleColor, multiColor, optionsColor, exitColor;
        #endregion
        #region Buttons
        public static Rectangle SINGLE_PLAYER = new Rectangle(80, 400, 180, 40);
        public static Rectangle MULTI_PLAYER = new Rectangle(80, 460, 164, 40);
        public static Rectangle OPTIONS = new Rectangle(80, 520, 110, 40);
        public static Rectangle EXIT = new Rectangle(80, 580, 58, 40);
        #endregion
        #region Mouse
        MouseEntity mouse;
        #endregion
        #region Fonts
        public static SpriteFont menuTitleFont; // Menu title font
        public static SpriteFont menuBackFont; // Menu Background font
        public static SpriteFont menuFont; // Menu font
        #endregion
        #region Audio
        private SoundEffectInstance buttonSound;
        private SoundEffectInstance music;
        #endregion
        #region Transition
        private enum TransitionType { FadeIn, FadeOut };
        private TransitionType currentTransition;
        private StateManager.State targetState;
        private int transitionCount;
        private bool isTransitioning, doChangeState;
        #endregion

        public StateMenu(LeGame g)
            : base(g)
        {
            Log("Creating state");
            background = new ScrollBackground(game, "menu/background");
            mouse = new MouseEntity(game);
            singleColor = multiColor = optionsColor = exitColor = TEXT_COLOR;
            isTransitioning = true;
            doChangeState = false;
            transitionCount = 0;
            targetState = StateManager.State.Single;
            currentTransition = TransitionType.FadeIn;
        }

        internal void Initialize()
        {
            Log("Initializing");
            background.Initialize();
        }
        internal void LoadContent(ContentManager c)
        {
            Log("Loading content");
            #region Load music
            music = c.Load<SoundEffect>("audio/menu/menu").CreateInstance();
            music.IsLooped = true; // Loop music
            music.Volume = 0.0f;
            buttonSound = c.Load<SoundEffect>("audio/menu/button").CreateInstance();
            buttonSound.IsLooped = false;
            buttonSound.Volume = 0.7f;
            #endregion
            #region Load textures
            textureButtons = c.Load<Texture2D>("menu/buttons");
            menuTitleFont = c.Load<SpriteFont>("fonts/menu/MenuTitleFont");
            menuBackFont = c.Load<SpriteFont>("fonts/menu/MenuBackFont");
            menuFont = c.Load<SpriteFont>("fonts/menu/MenuFont");
            #endregion
            mouse.Load(c);
            background.LoadContent(c);
        }
        internal void Update(GameTime g)
        {
            // Music
            if (music.State != SoundState.Playing)
                music.Play();

            background.Update(g);
            mouse.Update(Mouse.GetState(), g);

            // Transitions
            if (isTransitioning)
            {
                if (transitionCount >= 255) // Transition is finished
                {
                    transitionCount = 0;
                    isTransitioning = false;
                    if (doChangeState)
                    {
                        game.states.setCurrentState(targetState);
                        doChangeState = false;
                    }
                }
                else if (currentTransition == TransitionType.FadeIn) // Transition is ongoing, fade in
                {
                    transitionCount+=5;
                    TRANSITION_COLOR.A = (byte)(255 - transitionCount);
                    if (music.Volume < 0.7f) music.Volume += 0.005f;
                    else music.Volume = 0.8f;
                }
                else if (currentTransition == TransitionType.FadeOut)  // Transition is ongoing, fade out
                {
                    transitionCount+=5;
                    TRANSITION_COLOR.A = (byte)transitionCount;
                    if (music.Volume > 0.1f) music.Volume -= 0.005f;
                    else music.Volume = 0.0f;
                }
            }

        }
        internal void Draw(SpriteBatch s, GameTime g)
        {
            #region Clear screen
            s.GraphicsDevice.Clear(new Color(33, 33, 58));
            #endregion
            #region Draw background
            background.Draw(s);
            //s.Draw(textureBackground, game.window, Color.White);
            s.DrawString(LeGame.gameFont, "version " + LeGame.VERSION, new Vector2(game.window.Width - 80, game.window.Height - 14), VERSION_COLOR);
            // Version
            #endregion
            #region Draw title
            s.DrawString(menuTitleFont, "THE", new Vector2(50, 0), TITLE_COLOR_1);
            s.DrawString(menuTitleFont, "GAME", new Vector2(500, 0), TITLE_COLOR_2);
            #endregion
            #region Draw buttons
            s.DrawString(menuBackFont, "Singleplayer", new Vector2(SINGLE_PLAYER.X - 2, SINGLE_PLAYER.Y - 6), BUTTON_COLOR);
            s.DrawString(menuFont, "Singleplayer", new Vector2(SINGLE_PLAYER.X, SINGLE_PLAYER.Y - 5), singleColor);
            //s.DrawString(menuBackFont, "Multiplayer", new Vector2(MULTI_PLAYER.X - 2, MULTI_PLAYER.Y - 6), BUTTON_COLOR);
            //s.DrawString(menuFont, "Multiplayer", new Vector2(MULTI_PLAYER.X, MULTI_PLAYER.Y - 5), multiColor);
            //s.DrawString(menuBackFont, "Options", new Vector2(OPTIONS.X - 2, OPTIONS.Y - 6), BUTTON_COLOR);
            //s.DrawString(menuFont, "Options", new Vector2(OPTIONS.X, OPTIONS.Y - 5), optionsColor);
            s.DrawString(menuBackFont, "Exit", new Vector2(EXIT.X - 2, EXIT.Y - 6), BUTTON_COLOR);
            s.DrawString(menuFont, "Exit", new Vector2(EXIT.X, EXIT.Y - 5), exitColor);
            #endregion
            #region Transitions
            if (isTransitioning)
                s.Draw(textureButtons, game.window, TRANSITION_COLOR);
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
            {
                buttonSound.Play();
                TransitionToState(StateManager.State.Single);
            }
            // Multiplayer
            if (MULTI_PLAYER.Contains(clickLocation))
            {
                buttonSound.Play();
                TransitionToState(StateManager.State.Single);
            }
            // Options
            if (OPTIONS.Contains(clickLocation))
            {
                buttonSound.Play();
                TransitionToState(StateManager.State.Single);
            }
            // Exit
            if (EXIT.Contains(clickLocation))
            {
                buttonSound.Play();
                game.Exit();
            }
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

        /// <summary>
        /// Log a entry.
        /// </summary>
        /// <param name="message">Entry message</param>
        public void Log(String message) { Console.WriteLine("[StateMenu] " + message); }

        public void TransitionToState(StateManager.State s)
        {
            targetState = s;
            isTransitioning = true;
            doChangeState = true;
            currentTransition = TransitionType.FadeOut;

        }
    }
}