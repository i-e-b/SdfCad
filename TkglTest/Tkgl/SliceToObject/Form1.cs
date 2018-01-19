using System.Drawing;
using System.Windows.Forms;

namespace SliceToObject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            TestSlice();
        }

        private void TestSlice()
        {
            if (outputPictureBox.Image != null) {
                outputPictureBox.Image.Dispose();
            }
            Bitmap input = new Bitmap(Image.FromFile("Example2.png"));
            var argbWhite = Color.White.ToArgb();
            //using (var g = Graphics.FromImage(input))
            {
                for (int y = 0; y < input.Height; y++)
                {
                    for (int x = 0; x < input.Width; x++)
                    {
                        var color = input.GetPixel(x,y);
                        if (color.ToArgb() == argbWhite) {
                            input.SetPixel(x, y, Color.Black);
                        }
                        else {
                            input.SetPixel(x, y, Color.FromArgb(color.R, 0 /* green is up/down */, color.B));
                        }
                    }
                }
            }
            outputPictureBox.Image = input;
        }
    }
}
