using Dominio.Model.ClassWindows;
using MaterialSkin.Controls;
using Precentacion.User.Quote.Windows.Calculos_de_Precio;
using Precentacion.User.Quote.Windows.Seleccion_Diseño;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Precentacion.User.Quote.Windows
{
    //Comentario prueba
    public partial class frmSelectDesingVentila : MaterialForm
    {
        public frmSelectDesingVentila()
        {
            InitializeComponent();
            SeleccionDesign.loadMaterial(this);
        }

        private void btn1HojaHorizontal_Click(object sender, EventArgs e)
        {
            /*ClsWindows.Desing = "1 Hoja Horizontal";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Show();
            this.Close();*/

            ClsWindows.Desing = "1 Hoja Horizontal";
                frmCalcPriceWindows frm = new frmCalcPriceWindows();
                frm.Design2 = ClsWindows.Desing;
                frm.Show();
                this.Close();
            

        }

        private void btn2HojaHorizontal_Click(object sender, EventArgs e)
        {
            ClsWindows.Desing = "2 Hoja Horizontal";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Design2 = ClsWindows.Desing;
            frm.Show();
            this.Close();
        }

        private void btn3HojaHorizontal_Click(object sender, EventArgs e)
        {
            ClsWindows.Desing = "3 Hoja Horizontal";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Design2 = ClsWindows.Desing;
            frm.Show();
            this.Close();
        }

        private void btn4HojaHorizontal_Click(object sender, EventArgs e)
        {
            ClsWindows.Desing = "4 Hoja Horizontal";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Design2 = ClsWindows.Desing;
            frm.Show();
            this.Close();
        }

        private void btn5HojaHorizontal_Click(object sender, EventArgs e)
        {
            ClsWindows.Desing = "6 Hoja Horizontal";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Design2 = ClsWindows.Desing;
            frm.Show();
            this.Close();
        }

        private void btn6HojaHorizontal_Click(object sender, EventArgs e)
        {
            ClsWindows.Desing = "5 Hoja Horizontal";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Design2 = ClsWindows.Desing;
            frm.Show();
            this.Close();
        }

        private void btnVentila1Fijo_Click(object sender, EventArgs e)
        {
            ClsWindows.Desing = "1 Hoja Horizontal 1Fijo";
            frmCalcPriceVentila frm = new frmCalcPriceVentila();
            frm.Show();
            this.Close();
        }

        private void btnVT1Euro_Click(object sender, EventArgs e)
        {
            ClsWindows.System = "Ventila Euro";
            ClsWindows.Desing = "1 Hoja Horizontal";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.System2 = ClsWindows.System;
            frm.Design2 = ClsWindows.Desing;
            frm.Show();
            this.Close();
        }

        private void btnVT2Euro_Click(object sender, EventArgs e)
        {
            ClsWindows.System = "Ventila Euro";
            ClsWindows.Desing = "2 Hoja Horizontal";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.System2 = ClsWindows.System;
            frm.Design2 = ClsWindows.Desing;
            frm.Show();
            this.Close();
        }

        private void btnVT3Euro_Click(object sender, EventArgs e)
        {
            ClsWindows.System = "Ventila Euro";
            ClsWindows.Desing = "3 Hoja Horizontal";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.System2 = ClsWindows.System;
            frm.Design2 = ClsWindows.Desing;
            frm.Show();
            this.Close();
        }

        private void btnVT4Euro_Click(object sender, EventArgs e)
        {
            ClsWindows.System = "Ventila Euro";
            ClsWindows.Desing = "4 Hoja Horizontal";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.System2 = ClsWindows.System;
            frm.Design2 = ClsWindows.Desing;
            frm.Show();
            this.Close();
        }

        private void btnVT5Euro_Click(object sender, EventArgs e)
        {
            ClsWindows.System = "Ventila Euro";
            ClsWindows.Desing = "5 Hoja Horizontal";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.System2 = ClsWindows.System;
            frm.Design2 = ClsWindows.Desing;
            frm.Show();
            this.Close();
        }

        private void btnVT6Euro_Click(object sender, EventArgs e)
        {
            ClsWindows.System = "Ventila Euro";
            ClsWindows.Desing = "6 Hoja Horizontal";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.System2 = ClsWindows.System;
            frm.Design2 = ClsWindows.Desing;
            frm.Show();
            this.Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClsWindows.Desing = "2 Hoja Vertical";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Design2 = ClsWindows.Desing;
            frm.Show();
            this.Close();
        }

        private void btn1Hoja_Click(object sender, EventArgs e)
        {
            ClsWindows.Desing = "1 Hoja 1 Fijo Vertical";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Design2 = ClsWindows.Desing;
            frm.Show();
            this.Close();
        }

        private void btn3HojasVertical_Click(object sender, EventArgs e)
        {
            ClsWindows.Desing = "3 Hoja Vertical";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Design2 = ClsWindows.Desing;
            frm.Show();
            this.Close();
        }

        private void frmSelectDesingVentila_Load(object sender, EventArgs e)
        {
            // Ruta de la imagen
            string imagePath = Application.StartupPath + "\\Images\\SelectionDesigns\\FIJO VENTILA X.jpeg";

            // Verifica que el archivo de imagen exista
            if (System.IO.File.Exists(imagePath))
            {
                // Carga la imagen y asígnala al botón
                btnFijoVentila1.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Images\\SelectionDesigns\\FIJO VENTILA X.jpeg");
            }
            else
            {
                MessageBox.Show("La imagen no se encuentra en la ruta especificada.");
            }
        }

        private void btnBackSistema_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmSelectSystem frm = new frmSelectSystem();
            frm.Show();
            this.Close();
        }

        private void btnFijoVentila1_Click(object sender, EventArgs e)
        {
            ClsWindows.Desing = "Ventila1Fijo";
            frmCalcPriceVentila frm = new frmCalcPriceVentila();
            frm.Show();
            this.Close();
        }

        private void frmSelectDesingVentila_FormClosing(object sender, FormClosingEventArgs e)
        {
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClsWindows.Desing = "VerticalFijoMovilFijo";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Design2 = ClsWindows.Desing;
            frm.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ClsWindows.Desing = "VentilaEuroEspecialV1";
            frmCalcPriceVentila frm = new frmCalcPriceVentila();
            frm.Show();
            this.Close();
        }
    }
}
