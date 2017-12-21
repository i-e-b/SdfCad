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
            using (var previewWindow = new PreviewWindow())
            {
                var controls = new ViewControls(previewWindow);
                controls.Show();
                previewWindow.Run(60);
            }
        }
    }
}
