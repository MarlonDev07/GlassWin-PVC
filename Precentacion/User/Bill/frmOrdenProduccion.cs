
using Negocio.Company.OrdenProduccion;
using MaterialSkin.Controls;
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
using System.Text.RegularExpressions;
using Twilio.TwiML.Voice;


namespace Precentacion.User.Bill
{
    public partial class frmOrdenProduccion : MaterialForm //MaterialFoarm
    {
        N_Quote NQuote = new N_Quote();
        N_OrdenProduccion NOrden = new N_OrdenProduccion();
        bool CargaCompleta = false;                                                                                                                                               
        List<string> Piezas5020 = new List<string> { "Cargador", "Umbral", "Jamba", "Superior", "Inferior", "Vertical", "Vertical Centro", "Vertical fijo", "Vertical Centro fijo", "Vidrio M ancho", "Vidrio M alto", "Vidrio F ancho", "Vidrio F alto" };
        List<string> Piezas8025 = new List<string> { "Cargador", "Umbral", "Jamba", "Superior", "Inferior", "Vertical", "Vertical Centro", "PisaAlfombra", "Vertical fijo", "Vertical Centro fijo", "Vidrio M ancho", "Vidrio M alto", "Vidrio F ancho", "Vidrio F alto" };
        List<string> Piezas8025_3vias = new List<string> { "Cargador 3 Vias", "Umbral 3 Vias", "Jamba 3 Vias", "PisaAlfombra", "Superior", "Inferior", "Superior f", "Inferior f", "Vertical", "Vertical Centro", "Vertical Centro fijo", "Vertical fijo", "Vidrio M ancho", "Vidrio M alto", "Vidrio F ancho", "Vidrio F alto" };
        List<string> Piezas6030 = new List<string> { "Contramarco Sup-Lat Akari(ancho)", "Contramarco Sup-Lat Akari(alto)", "Contramarco Inferior Akari", "Marco Hoja 6030(alto)", "Marco Hoja 6030 (ancho)", "Marco Hoja Enganche 6030", "Vidrio M ancho", "Vidrio M alto", "Marco Hoja 6030 f (ancho)", "Vidrio F ancho", "Vidrio F alto", "Adaptador Marco Akari" };
        List<string> Piezas6030_3Vias = new List<string> { "Contramarco Sup-Lat Akari(ancho)3Vias", "Contramarco Sup-Lat Akari(alto)3Vias", "Contramarco Inferior Akari3Vias", "Marco Hoja 6030(alto)", "Marco Hoja 6030 (ancho)", "Marco Hoja Enganche 6030", "Vidrio M ancho", "Vidrio M alto", "Marco Hoja 6030 f (ancho)", "Vidrio F ancho", "Vidrio F alto" };
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

            /*ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripMenuItem optimizeMenuItem = new ToolStripMenuItem("Optimizar");
            optimizeMenuItem.Click += contextMenuStrip1_Click;
            contextMenu.Items.Add(optimizeMenuItem);
            dgvOrdenProduccion.ContextMenuStrip = contextMenu;*/
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
                // Obtener el Id del proyecto seleccionado
                int IdQuote = Convert.ToInt32(cbProyecto.SelectedValue);
                // Obtener las ventanas del proyecto
                DataTable windows = NQuote.WindowsData(IdQuote);

                int contadorVentana = 1;

                // Validar si se encontraron ventanas
                if (windows.Rows.Count != 0)
                {
                    // Contador para la columna Ventana
                    int contadorVentana5020 = 1;
                    int contadorVentana8025 = 1;
                    int contadorVentana8025_3Vias = 1;
                    int contadorVentana6030 = 1;
                    int contadorVentana6030_3Vias = 1;

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

                            // Obtener el valor de Cantidad
                            pattern = @"Cantidad:\s*(\d+)";
                            match = System.Text.RegularExpressions.Regex.Match(DescripcionUbicacion, pattern);
                            int Cantidad = int.Parse(match.Groups[1].Value);

                            // Agregar los Valores a Variables
                            string Sistema = row["System"].ToString();
                            string Diseño = row["Design"].ToString();
                            string Ancho = row["Width"].ToString();
                            string Alto = row["Height"].ToString();

                            // Recorrer la cantidad de ventanas
                            for (int i = 0; i < Cantidad; i++)
                            {
                                // Recorrer las piezas del sistema 5020
                                foreach (string pieza in Piezas5020)
                                {
                                    // Calcular los rebajos y Agregarlos A la Lista de Resultados
                                    ResultadosRebajo.Add(NOrden.CalculoRebajos5020(Diseño, Ancho, Alto, pieza));
                                    // Calcular la Cantidad de Piezas y Agregarlos A la Lista de Resultados
                                    ResultadosCantidad.Add(NOrden.CalculoCantidadPiezas(Diseño, pieza));
                                }

                                // Agregar los Resultados al DataGridView
                                dgvOrdenProduccion.Rows.Add(Sistema, Ubicacion, Diseño, Ancho, Alto,
                                    ResultadosRebajo[0], ResultadosCantidad[0],
                                    ResultadosRebajo[1], ResultadosCantidad[1],
                                    ResultadosRebajo[2], ResultadosCantidad[2],
                                    ResultadosRebajo[3], ResultadosCantidad[3],
                                    ResultadosRebajo[4], ResultadosCantidad[4],
                                    ResultadosRebajo[5], ResultadosCantidad[5],
                                    ResultadosRebajo[6], ResultadosCantidad[6],
                                    contadorVentana5020,
                                    ResultadosRebajo[7], ResultadosCantidad[7],
                                    ResultadosRebajo[8], ResultadosCantidad[8],
                                    ResultadosRebajo[9], ResultadosCantidad[9],
                                    ResultadosRebajo[10], ResultadosCantidad[10],
                                    ResultadosRebajo[11], ResultadosCantidad[11],
                                    ResultadosRebajo[12], ResultadosCantidad[12]
                                    );

                                // Incrementar el contador para la próxima fila
                                contadorVentana5020++;

                                // Limpiar las Listas de Resultados
                                ResultadosRebajo.Clear();
                                ResultadosCantidad.Clear();
                            }
                        }
                        #endregion

