using System;
using TheGame;
namespace TheGame
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (LeGame game = new LeGame())
            {
                game.Run();
            }
        }
    }
#endif
}

