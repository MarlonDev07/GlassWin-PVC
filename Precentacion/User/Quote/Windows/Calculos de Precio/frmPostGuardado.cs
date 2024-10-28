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

namespace Precentacion.User.Quote.Windows.Calculos_de_Precio
{
    public partial class frmPostGuardado : MaterialSkin.Controls.MaterialForm
    {
        // Mantén solo la declaración de Costo como decimal
        decimal Costo = 0;
        decimal Total = 0;
        decimal SubTotal = 0;
        decimal TempTotal = 0;
        bool Aplicado = false;
        private decimal originalCosto;
        private decimal originalSubTotal;
        private decimal originalTotal;

        public frmPostGuardado()
        {
            InitializeComponent();
        }

        public void ObtenerDatos(DataTable dt, decimal _Total)
        {
            try
            {
                // Setear el PrecioCosto
                if (dt != null)
                {
                    Costo = 0; // Reinicia Costo al principio del método
                    foreach (DataRow dr in dt.Rows)
                    {
                        // Convertir el valor de Price a decimal
                        decimal precio = Convert.ToDecimal(dr["Price"]);
                        Costo += precio; // Ahora Costo es decimal, por lo que puedes sumar precios decimal
                    }
                    txtPrecioCosto.Text = Costo.ToString("c");
                }

                // Setear el SubTotal
                if (_Total != 0)
                {
                    SubTotal = _Total;
                    txtSubTotal.Text = SubTotal.ToString("c");
                }

                // Convertir txtPrecioCosto.Text a decimal
                decimal costoDecimal = decimal.Parse(txtPrecioCosto.Text, NumberStyles.Currency, CultureInfo.CurrentCulture);

                // Calcular Utilidad
                decimal utilidad1 = _Total - costoDecimal;
                decimal utilidadTotal = (utilidad1 / _Total) * 100; // Multiplica por 100 para obtener porcentaje

                // Redondear a 2 Decimales
                utilidadTotal = Math.Round(utilidadTotal, 2);

                // Setear el Ajuste
                txtUtilidad.Text = utilidadTotal.ToString() + "%";

                // Calcular el Total
                Total = SubTotal;
                txtTotal.Text = Total.ToString("c");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void numCantidad_ValueChanged(object sender, EventArgs e)
        {
            // Obtener el valor actual de la cantidad
            int cantidad = Convert.ToInt32(numCantidad.Value);

            // Recalcular y actualizar los valores
            RecalcularDatos(cantidad);
        }

        private void RecalcularDatos(int cantidad)
        {
            // Si `originalCosto`, `originalSubTotal`, y `originalTotal` no se han inicializado, inicializarlos con los valores actuales
            if (originalCosto == 0 && originalSubTotal == 0 && originalTotal == 0)
            {
                if (decimal.TryParse(txtPrecioCosto.Text, NumberStyles.Currency, CultureInfo.CurrentCulture, out decimal costoDecimal))
                {
                    originalCosto = costoDecimal;
                }
                if (decimal.TryParse(txtSubTotal.Text, NumberStyles.Currency, CultureInfo.CurrentCulture, out decimal subTotalDecimal))
                {
                    originalSubTotal = subTotalDecimal;
                }
                if (decimal.TryParse(txtTotal.Text, NumberStyles.Currency, CultureInfo.CurrentCulture, out decimal totalDecimal))
                {
                    originalTotal = totalDecimal;
                }
            }

            // Actualizar SubTotal y Costo basado en la cantidad
            SubTotal = originalSubTotal * cantidad;
            txtSubTotal.Text = SubTotal.ToString("c");

            Costo = originalCosto * cantidad;
            txtPrecioCosto.Text = Costo.ToString("c");

            // Inicializar el Total con el SubTotal antes de aplicar descuento
            Total = SubTotal;

            // Aplicar descuento si existe
            decimal descuentoDecimal = 0;
            if (!string.IsNullOrEmpty(txtDescuento.Text))
            {
                // Validar que se haya ingresado un valor numérico
                if (decimal.TryParse(txtDescuento.Text, out decimal descuento))
                {
                    // Calcular descuento en porcentaje
                    descuentoDecimal = descuento / 100;

                    // Aplicar el descuento al SubTotal
                    TempTotal = SubTotal - (SubTotal * descuentoDecimal);
                    //txtSubTotal.Text = TempTotal.ToString("c");
                    txtTotal.Text = TempTotal.ToString("c");
                }
                else
                {
                    MessageBox.Show("El Descuento debe ser un valor numérico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Terminar la ejecución si el descuento es inválido
                }
            }
            else
            {
                // Si no hay descuento, el total es igual al SubTotal
                TempTotal = Total;
                txtTotal.Text = TempTotal.ToString("c");
            }

            // Ahora recalcular la Utilidad usando TempTotal (que puede tener descuento aplicado)
            decimal utilidad1 = TempTotal - Costo;
            decimal utilidadTotal = (TempTotal != 0) ? (utilidad1 / TempTotal) * 100 : 0; // Multiplica por 100 para obtener porcentaje

            // Redondear a 2 Decimales
            utilidadTotal = Math.Round(utilidadTotal, 2);

            // Setear el Ajuste en la caja de texto
            txtUtilidad.Text = utilidadTotal.ToString() + "%";
        }






        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (TempTotal != 0)
            {
                Total = TempTotal;
            }
            Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is frmCalcPriceWindows);
            if (frm != null)
            {
                ((frmCalcPriceWindows)frm).totalPrice = Total;
                ((frmCalcPriceWindows)frm).NumCantidad = Convert.ToInt32(numCantidad.Value);
                ((frmCalcPriceWindows)frm).btnInsertar_Click(null, null);
            }
            this.Close();
        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            if (txtDescuento.Text != "")
            {
                // Validar que se haya ingresado un valor numérico
                if (decimal.TryParse(txtDescuento.Text, out decimal descuento))
                {
                    // Solo llamamos a RecalcularDatos para aplicar el descuento correctamente
                    RecalcularDatos(Convert.ToInt32(numCantidad.Value));
                }
                else
                {
                    MessageBox.Show("El Descuento debe ser un valor numérico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                txtDescuento.Text = "0";
            }
        }

        private void txtDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                // Puedes manejar la tecla Enter aquí si es necesario
            }
        }

        private void txtSubTotal_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUtilidad_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmPostGuardado_Load(object sender, EventArgs e)
        {

        }
    }
}
