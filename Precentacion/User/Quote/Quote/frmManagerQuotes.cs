using Negocio.Company.Account;
using Negocio.Company.Bill;
using Negocio.Company.Quote;
using Precentacion.User.Bill;
using Precentacion.User.DashBoard;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using MaterialSkin.Controls;
using System.Drawing;

namespace Precentacion.User.Quote.Quote
{
    public partial class frmManagerQuotes : MaterialForm
    {
        #region Variables
        bool EventClose = true;
        N_Quote NQuote = new N_Quote();
        N_Bill N_Bill = new N_Bill();
        N_CxC N_CxC = new N_CxC();
        #endregion

        #region Constructor
        public frmManagerQuotes()
        {
            InitializeComponent();
            frmManagerQuotes_Load(null, null);
            QuoteUI.loadMaterial(this);

            // Suscribe el evento RowPrePaint
            dgvQuotes.RowPrePaint += new DataGridViewRowPrePaintEventHandler(dgvQuotes_RowPrePaint);
        }
        #endregion

        #region Load Functions
        public void frmManagerQuotes_Load(object sender, EventArgs e)
        {
            DataTable dataTable = NQuote.LoadQuotes();
            if (dataTable != null)
            {
                dgvQuotes.DataSource = dataTable;
                //Hacer Invisbles la Columna [0]
                dgvQuotes.Columns[0].Visible = false;

                //Cambiar Nombre de las Columnas
                dgvQuotes.Columns[1].HeaderText = "N° Proforma";
                dgvQuotes.Columns[2].HeaderText = "Nombre del Cliente";
                dgvQuotes.Columns[3].HeaderText = "Nombre del Proyecto";
                dgvQuotes.Columns[4].HeaderText = "Direcccion";
                dgvQuotes.Columns[5].HeaderText = "Subtotal";
                dgvQuotes.Columns[6].HeaderText = "Impuesto";
                dgvQuotes.Columns[7].HeaderText = "Total";

                //Ajustar las columnas al Ancho del formulario
                dgvQuotes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Configura el color de la fila seleccionada
                dgvQuotes.DefaultCellStyle.SelectionBackColor = Color.Green;
                dgvQuotes.DefaultCellStyle.SelectionForeColor = Color.Green;
            }
        }
        #endregion

        #region RowPrePaint Event Handler
        private void dgvQuotes_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
            {
                dgvQuotes.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
            }
            else
            {
                dgvQuotes.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Orange;
            }
        }
        #endregion

        #region Button Context Menu Strip
        private void editarProformaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Preguntar si desea editar la proforma seleccionada
            DialogResult result = MessageBox.Show("¿Desea editar la proforma n° " + dgvQuotes.CurrentRow.Cells[1].Value.ToString() + "?", "Editar Proforma", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //Enviar el numero de proforma seleccionado al formulario de proforma
            frmQuote frmQuote = new frmQuote();
            frmQuote.txtidClient.Text = dgvQuotes.CurrentRow.Cells[0].Value.ToString();
            frmQuote.btnBuscar_Click(null, null);
            frmQuote.txtProjetName.Text = dgvQuotes.CurrentRow.Cells[3].Value.ToString();
            frmQuote.txtAddress.Text = dgvQuotes.CurrentRow.Cells[4].Value.ToString();
            frmQuote.txtidQuote.Text = dgvQuotes.CurrentRow.Cells[1].Value.ToString();
            ((frmQuote)frmQuote).LoadDataQuote();
            frmQuote.Edit = true;
            frmQuote.EventClose = false;
            EventClose = false;
            frmQuote.Show();
        }

        private void cotizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventClose = false;
            frmQuote frmQuote = new frmQuote();
            frmQuote.Show();
            this.Close();
        }

        private void facturarProformaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Validar si el formulario frmFacturar esta abierto
            frmFacturar frm = Application.OpenForms.OfType<frmFacturar>().FirstOrDefault();
            if (frm != null)
            {
                ((frmFacturar)frm).txtidClient.Text = dgvQuotes.CurrentRow.Cells[0].Value.ToString();
                ((frmFacturar)frm).txtidQuote.Text = dgvQuotes.CurrentRow.Cells[1].Value.ToString();
                ((frmFacturar)frm).txtProjetName.Text = dgvQuotes.CurrentRow.Cells[3].Value.ToString();
                ((frmFacturar)frm).txtAddress.Text = dgvQuotes.CurrentRow.Cells[4].Value.ToString();
                ((frmFacturar)frm).Subtotal = Convert.ToDecimal(dgvQuotes.CurrentRow.Cells[5].Value.ToString());
                ((frmFacturar)frm).IVA = Convert.ToDecimal(dgvQuotes.CurrentRow.Cells[6].Value.ToString());
                ((frmFacturar)frm).Total = Convert.ToDecimal(dgvQuotes.CurrentRow.Cells[7].Value.ToString());
                ((frmFacturar)frm).btnBuscar_Click(null, null);
                ((frmFacturar)frm).InitializeComponents_Click(null, null);
                EventClose = false;
            }
            else
            {
                frmFacturar frmFacturar = new frmFacturar();
                frmFacturar.txtidClient.Text = dgvQuotes.CurrentRow.Cells[0].Value.ToString();
                frmFacturar.txtidQuote.Text = dgvQuotes.CurrentRow.Cells[1].Value.ToString();
                frmFacturar.txtProjetName.Text = dgvQuotes.CurrentRow.Cells[3].Value.ToString();
                frmFacturar.txtAddress.Text = dgvQuotes.CurrentRow.Cells[4].Value.ToString();
                frmFacturar.Subtotal = Convert.ToDecimal(dgvQuotes.CurrentRow.Cells[5].Value.ToString());
                frmFacturar.IVA = Convert.ToDecimal(dgvQuotes.CurrentRow.Cells[6].Value.ToString());
                frmFacturar.Total = Convert.ToDecimal(dgvQuotes.CurrentRow.Cells[7].Value.ToString());

                frmFacturar.btnBuscar_Click(null, null);
                frmFacturar.InitializeComponents_Click(null, null);
                frmFacturar.Show();
                EventClose = false;
                this.Close();
            }
        }
        #endregion

        private void frmManagerQuotes_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (EventClose)
            {
                frmDashUser frmDashUser = new frmDashUser();
                frmDashUser.Show();
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            dgvQuotes.CurrentCell = null;
            try
            {
                foreach (DataGridViewRow r in dgvQuotes.Rows)
                {
                    bool rowVisible = false;
                    foreach (DataGridViewCell c in r.Cells)
                    {
                        if (c.Value != null && c.Value.ToString().ToUpper().Contains(txtBuscar.Text.ToUpper()))
                        {
                            rowVisible = true;
                            break;
                        }
                    }
                    r.Visible = rowVisible;
                }
            }
            catch (Exception ex)
            {
                // Manejar excepción
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Método vacío, posiblemente no necesario
        }

        private void dgvQuotes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Método vacío, posiblemente no necesario
        }
    }


}
