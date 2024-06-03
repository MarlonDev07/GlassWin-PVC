using Dominio.Model.ClassWindows;
using Precentacion.User.Quote.Quote;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Precentacion.User.Quote.Accesorios
{
    public partial class frmMedidasVidrio : MaterialSkin.Controls.MaterialForm
    {
        public decimal Precio;
        public frmMedidasVidrio()
        {
            InitializeComponent();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            ClsWindows.Weight = Convert.ToDecimal(txtAncho.Text);
            ClsWindows.heigt = Convert.ToDecimal(txtAlto.Text);

            //Calcular el Precio del Vidrio
            Precio = ClsWindows.Weight * ClsWindows.heigt * Precio;

            decimal PrecioTotal = Precio * CantidadNum.Value;


            Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is frmListaAcesorios);
            if (frm != null)
            {
                ((frmListaAcesorios)frm).PrecioVidrio = PrecioTotal;
                ((frmListaAcesorios)frm).CantidadVidrios = Convert.ToInt16(CantidadNum.Value);
                this.Close();
            }


        }
    }
}
