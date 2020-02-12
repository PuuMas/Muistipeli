using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Muistipeli
{
    public partial class read : Form
    {
        public read()
        {
            InitializeComponent();
            try
            {
                String texti = System.IO.File.ReadAllText(@"C:\\temp\\Tiedosto.txt");
                label2.Text = texti;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiedostoa ei löytynyt!");
                this.Dispose();

            }
        }

        private void read_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void tyhjennäTilastotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult kysy = MessageBox.Show("Haluatko varmasti tyhjentää Tilastot?","Tyhjennä tilastot", MessageBoxButtons.YesNo);
            if(kysy == DialogResult.Yes)
            {
                string tiedosto = @"c:\temp\Tiedosto.txt";

                File.WriteAllText(tiedosto, String.Empty);
                label2.Text = "";
            }

        }
    }
}
