using System;
using System.Reflection;

namespace Donker.Pong.Game
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        public static AssemblyName AssemblyName = Assembly.GetExecutingAssembly().GetName();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new MainGame())
                game.Run();
        }
    }
#endif
}
