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
//using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using iTextSharp.text;
using Image = System.Drawing.Image;


namespace Precentacion.User.Bill
{
    public partial class frmOptimizador : Form
    {
        // Variables
        N_Quote NQuote = new N_Quote();
        private (decimal length, int window)[] requiredLengths;
        private decimal[] availableBars;
        private (decimal length, int window)[] requiredLengthsU;
        private decimal[] availableBarsU;
        private (decimal length, int window)[] requiredLengthsJ;
        private decimal[] availableBarsJ;
        private (decimal length, int window)[] requiredLengthsS;
        private decimal[] availableBarsS;
        private (decimal length, int window)[] requiredLengthsI;
        private decimal[] availableBarsI;
        private (decimal length, int window)[] requiredLengthsV;
        private decimal[] availableBarsV;
        private (decimal length, int window)[] requiredLengthsVC;
        private decimal[] availableBarsVC;
        private Image defaultImage;
        private Image specificImage;

        public frmOptimizador(
            (decimal length, int window)[] requiredLengths, decimal[] availableBars,
            (decimal length, int window)[] requiredLengthsU, decimal[] availableBarsU,
            (decimal length, int window)[] requiredLengthsJ, decimal[] availableBarsJ,
            (decimal length, int window)[] requiredLengthsS, decimal[] availableBarsS,
            (decimal length, int window)[] requiredLengthsI, decimal[] availableBarsI,
            (decimal length, int window)[] requiredLengthsV, decimal[] availableBarsV,
            (decimal length, int window)[] requiredLengthsVC, decimal[] availableBarsVC)
        {
            InitializeComponent();
            // Cargador
            this.requiredLengths = requiredLengths;
            this.availableBars = availableBars;
            // Umbral
            this.requiredLengthsU = requiredLengthsU;
            this.availableBarsU = availableBarsU;
            // Jamba
            this.requiredLengthsJ = requiredLengthsJ;
            this.availableBarsJ = availableBarsJ;
            // Superior
            this.requiredLengthsS = requiredLengthsS;
            this.availableBarsS = availableBarsS;
            // Inferior
            this.requiredLengthsI = requiredLengthsI;
            this.availableBarsI = availableBarsI;
            // Vertical
            this.requiredLengthsV = requiredLengthsV;
            this.availableBarsV = availableBarsV;
            // Vertical Centro
            this.requiredLengthsVC = requiredLengthsVC;
            this.availableBarsVC = availableBarsVC;

            // Configurar los DataGridView al inicializar el formulario
            ConfigureDataGridView(dgvResults1, defaultImage);
            ConfigureDataGridView(dgvResults2, defaultImage);
            ConfigureDataGridView(dgvResults3, defaultImage);
            ConfigureDataGridView(dgvResults4, defaultImage);
            ConfigureDataGridView(dgvResults5, defaultImage);
            ConfigureDataGridView(dgvResults6, defaultImage);
            ConfigureDataGridView(dgvResults7, defaultImage);

            // Cargar las imágenes
            string ruta = Path.GetDirectoryName(Application.ExecutablePath);
            string defaultUrl = "Images\\SelectionDesigns\\corte45.jpeg";
            string specificUrl = "Images\\SelectionDesigns\\corte90.jpeg";
            string rutaDefaultImage = Path.Combine(ruta, defaultUrl);
            string rutaSpecificImage = Path.Combine(ruta, specificUrl);
            defaultImage = Image.FromFile(rutaDefaultImage);
            specificImage = Image.FromFile(rutaSpecificImage);

            // Ejecutar la optimización
            OptimizeCutsAndDisplayResults(dgvResults1, requiredLengths, availableBars);
            OptimizeCutsAndDisplayResults(dgvResults2, requiredLengthsU, availableBarsU);
            OptimizeCutsAndDisplayResults(dgvResults3, requiredLengthsJ, availableBarsJ);
            OptimizeCutsAndDisplayResults(dgvResults4, requiredLengthsS, availableBarsS);
            OptimizeCutsAndDisplayResults(dgvResults5, requiredLengthsI, availableBarsI);
            OptimizeCutsAndDisplayResults(dgvResults6, requiredLengthsV, availableBarsV);
            OptimizeCutsAndDisplayResults(dgvResults7, requiredLengthsVC, availableBarsVC);
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
                //dgv.Rows.Add("", defaultImage, "", "");

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

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            // Llama a la función para imprimir todos los DataGridViews en un único PDF
            ExportDataGridViewsToPdf("Resultados_Optimizacion.pdf");
            MessageBox.Show("PDF generado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ExportDataGridViewsToPdf(string filename)
        {
            Document pdfDoc = new Document(PageSize.A4.Rotate());
            try
            {
                string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), filename);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(folderPath, FileMode.Create));
                pdfDoc.Open();

                AddDataGridViewToPdf(pdfDoc, dgvResults1, "003 Cargador 5020", "cargador.jpeg");
                AddDataGridViewToPdf(pdfDoc, dgvResults2, "002 Umbral 5020", "umbral.jpeg");
                AddDataGridViewToPdf(pdfDoc, dgvResults3, "004 Jamba 5020", "jamba.jpeg");
                AddDataGridViewToPdf(pdfDoc, dgvResults4, "006 Superior 5020", "superior2.jpeg");
                AddDataGridViewToPdf(pdfDoc, dgvResults5, "005 Inferior 5020", "inferior2.jpeg");
                AddDataGridViewToPdf(pdfDoc, dgvResults6, "007 Vertical 5020", "vertical.jpeg");
                AddDataGridViewToPdf(pdfDoc, dgvResults7, "008 Vertical Centro 5020", "verticalC.jpeg");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                pdfDoc.Close();
            }
        }



