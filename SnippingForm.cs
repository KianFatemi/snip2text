using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snip2Text
{
    public partial class SnippingForm : Form
    {
        readonly Bitmap screenCapture;
        Point startPoint;
        bool isSelecting = false;

        Rectangle selectionRectangle;

        public Rectangle SelectionRectangle { get; private set; }

        public SnippingForm(Bitmap screenCapture)
        {
            InitializeComponent();

            this.screenCapture = screenCapture;

            this.DoubleBuffered = true;
        }

        private void SnippingForm_MouseDown(object sender, MouseEventArgs e)
        {
            isSelecting = true;
            startPoint = e.Location;
        }

        private void SnippingForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isSelecting) return;

            int x = Math.Min(startPoint.X, e.Location.X);
            int y = Math.Min(startPoint.Y, e.Location.Y);
            int width = Math.Abs(startPoint.X - e.Location.X);
            int height = Math.Abs(startPoint.Y - e.Location.Y);

            selectionRectangle = new Rectangle(x, y, width, height);

            this.Invalidate();
        }

        private void SnippingForm_MouseUp(object sender, MouseEventArgs e)
        {
            isSelecting = false;

            SelectionRectangle = selectionRectangle;

            this.Close();
        }

        private void SnippingForm_Paint(object sender, PaintEventArgs e)
        {
            if (isSelecting && selectionRectangle.Width > 0 && selectionRectangle.Height > 0)
            {
                e.Graphics.DrawImage(screenCapture, selectionRectangle, selectionRectangle, GraphicsUnit.Pixel);

                using (Pen pen = new Pen(Color.Red, 1))
                {
                    e.Graphics.DrawRectangle(pen, selectionRectangle);
                }
            }
        }
    }
}
