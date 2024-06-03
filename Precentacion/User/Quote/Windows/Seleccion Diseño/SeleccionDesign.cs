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
                else if (control is Label label2 && label2.Name == "lblTitle")
                {
                    label2.Font = new Font("Arial", 19, FontStyle.Bold);
                    label2.ForeColor = Color.Orange;
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
