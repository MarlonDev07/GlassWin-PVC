using Dominio.Model.ClassWindows;
using Dominio.Model.PuertaBaño;
using MaterialSkin.Controls;
using Precentacion.User.Quote.Quote;
using Precentacion.User.Quote.SandBlasting;
using Precentacion.User.Quote.Windows.Seleccion_Diseño;
using Precentacion.User.Quote.Windows.Seleccion_Sistema;
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
    public partial class frmSelectSystem : MaterialForm
    {
        public frmSelectSystem()
        {
            InitializeComponent();
            SeleccionUI.loadMaterial(this);
        }

        #region Buttons
        private void btn5020_Click(object sender, EventArgs e)
        {
            ClsWindows.System = "5020";
            frmSelectDesing frm = new frmSelectDesing();
            frm.Show();
            this.Close();
        }

        private void btnVentila_Click(object sender, EventArgs e)
        {
            ClsWindows.System = "Ventila";
            frmSelectDesingVentila frm = new frmSelectDesingVentila();
            frm.Show();
            this.Close();

        }

        private void btn8025_2_Vias_Click(object sender, EventArgs e)
        {

            ClsWindows.System = "8025 2 Vias";
            frmSelectDesing frm = new frmSelectDesing();
            frm.Show();
            this.Close();
        }

        private void btn8025_3_Vias_Click(object sender, EventArgs e)
        {
            ClsWindows.System = "8025 3 Vias";
            frmSelectDesing frm = new frmSelectDesing();
            frm.Show();
            this.Close();
        }

        private void btn8040_2_Vias_Click(object sender, EventArgs e)
        {
            ClsWindows.System = "8040 2 Vias";
            frmSelectDesing frm = new frmSelectDesing();
            frm.Show();
            this.Close();
        }

        private void btn8040_3_Vias_Click(object sender, EventArgs e)
        {
            ClsWindows.System = "8040 3 Vias";
            frmSelectDesing frm = new frmSelectDesing();
            frm.Show();
            this.Close();
        }

        private void btn6030_2_Vias_Click(object sender, EventArgs e)
        {
            ClsWindows.System = "6030 2 Vias";
            frmSelectDesing frm = new frmSelectDesing();
            frm.Show();
            this.Close();
        }

        private void btn6030_3_Vias_Click(object sender, EventArgs e)
        {
            ClsWindows.System = "6030 3 Vias";
            frmSelectDesing frm = new frmSelectDesing();
            frm.Show();
            this.Close();
        }

        private void btnEuro_2_Vias_Click(object sender, EventArgs e)
        {
            ClsWindows.System = "Europa 2 Vias";
            frmSelectDesing frm = new frmSelectDesing();
            frm.Show();
            this.Close();
        }

        private void btnEuro_3_Vias_Click(object sender, EventArgs e)
        {
            ClsWindows.System = "Europa 3 Vias";
            frmSelectDesing frm = new frmSelectDesing();
            frm.Show();
            this.Close();
        }

        private void btnVitroStudio_Click(object sender, EventArgs e)
        {
            frmSelectSystemSB frm = new frmSelectSystemSB();
            frm.Show();
            this.Close();
        }
        private void btnVidrioFijo_Click(object sender, EventArgs e)
        {
            ClsWindows.System = "Vidrio Fijo";
            frmSelecDesingVentanaFija frm = new frmSelecDesingVentanaFija();
            frm.Show();
            this.Close();
           
        }
        private void btnEUFijo_Click(object sender, EventArgs e)
        {
            ClsWindows.System = "EuAbatible";
            frmSelecDesingVentanaFija frm = new frmSelecDesingVentanaFija();
            frm.Show();
            this.Close();
        }
        private void btnEuPuerta2Vias_Click(object sender, EventArgs e)
        {
            ClsWindows.System = "Europa 2 Vias Puerta";
            frmSelectDesing frm = new frmSelectDesing();
            frm.Show();
            this.Close();
        }

        private void btnEuPuerta3Vias_Click(object sender, EventArgs e)
        {
            ClsWindows.System = "Europa 3 Vias Puerta";
            frmSelectDesing frm = new frmSelectDesing();
            frm.Show();
            this.Close();
        }
        private void btnPuertaLujo_Click(object sender, EventArgs e)
        {
            ClsWindows.System = "Puerta Lujo";
            frmSelecDesingPuertLujo frm = new frmSelecDesingPuertLujo();
            frm.Show();
            this.Close();
        }

        private void btnPuertaBaño_Click(object sender, EventArgs e)
        {
            clsPuertaBaño.System = "Puerta Baño";
            frmSelectDiseñoPuertaBaño frm = new frmSelectDiseñoPuertaBaño();
            frm.Show();
            this.Close();
        }

        private void btnPuertaEuAbatible_Click(object sender, EventArgs e)
        {
            ClsWindows.System = "PuertaEuAbatible";
            frmSelecDesingPuertLujo frm = new frmSelecDesingPuertLujo();
            frm.Show();
            this.Close();

        }

        private void btnCedazoAkari_Click(object sender, EventArgs e)
        {
            ClsWindows.System = "CedazoAkari";
            frmSelecDesingCedazo frm = new frmSelecDesingCedazo();
            frm.Show();
            this.Close();
        }
        private void btnPuertaLiviana_Click(object sender, EventArgs e)
        {
            ClsWindows.System = "Puerta Liviana";
            frmSelecDesingPuertLujo frm = new frmSelecDesingPuertLujo();
            frm.Show();
            this.Close();
        }
        private void btnBackSistema_Click(object sender, EventArgs e)
        {
            this.Close();
        }





        #endregion

        private void btn50203Vias_Click(object sender, EventArgs e)
        {
            ClsWindows.System = "5020 3 Vias";
            frmSelectDesing frm = new frmSelectDesing();
            frm.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClsWindows.System = "Cedazo 1/2";
            ClsWindows.Desing = "Cedazo 12";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Design2 = ClsWindows.Desing = "Cedazo 12";
            frm.System2 = ClsWindows.System = "Cedazo 1/2";
            frm.Show();
            this.Close();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            ClsWindows.System = "Cedazo 1/2";
            ClsWindows.Desing = "Cedazo 1";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Show();
            this.Close();
        }

        private void btnCedazo2_Click(object sender, EventArgs e)
        {
            ClsWindows.System = "Cedazo 1/2";
            ClsWindows.Desing = "Cedazo 2";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Show();
            this.Close();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblVtNormal_Click(object sender, EventArgs e)
        {

        }

        private void PanelContenedorSistemaTradicional_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
