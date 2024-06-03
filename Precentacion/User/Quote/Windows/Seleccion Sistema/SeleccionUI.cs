using MaterialSkin.Controls;
using MaterialSkin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Precentacion.User.Quote.Windows.Seleccion_Sistema
{
    public class SeleccionUI
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
            Panel panelSeleccionDiseño = actualForm.Controls.Find("PanelContenedorSistemaTradicional", true).FirstOrDefault() as Panel;
            if (panelSeleccionDiseño != null)
            {
                panelSeleccionDiseño.BackColor = Color.White;
            }
            Label lblTitle = actualForm.Controls.Find("lblVtNormal", true).FirstOrDefault() as Label;
            if (lblTitle != null)
            {
                lblTitle.BackColor = Color.White;
            }
            Label lblSistemasAkari = actualForm.Controls.Find("lblSistemasAkari", true).FirstOrDefault() as Label;
            if (lblSistemasAkari != null)
            {
                lblSistemasAkari.BackColor = Color.White;
            }
            Panel panel1 = actualForm.Controls.Find("panel1", true).FirstOrDefault() as Panel;
            if (panel1 != null)
            {
                panel1.BackColor = Color.White;
            }
            Panel panel4 = actualForm.Controls.Find("panel4", true).FirstOrDefault() as Panel;
            if (panel4 != null)
            {
                panel4.BackColor = Color.White;
            }
            Label lblSistemasEuropa = actualForm.Controls.Find("lblSistemasEuropa", true).FirstOrDefault() as Label;
            if (lblSistemasEuropa != null)
            {
                lblSistemasEuropa.BackColor = Color.White;
            }
            Label lblPuertas = actualForm.Controls.Find("lblPuertas", true).FirstOrDefault() as Label;
            if (lblPuertas != null)
            {
                lblPuertas.BackColor = Color.White;
            }
            Panel panel5 = actualForm.Controls.Find("panel5", true).FirstOrDefault() as Panel;
            if (panel5 != null)
            {
                panel5.BackColor = Color.White;
            }


        }

        private static void ApplyFormattingToControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is Label label && label.Name != "lblTitle" && label.Name != "lblVtNormal" && label.Name != "lblSistemasAkari" && label.Name != "lblSistemasEuropa" && label.Name != "lblPuertas")//lblPuertas
                {
                    label.Font = new Font("Arial", 14, FontStyle.Regular);
                    label.ForeColor = Color.Black;
                }
                else if (control is Label label2 && label2.Name == "lblTitleEdit")
                {
                    label2.Font = new Font("Arial", 19, FontStyle.Bold);
                    label2.ForeColor = Color.Orange;
                }
                else if (control is TextBox textBox)
                {
                    textBox.BackColor = Color.White;
                }
                else if (control is Label label4 && label4.Name == "lblTitle")
                {
                    label4.Font = new Font("Arial", 19, FontStyle.Bold);
                    label4.ForeColor = Color.Orange;
                }//
                else if (control is Label label5 && label5.Name == "lblVtNormal" )
                {
                    label5.Font = new Font("Arial", 14, FontStyle.Bold);
                    label5.ForeColor = Color.Orange;
                }
                else if (control is Label label6 && label6.Name == "lblSistemasAkari")
                {
                    label6.Font = new Font("Arial", 14, FontStyle.Bold);
                    label6.ForeColor = Color.Orange;
                }
                else if (control is Label label7 && label7.Name == "lblSistemasEuropa")
                {
                    label7.Font = new Font("Arial", 14, FontStyle.Bold);
                    label7.ForeColor = Color.Orange;
                }
                else if (control is Label label8 &&  label8.Name == "lblPuertas")
                {
                    label8.Font = new Font("Arial", 14, FontStyle.Bold);
                    label8.ForeColor = Color.Orange;
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
