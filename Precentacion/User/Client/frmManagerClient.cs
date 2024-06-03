using Negocio.Client;
using Precentacion.User.DashBoard;
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
using MaterialSkin.Controls;
using Precentacion.User.Bill;

namespace Precentacion.User.Client
{
    public partial class frmManagerClient : MaterialForm//MaterialSkin.Controls.MaterialForm
    {
        #region Variables
        N_Client NClient = new N_Client();
        public bool EventFormClose = true;
        #endregion

        #region Constructor
        public frmManagerClient()
        {
            InitializeComponent();
            Initialize();
            SystemUI.loadMaterial(this);
            // Configurar el formulario para abrir en pantalla casi completa
            this.Size = new Size((int)(Screen.PrimaryScreen.WorkingArea.Width * 0.9), (int)(Screen.PrimaryScreen.WorkingArea.Height * 0.9));
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        #endregion

        #region Initialize
        private void Initialize()
        {
           dgvClientLoad();
           ConfigurateDGV();
        }
        private void dgvClientLoad() 
        { 
          dgvClient.DataSource = NClient.LoadClient();
        }
        private void ConfigurateDGV()
        {
            try
            {
                //Ocultar columnas
                dgvClient.Columns[3].Visible = false;
                dgvClient.Columns[6].Visible = false;

                //Modificar titulo de las columnas
                dgvClient.Columns[0].HeaderText = "ID";
                dgvClient.Columns[1].HeaderText = "Nombre";
                dgvClient.Columns[2].HeaderText = "Telefono";
                dgvClient.Columns[4].HeaderText = "Direccion";
                dgvClient.Columns[5].HeaderText = "Correo";

                //Modificar todas las columnas al ancho del Form
                dgvClient.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                //Ordenar Alfabeticamente
                dgvClient.Sort(dgvClient.Columns[1], ListSortDirection.Ascending);

            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar los datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
           
        }

        #endregion

        #region Validaciones
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Validar que el tab seleccionado sea el de editar
            if (tabControl.SelectedIndex == 2)
            {
                //Validar que se haya seleccionado un cliente
                if (txtId.Text != "")
                {

                }
                else
                {
                    //Si no se ha seleccionado un cliente, mostrar mensaje de error
                    tabControl.SelectedIndex = 0;
                    MessageBox.Show("Debe seleccionar un cliente para Editar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
        }
        #endregion

        #region Eventos
        private void editarClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Validar que se haya seleccionado un cliente en el DataGridView
            if (dgvClient.SelectedRows.Count > 0)
            {
                //Obtener el ID del cliente seleccionado
                txtId.Text = dgvClient.CurrentRow.Cells[0].Value.ToString();

                //Obtener el nombre del cliente seleccionado
                txtNameEdit.Text = dgvClient.CurrentRow.Cells[1].Value.ToString();

                //Obtener el telefono del cliente seleccionado
                txtPhoneEdit.Text = dgvClient.CurrentRow.Cells[2].Value.ToString();

                //Obtener la direccion del cliente seleccionado
                txtAddressEdit.Text = dgvClient.CurrentRow.Cells[4].Value.ToString();

                //Obtener el correo del cliente seleccionado
                txtEmailEdit.Text = dgvClient.CurrentRow.Cells[5].Value.ToString();

                //Cambiar al tab de editar
                tabControl.SelectedIndex = 2;
            }
            else
            {
                //Si no se ha seleccionado un cliente, mostrar mensaje de error
                MessageBox.Show("Debe seleccionar un cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void formclosing(object sender, FormClosingEventArgs e)
        {
            //Validar que el evento sea necesario
            if (EventFormClose == true)
            {
                //Validar que el form de DashBoard este abierto
                frmDashUser frm = Application.OpenForms.OfType<frmDashUser>().FirstOrDefault();
                if (frm != null)
                {
                    //Si esta abierto, cambiar el estado a normal
                    frm.WindowState = FormWindowState.Normal;
                }
                else
                {
                    //Si no esta abierto, crear una instancia y mostrarlo
                    frmDashUser frmDash = new frmDashUser();
                    frmDash.Show();
                }
            }
        }
        #endregion

        #region New Client
        //---------------------------New Client---------------------------------//
        private void btnNewClient_Click(object sender, EventArgs e)
        {
            //Validar que los campos no esten vacios
            if (ValidateNewClient())
            {
                //Validar que el cliente no exista
                if (NewClient())
                {
                    //Mostrar mensaje de exito
                    MessageBox.Show("Cliente creado correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Limpiar campos
                    CleanNewClient();

                    //Actualizar DGV
                    dgvClientLoad();
                }
                else
                {
                    //Mostrar mensaje de error
                    MessageBox.Show("Error al crear el cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private bool ValidateNewClient()
        {
            //Validar que los campos no esten vacios
            if (txtName.Text != "" && txtPhone.Text != "" && txtAddress.Text != "" )
            {
                return true;
            }
            else
            {
                MessageBox.Show("Debe llenar todos los campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }
        private bool NewClient() 
        { 
            string Name = txtName.Text;
            string Phone = txtPhone.Text;
            string Address = txtAddress.Text;
            string Email = txtEmail.Text;

            return NClient.Create(Name, Phone, Address, Email);
        }
        private void CleanNewClient()
        {
            txtName.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            txtEmail.Text = "";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CleanNewClient();
            tabControl.SelectedIndex = 0;
        }

        #endregion

        #region Edit Client
        //---------------------------Edit Client---------------------------------//
        private void btnEditClient_Click(object sender, EventArgs e)
        {
            //Validar que los campos no esten vacios
            if (ValidateEditClient())
            {
                //Validar que el cliente no exista
                if (EditClient())
                {
                    //Mostrar mensaje de exito
                    MessageBox.Show("Cliente editado correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Limpiar campos
                    CleanEditClient();

                    //Actualizar DGV
                    dgvClientLoad();
                }
                else
                {
                    //Mostrar mensaje de error
                    MessageBox.Show("Error al editar el cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        private bool ValidateEditClient()
        {
            //Validar que los campos no esten vacios
            if (txtNameEdit.Text != "" && txtPhoneEdit.Text != "" && txtAddressEdit.Text != "")
            {
                return true;
            }
            else
            {
                MessageBox.Show("Debe llenar todos los campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }
        private bool EditClient()
        {
            int ID = Convert.ToInt32(txtId.Text);
            string Name = txtNameEdit.Text;
            string Phone = txtPhoneEdit.Text;
            string Address = txtAddressEdit.Text;
            string Email = txtEmailEdit.Text;

            return NClient.update(ID, Name, Phone, Address, Email);
        }
        private void CleanEditClient()
        {
            txtId.Text = "";
            txtNameEdit.Text = "";
            txtPhoneEdit.Text = "";
            txtAddressEdit.Text = "";
            txtEmailEdit.Text = "";
        }

        private void btnCancelarEdit_Click(object sender, EventArgs e)
        {
            CleanEditClient();
            tabControl.SelectedIndex = 0;
        }
        #endregion

        private void cotizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Validar si el formulario de quotes esta abierto
            frmQuote frm = Application.OpenForms.OfType<frmQuote>().FirstOrDefault();
            if (frm != null)
            {
                //Si esta abierto, Pasar el ID del cliente seleccionado
                frm.txtidClient.Text = dgvClient.CurrentRow.Cells[0].Value.ToString();
                ((frmQuote)frm).btnBuscar_Click(null, null);
                
                //Cambiar el estado del EventFormClose a false
                EventFormClose = false;
            }
            else
            {
                //Si no esta abierto, crear una instancia y mostrarlo
                frmQuote frmQuote = new frmQuote();
                frmQuote.Show();
                frmQuote.txtidClient.Text = dgvClient.CurrentRow.Cells[0].Value.ToString();
                ((frmQuote)frmQuote).btnBuscar_Click(null, null);
                EventFormClose = false;
                this.Close();
            }
        }

        private void lblAgenda_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Ir a una URL
            System.Diagnostics.Process.Start("https://calendar.google.com/calendar/u/0/r");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dgvClient.CurrentCell = null;
            try
            {
                foreach (DataGridViewRow r in dgvClient.Rows)
                {
                    bool rowVisible = false;
                    foreach (DataGridViewCell c in r.Cells)
                    {
                        if (c.Value != null && c.Value.ToString().ToUpper().Contains(textBox1.Text.ToUpper()))
                        {
                            rowVisible = true;
                            break;
                        }
                    }
                    r.Visible = rowVisible;
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("Error al eliminar el cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminarClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvClient.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("¿Estas seguro de eliminar el cliente?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (NClient.DeleteClientData(Convert.ToInt32(dgvClient.CurrentRow.Cells[0].Value.ToString())))
                    {
                        MessageBox.Show("Cliente eliminado correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvClientLoad();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvClient_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cotizarToolStripMenuItem_Click(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CleanEditClient();
            tabControl.SelectedIndex = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Validar que los campos no esten vacios
            if (ValidateEditClient())
            {
                //Validar que el cliente no exista
                if (EditClient())
                {
                    //Mostrar mensaje de exito
                    MessageBox.Show("Cliente editado correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Limpiar campos
                    CleanEditClient();

                    //Actualizar DGV
                    dgvClientLoad();
                }
                else
                {
                    //Mostrar mensaje de error
                    MessageBox.Show("Error al editar el cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
