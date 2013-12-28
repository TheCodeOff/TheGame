using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheGame.Environment;

namespace TheGame
{
    public class EnvironmentLoader
    {
        private TileEnvironment environment;
        private string[] lines;

        public EnvironmentLoader(TileEnvironment environment)
        {
            this.environment = environment;
        }

        public void LoadTileTextureMap()
        {
            string line;
            string lns = "";

            // Read the file line by line.
            System.IO.StreamReader file = new System.IO.StreamReader("Content\\maps\\worldmap.txt");
            while ((line = file.ReadLine()) != null)
            {
                lns += line + "#";
            }
            file.Close();

            // Split the lines into a array.
            lines = lns.Split('#');

            // Goes through every line.
            for (int row = 0; row < lines.Length; row++)
            {
                char[] lineData = lines[row].ToCharArray();
                for (int col = 0; col < lineData.Length; col++)
                {
                    int textureID = Convert.ToInt32(lineData[col] + "");
                    // Make unwalkable tiles unwalkable
                    if (textureID == 0) environment.tileset[(row * environment.width) + col].SetWalkable(environment.texLoader.isWalkable(textureID));
                    // Set the typeID for the current tile
                    environment.tileset[(row * environment.width) + col].SetTypeID(textureID);
                    // Set the texture for simple tiles.
                    environment.tileset[(row * environment.width) + col].SetTexture(environment.texLoader.getTexture(textureID));
                }
            }
            // Fixes connected road textures
            ReloadRoadTextures();
        }

        public void ReloadRoadTextures()
        {
            for (int row = 1; row < LeGame.ENVIRONMENT_HEIGHT - 1; row++)
            {
                for (int col = 1; col < LeGame.ENVIRONMENT_WIDTH - 1; col++)
                {
                    Tile current = environment.tileset[(row * environment.width) + col];
                    if (current.GetTypeID() == TileTextureLoader.STONE_ROAD)
                    {
                        string data = "";
                        // Get the correct texture for the connected roads.
                        // TOP -------------------------------------------------------------------------------------------------------------
                        Tile top = environment.tileset[((row - 1) * environment.width) + col];
                        if (current.GetTypeID() == top.GetTypeID())
                            data += "1";
                        else
                            data += "0";
                        // BOTTOM -------------------------------------------------------------------------------------------------------------
                        Tile bottom = environment.tileset[((row + 1) * environment.width) + col];
                        if (current.GetTypeID() == bottom.GetTypeID())
                            data += "1";
                        else
                            data += "0";
                        // LEFT -------------------------------------------------------------------------------------------------------------
                        Tile left = environment.tileset[(row * environment.width) + col - 1];
                        if (current.GetTypeID() == left.GetTypeID())
                            data += "1";
                        else
                            data += "0";
                        // RIGHT -------------------------------------------------------------------------------------------------------------
                        Tile right = environment.tileset[(row * environment.width) + col + 1];
                        if (current.GetTypeID() == right.GetTypeID())
                            data += "1";
                        else
                            data += "0";

                        // Write line for debugging
                        ReportComparation(current, top, bottom, left, right, data);
                        // Set the texture for the current road tile.
                        environment.tileset[(row * environment.width) + col].SetTexture(environment.texLoader.getTexture(TileTextureLoader.STONE_ROAD, data));
                    }
                }
            }
        }

        internal void ReportComparation(Tile current, Tile top, Tile bottom, Tile left, Tile right, String data)
        {
            Console.Write("Comparating tile at (" + (current.tilespace.X / current.tilespace.Width) + "," + (current.tilespace.Y / current.tilespace.Width) + ")");
            Console.Write(" with tiles at (" + (top.tilespace.X / top.tilespace.Width) + "," + (top.tilespace.Y / top.tilespace.Width) + "), ");
            Console.Write("(" + (bottom.tilespace.X / bottom.tilespace.Width) + "," + (bottom.tilespace.Y / bottom.tilespace.Width) + "), ");
            Console.Write("(" + (left.tilespace.X / left.tilespace.Width) + "," + (left.tilespace.Y / left.tilespace.Width) + "), ");
            Console.WriteLine("(" + (right.tilespace.X / right.tilespace.Width) + "," + (right.tilespace.Y / right.tilespace.Width) + ") -> Found data (" + data + ")");

        }
    }
}
