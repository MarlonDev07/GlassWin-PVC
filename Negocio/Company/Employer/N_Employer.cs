using AccesoDatos.Company.Employer;
using System;
using System.Data;

namespace Negocio.Company.Employer
{
    public class N_Employer
    {
        AD_Employer AD_Employer = new AD_Employer();

        //Control Empleado
        #region Control Empleado
        public bool InsertEmployer(int ID, string name, string lastName, string salary, string date, string DateofBirth, string DateofEntry, string Phone, string Email, string Address, string Position, string Payment, decimal PaymentHours)
        {
           return AD_Employer.InsertEmployer(ID, name, lastName, salary, date, DateofBirth, DateofEntry, Phone, Email, Address, Position, Payment, PaymentHours);
        }

        public bool UpdateEmployer(int ID, string name, string lastName, string salary, string date, string DateofBirth, string DateofEntry, string Phone, string Email, string Address, string Position, string Payment, decimal PaymentHours)
        {
           return AD_Employer.UpdateEmployer(ID, name, lastName, salary, date, DateofBirth, DateofEntry, Phone, Email, Address, Position, Payment, PaymentHours);
        }


        public DataTable LoadEmployer()
        {
            return AD_Employer.GetEmployer();
        }

        public DataTable GetEmployerById(int ID)
        {
            return AD_Employer.GetEmployerById(ID);
        }

        public DataTable GetEmployerID() 
        {
            return AD_Employer.GetEmployerID();
        }

        public bool DeleteEmployer(int ID)
        {
            return AD_Employer.DeleteEmpleado(ID);
        }
        #endregion

        //Control Vacaciones
        #region Control Vacaciones
        public DataTable GetVacationById(int ID)
        {
            //Crear DataTable
            DataTable dataTable = new DataTable();

            //Llenar DataTable con las Vacaciones del Empleado
            dataTable = AD_Employer.GetVacationById(ID);

            //Retornar DataTable
            return dataTable;
        }

        public bool InsertVacation(int IdEmployer, int VacationDays,decimal VacationBalance)
        {
            return AD_Employer.InsertVacation(IdEmployer, VacationDays, VacationBalance);
        }

        public bool UpdateVacation(int IdEmployer, int VacationDays, decimal VacationBalance)
        {
            return AD_Employer.UpdateVacation(IdEmployer, VacationDays, VacationBalance);
        }

        #endregion

        //Control Aguinaldo
        #region Control Aguinaldo
        public DataTable GetAguinaldoById(int ID)
        {
            //Crear DataTable
            DataTable dataTable = new DataTable();

            //Llenar DataTable con las Vacaciones del Empleado
            dataTable = AD_Employer.GetAguinaldoById(ID);

            //Retornar DataTable
            return dataTable;
        }

        public bool InsertAguinaldo(int IdEmployer, DateTime Year, decimal BalanceAguinaldo) 
        {
            try
            {
                return AD_Employer.InsertAguinaldo(IdEmployer, Year, BalanceAguinaldo);
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
                return AD_Employer.UpdateAguinaldo(IdEmployer, Year, BalanceAguinaldo);
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

    }
}
