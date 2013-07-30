using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncPluggableProtocol
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Load += MainForm_Load;
        }

        void MainForm_Load(object sender, EventArgs e)
        {
            ProtocolFactory.Register("rsrc", () => new ResourceProtocol());
            var html = @"<html><body style=""background-image: url(rsrc:helloworld.png)""></body></html>";
            Browser.DocumentText = html;
        }
    }
}
