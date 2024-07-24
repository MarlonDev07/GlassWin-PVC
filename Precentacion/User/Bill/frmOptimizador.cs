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
        private Image defaultImage;
        private Image specificImage;

        public frmOptimizador(decimal[] requiredLengths, decimal[] availableBars)
        {
            InitializeComponent();
            this.requiredLengths = requiredLengths;
            this.availableBars = availableBars;

            // Configurar el DataGridView al inicializar el formulario
            ConfigureDataGridView();

            // Cargar las imágenes
            string ruta = Path.GetDirectoryName(Application.ExecutablePath);
            string defaultUrl = "\\Images\\SelectionDesigns\\corte45.jpeg";
            string specificUrl = "\\Images\\SelectionDesigns\\corte90.jpeg";
            string rutaDefaultImage = ruta + defaultUrl;
            string rutaSpecificImage = ruta + specificUrl;
            defaultImage = Image.FromFile(rutaDefaultImage);
            specificImage = Image.FromFile(rutaSpecificImage);

            // Ejecutar la optimización
            OptimizeCutsAndDisplayResults();
        }

        private void ConfigureDataGridView()
        {
            // Definir columnas para el DataGridView
            dgvResults1.Columns.Add("colBar", "Barra 6.40");

            // Crear y añadir una columna de imagen con imagen por defecto
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn
            {
                Name = "colUbicacion",
                HeaderText = "Corte",
                ImageLayout = DataGridViewImageCellLayout.Zoom
            };
            imageColumn.DefaultCellStyle.NullValue = defaultImage; // Establecer la imagen por defecto
            dgvResults1.Columns.Add(imageColumn);

            dgvResults1.Columns.Add("colCuts", "Dimensiones");
            dgvResults1.Columns.Add("colResiduos", "Retal"); // Nueva columna para residuos

            // Ajustar el tamaño de las columnas
            AdjustColumnWidthsToFitContent();
        }

        private void AdjustColumnWidthsToFitContent()
        {
            foreach (DataGridViewColumn column in dgvResults1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void OptimizeCutsAndDisplayResults()
        {
            try
            {
                // Asignar números secuenciales a las longitudes requeridas
                List<(decimal length, int number)> requiredLengthsWithNumbers = requiredLengths
                    .Select((length, index) => (length, number: index + 1))
                    .ToList();

                // Llamar al método OptimizeCuts
                List<List<(decimal length, int number)>> optimizedCuts = OptimizeCuts(availableBars, requiredLengthsWithNumbers);

                // Mostrar los resultados en el DataGridView
                dgvResults1.Rows.Clear();

                for (int i = 0; i < optimizedCuts.Count; i++)
                {
                    string bar = "Barra " + (i + 1);
                    string cuts = string.Join(", ", optimizedCuts[i].Select(c => $"{c.length:0.00} m (V{c.number})"));

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
                    dgvResults1.Rows.Add(bar, ubicacionImage, cuts, residue.ToString("0.00") + " m");
                }

                // Añadir una fila en blanco al final con la imagen por defecto
                dgvResults1.Rows.Add("", defaultImage, "", "");

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





        private List<List<(decimal length, int number)>> OptimizeCuts(decimal[] availableBars, List<(decimal length, int number)> requiredLengthsWithNumbers)
        {
            // Ordenar las longitudes requeridas de mayor a menor
            requiredLengthsWithNumbers = requiredLengthsWithNumbers
                .OrderByDescending(x => x.length)
                .ToList();

            // Lista para almacenar los cortes óptimos para cada barra
            List<List<(decimal length, int number)>> optimizedCuts = new List<List<(decimal length, int number)>>();

            foreach (decimal bar in availableBars)
            {
                List<(decimal length, int number)> cuts = new List<(decimal length, int number)>();
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
                optimizedCuts.Add(new List<(decimal length, int number)>(requiredLengthsWithNumbers));
            }

            return optimizedCuts;
        }

        private void dgvResults1_DataError_1(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Asignar la imagen por defecto a la celda que causa el error
            if (e.Context == DataGridViewDataErrorContexts.Formatting || e.Context == DataGridViewDataErrorContexts.Display)
            {
                if (dgvResults1.Columns[e.ColumnIndex] is DataGridViewImageColumn)
                {
                    dgvResults1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = defaultImage;
                    e.ThrowException = false; // Suprimir la excepción
                }
            }
        }
    }
}
