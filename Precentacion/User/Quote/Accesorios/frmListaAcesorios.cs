using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            if (Categoria == "Vidrio" || Categoria == "Arenado")
            {
                //Preguntar Cuantos Vidrios se van a agregar
                frmMedidasVidrio frm = new frmMedidasVidrio();
                ((frmMedidasVidrio)frm).Precio = Convert.ToDecimal(dgvAccesorios.CurrentRow.Cells[12].Value.ToString());
                frm.ShowDialog();

                //Crear la Descripcion del Articulo
                string description = CreateDescription(CantidadVidrios,"Vidrio");
                //Crear la URL de la Imagen
                string url = CreateURL();
                //Obtener Color del DataGrid
                string Color = dgvAccesorios.CurrentRow.Cells[7].Value.ToString();
        
                //Agregar el Articulo a la Proforma
                if (load.insertWindows(description, url, 0, 0, "", Color, "", PrecioVidrio, ClsWindows.IDQuote, "", "")) ;
                {
                    Form frmQ = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is frmQuote);
                    if (frmQ != null)
                    {
                        ((frmQuote)frmQ).loadWindows();
                    }
                    MessageBox.Show("Articulo Agregado");
                }

            }
            else 
            {

                //Preguntar Cuantos Accesorios se van a agregar
                Decimal Precio = 0;
                string Metraje = "0";
                string Cantidad = InputBox.Show("Digite la Cantidad de Articulos que desea Agregar", "Cantidad");
                if (dgvAccesorios.CurrentRow.Cells[3].Value.ToString() == "Aluminio")
                {
                    //Preguntar Cuantos Metros se van a agregar
                     Metraje = InputBox.Show("Digite la Medida de Articulos que desea Agregar", "Metraje");
                }
                
               
                //Verificar si el Usuario Ingreso un Valor y ademas que sea un Numero
                if (Cantidad != "" && int.TryParse(Cantidad, out int result))
                {
                    //Verificar si el Usuario Ingreso un Valor Mayor a 0
                    if (int.Parse(Cantidad) > 0)
                    {
                        //Crear la Descripcion del Articulo
                        string description = CreateDescription(Int16.Parse(Cantidad),"");
                        //Crear la URL de la Imagen
                        string url = CreateURL();
                        //Obtener Color del DataGrid
                        string Color = dgvAccesorios.CurrentRow.Cells[7].Value.ToString();
                        try
                        {
                            //Obtener el Precio Total
                             Precio = CalculoPrecio(int.Parse(Cantidad), decimal.Parse(Metraje));
                        }
                        catch (Exception)
                        {

                            MessageBox.Show("El Valor Ingresado no es Valido Porfavor igresar solo numeros y Comas No Puntos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //Para la Ejecucion del Metodo
                            return;
                        }
                        
                        //Agregar el Articulo a la Proforma

                        if (load.insertWindows(description, url, 0, 0, "", Color, "", Precio, ClsWindows.IDQuote, "", "")) ;
                        {
                            Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is frmQuote);
                            if (frm != null)
                            {
                                ((frmQuote)frm).loadWindows();
                            }
                            MessageBox.Show("Articulo Agregado");
                        }
                    }
                    else
                    {
                        MessageBox.Show("El Valor Ingresado no es Valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Solo se aceptan Numero, Verifique e Intente de Nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string Color = dgvAccesorios.CurrentRow.Cells[7].Value.ToString();

                description += "Nombre: " + name + "\n";
                description += "Color: " + Color + "\n";
                description += "Cantidad: " + Cantidad + "\n";
               
            }
           
           
            return description;
        }
        private string CreateURL()
        {
            //Obtener la ruta relativa de la Imagen de la ventana
            string url = "";
            url = "Images\\Accesorios\\" +dgvAccesorios.CurrentRow.Cells[1].Value.ToString().Trim()+ ".jpeg";
            return url;
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
