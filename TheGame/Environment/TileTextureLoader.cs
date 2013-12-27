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
        public static int ICE = 3;
        public static String ICE_PATH = "environment/ice2";
        public static int SAND = 3;
        public static String SAND_PATH = "environment/sand";
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
                case 2:
                    return c.Load<Texture2D>(STONE_ROAD_PATH + "0000");
                case 3:
                    return c.Load<Texture2D>(ICE_PATH);
                default:
                    return c.Load<Texture2D>(GRASS_PATH);
            }
        }

        public Texture2D getTexture(int tileID, String data)
        {
            switch (tileID)
            {
                case 0:
                    return c.Load<Texture2D>(VOID_PATH);
                case 2:
                    try
                    {
                        return c.Load<Texture2D>(STONE_ROAD_PATH + data);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("missing road texture:" + data);
                        return c.Load<Texture2D>(STONE_ROAD_PATH + "0000");
                    }
                case 3:
                    return c.Load<Texture2D>(ICE_PATH);
                default:
                    return c.Load<Texture2D>(GRASS_PATH);
            }
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
