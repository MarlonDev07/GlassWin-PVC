using Negocio.Login;
using Precentacion.Admin;
using Precentacion.RestorePass;
using Precentacion.User.DashBoard;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;

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

        #region Login Function
        private void Login() 
        {
            if (txtUserName.Text != "Ingrese el Usuario" && txtPassWord.Text != "Ingrese la Contraseña")
            {
               N_Login LoginData = new N_Login();
                //LoginData.pasarUsuario(txtUserName.Text);
               bool ValidLogin = LoginData.LoginUser(txtUserName.Text, txtPassWord.Text);
               if (ValidLogin == true) 
               {
                    if (UserCache.State == "Active") 
                    {
                        if (UserCache.Roll == "Admin")
                        {
                            UserCache.Pass = txtPassWord.Text;

                            frmAdminDashboard adminDashboard = new frmAdminDashboard();
                            adminDashboard.Show();
                            this.Hide();
                        }

                        if (UserCache.Roll == "User")
                        {  
                            frmDashUser frm = frmDashUser.Instance;
                            frm.Show();
                            frm.BringToFront();
                            this.Hide();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Su cuenta esta deshabilitada, porfavor pongase en contacto con soporte", "Cuenta Expirada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }                   
                }
                else
                {
                    MessageBox.Show("Los datos ingresados no coinciden, verifique e intente de nuevo", "Datos Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Todos los campos deben ser rellenados","Campos Incompletos",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            
        }
        #endregion

        #region Events
        private void btnAccess_Click(object sender, EventArgs e)
        {
            Login();
        }

        #region BtnClose

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
                txtPassWord.PasswordChar = '*';
            }

        }

        private void txtPassWord_Leave(object sender, EventArgs e)
        {
            if (txtPassWord.Text == "")
            {
                txtPassWord.Text = "Ingrese la Contraseña";
                txtPassWord.ForeColor = SystemColors.WindowFrame;
                txtPassWord.PasswordChar = '\0';
            }

        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                Login();
            }
        }


        #endregion

        #region BtnViewPassWord

        private void btnViewPassWord_MouseDown(object sender, MouseEventArgs e)
        {
            txtPassWord.PasswordChar = '\0';
        }

        private void btnViewPassWord_MouseUp(object sender, MouseEventArgs e)
        {
            txtPassWord.PasswordChar = '*';
        }

        #endregion

        #endregion

        #region Mover Formulario
        // Variables para almacenar la posición del mouse
        private bool isDragging = false;
        private Point startPoint = new Point(0, 0);
        private void BarraSuperior_MouseDown(object sender, MouseEventArgs e)
        {

        }
        private void BarraSuperior_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - startPoint.X, p.Y - startPoint.Y);
            }
        }
        private void BarraSuperior_MouseUp(object sender, MouseEventArgs e)
        {
            // Cuando el botón del mouse es soltado
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }
        #endregion


    }
}
