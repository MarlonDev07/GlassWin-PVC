using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio.Model.Proveedor;
using Negocio.Company.RegProveedor;
using Precentacion.User.DashBoard;


namespace Precentacion.User.RegProveedor
{
    public partial class frmRegistroProveedor : MaterialSkin.Controls.MaterialForm
    {
        public frmRegistroProveedor()
        {
            InitializeComponent();
            ListaProveedores();
        }

        #region ListaProveedores
        private void ListaProveedores()
        {
            try
            {
                N_RegProveedor n_RegProveedor = new N_RegProveedor();
                dgvProveedor.DataSource = n_RegProveedor.ListaProveedores();
                ConfigDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al listar los proveedores: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ConfigDataGrid() 
        {
            dgvProveedor.ReadOnly = true;

            //OCULTAR COLUMNAS 0 Y 1
            dgvProveedor.Columns[0].Visible = false;
            dgvProveedor.Columns[1].Visible = false;
            dgvProveedor.Columns[10].Visible = false;
            dgvProveedor.Columns[11].Visible = false;
            dgvProveedor.Columns[12].Visible = false;
            dgvProveedor.Columns[13].Visible = false;
            dgvProveedor.Columns[14].Visible = false;
            dgvProveedor.Columns[15].Visible = false;
            dgvProveedor.Columns[16].Visible = false;
            dgvProveedor.Columns[17].Visible = false;
            dgvProveedor.Columns[18].Visible = false;

            //ANCHO DE COLUMNAS IGUAL AL ANCHO DEL DATAGRID USANDO FILL
            dgvProveedor.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }
        #endregion

        #region Validaciones
        private void tabControlPrincipal_Selected(object sender, TabControlEventArgs e)
        {
            if (tabControlPrincipal.SelectedIndex == 1)
            {
                if (txtConsCedula.Text == "")
                {
                    MessageBox.Show("No se ha seleccionado un proveedor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPrincipal.SelectedIndex = 0;
                }
            }

            if (tabControlPrincipal.SelectedIndex == 3)
            {
                if (txtEditCedula.Text == "")
                {
                    MessageBox.Show("No se ha seleccionado un proveedor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPrincipal.SelectedIndex = 0;
                }
            }
        }
        #endregion

        #region Consultar
        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Pasar los datos de la fila seleccionada a los textbox
                txtConsCedula.Text = dgvProveedor.CurrentRow.Cells[2].Value.ToString();
                txtConsNombre.Text = dgvProveedor.CurrentRow.Cells[3].Value.ToString();
                txtConsDireccion.Text = dgvProveedor.CurrentRow.Cells[4].Value.ToString();
                txtConsCorreo.Text = dgvProveedor.CurrentRow.Cells[5].Value.ToString();
                txtConsTelefono.Text = dgvProveedor.CurrentRow.Cells[6].Value.ToString();
                txtConsApt.Text = dgvProveedor.CurrentRow.Cells[7].Value.ToString();
                txtConsDiasCredito.Text = dgvProveedor.CurrentRow.Cells[8].Value.ToString();
                txtConsLimite.Text = dgvProveedor.CurrentRow.Cells[9].Value.ToString();
                txtConsFechaUltimoPago.Text = dgvProveedor.CurrentRow.Cells[10].Value.ToString();
                txtConsDocUltimoPago.Text = dgvProveedor.CurrentRow.Cells[11].Value.ToString();
                txtConsFechaUltimaCompra.Text = dgvProveedor.CurrentRow.Cells[12].Value.ToString();
                txtConsDocUltimaCompra.Text = dgvProveedor.CurrentRow.Cells[13].Value.ToString();
                txtConsFechaApertura.Text = dgvProveedor.CurrentRow.Cells[14].Value.ToString();
                txtConsSaldoInicia.Text = dgvProveedor.CurrentRow.Cells[15].Value.ToString();
                txtConsCargos.Text = dgvProveedor.CurrentRow.Cells[16].Value.ToString();
                txtConsDescargo.Text = dgvProveedor.CurrentRow.Cells[17].Value.ToString();
                txtConsSaldoActual.Text = dgvProveedor.CurrentRow.Cells[18].Value.ToString();

                tabControlPrincipal.SelectedIndex = 1;
            }
            catch (Exception)
            {
                MessageBox.Show("Error al consultar el proveedor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion

        #region Nuevo
        private void btnAcceptNew_Click(object sender, EventArgs e)
        {
            if (ValidarCamposReg())
            {
                try
                {

                    cls_Proveedor cls_Proveedor = new cls_Proveedor();
                    cls_Proveedor.IdEmpresa = CompanyCache.IdCompany;
                    cls_Proveedor.CedulaJuridica = txtRegCedula.Text;
                    cls_Proveedor.Nombre = txtRegNombre.Text;
                    cls_Proveedor.Direccion = txtRegDireccion.Text;
                    cls_Proveedor.Correo = txtRegCorreo.Text;
                    cls_Proveedor.Telefono = txtRegTelefono.Text;
                    cls_Proveedor.Apc = txtRegATP.Text;
                    cls_Proveedor.DiasCredito = Convert.ToInt32(txtRegDiasCredito.Text);
                    cls_Proveedor.LimiteCredito = Convert.ToDecimal(txtRegLimite.Text);
                    
                    N_RegProveedor n_RegProveedor = new N_RegProveedor();
                    if (n_RegProveedor.InsertarProveedor(cls_Proveedor))
                    {
                        MessageBox.Show("Proveedor registrado correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListaProveedores();
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("Error al registrar el proveedor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception E)
                {
                    MessageBox.Show("Error al registrar el proveedor: "+E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        private bool ValidarCamposReg() 
        {
            if (txtRegCedula.Text == "") 
            {
                MessageBox.Show("El campo cedula es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtRegNombre.Text == "")
            {
                MessageBox.Show("El campo nombre es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtRegDireccion.Text == "")
            {
                MessageBox.Show("El campo direccion es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtRegCorreo.Text == "")
            {
                MessageBox.Show("El campo correo es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtRegTelefono.Text == "")
            {
                MessageBox.Show("El campo telefono es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtRegATP.Text == "")
            {
                MessageBox.Show("El campo apt es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtRegDiasCredito.Text == "")
            {
                MessageBox.Show("El campo dias de credito es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtRegLimite.Text == "")
            {
                MessageBox.Show("El campo limite es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void LimpiarCampos() 
        {             
            txtRegCedula.Text = "";
            txtRegNombre.Text = "";
            txtRegDireccion.Text = "";
            txtRegCorreo.Text = "";
            txtRegTelefono.Text = "";
            txtRegATP.Text = "";
            txtRegDiasCredito.Text = "";
            txtRegLimite.Text = "";
        }
        #endregion

        #region Editar
        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Pasar los datos de la fila seleccionada a los textbox
            txtEditCedula.Text = dgvProveedor.CurrentRow.Cells[2].Value.ToString();
            txtEditNombre.Text = dgvProveedor.CurrentRow.Cells[3].Value.ToString();
            txtEditDireccion.Text = dgvProveedor.CurrentRow.Cells[4].Value.ToString();
            txtEditCorreo.Text = dgvProveedor.CurrentRow.Cells[5].Value.ToString();
            txtEditTelefono.Text = dgvProveedor.CurrentRow.Cells[6].Value.ToString();
            txtEditAtc.Text = dgvProveedor.CurrentRow.Cells[7].Value.ToString();
            txtxEditDiasCredito.Text = dgvProveedor.CurrentRow.Cells[8].Value.ToString();
            txtEditLimiteCredito.Text = dgvProveedor.CurrentRow.Cells[9].Value.ToString();

            tabControlPrincipal.SelectedIndex = 3;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (ValidarCamposActualizar())
            {
                try
                {
                    cls_Proveedor cls_Proveedor = new cls_Proveedor();
                    cls_Proveedor.IdProveedor = Convert.ToInt32(dgvProveedor.CurrentRow.Cells[1].Value);
                    cls_Proveedor.IdEmpresa = CompanyCache.IdCompany;
                    cls_Proveedor.CedulaJuridica = txtEditCedula.Text;
                    cls_Proveedor.Nombre = txtEditNombre.Text;
                    cls_Proveedor.Direccion = txtEditDireccion.Text;
                    cls_Proveedor.Correo = txtEditCorreo.Text;
                    cls_Proveedor.Telefono = txtEditTelefono.Text;
                    cls_Proveedor.Apc = txtEditAtc.Text;
                    cls_Proveedor.DiasCredito = Convert.ToInt32(txtxEditDiasCredito.Text);
                    cls_Proveedor.LimiteCredito = Convert.ToDecimal(txtEditLimiteCredito.Text);

                    N_RegProveedor n_RegProveedor = new N_RegProveedor();
                    if (n_RegProveedor.ActualizarProveedor(cls_Proveedor))
                    {
                        MessageBox.Show("Proveedor actualizado correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListaProveedores();
                        LimpiarCamposActualizar();
                        tabControlPrincipal.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show("Error al actualizar el proveedor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception E)
                {
                    MessageBox.Show("Error al actualizar el proveedor: " + E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private bool ValidarCamposActualizar() 
        {
            if (txtEditCedula.Text == "")
            {
                MessageBox.Show("El campo cedula es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtEditNombre.Text == "")
            {
                MessageBox.Show("El campo nombre es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtEditDireccion.Text == "")
            {
                MessageBox.Show("El campo direccion es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtEditCorreo.Text == "")
            {
                MessageBox.Show("El campo correo es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtEditTelefono.Text == "")
            {
                MessageBox.Show("El campo telefono es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtEditAtc.Text == "")
            {
                MessageBox.Show("El campo apt es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtxEditDiasCredito.Text == "")
            {
                MessageBox.Show("El campo dias de credito es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtEditLimiteCredito.Text == "")
            {
                MessageBox.Show("El campo limite es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void LimpiarCamposActualizar()
        {
            txtEditCedula.Text = "";
            txtEditNombre.Text = "";
            txtEditDireccion.Text = "";
            txtEditCorreo.Text = "";
            txtEditTelefono.Text = "";
            txtEditAtc.Text = "";
            txtxEditDiasCredito.Text = "";
            txtEditLimiteCredito.Text = "";
        }
        #endregion

        #region Eliminar
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Está seguro de eliminar el proveedor?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int IdProveedor = Convert.ToInt32(dgvProveedor.CurrentRow.Cells[1].Value);
                    N_RegProveedor n_RegProveedor = new N_RegProveedor();
                    if (n_RegProveedor.EliminarProveedor(IdProveedor))
                    {
                        MessageBox.Show("Proveedor eliminado correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListaProveedores();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el proveedor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al eliminar el proveedor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        private void tabPageRegistrar_Click(object sender, EventArgs e)
        {

        }

        private void txtRegDireccion_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmRegistroProveedor_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmDashUser frm = frmDashUser.Instance;
            frm.WindowState = FormWindowState.Normal;
            frm.Show();
            frm.BringToFront();
            
        }
    }
}
