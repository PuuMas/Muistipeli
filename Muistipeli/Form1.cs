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
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Kun painetaan "Aloitapeli" nappulaa kirjoitetaan pelaajan/pelaajien tiedot tiedostoon
            string tiedosto = @"c:\temp\Tiedosto.txt";
            {
                
                try
                {
                    //Jos tiedostoa ei ole niin se luodaan
                        if (!File.Exists(tiedosto))
                        {
                            File.Create(tiedosto).Dispose();

                            using (TextWriter tw = new StreamWriter(tiedosto))
                            {
                                tw.WriteLine("Pelattu aika " + DateTime.Now);
                                tw.WriteLine("Pelaajan nimi: " + textBox1.Text + "\n" + "Pelaajan ikä: " + numericUpDown2.Value);
                            }
                            if (!String.IsNullOrWhiteSpace(textBox2.Text))
                            {
                                using (TextWriter tw = new StreamWriter(tiedosto))
                                tw.WriteLine("Toisen pelaajan nimi : " + textBox2.Text + "\n" + "Toisen pelaajan ikä: " + numericUpDown3.Value);
                            }

                        }
                        //Jos tiedosto on jo olemassa siihen lisätään vaan rivejä
                        else if (File.Exists(tiedosto))
                        {
                            using (TextWriter tw = new StreamWriter(tiedosto, true))
                            {
                            tw.WriteLine("Pelattu aika: " + DateTime.Now);
                            tw.WriteLine("Pelaajan nimi: " + textBox1.Text + "\n" + "Pelaajan ikä: " + numericUpDown2.Value);

                            if (!String.IsNullOrWhiteSpace(textBox2.Text))
                            {
                                tw.WriteLine("Toisen pelaajan nimi : " + textBox2.Text + "\n" + "Toisen pelaajan ikä: " + numericUpDown3.Value);
                            }
                        }
                    }
                }
                //Jos jokin meni pieleen heittää sanomaa
                catch (Exception ex)
                {
                    MessageBox.Show("Jotain meni nyt mönkään!");
                    MessageBox.Show(ex.ToString());
                }

                //Tässä on peli valinta vaihtoehtoja
                //Jos valinta on 8palaa mutta tekstiä ja ikää ei ole annettu ei peli lähde käyntiin.(Iän pitää olla yli 0)
                if (comboBox1.Text == "8Palaa" && textBox1.Text != "" && numericUpDown2.Value > 0)
                {
                    peli1 peli1 = new peli1();
                    peli1.Show();
                    this.Visible = false;
                }
                if(comboBox1.Text == "12Palaa" && textBox1.Text != "" && numericUpDown2.Value > 0)
                {
                    peli2 peli2 = new peli2();
                    peli2.Show();
                    this.Visible = false;
                }
                if (comboBox1.Text == "16Palaa" && textBox1.Text != "" && numericUpDown2.Value > 0)
                {
                    peli3 peli3 = new peli3();
                    peli3.Show();
                    this.Visible = false;
                }
                if (comboBox1.Text == "18Palaa" && textBox1.Text != "" && numericUpDown2.Value > 0)
                {
                    peli4 peli4 = new peli4();
                    peli4.Show();
                    this.Visible = false;
                }
                if (comboBox1.Text == "32Palaa" && textBox1.Text != "" && numericUpDown2.Value > 0)
                {
                    peli5 peli5 = new peli5();
                    peli5.Show();
                    this.Visible = false;
                }
            }

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void resetoiToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        //Kaksinpeli valinta näyttää toisen pelaajan tieto kentät
        public void kaksinpeliToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label3.Visible = true;
            textBox2.Visible = true;
            label5.Visible = true;
            numericUpDown3.Visible = true;
        
        }
        //Yksinpeli valinta piilottaa toisen pelaajan tieto kentät
        public void yksinpeliToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label3.Visible = false;
            textBox2.Visible = false;
            label5.Visible = false;
            numericUpDown3.Visible = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //Menun exit lopettaa ohjelman
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        //Menun help nappi näyttää "about" formin
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about about = new about();
            about.Show();
        }

        private void tilastotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                read tilastot = new read();
                tilastot.Show();

            }catch(Exception ex)
            {
                
            }
        }
    }
}
