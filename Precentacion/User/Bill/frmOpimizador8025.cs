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
using Org.BouncyCastle.Asn1.X500;

namespace Precentacion.User.Bill
{
    public partial class frmOpimizador8025 : Form
    {
        // Variables
        N_Quote NQuote = new N_Quote();


        private (decimal length, int window)[] requiredLengthsC8025;
        private decimal[] availableBarsC8025;
        private (decimal length, int window)[] requiredLengthsU8025;
        private decimal[] availableBarsU8025;
        private (decimal length, int window)[] requiredLengthsJ8025;
        private decimal[] availableBarsJ8025;
        private (decimal length, int window)[] requiredLengthsS8025;
        private decimal[] availableBarsS8025;
        private (decimal length, int window)[] requiredLengthsI8025;
        private decimal[] availableBarsI8025;
        private (decimal length, int window)[] requiredLengthsV8025;
        private decimal[] availableBarsV8025;
        private (decimal length, int window)[] requiredLengthsVC8025;
        private decimal[] availableBarsVC8025;
        private (decimal length, int window)[] requiredLengthsPA8025;
        private decimal[] availableBarsPA8025;
        private Image defaultImage;
        private Image specificImage;
        string orden;
        string proyecto;
        public frmOpimizador8025(
            (decimal length, int window)[] requiredLengthsC8025, decimal[] availableBarsC8025,
            (decimal length, int window)[] requiredLengthsU8025, decimal[] availableBarsU8025,
            (decimal length, int window)[] requiredLengthsJ8025, decimal[] availableBarsJ8025,
            (decimal length, int window)[] requiredLengthsS8025, decimal[] availableBarsS8025,
            (decimal length, int window)[] requiredLengthsI8025, decimal[] availableBarsI8025,
            (decimal length, int window)[] requiredLengthsV8025, decimal[] availableBarsV8025,
            (decimal length, int window)[] requiredLengthsVC8025, decimal[] availableBarsVC8025,
            (decimal length, int window)[] requiredLengthsPA8025, decimal[] availableBarsPA8025,
            string orden, string proyecto
            )
        
