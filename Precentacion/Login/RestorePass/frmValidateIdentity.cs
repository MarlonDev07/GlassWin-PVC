using Precentacion.Login;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Negocio.Users;
using Negocio.SMS__WhatsApp;

namespace Precentacion.RestorePass
{
    public partial class frmValidateIdentity : Form
    {
        private int borderRadius = 20;
        private int borderSize = 2;
        private Color borderColor = Color.FromArgb(0, 0, 0);
        int RandomNumber = 0;

        public frmValidateIdentity()
        {
            InitializeComponent();
            GenerateRandomNumber();
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

        #region Function
        private void GenerateRandomNumber() 
        {
            Random random = new Random();
            RandomNumber = random.Next(99999);
        }
        private void VerificationData() 
        {
            if (textBox1.Text != "" && txtEmail.Text != "" && txtPhone.Text != "") 
            {
                N_RestoreUser _RestoreUser = new N_RestoreUser();
                N_SMSTwilio _SMS = new N_SMSTwilio();

                bool Result = false;
                Result = _RestoreUser.VerificationData(textBox1.Text, txtEmail.Text, txtPhone.Text);

                if (Result == true)
                {
                    

                    string Message = "Su Codigo Para Restaurar la Contraseña es: " + RandomNumber;
                    Result = _SMS.SendSMS(txtPhone.Text, Message);
                    if (Result == true)
                    {
                        MessageBox.Show("El Mensaje Fue Enviado con Exito, Porfavor Verifica el Codigo","Envio Correcto",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        btnSendCode.Enabled = false;
                        PanelHide1.Visible = false;

                    }
                    else if (Result == false) { MessageBox.Show("Error al Enviar el Codigo", "Error al Enviar SMS", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error); }
                }
                else if (Result == false)
                {
                    MessageBox.Show("Error al Verificar sus Datos, Existe alguna incoherencias", "Datos no Coinciden", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else 
            {
                MessageBox.Show("Datos Faltantes, Porfavor rellene todos los Campos", "Faltante de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        private void VerificationCode() 
        {
            N_RestoreUser _RestoreUser = new N_RestoreUser();
            bool Result = _RestoreUser.VerificationCode(RandomNumber, txtCode.Text);
            if (Result == true)
            {
                MessageBox.Show("El Codigo es Correcto", "Codigo Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnValideCode.Enabled = false;
                PanelHide2.Visible = false;
            }
            else
            {
                MessageBox.Show("Error al Verificar el Codigo, Existe alguna incoherencias", "DCodigo Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ChangePassWord() 
        {
            N_RestoreUser _RestoreUser = new N_RestoreUser();
           
            if (txtPassWord.Text != "" && txtRepeatPass.Text != "" && txtPassWord.Text == txtRepeatPass.Text) 
            {
                bool Result;
                Result = _RestoreUser.ChangePassWord(textBox1.Text, txtPassWord.Text);
                if (Result == true) 
                {
                    MessageBox.Show("En horabuena su Contraseña ah Sido Actualizada", "Cambio Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmLogin login = new frmLogin();
                    login.Show();
                    this.Close();
                }
                else 
                {
                    MessageBox.Show("Error al Intentar Cambiar la Contraseña, Intente de Nuvo mas Tarde", "Error de Sistema", MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Las Contraseñas no Coinciden o Estan Vacias, Porfavor Verificar", "Incoincidencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        #endregion

        #region Event
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            frmLogin frmLogin = new frmLogin();
            frmLogin.Show();  
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            frmLogin frmLogin = new frmLogin();
            frmLogin.Show();
        }

        private void btnMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnSendCode_Click(object sender, EventArgs e)
        {
            VerificationData();
        }

        private void btnValideCode_Click(object sender, EventArgs e)
        {
            VerificationCode();
        }

        private void frmValidateIdentity_Paint(object sender, PaintEventArgs e)
        {
            FormRegionAndBorder(this, borderRadius, e.Graphics, borderColor, borderSize);
        }

        private void btnsavenewPasWord_Click(object sender, EventArgs e)
        {
            ChangePassWord();
        }
        #endregion


    }
}
