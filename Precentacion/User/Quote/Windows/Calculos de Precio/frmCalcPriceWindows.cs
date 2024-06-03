using Dominio.Model.ClassWindows;
using Negocio.LoadProduct;
using Precentacion.User.Quote.Quote;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;

namespace Precentacion.User.Quote.Windows
{
    public partial class frmCalcPriceWindows : Form
    {
        #region Variables
        N_LoadProduct loadProduct = new N_LoadProduct();
        decimal totalPrice = 0;
        decimal priceNewGlass = 0;
        decimal AuxpriceNewGlass = 0;
        decimal PrecioDescuento = 0;
        decimal SubTotal = 0;
        decimal TempTotal = 0;
        decimal TempDescuento = 0;
        string Lock = "";
        bool Calculator = false;
        private Timer timer = new Timer();
        string PanelState = "Close";
        bool Cedazo = false;
        string ControlFocus = "";
        string URL = "";
        bool AceptarAncho = false;
        bool AceptarAlto = false;

        // Relación de escala (1 metro = 1000 píxeles, 1 centímetro = 100 píxeles)
        private const decimal MetrosAPixeles = 1000.0m;
        private const decimal CentimetrosAPixeles = 100.0m;
        #endregion

        #region Constructor
        public frmCalcPriceWindows()
        {
            InitializeComponent();
            Initialize();
            timer.Tick += Timer_Tick;

            pbVentana.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            pbVentana.SizeMode = PictureBoxSizeMode.Zoom;
        }
        #endregion

