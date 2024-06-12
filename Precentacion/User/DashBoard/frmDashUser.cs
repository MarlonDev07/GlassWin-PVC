using Dominio.ClassSoundPlay;
using Precentacion.Admin;
using Precentacion.Pruebas;
using Precentacion.User.Accounts;
using Precentacion.User.AdmProyecto;
using Precentacion.User.AgregarFactura;
using Precentacion.User.Bill;
using Precentacion.User.Client;
using Precentacion.User.Employer;
using Precentacion.User.Quote.Quote;
using Precentacion.User.RegProveedor;
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

namespace Precentacion.User.DashBoard
{
    public partial class frmDashUser : Form
    {
        private int borderRadius = 20;
        private int borderSize = 2;
        private Color borderColor = Color.FromArgb(224, 224, 224);
        SoundPlayClass soundPlayClass = new SoundPlayClass();
        public frmDashUser()
        {
            InitializeComponent();
            LoadNameUser();
            Roles();
            ShowForm(this);
        }
        #region Restrinciones de Roll
        private void Roles()
        {
            if (UserCache.Name == "VitroTaller")
            {
                //Ocultar todos los Botones menos el de Proformas
                btnCliente.Visible = false;
                btnEmpleado.Visible = false;
                btnCxC.Visible = false;
                btnCalendario.Visible = false;
                btnProyecto.Visible = false;

                btnFactura.Visible = false;

                //Posicion de los Botones
                btnOrden.Location = new Point(339, 6);




            }
        }
        #endregion



        #region Functions
        private void LoadNameUser()
        {

        }
        #endregion

        #region Buttons
        private void btnClient_Click(object sender, EventArgs e)
        {
            frmManagerClient frmManagerClient = new frmManagerClient();
            frmManagerClient.Show();
            this.Hide();

        }
        private void btnNewQuote_Click(object sender, EventArgs e)
        {
            frmQuote frmQuote = new frmQuote();
            frmQuote.Show();
            this.Hide();
        }

        private void ManagerQuotes_Click(object sender, EventArgs e)
        {
            frmManagerQuotes frmManagerQuotes = new frmManagerQuotes();
            frmManagerQuotes.Show();
            this.Hide();
        }

        private void btnCxC_Click(object sender, EventArgs e)
        {
            frmManagerCxC frmCxC = new frmManagerCxC();
            frmCxC.Show();
            this.Hide();
        }
        private void btnEmployer_Click(object sender, EventArgs e)
        {
            frmManagerEmployers frmViewEmployer = new frmManagerEmployers();
            frmViewEmployer.Show();
            this.Hide();
        }
        private void btnPlanilla_Click(object sender, EventArgs e)
        {
            Int64 IdCompany = CompanyCache.IdCompany;
            Clipboard.SetText(IdCompany.ToString());
            //Abrir Otra Aplicacion
            System.Diagnostics.Process.Start("C:\\Users\\-Marlon\\Desktop\\GlassWin Proyect\\Contabilidad VitroStudio\\Presentacion\\bin\\Debug\\Presentacion.exe");
            this.Hide();

        }
        private void btnCalendar_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://calendar.google.com/calendar/u/0/r/tasks");

        }





        #endregion



        private void btnLibroBancos_Click(object sender, EventArgs e)
        {
            btnPlanilla_Click(sender, e);
            this.Hide();
        }

        private void btnAdmProyecto_Click(object sender, EventArgs e)
        {
            frmAdmProyecto frmAdmProyecto = new frmAdmProyecto();
            frmAdmProyecto.Show();
            this.Hide();

        }

        //Crear funcion MouseHover 
        private void Buttons_MouseHover(object sender, EventArgs e)
        {

            //cuando se seleccione cambiar el color del a trasparencia
            ((Button)sender).FlatStyle = FlatStyle.Popup;
            ((Button)sender).FlatAppearance.MouseDownBackColor = Color.Transparent;
            ((Button)sender).FlatAppearance.MouseOverBackColor = Color.Transparent;
        }

        //Crear funcion MouseLeave
        private void Buttons_MouseLeave(object sender, EventArgs e)
        {
        }

        private void btnRegProveedor_Click(object sender, EventArgs e)
        {
            frmRegistroProveedor frmRegistroProveedor = new frmRegistroProveedor();
            frmRegistroProveedor.Show();
            this.Hide();
        }

        private void btnFactura_Click(object sender, EventArgs e)
        {
            frmAgregarFacturaProveedor frmAgregarFacturaProveedor = new frmAgregarFacturaProveedor();
            frmAgregarFacturaProveedor.Show();
            this.Hide();
        }

        private void btnOrdenProd_Click(object sender, EventArgs e)
        {

            frmOrdenProduccion frmOrdenProduccion = new frmOrdenProduccion();

            // Obtener el tamaño de la pantalla
            Rectangle screen = Screen.PrimaryScreen.WorkingArea;

            // Establecer el tamaño del formulario para que ocupe la mitad de la pantalla
            frmOrdenProduccion.Size = new Size(screen.Width / 2, screen.Height);

            // Establecer la posición del formulario para que ocupe la mitad derecha de la pantalla
            frmOrdenProduccion.Location = new Point(screen.Width / 2, 0);

            //frmOrdenProduccion.FormBorderStyle = FormBorderStyle.None; // Sin bordes
            frmOrdenProduccion.TopMost = true; // Siempre en el tope
            frmOrdenProduccion.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmPruebaDimensionar frm = new frmPruebaDimensionar();
            frm.Show();
        }

        private void btnCliente_MouseEnter(object sender, EventArgs e)
        {

            btnCliente.Image = Image.FromFile(Application.StartupPath + "\\Images\\Icons\\Cliente Gift.gif");
        }

