using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        private Bitmap? originalImage;
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image files (*.jpg; *.png)|*.jpg;*.png";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                originalImage?.Dispose();
                originalImage = new Bitmap(ofd.FileName);
                pictureBox1.Image = originalImage;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (originalImage == null)
            {
                MessageBox.Show("Najpierw za³aduj obraz.");
                return;
            }

            Bitmap img1 = new Bitmap(originalImage);
            Bitmap img2 = new Bitmap(originalImage);
            Bitmap img3 = new Bitmap(originalImage);
            Bitmap img4 = new Bitmap(originalImage);

            Bitmap? grayscale = null;
            Bitmap? negative = null;
            Bitmap? edge = null;
            Bitmap? mirror = null;

            Parallel.Invoke(
                () => { grayscale = ImageProcessor.Grayscale(img1); },
                () => { negative = ImageProcessor.Negative(img2); },
                () => { edge = ImageProcessor.EdgeDetection(img3); },
                () => { mirror = ImageProcessor.Mirror(img4); }
            );

            // Zwolnij pamiêæ po poprzednich obrazach
            pictureBox2.Image?.Dispose();
            pictureBox3.Image?.Dispose();
            pictureBox4.Image?.Dispose();
            pictureBox5.Image?.Dispose();

            // Ustaw nowe obrazy
            pictureBox2.Image = grayscale;
            pictureBox3.Image = negative;
            pictureBox4.Image = edge;
            pictureBox5.Image = mirror;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }
    }
}

