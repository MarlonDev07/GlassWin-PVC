﻿using Dominio.ClassFunction.InputBox;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Negocio.Client;
using Negocio.Company.Account;
using Negocio.Company.AdmProyecto;
using Negocio.Company.Bill;
using Precentacion.User.DashBoard;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MaterialSkin.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;

namespace Precentacion.User.Accounts
{
    public partial class frmManagerCxC : MaterialForm
    {
        #region Variables
        N_CxC N_CxC = new N_CxC();
        N_CxP N_CxP = new N_CxP();
        N_AdmProyecto N_AdmProyecto = new N_AdmProyecto();
        N_Client N_Client = new N_Client();
        bool EventFormClose = true;
        #endregion

        #region Constructor
        public frmManagerCxC()
        {
            InitializeComponent();
            LoadClient();
            AccountsUI.loadMaterial(this);
            LoadSupplyers();
            //LoadCxC();
        }
        #endregion

        #region CxC

        #region Load Functions Client
        private void LoadClient()
        {
            //Cargar Clientes en un DataTable
            DataTable dataTable = N_Client.LoadClient();

            //Pasar los datos del DataTable a un dAtaGridView
            if (dataTable != null)
            {
                dgvClient.DataSource = dataTable;

                //Hacer Invisbles la Columna [0]
                dgvClient.Columns[0].Visible = false;
                dgvClient.Columns[3].Visible = false;

                //Cambiar Nombre de las Columnas
                dgvClient.Columns[1].HeaderText = "Nombre";
                dgvClient.Columns[2].HeaderText = "Telefono";
                dgvClient.Columns[4].HeaderText = "Direccion";
                dgvClient.Columns[5].HeaderText = "Correo";

                //Ajustar las columnas al Ancho del formulario
                dgvClient.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }


        }
        #endregion

        #region Load Functions CxC
        private void verCuentasPorCobrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
           //Pasar los Datos del Cliente Selecccionado a los Textbox
            txtId.Text = dgvClient.CurrentRow.Cells[0].Value.ToString();
            txtName.Text = dgvClient.CurrentRow.Cells[1].Value.ToString();
            LoadCxC();
            ConfigDataGrid();
            LoadBalance();
            TabControl.SelectedTab = tabCxC;


        }
        private void LoadCxC()
        {
            // Cargar CxC en un DataTable
            DataTable dataTable = N_CxC.FindCxCforClient(Convert.ToInt32(txtId.Text));
            if (dataTable != null)
            {
                // Cargar los datos del DataTable en el DataGridView
                dgvCxC.DataSource = dataTable;

                // Configurar el DataGrid después de cargar los datos
                ConfigDataGrid();
            }
        }

        private void ConfigDataGrid()
        {
            if (dgvCxC.Columns.Count == 0) return;  // Verificar que haya columnas

            // Cambiar Nombre de las Columnas
            dgvCxC.Columns[0].HeaderText = "Id Cuenta";
            dgvCxC.Columns[1].HeaderText = "Id Factura";
            dgvCxC.Columns[2].HeaderText = "Monto Inicial";
            dgvCxC.Columns[3].HeaderText = "Saldo Pendiente";
            dgvCxC.Columns[4].HeaderText = "Proyecto";
            dgvCxC.Columns[5].HeaderText = "Fecha";
            dgvCxC.Columns[6].HeaderText = "Fecha de Vencimiento";

            // Ajustar las columnas al Ancho del formulario
            dgvCxC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Seleccionar todas las filas
            dgvCxC.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Hacer que la columna "Fecha de Vencimiento" sea editable
            dgvCxC.Columns[6].ReadOnly = false;

            // Asegurar que las demás columnas no sean editables
            for (int i = 0; i < dgvCxC.Columns.Count; i++)
            {
                if (i != 6)
                {
                    dgvCxC.Columns[i].ReadOnly = true;
                }
            }
        }





