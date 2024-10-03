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
        public long InsertCompany2(long idUser, long idCompany, string phone, string address, string url, string name)
        {
            DataBase.ClsConnection Cnn = new DataBase.ClsConnection();
            using (SqlConnection connection = Cnn.OpenConecction()) // 'Cnn.OpenConecction()' es tu método para abrir la conexión.
            {
                using (SqlCommand command = new SqlCommand(
                    @"INSERT INTO dbo.Company2 (idUser, IdCompany, Phone, Address, URL, Name) 
              VALUES (@idUser, @IdCompany, @Phone, @Address, @URL, @Name);
              SELECT SCOPE_IDENTITY();", connection))
                {
                    command.CommandType = System.Data.CommandType.Text;

                    // Agregar los parámetros con sus tipos
                    command.Parameters.Add("@idUser", SqlDbType.BigInt).Value = idUser == 0 ? DBNull.Value : (object)idUser;  // Permite valores NULL si se pasa 0
                    command.Parameters.Add("@IdCompany", SqlDbType.BigInt).Value = idCompany == 0 ? DBNull.Value : (object)idCompany;  // Permite valores NULL si se pasa 0
                    command.Parameters.Add("@Phone", SqlDbType.VarChar, 20).Value = string.IsNullOrEmpty(phone) ? DBNull.Value : (object)phone;
                    command.Parameters.Add("@Address", SqlDbType.VarChar, 100).Value = string.IsNullOrEmpty(address) ? DBNull.Value : (object)address;
                    command.Parameters.Add("@URL", SqlDbType.VarChar, 1000).Value = string.IsNullOrEmpty(url) ? DBNull.Value : (object)url;
                    command.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = string.IsNullOrEmpty(name) ? DBNull.Value : (object)name;

                    // Ejecutar el comando y obtener el último ID insertado
                    long ultimoIDInsertado = Convert.ToInt64(command.ExecuteScalar());

                    return ultimoIDInsertado;
                }
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