        private void AddDataGridViewToPdf(Document pdfDoc, DataGridView dgv, string title, string imageName)
        {
            // Ruta de la imagen
            string ruta = Path.GetDirectoryName(Application.ExecutablePath);
            string url = $"\\Images\\Optimizador\\{imageName}";
            string rutaImagen = ruta + url;

            // Crear una tabla PDF con una celda para la imagen y otra para el título
            PdfPTable titleTable = new PdfPTable(2);
            titleTable.WidthPercentage = 100;
            titleTable.SetWidths(new float[] { 1f, 8f });

            // Agregar la imagen a la celda
            iTextSharp.text.Image titleImage = iTextSharp.text.Image.GetInstance(rutaImagen);
            PdfPCell imageTitleCell = new PdfPCell(titleImage, true)
            {
                Border = iTextSharp.text.Rectangle.NO_BORDER,
                Padding = 5
            };
            titleTable.AddCell(imageTitleCell);

            // Agregar el título de la sección a la celda
            PdfPCell titleTextCell = new PdfPCell(new Phrase(title, FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD)))
            {
                Border = iTextSharp.text.Rectangle.NO_BORDER,
                Padding = 5,
                HorizontalAlignment = Element.ALIGN_LEFT
            };
            titleTable.AddCell(titleTextCell);

            // Agregar la tabla de título con imagen al documento
            pdfDoc.Add(titleTable);
            pdfDoc.Add(new Paragraph("\n"));

            // Crear una tabla PDF con el mismo número de columnas que el DataGridView
            PdfPTable dataGridTable = new PdfPTable(dgv.ColumnCount);
            dataGridTable.WidthPercentage = 100;

            // Ajustar los anchos de las columnas (ej. 20%, 20%, 30%, 30%)
            float[] columnWidths = { 1f, 1f, 8f, 1f };
            dataGridTable.SetWidths(columnWidths);

            // Añadir las cabeceras de columna
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                PdfPCell headerCell = new PdfPCell(new Phrase(column.HeaderText))
                {
                    BackgroundColor = new BaseColor(240, 240, 240)
                };
                dataGridTable.AddCell(headerCell);
            }

            // Añadir las filas de datos
            foreach (DataGridViewRow row in dgv.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null)
                    {
                        if (cell.ValueType == typeof(System.Drawing.Image))
                        {
                            // Agregar imagen a la celda si el valor es una imagen
                            System.Drawing.Image cellImage = (System.Drawing.Image)cell.Value;
                            iTextSharp.text.Image pdfCellImage = iTextSharp.text.Image.GetInstance(cellImage, System.Drawing.Imaging.ImageFormat.Png);
                            PdfPCell cellImageCell = new PdfPCell(pdfCellImage, true);
                            dataGridTable.AddCell(cellImageCell);
                        }
                        else
                        {
                            dataGridTable.AddCell(new Phrase(cell.Value.ToString()));
                        }
                    }
                }
            }

            pdfDoc.Add(dataGridTable);
            pdfDoc.Add(new Paragraph("\n")); // Agregar un espacio entre tablas
        }

        private void btnImprimir_Click_1(object sender, EventArgs e)
        {
            // Llama a la función para imprimir todos los DataGridViews en un único PDF
            ExportDataGridViewsToPdf("Resultados_Optimizacion.pdf");
            MessageBox.Show("PDF generado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
