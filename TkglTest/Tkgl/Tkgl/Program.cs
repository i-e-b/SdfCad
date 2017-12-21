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
            using (var controls = new ViewControls(previewWindow))
            using (var model = new Model(previewWindow))
            {
                model.Show();
                controls.Show();
                controls.UpdatePreview();
                previewWindow.Run(30);
            }
        }
    }
}
