using MaterialSkin.Controls;
using MaterialSkin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Precentacion.User.Quote.Windows.Seleccion_Diseño
{
    internal class SeleccionDesign
    {
        public static void loadMaterial(MaterialForm actualForm)
        {
            // Crea un administrador de temas de materiales y agrega el formulario para administrar los forms
            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(actualForm);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            // Configura el esquema de colores a tonos de naranja
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Orange500, Primary.Orange600,
                Primary.Orange700, Accent.Orange200,
                TextShade.WHITE
            );

            // Aplica formato a los textos en los controles del formulario
            ApplyFormattingToControls(actualForm.Controls);

            // Establece el fondo del panel y label a blanco
            Panel panelSeleccionDiseño = actualForm.Controls.Find("PanelSeleccionDiseño", true).FirstOrDefault() as Panel;
            if (panelSeleccionDiseño != null)
            {
                panelSeleccionDiseño.BackColor = Color.White;
            }
            Label lblTitle = actualForm.Controls.Find("lblTitle", true).FirstOrDefault() as Label;
            if (lblTitle != null)
            {
                lblTitle.BackColor = Color.White;
            }
            Label lblVentilaH = actualForm.Controls.Find("lblVentilaH", true).FirstOrDefault() as Label;
            if (lblVentilaH != null)
            {
                lblVentilaH.BackColor = Color.White;
            }
            Panel panel2 = actualForm.Controls.Find("panel2", true).FirstOrDefault() as Panel;
            if (panel2 != null)
            {
                panel2.BackColor = Color.White;
            }
            Label lblVentilaV = actualForm.Controls.Find("lblVentilaV", true).FirstOrDefault() as Label;
            if (lblVentilaV != null)
            {
                lblVentilaV.BackColor = Color.White;
            }
            Panel panel7 = actualForm.Controls.Find("panel8", true).FirstOrDefault() as Panel;
            if (panel7 != null)
            {
                panel7.BackColor = Color.White;
            }
            Panel panel8 = actualForm.Controls.Find("panel7", true).FirstOrDefault() as Panel;
            if (panel8 != null)
            {
                panel8.BackColor = Color.Orange;
            }
            Panel panel1 = actualForm.Controls.Find("panel1", true).FirstOrDefault() as Panel;
            if (panel1 != null)
            {
                panel1.BackColor = Color.Orange;
            }
            Label lblVentilaHF = actualForm.Controls.Find("lblVentilaHF", true).FirstOrDefault() as Label;
            if (lblVentilaHF != null)
            {
                lblVentilaHF.BackColor = Color.White;
            }
            Panel panel4 = actualForm.Controls.Find("panel4", true).FirstOrDefault() as Panel;
            if (panel4 != null)
            {
                panel4.BackColor = Color.White;

            }
            Panel panel3 = actualForm.Controls.Find("panel3", true).FirstOrDefault() as Panel;
            if (panel3 != null)
            {
                panel3.BackColor = Color.Orange;

            }
            Panel panel5 = actualForm.Controls.Find("panel5", true).FirstOrDefault() as Panel;
            if (panel5 != null)
            {
                panel5.BackColor = Color.Orange;


            }
            Label lblVentilaE = actualForm.Controls.Find("lblVentilaE", true).FirstOrDefault() as Label;
            if (lblVentilaE != null)
            {
                lblVentilaE.BackColor = Color.White;
            }
            Panel panel6 = actualForm.Controls.Find("panel6", true).FirstOrDefault() as Panel;
            if (panel6 != null)
            {
                panel6.BackColor = Color.White;

            }
            Panel pSeleccionarPL = actualForm.Controls.Find("pSeleccionarPL", true).FirstOrDefault() as Panel;
            if (pSeleccionarPL != null)
            {
                pSeleccionarPL.BackColor = Color.White;

            }
            Panel pSelVen = actualForm.Controls.Find("pSelVen", true).FirstOrDefault() as Panel;
            if (pSelVen != null)
            {
                pSelVen.BackColor = Color.White;

            }


        }
        private static void ApplyFormattingToControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is Label label && label.Name != "lblTitle" && label.Name != "lblVtNormal" && label.Name != "lblSistemasAkari" && label.Name != "lblSistemasEuropa" && label.Name != "lblPuertas" && label.Name != "lblVentilaH" && label.Name != "lblVentilaV" && label.Name != "lblVentilaHF" && label.Name != "lblVentilaE")//
                {
                    label.Font = new Font("Arial", 14, FontStyle.Regular);
                    label.ForeColor = Color.Black;
                }
                else if (control is Label label2 && label2.Name == "lblTitle")
                {
                    label2.Font = new Font("Arial", 19, FontStyle.Bold);
                    label2.ForeColor = Color.Orange;
                }
                else if (control is Label label3 && label3.Name == "lblVentilaH")
                {
                    label3.Font = new Font("Arial", 16, FontStyle.Bold);
                    label3.ForeColor = Color.Orange;
                }
                else if (control is Label label4 && label4.Name == "lblVentilaV")
                {
                    label4.Font = new Font("Arial", 16, FontStyle.Bold);
                    label4.ForeColor = Color.Orange;
                }
                else if (control is Label label5 && label5.Name == "lblVentilaHF")
                {
                    label5.Font = new Font("Arial", 16, FontStyle.Bold);
                    label5.ForeColor = Color.Orange;
                }
                else if (control is Label label6 && label6.Name == "lblVentilaE")
                {
                    label6.Font = new Font("Arial", 16, FontStyle.Bold);
                    label6.ForeColor = Color.Orange;
                }
                else if (control is TextBox textBox)
                {
                    textBox.BackColor = Color.White;
                }

                // Llama recursivamente si el control tiene controles hijos
                if (control.Controls.Count > 0)
                {
                    ApplyFormattingToControls(control.Controls);
                }
            }
        }
    }
}
