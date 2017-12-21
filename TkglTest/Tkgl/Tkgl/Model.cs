using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace Tkgl
{
    public partial class Model : Form
    {
        private readonly PreviewWindow _previewWindow;

        public Model(PreviewWindow previewWindow)
        {
            _previewWindow = previewWindow;
            InitializeComponent();
        }

        private void loadButton_Click(object sender, EventArgs e) { openFileDialog.ShowDialog(); }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            programTextBox.Text = File.ReadAllText(openFileDialog.FileName);
        }

        private void uploadButton_Click(object sender, EventArgs e)
        {
            _previewWindow.SetModelFunction(programTextBox.Text);
        }
    }
}
