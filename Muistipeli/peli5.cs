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
    public partial class peli5 : Form
    {

        bool vuoro = false;
        PictureBox ekaArvaus;
        Random rnd = new Random();
        Timer aika = new Timer { Interval = 1000 };
        Timer klikkiAjastin = new Timer();
        System.Windows.Forms.Form f = System.Windows.Forms.Application.OpenForms["Form1"];
        //Esitellään tietue
        Muuttujat muuttujat = new Muuttujat();

        public struct Muuttujat
        {
            public int eka_pelaaja;
            public int toka_pelaaja;
            public int pistee_eka;
            public int pisteet_toka;
        }


        public peli5()
        {
            InitializeComponent();
            PelinAloitus();

        }

        private PictureBox[] KuvaListat
        {
            get { return Controls.OfType<PictureBox>().ToArray(); }
        }

        private static IEnumerable<Image> Kuvat
        {
            get
            {
                return new Image[]
                {
                    Properties.Resources.finland,
                    Properties.Resources.france,
                    Properties.Resources.united_states_of_america,
                    Properties.Resources.liberia,
                    Properties.Resources.gabon,
                    Properties.Resources.norway,
                    Properties.Resources.hawaii,
                    Properties.Resources.iceland,
                    Properties.Resources.japan,
                    Properties.Resources.england,
                    Properties.Resources.mauritius,
                    Properties.Resources.solomon_islands,
                    Properties.Resources.wales,
                    Properties.Resources.turkey,
                    Properties.Resources.vatican_city,
                    Properties.Resources.saudi_arabia
                };
            }
        }
        //Tämä funktio resetoi kuvat
        private void ResetoiKuvat()
        {
            foreach (var kuva in KuvaListat)
            {
                kuva.Tag = null;
                kuva.Visible = true;
            }
            PiilotaKuvat();
            SekoitaKuvat();
            aika.Start();
        }

        //Tämä funktio piilottaa kuvat
        private void PiilotaKuvat()
        {
            foreach (var kuva in KuvaListat)
            {
                kuva.Image = Properties.Resources.cadrback;
            }
        }

        //Tämä funktio valitsee vapaat kuvat
        private PictureBox VapaaVali()
        {
            int numero;
            do
            {
                numero = rnd.Next(0, KuvaListat.Count());
            }
            while (KuvaListat[numero].Tag != null);
            return KuvaListat[numero];
        }

        //Sekoitetaan kuvat ja valitaan 2paria.
        private void SekoitaKuvat()
        {
            foreach (var kuva in Kuvat)
            {
                //Jää jumiin..
                VapaaVali().Tag = kuva;
                VapaaVali().Tag = kuva;
            }
        }

        private void PelinAloitus()
        {
            vuoro = true;
            ResetoiKuvat();
            PiilotaKuvat();
            pelikello();
            klikkiAjastin.Interval = 1000;
            klikkiAjastin.Tick += ajastin;

        }

        private void ajastin(object sender, EventArgs e)
        {
            PiilotaKuvat();
            vuoro = true;
            klikkiAjastin.Stop();
        }

        private void pelikello()
        {
            aika.Start();
            aika.Tick += delegate
            {
            };
        }

        private void yksinpeliToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)Application.OpenForms["Form1"];
            if (form1 != null)
            {
                form1.Show();
            }
            this.Dispose();
        }

        private void kaksinpeliToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)Application.OpenForms["Form1"];
            if (form1 != null)
            {
                form1.Show();
                form1.label3.Visible = true;
                form1.textBox2.Visible = true;
                form1.label5.Visible = true;
                form1.numericUpDown3.Visible = true;
            }
            this.Dispose();
        }

        private void resetoiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetoiKuvat();

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void peli5_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!vuoro) return;

                var kuva = (PictureBox)sender;

                if (ekaArvaus == null)
                {
                    ekaArvaus = kuva;
                    kuva.Image = (Image)kuva.Tag;
                    return;
                }
                kuva.Image = (Image)kuva.Tag;

                if (kuva.Image == ekaArvaus.Image && kuva != ekaArvaus && muuttujat.eka_pelaaja == 0)
                {
                    kuva.Visible = ekaArvaus.Visible = false;
                    {
                        ekaArvaus = kuva;
                        muuttujat.pistee_eka++;
                    }
                    PiilotaKuvat();
                }
                else if (kuva.Image == ekaArvaus.Image && kuva != ekaArvaus && muuttujat.toka_pelaaja == 1)
                {
                    kuva.Visible = ekaArvaus.Visible = false;
                    {
                        ekaArvaus = kuva;
                        muuttujat.pisteet_toka++;
                    }
                    PiilotaKuvat();
                }
                else
                {

                    if (!String.IsNullOrEmpty(((Form1)f).textBox2.Text) && muuttujat.eka_pelaaja == 0)
                    {
                        MessageBox.Show(((Form1)f).textBox2.Text + "n vuoro!");
                        vuoro = false;
                        klikkiAjastin.Start();
                        muuttujat.eka_pelaaja++;
                        muuttujat.toka_pelaaja++;
                    }

                    else if (!String.IsNullOrEmpty(((Form1)f).textBox2.Text) && muuttujat.toka_pelaaja == 1)
                    {
                        MessageBox.Show(((Form1)f).textBox1.Text + "n vuoro!");
                        vuoro = false;
                        klikkiAjastin.Start();
                        muuttujat.toka_pelaaja--;
                        muuttujat.eka_pelaaja--;
                    }
                    else
                    {
                        vuoro = false;
                        klikkiAjastin.Start();
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Klikkasit ohi! Klikkaa korttia!");
            }
            ekaArvaus = null;

            if (KuvaListat.Any(p => p.Visible)) return;

            if (!String.IsNullOrEmpty(((Form1)f).textBox2.Text))
            {
                if (muuttujat.pistee_eka < muuttujat.pisteet_toka)
                {
                    MessageBox.Show(((Form1)f).textBox2.Text + " voitti!");
                }
                if (muuttujat.pistee_eka > muuttujat.pisteet_toka)
                {
                    MessageBox.Show(((Form1)f).textBox1.Text + " voitti!");
                }
                if (muuttujat.pistee_eka == muuttujat.pisteet_toka)
                {
                    MessageBox.Show("Tasapeli!");
                }

            }

            MessageBox.Show("Peli päättyi!");

            string tiedosto = @"c:\temp\Tiedosto.txt";

            try
            {
                if (!File.Exists(tiedosto) && String.IsNullOrEmpty(((Form1)f).textBox2.Text))
                {
                    File.Create(tiedosto).Dispose();

                    using (TextWriter tw = new StreamWriter(tiedosto))
                    {
                        tw.WriteLine("Pisteiden määrä: " + ((Form1)f).textBox1.Text + " " + muuttujat.pistee_eka);
                        tw.WriteLine("\n");
                    }
                }
                if (File.Exists(tiedosto) && String.IsNullOrEmpty(((Form1)f).textBox2.Text))
                {
                    using (TextWriter tw = new StreamWriter(tiedosto, true))
                    {
                        tw.WriteLine("Pisteiden määrä: " + ((Form1)f).textBox1.Text + " " + muuttujat.pistee_eka);
                        tw.WriteLine("\n");
                    }
                }

                if (!String.IsNullOrEmpty(((Form1)f).textBox2.Text))
                {
                    if (!File.Exists(tiedosto))
                    {
                        File.Create(tiedosto).Dispose();

                        using (TextWriter tw = new StreamWriter(tiedosto))
                        {
                            tw.WriteLine("Pisteiden määrä: " + ((Form1)f).textBox1.Text + " " + muuttujat.pistee_eka);
                            tw.WriteLine("Pisteiden määrä: " + ((Form1)f).textBox2.Text + " " + muuttujat.pisteet_toka);
                            tw.WriteLine("\n");
                        }
                    }
                    if (File.Exists(tiedosto))
                    {
                        using (TextWriter tw = new StreamWriter(tiedosto, true))
                        {
                            tw.WriteLine("Pisteiden määrä: " + ((Form1)f).textBox1.Text + " " + muuttujat.pistee_eka);
                            tw.WriteLine("Pisteiden määrä: " + ((Form1)f).textBox2.Text + " " + muuttujat.pisteet_toka);
                            tw.WriteLine("\n");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Voihan Rähmä!");
                MessageBox.Show(ex.ToString());
            }

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
