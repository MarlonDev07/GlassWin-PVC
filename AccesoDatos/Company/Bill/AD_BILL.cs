using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Company.Bill
{
    public class AD_BILL
    {
        private DataBase.ClsConnection Cnn;

        public AD_BILL()
        {
            Cnn = new DataBase.ClsConnection();
        }
        public DataTable readBill()
        {
            DataTable dataTable = new DataTable();
            string query = "select * from Bill";
            SqlCommand cmd = new SqlCommand(query, Cnn.OpenConecction());
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dataTable);
            Cnn.CloseConnection();
            return dataTable;
        }

        public bool InsertBill(int IdQuote, int IdClient,DateTime Date, DateTime DateExpiration )
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                if (IdQuote != 0)
                {
                    cmd.CommandText = "InsertBill";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdQuote", IdQuote);
                }
                if (IdQuote == 0)
                {
                    //Inserta una factura en la tabla con un insert normal
                    cmd.CommandText = "Insert into Bill (IdClient,Date,ExprirationDate) Values (@IdClient,@Date,@ExprirationDate)";
                    
                    cmd.CommandType = System.Data.CommandType.Text;
                }            
                
                cmd.Parameters.AddWithValue("@IdClient", IdClient);
                cmd.Parameters.AddWithValue("@Date", Date);
                cmd.Parameters.AddWithValue("@ExprirationDate", DateExpiration);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public int getLastID ()
        {
            int id = 0;
            string query = "select max(IdBill) from Bill";
            SqlCommand cmd = new SqlCommand(query, Cnn.OpenConecction());
            cmd.CommandType = CommandType.Text;
            id = Convert.ToInt32(cmd.ExecuteScalar());
            Cnn.CloseConnection();
            return id;
        }

        public int getIdClient(int IdBill)
        {
            int id = 0;
            string query = "select IdClient from Bill where IdBill = @IdBill";
            SqlCommand cmd = new SqlCommand(query, Cnn.OpenConecction());
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@IdBill", IdBill);
            id = Convert.ToInt32(cmd.ExecuteScalar());
            Cnn.CloseConnection();
            return id;
        }

        public bool InsertSell(int idEmployer , decimal Amount) 
        { 
            //Inserta una venta en la tabla 
            try
            {
                SqlCommand cmd = new SqlCommand(); 
                cmd.Connection = Cnn.OpenConecction();
                string Query = "Insert Into Ventas (IdEmpleado,IdCompany,Cantidad) Values (@IdEmployer, @IdCompany,@Cantidd)";
                cmd.CommandText = Query;
                cmd.Parameters.AddWithValue("@IdEmployer", idEmployer);
                cmd.Parameters.AddWithValue("@IdCompany", CompanyCache.IdCompany);
                cmd.Parameters.AddWithValue("@Cantidd", Amount);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true; 
            } 
            catch (Exception)
            {
                return false; 
            }
            
        }

        public DataTable SelectEmployersSeller() 
        {
            try
            {
                DataTable dataTable = new DataTable();
                string query = "select EmployeeID, FirstName from Employer where IdCompany = @IdCompany and Position = 'Vendedor'";
                SqlCommand cmd = new SqlCommand(query, Cnn.OpenConecction());
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IdCompany", CompanyCache.IdCompany);
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

        public bool DeleteBill(int IdBill)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                string query = "delete from Bill where IdBill = @IdBill";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IdBill", IdBill);
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
