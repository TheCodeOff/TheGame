using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using TheGame.Environment;
using TheGame.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace TheGame.States
{
    class StateSingle : GameState
    {
        #region Textures
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

        public StateSingle(LeGame g)
            : base(g)
        {
            #region Entities Construction
            player = new PlayerEntity(game, LeGame.PLAYER_STARTING_LOCATION);
            ogre = new OgreEntity(game, LeGame.OGRE_STARTING_LOCATION, 0.0f);
            mouse = new MouseEntity(game);
            #endregion
            #region Component Construction
            environment = new TileEnvironment(game, player, LeGame.ENVIRONMENT_WIDTH, LeGame.ENVIRONMENT_HEIGHT);
            audioControl = new AudioController(game, false, false);
            #endregion
        }

        public void Initialize() { }

        public void LoadContent(ContentManager c)
        {
            #region Load Background
            background = c.Load<Texture2D>("background/wood");
            #endregion
            #region Load Entities
            player.Load(c);
            mouse.Load(c);
            ogre.Load(c);
            #endregion
            #region Load Components
            environment.Load(c);
            audioControl.Load(c);
            #endregion
            #region Load GUI
            GuiTexture = c.Load<Texture2D>("gui/rect");
            #endregion
        }

        public void Update(GameTime g)
        {
            #region Update Entities
            player.Update(g);
            environment.Update(g);
            mouse.Update(Mouse.GetState(), g);
            ogre.Update(player, g);
            #endregion
            #region Update Components
            audioControl.Update(g);
            #endregion
            #region Logic
            environment.CheckCollisions(player);
            #endregion
        }

        internal void Draw(SpriteBatch s, GameTime g)
        {
            #region Draw background
            if (LeGame.DRAW_BACKGROUND)
                s.Draw(background, new Rectangle(0, 0, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height), Color.White);
            else
                s.GraphicsDevice.Clear(new Color(33, 33, 58));
            #endregion
            #region Draw Environment Component
            environment.DrawCentered(s);
            #endregion
            #region Draw Entities
            ogre.DrawRelativeToPlayer(player, s);
            player.DrawCentered(s);
            mouse.Draw(s);
            #endregion
            #region Draw GUI
            // Backdrop 
            s.Draw(GuiTexture, new Rectangle(0, 0, game.Window.ClientBounds.Width, guiHeight), BackdropColor);
            // Player Health
            s.Draw(GuiTexture, new Rectangle(borderWidth, borderWidth, (player.health * healthWidthScale) + (borderWidth * 2), guiHeight - (borderWidth * 2)), Color.Wheat);
            s.Draw(GuiTexture, new Rectangle(borderWidth * 2, borderWidth * 2, (player.health * healthWidthScale), guiHeight - (borderWidth * 4)), HealthBarColor);
            s.DrawString(LeGame.gameFont, "Health", new Vector2(70, 4), TextColor);
            // Player Location
            s.DrawString(LeGame.gameFont, "X: " + (int)player.getLocation().X, new Vector2((borderWidth * 2) + (player.health * healthWidthScale) + 5, borderWidth * 2), Color.Wheat);
            s.DrawString(LeGame.gameFont, "Y: " + (int)player.getLocation().Y, new Vector2((borderWidth * 2) + (player.health * healthWidthScale) + 55, borderWidth * 2), Color.Wheat);
            s.DrawString(LeGame.gameFont, "A: " + (double)player.angle, new Vector2((borderWidth * 2) + (player.health * healthWidthScale) + 105, borderWidth * 2), Color.Wheat);
            // Player Stamina and Sprinting
            s.DrawString(LeGame.gameFont, player.sprintFrame + "/" + player.sprintMax, new Vector2(10, 66), Color.Wheat);
            s.DrawString(LeGame.gameFont, player.sprintTimeoutFrame + "/" + player.sprintTimeout, new Vector2(10, 80), Color.Wheat);
            // Audio Controls
            audioControl.Draw(s, g);
            #endregion
        }

        internal void KeyPressed(Keys[] keys)
        {
            #region Player control keys
            if (keys.Contains(Keys.W)) player.PlayerPressedKey(Keys.W);
            if (keys.Contains(Keys.A)) player.PlayerPressedKey(Keys.A);
            if (keys.Contains(Keys.D)) player.PlayerPressedKey(Keys.D);
            if (keys.Contains(Keys.Q)) player.PlayerPressedKey(Keys.Q);
            if (keys.Contains(Keys.LeftShift)) player.SetSprinting(true);  // SHIFT - Sprint.
            else if (!keys.Contains(Keys.LeftShift)) player.SetSprinting(false); //Stop Sprint  
            #endregion
        }
    }
}
