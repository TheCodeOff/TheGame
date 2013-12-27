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
using System.Collections;
using TheGame.Entities;
using TheGame.Environment;


namespace TheGame.Environment
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class TileEnvironment : DrawableGameComponent
    {
        private PlayerEntity player;
        private Point prevLocation;
        private Point location;
        public Texture2D[] textures;

        public Tile[] tileset;
        public int width;
        public int height;

        public TileEnvironment(Game game, PlayerEntity player, int width, int height)
            : base(game)
        {
            this.player = player;
            location = new Point((int)player.getLocation().X, (int)player.getLocation().Y);
            prevLocation = location;
            this.width = width;
            this.height = height;
            tileset = new Tile[width * height];
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public void Load(ContentManager content)
        {
            textures = new TileTextureList().GetTextureList(content);

            int i = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    tileset[i] = new Tile(textures[1], x, y, 0, true);
                    // Put walk border on sides of map to prevent user from exiting game screen.
                    if (x == 0 || x == (width - 1) || y == 0 || y == (height - 1))
                    {
                        // No texture / transparent / void
                        tileset[i].SetTexture(textures[0]);
                        // Unwalkable
                        tileset[i].SetWalkable(false);
                    }
                    i++;
                }
            }

            //Load texture and collision map
            EnvironmentLoader loader = new EnvironmentLoader(this);
            loader.LoadTileTextureMap();
        }

        public override void Update(GameTime gameTime)
        {
            prevLocation = location;
            location = new Point((int)player.getLocation().X, (int)player.getLocation().Y);

            for (int i = 0; i < tileset.Length; i++) tileset[i].Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// Draw the environment starting in the top left corner of the window.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < tileset.Length; i++)
            {
                if (!LeGame.FOG_OF_WAR_ENABLED || (player.sight.Intersects(tileset[i].tilespace)))
                {
                    tileset[i].Draw(spriteBatch);
                }
            }
        }

        /// <summary>
        /// Draw the environment around the player in the center of the screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void DrawCentered(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < tileset.Length; i++)
            {
                if (!LeGame.FOG_OF_WAR_ENABLED || (player.sight.Intersects(tileset[i].tilespace)))
                {
                    tileset[i].DrawCentered(spriteBatch);
                }
            }
        }

        /// <summary>
        /// Check for player collisions with tiles
        /// </summary>
        /// <param name="player"></param>
        public void CheckCollisions(PlayerEntity player)
        {
            for (int i = 0; i < tileset.Length; i++)
            {
                if (tileset[i].tilespace.Intersects(player.collisionBox))
                {
                    tileset[i].PlayerWalkOnTile(player);
                }
            }

        }
    }
}

