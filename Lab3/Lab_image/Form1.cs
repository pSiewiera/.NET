using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_image
{
    public partial class Form1 : Form
    {
        private Bitmap? _originalImage;
        private PictureBox pictureBoxMain;
        private PictureBox pbNegative;
        private PictureBox pbGray;
        private PictureBox pbThreshold;
        private PictureBox pbMirror;
        private Button btnLoad;
        private Button btnProcess;

        public Form1()
        {
            InitializeComponent();
            InitializeMyComponents(); 
        }

        private void InitializeMyComponents()
        {
            this.Size = new Size(1000, 700);
            this.Text = "Laboratorium 3";

            btnLoad = new Button { Text = "Wczytaj pPNG", Location = new Point(10, 10), Size = new Size(100, 30) };
            btnLoad.Click += btnLoad_Click;

            btnProcess = new Button { Text = "Nałóż filtry", Location = new Point(120, 10), Size = new Size(100, 30) };
            btnProcess.Click += btnProcess_Click;

            pictureBoxMain = new PictureBox { Location = new Point(10, 50), Size = new Size(300, 250), SizeMode = PictureBoxSizeMode.Zoom, BorderStyle = BorderStyle.FixedSingle };
            pbNegative = new PictureBox { Location = new Point(330, 50), Size = new Size(300, 250), SizeMode = PictureBoxSizeMode.Zoom, BorderStyle = BorderStyle.FixedSingle };
            pbGray = new PictureBox { Location = new Point(650, 50), Size = new Size(300, 250), SizeMode = PictureBoxSizeMode.Zoom, BorderStyle = BorderStyle.FixedSingle };
            pbThreshold = new PictureBox { Location = new Point(330, 320), Size = new Size(300, 250), SizeMode = PictureBoxSizeMode.Zoom, BorderStyle = BorderStyle.FixedSingle };
            pbMirror = new PictureBox { Location = new Point(650, 320), Size = new Size(300, 250), SizeMode = PictureBoxSizeMode.Zoom, BorderStyle = BorderStyle.FixedSingle };

            this.Controls.Add(btnLoad);
            this.Controls.Add(btnProcess);
            this.Controls.Add(pictureBoxMain);
            this.Controls.Add(pbNegative);
            this.Controls.Add(pbGray);
            this.Controls.Add(pbThreshold);
            this.Controls.Add(pbMirror);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _originalImage = new Bitmap(openFileDialog.FileName);
                    pictureBoxMain.Image = _originalImage;
                }
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            if (_originalImage == null)
            {
                MessageBox.Show("musisz wczytac najpierw obrazek");
                return;
            }
            ApplyFiltersParallel(_originalImage);
        }

        private void ApplyFiltersParallel(Bitmap source)
        {
            Task.Run(() =>
            {
                Parallel.Invoke(
                    () => ProcessFilter(source, ApplyNegative, pbNegative),
                    () => ProcessFilter(source, ApplyGrayscale, pbGray),
                    () => ProcessFilter(source, ApplyThreshold, pbThreshold),
                    () => ProcessFilter(source, ApplyMirror, pbMirror)
                );
            });
        }

        private void ProcessFilter(Bitmap source, Func<Bitmap, Bitmap> filterMethod, PictureBox targetPb)
        {
            Bitmap copy = (Bitmap)source.Clone();
            Bitmap result = filterMethod(copy);

            targetPb.Invoke(new Action(() => {
                targetPb.Image = result;
            }));
        }


        private Bitmap ApplyNegative(Bitmap bmp)
        {
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    bmp.SetPixel(x, y, Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B));
                }
            }
            return bmp;
        }

        private Bitmap ApplyGrayscale(Bitmap bmp)
        {
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    int gray = (int)(c.R * 0.3 + c.G * 0.59 + c.B * 0.11);
                    bmp.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                }
            }
            return bmp;
        }

        private Bitmap ApplyThreshold(Bitmap bmp)
        {
            int threshold = 128;
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    int avg = (c.R + c.G + c.B) / 3;
                    Color newColor = avg >= threshold ? Color.White : Color.Black;
                    bmp.SetPixel(x, y, newColor);
                }
            }
            return bmp;
        }

        private Bitmap ApplyMirror(Bitmap bmp)
        {
            Bitmap result = new Bitmap(bmp.Width, bmp.Height);
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    result.SetPixel(bmp.Width - 1 - x, y, c);
                }
            }
            return result;
        }
    }
}