                        #region 8025
                        if (row["System"].ToString() == "8025 2 Vias")
                        {
                            // Obtener la ubicación de la ventana, el dato se encuentra en la descripción
                            string DescripcionUbicacion = row["Description"].ToString();
                            string pattern = @"\nUbicacion:\s*(.*)";
                            System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(DescripcionUbicacion, pattern);
                            string Ubicacion = match.Groups[1].Value;

                            // Obtener el valor de Cantidad
                            pattern = @"Cantidad:\s*(\d+)";
                            match = System.Text.RegularExpressions.Regex.Match(DescripcionUbicacion, pattern);
                            int Cantidad = int.Parse(match.Groups[1].Value);

                            // Agregar los Valores a Variables
                            string Sistema = row["System"].ToString();
                            string Diseño = row["Design"].ToString();
                            string Ancho = row["Width"].ToString();
                            string Alto = row["Height"].ToString();

                            // Recorrer la cantidad de ventanas
                            for (int i = 0; i < Cantidad; i++)
                            {
                                // Recorrer las piezas del sistema 8025
                                foreach (string pieza in Piezas8025)
                                {
                                    // Calcular los rebajos y Agregarlos A la Lista de Resultados
                                    ResultadosRebajo.Add(NOrden.CalculoRebajos8025(Diseño, Ancho, Alto, pieza));
                                    // Calcular la Cantidad de Piezas y Agregarlos A la Lista de Resultados
                                    ResultadosCantidad.Add(NOrden.CalculoCantidadPiezas8025(Diseño, pieza));
                                }

                                // Agregar los Resultados al DataGridView
                                dgvOrdenProduccion8025.Rows.Add(Sistema, Ubicacion, Diseño, Ancho, Alto,
                                    ResultadosRebajo[0], ResultadosCantidad[0],
                                    ResultadosRebajo[1], ResultadosCantidad[1],
                                    ResultadosRebajo[2], ResultadosCantidad[2],
                                    ResultadosRebajo[3], ResultadosCantidad[3],
                                    ResultadosRebajo[4], ResultadosCantidad[4],
                                    ResultadosRebajo[5], ResultadosCantidad[5],
                                    ResultadosRebajo[6], ResultadosCantidad[6],
                                    ResultadosRebajo[7], ResultadosCantidad[7],
                                    contadorVentana8025,
                                    ResultadosRebajo[8], ResultadosCantidad[8],
                                    ResultadosRebajo[9], ResultadosCantidad[9],
                                    ResultadosRebajo[10], ResultadosCantidad[10],
                                    ResultadosRebajo[11], ResultadosCantidad[11],
                                    ResultadosRebajo[12], ResultadosCantidad[12],
                                    ResultadosRebajo[13], ResultadosCantidad[13]
                                    );

                                // Incrementar el contador para la próxima fila
                                contadorVentana8025++;

                                // Limpiar las Listas de Resultados
                                ResultadosRebajo.Clear();
                                ResultadosCantidad.Clear();
                            }
                        }
                        #endregion

                        #region 8025 3 Vias
                        
                        if (row["System"].ToString() == "8025 3 Vias")
                        {
                            // Obtener la ubicación de la ventana, el dato se encuentra en la descripción
                            string DescripcionUbicacion = row["Description"].ToString();
                            string pattern = @"\nUbicacion:\s*(.*)";
                            System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(DescripcionUbicacion, pattern);
                            string Ubicacion = match.Groups[1].Value;

                            // Obtener el valor de Cantidad
                            pattern = @"Cantidad:\s*(\d+)";
                            match = System.Text.RegularExpressions.Regex.Match(DescripcionUbicacion, pattern);
                            int Cantidad = int.Parse(match.Groups[1].Value);

                            // Agregar los Valores a Variables
                            string Sistema = row["System"].ToString();
                            string Diseño = row["Design"].ToString();
                            string Ancho = row["Width"].ToString();
                            string Alto = row["Height"].ToString();

                            // Recorrer la cantidad de ventanas
                            for (int i = 0; i < Cantidad; i++)
                            {
                                // Recorrer las piezas del sistema 8025 3 Vias
                                foreach (string pieza in Piezas8025_3vias)
                                {
                                    // Calcular los rebajos y Agregarlos A la Lista de Resultados
                                    ResultadosRebajo.Add(NOrden.CalculoRebajos80253Vias(Diseño, Ancho, Alto, pieza));
                                    // Calcular la Cantidad de Piezas y Agregarlos A la Lista de Resultados
                                    ResultadosCantidad.Add(NOrden.CalculoCantidadPiezas80253Vias(Diseño, pieza));
                                }

                                // Agregar los Resultados al DataGridView
                                dgvOrdenProduccion8025_3vias.Rows.Add(Sistema, Ubicacion, Diseño, Ancho, Alto,
                                    ResultadosRebajo[0], ResultadosCantidad[0],
                                    ResultadosRebajo[1], ResultadosCantidad[1],
                                    ResultadosRebajo[2], ResultadosCantidad[2],
                                    ResultadosRebajo[3], ResultadosCantidad[3],
                                    ResultadosRebajo[4], ResultadosCantidad[4],
                                    ResultadosRebajo[5], ResultadosCantidad[5],
                                    ResultadosRebajo[6], ResultadosCantidad[6],
                                    ResultadosRebajo[7], ResultadosCantidad[7],
                                    ResultadosRebajo[8], ResultadosCantidad[8],
                                    ResultadosRebajo[9], ResultadosCantidad[9],
                                    contadorVentana8025_3Vias,
                                    ResultadosRebajo[10], ResultadosCantidad[10],
                                    ResultadosRebajo[11], ResultadosCantidad[11],
                                    ResultadosRebajo[12], ResultadosCantidad[12],
                                    ResultadosRebajo[13], ResultadosCantidad[13],
                                    ResultadosRebajo[14], ResultadosCantidad[14],
                                    ResultadosRebajo[15], ResultadosCantidad[15]
                                    );

                                // Incrementar el contador para la próxima fila
                                contadorVentana8025_3Vias++;

                                // Limpiar las Listas de Resultados
                                ResultadosRebajo.Clear();
                                ResultadosCantidad.Clear();
                            }
                        }
                        #endregion

