using Dominio;
using Negocio.Client;
using Negocio.Company.Account;
using Negocio.Company.AdmProyecto;
using Negocio.Company.Bill;
using Precentacion.User.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Precentacion.User.Accounts
{
    public partial class frmCxCNew : MaterialSkin.Controls.MaterialForm
    {
        #region Variables
        N_Bill n_Bill = new N_Bill();
        N_CxC n_CxC = new N_CxC();
        N_AdmProyecto n_AdmProyecto = new N_AdmProyecto();
        N_Client NClient = new N_Client();
        int IdBill;
        int? IdClient = null;
        decimal Monto;
        #endregion

        #region Constructor
        public frmCxCNew()
        {
            InitializeComponent();
        }
        #endregion

        #region Metodos
        private void CreateCxC()
        {
            if (IdClient != null)
            {
                if (InsertBill())
                {
                    InsertCxC();
                    this.Close();
                }
            }


        }
        private bool InsertBill()
        {
            try
            {
                n_Bill.InsertBill(0,Convert.ToInt32(IdClient), DateTime.Now, DateTime.Now);
                IdBill = n_Bill.getLastID();
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Error al insertar la factura");
                return false;
            }
        }
        private void InsertCxC()
        {
            try
            {
                bool Res = n_CxC.InsertCxC(IdBill, Convert.ToDecimal(Monto), Convert.ToDecimal(Monto),txtProyecto.Text.Trim());
                if (Res)
                {
                    n_AdmProyecto.InsertarAdmProyecto(n_CxC.LastIdCxC(), txtProyecto.Text);
                }               
            }
            catch (Exception)
            {
                MessageBox.Show("Error al insertar la cuenta por cobrar");
            }
        }
        #endregion

        #region Eventos
        private void txtidClient_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtidClient.Text != "")
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    btnBuscar_Click(sender, e);
                }
            }
        }

        #endregion

        #region Botones
        public void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                List<clsClient> list = new List<clsClient>();
                list = NClient.ListClient(txtidClient.Text);
                if (list.Count == 0)
                {
                    DialogResult result = MessageBox.Show("No se encontro el Cliente ¿Desea Ver la Lista de Cliente?", "Cliente", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        frmManagerClient frm = new frmManagerClient();
                        frm.EventFormClose = false;
                        frm.Show();

                    }
                }
                foreach (var item in list)
                {
                    if (item.IdClient.ToString() == txtidClient.Text && item.IdCompany == CompanyCache.IdCompany)
                    {
                        IdClient = item.IdClient;
                        txtidClient.Text = item.Name;
                    }
                }
            }
            catch (Exception)
            {
                DialogResult result = MessageBox.Show("No se encontro el Cliente ¿Desea Ver la Lista de Cliente?", "Cliente", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    frmManagerClient frm = new frmManagerClient();
                    frm.Show();
                }

            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnAccept_Click(object sender, EventArgs e)
        {
            CreateCxC();
        }




        #endregion

        private void txtMonto_Leave(object sender, EventArgs e)
        {
            try
            {
                Monto = Convert.ToDecimal(txtMonto.Text);
                txtMonto.Text = Monto.ToString("C");
            }
            catch (Exception)
            {
            }          
        }

        private void txtMonto_Enter(object sender, EventArgs e)
        {
            //QUITAR EL FORMATO DE MONEDA
            txtMonto.Text =Monto.ToString();
        }
    }
}
