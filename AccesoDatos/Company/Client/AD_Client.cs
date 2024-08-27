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
                cmd.CommandText = "SELECT \r\n    c.IdClient,\r\n    c.Name,\r\n    c.Phone,\r\n    c.IdCompany,\r\n    c.Address,\r\n    c.Correo,\r\n    c.Estado,\r\n    c.Registro,\r\n    c.LimiteCredito,\r\n    c.FechaVencimiento,\r\n    c.Dias,\r\n    COALESCE(SUM(ar.OutstandingBalance), 0) AS TotalOutstandingBalance\r\nFROM \r\n    Client c\r\nLEFT JOIN \r\n    Bill b ON c.IdClient = b.IdClient\r\nLEFT JOIN \r\n    AccountReceivable ar ON b.IdBill = ar.IdBill\r\nWHERE \r\n    c.IdCompany = @Id \r\n    AND c.Estado = 'ACTIVO'\r\nGROUP BY \r\n    c.IdClient,\r\n    c.Name,\r\n    c.Phone,\r\n    c.IdCompany,\r\n    c.Address,\r\n    c.Correo,\r\n    c.Estado,\r\n    c.Registro,\r\n    c.LimiteCredito,\r\n    c.FechaVencimiento,\r\n    c.Dias\r\n";
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
                                    Correo = reader.GetString(5),
                                    Limite = reader.GetString(8),
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

        public bool Create (string Name, string Phone, string Address, string Email, string Limite, DateTime fechaVencimiento, int dias)
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
                cmd.Parameters.AddWithValue("@Limite", Limite);
                cmd.Parameters.AddWithValue("@FechaVencimiento", fechaVencimiento);
                cmd.Parameters.AddWithValue("@Dias", dias);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception )
            {
                return false;
            }
        }

        public bool Update(int ID, string Name, string Phone, string Address, string Email, string Limite, DateTime fechaVencimiento, int dias)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "SP_EditClient";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idClient", ID);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Phone", Phone);
                cmd.Parameters.AddWithValue("@idCompany", CompanyCache.IdCompany);
                cmd.Parameters.AddWithValue("@Address", Address);
                 cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Limite", Limite);
                cmd.Parameters.AddWithValue("@FechaVencimiento", fechaVencimiento);
                cmd.Parameters.AddWithValue("@Dias", dias);
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

        public DataTable CargarProformasCliente(int IdCliente) 
        {
            try
            {
                DataTable dataTable = new DataTable();
                SqlDataReader Read;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "SELECT B.*, Q.* FROM Bill B INNER JOIN Quote Q ON B.IdQuote = Q.IdQuote WHERE B.IdClient = @ID;";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", IdCliente);
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


    }
}
