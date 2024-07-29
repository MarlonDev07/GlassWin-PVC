using Negocio.Company.Quote;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing;
using System.IO;
namespace Precentacion.User.Bill
{
    public partial class frmOptimizador : Form
    {
        // Variables
        N_Quote NQuote = new N_Quote();
        private decimal[] availableBars;
        private decimal[] requiredLengths;
        private decimal[] availableBarsU;
        private decimal[] requiredLengthsU;
        private decimal[] availableBarsJ;
        private (decimal length, int window)[] requiredLengthsJ;
        private decimal[] availableBarsS;
        private decimal[] requiredLengthsS;
        private decimal[] availableBarsI;
        private decimal[] requiredLengthsI;
        private decimal[] availableBarsV;
        private decimal[] requiredLengthsV;
        private decimal[] availableBarsVC;
        private decimal[] requiredLengthsVC;
        private Image defaultImage;
        private Image specificImage;


        public frmOptimizador((decimal length, int window)[] requiredLengthsJ, decimal[] availableBarsJ)
        {
            InitializeComponent();

            // Jamba
            this.requiredLengthsJ = requiredLengthsJ;
            this.availableBarsJ = availableBarsJ;

            // Configurar los DataGridView al inicializar el formulario
            ConfigureDataGridView(dgvResults3, defaultImage);

            // Ejecutar la optimización
            OptimizeCutsAndDisplayResults(dgvResults3, requiredLengthsJ, availableBarsJ);
        }



        private void ConfigureDataGridView(DataGridView dgv, Image defaultImage)
        {
            // Definir columnas para el DataGridView
            dgv.Columns.Clear();
            dgv.Columns.Add("colBar", "Barra 6.40");

            // Crear y añadir una columna de imagen con imagen por defecto
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn
            {
                Name = "colUbicacion",
                HeaderText = "Corte",
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                DefaultCellStyle = { NullValue = defaultImage }
            };
            dgv.Columns.Add(imageColumn);

            dgv.Columns.Add("colCuts", "Dimensiones");
            dgv.Columns.Add("colResiduos", "Retal"); // Nueva columna para residuos

            // Ajustar el tamaño de las columnas
            AdjustColumnWidthsToFitContent(dgv);
        }

        private void AdjustColumnWidthsToFitContent(DataGridView dgv)
        {
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void OptimizeCutsAndDisplayResults(DataGridView dgv, (decimal length, int window)[] requiredLengths, decimal[] availableBars)
        {
            try
            {
                // Asignar números secuenciales a las longitudes requeridas
                List<(decimal length, int window, int number)> requiredLengthsWithNumbers = requiredLengths
                    .Select((length, index) => (length.length, length.window, number: index + 1))
                    .ToList();

                // Llamar al método OptimizeCuts
                List<List<(decimal length, int window, int number)>> optimizedCuts = OptimizeCuts(availableBars, requiredLengthsWithNumbers);

                // Mostrar los resultados en el DataGridView
                dgv.Rows.Clear();

                for (int i = 0; i < optimizedCuts.Count; i++)
                {
                    string bar = "Barra " + (i + 1);
                    string cuts = string.Join(", ", optimizedCuts[i].Select(c => $"{c.length.ToString("0.000", CultureInfo.InvariantCulture)} m (V{c.window})"));

                    // Seleccionar la imagen adecuada
                    Image ubicacionImage = string.IsNullOrWhiteSpace(cuts) ? defaultImage : specificImage;

                    // Si "Cortes" está vacío, saltar esta iteración
                    if (string.IsNullOrWhiteSpace(cuts))
                    {
                        continue;
                    }

                    // Calcular el residuo
                    decimal totalCuts = optimizedCuts[i].Sum(c => c.length);
                    decimal barLength = availableBars[i];
                    decimal residue = barLength - totalCuts;

                    // Añadir la fila al DataGridView, incluyendo la imagen
                    dgv.Rows.Add(bar, ubicacionImage, cuts, residue.ToString("0.000", CultureInfo.InvariantCulture) + " m");
                }

                // Añadir una fila en blanco al final con la imagen por defecto
                dgv.Rows.Add("", defaultImage, "", "");

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


        private List<List<(decimal length, int window, int number)>> OptimizeCuts(decimal[] availableBars, List<(decimal length, int window, int number)> requiredLengthsWithNumbers)
        {
            // Ordenar las longitudes requeridas de mayor a menor
            requiredLengthsWithNumbers = requiredLengthsWithNumbers
                .OrderByDescending(x => x.length)
                .ToList();

            // Lista para almacenar los cortes óptimos para cada barra
            List<List<(decimal length, int window, int number)>> optimizedCuts = new List<List<(decimal length, int window, int number)>>();

            foreach (decimal bar in availableBars)
            {
                List<(decimal length, int window, int number)> cuts = new List<(decimal length, int window, int number)>();
                decimal remainingLength = bar;

                foreach (var lengthWithNumber in requiredLengthsWithNumbers.ToList())
                {
                    if (remainingLength >= lengthWithNumber.length)
                    {
                        cuts.Add(lengthWithNumber);
                        remainingLength -= lengthWithNumber.length;
                        requiredLengthsWithNumbers = requiredLengthsWithNumbers
                            .Where(val => val.number != lengthWithNumber.number)
                            .ToList();
                    }
                }

                optimizedCuts.Add(cuts);
            }

            // Agregar cualquier corte que no pudo ser optimizado
            if (requiredLengthsWithNumbers.Count > 0)
            {
                optimizedCuts.Add(new List<(decimal length, int window, int number)>(requiredLengthsWithNumbers));
            }

            return optimizedCuts;
        }

    }
}
