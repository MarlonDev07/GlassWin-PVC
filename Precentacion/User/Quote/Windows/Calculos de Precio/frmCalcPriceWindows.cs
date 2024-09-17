using Dominio.Model.ClassWindows;
using Negocio.LoadProduct;
using Negocio.Proveedor;
using Precentacion.User.Quote.Quote;
using Precentacion.User.Quote.Windows.Calculos_de_Precio;
using Precentacion.User.Quote.Windows.Seleccion_Diseño;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;

namespace Precentacion.User.Quote.Windows
{
    public partial class frmCalcPriceWindows : Form
    {
        #region Variables
        N_LoadProduct loadProduct = new N_LoadProduct();
        public decimal totalPrice = 0;
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
        public int NumCantidad = 1;
        decimal AjustePrecio = 0;
        DataTable dtAluminio2 = new DataTable();
        DataTable dtGlass2 = new DataTable();
        string pathVidrioFijo;

        // Relación de escala (1 metro = 1000 píxeles, 1 centímetro = 100 píxeles)
        private const decimal MetrosAPixeles = 1000.0m;
        private const decimal CentimetrosAPixeles = 1500.0m;

        // Tamaño máximo permitido para el PictureBox
        private const int MaxWidth = 450;
        private const int MaxHeight = 350;
        decimal anchoT;
        decimal altoT;
        #endregion

