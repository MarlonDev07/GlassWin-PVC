using Precentacion.User.Quote.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Precentacion.User.Quote.SandBlasting
{
    public partial class frmSelectSystemSB : Form
    {
        public frmSelectSystemSB()
        {
            InitializeComponent();
        }

        private void btnBackSistema_Click(object sender, EventArgs e)
        {
            frmSelectSystem frm = new frmSelectSystem();
            frm.Show();
            this.Close();
        }

        private void btnArenadoCoDiseño_Click(object sender, EventArgs e)
        {
            clsSandBlasting.System = "Arenado con Diseño";
            frmSelectDesingSB frm = new frmSelectDesingSB();
            frm.Show();
            this.Close();
        }

        private void btnArenadoLiso_Click(object sender, EventArgs e)
        {
            clsSandBlasting.System = "Arenado Liso";
            frmCalcPriceSandBlasting frm = new frmCalcPriceSandBlasting();
            frm.Show();
            this.Close();
        }

        private void btnArenado_Click(object sender, EventArgs e)
        {
            clsSandBlasting.System = "Arenado";
            frmCalcPriceSandBlasting frm = new frmCalcPriceSandBlasting();
            frm.Show();
            this.Close();
        }
    }
}
