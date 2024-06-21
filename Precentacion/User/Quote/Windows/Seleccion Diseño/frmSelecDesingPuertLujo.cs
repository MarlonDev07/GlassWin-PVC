﻿using Dominio.Model.ClassWindows;
using System;
using System.Windows.Forms;
using MaterialSkin.Controls;
using Precentacion.User.Quote.Windows.Seleccion_Diseño;

namespace Precentacion.User.Quote.Windows
{
    public partial class frmSelecDesingPuertLujo : MaterialForm
    {
        private bool _isOpeningAnotherForm = false;
        public string system2 { get; set; }

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

        private void OpenCalcPriceWindows(string desing)
        {
            ClsWindows.Desing = desing;
            _isOpeningAnotherForm = true;
            frmCalcPriceWindows frm = new frmCalcPriceWindows();
            frm.Show();
            this.Close();
        }

        private void btn1Fijo_Click(object sender, EventArgs e)
        {
            OpenCalcPriceWindows("1 Hoja PL");
        }

        private void btn2Hoja_Click(object sender, EventArgs e)
        {
            OpenCalcPriceWindows("2 Hoja PL");
        }

        private void btn1HojaDivicion_Click(object sender, EventArgs e)
        {
            OpenCalcPriceWindows("1 Hoja Con Divicion PL");
        }

        private void btnHojaDivicion_Click(object sender, EventArgs e)
        {
            OpenCalcPriceWindows("2 Hoja Con Divicion PL");
        }

        private void btnBackSistema_Click(object sender, EventArgs e)
        {
            _isOpeningAnotherForm = true;
            frmSelectSystem frm = new frmSelectSystem();
            frm.Show();
            this.Close();
        }

        private void frmSelecDesingPuertLujo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isOpeningAnotherForm)
            {
                frmSelectSystem frm = new frmSelectSystem();
                frm.Show();
            }
        }

        private void frmSelecDesingPuertLujo_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!_isOpeningAnotherForm)
            {
                frmSelectSystem frm = new frmSelectSystem();
                frm.Show();
            }
        }

        private void frmSelecDesingPuertLujo_Load(object sender, EventArgs e)
        {
            // Verificar el valor de la variable y cambiar el título del formulario.
            if (system2 == "Puerta Liviana")
            {
                this.Text = "Seleccionar diseño puerta liviana";
            }
            else if (system2 == "PuertaEuAbatible")
            {
                this.Text = "Seleccionar diseño Puerta Europa Abatible";
                MostrarBotonesPuertaEuAbatible();
            }
        }

        private void MostrarBotonesPuertaEuAbatible()
        {
            // Ocultar botones de Puerta Liviana
            btn1Hoja.Visible = false;
            btn1HojaDivicion.Visible = false;
            btn2Hoja.Visible = false;
            btnHojaDivicion.Visible = false;

            // Mostrar botones de Puerta Europa Abatible
            btnHojaEA.Visible = true;
            btnHoja2EA.Visible = true;
            btnHoja1DEA.Visible = true;
            btnHoja2DEA.Visible = true;
        }

        private void btnHojaEA_Click(object sender, EventArgs e)
        {
            OpenCalcPriceWindows("1 Hoja EA");
        }

        private void btnHoja2EA_Click(object sender, EventArgs e)
        {
            OpenCalcPriceWindows("2 Hoja EA");
        }

        private void btnHoja1DEA_Click(object sender, EventArgs e)
        {
            OpenCalcPriceWindows("1 Hoja DEA");
        }

        private void btnHoja2DEA_Click(object sender, EventArgs e)
        {
            OpenCalcPriceWindows("2 Hoja DEA");
        }
    }
}
