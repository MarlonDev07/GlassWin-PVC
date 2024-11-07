using Dominio;
using Dominio.Model.ClassWindows;
using Dominio.PriceProduct;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Negocio.Client;
using Negocio.Company.Account;
using Negocio.Company.AdmProyecto;
using Negocio.Company.Bill;
using Negocio.Company.Quote;
using Negocio.LoadProduct;
using Negocio.Products;
using Negocio.Proveedor;
using Precentacion.User.Client;
using Precentacion.User.DashBoard;
using Precentacion.User.Quote.Quote;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using Image = iTextSharp.text.Image;

namespace Precentacion.User.Bill
{
    public partial class frmFacturar : Form
    {
        #region variables
        public decimal Subtotal = 0;
        public decimal IVA = 0;
        public decimal Total = 0;
        int IdClient;
        int idEmployer;
        DataTable dt = new DataTable();
        N_Client NClient = new N_Client();
        N_Quote NQuote = new N_Quote();
        N_BankAccount NBankAccount = new N_BankAccount();
        N_MoveBank NMoveBank = new N_MoveBank();
        N_CxC N_CxC = new N_CxC();
        N_Bill N_Bill = new N_Bill();
        N_AdmProyecto N_AdmProyecto = new N_AdmProyecto();
        string color;
        bool bandera = true;
        DataTable dtAluminioFijo;
        #endregion

        #region Constructor
        public frmFacturar()
        {
            InitializeComponent();
            CargarProveedor();
            tabControl.Selecting += new TabControlCancelEventHandler(tabControl_Selecting);
          
        }
        #endregion

        #region Initialize
        public void InitializeComponents_Click(object sender, EventArgs e)
        {
            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            
            loadWindows();
            LoadEmployer();
            dgvWindowsLoad();
            dgvRectificacionLoad();
            LoadAccount();
            MostrarPrecio();
            cargarDatosTotalDesglose();


        }
        public void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                List<clsClient> list = new List<clsClient>();
                list = NClient.ListClient(txtidClient.Text);
                if (list.Count == 0)
                {
                    DialogResult result = MessageBox.Show("No se encontro el Cliente ¿Desea Ver la Lista de Cliente?", "Cliente", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        frmManagerClient frm = new frmManagerClient();
                        frm.Show();
                    }
                }
                foreach (var item in list)
                {
                    if (item.IdClient.ToString() == txtidClient.Text && item.IdCompany == CompanyCache.IdCompany)
                    {
                        IdClient = item.IdClient;
                        txtidClient.Text = item.Name;
                        txtTelefono.Text = item.Phone;
                        txtAdreesClient.Text = item.Address;
                        txtEmail.Text = item.Correo;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al quere facturar Proforma Verifique que haya seleccionado una Proforma", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void loadWindows()
        {
            dt = NQuote.WindowsData(Convert.ToInt32(txtidQuote.Text));
        }
        private void dgvWindowsLoad()
        {
            // Cargar el DataGridView
            dgvGlass.DataSource = dt;

            //Cambiar el Alto
            dgvGlass.RowTemplate.Height = 300;

            // Permitir saltos de línea en el dgv
            dgvGlass.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // Ajustar el Ancho de la celda al ancho del Formulario
            dgvGlass.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Ocultar las columnas que no se necesitan
            dgvGlass.Columns[0].Visible = false;
            dgvGlass.Columns[1].Visible = true;
            dgvGlass.Columns[2].Visible = true;
            dgvGlass.Columns[3].Visible = false;
            dgvGlass.Columns[4].Visible = false;
            dgvGlass.Columns[5].Visible = false;
            dgvGlass.Columns[6].Visible = false;
            dgvGlass.Columns[7].Visible = false;
            dgvGlass.Columns[8].Visible = true;
            dgvGlass.Columns[9].Visible = false;
            dgvGlass.Columns[10].Visible = false;
            dgvGlass.Columns[11].Visible = false;
            dgvGlass.Columns[12].Visible = false;

            // Ordenar las columnas para que sea primero la 2 y luego la 1 y 8
            dgvGlass.Columns[1].DisplayIndex = 2;
            dgvGlass.Columns[2].DisplayIndex = 1;
            dgvGlass.Columns[8].DisplayIndex = 3;

            // Cambiar Nombres
            dgvGlass.Columns[1].HeaderText = "Descripción";
            dgvGlass.Columns[8].HeaderText = "Precio";
            dgvGlass.Columns[3].HeaderText = "Ancho";
            dgvGlass.Columns[4].HeaderText = "Alto";


            foreach (DataGridViewColumn column in dgvGlass.Columns)
            {
                Console.WriteLine("DGV GLASS \n");
                Console.WriteLine("Column Name: " + column.Name);
            }


        }
        private void CargarDesglose()
        {
            try
            {
                //Cargar en una lista Todos los Id de las Ventanas
                N_LoadProduct NLoadProduct = new N_LoadProduct();
                DataTable dtTotalDesglose = new DataTable();

                foreach (DataGridViewRow row in dgvGlass.Rows)
                {
                    DataTable dtAluminio = new DataTable();

                    //Validar que la celda no este vacia
                    if (row.Cells[0].Value == null)
                    {
                        continue;
                    }
                    //Obtener el Alto y Ancho de la Ventana
                    double Ancho = Convert.ToDouble(row.Cells[3].Value);
                    ClsWindows.Weight = Convert.ToDecimal(Ancho);

                    double Alto = Convert.ToDouble(row.Cells[4].Value);
                    ClsWindows.heigt = Convert.ToDecimal(Alto);


                    //Obtener el Color de la Ventana
                    string Color = row.Cells[6].Value.ToString();

                    //Obtener url
                    string urlImagen = row.Cells[2].Value.ToString();

                    //Obtener La cantidad de ventana el dato se encuentra en la descripcion
                    string DescripcionCantidad = row.Cells[1].Value.ToString();

                    string pattern = @"Cantidad:\s*(\d+)";
                    System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(DescripcionCantidad, pattern);
                    int Cantidad = 0;
                    if (match.Groups[1].Value != "")
                    {
                        Cantidad = Convert.ToInt32(match.Groups[1].Value);
                    }




                    // Patrón para extraer "Ancho Fijo"
                    string patternAnchoFijo = @"Ancho Fijo:\s*([\d,.]+)";
                    System.Text.RegularExpressions.Match matchAnchoFijo = System.Text.RegularExpressions.Regex.Match(DescripcionCantidad, patternAnchoFijo);
                    decimal anchoFijo = matchAnchoFijo.Success ? Convert.ToDecimal(matchAnchoFijo.Groups[1].Value) : 0;

                    // Patrón para extraer "Alto Fijo"
                    string patternAltoFijo = @"Alto Fijo:\s*([\d,.]+)";
                    System.Text.RegularExpressions.Match matchAltoFijo = System.Text.RegularExpressions.Regex.Match(DescripcionCantidad, patternAltoFijo);
                    decimal altoFijo = matchAltoFijo.Success ? Convert.ToDecimal(matchAltoFijo.Groups[1].Value) : 0;

                    // Patrón para extraer "Aluminio"
                    string patternMaterial = @"Aluminio:\s*(.+)";
                    System.Text.RegularExpressions.Match matchMaterial = System.Text.RegularExpressions.Regex.Match(DescripcionCantidad, patternMaterial);
                    string material = matchMaterial.Success ? matchMaterial.Groups[1].Value : string.Empty;

                    // Patrón para extraer "Divisiones"
                    string patternDivisiones = @"Divisiones:\s*(\d+)";
                    System.Text.RegularExpressions.Match matchDivisiones = System.Text.RegularExpressions.Regex.Match(DescripcionCantidad, patternDivisiones);
                    int divisiones = matchDivisiones.Success ? Convert.ToInt32(matchDivisiones.Groups[1].Value) : 0;

                    // Patrón para extraer "Vidrio"
                    string patternVidrioF = @"Vidrio Fijo:\s*(.+)";
                    System.Text.RegularExpressions.Match matchVidrio = System.Text.RegularExpressions.Regex.Match(DescripcionCantidad, patternVidrioF);
                    string vidrioFijo = matchVidrio.Success ? matchVidrio.Groups[1].Value : string.Empty;

                    // Patrón para extraer "Color"
                    string patternColor = @"Color:\s*(.+)";
                    System.Text.RegularExpressions.Match matchColor = System.Text.RegularExpressions.Regex.Match(DescripcionCantidad, patternColor);
                    string color = matchColor.Success ? matchColor.Groups[1].Value : string.Empty;
                    this.color = color;

                    // Patrón para extraer "Cerradura"
                    string patternCerradura = @"Cerradura:\s*(.+)";
                    System.Text.RegularExpressions.Match matchCerradura = System.Text.RegularExpressions.Regex.Match(DescripcionCantidad, patternCerradura);
                    string cerradura = matchCerradura.Success ? matchCerradura.Groups[1].Value : string.Empty;




                    //Verificar si es Prefabricado

                    //Obtener el Sistema de la Ventana
                    string Sistema = row.Cells[10].Value.ToString();
                    ClsWindows.System = Sistema;

                    //Obtener el Diseño de la Ventana
                    string Diseno = row.Cells[11].Value.ToString();
                    ClsWindows.Desing = Diseno;

                    //Obtener el Vidrio de la Ventana
                    string Vidrio = row.Cells[5].Value.ToString();


                    if (ClsWindows.System == "Vidrio Fijo")
                    {
                        string Descripcion = row.Cells[1].Value.ToString();
                        string Material = "";
                        if (Descripcion.Contains("1x2"))
                        {
                            Material = "1x2";
                        }
                        if (Descripcion.Contains("1 3/4x3"))
                        {
                            Material = "1 3/4x3";
                        }
                        if (Descripcion.Contains("1 3/4x4"))
                        {
                            Material = "1 3/4x4";
                        }
                        //Obtener el Total del Aluminio del Vidrio Fijo                      
                        dtAluminio = NLoadProduct.loadAluminioVentanaFijaDesglose(Color, Sistema, cbProveedorDesglose.SelectedValue.ToString(), Material);

                        //Obtener el Metraje del dt Aluminio y Multiplicarlo por la Cantidad de Ventanas
                        foreach (DataRow item in dtAluminio.Rows)
                        {
                            item["Metraje"] = Convert.ToDecimal(item["Metraje"]) * Convert.ToDecimal(Cantidad);
                        }
                    }
                    else
                    {
                        // Verificar si el sistema es "Ventila"
                        if (anchoFijo > 0)
                        {
                            string Descripcion = row.Cells[1].Value.ToString();

                            if (urlImagen.Contains("Curva tapa"))
                            {
                                // Obtener el Total del Aluminio del Vidrio Fijo                      
                                 dtAluminioFijo = NLoadProduct.LoadAluminioFijoDesglose(Color, "Vidrio Fijo", "Extralum", anchoFijo, altoFijo, "Curva tapa", divisiones);
                            }
                            else if (urlImagen.Contains("Curva x12"))
                            {
                                // Obtener el Total del Aluminio del Vidrio Fijo                      
                                dtAluminioFijo = NLoadProduct.LoadAluminioFijoDesglose(Color, "Vidrio Fijo", "Extralum", anchoFijo, altoFijo, "Curva x12", divisiones);
                            }
                            else 
                            {
                                // Obtener el Total del Aluminio del Vidrio Fijo                      
                                dtAluminioFijo = NLoadProduct.LoadAluminioFijoDesglose(Color, "Vidrio Fijo", "Extralum", anchoFijo, altoFijo, material, divisiones);
                            }

                     

                            // Obtener el Metraje del dtAluminioFijo y multiplicarlo por la Cantidad de Ventanas
                            foreach (DataRow item in dtAluminioFijo.Rows)
                            {
                                item["Metraje"] = Convert.ToDecimal(item["Metraje"]) * Convert.ToDecimal(Cantidad);
                            }

                            // Agregar las filas del dtAluminioFijo al dtAluminio original
                            dtAluminio.Merge(dtAluminioFijo);
                        }
                        DataTable dtAluminioSistema = new DataTable();
                        if (Sistema == "Prefabricado")
                        {
                            dtAluminioSistema = NLoadProduct.LoadPrefabricadoDesglose(Convert.ToInt32(row.Cells[0].Value));
                        }
                        else
                        {
                            // Obtener el Total del Aluminio                     
                             dtAluminioSistema = NLoadProduct.loadAluminioDesglose(Color, Sistema, cbProveedorDesglose.SelectedValue.ToString());
                        }

                        // Obtener el Metraje del dtAluminioSistema y multiplicarlo por la Cantidad de Ventanas                    
                        foreach (DataRow item in dtAluminioSistema.Rows)
                        {
                            if (Sistema == "Prefabricado")
                            {
                                item["Metraje"] = Convert.ToDecimal(item["Metraje"]);
                            }
                            else
                            {
                                item["Metraje"] = Convert.ToDecimal(item["Metraje"]) * Convert.ToDecimal(Cantidad);
                            }
                        
                        }

                        // Agregar las filas del dtAluminioSistema al dtAluminio original
                        dtAluminio.Merge(dtAluminioSistema);

                        // Eliminar filas donde la columna Metraje tenga un valor de 0
                        foreach (DataRow row2 in dtAluminio.Rows.Cast<DataRow>().ToList())
                        {
                            if (Convert.ToDecimal(row2["Metraje"]) == 0)
                            {
                                row2.Delete();
                            }
                        }

                        // Asegúrate de aceptar los cambios para que se eliminen las filas marcadas para eliminación
                        dtAluminio.AcceptChanges();
                    }
                    //Validar todos las Celdas del dtAluminio para agregarlas al dtTotalDesglose                    
                    if (dtTotalDesglose.Rows.Count == 0)
                    {
                        dtTotalDesglose = dtAluminio;
                    }
                    else
                    {
                        foreach (DataRow item in dtAluminio.Rows)
                        {
                            //Validar si la celda Description tiene el mismo valor y si es asi sumar el Metraje              
                            if (dtTotalDesglose.Select("Description = '" + item["Description"].ToString() + "'").Length > 0)
                            {
                                DataRow[] rows = dtTotalDesglose.Select("Description = '" + item["Description"].ToString() + "'");
                                rows[0]["Metraje"] = Convert.ToDecimal(rows[0]["Metraje"]) + Convert.ToDecimal(item["Metraje"]);

                            }
                            else
                            {
                                dtTotalDesglose.ImportRow(item);
                            }
                        }
                    }

                    //Obtener el Total de los Accesorios
                    DataTable dtAccesorios = new DataTable();

                    //Obtener el Total de los Accesorios            
                    dtAccesorios = NLoadProduct.loadAccesoriosDesglose(Sistema, cbProveedorDesglose.SelectedValue.ToString());

                    //Obtener el Metraje del dt Accesorios y Multiplicarlo por la Cantidad de Ventanas
                    foreach (DataRow item in dtAccesorios.Rows)
                    {
                        item["Metraje"] = Convert.ToDecimal(item["Metraje"]) * Convert.ToDecimal(Cantidad);


                    }

                    //Validar todos las Celdas del dtAccesorios para agregarlas al dtTotalDesglose                    
                    if (dtTotalDesglose.Rows.Count == 0)
                    {
                        dtTotalDesglose = dtAccesorios;
                    }
                    else
                    {
                        foreach (DataRow item in dtAccesorios.Rows)
                        {
                            //Validar si la celda Description tiene el mismo valor y si es asi sumar el Metraje                  
                            if (dtTotalDesglose.Select("Description = '" + item["Description"].ToString() + "'").Length > 0)
                            {
                                DataRow[] rows = dtTotalDesglose.Select("Description = '" + item["Description"].ToString() + "'");
                                rows[0]["Metraje"] = Convert.ToDecimal(rows[0]["Metraje"]) + Convert.ToDecimal(item["Metraje"]);


                            }
                            else
                            {
                                dtTotalDesglose.ImportRow(item);
                            }
                        }
                    }
                    // Declarar la variable dtVidrios antes de su uso
                    DataTable dtVidrios = new DataTable();

                    string proveedorVidrio = cbProveedorDesglose.Text;

                    if (vidrioFijo == "Vid 4 mm claro Alu")
                    {
                        proveedorVidrio = "Aluma";
                    }
                    else if (vidrioFijo.EndsWith("Ex"))
                    {
                        proveedorVidrio = "Extralum";
                    }
                    else if (vidrioFijo.EndsWith("Ext"))
                    {
                        proveedorVidrio = "Extralum";
                    }
                    else if (vidrioFijo.EndsWith("Alu"))
                    {
                        proveedorVidrio = "Alumas";
                    }
                    else if (vidrioFijo.EndsWith("Ma"))
                    {
                        proveedorVidrio = "Macopa";
                    }
                    else if (vidrioFijo.EndsWith("Carbone"))
                    {
                        proveedorVidrio = "Carbone";
                    }



                    // Verificar si el sistema es "Ventila"
                    if (anchoFijo > 0)
                    {
                        string Descripcion = row.Cells[1].Value.ToString();

                        // Obtener el Total del Vidrio Fijo
                        DataTable dtVidriosFijo = NLoadProduct.LoadPriceNewGlassDesglose(proveedorVidrio, vidrioFijo, anchoFijo, altoFijo);

                        // Obtener el Metraje del dtVidriosFijo y multiplicarlo por la Cantidad de Ventanas
                        foreach (DataRow item in dtVidriosFijo.Rows)
                        {
                            item["Metraje"] = Convert.ToDecimal(item["Metraje"]) * Convert.ToDecimal(Cantidad);
                        }

                        // Agregar las filas del dtVidriosFijo al dtVidrios original
                        dtVidrios.Merge(dtVidriosFijo);
                    }

                  

                    if (Vidrio == "Vid 4 mm claro Alu")
                    {
                        proveedorVidrio = "Aluma";
                    }
                    else if (Vidrio.EndsWith("Ex"))
                    {
                        proveedorVidrio = "Extralum";
                    }
                    else if (Vidrio.EndsWith("Ext"))
                    {
                        proveedorVidrio = "Extralum";
                    }
                    else if (Vidrio.EndsWith("Alu"))
                    {
                        proveedorVidrio = "Alumas";
                    }
                    else if (Vidrio.EndsWith("Ma"))
                    {
                        proveedorVidrio = "Macopa";
                    }
                    else if (Vidrio.EndsWith("Carbone"))
                    {
                        proveedorVidrio = "Carbone";
                    }


                    // Obtener el Total de los Vidrios (del sistema general)
                    DataTable dtVidriosSistema = NLoadProduct.loadPricesGlassDesglose(proveedorVidrio, Vidrio);

                    // Obtener el Metraje del dtVidriosSistema y multiplicarlo por la Cantidad de Ventanas
                    foreach (DataRow item in dtVidriosSistema.Rows)
                    {
                        item["Metraje"] = Convert.ToDecimal(item["Metraje"]) * Convert.ToDecimal(Cantidad);
                    }

                    // Agregar las filas del dtVidriosSistema al dtVidrios original
                    dtVidrios.Merge(dtVidriosSistema);

                    // Eliminar filas donde la columna Metraje tenga un valor de 0
                    foreach (DataRow row2 in dtVidrios.Rows.Cast<DataRow>().ToList())
                    {
                        if (Convert.ToDecimal(row2["Metraje"]) == 0)
                        {
                            row2.Delete();
                        }
                    }

                    // Asegúrate de aceptar los cambios para que se eliminen las filas marcadas para eliminación
                    dtVidrios.AcceptChanges();



                    // Validar todas las celdas del dtVidrios para agregarlas al dtTotalDesglose
                    if (dtTotalDesglose.Rows.Count == 0)
                    {
                        dtTotalDesglose = dtVidrios;
                    }
                    else
                    {
                        foreach (DataRow item in dtVidrios.Rows)
                        {
                            // Validar si la celda Description tiene el mismo valor y, si es así, sumar el Metraje
                            DataRow[] rows = dtTotalDesglose.Select("Description = '" + item["Description"].ToString() + "'");
                            if (rows.Length > 0)
                            {
                                rows[0]["Metraje"] = Convert.ToDecimal(rows[0]["Metraje"]) + Convert.ToDecimal(item["Metraje"]);
                            }
                            else
                            {
                                dtTotalDesglose.ImportRow(item);
                            }
                        }
                    }


                    // Declarar la variable dtVidrios antes de su uso
                    DataTable dtCerradura = new DataTable();
                    dtCerradura = NLoadProduct.LoadPricesLockDesglose(cbProveedorDesglose.SelectedValue.ToString(), cerradura);

                    foreach (DataRow item in dtCerradura.Rows)
                    {
                        item["Metraje"] = Convert.ToDecimal(item["Metraje"]) * Convert.ToDecimal(Cantidad);
                    }

                    // Eliminar filas donde la columna Metraje tenga un valor de 0
                    foreach (DataRow row2 in dtCerradura.Rows.Cast<DataRow>().ToList())
                    {
                        if (Convert.ToDecimal(row2["Metraje"]) == 0)
                        {
                            row2.Delete();
                        }
                    }
                    dtCerradura.AcceptChanges();

                    // Validar todas las celdas del dtVidrios para agregarlas al dtTotalDesglose
                    if (dtTotalDesglose.Rows.Count == 0)
                    {
                        dtTotalDesglose = dtCerradura;
                    }
                    else
                    {
                        foreach (DataRow item in dtCerradura.Rows)
                        {
                            // Validar si la celda Description tiene el mismo valor y, si es así, sumar el Metraje
                            DataRow[] rows = dtTotalDesglose.Select("Description = '" + item["Description"].ToString() + "'");
                            if (rows.Length > 0)
                            {
                                rows[0]["Metraje"] = Convert.ToDecimal(rows[0]["Metraje"]) + Convert.ToDecimal(item["Metraje"]);
                            }
                            else
                            {
                                dtTotalDesglose.ImportRow(item);
                            }
                        }
                    }

                }
                if (dtTotalDesglose.Columns.Contains("Metraje"))
                {
                    // Filtrar las filas con Metraje > 0
                    DataRow[] filasFiltradas = dtTotalDesglose.Select("Metraje > 0");

                    if (filasFiltradas.Length > 0)
                    {
                        // Si se encuentran filas, crear el nuevo DataTable
                        dtTotalDesglose = filasFiltradas.CopyToDataTable();
                    }
                    else
                    {
                        // Si no se encuentran filas, manejarlo según sea necesario
                        // dtTotalDesglose.Rows.Clear(); // Esta es una opción si deseas vaciar el DataTable.
                    }
                }
                else
                {
                    // Manejar el caso en que la columna "Metraje" no exista
                    // Podrías optar por ignorar el proceso o inicializar de alguna forma dtTotalDesglose
                }


                //Cargar el dtTotalAluminio en el dgv
                dgvDesglose.DataSource = dtTotalDesglose;

                //Redonder todos los Metrajes a dos decimales 
                foreach (DataGridViewRow row in dgvDesglose.Rows)
                {
                    row.Cells[2].Value = Convert.ToDecimal(row.Cells[2].Value).ToString("N2");
                }

            }

            catch (Exception)
            {
                MessageBox.Show("Error al Cargar el Desglose", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void ConfigDataGridDesglose()
        {
            try
            {
                //Ocultar las columnas que no se necesitan
                dgvDesglose.Columns[1].Visible = true;
                dgvDesglose.Columns[1].HeaderText = "Precio Venta";
                dgvDesglose.Columns[2].Visible = true;
                dgvDesglose.Columns[2].HeaderText = "Costo";
                //Agregar una Columna para el Tamaño
                DataGridViewTextBoxColumn Columna = new DataGridViewTextBoxColumn();
                Columna.HeaderText = "Tamaño";
                Columna.Name = "Tamaño";
                dgvDesglose.Columns.Add(Columna);

                //Agregar una Columna para el Cantidad Piezas 
                DataGridViewTextBoxColumn Columna2 = new DataGridViewTextBoxColumn();
                Columna2.HeaderText = "Cantidad Piezas";
                Columna2.Name = "Cantidad Piezas";
                Columna2.ReadOnly = true;
                dgvDesglose.Columns.Add(Columna2);

                //Eliminar la ultima fila la Que esta en Blanco
                dgvDesglose.AllowUserToAddRows = false;


            }
            catch (Exception)
            {
            }
           
        }      
        private void dgvRectificacionLoad()
        {
            //Cargar solo Url, Descripcion, Ancho y Alto en el dgv
            dgvRectificacion.DataSource = dt;

            //Permitir saltos de linea en el dgv
            dgvRectificacion.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            //Ajustar el Ancho de la celda al ancho del Formulario
            dgvRectificacion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //Ocultar las columnas que no se necesitan
            dgvRectificacion.Columns[0].Visible = false;
            dgvRectificacion.Columns[1].Visible = true;
            dgvRectificacion.Columns[2].Visible = true;
            dgvRectificacion.Columns[3].Visible = true;
            dgvRectificacion.Columns[4].Visible = true;
            dgvRectificacion.Columns[5].Visible = false;
            dgvRectificacion.Columns[6].Visible = false;
            dgvRectificacion.Columns[7].Visible = false;
            dgvRectificacion.Columns[8].Visible = false;
            dgvRectificacion.Columns[9].Visible = false;
            dgvRectificacion.Columns[10].Visible = false;
            dgvRectificacion.Columns[11].Visible = false;
            dgvRectificacion.Columns[12].Visible = false;

            //Ordenar las columas para que sea primero la 2 y luego la 1 y 8
            dgvRectificacion.Columns[1].DisplayIndex = 2;
            dgvRectificacion.Columns[2].DisplayIndex = 1;
            dgvRectificacion.Columns[8].DisplayIndex = 3;


            //Alto de la celda
            dgvRectificacion.RowTemplate.Height = 300;
        }
        private void MostrarPrecio()
        {
            txtTotal.Text = Total.ToString("c");
        }
        #endregion

        #region Events
        private int MetrosAPixeles = 50; // Ajusta esto según sea necesario
        private void dgvGlass_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 2)
            {
                var cellValue = dgvGlass.Rows[e.RowIndex].Cells[2].Value;

                if (cellValue != null && !string.IsNullOrEmpty(cellValue.ToString()))
                {
                    string rutaRelativa = cellValue.ToString();

                    // Imprimir ruta relativa para depuración
                    Console.WriteLine($"Ruta relativa: {rutaRelativa}");

                    // Obtener la versión actual de la aplicación
                    System.Version versionActual = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                    string versionActualString = $"GlassWin{versionActual.Major}.{versionActual.Minor}.{versionActual.Build}.{versionActual.Revision}";

                    // Reemplazar la versión en la ruta con la versión actual
                    string rutaCorregida = ReemplazarVersionEnRuta(rutaRelativa, versionActualString);

                    // Imprimir ruta corregida para depuración
                    Console.WriteLine($"Ruta corregida: {rutaCorregida}");

                    string directorioDeTrabajo = Directory.GetCurrentDirectory();
                    string rutaAbsoluta;

                    if (Path.IsPathRooted(rutaCorregida))
                    {
                        if (File.Exists(rutaCorregida))
                        {
                            rutaAbsoluta = rutaCorregida;
                        }
                        else
                        {
                            string fileName = Path.GetFileName(rutaCorregida);
                            rutaAbsoluta = Path.Combine(directorioDeTrabajo, "Images\\Windows", fileName);
                        }
                    }
                    else
                    {
                        rutaAbsoluta = Path.Combine(directorioDeTrabajo, rutaCorregida);
                        rutaAbsoluta = Path.GetFullPath(rutaAbsoluta);
                    }

                    // Imprimir ruta absoluta para depuración
                    Console.WriteLine($"Ruta absoluta: {rutaAbsoluta}");

                    if (File.Exists(rutaAbsoluta))
                    {
                        e.PaintBackground(e.CellBounds, true);

                        using (System.Drawing.Image img = System.Drawing.Image.FromFile(rutaAbsoluta))
                        {
                            decimal anchoEnMetros = ObtenerAncho(dgvGlass.Rows[e.RowIndex].Cells[1].Value.ToString());
                            decimal alturaEnMetros = ObtenerAlto(dgvGlass.Rows[e.RowIndex].Cells[1].Value.ToString());
                            int anchoVentana = (int)(anchoEnMetros * MetrosAPixeles);
                            int altoVentana = (int)(alturaEnMetros * MetrosAPixeles);

                            Console.WriteLine($"Ancho ventana en píxeles: {anchoVentana}, Alto ventana en píxeles: {altoVentana}");

                            int anchoImagen = anchoVentana;
                            int altoImagen = altoVentana;

                            if (anchoImagen == 0) anchoImagen = 200;
                            if (altoImagen == 0) altoImagen = 200;

                            Console.WriteLine($"Ancho imagen ajustada: {anchoImagen}, Alto imagen ajustada: {altoImagen}");

                            int x = e.CellBounds.Left + (e.CellBounds.Width - anchoImagen) / 2;
                            int y = e.CellBounds.Top + (e.CellBounds.Height - altoImagen) / 2;

                            e.Graphics.DrawImage(img, new System.Drawing.Rectangle(x, y, anchoImagen, altoImagen));
                        }

                        e.Handled = true;
                    }
                }
            }
        }

        private string ReemplazarVersionEnRuta(string ruta, string versionActual)
        {
            // Suponiendo que la parte de la versión siempre está en el formato "GlassWinX.X.X.XX"
            string patron = @"GlassWin\d+\.\d+\.\d+\.\d+";
            return System.Text.RegularExpressions.Regex.Replace(ruta, patron, versionActual);
        }



        private void rbSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEfectivo.Checked == true)
            {
                rbTransferencia.Checked = false;
                lblAcount.Visible = false;
                cbAccount.Visible = false;
            }

            if (rbTransferencia.Checked == true)
            {
                rbEfectivo.Checked = false;
                lblAcount.Visible = true;
                cbAccount.Visible = true;
            }

        }
        private void cbEmployer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEmployer.SelectedIndex != -1)
            {
                //Separar el nombre del empleado y el id
                string[] words = cbEmployer.Text.Split('-');
                idEmployer = Convert.ToInt32(words[0]);
            }
        }
        #endregion

