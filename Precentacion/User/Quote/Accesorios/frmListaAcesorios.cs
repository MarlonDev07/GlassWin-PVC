using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio.ClassFunction.InputBox;
using Dominio.Model.ClassWindows;
using iTextSharp.text.pdf;
using Negocio.Company.Quote;
using Negocio.LoadProduct;
using Negocio.Products;
using Precentacion.User.Quote.Quote;

namespace Precentacion.User.Quote.Accesorios
{
    public partial class frmListaAcesorios : MaterialSkin.Controls.MaterialForm
    {
        N_Products loadProduct = new N_Products();
        N_Quote quote = new N_Quote();
        N_LoadProduct load = new N_LoadProduct();
        public Decimal PrecioVidrio;
        public Int16 CantidadVidrios;
        public frmListaAcesorios()
        {
            InitializeComponent();
            dgvAccesorios.DataSource = loadProduct.View();

        }

        private void agregarAProformaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = dgvAccesorios.CurrentRow.Cells[1].Value.ToString();
            string Categoria = dgvAccesorios.CurrentRow.Cells[3].Value.ToString();

            // Crear la URL de la Imagen
            string url = CreateURL();
            Console.WriteLine($"URL creada: {url}");

            if (Categoria == "Vidrio" || Categoria == "Arenado")
            {
                frmMedidasVidrio frm = new frmMedidasVidrio();
                ((frmMedidasVidrio)frm).Precio = Convert.ToDecimal(dgvAccesorios.CurrentRow.Cells[12].Value.ToString());
                frm.ShowDialog();

                string description = CreateDescription(CantidadVidrios, "Vidrio");
                string Color = dgvAccesorios.CurrentRow.Cells[7].Value.ToString();

                // Declarar una variable para capturar el mensaje de error
                string errorMessage;

                // Llamar al método insertWindows y pasar 'out' para capturar el mensaje de error
                if (load.insertWindows(description, url, 0, 0, "", Color, "", PrecioVidrio, ClsWindows.IDQuote, "", "", out errorMessage))
                {
                    // Si el formulario de cotización está abierto, recargar las ventanas
                    Form frmQ = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is frmQuote);
                    if (frmQ != null)
                    {
                        ((frmQuote)frmQ).loadWindows();
                    }
                    MessageBox.Show("Artículo Agregado");
                }
                else
                {
                    // Mostrar el mensaje de error específico si ocurre una falla
                    MessageBox.Show("Error al agregar el artículo: " + errorMessage);
                }

            }
            else
            {
                string Cantidad = InputBox.Show("Digite la Cantidad de Artículos que desea Agregar", "Cantidad");
                string Metraje = "0";
                if (dgvAccesorios.CurrentRow.Cells[3].Value.ToString() == "Aluminio")
                {
                    Metraje = InputBox.Show("Digite la Medida de Artículos que desea Agregar", "Metraje");
                }

                if (Cantidad != "" && int.TryParse(Cantidad, out int result))
                {
                    if (int.Parse(Cantidad) > 0)
                    {
                        string description = CreateDescription(Int16.Parse(Cantidad), "");
                        string Color = dgvAccesorios.CurrentRow.Cells[7].Value.ToString();
                        try
                        {
                            decimal Precio = CalculoPrecio(int.Parse(Cantidad), decimal.Parse(Metraje));
                            string errorMessage; // Variable para capturar el mensaje de error

                            // Intentar insertar el artículo
                            if (load.insertWindows(description, url, 0, 0, "", Color, "", Precio, ClsWindows.IDQuote, "", "", out errorMessage))
                            {
                                // Actualiza el DataGrid si el formulario frmQuote está abierto
                                Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is frmQuote);
                                if (frm != null)
                                {
                                    ((frmQuote)frm).loadWindows();
                                }
                                MessageBox.Show("Artículo Agregado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                // Muestra el mensaje de error específico
                                MessageBox.Show("Error al agregar el artículo: " + errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        catch (Exception)
                        {
                            MessageBox.Show("El Valor Ingresado no es Válido. Por favor, ingresar solo números y comas, no puntos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("El Valor Ingresado no es Válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Solo se aceptan Números. Verifique e Intente de Nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private string CreateDescription(Int16 Cantidad,string opc)
        {
            //crear descripcion del Articulo 
            string description = "";
            if (opc == "Vidrio")
            {
                //Obtener del DataGrid el nombre del Accesorio
                string name = dgvAccesorios.CurrentRow.Cells[1].Value.ToString();
                string Color = dgvAccesorios.CurrentRow.Cells[7].Value.ToString();

                description += "Nombre: " + name + "\n";
                description += "Ancho: " + ClsWindows.Weight + "\n";
                description += "Alto: " + ClsWindows.heigt + "\n";
                description += "Cantidad: " + Cantidad + "\n";
            }
            else
            {
                //Obtener del DataGrid el nombre del Accesorio
                string name = dgvAccesorios.CurrentRow.Cells[1].Value.ToString();
                string Color = dgvAccesorios.CurrentRow.Cells[9].Value.ToString();

                description += "Nombre: " + name + "\n";
                description += "Color: " + Color + "\n";
                description += "Cantidad: " + Cantidad + "\n";
               
            }
           
           
            return description;
        }
        private string CreateURL()
        {
            // Obtener la ruta relativa de la imagen de la ventana
            string rutaRelativa = "Images\\Accesorios\\" + dgvAccesorios.CurrentRow.Cells[1].Value.ToString().Trim() + ".jpeg";

            // Obtener el directorio de trabajo actual
            string directorioDeTrabajo = Directory.GetCurrentDirectory();

            // Convertir la ruta relativa a una ruta absoluta
            string rutaAbsoluta = Path.Combine(directorioDeTrabajo, rutaRelativa);
            rutaAbsoluta = Path.GetFullPath(rutaAbsoluta);

            // Imprimir la ruta absoluta para depuración
            Console.WriteLine($"Ruta absoluta: {rutaAbsoluta}");

            return rutaAbsoluta;
        }

        private Decimal CalculoPrecio(int Cantidad, decimal Metraje) 
        {
            //Obtener Precio
            Decimal Precio = Decimal.Parse(dgvAccesorios.CurrentRow.Cells[12].Value.ToString());
            //Obtener Nombre del Accesorio
            string name = dgvAccesorios.CurrentRow.Cells[1].Value.ToString();
            //Calcular el Precio Total
            decimal PrecioFinal = 0;
            if(Metraje == 0) 
            {
                PrecioFinal = Precio * Cantidad;
            }
            else
            {
                PrecioFinal = (Precio * Metraje) * Cantidad;
            }
            return  PrecioFinal;
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dgvAccesorios.CurrentCell = null;
            try
            {
                foreach (DataGridViewRow r in dgvAccesorios.Rows)
                {
                    bool rowVisible = false;
                    foreach (DataGridViewCell c in r.Cells)
                    {
                        if (c.Value != null && c.Value.ToString().ToUpper().Contains(textBox1.Text.ToUpper()))
                        {
                            rowVisible = true;
                            break;
                        }
                    }
                    r.Visible = rowVisible;
                }
            }
            catch (Exception)
            {
               
            }
        }
    }
}
