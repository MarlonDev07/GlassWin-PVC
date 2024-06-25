﻿using Negocio.Company.Employer;
using Precentacion.User.Quote.Quote;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Precentacion.User.Quote.Accesorios
{
    public partial class frmTablaViaticos : MaterialSkin.Controls.MaterialForm
    {
        #region Variables
        private decimal TotalGasolina = 0;
        private decimal TotalComida = 0;
        private decimal TotalHospedaje = 0;
        private decimal TotalSalarios = 0;
        decimal SubTotal;
        bool Sumado = false;
        #endregion

        #region Constructor
        public frmTablaViaticos()
        {
            InitializeComponent();
            CargarEmpleadosCmb();
            AccesoriosUI.loadMaterial(this);
            // Configurar el formulario para que se abra en el centro de la pantalla
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        #endregion

        #region Gasolina
        private void txtGasolina_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtDistancia.Text != "")
                {
                    if (txtPrecioxKM.Text != "")
                    {
                        Decimal Distancia = Convert.ToDecimal(txtDistancia.Text);
                        Decimal PrecioxKM = Convert.ToDecimal(txtPrecioxKM.Text);
                        int Cantidad = Convert.ToInt32(numericVeiculos.Text);
                        TotalGasolina = (Distancia * PrecioxKM)*Cantidad;
                        txtTotalGasolina.Text = TotalGasolina.ToString("c");   
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al ingresar un valor Utilizar solo Numeros", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAbrirMaps_Click(object sender, EventArgs e)
        {
            //Abrir el navegador con la direccion
            System.Diagnostics.Process.Start("https://www.google.com/maps");
        }
        #endregion

        #region Comida
        private void txtComida_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtDesayuno.Text != "")
                {
                    if (txtAlmuerzo.Text != "")
                    {
                        if(txtCena.Text != "") 
                        {
                            Decimal Desayuno = Convert.ToDecimal(txtDesayuno.Text);
                            Decimal Almuerzo = Convert.ToDecimal(txtAlmuerzo.Text);
                            Decimal Cena = Convert.ToDecimal(txtCena.Text);
                            
                            TotalComida = ((Desayuno+Almuerzo+Cena)* Convert.ToInt32(NumericDias.Value)) * Convert.ToInt32(NumericEmpleados.Value);
                            txtTotalComida.Text = TotalComida.ToString("c");
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al ingresar un valor Utilizar solo Numeros", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Hospedaje
        private void txtHospedaje_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPrecioHabitacion.Text != "")
                {
                    decimal PrecioxNoche = Convert.ToDecimal(txtPrecioHabitacion.Text);
                    TotalHospedaje = (PrecioxNoche * Convert.ToInt32(NumericHabitaciones.Value)) * Convert.ToInt32(NumericNoches.Value);
                    txtTotalHospedaje.Text = TotalHospedaje.ToString("c");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al ingresar un valor Utilizar solo Numeros", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnHospedaje_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.bing.com/travel/hotel-search?q=buscar+Cabinas+costarrica&cin=2024-05-05&cout=2024-05-06&guests=2A&rooms=1&sort=Popularity&type=hotel&mapBounds=10%2C866196%2C-85%2C27943%2C9%2C274269%2C-83%2C94219&cacheId=undefined__4b4044eb-8775-4b61-8ddd-dd14781af7e8__2__cabinas&form=HTSEHL&entrypoint=HTSEHL");
        }
        #endregion

        #region Salarios
        private void CargarEmpleadosCmb()
        {
            try
            {
                //Cargar Empleados
                N_Employer Employer = new N_Employer();
                DataTable dataTable = new DataTable();
                dataTable = Employer.LoadEmployer();
                //Juntar dos columnas en una separadas por un - la primera columna es el FirstName y la segunda el PaymentHours
                dataTable.Columns.Add("NameSalary", typeof(string), "FirstName+ '-' +PaymentHours");

                //Asignar la columna NameSalary al ComboBox
                cmbEmpleados.DataSource = dataTable;
                cmbEmpleados.DisplayMember = "NameSalary";
               


             
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar los Empleados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        private void ObtenerSalario_Click(object sender, EventArgs e)
        {
            try
            {
                //Obtener Salario
                decimal SalariosAcumulados = 0;
                string[] Salario = cmbEmpleados.Text.Split('-');
                if (txtSalarios.Text != "")
                {
                    SalariosAcumulados = Convert.ToDecimal(txtSalarios.Text);
                }
                SalariosAcumulados += Convert.ToDecimal(Salario[1]);
                txtSalarios.Text = SalariosAcumulados.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Error al obtener el Salario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtSalarios_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSalarios.Text != "")
                {
                    if (txtHoras.Text != "")
                    {
                        decimal Salarios = Convert.ToDecimal(txtSalarios.Text);

                        TotalSalarios = Salarios * Convert.ToInt32(txtHoras.Text);
                        textBox2.Text = TotalSalarios.ToString("c");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al ingresar un valor Utilizar solo Numeros", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region CalcularTotal
        private void CalcularTotal_TextChange(object sender, EventArgs e)
        {
            
            decimal Total = TotalGasolina + TotalComida + TotalHospedaje + TotalSalarios;
            txtTotalViaticos.Text = Total.ToString();
        }
        #endregion

        private void txtTotalViaticos_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtTotalViaticos.Text, "[^0-9.,]"))
            {
                MessageBox.Show("Por favor ingrese solo numeros", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTotalViaticos.Text = txtTotalViaticos.Text.Remove(txtTotalViaticos.Text.Length - 1);
                txtTotalViaticos.Select(txtTotalViaticos.Text.Length, 0);

                
               

            }
            else
            {
                if (txtTotalViaticos.Text.Contains("."))
                {
                    //Reemplazar el punto por una coma
                    txtTotalViaticos.Text = txtTotalViaticos.Text.Replace(".", ",");
                }
                if (txtUtilidad.Text.Contains("."))
                {
                    //Reemplazar el punto por una coma
                    txtUtilidad.Text = txtUtilidad.Text.Replace(".", ",");
                }

                if (txtTotalViaticos.Text != "")
                {
                    if (txtUtilidad.Text != "")
                    {
                        decimal TotalViaticos = Convert.ToDecimal(txtTotalViaticos.Text);
                        decimal Utilidad = Convert.ToDecimal(txtUtilidad.Text);
                        decimal CostoTotal = TotalViaticos + Utilidad;
                        decimal Porcentaje =  ((Utilidad + TotalViaticos) / SubTotal)*100;
                        txtPorcentaje.Text = Porcentaje.ToString("0.00");
                    }
                    else
                    {
                        txtPorcentaje.Text = "0";
                    }
                }
                else
                {
                    txtPorcentaje.Text = "0";
                }
            }
        }
        public void CargarSubTotal(decimal SubProforma) 
        {
            SubTotal = SubProforma;
            txtSubTotalProforma.Text = SubTotal.ToString();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            //Enviar los datos a la Proforma
            Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is frmQuote);
            if (frm != null)
            {
                ((frmQuote)frm).txtManoObra.Text = txtPorcentaje.Text;
                ((frmQuote)frm).btnApply_Click(sender, e);
            }
        }
    }
}
