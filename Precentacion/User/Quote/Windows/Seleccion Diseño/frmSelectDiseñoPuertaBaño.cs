using Dominio.Model.ClassWindows;
using Dominio.Model.PuertaBaño;
using Precentacion.User.Quote.Windows.Calculos_de_Precio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Precentacion.User.Quote.Windows.Seleccion_Diseño
{
    public partial class frmSelectDiseñoPuertaBaño : MaterialSkin.Controls.MaterialForm
    {
        public frmSelectDiseñoPuertaBaño()
        {
            InitializeComponent();
        }

        private void btnMovilMovil_Click(object sender, EventArgs e)
        {
            //PB MovilMovil
            clsPuertaBaño.Desing = "PB MovilMovil";//MovilMovil
            frmCalcPuertaBaño frm = new frmCalcPuertaBaño();
            frm.design2 = clsPuertaBaño.Desing;
            frm.Show();
            this.Close();
        }

        private void FijoMovilMovil_Click(object sender, EventArgs e)
        {
            //PB FijoMovilMoviL
            clsPuertaBaño.Desing = "PB FijoMovilMovil";//FijoMovilMovil
            frmCalcPuertaBaño frm = new frmCalcPuertaBaño();
            frm.design2 = clsPuertaBaño.Desing;
            frm.Show();
            this.Close();
        }

        private void btnBackSistema_Click(object sender, EventArgs e)
        {
            frmSelectSystem frm = new frmSelectSystem();
            frm.Show();
            this.Close();
        }

        private void frmSelectDiseñoPuertaBaño_FormClosing(object sender, FormClosingEventArgs e)
        {
         
        }
    }
}
