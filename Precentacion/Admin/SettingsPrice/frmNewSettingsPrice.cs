using Negocio.Proveedor;
using Negocio.SettingPrice;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Precentacion.Admin.SettingsPrice
{
    public partial class frmNewSettingsPrice : Form
    {
        private int borderRadius = 20;
        private int borderSize = 2;
        private Color borderColor = Color.FromArgb(224, 224, 224);
        private readonly N_SettingPrice settingPrice = new N_SettingPrice();
        public frmNewSettingsPrice()
        {
            InitializeComponent();
            CargarProveedor();
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

        #region EventTitleBar
        private void ViewNewSettingsPrice_Paint(object sender, PaintEventArgs e)
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
        #endregion

        private void CreateSettingPrice()
        {
            if (cbSystem.Text != "" && cbDesing.Text != "" && cbColor.Text != "")
            {
                if (txtPorsentaje.Text != "0")
                {
                    string Name = cbSystem.Text + cbDesing.Text + cbColor.Text;

                    bool Res = settingPrice.CreateSettingPrice(Name, txtPorsentaje.Text, cbSupplier.Text);
                    if (Res)
                    {
                        MessageBox.Show("El Ajuste de Precio se Inserto Correctamente", "Exito al insertar", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Error Interno al Insertar, Intente de Nuevo", "Error al Insertar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("El Valor no es Correcto o no es un numero entero", "Error en el Valor de Ajuste", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Complete la Descripcion del Ajuste de Precio", "Error en la Descripcion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarProveedor() 
        {
            LN_Proveedor proveedor = new LN_Proveedor();
            cbSupplier.DataSource = proveedor.CargarProveedor();
            cbSupplier.DisplayMember = "Nombre";
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
            CreateSettingPrice();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmViewSettingsPrice frm = new frmViewSettingsPrice();
            frm.Show();
            this.Close();
        }
    }
}