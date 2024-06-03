using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using AccesoDatos.Company.Employer;
namespace Negocio.Company.Employer
{
    public class N_Payment
    {
        AD_Payment AD_Payment = new AD_Payment();

        public bool InserPayment(int IdEmployer, decimal HoursOrdinary, decimal HoursExtra, decimal SalaryBase, decimal Deduccion, decimal SalaryNeto)
        {
            return AD_Payment.InserPayment(IdEmployer, HoursOrdinary, HoursExtra, SalaryBase, Deduccion, SalaryNeto);
        }

        public DataTable AllSalaryxEmployer(int IdEmployer)
        {
            try
            {
                DataTable dt = AD_Payment.AllSalaryxEmployer(IdEmployer);

                DataTable dtSalary = new DataTable();
                dtSalary.Columns.Add("Mes", typeof(string));
                dtSalary.Columns.Add("Salario", typeof(decimal));

                // Recorremos el DataTable para obtener los datos de la tabla
                foreach (DataRow row in dt.Rows)
                {
                    DateTime date = Convert.ToDateTime(row["DatePayment"]);
                    string month = date.ToString("MMMM");

                    // Buscamos el mes en dtSalary
                    DataRow[] foundRows = dtSalary.Select("Mes = '" + month + "'");

                    if (foundRows.Length > 0)
                    {
                        // Si el mes ya existe en dtSalary, sumamos el salario
                        decimal salary = Convert.ToDecimal(foundRows[0]["Salario"]);
                        decimal salaryTotal = salary + Convert.ToDecimal(row["SalaryNeto"]);
                        foundRows[0]["Salario"] = salaryTotal;
                    }
                    else
                    {
                        // Si el mes no existe en dtSalary, lo agregamos
                        dtSalary.Rows.Add(month, Convert.ToDecimal(row["SalaryNeto"]));
                    }
                }

                return dtSalary;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public decimal SelectAllSellByEmployer(int IdEmployer, string Planilla, out string Message) 
        {
            try
            {
                decimal total = 0;
                DateTime DateEnd = DateTime.Now;
                DateTime DateStar = new DateTime();
                Message = "";
                switch (Planilla)
                {
                    case "Semanal":
                        DateStar = DateEnd.AddDays(-7);
                        break;
                    case "Quincenal":
                        DateStar = DateEnd.AddDays(-15);
                        break;
                    case "Mensual":
                        DateStar = DateEnd.AddDays(-30);
                        break;
                    default:
                        break;
                }
                DataTable dt = AD_Payment.SelectAllSellByEmployer(IdEmployer, DateStar, DateEnd);
                foreach (DataRow row in dt.Rows)
                {
                    total += Convert.ToDecimal(row["Cantidad"]);
                }
                return total;
              
            }
            catch (Exception)
            {

                Message = "Error al calcular el total de ventas";
                return 0;
            }
        
        }



    }
}
