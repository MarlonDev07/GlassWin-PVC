using Negocio.Products;
using Dominio.PriceProduct;
using Precentacion.Product_Manager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Reflection.Emit;
using Dominio.Product;

namespace Precentacion.Admin.Product_Manager
{
    public partial class frmNewProduct : Form
    {
        private int borderRadius = 20;
        private int borderSize = 2;
        private Color borderColor = Color.FromArgb(224, 224, 224);
        List<PriceProductClass> listaPrecios = new List<PriceProductClass>();
        int Option = 0;
        public frmNewProduct()
        {
            InitializeComponent();
            LoadStyleDataGrid();
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
        private void frmViewerProducts_Paint(object sender, PaintEventArgs e)
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
            if (Option == 0)
            {
                frmViewProducts frmView = new frmViewProducts();
                frmView.Show();
                this.Close();
            }
            if (Option == 1) 
            {
                ConfigureAddProduct();
            }
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Option ==  0)
            {
                CreateProduct();
            }
            if (Option == 1)
            {
                CreateColor();
            }
        }

        #endregion

        #region Load Function
        private void LoadStyleDataGrid()
        {
            dgColor.Columns.Add("Color", "Color");
            dgColor.Columns.Add("PrecioBase", "PrecioBase");
            dgColor.Columns.Add("Descuento", "Descuento");
            dgColor.Columns.Add("Costo", "Costo");
            dgColor.Columns.Add("PrecioVenta", "PrecioVenta");
            dgColor.Columns.Add("PrecioVenta2", "PrecioVenta2");
            dgColor.Columns.Add("Tamaño", "Tamaño");
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
                            IdProduct = Convert.ToInt32(txtCode.Text),
                            Color = Convert.ToString(fila.Cells["Color"].Value),
                            BasePrice = Convert.ToDecimal(fila.Cells["PrecioBase"].Value),
                            Discount = Convert.ToDecimal(fila.Cells["Descuento"].Value),
                            Cost = Convert.ToDecimal(fila.Cells["Costo"].Value),
                            SalePrice1 = Convert.ToDecimal(fila.Cells["PrecioVenta"].Value),
                            Supplier = Convert.ToString(cbSupplier.Text),
                            SalePrice2 = Convert.ToDecimal(fila.Cells["PrecioVenta2"].Value),
                            Tamaño = Convert.ToDecimal(fila.Cells["Tamaño"].Value)
                        };

                        listaPrecios.Add(precio);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }


        }
        private void LoadLastCode()
        {
            N_Products Products = new N_Products();
            int LastCode;
            LastCode = Products.LastCode();
            txtCode.Text = LastCode.ToString();
        }
        #endregion

        #region Suport Function
        private void CalcPrice(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Verifica si se cambió el valor en las columnas "PrecioBase" o "Descuento"
                if (e.RowIndex >= 0 && (e.ColumnIndex == dgColor.Columns["PrecioBase"].Index || e.ColumnIndex == dgColor.Columns["Descuento"].Index || e.ColumnIndex == dgColor.Columns["PrecioVenta"].Index))
                {
                    // Obtiene los valores actuales de "PrecioBase" y "Descuento"
                    decimal precioBase = Convert.ToDecimal(dgColor.Rows[e.RowIndex].Cells["PrecioBase"].Value);
                    decimal descuento = Convert.ToDecimal(dgColor.Rows[e.RowIndex].Cells["Descuento"].Value);
                    decimal PV1 = Convert.ToDecimal(dgColor.Rows[e.RowIndex].Cells["PrecioVenta"].Value);


                    // Realiza el cálculo del "Costo" y actualiza la celda correspondiente
                    decimal costo = precioBase - (precioBase * descuento / 100);
                    dgColor.Rows[e.RowIndex].Cells["Costo"].Value = costo;
                }
            }
            catch (Exception ex) { MessageBox.Show("Error de Datos: " + ex.Message); }
        }
        private void CleanScreen()
        {
            LoadLastCode();
            txtDescription.Text = "";
            dgColor.Rows.Clear();
        }

        #endregion


        private void btnAddColor_Click(object sender, EventArgs e)
        {
            ConfigureAddColor();
        }

        private void ConfigureAddColor() 
        {
            Option = 1;
            btnFindUpdate.Visible = true;
            dgColor.Rows.Clear();
            txtCode.Text = "";
            txtCode.Enabled = true;
            txtDescription.Enabled = false;
            cbCategory.Enabled = false;
            cbSupplier.Enabled = true;
            cbSystem.Enabled = false;
            btnAddColor.Enabled = false;
            MessageBox.Show("Sistema Configurado para Agregar Color", "Mensaje Usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ConfigureAddProduct() 
        {
            CleanScreen();
            Option = 0;
            btnFindUpdate.Visible = false;
            txtCode.Enabled = false;
            txtDescription.Enabled = true;
            cbCategory.Enabled = true;
            cbSupplier.Enabled = true;
            cbSystem.Enabled = true;
            btnAddColor.Enabled = true;
            
            MessageBox.Show("Opcion Agregar Color Cancelado", "Mensaje Usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

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
                    cbSupplier.Text = _product.Supplier;
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

        private void btnFindUpdate_Click(object sender, EventArgs e)
        {
            if (txtCode.Text != "")
            {
               LoadDataProduct();
            }
            else
            {
                MessageBox.Show("Ingrese un numero para la Busqueda ", "Codigo Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateProduct() 
        {
            if (!string.IsNullOrEmpty(txtCode.Text) && !string.IsNullOrEmpty(txtDescription.Text) && !string.IsNullOrEmpty(cbSystem.Text) && !string.IsNullOrEmpty(cbCategory.Text) && !string.IsNullOrEmpty(cbSupplier.Text))
            {
                if (dgColor.Rows.Count > 0)
                {
                    N_Products Products = new N_Products();



                    bool ResProduct = Products.CreateProduct(txtCode.Text, txtDescription.Text.Trim(), cbSystem.Text, cbCategory.Text);

                    if (ResProduct == true)
                    {
                        LoadListPrice();
                        bool ResPrice = Products.CreatePriceProduct(listaPrecios);

                        if (ResPrice == true)
                        {
                            MessageBox.Show("Los datos del Producto se Guardaron Correctamente", "Guardado Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            listaPrecios.Clear();
                            CleanScreen();
                        }
                        else
                        {
                            MessageBox.Show("Se produjo un error en Guardar los Colores del Producto ", "Guardado Erroneo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            listaPrecios.Clear();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Se produjo un error en Guardar los Datos del Producto ", "Guardado Erroneo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        listaPrecios.Clear();
                    }

                }
                else
                {
                    MessageBox.Show("Ingrese al menos un Color, Verifique eh intente de nuevo", "Datos Incompletos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Ingrese Todos los Datos, Verifique eh intente de nuevo", "Datos Incompletos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateColor() 
        {
            N_Products Products = new N_Products();
            LoadListPrice();
            bool ResPrice = Products.CreatePriceProduct(listaPrecios);

            if (ResPrice == true)
            {
                MessageBox.Show("Los datos del Producto se Guardaron Correctamente", "Guardado Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listaPrecios.Clear();
                CleanScreen();
            }
            else
            {
                MessageBox.Show("Se produjo un error en Guardar los Colores del Producto ", "Guardado Erroneo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                listaPrecios.Clear();
            }
        }
    }
}
  