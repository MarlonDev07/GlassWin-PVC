﻿using Dominio.Model.ClasscmbArticulo;
using Dominio.Model.ClassComboArticulos;
using Dominio.Model.ClassWindows;
using iTextSharp.text.pdf;
using Negocio;
using Negocio.Accesorios;
using Negocio.Company;
using Negocio.LoadProduct;
using Precentacion.User.Quote.Accesorios;
using Precentacion.User.Quote.Quote;
using Precentacion.User.Quote.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using zPersonalizacion;

namespace Precentacion.User.Quote.Prefabricado
{
    public partial class frmPrefabricado : MaterialSkin.Controls.MaterialForm
    {
        private string UrlImagen = "";
        private bool Inicializado = false;
        private decimal GranTotal = 0;
        private bool Editar = false;
        private String IdCombo = "0";
        
        public frmPrefabricado()
        {       
            InitializeComponent();
            ConfigDataGrid();
            ConfigMaterialSkin();
            pbImagen.AllowDrop = true;
        }
        #region Carga Inicial
        private void ConfigDataGrid()
        {
            try
            {
                // Ajustar el ancho de las columnas al ancho del DataGrid
                dgvPrefabricado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvPrefabricado.AutoGenerateColumns = false;
                dgvPrefabricado.ColumnCount = 10; // Cambiar a 9 para agregar la columna de Descuento

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

                dgvPrefabricado.Columns[7].HeaderText = "Descuento"; // Nueva columna para Descuento
                dgvPrefabricado.Columns[7].DataPropertyName = "Descuento"; // Cambiar DataPropertyName si es necesario

                dgvPrefabricado.Columns[8].HeaderText = "PrecioTotal";
                dgvPrefabricado.Columns[8].DataPropertyName = "PrecioTotal";

                dgvPrefabricado.Columns[9].HeaderText = "Color";
                dgvPrefabricado.Columns[9].DataPropertyName = "Color";

                // Cambiar el DisplayIndex de la columna "Color" para que aparezca después de "Nombre"
                dgvPrefabricado.Columns[9].DisplayIndex = 2;

                // Agregar una línea en blanco
                dgvPrefabricado.Rows.Add();
                dgvPrefabricado.Rows[0].Cells[0].Value = "";
                dgvPrefabricado.Rows[0].Cells[1].Value = "";
                dgvPrefabricado.Rows[0].Cells[2].Value = "0";
                dgvPrefabricado.Rows[0].Cells[3].Value = "0";
                dgvPrefabricado.Rows[0].Cells[4].Value = "0";
                dgvPrefabricado.Rows[0].Cells[5].Value = "0";
                dgvPrefabricado.Rows[0].Cells[6].Value = "0";
                dgvPrefabricado.Rows[0].Cells[7].Value = "0"; // Nueva celda para Descuento
                dgvPrefabricado.Rows[0].Cells[8].Value = "0";
                dgvPrefabricado.Rows[0].Cells[9].Value = "";

                // Inicializar el total a 0
                txtTotalCMB.Text = "0.00"; // Inicializar con cero

                // Ocultar la columna de ID si es necesario
                // dgvPrefabricado.Columns[0].Visible = false;

                Inicializado = true;
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }

        public void ActualizarTotalPrecio()
        {
            decimal totalPrecio = 0;

            foreach (DataGridViewRow row in dgvPrefabricado.Rows)
            {
                // Asegurarse de que la celda de PrecioTotal no sea null y convertir a decimal
                if (row.Cells[8].Value != null && decimal.TryParse(row.Cells[8].Value.ToString(), out decimal precioTotal))
                {
                    totalPrecio += precioTotal; // Sumar el PrecioTotal
                }
            }
            GranTotal = totalPrecio + cls_ComboArticulos.Precio;
            txtTotalCMB.Text =  totalPrecio.ToString("c"); // Mostrar total con 2 decimales
        }

        public void ConfigMaterialSkin()
        {
            MaterialSkin.MaterialSkinManager materialSkinManager = MaterialSkin.MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.LIGHT;
            //Colores anaranjados
            materialSkinManager.ColorScheme = new MaterialSkin.ColorScheme(MaterialSkin.Primary.Orange800, MaterialSkin.Primary.Orange900, MaterialSkin.Primary.Orange500, MaterialSkin.Accent.Orange700, MaterialSkin.TextShade.WHITE);

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
            // Validar que se presiona la tecla F2
            if (e.KeyValue == (char)Keys.F2)
            {
                // Abrir la Lista de Artículos
                frmListArticulos frm = new frmListArticulos();
                frm.StartPosition = FormStartPosition.CenterParent; // Centrar el formulario hijo en el formulario padre
                frm.Show(this); // Establecer el formulario principal como el propietario
            }

            //Validar que se Preciona la Tecla Enter
            if (e.KeyValue == (char)Keys.Enter)
            {
                //Cargar los Datos del Producto
                BuscarProducto(Convert.ToInt32(dgvPrefabricado.CurrentRow.Cells[0].Value), 0);
            }
        }
        private void dgvPrefabricado_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Inicializado)
                {
                    // Verificar si la celda de ID ha cambiado
                    if (dgvPrefabricado.CurrentRow.Cells[0].Value != null && !string.IsNullOrEmpty(dgvPrefabricado.CurrentRow.Cells[0].Value.ToString()) &&
                    dgvPrefabricado.CurrentRow.Cells[0].Value.ToString() != IdCombo)
                    {
                        IdCombo = dgvPrefabricado.CurrentRow.Cells[0].Value.ToString();
                        if (ValidarCampos(IdCombo))
                        {
                            BuscarProducto(Convert.ToInt32(IdCombo), 0);
                        }
                    }

                    // Validar las Celdas Alto, Ancho y Cantidad
                    if (dgvPrefabricado.CurrentRow.Cells[2].Value != null &&
                        dgvPrefabricado.CurrentRow.Cells[3].Value != null &&
                        dgvPrefabricado.CurrentRow.Cells[4].Value != null &&
                        dgvPrefabricado.CurrentRow.Cells[6].Value != null) // Agregar validación para Precio
                    {
                        // Validar celdas
                        if (ValidarCeldas(dgvPrefabricado.CurrentRow.Cells[3].Value.ToString(),
                                          dgvPrefabricado.CurrentRow.Cells[2].Value.ToString(),
                                          dgvPrefabricado.CurrentRow.Cells[4].Value.ToString()))
                        {
                            // Calcular el Metraje
                            decimal Metraje = Convert.ToDecimal(dgvPrefabricado.CurrentRow.Cells[2].Value) *
                                              Convert.ToDecimal(dgvPrefabricado.CurrentRow.Cells[3].Value) *
                                              Convert.ToDecimal(dgvPrefabricado.CurrentRow.Cells[4].Value);
                            dgvPrefabricado.CurrentRow.Cells[5].Value = Metraje;

                            // Calcular el Precio Total
                            decimal PrecioUnitario = Convert.ToDecimal(dgvPrefabricado.CurrentRow.Cells[6].Value);
                            decimal Descuento = Convert.ToDecimal(dgvPrefabricado.CurrentRow.Cells[7].Value); // Columna Descuento
                            decimal PrecioTotalSinDescuento = PrecioUnitario * Metraje;
                            decimal PrecioTotalConDescuento = PrecioTotalSinDescuento - (PrecioTotalSinDescuento * (Descuento / 100));
                            PrecioTotalConDescuento = Math.Round(PrecioTotalConDescuento, 2); // Redondear a 2 decimales
                            dgvPrefabricado.CurrentRow.Cells[8].Value = PrecioTotalConDescuento; // Columna Precio Total

                            // Llamar al método para actualizar el total
                            ActualizarTotalPrecio();
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
            //Validar que se Preciona la Tecla Enter

        }
        private bool ValidarCampos(string Campo)
        {
            bool Resultado = false;
            //Validar que el Campo sean solo Numeros, Comas y Puntos
            if (System.Text.RegularExpressions.Regex.IsMatch(Campo, @"^[0-9\.,]*$"))
            {
                //Validar si el Campo Contiene Puntos y Remplaarlos por Comas
                if (Campo.Contains("."))
                {
                    Campo = Campo.Replace(".", ",");
                    dgvPrefabricado.CurrentRow.Cells[0].Value = Campo;
                    return true;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                MessageBox.Show("Solo se Permiten Numeros");
                Resultado = false;
            }

            return Resultado;
        }
        private bool ValidarCeldas(string Alto, string Ancho, string Cantidad)
        {
            //Validar que el Campo sean solo Numeros, Comas y Puntos
            bool Resultado = false;
            if (System.Text.RegularExpressions.Regex.IsMatch(Alto, @"^[0-9\.,]*$") && System.Text.RegularExpressions.Regex.IsMatch(Ancho, @"^[0-9\.,]*$") && System.Text.RegularExpressions.Regex.IsMatch(Cantidad, @"^[0-9\.,]*$"))
            {
                //Validar si el Campo Contiene Puntos y Remplaarlos por Comas
                if (Alto.Contains("."))
                {
                    Alto = Alto.Replace(".", ",");
                    dgvPrefabricado.CurrentRow.Cells[3].Value = Alto;
                    Resultado = true;
                }
                else
                {
                    Resultado = true;
                }
                if (Ancho.Contains("."))
                {
                    Ancho = Ancho.Replace(".", ",");
                    dgvPrefabricado.CurrentRow.Cells[2].Value = Ancho;
                    Resultado = true;
                }
                else
                {
                    Resultado = true;
                }
                if (Cantidad.Contains("."))
                {
                    Cantidad = Cantidad.Replace(".", ",");
                    dgvPrefabricado.CurrentRow.Cells[4].Value = Cantidad;
                    Resultado = true;
                }
                else
                {
                    Resultado = true;
                }
            }
            else
            {
                MessageBox.Show("Solo se Permiten Numeros");
                Resultado = false;
            }
            return Resultado;
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
                LN_ComboPrefabricado comboPrefabricado = new LN_ComboPrefabricado();
                N_LoadProduct loadProduct = new N_LoadProduct();
                string errorMessage = "";
                if (loadProduct.insertWindows("Prefabricado \n"+txtDescripcion.Text,UrlImagen,0,0,"","","",GranTotal,ClsWindows.IDQuote,"Prefabricado","",out errorMessage))
                {
                    foreach (DataGridViewRow row in dgvPrefabricado.Rows)
                    {
                        if (row.Cells[0].Value != null)
                        {
                            if (row.Cells[0].Value.ToString() != "")
                            {
                                cls_ComboArticulos.idPrice = Convert.ToInt32(row.Cells[0].Value);

                                if (comboPrefabricado.InsertarCombo(cls_ComboArticulos.idPrice, cls_ComboArticulos.IdWindows,Convert.ToInt32(row.Cells[5].Value), Convert.ToDecimal(row.Cells[4].Value)))
                                {
                                    GwMessageBox.Show("Datos Guardados Correctamente", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is frmQuote);
                                    if (frm != null)
                                    {
                                        ((frmQuote)frm).loadWindows();
                                    }
                                    this.Close();
                                }
                                else
                                {
                                    GwMessageBox.Show("Error al Guardar los Datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
                else
                {
                    GwMessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                }               
            }
            catch (Exception EX)
            {
                MessageBox.Show("Error al Guardar los Datos: " + EX);
            }
        }
        public void ListarArticulos(List<Cls_CmbArticulo> List)
        {
             if (!dgvPrefabricado.Columns.Contains("IdVentana"))
             {
                dgvPrefabricado.Columns.Add("IdVentana", "IdVentana");
             }

                // Desactivar temporalmente los eventos para evitar problemas
                dgvPrefabricado.CellValueChanged -= dgvPrefabricado_CellValueChanged;

                // Recorrer la Lista de Articulos
                foreach (Cls_CmbArticulo item in List)
                {
                    // Agregar una nueva fila
                    int newRowIndex = dgvPrefabricado.Rows.Add();

                    // Seleccionar la nueva fila
                    DataGridViewRow newRow = dgvPrefabricado.Rows[newRowIndex];
                    dgvPrefabricado.Rows[newRowIndex].Selected = true;  // Seleccionar la fila recién agregada

                    // Obtener los Datos de la Descripción
                    string[] Datos = ObtenerDatosDescripcion(item.Descripcion);

                    // Asignar los Datos a la fila
                    newRow.Cells[0].Value = Convert.ToInt32(Datos[0]); // Id
                    newRow.Cells[1].Value = Datos[1]; // Descripción
                    newRow.Cells[2].Value = Datos[2]; // Alto
                    newRow.Cells[3].Value = Datos[3]; // Ancho
                    newRow.Cells[4].Value = Datos[4]; // Cantidad
                    newRow.Cells[7].Value = Datos[5]; // Descuento
                    BuscarProducto(Convert.ToInt32(Datos[0]), 1); // Llamada a BuscarProducto
                    newRow.Cells[8].Value = item.Precio; // Precio
                    newRow.Cells[9].Value = Datos[6]; // Descuento
                    newRow.Cells[10].Value = item.IdVentana; // IdVentana
                }

                // Si necesitas eliminar la primera fila:
                if (dgvPrefabricado.Rows.Count > 1)
                {
                    dgvPrefabricado.Rows.RemoveAt(0);
                }

                // Reactivar el evento después de agregar todas las filas
                dgvPrefabricado.CellValueChanged += dgvPrefabricado_CellValueChanged;

                // Forzar el enfoque en el DataGridView
                dgvPrefabricado.Focus();

                // Si quieres asegurarte de que la fila recién seleccionada se muestre como activa
                dgvPrefabricado.CurrentCell = dgvPrefabricado.Rows[dgvPrefabricado.Rows.Count - 1].Cells[0]; // Asegúrate de que la última fila agregada sea la actual
        }
        public string[] ObtenerDatosDescripcion(string Descripcion)
        {
            string[] Datos = new string[7]; // Crear un array de 5 elementos para ID, Nombre, Ancho, Alto y Cantidad

            // Expresión regular para capturar ID
            string pattern = @"ID:\s*(\d+)";
            System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(Descripcion, pattern);
            int Id = Convert.ToInt32(match.Groups[1].Value);

            // Expresión regular para capturar Nombre
            string pattern1 = @"\nNombre:\s*(.*)";
            System.Text.RegularExpressions.Match match1 = System.Text.RegularExpressions.Regex.Match(Descripcion, pattern1);
            string Nombre = match1.Groups[1].Value;

            // Expresión regular para capturar Ancho, permitiendo valores decimales con comas o puntos
            string pattern2 = @"\nAncho:\s*([\d.,]+)";
            System.Text.RegularExpressions.Match match2 = System.Text.RegularExpressions.Regex.Match(Descripcion, pattern2);
            string Ancho = match2.Groups[1].Value;

            // Expresión regular para capturar Alto, permitiendo valores decimales con comas o puntos
            string pattern3 = @"\nAlto:\s*([\d.,]+)";
            System.Text.RegularExpressions.Match match3 = System.Text.RegularExpressions.Regex.Match(Descripcion, pattern3);
            string Alto = match3.Groups[1].Value;

            // Expresión regular para capturar Cantidad
            string pattern4 = @"\nCantidad:\s*(\d+)";
            System.Text.RegularExpressions.Match match4 = System.Text.RegularExpressions.Regex.Match(Descripcion, pattern4);
            int Cantidad = Convert.ToInt32(match4.Groups[1].Value);

            // Expresión regular para capturar Cantidad
            string pattern5 = @"\nDescuento:\s*(\d+)";
            System.Text.RegularExpressions.Match match5 = System.Text.RegularExpressions.Regex.Match(Descripcion, pattern5);
            int Descuento = Convert.ToInt32(match5.Groups[1].Value);

            // Expresión regular para capturar Nombre
            string pattern6 = @"\nColor:\s*(.*)";
            System.Text.RegularExpressions.Match match6= System.Text.RegularExpressions.Regex.Match(Descripcion, pattern6);
            string Color = match6.Groups[1].Value;

            Datos[0] = Id.ToString();
            Datos[1] = Nombre;
            Datos[2] = Ancho; // Reemplazar comas por puntos si es necesario
            Datos[3] = Alto;  // Reemplazar comas por puntos si es necesario
            Datos[4] = Cantidad.ToString();
            Datos[5] = Descuento.ToString();
            Datos[6] = Color;

            return Datos;
        }
        public void BuscarProducto(int Id, int Seleccion)
        {
            DataTable dataTable = new DataTable();
            N_LoadProduct n_LoadProduct = new N_LoadProduct();
            dataTable = n_LoadProduct.ListaArticulosxID(Id);

            // Asegúrate de que hay una fila seleccionada o actual
            if (dgvPrefabricado.CurrentRow != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    if (Seleccion == 0)
                    {
                        // Asignar valores desde la DataTable
                        dgvPrefabricado.CurrentRow.Cells[0].Value = dataTable.Rows[0]["IdPrice"].ToString();
                        dgvPrefabricado.CurrentRow.Cells[1].Value = dataTable.Rows[0]["Description"].ToString();

                        // Asignar precio dependiendo del usuario
                        if (UserCache.Name == "InnovaGlass")
                        {
                            dgvPrefabricado.CurrentRow.Cells[6].Value = dataTable.Rows[0]["Cost"].ToString();
                        }
                        else
                        {
                            dgvPrefabricado.CurrentRow.Cells[6].Value = dataTable.Rows[0]["SalePrice"].ToString();
                        }

                        // Después de cargar los datos, llamamos a AgregarFila si es necesario
                        AgregarFila();
                    }
                    else if (Seleccion == 1)
                    {
                        if (UserCache.Name == "InnovaGlass")
                        {
                            dgvPrefabricado.CurrentRow.Cells[6].Value = dataTable.Rows[0]["Cost"].ToString();
                        }
                        else
                        {
                            dgvPrefabricado.CurrentRow.Cells[6].Value = dataTable.Rows[0]["SalePrice"].ToString();
                        }
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show("El Articulo no Existe, Desea Abrir la Lista?", "Articulo no Encontrado", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        frmListArticulos frm = new frmListArticulos();
                        frm.Show();
                    }
                }
            }
            else
            {
                // Manejar el caso en que CurrentRow es null
                //MessageBox.Show("No hay ninguna fila seleccionada en el DataGridView.");
            }
        }
        public void ConfigEditar()
        {
            btnGuardar.Text = "Editar Combo";
            Editar = true;
        }
        public void LimpiarCampos()
        {
            dgvPrefabricado.Rows.Clear();
            ConfigDataGrid();
            Editar = false;
            btnGuardar.Text = "Guardar Combo";
        }
        private void frmPrefabricado_Load(object sender, EventArgs e)
        {

        }

        //Funciones para Agregar una imagen arrastrandolo al PBCombo y Guardar la Ruta
        private void pbImagen_DragEnter(object sender, DragEventArgs e)
        {
            // Verifica si el elemento arrastrado es un archivo
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy; // Permite copiar el archivo
            }
            else
            {
                e.Effect = DragDropEffects.None; // No permite arrastrar otros tipos de datos
            }
        }

        private void pbImagen_DragDrop(object sender, DragEventArgs e)
        {
            // Obtiene el archivo arrastrado
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                UrlImagen = files[0];
                pbImagen.BackgroundImage = Image.FromFile(UrlImagen);
                pbImagen.BackgroundImageLayout = ImageLayout.Stretch; // Ajusta la imagen al tamaño del PictureBox
            }
        }

        private void btnAgregarCombo_Click(object sender, EventArgs e)
        {
            cls_ComboArticulos.Abierto = true;
            frmSelectSystem frm = new frmSelectSystem();
            frm.Show();
        }
    }
}
