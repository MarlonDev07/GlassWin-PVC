using Dominio.Model.ClassWindows;
using MaterialSkin.Controls;
using Precentacion.User.Quote.Windows.Calculos_de_Precio;
using Precentacion.User.Quote.Windows.Seleccion_Diseño;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Precentacion.User.Quote.Windows
{
    public partial class frmSelectDesing : MaterialForm
    {
        public string system2 { get; set; }
        public frmSelectDesing()
        {
            InitializeComponent();
            SeleccionDesign.loadMaterial(this);
        }

        private void btnFijoMovi_Click(object sender, EventArgs e)
        {
            ClsWindows.Desing = "FijoMovil";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Show();
            this.Close();
        }

        private void btnBackSistema_Click(object sender, EventArgs e)
        {
            frmSelectSystem frm = new frmSelectSystem();
            frm.Show();
            this.Close();
        }

        private void btnMovilMovil_Click(object sender, EventArgs e)
        {
            ClsWindows.Desing = "MovilMovil";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Show();
            this.Close();
        }

        private void btnFijoMovilFijo_Click(object sender, EventArgs e)
        {
            ClsWindows.Desing = "FijoMovilFijo";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Show();
            this.Close();
        }

        private void MovilFijoMovil_Click(object sender, EventArgs e)
        {
            ClsWindows.Desing = "MovilFijoMovil";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Show();
            this.Close();
        }

        private void fijoMovilMovilFijo_Click(object sender, EventArgs e)
        {
            ClsWindows.Desing = "FijoMovilMovilFijo";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Show();
            this.Close();
        }

        private void FijoMovilMovil_Click(object sender, EventArgs e)
        {

            ClsWindows.Desing = "FijoMovilMovil";
           
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Show();
            this.Close();
        }

        private void FijoMovilMovilMovilMovilFijo_Click(object sender, EventArgs e)
        {
            ClsWindows.Desing = "FijoMovilMovilMovilMovilFijo";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClsWindows.Desing = "FijoMovilMovil";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Show();
            this.Close();   
        }

        private void btnFijoMovi_Click_1(object sender, EventArgs e)
        {
            
            ClsWindows.Desing = "FijoMovil";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Show();
            this.Close();

        }

        private void btnMovilMovil_Click_1(object sender, EventArgs e)
        {
            ClsWindows.Desing = "MovilMovil";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Show();
            this.Close();
        }

        private void btnFijoMovilFijo_Click_1(object sender, EventArgs e)
        {
            ClsWindows.Desing = "FijoMovilFijo";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Show();
            this.Close();
        }

        private void MovilFijoMovil_Click_1(object sender, EventArgs e)
        {
            ClsWindows.Desing = "MovilFijoMovil";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Show();
            this.Close();
        }

        private void FijoMovilMovil_Click_1(object sender, EventArgs e)
        {
            ClsWindows.Desing = "FijoMovilMovil";

            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Show();
            this.Close();
        }

        private void fijoMovilMovilFijo_Click_1(object sender, EventArgs e)
        {
            ClsWindows.Desing = "FijoMovilMovilFijo";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Show();
            this.Close();
        }

        private void FijoMovilMovilMovilMovilFijo_Click_1(object sender, EventArgs e)
        {
            ClsWindows.Desing = "FijoMovilMovilMovilMovilFijo";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Show();
            this.Close();
        }

        private void btnBackSistema_Click_1(object sender, EventArgs e)
        {
            frmSelectSystem frmSelectSystem = new frmSelectSystem();
            frmSelectSystem.Show();
            this.Close();
        }

        private void frmSelectDesing_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmSelectSystem frmSelectSystem = new frmSelectSystem();
            frmSelectSystem.Show();
            this.Close();
        }

        private void frmSelectDesing_Load(object sender, EventArgs e)
        {
            if (system2 == "5020" || system2 == "5020 3 Vias" || system2 == "8025 2 Vias"  || system2 == "Europa 2 Vias" || system2 == "8040 2 Vias")
            {
                FijoMovilMovilMovilMovilFijo.Visible = false;
                FijoMovilMovil.Visible = false;
            }
            else if (system2 == "6030 2 Vias") {
                FijoMovilMovilMovilMovilFijo.Visible = true;
                FijoMovilMovil.Visible = true;
            }
            else if (system2 == "6030 3 Vias")
            {
                FijoMovilMovilMovilMovilFijo.Visible = true;
                FijoMovilMovil.Visible = true;
            }
            // Ruta de la imagen
            string imagePath = Application.StartupPath + "\\Images\\SelectionDesigns\\FIJO VENTILA X.jpeg";

            // Verifica que el archivo de imagen exista
            if (System.IO.File.Exists(imagePath))
            {
                // Carga la imagen y asígnala al botón
                btnFijoMMM.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Images\\SelectionDesigns\\8040FijoMovilMovilMovil.jpeg");
                btn2ViasMMM.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Images\\SelectionDesigns\\SC MMM NATURAL.jpeg");
            }
            else
            {
                MessageBox.Show("La imagen no se encuentra en la ruta especificada.");
            }
        }

        private void btnFijoMMM_Click(object sender, EventArgs e)
        {
            ClsWindows.Desing = "FijoMovilMovilMovil";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Show();
            this.Close();
        }

        private void frmSelectDesing_FormClosing_1(object sender, FormClosingEventArgs e)
        {
           
        }

        private void btn2ViasMMM_Click(object sender, EventArgs e)
        {
            if (ClsWindows.System == "8025 3 Vias")
            {
                ClsWindows.Desing = "3ViasMovilMovilMovil";
            }
            else 
            {
                ClsWindows.Desing = "2ViasMovilMovilMovil";
            }
          
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Show();
            this.Close();
        }
    }
}
