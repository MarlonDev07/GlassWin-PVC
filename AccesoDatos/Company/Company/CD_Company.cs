using System;
using System.Data;
using System.Data.SqlClient;

namespace AccesoDatos.Company
{
    public class CD_Company
    {
        public DataTable ViewCompany()
        {
            DataBase.ClsConnection Cnn = new DataBase.ClsConnection();
            DataTable dataTable = new DataTable();
            SqlDataReader Read;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Cnn.OpenConecction();
            string query = "SELECT * FROM Company";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            Read = cmd.ExecuteReader();
            dataTable.Load(Read);
            Cnn.CloseConnection();
            return dataTable;
        }

        public DataTable BuscarCompany(long idCompany)
        {
            DataBase.ClsConnection Cnn = new DataBase.ClsConnection();
            DataTable dataTable = new DataTable();
            SqlDataReader Read;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Cnn.OpenConecction();

            string query = "SELECT * FROM Company WHERE idCompany = @idCompany";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            // Agregar el parámetro al comando
            cmd.Parameters.AddWithValue("@idCompany", idCompany);

            Read = cmd.ExecuteReader();
            dataTable.Load(Read);

            Cnn.CloseConnection();
            return dataTable;
        }



        public bool Create(Int64 ID, Int64 IDCompany, string phone, string Address, string Url, string Name)
        {
            try
            {
                DataBase.ClsConnection Cnn = new DataBase.ClsConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "SP_InsertCompany";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idUser", ID);
                cmd.Parameters.AddWithValue("@idCompany", IDCompany);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@Url", Url);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Update(int ID, int IDCompany, string phone, string Address, string Url, string Name)
        {
            try
            {
                DataBase.ClsConnection Cnn = new DataBase.ClsConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "SP_UpdateCompany";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idUser", ID);
                cmd.Parameters.AddWithValue("@idCompany", IDCompany);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@Url", Url);
                cmd.Parameters.AddWithValue("@Name", Name);
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
