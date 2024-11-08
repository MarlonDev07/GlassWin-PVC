﻿using Negocio.Company.AdmProyecto;
using Negocio.Company.FactProveedor;
using Negocio.Company.RegProveedor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio.Company;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Xml.Linq;
using Dominio.ClassFunction.InputBox;
using Negocio.Company.Account;
using Precentacion.User.DashBoard;
using System.Threading;
using System.Globalization;
using System.Net;


namespace Precentacion.User.AgregarFactura
{
    public partial class frmAgregarFacturaProveedor : MaterialSkin.Controls.MaterialForm
    {
        int IdProyecto;
        int IdProveedor;
        int IdFactura;
        int DiasVencimiento;
        decimal MontoAbonar;
        bool CargaProveedor = false;
        bool CargaProyecto = false;
        bool ColumnaVisible = false;
        bool Seleccionada = false;
        string Facturas = "Cancelada";
        string rutaImagen;
        bool fromButton = false;

        public frmAgregarFacturaProveedor()
        {
            InitializeComponent();
            CargarCombo();
            CargarDataGridPendiente();
            CalcularTotal();
            habilitaciones();

            dgvFacturas.RowsAdded += (s, args) => CalcularTotalVisible(dgvFacturas);
            dgvFacturas.RowsRemoved += (s, args) => CalcularTotalVisible(dgvFacturas);
            dgvFacturas.CellValueChanged += (s, args) => CalcularTotalVisible(dgvFacturas);
            dgvFacturas.DataError += (s, args) => CalcularTotalVisible(dgvFacturas);


        }
        private void dgvFacturas_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CalcularTotalVisible(dgvFacturas);
        }

