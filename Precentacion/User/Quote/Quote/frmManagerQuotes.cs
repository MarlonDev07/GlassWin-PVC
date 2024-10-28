using Negocio.Company.Account;
using Negocio.Company.Bill;
using Negocio.Company.Quote;
using Precentacion.User.Bill;
using Precentacion.User.DashBoard;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using MaterialSkin.Controls;
using System.Drawing;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace Precentacion.User.Quote.Quote
{
    public partial class frmManagerQuotes : MaterialForm
    {
        #region Variables
        bool EventClose = true;
        string Busqueda = "Pending";
        N_Quote NQuote = new N_Quote();
        N_Bill N_Bill = new N_Bill();
        N_CxC N_CxC = new N_CxC();
        #endregion

        #region Constructor
        public frmManagerQuotes()
        {
            InitializeComponent();
            frmManagerQuotes_Load(null, null);
            QuoteUI.loadMaterial(this);
         
            // Suscribe el evento RowPrePaint
            dgvQuotes.RowPrePaint += new DataGridViewRowPrePaintEventHandler(dgvQuotes_RowPrePaint);
        }
        #endregion

        #region Load Functions
        public void frmManagerQuotes_Load(object sender, EventArgs e)
        {
            DataTable dataTable = NQuote.LoadQuotes();
            if (dataTable != null)
            {
                dgvQuotes.DataSource = dataTable;
                //Hacer Invisibles la Columna [0]
                dgvQuotes.Columns[0].Visible = false;

                //Cambiar Nombre de las Columnas
                dgvQuotes.Columns[1].HeaderText = "N° Proforma";
                dgvQuotes.Columns[2].HeaderText = "Fecha";
                dgvQuotes.Columns[3].HeaderText = "Nombre del Cliente";
                dgvQuotes.Columns[4].HeaderText = "Nombre del Proyecto";
                dgvQuotes.Columns[5].HeaderText = "Dirección";
                dgvQuotes.Columns[6].HeaderText = "Subtotal";
                dgvQuotes.Columns[7].HeaderText = "Impuesto";
                dgvQuotes.Columns[8].HeaderText = "Total";

                //Ajustar las columnas al Ancho del formulario
                dgvQuotes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Configura el color de la fila seleccionada
                dgvQuotes.DefaultCellStyle.SelectionBackColor = Color.Green;
                dgvQuotes.DefaultCellStyle.SelectionForeColor = Color.Green;
                CargarTotales();

                // Controlar visibilidad del eliminarToolStripMenuItem
                if (btnVerEstadosProforma.Text == "Ver Facturas" || this.Text == "Proformas")
                {
                    eliminarToolStripMenuItem.Visible = true;
                    eliminarFacturaToolStripMenuItem.Visible = false;
                }
                else
                {
                    eliminarToolStripMenuItem.Visible = false;
                    eliminarFacturaToolStripMenuItem.Visible = true;
                }
            }
        }
        #endregion

        #region RowPrePaint Event Handler
        private void dgvQuotes_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
            {
                dgvQuotes.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
            }
            else
            {
                dgvQuotes.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Orange;
            }
        }
        #endregion

        #region Button Context Menu Strip
        private void editarProformaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("¿Desea editar la proforma n° " + dgvQuotes.CurrentRow.Cells[1].Value.ToString() + "?", "Editar Proforma", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {

                    frmQuote frmQuote = new frmQuote();
                    frmQuote.idQuoteVerificacion = Convert.ToInt32(dgvQuotes.CurrentRow.Cells[1].Value);
                    frmQuote.txtidClient.Text = dgvQuotes.CurrentRow.Cells[0].Value.ToString();
                    frmQuote.btnBuscar_Click(null, null);
                    frmQuote.txtProjetName.Text = dgvQuotes.CurrentRow.Cells[4].Value.ToString();
                    frmQuote.txtAddress.Text = dgvQuotes.CurrentRow.Cells[5].Value.ToString();

                    // Asignar el valor del total editado
                    frmQuote.precioTotalEdit = Convert.ToDecimal(dgvQuotes.CurrentRow.Cells[8].Value);
                    frmQuote.txtTotal.Text = frmQuote.precioTotalEdit.ToString("c");

                    frmQuote.txtidQuote.Text = dgvQuotes.CurrentRow.Cells[1].Value.ToString();
                    frmQuote.label4.Text = "Edicion de Proforma";



                    frmQuote.Edit = true;
                    frmQuote.EventClose = false;
                    EventClose = false;
                    frmQuote.LoadDataQuote();
                    frmQuote.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void cotizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventClose = false;
            frmQuote frmQuote = new frmQuote();
            frmQuote.Show();
            this.Close();
        }

        private void facturarProformaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Validar si el formulario frmFacturar esta abierto
            frmFacturar frm = Application.OpenForms.OfType<frmFacturar>().FirstOrDefault();
            if (frm != null)
            {
                ((frmFacturar)frm).txtidClient.Text = dgvQuotes.CurrentRow.Cells[0].Value.ToString();
                ((frmFacturar)frm).txtidQuote.Text = dgvQuotes.CurrentRow.Cells[1].Value.ToString();
                ((frmFacturar)frm).txtProjetName.Text = dgvQuotes.CurrentRow.Cells[4].Value.ToString();
                ((frmFacturar)frm).txtAddress.Text = dgvQuotes.CurrentRow.Cells[5].Value.ToString();
                ((frmFacturar)frm).Subtotal = Convert.ToDecimal(dgvQuotes.CurrentRow.Cells[6].Value.ToString());
                ((frmFacturar)frm).IVA = Convert.ToDecimal(dgvQuotes.CurrentRow.Cells[7].Value.ToString());
                ((frmFacturar)frm).Total = Convert.ToDecimal(dgvQuotes.CurrentRow.Cells[8].Value.ToString());
                ((frmFacturar)frm).btnBuscar_Click(null, null);
                ((frmFacturar)frm).InitializeComponents_Click(null, null);
                EventClose = false;
            }
            else
            {
                frmFacturar frmFacturar = new frmFacturar();
                frmFacturar.txtidClient.Text = dgvQuotes.CurrentRow.Cells[0].Value.ToString();
                frmFacturar.txtidQuote.Text = dgvQuotes.CurrentRow.Cells[1].Value.ToString();
                frmFacturar.txtProjetName.Text = dgvQuotes.CurrentRow.Cells[4].Value.ToString();
                frmFacturar.txtAddress.Text = dgvQuotes.CurrentRow.Cells[5].Value.ToString();
                frmFacturar.Subtotal = Convert.ToDecimal(dgvQuotes.CurrentRow.Cells[6].Value.ToString());
                frmFacturar.IVA = Convert.ToDecimal(dgvQuotes.CurrentRow.Cells[7].Value.ToString());
                frmFacturar.Total = Convert.ToDecimal(dgvQuotes.CurrentRow.Cells[8].Value.ToString());

                frmFacturar.btnBuscar_Click(null, null);
                frmFacturar.InitializeComponents_Click(null, null);
                frmFacturar.Show();
                EventClose = false;
                this.Close();
            }
        }
        #endregion

        private void frmManagerQuotes_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmDashUser frm = frmDashUser.Instance;
            if (EventClose)
            {
                frm.WindowState = FormWindowState.Normal;
                frm.Show();
                frm.BringToFront();
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            dgvQuotes.CurrentCell = null;
            try
            {
                foreach (DataGridViewRow r in dgvQuotes.Rows)
                {
                    bool rowVisible = false;
                    foreach (DataGridViewCell c in r.Cells)
                    {
                        if (c.Value != null && c.Value.ToString().ToUpper().Contains(txtBuscar.Text.ToUpper()))
                        {
                            rowVisible = true;
                            break;
                        }
                    }
                    r.Visible = rowVisible;
                }
            }
            catch (Exception ex)
            {
                // Manejar excepción
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {
            // Método vacío, posiblemente no necesario
        }
        private void dgvQuotes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Método vacío, posiblemente no necesario
        }

        private void btnVerEstadosProforma_Click(object sender, EventArgs e)
        {
            if (Busqueda == "Pending")
            {
                Busqueda = "Facturas";
                dgvQuotes.DataSource = NQuote.LoadQuotesFacturas();
                CargarTotales();
                btnVerEstadosProforma.Text = "Ver Proformas Pendientes";
                this.Text = "Facturas";
            }
            else
            {
                Busqueda = "Pending";
                dgvQuotes.DataSource = NQuote.LoadQuotes();
                CargarTotales();
                btnVerEstadosProforma.Text = "Ver Facturas";
                this.Text = "Proformas"; 
            }
            if (btnVerEstadosProforma.Text == "Ver Facturas" || this.Text == "Proformas")
            {
                eliminarToolStripMenuItem.Visible = true;
                eliminarFacturaToolStripMenuItem.Visible = false;
            }
            else
            {
                eliminarToolStripMenuItem.Visible = false;
                eliminarFacturaToolStripMenuItem.Visible = true;
            }
        }

        private void dtInicio_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dataTable = dgvQuotes.DataSource as DataTable;

                if (dataTable != null)
                {
                    string fechaInicio = dtInicio.Value.ToString("yyyy-MM-dd");
                    string fechaFin = dtFin.Value.ToString("yyyy-MM-dd");

                    dataTable.DefaultView.RowFilter = string.Format("Date >= #{0}# AND Date <= #{1}#", fechaInicio, fechaFin);
                    dgvQuotes.DataSource = dataTable;
                    CargarTotales();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }


        }

        private void CargarTotales()
        {
            decimal Subtotal = 0;
            decimal IVA = 0;
            decimal Total = 0;

            foreach (DataGridViewRow row in dgvQuotes.Rows)
            {
                Subtotal += Convert.ToDecimal(row.Cells[6].Value);
                IVA += Convert.ToDecimal(row.Cells[7].Value);
                Total += Convert.ToDecimal(row.Cells[8].Value);
            }

            txtSubtotal.Text = Subtotal.ToString();
            txtIva.Text = IVA.ToString();
            txtTotal.Text = Total.ToString();
        }

        private void imprimirRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                #region Crear el documento
                // Obtener el directorio del escritorio y las carpetas necesarias
                string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string CarpetaFactura = Path.Combine(escritorio, "Reporte de Facturas");

                string NameFile = "Reporte" + ".pdf";

                // Verificar si la carpeta "Orden Produccion" existe, si no, crearla
                if (!Directory.Exists(CarpetaFactura))
                {
                    Directory.CreateDirectory(CarpetaFactura);
                }



                // Crear la ruta completa del archivo PDF
                string rutaArchivoPDF = Path.Combine(CarpetaFactura, NameFile);

                // Crear el documento en modo horizontal (landscape)
                Document document = new Document(PageSize.A4.Rotate());
                // Crear un nuevo objeto PdfWriter para escribir el documento en un archivo
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(rutaArchivoPDF, FileMode.Create));

                // Abrir el documento
                document.Open();
                #endregion

                #region Crear el contenido del documento
                // Título para la Tabla 5020
                PdfPTable tituloTable5020 = new PdfPTable(1);
                tituloTable5020.TotalWidth = 800f;
                tituloTable5020.LockedWidth = true;

                PdfPCell tituloCell5020 = new PdfPCell(new Phrase("Reportes de Facturas", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, iTextSharp.text.BaseColor.WHITE)))
                {
                    BackgroundColor = new iTextSharp.text.BaseColor(255, 165, 0),
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    Border = iTextSharp.text.Rectangle.NO_BORDER,
                    Padding = 10f
                };

                tituloTable5020.AddCell(tituloCell5020);
                document.Add(tituloTable5020);

                // Espacio después del título
                document.Add(new Paragraph(" "));

                // Agregar al Documento los Datos del dgvOrdenProduccion
                PdfPTable table = new PdfPTable(dgvQuotes.Columns.Count)
                {
                    TotalWidth = 800f, // Ajusta el ancho total según tus necesidades
                    LockedWidth = true
                };

                // Ancho personalizado para cada una de las 19 columnas (ajusta los valores según tus necesidades)
                float[] anchosColumnas = new float[] { 50f, 50f, 50f, 40f, 40f, 50f, 25f, 50f, 25f };
                table.SetWidths(anchosColumnas);

                // Celda 1: Encabezados de las columnas
                foreach (DataGridViewColumn column in dgvQuotes.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, iTextSharp.text.BaseColor.BLACK)))
                    {
                        BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255),
                        BorderWidth = 1f,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_CENTER
                    };
                    table.AddCell(cell);
                }

                // Celda 2: Datos de las filas
                foreach (DataGridViewRow row in dgvQuotes.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        // Validar si la celda es nula
                        if (cell.Value == null)
                        {
                            cell.Value = " ";
                        }
                        PdfPCell celda = new PdfPCell(new Phrase(cell.Value.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, iTextSharp.text.BaseColor.BLACK)))
                        {
                            BorderWidth = 1f,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_CENTER
                        };
                        table.AddCell(celda);
                    }
                }
                document.Add(table);
                document.Add(new Paragraph(" "));
                #endregion

                #region Precios
                // Crear una tabla
                PdfPTable tablePrecio = new PdfPTable(3); // 3 columnas

                // Configurar la tabla

                tablePrecio.WidthPercentage = 96;
                tablePrecio.HorizontalAlignment = Element.ALIGN_CENTER;

                // Configurar el fondo de las celdas
                BaseColor fondoCelda = new BaseColor(192, 192, 192); // Color de fondo gris claro

                // Configurar la celda
                PdfPCell cellp = new PdfPCell();

                // Configurar el color de fondo
                cellp.BackgroundColor = fondoCelda;

                // Agregar los datos a la tabla
                cellp.Phrase = new Phrase("SubTotal: ");
                cellp.HorizontalAlignment = Element.ALIGN_CENTER;
                tablePrecio.AddCell(cellp);
                cellp.Phrase = new Phrase("Iva: ");
                tablePrecio.AddCell(cellp);
                cellp.Phrase = new Phrase("Total: ");
                tablePrecio.AddCell(cellp);
                cellp.Phrase = new Phrase("¢" + txtSubtotal.Text.ToString());
                tablePrecio.AddCell(cellp);

                cellp.Phrase = new Phrase("¢" + txtIva.Text.ToString());

                tablePrecio.AddCell(cellp);
                cellp.Phrase = new Phrase("¢" + txtTotal.Text.ToString());
                tablePrecio.AddCell(cellp);

                // Agregar la tabla al documento
                document.Add(tablePrecio);

                #endregion

                #region Cerrar Documento
                document.Close();
                MessageBox.Show("Hoja de Producción Generada Correctamente", "Hoja de Producción", MessageBoxButtons.OK, MessageBoxIcon.Information);
                #endregion
            }
            catch (Exception EX)
            {
                MessageBox.Show("Error al generar la hoja de producción: " + EX.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            try 
            {
               
                // Verifica si se ha seleccionado una fila
                if (dgvQuotes.SelectedRows.Count > 0)
                {
                    // Obtiene el ID del quote seleccionado
                    int idQuote = Convert.ToInt32(dgvQuotes.SelectedRows[0].Cells[1].Value); // Asumiendo que el ID está en la columna 1

                    // Confirma la eliminación con el usuario
                    DialogResult result = MessageBox.Show("¿Está seguro de que desea eliminar esta cotización? \nEsto eliminará completamente la proforma y sus ventanas.\nSe recomienda eliminar proformas de prueba o que definitivamente no se necesiten.", "Confirmar eliminación", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        // Llama al método de eliminación en la capa de negocios
                        bool success = NQuote.DeleteQuote(idQuote);

                        if (success)
                        {
                            MessageBox.Show("Cotización eliminada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Actualiza el DataGridView para reflejar los cambios
                            frmManagerQuotes_Load(null, null);
                        }
                        else
                        {
                            MessageBox.Show("Error al eliminar la cotización.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione una cotización para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void eliminarFacturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                // Verifica si se ha seleccionado una fila
                if (dgvQuotes.SelectedRows.Count > 0)
                {
                    // Obtiene el ID del quote seleccionado
                    int idQuote = Convert.ToInt32(dgvQuotes.SelectedRows[0].Cells[1].Value); // Asumiendo que el ID está en la columna 1

                    // Confirma la eliminación con el usuario
                    DialogResult result = MessageBox.Show("¿Está seguro de que desea eliminar esta factura? \nEsto eliminará completamente la factura, la proforma y sus ventanas.\nSe recomienda eliminar facturas de prueba o que definitivamente no se necesiten.", "Confirmar eliminación", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        // Llama al método de eliminación en la capa de negocios
                        bool success = NQuote.DeleteBill(idQuote);

                        if (success)
                        {
                            MessageBox.Show("Factura eliminada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Actualiza el DataGridView para reflejar los cambios
                            frmManagerQuotes_Load(null, null);
                        }
                        else
                        {
                            MessageBox.Show("Error al eliminar la factura.\n Verifique que el cliente no tenga saldos pendientes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione una factura para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            frmManagerQuotes_Load(null, null);
        }
    }


}
