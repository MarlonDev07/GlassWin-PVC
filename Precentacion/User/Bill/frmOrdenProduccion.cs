
﻿using Negocio.Company.OrdenProduccion;
﻿using MaterialSkin.Controls;
using Negocio.Company.Quote;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using Dominio.Model.ClassWindows;
using Precentacion.User.DashBoard;


namespace Precentacion.User.Bill
{
    public partial class frmOrdenProduccion : MaterialForm //MaterialFoarm
    {
        N_Quote NQuote = new N_Quote();
        N_OrdenProduccion NOrden = new N_OrdenProduccion();
        bool CargaCompleta = false;
        List<string> Piezas5020 = new List<string> { "Cargador", "Umbral", "Jamba", "Superior", "Inferior", "Vertical", "Vertical Centro" };
        List<string> Piezas8025 = new List<string> { "Cargador", "Umbral", "Jamba", "Superior", "Inferior", "Vertical", "Vertical Centro", "PisaAlfombra"};
        List<decimal> ResultadosRebajo = new List<decimal>();
        List<decimal> ResultadosCantidad = new List<decimal>();
        public frmOrdenProduccion()
        {
            InitializeComponent();
            BillUI.loadMaterial(this);
            // Formato a las fechas
            dtpFechaInicio.Format = DateTimePickerFormat.Custom;
            dtpFechaInicio.CustomFormat = "dddd, dd MMMM yyyy - hh:mm tt";
            dtpFechaSalida.Format = DateTimePickerFormat.Custom;
            dtpFechaSalida.CustomFormat = "dddd, dd MMMM yyyy - hh:mm tt";


        }
        private void LoadProjects()
        {
            try
            {
                DataTable projects = NQuote.GetProjectsByCompanyId();

                if (projects != null)
                {
                    cbProyecto.DataSource = projects;
                    cbProyecto.DisplayMember = "ProjectName";
                    cbProyecto.ValueMember = "IdQuote";
                    CargaCompleta = true;
                    cbProyecto.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("No se encontraron proyectos para la compañía seleccionada.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al cargar los proyectos: " + ex.Message);
            }
        }

        private void LoadWindowsData()
        {
            try
            {
                //Obtener el Id del proyecto seleccionado
                int IdQuote = Convert.ToInt32(cbProyecto.SelectedValue);
                //Obtener las ventanas del proyecto
                DataTable windows = NQuote.WindowsData(IdQuote);
                //Validar si se encontraron ventanas
                if (windows.Rows.Count != 0)
                {
                    // Recorrer las ventanas
                    foreach (DataRow row in windows.Rows)
                    {
                        #region 5020
                        if (row["System"].ToString() == "5020")
                        {
                            // Obtener la ubicación de la ventana, el dato se encuentra en la descripción
                            string DescripcionUbicacion = row["Description"].ToString();
                            string pattern = @"\nUbicacion:\s*(.*)";
                            System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(DescripcionUbicacion, pattern);
                            string Ubicacion = match.Groups[1].Value;
                            //Agregar los Valores a Variables
                            string Sistema = row["System"].ToString();
                            string Diseño = row["Design"].ToString();
                            string Ancho = row["Width"].ToString();
                            string Alto = row["Height"].ToString();

                            //Recorrer las piezas del sistema 5020
                            foreach (string pieza in Piezas5020)
                            {
                                //Calcular los rebajos y Agregarlos A la Lista de Resultados
                                ResultadosRebajo.Add(NOrden.CalculoRebajos5020(Diseño, Ancho, Alto, pieza));
                                //Calcular la Cantidad de Piezas y Agregarlos A la Lista de Resultados
                                ResultadosCantidad.Add(NOrden.CalculoCantidadPiezas(Diseño, pieza));
                            }

                            //Agregar los Resultados al DataGridView
                            dgvOrdenProduccion.Rows.Add(Sistema, Ubicacion, Diseño, Ancho, Alto,
                                ResultadosRebajo[0], ResultadosCantidad[0],
                                ResultadosRebajo[1], ResultadosCantidad[1],
                                ResultadosRebajo[2], ResultadosCantidad[2],
                                ResultadosRebajo[3], ResultadosCantidad[3],
                                ResultadosRebajo[4], ResultadosCantidad[4],
                                ResultadosRebajo[5], ResultadosCantidad[5],
                                ResultadosRebajo[6], ResultadosCantidad[6]);

                            //Limpiar las Listas de Resultados
                            ResultadosRebajo.Clear();
                            ResultadosCantidad.Clear();

                        }
                        #endregion

                        #region 8025
                        if (row["System"].ToString() == "8025 2 Vias" || row["System"].ToString() == "8025 3 Vias")
                        {
                            // Obtener la ubicación de la ventana, el dato se encuentra en la descripción
                            string DescripcionUbicacion = row["Description"].ToString();
                            string pattern = @"\nUbicacion:\s*(.*)";
                            System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(DescripcionUbicacion, pattern);
                            string Ubicacion = match.Groups[1].Value;
                            //Agregar los Valores a Variables
                            string Sistema = row["System"].ToString();
                            string Diseño = row["Design"].ToString();
                            string Ancho = row["Width"].ToString();
                            string Alto = row["Height"].ToString();

                            //Recorrer las piezas del sistema 5020
                            foreach (string pieza in Piezas8025)
                            {
                                //Calcular los rebajos y Agregarlos A la Lista de Resultados
                                ResultadosRebajo.Add(NOrden.CalculoRebajos8025(Diseño, Ancho, Alto, pieza));
                                //Calcular la Cantidad de Piezas y Agregarlos A la Lista de Resultados
                                ResultadosCantidad.Add(NOrden.CalculoCantidadPiezas8025(Diseño, pieza));
                            }

                            //Agregar los Resultados al DataGridView
                            dgvOrdenProduccion8025.Rows.Add(Sistema, Ubicacion, Diseño, Ancho, Alto,
                                ResultadosRebajo[0], ResultadosCantidad[0],
                                ResultadosRebajo[1], ResultadosCantidad[1],
                                ResultadosRebajo[2], ResultadosCantidad[2],
                                ResultadosRebajo[3], ResultadosCantidad[3],
                                ResultadosRebajo[4], ResultadosCantidad[4],
                                ResultadosRebajo[5], ResultadosCantidad[5],
                                ResultadosRebajo[6], ResultadosCantidad[6],
                                ResultadosRebajo[7], ResultadosCantidad[7]
                                );

                            //Limpiar las Listas de Resultados
                            ResultadosRebajo.Clear();
                            ResultadosCantidad.Clear();

                        }
                        #endregion
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron ventanas para el proyecto seleccionado.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al cargar las ventanas: " + ex.Message);
            }
        }

        private void frmOrdenProduccion_Load(object sender, EventArgs e)
        {
            LoadProjects();
        }

        private void cbProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CargaCompleta)
            {
                try
                {
                    dgvOrdenProduccion.Rows.Clear();
                    ResultadosRebajo.Clear();
                    ResultadosCantidad.Clear();
                    LoadWindowsData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al cargar las ventanas: " + ex.Message);
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            GenerarPdfHojaProduccion();
        }


        private void GenerarPdfHojaProduccion()
        {
            try
            {
                if (dgvOrdenProduccion.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos para generar la hoja de producción.", "Hoja de Producción", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                #region Crear el documento
                // Obtener el directorio del escritorio y las carpetas necesarias
                string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string CarpetaFactura = Path.Combine(escritorio, "Orden Produccion");
                string carpetaNombre = Path.Combine(CarpetaFactura, cbProyecto.Text.Trim());
                // Obtener el Id de la Ventana
                string IdWindows = cbProyecto.Text;
                string NameFile = "OrdenProduccion" + IdWindows + ".pdf";

                // Verificar si la carpeta "Orden Produccion" existe, si no, crearla
                if (!Directory.Exists(CarpetaFactura))
                {
                    Directory.CreateDirectory(CarpetaFactura);
                }

                // Verificar si la carpeta con el nombre del proyecto existe, si no, crearla
                if (!Directory.Exists(carpetaNombre))
                {
                    Directory.CreateDirectory(carpetaNombre);
                }

                // Crear la ruta completa del archivo PDF
                string rutaArchivoPDF = Path.Combine(carpetaNombre, NameFile);

                // Crear el documento en modo horizontal (landscape)
                Document document = new Document(PageSize.A4.Rotate());
                // Crear un nuevo objeto PdfWriter para escribir el documento en un archivo
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(rutaArchivoPDF, FileMode.Create));

                // Abrir el documento
                document.Open();
                #endregion

                #region Tabla de Información 
                // Agregar un Título con el Sistema, Color y Diseño de la Ventana
                PdfPTable datosTable = new PdfPTable(1)
                {
                    TotalWidth = 500f, // Ajusta el ancho total según tus necesidades
                    LockedWidth = true
                };

                // Celda 1: Datos del Proyecto
                PdfPCell cellDatosProyecto = new PdfPCell(new Phrase("Orden de Producción del Proyecto: " + cbProyecto.Text, FontFactory.GetFont(FontFactory.HELVETICA, 16, iTextSharp.text.BaseColor.WHITE)))
                {
                    BackgroundColor = new iTextSharp.text.BaseColor(70, 130, 180),
                    BorderWidth = 1f,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                };
                datosTable.AddCell(cellDatosProyecto);
                document.Add(datosTable);
                document.Add(new Paragraph(" "));
                #endregion

                #region Tabla de Ventanas
                #region Tabla 5020
                // Agregar al Documento los Datos del dgvOrdenProduccion
                PdfPTable table = new PdfPTable(dgvOrdenProduccion.Columns.Count)
                {
                    TotalWidth = 800f, // Ajusta el ancho total según tus necesidades
                    LockedWidth = true
                };

                // Ancho personalizado para cada una de las 19 columnas (ajusta los valores según tus necesidades)
                float[] anchosColumnas = new float[] { 50f, 50f, 50f, 50f, 50f, 50f, 50f, 50f, 50f, 50f, 50f, 50f, 50f, 50f, 50f, 50f, 50f, 50f, 50f };
                table.SetWidths(anchosColumnas);

                // Celda 1: Encabezados de las columnas
                foreach (DataGridViewColumn column in dgvOrdenProduccion.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 9, iTextSharp.text.BaseColor.WHITE)))
                    {
                        BackgroundColor = new iTextSharp.text.BaseColor(70, 130, 180),
                        BorderWidth = 1f,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_CENTER
                    };
                    table.AddCell(cell);
                }

                // Celda 2: Datos de las filas
                foreach (DataGridViewRow row in dgvOrdenProduccion.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        // Validar si la celda es nula
                        if (cell.Value == null)
                        {
                            cell.Value = " ";
                        }
                        PdfPCell celda = new PdfPCell(new Phrase(cell.Value.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, iTextSharp.text.BaseColor.BLACK)))
                        {
                            BorderWidth = 1f,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_CENTER
                        };
                        table.AddCell(celda);
                    }
                }
                document.Add(table);
                document.Add(new Paragraph(" "));
                #endregion