        private void dgvFacturas_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CalcularTotalVisible(dgvFacturas);
        }

        private void dgvFacturas_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si la columna es "Monto" y recalcular
            if (dgvFacturas.Columns[e.ColumnIndex].HeaderText == "Monto")
            {
                CalcularTotalVisible(dgvFacturas);
            }
        }
        public void habilitaciones() {
            if (lblTitulo.Text == "Crear Factura" || lblTitulo.Text == "Registro de Factura de Compra ")
            {
                btnCrear.Enabled = true;
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
            }
            else if (lblTitulo.Text == "Editar Factura")
            {
                btnEditar.Enabled = true;
                btnEliminar.Enabled = false;
                btnCrear.Enabled = false;
            }
            else if (lblTitulo.Text == "Eliminar Factura")
            {
                btnEditar.Enabled = false;
                btnEliminar.Enabled = true;
                btnCrear.Enabled = false;
            }
        }

        #region Cargas Iniciales
        private void CargarDataGridPendiente()
        {
            try
            {
                LimpiarDataGrid();
                N_FactProveedor n_FactProveedor = new N_FactProveedor();
                DataTable dt = n_FactProveedor.ListaFacturasProveedorPendiente();
                //Cargar el DGV
                dgvFacturas.DataSource = dt;



                //Ocultar Columnas
                dgvFacturas.Columns[0].Visible = false;
                dgvFacturas.Columns[2].Visible = false;
                //dgvFacturas.Columns[7].Visible = false;
                // dgvFacturas.Columns[8].Visible = false;


                if (ColumnaVisible == true)
                {
                    MontoAbonar = 0;
                    //Agregar una columna de Check
                    DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                    chk.HeaderText = "Seleccionar";
                    chk.Name = "chk";
                    dgvFacturas.Columns.Insert(10, chk);
                    //Agregar un evento para el Check cada que se seleccione
                    dgvFacturas.CellContentClick += new DataGridViewCellEventHandler(dgvFacturas_CellContentClick);
                }

                //Cambiar el nombre de las columnas
                dgvFacturas.Columns[1].HeaderText = "Proveedor";
                dgvFacturas.Columns[3].HeaderText = "Número Fact";
                dgvFacturas.Columns[4].HeaderText = "Fecha Compra";
                dgvFacturas.Columns[5].HeaderText = "Fecha Vencimiento";
                dgvFacturas.Columns[6].HeaderText = "Monto";
                dgvFacturas.Columns[7].HeaderText = "PEV";
                dgvFacturas.Columns[8].HeaderText = "Bodega";
                dgvFacturas.Columns[9].HeaderText = "Factura";
               dgvFacturas.Columns[9].Visible = false;

                //Ocultar todas las Facturas que el Monto sea 0
                foreach (DataGridViewRow row in dgvFacturas.Rows)
                {
                    if (row.Cells[6].Value != null)
                    {
                        if (Convert.ToDecimal(row.Cells[6].Value) == 0.00M)
                        {
                            row.Visible = false;
                        }
                    }

                }

                //Ajustar el Ancho de las Columnas al ancho del DGV
                dgvFacturas.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                //Cambiar el Modo de Seleccion
                dgvFacturas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception)
            {

                throw;
            }
        }






        private void CargarDataGridCancelada()
        {
            try
            {
                LimpiarDataGrid();
                N_FactProveedor n_FactProveedor = new N_FactProveedor();
                DataTable dt = n_FactProveedor.ListaFacturasProveedorCancelada();
                //Cargar el DGV
                dgvFacturas.DataSource = dt;



                //Ocultar Columnas
                dgvFacturas.Columns[0].Visible = false;
                dgvFacturas.Columns[2].Visible = false;          

                //Cambiar el nombre de las columnas
                dgvFacturas.Columns[1].HeaderText = "Proveedor";
                dgvFacturas.Columns[3].HeaderText = "Numero Fact";
                dgvFacturas.Columns[4].HeaderText = "Fecha Compra";
                dgvFacturas.Columns[5].HeaderText = "Fecha Vencimiento";
                dgvFacturas.Columns[6].HeaderText = "Monto";
                dgvFacturas.Columns[7].HeaderText = "PEV";

                dgvFacturas.Columns[8].HeaderText = "Bodega";



                //Ajustar el Ancho de las Columnas al ancho del DGV
                dgvFacturas.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                //Cambiar el Modo de Seleccion
                dgvFacturas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void BuscarProveedor()
        {
            try
            {
                N_RegProveedor n_RegProveedor = new N_RegProveedor();
                DataTable dt = n_RegProveedor.BuscarProveedor(IdProveedor);

                //Obtener La Fecha de Vencimiento
                DiasVencimiento = Convert.ToInt32(dt.Rows[0]["DiasCredito"]);

                //Calcular la fecha de vencimiento Sumando los dias de credito al dtpFechaCompra
                dtpFechaVencimiento.Value = dtpFechaCompra.Value.AddDays(DiasVencimiento);

            }
            catch (Exception)
            {

                throw;
            }
        }
        private void CargarCombo()
        {
            try
            {
                N_RegProveedor n_RegProveedor = new N_RegProveedor();
                DataTable dt = n_RegProveedor.ListaProveedores();
                cbProveedor.DataSource = dt;
                cbProveedor.DisplayMember = "Nombre";
                cbProveedor.ValueMember = "IdProvedor";
                CargaProveedor = true;
                cbProveedor.SelectedIndex = 0;

                cbBuscarProveedor.DataSource = dt;
                cbBuscarProveedor.DisplayMember = "Nombre";
                cbBuscarProveedor.ValueMember = "IdProvedor";
                cbBuscarProveedor.SelectedIndex = 0;



                N_AdmProyecto n_AdmProyecto = new N_AdmProyecto();
                cbProyecto.DataSource = n_AdmProyecto.ListarNombresProyectos("Activo");
                cbProyecto.DisplayMember = "Proyecto";
                cbProyecto.ValueMember = "IdAdmProyecto";
                CargaProyecto = true;
                cbProyecto.SelectedIndex = 0;

            }
            catch (Exception)
            {

            }
        }
        private void CalcularTotal()
        {
            try
            {
                txtTotal.Text = "";
                decimal Total = 0;
                foreach (DataGridViewRow row in dgvFacturas.Rows)
                {
                    Total += Convert.ToDecimal(row.Cells[6].Value);
                }
                txtTotal.Text = Total.ToString("C");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Calcular el Total: " + ex.Message);
            }
        }

        #endregion

        #region Lista

        #endregion

        #region SelectIndexChange Combo
        private void cbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProveedor.SelectedValue != null && CargaProveedor == true)
            {
                //Obtener el id del proveedor
                IdProveedor = Convert.ToInt32(cbProveedor.SelectedValue);
                if (IdProveedor != 0)
                {
                    BuscarProveedor();
                }
            }


        }

        private void cbProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProyecto.SelectedValue != null && CargaProyecto == true)
            {
                //Obtener el id del proyecto
                IdProyecto = Convert.ToInt32(cbProyecto.SelectedValue);
            }
        }
        #endregion

        private void ReiniciarFormulario()
        {
            // Limpiar campos de texto
            txtMonto.Clear();
            txtNumFactura.Clear();
            txtPEV.Clear();
            txtBodega.Clear();

            // Reiniciar DateTimePickers a la fecha actual
            dtpFechaCompra.Value = DateTime.Now;
            dtpFechaVencimiento.Value = DateTime.Now;

            // Reiniciar el ComboBox de proveedores
            cbProveedor.SelectedIndex = -1;

            // Restablecer el título y botones
            lblTitulo.Text = "Crear Factura";
           btnCrear.Enabled = true;
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar si el PictureBox tiene una imagen
                if (pbAccesorioExclusivo.Image == null)
                {
                    MessageBox.Show("Debe agregar una imagen al accesorio exclusivo antes de guardar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (cbProveedor.Text == "Extralum")
                {
                    IdProveedor = 2;
                }
                else if (cbProveedor.Text == "Macopa")
                {
                    IdProveedor = 3;
                }
                else if (cbProveedor.Text == "Nelson Martinez Vargas")
                {
                    IdProveedor = 4;
                }
                else if (cbProveedor.Text == "Vidrios Rocha")
                {
                    IdProveedor = 5;
                }
                else if (cbProveedor.Text == "Extrusiones de Aluminio S.A.")
                {
                    IdProveedor = 11;
                }
                else if (cbProveedor.Text == "Bodega Dialex")
                {
                    IdProveedor = 8;
                }
                else if (cbProveedor.Text == "EXTRALUM Santo Domingo")
                {
                    IdProveedor = 12;
                }
                else if (cbProveedor.Text == "Instalaciones y Servicios Macopa S.A")
                {
                    IdProveedor = 13;
                }
                else if (cbProveedor.Text == "Carbone")
                {
                    IdProveedor = 14;
                }
                else
                {
                    MessageBox.Show("Proveedor no válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Reemplazar punto por coma en el texto de monto
                string montoTexto = txtMonto.Text.Replace('.', ',');

                // Validación del monto
                decimal montoTotal;
                if (!decimal.TryParse(montoTexto, NumberStyles.Number, CultureInfo.CurrentCulture, out montoTotal))
                {
                    MessageBox.Show("El monto ingresado no tiene el formato correcto. Asegúrese de ingresar un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Resto del código
                string imagePath = string.Empty;
                string Proyecto = cbProyecto.Text;
                N_FactProveedor n_FactProveedor = new N_FactProveedor();
                n_FactProveedor.InsertarFacturaProveedor(IdProveedor, dtpFechaCompra.Value, dtpFechaVencimiento.Value, montoTotal, txtNumFactura.Text, txtPEV.Text, txtBodega.Text, rutaImagen, Proyecto);

                // Código para insertar en la tabla Gastos...
                N_Gastos n_Gastos = new N_Gastos();
                n_Gastos.InsertarGastos(IdProyecto, dtpFechaCompra.Value, "Factura n°" + txtNumFactura.Text, montoTotal);

                CargarDataGridPendiente();
                MessageBox.Show("Factura Creada Correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Crear Factura: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Obtener los datos de la factura seleccionada y llenar los campos
            IdFactura = Convert.ToInt32(dgvFacturas.CurrentRow.Cells[2].Value);
            IdProveedor = Convert.ToInt32(dgvFacturas.CurrentRow.Cells[0].Value);
            dtpFechaCompra.Value = Convert.ToDateTime(dgvFacturas.CurrentRow.Cells[4].Value);
            dtpFechaVencimiento.Value = Convert.ToDateTime(dgvFacturas.CurrentRow.Cells[5].Value);
            txtMonto.Text = dgvFacturas.CurrentRow.Cells[6].Value.ToString();
            txtNumFactura.Text = dgvFacturas.CurrentRow.Cells[3].Value.ToString();
            txtPEV.Text = dgvFacturas.CurrentRow.Cells[7].Value.ToString();
            txtBodega.Text = dgvFacturas.CurrentRow.Cells[8].Value.ToString();
            cbProveedor.SelectedValue = IdProveedor;

            N_FactProveedor n_FactProveedor = new N_FactProveedor();
            string urlImagen = n_FactProveedor.obtenerURLFactura(IdFactura);
            rutaImagen = urlImagen;

            // Descargar el archivo desde el servidor FTP
            string localPath = DescargarArchivoDesdeFTP(urlImagen);

            // Cargar la imagen o el ícono de PDF en el PictureBox
            if (!string.IsNullOrEmpty(localPath) && File.Exists(localPath))
            {
                string fileExtension = Path.GetExtension(localPath).ToLower();

                if (fileExtension == ".pdf")
                {
                    // Cargar el ícono de PDF en lugar de una imagen
                    pbAccesorioExclusivo.Image = Properties.Resources.pdf_icon; // Asegúrate de que 'pdf_icon' exista en tus recursos
                    pbAccesorioExclusivo.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    // Cargar la imagen si no es un PDF
                    pbAccesorioExclusivo.Image = new Bitmap(localPath);
                    pbAccesorioExclusivo.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            else
            {
                pbAccesorioExclusivo.Image = null; // O una imagen por defecto
            }


            lblTitulo.Text = "Editar Factura";
            habilitaciones();

            tabControlPrincipal.SelectedTab = tabPageConsulta;
        }



        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar si el PictureBox tiene una imagen
                if (pbAccesorioExclusivo.Image == null)
                {
                    MessageBox.Show("Debe agregar una imagen al accesorio exclusivo antes de guardar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (cbProveedor.Text == "Extralum")
                {
                    IdProveedor = 2;
                }
                else if (cbProveedor.Text == "Macopa")
                {
                    IdProveedor = 3;
                }
                else if (cbProveedor.Text == "Nelson Martinez Vargas")
                {
                    IdProveedor = 4;
                }
                else if (cbProveedor.Text == "Vidrios Rocha")
                {
                    IdProveedor = 5;
                }
                else if (cbProveedor.Text == "Extrusiones de Aluminio S.A.")
                {
                    IdProveedor = 11;
                }
                else if (cbProveedor.Text == "Bodega Dialex")
                {
                    IdProveedor = 8;
                }
                else if (cbProveedor.Text == "EXTRALUM Santo Domingo")
                {
                    IdProveedor = 12;
                }
                else if (cbProveedor.Text == "Instalaciones y Servicios Macopa S.A")
                {
                    IdProveedor = 13;
                }
                else if (cbProveedor.Text == "Carbone")
                {
                    IdProveedor = 14;
                }
                else
                {
                    MessageBox.Show("Proveedor no válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Reemplazar punto por coma en el texto de monto
                string montoTexto = txtMonto.Text.Replace('.', ',');

                // Validación del monto
                decimal montoTotal;
                if (!decimal.TryParse(montoTexto, NumberStyles.Number, CultureInfo.CurrentCulture, out montoTotal))
                {
                    MessageBox.Show("El monto ingresado no tiene el formato correcto. Asegúrese de ingresar un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Actualizar la factura
                string Proycto = cbProyecto.Text;
                N_FactProveedor n_FactProveedor = new N_FactProveedor();
                n_FactProveedor.ActualizarFacturaProveedor(IdFactura, IdProveedor, dtpFechaCompra.Value, dtpFechaVencimiento.Value, montoTotal, txtNumFactura.Text, txtPEV.Text, txtBodega.Text, rutaImagen, Proycto);

                MessageBox.Show("Factura Editada Correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarDataGridPendiente();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Editar Factura: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Obtener los datos de la factura seleccionada y llenar los campos
            IdFactura = Convert.ToInt32(dgvFacturas.CurrentRow.Cells[2].Value);
            IdProveedor = Convert.ToInt32(dgvFacturas.CurrentRow.Cells[0].Value);
            dtpFechaCompra.Value = Convert.ToDateTime(dgvFacturas.CurrentRow.Cells[4].Value);
            dtpFechaVencimiento.Value = Convert.ToDateTime(dgvFacturas.CurrentRow.Cells[5].Value);
            txtMonto.Text = dgvFacturas.CurrentRow.Cells[6].Value.ToString();
            txtNumFactura.Text = dgvFacturas.CurrentRow.Cells[3].Value.ToString();
            txtPEV.Text = dgvFacturas.CurrentRow.Cells[7].Value.ToString();
            txtBodega.Text = dgvFacturas.CurrentRow.Cells[8].Value.ToString();
            cbProveedor.SelectedValue = IdProveedor;

            N_FactProveedor n_FactProveedor = new N_FactProveedor();
            string urlImagen = n_FactProveedor.obtenerURLFactura(IdFactura);
            rutaImagen = urlImagen;

            // Descargar el archivo desde el servidor FTP
            string localPath = DescargarArchivoDesdeFTP(urlImagen);

            // Cargar la imagen o el ícono de PDF en el PictureBox
            if (!string.IsNullOrEmpty(localPath) && File.Exists(localPath))
            {
                string fileExtension = Path.GetExtension(localPath).ToLower();

                if (fileExtension == ".pdf")
                {
                    // Cargar el ícono de PDF en lugar de una imagen
                    pbAccesorioExclusivo.Image = Properties.Resources.pdf_icon; // Asegúrate de que 'pdf_icon' exista en tus recursos
                    pbAccesorioExclusivo.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    // Cargar la imagen si no es un PDF
                    pbAccesorioExclusivo.Image = new Bitmap(localPath);
                    pbAccesorioExclusivo.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            else
            {
                pbAccesorioExclusivo.Image = null; // O una imagen por defecto
            }
            lblTitulo.Text = "Eliminar Factura";
            habilitaciones();
            btnCargarImagen.Visible = false;






            tabControlPrincipal.SelectedTab = tabPageConsulta;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                
                // Confirmar eliminación
                DialogResult result = MessageBox.Show("¿Estás seguro de eliminar la factura?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    return;
                }

                // Eliminar factura de la base de datos
                N_FactProveedor n_FactProveedor = new N_FactProveedor();
                n_FactProveedor.EliminarFacturaProveedor(IdFactura);
                MessageBox.Show("Eliminado con exito.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Intentar eliminar la imagen del sistema de archivos
                if (File.Exists(rutaImagen))
                {
                    bool eliminado = false;
                    int intentos = 0;
                    while (!eliminado && intentos < 5)
                    {
                        try
                        {
                            File.Delete(rutaImagen);
                            eliminado = true;
                        }
                        catch (IOException)
                        {
                            // Esperar antes de intentar de nuevo
                            Thread.Sleep(500);
                            intentos++;
                        } 
                    }

                    if (!eliminado)
                    {
                       // MessageBox.Show("No se pudo eliminar el archivo porque está siendo utilizado por otro proceso.");
                    }
                }

                // Recargar datos y reiniciar formulario
                CargarDataGridPendiente();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }




        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            dgvFacturas.CurrentCell = null;
            try
            {
                foreach (DataGridViewRow r in dgvFacturas.Rows)
                {
                    bool rowVisible = false;
                    foreach (DataGridViewCell c in r.Cells)
                    {
                        if (c.Value != null && c.Value.ToString().ToUpper().Contains(txtBuscar.Text.ToUpper()))
                        {
                            rowVisible = true;
                            CalcularTotalVisible(dgvFacturas);
                            break;
                        }
                    }
                    r.Visible = rowVisible;
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void cbBuscarProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProveedor.SelectedValue != null && CargaProveedor == true)
            {
                dgvFacturas.CurrentCell = null;
                try
                {
                    foreach (DataGridViewRow r in dgvFacturas.Rows)
                    {
                        bool rowVisible = false;
                        foreach (DataGridViewCell c in r.Cells)
                        {
                            if (c.Value != null && c.Value.ToString().ToUpper().Contains(cbBuscarProveedor.Text.ToUpper()))
                            {
                                rowVisible = true;
                                CalcularTotalVisible(dgvFacturas);
                                break;
                            }
                        }
                        r.Visible = rowVisible;
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void abonarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnDeposit_Click(null, null);
        }
        private void GeneratePdfCxC(string Deposit)
        {
            #region Crear el documento
            try
            {
                decimal AmountPending = Convert.ToDecimal(dgvFacturas.CurrentRow.Cells[6].Value);
                decimal NewAmountPending = AmountPending - Convert.ToDecimal(Deposit);


                // Obtener el directorio del escritorio y las carpetas necesarias
                string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string CarpetaFactura = Path.Combine(escritorio, "Abono a Proveedor");
                string carpetaNombre = Path.Combine(CarpetaFactura, dgvFacturas.CurrentRow.Cells[1].Value.ToString().Trim());
                string NameFile = "Factura° " + dgvFacturas.CurrentRow.Cells[3].Value.ToString().Trim() + ".pdf";

                // Verificar si la carpeta "Proformas" existe, si no, crearla
                if (!Directory.Exists(CarpetaFactura))
                {
                    Directory.CreateDirectory(CarpetaFactura);
                }

                // Verificar si la carpeta con el nombre existe, si no, crearla
                if (!Directory.Exists(carpetaNombre))
                {
                    Directory.CreateDirectory(carpetaNombre);
                }

                // Crear la ruta completa del archivo PDF
                string rutaArchivoPDF = Path.Combine(carpetaNombre, NameFile);

                Document document = new Document(PageSize.LETTER); // Cambia el tamaño de la hoja a tamaño carta
                document.SetMargins(36, 36, 36, 36); // Establece los márgenes a 36 puntos

                // Crea un nuevo objeto PdfWriter para escribir el documento en un archivo
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(rutaArchivoPDF, FileMode.Create));

                // Configura el writer para centrar el contenido
                writer.PageEvent = new PdfPageEventHelper();

                // Asigna el objeto PdfWriter al documento
                document.Open();
                #endregion

                #region Encabezado

                // Crea un nuevo objeto Font para los textos
                iTextSharp.text.Font titleFont = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 19, iTextSharp.text.Font.BOLD, BaseColor.GRAY);
                iTextSharp.text.Font textFont = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font TextSubrayado = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 19, iTextSharp.text.Font.UNDERLINE, BaseColor.GRAY);


                PdfPTable Encabezado = new PdfPTable(2);
                Encabezado.WidthPercentage = 120;


                // Agrega la imagen a la primera celda
                string rutaLogo = "";
                if (CompanyCache.IdCompany == 3101794685)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\RioClaroLogo.jpeg";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 111560456)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\DialexLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 31028013)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\InnovaLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 1230123)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\GlassWinLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 31025820)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\AluviLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 205150849)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\MakyLogo.png";
                    rutaLogo = ruta + Url;
                }
                if (CompanyCache.IdCompany == 112540885)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VidriosAlturaLogo.png";
                    rutaLogo = ruta + Url;
                }
                if (CompanyCache.IdCompany == 25550555)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VitroLogo.png";
                    rutaLogo = ruta + Url;

                }
                PdfPCell imageCell = new PdfPCell(iTextSharp.text.Image.GetInstance(rutaLogo));
                imageCell.Border = PdfPCell.NO_BORDER;
                imageCell.FixedHeight = 120f; // Ajusta la altura de la imagen
                imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
                Encabezado.AddCell(imageCell);

                // Agrega los textos a la segunda celda
                PdfPCell textCell = new PdfPCell();
                textCell.Border = PdfPCell.NO_BORDER;

                // Alinea el contenido de la celda al centro
                textCell.HorizontalAlignment = Element.ALIGN_RIGHT;

                // Agrega el párrafo y los chunks al documento
                Paragraph paragraph = new Paragraph();
                paragraph.Add(Chunk.NEWLINE);// Salto de línea
                paragraph.Add(Chunk.NEWLINE);// Salto de línea
                paragraph.Add(Chunk.NEWLINE);// Salto de línea
                paragraph.Add(new Chunk(CompanyCache.Name, titleFont));
                paragraph.Add(Chunk.NEWLINE);// Salto de línea
                paragraph.Add(new Chunk(""));
                paragraph.Add(Chunk.NEWLINE);
                paragraph.Add(new Chunk(""));
                paragraph.Add(Chunk.NEWLINE);
                paragraph.Add(new Chunk(""));
                paragraph.Add(Chunk.NEWLINE);
                paragraph.Add(Chunk.NEWLINE);// Salto de línea


                textCell.AddElement(paragraph);
                Encabezado.AddCell(textCell);

                // Establece el ancho de la celda de la tabla (ajusta según tus necesidades)
                Encabezado.SetWidths(new float[] { 3f, 4f }); // Primer valor es el ancho de la celda de la imagen

                // Agrega la tabla al documento
                document.Add(Encabezado);

                Paragraph encabezadoParagraph = new Paragraph();
                encabezadoParagraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add(Chunk.NEWLINE);// Salto de línea
                paragraph.Add(Chunk.NEWLINE);// Salto de línea
                // Agregar un título
                Chunk tituloChunk = new Chunk("Comprobante", TextSubrayado);
                encabezadoParagraph.Add(tituloChunk);
                encabezadoParagraph.Add(Chunk.NEWLINE); // Salto de línea

                // Agregar un subtítulo


                Chunk subtituloChunk = new Chunk("Número de Factura: " + dgvFacturas.CurrentRow.Cells[3].Value.ToString().Trim(), textFont);
                encabezadoParagraph.Add(subtituloChunk);
                encabezadoParagraph.Add(Chunk.NEWLINE); // Salto de línea

                document.Add(encabezadoParagraph);
                document.Add(new Paragraph(" ")); // Espacio en blanco

                //Agregar Fecha y hora de la Factura Alineado a la Derecha
                Paragraph fechaHoraParagraph = new Paragraph();
                fechaHoraParagraph.Alignment = Element.ALIGN_RIGHT;
                fechaHoraParagraph.Add(new Chunk($"Hora: {DateTime.Now.ToString("HH:mm:ss")}   Fecha: {DateTime.Now.ToString("dd/MM/yyyy")}", textFont));
                document.Add(fechaHoraParagraph);
                document.Add(new Paragraph(" ")); // Espacio en blanco
                #endregion

                #region Datos del Abono y Cliente
                // Agrega el párrafo y los chunks al documento
                Paragraph paragraphh = new Paragraph();
                paragraphh.Add(new Chunk($"Hemos Pagado a {dgvFacturas.CurrentRow.Cells[1].Value.ToString().Trim()}  ", textFont));
                paragraphh.Add(new Chunk($"el Monto de {Deposit} ", textFont));
                paragraphh.Add(new Chunk($"por Concepto de Adelanto o Cancelacion de la Factura N° {dgvFacturas.CurrentRow.Cells[3].Value.ToString().Trim()}", textFont));
                paragraphh.Add(Chunk.NEWLINE);
                paragraphh.Add(Chunk.NEWLINE);
                //Agregar Los montos Anterior Abono y saldo Actual
                paragraphh.Add(new Chunk($"Monto Anterior: {AmountPending.ToString("c")} ", textFont));
                paragraphh.Add(Chunk.NEWLINE);
                paragraphh.Add(new Chunk($"Monto Abonado: {Deposit} ", textFont));
                paragraphh.Add(Chunk.NEWLINE);
                paragraphh.Add(new Chunk($"Saldo Actual: {NewAmountPending} ", textFont));
                paragraphh.Add(Chunk.NEWLINE);


                //Agregar el parrafo al documento
                document.Add(paragraphh);


                #endregion

                #region Cerrar el documento
                // Cerrar el documento
                document.Close();
            }
            catch (Exception)
            {

            }
            #endregion
        }
        private void btnDeposit_Click(object sender, EventArgs e)
        {
            try
            {
                //Preguntar si desea realizar el deposito
                DialogResult result = MessageBox.Show("¿Desea realizar el deposito a la factura: " + dgvFacturas.CurrentRow.Cells[3].Value.ToString() + "?", "Depositar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    //Preguntar el monto a depositar
                    string Result = InputBox.Show("Ingrese el Monto que se va Abonar", "Abonar a la Cuenta");
                    if (Result != "")
                    {
                        decimal Amount = Convert.ToDecimal(Result);
                        if (Amount > 0)
                        {
                            int IdAccount = Convert.ToInt32(dgvFacturas.CurrentRow.Cells[2].Value);
                            decimal OutstandingBalance = Convert.ToDecimal(dgvFacturas.CurrentRow.Cells[6].Value);
                            decimal NewOutstandingBalance = OutstandingBalance - Amount;
                            if (NewOutstandingBalance >= 0)
                            {
                                N_FactProveedor n_FactProveedor = new N_FactProveedor();
                                if (n_FactProveedor.ActualizaSaldo(IdAccount, NewOutstandingBalance.ToString()))
                                {
                                    MessageBox.Show("Se realizo el deposito correctamente", "Deposito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    GeneratePdfCxC(Result.ToString());
                                    if (ColumnaVisible = !true)
                                    {
                                        CargarDataGridPendiente();
                                        CalcularTotal();
                                    }
                                    else
                                    {
                                        CalcularTotal();
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("El monto a depositar no puede ser mayor al balance pendiente", "Deposito", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("El monto a depositar debe ser mayor a 0", "Deposito", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MontoAbonar = 0;
                ColumnaVisible = true;
                CargarDataGridPendiente();
                btnAbonarLista.Visible = true;
                btnCancel.Visible = true;
                txtTotalAbonar.Visible = true;
                txtTotalAbonar.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAbonarLista_Click(object sender, EventArgs e)
        {
            try
            {
                //Recorrer el DataGrid Verificando si esta seleccionado el Check
                foreach (DataGridViewRow row in dgvFacturas.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[7].Value) == true)
                    {
                        //Seleccionar la fila
                        dgvFacturas.CurrentCell = row.Cells[1];
                        abonarToolStripMenuItem_Click(null, null);
                    }
                }
                ColumnaVisible = false;
                CargarDataGridPendiente();
                CalcularTotal();
                btnAbonarLista.Visible = false;
                btnCancel.Visible = false;
                txtTotalAbonar.Visible = false;
                dgvFacturas.CellContentClick -= new DataGridViewCellEventHandler(dgvFacturas_CellContentClick);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MontoAbonar = 0;
            ColumnaVisible = false;
            CargarDataGridPendiente();
            btnAbonarLista.Visible = false;
            btnCancel.Visible = false;
            txtTotalAbonar.Visible = false;
            //Quitar el Evento del DataGrid
            dgvFacturas.CellContentClick -= new DataGridViewCellEventHandler(dgvFacturas_CellContentClick);

        }

        private void LimpiarDataGrid()
        {
            //Limpiar el DataGrid dejandolo sin datos ni columnas
            dgvFacturas.DataSource = null;
            dgvFacturas.Columns.Clear();

        }

        private decimal CalcularTotalVisible(DataGridView dgv)
        {
            decimal total = 0;
            string nombreColumna = "Monto";

            // Iterar sobre cada fila en el DataGridView
            foreach (DataGridViewRow fila in dgv.Rows)
            {
                // Verificar si la fila está visible
                if (fila.Visible)
                {
                    // Obtener el valor de la celda en la columna especificada
                    if (fila.Cells[nombreColumna].Value != null && decimal.TryParse(fila.Cells[nombreColumna].Value.ToString(), out decimal valor))
                    {
                        // Sumar el valor al total
                        total += valor;
                    }
                }
            }

            // Actualizar el TextBox con el total en formato de moneda
            txtTotal.Text = total.ToString("C");

            return total;
        }


        private void btnCancelBuscar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            cbBuscarProveedor.SelectedIndex = 0;

            CargarDataGridPendiente();
            CalcularTotal();
        }

        private void dgvFacturas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                // Verificar si la columna seleccionada es la del Check
                if (e.ColumnIndex == 10 && e.RowIndex != -1) // Asegurarse de que no sea la fila de encabezado
                {
                    DataGridViewCheckBoxCell chkCell = dgvFacturas.Rows[e.RowIndex].Cells[10] as DataGridViewCheckBoxCell;

                    if (chkCell != null)
                    {
                        bool isChecked = (bool)chkCell.EditedFormattedValue; // Obtener el valor actualizado de la celda

                        decimal valorFila = Convert.ToDecimal(dgvFacturas.Rows[e.RowIndex].Cells[6].Value);

                        if (isChecked)
                        {
                            // Sumar el valor de la fila al MontoAbonar
                            MontoAbonar += valorFila;
                            Seleccionada = true;
                        }
                        else
                        {
                            // Restar el valor de la fila del MontoAbonar
                            MontoAbonar -= valorFila;
                            Seleccionada = false;
                        }

                        // Actualizar el texto en el textbox con el nuevo MontoAbonar formateado
                        txtTotalAbonar.Text = MontoAbonar.ToString("C");

                        // Actualizar el valor de la celda de verificación si es necesario (puede ser redundante)
                      
                    }
                }
        }

        private void btnFactCanceladas_Click(object sender, EventArgs e)
        {
            if (Facturas == "Cancelada")
            {
                btnFactCanceladas.Text = "Fact.Pendientes";
                CargarDataGridCancelada();
                Facturas = "Pendiente";
                txtTotal.Text = "0";
            }
            else
            {
                btnFactCanceladas.Text = "Fact.Canceladas";
                CargarDataGridPendiente();
                CalcularTotal();
                Facturas = "Cancelada";
            }
           
        }

        private void frmAgregarFacturaProveedor_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmDashUser frm = frmDashUser.Instance;
            frm.WindowState = FormWindowState.Normal;
            frm.Show();
            frm.BringToFront();
            
        }

        private void txtNumFactura_TextChanged(object sender, EventArgs e)
        {
           /* if (txtNumFactura.Text == "" && btnEditar.Visible == true)
            {
                btnEditar.Visible = false;
                btnCrear.Visible = true;
            }*/
        }

        private void btnCargarImagen_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = "c:\\";
                    openFileDialog.Filter = "Archivos de imagen y PDF (*.jpg, *.jpeg, *.png, *.jfif, *.pdf) | *.jpg; *.jpeg; *.png; *.jfif; *.pdf";
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Obtener la ruta del archivo seleccionada
                        string filePath = openFileDialog.FileName;
                        string fileExtension = Path.GetExtension(filePath).ToLower();

                        if (fileExtension == ".pdf")
                        {
                            // Si es un archivo PDF, podrías mostrar un icono genérico
                            pbAccesorioExclusivo.Image = Properties.Resources.pdf_icon; // Asegúrate de tener un ícono de PDF en tus recursos.
                            pbAccesorioExclusivo.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                        else
                        {
                            // Si es una imagen, cargarla en el PictureBox.
                            pbAccesorioExclusivo.Image = new Bitmap(filePath);
                            pbAccesorioExclusivo.SizeMode = PictureBoxSizeMode.StretchImage;
                        }

                        // Guardar el archivo localmente en la carpeta "Facturas"
                        string rutaGuardada = GuardarArchivo(filePath);

                        // Subir el archivo guardado al servidor FTP
                        if (rutaGuardada != null)
                        {
                            List<string> nombres = new List<string> { Path.GetFileName(rutaGuardada) };
                            List<string> rutas = new List<string> { rutaGuardada };

                            SubirImagenesAFtp(nombres, rutas);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al cargar el archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void SubirImagenesAFtp(List<string> nombres, List<string> rutas)
        {
            //string ftpUrl = "ftp://138.59.135.48:2121//Imagenes/";
            //string ftpUsuario = "FtpUserMakyDG";
            //string ftpPassword = "MakyDG0192";

            // URL del servidor FTP y nombre de usuario/contraseña
            string ftpUrl = "ftp://138.59.135.48//Archivos/";
            string ftpUsuario = "ftpUser";
            string ftpPassword = "GlassWinFTP0192";

            using (WebClient client = new WebClient())
            {
                // Autenticación en el servidor FTP
                client.Credentials = new NetworkCredential(ftpUsuario, ftpPassword);

                for (int i = 0; i < nombres.Count; i++)
                {
                    string nombreArchivo = nombres[i];
                    string rutaArchivo = rutas[i];

                    // URL completa donde se subirá la imagen o archivo
                    string ftpFullUrl = ftpUrl + nombreArchivo;
                    rutaImagen = ftpFullUrl;


                    try
                    {
                        // Subir el archivo al servidor FTP
                        client.UploadFile(ftpFullUrl, WebRequestMethods.Ftp.UploadFile, rutaArchivo);
                        Console.WriteLine("Archivo subido correctamente: " + nombreArchivo);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al subir el archivo " + nombreArchivo + ": " + ex.Message);
                    }
                }
            }
        }

        private string GuardarArchivo(string sourcePath)
        {
            try
            {
                // Obtener la ruta de la carpeta Documentos del usuario
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                // Crear la ruta para la carpeta "Facturas" dentro de Documentos
                string directoryPath = Path.Combine(documentsPath, "Facturas");
                Directory.CreateDirectory(directoryPath); // Crear el directorio si no existe

                // Obtener el nombre del archivo de la ruta de origen
                string fileName = Path.GetFileName(sourcePath);
                // Crear la ruta de destino en la carpeta "Facturas"
                string destinationPath = Path.Combine(directoryPath, fileName);

                // Verificar si ya existe un archivo con el mismo nombre
                if (File.Exists(destinationPath))
                {
                    // Si existe, agregar un número secuencial al nombre del archivo
                    int count = 1;
                    string fileNameOnly = Path.GetFileNameWithoutExtension(fileName);
                    string extension = Path.GetExtension(fileName);

                    while (File.Exists(destinationPath))
                    {
                        string tempFileName = $"{fileNameOnly}({count++})";
                        destinationPath = Path.Combine(directoryPath, tempFileName + extension);
                    }
                }

                // Copiar el archivo seleccionado al directorio de destino
                File.Copy(sourcePath, destinationPath, true);

                // Retornar la ruta donde se guardó el archivo
                //rutaImagen = destinationPath;
                return destinationPath;
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción y devolver un mensaje de error o manejar el error según se necesite
                MessageBox.Show($"Ocurrió un error al guardar el archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private string GuardarImagen(string sourcePath)
        {
            // Obtener la ruta de la carpeta Documentos del usuario
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            // Crear la ruta para la carpeta "Facturas" dentro de Documentos
            string directoryPath = Path.Combine(documentsPath, "Facturas");
            Directory.CreateDirectory(directoryPath); // Crear el directorio si no existe

            // Obtener el nombre del archivo de la ruta de origen
            string fileName = Path.GetFileName(sourcePath);
            // Crear la ruta de destino en la carpeta "Facturas"
            string destinationPath = Path.Combine(directoryPath, fileName);

            // Copiar el archivo seleccionado al directorio de destino
            File.Copy(sourcePath, destinationPath, true);
            rutaImagen = destinationPath;

            return destinationPath;
        }

        private void verToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Obtener los datos de la factura seleccionada y llenar los campos
            IdFactura = Convert.ToInt32(dgvFacturas.CurrentRow.Cells[2].Value);
            IdProveedor = Convert.ToInt32(dgvFacturas.CurrentRow.Cells[0].Value);
            dtpFechaCompra.Value = Convert.ToDateTime(dgvFacturas.CurrentRow.Cells[4].Value);
            dtpFechaVencimiento.Value = Convert.ToDateTime(dgvFacturas.CurrentRow.Cells[5].Value);
            txtMonto.Text = dgvFacturas.CurrentRow.Cells[6].Value.ToString();
            txtNumFactura.Text = dgvFacturas.CurrentRow.Cells[3].Value.ToString();
            txtPEV.Text = dgvFacturas.CurrentRow.Cells[7].Value.ToString();
            txtBodega.Text = dgvFacturas.CurrentRow.Cells[8].Value.ToString();
            cbProveedor.SelectedValue = IdProveedor;

            N_FactProveedor n_FactProveedor = new N_FactProveedor();
            string urlImagen = n_FactProveedor.obtenerURLFactura(IdFactura);
            rutaImagen = urlImagen;

            // Descargar el archivo desde el servidor FTP
            string localPath = DescargarArchivoDesdeFTP(urlImagen);

            // Cargar la imagen o el ícono de PDF en el PictureBox
            if (!string.IsNullOrEmpty(localPath) && File.Exists(localPath))
            {
                string fileExtension = Path.GetExtension(localPath).ToLower();

                if (fileExtension == ".pdf")
                {
                    // Cargar el ícono de PDF en lugar de una imagen
                    pbAccesorioExclusivo.Image = Properties.Resources.pdf_icon; // Asegúrate de que 'pdf_icon' exista en tus recursos
                    pbAccesorioExclusivo.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    // Cargar la imagen si no es un PDF
                    pbAccesorioExclusivo.Image = new Bitmap(localPath);
                    pbAccesorioExclusivo.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            else
            {
                pbAccesorioExclusivo.Image = null; // O una imagen por defecto
            }

            lblTitulo.Text = "Ver Factura";

            btnCrear.Visible = false;
            btnEditar.Visible = false;
            btnEliminar.Visible = false;
            btnCargarImagen.Visible = false;

            cbProveedor.Enabled = false;
            txtNumFactura.Enabled = false;
            dtpFechaCompra.Enabled = false;
            cbProyecto.Enabled = false;
            txtPEV.Enabled = false;
            txtMonto.Enabled = false;
            dtpFechaVencimiento.Enabled = false;
            txtBodega.Enabled = false;
            btnBack.Visible = true;

            tabControlPrincipal.SelectedTab = tabPageConsulta;
            CargarDataGridPendiente();
        }

        private string DescargarArchivoDesdeFTP(string remoteUrl)
        {
            try
            {
                string ftpUrl = "ftp://138.59.135.48//Archivos/";
                string ftpUsuario = "ftpUser";
                string ftpPassword = "GlassWinFTP0192";

                // Crear el WebClient para la descarga
                using (WebClient request = new WebClient())
                {
                    // Establecer las credenciales
                    request.Credentials = new NetworkCredential(ftpUsuario, ftpPassword);

                    // Generar una ruta temporal para almacenar el archivo descargado
                    string localPath = Path.Combine(Path.GetTempPath(), Path.GetFileName(remoteUrl));

                    // Descargar el archivo desde el servidor FTP
                    request.DownloadFile(remoteUrl, localPath);

                    return localPath; // Retornar la ruta local donde se descargó el archivo
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al descargar el archivo desde FTP: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void nuevaToolStripMenuItem_Click(object sender, EventArgs e)
        {
           /*ReiniciarFormulario();
           tabControlPrincipal.SelectedTab = tabPageConsulta;*/
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ReiniciarFormulario();
            tabControlPrincipal.SelectedTab = tabPageConsulta;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            fromButton = true;
            // Limpiar campos de texto
            txtMonto.Clear();
            txtNumFactura.Clear();
            txtPEV.Clear();
            txtBodega.Clear();
            pbAccesorioExclusivo.Image = null;
            dtpFechaCompra.Value = DateTime.Now;
            dtpFechaVencimiento.Value = DateTime.Now;
            cbProveedor.Enabled = true;
            txtNumFactura.Enabled = true;
            dtpFechaCompra.Enabled = true;
            cbProyecto.Enabled = true;
            txtPEV.Enabled = true;
            txtMonto.Enabled = true;
            dtpFechaVencimiento.Enabled = true;
            txtBodega.Enabled = true;
            btnBack.Visible = false;

            btnCrear.Visible = true;
            btnEditar.Visible = true;
            btnEliminar.Visible = true;
            btnCargarImagen.Visible = true;
            btnBack.Visible = true;


            // Reiniciar DateTimePickers a la fecha actual
            dtpFechaCompra.Value = DateTime.Now;
            dtpFechaVencimiento.Value = DateTime.Now;

            // Reiniciar el ComboBox de proveedores
            cbProveedor.SelectedIndex = -1;

            // Restablecer el título y botones
            lblTitulo.Text = "Crear Factura";
            habilitaciones();
            tabControlPrincipal.SelectedTab = tabPageLista;
            fromButton = false;
        }

        private void tabControlPrincipal_Selecting(object sender, TabControlCancelEventArgs e)
        {
            // Verifica si la pestaña seleccionada es la pestaña de lista (tabPageLista)
            if (e.TabPage == tabPageLista)
            {
                // Solo muestra el mensaje si el cambio no es debido al botón
                if (!fromButton &&
                    (lblTitulo.Text == "Registro de Factura de Compra" ||
                     lblTitulo.Text == "Crear Factura" ||
                     lblTitulo.Text == "Editar Factura" ||
                     lblTitulo.Text == "Eliminar Factura" ||
                     lblTitulo.Text == "Ver Factura"))
                {
                    // Cancela el cambio de pestaña y muestra el mensaje
                    e.Cancel = true;
                    MessageBox.Show("No puede regresar a la pestaña de lista desde aquí.");
                }
            }
        }

        private void pbAccesorioExclusivo_Click(object sender, EventArgs e)
        {
            if (rutaImagen != null && Path.GetExtension(rutaImagen).ToLower() == ".pdf")
            {
                // El archivo es un PDF, descargarlo si es necesario y luego abrirlo
                try
                {
                    string localFilePath = DescargarArchivoDesdeFTP2(rutaImagen); // Descarga si es necesario

                    if (!string.IsNullOrEmpty(localFilePath) && File.Exists(localFilePath))
                    {
                        // Abrir el archivo PDF con el visor predeterminado
                        System.Diagnostics.Process.Start(localFilePath);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo encontrar o descargar el archivo PDF.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo abrir el archivo PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Si no es un PDF, mostrar la imagen en el formulario de visor de imágenes
                using (frmImageViewer viewer = new frmImageViewer())
                {
                    // Establece la imagen del PictureBox del formulario principal en el nuevo formulario
                    viewer.ImageToDisplay = pbAccesorioExclusivo.Image;

                    // Muestra el nuevo formulario
                    viewer.ShowDialog();
                }
            }
        }

        // Método para descargar el archivo desde el servidor FTP si no está disponible localmente
        private string DescargarArchivoDesdeFTP2(string remoteUrl)
        {
            try
            {
                string ftpUrl = "ftp://138.59.135.48//Archivos/";
                string ftpUsuario = "ftpUser";
                string ftpPassword = "GlassWinFTP0192";

                // Ruta local donde se descargará el archivo
                string localPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Facturas", Path.GetFileName(remoteUrl));

                // Verificar si el archivo ya existe localmente
                if (File.Exists(localPath))
                {
                    return localPath; // Ya existe, no es necesario descargar
                }

                // Descargar el archivo desde el servidor FTP
                using (WebClient client = new WebClient())
                {
                    client.Credentials = new NetworkCredential(ftpUsuario, ftpPassword);
                    client.DownloadFile(remoteUrl, localPath);
                }

                return localPath; // Devolver la ruta local del archivo descargado
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al descargar el archivo desde el servidor FTP: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }


        private void dgvFacturas_RowsAdded_1(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CalcularTotalVisible(dgvFacturas);
        }

        private void dgvFacturas_RowsRemoved_1(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CalcularTotalVisible(dgvFacturas);
        }
    }
}
