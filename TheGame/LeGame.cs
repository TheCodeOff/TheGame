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
using TheGame.Environment;
using TheGame.Entities;
using TheGame.States;

namespace TheGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class LeGame : Microsoft.Xna.Framework.Game
    {
        #region Game Settings
        #region Starting Locations
        public static Vector2 PLAYER_STARTING_LOCATION = new Vector2(500, 500);
        public static Vector2 OGRE_STARTING_LOCATION = new Vector2(200, 200);
        #endregion
        #region Client
        public static double CLIENT_SCREEN_RATIO = (double)16 / 9;
        public static double CLIENT_SCREEN_HEIGHT = 720.0;
        public static double CLIENT_SCREEN_WIDTH = CLIENT_SCREEN_HEIGHT * CLIENT_SCREEN_RATIO;
        #endregion
        #region Environment
        public static int ENVIRONMENT_WIDTH = 30;
        public static int ENVIRONMENT_HEIGHT = 30;
        public static int ENVIRONMENT_TILE_SIZE = 50;
        public static bool DRAW_BACKGROUND = false;
        #endregion
        #region States
        public static StateManager.State STARTING_STATE = StateManager.State.Menu;
        #endregion
        #region Sights and Fog of War
        public static bool SHOW_PLAYER_SIGHT = false;
        public static bool SHOW_ENEMY_SIGHT = false;
        public static bool FOG_OF_WAR_ENABLED = false;
        #endregion
        #region Audio
        public static bool AUDIO_IS_MUTED = true;
        #endregion
        #region Fonts
        public static SpriteFont gameFont; // Ingame font
        public static SpriteFont menuBackFont; // Menu Background font
        public static SpriteFont menuFont; // Menu font
        #endregion
        #endregion

        #region Core Objects
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public InputHandler input;
        public Rectangle window;
        public StateManager states;
        #endregion

        #region Game Info
        private String GAME_NAME = "ThEGaME";
        private String GAME_VERSION = "0.1.1";
        #endregion
        public LeGame()
        {
            Console.WriteLine("Starting " + GAME_NAME + ", version " + GAME_VERSION + "  at " + DateTime.Now.ToShortDateString());
            #region Graphics Setup
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = (int)CLIENT_SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = (int)CLIENT_SCREEN_HEIGHT;
            graphics.ApplyChanges();
            #endregion
            #region Content Setup
            Content.RootDirectory = "Content";
            #endregion

            states = new StateManager(this);

            input = new InputHandler(this);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// </summary>
        protected override void Initialize()
        {
            Console.WriteLine("Initializing Game...");
            // Initialize window
            window = new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height);
            // Base
            base.Initialize();
            // State
            states.Initialize();
        }

        /// <summary>
        /// The place to load all content.
        /// </summary>
        protected override void LoadContent()
        {
            // Batch
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            // State
            states.LoadContent(Content);
            // Fonts
            gameFont = Content.Load<SpriteFont>("fonts/GameFont");
            menuBackFont = Content.Load<SpriteFont>("fonts/MenuBackFont");
            menuFont = Content.Load<SpriteFont>("fonts/MenuFont");
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit using gamepad
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) this.Exit();

            //Update core components
            input.Update(gameTime);

            //Update state
            states.Update(gameTime);

            // Update base
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //spriteBatch.Begin();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            // Clear screen
            spriteBatch.GraphicsDevice.Clear(Color.Black);
            // Draw base
            base.Draw(gameTime);
            // Draw state
            states.Draw(spriteBatch, gameTime);
            // End drawing
            spriteBatch.End();            
        }
    }
}
