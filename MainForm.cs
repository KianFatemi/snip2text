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
                    MessageBox.Show("Hotkey pressed!");

                    // TODO: this will call StartSnipping()
                }
            }
            base.WndProc(ref m);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            NativeMethods.UnregisterHotKey(this.Handle, hotkeyId);
        }
    }
}
