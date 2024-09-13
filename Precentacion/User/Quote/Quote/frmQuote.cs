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
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Image = iTextSharp.text.Image;
using System.Text.RegularExpressions;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net.Http;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.Globalization;


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
        decimal tasaCambio;
        bool dolar = false;
        bool colon = false;
        public decimal precioTotalEdit;
        public int idQuoteVerificacion;
        int resultadoGlobal;
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
           // VerificarIdQuote();

            /*if (Edit)
            {
                btnSistemas.Enabled = false;
                btnSanBlast.Enabled = false;
                btnPrefabricado.Enabled = false;
                btnExclusivo.Enabled = false;

            }*/

        }
        #endregion
        // Capa de presentación
        private void VerificarIdQuote()
        {
            //int idQuote = Convert.ToInt32(txtidQuote.Text); // Asumiendo que el ID está en un TextBox

            if (NQuote.ExisteIdQuote(Convert.ToInt32(txtidQuote.Text))) // Llamada al método de la capa de lógica de negocio
            {
                MessageBox.Show("El ID de la cotización existe en la tabla TotalDesglose.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("El ID de la cotización no existe en la tabla TotalDesglose.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void InitializeCustomButtons()
        {
            try 
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
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
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
            try
            {
                // Crear un lápiz con el color y grosor deseado
                using (Pen pen = new Pen(borderColor, borderWidth))
                {
                    // Dibujar solo el borde superior del formulario
                    e.Graphics.DrawLine(pen, 0, 0, this.ClientSize.Width, 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            try
            {
                int ID = NQuote.InsertQuoteAndGetLastID(Date, "", "", "", 0, 0, 0, 0, 0, 4);
                txtidQuote.Text = ID.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            
            }
           
        }
        private void loadDate()
        {
            txtDate.Text = Date.ToString("dd/MM/yyyy");
        }
        public void loadWindows()
        {
            try
            {
                DataTable dt = NQuote.LoadWindows(Convert.ToInt32(txtidQuote.Text));
                dgCotizaciones.DataSource = dt;
                dgCotizaciones.RowTemplate.Height = 250;
                dgCotizaciones.Columns[0].Width = 90;
                dgCotizaciones.Columns[1].Width = 300;
                dgCotizaciones.Columns[2].Width = 200;
                dgCotizaciones.Columns[3].Width = 115;
                if (UserCache.Name == "VitroTaller")
                {
                    // Ocultar la columna de Precio
                    dgCotizaciones.Columns[3].Visible = false;
                }

                // Cambiar el nombre de las columnas
                dgCotizaciones.Columns[2].HeaderText = "Descripción";
                dgCotizaciones.Columns[3].HeaderText = "Precio";

                // Calcular precios
                CalcPrices();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        public void LoadConditionals()
        {
            try
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
                    txtConditional1.Text = "1.Esta oferta incluye, materiales, mano de obra, transporte e instalación.";
                    txtConditional2.Text = "2.Oferta NO incluye desinstalación de buques existente.";
                    txtConditional3.Text = "3.Se requiere realizar la visita para tomar medidas rectificadas.";
                    txtConditional4.Text = "4.Forma de pago 50% adelanto 50% contra entrega.";
                    txtConditional5.Text = "5.Entrega de prefabricados de 8 a 20 días hábiles.";
                    txtConditional6.Text = "6.Por favor revisar cantidades, sistema y acabados.";
                    txtConditional7.Text = "7.Validez de cotización 8 días.";
                    txtConditional8.Text = "8.Precio puede variar según aumentos del mercado.";
                    txtConditional9.Text = "9.Garantía 1 año contra defectos propios del sistema(cierres, rodajes, empaques) NO se incluye garantía sobre rayones o quebraduras de vidrios.";
                    txtConditional10.Text = "10.Después del giro del 50%, no se aceptan devoluciones o cambios en el proyecto.";

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
                //Vitro Esparza
                if (CompanyCache.IdCompany == 3101623589 || CompanyCache.IdCompany == 3101623581)
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
                //Prefalum
                if (CompanyCache.IdCompany == 111111111)
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
                //Vidrios Albo
                if (CompanyCache.IdCompany == 3102154177)
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
                //Mercado del Vidrio
                if (CompanyCache.IdCompany == 3102879949)
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
                //MS Soluciones
                if (CompanyCache.IdCompany == 204260627)
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
                //Constru
                if (CompanyCache.IdCompany == 3101704274)
                {
                    cbOpcion.Visible = false;
                    txtConditional1.Text = "1.PRECIO SUJETO A CAMBIO SIN PRECIO AVISO.";
                    txtConditional2.Text = "2.FORMA DE PAGO: 70 % adelantado 30% contra entrega.";
                    txtConditional3.Text = "3.GARANTIA: 1 año.";
                    txtConditional4.Text = "4.TIEMPO DE ENTREGA: 17 días hábiles.";
                    txtConditional5.Text = "5.VALIDEZ DE OFERTA: 15 días natural.";
                   

                }
                //Usuario Prueba
                if (CompanyCache.IdCompany == 999999999)
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
                //Viteco
                if (CompanyCache.IdCompany == 503320196)
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
                //Vidriera Palmares
                if (CompanyCache.IdCompany == 222222222)
                {
                    cbOpcion.Visible = false;
                    txtConditional1.Text = "1.Esta oferta incluye, materiales, mano de obra, transporte e instalación";
                    txtConditional2.Text = "2.Oferta NO incluye desinstalación de buques existente";
                    txtConditional3.Text = "3.Se cotizan productos marca Extralum";
                    txtConditional4.Text = "4.Se requiere realizar la visita para tomar medidas rectificadas";
                    txtConditional5.Text = "5.Forma de pago 30% adelanto 70% contra entrega";
                    txtConditional6.Text = "6.Entrega de prefabricados de 8 a 20 días hábiles";
                    txtConditional7.Text = "7.Por favor revisar cantidades, sistema y acabados";
                    txtConditional8.Text = "8.Validez de cotización 8 días";
                    txtConditional9.Text = "9.Precio puede variar según aumentos del mercado";
                    txtConditional10.Text = "10.Garantía 1 año contra defectos propios del sistema(cierres, rodajes, empaques) NO se incluye garantía sobre rayones o quebraduras de vidrios.";

                }
                //Perfect Glass
                if (CompanyCache.IdCompany == 333333333)
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
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
           

        }
        public void LoadDataQuote()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
          
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
            catch (Exception ex)
            {
                MessageBox.Show("Mensaje: " + ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    // Mostrar un cuadro de diálogo personalizado para seleccionar el diseño de PDF
                    string[] options = { "Diseño 1 (con desglose de precios)", "Diseño 2 (precio total con portada de inicio)", "Diseño 3 (precio total)" };
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
                                case "Diseño 1 (con desglose de precios)":
                                    res = Generate(); // Usar el diseño habitual
                                    break;
                                case "Diseño 2 (precio total con portada de inicio)":
                                    // Mostrar el formulario para ingresar la descripción
                                    frmDescripcion descripcionForm = new frmDescripcion();
                                    if (descripcionForm.ShowDialog() == DialogResult.OK)
                                    {
                                        // Obtener la descripción del trabajo
                                        string descripcionTrabajo = descripcionForm.Descripcion;

                                        // Llamar a GeneratePDF2 con la descripción ingresada
                                        res = GeneratePDF2(descripcionTrabajo);
                                    }
                                    else
                                    {
                                        // Si el usuario cancela, no generar el PDF
                                        res = false;
                                    }
                                    break;
                                case "Diseño 3 (precio total)":
                                    res = GeneratePDF3(); // Llamar a la función para el tercer diseño de PDF
                                    break;
                            }
                        }
                        else
                        {
                            // Si el usuario cancela la selección, no generar el PDF
                            res = false;
                        }
                    }

                    if (res)
                    {
                        res = NQuote.EditQuote(Convert.ToInt32(txtidQuote.Text), Date, txtProjetName.Text, txtAddress.Text, "", Convert.ToDecimal(txtDescuento.Text), Convert.ToDecimal(txtManoObra.Text), IVA, SubTotal, Total, IdClient);
                        if (res)
                        {
                            QuoteSave = 1;
                            MessageBox.Show("Cotización Guardada", "Proforma Guardada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SendQuoteforWhathsaap();
                            LimpiarCampos();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error al Guardar la Cotización");
                    }
                }
            }
            catch (Exception ex)
            {
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

        /*public void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar si txtManoObra y txtDescuento son números
                if (decimal.TryParse(txtManoObra.Text, out decimal manoObra) && decimal.TryParse(txtDescuento.Text, out decimal descuento))
                {
                    // Reinicializar las variables de cálculo
                    SubTotal = 0;
                    IVA = 0;
                    Discount = descuento / 100; // Convertir a porcentaje
                    Labour = manoObra / 100;    // Convertir a porcentaje
                    Total = 0;

                    // Recargar los datos de la ventana y calcular precios
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
        }*/
        public void btnApply_Click(object sender, EventArgs e)
        {

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
        #endregion

        #region Eventos
        // Factor de conversión de metros a píxeles (ajústalo según tu necesidad)
        private int MetrosAPixeles = 80; // Ajusta esto según sea necesario

        private void dgCotizaciones_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
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

                        // Obtener la versión actual de la aplicación
                        System.Version versionActual = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                        string versionActualString = $"GlassWin{versionActual.Major}.{versionActual.Minor}.{versionActual.Build}.{versionActual.Revision}";

                        // Reemplazar la versión en la ruta con la versión actual
                        string rutaCorregida = ReemplazarVersionEnRuta(rutaRelativa, versionActualString);

                        // Imprimir ruta corregida para depuración
                        Console.WriteLine($"Ruta corregida: {rutaCorregida}");

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
                                if (anchoImagen == 0) anchoImagen = 200;
                                if (altoImagen == 0) altoImagen = 200;

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
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private string ReemplazarVersionEnRuta(string ruta, string versionActual)
        {
            // Suponiendo que la parte de la versión siempre está en el formato "GlassWinX.X.X.XX"
            string patron = @"GlassWin\d+\.\d+\.\d+\.\d+";
            return System.Text.RegularExpressions.Regex.Replace(ruta, patron, versionActual);
        }





        private void cbOpcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            int resultado = 0;
            if (NQuote.ExisteIdQuote(Convert.ToInt32(idQuoteVerificacion))) // Llamada al método de la capa de lógica de negocio
            {
                resultado = 1;
                resultadoGlobal = resultado;
            }

            decimal total = 0;

            if (resultado > 0)
            {
                btnSistemas.Enabled = false;
                btnViaticos.Enabled = false;
                btnSanBlast.Enabled = false;
                btnPrefabricado.Enabled = false;
                btnExclusivo.Enabled = false;

                // Usar el valor editado
                total = precioTotalEdit;

                // Aplicar mano de obra y descuento si existen
                decimal priceWithLabour = total + (total * Labour);
                decimal priceWithDiscount = priceWithLabour - (priceWithLabour * Discount);
                total = priceWithDiscount;

                // Actualizar la variable global
                //precioTotalEdit = total;
                SubTotal = total;

                // Mostrar en el campo de texto
                txtTotal.Text = total.ToString("c");
                txtSubtotal.Text = total.ToString("c");
            }
            else if (Descuento != 0 || ManoObra != 0)
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
                    else
                    {
                        SubTotal = total;
                        Total = total;
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
                else if (resultadoGlobal > 0)
                {
                    txtTotal.Text = Total.ToString("c");
                    txtIVA.Text = IVA.ToString("c");
                }
                else
                {
                    txtTotal.Text = Total.ToString("c");
                    txtIVA.Text = IVA.ToString("c");
                }

            }
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
                
                string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                string Url = "\\Medidas de Fabricacion";
                string rutaCarpeta = ruta + Url;
                //Abre el HiperVinculo
                System.Diagnostics.Process.Start(rutaCarpeta);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error. " + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void dgCotizaciones_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Muestra el mensaje de error en la consola o en un MessageBox
            Console.WriteLine($"Error en la celda [{e.RowIndex}, {e.ColumnIndex}]: {e.Exception.Message}");
            MessageBox.Show($"Error en la celda [{e.RowIndex}, {e.ColumnIndex}]: {e.Exception.Message}", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);

            // Opcionalmente, puedes evitar que el error se propague
            e.ThrowException = false;
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
                string Ancho = matchAncho.Groups[1].Value/*.Replace(",", ".")*/;
                decimal AnchoDecimal = 0;
                if (Ancho != "")
                {
                    //if (Ancho.Contains("."))
                    //{
                      //  AnchoDecimal = Convert.ToDecimal(Ancho.Replace(".", ","));
                    //}
                    //else
                    //{
                        AnchoDecimal = Convert.ToDecimal(Ancho);
                    //}
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
                string Alto = matchAlto.Groups[1].Value/*.Replace(",", ".")*/;
                decimal AltoDecimal = 0;
                if (Alto != "")
                {
                   // if (Alto.Contains("."))
                   // {
                      //  AltoDecimal = Convert.ToDecimal(Alto.Replace(".", ","));
                   // }
                   // else
                   // {
                        AltoDecimal = Convert.ToDecimal(Alto);
                    //}
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
        // Función para agregar viñetas, excluyendo una viñeta adicional al final
        private string AgregarViñetas(string texto)
        {
            // Dividir el texto en líneas y eliminar líneas vacías
            var lineas = texto.Split('\n').Where(linea => !string.IsNullOrWhiteSpace(linea)).ToList();

            // Agregar viñetas a cada línea
            for (int i = 0; i < lineas.Count; i++)
            {
                lineas[i] = "• " + lineas[i].Trim();
            }

            // Unir las líneas con saltos de línea
            return string.Join("\n", lineas);
        }
        public bool Generate()
        {
            #region Crear el documento
            string rutaArchivoPDF = "";
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
                else if (CompanyCache.IdCompany == 205150849)
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

                // Añadir celdas de datos en dos filas para asegurar que todas se muestren
                // Fila 1
                datosTable.AddCell(new PdfPCell(new Phrase("Cotización: " + txtidQuote.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12))) { Border = PdfPCell.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                datosTable.AddCell(new PdfPCell(new Phrase("Cliente: " + txtidClient.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12))) { Border = PdfPCell.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });

                // Fila 2
                datosTable.AddCell(new PdfPCell(new Phrase("Fecha: " + txtDate.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12))) { Border = PdfPCell.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                datosTable.AddCell(new PdfPCell(new Phrase("Teléfono: " + txtTelefono.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12))) { Border = PdfPCell.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });

                // Fila 3
                datosTable.AddCell(new PdfPCell(new Phrase("Proyecto: " + txtProjetName.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12))) { Border = PdfPCell.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                datosTable.AddCell(new PdfPCell(new Phrase("Correo: " + txtEmail.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12))) { Border = PdfPCell.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                // Verifica si txtAdreesClient.Text está vacío o es nulo
                string direccionCliente = string.IsNullOrWhiteSpace(txtAdreesClient.Text) ? "Sin dirección" : txtAdreesClient.Text;

                // Fila 4
                datosTable.AddCell(new PdfPCell(new Phrase("")) { Border = PdfPCell.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                datosTable.AddCell(new PdfPCell(new Phrase("Dirección Cliente: " + direccionCliente, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    Border = PdfPCell.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });




                // Añadir tabla al documento
                document.Add(datosTable);
                document.Add(new Paragraph(" "));

                #endregion

                // Cambiar el nombre de las columnas en el DataGridView
                //dgCotizaciones.Columns["URL"].HeaderText = "Diseño";
                dgCotizaciones.Columns["idWindows"].HeaderText = "ID Ventana";
                dgCotizaciones.Columns["URL"].HeaderText = "Diseño";
              


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
                for (int i = 0; i < dgCotizaciones.Rows.Count; i++)
                {
                    for (int j = 0; j < dgCotizaciones.Columns.Count; j++)
                    {
                        if (dgCotizaciones[j, i].Value != null)
                        {
                            PdfPCell cell = null;

                            if (dgCotizaciones.Columns[j].HeaderText == "Diseño")
                            {
                                string rutaImagen = dgCotizaciones[j, i].Value.ToString();
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
                                    decimal anchoEnMetros = ObtenerAncho(dgCotizaciones.Rows[i].Cells[2].Value.ToString());
                                    decimal alturaEnMetros = ObtenerAlto(dgCotizaciones.Rows[i].Cells[2].Value.ToString());

                                    int anchoVentana = (int)(anchoEnMetros * MetrosAPixeles);
                                    int altoVentana = (int)(alturaEnMetros * MetrosAPixeles);
                                    if (anchoVentana > 220)
                                    {
                                        anchoVentana = 200; // Reducir el ancho a 200 píxeles
                                    }

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
                                if (dgCotizaciones.Columns[j].HeaderText == "Descripcion")
                                {
                                    // Obtener el texto de la celda y agregar viñetas
                                    string textoConViñetas = AgregarViñetas(dgCotizaciones[j, i].Value.ToString());

                                    // Crear la celda con el texto modificado
                                    cell = new PdfPCell(new Phrase(textoConViñetas, FontFactory.GetFont(FontFactory.HELVETICA)));
                                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                }
                                else if (dgCotizaciones.Columns[j].HeaderText == "Precio")
                                {
                                    // Obtener el valor de la celda y convertirlo a decimal
                                    decimal precioColones = Convert.ToDecimal(dgCotizaciones[j, i].Value);

                                    // Si dolar es true, convertir el precio a dólares
                                    if (dolar)
                                    {
                                        // Asegúrate de que la tasa de cambio esté definida y sea mayor a cero
                                        if (tasaCambio > 0)
                                        {
                                            decimal precioDolares = precioColones / tasaCambio;
                                            precioDolares = Math.Round(precioDolares, 2);
                                            cell = new PdfPCell(new Phrase("$" + precioDolares.ToString("F2"), FontFactory.GetFont(FontFactory.HELVETICA, 10)));
                                        }
                                        else
                                        {
                                            // Manejo de error si tasaCambio no es válida
                                            cell = new PdfPCell(new Phrase("Error en tasa de cambio", FontFactory.GetFont(FontFactory.HELVETICA, 10)));
                                        }
                                    }
                                    else
                                    {
                                        // Mantener el precio en colones
                                        precioColones = Math.Round(precioColones, 2);
                                        cell = new PdfPCell(new Phrase("¢" + precioColones.ToString("F2"), FontFactory.GetFont(FontFactory.HELVETICA, 10)));
                                    }

                                    // Alinear el texto a la izquierda
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

                if (dolar)
                {
                    cellp.Phrase = new Phrase(txtSubtotal.Text);
                    tablePrecio.AddCell(cellp);
                    cellp.Phrase = new Phrase(txtIVA.Text);
                    tablePrecio.AddCell(cellp);
                    cellp.Phrase = new Phrase(txtTotal.Text);
                    tablePrecio.AddCell(cellp);
                }
                else {
                    cellp.Phrase = new Phrase("¢" + txtSubtotal.Text);
                    tablePrecio.AddCell(cellp);
                    cellp.Phrase = new Phrase("¢" + txtIVA.Text);
                    tablePrecio.AddCell(cellp);
                    cellp.Phrase = new Phrase("¢" + txtTotal.Text);
                    tablePrecio.AddCell(cellp);
                }
              

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
                if (CompanyCache.IdCompany == 112540885)
                {
                    Paragraph CuentasParagraph = new Paragraph("CUENTAS", FontFactory.GetFont(FontFactory.HELVETICA, 12, 1, BaseColor.GRAY));
                    CuentasParagraph.Alignment = Element.ALIGN_CENTER;
                    document.Add(CuentasParagraph);

                    // Crear tabla con 2 columnas para organizar texto e imágenes
                    PdfPTable table = new PdfPTable(1);
                    table.WidthPercentage = 100; // Ajustar el ancho de la tabla al 100% del documento

                    // Agregar las cuentas y los logos en una tabla
                    // Cuenta Banco Popular
                    string rutaBP = Path.GetDirectoryName(Application.ExecutablePath) + "\\Images\\Logos\\logo_bancopopular.png";
                    Image imgBancoPopular = Image.GetInstance(rutaBP);
                    imgBancoPopular.ScaleToFit(50f, 50f); // Ajustar el tamaño de la imagen
                    PdfPCell cellLogoBP = new PdfPCell(imgBancoPopular);
                    cellLogoBP.Border = PdfPCell.NO_BORDER;
                    cellLogoBP.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cellLogoBP);

                    PdfPCell cellTextBP = new PdfPCell(new Phrase("• Cuenta Banco Popular colones CR32016111120141093142.", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK)));
                    cellTextBP.Border = PdfPCell.NO_BORDER;
                    cellTextBP.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cellTextBP);

                    // Cuenta IBAN
                    string rutaIBAN = Path.GetDirectoryName(Application.ExecutablePath) + "\\Images\\Logos\\Logos-PR-BCR.png";
                    Image imgIBAN = Image.GetInstance(rutaIBAN);
                    imgIBAN.ScaleToFit(50f, 50f); // Ajustar el tamaño de la imagen
                    PdfPCell cellLogoIBAN = new PdfPCell(imgIBAN);
                    cellLogoIBAN.Border = PdfPCell.NO_BORDER;
                    cellLogoIBAN.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cellLogoIBAN);

                    PdfPCell cellTextIBAN = new PdfPCell(new Phrase("• Cuenta IBAN colones CR36010200009449083184.", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK)));
                    cellTextIBAN.Border = PdfPCell.NO_BORDER;
                    cellTextIBAN.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cellTextIBAN);

                    // Cuenta BAC
                    string rutaBAC = Path.GetDirectoryName(Application.ExecutablePath) + "\\Images\\Logos\\logo_bacredomatic.png";
                    Image imgBAC = Image.GetInstance(rutaBAC);
                    imgBAC.ScaleToFit(50f, 50f); // Ajustar el tamaño de la imagen
                    PdfPCell cellLogoBAC = new PdfPCell(imgBAC);
                    cellLogoBAC.Border = PdfPCell.NO_BORDER;
                    cellLogoBAC.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cellLogoBAC);

                    PdfPCell cellTextBAC = new PdfPCell(new Phrase("• Cuenta BAC colones 944908318.", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK)));
                    cellTextBAC.Border = PdfPCell.NO_BORDER;
                    cellTextBAC.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cellTextBAC);


                    // Cuenta BN
                    string rutaBN = Path.GetDirectoryName(Application.ExecutablePath) + "\\Images\\Logos\\bn.png";
                    Image imgBN = Image.GetInstance(rutaBN);
                    imgBN.ScaleToFit(50f, 50f); // Ajustar el tamaño de la imagen
                    PdfPCell cellLogoBN = new PdfPCell(imgBN);
                    cellLogoBN.Border = PdfPCell.NO_BORDER;
                    cellLogoBN.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cellLogoBN);

                    PdfPCell cellTextBN = new PdfPCell(new Phrase("• Cuenta BN  CR37015112720010160574.", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK)));
                    cellTextBN.Border = PdfPCell.NO_BORDER;
                    cellTextBN.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cellTextBN);


                    // SINPE Móvil
                    string rutaSinpe = Path.GetDirectoryName(Application.ExecutablePath) + "\\Images\\Logos\\sinpe-movil-2.png";
                    Image imgSINPE = Image.GetInstance(rutaSinpe);
                    imgSINPE.ScaleToFit(50f, 50f); // Ajustar el tamaño de la imagen
                    PdfPCell cellLogoSINPE = new PdfPCell(imgSINPE);
                    cellLogoSINPE.Border = PdfPCell.NO_BORDER;
                    cellLogoSINPE.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cellLogoSINPE);

                    PdfPCell cellTextSINPE = new PdfPCell(new Phrase("• Número SINPE Móvil 89089444.", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK)));
                    cellTextSINPE.Border = PdfPCell.NO_BORDER;
                    cellTextSINPE.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cellTextSINPE);

                    document.Add(table);
                }
                //Prefalum
                if (CompanyCache.IdCompany == 3102154177)
                {
                    Paragraph NotasParagraph = new Paragraph("NOTAS", FontFactory.GetFont(FontFactory.HELVETICA, 12, 1, BaseColor.GRAY));
                    NotasParagraph.Alignment = Element.ALIGN_CENTER;
                    document.Add(NotasParagraph);

                    Paragraph Nota1Paragraph = new Paragraph("•Precio mediante pago con efectivo, sinpe o transferencia.", FontFactory.GetFont(FontFactory.HELVETICA, 10, 1, BaseColor.BLACK));
                    Nota1Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Nota1Paragraph);

                    Paragraph Nota2Paragraph = new Paragraph("•Adelanto del 50% al contratar el proyecto y el restante al concluir la instalación.", FontFactory.GetFont(FontFactory.HELVETICA, 10, 1, BaseColor.BLACK));
                    Nota2Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Nota2Paragraph);

                    Paragraph Nota3Paragraph = new Paragraph("•Pago con tarjeta está sujeto a cambios.", FontFactory.GetFont(FontFactory.HELVETICA, 10, 1, BaseColor.BLACK));
                    Nota3Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Nota3Paragraph);

                    Paragraph Nota4Paragraph = new Paragraph("•Validez de la oferta: 15 días hábiles.", FontFactory.GetFont(FontFactory.HELVETICA, 10, 1, BaseColor.BLACK));
                    Nota4Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Nota4Paragraph);

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
                if (CompanyCache.IdCompany == 111111111)
                {
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
                //Mercado del vidrio
                if (CompanyCache.IdCompany == 3102879949)
                {
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
                //Vitro Esparza
                if (CompanyCache.IdCompany == 3101623589 || CompanyCache.IdCompany == 3101623581)
                {
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
                //Viteco
                if (CompanyCache.IdCompany == 503320196)
                {
                    Paragraph CuentasParagraph = new Paragraph("CUENTAS", FontFactory.GetFont(FontFactory.HELVETICA, 12, 1, BaseColor.GRAY));
                    CuentasParagraph.Alignment = Element.ALIGN_CENTER;
                    document.Add(CuentasParagraph);

                    Paragraph Cuenta1Paragraph = new Paragraph("• Banco Popular IBAN  CR40011610012010001560", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    Cuenta1Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Cuenta1Paragraph);

                    Paragraph Cuenta2Paragraph = new Paragraph("• Banco Nacional IBAN  CR54015112720010041831", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    Cuenta2Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Cuenta2Paragraph);

                    Paragraph Cuenta3Paragraph = new Paragraph("• Sinpe Móvil 8751-7492 (Eduardo Alberto Salazar Vega)\r\n\r\n", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    Cuenta3Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Cuenta3Paragraph);
                }

                //MS
                if (CompanyCache.IdCompany == 204260627)
                {
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
                //Usuario Prueba
                if (CompanyCache.IdCompany == 3101704274)
                {
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
                //Usuario Prueba
                if (CompanyCache.IdCompany == 999999999)
                {
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
                //Vidriera Palmares
                if (CompanyCache.IdCompany == 222222222)
                {
                    Paragraph CuentasParagraph = new Paragraph("CUENTAS", FontFactory.GetFont(FontFactory.HELVETICA, 12, 1, BaseColor.GRAY));
                    CuentasParagraph.Alignment = Element.ALIGN_CENTER;
                    document.Add(CuentasParagraph);
                    Paragraph CuentasParagraph2 = new Paragraph("Banco Nacional", FontFactory.GetFont(FontFactory.HELVETICA, 10, 1, BaseColor.GRAY));
                    CuentasParagraph2.Alignment = Element.ALIGN_CENTER;
                    document.Add(CuentasParagraph2);

                    Paragraph Cuenta1Paragraph = new Paragraph("• Cuenta IBAN colones CR84015101910010039940", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    Cuenta1Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Cuenta1Paragraph);

                    Paragraph Cuenta2Paragraph = new Paragraph("• Cuenta IBAN dólares CR20015101920020050861 \r\n\r\n", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    Cuenta2Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Cuenta2Paragraph);

                    Paragraph Cuenta3Paragraph = new Paragraph("• Sinpe Móvil a nombre de Vidriera Palmares S.A 87091108 \r\n\r\n", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    Cuenta3Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Cuenta3Paragraph);
                }
                if (CompanyCache.IdCompany == 333333333)
                {
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

        #region Generacion de pdf DISEÑO 2
        private bool GeneratePDF2(string descripcionTrabajo)
        {
            #region Crear el documento
            string rutaArchivoPDF = "";
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
                //Vitro esparza
                if (CompanyCache.IdCompany == 3101623589 || CompanyCache.IdCompany == 3101623581)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\vitroEsparza.png";
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
                iTextSharp.text.Font textFont2 = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 10, iTextSharp.text.Font.NORMAL, BaseColor.GRAY);
                // Crea una fuente Calibri con un tamaño de 11 puntos y en negrita
                BaseFont calibriBaseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\calibri.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                iTextSharp.text.Font cotizacionFont = new iTextSharp.text.Font(calibriBaseFont, 13, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

               
                iTextSharp.text.Font calibrriFuente = new iTextSharp.text.Font(calibriBaseFont, 11, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font calibrriFuente2 = new iTextSharp.text.Font(calibriBaseFont, 11, iTextSharp.text.Font.NORMAL, BaseColor.RED);
                // Definir el color celeste azulado
                BaseColor celesteAzulado = new BaseColor(0, 191, 255); // RGB para un tono celeste azulado

                // Crear la fuente Calibri en el tono celeste azulado
                iTextSharp.text.Font calibriFuente3 = new iTextSharp.text.Font(calibriBaseFont, 11, iTextSharp.text.Font.NORMAL, celesteAzulado);

                // Agrega los textos a la segunda celda
                PdfPCell textCell = new PdfPCell();
                textCell.Border = PdfPCell.NO_BORDER;

                // Alinea el contenido de la celda al centro
                textCell.HorizontalAlignment = Element.ALIGN_RIGHT;

                //AQUI IBA LA PARTE DERECHA DE ARRIBA DEL DOCUMENTO ORIGINAL


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

                // Crea el objeto PdfPTable con una sola fila y dos columnas
                PdfPTable table2 = new PdfPTable(2);
                table2.WidthPercentage = 100;

                // Define el ancho de las columnas
                float[] columnWidths = new float[] { 1f, 1f }; // Ajusta los anchos según sea necesario
                table2.SetWidths(columnWidths);

                // Añade el texto "Cotización" a la primera celda
                PdfPCell cell1 = new PdfPCell(new Phrase("Cotización/Proforma " + txtidQuote.Text, cotizacionFont));
                cell1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell1.HorizontalAlignment = Element.ALIGN_LEFT;
                table2.AddCell(cell1);

                // Añade la fecha a la segunda celda
                PdfPCell cell2 = new PdfPCell(new Phrase(DateTime.Now.ToString("dd/MM/yyyy"), cotizacionFont));
                cell2.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell2.HorizontalAlignment = Element.ALIGN_RIGHT;
                table2.AddCell(cell2);

                // Añade la tabla al documento
                document.Add(table2);

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


                #region Tabla de Información
                iTextSharp.text.Font infoFont = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                // Crear una tabla para los datos del proyecto y la información del cliente
                PdfPTable datosTable = new PdfPTable(2);
                datosTable.TotalWidth = 500f; // Ajusta el ancho total según tus necesidades
                datosTable.LockedWidth = true;

                // Define el ancho de las columnas
                float[] columnWidths2 = new float[] { 1f, 1f }; // Ajusta los anchos según sea necesario
                datosTable.SetWidths(columnWidths2);

                // Celda 1: Etiqueta "Cotización"
                PdfPCell cellEtiquetaCotizacion = new PdfPCell(new Phrase("Nombre: " + txtidClient.Text, calibrriFuente))
                {
                    Border = PdfPCell.NO_BORDER, // Sin borde
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaCotizacion);

                // Celda 2: Etiqueta vacía
                PdfPCell cellEtiquetaCliente = new PdfPCell(new Phrase(""))
                {
                    Border = PdfPCell.NO_BORDER, // Sin borde
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaCliente);

                // Celda 3: Etiqueta "Forma Pago"
                PdfPCell cellEtiquetaFormaPago = new PdfPCell(new Phrase("Proyecto: " + txtProjetName.Text, calibrriFuente))
                {
                    Border = PdfPCell.NO_BORDER, // Sin borde
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaFormaPago);

                // Celda 4: Etiqueta vacía
                PdfPCell cellEtiquetaTelefono = new PdfPCell(new Phrase(""))
                {
                    Border = PdfPCell.NO_BORDER, // Sin borde
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaTelefono);

                // Celda 5: Etiqueta "Teléfono"
                PdfPCell cellEtiquetaDireccion = new PdfPCell(new Phrase("Teléfono: " + txtTelefono.Text, calibrriFuente))
                {
                    Border = PdfPCell.NO_BORDER, // Sin borde
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaDireccion);

                // Celda 6: Etiqueta vacía
                PdfPCell cellvacia4 = new PdfPCell(new Phrase(""))
                {
                    Border = PdfPCell.NO_BORDER, // Sin borde
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellvacia4);

                // Celda 7: Etiqueta "Correo"
                PdfPCell cellEtiquetaCorreo = new PdfPCell(new Phrase("Correo: " + txtEmail.Text, calibrriFuente))
                {
                    Border = PdfPCell.NO_BORDER, // Sin borde
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellEtiquetaCorreo);

                // Celda 8: Etiqueta vacía
                PdfPCell cellvacia5 = new PdfPCell(new Phrase(""))
                {
                    Border = PdfPCell.NO_BORDER, // Sin borde
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                datosTable.AddCell(cellvacia5);

                // Añade la tabla al documento
                document.Add(datosTable);
                document.Add(new Paragraph(" ")); // Espacio en blanco

                // Añadir primer párrafo
                /*iTextSharp.text.Font bodyFont = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 11, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                Paragraph paragraph1 = new Paragraph("Presente:\nEsperando que sus proyectos personales y profesionales sean todo un éxito, le presentamos la cotización solicitada por su representada.", calibrriFuente)
                {
                    Alignment = Element.ALIGN_LEFT
                };
                document.Add(paragraph1);
                document.Add(new Paragraph(" ")); // Espacio en blanco*/

                // Añadir el segundo párrafo con la descripción proporcionada
                Paragraph paragraph2 = new Paragraph(descripcionTrabajo, calibrriFuente)
                {
                    Alignment = Element.ALIGN_LEFT
                };
                document.Add(paragraph2);
                document.Add(new Paragraph(" ")); // Espacio en blanco

                // Añadir el monto total del proyecto
                Paragraph paragraphMontoTotal = new Paragraph("Monto total del proyecto: " + txtTotal.Text, calibrriFuente2)
                {
                    Alignment = Element.ALIGN_LEFT
                };
                document.Add(paragraphMontoTotal);
                document.Add(new Paragraph(" ")); // Espacio en blanco
                document.Add(new Paragraph(" "));

                #endregion
                #region Condiciones, Notas y Cuentas

                // Agregar las Condiciones desde el txtConditional1 hasta el txtConditional10 en una tabla
                iTextSharp.text.Font condicionesFont = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
               // iTextSharp.text.Font textFont = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 11, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                PdfPTable tableCondiciones = new PdfPTable(1); // 1 columna
                tableCondiciones.WidthPercentage = 97;
                tableCondiciones.HorizontalAlignment = Element.ALIGN_CENTER;
                tableCondiciones.DefaultCell.Border = PdfPCell.NO_BORDER; // Eliminar borde de la celda por defecto

                PdfPCell cellCondiciones = new PdfPCell();
                cellCondiciones.Border = PdfPCell.NO_BORDER; // Eliminar borde de la celda
                cellCondiciones.HorizontalAlignment = Element.ALIGN_LEFT;
                cellCondiciones.VerticalAlignment = Element.ALIGN_MIDDLE;

                Paragraph paragraphCondiciones = new Paragraph
                {
                    SpacingBefore = 10f, // Espaciado antes del párrafo
                    SpacingAfter = 10f, // Espaciado después del párrafo
                    Leading = 14f // Espaciado entre líneas
                };
                paragraphCondiciones.Add(new Chunk("Condiciones de la Oferta", cotizacionFont));
                document.Add(new Paragraph(" "));
                paragraphCondiciones.Add(Chunk.NEWLINE); // Salto de línea
                paragraphCondiciones.Add(new Chunk("       " + txtConditional1.Text, calibrriFuente));
                paragraphCondiciones.Add(Chunk.NEWLINE);
                paragraphCondiciones.Add(new Chunk("       " + txtConditional2.Text, calibrriFuente));
                paragraphCondiciones.Add(Chunk.NEWLINE);
                paragraphCondiciones.Add(new Chunk("       " + txtConditional3.Text, calibrriFuente));
                paragraphCondiciones.Add(Chunk.NEWLINE);
                paragraphCondiciones.Add(new Chunk("       " + txtConditional4.Text, calibrriFuente));
                paragraphCondiciones.Add(Chunk.NEWLINE);
                paragraphCondiciones.Add(new Chunk("       " + txtConditional5.Text, calibrriFuente));
                paragraphCondiciones.Add(Chunk.NEWLINE);
                paragraphCondiciones.Add(new Chunk("       " + txtConditional6.Text, calibrriFuente));
                paragraphCondiciones.Add(Chunk.NEWLINE);
                paragraphCondiciones.Add(new Chunk("       " + txtConditional7.Text, calibrriFuente));
                paragraphCondiciones.Add(Chunk.NEWLINE);
                paragraphCondiciones.Add(new Chunk("       " + txtConditional8.Text, calibrriFuente));
                paragraphCondiciones.Add(Chunk.NEWLINE);
                paragraphCondiciones.Add(new Chunk("       " + txtConditional9.Text, calibrriFuente));
                paragraphCondiciones.Add(Chunk.NEWLINE);
                paragraphCondiciones.Add(new Chunk("       " + txtConditional10.Text, calibrriFuente));
                paragraphCondiciones.Add(Chunk.NEWLINE);

                cellCondiciones.AddElement(paragraphCondiciones);
                tableCondiciones.AddCell(cellCondiciones);

                document.Add(tableCondiciones);
                document.Add(new Paragraph(" "));

                // Agregar línea para firma y nombre del usuario
                PdfPTable tableFirma = new PdfPTable(1); // 1 columna
                tableFirma.WidthPercentage = 97;
                tableFirma.HorizontalAlignment = Element.ALIGN_LEFT;
                tableFirma.DefaultCell.Border = PdfPCell.NO_BORDER; // Eliminar borde de la celda por defecto

                // Celda para la línea de firma
                PdfPCell cellFirma = new PdfPCell();
                cellFirma.Border = PdfPCell.NO_BORDER; // Eliminar borde de la celda
                cellFirma.HorizontalAlignment = Element.ALIGN_LEFT;
                cellFirma.VerticalAlignment = Element.ALIGN_MIDDLE;

                // Agregar línea
                Phrase firmaPhrase = new Phrase();
                firmaPhrase.Add(new Chunk("____________________________________________________", calibrriFuente)); // Línea
                cellFirma.AddElement(firmaPhrase);

                // Agregar el nombre del usuario debajo de la línea
                Phrase nombrePhrase = new Phrase();
                nombrePhrase.Add(Chunk.NEWLINE); // Salto de línea
                nombrePhrase.Add(new Chunk(UserCache.Name, calibrriFuente)); // Nombre del usuario
                cellFirma.AddElement(nombrePhrase);

                tableFirma.AddCell(cellFirma);
                document.Add(tableFirma);


                #endregion


                #region Tabla de Productos

                // Crear una tabla con una sola columna
                PdfPTable tabla = new PdfPTable(1);
                tabla.WidthPercentage = 100f; // Ajustar al 100% del ancho de la página

                // Fuente para los textos
                iTextSharp.text.Font fontTexto = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.BLACK);
                iTextSharp.text.Font fontPrecio = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK);
                iTextSharp.text.Font fontTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 13, BaseColor.WHITE);
                iTextSharp.text.Font fontTituloSeccion = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.BLACK); // Fuente para el título

                // Color de fondo para los encabezados
                BaseColor colorEncabezado = new BaseColor(70, 130, 180);

                // Contador para los títulos de cada ventana
                int contadorVentana = 1;

                // Añadir los datos de cada fila
                foreach (DataGridViewRow row in dgCotizaciones.Rows)
                {
                    if (row.IsNewRow) continue; // Omitir la fila nueva si la hay

                    // Crear una tabla temporal para cada conjunto de datos
                    PdfPTable filaTabla = new PdfPTable(1);
                    filaTabla.WidthPercentage = 100f;

                    // Añadir el título para cada ventana
                    PdfPCell cellTitulo = new PdfPCell(new Phrase($"Ventana tipo {contadorVentana}", cotizacionFont))
                    {
                        Colspan = 3, // Para que ocupe toda la columna de la tabla principal
                        Border = PdfPCell.NO_BORDER,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    };
                    filaTabla.AddCell(cellTitulo);
                    document.Add(new Paragraph(" "));





                    // Obtener la descripción completa y dividirla en dos partes
                    string descripcionCompleta = row.Cells["Description"]?.Value?.ToString() ?? "Descripción no disponible";
                    string[] partesDescripcion = descripcionCompleta.Split(new[] { "\n" }, StringSplitOptions.None);

                    // Primera parte de la descripción (en una sola celda)
                    string primeraParte = "";
                    string segundaParte = "";

                    // Asumir que las primeras líneas forman la primera parte de la descripción
                    // y el resto es la segunda parte.
                    for (int i = 0; i < partesDescripcion.Length; i++)
                    {
                        if (i < 5) // Puedes ajustar este número según tu necesidad
                        {
                            primeraParte += partesDescripcion[i] + ", "; // Cambié '\n' por ', ' para formato en fila
                        }
                        else
                        {
                            segundaParte += partesDescripcion[i] + "\n";
                        }
                    }

                    // Eliminar la última coma y espacio extra en la primera parte si es necesario
                    if (primeraParte.EndsWith(", "))
                    {
                        primeraParte = primeraParte.TrimEnd(',', ' '); // Elimina la coma y el espacio al final
                    }

                    // Eliminar la última coma en la segunda parte si es el único carácter antes del salto de línea
                    if (segundaParte.EndsWith(",\n"))
                    {
                        segundaParte = segundaParte.TrimEnd(',', '\n'); // Elimina la coma y el salto de línea al final
                    }

                    // Transformaciones en la primera parte
                    primeraParte = primeraParte.Replace(":", ""); // Eliminar los dos puntos
                    primeraParte = primeraParte.Replace(" vid ", " "); // Eliminar la palabra "vid" que está sola y rodeada por espacios

                    // Asegurarse de que la palabra "vidrio" no se vea afectada
                    // Eliminar "vid" que está entre comas o espacios, pero no afectará "vidrio"
                    string[] palabras = primeraParte.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    primeraParte = string.Join(" ", palabras.Where(p => p.ToLower() != "vid"));

                    // Eliminar "cerradura" que está entre comas o espacios, pero dejar solo una instancia si aparece más de una vez
                    string[] palabras2 = primeraParte.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    List<string> resultado2 = new List<string>();
                    bool cerraduraVista = false;

                    foreach (var palabra in palabras2)
                    {
                        if (palabra.ToLower() == "cerradura")
                        {
                            if (!cerraduraVista)
                            {
                                resultado2.Add(palabra);
                                cerraduraVista = true;
                            }
                            // Si la palabra es "cerradura" y ya se ha visto, simplemente no la añadimos a resultado2
                        }
                        else
                        {
                            resultado2.Add(palabra);
                        }
                    }

                    primeraParte = string.Join(" ", resultado2);


                    // Verificar y eliminar duplicados de la palabra "cerradura"
                    List<string> partesPrimeraParte = primeraParte.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    List<string> resultado = new List<string>();
                    HashSet<string> palabrasVista = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                    foreach (var parte in partesPrimeraParte)
                    {
                        string palabraLimpiada = parte.Trim().ToLower();
                        if (palabraLimpiada == "cerradura")
                        {
                            if (!palabrasVista.Contains("cerradura"))
                            {
                                resultado.Add(parte);
                                palabrasVista.Add("cerradura");
                            }
                        }
                        else
                        {
                            resultado.Add(parte);
                        }
                    }

                    primeraParte = string.Join(", ", resultado);

                    // Asegurarse de que la primera parte termine con un punto
                    if (!string.IsNullOrEmpty(primeraParte) && !primeraParte.TrimEnd().EndsWith("."))
                    {
                        primeraParte += ".";
                    }

                    // Capitalizar solo la primera letra de la primera palabra
                    if (!string.IsNullOrEmpty(primeraParte))
                    {
                        primeraParte = char.ToUpper(primeraParte[0]) + primeraParte.Substring(1).ToLower();
                    }

                    // Segunda parte de la descripción
                    // Puedes mantener la capitalización original de la segunda parte si es necesario
                    segundaParte = segundaParte.Replace(":", ""); // Eliminar los dos puntos
                    segundaParte = segundaParte.Replace(" vid ", " "); // Eliminar la palabra "vid" que está sola y rodeada por espacios

                    // Asegurarse de que la segunda parte termine con un punto y no esté en una nueva línea
                    if (!string.IsNullOrEmpty(segundaParte) && !segundaParte.TrimEnd().EndsWith("."))
                    {
                        segundaParte = segundaParte.TrimEnd() + ".";
                    }

                    // Primera parte en una celda
                    PdfPCell cellPrimeraParte = new PdfPCell(new Phrase(primeraParte.Trim(), calibrriFuente))
                    {
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        Border = PdfPCell.NO_BORDER
                    };
                    filaTabla.AddCell(cellPrimeraParte);

                    // Espacio entre la primera y la segunda parte
                    filaTabla.AddCell(new PdfPCell(new Phrase(" ")) { Border = PdfPCell.NO_BORDER });

                    // Segunda parte de la descripción
                    PdfPCell cellSegundaParte = new PdfPCell(new Phrase(segundaParte.Trim(), calibrriFuente))
                    {
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        Border = PdfPCell.NO_BORDER
                    };
                    filaTabla.AddCell(cellSegundaParte);








                    // Espacio entre la descripción y la imagen
                    filaTabla.AddCell(new PdfPCell(new Phrase(" ")) { Border = PdfPCell.NO_BORDER }); // Agregar un espacio adicional

                    // Imagen
                    PdfPCell cellImagen = new PdfPCell();
                    string rutaImagen = row.Cells["URL"]?.Value?.ToString() ?? string.Empty;
                    System.Version versionActual = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                    string versionActualString = $"GlassWin{versionActual.Major}.{versionActual.Minor}.{versionActual.Build}.{versionActual.Revision}";

                    // Reemplazar la versión en la ruta con la versión actual
                    string rutaCorregida = ReemplazarVersionEnRuta(rutaImagen, versionActualString);

                    // Obtener el directorio de trabajo actual
                    string directorioDeTrabajo = Directory.GetCurrentDirectory();
                    Console.WriteLine($"Directorio de trabajo: {directorioDeTrabajo}");


                    if (!string.IsNullOrEmpty(rutaImagen))
                    {
                        try
                        {
                            // Procesar la ruta de la imagen
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
                                // Obtener dimensiones de la descripción y convertirlas a píxeles
                                var (anchoEnMetros, alturaEnMetros) = ObtenerDimensionesDeDescripcion(descripcionCompleta);

                                int anchoVentana = (int)(anchoEnMetros * MetrosAPixeles);
                                int altoVentana = (int)(alturaEnMetros * MetrosAPixeles);

                                if (anchoVentana > 220)
                                {
                                    anchoVentana = 200; // Reducir el ancho a 200 píxeles
                                }


                                // Ajustar el ancho y la altura si son 0
                                if (anchoVentana == 0) anchoVentana = 150; // Ancho por defecto
                                if (altoVentana == 0) altoVentana = 100; // Alto por defecto

                                // Mostrar dimensiones calculadas para depuración
                                Console.WriteLine($"Ancho ventana en píxeles: {anchoVentana}, Alto ventana en píxeles: {altoVentana}");

                                // Cargar la imagen
                                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(rutaAbsoluta);

                                // Ajustar el tamaño de la imagen con ScaleAbsolute
                                img.ScaleAbsolute(anchoVentana, altoVentana);

                                // Crear una celda para la imagen
                                PdfPCell cellImagenTemp = new PdfPCell(img)
                                {
                                    HorizontalAlignment = Element.ALIGN_LEFT,
                                    Border = PdfPCell.NO_BORDER
                                };
                                filaTabla.AddCell(cellImagenTemp);
                            }
                            else
                            {
                                PdfPCell cellImagenTemp = new PdfPCell(new Phrase("Imagen no disponible", calibrriFuente))
                                {
                                    HorizontalAlignment = Element.ALIGN_CENTER,
                                    Border = PdfPCell.NO_BORDER
                                };
                                filaTabla.AddCell(cellImagenTemp);
                            }
                        }
                        catch (Exception)
                        {
                            cellImagen.Phrase = new Phrase("Imagen no disponible", calibrriFuente);
                        }
                    }
                    else
                    {
                        cellImagen.Phrase = new Phrase("Imagen no disponible", calibrriFuente);
                    }
                    cellImagen.HorizontalAlignment = Element.ALIGN_CENTER;
                    cellImagen.Border = PdfPCell.NO_BORDER;
                    filaTabla.AddCell(cellImagen);

                    // Añadir la tabla de fila a la tabla principal
                    PdfPCell cellFila = new PdfPCell(filaTabla)
                    {
                        Border = PdfPCell.NO_BORDER
                    };
                    tabla.AddCell(cellFila);

                    // Incrementar el contador de ventanas
                    contadorVentana++;

                    // Añadir un espacio entre cada conjunto de datos
                    tabla.AddCell(new PdfPCell(new Phrase(" ")) { Border = PdfPCell.NO_BORDER });
                }

                // Agregar la tabla al documento
                document.Add(tabla);
                document.Add(new Paragraph(" ")); // Agregar un espacio en blanco en el documento
                document.Add(new Paragraph(" ")); // Agregar un espacio en blanco en el documento

                #endregion

                #region Cuentas
                //Vidriera Palmares
                if (CompanyCache.IdCompany == 222222222)
                {
                    Paragraph CuentasParagraph = new Paragraph("CUENTAS", FontFactory.GetFont(FontFactory.HELVETICA, 12, 1, BaseColor.BLACK));
                    CuentasParagraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(CuentasParagraph);
                    Paragraph CuentasParagraph2 = new Paragraph("Banco Nacional", FontFactory.GetFont(FontFactory.HELVETICA, 10, 1, BaseColor.BLACK));
                    CuentasParagraph2.Alignment = Element.ALIGN_LEFT;
                    document.Add(CuentasParagraph2);

                    Paragraph Cuenta1Paragraph = new Paragraph("• Cuenta IBAN colones CR84015101910010039940", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    Cuenta1Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Cuenta1Paragraph);

                    Paragraph Cuenta2Paragraph = new Paragraph("• Cuenta IBAN dólares CR20015101920020050861", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    Cuenta2Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Cuenta2Paragraph);

                    Paragraph Cuenta3Paragraph = new Paragraph("• Sinpe Móvil a nombre de Vidriera Palmares S.A 87091108 \r\n\r\n", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    Cuenta3Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Cuenta3Paragraph);
                }
                //MS
                if (CompanyCache.IdCompany == 204260627)
                {
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
                //Usuario Prueba
                if (CompanyCache.IdCompany == 3101704274)
                {
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
        // Función para obtener las dimensiones de la descripción
        private (decimal ancho, decimal alto) ObtenerDimensionesDeDescripcion(string descripcion)
        {
            decimal ancho = 0;
            decimal alto = 0;

            // Expresiones regulares para encontrar los valores de Ancho y Alto
            var regexAncho = new Regex(@"Ancho:\s*(\d+(\.\d+)?)", RegexOptions.IgnoreCase);
            var regexAlto = new Regex(@"Alto:\s*(\d+(\.\d+)?)", RegexOptions.IgnoreCase);

            // Buscar los valores en la descripción
            var matchAncho = regexAncho.Match(descripcion);
            var matchAlto = regexAlto.Match(descripcion);

            if (matchAncho.Success)
            {
                decimal.TryParse(matchAncho.Groups[1].Value, out ancho);
            }
            if (matchAlto.Success)
            {
                decimal.TryParse(matchAlto.Groups[1].Value, out alto);
            }

            return (ancho, alto);
        }


        #endregion

        #region Mover form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        // Variables para almacenar la posición del mouse
        private bool isDragging = false;

        private async void btnDolarCambio_Click(object sender, EventArgs e)
        {
            dolar = true;
            colon = false;
            cbIva.Enabled = false;
            btnApply.Enabled = false;
            // Obtener la fecha actual del sistema
            string fechaActual = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

            // Usar la fecha actual como fecha de inicio y fecha final
            string fechaInicio = fechaActual;
            string fechaFinal = fechaActual;

            string valor = await TipoCambioService.ObtenerTipoCambioAsync(fechaInicio, fechaFinal);
            Console.WriteLine($"Valor exacto recibido: '{valor}'");

            // Elimina espacios en blanco alrededor
            valor = valor.Trim();

            Console.WriteLine($"Valor limpiado: '{valor}'");

            // Intenta convertir el valor a decimal
            if (decimal.TryParse(valor, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valorDecimal))
            {
                // Convertir a string y quitar ceros innecesarios usando el formato G29
                string valorFinal = valorDecimal.ToString("G29", CultureInfo.InvariantCulture);
                Console.WriteLine($"Valor convertido a decimal y limpiado: {valorFinal}");
                tasaCambio = valorDecimal;

                // Convertir el valor de txtTotal a decimal
                if (Total != 0)
                {
                    // Calcular el valor en dólares
                    decimal totalDolares = Total / tasaCambio;

                    // Mostrar el valor en dólares en txtTotal
                    txtTotal.Text = totalDolares.ToString("C2", CultureInfo.GetCultureInfo("en-US"));

                    //IVA
                    // Calcular el valor en dólares
                    decimal totalIVADolares = IVA / tasaCambio;

                    // Mostrar el valor en dólares en txtTotal
                    txtIVA.Text = totalIVADolares.ToString("C2", CultureInfo.GetCultureInfo("en-US"));

                    //Subtotal
                    // Calcular el valor en dólares
                    decimal subTotalDolares = SubTotal / tasaCambio;

                    // Mostrar el valor en dólares en txtTotal
                    txtSubtotal.Text = subTotalDolares.ToString("C2", CultureInfo.GetCultureInfo("en-US"));
                   


                }
                else
                {
                    MessageBox.Show("El valor en colones no es válido.", "Error de conversión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Console.WriteLine("No se pudo convertir el valor a decimal.");
                MessageBox.Show("No se pudo obtener la tasa de cambio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCambioDolar2_Click(object sender, EventArgs e)
        {
            colon = true;
            dolar = false;
            cbIva.Enabled = true;
            btnApply.Enabled = true;
            // Verificar si los valores ya están en dólares
            bool yaEnDolares = txtTotal.Text.StartsWith("₡") || txtIVA.Text.StartsWith("₡") || txtSubtotal.Text.StartsWith("₡");

            if (yaEnDolares)
            {
                MessageBox.Show("Los valores ya están en colones.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; // Salir del método sin realizar la conversión
            }

            // Asegúrate de que la tasa de cambio ha sido asignada correctamente
            if (tasaCambio > 0)
            {
                // Convertir el valor de txtTotal en dólares a decimal
                if (decimal.TryParse(txtTotal.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("en-US"), out decimal totalDolares))
                {
                    // Calcular el valor en colones
                    decimal totalColones = totalDolares * tasaCambio;

                    // Mostrar el valor en colones en txtTotal
                    txtTotal.Text = totalColones.ToString("c", CultureInfo.GetCultureInfo("es-CR"));

                    // Convertir el valor de txtIVA en dólares a decimal
                    if (decimal.TryParse(txtIVA.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("en-US"), out decimal ivaDolares))
                    {
                        // Calcular el valor en colones
                        decimal ivaColones = ivaDolares * tasaCambio;

                        // Mostrar el valor en colones en txtIVA
                        txtIVA.Text = ivaColones.ToString("c", CultureInfo.GetCultureInfo("es-CR"));
                    }
                    else
                    {
                        MessageBox.Show("El valor del IVA en dólares no es válido.", "Error de conversión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    // Convertir el valor de txtSubtotal en dólares a decimal
                    if (decimal.TryParse(txtSubtotal.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("en-US"), out decimal subtotalDolares))
                    {
                        // Calcular el valor en colones
                        decimal subtotalColones = subtotalDolares * tasaCambio;

                        // Mostrar el valor en colones en txtSubtotal
                        txtSubtotal.Text = subtotalColones.ToString("c", CultureInfo.GetCultureInfo("es-CR"));
                    }
                    else
                    {
                        MessageBox.Show("El valor del subtotal en dólares no es válido.", "Error de conversión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("El valor en dólares de total no es válido.", "Error de conversión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("La tasa de cambio no está disponible o no es válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private Point startPoint = new Point(0, 0);

        private void btnCargarDesglose_Click(object sender, EventArgs e)
        {
            

            // Crear y mostrar el formulario frmDesglose con los datos copiados
            frmDesglose desgloseForm = new frmDesglose( Convert.ToInt32(txtidQuote.Text));
            desgloseForm.ShowDialog();
        }






        private void frmQuote_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - startPoint.X, p.Y - startPoint.Y);
            }
        }
        private void frmQuote_MouseUp(object sender, MouseEventArgs e)
        {
            // Cuando el botón del mouse es soltado
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }
        private void frmQuote_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion
        #region Tasa Cambio
        public class TipoCambioService
        {
            private static readonly string url = "https://gee.bccr.fi.cr/Indicadores/Suscripciones/WS/wsindicadoreseconomicos.asmx";
            private static readonly int codigoIndicador = 317; // Código para tipo de cambio de compra
            private static readonly string nombreUsuario = "Alexander Durán Varela";
            private static readonly string indicadorSubNivel = "N";
            private static readonly string correoElectronico = "alexduva21@gmail.com";
            private static readonly string tokenSuscripcion = "VANUNMANAA"; // Reemplazar con tu token real

            public static async Task<string> ObtenerTipoCambioAsync(string fechaInicio, string fechaFinal)
            {
                using (HttpClient client = new HttpClient())
                {
                    // Crear el contenido de la solicitud en formato XML (SOAP)
                    var soapEnvelope = $@"
                <soap:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/'>
                    <soap:Body>
                        <ObtenerIndicadoresEconomicos xmlns='http://ws.sdde.bccr.fi.cr'>
                            <Indicador>{codigoIndicador}</Indicador>
                            <FechaInicio>{fechaInicio}</FechaInicio>
                            <FechaFinal>{fechaFinal}</FechaFinal>
                            <Nombre>{nombreUsuario}</Nombre>
                            <SubNiveles>{indicadorSubNivel}</SubNiveles>
                            <CorreoElectronico>{correoElectronico}</CorreoElectronico>
                            <Token>{tokenSuscripcion}</Token>
                        </ObtenerIndicadoresEconomicos>
                    </soap:Body>
                </soap:Envelope>";

                    // Crear el contenido de la solicitud
                    var content = new StringContent(soapEnvelope, Encoding.UTF8, "text/xml");

                    // Agregar el encabezado SOAPAction
                    client.DefaultRequestHeaders.Add("SOAPAction", "http://ws.sdde.bccr.fi.cr/ObtenerIndicadoresEconomicos");

                    // Hacer la solicitud POST
                    HttpResponseMessage response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        // Procesar el XML de respuesta
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(responseBody);

                        XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
                        nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                        nsmgr.AddNamespace("diffgr", "urn:schemas-microsoft-com:xml-diffgram-v1");
                        nsmgr.AddNamespace("msdata", "urn:schemas-microsoft-com:xml-msdata");

                        // Extraer datos del XML
                        XmlNode valorNode = doc.SelectSingleNode("//diffgr:diffgram/Datos_de_INGC011_CAT_INDICADORECONOMIC/INGC011_CAT_INDICADORECONOMIC/NUM_VALOR", nsmgr);

                        if (valorNode != null)
                        {
                            return valorNode.InnerText; // Devolver el valor del tipo de cambio
                        }
                        else
                        {
                            return "No se encontraron los datos esperados en la respuesta.";
                        }
                    }
                    else
                    {
                        return $"Error: {response.StatusCode}";
                    }
                }
            }
        }
        #endregion Tasa Cambio
        public bool GeneratePDF3()
        {
            #region Crear el documento
            string rutaArchivoPDF = "";
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
                //Usuario de Prueba
                if (CompanyCache.IdCompany == 999999999)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\UsuarioPrueba.png";
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
                if (CompanyCache.IdCompany == 999999999)
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
                else if(CompanyCache.IdCompany == 3101623589 || CompanyCache.IdCompany == 3101623581)
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
                else if(CompanyCache.IdCompany == 205150849)
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

                // Añadir celdas de datos en dos filas para asegurar que todas se muestren
                // Fila 1
                datosTable.AddCell(new PdfPCell(new Phrase("Cliente: " + txtidClient.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12))) { Border = PdfPCell.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                datosTable.AddCell(new PdfPCell(new Phrase("Cotización: " + txtidQuote.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12))) { Border = PdfPCell.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });


                // Fila 2
                datosTable.AddCell(new PdfPCell(new Phrase("Teléfono: " + txtTelefono.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12))) { Border = PdfPCell.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                datosTable.AddCell(new PdfPCell(new Phrase("Fecha: " + txtDate.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12))) { Border = PdfPCell.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });


                // Fila 3
                datosTable.AddCell(new PdfPCell(new Phrase("Correo: " + txtEmail.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12))) { Border = PdfPCell.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                datosTable.AddCell(new PdfPCell(new Phrase("Proyecto: " + txtProjetName.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12))) { Border = PdfPCell.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                
                // Verifica si txtAdreesClient.Text está vacío o es nulo
                string direccionCliente = string.IsNullOrWhiteSpace(txtAdreesClient.Text) ? "Sin dirección" : txtAdreesClient.Text;

                // Fila 4
                datosTable.AddCell(new PdfPCell(new Phrase("Dirección Cliente: " + direccionCliente, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
                {
                    Border = PdfPCell.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });
                datosTable.AddCell(new PdfPCell(new Phrase("")) { Border = PdfPCell.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
              




                // Añadir tabla al documento
                document.Add(datosTable);
                document.Add(new Paragraph(" "));

                #endregion


                // Cambiar el nombre de las columnas en el DataGridView
                dgCotizaciones.Columns["URL"].HeaderText = "Diseño";
                dgCotizaciones.Columns["idWindows"].HeaderText = "ID Ventana";
               
                dgCotizaciones.Columns["Description"].HeaderText = "Descripción"; // Cambio aquí

              

                #region Tabla de Productos
                // Crear una tabla con el número de columnas de tu DataGridView, menos la columna "Precio" y "ID Ventana"
                int numeroDeColumnas = dgCotizaciones.Columns.Count - 2; // Reducir el conteo de columnas por la columna "Precio" y "ID Ventana"
                PdfPTable tabla = new PdfPTable(numeroDeColumnas);
                tabla.TotalWidth = 500f; // Ajusta el ancho total según tus necesidades     
                tabla.LockedWidth = true;
                float[] tablaW = new float[numeroDeColumnas]; // Crear un array de anchos de columna con el nuevo número de columnas

                // Copia los anchos de columnas existentes, excepto la columna "Precio" y "ID Ventana"
                int k = 0;
                for (int i = 0; i < dgCotizaciones.Columns.Count; i++)
                {
                    if (dgCotizaciones.Columns[i].HeaderText != "Precio" && dgCotizaciones.Columns[i].HeaderText != "ID Ventana")
                    {
                        tablaW[k] = 190f; // Ajusta estos valores según los anchos de columna que desees
                        k++;
                    }
                }
                tabla.SetWidths(tablaW);

                // Agregar encabezados de columna, omitiendo "Precio" y "ID Ventana"
                for (int i = 0; i < dgCotizaciones.Columns.Count; i++)
                {
                    if (dgCotizaciones.Columns[i].HeaderText != "Precio" && dgCotizaciones.Columns[i].HeaderText != "ID Ventana")
                    {
                        PdfPCell celda = new PdfPCell(new Phrase(dgCotizaciones.Columns[i].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 13, BaseColor.WHITE))); // Reducimos el tamaño a 13 puntos
                        celda.HorizontalAlignment = Element.ALIGN_CENTER;
                        celda.BackgroundColor = new BaseColor(70, 130, 180);
                        tabla.AddCell(celda);
                    }
                }

                // Agregar filas de datos, omitiendo la columna "Precio" y "ID Ventana"
                for (int i = 0; i < dgCotizaciones.Rows.Count; i++)
                {
                    for (int j = 0; j < dgCotizaciones.Columns.Count; j++)
                    {
                        if (dgCotizaciones.Columns[j].HeaderText != "Precio" && dgCotizaciones.Columns[j].HeaderText != "ID Ventana")
                        {
                            PdfPCell cell = new PdfPCell(); // Inicializar la celda por defecto

                            if (dgCotizaciones[j, i].Value != null)
                            {
                                if (dgCotizaciones.Columns[j].HeaderText == "Diseño")
                                {
                                    string rutaImagen = dgCotizaciones[j, i].Value.ToString();
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
                                        decimal anchoEnMetros = ObtenerAncho(dgCotizaciones.Rows[i].Cells[2].Value.ToString());
                                        decimal alturaEnMetros = ObtenerAlto(dgCotizaciones.Rows[i].Cells[2].Value.ToString());

                                        int anchoVentana = (int)(anchoEnMetros * MetrosAPixeles);
                                        int altoVentana = (int)(alturaEnMetros * MetrosAPixeles);

                                        if (anchoVentana > 220)
                                        {
                                            anchoVentana = 200; // Reducir el ancho a 200 píxeles
                                        }


                                        if (anchoVentana == 0) anchoVentana = 150; // Ajuste por defecto
                                        if (altoVentana == 0) altoVentana = 100; // Ajuste por defecto

                                        // Mostrar dimensiones calculadas para depuración
                                        Console.WriteLine($"Ancho ventana en píxeles: {anchoVentana}, Alto ventana en píxeles: {altoVentana}");

                                        // Cargar la imagen y ajustar su tamaño
                                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(rutaAbsoluta);

                                        // Ajustar el tamaño de la imagen con ScaleAbsolute
                                        img.ScaleAbsolute(anchoVentana, altoVentana);

                                        cell = new PdfPCell(img);
                                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                        cell.FixedHeight = altoVentana; // Ajustar la altura de la celda para coincidir con la imagen
                                    }
                                    else
                                    {
                                        cell = new PdfPCell(new Phrase("Sin Imagen", FontFactory.GetFont(FontFactory.HELVETICA, 12)));
                                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                        cell.FixedHeight = 50f; // Ajusta la altura según sea necesario
                                    }
                                }
                                else if (dgCotizaciones.Columns[j].HeaderText == "Descripción") // Cambio aquí
                                {
                                    // Obtener el texto de la celda y agregar viñetas
                                    string textoConViñetas = AgregarViñetas(dgCotizaciones[j, i].Value.ToString());

                                    // Crear la celda con el texto modificado
                                    cell = new PdfPCell(new Phrase(textoConViñetas, FontFactory.GetFont(FontFactory.HELVETICA)));
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
                            }
                            else
                            {
                                cell = new PdfPCell(new Phrase("")); // Inicializa con una celda vacía si el valor es null
                            }

                            tabla.AddCell(cell);
                        }
                    }
                }

                // Crear una celda para mostrar el monto total con bordes y fondo
                PdfPCell celdaMontoTotal = new PdfPCell(new Phrase("Monto Total: " + (dolar ? txtTotal.Text : "¢" + txtTotal.Text), FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.WHITE)));
                celdaMontoTotal.Colspan = numeroDeColumnas; // Hacer que la celda abarque todas las columnas
                celdaMontoTotal.HorizontalAlignment = Element.ALIGN_RIGHT; // Alinear el texto a la derecha
                celdaMontoTotal.Border = iTextSharp.text.Rectangle.BOX; // Aplicar borde a la celda completa
                celdaMontoTotal.BorderWidth = 1f; // Espesor del borde
                celdaMontoTotal.BackgroundColor = new BaseColor(70, 130, 180); // Color de fondo
                celdaMontoTotal.Padding = 10f; // Agregar un padding para mejorar la apariencia

                // Agregar la celda a la tabla
                tabla.AddCell(celdaMontoTotal);

                // Agregar la tabla al documento
                document.Add(tabla);

                document.Add(new Paragraph(" ")); // Esto agrega un espacio en blanco en el documento

                #endregion








                #region Precios
                /*
                // Crear un párrafo para mostrar el monto total
                Paragraph paragraphTotal = new Paragraph();

                // Agregar el texto para el monto total
                string textoTotal = "Monto Total: ";
                if (dolar)
                {
                    textoTotal += txtTotal.Text;
                }
                else
                {
                    textoTotal += "¢" + txtTotal.Text;
                }

                // Crear una frase con el texto total
                Phrase phraseTotal = new Phrase(textoTotal, FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.BLACK));

                // Agregar la frase al párrafo
                paragraphTotal.Add(phraseTotal);

                // Configurar la alineación del párrafo
                paragraphTotal.Alignment = Element.ALIGN_RIGHT; // Cambia a Element.ALIGN_LEFT si prefieres alinearlo a la izquierda

                // Agregar el párrafo al documento
                document.Add(paragraphTotal);
                document.Add(new Paragraph(" ")); // Esto agrega un espacio en blanco en el documento
                */
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


                if (CompanyCache.IdCompany == 112540885)
                {
                    Paragraph CuentasParagraph = new Paragraph("CUENTAS", FontFactory.GetFont(FontFactory.HELVETICA, 12, 1, BaseColor.GRAY));
                    CuentasParagraph.Alignment = Element.ALIGN_CENTER;
                    document.Add(CuentasParagraph);

                    // Crear tabla con 2 columnas para organizar texto e imágenes
                    PdfPTable table = new PdfPTable(1);
                    table.WidthPercentage = 100; // Ajustar el ancho de la tabla al 100% del documento

                    // Agregar las cuentas y los logos en una tabla
                    // Cuenta Banco Popular
                    string rutaBP = Path.GetDirectoryName(Application.ExecutablePath) + "\\Images\\Logos\\logo_bancopopular.png";
                    Image imgBancoPopular = Image.GetInstance(rutaBP);
                    imgBancoPopular.ScaleToFit(50f, 50f); // Ajustar el tamaño de la imagen
                    PdfPCell cellLogoBP = new PdfPCell(imgBancoPopular);
                    cellLogoBP.Border = PdfPCell.NO_BORDER;
                    cellLogoBP.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cellLogoBP);

                    PdfPCell cellTextBP = new PdfPCell(new Phrase("• Cuenta Banco Popular colones CR32016111120141093142.", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK)));
                    cellTextBP.Border = PdfPCell.NO_BORDER;
                    cellTextBP.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cellTextBP);

                    // Cuenta IBAN
                    string rutaIBAN = Path.GetDirectoryName(Application.ExecutablePath) + "\\Images\\Logos\\Logos-PR-BCR.png";
                    Image imgIBAN = Image.GetInstance(rutaIBAN);
                    imgIBAN.ScaleToFit(50f, 50f); // Ajustar el tamaño de la imagen
                    PdfPCell cellLogoIBAN = new PdfPCell(imgIBAN);
                    cellLogoIBAN.Border = PdfPCell.NO_BORDER;
                    cellLogoIBAN.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cellLogoIBAN);

                    PdfPCell cellTextIBAN = new PdfPCell(new Phrase("• Cuenta IBAN colones CR36010200009449083184.", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK)));
                    cellTextIBAN.Border = PdfPCell.NO_BORDER;
                    cellTextIBAN.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cellTextIBAN);

                    // Cuenta BAC
                    string rutaBAC = Path.GetDirectoryName(Application.ExecutablePath) + "\\Images\\Logos\\logo_bacredomatic.png";
                    Image imgBAC = Image.GetInstance(rutaBAC);
                    imgBAC.ScaleToFit(50f, 50f); // Ajustar el tamaño de la imagen
                    PdfPCell cellLogoBAC = new PdfPCell(imgBAC);
                    cellLogoBAC.Border = PdfPCell.NO_BORDER;
                    cellLogoBAC.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cellLogoBAC);

                    PdfPCell cellTextBAC = new PdfPCell(new Phrase("• Cuenta BAC colones 944908318.", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK)));
                    cellTextBAC.Border = PdfPCell.NO_BORDER;
                    cellTextBAC.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cellTextBAC);


                    // Cuenta BN
                    string rutaBN = Path.GetDirectoryName(Application.ExecutablePath) + "\\Images\\Logos\\bn.png";
                    Image imgBN = Image.GetInstance(rutaBN);
                    imgBN.ScaleToFit(50f, 50f); // Ajustar el tamaño de la imagen
                    PdfPCell cellLogoBN = new PdfPCell(imgBN);
                    cellLogoBN.Border = PdfPCell.NO_BORDER;
                    cellLogoBN.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cellLogoBN);

                    PdfPCell cellTextBN = new PdfPCell(new Phrase("• Cuenta BN  CR37015112720010160574.", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK)));
                    cellTextBN.Border = PdfPCell.NO_BORDER;
                    cellTextBN.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cellTextBN);


                    // SINPE Móvil
                    string rutaSinpe = Path.GetDirectoryName(Application.ExecutablePath) + "\\Images\\Logos\\sinpe-movil-2.png";
                    Image imgSINPE = Image.GetInstance(rutaSinpe);
                    imgSINPE.ScaleToFit(50f, 50f); // Ajustar el tamaño de la imagen
                    PdfPCell cellLogoSINPE = new PdfPCell(imgSINPE);
                    cellLogoSINPE.Border = PdfPCell.NO_BORDER;
                    cellLogoSINPE.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cellLogoSINPE);

                    PdfPCell cellTextSINPE = new PdfPCell(new Phrase("• Número SINPE Móvil 89089444.", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK)));
                    cellTextSINPE.Border = PdfPCell.NO_BORDER;
                    cellTextSINPE.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cellTextSINPE);

                    document.Add(table);
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
          
                //Prefalum
                if (CompanyCache.IdCompany == 111111111)
                {
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
                //Mercado del vidrio
                if (CompanyCache.IdCompany == 3102879949)
                {
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
                //Usuario Prueba
                if (CompanyCache.IdCompany == 999999999)
                {
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
                //MS
                if (CompanyCache.IdCompany == 204260627)
                {
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
                //Usuario Prueba
                if (CompanyCache.IdCompany == 3101704274)
                {
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
                //Viteco
                if (CompanyCache.IdCompany == 503320196)
                {
                    Paragraph CuentasParagraph = new Paragraph("CUENTAS", FontFactory.GetFont(FontFactory.HELVETICA, 12, 1, BaseColor.GRAY));
                    CuentasParagraph.Alignment = Element.ALIGN_CENTER;
                    document.Add(CuentasParagraph);

                    Paragraph Cuenta1Paragraph = new Paragraph("• Banco Popular IBAN  CR40011610012010001560", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    Cuenta1Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Cuenta1Paragraph);

                    Paragraph Cuenta2Paragraph = new Paragraph("• Banco Nacional IBAN  CR54015112720010041831", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    Cuenta2Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Cuenta2Paragraph);

                    Paragraph Cuenta3Paragraph = new Paragraph("• Sinpe Móvil 8751-7492 (Eduardo Alberto Salazar Vega)\r\n\r\n", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    Cuenta3Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Cuenta3Paragraph);
                }
                //Vitro Esparza
                if (CompanyCache.IdCompany == 3101623589 || CompanyCache.IdCompany == 3101623581)
                {
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
                //Vidriera Palmares
                if (CompanyCache.IdCompany == 222222222)
                {
                    Paragraph CuentasParagraph = new Paragraph("CUENTAS", FontFactory.GetFont(FontFactory.HELVETICA, 12, 1, BaseColor.GRAY));
                    CuentasParagraph.Alignment = Element.ALIGN_CENTER;
                    document.Add(CuentasParagraph);
                    Paragraph CuentasParagraph2 = new Paragraph("Banco Nacional", FontFactory.GetFont(FontFactory.HELVETICA, 10, 1, BaseColor.GRAY));
                    CuentasParagraph2.Alignment = Element.ALIGN_CENTER;
                    document.Add(CuentasParagraph2);

                    Paragraph Cuenta1Paragraph = new Paragraph("• Cuenta IBAN colones CR84015101910010039940", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    Cuenta1Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Cuenta1Paragraph);

                    Paragraph Cuenta2Paragraph = new Paragraph("• Cuenta IBAN dólares CR20015101920020050861 \r\n\r\n", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    Cuenta2Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Cuenta2Paragraph);

                    Paragraph Cuenta3Paragraph = new Paragraph("• Sinpe Móvil a nombre de Vidriera Palmares S.A 87091108 \r\n\r\n", FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK));
                    Cuenta3Paragraph.Alignment = Element.ALIGN_LEFT;
                    document.Add(Cuenta3Paragraph);
                }
                if (CompanyCache.IdCompany == 333333333)
                {
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


    }
}