                        #region 6030
                        if (row["System"].ToString() == "6030 2 Vias")
                        {
                            // Obtener la ubicación de la ventana, el dato se encuentra en la descripción
                            string DescripcionUbicacion = row["Description"].ToString();
                            string pattern = @"\nUbicacion:\s*(.*)";
                            System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(DescripcionUbicacion, pattern);
                            string Ubicacion = match.Groups[1].Value;

                            // Obtener el valor de Cantidad
                            pattern = @"Cantidad:\s*(\d+)";
                            match = System.Text.RegularExpressions.Regex.Match(DescripcionUbicacion, pattern);
                            int Cantidad = int.Parse(match.Groups[1].Value);

                            // Agregar los Valores a Variables
                            string Sistema = row["System"].ToString();
                            string Diseño = row["Design"].ToString();
                            string Ancho = row["Width"].ToString();
                            string Alto = row["Height"].ToString();

                            // Recorrer la cantidad de ventanas
                            for (int i = 0; i < Cantidad; i++)
                            {
                                // Recorrer las piezas del sistema 5020
                                foreach (string pieza in Piezas6030)
                                {
                                    // Calcular los rebajos y Agregarlos A la Lista de Resultados
                                    ResultadosRebajo.Add(NOrden.CalculoRebajos6030(Diseño, Ancho, Alto, pieza));
                                    // Calcular la Cantidad de Piezas y Agregarlos A la Lista de Resultados
                                    ResultadosCantidad.Add(NOrden.CalculoCantidadPiezas6030(Diseño, pieza));
                                }

                                // Agregar los Resultados al DataGridView
                                dgvOrdenProduccion6030.Rows.Add(Sistema, Ubicacion, Diseño, Ancho, Alto,
                                    ResultadosRebajo[0], ResultadosCantidad[0],
                                    ResultadosRebajo[1], ResultadosCantidad[1],
                                    ResultadosRebajo[2], ResultadosCantidad[2],
                                    ResultadosRebajo[3], ResultadosCantidad[3],
                                    ResultadosRebajo[4], ResultadosCantidad[4],
                                    ResultadosRebajo[5], ResultadosCantidad[5],
                                    ResultadosRebajo[6], ResultadosCantidad[6],
                                    ResultadosRebajo[7], ResultadosCantidad[7],
                                    ResultadosRebajo[8], ResultadosCantidad[8],
                                    ResultadosRebajo[9], ResultadosCantidad[9],
                                    ResultadosRebajo[10], ResultadosCantidad[10],
                                    ResultadosRebajo[11], ResultadosCantidad[11],
                                    contadorVentana6030
                                    );

                                // Incrementar el contador para la próxima fila
                                contadorVentana6030++;

                                // Limpiar las Listas de Resultados
                                ResultadosRebajo.Clear();
                                ResultadosCantidad.Clear();
                            }
                        }
                        #endregion
                        #region 6030 3 Vias
                        if (row["System"].ToString() == "6030 3 Vias")
                        {
                            // Obtener la ubicación de la ventana, el dato se encuentra en la descripción
                            string DescripcionUbicacion = row["Description"].ToString();
                            string pattern = @"\nUbicacion:\s*(.*)";
                            System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(DescripcionUbicacion, pattern);
                            string Ubicacion = match.Groups[1].Value;

                            // Obtener el valor de Cantidad
                            pattern = @"Cantidad:\s*(\d+)";
                            match = System.Text.RegularExpressions.Regex.Match(DescripcionUbicacion, pattern);
                            int Cantidad = int.Parse(match.Groups[1].Value);

                            // Agregar los Valores a Variables
                            string Sistema = row["System"].ToString();
                            string Diseño = row["Design"].ToString();
                            string Ancho = row["Width"].ToString();
                            string Alto = row["Height"].ToString();

                            // Recorrer la cantidad de ventanas
                            for (int i = 0; i < Cantidad; i++)
                            {
                                // Recorrer las piezas del sistema 5020
                                foreach (string pieza in Piezas6030_3Vias)
                                {
                                    // Calcular los rebajos y Agregarlos A la Lista de Resultados
                                    ResultadosRebajo.Add(NOrden.CalculoRebajos60303Vias(Diseño, Ancho, Alto, pieza));
                                    // Calcular la Cantidad de Piezas y Agregarlos A la Lista de Resultados
                                    ResultadosCantidad.Add(NOrden.CalculoCantidadPiezas60303Vias(Diseño, pieza));
                                }

                                // Agregar los Resultados al DataGridView
                                dgvOrdenProduccion6030_3vias.Rows.Add(Sistema, Ubicacion, Diseño, Ancho, Alto,
                                    ResultadosRebajo[0], ResultadosCantidad[0],
                                    ResultadosRebajo[1], ResultadosCantidad[1],
                                    ResultadosRebajo[2], ResultadosCantidad[2],
                                    ResultadosRebajo[3], ResultadosCantidad[3],
                                    ResultadosRebajo[4], ResultadosCantidad[4],
                                    ResultadosRebajo[5], ResultadosCantidad[5],
                                    ResultadosRebajo[6], ResultadosCantidad[6],
                                    ResultadosRebajo[7], ResultadosCantidad[7],
                                    ResultadosRebajo[8], ResultadosCantidad[8],
                                    ResultadosRebajo[9], ResultadosCantidad[9],
                                    ResultadosRebajo[10], ResultadosCantidad[10],
                                    contadorVentana6030_3Vias
                                    );

                                // Incrementar el contador para la próxima fila
                                contadorVentana6030_3Vias++;

                                // Limpiar las Listas de Resultados
                                ResultadosRebajo.Clear();
                                ResultadosCantidad.Clear();
                            }
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
                    dgvOrdenProduccion6030_3vias.Rows.Clear();
                    dgvOrdenProduccion6030.Rows.Clear();
                    dgvOrdenProduccion8025_3vias.Rows.Clear();
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

                // Define el ancho que deseas para la página, por ejemplo, 1000 puntos
                float anchoPersonalizado = 1200f; // ancho en puntos
                float alto = PageSize.A4.Height; // altura estándar de una página A4

                // Crear un documento con un tamaño personalizado
                Document document = new Document(new Rectangle(anchoPersonalizado, alto));

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
                PdfPCell cellOrdenTitulo = new PdfPCell(new Phrase("Orden (Referencia FA):", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, iTextSharp.text.BaseColor.BLACK)));
                cellOrdenTitulo.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellOrdenTitulo.HorizontalAlignment = Element.ALIGN_LEFT;
                infoTable2.AddCell(cellOrdenTitulo);