        #region Constructor
        public frmCalcPriceWindows()
        {
            try
            {
                InitializeComponent();
                Initialize();
                timer.Tick += Timer_Tick;

                pbVentana.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                pbVentana.SizeMode = PictureBoxSizeMode.Zoom;
                // Agregar nuevas opciones al ComboBox
                cbUbicacion.Items.Add("Superior Corredizo");
                cbUbicacion.Items.Add("Inferior Corredizo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

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
            try
            {
                cbColor.SelectedIndex = 0;

                lblDescripcion.Text = "Sistema " + ClsWindows.System + " Con Diseño " + ClsWindows.Desing;
                if (ClsWindows.System == "8025 3 Vias" || ClsWindows.System == "8040 3 Vias" || ClsWindows.System == "6030 3 Vias")
                {
                    //Añadir Texto al la Descripcion
                    lblDescripcion.Text += " Con Cedazo";
                }
                cbImpacto.Checked = true;
                loadPicture();
                loadGlass();
                loadPanelLock();
                loadPanelHaladera();
                MostrarOpcionesCedazo();
                ConfigPantalla();
                ConfPictureBox();
                CargarProveedor();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



        }
        private void loadPanelHaladera()
        {
            try
            {
                if (ClsWindows.System == "Puerta Lujo")
                {
                    PanelHaladera.Visible = true;
                    cbkHaladeraConcha.Checked = true;
                    cbkBaraaEmpuje.Checked = true;
                    cbkCilindronPestillo.Checked = false;
                    cbkContramarco.Checked = false;
                    cbkBrazoHidraulico.Checked = false;
                    cbkUmbral.Checked = true;
                    cbkPH206.Checked = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void loadPicture()
        {
            try
            {
                string path = System.Windows.Forms.Application.StartupPath + "\\Images\\Windows\\" + ClsWindows.Desing + cbColor.Text + ".jpeg";
                /*if (ClsWindows.System == "Puerta Liviana")
                {
                    path = System.Windows.Forms.Application.StartupPath + "\\Images\\Windows\\Puerta Liviana" + ClsWindows.Desing + cbColor.Text + ".jpeg";
                }*/
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
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void loadPanelLock()
        {
            try
            {
                if (ClsWindows.System == "8025 2 Vias" || ClsWindows.System == "8025 3 Vias" || ClsWindows.System == "8040 2 Vias" || ClsWindows.System == "8040 3 Vias" || ClsWindows.System == "Europa 2 Vias Puerta" || ClsWindows.System == "Europa 3 Vias Puerta" || ClsWindows.System == "Europa 2 Vias" || ClsWindows.System == "Europa 3 Vias")
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void ConfPictureBox()
        {
            try
            {
                if (ClsWindows.System != "Ventila" && ClsWindows.System != "Puerta Lujo" && ClsWindows.System != "8025 2 Vias" && ClsWindows.System != "8025 3 Vias" && ClsWindows.System != "8040 2 Vias" && ClsWindows.System != "8040 3 Vias" && ClsWindows.System != "Europa 2 Vias Puerta" && ClsWindows.System != "Europa 3 Vias Puerta" && ClsWindows.System != "Europa 2 Vias" && ClsWindows.System != "Europa 3 Vias")
                {
                    //Mover la Imagen Hacia Arriba
                    Point pt = pbVentana.Location;
                    pt.Y = 200;
                    pbVentana.Location = pt;


                }
                else
                {
                    if (ClsWindows.System != "Ventila" && ClsWindows.System != "Puerta Lujo")
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
                if (ClsWindows.System == "Ventila" && ClsWindows.Desing != "1 Hoja Horizontal" && ClsWindows.Desing != "2 Hoja Horizontal")
                {
                    pbVentana.Width = 400;
                    pbVentana.Height = 250;//100;
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void MostrarOpcionesCedazo()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void ConfigPantalla()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        #endregion

        #region Buttons
        private void btnBackSistema_Click(object sender, EventArgs e)
        {
            try
            {
                if (PanelDetalle.Visible == true)
                {
                    MessageBox.Show("Pulse el botón 'Cerrar' en la parte inferior de este formulario.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
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
                            frmpu.Text = "Seleccionar diseño puerta liviana";
                            frmpu.system2 = "Puerta Liviana";
                            frmpu.Show();
                            this.Close();
                            break;
                        case "Ventila Euro":
                            frmSelectDesingVentila frm2 = new frmSelectDesingVentila();
                            frm2.Show();
                            this.Close();
                            break;
                        case "PuertaEuAbatible":
                            ClsWindows.System = "PuertaEuAbatible";
                            frmSelecDesingPuertLujo frm3 = new frmSelecDesingPuertLujo();
                            frm3.system2 = ClsWindows.System;
                            frm3.Show();
                            this.Close();
                            break;
                        default:
                            switch (ClsWindows.Desing)
                            {
                                case "CedazoAkariFijoMovil":
                                    frmSelecDesingCedazo frmCedazo = new frmSelecDesingCedazo();
                                    frmCedazo.Show();
                                    this.Close();
                                    return;
                                case "CedazoAkariFijoMovilMovilFijo":
                                    frmSelecDesingCedazo frmCedazo2 = new frmSelecDesingCedazo();
                                    frmCedazo2.Show();
                                    this.Close();
                                    return;
                            }
                            frmSelectDesing frmde = new frmSelectDesing();
                            frmde.Show();
                            this.Close();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }
        private void btnDesglose_Click(object sender, EventArgs e)
        {
            try
            {
                PanelDetalle.Visible = true;
                PanelMedidas.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void btnCargar_Click(object sender, EventArgs e)
        {
            if (pbVentana.Image != null)
            {
                try
                {

                    // Obtener las dimensiones desde ClsWindows
                    decimal anchoEnPixeles = ConvertirDimensionAPixeles(ClsWindows.Weight.ToString());
                    decimal alturaEnPixeles = ConvertirDimensionAPixeles(ClsWindows.heigt.ToString());

                    int newWidth = (int)anchoEnPixeles;
                    int newHeight = (int)alturaEnPixeles;

                    // Redimensionar la imagen
                    var resizedImage = ResizeImage(pbVentana.Image, newWidth, newHeight);

                    // Asignar la imagen redimensionada al PictureBox
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

            try
            {
                Calculator = true;
                if (ValidarCampos())
                {
                    DataTable dtAluminio = new DataTable();
                    loadProduct.CedazoValor(Cedazo);
                    dtAluminio = loadProduct.loadAluminio(cbColor.Text, ClsWindows.System, cbSupplier.Text);

                    dgAluminio.AutoGenerateColumns = true;
                    dgAluminio.DataSource = dtAluminio;

                    DataTable dtAccesorios = new DataTable();
                    dtAccesorios = loadProduct.loadAccesorios(ClsWindows.System, cbSupplier.Text);
                    dgAccesorios.AutoGenerateColumns = true;
                    dgAccesorios.DataSource = dtAccesorios;

                    DataTable dtVidrio = new DataTable();
                    string proveedorVidrio = cbSupplier.Text;

                    if (cbVidrio.Text == "Vid 4 mm claro Alu")
                    {
                        proveedorVidrio = "Aluma";
                    }
                    else if (cbVidrio.Text.EndsWith("Ex"))
                    {
                        proveedorVidrio = "Extralum";
                    }
                    else if (cbVidrio.Text.EndsWith("Alu"))
                    {
                        proveedorVidrio = "Alumas";
                    }
                    else if (cbVidrio.Text.EndsWith("Ma"))
                    {
                        proveedorVidrio = "Macopa";
                    }

                    dtVidrio = loadProduct.loadPricesGlass(proveedorVidrio, cbVidrio.Text);
                    dgVidrio.AutoGenerateColumns = true;
                    dgVidrio.DataSource = dtVidrio;

                    DataTable dtLock = new DataTable();
                    dtLock = loadProduct.LoadPricesLock(cbSupplier.Text, Lock);
                    dgvCerradura.AutoGenerateColumns = true;
                    dgvCerradura.DataSource = dtLock;

                    // Comentado el código de Cedazo por ahora

                    // Realiza los cálculos y ajustes de precios
                    string descripcion = ClsWindows.System + ClsWindows.Desing + cbColor.Text;
                    if (ClsWindows.System == "CedazoAkari")
                    {
                        descripcion = ClsWindows.Desing + cbColor.Text;
                    }
                    if (ClsWindows.System == "Cedazo 1/2")
                    {
                        descripcion = "Cedazo1/2" + ClsWindows.Desing + cbColor.Text;
                    }
                    if (ClsWindows.System == "Puerta Lujo")
                    {
                        QuitarArticulos();
                    }
                    if (ClsWindows.System == "5020" || ClsWindows.System == "6030 2 Vias")
                    {
                        QuitarArticulos();
                    }


                    SubTotal = loadProduct.CalcTotalPrice(dtAluminio, dtAccesorios, dtVidrio, dtLock, descripcion, cbSupplier.Text);

                    AjustePrecio = loadProduct.LoadAjustePrecio(cbSupplier.Text, descripcion);

                    txtTotal.Text = SubTotal.ToString("C");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        #region Función para redimensionar la imagen
        private Bitmap ResizeImage(Image image, int width, int height)
        {
            // try {
            // Crear un nuevo Bitmap con el tamaño deseado
            var destImage = new Bitmap(width, height);

            // Establecer la resolución del nuevo Bitmap igual a la resolución de la imagen original
            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            // Usar Graphics para dibujar la imagen redimensionada
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
                    graphics.DrawImage(image, new Rectangle(0, 0, width, height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            // Devolver la imagen redimensionada
            return destImage;

            // }
            // catch (Exception ex) {
            // MessageBox.Show("Error: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //return destImage;
            // }



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

            try
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
                    if (ClsWindows.System == "PuertaEuAbatible")
                    {
                        description += "Cerradura: " + "Cerradura Doble Manija Europa" + "\n";
                    }
                    if (ClsWindows.System == "Puerta Lujo")
                    {
                        description += "Cerradura: " + "Cerradura Completa" + "\n";
                    }

                    else
                    {
                        description += "Cerradura: " + ClsWindows.Lock + "\n";
                    }
                }
                description += "Ancho: " + ClsWindows.Weight + "\n";
                description += "Alto: " + ClsWindows.heigt + "\n";



                // Procesar txtAlto
                if (txtAddHeight.Text != "")
                {


                    decimal alto = Convert.ToDecimal(txtAddHeight.Text);

                    // Convertir a metros si el valor es mayor o igual a 1000
                    alto /= 1000;

                    // Detectar si el usuario ingresó un punto en vez de una coma
                    DetectarPunto();
                    altoT = alto;
                    //button2_Click(sender, e);
                }

                // Procesar txtAncho
                if (txtAddWeigth.Text != "")
                {


                    decimal ancho = Convert.ToDecimal(txtAddWeigth.Text);

                    // Convertir a metros si el valor es mayor o igual a 1000
                    ancho /= 1000;

                    // Detectar si el usuario ingresó un punto en vez de una coma
                    DetectarPunto();
                    anchoT = ancho;
                    // button2_Click(sender, e);
                }


                //if (txtAddWeigth.Text != "" && txtAddHeight.Text != "")
                //{
                //description += "Ancho Fijo: " + anchoT + "\n";
                //description += "Alto Fijo: " + altoT + "\n";
                //}
                //Añadir la Ubicacion
                if (txtUbicacion.Text != "")
                {
                    description += "Ubicacion: " + txtUbicacion.Text + "\n";
                }
                //Añadir la cantidad 
                description += "Cantidad: " + NumCantidad + "\n"; //cbColor
                if (cbCedazo.Checked)
                {
                    description += "Con Cedazo" + "\n";
                }
                if (txtAddWeigth.Text != "" && txtAddHeight.Text != "")
                {
                    description += "\n";
                    description += "Vidrio Fijo\n";
                    description += "Ancho Fijo: " + anchoT + "\n";
                    description += "Alto Fijo: " + altoT + "\n";
                    description += "Aluminio: " + cbAluminio.Text + "\n";
                    description += "Ubicación: " + cbUbicacion.Text + "\n";
                    description += "Divisiones: " + txtDiviciones.Text + "\n";
                    description += "Vidrio Fijo: " + cbGlass.Text + "\n";

                }
                return description;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

        }
        private string CreateURL()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }





        }
        private void SelectLock()
        {
            try
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
                    if (cbMultConLLave.Checked)
                    {
                        ClsWindows.Lock = cbMultConLLave.Text;
                    }
                    if (cbCilindro.Checked)
                    {
                        ClsWindows.Lock = cbCilindro.Text;
                    }
                }
                else { ClsWindows.Lock = "Cerradura Impacto"; }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }
        public void btnInsertar_Click(object sender, EventArgs e)
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

                MessageBox.Show("Error Inesperado " + ex.Message);
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
                // Procesar txtAlto
                if (txtAlto.Text != "")
                {


                    decimal alto = Convert.ToDecimal(txtAlto.Text);
                   
                        // Convertir a metros si el valor es mayor o igual a 1000
                        alto /= 1000;
                    
                    // Detectar si el usuario ingresó un punto en vez de una coma
                    DetectarPunto();
                    ClsWindows.heigt = alto;
                    //button2_Click(sender, e);
                }

                // Procesar txtAncho
                if (txtAncho.Text != "")
                {


                    decimal ancho = Convert.ToDecimal(txtAncho.Text);
                   
                        // Convertir a metros si el valor es mayor o igual a 1000
                        ancho /= 1000;
                    
                    // Detectar si el usuario ingresó un punto en vez de una coma
                    DetectarPunto();
                    ClsWindows.Weight = ancho;
                   // button2_Click(sender, e);
                }

                //Advertencias();
            }
            catch (Exception)
            {
                // Manejar excepciones, si es necesario
                // MessageBox.Show("ERROR");
            }
        }


        private void cbPico_CheckedChanged(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void cbSPuesta_CheckedChanged(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void cbImpacto_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                if (cbImpacto.Checked)
                {
                    Lock = "Cerradura Impacto Akari";
                    cbPico.Checked = false;
                    cbSPuesta.Checked = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        #endregion

        #region Support Functions
        private bool ValidarCampos()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }

        }
        private void CleanController()
        {
            try
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
                txtPecioFijo.Text = "";
                txtAddWeigth.Text = "";
                txtAddHeight.Text = "";
                txtTotalPrice.Text = "";
                //cbUbicacion.SelectedIndex = 0;
               // cbAluminio.SelectedIndex = 0;
                dgAluminioAdd.DataSource = null;
                dgVidrioAdd.DataSource = null;
                priceNewGlass = 0;
                cbColor.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void DetectarPunto()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        #endregion

        #region KeyPress
        private void txtAncho_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar cuando se presiona la tecla Enter o Tab
            if (e.KeyChar == (char)13 || e.KeyChar == (char)9)
            {
                txtAlto.Focus();
            }

        }
        private void txtAlto_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Verificar Cuando se presiona la tecla Enter
            if (e.KeyChar == (char)13 || e.KeyChar == (char)9)
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
            if (e.KeyChar == (char)13 || e.KeyChar == (char)9)
            {
                cbVidrio.Focus();
            }
        }
        private void cbVidrio_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Verificar Cuando se presiona la tecla Enter
            if (e.KeyChar == (char)13 || e.KeyChar == (char)9)
            {
                cbSupplier.Focus();
            }
        }
        private void cbSupplier_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Verificar Cuando se presiona la tecla Enter
            if (e.KeyChar == (char)13 || e.KeyChar == (char)9)
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
            if (cbGlass.SelectedIndex == 0 && ClsWindows.System != "5020" && ClsWindows.System != "Ventila")
            {
                DialogResult res = MessageBox.Show("El Aluminio seleccionado no es compatible con el sistema seleccionado, ¿Desea continuar?", "Aluminio no compatible", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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

                    // Procesar txtAlto
                    if (txtAddHeight.Text != "")
                    {


                        decimal alto = Convert.ToDecimal(txtAddHeight.Text);

                        // Convertir a metros si el valor es mayor o igual a 1000
                        alto /= 1000;

                        // Detectar si el usuario ingresó un punto en vez de una coma
                        DetectarPunto();
                        Height = alto;
                        //button2_Click(sender, e);
                    }

                    // Procesar txtAncho
                    if (txtAddWeigth.Text != "")
                    {


                        decimal ancho = Convert.ToDecimal(txtAddWeigth.Text);

                        // Convertir a metros si el valor es mayor o igual a 1000
                        ancho /= 1000;

                        // Detectar si el usuario ingresó un punto en vez de una coma
                        DetectarPunto();
                        Weigth = ancho;
                        // button2_Click(sender, e);
                    }


                    //Cargar el DataTable con los precios del vidrio
                    dtGlass = loadProduct.LoadPriceNewGlass(cbSupplier.Text, cbGlass.Text, Weigth, Height);
                    //Para la utilidad
                    dtGlass2 = loadProduct.LoadPriceNewGlassUtilidad(cbSupplier.Text, cbGlass.Text, Weigth, Height);

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

            try { }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            bool Res = false;
            //Validar Campos
            if (ValidateField())
            {
                //Crear un DataTable para cargar los precios del vidrio Nuevo
                DataTable dtAluminio = new DataTable();
                DataTable dtAluminio2 = new DataTable();

                //Obtener Datos del Vidrio
                decimal Weigth = Convert.ToDecimal(txtAddWeigth.Text);
                decimal Height = Convert.ToDecimal(txtAddHeight.Text);
                string Color = cbColor.Text;
                string Supplier = cbSupplier.Text;

                // Procesar txtAlto
                if (txtAddHeight.Text != "")
                {


                    decimal alto = Convert.ToDecimal(txtAddHeight.Text);

                    // Convertir a metros si el valor es mayor o igual a 1000
                    alto /= 1000;

                    // Detectar si el usuario ingresó un punto en vez de una coma
                    DetectarPunto();
                    Height = alto;
                    //button2_Click(sender, e);
                }

                // Procesar txtAncho
                if (txtAddWeigth.Text != "")
                {


                    decimal ancho = Convert.ToDecimal(txtAddWeigth.Text);

                    // Convertir a metros si el valor es mayor o igual a 1000
                    ancho /= 1000;

                    // Detectar si el usuario ingresó un punto en vez de una coma
                    DetectarPunto();
                    Weigth = ancho;
                    // button2_Click(sender, e);
                }
                if (pathVidrioFijo.Contains("Curva tapa"))
                {
                    dtAluminio = loadProduct.LoadAluminioFijo(Color, "Vidrio Fijo", Supplier, Weigth, Height, "Curva tapa", Convert.ToInt16(txtDiviciones.Value));
                }
                else if (pathVidrioFijo.Contains("Curva x12"))
                {
                    dtAluminio = loadProduct.LoadAluminioFijo(Color, "Vidrio Fijo", Supplier, Weigth, Height, "Curva x12", Convert.ToInt16(txtDiviciones.Value));
                }
                else
                {
                    //Cargar el DataTable con los precios del vidrio
                    dtAluminio = loadProduct.LoadAluminioFijo(Color, "Vidrio Fijo", Supplier, Weigth, Height, cbAluminio.Text, Convert.ToInt16(txtDiviciones.Value));
                }
              
                //Para la utilidad
                dtAluminio2 = loadProduct.LoadAluminioFijoUtilidad(Color, "Vidrio Fijo", Supplier, Weigth, Height, cbAluminio.Text, Convert.ToInt16(txtDiviciones.Value));

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
                string path = System.Windows.Forms.Application.StartupPath + "\\Images\\Windows\\" + ClsWindows.Desing + cbColor.Text + txtDiviciones.Value.ToString() + "fijo" + cbUbicacion.Text + ".jpeg";
                pathVidrioFijo = path;
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
            if (cbSupplier.Text == "Default")
            {
                try
                {
                    SubTotal = Convert.ToDecimal(txtTotal.Text);
                    priceNewGlass = Convert.ToDecimal(txtPecioFijo.Text);
                    totalPrice = SubTotal + priceNewGlass;
                    txtTotalPrice.Text = totalPrice.ToString("C");
                }
                catch (Exception)
                {

                }
            }
            else
            {
                try
                {
                    totalPrice = SubTotal + priceNewGlass;
                    txtTotalPrice.Text = totalPrice.ToString("C");
                }
                catch (Exception)
                {

                }
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
                    RemoveRowFromDataTable(dgAccesorios, "Description", "Haladera Concha PL D41");
                }

                if (cbkBaraaEmpuje.Checked == false)
                {
                    RemoveRowFromDataTable(dgAluminio, "Description", "Barra de Empuje PL D42");
                }

                if (cbkBrazoHidraulico.Checked == false)
                {
                    RemoveRowFromDataTable(dgAccesorios, "Description", "CIERRA PUERTA COMPACTO AL 303");
                }

                if (cbkCilindronPestillo.Checked == false)
                {
                    RemoveRowFromDataTable(dgAccesorios, "Description", "Cilindro Tipo RIM RC051 PL");
                }

                if (cbkContramarco.Checked == false)
                {
                    RemoveRowFromDataTable(dgAluminio, "Description", "Tubo 1 Aleta 13/4x4 PL");
                }

                if (cbkUmbral.Checked == false)
                {
                    RemoveRowFromDataTable(dgAluminio, "Description", "Umbral 4 PL D40");
                }

                if (cbkPH206.Checked == true)
                {
                    RemoveRowFromDataTable(dgAccesorios, "Description", "Juego Cerradura 31/32 AL DT1850");
                }

                if (cbkPH206.Checked == false)
                {
                    RemoveRowFromDataTable(dgAccesorios, "Description", "PH206");
                }

                // Verificar si cbkCedazo no está marcado
                if (cbCedazo.Checked == false)
                {
                    RemoveRowFromDataTable(dgAccesorios, "Description", "Escuadra Cedazo 1/2");
                    RemoveRowFromDataTable(dgAccesorios, "Description", "Empaque Cedazo 1/2");
                    RemoveRowFromDataTable(dgAccesorios, "Description", "Fibra Cedazo 5020");

                    RemoveRowFromDataTable(dgAluminio, "Description", "Marco Cedazo 1/2 Fijo");
                }

                // Actualiza los DataGridView después de modificar los DataTable
                dgAccesorios.Refresh();
                dgAluminio.Refresh();
            }
            catch (Exception ex)
            {
                // Manejo de errores
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void RemoveRowFromDataTable(DataGridView dgv, string columnName, string value)
        {
            DataTable dt = (DataTable)dgv.DataSource;
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[columnName] != DBNull.Value && row[columnName].ToString() == value)
                    {
                        // Resta el precio al subtotal
                        SubTotal -= Convert.ToDecimal(row["TotalPrice"]);
                        txtTotal.Text = SubTotal.ToString("C");

                        // Elimina la fila del DataTable
                        dt.Rows.Remove(row);
                        break;
                    }
                }
            }
        }


        private void Advertencias()
        {
            try
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



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
                                case "1 Hoja EA":
                                    if (ClsWindows.Weight >= 1.20m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 1.20 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "2 Hoja EA":
                                    if (ClsWindows.Weight >= 2.40m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 2.40 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "1 Hoja DEA":
                                    if (ClsWindows.Weight >= 1.20m)
                                    {
                                        MessageBox.Show("El ancho maximo para este sistema es de 1.20 Metros");
                                        AceptarAncho = true;
                                    }
                                    break;
                                case "2 Hoja DEA":
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (pbVentana.Image != null)
            {
                try
                {
                    // Obtener las dimensiones desde ClsWindows
                    decimal anchoEnPixeles = ConvertirDimensionAPixeles(ClsWindows.Weight.ToString());
                    decimal alturaEnPixeles = ConvertirDimensionAPixeles(ClsWindows.heigt.ToString());

                    int newWidth = (int)anchoEnPixeles;
                    int newHeight = (int)alturaEnPixeles;

                    // Redimensionar la imagen
                    var resizedImage = ResizeImage(pbVentana.Image, newWidth, newHeight);

                    // Asignar la imagen redimensionada al PictureBox
                    pbVentana.Image = resizedImage;

                    // Ajustar el tamaño del PictureBox para que coincida con la nueva imagen
                    AjustarTamañoPictureBox(pbVentana, newWidth, newHeight);
                }
                catch (FormatException)
                {
                    // Manejo de errores, si es necesario
                    // MessageBox.Show("Error en el formato de las dimensiones.");
                }
            }
            else
            {
                MessageBox.Show("No hay ninguna imagen cargada en el PictureBox.");
            }
        }

        private decimal ConvertirDimensionAPixeles(string dimensionTexto)
        {
            // Validar que la cadena no esté vacía y que sea un número válido
            if (!string.IsNullOrEmpty(dimensionTexto) && decimal.TryParse(dimensionTexto, out decimal dimension))
            {
                // Usar CentimetrosAPixeles si la dimensión empieza con 0, sino usar MetrosAPixeles
                if (dimensionTexto.StartsWith("0"))
                {
                    return dimension * CentimetrosAPixeles;
                }
                else
                {
                    return dimension * MetrosAPixeles;
                }
            }
            else
            {
                throw new FormatException("La dimensión no es válida.");
            }
        }


        private void AjustarTamañoPictureBox(PictureBox pb, int newWidth, int newHeight)
        {
            try
            {
                // Obtener el tamaño del contenedor padre
                var parentSize = pb.Parent.ClientSize;

                // Mantener la imagen dentro de los límites del contenedor padre
                if (newWidth > MaxWidth)
                {
                    newWidth = MaxWidth;
                }
                if (newHeight > MaxHeight)
                {
                    newHeight = MaxHeight;
                }

                // Ajustar el tamaño del PictureBox
                pb.Width = newWidth;
                pb.Height = newHeight;

                // Centrar el PictureBox en su contenedor padre
                pb.Left = (parentSize.Width - pb.Width) / 2;
                pb.Top = (parentSize.Height - pb.Height) / 2;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (cbSupplier.Text == "Default")
            {
                btnInsertar_Click(sender, e);
            }
            else
            {
                frmPostGuardado frm = new frmPostGuardado();
                DataTable dataTable = new DataTable();
                dataTable = ObtenerDatosDGV();
                frm.ObtenerDatos(dataTable, totalPrice);
                frm.Show();
            }
        }

        private DataTable ObtenerDatosDGV()
        {
            DataTable dtAluminio = new DataTable();
            dtAluminio = loadProduct.loadAluminioUtilidad(cbColor.Text, ClsWindows.System, cbSupplier.Text);

            DataTable dtAccesorios = new DataTable();
            dtAccesorios = loadProduct.loadAccesoriosUtilidad(ClsWindows.System, cbSupplier.Text);

            DataTable dtVidrio = new DataTable();
            dtVidrio = loadProduct.loadPricesGlassUtilidad(cbSupplier.Text, cbVidrio.Text);

            DataTable dtLock = new DataTable();
            dtLock = loadProduct.LoadPricesLockUtilidades(cbSupplier.Text, Lock);



            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Price", typeof(decimal));

            Action<DataTable, string> agregarDatosFromDataTable = (sourceTable, columnName) =>
            {
                if (sourceTable != null)
                {
                    foreach (DataRow row in sourceTable.Rows)
                    {
                        if (row[columnName] != null && row[columnName] != DBNull.Value)
                        {
                            decimal totalPrice;
                            if (Decimal.TryParse(row[columnName].ToString(), out totalPrice))
                            {
                                dataTable.Rows.Add(totalPrice);
                            }
                        }
                    }
                }
            };

            // Para dgAluminio, utiliza la columna "TotalCost"
            agregarDatosFromDataTable(dtAluminio, "TotalCost");

            // Para los demás DataGridViews, utiliza la columna "TotalPrice"
            agregarDatosFromDataTable(dtAccesorios, "TotalCost");
            agregarDatosFromDataTable(dtVidrio, "TotalCost");
            agregarDatosFromDataTable(dtAluminio2, "TotalCost");
            agregarDatosFromDataTable(dtGlass2, "TotalCost");
            agregarDatosFromDataTable(dtLock, "TotalCost");

            return dataTable; // Devuelve el DataTable lleno
        }

        private DataTable ObtenerDatosDGV2()
        {
            DataTable dtAluminio = new DataTable();
            dtAluminio = loadProduct.loadAluminio(cbColor.Text, ClsWindows.System, cbSupplier.Text);


            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Price", typeof(decimal));

            Action<DataGridView, string> agregarDatosFromDataGridView = (dataGridView, columnName) =>
            {
                if (dataGridView != null)
                {
                    foreach (DataGridViewRow row in dataGridView.Rows)
                    {
                        if (row.Cells[columnName].Value != null && row.Cells[columnName].Value != DBNull.Value)
                        {
                            decimal totalPrice;
                            if (Decimal.TryParse(row.Cells[columnName].Value.ToString(), out totalPrice))
                            {
                                dataTable.Rows.Add(totalPrice);
                            }
                        }
                    }
                }
            };

            // Para dgAluminio, utiliza la columna "TotalCost"
            agregarDatosFromDataGridView(dgAluminio, "TotalPrice");

            // Para los demás DataGridViews, utiliza la columna "TotalPrice"
            agregarDatosFromDataGridView(dgAccesorios, "TotalPrice");
            agregarDatosFromDataGridView(dgVidrio, "TotalPrice");
            agregarDatosFromDataGridView(dgAluminioAdd, "TotalPrice");
            agregarDatosFromDataGridView(dgVidrioAdd, "TotalPrTotalCostice");
            agregarDatosFromDataGridView(dgvCerradura, "TotalPrice");

            return dataTable; // Devuelve el DataTable lleno
        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CargarProveedor()
        {
            LN_Proveedor proveedor = new LN_Proveedor();
            cbSupplier.DataSource = proveedor.CargarProveedor();
            cbSupplier.DisplayMember = "Nombre";
        }

        private void cbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSupplier.Text == "Default")
            {
                txtTotal.Enabled = true;
                txtTotal.Text = "0";
                txtPecioFijo.Enabled = true;
                txtPecioFijo.Text = "0";
            }
            else
            {
                txtTotal.Enabled = false;
                txtPecioFijo.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PanelDetalle.Visible = false;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            PanelDetalle.Visible = false;
            PanelMedidas.Visible = true;
        }

        //Para oculatar la seccion de "Fijos"
        #region Ocultar Seccion de fijos y eliminar la etiqueta de vidrios en cedazo 1/2
        public string Design2 { get; set; }
        public string System2 { get; set; }
        private void frmCalcPriceWindows_Load(object sender, EventArgs e)
        {
            if (Design2 == "1 Hoja Horizontal" || Design2 == "2 Hoja Horizontal" || Design2 == "3 Hoja Horizontal" || Design2 == "4 Hoja Horizontal" || Design2 == "5 Hoja Horizontal" || Design2 == "6 Hoja Horizontal" || Design2 == "2 Hoja Vertical" || Design2 == "1 Hoja 1 Fijo Vertical" || Design2 == "3 Hoja Vertical")
            {
                label10.Visible = true;
                PanelVidrioFijo.Visible = true;
            }
            else if (System2 == "Ventila Euro")
            {
                if (Design2 == "1 Hoja Horizontal" || Design2 == "2 Hoja Horizontal" || Design2 == "3 Hoja Horizontal" || Design2 == "4 Hoja Horizontal" || Design2 == "5 Hoja Horizontal" || Design2 == "6 Hoja Horizontal")
                {
                    label10.Visible = false;
                    PanelVidrioFijo.Visible = false;

                }
            }
            if (System2 == "Cedazo 1/2")
            {
                if (Design2 == "Cedazo 12" || Design2 == "Cedazo 1" || Design2 == "Cedazo 2" || Design2 == "CedazoAkariFijoMovil" || Design2 == "CedazoAkariFijoMovilMovilFijo")
                {
                    label19.Visible = false;

                }
            }
            if (Design2 == "CedazoAkariFijoMovil" || Design2 == "CedazoAkariFijoMovilMovilFijo")
            {
                label19.Visible = false;
                label10.Visible = false;
                PanelVidrioFijo.Visible = false;
                cbVidrio.Visible = false;
                lblVidrio.Visible = false;
            }
            if (ClsWindows.System == "Ventila") 
            {
                label10.Visible = true;
                PanelVidrioFijo.Visible = true;
            }
        }

        #endregion

        private void button2_Click_2(object sender, EventArgs e)
        {
            PanelDetalle.Visible = false;
            PanelMedidas.Visible = true;
        }

        private void button2_Click_3(object sender, EventArgs e)
        {
            PanelDetalle.Visible = false;
            PanelMedidas.Visible = true;
        }

        private void button2_Click_4(object sender, EventArgs e)
        {
            PanelDetalle.Visible = false;
            PanelMedidas.Visible = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button2_Click_5(object sender, EventArgs e)
        {
            PanelDetalle.Visible = false;
            PanelMedidas.Visible = true;
        }

        private void button2_Click_6(object sender, EventArgs e)
        {
            PanelDetalle.Visible = false;
            PanelMedidas.Visible = true;
        }

        private void txtAddWeigth_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Verificar Cuando se presiona la tecla Enter
            if (e.KeyChar == (char)13 || e.KeyChar == (char)9)
            {
                txtAddHeight.Focus();
            }
        }

        private void txtAddHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Verificar Cuando se presiona la tecla Enter
            if (e.KeyChar == (char)13 || e.KeyChar == (char)9)
            {
                cbAluminio.Focus();
            }
        }

        private void cbAluminio_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            //Verificar Cuando se presiona la tecla Enter
            if (e.KeyChar == (char)13 || e.KeyChar == (char)9)
            {
                cbGlass.Focus();
            }
        }

        private void cbGlass_KeyPress(object sender, KeyPressEventArgs e)
        {
            

            //Verificar Cuando se presiona la tecla Enter
            if (e.KeyChar == (char)13 || e.KeyChar == (char)9)
            {
                cbUbicacion.Focus();
            }
        }

        private void cbUbicacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            //Verificar Cuando se presiona la tecla Enter
            if (e.KeyChar == (char)13 || e.KeyChar == (char)9)
            {
                txtDiviciones.Focus();
            }
        }

        private void txtAddWeigth_Enter(object sender, EventArgs e)
        {
            txtAddWeigth.BackColor = Color.Black;
            txtAddWeigth.ForeColor = Color.White;
            //Cambiar el Tamaño de Letra del txtAddWeigth
            txtAddWeigth.Font = new Font("Microsoft Sans Serif", 11);

        }

        private void txtAddWeigth_Leave(object sender, EventArgs e)
        {
            txtAddWeigth.BackColor = Color.White;
            txtAddWeigth.ForeColor = Color.Black;
            //Cambiar el Tamaño de Letra del txtAddWeigth
            txtAddWeigth.Font = new Font("Microsoft Sans Serif", 8);

        }

        private void txtAddHeight_Enter(object sender, EventArgs e)
        {
            txtAddHeight.BackColor = Color.Black;
            txtAddHeight.ForeColor = Color.White;
            //Cambiar el Tamaño de Letra del txtAddHeight
            txtAddHeight.Font = new Font("Microsoft Sans Serif", 11);
        }

        private void txtAddHeight_Leave(object sender, EventArgs e)
        {
            txtAddHeight.BackColor = Color.White;
            txtAddHeight.ForeColor = Color.Black;
            //Cambiar el Tamaño de Letra del  txtAddHeight
            txtAddHeight.Font = new Font("Microsoft Sans Serif", 8);
        }

        private void cbAluminio_Enter(object sender, EventArgs e)
        {
            cbAluminio.BackColor = Color.Black;
            cbAluminio.ForeColor = Color.White;
            //Cambiar el Tamaño de Letra del cbAluminio
            cbAluminio.Font = new Font("Microsoft Sans Serif", 11);
        }

        private void cbAluminio_Leave(object sender, EventArgs e)
        {
            cbAluminio.BackColor = Color.White;
            cbAluminio.ForeColor = Color.Black;
            //Cambiar el Tamaño de Letra del  cbAluminio
            cbAluminio.Font = new Font("Microsoft Sans Serif", 8);
        }

        private void cbGlass_Enter(object sender, EventArgs e)
        {
            cbGlass.BackColor = Color.Black;
            cbGlass.ForeColor = Color.White;
            //Cambiar el Tamaño de Letra del cbGlass
            cbGlass.Font = new Font("Microsoft Sans Serif", 11);
        }

        private void cbGlass_Leave(object sender, EventArgs e)
        {
            cbGlass.BackColor = Color.White;
            cbGlass.ForeColor = Color.Black;
            //Cambiar el Tamaño de Letra del  cbGlass
            cbGlass.Font = new Font("Microsoft Sans Serif", 8);
        }

        private void cbUbicacion_Enter(object sender, EventArgs e)
        {
            cbUbicacion.BackColor = Color.Black;
            cbUbicacion.ForeColor = Color.White;
            //Cambiar el Tamaño de Letra del cbUbicacion
            cbUbicacion.Font = new Font("Microsoft Sans Serif", 11);
        }

        private void cbUbicacion_Leave(object sender, EventArgs e)
        {
            cbUbicacion.BackColor = Color.White;
            cbUbicacion.ForeColor = Color.Black;
            //Cambiar el Tamaño de Letra del  cbUbicacion
            cbUbicacion.Font = new Font("Microsoft Sans Serif", 8);
        }

        private void txtDiviciones_Enter(object sender, EventArgs e)
        {
            txtDiviciones.BackColor = Color.Black;
            txtDiviciones.ForeColor = Color.White;
            //Cambiar el Tamaño de Letra del txtDiviciones
            txtDiviciones.Font = new Font("Microsoft Sans Serif", 11);
        }

        private void txtDiviciones_Leave(object sender, EventArgs e)
        {
            txtDiviciones.BackColor = Color.White;
            txtDiviciones.ForeColor = Color.Black;
            //Cambiar el Tamaño de Letra del  txtDiviciones
            txtDiviciones.Font = new Font("Microsoft Sans Serif", 8);
        }

        private void pbVentana_Click(object sender, EventArgs e)
        {

        }
    }
}
