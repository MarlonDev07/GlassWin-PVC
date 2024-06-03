using Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AccesoDatos.Client
{
    public class AD_Client
    {
        DataBase.ClsConnection Cnn = new DataBase.ClsConnection();

        public DataTable LoadClient()
        {
            try
            {
                DataTable dataTable = new DataTable();
                SqlDataReader Read;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "Select * from Client WHERE IdCompany = @Id and Estado = 'ACTIVO'";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", CompanyCache.IdCompany);
                Read = cmd.ExecuteReader();
                dataTable.Load(Read);
                Cnn.CloseConnection();
                return dataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable FindClient(int ID)
        {
            try
            {
                DataTable dataTable = new DataTable();
                SqlDataReader Read;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "Select * from Client where idClient = @ID and IdCompany = @IDc";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@IDc", CompanyCache.IdCompany);
                Read = cmd.ExecuteReader();
                dataTable.Load(Read);
                Cnn.CloseConnection();
                return dataTable;
            }
            catch (Exception )
            {
               return null;
            }
        }

        public List<clsClient> ListClient(Int64 id)
        {
            try
            {
                List<clsClient> List = new List<clsClient>();
                using (SqlConnection connection = Cnn.OpenConecction())
                {
                    using (SqlCommand cmd = new SqlCommand("Select * from Client where IdClient = @ID and IdCompany = @IDc", connection))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@IDc", CompanyCache.IdCompany);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clsClient client = new clsClient
                                {
                                    IdClient = reader.GetInt32(0),
                                    IdCompany = reader.GetInt64(3),
                                    Name = reader.GetString(1),
                                    Phone = reader.GetString(2),
                                    Address = reader.GetString(4),
                                    Correo = reader.GetString(5)
                                };
                                List.Add(client);
                            }
                        }
                    }
                }
                return List;
            }
            catch (Exception )
            {
                return null;
            }
        }

        public bool Create (string Name, string Phone, string Address, string Email)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "SP_CreateClient";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idCompany", CompanyCache.IdCompany);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Phone", Phone);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception )
            {
                return false;
            }
        }

        public bool Update(int ID,  string Name, string Phone, string Address, string Email)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "SP_EditClient";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idClient", ID);
                cmd.Parameters.AddWithValue("@idCompany", CompanyCache.IdCompany);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Phone", Phone);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception )
            {
                return false;
            }
        }

        public bool DeleteClientData (int ID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "DeleteClientData";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClientId", ID);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception )
            {
                return false;
            }
        }


    }
}
