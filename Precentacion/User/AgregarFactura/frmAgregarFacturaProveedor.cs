using Negocio.Company.AdmProyecto;
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
        public frmAgregarFacturaProveedor()
        {
            InitializeComponent();
            CargarCombo();
            CargarDataGridPendiente();
            CalcularTotal();
            habilitaciones();


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
                dgvFacturas.Columns[3].HeaderText = "Numero Fact";
                dgvFacturas.Columns[4].HeaderText = "Fecha Compra";
                dgvFacturas.Columns[5].HeaderText = "Fecha Vencimiento";
                dgvFacturas.Columns[6].HeaderText = "Monto";
                dgvFacturas.Columns[7].HeaderText = "PDV";
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
                dgvFacturas.Columns[7].HeaderText = "PDV";

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
                if (cbProveedor.Text == "Extralum")
                {
                    IdProveedor = 2;
                }
                if (cbProveedor.Text == "Macopa")
                {
                    IdProveedor = 3;
                }
                if (cbProveedor.Text == "Nelson Martinez Vargas")
                {
                    IdProveedor = 4;
                }
                if (cbProveedor.Text == "Vidrios Rocha")
                {
                    IdProveedor = 5;
                }

                string imagePath = string.Empty;

                N_FactProveedor n_FactProveedor = new N_FactProveedor();
                n_FactProveedor.InsertarFacturaProveedor(IdProveedor, dtpFechaCompra.Value, dtpFechaVencimiento.Value, txtMonto.Text, txtNumFactura.Text, txtPEV.Text, txtBodega.Text, rutaImagen);

                // Código para insertar en la tabla Gastos...
                N_Gastos n_Gastos = new N_Gastos();
                n_Gastos.InsertarGastos(IdProyecto, dtpFechaCompra.Value, "Factura n°" + txtNumFactura.Text, Convert.ToDecimal(txtMonto.Text));

                CargarDataGridPendiente();
                MessageBox.Show("Factura Creada Correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reiniciar el formulario después de crear una nueva factura
                ReiniciarFormulario();
                tabControlPrincipal.SelectedTab = tabPageLista;
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

            // Cargar la imagen en el PictureBox
            if (!string.IsNullOrEmpty(urlImagen) && File.Exists(urlImagen))
            {
                pbAccesorioExclusivo.Image = new Bitmap(urlImagen);
                pbAccesorioExclusivo.SizeMode = PictureBoxSizeMode.StretchImage;
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
                // Obtener la ruta de la imagen actual antes de la edición
               // string rutaImagenActual = ObtenerRutaImagenActual(IdFactura); // Método que obtiene la ruta de la imagen actual desde la base de datos

                if (cbProveedor.Text == "Extralum")
                {
                    IdProveedor = 2;
                }
                if (cbProveedor.Text == "Macopa")
                {
                    IdProveedor = 3;
                }
                if (cbProveedor.Text == "Nelson Martinez Vargas")
                {
                    IdProveedor = 4;
                }
                if (cbProveedor.Text == "Vidrios Rocha")
                {
                    IdProveedor = 5;
                }

                string imagePath = string.Empty;

               

                N_FactProveedor n_FactProveedor = new N_FactProveedor();
                n_FactProveedor.ActualizarFacturaProveedor(IdFactura, IdProveedor, dtpFechaCompra.Value, dtpFechaVencimiento.Value, txtMonto.Text, txtNumFactura.Text, txtPEV.Text, txtBodega.Text, rutaImagen);

                MessageBox.Show("Factura Editada Correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarDataGridPendiente();
                tabControlPrincipal.SelectedTab = tabPageLista;

                // Reiniciar el formulario después de editar una factura
                ReiniciarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

            // Cargar la imagen en el PictureBox
            if (!string.IsNullOrEmpty(urlImagen) && File.Exists(urlImagen))
            {
                pbAccesorioExclusivo.Image = new Bitmap(urlImagen);
                pbAccesorioExclusivo.SizeMode = PictureBoxSizeMode.StretchImage;
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
                            Thread.Sleep(1000);
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
                ReiniciarFormulario();
                tabControlPrincipal.SelectedTab = tabPageLista;
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
            txtTotal.Text = "";
            decimal total = 0;
            string nombreColumna = "Monto";

            // Iterar sobre cada fila visible en el DataGridView
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
                    openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Obtener la ruta del archivo seleccionada
                        string filePath = openFileDialog.FileName;
                        // Mostrar la imagen en el PictureBox
                        pbAccesorioExclusivo.Image = new Bitmap(filePath);
                        pbAccesorioExclusivo.SizeMode = PictureBoxSizeMode.StretchImage;
                        GuardarImagen(openFileDialog.FileName);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un error al cargar la imagen", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

    }
}
