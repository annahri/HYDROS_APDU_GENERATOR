using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYDROS_APDU_GENERATOR
{
    public partial class Form1 : Form
    {
        // Variables Global
        public string[] data = new string[7];
        

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Generate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox7.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0) {
                String lists = "";
                foreach (var item in listBox1.Items) {
                    lists += item + "\r\n";
                }
                Clipboard.SetText(lists);
                toolTip1.Show("Les trames ont été copiés!", button4);
            } else {
                toolTip1.Show("Vous n'avez aucun trames!", button4);
            }
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aProposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.Show();
        }

        private void Generate()
        {
            int i = 0;
            int count = 0;
            List<int> index = new List<int>();
            data[i++] = textBox1.Text;
            data[i++] = textBox2.Text;
            data[i++] = textBox7.Text;
            data[i++] = textBox3.Text;
            data[i++] = textBox4.Text;
            data[i++] = textBox5.Text;
            data[i++] = textBox6.Text;

            for (int j = 0; j < data.Length; j++) {
                if (data[j] == "") continue;
                count++;
            }

            float[] dataf = new float[4];
            if (count == data.Length) {
                if (int.TryParse(data[2], out int num)
                    && float.TryParse(data[3], out dataf[0])
                    && float.TryParse(data[4], out dataf[1])
                    && float.TryParse(data[5], out dataf[2])
                    && float.TryParse(data[6], out dataf[3])) {

                    GetData.NomTotem = Encoding.ASCII.GetBytes(data[0]);
                    GetData.CodeFonction = Convert.ToByte(int.Parse(data[1]));
                    GetData.NumPaquet = GetData.Get2Byte(num);
                    for (int j = 0; j < 4; j++) {
                        byte[] bytearr = GetData.GetByte(dataf[j]);
                        for (int k = 0; k < 4; k++) {
                            GetData.Donnees[j, k] = bytearr[k];
                        }
                    }

                    listBox1.Items.Add(GetData.Output());
                    GetData.Clear();

                } else {
                    MessageBox.Show("Donnée(s) incorrecte(s)\nNB: Valeur decimal utilise (,) non (.)", "Oups");
                }

            } else {
                MessageBox.Show("Aucune boite vide, svp!", "Oh nan");
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Generate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var randIt = new Random();
            var randFl = new Random();
            var idtotem = 'T';
            var numTotem = randIt.Next(100);
            var numero = randIt.Next(500);
            var donnees = new float[4];

            for (int i = 0; i < donnees.Length; i++) {
                if (i == 2) donnees[i] = randFl.Next(100);
                else donnees[i] = NextFloat(randFl);
            }

            textBox1.Text = idtotem.ToString() + (numTotem < 10 ? $"0{numTotem}" : $"{numTotem}");
            textBox7.Text = numero.ToString();
            textBox3.Text = donnees[0].ToString();
            textBox4.Text = donnees[1].ToString();
            textBox5.Text = donnees[2].ToString();
            textBox6.Text = donnees[3].ToString();
        }

        static float NextFloat(Random random)
        {
            double mantissa = (random.NextDouble() * 2.0) - 1.0;
            double exponent = Math.Pow(2.0, random.Next(-126, 128));
            return (float)(mantissa * exponent);
        }
    }
}
