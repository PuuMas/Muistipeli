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
    public partial class peli1 : Form
    {
        //Esitellään yhteisiä muuttujia mitä voidaan käyttää
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

        //Kun form ladataan kutsuu se PelinAloitus mehtodia
        public peli1()

        {
            InitializeComponent();
            PelinAloitus();
        }
        //Tämä palauttaa kuvat listaan
        private PictureBox[] KuvaListat
        {
            get { return Controls.OfType<PictureBox>().ToArray(); }
        }

        //Käytetään IEnumerable luokkaa jotta voidaan käydä kuvat läpi kätevästi foreachilla
        private static IEnumerable<Image> Kuvat
        {
            get
            {
               /*Kuvat on laitettu projektin resources luokkaan
                Kuvia on noin 262kpl ja sieltä on vaan valittu summassa 4
                Huom. Jotta ohjelma ei joutuisi ikuiseen looppiin myöhemmin pitää kuvia olla aina
                puolet vähemmän kuin mitä pelissä on "picturebox" elementtejä.
                Siis tässä tapauksessa 4 koska tämä on 8 palan peli alusta.*/

                return new Image[]
                {
                    Properties.Resources.finland,
                    Properties.Resources.france,
                    Properties.Resources.united_states_of_america,
                    Properties.Resources.liberia
                };
            }
        }

        //Tämä funktio resetoi kuvat
        //Ja antaa kuvan tagille arvon null
        //Kutsuu methodeja ja aloittaa ajastimen
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
        //Ja asettaa kaikki kuvat listassa kääntöpuoleksi
        private void PiilotaKuvat()
        {
            foreach (var kuva in KuvaListat)
            {
                kuva.Image = Properties.Resources.cadrback;
            }
        }

        //Tämä funktio valitsee vapaat kuvat
        //Numero muuttuja toimii valitsijana
        //Random ottaa numeron väliltä 0-kuvalistan koko(tässä tapauksessa 4)
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

        //Valitsee kaksi paria kuvista
        private void SekoitaKuvat()
        {
            foreach (var kuva in Kuvat)
            {
                
                VapaaVali().Tag = kuva;
                VapaaVali().Tag = kuva;
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }
        //Kun ikkuna suljetaan lopettaa ohjelman.
        private void peli1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        //Tämä resetoi kuvat ja nollaa piste laskurin.
        private void resetoiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            muuttujat.pistee_eka = 0;
            muuttujat.pisteet_toka = 0;
            ResetoiKuvat();
        }
        //Tässä on kuvan klikkaus toiminto
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

            //Jos vuoro on false palautuu
            if (!vuoro) return;

            //Esitellään muuttuja joka katsoo mitä kuvaa klikattiin.
            var kuva = (PictureBox)sender;

            //Jos eka arvaus on null tai tyhjä
            //Niin kuva muuttuja saa klikatun kuvan arvot ja palaa
            if (ekaArvaus == null)
            {
                ekaArvaus = kuva;
                kuva.Image = (Image)kuva.Tag;
                return;
            }
            //Tässä annetaan kuvalle tag uudestaan
            kuva.Image = (Image)kuva.Tag;

            /*Jos arvaus oli oikein piilotetaan kuvat ja lisätään piste ensinmäiselle pelaajalle
             */
            try
            {
                if (kuva.Image == ekaArvaus.Image && kuva != ekaArvaus && muuttujat.eka_pelaaja == 0)
                {
                    kuva.Visible = ekaArvaus.Visible = false;
                    {
                        ekaArvaus = kuva;
                        muuttujat.pistee_eka++;
                    }
                    PiilotaKuvat();
                }
                /*Jos arvaus oli oikea ja oli pelaajan 2 vuoro lisätään piste hänelle
                 */
               else if (kuva.Image == ekaArvaus.Image && kuva != ekaArvaus && muuttujat.toka_pelaaja == 1)
                {
                    kuva.Visible = ekaArvaus.Visible = false;
                    {
                        ekaArvaus = kuva;
                        muuttujat.pisteet_toka++;
                    }
                    PiilotaKuvat();
                }



                //Jos arvaus oli väärin vuoro vaihtuu ja ajastin käynnistyy uuudestaan
                else
                    {
                        /*Jos arvaus oli väärin ja oli ensimäisen pelaajan vuoro annetaan ilmoitus että
                         * vuoro vaihtuu ja lisätään vuoron vaihtumnis numero.
                         */
                        if (!String.IsNullOrEmpty(((Form1)f).textBox2.Text) && muuttujat.eka_pelaaja == 0)
                        {
                        MessageBox.Show(((Form1)f).textBox2.Text + "n vuoro!");
                            vuoro = false;
                            klikkiAjastin.Start();
                            muuttujat.eka_pelaaja++;
                            muuttujat.toka_pelaaja++;
                        }
                        /*Jos arvaus oli väärin ja oli toisen pelaajan vuoro annetaan ilmoitus että
                         * vuoro vaihtuu ja vähennetään vuoron vaihtumis numero.
                         */
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
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            ekaArvaus = null;

            //Tämä looppi katsoo että onko pelilaustalla vielä näkyviä picturebox elementtejä ja jos on niin jatketaan peliä
            //Jos pelialustalla ei ole enään elementtejä kone antaa Messageboxin sanomalla ja peli päättyy.
            if (KuvaListat.Any(p => p.Visible)) return;

            //Sekava IF lausekkeiden hässäkkä missä vertaillaan pisteitä ja niiden perusteella näytetään kumpi voitto pelin.
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

           //MessageBox.Show("Peli päättyi!" + "\n" + "Sait " + oikein + " paria oikein!");

            //Tässä kirjoitetaan tiedostoon saadut pisteet!
            string tiedosto = @"c:\temp\Tiedosto.txt";
            try
            {
                //Jos tiedostoa ei ole ja Form1 tekstikenttä on tyhjä tehdään tiedosto ja kirjoitetaan tiedot.
                if (!File.Exists(tiedosto) && String.IsNullOrEmpty(((Form1)f).textBox2.Text))
                {
                    File.Create(tiedosto).Dispose();

                    using (TextWriter tw = new StreamWriter(tiedosto))
                    {
                        tw.WriteLine("Pisteiden määrä: " + ((Form1)f).textBox1.Text + " " + muuttujat.pistee_eka);
                        tw.WriteLine("\n");
                    }
                }
                //Jos tiedosto on olemassa ja Form1 tekstikenttä on tyhjä tehdään tiedosto ja kirjoitetaan tiedot.
                if (File.Exists(tiedosto) && String.IsNullOrEmpty(((Form1)f).textBox2.Text))
                {
                    using (TextWriter tw = new StreamWriter(tiedosto, true))
                    {
                        tw.WriteLine("Pisteiden määrä: " + ((Form1)f).textBox1.Text + " " + muuttujat.pistee_eka);
                        tw.WriteLine("\n");
                    }
                }
                //Jos tiedostoa ei ole ja Form1 kenttään on annettu nimi kirjoitetaan tiedosto ja tiedot.
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
                    //Jos tiedosto on ja Form1 kenttään on annettu nimi kirjoitetaan tiedot tiedostoon
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //Menun yksinpeli painike palaa pelin alkuun.
        private void yksinpeliToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)Application.OpenForms["Form1"];
            if(form1 != null)
            {
                form1.Show();
            }
            this.Dispose();
            
        }

        //Menun kaksinpeli painike palaa pelin alkuun.
        //Ja näyttää kaksin peli kentät.
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
        //Tässä on pelin aloitus funkio
        //Annetaan vuoro,Kutsutaan resetoikuvat funktiota
        //Piilotakuvat funktio kutsu ja pelikello funktio kutsu
        //Annetaan kellolle 1sec aika.
        private void PelinAloitus()
        {
            vuoro = true;
            ResetoiKuvat();
            PiilotaKuvat();
            pelikello();
            //Tämä aika on se aika jonka kortit ovat näkyvissä ennenkuin ne piilotetaan.
            klikkiAjastin.Interval = 1000;
            klikkiAjastin.Tick += ajastin;

        }
        //Ajastin joka kutsuu piilotakuvat funktiota ja asettaa vuoron.
        private void ajastin(object sender,EventArgs e)
        {
            PiilotaKuvat();
            vuoro = true;
            klikkiAjastin.Stop();
            

        }
        //Pelikello funktio alottaa peli ajan.
        private void pelikello()
        {
            aika.Start();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about about = new about();
            about.Show();
        }

        private void tilastotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            read tilastot = new read();
            tilastot.Show();
        }

    }
}
