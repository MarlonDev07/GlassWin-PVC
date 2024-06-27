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
        decimal Total = 0;
        decimal SubTotal = 0;
        decimal TempTotal = 0;
        bool Aplicado = false; 
        public frmPostGuardado()
        {
            InitializeComponent();
        }


        public void ObtenerDatos(DataTable dt, decimal _Total) 
        {
            //Setear el PrecioCosto
            if (dt != null) 
            {
                Costo = 0;
                foreach (DataRow dr in dt.Rows) 
                {
                    Costo += Convert.ToInt32(dr["Price"]);
                }
                txtPrecioCosto.Text = Costo.ToString("c");
            }

            //Setear el  SubTotal
            if (_Total != 0)
            {
                SubTotal = _Total;
                txtSubTotal.Text = SubTotal.ToString("c");
            }

            //Setear el Ajuste
            decimal Ajuste = (SubTotal - Costo)/Costo;
            Ajuste = Ajuste * 100;
            //Redondear a 2 Decimales
            Ajuste = Math.Round(Ajuste, 2);
            txtUtilidad.Text = Ajuste.ToString()+"%";

            //Calcular el Total
            Total = SubTotal;
            txtTotal.Text = Total.ToString("c");
        }

        private void numCantidad_ValueChanged(object sender, EventArgs e)
        {
            TempTotal = Total * Convert.ToInt32(numCantidad.Value);
            txtTotal.Text = TempTotal.ToString("c");
            txtDescuento_TextChanged(null, null);
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
                //Validar que se haya ingresado un valor numerico
                if (decimal.TryParse(txtDescuento.Text, out decimal descuento))
                {
                    TempTotal = Total * Convert.ToInt32(numCantidad.Value);

                    //Obtener el Descuento
                    decimal Descuento = (Convert.ToDecimal(txtDescuento.Text) / 100);

                    //Calcular el Total
                    TempTotal = TempTotal - (TempTotal * Descuento);
                    txtTotal.Text = TempTotal.ToString("c");               
                }
                else
                {
                    MessageBox.Show("El Descuento debe ser un valor numerico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                
            }
        }

       
    }
}
