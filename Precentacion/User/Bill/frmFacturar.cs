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
using System.IO;
using System.Linq;
using System.Windows.Forms;

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
        #endregion

        #region Constructor
        public frmFacturar()
        {
            InitializeComponent();
            CargarProveedor();
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

                    //Obtener La cantidad de ventana el dato se encuentra en la descripcion
                    string DescripcionCantidad = row.Cells[1].Value.ToString();

                    string pattern = @"Cantidad:\s*(\d+)";
                    System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(DescripcionCantidad, pattern);
                    int Cantidad = Convert.ToInt32(match.Groups[1].Value);

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
                        dtAluminio = NLoadProduct.loadAluminioVentanaFija(Color, Sistema, "Extralum", Material);

                        //Obtener el Metraje del dt Aluminio y Multiplicarlo por la Cantidad de Ventanas
                        foreach (DataRow item in dtAluminio.Rows)
                        {
                            item["Metraje"] = Convert.ToDecimal(item["Metraje"]) * Convert.ToDecimal(Cantidad);
                        }
                    }
                    else
                    {
                        //Obtener el Total del Aluminio                     
                        dtAluminio = NLoadProduct.loadAluminio(Color, Sistema, "Extralum");

                        //Obtener el Metraje del dt Aluminio y Multiplicarlo por la Cantidad de Ventanas                    
                        foreach (DataRow item in dtAluminio.Rows)
                        {
                            item["Metraje"] = Convert.ToDecimal(item["Metraje"]) * Convert.ToDecimal(Cantidad);
                        }
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
                    dtAccesorios = NLoadProduct.loadAccesorios(Sistema, "Extralum");

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
                    //Obtener el Total de los Vidrios                      
                    DataTable dtVidrios = NLoadProduct.loadPricesGlass("Extralum", Vidrio);
                    //Obtener el Metraje del dt Vidrios y Multiplicarlo por la Cantidad de Ventanas
                    foreach (DataRow item in dtVidrios.Rows)
                    {
                        item["Metraje"] = Convert.ToDecimal(item["Metraje"]) * Convert.ToDecimal(Cantidad);
                    }

                    //Validar todos las Celdas del dtVidrios para agregarlas al dtTotalDesglose                     
                    if (dtTotalDesglose.Rows.Count == 0)
                    {
                        dtTotalDesglose = dtVidrios;
                    }
                    else
                    {
                        foreach (DataRow item in dtVidrios.Rows)
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
                }
                //Eliminar del DataTable la fila que el Metraje sea 0
                dtTotalDesglose = dtTotalDesglose.Select("Metraje > 0").CopyToDataTable();

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
                dgvDesglose.Columns[1].Visible = false;
                dgvDesglose.Columns[3].Visible = false;
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
                if (tabControl.SelectedIndex == 3)
                {
               
                }
            }

        private void dgvDesglose_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 4)
                {
                    if (dgvDesglose.Rows[e.RowIndex].Cells[2].Value != null)
                    {
                        // Limpieza del valor para eliminar espacios y caracteres no deseados
                        string metrajeStr = dgvDesglose.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();
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


        private void btnGenerarDesglose_Click(object sender, EventArgs e)
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
                    string Option = cbProveedorDesglose.Text;
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
                    //Agregar los datos del dgvDesglose a la tabla de pdf
                    PdfPTable tabla = new PdfPTable(dgvDesglose.Columns.Count);
                    tabla.TotalWidth = 500f; // Ajusta el ancho total según tus necesidades
                    tabla.LockedWidth = true;
                    float[] tablaW = { 100, 0, 30f, 0, 32, 32 }; // Ancho de las columnas
                    tabla.SetWidths(tablaW);
                    //Modificar el alto de las columnas



                    // Agregar encabezados de columna
                    for (int i = 0; i < dgvDesglose.Columns.Count; i++)
                    {
                        if (dgvDesglose.Columns[i].HeaderText == "")
                        {

                        }
                        PdfPCell celda = new PdfPCell(new Phrase(dgvDesglose.Columns[i].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 13, BaseColor.WHITE))); // Reducimos el tamaño a 13 puntos
                        celda.HorizontalAlignment = Element.ALIGN_CENTER;
                        celda.BackgroundColor = new BaseColor(70, 130, 180);
                        tabla.AddCell(celda);
                    }

                    // Agregar filas y celdas de datos con imágenes
                    for (int i = 0; i < dgvDesglose.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgvDesglose.Columns.Count; j++)
                        {
                            if (dgvDesglose[j, i].Value != null)
                            {
                                PdfPCell cell = null;

                                if (dgvDesglose.Columns[j].HeaderText == "Description")
                                {
                                    // Para la columna "Descripción", alinea el texto a la izquierda
                                    cell = new PdfPCell(new Phrase(dgvDesglose[j, i].Value.ToString(), FontFactory.GetFont(FontFactory.HELVETICA)));
                                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                }
                                else
                                {
                                    // Para otras columnas, mantén el texto centrado
                                    cell = new PdfPCell(new Phrase(dgvDesglose[j, i].Value.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 12)));
                                    cell.VerticalAlignment = Element.ALIGN_CENTER;
                                }

                                // Ajusta el tamaño de las celdas
                                cell.FixedHeight = 30f; // Aumentamos la altura a 150 unidades
                                cell.PaddingLeft = 5f; // Agrega un relleno a la izquierda para alinear el texto correctamente

                                // Centrar contenido verticalmente
                                cell.VerticalAlignment = Element.ALIGN_CENTER;
                                tabla.AddCell(cell);
                            }
                        }
                    }

                    // Agregar la tabla al documento
                    document.Add(tabla);
                    #endregion
                    //Cerrar el documento
                    document.Close();
                    MessageBox.Show("Desglose de Material Generado Correctamente", "Desglose de Material", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception)
                {
                    document.Close();

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
                            row.Cells[4].Value = tamanos[nombreProducto]; // Usa el índice correcto para la columna "Tamaño"
                        }
                        else
                        {
                            row.Cells[4].Value = row.Cells[2].Value;
                            row.Cells[5].Value = row.Cells[2].Value;
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
                        priceProduct.Supplier = cbProveedorDesglose.Text;
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
                if (CompanyCache.IdCompany == 310171783)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VidriosVegaLogo.png";
                    rutaLogo = ruta + Url;

                }

                if (CompanyCache.IdCompany == 3101794685)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\RioClaroLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 111560456)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\DialexLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 31025820)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\AluviLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 31028013)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\InnovaLogo.png";
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
                if (CompanyCache.IdCompany == 1230123)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\GlassWinLogo.png";
                    rutaLogo = ruta + Url;
                }
                if (CompanyCache.IdCompany == 310108681)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VidrioCentroLogo.png";
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

                // Agregar filas y celdas de datos con imágenes
                for (int i = 0; i < dgvGlass.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvGlass.Columns.Count; j++)
                    {
                        if (dgvGlass[j, i].Value != null)
                        {
                            // Si es la columna de imagen (ajusta según tu estructura de datos)
                            if (dgvGlass.Columns[j].HeaderText == "URL")
                            {
                                string rutaImagen = dgvGlass[j, i].Value.ToString();
                                if (!string.IsNullOrEmpty(rutaImagen) && File.Exists(rutaImagen))
                                {
                                    // Agregar la imagen al PDF y ajustar su tamaño
                                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(rutaImagen);
                                    img.ScaleToFit(100, 100); // Cambia el tamaño deseado aquí
                                    PdfPCell celdaImagen = new PdfPCell(img);
                                    celdaImagen.HorizontalAlignment = Element.ALIGN_CENTER;
                                    celdaImagen.VerticalAlignment = Element.ALIGN_MIDDLE;
                                    tabla.AddCell(celdaImagen);
                                }
                                else
                                {
                                    // Si no hay imagen, agrega una con texto que dice "No hay imagen"
                                    PdfPCell celda = new PdfPCell(new Phrase("No hay imagen", FontFactory.GetFont(FontFactory.HELVETICA, 12)));
                                    celda.HorizontalAlignment = Element.ALIGN_CENTER;
                                    celda.VerticalAlignment = Element.ALIGN_MIDDLE;
                                    tabla.AddCell(celda);
                                }
                            }
                            else
                            {
                                PdfPCell cell = null;

                                if (dgvGlass.Columns[j].HeaderText == "Description")
                                {
                                    // Para la columna "Descripción", alinea el texto a la izquierda
                                    cell = new PdfPCell(new Phrase(dgvGlass[j, i].Value.ToString(), FontFactory.GetFont(FontFactory.HELVETICA)));
                                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                }

                                if (dgvGlass.Columns[j].HeaderText == "Precio")
                                {
                                    // Para la columna "Descripción", alinea el texto a la izquierda
                                    decimal Prices = Convert.ToDecimal(dgvGlass[j, i].Value);
                                    Prices = Math.Round(Prices, 2);
                                    cell = new PdfPCell(new Phrase("¢" + Prices.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 10)));
                                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                }
                                else
                                {
                                    // Para otras columnas, mantén el texto centrado
                                    cell = new PdfPCell(new Phrase(dgvGlass[j, i].Value.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 12)));
                                    cell.VerticalAlignment = Element.ALIGN_LEFT;
                                }

                                // Ajusta el tamaño de las celdas
                                cell.FixedHeight = 170f; // Aumentamos la altura a 150 unidades
                                cell.PaddingLeft = 5f; // Agrega un relleno a la izquierda para alinear el texto correctamente

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
                if (CompanyCache.IdCompany == 310171783)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VidriosVegaLogo.png";
                    rutaLogo = ruta + Url;

                }

                if (CompanyCache.IdCompany == 3101794685)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\RioClaroLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 111560456)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\DialexLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 31025820)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\AluviLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 31028013)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\InnovaLogo.png";
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
                if (CompanyCache.IdCompany == 1230123)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\GlassWinLogo.png";
                    rutaLogo = ruta + Url;
                }
                if (CompanyCache.IdCompany == 310108681)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VidrioCentroLogo.png";
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

                // Agregar filas y celdas de datos con imágenes
                for (int i = 0; i < dgvGlass.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvGlass.Columns.Count; j++)
                    {
                        if (dgvGlass[j, i].Value != null)
                        {
                            // Si es la columna de imagen (ajusta según tu estructura de datos)
                            if (dgvGlass.Columns[j].HeaderText == "URL")
                            {
                                string rutaImagen = dgvGlass[j, i].Value.ToString();
                                if (!string.IsNullOrEmpty(rutaImagen) && File.Exists(rutaImagen))
                                {
                                    // Agregar la imagen al PDF y ajustar su tamaño
                                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(rutaImagen);
                                    img.ScaleToFit(100, 100); // Cambia el tamaño deseado aquí
                                    PdfPCell celdaImagen = new PdfPCell(img);
                                    celdaImagen.HorizontalAlignment = Element.ALIGN_CENTER;
                                    celdaImagen.VerticalAlignment = Element.ALIGN_MIDDLE;
                                    tabla.AddCell(celdaImagen);
                                }
                                else
                                {
                                    // Si no hay imagen, agrega una con texto que dice "No hay imagen"
                                    PdfPCell celda = new PdfPCell(new Phrase("No hay imagen", FontFactory.GetFont(FontFactory.HELVETICA, 12)));
                                    celda.HorizontalAlignment = Element.ALIGN_CENTER;
                                    celda.VerticalAlignment = Element.ALIGN_MIDDLE;
                                    tabla.AddCell(celda);
                                }
                            }
                            else
                            {
                                PdfPCell cell = null;

                                if (dgvGlass.Columns[j].HeaderText == "Description")
                                {
                                    // Para la columna "Descripción", alinea el texto a la izquierda
                                    cell = new PdfPCell(new Phrase(dgvGlass[j, i].Value.ToString(), FontFactory.GetFont(FontFactory.HELVETICA)));
                                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                }

                                if (dgvGlass.Columns[j].HeaderText == "Price")
                                {
                                    // Para la columna "Descripción", alinea el texto a la izquierda
                                    cell = new PdfPCell(new Phrase("¢" + dgvGlass[j, i].Value.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 10)));
                                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                }
                                else
                                {
                                    // Para otras columnas, mantén el texto centrado
                                    cell = new PdfPCell(new Phrase(dgvGlass[j, i].Value.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 12)));
                                    cell.VerticalAlignment = Element.ALIGN_LEFT;
                                }

                                // Ajusta el tamaño de las celdas
                                cell.FixedHeight = 170f; // Aumentamos la altura a 150 unidades
                                cell.PaddingLeft = 5f; // Agrega un relleno a la izquierda para alinear el texto correctamente

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
                if (CompanyCache.IdCompany == 310171783)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VidriosVegaLogo.png";
                    rutaLogo = ruta + Url;

                }

                if (CompanyCache.IdCompany == 3101794685)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\RioClaroLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 111560456)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\DialexLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 31025820)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\AluviLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 31028013)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\InnovaLogo.png";
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
                if (CompanyCache.IdCompany == 1230123)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\GlassWinLogo.png";
                    rutaLogo = ruta + Url;
                }
                if (CompanyCache.IdCompany == 310108681)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VidrioCentroLogo.png";
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

                // Agregar filas y celdas de datos con imágenes
                for (int i = 0; i < dgvGlass.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvGlass.Columns.Count; j++)
                    {
                        if (dgvGlass[j, i].Value != null)
                        {
                            // Si es la columna de imagen (ajusta según tu estructura de datos)
                            if (dgvGlass.Columns[j].HeaderText == "URL")
                            {
                                string rutaImagen = dgvGlass[j, i].Value.ToString();
                                if (!string.IsNullOrEmpty(rutaImagen) && File.Exists(rutaImagen))
                                {
                                    // Agregar la imagen al PDF y ajustar su tamaño
                                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(rutaImagen);
                                    img.ScaleToFit(100, 100); // Cambia el tamaño deseado aquí
                                    PdfPCell celdaImagen = new PdfPCell(img);
                                    celdaImagen.HorizontalAlignment = Element.ALIGN_CENTER;
                                    celdaImagen.VerticalAlignment = Element.ALIGN_MIDDLE;
                                    tabla.AddCell(celdaImagen);
                                }
                                else
                                {
                                    // Si no hay imagen, agrega una con texto que dice "No hay imagen"
                                    PdfPCell celda = new PdfPCell(new Phrase("No hay imagen", FontFactory.GetFont(FontFactory.HELVETICA, 12)));
                                    celda.HorizontalAlignment = Element.ALIGN_CENTER;
                                    celda.VerticalAlignment = Element.ALIGN_MIDDLE;
                                    tabla.AddCell(celda);

                                }
                            }
                            else
                            {
                                PdfPCell cell = null;

                                if (dgvGlass.Columns[j].HeaderText == "Description")
                                {
                                    // Para la columna "Descripción", alinea el texto a la izquierda
                                    cell = new PdfPCell(new Phrase(dgvGlass[j, i].Value.ToString(), FontFactory.GetFont(FontFactory.HELVETICA)));
                                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                }

                                if (dgvGlass.Columns[j].HeaderText == "Price")
                                {
                                    // Para la columna "Descripción", alinea el texto a la izquierda
                                    cell = new PdfPCell(new Phrase("¢" + dgvGlass[j, i].Value.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 10)));
                                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                }
                                else
                                {
                                    // Para otras columnas, mantén el texto centrado
                                    cell = new PdfPCell(new Phrase(dgvGlass[j, i].Value.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 12)));
                                    cell.VerticalAlignment = Element.ALIGN_LEFT;
                                }

                                // Ajusta el tamaño de las celdas
                                cell.FixedHeight = 170f; // Aumentamos la altura a 150 unidades
                                cell.PaddingLeft = 5f; // Agrega un relleno a la izquierda para alinear el texto correctamente

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
            cbProveedor.DataSource = dt;
            cbProveedor.DisplayMember = "Nombre";

            cbProveedorDesglose.DataSource = dt;
            cbProveedorDesglose.DisplayMember = "Nombre";
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
    }
}