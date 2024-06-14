using MaterialSkin.Controls;
using MaterialSkin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Precentacion.User.Quote.Accesorios
{
    internal class AccesoriosUI
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

            // Aplica formato al botón btnCargarImagen
            ApplyButtonFormatting(actualForm.Controls);

            // Cambia el color de fondo del formulario a blanco
            actualForm.BackColor = Color.White;

            PictureBox panel = actualForm.Controls.Find("pbAccesorioExclusivo", true).FirstOrDefault() as PictureBox;
            if (panel != null)
            {
                panel.BackColor = Color.White;
            }
            TextBox txt = actualForm.Controls.Find("txtDescripcion", true).FirstOrDefault() as TextBox;
            if (txt != null)
            {
                txt.BackColor = Color.White;
            }
            TextBox txt2 = actualForm.Controls.Find("txtPrecio", true).FirstOrDefault() as TextBox;
            if (txt2 != null)
            {
                txt2.BackColor = Color.White;
            }
        }

        private static void ApplyFormattingToControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is Label label && label.Name != "lblAgenda" && label.Name != "lblTitleNew" && label.Name != "lblBusquedaNombre" && label.Name != "lblTitleEdit" && label.Name != "lblBuscar") //lblBuscar
                {
                    label.Font = new Font("Arial", 14, FontStyle.Bold);
                    label.ForeColor = Color.Black;
                }

                // Llama recursivamente si el control tiene controles hijos
                if (control.Controls.Count > 0)
                {
                    ApplyFormattingToControls(control.Controls);
                }
            }
        }

        private static void ApplyButtonFormatting(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is Button button && button.Name == "btnCargarImagen")
                {
                    button.Font = new Font("Arial", 14, FontStyle.Bold);
                    button.ForeColor = Color.Black;
                }

                // Llama recursivamente si el control tiene controles hijos
                if (control.Controls.Count > 0)
                {
                    ApplyButtonFormatting(control.Controls);
                }
            }
        }
    }
}
