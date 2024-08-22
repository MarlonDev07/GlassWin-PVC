using Dominio.Model.ClassWindows;
using Dominio.PriceProduct;
using Negocio.LoadProduct;
using Negocio.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Precentacion.User.Quote.Quote
{
    public partial class frmDesglose : Form
    {

        DataGridView dgvGlass = new DataGridView();

        public frmDesglose(DataGridView dgvGlass)
        {
            InitializeComponent();
            this.dgvGlass = dgvGlass;

            if (dgvDesglose.Rows.Count == 0)
            {
                CargarDesglose();
                ConfigDataGridDesglose();
                CargarTamañoPieza();
            }
        }





        private void CargarDesglose()
        {
            try
            {
                //Cargar en una lista Todos los Id de las Ventanas
                N_LoadProduct NLoadProduct = new N_LoadProduct();
                DataTable dtTotalDesglose = new DataTable();

                foreach (DataGridViewRow row in dgvGlass.Rows)
                {
                    DataTable dtAluminio = new DataTable();

                    //Validar que la celda no este vacia
                    if (row.Cells[0].Value == null)
                    {
                        continue;
                    }
                    //Obtener el Alto y Ancho de la Ventana
                    double Ancho = Convert.ToDouble(row.Cells[3].Value);
                    ClsWindows.Weight = Convert.ToDecimal(Ancho);

                    double Alto = Convert.ToDouble(row.Cells[4].Value);
                    ClsWindows.heigt = Convert.ToDecimal(Alto);

                    //Obtener el Color de la Ventana
                    string Color = row.Cells[6].Value.ToString();

                    //Obtener La cantidad de ventana el dato se encuentra en la descripcion
                    string DescripcionCantidad = row.Cells[1].Value.ToString();

                    string pattern = @"Cantidad:\s*(\d+)";
                    System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(DescripcionCantidad, pattern);
                    int Cantidad = Convert.ToInt32(match.Groups[1].Value);




                    // Patrón para extraer "Ancho Fijo"
                    string patternAnchoFijo = @"Ancho Fijo:\s*([\d,.]+)";
                    System.Text.RegularExpressions.Match matchAnchoFijo = System.Text.RegularExpressions.Regex.Match(DescripcionCantidad, patternAnchoFijo);
                    decimal anchoFijo = matchAnchoFijo.Success ? Convert.ToDecimal(matchAnchoFijo.Groups[1].Value) : 0;

                    // Patrón para extraer "Alto Fijo"
                    string patternAltoFijo = @"Alto Fijo:\s*([\d,.]+)";
                    System.Text.RegularExpressions.Match matchAltoFijo = System.Text.RegularExpressions.Regex.Match(DescripcionCantidad, patternAltoFijo);
                    decimal altoFijo = matchAltoFijo.Success ? Convert.ToDecimal(matchAltoFijo.Groups[1].Value) : 0;

                    // Patrón para extraer "Aluminio"
                    string patternMaterial = @"Aluminio:\s*(.+)";
                    System.Text.RegularExpressions.Match matchMaterial = System.Text.RegularExpressions.Regex.Match(DescripcionCantidad, patternMaterial);
                    string material = matchMaterial.Success ? matchMaterial.Groups[1].Value : string.Empty;

                    // Patrón para extraer "Divisiones"
                    string patternDivisiones = @"Divisiones:\s*(\d+)";
                    System.Text.RegularExpressions.Match matchDivisiones = System.Text.RegularExpressions.Regex.Match(DescripcionCantidad, patternDivisiones);
                    int divisiones = matchDivisiones.Success ? Convert.ToInt32(matchDivisiones.Groups[1].Value) : 0;

                    // Patrón para extraer "Vidrio"
                    string patternVidrioF = @"Vidrio Fijo:\s*(.+)";
                    System.Text.RegularExpressions.Match matchVidrio = System.Text.RegularExpressions.Regex.Match(DescripcionCantidad, patternVidrioF);
                    string vidrioFijo = matchVidrio.Success ? matchVidrio.Groups[1].Value : string.Empty;


                    //Obtener el Sistema de la Ventana
                    string Sistema = row.Cells[10].Value.ToString();
                    ClsWindows.System = Sistema;

                    //Obtener el Diseño de la Ventana
                    string Diseno = row.Cells[11].Value.ToString();
                    ClsWindows.Desing = Diseno;

                    //Obtener el Vidrio de la Ventana
                    string Vidrio = row.Cells[5].Value.ToString();


                    if (ClsWindows.System == "Vidrio Fijo")
                    {
                        string Descripcion = row.Cells[1].Value.ToString();
                        string Material = "";
                        if (Descripcion.Contains("1x2"))
                        {
                            Material = "1x2";
                        }
                        if (Descripcion.Contains("1 3/4x3"))
                        {
                            Material = "1 3/4x3";
                        }
                        if (Descripcion.Contains("1 3/4x4"))
                        {
                            Material = "1 3/4x4";
                        }
                        //Obtener el Total del Aluminio del Vidrio Fijo                      
                        dtAluminio = NLoadProduct.loadAluminioVentanaFijaDesglose(Color, Sistema, cbProveedorDesglose.Text, Material);

                        //Obtener el Metraje del dt Aluminio y Multiplicarlo por la Cantidad de Ventanas
                        foreach (DataRow item in dtAluminio.Rows)
                        {
                            item["Metraje"] = Convert.ToDecimal(item["Metraje"]) * Convert.ToDecimal(Cantidad);
                        }
                    }
                    else
                    {
                        // Verificar si el sistema es "Ventila"
                        if (anchoFijo > 0)
                        {
                            string Descripcion = row.Cells[1].Value.ToString();

                            // Obtener el Total del Aluminio del Vidrio Fijo                      
                            DataTable dtAluminioFijo = NLoadProduct.LoadAluminioFijoDesglose(Color, "Vidrio Fijo", cbProveedorDesglose.Text, anchoFijo, altoFijo, material, divisiones);

                            // Obtener el Metraje del dtAluminioFijo y multiplicarlo por la Cantidad de Ventanas
                            foreach (DataRow item in dtAluminioFijo.Rows)
                            {
                                item["Metraje"] = Convert.ToDecimal(item["Metraje"]) * Convert.ToDecimal(Cantidad);
                            }

                            // Agregar las filas del dtAluminioFijo al dtAluminio original
                            dtAluminio.Merge(dtAluminioFijo);
                        }

                        // Obtener el Total del Aluminio                     
                        DataTable dtAluminioSistema = NLoadProduct.loadAluminioDesglose(Color, Sistema, cbProveedorDesglose.Text);

                        // Obtener el Metraje del dtAluminioSistema y multiplicarlo por la Cantidad de Ventanas                    
                        foreach (DataRow item in dtAluminioSistema.Rows)
                        {
                            item["Metraje"] = Convert.ToDecimal(item["Metraje"]) * Convert.ToDecimal(Cantidad);
                        }

                        // Agregar las filas del dtAluminioSistema al dtAluminio original
                        dtAluminio.Merge(dtAluminioSistema);

                        // Eliminar filas donde la columna Metraje tenga un valor de 0
                        foreach (DataRow row2 in dtAluminio.Rows.Cast<DataRow>().ToList())
                        {
                            if (Convert.ToDecimal(row2["Metraje"]) == 0)
                            {
                                row2.Delete();
                            }
                        }

                        // Asegúrate de aceptar los cambios para que se eliminen las filas marcadas para eliminación
                        dtAluminio.AcceptChanges();
                    }
                    //Validar todos las Celdas del dtAluminio para agregarlas al dtTotalDesglose                    
                    if (dtTotalDesglose.Rows.Count == 0)
                    {
                        dtTotalDesglose = dtAluminio;
                    }
                    else
                    {
                        foreach (DataRow item in dtAluminio.Rows)
                        {
                            //Validar si la celda Description tiene el mismo valor y si es asi sumar el Metraje              
                            if (dtTotalDesglose.Select("Description = '" + item["Description"].ToString() + "'").Length > 0)
                            {
                                DataRow[] rows = dtTotalDesglose.Select("Description = '" + item["Description"].ToString() + "'");
                                rows[0]["Metraje"] = Convert.ToDecimal(rows[0]["Metraje"]) + Convert.ToDecimal(item["Metraje"]);

                            }
                            else
                            {
                                dtTotalDesglose.ImportRow(item);
                            }
                        }
                    }

                    //Obtener el Total de los Accesorios
                    DataTable dtAccesorios = new DataTable();

                    //Obtener el Total de los Accesorios            
                    dtAccesorios = NLoadProduct.loadAccesoriosDesglose(Sistema, cbProveedorDesglose.Text);

                    //Obtener el Metraje del dt Accesorios y Multiplicarlo por la Cantidad de Ventanas
                    foreach (DataRow item in dtAccesorios.Rows)
                    {
                        item["Metraje"] = Convert.ToDecimal(item["Metraje"]) * Convert.ToDecimal(Cantidad);


                    }

                    //Validar todos las Celdas del dtAccesorios para agregarlas al dtTotalDesglose                    
                    if (dtTotalDesglose.Rows.Count == 0)
                    {
                        dtTotalDesglose = dtAccesorios;
                    }
                    else
                    {
                        foreach (DataRow item in dtAccesorios.Rows)
                        {
                            //Validar si la celda Description tiene el mismo valor y si es asi sumar el Metraje                  
                            if (dtTotalDesglose.Select("Description = '" + item["Description"].ToString() + "'").Length > 0)
                            {
                                DataRow[] rows = dtTotalDesglose.Select("Description = '" + item["Description"].ToString() + "'");
                                rows[0]["Metraje"] = Convert.ToDecimal(rows[0]["Metraje"]) + Convert.ToDecimal(item["Metraje"]);


                            }
                            else
                            {
                                dtTotalDesglose.ImportRow(item);
                            }
                        }
                    }
                    // Declarar la variable dtVidrios antes de su uso
                    DataTable dtVidrios = new DataTable();

                    // Verificar si el sistema es "Ventila"
                    if (anchoFijo > 0)
                    {
                        string Descripcion = row.Cells[1].Value.ToString();

                        // Obtener el Total del Vidrio Fijo
                        DataTable dtVidriosFijo = NLoadProduct.LoadPriceNewGlassDesglose(cbProveedorDesglose.Text, vidrioFijo, anchoFijo, altoFijo);

                        // Obtener el Metraje del dtVidriosFijo y multiplicarlo por la Cantidad de Ventanas
                        foreach (DataRow item in dtVidriosFijo.Rows)
                        {
                            item["Metraje"] = Convert.ToDecimal(item["Metraje"]) * Convert.ToDecimal(Cantidad);
                        }

                        // Agregar las filas del dtVidriosFijo al dtVidrios original
                        dtVidrios.Merge(dtVidriosFijo);
                    }

                    // Obtener el Total de los Vidrios (del sistema general)
                    DataTable dtVidriosSistema = NLoadProduct.loadPricesGlassDesglose(cbProveedorDesglose.Text, Vidrio);

                    // Obtener el Metraje del dtVidriosSistema y multiplicarlo por la Cantidad de Ventanas
                    foreach (DataRow item in dtVidriosSistema.Rows)
                    {
                        item["Metraje"] = Convert.ToDecimal(item["Metraje"]) * Convert.ToDecimal(Cantidad);
                    }

                    // Agregar las filas del dtVidriosSistema al dtVidrios original
                    dtVidrios.Merge(dtVidriosSistema);

                    // Eliminar filas donde la columna Metraje tenga un valor de 0
                    foreach (DataRow row2 in dtVidrios.Rows.Cast<DataRow>().ToList())
                    {
                        if (Convert.ToDecimal(row2["Metraje"]) == 0)
                        {
                            row2.Delete();
                        }
                    }

                    // Asegúrate de aceptar los cambios para que se eliminen las filas marcadas para eliminación
                    dtVidrios.AcceptChanges();


                    // Validar todas las celdas del dtVidrios para agregarlas al dtTotalDesglose
                    if (dtTotalDesglose.Rows.Count == 0)
                    {
                        dtTotalDesglose = dtVidrios;
                    }
                    else
                    {
                        foreach (DataRow item in dtVidrios.Rows)
                        {
                            // Validar si la celda Description tiene el mismo valor y, si es así, sumar el Metraje
                            DataRow[] rows = dtTotalDesglose.Select("Description = '" + item["Description"].ToString() + "'");
                            if (rows.Length > 0)
                            {
                                rows[0]["Metraje"] = Convert.ToDecimal(rows[0]["Metraje"]) + Convert.ToDecimal(item["Metraje"]);
                            }
                            else
                            {
                                dtTotalDesglose.ImportRow(item);
                            }
                        }
                    }

                }
                //Eliminar del DataTable la fila que el Metraje sea 0
                dtTotalDesglose = dtTotalDesglose.Select("Metraje > 0").CopyToDataTable();

                //Cargar el dtTotalAluminio en el dgv
                dgvDesglose.DataSource = dtTotalDesglose;

                //Redonder todos los Metrajes a dos decimales 
                foreach (DataGridViewRow row in dgvDesglose.Rows)
                {
                    row.Cells[2].Value = Convert.ToDecimal(row.Cells[2].Value).ToString("N2");
                }

            }

            catch (Exception)
            {
                MessageBox.Show("Error al Cargar el Desglose", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void ConfigDataGridDesglose()
        {
            try
            {
                //Ocultar las columnas que no se necesitan
                //dgvDesglose.Columns[1].Visible = false;
                //dgvDesglose.Columns[2].Visible = false;
                //Agregar una Columna para el Tamaño
                DataGridViewTextBoxColumn Columna = new DataGridViewTextBoxColumn();
                Columna.HeaderText = "Tamaño";
                Columna.Name = "Tamaño";
                dgvDesglose.Columns.Add(Columna);

                //Agregar una Columna para el Cantidad Piezas 
                DataGridViewTextBoxColumn Columna2 = new DataGridViewTextBoxColumn();
                Columna2.HeaderText = "Cantidad Piezas";
                Columna2.Name = "Cantidad Piezas";
                Columna2.ReadOnly = true;
                dgvDesglose.Columns.Add(Columna2);

                //Eliminar la ultima fila la Que esta en Blanco
                dgvDesglose.AllowUserToAddRows = false;


            }
            catch (Exception)
            {
            }



        }

        private void CargarTamañoPieza()
        {
            try
            {
                List<PriceProductClass> Lista = CargarLista();

                if (Lista == null || Lista.Count == 0)
                {
                    //MessageBox.Show("La lista de precios está vacía.");
                    return;
                }

                N_Products products = new N_Products();
                DataTable dt = products.ObtenerTamañoPieza(Lista);

                // Verificar si el DataTable tiene filas
                if (dt == null || dt.Rows.Count == 0)
                {
                    //MessageBox.Show("No se encontraron datos para los productos especificados.");
                    return;
                }

                // Convertir el DataTable a un diccionario para una búsqueda rápida
                Dictionary<string, decimal> tamanos = new Dictionary<string, decimal>();
                foreach (DataRow row in dt.Rows)
                {
                    string nombre = row["Description"].ToString().Trim(); // Asegúrate de usar el nombre correcto de la columna de descripción
                    decimal tamaño = Convert.ToDecimal(row["Tamaño"]);

                    if (!tamanos.ContainsKey(nombre))
                    {
                        tamanos.Add(nombre, tamaño);
                    }
                }

                // Asignar los tamaños correspondientes a las filas del DataGridView
                foreach (DataGridViewRow row in dgvDesglose.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        string nombreProducto = row.Cells[0].Value.ToString().Trim(); // Asegúrate de usar el índice correcto para el nombre del producto
                        if (tamanos.ContainsKey(nombreProducto))
                        {
                            row.Cells[4].Value = tamanos[nombreProducto]; // Usa el índice correcto para la columna "Tamaño"
                        }
                        else
                        {
                            row.Cells[4].Value = row.Cells[3].Value;//2
                            row.Cells[5].Value = row.Cells[3].Value;//2
                        }
                    }
                    if (UserCache.Name == "InnovaGlass")
                    {
                        // Validar y limpiar el valor de la columna "SalePrice"
                        if (row.Cells["Cost"].Value != null)
                        {
                            string salePriceStr = row.Cells["Cost"].Value.ToString();
                            salePriceStr = salePriceStr.Replace(" ", ""); // Eliminar espacios

                            // Validar si el valor es decimal y convertirlo
                            if (decimal.TryParse(salePriceStr, out decimal salePrice))
                            {
                                row.Cells["Cost"].Value = salePrice;
                            }
                            else
                            {
                                MessageBox.Show($"Valor inválido en la columna 'SalePrice' en la fila {row.Index + 1}. Valor: '{salePriceStr}'", "Error en los datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                row.Cells["Cost"].Value = DBNull.Value; // O establece un valor por defecto
                            }
                        }
                    }
                    else
                    {
                        // Validar y limpiar el valor de la columna "SalePrice"
                        if (row.Cells["SalePrice"].Value != null)
                        {
                            string salePriceStr = row.Cells["SalePrice"].Value.ToString();
                            salePriceStr = salePriceStr.Replace(" ", ""); // Eliminar espacios

                            // Validar si el valor es decimal y convertirlo
                            if (decimal.TryParse(salePriceStr, out decimal salePrice))
                            {
                                row.Cells["SalePrice"].Value = salePrice;
                            }
                            else
                            {
                                MessageBox.Show($"Valor inválido en la columna 'SalePrice' en la fila {row.Index + 1}. Valor: '{salePriceStr}'", "Error en los datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                row.Cells["SalePrice"].Value = DBNull.Value; // O establece un valor por defecto
                            }
                        }
                    }


                }
                //Aqui el nuevo codigo
                // Asegurarse de que las columnas 'Total Cost' y 'Total Price' existan en el DataGridView
                if (!dgvDesglose.Columns.Contains("Total Cost"))
                {
                    dgvDesglose.Columns.Add("Total Cost", "Total Cost");
                }
                if (!dgvDesglose.Columns.Contains("Total Price"))
                {
                    dgvDesglose.Columns.Add("Total Price", "Total Price");
                }

                // Calcular los valores para 'Total Cost' y 'Total Price' para cada fila
                foreach (DataGridViewRow row in dgvDesglose.Rows)
                {
                    if (row.Cells[4].Value != null && row.Cells[5].Value != null)
                    {
                        decimal tamaño = Convert.ToDecimal(row.Cells[4].Value);
                        decimal cantidad = Convert.ToDecimal(row.Cells[5].Value);

                        // Calcular Total Cost
                        if (row.Cells["Cost"].Value != null)
                        {
                            decimal cost = Convert.ToDecimal(row.Cells["Cost"].Value);
                            row.Cells["Total Cost"].Value = tamaño * cantidad * cost;
                        }

                        // Calcular Total Price
                        if (row.Cells["SalePrice"].Value != null)
                        {
                            decimal salePrice = Convert.ToDecimal(row.Cells["SalePrice"].Value);
                            row.Cells["Total Price"].Value = tamaño * cantidad * salePrice;
                        }
                    }
                }

                // Declarar variables para acumular las sumas de Total Cost y Total Price
                decimal sumaTotalCost = 0;
                decimal sumaTotalPrice = 0;

                // Recorrer cada fila del DataGridView para acumular las sumas
                foreach (DataGridViewRow row in dgvDesglose.Rows)
                {
                    // Sumar Total Cost
                    if (row.Cells["Total Cost"].Value != null)
                    {
                        sumaTotalCost += Convert.ToDecimal(row.Cells["Total Cost"].Value);
                    }

                    // Sumar Total Price
                    if (row.Cells["Total Price"].Value != null)
                    {
                        sumaTotalPrice += Convert.ToDecimal(row.Cells["Total Price"].Value);
                    }
                }

                // Mostrar las sumas en los TextBox correspondientes
                txtTotalC.Text = sumaTotalCost.ToString("N2"); // Formato con dos decimales
                txtTotalSP.Text = sumaTotalPrice.ToString("N2"); // Formato con dos decimales


            }
            catch (Exception ex)
            {
                // Manejar la excepción apropiadamente
                MessageBox.Show($"Se produjo un error: {ex.Message}");
            }
        }

        private List<PriceProductClass> CargarLista()
        {
            //Cargar la Lista con el dgvDesglose
            List<PriceProductClass> Lista = new List<PriceProductClass>();
            foreach (DataGridViewRow item in dgvDesglose.Rows)
            {
                //Validar que el Item no sea Nulo
                if (item.Cells[1].Value != null)
                {
                    PriceProductClass priceProduct = new PriceProductClass();
                    priceProduct.Nombre = item.Cells[0].Value.ToString();
                    priceProduct.Supplier = cbProveedorDesglose.Text;
                    priceProduct.Color = "Natural";
                    Lista.Add(priceProduct);
                }
            }

            return Lista;

        }


    }
}
