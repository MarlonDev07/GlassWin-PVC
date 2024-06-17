using Dominio.Model.ClassWindows;
using MaterialSkin.Controls;
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
    }
}
