using Dominio.Model.ClassWindows;
using Precentacion.User.Quote.Quote;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
            decimal ancho, alto;

            // Normalizar los valores ingresados reemplazando comas con puntos
            string anchoText = txtAncho.Text.Replace(',', '.');
            string altoText = txtAlto.Text.Replace(',', '.');

            // Intentar convertir los textos a decimal
            if (!Decimal.TryParse(anchoText, NumberStyles.Any, CultureInfo.InvariantCulture, out ancho))
            {
                MessageBox.Show("Por favor, ingrese un valor válido para el ancho.");
                return;
            }
            if (!Decimal.TryParse(altoText, NumberStyles.Any, CultureInfo.InvariantCulture, out alto))
            {
                MessageBox.Show("Por favor, ingrese un valor válido para el alto.");
                return;
            }

            ClsWindows.Weight = ancho;
            ClsWindows.heigt = alto;

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