                PdfPCell cellOrdenValor = new PdfPCell(new Phrase(txtOrden.Text, FontFactory.GetFont(FontFactory.HELVETICA, 11, iTextSharp.text.BaseColor.BLACK)));
                cellOrdenValor.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellOrdenValor.HorizontalAlignment = Element.ALIGN_LEFT;
                infoTable2.AddCell(cellOrdenValor);

                // Celda: Proyecto
                PdfPCell cellProyectoTitulo = new PdfPCell(new Phrase("Proyecto:", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, iTextSharp.text.BaseColor.BLACK)));
                cellProyectoTitulo.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellProyectoTitulo.HorizontalAlignment = Element.ALIGN_LEFT;
                infoTable2.AddCell(cellProyectoTitulo);

                PdfPCell cellProyectoValor = new PdfPCell(new Phrase(cbProyecto.Text, FontFactory.GetFont(FontFactory.HELVETICA, 11, iTextSharp.text.BaseColor.BLACK)));
                cellProyectoValor.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellProyectoValor.HorizontalAlignment = Element.ALIGN_LEFT;
                infoTable2.AddCell(cellProyectoValor);

                // Celda: Fecha Inicial
                PdfPCell cellFechaInicialTitulo = new PdfPCell(new Phrase("Fecha Inicial:", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, iTextSharp.text.BaseColor.BLACK)));
                cellFechaInicialTitulo.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellFechaInicialTitulo.HorizontalAlignment = Element.ALIGN_LEFT;
                infoTable2.AddCell(cellFechaInicialTitulo);

                PdfPCell cellFechaInicialValor = new PdfPCell(new Phrase(dtpFechaInicio.Value.ToString("dd/MM/yyyy"), FontFactory.GetFont(FontFactory.HELVETICA, 11, iTextSharp.text.BaseColor.BLACK)));
                cellFechaInicialValor.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellFechaInicialValor.HorizontalAlignment = Element.ALIGN_LEFT;
                infoTable2.AddCell(cellFechaInicialValor);

                // Celda: Fecha Salida
                PdfPCell cellFechaSalidaTitulo = new PdfPCell(new Phrase("Fecha Salida:", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, iTextSharp.text.BaseColor.BLACK)));
                cellFechaSalidaTitulo.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellFechaSalidaTitulo.HorizontalAlignment = Element.ALIGN_LEFT;
                infoTable2.AddCell(cellFechaSalidaTitulo);

                PdfPCell cellFechaSalidaValor = new PdfPCell(new Phrase(dtpFechaSalida.Value.ToString("dd/MM/yyyy"), FontFactory.GetFont(FontFactory.HELVETICA, 11, iTextSharp.text.BaseColor.BLACK)));
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
                tituloTable5020.TotalWidth = 1200f;
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
                    TotalWidth = 1200f, // Ajusta el ancho total según tus necesidades
                    LockedWidth = true
                };

                // Ancho personalizado para cada una de las 19 columnas (ajusta los valores según tus necesidades)
                float[] anchosColumnas = new float[] { 50f, 50f, 50f, 40f, 40f, 50f, 25f, 50f, 25f, 50f, 25f, 50f, 25f, 50f, 25f, 50f, 25f, 50f, 25f, 0f, 50f, 25f, 50f, 25f, 50f, 0f, 50f, 25f, 50f, 0f, 50f, 25f };
                table.SetWidths(anchosColumnas);

                // Celda 1: Encabezados de las columnas
                foreach (DataGridViewColumn column in dgvOrdenProduccion.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, iTextSharp.text.BaseColor.BLACK)))
                    {
                        BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255),
                        BorderWidth = .5f,
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
                        PdfPCell celda = new PdfPCell(new Phrase(cell.Value.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.BaseColor.BLACK)))
                        {
                            BorderWidth = .5f,
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
                tituloTable8025.TotalWidth = 1200f;
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
                    TotalWidth = 1200f, // Ajusta el ancho total según tus necesidades
                    LockedWidth = true
                };

                // Ancho personalizado para cada una de las 21 columnas (ajusta los valores según tus necesidades)
                float[] anchosColumnas2 = new float[] { 50f, 50f, 50f, 40f, 40f, 50f, 25f, 50f, 25f, 50f, 25f, 50f, 25f, 50f, 25f, 50f, 25f, 50f, 25f, 50f, 25f, 0f, 50f, 25f, 50f, 25f, 50f, 0f, 50f, 25f, 50f, 0f, 50f, 25f };
                table8025.SetWidths(anchosColumnas2);

                // Celda 1: Encabezados de las columnas
                foreach (DataGridViewColumn column8025 in dgvOrdenProduccion8025.Columns)
                {
                    PdfPCell cell8025 = new PdfPCell(new Phrase(column8025.HeaderText, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, iTextSharp.text.BaseColor.BLACK)))
                    {
                        BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255),
                        BorderWidth = .5f,
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
                        PdfPCell celda8025 = new PdfPCell(new Phrase(cell8025.Value.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.BaseColor.BLACK)))
                        {
                            BorderWidth = .5f,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_CENTER
                        };
                        table8025.AddCell(celda8025);
                    }
                }
                document.Add(table8025);
                document.Add(new Paragraph(" "));
                #endregion






                #region Tabla 8025 3 Vias
                // Título para la Tabla 8025
                PdfPTable tituloTable8025_3Vias = new PdfPTable(1);
                tituloTable8025_3Vias.TotalWidth = 1200f;
                tituloTable8025_3Vias.LockedWidth = true;

                PdfPCell tituloCell8025_3Vias = new PdfPCell(new Phrase("Orden de Producción 8025 3 Vias", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, iTextSharp.text.BaseColor.WHITE)))
                {
                    BackgroundColor = new iTextSharp.text.BaseColor(255, 165, 0),
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    Border = Rectangle.NO_BORDER,
                    Padding = 10f
                };

                tituloTable8025_3Vias.AddCell(tituloCell8025_3Vias);
                document.Add(tituloTable8025_3Vias);

                // Espacio después del título
                document.Add(new Paragraph(" "));

                // Agregar al Documento los Datos del dgvOrdenProduccion8025
                PdfPTable table8025_3Vias = new PdfPTable(dgvOrdenProduccion8025_3vias.Columns.Count)
                {
                    TotalWidth = 1200f, // Ajusta el ancho total según tus necesidades
                    LockedWidth = true
                };

                // Ancho personalizado para cada una de las 21 columnas (ajusta los valores según tus necesidades)
                float[] anchosColumnas3 = new float[] { 40f, 40f, 40f, 40f, 40f, 50f, 25f, 50f, 25f, 50f, 25f, 50f, 25f, 50f, 25f, 50f, 25f, 50f, 25f, 50f, 25f, 50f, 25f, 50f, 25f, 0f, 50f, 25f, 50f, 25f, 50f, 0f, 50f, 25f, 50f, 0f, 50f, 25f };
                table8025_3Vias.SetWidths(anchosColumnas3);

                // Celda 1: Encabezados de las columnas
                foreach (DataGridViewColumn column8025_3Vias in dgvOrdenProduccion8025_3vias.Columns)
                {
                    PdfPCell cell8025_3Vias = new PdfPCell(new Phrase(column8025_3Vias.HeaderText, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, iTextSharp.text.BaseColor.BLACK)))
                    {
                        BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255),
                        BorderWidth = .5f,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_CENTER
                    };
                    table8025_3Vias.AddCell(cell8025_3Vias);
                }

                // Celda 2: Datos de las filas
                foreach (DataGridViewRow row8025_3Vias in dgvOrdenProduccion8025_3vias.Rows)
                {
                    foreach (DataGridViewCell cell8025_3Vias in row8025_3Vias.Cells)
                    {
                        // Validar si la celda es nula
                        if (cell8025_3Vias.Value == null)
                        {
                            cell8025_3Vias.Value = " ";
                        }
                        PdfPCell celda8025_3Vias = new PdfPCell(new Phrase(cell8025_3Vias.Value.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.BaseColor.BLACK)))
                        {
                            BorderWidth = .5f,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_CENTER
                        };
                        table8025_3Vias.AddCell(celda8025_3Vias);
                    }
                }
                document.Add(table8025_3Vias);
                document.Add(new Paragraph(" "));
                #endregion







                #region Tabla 6030 2 Vias
                // Título para la Tabla 8025
                PdfPTable tituloTable6030_2Vias = new PdfPTable(1);
                tituloTable6030_2Vias.TotalWidth = 1200f;
                tituloTable6030_2Vias.LockedWidth = true;

                PdfPCell tituloCell6030_2Vias = new PdfPCell(new Phrase("Orden de Producción 6030 2 Vias", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, iTextSharp.text.BaseColor.WHITE)))
                {
                    BackgroundColor = new iTextSharp.text.BaseColor(255, 165, 0),
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    Border = Rectangle.NO_BORDER,
                    Padding = 10f
                };

                tituloTable6030_2Vias.AddCell(tituloCell6030_2Vias);
                document.Add(tituloTable6030_2Vias);

                // Espacio después del título
                document.Add(new Paragraph(" "));

                // Agregar al Documento los Datos del dgvOrdenProduccion8025
                PdfPTable table6030_2Vias = new PdfPTable(dgvOrdenProduccion6030.Columns.Count)
                {
                    TotalWidth = 1200f, // Ajusta el ancho total según tus necesidades
                    LockedWidth = true
                };

                // Ancho personalizado para cada una de las 21 columnas (ajusta los valores según tus necesidades)
                float[] anchosColumnas4 = new float[] { 40f, 40f, 40f, 40f, 40f, 50f, 25f, 50f, 25f, 50f, 25f, 50f, 25f, 50f, 25f, 50f, 25f, 50f, 0f, 50f, 25f, 40f, 20f, 40f, 0f, 40f, 20f, 40f, 20f, 0f };
                table6030_2Vias.SetWidths(anchosColumnas4);

                // Celda 1: Encabezados de las columnas
                foreach (DataGridViewColumn column6030_2Vias in dgvOrdenProduccion6030.Columns)
                {
                    PdfPCell cell6030_2Vias = new PdfPCell(new Phrase(column6030_2Vias.HeaderText, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, iTextSharp.text.BaseColor.BLACK)))
                    {
                        BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255),
                        BorderWidth = .5f,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_CENTER
                    };
                    table6030_2Vias.AddCell(cell6030_2Vias);
                }

                // Celda 2: Datos de las filas
                foreach (DataGridViewRow row6030_2Vias in dgvOrdenProduccion6030.Rows)
                {
                    foreach (DataGridViewCell cell6030_2Vias in row6030_2Vias.Cells)
                    {
                        // Validar si la celda es nula
                        if (cell6030_2Vias.Value == null)
                        {
                            cell6030_2Vias.Value = " ";
                        }
                        PdfPCell celda6030_2Vias = new PdfPCell(new Phrase(cell6030_2Vias.Value.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.BaseColor.BLACK)))
                        {
                            BorderWidth = .5f,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_CENTER
                        };
                        table6030_2Vias.AddCell(celda6030_2Vias);
                    }
                }
                document.Add(table6030_2Vias);
                document.Add(new Paragraph(" "));
                #endregion




                #region Tabla 6030 3 Vias
                // Título para la Tabla 8025
                PdfPTable tituloTable6030_3Vias = new PdfPTable(1);
                tituloTable6030_3Vias.TotalWidth = 1200f;
                tituloTable6030_3Vias.LockedWidth = true;

                PdfPCell tituloCell6030_3Vias = new PdfPCell(new Phrase("Orden de Producción 6030 3 Vias", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, iTextSharp.text.BaseColor.WHITE)))
                {
                    BackgroundColor = new iTextSharp.text.BaseColor(255, 165, 0),
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    Border = Rectangle.NO_BORDER,
                    Padding = 10f
                };

                tituloTable6030_3Vias.AddCell(tituloCell6030_3Vias);
                document.Add(tituloTable6030_3Vias);

                // Espacio después del título
                document.Add(new Paragraph(" "));

                // Agregar al Documento los Datos del dgvOrdenProduccion8025
                PdfPTable table6030_3Vias = new PdfPTable(dgvOrdenProduccion6030_3vias.Columns.Count)
                {
                    TotalWidth = 1200f, // Ajusta el ancho total según tus necesidades
                    LockedWidth = true
                };

                // Ancho personalizado para cada una de las 21 columnas (ajusta los valores según tus necesidades)
                float[] anchosColumnas5 = new float[] { 40f, 40f, 40f, 40f, 40f, 50f, 25f, 50f, 25f, 50f, 25f, 50f, 25f, 50f, 25f, 50f, 25f, 50f, 0f, 50f, 25f, 40f, 20f, 40f, 0f, 40f, 20f, 0f };
                table6030_3Vias.SetWidths(anchosColumnas5);

                // Celda 1: Encabezados de las columnas
                foreach (DataGridViewColumn column6030_3Vias in dgvOrdenProduccion6030_3vias.Columns)
                {
                    PdfPCell cell6030_3Vias = new PdfPCell(new Phrase(column6030_3Vias.HeaderText, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, iTextSharp.text.BaseColor.BLACK)))
                    {
                        BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255),
                        BorderWidth = .5f,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_CENTER
                    };
                    table6030_3Vias.AddCell(cell6030_3Vias);
                }

                // Celda 2: Datos de las filas
                foreach (DataGridViewRow row6030_3Vias in dgvOrdenProduccion6030_3vias.Rows)
                {
                    foreach (DataGridViewCell cell6030_3Vias in row6030_3Vias.Cells)
                    {
                        // Validar si la celda es nula
                        if (cell6030_3Vias.Value == null)
                        {
                            cell6030_3Vias.Value = " ";
                        }
                        PdfPCell celda6030_3Vias = new PdfPCell(new Phrase(cell6030_3Vias.Value.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.BaseColor.BLACK)))
                        {
                            BorderWidth = .5f,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_CENTER
                        };
                        table6030_3Vias.AddCell(celda6030_3Vias);
                    }
                }
                document.Add(table6030_3Vias);
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
           
        }


        private void btnOptimizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvOrdenProduccion.SelectedCells.Count == 0)
                {
                    MessageBox.Show("Debe elegir un proyecto antes de usar el optimizador.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // Mostrar el mensaje de confirmación
                    DialogResult result = MessageBox.Show("El optimizador está aún en desarrollo, ¿Desea continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    // Si el usuario elige "No", cerrar la ejecución del método
                    if (result == DialogResult.No)
                    {
                        return;
                    }

                    // Obtener la celda seleccionada
                    if (dgvOrdenProduccion.SelectedCells.Count > 0)
                    {
                        int columnIndex = dgvOrdenProduccion.SelectedCells[0].ColumnIndex;
                        string columnName = dgvOrdenProduccion.Columns[columnIndex].Name;

                        // Diccionario con columnas válidas y sus respectivas columnas de cantidad
                        Dictionary<string, string> validColumns = new Dictionary<string, string>
                {
                    { "Cargador", "cantCargador" },
                    { "Umbral", "cantUmbral" },
                    { "Jamba", "cantJamba" },
                    { "Superior", "cantSuperior" },
                    { "Inferior", "cantInferior" },
                    { "Vertical", "cantVertical" },
                    { "VerticalCentro", "cantVerticalC" }
                };

                        // Diccionario para almacenar longitudes requeridas por columna
                        Dictionary<string, List<(decimal length, int window)>> requiredLengthsDict = new Dictionary<string, List<(decimal length, int window)>>();
                        foreach (var column in validColumns.Keys)
                        {
                            requiredLengthsDict[column] = new List<(decimal length, int window)>();
                        }

                        // Diccionario para almacenar barras disponibles por columna
                        Dictionary<string, List<decimal>> availableBarsDict = new Dictionary<string, List<decimal>>();
                        foreach (var column in validColumns.Keys)
                        {
                            availableBarsDict[column] = new List<decimal>();
                        }

                        // Procesar filas
                        foreach (DataGridViewRow row in dgvOrdenProduccion.Rows)
                        {
                            int ventanaIndex = dgvOrdenProduccion.Columns["Ventana"].Index; // Índice de la columna de ventana
                            if (row.Cells[ventanaIndex].Value != null)
                            {
                                string ventanaValue = row.Cells[ventanaIndex].Value.ToString();
                                int ventana;
                                if (int.TryParse(ventanaValue, out ventana))
                                {
                                    foreach (var column in validColumns.Keys)
                                    {
                                        int colIndex = dgvOrdenProduccion.Columns[column].Index;
                                        int cantIndex = dgvOrdenProduccion.Columns[validColumns[column]].Index;

                                        if (row.Cells[colIndex].Value != null && row.Cells[cantIndex].Value != null)
                                        {
                                            string value = row.Cells[colIndex].Value.ToString();
                                            string cantValue = row.Cells[cantIndex].Value.ToString();
                                            decimal length;
                                            int cantidad;

                                            // Intentar convertir la cadena a decimal y la cantidad a int
                                            if ((decimal.TryParse(value, NumberStyles.Any, CultureInfo.GetCultureInfo("es-ES"), out length) ||
                                                 decimal.TryParse(value.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out length)) &&
                                                int.TryParse(cantValue, out cantidad))
                                            {
                                                for (int i = 0; i < cantidad; i++)
                                                {
                                                    requiredLengthsDict[column].Add((length, ventana));
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        // Obtener los tamaños de producto de la base de datos y llenar las barras disponibles
                        foreach (var column in validColumns.Keys)
                        {
                            FillAvailableBars(column, availableBarsDict[column], requiredLengthsDict[column].Count);
                        }

                        string orden = txtOrden.Text;
                        string proyecto = cbProyecto.Text;

                        // Mostrar el formulario del optimizador
                        frmOptimizador optimizerForm = new frmOptimizador(
                            requiredLengthsDict["Cargador"].ToArray(), availableBarsDict["Cargador"].ToArray(),
                            requiredLengthsDict["Umbral"].ToArray(), availableBarsDict["Umbral"].ToArray(),
                            requiredLengthsDict["Jamba"].ToArray(), availableBarsDict["Jamba"].ToArray(),
                            requiredLengthsDict["Superior"].ToArray(), availableBarsDict["Superior"].ToArray(),
                            requiredLengthsDict["Inferior"].ToArray(), availableBarsDict["Inferior"].ToArray(),
                            requiredLengthsDict["Vertical"].ToArray(), availableBarsDict["Vertical"].ToArray(),
                            requiredLengthsDict["VerticalCentro"].ToArray(), availableBarsDict["VerticalCentro"].ToArray(),
                            orden, proyecto
                        );
                        optimizerForm.TopMost = true;
                        optimizerForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("La tabla del sistema 5020 está vacía. Debe de tener datos para su optimización.", "Tabla vacía", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }






        private void FillAvailableBars(string columnName, List<decimal> availableBars, int requiredCount)
        {
            for (int i = 0; i < requiredCount; i++)
            {
                // Hacer la consulta a la base de datos
                DataTable productSizesTable = columnName == "Cargador" ? NQuote.GetProductSizes(columnName) :
                                              columnName == "Umbral" ? NQuote.GetProductSizesU(columnName) :
                                              columnName == "Jamba" ? NQuote.GetProductSizesJ(columnName) :
                                              columnName == "Superior" ? NQuote.GetProductSizesS(columnName) :
                                              columnName == "Inferior" ? NQuote.GetProductSizesI(columnName) :
                                              columnName == "Vertical" ? NQuote.GetProductSizesV(columnName) :
                                              NQuote.GetProductSizesVC(columnName);

                // Procesar los resultados de la consulta
                foreach (DataRow row in productSizesTable.Rows)
                {
                    if (row["Tamaño"] != DBNull.Value)
                    {
                        decimal size = Convert.ToDecimal(row["Tamaño"], CultureInfo.InvariantCulture);
                        availableBars.Add(size);
                    }
                }
            }
        }

        private void FillAvailableBars2(string columnName, List<decimal> availableBars, int requiredCount)
        {
            for (int i = 0; i < requiredCount; i++)
            {
                // Hacer la consulta a la base de datos
                DataTable productSizesTable2 = columnName == "Cargador8025" ? NQuote.GetProductSizes8025() :
                                              columnName == "Umbral8025" ? NQuote.GetProductSizesU8025() :
                                              columnName == "Jamba8025" ? NQuote.GetProductSizesJ8025() :
                                              columnName == "Superior8025" ? NQuote.GetProductSizesS8025() :
                                              columnName == "Inferior8025" ? NQuote.GetProductSizesI8025() :
                                              columnName == "Vertical8025" ? NQuote.GetProductSizesV8025() :
                                              columnName == "VerticalCentro8025" ? NQuote.GetProductSizesVC8025() : 
                                              NQuote.GetProductSizesPisaAlformbra8025();

                // Procesar los resultados de la consulta
                foreach (DataRow row in productSizesTable2.Rows)
                {
                    if (row["Tamaño"] != DBNull.Value)
                    {
                        decimal size = Convert.ToDecimal(row["Tamaño"], CultureInfo.InvariantCulture);
                        availableBars.Add(size);
                    }
                }
            }
        }

        private void btnOptimizar8025_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvOrdenProduccion8025.SelectedCells.Count == 0)
                {
                    MessageBox.Show("Debe elegir un proyecto antes de usar el optimizador.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // Mostrar el mensaje de confirmación
                    DialogResult result = MessageBox.Show("El optimizador está aún en desarrollo, ¿Desea continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    // Si el usuario elige "No", cerrar la ejecución del método
                    if (result == DialogResult.No)
                    {
                        return;
                    }

                    // Obtener la celda seleccionada
                    if (dgvOrdenProduccion8025.SelectedCells.Count > 0)
                    {
                        int columnIndex2 = dgvOrdenProduccion8025.SelectedCells[0].ColumnIndex;
                        string columnName2 = dgvOrdenProduccion8025.Columns[columnIndex2].Name;

                        // Diccionario con columnas válidas y sus respectivas columnas de cantidad
                        Dictionary<string, string> validColumns2 = new Dictionary<string, string>
                {
                    { "Cargador8025", "cantCargador8025" },
                    { "Umbral8025", "cantUmbral8025" },
                    { "Jamba8025", "cantJamba8025" },
                    { "Superior8025", "cantSuperior8025" },
                    { "Inferior8025", "cantInferior8025" },
                    { "Vertical8025", "cantVertical8025" },
                    { "VerticalCentro8025", "Cantidad" },
                    { "PisaAlfombra", "cantPisaAlfombra" }
                };

                        // Diccionario para almacenar longitudes requeridas por columna
                        Dictionary<string, List<(decimal length, int window)>> requiredLengthsDict2 = new Dictionary<string, List<(decimal length, int window)>>();
                        foreach (var column in validColumns2.Keys)
                        {
                            requiredLengthsDict2[column] = new List<(decimal length, int window)>();
                        }

                        // Diccionario para almacenar barras disponibles por columna
                        Dictionary<string, List<decimal>> availableBarsDict2 = new Dictionary<string, List<decimal>>();
                        foreach (var column in validColumns2.Keys)
                        {
                            availableBarsDict2[column] = new List<decimal>();
                        }

                        // Procesar filas
                        foreach (DataGridViewRow row in dgvOrdenProduccion8025.Rows)
                        {
                            int ventanaIndex = dgvOrdenProduccion8025.Columns["Ventana8025"].Index; // Índice de la columna de ventana
                            if (row.Cells[ventanaIndex].Value != null)
                            {
                                string ventanaValue = row.Cells[ventanaIndex].Value.ToString();
                                int ventana;
                                if (int.TryParse(ventanaValue, out ventana))
                                {
                                    foreach (var column in validColumns2.Keys)
                                    {
                                        int colIndex = dgvOrdenProduccion8025.Columns[column].Index;
                                        int cantIndex = dgvOrdenProduccion8025.Columns[validColumns2[column]].Index;

                                        if (row.Cells[colIndex].Value != null && row.Cells[cantIndex].Value != null)
                                        {
                                            string value = row.Cells[colIndex].Value.ToString();
                                            string cantValue = row.Cells[cantIndex].Value.ToString();
                                            decimal length;
                                            int cantidad;

                                            // Intentar convertir la cadena a decimal y la cantidad a int
                                            if ((decimal.TryParse(value, NumberStyles.Any, CultureInfo.GetCultureInfo("es-ES"), out length) ||
                                                 decimal.TryParse(value.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out length)) &&
                                                int.TryParse(cantValue, out cantidad))
                                            {
                                                for (int i = 0; i < cantidad; i++)
                                                {
                                                    requiredLengthsDict2[column].Add((length, ventana));
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        // Obtener los tamaños de producto de la base de datos y llenar las barras disponibles
                        foreach (var column in validColumns2.Keys)
                        {
                            FillAvailableBars2(column, availableBarsDict2[column], requiredLengthsDict2[column].Count);
                        }

                        string orden = txtOrden.Text;
                        string proyecto = cbProyecto.Text;

                        // Mostrar el formulario del optimizador
                        frmOpimizador8025 optimizerForm = new frmOpimizador8025(
                            requiredLengthsDict2["Cargador8025"].ToArray(), availableBarsDict2["Cargador8025"].ToArray(),
                            requiredLengthsDict2["Umbral8025"].ToArray(), availableBarsDict2["Umbral8025"].ToArray(),
                            requiredLengthsDict2["Jamba8025"].ToArray(), availableBarsDict2["Jamba8025"].ToArray(),
                            requiredLengthsDict2["Superior8025"].ToArray(), availableBarsDict2["Superior8025"].ToArray(),
                            requiredLengthsDict2["Inferior8025"].ToArray(), availableBarsDict2["Inferior8025"].ToArray(),
                            requiredLengthsDict2["Vertical8025"].ToArray(), availableBarsDict2["Vertical8025"].ToArray(),
                            requiredLengthsDict2["VerticalCentro8025"].ToArray(), availableBarsDict2["VerticalCentro8025"].ToArray(),
                            requiredLengthsDict2["PisaAlfombra"].ToArray(), availableBarsDict2["PisaAlfombra"].ToArray(),
                            orden, proyecto
                        );
                        optimizerForm.TopMost = true;
                        optimizerForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("La tabla del sistema 8025 está vacía. Debe de tener datos para su optimización.", "Tabla vacía", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

