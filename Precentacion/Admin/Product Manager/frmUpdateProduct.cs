using Precentacion.Product_Manager;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Negocio.Products;
using System.Collections.Generic;
using Dominio.Product;
using Dominio.PriceProduct;
using Negocio.Proveedor;
using System.Data;

namespace Precentacion.Admin.Product_Manager
{
    public partial class frmUpdateProduct : Form
    {
        private int borderRadius = 20;
        private int borderSize = 2;
        private Color borderColor = Color.FromArgb(224, 224, 224);
        List<PriceProductClass> listaPrecios = new List<PriceProductClass>();

        public frmUpdateProduct()
        {
            InitializeComponent();
            LoadStyleDataGrid();
            CargarProveedor();
            txtCode.KeyDown += new KeyEventHandler(txtCode_KeyDown);
        }

        #region Drag From
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion

        #region Border Rounded

        private GraphicsPath GetRoundedPath(Rectangle rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }
        private void FormRegionAndBorder(Form form, float radius, Graphics graph, Color borderColor, float borderSize)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                using (GraphicsPath roundPath = GetRoundedPath(form.ClientRectangle, radius))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                using (Matrix transform = new Matrix())
                {
                    graph.SmoothingMode = SmoothingMode.AntiAlias;
                    form.Region = new Region(roundPath);
                    if (borderSize >= 1)
                    {
                        Rectangle rect = form.ClientRectangle;
                        float scaleX = 1.0F - ((borderSize + 1) / rect.Width);
                        float scaleY = 1.0F - ((borderSize + 1) / rect.Height);

                        transform.Scale(scaleX, scaleY);
                        transform.Translate(borderSize / 1.6F, borderSize / 1.6F);

                        graph.Transform = transform;
                        graph.DrawPath(penBorder, roundPath);
                    }
                }
            }
        }
        #endregion

        #region Events
        private void frmUpdateProduct_Paint(object sender, PaintEventArgs e)
        {
            FormRegionAndBorder(this, borderRadius, e.Graphics, borderColor, borderSize);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMaxi_Click(object sender, EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width / 2, Screen.PrimaryScreen.WorkingArea.Height / 2);
            this.WindowState = FormWindowState.Maximized;
            btnMaxi.Visible = false;
            btnRestore.Visible = true;
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.CenterToScreen();
            btnMaxi.Visible = true;
            btnRestore.Visible = false;
        }

        private void btnMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmViewProducts frmView = new frmViewProducts();
            frmView.Show();
            this.Close();
        }

        private void btnFindUpdate_Click(object sender, EventArgs e)
        {
            if (txtCode.Text != "")
            {
                bool res;
                res = LoadDataProduct();
                if (res == true)
                {
                    res = LoadColorProduct();
                }

            }
            else
            {
                MessageBox.Show("Ingrese un numero para la Busqueda ", "Codigo Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar y corregir los valores de las columnas especificadas
                foreach (DataGridViewRow fila in dgColor.Rows)
                {
                    if (!fila.IsNewRow)
                    {
                        List<string> columnas = new List<string> { "Tamaño", "BasePrice", "Discount", "Cost", "SalePrice", "SalePrice2" };
                        foreach (var columna in columnas)
                        {
                            string valor = Convert.ToString(fila.Cells[columna].Value);
                            if (valor.Contains("."))
                            {
                                valor = valor.Replace('.', ',');
                                fila.Cells[columna].Value = valor;
                            }
                        }
                    }
                }

                if (txtCode.Text != "")
                {
                    N_Products products = new N_Products();
                    bool Res;
                    Res = products.UpdateProduct(txtCode.Text, txtDescription.Text, cbSystem.Text, cbCategory.Text);
                    if (Res)
                    {
                        LoadListPrice();
                        Res = products.UpdatePriceProduct(listaPrecios);
                        if (Res)
                        {
                            CleanScreen();
                            MessageBox.Show("Los Datos se han Actualizado Correctamente", "Guardado Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            listaPrecios.Clear();
                            MessageBox.Show("Los Datos no se han Actualizado Correctamente", "Guardado Fallido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        listaPrecios.Clear();
                        MessageBox.Show("Los Datos no se han Actualizado Correctamente", "Guardado Fallido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar los datos: " + ex.Message);
            }
        }



        private void btnBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region LoadFunction
        private bool LoadDataProduct()
        {
            N_Products products = new N_Products();
            List<ProductClass> DataProduct = new List<ProductClass>();
            DataProduct = products.FindDataProductxID(txtCode.Text);
            if (DataProduct.Count > 0)
            {
                foreach (var _product in DataProduct)
                {
                    txtDescription.Text = _product.Description;
                    cbCategory.Text = _product.Category;
                    cbSystem.Text = _product.System;
                }
                return true;
            }
            else
            {
                MessageBox.Show("El Articulo No Existe favor Revisar el ID", "Articulo Inexistente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


        }
        private bool LoadColorProduct()
        {
            try
            {
                dgColor.Rows.Clear();
                List<PriceProductClass> PriceProduct = new List<PriceProductClass>();
                N_Products products = new N_Products();
                PriceProduct = products.FindColorxID(txtCode.Text);
                bool Return = false;
                if (PriceProduct.Count != null)
                {
                    if (PriceProduct.Count > 0)
                    {
                        foreach (var Price in PriceProduct)
                        {
                            dgColor.Rows.Add(Price.IdPriceProduct, Price.Color, Price.BasePrice, Price.Discount, Price.Cost, Price.SalePrice1, Price.SalePrice2, Price.Supplier, Price.Tamaño);
                        }
                        Return = true;
                    }
                    else
                    {
                        MessageBox.Show("El Articulo No Contiene Colores favor Agregar", "Colores Inexistente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Return = false;
                    }
                }
                return Return;
            }
            catch (Exception)
            {
                return false;
            }

        }
        private void LoadListPrice()
        {
            try
            {
                foreach (DataGridViewRow fila in dgColor.Rows)
                {
                    if (!fila.IsNewRow) // Evitar la última fila de nuevo ingreso en el DataGridView
                    {
                        PriceProductClass precio = new PriceProductClass
                        {
                            IdPriceProduct = Convert.ToInt32(fila.Cells["IdPriceProduct"].Value),
                            Color = Convert.ToString(fila.Cells["Color"].Value),
                            BasePrice = Convert.ToDecimal(fila.Cells["BasePrice"].Value),
                            Discount = Convert.ToDecimal(fila.Cells["Discount"].Value),
                            Cost = Convert.ToDecimal(fila.Cells["Cost"].Value),
                            SalePrice1 = Convert.ToDecimal(fila.Cells["SalePrice"].Value),
                            SalePrice2 = Convert.ToDecimal(fila.Cells["SalePrice2"].Value),
                            Supplier = Convert.ToString(fila.Cells["Supplier"].Value),
                            Tamaño = Convert.ToDecimal(fila.Cells["Tamaño"].Value)

                        };

                        listaPrecios.Add(precio);
                    }
                }
            }
            catch (Exception e)
            {

                MessageBox.Show("Error : " + e);
            }


        }
        private void LoadStyleDataGrid()
        {
            // Agrega columnas al DataGridView
            dgColor.Columns.Add("IdPriceProduct", "ID Producto");
            dgColor.Columns.Add("Color", "Color");
            dgColor.Columns.Add("BasePrice", "Precio Base");
            dgColor.Columns.Add("Discount", "Descuento");
            dgColor.Columns.Add("Cost", "Costo");
            dgColor.Columns.Add("SalePrice", "Precio Venta 1");
            dgColor.Columns.Add("SalePrice2", "Precio Venta 2");
            dgColor.Columns.Add("Supplier", "Proveedor");
            dgColor.Columns.Add("Tamaño", "Tamaño");
            dgColor.Columns[0].Visible = false;

        }
        #endregion

        #region SoportFunction
        private void CleanScreen()
        {
            txtCode.Text = "";
            txtDescription.Text = "";
            cbCategory.Text = "";
            cbSupplier.Text = "";
            cbSystem.Text = "";
            dgColor.Rows.Clear();
        }
        #endregion
        private void CalcPrice(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Verifica si se cambió el valor en las columnas "Precio Base" o "Descuento"
                
                    // Obtiene los valores actuales de "Precio Base" y "Descuento"
                    decimal precioBase = Convert.ToDecimal(dgColor.Rows[e.RowIndex].Cells["BasePrice"].Value);
                    decimal descuento = Convert.ToDecimal(dgColor.Rows[e.RowIndex].Cells["Discount"].Value);

                    // Realiza el cálculo del "Precio Venta" con el descuento aplicado
                    decimal precioVenta = precioBase - (precioBase * descuento / 100);

                    // Actualiza la celda correspondiente de "Precio Venta"
                    dgColor.Rows[e.RowIndex].Cells["Cost"].Value = precioVenta;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de Datos: " + ex.Message);
            }
        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("KeyDown event triggered"); // Línea de depuración
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Evita que el sonido de 'ding' se reproduzca
                if (txtCode.Text != "")
                {
                    bool res;
                    res = LoadDataProduct();
                    if (res == true)
                    {
                        res = LoadColorProduct();
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese un numero para la Busqueda ", "Codigo Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CargarProveedor() 
        {
            LN_Proveedor proveedor = new LN_Proveedor();
            DataTable dt = proveedor.CargarProveedor();
            cbSupplier.DataSource = dt;
            cbSupplier.DisplayMember = "Nombre";
        }

    }
}

