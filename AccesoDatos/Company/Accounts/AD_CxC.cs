using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccesoDatos.Company.Bill.Accounts
{
    public class AD_CxC
    {
        private DataBase.ClsConnection Cnn;
        public AD_CxC() 
        { 
         Cnn = new DataBase.ClsConnection();
        }

        public bool InsertCxC(int IdBill, decimal InitialAmount, decimal OutstandingBalance,string Proyecto)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "InsertAccountReceivable";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdBill", IdBill);
                cmd.Parameters.AddWithValue("@InitialAmount", InitialAmount);
                cmd.Parameters.AddWithValue("@OutstandingBalance", OutstandingBalance);
                cmd.Parameters.AddWithValue("@Proyecto", Proyecto);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public DataTable LoadCxC()
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Cnn.OpenConecction();
            cmd.CommandText = "select * from AccountReceivable";
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            Cnn.CloseConnection();
            return dt;
        }

        public bool UpdateCxC(int IdAccount, int IdBill, decimal InitialAmount, decimal OutstandingBalance)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "UpdateAccountReceivable";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdAccount", IdAccount);
                cmd.Parameters.AddWithValue("@IdBill", IdBill);
                cmd.Parameters.AddWithValue("@InitialAmount", InitialAmount);
                cmd.Parameters.AddWithValue("@OutstandingBalance", OutstandingBalance);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public DataTable FindCxCforClient(int IdClient)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = @"
                    SELECT 
                        ar.IdAccount,
                        ar.IdBill,
                        ar.InitialAmount,
                        ar.OutstandingBalance,
                        ar.Proyecto,
                        b.Date,
                        b.ExprirationDate
                    FROM 
                        AccountReceivable ar
                    JOIN 
                        Bill b ON ar.IdBill = b.IdBill
                    WHERE 
                        b.IdClient = @IdClient";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IdClient", IdClient);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                Cnn.CloseConnection();
                return dt;
            }
            catch (Exception)
            {

                return null;
            }

        }

        public void ActualizarFechaVencimiento(int idCuenta, DateTime nuevaFecha)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "UPDATE Bill SET ExprirationDate = @nuevaFecha WHERE IdBill = (SELECT IdBill FROM AccountReceivable WHERE IdAccount = @idCuenta)";
                cmd.Parameters.AddWithValue("@nuevaFecha", nuevaFecha);
                cmd.Parameters.AddWithValue("@idCuenta", idCuenta);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la fecha de vencimiento: " + ex.Message);
            }
        }



        public int LastIdCxC()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "select max(IdAccount) from AccountReceivable";
                cmd.CommandType = CommandType.Text;
                int id = Convert.ToInt32(cmd.ExecuteScalar());
                Cnn.CloseConnection();
                return id;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public bool DeleteCxC(int IdAccount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                string query = "delete from AccountReceivable where IdAccount = @IdAccount";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IdAccount", IdAccount);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
