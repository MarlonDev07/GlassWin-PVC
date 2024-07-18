
﻿using Negocio.Company.OrdenProduccion;
﻿using MaterialSkin.Controls;
using Negocio.Company.Quote;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using Dominio.Model.ClassWindows;
using Precentacion.User.DashBoard;
using Rectangle = iTextSharp.text.Rectangle;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Globalization;
using System.Data.SqlClient;


namespace Precentacion.User.Bill
{
    public partial class frmOrdenProduccion : MaterialForm //MaterialFoarm
    {
        N_Quote NQuote = new N_Quote();
        N_OrdenProduccion NOrden = new N_OrdenProduccion();
        bool CargaCompleta = false;
        List<string> Piezas5020 = new List<string> { "Cargador", "Umbral", "Jamba", "Superior", "Inferior", "Vertical", "Vertical Centro" };
        List<string> Piezas8025 = new List<string> { "Cargador", "Umbral", "Jamba", "Superior", "Inferior", "Vertical", "Vertical Centro", "PisaAlfombra"};
        List<decimal> ResultadosRebajo = new List<decimal>();
        List<decimal> ResultadosCantidad = new List<decimal>();
        //public DataGridView dgvOrdenProduccion;

        public frmOrdenProduccion()
        {
            InitializeComponent();
            BillUI.loadMaterial(this);
            // Formato a las fechas
            dtpFechaInicio.Format = DateTimePickerFormat.Custom;
            dtpFechaInicio.CustomFormat = "dddd, dd MMMM yyyy - hh:mm tt";
            dtpFechaSalida.Format = DateTimePickerFormat.Custom;
            dtpFechaSalida.CustomFormat = "dddd, dd MMMM yyyy - hh:mm tt";

            cbProyecto.SelectedIndexChanged += cbProyecto_SelectedIndexChanged;

            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripMenuItem optimizeMenuItem = new ToolStripMenuItem("Optimizar");
            optimizeMenuItem.Click += contextMenuStrip1_Click;
            contextMenu.Items.Add(optimizeMenuItem);
            dgvOrdenProduccion.ContextMenuStrip = contextMenu;
            //contextMenu.Visible = false;
            


        }
        private void LoadProjects()
        {
            try
            {
                DataTable projects = NQuote.GetProjectsByCompanyId();

                if (projects != null)
                {
                    // Crear una nueva fila con valores en blanco
                    DataRow blankRow = projects.NewRow();
                    blankRow["ProjectName"] = "";
                    blankRow["IdQuote"] = DBNull.Value; // O el valor que consideres apropiado para un ID vacío
                    projects.Rows.InsertAt(blankRow, 0); // Insertar la fila al inicio del DataTable

                    cbProyecto.DataSource = projects;
                    cbProyecto.DisplayMember = "ProjectName";
                    cbProyecto.ValueMember = "IdQuote";
                    CargaCompleta = true;
                    cbProyecto.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("No se encontraron proyectos para la compañía seleccionada.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al cargar los proyectos: " + ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void LoadWindowsData()
        {
            try
            {
                //Obtener el Id del proyecto seleccionado
                int IdQuote = Convert.ToInt32(cbProyecto.SelectedValue);
                //Obtener las ventanas del proyecto
                DataTable windows = NQuote.WindowsData(IdQuote);
                //Validar si se encontraron ventanas
                if (windows.Rows.Count != 0)
                {
                    // Recorrer las ventanas
                    foreach (DataRow row in windows.Rows)
                    {
                        #region 5020
                        if (row["System"].ToString() == "5020")
                        {
                            // Obtener la ubicación de la ventana, el dato se encuentra en la descripción
                            string DescripcionUbicacion = row["Description"].ToString();
                            string pattern = @"\nUbicacion:\s*(.*)";
                            System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(DescripcionUbicacion, pattern);
                            string Ubicacion = match.Groups[1].Value;
                            //Agregar los Valores a Variables
                            string Sistema = row["System"].ToString();
                            string Diseño = row["Design"].ToString();
                            string Ancho = row["Width"].ToString();
                            string Alto = row["Height"].ToString();

                            //Recorrer las piezas del sistema 5020
                            foreach (string pieza in Piezas5020)
                            {
                                //Calcular los rebajos y Agregarlos A la Lista de Resultados
                                ResultadosRebajo.Add(NOrden.CalculoRebajos5020(Diseño, Ancho, Alto, pieza));
                                //Calcular la Cantidad de Piezas y Agregarlos A la Lista de Resultados
                                ResultadosCantidad.Add(NOrden.CalculoCantidadPiezas(Diseño, pieza));
                            }

                            //Agregar los Resultados al DataGridView
                            dgvOrdenProduccion.Rows.Add(Sistema, Ubicacion, Diseño, Ancho, Alto,
                                ResultadosRebajo[0], ResultadosCantidad[0],
                                ResultadosRebajo[1], ResultadosCantidad[1],
                                ResultadosRebajo[2], ResultadosCantidad[2],
                                ResultadosRebajo[3], ResultadosCantidad[3],
                                ResultadosRebajo[4], ResultadosCantidad[4],
                                ResultadosRebajo[5], ResultadosCantidad[5],
                                ResultadosRebajo[6], ResultadosCantidad[6]);

                            //Limpiar las Listas de Resultados
                            ResultadosRebajo.Clear();
                            ResultadosCantidad.Clear();

                        }
                        #endregion

                        #region 8025
                        if (row["System"].ToString() == "8025 2 Vias" || row["System"].ToString() == "8025 3 Vias")
                        {
                            // Obtener la ubicación de la ventana, el dato se encuentra en la descripción
                            string DescripcionUbicacion = row["Description"].ToString();
                            string pattern = @"\nUbicacion:\s*(.*)";
                            System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(DescripcionUbicacion, pattern);
                            string Ubicacion = match.Groups[1].Value;
                            //Agregar los Valores a Variables
                            string Sistema = row["System"].ToString();
                            string Diseño = row["Design"].ToString();
                            string Ancho = row["Width"].ToString();
                            string Alto = row["Height"].ToString();

                            //Recorrer las piezas del sistema 5020
                            foreach (string pieza in Piezas8025)
                            {
                                //Calcular los rebajos y Agregarlos A la Lista de Resultados
                                ResultadosRebajo.Add(NOrden.CalculoRebajos8025(Diseño, Ancho, Alto, pieza));
                                //Calcular la Cantidad de Piezas y Agregarlos A la Lista de Resultados
                                ResultadosCantidad.Add(NOrden.CalculoCantidadPiezas8025(Diseño, pieza));
                            }

                            //Agregar los Resultados al DataGridView
                            dgvOrdenProduccion8025.Rows.Add(Sistema, Ubicacion, Diseño, Ancho, Alto,
                                ResultadosRebajo[0], ResultadosCantidad[0],
                                ResultadosRebajo[1], ResultadosCantidad[1],
                                ResultadosRebajo[2], ResultadosCantidad[2],
                                ResultadosRebajo[3], ResultadosCantidad[3],
                                ResultadosRebajo[4], ResultadosCantidad[4],
                                ResultadosRebajo[5], ResultadosCantidad[5],
                                ResultadosRebajo[6], ResultadosCantidad[6],
                                ResultadosRebajo[7], ResultadosCantidad[7]
                                );

                            //Limpiar las Listas de Resultados
                            ResultadosRebajo.Clear();
                            ResultadosCantidad.Clear();

                        }
                        #endregion
                    }
                }
                else
                {
                    MessageBox.Show("No hay ventanas para el proyecto seleccionado.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al cargar las ventanas: " + ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmOrdenProduccion_Load(object sender, EventArgs e)
        {
            LoadProjects();
        }

        private void cbProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CargaCompleta)
            {
                try
                {
                    // Cargar el IdQuote en txtOrden
                    if (cbProyecto.SelectedIndex > 0)
                    {
                        int idQuote = Convert.ToInt32(cbProyecto.SelectedValue);
                        txtOrden.Text = idQuote.ToString();
                    }
                    else
                    {
                        txtOrden.Clear(); // Limpiar el campo si no hay proyecto seleccionado
                    }

                    // Limpiar y recargar los datos en el DataGridView dgvOrdenProduccion
    
                    dgvOrdenProduccion.Rows.Clear();
                    dgvOrdenProduccion8025.Rows.Clear();
                    ResultadosRebajo.Clear();
                    ResultadosCantidad.Clear();
                    LoadWindowsData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al cargar las ventanas: " + ex.Message);
                }
            }
        }


        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtOrden.Text != "" || cbProyecto.Text != "")
                {
                    // Verificar si la fecha de inicio es mayor que la fecha de salida
                    if (dtpFechaInicio.Value > dtpFechaSalida.Value)
                    {
                        MessageBox.Show("La fecha de inicio no puede ser mayor que la fecha de salida", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        GenerarPdfHojaProduccion();
                    }
                }
                else
                {
                    MessageBox.Show("Debe llenar los campos antes de imprimir", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



        }


        private void GenerarPdfHojaProduccion()
        {
            try
            {
                if (dgvOrdenProduccion.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos para generar la hoja de producción.", "Hoja de Producción", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                #region Crear el documento
                // Obtener el directorio del escritorio y las carpetas necesarias
                string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string CarpetaFactura = Path.Combine(escritorio, "Orden Produccion");
                string carpetaNombre = Path.Combine(CarpetaFactura, cbProyecto.Text.Trim());
                // Obtener el Id de la Ventana
                string IdWindows = cbProyecto.Text;
                string NameFile = "OrdenProduccion" + IdWindows + ".pdf";

                // Verificar si la carpeta "Orden Produccion" existe, si no, crearla
                if (!Directory.Exists(CarpetaFactura))
                {
                    Directory.CreateDirectory(CarpetaFactura);
                }

                // Verificar si la carpeta con el nombre del proyecto existe, si no, crearla
                if (!Directory.Exists(carpetaNombre))
                {
                    Directory.CreateDirectory(carpetaNombre);
                }

                // Crear la ruta completa del archivo PDF
                string rutaArchivoPDF = Path.Combine(carpetaNombre, NameFile);

                // Crear el documento en modo horizontal (landscape)
                Document document = new Document(PageSize.A4.Rotate());
                // Crear un nuevo objeto PdfWriter para escribir el documento en un archivo
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(rutaArchivoPDF, FileMode.Create));

                // Abrir el documento
                document.Open();
                #endregion

                #region Tabla de Información 
                // Crear tabla para información adicional
                PdfPTable infoTable = new PdfPTable(2)
                {
                    TotalWidth = 500f,
                    LockedWidth = true
                };
                infoTable.SpacingBefore = 10f;
                infoTable.SpacingAfter = 10f;

                // Crear tabla para la información adicional
                PdfPTable infoTable2 = new PdfPTable(2);
                infoTable2.WidthPercentage = 100;
                infoTable2.SpacingBefore = 10f;
                infoTable2.SpacingAfter = 10f;

                // Celda: Orden (Referencia FA)
                PdfPCell cellOrdenTitulo = new PdfPCell(new Phrase("Orden (Referencia FA):", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, iTextSharp.text.BaseColor.BLACK)));
                cellOrdenTitulo.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellOrdenTitulo.HorizontalAlignment = Element.ALIGN_LEFT;
                infoTable2.AddCell(cellOrdenTitulo);

                PdfPCell cellOrdenValor = new PdfPCell(new Phrase(txtOrden.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12, iTextSharp.text.BaseColor.BLACK)));
                cellOrdenValor.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellOrdenValor.HorizontalAlignment = Element.ALIGN_LEFT;
                infoTable2.AddCell(cellOrdenValor);

                // Celda: Proyecto
                PdfPCell cellProyectoTitulo = new PdfPCell(new Phrase("Proyecto:", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, iTextSharp.text.BaseColor.BLACK)));
                cellProyectoTitulo.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellProyectoTitulo.HorizontalAlignment = Element.ALIGN_LEFT;
                infoTable2.AddCell(cellProyectoTitulo);

                PdfPCell cellProyectoValor = new PdfPCell(new Phrase(cbProyecto.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12, iTextSharp.text.BaseColor.BLACK)));
                cellProyectoValor.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellProyectoValor.HorizontalAlignment = Element.ALIGN_LEFT;
                infoTable2.AddCell(cellProyectoValor);

                // Celda: Fecha Inicial
                PdfPCell cellFechaInicialTitulo = new PdfPCell(new Phrase("Fecha Inicial:", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, iTextSharp.text.BaseColor.BLACK)));
                cellFechaInicialTitulo.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellFechaInicialTitulo.HorizontalAlignment = Element.ALIGN_LEFT;
                infoTable2.AddCell(cellFechaInicialTitulo);

                PdfPCell cellFechaInicialValor = new PdfPCell(new Phrase(dtpFechaInicio.Value.ToString("dd/MM/yyyy"), FontFactory.GetFont(FontFactory.HELVETICA, 12, iTextSharp.text.BaseColor.BLACK)));
                cellFechaInicialValor.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellFechaInicialValor.HorizontalAlignment = Element.ALIGN_LEFT;
                infoTable2.AddCell(cellFechaInicialValor);

                // Celda: Fecha Salida
                PdfPCell cellFechaSalidaTitulo = new PdfPCell(new Phrase("Fecha Salida:", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, iTextSharp.text.BaseColor.BLACK)));
                cellFechaSalidaTitulo.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellFechaSalidaTitulo.HorizontalAlignment = Element.ALIGN_LEFT;
                infoTable2.AddCell(cellFechaSalidaTitulo);

                PdfPCell cellFechaSalidaValor = new PdfPCell(new Phrase(dtpFechaSalida.Value.ToString("dd/MM/yyyy"), FontFactory.GetFont(FontFactory.HELVETICA, 12, iTextSharp.text.BaseColor.BLACK)));
                cellFechaSalidaValor.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellFechaSalidaValor.HorizontalAlignment = Element.ALIGN_LEFT;
                infoTable2.AddCell(cellFechaSalidaValor);

                // Agregar la tabla al documento
                document.Add(infoTable2);
                document.Add(new Paragraph(" "));
                #endregion

                #region Tabla de Ventanas
                #region Tabla 5020
                // Título para la Tabla 5020
                PdfPTable tituloTable5020 = new PdfPTable(1);
                tituloTable5020.TotalWidth = 800f;
                tituloTable5020.LockedWidth = true;

                PdfPCell tituloCell5020 = new PdfPCell(new Phrase("Orden de Producción 5020", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, iTextSharp.text.BaseColor.WHITE)))
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
                PdfPTable table = new PdfPTable(dgvOrdenProduccion.Columns.Count)
                {
                    TotalWidth = 800f, // Ajusta el ancho total según tus necesidades
                    LockedWidth = true
                };

                // Ancho personalizado para cada una de las 19 columnas (ajusta los valores según tus necesidades)
                float[] anchosColumnas = new float[] { 50f, 50f, 50f, 40f, 40f, 50f, 25f, 50f, 25f, 50f, 25f, 50f, 25f, 50f, 25f, 50f, 25f, 50f, 25f };
                table.SetWidths(anchosColumnas);

                // Celda 1: Encabezados de las columnas
                foreach (DataGridViewColumn column in dgvOrdenProduccion.Columns)
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
                foreach (DataGridViewRow row in dgvOrdenProduccion.Rows)
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

                #region Tabla 8025
                // Título para la Tabla 8025
                PdfPTable tituloTable8025 = new PdfPTable(1);
                tituloTable8025.TotalWidth = 800f;
                tituloTable8025.LockedWidth = true;

                PdfPCell tituloCell8025 = new PdfPCell(new Phrase("Orden de Producción 8025", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, iTextSharp.text.BaseColor.WHITE)))
                {
                    BackgroundColor = new iTextSharp.text.BaseColor(255, 165, 0),
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    Border = Rectangle.NO_BORDER,
                    Padding = 10f
                };

                tituloTable8025.AddCell(tituloCell8025);
                document.Add(tituloTable8025);

                // Espacio después del título
                document.Add(new Paragraph(" "));

                // Agregar al Documento los Datos del dgvOrdenProduccion8025
                PdfPTable table8025 = new PdfPTable(dgvOrdenProduccion8025.Columns.Count)
                {
                    TotalWidth = 800f, // Ajusta el ancho total según tus necesidades
                    LockedWidth = true
                };

                // Ancho personalizado para cada una de las 21 columnas (ajusta los valores según tus necesidades)
                float[] anchosColumnas2 = new float[] { 50f, 50f, 50f, 40f, 40f, 50f, 25f, 50f, 25f, 50f, 25f, 50f, 25f, 50f, 25f, 50f, 25f, 50f, 25f, 50f, 25f };
                table8025.SetWidths(anchosColumnas2);

                // Celda 1: Encabezados de las columnas
                foreach (DataGridViewColumn column8025 in dgvOrdenProduccion8025.Columns)
                {
                    PdfPCell cell8025 = new PdfPCell(new Phrase(column8025.HeaderText, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, iTextSharp.text.BaseColor.BLACK)))
                    {
                        BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255),
                        BorderWidth = 1f,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_CENTER
                    };
                    table8025.AddCell(cell8025);
                }

