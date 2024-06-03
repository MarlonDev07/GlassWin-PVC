using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Negocio;
using Negocio.SettingPrice;
using Precentacion.Admin.Product_Manager;

namespace Precentacion.Admin.SettingsPrice
{
    public partial class frmViewSettingsPrice : Form
    {
        private int borderRadius = 20;
        private int borderSize = 2;
        private Color borderColor = Color.FromArgb(224, 224, 224);
        N_SettingPrice settingPrice = new N_SettingPrice();

        public frmViewSettingsPrice()
        {
            InitializeComponent();
            LoadDataGrid();
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

        #region ToolPanel
        private void ViewSettingsPrice_Paint(object sender, PaintEventArgs e)
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

        #region Events
        private void btnFind_Click(object sender, EventArgs e)
        {
            if (cbSystem.Text != "Seleccione Sistema" && cbDesing.Text != "Seleccione Diseño" && cbColor.Text != "Seleccione Color")
            {
                DataTable dt = new DataTable();
                string Name = cbSystem.Text + cbDesing.Text + cbColor.Text;
                dt = settingPrice.Find(Name);
                dgSetingPrice.DataSource = dt;
            }
            else { MessageBox.Show("Seleccione Todos los Campos Anteriores", "Error en el Nombre", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadDataGrid();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmNewSettingsPrice frm = new frmNewSettingsPrice();
            frm.Show();
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            frmUpdateSettingPrice frm = new frmUpdateSettingPrice();
            frm.Show();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmAdminDashboard frm = new frmAdminDashboard();
            frm.Show();
            this.Close();
        }
        #endregion

        #region SopportFuction
        private void LoadDataGrid()
        {


            DataTable dataTable = new DataTable();
            dataTable = settingPrice.View();
            dgSetingPrice.DataSource = dataTable;
        }

        #endregion

        
    }
}