        #region Mover Formulario
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private void lblTituloCotizacion_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void lblTituloCotizacion_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(diff));
            }
        }

        private void lblTituloCotizacion_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }


        #endregion

        #region Initialize Functions
        private void Initialize()
        {
            cbColor.SelectedIndex = 0;
            
            lblDescripcion.Text = "Sistema " + ClsWindows.System + " Con Diseño " + ClsWindows.Desing;
            if (ClsWindows.System == "8025 3 Vias" || ClsWindows.System == "8040 3 Vias" || ClsWindows.System == "6030 3 Vias")
            {
                //Añadir Texto al la Descripcion
                lblDescripcion.Text += " Con Cedazo";
            }
            cbImpacto.Checked = true;
            cbSupplier.SelectedIndex = 0;
            loadPicture();
            loadGlass();
            loadPanelLock();
            loadPanelHaladera();       
            MostrarOpcionesCedazo();
            ConfigPantalla();
            ConfPictureBox();


        }
        private void loadPanelHaladera()
        {
            if (ClsWindows.System == "Puerta Lujo")
            {
                PanelHaladera.Visible = true;
                cbkHaladeraConcha.Checked = true;
                cbkBaraaEmpuje.Checked = true;
                cbkCilindronPestillo.Checked = true;
                cbkContramarco.Checked = true;
                cbkBrazoHidraulico.Checked = true;
            }
        }
        private void loadPicture()
        {
            try
            {
                string path = System.Windows.Forms.Application.StartupPath + "\\Images\\Windows\\" + ClsWindows.Desing + cbColor.Text + ".jpeg";
                if (ClsWindows.System == "Puerta Liviana") 
                {
                    path = System.Windows.Forms.Application.StartupPath + "\\Images\\Windows\\Puerta Liviana" + ClsWindows.Desing + cbColor.Text + ".jpeg";
                }
                if (System.IO.File.Exists(path))
                {
                    pbVentana.Image = System.Drawing.Image.FromFile(path);
                    URL = path;
                }
                else
                {
                    MessageBox.Show("Color no disponible");
                    Console.WriteLine(path);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar la imagen");
            }
        }
        private void loadGlass() 
        { 
            DataTable dtVidrio = new DataTable();
            dtVidrio = loadProduct.loadOnlyGlass();
            cbVidrio.DataSource = dtVidrio;
            cbVidrio.DisplayMember = "Description";
            cbVidrio.ValueMember = "Description";
            cbVidrio.DataSource = dtVidrio;

            DataTable dtGlass = new DataTable();
            dtGlass = loadProduct.loadOnlyGlass();
            cbGlass.DataSource = dtGlass;
            cbGlass.DisplayMember = "Description";
            cbGlass.ValueMember = "Description";
            cbGlass.DataSource = dtGlass;
        }
        private void loadPanelLock() 
        {
            if (ClsWindows.System == "8025 2 Vias" || ClsWindows.System == "8025 3 Vias" || ClsWindows.System == "8040 2 Vias" || ClsWindows.System == "8040 3 Vias" || ClsWindows.System == "Europa 2 Vias Puerta" || ClsWindows.System == "Europa 3 Vias Puerta"|| ClsWindows.System == "Europa 2 Vias" || ClsWindows.System == "Europa 3 Vias")
            {
                panelCerradura.Visible = true;
                cbCilindro.Visible = false;

                if (ClsWindows.System == "Europa 2 Vias Puerta" || ClsWindows.System == "Europa 3 Vias Puerta")
                {   
                    cbCilindro.Visible = true;
                    cbPico.Text = "kid EU Multi con llave";
                }
                if (ClsWindows.System == "Europa 2 Vias" || ClsWindows.System == "Europa 3 Vias")
                {
                    cbPico.Text = "kid EU Multi con llave";
                    cbSPuesta.Visible = false;
                }

            }
        }    
        private void ConfPictureBox() 
        {
            if (ClsWindows.System != "Puerta Lujo" && ClsWindows.System != "8025 2 Vias" && ClsWindows.System != "8025 3 Vias" && ClsWindows.System != "8040 2 Vias" && ClsWindows.System != "8040 3 Vias" && ClsWindows.System != "Europa 2 Vias Puerta" && ClsWindows.System != "Europa 3 Vias Puerta" && ClsWindows.System != "Europa 2 Vias" && ClsWindows.System != "Europa 3 Vias") 
            {
                //Mover la Imagen Hacia Arriba
                Point pt = pbVentana.Location;
                pt.Y = 200;
                pbVentana.Location = pt;


            }
            else
            {
                if(ClsWindows.System != "Puerta Lujo") 
                {
                    //Mover la Imagen Hacia Arriba
                    Point pt = pbVentana.Location;
                    pt.Y = 300;
                    pbVentana.Location = pt;
                }
                else
                {
                    //Mover la Imagen Hacia Arriba
                    Point pt = PanelHaladera.Location;
                    pt.Y = 170;
                    PanelHaladera.Location = pt;
                }
            }
            if(ClsWindows.System == "Ventila" && ClsWindows.Desing != "1 Hoja Horizontal"&& ClsWindows.Desing != "2 Hoja Horizontal") 
            {
                pbVentana.Width = 400;
                pbVentana.Height = 100;
            }
            if (ClsWindows.System == "Puerta Liviana")
            {
                pbVentana.Width = 270;
                pbVentana.Height = 270;
            }
            if (ClsWindows.System == "Ventila" && ClsWindows.Desing == "2 Hoja Vertical" || ClsWindows.Desing == "1 Hoja 1 Fijo Vertical" || ClsWindows.Desing == "3 Hoja Vertical")
            {
                pbVentana.Width = 200;
                pbVentana.Height = 250;

                Point pt = pbVentana.Location;

                pt.X = 90;

                pbVentana.Location = pt;


            }
        }
        private void MostrarOpcionesCedazo() 
        {
            if (ClsWindows.System.Contains("3 Vias"))
            {
                lblPosicionCedazo.Visible = true;
                ckExterno.Visible = true;
                ckInterno.Visible = true;
            }
            else
            {
                lblPosicionCedazo.Visible = false;
                ckInterno.Visible = false;
                ckExterno.Visible = false;
            }
        }
        private void ConfigPantalla() 
        {
            if (ClsWindows.System == "Cedazo 1/2")
            {
                switch (ClsWindows.Desing)
                {
                    case "Cedazo 12":
                        lblDescripcion.Text = "Cedazo 1/2";
                        break;
                    case "Cedazo 1":
                        lblDescripcion.Text = "Cedazo 1";
                        break;
                    case "Cedazo 2":
                        lblDescripcion.Text = "Cedazo 2";
                        break;
                }
                lblVidrio.Visible = false;
                cbVidrio.Visible = false;
                cbVidrio.Text = "N/A";
                lblVidrioMetricas.Visible = false;
                cbCedazo.Visible = false;
                label10.Visible = false;
                PanelVidrioFijo.Visible = false;
            }
        }
       
        #endregion

        #region Buttons
        private void btnBackSistema_Click(object sender, EventArgs e)
        {

            switch (ClsWindows.System)
            {
                case "Ventila":
                    frmSelectDesingVentila frm = new frmSelectDesingVentila();
                    frm.Show();
                    this.Close();
                    break;
                case "Puerta Lujo":
                    frmSelecDesingPuertLujo frmpl = new frmSelecDesingPuertLujo();
                    frmpl.Show();
                    this.Close();
                    break;
                case "Cedazo 1/2":
                    frmSelectSystem frmce = new frmSelectSystem();
                    frmce.Show();
                    this.Close();
                    break;
                case "Puerta Liviana":
                    frmSelecDesingPuertLujo frmpu = new frmSelecDesingPuertLujo();
                    frmpu.Show();
                    this.Close();
                    break;
                default:
                    frmSelectDesing frmde = new frmSelectDesing();
                    frmde.Show();
                    this.Close();
                    break;
            }
        }
        private void btnDesglose_Click(object sender, EventArgs e)
        {
            PanelDetalle.Visible = true;
            PanelMedidas.Visible = false;
        }
        private void btnCargar_Click(object sender, EventArgs e)
        {
            if (pbVentana.Image != null)
            {
                try
                {

                    // Convertir las dimensiones ingresadas por el usuario a píxeles
                    decimal anchoEnMetros = decimal.Parse(txtAncho.Text);
                    decimal alturaEnMetros = decimal.Parse(txtAlto.Text);

                    int newWidth = (int)(anchoEnMetros * MetrosAPixeles);
                    int newHeight = (int)(alturaEnMetros * MetrosAPixeles);
                    //Redirecciona a la funcion
                    var resizedImage = ResizeImage(pbVentana.Image, newWidth, newHeight);
                    //La imagen que devuelve la funcion va a ser la nueva imagen del pictureBox
                    pbVentana.Image = resizedImage;
                }
                catch (FormatException)
                {
                    MessageBox.Show("Por favor, introduce valores válidos para el ancho y el alto.");
                }
            }
            else
            {
                MessageBox.Show("No hay ninguna imagen cargada en el PictureBox.");
            }



            Calculator = true;
            if (ValidarCampos())
            {
               
                DataTable dtAluminio = new DataTable();
                dtAluminio = loadProduct.loadAluminio(cbColor.Text, ClsWindows.System, cbSupplier.Text);
                dgAluminio.AutoGenerateColumns = true;
                dgAluminio.DataSource = dtAluminio;

                DataTable dtAccesorios = new DataTable();
                dtAccesorios = loadProduct.loadAccesorios(ClsWindows.System, cbSupplier.Text);
                dgAccesorios.AutoGenerateColumns = true;
                dgAccesorios.DataSource = dtAccesorios;

                DataTable dtVidrio = new DataTable();
                dtVidrio = loadProduct.loadPricesGlass(cbSupplier.Text, cbVidrio.Text);
                dgVidrio.AutoGenerateColumns = true;
                dgVidrio.DataSource = dtVidrio;

                DataTable dtLock = new DataTable();
                dtLock = loadProduct.LoadPricesLock(cbSupplier.Text, Lock);
                dgvCerradura.AutoGenerateColumns = true;
                dgvCerradura.DataSource = dtLock;

                if (cbCedazo.Checked == true)
                {
                    DataTable dtCedazoAluminio = new DataTable();
                    dtCedazoAluminio = loadProduct.LoadAluminioCedazo(cbColor.Text, "Cedazo", cbSupplier.Text);
                    foreach (DataRow row in dtCedazoAluminio.Rows)
                    {
                        dtAluminio.ImportRow(row);
                    }
                    dgAccesorios.DataSource = dtAluminio;

                    DataTable dtCedazoAccesorios = new DataTable();
                    dtCedazoAccesorios = loadProduct.loadAccesoriosCedazo("Cedazo", cbSupplier.Text);
                    //Agregar al dgvAccesorios sin eliminar los datos que ya tiene
                    foreach (DataRow row in dtCedazoAccesorios.Rows)
                    {
                        dtAccesorios.ImportRow(row);
                    }
                    dgAccesorios.DataSource = dtAccesorios;


                    
                }
              

                else
                {
                 
                    string descripcion = ClsWindows.System + ClsWindows.Desing + cbColor.Text;
                    if (ClsWindows.System == "CedazoAkari")
                    {
                        descripcion = ClsWindows.Desing + cbColor.Text;
                    }
                    if (ClsWindows.System == "Cedazo 1/2") 
                    {
                        descripcion = "Cedazo1/2"+ClsWindows.Desing + cbColor.Text;
                    }
                    SubTotal = loadProduct.CalcTotalPrice(dtAluminio, dtAccesorios, dtVidrio,dtLock,descripcion,cbSupplier.Text);
                   
                    txtTotal.Text = SubTotal.ToString("C");
                    if (ClsWindows.System == "Puerta Lujo")
                    {
                        QuitarArticulos();
                    }
                    
                }
            
            }
        }

        #region Función para redimensionar la imagen
        private Bitmap ResizeImage(Image image, int width, int height)
        {
            // Rectángulo de destino para la imagen redimensionada
            var destRect = new Rectangle(0, 0, width, height);
            // Crear un nuevo objeto Bitmap para la imagen redimensionada
            var destImage = new Bitmap(width, height);

            // Establecer la resolución del nuevo Bitmap igual a la resolución de la imagen original
            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            // Crear un objeto Graphics para la imagen redimensionada
            using (var graphics = Graphics.FromImage(destImage))
            {
                // Configurar la calidad de composición, interpolación, suavizado y compensación de píxeles para el objeto Graphics
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                // Configurar el modo de envoltura de imagen para el objeto Graphics
                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    // Dibujar la imagen original redimensionada en el rectángulo de destino utilizando el objeto Graphics
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            // Devolver la imagen redimensionada
            return destImage;
        }
        #endregion

        private void btnHidePanelDG_Click(object sender, EventArgs e)
        {
            PanelDetalle.Visible = false;
            PanelMedidas.Visible = true;
        }
        #endregion

        #region Insert Windows
        private string CreateDescription() 
        { 
            //crear descripcion de la ventana que incluya el sistema, diseño, color, vidrio separado por saltos de linea
            string description = "";
            description += "Sistema: " + ClsWindows.System + "\n";
            description += "Diseño: " + ClsWindows.Desing + "\n";
            if (ClsWindows.System == "8025 3 Vias" || ClsWindows.System == "8040 3 Vias" || ClsWindows.System == "6030 3 Vias")
            {
                //Añadir Texto al la Descripcion
                description += "Con Cedazo" + "\n";
            }
            if (ckInterno.Checked)
            {
               description += "Cedazo Interno" + "\n";
            }
            if (ckExterno.Checked)
            {
                description += "Cedazo Externo" + "\n";
            }
            description += "Color: " + cbColor.Text + "\n";
            description += "Vidrio: " + cbVidrio.Text + "\n";
            if (ClsWindows.System == "Ventila")
            {
                description += "Cerradura: VT " + "\n";
            }
            else
            {
                description += "Cerradura: " + ClsWindows.Lock + "\n";
            }        
            description += "Ancho: " +ClsWindows.Weight + "\n";
            description += "Alto: " + ClsWindows.heigt + "\n";
            if (txtAddWeigth.Text != "" && txtAddHeight.Text != "")
            {
                description += "Ancho Fijo: " + txtAddWeigth.Text + "\n";
                description += "Alto Fijo: " + txtAddHeight.Text + "\n";
            }
            //Añadir la Ubicacion
            if (txtUbicacion.Text != "")
            {
                description += "Ubicacion: " + txtUbicacion.Text + "\n";
            }
            //Añadir la cantidad 
            description += "Cantidad: " + NumCantidad.Value + "\n";
            return description;
        }
        private string CreateURL() 
        {
            //Obtener la ruta relativa de la Imagen de la ventana
            string url = "";
            if (cbUbicacion.Text != "")
            {             
                url = "Images\\Windows\\" + ClsWindows.Desing + cbColor.Text + "fijo" + cbUbicacion.Text + ".jpeg";              
            }
            else
            {
                url = "Images\\Windows\\" + ClsWindows.Desing + cbColor.Text + ".jpeg";        
            }
            return url;




        }
        private void SelectLock() 
        {
            if (panelCerradura.Visible == true)
            {
                if (cbPico.Checked)
                {
                    ClsWindows.Lock = cbPico.Text;
                }
                if (cbSPuesta.Checked)
                {
                    ClsWindows.Lock = cbSPuesta.Text;
                }
                if (cbImpacto.Checked)
                {
                    ClsWindows.Lock = cbImpacto.Text;
                }
            }
            else {ClsWindows.Lock = "Cerradura Impacto";}

        }
        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    if (MessageBox.Show("¿Desea agregar esta ventana?", "Agregar Ventana", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string Update = ClsWindows.Lock;
                        SelectLock();
                        string description = CreateDescription();
                        string url = CreateURL();
                        if (TempTotal != 0)
                        {
                            totalPrice = TempTotal;
                        }

                        if (Update == "Update")
                        {
                            if (loadProduct.EditWindows(ClsWindows.IdWindows.ToString(), description, URL, ClsWindows.Weight, ClsWindows.heigt, cbVidrio.Text, cbColor.Text, ClsWindows.Lock, totalPrice, ClsWindows.IDQuote, ClsWindows.System, ClsWindows.Desing))
                            {
                                //Actualize el data grid en la pantalla de cotizaciones
                                //primero verifique si el formulario esta abierto
                                Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is frmQuote);
                                if (frm != null)
                                {
                                    ((frmQuote)frm).loadWindows();
                                }
                                MessageBox.Show("Ventana Editada correctamente");
                                CleanController();
                            }
                            else
                            {
                                MessageBox.Show("Error al Editar la ventana");
                            }
                        }
                        else
                        {

                            if (loadProduct.insertWindows(description, URL, ClsWindows.Weight, ClsWindows.heigt, cbVidrio.Text, cbColor.Text, ClsWindows.Lock, totalPrice, ClsWindows.IDQuote, ClsWindows.System, ClsWindows.Desing))
                            {
                                //Actualize el data grid en la pantalla de cotizaciones
                                //primero verifique si el formulario esta abierto
                                Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is frmQuote);
                                if (frm != null)
                                {
                                    ((frmQuote)frm).loadWindows();
                                }
                                MessageBox.Show("Ventana agregada correctamente");
                                CleanController();

                            }
                            else
                            {
                                MessageBox.Show("Error al agregar la ventana");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

               MessageBox.Show("Error Inesperado "+ ex);
            }
           
        }
        #endregion

        #region Change Values
        private void cbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadPicture();
        }
        private void Values_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtAlto.Text != "")
                {
                    //Detectar si el usuario ingreso un punto en vez de una coma
                    DetectarPunto();
                    ClsWindows.heigt = Convert.ToDecimal(txtAlto.Text);
                   button2_Click(sender, e);


                }
                if (txtAncho.Text != "")
                {
                    //Detectar si el usuario ingreso un punto en vez de una coma
                    DetectarPunto();
                    ClsWindows.Weight = Convert.ToDecimal(txtAncho.Text);
                    button2_Click(sender, e);
                }
                Advertencias();
            }
            catch (Exception)
            {

            }
           

        }
        private void txtCantidad_ValueChanged(object sender, EventArgs e)
        { 
                try
                {
                    
                    SubTotal = (totalPrice);
                    txtTotal.Text = SubTotal.ToString("C");             
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error: " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                
        }
        private void cbPico_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPico.Checked && ClsWindows.System != "Europa 2 Vias Puerta")
            {
                Lock = "Cerradura Pico Lora con Jaladera Doble";
                cbImpacto.Checked = false;
                cbSPuesta.Checked = false;

            }
            if (cbPico.Checked && ClsWindows.System == "Europa 2 Vias Puerta" || ClsWindows.System == "Europa 3 Vias Puerta" || cbPico.Checked && ClsWindows.System == "Europa 2 Vias" || ClsWindows.System == "Europa 3 Vias")
            {
                Lock = "kid EU Multi con llave";
                cbImpacto.Checked = false;
                cbSPuesta.Checked = false;
            }
        }
        private void cbSPuesta_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSPuesta.Checked)
            {
                if (ClsWindows.Desing != "FijoMovilMovilFijo")
                {
                    Lock = "Cerradura SobrePuesta para FM";
                }
                else
                {
                    Lock = "Cerradura Sobre Puesta FMMF";
                }
                cbImpacto.Checked = false;
                cbPico.Checked = false;
            }
        }
        private void cbImpacto_CheckedChanged(object sender, EventArgs e)
        {
            if (cbImpacto.Checked)
            {
                Lock = "Cerradura Impacto Akari";
                cbPico.Checked = false;
                cbSPuesta.Checked = false;
            }
        }
       
        #endregion

        #region Support Functions
        private bool ValidarCampos()
        {
            if (txtAlto.Text == "")
            {
                MessageBox.Show("Debe ingresar el alto de la ventana");
                return false;
            }
            if (txtAncho.Text == "")
            {
                MessageBox.Show("Debe ingresar el ancho de la ventana");
                return false;
            }
            if (cbSupplier.Text == "")
            {
                MessageBox.Show("Debe seleccionar un proveedor");
                return false;
            }
            if (cbVidrio.Text == "")
            {
                MessageBox.Show("Debe seleccionar un vidrio");
                return false;
            }
            if (Calculator == true) 
            {
                Calculator = false;
                return true;
            }
            else
            {
                if (txtTotal.Text == "")
                {
                    MessageBox.Show("Debe Calcular el Precio de la ventana");
                    return false;
                }
            }
            
            return true;
        }   
        private void CleanController() 
        {
            txtAlto.Text = "";
            txtAncho.Text = "";
            txtTotal.Text = "";
            txtTotalPrice.Text = "";
            cbSupplier.Text = "";
            cbColor.Text = "";
            cbImpacto.Checked = true;
            cbPico.Checked = false;
            cbSPuesta.Checked = false;
            dgAluminio.DataSource = null;
            dgAccesorios.DataSource = null;
            dgVidrio.DataSource = null;
        }
        private void DetectarPunto()
        {
            if (txtAncho.Text.Contains("."))
            {
                txtAncho.Text = txtAncho.Text.Replace(".", ",");
                //Posicionar el cursor al final del texto
                txtAncho.SelectionStart = txtAncho.Text.Length;
            }
            if (txtAlto.Text.Contains("."))
            {
                txtAlto.Text = txtAlto.Text.Replace(".", ",");
                //Posicionar el cursor al final del texto
                txtAlto.SelectionStart = txtAlto.Text.Length;
            }
            if (txtAddHeight.Text.Contains("."))
            {
                txtAddHeight.Text = txtAddHeight.Text.Replace(".", ",");
                txtAddHeight.SelectionStart = txtAddHeight.Text.Length;
            }
            if (txtAddWeigth.Text.Contains("."))
            {
                txtAddWeigth.Text = txtAddWeigth.Text.Replace(".", ",");
                txtAddWeigth.SelectionStart = txtAddWeigth.Text.Length;
            }
        }

        #endregion

        #region KeyPress
        private void txtAncho_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Verificar Cuando se presiona la tecla Enter
            if (e.KeyChar == (char)13)
            {
                txtAlto.Focus();                     
            }
        }
        private void txtAlto_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Verificar Cuando se presiona la tecla Enter
            if (e.KeyChar == (char)13)
            {
                //Marcar el Boton del Campo que se esta llenando
                if (txtAlto.Focus() == true)
                {
                    txtAlto.BackColor = Color.Black;
                    txtAlto.ForeColor = Color.White;
                }
                else
                {
                    txtAlto.BackColor = Color.White;
                }
                cbColor.Focus();
            }
        }
        private void cbColor_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Verificar Cuando se presiona la tecla Enter
            if (e.KeyChar == (char)13)
            {
                cbVidrio.Focus();
            }
        }
        private void cbVidrio_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Verificar Cuando se presiona la tecla Enter
            if (e.KeyChar == (char)13)
            {
                cbCedazo.Focus();
            }
        }
        private void cbSupplier_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Verificar Cuando se presiona la tecla Enter
            if (e.KeyChar == (char)13)
            {
                btnCargar_Click(sender, e);
                btnInsertar_Click(sender, e);
            }
        }
        #endregion

        #region Add Windows
        #region Restricciones y Validaciones
        private void Restricciones()
        {
            if (cbGlass.SelectedIndex == 0 && ClsWindows.System != "5020")
            {
              DialogResult  res = MessageBox.Show("El Aluminio seleccionado no es compatible con el sistema seleccionado, ¿Desea continuar?", "Aluminio no compatible", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.No)
                {
                    cbGlass.SelectedIndex = 0;
                }
            }
        }
        private bool ValidateField() 
        { 
            //Validar que los campos del Panel panelAddglass esten llenos
            if (txtAddWeigth.Text == "")
            {
                MessageBox.Show("Debe ingresar el ancho del vidrio");
                return false;
            }
            if (txtAddHeight.Text == "")
            {
                MessageBox.Show("Debe ingresar el alto del vidrio");
                return false;
            }
            if (cbAluminio.Text == "")
            {
                MessageBox.Show("Debe seleccionar un aluminio");
                return false;
            }
            if (cbGlass.Text == "")
            {
                MessageBox.Show("Debe seleccionar un vidrio");
                return false;
            }
            if (cbSupplier.Text == "")
            {
                MessageBox.Show("Debe seleccionar un proveedor");
                return false;
            }
            return true;
        }
        #endregion

        #region Eventos
        private void cbAluminio_SelectedIndexChanged(object sender, EventArgs e)
        {
            Restricciones();
        }
        private void cbGlass_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }
        #endregion

        #region Functions
        private bool LoadAddGlass()
        {
            bool res = false;
            if (ValidateField())
            {
                try
                {
                    //Crear un DataTable para cargar los precios del vidrio Nuevo
                    DataTable dtGlass = new DataTable();

                    //Obtener Ancho y Alto del Vidrio
                    decimal Weigth = Convert.ToDecimal(txtAddWeigth.Text);
                    decimal Height = Convert.ToDecimal(txtAddHeight.Text);

                    //Cargar el DataTable con los precios del vidrio
                    dtGlass = loadProduct.LoadPriceNewGlass(cbSupplier.Text, cbGlass.Text, Weigth, Height);

                    // Validar si el DataTable está vacío
                    if (dtGlass.Rows.Count != 0)
                    {
                        // Verificar si el DataGridView ya tiene datos
                        dgVidrioAdd.AutoGenerateColumns = true;
                        dgVidrioAdd.DataSource = dtGlass;
                        res = true;
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron precios para el vidrio seleccionado");
                        res = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar el vidrio " + ex.Message);
                }
                
            }
            return res;

        }
        private bool LoadAddAluminio() 
        { 
            bool Res = false;
            //Validar Campos
            if (ValidateField())
            {
                //Crear un DataTable para cargar los precios del vidrio Nuevo
                DataTable dtAluminio = new DataTable();

                //Obtener Datos del Vidrio
                decimal Weigth = Convert.ToDecimal(txtAddWeigth.Text);
                decimal Height = Convert.ToDecimal(txtAddHeight.Text);
                string  Color = cbColor.Text;
                string  Supplier = cbSupplier.Text;

                //Cargar el DataTable con los precios del vidrio
                dtAluminio = loadProduct.LoadAluminioFijo(Color,"Vidrio Fijo",Supplier,Weigth,Height,cbAluminio.Text,Convert.ToInt16(txtDiviciones.Value));

                //Validar si el DataTable está vacío
                if (dtAluminio.Rows.Count != 0)
                {
                    // Verificar si el DataGridView ya tiene datos
                    
                        dgAluminioAdd.AutoGenerateColumns = true;
                        dgAluminioAdd.DataSource = dtAluminio;
                        Res = true;
                }
                else
                {
                    MessageBox.Show("No se encontraron precios para el vidrio seleccionado");
                    Res = false;
                }

            }
            return Res;
        }
        private void CalcPrice() 
        {
            priceNewGlass = 0;
            totalPrice = totalPrice - priceNewGlass;
            //Sumar el Precio de los DataGridView
            foreach (DataGridViewRow row in dgVidrioAdd.Rows)
            {
                priceNewGlass += Convert.ToDecimal(row.Cells["TotalPrice"].Value);
            }
            foreach (DataGridViewRow row in dgAluminioAdd.Rows)
            {
                priceNewGlass += Convert.ToDecimal(row.Cells["TotalPrice"].Value);
            }
            txtPecioFijo.Text = priceNewGlass.ToString("C");
        }
        #endregion

        #region Buttons
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (LoadAddGlass() && LoadAddAluminio())
            {
                CalcPrice();               
            }
        }
        #endregion
        #endregion

        #region PanelAddGlass
        private void cbUbicacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string path = System.Windows.Forms.Application.StartupPath + "\\Images\\Windows\\" + ClsWindows.Desing + cbColor.Text +txtDiviciones.Value.ToString()+ "fijo"+cbUbicacion.Text+".jpeg";
                if (System.IO.File.Exists(path))
                {
                    pbVentana.Image = System.Drawing.Image.FromFile(path);
                    URL = path;
                }
                else
                {
                    MessageBox.Show("Color no disponible");
                    Console.WriteLine(path);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar la imagen");
            }           
        }
        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            try
            {           
               totalPrice = SubTotal + priceNewGlass;
                if (txtDescuento.Text != "")
                {
                    decimal Descuento = Convert.ToDecimal(txtDescuento.Text) / 100;
                    totalPrice = totalPrice - (totalPrice * Descuento);
                }    
               txtTotalPrice.Text = totalPrice.ToString("C");
            }
            catch (Exception)
            {

            }
        }
       
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (PanelState == "Open")
            {
                // Si el panel está visible, lo ocultamos animadamente
                PanelVidrioFijo.Height -= 8;
                if (PanelVidrioFijo.Height <= 0)
                {
                    
                    timer.Stop();
                    PanelVidrioFijo.Visible = false;
                    PanelState = "Close";
                }
            }
            else
            {
                // Si el panel está oculto, lo mostramos animadamente
                PanelVidrioFijo.Visible = true;
                PanelVidrioFijo.Height += 8;
                if (PanelVidrioFijo.Height >= 355)
                {
                    //Cambiar la Imagen del btnOpen mediante una Uri
                    


                    timer.Stop();
                    PanelState = "Open";
                   
                }
            }
        }
        #endregion

        #region Focus and LostFocus
        private void txtAncho_Enter(object sender, EventArgs e)
        {
            ControlFocus = "TextAncho";
            txtAncho.BackColor = Color.Black;
            txtAncho.ForeColor = Color.White;
           
        }
        private void txtAncho_Leave(object sender, EventArgs e)
        {
            
            txtAncho.BackColor = Color.White;
            txtAncho.ForeColor = Color.Black;
          
        }
        private void txtAlto_Enter(object sender, EventArgs e)
        {
            ControlFocus = "TextAlto";
            txtAlto.BackColor = Color.Black;
            txtAlto.ForeColor = Color.White;
           
        }
        private void txtAlto_Leave(object sender, EventArgs e)
        {
           
            txtAlto.BackColor = Color.White;
            txtAlto.ForeColor = Color.Black;
           
        }
        private void cbColor_Enter(object sender, EventArgs e)
        {
            cbColor.BackColor = Color.Black;
            cbColor.ForeColor = Color.White;
            //Cambiar el Tamaño de Letra del cbVidrio
            cbColor.Font = new Font("Microsoft Sans Serif", 11);
            
        }
        private void cbColor_Leave(object sender, EventArgs e)
        {
            cbColor.BackColor = Color.White;
            cbColor.ForeColor = Color.Black;
            //Cambiar el Tamaño de Letra del cbVidrio
            cbColor.Font = new Font("Microsoft Sans Serif", 8);
        }
        private void cbVidrio_Enter(object sender, EventArgs e)
        {
            cbVidrio.BackColor = Color.Black;
            cbVidrio.ForeColor = Color.White;
            //Cambiar el Tamaño de Letra del cbVidrio
            cbVidrio.Font = new Font("Microsoft Sans Serif", 11);
        }
        private void cbVidrio_Leave(object sender, EventArgs e)
        {
            cbVidrio.BackColor = Color.White;
            cbVidrio.ForeColor = Color.Black;
            //Cambiar el Tamaño de Letra del cbVidrio
            cbVidrio.Font = new Font("Microsoft Sans Serif", 8);
        }
        private void cbSupplier_Enter(object sender, EventArgs e)
        {
            cbSupplier.BackColor = Color.Black;
            cbSupplier.ForeColor = Color.White;
            //Cambiar el Tamaño de Letra del cbVidrio
            cbSupplier.Font = new Font("Microsoft Sans Serif", 11);
        }
        private void cbSupplier_Leave(object sender, EventArgs e)
        {
            cbSupplier.BackColor = Color.White;
            cbSupplier.ForeColor = Color.Black;
            //Cambiar el Tamaño de Letra del cbVidrio
            cbSupplier.Font = new Font("Microsoft Sans Serif", 8);
        }
        #endregion

        private void cbCedazo_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCedazo.Checked == true)
            {
                Cedazo = true;
            }
            else
            {
                Cedazo = false;
            }
        }

        private void cbCedazo_Enter(object sender, EventArgs e)
        {
            cbCedazo.BackColor = Color.Black;
            cbCedazo.ForeColor = Color.White;
            //Cambiar el Tamaño de Letra del cbVidrio
            cbCedazo.Font = new Font("Microsoft Sans Serif", 11);
        }

        private void cbCedazo_Leave(object sender, EventArgs e)
        {
            cbCedazo.BackColor = Color.White;
            cbCedazo.ForeColor = Color.Black;
            //Cambiar el Tamaño de Letra del cbVidrio
            cbCedazo.Font = new Font("Microsoft Sans Serif", 8);
        }

        private void txtDiviciones_ValueChanged(object sender, EventArgs e)
        {
            cbUbicacion_SelectedIndexChanged(sender, e);
        }

        private void ButonNumeric_Click(object sender, EventArgs e)
        {
            //Obtener el Valor Tag del Boton
            int Valor = Convert.ToInt16(((Button)sender).Tag);

            //Cuando se Pulse el Boton agregar el Valor al textbox que esta seleccionado
            if (ControlFocus == "TextAncho")
            {
                txtAncho.Focus();
                txtAncho.Text += Valor.ToString();
            }
            if (ControlFocus == "TextAlto")
            {
                txtAlto.Focus();
                txtAlto.Text += Valor.ToString();
            }

        }

        private void buttonDeleteEfect_MouseHover(object sender, EventArgs e)
        {
            //cuando se seleccione cambiar el color del a trasparencia
            ((Button)sender).FlatStyle = FlatStyle.Flat;
            ((Button)sender).FlatAppearance.MouseDownBackColor = Color.Transparent;
            ((Button)sender).FlatAppearance.MouseOverBackColor = Color.Transparent;


        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtDescuento.Text != "")
                {
                    decimal Descuento = Convert.ToDecimal(txtDescuento.Text)/100;
                    PrecioDescuento = SubTotal - (SubTotal * Descuento);
                    SubTotal =  PrecioDescuento;
                    txtTotal.Text = PrecioDescuento.ToString("C");
                   
                }
                else
                {
                    txtTotal.Text = SubTotal.ToString("C");
                    PrecioDescuento = 0;
                }
       

            }
            catch (Exception)
            {
                MessageBox.Show("Datos Ingresados Erroneos","Datos Invalidos",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void cbMultConLLave_CheckedChanged(object sender, EventArgs e)
        {
            if (cbMultConLLave.Checked)
            {
                Lock = "kid EU Multi sin llave";
                cbImpacto.Checked = false;
                cbSPuesta.Checked = false;
                cbPico.Checked = false;
                cbCilindro.Checked = false;
            }
        }

        private void cbCilindro_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCilindro.Checked)
            {
                Lock = "Cerradura Multipunto con Cilindro";
                cbImpacto.Checked = false;
                cbSPuesta.Checked = false;
                cbPico.Checked = false;
                cbMultConLLave.Checked = false;
            }
        }

        private void QuitarArticulos() 
        {
            try
            {
                if (cbkHaladeraConcha.Checked == false)
                {
                    //Quitar del dgvAccesorios el Articulo 'Haladera Concha'
                    foreach (DataGridViewRow row in dgAccesorios.Rows)
                    {
                        //Validar si la Fila Esta Vacía
                        if (row.Cells["Description"].Value != null)
                        {
                            if (row.Cells["Description"].Value.ToString() == "Haladera Concha PL D41")
                            {
                                
                                //Quitar el Precio del Total
                                SubTotal = SubTotal - Convert.ToDecimal(row.Cells["TotalPrice"].Value);
                                txtTotal.Text = SubTotal.ToString("C");
                                //Quitar la Fila del DataGridView
                                dgAccesorios.Rows.Remove(row);

                            }
                        }
                    }
                }

                if(cbkBaraaEmpuje.Checked == false) 
                {
                    //Quitar del dgvAccesorios el Articulo 'Barra Empuje'
                    foreach (DataGridViewRow row in dgAluminio.Rows)
                    {
                        //Validar si la Fila Esta Vacía
                        if (row.Cells["Description"].Value != null)
                        {
                            if (row.Cells["Description"].Value.ToString() == "Barra de Empuje PL D42")
                            {
                                //Quitar el Precio del Total
                                SubTotal = SubTotal - Convert.ToDecimal(row.Cells["TotalPrice"].Value);
                                txtTotal.Text = SubTotal.ToString("C");
                                //Quitar la Fila del DataGridView
                                dgAluminio.Rows.Remove(row);
                            }
                        }
                    }
                }

                if (cbkCilindronPestillo.Checked == false)
                {
                    //Quitar del dgvAccesorios el Articulo 'Cilindro con Pestillo'
                    foreach (DataGridViewRow row in dgAccesorios.Rows)
                    {
                        //Validar si la Fila Esta Vacía
                        if (row.Cells["Description"].Value != null)
                        {
                            if (row.Cells["Description"].Value.ToString() == "Cilindro Tipo RIM RC051 PL")
                            {
                                //Quitar el Precio del Total
                                SubTotal = SubTotal - Convert.ToDecimal(row.Cells["TotalPrice"].Value);
                                txtTotal.Text = SubTotal.ToString("C");
                                //Quitar la Fila del DataGridView
                                dgAccesorios.Rows.Remove(row);
                            }
                        }
                    }
                }

                if (cbkBrazoHidraulico.Checked == false)
                {
                    //Quitar del dgvAccesorios el Articulo 'Cilindro'
                    foreach (DataGridViewRow row in dgAccesorios.Rows)
                    {
                        //Validar si la Fila Esta Vacía
                        if (row.Cells["Description"].Value != null)
                        {
                            if (row.Cells["Description"].Value.ToString() == "CIERRA PUERTA COMPACTO AL 303")
                            {
                                //Quitar el Precio del Total
                                SubTotal = SubTotal - Convert.ToDecimal(row.Cells["TotalPrice"].Value);
                                txtTotal.Text = SubTotal.ToString("C");
                                //Quitar la Fila del DataGridView
                                dgAccesorios.Rows.Remove(row);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
           
        }

        private void Advertencias()
        {
            #region Alto
            if (txtAlto.Text != "")
            {
                if (AceptarAlto == false)
                {
                    switch (ClsWindows.System)
                    {
                        case "5020":
                            if (ClsWindows.heigt >= 1.61m)
                            {
                                MessageBox.Show("El alto maximo para este sistema es de 1.60 Metros");
                                AceptarAlto = true;
                            }
                            break;
                        case "5020 3 Vias":
                            if (ClsWindows.heigt >= 1.61m)
                            {
                                MessageBox.Show("El alto maximo para este sistema es de 1.60 Metros");
                                AceptarAlto = true;
                            }
                            break;
                        case "Ventila":
                            if (ClsWindows.heigt >= 1.15m)
                            {
                                MessageBox.Show("El alto maximo para este sistema es de 1.15 Metros");
                                AceptarAlto = true;
                            }
                            break;
                        case "8025 2 Vias":
                            if (ClsWindows.heigt >= 2.35m)
                            {
                                MessageBox.Show("El alto maximo para este sistema es de 2.35 Metros");
                                AceptarAlto = true;
                            }
                            break;
                        case "8025 3 Vias":
                            if (ClsWindows.heigt >= 2.35m)
                            {
                                MessageBox.Show("El alto maximo para este sistema es de 2.35 Metros");
                                AceptarAlto = true;
                            }
                            break;
                        case "8040 2 Vias":
                            if (ClsWindows.heigt >= 2.35m)
                            {
                                MessageBox.Show("El alto maximo para este sistema es de 2.35 Metros");
                                AceptarAlto = true;
                            }
                            break;
                        case "8040 3 Vias":
                            if (ClsWindows.heigt >= 2.35m)
                            {
                                MessageBox.Show("El alto maximo para este sistema es de 2.35 Metros");
                                AceptarAlto = true;
                            }
                            break;
                        case "6030 2 Vias":
                            if (ClsWindows.heigt >= 1.85m)
                            {
                                MessageBox.Show("El alto maximo para este sistema es de 1.85 Metros");
                                AceptarAlto = true;
                            }
                            break;
                        case "6030 3 Vias":
                            if (ClsWindows.heigt >= 1.85m)
                            {
                                MessageBox.Show("El alto maximo para este sistema es de 1.85 Metros");
                                AceptarAlto = true;
                            }
                            break;
                        case "CedazoAkari":
                            if (ClsWindows.heigt >= 2.35m)
                            {
                                MessageBox.Show("El alto maximo para este sistema es de 2.35 Metros");
                                AceptarAlto = true;
                            }
                            break;
                        case "Europa 2 Vias":
                            if (ClsWindows.heigt >= 3.00m)
                            {
                                MessageBox.Show("El alto maximo para este sistema es de 3 Metros");
                                AceptarAlto = true;
                            }
                            break;
                        case "Europa 3 Vias":
                            if (ClsWindows.heigt >= 3.00m)
                            {
                                MessageBox.Show("El alto maximo para este sistema es de 3 Metros");
                                AceptarAlto = true;
                            }
                            break;
                        case "Europa 2 Vias Puerta":
                            if (ClsWindows.heigt >= 3.00m)
                            {
                                MessageBox.Show("El alto maximo para este sistema es de 3 Metros");
                                AceptarAlto = true;
                            }
                            break;
                        case "Europa 3 Vias Puerta":
                            if (ClsWindows.heigt >= 3.00m)
                            {
                                MessageBox.Show("El alto maximo para este sistema es de 3 Metros");
                                AceptarAlto = true;
                            }
                            break;
                        case "Puerta Lujo":
                            if (ClsWindows.heigt >= 2.35m)
                            {
                                MessageBox.Show("El alto maximo para este sistema es de 2.35 Metros");
                                AceptarAlto = true;
                            }
                            break;
                        case "Puerta Baño":
                            if (ClsWindows.heigt >= 1.83m)
                            {
                                MessageBox.Show("El alto maximo para este sistema es de 1.83 Metros");
                                AceptarAlto = true;
                            }
                            break;
                        case "Puerta Liviana":
                            if (ClsWindows.heigt >= 2.30m)
                            {
                                MessageBox.Show("El alto maximo para este sistema es de 2.30 Metros");
                                AceptarAlto = true;
                            }
                            break;
                        case "PuertaEuAbatible":
                            if (ClsWindows.heigt >= 3m)
                            {
                                MessageBox.Show("El alto maximo para este sistema es de 3 Metros");
                                AceptarAlto = true;
                            }
                            break;
                    }
                }

            }
            #endregion

            #region Ancho
            if (txtAncho.Text != "")
            {
                if (AceptarAncho == false)
                {
                    switch (ClsWindows.System)
                    {
                        case "5020":
                            switch (ClsWindows.Desing)
                            {
                                case "FijoMovil":
                                    if (ClsWindows.Weight >= 2.01m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 2 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "MovilMovil":
                                    if (ClsWindows.Weight >= 1.61m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 2 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "FijoMovilFijo":
                                    if (ClsWindows.Weight >= 3.61m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 3.60 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "MovilFijoMovil":
                                    if (ClsWindows.Weight >= 4.90m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 4.90 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "FijoMovilMovilFijo":
                                    if (ClsWindows.Weight >= 4.80m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 4.80 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                            }
                            break;
                        case "5020 3 Vias":
                            switch (ClsWindows.Desing)
                            {
                                case "FijoMovil":
                                    if (ClsWindows.Weight >= 2.01m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 2 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "MovilMovil":
                                    if (ClsWindows.Weight >= 1.61m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 2 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "FijoMovilFijo":
                                    if (ClsWindows.Weight >= 3.61m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 3.60 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "MovilFijoMovil":
                                    if (ClsWindows.Weight >= 4.90m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 4.90 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "FijoMovilMovilFijo":
                                    if (ClsWindows.Weight >= 4.80m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 4.80 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                            }
                            break;
                        case "Ventila":
                            switch (ClsWindows.Desing)
                            {
                                //1.15 por Hoja
                                case "1 Hoja Horizontal":
                                    if (ClsWindows.Weight >= 1.15m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 1.15 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "2 Hoja Horizontal":
                                    if (ClsWindows.Weight >= 2.30m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 2.30 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "3 Hoja Horizontal":
                                    if (ClsWindows.Weight >= 3.45m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 3.45 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "4 Hoja Horizontal":
                                    if (ClsWindows.Weight >= 4.60m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 4.60 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "5 Hoja Horizontal":
                                    if (ClsWindows.Weight >= 5.75m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 5.75 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "6 Hoja Horizontal":
                                    if (ClsWindows.Weight >= 6.90m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 6.90 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;


                            }
                            break;
                        case "8025 2 Vias":
                            switch (ClsWindows.Desing)
                            {
                                //1.35 por Hoja
                                case "FijoMovil":
                                    if (ClsWindows.Weight >= 2.70m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 2.70 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "MovilMovil":
                                    if (ClsWindows.Weight >= 2.70m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 2.70 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "FijoMovilFijo":
                                    if (ClsWindows.Weight >= 4.05m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 4.05 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "MovilFijoMovil":
                                    if (ClsWindows.Weight >= 5.40m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 5.40 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "FijoMovilMovilFijo":
                                    if (ClsWindows.Weight >= 5.40m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 5.40 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;

                            }
                            break;
                        case "8025 3 Vias":
                            switch (ClsWindows.Desing)
                            {
                                //1.35 por Hoja
                                case "FijoMovil":
                                    if (ClsWindows.Weight >= 2.70m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 2.70 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "MovilMovil":
                                    if (ClsWindows.Weight >= 2.70m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 2.70 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "FijoMovilFijo":
                                    if (ClsWindows.Weight >= 4.05m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 4.05 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "MovilFijoMovil":
                                    if (ClsWindows.Weight >= 5.40m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 5.40 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "FijoMovilMovilFijo":
                                    if (ClsWindows.Weight >= 5.40m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 5.40 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;

                            }
                            break;
                        case "8040 2 Vias":
                            switch (ClsWindows.Desing)
                            {
                                //1.35 por Hoja
                                case "FijoMovil":
                                    if (ClsWindows.Weight >= 2.70m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 2.70 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "MovilMovil":
                                    if (ClsWindows.Weight >= 2.70m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 2.70 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "FijoMovilFijo":
                                    if (ClsWindows.Weight >= 4.05m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 4.05 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "MovilFijoMovil":
                                    if (ClsWindows.Weight >= 5.40m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 5.40 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "FijoMovilMovilFijo":
                                    if (ClsWindows.Weight >= 5.40m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 5.40 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;

                            }
                            break;
                        case "8040 3 Vias":
                            switch (ClsWindows.Desing)
                            {
                                //1.35 por Hoja
                                case "FijoMovil":
                                    if (ClsWindows.Weight >= 2.70m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 2.70 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "MovilMovil":
                                    if (ClsWindows.Weight >= 2.70m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 2.70 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "FijoMovilFijo":
                                    if (ClsWindows.Weight >= 4.05m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 4.05 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "MovilFijoMovil":
                                    if (ClsWindows.Weight >= 5.40m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 5.40 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "FijoMovilMovilFijo":
                                    if (ClsWindows.Weight >= 5.40m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 5.40 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;

                            }
                            break;
                        case "6030 2 Vias":
                            switch (ClsWindows.Desing)
                            {
                                //1.35 por Hoja
                                case "FijoMovil":
                                    if (ClsWindows.Weight >= 2.70m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 2.70 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "MovilMovil":
                                    if (ClsWindows.Weight >= 2.70m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 2.70 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "FijoMovilFijo":
                                    if (ClsWindows.Weight >= 4.05m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 4.05 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "MovilFijoMovil":
                                    if (ClsWindows.Weight >= 5.40m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 5.40 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "FijoMovilMovilFijo":
                                    if (ClsWindows.Weight >= 5.40m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 5.40 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;

                            }
                            break;
                        case "6030 3 Vias":
                            switch (ClsWindows.Desing)
                            {
                                //1.35 por Hoja
                                case "FijoMovil":
                                    if (ClsWindows.Weight >= 2.70m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 2.70 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "MovilMovil":
                                    if (ClsWindows.Weight >= 2.70m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 2.70 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "FijoMovilFijo":
                                    if (ClsWindows.Weight >= 4.05m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 4.05 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "MovilFijoMovil":
                                    if (ClsWindows.Weight >= 5.40m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 5.40 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "FijoMovilMovilFijo":
                                    if (ClsWindows.Weight >= 5.40m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 5.40 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;

                            }
                            break;
                        case "CedazoAkari":
                            switch (ClsWindows.Desing)
                            {
                                //1.35 por Hoja
                                case "FijoMovil":
                                    if (ClsWindows.Weight >= 2.70m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 2.70 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "MovilMovil":
                                    if (ClsWindows.Weight >= 2.70m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 2.70 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "FijoMovilFijo":
                                    if (ClsWindows.Weight >= 4.05m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 4.05 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "MovilFijoMovil":
                                    if (ClsWindows.Weight >= 5.40m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 5.40 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "FijoMovilMovilFijo":
                                    if (ClsWindows.Weight >= 5.40m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 5.40 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;

                            }
                            break;
                        case "Europa 2 Vias":
                            switch (ClsWindows.Desing)
                            {
                                //1.60 por Hoja
                                case "FijoMovil":
                                    if (ClsWindows.Weight >= 3.20m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 3.20 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "MovilMovil":
                                    if (ClsWindows.Weight >= 3.20m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 3.20 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "FijoMovilFijo":
                                    if (ClsWindows.Weight >= 4.80m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 4.80 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "MovilFijoMovil":
                                    if (ClsWindows.Weight >= 4.80m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 6.40 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "FijoMovilMovilFijo":
                                    if (ClsWindows.Weight >= 6.40m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 6.40 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;


                            }
                            break;
                        case "Europa 3 Vias":
                            switch (ClsWindows.Desing)
                            {
                                //1.60 por Hoja
                                case "FijoMovil":
                                    if (ClsWindows.Weight >= 3.20m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 3.20 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "MovilMovil":
                                    if (ClsWindows.Weight >= 3.20m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 3.20 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "FijoMovilFijo":
                                    if (ClsWindows.Weight >= 4.80m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 4.80 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "MovilFijoMovil":
                                    if (ClsWindows.Weight >= 4.80m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 6.40 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "FijoMovilMovilFijo":
                                    if (ClsWindows.Weight >= 6.40m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 6.40 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;


                            }
                            break;
                        case "Europa 2 Vias Puerta":
                            switch (ClsWindows.Desing)
                            {
                                //1.60 por Hoja
                                case "FijoMovil":
                                    if (ClsWindows.Weight >= 3.20m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 3.20 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "MovilMovil":
                                    if (ClsWindows.Weight >= 3.20m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 3.20 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "FijoMovilFijo":
                                    if (ClsWindows.Weight >= 4.80m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 4.80 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "MovilFijoMovil":
                                    if (ClsWindows.Weight >= 4.80m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 6.40 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "FijoMovilMovilFijo":
                                    if (ClsWindows.Weight >= 6.40m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 6.40 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;


                            }
                            break;
                        case "Europa 3 Vias Puerta":
                            switch (ClsWindows.Desing)
                            {
                                //1.60 por Hoja
                                case "FijoMovil":
                                    if (ClsWindows.Weight >= 3.20m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 3.20 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "MovilMovil":
                                    if (ClsWindows.Weight >= 3.20m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 3.20 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "FijoMovilFijo":
                                    if (ClsWindows.Weight >= 4.80m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 4.80 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "MovilFijoMovil":
                                    if (ClsWindows.Weight >= 4.80m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 6.40 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "FijoMovilMovilFijo":
                                    if (ClsWindows.Weight >= 6.40m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 6.40 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;


                            }
                            break;
                        case "Puerta Lujo":
                            switch (ClsWindows.Desing)
                            {
                                //1.25 por Hoja PL
                                case "1 Hoja PL":
                                    if (ClsWindows.Weight >= 1.25m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 1.25 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "2 Hoja PL":
                                    if (ClsWindows.Weight >= 2.50m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 2.50 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "1 Hoja Con Divicion PL":
                                    if (ClsWindows.Weight >= 1.25m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 1.25 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "2 Hoja Con Divicion PL":
                                    if (ClsWindows.Weight >= 2.50m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 2.50 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;

                            }
                            break;
                        case "Puerta Baño":
                            switch (ClsWindows.Desing)
                            {
                                //1.22 por Hoja 
                                case "MovilMovil":
                                    if (ClsWindows.Weight >= 2.44m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 2.44 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "FijoMovilMovil":
                                    if (ClsWindows.Weight >= 3.66m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 3.66 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;


                            }
                            break;
                        case "Puerta Liviana":
                            switch (ClsWindows.Desing)
                            {
                                //1.00 por Hoja PL
                                case "1 Hoja PL":
                                    if (ClsWindows.Weight >= 1.00m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 1.00 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "2 Hoja PL":
                                    if (ClsWindows.Weight >= 2.00m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 2.00 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "1 Hoja Con Divicion PL":
                                    if (ClsWindows.Weight >= 1.00m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 1.00 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "2 Hoja Con Divicion PL":
                                    if (ClsWindows.Weight >= 2.00m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 2.00 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;


                            }
                            break;
                        case "PuertaEuAbatible":
                            switch (ClsWindows.Desing)
                            {
                                //1.20 por Hoja PL
                                case "1 Hoja PL":
                                    if (ClsWindows.Weight >= 1.20m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 1.20 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "2 Hoja PL":
                                    if (ClsWindows.Weight >= 2.40m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 2.40 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "1 Hoja Con Divicion PL":
                                    if (ClsWindows.Weight >= 1.20m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 1.20 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "2 Hoja Con Divicion PL":
                                    if (ClsWindows.Weight >= 2.40m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 2.40 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                            }
                            break;
                    }



                }
            }

            #endregion
        }

        private void NumCantidad_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (NumCantidad.Value > 0)
                {
                    TempTotal = 0;
                    TempTotal = (totalPrice) * Convert.ToDecimal(NumCantidad.Value);
                    txtTotalPrice.Text = TempTotal.ToString("C");
                }
               
            }
            catch (Exception)
            { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pbVentana.Image != null)
            {
                try
                {

                    // Convertir las dimensiones ingresadas por el usuario a píxeles
                    decimal anchoEnMetros = decimal.Parse(txtAncho.Text);
                    decimal alturaEnMetros = decimal.Parse(txtAlto.Text);

                    int newWidth = (int)(anchoEnMetros * MetrosAPixeles);
                    int newHeight = (int)(alturaEnMetros * MetrosAPixeles);
                    //Redirecciona a la funcion
                    var resizedImage = ResizeImage(pbVentana.Image, newWidth, newHeight);
                    //La imagen que devuelve la funcion va a ser la nueva imagen del pictureBox
                    pbVentana.Image = resizedImage;
                }
                catch (FormatException)
                {
                  
                }
            }
            else
            {
                MessageBox.Show("No hay ninguna imagen cargada en el PictureBox.");
            }
        }

        private void añadirVidrioFijoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAdd_Click(sender, e);
        }

        private void cotizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnCargar_Click(sender, e);
        }

        private void verDesgloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnDesglose_Click(sender, e);
        }

        private void enviarProformaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnInsertar_Click(sender, e);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }      
}
