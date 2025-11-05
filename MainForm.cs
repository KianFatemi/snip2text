using System.Drawing.Imaging;

namespace Snip2Text
{
    public partial class MainForm : Form
    {

        int hotkeyId = 1;

        public MainForm()
        {
            InitializeComponent();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Hide();

            uint modifiers = NativeMethods.MOD_CONTROL | NativeMethods.MOD_SHIFT;
            uint virtualKey = (uint)Keys.C;

            if (!NativeMethods.RegisterHotKey(this.Handle, hotkeyId, modifiers, virtualKey))
            {
                MessageBox.Show("Failed to register global hotkey. It might be in use by another program.", "Error");
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_HOTKEY)
            {
                if (m.WParam.ToInt32() == hotkeyId)
                {
                    var screenshot = CaptureScreen();
                    if (screenshot == null) return;

                    this.Hide();

                    using (var snipForm = new SnippingForm(screenshot))
                    {
                        snipForm.ShowDialog();
                    }

                    this.Show();
                    this.Hide();
                }
            }
            base.WndProc(ref m);
        }

        void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            NativeMethods.UnregisterHotKey(this.Handle, hotkeyId);
        }

        Bitmap? CaptureScreen()
        {
            int minX = SystemInformation.VirtualScreen.Left;
            int minY = SystemInformation.VirtualScreen.Top;
            int width = SystemInformation.VirtualScreen.Width;
            int height = SystemInformation.VirtualScreen.Height;

            if (width == 0 || height == 0) return null;

            //create buitmap to hold screenshot
            var screenshot = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            using (Graphics graphics = Graphics.FromImage(screenshot))
            {
                //copy screen contents into bitmap
                graphics.CopyFromScreen(minX, minY, 0, 0, new Size(width, height), CopyPixelOperation.SourceCopy);
            }

            return screenshot;
        }
    }
}
