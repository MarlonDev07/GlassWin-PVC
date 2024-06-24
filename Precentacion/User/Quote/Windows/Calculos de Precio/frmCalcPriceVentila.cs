using Dominio.Model.ClassWindows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio.LoadProduct;
using Precentacion.User.Quote.Quote;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Negocio.Proveedor;

namespace Precentacion.User.Quote.Windows.Calculos_de_Precio
{
    public partial class frmCalcPriceVentila : MaterialSkin.Controls.MaterialForm
    {
        string RutaImagen;
        decimal precioTotal;
        decimal TempPrecio;
        // Relación de escala (1 metro = 1000 píxeles, 1 centímetro = 100 píxeles)
        private const decimal MetrosAPixeles = 1000.0m;
        private const decimal CentimetrosAPixeles = 100.0m;
        public frmCalcPriceVentila()
        {
            InitializeComponent();
            cbColor.SelectedIndex = 0;
            cbSupplier.SelectedIndex = 0;
            Fn_CargarImagen();
            Fn_CargarVidrios();
            panelDetalle.Visible = false;
            
        }
        #region Fn_Iniciales
        private void Fn_CargarImagen() 
        {
            try
            {
                
                string path = Application.StartupPath + @"\Images\Windows\"+ClsWindows.Desing+cbColor.Text+".jpeg";
                RutaImagen = path;
                pbVentila.Image = Image.FromFile(path);
                //Ajustar el tamaño de la imagen
                pbVentila.SizeMode = PictureBoxSizeMode.StretchImage;
                //Hacer Mas Grueso el ancho de la imagen
                pbVentila.Width = 350;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la imagen: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        #endregion

        #region Eventos
        private void cbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fn_CargarImagen();
        }
        #endregion

        private void CargarProveedor() 
        {
            LN_Proveedor ln_Proveedor = new LN_Proveedor();
            cbSupplier.DataSource = ln_Proveedor.CargarProveedor();
            cbSupplier.DisplayMember = "Nombre";
        }
        private void btnCargar_Click(object sender, EventArgs e)
        {
            N_LoadProduct n_LoadProduct = new N_LoadProduct();
            
            DataTable dtAluminio = n_LoadProduct.loadAluminio(cbColor.Text,ClsWindows.System,cbSupplier.Text);
            DataTable dtVidrio = n_LoadProduct.loadPricesGlass(cbSupplier.Text,cbVidrio.Text);
            DataTable dtAccesorios = n_LoadProduct.loadAccesorios(ClsWindows.System, cbSupplier.Text);
            dgvAccesorios.DataSource = dtAccesorios;
            Aluminiodt.DataSource = dtAluminio;
            Vidriodt.DataSource = dtVidrio;
            if (dtAluminio.Rows.Count > 0 && dtVidrio.Rows.Count > 0 && dtAccesorios.Rows.Count > 0)
            {
               decimal Precio = n_LoadProduct.CalcTotalPrice(dtAluminio, dtVidrio, dtAccesorios,null,ClsWindows.System+ClsWindows.Desing+cbColor.Text,cbSupplier.Text);
               txtTotal.Text = Precio.ToString();
               txtTotalPrice.Text = Precio.ToString("C");
               precioTotal = Precio;
               MessageBox.Show("Precios cargados correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se encontraron precios para los productos seleccionados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                
            }
            else
            {
                ClsWindows.AnchoVentila = Convert.ToDecimal(textBox1.Text);
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

        private void txtAncho_TextChanged(object sender, EventArgs e)
        {
            try
            {
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

        private void txtAlto_TextChanged(object sender, EventArgs e)
        {
            try
            {
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

        private void btnDesglose_Click(object sender, EventArgs e)
        {
            panelDetalle.Visible = true;
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
        private void redimension_Click(object sender, EventArgs e)
        {
            if (pbVentila.Image != null)
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
                            var resizedImage = ResizeImage(pbVentila.Image, newWidth, newHeight);
                            pbVentila.Image = resizedImage;
                            pbVentila.SizeMode = PictureBoxSizeMode.Zoom;  // Ajusta según sea necesario
                            pbVentila.Refresh(); // Forzar actualización del PictureBox
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



        private void btnGuardar_Click(object sender, EventArgs e)
        {
            N_LoadProduct n_LoadProduct = new N_LoadProduct();
            //Crear Descripcion
            string description = "";
            description += "Sistema: " + ClsWindows.System + "\n";
            description += "Diseño: " + ClsWindows.Desing + "\n";
            description += "Color: " + cbColor.Text + "\n";
            description += "Vidrio: " + cbVidrio.Text + "\n";
            description += "Ancho: " + ClsWindows.Weight + "\n";
            description += "Alto: " + ClsWindows.heigt + "\n";
            description += "Cantidad: " + txtCantidad.Value + "\n";
            description += "Ubicación: " + txtUbicacion.Text + "\n";
            precioTotal = TempPrecio;
            if(n_LoadProduct.insertWindows(description,RutaImagen, ClsWindows.Weight, ClsWindows.heigt, cbVidrio.Text, cbColor.Text, "", precioTotal, ClsWindows.IDQuote, ClsWindows.System, ClsWindows.Desing)) 
            {
                LimpiarCampos();
                MessageBox.Show("Ventana guardada correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is frmQuote);
                if (frm != null)
                {
                    ((frmQuote)frm).loadWindows();
                }
            }
        }

        private void LimpiarCampos() 
        {
            txtAlto.Text = "0";
            txtAncho.Text = "0";
            txtTotal.Text = "";
            txtTotalPrice.Text = "";
            textBox1.Text = "0";
            cbColor.SelectedIndex = 0;
            cbSupplier.SelectedIndex = 0;
            cbVidrio.SelectedIndex = 0;
            Aluminiodt.DataSource = null;
            Vidriodt.DataSource = null;
            dgvAccesorios.DataSource = null;
        }

        private void txtCantidad_ValueChanged(object sender, EventArgs e)
        {
           TempPrecio = precioTotal * Convert.ToDecimal(txtCantidad.Value);
            txtTotalPrice.Text = TempPrecio.ToString("C");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            panelDetalle.Enabled = false;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            panelDetalle.Visible = false;
        }

        private void cbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSupplier.SelectedIndex == 2)
            {
                txtTotal.Enabled = true;
            }
            else
            {
                txtTotal.Enabled = false;
            }
        }

        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            panelDetalle.Visible = false;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
