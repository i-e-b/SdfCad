using System;

namespace Tkgl
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var window = new MainWindow())
            {
                window.Run(60);
            }
        }
    }
}
