using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using TheGame.Entities;

namespace TheGame.Environment
{
    public class Tile
    {
        private Texture2D parentTexture; //  GRASS
        private Texture2D texture;
        //private AnimatedSprite sprite;
        public Rectangle tilespace;
        private int x, y, typeID;
        private bool isWalkable;

        /// <summary>
        /// Create a new tile
        /// </summary>
        /// <param name="texture">The tile's texture</param>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="tileID">The ID of the tile</param>
        /// <param name="isWalkable">Whether the tile can be walked upon</param>
        public Tile(Texture2D texture, int x, int y, int tileID, bool isWalkable)
        {
            this.texture = texture;
            this.parentTexture = texture;
            this.x = x;
            this.y = y;
            // TODO: Implement tile heights
            this.typeID = tileID;
            this.isWalkable = isWalkable;
            //sprite = new AnimatedSprite(texture, 1, 4, 1.0);
            tilespace = new Rectangle((int)(x * LeGame.ENVIRONMENT_TILE_SIZE), (int)(y * LeGame.ENVIRONMENT_TILE_SIZE), LeGame.ENVIRONMENT_TILE_SIZE, LeGame.ENVIRONMENT_TILE_SIZE);
        }

        public void PlayerWalkOnTile(PlayerEntity player)
        {
            if (!isWalkable)
            {
                player.WalkOnUnwalkableTile();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //for updating tiles
            //sprite.Draw(spriteBatch, new Vector2((float)(y * size), (float)(x * size)), 0.0f);
            spriteBatch.Draw(texture, tilespace, Color.White);
        }

        public void DrawCentered(SpriteBatch spriteBatch)
        {
            // Get draw location relative to player location
            int dX = (int)(PlayerEntity.drawLocation.X - PlayerEntity.location.X + tilespace.X);
            int dY = (int)(PlayerEntity.drawLocation.Y - PlayerEntity.location.Y + tilespace.Y);
            Rectangle drawLocation = new Rectangle(dX, dY, tilespace.Width, tilespace.Height);
            // If this tile is part of a road, we draw grass underneath.
            if(typeID == TileTextureLoader.STONE_ROAD)
                spriteBatch.Draw(parentTexture, drawLocation, Color.White);
            spriteBatch.Draw(texture, drawLocation, Color.White);
        }

        public void SetWalkable(bool p)
        {
            this.isWalkable = p;
        }

        public void Update(GameTime gameTime)
        {
            //sprite.Update();
        }

        public void SetTexture(Texture2D texture2D)
        {
            this.texture = texture2D;
        }

        public Texture2D GetTexture()
        {
            return texture;
        }

        public int GetTypeID()
        {
            return typeID;
        }

        public void SetTypeID(int p)
        {
            typeID = p;
        }
    }
}