        private void LoadBalance() 
        { 
            //sumar eL Balance de las Cuentas por Cobrar
            decimal Balance = 0;
            for (int i = 0; i < dgvCxC.Rows.Count; i++)
            {
                Balance += Convert.ToDecimal(dgvCxC.Rows[i].Cells[2].Value);
            }
            txtTotal.Text = Balance.ToString("c");

            //Sumar el Balance Pendiente de las Cuentas por Cobrar
            decimal BalancePending = 0;
            for (int i = 0; i < dgvCxC.Rows.Count; i++)
            {
                BalancePending += Convert.ToDecimal(dgvCxC.Rows[i].Cells[3].Value);
            }
            txtPendiente.Text = BalancePending.ToString("c");

            txtCount.Text = ((dgvCxC.Rows.Count)-1).ToString();

        }


        #endregion

        #region ContextMenuStrip
        private void btnDeposit_Click(object sender, EventArgs e)
        {
            try
            {
                    //Preguntar si desea realizar el deposito
                    DialogResult result = MessageBox.Show("¿Desea realizar el deposito a la factura: " + dgvCxC.CurrentRow.Cells[1].Value.ToString() + " de la cuenta del Cliente: " + txtName.Text + "?", "Depositar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        //Preguntar el monto a depositar
                        string Result = InputBox.Show("Ingrese el Monto que se va Abonar", "Abonar a la Cuenta");
                        if (Result != "")
                        {
                            decimal Amount = Convert.ToDecimal(Result);
                            if (Amount > 0)
                            {
                                int IdAccount = Convert.ToInt32(dgvCxC.CurrentRow.Cells[0].Value);
                                decimal OutstandingBalance = Convert.ToDecimal(dgvCxC.CurrentRow.Cells[3].Value);
                                decimal NewOutstandingBalance = OutstandingBalance - Amount;
                                if (NewOutstandingBalance >= 0)
                                {
                                    if (N_CxC.UpdateCxC(IdAccount, Convert.ToInt32(dgvCxC.CurrentRow.Cells[1].Value), Convert.ToDecimal(dgvCxC.CurrentRow.Cells[2].Value), NewOutstandingBalance))
                                    {
                                       
                                        MessageBox.Show("Se realizo el deposito correctamente", "Deposito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        GeneratePdfCxC(Result.ToString());
                                        LoadCxC();
                                        LoadBalance();
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se pudo realizar el deposito", "Deposito", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void abonarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnDeposit_Click(null, null);
        }

        private void frmManagerCxC_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Validar que el evento sea necesario
            if (EventFormClose == true)
            {
                frmDashUser frm = new frmDashUser();
                frm.Show();
            }
        }
        #endregion

        #region Generacion de Reportes
        private void GeneratePdfCxC(string Deposit)
        {
                #region Crear el documento
            try
            {
                // Obtener el Monto Inicial y Final de la Cuenta x Cobrar
                decimal AmountInitial = Convert.ToDecimal(dgvCxC.CurrentRow.Cells[2].Value);
                decimal AmountPending = Convert.ToDecimal(dgvCxC.CurrentRow.Cells[3].Value);
                decimal NewAmountPending = AmountPending - Convert.ToDecimal(Deposit); 
                
                //Obtener el Nombre del Proyecto
                string Proyecto = dgvCxC.CurrentRow.Cells[4].Value.ToString();

                // Obtener el directorio del escritorio y las carpetas necesarias
                string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string CarpetaFactura = Path.Combine(escritorio, "Facturas Abono");
                string carpetaNombre = Path.Combine(CarpetaFactura, txtName.Text.Trim());
                string NameFile = "Abono° " + txtName.Text + ".pdf";

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
                Chunk tituloChunk = new Chunk("Recibo De Dinero ", TextSubrayado);
                encabezadoParagraph.Add(tituloChunk);
                encabezadoParagraph.Add(Chunk.NEWLINE); // Salto de línea

                // Agregar un subtítulo

                // Generar número Random para la Factura
                Random random = new Random();
                int randomNumber = random.Next(0, 1000000);

                Chunk subtituloChunk = new Chunk("Número de Factura: " + randomNumber.ToString(), textFont);
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
                paragraphh.Add(new Chunk($"Hemos Recibido de {txtName.Text}  ", textFont));
                paragraphh.Add(new Chunk($"el Monto de {Deposit} ", textFont) );
                paragraphh.Add(new Chunk($"por Concepto de Adelanto o Cancelacion del Proyecto {Proyecto}", textFont));
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


                // Línea para la Firma del Cliente
                Paragraph firmaParagraph = new Paragraph("___________________________\nFirma del Cliente", textFont);
                firmaParagraph.Alignment = Element.ALIGN_LEFT;

                document.Add(firmaParagraph);
                document.Add(new Paragraph(" ")); // Espacio en blanco
                #endregion

                #region Pie de Página
                Paragraph piePaginaParagraph = new Paragraph();
                piePaginaParagraph.Alignment = Element.ALIGN_CENTER;
                piePaginaParagraph.Add(new Chunk("¡Gracias por su preferencia!", textFont));
                document.Add(piePaginaParagraph);

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
            }
            catch (Exception)
            {

            }
            #endregion
        }


        #endregion

        #endregion

        #region CxP 

        #region Cargas de Datos
        private void LoadDGV()
        {
            //Cargar CxC en un DataTable
            DataTable dataTable = N_CxP.SelectAll();
            if (dataTable != null)
            {
                //Cargar los datos del DataTable en el DataGridView
                dgvCxP.DataSource = dataTable;
            }

            //Ocultar Columnas
            dgvCxP.Columns[5].Visible = false;

            //Cambiar Nombre de las Columnas
            dgvCxP.Columns[0].HeaderText = "Id Cuenta";
            dgvCxP.Columns[1].HeaderText = "Proveedor";
            dgvCxP.Columns[2].HeaderText = "Monto Inicial";
            dgvCxP.Columns[3].HeaderText = "Monto Pendiente";
            dgvCxP.Columns[4].HeaderText = "Estado";

            //Ajustar las columnas al Ancho del formulario
            dgvCxP.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            CalcBalances();

        }
        private void LoadProyects()
        {
            //Cargar los Proyectos en el ComboBox
            try
            {
                DataTable dataTable = N_AdmProyecto.ListarNombresProyectos("Activo");
                if (dataTable != null)
                {
                    cbProyect.DataSource = dataTable;
                    //Mostrar los Datos de la Columna ProyectoCompleto
                    cbProyect.DisplayMember = "ProyectoCompleto";
                    cbProyect.ValueMember = "IdAdmProyecto";
                    cbProyect.SelectedIndex = 0;

                }

            }
            catch (Exception)
            {

            }

        }
        #endregion

        #region Calculo
        private void CalcBalances() 
        { 
            //Sumar el Balance de las Cuentas por Pagar
            decimal Balance = 0;

            for (int i = 0; i < dgvCxP.Rows.Count; i++)
            {
                Balance += Convert.ToDecimal(dgvCxP.Rows[i].Cells[2].Value);
            }
            txtTotalCxP.Text = Balance.ToString("c");

            //Sumar el Balance Pendiente de las Cuentas por Pagar
            decimal BalancePending = 0;
            for (int i = 0; i < dgvCxP.Rows.Count; i++)
            {
                BalancePending += Convert.ToDecimal(dgvCxP.Rows[i].Cells[3].Value);
            }
            txtPendienteCxP.Text = BalancePending.ToString("c");
        }
        #endregion

        #region TabControl
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TabControl.SelectedIndex == 2)
            {
                LoadDGV();
                LoadProyects();
            }

        }
        #endregion

        #region Botones
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtSupplyer.Text != "")
            {
                string Supplyer = txtSupplyer.Text;

                DataTable dataTable = N_CxP.SelectBySupplyer(Supplyer);
                if (dataTable != null)
                {
                    txtSupplyer.Text = "";
                    //Cargar los datos del DataTable en el DataGridView
                    dgvCxP.DataSource = dataTable;
                    CalcBalances();
                }
            }
        }

        public void LoadSupplyers()
        {
            N_CxP supplyerLogic = new N_CxP();
            DataTable dtSupplyers = supplyerLogic.GetSupplyers();

            txtSupplyer.Items.Clear();
            foreach (DataRow row in dtSupplyers.Rows)
            {
                txtSupplyer.Items.Add(row["Supplyer"].ToString());
            }
        }


        private void btnList_Click(object sender, EventArgs e)
        {
            LoadDGV();
        }
        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (ValidateField()) 
            {
                string Supplyer = cbSupplier.Text;
                Decimal AmountInitial = Convert.ToDecimal(txtAmountInitial.Text);
                string Detail = txtDetail.Text;
                
                if (InsertCxP(Supplyer,AmountInitial,Detail))
                {
                    CleanFields();
                    LoadDGV();
                    tabControlCxP.SelectedIndex = 0;
                }
            }
        }
        #endregion

        #region Validaciones y F.Extras
        private bool ValidateField() 
        {
            try
            {
                if (cbSupplier.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe ingresar un proveedor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (txtAmountInitial.Text == "")
                {
                    MessageBox.Show("Debe Inresa un Monto para la Cuenta");
                    return false;
                }
                if (txtDetail.Text == "")
                {
                    MessageBox.Show("Debe Ingresar un Detalle para la Cuenta");
                    return false;
                }
                if (cbProyect.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe Seleccionar un Proyecto");
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Error al validar los campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private void CleanFields()
        {
            txtSupplyer.Text = "";
            txtAmountInitial.Text = "";
            txtDetail.Text = "";
        }
        #endregion

        #region Inserciones 
        private bool InsertCxP(string Supplyer, decimal AmountInitial, string Detail)
        {
            bool result = false;

            if (N_CxP.InsertCxP(Supplyer, AmountInitial, Detail)) 
            {
                int IdCxP = N_CxP.LastIdCxP();
                InsertIdCxP(IdCxP);

                result = true;
                MessageBox.Show("Cuenta por Pagar Creada correctamente", "CxP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadProyects();
            }
            else
            {
                MessageBox.Show("Error en la Creacion de la Cuenta por Pagar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return result;
        }
        private bool InsertIdCxP(int Id)
        {
            //Obtener el Valor anterior del - en el ComboBox
            string[] IdProyect = cbProyect.Text.Split('-');
            int IdProyectInt = Convert.ToInt32(IdProyect[0]);
            if (N_AdmProyecto.ActualizarIdCxP(IdProyectInt,Id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Eventos
        private void txtAmountInitial_TextChanged(object sender, EventArgs e)
        {     
          //Hacer que el campo solo acepte numeros
          if (System.Text.RegularExpressions.Regex.IsMatch(txtAmountInitial.Text, "[^0-9]"))
          {
              MessageBox.Show("Solo se permiten numeros", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
              txtAmountInitial.Text = txtAmountInitial.Text.Remove(txtAmountInitial.Text.Length - 1);
          }
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Obtener el Id de la Cuenta por Pagar
            int IdAccount = Convert.ToInt32(dgvCxP.CurrentRow.Cells[0].Value);

            //Pregunar si desea Abonar a la Cuenta
            DialogResult result = MessageBox.Show("¿Desea Abonar a la Cuenta por Pagar: " + IdAccount + "?", "Abonar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string Abono = InputBox.Show("¿Cuanto desea Abonar?", "Abono");
                if (Abono != "")
                {
                    decimal Amount = Convert.ToDecimal(Abono);
                    decimal AmountPending = Convert.ToDecimal(dgvCxP.CurrentRow.Cells[3].Value);
                    decimal NewAmountPending = AmountPending - Amount;
                    if (Amount <= AmountPending)
                    {
                        N_CxP.UpdateCxP(IdAccount, NewAmountPending);
                         GeneratePdfCxP(Amount);
                        LoadDGV();
                        
                        MessageBox.Show("Abono Realizado Correctamente", "Abono", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("El monto a Abonar debe ser mayor a 0 O Menor al Monto Actual", "Abono", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }


        }
        #endregion
       
        #region Generacion de Reportes
        private void GeneratePdfCxP(decimal Deposit)
        {
                #region Crear el documento
            try
            {
                // Obtener el Monto Inicial y Final de la Cuenta x Cobrar
                decimal AmountInitial = Convert.ToDecimal(dgvCxP.CurrentRow.Cells[2].Value);
                decimal AmountPending = Convert.ToDecimal(dgvCxP.CurrentRow.Cells[3].Value)-Deposit;
                string Supplier = dgvCxP.CurrentRow.Cells[1].Value.ToString();


                // Obtener el directorio del escritorio y las carpetas necesarias
                string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string CarpetaFactura = Path.Combine(escritorio, "Facturas Cuentas Por Pagar");
                string carpetaNombre = Path.Combine(CarpetaFactura, Supplier);
                string NameFile = "Abono° " + CompanyCache.Name + ".pdf";

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

                PdfPTable Encabezado = new PdfPTable(2);
                Encabezado.WidthPercentage = 120;
                string rutaLogo = "";
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
                if (CompanyCache.IdCompany == 205150849)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\MakyLogo.png";
                    rutaLogo = ruta + Url;
                }


                // Agrega la imagen a la primera celda
                PdfPCell imageCell = new PdfPCell(iTextSharp.text.Image.GetInstance(rutaLogo));
                imageCell.Border = PdfPCell.NO_BORDER;
                imageCell.FixedHeight = 100f; // Ajusta la altura de la imagen
                imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
                Encabezado.AddCell(imageCell);

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

                Paragraph encabezadoParagraph = new Paragraph();
                encabezadoParagraph.Alignment = Element.ALIGN_CENTER;

                // Agregar un título
                Chunk tituloChunk = new Chunk("Recibo por Dinero", titleFont);
                encabezadoParagraph.Add(tituloChunk);
                encabezadoParagraph.Add(Chunk.NEWLINE); // Salto de línea

                // Agregar un subtítulo

                // Generar número Random para la Factura
                Random random = new Random();
                int randomNumber = random.Next(0, 1000000);

                Chunk subtituloChunk = new Chunk("Número de Factura: " + randomNumber.ToString(), textFont);
                encabezadoParagraph.Add(subtituloChunk);
                encabezadoParagraph.Add(Chunk.NEWLINE); // Salto de línea

                document.Add(encabezadoParagraph);
                document.Add(new Paragraph(" ")); // Espacio en blanco
                #endregion

                #region Datos del Abono y Cliente
                // Crear una línea para el Nombre del Cliente
                Paragraph clienteParagraph = new Paragraph();
                clienteParagraph.Add(new Chunk("Proveedor: ", textFont)); // Etiqueta "Cliente:"
                clienteParagraph.Add(new Chunk(Supplier, textFont)); // Nombre del cliente obtenido de un cuadro de texto (asegúrate de tener este control en tu formulario)
                clienteParagraph.Add(Chunk.NEWLINE); // Salto de línea
                clienteParagraph.Add(new Chunk("Monto Inicial: ¢", textFont)); // Etiqueta "Monto Inicial:"
                clienteParagraph.Add(new Chunk(AmountInitial.ToString(), textFont)); // Monto inicial obtenido de un cuadro de texto (asegúrate de tener este control en tu formulario)
                clienteParagraph.Add(Chunk.NEWLINE); // Salto de línea
                clienteParagraph.Add(new Chunk("Monto a Depositar: ¢", textFont)); // Etiqueta "Monto a Depositar:"
                clienteParagraph.Add(new Chunk(Deposit.ToString(), textFont)); // Monto a depositar obtenido de un cuadro de texto (asegúrate de tener este control en tu formulario)
                clienteParagraph.Add(Chunk.NEWLINE); // Salto de línea
                clienteParagraph.Add(new Chunk("Monto Restante: ¢", textFont)); // Etiqueta "Monto Restante:"
                clienteParagraph.Add(new Chunk(AmountPending.ToString(), textFont)); // Monto restante obtenido de un cuadro de texto (asegúrate de tener este control en tu formulario)
                clienteParagraph.Add(Chunk.NEWLINE); // Salto de línea
                clienteParagraph.Alignment = Element.ALIGN_LEFT;

                document.Add(clienteParagraph);
                document.Add(new Paragraph(" ")); // Espacio en blanco
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
        #endregion

        #endregion

        private void btnnewCxC_Click(object sender, EventArgs e)
        {
            frmCxCNew frm = new frmCxCNew();
            frm.ShowDialog();
        }

        private void eliminarCuentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                N_Bill N_Bill = new N_Bill();
                //Obtener el Id de la Cuenta por Cobrar y la Factura
                int IdAccount = Convert.ToInt32(dgvCxC.CurrentRow.Cells[0].Value);
                int IdBill = Convert.ToInt32(dgvCxC.CurrentRow.Cells[1].Value);
                if (N_AdmProyecto.SeleccionarIdAdmProyectoyElimanar(IdAccount))
                {
                    if (N_CxC.DeleteCxC(IdAccount))
                    {
                        MessageBox.Show("Cuenta por Cobrar Eliminada Correctamente", "CxC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadCxC();
                        LoadBalance();
                    }
                    else
                    {
                        MessageBox.Show("Error al Eliminar la Cuenta por Cobrar", "CxC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    N_CxC.DeleteCxC(IdAccount);
                    MessageBox.Show("Cuenta por Cobrar Eliminada Correctamente", "CxC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCxC();
                    LoadBalance();
                }
               
            }
            catch (Exception)
            {

               
            }
           
        }

        private void textBusquedaNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBusquedaNombre2_TextChanged(object sender, EventArgs e)
        {
            dgvClient.CurrentCell = null;
            try
            {
                foreach (DataGridViewRow r in dgvClient.Rows)
                {
                    bool rowVisible = false;
                    foreach (DataGridViewCell c in r.Cells)
                    {
                        if (c.Value != null && c.Value.ToString().ToUpper().Contains(txtBusquedaNombre2.Text.ToUpper()))
                        {
                            rowVisible = true;
                            break;
                        }
                    }
                    r.Visible = rowVisible;
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("Error al eliminar el cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCxC_CellEndEdit_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6) // Verificar si es la columna "Fecha de Vencimiento"
            {
                try
                {
                    int idCuenta = Convert.ToInt32(dgvCxC.Rows[e.RowIndex].Cells[0].Value); // Obtener Id Cuenta
                    DateTime nuevaFecha = Convert.ToDateTime(dgvCxC.Rows[e.RowIndex].Cells[6].Value); // Obtener la nueva fecha de vencimiento

                    // Llamar al método para actualizar la base de datos
                    N_CxC.ActualizarFechaVencimiento(idCuenta, nuevaFecha);

                    MessageBox.Show("Fecha de vencimiento actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar la fecha de vencimiento: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvCxC_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

        }

        private void dgvCxC_DoubleClick(object sender, EventArgs e)
        {

        }

        private void dgvCxC_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Obtener la celda actualmente seleccionada
            DataGridViewCell currentCell = dgvCxC.CurrentCell;

            // Verificar si la celda actual es la columna "Fecha de Vencimiento"
            if (currentCell != null && currentCell.ColumnIndex == 6)
            {
                // Habilitar la edición de la celda
                currentCell.ReadOnly = false;
            }
        }

  

        private void dgvCxC_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Obtener la celda actualmente seleccionada
            DataGridViewCell currentCell = dgvCxC.CurrentCell;

            // Verificar si la celda actual es la columna "Fecha de Vencimiento"
            if (currentCell != null && currentCell.ColumnIndex == 6)
            {
                // Habilitar la edición de la celda
                currentCell.ReadOnly = false;
            }
        }

        private void dgvCxC_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Obtener la celda actualmente seleccionada
            DataGridViewCell currentCell = dgvCxC.CurrentCell;

            // Verificar si la celda actual es la columna "Fecha de Vencimiento"
            if (currentCell != null && currentCell.ColumnIndex == 6)
            {
                dgvCxC.Columns[6].ReadOnly = false; // Asegúrate de que la columna "Fecha de Vencimiento" no esté bloqueada para edición
                currentCell.ReadOnly = false; // Asegúrate de que la celda actual no esté bloqueada para edición

            }
        }
    }
}