                // Celda 2: Datos de las filas
                foreach (DataGridViewRow row8025 in dgvOrdenProduccion8025.Rows)
                {
                    foreach (DataGridViewCell cell8025 in row8025.Cells)
                    {
                        // Validar si la celda es nula
                        if (cell8025.Value == null)
                        {
                            cell8025.Value = " ";
                        }
                        PdfPCell celda8025 = new PdfPCell(new Phrase(cell8025.Value.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, iTextSharp.text.BaseColor.BLACK)))
                        {
                            BorderWidth = 1f,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_CENTER
                        };
                        table8025.AddCell(celda8025);
                    }
                }
                document.Add(table8025);
                document.Add(new Paragraph(" "));
                #endregion

                #endregion

                #region Cerrar Documento
                document.Close();
                MessageBox.Show("Hoja de Producción Generada Correctamente", "Hoja de Producción", MessageBoxButtons.OK, MessageBoxIcon.Information);
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al generar la hoja de producción: " + ex.Message);
            }
        }



        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void frmOrdenProduccion_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmDashUser frm = frmDashUser.Instance;
            frm.WindowState = FormWindowState.Normal;
            frm.Show();
            frm.BringToFront();
        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener la celda seleccionada
                if (dgvOrdenProduccion.SelectedCells.Count > 0)
                {
                    int columnIndex = dgvOrdenProduccion.SelectedCells[0].ColumnIndex;
                    string columnName = dgvOrdenProduccion.Columns[columnIndex].Name;

                    // Verificar si la columna es una de las columnas de artículos
                    string[] validColumns = { "Cargador" }; // Puedes agregar más columnas aquí
                    if (validColumns.Contains(columnName))
                    {
                        List<decimal> requiredLengths = new List<decimal>();
                        foreach (DataGridViewRow row in dgvOrdenProduccion.Rows)
                        {
                            if (row.Cells[columnIndex].Value != null)
                            {
                                string value = row.Cells[columnIndex].Value.ToString();

                                // Intentar convertir directamente la cadena a decimal
                                try
                                {
                                    decimal length = decimal.Parse(value, CultureInfo.GetCultureInfo("es-ES"));
                                    requiredLengths.Add(length);
                                }
                                catch (FormatException)
                                {
                                    // Si falla, intentar con InvariantCulture
                                    decimal length = decimal.Parse(value.Replace(',', '.'), CultureInfo.InvariantCulture);
                                    requiredLengths.Add(length);
                                }
                            }
                        }

                        //DE ESA FORMA SE OBTIENE LA CANTIDAD DE TAMAÑOS QUE SE NECESITEN
                        // Inicializar la lista para los tamaños disponibles
                        List<decimal> availableBars = new List<decimal>();

                        // Iterar sobre la cantidad de registros en requiredLengths
                        int index = 0;
                        while (index < requiredLengths.Count)
                        {
                            // Hacer la consulta a la base de datos
                            DataTable productSizesTable = NQuote.GetProductSizes(columnName);

                            // Procesar los resultados de la consulta
                            foreach (DataRow row in productSizesTable.Rows)
                            {
                                if (row["Tamaño"] != DBNull.Value)
                                {
                                    decimal size = Convert.ToDecimal(row["Tamaño"], CultureInfo.InvariantCulture);
                                    availableBars.Add(size);
                                }
                            }

                            // Incrementar el índice
                            index++;
                        }

                        /*DE ESTA MANERA SOLO SE OBTIENE 1 TAMAÑO*/
                        /*
                        DataTable productSizesTable = NQuote.GetProductSizes(columnName);
                        List<decimal> availableBars = new List<decimal>();
                        foreach (DataRow row in productSizesTable.Rows)
                        {
                            if (row["Tamaño"] != DBNull.Value)
                            {
                                decimal size = Convert.ToDecimal(row["Tamaño"], CultureInfo.InvariantCulture);
                                availableBars.Add(size);
                            }
                        }*/



                        // Mostrar el formulario del optimizador
                        frmOptimizador optimizerForm = new frmOptimizador(requiredLengths.ToArray(), availableBars.ToArray());
                        optimizerForm.TopMost = true;
                        optimizerForm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Seleccione una columna válida para optimizar.", "Columna no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }

}
