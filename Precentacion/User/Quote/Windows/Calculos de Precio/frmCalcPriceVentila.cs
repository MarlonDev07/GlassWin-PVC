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

namespace Precentacion.User.Quote.Windows.Calculos_de_Precio
{
    public partial class frmCalcPriceVentila : MaterialSkin.Controls.MaterialForm
    {
        string RutaImagen;
        decimal precioTotal;
        decimal TempPrecio;
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

        private void txtAncho_TextChanged(object sender, EventArgs e)
        {
            if (txtAncho.Text == "")
            {
               
            }else
            {
                ClsWindows.Weight = Convert.ToDecimal(txtAncho.Text);
            }
        }

        private void txtAlto_TextChanged(object sender, EventArgs e)
        {
           if (txtAlto.Text == "")
            {
               
            }
            else
            {
                ClsWindows.heigt = Convert.ToDecimal(txtAlto.Text);
            }
        }

        private void btnDesglose_Click(object sender, EventArgs e)
        {
            panelDetalle.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panelDetalle.Visible = false;
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
    }
}
