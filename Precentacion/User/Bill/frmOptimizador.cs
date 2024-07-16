using Negocio.Company.Quote;
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
        public frmOptimizador()
        {
            InitializeComponent();
            // Configurar el DataGridView al inicializar el formulario
           // ConfigureDataGridView();
            LoadProjects();
            cbProyecto.Focus();
        }

        private void ConfigureDataGridView()
        {
            // Definir columnas para el DataGridView
            dgvResults1.Columns.Add("colBar", "Barra");
            dgvResults1.Columns.Add("colCuts", "Cortes");
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

    }
}
