using Dominio.Model.ClassWindows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;

using Precentacion.User.Quote.Windows.Seleccion_Diseño;

namespace Precentacion.User.Quote.Windows
{
    public partial class frmSelecDesingPuertLujo : MaterialForm
    {
        public frmSelecDesingPuertLujo()
        {
            InitializeComponent();
            BloquearBotones();
            SeleccionDesign.loadMaterial(this);
        }

        private void BloquearBotones()
        {
            if (ClsWindows.System == "Puerta Liviana")
            {
                btn2Hoja.Visible = false;
                btnHojaDivicion.Visible = false; 
            }           
        }

        private void btn1Fijo_Click(object sender, EventArgs e)
        {
            ClsWindows.Desing = "1 Hoja PL";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Show();
            this.Close();
        }

        private void btn2Hoja_Click(object sender, EventArgs e)
        {
            
            ClsWindows.Desing = "2 Hoja PL";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Show();
            this.Close();
        }

        private void btn1HojaDivicion_Click(object sender, EventArgs e)
        {
            ClsWindows.Desing = "1 Hoja Con Divicion PL";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Show();
            this.Close();
        }

        private void btnHojaDivicion_Click(object sender, EventArgs e)
        {
            ClsWindows.Desing = "2 Hoja Con Divicion PL";
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Show();
            this.Close();
        }
    }
}
