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
            NumericUpDown cb = actualForm.Controls.Find("numericVeiculos", true).FirstOrDefault() as NumericUpDown;
            if (cb != null)
            {
                cb.BackColor = Color.White;
            }
            TextBox txt3 = actualForm.Controls.Find("txtDistancia", true).FirstOrDefault() as TextBox;
            if (txt3 != null)
            {
                txt3.BackColor = Color.White;
            }
            TextBox txt4 = actualForm.Controls.Find("txtPrecioxKM", true).FirstOrDefault() as TextBox;
            if (txt4 != null)
            {
                txt4.BackColor = Color.White;
            }
            TextBox txt5 = actualForm.Controls.Find("txtTotalGasolina", true).FirstOrDefault() as TextBox;
            if (txt5 != null)
            {
                txt5.BackColor = Color.WhiteSmoke;
            }
            NumericUpDown cb2 = actualForm.Controls.Find("NumericEmpleados", true).FirstOrDefault() as NumericUpDown;
            if (cb2 != null)
            {
                cb2.BackColor = Color.White;
            }
            TextBox txt6 = actualForm.Controls.Find("txtDesayuno", true).FirstOrDefault() as TextBox;
            if (txt6 != null)
            {
                txt6.BackColor = Color.White;
            }
            TextBox txt7 = actualForm.Controls.Find("txtAlmuerzo", true).FirstOrDefault() as TextBox;
            if (txt7 != null)
            {
                txt7.BackColor = Color.White;
            }
            TextBox txt8 = actualForm.Controls.Find("txtCena", true).FirstOrDefault() as TextBox;
            if (txt8 != null)
            {
                txt8.BackColor = Color.White;
            }
            NumericUpDown cb3 = actualForm.Controls.Find("NumericDias", true).FirstOrDefault() as NumericUpDown;
            if (cb3 != null)
            {
                cb3.BackColor = Color.White;
            }
            TextBox txt9 = actualForm.Controls.Find("txtTotalComida", true).FirstOrDefault() as TextBox;
            if (txt9 != null)
            {
                txt9.BackColor = Color.WhiteSmoke;
            }
            NumericUpDown cb4 = actualForm.Controls.Find("NumericHabitaciones", true).FirstOrDefault() as NumericUpDown;
            if (cb4 != null)
            {
                cb4.BackColor = Color.White;
            }
            TextBox txt10 = actualForm.Controls.Find("txtPrecioHabitacion", true).FirstOrDefault() as TextBox;
            if (txt10 != null)
            {
                txt10.BackColor = Color.White;
            }
            NumericUpDown cb5 = actualForm.Controls.Find("NumericNoches", true).FirstOrDefault() as NumericUpDown;
            if (cb5 != null)
            {
                cb5.BackColor = Color.White;
            }
            TextBox txt11 = actualForm.Controls.Find("txtTotalHospedaje", true).FirstOrDefault() as TextBox;
            if (txt11 != null)
            {
                txt11.BackColor = Color.WhiteSmoke;
            }
            ComboBox cb6 = actualForm.Controls.Find("cmbEmpleados", true).FirstOrDefault() as ComboBox;
            if (cb6 != null)
            {
                cb6.BackColor = Color.White;
            }
            TextBox txt12 = actualForm.Controls.Find("txtSalarios", true).FirstOrDefault() as TextBox;
            if (txt12 != null)
            {
                txt12.BackColor = Color.White;
            }
            TextBox txt13 = actualForm.Controls.Find("txtHoras", true).FirstOrDefault() as TextBox;
            if (txt13 != null)
            {
                txt13.BackColor = Color.White;
            }
            TextBox txt14 = actualForm.Controls.Find("textBox2", true).FirstOrDefault() as TextBox;
            if (txt14 != null)
            {
                txt14.BackColor = Color.WhiteSmoke;
            }
            TextBox txt15 = actualForm.Controls.Find("txtTotalViaticos", true).FirstOrDefault() as TextBox;
            if (txt15 != null)
            {
                txt15.BackColor = Color.WhiteSmoke;
            }

        }

        private static void ApplyFormattingToControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is Label label && label.Name != "lblAgenda" && label.Name != "lblTitleNew" && label.Name != "lblBusquedaNombre" && label.Name != "lblTitleEdit" && label.Name != "lblCantV" && label.Name != "lblDistK" && label.Name != "lblPPK" && label.Name != "lblTotal" && label.Name != "lblCantEmpleados" && label.Name != "lblDesayuna" && label.Name != "lblAlmuerzo" && label.Name != "lblCena" && label.Name != "lblCantDias" && label.Name != "lblTotal2" && label.Name != "lblCantHabitaciones" && label.Name != "lblPPH" && label.Name != "lblNoches" && label.Name != "lblTotal3" && label.Name != "lblListaEmpleados" && label.Name != "lblSalariosxHora" && label.Name != "lblCantHoras" && label.Name != "lblTotal4" && label.Name != "lblTotalT") //
                {
                    label.Font = new Font("Arial", 14, FontStyle.Bold);
                    label.ForeColor = Color.Black;
                }
                if (control is Label label2 && label2.Name == "lblGasolina")
                {
                    label2.Font = new Font("Arial", 14, FontStyle.Bold);
                    label2.ForeColor = Color.White;
                    label2.BackColor = Color.Orange;
                }//
                if (control is Label label3 && label3.Name == "lblComida")
                {
                    label3.Font = new Font("Arial", 14, FontStyle.Bold);
                    label3.ForeColor = Color.White;
                    label3.BackColor = Color.Orange;
                }//
                if (control is Label label4 && label4.Name == "lblHospedaje")
                {
                    label4.Font = new Font("Arial", 14, FontStyle.Bold);
                    label4.ForeColor = Color.White;
                    label4.BackColor = Color.Orange;
                }//
                if (control is Label label5 && label5.Name == "lblSalarios")
                {
                    label5.Font = new Font("Arial", 14, FontStyle.Bold);
                    label5.ForeColor = Color.White;
                    label5.BackColor = Color.Orange;
                }//
                if (control is Label label6 && label6.Name == "lblTotalViaticos")
                {
                    label6.Font = new Font("Arial", 14, FontStyle.Bold);
                    label6.ForeColor = Color.White;
                    label6.BackColor = Color.Orange;
                }//
                if (control is Label label7 && label7.Name == "lblTotalT")
                {
                    label7.Font = new Font("Arial", 14, FontStyle.Bold);
                    label7.ForeColor = Color.NavajoWhite;
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
                if (control is Button button2 && button2.Name == "btnAbrirMaps")
                {
                    button2.Font = new Font("Arial", 12, FontStyle.Bold);
                    button2.ForeColor = Color.White;
                    button2.BackColor = Color.Orange;
                }
                if (control is Button button3 && button3.Name == "btnHospedaje")
                {
                    button3.Font = new Font("Arial", 12, FontStyle.Bold);
                    button3.ForeColor = Color.White;
                    button3.BackColor = Color.Orange;
                }//lblTotalT



                // Llama recursivamente si el control tiene controles hijos
                if (control.Controls.Count > 0)
                {
                    ApplyButtonFormatting(control.Controls);
                }
            }
        }
    }
}
