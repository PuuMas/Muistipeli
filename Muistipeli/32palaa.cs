using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Muistipeli
{
    public partial class _32palaa : Form
    {
        public _32palaa()
        {
            InitializeComponent();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about about = new about();
            about.Show();
        }

        private void tilastotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            read tulokset = new read();
            tulokset.Show();
        }
    }
}
