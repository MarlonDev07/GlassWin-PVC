﻿using Dominio.Model.ClassWindows;
using Negocio.LoadProduct;
using Precentacion.User.Quote.Quote;
using System;
using System.Data;
using System.Drawing;
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
        N_LoadProduct n_LoadProduct = new N_LoadProduct();
        #endregion

        #region Constructor
        public frmCalcPriceVentanasFijas()
        {
            InitializeComponent();
            frmCalcPriceVentanasFijas_Load(null, null);
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
            cbSupplier.SelectedIndex = 0;
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
        private void btnCargar_Click(object sender, EventArgs e)
        {
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
                ValidarPunto();
                if (txtAlto.Text != "")
                {
                    ClsWindows.heigt = Convert.ToDecimal(txtAlto.Text);
                }
            }
            catch (Exception)
            {
                    MessageBox.Show("El Valor Ingresado no es Valido", "Valor no Valido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAlto.Text = "";
                    txtAlto.Focus();
               
            }
            
        }

        private void txtAncho_textChanged(object sender, EventArgs e)
        {
            try
            {
                ValidarPunto();
                if (txtAncho.Text != "")
                {
                    ClsWindows.Weight = Convert.ToDecimal(txtAncho.Text);
                }
                
            }
            catch (Exception)
            {
                if (txtAncho.Text != "")
                {
                    MessageBox.Show("El Valor Ingresado no es Valido", "Valor no Valido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAncho.Text = "";
                    txtAncho.Focus();
                }
               
            }
            
        }


        #endregion

        #region Metodos
        private void CargarPrecio()
        {
            try
            {
                Decimal PrecioAluminio = 0;
               //Obtener Precios de los DataGrid
               for (int i = 0; i < Aluminiodt.Rows.Count; i++)
               {
                    //Validar que sea la ultima fila
                    if (i != Aluminiodt.Rows.Count - 1)
                    {
                        PrecioAluminio = PrecioAluminio + Convert.ToDecimal(Aluminiodt.Rows[i].Cells[3].Value.ToString());
                    }
               }
               Decimal PrecioVidrio = 0;
               for (int i = 0; i < Vidriodt.Rows.Count; i++)
               {
                    //Validar que sea la ultima fila
                    if (i != Vidriodt.Rows.Count - 1)
                    {
                        PrecioVidrio = Convert.ToDecimal(Vidriodt.Rows[i].Cells[3].Value.ToString());
                    }
               }
               decimal Accesorios = 0;
                for(int i = 0; i < dgvAccesorios.Rows.Count; i++)
                {
                    //Validar que sea la ultima fila
                    if (i != dgvAccesorios.Rows.Count - 1)
                    {
                        Accesorios = Accesorios + Convert.ToDecimal(dgvAccesorios.Rows[i].Cells[3].Value.ToString());
                    }
                }
               decimal Subtotal = PrecioAluminio + PrecioVidrio + Accesorios;

                //Cargar el Ajuste de Precio
                string Descripcion = ClsWindows.System+ClsWindows.Desing;
              
                //Añadir el Tipo de Color
                Descripcion += cbColor.Text;

                decimal Ajuste = n_LoadProduct.LoadAjustePrecio(cbSupplier.Text, Descripcion);

                //Calcular Precio Total
                PrecioTotal = Subtotal + (Subtotal * Ajuste);
                txtTotal.Text = PrecioTotal.ToString("c");
            }
            catch (Exception)
            {
                MessageBox.Show("No se Encontro el Precio", "Precio no Disponible", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
#pragma warning restore CS0168 // La variable está declarada pero nunca se usa
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
            TempPrecio = 0;
            TempPrecio = PrecioTotal * Convert.ToDecimal(txtCantidad.Value);
            txtTotal.Text = TempPrecio.ToString("c");
        }

        private void lblDescripcion_Click(object sender, EventArgs e)
        {

        }
    }
}
