
using Negocio.Company.Employer;
using Precentacion.User.Quote.Quote;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Borders;
using iText.Kernel.Colors;
using iText.IO.Image;
using System.IO;
using System.Collections.Generic;


namespace Precentacion.User.Quote.Accesorios
{
    public partial class frmTablaViaticos : MaterialSkin.Controls.MaterialForm
    {
        #region Variables
        private decimal TotalGasolina = 0;
        private decimal TotalComida = 0;
        private decimal TotalHospedaje = 0;
        private decimal TotalSalarios = 0;
        decimal SubTotal;
        bool Sumado = false;
        #endregion

        #region Constructor
        public frmTablaViaticos()
        {
            InitializeComponent();
            CargarEmpleadosCmb();
            AccesoriosUI.loadMaterial(this);
            // Configurar el formulario para que se abra en el centro de la pantalla
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        #endregion

        #region Gasolina
        private void txtGasolina_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtDistancia.Text != "")
                {
                    if (txtPrecioxKM.Text != "")
                    {
                        Decimal Distancia = Convert.ToDecimal(txtDistancia.Text);
                        Decimal PrecioxKM = Convert.ToDecimal(txtPrecioxKM.Text);
                        int Cantidad = Convert.ToInt32(numericVeiculos.Text);
                        TotalGasolina = (Distancia * PrecioxKM) * Cantidad;
                        txtTotalGasolina.Text = TotalGasolina.ToString("c");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al ingresar un valor Utilizar solo Numeros", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAbrirMaps_Click(object sender, EventArgs e)
        {
            //Abrir el navegador con la direccion
            System.Diagnostics.Process.Start("https://www.google.com/maps");
        }
        #endregion

        #region Comida
        private void txtComida_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtDesayuno.Text != "")
                {
                    if (txtAlmuerzo.Text != "")
                    {
                        if (txtCena.Text != "")
                        {
                            Decimal Desayuno = Convert.ToDecimal(txtDesayuno.Text);
                            Decimal Almuerzo = Convert.ToDecimal(txtAlmuerzo.Text);
                            Decimal Cena = Convert.ToDecimal(txtCena.Text);

                            TotalComida = ((Desayuno + Almuerzo + Cena) * Convert.ToInt32(NumericDias.Value)) * Convert.ToInt32(NumericEmpleados.Value);
                            txtTotalComida.Text = TotalComida.ToString("c");
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al ingresar un valor Utilizar solo Numeros", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Hospedaje
        private void txtHospedaje_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPrecioHabitacion.Text != "")
                {
                    decimal PrecioxNoche = Convert.ToDecimal(txtPrecioHabitacion.Text);
                    TotalHospedaje = (PrecioxNoche * Convert.ToInt32(NumericHabitaciones.Value)) * Convert.ToInt32(NumericNoches.Value);
                    txtTotalHospedaje.Text = TotalHospedaje.ToString("c");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al ingresar un valor Utilizar solo Numeros", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnHospedaje_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.bing.com/travel/hotel-search?q=buscar+Cabinas+costarrica&cin=2024-05-05&cout=2024-05-06&guests=2A&rooms=1&sort=Popularity&type=hotel&mapBounds=10%2C866196%2C-85%2C27943%2C9%2C274269%2C-83%2C94219&cacheId=undefined__4b4044eb-8775-4b61-8ddd-dd14781af7e8__2__cabinas&form=HTSEHL&entrypoint=HTSEHL");
        }
        #endregion

        #region Salarios
        private void CargarEmpleadosCmb()
        {
            try
            {
                //Cargar Empleados
                N_Employer Employer = new N_Employer();
                DataTable dataTable = new DataTable();
                dataTable = Employer.LoadEmployer();
                //Juntar dos columnas en una separadas por un - la primera columna es el FirstName y la segunda el PaymentHours
                dataTable.Columns.Add("NameSalary", typeof(string), "FirstName+ '-' +PaymentHours");

                //Asignar la columna NameSalary al ComboBox
                cmbEmpleados.DataSource = dataTable;
                cmbEmpleados.DisplayMember = "NameSalary";




            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar los Empleados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void ObtenerSalario_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtHoras.Text == "")
                {
                    MessageBox.Show("Debe ingresar la cantidad de horas", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else {
                    // Obtener los datos del empleado seleccionado
                    string[] salarioParts = cmbEmpleados.Text.Split('-');
                    string nombreEmpleado = salarioParts[0].Trim();
                    decimal salario = Convert.ToDecimal(salarioParts[1].Trim());
                    int horas = Convert.ToInt32(txtHoras.Text);

                    // Calcular el total
                    decimal total = salario * horas;
                    textBox2.Text = total.ToString("c");

                    // Acumular los salarios
                    decimal salarioAcumulado = 0;
                    foreach (var empleado in empleadosSalarios)
                    {
                        salarioAcumulado += empleado.Salario * empleado.Horas;
                    }
                    salarioAcumulado += total; // Añadir el salario actual
                    txtSalarios.Text = salarioAcumulado.ToString();

                    // Agregar o actualizar el empleado en la lista
                    var empleadoExistente = empleadosSalarios.FirstOrDefault(empleado => empleado.Nombre == nombreEmpleado);

                    if (empleadoExistente != null)
                    {
                        // Actualizar los datos del empleado existente
                        empleadoExistente.Salario = salario;
                        empleadoExistente.Horas = horas;
                        empleadoExistente.Total = total;
                    }
                    else
                    {
                        // Agregar un nuevo empleado
                        empleadosSalarios.Add(new EmpleadoSalario
                        {
                            Nombre = nombreEmpleado,
                            Salario = salario,
                            Horas = horas,
                            Total = total
                        });
                    }

                    // Actualizar el total acumulado
                    decimal totalSalarios = empleadosSalarios.Sum(emp => emp.Total);
                    textBox2.Text = totalSalarios.ToString("c");
                }
                
            }
            catch (Exception)
            {
                MessageBox.Show("Error al obtener el Salario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        /*private void ObtenerSalario_Click(object sender, EventArgs e)
        {
            try
            {
                //Obtener Salario
                decimal SalariosAcumulados = 0;
                string[] Salario = cmbEmpleados.Text.Split('-');
                if (txtSalarios.Text != "")
                {
                    SalariosAcumulados = Convert.ToDecimal(txtSalarios.Text);
                }
                SalariosAcumulados += Convert.ToDecimal(Salario[1]);
                txtSalarios.Text = SalariosAcumulados.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Error al obtener el Salario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }*/


        private void txtSalarios_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSalarios.Text != "")
                {
                    if (txtHoras.Text != "")
                    {
                        decimal Salarios = Convert.ToDecimal(txtSalarios.Text);

                        TotalSalarios = Salarios * Convert.ToInt32(txtHoras.Text);
                        textBox2.Text = TotalSalarios.ToString("c");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al ingresar un valor Utilizar solo Numeros", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region CalcularTotal
        private void CalcularTotal_TextChange(object sender, EventArgs e)
        {

            decimal Total = TotalGasolina + TotalComida + TotalHospedaje + TotalSalarios;
            txtTotalViaticos.Text = Total.ToString();
        }
        #endregion

        private void txtTotalViaticos_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(txtTotalViaticos.Text, "[^0-9.,]"))
                {
                    MessageBox.Show("Por favor ingrese solo numeros", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTotalViaticos.Text = txtTotalViaticos.Text.Remove(txtTotalViaticos.Text.Length - 1);
                    txtTotalViaticos.Select(txtTotalViaticos.Text.Length, 0);




                }
                else
                {
                    if (txtTotalViaticos.Text.Contains("."))
                    {
                        //Reemplazar el punto por una coma
                        txtTotalViaticos.Text = txtTotalViaticos.Text.Replace(".", ",");
                    }
                    if (txtUtilidad.Text.Contains("."))
                    {
                        //Reemplazar el punto por una coma
                        txtUtilidad.Text = txtUtilidad.Text.Replace(".", ",");
                    }

                    if (txtTotalViaticos.Text != "")
                    {
                        if (txtUtilidad.Text != "")
                        {
                            decimal TotalViaticos = Convert.ToDecimal(txtTotalViaticos.Text);
                            decimal Utilidad = Convert.ToDecimal(txtUtilidad.Text);
                            decimal CostoTotal = TotalViaticos + Utilidad;
                            decimal Porcentaje = ((Utilidad + TotalViaticos) / SubTotal) * 100;
                            txtPorcentaje.Text = Porcentaje.ToString("0.00");
                        }
                        else
                        {
                            txtPorcentaje.Text = "0";
                        }
                    }
                    else
                    {
                        txtPorcentaje.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        public void CargarSubTotal(decimal SubProforma)
        {
            SubTotal = SubProforma;
            txtSubTotalProforma.Text = SubTotal.ToString();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            //Enviar los datos a la Proforma
            Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is frmQuote);
            if (frm != null)
            {
                ((frmQuote)frm).txtManoObra.Text = txtPorcentaje.Text;
                ((frmQuote)frm).btnApply_Click(sender, e);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            saveFileDialog.Title = "Guardar PDF como";
            saveFileDialog.FileName = "Viaticos"; // Nombre predeterminado

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    using (var writer = new PdfWriter(filePath))
                    {
                        using (var pdf = new PdfDocument(writer))
                        {
                            using (var document = new Document(pdf))
                            {
                                // Agregar encabezado con imagen y título
                                AddHeaderToPdf(document);

                                // Agregar información de Gasolina
                                AddSectionToPdf(document, "Gasolina",
                                    new[] { "Vehículos:", "Distancia:", "Precio por KM:", "Total Gasolina:" },
                                    new[] { numericVeiculos.Value.ToString(), txtDistancia.Text, txtPrecioxKM.Text, txtTotalGasolina.Text });

                                // Agregar información de Comida
                                AddSectionToPdf(document, "Comida",
                                    new[] { "Empleados:", "Desayuno:", "Almuerzo:", "Cena:", "Días:", "Total Comida:" },
                                    new[] { NumericEmpleados.Value.ToString(), txtDesayuno.Text, txtAlmuerzo.Text, txtCena.Text, NumericDias.Value.ToString(), txtTotalComida.Text });

                                // Agregar información de Hospedaje
                                AddSectionToPdf(document, "Hospedaje",
                                    new[] { "Habitaciones:", "Precio Habitación:", "Noches:", "Total Hospedaje:" },
                                    new[] { NumericHabitaciones.Value.ToString(), txtPrecioHabitacion.Text, NumericNoches.Value.ToString(), txtTotalHospedaje.Text });

                                // Agregar información de Salarios
                                AddSectionToPdf(document, "Salarios", empleadosSalarios);

                                // Agregar información de Total Viáticos
                                AddSectionToPdf(document, "Total Viáticos",
                                    new[] { "SubTotal Proforma:", "Total Viáticos:", "Utilidad:", "Porcentaje:" },
                                    new[] { txtSubTotalProforma.Text, txtTotalViaticos.Text, txtUtilidad.Text, txtPorcentaje.Text });

                                // Finalizar el documento
                                document.Close();
                            }
                        }
                    }

                    MessageBox.Show("El PDF se ha guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al generar el PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void AddHeaderToPdf(Document document)
        {
            // Crear una tabla con dos columnas para el encabezado
            Table headerTable = new Table(UnitValue.CreatePercentArray(new float[] { 1, 4 })).UseAllAvailableWidth();
            headerTable.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.LEFT);
            headerTable.SetMarginBottom(20); // Separación del contenido

            // Configurar la imagen del logo
            string rutaLogo = GetCompanyLogoPath();
            ImageData imageData = ImageDataFactory.Create(rutaLogo);
            iText.Layout.Element.Image pdfImage = new iText.Layout.Element.Image(imageData)
                .SetAutoScale(true)
                .SetHeight(60f);

            // Agregar la imagen a la primera celda
            Cell imageCell = new Cell().Add(pdfImage).SetBorder(Border.NO_BORDER);
            headerTable.AddCell(imageCell);

            // Agregar el título del PDF a la segunda celda
            Cell titleCell = new Cell().Add(new Paragraph("Formulario de Viáticos")
                .SetBold()
                .SetFontSize(18)
                .SetTextAlignment(TextAlignment.CENTER))
                .SetBorder(Border.NO_BORDER);
            headerTable.AddCell(titleCell);

            // Agregar la tabla del encabezado al documento
            document.Add(headerTable);
        }


        private string GetCompanyLogoPath()
        {
            string rutaLogo = "";
            switch (CompanyCache.IdCompany)
            {
                case 999999999:
                    rutaLogo = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Images", "Logos", "UsuarioPrueba.png");
                    break;
                case 205520679:
                    rutaLogo = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Images", "Logos", "VidriosMartinez.png");
                    break;
                case 111111111:
                    rutaLogo = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Images", "Logos", "Prefalum2.png");
                    break;
                case 3102879949:
                    rutaLogo = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Images", "Logos", "mercadoVidrio.png");
                    break;
                case 222222222:
                    rutaLogo = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Images", "Logos", "VidrieraPalmares.png");
                    break;
                case 333333333:
                    rutaLogo = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Images", "Logos", "PerfectGlass.png");
                    break;
                case 31025820:
                    rutaLogo = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Images", "Logos", "AluviLogo.png");
                    break;
                case 3101794685:
                    rutaLogo = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Images", "Logos", "RioClaroLogo.png");
                    break;
                case 205150849:
                    rutaLogo = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Images", "Logos", "MakyLogo.png");
                    break;
                case 112540885:
                    rutaLogo = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Images", "Logos", "VidriosAlturaLogo.png");
                    break;
                case 1230123:
                    rutaLogo = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Images", "Logos", "GlassWinLogo.png");
                    break;
                case 25550555:
                    rutaLogo = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Images", "Logos", "VitroLogo.png");
                    break;
                case 31028013:
                    rutaLogo = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Images", "Logos", "InnovaLogo.png");
                    break;
                case 111560456:
                    rutaLogo = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Images", "Logos", "DialexLogo.png");
                    break;
                case 310108681:
                    rutaLogo = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Images", "Logos", "VidrioCentroLogo.png");
                    break;
                case 310171783:
                    rutaLogo = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Images", "Logos", "VidriosVegaLogo.png");
                    break;
            }
            return rutaLogo;
        }

        private void AddSectionToPdf(Document document, string sectionTitle, string[] headers, string[] values)
        {
            // Agregar título de sección centrado
            document.Add(new Paragraph(sectionTitle)
                .SetBold()
                .SetFontSize(16)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetMarginBottom(10));

            // Crear una tabla con el mismo número de columnas que los encabezados
            Table table = new Table(UnitValue.CreatePercentArray(headers.Length)).UseAllAvailableWidth();
            table.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.LEFT);

            // Agregar encabezados de columna con colores
            foreach (var header in headers)
            {
                Cell headerCell = new Cell()
                    .Add(new Paragraph(header))
                    .SetBorder(new SolidBorder(1f)) // Grosor del borde de 1 punto
                    .SetBorderBottom(new SolidBorder(1f))
                    .SetBackgroundColor(new DeviceRgb(255, 165, 0)) // Color naranja
                    .SetTextAlignment(TextAlignment.CENTER);
                table.AddCell(headerCell);
            }

            // Agregar valores en una fila con colores alternos
            foreach (var value in values)
            {
                Cell valueCell = new Cell()
                    .Add(new Paragraph(value))
                    .SetBorder(new SolidBorder(1f)) // Grosor del borde de 1 punto
                    .SetBorderBottom(new SolidBorder(1f))
                    .SetBackgroundColor(new DeviceRgb(255, 255, 255)) // Color blanco
                    .SetTextAlignment(TextAlignment.CENTER);
                table.AddCell(valueCell);
            }

            // Agregar la tabla al documento
            document.Add(table);
        }
        private void AddSectionToPdf(Document document, string sectionTitle, List<EmpleadoSalario> empleados)
        {
            // Agregar título de sección centrado
            document.Add(new Paragraph(sectionTitle)
                .SetBold()
                .SetFontSize(16)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetMarginBottom(10));

            // Crear una tabla con las columnas necesarias
            Table table = new Table(UnitValue.CreatePercentArray(4)).UseAllAvailableWidth();
            table.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.LEFT);

            // Establecer el color de fondo de las celdas de encabezado
            Color headerColor = new DeviceRgb(255, 165, 0); // Naranja
            Color textColor = ColorConstants.WHITE;
            Color rowColor1 = new DeviceRgb(255, 255, 255); // Blanco
            Color rowColor2 = new DeviceRgb(255, 228, 196); // Color para filas alternas (Naranja claro)

            // Agregar encabezados de columna con bordes y color
            string[] headers = { "Empleado", "Salario", "Horas", "Total" };
            foreach (var header in headers)
            {
                table.AddHeaderCell(new Cell()
                    .Add(new Paragraph(header))
                    .SetBorder(new SolidBorder(1f)) // Grosor del borde de 1 punto
                    .SetBorderBottom(new SolidBorder(1f))
                    .SetBackgroundColor(headerColor) // Color naranja
                    .SetFontColor(textColor) // Color blanco
                    .SetTextAlignment(TextAlignment.CENTER));
            }

            // Agregar los datos de los empleados
            bool isWhiteRow = true;
            decimal totalSum = 0; // Variable para acumular la suma total
            foreach (var empleado in empleados)
            {
                // Alternar entre colores de fondo blanco y un tono claro de naranja para las filas
                Color rowColor = isWhiteRow ? rowColor1 : rowColor2;

                table.AddCell(new Cell()
                    .Add(new Paragraph(empleado.Nombre))
                    .SetBorder(new SolidBorder(1f)) // Grosor del borde de 1 punto
                    .SetBackgroundColor(rowColor)
                    .SetTextAlignment(TextAlignment.CENTER));
                table.AddCell(new Cell()
                    .Add(new Paragraph(empleado.Salario.ToString("c")))
                    .SetBorder(new SolidBorder(1f))
                    .SetBackgroundColor(rowColor)
                    .SetTextAlignment(TextAlignment.CENTER));
                table.AddCell(new Cell()
                    .Add(new Paragraph(empleado.Horas.ToString()))
                    .SetBorder(new SolidBorder(1f))
                    .SetBackgroundColor(rowColor)
                    .SetTextAlignment(TextAlignment.CENTER));
                Cell totalCell = new Cell()
                    .Add(new Paragraph(empleado.Total.ToString("c")))
                    .SetBorder(new SolidBorder(1f))
                    .SetBackgroundColor(rowColor)
                    .SetTextAlignment(TextAlignment.CENTER);
                table.AddCell(totalCell);

                totalSum += empleado.Total; // Acumular la suma total

                isWhiteRow = !isWhiteRow; // Alternar color
            }

            // Agregar la fila de suma total
            table.AddCell(new Cell(1, 3) // La fila de suma ocupa 3 columnas
                .Add(new Paragraph("Total"))
                .SetBorder(new SolidBorder(1f))
                .SetBackgroundColor(headerColor)
                .SetFontColor(textColor)
                .SetTextAlignment(TextAlignment.CENTER));
            table.AddCell(new Cell()
                .Add(new Paragraph(totalSum.ToString("c")))
                .SetBorder(new SolidBorder(1f))
                .SetBackgroundColor(headerColor)
                .SetFontColor(textColor)
                .SetTextAlignment(TextAlignment.CENTER));

            // Agregar la tabla al documento
            document.Add(table);
        }




        //Clase para manejar los salarios de los empleados en el pdf
        public class EmpleadoSalario
        {
            public string Nombre { get; set; }
            public decimal Salario { get; set; }
            public int Horas { get; set; }
            public decimal Total { get; set; }
        }
        private List<EmpleadoSalario> empleadosSalarios = new List<EmpleadoSalario>();

    }
}
