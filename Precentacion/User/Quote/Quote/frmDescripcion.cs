using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Precentacion.User.Quote.Quote
{
    public partial class frmDescripcion : Form
    {
        public string Descripcion { get; private set; }

        public frmDescripcion()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Descripcion = txtDescripcion.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
