using Dominio.Model.ClassWindows;
using Precentacion.User.Quote.Windows.Calculos_de_Precio.Copia_frmCalcPriceVentanasFijas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Precentacion.User.Quote.Windows
{
    public partial class frmSelecDesingVentanaFija : MaterialSkin.Controls.MaterialForm
    {
        public frmSelecDesingVentanaFija()
        {
            InitializeComponent();
        }

        private void btn1Fijo_Click(object sender, EventArgs e)
        {
            ClsWindows.Desing = "1Fijo";
            frmCalcPriceVentanasFijas frm = new frmCalcPriceVentanasFijas();
            frm.Show();
            this.Close();
        }

        private void btn2Fijo_Click(object sender, EventArgs e)
        {
            ClsWindows.Desing = "2Fijo";
            frmCalcPriceVentanasFijas frm = new frmCalcPriceVentanasFijas();
            frm.Show();
            this.Close();
        }

        private void btn3Fijo_Click(object sender, EventArgs e)
        {
            ClsWindows.Desing = "3Fijo";
            frmCalcPriceVentanasFijas frm = new frmCalcPriceVentanasFijas();
            frm.Show();
            this.Close();
        }

        private void btn4Fijo_Click(object sender, EventArgs e)
        {
            ClsWindows.Desing = "4Fijo";
            frmCalcPriceVentanasFijas frm = new frmCalcPriceVentanasFijas();
            frm.Show();
            this.Close();
        }

        private void btn5Fijo_Click(object sender, EventArgs e)
        {
            ClsWindows.Desing = "5Fijo";
            frmCalcPriceVentanasFijas frm = new frmCalcPriceVentanasFijas();
            frm.Show();
            this.Close();
        }

        private void btn6Fijo_Click(object sender, EventArgs e)
        {
            ClsWindows.Desing = "6Fijo";
            frmCalcPriceVentanasFijas frm = new frmCalcPriceVentanasFijas();
            frm.Show();
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmSelecDesingVentanaFija_Load(object sender, EventArgs e)
        {

        }

        private void btnBackSistema_Click(object sender, EventArgs e)
        {
            frmSelectSystem frm = new frmSelectSystem();
            frm.Show();
            this.Close();
        }

        private void frmSelecDesingVentanaFija_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmSelectSystem frm = new frmSelectSystem();
            frm.Show();
        }
    }
}
