using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Negocio.Products;
using Precentacion.Admin;
using Precentacion.Admin.Product_Manager;

namespace Precentacion.Product_Manager
{
    public partial class frmViewProducts : Form
    {
        private int borderRadius = 20;
        private int borderSize = 2;
        private Color borderColor = Color.FromArgb(224, 224, 224);

        public frmViewProducts()
        {
            InitializeComponent();
            ViewProducts();
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

        #region Eventos
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
            frmAdminDashboard dashboard = new frmAdminDashboard();
            dashboard.Show();
            this.Close();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (txtCode.Text != "" || cbSystem.SelectedIndex == 0 || cbCategory.SelectedIndex == 0 || cbColor.SelectedIndex != 0)
            {
                dgProducts.DataSource = null;
                DataTable table = new DataTable();
                N_Products products = new N_Products();
                table = products.Find(txtCode.Text, cbSystem.Text, cbCategory.Text, cbColor.Text);
                dgProducts.DataSource = table;
            }
        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            if (txtCode.Text != "")
            {
                cbCategory.Enabled = false;
                cbColor.Enabled = false;
                cbSystem.Enabled = false;
            }
            else
            {
                cbCategory.Enabled = true;
                cbColor.Enabled = true;
                cbSystem.Enabled = true;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmNewProduct frm = new frmNewProduct();
            frm.Show();
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            frmUpdateProduct frmUpdate = new frmUpdateProduct();
            frmUpdate.Show();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            ViewProducts();
        }
        #endregion

        #region Function

        private void ViewProducts()
        {
            DataTable table = new DataTable();
            N_Products products = new N_Products();
            table = products.View();
            dgProducts.DataSource = table;


        }


        #endregion

       
    }
}
