using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TheGame.Environment
{
    public class TileTextureLoader
    {
        private ContentManager c;
        #region TILE TYPES
        public static int VOID = 0;
        public static String VOID_PATH = "environment/void";
        public static int GRASS = 1;
        public static String GRASS_PATH = "environment/grass";
        public static int STONE_ROAD = 2;
        public static String STONE_ROAD_PATH = "environment/stoneroad/stoneroad";
        public static int WATER = 3;
        public static String WATER_PATH = "environment/water";
        public static int SAND = 4;
        public static String SAND_PATH = "environment/sand";
        public static int ICE = 5;
        public static String ICE_PATH = "environment/ice";
        #endregion

        public TileTextureLoader() { }

        public void prepareTextures(ContentManager c)
        {
            this.c = c;
        }

        public Texture2D getTexture(int tileID)
        {
            switch (tileID)
            {
                case 0:
                    return c.Load<Texture2D>(VOID_PATH);
                case 3:
                    return c.Load<Texture2D>(WATER_PATH);
                case 4:
                    return c.Load<Texture2D>(SAND_PATH);
                case 5:
                    return c.Load<Texture2D>(ICE_PATH);
                default: // 1
                    return c.Load<Texture2D>(GRASS_PATH);
            }
        }

        public Texture2D getTexture(int tileID, String data)
        {
            if (tileID == STONE_ROAD)
            {
                return c.Load<Texture2D>(STONE_ROAD_PATH + data);
            }
            else return getTexture(tileID);
        }

        public bool isWalkable(int tileID)
        {
            switch (tileID)
            {
                case 0:
                    return false;
                default:
                    return true;
            }
        }
    }
}
