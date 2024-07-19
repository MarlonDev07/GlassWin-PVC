﻿using Negocio.Company.Quote;
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
        //Variables
        N_Quote NQuote = new N_Quote();
        private decimal[] availableBars;
        private decimal[] requiredLengths;

        public frmOptimizador(decimal[] requiredLengths, decimal[] availableBars)
        {
            InitializeComponent();
            this.requiredLengths = requiredLengths;
            this.availableBars = availableBars;

            // Configurar el DataGridView al inicializar el formulario
            ConfigureDataGridView();

            // Ejecutar la optimización
            OptimizeCutsAndDisplayResults();
        }

        private void ConfigureDataGridView()
        {
            // Definir columnas para el DataGridView
            dgvResults1.Columns.Add("colBar", "Barra");
            dgvResults1.Columns.Add("colUbicacion", "Ubicación");
           // dgvResults1.Columns.Add("colMedidas", "Medidas");
            dgvResults1.Columns.Add("colCuts", "Cortes");
            dgvResults1.Columns.Add("colResiduos", "Residuos"); // Nueva columna para residuos
            AdjustColumnWidthsToFitContent();
        }


        private void AdjustColumnWidthsToFitContent()
        {
            foreach (DataGridViewColumn column in dgvResults1.Columns)
            {
                dgvResults1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }


        private void OptimizeCutsAndDisplayResults()
        {
            try
            {
                // Llamar al método OptimizeCuts
                List<List<decimal>> optimizedCuts = OptimizeCuts(availableBars, requiredLengths);

                // Mostrar los resultados en el DataGridView
                dgvResults1.Rows.Clear();

                // Obtén el DataGridView del formulario de producción
                var productionForm = Application.OpenForms.OfType<frmOrdenProduccion>().FirstOrDefault();
                if (productionForm == null)
                {
                    MessageBox.Show("No se pudo encontrar el formulario de producción.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var dgvOrdenProduccion = productionForm.dgvOrdenProduccion;

                for (int i = 0; i < optimizedCuts.Count; i++)
                {
                    string bar = "Barra " + (i + 1);
                    string cuts = string.Join(", ", optimizedCuts[i].Select(c => c.ToString("0.00") + " m"));

                    // Si "Cortes" está vacío, saltar esta iteración
                    if (string.IsNullOrWhiteSpace(cuts))
                    {
                        continue;
                    }

                    // Obtener Ubicación (se asume que se usa el índice de fila para obtener la información)
                    var ubicacion = dgvOrdenProduccion.Rows[i].Cells["Ubicacion"].Value?.ToString() ?? "";
                    // var medidas = dgvOrdenProduccion.Rows[i].Cells["Cargador"].Value?.ToString() ?? "";

                    // Calcular el residuo
                    decimal totalCuts = optimizedCuts[i].Sum();
                    decimal barLength = availableBars[i];
                    decimal residue = barLength - totalCuts;

                    // Añadir la fila al DataGridView
                    dgvResults1.Rows.Add(bar, ubicacion, /*medidas,*/ cuts, residue.ToString("0.00") + " m");
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



        private List<List<decimal>> OptimizeCuts(decimal[] availableBars, decimal[] requiredLengths)
        {
            // Ordenar las longitudes requeridas de mayor a menor
            Array.Sort(requiredLengths);
            Array.Reverse(requiredLengths);

            // Lista para almacenar los cortes óptimos para cada barra
            List<List<decimal>> optimizedCuts = new List<List<decimal>>();

            foreach (decimal bar in availableBars)
            {
                List<decimal> cuts = new List<decimal>();
                decimal remainingLength = bar;

                foreach (decimal length in requiredLengths.ToList())
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
                optimizedCuts.Add(new List<decimal>(requiredLengths));
            }

            return optimizedCuts;
        }















        private void cbProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Cargar el IdQuote en txtOrden y Name en txtName
                if (cbProyecto.SelectedIndex > 0)
                {
                    DataRowView selectedRow = (DataRowView)cbProyecto.SelectedItem;
                    int idQuote = Convert.ToInt32(selectedRow["IdQuote"]);
                    string name = selectedRow["Name"].ToString();

                    txtOrden.Text = idQuote.ToString();
                    txtName.Text = name; // Asignar el valor del campo "Name" al TextBox correspondiente

                    // Cargar los detalles del producto en dgvResults1
                    DataTable productDetails = NQuote.GetProductDetailsByIdQuote(idQuote);
                    if (productDetails != null)
                    {
                        dgvResults1.DataSource = productDetails;
                    }
                    else
                    {
                        dgvResults1.DataSource = null;
                        MessageBox.Show("No se encontraron detalles del producto para la cotización seleccionada.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    txtOrden.Clear(); // Limpiar el campo si no hay proyecto seleccionado
                    txtName.Clear(); // Limpiar el campo de nombre si no hay proyecto seleccionado
                    dgvResults1.DataSource = null; // Limpiar el DataGridView si no hay proyecto seleccionado
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al cargar las ventanas: " + ex.Message);
            }
        }



        //Metodo para cargar los proyectos
        private void LoadProjects()
        {
            try
            {
                DataTable projects = NQuote.GetProjectsByCompanyId();

                if (projects != null)
                {
                    // Crear una nueva fila con valores en blanco
                    DataRow blankRow = projects.NewRow();
                    blankRow["ProjectName"] = "";
                    blankRow["IdQuote"] = DBNull.Value; // O el valor que consideres apropiado para un ID vacío
                    blankRow["Name"] = ""; // Agregar el campo "Name" en blanco
                    projects.Rows.InsertAt(blankRow, 0); // Insertar la fila al inicio del DataTable

                    cbProyecto.DataSource = projects;
                    cbProyecto.DisplayMember = "ProjectName";
                    cbProyecto.ValueMember = "IdQuote";
                    //CargaCompleta = true;
                    cbProyecto.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("No se encontraron proyectos para la compañía seleccionada.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al cargar los proyectos: " + ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnOptimize_Click(object sender, EventArgs e)
        {
           /* try
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
                dgvResults1.Rows.Clear();
                for (int i = 0; i < optimizedCuts.Count; i++)
                {
                    string bar = "Barra " + (i + 1);
                    string cuts = string.Join(", ", optimizedCuts[i].Select(c => c.ToString("0.00") + " m"));
                    dgvResults1.Rows.Add(bar, cuts);
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Error: La cadena de entrada no tiene el formato correcto. Asegúrese de que los datos ingresados sean números válidos y estén separados por comas.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

    }
}
