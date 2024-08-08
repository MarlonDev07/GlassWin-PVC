using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Precentacion.User.AgregarFactura
{
    public partial class frmImageViewer : Form
    {
        public frmImageViewer()
        {
            InitializeComponent();
            // Configura el PictureBox para que estire la imagen para ajustarla al tamaño del control
            pbImageViewer.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        // Propiedad para establecer la imagen
        public Image ImageToDisplay
        {
            set
            {
                pbImageViewer.Image = value;
                // Asegúrate de que la imagen se estire para ajustarse al PictureBox
                pbImageViewer.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
    }
}
