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
        }
        #region Restrinciones de Roll
        private void Roles()
        {
            if (UserCache.Name == "VitroTaller")
            {
                //Ocultar todos los Botones menos el de Proformas
                btnClient.Visible = false;
                btnEmployer.Visible = false;
                btnCxC.Visible = false;
                btnCalendar.Visible = false;
                btnAdmProyecto.Visible = false;
                button1.Visible = false;
                button2.Visible = false;
               
                btnManagerQuotes.Visible = false;

                //Posicion de los Botones
                btnNewQuote.Location = new Point(339, 6);

               


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
           
        }
        private void btnNewQuote_Click(object sender, EventArgs e)
        {
            frmQuote frmQuote = new frmQuote();
            frmQuote.Show();
           
        }

        private void ManagerQuotes_Click(object sender, EventArgs e)
        {
            frmManagerQuotes frmManagerQuotes = new frmManagerQuotes();
            frmManagerQuotes.Show();
           
        }

        private void btnCxC_Click(object sender, EventArgs e)
        {
            frmManagerCxC frmCxC = new frmManagerCxC();
            frmCxC.Show();
           
        }
        private void btnEmployer_Click(object sender, EventArgs e)
        {
            frmManagerEmployers frmViewEmployer = new frmManagerEmployers();
            frmViewEmployer.Show();
          
        }
        private void btnPlanilla_Click(object sender, EventArgs e)
        {
            Int64 IdCompany = CompanyCache.IdCompany;
           Clipboard.SetText(IdCompany.ToString());
           //Abrir Otra Aplicacion
           System.Diagnostics.Process.Start("C:\\Users\\-Marlon\\Desktop\\GlassWin Proyect\\Contabilidad VitroStudio\\Presentacion\\bin\\Debug\\Presentacion.exe");
           
           
        }
        private void btnCalendar_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://calendar.google.com/calendar/u/0/r/tasks");
            
        }





        #endregion



        private void btnLibroBancos_Click(object sender, EventArgs e)
        {
            btnPlanilla_Click(sender, e);
        }

        private void btnAdmProyecto_Click(object sender, EventArgs e)
        {
            frmAdmProyecto frmAdmProyecto = new frmAdmProyecto();
            frmAdmProyecto.Show();
         

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
           
        }

        private void btnFactura_Click(object sender, EventArgs e)
        {
            frmAgregarFacturaProveedor frmAgregarFacturaProveedor = new frmAgregarFacturaProveedor();
            frmAgregarFacturaProveedor.Show();

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
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmPruebaDimensionar frm = new frmPruebaDimensionar();
            frm.Show();
        }
    }
}
