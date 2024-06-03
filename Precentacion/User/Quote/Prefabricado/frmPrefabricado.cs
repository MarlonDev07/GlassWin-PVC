using Dominio.Model.ClassWindows;
using Negocio;
using Negocio.Accesorios;
using Negocio.LoadProduct;
using Precentacion.User.Quote.Accesorios;
using Precentacion.User.Quote.Quote;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Precentacion.User.Quote.Prefabricado
{
    public partial class frmPrefabricado : MaterialSkin.Controls.MaterialForm
    {
        private string UrlImagen = "";
        private bool Inicializado = false;
        private decimal GranTotal = 0;
        public frmPrefabricado()
        {
            InitializeComponent();
            txtAlto.Text = "0";
            txtAncho.Text = "0";
            ConfigDataGrid();
            
        }
        #region Carga Inicial
        private void ConfigDataGrid() 
        {
            try
            {
                //Ajustar el ancho de las columnas al ancho del datagrid
                dgvPrefabricado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvPrefabricado.AutoGenerateColumns = false;
                dgvPrefabricado.ColumnCount = 6;
                dgvPrefabricado.Columns[0].HeaderText = "ID";
                dgvPrefabricado.Columns[0].DataPropertyName = "IdPrefabricado";

                dgvPrefabricado.Columns[1].HeaderText = "Nombre";
                dgvPrefabricado.Columns[1].DataPropertyName = "Nombre";

                dgvPrefabricado.Columns[2].HeaderText = "Cantidad";
                dgvPrefabricado.Columns[2].DataPropertyName = "Cantidad";

                dgvPrefabricado.Columns[3].HeaderText = "Metraje";
                dgvPrefabricado.Columns[3].DataPropertyName = "Metraje";

                dgvPrefabricado.Columns[4].HeaderText = "Precio";
                dgvPrefabricado.Columns[4].DataPropertyName = "Precio";

                dgvPrefabricado.Columns[5].HeaderText = "PrecioTotal";
                dgvPrefabricado.Columns[5].DataPropertyName = "PrecioTotal";

                //Agregar Una Linea en Blanco
                dgvPrefabricado.Rows.Add();
                dgvPrefabricado.Rows[0].Cells[0].Value = "";
                dgvPrefabricado.Rows[0].Cells[1].Value = "";
                dgvPrefabricado.Rows[0].Cells[2].Value = "0";
                dgvPrefabricado.Rows[0].Cells[3].Value = "";
                dgvPrefabricado.Rows[0].Cells[4].Value = "0";
                dgvPrefabricado.Rows[0].Cells[5].Value = "";
                //Ocultar la Columna de ID

                Inicializado = true;
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
            
           

        }
        #endregion

        private void btnCargarImagen_Click(object sender, EventArgs e)
        {
            //Cargar la Imagen del Producto
            try
            {
                OpenFileDialog BuscarImagen = new OpenFileDialog();
                BuscarImagen.Filter = "Archivos de Imagen|*.jpg;*.jpeg;*.png";
                BuscarImagen.FileName = "";
                BuscarImagen.Title = "Seleccione la Imagen del Producto";
                if (BuscarImagen.ShowDialog() == DialogResult.OK)
                {
                    UrlImagen = BuscarImagen.FileName;
                    pbPrefabricado.Image = Image.FromFile(UrlImagen);
                    //Ajustar la Imagen al PictureBox
                    pbPrefabricado.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            //Abrir la Lista de Artiulos
            frmListaAcesorios frm = new frmListaAcesorios();
            frm.Show();
        }


        private void dgvPrefabricado_KeyDown(object sender, KeyEventArgs e)
        {
            //Validar que se Precione la tecla Enter en la Columna de ID
            if (e.KeyValue == (char)Keys.Enter)
            {
               
            }
        }

        private void dgvPrefabricado_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //Validar que el DataGridView este Inicializado
            if (Inicializado)
            {
                //Validar que los TextBox de Alto y Ancho tengan un valor
                if (txtAncho.Text != "")
                {
                    if (txtAlto.Text != "")
                    {
                        try
                        {
                            if (dgvPrefabricado.CurrentRow.Cells[0].Value != null)
                            {
                                //Celda de la Columna Id del DataGridView Cambio
                                N_Accesorios n_Accesorios = new N_Accesorios();
                                List<object> Articulo = n_Accesorios.Articulo(Convert.ToInt32(dgvPrefabricado.CurrentRow.Cells[0].Value));
                                if (Articulo.Count > 0)
                                {
                                    dgvPrefabricado.CurrentRow.Cells[1].Value = Articulo[0];
                                    dgvPrefabricado.CurrentRow.Cells[4].Value = Articulo[1];
                                    //Sacar el Metraje
                                    dgvPrefabricado.CurrentRow.Cells[3].Value = Convert.ToDecimal(txtAlto.Text) * Convert.ToDecimal(txtAncho.Text);
                                    //Sacar el Precio Total
                                    dgvPrefabricado.CurrentRow.Cells[5].Value = (Convert.ToDecimal(dgvPrefabricado.CurrentRow.Cells[3].Value) * Convert.ToDecimal(dgvPrefabricado.CurrentRow.Cells[4].Value)) * Convert.ToDecimal(dgvPrefabricado.CurrentRow.Cells[2].Value);
                                    
                                    CalcularGranTotal();
                                }
                                else
                                {
                                    MessageBox.Show("No se encontro el Articulo, Verifique el Codigo");
                                }
                            }                     
                        }
                        catch (Exception ex)
                        {
                           
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe Ingresar el Alto Primero");
                        txtAlto.Focus();
                        //Limpiar el DataGridView
                        dgvPrefabricado.Rows.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Debe Ingresar el Ancho Primero");
                    txtAncho.Focus();
                    //Limpiar el DataGridView
                    dgvPrefabricado.Rows.Clear();
                }
            }
            
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            //Actualizar el Metraje y el Precio Total
            try
            {
                if (txtAlto.Text != "")
                {
                    if (txtAncho.Text != "")
                    {
                        PuntoDecimal();
                        for (int i = 0; i < dgvPrefabricado.Rows.Count; i++)
                        {
                            dgvPrefabricado.Rows[i].Cells[3].Value = Convert.ToDecimal(txtAlto.Text) * Convert.ToDecimal(txtAncho.Text);
                            dgvPrefabricado.Rows[i].Cells[5].Value = (Convert.ToDecimal(dgvPrefabricado.Rows[i].Cells[3].Value) * Convert.ToDecimal(dgvPrefabricado.Rows[i].Cells[4].Value)) * Convert.ToDecimal(dgvPrefabricado.Rows[i].Cells[2].Value);
                        }
                        CalcularGranTotal();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CalcularGranTotal()
        {
            //Calcular el Gran Total
            try
            {
                GranTotal = 0;
                for (int i = 0; i < dgvPrefabricado.Rows.Count; i++)
                {
                    GranTotal += Convert.ToDecimal(dgvPrefabricado.Rows[i].Cells[5].Value);
                }
                txtTotal.Text = GranTotal.ToString("c");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidacionCampos())
            {
                N_LoadProduct n_LoadProduct = new N_LoadProduct();
                //Crear una Descripcion del Producto con saltos de linea
                string Descripcion = "";
                Descripcion += "Prefabricado: " + txtNombre.Text + "\n";
                Descripcion += "Alto: " + txtAlto.Text + "\n";
                Descripcion += "Ancho: " + txtAncho.Text + "\n";
                Descripcion += "Total: " + txtTotal.Text + "\n";

                //Convertir Ancho y alto a Decimal
                decimal Ancho = Convert.ToDecimal(txtAncho.Text);
                decimal Alto = Convert.ToDecimal(txtAlto.Text);

                if (n_LoadProduct.insertWindows(Descripcion, UrlImagen, Ancho, Alto, "", "", "", GranTotal, ClsWindows.IDQuote, "Prefabricado", "Personalizado"))
                {
                    MessageBox.Show("Producto Guardado Correctamente");
                    Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is frmQuote);
                    if (frm != null)
                    {
                        ((frmQuote)frm).loadWindows();
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al Guardar el Producto");
                }
            }
        }

        private void txtAncho_KeyPress(object sender, KeyPressEventArgs e)
        {
           //Validar que se precione Enter 
           if (e.KeyChar == (char)Keys.Enter)
            {
                //Validar que el TextBox de Ancho tenga un valor
                if (txtAncho.Text != "")
                {
                    //Poner el Foco en el TextBox de Alto
                    txtAlto.Focus();
                }
                else
                {
                    MessageBox.Show("Debe Ingresar el Ancho Primero");
                    txtAncho.Focus();
                }
            }
        }

        private void txtAlto_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Validar que se precione Enter 
            if (e.KeyChar == (char)Keys.Enter)
            {
                //Validar que el TextBox de Alto tenga un valor
                if (txtAlto.Text != "")
                {
                    //Poner el Foco en el DataGridView
                    dgvPrefabricado.Focus();
                }
                else
                {
                    MessageBox.Show("Debe Ingresar el Alto Primero");
                    txtAlto.Focus();
                }
            }
        }

        //VALIDAR QUE TODOS LOS CAMPOS ESTEN LLENOS
        private bool ValidacionCampos() 
        {
            if (txtNombre.Text == "")
            {
                MessageBox.Show("Debe Ingresar el Nombre del Producto");
                txtNombre.Focus();
                return false;
            }
            if (txtAncho.Text == "")
            {
                MessageBox.Show("Debe Ingresar el Ancho del Producto");
                txtAncho.Focus();
                return false;
            }
            if (txtAlto.Text == "")
            {
                MessageBox.Show("Debe Ingresar el Alto del Producto");
                txtAlto.Focus();
                return false;
            }
            if (dgvPrefabricado.Rows.Count == 0)
            {
                MessageBox.Show("Debe Ingresar al menos un Accesorio");
                return false;
            }
            if (UrlImagen == "")
            {
                MessageBox.Show("Debe Ingresar la Imagen del Producto");
                return false;
            }
            return true;
        }

        //Cambiar . por , en los TextBox de Ancho y Alto
        private void PuntoDecimal()
        {
            if (txtAncho.Text != "")
            {
                txtAncho.Text = txtAncho.Text.Replace(".", ",");
                //POSICIONAR EL CURSOR AL FINAL DEL TEXTO
                txtAncho.SelectionStart = txtAncho.Text.Length;
            }
            if (txtAlto.Text != "")
            {
                txtAlto.Text = txtAlto.Text.Replace(".", ",");
                //POSICIONAR EL CURSOR AL FINAL DEL TEXTO
                txtAlto.SelectionStart = txtAlto.Text.Length;
            }
        }
    }
}
