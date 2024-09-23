using Dominio.SettingPrice;
using Negocio.Proveedor;
using Negocio.SettingPrice;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Precentacion.Admin.SettingsPrice
{
    public partial class frmUpdateSettingPrice : Form
    {
        #region Variables
        private int borderRadius = 20;
        private int borderSize = 2;
        private Color borderColor = Color.FromArgb(224, 224, 224);
        private readonly N_SettingPrice settingPrice = new N_SettingPrice();
        #endregion

        #region Constructor
        public frmUpdateSettingPrice()
        {
            InitializeComponent();
            CargarProveedor();
        }
        #endregion 

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

        #region EventTitleBar
        private void ViewSettingsPrice_Paint(object sender, PaintEventArgs e)
        {
            FormRegionAndBorder(this, borderRadius, e.Graphics, borderColor, borderSize);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
           frmViewSettingsPrice frmViewSettingsPrice = new frmViewSettingsPrice();
           frmViewSettingsPrice.Show();
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

        #endregion

        #region Events 
        private void btnFindUpdate_Click(object sender, EventArgs e)
        {
            LoadDataSettingPrice();
        }
        private void txtValue_TextChanged(object sender, EventArgs e)
        {
            decimal Percentage = 0;
            if (int.TryParse(txtValue.Text, out int Res))
            {
                Percentage = Convert.ToDecimal(Res) / 100;
                txtPorsentaje.Text = Percentage.ToString();
            }
            else
            {
                txtPorsentaje.Text = "0";
            }

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateSettingPrice();
        }
        #endregion

        #region SopportFunction
        private void LoadDataSettingPrice() 
        {
            if (txtId.Text != "")
            {
                List<SettingPriceClass> DataSettings = new List<SettingPriceClass>();
                DataSettings = settingPrice.DataSettingsPrice(txtId.Text);
                if (DataSettings.Count > 0)
                {
                    foreach (var _product in DataSettings)
                    {
                        txtName.Text = _product.Name;
                        cbSupplier.Text = _product.Supplier;
                        txtPorsentaje.Text = _product.Percentage.ToString();
                    }
                    txtValue.Text = Convert.ToString(Convert.ToInt32(Convert.ToDecimal(txtPorsentaje.Text)*100));
                    
                }
                else
                {
                    MessageBox.Show("El Articulo No Existe favor Revisar el ID", "Articulo Inexistente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                }
            }
        }
        #endregion

        #region Update Function
        private void UpdateSettingPrice() 
        {
            if (txtId.Text != "")
            {
                if (txtPorsentaje.Text != "0")
                {
                    string user = UserCache.Name;
                    bool Res = settingPrice.UpdateSettingPrice(txtId.Text,txtName.Text, txtPorsentaje.Text, cbSupplier.Text, user);
                    if (Res)
                    {
                        MessageBox.Show("El Ajuste de Precio se Modifico Correctamente", "Exito al insertar", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Error Interno al Modificar, Intente de Nuevo", "Error al Insertar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("El Valor no es Correcto o no es un numero entero", "Error en el Valor de Ajuste", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Favor ingrese el Codigo para cargar el Ajuste ", "Error en el ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void CargarProveedor() 
        {
            LN_Proveedor proveedor = new LN_Proveedor();
            cbSupplier.DataSource = proveedor.CargarProveedor();
            cbSupplier.DisplayMember = "Nombre";
        }
        
    }
}
