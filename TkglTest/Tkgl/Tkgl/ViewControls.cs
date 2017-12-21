using System.Windows.Forms;

namespace Tkgl
{
    public partial class ViewControls : Form
    {
        private readonly PreviewWindow _previewWindow;
        float theta = 0.0f;
        float elevation = 1.0f;
        float fov = 2.0f;

        public ViewControls(PreviewWindow previewWindow)
        {
            _previewWindow = previewWindow;
            InitializeComponent();
        }

        private void UpdatePreview()
        {
            _previewWindow.CameraPosition(theta, elevation);
            _previewWindow.Fov(fov);
        }

        private void camXZTrack_ValueChanged(object sender, System.EventArgs e)
        {
            theta = camXZTrack.Value / 100.0f; // 0..2π
            UpdatePreview();
        }

        private void camYTrack_ValueChanged(object sender, System.EventArgs e)
        {
            elevation = camYTrack.Value / 100.0f; // 0..2π
            UpdatePreview();
        }

        private void camFovTrack_ValueChanged(object sender, System.EventArgs e)
        {
            fov = 1.0f + (camFovTrack.Value / 50.0f); // 1..3;
            UpdatePreview();
        }
    }
}
