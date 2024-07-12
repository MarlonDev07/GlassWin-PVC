using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Precentacion.User.Bill
{
    public partial class frmOptimizador : Form
    {
        public frmOptimizador()
        {
            InitializeComponent();
            // Configurar el DataGridView al inicializar el formulario
            ConfigureDataGridView();
        }

        private void ConfigureDataGridView()
        {
            // Definir columnas para el DataGridView
            dgvResults.Columns.Add("colBar", "Barra");
            dgvResults.Columns.Add("colCuts", "Cortes");
        }

        private void btnOptimize_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener y validar las barras disponibles del TextBox
                double[] availableBars = txtAvailableBars.Text.Split(',')
                    .Select(s => ParseDouble(s.Trim()))
                    .ToArray();

                // Obtener y validar las longitudes requeridas del TextBox
                double[] requiredLengths = txtRequiredLengths.Text.Split(',')
                    .Select(s => ParseDouble(s.Trim()))
                    .ToArray();

                // Llamar al método OptimizeCuts
                List<List<double>> optimizedCuts = OptimizeCuts(availableBars, requiredLengths);

                // Mostrar los resultados en el DataGridView
                dgvResults.Rows.Clear();
                for (int i = 0; i < optimizedCuts.Count; i++)
                {
                    string bar = "Barra " + (i + 1);
                    string cuts = string.Join(", ", optimizedCuts[i].Select(c => c.ToString("0.00") + " m"));
                    dgvResults.Rows.Add(bar, cuts);
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Error: La cadena de entrada no tiene el formato correcto. Asegúrese de que los datos ingresados sean números válidos y estén separados por comas.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private double ParseDouble(string input)
        {
            if (!double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
            {
                throw new FormatException("La cadena de entrada no tiene el formato correcto.");
            }
            return result;
        }

        private List<List<double>> OptimizeCuts(double[] availableBars, double[] requiredLengths)
        {
            // Ordenar las longitudes requeridas de mayor a menor
            Array.Sort(requiredLengths);
            Array.Reverse(requiredLengths);

            // Lista para almacenar los cortes óptimos para cada barra
            List<List<double>> optimizedCuts = new List<List<double>>();

            foreach (double bar in availableBars)
            {
                List<double> cuts = new List<double>();
                double remainingLength = bar;

                foreach (double length in requiredLengths.ToList())
                {
                    if (remainingLength >= length)
                    {
                        cuts.Add(length);
                        remainingLength -= length;
                        requiredLengths = requiredLengths.Where(val => val != length).ToArray();
                    }
                }

                optimizedCuts.Add(cuts);
            }

            // Agregar cualquier corte que no pudo ser optimizado
            if (requiredLengths.Length > 0)
            {
                optimizedCuts.Add(new List<double>(requiredLengths));
            }

            return optimizedCuts;
        }
    }
}
