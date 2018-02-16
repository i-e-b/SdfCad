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
        readonly int argbWhite = Color.White.ToArgb();

        private void TestSlice()
        {
            if (outputPictureBox.Image != null) {
                outputPictureBox.Image.Dispose();
            }
            Bitmap input = new Bitmap(Image.FromFile("Example2.png"));
            //using (var g = Graphics.FromImage(input))
            {
                for (int y = 1; y < input.Height - 1; y++)
                {
                    for (int x = 1; x < input.Width - 1; x++)
                    {
                        var color = input.GetPixel(x, y);
                        if (color.ToArgb() == argbWhite)
                        {
                            //input.SetPixel(x, y, Color.Black);
                        }
                        else if (IsEdge(input, x, y))
                        { // is an edge
                            input.SetPixel(x, y, Color.Black);
                        }
                        else
                        {
                            input.SetPixel(x, y, Color.FromArgb(
                                127+color.R/2/*red is left/right*/,
                                127 /* green is up/down */,
                                127+color.B/2/*blue is front/back*/));
                        }
                    }
                }
            }
            outputPictureBox.Image = input;
        }

        private bool IsEdge(Bitmap input, int x, int y)
        {
            if (input.GetPixel(x,y).ToArgb() == argbWhite) return false;
            if (input.GetPixel(x-1,y).ToArgb() == argbWhite) return true;
            if (input.GetPixel(x,y-1).ToArgb() == argbWhite) return true;
            if (input.GetPixel(x+1,y).ToArgb() == argbWhite) return true;
            if (input.GetPixel(x,y+1).ToArgb() == argbWhite) return true;
            return false;
        }
    }
}
