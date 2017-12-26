using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Tkgl
{
    public partial class ViewControls : Form
    {
        private readonly PreviewWindow _previewWindow;
        float theta = 0.0f;
        float elevation = 1.0f;
        float fov = 2.0f;
        float distance = 1.0f;

        public ViewControls(PreviewWindow previewWindow)
        {
            _previewWindow = previewWindow;
            InitializeComponent();
            camXZTrack_ValueChanged(null,null);
            camYTrack_ValueChanged(null,null);
            camFovTrack_ValueChanged(null,null);
            camDistTrack_ValueChanged(null,null);
        }

        public void UpdatePreview()
        {
            _previewWindow.CameraPosition(theta, elevation, distance);
            _previewWindow.Fov(fov);
        }

        private void RebindShaderOptions()
        {
            _previewWindow.SetSlicePreview(slicePreviewCheckBox.Checked);
            _previewWindow.RebindShader(ambientOccCheckbox.Checked, shadowCheckbox.Checked, reflectionsCheckbox.Checked);
        }

        private void camXZTrack_ValueChanged(object sender, System.EventArgs e)
        {
            theta = camXZTrack.Value / 100.0f; // 0..2π
            UpdatePreview();
        }

        private void camYTrack_ValueChanged(object sender, System.EventArgs e)
        {
            elevation = (camYTrack.Value / 100.0f) - 1.0f; // -1..9
            UpdatePreview();
        }

        private void camFovTrack_ValueChanged(object sender, System.EventArgs e)
        {
            fov = 1.0f + (camFovTrack.Value / 50.0f); // 1..3;
            UpdatePreview();
        }

        private void camDistTrack_ValueChanged(object sender, System.EventArgs e)
        {
            distance = 1.0f + (camDistTrack.Value / 100.0f); // 1..11;
            UpdatePreview();
        }

        private void ambientOccCheckbox_CheckedChanged(object sender, System.EventArgs e)
        {
            RebindShaderOptions();
        }

        private void shadowCheckbox_CheckedChanged(object sender, System.EventArgs e)
        {
            RebindShaderOptions();
        }

        private void reflectionsCheckbox_CheckedChanged(object sender, System.EventArgs e)
        {
            RebindShaderOptions();
        }

        private void nearFieldTrack_ValueChanged(object sender, System.EventArgs e)
        {
            var near = 0.5f + (nearFieldTrack.Value / 200.0f); // 0.5..5.5;
            _previewWindow.SetNearLimit(near);
        }

        private void slicePreviewCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            RebindShaderOptions();
        }

        private void screenshotButton_Click(object sender, System.EventArgs e)
        {
            using (var bmp = _previewWindow.TakeScreenshot()) {
                bmp.Save(@"C:\Temp\Screenshot.png", ImageFormat.Png);
            }
        }
    }
}
