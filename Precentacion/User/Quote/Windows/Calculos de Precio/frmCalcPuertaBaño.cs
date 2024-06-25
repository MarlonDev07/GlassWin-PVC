using Dominio.Model.PuertaBaño;
using Negocio.LoadProduct;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Negocio.LoadProduct;
using Dominio.Model.ClassWindows;
using Precentacion.User.Quote.Quote;
using System.Linq;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Negocio.Proveedor;

namespace Precentacion.User.Quote.Windows.Calculos_de_Precio
{
    public partial class frmCalcPuertaBaño : MaterialSkin.Controls.MaterialForm
    {
        public string design2 { get; set; }
        string Url = "";
        decimal Price = 0;
        decimal TempPrice = 0;
        // Relación de escala (1 metro = 1000 píxeles, 1 centímetro = 100 píxeles)
        private const decimal MetrosAPixeles = 1000.0m;
        private const decimal CentimetrosAPixeles = 100.0m;
        public frmCalcPuertaBaño()
        {
            InitializeComponent();
            Fn_Inicializacion();
            CargarProveedor();
        }

        #region Fn_Iniciales
        private void Fn_Inicializacion() 
        {
          Fn_CargarImagen();
          Fn_CargarVidrios();
          FN_SelectComboIndex();
        }

        private void Fn_CargarImagen ()
        {
            try
            {
                string path = Application.StartupPath + @"\Images\Windows\PB "+clsPuertaBaño.Desing+".jpg";
                Url = path;
                picPuertaBaño.Image = Image.FromFile(path);
                picPuertaBaño.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la imagen: " + ex.Message);
            }
        }
        private void Fn_CargarVidrios()
        {
            N_LoadProduct n_LoadProduct = new N_LoadProduct();
            DataTable dt = n_LoadProduct.loadOnlyGlass();
            if (dt.Rows.Count > 0)
            {
                try
                {
                    cbVidrio.DataSource = dt;
                    cbVidrio.DisplayMember = "Description";
                    cbVidrio.ValueMember = "Description";
                }
                catch (Exception)
                {

                    MessageBox.Show("Error al cargar los vidrios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
        private void FN_SelectComboIndex() 
        {
            cbColor.SelectedIndex = 0;
            cbLaminaPlastica.SelectedIndex = 0;
            cbColorLamina.SelectedIndex = 0;
            //cbSupplier.SelectedIndex = 0;
        }
        #endregion

        #region Eventos
        private void PuntoDecimal_TextChanged(object sender, EventArgs e)
        {
            //Validar el punto decimal
            TextBox TextBox = (TextBox)sender;
            string text = TextBox.Text;
            text = text.Replace(".", ",");
            TextBox.Text = text;
            TextBox.SelectionStart = TextBox.Text.Length;

            //Asignar el valor a la clase clsPuertaBaño
            string Name = TextBox.Name;

            try
            {
                if (TextBox.Text != string.Empty || TextBox.Text != "")
                {
                    decimal Valor = Convert.ToDecimal(TextBox.Text);
                    switch (Name)
                    {
                        case "txtAncho":
                            clsPuertaBaño.WeightTotal = Valor;
                            break;
                        case "txtAnchoPanel":
                            clsPuertaBaño.WeightPanel = Valor;
                            break;
                        case "txtAlto":
                            clsPuertaBaño.heigt = Valor;
                            break;
                    }
                }

                if (txtAlto.Text != "")
                {
                    //Detectar si el usuario ingreso un punto en vez de una coma
                    DetectarPunto();
                    ClsWindows.heigt = Convert.ToDecimal(txtAlto.Text);
                    redimension_Click(sender, e);


                }
                if (txtAncho.Text != "")
                {
                    //Detectar si el usuario ingreso un punto en vez de una coma
                    DetectarPunto();
                    ClsWindows.Weight = Convert.ToDecimal(txtAncho.Text);
                    redimension_Click(sender, e);
                }
                //Advertencias();
            }
            catch (Exception)
            {

            }


        }
        private void redimension_Click(object sender, EventArgs e)
        {
            if (picPuertaBaño.Image != null)
            {
                try
                {
                    if (decimal.TryParse(txtAncho.Text, out decimal anchoEnMetros) &&
                        decimal.TryParse(txtAlto.Text, out decimal alturaEnMetros))
                    {
                        int newWidth = (int)(anchoEnMetros * MetrosAPixeles);
                        int newHeight = (int)(alturaEnMetros * MetrosAPixeles);

                        if (newWidth > 0 && newHeight > 0)
                        {
                            var resizedImage = ResizeImage(picPuertaBaño.Image, newWidth, newHeight);
                            picPuertaBaño.Image = resizedImage;
                            picPuertaBaño.SizeMode = PictureBoxSizeMode.Zoom;  // Ajusta según sea necesario
                            picPuertaBaño.Refresh(); // Forzar actualización del PictureBox
                        }
                        else
                        {
                            //MessageBox.Show("Las dimensiones deben ser mayores que cero.");
                        }
                    }
                    else
                    {
                        //MessageBox.Show("Por favor, ingresa valores numéricos válidos para el ancho y la altura.");
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show($"Ocurrió un error al redimensionar la imagen: {ex.Message}");
                }
            }
            else
            {
                //MessageBox.Show("No hay ninguna imagen cargada en el PictureBox.");
            }
        }
        #endregion
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
        #region KeyPress
        private void KeyPressTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Validar que se presione la tecla Enter
            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBox TextBox = (TextBox)sender;
                //Obtener el Nombre del TextBox
                string Name = TextBox.Name;
                switch (Name)
                {
                    case "txtAncho":
                        txtAnchoPanel.Focus();
                        break;
                    case "txtAnchoPanel":
                        txtAlto.Focus();
                        break;
                    case "txtAlto":
                        cbColor.Focus();
                        cbColor.DroppedDown = true;
                        break;

                }
            }
        }

        private void ComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Validar que se presione la tecla Enter
            if (e.KeyChar == (char)Keys.Enter)
            {
                ComboBox ComboBox = (ComboBox)sender;
                //Obtener el Nombre del TextBox
                string Name = ComboBox.Name;
                switch (Name)
                {
                    case "cbColor":
                        cbVidrio.Focus();
                        cbVidrio.DroppedDown = true;
                        break;
                    case "cbVidrio":
                        cbLaminaPlastica.Focus();
                        cbLaminaPlastica.DroppedDown = true;
                        break;
                    case "cbLaminaPlastica":
                        cbColorLamina.Focus();
                        cbColorLamina.DroppedDown = true;
                        break;
                    case "cbColorLamina":
                        cbSupplier.Focus();
                        cbSupplier.DroppedDown = true;
                        break;

                }
            }
        }
        #endregion

        #region Metodos
        private bool ValidarCampos() 
        {
            if (txtAncho.Text == string.Empty)
            {
               MessageBox.Show("Debe ingresar el ancho de la puerta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAncho.Focus();
                return false;
            }
            if (txtAnchoPanel.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar el ancho del panel", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAnchoPanel.Focus();
                return false;
            }
            if (txtAlto.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar el alto de la puerta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAlto.Focus();
                return false;
            }
            return true;
        }

        private void SumarValores(DataTable dtAluminio, DataTable dtVidrio, DataTable dtAccesorios)
        {
            DataTable dataTableAluminio = dtAluminio;
            //Sumar los valores de la tabla Aluminio
            clsPuertaBaño.Price = 0;
            foreach (DataRow row in dataTableAluminio.Rows)
            {
                clsPuertaBaño.Price += Convert.ToDecimal(row["TotalPrice"]);
            }

            //Sumar los valores de la tabla Vidrio
            DataTable dataTableVidrio = dtVidrio;
            foreach (DataRow row in dataTableVidrio.Rows)
            {
                clsPuertaBaño.Price += Convert.ToDecimal(row["TotalPrice"]);
            }

            //Sumar los valores de la tabla Accesorios
            DataTable dataTableAccesorios = dtAccesorios;
            foreach (DataRow row in dataTableAccesorios.Rows)
            {
                clsPuertaBaño.Price += Convert.ToDecimal(row["TotalPrice"]);
            }


            txtTotalPrice.Text = clsPuertaBaño.Price.ToString("C");
        }
        #endregion

        #region Botones
        private void btnCargar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                N_LoadProduct n_LoadProduct = new N_LoadProduct();
                //Aluminio
                DataTable dtAluminio = n_LoadProduct.loadAluminio(cbColor.Text, clsPuertaBaño.System, cbSupplier.Text);
                dgvAluminio.DataSource = dtAluminio;
                dgvAluminio.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
             

                //Vidrio
                DataTable dtVidrio = n_LoadProduct.loadPricesGlass(cbSupplier.Text, cbVidrio.Text);
                dgvVidrio.DataSource = dtVidrio;
                dgvVidrio.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                //Accesorios
                DataTable dtAccesorios = n_LoadProduct.loadAccesorios(clsPuertaBaño.System, cbSupplier.Text);
                dgvAccesorios.DataSource = dtAccesorios;
                dgvAccesorios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;



                SumarValores(dtAluminio,dtVidrio,dtAccesorios);



            }
        }
        #endregion

        private void btnDesglose_Click(object sender, EventArgs e)
        {
            panelDesglose.Visible = true;
        }

        private void btnOcultar_Click(object sender, EventArgs e)
        {
            panelDesglose.Visible = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            N_LoadProduct n_LoadProduct = new N_LoadProduct();
            //Crear Descripcion
            string Description= "";
            Description = "Puerta de Baño " + clsPuertaBaño.Desing+"\n";
            Description += "Ancho: " + clsPuertaBaño.WeightTotal + "\n";
            Description += "Alto: " + clsPuertaBaño.heigt + "\n";
            Description += "Ancho Panel: " + clsPuertaBaño.WeightPanel + "\n";
            Description += "Vidrio: " + cbVidrio.Text + "\n";
            Description += "Lamina Plastica: " + cbLaminaPlastica.Text + "\n";
            Description += "Color Lamina: " + cbColorLamina.Text + "\n";
            Description += "Color Aluminio: " + cbColor.Text + "\n";
            Description += "Ubicación: " + txtUbicacion.Text + "\n";

            clsPuertaBaño.Price = TempPrice;
            if (n_LoadProduct.insertWindows(Description,Url,clsPuertaBaño.WeightTotal,clsPuertaBaño.heigt,cbVidrio.Text,cbColor.Text,"",clsPuertaBaño.Price,ClsWindows.IDQuote,clsPuertaBaño.System,clsPuertaBaño.Desing))
            {
                Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is frmQuote);
                if (frm != null)
                {
                    ((frmQuote)frm).loadWindows();
                }
                MessageBox.Show("Puerta de Baño Guardada", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error al guardar la puerta de baño", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void txtCantidad_ValueChanged(object sender, EventArgs e)
        {
            TempPrice = 0;
            TempPrice = clsPuertaBaño.Price * Convert.ToDecimal(txtCantidad.Value);
            txtTotalPrice.Text = TempPrice.ToString("C");
        }

        private void frmCalcPuertaBaño_Load(object sender, EventArgs e)
        {
            if (design2 == "MovilMovil") 
            {
                lblAncho.Visible = false;
                txtAncho.Visible = false;
            }
        }
        private void CargarProveedor() 
        {
            LN_Proveedor ln_Proveedor = new LN_Proveedor();
            cbSupplier.DataSource = ln_Proveedor.CargarProveedor();
            cbSupplier.DisplayMember = "Nombre";
        }

        private void cbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSupplier.Text == "Default")
            {
                txtTotalPrice.Enabled = true;
            }
            else
            {
                txtTotalPrice.Enabled = false;
            }
        }

        

        private void lblAncho_Click(object sender, EventArgs e)
        {

        }
    }
}
