using System;
using System.Data;
using System.Data.SqlClient;


namespace AccesoDatos.Company.Employer
{
    public class AD_Employer
    {
        //crear Conexion a la base de datos
        DataBase.ClsConnection Cnn = new DataBase.ClsConnection();

        //Control Empleado
        #region Control Empleado

        // Método para insertar un nuevo Empleado
        public bool InsertEmployer(int ID, string name, string lastName, string salary, string date, string DateofBirth, string DateofEntry, string Phone, string Email, string Address, string Position, string Payment, decimal PaymentHours)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "INSERT INTO Employer (EmployeeID, FirstName, LastName, Salary,  IdCompany, DateofBirth, DateofEntry, Phone, Email, Address, Position, Payment, PaymentHours) " +
                                  "VALUES (@EmployeeID, @FirstName, @LastName, @Salary,  @IdCompany, @DateofBirth, @DateofEntry, @Phone, @Email, @Address, @Position, @Payment, @PaymentHours)";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@EmployeeID", ID);
                cmd.Parameters.AddWithValue("@FirstName", name);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@Salary", Convert.ToDecimal(salary));
                cmd.Parameters.AddWithValue("@IdCompany", CompanyCache.IdCompany);
                cmd.Parameters.AddWithValue("@DateofBirth", Convert.ToDateTime(DateofBirth));
                cmd.Parameters.AddWithValue("@DateofEntry", Convert.ToDateTime(DateofEntry));
                cmd.Parameters.AddWithValue("@Phone", Phone);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@Position", Position);
                cmd.Parameters.AddWithValue("@Payment", Payment);
                cmd.Parameters.AddWithValue("@PaymentHours", PaymentHours);

                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Método para actualizar un Empleado
        public bool UpdateEmployer(int ID, string name, string lastName, string salary, string date, string DateofBirth, string DateofEntry, string Phone, string Email, string Address, string Position, string Payment, decimal PaymentHours)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
               cmd.CommandText = "UPDATE Employer SET FirstName = @FirstName, LastName = @LastName, Salary = @Salary, DateofBirth = @DateofBirth, DateofEntry = @DateofEntry, Phone = @Phone, Email = @Email, Address = @Address, Position = @Position, Payment = @Payment, PaymentHours = @PaymentHours WHERE EmployeeID = @EmployeeID AND IdCompany = @IdCompany";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@EmployeeID", ID);
                cmd.Parameters.AddWithValue("@FirstName", name);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@Salary", Convert.ToDecimal(salary));
                cmd.Parameters.AddWithValue("@IdCompany", CompanyCache.IdCompany);
                cmd.Parameters.AddWithValue("@DateofBirth", Convert.ToDateTime(DateofBirth));
                cmd.Parameters.AddWithValue("@DateofEntry", Convert.ToDateTime(DateofEntry));
                cmd.Parameters.AddWithValue("@Phone", Phone);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@Position", Position);
                cmd.Parameters.AddWithValue("@Payment", Payment);
                cmd.Parameters.AddWithValue("@PaymentHours", PaymentHours);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Método para eliminar un Empleado
        public bool DeleteEmpleado(int IdEmployer)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "SP_EliminarEmpleado";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdEmpleado", IdEmployer);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Método para obtener todos los Empleados
        public DataTable GetEmployer()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Cnn.OpenConecction();
            cmd.CommandText = "SELECT * FROM Employer WHERE IdCompany = @IdCompany";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@IdCompany", CompanyCache.IdCompany);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Cnn.CloseConnection();
            return dt;
        }

        // Método para obtener un Empleado por su ID
        public DataTable GetEmployerById(int ID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Cnn.OpenConecction();
            cmd.CommandText = "SELECT * FROM Employer WHERE EmployeeID = @ID AND IdCompany = @IdCompany";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.AddWithValue("@IdCompany", CompanyCache.IdCompany);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Cnn.CloseConnection();
            return dt;
        }

        // Método para obtener un Empleado por su nombre
        public DataTable GetEmployerByName(string name)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Cnn.OpenConecction();
            cmd.CommandText = "SELECT * FROM Employer WHERE FirstName = @name AND IdCompany = @IdCompany";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@IdCompany", CompanyCache.IdCompany);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Cnn.CloseConnection();
            return dt;
        }

        // Método para obtener todos los ID de los Empleados
        public DataTable GetEmployerID()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Cnn.OpenConecction();
            cmd.CommandText = "SELECT EmployeeID FROM Employer WHERE IdCompany = @IdCompany";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@IdCompany", CompanyCache.IdCompany);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Cnn.CloseConnection();
            return dt;
        }
        #endregion

        //Control Vacaciones
        #region Control Vacaciones
        public DataTable GetVacationById(int Id)
        {
            try
            {
                DataTable dataTable = new DataTable();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "SELECT * FROM ControlVacation WHERE IdEmployer = @Id";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", Id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataTable);
                Cnn.CloseConnection();
                return dataTable;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool InsertVacation(int IdEmployer,int VacationDays,decimal VacationBalance) 
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "INSERT INTO ControlVacation (IdEmployer, VacationDays, VacationBalance) VALUES (@IdEmployr, @VacationDays, @VacationBalance)";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@IdEmployr", IdEmployer);
                cmd.Parameters.AddWithValue("@VacationDays", VacationDays);
                cmd.Parameters.AddWithValue("@VacationBalance", VacationBalance);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateVacation(int IdEmployer, int VacationDays, decimal VacationBalance)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "UPDATE ControlVacation SET VacationDays = @VacationDays, VacationBalance = @VacationBalance WHERE IdEmployer = @IdEmployr";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@IdEmployr", IdEmployer);
                cmd.Parameters.AddWithValue("@VacationDays", VacationDays);
                cmd.Parameters.AddWithValue("@VacationBalance", VacationBalance);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

      
        #endregion

        //Control Aguinaldo
        #region Control Aguinaldo
        public DataTable GetAguinaldoById(int id) 
        {
            try
            {
                DataTable dataTable = new DataTable();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "SELECT * FROM Aguinaldo WHERE IdEmployr = @Id";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataTable);
                Cnn.CloseConnection();
                return dataTable;
            }
            catch (Exception)
            {
                return null;
            }

            }

        public bool InsertAguinaldo(int IdEmployer,DateTime Year, decimal BalanceAguinaldo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "INSERT INTO Aguinaldo (IdEmployr, Year, BalanceAguinaldo) VALUES (@IdEmployr, @Year, @BalanceAguinaldo)";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@IdEmployr", IdEmployer);
                cmd.Parameters.AddWithValue("@Year", Year);
                cmd.Parameters.AddWithValue("@BalanceAguinaldo", BalanceAguinaldo);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateAguinaldo(int IdEmployer, DateTime Year, decimal BalanceAguinaldo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "UPDATE Aguinaldo SET Year = @Year, BalanceAguinaldo = @BalanceAguinaldo WHERE IdEmployr = @IdEmployr";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@IdEmployr", IdEmployer);
                cmd.Parameters.AddWithValue("@Year", Year);
                cmd.Parameters.AddWithValue("@BalanceAguinaldo", BalanceAguinaldo);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
}