        #region Metodos
        private void LoadEmployer()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = N_Bill.SelectEmployersSeller();
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        cbEmployer.Items.Add(row["EmployeeID"].ToString() + " - " + row["FirstName"].ToString());
                    }
                }
                else
                {
                    cbEmployer.Name = "No hay Vendedores";
                }

            }
            catch
            {
                cbEmployer.Name = "Error al Cargar";
            }

        }
        private void LoadAccount()
        {
            try
            {
                //Cargar las cuentas bancarias en un DataTable
                DataTable dt = new DataTable();
                dt = NBankAccount.ListBankAccount();
                if (dt != null)
                {
                    //Crear un Nombre con el IdAccount y el BanckEmisor y cargarlo en el ComboBox
                    foreach (DataRow row in dt.Rows)
                    {
                        cbAccount.Items.Add(row["IdAccount"].ToString() + " - " + row["BankEmisor"].ToString());
                    }
                }
                else
                {
                    cbAccount.Name = "No hay Cuentas";
                }


            }
            catch (Exception)
            {

                cbAccount.Name = "Error al Cargar";
            }

        }
        private bool Validatefield()
        {
            if (txtidClient.Text == "")
            {
                MessageBox.Show("Debe ingresar el Cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (txtProjetName.Text == "")
            {
                MessageBox.Show("Debe ingresar el Nombre del Proyecto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (txtidQuote.Text == "")
            {
                MessageBox.Show("Debe ingresar el Numero de Proforma", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (txtDate.Text == "")
            {
                MessageBox.Show("Debe ingresar la Fecha", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (rbEfectivo.Checked == false && rbTransferencia.Checked == false)
            {
                MessageBox.Show("Debe seleccionar una Forma de Pago", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (rbTransferencia.Checked == true && cbAccount.Text == "")
            {
                MessageBox.Show("Debe seleccionar una Cuenta Bancaria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (txtAddress.Text == "")
            {
                MessageBox.Show("Debe ingresar la Dirección", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            } else if (cbEmployer.Text == "")
            {
                MessageBox.Show("Debe Seleccionar un Vendedor", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {

            }

            return true;

        }

        #endregion

        #region Facturar
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //PREGUNTAR SI DESEA FACTURAR LA PROFORMA SELECCIONADA
            DialogResult result = MessageBox.Show("¿Desea Facturar la proforma n° " + txtidQuote.Text + "?", "Facturar Proforma", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (Validatefield())
                {
                    InsertSell();
                    Facturar();
                    //Validar si el frmDashUser esta abierto
                    frmDashUser frm = Application.OpenForms.OfType<frmDashUser>().FirstOrDefault();
                    if (frm != null)
                    {
                        frm.WindowState = FormWindowState.Normal;
                        this.Close();
                    }
                    else
                    {
                        frmDashUser frmDashUser = new frmDashUser();
                        frmDashUser.Show();
                        this.Close();
                    }
                }
            }
        }
        private void Facturar()
        {
            int IdBill = CreateBill();
            //Generar los documentos
            if (GeneratePDF() == true && UpdateStatusQuote() == true && CreateCxC(IdBill) == true)
            {
                if (rbTransferencia.Checked)
                {
                    GenerateMoveBanck();
                }
                //Mostrar mensaje de exito
                MessageBox.Show("Factura Generada Correctamente", "Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error al Generar los Documentos", "Factura", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool GeneratePDF()
        {
            try
            {
                GenerateFactura();
                GenerateRectificacion();
                GeneratePlanos();
                MessageBox.Show("Documentos Generados Correctamente", "Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }
        private bool GenerateMoveBanck()
        {
            try
            {
                //Obtener el IdAccount
                string[] IdAccount = cbAccount.Text.Split('-');
                //Obtener el IdQuote
                string Descripcion = "Factura del Proyecto:" + txtProjetName.Text;
                //Obtener el IdClient
                string typeMove = "Debito";
                //Obtener el Total
                decimal Ammount = Convert.ToDecimal(Total);
                //Obtener la Cliente
                string Client = txtidClient.Text;

                //Enviar los datos a la capa de Negocio
                NMoveBank.CreateMoveBank(Convert.ToInt64(IdAccount[0]), Descripcion, typeMove, Ammount, Client);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        private bool UpdateStatusQuote()
        {
            try
            {
                //Enviar el IdQuote a la capa de Negocio
                NQuote.UpdateQuoteStatus(Convert.ToInt32(txtidQuote.Text), "Factura");
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        private int CreateBill()
        {
            try
            {
                //Obtener el IdQuote
                int IdQuote = Convert.ToInt32(txtidQuote.Text);
                //Obtener la Fecha
                DateTime Date = Convert.ToDateTime(txtDate.Text);
                //Enviar los datos a la capa de Negocio
                N_Bill.InsertBill(IdQuote, IdClient, Date, Date);
                return N_Bill.getLastID();
            }
            catch (Exception)
            {
                return 0;
            }


        }
        private bool CreateCxC(int IdBill)
        {
            try
            {
                //Obtener el InitialBalance
                decimal initalize = Convert.ToDecimal(Total);
                //Obtener el Balance
                decimal Out = Convert.ToDecimal(Total);
                //Enviar los datos a la capa de Negocio
                N_CxC.InsertCxC(IdBill, initalize, Out, txtProjetName.Text.Trim());
                int IdCxc = N_CxC.LastIdCxC();
                InsertAdmProyecto(IdCxc);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool InsertSell()
        {
            bool Result = false;
            if (cbEmployer.Text != "No hay Vendedores")
            {
                decimal Amount = Convert.ToDecimal(Total);
                Result = N_Bill.InsertSell(idEmployer, Amount);
            }
            else
            {
                return true;
            }

            return Result;
        }
        private bool InsertAdmProyecto(int IdCxC)
        {
            try
            {
                N_AdmProyecto.InsertarAdmProyecto(IdCxC, txtProjetName.Text);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Desglose de Material
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (tabControl.SelectedIndex == 2)
                {
                    if (dgvDesglose.Rows.Count == 0)
                    {
                        CargarDesglose();
                        ConfigDataGridDesglose();
                        //Caragar el Tamaño de la Pieza
                        CargarTamañoPieza();
                    }

                }
            }

        private void dgvDesglose_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 4)
                {
                    if (dgvDesglose.Rows[e.RowIndex].Cells[3].Value != null)//2
                    {
                        // Limpieza del valor para eliminar espacios y caracteres no deseados
                        string metrajeStr = dgvDesglose.Rows[e.RowIndex].Cells[3].Value.ToString().Trim();//2
                        string tamañoStr = dgvDesglose.Rows[e.RowIndex].Cells[4].Value.ToString().Trim();

                        // Validar si Tamaño tiene punto en lugar de coma y reemplazarlo
                        if (tamañoStr.Contains("."))
                        {
                            tamañoStr = tamañoStr.Replace(".", ",");
                        }

                        // Convertir los valores a decimal
                        decimal metraje = Convert.ToDecimal(metrajeStr);
                        decimal tamaño = Convert.ToDecimal(tamañoStr);

                        // Calcular el resultado
                        decimal resultado = metraje / tamaño;
                        resultado = Math.Ceiling(resultado); // Redondear siempre a la unidad mayor

                        // Asignar el valor calculado a la celda correspondiente
                        dgvDesglose.Rows[e.RowIndex].Cells[5].Value = resultado;
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción y mostrar el mensaje de error
                MessageBox.Show($"Error al calcular el valor en la fila {e.RowIndex + 1}: {ex.Message}", "Error en el DataGridView", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void pdf1()
        {
                #region Crear el documento
            // Obtener el directorio del escritorio y las carpetas necesarias
            string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string CarpetaFactura = Path.Combine(escritorio, "Desglose Material");
            string carpetaNombre = Path.Combine(CarpetaFactura, txtidClient.Text.Trim());
            string NameFile = "Desglose de Material n° " + txtidQuote.Text + ".pdf";

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

            Document document = new Document();
            // Crea un nuevo objeto PdfWriter para escribir el documento en un archivo
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(rutaArchivoPDF, FileMode.Create));

            // Asigna el objeto PdfWriter al documento
            document.Open();
            #endregion
            try
            {
                #region Seleccion de Proveedor
                string Option = cbProveedorDesglose.SelectedValue.ToString();
                string NombreProvedor = "";
                string TelefonoProvedor = "";
                string CorreoProvedor = "";
                switch (Option)
                {
                    case "Extralum":
                        NombreProvedor = "Extralum";
                        TelefonoProvedor = "2277-1900";
                        CorreoProvedor = "aavila@extralum.co.cr";
                        break;
                    case "Macopa":
                        NombreProvedor = "Macopa";
                        TelefonoProvedor = "2010-7310";
                        CorreoProvedor = "proyectosvidrios@macopa.com";
                        break;
                    case "Espejos el Mundo":
                        NombreProvedor = "Espejos el Mundo";
                        TelefonoProvedor = "2293-4961";
                        CorreoProvedor = "N/A";
                        break;
                    default:
                        break;
                }
                #endregion

                #region Encabezado
                // Crea una tabla con dos columnas
                PdfPTable Encabezado = new PdfPTable(2);
                Encabezado.WidthPercentage = 120;
                string rutaLogo = "";

                //Vitro esparza
                if (CompanyCache.IdCompany == 3101623589 || CompanyCache.IdCompany == 3101623581)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\vitroEsparza.png";
                    rutaLogo = ruta + Url;

                }
                //Viteco
                if (CompanyCache.IdCompany == 503320196)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\Viteco.png";
                    rutaLogo = ruta + Url;

                }
                //MS
                if (CompanyCache.IdCompany == 204260627)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\ms.png";
                    rutaLogo = ruta + Url;

                }
                //Constru
                if (CompanyCache.IdCompany == 3101704274)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\constru.png";
                    rutaLogo = ruta + Url;

                }

                //Usuario de Prueba
                if (CompanyCache.IdCompany == 999999999)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\UsuarioPrueba.png";
                    rutaLogo = ruta + Url;

                }
                //Urbano
                if (CompanyCache.IdCompany == 110640721)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\urbano.png";
                    rutaLogo = ruta + Url;

                }

                //La familia
                if (CompanyCache.IdCompany == 114420180)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\familia.png";
                    rutaLogo = ruta + Url;

                }

                //glassmar
                if (CompanyCache.IdCompany == 3105806704)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\glassmar.png";
                    rutaLogo = ruta + Url;

                }
                //laCosta
                if (CompanyCache.IdCompany == 109650325)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\laCosta.png";
                    rutaLogo = ruta + Url;

                }
                //V Dayra
                if (CompanyCache.IdCompany == 9699999999)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\vdayra.png";
                    rutaLogo = ruta + Url;

                }
                //Usuario de Nel
                if (CompanyCache.IdCompany == 205520679)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VidriosMartinez.png";
                    rutaLogo = ruta + Url;

                }
                //Usuario de Nel Fin
                //Prefalum, cedula juridica de prueba
                if (CompanyCache.IdCompany == 111111111)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\Prefalum2.png";
                    rutaLogo = ruta + Url;

                }
                //Vidrios Albo
                if (CompanyCache.IdCompany == 3102154177)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\albo.png";
                    rutaLogo = ruta + Url;

                }
                //Mercado del vidrio
                if (CompanyCache.IdCompany == 3102879949)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\mercadoVidrio.png";
                    rutaLogo = ruta + Url;

                }
                //Vidriera Palmares, cedula juridica de prueba
                if (CompanyCache.IdCompany == 222222222)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VidrieraPalmares.png";
                    rutaLogo = ruta + Url;

                }
                //Perfect Glass, cedula juridica de prueba
                if (CompanyCache.IdCompany == 333333333)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\PerfectGlass.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 31025820)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\AluviLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 3101794685)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\RioClaroLogo.png";
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
                if (CompanyCache.IdCompany == 1230123)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\GlassWinLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 25550555)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VitroLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 31028013)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\InnovaLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 111560456)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\DialexLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 310108681)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VidrioCentroLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 310171783)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VidriosVegaLogo.png";
                    rutaLogo = ruta + Url;

                }
                PdfPCell imageCell = new PdfPCell(iTextSharp.text.Image.GetInstance(rutaLogo));
                imageCell.Border = PdfPCell.NO_BORDER;
                imageCell.FixedHeight = 120f; // Ajusta la altura de la imagen
                imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
                Encabezado.AddCell(imageCell);

                // Crea un nuevo objeto Font para los textos
                iTextSharp.text.Font titleFont = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 19, iTextSharp.text.Font.BOLD, BaseColor.GRAY);
                iTextSharp.text.Font textFont = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font textFont2 = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 9, iTextSharp.text.Font.NORMAL, BaseColor.GRAY);
                // Agrega los textos a la segunda celda
                PdfPCell textCell = new PdfPCell();
                textCell.Border = PdfPCell.NO_BORDER;

                // Alinea el contenido de la celda al centro
                textCell.HorizontalAlignment = Element.ALIGN_RIGHT;

                // Agrega el párrafo y los chunks al documento
                Paragraph paragraph = new Paragraph();
                paragraph.Add(new Chunk(CompanyCache.Name, titleFont));
                paragraph.Add(Chunk.NEWLINE);// Salto de línea
                if (CompanyCache.IdCompany == 3101623589 || CompanyCache.IdCompany == 3101623581)
                {
                    paragraph.Add(new Chunk("Cédula Jurídica : 3-101-623589", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Ubicados en: " + CompanyCache.Address, textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Teléfono: " + "2635-5510", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Correo: " + "Vitroesparzafacturadigital@outlook.com", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                }
                else if (CompanyCache.IdCompany == 3101704274)
                {
                    paragraph.Add(new Chunk("Cédula Jurídica : 3-101-704274", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Ubicados en: " + "Naranjo, San Miguel", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Teléfono: " + " 7010-5184 ", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Correo: " + "construserviciosdelnorte@gmail.com", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                }


                else if (CompanyCache.IdCompany == 999999999)
                {
                    paragraph.Add(new Chunk("Cédula Jurídica : 9-999-99999", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Ubicados en: " + CompanyCache.Address, textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Teléfono: " + CompanyCache.Phone, textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Correo: " + "UsuarioPrueba@gmail.com", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                }
                else if (CompanyCache.IdCompany == 110640721)
                {
                    paragraph.Add(new Chunk("Cédula Jurídica : 1-106-40721", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Ubicados en: " + "Barrio Cuba, San José, Costa Rica", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Teléfono: " + "8580-0953", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Correo: " + "vidriourbano@gmail.com", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                }
                else if (CompanyCache.IdCompany == 114420180)
                {
                    paragraph.Add(new Chunk("Cédula Jurídica : 1-144-20180", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Ubicados en: " + "DECOPLAZA local 3, La Unión, Costa Rica", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Teléfono: " + "8385-4893", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Correo: " + "gajosue@hotmail.com", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                }

                else if (CompanyCache.IdCompany == 109650325)
                {
                    paragraph.Add(new Chunk("Cédula Jurídica : 1-096-50325", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Ubicados en: " + "Las Huacas, Guanacaste, Costa Rica", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Teléfono: " + "6163 6730", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Correo: " + "lacosta@gmail.com", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                }
                else if (CompanyCache.IdCompany == 9699999999)
                {
                    paragraph.Add(new Chunk("Cédula Jurídica : 3-101-785963", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Ubicados en: " + "San Pablo de Heredia, C.R.", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Teléfono: " + "2101-4081 / 8462-8462", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Correo: " + "info@vidriosdayra.com / solucionesdayra@gmail.com", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                }
                else if (CompanyCache.IdCompany == 205150849)
                {
                    paragraph.Add(new Chunk("Cédula Jurídica : 3-101-897998", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Ubicados en: " + CompanyCache.Address, textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Teléfonos: " + CompanyCache.Phone, textFont));
                    paragraph.Add(Chunk.NEWLINE);
                }
                else if (CompanyCache.IdCompany == 31028013)
                {

                    paragraph.Add(new Chunk("Cédula Jurídica :" + "3-102-801388", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("650 METROS NORESTE Y 350 METROS NOROESTE DE KFC, BOSQUES DON JOSE, NICOYA.", textFont2));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Teléfonos: " + CompanyCache.Phone, textFont));
                    paragraph.Add(Chunk.NEWLINE);
                }
                else if (CompanyCache.IdCompany == 204260627)
                {
                    paragraph.Add(new Chunk("Cédula Jurídica : 2-042-60627", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Ubicados en: " + CompanyCache.Address, textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Teléfono: " + "2042-60627", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Correo: " + "marvinsalazar@gmail.com", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                }
                else if (CompanyCache.IdCompany == 111111111)
                {
                    paragraph.Add(new Chunk("EL COYOL ALAJUELA.\r\n", textFont2));
                    paragraph.Add(Chunk.NEWLINE);

                    paragraph.Add(new Chunk("Cédula Jurídica :" + "1-111-11111", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Whatsapp: +(506) " + "6134 7128", textFont));
                    paragraph.Add(new Chunk("Teléfono: +(506) " + CompanyCache.Phone, textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Correo: " + "info@prefalumcr.com", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Correo: " + "ventas@prefalumcr.com", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                }
                else if (CompanyCache.IdCompany == 3102154177)
                {
                    paragraph.Add(new Chunk("75 Mts Este de Mas X Menos, Rincón de Arias.", textFont2));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Cédula Jurídica :" + "3-102-154177", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Teléfono: +(506) " + "24940866 / 24944306", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                }

                else if (CompanyCache.IdCompany == 222222222)
                {
                    paragraph.Add(new Chunk("PALMARES, COSTA RICA.\r\n", textFont2));
                    paragraph.Add(Chunk.NEWLINE);

                    paragraph.Add(new Chunk("Cédula Jurídica :" + "3-101-176270", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Teléfono: +(506) " + CompanyCache.Phone, textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Correo: " + "info@vidrierapalmares.com", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Sitio Web: " + "http://www.vidrierapalmares.com/", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                }
                else if (CompanyCache.IdCompany == 333333333)
                {
                    paragraph.Add(new Chunk("SAN RAMÓN, ALAJUELA.\r\n", textFont2));
                    paragraph.Add(Chunk.NEWLINE);

                    paragraph.Add(new Chunk("Cédula Jurídica :" + "3-333-33333", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Teléfono: +(506) " + CompanyCache.Phone, textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("WhatsApp: +(506) " + "8671 5008", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Correo: " + "crperfectglass@gmail.com", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                }
                else if (CompanyCache.IdCompany == 503320196)
                {
                    paragraph.Add(new Chunk("Cédula Jurídica : 5-0332-0196", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Ubicados en: " + "Frente escual de Los Llanos.", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Teléfono: " + "+(506) 8751-7492/ 6337-2024", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Correo: " + "Vitecosr@gmail.com", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                }
                else
                {
                    paragraph.Add(new Chunk("Cédula Jurídica :" + CompanyCache.IdCompany, textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Ubicados en: " + CompanyCache.Address, textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Teléfonos: " + CompanyCache.Phone, textFont));
                    paragraph.Add(Chunk.NEWLINE);
                }
                textCell.AddElement(paragraph);
                Encabezado.AddCell(textCell);

                // Establece el ancho de la celda de la tabla (ajusta según tus necesidades)
                Encabezado.SetWidths(new float[] { 3f, 4f }); // Primer valor es el ancho de la celda de la imagen

                // Agrega la tabla al documento
                document.Add(Encabezado);

                

                document.Add(new Paragraph(" "));
                #endregion

                #region Tabla de Informacion 
                // Crear una tabla para los datos del proyecto y la información del cliente
                PdfPTable datosTable = new PdfPTable(2);
                datosTable.TotalWidth = 500f; // Ajusta el ancho total según tus necesidades
                datosTable.LockedWidth = true;

                // Celda 1: Datos del Proyecto
                PdfPCell cellDatosProyecto = new PdfPCell(new Phrase("Datos del Proyecto", FontFactory.GetFont(FontFactory.HELVETICA, 16, BaseColor.WHITE)))
                {
                    BackgroundColor = new BaseColor(70, 130, 180),
                    BorderWidth = 1f,
                    //Colspan = 1, // Fusionar una columna para "Datos del Proyecto"
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                };
                datosTable.AddCell(cellDatosProyecto);

                // Celda 2: Información del Cliente
                PdfPCell cellDatosCliente = new PdfPCell(new Phrase("Información del Provedor", FontFactory.GetFont(FontFactory.HELVETICA, 16, BaseColor.WHITE)))
                {
                    BackgroundColor = new BaseColor(70, 130, 180),
                    BorderWidth = 1f,
                    Colspan = 1, // Fusionar una columna para "Información del Cliente"
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellDatosCliente);

                // Celda 3: Etiqueta "Cotización"
                PdfPCell cellEtiquetaCotizacion = new PdfPCell(new Phrase("Cotización: " + txtidQuote.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    BorderWidth = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaCotizacion);

                // Celda 4: Etiqueta "Cliente"
                PdfPCell cellEtiquetaCliente = new PdfPCell(new Phrase("Provedor: " + NombreProvedor, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    BorderWidth = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaCliente);

                // Celda 5: Etiqueta "Forma Pago"
                PdfPCell cellEtiquetaFormaPago = new PdfPCell(new Phrase("Fecha: " + txtDate.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    BorderWidth = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaFormaPago);

                // Celda 6: Etiqueta "Teléfono"
                PdfPCell cellEtiquetaTelefono = new PdfPCell(new Phrase("Teléfono: " + TelefonoProvedor, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    BorderWidth = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaTelefono);

                // Celda 6: Etiqueta "Dirección"
                PdfPCell cellEtiquetaDireccion = new PdfPCell(new Phrase("Proyecto: " + txtProjetName.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    BorderWidth = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaDireccion);

                // Celda 7: Etiqueta "Correo"
                PdfPCell cellEtiquetaCorreo = new PdfPCell(new Phrase("Correo: " + CorreoProvedor, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    BorderWidth = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaCorreo);
                document.Add(datosTable);
                document.Add(new Paragraph(" "));

                #endregion

                #region Tabla de Productos
                #region Nombres con diferentes categorias
                N_LoadProduct NLoadProduct = new N_LoadProduct();
                DataTable dtAluminio = new DataTable();
                DataTable dtAccesorio = new DataTable();
                DataTable dtVidrio = new DataTable();

                dtAluminio = NLoadProduct.loadAluminioDesglose();
                dtAccesorio = NLoadProduct.loadAccesorioDesglose();
                dtVidrio = NLoadProduct.loadVidrioDesglose();
                #endregion

                // 1. Agregar la columna "Categoría" a dgvDesglose si aún no existe
                if (!dgvDesglose.Columns.Contains("Categoría"))
                {
                    dgvDesglose.Columns.Add("Categoría", "Categoría");
                }

                // 2. Asignar la categoría basada en la descripción
                foreach (DataGridViewRow row in dgvDesglose.Rows)
                {
                    if (row.Cells["Description"].Value != null)
                    {
                        string description = row.Cells["Description"].Value.ToString();
                        string categoria = "";

                        // Verificar si la descripción existe en dtAluminio
                        if (dtAluminio.AsEnumerable().Any(r => r.Field<string>("Description") == description))
                        {
                            categoria = "Aluminio";
                        }
                        // Verificar si la descripción existe en dtAccesorio
                        else if (dtAccesorio.AsEnumerable().Any(r => r.Field<string>("Description") == description))
                        {
                            categoria = "Accesorios";
                        }
                        // Verificar si la descripción existe en dtVidrio
                        else if (dtVidrio.AsEnumerable().Any(r => r.Field<string>("Description") == description))
                        {
                            categoria = "Vidrio";
                        }

                        // Asignar la categoría en la columna "Categoría"
                        row.Cells["Categoría"].Value = categoria;
                    }
                }

                // 3. Crear una lista ordenada según la categoría
                var orderedRows = dgvDesglose.Rows.Cast<DataGridViewRow>()
                    .Where(row => row.Cells["Categoría"].Value != null)
                    .OrderBy(row => row.Cells["Categoría"].Value.ToString())
                    .ToList();




                // 4. Agregar los datos del dgvDesglose a la tabla de PDF
                PdfPTable tabla = new PdfPTable(dgvDesglose.Columns.Count);
                tabla.TotalWidth = 500f; // Ajusta el ancho total según tus necesidades
                tabla.LockedWidth = true;
                float[] tablaW = { 85, 0, 0, 30, 30, 33, 32, 32, 0 }; // Ancho de las columnas, incluyendo la nueva columna "Categoría"


                for (int i = 0; i < dgvDesglose.Columns.Count; i++)
                {
                    string nombreColumna = dgvDesglose.Columns[i].HeaderText;
                    float anchoColumna = tablaW[i];
                    Console.WriteLine($"Columna: {nombreColumna}, Ancho asignado: {anchoColumna}");
                }


                tabla.SetWidths(tablaW);



                // Agregar encabezados de columna
                /*for (int i = 0; i < dgvDesglose.Columns.Count; i++)
                {
                    PdfPCell celda = new PdfPCell(new Phrase(dgvDesglose.Columns[i].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 13, BaseColor.WHITE))); // Reducimos el tamaño a 13 puntos
                    celda.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda.BackgroundColor = new BaseColor(70, 130, 180);
                    tabla.AddCell(celda);
                }*/

                // Agrupar las filas por categoría
                var groupedRows = orderedRows.GroupBy(row => row.Cells["Categoría"].Value.ToString());

                // Crear una tabla separada para cada categoría
                foreach (var group in groupedRows)
                {
                    // Agregar un título para la categoría con fondo azul y letra blanca
                    PdfPCell categoryTitleCell = new PdfPCell(new Phrase(group.Key, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.WHITE)));
                    categoryTitleCell.Colspan = dgvDesglose.Columns.Count; // Hacer que la celda ocupe todas las columnas
                    categoryTitleCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    categoryTitleCell.PaddingTop = 10f; // Agregar un espacio superior
                    categoryTitleCell.PaddingBottom = 10f; // Agregar un espacio inferior

                    // Establecer el color de fondo azul y el borde blanco
                    categoryTitleCell.BackgroundColor = new BaseColor(70, 130, 180); // Azul (RGB: 0, 102, 204)
                    categoryTitleCell.BorderColor = BaseColor.WHITE;

                    tabla.AddCell(categoryTitleCell);

                    // Agregar los encabezados de las columnas
                    foreach (DataGridViewColumn column in dgvDesglose.Columns)
                    {
                        PdfPCell headerCell = new PdfPCell(new Phrase(column.HeaderText, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
                        headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        tabla.AddCell(headerCell);
                    }

                    // Agregar las filas de datos para esta categoría
                    foreach (var row in group)
                    {
                        for (int j = 0; j < dgvDesglose.Columns.Count; j++)
                        {
                            if (row.Cells[j].Value != null)
                            {
                                PdfPCell cell = null;

                                if (dgvDesglose.Columns[j].HeaderText == "Description")
                                {
                                    // Para la columna "Descripción", alinea el texto a la izquierda
                                    cell = new PdfPCell(new Phrase(row.Cells[j].Value.ToString(), FontFactory.GetFont(FontFactory.HELVETICA)));
                                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                }
                                else
                                {
                                    // Para otras columnas, mantén el texto centrado
                                    cell = new PdfPCell(new Phrase(row.Cells[j].Value.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 12)));
                                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                }

                                // Ajusta el tamaño de las celdas
                                cell.FixedHeight = 30f; // Aumentamos la altura a 30 unidades
                                cell.PaddingLeft = 5f; // Agrega un relleno a la izquierda para alinear el texto correctamente

                                // Centrar contenido verticalmente
                                cell.VerticalAlignment = Element.ALIGN_CENTER;
                                tabla.AddCell(cell);
                            }
                        }
                    }

                    // Agregar un espacio entre tablas
                    PdfPCell emptyCell = new PdfPCell(new Phrase(" "));
                    emptyCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    emptyCell.Colspan = dgvDesglose.Columns.Count;
                    emptyCell.FixedHeight = 10f;
                    tabla.AddCell(emptyCell);
                }







                // Agregar la tabla al documento
                document.Add(tabla);
                #endregion


                //Cerrar el documento
                document.Close();
                MessageBox.Show("Desglose de Material Generado Correctamente", "Desglose de Material", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GenerarPdfHojaProduccion();

            }
            catch (Exception)
            {
                document.Close();

            }
        }

        public void pdf2()
        {
            #region Crear el documento
            // Obtener el directorio del escritorio y las carpetas necesarias
            string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string CarpetaFactura = Path.Combine(escritorio, "Desglose Material");
            string carpetaNombre = Path.Combine(CarpetaFactura, txtidClient.Text.Trim());
            string NameFile = "Desglose de Material n° " + txtidQuote.Text + ".pdf";

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

            Document document = new Document();
            // Crea un nuevo objeto PdfWriter para escribir el documento en un archivo
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(rutaArchivoPDF, FileMode.Create));

            // Asigna el objeto PdfWriter al documento
            document.Open();
            #endregion
            try
            {
                #region Seleccion de Proveedor
                string Option = cbProveedorDesglose.SelectedValue.ToString();
                string NombreProvedor = "";
                string TelefonoProvedor = "";
                string CorreoProvedor = "";
                switch (Option)
                {
                    case "Extralum":
                        NombreProvedor = "Extralum";
                        TelefonoProvedor = "2277-1900";
                        CorreoProvedor = "aavila@extralum.co.cr";
                        break;
                    case "Macopa":
                        NombreProvedor = "Macopa";
                        TelefonoProvedor = "2010-7310";
                        CorreoProvedor = "proyectosvidrios@macopa.com";
                        break;
                    case "Espejos el Mundo":
                        NombreProvedor = "Espejos el Mundo";
                        TelefonoProvedor = "2293-4961";
                        CorreoProvedor = "N/A";
                        break;
                    default:
                        break;
                }
                #endregion

                #region Encabezado
                // Crea una tabla con dos columnas
                PdfPTable Encabezado = new PdfPTable(2);
                Encabezado.WidthPercentage = 120;
                string rutaLogo = "";

                //Vitro esparza
                if (CompanyCache.IdCompany == 3101623589 || CompanyCache.IdCompany == 3101623581)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\vitroEsparza.png";
                    rutaLogo = ruta + Url;

                }
                //Viteco
                if (CompanyCache.IdCompany == 503320196)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\Viteco.png";
                    rutaLogo = ruta + Url;

                }
                //MS
                if (CompanyCache.IdCompany == 204260627)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\ms.png";
                    rutaLogo = ruta + Url;

                }
                //Constru
                if (CompanyCache.IdCompany == 3101704274)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\constru.png";
                    rutaLogo = ruta + Url;

                }

                //Usuario de Prueba
                if (CompanyCache.IdCompany == 999999999)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\UsuarioPrueba.png";
                    rutaLogo = ruta + Url;

                }
                //Urbano
                if (CompanyCache.IdCompany == 110640721)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\urbano.png";
                    rutaLogo = ruta + Url;

                }

                //La familia
                if (CompanyCache.IdCompany == 114420180)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\familia.png";
                    rutaLogo = ruta + Url;

                }

                //glassmar
                if (CompanyCache.IdCompany == 3105806704)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\glassmar.png";
                    rutaLogo = ruta + Url;

                }
                //laCosta
                if (CompanyCache.IdCompany == 109650325)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\laCosta.png";
                    rutaLogo = ruta + Url;

                }
                //V Dayra
                if (CompanyCache.IdCompany == 9699999999)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\vdayra.png";
                    rutaLogo = ruta + Url;

                }
                //Usuario de Nel
                if (CompanyCache.IdCompany == 205520679)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VidriosMartinez.png";
                    rutaLogo = ruta + Url;

                }
                //Usuario de Nel Fin
                //Prefalum, cedula juridica de prueba
                if (CompanyCache.IdCompany == 111111111)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\Prefalum2.png";
                    rutaLogo = ruta + Url;

                }
                //Vidrios Albo
                if (CompanyCache.IdCompany == 3102154177)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\albo.png";
                    rutaLogo = ruta + Url;

                }
                //Mercado del vidrio
                if (CompanyCache.IdCompany == 3102879949)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\mercadoVidrio.png";
                    rutaLogo = ruta + Url;

                }
                //Vidriera Palmares, cedula juridica de prueba
                if (CompanyCache.IdCompany == 222222222)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VidrieraPalmares.png";
                    rutaLogo = ruta + Url;

                }
                //Perfect Glass, cedula juridica de prueba
                if (CompanyCache.IdCompany == 333333333)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\PerfectGlass.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 31025820)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\AluviLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 3101794685)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\RioClaroLogo.png";
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
                if (CompanyCache.IdCompany == 1230123)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\GlassWinLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 25550555)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VitroLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 31028013)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\InnovaLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 111560456)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\DialexLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 310108681)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VidrioCentroLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 310171783)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VidriosVegaLogo.png";
                    rutaLogo = ruta + Url;

                }
                PdfPCell imageCell = new PdfPCell(iTextSharp.text.Image.GetInstance(rutaLogo));
                imageCell.Border = PdfPCell.NO_BORDER;
                imageCell.FixedHeight = 120f; // Ajusta la altura de la imagen
                imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
                Encabezado.AddCell(imageCell);

                // Crea un nuevo objeto Font para los textos
                iTextSharp.text.Font titleFont = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 19, iTextSharp.text.Font.BOLD, BaseColor.GRAY);
                iTextSharp.text.Font textFont = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font textFont2 = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 9, iTextSharp.text.Font.NORMAL, BaseColor.GRAY);
                // Agrega los textos a la segunda celda
                PdfPCell textCell = new PdfPCell();
                textCell.Border = PdfPCell.NO_BORDER;

                // Alinea el contenido de la celda al centro
                textCell.HorizontalAlignment = Element.ALIGN_RIGHT;

                // Agrega el párrafo y los chunks al documento
                Paragraph paragraph = new Paragraph();
                paragraph.Add(new Chunk(CompanyCache.Name, titleFont));
                paragraph.Add(Chunk.NEWLINE);// Salto de línea
                if (CompanyCache.IdCompany == 3101623589 || CompanyCache.IdCompany == 3101623581)
                {
                    paragraph.Add(new Chunk("Cédula Jurídica : 3-101-623589", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Ubicados en: " + CompanyCache.Address, textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Teléfono: " + "2635-5510", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Correo: " + "Vitroesparzafacturadigital@outlook.com", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                }
                else if (CompanyCache.IdCompany == 3101704274)
                {
                    paragraph.Add(new Chunk("Cédula Jurídica : 3-101-704274", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Ubicados en: " + "Naranjo, San Miguel", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Teléfono: " + " 7010-5184 ", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Correo: " + "construserviciosdelnorte@gmail.com", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                }
                else if (CompanyCache.IdCompany == 110640721)
                {
                    paragraph.Add(new Chunk("Cédula Jurídica : 1-106-40721", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Ubicados en: " + "Barrio Cuba, San José, Costa Rica", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Teléfono: " + "8580-0953", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Correo: " + "vidriourbano@gmail.com", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                }

                else if (CompanyCache.IdCompany == 999999999)
                {
                    paragraph.Add(new Chunk("Cédula Jurídica : 9-999-99999", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Ubicados en: " + CompanyCache.Address, textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Teléfono: " + CompanyCache.Phone, textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Correo: " + "UsuarioPrueba@gmail.com", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                }

                else if (CompanyCache.IdCompany == 114420180)
                {
                    paragraph.Add(new Chunk("Cédula Jurídica : 1-144-20180", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Ubicados en: " + "DECOPLAZA local 3, La Unión, Costa Rica", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Teléfono: " + "8385-4893", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Correo: " + "gajosue@hotmail.com", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                }

                else if (CompanyCache.IdCompany == 109650325)
                {
                    paragraph.Add(new Chunk("Cédula Jurídica : 1-096-50325", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Ubicados en: " + "Las Huacas, Guanacaste, Costa Rica", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Teléfono: " + "6163 6730", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Correo: " + "lacosta@gmail.com", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                }
                else if (CompanyCache.IdCompany == 9699999999)
                {
                    paragraph.Add(new Chunk("Cédula Jurídica : 3-101-785963", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Ubicados en: " + "San Pablo de Heredia, C.R.", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Teléfono: " + "2101-4081 / 8462-8462", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Correo: " + "info@vidriosdayra.com / solucionesdayra@gmail.com", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                }
                else if (CompanyCache.IdCompany == 205150849)
                {
                    paragraph.Add(new Chunk("Cédula Jurídica : 3-101-897998", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Ubicados en: " + CompanyCache.Address, textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Teléfonos: " + CompanyCache.Phone, textFont));
                    paragraph.Add(Chunk.NEWLINE);
                }
                else if (CompanyCache.IdCompany == 31028013)
                {

                    paragraph.Add(new Chunk("Cédula Jurídica :" + "3-102-801388", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("650 METROS NORESTE Y 350 METROS NOROESTE DE KFC, BOSQUES DON JOSE, NICOYA.", textFont2));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Teléfonos: " + CompanyCache.Phone, textFont));
                    paragraph.Add(Chunk.NEWLINE);
                }
                else if (CompanyCache.IdCompany == 204260627)
                {
                    paragraph.Add(new Chunk("Cédula Jurídica : 2-042-60627", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Ubicados en: " + CompanyCache.Address, textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Teléfono: " + "2042-60627", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Correo: " + "marvinsalazar@gmail.com", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                }
                else if (CompanyCache.IdCompany == 111111111)
                {
                    paragraph.Add(new Chunk("EL COYOL ALAJUELA.\r\n", textFont2));
                    paragraph.Add(Chunk.NEWLINE);

                    paragraph.Add(new Chunk("Cédula Jurídica :" + "1-111-11111", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Whatsapp: +(506) " + "6134 7128", textFont));
                    paragraph.Add(new Chunk("Teléfono: +(506) " + CompanyCache.Phone, textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Correo: " + "info@prefalumcr.com", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Correo: " + "ventas@prefalumcr.com", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                }
                else if (CompanyCache.IdCompany == 3102154177)
                {
                    paragraph.Add(new Chunk("75 Mts Este de Mas X Menos, Rincón de Arias.", textFont2));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Cédula Jurídica :" + "3-102-154177", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Teléfono: +(506) " + "24940866 / 24944306", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                }

                else if (CompanyCache.IdCompany == 222222222)
                {
                    paragraph.Add(new Chunk("PALMARES, COSTA RICA.\r\n", textFont2));
                    paragraph.Add(Chunk.NEWLINE);

                    paragraph.Add(new Chunk("Cédula Jurídica :" + "3-101-176270", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Teléfono: +(506) " + CompanyCache.Phone, textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Correo: " + "info@vidrierapalmares.com", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Sitio Web: " + "http://www.vidrierapalmares.com/", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                }
                else if (CompanyCache.IdCompany == 333333333)
                {
                    paragraph.Add(new Chunk("SAN RAMÓN, ALAJUELA.\r\n", textFont2));
                    paragraph.Add(Chunk.NEWLINE);

                    paragraph.Add(new Chunk("Cédula Jurídica :" + "3-333-33333", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Teléfono: +(506) " + CompanyCache.Phone, textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("WhatsApp: +(506) " + "8671 5008", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Correo: " + "crperfectglass@gmail.com", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                }
                else if (CompanyCache.IdCompany == 503320196)
                {
                    paragraph.Add(new Chunk("Cédula Jurídica : 5-0332-0196", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Ubicados en: " + "Frente escual de Los Llanos.", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Teléfono: " + "+(506) 8751-7492/ 6337-2024", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Correo: " + "Vitecosr@gmail.com", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                }
                else
                {
                    paragraph.Add(new Chunk("Cédula Jurídica :" + CompanyCache.IdCompany, textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Ubicados en: " + CompanyCache.Address, textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Teléfonos: " + CompanyCache.Phone, textFont));
                    paragraph.Add(Chunk.NEWLINE);
                }
                textCell.AddElement(paragraph);
                Encabezado.AddCell(textCell);

                // Establece el ancho de la celda de la tabla (ajusta según tus necesidades)
                Encabezado.SetWidths(new float[] { 3f, 4f }); // Primer valor es el ancho de la celda de la imagen

                // Agrega la tabla al documento
                document.Add(Encabezado);



                document.Add(new Paragraph(" "));
                #endregion

                #region Tabla de Informacion 
                // Crear una tabla para los datos del proyecto y la información del cliente
                PdfPTable datosTable = new PdfPTable(2);
                datosTable.TotalWidth = 500f; // Ajusta el ancho total según tus necesidades
                datosTable.LockedWidth = true;

                // Celda 1: Datos del Proyecto
                PdfPCell cellDatosProyecto = new PdfPCell(new Phrase("Datos del Proyecto", FontFactory.GetFont(FontFactory.HELVETICA, 16, BaseColor.WHITE)))
                {
                    BackgroundColor = new BaseColor(70, 130, 180),
                    BorderWidth = 1f,
                    //Colspan = 1, // Fusionar una columna para "Datos del Proyecto"
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                };
                datosTable.AddCell(cellDatosProyecto);

                // Celda 2: Información del Cliente
                PdfPCell cellDatosCliente = new PdfPCell(new Phrase("Información del Provedor", FontFactory.GetFont(FontFactory.HELVETICA, 16, BaseColor.WHITE)))
                {
                    BackgroundColor = new BaseColor(70, 130, 180),
                    BorderWidth = 1f,
                    Colspan = 1, // Fusionar una columna para "Información del Cliente"
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellDatosCliente);

                // Celda 3: Etiqueta "Cotización"
                PdfPCell cellEtiquetaCotizacion = new PdfPCell(new Phrase("Cotización: " + txtidQuote.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    BorderWidth = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaCotizacion);

                // Celda 4: Etiqueta "Cliente"
                PdfPCell cellEtiquetaCliente = new PdfPCell(new Phrase("Provedor: " + NombreProvedor, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    BorderWidth = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaCliente);

                // Celda 5: Etiqueta "Forma Pago"
                PdfPCell cellEtiquetaFormaPago = new PdfPCell(new Phrase("Fecha: " + txtDate.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    BorderWidth = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaFormaPago);

                // Celda 6: Etiqueta "Teléfono"
                PdfPCell cellEtiquetaTelefono = new PdfPCell(new Phrase("Teléfono: " + TelefonoProvedor, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    BorderWidth = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaTelefono);

                // Celda 6: Etiqueta "Dirección"
                PdfPCell cellEtiquetaDireccion = new PdfPCell(new Phrase("Proyecto: " + txtProjetName.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    BorderWidth = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaDireccion);

                // Celda 7: Etiqueta "Correo"
                PdfPCell cellEtiquetaCorreo = new PdfPCell(new Phrase("Correo: " + CorreoProvedor, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    BorderWidth = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaCorreo);
                document.Add(datosTable);
                document.Add(new Paragraph(" "));

                #endregion

                #region Tabla de Productos
                #region Nombres con diferentes categorias
                N_LoadProduct NLoadProduct = new N_LoadProduct();
                DataTable dtAluminio = new DataTable();
                DataTable dtAccesorio = new DataTable();
                DataTable dtVidrio = new DataTable();

                dtAluminio = NLoadProduct.loadAluminioDesglose();
                dtAccesorio = NLoadProduct.loadAccesorioDesglose();
                dtVidrio = NLoadProduct.loadVidrioDesglose();
                #endregion

                // 1. Agregar la columna "Categoría" a dgvDesglose si aún no existe
                if (!dgvDesglose.Columns.Contains("Categoría"))
                {
                    dgvDesglose.Columns.Add("Categoría", "Categoría");
                }

                // 2. Asignar la categoría basada en la descripción
                foreach (DataGridViewRow row in dgvDesglose.Rows)
                {
                    if (row.Cells["Description"].Value != null)
                    {
                        string description = row.Cells["Description"].Value.ToString();
                        string categoria = "";

                        // Verificar si la descripción existe en dtAluminio
                        if (dtAluminio.AsEnumerable().Any(r => r.Field<string>("Description") == description))
                        {
                            categoria = "Aluminio";
                        }
                        // Verificar si la descripción existe en dtAccesorio
                        else if (dtAccesorio.AsEnumerable().Any(r => r.Field<string>("Description") == description))
                        {
                            categoria = "Accesorios";
                        }
                        // Verificar si la descripción existe en dtVidrio
                        else if (dtVidrio.AsEnumerable().Any(r => r.Field<string>("Description") == description))
                        {
                            categoria = "Vidrio";
                        }

                        // Asignar la categoría en la columna "Categoría"
                        row.Cells["Categoría"].Value = categoria;
                    }
                }

                // 3. Crear una lista ordenada según la categoría
                var orderedRows = dgvDesglose.Rows.Cast<DataGridViewRow>()
                    .Where(row => row.Cells["Categoría"].Value != null)
                    .OrderBy(row => row.Cells["Categoría"].Value.ToString())
                    .ToList();

                // 4. Agregar los datos del dgvDesglose a la tabla de PDF
                PdfPTable tabla = new PdfPTable(2); // Cambia a 2 columnas para "Description" y "Cantidad Piezas"
                tabla.TotalWidth = 500f; // Ajusta el ancho total según tus necesidades
                tabla.LockedWidth = true;
                float[] tablaW = { 250, 250 }; // Ancho de las columnas

                tabla.SetWidths(tablaW);

                // Agrupar las filas por categoría
                var groupedRows = orderedRows.GroupBy(row => row.Cells["Categoría"].Value.ToString());

                // Crear una tabla separada para cada categoría
                foreach (var group in groupedRows)
                {
                    // Agregar un título para la categoría con fondo azul y letra blanca
                    PdfPCell categoryTitleCell = new PdfPCell(new Phrase(group.Key, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.WHITE)));
                    categoryTitleCell.Colspan = 2; // Hacer que la celda ocupe todas las columnas
                    categoryTitleCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    categoryTitleCell.PaddingTop = 10f; // Agregar un espacio superior
                    categoryTitleCell.PaddingBottom = 10f; // Agregar un espacio inferior
                    categoryTitleCell.BackgroundColor = new BaseColor(70, 130, 180); // Azul (RGB: 0, 102, 204)
                    categoryTitleCell.BorderColor = BaseColor.WHITE;

                    tabla.AddCell(categoryTitleCell);

                    // Agregar los encabezados de las columnas solo para las columnas que quieres imprimir
                    PdfPCell headerDescriptionCell = new PdfPCell(new Phrase("Description", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
                    headerDescriptionCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabla.AddCell(headerDescriptionCell);

                    PdfPCell headerCantidadCell = new PdfPCell(new Phrase("Cantidad Piezas", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
                    headerCantidadCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabla.AddCell(headerCantidadCell);

                    // Agregar las filas de datos para esta categoría
                    foreach (var row in group)
                    {
                        // Solo imprime "Description" y "Cantidad Piezas"
                        if (row.Cells["Description"].Value != null)
                        {
                            tabla.AddCell(new Phrase(row.Cells["Description"].Value.ToString(), FontFactory.GetFont(FontFactory.HELVETICA)));
                        }
                        else
                        {
                            tabla.AddCell(string.Empty); // Si no hay valor, agrega una celda vacía
                        }

                        if (row.Cells["Cantidad Piezas"].Value != null)
                        {
                            tabla.AddCell(new Phrase(row.Cells["Cantidad Piezas"].Value.ToString(), FontFactory.GetFont(FontFactory.HELVETICA)));
                        }
                        else
                        {
                            tabla.AddCell(string.Empty); // Si no hay valor, agrega una celda vacía
                        }
                    }

                    // Agregar un espacio entre tablas
                    PdfPCell emptyCell = new PdfPCell(new Phrase(" "));
                    emptyCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    emptyCell.Colspan = 2; // Cambiado a 2 columnas
                    emptyCell.FixedHeight = 10f;
                    tabla.AddCell(emptyCell);
                }

                // Agregar la tabla al documento
                document.Add(tabla);

                #endregion


                //Cerrar el documento
                document.Close();
                MessageBox.Show("Desglose de Material Generado Correctamente", "Desglose de Material", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GenerarPdfHojaProduccion();

            }
            catch (Exception)
            {
                document.Close();

            }
        }

        private void btnGenerarDesglose_Click(object sender, EventArgs e)
        {

           
            // Mostrar un cuadro de diálogo personalizado para seleccionar el diseño de PDF
            string[] options = { "Diseño 1 (con metraje)", "Diseño 2 (solo descripción y cantidad)" };
            using (Form form = new Form())
            {
                ComboBox cbOptions = new ComboBox();
                Button btnOK = new Button();

                cbOptions.DataSource = options;
                cbOptions.Dock = DockStyle.Fill;

                btnOK.Text = "Aceptar";
                btnOK.Dock = DockStyle.Bottom;
                btnOK.DialogResult = DialogResult.OK;

                form.Controls.Add(cbOptions);
                form.Controls.Add(btnOK);
                form.StartPosition = FormStartPosition.CenterParent;
                form.Size = new Size(350, 120);
                form.Text = "Seleccionar diseño de PDF";

                if (form.ShowDialog() == DialogResult.OK)
                {
                    string selectedOption = cbOptions.SelectedItem.ToString();
                    switch (selectedOption)
                    {
                        case "Diseño 1 (con metraje)":
                            pdf1(); 
                         
                            break;
                        case "Diseño 2 (solo descripción y cantidad)":
                            pdf2();

                            break;
                        
                    }
                }
                else
                {
                   
                }
            }


        }

        private void CargarTamañoPieza()
        {
            try
            {
                List<PriceProductClass> Lista = CargarLista();

                if (Lista == null || Lista.Count == 0)
                {
                    //MessageBox.Show("La lista de precios está vacía.");
                    return;
                }

                N_Products products = new N_Products();
                DataTable dt = products.ObtenerTamañoPieza(Lista);

                // Verificar si el DataTable tiene filas
                if (dt == null || dt.Rows.Count == 0)
                {
                    //MessageBox.Show("No se encontraron datos para los productos especificados.");
                    return;
                }

                // Convertir el DataTable a un diccionario para una búsqueda rápida
                Dictionary<string, decimal> tamanos = new Dictionary<string, decimal>();
                foreach (DataRow row in dt.Rows)
                {
                    string nombre = row["Description"].ToString().Trim(); // Asegúrate de usar el nombre correcto de la columna de descripción
                    decimal tamaño = Convert.ToDecimal(row["Tamaño"]);

                    if (!tamanos.ContainsKey(nombre))
                    {
                        tamanos.Add(nombre, tamaño);
                    }
                }


                // Asignar los tamaños correspondientes a las filas del DataGridView
                foreach (DataGridViewRow row in dgvDesglose.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        string nombreProducto = row.Cells[0].Value.ToString().Trim(); // Asegúrate de usar el índice correcto para el nombre del producto
                        if (tamanos.ContainsKey(nombreProducto))
                        {
                            
                            Console.WriteLine(nombreProducto);
                            row.Cells[4].Value = tamanos[nombreProducto]; // Usa el índice correcto para la columna "Tamaño"
                        }
                        else
                        {
                            if (nombreProducto.Contains("Vid"))
                            {
                                decimal TamañoVid = products.ObtenerTamañoVidrio(nombreProducto);
                                row.Cells[4].Value = TamañoVid;
                                continue;
                            }
                            row.Cells[4].Value = "1";
                            decimal metraje = Convert.ToDecimal(row.Cells[3].Value);
                            decimal Tamaño = Convert.ToDecimal(row.Cells[4].Value);
                            decimal Cantidad = metraje / Tamaño;
                            row.Cells[5].Value = Cantidad;
                        }
                    }
                    if (UserCache.Name == "InnovaGlass")
                    {
                        // Validar y limpiar el valor de la columna "SalePrice"
                        if (row.Cells["Cost"].Value != null)
                        {
                            string salePriceStr = row.Cells["Cost"].Value.ToString();
                            salePriceStr = salePriceStr.Replace(" ", ""); // Eliminar espacios

                            // Validar si el valor es decimal y convertirlo
                            if (decimal.TryParse(salePriceStr, out decimal salePrice))
                            {
                                row.Cells["Cost"].Value = salePrice;
                            }
                            else
                            {
                                MessageBox.Show($"Valor inválido en la columna 'SalePrice' en la fila {row.Index + 1}. Valor: '{salePriceStr}'", "Error en los datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                row.Cells["Cost"].Value = DBNull.Value; // O establece un valor por defecto
                            }
                        }
                    }
                    else {
                        // Validar y limpiar el valor de la columna "SalePrice"
                        if (row.Cells["SalePrice"].Value != null)
                        {
                            string salePriceStr = row.Cells["SalePrice"].Value.ToString();
                            salePriceStr = salePriceStr.Replace(" ", ""); // Eliminar espacios

                            // Validar si el valor es decimal y convertirlo
                            if (decimal.TryParse(salePriceStr, out decimal salePrice))
                            {
                                row.Cells["SalePrice"].Value = salePrice;
                            }
                            else
                            {
                                MessageBox.Show($"Valor inválido en la columna 'SalePrice' en la fila {row.Index + 1}. Valor: '{salePriceStr}'", "Error en los datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                row.Cells["SalePrice"].Value = DBNull.Value; // O establece un valor por defecto
                            }
                        }
                    }

                 
                }
                // Asegurarse de que las columnas 'Total Cost' y 'Total Price' existan en el DataGridView
                if (!dgvDesglose.Columns.Contains("Total Cost"))
                {
                    dgvDesglose.Columns.Add("Total Cost", "Total Cost");
                    dgvDesglose.Columns["Total Cost"].DefaultCellStyle.Format = "N2"; // Formato con dos decimales
                }
                if (!dgvDesglose.Columns.Contains("Total Price"))
                {
                    dgvDesglose.Columns.Add("Total Price", "Total Price");
                    dgvDesglose.Columns["Total Price"].DefaultCellStyle.Format = "N2"; // Formato con dos decimales
                }
                N_LoadProduct NLoadProduct = new N_LoadProduct();
                DataTable dtAccesorio = new DataTable();
                dtAccesorio = NLoadProduct.loadAccesorioDesglose();
                // Obtener todos los nombres de artículos en dtAccesorio en una lista
                List<string> accesorios = dtAccesorio.AsEnumerable().Select(row => row.Field<string>("Description")).ToList();

                // Calcular los valores para 'Total Cost' y 'Total Price' para cada fila
                foreach (DataGridViewRow row in dgvDesglose.Rows)
                {
                    if (row.Cells[4].Value != null && row.Cells[5].Value != null)
                    {
                        decimal tamaño = Convert.ToDecimal(row.Cells[4].Value);
                        decimal cantidad = Convert.ToDecimal(row.Cells[5].Value);
                        string articulo = row.Cells[0].Value.ToString();

                        if (accesorios.Contains(articulo))
                        {
                            // Si es un accesorio, se calcula sin el 'tamaño'
                            if (row.Cells["Cost"].Value != null)
                            {
                                decimal cost = Convert.ToDecimal(row.Cells["Cost"].Value);
                                row.Cells["Total Cost"].Value = Math.Round(cantidad * cost, 2);
                            }

                            if (row.Cells["SalePrice"].Value != null)
                            {
                                decimal salePrice = Convert.ToDecimal(row.Cells["SalePrice"].Value);
                                row.Cells["Total Price"].Value = Math.Round(cantidad * salePrice, 2);
                            }
                        }
                        else
                        {
                            // Si no es un accesorio, se calcula con el 'tamaño'
                            if (row.Cells["Cost"].Value != null)
                            {
                                decimal cost = Convert.ToDecimal(row.Cells["Cost"].Value);
                                row.Cells["Total Cost"].Value = Math.Round(tamaño * cantidad * cost, 2);
                            }

                            if (row.Cells["SalePrice"].Value != null)
                            {
                                decimal salePrice = Convert.ToDecimal(row.Cells["SalePrice"].Value);
                                row.Cells["Total Price"].Value = Math.Round(tamaño * cantidad * salePrice, 2);
                            }
                        }
                    }
                }

                // Declarar variables para acumular las sumas de Total Cost y Total Price
                decimal sumaTotalCost = 0;
                decimal sumaTotalPrice = 0;

                // Recorrer cada fila del DataGridView para acumular las sumas
                foreach (DataGridViewRow row in dgvDesglose.Rows)
                {
                    // Sumar Total Cost
                    if (row.Cells["Total Cost"].Value != null)
                    {
                        sumaTotalCost += Convert.ToDecimal(row.Cells["Total Cost"].Value);
                    }

                    // Sumar Total Price
                    if (row.Cells["Total Price"].Value != null)
                    {
                        sumaTotalPrice += Convert.ToDecimal(row.Cells["Total Price"].Value);
                    }
                }

                // Mostrar las sumas en los TextBox correspondientes
                txtTotalC.Text = sumaTotalCost.ToString("N2"); // Formato con dos decimales
                txtTotalSP.Text = sumaTotalPrice.ToString("N2"); // Formato con dos decimales


            }
            catch (Exception ex)
            {
                // Manejar la excepción apropiadamente
                MessageBox.Show($"Se produjo un error: {ex.Message}");
            }
        }

        // Método para cargar datos en el DataGridView
        private List<PriceProductClass> CargarLista() 
            {
                //Cargar la Lita con el dgvDesglose
                List<PriceProductClass> Lista = new List<PriceProductClass>();
                foreach (DataGridViewRow item in dgvDesglose.Rows)
                {
                    //Validar que el Item no sea Nulo
                    if (item.Cells[1].Value != null)
                    {
                        PriceProductClass priceProduct = new PriceProductClass();
                        priceProduct.Nombre = item.Cells[0].Value.ToString();
                        priceProduct.Supplier = cbProveedorDesglose.SelectedValue.ToString();
                        priceProduct.Color = "Natural";
                        Lista.Add(priceProduct);
                    }
                }
               
                return Lista;

            }

        #endregion

        #region Hoja de Produccion
        private DataTable CargarArticulos()
        {
            N_LoadProduct LoadProducts = new N_LoadProduct();
            DataTable dtArticulos = new DataTable();
            dtArticulos = LoadProducts.loadAluminio(ClsWindows.Color,ClsWindows.System,"Extralum");
            return dtArticulos;
        }
        private DataTable CargarAccesorios()
        {
            N_LoadProduct LoadProducts = new N_LoadProduct();
            DataTable dtAccesorios = new DataTable();
            dtAccesorios = LoadProducts.loadAccesorios(ClsWindows.System, "Extralum");
            return dtAccesorios;
        }
        private void GenerarPdfHojaProduccion() 
        {
            try
            {
                    #region Crear el documento
                //Recorrer Todo el DataTable
                foreach (DataRow item in dt.Rows)
                {
                    //Cargar Variables
                    ClsWindows.Color = item["Color"].ToString();
                    ClsWindows.System = item["System"].ToString();
                    ClsWindows.Desing = item["Design"].ToString();
                    ClsWindows.Weight = Convert.ToDecimal(item["Width"]);
                    ClsWindows.heigt = Convert.ToDecimal(item["Height"]);


                    // Obtener el directorio del escritorio y las carpetas necesarias
                    string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string CarpetaFactura = Path.Combine(escritorio, "Hoja Produccion");
                    string carpetaNombre = Path.Combine(CarpetaFactura, txtProjetName.Text.Trim());
                    //Obtener el Id de la Ventana
                    string IdWindows = item["IdWindows"].ToString();
                    string NameFile = "Ventana n° " + IdWindows + ".pdf";

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

                    Document document = new Document();
                    // Crea un nuevo objeto PdfWriter para escribir el documento en un archivo
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(rutaArchivoPDF, FileMode.Create));

                    // Asigna el objeto PdfWriter al documento
                    document.Open();
                    #endregion

                   
                    #region Tabla de Informacion 
                    //Agregar un Titulo con el el Sistema , Color y Diseño de la Vnetana
                    PdfPTable datosTable = new PdfPTable(1);
                    datosTable.TotalWidth = 500f; // Ajusta el ancho total según tus necesidades
                    datosTable.LockedWidth = true;

                    // Celda 1: Datos del Proyecto
                    PdfPCell cellDatosProyecto = new PdfPCell(new Phrase("Ventana: " + ClsWindows.System + " " + ClsWindows.Color + " " + ClsWindows.Desing, FontFactory.GetFont(FontFactory.HELVETICA, 16, BaseColor.WHITE)))
                    {
                        BackgroundColor = new BaseColor(70, 130, 180),
                        BorderWidth = 1f,
                        //Colspan = 1, // Fusionar una columna para "Datos del Proyecto"
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_CENTER
                    };
                    datosTable.AddCell(cellDatosProyecto);
                    document.Add(datosTable);
                    document.Add(new Paragraph(" "));
                    #endregion


                    #region Agregar la Imagen de la Ventana
                    //Agregar la Imagen de la Ventana Centrada en el PDF
                    string rutaImagen = item["Url"].ToString();
                    if (File.Exists(rutaImagen))
                    {
                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(rutaImagen);
                        img.ScaleToFit(100f, 100f);
                        img.Alignment = Element.ALIGN_CENTER;
                        document.Add(img);
                        document.Add(new Paragraph(" "));
                    }
                    #endregion


                    #region Tabla de Productos
                    //Cargar DataTables con los Articulos y Accesorios
                    DataTable dtArticulos = CargarArticulos();
                    DataTable dtAccesorios = CargarAccesorios();

                    // Crear la tabla en el PDF para mostrar los productos
                    PdfPTable tabla = new PdfPTable(2); // 2 columnas para "Descripción" y "Metraje"

                    // Encabezados de la tabla
                    tabla.AddCell(new PdfPCell(new Phrase("Descripción", FontFactory.GetFont(FontFactory.HELVETICA, 12))));
                    tabla.AddCell(new PdfPCell(new Phrase("Metraje", FontFactory.GetFont(FontFactory.HELVETICA,12))));

                    // Insertar los Datos de los Articulos
                    foreach (DataRow row in dtArticulos.Rows)
                    {
                        tabla.AddCell(new PdfPCell(new Phrase(row["Description"].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 12))));
                        tabla.AddCell(new PdfPCell(new Phrase(row["Metraje"].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 12))));
                    }

                    // Insertar los Datos de los Accesorios
                    foreach (DataRow row in dtAccesorios.Rows)
                    {
                        tabla.AddCell(new PdfPCell(new Phrase(row["Description"].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 12))));
                        tabla.AddCell(new PdfPCell(new Phrase(row["Metraje"].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 12))));
                    }

                    // Agregar la tabla al documento
                    document.Add(tabla);
                    #endregion


                    //Cerrar el documento
                    document.Close();
                    MessageBox.Show("Hoja de Produccion Generada Correctamente", "Hoja de Produccion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }


            }
            catch (Exception)
            {

               
            }
           
        }
        #endregion

        #region GENERATE PDF
        #region Generacion de pdf Factura
        public bool GenerateFactura()
        {
            #region Crear el documento
            try
            {
                // Obtener el directorio del escritorio y las carpetas necesarias
                string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string CarpetaFactura = Path.Combine(escritorio, "Facturas");
                string carpetaNombre = Path.Combine(CarpetaFactura, txtidClient.Text.Trim());
                string NameFile = "Cotizacion n° " + txtidQuote.Text + ".pdf";

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

                Document document = new Document();
                // Crea un nuevo objeto PdfWriter para escribir el documento en un archivo
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(rutaArchivoPDF, FileMode.Create));

                // Asigna el objeto PdfWriter al documento
                document.Open();
                #endregion

            #region Encabezado
                // Crea una tabla con dos columnas
                PdfPTable Encabezado = new PdfPTable(2);
                Encabezado.WidthPercentage = 100;


                // Agrega la imagen a la primera celda
                string rutaLogo = "";
                Console.WriteLine(CompanyCache.IdCompany);
                //Usuario de Prueba
                if (CompanyCache.IdCompany == 999999999)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\UsuarioPrueba.png";
                    rutaLogo = ruta + Url;

                }
                //Urbano
                if (CompanyCache.IdCompany == 110640721)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\urbano.png";
                    rutaLogo = ruta + Url;

                }
                //La familia
                if (CompanyCache.IdCompany == 114420180)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\familia.png";
                    rutaLogo = ruta + Url;

                }

                if (CompanyCache.IdCompany == 3105806704)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\glassmar.png";
                    rutaLogo = ruta + Url;

                }

                //laCosta
                if (CompanyCache.IdCompany == 109650325)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\laCosta.png";
                    rutaLogo = ruta + Url;

                }

                //V Dayra
                if (CompanyCache.IdCompany == 9699999999)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\vdayra.png";
                    rutaLogo = ruta + Url;

                }
                //Constru
                if (CompanyCache.IdCompany == 3101704274)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\constru.png";
                    rutaLogo = ruta + Url;

                }
                //MS
                if (CompanyCache.IdCompany == 204260627)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\ms.png";
                    rutaLogo = ruta + Url;

                }
                //Viteco
                if (CompanyCache.IdCompany == 503320196)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\Viteco.png";
                    rutaLogo = ruta + Url;

                }
                //Vitro esparza
                if (CompanyCache.IdCompany == 3101623589 || CompanyCache.IdCompany == 3101623581)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\vitroEsparza.png";
                    rutaLogo = ruta + Url;

                }
                //Usuario de Nel
                if (CompanyCache.IdCompany == 205520679)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VidriosMartinez.png";
                    rutaLogo = ruta + Url;

                }
                //Usuario de Nel Fin
                //Prefalum, cedula juridica de prueba
                if (CompanyCache.IdCompany == 111111111)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\Prefalum2.png";
                    rutaLogo = ruta + Url;

                }
                //Vidrios Albo
                if (CompanyCache.IdCompany == 3102154177)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\albo.png";
                    rutaLogo = ruta + Url;

                }
                //Mercado del vidrio
                if (CompanyCache.IdCompany == 3102879949)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\mercadoVidrio.png";
                    rutaLogo = ruta + Url;

                }
                //Vidriera Palmares, cedula juridica de prueba
                if (CompanyCache.IdCompany == 222222222)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VidrieraPalmares.png";
                    rutaLogo = ruta + Url;

                }
                //Perfect Glass, cedula juridica de prueba
                if (CompanyCache.IdCompany == 333333333)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\PerfectGlass.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 31025820)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\AluviLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 3101794685)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\RioClaroLogo.png";
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
                if (CompanyCache.IdCompany == 1230123)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\GlassWinLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 25550555)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VitroLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 31028013)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\InnovaLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 111560456)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\DialexLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 310108681)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VidrioCentroLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 310171783)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VidriosVegaLogo.png";
                    rutaLogo = ruta + Url;

                }
                PdfPCell imageCell = new PdfPCell(iTextSharp.text.Image.GetInstance(rutaLogo));
                imageCell.Border = PdfPCell.NO_BORDER;
                imageCell.FixedHeight = 120f; // Ajusta la altura de la imagen
                imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
                Encabezado.AddCell(imageCell);

                // Crea un nuevo objeto Font para los textos
                iTextSharp.text.Font titleFont = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 19, iTextSharp.text.Font.BOLD, BaseColor.GRAY);
                iTextSharp.text.Font textFont = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                // Agrega los textos a la segunda celda
                PdfPCell textCell = new PdfPCell();
                textCell.Border = PdfPCell.NO_BORDER;

                // Alinea el contenido de la celda al centro
                textCell.HorizontalAlignment = Element.ALIGN_RIGHT;

                // Agrega el párrafo y los chunks al documento
                Paragraph paragraph = new Paragraph();
                paragraph.Add(new Chunk(CompanyCache.Name, titleFont));
                paragraph.Add(Chunk.NEWLINE);// Salto de línea
                paragraph.Add(new Chunk("Cédula Jurídica :" + CompanyCache.IdCompany, textFont));
                paragraph.Add(Chunk.NEWLINE);
                paragraph.Add(new Chunk("Ubicados en: " + CompanyCache.Address, textFont));
                paragraph.Add(Chunk.NEWLINE);
                paragraph.Add(new Chunk("Teléfonos: " + CompanyCache.Phone, textFont));
                paragraph.Add(Chunk.NEWLINE);
                paragraph.Add(Chunk.NEWLINE);// Salto de línea

                textCell.AddElement(paragraph);
                Encabezado.AddCell(textCell);

                // Establece el ancho de la celda de la tabla (ajusta según tus necesidades)
                Encabezado.SetWidths(new float[] { 3f, 4f }); // Primer valor es el ancho de la celda de la imagen

                // Agrega la tabla al documento
                document.Add(Encabezado);

                // Añade la palabra "COTIZACIÓN" debajo de la tabla
                Paragraph cotizacionParagraph = new Paragraph("FACTURACION", titleFont);
                cotizacionParagraph.Alignment = Element.ALIGN_CENTER;
                document.Add(cotizacionParagraph);
                document.Add(new Paragraph(" "));// Esto agrega un espacio en blanco en el documento

                #endregion

            #region Tabla de Informacion 
                // Crear una tabla para los datos del proyecto y la información del cliente
                PdfPTable datosTable = new PdfPTable(2);
                datosTable.TotalWidth = 500f; // Ajusta el ancho total según tus necesidades
                datosTable.LockedWidth = true;

                // Celda 1: Datos del Proyecto
                PdfPCell cellDatosProyecto = new PdfPCell(new Phrase("Datos del Proyecto", FontFactory.GetFont(FontFactory.HELVETICA, 16, BaseColor.WHITE)))
                {
                    BackgroundColor = new BaseColor(70, 130, 180),
                    BorderWidth = 1f,
                    //Colspan = 1, // Fusionar una columna para "Datos del Proyecto"
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                };
                datosTable.AddCell(cellDatosProyecto);

                // Celda 2: Información del Cliente
                PdfPCell cellDatosCliente = new PdfPCell(new Phrase("Información del Cliente", FontFactory.GetFont(FontFactory.HELVETICA, 16, BaseColor.WHITE)))
                {
                    BackgroundColor = new BaseColor(70, 130, 180),
                    BorderWidth = 1f,
                    Colspan = 1, // Fusionar una columna para "Información del Cliente"
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellDatosCliente);

                // Celda 3: Etiqueta "Cotización"
                PdfPCell cellEtiquetaCotizacion = new PdfPCell(new Phrase("Cotización: " + txtidQuote.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    BorderWidth = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaCotizacion);

                // Celda 4: Etiqueta "Cliente"
                PdfPCell cellEtiquetaCliente = new PdfPCell(new Phrase("Cliente: " + txtidClient.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    BorderWidth = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaCliente);

                // Celda 5: Etiqueta "Forma Pago"
                PdfPCell cellEtiquetaFormaPago = new PdfPCell(new Phrase("Fecha: " + txtDate.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    BorderWidth = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaFormaPago);

                // Celda 6: Etiqueta "Teléfono"
                PdfPCell cellEtiquetaTelefono = new PdfPCell(new Phrase("Teléfono: " + txtTelefono.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    BorderWidth = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaTelefono);

                // Celda 6: Etiqueta "Dirección"
                PdfPCell cellEtiquetaDireccion = new PdfPCell(new Phrase("Proyecto: " + txtProjetName.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    BorderWidth = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaDireccion);

                // Celda 7: Etiqueta "Correo"
                PdfPCell cellEtiquetaCorreo = new PdfPCell(new Phrase("Correo: " + txtEmail.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    BorderWidth = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaCorreo);
                document.Add(datosTable);
                document.Add(new Paragraph(" "));

                #endregion

            #region Tabla de Productos
                PdfPTable tabla = new PdfPTable(dgvGlass.Columns.Count);
                tabla.TotalWidth = 500f; // Ajusta el ancho total según tus necesidades     
                tabla.LockedWidth = true;
                float[] tablaW = { 0, 160f, 180f, 0, 0, 0, 0, 0, 100f, 0, 0, 0, 0 }; // Ancho de las columnas
                tabla.SetWidths(tablaW);

                // Agregar encabezados de columna
                for (int i = 0; i < dgvGlass.Columns.Count; i++)
                {
                    PdfPCell celda = new PdfPCell(new Phrase(dgvGlass.Columns[i].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 13, BaseColor.WHITE))); // Reducimos el tamaño a 13 puntos
                    celda.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda.BackgroundColor = new BaseColor(70, 130, 180);
                    tabla.AddCell(celda);
                }

                for (int i = 0; i < dgvGlass.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvGlass.Columns.Count; j++)
                    {
                        if (dgvGlass[j, i].Value != null)
                        {
                            PdfPCell cell = null;

                            if (dgvGlass.Columns[j].HeaderText == "URL")
                            {
                                string rutaImagen = dgvGlass[j, i].Value.ToString();
                                System.Version versionActual = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                                string versionActualString = $"GlassWin{versionActual.Major}.{versionActual.Minor}.{versionActual.Build}.{versionActual.Revision}";

                                // Reemplazar la versión en la ruta con la versión actual
                                string rutaCorregida = ReemplazarVersionEnRuta(rutaImagen, versionActualString);

                                // Obtener el directorio de trabajo actual
                                string directorioDeTrabajo = Directory.GetCurrentDirectory();
                                Console.WriteLine($"Directorio de trabajo: {directorioDeTrabajo}");

                                string rutaAbsoluta;
                                bool esExclusivo = rutaCorregida.StartsWith("EXCLUSIVO:");
                                if (esExclusivo)
                                {
                                    rutaCorregida = rutaCorregida.Replace("EXCLUSIVO:", "");
                                }

                                if (Path.IsPathRooted(rutaCorregida))
                                {
                                    if (File.Exists(rutaCorregida))
                                    {
                                        rutaAbsoluta = rutaCorregida;
                                    }
                                    else
                                    {
                                        string fileName = Path.GetFileName(rutaCorregida);
                                        rutaAbsoluta = Path.Combine(directorioDeTrabajo, "Images\\Windows", fileName);
                                    }
                                }
                                else
                                {
                                    rutaAbsoluta = Path.Combine(directorioDeTrabajo, rutaCorregida);
                                    rutaAbsoluta = Path.GetFullPath(rutaAbsoluta);
                                }

                                if (!string.IsNullOrEmpty(rutaAbsoluta) && File.Exists(rutaAbsoluta))
                                {
                                    // Obtener dimensiones en metros y convertirlas a píxeles
                                    decimal anchoEnMetros = ObtenerAncho(dgvGlass.Rows[i].Cells[2].Value.ToString());
                                    decimal alturaEnMetros = ObtenerAlto(dgvGlass.Rows[i].Cells[2].Value.ToString());

                                    int anchoVentana = (int)(anchoEnMetros * MetrosAPixeles);
                                    int altoVentana = (int)(alturaEnMetros * MetrosAPixeles);

                                    if (anchoVentana == 0) anchoVentana = 150;//e.CellBounds.Width;
                                    if (altoVentana == 0) altoVentana = 100;//e.CellBounds.Height;

                                    // Mostrar dimensiones calculadas para depuración
                                    Console.WriteLine($"Ancho ventana en píxeles: {anchoVentana}, Alto ventana en píxeles: {altoVentana}");

                                    // Cargar la imagen y ajustar su tamaño
                                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(rutaAbsoluta);

                                    // Ajustar el tamaño de la imagen con ScaleAbsolute
                                    img.ScaleAbsolute(anchoVentana, altoVentana);

                                    PdfPCell celdaImagen = new PdfPCell(img);
                                    celdaImagen.HorizontalAlignment = Element.ALIGN_CENTER;
                                    celdaImagen.VerticalAlignment = Element.ALIGN_MIDDLE;
                                    celdaImagen.FixedHeight = altoVentana; // Ajustar la altura de la celda para coincidir con la imagen
                                    tabla.AddCell(celdaImagen);
                                }
                                else
                                {
                                    // Agregar una celda con texto "Sin Imagen"
                                    cell = new PdfPCell(new Phrase("Sin Imagen", FontFactory.GetFont(FontFactory.HELVETICA, 12)));
                                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                    // Ajustar el tamaño de la celda de la imagen
                                    cell.FixedHeight = 50f; // Ajusta la altura según sea necesario
                                    tabla.AddCell(cell);
                                }
                            }
                            else
                            {
                                if (dgvGlass.Columns[j].HeaderText == "Descripcion")
                                {
                                    // Para la columna "Descripción", alinea el texto a la izquierda
                                    cell = new PdfPCell(new Phrase(dgvGlass[j, i].Value.ToString(), FontFactory.GetFont(FontFactory.HELVETICA)));
                                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                }
                                else if (dgvGlass.Columns[j].HeaderText == "Precio")
                                {
                                    // Para la columna "Precio", alinea el texto a la izquierda y redondea a dos decimales
                                    decimal Prices = Convert.ToDecimal(dgvGlass[j, i].Value);
                                    Prices = Math.Round(Prices, 2);
                                    cell = new PdfPCell(new Phrase("¢" + Prices.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 10)));
                                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                }
                                else
                                {
                                    cell = new PdfPCell(new Phrase(dgvGlass[j, i].Value.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 10)));
                                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                }

                                // Ajusta el tamaño de las celdas
                                cell.FixedHeight = 150f; // Ajusta la altura según sea necesario
                                cell.PaddingLeft = 10f; // Agrega un relleno a la izquierda para alinear el texto correctamente
                                                        // Centrar contenido verticalmente
                                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                tabla.AddCell(cell);
                            }
                        }
                    }
                }


                // Agregar la tabla al documento
                document.Add(tabla);

                document.Add(new Paragraph(" "));// Esto agrega un espacio en blanco en el documento
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
                cellp.Phrase = new Phrase("¢" + Subtotal.ToString("N2"));
                tablePrecio.AddCell(cellp);

                cellp.Phrase = new Phrase("¢" + IVA.ToString("N2"));

                tablePrecio.AddCell(cellp);
                cellp.Phrase = new Phrase("¢" + Total.ToString("N2"));
                tablePrecio.AddCell(cellp);

                // Agregar la tabla al documento
                document.Add(tablePrecio);
                if (CompanyCache.IdCompany == 31025820)
                {
                    // Agregar una imagen al documento
                    string imagePath = Path.GetDirectoryName(Application.ExecutablePath) + "\\Images\\Firma\\Firma Reiner.jpeg";
                    Image img = Image.GetInstance(imagePath);
                    img.ScaleToFit(200, 200); // Ajustar el tamaño de la imagen
                    img.Alignment = Element.ALIGN_CENTER; // Alinear la imagen al centro
                    document.Add(img); // Agregar la imagen al documento
                }

                #endregion

            #region Cerrar el documento
                // Cerrar el documento
                document.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            #endregion
        }
        #endregion

        #region Generacion de pdf Hoja de Rectificacion
        public bool GenerateRectificacion()
        {
            string Option = cbProveedor.Text;
            string NombreProvedor = "";
            string TelefonoProvedor = "";
            string CorreoProvedor = "";
            switch (Option)
            {
                case "Extralum":
                    NombreProvedor = "Extralum";
                    TelefonoProvedor = "2277-1900";
                    CorreoProvedor = "aavila@extralum.co.cr";
                    break;
                case "Macopa":
                    NombreProvedor = "Macopa";
                    TelefonoProvedor = "2010-7310";
                    CorreoProvedor = "proyectosvidrios@macopa.com";
                    break;
                case "Espejos el Mundo":
                    NombreProvedor = "Espejos el Mundo";
                    TelefonoProvedor = "2293-4961";
                    CorreoProvedor = "N/A";
                    break;
                default:
                    break;
            }

               #region Crear el documento
            try
            {
                // Obtener el directorio del escritorio y las carpetas necesarias
                string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string CarpetaFactura = Path.Combine(escritorio, "Orden de Compra");
                string carpetaNombre = Path.Combine(CarpetaFactura, txtidClient.Text.Trim());
                string NameFile = "Orden de Compra n° " + txtidQuote.Text + ".pdf";

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

                Document document = new Document();
                // Crea un nuevo objeto PdfWriter para escribir el documento en un archivo
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(rutaArchivoPDF, FileMode.Create));

                // Asigna el objeto PdfWriter al documento
                document.Open();
                #endregion

                #region Encabezado
                // Crea una tabla con dos columnas
                PdfPTable Encabezado = new PdfPTable(2);
                Encabezado.WidthPercentage = 100;


                // Agrega la imagen a la primera celda
                string rutaLogo = "";
                //Usuario de Prueba
                if (CompanyCache.IdCompany == 999999999)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\UsuarioPrueba.png";
                    rutaLogo = ruta + Url;

                }
                //Urbano
                if (CompanyCache.IdCompany == 110640721)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\urbano.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 3105806704)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\glassmar.png";
                    rutaLogo = ruta + Url;

                }

                //La familia
                if (CompanyCache.IdCompany == 114420180)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\familia.png";
                    rutaLogo = ruta + Url;

                }


                //laCosta
                if (CompanyCache.IdCompany == 109650325)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\laCosta.png";
                    rutaLogo = ruta + Url;

                }

                //V Dayra
                if (CompanyCache.IdCompany == 9699999999)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\vdayra.png";
                    rutaLogo = ruta + Url;

                }
                //Constru
                if (CompanyCache.IdCompany == 3101704274)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\constru.png";
                    rutaLogo = ruta + Url;

                }
                //MS
                if (CompanyCache.IdCompany == 204260627)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\ms.png";
                    rutaLogo = ruta + Url;

                }
                //Viteco
                if (CompanyCache.IdCompany == 503320196)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\Viteco.png";
                    rutaLogo = ruta + Url;

                }
                //Vitro esparza
                if (CompanyCache.IdCompany == 3101623589 || CompanyCache.IdCompany == 3101623581)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\vitroEsparza.png";
                    rutaLogo = ruta + Url;

                }
                //Usuario de Nel
                if (CompanyCache.IdCompany == 205520679)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VidriosMartinez.png";
                    rutaLogo = ruta + Url;

                }
                //Usuario de Nel Fin
                //Prefalum, cedula juridica de prueba
                if (CompanyCache.IdCompany == 111111111)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\Prefalum2.png";
                    rutaLogo = ruta + Url;

                }
                //Vidrios Albo
                if (CompanyCache.IdCompany == 3102154177)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\albo.png";
                    rutaLogo = ruta + Url;

                }
                //Mercado del vidrio
                if (CompanyCache.IdCompany == 3102879949)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\mercadoVidrio.png";
                    rutaLogo = ruta + Url;

                }
                //Vidriera Palmares, cedula juridica de prueba
                if (CompanyCache.IdCompany == 222222222)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VidrieraPalmares.png";
                    rutaLogo = ruta + Url;

                }
                //Perfect Glass, cedula juridica de prueba
                if (CompanyCache.IdCompany == 333333333)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\PerfectGlass.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 31025820)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\AluviLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 3101794685)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\RioClaroLogo.png";
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
                if (CompanyCache.IdCompany == 1230123)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\GlassWinLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 25550555)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VitroLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 31028013)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\InnovaLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 111560456)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\DialexLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 310108681)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VidrioCentroLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 310171783)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VidriosVegaLogo.png";
                    rutaLogo = ruta + Url;

                }
                PdfPCell imageCell = new PdfPCell(iTextSharp.text.Image.GetInstance(rutaLogo));
                imageCell.Border = PdfPCell.NO_BORDER;
                imageCell.FixedHeight = 120f; // Ajusta la altura de la imagen
                imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
                Encabezado.AddCell(imageCell);

                // Crea un nuevo objeto Font para los textos
                iTextSharp.text.Font titleFont = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 19, iTextSharp.text.Font.BOLD, BaseColor.GRAY);
                iTextSharp.text.Font textFont = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                // Agrega los textos a la segunda celda
                PdfPCell textCell = new PdfPCell();
                textCell.Border = PdfPCell.NO_BORDER;

                // Alinea el contenido de la celda al centro
                textCell.HorizontalAlignment = Element.ALIGN_RIGHT;

                // Agrega el párrafo y los chunks al documento
                Paragraph paragraph = new Paragraph();
                paragraph.Add(new Chunk(CompanyCache.Name, titleFont));
                paragraph.Add(Chunk.NEWLINE);// Salto de línea
                paragraph.Add(new Chunk("Cédula Jurídica :" + CompanyCache.IdCompany, textFont));
                paragraph.Add(Chunk.NEWLINE);
                paragraph.Add(new Chunk("Ubicados en: " + CompanyCache.Address, textFont));
                paragraph.Add(Chunk.NEWLINE);
                paragraph.Add(new Chunk("Teléfonos: " + CompanyCache.Phone, textFont));
                paragraph.Add(Chunk.NEWLINE);
                paragraph.Add(Chunk.NEWLINE);// Salto de línea

                textCell.AddElement(paragraph);
                Encabezado.AddCell(textCell);

                // Establece el ancho de la celda de la tabla (ajusta según tus necesidades)
                Encabezado.SetWidths(new float[] { 3f, 4f }); // Primer valor es el ancho de la celda de la imagen

                // Agrega la tabla al documento
                document.Add(Encabezado);

                // Añade la palabra "COTIZACIÓN" debajo de la tabla
                Paragraph cotizacionParagraph = new Paragraph("Hoja Rectificacion", titleFont);
                cotizacionParagraph.Alignment = Element.ALIGN_CENTER;
                document.Add(cotizacionParagraph);
                document.Add(new Paragraph(" "));// Esto agrega un espacio en blanco en el documento

                #endregion

                #region Tabla de Informacion 
                // Crear una tabla para los datos del proyecto y la información del cliente
                PdfPTable datosTable = new PdfPTable(2);
                datosTable.TotalWidth = 500f; // Ajusta el ancho total según tus necesidades
                datosTable.LockedWidth = true;

                // Celda 1: Datos del Proyecto
                PdfPCell cellDatosProyecto = new PdfPCell(new Phrase("Datos del Proyecto", FontFactory.GetFont(FontFactory.HELVETICA, 16, BaseColor.WHITE)))
                {
                    BackgroundColor = new BaseColor(70, 130, 180),
                    BorderWidth = 1f,
                    //Colspan = 1, // Fusionar una columna para "Datos del Proyecto"
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                };
                datosTable.AddCell(cellDatosProyecto);

                // Celda 2: Información del Cliente
                PdfPCell cellDatosCliente = new PdfPCell(new Phrase("Información del Provedor", FontFactory.GetFont(FontFactory.HELVETICA, 16, BaseColor.WHITE)))
                {
                    BackgroundColor = new BaseColor(70, 130, 180),
                    BorderWidth = 1f,
                    Colspan = 1, // Fusionar una columna para "Información del Cliente"
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellDatosCliente);

                // Celda 3: Etiqueta "Cotización"
                PdfPCell cellEtiquetaCotizacion = new PdfPCell(new Phrase("Cotización: " + txtidQuote.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    BorderWidth = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaCotizacion);

                // Celda 4: Etiqueta "Cliente"
                PdfPCell cellEtiquetaCliente = new PdfPCell(new Phrase("Provedor: " + NombreProvedor, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    BorderWidth = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaCliente);

                // Celda 5: Etiqueta "Forma Pago"
                PdfPCell cellEtiquetaFormaPago = new PdfPCell(new Phrase("Fecha: " + txtDate.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    BorderWidth = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaFormaPago);

                // Celda 6: Etiqueta "Teléfono"
                PdfPCell cellEtiquetaTelefono = new PdfPCell(new Phrase("Teléfono: " + TelefonoProvedor, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    BorderWidth = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaTelefono);

                // Celda 6: Etiqueta "Dirección"
                PdfPCell cellEtiquetaDireccion = new PdfPCell(new Phrase("Proyecto: " + txtProjetName.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    BorderWidth = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaDireccion);

                // Celda 7: Etiqueta "Correo"
                PdfPCell cellEtiquetaCorreo = new PdfPCell(new Phrase("Correo: " + CorreoProvedor, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    BorderWidth = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaCorreo);
                document.Add(datosTable);
                document.Add(new Paragraph(" "));

                #endregion

            #region Tabla de Productos
                PdfPTable tabla = new PdfPTable(dgvGlass.Columns.Count);
                tabla.TotalWidth = 500f; // Ajusta el ancho total según tus necesidades     
                tabla.LockedWidth = true;
                float[] tablaW = { 0, 100, 180f, 50, 50, 0, 0, 0, 0, 0, 0, 0, 0 }; // Ancho de las columnas
                tabla.SetWidths(tablaW);

                // Agregar encabezados de columna
                for (int i = 0; i < dgvGlass.Columns.Count; i++)
                {
                    PdfPCell celda = new PdfPCell(new Phrase(dgvGlass.Columns[i].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 13, BaseColor.WHITE))); // Reducimos el tamaño a 13 puntos
                    celda.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda.BackgroundColor = new BaseColor(70, 130, 180);
                    tabla.AddCell(celda);
                }

                for (int i = 0; i < dgvGlass.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvGlass.Columns.Count; j++)
                    {
                        if (dgvGlass[j, i].Value != null)
                        {
                            PdfPCell cell = null;

                            if (dgvGlass.Columns[j].HeaderText == "URL")
                            {
                                string rutaImagen = dgvGlass[j, i].Value.ToString();
                                System.Version versionActual = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                                string versionActualString = $"GlassWin{versionActual.Major}.{versionActual.Minor}.{versionActual.Build}.{versionActual.Revision}";

                                // Reemplazar la versión en la ruta con la versión actual
                                string rutaCorregida = ReemplazarVersionEnRuta(rutaImagen, versionActualString);

                                // Obtener el directorio de trabajo actual
                                string directorioDeTrabajo = Directory.GetCurrentDirectory();
                                Console.WriteLine($"Directorio de trabajo: {directorioDeTrabajo}");

                                string rutaAbsoluta;
                                bool esExclusivo = rutaCorregida.StartsWith("EXCLUSIVO:");
                                if (esExclusivo)
                                {
                                    rutaCorregida = rutaCorregida.Replace("EXCLUSIVO:", "");
                                }

                                if (Path.IsPathRooted(rutaCorregida))
                                {
                                    if (File.Exists(rutaCorregida))
                                    {
                                        rutaAbsoluta = rutaCorregida;
                                    }
                                    else
                                    {
                                        string fileName = Path.GetFileName(rutaCorregida);
                                        rutaAbsoluta = Path.Combine(directorioDeTrabajo, "Images\\Windows", fileName);
                                    }
                                }
                                else
                                {
                                    rutaAbsoluta = Path.Combine(directorioDeTrabajo, rutaCorregida);
                                    rutaAbsoluta = Path.GetFullPath(rutaAbsoluta);
                                }

                                if (!string.IsNullOrEmpty(rutaAbsoluta) && File.Exists(rutaAbsoluta))
                                {
                                    // Obtener dimensiones en metros y convertirlas a píxeles
                                    decimal anchoEnMetros = ObtenerAncho(dgvGlass.Rows[i].Cells[2].Value.ToString());
                                    decimal alturaEnMetros = ObtenerAlto(dgvGlass.Rows[i].Cells[2].Value.ToString());

                                    int anchoVentana = (int)(anchoEnMetros * MetrosAPixeles);
                                    int altoVentana = (int)(alturaEnMetros * MetrosAPixeles);

                                    if (anchoVentana == 0) anchoVentana = 150;//e.CellBounds.Width;
                                    if (altoVentana == 0) altoVentana = 100;//e.CellBounds.Height;

                                    // Mostrar dimensiones calculadas para depuración
                                    Console.WriteLine($"Ancho ventana en píxeles: {anchoVentana}, Alto ventana en píxeles: {altoVentana}");

                                    // Cargar la imagen y ajustar su tamaño
                                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(rutaAbsoluta);

                                    // Ajustar el tamaño de la imagen con ScaleAbsolute
                                    img.ScaleAbsolute(anchoVentana, altoVentana);

                                    PdfPCell celdaImagen = new PdfPCell(img);
                                    celdaImagen.HorizontalAlignment = Element.ALIGN_CENTER;
                                    celdaImagen.VerticalAlignment = Element.ALIGN_MIDDLE;
                                    celdaImagen.FixedHeight = altoVentana; // Ajustar la altura de la celda para coincidir con la imagen
                                    tabla.AddCell(celdaImagen);
                                }
                                else
                                {
                                    // Agregar una celda con texto "Sin Imagen"
                                    cell = new PdfPCell(new Phrase("Sin Imagen", FontFactory.GetFont(FontFactory.HELVETICA, 12)));
                                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                    // Ajustar el tamaño de la celda de la imagen
                                    cell.FixedHeight = 50f; // Ajusta la altura según sea necesario
                                    tabla.AddCell(cell);
                                }
                            }
                            else
                            {
                                if (dgvGlass.Columns[j].HeaderText == "Descripcion")
                                {
                                    // Para la columna "Descripción", alinea el texto a la izquierda
                                    cell = new PdfPCell(new Phrase(dgvGlass[j, i].Value.ToString(), FontFactory.GetFont(FontFactory.HELVETICA)));
                                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                }
                                else if (dgvGlass.Columns[j].HeaderText == "Precio")
                                {
                                    // Para la columna "Precio", alinea el texto a la izquierda y redondea a dos decimales
                                    decimal Prices = Convert.ToDecimal(dgvGlass[j, i].Value);
                                    Prices = Math.Round(Prices, 2);
                                    cell = new PdfPCell(new Phrase("¢" + Prices.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 10)));
                                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                }
                                else
                                {
                                    cell = new PdfPCell(new Phrase(dgvGlass[j, i].Value.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 10)));
                                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                }

                                // Ajusta el tamaño de las celdas
                                cell.FixedHeight = 150f; // Ajusta la altura según sea necesario
                                cell.PaddingLeft = 10f; // Agrega un relleno a la izquierda para alinear el texto correctamente
                                                        // Centrar contenido verticalmente
                                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                tabla.AddCell(cell);
                            }
                        }
                    }
                }

                // Agregar la tabla al documento
                document.Add(tabla);

                document.Add(new Paragraph(" "));// Esto agrega un espacio en blanco en el documento
                #endregion

            #region Cerrar el documento
                // Cerrar el documento
                document.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            #endregion
        }

        #endregion

        #region Generacion de pdf Planos de Taller
        public bool GeneratePlanos()
        {
            #region Crear el documento
            try
            {
                // Obtener el directorio del escritorio y las carpetas necesarias
                string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string CarpetaFactura = Path.Combine(escritorio, "Planos de Taller");
                string carpetaNombre = Path.Combine(CarpetaFactura, txtidClient.Text.Trim());
                string NameFile = "Plano de Taller n° " + txtidQuote.Text + ".pdf";

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

                Document document = new Document();
                // Crea un nuevo objeto PdfWriter para escribir el documento en un archivo
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(rutaArchivoPDF, FileMode.Create));

                // Asigna el objeto PdfWriter al documento
                document.Open();
                #endregion

            #region Encabezado
                // Crea una tabla con dos columnas
                PdfPTable Encabezado = new PdfPTable(2);
                Encabezado.WidthPercentage = 100;


                // Agrega la imagen a la primera celda
                string rutaLogo = "";
                //Usuario de Prueba
                if (CompanyCache.IdCompany == 999999999)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\UsuarioPrueba.png";
                    rutaLogo = ruta + Url;

                }
                //Urbano
                if (CompanyCache.IdCompany == 110640721)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\urbano.png";
                    rutaLogo = ruta + Url;

                }
                //La familia
                if (CompanyCache.IdCompany == 114420180)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\familia.png";
                    rutaLogo = ruta + Url;

                }

                if (CompanyCache.IdCompany == 3105806704)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\glassmar.png";
                    rutaLogo = ruta + Url;

                }

                //laCosta
                if (CompanyCache.IdCompany == 109650325)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\laCosta.png";
                    rutaLogo = ruta + Url;

                }

                //Constru
                if (CompanyCache.IdCompany == 3101704274)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\constru.png";
                    rutaLogo = ruta + Url;

                }
                //V Dayra
                if (CompanyCache.IdCompany == 9699999999)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\vdayra.png";
                    rutaLogo = ruta + Url;

                }
                //MS
                if (CompanyCache.IdCompany == 204260627)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\ms.png";
                    rutaLogo = ruta + Url;

                }
                //Viteco
                if (CompanyCache.IdCompany == 503320196)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\Viteco.png";
                    rutaLogo = ruta + Url;

                }
                //Vitro esparza
                if (CompanyCache.IdCompany == 3101623589 || CompanyCache.IdCompany == 3101623581)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\vitroEsparza.png";
                    rutaLogo = ruta + Url;

                }
                //Usuario de Nel
                if (CompanyCache.IdCompany == 205520679)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VidriosMartinez.png";
                    rutaLogo = ruta + Url;

                }
                //Usuario de Nel Fin
                //Prefalum, cedula juridica de prueba
                if (CompanyCache.IdCompany == 111111111)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\Prefalum2.png";
                    rutaLogo = ruta + Url;

                }
                //Vidrios Albo
                if (CompanyCache.IdCompany == 3102154177)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\albo.png";
                    rutaLogo = ruta + Url;

                }
                //Mercado del vidrio
                if (CompanyCache.IdCompany == 3102879949)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\mercadoVidrio.png";
                    rutaLogo = ruta + Url;

                }
                //Vidriera Palmares, cedula juridica de prueba
                if (CompanyCache.IdCompany == 222222222)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VidrieraPalmares.png";
                    rutaLogo = ruta + Url;

                }
                //Perfect Glass, cedula juridica de prueba
                if (CompanyCache.IdCompany == 333333333)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\PerfectGlass.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 31025820)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\AluviLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 3101794685)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\RioClaroLogo.png";
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
                if (CompanyCache.IdCompany == 1230123)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\GlassWinLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 25550555)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VitroLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 31028013)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\InnovaLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 111560456)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\DialexLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 310108681)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VidrioCentroLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 310171783)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VidriosVegaLogo.png";
                    rutaLogo = ruta + Url;

                }
                PdfPCell imageCell = new PdfPCell(iTextSharp.text.Image.GetInstance(rutaLogo));
                imageCell.Border = PdfPCell.NO_BORDER;
                imageCell.FixedHeight = 120f; // Ajusta la altura de la imagen
                imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
                Encabezado.AddCell(imageCell);

                // Crea un nuevo objeto Font para los textos
                iTextSharp.text.Font titleFont = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 19, iTextSharp.text.Font.BOLD, BaseColor.GRAY);
                iTextSharp.text.Font textFont = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                // Agrega los textos a la segunda celda
                PdfPCell textCell = new PdfPCell();
                textCell.Border = PdfPCell.NO_BORDER;

                // Alinea el contenido de la celda al centro
                textCell.HorizontalAlignment = Element.ALIGN_RIGHT;

                // Agrega el párrafo y los chunks al documento
                Paragraph paragraph = new Paragraph();
                paragraph.Add(new Chunk(CompanyCache.Name, titleFont));
                paragraph.Add(Chunk.NEWLINE);// Salto de línea
                paragraph.Add(new Chunk("Cédula Jurídica :" + CompanyCache.IdCompany, textFont));
                paragraph.Add(Chunk.NEWLINE);
                paragraph.Add(new Chunk("Ubicados en: " + CompanyCache.Address, textFont));
                paragraph.Add(Chunk.NEWLINE);
                paragraph.Add(new Chunk("Teléfonos: " + CompanyCache.Phone, textFont));
                paragraph.Add(Chunk.NEWLINE);
                paragraph.Add(Chunk.NEWLINE);// Salto de línea

                textCell.AddElement(paragraph);
                Encabezado.AddCell(textCell);

                // Establece el ancho de la celda de la tabla (ajusta según tus necesidades)
                Encabezado.SetWidths(new float[] { 3f, 4f }); // Primer valor es el ancho de la celda de la imagen

                // Agrega la tabla al documento
                document.Add(Encabezado);

                // Añade la palabra "COTIZACIÓN" debajo de la tabla
                Paragraph cotizacionParagraph = new Paragraph("Planos Taller", titleFont);
                cotizacionParagraph.Alignment = Element.ALIGN_CENTER;
                document.Add(cotizacionParagraph);
                document.Add(new Paragraph(" "));// Esto agrega un espacio en blanco en el documento

                #endregion

            #region Tabla de Informacion 
                // Crear una tabla para los datos del proyecto y la información del cliente
                PdfPTable datosTable = new PdfPTable(2);
                datosTable.TotalWidth = 500f; // Ajusta el ancho total según tus necesidades
                datosTable.LockedWidth = true;

                // Celda 1: Datos del Proyecto
                PdfPCell cellDatosProyecto = new PdfPCell(new Phrase("Datos del Proyecto", FontFactory.GetFont(FontFactory.HELVETICA, 16, BaseColor.WHITE)))
                {
                    BackgroundColor = new BaseColor(70, 130, 180),
                    BorderWidth = 1f,
                    //Colspan = 1, // Fusionar una columna para "Datos del Proyecto"
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                };
                datosTable.AddCell(cellDatosProyecto);

                // Celda 2: Información del Cliente
                PdfPCell cellDatosCliente = new PdfPCell(new Phrase("Información del Cliente", FontFactory.GetFont(FontFactory.HELVETICA, 16, BaseColor.WHITE)))
                {
                    BackgroundColor = new BaseColor(70, 130, 180),
                    BorderWidth = 1f,
                    Colspan = 1, // Fusionar una columna para "Información del Cliente"
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellDatosCliente);

                // Celda 3: Etiqueta "Cotización"
                PdfPCell cellEtiquetaCotizacion = new PdfPCell(new Phrase("Cotización: " + txtidQuote.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    BorderWidth = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaCotizacion);

                // Celda 4: Etiqueta "Cliente"
                PdfPCell cellEtiquetaCliente = new PdfPCell(new Phrase("Cliente: " + txtidClient.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    BorderWidth = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaCliente);

                // Celda 5: Etiqueta "Forma Pago"
                PdfPCell cellEtiquetaFormaPago = new PdfPCell(new Phrase("Fecha: " + txtDate.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    BorderWidth = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaFormaPago);

                // Celda 6: Etiqueta "Teléfono"
                PdfPCell cellEtiquetaTelefono = new PdfPCell(new Phrase("Teléfono: " + txtTelefono.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    BorderWidth = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaTelefono);

                // Celda 6: Etiqueta "Dirección"
                PdfPCell cellEtiquetaDireccion = new PdfPCell(new Phrase("Proyecto: " + txtProjetName.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    BorderWidth = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaDireccion);

                // Celda 7: Etiqueta "Correo"
                PdfPCell cellEtiquetaCorreo = new PdfPCell(new Phrase("Correo: " + txtEmail.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    BorderWidth = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaCorreo);
                document.Add(datosTable);
                document.Add(new Paragraph(" "));

                #endregion

            #region Tabla de Productos
                PdfPTable tabla = new PdfPTable(dgvGlass.Columns.Count);
                tabla.TotalWidth = 500f; // Ajusta el ancho total según tus necesidades     
                tabla.LockedWidth = true;
                float[] tablaW = { 0, 150, 160f, 30, 32, 0, 0, 0, 0, 0, 0, 0, 0 }; // Ancho de las columnas
                tabla.SetWidths(tablaW);

                // Agregar encabezados de columna
                for (int i = 0; i < dgvGlass.Columns.Count; i++)
                {
                    PdfPCell celda = new PdfPCell(new Phrase(dgvGlass.Columns[i].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 13, BaseColor.WHITE))); // Reducimos el tamaño a 13 puntos
                    celda.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda.BackgroundColor = new BaseColor(70, 130, 180);
                    tabla.AddCell(celda);
                }

                for (int i = 0; i < dgvGlass.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvGlass.Columns.Count; j++)
                    {
                        if (dgvGlass[j, i].Value != null)
                        {
                            PdfPCell cell = null;

                            if (dgvGlass.Columns[j].HeaderText == "URL")
                            {
                                string rutaImagen = dgvGlass[j, i].Value.ToString();
                                System.Version versionActual = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                                string versionActualString = $"GlassWin{versionActual.Major}.{versionActual.Minor}.{versionActual.Build}.{versionActual.Revision}";

                                // Reemplazar la versión en la ruta con la versión actual
                                string rutaCorregida = ReemplazarVersionEnRuta(rutaImagen, versionActualString);

                                // Obtener el directorio de trabajo actual
                                string directorioDeTrabajo = Directory.GetCurrentDirectory();
                                Console.WriteLine($"Directorio de trabajo: {directorioDeTrabajo}");

                                string rutaAbsoluta;
                                bool esExclusivo = rutaCorregida.StartsWith("EXCLUSIVO:");
                                if (esExclusivo)
                                {
                                    rutaCorregida = rutaCorregida.Replace("EXCLUSIVO:", "");
                                }

                                if (Path.IsPathRooted(rutaCorregida))
                                {
                                    if (File.Exists(rutaCorregida))
                                    {
                                        rutaAbsoluta = rutaCorregida;
                                    }
                                    else
                                    {
                                        string fileName = Path.GetFileName(rutaCorregida);
                                        rutaAbsoluta = Path.Combine(directorioDeTrabajo, "Images\\Windows", fileName);
                                    }
                                }
                                else
                                {
                                    rutaAbsoluta = Path.Combine(directorioDeTrabajo, rutaCorregida);
                                    rutaAbsoluta = Path.GetFullPath(rutaAbsoluta);
                                }

                                if (!string.IsNullOrEmpty(rutaAbsoluta) && File.Exists(rutaAbsoluta))
                                {
                                    // Obtener dimensiones en metros y convertirlas a píxeles
                                    decimal anchoEnMetros = ObtenerAncho(dgvGlass.Rows[i].Cells[2].Value.ToString());
                                    decimal alturaEnMetros = ObtenerAlto(dgvGlass.Rows[i].Cells[2].Value.ToString());

                                    int anchoVentana = (int)(anchoEnMetros * MetrosAPixeles);
                                    int altoVentana = (int)(alturaEnMetros * MetrosAPixeles);

                                    if (anchoVentana == 0) anchoVentana = 150;//e.CellBounds.Width;
                                    if (altoVentana == 0) altoVentana = 100;//e.CellBounds.Height;

                                    // Mostrar dimensiones calculadas para depuración
                                    Console.WriteLine($"Ancho ventana en píxeles: {anchoVentana}, Alto ventana en píxeles: {altoVentana}");

                                    // Cargar la imagen y ajustar su tamaño
                                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(rutaAbsoluta);

                                    // Ajustar el tamaño de la imagen con ScaleAbsolute
                                    img.ScaleAbsolute(anchoVentana, altoVentana);

                                    PdfPCell celdaImagen = new PdfPCell(img);
                                    celdaImagen.HorizontalAlignment = Element.ALIGN_CENTER;
                                    celdaImagen.VerticalAlignment = Element.ALIGN_MIDDLE;
                                    celdaImagen.FixedHeight = altoVentana; // Ajustar la altura de la celda para coincidir con la imagen
                                    tabla.AddCell(celdaImagen);
                                }
                                else
                                {
                                    // Agregar una celda con texto "Sin Imagen"
                                    cell = new PdfPCell(new Phrase("Sin Imagen", FontFactory.GetFont(FontFactory.HELVETICA, 12)));
                                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                    // Ajustar el tamaño de la celda de la imagen
                                    cell.FixedHeight = 50f; // Ajusta la altura según sea necesario
                                    tabla.AddCell(cell);
                                }
                            }
                            else
                            {
                                if (dgvGlass.Columns[j].HeaderText == "Descripcion")
                                {
                                    // Para la columna "Descripción", alinea el texto a la izquierda
                                    cell = new PdfPCell(new Phrase(dgvGlass[j, i].Value.ToString(), FontFactory.GetFont(FontFactory.HELVETICA)));
                                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                }
                                else if (dgvGlass.Columns[j].HeaderText == "Precio")
                                {
                                    // Para la columna "Precio", alinea el texto a la izquierda y redondea a dos decimales
                                    decimal Prices = Convert.ToDecimal(dgvGlass[j, i].Value);
                                    Prices = Math.Round(Prices, 2);
                                    cell = new PdfPCell(new Phrase("¢" + Prices.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 10)));
                                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                }
                                else
                                {
                                    cell = new PdfPCell(new Phrase(dgvGlass[j, i].Value.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 10)));
                                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                }

                                // Ajusta el tamaño de las celdas
                                cell.FixedHeight = 150f; // Ajusta la altura según sea necesario
                                cell.PaddingLeft = 10f; // Agrega un relleno a la izquierda para alinear el texto correctamente
                                                        // Centrar contenido verticalmente
                                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                tabla.AddCell(cell);
                            }
                        }
                    }
                }

                // Agregar la tabla al documento
                document.Add(tabla);

                document.Add(new Paragraph(" "));// Esto agrega un espacio en blanco en el documento

                //Ecribir un texto para la firma del cliente
                Paragraph firma = new Paragraph("   Firma del Cliente: ______________________________________________________________", textFont);
                firma.Alignment = Element.ALIGN_LEFT;
                document.Add(firma);

                #endregion

            #region Cerrar el documento
                // Cerrar el documento
                document.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            #endregion
        }

        #endregion

        #endregion

        private void CargarProveedor()
        {
            LN_Proveedor lN_Proveedor = new LN_Proveedor();
            DataTable dt = lN_Proveedor.CargarProveedor();

            // Asigna el DataTable como la fuente de datos del ComboBox
            cbProveedorDesglose.DataSource = dt;
            cbProveedorDesglose.DisplayMember = "Nombre"; // La columna que se muestra en el ComboBox
            cbProveedorDesglose.ValueMember = "Nombre";   // La columna que se utiliza como valor

            // Selecciona "Extralum" si está disponible en el ComboBox
            cbProveedorDesglose.SelectedValue = "Extralum";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            GenerarPdfHojaProduccion();
        }
        private void lblTitleFactura_Click(object sender, EventArgs e)
        {

        }
        private void cbProveedorDesglose_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Limpia todas las filas y columnas del DataGridView
            dgvDesglose.DataSource = null;
            dgvDesglose.Rows.Clear();
            dgvDesglose.Columns.Clear();

            // Ahora llama a las funciones necesarias
            CargarDesglose();
            ConfigDataGridDesglose();
            CargarTamañoPieza();
        }
        private void frmFacturar_FormClosed(object sender, FormClosedEventArgs e)
        {
          frmManagerQuotes frmManagerQuotes = new frmManagerQuotes();
          frmManagerQuotes.Show();

        }
        private decimal ObtenerAncho(string Descripcion)
        {
            if (!Descripcion.Contains("Exclusivo"))
            {
                string patternAncho = @"\nAncho:\s*([\d,\.]+)";
                System.Text.RegularExpressions.Match matchAncho = System.Text.RegularExpressions.Regex.Match(Descripcion, patternAncho);
                string Ancho = matchAncho.Groups[1].Value.Replace(",", ".");
                decimal AnchoDecimal = 0;
                if (Ancho != "")
                {
                    if (Ancho.Contains("."))
                    {
                        AnchoDecimal = Convert.ToDecimal(Ancho.Replace(".", ","));
                    }
                    else
                    {
                        AnchoDecimal = Convert.ToDecimal(Ancho);
                    }
                }

                return AnchoDecimal;
            }
            else
            {
                return 1.5m;
            }
        }
        private decimal ObtenerAlto(string Descripcion)
        {
            if (!Descripcion.Contains("Exclusivo"))
            {
                string patternAlto = @"\nAlto:\s*([\d,\.]+)";
                System.Text.RegularExpressions.Match matchAlto = System.Text.RegularExpressions.Regex.Match(Descripcion, patternAlto);
                string Alto = matchAlto.Groups[1].Value.Replace(",", ".");
                decimal AltoDecimal = 0;
                if (Alto != "")
                {
                    if (Alto.Contains("."))
                    {
                        AltoDecimal = Convert.ToDecimal(Alto.Replace(".", ","));
                    }
                    else
                    {
                        AltoDecimal = Convert.ToDecimal(Alto);
                    }
                }

                return AltoDecimal;
            }
            else
            {
                return 1.5m;
            }
        }
        private void dgvDesglose_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Mostrar un mensaje de error
            //MessageBox.Show($"Error en la columna '{dgvDesglose.Columns[e.ColumnIndex].HeaderText}' y fila '{e.RowIndex + 1}'. Detalle del error: {e.Exception.Message}", "Error en el DataGridView", MessageBoxButtons.OK, MessageBoxIcon.Error);

            // Puedes manejar diferentes acciones dependiendo del tipo de error
            switch (e.Context)
            {
                case DataGridViewDataErrorContexts.Commit:
                    //MessageBox.Show("Error de commit.");
                    break;
                case DataGridViewDataErrorContexts.CurrentCellChange:
                    //MessageBox.Show("Error al cambiar de celda.");
                    break;
                case DataGridViewDataErrorContexts.Parsing:
                    //MessageBox.Show("Error de parsing.");
                    break;
                case DataGridViewDataErrorContexts.LeaveControl:
                    //MessageBox.Show("Error al dejar el control.");
                    break;
            }

            // Indicar que el error ha sido manejado
            e.ThrowException = false;
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }
        public void cargarDatosTotalDesglose()
        {
            try
            {
                DataTable dtTotalDesglose = new DataTable();
                int idQuote = Convert.ToInt32(txtidQuote.Text);

                // Llama al método que obtiene los datos y los asigna al DataTable
                dtTotalDesglose = NQuote.GetTotalDesgloseByQuoteId(idQuote);

                // Asigna el DataTable como fuente de datos del DataGridView
                dgvTotal.DataSource = dtTotalDesglose;

                // Ajustar el ancho de las columnas al contenido
                dgvTotal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Cambiar los encabezados de las columnas
                dgvTotal.Columns[0].HeaderText = "ID";
                dgvTotal.Columns[1].HeaderText = "Proforma";
                dgvTotal.Columns[2].HeaderText = "Precio Venta";
                dgvTotal.Columns[3].HeaderText = "Monto Fabricación";
                dgvTotal.Columns[4].HeaderText = "Monto Instalación";
                dgvTotal.Columns[5].HeaderText = "Total";
            }
            catch (Exception ex)
            {
                // Manejo de errores
                // MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
        }
        public static class Prompt
        {
            public static (string, string) ShowDialog(string text1, string text2, string caption)
            {
                Form prompt = new Form()
                {
                    Width = 500,
                    Height = 200,
                    Text = caption
                };

                // Aumentar la anchura de los Label
                Label textLabel1 = new Label() { Left = 50, Top = 20, Width = 400, Text = text1 };
                TextBox textBox1 = new TextBox() { Left = 50, Top = 50, Width = 400 };

                Label textLabel2 = new Label() { Left = 50, Top = 80, Width = 400, Text = text2 };
                TextBox textBox2 = new TextBox() { Left = 50, Top = 110, Width = 400 };

                Button confirmation = new Button() { Text = "Aceptar", Left = 350, Width = 100, Top = 140 };
                confirmation.Click += (sender, e) => { prompt.Close(); };

                prompt.Controls.Add(textLabel1);
                prompt.Controls.Add(textBox1);
                prompt.Controls.Add(textLabel2);
                prompt.Controls.Add(textBox2);
                prompt.Controls.Add(confirmation);

                prompt.ShowDialog();

                return (textBox1.Text, textBox2.Text);
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            bandera = true;
            // Convertir el valor de txtTotalSP.Text a decimal
            decimal totalPV = Convert.ToDecimal(txtTotalSP.Text);

            var (inputFabricacion, inputInstalacion) = Prompt.ShowDialog("Ingrese el monto de fabricación:", "Ingrese el monto de instalación:", "Montos");

            decimal montoFabricacion = 0;
            decimal montoInstalacion = 0;

            if (!decimal.TryParse(inputFabricacion, out montoFabricacion))
            {
                MessageBox.Show("Por favor, ingrese un monto de fabricación válido.");
                return;
            }

            if (!decimal.TryParse(inputInstalacion, out montoInstalacion))
            {
                MessageBox.Show("Por favor, ingrese un monto de instalación válido.");
                return;
            }

            // Continuar con los montos obtenidos
            Console.WriteLine($"Monto de Fabricación: {montoFabricacion}");
            Console.WriteLine($"Monto de Instalación: {montoInstalacion}");



            // Calcular el total sumando totalPV, montoFabricacion y montoInstalacion
            decimal total = totalPV + montoFabricacion + montoInstalacion;

          

            int idQuote = Convert.ToInt32(txtidQuote.Text);
            //    public int InsertTotalDesglose(int IdQuote, decimal TotalPV, decimal MontoFacturacion, decimal MontoInstalacion, decimal Total)
            int resul = NQuote.InsertTotalDesglose(idQuote, totalPV, montoFabricacion, montoInstalacion, total);

            bool resultado = NQuote.updateQuoteTotal(idQuote,total);

            cargarDatosTotalDesglose();

            // Asegurarse de que el DataGridView tenga las columnas necesarias
            /*if (dgvTotal.Columns.Count == 0)
            {
                dgvTotal.Columns.Add("TotalPV", "Total PV");
                dgvTotal.Columns.Add("MontoFabricacion", "Monto Fabricación");
                dgvTotal.Columns.Add("MontoInstalacion", "Monto Instalación");
                dgvTotal.Columns.Add("Total", "Total");
            }*/

            // Configurar el modo de ajuste automático de las columnas para que se llenen todo el ancho del DataGridView
            dgvTotal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Agregar una nueva fila al DataGridView con los valores calculados
            //dgvTotal.Rows.Add(totalPV, montoFabricacion, montoInstalacion, total);

            if (resultado)
            {
                // Opcional: Mostrar un mensaje de éxito
                MessageBox.Show("Datos añadidos correctamente a la tabla y monto total de la proforma modificado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else {
                MessageBox.Show("Ha ocurrido un error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            

            // Cambiar a la pestaña que contiene el DataGridView (tabPageTotal)
            tabControl.SelectedTab = tabPageTotal;
        }
        private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            // Verificar si se está intentando seleccionar la pestaña tabPageTotal y la bandera es false
            if (!bandera && e.TabPage == tabPageTotal)
            {
                // Cancelar la selección
                e.Cancel = true;

                // Opcional: Mostrar un mensaje informando al usuario
                MessageBox.Show("No puede acceder a esta pestaña en este momento.");
            }
        }
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Verifica que haya una fila seleccionada
            if (dgvTotal.SelectedRows.Count > 0)
            {
                // Pregunta de confirmación
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar este registro?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Si el usuario confirma la eliminación
                if (result == DialogResult.Yes)
                {
                    // Obtén el IdTotal de la fila seleccionada (suponiendo que está en la columna [0])
                    int idTotal = Convert.ToInt32(dgvTotal.SelectedRows[0].Cells[0].Value);

                    // Llama al método para eliminar el registro de la base de datos
                    if (NQuote.EliminarRegistro(idTotal))
                    {
                        // Elimina la fila del DataGridView
                        dgvTotal.Rows.RemoveAt(dgvTotal.SelectedRows[0].Index);
                        MessageBox.Show("Registro eliminado exitosamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el registro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una fila para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void dgvTotal_MouseClick(object sender, MouseEventArgs e)
        {
            // Verifica si se ha hecho clic con el botón derecho del mouse
            if (e.Button == MouseButtons.Right)
            {
                // Obtén la posición del mouse
                var hit = dgvTotal.HitTest(e.X, e.Y);

                // Verifica si se hizo clic en una fila válida
                if (hit.RowIndex >= 0)
                {
                    // Selecciona la fila bajo el cursor
                    dgvTotal.ClearSelection();
                    dgvTotal.Rows[hit.RowIndex].Selected = true;

                    // Muestra el menú contextual en la posición del mouse
                    contextMenuStrip1.Show(dgvTotal, new System.Drawing.Point(e.X, e.Y));
                }
            }
        }

    }
}