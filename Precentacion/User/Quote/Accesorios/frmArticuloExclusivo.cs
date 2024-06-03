using Dominio.Model.ClassWindows;
using Negocio.LoadProduct;
using Precentacion.User.Quote.Quote;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Precentacion.User.Quote.Accesorios
{
    public partial class frmArticuloExclusivo : MaterialSkin.Controls.MaterialForm
    {
        #region Variables
        private string rutaImagen;
        private string Precio;
        #endregion

        #region Constructor
        public frmArticuloExclusivo()
        {
            InitializeComponent();
            CargarDatostxtDescripcion();
        }
        #endregion

        #region Cargas Iniciales
        private void CargarDatostxtDescripcion() 
        {
            txtDescripcion.Text = "Nombre: " + Environment.NewLine;

            txtDescripcion.Text += "Color: " + Environment.NewLine;

            txtDescripcion.Text += "Ancho: " + Environment.NewLine;

            txtDescripcion.Text += "Alto: " + Environment.NewLine;

            txtDescripcion.Text += "Vidrio: " + Environment.NewLine; 
        }
        #endregion

        #region Botones
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            N_LoadProduct SaveWindows = new N_LoadProduct();
            try
            {
                if (ValidarCampos() == true)
                {
                    if (SaveWindows.insertWindows(txtDescripcion.Text, rutaImagen, 0, 0, "", "", "", Convert.ToDecimal(txtPrecio.Text), ClsWindows.IDQuote, "", ""))
                    {
                        Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is frmQuote);
                        if (frm != null)
                        {
                            ((frmQuote)frm).loadWindows();
                        }
                        MessageBox.Show("Articulo Agregado");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrio un error al guardar el articulo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void btnInsertarImagen_Click(object sender, EventArgs e)
        {
            try
            {
                //Buscamos la imagen en una Carpeta Especifica

                OpenFileDialog dialog = new OpenFileDialog();
                //Filtramos para que solo se muestren los archivos de tipo Imagen
                dialog.Filter = "Archivos de Imagen|*.jpg;*.gif;*.png;*.bmp;*.jpeg";

                //Abrir la Ruta de Accesorios Exclusivos
                
                    //Obtener la Ruta Principal
                    string ruta = Application.StartupPath;

                    //Abrir la Ruta de Accesorios Exclusivos
                    dialog.InitialDirectory = ruta + "\\Images\\Articulos Exclusivos";

                DialogResult result = dialog.ShowDialog();
                //Si seleccionamos una imagen
                if (result == DialogResult.OK)
                {
                    //Asignamos la imagen a nuestro PictureBox
                    pbAccesorioExclusivo.Image = Image.FromFile(dialog.FileName);

                    //Ajustamos la imagen al tamaño del PictureBox
                    pbAccesorioExclusivo.SizeMode = PictureBoxSizeMode.StretchImage;

                    //Guardamos la ruta de la imagen
                    rutaImagen = "Images\\Articulos Exclusivos\\"+dialog.SafeFileName;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrio un error al cargar la imagen", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        #endregion

        #region Funciones
        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                MessageBox.Show("El campo de la descripción no puede estar vacio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(txtPrecio.Text))
            {
                MessageBox.Show("El campo del precio no puede estar vacio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(rutaImagen))
            {
                MessageBox.Show("Debe seleccionar una imagen", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        #endregion

       
    }
}
