using MaterialSkin.Controls;
using MaterialSkin;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Precentacion.User.Quote.Quote
{
    internal class QuoteUI
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

            // Agregar un control MaterialScrollBar al formulario
            MaterialScrollBar materialScrollBar1 = new MaterialScrollBar();
            materialScrollBar1.Dock = DockStyle.Right;
            actualForm.Controls.Add(materialScrollBar1);

            // Manejar el evento Scroll del MaterialScrollBar para desplazar el contenido del formulario
            materialScrollBar1.Scroll += (sender, e) =>
            {
                // Ajusta la posición del formulario al desplazar la barra de desplazamiento vertical
                actualForm.VerticalScroll.Value = materialScrollBar1.Value;
            };

            // Aplica formato a los textos en los controles del formulario
            ApplyFormattingToControls(actualForm.Controls);

            // Agrega una barra de desplazamiento vertical al formulario
            actualForm.AutoScroll = true;
        }

        private static void ApplyFormattingToControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is Label label && label.Name != "lblAgenda" && label.Name != "lblTitleNew" && label.Name != "lblBusquedaNombre" && label.Name != "lblTitleEdit" && label.Name != "lblBuscar") //lblBuscar
                {
                    label.Font = new Font("Arial", 14, FontStyle.Regular);
                    label.ForeColor = Color.Black;
                }
                else if (control is Label label2 && label2.Name == "lblTitleEdit")
                {
                    label2.Font = new Font("Arial", 19, FontStyle.Bold);
                    label2.ForeColor = Color.Orange;
                }
                else if (control is Label label3 && label3.Name == "lblBusquedaNombre")
                {
                    label3.Font = new Font("Arial", 9, FontStyle.Regular);
                    label3.ForeColor = Color.Black;
                }
                else if (control is TextBox textBox)
                {
                    textBox.BackColor = Color.White;
                }
                else if (control is Label label4 && label4.Name == "lblTitleEdit")
                {
                    label4.Font = new Font("Arial", 19, FontStyle.Bold);
                    label4.ForeColor = Color.Orange;
                }
                else if (control is Label label5 && label5.Name == "lblTitleNew")
                {
                    label5.Font = new Font("Arial", 19, FontStyle.Bold);
                    label5.ForeColor = Color.Orange;
                }
                else if (control is Label label6 && label6.Name == "lblBuscar")
                {
                    label6.Font = new Font("Arial", 13, FontStyle.Regular);
                    label6.ForeColor = Color.Orange;
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
