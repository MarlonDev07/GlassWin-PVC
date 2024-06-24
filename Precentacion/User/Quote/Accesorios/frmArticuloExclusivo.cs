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
            //CargarDatostxtDescripcion();
            AccesoriosUI.loadMaterial(this);
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
                    string Descripcion = txtDescripcion.Text;
                    Descripcion += Environment.NewLine;
                    Descripcion += "Exclusivo";

                    if (SaveWindows.insertWindows(Descripcion, rutaImagen, 0, 0, "", "", "", Convert.ToDecimal(txtPrecio.Text), ClsWindows.IDQuote, "", ""))
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
               //Abrir un dialogo para seleccionar la imagen
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Title = "Seleccione una imagen";
                dialog.Multiselect = false;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    rutaImagen = dialog.FileName;
                    pbAccesorioExclusivo.Image = Image.FromFile(rutaImagen);
                    pbAccesorioExclusivo.SizeMode = PictureBoxSizeMode.StretchImage;
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
            if (string.IsNullOrEmpty(txtDescripcion.Text)/* || !CamposDescripcionCompletos()*/)
            {
                MessageBox.Show("Todos los campos de la descripción deben estar completos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private bool CamposDescripcionCompletos()
        {
            string[] lineas = txtDescripcion.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            if (lineas.Length < 5) return false;

            // Verifica que cada campo tiene un valor después del título
            if (string.IsNullOrWhiteSpace(lineas[0].Substring("Nombre: ".Length))) return false;
            if (string.IsNullOrWhiteSpace(lineas[1].Substring("Color: ".Length))) return false;
            if (string.IsNullOrWhiteSpace(lineas[2].Substring("Ancho: ".Length))) return false;
            if (string.IsNullOrWhiteSpace(lineas[3].Substring("Alto: ".Length))) return false;
            if (string.IsNullOrWhiteSpace(lineas[4].Substring("Vidrio: ".Length))) return false;

            return true;
        }
        #endregion



    }
}