                #region Tabla 8025
                // Agregar al Documento los Datos del dgvOrdenProduccion
                PdfPTable table8025 = new PdfPTable(dgvOrdenProduccion8025.Columns.Count)
                {
                    TotalWidth = 800f, // Ajusta el ancho total según tus necesidades
                    LockedWidth = true
                };

                // Celda 1: Encabezados de las columnas
                foreach (DataGridViewColumn column8025 in dgvOrdenProduccion8025.Columns)
                {
                    PdfPCell cell8025 = new PdfPCell(new Phrase(column8025.HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 9, iTextSharp.text.BaseColor.WHITE)))
                    {
                        BackgroundColor = new iTextSharp.text.BaseColor(70, 130, 180),
                        BorderWidth = 1f,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_CENTER
                    };
                    table8025.AddCell(cell8025);
                }

                // Celda 2: Datos de las filas
                foreach (DataGridViewRow row8025 in dgvOrdenProduccion8025.Rows)
                {
                    foreach (DataGridViewCell cell8025 in row8025.Cells)
                    {
                        // Validar si la celda es nula
                        if (cell8025.Value == null)
                        {
                            cell8025.Value = " ";
                        }
                        PdfPCell celda8025 = new PdfPCell(new Phrase(cell8025.Value.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, iTextSharp.text.BaseColor.BLACK)))
                        {
                            BorderWidth = 1f,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_CENTER
                        };
                        table8025.AddCell(celda8025);
                    }
                }
                document.Add(table8025);
                document.Add(new Paragraph(" "));
                #endregion

                #endregion

                #region Cerrar Documento
                document.Close();
                MessageBox.Show("Hoja de Producción Generada Correctamente", "Hoja de Producción", MessageBoxButtons.OK, MessageBoxIcon.Information);
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al generar la hoja de producción: " + ex.Message);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void frmOrdenProduccion_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmDashUser frmDash = new frmDashUser();
            frmDash.Show();
        }
    }
}