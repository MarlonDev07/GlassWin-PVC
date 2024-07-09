using Dominio;
using Dominio.ClassUser;
using Dominio.Model.ClasscmbArticulo;
using Dominio.Model.ClassWindows;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MaterialSkin.Controls;
using Negocio.Client;
using Negocio.Company.Quote;
using Precentacion.User.Bill;
using Precentacion.User.Client;
using Precentacion.User.DashBoard;
using Precentacion.User.Quote.Accesorios;
using Precentacion.User.Quote.Prefabricado;
using Precentacion.User.Quote.SandBlasting;
using Precentacion.User.Quote.Windows;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Image = iTextSharp.text.Image;

namespace Precentacion.User.Quote.Quote
{
    public partial class frmQuote : Form
    {
        #region Variables
        N_Quote NQuote = new N_Quote();
        N_Client NClient = new N_Client();
        int IdClient, UpdatePrice;
        int QuoteSave = 0;
        decimal SubTotal, IVA, Total, Discount, Labour;
        DateTime Date = DateTime.Now;
        public bool Edit = false;
        public bool EventClose = true;
        private decimal Descuento;
        private decimal ManoObra;
        private decimal LimiteCredito;
        #endregion

        #region Constructor

        // Propiedades para el borde personalizado
        private const int borderWidth = 45; // Grosor del borde
        private Color borderColor = Color.Orange; // Color del borde

