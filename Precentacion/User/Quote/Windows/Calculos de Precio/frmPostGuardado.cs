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

namespace Precentacion.User.Quote.Windows.Calculos_de_Precio
{
    public partial class frmPostGuardado : MaterialSkin.Controls.MaterialForm
    {
        int Costo = 0;
        decimal Ajuste = 0;
        decimal Total = 0;
        decimal SubTotal = 0;
        public frmPostGuardado()
        {
            InitializeComponent();
        }


        public void ObtenerDatos(DataTable dt, decimal AjustePrecio) 
        {
            //Setear el Ajuste de Precio
             Ajuste = AjustePrecio;
             txtUtilidad.Text = Ajuste.ToString()+"%";
            //Obtiene los datos de la tabla y los muestra en el formulario
            if (dt != null) 
            {
                Costo = 0;
                foreach (DataRow dr in dt.Rows) 
                {
                    Costo += Convert.ToInt32(dr["Price"]);
                }
                txtPrecioCosto.Text = Costo.ToString("c");
            }
            numCantidad_ValueChanged(null, null);
        }
        private void CalcularSubtotal_TextChange(Object Sender, EventArgs e) 
        {
            SubTotal = 0;
            if (Costo != 0 && Ajuste != 0) 
            {
                decimal AjustePrecio = Costo * Ajuste;
                SubTotal = Costo + AjustePrecio;
                txtSubTotal.Text = SubTotal.ToString("c");
            }
        }

        private void numCantidad_ValueChanged(object sender, EventArgs e)
        {
            Total = 0;
            //Obtener la Cantidad
            int Cantidad = Convert.ToInt32(numCantidad.Value);

            //Calcular el Total
            Total = SubTotal * Cantidad;

            //Mostrar el Total
            txtTotal.Text = Total.ToString("c");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is frmCalcPriceWindows);
            if (frm != null)
            {
               ((frmCalcPriceWindows)frm).totalPrice = Total;
               ((frmCalcPriceWindows)frm).NumCantidad = Convert.ToInt32(numCantidad.Value);
               ((frmCalcPriceWindows)frm).btnInsertar_Click(null, null);
            }
        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            //Validar que se haya ingresado un valor numerico
            if (decimal.TryParse(txtDescuento.Text, out decimal descuento))
            {
                //Obtener el Descuento
                decimal Descuento = (Convert.ToDecimal(txtDescuento.Text)/100);

                //Calcular el Total
                Total = SubTotal - (SubTotal * Descuento);
                txtTotal.Text = Total.ToString("c");
            }
        }
    }
}
