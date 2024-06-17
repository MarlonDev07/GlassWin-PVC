using Negocio.Company.AdmProyecto;
using Precentacion.User.DashBoard;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Precentacion.User.AdmProyecto
{
    public partial class frmAdmProyecto : MaterialSkin.Controls.MaterialForm
    {
        #region Variables
        N_AdmProyecto n_AdmProyecto = new N_AdmProyecto();
        N_Gastos n_Gastos = new N_Gastos();
        decimal Gastos = 0;
        decimal MontoPagar = 0;
        decimal TotalGastos = 0;
        decimal CapitalProyecto = 0;
        decimal TotalIngresos = 0;
        decimal Utilidad = 0;
        #endregion

        #region Constructor
        public frmAdmProyecto()
        {
            InitializeComponent();
        
        }
        #endregion

        #region Metodos
        //Metodos de Carga
        private void CargarProyectos()
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = n_AdmProyecto.ListarNombresProyectos("Activo");
                //Asignar los Datos al ComboBox
                cbProyecto.DataSource = dataTable;
                //Mostrar los Datos de la Columna ProyectoCompleto
                cbProyecto.DisplayMember = "ProyectoCompleto";
                cbProyecto.ValueMember = "IdAdmProyecto";
                //cbProyecto.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CargarGastos() 
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = n_Gastos.ListarGastos(Convert.ToInt32(txtIdProyecto.Text));
                dgvGastos.DataSource = dataTable;


                //Ajustar las columnas al Ancho del formulario
                dgvGastos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                //Ocultar la Columna 1
                dgvGastos.Columns[1].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CargarMontoPagar()
        {
            try
            {
                MontoPagar = n_AdmProyecto.MontoPagar(Convert.ToInt32(txtIdProyecto.Text));
                txtMontoPagar.Text = MontoPagar.ToString("c");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CargarCapitalProyecto()
        {
            try
            {
                CapitalProyecto = n_AdmProyecto.CapitalProyecto(Convert.ToInt32(txtIdProyecto.Text));
                txtCapital.Text = CapitalProyecto.ToString("c");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //*****************

        //Metodos de Apoyo
        private bool ValidarCampos() 
        { 
            if (txtIdProyecto.Text == string.Empty)
            {
                MessageBox.Show("Seleccione un Proyecto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (dtpFecha.Value == null)
            {
                MessageBox.Show("Seleccione una Fecha", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtMotivo.Text == string.Empty)
            {
                MessageBox.Show("Ingrese un Motivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtMonto.Text == string.Empty)
            {
                MessageBox.Show("Ingrese un Monto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void LimpiarCampos()
        {
            txtIdGasto.Text = string.Empty;
            dtpFecha.Value = DateTime.Now;
            txtMotivo.Text = string.Empty;
            txtMonto.Text = string.Empty;
        }
       
        //*****************

        //Metodos de Calculo
        private void CalculoTotalGastos()
        {
            try
            {
                TotalGastos = Gastos+MontoPagar;
                txtTotalGastos.Text = TotalGastos.ToString("c");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CalculoGastos()
        {
            try
            {
                Gastos = 0;
                foreach (DataGridViewRow row in dgvGastos.Rows)
                {
                    Gastos += Convert.ToDecimal(row.Cells[4].Value);
                }
                txtGastosProyecto.Text = Gastos.ToString("c");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CalculoIngresos()
        {
            try
            {

                TotalIngresos = CapitalProyecto;
                txtTotalIngresos.Text = TotalIngresos.ToString("c");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CalcularUtilidad() 
        {
            try
            {
                Utilidad = TotalIngresos - TotalGastos;
                txtUtilidad.Text = Utilidad.ToString("c");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //*****************

        //Metodos de Accion CRUD para Gastos
        private bool InsertarGasto() 
        {
            try
            {
                if (ValidarCampos())
                {
                    if (n_Gastos.InsertarGastos(Convert.ToInt32(txtIdProyecto.Text), dtpFecha.Value, txtMotivo.Text, Convert.ToDecimal(txtMonto.Text)))
                    {
                        MessageBox.Show("Gasto Insertado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbProyecto_SelectedIndexChanged(null, null);
                        LimpiarCampos();
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error al Insertar el Gasto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private bool ActualizarGasto()
        {
            try
            {
                if (ValidarCampos())
                {
                    if (n_Gastos.ActualizarGastos(Convert.ToInt32(txtIdGasto.Text), dtpFecha.Value, txtMotivo.Text, Convert.ToDecimal(txtMonto.Text)))
                    {
                        MessageBox.Show("Gasto Actualizado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbProyecto_SelectedIndexChanged(null, null);
                        LimpiarCampos();
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error al Actualizar el Gasto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private bool EliminarGasto()
        {
            try
            {
                if (ValidarCampos())
                {
                    if (n_Gastos.EliminarGastos(Convert.ToInt32(txtIdGasto.Text)))
                    {
                        MessageBox.Show("Gasto Eliminado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbProyecto_SelectedIndexChanged(null, null);
                        LimpiarCampos();
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error al Eliminar el Gasto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        //**********************************

        //Metodos de Accion CRUD para Proyectos
        private bool FinalizarProyecto()
        {
            try
            {
                if (n_AdmProyecto.FinalzarProyecto(Convert.ToInt32(txtIdProyecto.Text)))
                {
                    MessageBox.Show("Proyecto Finalizado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarProyectos();
                    return true;
                }
                else
                {
                    MessageBox.Show("Error al Finalizar el Proyecto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        //*************************************
        #endregion

        #region Eventos
        private void frmAdmProyecto_Load(object sender, EventArgs e)
        {
            CargarProyectos();
        }
        private void cbProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Obtener el IdAdmProyecto qu esta antes del - en el ComboBox
                string[] IdAdmProyecto = cbProyecto.Text.Split('-');
                //Asignar el IdAdmProyecto al TextBox
                txtIdProyecto.Text = IdAdmProyecto[0].Trim();

                //Obtener el Proyecto que esta despues del - en el ComboBox
                string[] Proyecto = cbProyecto.Text.Split('-');
                //Asignar el Proyecto al TextBox
                txtNombreProyecto.Text = Proyecto[1].Trim();

                //Cargar los Gastos del Proyecto
                CargarGastos();
                CargarMontoPagar();
                CalculoGastos();
                CalculoTotalGastos();

                //Cargar el Capital del Proyecto
                CargarCapitalProyecto();
                CalculoIngresos();

                //Cargar la Utilidad
                CalcularUtilidad();

               

            }
            catch (Exception)
            {
            }
        }
        private void dgvGastos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //Pasar los Datos del DataGridView a los TextBox
                txtIdGasto.Text = dgvGastos.CurrentRow.Cells[0].Value.ToString();
                dtpFecha.Value = Convert.ToDateTime(dgvGastos.CurrentRow.Cells[2].Value);
                txtMotivo.Text = dgvGastos.CurrentRow.Cells[3].Value.ToString();
                txtMonto.Text = dgvGastos.CurrentRow.Cells[4].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void txtUtilidad_TextChanged(object sender, EventArgs e)
        {
            //Validar si la Utilidad es Negativa
            if (Utilidad < 0)
            {
                txtUtilidad.ForeColor = Color.Red;
            }
            else
            {
                txtUtilidad.ForeColor = Color.Green;
            }
        }
        #endregion

        #region Botones
        private void btnAccept_Click(object sender, EventArgs e)
        {
            //Preguntar si desea Insertar el Gasto
            DialogResult result = MessageBox.Show("Desea Insertar el Gasto", "Insertar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                InsertarGasto();
            }
        }
        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            //Preguntar si desea Actualizar el Gasto
            DialogResult result = MessageBox.Show("Desea Actualizar el Gasto", "Actualizar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ActualizarGasto();
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Preguntar si desea Eliminar el Gasto
            DialogResult result = MessageBox.Show("Desea Eliminar el Gasto", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                EliminarGasto();
            }
        }
        private void btnTerminar_Click(object sender, EventArgs e)
        {
            FinalizarProyecto();
        }

        #endregion

        private void frmAdmProyecto_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmDashUser frm = frmDashUser.Instance;
            frm.WindowState = FormWindowState.Normal;
            frm.Show();
            frm.BringToFront();
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Obtener el IdAdmProyecto qu esta antes del - en el ComboBox
            string[] IdAdmProyecto = cbProyecto.Text.Split('-');
            
            //preguntar si desea Eliminar el Proyecto
            DialogResult result = MessageBox.Show("Desea Eliminar el Proyecto", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (n_AdmProyecto.EliminarProyecto(Convert.ToInt32(IdAdmProyecto[0].Trim())))
                {
                    MessageBox.Show("Proyecto Eliminado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarProyectos();
                }
                else
                {
                    MessageBox.Show("Error al Eliminar el Proyecto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtMonto_TextChanged(object sender, EventArgs e)
        {
            //Validar Si se Ingreso un punto y cambiarlo por una coma
            if (txtMonto.Text.Contains("."))
            {
                txtMonto.Text = txtMonto.Text.Replace(".", ",");

                //posicionar el cursor al final del texto
                txtMonto.SelectionStart = txtMonto.Text.Length;

            }
        }
    }
}
