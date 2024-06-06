using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Company.Accounts
{
    public class AD_CxP
    {
        private DataBase.ClsConnection Cnn;
        public AD_CxP()
        {
            Cnn = new DataBase.ClsConnection();
        }

        //Insertar CxP
        public bool InsertCxP(string Supplyer, decimal AmountInitial, string Datail)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "Insert Into AccountPayable(Supplyer,AmountInitial,State,AmountPending,Detail,IdCompany) Values (@Supplyer,@AmountInitial,'Pendiente',@AmountPending,@Detail,@IdCompany)";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Supplyer", Supplyer);
                cmd.Parameters.AddWithValue("@AmountInitial", AmountInitial);
                cmd.Parameters.AddWithValue("@Detail", Datail);
                cmd.Parameters.AddWithValue("@IdCompany", CompanyCache.IdCompany);
                cmd.Parameters.AddWithValue("@AmountPending", AmountInitial);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public DataTable GetSupplyers()
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Cnn.OpenConecction();
            cmd.CommandText = "select distinct Supplyer from AccountPayable where IdCompany = @IdCompany";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@IdCompany", CompanyCache.IdCompany);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            Cnn.CloseConnection();
            return dt;
        }


        //Seleccionar CxP 
        public DataTable SelectBySupplyer(string Supplyer)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Cnn.OpenConecction();
            cmd.CommandText = "select * from AccountPayable where IdCompany = @IdCompany and Supplyer = @Supplyer";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@IdCompany", CompanyCache.IdCompany);
            cmd.Parameters.AddWithValue("@Supplyer", Supplyer);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            Cnn.CloseConnection();
            return dt;
        }
        public DataTable SelectAll()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "select * from AccountPayable where IdCompany = @IdCompany";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IdCompany", CompanyCache.IdCompany);
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
        //Actualizar CxP
        public bool UpdateCxP(int IdAccount, decimal AmountPending)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                string sql = "Update AccountPayable set AmountPending = @AmountPending where IdAccount = @IdAccount"; 
                cmd.CommandText = sql;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@IdAccount", IdAccount);
                cmd.Parameters.AddWithValue("@AmountPending", AmountPending);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public int LastIdCxP()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "select top 1 IdAccount from AccountPayable order by IdAccount desc";
                cmd.CommandType = CommandType.Text;
                int IdAccount = Convert.ToInt32(cmd.ExecuteScalar());
                Cnn.CloseConnection();
                return IdAccount;
            }
            catch (Exception)
            {

                return 0;
            }
        }
    }
}
