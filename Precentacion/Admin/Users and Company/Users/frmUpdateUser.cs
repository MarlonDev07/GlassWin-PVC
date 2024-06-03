using Dominio.ClassUser;
using Dominio.SettingPrice;
using Negocio.SettingPrice;
using Negocio.Users;
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

namespace Precentacion.Admin.Users_and_Company.Users
{
    public partial class frmUpdateUser : Form
    {
        private int borderRadius = 20;
        private int borderSize = 2;
        private Color borderColor = Color.FromArgb(224, 224, 224);
        N_Users _Users = new N_Users();

        public frmUpdateUser()
        {
            InitializeComponent();
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

        private void btnRestore_Click(object sender, EventArgs e)
        {

        }

        private void btnMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion

        #region SopportFunction
        private void LoadDataUser()
        {
            if (txtID.Text != "")
            {

                List<clsUser> Data = new List<clsUser>();
                Data = _Users.FindxID(txtID.Text);
                if (Data.Count > 0)
                {
                    foreach (var _product in Data)
                    {
                        txtName.Text = _product.Name;
                        cbRoll.Text = _product.Roll;
                        txtUserName.Text = _product.User;
                        txtTel.Text = _product.Phone;
                        txtEmail.Text = _product.Email;


                    }
                }
                else
                {
                    MessageBox.Show("El Articulo No Existe favor Revisar el ID", "Articulo Inexistente", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }
        private bool TextValidate()
        {
            if (cbRoll.Text != "" && txtName.Text != "" && txtTel.Text != "" && txtUserName.Text != "" && txtPassWord.Text != "" && txtEmail.Text != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void CleanScreen()
        {
            txtID.Text = "";
            cbRoll.Text = "";
            txtName.Text = "";
            txtEmail.Text = "";
            txtPassWord.Text = "";
            txtTel.Text = "";
            txtUserName.Text = "";
        }
        #endregion

        #region ButtomFunction
        private void btnFindUpdate_Click(object sender, EventArgs e)
        {
            LoadDataUser();
        }
        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (TextValidate())
            {
                if (_Users.Update(Convert.ToInt32(txtID.Text), txtName.Text, txtTel.Text, txtEmail.Text, txtUserName.Text, cbRoll.Text))
                {
                    MessageBox.Show("El cliente se Modifico Correctamente", "Exito al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CleanScreen();
                }
                else
                {
                    MessageBox.Show("Error inesperado al Modificar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Revise Todos los campos porfavor", "Error : Campos Vacidos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmViewsUsers frmViewsUsers = new frmViewsUsers();
            frmViewsUsers.Show();
            this.Close();
        }
        #endregion

    }
}
