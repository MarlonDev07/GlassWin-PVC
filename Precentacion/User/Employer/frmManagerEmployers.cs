using Dominio.ClassFunction.InputBox;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Negocio.Company.Employer;
using Precentacion.User.DashBoard;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Precentacion.User.Employer
{
    public partial class frmManagerEmployers : MaterialSkin.Controls.MaterialForm
    {
        #region Variables
        N_Employer N_Employer = new N_Employer();
        N_Payment N_Payment = new N_Payment();
        clsInfo24Hrs Info24Hrs = new clsInfo24Hrs();
        bool EventFormClose = true;
        decimal SalaryxHours;
        int IdEmployer;
        decimal HoursOrdinary;
        decimal HoursExtra;
        decimal SalaryBase;
        decimal CCSS;
        decimal SalaryPay;
        decimal Comision;
        decimal SalaryxHoursPay;
        string Payment;
        #endregion

        #region Constructor
        public frmManagerEmployers()
        {
            InitializeComponent();
            Initialize();
        }
        #endregion

        #region Initialize
        private void Initialize()
        {
            LoadEmployers();
            EmplyeeUI.loadMaterial(this);
        }
        private void ConfigureDataGridView()
        {
            // Supongamos que tu DataGridView se llama dataGridView1
            dgvEmployers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEmployers.MultiSelect = false; // Si deseas permitir la selección de una sola fila
        }
        private void LoadEmployers()
        {
            //Cargar Los Empleados en el DataGridView
            dgvEmployers.DataSource = N_Employer.LoadEmployer();
            ConfigDataGridEmployers();
        }
        private void ConfigDataGridEmployers()
        {
            //Modificar todas las columnas al ancho del Form
            dgvEmployers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //Ocultar Columnas
            dgvEmployers.Columns[4].Visible = false;
            dgvEmployers.Columns[5].Visible = false;
            dgvEmployers.Columns[6].Visible = false;
            dgvEmployers.Columns[9].Visible = false;

            //Cambiar el nombre de las columnas
            dgvEmployers.Columns[0].HeaderText = "Id";
            dgvEmployers.Columns[1].HeaderText = "Nombre";
            dgvEmployers.Columns[2].HeaderText = "Apellido";
            dgvEmployers.Columns[3].HeaderText = "Salario Base";
            dgvEmployers.Columns[7].HeaderText = "Telefono";
            dgvEmployers.Columns[8].HeaderText = "Correo";
            dgvEmployers.Columns[10].HeaderText = "Posicion";
            dgvEmployers.Columns[11].HeaderText = "Pago";
            dgvEmployers.Columns[12].HeaderText = "Pago por Hora";

            //Cambiar la Seleccion del DataGridView
            dgvEmployers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //Activar el ReadOnly
            dgvEmployers.ReadOnly = true;

        }
        #endregion

        #region FormClosing
        private void frmManagerEmployers_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmDashUser frm = frmDashUser.Instance;
            if (EventFormClose)
            {
                frm.WindowState = FormWindowState.Normal;
                frm.Show();
                frm.BringToFront();
            }
        }
        #endregion
        
        #region Events
        private void tabControlPlanilla_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlPlanilla.SelectedIndex == 2)
            {
                DGVConfig();
            }
            if (tabControlPlanilla.SelectedIndex == 1)
            {
                ViewVacation();
            }
        }
        private void Nvacaciones_ValueChanged(object sender, EventArgs e)
        {
            //Obtener Salario por Hora
            decimal SalaryxHour = Convert.ToDecimal(dgvEmployers.CurrentRow.Cells[12].Value.ToString());

            //Calcular el Salario de Los dias de Vacaciones Dados
            decimal SalaryVacation = ((SalaryxHour * 8) * Convert.ToDecimal(Nvacaciones.Value));

            //Mostrar el Salario de los Dias de Vacaciones Dados
            txtSalaryforDay.Text = SalaryVacation.ToString("N2");
        }
        #endregion

        //Logica para Mantenimiento de Empleados
        #region Employers

        #region ListEmployers
        private void editarEmpleadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Pasar los datos del DataGridView a los TextBox del TabUpdate
                txtIdUpdate.Text = dgvEmployers.CurrentRow.Cells[0].Value.ToString();
                txtNameUpdate.Text = dgvEmployers.CurrentRow.Cells[1].Value.ToString();
                txtLastNameUpdate.Text = dgvEmployers.CurrentRow.Cells[2].Value.ToString();
                txtSalaryUpdate.Text = dgvEmployers.CurrentRow.Cells[3].Value.ToString();
                txtPhoneUpdate.Text = dgvEmployers.CurrentRow.Cells[7].Value.ToString();
                txtEmailUpdate.Text = dgvEmployers.CurrentRow.Cells[8].Value.ToString();
                txtAddressUpdate.Text = dgvEmployers.CurrentRow.Cells[9].Value.ToString();
                txtPositionUpdate.Text = dgvEmployers.CurrentRow.Cells[10].Value.ToString();
                cbPaymentUpdate.Text = dgvEmployers.CurrentRow.Cells[11].Value.ToString();
                dtDateofBirthUpdate.Value = Convert.ToDateTime(dgvEmployers.CurrentRow.Cells[5].Value.ToString());
                dtDateofEntryUpdate.Value = Convert.ToDateTime(dgvEmployers.CurrentRow.Cells[6].Value.ToString());
                //CalcSalaryUpdate_textchange(sender, e);
                txtSalaryxHoursUpdate.Text = dgvEmployers.CurrentRow.Cells[12].Value.ToString();
                tabControlEmployers.SelectedIndex = 2;
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Cargar los Datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void tabControlEmployers_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Validar que el campo de Cedula no este vacio en TabUpdate
            if (tabControlEmployers.SelectedIndex == 2)
            {
                if (txtIdUpdate.Text == "")
                {
                    tabControlEmployers.SelectedIndex = 0;
                    MessageBox.Show("Debe Seleccionar un Empleado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }

        }
        private void pagarSalarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Pasar los datos del DataGridView a los TextBox del TabPlanilla
            try
            {
                IdEmployer = Convert.ToInt32(dgvEmployers.CurrentRow.Cells[0].Value.ToString());
                txtEmployerName.Text = dgvEmployers.CurrentRow.Cells[1].Value.ToString() + " " + dgvEmployers.CurrentRow.Cells[2].Value.ToString();
                txtEmpleadoVac.Text = dgvEmployers.CurrentRow.Cells[1].Value.ToString() + " " + dgvEmployers.CurrentRow.Cells[2].Value.ToString();
                txtEmpleadoAgui.Text = dgvEmployers.CurrentRow.Cells[1].Value.ToString() + " " + dgvEmployers.CurrentRow.Cells[2].Value.ToString();
                txtSalaryBase.Text = dgvEmployers.CurrentRow.Cells[3].Value.ToString();
                txtSalaryPay.Text = dgvEmployers.CurrentRow.Cells[11].Value.ToString();
                SalaryxHoursPay = Convert.ToDecimal(dgvEmployers.CurrentRow.Cells[12].Value);
                ConfigDataGrid();
                tabControlManager.SelectedIndex = 1;
                tabControlPlanilla.SelectedIndex = 0;

            }
            catch (Exception)
            {
                MessageBox.Show("Error al Cargar los Datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion  

        #region NewEmployer 
        private void btnNewEmployer_Click(object sender, EventArgs e)
        {
            NewEmployer();
        }
        private void NewEmployer()
        {
            try
            {
                if (ValidateField())
                {
                    if (InsertEmployer())
                    {
                        if (InsertVacaion())
                        {
                            CleanFieldsNewEmployer();
                        }                     
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void CalcSalary_textchange(object sender, EventArgs e)
        {
            try
            {
                string Option = cbPaymentNew.Text;
                switch (Option)
                {
                    case "Semanal":
                        decimal Salary = Convert.ToDecimal(txtSalaryNew.Text);
                        //LIMITAR LA VARIABLE SALARYXHOURS A 2 DECIMALES
                        SalaryxHours = (Salary / 4.333m) / 48;
                        txtSalaryxHoursNew.Text = SalaryxHours.ToString("N2");
                        break;
                    case "Quincenal":
                        decimal Salary1 = Convert.ToDecimal(txtSalaryNew.Text);
                        SalaryxHours = Math.Round(Salary1, 2) / 300;
                        txtSalaryxHoursNew.Text = SalaryxHours.ToString("N2");
                        break;
                    case "Mensual":
                        decimal Salary2 = Convert.ToDecimal(txtSalaryNew.Text);
                        SalaryxHours = Math.Round(Salary2, 2);
                        txtSalaryxHoursNew.Text = SalaryxHours.ToString("N2");
                        break;
                }
            }
            catch (Exception)
            {

            }
        }
        private bool ValidateField()
        {
            try
            {
                if (txtIdNew.Text == "")
                {
                    MessageBox.Show("El campo Cedula es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtIdNew.Focus();
                    return false;
                }
                if (txtNameNew.Text == string.Empty)
                {
                    MessageBox.Show("El campo Nombre es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNameNew.Focus();
                    return false;
                }
                if (txtLastNameNew.Text == string.Empty)
                {
                    MessageBox.Show("El campo Apellido es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtLastNameNew.Focus();
                    return false;
                }
                if (txtPhoneNew.Text == string.Empty)
                {
                    MessageBox.Show("El campo Telefono es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPhoneNew.Focus();
                    return false;
                }
                if (txtEmailNew.Text == string.Empty)
                {
                    MessageBox.Show("El campo Correo es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmailNew.Focus();
                    return false;
                }
                if (txtAddressNew.Text == "")
                {
                    MessageBox.Show("El campo Direccion es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAddressNew.Focus();
                    return false;
                }
                if (txtPositionNew.Text == string.Empty)
                {
                    MessageBox.Show("El campo Posicion es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPositionNew.Focus();
                    return false;
                }
                if (txtSalaryNew.Text == string.Empty)
                {
                    MessageBox.Show("El campo Salario es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSalaryNew.Focus();
                    return false;
                }
                if (cbPaymentNew.Text == "")
                {
                    MessageBox.Show("El Periodo de Pago es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSalaryNew.Focus();
                    return false;
                }
                if (txtSalaryNew.Text == string.Empty)
                {
                    MessageBox.Show("El campo Pago es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSalaryNew.Focus();
                    return false;
                }
                if (txtSalaryxHoursNew.Text == string.Empty)
                {
                    MessageBox.Show("El campo Pago por Hora es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSalaryxHoursNew.Focus();
                    return false;
                }
                if (dtDateofBirthNew.Value > DateTime.Now)
                {
                    MessageBox.Show("La Fecha de Nacimiento no puede ser mayor a la Fecha Actual", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dtDateofBirthNew.Focus();
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Validar los Campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private bool InsertEmployer()
        {
            try
            {
                int Id = Convert.ToInt32(txtIdNew.Text);
                string Name = txtNameNew.Text;
                string LastName = txtLastNameNew.Text;
                string Phone = txtPhoneNew.Text;
                string Email = txtEmailNew.Text;
                string Address = txtAddressNew.Text;
                string Position = txtPositionNew.Text;
                decimal Salary = Convert.ToDecimal(txtSalaryNew.Text);
                string Payment = cbPaymentNew.Text;
                DateTime DateofBirth = dtDateofBirthNew.Value;
                DateTime DateofEntry = dtDateofEntryNew.Value;
                decimal SalarioHora = Convert.ToDecimal(txtSalaryxHoursNew.Text);
                if (N_Employer.InsertEmployer(Id, Name, LastName, Salary.ToString(), "", DateofBirth.ToString(), DateofEntry.ToString(), Phone, Email, Address, Position, Payment, SalarioHora /*SalaryxHours*/))
                {
                    MessageBox.Show("Empleado Registrado Correctamente", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadEmployers();
                    return true;
                }
                else
                {
                    MessageBox.Show("No se pudo Registrar el Empleado", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }
        private void CleanFieldsNewEmployer()
        {
            txtIdNew.Text = "";
            txtNameNew.Text = "";
            txtLastNameNew.Text = "";
            txtPhoneNew.Text = "";
            txtEmailNew.Text = "";
            txtAddressNew.Text = "";
            txtPositionNew.Text = "";
            txtSalaryNew.Text = "";
            cbPaymentNew.Text = "";
            txtSalaryxHoursNew.Text = "";
            dtDateofBirthNew.Value = DateTime.Now;
            dtDateofEntryNew.Value = DateTime.Now;
            tabControlEmployers.SelectedIndex = 0;
        }
        private void btnCancelNew_Click(object sender, EventArgs e)
        {
            CleanFieldsNewEmployer();
        }
        #endregion

        #region UpdateEmployer
        private void btnUpdateEmployer_Click(object sender, EventArgs e)
        {
            UpdateEmployer();
        }
        private void UpdateEmployer()
        {
            try
            {
                if (ValidateFieldUpdate())
                {
                    if (UpdateEmployerDB())
                    {
                        CleanFieldsUpdateEmployer();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void CalcSalaryUpdate_textchange(object sender, EventArgs e)
        {
            try
            {
                string Option = cbPaymentUpdate.Text;
                switch (Option)
                {
                    case "Semanal":
                        decimal Salary = Convert.ToDecimal(txtSalaryUpdate.Text);
                        SalaryxHours = (Salary / 4.333m) / 48;
                        txtSalaryxHoursUpdate.Text = SalaryxHours.ToString("c");
                        break;
                    case "Quincenal":
                        decimal Salary1 = Convert.ToDecimal(txtSalaryUpdate.Text);
                        SalaryxHours = Math.Round(Salary1, 2) / 240;
                        txtSalaryxHoursUpdate.Text = SalaryxHours.ToString("c");
                        break;
                    case "Mensual":
                        decimal Salary2 = Convert.ToDecimal(txtSalaryUpdate.Text);
                        SalaryxHours = Math.Round(Salary2, 2);
                        txtSalaryxHoursUpdate.Text = SalaryxHours.ToString("c");
                        break;
                }
               
            }
            catch (Exception)
            {

            }
        }
        private bool ValidateFieldUpdate()
        {
            try
            {
                if (txtIdUpdate.Text == "")
                {
                    MessageBox.Show("El campo Cedula es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtIdUpdate.Focus();
                    return false;
                }
                if (txtNameUpdate.Text == string.Empty)
                {
                    MessageBox.Show("El campo Nombre es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNameUpdate.Focus();
                    return false;
                }
                if (txtLastNameUpdate.Text == string.Empty)
                {
                    MessageBox.Show("El campo Apellido es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtLastNameUpdate.Focus();
                    return false;
                }
                if (txtPhoneUpdate.Text == string.Empty)
                {
                    MessageBox.Show("El campo Telefono es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPhoneUpdate.Focus();
                    return false;
                }
                if (txtEmailUpdate.Text == string.Empty)
                {
                    MessageBox.Show("El campo Correo es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmailUpdate.Focus();
                    return false;
                }
                if (txtAddressUpdate.Text == "")
                {
                    MessageBox.Show("El campo Direccion es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAddressUpdate.Focus();
                    return false;
                }
                if (txtPositionUpdate.Text == string.Empty)
                {
                    MessageBox.Show("El campo Posicion es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPositionUpdate.Focus();
                    return false;
                }
                if (txtSalaryUpdate.Text == string.Empty)
                {
                    MessageBox.Show("El campo Salario es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSalaryUpdate.Focus();
                    return false;
                }
                if (cbPaymentUpdate.Text == "")
                {
                    MessageBox.Show("El Periodo de Pago es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSalaryUpdate.Focus();
                    return false;
                }
                if (txtSalaryUpdate.Text == string.Empty)
                {
                    MessageBox.Show("El campo Pago es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSalaryUpdate.Focus();
                    return false;
                }
                if (txtSalaryxHoursUpdate.Text == string.Empty)
                {
                    MessageBox.Show("El campo Pago por Hora es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (dtDateofBirthUpdate.Value > DateTime.Now)
                {
                    MessageBox.Show("La Fecha de Nacimiento no puede ser mayor a la Fecha Actual", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dtDateofBirthUpdate.Focus();
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Validar los Campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private bool UpdateEmployerDB()
        {
            try
            {
                int Id = Convert.ToInt32(txtIdUpdate.Text);
                string Name = txtNameUpdate.Text;
                string LastName = txtLastNameUpdate.Text;
                string Phone = txtPhoneUpdate.Text;
                string Email = txtEmailUpdate.Text;
                string Address = txtAddressUpdate.Text;
                string Position = txtPositionUpdate.Text;
                decimal Salary = Convert.ToDecimal(txtSalaryUpdate.Text);
                string Payment = cbPaymentUpdate.Text;
                DateTime DateofBirth = dtDateofBirthUpdate.Value;
                DateTime DateofEntry = dtDateofEntryUpdate.Value;
                decimal SalaryHours = Convert.ToDecimal(txtSalaryxHoursUpdate.Text);
                if (N_Employer.UpdateEmployer(Id, Name, LastName, Salary.ToString(), "", DateofBirth.ToString(), DateofEntry.ToString(), Phone, Email, Address, Position, Payment, SalaryHours/*SalaryxHours*/))
                {
                    MessageBox.Show("Empleado Actualizado Correctamente", "Actualizacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadEmployers();
                    return true;
                }
                else
                {
                    MessageBox.Show("No se pudo Actualizar el Empleado", "Actualizacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }
        private void CleanFieldsUpdateEmployer()
        {
            txtIdUpdate.Text = "";
            txtNameUpdate.Text = "";
            txtLastNameUpdate.Text = "";
            txtPhoneUpdate.Text = "";
            txtEmailUpdate.Text = "";
            txtAddressUpdate.Text = "";
            txtPositionUpdate.Text = "";
            txtSalaryUpdate.Text = "";
            cbPaymentUpdate.Text = "";
            txtSalaryxHoursUpdate.Text = "";
            dtDateofBirthUpdate.Value = DateTime.Now;
            dtDateofEntryUpdate.Value = DateTime.Now;
            tabControlEmployers.SelectedIndex = 0;
        }

        private void btnCancelUpdate_Click(object sender, EventArgs e)
        {
            CleanFieldsUpdateEmployer();
        }

        #endregion

        #endregion

        //Logica para Mantenimiento de Planilla
        #region PayRoll
        #region DataGrid Config
        private void ConfigDataGrid() 
        {
            try
            {
                //Limpiar el DataGrid por Completo
                dgvPayRoll.Columns.Clear();

                //Agregar Columnas al DataGridView
                dgvPayRoll.Columns.Add("Date", "Fecha");
                dgvPayRoll.Columns.Add("HourEntry", "Hora de Entrada");
                dgvPayRoll.Columns.Add("HourExit", "Hora de Salida");
                dgvPayRoll.Columns.Add("HoursOrdinary", "Horas Ordinarias");
                dgvPayRoll.Columns.Add("HourExtra", "Horas Extras");

                //Modificar todas las columnas al ancho del Form
                dgvPayRoll.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                //Funcion para agregar las fechas al DataGridView
                LoadDates();

                //Funcion para calcular las horas
                LoadHours();

                //Funcion para sumar las horas
                SumHours();

                //Funcion para calcular la Comision
                SelectAllSellByEmployer();

                //Calcular el Salario
                CalcSalary();
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Configurar el DataGridView", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadDates() 
        {
            //Agregar Fechas al dataGrid segun el tipo de pago (Semanal, Quincenal o Mensual)
            try
            {
                string HoursEntry = "07:00";
                string HoursExit = "17:00";
                string Option = txtSalaryPay.Text;
                switch (Option)
                {
                    case "Semanal":
                        //Cambiar Payment a Semanal
                        Payment = "Semanal";
                        //Obtener la Fecha de Hoy
                        DateTime Date = DateTime.Now;

                        //Restarele 6 dias a la fecha de hoy
                        Date = Date.AddDays(-6);

                        //Agregar las fechas al DataGridView
                        for (int i = 0; i < 7; i++)
                        {
                            dgvPayRoll.Rows.Add(Date.ToString("dd/MM/yyyy"), "", "", "", "");
                            //Validar que la Fecha sea Miercoles
                            if (Date.DayOfWeek == DayOfWeek.Wednesday)
                            {
                                //Agregar Fecha de Entrada y Salida
                                dgvPayRoll.Rows[i].Cells[1].Value = "07:00";
                                dgvPayRoll.Rows[i].Cells[2].Value = "17:00";
                            }
                            else
                            {
                                //Agregar Fecha de Entrada y Salida
                                dgvPayRoll.Rows[i].Cells[1].Value = HoursEntry;
                                dgvPayRoll.Rows[i].Cells[2].Value = HoursExit;
                            }
                            Date = Date.AddDays(1);
                        }
                        break;
                    case "Quincenal":
                        //Cambiar Payment a Quincenal
                        Payment = "Quincenal";
                        //Obtener la Fecha de Hoy
                        DateTime Date1 = DateTime.Now;

                        //Restarle 15 dias a la fecha de hoy
                        Date1 = Date1.AddDays(-15);

                        //Agregar las fechas al DataGridView
                        for (int i = 0; i < 15; i++)
                        {
                            dgvPayRoll.Rows.Add(Date1.ToString("dd/MM/yyyy"), "", "", "", "");

                            //Validar que la Fecha sea Miercoles
                            if (Date1.DayOfWeek == DayOfWeek.Wednesday)
                            {
                                //Agregar Fecha de Entrada y Salida
                                dgvPayRoll.Rows[i].Cells[1].Value = "07:00";
                                dgvPayRoll.Rows[i].Cells[2].Value = "17:00";
                            }
                            else
                            {
                                //Agregar Fecha de Entrada y Salida
                                dgvPayRoll.Rows[i].Cells[1].Value = HoursEntry;
                                dgvPayRoll.Rows[i].Cells[2].Value = HoursExit;
                            }

                            Date1 = Date1.AddDays(1);
                        }
                        break;
                    case "Mensual":
                        //Obtener la Fecha de Hoy
                        DateTime Date2 = DateTime.Now;

                        //Restarle 28 dias a la fecha de hoy
                        Date2 = Date2.AddDays(-28);

                        //Agregar las fechas al DataGridView
                        for (int i = 0; i < 30; i++)
                        {
                            dgvPayRoll.Rows.Add(Date2.ToString("dd/MM/yyyy"), "", "", "", "");
                            //Validar que la Fecha sea Miercoles
                            if (Date2.DayOfWeek == DayOfWeek.Wednesday)
                            {
                                //Agregar Fecha de Entrada y Salida
                                dgvPayRoll.Rows[i].Cells[1].Value = "07:00";
                                dgvPayRoll.Rows[i].Cells[2].Value = "17:00";
                            }
                            else
                            {
                                //Agregar Fecha de Entrada y Salida
                                dgvPayRoll.Rows[i].Cells[1].Value = HoursEntry;
                                dgvPayRoll.Rows[i].Cells[2].Value = HoursExit;
                            }

                            Date2 = Date2.AddDays(1);
                        }
                        break;
                }
            }
            catch (Exception)
            {

               MessageBox.Show("Error al Cargar las Fechas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadHours() 
        { 
            //Calcular las Horas Ordinarias y Extras
            try
            {
                //Recorrer el DataGridView
                for (int i = 0; i < dgvPayRoll.Rows.Count-1; i++)
                {
                    //Obtener la Fecha
                    string Date = dgvPayRoll.Rows[i].Cells[0].Value.ToString();

                    //Convertir la Fecha de String a DateTime
                    DateTime DateDate = Convert.ToDateTime(Date);

                    //Obtener la Hora de Entrada y Salida
                    string HourEntry = dgvPayRoll.Rows[i].Cells[1].Value.ToString();
                    string HourExit = dgvPayRoll.Rows[i].Cells[2].Value.ToString();

                    //Convertir las Horas de String a DateTime
                    DateTime HourEntryDate = Convert.ToDateTime(HourEntry);
                    DateTime HourExitDate = Convert.ToDateTime(HourExit);

                    //Calcular las Horas Trabajadas
                    TimeSpan HoursWorck = HourExitDate - HourEntryDate;

                    //Validar si la Hora de entrada es mayor o igual a las 16:30 y la Hora de Salida es menor o igual a las 06:00
                    if (HourEntryDate >= Convert.ToDateTime("16:30") && HourExitDate <= Convert.ToDateTime("23:59"))
                    {
                        if (HourEntryDate >= Convert.ToDateTime("16:30") && HourExitDate <= Convert.ToDateTime("06:00"))
                        {
                            //Calcular las Horas Trabajadas
                            HoursWorck = HourExitDate.AddDays(1) - HourEntryDate;

                            //Agregar las Horas al DataGridView
                            dgvPayRoll.Rows[i].Cells[3].Value = 0;
                            dgvPayRoll.Rows[i].Cells[4].Value = HoursWorck.TotalHours;
                            continue;
                        }
                        else
                        {
                            //Calcular las Horas Trabajadas
                            HoursWorck = HourExitDate - HourEntryDate;

                            //Agregar las Horas al DataGridView
                            dgvPayRoll.Rows[i].Cells[3].Value = 0;
                            dgvPayRoll.Rows[i].Cells[4].Value = HoursWorck.TotalHours;
                            continue;
                           
                        }   
                    }
                    else
                    {
                        //Validar que la Fecha no sea fin de Semana
                        if (DateDate.DayOfWeek == DayOfWeek.Saturday || DateDate.DayOfWeek == DayOfWeek.Sunday)
                        {
                            switch (Payment)
                            {
                                case "Semanal":
                                    //Agregar las Horas en Variables
                                    string HE = dgvPayRoll.Rows[i].Cells[4].Value.ToString();

                                    //Validar que las Horas Extras sean mayores a 0
                                    if (HE != "0" && HE != "")
                                    {
                                        //Agregar las Horas al DataGridView
                                        dgvPayRoll.Rows[i].Cells[4].Value = HE;
                                    }
                                    else
                                    {
                                        //Agregar las Horas al DataGridView
                                        dgvPayRoll.Rows[i].Cells[3].Value = 0;
                                        dgvPayRoll.Rows[i].Cells[4].Value = 0;
                                    }
                                    break;
                                case "Quincenal":
                                    //Calcular las Horas Ordinarias Que son las primeras 9 horas
                                    TimeSpan HoursOrdinary = TimeSpan.FromHours(10);

                                    //Calcular las Horas Extras
                                    TimeSpan HoursExtra = HoursWorck - HoursOrdinary;

                                    //Agregar las Horas al DataGridView
                                    dgvPayRoll.Rows[i].Cells[3].Value = HoursOrdinary.TotalHours;
                                    dgvPayRoll.Rows[i].Cells[4].Value = HoursExtra.TotalHours;
                                    break;
                            }
                        }
                        else
                        {
                            //Validar que las Horas Trabajadas sean mayores a 9.5
                            if (HoursWorck.TotalHours > 10)
                            {
                                if (DateDate.DayOfWeek == DayOfWeek.Wednesday && HoursWorck.TotalHours == 10)
                                {
                                    //Agregar las Horas Trabajadas al DataGridView
                                    dgvPayRoll.Rows[i].Cells[3].Value = HoursWorck.TotalHours;
                                    dgvPayRoll.Rows[i].Cells[4].Value = 0;
                                }
                                else
                                {
                                    //Calcular las Horas Ordinarias Que son las primeras 9 horas
                                    TimeSpan HoursOrdinary = TimeSpan.FromHours(10);

                                    //Calcular las Horas Extras
                                    TimeSpan HoursExtra = HoursWorck - HoursOrdinary;

                                    //Agregar las Horas al DataGridView
                                    dgvPayRoll.Rows[i].Cells[3].Value = HoursOrdinary.TotalHours;
                                    dgvPayRoll.Rows[i].Cells[4].Value = HoursExtra.TotalHours;
                                }
                            }
                            else
                            {
                                //Agregar las Horas Trabajadas al DataGridView
                                dgvPayRoll.Rows[i].Cells[3].Value = HoursWorck.TotalHours;
                                dgvPayRoll.Rows[i].Cells[4].Value = 0;
                            }
                        }
                    }
                }         
            }
            catch (Exception)
            {

                MessageBox.Show("Error al Calcular las Horas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SumHours() 
        {
            //Sumar las Horas Ordinarias y Extras
            try
            {
                //Variables para almacenar las Horas
                decimal HoursOrdinary = 0;
                decimal HoursExtra = 0;

                //Recorrer el DataGridView
                for (int i = 0; i < dgvPayRoll.Rows.Count - 1; i++)
                {
                    //Obtener las Horas Ordinarias y Extras
                    HoursOrdinary += Convert.ToDecimal(dgvPayRoll.Rows[i].Cells[3].Value);
                    HoursExtra += Convert.ToDecimal(dgvPayRoll.Rows[i].Cells[4].Value);
                }

                //Agregar las Horas al TextBox
                txtHO.Text = HoursOrdinary.ToString();
                txtHE.Text = HoursExtra.ToString();
            }
            catch (Exception)
            {

                MessageBox.Show("Error al Sumar las Horas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CalcSalary() 
        { 
            HoursOrdinary = Convert.ToDecimal(txtHO.Text);
            HoursExtra = Convert.ToDecimal(txtHE.Text);
            decimal SalaryOrdinary = SalaryxHoursPay * HoursOrdinary;
            decimal HourExtrasPay = SalaryxHoursPay * 1.60m;
            decimal SalaryExtra = HourExtrasPay * HoursExtra;
            SalaryBase = SalaryOrdinary + SalaryExtra;
            CCSS = SalaryBase * 0.1067m;
            SalaryPay = SalaryBase - CCSS + Comision;
            txtSalaryBruto.Text = SalaryBase.ToString("c");
            txtCCSS.Text = CCSS.ToString("c");
            txtSalaryNeto.Text = SalaryPay.ToString("c");
            cbkccss.Checked = true;
        }
        private void btnCalc_Click(object sender, EventArgs e)
        {
            LoadHours();
            SumHours();
            CalcSalary();
        }
        private void SelectAllSellByEmployer() 
        {
            string Message;
            decimal Sell = N_Payment.SelectAllSellByEmployer(IdEmployer,txtSalaryPay.Text, out Message);
            Comision = Sell * 0.03m;
            txtComision.Text = Comision.ToString("c");
        }
        public bool GeneratePdfPayRoll()
        {
            
            try
            {
                #region Crear el documento
                // Obtener el directorio del escritorio y las carpetas necesarias
                string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string carpetaPlanillas = Path.Combine(escritorio, "Planillas");
                string carpetaNombre = Path.Combine(carpetaPlanillas, txtEmployerName.Text);
                string NameFile = "Planilla n° " + txtEmployerName.Text + ".pdf";

                // Verificar si la carpeta "Proformas" existe, si no, crearla
                if (!Directory.Exists(carpetaPlanillas))
                {
                    Directory.CreateDirectory(carpetaPlanillas);
                }

                // Verificar si la carpeta con el nombre existe, si no, crearla
                if (!Directory.Exists(carpetaNombre))
                {
                    Directory.CreateDirectory(carpetaNombre);
                }

                // Crear la ruta completa del archivo PDF
                string rutaArchivoPDF = Path.Combine(carpetaNombre, NameFile);

                Document document = new Document();
                // Crea un nuevo objeto PdfWriter para escribir el documento en un archivo
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(rutaArchivoPDF, FileMode.Create));

                // Asigna el objeto PdfWriter al documento
                document.Open();
                #endregion

                #region Encabezado
                // Crea una tabla con dos columnas
                PdfPTable Encabezado = new PdfPTable(2);
                Encabezado.WidthPercentage = 120;


                // Agrega la imagen a la primera celda
                string rutaLogo = "";
                if (CompanyCache.IdCompany == 3101794685)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\RioClaroLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 111560456)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\DialexLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 31028013)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\InnovaLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 31025820)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\AluviLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 1230123)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\GlassWinLogo.png";
                    rutaLogo = ruta + Url;

                }
                if (CompanyCache.IdCompany == 205150849)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\MakyLogo.png";
                    rutaLogo = ruta + Url;
                }
                if (CompanyCache.IdCompany == 112540885)
                {
                    //Obtener la Ruta de la Carpeta bin
                    string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                    string Url = "\\Images\\Logos\\VidriosAlturaLogo.png";
                    rutaLogo = ruta + Url;
                }
                PdfPCell imageCell = new PdfPCell(iTextSharp.text.Image.GetInstance(rutaLogo));
                imageCell.Border = PdfPCell.NO_BORDER;
                imageCell.FixedHeight = 120f; // Ajusta la altura de la imagen
                imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
                Encabezado.AddCell(imageCell);

                // Crea un nuevo objeto Font para los textos
                iTextSharp.text.Font titleFont = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 19, iTextSharp.text.Font.BOLD, BaseColor.GRAY);
                iTextSharp.text.Font textFont = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                // Agrega los textos a la segunda celda
                PdfPCell textCell = new PdfPCell();
                textCell.Border = PdfPCell.NO_BORDER;

                // Alinea el contenido de la celda al centro
                textCell.HorizontalAlignment = Element.ALIGN_RIGHT;

                // Agrega el párrafo y los chunks al documento
                Paragraph paragraph = new Paragraph();
                paragraph.Add(new Chunk(CompanyCache.Name, titleFont));
                paragraph.Add(Chunk.NEWLINE);// Salto de línea
                paragraph.Add(new Chunk("Cedula Juridica "+CompanyCache.IdCompany.ToString(), textFont));
                paragraph.Add(Chunk.NEWLINE);
                paragraph.Add(new Chunk("Ubicados en: "+CompanyCache.Address, textFont));
                paragraph.Add(Chunk.NEWLINE);
                paragraph.Add(new Chunk("Teléfonos: "+ CompanyCache.Phone, textFont));
                paragraph.Add(Chunk.NEWLINE);// Salto de línea
                paragraph.Add(Chunk.NEWLINE);// Salto de línea


                textCell.AddElement(paragraph);
                Encabezado.AddCell(textCell);

                // Establece el ancho de la celda de la tabla (ajusta según tus necesidades)
                Encabezado.SetWidths(new float[] { 3f, 4f }); // Primer valor es el ancho de la celda de la imagen

                // Agrega la tabla al documento
                document.Add(Encabezado);

                // Añade la palabra "COTIZACIÓN" debajo de la tabla
                Paragraph cotizacionParagraph = new Paragraph("PLANILLA", titleFont);
                cotizacionParagraph.Alignment = Element.ALIGN_CENTER;
                document.Add(cotizacionParagraph);
                document.Add(new Paragraph(" "));// Esto agrega un espacio en blanco en el documento

                #endregion

                #region Datos del Empleado
                // Crea una tabla con dos columnas
                PdfPTable DatosEmpleado = new PdfPTable(2);
                DatosEmpleado.WidthPercentage = 100;
                DatosEmpleado.HorizontalAlignment = Element.ALIGN_LEFT;
                DatosEmpleado.SetWidths(new float[] { 1f, 1f }); // Primer valor es el ancho de la celda de la imagen

                document.Add(DatosEmpleado);

                // Agregar los datos del Empleado Como Nombre Salario y  Tipo de Planilla
                DatosEmpleado.AddCell(new Phrase("Nombre: ", textFont));
                DatosEmpleado.AddCell(new Phrase(txtEmployerName.Text, textFont));
                DatosEmpleado.AddCell(new Phrase("Salario Base: ", textFont));
                DatosEmpleado.AddCell(new Phrase(txtSalaryBase.Text, textFont));
                DatosEmpleado.AddCell(new Phrase("Tipo de Planilla: ", textFont));
                DatosEmpleado.AddCell(new Phrase(txtSalaryPay.Text, textFont));

                // Agregar la tabla al documento
                document.Add(DatosEmpleado);
                #endregion

                #region Datos de la Planilla
                //Salto Linea
                document.Add(new Paragraph(" "));

                //Agregar un titulo que diga Horario Laborado
                Paragraph HorarioLaborado = new Paragraph("Horario Laborado", titleFont);
                HorarioLaborado.Alignment = Element.ALIGN_CENTER;
                document.Add(HorarioLaborado);
                document.Add(new Paragraph(" "));

                // Cargar el DataGrid al PDF
                PdfPTable DatosHorario = new PdfPTable(dgvPayRoll.Columns.Count);
                DatosHorario.WidthPercentage = 100;
                DatosHorario.HorizontalAlignment = Element.ALIGN_LEFT;
                DatosHorario.SetWidths(new float[] { 1f, 1f, 1f, 1f, 1f }); // Ajusta el ancho de las celdas según tus necesidades
                DatosHorario.HorizontalAlignment = 0; // Ajusta la alineación según tus necesidades
                document.Add(DatosHorario);

                // Agregar los datos del DataGrid al PDF
                for (int i = 0; i < dgvPayRoll.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvPayRoll.Columns.Count; j++)
                    {
                        object value = dgvPayRoll[j, i].Value;
                        string cellValue = value != null ? value.ToString() : string.Empty;
                        DatosHorario.AddCell(new Phrase(cellValue, textFont));
                    }
                }

                // Agregar la tabla al documento
                document.Add(DatosHorario);
                #endregion

                #region Datos de la Planilla
                //Salto Linea
                document.Add(new Paragraph(" "));

                //Agregar un titulo que diga Detalles de la Planilla
                Paragraph DetallesPlanilla = new Paragraph("Detalles de la Planilla", titleFont);
                DetallesPlanilla.Alignment = Element.ALIGN_CENTER;
                document.Add(DetallesPlanilla);
                document.Add(new Paragraph(" "));

                // Crea una tabla con dos columnas
                PdfPTable DatosPlanilla = new PdfPTable(2);
                DatosPlanilla.WidthPercentage = 100;
                DatosPlanilla.HorizontalAlignment = Element.ALIGN_LEFT;
                DatosPlanilla.SetWidths(new float[] { 1f, 1f }); // Primer valor es el ancho de la celda de la imagen
                //Centrar la tabla
                DatosPlanilla.HorizontalAlignment = 1;
                document.Add(DatosPlanilla);

                // Agregar los datos del Empleado Como Nombre Salario y  Tipo de Planilla
                DatosPlanilla.AddCell(new Phrase("Horas Ordinarias: ", textFont));
                DatosPlanilla.AddCell(new Phrase(txtHO.Text, textFont));
                DatosPlanilla.AddCell(new Phrase("Horas Extraas: ", textFont));
                DatosPlanilla.AddCell(new Phrase(txtHE.Text, textFont));
                DatosPlanilla.AddCell(new Phrase("Salario Base: ", textFont));
                DatosPlanilla.AddCell(new Phrase(txtSalaryBruto.Text, textFont));
                DatosPlanilla.AddCell(new Phrase("CCSS: ", textFont));
                DatosPlanilla.AddCell(new Phrase(txtCCSS.Text, textFont));
                DatosPlanilla.AddCell(new Phrase("Salario Neto: ", textFont));
                DatosPlanilla.AddCell(new Phrase(txtSalaryNeto.Text, textFont));

                // Agregar la tabla al documento
                document.Add(DatosPlanilla);
                #endregion



                #region Cerrar el documento
                // Cerrar el documento
                document.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            #endregion
        }

        #region SavePayment
        private void btnSavePayment_Click(object sender, EventArgs e)
        {
            SavePayment();
        }
        private void SavePayment()
        {
            try
            {
                if (ValidateFieldPayment())
                {
                    if (UpdateVacation())
                    {
                        if (InsertPayment())
                        {
                            GeneratePdfPayRoll();
                            CleanFieldsPayment();
                        }
                    }
                   
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private bool ValidateFieldPayment()
        {
            try
            {
                if (txtHO.Text == "")
                {
                    MessageBox.Show("El campo Horas Ordinarias es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtHO.Focus();
                    return false;
                }
                if (txtHE.Text == string.Empty)
                {
                    MessageBox.Show("El campo Horas Extras es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtHE.Focus();
                    return false;
                }
                if (txtSalaryBruto.Text == string.Empty)
                {
                    MessageBox.Show("El campo Salario Bruto es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSalaryBruto.Focus();
                    return false;
                }
                if (txtCCSS.Text == string.Empty)
                {
                    MessageBox.Show("El campo CCSS es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCCSS.Focus();
                    return false;
                }
                if (txtSalaryNeto.Text == "")
                {
                    MessageBox.Show("El campo Salario Neto es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSalaryNeto.Focus();
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Validar los Campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private bool InsertPayment()
        {
            try
            {
                decimal HursOrdinary = Convert.ToDecimal(txtHO.Text);
                decimal HoursExtra = Convert.ToDecimal(txtHE.Text);
                if (N_Payment.InserPayment(IdEmployer,HursOrdinary,HoursExtra, SalaryBase,CCSS, SalaryPay))
                {
                    MessageBox.Show("Pago Registrado Correctamente", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show("No se pudo Registrar el Pago", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }
        private void CleanFieldsPayment()
        {
            txtEmployerName.Text = "";
            txtSalaryBase.Text = "";
            txtSalaryPay.Text = "";
            txtHO.Text = "";
            txtHE.Text = "";
            txtSalaryBruto.Text = "";
            txtCCSS.Text = "";
            txtSalaryNeto.Text = "";
            dgvPayRoll.Columns.Clear();
            tabControlEmployers.SelectedIndex = 0;
        }

        #endregion

        #endregion
        #endregion

        //Logica para Mantenimiento de Aguinaldo
        #region Aguinaldo

        #region DataGrid Config
        private void DGVConfig()
        {
            dgvAguinaldo.DataSource = N_Payment.AllSalaryxEmployer(IdEmployer);
            
            //Modificar todas las columnas al ancho del Form
            dgvAguinaldo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //Centrar el Contenido de las Columnas
            dgvAguinaldo.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvAguinaldo.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            //Sumar todos los Montos del DataGridView
            decimal Total = 0;
            for (int i = 0; i < dgvAguinaldo.Rows.Count; i++)
            {
                Total += Convert.ToDecimal(dgvAguinaldo.Rows[i].Cells[1].Value);
            }
            Total = Total / 12;
            txtDispAgui.Text = Total.ToString("c");
        }
        #endregion

        #endregion

        //Logica para Mantenimiento de Vacaciones
        #region Vacaciones

        #region ViewsVacation
        private void ViewVacation() 
        {
            try
            {
                //Cargar el Datagrid
                DataTable dt = new DataTable();
                dt = N_Employer.GetVacationById(IdEmployer);
                Nvacaciones.Maximum = Convert.ToInt32(dt.Rows[0][2].ToString());
                dgvVacation1.DataSource = dt;

                //Cambiar Nombres del DataGrid
                dgvVacation1.Columns[0].HeaderText = "Id Vacacion";
                dgvVacation1.Columns[1].HeaderText = "Id Empleado";
                dgvVacation1.Columns[2].HeaderText = "Dias de Vacaciones";
                dgvVacation1.Columns[3].HeaderText = "Balance de Vacaciones";
                dgvVacation1.Columns[4].HeaderText = "Ultima Fecha de Actualizacion";

                //Modificar todas las columnas al ancho del Form
                dgvVacation1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                //Activar el ReadOnly
                dgvVacation1.ReadOnly = true;
            }
            catch (Exception)
            {
            }
          
        }

        #endregion

        #region InsertVacation
        private bool InsertVacaion() 
        {
            try
            {
                int IdEmployer = Convert.ToInt32(txtIdNew.Text);
                int Days = DaysVacation();
                Decimal Balance = BalanceVacation();
                if (N_Employer.InsertVacation(IdEmployer, Days, Balance))
                {
                    MessageBox.Show("Vacaciones Registradas Correctamente", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show("No se pudo Registrar las Vacaciones", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }   
        }

        private int DaysVacation() 
        {
            try
            {
                int monthsWorked = 0;

                // Obtener las fechas
                DateTime dateEntry = Convert.ToDateTime(dtDateofEntryNew.Value);
                DateTime dateNow = DateTime.Now;

                // Validar si el año de entrada es anterior al año actual
                if (dateEntry.Year < dateNow.Year)
                {
                    // Calcular los meses trabajados en el año de entrada
                    monthsWorked = (dateNow.Year - dateEntry.Year - 1) * 12;
                    monthsWorked += 12 - dateEntry.Month;
                    monthsWorked += dateNow.Month;
                }
                else
                {
                    // Calcular los meses desde la fecha de entrada hasta la fecha actual
                    monthsWorked = dateNow.Month - dateEntry.Month;
                }

                return monthsWorked;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private decimal BalanceVacation() 
        { 
            int Days = DaysVacation();
            decimal Balance = 0;
            string Option = cbPaymentNew.Text;
            Decimal SalaryxHour = Convert.ToDecimal(txtSalaryxHoursNew.Text);
            Decimal SalaryBase = Convert.ToDecimal(txtSalaryNew.Text);

            switch (Option)
            {
                case "Semanal":
                    Balance = (((SalaryxHour*48)/6)*Days);
                    break;
                case "Quincenal":
                    Balance = ((SalaryBase/30) * Days);
                    break;
                case "Mensual":
                    Balance = ((SalaryBase / 30) * Days);
                    break;
            }
            return Balance;
        }

        #endregion

        #region UpdateVacation
        private bool UpdateVacation()
        {
            try
            {
                bool Result = false;
                //Obtener los Datos de Las Vacaiones
                DataTable dt = N_Employer.GetVacationById(IdEmployer);
                DateTime LastUpdate = Convert.ToDateTime(dt.Rows[0][4].ToString());

                //Validar si el mes de la Ultima Modificacion es menor o Igual al Mes Actual
                if (LastUpdate.Month < DateTime.Now.Month)
                {
                    int DaysActually = Convert.ToInt32(dt.Rows[0][2].ToString());
                    int Days = UpdateDays(DaysActually);
                    decimal Balance = UpdateBalanceVacation(Days);
                    if (N_Employer.UpdateVacation(IdEmployer, Days, Balance))
                    {
                        MessageBox.Show("Vacaciones Actualizadas Correctamente", "Actualizacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Result = true;
                    }
                    else
                    {
                        MessageBox.Show("No se pudo Actualizar las Vacaciones", "Actualizacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Result =  false;
                    }
                }
                else
                {
                    Result = true;
                }
                return Result;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private int UpdateDays(int DaysActually) 
        {
            try
            {
                int monthsWorked = 0;
                return monthsWorked = DaysActually + 1;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private decimal UpdateBalanceVacation(int Days)
        {
            
            decimal Balance = 0;
            string Option = txtSalaryPay.Text;
            Decimal SalaryxHour = Convert.ToDecimal(dgvEmployers.CurrentRow.Cells[12].Value.ToString());
            Decimal SalaryBase = Convert.ToDecimal(dgvEmployers.CurrentRow.Cells[3].Value.ToString());

            switch (Option)
            {
                case "Semanal":
                    Balance = (((SalaryxHour * 48) / 6) * Days);
                    break;
                case "Quincenal":
                    Balance = ((SalaryBase / 30) * Days);
                    break;
                case "Mensual":
                    Balance = ((SalaryBase / 30) * Days);
                    break;
            }
            return Balance;
        }

       
        #endregion

        #region Dar Dias de Vacaciones

        private void btnGiveDays_Click(object sender, EventArgs e)
        {
            //Preguntar si desea dar los dias de vacaciones
            DialogResult result = MessageBox.Show("¿Desea Dar los Dias de Vacaciones?", "Vacaciones", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (GiveDayVacation())
                {
                    GeneratepdfVacation();
                    ViewVacation();
                }
            }
           
        }

        private bool GiveDayVacation() 
        {
            bool Result = false;
            if (Nvacaciones.Value>0)
            {
                //Obtener los Datos de Las Vacaiones
                DataTable dt = N_Employer.GetVacationById(IdEmployer);

                int DaysActually = Convert.ToInt32(dt.Rows[0][2].ToString());
                int Days = DaysActually - Convert.ToInt32(Nvacaciones.Value);
                decimal Balance = UpdateBalanceVacation(Days);
                if (N_Employer.UpdateVacation(IdEmployer, Days, Balance))
                {
                    MessageBox.Show("Vacaciones Actualizadas Correctamente", "Actualizacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Result = true;
                }
                else
                {
                    MessageBox.Show("No se pudo Actualizar las Vacaciones", "Actualizacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Result = false;
                }
            }
            else
            {
              MessageBox.Show("No se puede dar 0 dias de vacaciones", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Result;
        }

        #region Genarate PDF Vacation
        private void GeneratepdfVacation()
        {

            #region Crear el documento
            // Obtener el directorio del escritorio y las carpetas necesarias
            string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string carpetaVacaciones = Path.Combine(escritorio, "Vacaciones");
            string carpetaNombre = Path.Combine(carpetaVacaciones, txtEmployerName.Text);
            string NameFile = "Doc_Vacaciones" + txtEmployerName.Text + ".pdf";

            // Verificar si la carpeta "Proformas" existe, si no, crearla
            if (!Directory.Exists(carpetaVacaciones))
            {
                Directory.CreateDirectory(carpetaVacaciones);
            }

            // Verificar si la carpeta con el nombre existe, si no, crearla
            if (!Directory.Exists(carpetaNombre))
            {
                Directory.CreateDirectory(carpetaNombre);
            }

            // Crear la ruta completa del archivo PDF
            string rutaArchivoPDF = Path.Combine(carpetaNombre, NameFile);

            Document document = new Document();
            // Crea un nuevo objeto PdfWriter para escribir el documento en un archivo
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(rutaArchivoPDF, FileMode.Create));

            // Asigna el objeto PdfWriter al documento
            document.Open();
            #endregion

            #region Encabezado
            // Crea una tabla con dos columnas
            PdfPTable Encabezado = new PdfPTable(2);
            Encabezado.WidthPercentage = 120;


            // Agrega la imagen a la primera celda
            // Agrega la imagen a la primera celda
            string rutaLogo = "";
            if (CompanyCache.IdCompany == 31025820)
            {
                //Obtener la Ruta de la Carpeta bin
                string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                string Url = "\\Images\\Logos\\AluviLogo.png";
                rutaLogo = ruta + Url;

            }
            if (CompanyCache.IdCompany == 205150849)
            {
                //Obtener la Ruta de la Carpeta bin
                string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                string Url = "\\Images\\Logos\\MakyLogo.png";
                rutaLogo = ruta + Url;
            }
            if (CompanyCache.IdCompany == 112540885)
            {
                //Obtener la Ruta de la Carpeta bin
                string ruta = Path.GetDirectoryName(Application.ExecutablePath);
                string Url = "\\Images\\Logos\\VidriosAlturaLogo.png";
                rutaLogo = ruta + Url;
            }
            PdfPCell imageCell = new PdfPCell(iTextSharp.text.Image.GetInstance(rutaLogo));
            imageCell.Border = PdfPCell.NO_BORDER;
            imageCell.FixedHeight = 120f; // Ajusta la altura de la imagen
            imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
            Encabezado.AddCell(imageCell);

            // Crea un nuevo objeto Font para los textos
            iTextSharp.text.Font titleFont = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 19, iTextSharp.text.Font.BOLD, BaseColor.GRAY);
            iTextSharp.text.Font textFont = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            // Agrega los textos a la segunda celda
            PdfPCell textCell = new PdfPCell();
            textCell.Border = PdfPCell.NO_BORDER;

            // Alinea el contenido de la celda al centro
            textCell.HorizontalAlignment = Element.ALIGN_RIGHT;

            // Agrega el párrafo y los chunks al documento
            Paragraph paragraph = new Paragraph();
            paragraph.Add(new Chunk(CompanyCache.Name, titleFont));
            paragraph.Add(Chunk.NEWLINE);// Salto de línea
            paragraph.Add(new Chunk("Cedula Juridica " + CompanyCache.IdCompany.ToString(), textFont));
            paragraph.Add(Chunk.NEWLINE);
            paragraph.Add(new Chunk("Ubicados en: " + CompanyCache.Address, textFont));
            paragraph.Add(Chunk.NEWLINE);
            paragraph.Add(new Chunk("Teléfonos: " + CompanyCache.Phone, textFont));
            paragraph.Add(Chunk.NEWLINE);// Salto de línea
            paragraph.Add(Chunk.NEWLINE);// Salto de línea


            textCell.AddElement(paragraph);
            Encabezado.AddCell(textCell);

            // Establece el ancho de la celda de la tabla (ajusta según tus necesidades)
            Encabezado.SetWidths(new float[] { 3f, 4f }); // Primer valor es el ancho de la celda de la imagen

            // Agrega la tabla al documento
            document.Add(Encabezado);

            // Añade la palabra "COTIZACIÓN" debajo de la tabla
            Paragraph cotizacionParagraph = new Paragraph("Vacaciones", titleFont);
            cotizacionParagraph.Alignment = Element.ALIGN_CENTER;
            document.Add(cotizacionParagraph);
            document.Add(new Paragraph(" "));// Esto agrega un espacio en blanco en el documento

            #endregion

            #region Datos del Empleado
            // Crea una tabla con dos columnas
            PdfPTable DatosEmpleado = new PdfPTable(2);
            DatosEmpleado.WidthPercentage = 100;
            DatosEmpleado.HorizontalAlignment = Element.ALIGN_LEFT;
            DatosEmpleado.SetWidths(new float[] { 1f, 1f }); // Primer valor es el ancho de la celda de la imagen

            document.Add(DatosEmpleado);

            // Agregar los datos del Empleado Como Nombre Salario y  Tipo de Planilla
            DatosEmpleado.AddCell(new Phrase("Nombre: ", textFont));
            DatosEmpleado.AddCell(new Phrase(txtEmployerName.Text, textFont));
            DatosEmpleado.AddCell(new Phrase("Salario Base: ", textFont));
            DatosEmpleado.AddCell(new Phrase(txtSalaryBase.Text, textFont));
            DatosEmpleado.AddCell(new Phrase("Tipo de Planilla: ", textFont));
            DatosEmpleado.AddCell(new Phrase(txtSalaryPay.Text, textFont));

            // Agregar la tabla al documento
            document.Add(DatosEmpleado);
            #endregion

            #region Datos de la Planilla
            //Salto Linea
            document.Add(new Paragraph(" "));

            //Agregar un Parrafo donde se Diga que se le van a dar cierta cantidad de dias al empleado tal y el monto a pagar por esos dias es de tal
            Paragraph DetallesPlanilla = new Paragraph("Al Empleado: "+txtEmployerName.Text+" Se le van a dar " + Nvacaciones.Value + " dias de Vacaciones, el monto a pagar por esos dias es de ₡" + txtSalaryforDay.Text, textFont);
            DetallesPlanilla.Alignment = Element.ALIGN_LEFT;
            document.Add(DetallesPlanilla);
            document.Add(new Paragraph(" "));

            //Agregar un campo para La firma del Empleado
            Paragraph FirmaEmpleado = new Paragraph("Firma del Empleado:______________________________________ ", textFont);
            document.Add(FirmaEmpleado);




            #endregion

            #region Cerrar el documento
            // Cerrar el documento
            document.Close();
            #endregion



        }

        #endregion

        #endregion

        #endregion

        //Abrir Formulario de Ayuda para la 24Hrs
        private void button1_Click(object sender, EventArgs e)
        {
            frmInfo24Hrs frm = new frmInfo24Hrs();
            frm.Show();
        }

        private void eliminarEmpleadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //SELECCIONAR EL ID DEL EMPLEADO
            IdEmployer = Convert.ToInt32(dgvEmployers.CurrentRow.Cells[0].Value);
            if (N_Employer.DeleteEmployer(IdEmployer))
            {
                MessageBox.Show("Empleado Eliminado Correctamente", "Eliminacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadEmployers();
            }
        }

        private void cbkccss_CheckedChanged(object sender, EventArgs e)
        {
            //Si esta Seleccionado Restarle lo de la CCSS al Salario
            if (cbkccss.Checked)
            {
                SalaryPay = SalaryBase-CCSS;
                //Mostrar el Monto de la CCSS
                txtCCSS.Text = CCSS.ToString("c");
                //Mostrar el Salario Neto
                txtSalaryNeto.Text = SalaryPay.ToString("c");

            }
            else
            {

                SalaryPay = SalaryBase;
                //Dejar en cero el Textbox de la CCSS
                txtCCSS.Text = "0";
                //Mostrar el Salario Neto
                txtSalaryNeto.Text = SalaryPay.ToString("c");
            }
        }
    }
}