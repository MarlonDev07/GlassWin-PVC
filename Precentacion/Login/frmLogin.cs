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
using Negocio.Login;
using Precentacion.Admin;
using Precentacion.RestorePass;

namespace Precentacion.Login
{
    public partial class frmLogin : Form
    {
        private int borderRadius = 20;
        private int borderSize = 2;
        private Color borderColor = Color.FromArgb(0, 0, 0);

        public frmLogin()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Padding = new Padding(borderSize);
            this.BackColor = borderColor;
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

        #region Login Function
        private void Login() 
        {
            if (txtUserName.Text != "Ingrese el Usuario" && txtPassWord.Text != "Ingrese la Contraseña")
            {
               N_Login LoginData = new N_Login();
               bool ValidLogin = LoginData.LoginUser(txtUserName.Text, txtPassWord.Text);
               if (ValidLogin == true) 
               {
                    if (UserCache.State == "Active") 
                    {
                        if (UserCache.Roll == "Admin")
                        {
                            UserCache.Pass = txtPassWord.Text;

                            frmAdminDashboard adminDashboard = new frmAdminDashboard();
                            MessageBox.Show("Bienvenido " + UserCache.Name, "Bienvenida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            adminDashboard.Show();
                            this.Hide();
                        }

                        if (UserCache.Roll == "User")
                        {
                            MessageBox.Show("Bienvenido " + UserCache.Name + " Usuario", "Bienvenida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Su cuenta esta Desabilitada, Porfavor pongase en contacto con Soporte", "Cuenta Expirada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }                   
                }
                else
                {
                    MessageBox.Show("Los Datos Ingresados no coinciden, Verifique eh intente de nuevo", "Datos Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Todos los campos deben ser Rellenados","Campos Incompletos",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            
        }
        #endregion

        #region Events

        private void frmLogin_Paint(object sender, PaintEventArgs e)
        {
            FormRegionAndBorder(this,borderRadius,e.Graphics,borderColor,borderSize);
        }

        #region BtnClose
        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.BackColor = SystemColors.ControlDarkDark;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackColor = SystemColors.ButtonShadow;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region TextBox User and Pass

        private void txtUserName_Enter(object sender, EventArgs e)
        {
            if (txtUserName.Text == "Ingrese el Usuario")
            {
                txtUserName.Text = "";
                txtUserName.ForeColor = Color.Black;
            }

        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                txtUserName.Text = "Ingrese el Usuario";
                txtUserName.ForeColor = SystemColors.WindowFrame;
            }

        }

        private void txtPassWord_Enter(object sender, EventArgs e)
        {
            if (txtPassWord.Text == "Ingrese la Contraseña")
            {
                txtPassWord.Text = "";
                txtPassWord.ForeColor = Color.Black;
            }

        }

        private void txtPassWord_Leave(object sender, EventArgs e)
        {
            if (txtPassWord.Text == "")
            {
                txtPassWord.Text = "Ingrese la Contraseña";
                txtPassWord.ForeColor = SystemColors.WindowFrame;
            }

        }


        #endregion

        private void btnAccess_Click(object sender, EventArgs e)
        {
            Login();
        }




        #endregion

        private void lblForgetPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           frmValidateIdentity frmValidate = new frmValidateIdentity();
            frmValidate.Show();
            this.Hide();
        }
    }
}
