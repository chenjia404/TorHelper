using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TorHelper
{
    public partial class FrmBrowser : Form
    {
        public FrmBrowser()
        {
            
            InitializeComponent();
        }

        private void webView21_CoreWebView2InitializationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
        }

        private void FrmBrowser_Load(object sender, EventArgs e)
        {

        }
    }
}
