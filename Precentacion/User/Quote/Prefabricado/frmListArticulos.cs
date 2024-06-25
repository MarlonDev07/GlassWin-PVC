using Dominio.Model.ClasscmbArticulo;
using Negocio.LoadProduct;
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

namespace Precentacion.User.Quote.Prefabricado
{
    public partial class frmListArticulos : MaterialSkin.Controls.MaterialForm
    {
        public frmListArticulos()
        {
            InitializeComponent();
            ListarArticulos();

            // Usar BeginInvoke para asegurar que el Focus se establezca después de que el formulario se haya cargado completamente
            this.Load += new EventHandler(frmListArticulos_Load);

            // Agregar evento KeyDown al DataGridView dgvArticulos
            dgvArticulos.KeyDown += dgvArticulos_KeyDown;
        }

        private void frmListArticulos_Load(object sender, EventArgs e)
        {
            this.BeginInvoke((Action)delegate
            {
                txtBuscar.Focus();
            });
        }

        public void ListarArticulos()
        {
            N_LoadProduct objN = new N_LoadProduct();
            dgvArticulos.DataSource = objN.ListaArticulosxColor();

            //Cambiar Nombre de las Columnas
            dgvArticulos.Columns[3].HeaderText = "Precio";
        }

        private void cargarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CargarArticuloSeleccionado();
        }

        private void dgvArticulos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            CargarArticuloSeleccionado();
        }

        private void dgvArticulos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CargarArticuloSeleccionado();
            }
        }

        private void CargarArticuloSeleccionado()
        {
            // Verificar si hay una fila seleccionada
            if (dgvArticulos.CurrentRow != null)
            {
                string Id = dgvArticulos.CurrentRow.Cells[0].Value?.ToString();
                string Nombre = dgvArticulos.CurrentRow.Cells[1].Value?.ToString();
                string Precio = dgvArticulos.CurrentRow.Cells[3].Value?.ToString();

                if (string.IsNullOrEmpty(Id) || string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Precio))
                {
                    MessageBox.Show("La fila seleccionada no tiene datos válidos.");
                    return;
                }

                Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is frmPrefabricado);
                if (frm != null)
                {
                    // Supongamos que quieres asignar los valores a la fila seleccionada actualmente
                    DataGridViewRow filaSeleccionada = ((frmPrefabricado)frm).dgvPrefabricado.CurrentRow;

                    if (filaSeleccionada != null)
                    {
                        // Asignar los valores a las celdas específicas (suponiendo que la columna 0 es para Id, columna 1 para Nombre y columna 6 para Precio)
                        filaSeleccionada.Cells[0].Value = Id;
                        filaSeleccionada.Cells[1].Value = Nombre;
                        filaSeleccionada.Cells[6].Value = Precio;
                        filaSeleccionada.Cells[2].Value = "1";
                        filaSeleccionada.Cells[3].Value = "1";
                        filaSeleccionada.Cells[4].Value = "1";

                        //Agregar Fila
                        ((frmPrefabricado)frm).AgregarFila();

                        // Cerrar este formulario
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No hay una fila seleccionada en dgvPrefabricado.");
                    }
                }
                else
                {
                    MessageBox.Show("La ventana frmPrefabricado no está abierta.");
                }
            }
           
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            N_LoadProduct objN = new N_LoadProduct();
            string descripcion = txtBuscar.Text;

            // Reemplazar punto (.) por coma (,) y manejar el evento
            if (descripcion.Contains('.'))
            {
                descripcion = descripcion.Replace('.', ',');
                int cursorPosition = txtBuscar.SelectionStart;
                txtBuscar.Text = descripcion;
                txtBuscar.SelectionStart = cursorPosition;
            }

            DataTable dt = objN.BuscarArticulosPorDescripcion(descripcion);

            if (dt != null)
            {
                dgvArticulos.DataSource = dt;
                // Cambiar nombre de las columnas si es necesario
                dgvArticulos.Columns[3].HeaderText = UserCache.Name != "InnovaGlass" ? "Precio" : "Costo";
            }
            else
            {
                MessageBox.Show("Error al cargar los artículos.");
            }
        }
    }
}
