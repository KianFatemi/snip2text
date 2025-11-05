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
        private readonly Bitmap screenCapture;
        public SnippingForm(Bitmap screenCapture)
        {
            InitializeComponent();

            this.screenCapture = screenCapture;
        }
    }
}
