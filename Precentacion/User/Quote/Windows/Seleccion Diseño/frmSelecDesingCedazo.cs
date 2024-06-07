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
using MaterialSkin.Controls;

namespace Precentacion.User.Quote.Windows.Seleccion_Diseño
{
    public partial class frmSelecDesingCedazo : MaterialForm
    {
        public frmSelecDesingCedazo()
        {
            InitializeComponent();
            SeleccionDesign.loadMaterial(this);
        }

        private void btnFijoMovi_Click(object sender, EventArgs e)
        {
            ClsWindows.Desing = "CedazoAkariFijoMovil";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Show();
            this.Close();
        }

        private void fijoMovilMovilFijo_Click(object sender, EventArgs e)
        {
            ClsWindows.Desing = "CedazoAkariFijoMovilMovilFijo";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Show();
            this.Close();
        }

        private void btnBackSistema_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
