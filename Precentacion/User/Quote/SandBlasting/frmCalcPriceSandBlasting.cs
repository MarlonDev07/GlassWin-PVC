using Dominio.Model.ClassPreciosSandBlasting;
using Dominio.Model.ClassWindows;
using Negocio.LoadProduct;
using Precentacion.User.Quote.Quote;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Precentacion.User.Quote.SandBlasting
{
    public partial class frmCalcPriceSandBlasting : Form
    {
        string RutaIMagen;
        decimal SubTotal;
        public frmCalcPriceSandBlasting()
        {
            InitializeComponent();
            Fn_Iniciales();
        }
        #region Funciones Iniciales
        private void Fn_Iniciales() 
        {
            Fn_CargarCbCategoria();
            Fn_ConfigurarDGV();
        }

        private void Fn_CargarCbCategoria() 
        {
            try
            {
                //Cargar los Nombre de la Carpetas que estan en el Directorio base del Sistema en la Carpeta Imagenes, de Sanblasting en el ComboBox
                string[] carpetas = System.IO.Directory.GetDirectories(Application.StartupPath + @"\Images\SandBlasting");
                //Cargar los Nombres en el ComboBox
                foreach (string carpeta in carpetas)
                {
                    string[] nombre = carpeta.Split('\\');
                    cbCategoria.Items.Add(nombre[nombre.Length - 1]);
                }

                cbCategoria.SelectedIndex = 0;
            }
            catch (Exception)
            {

               MessageBox.Show("Error al cargar las Categorias de SandBlasting", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //this.Close();
            }
            

           
        }
        private void cbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fn_CargarCbDiseños();
        }

        private void Fn_CargarCbDiseños() 
        {
            try
            {
                //Cargar los Nombres de las Imagenes que estan en el Directorio base del Sistema en la Carpeta Imagenes, de Sanblasting en el ComboBox
                string[] archivos = System.IO.Directory.GetFiles(Application.StartupPath + @"\Images\SandBlasting\" + cbCategoria.Text);
                //Cargar los Nombres en el ComboBox
                cbDiseño.Items.Clear();
                //Ordenar los Nombres
                Array.Sort(archivos);
                //Cargar los Nombres en el ComboBox
                foreach (string archivo in archivos)
                {
                    string[] nombre = archivo.Split('\\');
                    cbDiseño.Items.Add(nombre[nombre.Length - 1]);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar los Diseños de SandBlasting", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cbDiseño_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Cargar la Imagen del Diseño en el PictureBox
            try
            {
                pbDiseño.Image = Image.FromFile(Application.StartupPath + @"\Images\SandBlasting\" + cbCategoria.Text + @"\" + cbDiseño.Text);

                //Guardar la Ruta de la Imagen
                RutaIMagen = Application.StartupPath + @"\Images\SandBlasting\" + cbCategoria.Text + @"\" + cbDiseño.Text;
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar la Imagen del Diseño", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Fn_ConfigurarDGV()
        {
            dgvDesglose.ColumnCount = 2;
            dgvDesglose.Columns[0].Name = "Concepto";
            dgvDesglose.Columns[1].Name = "Precio";

            //Ajustar el Ancho de las Columnas al Ancho del DataGridView
            dgvDesglose.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Ocultar el Panel Desglose
            PanelDesglose.Visible = false;
        }

        #endregion

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            //Limpiar el DataGridView
            dgvDesglose.Rows.Clear();

            //Obtener Alto y Ancho cambiando el punto por la coma
            decimal alto = Convert.ToDecimal(txtAlto.Text.Replace('.', ','));
            decimal ancho = Convert.ToDecimal(txtAncho.Text.Replace('.', ','));

            //Obtener el Nombre de la Categoria Quitando los Espacios
            string categoria = cbCategoria.Text.Replace(" ", "");

            //Obtener el Precio de la Categoria desde la clase de Dominio
            decimal precioDiseño = clsPricioSB.ObtenerPrecio(categoria);

            //Agregar el Precio del Desglose al DataGridView
            dgvDesglose.Rows.Add("Diseño", precioDiseño.ToString("c"));

            //Obtener el Precio del Servicio de SandBlasting
            decimal precioServicio = (ancho*alto)* clsPricioSB.ObtenerPrecio("ServicioDiseñoArenado");

            //Agregar el Precio del Desglose al DataGridView
            dgvDesglose.Rows.Add("Servicio de Diseño Arenado", precioServicio.ToString("c"));

            //Obtener el Precio Total
             SubTotal = precioDiseño + precioServicio;

            //Validar si LLeva Sello
            if (ckSello.Checked)
            {
                SubTotal += clsPricioSB.ObtenerPrecio("ServicioSello");
                //Agregar el Precio del Desglose al DataGridView
                dgvDesglose.Rows.Add("Servicio de Sello", clsPricioSB.ObtenerPrecio("ServicioSello").ToString("c"));
            }
            if (UserCache.Name != "VitroTaller")
            {
                //Mostrar el Precio Total
                txtSubTotal.Text = SubTotal.ToString("c");
            }
            else
            {
                txtSubTotal.Text = "Precio Restringido";
            }
            
        }

        private void btnDesglose_Click(object sender, EventArgs e)
        {
            if (UserCache.Name != "VitroTaller")
            {
                PanelDesglose.Visible = true;
            }
            else
            {
                MessageBox.Show("No tiene permisos para ver el desglose", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void btnOcultar_Click(object sender, EventArgs e)
        {
            //Ocultar el Panel
            PanelDesglose.Visible = false;
        }

        private void btnGuardadr_Click(object sender, EventArgs e)
        {
            //Guardar en base de datos
            try
            {
                //Crear Descripcion Con Saltos de Linea
                string descripcion = "Categoria: " + cbCategoria.Text + Environment.NewLine;
                descripcion += "Diseño: " + cbDiseño.Text + Environment.NewLine;
                descripcion += "Alto: " + txtAlto.Text + Environment.NewLine;
                descripcion += "Ancho: " + txtAncho.Text + Environment.NewLine;
                descripcion += "Sello: " + (ckSello.Checked ? "Si" : "No") + Environment.NewLine;
                descripcion += "Cantidad: 1" + Environment.NewLine;
             
                //Llamar al Metodo de Guardar
                N_LoadProduct objNegocio = new N_LoadProduct();
                if (objNegocio.insertWindows(descripcion, RutaIMagen, Convert.ToDecimal(txtAlto.Text),Convert.ToDecimal(txtAncho.Text), "","","", SubTotal,ClsWindows.IDQuote,"SandBlasting",cbDiseño.Text))
                {
                    MessageBox.Show("Guardado Correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Recargar el Formulario Quote
                    Form frmQ = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is frmQuote);
                    if (frmQ != null)
                    {
                        ((frmQuote)frmQ).loadWindows();
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al Guardar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnOcultar_Click_1(object sender, EventArgs e)
        {
            PanelDesglose.Visible = false;
        }

        private void txtAncho_TextChanged(object sender, EventArgs e)
        {
            // Obtener el texto actual
            string text = txtAncho.Text;

            // Reemplazar punto (.) por coma (,)
            if (text.Contains('.'))
            {
                text = text.Replace('.', ',');

                // Actualizar el texto del TextBox sin mover el cursor al final
                int cursorPosition = txtAncho.SelectionStart;
                txtAncho.Text = text;
                txtAncho.SelectionStart = cursorPosition;
            }
        }

        private void txtAlto_TextChanged(object sender, EventArgs e)
        {
            // Obtener el texto actual
            string text = txtAlto.Text;

            // Reemplazar punto (.) por coma (,)
            if (text.Contains('.'))
            {
                text = text.Replace('.', ',');

                // Actualizar el texto del TextBox sin mover el cursor al final
                int cursorPosition = txtAlto.SelectionStart;
                txtAlto.Text = text;
                txtAlto.SelectionStart = cursorPosition;
            }
        }
    }
}