        private void btnCliente_MouseLeave(object sender, EventArgs e)
        {
            btnCliente.Image = Image.FromFile(Application.StartupPath + "\\Images\\Icons\\Cliente Statico.png");
        }

        //Crear funcion qQue muestre el Formulario en la Parte Superior de la Pantalla y en el centro
        private void ShowForm(Form frm)
        {
            // Obtener el tamaño de la pantalla
            Rectangle screen = Screen.PrimaryScreen.WorkingArea;

            // Establecer la posición del formulario para que ocupe la mitad Superior de la pantalla
            frm.Location = new Point(screen.Width / 5, 0);

            // Establecer el formulario sin bordes
            //frm.FormBorderStyle = FormBorderStyle.None;

            // Establecer el formulario siempre en el tope
            frm.TopMost = true;

            // Mostrar el formulario
            frm.Show();
        }

        private void btnOrden_MouseEnter(object sender, EventArgs e)
        {
            btnOrden.Image = Image.FromFile(Application.StartupPath + "\\Images\\Icons\\Orden Gift.gif");
        }

        private void btnOrden_MouseLeave(object sender, EventArgs e)
        {
            btnOrden.Image = Image.FromFile(Application.StartupPath + "\\Images\\Icons\\Orden Statica.png");
        }

        private void btnFactura_MouseHover(object sender, EventArgs e)
        {
            btnFactura.Image = Image.FromFile(Application.StartupPath + "\\Images\\Icons\\Factura Gift.gif");
        }

        private void btnFactura_MouseLeave(object sender, EventArgs e)
        {
            btnFactura.Image = Image.FromFile(Application.StartupPath + "\\Images\\Icons\\Factura Statico.png");
        }

        private void btnCerrar_MouseEnter(object sender, EventArgs e)
        {
            btnCerrar.Image = Image.FromFile(Application.StartupPath + "\\Images\\Icons\\Cerrar Gift.gif");
        }

        private void btnCerrar_MouseLeave(object sender, EventArgs e)
        {
            btnCerrar.Image = Image.FromFile(Application.StartupPath + "\\Images\\Icons\\Cerrar Statico.png");
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea cerrar la aplicación?", "Cerrar aplicación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnCxC_MouseEnter(object sender, EventArgs e)
        {
            btnCxC.Image = Image.FromFile(Application.StartupPath + "\\Images\\Icons\\CxC Gift.gif");
        }

        private void btnCxC_MouseLeave(object sender, EventArgs e)
        {
            btnCxC.Image = Image.FromFile(Application.StartupPath + "\\Images\\Icons\\CxC Static.png");
        }

        private void btnEmpleado_MouseHover(object sender, EventArgs e)
        {
            btnEmpleado.Image = Image.FromFile(Application.StartupPath + "\\Images\\Icons\\Empleado Gift.gif");
        }

        private void btnEmpleado_MouseLeave(object sender, EventArgs e)
        {
            btnEmpleado.Image = Image.FromFile(Application.StartupPath + "\\Images\\Icons\\Empleado Statico.png");
        }

        private void btnProyecto_MouseEnter(object sender, EventArgs e)
        {
            btnProyecto.Image = Image.FromFile(Application.StartupPath + "\\Images\\Icons\\Proyecto Gift.gif");
        }

        private void btnProyecto_MouseLeave(object sender, EventArgs e)
        {
            btnProyecto.Image = Image.FromFile(Application.StartupPath + "\\Images\\Icons\\Proyecto Statico.png");
        }

        private void btnCalendario_MouseEnter(object sender, EventArgs e)
        {
            btnCalendario.Image = Image.FromFile(Application.StartupPath + "\\Images\\Icons\\Calendario Gift.gif");
        }

        private void btnCalendario_MouseLeave(object sender, EventArgs e)
        {
            btnCalendario.Image = Image.FromFile(Application.StartupPath + "\\Images\\Icons\\Calendario Statico.png");
        }

        private void btnFactProveedor_MouseEnter(object sender, EventArgs e)
        {
            btnFactProveedor.Image = Image.FromFile(Application.StartupPath + "\\Images\\Icons\\FactProveedor Gift.gif");
        }

        private void btnFactProveedor_MouseLeave(object sender, EventArgs e)
        {
            btnFactProveedor.Image = Image.FromFile(Application.StartupPath + "\\Images\\Icons\\FactProveedor Statico.png");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmOrdenProduccion frmOrdenProduccion = new frmOrdenProduccion();
            frmOrdenProduccion.Owner = this;
            frmOrdenProduccion.Show();
            this.Hide();

        }



        // Variables para almacenar la posición del mouse
        private bool isDragging = false;
        private Point startPoint = new Point(0, 0);

        private void BarraSuperior_MouseDown(object sender, MouseEventArgs e)
        {
            // Cuando el botón izquierdo del mouse es presionado
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                startPoint = new Point(e.X, e.Y);
            }
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            frmAdminDashboard frmAdminDashboard = new frmAdminDashboard();
            frmAdminDashboard.Owner = this;
            frmAdminDashboard.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            //pictureBox4.Image = Image.FromFile(Application.StartupPath + "\\Images\\Icons\\minimize.gif");
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            //pictureBox4.Image = Image.FromFile(Application.StartupPath + "\\Images\\Icons\\minimize.png");
        }

        private void btnMinimizar_MouseEnter(object sender, EventArgs e)
        {
            btnMinimizar.Image = Image.FromFile(Application.StartupPath + "\\Images\\Icons\\minimize Gift.gif");
        }

        private void btnMinimizar_MouseLeave(object sender, EventArgs e)
        {
            btnMinimizar.Image = Image.FromFile(Application.StartupPath + "\\Images\\Icons\\minimize Static.png");
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
