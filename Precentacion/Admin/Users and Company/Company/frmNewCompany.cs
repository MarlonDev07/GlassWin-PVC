using Negocio.Company;
using Precentacion.Admin.Users_and_Company.Users;
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

namespace Precentacion.Admin.Users_and_Company.Company
{
    public partial class frmNewCompany : Form
    {
        private int borderRadius = 20;
        private int borderSize = 2;
        private Color borderColor = Color.FromArgb(224, 224, 224);
        string IdUser;
        N_Company ObjNCompany = new N_Company();
        DataTable dtCompanyVerification;
        public frmNewCompany(string id)
        {
            InitializeComponent();
            IdUser = id;
            txtId.Text = id;
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmViewsUsers frm = new frmViewsUsers();
            frm.Show();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                bool resp = Validar();
                if (resp) 
                {
                    /*if (dtCompanyVerification != null) { 
                        
                    }*/
                    resp = false;
                    resp = ObjNCompany.Create(txtId.Text, txtCedJuridica.Text, txtTelefono.Text, txtDireccionEmpresa.Text, "", txtEmpresa.Text);
                    if (resp)
                    {
                        MessageBox.Show("Se ha asignado la empresa correctamente", "Empresa Asignada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmViewsUsers frm = new frmViewsUsers();
                        frm.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se ha podido crear la empresa", "Error al crear", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool Validar()
        {
            bool resp = false;
            if (txtCedJuridica.Text == "")
            {
                errorProvider1.SetError(txtCedJuridica, "Ingrese la cedula juridica");
                txtCedJuridica.Focus();
                resp = false;
            }
            else if (txtEmpresa.Text == "")
            {
                errorProvider1.SetError(txtEmpresa, "Ingrese el nombre de la empresa");
                txtEmpresa.Focus();
                resp = false;
            }
            else if (txtDireccionEmpresa.Text == "")
            {
                errorProvider1.SetError(txtDireccionEmpresa, "Ingrese la direccion de la empresa");
                txtDireccionEmpresa.Focus();
                resp = false;
            }
            else if (txtTelefono.Text == "")
            {
                errorProvider1.SetError(txtTelefono, "Ingrese el telefono de la empresa");
                txtTelefono.Focus();
                resp = false;
            }
            else
            {
                resp = true;
            }
            return resp;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            long idCompany = Convert.ToInt64(txtBC.Text);
            DataTable dtCompany = ObjNCompany.BuscarCompany(idCompany);
            dtCompanyVerification = dtCompany;

            if (dtCompany.Rows.Count > 0) // Asegurarse de que haya resultados
            {
                DataRow row = dtCompany.Rows[0]; // Obtener la primera fila del resultado

                txtEmpresa.Text = row[5].ToString(); // Usa el nombre de la columna
                txtCedJuridica.Text = row[1].ToString();
                txtTelefono.Text = row[2].ToString();
                txtDireccionEmpresa.Text = row[3].ToString();
            }
            else
            {
                // Manejar el caso en que no se encuentra la compañía
                MessageBox.Show("No se encontró ninguna compañía con ese ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblCorreo_Click(object sender, EventArgs e)
        {

        }

        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