        {
                InitializeComponent();
                this.orden = orden;
                this.proyecto = proyecto;

              
                // Cargador 8025
                this.requiredLengthsC8025 = requiredLengthsC8025;
                this.availableBarsC8025 = availableBarsC8025;
                // Umbral 8025
                this.requiredLengthsU8025 = requiredLengthsU8025;
                this.availableBarsU8025 = availableBarsU8025;
                // Jamba 8025
                this.requiredLengthsJ8025 = requiredLengthsJ8025;
                this.availableBarsJ8025 = availableBarsJ8025;
                // Superior 8025
                this.requiredLengthsS8025 = requiredLengthsS8025;
                this.availableBarsS8025 = availableBarsS8025;
                // Inferior 8025
                this.requiredLengthsI8025 = requiredLengthsI8025;
                this.availableBarsI8025 = availableBarsI8025;
                // Vertical 8025
                this.requiredLengthsV8025 = requiredLengthsV8025;
                this.availableBarsV8025 = availableBarsV8025;
                // Vertical Centro 8025
                this.requiredLengthsVC8025 = requiredLengthsVC8025;
                this.availableBarsVC8025 = availableBarsVC8025;
                // Pisa Alfombra 8025
                this.requiredLengthsPA8025 = requiredLengthsPA8025;
                this.availableBarsPA8025 = availableBarsPA8025;


           

                // Configurar los DataGridView al inicializar el formulario
                ConfigureDataGridView(dgvCargador8025, defaultImage);
                ConfigureDataGridView(dgvUmbral8025, defaultImage);
                ConfigureDataGridView2(dgvJamba8025, defaultImage);
                ConfigureDataGridView(dgvSuperior8025, defaultImage);
                ConfigureDataGridView(dgvInferior8025, defaultImage);
                ConfigureDataGridView2(dgvVertical8025, defaultImage);
                ConfigureDataGridView2(dgvVerticalC8025, defaultImage);
                ConfigureDataGridView(dgvPisaAl8025, defaultImage);

                // Cargar las imágenes
                string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                string defaultUrl = "Images\\SelectionDesigns\\corte45.jpeg";
                string specificUrl = "Images\\SelectionDesigns\\corte90.jpeg";
                string rutaDefaultImage = Path.Combine(ruta, defaultUrl);
                string rutaSpecificImage = Path.Combine(ruta, specificUrl);
                defaultImage = Image.FromFile(rutaDefaultImage);
                specificImage = Image.FromFile(rutaSpecificImage);

             

                // Ejecutar la optimización
                OptimizeCutsAndDisplayResults(dgvCargador8025, requiredLengthsC8025, availableBarsC8025);
                OptimizeCutsAndDisplayResults(dgvUmbral8025, requiredLengthsU8025, availableBarsU8025);
                OptimizeCutsAndDisplayResults(dgvJamba8025, requiredLengthsJ8025, availableBarsJ8025);
                OptimizeCutsAndDisplayResults(dgvSuperior8025, requiredLengthsS8025, availableBarsS8025);
                OptimizeCutsAndDisplayResults(dgvInferior8025, requiredLengthsI8025, availableBarsI8025);
                OptimizeCutsAndDisplayResults(dgvVertical8025, requiredLengthsV8025, availableBarsV8025);
                OptimizeCutsAndDisplayResults(dgvVerticalC8025, requiredLengthsVC8025, availableBarsVC8025);
                OptimizeCutsAndDisplayResults(dgvPisaAl8025, requiredLengthsPA8025, availableBarsPA8025);
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
        private void ConfigureDataGridView2(DataGridView dgv, Image defaultImage)
        {
            // Definir columnas para el DataGridView
            dgv.Columns.Clear();
            dgv.Columns.Add("colBar", "Barra 4.60");

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




        private void ExportDataGridViewsToPdf(string filename)
        {
            Document pdfDoc = new Document(PageSize.A4.Rotate());
            try
            {
                string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), filename);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(folderPath, FileMode.Create));
                pdfDoc.Open();

                // Info general
                AddGeneralInfoToPdf(pdfDoc, orden, proyecto);

               

                // Título 2
                AddTitleToPdf(pdfDoc, "Optimización 8025");
                AddDataGridViewToPdf(pdfDoc, dgvCargador8025, "016 Cargador 8025", "cargador.jpeg");
                AddDataGridViewToPdf(pdfDoc, dgvUmbral8025, "017 Umbral 8025", "umbral.jpeg");
                AddDataGridViewToPdf(pdfDoc, dgvJamba8025, "018 Jamba 8025", "jamba.jpeg");
                AddDataGridViewToPdf(pdfDoc, dgvSuperior8025, "022 Superior 8025", "superior2.jpeg");
                AddDataGridViewToPdf(pdfDoc, dgvInferior8025, "023 Inferior 8025", "inferior2.jpeg");
                AddDataGridViewToPdf(pdfDoc, dgvVertical8025, "024 Vertical 8025", "vertical.jpeg");
                AddDataGridViewToPdf(pdfDoc, dgvVerticalC8025, "025 Vertical Centro 8025", "verticalC.jpeg");
                AddDataGridViewToPdf(pdfDoc, dgvPisaAl8025, "027 Pisa Alfombra 8025", "verticalC.jpeg");
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

        // Método para agregar la información general al PDF
        private void AddGeneralInfoToPdf(Document pdfDoc, string orden, string proyecto)
        {
            // Configura el estilo de fuente y tamaño
            iTextSharp.text.Font font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);

            // Añadir N° Orden
            pdfDoc.Add(new Paragraph($"N° Orden: {orden}", font));

            // Añadir Proyecto
            pdfDoc.Add(new Paragraph($"Proyecto: {proyecto}", font));

            // Añadir un espacio entre las secciones
            pdfDoc.Add(new Paragraph("\n"));
        }


        private void AddTitleToPdf(Document pdfDoc, string title)
        {
            iTextSharp.text.Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            Paragraph titleParagraph = new Paragraph(title, titleFont)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 10f
            };
            pdfDoc.Add(titleParagraph);
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

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            // Llama a la función para imprimir todos los DataGridViews en un único PDF
            ExportDataGridViewsToPdf($"8025 - Resultados Optimizacion {orden}.pdf");
            MessageBox.Show("PDF generado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
