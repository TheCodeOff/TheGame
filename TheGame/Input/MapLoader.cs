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
        //private int[] data;

        public EnvironmentLoader(TileEnvironment environment)
        {
            this.environment = environment;
            //this.data = new int[environment.width * environment.height];
        }

        public void LoadTileTextureMap()
        {
            int counter = 0;
            string line;

            // Read the file and display it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader("Content\\maps\\worldmap.txt");
            while ((line = file.ReadLine()) != null)
            {
                char[] lineData = line.ToCharArray();
                for (int x = 0; x < lineData.Length; x++)
                {
                    int textureID = Convert.ToInt32(lineData[x] + "");
                    if (textureID == 0) environment.tileset[(counter * environment.width) + x].SetWalkable(false);
                    environment.tileset[(counter * environment.width) + x].SetTexture(environment.textures[textureID]);
                }
                counter++;
            }

            file.Close();
        }
    }
}
