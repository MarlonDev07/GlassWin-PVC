using Dominio.Model.ClassWindows;
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
    public partial class frmSelectDesing : Form
    {
        public frmSelectDesing()
        {
            InitializeComponent();
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
    }
}
