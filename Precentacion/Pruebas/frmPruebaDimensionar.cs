using System;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;

namespace Precentacion.Pruebas
{
    public partial class frmPruebaDimensionar : Form
    {
        // Relación de escala (1 metro = 1000 píxeles, 1 centímetro = 100 píxeles)
        private const decimal MetrosAPixeles = 1000.0m;
        private const decimal CentimetrosAPixeles = 100.0m;

        public frmPruebaDimensionar()
        {
            InitializeComponent();
            pictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                try
                {

                    // Convertir las dimensiones ingresadas por el usuario a píxeles
                    decimal anchoEnMetros = decimal.Parse(txtAncho.Text);
                    decimal alturaEnMetros = decimal.Parse(txtAlto.Text);

                    int newWidth = (int)(anchoEnMetros * MetrosAPixeles);
                    int newHeight = (int)(alturaEnMetros * MetrosAPixeles);
                    //Redirecciona a la funcion
                    var resizedImage = ResizeImage(pictureBox.Image, newWidth, newHeight);
                    //La imagen que devuelve la funcion va a ser la nueva imagen del pictureBox
                    pictureBox.Image = resizedImage;
                }
                catch (FormatException)
                {
                    MessageBox.Show("Por favor, introduce valores válidos para el ancho y el alto.");
                }
            }
            else
            {
                MessageBox.Show("No hay ninguna imagen cargada en el PictureBox.");
            }
        }

        private Bitmap ResizeImage(Image image, int width, int height)
        {
            // Rectángulo de destino para la imagen redimensionada
            var destRect = new Rectangle(0, 0, width, height);
            // Crear un nuevo objeto Bitmap para la imagen redimensionada
            var destImage = new Bitmap(width, height);

            // Establecer la resolución del nuevo Bitmap igual a la resolución de la imagen original
            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            // Crear un objeto Graphics para la imagen redimensionada
            using (var graphics = Graphics.FromImage(destImage))
            {
                // Configurar la calidad de composición, interpolación, suavizado y compensación de píxeles para el objeto Graphics
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                // Configurar el modo de envoltura de imagen para el objeto Graphics
                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    // Dibujar la imagen original redimensionada en el rectángulo de destino utilizando el objeto Graphics
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            // Devolver la imagen redimensionada
            return destImage;
        }

        private void frmPruebaDimensionar_Load(object sender, EventArgs e)
        {

        }
    }
}

/*
Bitmap es una clase en .NET que representa una imagen de mapa de bits, es decir, una imagen formada por una
cuadrícula de píxeles. Cada píxel en un mapa de bits contiene información sobre su color. En el contexto de 
C# y .NET, Bitmap se utiliza comúnmente para manipular imágenes a nivel de píxel, como cargar imágenes desde
archivos, crear imágenes nuevas, dibujar en imágenes, etc.
*/
