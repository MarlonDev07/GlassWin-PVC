using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Company.Employer
{
    public class AD_Payment
    {
        //crear Conexion a la base de datos
        DataBase.ClsConnection Cnn = new DataBase.ClsConnection();

        // Método para insertar un nuevo Empleado
        public bool InserPayment(int IdEmployer,decimal HoursOrdinary, decimal HoursExtra,decimal SalaryBase,decimal Deduccion, decimal SalaryNeto) 
        {
			try
			{
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "INSERT INTO Payment (IdEmployer, HoursOrdinary, HoursExtra, SalaryBase, Deduccion, SalaryNeto) " +
                                  "VALUES (@IdEmployer, @HoursOrdinary, @HoursExtra, @SalaryBase, @Deduccion, @SalaryNeto)";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@IdEmployer", IdEmployer);
                cmd.Parameters.AddWithValue("@HoursOrdinary", HoursOrdinary);
                cmd.Parameters.AddWithValue("@HoursExtra", HoursExtra);
                cmd.Parameters.AddWithValue("@SalaryBase", SalaryBase);
                cmd.Parameters.AddWithValue("@Deduccion", Deduccion);
                cmd.Parameters.AddWithValue("@SalaryNeto", SalaryNeto);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
			}
			catch (Exception)
			{
                return false;
			}
        }

        public DataTable AllSalaryxEmployer(int IdEmployer)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Cnn.OpenConecction();
            cmd.CommandText = "SELECT * FROM Payment WHERE IdEmployer = @IdEmployer";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@IdEmployer", IdEmployer);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Cnn.CloseConnection();
            return dt;
        }

        public DataTable SelectAllSellByEmployer(int IdEmployer, DateTime DateStar, DateTime DateEnd) 
         {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "SELECT * FROM Ventas WHERE IdEmpleado = @IdEmployer AND Fecha BETWEEN @DateStar AND @DateEnd";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@IdEmployer", IdEmployer);
                cmd.Parameters.AddWithValue("@DateStar", DateStar);
                cmd.Parameters.AddWithValue("@DateEnd", DateEnd);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Cnn.CloseConnection();
                return dt;

            }
            catch (Exception)
            {

                throw;
            }
        }



    }
}
