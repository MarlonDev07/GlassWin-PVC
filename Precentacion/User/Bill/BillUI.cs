using MaterialSkin.Controls;
using MaterialSkin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Precentacion.User.Bill
{
    internal class BillUI
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
                if (control is TextBox textBox )
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
                if (control is Label label && label.Name != "label2" && label.Name != "label3" && label.Name != "label4" && label.Name != "label5" && label.Name != "label6" && label.Name != "label7") //lblBuscar
                {
                    label.Font = new Font("Arial", 14, FontStyle.Regular);
                    label.ForeColor = Color.Black;
                }
                else if (control is Label label2 && label2.Name == "label2") {
                    label2.Font = new Font("Arial", 11, FontStyle.Regular);
                    label2.ForeColor = Color.Black;
                }
                else if (control is Label label3 && label3.Name == "label3")
                {
                    label3.Font = new Font("Arial", 11, FontStyle.Regular);
                    label3.ForeColor = Color.Black;
                }
                else if (control is Label label4 && label4.Name == "label4")
                {
                    label4.Font = new Font("Arial", 11, FontStyle.Regular);
                    label4.ForeColor = Color.Black;
                }
                else if (control is Label label5 && label5.Name == "label5")
                {
                    label5.Font = new Font("Arial", 11, FontStyle.Regular);
                    label5.ForeColor = Color.Black;
                }
                else if (control is Label label6 && label6.Name == "label6")
                {
                    label6.Font = new Font("Arial", 14, FontStyle.Bold);
                    label6.ForeColor = Color.Orange;
                }
                else if (control is Label label7 && label7.Name == "label7")
                {
                    label7.Font = new Font("Arial", 14, FontStyle.Bold);
                    label7.ForeColor = Color.Orange;
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
