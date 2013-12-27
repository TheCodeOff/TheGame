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

namespace TheGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class LeGame : Microsoft.Xna.Framework.Game
    {
        #region Game Settings
        public static Vector2 PLAYER_STARTING_LOCATION = new Vector2(200, 200);
        public static Vector2 OGRE_STARTING_LOCATION = new Vector2(400, 400);
        public static double CLIENT_SCREEN_RATIO = (double)9 / 16;
        public static double CLIENT_SCREEN_WIDTH = 1080.0;
        public static double CLIENT_SCREEN_HEIGHT = CLIENT_SCREEN_WIDTH * CLIENT_SCREEN_RATIO;
        public static int ENVIRONMENT_WIDTH = 100;
        public static int ENVIRONMENT_HEIGHT = 100;
        public static int ENVIRONMENT_TILE_SIZE = 30;
        public static int STARTING_STATE = 0;
        public static bool SHOW_PLAYER_SIGHT = true;
        public static bool SHOW_ENEMY_SIGHT = false;
        public static bool FOG_OF_WAR_ENABLED = false;
        #endregion

        #region Core Variables
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public InputHandler input;
        public SpriteFont font;
        public Rectangle window;
        #endregion

        #region Draw Variables
        private Texture2D background;
        #endregion

        #region Component Variables
        public TileEnvironment environment;
        public AudioController audioControl;
        #endregion

        #region Entity Variables
        public MouseEntity mouse;
        public PlayerEntity player;
        public OgreEntity ogre;
        #endregion

        #region GUI Variables
        private Texture2D GuiTexture;
        private Color BackdropColor = new Color(100, 100, 100, 100);
        private Color HealthBarColor = new Color(200, 0, 0, 255);
        private Color TextColor = Color.WhiteSmoke;
        private int guiHeight = 24;
        private int borderWidth = 2;
        private int healthWidthScale = 8;
        #endregion

        #region Sound Variables
        #endregion

        public LeGame()
        {
            #region Graphics Setup
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = (int)CLIENT_SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = (int)CLIENT_SCREEN_HEIGHT;
            graphics.ApplyChanges();
            #endregion
            #region Content Setup
            Content.RootDirectory = "Content";
            #endregion

            #region Component Construction
            player = new PlayerEntity(this, PLAYER_STARTING_LOCATION);
            ogre = new OgreEntity(this, OGRE_STARTING_LOCATION, 0.0f);
            mouse = new MouseEntity(this);
            environment = new TileEnvironment(this, player, ENVIRONMENT_WIDTH, ENVIRONMENT_HEIGHT);
            audioControl = new AudioController(this, false, false);
            #endregion

            input = new InputHandler(this);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// </summary>
        protected override void Initialize()
        {
            window = new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height);
            base.Initialize();
        }

        /// <summary>
        /// The place to load all content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            #region Load Background
            background = Content.Load<Texture2D>("background/wood");
            #endregion
            #region Load Components
            player.Load(Content);
            environment.Load(Content);
            mouse.Load(Content);
            ogre.Load(Content);
            #endregion
            #region Load GUI
            GuiTexture = Content.Load<Texture2D>("gui/rect");
            font = Content.Load<SpriteFont>("fonts/GameFont");
            audioControl.Load(Content);
            #endregion
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) this.Exit();
            else if (Keyboard.GetState().IsKeyDown(Keys.Escape)) this.Exit();

            //Update core components
            input.Update(gameTime);

            //Update components
            player.Update(gameTime);
            environment.Update(gameTime);
            mouse.Update(Mouse.GetState(), gameTime);
            ogre.Update(player.getLocation(), gameTime);

            //Update GUI
            audioControl.Update(gameTime);

            //LOGIC
            environment.CheckCollisions(player);

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
            // Paint background
            spriteBatch.Draw(background, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);

            #region Draw Components
            environment.DrawCentered(spriteBatch);
            ogre.Draw(spriteBatch);
            player.DrawCentered(spriteBatch);
            mouse.Draw(spriteBatch);
            #endregion

            #region Draw GUI
            // Backdrop 
            spriteBatch.Draw(GuiTexture, new Rectangle(0, 0, Window.ClientBounds.Width, guiHeight), BackdropColor);
            // Player Health
            spriteBatch.Draw(GuiTexture, new Rectangle(borderWidth, borderWidth, (player.health * healthWidthScale) + (borderWidth * 2), guiHeight - (borderWidth * 2)), Color.Wheat);
            spriteBatch.Draw(GuiTexture, new Rectangle(borderWidth * 2, borderWidth * 2, (player.health * healthWidthScale), guiHeight - (borderWidth * 4)), HealthBarColor);
            spriteBatch.DrawString(font, "Health", new Vector2(70, 4), TextColor);
            // Player Location
            spriteBatch.DrawString(font, "X: " + (int)player.getLocation().X, new Vector2((borderWidth * 2) + (player.health * healthWidthScale) + 5, borderWidth * 2), Color.Wheat);
            spriteBatch.DrawString(font, "Y: " + (int)player.getLocation().Y, new Vector2((borderWidth * 2) + (player.health * healthWidthScale) + 55, borderWidth * 2), Color.Wheat);
            spriteBatch.DrawString(font, "A: " + (double)player.angle, new Vector2((borderWidth * 2) + (player.health * healthWidthScale) + 105, borderWidth * 2), Color.Wheat);
            // Player Stamina and Sprinting
            spriteBatch.DrawString(font, player.sprintFrame + "/" + player.sprintMax, new Vector2(10, 66), Color.Wheat);
            spriteBatch.DrawString(font, player.sprintTimeoutFrame + "/" + player.sprintTimeout, new Vector2(10, 80), Color.Wheat);
            // Audio Controls
            audioControl.Draw(spriteBatch, gameTime);
            #endregion

            // End drawing
            spriteBatch.End();
        }
    }
}
