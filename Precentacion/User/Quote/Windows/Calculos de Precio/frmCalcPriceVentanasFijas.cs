using Dominio.Model.ClassWindows;
using Negocio.LoadProduct;
using Negocio.Proveedor;
using Precentacion.User.Quote.Quote;
using Precentacion.User.Quote.Windows.Seleccion_Diseño;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace Precentacion.User.Quote.Windows
{
    public partial class frmCalcPriceVentanasFijas : MaterialSkin.Controls.MaterialForm
    {
        #region Variables
        decimal PrecioTotal;
        decimal TempPrecio;
        string URL;
        // Relación de escala (1 metro = 1000 píxeles, 1 centímetro = 100 píxeles)
        private const decimal MetrosAPixeles = 1000.0m;
        private const decimal CentimetrosAPixeles = 1500.0m;

        // Tamaño máximo permitido para el PictureBox
        private const int MaxWidth = 450;
        private const int MaxHeight = 350;
        N_LoadProduct n_LoadProduct = new N_LoadProduct();
        #endregion

        #region Constructor
        public frmCalcPriceVentanasFijas()
        {
            InitializeComponent();
            frmCalcPriceVentanasFijas_Load(null, null);
            CargarProveedores();
        }
        #endregion

        #region Metodos Inicio
        private void frmCalcPriceVentanasFijas_Load(object sender, EventArgs e)
        {
            if (ClsWindows.System != "Vidrio Fijo")
            {
                lblAluminio.Visible = false;
                cbAluminio.Visible = false;
            }
            cbColor.SelectedIndex = 0;
            cbAluminio.SelectedIndex = 0;
            //cbSupplier.SelectedIndex = 0;
            CargarImagen();
            CargarDescripcion();
            CargarVidrio();
            OcultarMaterial();
        }
        private void CargarImagen()
        {
            try
            {
                //Cargar imagen
                string path = Application.StartupPath + "\\Images\\Windows\\VidrioFijo" + ClsWindows.Desing.Trim() + cbColor.Text + ".jpeg";
                pbVentana.Image = Image.FromFile(path);
                URL = path;

                //Ajustar imagen
                if (pbVentana.Image.Width > pbVentana.Width || pbVentana.Image.Height > pbVentana.Height)
                {
                    pbVentana.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    pbVentana.SizeMode = PictureBoxSizeMode.Normal;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se Encontro el Color Seleccionado", "Color no Disponible", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (cbColor.SelectedIndex > 0)
                {
                    cbColor.SelectedIndex = 0;
                }
            }

        }
        private void CargarDescripcion()
        {
            try
            {
                //Cargar descripcion
                string Descripcion = ClsWindows.System + " Con Diseño " + ClsWindows.Desing.Trim() + " " + cbColor.Text.Trim();
                lblDescripcion.Text = Descripcion;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se Encontro la Descripcion", "Descripcion no Disponible", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void CargarVidrio()
        {
            try
            {
                //Cargar Vidrio
                DataTable Vidrio = n_LoadProduct.loadOnlyGlass();
                cbVidrio.DataSource = Vidrio;
                cbVidrio.DisplayMember = "Description";
                cbVidrio.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se Encontro el Vidrio", "Vidrio no Disponible", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void OcultarMaterial()
        {
            if (ClsWindows.System == "EuAbatible")
            {
                lblAluminio.Visible = false;
                cbAluminio.Visible = false;
            }
        }
        #endregion

        #region Validaciones
        private bool ValidarCampos() 
        {
            if (string.IsNullOrEmpty(txtAlto.Text))
            {
                MessageBox.Show("El Campo Alto no puede estar Vacio", "Campo Vacio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAlto.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtAncho.Text))
            {
                MessageBox.Show("El Campo Ancho no puede estar Vacio", "Campo Vacio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAncho.Focus();
                return false;
            }
            return true;

        }

        private void ValidarPunto()
        {
            if (txtAlto.Text.Contains("."))
            {
                txtAlto.Text = txtAlto.Text.Replace(".", ",");
                txtAlto.SelectionStart = txtAlto.Text.Length;
            }
            if (txtAncho.Text.Contains("."))
            {
                txtAncho.Text = txtAncho.Text.Replace(".", ",");
                txtAncho.SelectionStart = txtAncho.Text.Length;
            }
        }

        private void LimpiarCampos()
        {
            txtAlto.Text = "";
            txtAncho.Text = "";
            txtCantidad.Value = 1;
            cbColor.SelectedIndex = 0;
            cbAluminio.SelectedIndex = 0;
            cbSupplier.SelectedIndex = 0;
            cbVidrio.SelectedIndex = 0;
            txtUbicacion.Text = "";
            txtTotal.Text = "";
        }
        #endregion

        #region Botones
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
        private void btnCargar_Click(object sender, EventArgs e)
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
            if (ValidarCampos())
            {
                DataTable dtAluminio = n_LoadProduct.loadAluminioVentanaFija(cbColor.Text, ClsWindows.System, cbSupplier.Text, cbAluminio.Text);
                DataTable dtVidrio = n_LoadProduct.loadPricesGlass(cbSupplier.Text, cbVidrio.Text);
                if (ClsWindows.System == "EuAbatible" || ClsWindows.System == "PuertaEuAbatible")
                {
                    DataTable dtAccesorios = n_LoadProduct.loadAccesorios(ClsWindows.System, cbSupplier.Text);
                    dgvAccesorios.DataSource = dtAccesorios;
                }
                Aluminiodt.DataSource = dtAluminio;
                Vidriodt.DataSource = dtVidrio;

                CargarPrecio();
            }
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            frmSelecDesingVentanaFija frm = new frmSelecDesingVentanaFija();
            frm.Show();
            this.Close();
        }
        private void btnAbrirDesglose_Click(object sender, EventArgs e)
        {
            panelDetalle.Visible = false;
        }
        private void btnDesglose_Click_1(object sender, EventArgs e)
        {
            panelDetalle.Visible = true;
        }
        private void btnAgregarCotizacion_Click(object sender, EventArgs e)
        {
            Guardar();
        }
        #endregion

        #region Eventos
        private void cbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarImagen();
            CargarDescripcion();
        }

        private void txtAlto_textChanged(object sender, EventArgs e)
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
            }
            catch (Exception)
            {

            }

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
        private void txtAncho_textChanged(object sender, EventArgs e)
        {
            try
            {// Procesar txtAlto
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
            }
            catch (Exception)
            {

            }

        }
        #endregion


        #region Metodos
        private void CargarPrecio()
        {
            try
            {
                decimal PrecioAluminio = 0;
                for (int i = 0; i < Aluminiodt.Rows.Count; i++)
                {
                    if (i != Aluminiodt.Rows.Count - 1)
                    {
                        PrecioAluminio += Convert.ToDecimal(Aluminiodt.Rows[i].Cells[3].Value.ToString());
                    }
                }

                decimal PrecioVidrio = 0;
                for (int i = 0; i < Vidriodt.Rows.Count; i++)
                {
                    if (i != Vidriodt.Rows.Count - 1)
                    {
                        PrecioVidrio += Convert.ToDecimal(Vidriodt.Rows[i].Cells[3].Value.ToString());
                    }
                }

                decimal Accesorios = 0;
                for (int i = 0; i < dgvAccesorios.Rows.Count; i++)
                {
                    if (i != dgvAccesorios.Rows.Count - 1)
                    {
                        Accesorios += Convert.ToDecimal(dgvAccesorios.Rows[i].Cells[3].Value.ToString());
                    }
                }

                decimal Subtotal = PrecioAluminio + PrecioVidrio + Accesorios;

                string Descripcion = ClsWindows.System + ClsWindows.Desing + cbColor.Text;
                decimal Ajuste = n_LoadProduct.LoadAjustePrecio(cbSupplier.Text, Descripcion);

                PrecioTotal = Subtotal + (Subtotal * Ajuste);
                TempPrecio = PrecioTotal * Convert.ToDecimal(txtCantidad.Value);
                txtTotal.Text = TempPrecio.ToString("c");
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se encontró el precio: " + ex.Message, "Precio no disponible", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region GuardarVentana
        private void Guardar() 
        {
            N_LoadProduct n_LoadProduct = new N_LoadProduct();
            try
            {
                //Cargar Datos Necesarios para Guardar
                string Description = CrearDescripcion();

                //Validar si TempPrecio es 0
                if (TempPrecio != 0)
                {
                    PrecioTotal = TempPrecio;
                }


                //Guardar Ventana
                bool result = n_LoadProduct.insertWindows(Description,URL,ClsWindows.Weight,ClsWindows.heigt,cbVidrio.Text,cbColor.Text,"",PrecioTotal,ClsWindows.IDQuote,ClsWindows.System,ClsWindows.Desing);
                if (result)
                {
                    MessageBox.Show("Ventana Guardada Correctamente", "Guardado Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ActualizarCotizacion();
                    LimpiarCampos();


                }
                else
                {
                    MessageBox.Show("No se Pudo Guardar la Ventana", "Guardado Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hubo un Error al Guardar", "Guardado Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Information);
              
            }
        }
        private string CrearDescripcion()
        {
            //crear descripcion de la ventana que incluya el sistema, diseño, color, vidrio separado por saltos de linea
            string description = "";
            description += "Sistema: " + ClsWindows.System + "\n";
            description += "Diseño: " + ClsWindows.Desing + "\n";
            description += "Color: " + cbColor.Text + "\n";
            description += "Vidrio: " + cbVidrio.Text + "\n";
            description += "Cantidad: " + txtCantidad.Value + "\n";
            description += "Ancho: " + ClsWindows.Weight + "\n";
            description += "Alto: " + ClsWindows.heigt + "\n";
            description += "Material " + cbAluminio.Text + "\n";
            
            //Añadir la Ubicacion
            if (txtUbicacion.Text != "")
            {
                description += "Ubicacion: " + txtUbicacion.Text + "\n";
            }
            return description;
        }
        private void ActualizarCotizacion()
        {
            Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is frmQuote);
            if (frm != null)
            {
                ((frmQuote)frm).loadWindows();
            }      
        }

        #endregion

        #region KeyPress
        private void txtAncho_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Validar si se preciono Enter
            if (e.KeyChar == (char)13)
            {
                txtAlto.Focus();
            }
        }
        private void txtAlto_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Validar si se preciono Enter
            if (e.KeyChar == (char)13)
            {
                cbColor.Focus();
            }
        }
        private void cbColor_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Validar si se preciono Enter
            if (e.KeyChar == (char)13)
            {
                cbAluminio.Focus();
            }
        }
        private void cbAluminio_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Validar si se preciono Enter
            if (e.KeyChar == (char)13)
            {
                cbVidrio.Focus();
            }
        }
        private void cbVidrio_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Validar si se preciono Enter
            if (e.KeyChar == (char)13)
            {
                txtCantidad.Focus();
            }
        }
        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Validar si se preciono Enter
            if (e.KeyChar == (char)13)
            {
                cbSupplier.Focus();
            }
        }
        private void cbSupplier_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Validar si se preciono Enter
            if (e.KeyChar == (char)13)
            {
                btnCargar_Click(null,null);
                btnAgregarCotizacion_Click(null,null );
            }
        }
        #endregion

        #region Focus and Leave
        private void txtAlto_Enter(object sender, EventArgs e)
        {
            txtAlto.BackColor = Color.Black;
            txtAlto.ForeColor = Color.White;
        }
        private void txtAlto_Leave(object sender, EventArgs e)
        {
            txtAlto.BackColor = Color.White;
            txtAlto.ForeColor = Color.Black;
        }
        private void txtAncho_Enter(object sender, EventArgs e)
        {
            txtAncho.BackColor = Color.Black;
            txtAncho.ForeColor = Color.White;
        }
        private void txtAncho_Leave(object sender, EventArgs e)
        {
            txtAncho.BackColor = Color.White;
            txtAncho.ForeColor = Color.Black;
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
            cbColor.Font = new Font("Microsoft Sans Serif", 9);
        }
        private void cbAluminio_Enter(object sender, EventArgs e)
        {
            cbAluminio.BackColor = Color.Black;
            cbAluminio.ForeColor = Color.White;
            //Cambiar el Tamaño de Letra del cbVidrio
            cbAluminio.Font = new Font("Microsoft Sans Serif", 11);
        }
        private void cbAluminio_Leave(object sender, EventArgs e)
        {
            cbAluminio.BackColor = Color.White;
            cbAluminio.ForeColor = Color.Black;
            //Cambiar el Tamaño de Letra del cbVidrio
            cbAluminio.Font = new Font("Microsoft Sans Serif", 9);
        }
        private void cbVidrio_Enter(object sender, EventArgs e)
        {
            cbVidrio.BackColor = Color.Black;
            cbVidrio.ForeColor = Color.White;
            //Cambiar el Tamaño de Letra del cbVidrio
            cbVidrio.Font = new Font("Microsoft Sans Serif", 10);
        }
        private void cbVidrio_Leave(object sender, EventArgs e)
        {
            cbVidrio.BackColor = Color.White;
            cbVidrio.ForeColor = Color.Black;
            //Cambiar el Tamaño de Letra del cbVidrio
            cbVidrio.Font = new Font("Microsoft Sans Serif", 9);
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
            cbSupplier.Font = new Font("Microsoft Sans Serif", 9);
        }

        #endregion

        private void txtCantidad_ValueChanged(object sender, EventArgs e)
        {
            /*TempPrecio = 0;
            TempPrecio = PrecioTotal * Convert.ToDecimal(txtCantidad.Value);
            txtTotal.Text = TempPrecio.ToString();*/
        }

        private void lblDescripcion_Click(object sender, EventArgs e)
        {

        }
        private void CargarProveedores() 
        {
            LN_Proveedor ln_Proveedor = new LN_Proveedor();
            cbSupplier.DataSource = ln_Proveedor.CargarProveedor();
            cbSupplier.DisplayMember = "Nombre";
        }
        private void cbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbSupplier.SelectedIndex == 3) 
            {
                txtTotal.Enabled = true;
            }
            else
            {
                txtTotal.Enabled = false;
            }
        }

        private bool isUpdatingText = false;

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            if (isUpdatingText) return;

            try
            {
                isUpdatingText = true;

                if (TempPrecio != 0)
                {
                    if (!string.IsNullOrWhiteSpace(txtTotal.Text))
                    {
                        // Permitir números, puntos, comas, espacios y el símbolo de colón "₡"
                        if (System.Text.RegularExpressions.Regex.IsMatch(txtTotal.Text, @"[^0-9^.^,₡\s]"))
                        {
                            MessageBox.Show("Por favor, solo ingrese números", "Solo números", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtTotal.Text = txtTotal.Text.Substring(0, txtTotal.Text.Length - 1);
                            txtTotal.SelectionStart = txtTotal.Text.Length;
                        }
                        else
                        {
                            // Remover el símbolo de colón para la conversión a decimal
                            string cleanText = txtTotal.Text.Replace("₡", "").Trim();
                            if (cleanText.Contains("."))
                            {
                                cleanText = cleanText.Replace(".", ",");
                                txtTotal.Text = "₡ " + cleanText;
                                txtTotal.SelectionStart = txtTotal.Text.Length;
                                PrecioTotal = Convert.ToDecimal(cleanText);
                            }
                            else
                            {
                                PrecioTotal = Convert.ToDecimal(cleanText);
                                txtTotal.Text = "₡ " + PrecioTotal.ToString("N2");
                            }
                        }
                    }
                    else
                    {
                        txtTotal.Text = "₡ " + TempPrecio.ToString("N2");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ingresar el precio: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isUpdatingText = false;
            }
        }

        private void frmCalcPriceVentanasFijas_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void frmCalcPriceVentanasFijas_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (panelDetalle.Visible == true)
            {
                MessageBox.Show("Pulse el botón 'Salir' en la parte inferior de este formulario.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Cancel = true; // Cancel the closing event
            }
            else
            {
                switch (ClsWindows.Desing)
                {
                    case "1Fijo":
                    case "2Fijo":
                    case "3Fijo":
                    case "4Fijo":
                    case "5Fijo":
                    case "6Fijo":
                        frmSelecDesingVentanaFija frm = new frmSelecDesingVentanaFija();
                        frm.Show();
                        break;
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            panelDetalle.Visible = false;
        }
    }
}