        // Botones personalizados
        private Button btnMinimize;
        private Button btnMaximize;
        private Button btnClose;
        public frmQuote()
        {
            InitializeComponent();
            loadFunction();
            this.FormBorderStyle = FormBorderStyle.None; // Eliminar el borde predeterminado
            this.Padding = new Padding(0, borderWidth, 0, 0); // Asegurar que el contenido no se superponga al borde superior
            this.Paint += new PaintEventHandler(frmQuote_Paint); // Agregar el manejador del evento Paint
            this.BackColor = Color.White; // Color de fondo del formulario

            // Crear y configurar los botones personalizados
            InitializeCustomButtons();

        }
        #endregion
        private void InitializeCustomButtons()
        {
            btnMinimize = new Button();
            btnMinimize.Text = "_";
            btnMinimize.Size = new Size(23, 23);
            btnMinimize.Location = new Point(this.Width - 90, 0);
            btnMinimize.Click += BtnMinimize_Click;
            this.Controls.Add(btnMinimize);

            btnMaximize = new Button();
            btnMaximize.Text = "⬜";
            btnMaximize.Size = new Size(23, 23);
            btnMaximize.Location = new Point(this.Width - 60, 0);
            btnMaximize.Click += BtnMaximize_Click;
            this.Controls.Add(btnMaximize);

            btnClose = new Button();
            btnClose.Text = "X";
            btnClose.Size = new Size(23, 23);
            btnClose.Location = new Point(this.Width - 30, 0);
            btnClose.Click += BtnClose_Click;
            this.Controls.Add(btnClose);

            cbIva.SelectedIndex = 6;
            // Ejecutar el cálculo de IVA y precios automáticamente al cargar el formulario
            cbIva_SelectedIndexChanged(this, EventArgs.Empty);
        }

        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void BtnMaximize_Click(object sender, EventArgs e)
        {
            this.WindowState = (this.WindowState == FormWindowState.Maximized) ? FormWindowState.Normal : FormWindowState.Maximized;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmQuote_Paint(object sender, PaintEventArgs e)
        {
            // Crear un lápiz con el color y grosor deseado
            using (Pen pen = new Pen(borderColor, borderWidth))
            {
                // Dibujar solo el borde superior del formulario
                e.Graphics.DrawLine(pen, 0, 0, this.ClientSize.Width, 0);
            }
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_NCPAINT = 0x85;
            const int WM_NCHITTEST = 0x84;
            const int HTCAPTION = 0x2;

            if (m.Msg == WM_NCPAINT)
            {
                // Forzar la redibujación del borde
                m.Result = IntPtr.Zero;
                return;
            }

            if (m.Msg == WM_NCHITTEST)
            {
                // Permitir mover el formulario al hacer clic en la zona del borde
                base.WndProc(ref m);
                if ((int)m.Result == HTCAPTION)
                {
                    m.Result = (IntPtr)HTCAPTION;
                    return;
                }
            }

            base.WndProc(ref m);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Invalidate(); // Forzar la redibujación del formulario

            // Ajustar la posición de los botones personalizados
            if (btnMinimize != null && btnMaximize != null && btnClose != null)
            {
                btnMinimize.Location = new Point(this.Width - 90, 0);
                btnMaximize.Location = new Point(this.Width - 60, 0);
                btnClose.Location = new Point(this.Width - 30, 0);
            }
        }


        #region LoadFunctions
        private void loadFunction()
        {
            cbIva.SelectedIndex = 6;
            txtManoObra.Text = "0";
            txtDescuento.Text = "0";
            if (txtidQuote.Text == "")
            {
                loadIDQuote();
            }
            loadDate();
            loadWindows();
            LoadConditionals();
            if (UserCache.Name == "VitroTaller")
            {
                btnSistemas.Visible = false;
            }
        }

        private void loadIDQuote()
        {
            int ID = NQuote.InsertQuoteAndGetLastID(Date, "", "", "", 0, 0, 0, 0, 0, 4);
            txtidQuote.Text = ID.ToString();
        }
        private void loadDate()
        {
            txtDate.Text = Date.ToString("dd/MM/yyyy");
        }
        public void loadWindows()
        {
            //cbIva.SelectedIndex = 6;
            DataTable dt = new DataTable();
            dt = NQuote.LoadWindows(Convert.ToInt32(txtidQuote.Text));
            dgCotizaciones.DataSource = dt;
            dgCotizaciones.RowTemplate.Height = 250;
            dgCotizaciones.Columns[0].Width = 90;
            dgCotizaciones.Columns[1].Width = 300;
            dgCotizaciones.Columns[2].Width = 200;
            dgCotizaciones.Columns[3].Width = 115;
            if (UserCache.Name == "VitroTaller")
            {
                //Ocultar la Columna Precio
                dgCotizaciones.Columns[3].Visible = false;
            }

            //Cambiar el nombre de las columnas
            dgCotizaciones.Columns[2].HeaderText = "Descripcion";
            dgCotizaciones.Columns[3].HeaderText = "Precio";
            CalcPrices();


        }
        public void LoadConditionals()
        {
            //Vidrios Maky
            if (CompanyCache.IdCompany == 205150849 || CompanyCache.IdCompany == 3101794685)
            {
                cbOpcion.SelectedIndex = 0;
                txtConditional2.Text = "2.El tiempo de entrega en promedio es de 15 días hábiles, y rige a partir de la cancelación del adelanto.";
                txtConditional3.Text = "3.La garantía no cubre el vidrio cuando hay daños ocacionados por el cliente o terceros";
                txtConditional4.Text = "4.Para un mejor acabado e instalación del producto, se recomienda que el área de instalación cuente con la primera capa o mano de pintura.";
                txtConditional5.Text = "5.No se incluye refuerzos en paredes livianas.";
                txtConditional6.Text = "6.No se incluye andamios. De ser necesarios se le pueden brindar por un costo adicional.";
                txtConditional7.Text = "7.No es nuestra responsabilidad, si durante el proceso de instalación se perforan tuberías, sea de agua potable, de aguas negras o de conducción eléctrica.";
                txtConditional8.Visible = false;
                txtConditional9.Visible = false;
                txtConditional10.Visible = false;
            }
            //Vidrios DiAlex
            if (CompanyCache.IdCompany == 111560456)
            {
                cbOpcion.Visible = false;
                txtConditional1.Text = "1.Cotización valida por 10 días naturales después de la fecha de emisión";
                txtConditional2.Text = "2.Se requiere el 60% de anticipo, 20% trascurrida la obra y el 20% restante contra entrega";
                txtConditional3.Text = "3.Cualquier variación en las cantidades o detalles en esta Oferta-Contrato requiere de una nueva cotización.";
                txtConditional4.Text = "4.Plazo de entrega 22 días hábiles a partir de emitida la Orden de Compra";
                txtConditional5.Visible = false;
                txtConditional6.Visible = false;
                txtConditional7.Visible = false;
                txtConditional8.Visible = false;
                txtConditional9.Visible = false;
                txtConditional10.Visible = false;
            }
            //Aluvi
            if (CompanyCache.IdCompany == 31025820)
            {

                cbOpcion.Visible = false;
                txtConditional1.Text = "1.Esta oferta incluye, materiales, mano de obra, transporte e instalación";
                txtConditional2.Text = "2.Oferta NO incluye desinstalación de buques existente";
                txtConditional3.Text = "3.Se cotizan productos marca Extralum";
                txtConditional4.Text = "4.Se requiere realizar la visita para tomar medidas rectificadas";
                txtConditional5.Text = "5.Forma de pago 50% adelanto 50% contra entrega";
                txtConditional6.Text = "6.Entrega de prefabricados de 8 a 20 días hábiles";
                txtConditional7.Text = "7.Por favor revisar cantidades, sistema y acabados";
                txtConditional8.Text = "8.Validez de cotización 8 días";
                txtConditional9.Text = "9.Precio puede variar según aumentos del mercado";
                txtConditional10.Text = "10.Garantía 1 año contra defectos propios del sistema(cierres, rodajes, empaques) NO se incluye garantía sobre rayones o quebraduras de vidrios.";

            }
            //Vidrios Altura
            if (CompanyCache.IdCompany == 112540885)
            {

                cbOpcion.Visible = false;
                txtConditional1.Text = "1.Esta oferta incluye, materiales, mano de obra, transporte e instalación";
                txtConditional2.Text = "2.Oferta NO incluye desinstalación de buques existente";
                txtConditional3.Text = "3.Se requiere realizar la visita para tomar medidas rectificadas";
                txtConditional4.Text = "4.Forma de pago 50% adelanto 50% contra entrega";
                txtConditional5.Text = "5.Entrega de prefabricados de 8 a 20 días hábiles";
                txtConditional6.Text = "6.Por favor revisar cantidades, sistema y acabados";
                txtConditional7.Text = "7.Validez de cotización 8 días";
                txtConditional8.Text = "8.Precio puede variar según aumentos del mercado";
                txtConditional9.Text = "9.Garantía 1 año contra defectos propios del sistema(cierres, rodajes, empaques) NO se incluye garantía sobre rayones o quebraduras de vidrios.";
                txtConditional10.Visible = false;

            }
            //Vidrios Maky
            if (CompanyCache.IdCompany == 25550555)
            {
                cbOpcion.Visible = false;
                txtConditional1.Text = "1.Esta oferta incluye, materiales, mano de obra, transporte e instalación";
                txtConditional2.Text = "2.Oferta NO incluye desinstalación de buques existente";
                txtConditional3.Text = "3.Se cotizan productos marca Extralum";
                txtConditional4.Text = "4.Se requiere realizar la visita para tomar medidas rectificadas";
                txtConditional5.Text = "5.Forma de pago 50% adelanto 50% contra entrega";
                txtConditional6.Text = "6.Entrega de prefabricados de 8 a 20 días hábiles";
                txtConditional7.Text = "7.Por favor revisar cantidades, sistema y acabados";
                txtConditional8.Text = "8.Validez de cotización 8 días";
                txtConditional9.Text = "9.Precio puede variar según aumentos del mercado";
                txtConditional10.Text = "10.Garantía 1 año contra defectos propios del sistema(cierres, rodajes, empaques) NO se incluye garantía sobre rayones o quebraduras de vidrios.";

            }
            //J123
            if (CompanyCache.IdCompany == 1230123)
            {
                cbOpcion.Visible = false;
                txtConditional1.Text = "1.Esta oferta incluye, materiales, mano de obra, transporte e instalación";
                txtConditional2.Text = "2.Oferta NO incluye desinstalación de buques existente";
                txtConditional3.Text = "3.Se cotizan productos marca Extralum";
                txtConditional4.Text = "4.Se requiere realizar la visita para tomar medidas rectificadas";
                txtConditional5.Text = "5.Forma de pago 50% adelanto 50% contra entrega";
                txtConditional6.Text = "6.Entrega de prefabricados de 8 a 20 días hábiles";
                txtConditional7.Text = "7.Por favor revisar cantidades, sistema y acabados";
                txtConditional8.Text = "8.Validez de cotización 8 días";
                txtConditional9.Text = "9.Precio puede variar según aumentos del mercado";
                txtConditional10.Text = "10.Garantía 1 año contra defectos propios del sistema(cierres, rodajes, empaques) NO se incluye garantía sobre rayones o quebraduras de vidrios.";

            }
            //Innova
            if (CompanyCache.IdCompany == 31028013)
            {
                cbOpcion.Visible = false;
                txtConditional1.Text = "1.Garantía 12 meses contra defectos de fábrica e instalación";
                txtConditional2.Text = "2.El costo total de la proforma incluye mano de obra.";
                txtConditional3.Text = "3.Tiempo de entrega: 4 a 5 semanas después de la fecha de pago del primer adelanto";
                txtConditional4.Text = "4.FORMA DE PAGO: 70% de adelanto - 30% contra entrega del proyecto";
                txtConditional5.Visible = false;
                txtConditional6.Visible = false;
                txtConditional7.Visible = false;
                txtConditional8.Visible = false;
                txtConditional9.Visible = false;
                txtConditional10.Visible = false;

            }

        }
        public void LoadDataQuote()
        {
            DataTable dtQuote = new DataTable();
            dtQuote = NQuote.LoadDataQuote(Convert.ToInt32(txtidQuote.Text));
            //Obtener el Valor de La Columna Discount y Labour
            Descuento = Convert.ToDecimal(dtQuote.Rows[0]["Discount"]);
            ManoObra = Convert.ToDecimal(dtQuote.Rows[0]["Labour"]);
            //Asignar los valores a los TextBox
            txtDescuento.Text = Descuento.ToString();
            txtManoObra.Text = ManoObra.ToString();
            //Volver a Calcular los Precios
            CalcPrices();
        }
        #endregion

        #region Bottoms
        public void btnBuscar_Click(object sender, EventArgs e)
        {

            try
            {
                List<clsClient> list = new List<clsClient>();

                if (txtidClient.Text != "" && txtidClient.Text.All(char.IsDigit) == true)
                {
                    list = NClient.ListClient(txtidClient.Text);
                    foreach (var item in list)
                    {
                        if (item.IdClient.ToString() == txtidClient.Text && item.IdCompany == CompanyCache.IdCompany)
                        {
                            IdClient = item.IdClient;
                            txtidClient.Text = item.Name;
                            txtTelefono.Text = item.Phone;
                            txtAdreesClient.Text = item.Address;
                            txtEmail.Text = item.Correo;
                            LimiteCredito = Convert.ToDecimal(item.Limite);
                        }
                    }
                }
                else
                {

                    frmManagerClient frm = new frmManagerClient();
                    frm.EventFormClose = false;
                    frm.Show();
                }

            }
            catch (Exception)
            {
                frmManagerClient frm = new frmManagerClient();
                frm.EventFormClose = false;
                frm.Show();

            }

        }
        private void btnSistemas_Click(object sender, EventArgs e)
        {
            
            try {
                ClsWindows.IDQuote = Convert.ToInt32(txtidQuote.Text);
                frmSelectSystem frm = new frmSelectSystem();
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error. " + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            try { loadWindows(); }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error. " + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                bool Validate = ValidateFields();
                if (Validate)
                {
                    bool res = false;

                    if (CompanyCache.IdCompany == 1230123)
                    {
                        res = true;
                        res = Generate();
                    }
                    else
                    {
                        res = Generate();
                    }

                    if (res)
                    {

                        res = NQuote.EditQuote(Convert.ToInt32(txtidQuote.Text), Date, txtProjetName.Text, txtAddress.Text, "", Convert.ToDecimal(txtDescuento.Text), Convert.ToDecimal(txtManoObra.Text), IVA, SubTotal, Total, IdClient);
                        if (res)
                        {
                            QuoteSave = 1;
                            MessageBox.Show("Cotizacion Guardada", "Proforma Guardada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SendQuoteforWhathsaap();
                            LimpiarCampos();

                        }
                    }
                    else
                    {
                        MessageBox.Show("Error al Guardar la Cotizacion");
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Ocurrió un error. Por favor, verifique que haya ingresado bien todos los datos.\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }


        }

        private void txtidQuote_TextChanged(object sender, EventArgs e)
        {
            loadWindows();
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            { //Obtener La Descripccion de la Ventana
                string Description = dgCotizaciones.CurrentRow.Cells["Description"].Value.ToString();
                if (Description.Contains("Sistema"))
                {
                    //Obtener el id de la ventana seleccionada
                    int idWindows = Convert.ToInt32(dgCotizaciones.CurrentRow.Cells["IdWindows"].Value);
                    ClsWindows.IdWindows = idWindows;
                    ClsWindows.IDQuote = Convert.ToInt32(txtidQuote.Text);

                    //preguntar si desea editar la ventana
                    DialogResult result = MessageBox.Show("¿Desea editar la ventana n° " + dgCotizaciones.CurrentRow.Cells["IdWindows"].Value.ToString() + "?", "Editar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        bool res = NQuote.FindWindows(idWindows);
                        if (res)
                        {
                            frmCalcPriceWindows frm = new frmCalcPriceWindows();
                            frm.Show();
                        }
                        else
                        {
                            MessageBox.Show("Error al Editar la Ventana");
                        }

                    }
                }
                else
                {
                    if (Description.Contains("cmbArticulo"))
                    {
                        //Recorrer el DataGrid y Guardar los Datos de las Ventanas que en la Descricion Contengan cmbArticulo
                        try
                        {
                            ClsWindows.IDQuote = Convert.ToInt32(txtidQuote.Text);
                            List<Cls_CmbArticulo> list = new List<Cls_CmbArticulo>();

                            frmPrefabricado frm = new frmPrefabricado();
                            foreach (DataGridViewRow row in dgCotizaciones.Rows)
                            {
                                if (row.Cells[0].Value != null)
                                {
                                    if (row.Cells[0].Value.ToString() != "")
                                    {
                                        if (row.Cells[2].Value.ToString().Contains("cmbArticulo"))
                                        {
                                            //Guardar en una Lista los Datos
                                            Cls_CmbArticulo cls_CmbArticulo = new Cls_CmbArticulo();
                                            cls_CmbArticulo.IdVentana = Convert.ToInt32(row.Cells[0].Value);
                                            cls_CmbArticulo.Descripcion = row.Cells[2].Value.ToString();
                                            cls_CmbArticulo.Precio = row.Cells[3].Value.ToString();
                                            list.Add(cls_CmbArticulo);
                                        }
                                    }
                                }
                            }
                            frm.ListarArticulos(list);
                            frm.ConfigEditar();
                            frm.Show();
                        }
                        catch (Exception EX)
                        {
                        }
                    }
                    else
                    {
                        //Obtener el id de la ventana seleccionada
                        int idWindows = Convert.ToInt32(dgCotizaciones.CurrentRow.Cells["IdWindows"].Value);
                        //Eliminar la ventana
                        bool res = NQuote.DeleteWindows(idWindows);
                        if (res)
                        {
                            loadWindows();
                        }
                        agregarAccesorioToolStripMenuItem_Click(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error." + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            { //Preguntar si desea eliminar la ventana
                DialogResult result = MessageBox.Show("¿Desea eliminar la ventana n° " + dgCotizaciones.CurrentRow.Cells["IdWindows"].Value.ToString() + "?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    //Obtener el id de la ventana seleccionada
                    int idWindows = Convert.ToInt32(dgCotizaciones.CurrentRow.Cells["IdWindows"].Value);
                    //Eliminar la ventana
                    bool res = NQuote.DeleteWindows(idWindows);
                    if (res)
                    {
                        MessageBox.Show("Ventana Eliminada");
                        loadWindows();
                    }
                    else
                    {
                        MessageBox.Show("Error al Eliminar la Ventana");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error." + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }

        public void btnApply_Click(object sender, EventArgs e)
        {
            try {
                if (int.Parse(txtManoObra.Text) == 0 && int.Parse(txtDescuento.Text) == 0) {
                    MessageBox.Show("Por favor, verifique que haya ingresado bien todos los datos.\nDebe ingresar la mano de obra, o el descuento.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                //Validar si txtManoObrea y txtDescuento son numeros
                if (decimal.TryParse(txtManoObra.Text, out decimal manoObra) && decimal.TryParse(txtDescuento.Text, out decimal descuento))
                {

                    SubTotal = 0;
                    IVA = 0;
                    Discount = 0;
                    Labour = 0;
                    Total = 0;
                    Descuento = 0;
                    ManoObra = 0;

                    Discount = descuento / 100;
                    Labour = manoObra / 100;


                    loadWindows();
                }
                else
                {
                    MessageBox.Show("El descuento y la mano de obra deben ser números");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error. Por favor, verifique que haya ingresado bien todos los datos.\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }
        #endregion

        #region Eventos
        // Factor de conversión de metros a píxeles (ajústalo según tu necesidad)
        private int MetrosAPixeles = 80; // Ajusta esto según sea necesario

        private void dgCotizaciones_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                var cellValue = dgCotizaciones.Rows[e.RowIndex].Cells[1].Value;

                if (cellValue != null && !string.IsNullOrEmpty(cellValue.ToString()))
                {
                    string rutaRelativa = cellValue.ToString();

                    Console.WriteLine($"Ruta relativa: {rutaRelativa}");

                    for (int i = 0; i < dgCotizaciones.Rows[e.RowIndex].Cells.Count; i++)
                    {
                        var cellVal = dgCotizaciones.Rows[e.RowIndex].Cells[i].Value;
                        Console.WriteLine($"Celda {i}: {cellVal?.ToString() ?? "null"}");
                    }

                    string directorioDeTrabajo = Directory.GetCurrentDirectory();
                    Console.WriteLine($"Directorio de trabajo: {directorioDeTrabajo}");

                    string rutaAbsoluta;

                    bool esExclusivo = rutaRelativa.StartsWith("EXCLUSIVO:");
                    if (esExclusivo)
                    {
                        rutaRelativa = rutaRelativa.Replace("EXCLUSIVO:", "");
                    }

                    if (Path.IsPathRooted(rutaRelativa))
                    {
                        if (File.Exists(rutaRelativa))
                        {
                            rutaAbsoluta = rutaRelativa;
                        }
                        else
                        {
                            string fileName = Path.GetFileName(rutaRelativa);
                            rutaAbsoluta = Path.Combine(directorioDeTrabajo, "Images\\Windows", fileName);
                        }
                    }
                    else
                    {
                        rutaAbsoluta = Path.Combine(directorioDeTrabajo, rutaRelativa);
                        rutaAbsoluta = Path.GetFullPath(rutaAbsoluta);
                    }

                    Console.WriteLine($"Ruta absoluta: {rutaAbsoluta}");

                    if (File.Exists(rutaAbsoluta))
                    {
                        e.PaintBackground(e.CellBounds, true);

                        using (System.Drawing.Image img = System.Drawing.Image.FromFile(rutaAbsoluta))
                        {
                            int anchoImagen = img.Width;
                            int altoImagen = img.Height;

                            if (!esExclusivo)
                            {
                                decimal anchoEnMetros = ObtenerAncho(dgCotizaciones.Rows[e.RowIndex].Cells[2].Value.ToString());
                                decimal alturaEnMetros = ObtenerAlto(dgCotizaciones.Rows[e.RowIndex].Cells[2].Value.ToString());
                                int anchoVentana = (int)(anchoEnMetros * MetrosAPixeles);
                                int altoVentana = (int)(alturaEnMetros * MetrosAPixeles);

                                anchoImagen = anchoVentana;
                                altoImagen = altoVentana;
                            }
                            else
                            {
                                float ratio = Math.Min((float)e.CellBounds.Width / anchoImagen, (float)e.CellBounds.Height / altoImagen);
                                anchoImagen = (int)(anchoImagen * ratio);
                                altoImagen = (int)(altoImagen * ratio);
                            }

                            // Asegúrate de que anchoImagen y altoImagen no sean 0
                            if (anchoImagen == 0) anchoImagen = 200;//e.CellBounds.Width;
                            if (altoImagen == 0) altoImagen = 200;//e.CellBounds.Height;

                            Console.WriteLine($"Ancho imagen: {anchoImagen}, Alto imagen: {altoImagen}");

                            int x = e.CellBounds.Left + (e.CellBounds.Width - anchoImagen) / 2;
                            int y = e.CellBounds.Top + (e.CellBounds.Height - altoImagen) / 2;

                            e.Graphics.DrawImage(img, new System.Drawing.Rectangle(x, y, anchoImagen, altoImagen));
                        }

                        e.Handled = true;
                    }
                }
            }
        }




        private void cbOpcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbOpcion.SelectedIndex == 0)
            {
                txtConditional1.Text = "1.50% por concepto de adelanto para inicio de producción e instalación, y 50% contra entrega del proyecto.";
            }
            if (cbOpcion.SelectedIndex == 1)
            {
                txtConditional1.Text = "1.50% por concepto de adelanto para inicio de producción e instalación, un 25% contra avance del proyecto y un 25% al finalizar el proyecto.";
            }
            if (cbOpcion.SelectedIndex == 2)
            {
                txtConditional1.Text = "1.50% por concepto de adelanto para inicio de producción e instalación, un 20% contra primer avance del proyecto, un 20% contra segundo avance del proyecto y un 10% al finalizar el proyecto.";
            }
        }

        private void txtidClient_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnBuscar_Click(sender, e);
            }
        }

        private void frmQuote_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmDashUser frm = frmDashUser.Instance;
            if (EventClose)
            {
                frm.WindowState = FormWindowState.Normal;
                frm.Show();
                frm.BringToFront();      
            }

        }

        private void frmQuote_Load(object sender, EventArgs e)
        {
            //Hacer que el Formulario se adapte a la pantalla
            this.Location = new System.Drawing.Point(0, 0);
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;

        }

        private void agregarAccesorioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ClsWindows.IDQuote = Convert.ToInt32(txtidQuote.Text);
                frmListaAcesorios frm = new frmListaAcesorios();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error." + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }

        private void btnExclusivo_Click(object sender, EventArgs e)
        {
            try {
                ClsWindows.IDQuote = Convert.ToInt32(txtidQuote.Text);
                frmArticuloExclusivo frm = new frmArticuloExclusivo();
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error. " + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }


        }

        private void btnSanBlast_Click(object sender, EventArgs e)
        {
            try {
                ClsWindows.IDQuote = Convert.ToInt32(txtidQuote.Text);
                frmCalcPriceSandBlasting frm = new frmCalcPriceSandBlasting();
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error. " + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }

        private void cbIva_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el Numero Antes del % en El comboBox
            string Iva = cbIva.SelectedItem.ToString();
            string IvaNumber = Iva.Substring(0, Iva.Length - 1);
            if (IvaNumber != "")
            {
                decimal IvaDecimal = Convert.ToDecimal(IvaNumber);
                IVA = SubTotal * (IvaDecimal / 100);
                Total = SubTotal + IVA;

                if (UserCache.Name == "VitroTaller")
                {
                    txtTotal.Text = "Precio Restringido";
                    txtIVA.Text = "Precio Restringido";
                }
                else
                {
                    txtTotal.Text = Total.ToString("c");
                    txtIVA.Text = IVA.ToString("c");
                }
            }
        }

        private void btnPrefabricado_Click(object sender, EventArgs e)
        {
            try {
                ClsWindows.IDQuote = Convert.ToInt32(txtidQuote.Text);
                frmPrefabricado frm = new frmPrefabricado();
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error. " + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }

        private void dgCotizaciones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Abre el HiperVinculo
            //System.Diagnostics.Process.Start("C:\\GlassWin\\Debug\\Medidas de Fabricacion");





        }

        private void btnViaticos_Click(object sender, EventArgs e)
        {
            try {
                frmTablaViaticos frm = new frmTablaViaticos();
                frm.CargarSubTotal(SubTotal);
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error. " + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }


        }

        #endregion

        #region Metodos
        private void CalcPrices()
        {
            decimal total = 0;
            if (Descuento != 0 || ManoObra != 0)
            {
                foreach (DataGridViewRow row in dgCotizaciones.Rows)
                {
                    // Calcular el Precio con la mano de obra y Descuento Luego asignarlo a la columna Price
                    decimal price = Convert.ToDecimal(row.Cells["Price"].Value);
                    decimal priceWithLabour = price + (price + ManoObra);
                    decimal priceWithDiscount = priceWithLabour - (price * Descuento);
                    decimal TotalPriceWindows = ((priceWithLabour + priceWithDiscount));
                    row.Cells["Price"].Value = TotalPriceWindows;
                    // Sumar el total de la columna Price
                    total += Convert.ToDecimal(row.Cells["Price"].Value);
                }

                if (UserCache.Name == "VitroTaller")
                {
                    txtSubtotal.Text = "Precio Restringido";
                    txtTotal.Text = "Precio Restringido";
                }
                else
                {
                    txtSubtotal.Text = total.ToString("c");
                    txtTotal.Text = Total.ToString("c");
                }
                btnApply_Click(null, null);
            }
            else
            {
                foreach (DataGridViewRow row in dgCotizaciones.Rows)
                {
                    // Calcular el Precio con la mano de obra y Descuento Luego asignarlo a la columna Price
                    decimal price = Convert.ToDecimal(row.Cells["Price"].Value);
                    decimal priceWithLabour = price + (price * Labour);
                    decimal priceWithDiscount = priceWithLabour - (priceWithLabour * Discount);
                    row.Cells["Price"].Value = priceWithDiscount;
                    // Sumar el total de la columna Price
                    total += Convert.ToDecimal(row.Cells["Price"].Value);
                }
                if (LimiteCredito != 0)
                {
                    if (total > LimiteCredito)
                    {
                        // Preguntar Si desea Agregar los Precios
                        DialogResult result = MessageBox.Show("El Total de la Cotizacion supera el Limite de Credito del Cliente, ¿Desea Continuar?", "Limite de Credito", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.No)
                        {
                            txtSubtotal.Text = "";
                            txtTotal.Text = "";
                            return;
                        }
                        else
                        {
                            SubTotal = total;
                            Total = total;
                        }
                    }
                }
                else
                {
                    SubTotal = total;
                    Total = total;
                }
                if (UserCache.Name == "VitroTaller")
                {
                    txtSubtotal.Text = "Precio Restringido";
                    txtTotal.Text = "Precio Restringido";
                }
                else
                {
                    txtSubtotal.Text = total.ToString("c");
                    txtTotal.Text = Total.ToString("c");
                }
            }

            // Asegurarse de que el IVA se calcule automáticamente con el subtotal actualizado
            cbIva_SelectedIndexChanged(this, EventArgs.Empty);
        }

        private bool ValidateFields()
        {
            if (txtidClient.Text == "")
            {
                MessageBox.Show("Debe ingresar el Cliente");
                return false;
            }
            if (txtProjetName.Text == "")
            {
                MessageBox.Show("Debe ingresar el Nombre del Proyecto");
                return false;
            }
            if (txtAdreesClient.Text == "")
            {
                MessageBox.Show("Debe ingresar la Direccion del Cliente");
                return false;
            }
            if (txtTelefono.Text == "")
            {
                MessageBox.Show("Debe ingresar el Telefono del Cliente");
                return false;
            }
            if (txtEmail.Text == "")
            {
                MessageBox.Show("Debe ingresar el Correo del Cliente");
                return false;
            }
            if (txtManoObra.Text == "")
            {
                MessageBox.Show("Debe ingresar la Mano de Obra");
                return false;
            }
            if (txtDescuento.Text == "")
            {
                MessageBox.Show("Debe ingresar el Descuento");
                return false;
            }
            if (txtAddress.Text == "")
            {
                MessageBox.Show("Debe ingresar la Direccion del Proyecto");
                return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //Abre el HiperVinculo
                System.Diagnostics.Process.Start("C:\\GlassWin\\Debug\\Medidas de Fabricacion");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error. " + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void SendQuoteforWhathsaap()
        {
            //Preguntar si desea enviar la cotizacion por whatsapp
            DialogResult result = MessageBox.Show("¿Desea enviar la cotizacion por whatsapp?", "Enviar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string message = "Hola, le envío la cotizacion n° " + txtidQuote.Text;
                string url = "https://api.whatsapp.com/send?phone=506" + txtTelefono.Text + "&text=" + message;
                System.Diagnostics.Process.Start(url);
            }

        }

        private void LimpiarCampos()
        {
            dgCotizaciones.DataSource = null;
            txtidClient.Text = "";
            txtProjetName.Text = "";
            txtAdreesClient.Text = "";
            txtTelefono.Text = "";
            txtEmail.Text = "";
            txtManoObra.Text = "0";
            txtDescuento.Text = "0";
            txtAddress.Text = "";
            txtSubtotal.Text = "";
            //txtIVA.Text = "";
            txtTotal.Text = "";
            loadIDQuote();
            
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
        #endregion

        #region Generacion de pdf
        public bool Generate()
        {
            string rutaArchivoPDF = "";
            #region Crear el documento
            Document document = new Document();
            // Obtener el directorio del escritorio y las carpetas necesarias
            string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string carpetaProformas = Path.Combine(escritorio, "Proformas");
            string carpetaNombre = Path.Combine(carpetaProformas, txtidClient.Text.Trim());
            string NameFile = "Cotizacion n° " + txtidQuote.Text + ".pdf";

            // Verificar si la carpeta "Proformas" existe, si no, crearla
            if (!Directory.Exists(carpetaProformas))
            {
                Directory.CreateDirectory(carpetaProformas);
            }

            // Verificar si la carpeta con el nombre existe, si no, crearla
            if (!Directory.Exists(carpetaNombre))
            {
                Directory.CreateDirectory(carpetaNombre);
            }

            // Crear la ruta completa del archivo PDF
            rutaArchivoPDF = Path.Combine(carpetaNombre, NameFile);


            // Crea un nuevo objeto PdfWriter para escribir el documento en un archivo
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(rutaArchivoPDF, FileMode.Create));

            // Asigna el objeto PdfWriter al documento
            document.Open();
            #endregion


            try
            {
                #region Encabezado
                // Crea una tabla con dos columnas
                PdfPTable Encabezado = new PdfPTable(2);
                Encabezado.WidthPercentage = 120;
                string rutaLogo = "";
                //Usuario de Nel
                if (CompanyCache.IdCompany == 205520679)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VidriosMartinez.png";
                    rutaLogo = ruta + Url;

                }
                //Usuario de Nel Fin
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
                iTextSharp.text.Font textFont2 = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 7, iTextSharp.text.Font.NORMAL, BaseColor.GRAY);
                // Agrega los textos a la segunda celda
                PdfPCell textCell = new PdfPCell();
                textCell.Border = PdfPCell.NO_BORDER;

                // Alinea el contenido de la celda al centro
                textCell.HorizontalAlignment = Element.ALIGN_RIGHT;

                // Agrega el párrafo y los chunks al documento
                Paragraph paragraph = new Paragraph();
                paragraph.Add(new Chunk(CompanyCache.Name, titleFont));
                paragraph.Add(Chunk.NEWLINE);// Salto de línea
                if (CompanyCache.IdCompany == 205150849)
                {
                    paragraph.Add(new Chunk("Cédula Jurídica : 3-101-897998", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Ubicados en: " + CompanyCache.Address, textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Teléfonos: " + CompanyCache.Phone, textFont));
                    paragraph.Add(Chunk.NEWLINE);
                }else if (CompanyCache.IdCompany == 31028013)
                {
                   
                    paragraph.Add(new Chunk("Cédula Jurídica :" + "3-102-801388", textFont));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("650 METROS NORESTE Y 350 METROS NOROESTE DE KFC, BOSQUES DON JOSE, NICOYA.", textFont2));
                    paragraph.Add(Chunk.NEWLINE);
                    paragraph.Add(new Chunk("Teléfonos: " + CompanyCache.Phone, textFont));
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

                // Añade la palabra "COTIZACIÓN" debajo de la tabla
                Paragraph cotizacionParagraph = new Paragraph("COTIZACIÓN", titleFont);
                cotizacionParagraph.Alignment = Element.ALIGN_LEFT;
                document.Add(cotizacionParagraph);
                document.Add(new Paragraph(" "));// Esto agrega un espacio en blanco en el documento

                // Agregar una línea de separación
                PdfPTable lineaTable = new PdfPTable(1);
                lineaTable.TotalWidth = 525f;
                lineaTable.LockedWidth = true;

                PdfPCell cellLinea = new PdfPCell(new Phrase(" "))
                {
                    BorderWidthTop = 1f, // Línea en la parte superior
                    BorderWidthBottom = 0f, // Sin borde en la parte inferior
                    BorderWidthLeft = 0f, // Sin borde en la parte izquierda
                    BorderWidthRight = 0f, // Sin borde en la parte derecha
                    FixedHeight = 10f, // Altura fija para la celda
                    HorizontalAlignment = Element.ALIGN_CENTER
                };
                lineaTable.AddCell(cellLinea);

                document.Add(lineaTable);

                document.Add(new Paragraph(" "));
                #endregion


                #region Tabla de Informacion 
                // Crear una tabla para los datos del proyecto y la información del cliente
                PdfPTable datosTable = new PdfPTable(2);
                datosTable.TotalWidth = 500f; // Ajusta el ancho total según tus necesidades
                datosTable.LockedWidth = true;

                // Celda 1: Datos del Proyecto
                /*PdfPCell cellDatosProyecto = new PdfPCell(new Phrase("Datos del Proyecto", FontFactory.GetFont(FontFactory.HELVETICA, 16, BaseColor.WHITE)))
                {
                    BackgroundColor = new BaseColor(70, 130, 180),
                    Border = PdfPCell.NO_BORDER, // Sin borde
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                };
                datosTable.AddCell(cellDatosProyecto);

                // Celda 2: Información del Cliente
                PdfPCell cellDatosCliente = new PdfPCell(new Phrase("Información del Cliente", FontFactory.GetFont(FontFactory.HELVETICA, 16, BaseColor.WHITE)))
                {
                    BackgroundColor = new BaseColor(70, 130, 180),
                    Border = PdfPCell.NO_BORDER, // Sin borde
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellDatosCliente);*/

                // Celda 3: Etiqueta "Cotización"
                PdfPCell cellEtiquetaCotizacion = new PdfPCell(new Phrase("Cotización: " + txtidQuote.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    Border = PdfPCell.NO_BORDER, // Sin borde
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaCotizacion);

                // Celda 4: Etiqueta "Cliente"
                PdfPCell cellEtiquetaCliente = new PdfPCell(new Phrase("Cliente: " + txtidClient.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    Border = PdfPCell.NO_BORDER, // Sin borde
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaCliente);

                // Celda 5: Etiqueta "Forma Pago"
                PdfPCell cellEtiquetaFormaPago = new PdfPCell(new Phrase("Fecha: " + txtDate.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    Border = PdfPCell.NO_BORDER, // Sin borde
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaFormaPago);

                // Celda 6: Etiqueta "Teléfono"
                PdfPCell cellEtiquetaTelefono = new PdfPCell(new Phrase("Teléfono: " + txtTelefono.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    Border = PdfPCell.NO_BORDER, // Sin borde
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaTelefono);

                // Celda 7: Etiqueta "Dirección"
                PdfPCell cellEtiquetaDireccion = new PdfPCell(new Phrase("Proyecto: " + txtProjetName.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    Border = PdfPCell.NO_BORDER, // Sin borde
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaDireccion);

                // Celda 8: Etiqueta "Correo"
                PdfPCell cellEtiquetaCorreo = new PdfPCell(new Phrase("Correo: " + txtEmail.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    Border = PdfPCell.NO_BORDER, // Sin borde
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaCorreo);

                document.Add(datosTable);
                document.Add(new Paragraph(" "));


                #endregion

                #region Tabla de Productos
                // Crear una tabla con el número de columnas de tu DataGridView
                PdfPTable tabla = new PdfPTable(dgCotizaciones.Columns.Count);
                tabla.TotalWidth = 500f; // Ajusta el ancho total según tus necesidades     
                tabla.LockedWidth = true;
                float[] tablaW = { 0f, 190f, 140f, 50f }; // Ancho de las columnas
                tabla.SetWidths(tablaW);

                // Agregar encabezados de columna
                for (int i = 0; i < dgCotizaciones.Columns.Count; i++)
                {
                    PdfPCell celda = new PdfPCell(new Phrase(dgCotizaciones.Columns[i].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 13, BaseColor.WHITE))); // Reducimos el tamaño a 13 puntos
                    celda.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda.BackgroundColor = new BaseColor(70, 130, 180);
                    tabla.AddCell(celda);
                }

                // Agregar filas y celdas de datos con imágenes
                for (int i = 0; i < dgCotizaciones.Rows.Count; i++)
                {
                    for (int j = 0; j < dgCotizaciones.Columns.Count; j++)
                    {
                        if (dgCotizaciones[j, i].Value != null)
                        {
                            PdfPCell cell = null;

                            if (dgCotizaciones.Columns[j].HeaderText == "URL")
                            {
                                string rutaImagen = dgCotizaciones[j, i].Value.ToString();
                                if (!string.IsNullOrEmpty(rutaImagen) && File.Exists(rutaImagen))
                                {
                                    // Obtener dimensiones en metros y convertirlas a píxeles
                                    decimal anchoEnMetros = ObtenerAncho(dgCotizaciones.Rows[i].Cells[2].Value.ToString());
                                    decimal alturaEnMetros = ObtenerAlto(dgCotizaciones.Rows[i].Cells[2].Value.ToString());
                                  
                                    int anchoVentana = (int)(anchoEnMetros * MetrosAPixeles);
                                    int altoVentana = (int)(alturaEnMetros * MetrosAPixeles);

                                    if (anchoVentana == 0) anchoVentana = 150;//e.CellBounds.Width;
                                    if (altoVentana == 0) altoVentana = 100;//e.CellBounds.Height;

                                    // Mostrar dimensiones calculadas para depuración
                                    Console.WriteLine($"Ancho ventana en píxeles: {anchoVentana}, Alto ventana en píxeles: {altoVentana}");

                                    // Cargar la imagen y ajustar su tamaño
                                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(rutaImagen);

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
                                if (dgCotizaciones.Columns[j].HeaderText == "Descripcion")
                                {
                                    // Para la columna "Descripción", alinea el texto a la izquierda
                                    cell = new PdfPCell(new Phrase(dgCotizaciones[j, i].Value.ToString(), FontFactory.GetFont(FontFactory.HELVETICA)));
                                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                }
                                else if (dgCotizaciones.Columns[j].HeaderText == "Precio")
                                {
                                    // Para la columna "Precio", alinea el texto a la izquierda y redondea a dos decimales
                                    decimal Prices = Convert.ToDecimal(dgCotizaciones[j, i].Value);
                                    Prices = Math.Round(Prices, 2);
                                    cell = new PdfPCell(new Phrase("¢" + Prices.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 10)));
                                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                }
                                else
                                {
                                    cell = new PdfPCell(new Phrase(dgCotizaciones[j, i].Value.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 10)));
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

                document.Add(new Paragraph(" ")); // Esto agrega un espacio en blanco en el documento

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
                cellp.Phrase = new Phrase("SubTotal :");
                cellp.HorizontalAlignment = Element.ALIGN_CENTER;
                tablePrecio.AddCell(cellp);
                cellp.Phrase = new Phrase("IVA");
                tablePrecio.AddCell(cellp);

                cellp.Phrase = new Phrase("Total :");
                tablePrecio.AddCell(cellp);
                cellp.Phrase = new Phrase("¢" + txtSubtotal.Text);
                tablePrecio.AddCell(cellp);

                cellp.Phrase = new Phrase("¢" + txtIVA.Text);
                tablePrecio.AddCell(cellp);
                cellp.Phrase = new Phrase("¢" + txtTotal.Text);
                tablePrecio.AddCell(cellp);

                // Agregar la tabla al documento
                document.Add(tablePrecio);
                document.Add(new Paragraph(" "));// Esto agrega un espacio en blanco en el documento

                #endregion

                #region Condiciones, Notas y Cuentas

                //Agregar las Condiciones desde el txtConditional1 hasta el txtConditional7 en una tabla
                PdfPTable tableCondiciones = new PdfPTable(1); // 1 columna
                tableCondiciones.WidthPercentage = 97;
                tableCondiciones.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell cellCondiciones = new PdfPCell(); ;
                cellCondiciones.HorizontalAlignment = Element.ALIGN_LEFT;
                cellCondiciones.VerticalAlignment = Element.ALIGN_MIDDLE;
                Paragraph paragraphCondiciones = new Paragraph();
                paragraphCondiciones.Add(new Chunk("Condiciones", titleFont));
                paragraphCondiciones.Add(Chunk.NEWLINE);// Salto de línea
                paragraphCondiciones.Add(new Chunk(txtConditional1.Text, textFont));
                paragraphCondiciones.Add(Chunk.NEWLINE);
                paragraphCondiciones.Add(new Chunk(txtConditional2.Text, textFont));
                paragraphCondiciones.Add(Chunk.NEWLINE);
                paragraphCondiciones.Add(new Chunk(txtConditional3.Text, textFont));
                paragraphCondiciones.Add(Chunk.NEWLINE);
                paragraphCondiciones.Add(new Chunk(txtConditional4.Text, textFont));
                paragraphCondiciones.Add(Chunk.NEWLINE);
                paragraphCondiciones.Add(new Chunk(txtConditional5.Text, textFont));
                paragraphCondiciones.Add(Chunk.NEWLINE);
                paragraphCondiciones.Add(new Chunk(txtConditional6.Text, textFont));
                paragraphCondiciones.Add(Chunk.NEWLINE);
                paragraphCondiciones.Add(new Chunk(txtConditional7.Text, textFont));
                paragraphCondiciones.Add(Chunk.NEWLINE);
                paragraphCondiciones.Add(new Chunk(txtConditional8.Text, textFont));
                paragraphCondiciones.Add(Chunk.NEWLINE);
                paragraphCondiciones.Add(new Chunk(txtConditional9.Text, textFont));
                paragraphCondiciones.Add(Chunk.NEWLINE);
                paragraphCondiciones.Add(new Chunk(txtConditional10.Text, textFont));
                paragraphCondiciones.Add(Chunk.NEWLINE);
                cellCondiciones.AddElement(paragraphCondiciones);
                tableCondiciones.AddCell(cellCondiciones);
                document.Add(tableCondiciones);
                document.Add(new Paragraph(" "));


                if (CompanyCache.IdCompany == 205150849)
                {
                    Paragraph NotasParagraph = new Paragraph("NOTAS", FontFactory.GetFont(FontFactory.HELVETICA, 12, 1, BaseColor.GRAY));
                    NotasParagraph.Alignment = Element.ALIGN_CENTER;
                    document.Add(NotasParagraph);

                    Paragraph Nota1Paragraph = new Paragraph("•1.Utilizamos toda nuestra experiencia y conocimiento en beneficio de la obra.", FontFactory.GetFont(FontFactory.HELVETICA, 10, 1, BaseColor.BLACK));
                    Nota1Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Nota1Paragraph);

                    Paragraph Nota2Paragraph = new Paragraph("•2.Instalaciones Maky brinda garantía de un año por defecto de instalación, y garantía de un año en accesorios por defecto de fábrica.", FontFactory.GetFont(FontFactory.HELVETICA, 10, 1, BaseColor.BLACK));
                    Nota2Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Nota2Paragraph);

                    Paragraph Nota3Paragraph = new Paragraph("•3.Se requiere que, previo al inicio del trabajo, todo el perímetro de la ventana esté listo para verificar medidas y mandar a producción.", FontFactory.GetFont(FontFactory.HELVETICA, 10, 1, BaseColor.BLACK));
                    Nota3Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Nota3Paragraph);

                    Paragraph Nota4Paragraph = new Paragraph("•4.Todos los materiales que utiliza Instalaciones Maky son de alta calidad (EXTRALUM).", FontFactory.GetFont(FontFactory.HELVETICA, 10, 1, BaseColor.BLACK));
                    Nota4Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Nota4Paragraph);

                    Paragraph Nota5Paragraph = new Paragraph("•5.El precio corresponde a materiales, fabricación, transporte e instalación, según medidas tomadas en la obra o suministradas por el cliente. Cualquier otro costo adicional será cotizado por aparte.", FontFactory.GetFont(FontFactory.HELVETICA, 10, 1, BaseColor.BLACK));
                    Nota5Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Nota5Paragraph);

                    document.Add(new Paragraph(" "));// Esto agrega un espacio en blanco en el documento

                    Paragraph CuentasParagraph = new Paragraph("CUENTAS", FontFactory.GetFont(FontFactory.HELVETICA, 12, 1, BaseColor.GRAY));
                    CuentasParagraph.Alignment = Element.ALIGN_CENTER;
                    document.Add(CuentasParagraph);
                    document.Add(new Paragraph(" "));

                    // Crear tabla con 5 columnas y ajustar porcentaje de ancho
                    PdfPTable tablaCuentas = new PdfPTable(2);
                    tablaCuentas.WidthPercentage = 100;

                    // Agregar encabezados de las columnas
                    tablaCuentas.AddCell(new PdfPCell(new Phrase("Colones", FontFactory.GetFont(FontFactory.HELVETICA, 12, 1, BaseColor.GRAY))));
                    tablaCuentas.AddCell(new PdfPCell(new Phrase("Dolares", FontFactory.GetFont(FontFactory.HELVETICA, 12, 1, BaseColor.GRAY))));

                    // Agregar fila con información de la cuenta
                    tablaCuentas.AddCell(new PdfPCell(new Phrase("BANCO: BCR", textFont)));
                    tablaCuentas.AddCell(new PdfPCell(new Phrase("BANCO: BCR", textFont)));
                    tablaCuentas.AddCell(new PdfPCell(new Phrase("Cuenta IBAN: CR09015202001375505431", textFont)));
                    tablaCuentas.AddCell(new PdfPCell(new Phrase("Cuenta IBAN: CR75015202001375505601", textFont)));
                    tablaCuentas.AddCell(new PdfPCell(new Phrase("Nombre: Vidrios e Instalaciones Maky S.A", textFont)));
                    tablaCuentas.AddCell(new PdfPCell(new Phrase("Nombre: Vidrios e Instalaciones Maky S.A", textFont)));
                    tablaCuentas.AddCell(new PdfPCell(new Phrase("Num.Identificacion: 3-101-897-998", textFont)));
                    tablaCuentas.AddCell(new PdfPCell(new Phrase("Num.Identificacion: 3-101-897-998", textFont)));


                    // Agregar tabla al documento
                    document.Add(tablaCuentas);
                }
                if (CompanyCache.IdCompany == 111560456)
                {
                    Paragraph NotasParagraph = new Paragraph("NOTAS", FontFactory.GetFont(FontFactory.HELVETICA, 12, 1, BaseColor.GRAY));
                    NotasParagraph.Alignment = Element.ALIGN_CENTER;
                    document.Add(NotasParagraph);

                    Paragraph Nota1Paragraph = new Paragraph("•Nuestro equipo técnico es guiado por compañeros certificados por el Instituto Nacional de Aprendizaje I.N.A. GARANTIZANDO LA EXCELENTE INSTALACION DE LOS PRODUCTOS.", FontFactory.GetFont(FontFactory.HELVETICA, 10, 1, BaseColor.BLACK));
                    Nota1Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Nota1Paragraph);

                    Paragraph Nota2Paragraph = new Paragraph("•Todo incluye transporte e instalación en la zona.", FontFactory.GetFont(FontFactory.HELVETICA, 10, 1, BaseColor.BLACK));
                    Nota2Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Nota2Paragraph);

                    Paragraph Nota3Paragraph = new Paragraph("•Para este tipo de proyectos les ofrecemos una garantía de 12 meses en lo que se trate por daño de fábrica, mala instalación, no cubre por fenómenos sobre naturales.", FontFactory.GetFont(FontFactory.HELVETICA, 10, 1, BaseColor.BLACK));
                    Nota3Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Nota3Paragraph);

                    document.Add(new Paragraph(" "));// Esto agrega un espacio en blanco en el documento

                    Paragraph CuentasParagraph = new Paragraph("CUENTAS", FontFactory.GetFont(FontFactory.HELVETICA, 12, 1, BaseColor.GRAY));
                    CuentasParagraph.Alignment = Element.ALIGN_CENTER;
                    document.Add(CuentasParagraph);

                    Paragraph Cuenta1Paragraph = new Paragraph("•Número de Cuenta CC: 200 01 114 018966 5", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    Cuenta1Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Cuenta1Paragraph);

                    Paragraph Cuenta2Paragraph = new Paragraph("•Número de Cuenta IBAN : CR360 15111420010189660 ", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    Cuenta2Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Cuenta2Paragraph);

                    Paragraph DetalleParagraph = new Paragraph("•Detalle: # de Cotización y Nombre el Cliente", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    DetalleParagraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(DetalleParagraph);

                    Paragraph Detalle2Paragraph = new Paragraph("•Favor enviar comprobante de pago vía correo", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    Detalle2Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Detalle2Paragraph);
                }


                //J123
                if (CompanyCache.IdCompany == 1230123)
                {
                    Paragraph NotasParagraph = new Paragraph("NOTAS", FontFactory.GetFont(FontFactory.HELVETICA, 12, 1, BaseColor.GRAY));
                    NotasParagraph.Alignment = Element.ALIGN_CENTER;
                    document.Add(NotasParagraph);

                    Paragraph Nota1Paragraph = new Paragraph("•1.Utilizamos toda nuestra experiencia y conocimiento en beneficio de la obra.", FontFactory.GetFont(FontFactory.HELVETICA, 10, 1, BaseColor.BLACK));
                    Nota1Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Nota1Paragraph);

                    Paragraph Nota2Paragraph = new Paragraph("•2.Instalaciones Maky brinda garantía de un año por defecto de instalación, y garantía de un año en accesorios por defecto de fábrica.", FontFactory.GetFont(FontFactory.HELVETICA, 10, 1, BaseColor.BLACK));
                    Nota2Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Nota2Paragraph);

                    Paragraph Nota3Paragraph = new Paragraph("•3.Se requiere que, previo al inicio del trabajo, todo el perímetro de la ventana esté listo para verificar medidas y mandar a producción.", FontFactory.GetFont(FontFactory.HELVETICA, 10, 1, BaseColor.BLACK));
                    Nota3Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Nota3Paragraph);

                    Paragraph Nota4Paragraph = new Paragraph("•4.Todos los materiales que utiliza Instalaciones Maky son de alta calidad (EXTRALUM).", FontFactory.GetFont(FontFactory.HELVETICA, 10, 1, BaseColor.BLACK));
                    Nota4Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Nota4Paragraph);

                    Paragraph Nota5Paragraph = new Paragraph("•5.El precio corresponde a materiales, fabricación, transporte e instalación, según medidas tomadas en la obra o suministradas por el cliente. Cualquier otro costo adicional será cotizado por aparte.", FontFactory.GetFont(FontFactory.HELVETICA, 10, 1, BaseColor.BLACK));
                    Nota5Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Nota5Paragraph);

                    document.Add(new Paragraph(" "));// Esto agrega un espacio en blanco en el documento

                    Paragraph CuentasParagraph = new Paragraph("CUENTAS", FontFactory.GetFont(FontFactory.HELVETICA, 12, 1, BaseColor.GRAY));
                    CuentasParagraph.Alignment = Element.ALIGN_CENTER;
                    document.Add(CuentasParagraph);

                    Paragraph Cuenta1Paragraph = new Paragraph("• Cuenta IBAN colones XXXXXXXXXXXXXXXXXXXX", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    Cuenta1Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Cuenta1Paragraph);

                    Paragraph Cuenta2Paragraph = new Paragraph("• Cuenta IBAN dólares XXXXXXXXXXXXXXXXXXXXX \r\n\r\n", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    Cuenta2Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Cuenta2Paragraph);
                }


                if (CompanyCache.IdCompany == 31025820)
                {
                    Paragraph CuentasParagraph = new Paragraph("CUENTAS", FontFactory.GetFont(FontFactory.HELVETICA, 12, 1, BaseColor.GRAY));
                    CuentasParagraph.Alignment = Element.ALIGN_CENTER;
                    document.Add(CuentasParagraph);

                    Paragraph Cuenta1Paragraph = new Paragraph("•BAC 914064795 C/CLIENTE 1020000914064798 BNCR REINIER ARTURO BRENES CALVO", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    Cuenta1Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Cuenta1Paragraph);

                    Paragraph Cuenta2Paragraph = new Paragraph("•BNCR 200-01-033-086908-9 C/CLIENTE 15103320010869082 REINER BRENES CALVO,", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    Cuenta2Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Cuenta2Paragraph);

                    Paragraph Cuenta3Paragraph = new Paragraph("•BAC C/IBAN CR50010200009140647958", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    Cuenta3Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Cuenta3Paragraph);

                    Paragraph Cuenta4Paragraph = new Paragraph("•BNCR C/IBAN CR62015103320010869082", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    Cuenta4Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Cuenta4Paragraph);

                    Paragraph Cuenta5Paragraph = new Paragraph("SINPE", FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.GRAY));
                    Cuenta5Paragraph.Alignment = Element.ALIGN_CENTER;
                    document.Add(Cuenta5Paragraph);

                    Paragraph Cuenta6Paragraph = new Paragraph("•REINIER ARTURO BRENES CALVO / CEDULA 2-0628-0081", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    Cuenta6Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Cuenta6Paragraph);

                    Paragraph Cuenta7Paragraph = new Paragraph("•SINPE 8877-1193", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    Cuenta7Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Cuenta7Paragraph);

                    document.Add(new Paragraph(" "));// Esto agrega un espacio en blanco en el documento

                    Paragraph Cuenta8Paragraph = new Paragraph("•Contamos con servicio y repuestos para todo equipo suministrado. Somos Distribuidores autorizados de EXTRALUM Cta. # 003914 ESPEJOS DEL MUNDO # 2280", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    Cuenta8Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Cuenta8Paragraph);

                    Paragraph Cuenta9Paragraph = new Paragraph("•Nombre de la Persona Responsable…  REINIER BRENES CALVO 8877-1193", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    Cuenta9Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Cuenta9Paragraph);

                    document.Add(new Paragraph(" "));// Esto agrega un espacio en blanco en el documento

                    // Agregar una imagen al documento
                    string imagePath = Path.GetDirectoryName(Application.ExecutablePath) + "\\Images\\Firma\\Firma Reiner.jpeg";
                    Image img = Image.GetInstance(imagePath);
                    img.ScaleToFit(200, 200); // Ajustar el tamaño de la imagen
                    img.Alignment = Element.ALIGN_CENTER; // Alinear la imagen al centro
                    document.Add(img); // Agregar la imagen al documento

                }
                if (CompanyCache.IdCompany == 25550555)
                {
                    Paragraph NotasParagraph = new Paragraph("NOTAS", FontFactory.GetFont(FontFactory.HELVETICA, 12, 1, BaseColor.GRAY));
                    NotasParagraph.Alignment = Element.ALIGN_CENTER;
                    document.Add(NotasParagraph);

                    Paragraph Nota1Paragraph = new Paragraph("•1.Utilizamos toda nuestra experiencia y conocimiento en beneficio de la obra.", FontFactory.GetFont(FontFactory.HELVETICA, 10, 1, BaseColor.BLACK));
                    Nota1Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Nota1Paragraph);

                    Paragraph Nota2Paragraph = new Paragraph("•2.brinda garantía de un año por defecto de instalación, y garantía de un año en accesorios por defecto de fábrica.", FontFactory.GetFont(FontFactory.HELVETICA, 10, 1, BaseColor.BLACK));
                    Nota2Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Nota2Paragraph);

                    Paragraph Nota3Paragraph = new Paragraph("•3.Se requiere que, previo al inicio del trabajo, todo el perímetro de la ventana esté listo para verificar medidas y mandar a producción.", FontFactory.GetFont(FontFactory.HELVETICA, 10, 1, BaseColor.BLACK));
                    Nota3Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Nota3Paragraph);

                    Paragraph Nota4Paragraph = new Paragraph("•4.Todos los materiales que utiliza Instalaciones Maky son de alta calidad (EXTRALUM).", FontFactory.GetFont(FontFactory.HELVETICA, 10, 1, BaseColor.BLACK));
                    Nota4Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Nota4Paragraph);

                    Paragraph Nota5Paragraph = new Paragraph("•5.El precio corresponde a materiales, fabricación, transporte e instalación, según medidas tomadas en la obra o suministradas por el cliente. Cualquier otro costo adicional será cotizado por aparte.", FontFactory.GetFont(FontFactory.HELVETICA, 10, 1, BaseColor.BLACK));
                    Nota5Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Nota5Paragraph);

                    document.Add(new Paragraph(" "));// Esto agrega un espacio en blanco en el documento

                    Paragraph CuentasParagraph = new Paragraph("CUENTAS", FontFactory.GetFont(FontFactory.HELVETICA, 12, 1, BaseColor.GRAY));
                    CuentasParagraph.Alignment = Element.ALIGN_CENTER;
                    document.Add(CuentasParagraph);

                    Paragraph Cuenta1Paragraph = new Paragraph("• Cuenta IBAN colones CR66015202250000607041", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    Cuenta1Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Cuenta1Paragraph);

                    Paragraph Cuenta2Paragraph = new Paragraph("• Cuenta IBAN dólares CR29015202001242164021 \r\n\r\n", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    Cuenta2Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Cuenta2Paragraph);
                }
                if (CompanyCache.IdCompany == 112540885) {
                    Paragraph CuentasParagraph = new Paragraph("CUENTAS", FontFactory.GetFont(FontFactory.HELVETICA, 12, 1, BaseColor.GRAY));
                    CuentasParagraph.Alignment = Element.ALIGN_CENTER;
                    document.Add(CuentasParagraph);

                    Paragraph Cuenta1Paragraph = new Paragraph("• Cuenta IBAN colones CR36010200009449083184.", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    Cuenta1Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Cuenta1Paragraph);

                    Paragraph Cuenta2Paragraph = new Paragraph("• Cuenta BAC colones 944908318.", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    Cuenta2Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Cuenta2Paragraph);

                    Paragraph Cuenta3Paragraph = new Paragraph("• Número SINPE Móvil 83984523.  \r\n\r\n", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    Cuenta3Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Cuenta3Paragraph);

                }
                    #endregion

                    #region Cerrar el documento
                    // Cerrar el documento
                    document.Close();
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al Generar el PDF, Error: " + ex.Message);
                return false;

            }
            #endregion
        }

        #endregion
    }
}
