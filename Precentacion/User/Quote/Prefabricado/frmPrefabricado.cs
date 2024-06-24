using Dominio.Model.ClasscmbArticulo;
using Dominio.Model.ClassWindows;
using iTextSharp.text.pdf;
using Negocio;
using Negocio.Accesorios;
using Negocio.LoadProduct;
using Precentacion.User.Quote.Accesorios;
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
    public partial class frmPrefabricado : MaterialSkin.Controls.MaterialForm
    {
        private string UrlImagen = "";
        private bool Inicializado = false;
        private decimal GranTotal = 0;
        private bool Editar = false;
        public frmPrefabricado()
        {
            InitializeComponent();
            ConfigDataGrid();
            
        }
        #region Carga Inicial
        private void ConfigDataGrid() 
        {
            try
            {
                //Ajustar el ancho de las columnas al ancho del datagrid
                dgvPrefabricado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvPrefabricado.AutoGenerateColumns = false;
                dgvPrefabricado.ColumnCount = 8;
                dgvPrefabricado.Columns[0].HeaderText = "ID";
                dgvPrefabricado.Columns[0].DataPropertyName = "IdPrefabricado";

                dgvPrefabricado.Columns[1].HeaderText = "Nombre";
                dgvPrefabricado.Columns[1].DataPropertyName = "Nombre";

                dgvPrefabricado.Columns[2].HeaderText = "Ancho";
                dgvPrefabricado.Columns[2].DataPropertyName = "Ancho";

                dgvPrefabricado.Columns[3].HeaderText = "Alto";
                dgvPrefabricado.Columns[3].DataPropertyName = "Alto";

                dgvPrefabricado.Columns[4].HeaderText = "Cantidad";
                dgvPrefabricado.Columns[4].DataPropertyName = "Cantidad";

                dgvPrefabricado.Columns[5].HeaderText = "Metraje";
                dgvPrefabricado.Columns[5].DataPropertyName = "Metraje";

                dgvPrefabricado.Columns[6].HeaderText = "Precio";
                dgvPrefabricado.Columns[6].DataPropertyName = "Precio";

                dgvPrefabricado.Columns[7].HeaderText = "PrecioTotal";
                dgvPrefabricado.Columns[7].DataPropertyName = "PrecioTotal";

                //Agregar Una Linea en Blanco
                dgvPrefabricado.Rows.Add();
                dgvPrefabricado.Rows[0].Cells[0].Value = "";
                dgvPrefabricado.Rows[0].Cells[1].Value = "";
                dgvPrefabricado.Rows[0].Cells[2].Value = "0";
                dgvPrefabricado.Rows[0].Cells[3].Value = "0";
                dgvPrefabricado.Rows[0].Cells[4].Value = "0";
                dgvPrefabricado.Rows[0].Cells[5].Value = "0";
                dgvPrefabricado.Rows[0].Cells[6].Value = "0";
                dgvPrefabricado.Rows[0].Cells[7].Value = "0";

                //Ocultar la Columna de ID

                Inicializado = true;
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
            
           

        }
        #endregion

        private void btnCargar_Click(object sender, EventArgs e)
        {
            //Abrir la Lista de Artiulos
            frmListArticulos frm = new frmListArticulos();
            frm.Show();
        }
        private void dgvPrefabricado_KeyDown(object sender, KeyEventArgs e)
        {
            //Validar que se Preciona la Tecla F2
            if (e.KeyValue == (char)Keys.F2)
            {
                //Abrir la Lista de Artiulos
                frmListArticulos frm = new frmListArticulos();
                frm.Show();
            }           
        }
        private void dgvPrefabricado_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Inicializado)
                {
                    //Calcular el Metraje
                    decimal Metraje = Convert.ToDecimal(dgvPrefabricado.CurrentRow.Cells[2].Value) * Convert.ToDecimal(dgvPrefabricado.CurrentRow.Cells[3].Value) * Convert.ToDecimal(dgvPrefabricado.CurrentRow.Cells[4].Value);
                    dgvPrefabricado.CurrentRow.Cells[5].Value = Metraje;

                    //Calcular el Precio Total
                    decimal Precio = Convert.ToDecimal(dgvPrefabricado.CurrentRow.Cells[6].Value);
                    decimal PrecioTotal = Metraje * Precio;
                    dgvPrefabricado.CurrentRow.Cells[7].Value = PrecioTotal;


                    //Calcular el Gran Total
                    GranTotal = 0;
                    foreach (DataGridViewRow row in dgvPrefabricado.Rows)
                    {
                        if (row.Cells[7].Value != null)
                        {
                            GranTotal += Convert.ToDecimal(row.Cells[7].Value);
                        }
                    }
                }
                
            }
            catch (Exception EX)
            {
                MessageBox.Show("Error al Cargar el Articulo: " + EX);
            }
        }
        private void frmPrefabricado_KeyDown(object sender, KeyEventArgs e)
        {
           


           
        }
        public void ConfigEditar() 
        {
            btnGuardar.Text = "Editar Combo";
            Editar = true;
        }
        public void AgregarFila() 
        {
            //Agregar una fila nueva
            dgvPrefabricado.Rows.Add();

            // Obtener el índice de la última fila agregada
            int indiceUltimaFila = dgvPrefabricado.Rows.Count - 2;

            //Seleccionar la última fila agregada
            dgvPrefabricado.CurrentCell = dgvPrefabricado.Rows[indiceUltimaFila].Cells[0];
        }
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Eliminar la Fila Seleccionada
            dgvPrefabricado.Rows.Remove(dgvPrefabricado.CurrentRow);


        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Recorrer el DataGrid y Guardar los Datos
            try
            {
                N_LoadProduct n_LoadProduct = new N_LoadProduct();            
                    foreach (DataGridViewRow row in dgvPrefabricado.Rows)
                    {
                        if (row.Cells[0].Value != null)
                        {
                            if (row.Cells[0].Value.ToString() != "")
                            {
                                //Crear Descripcion Con Salto de Linea
                                string description = "";
                                description += "ID: " + row.Cells[0].Value.ToString() + "\n";
                                description += "Nombre: " + row.Cells[1].Value.ToString() + "\n";
                                description += "Ancho: " + row.Cells[2].Value.ToString() + "\n";
                                description += "Alto: " + row.Cells[3].Value.ToString() + "\n";
                                description += "Cantidad: " + row.Cells[4].Value.ToString() + "\n";
                                description += "cmbArticulo";
 

                            if (Editar)
                            {
                                n_LoadProduct.EditWindows(row.Cells[8].Value.ToString(), description, "", Convert.ToDecimal(row.Cells[2].Value), Convert.ToDecimal(row.Cells[3].Value), "", "", "", Convert.ToDecimal(row.Cells[7].Value), ClsWindows.IDQuote, "cmbArticulos", "");
                            }
                            else
                            {
                                n_LoadProduct.insertWindows(description, "", Convert.ToDecimal(row.Cells[2].Value), Convert.ToDecimal(row.Cells[3].Value), "", "", "", Convert.ToDecimal(row.Cells[7].Value), ClsWindows.IDQuote, "cmbArticulos", "");
                            }

                            }
                        }
                    }
                    Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is frmQuote);
                    if (frm != null)
                    {
                        ((frmQuote)frm).loadWindows();
                    }
                if (Editar)
                {
                    MessageBox.Show("Combo Editado Correctamente");
                }
                else
                {
                    MessageBox.Show("Combo Guardado Correctamente");
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show("Error al Guardar los Datos: " + EX);
            }
        }
        public void ListarArticulos(List<Cls_CmbArticulo> List)
        {
            //Agregar una Columna nueva al dgvPrefabricado con el IdVentana
            dgvPrefabricado.Columns.Add("IdVentana", "IdVentana");
            //Recorrer la Lista de Articulos
            foreach (Cls_CmbArticulo item in List)
            {
                //Agregar una fila nueva
                dgvPrefabricado.Rows.Add();

                // Obtener el índice de la última fila agregada
                int indiceUltimaFila = dgvPrefabricado.Rows.Count - 2;

                //Seleccionar la última fila agregada
                dgvPrefabricado.CurrentCell = dgvPrefabricado.Rows[indiceUltimaFila].Cells[0];

                //Obtener los Datos de la Descripcion
                string[] Datos = ObtenerDatosDescripcion(item.Descripcion);

                //Asignar los Datos a la Fila
                dgvPrefabricado.CurrentRow.Cells[0].Value = Datos[0];
                dgvPrefabricado.CurrentRow.Cells[1].Value = Datos[1];
                dgvPrefabricado.CurrentRow.Cells[2].Value = Datos[2];
                dgvPrefabricado.CurrentRow.Cells[3].Value = Datos[3];
                dgvPrefabricado.CurrentRow.Cells[4].Value = Datos[4];
                dgvPrefabricado.CurrentRow.Cells[6].Value = item.Precio;
                dgvPrefabricado.CurrentRow.Cells[7].Value = item.Precio;
                dgvPrefabricado.CurrentRow.Cells[8].Value = item.IdVentana;
            }
            //Borrar el Index 0
            dgvPrefabricado.Rows.RemoveAt(0);
        }
        public string[] ObtenerDatosDescripcion(string Descripcion)
        {
            string[] Datos = new string[5]; // Crear un array de 5 elementos para ID, Nombre, Ancho, Alto y Cantidad

            string pattern = @"ID:\s*(\d+)";
            System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(Descripcion, pattern);
            int Id = Convert.ToInt32(match.Groups[1].Value);

            string pattern1 = @"\nNombre:\s*(.*)";
            System.Text.RegularExpressions.Match match1 = System.Text.RegularExpressions.Regex.Match(Descripcion, pattern1);
            string Nombre = match1.Groups[1].Value;

            string pattern2 = @"\nAncho:\s*(\d+)";
            System.Text.RegularExpressions.Match match2 = System.Text.RegularExpressions.Regex.Match(Descripcion, pattern2);
            int Ancho = Convert.ToInt32(match2.Groups[1].Value);

            string pattern3 = @"\nAlto:\s*(\d+)";
            System.Text.RegularExpressions.Match match3 = System.Text.RegularExpressions.Regex.Match(Descripcion, pattern3);
            int Alto = Convert.ToInt32(match3.Groups[1].Value);

            string pattern4 = @"\nCantidad:\s*(\d+)";
            System.Text.RegularExpressions.Match match4 = System.Text.RegularExpressions.Regex.Match(Descripcion, pattern4);
            int Cantidad = Convert.ToInt32(match4.Groups[1].Value);

            Datos[0] = Id.ToString();
            Datos[1] = Nombre;
            Datos[2] = Ancho.ToString();
            Datos[3] = Alto.ToString();
            Datos[4] = Cantidad.ToString();

            return Datos;
        }
        private void LimpiarCambios() 
        {
            //Limpiar el DataGrid
            dgvPrefabricado.Rows.Clear();
            ConfigDataGrid();
            //Limpiar el Gran Total
            GranTotal = 0;
        }
    }
}
