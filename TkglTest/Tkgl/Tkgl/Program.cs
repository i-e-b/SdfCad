using System;
using System.Windows.Forms;

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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var previewWindow = new PreviewWindow())
            {
                var controls = new ViewControls(previewWindow);
                controls.Show();
                controls.UpdatePreview();
                previewWindow.Run(30);
            }
        }
    }
}
