using MaterialSkin.Controls;
using MaterialSkin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Precentacion.User.Employer
{
    internal class EmplyeeUI
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
        }

        private static void ApplyFormattingToControls(Control.ControlCollection controls)
        {//txtOrden
            foreach (Control control in controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.BackColor = Color.White;

                }
                else if (control is ComboBox combo)
                {

                    combo.BackColor = Color.White;
                }
                else if (control is GroupBox box)
                {

                    box.BackColor = Color.White;
                }
                else if (control is Label lab)
                {

                    lab.BackColor = Color.White;
                }
                else if (control is Panel pan)
                {

                    pan.BackColor = Color.White;
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
